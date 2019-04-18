using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using Microsoft.Win32;

namespace gloSettings
{
    public partial class frmSettings : Form
    {
        #region "Variable Declaration"

        private string _databaseConnectionString = "";
        private string _messageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _userID = 0;
        private bool nonNumberEntered = false;
        private bool nonNoOfApptInHour= false;
        private bool nonNoAutoLockTime = false;
        // C1 Grid Contants
        const Int32 COL_PROVIDERID = 0;
        const Int32 COL_PROVIDERNAME = 1;
        const Int32 COL_SUBMITTER = 2;
        const Int32 COL_RENDERING = 3;
        const Int32 COL_BILLING = 4;
        const Int32 COL_COUNT = 5;
        //


        #endregion

        #region "Constructor"

        public frmSettings()
        {
            InitializeComponent();

            if (appSettings["ClinicID"] != null)
            {
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            }
            else
            {
                _ClinicID = 0;
            }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userID = Convert.ToInt64(appSettings["UserID"]); }
                else { _userID = 0; }
            }
            else
            { _userID = 0; }


            //Added By Pramod Nair For Messagebox Caption 
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

        public frmSettings(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            }
            else
            {
                _ClinicID = 0;
            }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userID = Convert.ToInt64(appSettings["UserID"]); }
                else { _userID = 0; }
            }
            else
            { _userID = 0; }


            //Added By Pramod Nair For Messagebox Caption 
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

        #endregion

        # region " Private Variables "

        String _LockScreenTime;
        //public bool gblnAutoLockEnable = false;
        //Variables gloEMR Database Setting
        private string _EMRsqlservername = "";
        private string _EMRdatabasename = "";
        private bool _EMRwindowsauthentication = false;
        private string _EMRSQLusername = "";
        private string _EMRSQLpassword = "";
        private Boolean _blnGetEMRTreatment = false;
        
        #endregion

        # region " Property Procedures "
        public string LockScreenTime
        {
            get { return _LockScreenTime; }
            set { _LockScreenTime = value; }
        }

        public string EMRSQLServerName
        {
            get { return _EMRsqlservername; }
            set { _EMRsqlservername = value; }
        }

        public string EMRSQLDatabaseName
        {
            get { return _EMRdatabasename; }
            set { _EMRdatabasename = value; }
        }

        public bool EMRWindowsAuthentication
        {
            get { return _EMRwindowsauthentication; }
            set { _EMRwindowsauthentication = value; }
        }

        public string EMRSQLusername
        {
            get { return _EMRSQLusername; }
            set { _EMRSQLusername = value; }
        }

        public string EMRSQLpassword
        {
            get { return _EMRSQLpassword; }
            set { _EMRSQLpassword = value; }
        }

        public Boolean IsGetEMRTreatment
        {
            get { return _blnGetEMRTreatment; }
            set { _blnGetEMRTreatment = value; }
        }


        #endregion

        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                num_NoofColOnCalndr.ContextMenu = null;// new ContextMenu();
                num_NoofApptInaSlot.ContextMenu = null; // new ContextMenu();
                num_LockScreen.ContextMenu = null; // new ContextMenu();
                GetNoOfColsOnCalendar();
                // _databaseconnectionstring = DatabaseConnectionString;
                DesignGrid();
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);

                if (gloRegistrySetting.GetRegistryValue("AutoLockEnable") != null)
                {
                    if (gloRegistrySetting.GetRegistryValue("AutoLockEnable").ToString() == "1")
                    {
                        //Added by Sandip Dhakane on 28-09-2010 
                        chkAutoApplicationLock.Checked = true;//gloRegistrySetting.gblnAutoLockEnable;
                        gloRegistrySetting.gblnAutoLockEnable = true;
                    }
                    else
                    {
                        //Added by Sandip Dhakane on 28-09-2010 
                        chkAutoApplicationLock.Checked = false; //    gloRegistrySetting.gblnAutoLockEnable;
                        gloRegistrySetting.gblnAutoLockEnable = false;
                    }
                    num_LockScreen.Value = Convert.ToDecimal(_LockScreenTime);
                }
                else
                {
                    chkAutoApplicationLock.Checked = true;
                    num_LockScreen.Value = 10;
                }

                if (gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") != null)
                {
                    if (gloRegistrySetting.GetRegistryValue("UseDefaultPrinter").ToString() == "1")
                    {
                        chkUseDefaultPrinter.Checked = true;
                    }
                    else
                    {
                        chkUseDefaultPrinter.Checked = false;
                    }
                }
                else
                {
                    chkUseDefaultPrinter.Checked = false;
                }

                //Check gloTSprint Variable
                if ( (gloGlobal.gloTSPrint.TerminalServer() != "RDP") || !gloGlobal.gloTSPrint.isMapped() )
                {
                    gbRemotePrintSetting.Enabled = false;
                }
                else
                {
                    //Fill No of pages to split combo box
                    cmbNoPagesSplit.Items.Clear();
                    for (int i = 0; i <= 20; i = i + 2)
                    {
                        cmbNoPagesSplit.Items.Add(i);
                    }
                    int index = 0;
                    //Fill No of pages to split combo box
                    cmbNoTemplatesJob.Items.Clear();
                    for (int i = 0; i <= 30; i = i + 1)
                    {
                        cmbNoTemplatesJob.Items.Add(i);
                    }
                    int jindex = 1;
                    chkAddFooterService.Enabled = false;
                    cmbNoPagesSplit.Enabled = false;
                    cmbNoTemplatesJob.Enabled = false;
                    rbPrintWordDocPDF.Enabled = false;
                    rbPrintWordDocEMF.Enabled = false;
                    rbPrintSSRSReportPDF.Enabled = false;
                    rbPrintSSRSReportEMF.Enabled = false;
                    rbPrintClaimsPDF.Enabled = false;
                    rbPrintClaimsEMF.Enabled = false;
                    rbPrintImagesPNG.Enabled = false;
                    rbPrintImagesEMF.Enabled = false;
                    chkZipMetadata.Enabled = false;
                    if (gloRegistrySetting.GetRegistryValue("EnableLocalPrinter") != null)
                    {
                        if (gloRegistrySetting.GetRegistryValue("EnableLocalPrinter").ToString() == "1")
                        {
                            chkEnableLocalPrinter.Checked = true;
                            chkAddFooterService.Enabled = true;
                            cmbNoPagesSplit.Enabled = true;
                            cmbNoTemplatesJob.Enabled = true;
                            rbPrintWordDocPDF.Enabled = true;
                            rbPrintWordDocEMF.Enabled = true;
                            rbPrintSSRSReportPDF.Enabled = true;
                            rbPrintSSRSReportEMF.Enabled = true;
                            rbPrintClaimsPDF.Enabled = true;
                            rbPrintClaimsEMF.Enabled = true;
                            rbPrintImagesPNG.Enabled = true;
                            rbPrintImagesEMF.Enabled = true;
                            chkZipMetadata.Enabled = true;
                            index = cmbNoPagesSplit.Items.IndexOf(gloGlobal.gloTSPrint.NoOfPages);
                            if (index == -1)
                                index = 0;
                            jindex = cmbNoTemplatesJob.Items.IndexOf(gloGlobal.gloTSPrint.NoOfTemplatesPerJob);
                            if (jindex == -1)
                                jindex = 1;
                            if (gloRegistrySetting.GetRegistryValue("AddFooterInService") != null)
                            {
                                if (gloRegistrySetting.GetRegistryValue("AddFooterInService").ToString() == "1")
                                {
                                    chkAddFooterService.Checked = true;
                                }
                                else
                                {
                                    chkAddFooterService.Checked = false;
                                }
                            }
                            chkZipMetadata.Checked = gloGlobal.gloTSPrint.UseZippedMetadata;
                        }
                        else
                        {
                            chkEnableLocalPrinter.Checked = false;
                            chkAddFooterService.Checked = false;
                            chkZipMetadata.Checked = false;
                        }
                    }
                    if (gloGlobal.gloTSPrint.UseEMFForWord)
                    {
                        rbPrintWordDocEMF.Checked = true;
                        rbPrintWordDocPDF.Checked = false;
                    }
                    else
                    {
                        rbPrintWordDocEMF.Checked = false;
                        rbPrintWordDocPDF.Checked = true;
                    }
                    if (gloGlobal.gloTSPrint.UseEMFForSSRS)
                    {
                        rbPrintSSRSReportEMF.Checked = true;
                        rbPrintSSRSReportPDF.Checked = false;
                    }
                    else
                    {
                        rbPrintSSRSReportEMF.Checked = false;
                        rbPrintSSRSReportPDF.Checked = true;
                    }
                    if (gloGlobal.gloTSPrint.UseEMFForClaims)
                    {
                        rbPrintClaimsEMF.Checked = true;
                        rbPrintClaimsPDF.Checked = false;
                    }
                    else
                    {
                        rbPrintClaimsEMF.Checked = false;
                        rbPrintClaimsPDF.Checked = true;
                    }
                    if (gloGlobal.gloTSPrint.UseEMFForImages)
                    {
                        rbPrintImagesEMF.Checked = true;
                        rbPrintImagesPNG.Checked = false;
                    }
                    else
                    {
                        rbPrintImagesEMF.Checked = false;
                        rbPrintImagesPNG.Checked = true;
                    }
                    cmbNoPagesSplit.SelectedIndex = index;
                    cmbNoTemplatesJob.SelectedIndex = jindex;
                }
                
              



                if (chkAutoApplicationLock.Checked == false)
                    num_LockScreen.Enabled = false;
                //---- Fill Patient Columns Tree Nodes 
                TreeNode oNode;
                oNode = new TreeNode("Middle Name");
                oNode.Tag = "MI";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("SSN");
                oNode.Tag = "SSN";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Date Of Birth");
                oNode.Tag = "DOB";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Provider");
                oNode.Tag = "Provider";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Phone");
                oNode.Tag = "Phone";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Mobile");
                oNode.Tag = "Mobile";
                trvPatientColumns.Nodes.Add(oNode);
                oNode = null;
                //--------------------------------------

                //CR00000334 :Add new setting for searching patient.
                //Fill Patient Search Tree Nodes.
                //START
                #region"Patient Search"
                TreeNode oSearchNode;
                oSearchNode = new TreeNode("Middle Name");
                oSearchNode.Tag = "MI";
                trvPatientSearch.Nodes.Add(oSearchNode);
                oSearchNode = null;

                oSearchNode = new TreeNode("SSN");
                oSearchNode.Tag = "SSN";
                trvPatientSearch.Nodes.Add(oSearchNode);
                oSearchNode = null;

                oSearchNode = new TreeNode("Date Of Birth");
                oSearchNode.Tag = "DOB";
                trvPatientSearch.Nodes.Add(oSearchNode);
                oSearchNode = null;

                oSearchNode = new TreeNode("Phone");
                oSearchNode.Tag = "Phone";
                trvPatientSearch.Nodes.Add(oSearchNode);
                oSearchNode = null;

                oSearchNode = new TreeNode("Mobile");
                oSearchNode.Tag = "Mobile";
                trvPatientSearch.Nodes.Add(oSearchNode);
                oSearchNode = null;
                #endregion
                //END

                FillPatientDemographics();
                FillProviders();

                cmbAuthentication.Items.Add("Windows");
                cmbAuthentication.Items.Add("SQL");
                if (cmbAuthentication.Items.Count > 0)
                {
                    cmbAuthentication.SelectedIndex = 0;
                }

                FillSettings();
                tb_Settings.TabPages.Remove(tbpg_EMRDBSettings);
                tb_Settings.TabPages.Remove(tbpg_BillingSettings);
                tb_Settings.TabPages.Remove(tbpg_ExchangeSettings);
                tb_Settings.TabPages.Remove(tbpg_ProviderSettings);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                gloRegistrySetting.CloseRegistryKey();
            }

        }
        private void Read_ErrorLogSettings()
        {
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
            if (gloRegistrySetting.GetRegistryValue("EnableApplicationLogs") == null)
            {
                gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", false);
                chk_EnableApplicationLogs.Checked = false;
            }
            else
            {
                chk_EnableApplicationLogs.Checked = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("EnableApplicationLogs"));
            }

            if (gloRegistrySetting.GetRegistryValue("EnableErrorLogs") == null)
            {
                gloRegistrySetting.SetRegistryValue("EnableErrorLogs", true);
                chk_EnableErrorLogs.Checked = true;
            }
            else
            {
                chk_EnableErrorLogs.Checked = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("EnableErrorLogs"));
            }
            gloRegistrySetting.CloseRegistryKey();
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_EnableApplicationLogs.Checked;
            gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs  = chk_EnableErrorLogs.Checked;
        }

        private void GetNoOfColsOnCalendar()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                string _sNoOfColsOnCalendar = oSettings.ReadSettings_XML("Appointment", "NoOfColsOnCalendar");
                if (_sNoOfColsOnCalendar.Trim() != "")
                {
                    num_NoofColOnCalndr.Value = Convert.ToInt32(_sNoOfColsOnCalendar);
                }
                else
                {
                    num_NoofColOnCalndr.Value = 3;
                }
                _sNoOfColsOnCalendar = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
            }
        }

        private void FillProviders()
        {
            try
            {
                DataTable dtProvider = new DataTable();
                dtProvider = gloGlobal.gloPMMasters.GetProviders();

                if (dtProvider != null)
                {
                    DataRow dr = dtProvider.NewRow();
                    dr["nProviderID"] = "0";
                    dr["sProviderName"] = "";
                    dtProvider.Rows.InsertAt(dr, 0);
                    dtProvider.AcceptChanges();
 
                    cmbDefaultProvider.DataSource = dtProvider;
                    cmbDefaultProvider.DisplayMember = "sProviderName";
                    cmbDefaultProvider.ValueMember = "nProviderID"; 
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #region "Tool Strip Buttons Events"

        private void btnOK_Click(object sender, EventArgs e)
        {
            //object value;
            try
            {
                if (ValidateData() == true)
                {
                    SaveSettings();
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    oSettings.DBConnectionString = _databaseConnectionString;
                    _LockScreenTime = num_LockScreen.Value.ToString();
                    gloRegistrySetting.SetRegistryValue("LockTime", num_LockScreen.Value);
                    gloRegistrySetting.gblnAutoLockEnable = chkAutoApplicationLock.Checked;
                    if (gloRegistrySetting.gblnAutoLockEnable == true)
                        //regKey.SetValue("AutoLockEnable", "1")
                        gloRegistrySetting.SetRegistryValue("AutoLockEnable", "1");
                    else
                        //regKey.SetValue("AutoLockEnable", "0")
                        gloRegistrySetting.SetRegistryValue("AutoLockEnable", "0");

                    if (chkUseDefaultPrinter.Checked == true)
                    {
                        appSettings.Set("DefaultPrinter", "true");
                        gloRegistrySetting.SetRegistryValue("UseDefaultPrinter", "1");
                    }
                    else
                    {
                        appSettings.Set("DefaultPrinter", "false");
                        gloRegistrySetting.SetRegistryValue("UseDefaultPrinter", "0");
                    }
                    //Added gloTSprint variable to Registry                 
                    // Local Printer setting
                    if (chkEnableLocalPrinter.Checked == true)
                    {
                        gloGlobal.gloTSPrint.isCopyPrint = true;
                        gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "1");
                    }
                    else 
                    {
                        gloGlobal.gloTSPrint.isCopyPrint = false;
                        gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "0");                  
                    }

                    //Footer setting
                    if (chkAddFooterService.Checked == true)
                    {
                        gloGlobal.gloTSPrint.AddFooterInService = true;
                        gloRegistrySetting.SetRegistryValue("AddFooterInService", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.AddFooterInService = false;
                        gloRegistrySetting.SetRegistryValue("AddFooterInService", "0");
                    }

                    //Page Split Settings
                    if (cmbNoPagesSplit.Items.Count > 0)
                    {
                        gloRegistrySetting.SetRegistryValue("NoOfPagesToSplit", cmbNoPagesSplit.SelectedItem);
                        int result = 0;
                        int.TryParse(cmbNoPagesSplit.SelectedItem.ToString(), out result);
                        gloGlobal.gloTSPrint.NoOfPages = result;
                    }

                    //No of templates per job Settings
                    if (cmbNoTemplatesJob.Items.Count > 0)
                    {
                        try
                        {
                            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);
                            gloRegistrySetting.SetRegistryValue("NoOfTemplatesPerJob", cmbNoTemplatesJob.SelectedItem);
                            int result = 0;
                            int.TryParse(cmbNoTemplatesJob.SelectedItem.ToString(), out result);
                            gloGlobal.gloTSPrint.NoOfTemplatesPerJob = result;
                            gloRegistrySetting.CloseRegistryKey();
                            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                        }
                        catch
                        {
                            gloRegistrySetting.CloseRegistryKey();
                            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                        }
                    }

                    //File type setting for PDF/EMF
                    if (rbPrintWordDocEMF.Checked == true)
                    {
                        gloGlobal.gloTSPrint.UseEMFForWord = true;
                        gloRegistrySetting.SetRegistryValue("UseEMFFile", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.UseEMFForWord = false;
                        gloRegistrySetting.SetRegistryValue("UseEMFFile", "0");
                    }

                    //File type setting for PDF/EMF
                    if (rbPrintSSRSReportEMF.Checked == true)
                    {
                        gloGlobal.gloTSPrint.UseEMFForSSRS = true;
                        gloRegistrySetting.SetRegistryValue("UseEMFFileSSRS", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.UseEMFForSSRS = false;
                        gloRegistrySetting.SetRegistryValue("UseEMFFileSSRS", "0");
                    }

                    //File type setting for PDF/EMF for claim printing
                    if (rbPrintClaimsEMF.Checked == true)
                    {
                        gloGlobal.gloTSPrint.UseEMFForClaims = true;
                        gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.UseEMFForClaims = false;
                        gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "0");
                    }

                    //File type setting for PDF/EMF for Images printing
                    if (rbPrintImagesEMF.Checked == true)
                    {
                        gloGlobal.gloTSPrint.UseEMFForImages = true;
                        gloRegistrySetting.SetRegistryValue("UseEMFForImages", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.UseEMFForImages = false;
                        gloRegistrySetting.SetRegistryValue("UseEMFForImages", "0");
                    }
                    // Setting for using zipped metadata file
                    if (chkZipMetadata.Checked == true)
                    {
                        gloGlobal.gloTSPrint.UseZippedMetadata = true;
                        gloRegistrySetting.SetRegistryValue("UseZippedMetadata", "1");
                    }
                    else
                    {
                        gloGlobal.gloTSPrint.UseZippedMetadata = false;
                        gloRegistrySetting.SetRegistryValue("UseZippedMetadata", "0");
                    }

                    //End
                    


                    //Saving the last Patient to file.
                    oSettings.WriteSettings_XML(oSettings.ProfileGeneralSettings, "LOCKSCREENTIME", Convert.ToString(_LockScreenTime));
                    oSettings.WriteSettings_XML("Appointment", "NoOfColsOnCalendar", num_NoofColOnCalndr.Value.ToString());
                    oSettings.Dispose();
                    oSettings = null;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                gloRegistrySetting.CloseRegistryKey();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Setting, ActivityCategory.None, ActivityType.View, "View Settings", 0, 0, 0, ActivityOutCome.Success);
            this.Close();
        }

        #endregion       

        #region "Save / Load Settings"

        private bool ValidateData()
        {
            //if (txtPatientCodePrefix.Text.Trim().Length != 3 && txtPatientCodePrefix.Text.Trim() != "")
            //{
            //    MessageBox.Show("Patient Code Prefix must be 3 characters ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtPatientCodePrefix.Focus();
            //    return false;
            //}
            return true;
        }

        private void SaveSettings()
        {
            gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseConnectionString);
            try
            {
                #region "Patient Default Search Column"

                if (cmbSearchColumns.SelectedIndex != -1)
                {
                    ogloSettings.AddSetting("Patient Search Column", cmbSearchColumns.SelectedItem.ToString(), _ClinicID, 0, SettingFlag.Clinic);
                }

                #endregion

                #region "Patient Display Columns"

                string _SettingValue = "";
                for (int i = 0; i < trvPatientColumns.Nodes.Count; i++)
                {
                    if (trvPatientColumns.Nodes[i].Checked == true)
                    {
                        _SettingValue += "," + trvPatientColumns.Nodes[i].Tag.ToString().Trim();
                    }
                }
                if (_SettingValue.Trim() != "")
                {
                    _SettingValue = _SettingValue.Substring(1, _SettingValue.Length - 1);
                }
                ogloSettings.AddSetting("Patient Columns", _SettingValue, _ClinicID, _userID , SettingFlag.Clinic);

                _SettingValue = null;
                #endregion

                //CR00000334 :Add new setting for searching patient.
                //Added to save checked node in settings table for patient search.
                //START
                #region "Patient Search"

                string _SearchValue = "";
                for (int i = 0; i < trvPatientSearch.Nodes.Count; i++)
                {
                    if (trvPatientSearch.Nodes[i].Checked == true)
                    {
                        _SearchValue += "," + trvPatientSearch.Nodes[i].Tag.ToString().Trim();
                    }
                }
                if (_SearchValue.Trim() != "")
                {
                    _SearchValue = _SearchValue.Substring(1, _SearchValue.Length - 1);
                }
                ogloSettings.AddSetting("Patient Search", _SearchValue, _ClinicID, _userID, SettingFlag.Clinic);

                _SearchValue = null;
                #endregion
                //END

                #region "Patient Demographics"

                string _DemographicsSettingValue = "";
                for (int i = 0; i < trvDemographics.Nodes.Count; i++)
                {
                    if (trvDemographics.Nodes[i].Checked == true)
                    {
                        _DemographicsSettingValue += "," + trvDemographics.Nodes[i].Tag.ToString().Trim();
                    }
                }
                if (_DemographicsSettingValue.Trim() != "")
                {
                    _DemographicsSettingValue = _DemographicsSettingValue.Substring(1, _DemographicsSettingValue.Length - 1);
                }
                ogloSettings.AddSetting("Patient Demographics PM", _DemographicsSettingValue, _ClinicID, _userID, SettingFlag.Clinic);
                
                _DemographicsSettingValue = null;
                #endregion

                #region "Patient Code"
                
                ogloSettings.AddSetting("PatientCodePrefix", txtPatientCodePrefix.Text.ToString(), _ClinicID, 0, SettingFlag.Clinic);
                ogloSettings.AddSetting("PatientCodeIncrement", numPatientCodeIncrement.Value.ToString(), _ClinicID, 0, SettingFlag.Clinic);

                #endregion

                #region Patient Default Provider

                if(cmbDefaultProvider.SelectedIndex > -1)
                {
                    ogloSettings.AddSetting("PatientDefaultProvider",cmbDefaultProvider.SelectedValue.ToString() , _ClinicID, 0, SettingFlag.Clinic);
                }

                #endregion

                //Commented by Pranit 24 sep 11 This field is taken in admin
                //#region " Restricted Template Appointments"

                //ogloSettings.AddSetting("RegisterTemplateAppointmentOnly", chbox_restrictedaptmnt.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic);

                //#endregion

                #region " Export to Default Location"

                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

                oSettings.WriteSettings_XML("Reports", "ExportToDefaultLocation", chbox_ExptoDefaultlocation.Checked.ToString());
                oSettings.WriteSettings_XML("Reports", "ExportToDefaultLocationPath", Convert.ToString(txt_Path.Text));
                oSettings.Dispose(); 

                //ogloSettings.AddSetting("ExportToDefaultLocation", chbox_ExptoDefaultlocation.Checked.ToString(), _ClinicID, 0, SettingFlag.Clinic);
                //ogloSettings.AddSetting("ExportToDefaultLocationPath ",Convert.ToString(txt_Path.Text), _ClinicID, 0, SettingFlag.Clinic);

                 #endregion

                #region "Appointment"

                ogloSettings.AddSetting("MaxAppointmentsInSlot", num_NoofApptInaSlot.Value.ToString(), _ClinicID, 0, SettingFlag.Clinic);
                ogloSettings.AddSetting("ShowTemplate", chkShowTemplate.Checked.ToString() , _ClinicID, 0, SettingFlag.Clinic);

                if (rbFollowupFromToday.Checked == true)
                {
                    ogloSettings.AddSetting("FolloupDate", "Today", _ClinicID, 0, SettingFlag.Clinic);
                }
                else
                {
                    ogloSettings.AddSetting("FolloupDate", "FolloupDate", _ClinicID, 0, SettingFlag.Clinic);
                }


                #endregion

                #region "Exchange Server Settings"

                ogloSettings.AddSetting("ExchangeDomain", txtExchangeDomain.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                ogloSettings.AddSetting("ExchangeURL", txtExchangeURL.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);

                #endregion

                #region "gloEMR Database Settings"

                ogloSettings.AddSetting("gloEMR SQL Server Name", txt_EMRDB_ServerName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                ogloSettings.AddSetting("gloEMR Database Name", txt_EMRDB_Database.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                ogloSettings.AddSetting("gloEMR Authentication", cmbAuthentication.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                if (cmbAuthentication.Text.Trim() == "SQL")
                {
                    ogloSettings.AddSetting("gloEMR User Name", txt_EMRDB_UserName.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                    ogloSettings.AddSetting("gloEMR Password", txt_EMRDB_Password.Text.Trim(), _ClinicID, 0, SettingFlag.Clinic);
                }
                else if (cmbAuthentication.Text.Trim() == "Windows")
                {
                    ogloSettings.AddSetting("gloEMR User Name", "", _ClinicID, 0, SettingFlag.Clinic);
                    ogloSettings.AddSetting("gloEMR Password", "", _ClinicID, 0, SettingFlag.Clinic);
                }

                #endregion

                #region "Provider Settings"

                for (int i = 1; i < c1Providers.Rows.Count ; i++)
                {
                    string sSubmitter = "";
                    string sRendering = "";
                    string sBilling = "";

                    Int64 nProviderID = 0;
                    sSubmitter = Convert.ToString(c1Providers.GetData(i, COL_SUBMITTER));
                    sRendering = Convert.ToString(c1Providers.GetData(i, COL_RENDERING));
                    sBilling = Convert.ToString(c1Providers.GetData(i, COL_BILLING));

                    nProviderID = Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDERID));
                    ogloSettings.AddSetting("SubmitterSetting", sSubmitter, _ClinicID, nProviderID ,SettingFlag.User);
                    ogloSettings.AddSetting("BillingSetting", sBilling, _ClinicID, nProviderID, SettingFlag.User);
                    ogloSettings.AddSetting("RenderingSetting", sRendering, _ClinicID, nProviderID, SettingFlag.User);

                }

                #endregion

                #region " Billing Settings "

                ogloSettings.AddSetting("NoOfDiagnosis", Convert.ToString(numDiagnosis.Value), _ClinicID, _userID, SettingFlag.Clinic);
                //ogloSettings.AddSetting("NoOfModifiers", Convert.ToString(numModifiers.Value), _ClinicID, _userID, SettingFlag.Clinic);

                #endregion " Billing Settings "

                #region " Alert Settings "

                ogloSettings.AddSetting("BlinkingAlert", Convert.ToString(chkShowBlinkAlert.Checked.ToString()), _ClinicID, _userID, SettingFlag.Clinic);
                ogloSettings.AddSetting("AlertColor", Convert.ToString(txtAlertColor.BackColor.ToArgb()), _ClinicID, _userID, SettingFlag.Clinic);

                #endregion " Alert Settings "


                #region "HL7 Settings"

                // Added by Abhijeet on 20110926. commenetd code for old combined outbound setting. 
                //Added new code for separate outbound setting to be use
                
                //appSettings["GenerateHL7Message"] = chkHL7Message.Checked.ToString();
                //code commented on 21-nov-2012
                //if (chkGenerateOutboundMsg.Checked)
                //{
                //    appSettings["SendPatientDetails"] = chkSendPatientDetails.Checked.ToString();
                //    appSettings["SendAppointmentDetails"] = chkSendAppointmentDetails.Checked.ToString();
                //    appSettings["GenerateHL7Message"] = chkHL7.Checked.ToString();
                //    appSettings["GenerateOutboundMessage"] = chkGenerateOutboundMsg.Checked.ToString();
                //}
                //else
                //{
                //    appSettings["SendPatientDetails"] = "False";
                //    appSettings["SendAppointmentDetails"] = "False";
                //    appSettings["GenerateHL7Message"] = "False";
                //    appSettings["GenerateOutboundMessage"] = "False";
                //}
                //code above  commented on 21-nov-2012
                //End of changes by Abhijeet on 20110926

               // write settings to registry 
               //// //RegistryKey oRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
                //////oRegistryKey = oRegistryKey.CreateSubKey("Software\\gloPM");
               //// //oRegistryKey.SetValue("GenerateHL7Message", chkHL7Message.Checked.ToString());
               //// //oRegistryKey.Close();

                //****** added by sandip dhakane 20100707
                //code commented on 21-nov-2012
              //  gloRegistrySetting.OpenRemoteBaseKey();
              //  gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftPM);
                //code above commented on 21-nov-2012
                //Added by Abhijeet on 20110926. commented code for old combined outbound setting 
                //Added new code for separate outbounnd setting to be use
               
                //////gloRegistrySetting.SetRegistryValue("GenerateHL7Message", chkHL7Message.Checked.ToString());               
                //code commented on 21-nov-2012
                //if (chkGenerateOutboundMsg.Checked)
                //{
                //    gloRegistrySetting.SetRegistryValue("SendPatientDetails", chkSendPatientDetails.Checked.ToString());
                //    gloRegistrySetting.SetRegistryValue("SendAppointmentDetails", chkSendAppointmentDetails.Checked.ToString());
                //    gloRegistrySetting.SetRegistryValue("GenerateHL7Message", chkHL7.Checked.ToString());
                //    gloRegistrySetting.SetRegistryValue("GenerateOutboundMessage", chkGenerateOutboundMsg.Checked.ToString());
                //}
                //else
                //{
                //    gloRegistrySetting.SetRegistryValue("SendPatientDetails", "False");
                //    gloRegistrySetting.SetRegistryValue("SendAppointmentDetails", "False");
                //    gloRegistrySetting.SetRegistryValue("GenerateHL7Message", "False");
                //    gloRegistrySetting.SetRegistryValue("GenerateOutboundMessage", "False");
                //}
                //End of changes by Abhijeet on 20110926
               // gloRegistrySetting.CloseRegistryKey();
                //code above commented on 21-nov-2012
         
                #endregion

                #region "Error Log Settings"
                         gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                        if (chk_EnableApplicationLogs.Checked == true)
                        {
                            gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", true);                    
                        }
                        else
                        {
                            gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", false);
                        }

                        if (chk_EnableErrorLogs.Checked  == true)
                        {
                            gloRegistrySetting.SetRegistryValue("EnableErrorLogs", true);
                        }
                        else
                        {
                            gloRegistrySetting.SetRegistryValue("EnableErrorLogs", false);
                        }
                        gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_EnableApplicationLogs.Checked;
                        gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = chk_EnableErrorLogs.Checked;
                #endregion

                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Setting, ActivityCategory.None, ActivityType.Modify, "Settings modified", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Setting, ActivityCategory.None, ActivityType.Modify, "Modify Settings", 0, 0, 0, ActivityOutCome.Failure);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                gloRegistrySetting.CloseRegistryKey();
            }
        }

        private void FillSettings()
        {
            gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseConnectionString);
            object value = new object();
            try
            {
                #region "Patient Default Search Column"

                ogloSettings.GetSetting("Patient Search Column", out value);
                if (value != null)
                {
                    cmbSearchColumns.SelectedItem = Convert.ToString(value);
                }
                value = null;

                #endregion


                //Commented by Pranit 24 sep 11 This field is taken in admin
                //#region "Restricted Template Appointments"

                //ogloSettings.GetSetting("RegisterTemplateAppointmentOnly", out value);
                //if (value != null)
                //{
                //    if (value != "")
                //    {
                //        chbox_restrictedaptmnt.Checked = Convert.ToBoolean(value);
                //    }
                //}
                //value = null;

                // #endregion

                #region "Export To Default Location"

                //ogloSettings.GetSetting("ExportToDefaultLocation", out value);
                //if (value != null)
                //{
                //    if (value != "")
                //    {
                //        chbox_ExptoDefaultlocation.Checked = Convert.ToBoolean(value);
                //        if (chbox_ExptoDefaultlocation.Checked == false)
                //        {
                //            txt_Path.Enabled = false;
                //        }
                //        else
                //        {
                //            txt_Path.Enabled = true;
                //        }

                //    }
                //}
                //value = null;
                //ogloSettings.GetSetting("ExportToDefaultLocationPath", out value);
                //if (value != null)
                //{
                //    if (value != "")
                //    {
                //        txt_Path.Text = Convert.ToString(value);
                //    }
                //}
                //value = null;

                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

                btn_Browse.Enabled = false;
                btn_Clear.Enabled = false;

                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    chbox_ExptoDefaultlocation.Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    chbox_ExptoDefaultlocation.Checked = false; 
                }
                txt_Path.Text = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                
                oSettings.Dispose();
 
                #endregion

                #region "Appointments"

                ogloSettings.GetSetting("MaxAppointmentsInSlot", out value);
                if (value != null)
                {
                    if (Convert.ToString(value) != "")
                    {
                        num_NoofApptInaSlot.Value = Convert.ToInt16(value);
                    }
                }
                value = null;

                ogloSettings.GetSetting("ShowTemplate", out value);
                if (value != null)
                {
                    if (Convert.ToString(value) != "")
                    {
                        chkShowTemplate.Checked = Convert.ToBoolean(value);
                    }
                }
                value = null;

                ogloSettings.GetSetting("FolloupDate", out value);
                if (value != null)
                {
                    if (Convert.ToString(value) == "FolloupDate")
                        rbFolloupFromDate.Checked = true;
                    else
                        rbFollowupFromToday.Checked = true;  
                }
                value = null;


                #endregion

                #region "Patient Columns"

                //ogloSettings.GetSetting("Patient Columns", out value);
                ogloSettings.GetSetting("Patient Columns", _userID, _ClinicID, out value);

                if (value != null)
                {
                    if (Convert.ToString(value).Trim() != "")
                    {
                        string[] PatientColumns = Convert.ToString(value).Trim().Split(',');
                        for (int i = 0; i < PatientColumns.Length; i++)
                        {
                            for (int j = 0; j <= trvPatientColumns.Nodes.Count - 1; j++)
                            {
                                if (trvPatientColumns.Nodes[j].Tag.ToString().Trim() == PatientColumns[i].Trim())
                                {
                                    trvPatientColumns.Nodes[j].Checked = true;
                                }
                            }
                        }
                    }
                }
                value = null;
                #endregion

                //CR00000334 :Add new setting for searching patient.
                //Retrive setting from DB and mark node as check/uncheck.
                //START
                #region "Patient Search"
                
                //Added to check whether setting is present in table or not (for first time).
                DataTable CheckSetting = null;
                CheckSetting=ogloSettings.GetSetting("Patient Search");
                
                //if (CheckSetting.Rows.Count == 0)
                //{
                //    //If setting is not present in the table then checked all node in patient search (for first time only).
                //    for (int j = 0; j <= trvPatientSearch.Nodes.Count - 1; j++)
                //    {
                //        trvPatientSearch.Nodes[j].Checked = true;
                //    }
                //}
                //else
                //{
                bool chkUser = false;
                for (int s = 0; s < CheckSetting.Rows.Count; s++)
                {
                    if (CheckSetting.Rows[s][1].ToString().Trim() == _userID.ToString().Trim())
                    {
                        chkUser = true;
                        break;
                    }
                }

                if (CheckSetting != null) { CheckSetting.Dispose(); CheckSetting = null; }

                //If setting is present then retrive it and checked node according to patient search setting.
                if (chkUser)
                {
                    ogloSettings.GetSetting("Patient Search", _userID, _ClinicID, out value);

                    if (value != null)
                    {
                        if (Convert.ToString(value).Trim() != "")
                        {
                            string[] PatientSearch = Convert.ToString(value).Trim().Split(',');
                            for (int i = 0; i < PatientSearch.Length; i++)
                            {
                                for (int j = 0; j <= trvPatientSearch.Nodes.Count - 1; j++)
                                {
                                    if (trvPatientSearch.Nodes[j].Tag.ToString().Trim() == PatientSearch[i].Trim())
                                    {
                                        trvPatientSearch.Nodes[j].Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //If setting is not present in the table then checked all node in patient search (for current user).
                    for (int j = 0; j <= trvPatientSearch.Nodes.Count - 1; j++)
                    {
                        trvPatientSearch.Nodes[j].Checked = true;
                    }
                }
                    
                //}
                value = null;
                #endregion
                //END

                #region "Patient Demographics"

                //ogloSettings.GetSetting("Patient Demographics", out value);
                ogloSettings.GetSetting("Patient Demographics PM", _userID, _ClinicID, out value);

                if (value != null)
                {
                    if (Convert.ToString(value).Trim() != "")
                    {
                        string[] PatientDemographics = Convert.ToString(value).Trim().Split(',');
                        for (int i = 0; i < PatientDemographics.Length; i++)
                        {
                            for (int j = 0; j <= trvDemographics.Nodes.Count - 1; j++)
                            {
                                if (trvDemographics.Nodes[j].Tag.ToString().Trim() == PatientDemographics[i].Trim())
                                {
                                    trvDemographics.Nodes[j].Checked = true;
                                }
                            }
                        }
                    }
                }

                 #endregion

                #region "Patient Code Settings"
                ogloSettings.GetSetting("PatientCodePrefix", out value);
                if (value != null)
                {
                    txtPatientCodePrefix.Text = Convert.ToString(value);
                }
                value = null;

                ogloSettings.GetSetting("PatientCodeIncrement", out value);
                if (value != null)
                {
                    if (Convert.ToString(value).Trim() != "")
                        numPatientCodeIncrement.Value = Convert.ToDecimal(value);
                }
                value = null;

                #endregion

                #region "Patient Provider Settings"

                ogloSettings.GetSetting("PatientDefaultProvider", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    cmbDefaultProvider.SelectedValue = Convert.ToInt64(value);
                }
                value = null;

                #endregion

                #region "Exchange Server Settings"

                ogloSettings.GetSetting("ExchangeDomain", out value);
                if (value != null)
                {
                    txtExchangeDomain.Text = Convert.ToString(value);
                    value = null;
                }

                ogloSettings.GetSetting("ExchangeURL", out value);
                if (value != null)
                {
                    txtExchangeURL.Text = Convert.ToString(value);
                    value = null;
                }

                #endregion

                #region "gloEMR Database Settings"

                ogloSettings.GetSetting("gloEMR SQL Server Name", out value);
                if (value != null)
                {
                    txt_EMRDB_ServerName.Text = Convert.ToString(value);
                    value = null;
                }

                ogloSettings.GetSetting("gloEMR Database Name", out value);
                if (value != null)
                {
                    txt_EMRDB_Database.Text = Convert.ToString(value);
                    value = null;
                }
                ogloSettings.GetSetting("gloEMR Authentication", out value);
                if (value != null)
                {
                    if (Convert.ToString(value) == "SQL")
                    {
                        cmbAuthentication.SelectedIndex = 1;
                        value = null;
                        ogloSettings.GetSetting("gloEMR User Name", out value);
                        if (value != null)
                        {
                            txt_EMRDB_UserName.Text = Convert.ToString(value);
                            value = null;
                        }
                        ogloSettings.GetSetting("gloEMR Password", out value);
                        if (value != null)
                        {
                            txt_EMRDB_Password.Text = Convert.ToString(value);
                            value = null;
                        }
                    }
                    else
                    {
                        txt_EMRDB_UserName.Text = "";
                        txt_EMRDB_Password.Text = "";
                    }
                }
                #endregion

                #region "Provider Settings"

                DataTable dtSubmetter = ogloSettings.GetSetting("SubmitterSetting");
                if (dtSubmetter != null && dtSubmetter.Rows.Count > 0)
                {
                    for (int i = 1; i < c1Providers.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtSubmetter.Rows.Count; k++)
                        {
                            if (Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDERID)) == Convert.ToInt64(dtSubmetter.Rows[k]["nUserID"]))
                            {
                                c1Providers.SetData(i, COL_SUBMITTER, Convert.ToString(dtSubmetter.Rows[k]["sSettingsValue"]));
                                break;
                            }
                        }
                    }
                }

                DataTable dtBilling = ogloSettings.GetSetting("BillingSetting");
                if (dtBilling != null && dtBilling.Rows.Count > 0)
                {
                    for (int i = 1; i < c1Providers.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtBilling.Rows.Count; k++)
                        {
                            if (Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDERID)) == Convert.ToInt64(dtBilling.Rows[k]["nUserID"]))
                            {
                                c1Providers.SetData(i, COL_BILLING, Convert.ToString(dtBilling.Rows[k]["sSettingsValue"]));
                                break;
                            }
                        }
                    }
                }

                DataTable dtRendering = ogloSettings.GetSetting("RenderingSetting");
                if (dtRendering != null && dtRendering.Rows.Count > 0)
                {
                    for (int i = 1; i < c1Providers.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtRendering.Rows.Count; k++)
                        {
                            if (Convert.ToInt64(c1Providers.GetData(i, COL_PROVIDERID)) == Convert.ToInt64(dtRendering.Rows[k]["nUserID"]))
                            {
                                c1Providers.SetData(i, COL_RENDERING, Convert.ToString(dtRendering.Rows[k]["sSettingsValue"]));
                                break;
                            }
                        }
                    }
                } 

                #endregion

                #region " Billing Settings "

                ogloSettings.GetSetting("NoOfDiagnosis", out value);
                if (value != null && Convert.ToString(value) != "") { numDiagnosis.Value = Convert.ToInt32(value); }

                //ogloSettings.GetSetting("NoOfModifiers", out value);
                //if (value != null && Convert.ToString(value) != "") { numModifiers.Value = Convert.ToInt32(value); }


                #endregion " Billing Settings "

                #region " Alert Settings "

                ogloSettings.GetSetting("BlinkingAlert", _userID, _ClinicID, out value);
                if (value != null && Convert.ToString(value) != "") { chkShowBlinkAlert.Checked = Convert.ToBoolean(value); }

                ogloSettings.GetSetting("AlertColor", _userID, _ClinicID, out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    txtAlertColor.BackColor = Color.FromArgb(Convert.ToInt32(value));
                }
                else
                {
                    txtAlertColor.BackColor = Color.Red;
                }


                #endregion " Alert Settings "

                #region "HL7 Settings"
                //commented on 21-nov-2012 to disable hl7outbound setting
                //if (Convert.ToString(appSettings["GenerateOutboundMessage"]) != "")
                //{
                //    chkGenerateOutboundMsg.Checked = Convert.ToBoolean(appSettings["GenerateOutboundMessage"]);
                //}
                //else
                //{ chkGenerateOutboundMsg.Checked = false; }

                //if (Convert.ToString(appSettings["GenerateHL7Message"]) != "")
                //{
                //    chkHL7.Checked = Convert.ToBoolean(appSettings["GenerateHL7Message"]);
                //}
                //else
                //{ chkHL7.Checked = false; }

                //if (Convert.ToString(appSettings["SendPatientDetails"]) != "")
                //{
                //    chkSendPatientDetails.Checked = Convert.ToBoolean(appSettings["SendPatientDetails"]);
                //}
                //else
                //{ chkSendPatientDetails.Checked = false; }

                //if (Convert.ToString(appSettings["SendAppointmentDetails"]) != "")
                //{
                //    chkSendAppointmentDetails.Checked = Convert.ToBoolean(appSettings["SendAppointmentDetails"]);
                //}
                //else
                //{ chkSendAppointmentDetails.Checked = false; }
                //commented on 21-nov-2012 to disable hl7outbound setting
                panel24.Visible = false; //added on 21-nov-2012 to make hl7Outbound visibility false 
                #endregion

                #region "Error Log Settings"
                Read_ErrorLogSettings(); 
                #endregion""
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
            }
        } 

        #endregion

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.Text.Trim() == "Windows")
            {
                txt_EMRDB_UserName.Text = "";
                txt_EMRDB_Password.Text = "";
            }
            else
            {
                //RetrievegloEMRDatabaseSettings();
            }
        }

        private void DesignGrid()
        {
            try
            {
                c1Providers.Rows.Count = 1;
                c1Providers.Cols.Count = COL_COUNT;

                c1Providers.SetData(0, COL_PROVIDERID, "Provider ID");
                c1Providers.SetData(0, COL_PROVIDERNAME, "Provider Name");
                c1Providers.SetData(0, COL_SUBMITTER, "Submitter");
                c1Providers.SetData(0, COL_RENDERING, "Rendering");
                c1Providers.SetData(0, COL_BILLING, "Billing");

                c1Providers.Cols[COL_PROVIDERID].Visible = false;
                c1Providers.Cols[COL_PROVIDERNAME].Visible = true;
                c1Providers.Cols[COL_SUBMITTER].Visible = true;
                c1Providers.Cols[COL_RENDERING].Visible = true;
                c1Providers.Cols[COL_BILLING].Visible = true;

                c1Providers.AllowEditing = true;

                c1Providers.Cols[COL_PROVIDERID].AllowEditing = false;
                c1Providers.Cols[COL_PROVIDERNAME].AllowEditing = false;
                c1Providers.Cols[COL_SUBMITTER].AllowEditing = true;
                c1Providers.Cols[COL_RENDERING].AllowEditing = true;
                c1Providers.Cols[COL_BILLING].AllowEditing = true;

                c1Providers.Cols[COL_SUBMITTER].ComboList = " |Company|Practice|Business|Clinic";
                c1Providers.Cols[COL_BILLING].ComboList = " |Company|Practice|Business|Clinic";
                c1Providers.Cols[COL_RENDERING].ComboList = " |Company|Practice|Business|Clinic";

                Int32 _width = c1Providers.Width - 5;
                c1Providers.Cols[COL_PROVIDERNAME].Width = Convert.ToInt32(_width * 0.4);
                c1Providers.Cols[COL_SUBMITTER].Width = Convert.ToInt32(_width * 0.2);
                c1Providers.Cols[COL_RENDERING].Width = Convert.ToInt32(_width * 0.2);
                c1Providers.Cols[COL_BILLING].Width = Convert.ToInt32(_width * 0.2);


                //Fill Providers To Grid
                DataTable dtProviders = GetProviders();
                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProviders.Rows.Count; i++)
                    {
                        c1Providers.Rows.Add();
                        Int32 RowIndex = c1Providers.Rows.Count - 1;
                        c1Providers.SetData(RowIndex, COL_PROVIDERID, Convert.ToString(dtProviders.Rows[i]["nProviderID"]));
                        c1Providers.SetData(RowIndex, COL_PROVIDERNAME, Convert.ToString(dtProviders.Rows[i]["ProviderName"]));
                    }
                }
                if (dtProviders != null) { dtProviders.Dispose(); dtProviders = null; }
                //----------------
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillPatientDemographics()
        {
            try
            {
               //Added by Rahul Patel on 27-09-2010
               // For resolving the issuse  no :3993 
                trvDemographics.BeginUpdate();
                TreeNode oNode;
                oNode = new TreeNode("Date Of Birth");
                oNode.Tag = "DOB";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Gender");
                oNode.Tag = "Gender";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Phone");
                oNode.Tag = "Phone";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Mobile");
                oNode.Tag = "Mobile";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Fax");
                oNode.Tag = "Fax";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Email");
                oNode.Tag = "Email";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Emergency Contact");
                oNode.Tag = "Emergency Contact";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Emergency Phone");
                oNode.Tag = "Emergency Phone";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Emergency Mobile");
                oNode.Tag = "Emergency Mobile";
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Provider");
                oNode.Tag = "Provider";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;
                               
                oNode = new TreeNode("Pharmacy");
                oNode.Tag = "Pharmacy";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("PCP Phone");
                oNode.Tag = "PCP Phone";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("PCP Mobile");
                oNode.Tag = "PCP Mobile";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Primary Care Physician");
                oNode.Tag = "Primary Care Physician";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Referring Physician");
                oNode.Tag = "Referral";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Race");
                oNode.Tag = "Race";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Ethnicity");
                oNode.Tag = "Ethnicity";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Language");
                oNode.Tag = "Language";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Primary Insurance");
                oNode.Tag = "Primary Insurance";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Secondary Insurance");
                oNode.Tag = "Secondary Insurance";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Tertiary Insurance");
                oNode.Tag = "Tertiary Insurance";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Status");
                oNode.Tag = "Status";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;


                oNode = new TreeNode("Medical Category");
                oNode.Tag = "Medical Category";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Occupation");
                oNode.Tag = "Occupation";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Work Phone");
                oNode.Tag = "Work Phone";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;

                oNode = new TreeNode("Business Center");
                oNode.Tag = "Business Center";
                oNode.Checked = false;
                trvDemographics.Nodes.Add(oNode);
                oNode = null;
                
                //oNode = new TreeNode("Hidden");
                //oNode.Tag = "";
                //oNode.Checked = false;
                //trvDemographics.Nodes.Add(oNode);
                //oNode = null;

                //trvDemographics.ExpandAll();
                trvDemographics.Nodes[trvDemographics.Nodes.Count -1].EnsureVisible();
               
                //Added by Rahul Patel on 27-09-2010
                // For resolving the issuse  no :3993 
                trvDemographics.EndUpdate();
                //oNode = new TreeNode("        ");
                //oNode.Tag = "Emergency Mobile";
                              
                //trvDemographics.Nodes.Add(oNode);
                //oNode = null;

                //int lastNode =  trvDemographics.GetNodeCount(false);
                //trvDemographics.Nodes[8].EnsureVisible();
                //trvDemographics.Nodes[trvDemographics.Nodes.Count-1].EnsureVisible();
                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
            }
        }

        public DataTable GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            String _strSQL = "";
            DataTable dtProviderType = null;
            try
            {
                if (this._ClinicID == 0)
                    _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST ORDER BY ProviderName";
                else
                    _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE nClinicID = " + this._ClinicID + " ORDER BY ProviderName";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out dtProviderType);
                oDB.Disconnect();
                if (dtProviderType != null && dtProviderType.Rows.Count > 0)
                {
                    return dtProviderType;
                }
                else
                {
                    return null;
                }
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                _strSQL = null;
            }
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

            if (txt_Path.Text.Trim() != "")
            {
                openFileDialog1.SelectedPath = txt_Path.Text.Trim();
            }
            else
            {
                openFileDialog1.SelectedPath = Application.StartupPath;
            }


            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txt_Path.Text = openFileDialog1.SelectedPath;
                
            }
            openFileDialog1.Dispose();
            openFileDialog1 = null;
        }

        private void chbox_ExptoDefaultlocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chbox_ExptoDefaultlocation.Checked == false)
            {
                txt_Path.Enabled = false;
                btn_Browse.Enabled = false;
                btn_Clear.Enabled = false;
            }
            else
            {
                txt_Path.Enabled = true;
                btn_Browse.Enabled = true;
                btn_Clear.Enabled = true;
            }

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Path.Text = "";
       }

        private void rbFolloupFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFolloupFromDate.Checked == true)
            {
                rbFolloupFromDate.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbFolloupFromDate.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbFollowupFromToday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFollowupFromToday.Checked == true)
            {
                rbFollowupFromToday.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbFollowupFromToday.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
               
        private void btnBrowseAppColor_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    clDlg.CustomColors = gloGlobal.gloCustomColor.customColor;
                }
                catch
                {
                }
                if (clDlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtAlertColor.BackColor = clDlg.Color;
                    try
                    {
                        gloGlobal.gloCustomColor.customColor = clDlg.CustomColors;
                    }
                    catch
                    {
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void chkAutoApplicationLock_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAutoApplicationLock.Checked == true)
                num_LockScreen.Enabled = true;
            else
                num_LockScreen.Enabled = false;
        }

        private void num_NoofColOnCalndr_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }

        }

        private void num_NoofColOnCalndr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void num_LockScreen_KeyDown(object sender, KeyEventArgs e)
        {
            nonNoAutoLockTime = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNoAutoLockTime = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNoAutoLockTime = true;
            }
        }

        private void num_LockScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNoAutoLockTime == true)
            {
                e.Handled = true;
            }
        }

        private void num_NoofApptInaSlot_KeyDown(object sender, KeyEventArgs e)
        {
            nonNoOfApptInHour = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNoOfApptInHour = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNoOfApptInHour = true;
            }
        }

        private void num_NoofApptInaSlot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNoOfApptInHour == true)
            {
                e.Handled = true;
            }
        }

        private void chkGenerateOutboundMsg_CheckedChanged(object sender, EventArgs e)
        { //Added by Abhijeet on 20110927
          //code commented on 21-nov-2012
            //if (chkGenerateOutboundMsg.Checked == true)
            //{
            //    grpOutboundSettings.Enabled = true;
            //}
            //else
            //{
            //    grpOutboundSettings.Enabled = false;
            //    chkHL7.Checked = false;
            //    chkSendPatientDetails.Checked = false;
            //    chkSendAppointmentDetails.Checked = false;
            //}
            //code commented on 21-nov-2012 for making hl7outbound
        }

        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkEnableLocalPrinter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableLocalPrinter.Checked == false)
            {
                chkAddFooterService.Enabled = false;
                chkAddFooterService.Checked = false;
                cmbNoPagesSplit.Enabled = false;
                if (cmbNoPagesSplit.Items.Count > 0)
                {
                    cmbNoPagesSplit.SelectedIndex = 0;
                }
                cmbNoTemplatesJob.Enabled = false;
                if (cmbNoTemplatesJob.Items.Count > 1)
                {
                    cmbNoTemplatesJob.SelectedIndex = 1;
                }
                rbPrintWordDocPDF.Enabled = false;
                rbPrintWordDocEMF.Enabled = false;
                rbPrintSSRSReportPDF.Enabled = false;
                rbPrintSSRSReportEMF.Enabled = false;
                rbPrintClaimsPDF.Enabled = false;
                rbPrintClaimsEMF.Enabled = false;
                rbPrintImagesPNG.Enabled = false;
                rbPrintImagesEMF.Enabled = false;
                chkZipMetadata.Enabled = false;
                chkZipMetadata.Checked = false;
            }
            else
            {
                chkAddFooterService.Enabled = true;
                cmbNoPagesSplit.Enabled = true;
                cmbNoTemplatesJob.Enabled = true;
                rbPrintWordDocPDF.Enabled = true;
                rbPrintWordDocEMF.Enabled = true;
                rbPrintSSRSReportPDF.Enabled = true;
                rbPrintSSRSReportEMF.Enabled = true;
                rbPrintClaimsPDF.Enabled = true;
                rbPrintClaimsEMF.Enabled = true;
                rbPrintImagesPNG.Enabled = true;
                rbPrintImagesEMF.Enabled = true;
                chkZipMetadata.Enabled = true;
                chkZipMetadata.Checked = gloGlobal.gloTSPrint.UseZippedMetadata;
            }
            
        }

        private void rbPrintWordDocPDF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintWordDocPDF.Checked == true)
            {
                rbPrintWordDocPDF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintWordDocPDF.Font = gloGlobal.clsgloFont.gFont;
            }

        }

        private void rbPrintWordDocEMF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintWordDocEMF.Checked == true)
            {
                rbPrintWordDocEMF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintWordDocEMF.Font = gloGlobal.clsgloFont.gFont;
            }

        }

        private void rbPrintSSRSReportPDF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintSSRSReportPDF.Checked == true)
            {
                rbPrintSSRSReportPDF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintSSRSReportPDF.Font = gloGlobal.clsgloFont.gFont;
            }

        }

        private void rbPrintSSRSReportEMF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintSSRSReportEMF.Checked == true)
            {
                rbPrintSSRSReportEMF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintSSRSReportEMF.Font = gloGlobal.clsgloFont.gFont;
            }

        }

        private void rbPrintClaimsPDF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintClaimsPDF.Checked == true)
            {
                rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont;
            }
        }

        private void rbPrintClaimsEMF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintClaimsEMF.Checked == true)
            {
                rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont;
            }
        }

        private void rbPrintImagesPNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintImagesPNG.Checked == true)
            {
                rbPrintImagesPNG.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintImagesPNG.Font = gloGlobal.clsgloFont.gFont;
            }
        }

        private void rbPrintImagesEMF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintImagesEMF.Checked == true)
            {
                rbPrintImagesEMF.Font = gloGlobal.clsgloFont.gFont_BOLD;
            }
            else
            {
                rbPrintImagesEMF.Font = gloGlobal.clsgloFont.gFont;
            }
        }

        private void btnClearPatientContext_Click(object sender, EventArgs e)
        {
            ClearPatientContextSetting();
        }

        private void ClearPatientContextSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                string query = "Delete from Settings where sSettingsName ='SyncPatient' and nUserid=" + gloGlobal.gloPMGlobal.UserID + "";
                int Result = oDB.Execute_Query(query);
                if (Result > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.PatientAlert,
                        gloAuditTrail.ActivityType.Action,
                        "Patient context setting cleared for user " + gloGlobal.gloPMGlobal.UserName + " by user " + gloGlobal.gloPMGlobal.UserName + "",
                        0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    MessageBox.Show("User's patient context setting cleared successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.PatientAlert,
                        gloAuditTrail.ActivityType.Action,
                        "Error while clearing users patient context setting for user " + gloGlobal.gloPMGlobal.UserName + " by user " + gloGlobal.gloPMGlobal.UserName + "",
                        0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void chkZipMetadata_CheckedChanged(object sender, EventArgs e)
        {

        }
       

       
        

       
    }
}