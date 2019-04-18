using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Janus.Windows.Schedule;
using gloAuditTrail;
using gloSettings;
using gloSecurity;
using gloPMGeneral.gloPriorAuthorization;
using gloPMGeneral;
using gloSSRSApplication; 
 
namespace gloAppointmentScheduling
{
    public partial class frmViewAppointment : gloAUSLibrary.MasterForm 
    {
        #region " Variable Declarations "
        
        private static frmViewAppointment _frm = null;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        private string _MessageBoxCaption = string.Empty;
        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");
        private bool _IsProvidersLoading = false;
        private Int64 _PatientD = 0;
        private Boolean _isConflict = false;
        private string _PatientName = "";
        private bool _isMessageDisplayed = false;
        private bool _RegAppUsingTemplateOnly = false;

        //removed unused variable '_GenerateHL7Message' by Abhijeet on 20110920
        // Added variable by Abhijeet on 20110607 for savinf registry setting value for HL7 Outbound generation
        //private Boolean _GenerateHL7Message = false;

        //Used For Context Menu Only Don't use at other places, it may be inconsistent 
        Janus.Windows.Schedule.ScheduleAppointment _LastSelectedAppointment = null;

        gloPatient.PatientListControl oPatientListControl=null;
        private bool _ShowTemplateAppointment = false;
        //Int64 _CurrentTemplateID = 0;
        //Int64 _TemplateLineNo = 0;
        ToolTip oToolTip = new ToolTip();
        ToolTip oToolTip1 = new ToolTip();
        Point _JanusPastePoint;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public delegate void CalendarViewClosed(object sender, EventArgs e);
        public event CalendarViewClosed CalendarView_Closed;


        //Added By Pramod Nair For UserRights 20090720
        private Int64 _UserID = 0;
        private string _UserName = "";
        gloUserRights.ClsgloUserRights oClsgloUserRights = null;
        private Boolean isTemplate = false;


        public string gstrSQLServerName = "";
        public string gstrDatabaseName = "";
        public bool gblnSQLAuthentication = false;
        public string gstrSQLUser = "";
        public string gstrSQLPassword = "";
        public string gstrCaption = "";
        #endregion

        #region " Enumeration Declarations "

        public enum CutCopyPaste
        {
            Cut = 1, Copy = 2, Paste = 3
        }

        #endregion

        #region "Constructor"
        public frmViewAppointment()
        {
            InitializeComponent();

            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

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
            //

            //Get User Name
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

            #region "PatientId"
            if (appSettings["PatientID"] != null)
            {
                if (appSettings["PatientID"] != "")
                { _PatientD = Convert.ToInt64(appSettings["PatientID"]); }
                else
                { _PatientD = 1; }
            }
            else
            { _PatientD = 1; }
            #endregion
            //


            #region "GenerateHL7Message"
            // below code commented by Abhijeet on 20110920 and call gloHL7 class function to read registry
           
            //if (appSettings["GenerateHL7Message"] != null)
            //{
            //    if (appSettings["GenerateHL7Message"] != "")
            //    {
            //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
            //        {
            //            _GenerateHL7Message = true;
            //        }
            //    }
            //}
            //End of changes to above code commented by Abhijeet on 20110920 
            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            gloHL7.GetgloEMRSettings(_databaseconnectionstring);
            #endregion
        }


        private frmViewAppointment(string DatabaseConnectionString, Int64 ClinicID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _ClinicID = ClinicID;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                { _ClinicID = 0; }

            }
            else
            { _ClinicID = 0; }
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

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

            //Get User Name
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
            #region "PatientId"

            if (appSettings["PatientID"] != null)
            {
                if (appSettings["PatientID"] != "")
                { _PatientD = Convert.ToInt64(appSettings["PatientID"]); }
                else
                { _PatientD = 1; }
            }
            else
            { _PatientD = 1; }
            #endregion

            #region "GenerateHL7Message"
            // below code commented by Abhijeet on 20110920 and call gloHL7 class function to read registry

            //if (appSettings["GenerateHL7Message"] != null)
            //{
            //    if (appSettings["GenerateHL7Message"] != "")
            //    {
            //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
            //        {
            //            _GenerateHL7Message = true;
            //        }
            //    }
            //}                       
            //End of changes to above code commented by Abhijeet on 20110920 
            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            gloHL7.GetgloEMRSettings(_databaseconnectionstring);
            #endregion 
        }

        public static frmViewAppointment GetInstance()
        {

            if (_frm != null)
            {
                return _frm;
            }
            else
            {
                _frm = new frmViewAppointment();
                return _frm;
            }
        }

        public static frmViewAppointment GetInstance(string DatabaseConnectionString, Int64 ClinicID)
        {
            if (_frm != null)
            {
                _frm._databaseconnectionstring = DatabaseConnectionString;
                _frm._ClinicID = ClinicID;
                return _frm;
            }
            else
            {
                _frm = new frmViewAppointment(DatabaseConnectionString, ClinicID);
                return _frm;
            }


        }

        bool blnDisposed;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (blnDisposed == false)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                    base.Dispose(disposing);  
                    
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (oClsgloUserRights != null)
                        {
                            oClsgloUserRights.Dispose();
                            oClsgloUserRights = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        System.Windows.Forms.ContextMenuStrip[] cntmenuControls = { cmnu_AppointmentEdit, cmnu_AppointmentNew };
                        System.Windows.Forms.Control[] cntControl = { cmnu_AppointmentEdit, cmnu_AppointmentNew };

                        if (cntmenuControls != null)
                        {
                            if (cntmenuControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmenuControls);
                            }
                        }
                        if (cntControl != null)
                        {
                            if (cntControl.Length > 0)
                            {
                                gloGlobal.cEventHelper.DisposeAllControls(ref cntControl);
                            }
                        }
                        // if (cmnu_AppointmentEdit != null)
                        //{
                        //    gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_AppointmentEdit);
                        //    if (cmnu_AppointmentEdit.Items != null)
                        //    {
                        //        cmnu_AppointmentEdit.Items.Clear();

                        //    }
                        //    cmnu_AppointmentEdit.Dispose();
                        //    cmnu_AppointmentEdit = null;
                        //}
                    }
                    catch
                    {
                    } 
                   
                   
                
                    if (oToolTip != null)
                    {
                        oToolTip.Dispose();
                        oToolTip = null;
                    }
                    if (oToolTip1 != null)
                    {
                        oToolTip1.Dispose();
                        oToolTip1 = null;
                    }
                    try
                    {
                        if (printAppointment != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(printAppointment);
                            printAppointment.Dispose();
                            printAppointment = null;
                        }
                    }
                    catch
                    {
                    }
                }
              
            }
            _frm = null;
            blnDisposed = true;
        }

        private void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #endregion

        #region " Form Events "

        private void frmViewAppointment_Load(object sender, EventArgs e)
        {

            if (Screen.PrimaryScreen.Bounds.Height < 900)
            {
                pnlLeft.AutoScroll = true;
            }
            else
            {
                pnlLeft.AutoScroll = false;
            }

            juc_Appointment.DayColumns = GetNumberofColumninDayView();

            // Get Work week from DataBase
            GetWorkWeekForJanus();
           
            btnLeft.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btnLeft.BackgroundImageLayout = ImageLayout.Center;

            btn_Right.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_Right.ImageAlign = ContentAlignment.MiddleCenter;


            lblCopayAlert.Text = "";
            lblPrePayAlert.Text = "";

            GetClinicTiming();
            GetTemplateSettings();
           
            //GetRegisterAppointmentSettings();

            GetRegisterAppointmentSettingsUserSpecific();



            GetZoomTimeSettings();

            FillResources();
            FillProviders();
            FillLocations();
            FillLegend();
            FillMarkAsStatus();

            pnlOther2.Visible = false;
            splOther2.Visible = false;

            FillAppointments(_ShowTemplateAppointment);
            FillFolowupMenu(tls_btnFollowUp);
            AssignUserRights();
        }

        public void GetWorkWeekForJanus()
        {
         
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            try
            {
                int workWeek = 0;  
                ogloSettings.GetSetting("Week Days", 0, _ClinicID, out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                 {
                     string[] WeekDays = Convert.ToString(value).Trim().Split(',');
                     for (int j = 0; j <= (WeekDays.Length - 1); j++)
                     {
                         if (WeekDays[j].Trim() == "0")
                         {
                             workWeek = workWeek + 1;
                         }
                         if (WeekDays[j].Trim() == "1")
                         {
                             workWeek = workWeek + 2;
                         }
                         if (WeekDays[j].Trim() == "2")
                         {
                             workWeek = workWeek + 4;
                         }
                         if (WeekDays[j].Trim() == "3")
                         {
                             workWeek = workWeek + 8;
                         }
                         if (WeekDays[j].Trim() == "4")
                         {
                             workWeek = workWeek + 16;
                         }
                         if (WeekDays[j].Trim() == "5")
                         {
                             workWeek = workWeek + 32;
                         }
                         if (WeekDays[j].Trim() == "6")
                         {
                             workWeek = workWeek + 64;
                         }
                        
                     }

                      if (workWeek>0)
                      {
                         juc_Appointment.WorkWeek = (ScheduleDayOfWeek)(workWeek); 
                         // Janus.Windows.Schedule.ScheduleDayOfWeek.Monday|Janus.Windows.Schedule.ScheduleDayOfWeek.Tuesday;
                      }                      

                }
                value = null;               

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

        private void frmViewAppointment_Resize(object sender, EventArgs e)
        {
        }

        #endregion

        #region " Tool Strip Item Click Event "

        private void tsb_Appointment_Click(object sender, EventArgs e)
        {
            
            
            //New Appointment
            if (_RegAppUsingTemplateOnly == true)
            {
                MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //code added for lock chart
            gloSecurity.gloSecurity oSecurity = null;
            frmSetupAppointment oSetupAppointment = null;
            gloAppointment ogloAppointment = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            DataTable dt = null;
            try
            {
                oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
                if (oSecurity.isPatientLock(_PatientD, true))
                {

                    return;
                }


                /////end of code
                oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                Int64 _OwnerID = 0;
                Int64 _OwnerTypeID = 0;
                string _OwnerName = "";

                bool isBlocked = false;
                

                ogloAppointment = new gloAppointment(_databaseconnectionstring);

                if (juc_Appointment.CurrentOwner != null)
                {
                    _OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1));
                    _OwnerTypeID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 3));
                    _OwnerName = juc_Appointment.CurrentOwner.Text.ToString();
                }

                #region "Set Appointment Parameters"
                oSetupAppointment.SetAppointmentParameters.MasterAppointmentID = 0;
                oSetupAppointment.SetAppointmentParameters.AppointmentID = 0;
                oSetupAppointment.SetAppointmentParameters.ClinicID = _ClinicID;
                oSetupAppointment.SetAppointmentParameters.ProviderID = _OwnerID;
                oSetupAppointment.SetAppointmentParameters.ProviderName = _OwnerName;
                oSetupAppointment.SetAppointmentParameters.AddTrue_ModifyFalse_Flag = true; // Add - true, Modify - false
                oSetupAppointment.SetAppointmentParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                oSetupAppointment.SetAppointmentParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                oSetupAppointment.SetAppointmentParameters.ModifySingleAppointmentFromReccurence = false;
                oSetupAppointment.SetAppointmentParameters.StartDate = juc_Appointment.GetDateTimeAt(_JanusPastePoint);// .GetDateAt();
                oSetupAppointment.SetAppointmentParameters.StartTime = Convert.ToDateTime(juc_Appointment.SelectedDays.Start);//Convert.ToDateTime( juc_Appointment.GetDateTimeAt().TimeOfDay.ToString());//Convert.ToDateTime(string.Format(juc_Appointment.GetDateTimeAt().ToShortTimeString(),"hh:mm:tt"));//Convert.ToDateTime(string.Format(juc_Appointment.GetDateAt().ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(juc_Appointment.GetTimeAt().ToString(), "hh:mm tt"));
                oSetupAppointment.SetAppointmentParameters.Duration = Convert.ToDecimal((juc_Appointment.SelectedDays.End - juc_Appointment.SelectedDays.Start).TotalMinutes);
                oSetupAppointment.SetAppointmentParameters.LoadParameters = true;
                oSetupAppointment.SetAppointmentParameters.PatientID = _PatientD;
                oSetupAppointment.SetAppointmentParameters.ShowTemplateAppointment_Flag = _ShowTemplateAppointment;

                string LocationName = "";
                bool SelectedLocationCount = false;

                foreach (TreeNode n in trvLocations.Nodes)
                    if (n.Checked == true)
                    {
                        SelectedLocationCount = true;
                        if (LocationName == "")
                        { LocationName = n.Text.ToString(); }
                        else
                        { LocationName = LocationName + "~" + n.Text.ToString(); }
                    }

                if (SelectedLocationCount)
                {
                    oSetupAppointment.SetAppointmentParameters.Location = GetSelectedLocation(LocationName);
                }

                foreach (TreeNode n in trvLocations.Nodes)
                    if (n.Checked == true)
                    {
                        string str = n.Tag.ToString();
                        String[] _arrSpliter;
                        _arrSpliter = str.Split('~');
                        if (oSetupAppointment.SetAppointmentParameters.LocationIDs == "")
                        {
                            oSetupAppointment.SetAppointmentParameters.LocationIDs = _arrSpliter[0];
                        }
                        else
                        {
                            oSetupAppointment.SetAppointmentParameters.LocationIDs = oSetupAppointment.SetAppointmentParameters.LocationIDs + "~" + _arrSpliter[0];
                        }

                    }

                #endregion
                foreach (TreeNode n in trvResources.Nodes)
                    if (n.Checked == true)
                    {
                        string str = n.Tag.ToString();
                        String[] _arrSpliter;
                        _arrSpliter = str.Split('~');
                        oSetupAppointment.SetAppointmentParameters.Resources.Add(_arrSpliter[0]);
                    }

                try
                {
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                }
                catch
                {
                }
                oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                oClsgloUserRights.CheckForUserRights(_UserName);

                if (_OwnerID > 0)
                {
                    isBlocked = ogloAppointment.BlockedSlots(_OwnerID, juc_Appointment.SelectedDays.Start, juc_Appointment.SelectedDays.End, oSetupAppointment.SetAppointmentParameters.StartDate, LocationName);
                }

                if (isBlocked == true)
                {
                    isBlocked = false;
                    dt = ogloAppointment.ResourseName(_OwnerID, juc_Appointment.SelectedDays.Start, juc_Appointment.SelectedDays.End, oSetupAppointment.SetAppointmentParameters.StartDate, LocationName);
                    if (dt.Rows.Count >= 1 && dt != null)
                    {
                        if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                        {
                            MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + juc_Appointment.SelectedDays.Start.ToShortTimeString() + " - " + juc_Appointment.SelectedDays.End.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            { return; }
                        }
                    }
                }

                oSetupAppointment.ShowDialog(this);

                FillAppointments(_ShowTemplateAppointment);

                ViewAppointmentDetails();
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

              
            }
            catch
            {
            }
            finally
            {
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
                if (oSetupAppointment != null)
                {
                    oSetupAppointment.Dispose();
                    oSetupAppointment = null;
                }
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
            }
        }

        private string GetSelectedLocation (string LocationName)
        {
             string _DefaultLocationName = "";
             bool _blnDefaultLocation = false;

             System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
             if (appSettings["DefaultLocation"] != null)
             {
                 if (Convert.ToString(appSettings["DefaultLocation"]).Trim() != "")
                 {
                     _DefaultLocationName = Convert.ToString(appSettings["DefaultLocation"]).Trim();
                 }
             }

            String[] _arrLocationName;
            _arrLocationName = LocationName.Split('~');

            if (_arrLocationName.Length == 1)
            {
                return LocationName;
            }
            else
            {
                for (int i = 0; i < _arrLocationName.Length; i++ )
                {
                    if (_arrLocationName[i] == _DefaultLocationName)
                    {
                        _blnDefaultLocation = true;
                        break;
                    }
                }

                if (_blnDefaultLocation)
                { return _DefaultLocationName;}
                else
                {return null;}
            }
        }

        private void tsb_ModifyAppointment_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloSecurity.gloSecurity oSecurity = null;
            gloAppointment ogloAppointment = null;
            DataTable dtPatient = null;
            try
            {
                bool allowRecurence = false;
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

                if (juc_Appointment.CurrentAppointment == null)
                {
                    juc_Appointment.CurrentAppointment = _LastSelectedAppointment;
                }

                if (juc_Appointment.CurrentAppointment != null)
                {

                    
                    Int64 _resourceID = 0;
                    DateTime Starttime;
                    DateTime Endtime;
                    DateTime StartDate;
                    string Location = "";
                    //  juc_Appointment.Location = _JanusPastePoint;


                    //_resourceID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1).ToString());

                    _resourceID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 1));


                    Starttime = juc_Appointment.CurrentAppointment.StartTime;
                    Endtime = juc_Appointment.CurrentAppointment.EndTime;
                    StartDate = Convert.ToDateTime(juc_Appointment.CurrentAppointment.StartTime.ToShortDateString());


                    AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                    _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));

                    if (_AppORTemp == AppointmentScheduleFlag.Appointment || _AppORTemp == AppointmentScheduleFlag.TemplateAppointment)
                    {
                         ogloAppointment = new gloAppointment(_databaseconnectionstring);
                        Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                        Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());
                        Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6).ToString();


                        //code added for lock chart
                       
                        dtPatient = ogloAppointment.GetPatient(MasterAppointmentID, DetailAppointmentID);
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            Int64 nPatient = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                            oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
                            if (oSecurity.isPatientLock(nPatient, true))
                            {
                                return;
                            }
                            ///end of code
                        }
                        dt = ogloAppointment.ResourseName(_resourceID, Starttime, Endtime, StartDate, Location);


                        if (dt.Rows.Count >= 1 && _isMessageDisplayed == false && dt != null)
                        {
                            if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Modify this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                allowRecurence = false;
                                _isMessageDisplayed = false;
                                return;
                            }
                            else
                            {
                                allowRecurence = true;
                                _isMessageDisplayed = false;
                            }
                        }
                        _PatientName = Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 1)); //ogloPatient.GetPatientName(_PatientD);
                        if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true)
                        {
                            if (MessageBox.Show("Patient '" + _PatientName + "' is already checked out. Are you sure you wish to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                            }
                            else
                            {
                                return;
                            }
                        }

                        SetAppointmentParameter oAppParameters = new SetAppointmentParameter();
                        oAppParameters.AllowRecurrenceInBlocked = allowRecurence;


                        frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                        oSetupAppointment.MasterAppointmentId = MasterAppointmentID;
                        oSetupAppointment.DetailAppointmentId = DetailAppointmentID;


                        bool _ModifySelectedAppointment = false;

                        #region "Ask for Single/Recurrence Option"
                        SingleRecurrence _SinRecCriteria = SingleRecurrence.Single;
                        _SinRecCriteria = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3));

                        frmSearchModDelCriteria oCriteria = new frmSearchModDelCriteria();
                        if (_SinRecCriteria == SingleRecurrence.Single)
                        {
                            _ModifySelectedAppointment = true;
                            oAppParameters.AddTrue_ModifyFalse_Flag = false;
                            oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifySingleAppointmentFromReccurence = false;
                        }
                        else
                        {
                            oCriteria.SelectedForModDel = "Modify";
                            oCriteria.SelectedCriteria = "Recurrence";
                            oCriteria.ShowDialog(this);
                            if (oCriteria.SelectedResult == true)
                            {
                                _ModifySelectedAppointment = true;
                                oAppParameters.AddTrue_ModifyFalse_Flag = false;

                                if (oCriteria.SelectedCriteria == "Simple") // "Recurrence";
                                {
                                    oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                                    oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Recurrence;
                                    oAppParameters.ModifySingleAppointmentFromReccurence = true;
                                }
                                else
                                {
                                    oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Recurrence;
                                    oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Recurrence;
                                    oAppParameters.ModifySingleAppointmentFromReccurence = false;
                                }
                            }
                        }
                        oCriteria.Dispose();
                        #endregion

                        try
                        {
                            if (_ModifySelectedAppointment == true)
                            {
                                #region "Set Appointment Parameter"
                                oAppParameters.MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                                oAppParameters.AppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;

                                oDB.Connect(false);
                                string strDate = string.Empty;

                                // Commented below query and written new by pranit on 5 March 2012 as only 2000 characters get selected from query so STUFF is used 
                                //strDate = (string)(oDB.ExecuteScalar_Query("SELECT CONVERT(varchar,dtStartDate)+'_'+CONVERT(varchar,nUsedStatus)+',' FROM AS_Appointment_DTL WHERE nMSTAppointmentID=" + oAppParameters.MasterAppointmentID + " and nASBaseID > 0 and nUsedStatus in (1,2,3,4) FOR XML PATH('')"));
                                strDate = Convert.ToString((oDB.ExecuteScalar_Query("SELECT STUFF((select ',' + replace(isnull(CONVERT(varchar,dtStartDate)+'_'+CONVERT(varchar,nUsedStatus),''),SPACE(2),SPACE(1)) from AS_Appointment_DTL ApptDTL  WITH(NOLOCK) WHERE nMSTAppointmentID=" + oAppParameters.MasterAppointmentID + " and nASBaseID > 0 and nUsedStatus in (0,1,2,3,4) FOR XML PATH('')),1,1,'') ")));
                                //0 added in above query to handle null condition which was giving error 

                                if (strDate != null)
                                {
                                    if (strDate != "")
                                    {
                                        string lastChar = strDate.Trim().Substring(strDate.Length - 1, 1);
                                        if (lastChar == ",")
                                        {
                                            strDate = strDate.Trim().Substring(0, strDate.Trim().Length - 1);
                                        }
                                        oAppParameters.DatesWithCommaSeperator = strDate;
                                    }
                                }


                                oAppParameters.AppointmentFlag = _AppORTemp;
                                oAppParameters.AppointmentTypeID = 0;
                                oAppParameters.AppointmentTypeCode = "";
                                oAppParameters.AppointmentTypeDesc = "";
                                oAppParameters.ProviderID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1).ToString());
                                oAppParameters.ProviderName = "";
                                oAppParameters.ProblemTypes = null;
                                oAppParameters.Resources = null;
                                oAppParameters.PatientID = 0;
                                #region "Setup as per selection criteria in above method"
                                #endregion
                                oAppParameters.Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                                oAppParameters.Department = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 8).ToString();
                                oAppParameters.StartDate = juc_Appointment.CurrentAppointment.StartTime;
                                oAppParameters.StartTime = juc_Appointment.CurrentAppointment.StartTime;
                                oAppParameters.Duration = Convert.ToDecimal(juc_Appointment.CurrentAppointment.Duration.TotalMinutes);
                                oAppParameters.ClinicID = _ClinicID;
                                oAppParameters.LineNumber = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                                oAppParameters.LoadParameters = false;
                                oAppParameters.ShowTemplateAppointment_Flag = _ShowTemplateAppointment;

                                foreach (TreeNode n in trvLocations.Nodes)
                                    if (n.Checked == true)
                                    {
                                        string str = n.Tag.ToString();
                                        String[] _arrSpliter;
                                        _arrSpliter = str.Split('~');
                                        if (oAppParameters.LocationIDs == "")
                                        {
                                            oAppParameters.LocationIDs = _arrSpliter[0];
                                        }
                                        else
                                        {
                                            oAppParameters.LocationIDs = oAppParameters.LocationIDs + "~" + _arrSpliter[0];
                                        }
                                    }

                                #endregion

                                oSetupAppointment.SetAppointmentParameters = oAppParameters;
                                oSetupAppointment.ShowDialog(this);

                                FillAppointments(_ShowTemplateAppointment);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                        }
                        finally
                        {
                            oSetupAppointment.Dispose();
                            oAppParameters.Dispose();
                        }

                        ViewAppointmentDetails();
                    }
                    else
                    {
                        if (_AppORTemp == AppointmentScheduleFlag.BlockedSchedule)
                        {

                            //By Pranit on 20111206 to check admin setting (new schedule)
                            Boolean isResult = checkSchedule();
                            if (isResult == false)
                            {
                                MessageBox.Show("You don't have rights to access schedule. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }




                            Int64 ScheduleMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                            Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                            AppointmentScheduleFlag _SchORApp = AppointmentScheduleFlag.None;
                            _SchORApp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));
                            if (_SchORApp == AppointmentScheduleFlag.BlockedSchedule || _SchORApp == AppointmentScheduleFlag.ProviderSchedule || _SchORApp == AppointmentScheduleFlag.ResourceSchedule)
                            {
                                SetScheduleParameter oScheduleParameters = new SetScheduleParameter();
                                frmSetupSchedule oSetupSchedule = new frmSetupSchedule(ScheduleMasterID, _databaseconnectionstring);
                                bool _ModifySelectedSchedule = false;

                                SingleRecurrence IsReccuring = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3));


                                if (IsReccuring == SingleRecurrence.Single)
                                {

                                    _ModifySelectedSchedule = true;
                                    oScheduleParameters.AddTrue_ModifyFalse_Flag = false;
                                    oScheduleParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                                    oScheduleParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                                    oScheduleParameters.ModifySingleAppointmentFromReccurence = false;
                                }
                                else if (IsReccuring == SingleRecurrence.Recurrence || IsReccuring == SingleRecurrence.SingleInRecurrence)
                                {
                                    #region "Ask for Single/Recurrence Option"
                                    frmSearchModDelCriteria oCriteria = new frmSearchModDelCriteria();
                                    oCriteria.SelectedForModDel = "Modify";
                                    oCriteria.SelectedCriteria = "Recurrence";
                                    oCriteria.ShowDialog(this);
                                    if (oCriteria.SelectedResult == true)
                                    {
                                        _ModifySelectedSchedule = true;
                                        oScheduleParameters.AddTrue_ModifyFalse_Flag = false;

                                        if (oCriteria.SelectedCriteria == "Simple")
                                        {
                                            oScheduleParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                                            oScheduleParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Recurrence;
                                            oScheduleParameters.ModifySingleAppointmentFromReccurence = true;
                                        }
                                        else
                                        {
                                            oScheduleParameters.ModifyAppointmentMethod = SingleRecurrence.Recurrence;
                                            oScheduleParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Recurrence;
                                            oScheduleParameters.ModifySingleAppointmentFromReccurence = false;
                                        }
                                    }
                                    oCriteria.Dispose();
                                    #endregion
                                }


                                if (_ModifySelectedSchedule == true)
                                {
                                    #region "Set Schedule Parameter"
                                    oScheduleParameters.MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                                    oScheduleParameters.AppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                                    oScheduleParameters.AppointmentFlag = AppointmentScheduleFlag.None;
                                    oScheduleParameters.AppointmentTypeID = 0;
                                    oScheduleParameters.AppointmentTypeCode = "";
                                    oScheduleParameters.AppointmentTypeDesc = "";
                                    oScheduleParameters.ProviderID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1).ToString());
                                    oScheduleParameters.ProviderName = "";
                                    oScheduleParameters.ProblemTypes = null;
                                    oScheduleParameters.Resources = null;
                                    oScheduleParameters.PatientID = 0;
                                    #region "Setup as per selection criteria in above method"
                                    //oAppParameters.AddTrue_ModifyFalse_Flag = true;
                                    //oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                                    //oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                                    //oAppParameters.ModifySingleAppointmentFromReccurence = false;
                                    #endregion
                                    oScheduleParameters.Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                                    oScheduleParameters.Department = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 8).ToString();
                                    oScheduleParameters.StartDate = juc_Appointment.CurrentAppointment.StartTime;
                                    oScheduleParameters.StartTime = juc_Appointment.CurrentAppointment.StartTime;
                                    oScheduleParameters.Duration = Convert.ToDecimal(juc_Appointment.CurrentAppointment.Duration.TotalMinutes);
                                    oScheduleParameters.ClinicID = _ClinicID;
                                    //oScheduleParameters.LineNumber = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                                    oScheduleParameters.LoadParameters = false;

                                    #endregion

                                    oSetupSchedule.SetScheduleParameters = oScheduleParameters;
                                    oSetupSchedule.ShowDialog(this);
                                    if (_ModifySelectedSchedule == true) { FillAppointments(_ShowTemplateAppointment); }
                                    //FillScheduleOnCalendar();
                                    //juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
                                }
                                if (oSetupSchedule != null)
                                {
                                    oSetupSchedule.Dispose();
                                    oSetupSchedule = null;
                                }
                                if (oScheduleParameters != null)
                                {
                                    oScheduleParameters.Dispose();
                                    oScheduleParameters = null;
                                }
                                                          

                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select appointment.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                _LastSelectedAppointment = null;
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (dtPatient != null)
                {
                   dtPatient.Dispose();
                   dtPatient = null;
                }
            }
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            bool _isDelete = false;
            gloAppointment ogloAppointment = null;
            gloPatient.gloPatient ogloPatient = null;
            try
            {
                #region "Change Appointment Status to delete"

                if (juc_Appointment.CurrentAppointment != null)
                {
                    AppointmentScheduleFlag IsTemplate = AppointmentScheduleFlag.None;
                    IsTemplate = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));
                    SingleRecurrence IsReccuring = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3));

                    if (IsTemplate == AppointmentScheduleFlag.TemplateBlock)
                    {

                        MessageBox.Show("Cannot delete template block.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //If schedule is blocked then user can't delete that schedule
                    if (IsTemplate == AppointmentScheduleFlag.BlockedSchedule)
                    {
                       // SingleRecurrence IsReccuring = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3));

                        if (IsReccuring == SingleRecurrence.Recurrence || IsReccuring == SingleRecurrence.SingleInRecurrence)
                        {

                            #region "Ask for Single/Recurrence Option"

                            frmSearchModDelCriteria oCriteria = new frmSearchModDelCriteria();
                            oCriteria.SelectedForModDel = "Delete";
                            oCriteria.SelectedCriteria = "Recurrence";
                            oCriteria.ShowDialog(this);
                            if (oCriteria.SelectedResult == true)
                            {
                                Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                                Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                                if (oCriteria.SelectedCriteria == "Simple")
                                {
                                    if (_TempMasterID > 0 && _TempDetailID > 0)
                                    {
                                        DeleteSchedule(_TempMasterID, _TempDetailID, false);
                                       // Refresh(sender);
                                    }
                                }
                                else // "Recurrence";
                                {
                                    if (_TempMasterID > 0 && _TempDetailID > 0)
                                    {
                                        DeleteSchedule(_TempMasterID, _TempDetailID, true);
                                        //Refresh(sender);
                                    }
                                }
                            }
                            oCriteria.Dispose();

                            #endregion

                        }
                        else if (IsReccuring == SingleRecurrence.Single)
                        {
                            if (MessageBox.Show("Are you sure to delete this schedule?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //delete single appointment 
                                Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                                Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    DeleteSchedule(_TempMasterID, _TempDetailID, true);
                                   // Refresh(sender);
                                }
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Delete, "Schedule deleted", 0, 0, 0, ActivityOutCome.Success);
                            }
                        }
                        FillAppointments(_ShowTemplateAppointment);
                        ViewAppointmentDetails();
                        return;
                    }

                     ogloAppointment = new gloAppointment(_databaseconnectionstring);
                    Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                    Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;

                    //Code Start- added by kanchan on 20091230 for Appointment outbound of HL7
                    // Extract PatientID for insert it into HL7 Message queue
                    long _nPatientID = 0;
                    Object result = null;
                    //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                    result = ogloAppointment.GetPatientId(MasterAppointmentID);
                    if (result != null)
                    {
                        _nPatientID = Convert.ToInt64(result);
                    }
                    //Code End- added by kanchan on 20091230 for Appointment outbound of HL7
                    long _nProviderID = 0;
                    using (DataTable dtPatient = ogloAppointment.GetAppointmentProviderID(MasterAppointmentID))
                    {
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            _nProviderID = Convert.ToInt64(dtPatient.Rows[0]["nASBaseID"]);
                        }
                    }
                     ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                    _PatientName = ogloAppointment.GetAppointmentPatientName(MasterAppointmentID);
                    //
                    if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true)
                    {
                        if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true)
                        {
                            if (MessageBox.Show("Patient '" + _PatientName + "' is already checked out.  Are you sure you wish to delete this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                _isDelete = true;

                                Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                                Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    ogloAppointment.UpdateAppointmentUsedStatus(_TempMasterID, _TempDetailID, ASUsedStatus.Delete.GetHashCode());

                                    //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Delete, "Appointment Deleted", _nPatientID, _TempMasterID, _nProviderID, ActivityOutCome.Success);

                                    #region "Generate HL7 Message Queue for New Appointment"
                                    // Code Start - Added by kanchan on 20091230 for HL7 appointment outbound
                                    if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                                    {
                                        if (_TempMasterID > 0)
                                        {
                                            gloHL7.InsertInMessageQueue("S15", _nPatientID, _TempMasterID, _TempDetailID.ToString(), _databaseconnectionstring);
                                        }
                                    }
                                    // Code End - Added by kanchan on 20091230 for HL7 appointment outbound
                                    #endregion
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    if (IsReccuring == SingleRecurrence.Recurrence || IsReccuring == SingleRecurrence.SingleInRecurrence)
                    {
                        #region "Ask for Single/Recurrence Option"

                        frmSearchModDelCriteria oCriteria = new frmSearchModDelCriteria();
                        oCriteria.SelectedForModDel = "Delete";
                        oCriteria.SelectedCriteria = "Recurrence";
                        oCriteria.ShowDialog(this);
                        if (oCriteria.SelectedResult == true)
                        {
                            Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                            Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                            if (oCriteria.SelectedCriteria == "Simple")
                            {
                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    ogloAppointment.UpdateAppointmentUsedStatus(_TempMasterID, _TempDetailID, ASUsedStatus.Delete.GetHashCode());
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Delete, "Appointment Occurrence Deleted", _nPatientID, _TempMasterID, _nProviderID, ActivityOutCome.Success);

                                    #region "Generate HL7 Message Queue for New Appointment"
                                    // Code Start - Added by kanchan on 20091230 for HL7 appointment outbound
                                    if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                                    {
                                        if (_TempMasterID > 0)
                                        {
                                            gloHL7.InsertInMessageQueue("S15", _nPatientID, _TempMasterID, _TempDetailID.ToString(), _databaseconnectionstring);
                                        }
                                    }
                                    // Code End - Added by kanchan on 20091230 for HL7 appointment outbound
                                    #endregion
                                }
                            }
                            else // "Recurrence";
                            {
                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    ogloAppointment.UpdateRecurrenceAppointmentUsedStatus(_TempMasterID, ASUsedStatus.Delete.GetHashCode());
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Delete, "Appointment Series Deleted", _nPatientID , _TempMasterID, _nProviderID, ActivityOutCome.Success);

                                    #region "Generate HL7 Message Queue for New Appointment"
                                    // Code Start - Added by kanchan on 20091230 for HL7 appointment outbound
                                    if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                                    {
                                        if (_TempMasterID > 0)
                                        {
                                            gloHL7.InsertInMessageQueue("S15", _nPatientID, _TempMasterID, "", _databaseconnectionstring);
                                        }
                                    }
                                    // Code End - Added by kanchan on 20091230 for HL7 appointment outbound
                                    #endregion
                                }
                            }
                            //Commented for AuditTrail as two entries were seen in for Delete in Audit Log for Recurrence
                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Delete, "Appointment Deleted", _nPatientID , _TempMasterID, _nProviderID , ActivityOutCome.Success);
                        }
                        oCriteria.Dispose();

                        #endregion
                    }
                    else if (IsReccuring == SingleRecurrence.Single)
                    {
                        //if else Statement added by dipak to fix mantis Id 0000101: Calendar >> Application allow to delete,cancel,No Show previous date appointments
                        if (juc_Appointment.CurrentAppointment.EndTime.Date.CompareTo(System.DateTime.Now.Date) >= 0)
                        {
                            if (_isDelete == false)
                            {
                                if (MessageBox.Show("Are you sure you want to delete this appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //delete single appointment code goes here....;

                                    Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                                    Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                                    if (_TempMasterID > 0 && _TempDetailID > 0)
                                    {
                                        ogloAppointment.UpdateAppointmentUsedStatus(_TempMasterID, _TempDetailID, ASUsedStatus.Delete.GetHashCode());

                                        //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Delete, "Appointment Deleted", _nPatientID, _TempMasterID, _nProviderID, ActivityOutCome.Success);

                                        #region "Generate HL7 Message Queue for New Appointment"
                                        // Code Start - Added by kanchan on 20091230 for HL7 appointment outbound
                                        if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                                        {
                                            if (_TempMasterID > 0)
                                            {
                                                gloHL7.InsertInMessageQueue("S15", _nPatientID, _TempMasterID, _TempDetailID.ToString(), _databaseconnectionstring);
                                            }
                                        }
                                        // Code End - Added by kanchan on 20091230 for HL7 appointment outbound
                                        #endregion
                                    }

                                }
                            }
                        }
                        else //if date is previous then show message
                        {
                            MessageBox.Show("Can not delete past appointment. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //end code added by dipak 20091112
                    }
                    FillAppointments(_ShowTemplateAppointment);
                }
                else
                {
                    MessageBox.Show("Please select appoinment.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                ViewAppointmentDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _isDelete = false;
            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (ogloPatient != null)
                {
                    ogloPatient.Dispose();
                    ogloPatient = null;
                }
            }
        }

        private bool DeleteSchedule(Int64 MasterScheduleId, Int64 ScheduleDetailId, bool DeleteMaster)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            bool _retValue = false;

            try
            {
                oDB.Connect(false);

                //2. If for Cut Paste Delete the Master entry 
                if (DeleteMaster)
                {
                    _sqlQuery = "DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + MasterScheduleId + " AND nClinicID =" + this._ClinicID + " ";
                    int IsDeleted = oDB.Execute_Query(_sqlQuery);

                    if (IsDeleted > 0)
                    {
                        _sqlQuery = "DELETE FROM AS_Schedule_MST WHERE nMSTScheduleID = " + MasterScheduleId + " AND nClinicID =" + this._ClinicID + " ";
                        int IsMasterDeleted = oDB.Execute_Query(_sqlQuery);

                        if (IsMasterDeleted > 0)
                            _retValue = true;
                    }
                }
                else
                {
                    //1.Delete the Details Table entry for the Schedule
                    _sqlQuery = "DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + MasterScheduleId + " AND (nDTLScheduleID = " + ScheduleDetailId + " OR nRefID = " + ScheduleDetailId + ") AND nClinicID =" + this._ClinicID + " ";
                    int IsDeleted = oDB.Execute_Query(_sqlQuery);

                    if (IsDeleted > 0)
                        _retValue = true;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                _retValue = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                _retValue = false;
            }
            finally
            {
                if (oDB.Connect(false)) { oDB.Disconnect(); }

                if (oDB != null) { oDB.Dispose(); }
            }

            return _retValue;
        }


        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Help_Click(object sender, EventArgs e)
        {
        }

        private void tsb_Today_Click(object sender, EventArgs e)
        {
            // 20100108 Mahesh Nawal Set the current date
            juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Single;
            tls_btnTimeNavigation.Visible = true;
            tsb_WeekView.Checked = false;
            tsb_MonthView.Checked = false;
            tsb_DayView.Checked = false;
            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.DayView;
            juc_Appointment.Date = DateTime.Today.Date;
            juc_Calendar.CurrentDate = DateTime.Today.Date;
            juc_Calendar.CurrentDate = DateTime.Now;
            juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Schedule;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            // juc_Calendar.SelectedDates[0] = DateTime.Today.Date;
        }

        private void tsb_DayView_Click(object sender, EventArgs e)
        {
            tls_btnTimeNavigation.Visible = true;
            tsb_Today.Checked = false;
            tsb_WeekView.Checked = false;
            tsb_MonthView.Checked = false;
            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.DayView;
            juc_Appointment.Date = juc_Calendar.CurrentDate;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tsb_WeekView_Click(object sender, EventArgs e)
        {
            tls_btnTimeNavigation.Visible = false;
            tsb_Today.Checked = false;
            tsb_DayView.Checked = false;
            tsb_MonthView.Checked = false;
            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.WeekView;
            juc_Appointment.Date = DateTime.Today.Date;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tsb_MonthView_Click(object sender, EventArgs e)
        {
            tls_btnTimeNavigation.Visible = false;
            tsb_Today.Checked = false;
            tsb_DayView.Checked = false;
            tsb_WeekView.Checked = false;
            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.MonthView;
            juc_Appointment.Date = DateTime.Today.Date;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_SearchAppt_Click(object sender, EventArgs e)
        {
            frmSearchAppointment oSearchApp = new frmSearchAppointment();
            oSearchApp.ShowDialog(this);
            oSearchApp.Dispose();
            oSearchApp = null;
            FillAppointments(_ShowTemplateAppointment);
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_SendToExchange_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        }

        private void tls_btnTemplate_Click(object sender, EventArgs e)
        {
            tls_btnHideTemplate.Visible = true;
            tls_btnTemplate.Visible = false;
            _ShowTemplateAppointment = true;
            FillAppointments(_ShowTemplateAppointment);
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnHideTemplate_Click(object sender, EventArgs e)
        {
            tls_btnHideTemplate.Visible = false;
            tls_btnTemplate.Visible = true;
            _ShowTemplateAppointment = false;
            if (tsb_ModifyAppointment.Enabled == false) { tsb_ModifyAppointment.Enabled = true; }
            if (tsb_Delete.Enabled == false) { tsb_Delete.Enabled = true; }
            oPatientListControl_ItemClosedClick(null, null);
            FillAppointments(_ShowTemplateAppointment);
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }


        #region Calander Time Duration

        private void tls_btnTimeNavigation_5Min_Click(object sender, EventArgs e)
        {
            juc_Appointment.Interval = Interval.FiveMinutes;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_10Min_Click(object sender, EventArgs e)
        {
            juc_Appointment.Interval = Interval.TenMinutes;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_15Min_Click(object sender, EventArgs e)
        {
            juc_Appointment.Interval = Interval.FifteenMinutes;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_20Min_Click(object sender, EventArgs e)
        {
            juc_Appointment.Interval = Interval.TwentyMinutes;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_30Min_Click(object sender, EventArgs e)
        {
            juc_Appointment.Interval = Interval.ThirtyMinutes;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }
        #endregion

        #endregion

        #region "Procedure & Functions"
        private static Font regFont = null;
        private static Font boldFont = null;
        private void FillProviders()
        {
            _IsProvidersLoading = true;

            gloAppointmnetScheduleCommon oCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);
            //gloGeneralItem.gloItems oProviders = new gloGeneralItem.gloItems();
            gloAppointmentBook.Books.Resource ogloResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dtProviderList = null;
            DataTable dtActiveProviders = null;
            ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
            if (regFont == null)
            {
                regFont = new Font(trvProvider.Font.FontFamily, trvProvider.Font.SizeInPoints, FontStyle.Regular);
            }
            if (boldFont == null)
            {
                boldFont = new Font(trvProvider.Font.FontFamily, trvProvider.Font.SizeInPoints - 1, FontStyle.Bold);
            }
            try
            {
                
                dtActiveProviders = ogloResource.GetActiveProviders();

                trvProvider.Nodes.Clear();
                // oProviders = oCommon.GetProviders();
                dtProviderList = oCommon.GetProviderListView(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()));
                trvProvider.Nodes.Clear();
                if (dtProviderList != null)
                {
                    Int32 nActiveProviderIndex = 0;
                    for (int i = 0; i < dtProviderList.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();

                        // Highlight Active Providers 
                        //bool IsActive = false; 
                        //if (dtActiveProviders != null)
                        //{
                        //    for (int k = 0; k < dtActiveProviders.Rows.Count; k++)
                        //    {
                        //        if (dtProviderList.Rows[i]["ProviderID"] == dtActiveProviders.Rows[k]["nProviderID"])
                        //        {
                        //            IsActive = true; 
                        //        }
                        //    }
                        //    if (IsActive == false)
                        //    {
                        //        oNode.NodeFont = new Font(trvProvider.Font.FontFamily, trvProvider.Font.SizeInPoints, FontStyle.Regular);
                        //    }
                        //    else
                        //    {
                        //        oNode.NodeFont = new Font(trvProvider.Font.FontFamily, trvProvider.Font.SizeInPoints-1, FontStyle.Bold );

                        //    }
                        //}


                        if (dtProviderList.Rows[i]["Active"].ToString() == "0")
                        {
                            oNode.NodeFont = regFont;
                        }
                        else
                        {
                            oNode.NodeFont = boldFont;
                        }

                        oNode.Text = dtProviderList.Rows[i]["ProviderName"].ToString();
                        oNode.Tag = dtProviderList.Rows[i]["ProviderID"] + "~" + dtProviderList.Rows[i]["ProviderName"] + "~" + gloAppointmentScheduling.ASBaseType.Provider.GetHashCode();

                        //oNode.Text = oProviders[i].Description;
                        //oNode.Tag = oProviders[i].ID + "~" + oProviders[i].Description + "~" + gloAppointmentScheduling.ASBaseType.Provider.GetHashCode();

                        if (dtProviderList.Rows[i]["Active"].ToString() == "0")
                            trvProvider.Nodes.Add(oNode);
                        else
                        {
                            trvProvider.Nodes.Insert(nActiveProviderIndex, oNode);
                            nActiveProviderIndex++;
                        }

                        oNode = null;
                    }

                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    string _sLastSelectedProviders = oSettings.ReadSettings_XML("Provider", "LastProviderIDs");
                    oSettings.Dispose();
                    oSettings = null;
                    if (_sLastSelectedProviders.Trim() != "")
                    {
                        string[] ProviderIDs = _sLastSelectedProviders.Split(',');

                        for (int i = 0; i < trvProvider.Nodes.Count; i++)
                        {
                            for (int k = 0; k < ProviderIDs.Length; k++)
                            {
                                if (Convert.ToInt64(ProviderIDs[k]) == Convert.ToInt64(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1)))
                                {
                                    trvProvider.Nodes[i].Checked = true;

                                    // AuditTrail Log for View Provider Appointment
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.View, "View Appointments (" + trvProvider.Nodes[i].Text.Trim() + ")", _PatientD, oShortAppointment.MasterID, Convert.ToInt64(ProviderIDs[k]), ActivityOutCome.Success);
                                }
                            }

                        }
                        if (trvProvider.Nodes.Count == ProviderIDs.Length)
                        {
                            btnDeSelectProvider.Visible = true;
                            btnSelectProvider.Visible = false;
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
                //oProviders.Dispose();
                if (oCommon != null)
                {
                    oCommon.Dispose();
                    oCommon = null;
                }
                if (dtProviderList != null)
                {
                    dtProviderList.Dispose();
                    dtProviderList = null;
                }
                if( ogloResource != null )
                {
                    ogloResource.Dispose();
                    ogloResource=null;
                }
                if (dtActiveProviders != null)
                {
                    dtActiveProviders.Dispose();
                    dtActiveProviders = null;
                }
                oShortAppointment.Dispose();
                oShortAppointment = null;
                _IsProvidersLoading = false;
            }
        }

        private void FillResources()
        {
            gloAppointmentBook.Books.Resource oResources = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dtResources = null;
            ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
            try
            {
               
                dtResources = oResources.GetResources();
                trvResources.Nodes.Clear();
                //AB_Resource_MST.nResourceID, AB_Resource_MST.sDescription 
                if (dtResources != null)
                {
                    for (int i = 0; i < dtResources.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = dtResources.Rows[i]["sCode"].ToString().Trim() + "  " + dtResources.Rows[i]["sDescription"].ToString().Trim();
                        oNode.Tag = Convert.ToInt64(dtResources.Rows[i]["nResourceID"]) + "~" + dtResources.Rows[i]["sDescription"].ToString().Trim() + "~" + gloAppointmentScheduling.ASBaseType.Resource.GetHashCode() + "~" + dtResources.Rows[i]["sCode"].ToString().Trim();

                        trvResources.Nodes.Add(oNode);
                        oNode = null;
                    }

                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    string _sLastSelectedResources = oSettings.ReadSettings_XML("Resource", "LastResourceIDs");
                    oSettings.Dispose();
                    oSettings = null;
                    if (_sLastSelectedResources.Trim() != "")
                    {
                        string[] ResourceIDs = _sLastSelectedResources.Split(',');

                        for (int i = 0; i < trvResources.Nodes.Count; i++)
                        {
                            for (int k = 0; k < ResourceIDs.Length; k++)
                            {
                                if (Convert.ToInt64(ResourceIDs[k]) == Convert.ToInt64(GetTagElement(trvResources.Nodes[i].Tag.ToString(), '~', 1)))
                                {
                                    trvResources.Nodes[i].Checked = true;

                                    // AuditTrail Log for View Provider Appointment
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.View, "View Appointments (" + trvResources.Nodes[i].Text.Trim() + ")", _PatientD, oShortAppointment.MasterID, Convert.ToInt64(ResourceIDs[k]), ActivityOutCome.Success);
                                }
                            }

                        }
                        if (trvResources.Nodes.Count == ResourceIDs.Length)
                        {
                            btnDeSelectResource.Visible = true;
                            btnSelectResource.Visible = false;
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
                if (oResources != null)
                {
                    oResources.Dispose();
                    oResources = null;
                }
                if (dtResources != null)
                {
                    dtResources.Dispose();
                    dtResources = null;
                }
                oShortAppointment.Dispose();
                oShortAppointment = null;
            }
        }

        private void FillLocations()
        {
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable dtLocations = null;
            ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
            try
            {
                
                dtLocations = oLocation.GetList();
                trvLocations.Nodes.Clear();
                //nLocationID, sLocation
                if (dtLocations != null)
                {
                    for (int i = 0; i < dtLocations.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = dtLocations.Rows[i]["sLocation"].ToString().Trim();
                        oNode.Tag = Convert.ToInt64(dtLocations.Rows[i]["nLocationID"]) + "~" + dtLocations.Rows[i]["sLocation"].ToString().Trim();

                        trvLocations.Nodes.Add(oNode);
                        oNode = null;
                    }
                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    string _sLastSelectedLocation = oSettings.ReadSettings_XML("Location", "LastLocationIDs");
                    oSettings.Dispose();
                    oSettings = null;
                    if (_sLastSelectedLocation.Trim() != "")
                    {
                        string[] LocationIDs = _sLastSelectedLocation.Split(',');

                        for (int i = 0; i < trvLocations.Nodes.Count; i++)
                        {
                            for (int k = 0; k < LocationIDs.Length; k++)
                            {
                                if (Convert.ToInt64(LocationIDs[k]) == Convert.ToInt64(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)))
                                {
                                    trvLocations.Nodes[i].Checked = true;

                                    // AuditTrail Log for View Provider Appointment
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.View, "View Appointments (" + trvLocations.Nodes[i].Text.Trim() + ")", _PatientD, oShortAppointment.MasterID, Convert.ToInt64(LocationIDs[k]), ActivityOutCome.Success);
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
                if (oLocation != null)
                {
                    oLocation.Dispose();
                    oLocation = null;
                }
                if (dtLocations != null)
                {
                    dtLocations.Dispose();
                    dtLocations = null;
                }
                oShortAppointment.Dispose();
                oShortAppointment = null;
            }
        }

        private void FillLegend()
        {
            DataTable dt = null;
            gloAppointmentBook.Books.AppointmentType oAppointmentType = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
            try
            {

                dt = oAppointmentType.GetList(gloAppointmentBook.AppointmentProcedureType.AppointmentType);

                Label lblLegendColor;
                Label lblLegendName;

                int XLegColor = 5;
                int YLegColor = 7;
                int XLegName = 20;
                int YLegName = 5;

                for (Int16 i = 0; i < dt.Rows.Count; i++)
                {
                    lblLegendColor = new Label();
                    lblLegendName = new Label();

                    pnlLegendContainer.Controls.Add(lblLegendColor);
                    pnlLegendContainer.Controls.Add(lblLegendName);

                    lblLegendColor.Visible = true;
                    lblLegendName.Visible = true;

                    lblLegendColor.Text = "";
                    lblLegendName.Text = dt.Rows[i]["sAppointmentType"].ToString();

                    lblLegendColor.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[i]["sColorCode"]));
                    lblLegendName.BackColor = Color.Transparent;

                    lblLegendColor.Dock = DockStyle.None;
                    lblLegendName.Dock = DockStyle.None;

                    lblLegendColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    lblLegendName.BorderStyle = System.Windows.Forms.BorderStyle.None;

                    lblLegendColor.Size = new Size(11, 11);
                    lblLegendName.Size = new Size(170, 15);

                    if (i > 0)
                    {
                        YLegName = YLegName + 20;
                        YLegColor = YLegName + 2;
                    }

                    lblLegendColor.Location = new Point(XLegColor, YLegColor);
                    lblLegendName.Location = new Point(XLegName, YLegName);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (dt != null) { dt.Dispose(); dt = null; }
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
            }
        }

        private void FillAppointments(bool FillTemplateBlock)
        {


            gloAppointmentScheduling.gloAppointment oFindAppointments = null;
            gloAppointmentScheduling.CalendarApointmentSchedules oAppointments = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (oPatientListControl != null)
                {
                    pnlOther2.Visible = false;
                    splOther2.Visible = false;
                    pnlOther1.Visible = true;
                    removeOpatientListControl();
                    //oPatientListControl = null;
                    //pnlOther2.Controls.Remove(oPatientListControl);
                }

                juc_Appointment.Update();
                juc_Appointment.Refresh();
                DateTime _FillStartDateTime = juc_Appointment.Dates[0];
                DateTime _FillEndDateTime = juc_Appointment.Dates[juc_Appointment.Dates.Count - 1];
                Int64 _FillClinicID = _ClinicID;

                ArrayList _FillPrvRes = new ArrayList();
                ArrayList _FillProvider = new ArrayList();
                ArrayList _FillResources = new ArrayList();
                ArrayList _FillLocations = new ArrayList();
                //--


                string _LastSelectedOwner = "";

                if (juc_Appointment.Owners.Count > 0)
                {
                    _LastSelectedOwner = juc_Appointment.CurrentOwner.Text;
                }
                juc_Appointment.Appointments.Clear();
                juc_Appointment.Owners.Clear();
                juc_Appointment.Date = _FillStartDateTime;

                #region "Get Provider or Resource List or Locations List"

                //--

                for (int i = 0; i <= trvProvider.Nodes.Count - 1; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        Int64 PrvID = Convert.ToInt64(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1));
                        _FillProvider.Add(PrvID);
                        juc_Appointment.Owners.Add(trvProvider.Nodes[i].Tag, trvProvider.Nodes[i].Text);
                    }
                }

                for (int i = 0; i <= trvResources.Nodes.Count - 1; i++)
                {
                    if (trvResources.Nodes[i].Checked == true)
                    {
                        Int64 ResID = Convert.ToInt64(GetTagElement(trvResources.Nodes[i].Tag.ToString(), '~', 1));
                        _FillResources.Add(ResID);
                        juc_Appointment.Owners.Add(trvResources.Nodes[i].Tag, trvResources.Nodes[i].Text);
                    }
                }

                for (int i = 0; i <= trvLocations.Nodes.Count - 1; i++)
                {
                    if (trvLocations.Nodes[i].Checked == true)
                    {
                        _FillLocations.Add(trvLocations.Nodes[i].Text.Trim());
                    }
                }


                if (_FillProvider.Count <= 0 && _FillResources.Count <= 0)
                {
                    return;
                }
                //--

                #endregion

                 oFindAppointments = new gloAppointment(_databaseconnectionstring);
 
                Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
                Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_Owner;

                if (_FillLocations.Count <= 0)
                    oAppointments = oFindAppointments.GetCalendarAppointments(_FillStartDateTime, _FillEndDateTime, _FillProvider, _FillResources, _FillClinicID, FillTemplateBlock);
                else
                    oAppointments = oFindAppointments.GetCalendarAppointments(_FillStartDateTime, _FillEndDateTime, _FillProvider, _FillResources, _FillLocations, _FillClinicID, FillTemplateBlock);


                if (oAppointments != null)
                {
                    //Added by Mayuri:20100108-To fix issue-#2731- the edit and delete buttons should be disabled if there is no appointment
                    if (oAppointments != null)
                    {
                        if (oAppointments.Count == 0)
                        {
                            tsb_Delete.Enabled = false;
                            tsb_ModifyAppointment.Enabled = false;
                            // tsb
                        }
                        else
                        {
                            tsb_Delete.Enabled = true;
                            tsb_ModifyAppointment.Enabled = true;
                        }
                    }
                    //End code Added by Mayuri:20100108

                    for (int i = 0; i <= oAppointments.Count - 1; i++)
                    {
                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Owner = null;

                        #region "Owner"
                        for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
                        {
                            Int64 OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.Owners[j].Value.ToString(), '~', 1).ToString());
                            gloAppointmentScheduling.ASBaseType OwnerType = (gloAppointmentScheduling.ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.Owners[j].Value.ToString(), '~', 3));

                            if (OwnerID == oAppointments[i].PrvResUsrID && OwnerType == oAppointments[i].PrvResUsrFlag)
                            {
                                oJUC_Owner = juc_Appointment.Owners[j];
                                oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                                break;
                            }
                        }
                        if (oJUC_Owner == null)
                        {
                            oJUC_Owner = new Janus.Windows.Schedule.ScheduleAppointmentOwner();
                        }
                        #endregion

                        #region "Appintment Data"

                        oAppointments[i].ASText = oAppointments[i].ASText.Trim();
                        while (oAppointments[i].ASText.Trim().EndsWith(",") == true)
                        {
                            oAppointments[i].ASText = oAppointments[i].ASText.Remove(oAppointments[i].ASText.Length - 1);
                        }

                        oJUC_Appointment.Prefix = oAppointments[i].ASText; //we add into prefix bcs control is not sorting on prefix, is sorting on text
                        oJUC_Appointment.Text = ""; // oAppointments[i].ASText;

                        oJUC_Appointment.Tag = oAppointments[i].ASTag;

                        if (oAppointments[i].ASDescription == "")
                        {
                            if (oJUC_Appointment.Prefix == "")
                            {
                                oJUC_Appointment.Prefix = oAppointments[i].StartDateTime.ToShortTimeString() + " - " + oAppointments[i].EndDateTime.ToShortTimeString();
                            }
                            else
                            {
                                oJUC_Appointment.Description = oAppointments[i].StartDateTime.ToShortTimeString() + " - " + oAppointments[i].EndDateTime.ToShortTimeString();

                            }
                        }
                        else
                        {
                            oJUC_Appointment.Description = oAppointments[i].ASDescription + ""
                                                    + Environment.NewLine + oAppointments[i].StartDateTime.ToShortTimeString() + " - " + oAppointments[i].EndDateTime.ToShortTimeString();
                        }
                        oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(oAppointments[i].ColorCode);
                        
                        // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                        oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);

                        #endregion

                        #region "Format Appointment"

                        bool _isResourceOnly = false;
                        if (oAppointments[i].PrvResUsrFlag == gloAppointmentScheduling.ASBaseType.Resource)
                        {
                            Int64 MSTAppointmentID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1));
                            _isResourceOnly = IsResourceOnlyAppointment(MSTAppointmentID);
                        }

                        AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                        ASUsedStatus _AppStatus = ASUsedStatus.Unknown5;
                        _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 4));
                        _AppStatus = (ASUsedStatus)Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 10));

                        if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                        {
                            if (chkShowTemplateColor.Checked == true)
                            {
                                oJUC_Appointment.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Horizontal;
                                oJUC_Appointment.FormatStyle.BackColorGradient = Color.GhostWhite;
                            }
                            else
                            {
                                oJUC_Appointment.BorderColor = Color.FromArgb(oAppointments[i].ColorCode);
                                oJUC_Appointment.FormatStyle.BackColor = Color.GhostWhite;

                                //30-Jan-15 Aniket: Resolving Bug #79152: gloEMR - Template Appointment - Application does not display Appointment Type in calendar.
                                oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);
                            }

                            oJUC_Appointment.ImageIndex1 = 0;
                        }
                        if (_AppORTemp == AppointmentScheduleFlag.BlockedSchedule)
                        {
                            oJUC_Appointment.ImageIndex2 = -1;
                        }
                        else
                        {
                            oJUC_Appointment.ImageIndex2 = 8;

                            if (_isResourceOnly == true)
                            {
                                oJUC_Appointment.ImageIndex2 = 9;
                            }
                        }




                        SingleRecurrence _AppSinRec = SingleRecurrence.Single;
                        _AppSinRec = (SingleRecurrence)Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 3));



                        if (_AppSinRec == SingleRecurrence.Recurrence)
                        {
                            oJUC_Appointment.ImageIndex1 = 1;
                        }
                        else if (_AppSinRec == SingleRecurrence.SingleInRecurrence)
                        {
                            oJUC_Appointment.ImageIndex1 = 2;
                        }

                        if (_AppStatus == ASUsedStatus.CheckIn)
                        {
                            oJUC_Appointment.ImageIndex2 = 3;
                        }
                        else if (_AppStatus == ASUsedStatus.CheckOut)
                        {
                            oJUC_Appointment.ImageIndex2 = 4;
                        }


                        #endregion

                        #region "Appointment Date & Time"
                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.EndTime = oAppointments[i].EndDateTime;
                            oJUC_Appointment.StartTime = oAppointments[i].StartDateTime;
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.StartTime = oAppointments[i].StartDateTime;
                                oJUC_Appointment.EndTime = oAppointments[i].EndDateTime;
                            }
                            catch { }
                        }
                        #endregion

                        if (ASUsedStatus.NoShow == _AppStatus || ASUsedStatus.Cancel == _AppStatus)
                        {
                            oJUC_Appointment.FormatStyle.FontStrikeout = TriState.True;
                            oJUC_Appointment.FormatStyle.ForeColor = Color.Maroon;
                        }

                        //if (_AppStatus != ASUsedStatus.NoShow && _AppStatus != ASUsedStatus.Cancel && _AppStatus != ASUsedStatus.Delete)
                        //{
                            //if (oJUC_Appointment.Prefix.Contains("<All Locations>"))
                            //{
                            //    oJUC_Appointment.Prefix = "";
                            //}
                            juc_Appointment.Appointments.Add(oJUC_Appointment);
                       // }
                        //Added by Mayuri:20100108-To fix issue-#2731- the edit and delete buttons should be disabled if there is no appointment
                        if (juc_Appointment.Appointments != null)
                        {
                            if (juc_Appointment.Appointments.Count == 0)
                            {
                                tsb_Delete.Enabled = false;
                                tsb_ModifyAppointment.Enabled = false;
                                // tsb
                            }
                            else
                            {
                                tsb_Delete.Enabled = true;
                                tsb_ModifyAppointment.Enabled = true;
                            }
                        }
                        //End code Added by Mayuri:20100108                       
                        oJUC_Appointment = null;
                    }
                    juc_Appointment.Update();
                }

                #region "Select Current Owner"
                //juc_Appointment.DayColumns = -2;
                if (juc_Appointment.View == ScheduleView.MonthView || juc_Appointment.View == ScheduleView.WeekView)
                {
                    if (_LastSelectedOwner.Trim() != "")
                    {
                        if (juc_Appointment.Owners.Count > 0)
                        {
                            for (int i = 0; i <= juc_Appointment.Owners.Count - 1; i++)
                            {
                                if (juc_Appointment.Owners[i].Text == _LastSelectedOwner)
                                {
                                    juc_Appointment.CurrentOwner = juc_Appointment.Owners[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oFindAppointments != null)
                {
                    oFindAppointments.Dispose();
                    oFindAppointments = null;
                }
                //SLR: 8/20/2014: Please check whether we can free oAppointments or not.
                //if (oAppointments != null)
                //{
                //    oAppointments.Dispose();
                //    oAppointments = null;
                //}
 
            }
        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            //1. Master ID 
            //2. Detail ID 
            //3. Single/Recurrence/SingleInRecurrence 
            //4. Is Appointment or Template Block Hash Code Value (AppointmentScheduleFlag) 
            //(Location & Department for send to app form while register from template block)
            //5. Location ID 
            //6. Location Name 
            //7. Department ID 
            //8. Department Name 
            //9. Line Number
            //10. Used Status
            string[] temp;
            try
            {
                temp = TagContent.Split(Delimeter);
                if (Position - 1 < temp.Length)
                {
                    return temp[Position - 1];
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return (object)"";
        }

        private bool IsResourceOnlyAppointment(Int64 MasterAppointmentID)
        {
            bool _isResourceOnly = false;
            gloAppointment ogloApp = new gloAppointment(_databaseconnectionstring);
            try
            {
                _isResourceOnly = ogloApp.IsResourceOnlyAppointment(MasterAppointmentID);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloApp.Dispose();
                ogloApp = null;
            }
            return _isResourceOnly;

        }


        // By Pranit on 16 feb 2011
        private bool IsProviderResourceAppointment(Int64 MasterAppointmentID)
        {
            bool _isProiverResource = false;
            gloAppointment ogloApp = new gloAppointment(_databaseconnectionstring);
            try
            {
                _isProiverResource = ogloApp.IsProviderResourceAppointment(MasterAppointmentID);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloApp.Dispose();
                ogloApp = null;
            }
            return _isProiverResource;

        }


        private bool IsMultiResourceAppointment(Int64 MasterAppointmentID)
        {
            bool _isResourceOnly = false;
            gloAppointment ogloApp = new gloAppointment(_databaseconnectionstring);
            try
            {
                _isResourceOnly = ogloApp.IsMultiResourceAppointment(MasterAppointmentID);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloApp.Dispose();
                ogloApp = null;
            }
            return _isResourceOnly;

        }

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

                        juc_Appointment.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
                        juc_Appointment.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);


                        try
                        {
                            DateRange dtClinicTimeRange = new DateRange();

                            dtClinicTimeRange.Start = _dtClinicStartTime;
                            dtClinicTimeRange.End = _dtClinicStartTime.AddMinutes(15);

                            //juc_Appointment.FirstVisibleTime = _dtClinicStartTime.AddHours(-1).TimeOfDay;
                            juc_Appointment.FirstVisibleTime = _dtClinicStartTime.TimeOfDay;
                            juc_Appointment.SelectedDays = dtClinicTimeRange;
                            juc_Appointment.Select();
                            
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                        
                    }
                    dtClinicTime.Dispose();
                    dtClinicTime = null;
                }
                ogloSettings.Dispose();
                ogloSettings = null;

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        //private void GetClinicTiming()
        //{
        //    gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
        //    object value = new object();
        //    try
        //    {
        //        ogloSettings.GetSetting("ClinicStartTime", out value);
        //        if (value != null && Convert.ToString(value).Trim() != "")
        //        {
        //            _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
        //            value = null;
        //        }

        //        ogloSettings.GetSetting("ClinicEndTime", out value);
        //        if (value != null && Convert.ToString(value).Trim() != "")
        //        {
        //            _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
        //            value = null;
        //        }

        //        juc_Appointment.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
        //        juc_Appointment.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);

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

        private void GetTemplateSettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            try
            {
                ogloSettings.GetSetting("ShowTemplate", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        tls_btnHideTemplate.Visible = true;
                        tls_btnTemplate.Visible = false;
                        _ShowTemplateAppointment = true;
                    }
                    else
                    {
                        tls_btnHideTemplate.Visible = false;
                        tls_btnTemplate.Visible = true;
                        _ShowTemplateAppointment = false;
                    }
                    value = null;
                }
                else
                {
                    tls_btnHideTemplate.Visible = true;
                    tls_btnTemplate.Visible = false;
                    _ShowTemplateAppointment = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        private void GetZoomTimeSettings()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                string _sZoomInterval = oSettings.ReadSettings_XML("Appointment", "ZoomTime");
                if (_sZoomInterval.Trim() != "")
                {
                    Interval oInterval = (Interval)Convert.ToInt32(_sZoomInterval);
                    switch (oInterval)
                    {
                        case Interval.FiveMinutes:
                            tls_btnTimeNavigation_5Min_Click(null, null);
                            break;
                        case Interval.FifteenMinutes:
                            tls_btnTimeNavigation_15Min_Click(null, null);
                            break;
                        case Interval.TenMinutes:
                            tls_btnTimeNavigation_10Min_Click(null, null);
                            break;
                        //08-Apr-14 Aniket: Resolving Bug #66661:
                        case Interval.TwentyMinutes:
                            tls_btnTimeNavigation_20Min_Click(null, null);
                            break;
                        case Interval.ThirtyMinutes:
                            tls_btnTimeNavigation_30Min_Click(null, null);
                            break;
                        default:
                            tls_btnTimeNavigation_30Min_Click(null, null);
                            break;
                    }
                }

                string _sShowAppointmentColor = Convert.ToString(oSettings.ReadSettings_XML("Appointment", "ShowAppointmentColor"));
                if (_sShowAppointmentColor.Trim() != "")
                {
                    chkShowTemplateColor.Checked = Convert.ToBoolean(_sShowAppointmentColor);
                }
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

        private void GetRegisterAppointmentSettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            try
            {
                ogloSettings.GetSetting("RegisterTemplateAppointmentOnly", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    if (Convert.ToInt16(value) == 1)
                    {
                        _RegAppUsingTemplateOnly = true;
                    }
                    else
                    {
                        _RegAppUsingTemplateOnly = false;
                    }
                }
                else
                {
                    _RegAppUsingTemplateOnly = false;
                }

                if (_RegAppUsingTemplateOnly == true)
                {
                    //tsb_Appointment.Enabled = false;  
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }


        private void GetRegisterAppointmentSettingsUserSpecific()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            bool value = false;
            try
            {
                value = ogloSettings.GetSettingUserSpecific("RegisterTemplateAppointmentOnly", _UserID, _ClinicID);
               if (value == true)
                    {
                        _RegAppUsingTemplateOnly = true;
                    }
               else
                    {
                        _RegAppUsingTemplateOnly = false;
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

        private void RegisterPatientSaveAppointment(string Location, Int64 LocationId)
        {
            SetAppointmentParameter oAppParameters = null;

            //if (juc_Appointment.CurrentAppointment != null)
            if (_LastSelectedAppointment != null)
            {
                AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 4));

                if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                {
                    //1.Get the selected Appointment Parameters

                    #region "Get Appointment Parameter"

                    oAppParameters = new SetAppointmentParameter();
                    oAppParameters.MasterAppointmentID = 0;
                    oAppParameters.AppointmentID = 0;
                    oAppParameters.AppointmentFlag = AppointmentScheduleFlag.TemplateAppointment;
                    oAppParameters.AppointmentTypeID = 0;
                    oAppParameters.AppointmentTypeCode = "";
                    oAppParameters.AppointmentTypeDesc = GetTagElement(_LastSelectedAppointment.Prefix.ToString(), ',', 1).ToString();
                    oAppParameters.ProviderID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Owner.ToString(), '~', 1).ToString());
                    oAppParameters.ProviderName = Convert.ToString(GetTagElement(_LastSelectedAppointment.Owner.ToString(), '~', 2));
                    oAppParameters.ProblemTypes = null;
                    oAppParameters.Resources = null;
                    oAppParameters.PatientID = 0;//Set zero till Patient is Registered
                    oAppParameters.AddTrue_ModifyFalse_Flag = true; // true for new Appointment
                    oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                    oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                    oAppParameters.ModifySingleAppointmentFromReccurence = false;
                    if (GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 6).ToString() != "")
                    {
                        oAppParameters.Location = GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 6).ToString();
                    }
                    else
                    {
                        oAppParameters.Location = Location;
                    }
                    oAppParameters.Department = GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 8).ToString();
                    oAppParameters.StartDate = _LastSelectedAppointment.StartTime;
                    oAppParameters.StartTime = _LastSelectedAppointment.StartTime;
                    oAppParameters.Duration = Convert.ToDecimal(_LastSelectedAppointment.Duration.TotalMinutes);
                    oAppParameters.ClinicID = _ClinicID;
                    oAppParameters.LineNumber = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 9).ToString());

                    // To Register the Template 
                    oAppParameters.TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 1).ToString()); ;
                    oAppParameters.TemplateAllocationID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 2).ToString()); ;

                    oAppParameters.LoadParameters = true;

                    #endregion

                    //2. Initialize Setup Appointment form & set RegisterPatientnSaveAppointment true
                    //   to Register Patient & Save Appointment

                    frmSetupAppointment ofrmSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                    ofrmSetupAppointment.RegisterPatientnSaveAppointment = true;

                    //3.Set the Appointment Parameters to Setup Appointment form
                    ofrmSetupAppointment.SetAppointmentParameters = oAppParameters;

                    //4.Open Form 
                    ofrmSetupAppointment.ShowDialog(this);
                    ofrmSetupAppointment.Dispose();
                    oAppParameters.Dispose();
                }
            }

        }

        #endregion

        #region "Control Events"
        private void btn_Right_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = false;
            pnlSmallStrip.Visible = true;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void btnDeSelectProvider_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.trvProvider.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                }
                catch
                {
                }

                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = false;
                    }
                }
                btnDeSelectProvider.Visible = false;
                btnSelectProvider.Visible = true;

                this.trvProvider.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSelectProvider_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.trvProvider.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                }
                catch
                {
                }
                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = true;
                    }
                }
                btnDeSelectProvider.Visible = true;
                btnSelectProvider.Visible = false;

                this.trvProvider.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnSelectResource_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.trvResources.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                }
                catch
                {
                }
                if (trvResources.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        trvResources.Nodes[i].Checked = true;
                    }
                }
                btnSelectResource.Visible = false;
                btnDeSelectResource.Visible = true;

                this.trvResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDeSelectResource_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.trvResources.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                }
                catch
                {
                }
                btnDeSelectResource.Visible = false;
                btnSelectResource.Visible = true;

                if (trvResources.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        trvResources.Nodes[i].Checked = false;
                    }
                }

                this.trvResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void rbProvider_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbResources_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void trvProvider_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
            try
            {
                    this.Cursor = Cursors.WaitCursor;
                    

                    if (_IsProvidersLoading == false)
                    {

                        FillAppointments(_ShowTemplateAppointment);

                        if (e.Node.Checked == true)
                        {
                            Int64 PrvID = Convert.ToInt64(GetTagElement(e.Node.Tag.ToString(), '~', 1));
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.View, "View Appointments (" + e.Node.Text.Trim() + ")",_PatientD , oShortAppointment.MasterID , PrvID, ActivityOutCome.Success);

                        }

                        #region  selectAll/DeselectAll
                        if (e.Node.Checked == true)
                        {

                            int CountNode = 0;
                            for (int i = 0; i < trvProvider.Nodes.Count; i++)
                            {
                                if (trvProvider.Nodes[i].Checked == true)
                                {
                                    CountNode++;
                                }
                            }
                            if (trvProvider.Nodes.Count == CountNode)
                            {
                                btnDeSelectProvider.Visible = true;
                                btnSelectProvider.Visible = false;
                            }


                        }
                        else
                        {

                            int CountNode = 0;
                            for (int i = 0; i < trvProvider.Nodes.Count; i++)
                            {
                                if (trvProvider.Nodes[i].Checked == false)
                                {
                                    CountNode++;
                                }
                            }
                            if (trvProvider.Nodes.Count == CountNode)
                            {
                                btnDeSelectProvider.Visible = false;
                                btnSelectProvider.Visible = true;

                            }

                        }
                        #endregion

                    }
                    juc_Appointment.DayColumns = GetNumberofColumninDayView();

                   
                    this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void trvResources_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                FillAppointments(_ShowTemplateAppointment);

                #region  selectAll/DeselectAll
                if (e.Node.Checked == true)
                {

                    int CountNode = 0;
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        if (trvResources.Nodes[i].Checked == true)
                        {
                            CountNode++;
                        }
                    }
                    if (trvResources.Nodes.Count == CountNode)
                    {
                        btnDeSelectResource.Visible = true;
                        btnSelectResource.Visible = false;
                    }


                }
                else
                {

                    int CountNode = 0;
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        if (trvResources.Nodes[i].Checked == false)
                        {
                            CountNode++;
                        }
                    }
                    if (trvResources.Nodes.Count == CountNode)
                    {
                        btnDeSelectResource.Visible = false;
                        btnSelectResource.Visible = true;

                    }

                }
                #endregion
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void trvLocations_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }



        private void chkShowTemplateColor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Appointment General Clicks"

        private void juc_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            #region "Today's Selection"
            if (juc_Calendar.CurrentDate != DateTime.Today.Date)
            {
                if (tsb_Today.Checked == true)
                {
                    tsb_Today.Checked = false;
                }
                if (juc_Appointment.View == ScheduleView.DayView)
                {
                    tsb_WeekView.Checked = false;
                    tsb_DayView.Checked = false;
                    tsb_MonthView.Checked = false;
                }
            }
            else
            {
                if (juc_Appointment.View == ScheduleView.DayView)
                {
                    tsb_Today.Checked = true;
                    tsb_WeekView.Checked = false;
                    tsb_DayView.Checked = false;
                    tsb_MonthView.Checked = false;
                }
            }

            #endregion

            FillAppointments(_ShowTemplateAppointment);
            ViewAppointmentDetails();
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void juc_Appointment_DoubleClick(object sender, EventArgs e)
        {
        }

        private void juc_Appointment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            gloUserRights.ClsgloUserRights ObjUserRights = null;
            gloAppointment ogloAppointment = null;
            gloAppointmentBook.Books.Resource oResource = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            DataTable dt=null;
            gloSecurity.gloSecurity oSecurity = null;
            frmSetupAppointment oSetupAppointment = null;
            try
            {

                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                  ObjUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                ObjUserRights.CheckForUserRights(_UserName);
                 ogloAppointment = new gloAppointment(_databaseconnectionstring);
                  oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);


                Int64 _OwnerID = 0;
                Int64 _OwnerTypeID = 0;
                string _OwnerName = "";
                Int64 _resourceID = 0;
                string ProviderName = "";
                String Location = "";
                DateTime Starttime;
                DateTime Endtime;
                DateTime StartDate;
                decimal duration;
                bool isBlocked = false;
             

                Starttime = juc_Appointment.SelectedDays.Start;
                Endtime = juc_Appointment.SelectedDays.End;
                StartDate = juc_Appointment.GetDateAt(e.Location);
                duration = juc_Appointment.MinuteInterval;

                string LocationName = "";
                bool SelectedLocationCount = false;

                if (oPatientListControl != null)
                {
                    for (int i = 0; i <= pnlOther2.Controls.Count - 1; i++)
                    {
                        if (pnlOther2.Controls[i].Name == oPatientListControl.Name)
                        {
                            return;
                        }
                    }
                }

                foreach (TreeNode n in trvLocations.Nodes)
                    if (n.Checked == true)
                    {
                        SelectedLocationCount = true;
                        if (LocationName == "")
                        { LocationName = n.Text.ToString(); }
                        else
                        { LocationName = LocationName + "~" + n.Text.ToString(); }
                    }

                if (ObjUserRights.Appointment == true)
                {
                    //bool _FillAppointments = true;
                    if (juc_Appointment.CurrentAppointment != null)
                    {
                        AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                        _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));
                        if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                        {
                            //_FillAppointments = false;
                        }
                        tsb_ModifyAppointment_Click(null, null);
                    }
                    else
                    {

                        //RegAppUsingTemplateOnly Setting is true then allow to 
                        //create Appointments using template only
                        if (_RegAppUsingTemplateOnly == true)
                        {

                            //if else condition added by dipak to fix mantis bug : 	 0000100: Calendar >> Unnecessarily prompts message while changing date 
                            if (juc_Appointment.GetDateTimeAt(e.Location).TimeOfDay.TotalMinutes == 0)
                            {
                                return;
                            }
                            else
                            {
                                MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //end code added by dipak 20091128
                        }


                      
                        oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                        oClsgloUserRights.CheckForUserRights(_UserName);


                        //////SHUBHANGI 20110201 TO CHECK WHETHER PROVIDER IS BLOKED FOR THE SELECTED SLOT
                        if (juc_Appointment.GetOwnerAt(e.Location) != null)
                        {

                            _OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 1));
                            _OwnerTypeID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 3));
                            _OwnerName = juc_Appointment.GetOwnerAt(e.Location).Text.ToString();
                            _resourceID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 1));
                            
                            ASBaseType OwnerType = ASBaseType.None;
                            OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 3));


                            if (OwnerType == ASBaseType.Provider)
                            {
                                if (_OwnerID > 0)
                                    ProviderName = oResource.GetProviderName(_OwnerID);
                                isBlocked = ogloAppointment.BlockedSlots(_OwnerID, Starttime, Endtime, StartDate,LocationName );

                                if (isBlocked == true)
                                {
                                    isBlocked = false;

                                    dt = ogloAppointment.ResourseName(_resourceID, Starttime, Endtime, StartDate, LocationName);
                                    if (dt.Rows.Count >= 1 && dt != null)
                                    {
                                        string strmsg = "";

                                        //if (juc_Appointment.CurrentAppointment != null)
                                        //{
                                        //    //Added below condition to checked current clicked block is Blocked Schedule the not showing message. Showing
                                        //    //directly schedule screen
                                        //    if ((AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4)) != AppointmentScheduleFlag.BlockedSchedule)
                                        //    {
                                        //        strmsg = "Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Modify this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ";
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    strmsg = "Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ";
                                        //}


                                        if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {
                                            if (juc_Appointment.CurrentAppointment == null)
                                            {
                                                strmsg = "Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ";
                                            }

                                            if (strmsg != "")
                                            {
                                                if (DialogResult.No == MessageBox.Show(strmsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                                {
                                                    _isMessageDisplayed = true;
                                                    return;
                                                }
                                                else
                                                {
                                                    _isMessageDisplayed = true;
                                                }
                                            }
                                            else
                                            {
                                                _isMessageDisplayed = true;
                                            }
                                        }
                                    }
                                }

                            }
                            else if (OwnerType == ASBaseType.Resource)
                            {
                                //TO CHECK WHETHER RESOURSE IS BLOKED FOR THE SELECTED SLOT
                                isBlocked = ogloAppointment.ResourceBlockedSlots(_resourceID, Starttime, Endtime, StartDate);
                                if (isBlocked == true)
                                {
                                    dt = ogloAppointment.ResourseName(_resourceID, Starttime, Endtime, StartDate,Location);
                                    if (dt.Rows.Count >= 1 && dt != null)
                                    {

                                        //if (juc_Appointment.CurrentAppointment != null)
                                        //{
                                        //    if ((AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4)) != AppointmentScheduleFlag.BlockedSchedule)
                                        //    {
                                        //        MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                        //    }
                                        //}
                                        //else

                                        if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {

                                            if (juc_Appointment.CurrentAppointment == null)
                                            {
                                                if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                                {
                                                    return;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }


                        //Added By Pramod Nair For Implementing the User Rights
                        //Get User Name
                        if (appSettings["UserName"] != null)
                        {
                            if (appSettings["UserName"] != "")
                            { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                            else
                            { _UserName = ""; }
                        }
                        else
                        { _UserName = ""; }

                        //Added By Pramod Nair For User Rights Mngnt
                        //oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                        //oClsgloUserRights.CheckForUserRights(_UserName);
                        if (oClsgloUserRights.Appointment)
                        {

                            HitTest HitTestInfo = juc_Appointment.HitTest(e.Location);
                            if (HitTestInfo != HitTest.Schedule)
                            {
                                // if clicked on Calender Header
                                return;
                            }
                            else if (juc_Appointment.GetDateTimeAt(e.Location).TimeOfDay.TotalMinutes == 0)
                            {
                                // if clicked on day nevigation button on Calender Header 
                                return;
                            }

                            ////New Appointment
                             oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                            Int64 _ResourceID = 0;
                            if (juc_Appointment.GetOwnerAt(e.Location) != null)
                            {
                                ASBaseType OwnerType = ASBaseType.None;
                                OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 3));
                                if (OwnerType == ASBaseType.Provider)
                                {
                                    _OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 1));
                                    _OwnerTypeID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 3));
                                    _OwnerName = juc_Appointment.GetOwnerAt(e.Location).Text.ToString();
                                }
                                else if (OwnerType == ASBaseType.Resource)
                                {
                                    _ResourceID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(e.Location).Value.ToString(), '~', 1));
                                }
                            }

                            #region "Set Appointment Parameters"
                            oSetupAppointment.SetAppointmentParameters.MasterAppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.AppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.ClinicID = _ClinicID;
                            oSetupAppointment.SetAppointmentParameters.ProviderID = _OwnerID;
                            oSetupAppointment.SetAppointmentParameters.ProviderName = _OwnerName;
                            oSetupAppointment.SetAppointmentParameters.AddTrue_ModifyFalse_Flag = true; // Add - true, Modify - false
                            oSetupAppointment.SetAppointmentParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifySingleAppointmentFromReccurence = false;
                            oSetupAppointment.SetAppointmentParameters.StartDate = juc_Appointment.GetDateTimeAt(e.Location);// .GetDateAt();
                            oSetupAppointment.SetAppointmentParameters.StartTime = juc_Appointment.GetDateTimeAt(e.Location);//Convert.ToDateTime( juc_Appointment.GetDateTimeAt().TimeOfDay.ToString());//Convert.ToDateTime(string.Format(juc_Appointment.GetDateTimeAt().ToShortTimeString(),"hh:mm:tt"));//Convert.ToDateTime(string.Format(juc_Appointment.GetDateAt().ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(juc_Appointment.GetTimeAt().ToString(), "hh:mm tt"));
                            oSetupAppointment.SetAppointmentParameters.Duration = juc_Appointment.MinuteInterval;
                            oSetupAppointment.SetAppointmentParameters.LoadParameters = true;
                            oSetupAppointment.SetAppointmentParameters.ShowTemplateAppointment_Flag = _ShowTemplateAppointment;


                            if (SelectedLocationCount)
                            {
                                oSetupAppointment.SetAppointmentParameters.Location = GetSelectedLocation(LocationName);
                            }
                            foreach (TreeNode n in trvLocations.Nodes)
                                if (n.Checked == true)
                                {
                                    string str = n.Tag.ToString();
                                    String[] _arrSpliter;
                                    _arrSpliter = str.Split('~');
                                    if (oSetupAppointment.SetAppointmentParameters.LocationIDs == "")
                                    {
                                        oSetupAppointment.SetAppointmentParameters.LocationIDs = _arrSpliter[0];
                                    }
                                    else
                                    {
                                        oSetupAppointment.SetAppointmentParameters.LocationIDs = oSetupAppointment.SetAppointmentParameters.LocationIDs + "~" + _arrSpliter[0];
                                    }
                                }


                            if (_ResourceID > 0)
                            {
                                oSetupAppointment.SetAppointmentParameters.Resources.Add(_ResourceID);
                            }

                            oSetupAppointment.SetAppointmentParameters.PatientID = _PatientD;
                            //code added for lock chart
                            oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);

                            if (oSecurity.isPatientLock(_PatientD, true))
                            {
                                return;
                            }
                            //end of code
                            #endregion
                            oSetupAppointment.ShowDialog(this);
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                        }

                        tsb_Appointment.Enabled = oClsgloUserRights.Appointment;
                        tsb_ModifyAppointment.Enabled = oClsgloUserRights.Appointment;
                        tsb_Delete.Enabled = oClsgloUserRights.Appointment;
                        cmnu_Appointment_New.Enabled = oClsgloUserRights.Appointment;
                        FillAppointments(_ShowTemplateAppointment);
                    }

                    //if (_FillAppointments == true) { FillAppointments(_ShowTemplateAppointment); }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _isMessageDisplayed = false;
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                if (ObjUserRights != null)
                {
                    ObjUserRights.Dispose();
                    ObjUserRights = null;
                }
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (oResource != null)
                {
                    oResource.Dispose();
                    oResource = null;
                }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
                if (oSetupAppointment != null)
                {
                    oSetupAppointment.Dispose();
                    oSetupAppointment = null;
                }
            }
            
        }

        private void juc_Appointment_AppointmentDrag(object sender, AppointmentDragEventArgs e)
        {
            // GLO2011-0011970 
            // If status is legal pending restrict user to drag appointment

            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            if (juc_Appointment.CurrentAppointment != null)
            {
                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());

                if (ogloAppointment.IsLegalPending(nMSTAppointmentID, nDTLAppointmentID))
                {
                    MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }

                AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 4));

                if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                {
                    e.Cancel = true;
                }
                else
                {
                }
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            ogloAppointment.Dispose();
            ogloAppointment = null;
        }

        private void juc_Appointment_MouseMove(object sender, MouseEventArgs e)
        {
            ScheduleAppointment oAppointment = juc_Appointment.GetAppointmentAt(e.X, e.Y);
            if (oAppointment != null)
            {
                AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                {
                    this.Cursor = Cursors.Hand;

                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void juc_Appointment_Click(object sender, EventArgs e)
        {
            try
            {

                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //Clear Appointment Details
                txtViewAppDetails.Clear();
                lblCopayAlert.Text = "";
                lblPrePayAlert.Text = "";
                
              _JanusPastePoint = ((MouseEventArgs)e).Location; 


                if (juc_Appointment.CurrentAppointment != null)
                {
                    this.Cursor = Cursors.WaitCursor;

                    AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                    _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));

                    if (_AppORTemp == AppointmentScheduleFlag.TemplateBlock)
                    {
                        tsb_ModifyAppointment.Enabled = false;
                        tsb_Delete.Enabled = false;

                        if (pnlOther2.Visible == false && oPatientListControl == null)
                        {

                            if (oPatientListControl != null)
                            {
                                //for (int i = 0; i <= pnlOther2.Controls.Count - 1; i++)
                                //{
                                //    if (pnlOther2.Controls[i].Name == oPatientListControl.Name)
                                //    {
                                //        //pnlOther2.Controls.RemoveAt(i);
                                //        oPatientListControl = null;
                                //        pnlOther2.Controls.Remove(oPatientListControl);

                                //        break;
                                //    }
                                //}
                                removeOpatientListControl();
                            }

                            oPatientListControl = new gloPatient.PatientListControl();
                            //oPatientListControl.IsOpenedFromCalender = true;
                            oPatientListControl.DatabaseConnection = _databaseconnectionstring;
                            oPatientListControl.ClinicID = _ClinicID;
                            oPatientListControl.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                            oPatientListControl.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                            oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                            oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);

                            //for (int i = 0; i <= pnlOther2.Controls.Count - 1; i++)
                            //{
                            //    if (pnlOther2.Controls[i].Name == oPatientListControl.Name)
                            //    {
                            //        pnlOther2.Controls.RemoveAt(i);
                            //        break;
                            //    }
                            //}

                            pnlOther2.Height = 0;
                            pnlOther2.Visible = true;
                            splOther2.Visible = true;
                            pnlOther2.Controls.Add(oPatientListControl);
                            oPatientListControl.FillPatients();
                            oPatientListControl.Padding = new Padding(0);
                            oPatientListControl.BringToFront();
                            oPatientListControl.Dock = DockStyle.Fill;
                            oPatientListControl.Select();
                            oPatientListControl.ClearSearch();
                            pnlOther2.Height = 300;
                            pnlOther1.Visible = false;
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        tsb_ModifyAppointment.Enabled = true;
                        tsb_Delete.Enabled = true;

                        #region " Display Selected Appointment Information "

                        ViewAppointmentDetails();

                        #endregion " Display Selected Appointment Information "

                        if (oPatientListControl != null)
                        {
                            //for (int i = 0; i <= pnlOther2.Controls.Count - 1; i++)
                            //{
                            //    if (pnlOther2.Controls[i].Name == oPatientListControl.Name)
                            //    {
                            //        //pnlOther2.Controls.RemoveAt(i);
                            //        //break;
                            //        oPatientListControl = null;
                            //        pnlOther2.Controls.Remove(oPatientListControl);
                            //        break;
                            //    }
                            //}
                            removeOpatientListControl();
                        }

                        pnlOther2.Visible = false;
                        splOther2.Visible = false;
                        pnlOther1.Visible = true;
                    }
                
                }
                else
                {
                    if (oPatientListControl != null)
                    {
                        //for (int i = 0; i <= pnlOther2.Controls.Count - 1; i++)
                        //{
                        //    if (pnlOther2.Controls[i].Name == oPatientListControl.Name)
                        //    {
                        //        //pnlOther2.Controls.RemoveAt(i);
                        //        //break;
                        //        oPatientListControl = null;
                        //        pnlOther2.Controls.Remove(oPatientListControl);
                        //        break;
                        //    }
                        //}
                        removeOpatientListControl();
                    }
                    pnlOther2.Visible = false;
                    splOther2.Visible = false;
                    pnlOther1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void removeOpatientListControl()
        {
            if (oPatientListControl != null)
            {
                if (pnlOther2.Controls.Contains(oPatientListControl))
                {

                    pnlOther2.Controls.Remove(oPatientListControl);

                }
                try
                {
                    oPatientListControl.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                    oPatientListControl.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                    oPatientListControl.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                    oPatientListControl.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);
                }
                catch
                {
                }
                try
                {
                    oPatientListControl.Dispose();
                    oPatientListControl = null;
                }
                catch
                {
                }
            }
        }

        private void juc_Appointment_MouseDown(object sender, MouseEventArgs e)
        {
            gloAppointment ogloAppointment = null;
            DataTable dtPatient = null;
            gloSecurity.gloSecurity oSecurity = null;
            try
            {
                //Clear Appointment Details
                txtViewAppDetails.Clear();
                lblCopayAlert.Text = "";
                lblPrePayAlert.Text = "";
                isTemplate = false;
               
                if (e.Button == MouseButtons.Right)
                {
                    cmnu_Appointment_RegisterPatient.DropDownItems.Clear();

                    _JanusPastePoint = e.Location;

                    juc_Appointment.SelectedAppointments.Clear();
                    if ((juc_Appointment.HitTest(e.X, e.Y) == Janus.Windows.Schedule.HitTest.TimeNavigator) || (juc_Appointment.HitTest(e.X, e.Y) == Janus.Windows.Schedule.HitTest.Header) || (juc_Appointment.HitTest(e.X, e.Y) == Janus.Windows.Schedule.HitTest.AllDayArea))
                    {
                        juc_Appointment.ContextMenuStrip = null;
                        return;
                    }
                    if (juc_Appointment.HitTest(e.X, e.Y) == Janus.Windows.Schedule.HitTest.Appointment)
                    {

                        Point oPoint = new Point(e.X, e.Y);
                        juc_Appointment.SelectedAppointments.Add(juc_Appointment.GetAppointmentAt(oPoint));
                        juc_Appointment.CurrentAppointment = juc_Appointment.GetAppointmentAt(oPoint);

                        //------------
                        _LastSelectedAppointment = juc_Appointment.CurrentAppointment;
                        //------------
                        //code added for lock chart
                          ogloAppointment = new gloAppointment(_databaseconnectionstring);
                        Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                        Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                        
                        dtPatient = ogloAppointment.GetPatient(MasterAppointmentID, DetailAppointmentID);

                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            Int64 nPatient = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                         oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);

                            if (oSecurity.isPatientLock(nPatient, true))
                            {
                                return;
                            }
                        }
                        //end of code
                        ViewAppointmentDetails();

                        ASBaseType OwnerType = ASBaseType.None;
                        OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Owner.ToString(), '~', 3));

                        //  Commented by pranit on 16 feb 2012

                        // juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                        // ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);

                        // By Pranit on 16 feb 2012
                        if (OwnerType == ASBaseType.Resource)
                        {
                            bool _isProviderResource = false;
                            Int64 MSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                            _isProviderResource = IsProviderResourceAppointment(MSTAppointmentID);

                            //Show Right click menu only for Resource Only Appointment if Owner is resource
                            if (_isProviderResource == true)
                            {
                                juc_Appointment.ContextMenuStrip = null;
                            }
                            else
                            {
                                juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                                ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);
                            }
                        }
                        else
                        {
                            juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                            ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);
                        }

                        // End by Pranit on 16 Feb 2012

                        //if (OwnerType == ASBaseType.Resource)
                        //{
                        //    bool _isResourceOnly = false;
                        //    Int64 MSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                        //    _isResourceOnly = IsResourceOnlyAppointment(MSTAppointmentID);

                        //    //Show Right click menu only for Resource Only Appointment if Owner is Resource
                        //    if (_isResourceOnly == true)
                        //    {
                        //        juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                        //        ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);
                        //    }
                        //    else
                        //    {
                        //        //juc_Appointment.ContextMenuStrip = null;
                        //        juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                        //        ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);
                        //    }
                        //}
                        //else
                        //{
                        //    juc_Appointment.ContextMenuStrip = cmnu_AppointmentEdit;
                        //    ShowHideContectMenuItems(juc_Appointment.CurrentAppointment);
                        //}
                    }
                    else
                    {
                       // string owner;
                        juc_Appointment.ContextMenuStrip = cmnu_AppointmentNew;
                        if (cmnu_Appointment_Copy.Tag == null && cmnu_Appointment_Cut.Tag == null)
                        {
                            cmnu_Appointment_Paste.Visible = false;

                        }
                        else
                        {
                            if (juc_Appointment.CurrentOwner != null)
                            {
                                cmnu_Appointment_Paste.Visible = true;
                            }
                            else if (juc_Appointment.CurrentOwner == null)
                            {
                                cmnu_Appointment_Paste.Visible = false;
                            }
                        }
                        //Added By MaheshB
                        if (_RegAppUsingTemplateOnly == true)
                        {
                            cmnu_Appointment_NewPatient.Enabled = false;
                        }
                        else
                        {
                            cmnu_Appointment_NewPatient.Enabled = true;
                        }
                    }
                }
                else
                {
                    juc_Appointment.ContextMenuStrip = null;
                    _LastSelectedAppointment = null;
                }
                SetLicenseModule();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
            }
        }
        private void SetLicenseModule()
        {
            // License Check
            List<object> _ToolStrip = new List<object>();
            _ToolStrip.Add(this.cmnu_Appointment_ModifyPatient);
            _ToolStrip.Add(this.cmnu_Appointment_RegisterPatient);
            _ToolStrip.Add(this.cmnu_Appointment_NewPatient);
            _ToolStrip.Add(this.cmnu_Appointment_New);
            _ToolStrip.Add(this.cmnu_Appointment_Copy);
            _ToolStrip.Add(this.cmnu_Appointment_Paste);
            _ToolStrip.Add(this.tsb_Appointment);
            _ToolStrip.Add(this.tsb_ModifyAppointment);
            base.FormControls = null;
            base.FormControls = _ToolStrip.ToArray();
            base.SetChildFormControls();
            _ToolStrip = null;
            // end License Check
        }
        private void clearAssociationMenus(ToolStripMenuItem oCatMenuItem)
        {
           
            for (int i = oCatMenuItem.DropDownItems.Count - 1; i >= 0; i--)
            {

                try
                {
                    ToolStripMenuItem oTemplateItem = oCatMenuItem.DropDownItems[i] as ToolStripMenuItem;
                    oTemplateItem.Click -= new EventHandler(cmnuTemplateItem_Click);

                    oCatMenuItem.DropDownItems.RemoveAt(i);
                    try
                    {
                        oTemplateItem.Dispose();
                    }
                    catch
                    {
                    }
                }
                catch
                {

                }

            }

        }
        private void clearTemplateMenus(ToolStripMenuItem oCatMenuItem)
        {

            for (int i = oCatMenuItem.DropDownItems.Count - 1; i >= 0; i--)
            {

                try
                {
                    ToolStripMenuItem oTemplateItem = oCatMenuItem.DropDownItems[i] as ToolStripMenuItem;
                    clearAssociationMenus(oTemplateItem);
                    oCatMenuItem.DropDownItems.RemoveAt(i);
                    try
                    {
                        oTemplateItem.Dispose();
                    }
                    catch
                    {
                    }
                }
                catch
                {

                }

            }

        }

        private void cleartsbFollowup(ToolStripDropDownButton tsbFollowup)
        {
            for (int i = tsbFollowup.DropDownItems.Count - 1; i >= 0; i--)
            {
                try
                {
                    ToolStripMenuItem oFolloupSubMenuItem = tsbFollowup.DropDownItems[i] as ToolStripMenuItem;
                    try
                    {
                        oFolloupSubMenuItem.Click -= new EventHandler(cmnuFolloupMenuItem_Click);
                    }
                    catch
                    {
                    }
                    tsbFollowup.DropDownItems.RemoveAt(i);
                    try
                    {
                        oFolloupSubMenuItem.Dispose();
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }
            }
        }
        private void ShowHideContectMenuItems(ScheduleAppointment oAppointment)
        {
            try
            {
                for (int i = cmnuPatientItem_Template.DropDownItems.Count - 1; i >= 0; i--)
                {
                    if (cmnuPatientItem_Template.DropDownItems[i].Text == "All")
                    {
                        try
                        {
                            ToolStripMenuItem catItem = cmnuPatientItem_Template.DropDownItems[i] as ToolStripMenuItem;
                            clearTemplateMenus(catItem);
                            cmnuPatientItem_Template.DropDownItems.RemoveAt(i);
                            try
                            {
                                catItem.Dispose();
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        if (cmnuPatientItem_Template.DropDownItems[i].Text == "Appoiment Letters" || cmnuPatientItem_Template.DropDownItems[i].Text == "Check In")
                        {
                            try
                            {
                                ToolStripMenuItem catItem = cmnuPatientItem_Template.DropDownItems[i] as ToolStripMenuItem;
                                clearAssociationMenus(catItem);
                                cmnuPatientItem_Template.DropDownItems.RemoveAt(i);
                                try
                                {
                                    catItem.Dispose();
                                }
                                catch
                                {
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                cleartsbFollowup(tls_btnFollowUp);
                tls_btnFollowUp.DropDownItems.Clear();
                FillFolowupMenu(tls_btnFollowUp);
                cmnuPatientItem_Template.Enabled = false;
                cmnu_Appointment_Open.Enabled = false;
                cmnu_Appointment_Print.Enabled = false;
                cmnu_Appointment_Delete.Enabled = false;
                cmnu_Appointment_MarkAs.Enabled = false;
                cmnu_Appointment_AddNotes.Enabled = false;
                cmnu_Appointment_Copy.Enabled = false;
                cmnu_Appointment_Cut.Enabled = false;
                cmnu_Appointment_MailToPatient.Enabled = false;
                cmnu_Appointment_RegisterPatient.Enabled = false;
                cmnu_Appointment_CheckIn.Enabled = false;
                cmnu_Appointment_Checkout.Enabled = false;
                cmnu_Appointment_Task.Enabled = false;
                cmnu_Appointment_ModifyPatient.Enabled = false;
                cmnu_AppointmentEdit_Paste.Visible = false; //20091207 dipak
                cmnu_Appointment_ViewHistory.Visible = false;

                AppointmentScheduleFlag ASAppointment = AppointmentScheduleFlag.None;
                ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                if (ASAppointment == AppointmentScheduleFlag.BlockedSchedule)
                {
                    //Added by Amit for open and delete the blocked schedule
                    cmnuPatientItem_Template.Enabled = false;
                    cmnu_Appointment_Open.Enabled = true;
                    cmnu_Appointment_Delete.Enabled = true;
                    cmnu_Appointment_Print.Enabled = false;
                    cmnu_Appointment_MarkAs.Enabled = false;
                    cmnu_Appointment_AddNotes.Enabled = false;
                    cmnu_Appointment_Copy.Enabled = false;
                    cmnu_Appointment_Cut.Enabled = false;
                    cmnu_Appointment_MailToPatient.Enabled = false;
                    cmnu_Appointment_CheckIn.Enabled = false;
                    cmnu_Appointment_Checkout.Enabled = false;
                    cmnu_Appointment_Task.Enabled = false;
                    cmnu_Appointment_ModifyPatient.Enabled = false;
                    cmnu_Appointment_ViewHistory.Visible = false;
                }
                else if (ASAppointment == AppointmentScheduleFlag.TemplateBlock)
                {
                    cmnu_Appointment_RegisterPatient.Enabled = true;

                    //code added by dipak to allow past appointment on template
                    if (cmnu_Appointment_Copy.Tag == null && cmnu_Appointment_Cut.Tag == null)
                    {
                        cmnu_AppointmentEdit_Paste.Visible = false;
                    }
                    else
                    {
                        cmnu_AppointmentEdit_Paste.Visible = true;
                        isTemplate = true;
                    }

                    string _sTemplateLocation = Convert.ToString(GetTagElement(oAppointment.Tag.ToString(), '~', 6));
                    if (_sTemplateLocation == "")
                    {
                        FillRegPatientLocations();
                    }
                }
                else
                {
                    cmnuPatientItem_Template.Enabled = true;
                    cmnu_Appointment_Open.Enabled = true;
                    cmnu_Appointment_Delete.Enabled = true;
                    cmnu_Appointment_Print.Enabled = true;
                    cmnu_Appointment_MarkAs.Enabled = true;
                    cmnu_Appointment_AddNotes.Enabled = true;
                    cmnu_Appointment_Copy.Enabled = true;
                    cmnu_Appointment_Cut.Enabled = true;
                    cmnu_Appointment_MailToPatient.Enabled = true;
                    cmnu_Appointment_CheckIn.Enabled = true;
                    cmnu_Appointment_Checkout.Enabled = true;
                    cmnu_Appointment_Task.Enabled = true;
                    cmnu_Appointment_ModifyPatient.Enabled = true;
                    cmnu_Appointment_ViewHistory.Visible = true;

                    ASUsedStatus oASUsedStatus;
                    oASUsedStatus = (ASUsedStatus)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 10));
                    if (oASUsedStatus == ASUsedStatus.NoShow)
                    {
                        //cmnu_Appointment_MarkAs.Enabled = true; 
                        for (int i = 0; i < cmnu_Appointment_MarkAs.DropDownItems.Count; i++)
                        {
                            if (cmnu_Appointment_MarkAs.DropDownItems[i].Text == "No show")
                            {
                                cmnu_Appointment_MarkAs.DropDownItems[i].Enabled = false;
                            }
                            else
                            {
                                cmnu_Appointment_MarkAs.DropDownItems[i].Enabled = true;
                            }
                        }
                    }
                    else if (oASUsedStatus == ASUsedStatus.Cancel)
                    {
                        for (int i = 0; i < cmnu_Appointment_MarkAs.DropDownItems.Count; i++)
                        {
                            if (cmnu_Appointment_MarkAs.DropDownItems[i].Text == "Cancel")
                            {
                                cmnu_Appointment_MarkAs.DropDownItems[i].Enabled = false;
                            }
                            else
                            {
                                cmnu_Appointment_MarkAs.DropDownItems[i].Enabled = true;
                            }
                        }
                        //cmnu_Appointment_MarkAs.Enabled = false;
                    }


                    gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                    Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1).ToString()); ;
                    Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2).ToString()); ;

                    if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true && oASUsedStatus == ASUsedStatus.CheckOut)
                    {
                        cmnu_Appointment_CheckIn.Enabled = false;
                        cmnu_Appointment_Checkout.Enabled = false;
                    }
                    else if (ogloAppointment.IsPatientCheckIn(MasterAppointmentID, DetailAppointmentID) == true && oASUsedStatus == ASUsedStatus.CheckIn)
                    {
                        cmnu_Appointment_CheckIn.Enabled = false;
                        cmnu_Appointment_Checkout.Enabled = true;
                    }
                    else
                    {
                        if (oAppointment.StartTime.Date == DateTime.Now.Date)
                            cmnu_Appointment_CheckIn.Enabled = true;
                        else
                            cmnu_Appointment_CheckIn.Enabled = false;
                        cmnu_Appointment_Checkout.Enabled = false;
                        cmnu_Appointment_Open.Enabled = true;
                        cmnu_Appointment_Delete.Enabled = true;
                    }
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                    //Fill Word Templates to menu Items
                    // FillTemplatesMenu(cmnuPatientItem_Template);
                    ToolStripMenuItem cmnu_Appointment_Template_All_temp;
                    cmnu_Appointment_Template_All_temp = getAllTemplatesMenu();
                    cmnu_Appointment_Template_All_temp.Text = "All";
                    cmnuPatientItem_Template.DropDownItems.Add(cmnu_Appointment_Template_All_temp);


                    ToolStripMenuItem cmnu_Appointment_Template_ApptLetters_temp;
                    cmnu_Appointment_Template_ApptLetters_temp = Get_AssociationTemplatesMenu(gloOffice.AssociationCategories.AppointmentLetters);
                    cmnu_Appointment_Template_ApptLetters_temp.Text = "Appoiment Letters";
                    cmnuPatientItem_Template.DropDownItems.Add(cmnu_Appointment_Template_ApptLetters_temp);


                    ToolStripMenuItem cmnu_Appointment_Template_ChkIn_temp;
                    cmnu_Appointment_Template_ChkIn_temp = Get_AssociationTemplatesMenu(gloOffice.AssociationCategories.CheckIn);
                    cmnu_Appointment_Template_ChkIn_temp.Text = "Check In";
                    cmnuPatientItem_Template.DropDownItems.Add(cmnu_Appointment_Template_ChkIn_temp);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        private void ViewAppointmentDetails()
        {
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            try
            {
                txtViewAppDetails.Clear();
                lblCopayAlert.Text = "";
                lblPrePayAlert.Text = "";

                if (juc_Appointment.CurrentAppointment != null)
                {

                    Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                    Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;

                    string _AppointmentReferral = "";
                    _AppointmentReferral = ogloAppointment.GetAppointmentReferral(MasterAppointmentID);

                    string _PatientMobileNumber = "";
                    string _PatientPhoneNumber = "";
                    string _PatientNameContact = "";
                    
                    DataTable dtPatient = ogloAppointment.GetPatient(MasterAppointmentID, DetailAppointmentID);
                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                    {
                        _PatientMobileNumber = Convert.ToString(dtPatient.Rows[0]["PatientMobile"]);
                        _PatientPhoneNumber = Convert.ToString(dtPatient.Rows[0]["PatientPhone"]);
                    }

                    string _StartTime = "Start Time - " + juc_Appointment.CurrentAppointment.StartTime.ToString("MM/dd/yyyy hh:mm tt");
                    txtViewAppDetails.AppendText(_StartTime);
                    txtViewAppDetails.AppendText("       ");

                    string _EndTime = "End Time - " + juc_Appointment.CurrentAppointment.EndTime.ToString("MM/dd/yyyy hh:mm tt");
                    txtViewAppDetails.AppendText(_EndTime);

                    if (_PatientPhoneNumber.Trim() != "")
                    { txtViewAppDetails.AppendText(",   Phone : " + _PatientPhoneNumber.Trim() + " "); }
                    if (_PatientMobileNumber.Trim() != "")
                    { txtViewAppDetails.AppendText(",   Mobile : " + _PatientMobileNumber.Trim() + " "); }

                    txtViewAppDetails.AppendText(Environment.NewLine);

                    txtViewAppDetails.Select(0, txtViewAppDetails.Text.Length);
                    txtViewAppDetails.SelectionFont = myBoldFont; // new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    txtViewAppDetails.SelectionColor = Color.FromArgb(194, 72, 0);

                    AppointmentScheduleFlag ASAppointment = AppointmentScheduleFlag.None;
                    ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));

                    if (ASAppointment != AppointmentScheduleFlag.BlockedSchedule)
                    {
                        txtViewAppDetails.AppendText("Patient Name - " + Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 1)));
                        txtViewAppDetails.AppendText(_PatientNameContact);
                        txtViewAppDetails.AppendText(Environment.NewLine);
                        txtViewAppDetails.AppendText("Location - " + Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 2)));
                        txtViewAppDetails.AppendText(", ");
                        txtViewAppDetails.AppendText("Department - " + GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 3));
                        txtViewAppDetails.AppendText(Environment.NewLine);
                        txtViewAppDetails.AppendText("Referral Provider - " + _AppointmentReferral);
                        txtViewAppDetails.AppendText(Environment.NewLine);
                        string _UpdatedName = "";
                        string _UpdatedDateTime = "";
                        DataTable dtUpdate = ogloAppointment.GetUpdatedDateTime(MasterAppointmentID, DetailAppointmentID);
                        if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                        {
                            _UpdatedName = Convert.ToString(dtUpdate.Rows[0]["User"]);
                            _UpdatedDateTime = Convert.ToString(dtUpdate.Rows[0]["UpdatedDateTime"]);
                        }
                        txtViewAppDetails.AppendText("Updated by - " + _UpdatedName);
                        txtViewAppDetails.AppendText(", ");
                        txtViewAppDetails.AppendText("Updated Date/Time - " + _UpdatedDateTime);
                        txtViewAppDetails.AppendText(Environment.NewLine);

                        _UpdatedName = null;
                        _UpdatedDateTime = null;

                        if (dtUpdate != null)
                        {
                            dtUpdate.Dispose();
                            dtUpdate = null;
                        }
                    }
                    else
                    {
                        ASBaseType ASBlockType = ASBaseType.None;
                        ASBlockType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 3));
                        if (ASBlockType == ASBaseType.Provider)
                            txtViewAppDetails.AppendText("Provider Block  ");
                        else
                            txtViewAppDetails.AppendText("Resource Block  ");

                        txtViewAppDetails.AppendText(Environment.NewLine);
                        txtViewAppDetails.AppendText("Location - " + Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 1)));
                        txtViewAppDetails.AppendText(", ");
                        txtViewAppDetails.AppendText("Department - " + GetTagElement(juc_Appointment.CurrentAppointment.Prefix, ',', 2));
                        txtViewAppDetails.AppendText(Environment.NewLine);

                    }
                    
                    string[] _AppDescription = juc_Appointment.CurrentAppointment.Description.Split('\n');
                    if (_AppDescription.Length > 0)
                    {
                        if (_AppDescription.Length > 1)
                        {
                            for (int i = 0; i < _AppDescription.Length - 1; i++)
                            {                                                              
                                    if (i == 0)
                                    {
                                        //GLO2012-0016240 : Appointment block type by time
                                        //Added to show reason for block appointment type
                                        //start
                                        if (_AppDescription[i].Contains(":"))
                                        {
                                            string _Notes = _AppDescription[i].ToString();
                                            string[] _splitNotes = _Notes.Split(':');
                                            for (int j = 0; j < _splitNotes.Length - 1; j++)
                                            {
                                                txtViewAppDetails.AppendText("Block Type - " + _splitNotes[j].ToString() + "\n");
                                                txtViewAppDetails.AppendText("Notes - " + _splitNotes[j + 1].ToString() + "\n");
                                            }
                                        }
                                        else
                                        {
                                            txtViewAppDetails.AppendText("Notes - " + _AppDescription[i].ToString() + "\n");
                                        }
                                        //End
                                    }
                                    else
                                    { txtViewAppDetails.AppendText("         - " + _AppDescription[i].ToString() + "\n"); }  // do not delete that extra space before '-'.It is used for formating the Notes.
                                                              
                            }
                        }
                        else
                        {
                            txtViewAppDetails.AppendText("Notes - ");//+ _AppDescription[0].ToString());
                            txtViewAppDetails.AppendText(Environment.NewLine);                          
                        }
                    }
                    
                    #region Copay Alert

                    lblCopayAlert.Text = "";
                    lblPrePayAlert.Text = "";

                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                    {
                        //For Showing Co-Pay Alert
                        DataTable _dtCopayAlert = GetCopayAlert(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), juc_Appointment.CurrentAppointment.StartTime.Date);
                        if (_dtCopayAlert != null && _dtCopayAlert.Rows.Count > 0)
                        {
                            if (Convert.ToString(_dtCopayAlert.Rows[0]["nCoPay"]) != "")
                            {
                                decimal damount = Convert.ToDecimal((_dtCopayAlert.Rows[0]["nCoPay"]));
                                lblCopayAlert.Text = "Copay Pending : " + "$" + (damount).ToString("N2").Trim();
                            }

                        }
                        ////For Showing Advance
                        //if (IsCopayUnapplied(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"])) == true)
                        //{
                        //    lblPrePayAlert.Text = "Copay not applied";
                        //}
                        if (_dtCopayAlert != null)
                        {
                            _dtCopayAlert.Dispose();
                            _dtCopayAlert = null;
                        }
    
                        tmrAlert.Enabled = true;
                        tmrAlert.Start();
                    }
                    if (dtPatient != null)
                    {
                        dtPatient.Dispose();
                        dtPatient = null;
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
            }
        }
        

        #endregion

        #region "Patient List Control Events"
        void oPatientListControl_Grid_DoubleClick(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (oPatientListControl != null)
            {
                if (oPatientListControl.PatientID > 0)
                {
                    if (juc_Appointment.CurrentAppointment != null)
                    {
                        frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                        SetAppointmentParameter oAppParameters = new SetAppointmentParameter();
                        gloAppointment ogloAppointment = null;
                        DataTable dt = null;
                        gloUserRights.ClsgloUserRights oClsgloUserRights = null;
                        try
                        {
                            #region "Set Appointment Parameter"
                            oAppParameters.MasterAppointmentID = 0;
                            oAppParameters.AppointmentID = 0;
                            oAppParameters.AppointmentFlag = AppointmentScheduleFlag.TemplateAppointment;
                            oAppParameters.AppointmentTypeID = 0;
                            oAppParameters.AppointmentTypeCode = "";
                            //oAppParameters.AppointmentTypeDesc = GetTagElement(juc_Appointment.CurrentAppointment.Text.ToString(), ',', 1).ToString();
                            oAppParameters.AppointmentTypeDesc = GetTagElement(juc_Appointment.CurrentAppointment.Prefix.ToString(), ',', 1).ToString();
                            oAppParameters.ProviderID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1).ToString());
                            oAppParameters.ProviderName = juc_Appointment.CurrentOwner.Text;
                            oAppParameters.ProblemTypes = null;
                            oAppParameters.Resources = null;
                            oAppParameters.PatientID = oPatientListControl.PatientID;
                            oAppParameters.AddTrue_ModifyFalse_Flag = true;
                            oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifySingleAppointmentFromReccurence = false;
                            oAppParameters.Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                            oAppParameters.Department = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 8).ToString();
                            oAppParameters.StartDate = juc_Appointment.CurrentAppointment.StartTime;
                            oAppParameters.StartTime = juc_Appointment.CurrentAppointment.StartTime;
                            oAppParameters.Duration = Convert.ToDecimal(juc_Appointment.CurrentAppointment.Duration.TotalMinutes);
                            oAppParameters.ClinicID = _ClinicID;
                            oAppParameters.LineNumber = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                            // To Register the Template 
                            oAppParameters.TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                            oAppParameters.TemplateAllocationID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;

                            oAppParameters.LoadParameters = true;

                            //Amit to check Show Template Clicked or not when save new Appointment
                            oAppParameters.ShowTemplateAppointment_Flag = _ShowTemplateAppointment;

                            #endregion
                            //SHUBHANGI 20110317 TO RESOLVED 8796
                            ogloAppointment = new gloAppointment(_databaseconnectionstring);

                            DateTime starttime = oAppParameters.StartTime;
                            DateTime endtime = oAppParameters.StartTime.Add(new TimeSpan(0, Convert.ToInt32(juc_Appointment.CurrentAppointment.Duration.TotalMinutes), 0));


                            oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                            oClsgloUserRights.CheckForUserRights(_UserName);

                            if (oAppParameters.Location.ToLower() != "<All Locations>".ToLower())
                            {
                                dt = ogloAppointment.ResourseName(oAppParameters.ProviderID, oAppParameters.StartTime, endtime, oAppParameters.StartDate, oAppParameters.Location);
                                if (dt.Rows.Count >= 1 && dt != null)
                                {
                                    if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                    {
                                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _isMessageDisplayed = true;
                                        pnlOther2.Visible = false;
                                        splOther2.Visible = false;
                                        pnlOther1.Visible = true;
                                        //oPatientListControl = null;
                                        //pnlOther2.Controls.Remove(oPatientListControl);
                                        ////if (oPatientListControl != null) { oPatientListControl.Dispose(); oPatientListControl = null; }
                                        removeOpatientListControl();
                                        return;
                                    }
                                    else
                                    {
                                        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + starttime.ToShortTimeString() + " - " + endtime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {
                                            _isMessageDisplayed = true;
                                            pnlOther2.Visible = false;
                                            splOther2.Visible = false;
                                            pnlOther1.Visible = true;
                                            //oPatientListControl = null;
                                            //pnlOther2.Controls.Remove(oPatientListControl);
                                            ////if (oPatientListControl != null) { oPatientListControl.Dispose(); oPatientListControl = null;}
                                            removeOpatientListControl();
                                            return;
                                        }
                                    }
                                }
                            }

                            oSetupAppointment.SetAppointmentParameters = oAppParameters;
                            oSetupAppointment.ShowDialog(this);

                            if (oSetupAppointment.AppointmentChanged)
                            {
                                pnlOther2.Visible = false;
                                splOther2.Visible = false;
                                pnlOther1.Visible = true;
                                //oPatientListControl = null;
                                //pnlOther2.Controls.Remove(oPatientListControl);
                                removeOpatientListControl();
                                FillAppointments(_ShowTemplateAppointment);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        finally
                        {
                            if (oSetupAppointment != null)
                            {
                                oSetupAppointment.Dispose();
                                oSetupAppointment = null;
                            }
                            if (oAppParameters != null)
                            {
                                oAppParameters.Dispose();
                                oAppParameters = null;
                            }
                            if (oSetupAppointment != null)
                            {
                                oSetupAppointment.Dispose();
                                oSetupAppointment = null;
                            }
                            if (ogloAppointment != null)
                            {
                                ogloAppointment.Dispose();
                                ogloAppointment = null;
                            }
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                            if (oClsgloUserRights != null)
                            {
                                oClsgloUserRights.Dispose();
                                oClsgloUserRights = null;
                            }

                        }
                    }
                    else
                    {
                        pnlOther2.Visible = false;
                        splOther2.Visible = false;
                        pnlOther1.Visible = true;
                        //oPatientListControl = null;
                        //pnlOther2.Controls.Remove(oPatientListControl);
                        ////if (oPatientListControl != null) { oPatientListControl.Dispose(); }
                        removeOpatientListControl();
                    }
                }
                else
                {
                    pnlOther2.Visible = false;
                    splOther2.Visible = false;
                    pnlOther1.Visible = true;
                    //oPatientListControl = null;
                    //pnlOther2.Controls.Remove(oPatientListControl);
                    ////if (oPatientListControl != null) { oPatientListControl.Dispose(); }
                    removeOpatientListControl();
                }
            }
        }
        void oPatientListControl_ItemClosedClick(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlOther2.Visible = false;
            splOther2.Visible = false;
            pnlOther1.Visible = true;
            //oPatientListControl = null;
            //pnlOther2.Controls.Remove(oPatientListControl);
            ////if (oPatientListControl != null) { oPatientListControl.Dispose(); }
            removeOpatientListControl();
        }
        void oPatientListControl_GridRowSelect_Click(object sender, EventArgs e)
        {

        }
        void oPatientListControl_Grid_MouseDown(object sender, EventArgs e)
        {
        }

        #endregion

        #region "Modify Appointments"

        private void juc_Appointment_AppointmentChanging(object sender, AppointmentChangeCancelEventArgs e)
        {
        }

        private void juc_Appointment_DroppingAppointment(object sender, DroppingAppointmentEventArgs e)
        {
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            ShortApointmentSchedule oShortAppointment = null;

            #region Variables to check drag/drop Appointment Type 

            bool IsDragTemplateAppointment = false;
            bool IsDropTemplateAppointment = false;

            bool restrictedTempAppAndSameTypeAppFlag = false;
            
            int dragTemplateAppointmentType = 0;
           // int dropTemplateAppointmentType = 0;   

            long dragTemplateAppointmentTypeID = 0;
            long dropTemplateAppointmentTypeID = 0;

            int dragTemplateAppointmentTypeFlag = 0;
            int dropTemplateAppointmentTypeFlag = 0;

            string dragTemplateAppointmentName = "";
            string dropTemplateAppointmentName = "";

            #endregion

            ScheduleAppointment oAppointment = null;
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //added by dipak 20091207
                AppointmentScheduleFlag ASAppointment = AppointmentScheduleFlag.None;
                
                oAppointment = juc_Appointment.GetAppointmentAt();

             //   bool isBlocked = false;
               // DataTable dt = new DataTable();
                
               //end added by dipak 20091207
                if (e.Appointment != null)
                {
                    ASBaseType OwnerType = ASBaseType.None;
                    ASBaseType ProposedOwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.Appointment.Owner.ToString(), '~', 3));
                    ProposedOwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.ProposedOwner.ToString(), '~', 3));
                    bool _isResourceOnly = false;
                    Int64 MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                    _isResourceOnly = IsResourceOnlyAppointment(MasterID);

                   

                    #region Check Is Drag Appointment Template Appointment or not           


                    // Get Appointment Type Details for Drag Appointment 
                    DataTable dtAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2)));
                    if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 0)
                    {
                        dragTemplateAppointmentType = Convert.ToInt16(dtAppointmentType.Rows[0]["TemplateAppointment"].ToString());
                        dragTemplateAppointmentTypeID = Convert.ToInt64(dtAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                        dragTemplateAppointmentTypeFlag = Convert.ToInt16(dtAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                        dragTemplateAppointmentName = dtAppointmentType.Rows[0]["AppointmentName"].ToString();
                        
                    }
                    if (dtAppointmentType != null)
                    {
                        dtAppointmentType.Dispose();
                        dtAppointmentType = null;
                    }

                    // Check is Drag Appointment an template Appointment
                    if (dragTemplateAppointmentType > 0)
                    {
                        IsDragTemplateAppointment = true;
                    }
                    else
                    {
                        IsDragTemplateAppointment = false;
                    }


                    if (_RegAppUsingTemplateOnly)
                    {
                        if (!IsDragTemplateAppointment)
                        {
                            MessageBox.Show("You don't have rights to Drag/Drop appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            return;
                        }
                    }



                    #endregion




                    if ((OwnerType == ASBaseType.Provider && ProposedOwnerType == ASBaseType.Provider)
                         || (_isResourceOnly == true && OwnerType == ASBaseType.Resource && ProposedOwnerType == ASBaseType.Resource)
                       )
                    {
                        SingleRecurrence AppointmentType;
                        AppointmentType = (SingleRecurrence)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 3));
                        if (AppointmentType == SingleRecurrence.Recurrence || AppointmentType == SingleRecurrence.SingleInRecurrence)
                        {

                            #region " Validate Modify "

                            if (gloDateMaster.gloTime.TimeAsNumber(e.ProposedStartTime.ToShortTimeString()) == 0 && gloDateMaster.gloTime.TimeAsNumber(e.ProposedEndTime.ToShortTimeString()) == 0)
                            {
                                return;
                            }
                            if (ValidateModify(e.Appointment, gloDateMaster.gloTime.TimeAsNumber(e.ProposedStartTime.ToShortTimeString()),
                                                    gloDateMaster.gloTime.TimeAsNumber(e.ProposedEndTime.ToShortTimeString()), GetTagElement(e.ProposedOwner.ToString(), '~', 2).ToString () ) == true)
                            {
                                //Check New Appointment Time is within Clinic Time
                                Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
                                Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
                                Int32 _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(e.ProposedStartTime.ToShortTimeString());
                                Int32 _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(e.ProposedEndTime.ToShortTimeString());
                                Int32 _nProposedStartdate = gloDateMaster.gloDate.DateAsNumber(e.ProposedStartTime.ToShortDateString());

                                DialogResult _DialogResult = DialogResult.None;
                                // Int64 _nBaseID = Convert.ToInt64(GetTagElement(e.Appointment.Owner.ToString(), '~', 1));
                                Int64 _nBaseID = Convert.ToInt64(GetTagElement(e.ProposedOwner.ToString(), '~', 1));
                                #region "Appointment Conflict Module"
                                ////SHUBHANGI 20100726 CHECK FOR CONFLICT
                                Int64 _appointmentID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                                if (IsAppointmentRegisterd(e.ProposedStartTime, e.ProposedEndTime, _nBaseID, _appointmentID) == true)
                                {
                                    DialogResult dgResult = DialogResult.None;
                                    dgResult = MessageBox.Show("Conflicts with another appointment on your calendar. Do you want to save it any way?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    _isConflict = true;
                                    if (dgResult != DialogResult.Yes)
                                    {

                                        return;
                                    }

                                }
                                //END
                                #endregion "Appointment Conflict Module"

                                if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
                                {
                                    _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (_DialogResult == DialogResult.No)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
                                {
                                    _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (_DialogResult == DialogResult.No)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                }

                                //isBlocked = ogloAppointment.ResourceBlockedSlots(_nBaseID, Convert.ToDateTime(e.ProposedStartTime.ToShortTimeString()), Convert.ToDateTime(e.ProposedEndTime.ToShortTimeString()), Convert.ToDateTime(e.ProposedStartTime.ToShortDateString()));
                                //if (isBlocked == true)
                                //{
                                //    dt = ogloAppointment.ResourseName(_nBaseID, e.ProposedStartTime, e.ProposedEndTime, e.ProposedStartTime);
                                //    if (dt.Rows.Count >= 1 && dt != null)
                                //    {
                                //        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + e.ProposedStartTime.ToShortTimeString() + " - " + e.ProposedEndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                //        {
                                //            return;
                                //        }
                                //    }
                                //}
                            }
                            else
                            {
                                e.Cancel = true;
                                return;
                            }

                            #endregion

                            #region " Get Appointment Information "

                              oShortAppointment = new ShortApointmentSchedule();

                            oShortAppointment.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                            oShortAppointment.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2));
                            oShortAppointment.StartDate = e.ProposedStartTime;
                            oShortAppointment.StartTime = e.ProposedStartTime;
                            oShortAppointment.EndDate = e.ProposedEndTime;
                            oShortAppointment.EndTime = e.ProposedEndTime;
                            oShortAppointment.ASCommonFlag = ASBaseType.Provider;
                            oShortAppointment.ASCommonID = Convert.ToInt64(GetTagElement(e.ProposedOwner.ToString(), '~', 1));
                            oShortAppointment.ASCommonCode = "";
                            oShortAppointment.ASCommonDescription = Convert.ToString(GetTagElement(e.ProposedOwner.ToString(), '~', 2));
                            //----Bug #69822: 00000713: Appointment Scheduling. Issue with drag drop of recurrence appointment
                            oShortAppointment.IsRecurrence = (SingleRecurrence)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 3));

                            #endregion " Get Appointment Information "

                            #region "Modify Appointment"

                            if (e.Appointment.Owner == e.ProposedOwner)
                            {
                                // Update Detail entry  for that occurence
                                //UpdateReccurance(oShortAppointment);
                               // MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut); 

                                #region "Allow drop in teplatte for recurance appoinment"
                                if (oAppointment != null)
                                {
                                    ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                                    if (ASAppointment == AppointmentScheduleFlag.TemplateBlock)
                                    {
                                       //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                        Int64 TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1).ToString()); ;
                                        Int64 TemplateAllocationID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2).ToString()); ;
                                        Int64 LineNumber = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 9).ToString());


                                        //Check Drop Appointment an template or not
                                        IsDropTemplateAppointment = true;
                                        DataTable dtDropAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                                        if (dtDropAppointmentType != null && dtDropAppointmentType.Rows.Count > 0)
                                        {
                                            dropTemplateAppointmentTypeID = Convert.ToInt64(dtDropAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                            dropTemplateAppointmentTypeFlag = Convert.ToInt16(dtDropAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                                            dropTemplateAppointmentName = dtDropAppointmentType.Rows[0]["AppointmentName"].ToString();
                                            //dtDropAppointmentType.Dispose();
                                        }
                                        if (dtDropAppointmentType != null)
                                        {
                                            dtDropAppointmentType.Dispose();
                                            dtDropAppointmentType = null;
                                        }
                                        if (_RegAppUsingTemplateOnly)
                                        {
                                            if (!IsDragTemplateAppointment || !IsDropTemplateAppointment)
                                            {
                                                MessageBox.Show("You don't have rights to Drag/Drop appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                e.Cancel = true;
                                                return;
                                            }
                                            else
                                            {
                                                if (dragTemplateAppointmentName  != dropTemplateAppointmentName)
                                                {
                                                    MessageBox.Show("Appointment types are different, cannot Drag/Drop appointments on template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    e.Cancel = true;
                                                    return;
                                                }
                                                else
                                                {
                                                    restrictedTempAppAndSameTypeAppFlag = true;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (IsDropTemplateAppointment)
                                            {
                                                restrictedTempAppAndSameTypeAppFlag = true;
                                            }
                                            else
                                            {
                                                restrictedTempAppAndSameTypeAppFlag = false;
                                            }
                                        }


                                          if (restrictedTempAppAndSameTypeAppFlag == true)
                                            {
                                                oShortAppointment.StartDate = e.ProposedStartTime;
                                                oShortAppointment.StartTime = oAppointment.StartTime;
                                                oShortAppointment.EndDate = e.ProposedEndTime;
                                                oShortAppointment.EndTime = oAppointment.EndTime;

                                            }


                                          UpdateSingleInReccurance(oShortAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);



                                        //if (oDB != null)
                                        //{
                                        //    oDB.Connect(false);
                                        //    oDB.Execute_Query("UPDATE AB_AppointmentTemplate_Allocation SET nIsRegistered = 1 WHERE nTemplateAllocationMasterID = " + TemplateAllocationMasterID + " AND nTemplateAllocationID = " + TemplateAllocationID + "");
                                        //}

                                    }
                                    else
                                    {
                                      //  MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut); 

                                        if (gloDateMaster.gloDate.DateAsNumber(e.ProposedStartTime.ToShortDateString()) == gloDateMaster.gloDate.DateAsNumber(e.Appointment.StartTime.ToShortDateString ()))
                                        {
                                            UpdateReccurance(oShortAppointment);
                                        }
                                        else
                                        {
                                            MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut);
                                        }
                                    }

                                }
                                else
                                {
                                    //MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut); 

                                    if (gloDateMaster.gloDate.DateAsNumber(e.ProposedStartTime.ToShortDateString()) == gloDateMaster.gloDate.DateAsNumber(e.Appointment.StartTime.ToShortDateString()))
                                    {
                                        UpdateReccurance(oShortAppointment);
                                    }
                                    else
                                    {
                                        MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut);
                                    }
                                }



                                #endregion


                                FillAppointments(_ShowTemplateAppointment);
                            }
                            else
                            {
                                //Create single Appointment
                                try
                                {

                                    if (oAppointment != null)
                                    {
                                        ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                                        if (ASAppointment == AppointmentScheduleFlag.TemplateBlock)
                                        {
                                            Int64 TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1).ToString()); ;
                                            Int64 TemplateAllocationID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2).ToString()); ;
                                            Int64 LineNumber = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 9).ToString());


                                            //Check Drop Appointment an template or not
                                            IsDropTemplateAppointment = true;
                                            DataTable dtDropAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                                            if (dtDropAppointmentType != null && dtDropAppointmentType.Rows.Count > 0)
                                            {
                                                dropTemplateAppointmentTypeID = Convert.ToInt64(dtDropAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                                dropTemplateAppointmentTypeFlag = Convert.ToInt16(dtDropAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                                                dropTemplateAppointmentName = dtDropAppointmentType.Rows[0]["AppointmentName"].ToString();
                                              //  dtDropAppointmentType.Dispose();
                                            }
                                            if (dtDropAppointmentType != null)
                                            {
                                                dtDropAppointmentType.Dispose();
                                                dtDropAppointmentType = null;
                                            }
                                            if (_RegAppUsingTemplateOnly)
                                            {
                                                if (!IsDragTemplateAppointment || !IsDropTemplateAppointment)
                                                {
                                                    MessageBox.Show("You don't have rights to Drag/Drop appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    e.Cancel = true;
                                                    return;
                                                }
                                                else
                                                {
                                                    if (dragTemplateAppointmentName != dropTemplateAppointmentName)
                                                    {
                                                        MessageBox.Show("Appointment types are different, cannot Drag/Drop appointments on template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        e.Cancel = true;
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        restrictedTempAppAndSameTypeAppFlag = true;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (IsDropTemplateAppointment)
                                                {
                                                    restrictedTempAppAndSameTypeAppFlag = true;
                                                }
                                                else
                                                {
                                                    restrictedTempAppAndSameTypeAppFlag = false;
                                                }
                                            }


                                            oShortAppointment.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                                            oShortAppointment.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2));

                                           if (restrictedTempAppAndSameTypeAppFlag == true)
                                            {
                                                oShortAppointment.StartDate = e.ProposedStartTime;
                                                oShortAppointment.StartTime = oAppointment.StartTime;
                                                oShortAppointment.EndDate = e.ProposedEndTime;
                                                oShortAppointment.EndTime = oAppointment.EndTime;

                                            }

                                            
                                            
                                            
                                            UpdateSingleInReccurance(oShortAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                                        }
                                        else
                                        {
                                            //UpdateSingleInReccurance(oShortAppointment);
                                            MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut); 
                                        }
                                    }
                                    else
                                    {
                                        //UpdateSingleInReccurance(oShortAppointment);
                                        MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut); 

                                    }
                                    FillAppointments(_ShowTemplateAppointment);

                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                                }


                            }

                            #endregion

                        }
                        //---
                        else
                        {
                            try
                            {
                                if (oAppointment != null)
                                {
                                    ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                                    oShortAppointment = new ShortApointmentSchedule();
                                    if (ASAppointment == AppointmentScheduleFlag.TemplateBlock)
                                    {
                                        if (ValidateModify(e.Appointment) == true)
                                        {

                                            Int64 TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1).ToString()); ;
                                            Int64 TemplateAllocationID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2).ToString()); ;
                                            Int64 LineNumber = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 9).ToString());
                                            
                                            
                                            //Check Drop Appointment an template or not
                                              IsDropTemplateAppointment = true;
                                              DataTable dtDropAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                                              if (dtDropAppointmentType != null && dtDropAppointmentType.Rows.Count > 0)
                                                {
                                                    dropTemplateAppointmentTypeID = Convert.ToInt64(dtDropAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                                    dropTemplateAppointmentTypeFlag = Convert.ToInt16(dtDropAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                                                    dropTemplateAppointmentName = dtDropAppointmentType.Rows[0]["AppointmentName"].ToString();
                                                    dtDropAppointmentType.Dispose();
                                                }
                                              if (dtDropAppointmentType != null)
                                              {
                                                  dtDropAppointmentType.Dispose();
                                                  dtDropAppointmentType = null;
                                              }
                                              if (_RegAppUsingTemplateOnly)
                                              {
                                                  if (!IsDragTemplateAppointment || !IsDropTemplateAppointment)
                                                  {
                                                      MessageBox.Show("You don't have rights to Drag/Drop appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                      e.Cancel = true;
                                                      return;
                                                  }
                                                  else
                                                  {
                                                      if (dragTemplateAppointmentName != dropTemplateAppointmentName)
                                                      {
                                                          MessageBox.Show("Appointment types are different, cannot Drag/Drop appointments on template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                          e.Cancel = true;
                                                          return;
                                                      }
                                                      else
                                                      {
                                                          restrictedTempAppAndSameTypeAppFlag = true;
                                                      }
                                                  }

                                              }
                                              else
                                              {
                                                  if (IsDropTemplateAppointment)
                                                  {
                                                      restrictedTempAppAndSameTypeAppFlag = true;
                                                  }
                                                  else
                                                  {
                                                      restrictedTempAppAndSameTypeAppFlag = false;
                                                  }
                                              }
                                      

                                            oShortAppointment.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                                            oShortAppointment.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2));

                                            if (restrictedTempAppAndSameTypeAppFlag == false)
                                            {
                                                oShortAppointment.StartDate = e.ProposedStartTime;
                                                oShortAppointment.StartTime = e.ProposedStartTime;
                                                oShortAppointment.EndDate = e.ProposedEndTime;
                                                oShortAppointment.EndTime = e.ProposedEndTime;
                                            }
                                            else if (restrictedTempAppAndSameTypeAppFlag == true)
                                            {
                                                oShortAppointment.StartDate = e.ProposedStartTime;
                                                oShortAppointment.StartTime = oAppointment.StartTime;
                                                oShortAppointment.EndDate = e.ProposedEndTime;
                                                oShortAppointment.EndTime = oAppointment.EndTime;

                                            }




                                            oShortAppointment.ASCommonFlag = ASBaseType.Provider;
                                            oShortAppointment.ASCommonID = Convert.ToInt64(GetTagElement(e.ProposedOwner.ToString(), '~', 1));
                                            oShortAppointment.ASCommonCode = "";
                                            oShortAppointment.ASCommonDescription = Convert.ToString(GetTagElement(e.ProposedOwner.ToString(), '~', 2));
                                            oShortAppointment.ASFlag = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 4));
                                            UpdateSingleInReccurance(oShortAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                                            FillAppointments(_ShowTemplateAppointment);
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (oShortAppointment != null)
                {
                    oShortAppointment.Dispose();
                    oShortAppointment = null;
                }
                if (oAppointment != null)
                {
                    oAppointment = null;
                }
            }
        }

        private void juc_Appointment_AppointmentChanged(object sender, AppointmentChangeEventArgs e)
        {
           

            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                if (e.Appointment != null)
                {
                    ASBaseType OwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.Appointment.Owner.ToString(), '~', 3));
                    Int64 _appointmentID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                    bool _isResourceOnly = IsResourceOnlyAppointment(_appointmentID);
                    string _sProviderName = Convert.ToString(GetTagElement(e.Appointment.Owner.ToString(), '~', 2));
                    Int64 _nBaseID = Convert.ToInt64(GetTagElement(e.Appointment.Owner.ToString(), '~', 1));
                    Int64 AppProvoderID = 0;

                    //Check appointment an template Appointment or not
                    bool IsDragDropTemplateAppointment = false;
                   

                    int dragDropTemplateAppointmentType = 0;
                    long dragDropTemplateAppointmentTypeID = 0;
                    int dragDropTemplateAppointmentTypeFlag = 0;


                    #region "Check License"
                    if (base.SetChildFormModules("juc_Appointment_AppointmentChanged", "Modify Appointment", Convert.ToString(_nBaseID)) == true)
                    {
                        return;
                    }
                    #endregion "" 


                    // Get Appointment Type Details for Appointment 
                    DataTable dtAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2)));
                    if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 0)
                    {
                        dragDropTemplateAppointmentType = Convert.ToInt16(dtAppointmentType.Rows[0]["TemplateAppointment"].ToString());
                        dragDropTemplateAppointmentTypeID = Convert.ToInt64(dtAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                        dragDropTemplateAppointmentTypeFlag = Convert.ToInt16(dtAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                        //dtAppointmentType.Dispose();
                    }
                    if (dtAppointmentType != null)
                    {
                        dtAppointmentType.Dispose();
                        dtAppointmentType = null;
                    }

                    // Check is Drag/Drop Appointment an template Appointment
                    if (dragDropTemplateAppointmentType > 0)
                    {
                        IsDragDropTemplateAppointment = true;
                    }
                    else
                    {
                        IsDragDropTemplateAppointment = false;
                    }



                    if (_RegAppUsingTemplateOnly)
                    {
                        if (IsDragDropTemplateAppointment)
                        {
                            MessageBox.Show("You don't have rights to Drag/Drop appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }


                    //Int32 _nProposedStartTime1 = gloDateMaster.gloDate.DateAsNumber(e.Appointment.StartTime.ToShortDateString());
                    //Int32 _nProposedEndTime1 = gloDateMaster.gloDate.DateAsNumber(e.Appointment.EndTime.ToShortDateString());


                    //if (_nProposedStartTime1 != _nProposedEndTime1)
                    //{
                    //    return;
                    //}

                    TimeSpan span = e.Appointment.EndTime.Subtract(e.Appointment.StartTime);

                    if (span.TotalHours > 24)
                    {
                        return;
                    }

                    if (OwnerType == ASBaseType.Provider || (_isResourceOnly == true && OwnerType == ASBaseType.Resource))
                    {
                        if (gloDateMaster.gloTime.TimeAsNumber(e.Appointment.StartTime.ToShortTimeString()) == 0 && gloDateMaster.gloTime.TimeAsNumber(e.Appointment.EndTime.ToShortTimeString()) == 0)
                        {
                            return;
                        }
                        #region "Validate Modify"
                        if (ValidateModify(e.Appointment) == true)
                        {
                            //Check New Appointment Time is within Clinic Time
                            Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
                            Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
                            Int32 _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(e.Appointment.StartTime.ToShortTimeString());
                            Int32 _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(e.Appointment.EndTime.ToShortTimeString());
                            DialogResult _DialogResult = DialogResult.None;

                            //CHECK FOR CONFLICT

                            if (_isConflict == false)
                            {
                                if (IsAppointmentRegisterd(e.Appointment.StartTime, e.Appointment.EndTime, _nBaseID, _appointmentID) == true)
                                {
                                    _isConflict = false;
                                    DialogResult dgResult = DialogResult.None;
                                    dgResult = MessageBox.Show("Warning –  " + _sProviderName + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (dgResult != DialogResult.Yes)
                                    {
                                        return;
                                    }
                                }
                            }
                            else { return; }

                            if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
                            {
                                _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (_DialogResult == DialogResult.No)
                                {
                                    FillAppointments(_ShowTemplateAppointment);
                                    return;
                                }
                            }
                            else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
                            {
                                _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (_DialogResult == DialogResult.No)
                                {
                                    FillAppointments(_ShowTemplateAppointment);
                                    return;
                                }
                            }

                        }
                        else
                        {
                            FillAppointments(_ShowTemplateAppointment);
                            return;
                        }
                        #endregion

                        #region "Modify Appointment"

                        SingleRecurrence AppointmentType;
                        AppointmentType = (SingleRecurrence)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 3));
                        
                        #region " Get Appointment Information "

                        ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
                        MasterAppointment omstAppointment = new MasterAppointment();
                        ShortApointmentSchedule oShortResource = null;
                        try
                        {
                            oShortAppointment.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                            oShortAppointment.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2));
                            oShortAppointment.StartDate = e.Appointment.StartTime;
                            oShortAppointment.StartTime = e.Appointment.StartTime;
                            oShortAppointment.EndDate = e.Appointment.EndTime;
                            oShortAppointment.EndTime = e.Appointment.EndTime;




                            if (oShortAppointment.ASFlag == AppointmentScheduleFlag.TemplateAppointment)
                            {
                                oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                            }
                            else
                            {
                                if (IsMaximumAppointmentRegisterd(oShortAppointment.StartDate, oShortAppointment.StartTime, _nBaseID) == false)
                                {
                                    oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                                }

                                //In drag-Drop case, no need to show below message

                                else
                                {
                                    gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                                    using (DataTable dtPatient = ogloAppointment.GetAppointmentProviderID(oShortAppointment.MasterID))
                                    {
                                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                                        {
                                            AppProvoderID = Convert.ToInt64(dtPatient.Rows[0]["nASBaseID"]);
                                        }
                                    }
                                    ogloAppointment.Dispose();
                                    ogloAppointment = null;
                                    if (AppProvoderID != _nBaseID)
                                    {
                                        DialogResult dgResult = DialogResult.None;
                                        //dgResult = MessageBox.Show("All appointments for " + _sProviderName + " are filled for this time.  Do you want to create an additional appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                        dgResult = MessageBox.Show("All appointments for " + _sProviderName + " are filled for this time.  Do you want to continue?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (dgResult == DialogResult.Yes)
                                        {
                                            oShortAppointment.UsedStatus = ASUsedStatus.Waiting;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                            // to resolved issue 10901 Check usedStatus
                            //if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oShortAppointment.UsedStatus == ASUsedStatus.CheckIn)
                            //{
                            //    if (MessageBox.Show("You have changed the appointment date for this appointment to " + e.Appointment.StartTime.ToShortDateString() + ".The appointment status will be reset and the appointment will no longer be checked in."
                            //             + Environment.NewLine + "Are you sure you want to move this appointment to " + e.Appointment.StartTime.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            //    {
                            //        oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                            //        omstAppointment.AppointmentStatusID = 1;
                            //    }
                            //    else
                            //    {
                            //        return;
                            //    }

                            //}

                            if (_isResourceOnly == false)
                            {
                                oShortAppointment.ASCommonFlag = ASBaseType.Provider;
                                oShortAppointment.ASCommonID = Convert.ToInt64(GetTagElement(e.Appointment.Owner.ToString(), '~', 1));
                                oShortAppointment.ASCommonCode = "";
                                oShortAppointment.ASCommonDescription = Convert.ToString(GetTagElement(e.Appointment.Owner.ToString(), '~', 2)).Replace("'", "''");
                            }
                            else
                            {
                                oShortAppointment.ASCommonFlag = ASBaseType.Resource;
                                oShortAppointment.ASCommonID = 0;
                                oShortAppointment.ASCommonCode = "";
                                oShortAppointment.ASCommonDescription = "";
                            }


                            oShortResource = new ShortApointmentSchedule();
                            if (_isResourceOnly == true)
                            {
                                oShortResource.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1)); ;
                                oShortResource.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2)); ;
                                oShortResource.StartDate = e.Appointment.StartTime; ;
                                oShortResource.StartTime = e.Appointment.StartTime;
                                oShortResource.EndDate = e.Appointment.EndTime; ;
                                oShortResource.EndTime = e.Appointment.EndTime;
                                oShortResource.ASCommonFlag = ASBaseType.Resource;
                                oShortResource.ASCommonID = Convert.ToInt64(GetTagElement(e.Appointment.Owner.ToString(), '~', 1));
                                oShortResource.ASCommonCode = Convert.ToString(GetTagElement(e.Appointment.Owner.ToString(), '~', 4));
                                oShortResource.ASCommonDescription = Convert.ToString(GetTagElement(e.Appointment.Owner.ToString(), '~', 2));
                                oShortResource.IsRecurrence = (SingleRecurrence)Convert.ToInt32(GetTagElement(e.Appointment.Tag.ToString(), '~', 3));
                                oShortResource.Location = Convert.ToString(GetTagElement(e.Appointment.Tag.ToString(), '~', 6));
                            }
                        #endregion " Get Appointment Information "


                            if (AppointmentType == SingleRecurrence.Single)
                            {
                                if (_isResourceOnly == false)
                                    //UpdateSingleAppointment(oShortAppointment, omstAppointment);

                                    MakePasteAppointment(oShortAppointment, CutCopyPaste.Cut);
                                else
                                    UpdateSingleResourceAppointment(oShortAppointment, oShortResource, omstAppointment);
                            }
                            else if (AppointmentType == SingleRecurrence.Recurrence || AppointmentType == SingleRecurrence.SingleInRecurrence)
                            {
                                UpdateReccurance(oShortAppointment);
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            if (oShortAppointment != null)
                            {
                                oShortAppointment.Dispose();
                                oShortAppointment = null;
                            }
                            if (omstAppointment != null)
                            {
                                omstAppointment.Dispose();
                                omstAppointment = null;
                            }
                            if (oShortResource != null)
                            {
                                oShortResource.Dispose();
                                oShortResource = null;
                            }

                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                FillAppointments(_ShowTemplateAppointment);
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
            finally
            {
                ogloAppointment.Dispose();
                ogloAppointment = null;
            }
            return _result;
        }

        //
        private bool ValidateModify(ScheduleAppointment oAppointment, int ProposedStartTime = 0, int ProposedEndTime =0, string ProposedOwner = "")
        {
            bool _result = false;
            Boolean _IsModified = false; //SET FOR ALLOW TO MODIFY CHECKED OUT APPOINTMENT 
         //   DataTable dt = new DataTable();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
           // gloPatient.gloPatient ogloPatient = null;
            MasterAppointment oMasterAppointment = null;
            ScheduleAppointment oTemplateAppointment = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = null;
            try
            {
                // Template/Blocked Schedule/Used Appointments can not be modified
                AppointmentScheduleFlag ASAppointment = AppointmentScheduleFlag.None;
                ASAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 4));
                if (ASAppointment == AppointmentScheduleFlag.TemplateBlock || ASAppointment == AppointmentScheduleFlag.BlockedSchedule)
                {
                    if (ASAppointment == AppointmentScheduleFlag.BlockedSchedule)
                    {
                        MessageBox.Show(" Provider schedule can not be modified.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return false;
                }

                //Checked In Appointments cannot be modified
                //gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                Int64 MasterAppointmentID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1).ToString()); ;
                Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2).ToString()); ;
                Int64 ProviderID = Convert.ToInt64(GetTagElement(oAppointment.Owner.ToString(), '~', 1));

                if (IsMultiResourceAppointment(MasterAppointmentID) == true)
                {
                    MessageBox.Show("Drag is disabled for Multi-resource appointments. Try the Edit option.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

             //   ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                _PatientName = ogloAppointment.GetAppointmentPatientName(MasterAppointmentID);

                String AddMessage = "";


                if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true)
                {
                    AddMessage = "Patient '" + _PatientName + "' is already checked out. ";
                    //if (ogloAppointment.IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == true)
                    //{
                    //    if (MessageBox.Show("Patient '" + _PatientName + "' is already checked out.  Are you sure you wish to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    //    {
                    //        _result = true;
                    //        _IsModified = true;
                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }
                    //}
                }


                oMasterAppointment = ogloAppointment.GetMasterAppointment(MasterAppointmentID, DetailAppointmentID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);

                decimal OldDuration = oMasterAppointment.Duration;

                // Ask for Modification  
                SingleRecurrence AppointmentType;
                AppointmentType = (SingleRecurrence)Convert.ToInt32(GetTagElement(oAppointment.Tag.ToString(), '~', 3));

                oTemplateAppointment = juc_Appointment.GetAppointmentAt();

                Int16 IsTemplate = 0;

                if (oTemplateAppointment != null)
                {
                    IsTemplate = Convert.ToInt16(GetTagElement(oTemplateAppointment.Tag.ToString(), '~', 10));
                }


                if (AppointmentType == SingleRecurrence.Single)
                {
                    if (_isConflict == false && _IsModified == false)
                    {
                        if (_ShowTemplateAppointment)
                        {
                            if (oTemplateAppointment != null)
                            {
                                if (IsTemplate == 0)
                                {
                                    AppointmentScheduleFlag ASTemplateAppointment = AppointmentScheduleFlag.None;
                                    ASTemplateAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oTemplateAppointment.Tag.ToString(), '~', 4));


                                    if (ASTemplateAppointment == AppointmentScheduleFlag.TemplateBlock)
                                    {

                                        // Check Condition 
                                        if (oAppointment != null)
                                        {
                                            // if ((decimal)oAppointment.Duration.TotalMinutes == (decimal)oTemplateAppointment.Duration.TotalMinutes)
                                            // {
                                            if (CheckSameTemplateSlotType(oTemplateAppointment, oAppointment) == true)
                                            {
                                                if (MessageBox.Show(AddMessage + "Do you wish to book this appointment into the existing template slot?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    return _result = true;

                                                }
                                                else
                                                {
                                                    return _result = false;
                                                }


                                            }
                                            //}
                                        }


                                        if (MessageBox.Show(AddMessage + "The appointment will take on the appointment type and length of the new template slot. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            _result = true;
                                        }

                                    }

                                    else
                                        if (MessageBox.Show(AddMessage + "The appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            _result = true;
                                        }
                                }
                                //Template==1
                                else
                                {


                                    // check any slot present with this oAppointment start time , end time and appointment type


                                    DataTable dtPasteAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2)));
                                    long pasteTemplateAppointmentTypeID = 0;
                                    int pasteTemplateAppointmentDuration = 0;
                                    if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                                    {
                                        pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                        pasteTemplateAppointmentDuration = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentDuration"].ToString());
                                        // dtPasteAppointmentType.Dispose();
                                    }
                                    if (dtPasteAppointmentType != null)
                                    {
                                        dtPasteAppointmentType.Dispose();
                                        dtPasteAppointmentType = null;
                                    }

                                    // Get ALL SELECTED LOCATIONS


                                    string _sLastSelectedLocation = string.Empty;


                                    for (int i = 0; i < trvLocations.Nodes.Count; i++)
                                    {
                                        if (trvLocations.Nodes[i].Checked == true)
                                        {
                                            _sLastSelectedLocation += Convert.ToString(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                                        }
                                    }
                                    if (_sLastSelectedLocation.Trim() != "")
                                    {
                                        _sLastSelectedLocation = _sLastSelectedLocation.Remove(_sLastSelectedLocation.LastIndexOf(','));
                                    }




                                    if (CheckSlotPresent(oAppointment.StartTime, gloDateMaster.gloTime.TimeAsNumber(oAppointment.StartTime.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(oAppointment.EndTime.TimeOfDay.ToString()), pasteTemplateAppointmentTypeID, ProviderID, _ClinicID, Convert.ToDateTime(oAppointment.StartTime.TimeOfDay.ToString()), Convert.ToDateTime(oAppointment.EndTime.TimeOfDay.ToString()), _sLastSelectedLocation, Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2))) == true)
                                    {
                                        //  if ((decimal)oAppointment.Duration.TotalMinutes == (decimal)pasteTemplateAppointmentDuration)
                                        // {
                                        if (MessageBox.Show(AddMessage + "Do you wish to double book this time slot, placing the new appointment in the slot and retaining an open slot at this time?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            return _result = true;
                                        }
                                        else
                                        {
                                            return _result = false;
                                        }
                                        //  }
                                    }


                                    if (MessageBox.Show(AddMessage + "The appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _result = true;
                                    }

                                    //else
                                    //{
                                    //    if (MessageBox.Show(AddMessage + "Do you want to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //    {
                                    //        _result = true;
                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                if ((decimal)oAppointment.Duration.TotalMinutes == OldDuration)
                                {
                                    if (MessageBox.Show(AddMessage + "The appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _result = true;
                                    }
                                }
                                else
                                    if (MessageBox.Show(AddMessage + "Do you want to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _result = true;
                                    }
                            }
                        }
                        else
                        {
                            if (MessageBox.Show(AddMessage + "Do you want to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _result = true;
                            }
                        }
                    }

                    else
                    { _isConflict = false; }
                }

                    // Recurrence Condition
                else if (AppointmentType == SingleRecurrence.Recurrence || AppointmentType == SingleRecurrence.SingleInRecurrence)
                {
                    if (_isConflict == false && _IsModified == false)
                    {
                        if (_ShowTemplateAppointment)
                        {
                            if (oTemplateAppointment != null)
                            {

                                if (IsTemplate == 0)
                                {
                                    AppointmentScheduleFlag ASTemplateAppointment = AppointmentScheduleFlag.None;
                                    ASTemplateAppointment = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oTemplateAppointment.Tag.ToString(), '~', 4));

                                    if (ASTemplateAppointment == AppointmentScheduleFlag.TemplateBlock)
                                    {
                                        if (oAppointment != null)
                                        {
                                            //if ((decimal)oAppointment.Duration.TotalMinutes == (decimal)oTemplateAppointment.Duration.TotalMinutes)
                                            //{
                                            if (CheckSameTemplateSlotType(oTemplateAppointment, oAppointment) == true)
                                            {
                                                if (MessageBox.Show(AddMessage + "Do you wish to book this appointment into the existing template slot?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {
                                                    return _result = true;

                                                }
                                                else
                                                {
                                                    return _result = false;
                                                }


                                            }
                                            //}
                                        }


                                        if (MessageBox.Show(AddMessage + "The recurring appointment will take on the appointment type and length of the new template slot. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            _result = true;
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show(AddMessage + "The recurring appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            _result = true;
                                        }
                                    }
                                }

                                    //Template==1
                                else
                                {
                                    DateTime dtStartDate = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedStartTime);
                                    DateTime dtEndDate = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedEndTime);

                                    TimeSpan tsDiff = dtEndDate - dtStartDate;




                                    // check any slot present with this oAppointment start time , end time and appointment type


                                    DataTable dtPasteAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2)));
                                    long pasteTemplateAppointmentTypeID = 0;
                                    int pasteTemplateAppointmentDuration = 0;
                                    if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                                    {
                                        pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                        pasteTemplateAppointmentDuration = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentDuration"].ToString());
                                        //   dtPasteAppointmentType.Dispose();
                                    }
                                    if (dtPasteAppointmentType != null)
                                    {
                                        dtPasteAppointmentType.Dispose();
                                        dtPasteAppointmentType = null;
                                    }


                                    // Get ALL SELECTED LOCATIONS


                                    string _sLastSelectedLocation = string.Empty;


                                    for (int i = 0; i < trvLocations.Nodes.Count; i++)
                                    {
                                        if (trvLocations.Nodes[i].Checked == true)
                                        {
                                            _sLastSelectedLocation += Convert.ToString(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                                        }
                                    }
                                    if (_sLastSelectedLocation.Trim() != "")
                                    {
                                        _sLastSelectedLocation = _sLastSelectedLocation.Remove(_sLastSelectedLocation.LastIndexOf(','));
                                    }



                                    if (CheckSlotPresent(oAppointment.StartTime, ProposedStartTime, ProposedEndTime, pasteTemplateAppointmentTypeID, ProviderID, _ClinicID, Convert.ToDateTime(oAppointment.StartTime.TimeOfDay.ToString()), Convert.ToDateTime(oAppointment.EndTime.TimeOfDay.ToString()), _sLastSelectedLocation, Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2))) == true)
                                    {
                                        // if ((decimal)oAppointment.Duration.TotalMinutes == (decimal)pasteTemplateAppointmentDuration)
                                        // {
                                        if (MessageBox.Show(AddMessage + "Do you wish to double book this time slot, placing the new appointment in the slot and retaining an open slot at this time?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            return _result = true;
                                        }
                                        else
                                        {
                                            return _result = false;
                                        }
                                        // }
                                    }
                                    //SLR :Check this on 4/22/2014
                                    //06-May-14 Aniket: Remove ; after if
                                    if (MessageBox.Show("This occurrence of the recurring appointment for " + _PatientName + " will be scheduled to the new time slot: " + "\n\n" + "New Time:  " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedStartTime).ToShortTimeString() + " - " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedEndTime).ToShortTimeString() + "  for: " + oMasterAppointment.ASBaseDescription + "  at: " + oMasterAppointment.LocationName + "\n" + "Previous:  " + oAppointment.Description + "  for: " + ProposedOwner + "  at: " + oMasterAppointment.LocationName + "\n\n" + "The rest of the appointments in this recurring series of appointments will remain unchanged.  Would you like to continue? " + "\n\n" + "NOTE:  If you want to change the time for all the appointments in the series, you can double click the appointment, choose to modify the series, and select “Recurrence” in the tool bar to change the appointment times.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _result = true;
                                    }


                                    //if (MessageBox.Show(AddMessage + "The recurring appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //{
                                    //    _result = true;
                                    //}

                                    //else
                                    //{
                                    //    if (MessageBox.Show(AddMessage + "Do you want to modify this recurring appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //    {
                                    //        _result = true;
                                    //    }
                                    //}
                                }
                            }
                            else
                            {//SLR: Check on 4/22/2014
                                //06-May-14 Aniket: Remove ; after if
                                if (MessageBox.Show("This occurrence of the recurring appointment for " + _PatientName + " will be scheduled to the new time slot: " + "\n\n" + "New Time:  " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedStartTime).ToShortTimeString() + " - " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedEndTime).ToShortTimeString() + "  for: " + oMasterAppointment.ASBaseDescription + "  at: " + oMasterAppointment.LocationName + "\n" + "Previous:  " + oAppointment.Description + "  for: " + ProposedOwner + "  at: " + oMasterAppointment.LocationName + "\n\n" + "The rest of the appointments in this recurring series of appointments will remain unchanged.  Would you like to continue? " + "\n\n" + "NOTE:  If you want to change the time for all the appointments in the series, you can double click the appointment, choose to modify the series, and select “Recurrence” in the tool bar to change the appointment times.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _result = true;
                                }


                                //if (MessageBox.Show(AddMessage + "The recurring appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //{
                                //    _result = true;
                                //}
                            }
                        }
                        else
                        {
                            //if (MessageBox.Show(AddMessage + "Do you want to modify this recurring appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{
                            //    _result = true;
                            //}
                            //SLR: Check 4/22/2014
                            //06-May-14 Aniket: Remove ; after if
                            if (MessageBox.Show("This occurrence of the recurring appointment for " + _PatientName + " will be scheduled to the new time slot: " + "\n\n" + "New Time:  " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedStartTime).ToShortTimeString() + " - " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, ProposedEndTime).ToShortTimeString() + "  for: " + oMasterAppointment.ASBaseDescription + "  at: " + oMasterAppointment.LocationName + "\n" + "Previous:  " + oAppointment.Description + "  for: " + ProposedOwner + "  at: " + oMasterAppointment.LocationName + "\n\n" + "The rest of the appointments in this recurring series of appointments will remain unchanged.  Would you like to continue? " + "\n\n" + "NOTE:  If you want to change the time for all the appointments in the series, you can double click the appointment, choose to modify the series, and select “Recurrence” in the tool bar to change the appointment times.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _result = true;
                            }
                        }
                    }
                    else
                    { _isConflict = false; }

                }

                // Verify for blocked provider 



                if (oMasterAppointment.Criteria.SingleCriteria.StartDate == 0 && oMasterAppointment.Criteria.SingleCriteria.EndDate == 0)
                {
                    oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oAppointment.StartTime.ToShortDateString()); //gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy"));
                    oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oAppointment.EndTime.ToShortDateString()); //gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy"));
                }
                
                gloAppointmentScheduling.Criteria.FindRecurrences dummyRecurrences = new gloAppointmentScheduling.Criteria.FindRecurrences();
                _AppointmentDates = dummyRecurrences.GetRecurrence(oMasterAppointment.Criteria, ProviderID, oAppointment.StartTime, oAppointment.EndTime);
                dummyRecurrences.Dispose();
                dummyRecurrences = null;

                //Added by Mukesh for Override Provider Block Schedule
                #region "Override Provider Block Schedule"


                oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                oClsgloUserRights.CheckForUserRights(_UserName);

                if (!oClsgloUserRights.OverrideProviderBlockSchedule)
                {
                    if (_AppointmentDates.Dates.Count <= 0 && _AppointmentDates.BlockedDates.Count > 0)
                    {
                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //_AppointmentDates.Dispose();
                        //oMasterAppointment.Dispose();
                        //oClsgloUserRights.Dispose();
                        return false;
                    }
                }
                else if (AppointmentType == SingleRecurrence.Single)
                {
                    if (oClsgloUserRights.OverrideProviderBlockSchedule && _AppointmentDates.Dates.Count <= 0 && _AppointmentDates.BlockedDates.Count > 0)
                    {
                        Int64 _OwnerId = 0;
                        _OwnerId = Convert.ToInt64(GetTagElement(oAppointment.Owner.ToString(), '~', 1));
                        string Location = GetTagElement(oAppointment.Owner.ToString(), '~', 6).ToString();
                        DataTable dt = ogloAppointment.ResourseName(_OwnerId, oAppointment.StartTime, oAppointment.EndTime, oAppointment.StartTime, Location);
                        if (dt.Rows.Count >= 1 && dt != null)
                        {
                            if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oAppointment.StartTime.ToShortTimeString() + " - " + oAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                for (int i = _AppointmentDates.BlockedDates.Count - 1; i >= 0; i--)
                                {
                                    _AppointmentDates.Dates.Add(_AppointmentDates.BlockedDates[i]);
                                    _AppointmentDates.BlockedDates.RemoveAt(i);
                                }
                                //_AppointmentDates.Dispose();
                                //oMasterAppointment.Dispose();
                                //oClsgloUserRights.Dispose();
                                dt.Dispose();
                                dt = null;
                                return true;
                            }
                            else
                            {
                                //_AppointmentDates.Dispose();
                                //oMasterAppointment.Dispose();
                                //oClsgloUserRights.Dispose();
                                dt.Dispose();
                                dt = null;
                                return false;
                            }
                        }
                        else
                        {
                            //_AppointmentDates.Dispose();
                            //oMasterAppointment.Dispose();
                            //oClsgloUserRights.Dispose();
                            if (dt != null)
                            {
                                dt.Dispose();
                                dt = null;
                            }
                            return false;
                        }
                        
                    }
                }

                if (AppointmentType == SingleRecurrence.Single)
                {
                    if (oMasterAppointment.Resources.Count > 0)
                    {

                        for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                        {

                            Int64 OwnerID = Convert.ToInt64(GetTagElement(oAppointment.Owner.ToString(), '~', 1));


                            //if (_AppointmentDates.RemoveResourceBlockedSlots(oMasterAppointment.Resources[i].ASCommonID, oAppointment.StartTime, oAppointment.EndTime, oMasterAppointment.StartDate))
                            if (_AppointmentDates.RemoveResourceBlockedSlots(OwnerID, oAppointment.StartTime, oAppointment.EndTime, oAppointment.StartTime))
                            {

                                if (!oClsgloUserRights.OverrideProviderBlockSchedule)
                                {
                                    MessageBox.Show("Schedule is blocked for the resource. Appointment cannot be saved. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //_AppointmentDates.Dispose();
                                    //oMasterAppointment.Dispose();
                                    //oClsgloUserRights.Dispose();
                                    return false;
                                }
                                else
                                {
                                    //dt = ogloAppointment.ResourseName(oMasterAppointment.Resources[i].ASCommonID, oAppointment.StartTime, oAppointment.EndTime, oMasterAppointment.StartDate);
                                    string Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                                    DataTable dt = ogloAppointment.ResourseName(OwnerID, oAppointment.StartTime, oAppointment.EndTime, oAppointment.StartTime, Location);
                                    if (dt.Rows.Count >= 1 && dt != null)
                                    {
                                        //if (DialogResult.Yes == MessageBox.Show("Schedule is blocked for the resource. Do you want to save the changes? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                        if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oAppointment.StartTime.ToShortTimeString() + " - " + oAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {
                                            //_AppointmentDates.Dispose();
                                            //oMasterAppointment.Dispose();
                                            //oClsgloUserRights.Dispose();
                                            if (dt != null)
                                            {
                                                dt.Dispose();
                                                dt = null;
                                            }
                                            return true;
                                        }
                                        else
                                        {
                                            //_AppointmentDates.Dispose();
                                            //oMasterAppointment.Dispose();
                                            //oClsgloUserRights.Dispose();
                                            if (dt != null)
                                            {
                                                dt.Dispose();
                                                dt = null;
                                            }
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //_AppointmentDates.Dispose();
                                        //oMasterAppointment.Dispose();
                                        //oClsgloUserRights.Dispose();
                                        if (dt != null)
                                        {
                                            dt.Dispose();
                                            dt = null;
                                        }
                                        return false;
                                    }
                                    
                                }

                            }
                        }
                    }
                }

                //oClsgloUserRights.Dispose();
                #endregion



                //_AppointmentDates.Dispose();
                //oMasterAppointment.Dispose();

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
                _IsModified = false;
            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                //if (ogloPatient != null)
                //{
                //    ogloPatient.Dispose();
                //    ogloPatient = null;
                //}
                if (oMasterAppointment != null)
                {
                    oMasterAppointment.Dispose();
                    oMasterAppointment = null;
                }
                if (oTemplateAppointment != null)
                {
                   
                    oTemplateAppointment = null;
                }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
                if (_AppointmentDates != null)
                {
                    _AppointmentDates.Dispose();
                    _AppointmentDates = null;
                }
            }
            return _result;
        }

        private bool CheckSlotPresent(DateTime startDate,int startTime, int endTime,long appointmentTypeID,long providerId,long clinicID,DateTime dStartTime,DateTime dEndTime, string locations,long appointmentID)
        {


            //object ResultCount = GetAppointmentConflictTimeOverlapSetTemplate(appointmentID, providerId, gloDateMaster.gloDate.DateAsNumber(startTime.ToShortDateString()), gloDateMaster.gloTime.TimeAsNumber(startTime.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(endTime.TimeOfDay.ToString()),appointmentTypeID, _ClinicID, locations);
            object ResultCount = GetAppointmentConflictTimeOverlapSetTemplate(appointmentID, providerId, gloDateMaster.gloDate.DateAsNumber(startDate.ToShortDateString()), startTime, endTime, appointmentTypeID, _ClinicID, Convert.ToDateTime(startDate.ToShortDateString()), Convert.ToDateTime(dStartTime.ToShortTimeString()), Convert.ToDateTime(dEndTime.ToShortTimeString()), locations);
            if (Convert.ToInt16(ResultCount) > 0)
                return true;
            else
                return false;
        }




        private Boolean CheckSameTemplateSlotType(ScheduleAppointment oTemplateAppointment, ScheduleAppointment oAppointment)
        {
          
            int pasteTemplateAppointmentType = 0;

            long appointmentTypeID = 0;
            long pasteTemplateAppointmentTypeID = 0;

            int appointmentTypeFlag = 0;
            int pasteTemplateAppointmentTypeFlag = 0;

            string appointmentName = "";
            string pasteTemplateAppointmentName = "";

            // Get Appointment Type Details for Cut OR Copy Appointment 
            DataTable dtAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(Convert.ToInt64(GetTagElement(oTemplateAppointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oTemplateAppointment.Tag.ToString(), '~', 2)));
            if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 0)
            {
               // appointmentType = Convert.ToInt16(dtAppointmentType.Rows[0]["TemplateAppointment"].ToString());
                appointmentTypeID = Convert.ToInt64(dtAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                appointmentTypeFlag = Convert.ToInt16(dtAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                appointmentName = dtAppointmentType.Rows[0]["AppointmentName"].ToString();
                //dtAppointmentType.Dispose();
            }
            if (dtAppointmentType != null)
            {
                dtAppointmentType.Dispose();
                dtAppointmentType = null;
            }

            pasteTemplateAppointmentType = Convert.ToInt16(Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 4)));
           
                DataTable dtPasteAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oAppointment.Tag.ToString(), '~', 2)));
                if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                {
                    pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                    pasteTemplateAppointmentTypeFlag = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                    pasteTemplateAppointmentName = dtPasteAppointmentType.Rows[0]["AppointmentName"].ToString();
          //          dtPasteAppointmentType.Dispose();
                }
                if (dtPasteAppointmentType != null)
                {
                    dtPasteAppointmentType.Dispose();
                    dtPasteAppointmentType = null;
                }

            if ((string.IsNullOrEmpty(appointmentName)) && (string.IsNullOrEmpty(pasteTemplateAppointmentName)))
                return false;

            if (appointmentName != pasteTemplateAppointmentName)
                return false;       



            return true;
        }








        private void UpdateSingleAppointment(ShortApointmentSchedule oShortAppointment, MasterAppointment omstAppointment)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            int retValue = 0;

            try
            {
                oDB.Connect(false);
                if (oShortAppointment != null)
                {
                    //1.First Update the Master Table entry
                    _sqlQuery = " UPDATE AS_Appointment_MST SET "
                               + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                               + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                               + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                               + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                               + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                               + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                               + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                               + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " "
                               + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + "  AND  nClinicID = " + this._ClinicID + "  ";

                    retValue = oDB.Execute_Query(_sqlQuery);

                    //2.Check if the Mater entry is modified ,If yes Update the Detail Table entry for provider
                    if (retValue > 0)
                    {
                        if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                                 + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                                 + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                                 + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                                 + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                                 + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                                 + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                                 + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                                 + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                                 + " nUsedStatus= " + oShortAppointment.UsedStatus.GetHashCode() + " "
                                 + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                 + " AND  nClinicID = " + this._ClinicID + " ";

                        }
                        else
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                                       + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                                       + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                                       + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                                       + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                                       + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                                       + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                                       + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                                       + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " "
                                       + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                       + " AND  nClinicID = " + this._ClinicID + " ";
                        }

                        retValue = oDB.Execute_Query(_sqlQuery);

                        //Update the Detail Table entry for resource and problem type
                        if (retValue > 0)
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                            + " dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                            + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                            + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                            + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " "
                            + " WHERE nMSTAppointmentID =  " + oShortAppointment.MasterID + " AND (nRefID = " + oShortAppointment.DetailID + " AND nRefFlag <> 0) "
                            + " AND  nClinicID = " + this._ClinicID + " ";

                            oDB.Execute_Query(_sqlQuery);

                            //_IsUpdated = true;

                            #region "Generate HL7 Message Queue for Modify Appointment"
                            // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                            if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                            {

                                if (oShortAppointment.DetailID > 0)
                                {
                                    string _strSQL = "";
                                    long _nPatientID = 0;
                                    Object result = null;
                                    _strSQL = "SELECT nPatientID from AS_Appointment_MST where nMSTAppointmentID = " + oShortAppointment.MasterID;
                                    result = oDB.ExecuteScalar_Query(_strSQL);
                                    if (result != null)
                                    { _nPatientID = Convert.ToInt64(result); }

                                    gloHL7.InsertInMessageQueue("S13", _nPatientID, oShortAppointment.MasterID, "", _databaseconnectionstring);
                                }
                            }
                            // Code End - Added by kanchan on 20091205 for HL7 appointment outbound
                            #endregion
                        }

                        gloDatabaseLayer.DBLayer oDBUpdate = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        try
                        {
                            oDBUpdate.Connect(false);

                            String _strSQL = "UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered=0 from AS_Appointment_DTL join AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID where  AB_AppointmentTemplate_Allocation.nTemplateAllocationID=AS_Appointment_DTL.nTemplateAllocationID AND AS_Appointment_DTL.nDTLAppointmentID=" + oShortAppointment.DetailID;
                            oDBUpdate.Execute_Query(_strSQL);
                            oDBUpdate.Execute_Query("UPDATE AS_Appointment_DTL SET  AS_Appointment_DTL.nTemplateAllocationID=" + 0 + ",AS_Appointment_DTL.nTemplateAllocationMasterID=" + 0 + " WHERE nDTLAppointmentID=" + oShortAppointment.DetailID);
                            oDBUpdate.Disconnect();
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
                            if (oDBUpdate != null) { oDBUpdate.Dispose(); }
                        }
                        if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            gloDatabaseLayer.DBLayer oDBUpdateTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            try
                            {

                                oDBUpdateTrackingStatus.Connect(false);

                                String _strSQL = "UPDATE PatientTracking SET nTrackingStatus= " + omstAppointment.AppointmentStatusID + " "
                                    + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                    + " AND  nClinicID = " + this._ClinicID + " ";
                                oDBUpdateTrackingStatus.Execute_Query(_strSQL);

                                oDBUpdateTrackingStatus.Disconnect();
                                //}
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                ex = null;
                            }
                            finally
                            {
                                if (oDBUpdateTrackingStatus != null) { oDBUpdateTrackingStatus.Dispose(); }
                            }

                        }

                    }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment Modified", _PatientD, oShortAppointment.MasterID, oShortAppointment.ASCommonID, ActivityOutCome.Success);
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                //_IsUpdated = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //_IsUpdated = false;
            }
            finally
            {
                if (oDB.Connect(false))
                { oDB.Disconnect(); }
                if (oDB != null)
                { oDB.Dispose(); }
            }
        }

        //private void UpdateSingleResourceAppointment(ShortApointmentSchedule oShortAppointment, ShortApointmentSchedule oShortResource)
        private void UpdateSingleResourceAppointment(ShortApointmentSchedule oShortAppointment, ShortApointmentSchedule oShortResource, MasterAppointment omstAppointment)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            //bool _IsUpdated = false;
            int retValue = 0;

            try
            {
                oDB.Connect(false);
                if (oShortAppointment != null)
                {
                    //1.First Update the Master Table entry
                    _sqlQuery = " UPDATE AS_Appointment_MST SET "
                               + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                               + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                               + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                               + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                               + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                               + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                               + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                               + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " "
                               + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + "  AND  nClinicID = " + this._ClinicID + "  ";

                    retValue = oDB.Execute_Query(_sqlQuery);

                    //Resolved : Bug #89094 if another user updates resource only appt. then in this case last user updated in audit logs
                    //2.Check if the Mater entry is modified ,If yes Update the Detail Table entry for provider
                    if (retValue > 0)
                    {
                        if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                                 + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                                 + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                                 + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                                 + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                                 + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                                 + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                                 + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                                 + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                                 + " nUsedStatus= " + oShortAppointment.UsedStatus.GetHashCode() + " ,"
                                 + " nUserID = " + _UserID + " ,"
                                 + " nTemplateAllocationID = 0 , "
                                 + " nTemplateAllocationMasterID = 0 , " 
                                 + " dtUpdatedDateTime = '" + System.DateTime.Now.ToString () + "' "
                                 + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                 + " AND  nClinicID = " + this._ClinicID + " ";
                        }
                        else
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                                       + " nASBaseID = " + oShortAppointment.ASCommonID + " , "
                                       + " sASBaseCode= '" + oShortAppointment.ASCommonCode + "' ,"
                                       + " sASBaseDesc= '" + oShortAppointment.ASCommonDescription + "' ,"
                                       + " nASBaseFlag= " + oShortAppointment.ASCommonFlag.GetHashCode() + " ,"
                                       + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                                       + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                                       + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                                       + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                                       + " nUserID = " + _UserID + " ,"
                                       + " nTemplateAllocationID = 0 , " 
                                       + " nTemplateAllocationMasterID = 0 , " 
                                       + " dtUpdatedDateTime = '" + System.DateTime.Now.ToString() + "' "
                                       + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                       + " AND  nClinicID = " + this._ClinicID + " ";
                        }

                        retValue = oDB.Execute_Query(_sqlQuery);

                        //Update the Detail Table entry for resource and problem type
                        if (retValue > 0)
                        {
                            _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                            + " nASBaseID = " + oShortResource.ASCommonID + " , "
                            + " sASBaseCode= '" + oShortResource.ASCommonCode + "' ,"
                            + " sASBaseDesc= '" + oShortResource.ASCommonDescription + "' ,"
                            + " nASBaseFlag= " + oShortResource.ASCommonFlag.GetHashCode() + " ,"
                            + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortResource.StartDate.ToString()) + " ,"
                            + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortResource.StartTime.ToString()) + " ,"
                            + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortResource.EndDate.ToString()) + " ,"
                            + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortResource.EndTime.ToString()) + " ,"
                            + " nUserID = " + _UserID + " ,"
                            + " nTemplateAllocationID = 0 , "
                            + " nTemplateAllocationMasterID = 0 , " 
                            + " dtUpdatedDateTime = '" + System.DateTime.Now.ToString() + "' "
                            + " WHERE nMSTAppointmentID =  " + oShortAppointment.MasterID + " AND (nRefID = " + oShortResource.DetailID + " AND nRefFlag = " + ASBaseType.Resource.GetHashCode() + ") "
                            + " AND  nClinicID = " + this._ClinicID + " ";

                            oDB.Execute_Query(_sqlQuery);


                            //_sqlQuery = " UPDATE AS_Appointment_DTL SET "
                            //+ " dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                            //+ " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                            //+ " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                            //+ " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                            //+ " nUserID = " + _UserID + " ,"
                            //+ " dtUpdatedDateTime = '" + System.DateTime.Now.ToString() + "' "
                            //+ " WHERE nMSTAppointmentID =  " + oShortAppointment.MasterID + " AND (nRefID = " + oShortAppointment.DetailID + " AND nRefFlag = " + ASBaseType.ProblemType.GetHashCode() + ") "
                            //+ " AND  nClinicID = " + this._ClinicID + " ";

                            //_IsUpdated = true;

                            #region "Generate HL7 Message Queue for Modify Appointment"
                            // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                            if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                            {
                                if (oShortAppointment.DetailID > 0)
                                {
                                    string _strSQL = "";
                                    long _nPatientID = 0;
                                    Object result = null;
                                    _strSQL = "SELECT nPatientID from AS_Appointment_MST  WITH(NOLOCK) where nMSTAppointmentID = " + oShortAppointment.MasterID;
                                    result = oDB.ExecuteScalar_Query(_strSQL);
                                    if (result != null)
                                    { _nPatientID = Convert.ToInt64(result); }

                                    gloHL7.InsertInMessageQueue("S13", _nPatientID, oShortAppointment.MasterID, "", _databaseconnectionstring);
                                }
                            }
                            // Code End - Added by kanchan on 20091205 for HL7 appointment outbound
                            #endregion
                        }

                        gloDatabaseLayer.DBLayer oDBUpdate = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        try
                        {
                            oDBUpdate.Connect(false);

                            String _strSQL = "UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered=0 from AS_Appointment_DTL join AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID where  AB_AppointmentTemplate_Allocation.nTemplateAllocationID=AS_Appointment_DTL.nTemplateAllocationID AND AS_Appointment_DTL.nDTLAppointmentID=" + oShortAppointment.DetailID;
                            oDBUpdate.Execute_Query(_strSQL);
                            //oDBUpdate.Execute_Query("UPDATE AS_Appointment_DTL SET  AS_Appointment_DTL.nTemplateAllocationID=" + 0 + ",AS_Appointment_DTL.nTemplateAllocationMasterID=" + 0 + " WHERE nDTLAppointmentID=" + oShortAppointment.DetailID);
                            oDBUpdate.Disconnect();
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
                            if (oDBUpdate != null) { oDBUpdate.Dispose(); }
                        }

                        if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {

                            gloDatabaseLayer.DBLayer oDBUpdateTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            try
                            {
                                oDBUpdateTrackingStatus.Connect(false);

                                String _strSQL = "UPDATE PatientTracking SET nTrackingStatus= " + omstAppointment.AppointmentStatusID + " "
                                    + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                                    + " AND  nClinicID = " + this._ClinicID + " ";
                                oDBUpdateTrackingStatus.Execute_Query(_strSQL);
                                oDBUpdateTrackingStatus.Disconnect();
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                ex = null;
                            }
                            finally
                            {
                                if (oDBUpdateTrackingStatus != null) { oDBUpdateTrackingStatus.Dispose(); }
                            }
                        }
                    }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment Modified", _PatientD , oShortAppointment.MasterID, oShortAppointment.ASCommonID, ActivityOutCome.Success);
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                //_IsUpdated = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //_IsUpdated = false;
            }
            finally
            {
                if (oDB.Connect(false))
                { oDB.Disconnect(); }
                if (oDB != null)
                { oDB.Dispose(); }
            }
        }

        //In Progress (With Problem /Resource Time Modification)
        //private void UpdateSingleAppointment_New(ShortApointmentSchedule oShortAppointment)
        //{

        //    gloAppointmentScheduling.gloAppointment ogloAppointment = null;
        //    gloAppointmentScheduling.MasterAppointment oMasterAppointment = null;
        //    Int64 _retValue = 0;
        //    object _CheckValue = new object();

        //    try
        //    {
        //        if (oShortAppointment != null)
        //        {
        //            ogloAppointment = new gloAppointment(_databaseconnectionstring);
        //            oMasterAppointment = new MasterAppointment();
        //            oMasterAppointment = ogloAppointment.GetMasterAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);

        //            if (oMasterAppointment != null)
        //            {
        //                #region " Ask User for Modify "

        //                frmModifyScheduleCriteria ofrmModifyAppointmentCriteria = new frmModifyScheduleCriteria();
        //                ofrmModifyAppointmentCriteria.ProblemTypes = oMasterAppointment.ProblemTypes;
        //                ofrmModifyAppointmentCriteria.Resources = oMasterAppointment.Resources;
        //                ofrmModifyAppointmentCriteria.NewStartTime = oShortAppointment.StartTime;
        //                ofrmModifyAppointmentCriteria.NewEndTime = oShortAppointment.EndTime;
        //                ofrmModifyAppointmentCriteria.MessageText = "Do you want to modify this Appointment ?";
        //                DialogResult _dialogResult = ofrmModifyAppointmentCriteria.ShowDialog();

        //                if (_dialogResult != DialogResult.OK)
        //                {
        //                    return;
        //                }

        //                oMasterAppointment.ProblemTypes = ofrmModifyAppointmentCriteria.ProblemTypes;
        //                oMasterAppointment.Resources = ofrmModifyAppointmentCriteria.Resources;

        //                ofrmModifyAppointmentCriteria.Dispose();
        //                #endregion

        //                //Make new single entry for the appointment

        //                #region "Master Appointment Data"

        //                //if Modify Appointment and 

        //                oMasterAppointment.MasterID = 0;
        //                oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
        //                oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;

        //                _CheckValue = oMasterAppointment.AppointmentTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
        //                _CheckValue = oMasterAppointment.AppointmentTypeCode;// = cmbApp_AppointmentType.Text;//Remark
        //                _CheckValue = oMasterAppointment.AppointmentTypeDesc;// = cmbApp_AppointmentType.Text;

        //                oMasterAppointment.ASBaseID = oShortAppointment.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
        //                oMasterAppointment.ASBaseCode = oShortAppointment.ASCommonCode;  //cmbApp_Provider.Text; //Remark
        //                oMasterAppointment.ASBaseDescription = oShortAppointment.ASCommonDescription; // cmbApp_Provider.Text;
        //                oMasterAppointment.ASBaseFlag = ASBaseType.Provider;

        //                _CheckValue = oMasterAppointment.ReferralProviderID;// = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
        //                _CheckValue = oMasterAppointment.ReferralProviderCode; // = cmbApp_ReferralDoctor.Text; //Remark
        //                _CheckValue = oMasterAppointment.ReferralProviderName; //= cmbApp_ReferralDoctor.Text;

        //                _CheckValue = oMasterAppointment.PatientID; //  = Convert.ToInt64(txtApp_Patient.Tag.ToString());


        //                oMasterAppointment.StartDate = oShortAppointment.StartDate;
        //                oMasterAppointment.StartTime = oShortAppointment.StartTime;
        //                oMasterAppointment.EndDate = oShortAppointment.EndDate;
        //                oMasterAppointment.EndTime = oShortAppointment.EndTime;
        //                oMasterAppointment.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;
        //                _CheckValue = oMasterAppointment.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

        //                _CheckValue = oMasterAppointment.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
        //                _CheckValue = oMasterAppointment.LocationName; // = cmbApp_Location.Text;

        //                _CheckValue = oMasterAppointment.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
        //                _CheckValue = oMasterAppointment.DepartmentName; // = cmbApp_Department.Text;

        //                _CheckValue = oMasterAppointment.Notes; // = txtApp_Notes.Text;
        //                _CheckValue = oMasterAppointment.ClinicID; // = _SetAppointmentParameter.ClinicID;
        //                oMasterAppointment.UsedStatus = ASUsedStatus.NotUsed;

        //                #endregion

        //                #region "Appointment Criteria"

        //                oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

        //                oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString("MM/dd/yyyy"));
        //                oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString("hh:mm tt"));
        //                oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString("MM/dd/yyyy"));
        //                oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString("hh:mm tt"));
        //                oMasterAppointment.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;

        //                #endregion

        //                #region "Problem Types"

        //                for (int i = 0; i < oMasterAppointment.ProblemTypes.Count; i++)
        //                {

        //                    oMasterAppointment.ProblemTypes[i].MasterID = 0;
        //                    oMasterAppointment.ProblemTypes[i].DetailID = 0;
        //                    oMasterAppointment.ProblemTypes[i].IsRecurrence = oMasterAppointment.IsRecurrence;
        //                    oMasterAppointment.ProblemTypes[i].PatientID = oMasterAppointment.PatientID;
        //                    oMasterAppointment.ProblemTypes[i].LineNo = 0;
        //                    oMasterAppointment.ProblemTypes[i].ASFlag = oMasterAppointment.ASFlag;
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
        //                    oMasterAppointment.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

        //                    oMasterAppointment.ProblemTypes[i].StartDate = oShortAppointment.StartDate;
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].StartTime; //oMasterAppointment.ProblemTypes[i].StartTime = oShortAppointment.StartTime;
        //                    oMasterAppointment.ProblemTypes[i].EndDate = oShortAppointment.EndDate;
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].EndTime; //oMasterAppointment.ProblemTypes[i].EndTime = oShortAppointment.EndTime;
        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

        //                    _CheckValue = oMasterAppointment.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
        //                    oMasterAppointment.ProblemTypes[i].ViewOtherDetails = "";
        //                    oMasterAppointment.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
        //                }

        //                #endregion

        //                #region "Resources"

        //                for (int i = 0; i < oMasterAppointment.Resources.Count; i++)
        //                {
        //                    oMasterAppointment.Resources[i].MasterID = 0;
        //                    oMasterAppointment.Resources[i].DetailID = 0;
        //                    oMasterAppointment.Resources[i].IsRecurrence = oMasterAppointment.IsRecurrence;
        //                    oMasterAppointment.Resources[i].PatientID = oMasterAppointment.PatientID;
        //                    oMasterAppointment.Resources[i].LineNo = 0;
        //                    oMasterAppointment.Resources[i].ASFlag = oMasterAppointment.ASFlag;
        //                    _CheckValue = oMasterAppointment.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
        //                    _CheckValue = oMasterAppointment.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
        //                    _CheckValue = oMasterAppointment.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
        //                    oMasterAppointment.Resources[i].ASCommonFlag = ASBaseType.Resource;

        //                    oMasterAppointment.Resources[i].StartDate = oShortAppointment.StartDate;
        //                    _CheckValue = oMasterAppointment.Resources[i].StartTime; //oMasterAppointment.Resources[i].StartTime = oShortAppointment.StartTime;
        //                    oMasterAppointment.Resources[i].EndDate = oShortAppointment.EndDate;
        //                    _CheckValue = oMasterAppointment.Resources[i].EndTime; //oMasterAppointment.Resources[i].EndTime = oShortAppointment.EndTime;
        //                    _CheckValue = oMasterAppointment.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

        //                    _CheckValue = oMasterAppointment.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
        //                    oMasterAppointment.Resources[i].ViewOtherDetails = "";
        //                    oMasterAppointment.Resources[i].UsedStatus = ASUsedStatus.NotUsed;

        //                }

        //                #endregion

        //                oMasterAppointment.Insurances = null;

        //                _retValue = ogloAppointment.Add(oMasterAppointment);

        //                //Delete the Reccurance entry of the appointment

        //                if (_retValue > 0)
        //                {
        //                    bool _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, true);
        //                }

        //            }

        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //    finally
        //    {
        //        if (oMasterAppointment != null) { oMasterAppointment.Dispose(); }
        //        if (_CheckValue != null) { _CheckValue = null; }
        //        if (oShortAppointment != null) { oShortAppointment.Dispose(); }
        //    }
        //}

        private void UpdateReccurance(ShortApointmentSchedule oShortAppointment)
        {
            // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
            // bool _IsUpdated = false;
            int retValue = 0;

            try
            {
                oDB.Connect(false); // Added by kanchan on 20091231 for HL7 appointment outbound
                if (oShortAppointment != null)
                {
                    //1.Update the Detail Table entry for provider
                    _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                               + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                               + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                               + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                               + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                               + " bIsSingleRecurrence = " + SingleRecurrence.SingleInRecurrence.GetHashCode() + " "
                               + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND nDTLAppointmentID = " + oShortAppointment.DetailID + " "
                               + " AND  nClinicID = " + this._ClinicID + " ";

                    retValue = oDB.Execute_Query(_sqlQuery);

                    //1.Update the Detail Table entry for resource and problem types
                    if (retValue > 0)
                    {
                        _sqlQuery = " UPDATE AS_Appointment_DTL SET "
                               + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString()) + " ,"
                               + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString()) + " ,"
                               + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString()) + " ,"
                               + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString()) + " ,"
                               + " bIsSingleRecurrence = " + SingleRecurrence.SingleInRecurrence.GetHashCode() + " "
                               + " WHERE nMSTAppointmentID = " + oShortAppointment.MasterID + " AND (nRefID = " + oShortAppointment.DetailID + " AND nRefFlag <> 0) "
                               + " AND  nClinicID = " + this._ClinicID + " ";

                        retValue = oDB.Execute_Query(_sqlQuery);

                        //_IsUpdated = true; 
                    }

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment Modified", _PatientD, oShortAppointment.MasterID, oShortAppointment.ASCommonID, ActivityOutCome.Success);

                    #region "Generate HL7 Message Queue for Modify Appointment"
                    // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
                    if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                    {
                        if (oShortAppointment.DetailID > 0)
                        {
                            string _strSQL = "";
                            long _nPatientID = 0;
                            Object result = null;
                            _strSQL = "SELECT nPatientID from AS_Appointment_MST  WITH(NOLOCK) where nMSTAppointmentID = " + oShortAppointment.MasterID;
                            result = oDB.ExecuteScalar_Query(_strSQL);
                            if (Convert.ToString(result) != "")
                            { _nPatientID = Convert.ToInt64(result); }

                            gloHL7.InsertInMessageQueue("S13", _nPatientID, oShortAppointment.MasterID, "", _databaseconnectionstring);
                        }
                    }
                    // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
                    #endregion
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                //_IsUpdated = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                // _IsUpdated = false;
            }
            finally
            {
                if (oDB.Connect(false))
                { oDB.Disconnect(); }
                if (oDB != null)
                { oDB.Dispose(); }
            }
        }

        private void UpdateSingleInReccurance(ShortApointmentSchedule oShortAppointment)
        {
            // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //  string _sqlQuery = "";
            // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
            gloAppointmentScheduling.gloAppointment ogloAppointment = null;
            gloAppointmentScheduling.MasterAppointment oMasterAppointment = null;
            Int64 _retValue = 0;
            object _CheckValue = null;

            try
            {
                oDB.Connect(false); // Added by kanchan on 20091231 for HL7 appointment outbound
                if (oShortAppointment != null)
                {
                    ogloAppointment = new gloAppointment(_databaseconnectionstring);
                    //oMasterAppointment = new MasterAppointment();
                    oMasterAppointment = ogloAppointment.GetMasterAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);

                    if (oMasterAppointment != null)
                    {
                        //Make new single entry for the appointment

                        #region "Master Appointment Data"

                        //if Modify Appointment and 

                        oMasterAppointment.MasterID = 0;
                        oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
                        oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;

                        _CheckValue = oMasterAppointment.AppointmentTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterAppointment.AppointmentTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterAppointment.AppointmentTypeDesc;// = cmbApp_AppointmentType.Text;

                        oMasterAppointment.ASBaseID = oShortAppointment.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        oMasterAppointment.ASBaseCode = oShortAppointment.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        oMasterAppointment.ASBaseDescription = oShortAppointment.ASCommonDescription; // cmbApp_Provider.Text;
                        oMasterAppointment.ASBaseFlag = ASBaseType.Provider;

                        _CheckValue = oMasterAppointment.ReferralProviderID;// = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        _CheckValue = oMasterAppointment.ReferralProviderCode; // = cmbApp_ReferralDoctor.Text; //Remark
                        _CheckValue = oMasterAppointment.ReferralProviderName; //= cmbApp_ReferralDoctor.Text;

                        _CheckValue = oMasterAppointment.PatientID; //  = Convert.ToInt64(txtApp_Patient.Tag.ToString());


                        oMasterAppointment.StartDate = oShortAppointment.StartDate;
                        oMasterAppointment.StartTime = oShortAppointment.StartTime;
                        oMasterAppointment.EndDate = oShortAppointment.EndDate;
                        oMasterAppointment.EndTime = oShortAppointment.EndTime;
                        oMasterAppointment.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;
                        _CheckValue = oMasterAppointment.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                        _CheckValue = oMasterAppointment.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterAppointment.LocationName; // = cmbApp_Location.Text;

                        _CheckValue = oMasterAppointment.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterAppointment.DepartmentName; // = cmbApp_Department.Text;

                        _CheckValue = oMasterAppointment.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterAppointment.ClinicID; // = _SetAppointmentParameter.ClinicID;
                        oMasterAppointment.UsedStatus = ASUsedStatus.NotUsed;

                        #endregion

                        #region "Appointment Criteria"

                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterAppointment.ProblemTypes.Count; i++)
                        {

                            oMasterAppointment.ProblemTypes[i].MasterID = 0;
                            oMasterAppointment.ProblemTypes[i].DetailID = 0;
                            oMasterAppointment.ProblemTypes[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.ProblemTypes[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.ProblemTypes[i].LineNo = 0;
                            oMasterAppointment.ProblemTypes[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterAppointment.ProblemTypes[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.ProblemTypes[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.ProblemTypes[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.ProblemTypes[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.ProblemTypes[i].ViewOtherDetails = "";
                            oMasterAppointment.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                        }

                        #endregion

                        #region "Resources"

                        for (int i = 0; i < oMasterAppointment.Resources.Count; i++)
                        {
                            oMasterAppointment.Resources[i].MasterID = 0;
                            oMasterAppointment.Resources[i].DetailID = 0;
                            oMasterAppointment.Resources[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.Resources[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.Resources[i].LineNo = 0;
                            oMasterAppointment.Resources[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.Resources[i].ASCommonFlag = ASBaseType.Resource;

                            oMasterAppointment.Resources[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.Resources[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.Resources[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.Resources[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.Resources[i].ViewOtherDetails = "";
                            oMasterAppointment.Resources[i].UsedStatus = ASUsedStatus.NotUsed;

                        }

                        #endregion

                        oMasterAppointment.Insurances = null;

                        #region "Generate HL7 Message Queue for Modify Appointment"
                        // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
                        if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                        {
                            if (oShortAppointment.DetailID > 0)
                            {
                                string _strSQL = "";
                                long _nPatientID = 0;
                                Object result = null;
                                _strSQL = "SELECT nPatientID from AS_Appointment_MST  WITH(NOLOCK) where nMSTAppointmentID = " + oShortAppointment.MasterID;
                                result = oDB.ExecuteScalar_Query(_strSQL);
                                if (Convert.ToString(result) != "")
                                { _nPatientID = Convert.ToInt64(result); }

                                gloHL7.InsertInMessageQueue("S15", _nPatientID, oShortAppointment.MasterID, "", _databaseconnectionstring);
                            }
                        }
                        // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
                        #endregion

                        _retValue = ogloAppointment.Add(oMasterAppointment);

                        //Delete the Reccurance entry of the appointment

                        if (_retValue > 0)
                        {
                            bool _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, false);
                        }

                        //Audit Trail
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment Modified", _PatientD , oShortAppointment.MasterID, oShortAppointment.ASCommonID, ActivityOutCome.Success);
                    }

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oMasterAppointment != null) { oMasterAppointment.Dispose(); oMasterAppointment = null; }
                if (_CheckValue != null) { _CheckValue = null; }
                if (ogloAppointment != null) { ogloAppointment.Dispose(); ogloAppointment = null; }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }
        private void UpdateSingleInReccurance(ShortApointmentSchedule oShortAppointment, Int64 TemplateAllocationMasterID, Int64 TemplateAllocationID, Int64 LineNumber)
        {

            gloAppointmentScheduling.gloAppointment ogloAppointment = null;
            gloAppointmentScheduling.MasterAppointment oMasterAppointment = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = null;
            Int64 _retValue = 0;
            object _CheckValue = null;

            try
            {
                if (oShortAppointment != null)
                {
                    ogloAppointment = new gloAppointment(_databaseconnectionstring);
                   // oMasterAppointment = new MasterAppointment();
                    oMasterAppointment = ogloAppointment.GetMasterAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);



                    if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oMasterAppointment.UsedStatus == ASUsedStatus.CheckIn)
                    {

                        if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked in."
                                 + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oMasterAppointment.UsedStatus == ASUsedStatus.CheckOut)
                    {

                        if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked out."
                                 + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else
                        {
                            return;
                        }
                    }




                    if (oMasterAppointment != null)
                    {
                        //Checking Block timing 
                        //

                        gloAppointmentScheduling.Criteria.FindRecurrences dummyFindRecurrence = new gloAppointmentScheduling.Criteria.FindRecurrences();
                        _AppointmentDates = dummyFindRecurrence.GetRecurrence(oMasterAppointment.Criteria, oShortAppointment.ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.MasterID, oMasterAppointment.ResourceIDS);
                        dummyFindRecurrence.Dispose();
                        dummyFindRecurrence = null;

                     

                        oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                        oClsgloUserRights.CheckForUserRights(_UserName);

                       

                        if (oShortAppointment.ASCommonID > 0)
                        {


                            if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                DataTable dt = null;

                                if (_AppointmentDates.Dates.Count > 0)
                                {
                                    dt = ResourseName(oShortAppointment.ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate, oMasterAppointment.LocationName);
                                }
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            dt.Dispose();
                                            dt = null;
                                            return;
                                        }
                                        else
                                        {
                                            if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oShortAppointment.StartTime.ToShortTimeString() + " - " + oShortAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                            {

                                            }
                                            else
                                            {
                                                dt.Dispose();
                                                dt = null;
                             
                                                return;
                                            }
                                        }
                                    }
                                    if (dt != null)
                                    {
                                        dt.Dispose();
                                        dt = null;
                                    }
                             
                                }

                            }
                        }


                        if (oMasterAppointment.Resources.Count > 0)
                        {

                          
                            // if ((oClsgloUserRights.OverrideProviderBlockSchedule) && (oMasterAppointment.IsRecurrence == SingleRecurrence.Single))
                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                                {
                                    DataTable dt = null;

                                    if (_AppointmentDates.RemoveResourceBlockedSlots(oMasterAppointment.Resources[i].ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate))
                                    {
                                        dt = ResourseName(oMasterAppointment.Resources[i].ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate,oMasterAppointment.LocationName);
                                    }

                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                            {
                                                //isResourcesBlocked = true;
                                                MessageBox.Show("Schedule is blocked for the resource. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                dt.Dispose();
                                                dt = null;
                                                return;
                                            }
                                            else
                                            {

                                                if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.Resources[i].StartTime.ToShortTimeString() + " - " + oMasterAppointment.Resources[i].EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                                {
                                                }
                                                else
                                                {
                                                    dt.Dispose();
                                                    dt = null;
                                                    return;
                                                }
                                            }

                                        }
                                        if (dt != null)
                                        {
                                            dt.Dispose();
                                            dt = null;
                                        }
                                    }
                                }

                            }
                        }



                        //oClsgloUserRights.Dispose();
                        //oClsgloUserRights = null;



                        //
                        //
                        //End Checking Block timing 




                    #region "Overlap Template Block"

                    gloSettings.GeneralSettings oSet = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);

                    object _objSettingValue;
                    oSet.GetSetting("OverlapTemplateAppointment", 0, _ClinicID, out _objSettingValue);
                    oSet.Dispose();
                    oSet = null;

                    if (_objSettingValue.ToString () == "")
                    {
                        _objSettingValue = "U";
                    }
                        

                        bool UpdateOverlapAppointment = false;
                        Int32 TemplateCount;
                        TemplateCount = 0;

                        if (_ShowTemplateAppointment)
                        {
                            if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                            {

                                TemplateCount = 0;

                                    TemplateCount = Convert.ToInt32(GetAppointmentConflictTimeOverlapSet(
                                                                                    TemplateAllocationID,
                                                                                    Convert.ToInt64(oShortAppointment.ASCommonID),
                                                                                    Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                                                    gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                                                    gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()), _ClinicID,
                                                                                    Convert.ToDateTime(oShortAppointment.StartDate.ToString()),Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()),Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString())));
                                    if (TemplateCount > 0)
                                    {
                                        //DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template appointment slots.  Would you like to schedule all of the template slots this appointment overlaps or would you like to leave a conflict in the schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                        if (_objSettingValue.ToString() == "U")
                                        {
                                            DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template slots. Would you like to use all of the template slots this appointment overlaps?." + "\n\n" + "Yes – Schedule all template slots overlapped by this appointment." + "\n\n" + "No – Only use the existing slot.  A conflict will be created in the appointment schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                            if (dresult == DialogResult.Cancel)
                                            {
                                                return;
                                            }
                                            else if (dresult == DialogResult.Yes)
                                            {
                                                UpdateOverlapAppointment = true;
                                            }
                                        }
                                        else if (_objSettingValue.ToString() == "Y")
                                        {
                                            UpdateOverlapAppointment = true;
                                        }
                                    }
                            }
                        }
                        #endregion

                        if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                        {

                            //if (_result)
                            //{

                            if (UpdateOverlapAppointment)
                            {


                                // if modify the appointment time then it change the template status between original time period
                                if (oMasterAppointment.StartTime != oShortAppointment.StartTime || oMasterAppointment.Duration != (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes)
                                {
                                    ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);

                                }

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(TemplateAllocationID,
                                                            Convert.ToInt64(oShortAppointment.ASCommonID),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()),
                                                            Convert.ToDateTime(oShortAppointment.StartDate.ToString()),Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()),Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString()),
                                                            _ClinicID);
                            }
                            else if (((oMasterAppointment.StartTime != oShortAppointment.StartTime || oMasterAppointment.Duration != (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes)
                                        && TemplateCount == 0 && Convert.ToInt32(ApptFindCountOfOverlapTemplate(oShortAppointment.MasterID)) > 0)
                                       // || oMasterAppointment.ASBaseID != oShortAppointment.ASCommonID
                                        )
                            {


                                // if modify the appointment time then it change the template status between original time period
                                //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                //{
                                ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);

                                //}

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(TemplateAllocationID,
                                                            Convert.ToInt64(oShortAppointment.ASCommonID),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()),
                                                            Convert.ToDateTime(oShortAppointment.StartDate.ToString()),Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()),Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString())
                                                            , _ClinicID);
                            }
                            else if ((oMasterAppointment.StartTime != oShortAppointment.StartTime) && TemplateCount > 0)
                            {
                                // if modify the appointment time then it change the template status between original time period
                                //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                //{
                                ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);

                            }

                        }

                    //}
                        
                        
                        
                        
                        
                        //Make new single entry for the appointment


                        #region "Master Appointment Data"

                        if (gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }

                        oMasterAppointment.MasterID = 0;
                        oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
                        oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;

                        _CheckValue = oMasterAppointment.AppointmentTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterAppointment.AppointmentTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterAppointment.AppointmentTypeDesc;// = cmbApp_AppointmentType.Text;

                        oMasterAppointment.ASBaseID = oShortAppointment.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        oMasterAppointment.ASBaseCode = oShortAppointment.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        oMasterAppointment.ASBaseDescription = oShortAppointment.ASCommonDescription; // cmbApp_Provider.Text;
                        oMasterAppointment.ASBaseFlag = ASBaseType.Provider;

                        _CheckValue = oMasterAppointment.ReferralProviderID;// = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        _CheckValue = oMasterAppointment.ReferralProviderCode; // = cmbApp_ReferralDoctor.Text; //Remark
                        _CheckValue = oMasterAppointment.ReferralProviderName; //= cmbApp_ReferralDoctor.Text;

                        _CheckValue = oMasterAppointment.PatientID; //  = Convert.ToInt64(txtApp_Patient.Tag.ToString());


                        oMasterAppointment.StartDate = oShortAppointment.StartDate;
                        oMasterAppointment.StartTime = oShortAppointment.StartTime;
                        oMasterAppointment.EndDate = oShortAppointment.EndDate;
                        oMasterAppointment.EndTime = oShortAppointment.EndTime;
                        oMasterAppointment.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;
                        _CheckValue = oMasterAppointment.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                        _CheckValue = oMasterAppointment.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterAppointment.LocationName; // = cmbApp_Location.Text;

                        _CheckValue = oMasterAppointment.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterAppointment.DepartmentName; // = cmbApp_Department.Text;

                        _CheckValue = oMasterAppointment.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterAppointment.ClinicID; // = _SetAppointmentParameter.ClinicID;
                        //oMasterAppointment.UsedStatus = ASUsedStatus.NotUsed;

                        #endregion

                        #region "Appointment Criteria"

                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterAppointment.ProblemTypes.Count; i++)
                        {

                            oMasterAppointment.ProblemTypes[i].MasterID = 0;
                            oMasterAppointment.ProblemTypes[i].DetailID = 0;
                            oMasterAppointment.ProblemTypes[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.ProblemTypes[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.ProblemTypes[i].LineNo = 0;
                            oMasterAppointment.ProblemTypes[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterAppointment.ProblemTypes[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.ProblemTypes[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.ProblemTypes[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.ProblemTypes[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.ProblemTypes[i].ViewOtherDetails = "";
                            oMasterAppointment.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                        }

                        #endregion

                        #region "Resources"

                        for (int i = 0; i < oMasterAppointment.Resources.Count; i++)
                        {
                            oMasterAppointment.Resources[i].MasterID = 0;

                            oMasterAppointment.Resources[i].DetailID = 0;
                            oMasterAppointment.Resources[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.Resources[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.Resources[i].LineNo = 0;
                            oMasterAppointment.Resources[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.Resources[i].ASCommonFlag = ASBaseType.Resource;

                            oMasterAppointment.Resources[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.Resources[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.Resources[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.Resources[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.Resources[i].ViewOtherDetails = "";
                            oMasterAppointment.Resources[i].UsedStatus = ASUsedStatus.NotUsed;

                        }

                        #endregion

                        oMasterAppointment.Insurances = null;

                        // Added new AppointmentType and AppointmentTypeDesc
                        DataTable dtPasteAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                        if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                        {
                            oMasterAppointment.AppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                            oMasterAppointment.AppointmentTypeDesc = dtPasteAppointmentType.Rows[0]["AppointmentName"].ToString();
                            oMasterAppointment.ColorCode = Convert.ToInt32(dtPasteAppointmentType.Rows[0]["AppointmentColor"].ToString());

//                            dtPasteAppointmentType.Dispose();
                        }
                        if (dtPasteAppointmentType != null)
                        {
                            dtPasteAppointmentType.Dispose();
                            dtPasteAppointmentType = null;
                        }
                        if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            bool _result;
                            _result = ogloAppointment.Update_TVP(oMasterAppointment, SingleRecurrence.Single, oShortAppointment.MasterID, oShortAppointment.DetailID, 1, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                            _retValue = oShortAppointment.MasterID;
                        }
                        else
                        {
                            _retValue = ogloAppointment.Add(oMasterAppointment, AppointmentScheduleFlag.TemplateAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);

                        //Update TemplateAllocation
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                      
                        if (oDB != null)
                        {

                            oDB.Connect(false);
                            string _sqlQuery = " UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered = 0 "
                          + " FROM AS_Appointment_DTL INNER JOIN AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID "
                          + " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = AS_Appointment_DTL.nTemplateAllocationID "
                          + " AND AS_Appointment_DTL.nDTLAppointmentID = " + oShortAppointment.DetailID + " AND AS_Appointment_DTL.nMSTAppointmentID = " + oShortAppointment.MasterID + " AND  AS_Appointment_DTL.nClinicID = " + _ClinicID + "  ";
                            oDB.ExecuteScalar_Query(_sqlQuery);
                            oDB.Disconnect();
                            oDB.Dispose();
                            oDB = null;
                        }


                        //Delete the Recurrence entry of the appointment

                            if (_retValue > 0)
                            {
                                bool _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, false);
                                if (_IsDeleted)
                                {
                                    if (gloHL7.boolSendAppointmentDetails) // condition Added by Abhijeet on 20110920
                                    {
                                        gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, oShortAppointment.MasterID, "", _databaseconnectionstring, true);
                                    }
                                }
                            }
                        }

                        //Audit Trail
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment Modified", _PatientD, oShortAppointment.MasterID, oShortAppointment.ASCommonID, ActivityOutCome.Success);
                    }

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oMasterAppointment != null) { oMasterAppointment.Dispose(); oMasterAppointment = null; }
                if (_CheckValue != null) { _CheckValue = null; }
                if (ogloAppointment != null) { ogloAppointment.Dispose(); ogloAppointment = null; }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
                if (_AppointmentDates != null)
                {
                    _AppointmentDates.Dispose();
                    _AppointmentDates = null;
                }
            }
        }


        private void PasteAppointment_Click(CutCopyPaste oCutCopy)
        {
            cmnu_AppointmentNew.Visible = false;
            TimeSpan tsPasteTime = new TimeSpan(0, 0, 0);
            DateTime dtDate;
            ScheduleAppointmentOwner PasteOwner = null;
            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment = null;
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            Boolean _IsModified = false;
            //DataTable dt = new DataTable();
            ShortApointmentSchedule oShortAppointment = null;
            MasterAppointment oMstAppointment = null;
            ShortApointmentSchedule oShortResource = null;
            int tempValue = 0;

            try
            {
                if (_JanusPastePoint.IsEmpty == false)
                {
                    if (oCutCopy == CutCopyPaste.Copy && cmnu_Appointment_Copy.Tag != null)
                    {
                        oJUC_Appointment = (ScheduleAppointment)cmnu_Appointment_Copy.Tag;
                    }
                    else if (oCutCopy == CutCopyPaste.Cut && cmnu_Appointment_Cut.Tag != null)
                    {
                        oJUC_Appointment = (ScheduleAppointment)cmnu_Appointment_Cut.Tag;
                    }
                    else
                    {
                        oJUC_Appointment = null;
                    }




                    #region Check Is Cut/Copy Appointment and Paste Appointment Template Appointment or not

                    bool IsCutOrCopyTemplateAppointment = false;
                    bool IsPasteTemplateAppointment = false;

                    int cutOrCopyTemplateAppointmentType = 0;
                    int pasteTemplateAppointmentType = 0;

                    long cutOrCopyTemplateAppointmentTypeID = 0;
                    long pasteTemplateAppointmentTypeID = 0;

                    int cutOrCopyTemplateAppointmentTypeFlag = 0;
                    int pasteTemplateAppointmentTypeFlag = 0;

                    string cutOrCopyTemplateAppointmentName = "";
                    string pasteTemplateAppointmentName = "";

                    bool restrictedTempAppAndSameTypeAppFlag = false;

                    int pasteTemplateAppointmentDuration = 0;


                    // Get Appointment Type Details for Cut OR Copy Appointment 
                    DataTable dtAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2)));
                    if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 0)
                    {
                        cutOrCopyTemplateAppointmentType = Convert.ToInt16(dtAppointmentType.Rows[0]["TemplateAppointment"].ToString());
                        cutOrCopyTemplateAppointmentTypeID = Convert.ToInt64(dtAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                        cutOrCopyTemplateAppointmentTypeFlag = Convert.ToInt16(dtAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                        cutOrCopyTemplateAppointmentName = dtAppointmentType.Rows[0]["AppointmentName"].ToString();
                     //   dtAppointmentType.Dispose();
                    }

                    if (dtAppointmentType != null)
                    {
                        dtAppointmentType.Dispose();
                        dtAppointmentType = null;
                    }
                    // Check is Cut/Copy Appointment an template Appointment
                    if (cutOrCopyTemplateAppointmentType > 0)
                    {
                        IsCutOrCopyTemplateAppointment = true;
                    }
                    else
                    {
                        IsCutOrCopyTemplateAppointment = false;
                    }

                    // Check is Paste Appointment an template Appointment

                    if (juc_Appointment.CurrentAppointment != null)
                    {
                        pasteTemplateAppointmentType = Convert.ToInt16(Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4)));
                        if (pasteTemplateAppointmentType == Convert.ToInt16(AppointmentScheduleFlag.TemplateBlock))
                        {
                            IsPasteTemplateAppointment = true;
                            DataTable dtPasteAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2)));
                            if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                            {
                                pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                pasteTemplateAppointmentTypeFlag = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentTypeFlag"].ToString());
                                pasteTemplateAppointmentName = dtPasteAppointmentType.Rows[0]["AppointmentName"].ToString();
                              //  dtPasteAppointmentType.Dispose();
                            }
                            if (dtPasteAppointmentType != null)
                            {
                                dtPasteAppointmentType.Dispose();
                                dtPasteAppointmentType = null;
                            }
                        }
                        else
                        {
                            IsPasteTemplateAppointment = false;
                        }
                    }
                    else
                    {
                        IsPasteTemplateAppointment = false;
                    }



                    if (_RegAppUsingTemplateOnly)
                    {
                        if (!IsCutOrCopyTemplateAppointment)
                        {
                            MessageBox.Show("You don't have rights to Cut/Copy appointment from non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (!IsCutOrCopyTemplateAppointment || !IsPasteTemplateAppointment)
                        {
                            MessageBox.Show("You don't have rights to paste appointments on non template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            if (cutOrCopyTemplateAppointmentName != pasteTemplateAppointmentName)
                            {
                                MessageBox.Show("Appointment types are different, cannot paste appointments on template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                restrictedTempAppAndSameTypeAppFlag = true;
                            }
                        }

                    }
                    else
                    {
                        //if (IsCutOrCopyTemplateAppointment && IsPasteTemplateAppointment)
                        //{
                        //    if (cutOrCopyTemplateAppointmentTypeFlag != pasteTemplateAppointmentTypeFlag)
                        //    {
                        //        MessageBox.Show("Sorry appointment types are different, cannot paste appointments on template slots. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        return;
                        //    }
                        //}


                        if (IsPasteTemplateAppointment)
                        {
                            restrictedTempAppAndSameTypeAppFlag = true;
                        }
                        else
                        {
                            restrictedTempAppAndSameTypeAppFlag = false;
                        }
                    }






                    #endregion


                    #region " Get Appointment Time "

                    switch (juc_Appointment.View)
                    {
                        case ScheduleView.DayView:
                            {
                                tsPasteTime = juc_Appointment.GetTimeAt(_JanusPastePoint);
                            }
                            break;
                        case ScheduleView.MonthView:
                            {
                                tsPasteTime = oJUC_Appointment.StartTime.TimeOfDay;
                            }
                            break;
                        case ScheduleView.WeekView:
                            {
                                tsPasteTime = juc_Appointment.GetTimeAt(_JanusPastePoint);
                            }
                            break;
                        case ScheduleView.WorkWeek:
                            {
                                tsPasteTime = juc_Appointment.GetTimeAt(_JanusPastePoint);
                            }
                            break;
                        default:
                            {
                                tsPasteTime = oJUC_Appointment.StartTime.TimeOfDay;
                            }
                            break;
                    }

                    #endregion " Get Appointment Time "

                    #region " Get New Appointment Date,Owner Information "

                    dtDate = juc_Appointment.GetDateAt(_JanusPastePoint);
                    PasteOwner = juc_Appointment.GetOwnerAt(_JanusPastePoint);
                    //TimeSpan tsAppointmentDuration = oJUC_Appointment.EndTime.TimeOfDay.Subtract(oJUC_Appointment.StartTime.TimeOfDay);
                    TimeSpan tsAppointmentDuration = oJUC_Appointment.Duration;

                    #endregion " Get New Appointment Date,Owner Information "

                    #region "Check License"
                    if (base.SetChildFormModules("PasteAppointment_Click", "Paste Appointment", Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 1))) == true)
                    {
                        return;
                    }
                    #endregion ""

                    ASBaseType PasteOwnerType = ASBaseType.None;
                    PasteOwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(PasteOwner.Value.ToString(), '~', 3));
                    bool _isResourceOnly = false;

                    #region " Get Appointment Information "

                    oShortAppointment = new ShortApointmentSchedule();
                    oMstAppointment = new MasterAppointment();
                    oShortAppointment.MasterID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1));
                    oShortAppointment.DetailID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2));
                    // to resolved issue 10901 Check usedStatus
                    oShortAppointment.UsedStatus = (ASUsedStatus)(Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 10)));

                    _isResourceOnly = IsResourceOnlyAppointment(oShortAppointment.MasterID);

                    if (restrictedTempAppAndSameTypeAppFlag == true)
                    {
                        oShortAppointment.StartDate = dtDate;
                        oShortAppointment.StartTime = Convert.ToDateTime(juc_Appointment.CurrentAppointment.StartTime.ToShortTimeString());
                        oShortAppointment.EndDate = dtDate;
                        oShortAppointment.EndTime = Convert.ToDateTime(juc_Appointment.CurrentAppointment.EndTime.ToShortTimeString());
                    }
                    else
                    {
                        oShortAppointment.StartDate = dtDate;
                        if (tsPasteTime.ToString() == "00:00:00")
                        {
                            oShortAppointment.StartTime = Convert.ToDateTime(oJUC_Appointment.StartTime.ToShortTimeString());
                            oShortAppointment.EndTime = Convert.ToDateTime(oJUC_Appointment.EndTime.ToShortTimeString());
                        }
                        else
                        {
                            oShortAppointment.StartTime = Convert.ToDateTime(dtDate.ToShortDateString() + " " + tsPasteTime.Hours + ":" + tsPasteTime.Minutes + ":" + tsPasteTime.Seconds);
                            oShortAppointment.EndTime = oShortAppointment.StartTime.Add(tsAppointmentDuration);
                        }
                        //oShortAppointment.EndDate = dtDate;
                        oShortAppointment.EndDate = oShortAppointment.StartTime.Add(tsAppointmentDuration);
                    }

                    if (_isResourceOnly == false)
                    {
                        oShortAppointment.ASCommonFlag = ASBaseType.Provider;
                        oShortAppointment.ASCommonID = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                        oShortAppointment.ASCommonCode = "";
                        oShortAppointment.ASCommonDescription = Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 2));
                    }
                    else
                    {
                        oShortAppointment.ASCommonFlag = ASBaseType.Resource;
                        oShortAppointment.ASCommonID = 0;
                        oShortAppointment.ASCommonCode = "";
                        oShortAppointment.ASCommonDescription = "";
                    }
                    oShortAppointment.IsRecurrence = (SingleRecurrence)Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 3));
                    if (_isConflict == false && _IsModified == false)
                    {
                        if (oCutCopy == CutCopyPaste.Cut)
                        {
                            cmnu_AppointmentNew.Visible = false;




                            if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                if (_ShowTemplateAppointment)
                                {
                                    if (juc_Appointment.CurrentAppointment != null)
                                    {
                                        pasteTemplateAppointmentType = Convert.ToInt16(Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4)));
                                        if (pasteTemplateAppointmentType == Convert.ToInt16(AppointmentScheduleFlag.TemplateBlock))
                                        {


                                            if (oShortAppointment != null)
                                            {
                                                TimeSpan timeSpan = oShortAppointment.EndTime - oShortAppointment.StartTime;

                                                //if ((decimal)timeSpan.Minutes == (decimal)oJUC_Appointment.Duration.TotalMinutes)
                                                // {
                                                if (CheckSameTemplateSlotType(juc_Appointment.CurrentAppointment, oJUC_Appointment) == true)
                                                {
                                                    if (MessageBox.Show("Do you wish to book this appointment into the existing template slot?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                    {
                                                        return;

                                                    }
                                                    else
                                                    {
                                                        tempValue = 1;
                                                    }


                                                }
                                                //}
                                            }

                                            if (tempValue == 0)
                                            {
                                                if (MessageBox.Show("The appointment will take on the appointment type and length of the new template slot. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                            }
                                        }

                                        else
                                            if (MessageBox.Show("The appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                return;
                                            }
                                    }



                                    else

                                        if (oJUC_Appointment != null && oShortAppointment != null)
                                        {
                                            TimeSpan ts =  oShortAppointment.EndTime - oShortAppointment.StartTime;

                                            // check any slot present with this oAppointment start time , end time and appointment type


                                            DataTable dtPasteAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2)));
                                            pasteTemplateAppointmentTypeID = 0;
                                            pasteTemplateAppointmentDuration = 0;
                                            if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                                            {
                                                pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                                pasteTemplateAppointmentDuration = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentDuration"].ToString());
                                                //dtPasteAppointmentType.Dispose();
                                            }

                                            if (dtPasteAppointmentType != null)
                                            {
                                                dtPasteAppointmentType.Dispose();
                                                dtPasteAppointmentType = null;
                                            }
                                            // Get ALL SELECTED LOCATIONS
                                           string _sLastSelectedLocation = string.Empty;


                                            for (int i = 0; i < trvLocations.Nodes.Count; i++)
                                            {
                                                if (trvLocations.Nodes[i].Checked == true)
                                                {
                                                    _sLastSelectedLocation += Convert.ToString(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                                                }
                                            }
                                            if (_sLastSelectedLocation.Trim() != "")
                                            {
                                                _sLastSelectedLocation = _sLastSelectedLocation.Remove(_sLastSelectedLocation.LastIndexOf(','));
                                            }



                                            if (CheckSlotPresent(oShortAppointment.StartTime, gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.TimeOfDay.ToString()), pasteTemplateAppointmentTypeID, oShortAppointment.ASCommonID, _ClinicID, Convert.ToDateTime(oShortAppointment.StartTime.TimeOfDay.ToString()), Convert.ToDateTime(oShortAppointment.EndTime.TimeOfDay.ToString()), _sLastSelectedLocation, Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2))) == true)
                                            {
                                                // if ((decimal)pasteTemplateAppointmentDuration == (decimal)ts.Minutes)
                                                // {
                                                if (MessageBox.Show("Do you wish to double book this time slot, placing the new appointment in the slot and retaining an open slot at this time?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    tempValue = 1;
                                                }
                                                // }
                                            }


                                            if (tempValue == 0)
                                            {
                                                if (MessageBox.Show("The appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                }
                                else
                                    if (MessageBox.Show("Do you want to modify this appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        return;
                                    }
                            }

                                //recurrence
                            else
                            {
                                if (_ShowTemplateAppointment)
                                {
                                    if (juc_Appointment.CurrentAppointment != null)
                                    {
                                        pasteTemplateAppointmentType = Convert.ToInt16(Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4)));
                                        if (pasteTemplateAppointmentType == Convert.ToInt16(AppointmentScheduleFlag.TemplateBlock))
                                        {



                                            if (oShortAppointment != null)
                                            {
                                                TimeSpan timeSpan =  oShortAppointment.EndTime - oShortAppointment.StartTime;

                                                // if ((decimal)timeSpan.Minutes == (decimal)juc_Appointment.CurrentAppointment.Duration.TotalMinutes)
                                                // {
                                                if (CheckSameTemplateSlotType(juc_Appointment.CurrentAppointment, oJUC_Appointment) == true)
                                                {
                                                    if (MessageBox.Show("Do you wish to book this appointment into the existing template slot?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                    {
                                                        return;

                                                    }
                                                    else
                                                    {
                                                        tempValue = 1;
                                                    }


                                                }
                                                // }
                                            }

                                            if (tempValue == 0)
                                            {
                                                if (MessageBox.Show("The recurring appointment will take on the appointment type and length of the new template slot. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (MessageBox.Show("The recurring appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                return;
                                            }
                                        }
                                    }





                                    else


                                        if (oJUC_Appointment != null && oShortAppointment != null)
                                        {
                                            TimeSpan ts = oShortAppointment.EndTime - oShortAppointment.StartTime;


                                            // check any slot present with this oAppointment start time , end time and appointment type


                                            DataTable dtPasteAppointmentType = GetAppointmentTypeDetails(Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1)), Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2)));
                                            pasteTemplateAppointmentTypeID = 0;
                                            pasteTemplateAppointmentDuration = 0;
                                            if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                                            {
                                                pasteTemplateAppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                                pasteTemplateAppointmentDuration = Convert.ToInt16(dtPasteAppointmentType.Rows[0]["AppointmentDuration"].ToString());
                                             //   dtPasteAppointmentType.Dispose();
                                            }
                                            if (dtPasteAppointmentType != null)
                                            {
                                                dtPasteAppointmentType.Dispose();
                                                dtPasteAppointmentType = null;
                                            }

                                            // Get ALL SELECTED LOCATIONS


                                            string _sLastSelectedLocation = string.Empty;


                                            for (int i = 0; i < trvLocations.Nodes.Count; i++)
                                            {
                                                if (trvLocations.Nodes[i].Checked == true)
                                                {
                                                    _sLastSelectedLocation += Convert.ToString(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                                                }
                                            }
                                            if (_sLastSelectedLocation.Trim() != "")
                                            {
                                                _sLastSelectedLocation = _sLastSelectedLocation.Remove(_sLastSelectedLocation.LastIndexOf(','));
                                            }



                                            if (CheckSlotPresent(oShortAppointment.StartTime, gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.TimeOfDay.ToString()), pasteTemplateAppointmentTypeID, oShortAppointment.ASCommonID, _ClinicID, Convert.ToDateTime(oShortAppointment.StartTime.TimeOfDay.ToString()), Convert.ToDateTime(oShortAppointment.EndTime.TimeOfDay.ToString()), _sLastSelectedLocation, Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2))) == true)
                                            {
                                                // if ((decimal)pasteTemplateAppointmentDuration == (decimal)ts.Minutes)
                                                // {
                                                if (MessageBox.Show("Do you wish to double book this time slot, placing the new appointment in the slot and retaining an open slot at this time?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    tempValue = 1;
                                                }
                                                // }
                                            }





                                            if (tempValue == 0)
                                            {

                                                if (MessageBox.Show("The recurring appointment will be scheduled into the new time slot with no change to the original appointment type and length. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                {
                                                    return;
                                                }
                                            }
                                        }

                                }
                                else
                                {
                                    if (MessageBox.Show("Do you want to modify this recurring appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    else
                    { _isConflict = false; }

                    if (oCutCopy == CutCopyPaste.Copy)
                    {
                        oShortAppointment.UsedStatus = ASUsedStatus.Registred;

                    }
                    //else if (oCutCopy == CutCopyPaste.Cut && _isResourceOnly == false && gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oShortAppointment.UsedStatus == ASUsedStatus.CheckIn)
                    //{

                    //    if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked in."
                    //             + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    //    {
                    //        oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                    //        oMstAppointment.AppointmentStatusID = 1;
                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //}
                    //else if (oCutCopy == CutCopyPaste.Cut && _isResourceOnly == false && gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oShortAppointment.UsedStatus == ASUsedStatus.CheckOut)
                    //{
                    //    if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked Out."
                    //             + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    //    {
                    //        oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                    //        oMstAppointment.AppointmentStatusID = 1;
                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //}

                    #region " Get Clinic time & proposed start time & end time "

                    DateTime _nClinicStartTime = _dtClinicStartTime;
                    DateTime _nClinicEndTime = _dtClinicEndTime;
                    DateTime _nProposedStartTime = oShortAppointment.StartTime;
                    DateTime _nProposedEndTime = oShortAppointment.EndTime;
                    Int64 _nBaseId = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                    string _sProviderName = Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 2));

                    #endregion " Get Clinic time & proposed start time & end time "

                    DialogResult _DialogResult = DialogResult.None;
                    if ((Convert.ToDateTime(_nProposedStartTime.ToShortTimeString())) < (Convert.ToDateTime(_nClinicStartTime.ToShortTimeString())) || (Convert.ToDateTime(_nProposedStartTime.ToShortTimeString())) > (Convert.ToDateTime(_nClinicEndTime.ToShortTimeString())))
                    {
                        cmnu_AppointmentNew.Visible = false;
                        _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (_DialogResult == DialogResult.No)
                        {
                            FillAppointments(_ShowTemplateAppointment);
                            return;
                        }
                    }
                    else if (Convert.ToDateTime(_nProposedEndTime.ToShortTimeString()) < (Convert.ToDateTime(_nClinicStartTime.ToShortTimeString())) || (Convert.ToDateTime(_nProposedEndTime.ToShortTimeString())) > (Convert.ToDateTime(_nClinicEndTime.ToShortTimeString())))
                    {
                        cmnu_AppointmentNew.Visible = false;
                        _DialogResult = MessageBox.Show(" Appointment is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (_DialogResult == DialogResult.No)
                        {
                            FillAppointments(_ShowTemplateAppointment);
                            return;
                        }
                    }

                    if (oShortAppointment.ASFlag == AppointmentScheduleFlag.TemplateAppointment)
                    {
                        oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                    }
                    else
                    {

                        #region Same Date Validation

                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            gloAppointment objappoinmnet = new gloAppointment(_databaseconnectionstring);

                            Int64 PatientID = Convert.ToInt64(oShortAppointment.PatientID);
                            Int64 _Appointmentcnt;

                            using (DataTable dtPatient = ogloAppointment.GetPatient(oShortAppointment.MasterID, oShortAppointment.DetailID))
                            {
                                if (dtPatient != null && dtPatient.Rows.Count > 0)
                                {
                                    PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                }
                            }

                            _Appointmentcnt = objappoinmnet.IsAppointmentOnToday(PatientID, _ClinicID, dtDate, 0);
                            objappoinmnet.Dispose();
                            objappoinmnet = null;
                            if (_Appointmentcnt >= 1)
                            {

                                DialogResult dresult = MessageBox.Show("This patient is already scheduled for selected date. Do you want to register a new Appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dresult == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }


                        #endregion Same Date Validation

                        if (IsMaximumAppointmentRegisterd(oShortAppointment.StartDate, oShortAppointment.StartTime, _nBaseId) == false)
                        {
                            oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                        }
                        else
                        {
                            DialogResult dgResult = DialogResult.None;
                            //dgResult = MessageBox.Show("All appointments for " + _sProviderName + " are filled for this time.  Do you want to create an additional appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            dgResult = MessageBox.Show("All appointments for " + _sProviderName + " are filled for this time.  Do you want to create an additional appointment?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dgResult == DialogResult.Yes)
                            {
                                oShortAppointment.UsedStatus = ASUsedStatus.Waiting;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                     oShortResource = new ShortApointmentSchedule();
                    if (_isResourceOnly == true)
                    {

                                    Int64 OwnerId = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                                    string Location = GetTagElement(PasteOwner.Value.ToString(), '~', 6).ToString();
                                   DataTable dt = ogloAppointment.ResourseName(OwnerId, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate, Location);
                                    if (dt.Rows.Count >= 1 && dt != null)
                                    {
                                        if (MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oShortAppointment.StartTime.ToShortTimeString() + " - " + oShortAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            dt.Dispose();
                                            dt = null;
                                            return;
                                        }
                                    }
                                    if (dt != null)
                                    {
                                        dt.Dispose();
                                        dt = null;
                                    }

                        oShortResource.MasterID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1)); ;
                        oShortResource.DetailID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 2)); ;
                        oShortResource.StartDate = dtDate;
                        oShortResource.EndDate = dtDate;


                        if (restrictedTempAppAndSameTypeAppFlag == true)
                        {
                            oShortAppointment.StartTime = Convert.ToDateTime(juc_Appointment.CurrentAppointment.StartTime.ToShortTimeString());
                            oShortAppointment.EndTime = Convert.ToDateTime(juc_Appointment.CurrentAppointment.EndTime.ToShortTimeString());
                        }
                        else
                        {
                            oShortResource.StartTime = Convert.ToDateTime(dtDate.ToShortDateString() + " " + tsPasteTime.Hours + ":" + tsPasteTime.Minutes + ":" + tsPasteTime.Seconds);
                            oShortResource.EndTime = oShortAppointment.StartTime.Add(tsAppointmentDuration);
                        }


                        oShortResource.ASCommonFlag = ASBaseType.Resource;
                        oShortResource.ASCommonID = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                        oShortResource.ASCommonCode = Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 4));
                        oShortResource.ASCommonDescription = Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 2));
                        oShortResource.IsRecurrence = (SingleRecurrence)Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 3));
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            oShortResource.UsedStatus = ASUsedStatus.Registred;
                        }
                        else if (oCutCopy == CutCopyPaste.Cut && gloDateMaster.gloDate.DateAsNumber(oShortResource.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ".The appointment status will be reset and the appointment will no longer be checked in."
                                     + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                                oMstAppointment.AppointmentStatusID = 1;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    #endregion " Get Appointment Information "

                    //Allow to paste to "Resorce Owner" only if appointment is RESOURCE ONLY
                    if ((_isResourceOnly == true && PasteOwnerType == ASBaseType.Resource)
                         || (_isResourceOnly == false && PasteOwnerType == ASBaseType.Provider)
                        )
                    {

                        ////SHUBHANGI 20100724 RESTRICT TO ADD NEW APPOINTMENT IN THE ALREADY ALLOCATED TIME SPAN
                        Int64 _appointmentID = Convert.ToInt64(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 1));
                        
                       //// BY Pranit on 20111221 to solve issue # 17239
                       //// IF case = copy then To show conflict message for same appointment make appointmentID=0
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            _appointmentID = 0;
                        }
                        //// End by Pranit on 20111221 to solve issue # 17239
                        
                        if (IsAppointmentRegisterd(oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.ASCommonID, _appointmentID) == false)
                        {
                            if (oCutCopy == CutCopyPaste.Copy)
                            {
                                oShortAppointment.UsedStatus = ASUsedStatus.Registred;
                            }
                        }
                        else
                        {
                            DialogResult dgResult = DialogResult.None;
                            dgResult = MessageBox.Show("Warning –  " + oShortAppointment.ASCommonDescription + "  has appointment conflicts during this time. Continue with this new appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dgResult == DialogResult.Yes)
                            {
                                oShortAppointment.UsedStatus = ASUsedStatus.Waiting;
                            }
                            else
                            {
                                return;
                            }
                        }

                        if (!SavePAValidation(Convert.ToInt64(oShortAppointment.MasterID), Convert.ToInt64(oShortAppointment.DetailID), Convert.ToDateTime(oShortAppointment.StartDate), Convert.ToDateTime(oShortAppointment.EndDate), oCutCopy))
                        {
                            return;
                        }
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            if (_isResourceOnly == false)
                                MakePasteAppointment(oShortAppointment, oCutCopy);
                            else
                                MakePasteAppointmentResourceOnly(oShortAppointment, oShortResource, oCutCopy);

                        }
                        else if (oCutCopy == CutCopyPaste.Cut)
                        {
                            if ((SingleRecurrence)(Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 3))) == SingleRecurrence.Recurrence
                               || (SingleRecurrence)(Convert.ToInt32(GetTagElement(oJUC_Appointment.Tag.ToString(), '~', 3))) == SingleRecurrence.SingleInRecurrence)
                            {
                                if (PasteOwner.Value == oJUC_Appointment.Owner)
                                {
                                    Int64 OwnerId = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                                    string Location = GetTagElement(PasteOwner.Value.ToString(), '~', 6).ToString(); 
                                    DataTable dt = ogloAppointment.ResourseName(OwnerId, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate,Location);
                                    // To Do : Check record count for dt find all dependancies
                                    if (dt.Rows.Count >= 1 && dt != null)
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oShortAppointment.StartTime.ToShortTimeString() + " - " + oShortAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                            UpdateReccurance(oShortAppointment);
                                        MakePasteAppointment(oShortAppointment, oCutCopy);
                                    }
                                    else
                                    {
                                        //Appointment cut paste 02182011 Mahesh Nawal 
                                        //UpdateReccurance(oShortAppointment);

                                        // Commented last line below by Pranit on 10 feb 2012 and created new If Block (first if 
                                        // block was Not present)

                                        if (_isResourceOnly == false)
                                            MakePasteAppointment(oShortAppointment, oCutCopy);
                                        else
                                            UpdateReccurance(oShortAppointment);

                                        //MakePasteAppointment(oShortAppointment, oCutCopy);  // Commented line on 10 feb 2012
                                    }
                                    if (dt != null)
                                    {
                                        dt.Dispose();
                                        dt = null;
                                    }
                                }
                                else
                                {
                                    if (_isResourceOnly == false)
                                        MakePasteAppointment(oShortAppointment, oCutCopy);
                                    else
                                        MakePasteAppointmentResourceOnly(oShortAppointment, oShortResource, oCutCopy);
                                }
                            }
                            else
                            {
                                if (_isResourceOnly == false)
                                    MakePasteAppointment(oShortAppointment, oCutCopy);
                                else
                                 // Commented by Pranit on 10 feb 2012  and opened below commented code
                                // MakePasteAppointmentResourceOnly(oShortAppointment, oShortResource, oCutCopy);
                                UpdateSingleResourceAppointment(oShortAppointment, oShortResource, oMstAppointment);

                                //if (_isResourceOnly == false)
                                //    UpdateSingleAppointment(oShortAppointment, oMstAppointment);
                                //else
                                //    UpdateSingleResourceAppointment(oShortAppointment, oShortResource, oMstAppointment);
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
                cmnu_Appointment_Cut.Tag = null;
                if (PasteOwner != null)
                {
                    PasteOwner = null;
                }
                if (oJUC_Appointment != null)
                {
                    oJUC_Appointment = null;
                }
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
                if (oShortAppointment != null)
                {
                    oShortAppointment.Dispose();
                    oShortAppointment = null;
                }
                if (oMstAppointment != null)
                {
                    oMstAppointment.Dispose();
                    oMstAppointment = null;
                }
                if (oShortResource != null)
                {
                    oShortResource.Dispose();
                    oShortResource = null;
                }
            }

        }

        private DataTable GetAppointmentTypeDetails(long mstAppID, long dtlAppID)
        {
          
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            DataTable dt =   ogloAppointment.GetAppointmentTypeDetails(mstAppID, dtlAppID);
            ogloAppointment.Dispose();
            ogloAppointment = null;
            return dt;
        }

        private DataTable GetAppointmentTypeDetailsUsingAllocationID (long mstAllID, long allID)
        {
            
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            DataTable dt = ogloAppointment.GetAppointmentTypeDetailsUsingAllocationID(mstAllID, allID);
            ogloAppointment.Dispose();
            ogloAppointment = null;
            return dt;
        }


       
        
        //private bool IsAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime, Int64 AppointmentID)
        //{            
        //    bool _result = false;
        //    gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
        //    try
        //    {
            
        //       object ResultCount = GetTemplateConflictTime(AppointmentID, providerId, gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()), gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()), _ClinicID);
        //        _result = ogloAppointment.IsAppointmentRegisterd(dtAppDate, dtAppTime, ProviderID, AppointmentID);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    return _result;
        //}








        //SHUBHANGI 20100724 RESTRICT TO ADD NEW APPOINTMENT IN THE ALREADY ALLOCATED TIME SPAN 
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
            ogloAppointment.Dispose();
            ogloAppointment = null;
            return _result;
        }

        private void MakePasteAppointment(ShortApointmentSchedule oShortAppointment, CutCopyPaste oCutCopy)
        {

            gloAppointmentScheduling.gloAppointment ogloAppointment = null;
            gloAppointmentScheduling.MasterAppointment oMasterAppointment = null;
          //  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            PriorAuthorizationTransaction oPATransaction = null;
            Int64 _retValue = 0;
            Int64 _PrevmstAppID = 0;
            Int64 _PrevdtlAppID = 0;
            object _CheckValue = null;

            try
            {
                if (oShortAppointment != null)
                {
                    ogloAppointment = new gloAppointment(_databaseconnectionstring);
                    
                    oMasterAppointment = ogloAppointment.GetMasterAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);



                    if (oCutCopy == CutCopyPaste.Cut && oShortAppointment.IsRecurrence == SingleRecurrence.Single && gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oMasterAppointment.UsedStatus == ASUsedStatus.CheckIn)
                    {

                        if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked in."
                                 + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (oCutCopy == CutCopyPaste.Cut && oShortAppointment.IsRecurrence == SingleRecurrence.Single && gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) && oMasterAppointment.UsedStatus == ASUsedStatus.CheckOut)
                    {

                        if (MessageBox.Show("You have changed the appointment date for this appointment to " + oShortAppointment.StartDate.ToShortDateString() + ". The appointment status will be reset and the appointment will no longer be checked out."
                                 + Environment.NewLine + "Are you sure you want to move this appointment to " + oShortAppointment.StartDate.ToShortDateString() + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else
                        {
                            return;
                        }
                    }

                    //Checking Block timing 
                    //
                    //

                    gloAppointmentScheduling.Criteria.FindRecurrences dummyRecurrence = new gloAppointmentScheduling.Criteria.FindRecurrences();
                    _AppointmentDates = dummyRecurrence.GetRecurrence(oMasterAppointment.Criteria, oShortAppointment.ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.MasterID, oMasterAppointment.ResourceIDS);
                    dummyRecurrence.Dispose();
                    dummyRecurrence = null;

                 

                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);

                    //_ProviderName = GetProviderName(oMasterAppointment.ASBaseID, _ClinicID);

                    //bool isResourcesBlocked = false;
                    //bool isProviderBlocked = false;

                   


                    if (oShortAppointment.ASCommonID > 0)
                    {

                        
                        if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            DataTable dt=null;
                            if (_AppointmentDates.Dates.Count > 0)
                            {
                                dt = ResourseName(oShortAppointment.ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate,oMasterAppointment.LocationName);
                            }
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                    {
                                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dt.Dispose();
                                        dt = null;
                                        return;
                                    }
                                    else
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oShortAppointment.StartTime.ToShortTimeString() + " - " + oShortAppointment.EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {

                                        }
                                        else
                                        {
                                            dt.Dispose();
                                            dt = null;
                                            return;
                                        }
                                    }
                                }

                                if (dt != null)
                                {
                                    dt.Dispose();
                                    dt = null;
                                }
                            }
                        }
                    }


                    if (oMasterAppointment.Resources.Count > 0)
                    {

                        
                        // if ((oClsgloUserRights.OverrideProviderBlockSchedule) && (oMasterAppointment.IsRecurrence == SingleRecurrence.Single))
                        if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            DataTable dt = null;
                            for (int i = 0; i <= oMasterAppointment.Resources.Count - 1; i++)
                            {

                                if (_AppointmentDates.RemoveResourceBlockedSlots(oMasterAppointment.Resources[i].ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate))
                                {
                                    dt = ResourseName(oMasterAppointment.Resources[i].ASCommonID, oShortAppointment.StartTime, oShortAppointment.EndTime, oShortAppointment.StartDate, oMasterAppointment.LocationName);
                                }

                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                        {
                                            //isResourcesBlocked = true;
                                            MessageBox.Show("Schedule is blocked for the resource. Appointment cannot be created.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            dt.Dispose();
                                            dt = null;
                                            return;
                                        }
                                        else
                                        {

                                            if (DialogResult.Yes == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Save this " + oMasterAppointment.Resources[i].StartTime.ToShortTimeString() + " - " + oMasterAppointment.Resources[i].EndTime.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                            {
                                            }
                                            else
                                            {
                                                dt.Dispose();
                                                dt = null;
                                                return;
                                            }
                                        }

                                    }
                                    if (dt != null)
                                    {
                                        dt.Dispose();
                                        dt = null;
                                    }
                                }
                            }

                        }
                    }



                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;



                    //
                    //
                    //End Checking Block timing 





                if (oMasterAppointment != null)
                {
                    //Make new single entry for the appointment


                    #region "Overlap Template Block"

                    gloSettings.GeneralSettings oSet = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    object _objSettingValue;
                    oSet.GetSetting("OverlapTemplateAppointment", 0, _ClinicID, out _objSettingValue);
                    oSet.Dispose();
                    oSet = null;

                    if (_objSettingValue.ToString() == "")
                    {
                        _objSettingValue = "U";
                    }

                    //if (_objSettingValue.ToString() == "U")
                    //{
                        Int64 TemplateAllocationID = 0;
                        if (juc_Appointment.CurrentAppointment == null)
                        {
                            TemplateAllocationID = 0;
                        }
                        else
                        {
                            TemplateAllocationID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));
                        }

                        bool UpdateOverlapAppointment = false;
                        Int32 TemplateCount;
                        TemplateCount = 0;

                        if (_ShowTemplateAppointment)
                        {
                            if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                TemplateCount = 0;

                                string LocationIDs = "";
                                foreach (TreeNode n in trvLocations.Nodes)
                                    if (n.Checked == true)
                                    {
                                        string str = n.Tag.ToString();
                                        String[] _arrSpliter;
                                        _arrSpliter = str.Split('~');
                                        if (LocationIDs == "")
                                        {
                                            LocationIDs = _arrSpliter[0];
                                        }
                                        else
                                        {
                                            LocationIDs = LocationIDs + "~" + _arrSpliter[0];
                                        }
                                    }

                                    TemplateCount = Convert.ToInt32(GetAppointmentConflictTimeOverlapSet(
                                                                                    TemplateAllocationID,
                                                                                    Convert.ToInt64(oShortAppointment.ASCommonID),
                                                                                    Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                                                    gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                                                    gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()), _ClinicID,
                                                                                    Convert.ToDateTime(oShortAppointment.StartDate.ToString()),Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()),Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString()),LocationIDs));
                                    if (TemplateCount > 0)
                                    {

                                        if (_objSettingValue.ToString() == "U")
                                        {
                                            DialogResult dresult = MessageBox.Show("This appointment is longer than the existing template slot and overlaps other template slots. Would you like to use all of the template slots this appointment overlaps?." + "\n\n" + "Yes – Schedule all template slots overlapped by this appointment." + "\n\n" + "No – Only use the existing slot.  A conflict will be created in the appointment schedule.", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                            if (dresult == DialogResult.Cancel)
                                            {
                                                return;
                                            }
                                            else if (dresult == DialogResult.Yes)
                                            {
                                                UpdateOverlapAppointment = true;
                                            }
                                        }
                                        else if (_objSettingValue.ToString() == "Y")
                                        {
                                            UpdateOverlapAppointment = true;
                                        }
                                    }
                             }
                        }


                        if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                        {

                            if (UpdateOverlapAppointment)
                            {

                                // if modify the appointment time then it change the template status between original time period
                                if (oMasterAppointment.StartTime != oShortAppointment.StartTime || oMasterAppointment.Duration != (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes)
                                {
                                    if (oCutCopy != CutCopyPaste.Copy)
                                    {
                                    ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);
                                    }

                                }
                                else if (oMasterAppointment.ASBaseID != oShortAppointment.ASCommonID)
                                {
                                    if (oCutCopy != CutCopyPaste.Copy)
                                    {
                                        ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);
                                    }

                                }

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(TemplateAllocationID,
                                                            Convert.ToInt64(oShortAppointment.ASCommonID),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()),
                                                            Convert.ToDateTime(oShortAppointment.StartDate.ToString()), Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()), Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString())
                                                            , _ClinicID);
                            }
                            else if ((
                                (oMasterAppointment.StartTime != oShortAppointment.StartTime || oMasterAppointment.Duration != (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes)
                                        && TemplateCount == 0 && Convert.ToInt32(ApptFindCountOfOverlapTemplate(oShortAppointment.MasterID)) > 0
                                )
                                       // || oMasterAppointment.ASBaseID != oShortAppointment.ASCommonID
                                        )
                            {


                                // if modify the appointment time then it change the template status between original time period
                                //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                //{
                                if (oCutCopy != CutCopyPaste.Copy)
                                {
                                    ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);
                                }

                                //}

                                //updating AB_AppointmentTemplate_Allocation table if appointments overlaps other template
                                //appointment block then those block updates
                                //updating nIsRegistered = 2
                                ApptUpdateOverlapTemplate(TemplateAllocationID,
                                                            Convert.ToInt64(oShortAppointment.ASCommonID),
                                                            Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString())),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToShortTimeString()),
                                                            gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToShortTimeString()),
                                                            Convert.ToDateTime(oShortAppointment.StartDate.ToString()), Convert.ToDateTime(oShortAppointment.StartTime.ToShortTimeString()), Convert.ToDateTime(oShortAppointment.EndTime.ToShortTimeString())
                                                            , _ClinicID);
                            }
                            else if ((oMasterAppointment.StartTime != oShortAppointment.StartTime) && TemplateCount > 0)
                            {
                                // if modify the appointment time then it change the template status between original time period
                                //if (_SetAppointmentParameter.StartTime != dtpApp_DateTime_StartTime.Value || _SetAppointmentParameter.Duration != numApp_DateTime_Duration.Value)
                                //{
                                if (oCutCopy != CutCopyPaste.Copy)
                                {
                                    ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);
                                }

                            }
                            else if (oMasterAppointment.ASBaseID != oShortAppointment.ASCommonID)
                            {
                                if (oCutCopy != CutCopyPaste.Copy)
                                {
                                    ApptRemovePreviousOverlapTemplate(oShortAppointment.MasterID);
                                }

                            }

                        }
                    //}
                    #endregion



                        #region "Master Appointment Data"

                        //if Modify Appointment and 

                        _PrevmstAppID = oShortAppointment.MasterID;
                        _PrevdtlAppID = oShortAppointment.DetailID;
                        oMasterAppointment.MasterID = 0;


                        oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
                        oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;

                        _CheckValue = oMasterAppointment.AppointmentTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterAppointment.AppointmentTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterAppointment.AppointmentTypeDesc;// = cmbApp_AppointmentType.Text;

                        oMasterAppointment.ASBaseID = oShortAppointment.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        oMasterAppointment.ASBaseCode = oShortAppointment.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        oMasterAppointment.ASBaseDescription = oShortAppointment.ASCommonDescription; // cmbApp_Provider.Text;
                        oMasterAppointment.ASBaseFlag = oShortAppointment.ASCommonFlag;

                        _CheckValue = oMasterAppointment.ReferralProviderID;// = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        _CheckValue = oMasterAppointment.ReferralProviderCode; // = cmbApp_ReferralDoctor.Text; //Remark
                        _CheckValue = oMasterAppointment.ReferralProviderName; //= cmbApp_ReferralDoctor.Text;

                        _CheckValue = oMasterAppointment.PatientID; //  = Convert.ToInt64(txtApp_Patient.Tag.ToString());


                        oMasterAppointment.StartDate = oShortAppointment.StartDate;
                        oMasterAppointment.StartTime = oShortAppointment.StartTime;
                        oMasterAppointment.EndDate = oShortAppointment.EndDate;
                        oMasterAppointment.EndTime = oShortAppointment.EndTime;
                        oMasterAppointment.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;
                        _CheckValue = oMasterAppointment.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                        _CheckValue = oMasterAppointment.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterAppointment.LocationName; // = cmbApp_Location.Text;

                        _CheckValue = oMasterAppointment.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterAppointment.DepartmentName; // = cmbApp_Department.Text;

                        _CheckValue = oMasterAppointment.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterAppointment.ClinicID; // = _SetAppointmentParameter.ClinicID;
                        //oMasterAppointment.UsedStatus = oShortAppointment.UsedStatus;
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else if (oCutCopy == CutCopyPaste.Cut && gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }

                        #endregion

                        #region "Appointment Criteria"

                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterAppointment.ProblemTypes.Count; i++)
                        {
                            oMasterAppointment.ProblemTypes[i].MasterID = 0;
                            oMasterAppointment.ProblemTypes[i].DetailID = 0;
                            oMasterAppointment.ProblemTypes[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.ProblemTypes[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.ProblemTypes[i].LineNo = 0;
                            oMasterAppointment.ProblemTypes[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterAppointment.ProblemTypes[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.ProblemTypes[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.ProblemTypes[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.ProblemTypes[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.ProblemTypes[i].ViewOtherDetails = "";

                            // Commented by Pranit on 10 feb 2012 and changed status
                            // oMasterAppointment.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                            oMasterAppointment.ProblemTypes[i].UsedStatus = oMasterAppointment.UsedStatus;
                        }

                        #endregion

                        #region "Resources"

                        bool _isResourceOnly = false;
                        _isResourceOnly = IsResourceOnlyAppointment(oShortAppointment.MasterID);

                        if (_isResourceOnly && oMasterAppointment.Resources.Count == 1)
                        {
                            oMasterAppointment.Resources[0].MasterID = 0;
                            oMasterAppointment.Resources[0].DetailID = 0;
                            oMasterAppointment.Resources[0].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.Resources[0].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.Resources[0].LineNo = 0;
                            oMasterAppointment.Resources[0].ASFlag = oMasterAppointment.ASFlag;

                            oMasterAppointment.Resources[0].ASCommonID = oShortAppointment.ASCommonID;
                            oMasterAppointment.Resources[0].ASCommonCode = oShortAppointment.ASCommonCode;
                            oMasterAppointment.Resources[0].ASCommonDescription = oShortAppointment.ASCommonDescription;

                            oMasterAppointment.Resources[0].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.Resources[0].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.Resources[0].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.Resources[0].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.Resources[0].ColorCode; 

                            _CheckValue = oMasterAppointment.Resources[0].ClinicID; 
                            oMasterAppointment.Resources[0].ViewOtherDetails = "";
                            oMasterAppointment.Resources[0].UsedStatus = oMasterAppointment.UsedStatus;

                        }
                        else
                        {
                            for (int i = 0; i < oMasterAppointment.Resources.Count; i++)
                            {
                                oMasterAppointment.Resources[i].MasterID = 0;
                                oMasterAppointment.Resources[i].DetailID = 0;
                                oMasterAppointment.Resources[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                                oMasterAppointment.Resources[i].PatientID = oMasterAppointment.PatientID;
                                oMasterAppointment.Resources[i].LineNo = 0;
                                oMasterAppointment.Resources[i].ASFlag = oMasterAppointment.ASFlag;
                                _CheckValue = oMasterAppointment.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                _CheckValue = oMasterAppointment.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                                _CheckValue = oMasterAppointment.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                                oMasterAppointment.Resources[i].ASCommonFlag = ASBaseType.Resource;

                                oMasterAppointment.Resources[i].StartDate = oShortAppointment.StartDate;
                                oMasterAppointment.Resources[i].StartTime = oShortAppointment.StartTime;
                                oMasterAppointment.Resources[i].EndDate = oShortAppointment.EndDate;
                                oMasterAppointment.Resources[i].EndTime = oShortAppointment.EndTime;
                                _CheckValue = oMasterAppointment.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.Resources[i].ViewOtherDetails = "";
                            // Commented by Pranit on 10 feb 2012 and changed status
                            //oMasterAppointment.Resources[i].UsedStatus = ASUsedStatus.NotUsed;
                            oMasterAppointment.Resources[i].UsedStatus = oMasterAppointment.UsedStatus;

                            }
                        }

                        #endregion

                        #region " PA Transaction "

                        gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                        DataTable dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);
                        oSetting.Dispose();
                        oSetting = null;
                         oPATransaction = new PriorAuthorizationTransaction();

                        DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(oShortAppointment.MasterID, oShortAppointment.DetailID);

                        if (drPA != null)
                        {
                            oPATransaction.PriorAuthorizationID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
                            oPATransaction.PriorAuthorizationNo = Convert.ToString(drPA["sPriorAuthorizationNo"]);
                            oPATransaction.PatientID = oMasterAppointment.PatientID;

                            if (dtversion != null && dtversion.Rows.Count > 0)
                            { oPATransaction.Version = dtversion.Rows[0]["sSettingsValue"].ToString(); }
                            drPA = null;
                        }
                        if (dtversion != null)
                        {
                            dtversion.Dispose();
                            dtversion = null;
                        }
                        oPATransaction.MasterAppointmentID = oShortAppointment.MasterID;
                        oPATransaction.DetailAppointmentID = oShortAppointment.DetailID;
                        oPATransaction.IsSingleOccurance = oMasterAppointment.IsRecurrence.GetHashCode();
                        oPATransaction.IsDeleted = false;
                        oPATransaction.IsUpdated = false;

                        oMasterAppointment.PATransaction = oPATransaction;

                        #endregion

                        oMasterAppointment.Insurances = null;

                        // Code Start - Added by kanchan on 20091205 
                        // Region for getting HL7 message name is this action for S12/S13
                        if (oCutCopy == CutCopyPaste.Cut)
                        {
                            gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                        }
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                        }
                        // Code End - Added by kanchan on 20091205 

                        //code added by dipak 20091
                        //for paste appoinment on template


                        if (isTemplate == true)
                        {
                            Int64 TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                            TemplateAllocationID = 0;
                            TemplateAllocationID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); 
                            Int64 LineNumber = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                            // Added new AppointmentType and AppointmentTypeDesc
                            DataTable dtPasteAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                            if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                            {
                                oMasterAppointment.AppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                oMasterAppointment.AppointmentTypeDesc = dtPasteAppointmentType.Rows[0]["AppointmentName"].ToString();
                                oMasterAppointment.ColorCode = Convert.ToInt32(dtPasteAppointmentType.Rows[0]["AppointmentColor"].ToString());
//                                dtPasteAppointmentType.Dispose();
                            }
                            if (dtPasteAppointmentType != null)
                            {
                                dtPasteAppointmentType.Dispose();
                                dtPasteAppointmentType = null;
                            }

                            if (oCutCopy == CutCopyPaste.Cut)
                            {
                                if (oShortAppointment.IsRecurrence != gloAppointmentScheduling.SingleRecurrence.Recurrence)
                                {
                                    gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                                    bool _result;
                                    _result = ogloAppointment.Update_TVP(oMasterAppointment, SingleRecurrence.Single, oShortAppointment.MasterID, oShortAppointment.DetailID, _ClinicID, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                                    _retValue = oShortAppointment.MasterID;
                                }
                                else
                                {
                                    _retValue = ogloAppointment.Add(oMasterAppointment, AppointmentScheduleFlag.TemplateAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                                    isTemplate = false;
                                }
                            }
                            else
                            {
                                _retValue = ogloAppointment.Add(oMasterAppointment, AppointmentScheduleFlag.TemplateAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                                isTemplate = false;
                            }
                        }
                        else
                        {
                            if (oCutCopy == CutCopyPaste.Cut)
                            {
                                if (oShortAppointment.IsRecurrence != gloAppointmentScheduling.SingleRecurrence.Recurrence)
                                {
                                    gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                                    bool _result;
                                    _result = ogloAppointment.Update_TVP(oMasterAppointment, SingleRecurrence.Single, oShortAppointment.MasterID, oShortAppointment.DetailID, _ClinicID);
                                    _retValue = oShortAppointment.MasterID;
                                }
                                else
                                {
                                    gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                                    _retValue = ogloAppointment.Add(oMasterAppointment);
                                }
                            }
                            else
                            {
                                gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                                _retValue = ogloAppointment.Add(oMasterAppointment);
                            }
                        }

                        if (_retValue > 0)
                        {
                            if (oCutCopy == CutCopyPaste.Cut)
                            {
                                //--Free Template slot 
                              //  oDB.Connect(false);
                              //  string _sqlQuery = " UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered = 0 "
                              //+ " FROM AS_Appointment_DTL INNER JOIN AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID "
                              //+ " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = AS_Appointment_DTL.nTemplateAllocationID and  nIsRegistered = 1"
                              //+ " AND AS_Appointment_DTL.nDTLAppointmentID = " + oShortAppointment.DetailID + " AND AS_Appointment_DTL.nMSTAppointmentID = " + oShortAppointment.MasterID + " AND  AS_Appointment_DTL.nClinicID = " + _ClinicID + "  ";
                              //  oDB.ExecuteScalar_Query(_sqlQuery);
                              //  oDB.Disconnect();

                                bool _IsDeleted = false;
                                if (oShortAppointment.IsRecurrence == SingleRecurrence.Recurrence || oShortAppointment.IsRecurrence == SingleRecurrence.SingleInRecurrence)
                                {
                                    _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, false);

                                    if (_IsDeleted)
                                    {
                                        if (gloHL7.boolSendAppointmentDetails)
                                        {
                                            gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, oShortAppointment.MasterID, "", _databaseconnectionstring, true);
                                        }
                                    }
                                }
                                else if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    //_IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, false);
                                    //if (_IsDeleted)
                                    //{
                                    //    if (gloHL7.boolSendAppointmentDetails)
                                    //    {
                                    //        gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, oShortAppointment.MasterID, "", _databaseconnectionstring, true);
                                    //    }
                                    //}
                                }
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment modified", oMasterAppointment.PatientID , _retValue, oMasterAppointment.ASBaseID, ActivityOutCome.Success);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Add, "Appointment added", oMasterAppointment.PatientID, _retValue, oMasterAppointment.ASBaseID, ActivityOutCome.Success);
                            }
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oMasterAppointment != null) { oMasterAppointment.Dispose(); oMasterAppointment = null; }
                if (_CheckValue != null) { _CheckValue = null; }
                if (ogloAppointment != null) { ogloAppointment.Dispose(); ogloAppointment = null; }
               // if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

                if (_AppointmentDates != null)
                {
                    _AppointmentDates.Dispose();
                    _AppointmentDates=null;
                }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
                if (oPATransaction != null)
                {
                    oPATransaction.Dispose();
                    oPATransaction = null;
                }
            }
        }

        public DataTable ResourseName(Int64 ProviderID, DateTime StartTime, DateTime EndTime, DateTime AppoinmentDate, String Location)
        {
           

            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }


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

               
                string splitDate = AppoinmentDate.ToShortDateString() + " " + StartTime.ToShortTimeString();
                DateTime dateTime =  Convert.ToDateTime(splitDate);

                DateTime newDateTime =  dateTime.AddMinutes((int)duration);


                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Flag", 2, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                DataTable dt = null;
                oDB.Retrive("AS_BlockedSlots", oDBParameters, out  dt);
                oDB.Disconnect();

              

                return dt;

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }


        public object GetAppointmentConflictTimeOverlapSet(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, Int64 ClinicID,DateTime Startdate,DateTime StartTime,DateTime EndTime, String LocationIDs = "")
        {
            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }
          
            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

            
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            DateTime dateTime  = Convert.ToDateTime(splitDate);

            DateTime newDateTime  = dateTime.AddMinutes((int)duration);
           
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


        public object GetAppointmentConflictTimeOverlapSetTemplate(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, Int64 nAppointmentTypeID, Int64 ClinicID,DateTime Startdate, DateTime StartTime, DateTime EndTime, String LocationIDs = "")
        {
            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }
            
            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

           
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            DateTime dateTime   = Convert.ToDateTime(splitDate);

            DateTime newDateTime  = dateTime.AddMinutes((int)duration);
           
            
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
            oDBParameters.Add("@AppointmentType", nAppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@LocationIDs", LocationIDs, ParameterDirection.Input, SqlDbType.Text);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);

            oDB.Execute("GetAppointmentConflictTimeOverlapSetTemplate", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            return Count;
        }


        public void ApptUpdateOverlapTemplate(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, DateTime Startdate, DateTime StartTime, DateTime EndTime, Int64 ClinicID)
        {
            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }
           
            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

           
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            DateTime dateTime =   Convert.ToDateTime(splitDate);

            DateTime newDateTime  = dateTime.AddMinutes((int)duration);
            
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

        private void MakePasteAppointmentResourceOnly(ShortApointmentSchedule oShortAppointment, ShortApointmentSchedule oShortResource, CutCopyPaste oCutCopy)
        {

            gloAppointmentScheduling.gloAppointment ogloAppointment = null;
            gloAppointmentScheduling.MasterAppointment oMasterAppointment = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ShortApointmentSchedule oNewResource = null;
            PriorAuthorizationTransaction oPATransaction = null;
            Int64 _retValue = 0;
            object _CheckValue = null;

            try
            {
                if (oShortAppointment != null)
                {
                    ogloAppointment = new gloAppointment(_databaseconnectionstring);
               
                    oMasterAppointment = ogloAppointment.GetMasterAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);

                    if (oMasterAppointment != null)
                    {
                        //Make new single entry for the appointment

                        #region "Master Appointment Data"

                        //if Modify Appointment and 

                        oMasterAppointment.MasterID = 0;
                        oMasterAppointment.AppointmentStatusID = 1; //"registered"
                        oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
                        oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;

                        _CheckValue = oMasterAppointment.AppointmentTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterAppointment.AppointmentTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterAppointment.AppointmentTypeDesc;// = cmbApp_AppointmentType.Text;

                        oMasterAppointment.ASBaseID = oShortAppointment.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        oMasterAppointment.ASBaseCode = oShortAppointment.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        oMasterAppointment.ASBaseDescription = oShortAppointment.ASCommonDescription; // cmbApp_Provider.Text;
                        oMasterAppointment.ASBaseFlag = oShortAppointment.ASCommonFlag;

                        _CheckValue = oMasterAppointment.ReferralProviderID;// = Convert.ToInt64(cmbApp_ReferralDoctor.SelectedValue);
                        _CheckValue = oMasterAppointment.ReferralProviderCode; // = cmbApp_ReferralDoctor.Text; //Remark
                        _CheckValue = oMasterAppointment.ReferralProviderName; //= cmbApp_ReferralDoctor.Text;

                        _CheckValue = oMasterAppointment.PatientID; //  = Convert.ToInt64(txtApp_Patient.Tag.ToString());


                        oMasterAppointment.StartDate = oShortAppointment.StartDate;
                        oMasterAppointment.StartTime = oShortAppointment.StartTime;
                        oMasterAppointment.EndDate = oShortAppointment.EndDate;
                        oMasterAppointment.EndTime = oShortAppointment.EndTime;
                        oMasterAppointment.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;
                        _CheckValue = oMasterAppointment.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                        _CheckValue = oMasterAppointment.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterAppointment.LocationName; // = cmbApp_Location.Text;

                        _CheckValue = oMasterAppointment.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterAppointment.DepartmentName; // = cmbApp_Department.Text;

                        _CheckValue = oMasterAppointment.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterAppointment.ClinicID; // = _SetAppointmentParameter.ClinicID;
                        
                        // Commented by pranit on 10 feb 2012
                        // oMasterAppointment.UsedStatus = oShortAppointment.UsedStatus;

                        // Done by pranit on 10 feb 2011
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        else if (oCutCopy == CutCopyPaste.Cut && gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                        {
                            oMasterAppointment.UsedStatus = ASUsedStatus.Registred;
                            oMasterAppointment.AppointmentStatusID = 1;
                        }
                        // End by pranit on 10 feb 2011

                        #endregion

                        #region "Appointment Criteria"

                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.StartDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.StartTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortAppointment.EndDate.ToString("MM/dd/yyyy"));
                        oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortAppointment.EndTime.ToString("hh:mm tt"));
                        oMasterAppointment.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortAppointment.EndTime.TimeOfDay.Subtract(oShortAppointment.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterAppointment.ProblemTypes.Count; i++)
                        {

                            oMasterAppointment.ProblemTypes[i].MasterID = 0;
                            oMasterAppointment.ProblemTypes[i].DetailID = 0;
                            oMasterAppointment.ProblemTypes[i].IsRecurrence = oMasterAppointment.IsRecurrence;
                            oMasterAppointment.ProblemTypes[i].PatientID = oMasterAppointment.PatientID;
                            oMasterAppointment.ProblemTypes[i].LineNo = 0;
                            oMasterAppointment.ProblemTypes[i].ASFlag = oMasterAppointment.ASFlag;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterAppointment.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterAppointment.ProblemTypes[i].StartDate = oShortAppointment.StartDate;
                            oMasterAppointment.ProblemTypes[i].StartTime = oShortAppointment.StartTime;
                            oMasterAppointment.ProblemTypes[i].EndDate = oShortAppointment.EndDate;
                            oMasterAppointment.ProblemTypes[i].EndTime = oShortAppointment.EndTime;
                            _CheckValue = oMasterAppointment.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterAppointment.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterAppointment.ProblemTypes[i].ViewOtherDetails = "";
                            // oMasterAppointment.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                            oMasterAppointment.ProblemTypes[i].UsedStatus = oMasterAppointment.UsedStatus;
 
                        }

                        #endregion

                        #region "Resources"

                         oNewResource = new ShortApointmentSchedule();

                        oNewResource.MasterID = 0;
                        oNewResource.DetailID = 0;
                        oNewResource.IsRecurrence = oMasterAppointment.IsRecurrence;
                        oNewResource.PatientID = oMasterAppointment.PatientID;
                        oNewResource.LineNo = 0;
                        oNewResource.ASFlag = oMasterAppointment.ASFlag;

                        oNewResource.ASCommonID = oShortResource.ASCommonID;
                        oNewResource.ASCommonCode = oShortResource.ASCommonCode;
                        oNewResource.ASCommonDescription = oShortResource.ASCommonDescription;
                        oNewResource.ASCommonFlag = oShortResource.ASCommonFlag;
                        oNewResource.StartDate = oShortResource.StartDate;
                        oNewResource.StartTime = oShortResource.StartTime;
                        oNewResource.EndDate = oShortResource.EndDate;
                        oNewResource.EndTime = oShortResource.EndTime;
                        oNewResource.ClinicID = _ClinicID;
                        //oNewResource.UsedStatus = ASUsedStatus.NotUsed;
                        oNewResource.UsedStatus = oMasterAppointment.UsedStatus;

                        oMasterAppointment.Resources.Clear();
                        oMasterAppointment.Resources.Add(oNewResource);

                        #endregion

                        #region " PA Transaction "

                        gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                        DataTable dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);
                        oSetting.Dispose();
                        oSetting = null;

                         oPATransaction = new PriorAuthorizationTransaction();

                        DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(oShortAppointment.MasterID, oShortAppointment.DetailID);

                        if (drPA != null)
                        {
                            oPATransaction.PriorAuthorizationID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
                            oPATransaction.PriorAuthorizationNo = Convert.ToString(drPA["sPriorAuthorizationNo"]);
                            oPATransaction.PatientID = oMasterAppointment.PatientID;

                            if (dtversion != null && dtversion.Rows.Count > 0)
                            { oPATransaction.Version = dtversion.Rows[0]["sSettingsValue"].ToString(); }
                            drPA = null;
                           // dtversion.Dispose();
                        }
                        if (dtversion != null)
                        {
                            dtversion.Dispose();
                            dtversion = null;
                        }
                        oPATransaction.MasterAppointmentID = oShortAppointment.MasterID;
                        oPATransaction.DetailAppointmentID = oShortAppointment.DetailID;
                        oPATransaction.IsSingleOccurance = oMasterAppointment.IsRecurrence.GetHashCode();
                        oPATransaction.IsDeleted = false;
                        oPATransaction.IsUpdated = false;

                        oMasterAppointment.PATransaction = oPATransaction;

                        #endregion

                        oMasterAppointment.Insurances = null;

                        // Code Start - Added by kanchan on 20091205 
                        // Region for getting HL7 message name is this action for S12/S13
                        if (oCutCopy == CutCopyPaste.Cut)
                        {
                            gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update;
                        }
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add;
                        }
                        // Code End - Added by kanchan on 20091205 

                        //code added by dipak 20091
                        //for paste appoinment on template
                        if (isTemplate == true)
                        {
                            Int64 TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                            Int64 TemplateAllocationID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                            Int64 LineNumber = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                            // Added new AppointmentType and AppointmentTypeDesc
                            DataTable dtPasteAppointmentType = GetAppointmentTypeDetailsUsingAllocationID(TemplateAllocationMasterID, TemplateAllocationID);
                            if (dtPasteAppointmentType != null && dtPasteAppointmentType.Rows.Count > 0)
                            {
                                oMasterAppointment.AppointmentTypeID = Convert.ToInt64(dtPasteAppointmentType.Rows[0]["AppointmentTypeID"].ToString());
                                oMasterAppointment.AppointmentTypeDesc = dtPasteAppointmentType.Rows[0]["AppointmentName"].ToString();
                                oMasterAppointment.ColorCode = Convert.ToInt32(dtPasteAppointmentType.Rows[0]["AppointmentColor"].ToString());
                               // dtPasteAppointmentType.Dispose();
                            }
                            if (dtPasteAppointmentType != null)
                            {
                                dtPasteAppointmentType.Dispose();
                                dtPasteAppointmentType = null;
                            }
                            _retValue = ogloAppointment.Add(oMasterAppointment, AppointmentScheduleFlag.TemplateAppointment, TemplateAllocationMasterID, TemplateAllocationID, LineNumber);
                            isTemplate = false;
                        }
                        else
                        {
                            _retValue = ogloAppointment.Add(oMasterAppointment);
                        }

                        if (_retValue > 0)
                        {
                            if (oCutCopy == CutCopyPaste.Cut)
                            {
                                bool _IsDeleted = false;
                                if (oShortAppointment.IsRecurrence == SingleRecurrence.Recurrence || oShortAppointment.IsRecurrence == SingleRecurrence.SingleInRecurrence)
                                {
                                    _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, false);
                                }
                                else if (oShortAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    _IsDeleted = DeleteAppointment(oShortAppointment.MasterID, oShortAppointment.DetailID, true);
                                }

                                oDB.Connect(false);
                                string _sqlQuery = " UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered = 0 "
                              + " FROM AS_Appointment_DTL INNER JOIN AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID "
                              + " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = AS_Appointment_DTL.nTemplateAllocationID "
                              + " AND AS_Appointment_DTL.nDTLAppointmentID = " + oShortAppointment.DetailID + " AND AS_Appointment_DTL.nMSTAppointmentID = " + oShortAppointment.MasterID + " AND  AS_Appointment_DTL.nClinicID = " + _ClinicID + "  ";
                                oDB.ExecuteScalar_Query(_sqlQuery);
                                oDB.Disconnect();
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Appointment modified", oShortAppointment.PatientID , _retValue, oShortAppointment.ASCommonID , ActivityOutCome.Success);
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Add, "Appointment added", oShortAppointment.PatientID, _retValue, oShortAppointment.ASCommonID , ActivityOutCome.Success);
                            }
                        }
                    }

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oMasterAppointment != null) { oMasterAppointment.Dispose(); oMasterAppointment = null; }
                if (_CheckValue != null) { _CheckValue = null; }
                if (ogloAppointment != null) { ogloAppointment.Dispose(); ogloAppointment = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oNewResource != null)
                {
                    oNewResource.Dispose();
                    oNewResource = null;
                }
                if (oPATransaction != null)
                {
                    oPATransaction.Dispose();
                    oPATransaction=null;
                }
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
                    _sqlQuery = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + this._ClinicID + " ";// AND (nUsedStatus <> 4)";
                    oDB.Execute_Query(_sqlQuery);

                    //if all detail appointments are deleted then delete master table entry 
                    _sqlQuery = "SELECT COUNT(*) FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + this._ClinicID + "";
                    object oDetailsAppCount = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oDetailsAppCount != null && Convert.ToString(oDetailsAppCount) != "")
                    {
                        if (Convert.ToInt32(oDetailsAppCount) == 0)
                        {
                            _sqlQuery = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND nClinicID =" + this._ClinicID + " ";
                            oDB.Execute_Query(_sqlQuery);
                        }
                    }
                    _retValue = true;
                }
                else
                {
                    //1.Delete the Details Table entry for the Appointment
                    //_sqlQuery = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND (nDTLAppointmentID = " + AppointmentDetailId + " OR nRefID = " + AppointmentDetailId + ") AND nClinicID =" + this._ClinicID + " ";

                    //Added by Amit
                    //updating old used status to 7 appointment in case of drag-Drop and cut-Paste

                    _sqlQuery = "update AS_Appointment_DTL set nUsedStatus = 7 WHERE nMSTAppointmentID = " + MasterAppointmentId + " AND (nDTLAppointmentID = " + AppointmentDetailId + " OR nRefID = " + AppointmentDetailId + ") AND nClinicID =" + this._ClinicID + " ";
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

        #endregion

        #region " Context Menu Item Click Event "

        private void FillMarkAsStatus()
        {

            gloAppointmnetScheduleCommon oApptCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);
            try
            {
                ToolStripItem ocmnu_Status = new ToolStripMenuItem();
                ocmnu_Status.Text = "No show";
                ocmnu_Status.Tag = "5";
                ocmnu_Status.Click += new EventHandler(ocmnu_MarkASStatus_Click);
                cmnu_Appointment_MarkAs.DropDownItems.Add(ocmnu_Status);
                ocmnu_Status = null;

                ocmnu_Status = new ToolStripMenuItem();
                ocmnu_Status.Text = "Cancel";
                ocmnu_Status.Tag = "6";
                ocmnu_Status.Click += new EventHandler(ocmnu_MarkASStatus_Click);
                cmnu_Appointment_MarkAs.DropDownItems.Add(ocmnu_Status);
                ocmnu_Status = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oApptCommon != null) { oApptCommon.Dispose(); }
            }

        }

        void ocmnu_MarkASStatus_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            string _AppStatus = "";
            ShortApointmentSchedule oShortAppointment = new ShortApointmentSchedule();
            try
            {
                if (((ToolStripItem)sender).Tag != null)
                {
                    Int32 _nAppointmentStatusID = Convert.ToInt32(((ToolStripItem)sender).Tag);
                    if (_LastSelectedAppointment != null)
                    {
                        AppointmentScheduleFlag _AppORTemp = AppointmentScheduleFlag.None;
                        _AppORTemp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 4));

                        if (_AppORTemp == AppointmentScheduleFlag.Appointment || _AppORTemp == AppointmentScheduleFlag.TemplateAppointment)
                        {
                            Int64 nMasterAppointmentID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 1).ToString()); ;
                            Int64 nDetailAppointmentID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 2).ToString()); ;

                            if (ogloAppointment.IsPatientCheckOut(nMasterAppointmentID, nDetailAppointmentID) == true)
                            {
                                _PatientName = ogloAppointment.GetAppointmentPatientName(nMasterAppointmentID);
                                if (_nAppointmentStatusID == 5)
                                {
                                    _AppStatus = "marked as No Show";
                                }
                                if (_nAppointmentStatusID == 6)
                                {
                                    _AppStatus = "marked as Cancel";
                                }
                                if (MessageBox.Show("Patient '" + _PatientName + "' is already checked out. Are you sure you wish to " + _AppStatus + " this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {

                                }
                                else
                                {
                                    return;
                                }

                            }

                            frmAddNotes ofrmAddNotes = new frmAddNotes(_databaseconnectionstring);
                            ofrmAddNotes.NotesPrefix = " " + ((ToolStripItem)sender).Text + " Notes :";
                            ofrmAddNotes.MSTAppointmentID = nMasterAppointmentID;
                            ofrmAddNotes.DTLAppointmentID = nDetailAppointmentID;
                            ofrmAddNotes.nAppointmentStatusID = _nAppointmentStatusID;
                            
                            ofrmAddNotes.ShowDialog(this);
                            if (ofrmAddNotes.DialogResult == DialogResult.OK)
                            {
                                //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                                long _nPatientID = 0;
                                Object result = null;
                                result = ogloAppointment.GetPatientId(nMasterAppointmentID);
                                if (result != null)
                                {
                                    _nPatientID = Convert.ToInt64(result);
                                }
                                long _nProviderID = 0;
                                using (DataTable dtPatient = ogloAppointment.GetAppointmentProviderID(nMasterAppointmentID))
                                {
                                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                                    {
                                        _nProviderID = Convert.ToInt64(dtPatient.Rows[0]["nASBaseID"]);
                                    }
                                }
                                ogloAppointment.UpdateAppointmentUsedStatus(nMasterAppointmentID, nDetailAppointmentID, _nAppointmentStatusID);
                                if (_nAppointmentStatusID == 5)
                                {
                                    //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.None, "Appointment marked as No Show", _nPatientID, nMasterAppointmentID , _nProviderID , ActivityOutCome.Success);
                                }
                                else if (_nAppointmentStatusID == 6)
                                {
                                    //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SetupAppointment, ActivityType.None, "Appointment marked as Cancel", _nPatientID, nMasterAppointmentID, _nProviderID, ActivityOutCome.Success);



                                    #region "Generate HL7 Message Queue for New Appointment"
                                    //Code Added by Abhijeet for generating appointment cancle entry in HL7 message queue
                                    if (gloHL7.boolSendAppointmentDetails)  // (_GenerateHL7Message)
                                    {
                                        if (nMasterAppointmentID > 0)
                                        {
                                            DataTable dtpatient = ogloAppointment.GetPatient(nMasterAppointmentID, nDetailAppointmentID);
                                            if (dtpatient != null)
                                            {
                                                long nPatientId = Convert.ToInt64(dtpatient.Rows[0]["nPatientID"]);
                                                if (nPatientId > 0)
                                                {
                                                    gloHL7.InsertInMessageQueue("S15", nPatientId, nMasterAppointmentID, nDetailAppointmentID.ToString(), _databaseconnectionstring);
                                                }
                                                if (dtpatient != null)
                                                {
                                                    dtpatient.Dispose();
                                                    dtpatient = null;
                                                }
                                            }

                                        }
                                    }
                                    //End of Code Added by Abhijeet for generating appointment cancle entry in HL7 message queue
                                    #endregion
                                }
                            }


                            ofrmAddNotes.Dispose();
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
                _LastSelectedAppointment = null;
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                }
                FillAppointments(_ShowTemplateAppointment);
            }
        }

        //Fill Location to Context Menu for Template Appointment
        private void FillRegPatientLocations()
        {
            try
            {
                gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
                DataTable dt = oLocation.GetList();
                //nLocationID, sLocation, nClinicID 
                oLocation.Dispose();
                oLocation = null;

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ToolStripItem ocmnu_Location = new ToolStripMenuItem();
                        ocmnu_Location.Text = Convert.ToString(dt.Rows[i]["sLocation"]);
                        ocmnu_Location.Tag = Convert.ToString(dt.Rows[i]["nLocationID"]);
                        ocmnu_Location.Click += new EventHandler(ocmnu_RegisterPatient_Click);
                        cmnu_Appointment_RegisterPatient.DropDownItems.Add(ocmnu_Location);

                        ocmnu_Location = null;
                    }
                    dt.Dispose();
                    dt=null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        void ocmnu_RegisterPatient_Click(object sender, EventArgs e)
        {
            try
            {

                if (((ToolStripItem)sender).Tag != null)
                {
                    RegisterPatientSaveAppointment(((ToolStripItem)sender).Text, Convert.ToInt64(((ToolStripItem)sender).Tag.ToString()));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                juc_Appointment.ContextMenu = null;
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
        }

        private void cmnu_AppointmentEdit_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
           
            gloSecurity.gloSecurity oSecurity = null;
            frmSetupAppointment oSetupAppointment = null;
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            try
            {
                switch (e.ClickedItem.Text)
                {
                    case "Copy":
                        {
                            #region " Copy Appointment "

                            //1.Copy the selected Appointment object in context menu tag property
                            cmnu_AppointmentEdit.Hide();
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());

                                // GLO2011-0011970 
                                // If status is legal pending restrict user to cut/copy appointment
                                if (ogloAppointment.IsLegalPending(nMSTAppointmentID, nDTLAppointmentID))
                                {
                                    MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }

                                if (IsMultiResourceAppointment(nMSTAppointmentID) == true)
                                {
                                    MessageBox.Show(" Copy is disabled for Multi-resource appointments. Try the Edit option.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmnu_Appointment_Copy.Tag = null;
                                    cmnu_Appointment_Cut.Tag = null;
                                    break;
                                }

                                cmnu_Appointment_Copy.Tag = juc_Appointment.CurrentAppointment;
                                cmnu_Appointment_Paste.Tag = CutCopyPaste.Copy;
                                cmnu_Appointment_Cut.Tag = null;
                            }

                            #endregion
                        }
                        break;
                    case "Open":
                        {
                            cmnu_AppointmentEdit.Hide();
                            _isMessageDisplayed = false;
                            tsb_ModifyAppointment_Click(null, null);
                        }
                        break;
                    case "Print":
                        {
                            #region " Print Appointment "

                            Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));
                            Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2));

                            if (_TempMasterID > 0 && _TempDetailID > 0)
                            {
                            }
                            #endregion
                        }
                        break;
                    case "Delete":
                        {
                            cmnu_AppointmentEdit.Hide();
                            tsb_Delete_Click(null, null);
                        }
                        break;
                    case "Add Notes":
                        {
                            cmnu_AppointmentEdit.Hide();
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());

                                frmAddNotes ofrmAddNotes = new frmAddNotes(_databaseconnectionstring);
                                ofrmAddNotes.MSTAppointmentID = nMSTAppointmentID;
                                ofrmAddNotes.DTLAppointmentID = nDTLAppointmentID;
                                ofrmAddNotes.ShowDialog(this);
                                ofrmAddNotes.Dispose();
                            }
                        }
                        break;

                    case "View History":
                        {
                            cmnu_AppointmentEdit.Hide();
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                frmViewApptHistory ofrmViewAppointmentHistory = new frmViewApptHistory(_databaseconnectionstring);
                                ofrmViewAppointmentHistory.AppointmentID = nMSTAppointmentID;
                                ofrmViewAppointmentHistory.ShowDialog(this);
                                ofrmViewAppointmentHistory.Dispose();
                            }

                        }
                        break;
                    case "Cut":
                        {
                            #region " Cut Appointment "

                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                cmnu_AppointmentEdit.Hide();
                                //gloPatient.gloPatient ogloPatient = null;
                                //ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                                //if (ogloPatient != null)
                                //{
                                //    ogloPatient.Dispose();
                                //    ogloPatient = null;
                                //}
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());

                                // GLO2011-0011970 
                                // If status is legal pending restrict user to cut/copy appointment
                                if (ogloAppointment.IsLegalPending(nMSTAppointmentID, nDTLAppointmentID))
                                {
                                    MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }

                                _PatientName = ogloAppointment.GetAppointmentPatientName(nMSTAppointmentID);
                                if (IsMultiResourceAppointment(nMSTAppointmentID) == true)
                                {
                                    MessageBox.Show(" Cut is disabled for Multi-resource appointments. Try the Edit option.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmnu_Appointment_Copy.Tag = null;
                                    cmnu_Appointment_Cut.Tag = null;
                                    break;
                                }

                                SingleRecurrence IsRecurrence = (SingleRecurrence)(Convert.ToInt32(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3)));
                                if (IsRecurrence == SingleRecurrence.Recurrence || IsRecurrence == SingleRecurrence.SingleInRecurrence)
                                {
                                    //if (MessageBox.Show(" This is Recurrence Appointment. Do you want to modify it ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    //{
                                    //    break;
                                    //}
                                }
                                else
                                {
                                    if (ogloAppointment.IsPatientCheckOut(nMSTAppointmentID, nDTLAppointmentID) == true)
                                    {
                                        if (MessageBox.Show("Patient '" + _PatientName + "' is already checked out.  Are you sure you wish to modify this appointment?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }

                                cmnu_Appointment_Cut.Tag = juc_Appointment.CurrentAppointment;
                                cmnu_Appointment_Paste.Tag = CutCopyPaste.Cut;
                                cmnu_Appointment_Copy.Tag = null;
                            }

                            #endregion
                        }
                        break;
                    case "Mail To Patient":
                        {

                        }
                        break;
                    case "New Appointment":
                        {

                            juc_Appointment.ContextMenuStrip.Hide();
                            //RegAppUsingTemplateOnly Setting is true then allow to 
                            //create Appointemnts using template only
                            if (_RegAppUsingTemplateOnly == true)
                            {
                                cmnu_AppointmentEdit.Hide();
                                Application.DoEvents();
                                MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //New Appointment
                            ASBaseType _OwnerType = ASBaseType.None;
                            Int64 _OwnerID = 0;
                            Int64 _OwnerTypeID = 0;
                            string _OwnerName = "";
                       
                            bool isBlocked = false;
                            
                            if (juc_Appointment.GetOwnerAt(_JanusPastePoint) != null)
                            {
                                _OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                                _OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 1));
                                _OwnerTypeID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                                _OwnerName = Convert.ToString(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 2));
                               
                            }
                             oSecurity = new gloSecurity.gloSecurity(_databaseconnectionstring);
                            if (oSecurity.isPatientLock(_PatientD, false))
                            {
                                juc_Appointment.ContextMenuStrip.Hide();
                                juc_Appointment.Refresh();
                                //Bug #81090: 00000879: deceased patient status
                                MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                return;
                            }
                            //end of code
                           oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);


                            #region "Set Appointment Parameters"
                            oSetupAppointment.SetAppointmentParameters.MasterAppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.AppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.ClinicID = _ClinicID;
                            oSetupAppointment.SetAppointmentParameters.ProviderID = _OwnerID;
                            oSetupAppointment.SetAppointmentParameters.ProviderName = _OwnerName;
                            oSetupAppointment.SetAppointmentParameters.AddTrue_ModifyFalse_Flag = true; // Add - true, Modify - false
                            oSetupAppointment.SetAppointmentParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifySingleAppointmentFromReccurence = false;
                            oSetupAppointment.SetAppointmentParameters.StartDate = juc_Appointment.GetDateTimeAt(_JanusPastePoint);// .GetDateAt();
                            
                            //ADDED BY SHUBHANGI 20100717 TO SET THE TIME INTERVAL FOR THE SELECTED AREA
                            oSetupAppointment.SetAppointmentParameters.StartTime = Convert.ToDateTime(juc_Appointment.SelectedDays.Start);//Convert.ToDateTime( juc_Appointment.GetDateTimeAt().TimeOfDay.ToString());//Convert.ToDateTime(string.Format(juc_Appointment.GetDateTimeAt().ToShortTimeString(),"hh:mm:tt"));//Convert.ToDateTime(string.Format(juc_Appointment.GetDateAt().ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(juc_Appointment.GetTimeAt().ToString(), "hh:mm tt"));

                            //ADDED BY SHUBHANGI 20100717 TO SET THE TIME INTERVAL FOR THE SELECTED AREA
                            oSetupAppointment.SetAppointmentParameters.Duration = Convert.ToDecimal((juc_Appointment.SelectedDays.End - juc_Appointment.SelectedDays.Start).TotalMinutes);
                            oSetupAppointment.SetAppointmentParameters.LoadParameters = true;
                            oSetupAppointment.SetAppointmentParameters.ShowTemplateAppointment_Flag = _ShowTemplateAppointment;
                            oSetupAppointment.SetAppointmentParameters.PatientID = _PatientD;
                            string LocationName = "";
                            bool SelectedLocationCount = false;

                            foreach (TreeNode n in trvLocations.Nodes)
                                if (n.Checked == true)
                                {
                                    SelectedLocationCount = true;
                                    if (LocationName == "")
                                    { LocationName = n.Text.ToString(); }
                                    else
                                    { LocationName = LocationName + "~" + n.Text.ToString(); }
                                }

                            if (SelectedLocationCount)
                            {
                                oSetupAppointment.SetAppointmentParameters.Location = GetSelectedLocation(LocationName);
                            }
            
                            foreach (TreeNode n in trvLocations.Nodes)
                                if (n.Checked == true)
                                {
                                    string str = n.Tag.ToString();
                                    String[] _arrSpliter;
                                    _arrSpliter = str.Split('~');
                                    if (oSetupAppointment.SetAppointmentParameters.LocationIDs == "")
                                    {
                                        oSetupAppointment.SetAppointmentParameters.LocationIDs = _arrSpliter[0];
                                    }
                                    else
                                    {
                                        oSetupAppointment.SetAppointmentParameters.LocationIDs = oSetupAppointment.SetAppointmentParameters.LocationIDs + "~" + _arrSpliter[0];
                                    }
                                }

                            #endregion
                            if (_OwnerID > 0)
                            {
                                isBlocked = ogloAppointment.BlockedSlots(_OwnerID, juc_Appointment.SelectedDays.Start, juc_Appointment.SelectedDays.End, oSetupAppointment.SetAppointmentParameters.StartDate, LocationName);
                            }
                            if (isBlocked == true)
                            {
                                
                                oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                                oClsgloUserRights.CheckForUserRights(_UserName);
                                DataTable dt = null;
                                isBlocked = false;
                                dt = ogloAppointment.ResourseName(_OwnerID, juc_Appointment.SelectedDays.Start, juc_Appointment.SelectedDays.End, oSetupAppointment.SetAppointmentParameters.StartDate, LocationName);

                                if (dt.Rows.Count >= 1 && dt != null)
                                {
                                    if (oClsgloUserRights.OverrideProviderBlockSchedule == false)
                                    {
                                        MessageBox.Show("Schedule is blocked for the provider. Appointment cannot be created. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dt.Dispose();
                                        dt = null;
                                        return;
                                    }
                                    else
                                    {
                                        cmnu_AppointmentNew.Visible = false;
                                        if (DialogResult.No == MessageBox.Show("Schedule for " + dt.Rows[0]["sASBaseDesc"] + " is blocked from " + dt.Rows[0]["dtStarttime"] + " to " + dt.Rows[0]["dtEndtime"] + "." + Environment.NewLine + "Create this " + juc_Appointment.SelectedDays.Start.ToShortTimeString() + " - " + juc_Appointment.SelectedDays.End.ToShortTimeString() + " appointment? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                                        {
                                            dt.Dispose();
                                            dt = null;
                                            return; 
                                        }
                                    }
                                }
                                if (dt != null)
                                {
                                    dt.Dispose();
                                    dt = null;
                                }
                            }

                            oSetupAppointment.ShowDialog(this);
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                        }
                        break;
                    case "Go To":
                        {
                            frmSearchDate oGoTo = new frmSearchDate();
                            oGoTo.SelectedDate = juc_Calendar.CurrentDate;
                            if (juc_Appointment.View == ScheduleView.DayView)
                            {
                                oGoTo.SelectedView = "Day View";
                            }
                            else if (juc_Appointment.View == ScheduleView.WeekView)
                            {
                                oGoTo.SelectedView = "Week View";
                            }
                            else if (juc_Appointment.View == ScheduleView.MonthView)
                            {
                                oGoTo.SelectedView = "Monthly View";
                            }
                            oGoTo.ShowDialog(this);

                            if (oGoTo.SelectedResult == true)
                            {
                                juc_Calendar.CurrentDate = oGoTo.SelectedDate;
                                juc_Appointment.Date = oGoTo.SelectedDate;
                                if (oGoTo.SelectedView == "Day View")
                                {
                                    juc_Appointment.View = ScheduleView.DayView;
                                }
                                else if (oGoTo.SelectedView == "Week View")
                                {
                                    juc_Appointment.View = ScheduleView.WeekView;
                                }
                                else if (oGoTo.SelectedView == "Monthly View")
                                {
                                    juc_Appointment.View = ScheduleView.MonthView;
                                }
                            }
                            oGoTo.Dispose();

                        }
                        break;
                    case "Refresh":
                        {
                            FillAppointments(_ShowTemplateAppointment);
                        }
                        break;
                    case "Paste":
                        {
                            #region " Paste Appointment "
                            if (cmnu_Appointment_Paste.Tag != null)
                            {
                                cmnu_AppointmentEdit.Hide();   //GLO2011-0013923 :Hide the context menu.

                                if (((CutCopyPaste)cmnu_Appointment_Paste.Tag) == CutCopyPaste.Cut)
                                {
                                    PasteAppointment_Click(CutCopyPaste.Cut);
                                    cmnu_Appointment_Paste.Tag = null;
                                    cmnu_Appointment_Paste.Visible = false;
                                }
                                else if (((CutCopyPaste)cmnu_Appointment_Paste.Tag) == CutCopyPaste.Copy)
                                {
                                    PasteAppointment_Click(CutCopyPaste.Copy);
                                }
                            }

                            #endregion
                        }
                        break;
                    case "Confirmed":
                        {

                        }
                        break;
                    case "Register Patient":    //Register Patient for Template 
                        {
                            if (cmnu_Appointment_RegisterPatient.DropDownItems.Count == 0)
                            {
                                RegisterPatientSaveAppointment("", 0);
                            }
                        }
                        break;
                    case "Modify Patient":
                        {
                            gloSettings.GeneralSettings oSettings = new GeneralSettings(_databaseconnectionstring);
                            //Check patient Is Legal pending or Deceased 
                            if (oSettings.IsAdminUser(_UserID,_ClinicID) == false)
                            {
                                if (string.Compare(_MessageBoxCaption, "gloEMR", true) == 0)
                                {
                                    using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_databaseconnectionstring))
                                    {
                                        string sStatusmsg = objPatient.CheckStaus(_PatientD);
                                        if (sStatusmsg != "")
                                        {
                                            MessageBox.Show(sStatusmsg, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            if (oSettings != null) { oSettings.Dispose(); oSettings = null; } 
                                            return;
                                        }
                                    }
                                }
                            }
                            if (oSettings!=null) {oSettings.Dispose();  oSettings=null; } 
                            //---end 


                            cmnu_AppointmentEdit.Hide();
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());
                                DataTable dtPatient = ogloAppointment.GetPatient(nMSTAppointmentID, nDTLAppointmentID);
                                if (dtPatient != null && dtPatient.Rows.Count > 0)
                                {
                                    Int64 _CurrentPatientID = 0;
                                    _CurrentPatientID = _PatientD;
                                    //Modify Patient
                                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), _databaseconnectionstring);

                                    //Handle Save close event to do patient modifiy Hl7 message queue by Abhijeet on 20110607
                                    ofrmSetupPatient.EvntSaveandClose += new gloPatient.frmSetupPatient.SaveandCloseHandler(ofrmSetupPatient_EvntSaveandClose);
                                    //End of changes by Abhijeet on 20110607 for Handling save close event to do patient modify message queue entry

                                    ofrmSetupPatient.ShowDialog(this);
                                    ofrmSetupPatient.EvntSaveandClose -= new gloPatient.frmSetupPatient.SaveandCloseHandler(ofrmSetupPatient_EvntSaveandClose);
                                    _CurrentPatientID = ofrmSetupPatient.ReturnPatientID;
                                    ofrmSetupPatient.Dispose();

                                    if (_CurrentPatientID > 0)
                                    {
                                        _PatientD = _CurrentPatientID;
                                    }

                                    //Refresh Appointment & details
                                    string sPatientName = Convert.ToString(dtPatient.Rows[0]["sPatientName"]).Trim();
                                    dtPatient.Dispose();
                                    dtPatient = null;
                                    dtPatient = ogloAppointment.GetPatient(nMSTAppointmentID, nDTLAppointmentID);
                                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                                    {
                                        juc_Appointment.CurrentAppointment.Prefix = juc_Appointment.CurrentAppointment.Prefix.Replace(sPatientName, Convert.ToString(dtPatient.Rows[0]["sPatientName"]));

                                    }
                                    ViewAppointmentDetails();
                                }
                                if (dtPatient != null)
                                {
                                    dtPatient.Dispose();
                                    dtPatient = null;
                                }
                            }
                        }
                        break;
                    case "New Patient":
                        {
                            //RegAppUsingTemplateOnly Setting is true then allow to 
                            //create Appointemnts using template only
                            cmnu_AppointmentEdit.Hide();
                            if (cmnu_AppointmentNew != null)
                            {
                                cmnu_AppointmentNew.Hide();
                            }
                            if (_RegAppUsingTemplateOnly == true)
                            {

                                return;
                            }

                            //New Appointment
                            ASBaseType _OwnerType = ASBaseType.None;
                            Int64 _OwnerID = 0;
                            Int64 _OwnerTypeID = 0;
                            string _OwnerName = "";
                            if (juc_Appointment.GetOwnerAt(_JanusPastePoint) != null)
                            {
                                _OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                                _OwnerID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 1));
                                _OwnerTypeID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                                _OwnerName = Convert.ToString(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 2));
                            }

                            if (_OwnerType != ASBaseType.Provider)
                            {
                                //Added by Mayuri:2010-To fix issue-#5625-When on calander no "provider" is selected then it should display message "Please select  provider"
                                cmnu_AppointmentEdit.Hide();
                                MessageBox.Show("Please select provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);

                            #region "Set Appointment Parameters"
                            oSetupAppointment.SetAppointmentParameters.MasterAppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.AppointmentID = 0;
                            oSetupAppointment.SetAppointmentParameters.ClinicID = _ClinicID;
                            oSetupAppointment.SetAppointmentParameters.ProviderID = _OwnerID;
                            oSetupAppointment.SetAppointmentParameters.ProviderName = _OwnerName;
                            oSetupAppointment.SetAppointmentParameters.AddTrue_ModifyFalse_Flag = true; // Add - true, Modify - false
                            oSetupAppointment.SetAppointmentParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oSetupAppointment.SetAppointmentParameters.ModifySingleAppointmentFromReccurence = false;
                            oSetupAppointment.SetAppointmentParameters.StartDate = juc_Appointment.GetDateTimeAt(_JanusPastePoint);// .GetDateAt();
                            oSetupAppointment.SetAppointmentParameters.StartTime = juc_Appointment.GetDateTimeAt(_JanusPastePoint);//Convert.ToDateTime( juc_Appointment.GetDateTimeAt().TimeOfDay.ToString());//Convert.ToDateTime(string.Format(juc_Appointment.GetDateTimeAt().ToShortTimeString(),"hh:mm:tt"));//Convert.ToDateTime(string.Format(juc_Appointment.GetDateAt().ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(juc_Appointment.GetTimeAt().ToString(), "hh:mm tt"));
                            oSetupAppointment.SetAppointmentParameters.Duration = juc_Appointment.MinuteInterval;
                            oSetupAppointment.SetAppointmentParameters.LoadParameters = true;

                            #endregion
                            oSetupAppointment.RegisterPatientnSaveAppointment = true;
                            oSetupAppointment.ShowDialog(this);
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                        }
                        break;
                         
                    case "Checkin":
                        {
                            cmnu_AppointmentEdit.Hide();
                            
                            
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                try
                                {
                                    oDB.Connect(false);
                                    string strSQL = "";
                                    Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                    Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());
                                    
                                    DataTable dtPatient = ogloAppointment.GetPatient(nMSTAppointmentID, nDTLAppointmentID);

                                    // GLO2011-0011970 
                                    // If status is legal pending restrict user to Check-in an appointment
                                    if (ogloAppointment.IsLegalPending(nMSTAppointmentID, nDTLAppointmentID))
                                    {
                                        MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        if (dtPatient != null)
                                        {
                                            dtPatient.Dispose();
                                            dtPatient = null;
                                        }
                                        break;
                                    }

                                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                                    {
                                        strSQL = "SELECT ISNULL(bIsPARequired,0) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID=" + nMSTAppointmentID + " AND nDTLAppointmentID=" + nDTLAppointmentID;
                                        
                                        DataTable dtIsPA = null;
                                        oDB.Retrive_Query(strSQL, out dtIsPA);
                                        if (dtIsPA != null)
                                        {
                                            if (dtIsPA.Rows[0][0].ToString() == "True")
                                            {
                                                if (!PriorAuthorizationTransaction.IsExist(nMSTAppointmentID, nDTLAppointmentID))
                                                { MessageBox.Show("Warning – Today's appointment requires authorization but authorization is not found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                            }
                                            dtIsPA.Dispose();
                                            dtIsPA = null;
                                        }
                                        
                                        gloPMGeneral.frmCheckIn ofrmCheckIn = new gloPMGeneral.frmCheckIn(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]), nMSTAppointmentID, nDTLAppointmentID, _databaseconnectionstring);

                                        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                                        // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
                                        if (ogloAppointment != null)
                                        {
                                            ogloAppointment.Dispose();
                                            ogloAppointment = null;
                                        }
                                        ogloAppointment = new gloAppointment(_databaseconnectionstring);
                                        ofrmCheckIn.VisitID = ogloAppointment.GetCurrentVisitID(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]));

                                        ofrmCheckIn.ShowDialog(this);
                                        ofrmCheckIn.Dispose();
                                    }
                                    if (dtPatient != null)
                                    {
                                        dtPatient.Dispose();
                                        dtPatient = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    if (ogloAppointment != null)
                                    {
                                        ogloAppointment.Dispose();
                                        ogloAppointment = null;
                                    }
                                    if (oDB != null)
                                    {
                                        oDB.Dispose();
                                        oDB = null;
                                    }
                                }
                            }
                        }
                        break;
                    case "Checkout":
                        {
                                cmnu_AppointmentEdit.Hide();
                                if (juc_Appointment.CurrentAppointment != null)
                                {
                                    Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                    Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());
                                    DateTime dtstartDate = juc_Appointment.CurrentAppointment.StartTime;
                                    DataTable dtPatient = ogloAppointment.GetPatient(nMSTAppointmentID, nDTLAppointmentID);

                                    if (dtPatient != null && dtPatient.Rows.Count > 0)
                                    {
                                        gloPMGeneral.PatientStatus oPatientStatus = new gloPMGeneral.PatientStatus(_databaseconnectionstring);
                                        oPatientStatus.PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                        oPatientStatus.patientStatusDate = DateTime.Now;
                                        oPatientStatus.TimeIn = "";
                                        oPatientStatus.TimeOut = DateTime.Now.ToLocalTime().ToShortTimeString();
                                        oPatientStatus.Location = "";
                                        oPatientStatus.Status = "";
                                        oPatientStatus.TrackingStatus = 4; // 0=None ,3=CheckIn, 4=CheckOut
                                        oPatientStatus.MasterAppointmentID = nMSTAppointmentID;
                                        oPatientStatus.DetailAppointmentID = nDTLAppointmentID;
                                        oPatientStatus.StartAppointmentDate = dtstartDate;
                                        DisplayGlobalPeriodMessage(Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]));
                                        oPatientStatus.PatientCheckOut();
                                        oPatientStatus.Dispose();
                                        oPatientStatus = null;
                                    }
                                    if (dtPatient != null)
                                    {
                                        dtPatient.Dispose();
                                        dtPatient = null;
                                    }
                                }
                        }
                        break;
                    case "Task":
                        {
                            cmnu_AppointmentEdit.Hide();
                            if (juc_Appointment.CurrentAppointment != null)
                            {
                                Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString());
                                DataTable dtPatient = ogloAppointment.GetPatient(nMSTAppointmentID, nDTLAppointmentID);
                                if (dtPatient != null && dtPatient.Rows.Count > 0)
                                {
                                    gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(_databaseconnectionstring, 0);
                                    ofrmTask.PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);
                                    ofrmTask.ProviderID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Owner.ToString(), '~', 1).ToString());
                                    ofrmTask.StartDate = juc_Appointment.CurrentAppointment.StartTime;
                                    ofrmTask.DueDate = juc_Appointment.CurrentAppointment.StartTime.Date;
                                    ofrmTask.ShowDialog(this);
                                    ofrmTask.Dispose();

                                }
                                if (dtPatient != null)
                                {
                                    dtPatient.Dispose();
                                    dtPatient = null;
                                }
                            }
                        }
                        break;
                    case "New Block/Schedule":
                    {
                        ShowScheduleForm();
                    }
                    break;
                 
                    default:
                        {
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloAppointment != null)
                {
                    ogloAppointment.Dispose();
                    ogloAppointment = null;
                }
               
                if (oSecurity != null)
                {
                    oSecurity.Dispose();
                    oSecurity = null;
                }
                if (oSetupAppointment != null)
                {
                    oSetupAppointment.Dispose();
                    oSetupAppointment = null;
                }
                if (oClsgloUserRights != null)
                {
                    oClsgloUserRights.Dispose();
                    oClsgloUserRights = null;
                }
                
                juc_Appointment.ContextMenu = null;
                FillAppointments(_ShowTemplateAppointment);
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
        }

        private void DisplayGlobalPeriodMessage(long _patientId)
        {
            PatientStatus objPatientStatus = new PatientStatus(_databaseconnectionstring);

            DataTable _dtGlobalPeriod = null;
            _dtGlobalPeriod = objPatientStatus.GetGlobalPeriods_ForAlter(_patientId);
            objPatientStatus.Dispose();
            objPatientStatus = null;
            if (_dtGlobalPeriod != null)
            {
                if (_dtGlobalPeriod.Rows.Count == 1)
                {
                    String _strMessage = "Today’s visit falls within a Global Period:"
                                     + Environment.NewLine + "CPT : " + _dtGlobalPeriod.Rows[0]["CPT"].ToString().Trim()
                                     + Environment.NewLine + "Dates : " + _dtGlobalPeriod.Rows[0]["Dates"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Provider : " + _dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Insurance : " + _dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Reminder : " + _dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Comment : " + _dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim();
                    MessageBox.Show(_strMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_dtGlobalPeriod.Rows.Count > 1)
                {
                    MessageBox.Show("Today’s visit falls within MULTIPLE Global Periods.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _dtGlobalPeriod.Dispose();
                _dtGlobalPeriod = null;
            }
        }
        private void ShowScheduleForm ()
        {
            //gloAppointmentScheduling.gloSchedule oSchedule = new gloAppointmentScheduling.gloSchedule(_databaseconnectionstring);
            //oSchedule.ShowSchedule();
            //oSchedule.Dispose();
            //oSchedule = null;

            frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);

            #region " Set initial parameters"

            ofrmSchedule.StartTime = juc_Appointment.GetDateTimeAt(_JanusPastePoint);

            if (juc_Appointment.GetOwnerAt(_JanusPastePoint) != null)
            {
                ASBaseType OwnerType = ASBaseType.None;
                OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                if (OwnerType == ASBaseType.Provider)
                {
                    ofrmSchedule.ScheduleType = AppointmentScheduleFlag.ProviderSchedule;
                }
                if (OwnerType == ASBaseType.Resource)
                {
                    ofrmSchedule.ScheduleType = AppointmentScheduleFlag.BlockedSchedule;
                }

                ofrmSchedule.PRUID = Convert.ToInt64(GetTagElement(juc_Appointment.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 1));
            }

            //if (cmbLocation.SelectedIndex > 0)
            //{
            //    ofrmSchedule.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
            //}
            //if (cmbDepartment.SelectedIndex > 0)
            //{
            //    ofrmSchedule.DepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);
            //}
            #endregion

            ofrmSchedule.ShowDialog(this);
            ofrmSchedule.Dispose();
            ofrmSchedule = null;
        }


        //Handle Save close event to do patient modifiy Hl7 message queue by Abhijeet on 20110607
        void ofrmSetupPatient_EvntSaveandClose(long PatientID)
        {
            try
            {
                Int32 nTotalrecords = 0;
                ArrayList sTestNames = new ArrayList();

                //commented by Abhijeet on 20110920 and call it in constructor
                //gloHL7.ReadRegistryHL7OutboundSettings();
                //End of changes for commented line by Abhijeet on 20110920
               
                    if (gloHL7.boolSendPatientDetails)
                    {
                        gloHL7.InsertInMessageQueueforgloLab("A08", PatientID, PatientID, ref nTotalrecords, sTestNames, _databaseconnectionstring);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //End of code to Handle Save close event to do patient modifiy Hl7 message queue by Abhijeet on 20110607

        #endregion
        public void UpdateAdvDirective(long nPatientID)
        {
            SqlCommand Cmd=null;
            SqlConnection Conn = new SqlConnection(_databaseconnectionstring);
            try
            {
                Cmd = new System.Data.SqlClient.SqlCommand();
                Cmd.Connection = Conn;

                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = "Update Patient set nPatientDirective = 0 where nPatientID = " + nPatientID;

                Conn.Open();

                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.Dispose();
                Conn = null;
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
            }
        }

        #region " Follow Up Events & Methods "
        static System.Drawing.Font myRegularFont = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        static System.Drawing.Font myBoldFont = gloGlobal.clsgloFont.gFontArial_Bold;//new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        private void FillFolowupMenu(ToolStripDropDownButton tsbFollowup)
        {
            DataTable dtFolloUp = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("SELECT  nFollowUpID, sFollowUpName, nDuration, nCriteria, nClinicID FROM AB_FollowUp_MST ORDER BY nCriteria,nDuration ", out dtFolloUp);
                if (dtFolloUp != null && dtFolloUp.Rows.Count > 0)
                {
                   for (int i = 0; i < dtFolloUp.Rows.Count; i++)
                    {
                        ToolStripMenuItem oFolloupSubMenuItem = new ToolStripMenuItem();
                        oFolloupSubMenuItem.Text = dtFolloUp.Rows[i]["sFollowUpName"].ToString();
                        oFolloupSubMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                        oFolloupSubMenuItem.Font = myRegularFont;
                        oFolloupSubMenuItem.Tag = dtFolloUp.Rows[i]["nCriteria"].ToString() + "~" + dtFolloUp.Rows[i]["nDuration"].ToString();
                        oFolloupSubMenuItem.Click += new EventHandler(cmnuFolloupMenuItem_Click);
                        oFolloupSubMenuItem.Image = global::gloAppointmentScheduling.Properties.Resources.FollowUP;
                        oFolloupSubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        oFolloupSubMenuItem.ImageScaling = ToolStripItemImageScaling.None;
                        tsbFollowup.DropDownItems.Add(oFolloupSubMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dtFolloUp != null)
                {
                    dtFolloUp.Dispose();
                    dtFolloUp = null;
                }
            }
        }

        private void cmnuFolloupMenuItem_Click(object sender, EventArgs e)
        {
            gloSettings.GeneralSettings ogloSettings = null;
         
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                if (Convert.ToString(((ToolStripItem)sender).Tag) != "")
                {
                    //Get folloup Settings

                    string _sFolloupStart = "";
                    ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                    object value = null;
                    ogloSettings.GetSetting("FolloupDate", out value);
                    if (value != null && Convert.ToString(value).Trim() != "")
                    {
                        _sFolloupStart = Convert.ToString(value);
                    }
                    String[] FolloUps = Convert.ToString(((ToolStripItem)sender).Tag).Split('~');
                    gloAppointmentBook.FollowUpType oFollowUpType = (gloAppointmentBook.FollowUpType)Convert.ToInt32(FolloUps[0]);
                    Int32 nDuration = Convert.ToInt32(FolloUps[1]);

                    if (juc_Appointment.CurrentOwner != null)
                    {
                        //Set Folloup criteria
                        frmSearchAppointment ofrmSearchAppointment = new frmSearchAppointment();
                        ofrmSearchAppointment.bFindFollowUp = true;
                        ofrmSearchAppointment.sProviderName = juc_Appointment.CurrentOwner.Text;
                        ofrmSearchAppointment.nProviderID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentOwner.Value.ToString(), '~', 1).ToString());

                        switch (oFollowUpType)
                        {
                            case gloAppointmentBook.FollowUpType.Day:
                                {
                                    if (_sFolloupStart == "FolloupDate")
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddDays(nDuration);
                                    }
                                    else
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddDays(1);
                                    }

                                    ofrmSearchAppointment.dtFollowUpEndDate = ofrmSearchAppointment.dtFollowUpStartDate.AddDays(nDuration);
                                }
                                break;
                            case gloAppointmentBook.FollowUpType.Week:
                                {
                                    if (_sFolloupStart == "FolloupDate")
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddDays(nDuration * 7);
                                    }
                                    else
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddDays(1);
                                    }

                                    ofrmSearchAppointment.dtFollowUpEndDate = ofrmSearchAppointment.dtFollowUpStartDate.AddDays(nDuration * 7);
                                }
                                break;
                            case gloAppointmentBook.FollowUpType.Month:
                                {
                                    if (_sFolloupStart == "FolloupDate")
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddMonths(nDuration);
                                    }
                                    else
                                    {
                                        ofrmSearchAppointment.dtFollowUpStartDate = juc_Appointment.Date.AddDays(1);
                                    }

                                    ofrmSearchAppointment.dtFollowUpEndDate = ofrmSearchAppointment.dtFollowUpStartDate.AddMonths(nDuration);
                                }
                                break;
                        }

                        ofrmSearchAppointment.ShowDialog(this);
                        ofrmSearchAppointment.Dispose();
                        FillAppointments(_ShowTemplateAppointment);
                    }
                    else
                    {
                        MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion


        #region "Alerts Methods"

        public DataTable GetCopayAlert(Int64 Patientid, DateTime CopayDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtCopayAlert = null;
            Object retVal = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT Count(AS_Appointment_DTL.dtStartDate) " +
                            " FROM  AS_Appointment_MST  WITH(NOLOCK) INNER JOIN AS_Appointment_DTL  WITH(NOLOCK)  " +
                            " ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID " +
                            " WHERE " +
                            " (AS_Appointment_MST.nPatientID = " + Patientid + ")  " +
                            " AND (AS_Appointment_DTL.dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString()) + ")  " +
                            " AND (AS_Appointment_DTL.nClinicID = " + this._ClinicID + ")";

                retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                if (retVal != null && Convert.ToString(retVal) != "" && Convert.ToInt64(retVal) > 0)
                {
                    //_sqlQuery = "";
                    //_sqlQuery = " SELECT COUNT(nAdvPayID) FROM BL_Transaction_AdvancePayment_MST " +
                    //            " WHERE nAppointmentDate = " + gloDateMaster.gloDate.DateAsNumber(CopayDate.ToShortDateString()) + " " +
                    //            " AND nPatientID = " + Patientid + " AND nClinicID = " + this._ClinicID + "";

                    //retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                    //if (retVal != null && Convert.ToString(retVal) != "" && Convert.ToInt64(retVal) <= 0)
                    //{ 
                        _sqlQuery = "";
                        _sqlQuery = " SELECT  distinct   nPatientID, ISNULL(nCoPay,0) AS nCoPay, nInsuranceID, sInsuranceName,nInsuranceFlag, " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)  " +
                                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'   " +
                                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'  " +
                                    " ELSE '' END  AS SortOrder,   " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)     " +
                                    " WHEN 0 THEN 4   " +
                                    " ELSE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0)  END  AS SortIndex  " +
                                    " FROM      PatientInsurance_DTL  WITH(NOLOCK) " +
                                    " WHERE     (nPatientID = " + Patientid + ") AND ISNULL(nCoPay,0) > 0 " +
                                    " ORDER BY SortIndex";

                        oDB.Retrive_Query(_sqlQuery, out _dtCopayAlert);
                    //}
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _dtCopayAlert;
        }

        public bool IsCopayUnapplied(Int64 Patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _retVal = null;
            bool _isCopayUnapplied = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = " select COUNT(nAdvPayID) from BL_Transaction_AdvancePayment_MST " +
                            " where  " +
                            " nAdvPayID NOT IN (select nCoPayID from BL_Transaction_Payment_DTL WHERE nPatientID = " + Patientid + " AND nClinicID = " + this._ClinicID + ") " +
                            " AND nOtherPaymentMode = '1' and nPatientID = " + Patientid + " " +
                            " AND nClinicID = " + this._ClinicID + " ";


                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_retVal != null)
                { _isCopayUnapplied = Convert.ToBoolean(_retVal); }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); oDB = null; } }

            return _isCopayUnapplied;
        }

        #endregion

        #region "Print Word Templates"

        //Added By MaheshB to Fill Templates in Context Menu 
        private void FillTemplatesMenu(ToolStripMenuItem cmnuTemplate)
        {

           
            
            clearTemplateMenus(cmnuTemplate);
            cmnuTemplate.DropDownItems.Clear();

            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
            DataTable dtCategories = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
         
            String CategoryName = "";
            try
            {
                oDB.Connect(false);
                dtCategories = ogloTemplate.GetTemplateCategoryList();

                if (dtCategories != null)
                {
                    if (dtCategories.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCategories.Rows.Count; i++)
                        {
                            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();

                            CategoryName = dtCategories.Rows[i]["CategoryName"].ToString();
                            oCatMenuItem.Text = CategoryName;

                            string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
                                               "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') + SPACE(1)  " +
                                               " + ISNULL(Provider_MST.sMiddleName, '') AS sProviderName " +
                                               " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
                                               " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                                               " WHERE  TemplateGallery_MST.sCategoryName = '" + CategoryName.Replace("'", "''") + "' ";
                            DataTable dtTemplates = null;
                            oDB.Retrive_Query(_sqlQuery, out dtTemplates);

                            if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtTemplates.Rows.Count; j++)
                                {
                                    ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                                    oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                                    oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                                    oCatMenuItem.DropDownItems.Add(oTemplateItem);
                                    oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                                }
                            }
                            if (dtTemplates != null) { dtTemplates.Dispose(); dtTemplates = null; }
                            cmnuTemplate.DropDownItems.Add(oCatMenuItem);
                        }

                        //To Get Selected Appointmnet's PatientID
                        #region "Get Selected Appointmnet's PatientID"

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                    ogloTemplate = null;
                }
                if (dtCategories != null)
                {
                    dtCategories.Dispose();
                    dtCategories = null;
                }
            
            }
        }

        private ToolStripMenuItem getAllTemplatesMenu()
        {
            ToolStripMenuItem cmnuTemplate = new ToolStripMenuItem();
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
            DataTable dtCategories = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
       
            String CategoryName = "";
            try
            {
                oDB.Connect(false);
                dtCategories = ogloTemplate.GetTemplateCategoryList();

                if (dtCategories != null)
                {
                    if (dtCategories.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCategories.Rows.Count; i++)
                        {
                            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();
                            CategoryName = dtCategories.Rows[i]["CategoryName"].ToString();
                            oCatMenuItem.Text = CategoryName;

                            string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
                                               "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') + SPACE(1)  " +
                                               " + ISNULL(Provider_MST.sMiddleName, '') AS sProviderName " +
                                               " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
                                               " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                                               " WHERE  TemplateGallery_MST.sCategoryName = '" + CategoryName.Replace("'", "''") + "' ";
                            DataTable dtTemplates = null;
                            oDB.Retrive_Query(_sqlQuery, out dtTemplates);
                            if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtTemplates.Rows.Count; j++)
                                {
                                    ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                                    oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                                    oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                                    oCatMenuItem.DropDownItems.Add(oTemplateItem);
                                    oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                                }
                            }

                            if (dtTemplates != null) { dtTemplates.Dispose(); dtTemplates = null; }
                            cmnuTemplate.DropDownItems.Add(oCatMenuItem);
                        }


                        //To Get Selected Appointmnet's PatientID
                        #region "Get Selected Appointmnet's PatientID"
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                    ogloTemplate = null;
                }
                if (dtCategories != null)
                {
                    dtCategories.Dispose();
                    dtCategories = null;
                }
            }
            return cmnuTemplate;

        }

        private ToolStripMenuItem Get_AssociationTemplatesMenu(gloOffice.AssociationCategories oAssociateCategory)
        {

 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            
            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();

        //    String CategoryName = "";
            try
            {
                oDB.Connect(false);
                gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);

                DataTable dtTemplates = ogloTemplate.GetAssociation(oAssociateCategory);
                ogloTemplate.Dispose();
                ogloTemplate = null;
                if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                {
                    for (int j = 0; j < dtTemplates.Rows.Count; j++)
                    {
                        ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                        oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                        oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                        oCatMenuItem.DropDownItems.Add(oTemplateItem);
                        oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                    }
                }
                if (dtTemplates != null) { dtTemplates.Dispose(); dtTemplates = null; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return oCatMenuItem;
        }

        protected void cmnuTemplateItem_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
  
            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
            long _nVisitID = 0;

            try
            {

                Int64 _PatientId = 0;
                if (sender != null)
                {
                    ToolStripMenuItem cmnuTemplateItem  = (ToolStripMenuItem)sender;

                    //To get Selected Patient Information
                    //Object objCurrentAppointment = (Janus.Windows.Schedule.Schedule)objPatient;
                    Int64 MasterAppointmentID = 0;
                    Int64 DetailAppointmentID = 0;

                    if (_LastSelectedAppointment != null)
                    {

                        MasterAppointmentID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 1).ToString()); ;
                        DetailAppointmentID = Convert.ToInt64(GetTagElement(_LastSelectedAppointment.Tag.ToString(), '~', 2).ToString()); ;

                        string _AppointmentReferral = "";
                        _AppointmentReferral = ogloAppointment.GetAppointmentReferral(MasterAppointmentID);


                        DataTable dtPatient = ogloAppointment.GetPatient(MasterAppointmentID, DetailAppointmentID);
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            _PatientId = Convert.ToInt64(dtPatient.Rows[0]["nPatientId"]);

                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                            _nVisitID = ogloAppointment.GetCurrentVisitID(_PatientId);
                        }
                        if (dtPatient != null)
                        {
                            dtPatient.Dispose();
                            dtPatient = null;
                        }
                    }
                    //End Here Patient Information

                    if (_PatientId > 0)
                    {
                        gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);

                        ogloTemplate.CategoryID = Convert.ToInt64(cmnuTemplateItem.OwnerItem.Tag);
                        ogloTemplate.CategoryName = cmnuTemplateItem.OwnerItem.Text;
                        ogloTemplate.TemplateID = Convert.ToInt64(cmnuTemplateItem.Tag);
                        ogloTemplate.PrimeryID = Convert.ToInt64(DetailAppointmentID);//Convert.ToInt64(MasterAppointmentID);
                        ogloTemplate.TemplateName = cmnuTemplateItem.Text;
                        ogloTemplate.PatientID = _PatientId;
                        ogloTemplate.ClinicID = _ClinicID;
                         
                        //Bug #92723: 00001067: Appointment 
                        DateTime Date = Convert.ToDateTime(juc_Calendar.CurrentDate);
                        ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(Date.ToString());

                        //20-Dec-16 Aniket: Resolving Incident #102466: CAS-01062-J5K3T1
                        ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(Date.ToString());

                        //set here appointment tab=true to get next appointment as per selected
                        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                        // We were not sending Visit Id, so history liquid links was not populating in check-in template
                        ogloTemplate.VisitID = _nVisitID;
                        ogloTemplate.isFromDashboradAppt = true;
                         //The below constructor is commented to get Check in templates accroding to the calender date seleted
                        //gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring, ogloTemplate);
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring,true, ogloTemplate);
                        frm.Text = cmnuTemplateItem.Text;
                        frm.MdiParent = this.ParentForm;
                        frm.Show();
                        frm.WindowState = FormWindowState.Maximized;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ogloAppointment != null) { ogloAppointment.Dispose(); ogloAppointment = null; }
                //SLR: This is used in the frm: if (ogloTemplate != null) { ogloTemplate.Dispose(); }
            }

        }


        #endregion


        private void frmViewAppointment_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                string _sLastSelectedProviders = "";
                string _sLastSelectedResources = "";
                string _sLastSelectedLocation = "";

                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        _sLastSelectedProviders += Convert.ToString(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                    }
                }
                if (_sLastSelectedProviders.Trim() != "")
                {
                    _sLastSelectedProviders = _sLastSelectedProviders.Remove(_sLastSelectedProviders.LastIndexOf(','));
                }
                for (int i = 0; i < trvResources.Nodes.Count; i++)
                {
                    if (trvResources.Nodes[i].Checked == true)
                    {
                        _sLastSelectedResources += Convert.ToString(GetTagElement(trvResources.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                    }
                }
                if (_sLastSelectedResources.Trim() != "")
                {
                    _sLastSelectedResources = _sLastSelectedResources.Remove(_sLastSelectedResources.LastIndexOf(','));
                }
                for (int i = 0; i < trvLocations.Nodes.Count; i++)
                {
                    if (trvLocations.Nodes[i].Checked == true)
                    {
                        _sLastSelectedLocation += Convert.ToString(GetTagElement(trvLocations.Nodes[i].Tag.ToString(), '~', 1)) + ",";
                    }
                }
                if (_sLastSelectedLocation.Trim() != "")
                {
                    _sLastSelectedLocation = _sLastSelectedLocation.Remove(_sLastSelectedLocation.LastIndexOf(','));
                }

                oSettings.WriteSettings_XML("Provider", "LastProviderIDs", _sLastSelectedProviders);
                oSettings.WriteSettings_XML("Resource", "LastResourceIDs", _sLastSelectedResources);
                oSettings.WriteSettings_XML("Location", "LastLocationIDs", _sLastSelectedLocation);
                oSettings.WriteSettings_XML("Appointment", "ZoomTime", juc_Appointment.Interval.GetHashCode().ToString());
                oSettings.WriteSettings_XML("Appointment", "ShowAppointmentColor", chkShowTemplateColor.Checked.ToString());
                //appSettings.Set("LastSelectedProviders",_sLastSelectedProviders);  

                CalendarView_Closed(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
            }

        }

        private int GetNumberofColumninDayView()
        {
            int num_NoofColOnCalndr = 3;
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                string _sNoOfColsOnCalendar = oSettings.ReadSettings_XML("Appointment", "NoOfColsOnCalendar");
                if (_sNoOfColsOnCalendar.Trim() != "")
                {
                    num_NoofColOnCalndr = Convert.ToInt32(_sNoOfColsOnCalendar);
                }
                else
                {
                    num_NoofColOnCalndr = 3;
                }
                return num_NoofColOnCalndr;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return num_NoofColOnCalndr;
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
            }
        }
        #region "Designer events"

        private void btn_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Yellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //event to change buttons color on MouseLeave 
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }

        #endregion

        private void tmrAlert_Tick(object sender, EventArgs e)
        {

            try
            {
                tmrAlert.Enabled = false;
                if (lblCopayAlert.Text != "" || lblPrePayAlert.Text != "")
                {
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);

                    Int64 _userID = 0;
                    //Get User ID
                    if (appSettings["UserID"] != null)
                    {
                        if (appSettings["UserID"] != "")
                        {
                            _userID = Convert.ToInt64(appSettings["UserID"]);
                        }
                        else { _userID = 0; }
                    }
                    else
                    { _userID = 0; }

                    //Read Alert Settings (Blinking and Color Settings)
                    bool IsBlinking = true;
                    Color AlertColor = Color.Red;
                    object oValue = null;
                    oSettings.GetSetting("BlinkingAlert", _userID, _ClinicID, out oValue);
                    if (oValue != null && Convert.ToString(oValue) != "")
                    {
                        IsBlinking = Convert.ToBoolean(oValue);
                    }
                    oValue = null;
                    oSettings.GetSetting("AlertColor", _userID, _ClinicID, out oValue);
                    if (oValue != null && Convert.ToString(oValue) != "")
                    {
                        AlertColor = Color.FromArgb(Convert.ToInt32(oValue));
                    }


                    if (IsBlinking == true)
                    {
                        lblCopayAlert.ForeColor = (lblCopayAlert.ForeColor == Color.Black) ? AlertColor : Color.Black;
                        lblPrePayAlert.ForeColor = (lblPrePayAlert.ForeColor == Color.Black) ? AlertColor : Color.Black;
                    }
                    else
                    {
                        lblCopayAlert.ForeColor = AlertColor;
                        lblPrePayAlert.ForeColor = AlertColor;
                    }
                    if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                tmrAlert.Enabled = true; 
            }

        }

        private void ts_SmallStrip_btn_Document_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void ts_SmallStrip_btn_Provider_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void ts_SmallStrip_btn_Resources_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void ts_SmallStrip_btn_Legend_Click(object sender, EventArgs e)
        {
            pnlLeft.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void btn_Right_MouseHover(object sender, EventArgs e)
        {
            btn_Right.Image = global::gloAppointmentScheduling.Properties.Resources.ForwardHover;
            btn_Right.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btn_Right_MouseLeave(object sender, EventArgs e)
        {
            btn_Right.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_Right.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btnLeft_MouseHover(object sender, EventArgs e)
        {

            btnLeft.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.RewindHover;
            btnLeft.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnLeft_MouseLeave(object sender, EventArgs e)
        {

            btnLeft.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btnLeft.BackgroundImageLayout = ImageLayout.Center;
        }

        //Added By Pramod Nair For UserRights 20090720
        private void AssignUserRights()
        {
            try
            {

                if (_UserName.Trim() != "")
                {
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);
                    if (!oClsgloUserRights.NewPatient)
                    {
                        cmnu_Appointment_RegisterPatient.Visible = false;
                        cmnu_Appointment_NewPatient.Visible = false;
                    }

                    if (!oClsgloUserRights.ModifyPatient)
                    {
                        cmnu_Appointment_ModifyPatient.Visible = false;
                    }

                    if (!oClsgloUserRights.Appointment)
                    {
                        tsb_Appointment.Enabled = false;
                        tsb_ModifyAppointment.Enabled = false;
                        tsb_Delete.Enabled = false;
                        cmnu_Appointment_New.Enabled = false;
                    }

                   
                    // By Pranit on 20111206 to check admin setting (new schedule)
                    if (!oClsgloUserRights.NewSchedule)
                   {
                        cmnu_Appointment_Schedule.Enabled = false;
                   }
                    


                }
                SetLicenseModule();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }





   //By Pranit on 20111206 to check admin setting (new schedule)
   
        public bool checkSchedule()
     {
	   bool _isResult = false;
	   string _UserName = null;
	
	try 
    {
		//Get User Name
		if (appSettings["UserName"] != null)
        {
			if (!string.IsNullOrEmpty(appSettings["UserName"])) 
            {
				_UserName = Convert.ToString(appSettings["UserName"].Trim());
			} 
            else
            {
				_UserName = "";
			}
		} 
        else
        {
			_UserName = "";
		}

		//'user rights
		
		gloUserRights.ClsgloUserRights oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
		try 
        {
			if ((oClsgloUserRights != null))
            {
				oClsgloUserRights.CheckForUserRights(_UserName);
				if ((oClsgloUserRights.NewSchedule))
                {
					_isResult = true;
				}
			}
		} 
        catch (Exception ex)
        {
		 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
     	} 
        finally 
        {
			if ((_UserName != null))
            {
				_UserName = null;
			}
            if (oClsgloUserRights != null)
            {
                oClsgloUserRights.Dispose();
                oClsgloUserRights = null;
            }
		}

	} 
    catch (Exception ex1)
    {
		 gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, true);
	}
	return _isResult;
}
   









        private void tsb_PrintPreview_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            DataTable dtproviders = new DataTable();
            dtproviders.Columns.Add("ID");
            dtproviders.Columns.Add("DispName");
            try
            {
                foreach (TreeNode trv in trvProvider.Nodes)
                {
                    if (trv != null)
                    {
                        if (trv.Checked == true)
                        {
                            DataRow dr = dtproviders.NewRow();
                            dr["ID"] = Convert.ToInt64(GetTagElement(trv.Tag.ToString(), '~', 1));
                            dr["DispName"] = trv.Text.ToString();
                            dtproviders.Rows.Add(dr);
                        }
                    }
                }

               // gloReports.C1Reports.frmRpt_Appointments Objrptapp = new gloReports.C1Reports.frmRpt_Appointments(_databaseconnectionstring, dtproviders, juc_Appointment.DateRange.Start, juc_Appointment.DateRange.End, false);
                gloReports.frmRPT_AppointmentView Objrptapp = new gloReports.frmRPT_AppointmentView(_databaseconnectionstring, dtproviders, juc_Appointment.DateRange.Start, juc_Appointment.DateRange.End, false, gstrCaption);

               Objrptapp.gstrSQLServerName = gstrSQLServerName;
               Objrptapp.gstrDatabaseName = gstrDatabaseName;
               Objrptapp.gblnSQLAuthentication = gblnSQLAuthentication;
               Objrptapp.gstrSQLUser = gstrSQLUser;
               Objrptapp.gstrSQLPassword = gstrSQLPassword;
               //Objrptapp.msgCaption = gstrCaption;
          

                Objrptapp.MdiParent = this.ParentForm;
                Objrptapp.WindowState = FormWindowState.Maximized;
                Objrptapp.ShowInTaskbar = false;
                Objrptapp.StartPosition = FormStartPosition.CenterParent;
                Objrptapp.Show();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                dtproviders.Dispose();
                dtproviders = null;
            }
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            //DataTable dtproviders = new DataTable();
            //dtproviders.Columns.Add("ID");
            //dtproviders.Columns.Add("DispName");
            StringBuilder sbprovid=new StringBuilder()  ;
            try
            {
                foreach (TreeNode trv in trvProvider.Nodes)
                {
                    if (trv != null)
                    {
                        if (trv.Checked == true)
                        {
                            //DataRow dr = dtproviders.NewRow();
                          sbprovid.Append( Convert.ToString (GetTagElement(trv.Tag.ToString(), '~', 1)));
                          sbprovid.Append("|");
                          //  dr["DispName"] = trv.Text.ToString();
                          //  dtproviders.Rows.Add(dr);
                        }
                    }
                }
                clsPrintReport objclsprint = new clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword);
                string ParameterList = "FromDate,ToDate,ClinicID,UserName";
                 Int32 nStartdt = 0;
                 Int32 nEnddt = 0;
                 nStartdt = gloDateMaster.gloDate.DateAsNumber(juc_Appointment.DateRange.Start.ToString());
                 nEnddt = gloDateMaster.gloDate.DateAsNumber(juc_Appointment.DateRange.End.ToString());         
                
                string  ParameterValue = "" + nStartdt + "," + nEnddt + "," + _ClinicID + "," + _UserName  + "";

                 if (sbprovid.ToString().Trim() != "")
                 {
                     ParameterList += ",Providers";
                     ParameterValue += "," + sbprovid.ToString();
                 }
                 else
                 {
                     ParameterList += ",Providers";
                     ParameterValue += ",0";
                 }
                 bool gblnDefaultPrinter = false; 
                
                if (appSettings["DefaultPrinter"] != null)
                 {
                     if (!string.IsNullOrEmpty(appSettings["DefaultPrinter"]))
                     {
                         gblnDefaultPrinter = !Convert.ToBoolean(appSettings["DefaultPrinter"]);
                     }
                 }
                else
                {
                    gblnDefaultPrinter = true;
                }
                
                objclsprint.PrintReport("rptAppointmentlist", ParameterList, ParameterValue, gblnDefaultPrinter, "");
                objclsprint.Dispose();
                objclsprint = null; 
            }
                 
                //   gloReports.C1Reports.frmRpt_Appointments Objrptapp = new gloReports.C1Reports.frmRpt_Appointments(_databaseconnectionstring, dtproviders, juc_Appointment.DateRange.Start, juc_Appointment.DateRange.End, true);
                //gloReports.frmRPT_AppointmentView Objrptapp = new gloReports.frmRPT_AppointmentView(_databaseconnectionstring, dtproviders, juc_Appointment.DateRange.Start, juc_Appointment.DateRange.End, true);
                //Objrptapp.gstrSQLServerName = gstrSQLServerName;
                //Objrptapp.gstrDatabaseName = gstrDatabaseName;
                //Objrptapp.gblnSQLAuthentication = gblnSQLAuthentication;
                //Objrptapp.gstrSQLUser = gstrSQLUser;
                //Objrptapp.gstrSQLPassword = gstrSQLPassword;
                //Objrptapp.msgCaption = gstrCaption;
                //Objrptapp.WindowState = FormWindowState.Maximized;
                //Objrptapp.ShowInTaskbar = false;
                //Objrptapp.StartPosition = FormStartPosition.CenterParent;
                //Objrptapp.Show();
         
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                
            }

        }

        private void frmViewAppointment_Deactivate(object sender, EventArgs e)
        {
        }

        private bool SavePAValidation(Int64 nApptMstID, Int64 nApptMstDtlID, DateTime dtStartDate, DateTime dtEndDate, CutCopyPaste oCutCopy)
        {
          //  gloAppointmentScheduling.frmSetupAppointment ofrmSetupAppointment = null;
            try
            {
              //  ofrmSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                #region " Get Prior Authorization Details "
                Int64 _paID = 0;
                DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(nApptMstID, nApptMstDtlID);
                Int64 _nPriorAuthorizationID = 0;
                String _sPriorAuthorizationNo = "";
                if (drPA != null)
                {
                    _nPriorAuthorizationID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
                    _sPriorAuthorizationNo = Convert.ToString(drPA["sPriorAuthorizationNo"]);
                    drPA = null;
                }
                if (Convert.ToString(_nPriorAuthorizationID) != "")
                { _paID = Convert.ToInt64(_nPriorAuthorizationID); }

                Int64 _visitsAllowed = 0;
                Int64 _expDate = 0;
                bool _trackLimit = false;

                DataRow _drPAInfo = clsgloPriorAuthorization.GetPriorAuthorizationInfo(_paID);

                if (_drPAInfo != null)
                {
                    _visitsAllowed = Convert.ToInt64(_drPAInfo["nVisitsAllowed"]);
                    _trackLimit = Convert.ToBoolean(_drPAInfo["bIsTrackAuthLimit"]);

                    if (_trackLimit && Convert.ToString(_drPAInfo["nExpDate"]) != "") { _expDate = Convert.ToInt64(_drPAInfo["nExpDate"]); }
                    _drPAInfo = null;
                }

                #endregion

                #region " Visits calculations "

                Int64 _startDate = 0;
                Int64 _endDate = 0;

                Int64 _visitsRequired = 0;
                Int64 _visitsUsed = 0;
                Int64 _visitsRemaining = 0;
                Int64 _visitsUsedTotal = 0;

                _startDate = gloDateMaster.gloDate.DateAsNumber(dtStartDate.ToString("MM/dd/yyyy"));
                _endDate = gloDateMaster.gloDate.DateAsNumber(dtEndDate.ToString("MM/dd/yyyy"));
                if (clsgloPriorAuthorization.GetVisitsUsed(_paID, _startDate, _endDate) <= 0)
                { _visitsRequired += 1; }
                _visitsUsed = clsgloPriorAuthorization.GetVisitsUsed(_paID, 0, _endDate) + _visitsRequired;
                if (oCutCopy == CutCopyPaste.Cut)
                {
                    _visitsRemaining = clsgloPriorAuthorization.GetVisitsRemainingCutAppt(_paID, _endDate, nApptMstID, nApptMstDtlID, false) - _visitsRequired;
                }
                else
                {
                    _visitsRemaining = clsgloPriorAuthorization.GetVisitsRemaining(_paID, _endDate, false) - _visitsRequired;
                }
                _visitsUsedTotal = _visitsAllowed - _visitsRemaining;

                #endregion

                #region " Warning messages "

                if (_expDate != 0)
                {
                    if (_expDate < _endDate)
                    {
                        if (MessageBox.Show("Prior authorization " + Convert.ToString(_sPriorAuthorizationNo) + " has expired.\nPrior authorization is valid only until " + gloDateMaster.gloDate.DateAsDateString(_expDate) + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        { return false; }
                    }
                }
                if (_trackLimit)
                {
                    if (_visitsAllowed != 0)
                    {
                        if (_visitsUsedTotal > _visitsAllowed)
                        {
                            if (MessageBox.Show("Prior authorization " + Convert.ToString(_sPriorAuthorizationNo) + " has exceeded its # visits allowed.\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            { return false; }
                        }
                        else
                        {
                            string _visit = "visit";
                            if (_visitsRequired > 1) { _visit = _visit + "s"; }

                            if (!_visitsRequired.Equals(0))
                            {
                                if (MessageBox.Show("Prior authorization " + Convert.ToString(_sPriorAuthorizationNo) + " will use "
                                    + _visitsRequired + " " + _visit + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                //+ _visitsUsedTotal + " " + _visit + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
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

        private void frmViewAppointment_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (regFont != null) { regFont.Dispose(); regFont = null; }
            if (boldFont != null) { boldFont.Dispose(); boldFont = null; }

            //15-Dec-15 Aniket: Resolving Bug #91896 ( Modified): gloPM>>Application shows exception on calendar close
            //Cannot dispose the following two fonts as they are assigned from  gloGlobal.clsgloFont.gFont

            //if (myRegularFont != null) { myRegularFont.Dispose(); boldFont = null; }
            //if (myBoldFont != null) { myBoldFont.Dispose(); myBoldFont = null; }
        }

        }
}
