using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using gloSecurity;
using System.Configuration;
using System.Collections;
using gloSettings;




namespace gloPM
{
    public partial class frmSplash : gloAUSLibrary.MasterForm
    {

        ////======================= Load our bitmaps
        //private Bitmap bmpFrmBack = new Bitmap(gloGlobal.Properties.Resources.SplashTransperentFront);
        //private Bitmap bmpFrmWhite = new Bitmap(gloGlobal.Properties.Resources.whitebitmap);

        //private bool bAfterLoad = false;
        ////===========================================


        #region 'Declarations'

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _MessageBoxCaption = "gloPM";
        private string _databaseconnectionstring = "";
        private static int _cntLoginTry = 0;
        private const string _encryptionKey = "12345678";
        private string _ProductVersion = " ";
        private DataTable dtDatabase;

        //Added gstrServicesDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 
        //private string sServiceDatabaseName = "gloServices";
        //  private string sServiceDatabaseName = "";
        //Added gstrServicesDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 

        #endregion 'Declarations'

        #region 'Constructors & Properties'

        public frmSplash()
        {
            InitializeComponent();
        }

        #endregion 'Constructors & Properties'

        //Bug #55199: 00000517 : PM dashboard performance issue
        //Added changes to resolve the performance issue.
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

        /// <summary>
        /// If the Connection Settings are satisfied Login Form is displayed 
        /// else settings form is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplash_Load(object sender, EventArgs e)
        {

            ////=================== Make our bitmap region for the form
            //bAfterLoad = true;
            //gloGlobal.BitmapRegion.CreateControlRegion(this, bmpFrmBack);
            ////===========================================
            // gloPM.Classes.BitmapRegion.CreateControlRegion(this, bmpFrmWhite);
            //pnlLogin.Visible = false;
            // lblSetup.Visible = true;
            lblPleaseWait.Visible = false;
            cmbDatabaseName.Visible = false;
            pnlDataBase.Visible = false;
            lblDataBase.Visible = false;
            lblCopyrghTag.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain;
            label1.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightSub;

            //If Connection & Database Settings are alright then only show login panel or it will show Settings form.
            if (_chkSettings())
            {
                ////=================== Make our bitmap region for the form
                //bAfterLoad = true;
                //gloGlobal.BitmapRegion.CreateControlRegion(this, bmpFrmBack);
                ////===========================================

                FillDatabases();
                ShowSoftwareDateTime();
                string strUpdateLocation = "";
                strUpdateLocation = CheckforUpdate();
                if (!string.IsNullOrEmpty(strUpdateLocation))
                {
                    InstallClientUpdate(strUpdateLocation);
                }

                tmr_AUSUpdates.Enabled = true;
                tmr_AUSUpdates.Start();

                pnlPMS.Visible = true;
                txtUserName.Focus();


                //*********************************************
                //Added By Debasish Das on 26th Mar 2010
                //*********************************************
                Program.LockOutAttempts = getLockOutAttempts();
                //******************* Ends Here ***************
                gloGlobal.gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting();
            }
            tmr_ShowSetting.Enabled = true;
        }

        /// <summary>
        /// Checking Login Validatation's.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblPleaseWait.Visible = true;
            Application.DoEvents();
            LoginClick();
        }

        private void LoginClick(Boolean SingleSignOn = false)
        {
            Boolean bIsSingleSignOn = false;

            ClsEncryption oEncrypt = new ClsEncryption();
            if (cmbDatabaseName.Visible)
            {
                if (!string.IsNullOrEmpty(cmbDatabaseName.Text) & dtDatabase.Rows.Count > 0)
                {
                    for (int iRow = 0; iRow <= dtDatabase.Rows.Count - 1; iRow++)
                    {
                        if (dtDatabase.Rows[iRow]["nDBConnectionId"].ToString() == cmbDatabaseName.SelectedValue.ToString())
                        {



                            Program.gSQLServerName = dtDatabase.Rows[iRow]["sServerName"].ToString();
                            Program.gDatabase = dtDatabase.Rows[iRow]["sDatabaseName"].ToString();
                            Program.gLoginUser = dtDatabase.Rows[iRow]["sSqlUserName"].ToString();


                            Program.gLoginPassword = oEncrypt.DecryptFromBase64String(dtDatabase.Rows[iRow]["sSqlPassword"].ToString(), _encryptionKey);


                            _databaseconnectionstring = Program.GetConnectionString(Program.gWindowsAuthentication, Program.gSQLServerName, Program.gDatabase, Program.gLoginUser, Program.gLoginPassword);
                            _MessageBoxCaption = "gloPM";

                            appSettings["SQLServerName"] = dtDatabase.Rows[iRow]["sServerName"].ToString();
                            appSettings["DatabaseName"] = dtDatabase.Rows[iRow]["sDatabaseName"].ToString();
                            appSettings["DataBaseConnectionString"] = _databaseconnectionstring;
                            appSettings["EMRConnectionString"] = _databaseconnectionstring;

                            //appSettings["SQLLoginName"] = dtDatabase.Rows[iRow]["sDatabaseName"].ToString();
                            appSettings["SQLLoginName"] = Convert.ToString(dtDatabase.Rows[iRow]["sSqlUserName"]);
                            appSettings["SQLPassword"] = Program.gLoginPassword;

                            appSettings["MessageBOXCaption"] = _MessageBoxCaption;
                            appSettings["ClinicID"] = "1";

                            // Application version Implementation - 7005

                            gloSettings.GeneralSettings oPMApplicationVersionSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                            oPMApplicationVersionSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                            object value = null;
                            oPMApplicationVersionSettings.GetSetting("gloPMApplicationVersion", out value);

                            if (value != null && Convert.ToString(value) != "")
                            {
                                gloGlobal.gloPMGlobal.ApplicationVersion = value.ToString();
                            }
                            else
                            {
                                gloGlobal.gloPMGlobal.ApplicationVersion = string.Empty;
                            }
                            value = null;

                            // Get AusID 
                            object objAus = null;
                            oPMApplicationVersionSettings.GetClinicMasterSetting("sExternalcode", out objAus);

                            if (objAus != null && Convert.ToString(objAus) != "")
                            {
                                gloGlobal.gloPMGlobal.AusID = objAus.ToString();
                            }
                            else
                            {
                                gloGlobal.gloPMGlobal.AusID = string.Empty;
                            }
                            objAus = null;

                            // Application version Implementation - 7005

                            gloGlobal.gloPMGlobal.DatabaseConnectionString = _databaseconnectionstring;
                            gloGlobal.gloPMGlobal.MessageBoxCaption = _MessageBoxCaption;

                            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(Program.GetConnectionString());

                            if (oDBLayer.CheckConnection() == false)
                            {
                                MessageBox.Show("Selected database/SQL server is not valid", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                oDBLayer.Dispose();
                                oDBLayer = null;
                                return;
                            }

                            //'to check the connection has been done or not.
                            //System.Diagnostics.FileVersionInfo gloPMVersion = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\" + Application.ProductName + ".exe");
                            //System.Diagnostics.FileVersionInfo gloPMVersion = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\" +  "gloPM.exe");

                            DataTable dtversion = null;
                            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(Program.GetConnectionString());
                            dtversion = oSetting.GetSetting("DB Version", 0);

                            String _DBVersion = "";
                            if (dtversion != null && dtversion.Rows.Count > 0)
                            {
                                _DBVersion = dtversion.Rows[0]["sSettingsValue"].ToString();
                            }
                            if (dtversion != null)
                            {
                                dtversion.Dispose();
                            }

                            //if (gloPMVersion.ProductVersion != _DBVersion)
                            if (Application.ProductVersion != _DBVersion)
                            {
                                MessageBox.Show("Application version mismatch, Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                lblPleaseWait.Visible = false;
                                oDBLayer.Dispose();
                                oDBLayer = null;
                                return;
                            }


                            if (oDBLayer.CheckConnection() == false)
                            {
                                MessageBox.Show("Selected database/SQL server is not valid", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                lblPleaseWait.Visible = false;
                                oDBLayer.Dispose();
                                oDBLayer = null;
                                return;
                            }

                            oDBLayer.Dispose();
                            oDBLayer = null;

                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
            }
            else //If multiple databases are not present check for database setup in registry
            {
                gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(Program.GetConnectionString());
                try
                {
                    if (oDBLayer.CheckConnection() == false)
                    {
                        MessageBox.Show("Selected database/SQL server is not valid", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblPleaseWait.Visible = false;
                        oDBLayer.Dispose();
                        oDBLayer = null;
                        return;
                    }

                    //'to check the connection has been done or not.
                    //System.Diagnostics.FileVersionInfo gloPMVersion = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\" + Application.ProductName + ".exe");
                    //System.Diagnostics.FileVersionInfo gloPMVersion = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\" +  "gloPM.exe");

                    DataTable dtversion = null;
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(Program.GetConnectionString());
                    dtversion = oSetting.GetSetting("DB Version", 0);


                    String _DBVersion = "";
                    if (dtversion != null && dtversion.Rows.Count > 0)
                    {
                        _DBVersion = dtversion.Rows[0]["sSettingsValue"].ToString();
                    }
                    if (dtversion != null)
                    {
                        dtversion.Dispose(); dtversion = null;
                    }

                    //if (gloPMVersion.ProductVersion != _DBVersion)
                    if (Application.ProductVersion != _DBVersion)
                    {
                        MessageBox.Show("Application version mismatch, Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblPleaseWait.Visible = false;
                        oDBLayer.Dispose();
                        oDBLayer = null;
                        return;
                    }
                    gloSettings.GeneralSettings oPMApplicationVersionSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    oPMApplicationVersionSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    object value = null;
                    oPMApplicationVersionSettings.GetSetting("gloPMApplicationVersion", out value);

                    if (value != null && Convert.ToString(value) != "")
                    {
                        gloGlobal.gloPMGlobal.ApplicationVersion = value.ToString();
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.ApplicationVersion = string.Empty;
                    }
                    value = null;

                      // Get AusID 
                    object objAus = null;
                    oPMApplicationVersionSettings.GetClinicMasterSetting("sExternalcode", out objAus);

                    if (objAus != null && Convert.ToString(objAus) != "")
                    {
                        gloGlobal.gloPMGlobal.AusID = objAus.ToString();
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.AusID = string.Empty;
                    }
                    objAus = null;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDBLayer != null) { oDBLayer.Dispose(); oDBLayer = null; }
                }
            }


            //Set gloTSprint variable value                            
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
            if (gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseDefaultPrinter").ToString() == "1")
                {
                    appSettings.Set("DefaultPrinter", "true");
                }
                else
                {
                    appSettings.Set("DefaultPrinter", "false");
                }
            }
            if (gloRegistrySetting.GetRegistryValue("EnableLocalPrinter") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("EnableLocalPrinter").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.isCopyPrint = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.isCopyPrint = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.isCopyPrint = false;
            }
            if (gloRegistrySetting.GetRegistryValue("AddFooterInService") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("AddFooterInService").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.AddFooterInService = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.AddFooterInService = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.AddFooterInService = false;
            }
            gloGlobal.gloTSPrint.TempPath = gloSettings.FolderSettings.AppTempFolderPath;
            int result = 0;
            if (gloRegistrySetting.GetRegistryValue("NoOfPagesToSplit") != null)
            {
                int.TryParse(gloRegistrySetting.GetRegistryValue("NoOfPagesToSplit").ToString(), out result);
            }
            gloGlobal.gloTSPrint.NoOfPages = result;
            //get file type to be used for word printing - EMF / PDF
            if (gloRegistrySetting.GetRegistryValue("UseEMFFile") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseEMFFile").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.UseEMFForWord = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.UseEMFForWord = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.UseEMFForWord = true;
            }
            //get file type to be used for SSRS printing - EMF / PDF
            if (gloRegistrySetting.GetRegistryValue("UseEMFFileSSRS") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseEMFFileSSRS").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.UseEMFForSSRS = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.UseEMFForSSRS = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.UseEMFForSSRS = true;
            }
            //get file type to be used for Claims printing - EMF / PDF
            if (gloRegistrySetting.GetRegistryValue("UseEMFForClaims") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseEMFForClaims").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.UseEMFForClaims = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.UseEMFForClaims = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.UseEMFForClaims = true;
            }
            //get file type to be used for Claims printing - EMF / PDF
            if (gloRegistrySetting.GetRegistryValue("UseEMFForImages") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseEMFForImages").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.UseEMFForImages = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.UseEMFForImages = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.UseEMFForImages = true;
            }
            //get file type to be used for metadata file
            if (gloRegistrySetting.GetRegistryValue("UseZippedMetadata") != null)
            {
                if (gloRegistrySetting.GetRegistryValue("UseZippedMetadata").ToString() == "1")
                {
                    gloGlobal.gloTSPrint.UseZippedMetadata = true;
                }
                else
                {
                    gloGlobal.gloTSPrint.UseZippedMetadata = false;
                }
            }
            else
            {
                gloGlobal.gloTSPrint.UseZippedMetadata = false;
            }

            //Check Network DIR 
            
            //gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();

            try
            {
                System.Threading.Thread networkDriCheckThread = new System.Threading.Thread(DoesNetworkDirExist);
                networkDriCheckThread.Start();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }


            //

            gloValidateUser oVU = new gloValidateUser(_databaseconnectionstring);
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            gloUser oGetUser = new gloUser(_databaseconnectionstring);
            User oUser = new User();
            string sLoginName = String.Empty;
            string sPassword = String.Empty;
            try
            {

                //-----
                DataTable dtMigrationStatus = new DataTable();
                Boolean _dtMigrationStatus = false;
                dtMigrationStatus = ogloSettings.GetSetting("ISBILLINGMIGRATED", 0);
                if (dtMigrationStatus != null && dtMigrationStatus.Rows.Count > 0)
                {
                    if (Convert.ToString(dtMigrationStatus.Rows[0]["sSettingsValue"]) != "")
                    {
                        _dtMigrationStatus = Convert.ToBoolean(dtMigrationStatus.Rows[0]["sSettingsValue"]);
                    }
                }

                if (_dtMigrationStatus == false)
                {
                    MessageBox.Show("Database migration not done. Please Contact Administrator. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK);
                    this.Close();

                }

                if (SingleSignOn)
                {
                    DataTable dtSingleSignOn = ogloSettings.GetSetting("EnableSingleSignON", 0);
                    if (dtSingleSignOn != null && dtSingleSignOn.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(dtSingleSignOn.Rows[0]["sSettingsValue"]) > 0)
                            bIsSingleSignOn = true;
                        else
                            bIsSingleSignOn = false;

                        if (bIsSingleSignOn)
                        {
                            SingleSignONUserNamePassword();
                            if (txtUserName.Text == "" || txtPassword.Text == "")
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                #region "Check Blank"
                if (txtUserName.Text.Trim() == "")
                {
                    MessageBox.Show("User Name must be entered", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserName.Focus();
                    lblPleaseWait.Visible = false;
                    txtPassword.Clear();
                    return;
                }

                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Password must be entered", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPassword.Focus();
                    lblPleaseWait.Visible = false;
                    return;
                }
                #endregion

                sLoginName = txtUserName.Text;
                sPassword = txtPassword.Text;

                string e_sPassword = oEncrypt.EncryptToBase64String(sPassword, _encryptionKey);
                object oResult = null;

                // Fetch HL7 outbound setting.
                ogloSettings.ScanClientInterfaceForPM(_MessageBoxCaption, System.Environment.MachineName);

                #region "Pefix Validations"
                ogloSettings.GetSetting("UseSitePrefix", out oResult);
                Int32 _UseSitePrefix = 0;
                if (oResult != null && !string.IsNullOrEmpty(oResult.ToString()))
                {
                    _UseSitePrefix = Convert.ToInt32(oResult);
                }
                if (_UseSitePrefix != 0)
                {
                    //Code For prefix validation
                    DataTable dtPrefixDTL = new DataTable();
                    gloPatient.gloPatient oPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                    dtPrefixDTL = oPatient.GetPrefix();
                    if (dtPrefixDTL != null)
                    {
                        if (dtPrefixDTL.Rows.Count > 0)
                        {
                            appSettings["PatientPrefix"] = Convert.ToString(dtPrefixDTL.Rows[0]["sPreFix"]);
                        }
                        else
                        {

                            MessageBox.Show("Patient Code Prefix is not set up for this site.  Please contact your administrator to set up the patient code prefix before using the application.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Patient Code Prefix is not set up for this site.  Please contact your administrator to set up the patient code prefix before using the application.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cursor = Cursors.Default;
                        return;
                    }
                    if (dtPrefixDTL != null)
                    {
                        dtPrefixDTL.Dispose();
                    }
                    if (oPatient != null)
                    {
                        oPatient.Dispose();
                    }
                }
                #endregion

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
                    #region "Password Reset"

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
                        lblPleaseWait.Visible = false;

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

                    //// 2. validate password [complexity] -------------------<< FOR Info only  >>>                
                    //if (!oVU.IsUserAdministrator(sLoginName))
                    //{
                    //    if (!oVU.IsPasswordResetted(sLoginName))
                    //    {
                    //        if (!oVU.ValidatePassword(sLoginName, sPassword))
                    //        {
                    //            //MessageBox.Show("Your password do not meet the password complexity. So, you need to change your password.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                                
                    //            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                    //            MessageBox.Show("Your Password has been reset by the administrator. Please contact the administrator to get the new password.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            txtPassword.Clear();
                    //            txtUserName.Focus();
                    //            this.Close();
                    //            return;
                    //            //Dim frm As New frmChangePassword(gstrLoginName)
                    //            //frm.ShowDialog()
                    //            //Exit Function                                
                    //            //return;
                    //        }
                    //    }
                    //}
                    //-------------------------------------------------------------------------------

                    #endregion


                    MessageBox.Show("Invalid user name or password.  Please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, false, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), 1);

                    lblPleaseWait.Visible = false;
                    txtPassword.Clear();
                    txtUserName.Focus();

                    #region "Login Attempts"

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
                        lblPleaseWait.Visible = false;
                    }
                    #endregion

                    return;

                }
                #endregion " User Authentication & Credentials check"

                #region  "Blocked/Access Denied/Password Reset  "

                if (oVU.chkISBlocked(sLoginName))
                {
                    MessageBox.Show("User is Blocked.Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                if (oVU.chkIsAccessDenied(sLoginName)) //Checking for Access is given to user or not.
                {
                    MessageBox.Show("Access denied. Please contact your administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //added on 21/04/2010 case:0004256                   
                    this.Close();
                    return;
                }
                if (oVU.IsPasswordResetted(sLoginName)) //Password Reset
                {
                    ////Your Password has been reset by the administrator. Please contact the administrator to get the new password.  
                    ////MessageBox.Show("Your Password has been reset by the administrator. Please contact the administrator to get the new password.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                    frmChangePassword ofrmpsw = new frmChangePassword(sLoginName, _databaseconnectionstring);
                    ofrmpsw.StartPosition = FormStartPosition.CenterParent;

                    ofrmpsw.ShowDialog(this);

                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                    lblPleaseWait.Visible = false;
                    ofrmpsw.Dispose();
                    ofrmpsw = null;
                    return;
                }
                #endregion

                #region  " Set Values for logged in User "

                DataTable dttempUser;
                dttempUser = oGetUser.GetUser(oVU.nUserID);
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
                    if (dttempUser != null)
                    {
                        dttempUser.Dispose();
                    }
                }

                #endregion  " Set Values for logged in User "

                #region " Set HL7 Database creadentials "

                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("GLOHL7SERVERNAME", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        HL7Settings.gloHL7ServerName = Convert.ToString(objSettingValue);
                    }

                    objSettingValue = null;

                    ogloSettings.GetSetting("GLOHL7DBNAME", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        HL7Settings.gloHL7DatabaseName = Convert.ToString(objSettingValue);
                    }

                    objSettingValue = null;


                    ogloSettings.GetSetting("GLOHL7USERID", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        HL7Settings.gloHL7UserID = Convert.ToString(objSettingValue);
                    }

                    objSettingValue = null;

                    ogloSettings.GetSetting("GLOHL7PASSWORD", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        HL7Settings.gloHL7Password = oEncrypt.DecryptFromBase64String(Convert.ToString(objSettingValue), _encryptionKey);
                    }

                    objSettingValue = null;

                    ogloSettings.GetSetting("GLOHL7AUTHEN", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        HL7Settings.IsSQLAuthenticationOnForGloHL7 = Convert.ToBoolean(objSettingValue);
                    }

                    objSettingValue = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #endregion " Set HL7 settings "

                #region "ExternalCollectionfeature Setting"
                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("ExternalCollectionfeature", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region "Reason Code Setup Setting"
                try
                {
                    object objSettingValue = null;
                    if (ogloSettings == null)
                    { ogloSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString); }
                    ogloSettings.GetSetting("InsurancePaymentResoneCodeSetup", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ReasonCodeSetup = Convert.ToInt16(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion


                #region "Login and Show dashboard"

                try
                {

                    appSettings.Set("UserName", oUser.UserName);
                    appSettings.Set("UserID", Convert.ToString(oUser.UserID));
                    appSettings.Set("ProviderID", Convert.ToString(oUser.ProviderID));
                    appSettings.Set("DataBaseConnectionString", _databaseconnectionstring);
                    gloGlobal.gloPMGlobal.UserID = oUser.UserID;
                    gloGlobal.gloPMGlobal.UserName = oUser.UserName;
                    gloGlobal.gloPMGlobal.LoginProviderID = oUser.ProviderID;
                    gloGlobal.gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting();

                    #region "License Check"

                    string smessage = "";
                    smessage = base.ValidateLogin(gloGlobal.gloPMGlobal.LoginProviderID, _databaseconnectionstring);
                    if (!string.IsNullOrEmpty((smessage)))
                    {
                        if (MessageBox.Show(smessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            System.Environment.Exit(0);
                        }
                    }
                    smessage = "";
                    #endregion "License Check"
                    frmDashBoardMain ofrmMainMenu = new frmDashBoardMain();
                    ofrmMainMenu.IsSingleSignOn = SingleSignOn;
                    this.Hide();

                    //Bug #55199: 00000517 : PM dashboard performance issue
                    //Added changes to resolve the performance issue.
                    ofrmMainMenu.SuspendLayout();
                    ofrmMainMenu.Show();
                    ofrmMainMenu.ResumeLayout();
                    gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, true, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), 1);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Success);
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                    throw;
                }
                finally
                {
                    oVU.updateLoginStatus(sLoginName, true, System.Windows.Forms.SystemInformation.ComputerName);
                }

                #endregion "Login and Show dashboard"

                #region "SpiltSCreen Settings"
                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("bViewDocumentsOnCharges", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ViewDocumentsOnCharges = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region "ClaimValidationSetting"

                ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                object value = null;
                ogloSettings.GetSetting("ClaimValidationSetting", out value);
                //gloPM5061
                //appSettings.Set("ClaimValidationSetting", "Alpha2");
                if (value != null && Convert.ToString(value) != "")
                {
                    //if (Convert.ToString(value) == "YOST")
                    //{
                    appSettings.Set("ClaimValidationSetting", value.ToString());
                    //}
                }
                else
                {
                    appSettings.Set("ClaimValidationSetting", "None");
                }
                value = null;

                ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("Country", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    appSettings.Set("Country", value.ToString());
                }
                else
                {
                    appSettings.Set("Country", "US");
                }
                value = null;

                #endregion

                #region "Appointment Default location Setting"

                SetAppointmentDefaultLocationSetting();

                #endregion ""

                #region "Get\Set Error logs settings"
                Read_ErrorLogSettings();
                #endregion ""

                #region "charge edit validate all claim Insurancess Setting"
                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("bchargeeditvalidateallclaimInsurancess", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.ChargeEditValidateAllClaimInsurances = Convert.ToBoolean(objSettingValue);
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #endregion

                #region "getICDReviosn"
                try
                {
                    gloGlobal.gloPMGlobal.CurrentICDRevision = gloGlobal.clsICD.GetICDCodeType();
                }
                catch (Exception Ex)
                {
                    gloGlobal.gloPMGlobal.CurrentICDRevision = gloGlobal.gloICD.CodeRevision.ICD10;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                }
                #endregion

                #region "Patient Appointments Linking to Charges Settings"
                try
                {
                    object objSettingValue = null;

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
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region "sClaimPrefix Setting"

                try
                {
                    object objSettingValue = null;

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
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("bUsePrefixforBatch", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix = Convert.ToBoolean(objSettingValue);
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix =false;
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    object objSettingValue = null;

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
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region "Skip Scanner Setting Settings"
                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("SKIPSCANNERLIST", out objSettingValue);
                    if (objSettingValue != null)
                    {
                        String skipScanners = Convert.ToString(objSettingValue);
                        if (!String.IsNullOrEmpty(skipScanners))
                        {
                            gloGlobal.gloRemoteScanSettings.skipScannerList = skipScanners.Split(',');
                        }
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                ///
                //Scanner Settings for local or glo Scan
                gloEDocumentV3.Common.RemoteScanCommon oRemoteScan = new gloEDocumentV3.Common.RemoteScanCommon();
                string sRetVal = null;

                ///
                ///Remote Scanning
                ///
                //RegistryKey key = null;
                try
                {
                    //Refresh Scanners


                    //EnableRemoteScan Setting
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);

                    //get no of templates to print per jon for Batch Print template report
                    int NoOfTemplates = 1;
                    if (gloRegistrySetting.GetRegistryValue("NoOfTemplatesPerJob") != null)
                    {
                        int.TryParse(gloRegistrySetting.GetRegistryValue("NoOfTemplatesPerJob").ToString(), out NoOfTemplates);
                    }
                    gloGlobal.gloTSPrint.NoOfTemplatesPerJob = NoOfTemplates;

                    //key = Registry.CurrentUser.OpenSubKey(gloRegistrySetting.gstrSoftEMR);
                    string sRemoteScanRegKey = "false";
                    //if (key != null)
                    //{
                    //    sRemoteScanRegKey = Convert.ToString(key.GetValue("EnableRemoteScan"));
                    //}
                    sRemoteScanRegKey = Convert.ToString(gloRegistrySetting.GetRegistryValue("EnableRemoteScan"));

                    //oRemoteScan.GetRegistryValue("EnableRemoteScan");
                    if (!string.IsNullOrEmpty(sRemoteScanRegKey))
                    {
                        gloGlobal.gloRemoteScanSettings.EnableRemoteScan = Convert.ToBoolean(sRemoteScanRegKey);
                    }
                    // Scanner Settings Masters
                    if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                    {
                        string sZipScanSettingsRegKey = "false";
                        sZipScanSettingsRegKey = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrZipScannerSettings));

                        if (!string.IsNullOrEmpty(sZipScanSettingsRegKey))
                        {
                            gloGlobal.gloRemoteScanSettings.bZipScanSettings = Convert.ToBoolean(sZipScanSettingsRegKey);
                        }

                        if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings == null)
                        {
                            gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject();

                            //Current Settings
                            sRetVal = oRemoteScan.SetRemoteScannerCurrentSettings(null, null, null);
                            if (!string.IsNullOrEmpty(sRetVal)) { gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure); }
                            //
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    // MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oRemoteScan = null;
                    //key = null;
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                }
                ///

                /// 
                /// Eliminate EliminatePegasus
                if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                {
                    gloGlobal.gloEliminatePegasus.bEliminatePegasus = false;
                }
                else
                {
                    try
                    {
                        gloRegistrySetting.CloseRegistryKey();
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                        string sEliminatePegasus = "false";
                        sEliminatePegasus = Convert.ToString(gloRegistrySetting.GetRegistryValue("EliminatePegasus"));

                        if (!string.IsNullOrEmpty(sEliminatePegasus))
                        {
                            gloGlobal.gloEliminatePegasus.bEliminatePegasus = Convert.ToBoolean(sEliminatePegasus);
                        }

                        if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                        {
                            if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings == null)
                            {
                                gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog();
                                if (gloRemoteScanGeneral.TwainScanFunctionality.CreateTwainScanSettingsFile())
                                {
                                    gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(true);

                                    //Current Settings
                                    if (oRemoteScan == null)
                                    {
                                        oRemoteScan = new gloEDocumentV3.Common.RemoteScanCommon();
                                    }
                                    sRetVal = oRemoteScan.SetRemoteScannerCurrentSettings(null, null, null);
                                    if (!string.IsNullOrEmpty(sRetVal))
                                    {
                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure);
                                    }
                                }
                                //
                            }

                            //Delete old scan config files (local) 
                            try
                            {
                                gloRemoteScanGeneral.gloRemoteScanMetaDataWriter.DeleteScanConfigFiles();
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }
                    finally
                    {
                        gloRegistrySetting.CloseRegistryKey();
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                        if (oRemoteScan != null)
                        {
                            oRemoteScan = null;
                        }
                    }
                }

                //read pegasus brightness
                try
                {
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                    string sPegasusBright = "";
                    sPegasusBright = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrPegasusBright));

                    if (!string.IsNullOrEmpty(sPegasusBright))
                    {
                        gloGlobal.gloEliminatePegasus.sPegasusBright = sPegasusBright;
                    }
                    else
                    {
                        sPegasusBright = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright));
                        if (!string.IsNullOrEmpty(sPegasusBright))
                        {
                            gloGlobal.gloEliminatePegasus.sPegasusBright = sPegasusBright;
                            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusBright, sPegasusBright);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                }
                //

                //read pegasus contrast
                try
                {
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                    string sPegasusContrast = "";
                    sPegasusContrast = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrPegasusContrast));

                    if (!string.IsNullOrEmpty(sPegasusContrast))
                    {
                        gloGlobal.gloEliminatePegasus.sPegasusContrast = sPegasusContrast;
                    }
                    else
                    {
                        sPegasusContrast = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast));
                        if (!string.IsNullOrEmpty(sPegasusContrast))
                        {
                            gloGlobal.gloEliminatePegasus.sPegasusContrast = sPegasusContrast;
                            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusContrast, sPegasusContrast);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                }
                //

                //gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(sLoginName, true, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), 1);
                #region "Communication Service URL"
                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("SCENTRALIZEDQCOMMUNICATIONSERVICEURL", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.sCommunicationServiceURL = Convert.ToString(objSettingValue);
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.sCommunicationServiceURL = "";
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    object objSettingValue = null;

                    ogloSettings.GetSetting("ISCENTRALIZEDRULEENGINEENABLE", out objSettingValue);

                    if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                    {
                        gloGlobal.gloPMGlobal.IsCommunicationServiceEnable = objSettingValue.ToString()=="1" ? true : false;
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.IsCommunicationServiceEnable = false;
                    }
                    objSettingValue = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                #region AUSID
                if (string.IsNullOrEmpty(gloGlobal.gloPMGlobal.AusID))
                {
                    object objAus = null;
                    ogloSettings.GetClinicMasterSetting("sExternalcode", out objAus);

                    if (objAus != null && Convert.ToString(objAus) != "")
                    {
                        gloGlobal.gloPMGlobal.AusID = objAus.ToString();
                    }
                    else
                    {
                        gloGlobal.gloPMGlobal.AusID = string.Empty;
                    }
                    objAus = null;  
                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, "Log in ", gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oUser.Dispose();
                oGetUser.Dispose();
                ogloSettings.Dispose();
                oVU.Dispose();
                oEncrypt.Dispose();
            }
        }

        private void DoesNetworkDirExist()
        {
            try
            {
                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private void SingleSignONUserNamePassword()
        {
            DataTable dtWindowsUser = null;
            ClsEncryption oEncryption = new ClsEncryption();
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();

            dtWindowsUser = GetSingleSignONUser(localMachine.UserName);

            if (dtWindowsUser != null && dtWindowsUser.Rows.Count > 0)
            {
                txtUserName.Text = Convert.ToString(dtWindowsUser.Rows[0]["sLoginName"]);
                if (txtUserName.Text != "")
                {
                    txtPassword.Text = oEncryption.DecryptFromBase64String(Convert.ToString(dtWindowsUser.Rows[0]["sPassword"]), _encryptionKey);
                }
            }

            if (oEncryption != null)
            {
                oEncryption.Dispose();
                oEncryption = null;
            }
            if (dtWindowsUser != null)
            {
                dtWindowsUser.Dispose();
                dtWindowsUser = null;
            }
        }

        public DataTable GetSingleSignONUser(string sUserName)
        {
            DataTable dtUser = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //dtIDQualifiers = new DataTable();

                oDBParameters.Add("@WindowsUser", sUserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDB.Retrive("gsp_GetSingleSignONUser", oDBParameters, out dtUser);

                return dtUser;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                dtUser = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                dtUser = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }

            return dtUser;
        }

        private void SetAppointmentDefaultLocationSetting()
        {
            System.Collections.Specialized.NameValueCollection appSettngs = System.Configuration.ConfigurationManager.AppSettings;
            int i = 0;
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable dt = new DataTable();
            try
            {
                dt = oLocation.GetDefaultLocation();
                if ((dt != null))
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        appSettngs["DefaultLocationID"] = dt.Rows[i]["nLocationID"].ToString();
                        appSettngs["DefaultLocation"] = dt.Rows[i]["sLocation"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((dt != null))
                {
                    dt.Dispose();
                    dt = null;
                }
                if ((oLocation != null))
                {
                    oLocation.Dispose();
                    oLocation = null;
                }
            }
        }

        private void Read_ErrorLogSettings()
        {
            object retValue = null;
            try
            {
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);

                #region " Read and set EnableApplicationLogs registry setting "

                retValue = gloRegistrySetting.GetRegistryValue("EnableApplicationLogs");
                if (retValue == null || Convert.ToString(retValue).Trim().Length <= 0)
                {
                    gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", false);
                    gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = false;
                }
                else
                {
                    try
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = Boolean.Parse(Convert.ToString(retValue));
                    }
                    catch (ArgumentNullException argNullEx)
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = false;
                        throw argNullEx;
                    }
                    catch (FormatException formatEx)
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = false;
                        throw formatEx;
                    }

                }

                #endregion " Read and set EnableApplicationLogs registry setting "

                retValue = null; //re-set retValue

                #region " Read and set EnableErrorLogs registry setting "

                retValue = gloRegistrySetting.GetRegistryValue("EnableErrorLogs");

                if (retValue == null || Convert.ToString(retValue).Trim().Length <= 0)
                {
                    gloRegistrySetting.SetRegistryValue("EnableErrorLogs", true);
                    gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = true;
                }
                else
                {
                    try
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = Boolean.Parse(Convert.ToString(retValue));
                    }
                    catch (ArgumentNullException argNullEx)
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = true;
                        throw argNullEx;
                    }
                    catch (FormatException formatEx)
                    {
                        gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = true;
                        throw formatEx;
                    }

                }

                #endregion "Read and set EnableErrorLogs registry setting "
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                retValue = null;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "Other events"

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();

                #region 'Application Instance check'

                //System.Diagnostics.Process processTemp = new System.Diagnostics.Process();

                ////Getting currently active process.
                //processTemp = System.Diagnostics.Process.GetCurrentProcess();

                ////Storing the process MainModule's ModuleName.
                //string gloMainModuleName = processTemp.MainModule.ModuleName;

                ////Getting the ProcessName by eliminating the extension.
                //string gloProcessName = System.IO.Path.GetFileNameWithoutExtension(gloMainModuleName);

                ////Check if application is already running
                ////if yes then exit.
                ////if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length  > 1) 
                //if (System.Diagnostics.Process.GetProcessesByName(gloProcessName).Length > 1)
                //{
                //    MessageBox.Show("Application instance already running.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    Application.Exit();
                //}
                #endregion 'Application Instance check'

                pnlLogin.Visible = true;
                //txtUserName.Focus();
                FileInfo oFile;
                oFile = new FileInfo(Application.StartupPath + "\\gloPM2008_Alpha.EXE");
                lblApplicationDate.Text = "Last Modified on " + oFile.LastWriteTime.ToString();
                oFile = null;
                string _sServerPath = "";
                gloRegistrySetting.OpenRemoteBaseKey();
                try
                {
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM))
                    {
                        if (gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrServpth) != null && gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrServpth).ToString() != "")
                            _sServerPath = Convert.ToString(gloRegistrySetting.GetRegistryValue((gloRegistrySetting.gstrServpth).ToString()));
                    }
                }
                catch { }
                //gloAus Library Code ....for calling update for PM...
                gloAUSLibrary.clsAus objAus = new gloAUSLibrary.clsAus();
                Boolean bIsUpdatePresent = false;
                bIsUpdatePresent = objAus.CheckforUpdate(Program.GetConnectionString(), "gloPM", _sServerPath, appSettings["DatabaseName"]);

                if (bIsUpdatePresent == false)
                {
                    LoginClick(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        // To Show the Start 
        private void tmr_ShowSetting_Tick(object sender, EventArgs e)
        {
            try
            {
                tmr_ShowSetting.Stop();
                // lblSetup.Visible = false;
                tmr_ShowSetting.Enabled = false;
                timer1.Start();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //MessageBox.Show(ex.Message.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // F11 Key Pressed for database Settings
        private void mnuSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (tmr_ShowSetting.Enabled == true)
                {
                    // lblSetup.Visible = false;
                    tmr_ShowSetting.Stop();
                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    if (oSettings.ShowDatabaseSettings(this) == true)
                    {

                        oSettings.GetXMLSettings();

                        Program.gSQLServerName = oSettings.SQLServerName;
                        Program.gDatabase = oSettings.DatabaseName;
                        Program.gWindowsAuthentication = oSettings.WindowsAuthentication;
                        Program.gLoginUser = oSettings.LoginUser;
                        Program.gLoginPassword = oSettings.LoginPassword;
                        Program.gblnShowReportDesigner = oSettings.ShowReportDesigner;

                        _databaseconnectionstring = Program.GetConnectionString(Program.gWindowsAuthentication, Program.gSQLServerName, Program.gDatabase, Program.gLoginUser, Program.gLoginPassword);
                        _MessageBoxCaption = "gloPM";

                        appSettings["SQLServerName"] = oSettings.SQLServerName;
                        appSettings["DatabaseName"] = oSettings.DatabaseName;
                        appSettings["SQLLoginName"] = oSettings.LoginUser;
                        appSettings["SQLPassword"] = oSettings.LoginPassword;
                        appSettings["WindowAuthentication"] = Convert.ToString(oSettings.WindowsAuthentication);
                        appSettings["DataBaseConnectionString"] = _databaseconnectionstring;
                        appSettings["EMRConnectionString"] = _databaseconnectionstring;
                        appSettings["MessageBOXCaption"] = _MessageBoxCaption;


                        gloGlobal.gloPMGlobal.ClinicID = 1;
                        gloGlobal.gloPMGlobal.DatabaseConnectionString = _databaseconnectionstring;
                        gloGlobal.gloPMGlobal.MessageBoxCaption = _MessageBoxCaption;
                        gloGlobal.gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting();



                        // Set Last PatientID
                        if (oSettings.LastPatient.Trim() != "")
                        {
                            Program.gnPatientID = Convert.ToInt64(oSettings.LastPatient);

                            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(Program.GetConnectionString());
                            if (oSecurity.isPatientLock(Program.gnPatientID, false))
                                Program.gnPatientID = 0;

                            if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }
                        }

                        // Set Lock Screen Time
                        gloRegistrySetting.OpenRemoteBaseKey();
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                        if (gloRegistrySetting.GetRegistryValue("AutoLockEnable") != null)
                        {
                            if (gloRegistrySetting.GetRegistryValue("AutoLockEnable").ToString() == "1")
                            {
                                if (gloRegistrySetting.GetRegistryValue("LockTime") != null)
                                {
                                    if (Convert.ToInt64(gloRegistrySetting.GetRegistryValue("LockTime").ToString()) > 0)
                                        Program.gLockScreenTime = Convert.ToInt64(gloRegistrySetting.GetRegistryValue("LockTime").ToString());
                                    else
                                        Program.gLockScreenTime = 10;
                                }
                            }
                        }
                        else
                        {
                            Program.gLockScreenTime = 10;
                        }
                        FillDatabases();
                    }

                    txtUserName.Focus();
                    timer1.Start();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion

        #region "Private Methods"

        public Int32 getLockOutAttempts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtDataTable = new DataTable();
            string sSQL;
            Int32 iReturn = 1;
            try
            {
                oDB.Connect(false);
                sSQL = "SELECT sSettingsValue FROM SETTINGS WHERE sSettingsName = 'No. Of. Attempts'";
                oDB.Retrive_Query(sSQL, out dtDataTable);

                if (dtDataTable != null && dtDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtDataTable.Rows[0]["sSettingsValue"]) != 0)
                    {
                        iReturn = Convert.ToInt32(dtDataTable.Rows[0]["sSettingsValue"]);
                    }
                    else
                    {
                        iReturn = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return iReturn;
        }

        private string getServicesDBName()
        {
            //Added getServicessDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtDataTable = new DataTable();
            string sSQL;
            string iReturn = "gloServices";
            try
            {
                oDB.Connect(false);
                sSQL = "SELECT sSettingsValue FROM SETTINGS WHERE sSettingsName = 'SERVICESDATABASENAME'";
                oDB.Retrive_Query(sSQL, out dtDataTable);

                if (dtDataTable != null && dtDataTable.Rows.Count > 0)
                {
                    if (Convert.ToString(dtDataTable.Rows[0]["sSettingsValue"]).Trim() != string.Empty)
                    {
                        iReturn = Convert.ToString(dtDataTable.Rows[0]["sSettingsValue"]);
                    }
                    else
                    {
                        iReturn = "gloServices";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return iReturn;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return iReturn;
            //Added getServicessDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 
        }


        ///// <summary>
        ///// Checking the connection Settings if they are true we develop the Connection String.
        ///// </summary>
        ///// <returns></returns>

        private bool _chkSettings()
        {
            bool Result = false;
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            //DataSet ds = new DataSet();
            try
            {

                bool blnValidSettings = false;

                if (oSettings.GetDatabaseSettings_Registry() == true)
                {
                    oSettings.GetXMLSettings();
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
                            return false;
                        }
                        else
                        {
                            oSettings.GetXMLSettings();
                        }
                    }
                    else if (oDlgResult == DialogResult.No)
                    {
                        Application.ExitThread(); ;
                    }

                }

                Program.gSQLServerName = oSettings.SQLServerName;
                Program.gDatabase = oSettings.DatabaseName;
                Program.gWindowsAuthentication = oSettings.WindowsAuthentication;
                Program.gLoginUser = oSettings.LoginUser;
                Program.gLoginPassword = oSettings.LoginPassword;
                Program.gblnShowReportDesigner = oSettings.ShowReportDesigner;

                _databaseconnectionstring = Program.GetConnectionString(Program.gWindowsAuthentication, Program.gSQLServerName, Program.gDatabase, Program.gLoginUser, Program.gLoginPassword);
                _MessageBoxCaption = "gloPM";

                appSettings["SQLServerName"] = oSettings.SQLServerName;
                appSettings["DatabaseName"] = oSettings.DatabaseName;
                appSettings["SQLLoginName"] = oSettings.LoginUser;
                appSettings["SQLPassword"] = oSettings.LoginPassword;
                appSettings["WindowAuthentication"] = Convert.ToString(oSettings.WindowsAuthentication);
                appSettings["DataBaseConnectionString"] = _databaseconnectionstring;
                appSettings["EMRConnectionString"] = _databaseconnectionstring;


                //appSettings["GenerateHL7Message"] = oSettings.GenerateHL7Message.ToString();
                //appSettings["GenerateOutboundMessage"] = oSettings.GenerateOutboundMessage.ToString();
                //appSettings["SendPatientDetails"] = oSettings.SendPatientDetails.ToString();
                //appSettings["SendAppointmentDetails"] = oSettings.SendAppointmentDetails.ToString();

                ////Added gstrServicesDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 


                try
                {
                    DataTable dtService;
                    dtService = gloAUSLibrary.clsGeneral.GetServicesDBCredentials(_databaseconnectionstring);
                    if ((dtService != null))
                    {
                        foreach (DataRow dr in dtService.Rows)
                        {
                            switch (Convert.ToString(dr["sSettingsName"]).ToUpper())
                            {
                                case "SERVICESAUTHEN":
                                    gloGlobal.gloPMGlobal.gbServicesIsSQLAUTHEN = Convert.ToBoolean(dr["sSettingsValue"]);
                                    break;
                                case "SERVICESDATABASENAME":
                                    gloGlobal.gloPMGlobal.gstrServicesDBName = Convert.ToString(dr["sSettingsValue"]);
                                    break;
                                case "SERVICESPASSWORD":
                                    ClsEncryption objgloServicesDecryptions = new ClsEncryption();
                                    if ((objgloServicesDecryptions != null))
                                    {
                                        gloGlobal.gloPMGlobal.gstrServicesPassWord = objgloServicesDecryptions.DecryptFromBase64String(Convert.ToString(dr["sSettingsValue"]), _encryptionKey);
                                        objgloServicesDecryptions = null;
                                    }
                                    objgloServicesDecryptions = null;
                                    break;
                                case "SERVICESSERVERNAME":
                                    gloGlobal.gloPMGlobal.gstrServicesServerName = Convert.ToString(dr["sSettingsValue"]);
                                    break;
                                case "SERVICESUSERID":
                                    gloGlobal.gloPMGlobal.gstrServicesUserID = Convert.ToString(dr["sSettingsValue"]);
                                    break;
                            }
                        }
                        dtService.Dispose();
                        dtService = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //gloGlobal.gloPMGlobal.gstrServicesDBName = getServicesDBName();
                //sServiceDatabaseName = gloGlobal.gloPMGlobal.gstrServicesDBName;
                ////Added gstrServicesDBName by Ujwala on 25022015 to get ServicesDB Name from settings table 

                appSettings["MessageBOXCaption"] = _MessageBoxCaption;
                appSettings["ClinicID"] = "1";

                gloGlobal.gloPMGlobal.ClinicID = 1;
                gloGlobal.gloPMGlobal.DatabaseConnectionString = _databaseconnectionstring;
                gloGlobal.gloPMGlobal.MessageBoxCaption = _MessageBoxCaption;


                // Set Last PatientID
                if (oSettings.LastPatient.Trim() != "")
                {
                    Program.gnPatientID = Convert.ToInt64(oSettings.LastPatient);

                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(Program.GetConnectionString());
                    if (oSecurity.isPatientLock(Program.gnPatientID, false))
                    {
                        Program.gnPatientID = 0;
                    }
                    if (oSecurity != null)
                    { oSecurity.Dispose(); oSecurity = null; }
                }

                // Set Lock Screen Time
                gloRegistrySetting.OpenRemoteBaseKey();
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                GetRegistryvalueforHelp();
                if (gloRegistrySetting.GetRegistryValue("AutoLockEnable") != null)
                {
                    if (gloRegistrySetting.GetRegistryValue("AutoLockEnable").ToString() == "1")
                    {
                        if (gloRegistrySetting.GetRegistryValue("LockTime") != null)
                        {
                            if (Convert.ToInt64(gloRegistrySetting.GetRegistryValue("LockTime").ToString()) > 0)
                                Program.gLockScreenTime = Convert.ToInt64(gloRegistrySetting.GetRegistryValue("LockTime").ToString());
                            else
                                Program.gLockScreenTime = 10;
                        }
                    }
                }
                else
                {
                    Program.gLockScreenTime = 10;
                }
                if (gloRegistrySetting.GetRegistryValue("ZoomVersion") == null)
                {
                    gloRegistrySetting.SetRegistryValue("ZoomVersion", "5x");
                }

                Result = true;
            }
            catch
            {
                Result = false;
            }
            finally
            {
                oSettings.Dispose();
            }
            return Result;
        }

        private void FillDatabases()
        {
            //'Local variable declaration

            //string _Query = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(Program.GetConnectionString(!gloGlobal.gloPMGlobal.gbServicesIsSQLAUTHEN, gloGlobal.gloPMGlobal.gstrServicesServerName, gloGlobal.gloPMGlobal.gstrServicesDBName, gloGlobal.gloPMGlobal.gstrServicesUserID, gloGlobal.gloPMGlobal.gstrServicesPassWord));
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                if (oDB.CheckConnection())
                {
                    oParameters = new gloDatabaseLayer.DBParameters();
                    oParameters.Add(new gloDatabaseLayer.DBParameter("@ServiceName", "gloEMR", ParameterDirection.Input, SqlDbType.VarChar, 150));
                    oParameters.Add(new gloDatabaseLayer.DBParameter("@MachineName", gloAuditTrail.MachineDetails.LocalMachineDetails().MachineName, ParameterDirection.Input, SqlDbType.VarChar, 150));

                    oDB.Connect(false);
                    oDB.Retrive("gsp_GetMultipleDatabases", oParameters, out dtDatabase);

                    //oDB.Retrive_Query(_Query, out dtDatabase);


                    if ((dtDatabase != null))
                    {
                        if (dtDatabase.Rows.Count > 0)
                        {

                            cmbDatabaseName.DataSource = dtDatabase;
                            cmbDatabaseName.DisplayMember = "sDatabaseName";
                            cmbDatabaseName.ValueMember = "nDBConnectionId";
                            cmbDatabaseName.SelectedItem = null;

                            //' CHECK FOR DEFAULT DATABASE IN COMBO LIST ''
                            for (int iRow = 0; iRow <= dtDatabase.Rows.Count - 1; iRow++)
                            {
                                if (Convert.ToBoolean(dtDatabase.Rows[iRow]["bEnabled"]) == true)
                                {
                                    cmbDatabaseName.Text = dtDatabase.Rows[iRow]["sDatabaseName"].ToString();
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            cmbDatabaseName.Visible = true;
                            pnlDataBase.Visible = true;
                            lblDataBase.Visible = true;
                        }
                        else
                        {
                            cmbDatabaseName.Visible = false;
                            pnlDataBase.Visible = false;
                            lblDataBase.Visible = false;

                        }
                    }
                    else
                    {
                        cmbDatabaseName.Visible = false;
                        pnlDataBase.Visible = false;
                        lblDataBase.Visible = false;
                    }
                }
                else
                {
                    cmbDatabaseName.Visible = false;
                    pnlDataBase.Visible = false;
                    lblDataBase.Visible = false;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
                cmbDatabaseName.Visible = false;
                pnlDataBase.Visible = false;
                lblDataBase.Visible = false;
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null;
                }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }


        private void GetRegistryvalueforHelp()
        {

            // gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
            if ((gloRegistrySetting.GetRegistryValue("HelpProvider") == null) == true)
            {


                gloRegistrySetting.SetRegistryValue("HelpProvider", "Client");
                Program.gstrHelpProvider = "Client";
                //this.HelpComponent1.Mode = HelpComponent.ProviderMode.Client;
            }
            else
            {
                Program.gstrHelpProvider = gloRegistrySetting.GetRegistryValue("HelpProvider").ToString();
                if (Program.gstrHelpProvider == "Client")
                {
                    Program.gstrHelpProvider = "Client";
                }
                else
                {
                    //this.HelpComponent1.Mode = HelpComponent.ProviderMode.Builder;
                    Program.gstrHelpProvider = "Builder";
                }

            }

            //  gloRegistrySetting.CloseRegistryKey(); 
        }


        private void ShowSoftwareDateTime()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(Program.GetConnectionString());

            try
            {

                DataTable dtversion = null;
                dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);
                if (dtversion != null && dtversion.Rows.Count > 0)
                {
                    _ProductVersion = dtversion.Rows[0]["sSettingsValue"].ToString();
                }
                if (dtversion != null)
                {
                    dtversion.Dispose();
                }

                //20100630 Version set
                _ProductVersion = Application.ProductVersion;

                //lblVersion.Text = _ProductVersion;
                lblVersion.Text = gloGlobal.clsMISC.GetMajorVersion(_ProductVersion);

                //lblLastModifiedDate.Text = "Last Modified  " + File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("dd MMM, yyyy");
                lblLastModifiedDate.Text = File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("MMM dd, yyyy");
                //lblLastModifiedTime.Text = File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("hh:mm:ss tt");

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oSetting.Dispose();
            }
        }

        #endregion

        public string CheckforUpdate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(Program.GetConnectionString());
            DataTable dt = new DataTable();
            string _strSQL = "";
            Int64 UpdateID = default(Int64);
            string UpdateLocation = "";
            try
            {
                oDB.Connect(false);
                _strSQL = "select nClientID, sMachineName, sProductCode, sCurrentProductVersion, sLatestProductVersion, dtUpdatedate, blnIsUpdated, nUpdateID from ClientSettings_MST where blnIsUpdated = 'False' and sMachineName = '" + System.Environment.MachineName + "' and sProductCode ='2'";
                oDB.Retrive_Query(_strSQL, out dt);
                //oDB.Disconnect();
                if ((dt != null))
                {
                    if (dt.Rows.Count > 0)
                    {

                        MessageBox.Show("Updates are available for your machine.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UpdateID = Convert.ToInt64(dt.Rows[0]["nUpdateID"]);
                        //_strSQL = "select [gloServices].[dbo].[sUpdategloSuiteClientLocation] from [gloServices].[dbo].[AusUpdateDetails] where [gloServices].[dbo].[AusUpdateDetails].[nUpdateAusId] = '" + UpdateID + "'";
                        _strSQL = "select [" + gloGlobal.gloPMGlobal.gstrServicesDBName + "].[dbo].[AusUpdateDetails].[sUpdategloSuiteClientLocation] from [" + gloGlobal.gloPMGlobal.gstrServicesDBName + "].[dbo].[AusUpdateDetails] where [" + gloGlobal.gloPMGlobal.gstrServicesDBName + "].[dbo].[AusUpdateDetails].[nUpdateAusId] =  '" + UpdateID + "'";
                        object _ret = new object();

                        _ret = oDB.ExecuteScalar_Query(_strSQL);
                        if (_ret != null && _ret.ToString().Trim().Length > 0)
                        {
                            UpdateLocation = _ret.ToString().Trim();
                        }
                        _ret = null;
                        return UpdateLocation;
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                }
            }
            return UpdateLocation;
        }

        private void InstallClientUpdate(string sUpdateLocation)
        {


            //******* added by Sandip dhakane 20100707
            try
            {

                string _sServerPath = "";
                if (gloRegistrySetting.OpenRemoteBaseKey())
                {
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM))
                    {
                        if (gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrServpth).ToString() != "")
                            _sServerPath = Convert.ToString(gloRegistrySetting.GetRegistryValue((gloRegistrySetting.gstrServpth).ToString()));


                        string tempLacation = _sServerPath + "\\Updates\\" + sUpdateLocation;

                        //string UnZipUpdateLoacation = Application.StartupPath + "\\Temp\\ClientUpdate.exe";
                        //Roopali for terminal server
                        string UnZipUpdateLoacation = gloSettings.FolderSettings.AppTempFolderPath + "ClientUpdate.exe";


                        UnZipFile.clsExtractFile.ExtractZipFile(tempLacation, UnZipUpdateLoacation);
                        string strSetupPath = UnZipUpdateLoacation;
                        FileInfo ofile = new FileInfo(strSetupPath);

                        System.Diagnostics.ProcessStartInfo startInfo = default(System.Diagnostics.ProcessStartInfo);
                        System.Diagnostics.Process pStart = new System.Diagnostics.Process();

                        startInfo = new System.Diagnostics.ProcessStartInfo(strSetupPath);
                        pStart.StartInfo = startInfo;
                        pStart.Start();
                        System.Environment.Exit(0);

                        gloRegistrySetting.CloseRegistryKey();

                    }


                }


            }

             //RegistryKey oRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
            //oRegistryKey = oRegistryKey.OpenSubKey("Software\\gloPM");

           // Boolean _Result = false;
            //try
            //{
            //    string _sServerPath = "";
            //    if (oRegistryKey != null)
            //    {
            //        // Read Setting From Registry
            //        if (Convert.ToString(oRegistryKey.GetValue("ServerPath")) != "")
            //        {
            //            _sServerPath = Convert.ToString(oRegistryKey.GetValue("ServerPath"));
            //        }

            //        string tempLacation = _sServerPath + "\\Updates\\" + sUpdateLocation;

            //        //string UnZipUpdateLoacation = Application.StartupPath + "\\Temp\\ClientUpdate.exe";
            //        //Roopali for terminal server
            //        string UnZipUpdateLoacation = appSettings["StartupPath"].ToString() + "\\Temp\\ClientUpdate.exe";


            //        UnZipFile.clsExtractFile.ExtractZipFile(tempLacation, UnZipUpdateLoacation);
            //        string strSetupPath = UnZipUpdateLoacation;
            //        FileInfo ofile = new FileInfo(strSetupPath);

            //        System.Diagnostics.ProcessStartInfo startInfo = default(System.Diagnostics.ProcessStartInfo);
            //        System.Diagnostics.Process pStart = new System.Diagnostics.Process();

            //        startInfo = new System.Diagnostics.ProcessStartInfo(strSetupPath);
            //        pStart.StartInfo = startInfo;
            //        pStart.Start();
            //        System.Environment.Exit(0);
            //    }


            //}
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tmr_AUSUpdates_Tick(object sender, EventArgs e)
        {
            tmr_AUSUpdates.Stop();
            try
            {
                //string strUpdateLocation = "";
                //strUpdateLocation = CheckforUpdate();
                //if (!string.IsNullOrEmpty(strUpdateLocation))
                //{
                //    InstallClientUpdate(strUpdateLocation);
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                tmr_AUSUpdates.Start();
            }
        }

        private bool GetPatientAccountFeatureSetting()
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                string _strSqlQuery = "select ISNULL(sSettingValue,'') AS sSettingsValue from Settings_Replication where sSettingName='Patient Account Feature'";
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

        private void btnLogin_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloPM.Properties.Resources.Img_OrangeBtnSplashScreen;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }

        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloPM.Properties.Resources.Img_OrangeBtnHoverSplashScreen;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }

        }

        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloPM.Properties.Resources.Img_BlueBtnSplashScreen;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }

        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloPM.Properties.Resources.Img_BlueBtnHoverSplashScreen;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }

        }


        ////***************Add for TaskBar Minimized ***************
        //private void frmSplash_SizeChanged(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.Region = null;
        //    }
        //    else
        //    {
        //        if (bAfterLoad)
        //        {
        //            gloGlobal.BitmapRegion.CreateControlRegion(this, bmpFrmBack);
        //        }
        //    }
        //}

        //private void frmSplash_Resize(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.Region = null;
        //    }
        //    else
        //    {
        //        if (bAfterLoad)
        //        {
        //            gloGlobal.BitmapRegion.CreateControlRegion(this, bmpFrmBack);
        //        }
        //    }
        //    //***************End for TaskBar Minimized ***************

        //}
    }
}
