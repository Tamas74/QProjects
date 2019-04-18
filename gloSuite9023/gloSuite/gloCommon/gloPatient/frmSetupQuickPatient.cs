using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloPatientPortalCommon;
using System.Data.SqlClient;

namespace gloPatient
{
    public partial class frmSetupQuickPatient : gloAUSLibrary.MasterForm
    {


        #region " Declarations "

        private string _databaseconnectionstring = "";
        //private string _messageboxcaption = "gloPM";
        private string _messageboxcaption = String.Empty;

        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _ReturnPatientID = 0;
        private Int64 _nProviderID = 0;
        private string _sProviderName = "";
        private string _ReturnPatientName = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _EMRdatabaseconnectionstring = "";
        //private Int64 _EMRPatientID = 0;
        private bool _AddPatientToEMR = false;

        gloQuickPatientControl ogloQuickPatientControl;
        Patient oPatient = new Patient();
        gloPatient ogloPatient;

        private string _patientalert = "";
       
        //For RxHub
        string _sRxPharmacysNCPDPID = "";

        private Boolean gblnAddModPatient = false;
        private bool _IsFromRefillRequest = false;

        //Patient Portal
        public Boolean gblnPatientPortalSendActivationEmail = false;
        public Boolean gblnPatientPortalActivationEmailAlreadySent = false;
        //Patient Portal
        //API
     

        public Boolean gblnAPIActivation = false;
        #endregion " Declarations "

        #region " Property Procedures "

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

        public Int64 ReturnPatientID
        {
            get { return _ReturnPatientID; }
            set { _ReturnPatientID = value; }
        }

        public string ReturnPatientName
        {
            get { return _ReturnPatientName; }
            set { _ReturnPatientName = value; }
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

        public string PatientAlert
        {
            get { return _patientalert; }
            set { _patientalert = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupQuickPatient(string DatabaseConnectionString)
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
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["EMRConnectionString"] != null)
            { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
            else
            { _EMRdatabaseconnectionstring = ""; }

            if (appSettings["Add Patient To gloEMR"] != null)
            {
                _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
            }
            else
            {
                _AddPatientToEMR = false;
            }

            ogloQuickPatientControl = new gloQuickPatientControl(_databaseconnectionstring);
            ogloQuickPatientControl.onbtnMoreLess_Click +=new gloQuickPatientControl.onbtnMoreLessClicked(ogloQuickPatientControl_onbtnMoreLess_Click);
            ogloQuickPatientControl.Dock = DockStyle.Fill;
            ogloPatient = new gloPatient(_databaseconnectionstring);



            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

        }

        //Constructor to register patient from HxHUB
        public frmSetupQuickPatient(string  FirstName,string  LastName, DateTime DOB, string Gender, 
                                    string AddressLine1,string AddressLine2,string Phone,string City,string State,string Zip,
                                    string PharmacysNCPDPID, string DatabaseConnectionString,Int64 ProviderID,String Fax, String MiddleName,bool IsFromRefillRequest=false)
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
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["EMRConnectionString"] != null)
            { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
            else
            { _EMRdatabaseconnectionstring = ""; }

            if (appSettings["Add Patient To gloEMR"] != null)
            {
                _AddPatientToEMR = Convert.ToBoolean(appSettings["Add Patient To gloEMR"]);
            }
            else
            {
                _AddPatientToEMR = false;
            }

         

            ogloQuickPatientControl = new gloQuickPatientControl(_databaseconnectionstring);

            //Set RX Hub Patient details 
            _sRxPharmacysNCPDPID = PharmacysNCPDPID; 
            ogloQuickPatientControl.RxFirstName = FirstName;
            ogloQuickPatientControl.RxLastName = LastName;
            ogloQuickPatientControl.RxMiddleName = MiddleName;
            ogloQuickPatientControl.RxDOB = DOB;
            ogloQuickPatientControl.RxGender = Gender;
            ogloQuickPatientControl.RxAddressLine1 = AddressLine1;
            ogloQuickPatientControl.RxAddressLine2 = AddressLine2;
            ogloQuickPatientControl.RxPhone = Phone;
            ogloQuickPatientControl.RxCity = City;
            ogloQuickPatientControl.RxState = State;
            ogloQuickPatientControl.RxZip = Zip;
            ogloQuickPatientControl.RxFax = Fax;
            //-------------------------------------

            ogloQuickPatientControl.onbtnMoreLess_Click += new gloQuickPatientControl.onbtnMoreLessClicked(ogloQuickPatientControl_onbtnMoreLess_Click);
            ogloQuickPatientControl.Dock = DockStyle.Fill;
            ogloPatient = new gloPatient(_databaseconnectionstring);

            _nProviderID = ProviderID;//RXHUB

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion
            _IsFromRefillRequest = IsFromRefillRequest;
        }


        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupQuickPatient_Load(object sender, EventArgs e)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                
            try
            {

                //Patient Portal
                IsPatientPortalEnabled();
                ogloQuickPatientControl.gblnPatientPortalEnabled = blnPatientPortalEnabled;
                //Patient Portal

                this.Size = new Size(698, 315);
                //this.Size = new Size(504, 289);
               // this.Size = ogloQuickPatientControl.Size;
              //  ogloQuickPatientControl.txtPACode.ReadOnly = true;
                
             
              
                object oResult;
                object oResultAllowEditablePatientCode;
                    ogloSettings.GetSetting("UseSitePrefix", out oResult);
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
                                ogloQuickPatientControl.txtPatientPrefix.Text = Convert.ToString(appSettings["PatientPrefix"]);
                                ogloQuickPatientControl.txtPACode.Text = Convert.ToString(appSettings["PatientPrefix"]);
                            }
                        }
                        if (ogloQuickPatientControl.txtPatientPrefix.Text.Trim() == "")
                        {
                            gloPatient oPatient = new gloPatient(_databaseconnectionstring);
                            DataTable dtPrefix = oPatient.GetPrefix();
                            if (dtPrefix != null)
                            {
                                if (dtPrefix.Rows.Count > 0)
                                {
                                    ogloQuickPatientControl.txtPatientPrefix.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                    ogloQuickPatientControl.txtPACode.Text = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                    appSettings["PatientPrefix"] = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);


                                }
                            }
                            if (dtPrefix != null) { dtPrefix.Dispose(); }
                            if (oPatient != null) { oPatient.Dispose(); }
                        }

                        ogloQuickPatientControl.ProviderID = _nProviderID;
                        ogloQuickPatientControl.ProviderName = _sProviderName;
                        if (ogloQuickPatientControl.txtPatientPrefix.Text.Trim() == "")
                        {

                            MessageBox.Show("Site Prefix is not set up for this site. Please contact your administrator to set up the site prefix before registering  patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Opacity = 0;
                            this.Close();
                            return;
                        }
                    }
                    else
                    {
                        ogloQuickPatientControl.txtPACode.Mask = "AAAAAAAAAAAAA";
                    }

                    

                        // solving sales force case-GLO2010-0006466
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
                                ogloQuickPatientControl.txtPACode.Enabled = false;
                            }
                            else if (_AutoGenerate != 0 && _AllowEditableCode != 0) //both true
                            {
                                ogloQuickPatientControl.txtPACode.Enabled = true;
                            }
                            else  //autogenerate false
                            {
                                ogloQuickPatientControl.txtPACode.Enabled = true;
                                ogloQuickPatientControl.txtPACode.Text = "";
                            }
                            //if (_AutoGenerate != 0) //Auto generate is true
                            //{
                            //    ogloQuickPatientControl.txtPACode.Enabled = false;
                            //}
                            //else  //Auto generate is false
                            //{
                            //    ogloQuickPatientControl.txtPACode.Enabled = true;
                            //    ogloQuickPatientControl.txtPACode.Text = "";
                            //}
                        }
                          
                        pnlContainer.Controls.Add(ogloQuickPatientControl);

                        if (_IsFromRefillRequest == true)
                        {
                            ogloQuickPatientControl.txtPAFname.Enabled = false;
                            ogloQuickPatientControl.txtPAMName.Enabled = false;
                            ogloQuickPatientControl.txtPALName.Enabled = false;
                            ogloQuickPatientControl.txtmPADOB.Enabled = false;
                            ogloQuickPatientControl.gbPAGender.Enabled = false;
                        }
                //End
                        // License Check
                        List<object> _ToolStrip = new List<object>();
                        _ToolStrip.Add(this.tsb_OK);
                        base.FormControls = null;
                        base.FormControls = _ToolStrip.ToArray();
                        base.SetChildFormControls();
                        _ToolStrip = null;
                // end License Check
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
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }
        }

        public void ogloQuickPatientControl_onbtnMoreLess_Click(object sender, EventArgs e)
        {
            
           
            try
            {
               
                if (ogloQuickPatientControl.MoreLess == 2)
                {
                    //this.Size = new Size(698, 615);
                    this.Size = new Size(698, 650);
                    pnlTOP.Visible = true;
                }
                else if(ogloQuickPatientControl.MoreLess==3)
                {
                    pnlTOP.Visible = false;
                    this.Size = new Size(750, 730);
                    //ogloQuickPatientControl.Dock = DockStyle.Fill;
                }
                else
                {
                    pnlTOP.Visible = true;
                    //this.Size = new Size(698, 298);
                    this.Size = new Size(698, 315);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        

        #endregion " Form Load "

        #region  " Tool Strip Click Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            if (ogloQuickPatientControl.GetData() == true)
                            {
                                if (base.SetChildFormModules("ts_Commands_ItemClicked", "Save Quick Patient", ogloQuickPatientControl.PatientDemo.PatientProviderID.ToString()) == true)
                                {
                                    return;
                                }
                                long _PatientId = 0;
                                oPatient.DemographicsDetail = ogloQuickPatientControl.PatientDemo;
                              //added validation message for duplicate SSN condition for problem 00000349  
                                if (oPatient.DemographicsDetail.PatientSSN.ToString().Trim() != "")
                                {
                                    gloPatient ogloPat = new gloPatient(_databaseconnectionstring);
                                    Boolean _IsExist = ogloPatient.IsPatientSSNExists(oPatient.DemographicsDetail.PatientSSN.ToString().Trim(), _PatientId);

                                    if (_IsExist == true)
                                    {
                                        DialogResult oDialogResult = DialogResult.None;
                                        oDialogResult = MessageBox.Show("SSN already exists for another patient. Do you want to save patient with same SSN ? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (oDialogResult != DialogResult.Yes)
                                        {
                                            ogloQuickPatientControl.txtmPASSN.Focus();
                                            break;
                                        }
                                    }




                                }
                                
                              
                                oPatient.InsuranceDetails = ogloQuickPatientControl.PatientInsuranceDetails ;
                                //20100503  For GLO2010-0004931 case no. Signature on file is not defaulting when adding a new patient
                                oPatient.PatientDemographicOtherInfo = ogloQuickPatientControl.PatientDemoOtherInfo;
                                //Add Patient pharmacy while reg patient through RxHUB
                                if (_sRxPharmacysNCPDPID.Trim() != "")
                                {
                                    PatientDetail oPatientDetail = GetPatientPharmacy(_sRxPharmacysNCPDPID);
                                    oPatient.PatientPharmacies.Add(oPatientDetail);
                                    oPatientDetail.Dispose();
                                    oPatientDetail = null;
                                }
  
                                _patientalert = ogloQuickPatientControl.AlertDescription;
                                //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                                //PatientAccountGuarantor, Account and PatientAccount objects are assigned to oPatient object
                                oPatient.PatientGuarantors = ogloQuickPatientControl.PatientGuarantors;
                                oPatient.Account = ogloQuickPatientControl.Account;
                                oPatient.PatientAccount = ogloQuickPatientControl.PatientAccount;
                                gblnPatientPortalActivationEmailAlreadySent = ogloQuickPatientControl.gblnPatientPortalActivationEmailAlreadySent;
                                gblnPatientPortalSendActivationEmail = ogloQuickPatientControl.gblnPatientPortalSendActivationEmail;
                                gblnAPIActivation = ogloQuickPatientControl.gblnPatientAPISendActivationEmail;
                                oPatient.IsInsuranceModified = true;
                                ReturnPatientID = ogloPatient.Add(oPatient);
                                oPatient.IsInsuranceModified = false;
                                if (ReturnPatientID>0)
                                {
                                    APIActivation(ReturnPatientID,ogloQuickPatientControl.txtPAEmail.Text);
                                }
                                ReturnPatientName = oPatient.DemographicsDetail.PatientFirstName + " " + oPatient.DemographicsDetail.PatientMiddleName + " " + oPatient.DemographicsDetail.PatientLastName;
                                
                                if (ReturnPatientID > 0)
                                {
                                    //Code added on 20090506 By - Sagar Ghodke
                                    //Code added to implement Patient alert functionality

                                    if (_patientalert.Trim() != "")
                                    {
                                        SaveAlerts(ReturnPatientID,0, _patientalert, true);
                                    }

                                    //End code add 20090506,Sagar Ghodke
                                    
                                    //Added General message queue entry
                                    if (blnPatientPortalEnabled)
                                    {
                                        SendPatientPortalEmails(ReturnPatientID);
                                    }
                                    else
                                    {
                                        InsertInMessageQueue(ReturnPatientID,0);
                                    }
                                    //End to add general message queue 


                                    //Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926
                                    //Also used 'HL7' Setting flag
                                    if (_messageboxcaption  == "gloEMR")
                                    {
                                        if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != null)
                                        {
                                            if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != "")
                                            {
                                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOEMR"])) == true))
                                                {
                                                    if (appSettings["SendPatientDetails"] != null)
                                                    {
                                                        if (appSettings["SendPatientDetails"] != "")
                                                        {
                                                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                                            {
                                                                gblnAddModPatient = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (gloGlobal.gloPMGlobal.MessageBoxCaption == "gloPM")
                                    {
                                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null)
                                        {
                                            if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "")
                                            {
                                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true))
                                                {
                                                    if (appSettings["SendPatientDetails"] != null)
                                                    {
                                                        if (appSettings["SendPatientDetails"] != "")
                                                        {
                                                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                                            {
                                                                gblnAddModPatient = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                  
                                    if (gblnAddModPatient == true)
                                    {
                                        InsertInMessageQueue("A04", ReturnPatientID, ReturnPatientID, _databaseconnectionstring);
                                    }

                                    //if (appSettings["GenerateHL7Message"] != null && appSettings["SendPatientDetails"] != null)
                                    //{
                                    //    if (appSettings["GenerateHL7Message"] != "" && appSettings["SendPatientDetails"] != "")
                                    //    {
                                    //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"]) == true) && (Convert.ToBoolean(appSettings["SendPatientDetails"]) == true))
                                    //        {
                                    //            InsertInMessageQueue("A04", ReturnPatientID, ReturnPatientID, _databaseconnectionstring);
                                    //        }
                                    //    }
                                    //}
                                    //End of code to Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926
                                    //audit trail added to resolve problem  00000302 : 
                                    string userAction = "";
                                    long AUditTrailId = 0;
                                    AUditTrailId=  gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add Patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                                    userAction = "Add Patient";
                                    clsgloPatientAudit oAudit = new clsgloPatientAudit(_databaseconnectionstring);
                                    oAudit.SavePatientAuditDetails(ReturnPatientID, AUditTrailId, userAction);
                                    oAudit.Dispose();
                                    oAudit = null;
                                    //MigratePatient(ReturnPatientID); 
                                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Patient, ActivityCategory.SetupPatient, ActivityType.Add, "Add quick patient", ReturnPatientID, 0, 0, ActivityOutCome.Success);
                                }

                                this.Close();
                            }

                        }
                        break;

                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;

                    default:
                        {
                            this.Close();

                        }
                        break;
                }    
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
                
            }
        }

        private PatientDetail GetPatientPharmacy(string PharmacysNCPDPID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            PatientDetail oPharmacy = null;  
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT Contacts_MST.nContactID,Contacts_MST.sName,ISNULL(Contacts_MST.sContact,'') AS sContact,  ISNULL(Contacts_MST.sAddressLine1,'') AS sAddressLine1, ISNULL(Contacts_MST.sAddressLine2,'') AS sAddressLine2, "
                + " ISNULL(Contacts_MST.sCity,'') AS sCity, ISNULL(Contacts_MST.sState,'') AS sState, ISNULL(Contacts_MST.sZIP,'') AS sZIP, ISNULL(Contacts_MST.sPhone,'') AS sPhone, "
                + " ISNULL(Contacts_MST.sFax,'') AS sFax, ISNULL(Contacts_MST.sEmail,'') AS sEmail, ISNULL(Contacts_MST.sURL, '') AS sURL, ISNULL(Contacts_MST.sMobile,'') AS sMobile, ISNULL(Contacts_MST.sPager,'') AS sPager, "
                + " ISNULL(Contacts_MST.sNotes,'') AS sNotes, ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName, ISNULL(Contacts_MST.sLastName,'') AS sLastName, "
                + " ISNULL(Contacts_MST.sGender,'') AS sGender, ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy, ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, "
                + " ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID, ISNULL(Contacts_Physician_DTL.sUPIN,'') AS sUPIN, ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI, "
                + " ISNULL(Contacts_Physician_DTL.sHospitalAffiliation,'') AS sHospitalAffiliation, ISNULL(Contacts_Physician_DTL.sExternalCode,'') AS sExternalCode, ISNULL(Contacts_Physician_DTL.sDegree,'') AS sDegree,"
                + " ISNULL(Contacts_MST.sNCPDPID,'') AS sNCPDPID,ISNULL(Contacts_MST.sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(Contacts_MST.sServiceLevel,'') AS sServiceLevel,Contacts_MST.ActiveStartTime,Contacts_MST.ActiveEndTime"
                + " FROM Contacts_MST LEFT OUTER JOIN Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID "
                + " WHERE sContactType = 'Pharmacy' AND sNCPDPID = '" + PharmacysNCPDPID + "'";
              
                oDB.Retrive_Query(SqlQuery, out  dt);

                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {
                    oPharmacy = new PatientDetail();
 
                    oPharmacy.ContactId = Convert.ToInt64(dt.Rows[0]["nContactID"]);
                    oPharmacy.Name = Convert.ToString(dt.Rows[0]["sName"]);
                    oPharmacy.Contact = Convert.ToString(dt.Rows[0]["sContact"]);
                    oPharmacy.AddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                    oPharmacy.AddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                    oPharmacy.City = Convert.ToString(dt.Rows[0]["sCity"]);
                    oPharmacy.State = Convert.ToString(dt.Rows[0]["sState"]);
                    oPharmacy.ZIP = Convert.ToString(dt.Rows[0]["sZIP"]);
                    oPharmacy.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                    oPharmacy.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                    oPharmacy.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                    oPharmacy.URL = Convert.ToString(dt.Rows[0]["sURL"]);
                    oPharmacy.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                    oPharmacy.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                    oPharmacy.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                    oPharmacy.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                    oPharmacy.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                    oPharmacy.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                    oPharmacy.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                    oPharmacy.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                    oPharmacy.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                    oPharmacy.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                    oPharmacy.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                    oPharmacy.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);
                    oPharmacy.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                    oPharmacy.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                    oPharmacy.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);

                    oPharmacy.NCPDPID = Convert.ToString(dt.Rows[0]["sNCPDPID"]);

                    if (dt.Rows[0]["ActiveStartTime"] != DBNull.Value)
                        oPharmacy.ActiveStartTime = Convert.ToDateTime(dt.Rows[0]["ActiveStartTime"]);
                    if (dt.Rows[0]["ActiveEndTime"] != DBNull.Value)
                        oPharmacy.ActiveEndTime = Convert.ToDateTime(dt.Rows[0]["ActiveEndTime"]);

                    oPharmacy.ServiceLevel = Convert.ToString(dt.Rows[0]["sServiceLevel"]);
                    oPharmacy.PharmacyStatus = Convert.ToString(dt.Rows[0]["sPharmacyStatus"]);

                    oPharmacy.ContactFlag = PatientContactType.Pharmacy;
                    oPharmacy.ClinicID = 1;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);     
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oPharmacy;
        }

        #endregion  " Tool Strip Click Event "

        private void MigratePatient(long PMPatientID)
        {
            try
            {
                bool _regpatinemr = false;
                if (PMPatientID > 0 && _EMRdatabaseconnectionstring != "" && _AddPatientToEMR == true)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_EMRdatabaseconnectionstring);
                    _regpatinemr = oDB.CheckConnection();
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (_regpatinemr == true)
                {
                    gloPatientMigration.gloPatientMigration ogloPatientMigration = new gloPatientMigration.gloPatientMigration(_EMRdatabaseconnectionstring, _databaseconnectionstring, 1);
                    ogloPatientMigration.SendPatientPMtoEMR(PMPatientID, gloPatientMigration.EnumPatientConflict.None);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private Int64 SaveAlerts(Int64 Patientid,Int64 AlertId, string AlertName, bool bStatus)
        {
            //*** Note 
            //Same method implemented on frmPatientAlert 
            //Any modifications to this method please check it for frmPatientAlert in gloPM
            //**

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nAlertID", AlertId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sAlertName", AlertName, ParameterDirection.Input, SqlDbType.VarChar, 250);
                oParameters.Add("@nAlertType", 0, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@bAlertStatus", bStatus, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@sAlertColor", "", ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nPatientID", Patientid, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("AB_INUP_PatientAlerts", oParameters, out _oResult);

                return Convert.ToInt64(_oResult);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                _oResult = null;
            }
        }

        private void frmSetupQuickPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(ReturnPatientID > 0)
                    appSettings["IsPatientAdded"] = "True";
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #region "HL7"

        private void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string _ConnectionString)
        {

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_ConnectionString);
            gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
            try
            {

                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", "1", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);

                oDBParamters.Add("@sField1", "", ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBLayer != null) { oDBLayer.Dispose(); }
                if (oDBParamters != null) { oDBParamters.Dispose(); }

            }
        }
    

        #endregion

        #region "General Message Queue"
        private void InsertInMessageQueue(long PatientID,long OtherID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParams = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDBParams = new gloDatabaseLayer.DBParameters();
                oDBParams.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParams.Add("@sMachineID", "", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParams.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParams.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParams.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParams.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int);
                oDBParams.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParams.Add("@sServiceName", "GENERAL", ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Connect(false);
                oDB.ExecuteScalar("Gl_InsertMessageQueue", oDBParams);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if(oDBParams!=null)
                {
                    oDBParams.Dispose();
                    oDBParams = null;
                }
            }
        }
        #endregion "General Message Queue

        #region"Patient Portal"
        //Patient Portal
        Boolean blnPatientPortalEnabled = false;
        ClsMessageQueue oclsMessageQueue;
        public void IsPatientPortalEnabled()
        {
            if ((oclsMessageQueue != null))
            {
                oclsMessageQueue = null;
            }
            oclsMessageQueue = new ClsMessageQueue(_databaseconnectionstring, DateTime.Now, 0);
            blnPatientPortalEnabled = oclsMessageQueue.IsPatientPortalEnabled();
            oclsMessageQueue = null;
        }

        private void SendPatientPortalEmails(Int64 PatientID)
        {
            if (blnPatientPortalEnabled && gblnPatientPortalSendActivationEmail)
            {
                if ((oclsMessageQueue != null))
                {
                    oclsMessageQueue = null;
                }
                oclsMessageQueue = new ClsMessageQueue(_databaseconnectionstring,DateTime.Now,PatientID);
                oclsMessageQueue.SendPortalEmails("PatientPortal", gblnPatientPortalSendActivationEmail, gblnPatientPortalActivationEmailAlreadySent);
                oclsMessageQueue = null;
            }
        }
        #endregion

        clsAPIAcceess objclsAPIAcceess = new clsAPIAcceess();
        private void APIActivation(Int64 PatientID, string sEmail)
        {
            if (gblnAPIActivation)
            {
                if ((objclsAPIAcceess != null))
                {
                    objclsAPIAcceess = null;
                }

                APIAccess[] arrAPIAccess = new APIAccess[1];

                APIAccess objAPIAccess = new APIAccess();
                objAPIAccess.APIUserID = PatientID;
                objAPIAccess.UserName = sEmail;
                objAPIAccess.Password = "";

                arrAPIAccess[0] = objAPIAccess;

                objclsAPIAcceess = new clsAPIAcceess();
                Int64 _result = -1;
                _result = objclsAPIAcceess.APIAccessProceess(_databaseconnectionstring, arrAPIAccess, 1, 1, "", DateTime.Now);
                objclsAPIAcceess = null;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Activate, "API activated for patient", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

            }
        }

    }

}