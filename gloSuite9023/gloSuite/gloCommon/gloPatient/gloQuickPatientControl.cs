using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using gloAddress;
using gloPatient.Classes;
using System.Data.SqlClient;

namespace gloPatient
{
    public partial class gloQuickPatientControl : UserControl
    {

        #region "Constructors And Destructors"
        gloAddressControl oAddressControl;
      //  private string sGender = ""; //Dhruv -> Selecting Gender
        private bool _IsInternetFax = false;
        private bool _isAutogenerate = false;

        //Patient Portal
        public Boolean gblnPatientPortalSendActivationEmail = false;
        public Boolean gblnPatientPortalActivationEmailAlreadySent = false;
        public Boolean gblnPatientPortalEnabled = false;
        //Patient Portal
        //API
        public Boolean gblnPatientAPISendActivationEmail = false;
        public enum Gender 
        {
            Male =1,Female,Other
        };
        public gloQuickPatientControl()
        {
            InitializeComponent();
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            oPatientDemo = new PatientDemographics();
            _PatientInsurance = new PatientInsuranceOther();

            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            oPatientGuarantors = new PatientOtherContacts();
            oAccount = new Account();
            oPatientAccount = new PatientAccount();

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
            }
            else
                _UserID = 1;

            #endregion


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

        public gloQuickPatientControl(string Connectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = Connectionstring;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

            oPatientDemo = new PatientDemographics();
            _PatientInsurance = new PatientInsuranceOther();
            oPatientDemographicOtherInfo = new PatientDemographicOtherInfo();

            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            oPatientGuarantors = new PatientOtherContacts();
            oAccount = new Account();
            oPatientAccount = new PatientAccount();

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
            }
            else
                _UserID = 1;
            #endregion

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
        
        #region "Private And Public Variables"

        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;

        private Int64  _PatientID = 0;
        private Int64 _nProviderID = 0;
        private string _sProviderName = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private int _MoreLess = 1;
        private PatientDemographics oPatientDemo = null;
        //private gloListControl.gloListControl oListControl;
       // private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Insurance;
       //private bool _isInsurenceModified = false;
        PatientInsuranceOther _PatientInsurance = null;
        gloPatientInsuranceControl ogloPatientInsuranceControl = null;
        //20100503
        PatientDemographicOtherInfo oPatientDemographicOtherInfo = null;

        private string _alertDescription = "";

        //For Rx HuB 
        public string RxFirstName = "";
        public string RxLastName = "";
        public string RxMiddleName = "";
        public DateTime RxDOB = DateTime.MinValue;
        public string RxGender = "";
        public string RxAddressLine1 = "";
        public string RxAddressLine2 = "";
        public string RxPhone = "";
        public string RxCity = "";
        public string RxState = "";
        public string RxZip = "";
        public string RxFax = "";

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        private PatientOtherContacts oPatientGuarantors = null;
        private Account oAccount = null;
        private PatientAccount oPatientAccount = null;
        private Int64 _UserID = 1;

        #endregion "Private Variables"

        #region "Properties Procedures"

        public string DatabaseConnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public Int64 ProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public string ProviderName
        {
            get { return _sProviderName; }
            set { _sProviderName = value; }
        }


        public PatientDemographics PatientDemo
        {
            get { return oPatientDemo; }
            set { oPatientDemo = value; }
        }

        public int MoreLess
        {
            get { return _MoreLess; }
            set { _MoreLess = value; }
        }

        public PatientInsuranceOther PatientInsuranceDetails
        {
            get { return _PatientInsurance; }
            set { _PatientInsurance = value; }
        }

        public string AlertDescription
        {
            get { return _alertDescription; }
            set { _alertDescription = value; }
        }

        // 20100503 For adding Signature on File value 
        public PatientDemographicOtherInfo PatientDemoOtherInfo
        { 
            get {return oPatientDemographicOtherInfo;}
            set { oPatientDemographicOtherInfo = value; }
        }

        //Code added by SaiKrishna date(02-21-2011) for Patient Account Feature.
        public PatientOtherContacts PatientGuarantors
        {
            get { return oPatientGuarantors; }
            set { oPatientGuarantors = value; }
        }

        public Account Account
        {
            get { return oAccount; }
            set { oAccount = value; }
        }

        public PatientAccount PatientAccount
        {
            get { return oPatientAccount; }
            set { oPatientAccount = value; }
        }


        #endregion
        
        #region Events And Delegates

        public delegate void onbtnMoreLessClicked(object sender, EventArgs e);
        public event onbtnMoreLessClicked onbtnMoreLess_Click;

        #endregion
       
        private void gloQuickPatientControl_Load(object sender, EventArgs e)
        {
            // solving sales force case-GLO2010-0006466
           // txtPACode.ReadOnly = true;
            //End

            //DataTable dtPrefix = new DataTable();
            //gloPatient oPatient = new gloPatient(_databaseconnectionstring);

            //if (appSettings["PatientPrefix"] != null)
            //{
            //    if (appSettings["PatientPrefix"] != "")
            //    {
            //        txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
            //    }
            //}
            //if (txtPACode.Text.Trim() == "")
            //{
            //    dtPrefix = oPatient.GetPrefix();
            //    if (dtPrefix != null)
            //    {
            //        if (dtPrefix.Rows.Count > 0)
            //        {
            //            txtPatientPrefix.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);


            //        }
            //    }
            //    if (dtPrefix != null) { dtPrefix.Dispose(); }
            //    if (oPatient != null) { oPatient.Dispose(); }
            //}
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                mtxtPAFax.MaskType  = gloMaskControl.gloMaskType.Other;
            }
           
            FillProviders();
            //fillStates();
            GeneratePatientCode();
            Set_DefaultPatientGender();     
         
            //Load RxHub patient details
            txtPAFname.Text = RxFirstName;
            txtPALName.Text = RxLastName;
            txtPAMName.Text = RxMiddleName;
            if (RxDOB != DateTime.MinValue)
            {
                txtmPADOB.Text = RxDOB.ToString("MM/dd/yyyy");
            }

            if (RxGender.ToUpper() == "MALE")
            {
                rbGender1.Checked = true;
                rbGender2.Checked = false;
                rbGender3.Checked = false;
            }
            else if (RxGender.ToUpper() == "FEMALE")
            {
                rbGender1.Checked = false;
                rbGender2.Checked = true;
                rbGender3.Checked = false;
            }

            oAddressControl = new gloAddressControl(_databaseconnectionstring);
            
            //7022Items: Home Billing
            //Added to check setting from Settings table for USEAREACODEFORPATIENT if value =1(true) then show atra code textbox on patient registration form else not.
            #region "Setting an Area Code for Patient Address "

            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);

            object oUseAreaCode = new object();
            oSettings.GetSetting("USEAREACODEFORPATIENT", out oUseAreaCode);

            //7022Items: Home Billing
            //check for country only for US to disply area code.
            oAddressControl.UseAreaCodeForPatient = Convert.ToBoolean(Convert.ToInt16(oUseAreaCode));
            oAddressControl.SetAreaCode();

            oSettings.Dispose();
            oSettings = null;

            #endregion

            oAddressControl.Dock = DockStyle.Fill;
            pnlAddresControl.Controls.Add(oAddressControl);

            //txtPAAddress1.Text =  RxAddressLine1;
            //txtPAAddress2.Text =  RxAddressLine2;
            //mtxtPAPhone.Text = RxPhone;
            //txtPACity.Text = RxCity;
            //cmbPAState.Text = RxState;
            //txtPAZip.Text = RxZip;
            oAddressControl.txtAddress1.Text =  RxAddressLine1;
            oAddressControl.txtAddress2.Text =  RxAddressLine2;
            mtxtPAPhone.Text = RxPhone;
            oAddressControl.txtCity.Text = RxCity;
            oAddressControl.cmbState.Text = RxState;
            oAddressControl.txtZip.Text = RxZip;
            mtxtPAFax.Text = RxFax;

            //Patient Portal
            if (gblnPatientPortalEnabled)
            {
                pnlPortalInvitaitonEmail.Visible = true;
            }
            //Patient Portal
        }
        
        #region Methods to Fill Combo Boxes

     
        private void FillProviders()
        {
            try
            {
                DataTable dt;
                // Fill Providers in the Combo Box
                gloAppointmentBook.Books.Resource oProvider = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                dt = oProvider.GetProviders();
                DataRow r;
                r = dt.NewRow();
                r["nProviderID"] = 0;
                r["ProviderName"] = "";
                dt.Rows.InsertAt(r, 0);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmb_Providers.DataSource = dt.Copy();
                        cmb_Providers.ValueMember = dt.Columns["nProviderID"].ColumnName;
                        cmb_Providers.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                        cmb_Providers.Refresh();
                        //if(dt.Rows.Count > 1 )
                        //cmb_Providers.SelectedIndex = 0;
                    }

                }
                dt = null;
                oProvider.Dispose();

                //cmb_Providers.SelectedIndex = cmb_Providers.FindStringExact(_sProviderName);
                if (_nProviderID != 0)
                {
                    cmb_Providers.SelectedValue = _nProviderID;
                }
                else
                {
                    SetDefaultProvider();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SetDefaultProvider()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                Int64 nProviderID = 0;
                ogloSettings.GetSetting("PatientDefaultProvider", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    nProviderID = Convert.ToInt64(value);
                }
                value = null;

                if (nProviderID <= 0)
                {
                    _strSQL = " SELECT nProviderID AS ProviderID, " +
                                " (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                                " From Provider_MST WHERE Provider_MST.nClinicID = " + _ClinicID + " ";
                }
                else
                {
                    _strSQL = " SELECT nProviderID AS ProviderID, " +
                                " (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') + space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                                " From Provider_MST WHERE Provider_MST.nProviderID = " + nProviderID + " AND Provider_MST.nClinicID = " + _ClinicID + " ";
                }

                oDB.Retrive_Query(_strSQL, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    //txtPAProvider.Text = Convert.ToString(dtResult.Rows[0]["ProviderName"]);
                    if (nProviderID == 0)
                    {
                        cmb_Providers.SelectedIndex = 0;
                    }
                    else
                    {
                        cmb_Providers.SelectedValue = Convert.ToInt64(dtResult.Rows[0]["ProviderID"]);
                    }
                }
                else
                {
                    cmb_Providers.SelectedIndex = 1;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
                if (dtResult != null)
                {
                    dtResult.Dispose();
                    dtResult = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        //private void fillStates()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    oDB.Connect(false);
        //    try
        //    {
        //        DataTable dtStates = new DataTable();
        //        string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
        //        oDB.Retrive_Query(_sqlQuery, out dtStates);
        //        oDB.Disconnect();

        //        if (dtStates != null)
        //        {
        //            DataRow dr = dtStates.NewRow();
        //            dr["ST"] = "";
        //            dtStates.Rows.InsertAt(dr, 0);
        //            dtStates.AcceptChanges();

        //            //cmbPAState.DataSource = dtStates;
        //            //cmbPAState.DisplayMember = "ST";

        //            oAddressControl.cmbState.DataSource = dtStates;
        //           oAddressControl.cmbState.DisplayMember = "ST";
        //        }

        //        //if (_States != "")
        //        //{
        //        //    cmbPAState.Text = _States;
        //        //}



        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }

        //}
        private void AutoGenratePatientCode_Off()
        {
            SqlConnection objCon = new SqlConnection();
            objCon.ConnectionString = _databaseconnectionstring;
            SqlCommand objCmd = new SqlCommand();
            //Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "gsp_UpdateSettings";
            SqlParameter objParaSettingsName = new SqlParameter();
            SqlParameter objParaSettingsValue = new SqlParameter();

            SqlParameter objParaSettingsClinicID = new SqlParameter();
            SqlParameter objParaSettingsUserID = new SqlParameter();
            SqlParameter objParaSettingsUserClinicFlag = new SqlParameter();
            try
            {
                objCmd.Connection = objCon;

                objCon.Open();
                objCmd.Parameters.Clear();
                objParaSettingsName.ParameterName = "@SettingsName";
                objParaSettingsName.Value = "Auto-Generate Patient Code";
                objParaSettingsName.Direction = ParameterDirection.Input;
                objParaSettingsName.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaSettingsName);

                objParaSettingsValue.ParameterName = "@SettingsValue";
                objParaSettingsValue.Value = "0";
                objParaSettingsValue.Direction = ParameterDirection.Input;
                objParaSettingsValue.SqlDbType = SqlDbType.VarChar;

                objCmd.Parameters.Add(objParaSettingsValue);
                objParaSettingsClinicID.ParameterName = "@nClinicID";
                objParaSettingsClinicID.Value = 1;
                objParaSettingsClinicID.Direction = ParameterDirection.Input;
                objParaSettingsClinicID.SqlDbType = SqlDbType.BigInt;

                objCmd.Parameters.Add(objParaSettingsClinicID);


                objParaSettingsUserID.ParameterName = "@nUserID";
                objParaSettingsUserID.Value = 0;
                objParaSettingsUserID.Direction = ParameterDirection.Input;
                objParaSettingsUserID.SqlDbType = SqlDbType.BigInt;

                objCmd.Parameters.Add(objParaSettingsUserID);

                {
                    objParaSettingsUserClinicFlag.ParameterName = "@nUserClinicFlag";
                    objParaSettingsUserClinicFlag.Value = 1;
                    objParaSettingsUserClinicFlag.Direction = ParameterDirection.Input;
                    objParaSettingsUserClinicFlag.SqlDbType = SqlDbType.Int;
                }
                objCmd.Parameters.Add(objParaSettingsUserClinicFlag);
                //' End Add ClinicID, UserID,UserClinicFlag
                objCmd.ExecuteNonQuery();

                object oResult;
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("UseSitePrefix", out oResult);
                ogloSettings.Dispose();
                ogloSettings = null;
                Int32 _UseSitePrefix = 0;
                if (oResult != null && oResult.ToString() != "")
                {
                    _UseSitePrefix = Convert.ToInt32(oResult);
                }
                if (_UseSitePrefix != 0)
                {
                    if (appSettings["PatientPrefix"] != null)
                    {
                        if (appSettings["PatientPrefix"] != "")
                        {
                            txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                            SendKeys.Send("-");
                        }
                    }
                    if (txtPACode.Text.Trim() == "")
                    {


                        DataTable dtPrefix = new DataTable();
                        gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                        dtPrefix = oPatient.GetPrefix();
                        if (dtPrefix != null)
                        {
                            if (dtPrefix.Rows.Count > 0)
                            {
                                txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                SendKeys.Send("-");

                            }
                        }
                        if (dtPrefix != null) { dtPrefix.Dispose(); }
                        if (oPatient != null) { oPatient.Dispose(); }
                    }
                    txtPACode.Focus();
                }
                else
                {
                    txtPACode.Mask = "AAAAAAAAAAAAA";
                    txtPatientPrefix.Text = "";
                    txtPACode.Text = "";
                    txtPACode.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if ((objCon != null))
                {
                    objCon.Dispose();
                    objCon = null;
                }

                if ((objParaSettingsName  != null))
                {
                    objParaSettingsName = null;
                }

                if ((objParaSettingsValue  != null))
                {
                    objParaSettingsValue = null;
                }

                if ((objParaSettingsClinicID != null))
                {
                    objParaSettingsClinicID = null;
                }

                if ((objParaSettingsUserID != null))
                {
                    objParaSettingsUserID = null;
                }
                if ((objParaSettingsUserClinicFlag != null))
                {
                    objParaSettingsUserClinicFlag = null;
                }

                if ((objCmd != null))
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
            }
            
        }
        //Auto Generate Patient Code
        private void GeneratePatientCode()
        {
            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
            //***********************************************************************************************
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            object oResult;
            object oResultAllowEditablePatientCode;
            oDB.Connect(false);
            
            try
            {
                  ogloSettings.GetSetting("Auto-Generate Patient Code", out oResult);
                  ogloSettings.GetSetting("Allow-Editable Patient Code", out oResultAllowEditablePatientCode);
                    Int32 _AutoGenerate = 0;
                    Int32 _AllowEditableCode = 0;
                    if (oResult != null && oResult.ToString() != "" && oResultAllowEditablePatientCode != null && oResultAllowEditablePatientCode.ToString() != "")
                    {
                        _AutoGenerate = Convert.ToInt32(oResult);
                        _AllowEditableCode = Convert.ToInt32(oResultAllowEditablePatientCode);
                        if (_AutoGenerate != 0 && _AllowEditableCode == 0) //only autogenerate true
                        {
                            txtPACode.ReadOnly = true;
                        }
                        // Commented 20101228 By MaheshS: for 5073: SFCase# 0008153
                        // No need of this condition coz whenever allow Allow  ediatble  patient code setting is on user should allow to edit patient code even if patient code is auto generated
                        //else if (_AutoGenerate != 0 && _AllowEditableCode != 0) //both true
                        //{
                        //    txtPACode.ReadOnly = false ;
                        //}
                        else  //autogenerate false
                        {
                            txtPACode.ReadOnly = false;
                            // txtPACode.Enabled = true;
                        }
                        if (_AutoGenerate != 0)
                        {
                            //txtPACode.ReadOnly = true;
                            if (PatientID == 0)
                            {
                                if (_AllowEditableCode == 0)
                                {
                                    txtmPASSN.Select();
                                }
                                else
                                {
                                    txtPACode.Focus();
                                }
                            }
                            else
                            {
                                txtPACode.Focus();
                            }
                            //*****************************************************************************
                            txtPACode.Text = ogloPatient.GeneratePatientCode();
                            _isAutogenerate = true;
                            if (ogloPatient._result.Length > 13)
                            {
                                if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue with manual entry of patient code?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtPACode.ReadOnly = false;
                                    AutoGenratePatientCode_Off();
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                                    return;
                                }
                                else
                                {
                                    txtPACode.Mask = "AAAAAAAAAAAAAA";
                                    txtPACode.Text = ogloPatient._result;
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                                }
                            }
                        }
                    }
               
               //****************************************************************************************
                //ogloSettings.GetSetting("UseSitePrefix", out oResult);
                //Int32 _UseSitePrefix = 0;
                //if (oResult != null && oResult.ToString() != "")
                //{
                //    _UseSitePrefix = Convert.ToInt32(oResult);
                //}
                //if (_UseSitePrefix == 0)
                //{
                //    txtPACode.Mask = "AAAAAAAAAAAAA";
                //    txtPatientPrefix.Text  ="";
                //}

                    ogloSettings.GetSetting("UseSitePrefix", out oResult);
                    Int32 _UseSitePrefix = 0;
                    if (oResult != null && oResult.ToString() != "")
                    {
                        _UseSitePrefix = Convert.ToInt32(oResult);
                    }

                    if (_UseSitePrefix != 0)
                    {
                        if (_AutoGenerate == 0)
                        {
                            if (appSettings["PatientPrefix"] != null)
                            {
                                if (appSettings["PatientPrefix"] != "")
                                {
                                    txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                                    SendKeys.Send("-");
                                }
                            }
                            if (txtPACode.Text.Trim() == "")
                            {


                                DataTable dtPrefix = null;
                                gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                                dtPrefix = oPatient.GetPrefix();
                                if (dtPrefix != null)
                                {
                                    if (dtPrefix.Rows.Count > 0)
                                    {
                                        txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                        SendKeys.Send("-");

                                    }
                                }
                                if (dtPrefix != null) { dtPrefix.Dispose(); }
                                if (oPatient != null) { oPatient.Dispose(); }
                            }
                        }
                    }
                //***************************************************************************************************
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }
                if (ogloPatient != null) { ogloPatient.Dispose(); }
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }
        }

        #endregion

        //get demographic data
        public bool GetData()
        {
            try
            {
                if (ValidateData())
                {
                    //Patient Code,Name
                    oPatientDemo.PatientCode = txtPACode.Text.Trim();
                    oPatientDemo.PatientFirstName = txtPAFname.Text.Trim();
                    oPatientDemo.PatientMiddleName = txtPAMName.Text.Trim();
                    oPatientDemo.PatientLastName = txtPALName.Text.Trim();
                    oPatientDemo.PatientPrefix = txtPatientPrefix.Text.Trim();   
                    //SSN
                    //if (txtmPASSN.MaskCompleted)
                    //    oPatientDemo.PatientSSN = txtmPASSN.Text;
                    //else
                    //    oPatientDemo.PatientSSN = "";
                    if (txtmPASSN.IsValidated==true)
                        oPatientDemo.PatientSSN = txtmPASSN.Text;
                    else
                        oPatientDemo.PatientSSN = "";

                    //DOB
                    oPatientDemo.PatientDOB = Convert.ToDateTime(txtmPADOB.Text.Trim());

                    
                    //gender
                    if (rbGender1.Checked)
                    {

                        oPatientDemo.PatientGender = "Male";
                    }
                    if (rbGender2.Checked)
                    {
                        oPatientDemo.PatientGender = "Female";
                    }
                    if (rbGender3.Checked)
                    {
                        oPatientDemo.PatientGender = "Other";
                    }
                                 
                     //Address Details
                    //oPatientDemo.PatientAddress2 = txtPAAddress2.Text.Trim();
                    //oPatientDemo.PatientCity = txtPACity.Text.Trim();

                    oPatientDemo.PatientAddress1 = oAddressControl.txtAddress1.Text.Trim();
                    oPatientDemo.PatientAddress2 =  oAddressControl.txtAddress2.Text.Trim();
                    oPatientDemo.PatientCity = oAddressControl.txtCity.Text.Trim();
                    //20100503
                   
                    // Problem : 00000183
                    // Reason : changed the If condition 
                    // Description: change if condition if (pnl_AddressDetails.Visible == true)
                    if (oAddressControl.cmbCountry.SelectedIndex != -1)
                    {
                        //oPatientDemo.PatientState = cmbPAState.Text.ToString();
                        //oPatientDemo.PatientCountry = cmbPACountry.Text.Trim();
                        oPatientDemo.PatientState = oAddressControl.cmbState.Text.ToString();
                        oPatientDemo.PatientCountry = oAddressControl.cmbCountry.Text.Trim();
                    }
                    else
                    {
                        oPatientDemo.PatientState = "";
                        oPatientDemo.PatientCountry = "";
                    }
                    //oPatientDemo.PatientZip = txtPAZip.Text.Trim();
                    //oPatientDemo.PatientCounty = txtPACountry.Text.Trim();
                    oPatientDemo.PatientZip = oAddressControl.txtZip.Text.Trim();
                    oPatientDemo.PatientCounty = oAddressControl.txtCounty.Text.Trim();
                    //7022Items: Home Billing
                    //Added to save area code for patient
                    oPatientDemo.AreaCode = oAddressControl.txtAreaCode.Text.Trim();

                    //Contact
                    oPatientDemo.PatientMobile = mtxtPAMobile.Text.Trim();
                    oPatientDemo.PatientPhone = mtxtPAPhone.Text.Trim();
                    oPatientDemo.PatientEmail = txtPAEmail.Text.Trim();

                    //Patient Portal
                    if (cbSendPatientPortalActivationEmail.Visible)
                    {
                        if (cbSendPatientPortalActivationEmail.Checked)
                        {
                            gblnPatientPortalSendActivationEmail = true;
                        }
                        else
                        {
                            gblnPatientPortalSendActivationEmail = false;
                        }
                    }
                    else
                    {
                        gblnPatientPortalSendActivationEmail = false;
                        gblnPatientPortalActivationEmailAlreadySent = false;
                    }
                    //Patient Portal
                    //API
                    if (cbSendAPIInvitation.Visible)
                    {
                        if (cbSendAPIInvitation.Checked)
                        {
                            gblnPatientAPISendActivationEmail = true;
                        }
                        else
                        {
                            gblnPatientAPISendActivationEmail = false;
                        }
                    }
                    else
                    {
                        gblnPatientAPISendActivationEmail = false;
                        //ASK gblnPatientPortalActivationEmailAlreadySent = false;
                    }
                    //API
                    //oPatientDemo.PatientFax = txtPAFax.Text.Trim();
                    oPatientDemo.PatientFax =mtxtPAFax.Text.Trim();
                    oPatientDemo.PatientID = _PatientID;

                    if (cmb_Providers.SelectedIndex != -1)
                    {
                        oPatientDemo.ProvideName = cmb_Providers.Text.ToString();
                        oPatientDemo.PatientProviderID = Convert.ToInt64(cmb_Providers.SelectedValue);
                    }
                    else 
                    {
                        oPatientDemo.ProvideName = "";
                        oPatientDemo.PatientProviderID =0;
                    }

                    //Code added on 20090506 By - Sagar Ghodke
                    //Code added to implement Patient Alert functionality 

                    this.AlertDescription = txtAlert.Text.Trim();

                    oPatientDemographicOtherInfo.SOF = true;

                    //End Code add 20090506,Sagar Ghodke

                    //Code added by SaiKrishna date(02-21-2011) for Patient Account Feature.
                    GetAccountDataForQuickPatient();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        //validate demographic data
        private bool ValidateData()
        {


            if (txtPACode.Text.Trim() == "")
            {
                MessageBox.Show("Enter the patient code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPACode.Focus();
                return false;
            }

            //7022Items: Home Billing
            //Added validation if area code enter and it's length >0 and <4 then this message is shown on save&cls.
            if (oAddressControl.txtAreaCode.TextLength > 0 && oAddressControl.txtAreaCode.TextLength < 4)
            {
                if (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    oAddressControl.txtAreaCode.Select();
                    oAddressControl.txtAreaCode.Focus();
                    return false;
                }
            }

            //gloPatient oPatientTrans = new gloPatient(_databaseconnectionstring);
            //if (_PatientID == 0)
            //{
                //Commented By Pramod Nair --- Buse Patient ID Generation is automatically taken Care in Stored Procedures
                //if (oPatientTrans.ChkExistPatientID(txtPACode.Text.Trim()) == true)
                //{
                //    MessageBox.Show("Patient code is assigned to another patient.  Please select a unique patient code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPACode.Focus();
                //    return false;
                //}

            //}
            //else
            //{
                //Commented By Pramod Nair --- Buse Patient ID Generation is automatically taken Care in Stored Procedures
                //if (oPatientTrans.ChkExistPatientIDUpdate(txtPACode.Text.Trim(), _PatientID) == true)
                //{
                //    MessageBox.Show("Patient code is assigned to another patient.  Please select a unique patient code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPACode.Focus();
                //    return false;
                //}
            //}
            //oPatientTrans.Dispose();

            //Check if SSN is completed
            //txtmPASSN.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (txtmPASSN.Text.Length > 0 && txtmPASSN.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a valid SSN number.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtmPASSN.Focus();
            //    return false;
            //}
            if (txtmPASSN.IsValidated == false)
            {
                return false;
            }
            if (mtxtPAFax.IsValidated == false)
            {
                return false;
            }
            //******first name
            if (txtPAFname.Text.Trim() == "")
            {
                MessageBox.Show("Enter a first name for the patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAFname.Focus();
                return false;
            }

            //******Last name
            if (txtPALName.Text.Trim() == "")
            {
                MessageBox.Show("Enter a last name for the patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPALName.Focus();
                return false;
            }
            //date of birth            
            if (txtmPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmPADOB.Focus();
                return false;
            }
            //Provider
            //Added on 20091128:Mayuri-To fix issue:#485
            if (cmb_Providers.Text == "")
            {
                MessageBox.Show("Select a provider for the patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_Providers.Focus();
                return false;
            }

            //Validate DOB
            try
            {
                txtmPADOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals; 
                Convert.ToDateTime(txtmPADOB.Text.Trim()).ToString("MM/dd/yyyy");
                long year = Convert.ToInt64(txtmPADOB.Text.Trim().Substring(6, 4));
                if (1754 > year || year > 9999)
                    throw new Exception();
                if (Convert.ToDateTime(txtmPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(txtmPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(txtmPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                {
                    MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmPADOB.Focus();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Enter a valid date of birth.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmPADOB.Focus();
                return false;
            }


            try
            {
                txtmPADOB.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                DateTime dtDOB = Convert.ToDateTime(txtmPADOB.Text.Trim());
                if (dtDOB.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("Enter a valid date of birth.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmPADOB.Focus();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Enter a valid date of birth.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmPADOB.Focus();
                return false;
            }


            //Incomplete Phone Numbers
            //mtxtPAPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mtxtPAPhone.Text.Length > 0 && mtxtPAPhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtPAPhone.Focus();
            //    return false;
            //}
            if (mtxtPAPhone.IsValidated == false)
            {
                return false;
            }

            //Incomplete 
            //mtxtPAMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mtxtPAMobile.Text.Length > 0 && mtxtPAMobile.MaskCompleted == false)
            //{

            //    MessageBox.Show("Please enter a 10 digit number for mobile.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtPAMobile.Focus();
            //    return false;

            //}
            if (mtxtPAMobile.IsValidated == false)
            {
                return false;
            }
            //Code Added by Mayuri:20091127-Calendar>New Patient>window gets closed without any error message on clicking ‘save And Close’ button even after email is incomplete.
            if (CheckEmailAddress(txtPAEmail.Text) == false)
            {
                MessageBox.Show("Enter a valid email ID.", _messageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAEmail.Focus();
                return false;
            }

            //Patient Portal
            if (cbSendPatientPortalActivationEmail.Visible)
            {
                if (cbSendPatientPortalActivationEmail.Checked)
                {
                    if ((txtPAEmail.Text.Trim() == "") && (oAddressControl.txtZip.Text.Trim() == ""))
                    {
                        MessageBox.Show("You must enter a valid Email address and a Zip Code to send Patient Portal Invitation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    else if (txtPAEmail.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a valid Email address to send Patient Portal Invitation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    else if (oAddressControl.txtZip.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a Zip Code to send Patient Portal Invitation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oAddressControl.txtZip.Focus();
                        return false;
                    }
                }
            }
            //Patient Portal
            //API

            if (cbSendAPIInvitation.Visible)
            {
                if (cbSendAPIInvitation.Checked)
                {

                    if (txtPAEmail.Text.Trim() == "")
                    {
                        MessageBox.Show("You must enter a valid Email address to send Patient API Invitation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPAEmail.Focus();
                        return false;
                    }
                    ////else if (oAddresscontrol.txtZip.Text.Trim() == "")
                    ////{
                    ////    MessageBox.Show("You must enter a Zip Code to send Patient Portal Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    oAddresscontrol.txtZip.Focus();
                    ////    return false;
                    ////}
                }
            }

            //API
            //End code Added by Mayuri:20091127
           //Added on 20101210
            //for new patient, Give Alert when patient name and DOB already exists ...

            //Code added by SaiKrishna  2011-06-27(yyyy-mm-dd) for address validation.
            //if (oAddressControl.txtAddress1.Text.Trim() == "" && btnMoreLess.Text == "<< Less Details")
            //{
            //    MessageBox.Show("Enter address for patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddressControl.txtAddress1.Focus();
            //    return false;
            //}
            //if (oAddressControl.txtCity.Text.Trim() == "" && btnMoreLess.Text == "<< Less Details")
            //{
            //    MessageBox.Show("Enter city for patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddressControl.txtCity.Focus();
            //    return false;
            //}
            //if (oAddressControl.cmbState.Text.Trim() == "" && btnMoreLess.Text == "<< Less Details")
            //{
            //    MessageBox.Show("Enter state for patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddressControl.cmbState.Focus();
            //    return false;
            //}
            //if (oAddressControl.txtZip.Text.Trim() == "" && btnMoreLess.Text == "<< Less Details")
            //{
            //    MessageBox.Show("Enter zip for patient.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddressControl.txtZip.Focus();
            //    return false;
            //}


            gloPatient ogloDPatient = new gloPatient(_databaseconnectionstring);



            Boolean _IspatientExists = ogloDPatient.IsPatientExists(txtPAFname.Text.Trim(), txtPALName.Text.Trim(), Convert.ToDateTime(txtmPADOB.Text.Trim()), _PatientID);
            ogloDPatient.Dispose();
            ogloDPatient = null;

            if (_IspatientExists == true)
            {
                string sMessage = "";
                if (_PatientID == 0)
                    sMessage = "Patient with same name and date of birth already exists. Do you want to register as a new patient?";
                 else
                    sMessage = "Patient with same name and date of birth already exists. Do you want to modify patient with same name?";

                DialogResult oDialogResult = DialogResult.None;

                oDialogResult = MessageBox.Show(sMessage, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oDialogResult != DialogResult.Yes)
                {
                    txtPAFname.Focus();
                    return false;
                }

            }
            //End code Added on 20101210
            object oResult;
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            ogloSettings.GetSetting("UseSitePrefix", out oResult);
            ogloSettings.Dispose();
            ogloSettings = null;
            Int32 _UseSitePrefix = 0;
            if (oResult != null && oResult.ToString() != "")
            {
                _UseSitePrefix = Convert.ToInt32(oResult);
            }
            if (_UseSitePrefix != 0)
            {
                if (txtPACode.Text.Substring(3).Trim().ToString() == "9999999999")
                {
                    if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        txtPACode.Focus();
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                        return false;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                    }
                }
            }
            else
            {
                if (txtPACode.Text.Trim().ToString() == "9999999999999")
                {
                    if (MessageBox.Show("You have entered the maximum possible Patient Code. No more Patient Codes can be auto generated.  In order to register additional patients, auto generation of patient codes must be turned off in the gloEMR Admin. Do you want to continue?.", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        txtPACode.Focus();
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is now made turn off as no more Patient Codes can be auto generated, continue with manual entry.", gloAuditTrail.ActivityOutCome.Success);
                        return false;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.None, "Patient Code auto genration is still turn on even no more Patient Codes can be auto generated, now patient code will be differenet that what it is appearing on new patient registration screen.", gloAuditTrail.ActivityOutCome.Success);
                    }

                }

            }
            //if (txtPACode.ReadOnly == false )
            //{
            gloPatient oPatientTrans = new gloPatient(_databaseconnectionstring);
            if (oPatientTrans != null)
            {
                if (oPatientTrans.ChkExistPatientIDUpdate(txtPACode.Text.Trim(), PatientID) == true)
                {
                    oPatientTrans.Dispose();
                    oPatientTrans = null;
                    if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                        txtPACode.Text = ogloPatient.GeneratePatientCode();
                        if (ogloPatient._UseSitePrefix != 0)
                        {
                            if (txtPACode.Text.Length <= 10)
                            { txtPACode.Mask = "AAA-AAAAAAAAAA"; }
                            else
                            { txtPACode.Mask = "AAA-AAAAAAAAAAA"; }
                            txtPatientPrefix.Text = txtPACode.Text.Substring(0, 3);
                        }
                        else
                        {
                            txtPACode.Mask = "AAAAAAAAAAAAA";
                            txtPatientPrefix.Text = "";

                        }
                        ogloPatient.Dispose();
                        ogloPatient = null;

                    }

                    //MessageBox.Show("Patient code is assigned to another patient. Select a unique patient code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPACode.Focus();
                    return false;
                    //}
                    //bool _IsExists = oPatient.IsPatientCodeExists(txtPACode.Text.Trim().ToString());
                    //oPatient.Dispose();
                    //if (_IsExists)
                    //{
                    //    MessageBox.Show("Patient code is assigned to another patient.  Please select a unique patient code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtPACode.Focus();
                    //    return false;
                    //}

                }
                else
                {
                    oPatientTrans.Dispose();
                    oPatientTrans = null;
                }
            }
            #region "Check special Character"

            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z\b]");
            if (regex.IsMatch(txtPAFname.Text.Trim()) || regex.IsMatch(txtPAMName.Text.Trim()) || regex.IsMatch(txtPALName.Text.Trim()))
            {
                if ((MessageBox.Show("Patient name contains special/numeric character(s) which may cause billing rejection.\n Continue Save?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) == DialogResult.No)
                {
                    if (regex.IsMatch(txtPAFname.Text.Trim())) { txtPAFname.Select(); return false; }
                    if (regex.IsMatch(txtPAMName.Text.Trim())) { txtPAMName.Select(); return false; }
                    if (regex.IsMatch(txtPALName.Text.Trim())) { txtPALName.Select(); return false; }
                }
            }

            #endregion "Check special Character"
            return true;
        }
        #region "Validating the PatientCode - dhruv 20100720"

        private bool ValidateDescription(string PatientCode, Int64 PatientID)
        {
            bool _Result = false;
            SqlConnection objConn = null;
            SqlCommand objCmd = null;
            SqlParameter objParam = null;
            try
            {
                objConn = new SqlConnection(_databaseconnectionstring);
                if (objConn != null)
                {
                    objCmd = new SqlCommand("gsp_checkPatient", objConn);
                    if (objCmd != null)
                    {
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objParam = new SqlParameter();
                        if (objParam != null)
                        {
                            objParam = objCmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar);
                            objParam.Direction = ParameterDirection.Input;
                            objParam.Value = PatientCode;
                            //---------------------------
                            objParam = objCmd.Parameters.Add("@nPatientId", SqlDbType.BigInt);
                            objParam.Direction = ParameterDirection.Input;
                            objParam.Value = PatientID;
                            if (objConn.State == ConnectionState.Closed)
                            {
                                objConn.Open();
                            }
                            if (objConn.State == ConnectionState.Open)
                            {
                                object Count = objCmd.ExecuteScalar();
                                if ((int)Count > 0)
                                {
                                    _Result = true;
                                }
                                else
                                {
                                    _Result = false;

                                }
                                objConn.Close();
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                string _errorMessage = "Connection not established succefully" + Ex.ToString();

                if (objConn != null)
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                    objConn.Dispose();
                    objConn = null;
                }
                if (objCmd != null)
                {
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (objParam != null)
                {
                    objParam = null;
                }

                _Result = false;
            }
            finally
            {
                if (objConn != null)
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                    objConn.Dispose();
                    objConn = null;
                }
                if (objCmd != null)
                {
                    objCmd.Dispose();
                    objCmd = null;
                }
                if (objParam != null)
                {
                    objParam = null;
                }
            }
            return _Result;
        }
        #endregion

       
      
        private void txtPACode_KeyDown(object sender, KeyEventArgs e)
        {
            // txtPACode.Text = txtPACode.Text.Trim();
            if ((txtPACode.Mask == "AAA-AAAAAAAAAA"))
            {
                if (txtPACode.SelectionStart <= 4)
                {
                    e.Handled = true;
                    txtPACode.SelectionStart = 4;
                }

                if (((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete)) & (txtPACode.SelectionStart < 4))
                {
                    e.Handled = true;
                    txtPACode.SelectionStart = 4;
                }
            }


        }
        //added code to check the space between prefix and code
        private void txtPACode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //txtPACode.SelectionStart = txtPACode.Text.Trim().Length+1;

                //if (char.IsWhiteSpace(e.KeyChar) == true)
                if (e.KeyChar == Convert.ToChar(32))
                {
                    //Dont Allow space 
                    //if (!string.IsNullOrEmpty(txtPACode.Text))
                    {


                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void Set_DefaultPatientGender()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {

                ogloSettings.GetSetting("DefaultPatientGender", out value);
                if (value != null && Convert.ToString(value) != "")
                {

                    if (Convert.ToString(value) == "Male")
                    {
                        rbGender1.Checked = true;
                    }

                    if (Convert.ToString(value) == "Female")
                    {
                        rbGender2.Checked = true;
                    }

                    if (Convert.ToString(value) == "Other")
                    {
                        rbGender3.Checked = true;
                    }

                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }
        }

        //To fill the City,State,County according to zip Code
        //private void txtPAZip_Leave(object sender, EventArgs e)
        //{
        //    if (txtPAZip.Text.Trim() != "" && Regex.IsMatch(txtPAZip.Text.Trim(), @"^[0-9]+$") == true)
        //    {
        //        DataTable dt = new System.Data.DataTable();
        //        gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        try
        //        {
        //            oDb.Connect(false);
        //            string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtPAZip.Text.Trim() + "'";
        //            oDb.Retrive_Query(qry, out dt);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
                       
        //                cmbPAState.Text = Convert.ToString(dt.Rows[0]["ST"].ToString());
        //                if (txtPACity.Text.Trim() == "")
        //                    txtPACity.Text = Convert.ToString(dt.Rows[0]["City"]);
        //                txtPACountry.Text = Convert.ToString(dt.Rows[0]["County"].ToString());
        //                cmbPACountry.Text = "US"; 
        //            }

        //        }
        //        catch (gloDatabaseLayer.DBException ex)
        //        {
        //            ex.ERROR_Log(ex.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //        }
        //        finally
        //        {
        //            dt.Dispose();
        //            oDb.Disconnect();
        //            oDb.Dispose();
        //        }
        //    }
        //}
       
        private void btnMoreLess_Click(object sender, EventArgs e)
        {
            
            if (pnl_AddressDetails.Visible == false)
            {
                //this.Size = new Size(504, 479);
                this.Size = new Size(698, 650);
                pnl_AddressDetails.Visible = true;
                btnMoreLess.Text = "<< Less Details";
                MoreLess = 2;
            }
            else
            {
                this.Size = new Size(698, 287);
                pnl_AddressDetails.Visible = false;
                btnMoreLess.Text = "More Details >>";
              
                MoreLess = 1;
            }
           
            onbtnMoreLess_Click(sender, e);
        }
      
        private void btnInsurInfo_Click_1(object sender, EventArgs e)
        {
            MoreLess = 3;
            onbtnMoreLess_Click(sender, e);
            try
            {
                if (ogloPatientInsuranceControl != null)
                {
                    try
                    {
                        ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                        ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

                    }
                    catch
                    {
                    }
                    ogloPatientInsuranceControl.Dispose();
                    ogloPatientInsuranceControl = null;
                }
                ogloPatientInsuranceControl = new gloPatientInsuranceControl(_databaseconnectionstring);
                ogloPatientInsuranceControl.InsuranceOtherDetails = this.PatientInsuranceDetails;
                ogloPatientInsuranceControl.onInsuranceSave_Clicked += new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                ogloPatientInsuranceControl.onInsuranceClose_Clicked += new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);
                //ogloPatientInsuranceControl.PatientName = txtPAFname.Text.Trim() + " " + txtPALName.Text.Trim();
                ogloPatientInsuranceControl.PatientFName = txtPAFname.Text.Trim();
                ogloPatientInsuranceControl.PatientMName = txtPAMName.Text.Trim();
                ogloPatientInsuranceControl.PatientLName = txtPALName.Text.Trim();
                ogloPatientInsuranceControl.PatientDOB = txtmPADOB.Text.Trim();
                ogloPatientInsuranceControl.PatientPhone = mtxtPAPhone.Text.Trim();
                ogloPatientInsuranceControl.PatientAddressLine1 = oAddressControl. txtAddress1.Text.Trim();
                ogloPatientInsuranceControl.PatientAddressLine2 = oAddressControl.txtAddress2.Text.Trim();
                ogloPatientInsuranceControl.PatientCity = oAddressControl.txtCity.Text.Trim();
                ogloPatientInsuranceControl.PatientCountry = oAddressControl.cmbCountry.Text.Trim();
                ogloPatientInsuranceControl.PatientCounty = oAddressControl.txtCounty .Text.Trim();
                ogloPatientInsuranceControl.PatientZip = oAddressControl.txtZip.Text.Trim();
                ogloPatientInsuranceControl.PatientState = oAddressControl.cmbState.Text.Trim();
                #region "Dhruv 20100622 ->Setting Gender while it is selected"
                if (rbGender1.Checked == true)
                    ogloPatientInsuranceControl.PatientGender = Gender.Male.ToString();
                else if (rbGender2.Checked == true)
                    ogloPatientInsuranceControl.PatientGender = Gender.Female.ToString();
                else if (rbGender3.Checked == true)
                    ogloPatientInsuranceControl.PatientGender = Gender.Other.ToString();
                else
                    ogloPatientInsuranceControl.PatientGender = Gender.Other.ToString();
                #endregion "Dhruv 20100622 ->Setting Gender while it is selected"
                         ogloPatientInsuranceControl.Dock = DockStyle.Fill;
                this.Controls.Add(ogloPatientInsuranceControl);
                ogloPatientInsuranceControl.BringToFront();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void ogloPatientInsuranceControl_onInsuranceSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                MoreLess = 2;
                onbtnMoreLess_Click(sender, e);
                _PatientInsurance = ogloPatientInsuranceControl.InsuranceOtherDetails;
                cmbGenInfoInsurance.Items.Clear();
                for (int i = 0; i < _PatientInsurance.InsurancesDetails.Count; i++)
                {
                    cmbGenInfoInsurance.Items.Add(_PatientInsurance.InsurancesDetails[i].InsuranceName);
                    cmbGenInfoInsurance.SelectedIndex = 0;
                }
                //_isInsurenceModified = true;
                this.Controls.Remove(ogloPatientInsuranceControl);
                if (ogloPatientInsuranceControl != null)
                {
                    try
                    {
                        ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                        ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

                    }
                    catch
                    {
                    }
                    
                }    
            }
            catch (Exception ex )
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

        //Close Insurence control
        private void ogloPatientInsuranceControl_onInsuranceClose_Clicked(object sender, EventArgs e)
        {
            this.Size = new Size(698, 650);
            MoreLess = 2;
            onbtnMoreLess_Click(sender, e);
            this.Controls.Remove(ogloPatientInsuranceControl);
            if (ogloPatientInsuranceControl != null)
            {
                try
                {
                    ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                    ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

                }
                catch
                {
                }
    
            }

        }
        private void btnClrInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                cmbGenInfoInsurance.Items.Clear();
            }
            catch (Exception ex )
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #region "Mouse Hover & Leave Events"
        //event to change buttons color on MouseOver 
        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        //event to change buttons color on MouseLeave 
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseHover(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((ToolStripButton)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = null;
        }
        #endregion

       
        #region "Email Address Validation"

        public bool CheckEmailAddress(string input)
        {
            bool response = false;
            if (Regex.IsMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") || input.Trim() == "")
            {
                response = true;
            }
            else
            {
                response = false;
            }
            return response;
        }

        private void txtPAEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtPAEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion        

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void rbGender1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGender1.Checked == true)
            {
                rbGender1.Font = gloGlobal.clsgloFont.gFont_BOLD ;// new Font("Tahoma", 9, FontStyle.Bold);
              
            }
            else
            {
                rbGender1.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
              
            }

        }

        private void rbGender2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGender2.Checked == true)
            {
                rbGender2.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
               
            }
            else
            {
                rbGender2.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
               
            }

        }

        private void rbGender3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGender3.Checked == true)
            {
                rbGender3.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
               
            }
            else
            {
                rbGender3.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
               
            }

        }

        private void txtPACode_KeyUp(object sender, KeyEventArgs e)
        {
            txtPACode.Text = txtPACode.Text.Replace(" ", "");
        }

        private void txtPACode_Validating(object sender, CancelEventArgs e)
        {
            {
                if (_isAutogenerate == false || txtPACode.ReadOnly == false)
                {
                    string strtxtPACode = txtPACode.Text.Trim();
                    if (strtxtPACode != string.Empty)
                    {
                        //if (_IsSaveAsCopy == true)
                        //{
                        //    _PatientId = 0;
                        //}
                        if (ValidateDescription(strtxtPACode, PatientID) == true)
                        {

                            if (MessageBox.Show("Duplicate patient code, do you want to generate new patient code?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                                gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);


                                txtPACode.Text = ogloPatient.GeneratePatientCode();
                                if (ogloPatient._UseSitePrefix != 0)
                                {
                                    if (txtPACode.Text.Length <= 10)
                                    { txtPACode.Mask = "AAA-AAAAAAAAAA"; }
                                    else
                                    { txtPACode.Mask = "AAA-AAAAAAAAAAA"; }
                                    txtPatientPrefix.Text = txtPACode.Text.Substring(0, 3);
                                }
                                else
                                {
                                    txtPACode.Mask = "AAAAAAAAAAAAA";
                                    txtPatientPrefix.Text = "";

                                }
                                ogloPatient.Dispose();
                                ogloPatient = null;
                            }

                            txtPACode.Focus();

                            //_IsValidated = false;


                        }
                        //if (ValidateDescription_Excluding_sufix(strtxtPACode, _PatientId) == true)
                        //{
                        //    MessageBox.Show("Same patient code is exist with given sufix", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtPACode.Focus();

                        //}
                    }
                }
            }
        }

        #region "Patient Account Feature related Methods"

        /// <summary>
        /// Method to assign Account data for QuickPatient.
        /// Create guarantor object with same as patient,assign sameas patient guarantor to account object. 
        /// </summary>
        private void GetAccountDataForQuickPatient()
        {
            try
            {
                //create guarantor object with same as patient and assign sameas patient guarantor to account object.
                PatientOtherContact oGuarantor = new PatientOtherContact();
                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag();
                oGuarantor.PatientContactID = 0;
                oGuarantor.GuarantorAsPatientID = _PatientID;
                oGuarantor.IsActive = true;
                oGuarantor.FirstName = txtPAFname.Text.Trim();
                oGuarantor.MiddleName = txtPAMName.Text.Trim();
                oGuarantor.LastName = txtPALName.Text.Trim();
                if (txtmPADOB.MaskCompleted == true)
                { oGuarantor.DOB = Convert.ToDateTime(txtmPADOB.Text); }

                if (txtmPASSN.IsValidated == true)
                    oGuarantor.SSN = txtmPASSN.Text.Trim();
                else
                    oGuarantor.SSN = "";

                if (rbGender1.Checked)
                {
                    oGuarantor.Gender = "Male";
                }
                if (rbGender2.Checked)
                {
                    oGuarantor.Gender = "Female";
                }
                if (rbGender3.Checked)
                {
                    oGuarantor.Gender = "Other";
                }

                oGuarantor.AddressLine1 = oAddressControl.txtAddress1.Text.Trim();
                oGuarantor.AddressLine2 = oAddressControl.txtAddress2.Text.Trim();
                oGuarantor.City = oAddressControl.txtCity.Text.Trim();
                oGuarantor.County = oAddressControl.txtCounty.Text.Trim();
                oGuarantor.Zip = oAddressControl.txtZip.Text.Trim();
                oGuarantor.State = oAddressControl.cmbState.Text.Trim();
                oGuarantor.Country = oAddressControl.cmbCountry.Text;

                oGuarantor.Relation = "Self";
                oGuarantor.Phone = mtxtPAPhone.Text.Trim();

                if (mtxtPAMobile.IsValidated == true) { oGuarantor.Mobile = mtxtPAMobile.Text; }
                oGuarantor.Email = txtPAEmail.Text.Trim();
                oGuarantor.Fax = txtPAFax.Text.Trim();
                oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;
                //Represents Personal Guarantor
                oGuarantor.GurantorType = GuarantorType.Personal;
                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                oGuarantor.IsAccountGuarantor = true;
                oPatientGuarantors.Add(oGuarantor);

                //fill account object
                oAccount.PAccountID = 0;
                //Indicates new account
                oAccount.IsExistingAccount = false;
                oAccount.AccountNo = oPatientDemo.PatientCode;
                oAccount.AccountDesc = "";
                oAccount.SentToCollection = false;
                oAccount.ExcludeStatement = false;
                oAccount.AccountClosedDate = DateTime.MinValue;
                oAccount.RecordDate = DateTime.Now;
                oAccount.IsAccountFeatureEnabled = false;
                oAccount.FirstName = txtPAFname.Text.Trim();
                oAccount.LastName = txtPALName.Text.Trim();
                oAccount.MiddleName = txtPAMName.Text.Trim();
                oAccount.AddressLine1 = oAddressControl.txtAddress1.Text.Trim();
                oAccount.AddressLine2 = oAddressControl.txtAddress2.Text.Trim();
                oAccount.Active = true;
                oAccount.City = oAddressControl.txtCity.Text.Trim();
                oAccount.Zip = oAddressControl.txtZip.Text.Trim();
                oAccount.State = oAddressControl.cmbState.Text.Trim();
                oAccount.Country = oAddressControl.cmbCountry.Text.Trim();
                oAccount.County = oAddressControl.txtCounty.Text.Trim();
                oAccount.ClinicID = _ClinicID;
                oAccount.MachineName = System.Environment.MachineName;
                oAccount.GuarantorCode = "";
                oAccount.AreaCode = "";
                oAccount.UserID = _UserID;
                oAccount.EntityType = GuarantorType.Personal.GetHashCode();
                oAccount.SiteID = 1;

                //Fill patientaccount object
                oPatientAccount.AccountPatientID = 0;
                oPatientAccount.PatientID = _PatientID;
                oPatientAccount.AccountNo = oPatientDemo.PatientCode;
                oPatientAccount.PatientCode = oPatientDemo.PatientCode;
                oPatientAccount.AccountClosedDate = DateTime.MinValue;
                oPatientAccount.ClinicID = _ClinicID;
                oPatientAccount.SiteID = 1;
                oPatientAccount.UserID = _ClinicID;
                oPatientAccount.MachineName = System.Environment.MachineName;
                oPatientAccount.RecordDate = DateTime.Now;
                oPatientAccount.Active = true;
                oPatientAccount.OwnAccount = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Method to get the GuarantorTypeFlag.
        /// </summary>
        /// <returns></returns>
        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlag()
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if (oPatientGuarantors.Count != 0)
            {
                for (int i = 0; i < oPatientGuarantors.Count; i++)
                {
                    if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    { isPrimaryPresent = true; }
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    { isSecondaryPresent = true; }
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                    { isTertioryPresent = true; }
                }

                if (!isPrimaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary; }
                else if (!isSecondaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary; }
                else if (!isTertioryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
                else
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
            }
            else
            {
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
            }
            return _GuarantorTypeFlag;
        }

        #endregion "Patient Account Feature related Methods"

        private void txtmPADOB_Validating(object sender, CancelEventArgs e)
        {
            txtmPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (txtmPADOB.Text.Length > 0 && txtmPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (txtmPADOB.MaskCompleted == true)
                {
                    try
                    {
                        txtmPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(txtmPADOB.Text))
                        {
                            //if (Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date)
                            //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            if (Convert.ToDateTime(txtmPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(txtmPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(txtmPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtmPADOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
        }
        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;
            //vishal 
            if (DOB.Trim().Length <= 4)   // for blank date,length=4 ,including '/' character...  
            {
                return true;
            }

            //*****
            string[] _Date = DOB.Split('/');
            if (_Date.Length == 3)
            {
                for (int i = 0; i < _Date.Length; i++)
                {
                    if (_Date[i].Trim() != "")
                    {
                        if (i == 0)
                        {
                            month = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 1)
                        {
                            day = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 2)
                        {

                            if (_Date[i].Trim().Replace("_", "").Length == 4)
                                year = Convert.ToInt32(_Date[i]);
                            else
                                return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }

                if (month > 12)
                {
                    return false;
                }

                if (day == 29)
                {
                    if (month == 2)
                    {
                        if (year % 4 == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return true;
                    }
                }
                else if (day > 31)
                {
                    return false;
                }
                else if (day == 0)
                {
                    return false;
                }
                else if (day == 31)
                {
                    if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

    }
}
