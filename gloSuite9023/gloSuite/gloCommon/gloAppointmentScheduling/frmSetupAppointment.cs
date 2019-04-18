using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAuditTrail;
using gloGeneralItem;
using gloPMGeneral.gloPriorAuthorization;
using gloSettings;
using gloBilling;

namespace gloAppointmentScheduling
{
    public partial class frmSetupAppointment : gloAUSLibrary.MasterForm
    {

        #region "Internal Variable Declaration"
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _clinicID = 0;
        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");
        private Int64 _DefaultLocationID = 0;
   //     private string _DefaultLocation = "";
        private string _ExistingNote = "";
        private Int64 _PrintAppointmentID = 0;
        private bool _IsAppointmentPrinted = false;
        private Int64 _contactid = 0;
        private bool _IsColored = false;
        private bool _IsDateChanged = false;
        private Int64 _nPatientId;
        private string _sSPIID = string.Empty;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 nUsedstatus = 0;
        C1.Win.C1FlexGrid.CellStyle style;
        private int _intcmbapptind = -1;   //variable added to maintain color if user use tab against  for Bug ID : 49128  
        // GLO2011-0011476
        // Variable declared to hold the setting (IsResisterTemplateAppointmentOnly)
        private bool _IsTemplateAppointment = false;

        ArrayList nBlinkingCells = new ArrayList();

        #region "List Controls & Find Criteria Variables"
        private gloListControl.gloListControl oListControl;
        private gloPatient.PatientListControl oPatientListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        #endregion

        #region "List Controls & Find Criteria Variables"
        gloAppointmentScheduling.Criteria.FindRecurrences oFindCriteria = new gloAppointmentScheduling.Criteria.FindRecurrences();
        #endregion

        #region "Load/Unload Flags"
        private bool _IsFormLoading = false;
        private bool _IsPatternChanging = false;
        private bool _IsPatternFinding = false;
        #endregion
        #region C1Grid column Constants
        private const int COL_ID = 0;
        private const int COL_CODE = 1;
        private const int COL_DESC = 2;
        private const int COL_STARTTIME = 3;
        private const int COL_ENDTIME = 4;
        private const int COL_COLUMNCOUNT = 5;
        #endregion

        string ProviderID = "";
        DateTime AppoinmentDate;

        public Int64 CurrentPriorAuthorizationID { get; set; }
        public Int64 UpdatedPriorAuthorizationID { get; set; }

        public Int64 CurrentPatientID { get; set; }
        public Int64 UpdatedPatientID { get; set; }

        #endregion

        DataTable oTableProvider = new DataTable();
        DataTable oTableLocations = new DataTable();
        DataTable oTableAppTypes = new DataTable();
        DataTable oTableStatus = new DataTable();

        bool isBaddebtPatient = false;

        #region "Constructors and Destructors"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmSetupAppointment(string DatabaseConnectionString)
        {
            InitializeComponent();
            _SetAppointmentParameter = new SetAppointmentParameter();
            _databaseconnectionstring = DatabaseConnectionString;


            #region "Retrieve ClinicID from Application Setting"

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _SetAppointmentParameter.ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _SetAppointmentParameter.ClinicID = 0;
                    _clinicID = 0;
                }
            }
            else
            { _SetAppointmentParameter.ClinicID = 0; }

            #endregion " Retrieve ClinicID from Application Setting "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

            #region "UserId"

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            //
            #endregion
           
            #region "HL7 Message queue settings"
            //Added by Abhijeet on 20110921
            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            //End of changes by Abhijeet on 20110921
            #endregion
        }

        private SetAppointmentParameter _SetAppointmentParameter;

        public SetAppointmentParameter SetAppointmentParameters
        {
            get { return _SetAppointmentParameter; }
            set { _SetAppointmentParameter = value; }
        }

        private bool _blnAppointmentChanged = false;
        public bool AppointmentChanged
        {
            get { return _blnAppointmentChanged; }
            set { _blnAppointmentChanged = value; }
        }

        private bool _RegisterPatient = false;
        public bool RegisterPatientnSaveAppointment
        {
            get { return _RegisterPatient; }
            set { _RegisterPatient = value; }
        }
        private bool _IsResourceAppointment = false;
        public bool IsResourceAppointment
        {
            get { return _IsResourceAppointment; }
            set { _IsResourceAppointment = value; }
        }
       

        #endregion

        #region "Property methods"

        public bool IsPARemoved
        {
            get
            {
                if ((CurrentPriorAuthorizationID != 0) && (UpdatedPriorAuthorizationID.Equals(0)))
                { return true; }
                else
                { return false; }
            }
        }

        public bool IsPAUpdated
        {
            get
            {
                if (!IsPARemoved)
                {
                    if (UpdatedPriorAuthorizationID != CurrentPriorAuthorizationID)
                    { return true; }
                    else
                    { return false; }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsPatientRemoved
        {
            get
            {
                if ((CurrentPatientID != 0) && (UpdatedPatientID.Equals(0)))
                { return true; }
                else
                { return false; }
            }
        }

        public bool IsPatientUpdated
        {
            get
            {
                if (!IsPatientRemoved)
                {
                    if (UpdatedPatientID != CurrentPatientID)
                    { return true; }
                    else
                    { return false; }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsPAEntered
        {
            get
            {
                if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                { return true; }
                else
                { return false; }
            }
        }

        private Int64 _MasterAppointmentId;
        public Int64 MasterAppointmentId
        {
            get
            {
                return _MasterAppointmentId;
            }
            set
            {
                _MasterAppointmentId = value;
            }
        }
        private Int64 _DetailAppointmentId;
        public Int64 DetailAppointmentId
        {
            get
            {
                return _DetailAppointmentId;
            }
            set
            {
                _DetailAppointmentId = value;
            }
        }

        #endregion

        private void frmSetupAppointment_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

          



            this.cmbApp_Provider.SelectedIndexChanged -= new System.EventHandler(this.cmbApp_Provider_SelectedIndexChanged);
            this.cmbApp_AppointmentType.SelectedIndexChanged-=new EventHandler(cmbApp_AppointmentType_SelectedIndexChanged);
            _IsFormLoading = true;
            _DefaultLocationID = GetDefaultLocation();
            ClearData();
            GetClinicTiming();
            //lostfocus Event added for Bug ID : 49128
            cmbApp_AppointmentType.LostFocus += new EventHandler(cmbApp_AppointmentType_LostFocus);

            // gloC1FlexStyle.Style(c1GlobalPeriod, true);

            
            rbRec_Pattern_Weekly.Checked = true;

            // GLO2011-0011476
            // Set the variable _IsTemplateAppointment
            SetRegisterAppointmentSettings();       

            if (_SetAppointmentParameter != null)
            {
                // Commented code by pranit on 11 oct 2011 to solve case #12523
                if (_SetAppointmentParameter.LoadParameters == true)
                {
                    if (_SetAppointmentParameter.TemplateAllocationMasterID > 0 && _SetAppointmentParameter.TemplateAllocationID > 0)
                    {
                        tsb_Recurrence.Visible = false;

                        if (_IsTemplateAppointment == true)
                        {
                            // Template Appointment
                            cmbApp_Provider.Enabled = false; 
                            dtpApp_DateTime_StartTime.Enabled = false;
                            dtpApp_DateTime_StartDate.Enabled = false;
                            numApp_DateTime_Duration.Enabled = false;
                            chkApp_DateTime_IsAllDayEvent.Enabled = false;
                            LoadParameters();
                        }
                        else
                        {
                            //Template Appointment
                            cmbApp_Provider.Enabled = true;
                            dtpApp_DateTime_StartTime.Enabled = true;
                            dtpApp_DateTime_StartDate.Enabled = true;
                            numApp_DateTime_Duration.Enabled = true;
                            chkApp_DateTime_IsAllDayEvent.Enabled = true;
                            LoadParameters();

                        }              

                    }
                    else
                    {
                       
                        if (_IsTemplateAppointment == true)
                        {
                            // Template Appointment
                            cmbApp_Provider.Enabled = false ;
                            dtpApp_DateTime_StartTime.Enabled = false;
                            dtpApp_DateTime_StartDate.Enabled = false;
                            numApp_DateTime_Duration.Enabled = false;
                            chkApp_DateTime_IsAllDayEvent.Enabled = false;
                            tsb_Recurrence.Visible = true;
                            LoadParameters();
                        }
                        else
                        {
                            //Template Appointment
                            cmbApp_Provider.Enabled = true;
                            tsb_Recurrence.Visible = true;
                            dtpApp_DateTime_StartTime.Enabled = true;
                            dtpApp_DateTime_StartDate.Enabled = true;
                            numApp_DateTime_Duration.Enabled = true;
                            chkApp_DateTime_IsAllDayEvent.Enabled = true;
                            LoadParameters();

                            GetAppointmentTypeFromAdmin();

                        }             
                    }

                    SetDefaultPASetting(Convert.ToInt64(txtApp_Patient.Tag));
                }
                else
                {
                    if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == false && _SetAppointmentParameter.MasterAppointmentID > 0)
                    {
                                              
                        // solving bud id - 8248
                        // if Restrict to template is On then do not modify the appointment time for existing appointment.
                        if (_IsTemplateAppointment == true)
                        {
                            cmbApp_Provider.Enabled = false; 
                            dtpApp_DateTime_StartTime.Enabled = false;
                            dtpApp_DateTime_StartDate.Enabled = false;
                            numApp_DateTime_Duration.Enabled = false;
                            chkApp_DateTime_IsAllDayEvent.Enabled = false;
                            //Modify Appointment
                            LoadAppointment();

                            if (_SetAppointmentParameter.Usedstatus == ASUsedStatus.Cancel.GetHashCode() || _SetAppointmentParameter.Usedstatus == ASUsedStatus.NoShow.GetHashCode() )
                            {
                                DisableIfCancelledAppt();
                            }
                        }
                        else
                        {                   
                            //Modify Appointment
                            LoadAppointment();

                            if (_SetAppointmentParameter.Usedstatus == ASUsedStatus.Cancel.GetHashCode() || _SetAppointmentParameter.Usedstatus == ASUsedStatus.NoShow.GetHashCode())
                            {
                                DisableIfCancelledAppt();
                            }
                        }
                    }
                }
            }
           
            if (RegisterPatientnSaveAppointment == true)
            {
                this.Opacity = 0;
                Int64 _RegisteredPatientId = 0;
                _RegisteredPatientId = RegisterPatient();

                if (_RegisteredPatientId == 0)
                {
                    this.Close();
                    return;
                }
            }

            ProviderID = Convert.ToString(cmbApp_Provider.SelectedValue);
            AppoinmentDate = dtpApp_DateTime_StartDate.Value;
            _IsFormLoading = false;

            AssignUserRights();
            ShowTotalBalance();
           

            CheckForPA();
            if (Convert.ToInt64(cmbApp_Provider.SelectedValue) == 0)
            {
                IsResourceAppointment = true;

                if (_SetAppointmentParameter != null)
                {
                    if (_SetAppointmentParameter.AppointmentID == 0)
                    {
                        GetDefaultResource();
                    }
                }

            }
            if (txtApp_Patient.Text != "")
            {
                _nPatientId = Convert.ToInt64(txtApp_Patient.Tag.ToString());
            }



            this.cmbApp_AppointmentType.SelectedIndexChanged += new EventHandler(cmbApp_AppointmentType_SelectedIndexChanged);
            this.cmbApp_Provider.SelectedIndexChanged += new System.EventHandler(this.cmbApp_Provider_SelectedIndexChanged);

            #region " Filling Alerts "

            FillPatientAlert();

            if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true && _IsTemplateAppointment == false)
            {
                if (_SetAppointmentParameter.TemplateAllocationMasterID > 0 && _SetAppointmentParameter.TemplateAllocationID > 0)
                {
                }
                else
                {
                    dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.Date.ToShortDateString() + " " + SetAppointmentParameters.StartTime.ToShortTimeString());
                   // numApp_DateTime_Duration.Value = 15;
                    dtpApp_DateTime_StartTime.Enabled = true;
                    dtpApp_DateTime_EndTime.Enabled = true;
                    numApp_DateTime_Duration.Enabled = true;
                    chkApp_DateTime_IsAllDayEvent.Checked = false;
                    cmbApp_AppointmentType_SelectionChangeCommitted(null, null);
                }
            }

            #endregion

            //06-Feb-15 Aniket: Bug #79214 ( Modified): gloEMR: Appoinment- Application gives exception
            c1ProviderProblemType.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1Resources.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

            
            this.Cursor = Cursors.Default;
            // License Check
            List<object> _ToolStrip = new List<object>();
            _ToolStrip.Add(this.tsb_OK);
            _ToolStrip.Add(this.tsb_RegPatient);
            base.FormControls = null;
            base.FormControls = _ToolStrip.ToArray();
            //if (Convert.ToString(appSettings["ProviderID"]) != "0" && Convert.ToString(appSettings["ProviderID"]) != "")
            //{ base.strProviderID = Convert.ToString(appSettings["ProviderID"]); }
            //else
            //{ base.strProviderID = ""; }
            base.SetChildFormControls();
            _ToolStrip = null;
            // end License Check
        }

        private void GetAppointmentTypeFromAdmin()
        {
            gloSettings.GeneralSettings oSet = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
            object _objSettingValue;
            if (System.DateTime.Now.Date != _SetAppointmentParameter.StartDate.Date)
            {
                oSet.GetSetting("Default AppointmentType for future", 0, _clinicID, out _objSettingValue);
                if ( Convert.ToString(_objSettingValue) != "")
                {
                    cmbApp_AppointmentType.SelectedValue = _objSettingValue;
                }
            }
            else
            {
                oSet.GetSetting("Default AppointmentType for Same Day", 0, _clinicID, out _objSettingValue);
                if ((Convert.ToString(_objSettingValue) == "" || Convert.ToString(_objSettingValue) == "0"))
                {
                    oSet.GetSetting("Default AppointmentType for future", 0, _clinicID, out _objSettingValue);

                    if (Convert.ToString(_objSettingValue) != "")
                    {
                        cmbApp_AppointmentType.SelectedValue = _objSettingValue;
                    }
                }
                else
                {
                    cmbApp_AppointmentType.SelectedValue = _objSettingValue;
                }
            }
            if (oSet != null)
            {
                oSet.Dispose();
                oSet = null;
            }
            _objSettingValue = null;
        }

        private void DisableIfCancelledAppt()
        {
            tsb_RegPatient.Enabled = false;
            cmbApp_Provider.Enabled = false; 
            btnApp_Provider.Enabled = false;
            btnApp_ClearProvider.Enabled = false;
            cmbApp_Location.Enabled = false;
            txtApp_Patient.Enabled = false;
            btnApp_Patient.Enabled = false;
            btnApp_ClearPatient.Enabled = false;
            cmbApp_Department.Enabled = false;
            txtPriorAuthorizationNo.Enabled = false;
            btnAdd_PriorAuthorization.Enabled = false;
            btnRemove_PriorAuthorization.Enabled = false;
            cmbApp_AppointmentType.Enabled = false;
            cmbApp_ReferralDoctor.Enabled = false;
            btnApp_ReferralDoctor.Enabled = false;
            btnApp_ClearReferralDoctor.Enabled = false;
            chkPARequired.Enabled = false;
            dtpApp_DateTime_StartDate.Enabled = false;
            dtpApp_DateTime_StartTime.Enabled = false;
            numApp_DateTime_Duration.Enabled = false;
            chkApp_DateTime_IsAllDayEvent.Enabled = false;
            //btnApp_DateTime_Color.Enabled = false;
            //btnApp_ClearDateTime_Color.Enabled = false;
            btnApp_Procedures.Enabled = false;
            btnApp_ClearProcedures.Enabled = false;
            btnApp_Resources.Enabled = false;
            btnApp_ClearResources.Enabled = false;
            txtApp_Notes.Select();
            txtApp_Notes.Focus();
            txtApp_Notes.SelectionStart = txtApp_Notes.Text.Length + 1;
        }

        // GLO2011-0011970 
        // To check patient status is Legal Pending or not and give proper message
        private void frmSetupAppointment_Shown(object sender, EventArgs e)
        {
            try
            {
                using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_databaseconnectionstring))
                {
                    if (objPatient.IsLegalPending(_nPatientId))
                    {
                        if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true && _SetAppointmentParameter.MasterAppointmentID == 0)
                        {
                            MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not create an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtApp_Patient.Text = "";
                            txtApp_Patient.Tag = null;

                            btnApp_ClearPatient_Click(null, null);
                            return;
                        }
                        else
                        {
                            if (Convert.ToInt64(txtApp_Patient.Tag).Equals(CurrentPatientID))
                            {
                                MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not create an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtApp_Patient.Text = "";
                                txtApp_Patient.Tag = null;

                                btnApp_ClearPatient_Click(null, null);
                                return;
                            }
                        }
                    }
                }

                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                if (oSecurity.isBadDebtPatient(_nPatientId, true))
                {

                    DialogResult dr = System.Windows.Forms.MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to schedule a new appointment ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
                    if (dr.ToString() == "No")
                    {
                        this.Close();
                    }
                    else
                    {
                        isBaddebtPatient = true;
                    }
                }
                if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillPatientAlert()
        {
            pnlCommanAlerts.Visible = true;
            pnlgloEMRAlerts.Visible = true;
            pnlgloPMAlerts.Visible = true;
            pnlEMRCaption.Visible = true;
            pnlPMCaption.Visible = true;

            SystemAlert();

            GeneralSettings oSettings = new GeneralSettings(_databaseconnectionstring);
            string sType = oSettings.GetInstallationType(0, 1);
            oSettings.Dispose();
            oSettings = null;

            switch (sType)
            {
                case "gloEMR":
                    pnlgloEMRAlerts.Visible = true;
                    pnlEMRCaption.Visible = false;
                    pnlgloEMRAlerts.Dock = DockStyle.Fill;

                    pnlgloPMAlerts.Visible = false;
                    pnlPMCaption.Visible = false;

                    gloEMRAlerts();
                    break;
                case "gloPM":
                    pnlgloPMAlerts.Visible = true;
                    pnlPMCaption.Visible = false;

                    lblPatientBalanceName.Visible = false;
                    lblPatientBalance.Visible = false;

                    pnlgloPMAlerts.Dock = DockStyle.Fill;

                    pnlgloEMRAlerts.Visible = false;
                    pnlEMRCaption.Visible = false;
                    gloPMAlerts(true);
                    break;
                case "Both":
                case "None":
                    pnlgloEMRAlerts.Visible = true;
                    pnlEMRCaption.Visible = true;
                    lblPatientBalanceName.Visible = false;
                    lblPatientBalance.Visible = false;

                    pnlgloPMAlerts.Visible = true;
                    pnlPMCaption.Visible = true;

                    btnUpEMR.Visible = false;
                    btnUpPM.Visible = true;
                    btnDownEMR.Visible = true;
                    btnDownPM.Visible = false;

                    gloPMAlerts(false);

                    gloEMRAlerts();
                    break;
                default:
                    pnlgloEMRAlerts.Visible = false;
                    pnlEMRCaption.Visible = false;

                    pnlgloPMAlerts.Visible = false;
                    pnlPMCaption.Visible = false;
                    break;
            }
        }
        private void CheckForPA()
        {
            try
            {
                Int64 _patientID = Convert.ToInt64(txtApp_Patient.Tag);
                if (clsgloPriorAuthorization.HasPriorAuthorization(_patientID))
                {
                    if ((Convert.ToString(txtPriorAuthorizationNo.Tag) == "") || (txtPriorAuthorizationNo.Tag == null))
                    {
                        txtPriorAuthorizationNo.Text = "<available>";
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Center;
                        txtPriorAuthorizationNo.ForeColor = Color.Maroon;
                    }
                    else
                    {
                        txtPriorAuthorizationNo.TextAlign = HorizontalAlignment.Left;
                        txtPriorAuthorizationNo.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void AssignUserRights()
        {
            gloUserRights.ClsgloUserRights oLocalClsgloUserRights = null;
            try
            {
                

                if (_UserName.Trim() != "")
                {
                    oLocalClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oLocalClsgloUserRights.CheckForUserRights(_UserName);
                    if (!oLocalClsgloUserRights.NewPatient)
                    {
                        tsb_RegPatient.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (oLocalClsgloUserRights != null)
                    {
                        oLocalClsgloUserRights.Dispose();
                        oLocalClsgloUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }

        # region " Patient Alerts "

        private const int COL_ALERTID = 0;
        private const int COL_ALERTNAME = 1;
        private const int COL_ALERTCOLOR = 2;
        private const int COL_ALERTSTATUS = 3;
        private const int COL_ALERTTYPE = 4;
        private const int COL_PATIENTID = 5;
        private const int COL_COUNT = 6;

        private void gloPMAlerts(bool isPMSite)
        {
            try
            {
                int nHeight = 0;

                Int64 _npatientid = 0;
                if (txtApp_Patient.Tag != null && Convert.ToString(txtApp_Patient.Tag).ToString().Trim() != "" && Convert.ToInt64(txtApp_Patient.Tag) > 0)
                {
                    _npatientid = Convert.ToInt64(txtApp_Patient.Tag);
                }

                FillGlobalPeriodAlerts(_npatientid);  
                //DesignGridPatientAlerts();
                ShowCopayAlert(_npatientid);
                FillPatientAlerts(_npatientid);
                ShowEligibilityCheck(_npatientid);

                if (c1CopayAlert.Visible)
                {
                    nHeight += c1CopayAlert.Height;
                }

                if (c1EligibilityCheck.Visible)
                {
                    nHeight += c1EligibilityCheck.Height;
                }

                if (c1PatientAlerts.Visible)
                {
                    nHeight += c1PatientAlerts.Height;
                }
                if (c1GlobalPeriod.Visible)
                {
                    nHeight += c1GlobalPeriod.Height;
                }
                pnlgloPMAlerts.Height = nHeight;

                if (isPMSite) { c1PatientAlerts.Dock = DockStyle.Fill; }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gloEMRAlerts()
        {
            try
            {
                Int64 _npatientid = 0;
                if (txtApp_Patient.Tag != null && Convert.ToString(txtApp_Patient.Tag).ToString().Trim() != "" && Convert.ToInt64(txtApp_Patient.Tag) > 0)
                {
                    _npatientid = Convert.ToInt64(txtApp_Patient.Tag);
                }
                DesignGridEMRAlerts();
                AddStyleEMRAlertGrid();
                FillEMRAlerts(_npatientid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DesignGridPatientAlerts()
        {
            try
            {
                c1PatientAlerts.Cols.Count = COL_COUNT;
                c1PatientAlerts.Rows.Count = 1;

                c1PatientAlerts.SetData(0, COL_ALERTID, "nAlertID ");
                c1PatientAlerts.SetData(0, COL_ALERTNAME, "Alerts");
                c1PatientAlerts.SetData(0, COL_ALERTTYPE, "Alert Type");
                c1PatientAlerts.SetData(0, COL_ALERTSTATUS, "Status");
                c1PatientAlerts.SetData(0, COL_PATIENTID, "PatientID");
                c1PatientAlerts.SetData(0, COL_ALERTCOLOR, "Alert Color");

                c1PatientAlerts.Cols[COL_ALERTID].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTNAME].Visible = true;
                c1PatientAlerts.Cols[COL_ALERTTYPE].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Visible = false;
                c1PatientAlerts.Cols[COL_PATIENTID].Visible = false;

                c1PatientAlerts.Cols[COL_ALERTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTSTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                int nWidth = pnlAlerts.Width;
                c1PatientAlerts.Cols[COL_ALERTID].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTNAME].Width = (int)(0.90 * (nWidth));
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTTYPE].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Width = 0;
                c1PatientAlerts.Cols[COL_PATIENTID].Width = 0;

                c1PatientAlerts.AllowEditing = false;
                c1PatientAlerts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                c1PatientAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                c1PatientAlerts.Cols[COL_ALERTNAME].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                c1PatientAlerts.Cols[COL_ALERTNAME].ImageAndText = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DesignGridEMRAlerts()
        {
            try
            {
                c1EMRAlerts.Cols.Count = 3;
                c1EMRAlerts.Rows.Count = 0;

                c1EMRAlerts.Cols[0].Visible = false;
                c1EMRAlerts.Cols[1].Visible = true;
                c1EMRAlerts.Cols[2].Visible = false;

                c1EMRAlerts.Cols[1].Width = 100;
                c1EMRAlerts.ExtendLastCol = true;

                c1EMRAlerts.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1EMRAlerts.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1EMRAlerts.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                c1EMRAlerts.AllowEditing = false;
                c1EMRAlerts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                c1EMRAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;

                c1EMRAlerts.Cols[COL_ALERTNAME].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                c1EMRAlerts.Cols[COL_ALERTNAME].ImageAndText = true;                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DesignGridSystemAlerts()
        {
            try
            {
                //c1SystemAlert.Cols.Count = COL_COUNT;
                //c1SystemAlert.Rows.Count = 1;

                //c1SystemAlert.SetData(0, COL_ALERTID, "nAlertID ");
                //c1SystemAlert.SetData(0, COL_ALERTNAME, "Alerts");
                //c1SystemAlert.SetData(0, COL_ALERTTYPE, "Alert Type");
                //c1SystemAlert.SetData(0, COL_ALERTSTATUS, "Status");
                //c1SystemAlert.SetData(0, COL_PATIENTID, "PatientID");
                //c1SystemAlert.SetData(0, COL_ALERTCOLOR, "Alert Color");

                //c1SystemAlert.Cols[COL_ALERTID].Visible = false;
                //c1SystemAlert.Cols[COL_ALERTNAME].Visible = true;
                //c1SystemAlert.Cols[COL_ALERTTYPE].Visible = false;
                //c1SystemAlert.Cols[COL_ALERTSTATUS].Visible = false;
                //c1SystemAlert.Cols[COL_ALERTCOLOR].Visible = false;
                //c1SystemAlert.Cols[COL_PATIENTID].Visible = false;

                //c1SystemAlert.Cols[COL_ALERTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SystemAlert.Cols[COL_ALERTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SystemAlert.Cols[COL_ALERTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SystemAlert.Cols[COL_ALERTSTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SystemAlert.Cols[COL_ALERTCOLOR].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SystemAlert.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                //int nWidth = pnlAlerts.Width;
                //c1SystemAlert.Cols[COL_ALERTID].Width = 0;
                //c1SystemAlert.Cols[COL_ALERTNAME].Width = (int)(0.90 * (nWidth));
                //c1SystemAlert.Cols[COL_ALERTSTATUS].Width = 0;
                //c1SystemAlert.Cols[COL_ALERTTYPE].Width = 0;
                //c1SystemAlert.Cols[COL_ALERTCOLOR].Width = 0;
                //c1SystemAlert.Cols[COL_PATIENTID].Width = 0;

                //c1SystemAlert.AllowEditing = false;
                //c1SystemAlert.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                //c1SystemAlert.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                c1SystemAlert.Rows[0].Visible = false;
                //c1SystemAlert.Cols[COL_ALERTNAME].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                c1SystemAlert.Cols[COL_ALERTNAME].ImageAndText = true;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FillGlobalPeriodAlerts(long _npatientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtGlobalPeriodCount = null;
            oDB.Connect(false);
            string _strSQL = "select COUNT(nid) As GPCount  from Patient_Global_Periods where nPatientID =" + _npatientid + "  and CONVERT(varchar(10),dbo.gloGetDate(),101) between dtStartDate AND dtEndDate";
            oDB.Retrive_Query(_strSQL, out dtGlobalPeriodCount);
            oDB.Disconnect();
            if (dtGlobalPeriodCount != null && dtGlobalPeriodCount.Rows.Count > 0)
            {
                if (dtGlobalPeriodCount.Rows[0][0].ToString() != "0")
                {
                    c1GlobalPeriod.Rows.Add();
                    int _rowIndex = c1GlobalPeriod.Rows.Count - 1;
                    c1GlobalPeriod.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1GlobalPeriod.SetData(_rowIndex, COL_ALERTNAME, "Global Period in Effect");
                    c1GlobalPeriod.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1GlobalPeriod.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                    c1GlobalPeriod.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1GlobalPeriod.SetData(_rowIndex, COL_ALERTCOLOR, "");
                }
            }
            if (c1GlobalPeriod.Rows.Count == 1) { c1GlobalPeriod.Visible = true; } else { c1GlobalPeriod.Visible = false; }


        }
        private void SystemAlert()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            int _rowIndex = 0;
            c1SystemAlert.Rows.Count = 1;
            Int64 _patientid = 0;

            try
            {
                DesignGridSystemAlerts();
                Int64 _npatientid = 0;
                if (txtApp_Patient.Tag != null && Convert.ToString(txtApp_Patient.Tag).ToString().Trim() != "" && Convert.ToInt64(txtApp_Patient.Tag) > 0)
                {
                    _npatientid = Convert.ToInt64(txtApp_Patient.Tag);
                }

                c1SystemAlert.Height = 38;
                pnlCommanAlerts.Height = 39;
                if (_npatientid > 0)
                {
                    _patientid = _npatientid;
                    string LastSeendate = GetLastSeenDate(_patientid);

                    if (LastSeendate.Trim().Length > 0)
                    {
                        c1SystemAlert.Cols.Count = 6;
                        c1SystemAlert.Rows.Add();
                        _rowIndex = c1SystemAlert.Rows.Count - 1;
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTNAME, "Last Seen On : " + Convert.ToDateTime(LastSeendate).ToString("MM/dd/yyyy"));
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1SystemAlert.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    }
                    else
                    {
                        c1SystemAlert.Rows.Add();
                        _rowIndex = c1SystemAlert.Rows.Count - 1;
                        c1SystemAlert.Cols.Count = 6;
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTNAME, "Last Seen On :");
                    }

                    string _nextFutureAppointment = GetNextFutureAppointmentDate(_patientid);

                    if (_nextFutureAppointment.Trim().Length > 0)
                    {
                        c1SystemAlert.Rows.Add();
                        _rowIndex = c1SystemAlert.Rows.Count - 1;
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTNAME, "Future Appointment : " + Convert.ToDateTime(_nextFutureAppointment).ToString("MM/dd/yyyy"));
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1SystemAlert.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    }
                    else
                    {
                        c1SystemAlert.Rows.Add();
                        _rowIndex = c1SystemAlert.Rows.Count - 1;
                        c1SystemAlert.SetData(_rowIndex, COL_ALERTNAME, "Future Appointment : ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private void FillPatientAlerts(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string _strSQL = "";
            int _rowIndex = 0;
            c1PatientAlerts.Rows.Count = 1;
            Int64 _patientid = 0;

            try
            {
                if (PatientID > 0)
                {
                    _patientid = PatientID;

                    //style = c1PatientAlerts.Styles.Add("Node");
                    try
                    {
                        if (c1PatientAlerts.Styles.Contains("Node"))
                        {
                            style = c1PatientAlerts.Styles["Node"];
                        }
                        else
                        {
                            style = c1PatientAlerts.Styles.Add("Node");
                            style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                            style.ForeColor = Color.White;
                            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                            style.Border.Width = 1;
                        }

                    }
                    catch
                    {
                        style = c1PatientAlerts.Styles.Add("Node");
                        style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                        style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                        style.ForeColor = Color.White;
                        style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                        style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                        style.Border.Width = 1;
                    }
               
 

                    oDB.Connect(false);
                    _strSQL = " SELECT nAlertID, sAlertName, nAlertType, bAlertStatus, sAlertColor, nPatientID, nClinicID " +
                              " FROM PatientAlerts " +
                              " WHERE (nPatientID = " + _patientid + ") AND (nClinicID = " + _clinicID + ") " +
                              " ORDER BY dtCreatedDate desc";
                    oDB.Retrive_Query(_strSQL, out dt);
                    oDB.Disconnect();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Alerts ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        c1PatientAlerts.SetCellStyle(_rowIndex, COL_ALERTNAME, "Node");

                        //c1PatientAlerts.Rows.Add();
                        //_rowIndex = c1PatientAlerts.Rows.Count - 1;
                        //c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        //c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Alerts ");
                        //c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        //c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        //c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        //c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        //c1PatientAlerts.SetCellStyle(_rowIndex, COL_ALERTNAME, "Node");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            c1PatientAlerts.Rows.Add();
                            _rowIndex = c1PatientAlerts.Rows.Count - 1;

                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, Convert.ToInt64(dt.Rows[i]["nAlertID"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, Convert.ToString(dt.Rows[i]["sAlertName"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, Convert.ToString(dt.Rows[i]["nAlertType"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, Convert.ToString(dt.Rows[i]["bAlertStatus"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, Convert.ToString(dt.Rows[i]["sAlertColor"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, Convert.ToInt64(dt.Rows[i]["nPatientID"]));
                        }
                    }


                    DataTable dtNotes = new DataTable();
                    dtNotes = GetPatientNotes(_patientid);
                    if (dtNotes != null && dtNotes.Rows.Count > 0)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Calendar Notes ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");

                        for (int i = 0; i < dtNotes.Rows.Count; i++)
                        {
                            c1PatientAlerts.Rows.Add();
                            _rowIndex = c1PatientAlerts.Rows.Count - 1;
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "    " + Convert.ToString(dtNotes.Rows[i]["sNotes"]));
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                            c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        }
                    }                  

                    if (c1PatientAlerts.Rows.Count == 1) { c1PatientAlerts.Visible = false; } else { c1PatientAlerts.Visible = true; }

                    c1PatientAlerts.Dock = DockStyle.Top;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private void FillEMRAlerts(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            int _rowIndex = 0;
            string _strSQL = "";
            c1EMRAlerts.Rows.Count = 0;
            c1EMRAlerts.Cols.Count = 3;
            Int64 _patientid = 0;
            string sPreCaption = string.Empty;

            try
            {
                if (PatientID > 0)
                {
                    _patientid = PatientID;

                    oDB.Connect(false);
                    _strSQL = "SELECT sPatientCode From Patient WHERE nPatientID = " + _patientid;

                    oDB.Retrive_Query(_strSQL, out dt);
                    oDB.Disconnect();


                    dt = ogloAppointment.PatientAlerts(_patientid, Convert.ToString(dt.Rows[0]["sPatientCode"]));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (Convert.ToString(dt.Rows[i]["Alerts"]) != sPreCaption)
                            {
                                c1EMRAlerts.Rows.Add();
                                _rowIndex = c1EMRAlerts.Rows.Count - 1;
                                c1EMRAlerts.SetData(_rowIndex, 1, Convert.ToString(dt.Rows[i]["Alerts"]));
                                c1EMRAlerts.SetCellStyle(_rowIndex, 1, "Node");
                                c1EMRAlerts.Rows.Add();
                                _rowIndex = c1EMRAlerts.Rows.Count - 1;
                                c1EMRAlerts.SetData(_rowIndex, 1, Convert.ToString(dt.Rows[i]["Value"]));
                                // Bug #52559: 00000438 : EMR Dashboard
                                // style and Autosize row added to resolve the issue
                                c1EMRAlerts.SetCellStyle(_rowIndex, 1, "DefaultChildItemRegular");
                                c1EMRAlerts.AutoSizeRow(_rowIndex);                                                          
                            }
                            else
                            {
                                c1EMRAlerts.Rows.Add();
                                _rowIndex = c1EMRAlerts.Rows.Count - 1;
                                c1EMRAlerts.SetData(_rowIndex, 1, Convert.ToString(dt.Rows[i]["Value"]));
                            }

                            sPreCaption = Convert.ToString(dt.Rows[i]["Alerts"]);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private string GetLastSeenDate(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtNotes = new DataTable();

            try
            {
                oDB.Connect(false);
                strSQL = "SELECT MAX(dtDate) FROM PatientTracking WHERE (nPatientID = " + PatientID + ") AND (nClinicID = " + _clinicID + ") AND (nTrackingStatus = 3)";
                oDB.Retrive_Query(strSQL, out dtNotes);
                oDB.Disconnect();

                if (dtNotes.Rows.Count > 0 && dtNotes != null)
                {
                    if (dtNotes.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToDateTime(dtNotes.Rows[0][0]).ToShortDateString();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }


        private string GetNextFutureAppointmentDate(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtNotes = new DataTable();

            try
            {
                oDB.Connect(false);
                strSQL = "SELECT dbo.GET_FUTURE_APPOINTMENT(" + PatientID + ") AS FutureAppointment";
                oDB.Retrive_Query(strSQL, out dtNotes);
                oDB.Disconnect();

                if (dtNotes.Rows.Count > 0 && dtNotes != null)
                {
                    if (Convert.ToString(dtNotes.Rows[0][0]).Trim() != "" && dtNotes.Rows[0][0] != DBNull.Value)
                    { return Convert.ToDateTime(dtNotes.Rows[0][0]).ToShortDateString(); }
                    else
                    { return ""; }
                }
                else
                { return ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (dtNotes != null) { dtNotes.Dispose(); dtNotes = null; }
            }
        }

        private DataTable GetPatientNotes(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtNotes = new DataTable();

            try
            {
                oDB.Connect(false);
                strSQL = "SELECT sNotes FROM Patient_Notes WHERE (nPatientID = " + PatientID + ") AND (nClinicID = " + _clinicID + ")";
                oDB.Retrive_Query(strSQL, out dtNotes);
                oDB.Disconnect();
                if (dtNotes != null)
                {
                    return dtNotes;
                }
                return null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private void AddStyle()
        {
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            Color AlertColor = Color.Red;
            object oValue = new object();

            oSettings.GetSetting("BlinkingAlert", _UserID, _clinicID, out oValue);
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (Convert.ToBoolean(oValue)) { tmrCopayAlertBlink.Start(); }
                else
                {
                    tmrCopayAlertBlink.Stop();
                    if (c1CopayAlert.Rows.Count > 1 && c1CopayAlert.Cols.Count > 1)
                    {
                        c1CopayAlert.SetCellStyle(1, 3, "ChildItemRegular");
                    }
                    _IsColored = false;
                }
            }
            else
            {
                tmrCopayAlertBlink.Stop();
                if (c1CopayAlert.Rows.Count > 1 && c1CopayAlert.Cols.Count > 1)
                {
                    c1CopayAlert.SetCellStyle(1, 3, "ChildItemRegular");
                }
                _IsColored = false;
            }

            oSettings.GetSetting("AlertColor", _UserID, _clinicID, out oValue);
            oSettings.Dispose();
            oSettings = null;
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (oValue.ToString() == "-1")  //code added to replace while color with blue for PM Alert, v8022 PRD change 
                {
                    oValue = "-14726787";
                }

                AlertColor = Color.FromArgb(Convert.ToInt32(oValue));
            }
            else
            {
                AlertColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

           // style = c1CopayAlert.Styles.Add("Default");
            try
            {
                if (c1CopayAlert.Styles.Contains("Default"))
                {
                    style = c1CopayAlert.Styles["Default"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("Default");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = Color.White;
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("Default");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = Color.White;
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }


        //    style = c1CopayAlert.Styles.Add("Node");
            try
            {
                if (c1CopayAlert.Styles.Contains("Node"))
                {
                    style = c1CopayAlert.Styles["Node"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("Node");
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.ForeColor = Color.White;
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("Node");
                style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                style.ForeColor = Color.White;
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }
 

   //         style = c1CopayAlert.Styles.Add("ChildItemBold");
            try
            {
                if (c1CopayAlert.Styles.Contains("ChildItemBold"))
                {
                    style = c1CopayAlert.Styles["ChildItemBold"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("ChildItemBold");
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("ChildItemBold");
                style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            }
 

            style = c1CopayAlert.Styles.Add("ChildItemRegular");
            try
            {
                if (c1CopayAlert.Styles.Contains("ChildItemRegular"))
                {
                    style = c1CopayAlert.Styles["ChildItemRegular"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("ChildItemRegular");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("ChildItemRegular");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }
 

  //          style = c1CopayAlert.Styles.Add("ChildItemRegularRevised");
            try
            {
                if (c1CopayAlert.Styles.Contains("ChildItemRegularRevised"))
                {
                    style = c1CopayAlert.Styles["ChildItemRegularRevised"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("ChildItemRegularRevised");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("ChildItemRegularRevised");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }
 
 


   //         style = c1CopayAlert.Styles.Add("LastChildItem");
            try
            {
                if (c1CopayAlert.Styles.Contains("LastChildItem"))
                {
                    style = c1CopayAlert.Styles["LastChildItem"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("LastChildItem");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Width = 0;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("LastChildItem");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Width = 0;
            }
 

     //       style = c1CopayAlert.Styles.Add("DefaultChildItemRegular");
            try
            {
                if (c1CopayAlert.Styles.Contains("DefaultChildItemRegular"))
                {
                    style = c1CopayAlert.Styles["DefaultChildItemRegular"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("DefaultChildItemRegular");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("DefaultChildItemRegular");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }
 
 

  //          style = c1CopayAlert.Styles.Add("DefaultLastChildItem");
            try
            {
                if (c1CopayAlert.Styles.Contains("DefaultLastChildItem"))
                {
                    style = c1CopayAlert.Styles["DefaultLastChildItem"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("DefaultLastChildItem");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                    style.Border.Width = 0;
                }

            }
            catch
            {
                style = c1CopayAlert.Styles.Add("DefaultLastChildItem");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                style.Border.Width = 0;
            }

            try
            {
                if (c1CopayAlert.Styles.Contains("BadDebtItem"))
                {
                    style = c1CopayAlert.Styles["BadDebtItem"];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add("BadDebtItem");
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
                    style.ForeColor = System.Drawing.Color.Red; //System.DrawingColor.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
                    style.Border.Width = 0;
                }
            }
            catch 
            {

                style = c1CopayAlert.Styles.Add("BadDebtItem");
                style.Font = gloGlobal.clsgloFont.gFont_BOLD; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
                style.ForeColor = System.Drawing.Color.Red; //System.DrawingColor.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
                style.Border.Width = 0;
            }
 
        }

        private void AddStyleEMRAlertGrid()
        {
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            Color AlertColor = Color.Red;
            object oValue = new object();

            oSettings.GetSetting("BlinkingAlert", _UserID, _clinicID, out oValue);


            oSettings.GetSetting("AlertColor", _UserID, _clinicID, out oValue);
            oSettings.Dispose();
            oSettings = null;

            if (oValue != null && Convert.ToString(oValue) != "")
            {
                AlertColor = Color.FromArgb(Convert.ToInt32(oValue));
            }
            else
            {
                AlertColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

//            style = c1EMRAlerts.Styles.Add("Default");
            try
            {
                if (c1EMRAlerts.Styles.Contains("Default"))
                {
                    style = c1EMRAlerts.Styles["Default"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("Default");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = Color.White;
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("Default");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = Color.White;
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }


 //           style = c1EMRAlerts.Styles.Add("Node");
            try
            {
                if (c1EMRAlerts.Styles.Contains("Node"))
                {
                    style = c1EMRAlerts.Styles["Node"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("Node");
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.ForeColor = Color.White;
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1; 
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("Node");
                style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
                style.ForeColor = Color.White;
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1; 
            }
            

         //   style = c1EMRAlerts.Styles.Add("ChildItemBold");
            try
            {
                if (c1EMRAlerts.Styles.Contains("ChildItemBold"))
                {
                    style = c1EMRAlerts.Styles["ChildItemBold"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("ChildItemBold");
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("ChildItemBold");
                style.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            }


      //      style = c1EMRAlerts.Styles.Add("ChildItemRegular");
            try
            {
                if (c1EMRAlerts.Styles.Contains("ChildItemRegular"))
                {
                    style = c1EMRAlerts.Styles["ChildItemRegular"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("ChildItemRegular");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("ChildItemRegular");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
            }


         //   style = c1EMRAlerts.Styles.Add("LastChildItem");
            try
            {
                if (c1EMRAlerts.Styles.Contains("LastChildItem"))
                {
                    style = c1EMRAlerts.Styles["LastChildItem"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("LastChildItem");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = AlertColor;
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Both;
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                    style.Border.Width = 0;
                    style.Border.Color = Color.Transparent;
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("LastChildItem");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = AlertColor;
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Both;
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
                style.Border.Width = 0;
                style.Border.Color = Color.Transparent;
            }
   

      //      style = c1EMRAlerts.Styles.Add("DefaultChildItemRegular");
            try
            {
                if (c1EMRAlerts.Styles.Contains("DefaultChildItemRegular"))
                {
                    style = c1EMRAlerts.Styles["DefaultChildItemRegular"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("DefaultChildItemRegular");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                    style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                    style.Border.Width = 1;
                    //Bug #52559: 00000438 : EMR Dashboard
                    style.WordWrap = true;
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("DefaultChildItemRegular");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
                style.Border.Width = 1;
                //Bug #52559: 00000438 : EMR Dashboard
                style.WordWrap = true;
            }
   


       //     style = c1EMRAlerts.Styles.Add("DefaultLastChildItem");
            try
            {
                if (c1EMRAlerts.Styles.Contains("DefaultLastChildItem"))
                {
                    style = c1EMRAlerts.Styles["DefaultLastChildItem"];
                }
                else
                {
                    style = c1EMRAlerts.Styles.Add("DefaultLastChildItem");
                    style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Both;
                    style.Border.Color = Color.Transparent;
                    style.Border.Width = 0;
                }

            }
            catch
            {
                style = c1EMRAlerts.Styles.Add("DefaultLastChildItem");
                style.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Both;
                style.Border.Color = Color.Transparent;
                style.Border.Width = 0;
            }
   
 

        }

        private void ShowEligibilityCheck(Int64 _CurrentPatientId)
        {

            int COL_ELIGIBILITYCHECK_INSID = 0;
            int COL_ELIGIBILITYCHECK_COUNT = 1;
            int rowIndex = 0;
            string strAlert = "";

            try
            {
                c1EligibilityCheck.Cols.Count = COL_ELIGIBILITYCHECK_COUNT;

                gloAdvancePayment ogloAdvancePayment = new gloAdvancePayment(_databaseconnectionstring);
                DataTable _dtEligibilityCheck = new DataTable();
                _dtEligibilityCheck = GetEligibilityDate(_CurrentPatientId);

                if (_dtEligibilityCheck != null && _dtEligibilityCheck.Rows.Count > 0 && _dtEligibilityCheck.Rows[0]["dtEligibilityCheck"].ToString() != "")
                {
                    c1EligibilityCheck.AllowEditing = false;
                    C1.Win.C1FlexGrid.CellStyle cs;// = c1CopayAlert.Styles.Add("Blink");
                    try
                    {
                        if (c1CopayAlert.Styles.Contains("Blink"))
                        {
                            cs = c1CopayAlert.Styles["Blink"];
                        }
                        else
                        {
                            cs = c1CopayAlert.Styles.Add("Blink");
                            cs.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        }

                    }
                    catch
                    {
                        cs = c1CopayAlert.Styles.Add("Blink");
                        cs.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                    c1EligibilityCheck.Rows.Count = 0;

                    for (int i = 0; i < _dtEligibilityCheck.Rows.Count; i++)
                    {
                        c1EligibilityCheck.Rows.Add();
                        rowIndex = c1EligibilityCheck.Rows.Count - 1;
                        strAlert = "";
                        strAlert = "Eligibility Check was done for ";
                        strAlert = strAlert + Convert.ToString(_dtEligibilityCheck.Rows[0]["sPayerName"]) + " " + "On ";
                        strAlert = strAlert + Convert.ToString(Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]).Date.ToShortDateString());
                        //strAlert = strAlert + " " + "at " + Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]).TimeOfDay.ToString().Remove(8);
                        strAlert = strAlert + " " + "at " + String.Format("{0:t}", Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]));

                        c1EligibilityCheck.SetData(rowIndex, COL_ELIGIBILITYCHECK_INSID, strAlert);
                        c1EligibilityCheck.SetCellStyle(rowIndex, COL_ELIGIBILITYCHECK_INSID, cs);
                    }

                    c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                }
                else
                {
                    c1EligibilityCheck.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetEligibilityDate(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtEligibility = new DataTable();
            try
            {
                oDB.Connect(false);
                //string query = "Select max(dtEligibilityCheck) as dtEligibilityCheck,sPayerName from BL_EligibilityResponse_MST where nPatientID='" + PatientId + "' group by sPayerName";
                string query = "Select top 1 dtEligibilityCheck,sPayerName from BL_EligibilityResponse_MST where nPatientID=" + PatientId + "  order by dtEligibilityCheck desc";
                oDB.Retrive_Query(query, out dtEligibility);
                if (dtEligibility.Rows.Count > 0 && dtEligibility.Rows[0]["dtEligibilityCheck"].ToString() != "")
                {
                    oDB.Disconnect();
                    return dtEligibility;
                }
                else
                {
                    oDB.Disconnect();
                    return null;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        private void ShowCopayAlert(Int64 PatientID)
        {
            int COL_COPAYALERT_INSID = 0;
            int COL_COPAYALERT_INSNAME = 1;
            int COL_COPAYALERT_COPAYAMT = 2;
            int COL_COPAYALERT_ALERTTEXT = 3;
            int COL_COPAYALERT_COUNT = 4;
            decimal damount = 0;
            decimal PAaccountBadDept = 0;

            DataTable _dtCopayAlert = null;
            DataTable _dtLastPatPmt = null;
            DataRow _drPayments = null;
            DataRow _drAccBaddept = null;

            try
            {
                AddStyle();

                c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
                c1CopayAlert.Cols.Count = COL_COPAYALERT_COUNT;
                gloAccountsV2.gloPatientPaymentV2 ogloAdvancePaymentV2 = new gloAccountsV2.gloPatientPaymentV2();
                c1CopayAlert.Height = 19;
                c1CopayAlert.Rows.Count = 0;
                c1CopayAlert.AllowEditing = false;
                c1CopayAlert.Tree.Column = 3;
                c1CopayAlert.Tree.LineColor = Color.Transparent;

                _dtCopayAlert = ogloAdvancePaymentV2.GetExpectedCopayAmt(PatientID);

                if (_dtCopayAlert != null && _dtCopayAlert.Rows.Count > 0)
                {

                    if (_dtCopayAlert.Rows.Count > 1)
                    {
                        if (Convert.ToDecimal(_dtCopayAlert.Compute("sum(nCopay)", "")) > 0)
                        {
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            c1CopayAlert.Rows.Add();
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                        }

                        for (int iCount = 0; iCount <= _dtCopayAlert.Rows.Count - 1; iCount++)
                        {
                            if (Convert.ToDecimal(_dtCopayAlert.Rows[iCount]["nCopay"]) != 0)
                            {
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                c1CopayAlert.Rows.Add();
                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemBold");
                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, Convert.ToString(_dtCopayAlert.Rows[iCount]["sInsuranceName"]));
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                c1CopayAlert.Rows.Add();
                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                                damount = Convert.ToDecimal(_dtCopayAlert.Rows[iCount]["nCopay"]);
                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                                nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                                if (c1CopayAlert.Rows.Count > 0)
                                {
                                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                                }
                            }
                            //if (c1CopayAlert.Rows.Count > 0)
                            //{
                            //    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                            //}

                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(_dtCopayAlert.Rows[0]["nCopay"]) != 0)
                        {
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            c1CopayAlert.Rows.Add();
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            c1CopayAlert.Rows.Add();
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                            damount = Convert.ToDecimal(_dtCopayAlert.Rows[0]["nCopay"]);
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                            nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                        }

                    }
                }
                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                c1CopayAlert.Rows.Add();
                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");
                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Balance");

                _dtLastPatPmt = ogloAdvancePaymentV2.GetLastPatientPmtAmt(PatientID);

                if (_dtLastPatPmt.Rows.Count > 0)
                {
                    damount = Convert.ToDecimal(_dtLastPatPmt.Rows[0]["LastPay"]);
                    if (damount != 0)
                    {
                        c1CopayAlert.Height = c1CopayAlert.Height + 19;
                        c1CopayAlert.Rows.Add();
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: " + Convert.ToDateTime(_dtLastPatPmt.Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yyyy"));

                        c1CopayAlert.Height = c1CopayAlert.Height + 19;
                        c1CopayAlert.Rows.Add();

                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "                     $" + (damount).ToString("N2").Trim());
                    }
                }

                _drPayments = ogloAdvancePaymentV2.GetPatientBalances(PatientID);

                c1CopayAlert.Height = c1CopayAlert.Height + 19;

                c1CopayAlert.Rows.Add();

                if (_drPayments != null)
                {
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) + Convert.ToDecimal(_drPayments["BadDebt"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    }
                    else
                    {
                        damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    }

                   // damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                }

                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                c1CopayAlert.Rows.Add();

                if (_drPayments != null)
                {
                    damount = Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]);
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");

                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }
                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                }

                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    c1CopayAlert.Height = c1CopayAlert.Height + 19;
                    c1CopayAlert.Rows.Add();
                    //c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                    //nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                    //nd.Level = 1;
                    if (_drPayments != null)
                    {
                        damount = Convert.ToDecimal(_drPayments["BadDebt"]);
                        if (damount == 0)
                        {
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                        }
                        else
                        {
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                            
                        }
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");
                        damount = 0;
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                    }

                    _drAccBaddept = ogloAdvancePaymentV2.GetPatientAccountBadDept(PatientID);

                    if (_drPayments != null)
                    {
                        PAaccountBadDept = Convert.ToDecimal(_drAccBaddept["PAccountBadDept"]);
                        if (PAaccountBadDept != 0)
                        {
                            c1CopayAlert.Height = (c1CopayAlert.Height + 19);
                            c1CopayAlert.Rows.Add();
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Acct. Bad Dept: $" + (PAaccountBadDept).ToString("N2").Trim());
                        }

                    }

                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    if (oSecurity.isBadDebtPatient(PatientID, true))
                    {
                        c1CopayAlert.Height = (c1CopayAlert.Height + 19);
                        c1CopayAlert.Rows.Add();
                        // c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "BadDebtItem");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "BAD DEBT");
                    }
                    else
                    {
                        isBaddebtPatient = false;
                    }
                  
                }

                
               

                c1CopayAlert.Cols[COL_COPAYALERT_INSID].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_INSNAME].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_COPAYAMT].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_ALERTTEXT].Visible = true;
                c1CopayAlert.Row = -1;

                c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1CopayAlert.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;

                c1CopayAlert.Height = c1CopayAlert.Rows.Count * 19 + 3;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        #endregion

        #region "New & Load Appointment"


        public void LoadParameters()
        {
            try
            {
                //PrintDialog oPrint = new PrintDialog();
                //System.Drawing.Printing.PrintDocument oDoc = new System.Drawing.Printing.PrintDocument();

                if (_SetAppointmentParameter.AppointmentTypeDesc.Trim() != "")
                {
                    cmbApp_AppointmentType.SelectedIndex = cmbApp_AppointmentType.FindStringExact(_SetAppointmentParameter.AppointmentTypeDesc);
                    cmbApp_AppointmentType_SelectionChangeCommitted(null, null);
                }

                //Set Provider
                if (_SetAppointmentParameter.ProviderName.Trim() != "")
                {
                    //Bug #93369: 00001074: setup Appointment
                     cmbApp_Provider.SelectedValue = _SetAppointmentParameter.ProviderID;
                    //cmbApp_Provider.Text = _SetAppointmentParameter.ProviderName;
                }

                //Set Patient
                if (_SetAppointmentParameter.PatientID > 0)
                {
                    //Load patient Name
                    string _strSQL = "";
                    DataTable dt = new DataTable();
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                    _strSQL = "SELECT ISNULL(sFirstName,'')+ Space(1) + ISNULL(sMiddleName,'')+ Space(1) + ISNULL(sLastName,'') AS sPatientName FROM Patient where nPatientID= " + _SetAppointmentParameter.PatientID + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtApp_Patient.Text = dt.Rows[0]["sPatientName"].ToString();
                        txtApp_Patient.Tag = _SetAppointmentParameter.PatientID;
                        CurrentPatientID = _SetAppointmentParameter.PatientID;
                        this.Text = "Appointment";
                        try
                        {
                            gloPatient.gloPatient.GetWindowTitle(this, CurrentPatientID, _databaseconnectionstring, _MessageBoxCaption);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                    }

                    _strSQL = " SELECT ISNULL(nContactId,0) AS nContactId ,sReferralName=DBO.GET_NAME_WithPrefix(sFirstName,sMiddleName,sLastName,sPrefix,sDegree) "
                            + " FROM  Patient_DTL "
                            + " WHERE  nPatientID = " + _SetAppointmentParameter.PatientID + " AND nClinicID = " + _clinicID + " AND nContactFlag = 3 ORDER BY sReferralName ";
                    DataTable dtReferral = new DataTable();
                    oDB.Retrive_Query(_strSQL, out dtReferral);
                    {
                      
                        cmbApp_ReferralDoctor.DataSource = null;
                        cmbApp_ReferralDoctor.Items.Clear();
                        if (dtReferral != null && dtReferral.Rows.Count > 0)
                        {
                            cmbApp_ReferralDoctor.DisplayMember = "sReferralName";
                            cmbApp_ReferralDoctor.ValueMember = "nContactId";
                            cmbApp_ReferralDoctor.DataSource = dtReferral;
                            cmbApp_ReferralDoctor.SelectedIndex = 0;
                        }
                    }
                    oDB.Disconnect();

                    //Load Patient balance
                    Decimal _TotalBalance = 0;
                    lblPatientBalance.Text = "Total Balance : " + _TotalBalance.ToString("#0.00");
                }
                if (_SetAppointmentParameter.Location != "")
                {
                    cmbApp_Location.SelectedIndex = cmbApp_Location.FindStringExact(_SetAppointmentParameter.Location);
                }
                else
                {
                    if (cmbApp_Location != null && cmbApp_Location.Items.Count > 0)
                    {
                        if (_DefaultLocationID > 0)
                        {
                            cmbApp_Location.SelectedValue = _DefaultLocationID;
                        }
                    }
                }

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);

                object value = new object();
                try
                {
                    oSettings.GetSetting("Appointment Interval", out value);
                    if (value != null && Convert.ToString(value).Trim() != "")
                    {
                        numApp_DateTime_Duration.Value = Convert.ToInt32(value);
                        numRec_DateTime_Duration.Value = Convert.ToInt32(value);
                        value = null;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
                    value = null;

                }

                if ((Convert.ToInt64(_SetAppointmentParameter.Duration) < numApp_DateTime_Duration.Minimum) || (Convert.ToInt64(_SetAppointmentParameter.Duration) > numApp_DateTime_Duration.Maximum))
                {
                    if (Convert.ToInt64(_SetAppointmentParameter.Duration) < numApp_DateTime_Duration.Minimum)
                    {
                        numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Minimum;
                        numRec_DateTime_Duration.Value = numRec_DateTime_Duration.Minimum;
                    }
                    else
                    {
                        if (Convert.ToInt64(_SetAppointmentParameter.Duration) > numApp_DateTime_Duration.Maximum)
                        {
                            numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Maximum;
                            numRec_DateTime_Duration.Value = numRec_DateTime_Duration.Maximum;
                        }
                    }
                }
                else
                {
                    numApp_DateTime_Duration.Value = _SetAppointmentParameter.Duration;
                    numRec_DateTime_Duration.Value = _SetAppointmentParameter.Duration;
                }

                cmbApp_Department.SelectedIndex = cmbApp_Department.FindStringExact(_SetAppointmentParameter.Department);
                dtpApp_DateTime_StartDate.Value = _SetAppointmentParameter.StartDate;
                dtpApp_DateTime_StartTime.Value = _SetAppointmentParameter.StartTime;
                dtpRec_Range_StartDate.Value = _SetAppointmentParameter.StartDate;
                dtpRec_DateTime_StartTime.Value = _SetAppointmentParameter.StartTime;
                dtpRec_DateTime_EndTime.Value = _SetAppointmentParameter.StartTime.AddMinutes(Convert.ToDouble(_SetAppointmentParameter.Duration));

                numRec_Range_EndAfterOccurence.Value = 1;

                if (_SetAppointmentParameter.PatientID == 0)
                {
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
        }

        public void LoadAppointment()
        {
            try
            {
                gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                gloAppointmentScheduling.MasterAppointment oMasterAppointment = new MasterAppointment();
                oMasterAppointment = ogloAppointment.GetMasterAppointment(_SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID, _SetAppointmentParameter.ModifyAppointmentMethod, _SetAppointmentParameter.ModifyMasterAppointmentMethod, _SetAppointmentParameter.ModifySingleAppointmentFromReccurence, _SetAppointmentParameter.ClinicID);
                 if (oMasterAppointment != null)
                {
                    #region "Providers"
                    cmbApp_Provider.Text = oMasterAppointment.ASBaseDescription;
                    #endregion

                    #region "Patient & Notes"
                    txtApp_Patient.Text = oMasterAppointment.PatientName;
                    txtApp_Patient.Tag = oMasterAppointment.PatientID;
                    CurrentPatientID = oMasterAppointment.PatientID;

                    txtApp_Notes.Text = oMasterAppointment.Notes;
                    _ExistingNote = oMasterAppointment.Notes;
                    this.Text = "Appointment";
                    try
                    {
                        gloPatient.gloPatient.GetWindowTitle(this, CurrentPatientID, _databaseconnectionstring, _MessageBoxCaption);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }
                    #endregion

                    #region "Location"
                    if (oMasterAppointment.LocationName != "")
                    {
                        bool _AddItem = true;
                        int _t = cmbApp_Location.FindStringExact(oMasterAppointment.LocationName);
                        if (_t >= 0) { _AddItem = false; }

                        if (_AddItem == true)
                        {
                            DataTable oBindTable;
                            if (cmbApp_Location.DataSource != null)
                            {
                                oBindTable = (DataTable)cmbApp_Location.DataSource;
                            }
                            else
                            {
                                oBindTable = new DataTable();
                            }

                            DataRow oRow;
                            oRow = oBindTable.NewRow();
                            oRow[0] = oMasterAppointment.LocationID;
                            oRow[1] = oMasterAppointment.LocationName;
                            oBindTable.Rows.Add(oRow);
                        }

                        cmbApp_Location.SelectedIndex = cmbApp_Location.FindStringExact(oMasterAppointment.LocationName);
                    }
                    #endregion

                    #region "Department"
                    if (oMasterAppointment.DepartmentName != "")
                    {
                        bool _AddItem = true;
                        int _t = cmbApp_Department.FindStringExact(oMasterAppointment.DepartmentName);
                        if (_t >= 0) { _AddItem = false; }

                        if (_AddItem == true)
                        {
                            DataTable oBindTable;
                            if (cmbApp_Department.DataSource != null)
                            {
                                oBindTable = (DataTable)cmbApp_Department.DataSource;
                            }
                            else
                            {
                                oBindTable = new DataTable();
                            }

                            DataRow oRow;
                            oRow = oBindTable.NewRow();
                            oRow[0] = oMasterAppointment.DepartmentID;
                            oRow[1] = oMasterAppointment.DepartmentName;
                            oBindTable.Rows.Add(oRow);
                        }

                        cmbApp_Department.SelectedIndex = cmbApp_Department.FindStringExact(oMasterAppointment.DepartmentName);
                    }
                    #endregion

                    #region "Appointment Type"
                    if (oMasterAppointment.AppointmentTypeDesc != "")
                    {
                        bool _AddItem = true;
                        int _t = cmbApp_AppointmentType.FindStringExact(oMasterAppointment.AppointmentTypeDesc);
                        if (_t >= 0) { _AddItem = false; }

                        if (_AddItem == true)
                        {
                            DataTable oBindTable;
                            if (cmbApp_AppointmentType.DataSource != null)
                            {
                                oBindTable = (DataTable)cmbApp_AppointmentType.DataSource;
                            }
                            else
                            {
                                oBindTable = new DataTable();
                            }

                            DataRow oRow;
                            oRow = oBindTable.NewRow();
                            oRow[0] = oMasterAppointment.AppointmentTypeID;
                            oRow[1] = oMasterAppointment.AppointmentTypeDesc;
                            oBindTable.Rows.Add(oRow);
                        }

                      
                        cmbApp_AppointmentType.SelectedIndex = cmbApp_AppointmentType.FindStringExact(oMasterAppointment.AppointmentTypeDesc);
                        cmbApp_AppointmentType_SelectionChangeCommitted(null, null);
                    }
                    #endregion

                    #region "Referral Physician"
                  
                    cmbApp_ReferralDoctor.DataSource = null;
                    cmbApp_ReferralDoctor.Items.Clear();
                    if (oMasterAppointment.ReferralProviderID > 0)
                    {
                        DataTable oBindTable = new DataTable();

                        oBindTable.Columns.Add("ID");
                        oBindTable.Columns.Add("DispName");

                        DataRow oRow;
                        oRow = oBindTable.NewRow();
                        oRow[0] = oMasterAppointment.ReferralProviderID;
                        oRow[1] = oMasterAppointment.ReferralProviderName;
                        oBindTable.Rows.Add(oRow);

                        cmbApp_ReferralDoctor.DisplayMember = "DispName";
                        cmbApp_ReferralDoctor.ValueMember = "ID";
                        cmbApp_ReferralDoctor.DataSource = oBindTable;
                    }
                    #endregion

                    #region "Appointment Status"

                    cmbApp_Status.SelectedValue = oMasterAppointment.AppointmentStatusID;
                    cmbApp_Status.Refresh();
                    nUsedstatus = oMasterAppointment.AppointmentStatusID;
                    #endregion

                    #region "Simple Criteria"
                    dtpApp_DateTime_StartDate.Value = oMasterAppointment.StartDate;
                    dtpApp_DateTime_StartTime.Value = oMasterAppointment.StartTime;
                    dtpApp_DateTime_EndDate.Value = oMasterAppointment.EndDate;
                    dtpApp_DateTime_EndTime.Value = oMasterAppointment.EndTime;

                    if (oMasterAppointment.Duration < numApp_DateTime_Duration.Minimum)
                    {
                        numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Minimum;
                    }
                    else
                    {
                        if (oMasterAppointment.Duration > numApp_DateTime_Duration.Maximum)
                        {
                            numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Maximum;
                        }

                        numApp_DateTime_Duration.Value = oMasterAppointment.Duration + 1;
                        numApp_DateTime_Duration.Value = oMasterAppointment.Duration;
                     

                    }

                    lblApp_DateTime_ColorContainer.BackColor = Color.FromArgb(oMasterAppointment.ColorCode);

                    dtpRec_Range_StartDate.Value = oMasterAppointment.StartDate;
                    dtpRec_DateTime_StartTime.Value = oMasterAppointment.StartTime;
                    dtpRec_Range_EndBy.Value = oMasterAppointment.EndDate;
                    dtpRec_DateTime_EndTime.Value = oMasterAppointment.EndTime;
                    numRec_DateTime_Duration.Value = oMasterAppointment.Duration;
                    numRec_Range_EndAfterOccurence.Value = 1;
                    lblRec_DateTime_ColorContainer.BackColor = Color.FromArgb(oMasterAppointment.ColorCode);


                    //if (cmbApp_AppointmentType.SelectedIndex > 0)
                    //{
                    //    btnApp_DateTime_Color.Enabled = false;
                    //    btnApp_ClearDateTime_Color.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnApp_DateTime_Color.Enabled = true;
                    //    btnApp_ClearDateTime_Color.Enabled = true;
                    //}

                    lblApp_Recurrence_Time.Text = "";
                    pnlApp_DateTimeContainer.Visible = true;
                    lblApp_Recurrence_Time.Visible = false;
                    #endregion

                    #region "Recurring Criteria"
                    if (oMasterAppointment.IsRecurrence == SingleRecurrence.Recurrence)
                    {
                        chkRecurring.Checked = true;

                        #region "Recurrence Range"
                        if (oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                        {
                            rbRec_Range_EndAfterOccurence.Checked = true;
                        }
                        else if (oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                        {
                            rbRec_Range_EndBy.Checked = true;
                        }
                        else
                        {
                            rbRec_Range_NoEndDate.Checked = true;
                        }


                        dtpRec_Range_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate);
                        dtpRec_Range_EndBy.Value = gloDateMaster.gloDate.DateAsDate(oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate);
                        numRec_Range_EndAfterOccurence.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                        if (cmbRec_Range_NoEndDateYear.Items.Count > 0)
                        {
                            cmbRec_Range_NoEndDateYear.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear.ToString();
                        }
                        #endregion

                        #region "Appointment Date & Time"
                        dtpRec_DateTime_StartTime.Value = gloDateMaster.gloTime.TimeAsDateTime(dtpRec_Range_StartDate.Value, Convert.ToInt32(oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime));
                        dtpRec_DateTime_EndTime.Value = gloDateMaster.gloTime.TimeAsDateTime(dtpRec_Range_EndBy.Value, Convert.ToInt32(oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime));
                        #endregion

                        #region "Recurrence Pattern"

                        if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                        {
                            rbRec_Pattern_Daily.Checked = true;
                        }
                        else if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                        {
                            rbRec_Pattern_Weekly.Checked = true;
                        }
                        else if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                        {
                            rbRec_Pattern_Monthly.Checked = true;
                        }
                        else if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                        {
                            rbRec_Pattern_Yearly.Checked = true;
                        }

                        #endregion

                        #region "Recurrence Pattern - Daily"
                        if (rbRec_Pattern_Daily.Checked == true)
                        {
                            if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay == RecurrencePatternFlag.EveryDay)
                            {
                                rbRec_Pattern_Daily_EveryDay.Checked = true;
                                numRec_Pattern_Daily_EveryDay.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                            }
                            else
                            {
                                rbRec_Pattern_Daily_EveryWeekday.Checked = true;
                            }
                        }
                        #endregion

                        #region "Recurrence Pattern - Weekly"
                        else if (rbRec_Pattern_Weekly.Checked == true)
                        {
                            numRec_Pattern_Weekly_WeekOn.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;

                            ChkRec_Pattern_Weekly_Sunday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday;
                            ChkRec_Pattern_Weekly_Monday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday;
                            ChkRec_Pattern_Weekly_Tuesday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday;
                            ChkRec_Pattern_Weekly_Wednesday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday;
                            ChkRec_Pattern_Weekly_Thursday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday;
                            ChkRec_Pattern_Weekly_Friday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday;
                            ChkRec_Pattern_Weekly_Saturday.Checked = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday;
                        }
                        #endregion

                        #region "Recurrence Pattern - Monthly"
                        else if (rbRec_Pattern_Monthly.Checked == true)
                        {
                            if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                            {
                                rbRec_Pattern_Monthly_Day.Checked = true;
                                numRec_Pattern_Monthly_Day_Day.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                                numRec_Pattern_Monthly_Day_Month.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                            }
                            else if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria == RecurrencePatternFlag.SelectedCriteria)
                            {
                                rbRec_Pattern_Monthly_Criteria.Checked = true;
                                numRec_Pattern_Monthly_Criteria_Month.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                                cmbRec_Pattern_Monthly_Criteria_FstLst.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.ToString();
                                cmbRec_Pattern_Monthly_Criteria_DayWeekday.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.ToString();
                            }
                        }
                        #endregion

                        #region "Recurrence Pattern - Yearly"
                        else if (rbRec_Pattern_Yearly.Checked == true)
                        {
                            if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                            {
                                rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
                                numRec_Pattern_Yearly_Every_MonthDay.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                                cmbRec_Pattern_Yearly_Every_Month.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.ToString();
                            }
                            else if (oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.SelectedCriteria)
                            {
                                rbRec_Pattern_Yearly_Criteria.Checked = true;
                                cmbRec_Pattern_Yearly_Criteria_Month.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.ToString();
                                cmbRec_Pattern_Yearly_Criteria_FstLst.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.ToString();
                                cmbRec_Pattern_Yearly_Criteria_DayWeekday.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.ToString();
                            }
                        }
                        #endregion

                        #region "Recurrence Range//  we put it again to avoid event fire and fill list of occurrence"
                        if (oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                        {
                            rbRec_Range_EndAfterOccurence.Checked = true;

                        }
                        else if (oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                        {
                            rbRec_Range_EndBy.Checked = true;
                        }
                        else
                        {
                            rbRec_Range_NoEndDate.Checked = true;
                        }


                        dtpRec_Range_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate);
                        dtpRec_Range_EndBy.Value = gloDateMaster.gloDate.DateAsDate(oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate);
                        numRec_Range_EndAfterOccurence.Value = oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                        if (cmbRec_Range_NoEndDateYear.Items.Count > 0)
                        {
                            cmbRec_Range_NoEndDateYear.Text = oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear.ToString();
                        }

                        // Commented by pranit and "tsb_ShowRecurrence_Click()" is called after filling c1Resource 
                        // tsb_ShowRecurrence_Click(null, null);


                        lblApp_Recurrence_Time.Text = "";
                        pnlApp_DateTimeContainer.Visible = false;
                        lblApp_Recurrence_Time.Visible = true;
                        string _RecText = "";
                        _RecText = "Start Date: " + dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy") + "  End Date: " + dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy") + "  Start Time: " + dtpRec_DateTime_StartTime.Value.ToShortTimeString() + "  Duration: " + numRec_DateTime_Duration.Value.ToString() + "  Occurences: " + numRec_Range_EndAfterOccurence.Value.ToString();
                        lblApp_Recurrence_Time.Text = _RecText;
                        #endregion
                    }

                    if (_SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                    {
                        dtpApp_DateTime_StartDate.Value = oMasterAppointment.StartDate;
                        dtpApp_DateTime_StartTime.Value = oMasterAppointment.StartTime;
                        dtpApp_DateTime_EndDate.Value = oMasterAppointment.EndDate;
                        dtpApp_DateTime_EndTime.Value = oMasterAppointment.EndTime;
                        numApp_DateTime_Duration.Value = oMasterAppointment.Duration;
                        lblApp_DateTime_ColorContainer.BackColor = Color.FromArgb(oMasterAppointment.ColorCode);
                        lblRec_DateTime_ColorContainer.BackColor = Color.FromArgb(oMasterAppointment.ColorCode);

                        lblApp_Recurrence_Time.Text = "";
                        pnlApp_DateTimeContainer.Visible = true;
                        lblApp_Recurrence_Time.Visible = false;
                    }
                    #endregion

                    #region "Problem Types"
                    c1ProviderProblemType.Rows.Count = 1;
                    if (oMasterAppointment.ProblemTypes.Count > 0)
                    {
                        for (int i = 0; i <= oMasterAppointment.ProblemTypes.Count - 1; i++)
                        {
                            c1ProviderProblemType.Rows.Add();
                            Int32 RowIndex = c1ProviderProblemType.Rows.Count - 1;
                            c1ProviderProblemType.SetData(RowIndex, COL_ID, oMasterAppointment.ProblemTypes[i].ASCommonID);
                            c1ProviderProblemType.SetData(RowIndex, COL_CODE, oMasterAppointment.ProblemTypes[i].ASCommonCode);
                            c1ProviderProblemType.SetData(RowIndex, COL_DESC, oMasterAppointment.ProblemTypes[i].ASCommonDescription);
                            c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, oMasterAppointment.ProblemTypes[i].StartTime.ToShortTimeString());
                            c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, oMasterAppointment.ProblemTypes[i].EndTime.ToShortTimeString());
                        }
                    }
                    #endregion

                    #region "Resources"

                    c1Resources.Rows.Count = 1;
                    if (oMasterAppointment.Resources.Count > 0)
                    {
                        for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                        {
                            c1Resources.Rows.Add();
                            Int32 RowIndex = c1Resources.Rows.Count - 1;
                            c1Resources.SetData(RowIndex, COL_ID, oMasterAppointment.Resources[i].ASCommonID);
                            c1Resources.SetData(RowIndex, COL_CODE, oMasterAppointment.Resources[i].ASCommonCode);
                            c1Resources.SetData(RowIndex, COL_DESC, oMasterAppointment.Resources[i].ASCommonDescription);
                            c1Resources.SetData(RowIndex, COL_STARTTIME, oMasterAppointment.Resources[i].StartTime.ToShortTimeString());
                            c1Resources.SetData(RowIndex, COL_ENDTIME, oMasterAppointment.Resources[i].EndTime.ToShortTimeString());
                        }



                    }

                    // Added by Pranit to add Resource ID in find Recurrence 
                    tsb_ShowRecurrence_Click(null, null);

                    #endregion

                    #region "Insurances"

                    #endregion

                    #region "Control Show/Hide"

                    #endregion

                    #region "Prior Authorization"

                    DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(_SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID);

                    if (drPA != null)
                    {
                        txtPriorAuthorizationNo.Tag = Convert.ToString(drPA["nPriorAuthorizationID"]);
                        txtPriorAuthorizationNo.Text = Convert.ToString(drPA["sPriorAuthorizationNo"]);

                        if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                        {
                            CurrentPriorAuthorizationID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                            UpdatedPriorAuthorizationID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                        }
                    }

                    chkPARequired.Checked = oMasterAppointment.PARequired;

                    #endregion

                    //Changes: storing appointment master provider details when appointment modified
                    //gloHL7.nOldBaseCode=oMasterAppointment.ASBaseCode; 

                    gloHL7.nOldBaseId = oMasterAppointment.ASBaseID;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void cmbApp_AppointmentType_LostFocus(object sender, EventArgs e)
        {
            //lostfocus Event added for Bug ID : 49128 while pressing Tab color is not changing 
            try
            {
                if (_intcmbapptind != cmbApp_AppointmentType.SelectedIndex)
                {

                    SetAppointmentTypeData();
                  _intcmbapptind = cmbApp_AppointmentType.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        
        public Int64 GetDefaultLocation()
        {
            object oValue;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                oValue = oDB.ExecuteScalar("GetDefaultLocationID");
                oDB.Disconnect();
                return Convert.ToInt64(oValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                oValue = null;
            }
        }

       private void SetAppointmentTypeData()
        {
      

            DataTable dt = new DataTable();
            gloAppointmentBook.Books.AppointmentType oa = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
            try
            {
                if (cmbApp_AppointmentType.SelectedIndex >= 0)
                {
                    //btnApp_DateTime_Color.Enabled = false;
                    //btnApp_ClearDateTime_Color.Enabled = false;
                    chkApp_DateTime_IsAllDayEvent.Checked = false;


                    dt = oa.GetAppointmentType(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            // solving Bug id - 4896
                            // added if (_IsTemplateAppointment == false) condition
                            // Do not change the appointment time if Restrict to template setting (true) is on.

                            if (_IsTemplateAppointment == false)
                            {
                                if ((Convert.ToInt64(dt.Rows[0]["nDuration"]) < numApp_DateTime_Duration.Minimum) || (Convert.ToInt64(dt.Rows[0]["nDuration"]) > numApp_DateTime_Duration.Maximum))
                                {
                                    if (Convert.ToInt64(dt.Rows[0]["nDuration"]) < numApp_DateTime_Duration.Minimum)
                                    {
                                        numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Minimum;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt64(dt.Rows[0]["nDuration"]) > numApp_DateTime_Duration.Maximum)
                                        {
                                            numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Maximum;
                                        }
                                    }
                                }
                                else
                                {
                                    numApp_DateTime_Duration.Value = Convert.ToInt64(dt.Rows[0]["nDuration"]);
                                }
                            }
                            lblApp_DateTime_ColorContainer.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[0]["sColorCode"].ToString()));
                            lblRec_DateTime_ColorContainer.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[0]["sColorCode"].ToString()));
                                            
                                // fill Problem Type Associated with Appointment type
                            DataTable dtProblemType = new DataTable();
                            dtProblemType = oa.GetAppointmentTypeProcedures(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue));

                            if (dtProblemType != null)
                            {
                                if (dtProblemType.Rows.Count > 0 && c1Resources.Rows.Count > 1)
                                {
                                    if (DialogResult.Yes == MessageBox.Show("Selected appointment type has problem type and resource(s) associated. Do you want to overwrite selected resources? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                    {
                                        c1Resources.Rows.Count = 1;
                                        c1ProviderProblemType.Rows.Count = 1;
                                        FillAppResource(dtProblemType);
                                    }
                                }
                                else if (dtProblemType.Rows.Count > 0 && c1Resources.Rows.Count == 1)
                                {
                                    c1Resources.Rows.Count = 1;
                                    c1ProviderProblemType.Rows.Count = 1;
                                    FillAppResource(dtProblemType);

                                }
                                else if (dtProblemType.Rows.Count == 0 && c1Resources.Rows.Count > 1)
                                {
                                }
                            }
                        }
                        else
                        {
                            lblApp_DateTime_ColorContainer.BackColor = Color.White;
                            lblRec_DateTime_ColorContainer.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        lblApp_DateTime_ColorContainer.BackColor = Color.White;
                        lblRec_DateTime_ColorContainer.BackColor = Color.White;
                    }
                }
                else
                {
                    lblApp_DateTime_ColorContainer.BackColor = Color.White;
                    lblRec_DateTime_ColorContainer.BackColor = Color.White;
                    //btnApp_DateTime_Color.Enabled = true;
                    //btnApp_ClearDateTime_Color.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oa != null) { oa.Dispose(); oa = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
               
            }
        
        }


        public void ShowTotalBalance()
        {
            try
            {
                if (txtApp_Patient.Tag != null)
                {
                    #region " Commented code "

                    #endregion

                    //Calculating the Patient Due
                    #region " Calculate the Patient Due "

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    gloAccountsV2.gloPatientPaymentV2 ogloAdvancePaymentV2 = new gloAccountsV2.gloPatientPaymentV2();
                    DataRow _drPayments = null;
                  
                    try
                    {
                        _drPayments = ogloAdvancePaymentV2.GetPatientBalances(Convert.ToInt64(txtApp_Patient.Tag));

                        if (_drPayments != null)
                        {
                            if (Convert.ToString(_drPayments["PatientDue"]) != "" && Convert.ToString(_drPayments["AvailableReserve"]) != "")
                            {
                                decimal _PatDue = Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]);
                                lblPatientBalance.Text = "$ " + _PatDue.ToString("#0.00");
                            }
                            else
                            {
                                lblPatientBalance.Text = "$ " + "0.00";
                            }
                        }
                        else
                        {
                            lblPatientBalance.Text = "$ " + "0.00";
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; };
                    }

                    #endregion
                }
                else
                {
                    lblPatientBalance.Text = "$ " + "0.00";
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        #endregion

        #region "Save Appointments - New Appointment"

        private Int64 SaveAppointment()
        {
            Cursor.Current = Cursors.WaitCursor;
            MasterAppointment oMasterAppointment = new MasterAppointment();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            Int64 _result = 0;
            string tmpProviderID = "";
            try
            {
              
                if (SaveValidation() == true)
                {
                    #region "License validate"
                        if (Convert.ToString(appSettings["ProviderID"]) != "0")
                        { tmpProviderID = ""; }
                        else
                        {
                            if (Convert.ToString(cmbApp_Provider.SelectedValue) != "" && Convert.ToString(cmbApp_Provider.SelectedValue) != "0")
                            {
                                tmpProviderID = cmbApp_Provider.SelectedValue.ToString();
                            }
                            else
                            {
                                using (gloPatient.gloPatientEiligibility oPatientprovider = new gloPatient.gloPatientEiligibility(_databaseconnectionstring))
                                {
                                    tmpProviderID = oPatientprovider.GetPatientProviderID(CurrentPatientID).ToString();
                                }

                            }
                        }
                        if (base.SetChildFormModules("SaveAppointment", "Save Appointment", tmpProviderID) == true)
                        {
                            return 0;
                        }
                    #endregion""
                    CheckPatientInsuranceEligibility(Convert.ToInt64(txtApp_Patient.Tag.ToString()));
                    DialogResult dialog = CheckPriorAuthorization(Convert.ToInt64(txtApp_Patient.Tag.ToString()));
                    if (dialog.ToString() != "OK")
                        return 0;
                    if (_IsAppointmentPrinted == true) // PRINT APPOINTMENT CREATES NEW APPOINTMENT, SAVE AGAIN CREATES NEW APPOINTMENT. SO DELETE PRINTED APPOINTMENT.
                    {
                        DeleteAppointment(_PrintAppointmentID, 0, true);
                    }

                    #region "Master Appointment Data"

                    //if Modify Appointment and 
                    if (_SetAppointmentParameter != null && _SetAppointmentParameter.LoadParameters == true)
                    {
                        oMasterAppointment.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                    }
                    else
                    {
                        oMasterAppointment.MasterID = 0;
                    }

                    oMasterAppointment.IsRecurrence = SingleRecurrence.Single; if (chkRecurring.Checked == true) { oMasterAppointment.IsRecurrence = SingleRecurrence.Recurrence; }
                    oMasterAppointment.ASFlag = _SetAppointmentParameter.AppointmentFlag;

                    oMasterAppointment.AppointmentTypeID = Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                    oMasterAppointment.AppointmentTypeCode = cmbApp_AppointmentType.Text;//Remark
                    oMasterAppointment.AppointmentTypeDesc = cmbApp_AppointmentType.Text;

                    oMasterAppointment.ASBaseID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                    oMasterAppointment.ASBaseCode = cmbApp_Provider.Text; //Remark
                    oMasterAppointment.ASBaseDescription = cmbApp_Provider.Text;
                    if (cmbApp_Provider.Text == "")
                    {
                        oMasterAppointment.ASBaseFlag = ASBaseType.Resource;
                    }
                    else
                    {
                        oMasterAppointment.ASBaseFlag = ASBaseType.Provider;
                    }

                    if (cmbApp_Provider.SelectedIndex > -1)
                    {
                        oMasterAppointment.ReferralProviderID = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        oMasterAppointment.ReferralProviderCode = GetReferralDoctor(Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue));
                        oMasterAppointment.ReferralProviderName = GetReferralDoctor(Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue));
                    }

                    oMasterAppointment.PatientID = Convert.ToInt64(txtApp_Patient.Tag.ToString());

                    if (chkPARequired.Checked == true)
                    { oMasterAppointment.PARequired = true; }
                    else
                    { oMasterAppointment.PARequired = false; }

                    if (chkRecurring.Checked == true)
                    {
                        oMasterAppointment.StartDate = dtpRec_Range_StartDate.Value;
                        oMasterAppointment.StartTime = dtpRec_DateTime_StartTime.Value;
                        oMasterAppointment.EndDate = dtpRec_Range_EndBy.Value;
                        oMasterAppointment.EndTime = dtpRec_DateTime_EndTime.Value;
                        oMasterAppointment.Duration = numRec_DateTime_Duration.Value;
                        oMasterAppointment.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                    }
                    else
                    {
                        oMasterAppointment.StartDate = dtpApp_DateTime_StartDate.Value;
                        oMasterAppointment.EndDate = dtpApp_DateTime_EndDate.Value;

                        oMasterAppointment.StartTime = dtpApp_DateTime_StartTime.Value;
                        oMasterAppointment.EndTime = dtpApp_DateTime_EndTime.Value;

                        oMasterAppointment.Duration = numApp_DateTime_Duration.Value;
                        oMasterAppointment.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                    }


                    if (cmbApp_Location.SelectedIndex > -1)
                    {
                        oMasterAppointment.LocationID = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        oMasterAppointment.LocationName = cmbApp_Location.Text;
                    }

                    if (cmbApp_Department.SelectedIndex > -1)
                    {
                        oMasterAppointment.DepartmentID = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        oMasterAppointment.DepartmentName = cmbApp_Department.Text;
                    }

                    //if (IsNotesPresent(_SetAppointmentParameter.MasterAppointmentID) == false)
                    //{
                    //    if (txtApp_Notes.Text.ToString() != "")
                    //    {
                    //       // DateTime date = DateTime.Now;
                    //        oMasterAppointment.Notes = txtApp_Notes.Text;
                    //    }
                    //    else
                    //    {
                    //        oMasterAppointment.Notes = txtApp_Notes.Text;
                    //    }
                    //}
                    
                   
                    oMasterAppointment.Notes = txtApp_Notes.Text;
                   

                    oMasterAppointment.ClinicID = _SetAppointmentParameter.ClinicID;
                    oMasterAppointment.UsedStatus = ASUsedStatus.NotUsed;

                    // By Default Status should be "Registered" For New Appointment
                    cmbApp_Status.SelectedIndex = cmbApp_Status.FindStringExact("Registred");
                    if (cmbApp_Status.SelectedIndex > 0)
                        oMasterAppointment.AppointmentStatusID = Convert.ToInt64(cmbApp_Status.SelectedValue);

                    // If maximum appointments are registered in a hour then ask to create waiting appointment 
                    if (oMasterAppointment.ASFlag == AppointmentScheduleFlag.TemplateAppointment)
                    {
                        if (IsAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.EndTime, oMasterAppointment.ASBaseID, _SetAppointmentParameter.MasterAppointmentID) == false)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                        }
                        else
                        {
                            DialogResult dgResult = DialogResult.None;

                            if (_IsResourceAppointment == true)
                            {
                                Int64 _nResourceID = 0;
                                string _sResource = string.Empty;
                                bool _blnIsUsed = false;
                                for (int i = 1; i < c1Resources.Rows.Count; i++)
                                {

                                    _nResourceID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString());
                                    if (IsAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.EndDate, _nResourceID, _SetAppointmentParameter.MasterAppointmentID) == true)
                                    {
                                        _blnIsUsed = true;
                                        _sResource = Convert.ToString(c1Resources.GetData(i, COL_DESC).ToString());
                                        break;
                                    }

                                }
                                if (_blnIsUsed == false)
                                {
                                    oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                                }
                                else
                                {

                                    if (cmbApp_Provider.Text != "")
                                    {
                                        dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text.ToString() + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }
                                    else if (c1Resources.Rows.Count > 1)
                                    {
                                        dgResult = MessageBox.Show("Warning –  " + _sResource + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }
                                    if (dgResult == DialogResult.Yes)
                                    {
                                        oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                                    }
                                    else
                                    {
                                        return 0;
                                    }

                                }
                            }

                            else
                            {
                                if (cmbApp_Provider.Text != "")
                                {
                                    dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text.ToString() + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }


                                if (dgResult == DialogResult.Yes)
                                {
                                    oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                                }
                                else
                                {
                                    return 0;
                                }
                            }






                        }
                       
                    }
                    else
                    {
                        if (IsMaximumAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.StartTime, oMasterAppointment.ASBaseID) == false)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                        }
                        else
                        {
                            DialogResult dgResult = DialogResult.None;

                            dgResult = MessageBox.Show("All appointments for " + cmbApp_Provider.Text + " are filled for this time.  Do you want to create an additional appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dgResult == DialogResult.Yes)
                            {
                                oMasterAppointment.UsedStatus = ASUsedStatus.Waiting;
                            }
                            else if (dgResult == DialogResult.No)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        if (_IsResourceAppointment == true)
                        {
                            Int64 _nResourceID = 0;
                            string _sResource = string.Empty;
                            bool _blnIsUsed = false;
                            for (int i = 1; i < c1Resources.Rows.Count; i++)
                            {

                                _nResourceID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString());
                                if (IsAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.EndDate, _nResourceID, _SetAppointmentParameter.MasterAppointmentID) == true)
                                {
                                    _blnIsUsed = true;
                                    _sResource = Convert.ToString(c1Resources.GetData(i, COL_DESC).ToString());
                                    break;
                                }

                            }
                            if (_blnIsUsed == false)
                            {
                                oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            }
                            else
                            {
                                DialogResult dgResult = DialogResult.None;
                                if (cmbApp_Provider.Text != "")
                                {
                                    dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text.ToString() + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }
                                else if (c1Resources.Rows.Count > 1)
                                {
                                    dgResult = MessageBox.Show("Warning –  " + _sResource + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }
                                if (dgResult == DialogResult.Yes)
                                {
                                    oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                                }
                                else
                                {
                                    return 0;
                                }

                            }
                        }
                        else
                        {
                            //COMMENTED BY SHUBHANGI TO RESOLVED ISSUE : CREATING RECURRNACE APPOINTMENT IT WAS SHOWING CONFLCT MESSAGE THOUGH THERE IS NO APPOINTMENT FOR CONFLICT
                            //SHUBHANGI 20100715 RESTRICT TO ADD NEW APPOINTMENT IN THE ALREADY ALLOCATED TIME SPAN

                            if (IsAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.EndTime, oMasterAppointment.ASBaseID, _SetAppointmentParameter.MasterAppointmentID) == false)
                            {
                                oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            }
                            else
                            {
                                DialogResult dgResult = DialogResult.None;
                                dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (dgResult == DialogResult.Yes)
                                {
                                    oMasterAppointment.UsedStatus = ASUsedStatus.Waiting;
                                }
                                else
                                {
                                    return 0;
                                }

                            }
                        }
                    }

                    #endregion

                    #region "Appointment Criteria"

                    if (chkRecurring.Checked == false)
                    {
                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;
                    }
                    else
                    {
                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Recurrence;
                    }

                    if (chkRecurring.Checked == false)
                    {
                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;
                    }
                    //2.3 If single appointment then setup -- ***RECURRENCE APPOINTMENT***
                    else
                    {
                        //2.3.1 Appointment Date & Time
                        oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_EndTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.Duration = numRec_DateTime_Duration.Value;

                        //2.3.2 Recurrence Pattern

                        if (rbRec_Pattern_Daily.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;
                        }
                        else if (rbRec_Pattern_Weekly.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Weekly;
                        }
                        else if (rbRec_Pattern_Monthly.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Monthly;
                        }
                        else if (rbRec_Pattern_Yearly.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Yearly;
                        }


                        //2.3.2.1 Recurrence Pattern - Daily
                        if (rbRec_Pattern_Daily.Checked == true)
                        {
                            if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Value);
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                            }
                            else
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 0;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryWeekday;
                            }
                        }
                        //2.3.2.2 Recurrence Pattern - Weekly
                        else if (rbRec_Pattern_Weekly.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Value);
                            if (ChkRec_Pattern_Weekly_Sunday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = true; }
                            if (ChkRec_Pattern_Weekly_Monday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = true; }
                            if (ChkRec_Pattern_Weekly_Tuesday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = true; }
                            if (ChkRec_Pattern_Weekly_Wednesday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = true; }
                            if (ChkRec_Pattern_Weekly_Thursday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = true; }
                            if (ChkRec_Pattern_Weekly_Friday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = true; }
                            if (ChkRec_Pattern_Weekly_Saturday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = true; }
                        }
                        //2.3.2.3 Recurrence Pattern - Monthly
                        else if (rbRec_Pattern_Monthly.Checked == true)
                        {
                            if (rbRec_Pattern_Monthly_Day.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Value);
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Value);
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = DayWeekday.day;
                            }
                            else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = 0;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Value);
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString());
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString());
                            }
                        }
                        //2.3.2.4 Recurrence Pattern - Yearly
                        else if (rbRec_Pattern_Yearly.Checked == true)
                        {
                            if (rbRec_Pattern_Yearly_EveryMonthDay.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Value);
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString());
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;
                            }
                            else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString());
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString());
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString());
                            }

                        }

                        //--- Recurrence Range
                        if (rbRec_Range_EndAfterOccurence.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndAfterOccurence;
                        }
                        else if (rbRec_Range_EndBy.Checked == true)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate;
                        }
                        else
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndByYear;
                        }

                        oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(numRec_Range_EndAfterOccurence.Value);
                        if (cmbRec_Range_NoEndDateYear.SelectedItem != null)
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(cmbRec_Range_NoEndDateYear.SelectedItem.ToString());
                        }
                        else
                        {
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(DateTime.Now.Year);
                        }
                        //--- Recurrence Range

                    }
                    //-------------------------------------------------------------

                    #endregion

                    #region "Problem Types"
                    for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterAppointment.IsRecurrence;
                        oShortDetail.PatientID = oMasterAppointment.PatientID;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterAppointment.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.ProblemType;

                        if (chkRecurring.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpApp_DateTime_EndDate.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        oShortDetail.ClinicID = _SetAppointmentParameter.ClinicID;
                        oShortDetail.ViewOtherDetails = "";
                        oShortDetail.UsedStatus = ASUsedStatus.NotUsed;
                        oMasterAppointment.ProblemTypes.Add(oShortDetail);
                    }
                    #endregion

                    #region "Resources"

                    StringBuilder resourceIDS = new StringBuilder("");

                    for (int i = 1; i < c1Resources.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterAppointment.IsRecurrence;
                        oShortDetail.PatientID = oMasterAppointment.PatientID;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterAppointment.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1Resources.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1Resources.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.Resource;

                        if (chkRecurring.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpApp_DateTime_EndDate.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        oShortDetail.ClinicID = _SetAppointmentParameter.ClinicID;
                        oShortDetail.ViewOtherDetails = "";
                        oShortDetail.UsedStatus = ASUsedStatus.NotUsed;

                        oMasterAppointment.Resources.Add(oShortDetail);

                        resourceIDS= resourceIDS.Append(c1Resources.GetData(i, COL_ID).ToString());
                        resourceIDS = resourceIDS.Append(",");          

                    }

                    if (c1Resources.Rows.Count > 1)
                    {
                        resourceIDS = resourceIDS.Remove(resourceIDS.Length - 1, 1);
                        oMasterAppointment.ResourceIDS = resourceIDS;
                    }
                    else
                    {
                        oMasterAppointment.ResourceIDS = new StringBuilder("");
                    }
           

                    #endregion

                    #region " PA Transaction "

                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    DataTable dtversion = new DataTable();
                    dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);

                    PriorAuthorizationTransaction oPATransaction = new PriorAuthorizationTransaction();

                    if ((txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != ""))
                    {
                        oPATransaction.PriorAuthorizationID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                        oPATransaction.PriorAuthorizationNo = Convert.ToString(txtPriorAuthorizationNo.Text);
                        oPATransaction.PatientID = oMasterAppointment.PatientID;

                        if (dtversion != null && dtversion.Rows.Count > 0)
                        { oPATransaction.Version = dtversion.Rows[0]["sSettingsValue"].ToString(); }

                        dtversion.Dispose();
                        dtversion = null;
                    }
                    else
                    {
                        oPATransaction.PriorAuthorizationID = 0;
                        oPATransaction.PriorAuthorizationNo = "";
                        oPATransaction.PatientID = oMasterAppointment.PatientID;
                        if (dtversion != null && dtversion.Rows.Count > 0)
                        { oPATransaction.Version = dtversion.Rows[0]["sSettingsValue"].ToString(); }

                        dtversion.Dispose();
                        dtversion = null;
                    }
                    oPATransaction.MasterAppointmentID = _SetAppointmentParameter.MasterAppointmentID;
                    oPATransaction.DetailAppointmentID = _SetAppointmentParameter.AppointmentID;
                    oPATransaction.PriorAuthorizationID = UpdatedPriorAuthorizationID;
                    oPATransaction.IsSingleOccurance = oMasterAppointment.IsRecurrence.GetHashCode();
                    oPATransaction.IsDeleted = IsPARemoved;
                    oPATransaction.IsUpdated = IsPAUpdated;

                    oMasterAppointment.PATransaction = oPATransaction;

                    #endregion

                    oMasterAppointment.Insurances = null;



                    //Checking Block timing 
                    //
                    //

                    gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
                    _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.MasterID, oMasterAppointment.ResourceIDS);

                    
                    gloUserRights.ClsgloUserRights oLocalClsgloUserRights = null;
                    try
                    {
                        oLocalClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                        oLocalClsgloUserRights.CheckForUserRights(_UserName);

                        //_ProviderName = GetProviderName(oMasterAppointment.ASBaseID, _ClinicID);

                        //bool isResourcesBlocked = false;
                        //bool isProviderBlocked = false;
                        DataTable dt;

                        String Location = "";
                        Location = Convert.ToString(cmbApp_Location.Text);
                        if (oMasterAppointment.ASBaseID > 0)
                        {

                            dt = new DataTable();
                            // if ((oClsgloUserRights.OverrideProviderBlockSchedule) && (oMasterAppointment.IsRecurrence == SingleRecurrence.Single))
                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {

                                if (_AppointmentDates.Dates.Count > 0)
                                {
                                    dt = ResourseName(oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.StartDate, Location);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    if (oLocalClsgloUserRights.OverrideProviderBlockSchedule == false)
                                    {
                                        //isProviderBlocked = true;
                                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return 0;
                                    }
                                    else
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.StartTime.ToShortTimeString() + " - " + oMasterAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {

                                        }
                                        else
                                        {
                                            return 0;
                                        }
                                    }
                                }
                            }
                        }


                        if (oMasterAppointment.Resources.Count > 0)
                        {

                            dt = new DataTable();
                            // if ((oClsgloUserRights.OverrideProviderBlockSchedule) && (oMasterAppointment.IsRecurrence == SingleRecurrence.Single))
                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                                {

                                    if (_AppointmentDates.RemoveResourceBlockedSlots(oMasterAppointment.Resources[i].ASCommonID, oMasterAppointment.Resources[i].StartTime, oMasterAppointment.Resources[i].EndTime, oMasterAppointment.StartDate))
                                    {
                                        dt = ResourseName(oMasterAppointment.Resources[i].ASCommonID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.StartDate, Location);
                                    }


                                    if (dt.Rows.Count > 0)
                                    {
                                        if (oLocalClsgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            //isResourcesBlocked = true;
                                            MessageBox.Show("Schedule is blocked for the resource. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return 0;
                                        }
                                        else
                                        {

                                            if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.Resources[i].StartTime.ToShortTimeString() + " - " + oMasterAppointment.Resources[i].EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                            {
                                            }
                                            else
                                            {
                                                return 0;
                                            }
                                        }

                                    }
                                }

                            }
                        }

                    }
                    catch
                    {
                    }
                    finally
                    {
                        try
                        {
                            if (oLocalClsgloUserRights != null)
                            {
                                oLocalClsgloUserRights.Dispose();
                                oLocalClsgloUserRights = null;
                            }
                        }
                        catch
                        {
                        }
                    }


                    //
                    //
                    //End Checking Block timing 






                    #region "Overlap Template Block"

                    //bool UpdateOverlapAppointment = false;

                    if (SetAppointmentParameters.ShowTemplateAppointment_Flag)
                    {
                        if (chkRecurring.Checked == false)
                        {

                            gloSettings.GeneralSettings oSet = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);

                            Int32 TemplateCount;
                            TemplateCount = 0;

                            object _objSettingValue;
                            oSet.GetSetting("OverlapTemplateAppointment", 0, _clinicID, out _objSettingValue);
                            oSet.Dispose();
                            oSet = null;

                            if (_objSettingValue.ToString() == "")
                            {
                                _objSettingValue = "U";
                            }


                                TemplateCount = Convert.ToInt32(GetAppointmentConflictTimeOverlapSet(_SetAppointmentParameter.TemplateAllocationID, Convert.ToInt64(cmbApp_Provider.SelectedValue), Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())), gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID, Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToShortDateString()), Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), SetAppointmentParameters.LocationIDs));
                                if (TemplateCount > 0)
                                {
                                    //DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template appointment slots.  Would you like to schedule all of the template slots this appointment overlaps or would you like to leave a conflict in the schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                    if (_objSettingValue.ToString() == "U")
                                    {
                                        DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template slots. Would you like to use all of the template slots this appointment overlaps?." + "\n\n" + "Yes – Schedule all template slots overlapped by this appointment." + "\n\n" + "No – Only use the existing slot.  A conflict will be created in the appointment schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                        if (dresult == DialogResult.Cancel)
                                        {
                                            return 0;
                                        }
                                        else if (dresult == DialogResult.Yes)
                                        {
                                            ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID, Convert.ToInt64(cmbApp_Provider.SelectedValue), Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
                                                gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                                Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);
                                        }
                                    }
                                    else if (_objSettingValue.ToString() == "Y")
                                    {
                                        ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID, Convert.ToInt64(cmbApp_Provider.SelectedValue), Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                            Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);
                                    }
                                }
                        }
                    }
                    #endregion


                    if (_SetAppointmentParameter != null && _SetAppointmentParameter.LoadParameters == true)
                    {
                        if (_SetAppointmentParameter.ProviderID != oMasterAppointment.ASBaseID)
                        {
                            _SetAppointmentParameter.TemplateAllocationMasterID = 0; 
                            _SetAppointmentParameter.TemplateAllocationID = 0;
                            oMasterAppointment.TempAllocationID = _SetAppointmentParameter.TemplateAllocationID; 
                        }
                        
                        oMasterAppointment.ShowTemplateAppt_Flag = SetAppointmentParameters.ShowTemplateAppointment_Flag;
                        oMasterAppointment.LocationIDs = _SetAppointmentParameter.LocationIDs;
                        oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = _SetAppointmentParameter.AllowRecurrenceInBlocked;
                        _result = ogloAppointment.Add(oMasterAppointment, AppointmentScheduleFlag.TemplateAppointment, _SetAppointmentParameter.TemplateAllocationMasterID, _SetAppointmentParameter.TemplateAllocationID, _SetAppointmentParameter.LineNumber);
                    }
                    else
                    {
                        if (_SetAppointmentParameter != null)
                        {
                            if (_SetAppointmentParameter.ProviderID != oMasterAppointment.ASBaseID)
                            {
                                _SetAppointmentParameter.TemplateAllocationMasterID = 0;
                                _SetAppointmentParameter.TemplateAllocationID = 0;
                                oMasterAppointment.TempAllocationID = _SetAppointmentParameter.TemplateAllocationID;
                            }

                            oMasterAppointment.LocationIDs = _SetAppointmentParameter.LocationIDs;
                            oMasterAppointment.ShowTemplateAppt_Flag = SetAppointmentParameters.ShowTemplateAppointment_Flag;
                            oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = _SetAppointmentParameter.AllowRecurrenceInBlocked;
                        }
                        else
                        {
                            oMasterAppointment.TempAllocationID = 0;
                            oMasterAppointment.LocationIDs = SetAppointmentParameters.LocationIDs;
                            oMasterAppointment.ShowTemplateAppt_Flag = false;
                            oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = false;
                        }
                        _result = ogloAppointment.Add(oMasterAppointment);
                    }
                }

                if (_result > 0)
                {
                    //Audit Trail 
                    AppointmentChanged = true;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.Add, "Appointment added", oMasterAppointment.PatientID, _result, _SetAppointmentParameter.ProviderID, ActivityOutCome.Success);

                    //Prior Authorization

                    #region "Prior Authorization"
                    #endregion
                }

                //gloDatabaseLayer.DBLayer oDBUpdate = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //try
                //{
                //    if ((ProviderID != cmbApp_Provider.SelectedValue.ToString()) || (AppoinmentDate != dtpApp_DateTime_StartDate.Value))
                //    {
                //        oDBUpdate.Connect(false);
                //        oDBUpdate.Execute_Query("UPDATE AS_Appointment_DTL SET  AS_Appointment_DTL.nTemplateAllocationID=" + 0 + ",AS_Appointment_DTL.nTemplateAllocationMasterID=" + 0 + " WHERE nDTLAppointmentID=" + ogloAppointment.DetailAppointmentIDForAuthorization);
                //        oDBUpdate.Execute_Query("UPDATE AB_AppointmentTemplate_Allocation SET nIsRegistered = 0 WHERE nTemplateAllocationMasterID = " + _SetAppointmentParameter.TemplateAllocationMasterID + " AND nTemplateAllocationID = " + _SetAppointmentParameter.TemplateAllocationID + " AND nIsRegistered = 1");
                //        oDBUpdate.Disconnect();
                //    }
                //}
                //catch (gloDatabaseLayer.DBException dbex)
                //{
                //    dbex.ERROR_Log(dbex.ToString());
                //}
                //catch (Exception ex)
                //{
                //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //    ex = null;
                //}
                //finally
                //{
                //    if (oDBUpdate != null) { oDBUpdate.Dispose(); oDBUpdate = null; }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.Add, "Add appointment", 0, 0, 0, ActivityOutCome.Failure);
                _result = 0;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                oMasterAppointment.Dispose();
                oMasterAppointment = null;
                ogloAppointment.Dispose();
                ogloAppointment = null;
            }
            return _result;
        }

        private void CheckPatientInsuranceEligibility(Int64 PatientID)
        {
            gloSettings.GeneralSettings oSettings = null;
            Object objEnableAutoEligibilityAppoinments = null;
            bool bIsEnableAutoEligibilityAppoinments = false;
            DataTable dtPatientIns = null;
            bool bIsPatientInsurancePresent = false;
            gloPatient.gloPatientEiligibility ogloEiligibility = null;
            Int64 _insuranceContactid = 0;
            Int64 _insuranceID = 0;
            try
            {
                ogloEiligibility = new gloPatient.gloPatientEiligibility(_databaseconnectionstring);
                oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("EnableAutoEligibilityAppointment", out objEnableAutoEligibilityAppoinments);
                if (objEnableAutoEligibilityAppoinments == null || objEnableAutoEligibilityAppoinments == "")
                {
                    bIsEnableAutoEligibilityAppoinments = false;
                }
                else
                {
                    bIsEnableAutoEligibilityAppoinments = Convert.ToBoolean(objEnableAutoEligibilityAppoinments);
                }

                //SLR: Free oSettings, _objSresult
                oSettings.Dispose();
                dtPatientIns = ogloEiligibility.GetPatientInsuranceID(PatientID);
                if (dtPatientIns!=null && dtPatientIns.Rows.Count>0)
                {
                    if (dtPatientIns != null && dtPatientIns.Rows.Count > 0)
                    {
                        _insuranceContactid = Convert.ToInt64(dtPatientIns.Rows[0]["nContactID"]);
                        _insuranceID = Convert.ToInt64(dtPatientIns.Rows[0]["nPatientInsuranceID"]);
                    }
                    bIsPatientInsurancePresent = true;
                }
            }
            catch (Exception ex)
            {
                bIsPatientInsurancePresent = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings!=null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
                if (dtPatientIns != null)
                {
                    dtPatientIns.Dispose();
                    dtPatientIns = null;
                }
                if (objEnableAutoEligibilityAppoinments!=null)
                {
                    objEnableAutoEligibilityAppoinments = null;
                }
                if (ogloEiligibility != null) { ogloEiligibility.Dispose(); }
            }

            if (bIsEnableAutoEligibilityAppoinments && bIsPatientInsurancePresent)
            {
                string strMessage = string.Format("Automatic Insurance Eligibility will be perform for patient primary insurance.\nDo you want to continue?\n\nYes: Automatic eligibility check take some time.\nNo:  Skip automatic eligibility check.");
                DialogResult diaResult = MessageBox.Show(strMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (diaResult == DialogResult.No)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.Eligibility, ActivityType.EligibilityCheck, "Appointment Insurance eligibility skipped for Patient: " + Convert.ToString(PatientID), PatientID, 0, 0, ActivityOutCome.Success);
                    return;
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.Eligibility, ActivityType.EligibilityCheck, "Appointment Insurance eligibility started for Patient: " + Convert.ToString(PatientID), PatientID, 0, 0, ActivityOutCome.Success);
                gloPatient.EiligiblityData EData = null;
                Int64 _patientProviderid = 0;
                //Int64 _insuranceContactid = 0;
                //Int64 _insuranceID = 0;

                Object _objResult = null;
                string _result = "";
                int nANSIVersion = 0;

                try
                {
                    //SLR: Free previoisly allocated memory before allocating again or declare locally?
                    ogloEiligibility = new gloPatient.gloPatientEiligibility(_databaseconnectionstring);
                    _patientProviderid = ogloEiligibility.GetPatientProviderID(PatientID);
                    //DataTable dtPatientInsDetails = ogloEiligibility.GetPatientInsuranceID(PatientID);
                    //if (dtPatientInsDetails != null && dtPatientInsDetails.Rows.Count > 0)
                    //{
                    //    _insuranceContactid = Convert.ToInt64(dtPatientInsDetails.Rows[0]["nContactID"]);
                    //    _insuranceID = Convert.ToInt64(dtPatientInsDetails.Rows[0]["nPatientInsuranceID"]);
                    //}



                    this.Cursor = Cursors.WaitCursor;
                    if (Convert.ToString(PatientID).Trim() != "" && Convert.ToString(_patientProviderid).Trim() != ""
                        && Convert.ToString(_insuranceID).Trim() != "" && Convert.ToString(_insuranceContactid).Trim() != "")
                    {
                        EData = ogloEiligibility.GetEiligibilityData(PatientID, _patientProviderid, _insuranceID, _insuranceContactid);

                        oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                        oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                        _result = Convert.ToString(_objResult);

                        nANSIVersion = oSettings.getANSIVersion(_insuranceContactid, "ELIGIBILITY", gloGlobal.gloPMGlobal.ClinicID);
                        //SLR: Free oSettings, _objSresult
                        oSettings.Dispose();
                        if (_objResult != null)
                        {
                            _objResult = null;
                        }

                        if (EData != null)
                        {
                            if (_result == "" || _result == "BYCODE")
                            {
                                if (nANSIVersion == 0)
                                {
                                    MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (nANSIVersion == (int)ANSIVersions.ANSI_4010)
                                {
                                    ogloEiligibility.EDIGeneration_270(EData);
                                }
                                else if (nANSIVersion == (int)ANSIVersions.ANSI_5010)
                                {
                                    ogloEiligibility.EDI5010Generation_270(EData, ANSIVersions.ANSI_5010);
                                }
                            }
                            else if (_result == "BYSERVICE")
                            {

                                if (nANSIVersion == 0)
                                {
                                    MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (nANSIVersion == (Int64)ANSIVersions.ANSI_4010)
                                {
                                    ogloEiligibility.EDIGeneration_270(EData, _insuranceID, _patientProviderid);
                                }
                            }
                        }
                    }
                    if (ogloEiligibility != null) { ogloEiligibility.Dispose(); }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.Eligibility, ActivityType.EligibilityCheck, "Appointment Insurance eligibility completed for Patient: " + Convert.ToString(PatientID), PatientID, 0, 0, ActivityOutCome.Success);

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.Eligibility, ActivityType.EligibilityCheck, "Exception while Appointment Insurance eligibility check." + ex.Message, PatientID, 0, 0, ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    if (EData != null) { EData.Dispose(); }
                }
            }
            else
            {
                if (!bIsPatientInsurancePresent)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Patient not having any insurance, Appoinment Insurance eligibility skipped", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.EligibilityCheck, "Enable Auto Eligibility Appoinments setting: false, Appoinment Insurance eligibility skipped", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                }
            }
        }


        public DataTable ResourseName(Int64 ProviderID, DateTime StartTime, DateTime EndTime, DateTime AppoinmentDate, String Location)
        {
            DataTable dt = new DataTable();

            TimeSpan ts = EndTime - StartTime;
            decimal duration = (decimal)ts.TotalMinutes;


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            Int64 _clinicID = 1;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _clinicID = 1;
                }
            }
            else
            { _clinicID = 1; }

            try
            {
                oDB.Connect(false);

                DateTime dateTime = new DateTime();
                string splitDate = AppoinmentDate.ToShortDateString() + " " + StartTime.ToShortTimeString();
                dateTime = Convert.ToDateTime(splitDate);

                DateTime newDateTime = new DateTime();
                newDateTime = dateTime.AddMinutes((int)duration);
                
                
                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Flag", 2, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("AS_BlockedSlots", oDBParameters, out  dt);
                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

                return dt;

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }
        }



        private bool SaveValidation()
        {
            bool _result = true;
            if (cmbApp_Provider.Text.Trim() == "" && c1Resources.Rows.Count == 1)
            {
                MessageBox.Show("Select provider or resource.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbApp_Provider.Focus();
                return false;
            }

            else if (cmbApp_Location.SelectedIndex < 0)
            {
                MessageBox.Show("Select a location.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
                return _result;
            }

            else if
            (numApp_DateTime_Duration.Value < 1 && chkRecurring.Checked == false)
            {
                MessageBox.Show("Select a valid duration.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                numApp_DateTime_Duration.Focus();
                _result = false;
                return _result;
            }

            else if
            (numRec_DateTime_Duration.Value < 1 && chkRecurring.Checked == true)
            {
                MessageBox.Show("Select a valid duration.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                numRec_DateTime_Duration.Focus();
                _result = false;
                return _result;
            }

            //Patient
            else if (txtApp_Patient.Tag != null)
            {
                if (txtApp_Patient.Tag.ToString() == "")
                {
                    MessageBox.Show("Select a patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _result = false;
                    return _result;
                }
                else
                {
                    if (Convert.ToInt64(txtApp_Patient.Tag.ToString()) == 0)
                    {
                        MessageBox.Show("Select a patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _result = false;
                        return _result;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
                return _result;

            }

            if (txtApp_Patient.Tag != null)
            {
                if (GetPatientStaus(Convert.ToInt64(txtApp_Patient.Tag.ToString())) == "Deceased")
                {
                    MessageBox.Show("The status of the patient is 'Deceased'. Only administrator can modify this patient's information.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtApp_Patient.Focus();
                    _result = false;
                    return _result;
                }
            }

            #region " Problem Type/ Resource Time Validations "

            DateTime dtScheduleStartTime;
            DateTime dtScheduleEndTime;
            if (chkRecurring.Checked == false)
            {
                //dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                dtScheduleStartTime = dtpApp_DateTime_StartTime.Value;
                dtScheduleEndTime = dtpApp_DateTime_EndTime.Value;
            }
            else
            {

                // Commented to solve issue #18719 and written 2 more line to set dtScheduleStartTime and dtScheduleEndTime
             
                // dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                // dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));                
                
                dtScheduleStartTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_StartTime.Value.ToShortDateString() + " " + dtpRec_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtScheduleEndTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_EndTime.Value.ToShortDateString() + " " + dtpRec_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            }

            //Check Problem Type Start & End Time is between Schedule Time 
            for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
            {
                DateTime dtPTStartTime;
                DateTime dtPTEndTime;

                //dtPTStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtPTEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                

                dtPTStartTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtPTEndTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


                if (dtPTStartTime < dtScheduleStartTime || dtPTStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'Start time' must be in 'Appointment time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProviderProblemType.Focus();
                    c1ProviderProblemType.Row = i;
                    c1ProviderProblemType.Col = COL_STARTTIME;
                    return false;
                }
                if (dtPTEndTime < dtScheduleStartTime || dtPTEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'End time' must be in 'Appointment time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProviderProblemType.Focus();
                    c1ProviderProblemType.Row = i;
                    c1ProviderProblemType.Col = COL_ENDTIME;
                    return false;
                }
            }

            //Check Resource Start & End Time is between Schedule Time 
            for (int i = 1; i < c1Resources.Rows.Count; i++)
            {
                DateTime dtRSStartTime;
                DateTime dtRSEndTime;

                //dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                dtRSStartTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtRSEndTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


                if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'Start time' must be in 'Appointment time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resources.Focus();
                    c1Resources.Row = i;
                    c1Resources.Col = COL_STARTTIME;
                    return false;
                }
                if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'End time' must be in 'Appointment time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resources.Focus();
                    c1Resources.Row = i;
                    c1Resources.Col = COL_ENDTIME;
                    return false;
                }

            }
            Int64 _npatientId = Convert.ToInt64(txtApp_Patient.Tag.ToString());
            string _strSQL = "";
            _strSQL = "Select dtDOB from Patient Where npatientID ='" + _npatientId + "'";
            DataTable dtBirthDate = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            oDB.Retrive_Query(_strSQL, out dtBirthDate);
            _strSQL = "";
            oDB.Disconnect();
            if (dtBirthDate != null && dtBirthDate.Rows.Count > 0)
            {
                if (dtpApp_DateTime_StartDate.Value.CompareTo(Convert.ToDateTime(dtBirthDate.Rows[0][0].ToString())) < 0)
                {
                    MessageBox.Show(" Appointment Date must greater than Patient's 'Date of Birth'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpApp_DateTime_StartDate.Focus();
                    return false;
                }
            }
            #endregion


            #region "Clinic Timing validations"

            //Clinic Timing validations
            Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
            Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
            Int32 _nProposedStartTime;
            Int32 _nProposedEndTime;

            if (chkRecurring.Checked == false)
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());
            }
            else
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_EndTime.Value.ToShortTimeString());
            }


            DialogResult _DialogResult = DialogResult.None;


            if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
            {
                _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_DialogResult == DialogResult.No)
                {
                    if (chkRecurring.Checked == false)
                        dtpApp_DateTime_StartTime.Focus();
                    else
                        dtpRec_DateTime_StartTime.Focus();

                    return false;
                }
            }
            else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
            {
                _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_DialogResult == DialogResult.No)
                {
                    if (chkRecurring.Checked == false)
                        dtpApp_DateTime_StartTime.Focus();
                    else
                        dtpRec_DateTime_StartTime.Focus();

                    return false;
                }
            }
            #endregion

            #region Same Date Validation  //Added By MaheshB
            gloAppointment objappoinmnet = new gloAppointment(_databaseconnectionstring);

            Int64 _patientId = Convert.ToInt64(txtApp_Patient.Tag.ToString());
            Int64 _Appointmentcnt;
            if (chkRecurring.Checked == false)
            {

                _Appointmentcnt = objappoinmnet.IsAppointmentOnToday(_patientId, _clinicID, dtpApp_DateTime_StartDate.Value, _PrintAppointmentID);

                if (_Appointmentcnt >= 1)
                {

                    DialogResult dresult = MessageBox.Show("This patient is already scheduled for selected date. Do you want to register a new Appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dresult == DialogResult.No)
                    {
                        return false;
                    }
                }
            }


            #endregion


            if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
            {
                if (!SavePAValidation())
                {
                    return false;
                }
            }

            return _result;
        }

        public object GetModifiedRecurrenceOccurance(long nMstAppointmentid, string dtrecurr, int StartTime, int EndTime)
        {
           
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Count;
            Count = 0;

            oDB.Connect(false);

            oDBParameters.Add("@nMstAppointmentid", nMstAppointmentid, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtrecurr", dtrecurr, ParameterDirection.Input, SqlDbType.Text);
            oDBParameters.Add("@StartTime", StartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", EndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);
            oDB.Execute("ApptGetModifiedRecurrenceOccurance", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            return Count;
        }

        public object GetAppointmentConflictTimeOverlapSet(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, Int64 ClinicID,DateTime Startdate,DateTime StartTime,DateTime EndTime,String LocationIDs = null)
        {
            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }

            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

            DateTime dateTime = new DateTime();
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            dateTime = Convert.ToDateTime(splitDate);

            DateTime newDateTime = new DateTime();
            newDateTime = dateTime.AddMinutes((int)duration);
                

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Count;
            Count = 0;

            oDB.Connect(false);

            oDBParameters.Add("@nTemplateAllocationID", nTemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@providerId", providerId, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtEndDate", Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString())), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@StartTime", dtStartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", dtEndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@LocationIDs", LocationIDs, ParameterDirection.Input, SqlDbType.Text);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);

            oDB.Execute("GetAppointmentConflictTimeOverlapSet", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            return Count;
        }


        public void ApptUpdateOverlapTemplate(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime,DateTime Startdate,DateTime StartTime,DateTime EndTime, Int64 ClinicID)
        {

            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }

            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

            DateTime dateTime = new DateTime();
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            dateTime = Convert.ToDateTime(splitDate);

            DateTime newDateTime = new DateTime();
            newDateTime = dateTime.AddMinutes((int)duration);
            


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);

            oDBParameters.Add("@nTemplateAllocationID", nTemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@providerId", providerId, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@StartTime", dtStartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", dtEndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtEndDate", Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString())), ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Execute("ApptUpdateOverlapTemplate", oDBParameters);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;

        }


        private bool SavePAValidation()
        {
            try
            {
                #region " Get Prior Authorization Details "

                Int64 _paID = 0;

                if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                { _paID = Convert.ToInt64(txtPriorAuthorizationNo.Tag); }

                Int64 _visitsAllowed = 0;
                Int64 _expDate = 0;
                bool _trackLimit = false;

                DataRow _drPAInfo = clsgloPriorAuthorization.GetPriorAuthorizationInfo(_paID);

                if (_drPAInfo != null)
                {
                    _visitsAllowed = Convert.ToInt64(_drPAInfo["nVisitsAllowed"]);
                    _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);

                    if (_trackLimit && Convert.ToString(_drPAInfo["nExpDate"]) != "") { _expDate = Convert.ToInt64(_drPAInfo["nExpDate"]); }
                }

                #endregion

                #region " Visits calculations "

                Int64 _startDate = 0;
                Int64 _endDate = 0;

                Int64 _visitsRequired = 0;
                Int64 _visitsUsed = 0;
                Int64 _visitsRemaining = 0;
                Int64 _visitsUsedTotal = 0;

                if (oFindCriteria != null)
                {
                    if (!chkRecurring.Checked)
                    {
                        _startDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                        _endDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));

                        if (clsgloPriorAuthorization.GetVisitsUsed(_paID, _startDate, _endDate) <= 0)
                        { _visitsRequired += 1; }
                    }
                    else
                    {
                        _startDate = gloDateMaster.gloDate.DateAsNumber(oFindCriteria.Dates[0].ToString());
                        _endDate = gloDateMaster.gloDate.DateAsNumber(oFindCriteria.Dates[oFindCriteria.Dates.Count - 1].ToString());

                        foreach (DateTime _apptDate in oFindCriteria.Dates)
                        {
                            if (clsgloPriorAuthorization.GetVisitsUsed(_paID, gloDateMaster.gloDate.DateAsNumber(_apptDate.ToString("MM/dd/yyyy")), gloDateMaster.gloDate.DateAsNumber(_apptDate.ToString("MM/dd/yyyy"))) <= 0)
                            { _visitsRequired += 1; }
                        }
                    }

                    _visitsUsed = clsgloPriorAuthorization.GetVisitsUsed(_paID, 0, _endDate) + _visitsRequired;
                    _visitsRemaining = clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false) - _visitsRequired;
                    _visitsUsedTotal = _visitsAllowed - _visitsRemaining;
                }
                #endregion

                #region " Warning messages "

                if (_expDate != 0)
                {
                    if (_expDate < _endDate)
                    {
                        if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has expired.\nPrior authorization is valid only until " + gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_drPAInfo["nExpDate"])) + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        { return false; }
                    }
                }
                if (_trackLimit)
                {
                    if (_visitsAllowed != 0)
                    {
                        if (_visitsUsedTotal > _visitsAllowed)
                        {
                            if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has exceeded its # visits allowed.\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            { return false; }
                        }
                        else
                        {
                            string _visit = "visit";
                            if (_visitsRequired > 1) { _visit = _visit + "s"; }

                            if (!_visitsRequired.Equals(0))
                            {
                                if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " will use "
                                    + _visitsRequired + " " + _visit+" out of "+Convert.ToString(clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false)) + " visits.\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                { return false; }
                            }
                        }
                    }
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        private bool UpdatePAValidation()
        {
            try
            {
                #region " Get Prior Authorization Details "

                bool _trackLimit = false;

                Int64 _paID = 0;
                Int64 _visitsAllowed = 0;
                Int64 _expDate = 0;

                if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                {
                    if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                    { _paID = Convert.ToInt64(txtPriorAuthorizationNo.Tag); }
                }
                else
                {
                    if (IsPARemoved && CurrentPriorAuthorizationID != 0)
                    { _paID = CurrentPriorAuthorizationID; }
                }

                DataRow _drPAInfo = clsgloPriorAuthorization.GetPriorAuthorizationInfo(_paID);
                if (_drPAInfo != null)
                {
                    _visitsAllowed = Convert.ToInt64(_drPAInfo["nVisitsAllowed"]);
                    _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);

                    if (_trackLimit)
                    { _expDate = Convert.ToInt64(_drPAInfo["nExpDate"]); }
                }

                #endregion

                #region " Visits calculations "

                Int64 _startDate = 0;
                Int64 _endDate = 0;

                Int64 _visitsRequired = 0;

                Int64 _visitsUsed = 0;
                Int64 _visitsRemaining = 0;
                Int64 _visitsUsedTotal = 0;

                if (oFindCriteria != null)
                {
                    if (_SetAppointmentParameter.ModifyAppointmentMethod == SingleRecurrence.Single)
                    {
                        _startDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                        _endDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));

                        _visitsUsed = clsgloPriorAuthorization.GetVisitsUsedByAppointment(_SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID, _paID);

                        if (clsgloPriorAuthorization.GetVisitsUsed(_paID, _startDate, _endDate) <= 0)
                        { _visitsRequired += 1; }
                    }
                    else
                    {
                        _startDate = gloDateMaster.gloDate.DateAsNumber(oFindCriteria.Dates[0].ToString());
                        _endDate = gloDateMaster.gloDate.DateAsNumber(oFindCriteria.Dates[oFindCriteria.Dates.Count - 1].ToString());

                        _visitsUsed = clsgloPriorAuthorization.GetVisitsUsedByAppointment(_SetAppointmentParameter.MasterAppointmentID, 0, _paID);

                        if (!IsPARemoved)
                        {
                            foreach (DateTime _apptDate in oFindCriteria.Dates)
                            {
                                if (clsgloPriorAuthorization.GetVisitsUsed(_paID, gloDateMaster.gloDate.DateAsNumber(_apptDate.ToString("MM/dd/yyyy")), gloDateMaster.gloDate.DateAsNumber(_apptDate.ToString("MM/dd/yyyy"))) <= 0)
                                { _visitsRequired += 1; }
                            }
                        }
                    }

                    if (IsPARemoved)
                    { _visitsRemaining = clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false) + _visitsUsed; }
                    else
                    { _visitsRemaining = clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false) - _visitsRequired; }

                    _visitsUsedTotal = _visitsAllowed - _visitsRemaining;
                }

                #endregion

                #region " Warning messages "

                if ((_expDate != 0) && (_expDate < _endDate))
                {
                    if (IsPAEntered)
                    {
                        if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has expired.\nPrior authorization is valid only until " + gloDateMaster.gloDate.DateAsDateString(_expDate) + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        { return false; }
                    }
                }

                if (_trackLimit)
                {
                    if (_visitsAllowed != 0)
                    {
                        if (_visitsUsedTotal > _visitsAllowed)
                        {
                            if (IsPAEntered)
                            {
                                if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " has exceeded its # visits allowed.\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                { return false; }
                            }
                        }
                        else
                        {
                            string _visit = "visit";
                            if (_visitsRequired > 1) { _visit = _visit + "s"; }

                            if (!_visitsRequired.Equals(0))
                            {
                                if (MessageBox.Show("Prior authorization " + Convert.ToString(txtPriorAuthorizationNo.Text) + " will use "
                                    + _visitsRequired + " " + _visit + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                { return false; }
                            }
                            else if (IsPARemoved)
                            {
                                string _oldAuthorizationNo = "";

                                DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(CurrentPriorAuthorizationID);
                                if (drPA != null)
                                {
                                    _oldAuthorizationNo = Convert.ToString(drPA["sPriorAuthorizationNo"]);
                                }

                                if (MessageBox.Show("Prior authorization " + _oldAuthorizationNo + " will use -"
                                   + _visitsUsed + " " + _visit + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                { return false; }
                            }
                        }
                    }
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        private bool IsMaximumAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime, Int64 ASBaseID)
        {
            bool _result = false;
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            try
            {
                _result = ogloAppointment.IsMaximumAppointmentRegisterd(dtAppDate, dtAppTime, ASBaseID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        //SHUBHANGI 20100715 RESTRICT TO ADD NEW APPOINTMENT IN THE ALREADY ALLOCATED TIME SPAN 
        private bool IsAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime, Int64 ProviderID, Int64 AppointmentID)
        {
            bool _result = false;
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            try
            {
                _result = ogloAppointment.IsAppointmentRegisterd(dtAppDate, dtAppTime, ProviderID, AppointmentID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }
        //END


        #endregion

        #region "Update Appointments - Modify Appointment"

        private bool UpdateAppointment()
        {
            MasterAppointment oMasterAppointment = new MasterAppointment();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            bool _result = false;
            ASUpdateCriteria _UpdateCriteria = ASUpdateCriteria.None;
            ArrayList _DontDeleteList = new ArrayList();
            Int64 AppProvoderID = 0;
            string tmpProviderID = "";
            try
            {
                

                if (UpdateValidation(out _UpdateCriteria, out _DontDeleteList) == true)
                {
                    #region "License validation"                  
                        if (Convert.ToString(appSettings["ProviderID"]) != "0")
                        { tmpProviderID = ""; }
                        else
                        {
                            if (Convert.ToString(cmbApp_Provider.SelectedValue) != "" && Convert.ToString(cmbApp_Provider.SelectedValue) != "0")
                            {
                                tmpProviderID = cmbApp_Provider.SelectedValue.ToString();
                            }
                            else
                            {
                                using (gloPatient.gloPatientEiligibility oPatientprovider = new gloPatient.gloPatientEiligibility(_databaseconnectionstring))
                                {
                                    tmpProviderID = oPatientprovider.GetPatientProviderID(CurrentPatientID).ToString();
                                }
                            }
                        }
                        if (base.SetChildFormModules("UpdateAppointment", "Modify Appointment", tmpProviderID) == true)
                        {
                            return false;
                        }
                    #endregion ""
                    DialogResult dialog = CheckPriorAuthorization(Convert.ToInt64(txtApp_Patient.Tag.ToString()));
                    if (dialog.ToString() != "OK")
                        return false;

                    #region "Master Appointment Data"

                    //if Modify Appointment and 
                    if (_SetAppointmentParameter != null && _SetAppointmentParameter.LoadParameters == true)
                    {
                        oMasterAppointment.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                    }
                    else
                    {
                        oMasterAppointment.MasterID = 0;
                    }

                    oMasterAppointment.IsRecurrence = SingleRecurrence.Single; if (chkRecurring.Checked == true) { oMasterAppointment.IsRecurrence = SingleRecurrence.Recurrence; }
                    oMasterAppointment.ASFlag = _SetAppointmentParameter.AppointmentFlag;
                    oMasterAppointment.DatesWithCommaSeparator = _SetAppointmentParameter.DatesWithCommaSeperator;
                    oMasterAppointment.AppointmentTypeID = Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                    oMasterAppointment.AppointmentTypeCode = cmbApp_AppointmentType.Text;//Remark
                    oMasterAppointment.AppointmentTypeDesc = cmbApp_AppointmentType.Text;

                    oMasterAppointment.ASBaseID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                    oMasterAppointment.ASBaseCode = cmbApp_Provider.Text; //Remark
                    oMasterAppointment.ASBaseDescription = cmbApp_Provider.Text;
                    if (cmbApp_Provider.Text == "")
                    {
                        oMasterAppointment.ASBaseFlag = ASBaseType.Resource;
                    }
                    else
                    {
                        oMasterAppointment.ASBaseFlag = ASBaseType.Provider;
                    }

                    if (cmbApp_Provider.SelectedIndex > -1)
                    {
                        oMasterAppointment.ReferralProviderID = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        oMasterAppointment.ReferralProviderCode = cmbApp_ReferralDoctor.Text; //Remark
                        oMasterAppointment.ReferralProviderName = cmbApp_ReferralDoctor.Text;
                    }

                    oMasterAppointment.PatientID = Convert.ToInt64(txtApp_Patient.Tag.ToString());

                    if (chkPARequired.Checked == true)
                    { oMasterAppointment.PARequired = true; }
                    else
                    { oMasterAppointment.PARequired = false; }

                    if (chkRecurring.Checked == true)
                    {
                        if (_SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                        {
                            oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;

                            oMasterAppointment.StartDate = dtpApp_DateTime_StartDate.Value;
                            oMasterAppointment.StartTime = dtpApp_DateTime_StartTime.Value;
                            oMasterAppointment.EndDate = dtpApp_DateTime_EndDate.Value;
                            oMasterAppointment.EndTime = dtpApp_DateTime_EndTime.Value;
                            oMasterAppointment.Duration = numApp_DateTime_Duration.Value;
                            oMasterAppointment.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        else
                        {
                            oMasterAppointment.StartDate = dtpRec_Range_StartDate.Value;
                            oMasterAppointment.StartTime = dtpRec_DateTime_StartTime.Value;
                            oMasterAppointment.EndDate = dtpRec_Range_EndBy.Value;
                            oMasterAppointment.EndTime = dtpRec_DateTime_EndTime.Value;
                            oMasterAppointment.Duration = numRec_DateTime_Duration.Value;
                            oMasterAppointment.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                    }
                    else
                    {
                        oMasterAppointment.StartDate = dtpApp_DateTime_StartDate.Value;
                        oMasterAppointment.StartTime = dtpApp_DateTime_StartTime.Value;
                        oMasterAppointment.EndDate = dtpApp_DateTime_EndDate.Value;
                        oMasterAppointment.EndTime = dtpApp_DateTime_EndTime.Value;
                        oMasterAppointment.Duration = numApp_DateTime_Duration.Value;
                        oMasterAppointment.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                    }


                    if (cmbApp_Location.SelectedIndex > -1)
                    {
                        oMasterAppointment.LocationID = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        oMasterAppointment.LocationName = cmbApp_Location.Text;
                    }

                    if (cmbApp_Department.SelectedIndex > -1)
                    {
                        oMasterAppointment.DepartmentID = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        oMasterAppointment.DepartmentName = cmbApp_Department.Text;
                    }

                    ////if(IsNotesPresent(_SetAppointmentParameter.MasterAppointmentID) == false )
                    //{
                    //    if (txtApp_Notes.Text.ToString() != "")
                    //    {
                    //        //DateTime date = DateTime.Now;
                    //        oMasterAppointment.Notes = txtApp_Notes.Text.TrimStart();
                    //    }
                    //    else
                    //    {
                    //        oMasterAppointment.Notes = txtApp_Notes.Text;
                    //    }
                    //}
                   

                    oMasterAppointment.Notes = txtApp_Notes.Text;
                    
                    oMasterAppointment.ClinicID = _SetAppointmentParameter.ClinicID;
                    oMasterAppointment.UsedStatus = (ASUsedStatus)nUsedstatus;
                    if (Convert.ToInt64 (cmbApp_Status.SelectedValue) > 0)
                    {
                        oMasterAppointment.AppointmentStatusID = Convert.ToInt64(cmbApp_Status.SelectedValue);
                    }
                    if (_IsDateChanged == true)
                    {
                        if (oMasterAppointment.UsedStatus == ASUsedStatus.Cancel || oMasterAppointment.UsedStatus == ASUsedStatus.NoShow)
                        { }
                        else
                        {
                            nUsedstatus = 1;
                            oMasterAppointment.UsedStatus = (ASUsedStatus)nUsedstatus;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                    }


                    #endregion

                    #region "Appointment Criteria"
                    //2.1 Criteria Flag

                    if (chkRecurring.Checked == false)
                    {
                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;
                    }
                    else
                    {
                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Recurrence;
                    }

                    //-------------------------------------------------------------
                    //2.2 If single appointment then setup -- 
                    if (chkRecurring.Checked == false)
                    {
                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;
                    }
                    //2.3 If single appointment then setup -- ***RECURRENCE APPOINTMENT***
                    else
                    {
                        if (_SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                        {
                            oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;
                        }
                        else
                        {
                            //2.3.1 Appointment Date & Time
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_StartTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_EndTime.Value.ToString("hh:mm tt"));
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.Duration = numRec_DateTime_Duration.Value;

                            //2.3.2 Recurrence Pattern

                            if (rbRec_Pattern_Daily.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;
                            }
                            else if (rbRec_Pattern_Weekly.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Weekly;
                            }
                            else if (rbRec_Pattern_Monthly.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Monthly;
                            }
                            else if (rbRec_Pattern_Yearly.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Yearly;
                            }


                            //2.3.2.1 Recurrence Pattern - Daily
                            if (rbRec_Pattern_Daily.Checked == true)
                            {
                                if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Value);
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                                }
                                else
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 0;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryWeekday;
                                }
                            }
                            //2.3.2.2 Recurrence Pattern - Weekly
                            else if (rbRec_Pattern_Weekly.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Value);
                                if (ChkRec_Pattern_Weekly_Sunday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = true; }
                                if (ChkRec_Pattern_Weekly_Monday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = true; }
                                if (ChkRec_Pattern_Weekly_Tuesday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = true; }
                                if (ChkRec_Pattern_Weekly_Wednesday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = true; }
                                if (ChkRec_Pattern_Weekly_Thursday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = true; }
                                if (ChkRec_Pattern_Weekly_Friday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = true; }
                                if (ChkRec_Pattern_Weekly_Saturday.Checked == true) { oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = true; }
                            }
                            //2.3.2.3 Recurrence Pattern - Monthly
                            else if (rbRec_Pattern_Monthly.Checked == true)
                            {
                                if (rbRec_Pattern_Monthly_Day.Checked == true)
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Value);
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Value);
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = DayWeekday.day;
                                }
                                else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = 0;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Value);
                                    //GLO2012-0016071 : gloEMR crashes when trying to update the time on a reocurring appointment series
                                    //instead of hashcode values get the respective enum values of selected criteria
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString().GetHashCode();
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (DayWeekday)cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString().GetHashCode();
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString());
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString());
                                }
                            }
                            //2.3.2.4 Recurrence Pattern - Yearly
                            else if (rbRec_Pattern_Yearly.Checked == true)
                            {
                                if (rbRec_Pattern_Yearly_EveryMonthDay.Checked == true)
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Value);
                                    //GLO2012-0016071 : gloEMR crashes when trying to update the time on a reocurring appointment series
                                    //instead of hashcode values get the respective enum values of selected criteria
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString().GetHashCode();
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString());
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;
                                }
                                else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                                {
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                                    //GLO2012-0016071 : gloEMR crashes when trying to update the time on a reocurring appointment series
                                    //instead of hashcode values get the respective enum values of selected criteria
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString().GetHashCode();
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString().GetHashCode();
                                    //oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString().GetHashCode();
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString());
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString());
                                    oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString());
                                }
                            }

                            //--- Recurrence Range
                            if (rbRec_Range_EndAfterOccurence.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndAfterOccurence;
                            }
                            else if (rbRec_Range_EndBy.Checked == true)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate;
                            }
                            else
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndByYear;
                            }

                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy"));
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(numRec_Range_EndAfterOccurence.Value);
                            if (cmbRec_Range_NoEndDateYear.SelectedItem != null)
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(cmbRec_Range_NoEndDateYear.SelectedItem.ToString());
                            }
                            else
                            {
                                oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(DateTime.Now.Year);
                            }
                            //--- Recurrence Range
                        }
                    }
                    //-------------------------------------------------------------

                    #endregion

                    #region "Problem Types"
                    for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterAppointment.IsRecurrence;
                        oShortDetail.PatientID = oMasterAppointment.PatientID;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterAppointment.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.ProblemType;

                        if (chkRecurring.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpApp_DateTime_EndDate.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        oShortDetail.ClinicID = _SetAppointmentParameter.ClinicID;
                        oShortDetail.ViewOtherDetails = "";
                        oShortDetail.UsedStatus = ASUsedStatus.NotUsed;
                        oMasterAppointment.ProblemTypes.Add(oShortDetail);
                    }
                    #endregion

                    #region "Resources"

                   //by pranit on 14 sep 2011 
                    StringBuilder resourceIDS = new StringBuilder("");
                    for (int i = 1; i < c1Resources.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _SetAppointmentParameter.MasterAppointmentID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterAppointment.IsRecurrence;
                        oShortDetail.PatientID = oMasterAppointment.PatientID;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterAppointment.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1Resources.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1Resources.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.Resource;

                        if (chkRecurring.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblRec_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.StartTime = Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME));
                            oShortDetail.EndDate = dtpApp_DateTime_EndDate.Value;
                            oShortDetail.EndTime = Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME));
                            oShortDetail.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        }
                        oShortDetail.ClinicID = _SetAppointmentParameter.ClinicID;
                        oShortDetail.ViewOtherDetails = "";
                        oShortDetail.UsedStatus = ASUsedStatus.NotUsed;

                        oMasterAppointment.Resources.Add(oShortDetail);

                        resourceIDS = resourceIDS.Append(c1Resources.GetData(i, COL_ID).ToString());
                        resourceIDS = resourceIDS.Append(",");   
                    }


                    if (c1Resources.Rows.Count > 1)
                    {
                        resourceIDS = resourceIDS.Remove(resourceIDS.Length - 1, 1);
                        oMasterAppointment.ResourceIDS = resourceIDS;
                    }
                    else
                    {
                        oMasterAppointment.ResourceIDS = new StringBuilder("");
                    }

                    #endregion

                    #region " PA Transaction "


                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    DataTable dtversion = new DataTable();
                    dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);

                    PriorAuthorizationTransaction oPATransaction = new PriorAuthorizationTransaction();

                    if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                    {
                        oPATransaction.PriorAuthorizationID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                        oPATransaction.PriorAuthorizationNo = Convert.ToString(txtPriorAuthorizationNo.Text);
                        oPATransaction.PatientID = oMasterAppointment.PatientID;

                        if (dtversion != null && dtversion.Rows.Count > 0)
                        { oPATransaction.Version = dtversion.Rows[0]["sSettingsValue"].ToString(); }

                        dtversion.Dispose();
                        dtversion = null;
                    }
                    oPATransaction.MasterAppointmentID = _SetAppointmentParameter.MasterAppointmentID;
                    oPATransaction.DetailAppointmentID = _SetAppointmentParameter.AppointmentID;
                    oPATransaction.PriorAuthorizationID = UpdatedPriorAuthorizationID;
                    oPATransaction.IsSingleOccurance = oMasterAppointment.IsRecurrence.GetHashCode();
                    oPATransaction.IsDeleted = IsPARemoved;
                    oPATransaction.IsUpdated = IsPAUpdated;

                    oMasterAppointment.PATransaction = oPATransaction;
                    
                    #endregion

                    if (oMasterAppointment.ASFlag == AppointmentScheduleFlag.TemplateAppointment)
                    {
                        if (oMasterAppointment.UsedStatus == ASUsedStatus.Cancel || oMasterAppointment.UsedStatus == ASUsedStatus.NoShow)
                        { }
                        else
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                        }
                    }
                    else
                    {
                        if (IsMaximumAppointmentRegisterd(oMasterAppointment.StartDate, oMasterAppointment.StartTime, oMasterAppointment.ASBaseID) == false)
                        {
                            if (oMasterAppointment.UsedStatus != ASUsedStatus.CheckIn && oMasterAppointment.UsedStatus != ASUsedStatus.CheckOut)
                            {
                                if (oMasterAppointment.UsedStatus == ASUsedStatus.Cancel || oMasterAppointment.UsedStatus == ASUsedStatus.NoShow)
                                { }
                                else
                                {
                                    oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                                }
                            }
                        }
                        
                        //In case of update record no need to show below message
                        else
                        {
                            //gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                            using (DataTable dtProviderID = ogloAppointment.GetAppointmentProviderID(_SetAppointmentParameter.MasterAppointmentID))
                                {
                                    if (dtProviderID != null && dtProviderID.Rows.Count > 0)
                                    {
                                        AppProvoderID = Convert.ToInt64(dtProviderID.Rows[0]["nASBaseID"]);
                                    }
                                }

                            if (AppProvoderID != oMasterAppointment.ASBaseID)
                                {

                                    DialogResult dgResult = DialogResult.None;
                                    dgResult = MessageBox.Show("All appointments for " + cmbApp_Provider.Text + " are filled for this time.  Do you want to create an additional appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (dgResult == DialogResult.Yes)
                                    {
                                        if (oMasterAppointment.UsedStatus != ASUsedStatus.CheckIn && oMasterAppointment.UsedStatus != ASUsedStatus.CheckOut)
                                        {
                                            oMasterAppointment.UsedStatus = ASUsedStatus.Waiting;
                                        }
                                    }
                                    else if (dgResult == DialogResult.No)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                        }
                    }

                    oMasterAppointment.Insurances = null;


                    //Checking Block timing 
                    //
                    //

                    gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
                    _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.MasterID, oMasterAppointment.ResourceIDS);

                   
                    gloUserRights.ClsgloUserRights oClsLocalgloUserRights = null;
                    try
                    {

                        oClsLocalgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                        oClsLocalgloUserRights.CheckForUserRights(_UserName);

                        DataTable dt;
                        String Location = "";
                        Location = Convert.ToString(cmbApp_Location.Text);
                        if (oMasterAppointment.ASBaseID > 0)
                        {

                            dt = new DataTable();
                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {

                                if (_AppointmentDates.Dates.Count > 0)
                                {
                                    dt = ResourseName(oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.StartDate, Location);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    if (oClsLocalgloUserRights.OverrideProviderBlockSchedule == false)
                                    {
                                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be saved.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return false;
                                    }
                                    else
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.StartTime.ToShortTimeString() + " - " + oMasterAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }


                        if (oMasterAppointment.Resources.Count > 0)
                        {

                            dt = new DataTable();
                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                                {

                                    if (_AppointmentDates.RemoveResourceBlockedSlots(oMasterAppointment.Resources[i].ASCommonID, oMasterAppointment.Resources[i].StartTime, oMasterAppointment.Resources[i].EndTime, oMasterAppointment.StartDate))
                                    {
                                        dt = ResourseName(oMasterAppointment.Resources[i].ASCommonID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.StartDate, Location);
                                    }


                                    if (dt.Rows.Count > 0)
                                    {
                                        if (oClsLocalgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            MessageBox.Show("Schedule is blocked for the resource. Appointment cannot be saved. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return false;
                                        }
                                        else
                                        {
                                            if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.Resources[i].StartTime.ToShortTimeString() + " - " + oMasterAppointment.Resources[i].EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                            {
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {

                        try
                        {
                            if (oClsLocalgloUserRights != null)
                            {
                                oClsLocalgloUserRights.Dispose();
                                oClsLocalgloUserRights = null;
                            }
                        }
                        catch
                        {
                        }

                    }

                    //
                    //
                    //End Checking Block timing 



                    //Checking if one of the occurence modified or not in recurrence series.
                    //By Amit on 23/05/2012
                    if (chkRecurring.Checked == true)
                    {
                        string dtrecurr = "";

                        for (int i = 0; i <= _AppointmentDates.Dates.Count - 1; i++)
                        {
                            if (dtrecurr == "")
                            {
                                dtrecurr =  Convert.ToString (gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[i].ToString ()));
                            }
                            else
                            {
                                dtrecurr = dtrecurr + "," + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[i].ToString()));
                            }
                        }

                        int recrrStartTime = 0;
                        int recrrEndTime = 0;

                        //recrrStartTime = Convert.ToString (gloDateMaster.gloDate.DateTimeAsNumber(oMasterAppointment.StartTime.ToString ()));
                        //recrrEndTime = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()));

                        recrrStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        recrrEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));



                        Int32 RecurrCount = 0;
                        RecurrCount = Convert.ToInt32(GetModifiedRecurrenceOccurance(_SetAppointmentParameter.MasterAppointmentID, dtrecurr, recrrStartTime, recrrEndTime));

                        if (RecurrCount > 0)
                        {
                             DialogResult reccresult =  MessageBox.Show("Some occurrences of this appointment have been changed.  By saving this appointment series, you will undo those changes.  Would you like to proceed?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) ;

                             if (reccresult == DialogResult.No)
                                {
                                    return false;
                                }
                         
                        }

                    }

                //--------------------------------------------------------------------------------
                // Add all validation or conditions before this three comment line
                //--------------------------------------------------------------------------------

                #region "Overlap Template Block"

                gloSettings.GeneralSettings oSet = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                object _objSettingValue;
                oSet.GetSetting("OverlapTemplateAppointment", 0, _clinicID, out _objSettingValue);
                oSet.Dispose();
                oSet = null;
               
                if (Convert.ToString(_objSettingValue) == "")
                {
                    _objSettingValue = "U";
                }

                if (Convert.ToString(_objSettingValue) == "U")
                  {

                    bool UpdateOverlapAppointment = false;
                    Int32 TemplateCount;
                    TemplateCount = 0;

                    if (SetAppointmentParameters.ShowTemplateAppointment_Flag)
                    {
                        if (chkRecurring.Checked == false)
                        {

                            TemplateCount = 0;



                            TemplateCount = Convert.ToInt32(GetAppointmentConflictTimeOverlapSet(
                                                                            _SetAppointmentParameter.TemplateAllocationID,
                                                                            Convert.ToInt64(cmbApp_Provider.SelectedValue),
                                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
                                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
                                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID,
                                                                            Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToShortDateString()),
                                                                            Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
                                                                            Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                                                            SetAppointmentParameters.LocationIDs));

                            if (TemplateCount > 0)
                            {
                                //   DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template appointment slots.  Would you like to schedule all of the template slots this appointment overlaps or would you like to leave a conflict in the schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template slots. Would you like to use all of the template slots this appointment overlaps?." + "\n\n" + "Yes – Schedule all template slots overlapped by this appointment." + "\n\n" + "No – Only use the existing slot.  A conflict will be created in the appointment schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                if (dresult == DialogResult.Cancel)
                                {
                                    return false;
                                }
                                else if (dresult == DialogResult.Yes)
                                {
                                    UpdateOverlapAppointment = true;
                                }

                            }
                        }
                        
                        }



                    if (chkRecurring.Checked == false)
                    {
                            if (UpdateOverlapAppointment)
                            {

                                // if modify the appointment time then it change the template status between original time period
                                if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                {
                                    ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);
                                }

                                else if (oMasterAppointment.ASBaseID != _SetAppointmentParameter.ProviderID)
                                {
                                    ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);
                                }

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID,
                                                            Convert.ToInt64(cmbApp_Provider.SelectedValue),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                                            Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);
                            }
                            else if (((_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                        && TemplateCount == 0 && Convert.ToInt32(ApptFindCountOfOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID)) > 0)
                                        //|| _SetAppointmentParameter.ProviderID != Convert.ToInt64(cmbApp_Provider.SelectedValue)
                                        )
                            {
                                ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID,
                                                            Convert.ToInt64(cmbApp_Provider.SelectedValue),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                                            Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToShortTimeString()),
                                                            _clinicID);
                            }
                            else if ((_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value) && TemplateCount > 0)
                            {
                                ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);
                            }
                            else if (oMasterAppointment.ASBaseID != _SetAppointmentParameter.ProviderID)
                            {
                                    ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);
                            }
                    }

                  }
                    #endregion

                
                // ADD PARAMETERS TO oMasterAppointment by Pranit on 30 jan 2012
                if (_SetAppointmentParameter != null && _SetAppointmentParameter.LoadParameters == true)
                {
                    oMasterAppointment.TempAllocationID = _SetAppointmentParameter.TemplateAllocationID;
                    oMasterAppointment.ShowTemplateAppt_Flag = SetAppointmentParameters.ShowTemplateAppointment_Flag;
                    oMasterAppointment.LocationIDs = _SetAppointmentParameter.LocationIDs;
                    oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = _SetAppointmentParameter.AllowRecurrenceInBlocked;
                }
                else
                {
                    if (_SetAppointmentParameter != null)
                    {
                        oMasterAppointment.TempAllocationID = _SetAppointmentParameter.TemplateAllocationID;
                        oMasterAppointment.LocationIDs = _SetAppointmentParameter.LocationIDs;
                        oMasterAppointment.ShowTemplateAppt_Flag = SetAppointmentParameters.ShowTemplateAppointment_Flag;
                        oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = _SetAppointmentParameter.AllowRecurrenceInBlocked;
                    }
                    else
                    {
                        oMasterAppointment.TempAllocationID = 0;
                        oMasterAppointment.LocationIDs = SetAppointmentParameters.LocationIDs;
                        oMasterAppointment.ShowTemplateAppt_Flag = false;
                        oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment = false;
                    }
                }
                // End by Pranit on 30 jan 2012



                    Int64 MstAppointmentID;
                    MstAppointmentID = 0;

                    MstAppointmentID = ogloAppointment.Modify(oMasterAppointment, _SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID, _SetAppointmentParameter.ClinicID, _SetAppointmentParameter.ModifyAppointmentMethod, _SetAppointmentParameter.ModifyMasterAppointmentMethod, _SetAppointmentParameter.ModifySingleAppointmentFromReccurence, _UpdateCriteria, _DontDeleteList);
                    //_result = ogloAppointment.Modify(oMasterAppointment, _SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID, _SetAppointmentParameter.ClinicID, _SetAppointmentParameter.ModifyAppointmentMethod, _SetAppointmentParameter.ModifyMasterAppointmentMethod, _SetAppointmentParameter.ModifySingleAppointmentFromReccurence, _UpdateCriteria, _DontDeleteList);



                    if (MstAppointmentID > 0)
                    {
                        _SetAppointmentParameter.MasterAppointmentID = MstAppointmentID;
                        _result = true;
                    }

                    gloDatabaseLayer.DBLayer oDBUpdate = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    try
                    {
                        if (oMasterAppointment.IsRecurrence != SingleRecurrence.Single)
                        {
                            if ((ProviderID != cmbApp_Provider.SelectedValue.ToString()) || (AppoinmentDate != dtpApp_DateTime_StartDate.Value))
                            {
                                oDBUpdate.Connect(false);

                                string _strSQL = "UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered=0 from AS_Appointment_DTL join AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID where  AB_AppointmentTemplate_Allocation.nTemplateAllocationID=AS_Appointment_DTL.nTemplateAllocationID AND nIsRegistered = 1 AND AS_Appointment_DTL.nDTLAppointmentID=" + _SetAppointmentParameter.AppointmentID;
                                oDBUpdate.Execute_Query(_strSQL);
                                oDBUpdate.Execute_Query("UPDATE AS_Appointment_DTL SET  AS_Appointment_DTL.nTemplateAllocationID=" + 0 + ",AS_Appointment_DTL.nTemplateAllocationMasterID=" + 0 + " WHERE nDTLAppointmentID=" + _SetAppointmentParameter.AppointmentID);
                                oDBUpdate.Disconnect();
                            }
                        }
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null;
                    }
                    finally
                    {
                        if (oDBUpdate != null) { oDBUpdate.Dispose(); oDBUpdate = null; }
                    }
                }

                if (_result == true)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.Modify, "Appointment modified", oMasterAppointment.PatientID, _SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.ProviderID, ActivityOutCome.Success);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                oMasterAppointment.Dispose();
                oMasterAppointment = null;
                ogloAppointment.Dispose();
                ogloAppointment = null;
            }
            return _result;
        }

        private bool UpdateValidation(out ASUpdateCriteria UpdateCriteria, out ArrayList DontDeleteList)
        {

            bool _result = true;
            ASUpdateCriteria _UpdateCriteria = ASUpdateCriteria.None;
            ArrayList _DontDeleteList = new ArrayList();

            if (cmbApp_Provider.Text.Trim() == "" && c1Resources.Rows.Count == 1)
            {
                MessageBox.Show("Select provider or resource.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbApp_Provider.Focus();
                _result = false;
            }

            //Location
            else if (cmbApp_Location.SelectedIndex < 0)
            {
                MessageBox.Show("Select location.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;

            }

            //Time
            else if
            (numApp_DateTime_Duration.Value < 1)
            {
                MessageBox.Show("Select valid duration.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
            }

            //Patient
            else if (txtApp_Patient.Tag != null)
            {
                if (txtApp_Patient.Tag.ToString() == "")
                {
                    MessageBox.Show("Select patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _result = false;
                }
                else
                {
                    if (Convert.ToInt64(txtApp_Patient.Tag.ToString()) == 0)
                    {
                        MessageBox.Show("Select patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _result = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
            }

            if (txtApp_Patient.Tag != null)
            {
                if (GetPatientStaus(Convert.ToInt64(txtApp_Patient.Tag.ToString())) == "Deceased")
                {
                    MessageBox.Show("The status of the patient is 'Deceased'. Only administrator can modify this patient's information.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtApp_Patient.Focus();
                    _result = false;
                }
            }
            //If modify patient appointment and change the patient, who already has an appointment for that date then message is not prompting.
            gloAppointment objappoinmnet = new gloAppointment(_databaseconnectionstring);

            Int64 _patientId = 0;
            if (txtApp_Patient.Text != "")
            {
                _patientId = Convert.ToInt64(txtApp_Patient.Tag.ToString());
            }


            Int64 _Appointmentcnt;

            if (_nPatientId != _patientId) //THIS CONDITION IS ADDED TO AVOID THIS MESSAGE BOX IF WE OPEN APPOINTMENT FOR MODIFY & DO NOT CHANGE THE PATIENT
            {
                _Appointmentcnt = objappoinmnet.IsAppointmentOnToday(_patientId, _clinicID, dtpApp_DateTime_StartDate.Value, _PrintAppointmentID);

                if (_Appointmentcnt > 1)
                {
                    DialogResult dresult = MessageBox.Show("This patient is already scheduled for selected date. Do you want to register a new Appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (dresult == DialogResult.No)
                    {
                        _result = false;
                    }
                }
            }

            #region "Appointment Conflict"
            Int64 _nResourceID = 0;
            Int64 _nProviderID = 0;
            string _sResource = string.Empty;
            string _sProvider = string.Empty;

            if (_SetAppointmentParameter.AppointmentID <= 0)
            {
                if (IsAppointmentRegisterd(Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToString()), Convert.ToInt64(cmbApp_Provider.SelectedValue), _SetAppointmentParameter.MasterAppointmentID) == true)
                {
                    DialogResult dgResult = DialogResult.None;
                    dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dgResult == DialogResult.No)
                    {
                        _result = false;
                    }
                }


            }
            else if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.ProviderID != Convert.ToInt64(cmbApp_Provider.SelectedValue) || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
            {
                for (int i = 1; i < c1Resources.Rows.Count; i++)
                {
                    _nResourceID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString());
                    if (IsAppointmentRegisterd(Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToString()), _nResourceID, _SetAppointmentParameter.MasterAppointmentID) == true)
                    {
                        _sResource = Convert.ToString(c1Resources.GetData(i, COL_DESC).ToString());
                        break;
                    }
                }


                _nProviderID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                if (IsAppointmentRegisterd(Convert.ToDateTime(dtpApp_DateTime_StartTime.Value.ToString()), Convert.ToDateTime(dtpApp_DateTime_EndTime.Value.ToString()), _nProviderID, _SetAppointmentParameter.MasterAppointmentID) == true)
                {
                    _sProvider = cmbApp_Provider.Text;
                }

                DialogResult dgResult = DialogResult.None;
                if (_sProvider != "")
                {
                    dgResult = MessageBox.Show("Warning –  " + cmbApp_Provider.Text + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                }
                else if (_sResource != "")
                {
                    dgResult = MessageBox.Show("Warning –  " + _sResource + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                }
                if (dgResult == DialogResult.No)
                {
                    _result = false;
                }
            }

            #endregion "Appointment Conflict"

            #region " Problem Type/ Resource Time Validations "

            DateTime dtScheduleStartTime;
            DateTime dtScheduleEndTime;
            if (chkRecurring.Checked == false || _SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
            {
                //dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                dtScheduleStartTime = dtpApp_DateTime_StartTime.Value;
                dtScheduleEndTime = dtpApp_DateTime_EndTime.Value;


            }
            else
            {
                //dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                               
               dtScheduleStartTime = dtpRec_DateTime_StartTime.Value;
               dtScheduleEndTime = dtpRec_DateTime_EndTime.Value;             

            }

            //Check Problem Type Start & End Time is between Schedule Time 
            for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
            {
                DateTime dtPTStartTime;
                DateTime dtPTEndTime;

                // Added if condition  "if (chkRecurring.Checked == true)"  by pranit on 20 feb 2012
                // Changed Above condition by pranit on 29 feb 2012 TO SOLVE ISSUE #21679
                if (chkRecurring.Checked == false || _SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                {
                    dtPTStartTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtPTEndTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));                    
                }
                else
                {
                    dtPTStartTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtPTEndTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                }


                //dtPTStartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME).ToString ());
                //dtPTEndTime =  Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME).ToString ());


                if (dtPTStartTime < dtScheduleStartTime || dtPTStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'Start time' must be in 'Appointment time'.   ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProviderProblemType.Focus();
                    c1ProviderProblemType.Row = i;
                    c1ProviderProblemType.Col = COL_STARTTIME;
                    _result = false;
                    break;
                }
                if (dtPTEndTime < dtScheduleStartTime || dtPTEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'End time' must be in 'Appointment time'.   ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProviderProblemType.Focus();
                    c1ProviderProblemType.Row = i;
                    c1ProviderProblemType.Col = COL_ENDTIME;
                    _result = false;
                    break;
                }
            }

            //Check Resource Start & End Time is between Schedule Time 
            for (int i = 1; i < c1Resources.Rows.Count; i++)
            {
                DateTime dtRSStartTime;
                DateTime dtRSEndTime;

                //              dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //              dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


                // Added if condition  "if (chkRecurring.Checked == true)"  by pranit on 20 feb 2012
                // Changed Above condition by pranit on 29 feb 2012 TO SOLVE ISSUE #21679
                if (chkRecurring.Checked == false || _SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                {
                    dtRSStartTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtRSEndTime = Convert.ToDateTime(String.Format(dtpApp_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));                   
                }
                else
                {
                    dtRSStartTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_StartTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtRSEndTime = Convert.ToDateTime(String.Format(dtpRec_DateTime_EndTime.Value.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                }

                if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'Start time' must be in 'Appointment time'.   ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resources.Focus();
                    c1Resources.Row = i;
                    c1Resources.Col = COL_STARTTIME;
                    _result = false;
                    break;
                }
                if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'End time' must be in 'Appointment time'.   ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resources.Focus();
                    c1Resources.Row = i;
                    c1Resources.Col = COL_ENDTIME;
                    _result = false;
                    break;
                }
            }
            if (txtApp_Patient.Tag != null)
            {
                Int64 _npatientId = Convert.ToInt64(txtApp_Patient.Tag.ToString());
                string _strSQL = "";
                _strSQL = "Select dtDOB from Patient Where npatientID ='" + _npatientId + "'";
                DataTable dtBirthDate = new DataTable();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                oDB.Retrive_Query(_strSQL, out dtBirthDate);
                _strSQL = "";
                oDB.Disconnect();
                if (dtBirthDate != null && dtBirthDate.Rows.Count > 0)
                {
                    if (dtpApp_DateTime_StartDate.Value.CompareTo(Convert.ToDateTime(dtBirthDate.Rows[0][0].ToString())) < 0)
                    {
                        MessageBox.Show(" Appointment Date must greater than Patient's 'Date of Birth'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpApp_DateTime_StartDate.Focus();
                        _result = false;
                    }
                }
            }

            #endregion

            #region "Clinic Timing validations"

            //Clinic Timing validations
            Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
            Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
            Int32 _nProposedStartTime;
            Int32 _nProposedEndTime;

            if (chkRecurring.Checked == false || _SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());
            }
            else
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_EndTime.Value.ToShortTimeString());
            }


            DialogResult _DialogResult = DialogResult.None;

            if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
            {
                //IF CONDITION IS ADDED TO RESOLVED ISSUE THAT THIS PATIENT IS ALREADY HAVING AN APPOINTMENT & IF WE SELECT NO I SHOULD NOT SHOW ANOTHER MESSAGE
                if (_result == true)
                {
                    _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (_DialogResult == DialogResult.No)
                    {
                        if (chkRecurring.Checked == false)
                            dtpApp_DateTime_StartTime.Focus();
                        else
                            dtpRec_DateTime_StartTime.Focus();

                        _result = false;
                    }
                }
            }
            else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
            {
                _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_DialogResult == DialogResult.No)
                {
                    if (chkRecurring.Checked == false)
                        dtpApp_DateTime_StartTime.Focus();
                    else
                        dtpRec_DateTime_StartTime.Focus();

                    _result = false;
                }
            }
            #endregion



            if (_SetAppointmentParameter.ModifyAppointmentMethod == SingleRecurrence.Recurrence)
            {
                gloAppointment oUsedAppointment = new gloAppointment(_databaseconnectionstring);
                gloAppointmentScheduling.AppointmentSchedules oUsedList = new AppointmentSchedules();

                oUsedList = oUsedAppointment.GetUsedAppointment(_SetAppointmentParameter.MasterAppointmentID, 0, _SetAppointmentParameter.ClinicID, ASUsedStatus.CheckIn);
                if (oUsedList.Count > 0)
                {
                    frmSetupUsedAppSch ofrmSetupUsedAppSch = new frmSetupUsedAppSch();
                    ofrmSetupUsedAppSch.oUsedAppTrue_SchFalse = true;
                    ofrmSetupUsedAppSch.oUsedList = oUsedList;
                    ofrmSetupUsedAppSch.ShowDialog(this);
                    _UpdateCriteria = ofrmSetupUsedAppSch.oDialogResult;
                    _DontDeleteList = ofrmSetupUsedAppSch.oDialogDontDeleteIDsList;
                    ofrmSetupUsedAppSch.Dispose();
                    ofrmSetupUsedAppSch = null;
                }
                oUsedAppointment.Dispose();
                oUsedAppointment = null;

                oUsedList.Dispose();
                oUsedList = null;

                if (_UpdateCriteria == ASUpdateCriteria.CancelSave)
                {
                    _result = false;
                }
                else
                {
                    DontDeleteList = _DontDeleteList;
                    UpdateCriteria = _UpdateCriteria;
                }
            }

            DontDeleteList = _DontDeleteList;
            UpdateCriteria = _UpdateCriteria;

            if (!UpdatePAValidation())
            {
                return false;
            }


            //#region "Overlap Template Block"

            //bool UpdateOverlapAppointment = false;
            //Int32 TemplateCount;
            //TemplateCount = 0;

            //if (SetAppointmentParameters.ShowTemplateAppointment_Flag)
            //{
            //    if (chkRecurring.Checked == false)
            //    {

            //        gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);


            //        TemplateCount = 0;

            //        object _objSettingValue;
            //        oSetting.GetSetting("OverlapTemplateAppointment", 0, _clinicID, out _objSettingValue);
            //        oSetting.Dispose();
            //        oSetting = null;

            //        if (Convert.ToInt16(_objSettingValue) == 0)
            //        {
            //            TemplateCount = Convert.ToInt32(GetAppointmentConflictTimeOverlapSet(
            //                                                            _SetAppointmentParameter.TemplateAllocationID, 
            //                                                            Convert.ToInt64(cmbApp_Provider.SelectedValue), 
            //                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())), 
            //                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()), 
            //                                                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID));
            //            if (TemplateCount > 0)
            //            {
            //                DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template appointment slots.  Would you like to schedule all of the template slots this appointment overlaps or would you like to leave a conflict in the schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            //                if (dresult == DialogResult.Cancel)
            //                {
            //                    _result = false;
            //                }
            //                else if (dresult == DialogResult.Yes)
            //                {
            //                    UpdateOverlapAppointment = true;
            //                }

            //            }
            //        }
            //    }
            //}
            //#endregion



            ////--------------------------------------------------------------------------------
            //// Add all validation or conditions before this three comment line
            ////--------------------------------------------------------------------------------

            //if (chkRecurring.Checked == false)
            //{

            //    if (_result)
            //    {

            //        if (UpdateOverlapAppointment)
            //        {


            //            // if modify the appointment time then it change the template status between original time period
            //            if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
            //            {
            //                ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);

            //            }

            //            //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
            //            //appointment block then those block updates
            //            //updating nIsRegistered = 2
            //            ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID,
            //                                        Convert.ToInt64(cmbApp_Provider.SelectedValue),
            //                                        Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
            //                                        gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
            //                                        gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);
            //        }
            //        else if (((_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
            //                    && TemplateCount == 0 && Convert.ToInt32(ApptFindCountOfOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID)) > 0)
            //                    || _SetAppointmentParameter.ProviderID != Convert.ToInt64(cmbApp_Provider.SelectedValue)
            //                    )
            //        {


            //            // if modify the appointment time then it change the template status between original time period
            //            //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
            //            //{
            //            ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);

            //            //}

            //            //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
            //            //appointment block then those block updates
            //            //updating nIsRegistered = 2
            //            ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID,
            //                                        Convert.ToInt64(cmbApp_Provider.SelectedValue),
            //                                        Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
            //                                        gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
            //                                        gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);
            //        }
            //        else if ((_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value) && TemplateCount > 0)
            //        {
            //            // if modify the appointment time then it change the template status between original time period
            //            //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
            //            //{
            //            ApptRemovePreviousOverlapTemplate(_SetAppointmentParameter.MasterAppointmentID);

            //            //}

            //            //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
            //            //appointment block then those block updates
            //            //updating nIsRegistered = 2
            //            //ApptUpdateOverlapTemplate(_SetAppointmentParameter.TemplateAllocationID,
            //            //                            Convert.ToInt64(cmbApp_Provider.SelectedValue),
            //            //                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString())),
            //            //                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString()),
            //            //                            gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString()), _clinicID);

            //        }

            //    }
            //}

            return _result;
        }

        #endregion


        public void ApptRemovePreviousOverlapTemplate(Int64 nMSTAppointmentID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);

            oDBParameters.Add("@nMSTAppointmentID", nMSTAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
            oDB.Execute("ApptRemovePreviousOverlapTemplate", oDBParameters);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;

        }

        public object ApptFindCountOfOverlapTemplate(Int64 nMSTAppointmentID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            object Count;
            Count = 0;

            oDB.Connect(false);

            oDBParameters.Add("@nMSTAppointmentID", nMSTAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);

            oDB.Execute("ApptFindCountOfOverlapTemplate", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;

            return Count;

        }


        #region "Date/Time Event for Simple and Recurring Panel"
        #region "Leave Validation"
        private void dtpApp_DateTime_StartDate_Leave(object sender, EventArgs e)
        {
            SetAppointmentParameter oAppParameters = new SetAppointmentParameter();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            string AppointmentStatus = "";

            if (dtpApp_DateTime_StartTime.Value > Convert.ToDateTime("12/31/2100"))
            {
                MessageBox.Show("Check the date it should not be greater than 12/31/2100.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpApp_DateTime_StartDate.Value = DateTime.Now;
            }


            if (ogloAppointment.IsPatientCheckOut(_MasterAppointmentId, _DetailAppointmentId) == true)
            {
                AppointmentStatus = "checked out";
            }
            else if (ogloAppointment.IsPatientCheckIn(_MasterAppointmentId, _DetailAppointmentId) == true)
            {
                AppointmentStatus = "checked in";
            }
            if (AppointmentStatus == "checked in" || AppointmentStatus == "checked out")
            {
                if (gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                {
                    if (MessageBox.Show("You have changed the appointment date for this appointment to " + dtpApp_DateTime_StartTime.Value.ToShortDateString() + ".The appointment status will be reset and the appointment will no longer be " + AppointmentStatus + "."
                                     + Environment.NewLine + "Are you sure you want to move this appointment to " + dtpApp_DateTime_StartTime.Value.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        _SetAppointmentParameter.AddTrue_ModifyFalse_Flag = false;
                        _IsDateChanged = true;
                    }
                    else
                    {
                        dtpApp_DateTime_StartDate.Value = DateTime.Now;
                        _IsDateChanged = false;
                        return;
                    }
                }
                else
                {
                    _IsDateChanged = false;
                }
            }
        }

        private void dtpRec_Range_StartDate_Leave(object sender, EventArgs e)
        {
            if (dtpRec_Range_StartDate.Value > Convert.ToDateTime("12/31/2100"))
            {
                MessageBox.Show("Check the date it should not be greater than 12/31/2100.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpRec_Range_StartDate.Value = DateTime.Now;
                dtpRec_Range_EndBy.Value = DateTime.Now;
            }
        }

        private void dtpRec_Range_EndBy_Leave(object sender, EventArgs e)
        {
            if (dtpRec_Range_EndBy.Value > Convert.ToDateTime("12/31/2100"))
            {
                MessageBox.Show("Check the date it should not be greater than 12/31/2100.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpRec_Range_EndBy.Value = DateTime.Now;
            }
        }
        #endregion

        #region "Appointment Panel"
        private void dtpApp_DateTime_StartDate_ValueChanged(object sender, EventArgs e)
        {
            string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
            dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
            dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

            dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
            dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));

            ValidateResourceTime();
        }

        private void dtpApp_DateTime_StartTime_ValueChanged(object sender, EventArgs e)
        {
            string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
            dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
            dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

            dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
            dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));

            ValidateResourceTime();
        }

        private void numApp_DateTime_Duration_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numApp_DateTime_Duration.Value) * 600000000);

            string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
            dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
            dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

            dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));
            dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

            //IF APPOINTMENT IS GOING TO NEXT DAY OR IF THE END TIME IS " 12.00 AM " THEN MAKE IT AS "11.59 AM" & REGISTER APPOINTMENT LIKE LOGIC OF 720
            if (Convert.ToString(dtpApp_DateTime_EndDate.Value.TimeOfDay) == "00:00:00")
            { numApp_DateTime_Duration.Value = numApp_DateTime_Duration.Value - 1; }

            if (numApp_DateTime_Duration.Value >= 720)
            {
                chkApp_DateTime_IsAllDayEvent.Checked = true;
            }
            ValidateResourceTime();
        }

        private void chkApp_DateTime_IsAllDayEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkApp_DateTime_IsAllDayEvent.Checked == true)
                {
                    SetAppointmentParameters.StartTime = Convert.ToDateTime(dtpApp_DateTime_StartTime.Value);
                    dtpApp_DateTime_StartTime.Value = _dtClinicStartTime;
                    dtpApp_DateTime_EndTime.Value = _dtClinicEndTime;
                    numApp_DateTime_Duration.Value = Convert.ToDecimal(((TimeSpan)_dtClinicEndTime.Subtract(_dtClinicStartTime)).TotalMinutes);
                    dtpApp_DateTime_StartTime.Enabled = false;
                    dtpApp_DateTime_EndTime.Enabled = false;
                    numApp_DateTime_Duration.Enabled = false;
                }
                else
                {
                    dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(dtpApp_DateTime_StartDate.Value.Date.ToShortDateString() + " " + SetAppointmentParameters.StartTime.ToShortTimeString());
                    //numApp_DateTime_Duration.Value = 15;
                    dtpApp_DateTime_StartTime.Enabled = true;
                    dtpApp_DateTime_EndTime.Enabled = true;
                    numApp_DateTime_Duration.Enabled = true;
                    cmbApp_AppointmentType_SelectionChangeCommitted(null, null);
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        #endregion

        #region "Recurrence Panel"

        #endregion

        #region "Pattern - Date & Time"
        private void dtpRec_DateTime_StartTime_ValueChanged(object sender, EventArgs e)
        {
            dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numRec_DateTime_Duration.Value));
            ValidateResourceTime();
        }

        private void dtpRec_DateTime_EndTime_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpRec_DateTime_EndTime.Value >= dtpRec_DateTime_StartTime.Value)
            //{


            //    TimeSpan _tempTS = new TimeSpan();
            //    _tempTS = dtpRec_DateTime_EndTime.Value.TimeOfDay - dtpRec_DateTime_StartTime.Value.TimeOfDay;


            //    if (_tempTS.TotalMinutes >= Convert.ToDouble(numRec_DateTime_Duration.Minimum) && Convert.ToDouble(_tempTS.TotalMinutes) <= Convert.ToDouble(numRec_DateTime_Duration.Maximum))
            //    {
            //        numRec_DateTime_Duration.Value = Math.Round(Convert.ToDecimal(_tempTS.TotalMinutes), 0);
            //    }
            //    else
            //    {
            //        dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(5));
            //    }
            //}
            //else
            //{
            //    dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(5));
            //}
            ValidateResourceTime();
        }

        private void numRec_DateTime_Duration_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numRec_DateTime_Duration.Value) * 600000000);
            dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));
            ValidateResourceTime();
        }

        private void chkRec_DateTime_IsAllDayEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRec_DateTime_IsAllDayEvent.Checked == true)
            {
                dtpRec_DateTime_StartTime.Value = _dtClinicStartTime;
                dtpRec_DateTime_EndTime.Value = _dtClinicEndTime;
                numRec_DateTime_Duration.Value = Convert.ToDecimal(((TimeSpan)_dtClinicEndTime.Subtract(_dtClinicStartTime)).TotalMinutes);
                dtpRec_DateTime_StartTime.Enabled = false;
                dtpRec_DateTime_EndTime.Enabled = false;
                numRec_DateTime_Duration.Enabled = false;
            }
            else
            {
                dtpRec_DateTime_StartTime.Value = Convert.ToDateTime(dtpRec_Range_StartDate.Value.Date.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(15);
                dtpRec_DateTime_StartTime.Enabled = true;
                dtpRec_DateTime_EndTime.Enabled = true;
                numRec_DateTime_Duration.Enabled = true;
            }
        }
        #endregion

        #region "Pattern Selection"
        private void rbRec_Pattern_Daily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Pattern_Daily.Checked == true)
            {
                _IsPatternChanging = true;
                pnlRec_Pattern_Daily.Visible = true;
                pnlRec_Pattern_Weekly.Visible = false;
                pnlRec_Pattern_Monthly.Visible = false;
                pnlRec_Pattern_Yearly.Visible = false;

                rbRec_Pattern_Daily_EveryDay.Checked = true;
                numRec_Pattern_Daily_EveryDay.Value = 1;

                rbRec_Range_EndAfterOccurence.Checked = true;
                numRec_Range_EndAfterOccurence.Value = 1;
                FindRecurrence();
                _IsPatternChanging = false;
            }
        }

        private void rbRec_Pattern_Weekly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Pattern_Weekly.Checked == true)
            {
                _IsPatternChanging = true;
                pnlRec_Pattern_Daily.Visible = false;
                pnlRec_Pattern_Weekly.Visible = true;
                pnlRec_Pattern_Monthly.Visible = false;
                pnlRec_Pattern_Yearly.Visible = false;

                numRec_Pattern_Weekly_WeekOn.Value = 1;
                ChkRec_Pattern_Weekly_Monday.Checked = false;
                ChkRec_Pattern_Weekly_Tuesday.Checked = false;
                ChkRec_Pattern_Weekly_Wednesday.Checked = false;
                ChkRec_Pattern_Weekly_Thursday.Checked = false;
                ChkRec_Pattern_Weekly_Friday.Checked = false;
                ChkRec_Pattern_Weekly_Saturday.Checked = false;
                ChkRec_Pattern_Weekly_Sunday.Checked = false;
                CheckOneWeekDay();

                rbRec_Range_EndAfterOccurence.Checked = true;
                numRec_Range_EndAfterOccurence.Value = 1;
                FindRecurrence();
                _IsPatternChanging = false;
            }
        }

        private void rbRec_Pattern_Monthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Pattern_Monthly.Checked == true)
            {
                _IsPatternChanging = true;
                pnlRec_Pattern_Daily.Visible = false;
                pnlRec_Pattern_Weekly.Visible = false;
                pnlRec_Pattern_Monthly.Visible = true;
                pnlRec_Pattern_Yearly.Visible = false;

                rbRec_Pattern_Monthly_Day.Checked = true;
                rbRec_Pattern_Monthly_Criteria.Checked = false;

                numRec_Pattern_Monthly_Day_Day.Value = 1;
                numRec_Pattern_Monthly_Day_Month.Value = 1;

                cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndex = 0;
                cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndex = 0;
                numRec_Pattern_Monthly_Criteria_Month.Value = 1;

                rbRec_Range_EndAfterOccurence.Checked = true;
                numRec_Range_EndAfterOccurence.Value = 1;
                FindRecurrence();
                _IsPatternChanging = false;
            }
        }

        private void rbRec_Pattern_Yearly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Pattern_Yearly.Checked == true)
            {
                _IsPatternChanging = true;
                pnlRec_Pattern_Daily.Visible = false;
                pnlRec_Pattern_Weekly.Visible = false;
                pnlRec_Pattern_Monthly.Visible = false;
                pnlRec_Pattern_Yearly.Visible = true;

                rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
                rbRec_Pattern_Yearly_Criteria.Checked = false;

                cmbRec_Pattern_Yearly_Every_Month.SelectedIndex = 0;
                numRec_Pattern_Yearly_Every_MonthDay.Value = 1;

                cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndex = 0;
                cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndex = 0;
                cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndex = 0;

                rbRec_Range_EndAfterOccurence.Checked = true;
                numRec_Range_EndAfterOccurence.Value = 1;
                FindRecurrence();
                _IsPatternChanging = false;
            }
        }
        #endregion

        #region "Pattern Range Selection"
        private void dtpRec_Range_StartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRec_Range_StartDate.Value < Convert.ToDateTime("12/31/2100"))
            {
                if (dtpRec_Range_EndBy.Value < Convert.ToDateTime("12/31/2100"))
                {
                    if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
                    {
                        FindRecurrence();
                        return;
                    }
                }
            }
        }

        private void numRec_Range_EndAfterOccurence_ValueChanged(object sender, EventArgs e)
        {

            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Range_EndAfterOccurence.Checked == false)
                {
                    rbRec_Range_EndAfterOccurence.Checked = true;
                }
                if (rbRec_Range_EndAfterOccurence.Checked == true)
                {
                    FindRecurrence();
                    return;
                }
            }

        }

        private void dtpRec_Range_EndBy_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRec_Range_StartDate.Value < Convert.ToDateTime("12/31/2100"))
            {
                if (dtpRec_Range_EndBy.Value < Convert.ToDateTime("12/31/2100"))
                {
                    if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
                    {
                        if (rbRec_Range_EndBy.Checked == false)
                        {
                            rbRec_Range_EndBy.Checked = true;
                        }
                        if (rbRec_Range_EndBy.Checked == true)
                        {
                            FindRecurrence();
                            return;
                        }
                    }
                }
            }
        }

        #endregion

        #region "Event which affect on changing recurrence criteria"
        #region "Daily Pattern"
        private void rbRec_Pattern_Daily_EveryDay_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                {
                    FindRecurrence();
                    return;
                }
            }
        }

        private void numRec_Pattern_Daily_EveryDay_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Pattern_Daily_EveryDay.Checked == false)
                {
                    rbRec_Pattern_Daily_EveryDay.Checked = true;
                }
                if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                {
                    FindRecurrence();
                    return;
                }
            }
        }

        private void rbRec_Pattern_Daily_EveryWeekday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Pattern_Daily_EveryWeekday.Checked == true)
                {
                    FindRecurrence();
                    return;
                }
            }
        }
        #endregion

        #region "Weekly Pattern"
        private void numRec_Pattern_Weekly_WeekOn_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Monday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Friday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void IsWeekDayChecked()
        {
            bool _Found = false;
            if (ChkRec_Pattern_Weekly_Sunday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Monday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Tuesday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Wednesday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Thursday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Friday.Checked == true) { _Found = true; }
            if (ChkRec_Pattern_Weekly_Saturday.Checked == true) { _Found = true; }
            if (_Found == false)
            {
                CheckOneWeekDay();
            }
        }

        private void CheckOneWeekDay()
        {
            string _Weekday = DateTime.Now.Date.DayOfWeek.ToString().ToUpper(); ;

            if (_Weekday == "SUNDAY") { ChkRec_Pattern_Weekly_Sunday.Checked = true; }
            if (_Weekday == "MONDAY") { ChkRec_Pattern_Weekly_Monday.Checked = true; }
            if (_Weekday == "TUESDAY") { ChkRec_Pattern_Weekly_Tuesday.Checked = true; }
            if (_Weekday == "WEDNESDAY") { ChkRec_Pattern_Weekly_Wednesday.Checked = true; }
            if (_Weekday == "THURSDAY") { ChkRec_Pattern_Weekly_Thursday.Checked = true; }
            if (_Weekday == "FRIDAY") { ChkRec_Pattern_Weekly_Friday.Checked = true; }
            if (_Weekday == "SATURDAY") { ChkRec_Pattern_Weekly_Saturday.Checked = true; }

        }
        #endregion

        #region "Monthly Pattern"
        private void rbRec_Pattern_Monthly_Day_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void numRec_Pattern_Monthly_Day_Day_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void numRec_Pattern_Monthly_Day_Month_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void rbRec_Pattern_Monthly_Criteria_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }

        private void numRec_Pattern_Monthly_Criteria_Month_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
            }
        }
        #endregion

        #region "Yearly Pattern"
        private void rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numRec_Pattern_Yearly_Every_MonthDay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rbRec_Pattern_Yearly_Criteria_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        #region " Resource/Problem Type Time Validation Supporting Method "

        private void ValidateResourceTime()
        {
            try
            {

                Int32 _dtStartTime;
                Int32 _dtEndTime;

                if (chkRecurring.Checked == false)
                {
                    _dtStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                    _dtEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                }
                else
                {
                    if (_SetAppointmentParameter != null && _SetAppointmentParameter.ModifySingleAppointmentFromReccurence == true)
                    {
                        _dtStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                        _dtEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                    }
                    else
                    {
                        _dtStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                        _dtEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                    }
                }

                //Problem Type
                for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                {
                    c1ProviderProblemType.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                    c1ProviderProblemType.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                }

                //Resource
                for (int i = 1; i < c1Resources.Rows.Count; i++)
                {
                    c1Resources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                    c1Resources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                }
                //End

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion

        #endregion

        #region "Procedures & Functions"

        private void ShowAppointmentRecurrence(bool IsRecurrence)
        {
            if (IsRecurrence == false)
            {
                #region "Hide Recurrence"
                pnlRecurring.SendToBack();
                pnlAppointment.BringToFront();

                dtpApp_DateTime_StartTime.Value = dtpRec_DateTime_StartTime.Value;
                numApp_DateTime_Duration.Value = numRec_DateTime_Duration.Value;               

                tsb_OK.Visible = true;
                tsb_Cancel.Visible = true;
                tsb_Print.Visible = true;
                tsb_RegPatient.Visible = true;
                tsb_Recurrence.Visible = true;
                tsb_RemoveRecurrence.Visible = false;
                tsb_ShowRecurrence.Visible = false;
                tsb_ApplyRecurrence.Visible = false;
                tsb_CancelRecurrence.Visible = false;
                #endregion
            }
            else
            {
                #region "Show Recurrence"
                pnlAppointment.SendToBack();
                pnlRecurring.BringToFront();

                tsb_OK.Visible = false;
                tsb_Cancel.Visible = false;
                tsb_Print.Visible = false;
                tsb_Fax.Visible = false;
                tsb_Email.Visible = false;
                tsb_RegPatient.Visible = false;

                tsb_Recurrence.Visible = false;
                tsb_ShowRecurrence.Visible = true;
                tsb_ApplyRecurrence.Visible = true;
                tsb_CancelRecurrence.Visible = true;

                dtpRec_DateTime_StartTime.Value = dtpApp_DateTime_StartTime.Value;
                numRec_DateTime_Duration.Value = numApp_DateTime_Duration.Value;

                if (chkRecurring.Checked == true)
                {
                    if (lvwRec_Apointments.Items.Count > 0)
                    {
                        tsb_RemoveRecurrence.Visible = true;
                    }
                }
                else
                {
                    tsb_RemoveRecurrence.Visible = false;
                }
                #endregion
            }
        }

        private void ShowHideToolStripButtons()
        {
            if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true)
            {
                tsb_OK.Visible = true;
                tsb_Cancel.Visible = true;
                tsb_Print.Visible = true;
                tsb_RegPatient.Visible = true;
                tsb_Recurrence.Visible = true;
                tsb_ShowRecurrence.Visible = false;
                tsb_ApplyRecurrence.Visible = false;
                tsb_CancelRecurrence.Visible = false;
            }
            else
            {
                if (_SetAppointmentParameter.ModifyAppointmentMethod == SingleRecurrence.Single)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;
                    tsb_Print.Visible = true;
                    tsb_RegPatient.Visible = true;
                    tsb_Recurrence.Visible = false;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
                else if (_SetAppointmentParameter.ModifyAppointmentMethod == SingleRecurrence.Recurrence)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;
                    tsb_Print.Visible = true;
                    tsb_RegPatient.Visible = true;
                    tsb_Recurrence.Visible = true;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
                else if (_SetAppointmentParameter.ModifyAppointmentMethod == SingleRecurrence.SingleInRecurrence)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;
                    tsb_Print.Visible = true;
                    tsb_RegPatient.Visible = true;
                    tsb_Recurrence.Visible = false;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
            }
        }

        private void FindRecurrence()
        {
            _IsPatternFinding = true;
            oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_StartDate.Value.Date.ToShortDateString());
            if (dtpRec_Range_StartDate.Value.Date > dtpRec_Range_EndBy.Value.Date)
            {
                dtpRec_Range_EndBy.Value = dtpRec_Range_StartDate.Value.Date.AddDays(1).Date;
            }
            oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_EndBy.Value.Date.ToShortDateString());


            oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(numRec_Range_EndAfterOccurence.Value);
            oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(dtpRec_Range_EndBy.Value.Date.Year);

            if (rbRec_Range_EndAfterOccurence.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndAfterOccurence;
            }
            else if (rbRec_Range_EndBy.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate;
            }
            else
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndByYear;
            }

            //Daily
            if (rbRec_Pattern_Daily.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;
                if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Value);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                else
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryWeekday;
                }
            }
            //Weekly
            else if (rbRec_Pattern_Weekly.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Weekly;

                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Value);

                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = ChkRec_Pattern_Weekly_Sunday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = ChkRec_Pattern_Weekly_Monday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = ChkRec_Pattern_Weekly_Tuesday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = ChkRec_Pattern_Weekly_Wednesday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = ChkRec_Pattern_Weekly_Thursday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = ChkRec_Pattern_Weekly_Friday.Checked;
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = ChkRec_Pattern_Weekly_Saturday.Checked;
            }
            //Monthly
            else if (rbRec_Pattern_Monthly.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Monthly;
                if (rbRec_Pattern_Monthly_Day.Checked == true)
                {
                    //numRec_Pattern_Monthly_Day_Day
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Value);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Value);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = DayWeekday.day;

                }
                else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Value);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString()); //(FirstLastCriteria)cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.GetHashCode();//.ToString();//.GetHashCode();
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString()); //(DayWeekday)cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString().GetHashCode();
                }
            }
            //Yearly
            else if (rbRec_Pattern_Yearly.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Yearly;
                if (rbRec_Pattern_Yearly_EveryMonthDay.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Value);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString()); // (MonthRange)cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString().GetHashCode();

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;
                }
                else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = MonthRange.January;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString()); // (FirstLastCriteria)cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString().GetHashCode();
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString());  //(DayWeekday)cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString().GetHashCode();
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString());  //(MonthRange)cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString().GetHashCode();
                }
            }

            Int64 ProviderID = 0;
            if (cmbApp_Provider.SelectedValue != null && Convert.ToString(cmbApp_Provider.SelectedValue) != "")
            {
                ProviderID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
            }


            //By Pranit on 9 sep 2011 for single Resource with recurrence
            //By Pranit on 13 sep 2011 for Multiple Resource with recurrence
           // Int64 resourceID = 0;
            StringBuilder resourceID = new StringBuilder("");
            for (int i = 1; i < c1Resources.Rows.Count; i++)
            {
                 resourceID = resourceID.Append(c1Resources.GetData(i, COL_ID).ToString());
                 resourceID = resourceID.Append(",");
            }
            if (c1Resources.Rows.Count > 1)
            {
                resourceID = resourceID.Remove(resourceID.Length - 1, 1);
            }
           
            
            // By Pranit on 8 sep 2011 added parameter MasterAppointmentID and resourceID
            //By Pranit on 13 sep 2011 for Multiple Resource with recurrence
            if (oFindCriteria.FindRecurrence(ProviderID, dtpRec_DateTime_StartTime.Value, dtpRec_DateTime_EndTime.Value, _SetAppointmentParameter.MasterAppointmentID, resourceID) == true)
            {
                if (rbRec_Range_EndAfterOccurence.Checked == true)
                {
                    dtpRec_Range_EndBy.Value = oFindCriteria.EndDate;
                }
                else if (rbRec_Range_EndBy.Checked == true)
                {
                    numRec_Range_EndAfterOccurence.Value = Convert.ToDecimal(oFindCriteria.NoOfOccurrences);
                }
            }
            else
            {
                MessageBox.Show("Error while finding range.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           



            _IsPatternFinding = false;
        }

        private void FillControls()
        {
            int _Counter = 0;
            //gloGeneralItem.gloItems oListItems;
            gloAppointmnetScheduleCommon oApptCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);

            //Providers
            //oListItems = new gloGeneralItem.gloItems();
            //oListItems = oApptCommon.GetProviders();

            //DataTable oTableProvider = new DataTable();
            oTableProvider = oApptCommon.GetProvidersList();

            //oTableProvider.Columns.Add("ID");
            //oTableProvider.Columns.Add("DispName");

          //  Added for resource appointment  case 
            //DataRow row1 = oTableProvider.NewRow();
            //row1["ProviderID"] = 0;
            //row1["ProviderName"] = "";
            //oTableProvider.Rows.InsertAt(row1, 0);
            //oTableProvider.AcceptChanges();



            //for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
            //{
            //    DataRow oRow;
            //    oRow = oTableProvider.NewRow();
            //    oRow[0] = oListItems[_Counter].ID;
            //    oRow[1] = oListItems[_Counter].Description;
            //    oTableProvider.Rows.Add(oRow);
            //}

            cmbApp_Provider.DataSource = oTableProvider;
            cmbApp_Provider.ValueMember = "ProviderID";
            cmbApp_Provider.DisplayMember = "ProviderName";
            cmbApp_Provider.SelectedIndex = -1;
            if (cmbApp_Provider != null && cmbApp_Provider.Items.Count > 0)
            {
                cmbApp_Provider.SelectedIndex = 0;
            }

            //oListItems.Dispose();

            //Design Procedures
            //Design Resources

            //Locations

            //oListItems = new gloGeneralItem.gloItems();
            //oListItems = oApptCommon.GetLocations();

            //DataTable oTableLocations = new DataTable();
            oTableLocations = oApptCommon.GetLocationsList();


            //oTableLocations.Columns.Add("ID");
            //oTableLocations.Columns.Add("DispName");

            //for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
            //{
            //    DataRow oRow;
            //    oRow = oTableLocations.NewRow();
            //    oRow[0] = oListItems[_Counter].ID;
            //    oRow[1] = oListItems[_Counter].Description;
            //    oTableLocations.Rows.Add(oRow);
            //}


            cmbApp_Location.DataSource = oTableLocations;
            cmbApp_Location.ValueMember = "nLocationID";
            cmbApp_Location.DisplayMember = "sLocation";
            cmbApp_Location.SelectedIndex = -1;

            if (cmbApp_Location != null && cmbApp_Location.Items.Count > 0)
            {
                if (_DefaultLocationID > 0)
                {
                    cmbApp_Location.SelectedValue = _DefaultLocationID;
                }
            }

            //oListItems.Dispose();

            //Appointment Types

            //oListItems = new gloGeneralItem.gloItems();
            //oListItems = oApptCommon.GetAppointmentTypes();

            //DataTable oTableAppTypes = new DataTable();
            oTableAppTypes = oApptCommon.GetAppointmentTypesList();


            //oTableAppTypes.Columns.Add("ID");
            //oTableAppTypes.Columns.Add("DispName");

            //DataRow dr;
            //dr = oTableAppTypes.NewRow();
            //dr[0] = 0;
            //dr[1] = "";
            //oTableAppTypes.Rows.Add(dr);

            //for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
            //{
            //    DataRow oRow;
            //    oRow = oTableAppTypes.NewRow();
            //    oRow[0] = oListItems[_Counter].ID;
            //    oRow[1] = oListItems[_Counter].Description;
            //    oTableAppTypes.Rows.Add(oRow);
            //}

            cmbApp_AppointmentType.DataSource = oTableAppTypes;
            cmbApp_AppointmentType.ValueMember = "nAppointmentTypeID";
            cmbApp_AppointmentType.DisplayMember = "sAppointmentType";

            //oListItems.Dispose();

            //Status

            //oListItems = new gloGeneralItem.gloItems();
            //oListItems = oApptCommon.GetStatus();

            //DataTable oTableStatus = new DataTable();
            oTableStatus = oApptCommon.GetStatusList();

            //oTableStatus.Columns.Add("ID");
            //oTableStatus.Columns.Add("DispName");

            //DataRow oBlankRow;
            //oBlankRow = oTableStatus.NewRow();
            //oBlankRow[0] = "0";
            //oBlankRow[1] = "";
            //oTableStatus.Rows.Add(oBlankRow);

            //for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
            //{
            //    DataRow oRow;
            //    oRow = oTableStatus.NewRow();
            //    oRow[0] = oListItems[_Counter].ID;
            //    oRow[1] = oListItems[_Counter].Description;
            //    oTableStatus.Rows.Add(oRow);
            //}

            cmbApp_Status.DataSource = oTableStatus;
            cmbApp_Status.ValueMember = "nAppointmentStatusID";
            cmbApp_Status.DisplayMember = "sAppointmentStatus";

            //oListItems.Dispose();

            //------------RECURRENCE RELEATED---------------------//
            //No End Date Years

            cmbRec_Range_NoEndDateYear.Items.Clear();
            for (_Counter = DateTime.Now.Year; _Counter <= DateTime.Now.Year + 11; _Counter++)
            {
                cmbRec_Range_NoEndDateYear.Items.Add(_Counter.ToString());
            }
            oApptCommon.Dispose();
            oApptCommon = null;
        }

        private void ClearData()
        {
            
            cmbApp_AppointmentType.DataSource = null;
            cmbApp_AppointmentType.Items.Clear();
            cmbApp_AppointmentType.Text = "";

            pnlApp_DateTime.Visible = true;
            tsb_Recurrence.Visible = true;
            tsb_RemoveRecurrence.Visible = false;

            lblApp_DateTime.Tag = "";
            lblApp_DateTime.Visible = true;
            lblApp_Recurrence.Tag = "";
            lblApp_Recurrence.Visible = false;


            numApp_DateTime_Duration.Value = 5;
            numApp_DateTime_Duration.Tag = "";

            chkApp_DateTime_IsAllDayEvent.Checked = false;
            chkApp_DateTime_IsAllDayEvent.Tag = "";

            lblApp_DateTime_ColorContainer.BackColor = Color.White;
            lblApp_DateTime_ColorContainer.Tag = "";

            lblApp_Recurrence_Time.Text = "";
            lblApp_Recurrence_Time.Tag = "";
            pnlApp_DateTimeContainer.Visible = true;
            lblApp_Recurrence_Time.Visible = false;

            txtApp_Notes.Text = "";
            txtApp_Notes.Tag = "";
            
            cmbApp_ReferralDoctor.DataSource = null;
            cmbApp_ReferralDoctor.Items.Clear();
            cmbApp_ReferralDoctor.Text = "";

            //DesignGrid();
            
            cmbApp_Coverage.DataSource = null;
            cmbApp_Coverage.Items.Clear();
            cmbApp_Coverage.Text = "";

            
            cmbApp_Status.DataSource = null;
            cmbApp_Status.Items.Clear();
            cmbApp_Status.Text = "";

            //btnApp_DateTime_Color.Enabled = true;
            //btnApp_ClearDateTime_Color.Enabled = true;

            //Recurring
            ClearRecurrence();

            FillControls();

            ShowAppointmentRecurrence(false);
            ShowHideToolStripButtons();

            //Fill Monthly Criteria
            cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.first.ToString());
            cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.second.ToString());
            cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.third.ToString());
            cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.fourth.ToString());
            cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.last.ToString());

            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.day.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.weekday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.weekendday.ToString());

            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Sunday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Monday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Tuesday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Wednesday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Thursday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Friday.ToString());
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Add(DayWeekday.Saturday.ToString());

            //Fill yearly Criteria
            cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.first.ToString());
            cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.second.ToString());
            cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.third.ToString());
            cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.fourth.ToString());
            cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.last.ToString());

            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.day.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.weekday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.weekendday.ToString());

            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Sunday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Monday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Tuesday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Wednesday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Thursday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Friday.ToString());
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Add(DayWeekday.Saturday.ToString());


            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.January.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.February.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.March.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.April.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.May.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.June.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.July.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.August.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.September.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.October.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.November.ToString());
            cmbRec_Pattern_Yearly_Every_Month.Items.Add(MonthRange.December.ToString());


            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.January.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.February.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.March.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.April.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.May.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.June.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.July.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.August.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.September.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.October.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.November.ToString());
            cmbRec_Pattern_Yearly_Criteria_Month.Items.Add(MonthRange.December.ToString());

            //
            cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndex = 0;
            cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndex = 0;
            cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndex = 0;
            cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndex = 0;
            cmbRec_Pattern_Yearly_Every_Month.SelectedIndex = 0;
            cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndex = 0;

            CurrentPriorAuthorizationID = 0;
            UpdatedPriorAuthorizationID = 0;
        }

        private void ClearRecurrence()
        {
            pnlRec_Pattern_Daily.Visible = true;
            pnlRec_Pattern_Weekly.Visible = false;
            pnlRec_Pattern_Monthly.Visible = false;
            pnlRec_Pattern_Yearly.Visible = false;

            chkRecurring.Checked = false;
            chkRecurring.Tag = "";

            // Commented by pranit on 1 march 2012 to solve issue #22162           
            // numRec_DateTime_Duration.Value = 1;
            numRec_DateTime_Duration.Tag = "";            

            chkRec_DateTime_IsAllDayEvent.Checked = false;
            chkRec_DateTime_IsAllDayEvent.Tag = "";

            lblRec_DateTime_ColorContainer.Text = "";
            lblRec_DateTime_ColorContainer.Tag = "";
            lblRec_DateTime_ColorCode.Tag = "";
            lblRec_DateTime_ColorContainer.BackColor = Color.White;


            rbRec_Pattern_Daily.Checked = true;
            rbRec_Pattern_Daily.Tag = "";

            rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
            rbRec_Pattern_Yearly_EveryMonthDay.Tag = "";

            numRec_Pattern_Yearly_Every_MonthDay.Value = 1;
            numRec_Pattern_Yearly_Every_MonthDay.Tag = "";

            numRec_Pattern_Monthly_Day_Day.Value = 1;
            numRec_Pattern_Monthly_Day_Day.Tag = "";

            numRec_Pattern_Monthly_Day_Month.Value = 1;
            numRec_Pattern_Monthly_Day_Month.Tag = "";

            numRec_Pattern_Monthly_Criteria_Month.Value = 1;
            numRec_Pattern_Monthly_Criteria_Month.Tag = "";

            numRec_Pattern_Daily_EveryDay.Value = 1;
            numRec_Pattern_Daily_EveryDay.Tag = "";

            numRec_Pattern_Weekly_WeekOn.Value = 1;
            numRec_Pattern_Weekly_WeekOn.Tag = "";

            ChkRec_Pattern_Weekly_Sunday.Checked = false;
            ChkRec_Pattern_Weekly_Sunday.Tag = "";

            ChkRec_Pattern_Weekly_Monday.Checked = false;
            ChkRec_Pattern_Weekly_Monday.Tag = "";

            ChkRec_Pattern_Weekly_Tuesday.Checked = false;
            ChkRec_Pattern_Weekly_Tuesday.Tag = "";

            ChkRec_Pattern_Weekly_Wednesday.Checked = false;
            ChkRec_Pattern_Weekly_Wednesday.Tag = "";

            ChkRec_Pattern_Weekly_Thursday.Checked = false;
            ChkRec_Pattern_Weekly_Thursday.Tag = "";

            ChkRec_Pattern_Weekly_Friday.Checked = false;
            ChkRec_Pattern_Weekly_Friday.Tag = "";

            ChkRec_Pattern_Weekly_Saturday.Checked = false;
            ChkRec_Pattern_Weekly_Saturday.Tag = "";

            rbRec_Range_NoEndDate.Checked = true;
            rbRec_Range_NoEndDate.Tag = "";

            numRec_Range_EndAfterOccurence.Value = 1;
            numRec_Range_EndAfterOccurence.Tag = "";

            lvwRec_Apointments.Items.Clear();
            lvwRec_Exception.Items.Clear();

            DesignRecurrenceGrid();
        }

        private void DesignRecurrenceGrid()
        {
            //Design Procedures
            lvwRec_Apointments.Columns.Clear();
            lvwRec_Apointments.Items.Clear();
            lvwRec_Apointments.Columns.Add("No."); // Number 0
            lvwRec_Apointments.Columns.Add("Start Date"); // Start Date 1
            lvwRec_Apointments.Columns.Add("Start Time"); // Start Time 2
            lvwRec_Apointments.Columns.Add("End Date"); // End Date 3
            lvwRec_Apointments.Columns.Add("End Time"); // End Time 4
            lvwRec_Apointments.Columns.Add("Duration"); // Duration 5
            lvwRec_Apointments.Columns.Add("Color Code"); // Color Code 6
            lvwRec_Apointments.Columns.Add("Status ID"); // Status ID 7
            lvwRec_Apointments.Columns.Add("ID"); // Master Appointment ID 8
            lvwRec_Apointments.Columns.Add("Appointment ID"); // Appointment ID 9
            lvwRec_Apointments.Columns.Add("Status"); // Appointment status 10

            int _Width = (lvwRec_Apointments.Width - 15) / 15;

            lvwRec_Apointments.Columns[0].Width = _Width * 2; // Number
            lvwRec_Apointments.Columns[1].Width = _Width * 3; // Start Date
            lvwRec_Apointments.Columns[2].Width = _Width * 3; // Start Time
            lvwRec_Apointments.Columns[3].Width = 0; // End Date
            lvwRec_Apointments.Columns[4].Width = _Width * 3; // End Time
            lvwRec_Apointments.Columns[5].Width = _Width * 2; // Duration
            lvwRec_Apointments.Columns[6].Width = 0; // Color Code
            lvwRec_Apointments.Columns[7].Width = 0; // Status ID
            lvwRec_Apointments.Columns[8].Width = 0; // ID
            lvwRec_Apointments.Columns[9].Width = 0; // ID
            lvwRec_Apointments.Columns[10].Width = _Width * 3; ; // status
        }

        //private void DesignGrid()
        //{
        //    try
        //    {
        //        int _width;

        //        #region ProblemType

        //        c1ProviderProblemType.Rows.Count = 1;
        //        c1ProviderProblemType.Rows.Fixed = 1;
        //        c1ProviderProblemType.Cols.Count = COL_COLUMNCOUNT;
        //        c1ProviderProblemType.Cols.Fixed = 0;

        //        c1ProviderProblemType.SetData(0, COL_ID, "ID");
        //        c1ProviderProblemType.SetData(0, COL_CODE, "Code");
        //        c1ProviderProblemType.SetData(0, COL_DESC, "Description");
        //        c1ProviderProblemType.SetData(0, COL_STARTTIME, "Start Time");
        //        c1ProviderProblemType.SetData(0, COL_ENDTIME, "End Time");

        //        c1ProviderProblemType.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
        //        c1ProviderProblemType.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
        //        // 20100109 Mahesh Nawal Set the proper format for time control in grid
        //        c1ProviderProblemType.Cols[COL_STARTTIME].Format = "t";
        //        c1ProviderProblemType.Cols[COL_ENDTIME].Format = "t";

        //        c1ProviderProblemType.Cols[COL_ID].Visible = false;
        //        c1ProviderProblemType.Cols[COL_CODE].Visible = false;
        //        c1ProviderProblemType.Cols[COL_DESC].Visible = true;
        //        c1ProviderProblemType.Cols[COL_STARTTIME].Visible = true;
        //        c1ProviderProblemType.Cols[COL_ENDTIME].Visible = true;

        //        c1ProviderProblemType.Cols[COL_ID].TextAlign = TextAlignEnum.LeftCenter;
        //        c1ProviderProblemType.Cols[COL_CODE].TextAlign = TextAlignEnum.LeftCenter;
        //        c1ProviderProblemType.Cols[COL_DESC].TextAlign = TextAlignEnum.LeftCenter;
        //        c1ProviderProblemType.Cols[COL_STARTTIME].TextAlign = TextAlignEnum.LeftCenter;
        //        c1ProviderProblemType.Cols[COL_ENDTIME].TextAlign = TextAlignEnum.LeftCenter;

        //        _width = (pnlCriteria_ProviderProblemType.Width - 15);

        //        c1ProviderProblemType.Cols[COL_ID].Width = 0;
        //        c1ProviderProblemType.Cols[COL_CODE].Width = 0;
        //        c1ProviderProblemType.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.50);
        //        c1ProviderProblemType.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
        //        c1ProviderProblemType.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

        //        c1ProviderProblemType.AllowEditing = true;
        //        c1ProviderProblemType.Cols[COL_ID].AllowEditing = false;
        //        c1ProviderProblemType.Cols[COL_CODE].AllowEditing = false;
        //        c1ProviderProblemType.Cols[COL_DESC].AllowEditing = false;
        //        c1ProviderProblemType.Cols[COL_STARTTIME].AllowEditing = true;
        //        c1ProviderProblemType.Cols[COL_ENDTIME].AllowEditing = true;

        //        #endregion

        //        #region Resources

        //        c1Resources.Rows.Count = 1;
        //        c1Resources.Rows.Fixed = 1;
        //        c1Resources.Cols.Count = COL_COLUMNCOUNT;
        //        c1Resources.Cols.Fixed = 0;

        //        c1Resources.SetData(0, COL_ID, "ID");
        //        c1Resources.SetData(0, COL_CODE, "Code");
        //        c1Resources.SetData(0, COL_DESC, "Description");
        //        c1Resources.SetData(0, COL_STARTTIME, "Start Time");
        //        c1Resources.SetData(0, COL_ENDTIME, "End Time");

        //        c1Resources.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
        //        c1Resources.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
        //        c1Resources.Cols[COL_STARTTIME].Format = "t";
        //        c1Resources.Cols[COL_ENDTIME].Format = "t";

        //        c1Resources.Cols[COL_ID].Visible = false;
        //        c1Resources.Cols[COL_CODE].Visible = true;
        //        c1Resources.Cols[COL_DESC].Visible = true;
        //        c1Resources.Cols[COL_STARTTIME].Visible = true;
        //        c1Resources.Cols[COL_ENDTIME].Visible = true;

        //        c1Resources.Cols[COL_ID].TextAlign = TextAlignEnum.LeftCenter;
        //        c1Resources.Cols[COL_CODE].TextAlign = TextAlignEnum.LeftCenter;
        //        c1Resources.Cols[COL_DESC].TextAlign = TextAlignEnum.LeftCenter;
        //        c1Resources.Cols[COL_STARTTIME].TextAlign = TextAlignEnum.LeftCenter;
        //        c1Resources.Cols[COL_ENDTIME].TextAlign = TextAlignEnum.LeftCenter;


        //        _width = (pnlCriteria_Resources.Width - 15);

        //        c1Resources.Cols[COL_ID].Width = 0;
        //        c1Resources.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.22);
        //        c1Resources.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
        //        c1Resources.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
        //        c1Resources.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

        //        c1Resources.AllowEditing = true;
        //        c1Resources.Cols[COL_ID].AllowEditing = false;
        //        c1Resources.Cols[COL_CODE].AllowEditing = false;
        //        c1Resources.Cols[COL_DESC].AllowEditing = false;
        //        c1Resources.Cols[COL_STARTTIME].AllowEditing = true;
        //        c1Resources.Cols[COL_ENDTIME].AllowEditing = true;

        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //}

        private void GetClinicTiming()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            DataTable dtClinicTime = null;
            try
            {
                dtClinicTime = ogloSettings.GetClinicTime();
                if (dtClinicTime != null)
                {
                    if (dtClinicTime.Rows.Count > 0)
                    {
                        _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dtClinicTime.Rows[0]["StartTime"].ToString());
                        _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dtClinicTime.Rows[0]["endtime"].ToString());
                    }
                    dtClinicTime.Dispose();
                    dtClinicTime = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }
        }

        private Int64  RegisterPatient()
        {
            Int64 _RegisteredPatientId = 0;

            try
            {
                //1.Show Quick Registration Form & Save Patient
                gloPatient.frmSetupQuickPatient ofrmSetupQuickPatient = new gloPatient.frmSetupQuickPatient(_databaseconnectionstring);
                if (cmbApp_Provider.SelectedIndex != -1)
                {
                    ofrmSetupQuickPatient.ProviderName = cmbApp_Provider.Text.Trim();
                    ofrmSetupQuickPatient.ProviderID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                }
                ofrmSetupQuickPatient.ShowDialog(this);
                _RegisteredPatientId = ofrmSetupQuickPatient.ReturnPatientID;
                ofrmSetupQuickPatient.Dispose();
                ofrmSetupQuickPatient = null;
                //2.Check if Patient Registered successfully if yes 
                if (_RegisteredPatientId > 0)
                {

                    this.SetAppointmentParameters.PatientID = _RegisteredPatientId;

                    string _strSQL = "";
                    _strSQL = "SELECT ISNULL(sFirstName,'')+ Space(1) + ISNULL(sMiddleName,'')+ Space(1) + ISNULL(sLastName,'') AS sPatientName FROM Patient where nPatientID= " + _SetAppointmentParameter.PatientID + "";
                    DataTable dt = new DataTable();
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                    _strSQL = "";
                    oDB.Disconnect();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtApp_Patient.Text = dt.Rows[0]["sPatientName"].ToString();
                        txtApp_Patient.Tag = _SetAppointmentParameter.PatientID;
                        CurrentPatientID = _SetAppointmentParameter.PatientID;
                        this.Text = "Appointment";
                        try
                        {
                            gloPatient.gloPatient.GetWindowTitle(this, CurrentPatientID, _databaseconnectionstring, _MessageBoxCaption);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                    }

                    //3.Call the Save Appointment Event to save the Appointment
                    if (SetAppointmentParameters.AppointmentFlag == AppointmentScheduleFlag.TemplateBlock)
                    {
                        if (SaveValidation() == true)
                        {
                            tsb_OK_Click(null, null);
                        }
                        else
                        {
                            this.Opacity = 100;
                        }
                    }
                    else
                    {
                        this.Opacity = 100;
                    }
                }
                //else
                //{
                //    this.Close();
                //}
                return _RegisteredPatientId;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                this.Opacity = 100;
                return 0;
            }
            finally
            {
            }
        }

        private bool DeleteAppointment(Int64 MasterAppointmentId, Int64 AppointmentDetailId, bool DeleteMaster)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            bool _retValue = false;

            try
            {
                oDB.Connect(false);

                //2.Delete the Master entry for the Appointment
                if (DeleteMaster)
                {
                    // Delete detail Table entry if not check-in or check-out   
                    _sqlQuery = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + _clinicID + " AND (nUsedStatus <> 3 AND nUsedStatus <> 4)";
                    oDB.Execute_Query(_sqlQuery);

                    //if all detail appointments are deleted then delete master table entry 
                    _sqlQuery = "SELECT COUNT(*) FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + _clinicID + "";
                    object oDetailsAppCount = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oDetailsAppCount != null && Convert.ToString(oDetailsAppCount) != "")
                    {
                        if (Convert.ToInt32(oDetailsAppCount) == 0)
                        {
                            _sqlQuery = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + _clinicID + " ";
                            oDB.Execute_Query(_sqlQuery);
                        }
                    }
                    _retValue = true;
                }
                else
                {
                    //1.Delete the Details Table entry for the Appointment
                    _sqlQuery = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND (nDTLAppointmentID = " + AppointmentDetailId + " OR nRefID = " + AppointmentDetailId + ") AND nClinicID =" + _clinicID + " ";
                    int IsDeleted = oDB.Execute_Query(_sqlQuery);
                    if (IsDeleted > 0)
                    {
                        _retValue = true;
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _retValue = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                _retValue = false;
            }
            finally
            {
                if (oDB.Connect(false)) { oDB.Disconnect(); }

                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _retValue;
        }

        public string GetReferralDoctor(Int64 ID)
        {
            string _strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strReferralName = "";
            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT   (ISNULL(Contacts_Physician_DTL.sPrefix,'')  + SPACE(1) + ISNULL(Contacts_MST.sFirstName,'')  + SPACE(1) + ISNULL(Contacts_MST.sMiddleName,'')  + SPACE(1) + "
                        + " ISNULL(Contacts_MST.sLastName,'')  + SPACE(1) + ISNULL(Contacts_Physician_DTL.sDegree,'') ) AS sName  "
                        + " FROM  Contacts_MST INNER JOIN Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID  "
                        + "  WHERE  Contacts_MST.nContactID = " + ID + "";

                _strReferralName = Convert.ToString(oDB.ExecuteScalar_Query(_strSQL));
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
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            return _strReferralName;
        }

        #endregion

        #region "List Selection Methods"

        private void cmbApp_AppointmentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //variable added to maintain color if user use tab against  for Bug ID : 49128
                _intcmbapptind = cmbApp_AppointmentType.SelectedIndex;

               
                SetAppointmentTypeData();
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

       

        private void FillAppResource(DataTable dtProblemType)
        {
            gloAppointmentBook.Books.AppointmentType oa = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);

            try
            {
                if (dtProblemType != null)
                {
                    for (int i = 0; i < dtProblemType.Rows.Count; i++)
                    {
                        c1ProviderProblemType.Rows.Add();
                        Int32 RowIndex = c1ProviderProblemType.Rows.Count - 1;
                        c1ProviderProblemType.SetData(RowIndex, COL_ID, Convert.ToString(dtProblemType.Rows[i]["nProblemTypeID"]));
                        c1ProviderProblemType.SetData(RowIndex, COL_CODE, Convert.ToString(""));
                        c1ProviderProblemType.SetData(RowIndex, COL_DESC, Convert.ToString(dtProblemType.Rows[i]["sProblemType"]));
                        if (chkRecurring.Checked == false)
                        {
                            c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                            c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                        }
                        else
                        {
                            c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                            c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                        }


                        // Fill Resources Associated with problem type

                        DataTable dtResources = new DataTable();
                        dtResources = oa.GetProblemTypeResources(Convert.ToInt64(dtProblemType.Rows[i]["nProblemTypeID"]));
                        if (dtResources != null)
                        {
                            for (int k = 0; k < dtResources.Rows.Count; k++)
                            {
                                bool _isResourceAdded = false;
                                for (int l = 1; l < c1Resources.Rows.Count; l++)
                                {

                                    if (Convert.ToInt64(c1Resources.GetData(l, COL_ID)) == Convert.ToInt64(dtResources.Rows[k]["nResourceID"]))
                                    {
                                        _isResourceAdded = true;
                                        break;
                                    }
                                }
                                if (_isResourceAdded == true)
                                    continue;

                                c1Resources.Rows.Add();
                                Int32 ChildRowIndex = c1Resources.Rows.Count - 1;
                                c1Resources.SetData(ChildRowIndex, COL_ID, Convert.ToString(dtResources.Rows[k]["nResourceID"]));
                                c1Resources.SetData(ChildRowIndex, COL_CODE, Convert.ToString(dtResources.Rows[k]["sCode"]));
                                c1Resources.SetData(ChildRowIndex, COL_DESC, Convert.ToString(dtResources.Rows[k]["sDescription"]));
                                if (chkRecurring.Checked == false)
                                {
                                    c1Resources.SetData(ChildRowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                    c1Resources.SetData(ChildRowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                }
                                else
                                {
                                    c1Resources.SetData(ChildRowIndex, COL_STARTTIME, dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                                    c1Resources.SetData(ChildRowIndex, COL_ENDTIME, dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oa != null) { oa.Dispose(); oa = null; }
            }

        }

        private void btnApp_Provider_Click(object sender, EventArgs e)
        {

            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
            oListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oListControl.ControlHeader = " Providers";
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_Recurrence.Enabled = false;
            tsb_RegPatient.Enabled = false;
            _CurrentControlType = gloListControl.gloListControlType.Providers;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);

            if (cmbApp_Provider.DataSource != null)
            {
                DataTable oFillTable = new DataTable();
                oFillTable = (DataTable)cmbApp_Provider.DataSource;

                for (int i = 0; i <= oFillTable.Rows.Count - 1; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString());
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private void btnApp_Patient_Click(object sender, EventArgs e)
        {
            if (oPatientListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oPatientListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    //dtpApp_DateTime_EndDate.ValueChanged +=new EventHandler(dtpApp_DateTime_EndDate_ValueChanged);
                    oPatientListControl.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
  
                    oPatientListControl.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
  
                    oPatientListControl.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
    
                    oPatientListControl.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);
     
                }
                catch
                {
                }
                oPatientListControl.Dispose();
                oPatientListControl = null;
            }
            oPatientListControl = new gloPatient.PatientListControl();
            oPatientListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oPatientListControl.DatabaseConnection = _databaseconnectionstring;
            oPatientListControl.ControlHeader = "Patient";
            tsb_Recurrence.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_RegPatient.Enabled = false;
            oPatientListControl.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
            oPatientListControl.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
            oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
            oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);

            this.Width = 875;
            this.Controls.Add(oPatientListControl);
            if (txtApp_Patient.Tag != null && Convert.ToString(txtApp_Patient.Tag) != "")
            {
                oPatientListControl.SelectedPatientID = Convert.ToInt64(Convert.ToString(txtApp_Patient.Tag));
            }

            oPatientListControl.FillPatients();
            oPatientListControl.ShowOKCancel(true);
            oPatientListControl.ShowHeader(true);
            oPatientListControl.Dock = DockStyle.Fill;
            oPatientListControl.BringToFront();
        }

        private void btnApp_ReferralDoctor_Click(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                                 oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                            oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }
 
                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;

            }
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, false, this.Width);
            oListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oListControl.ControlHeader = " Referral Doctors";
            tsb_Recurrence.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_RegPatient.Enabled = false;
            _CurrentControlType = gloListControl.gloListControlType.Referrals;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
            oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
            this.Width = 820;
            this.Height = 536;
            this.Controls.Add(oListControl);

            if (cmbApp_ReferralDoctor.DataSource != null)
            {
                DataTable oFillTable = new DataTable();
                oFillTable = (DataTable)cmbApp_ReferralDoctor.DataSource;

                for (int i = 0; i <= oFillTable.Rows.Count - 1; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString());
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            this.Width = 820;
            this.Height = 536;
        }

        private void btnApp_Procedures_Click(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Procedures, true, this.Width);
            oListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oListControl.ControlHeader = "Problem Types";
            tsb_Recurrence.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_RegPatient.Enabled = false;
            _CurrentControlType = gloListControl.gloListControlType.Procedures;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            pnlMainNToolStrip.Visible = false;

            this.Controls.Add(oListControl);

            for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
            {
                oListControl.SelectedItems.Add(Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID)), Convert.ToString(c1ProviderProblemType.GetData(i, COL_DESC)));
            }

            for (int i = 1; i < c1Resources.Rows.Count; i++)
            {
                oListControl.SelectedItems.Add(Convert.ToInt64(c1Resources.GetData(i, COL_ID)), Convert.ToString(c1Resources.GetData(i, COL_DESC)));
            }

            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            this.Width = 820;
            this.Height = 536;
        }

        private void btnApp_Resources_Click(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Resources, true, 0);
            oListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oListControl.ControlHeader = " Resources";
            tsb_Recurrence.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_RegPatient.Enabled = false;
            _CurrentControlType = gloListControl.gloListControlType.Resources;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            this.Width = 820;
            this.Height = 536;
            this.Controls.Add(oListControl);

            for (int i = 1; i < c1Resources.Rows.Count; i++)
            {
                oListControl.SelectedItems.Add(Convert.ToInt64(c1Resources.GetData(i, COL_ID)), Convert.ToString(c1Resources.GetData(i, COL_DESC)));
            }


            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            this.Width = 820;
            this.Height = 536;
        }

        private void btnApp_Coverage_Click(object sender, EventArgs e)
        {
            //Anil 20080128  
            // Don't delete this code, it is required later when coverages will be added

            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            if (txtApp_Patient.Tag == null)
                return;

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientInsurence, true, this.Width);
            oListControl.ClinicID = _SetAppointmentParameter.ClinicID;
            oListControl.PatientID = Convert.ToInt64(txtApp_Patient.Tag);
            oListControl.ControlHeader = " Coverages";
            tsb_Recurrence.Enabled = false;
            tsb_OK.Enabled = false;
            tsb_Print.Enabled = false;
            tsb_Email.Enabled = false;
            tsb_Fax.Enabled = false;
            tsb_RegPatient.Enabled = false;
            _CurrentControlType = gloListControl.gloListControlType.Coverage;

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            this.Width = 820;
            this.Height = 536;
            this.Controls.Add(oListControl);

            if (cmbApp_Coverage.DataSource != null)
            {
                DataTable oFillTable = new DataTable();
                oFillTable = (DataTable)cmbApp_Coverage.DataSource;

                for (int i = 0; i <= oFillTable.Rows.Count - 1; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString());
                }
            }
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            this.Width = 820;
            this.Height = 536;
        }

        private void btnApp_ClearProcedures_Click(object sender, EventArgs e)
        {
            // solving TFS issue id-3945
            if (c1ProviderProblemType != null && c1ProviderProblemType.Rows.Count > 0)
            {
                if (c1ProviderProblemType.RowSel > 0)
                {
                    int _RemoveItem = c1ProviderProblemType.RowSel;
                    c1ProviderProblemType.RemoveItem(_RemoveItem);
                }
            }
        }

        private void btnApp_ClearResources_Click(object sender, EventArgs e)
        {
            if (c1Resources != null && c1Resources.Rows.Count > 0)
            {
                if (c1Resources.RowSel > 0)
                {
                    int _RemoveItem = c1Resources.RowSel;
                    c1Resources.RemoveItem(_RemoveItem);
                }
            }
        }

        private void btnApp_ClearProvider_Click(object sender, EventArgs e)
        {
           // cmbApp_Provider.Items.Clear();
            cmbApp_Provider.DataSource = null;
            cmbApp_Provider.Refresh();
        }

        private void btnApp_ClearPatient_Click(object sender, EventArgs e)
        {
            txtApp_Patient.Text = "";
            txtApp_Patient.Tag = null;

            CurrentPatientID = 0;
            UpdatedPatientID = 0;
            isBaddebtPatient = false;

            txtPriorAuthorizationNo.Text = "";
            txtPriorAuthorizationNo.Tag = null;

            
            cmbApp_ReferralDoctor.DataSource = null;
            cmbApp_ReferralDoctor.Items.Clear();

            CheckForPA();

            lblAuthorizaionName.Tag = null;
            lblPatientBalance.Text = "$ " + "0.00";
            txtApp_Patient.Refresh();

            pnlCommanAlerts.Visible = false;
            pnlgloEMRAlerts.Visible = false;
            pnlgloPMAlerts.Visible = false;
            pnlEMRCaption.Visible = false;
            pnlPMCaption.Visible = false;
        }

        private void btnApp_ClearReferralDoctor_Click(object sender, EventArgs e)
        {
          //  cmbApp_ReferralDoctor.Items.Clear();
            cmbApp_ReferralDoctor.DataSource = null;
            cmbApp_ReferralDoctor.Refresh();
        }

        private void btnApp_ClearDateTime_Color_Click(object sender, EventArgs e)
        {
            lblApp_DateTime_ColorContainer.BackColor = Color.White;
            lblRec_DateTime_ColorContainer.BackColor = Color.White;
            lblApp_DateTime_ColorContainer.Refresh();
        }

        private void btnApp_ClearInsurance_Click(object sender, EventArgs e)
        {
            //cmbApp_Coverage.Items.Clear();
            cmbApp_Coverage.DataSource = null;
            cmbApp_Coverage.Refresh();
        }
        //
        void oPatientListControl_GridRowSelect_Click(object sender, EventArgs e)
        {
            //code added for lock chart
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
            if (oSecurity.isPatientLock(oPatientListControl.PatientID, true) )
            {
                return;
            }
            
            //end

            // GLO2011-0011970
            // If patient status is legal pending then don't allow to create / modify appointment
            using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_databaseconnectionstring))
            {
                if (objPatient.IsLegalPending(oPatientListControl.PatientID))
                {
                    MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not create an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);        
                    return;
                }
            }
        }
        void oPatientListControl_Grid_MouseDown(object sender, EventArgs e)
        {

        }
        void oPatientListControl_ItemClosedClick(object sender, EventArgs e)
        {
            tsb_Recurrence.Enabled = true;
            tsb_OK.Enabled = true;
            tsb_Print.Enabled = true;
            tsb_Email.Enabled = true;
            tsb_Fax.Enabled = true;
            tsb_RegPatient.Enabled = true;
            this.Width = 820;
            //To make tsb_RegPatient enable=false to fix bug 5569 :From "Appointments" window we cannot add new patient.
            AssignUserRights();

            #region " Filling Alerts "

            FillPatientAlert();

            #endregion


        }
        void oPatientListControl_Grid_DoubleClick(object sender, EventArgs e)
        {
            this.Width = 820;
            //Check Prior Auth required after patient changed.
            SetDefaultPASetting(oPatientListControl.SelectedPatientID);

            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            if (oSecurity.isBadDebtPatient(oPatientListControl.SelectedPatientID, true))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to schedule a new appointment ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr.ToString() == "No")
                {
                    return;
                }
                else
                {
                    isBaddebtPatient = true;
                }
            }  
            else
            {
                isBaddebtPatient = false;
            }
            oSecurity.Dispose();
            oSecurity = null;

            txtApp_Patient.Tag = oPatientListControl.SelectedPatientID;

            UpdatedPatientID = oPatientListControl.SelectedPatientID;
            
            // GLO2011-0011970
            // If patient status is legal pending then don't allow to create / modify appointment
            using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_databaseconnectionstring))
            {
                if (objPatient.IsLegalPending(UpdatedPatientID))
                {
                    MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not create an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Clear the patient list selected patient info, as no patient is selected.
                    txtApp_Patient.Text = "";
                    txtApp_Patient.Tag = null;
                    return;
                }
            }

            UpdatedPriorAuthorizationID = 0;
            this.Text = "Appointment";
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, oPatientListControl.SelectedPatientID, _databaseconnectionstring, _MessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            if (oPatientListControl.MiddleName != "")
            {
                txtApp_Patient.Text = oPatientListControl.FirstName + " " + oPatientListControl.MiddleName + " " + oPatientListControl.LastName;
            }
            else if (oPatientListControl.MiddleName.Trim() == "")
            {
                txtApp_Patient.Text = oPatientListControl.FirstName + " " + oPatientListControl.LastName;
            }
            //Load Patient referrals
            string _strSQL = "";
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            _strSQL = " SELECT ISNULL(nContactId,0) AS nContactId , ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS sReferralName "
                    + " FROM  Patient_DTL "
                    + " WHERE  nPatientID = " + Convert.ToInt64(txtApp_Patient.Tag) + " AND nClinicID = " + _clinicID + " AND nContactFlag = 3 ORDER BY sReferralName ";
            DataTable dtReferral = new DataTable();

            oDB.Connect(false);
            oDB.Retrive_Query(_strSQL, out dtReferral);
           
            cmbApp_ReferralDoctor.DataSource = null;
            cmbApp_ReferralDoctor.Items.Clear();
            if (dtReferral != null && dtReferral.Rows.Count > 0)
            {
                cmbApp_ReferralDoctor.DisplayMember = "sReferralName";
                cmbApp_ReferralDoctor.ValueMember = "nContactId";
                cmbApp_ReferralDoctor.DataSource = dtReferral;
                cmbApp_ReferralDoctor.SelectedIndex = 0;
            }
            else
            {
               
                cmbApp_ReferralDoctor.DataSource = null;
                cmbApp_ReferralDoctor.Items.Clear();
            }
            oDB.Disconnect();
            if (oDB != null) { oDB.Dispose(); oDB = null; }
            txtPriorAuthorizationNo.Text = "";
            txtPriorAuthorizationNo.Tag = null;

            CheckForPA();

            lblAuthorizaionName.Tag = null;
            tsb_Recurrence.Enabled = true;
            tsb_OK.Enabled = true;
            tsb_Print.Enabled = true;
            tsb_Email.Enabled = true;
            tsb_Fax.Enabled = true;
            tsb_RegPatient.Enabled = true;
            oPatientListControl.SendToBack();
            AssignUserRights();

            #region " Filling Alerts "
            FillPatientAlert();
            #endregion

            
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {
                case gloListControl.gloListControlType.Providers:
                    {
                        
                        cmbApp_Provider.DataSource = null;
                        cmbApp_Provider.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbApp_Provider.DisplayMember = "DispName";
                            cmbApp_Provider.ValueMember = "ID";
                            cmbApp_Provider.DataSource = oBindTable;
                        }

                    }
                    break;

                case gloListControl.gloListControlType.Referrals:
                    {
                       
                        cmbApp_ReferralDoctor.DataSource = null;
                        cmbApp_ReferralDoctor.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }
                           
                            cmbApp_ReferralDoctor.DataSource = null;
                            cmbApp_ReferralDoctor.Items.Clear();
                            cmbApp_ReferralDoctor.DisplayMember = "DispName";
                            cmbApp_ReferralDoctor.ValueMember = "ID";
                            cmbApp_ReferralDoctor.DataSource = oBindTable;
                        }
                    }
                    break;
                case gloListControl.gloListControlType.Procedures:
                    {
                        c1ProviderProblemType.Rows.Count = 1;
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                            {
                                c1ProviderProblemType.Rows.Add();
                                Int32 RowIndex = c1ProviderProblemType.Rows.Count - 1;
                                c1ProviderProblemType.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                c1ProviderProblemType.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                c1ProviderProblemType.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));
                                if (chkRecurring.Checked == false)
                                {
                                    c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                    c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                }
                                else
                                {
                                    c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                                    c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                                }

                                for (int k = 0; k < oListControl.SelectedItems[i].SubItems.Count; k++)
                                {
                                    bool _isResourceAdded = false;
                                    for (int l = 1; l < c1Resources.Rows.Count; l++)
                                    {
                                        if (Convert.ToInt64(c1Resources.GetData(l, COL_ID)) == Convert.ToInt64(oListControl.SelectedItems[i].SubItems[k].ID))
                                        {
                                            _isResourceAdded = true;
                                            break;
                                        }
                                    }
                                    if (_isResourceAdded == true)
                                        continue;

                                    c1Resources.Rows.Add();
                                    Int32 ResorceRowIndex = c1Resources.Rows.Count - 1;
                                    c1Resources.SetData(ResorceRowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].ID));
                                    c1Resources.SetData(ResorceRowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].Code));
                                    c1Resources.SetData(ResorceRowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].Description));
                                    if (chkRecurring.Checked == false)
                                    {
                                        c1Resources.SetData(ResorceRowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                        c1Resources.SetData(ResorceRowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                    }
                                    else
                                    {
                                        c1Resources.SetData(ResorceRowIndex, COL_STARTTIME, dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                                        c1Resources.SetData(ResorceRowIndex, COL_ENDTIME, dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (c1Resources.Rows.Count > 0)
                            {
                                c1Resources.Rows.Count = 1;
                                //c1Resources.Rows.RemoveRange (1, c1Resources.Rows.Count -1);   
                            }
                        }
                    }
                    break;
                case gloListControl.gloListControlType.Resources:
                    {
                        c1Resources.Rows.Count = 1;
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            //c1Resources.Rows.Count = 1;
                            for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                            {
                                c1Resources.Rows.Add();
                                Int32 RowIndex = c1Resources.Rows.Count - 1;
                                c1Resources.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                c1Resources.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                c1Resources.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));
                                if (chkRecurring.Checked == false)
                                {
                                    c1Resources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                    c1Resources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                }
                                else
                                {
                                    c1Resources.SetData(RowIndex, COL_STARTTIME, dtpRec_DateTime_StartTime.Value.ToShortTimeString());
                                    c1Resources.SetData(RowIndex, COL_ENDTIME, dtpRec_DateTime_EndTime.Value.ToShortTimeString());
                                }
                            }
                        }

                    }
                    break;
                case gloListControl.gloListControlType.Coverage:
                    {
                       
                        cmbApp_Coverage.DataSource = null;
                        cmbApp_Coverage.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbApp_Coverage.DataSource = oBindTable;
                            cmbApp_Coverage.DisplayMember = "DispName";
                            cmbApp_Coverage.ValueMember = "ID";
                        }
                    }
                    break;
                default:
                    {
                    }
                    break;
            }

            //oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            //oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            //oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
            //oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);

            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = null;


            pnlMainNToolStrip.Visible = true;

            tsb_Recurrence.Enabled = true;
            tsb_OK.Enabled = true;
            tsb_Print.Enabled = true;
            tsb_Email.Enabled = true;
            tsb_Fax.Enabled = true;
            tsb_RegPatient.Enabled = true;
            AssignUserRights();
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            pnlMainNToolStrip.Visible = true;
            tsb_Recurrence.Enabled = true;
            tsb_OK.Enabled = true;
            tsb_Print.Enabled = true;
            tsb_Email.Enabled = true;
            tsb_Fax.Enabled = true;
            tsb_RegPatient.Enabled = true;
            AssignUserRights();

            //oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            //oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            //oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
            //oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);

            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = null;


        }
        void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == " Referral Doctors")
            {

                gloContacts.frmSetupPhysician ofrmAddContact = new gloContacts.frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.ShowDialog(this);
               
                oListControl.FillListAsCriteria(ofrmAddContact.ContactID);
                ofrmAddContact.Dispose();
                ofrmAddContact = null;
            }
        }
        void oListControl_ModifyFormHandlerClick(object sender, EventArgs e)
        {

            if (oListControl.ControlHeader == " Referral Doctors")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                    _sSPIID = Convert.ToString(oListControl.dgListView["sSPI", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    gloContacts.frmSetupPhysician ofrmModifyContact = new gloContacts.frmSetupPhysician(_contactid, _databaseconnectionstring);
                    if (_sSPIID == "")
                    { ofrmModifyContact.CallFrom = "Physician"; }
                    else
                    { ofrmModifyContact.CallFrom = "Direct Physician"; }
                    ofrmModifyContact.ShowDialog(this);
                 
                    oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);
                    ofrmModifyContact.Dispose();
                    ofrmModifyContact = null;
                }

            }
        }

        private void btnApp_DateTime_Color_Click(object sender, EventArgs e)
        {
           // System.Windows.Forms.ColorDialog oColorDialog = new ColorDialog();
            try
            {
                colorDialog1.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                lblApp_DateTime_ColorContainer.BackColor = colorDialog1.Color;
                lblRec_DateTime_ColorContainer.BackColor = colorDialog1.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                }
                catch
                {
                }
            }
            //oColorDialog.Dispose();
            //oColorDialog = null;
        }

        private void cmbApp_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            gloAppointmnetScheduleCommon oApptCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);
            gloGeneralItem.gloItems oListItems = null;
            int _Counter = 0;
            
            cmbApp_Department.DataSource = null;
            cmbApp_Department.Items.Clear();
            try
            {
                //Departments
                if (cmbApp_Location.SelectedItem != null)
                {
                   // oListItems = new gloGeneralItem.gloItems();
                    oListItems = oApptCommon.GetDepartments(Convert.ToInt64(((System.Data.DataRowView)(cmbApp_Location.Items[cmbApp_Location.SelectedIndex])).Row.ItemArray[0]));
                    DataTable oTableDepartments = new DataTable();

                    oTableDepartments.Columns.Add("ID");
                    oTableDepartments.Columns.Add("DispName");

                    //Add blank row
                    DataRow _dtRow = null;
                    _dtRow = oTableDepartments.NewRow();
                    _dtRow["ID"] = 0;
                    _dtRow["DispName"] = "";
                    oTableDepartments.Rows.InsertAt(_dtRow, 0);
                    _dtRow = null;
                    if (oListItems != null)
                    {
                        for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
                        {
                            DataRow oRow;
                            oRow = oTableDepartments.NewRow();
                            oRow[0] = oListItems[_Counter].ID;
                            oRow[1] = oListItems[_Counter].Description;
                            oTableDepartments.Rows.Add(oRow);
                        }
                    }
                    cmbApp_Department.DataSource = oTableDepartments;
                    cmbApp_Department.DisplayMember = "DispName";
                    cmbApp_Department.ValueMember = "ID";
                    cmbApp_Department.SelectedIndex = -1;
                    if (oListItems != null)
                    {
                        oListItems.Clear();
                        oListItems.Dispose();
                        oListItems = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oApptCommon.Dispose();
                oApptCommon = null;
                _Counter = 0;
            }
        }

        private void btnRec_DateTime_Color_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.ColorDialog oColorDialog = new ColorDialog();
            try
            {
                colorDialog1.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                lblRec_DateTime_ColorContainer.BackColor = colorDialog1.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                }
                catch
                {
                }
            }
            //oColorDialog.Dispose();
            //oColorDialog = null;
        }

        #endregion

        #region Mouse Hover And MouseLeave Events


        //These functions are created for MouseHover & MouseLeave Events
        private void MouseHover(Button btn)
        {
            btn.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Yellow;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void MouseLeave(Button btn)
        {
            btn.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnApp_Provider_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_Provider);
        }

        private void btnApp_Provider_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_Provider);
        }

        private void btnApp_Patient_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_Patient);
        }

        private void btnApp_Patient_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_Patient);
        }

        private void btnApp_DateTime_Color_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_DateTime_Color);
        }

        private void btnApp_DateTime_Color_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_DateTime_Color);
        }

        private void btnApp_Procedures_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_Procedures);
        }

        private void btnApp_Procedures_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_Procedures);
        }

        private void btnApp_ReferralDoctor_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ReferralDoctor);
        }

        private void btnApp_ReferralDoctor_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ReferralDoctor);
        }

        private void btnApp_Resources_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_Resources);
        }

        private void btnApp_Resources_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_Resources);
        }

        private void btnApp_Coverage_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_Insurance);
        }

        private void btnApp_Coverage_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_Insurance);
        }

        private void btnRec_DateTime_Color_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnRec_DateTime_Color);
        }

        private void btnRec_DateTime_Color_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnRec_DateTime_Color);
        }

        private void btnAdd_PriorAuthorization_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnAdd_PriorAuthorization);
        }

        private void btnAdd_PriorAuthorization_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnAdd_PriorAuthorization);
        }

        private void btnRemove_PriorAuthorization_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnRemove_PriorAuthorization);
        }

        private void btnRemove_PriorAuthorization_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnRemove_PriorAuthorization);
        }
        
        private void btnRec_Save_MouseHover(object sender, EventArgs e)
        {
            btnRec_Save.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_ButtonHover;
            btnRec_Save.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnRec_Save_MouseLeave(object sender, EventArgs e)
        {
            btnRec_Save.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_2003DarkHeader;
            btnRec_Save.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRec_Close_MouseHover(object sender, EventArgs e)
        {
            btnRec_Close.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_CloseYellow;
            btnRec_Close.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnRec_Close_MouseLeave(object sender, EventArgs e)
        {
            btnRec_Close.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_CloseBlue;
            btnRec_Close.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnSearchFirstAppt_MouseHover(object sender, EventArgs e)
        {
            MouseHover(((Button)sender));
        }

        private void btnSearchFirstAppt_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(((Button)sender));
        }

        #endregion

        #region "ToolBar Events"

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            ShowAppointmentRecurrence(false);
        }

        private void tsb_ShowRecurrence_Click(object sender, EventArgs e)
        {
            
            //Setting End time 
            TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numRec_DateTime_Duration.Value) * 600000000);
            dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));
            ValidateResourceTime();

            FindRecurrence();
            ArrayList _FindDates = new ArrayList();
            _FindDates = oFindCriteria.Dates;

            ArrayList _ScheduleStatus = new ArrayList();
            _ScheduleStatus = oFindCriteria.ScheduleStatus;

            lvwRec_Apointments.Items.Clear();

            for (int i = 0; i <= _FindDates.Count - 1; i++)
            {
                ListViewItem oItem = new ListViewItem();
                oItem.Text = Convert.ToString(i + 1); //0. Serial Number
                oItem.SubItems.Add(Convert.ToDateTime(_FindDates[i].ToString()).ToString("MM/dd/yyyy")); //1. Start Date
                oItem.SubItems.Add(dtpRec_DateTime_StartTime.Value.ToShortTimeString()); //2. Start Time
                oItem.SubItems.Add(Convert.ToDateTime(_FindDates[i].ToString()).ToShortDateString()); //3. End Date
                oItem.SubItems.Add(dtpRec_DateTime_EndTime.Value.ToShortTimeString()); //4. End Time
                oItem.SubItems.Add(numRec_DateTime_Duration.Value.ToString()); //5. Duration
                oItem.SubItems.Add(lblRec_DateTime_ColorContainer.BackColor.ToArgb().ToString()); //6. Color Code
                oItem.SubItems.Add("0"); //7. Status ID
                oItem.SubItems.Add("0"); //8. ID
                oItem.SubItems.Add("0"); //9. ID
                oItem.SubItems.Add(_ScheduleStatus[i].ToString()); //10. Status  // Added by pranit to show schedule status
                lvwRec_Apointments.Items.Add(oItem);
                if (_ScheduleStatus[i].ToString() == "Blocked")   
                {
                    lvwRec_Apointments.Items[i].ForeColor = Color.Red;
                    lvwRec_Apointments.Items[i].Font = gloGlobal.clsgloFont.getFontFromExistingSource(lvwRec_Apointments.Font, FontStyle.Strikeout);
                }
                oItem = null;
            }

            //Set Controls
            if (lvwRec_Apointments.Items.Count > 0)
            {
            }
            else
            {
                tsb_RemoveRecurrence.Visible = false;

                chkRecurring.Checked = false;
            }
        }

        //'Get patient status to display in demographics information 
        //'i.e. whether patient is active ,deceased, dead etc....
        private string GetPatientStaus(Int64 PatientID)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //string strSQL = null;
            //string Status = null;
            //strSQL = "SELECT ISNULL(sPatientStatus,'') AS sPatientStatus FROM Patient WHERE  nPatientID = " + PatientID + "";
            //oDB.Connect(false);
            //Status = (oDB.ExecuteScalar_Query(strSQL).ToString());
            //oDB.Disconnect();
            //oDB = null;
            //if (string.IsNullOrEmpty(Status))
            //{
            //    return "";
            //}
            //else
            //{
            //    return Status;

            //}
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            //DataTable dtstatus =  null;
            string strStatus = "";

            try
            {
                oDB.Connect(false);
                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                strStatus = Convert.ToString(oDB.ExecuteScalar("gsp_GetPatientStatus", oParameters));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            if (string.IsNullOrEmpty(strStatus))
            {
                return "";
            }
            else
            {
                return strStatus;

            }
         
        }
        private void tsb_ApplyRecurrence_Click(object sender, EventArgs e)
        {
            FindRecurrence();

            ArrayList _FindDates = new ArrayList();
            _FindDates = oFindCriteria.Dates;

            ArrayList _ScheduleStatus = new ArrayList();
            _ScheduleStatus = oFindCriteria.ScheduleStatus;


            lvwRec_Apointments.Items.Clear();

            for (int i = 0; i <= _FindDates.Count - 1; i++)
            {
                ListViewItem oItem = new ListViewItem();
                oItem.Text = Convert.ToString(i + 1); //0. Serial Number
                oItem.SubItems.Add(Convert.ToDateTime(_FindDates[i].ToString()).ToString("MM/dd/yyyy")); //1. Start Date
                oItem.SubItems.Add(dtpRec_DateTime_StartTime.Value.ToShortTimeString()); //2. Start Time
                oItem.SubItems.Add(Convert.ToDateTime(_FindDates[i].ToString()).ToShortDateString()); //3. End Date

                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numRec_DateTime_Duration.Value) * 600000000);
                dtpRec_DateTime_EndTime.Value = dtpRec_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));


                oItem.SubItems.Add(dtpRec_DateTime_EndTime.Value.ToShortTimeString()); //4. End Time
                oItem.SubItems.Add(numRec_DateTime_Duration.Value.ToString()); //5. Duration
                oItem.SubItems.Add(lblRec_DateTime_ColorContainer.BackColor.ToArgb().ToString()); //6. Color Code
                oItem.SubItems.Add("0"); //7. Status ID
                oItem.SubItems.Add("0"); //8. ID
                oItem.SubItems.Add("0"); //9. ID
                oItem.SubItems.Add(_ScheduleStatus[i].ToString()); //10. Status  // Added by pranit to show schedule status
                lvwRec_Apointments.Items.Add(oItem);
                if (_ScheduleStatus[i].ToString() == "Blocked")
                {
                    lvwRec_Apointments.Items[i].ForeColor = Color.Red;
                    lvwRec_Apointments.Items[i].Font = gloGlobal.clsgloFont.getFontFromExistingSource(lvwRec_Apointments.Font, FontStyle.Strikeout);
                }
                oItem = null;
            }
            // END in Show Recurrence

            if (lvwRec_Apointments.Items.Count > 0)
            {
                tsb_RemoveRecurrence.Visible = true;
                string _RecText = "";

                _RecText = "Start Date: " + dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy") + "  End Date: " + dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy") + "  Start Time: " + dtpRec_DateTime_StartTime.Value.ToShortTimeString() + "  Duration: " + numRec_DateTime_Duration.Value.ToString() + "  Occurences: " + numRec_Range_EndAfterOccurence.Value.ToString();

                lblApp_Recurrence_Time.Text = _RecText;
                chkRecurring.Checked = true;
                pnlApp_DateTimeContainer.Visible = false;
                lblApp_Recurrence_Time.Visible = true;
            }
            else
            {
                tsb_RemoveRecurrence.Visible = false;

                lblApp_Recurrence_Time.Text = "";
                lvwRec_Apointments.Items.Clear();
                lvwRec_Exception.Items.Clear();
                pnlApp_DateTimeContainer.Visible = true;
                lblApp_Recurrence_Time.Visible = false;

                chkRecurring.Checked = false;
            }
            ValidateResourceTime();
            ShowAppointmentRecurrence(false);
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            

            //added for resource appointment 
            ts_Commands.Focus();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);


            oDB.Connect(false);

            if (cmbApp_Provider.Text.Trim() == "" && c1Resources.Rows.Count > 1)
            {
                _IsResourceAppointment = true;
            }
           
            //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //if (oSecurity.isBadDebtPatient(_nPatientId, true))
            if (isBaddebtPatient)
            {

                DialogResult dr = System.Windows.Forms.MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to create a new appointment?", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr.ToString() == "No")
                {
                    return;
                }
            }
            //if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }

            if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true)
            {
                gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                // for getting HL7 message name is this action for S12/S13
                if (SaveAppointment() > 0)
                {
                    this.Close();
                }
            }
            else
            {
                gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                // for getting HL7 message name is this action for S12/S13
                if (UpdateAppointment() == true)
                {
                    this.Close();
                }
            }
            if ((_IsDateChanged == true && ogloAppointment.IsPatientCheckOut(_SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID) == true) || (_IsDateChanged == true && ogloAppointment.IsPatientCheckIn(_SetAppointmentParameter.MasterAppointmentID, _SetAppointmentParameter.AppointmentID) == true))
            {
                string _sqlQuery = " UPDATE AS_Appointment_DTL SET nUsedStatus = 1 WHERE nMSTAppointmentID = " + _SetAppointmentParameter.MasterAppointmentID + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString()) + " AND nClinicID = " + _SetAppointmentParameter.ClinicID + " ";
                oDB.Execute_Query(_sqlQuery);

                _sqlQuery = " UPDATE PatientTracking SET bIsCheckOut = 1 WHERE nDTLAppointmentID = " + _SetAppointmentParameter.AppointmentID + " AND nClinicID = " + _SetAppointmentParameter.ClinicID + " ";
                oDB.Execute_Query(_sqlQuery);
            }

        }

        private System.Windows.Forms.DialogResult CheckPriorAuthorization(Int64 nPatientID)
        {
            DialogResult dialog=DialogResult.OK;
            try
            {
                string sMsg = string.Empty;
                string sAuthFor = string.Empty;
                if (PriorAuthRequired(nPatientID, out sAuthFor) && (string.IsNullOrEmpty(txtPriorAuthorizationNo.Text) || txtPriorAuthorizationNo.Text == "<available>"))
                {
                    string[] strAuthsplit = sAuthFor.Split(',');
                    switch (strAuthsplit[0])
                    {
                        case "Insurance":
                            string sPRInsurance = string.Empty;
                            if (strAuthsplit.Length > 1)
                            {
                                sPRInsurance = strAuthsplit[1];
                            }
                            sMsg = string.Format("Patient insurance(s) requires Prior Authorization.\n\nInsurance: \n{0}\nContinue appointment registration without prior authorization?", sPRInsurance);
                            break;
                        case "ApptType":
                            sMsg = string.Format("Appointment type \"{0}\" requires Prior Authorization.\n\nContinue appointment registration without prior authorization?", cmbApp_AppointmentType.Text.ToString());
                            break;
                    } 
                }

                if (!string.IsNullOrEmpty(sMsg))
                    dialog = MessageBox.Show(sMsg, _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                else
                    dialog = DialogResult.OK;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
            return dialog;
        }
        private Boolean PriorAuthRequired(Int64 nPatientID, out string sPriorAuth) 
        { 
            Boolean bIsPriorAuthRequired=false;
            sPriorAuth="";
            DataTable _dtInsurances = null;
            DataTable _dtAppointment = null;
            gloAppointmentBook.Books.AppointmentType oAppointmentType = null;
            try
            {
                string sPRInsurance = string.Empty;
                Boolean bIsPriorAuthReq_Insurance = false;
                Boolean bIsPriorAuthReq_ApptType = false;

                oAppointmentType = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);

                _dtInsurances = clsgloPriorAuthorization.GetAllActiveInsurancePlan(nPatientID);
                _dtAppointment = oAppointmentType.GetList(gloAppointmentBook.AppointmentProcedureType.AppointmentType);

                if (_dtInsurances != null && _dtInsurances.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtInsurances.Rows)
                    {
                        //sActiveInsurance = sActiveInsurance + "- " + Convert.ToString(dr["sInsuranceName"]) + "\n";
                        if (Convert.ToBoolean(dr["bIsPARequired"]) == true)
                        {
                            sPRInsurance = sPRInsurance + "- " + Convert.ToString(dr["sInsuranceName"]) + "\n";
                        }
                    }
                }
                if (string.IsNullOrEmpty(sPRInsurance) == false)
                {
                    bIsPriorAuthReq_Insurance = true;
                }
                if ( (_dtAppointment != null) && (_dtAppointment.Rows.Count > 0) )
                {
                    if (cmbApp_AppointmentType.SelectedValue != null)
                    {
                        DataRow[] drAppointment = _dtAppointment.Select("nAppointmentTypeID='" + cmbApp_AppointmentType.SelectedValue.ToString() + "'");
                        if (drAppointment != null && drAppointment.Length > 0)
                        {
                            foreach (DataRow dr in drAppointment)
                            {
                                if (Convert.ToString(dr["bIsPriorAuthRequired"]).ToLower() == "true")
                                {
                                    bIsPriorAuthReq_ApptType = true;
                                }
                            }
                        }
                    }
                }
                if (bIsPriorAuthReq_Insurance)
                {
                    sPriorAuth = "Insurance"+","+sPRInsurance;
                    bIsPriorAuthRequired=true;
                }
                else if (bIsPriorAuthReq_ApptType)
                {
                    sPriorAuth = "ApptType";
                    bIsPriorAuthRequired = true;
                }
                else
                {
                    sPriorAuth = "";
                    bIsPriorAuthRequired = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dtInsurances!=null){ _dtInsurances.Dispose(); _dtInsurances = null; }
                if (_dtAppointment != null) { _dtAppointment.Dispose(); _dtAppointment = null; }
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
            }

            return bIsPriorAuthRequired;
        }
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {

            DialogResult dgResult = DialogResult.None;

            dgResult = MessageBox.Show("Do you want to save the changes? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dgResult == DialogResult.Yes)
            {
                if (cmbApp_Provider.Text.Trim() == "" && c1Resources.Rows.Count > 1)
                {
                    _IsResourceAppointment = true;
                }

                if (_SetAppointmentParameter != null)
                {
                    if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true)
                    {
                        gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                        // for getting HL7 message name is this action for S12/S13
                        if (SaveAppointment() > 0)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                        // for getting HL7 message name is this action for S12/S13
                        if (UpdateAppointment() == true)
                        {
                            this.Close();
                        }
                    }

                }
            }
            else if (dgResult == DialogResult.No)
            {
                if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == false)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.View, "View Appointment", 0, _SetAppointmentParameter.MasterAppointmentID, 0, ActivityOutCome.Success);
                }
                this.Close();
            }
            else
            {

            }

        }
        private void tsb_Print_Click(object sender, EventArgs e)
        {

            if (_SetAppointmentParameter.AddTrue_ModifyFalse_Flag == true)
            {
                gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Print;
                // for getting HL7 message name is this action for S12/S13
                _PrintAppointmentID = SaveAppointment();
                _IsAppointmentPrinted = true;
            }
            else
            {
                gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Print;
                // for getting HL7 message name is this action for S12/S13
                if (UpdateAppointment() == true)
                {
                    _PrintAppointmentID = _SetAppointmentParameter.AppointmentID;
                    _IsAppointmentPrinted = true;
                }
                else
                {
                    return;
                }
            }



            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
            DataTable dtAssociation;

            try
            {
                if (_PrintAppointmentID > 0)
                {
                    dtAssociation = ogloTemplate.GetAssociation(gloOffice.AssociationCategories.AppointmentPrint);
                    Int64 _TemplateID = 0;

                    if (dtAssociation != null && dtAssociation.Rows.Count > 0)
                    {
                        _TemplateID = Convert.ToInt64(dtAssociation.Rows[0]["nTemplateID"]);
                    }
                    if (_TemplateID > 0)
                    {

                        ogloTemplate.TemplateID = _TemplateID;
                        ogloTemplate.PrimeryID = _PrintAppointmentID;
                        ogloTemplate.ClinicID = _clinicID;
                        ogloTemplate.PatientID = Convert.ToInt64(txtApp_Patient.Tag.ToString());

                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring, ogloTemplate);
                        frm.Show();
                        frm.WindowState = FormWindowState.Maximized;

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.Print, "Appointment Template Viewed", Convert.ToInt64(txtApp_Patient.Tag.ToString()), 0, 0, ActivityOutCome.Success);
                    }
                    else
                    {
                        MessageBox.Show("Appointment template is not associated, Use Edit->Template Association.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Email_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Fax_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Recurrence_Click(object sender, EventArgs e)
        {
            ShowAppointmentRecurrence(true);
        }

        private void tsb_RemoveRecurrence_Click(object sender, EventArgs e)
        {
            if (lvwRec_Apointments.Items.Count > 0)
            {
                if (chkRecurring.Checked == true)
                {
                    if (MessageBox.Show("Are you sure, you want to clear this recurring appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tsb_Recurrence.Visible = true;
                        tsb_RemoveRecurrence.Visible = false;

                        lblApp_Recurrence_Time.Text = "";
                        lblApp_Recurrence_Time.Tag = "";

                        pnlApp_DateTimeContainer.Visible = true;
                        lblApp_Recurrence_Time.Visible = false;

                        lvwRec_Apointments.Items.Clear();
                        lvwRec_Exception.Items.Clear();

                        chkRecurring.Checked = false;
                        
                        ClearRecurrence();                       

                        ShowAppointmentRecurrence(false);
                        ValidateResourceTime();


                    }
                }
            }
        }

        private void tsb_Help_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Search_Click(object sender, EventArgs e)
        {

        }

        private void tsb_RegPatient_Click(object sender, EventArgs e)
        {
           // gloGeneralItem.gloItem ogloItem = new gloItem();
           // gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
            //Quick Patient Registration
            try
            {

                gloPatient.frmSetupQuickPatient ofrmSetupQuickPatient = new gloPatient.frmSetupQuickPatient(_databaseconnectionstring);
                if (cmbApp_Provider.SelectedIndex != -1)
                {
                    ofrmSetupQuickPatient.ProviderName = cmbApp_Provider.Text.Trim();
                    ofrmSetupQuickPatient.ProviderID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                }
                ofrmSetupQuickPatient.ShowDialog(this);

                //Patient name get clear after quick patient registration.
                
                cmbApp_ReferralDoctor.DataSource = null;
                cmbApp_ReferralDoctor.Items.Clear();
                if (ofrmSetupQuickPatient.ReturnPatientID > 0)
                {
                    txtApp_Patient.Tag = ofrmSetupQuickPatient.ReturnPatientID;
                    txtApp_Patient.Text = ofrmSetupQuickPatient.ReturnPatientName;

                    UpdatedPatientID = ofrmSetupQuickPatient.ReturnPatientID;
                    UpdatedPriorAuthorizationID = 0;

                    txtPriorAuthorizationNo.Text = "";
                    txtPriorAuthorizationNo.Tag = null;
                    this.Text = "Appointment";
                    try
                    {
                        gloPatient.gloPatient.GetWindowTitle(this, ofrmSetupQuickPatient.ReturnPatientID, _databaseconnectionstring, _MessageBoxCaption);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }

                }
                ofrmSetupQuickPatient.Dispose();
                ofrmSetupQuickPatient = null;
                //(Refreshing Patient Alert)
                FillPatientAlert();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Get Patient Balance (Billing) "

        private DataTable GetPatientBalance(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtBalance = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = "SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                        + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                        + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription, "
                        + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description, "
                        + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description, "
                        + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description, "
                        + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description, "
                        + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                        + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                        + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                        + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount FROM   BL_Transaction_Payment_DTL "
                        + " WHERE  (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                        + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) AS BalanceAmount "
                        + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                        + " WHERE (BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount  FROM  BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1  "
                        + " WHERE (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                        + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) > 0) AND (BL_Transaction_MST.nPatientID = " + PatientID + ") AND (BL_Transaction_Lines.nClinicID = " + _clinicID + ") "
                        + " UNION "
                        + " SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                        + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                        + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription,   "
                        + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description,   "
                        + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description,   "
                        + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description,   "
                        + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description,   "
                        + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                        + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                        + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                        + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed AS BalanceAmount  "
                        + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                        + " WHERE (BL_Transaction_MST.nTransactionID  NOT IN (SELECT nTransactionID FROM BL_Transaction_Payment_DTL WHERE nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _clinicID + ") "
                        + " AND BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _clinicID + ") "
                        + " ORDER BY BL_Transaction_Lines.nFromDate DESC ";

                oDB.Retrive_Query(strSQL, out dtBalance);
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
                    oDB = null;
                }
            }
            return dtBalance;
        }

        private Decimal GetPatientTotalBalance(Int64 PatientID)
        {
            DataTable dtBalance = new DataTable();
            Decimal _dBalance = 0;
            try
            {
                dtBalance = GetPatientBalance(PatientID);

                if (dtBalance != null)
                {
                    for (int i = 0; i < dtBalance.Rows.Count; i++)
                    {
                        _dBalance += Convert.ToDecimal(dtBalance.Rows[i]["BalanceAmount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return _dBalance;
        }

        #endregion

        #region " Combo Events "
        #endregion " Combo Events "

        #region "Designer Events"

        private void btnApp_ClearProvider_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearProvider);

        }

        private void btnApp_ClearProvider_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearProvider);
        }

        private void btnApp_ClearPatient_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearPatient);
        }

        private void btnApp_ClearPatient_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearPatient);
        }

        private void btnApp_ClearReferralDoctor_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearReferralDoctor);
        }

        private void btnApp_ClearReferralDoctor_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearReferralDoctor);
        }

        private void btnApp_ClearDateTime_Color_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearDateTime_Color);
        }

        private void btnApp_ClearDateTime_Color_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearDateTime_Color);
        }

        private void btnApp_ClearProcedures_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearProcedures);
        }

        private void btnApp_ClearProcedures_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearProcedures);
        }

        private void btnApp_ClearResources_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearResources);
        }

        private void btnApp_ClearResources_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearResources);
        }

        private void btnApp_ClearInsurance_MouseHover(object sender, EventArgs e)
        {
            MouseHover(btnApp_ClearInsurance);
        }

        private void btnApp_ClearInsurance_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(btnApp_ClearInsurance);
        }

        #endregion

        #region "Prior Authorization"

        private void btnAdd_PriorAuthorization_Click(object sender, EventArgs e)
        {
            //if condition added by dipak 20091229to fix bug No : 5171	Appointment>without selecting any patient, on clicking authorization
            if (txtApp_Patient.Tag == null)
            {
                MessageBox.Show("Select patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtApp_Patient.Focus();
                return;
            }

            try
            {
                Int64 _patientID = Convert.ToInt64(txtApp_Patient.Tag);
                Int64 _asOfDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtpApp_DateTime_StartDate.Value));
                Int64 _paID = 0;
                string _strSQL = "";
                DataTable dt = new DataTable();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                if (txtPriorAuthorizationNo.Tag != null && Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                {
                    _paID = Convert.ToInt64(txtPriorAuthorizationNo.Tag);
                    CurrentPriorAuthorizationID = _paID;
                }

                if (clsgloPriorAuthorization.HasPriorAuthorization(_patientID))
                {
                    // If patient has prior authorizations then display PA selection screen
                    using (gloPMGeneral.frmShowPriorAuthorization oPriorAuthorization = new gloPMGeneral.frmShowPriorAuthorization(_databaseconnectionstring, _asOfDate, _paID, _patientID))
                    {
                        oPriorAuthorization.ShowDialog(this);
                        if (oPriorAuthorization.CurrentPriorAuthorization != 0)
                        {
                            txtPriorAuthorizationNo.Tag = oPriorAuthorization.CurrentPriorAuthorization;
                            txtPriorAuthorizationNo.Text = oPriorAuthorization.CurrentPriorAuthorizationNo;
                        }
                        _strSQL = " SELECT ISNULL(nContactId,0) AS nContactId ,sReferralName=DBO.GET_NAME_WithPrefix(sFirstName,sMiddleName,sLastName,sPrefix,sDegree) "
                            + " FROM  Patient_DTL "
                            + " WHERE  nPatientID = " + _patientID + " AND nClinicID = " + _clinicID + " AND nContactFlag = 3 ORDER BY sReferralName ";
                        DataTable dtReferral;
                        oDB.Retrive_Query(_strSQL, out dtReferral);
                        {
                            
                            cmbApp_ReferralDoctor.DataSource = null;
                            cmbApp_ReferralDoctor.Items.Clear();
                            if (dtReferral != null && dtReferral.Rows.Count > 0)
                            {
                                cmbApp_ReferralDoctor.DisplayMember = "sReferralName";
                                cmbApp_ReferralDoctor.ValueMember = "nContactId";
                                cmbApp_ReferralDoctor.DataSource = dtReferral;
                                cmbApp_ReferralDoctor.SelectedIndex = 0;
                            }
                        }
                        oDB.Disconnect();

                        cmbApp_ReferralDoctor.SelectedValue = oPriorAuthorization.CurrentReferralProviderID;
                    }
                }
                else
                {
                    // If patient does not have prior authorizations then display PA Setup screen
                    using (gloPMGeneral.frmSetupAuthorization objSetupAuthorization = new gloPMGeneral.frmSetupAuthorization(_patientID))
                    {
                        objSetupAuthorization.ShowDialog(this);
                        txtPriorAuthorizationNo.Tag = objSetupAuthorization._PriorAuthorizationID;
                        txtPriorAuthorizationNo.Text = Convert.ToString(objSetupAuthorization._PriorAuthorizationNo);
                    }
                }

                if (Convert.ToInt64(txtPriorAuthorizationNo.Tag) > 0)
                {
                    if (!getPriorAuthorizationStatus(Convert.ToInt64(txtPriorAuthorizationNo.Tag)))
                    {
                        txtPriorAuthorizationNo.Tag = null;
                        txtPriorAuthorizationNo.Text = "";
                        //Bug #99045: Application showing exception on appointment save after deactivating prior auth.
                        UpdatedPriorAuthorizationID = 0;
                        //CheckForPA();
                    }
                }

                if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
                { UpdatedPriorAuthorizationID = Convert.ToInt64(txtPriorAuthorizationNo.Tag); }



                CheckForPA();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

        }

        private void btnRemove_PriorAuthorization_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(txtPriorAuthorizationNo.Tag) != "")
            {
                UpdatedPriorAuthorizationID = 0;
            }
            txtPriorAuthorizationNo.Text = "";
            txtPriorAuthorizationNo.Tag = null;

            CheckForPA();
        }

        private bool getPriorAuthorizationStatus(Int64 _PaID)
        {
            object count = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string sqlstring = "SELECT count(*) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPriorAuthorizationID='" + _PaID + "' AND bIsActive=1";
            oDB.Connect(false);
            count = oDB.ExecuteScalar_Query(sqlstring);
            oDB.Disconnect();
            if (Convert.ToInt64(count) > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ViewPriorAuthorization(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_PriorAuthorization = null;
            try
            {
                string Sqlquery = "SELECT   ISNULL(nAuthorizationID,0)AS nAuthorizationID, ISNULL(nPatientID,0) AS  nPatientID, " +
                "ISNULL(nInsuranceID,0) AS nInsuranceID,ISNULL(sInsuranceName,'') AS sInsuranceName, " +
                "dtAuthorization, dtAuthorizationThrough, ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber,  " +
                "ISNULL(nTotalVisits,0) AS nTotalVisits,  ISNULL(nVisitsMade,0) AS nVisitsMade, ISNULL(nAppointmentType,0) AS nAppointmentType,  " +
                "ISNULL(sAuthorizationStatus,'') AS sAuthorizationStatus,  dtAuthorizationStatus " +
                "FROM PatientPriorAuthorization WHERE nPatientID= " + PatientID + "  ORDER BY nAuthorizationID ";

                oDB.Retrive_Query(Sqlquery, out dt_PriorAuthorization);
                if (dt_PriorAuthorization != null)
                {
                    return dt_PriorAuthorization;

                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }

        }

        private void SetDefaultPASetting(Int64 nPatientID)
        {
            try
            {
                //condition added to check Prior Auth in the sequence of All active insurance plan, Appt. Type and then Provider setting in admin
                string sAuthFor = string.Empty;
                bool bIsPriorAuthreq = PriorAuthRequired(nPatientID, out sAuthFor);
                if (sAuthFor != "")
                {
                    if (sAuthFor.Contains("ApptType"))
                    {
                        if (bIsPriorAuthreq)
                            chkPARequired.Checked = true;
                        else
                            chkPARequired.Checked = false;
                    }
                    else
                    {
                        if (bIsPriorAuthreq)
                            chkPARequired.Checked = true;
                        else
                            chkPARequired.Checked = false;
                    }
                }
                else
                {
                    chkPARequired.Checked = false;
                }
                if (chkPARequired.Checked == false)
                {
                    if (cmbApp_Provider.SelectedIndex >= 0)
                    {
                        Int64 _providerID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        //Int64 _patientID = Convert.ToInt64(txtApp_Patient.Tag);

                        chkPARequired.Checked = clsgloPriorAuthorization.GetProviderSettingForPA(_providerID, nPatientID);

                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        private void GetDefaultResource()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtResource = new DataTable();
            try
            {
                oDB.Connect(false);
                if (_SetAppointmentParameter.Resources != null)
                {
                    if (_SetAppointmentParameter.Resources.Count == 0)
                    {
                        strSQL = " SELECT ISNULL(nResourceID,0) AS ID ,ISNULL(sCode,'') AS Code ,ISNULL(sDescription,'') AS Description  FROM AB_resource_MST  WHERE nResourceID =" + _SetAppointmentParameter.ProviderID + "";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < _SetAppointmentParameter.Resources.Count; i++)
                        {
                            if (i == 0)
                            {
                                sb.Append(Convert.ToString(_SetAppointmentParameter.Resources[i]));
                            }
                            else
                            {
                                sb.Append("," + Convert.ToString(_SetAppointmentParameter.Resources[i]));

                            }

                        }
                        strSQL = " SELECT ISNULL(nResourceID,0) AS ID ,ISNULL(sCode,'') AS Code ,ISNULL(sDescription,'') AS Description  FROM AB_resource_MST  WHERE nResourceID  IN  (  " + sb.ToString() + " ) ";

                    }
                }
                else
                {
                    strSQL = " SELECT ISNULL(nResourceID,0) AS ID ,ISNULL(sCode,'') AS Code ,ISNULL(sDescription,'') AS Description  FROM AB_resource_MST  WHERE nResourceID =" + _SetAppointmentParameter.ProviderID + "";


                }

                oDB.Retrive_Query(strSQL, out dtResource);
                if (dtResource.Rows.Count > 0)
                {
                    for (int i = 0; i < dtResource.Rows.Count; i++)
                    {
                        c1Resources.Rows.Add();
                        Int32 RowIndex = c1Resources.Rows.Count - 1;
                        c1Resources.SetData(RowIndex, COL_ID, Convert.ToInt64(dtResource.Rows[i]["ID"]));
                        c1Resources.SetData(RowIndex, COL_CODE, Convert.ToString(dtResource.Rows[i]["Code"]));
                        c1Resources.SetData(RowIndex, COL_DESC, Convert.ToString(dtResource.Rows[i]["Description"]));
                        c1Resources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                        c1Resources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                    }

                }




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
                    oDB = null;
                }
            }

        }
        private void pnlRec_Pattern_Daily_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlAppointment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtApp_Patient_TextChanged(object sender, EventArgs e)
        {
            string str = txtApp_Patient.Text;
            ShowTotalBalance();
        }

        private void txtApp_Notes_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 20100113 Mahesh Nawal Not allow the enter this "|" character   Bug No 494 
            if (e.KeyChar == Convert.ToChar(124))
            {
                e.Handled = true;
            }
        }

        private void cmbApp_ReferralDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetDefaultPASetting();
        }

        private void cmbApp_Provider_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultPASetting(Convert.ToInt64(txtApp_Patient.Tag));
            if (cmbApp_Provider.Text != "" && c1Resources.Rows.Count <= 1)
            {
                _IsResourceAppointment = false;
            }
        }

        private void tmrCopayAlertBlink_Tick(object sender, EventArgs e)
        {
            tmrCopayAlertBlink.Enabled = false; 
            int COL_COPAYALERT_ALERTTEXT = 3;
            if (Convert.ToString(c1CopayAlert.GetData(0, COL_COPAYALERT_ALERTTEXT)) == "Copay")
            {
                if (_IsColored)
                {
                    for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                    {
                        c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "ChildItemRegularRevised");
                    }
                    _IsColored = false;
                }
                else
                {
                    for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                    {
                        c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "Default");
                    }
                    _IsColored = true;
                }
            }
            tmrCopayAlertBlink.Enabled = true;
        }

        #region "Alerts Panel Designer Code "


        private void btnUpPM_MouseHover(object sender, EventArgs e)
        {
            MouseHover(((Button)sender));
        }

        private void btnUpPM_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(((Button)sender));
        }

        private void btnDownPM_MouseHover(object sender, EventArgs e)
        {
            MouseHover(((Button)sender));
        }

        private void btnDownPM_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(((Button)sender));
        }

        private void btnUpEMR_MouseHover(object sender, EventArgs e)
        {
            MouseHover(((Button)sender));
        }

        private void btnUpEMR_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(((Button)sender));
        }

        private void btnDownEMR_MouseHover(object sender, EventArgs e)
        {
            MouseHover(((Button)sender));
        }

        private void btnDownEMR_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave(((Button)sender));
        }



        #endregion

        private void btnDownPM_Click(object sender, EventArgs e)
        {
            pnlgloPMAlerts.Visible = true;
            btnDownPM.Visible = false;
            btnUpPM.Visible = true;
        }

        private void btnUpPM_Click(object sender, EventArgs e)
        {
            pnlgloPMAlerts.Visible = false;
            btnDownPM.Visible = true;
            btnUpPM.Visible = false;
        }

        private void btnUpEMR_Click(object sender, EventArgs e)
        {

            pnlgloEMRAlerts.Visible = true;
            pnlEMRCaption.Dock = DockStyle.Top;
            pnlgloPMAlerts.Dock = DockStyle.Top;
            btnDownEMR.Visible = true;
            btnUpEMR.Visible = false;
        }

        private void btnDownEMR_Click(object sender, EventArgs e)
        {
            pnlgloEMRAlerts.Visible = false;
            pnlEMRCaption.Dock = DockStyle.Bottom;
            pnlgloPMAlerts.Dock = DockStyle.Fill;
            btnDownEMR.Visible = false;
            btnUpEMR.Visible = true;
        }

        private void numApp_DateTime_Duration_Leave(object sender, EventArgs e)
        {
            //numApp_DateTime_Duration_ValueChanged(sender,e);
            if (ts_Commands.Focused == true)
            { return; }

            Int64 _OwnerID = 0;
            string _OwnerName = "";
            //string _ResourseID = "";
            Int64 _ResourseID = 0;
            DataTable dt;
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);

            _OwnerID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
            _OwnerName = cmbApp_Provider.Text;
            if (c1Resources.Rows.Count > 1)
            {
                if (c1Resources.GetData(1, 0).ToString() != "")
                {
                    _ResourseID = Convert.ToInt64(c1Resources.GetData(1, 0).ToString());
                }
            }

            DateTime Starttime;
            DateTime Endtime;
            DateTime StartDate;
            decimal duration;
            string ProviderName = "";
            //string ResourceName = "";
            bool isBlocked = false;
            String Location = "";
            
            Starttime = Convert.ToDateTime(dtpApp_DateTime_StartTime.Text);
            Endtime = Starttime.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
            StartDate = Convert.ToDateTime(dtpApp_DateTime_StartDate.Text);
            duration = numApp_DateTime_Duration.Value;
           Location = Convert.ToString (cmbApp_Location.SelectedValue);
            if (cmbApp_Provider.Text != "")
            {
                if (_OwnerID > 0)
                    ProviderName = oResource.GetProviderName(_OwnerID);

                isBlocked = ogloAppointment.BlockedSlots(_OwnerID, Starttime, Endtime, StartDate, Location);
                if (isBlocked == true)
                {
                    if (_OwnerID != 0)
                    {
                        dt = ogloAppointment.ResourseName(_OwnerID, Starttime, Endtime, StartDate, Location);

                    }
                    else
                    {
                        dt = ogloAppointment.ResourseName(_ResourseID, Starttime, Endtime, StartDate, Location);
                    }
                    isBlocked = false;

                    if (dt.Rows.Count >= 1 && dt != null)
                    {
                        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            if (dtpApp_DateTime_StartTime.Focused == true)
                            {
                                dtpApp_DateTime_StartTime.Focus();
                            }
                            else if (numApp_DateTime_Duration.Focused == true)
                            {
                                numApp_DateTime_Duration.Focus();
                            }

                            return;
                        }
                    }
                }
            }
            //TO CHECK WHETHER RESOURSE IS BLOKED FOR THE SELECTED SLOT
            if (c1Resources.Rows.Count > 0)
                isBlocked = ogloAppointment.ResourceBlockedSlots(_ResourseID, Starttime, Endtime, StartDate,Location );
            if (isBlocked == true)
            {
                dt = ogloAppointment.ResourseName(_ResourseID, Starttime, Endtime, StartDate,Location );
                if (dt.Rows.Count > 0 && dt != null)
                {
                    if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        numApp_DateTime_Duration.Focus();
                        return;
                    }
                }
            }

        }

        private void dtpApp_DateTime_StartTime_Leave(object sender, EventArgs e)
        {
            if (ts_Commands.Focused == true)
            { return; }

            Int64 _OwnerID = 0;
            string _OwnerName = "";
            Int64 _ResourseID = 0;
            string ProviderName = "";

            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);

            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);


            _OwnerID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
            _OwnerName = cmbApp_Provider.Text;


            DateTime Starttime;
            DateTime Endtime;
            DateTime StartDate;
            decimal duration;
            bool isBlocked = false;
            bool isBlockFound = false;
            DataTable dt;


            Starttime = Convert.ToDateTime(dtpApp_DateTime_StartTime.Text);
            Endtime = Starttime.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
            StartDate = Convert.ToDateTime(dtpApp_DateTime_StartDate.Text);
            duration = numApp_DateTime_Duration.Value;
            String Location = "";
            Location = Convert.ToString(cmbApp_Location.SelectedValue);

            if (cmbApp_Provider.Text != "")
            {
                if (_OwnerID > 0)
                    isBlocked = ogloAppointment.BlockedSlots(_OwnerID, Starttime, Endtime, StartDate,Location );
                if (isBlocked == true)
                {
                    isBlocked = false;
                    ProviderName = oResource.GetProviderName(_OwnerID);
                    dt = ogloAppointment.ResourseName(_OwnerID, Starttime, Endtime, StartDate, Location);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            if (dtpApp_DateTime_StartTime.Focused == true)
                            {
                                dtpApp_DateTime_StartTime.Focus();
                            }
                            else if (numApp_DateTime_Duration.Focused == true)
                            {
                                numApp_DateTime_Duration.Focus();
                            }

                            return;
                        }
                    }


                }
            }
            //TO CHECK WHETHER RESOURSE IS BLOKED FOR THE SELECTED SLOT
            //if (c1Resources.Rows.Count > 0)
            if (c1Resources.Rows.Count > 1)
            {
                for (int i = 1; i < c1Resources.Rows.Count; i++)
                {
                    if (c1Resources.GetData(i, 0).ToString() != "")
                    {
                        _ResourseID = Convert.ToInt64(c1Resources.GetData(i, 0).ToString());
                    }
                    isBlocked = ogloAppointment.ResourceBlockedSlots(_ResourseID, Starttime, Endtime, StartDate, Location);
                    if (isBlocked == true)
                    {
                        isBlockFound = true;
                    }
                }

                //}
                if (isBlockFound == true)
                {
                    dt = ogloAppointment.ResourseName(_ResourseID, Starttime, Endtime, StartDate, Location);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + "to" + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            dtpApp_DateTime_StartTime.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void frmSetupAppointment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (oFindCriteria != null)
            {
                oFindCriteria.Dispose();
                oFindCriteria = null;
            }

            if (oTableProvider != null)
            {
                oTableProvider.Dispose();
                oTableProvider = null;
            }

            if (oTableLocations != null)
            {
                oTableLocations.Dispose();
                oTableLocations = null;
            }

            if (oTableAppTypes != null)
            {
                oTableAppTypes.Dispose();
                oTableAppTypes = null;
            }

            if (oTableStatus != null)
            {
                oTableStatus.Dispose();
                oTableStatus = null;
            }

            if (oListControl != null)
            {
                oListControl.Dispose();
                oListControl = null;
            }



            if (oPatientListControl != null)
            {
                oPatientListControl.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                oPatientListControl.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                oPatientListControl.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                oPatientListControl.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);

                oPatientListControl.Dispose();
                oPatientListControl = null;
            }
        }

        // GLO2011-0011476
        // Created a function to read the restricted setting value & set the variable _IsTemplateAppointment
        //private void SetRegisterAppointmentSettings()
        //{
        //    gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
        //    object value = new object();
        //    try
        //    {
        //        ogloSettings.GetSetting("RegisterTemplateAppointmentOnly", out value);
        //        if (value != null && Convert.ToString(value).Trim() != "")
        //        {
        //            if (Convert.ToInt16(value) == 1)
        //            {
        //                _IsTemplateAppointment = true;
        //            }
        //            else
        //            {
        //                _IsTemplateAppointment = false;
        //            }
        //        }
        //        else
        //        {
        //            _IsTemplateAppointment = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (ogloSettings != null) { ogloSettings.Dispose(); }
        //        value = null;
        //    }
        //}

        // commented previous code
        // Created new function on 11 oct 2011 for case no 12523
        private void SetRegisterAppointmentSettings()
        {

            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            bool value = false;
            try
            {
                value = ogloSettings.GetSettingUserSpecific("RegisterTemplateAppointmentOnly", _UserID, _clinicID);
                if (value == true)
                {
                    _IsTemplateAppointment = true;
                }
                else
                {
                    _IsTemplateAppointment = false;
                }

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

        private void cmbApp_AppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultPASetting(Convert.ToInt64(txtApp_Patient.Tag));
        }

      



        //public bool IsNotesPresent(Int64 MasterApptID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sql = "";
        //    try
        //    {
        //        oDB.Connect(false);

        //        _sql = "select sNotes from as_Appointment_dtl where nMSTAppointmentID = " + MasterApptID + " ";
        //        object ResultCount = oDB.ExecuteScalar_Query(_sql);

        //        if (ResultCount != null && Convert.ToString(ResultCount) != "")
        //        {
        //            return true;

        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if(oDB != null)
        //        {
        //            oDB.Disconnect ();
        //            oDB.Dispose ();
        //        }
        //    }

        //    return false;
        //}

     }

    public partial class SetAppointmentParameter : IDisposable
    {
        public Int64 MasterAppointmentID = 0;
        public Int64 AppointmentID = 0;
        public Int64 LineNumber = 0;
        public AppointmentScheduleFlag AppointmentFlag = AppointmentScheduleFlag.None;
        public Int64 AppointmentTypeID = 0;
        public string AppointmentTypeCode = "";
        public string AppointmentTypeDesc = "";
        public Int64 ProviderID = 0;
        public string ProviderName = "";
        public ArrayList ProblemTypes = new ArrayList();
        public ArrayList Resources = new ArrayList();
        public Int64 PatientID = 0;
        public bool AddTrue_ModifyFalse_Flag = true;
        public gloAppointmentScheduling.SingleRecurrence ModifyAppointmentMethod = SingleRecurrence.Single;
        public gloAppointmentScheduling.SingleRecurrence ModifyMasterAppointmentMethod = SingleRecurrence.Single;
        public bool ModifySingleAppointmentFromReccurence = false;
        public string Location = "";
        public string LocationIDs = "";
        public string Department = "";
        public DateTime StartDate = DateTime.Now;
        public DateTime StartTime = DateTime.Now;
        public decimal Duration = 15;
        public Int64 ClinicID = 0;
        public bool LoadParameters = false;
        public Int64 TemplateAllocationMasterID = 0;
        public Int64 TemplateAllocationID = 0;
        public Int64 Usedstatus = 0;
        public bool ShowTemplateAppointment_Flag = false;
        public bool AllowRecurrenceInBlocked = false;
        public long OldMasterID = 0;
        public string DatesWithCommaSeperator = string.Empty;

        #region "Constructors and Destructors"
        public SetAppointmentParameter()
        {
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   
                }
            }
            disposed = true;
        }

        ~SetAppointmentParameter()
        {
            Dispose(false);
        }

        #endregion
    }
}
