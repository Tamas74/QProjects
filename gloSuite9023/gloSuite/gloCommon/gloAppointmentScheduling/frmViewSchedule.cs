using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook.Books;
using Janus.Windows.Schedule;
using gloAuditTrail;


namespace gloAppointmentScheduling
{

    /// This is Informative Section for the Form below.
    /// Appointment object on calender has Tag = MasterScheduleID~ScheduleID~SingleRecurrenceFlag
    /// Owner Tag- id~Restype      -->ResType is for either isProvider or isResource id
    ///

    internal partial class frmViewSchedule : Form
    {

        #region "Variable Declarations"

        //Dropping schedule Variables

        public Janus.Windows.Schedule.ScheduleAppointment oDropingSchedule = new Janus.Windows.Schedule.ScheduleAppointment();
        public Int64 _oldOwner = 0;

        //Variables for Message box Title and databaseConnectionstring.

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        Point _JanusPastePoint;

        //Added By Pramod Nair For UserRights
        private Int64 _UserID = 0;
        private string _UserName = "";
        gloUserRights.ClsgloUserRights oClsgloUserRights = null;

        private static frmViewSchedule frm;
        private bool blnDisposed;

       // private bool _IsProvidersLoading = false;
        //Added by Mayuri:20100108-To check whether Provider selected or not,to fix issue-#5715
        private bool _IsNodeChecked = false;

        #endregion "Variable Declarations"

        #region " Enumeration Declarations "

        public enum CutCopyPaste
        {
            Cut = 1, Copy = 2, Paste = 3
        }

        #endregion

        #region "Constructor

        //Constructors    

        private frmViewSchedule()
        {

            //Simple Constructor

            InitializeComponent();
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            #region "UserId And UserName"

            //Get User ID
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

            #endregion

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

        }

        private frmViewSchedule(string DatabaseConnectionString)
        {

            //Parameterized Constructor

            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region "UserId And UserName"

            //Get User ID
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

            #endregion

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


        }

        #endregion "Constructor and Variable Declarations"

        public static frmViewSchedule GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmViewSchedule();
                }
            }
            finally
            {

            }
            return frm;
        }

        public static frmViewSchedule GetInstance(string DatabaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmViewSchedule(DatabaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #region "Form Load Event"

        private void frmViewSchedule_Load(object sender, EventArgs e)
        {

            
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();

            // Get Work week for janus from database
            GetWorkWeekForJanus();
            
            btnLeft.Image = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btnLeft.ImageAlign = ContentAlignment.MiddleCenter;

            btn_Right1.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_Right1.ImageAlign = ContentAlignment.MiddleCenter;

            
            //Hide Print Schedule
            cmnu_Schedule_Print.Visible = false;


            GetClinicTiming();
            GetZoomTimeSettings();
            //Fill all Controls on form Load;
            //rbProvider.Checked = true;
            //rbProvider_CheckedChanged(null, null);

            Fill_Locations();
            Fill_Resources();
            Fill_Providers();

            //Refresh Calendar control to get the Schedules on it.
            FillScheduleOnCalendar();

            //Set the date to now for Janus Control.
            this.juc_ViewSchedule.Date = DateTime.Today.Date;

            //Assign User Rights 20090720
            AssignUserRights();


        }

        private void frmViewSchedule_Resize(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        public void GetWorkWeekForJanus()
        {

            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
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

                    if (workWeek > 0)
                    {
                        juc_ViewSchedule.WorkWeek = (ScheduleDayOfWeek)(workWeek);
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




        #endregion "Form Load Event"

        #region "ToolBar Button Click"

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //try
            //{
            //    switch (e.ClickedItem.Tag.ToString())
            //    {
            //        case "Add":
            //            {
            //                frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);
            //                ofrmSchedule.ShowDialog();
            //                ofrmSchedule.Dispose();
            //                FillScheduleOnCalendar();
            //            }
            //            break;
            //        case "Modify":
            //            {
            //                if (juc_ViewSchedule.CurrentAppointment != null && juc_ViewSchedule.CurrentAppointment.Tag != null)
            //                {
            //                    Int64 ScheduleMasterID;
            //                    ScheduleMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
            //                    frmSetupSchedule ofrm = new frmSetupSchedule(ScheduleMasterID, _databaseconnectionstring);
            //                    ofrm.ShowDialog();
            //                    ofrm.Dispose();
            //                }
            //            }
            //            break;
            //        case "Delete":
            //            break;
            //        case "Close":
            //            this.Close();
            //            break;
            //        case "DayView":
            //            this.juc_ViewSchedule.Date = DateTime.Today.Date;
            //            this.juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.DayView;
            //            break;
            //        case "WeekView":
            //            this.juc_ViewSchedule.Date = DateTime.Today.Date;
            //            this.juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.WeekView;
            //            break;
            //        case "MonthView":
            //            this.juc_ViewSchedule.Date = DateTime.Today.Date;
            //            this.juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.MonthView;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //Add New Schedule
        private void tsb_NewSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);

                #region " Set initial parameters"
                
                ofrmSchedule.StartTime = Convert.ToDateTime(juc_Calendar.CurrentDate.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
              
                if (juc_ViewSchedule.CurrentOwner != null)
                {
                    ASBaseType OwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_ViewSchedule.CurrentOwner.Value.ToString(), '~', 3));
                    if (OwnerType == ASBaseType.Provider)
                    {
                        ofrmSchedule.ScheduleType = AppointmentScheduleFlag.ProviderSchedule;
                    }
                    if (OwnerType == ASBaseType.Resource)
                    {
                        ofrmSchedule.ScheduleType = AppointmentScheduleFlag.BlockedSchedule;
                    }
                    ofrmSchedule.PRUID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentOwner.Value.ToString(), '~', 1));
                }

                if (cmbLocation.SelectedIndex > 0)
                {
                    ofrmSchedule.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                }
                if (cmbDepartment.SelectedIndex > 0)
                {
                    ofrmSchedule.DepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);
                }
                #endregion

                ofrmSchedule.ShowDialog(this);
                ofrmSchedule.Dispose();
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Modify Schedule
        //private void tsb_EditSchedule_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (juc_ViewSchedule.CurrentAppointment != null && juc_ViewSchedule.CurrentAppointment.Tag != null)
        //        {
        //            Int64 ScheduleMasterID;
        //            AppointmentScheduleFlag _SchORApp = AppointmentScheduleFlag.None;
        //            _SchORApp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 4));
        //            if (_SchORApp == AppointmentScheduleFlag.BlockedSchedule || _SchORApp == AppointmentScheduleFlag.ProviderSchedule || _SchORApp == AppointmentScheduleFlag.ResourceSchedule)
        //            {
        //                ScheduleMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
        //                frmSetupSchedule ofrm = new frmSetupSchedule(ScheduleMasterID, _databaseconnectionstring);
        //                ofrm.ShowDialog();
        //                ofrm.Dispose();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}



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

                gloUserRights.ClsgloUserRights oClsLocalgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                try
                {
                    if ((oClsLocalgloUserRights != null))
                    {
                        oClsLocalgloUserRights.CheckForUserRights(_UserName);
                        if ((oClsLocalgloUserRights.NewSchedule))
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

            }
            catch (Exception ex1)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, true);
            }
            return _isResult;
        }
   
        private void tsb_EditSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (juc_ViewSchedule.CurrentAppointment != null && juc_ViewSchedule.CurrentAppointment.Tag != null)
                {

                    //By Pranit on 20111206 to check admin setting (new schedule)
                    Boolean isResult = checkSchedule();
                    if (isResult == false)
                    {
                        MessageBox.Show("You don't have rights to access schedule. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                           
                    
                    Int64 ScheduleMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
                    Int64 DetailAppointmentID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                    AppointmentScheduleFlag _SchORApp = AppointmentScheduleFlag.None;
                    _SchORApp = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 4));
                    if (_SchORApp == AppointmentScheduleFlag.BlockedSchedule || _SchORApp == AppointmentScheduleFlag.ProviderSchedule || _SchORApp == AppointmentScheduleFlag.ResourceSchedule)
                    {
                        SetScheduleParameter oScheduleParameters = new SetScheduleParameter();
                        frmSetupSchedule oSetupSchedule = new frmSetupSchedule(ScheduleMasterID, _databaseconnectionstring);
                        bool _ModifySelectedSchedule = false;

                        SingleRecurrence IsReccuring = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 3));


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
                            oScheduleParameters.MasterAppointmentID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1).ToString()); ;
                            oScheduleParameters.AppointmentID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2).ToString()); ;
                            oScheduleParameters.AppointmentFlag = AppointmentScheduleFlag.None;
                            oScheduleParameters.AppointmentTypeID = 0;
                            oScheduleParameters.AppointmentTypeCode = "";
                            oScheduleParameters.AppointmentTypeDesc = "";
                            oScheduleParameters.ProviderID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentOwner.Value.ToString(), '~', 1).ToString());
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

                            
                            // By Pranit on 20111224 to solve issue #17288
                            
                            string[] temp;
                            temp = juc_ViewSchedule.CurrentAppointment.Tag.ToString().Split('~');
                            if (6 <= temp.Length)
                            {
                                 oScheduleParameters.Location = GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                            }
                            if (8 <= temp.Length)
                            {
                                oScheduleParameters.Department = GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 8).ToString();
                            }
                            

                           // oScheduleParameters.Location = GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 6).ToString();
                           // oScheduleParameters.Department = GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 8).ToString();

                            // End By Pranit on 20111224 to solve issue #17288
                            
                            oScheduleParameters.StartDate = juc_ViewSchedule.CurrentAppointment.StartTime;
                            oScheduleParameters.StartTime = juc_ViewSchedule.CurrentAppointment.StartTime;
                            oScheduleParameters.Duration = Convert.ToDecimal(juc_ViewSchedule.CurrentAppointment.Duration.TotalMinutes);
                            oScheduleParameters.ClinicID = _ClinicID;
                            //oScheduleParameters.LineNumber = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 9).ToString());

                            oScheduleParameters.LoadParameters = false;
                            #endregion

                            oSetupSchedule.SetScheduleParameters = oScheduleParameters;
                            oSetupSchedule.ShowDialog(this);

                            FillScheduleOnCalendar();
                            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
                        }
                        if (oSetupSchedule != null)
                        {
                            oSetupSchedule.Dispose();
                            oSetupSchedule = null;
                        }

                    }
                }
                //COMMENTED BY SHUBHANGI 20110314 TO RESOLVED ISSUE 8791
                ////Added by Mayuri:20100108-To check whether Provider selected or not,to fix issue-#5715

                //if (_IsNodeChecked == false)
                //{
                //    MessageBox.Show("Please select Provider", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Delete Schedule 
        private void tsb_DeleteSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                #region " Delete Schedule"

                if (juc_ViewSchedule.CurrentAppointment != null)
                {
                    SingleRecurrence IsReccuring = (SingleRecurrence)Convert.ToInt32(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 3));

                    if (IsReccuring == SingleRecurrence.Recurrence || IsReccuring == SingleRecurrence.SingleInRecurrence)
                    {

                        #region "Ask for Single/Recurrence Option"

                        frmSearchModDelCriteria oCriteria = new frmSearchModDelCriteria();
                        oCriteria.SelectedForModDel = "Delete";
                        oCriteria.SelectedCriteria = "Recurrence";
                        oCriteria.ShowDialog(this);
                        if (oCriteria.SelectedResult == true)
                        {
                            Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
                            Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2));

                            if (oCriteria.SelectedCriteria == "Simple")
                            {
                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    DeleteSchedule(_TempMasterID, _TempDetailID, false);
                                    Refresh(sender);
                                }
                            }
                            else // "Recurrence";
                            {
                                if (_TempMasterID > 0 && _TempDetailID > 0)
                                {
                                    DeleteSchedule(_TempMasterID, _TempDetailID, true);
                                    Refresh(sender);
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
                            Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
                            Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2));

                            if (_TempMasterID > 0 && _TempDetailID > 0)
                            {
                                DeleteSchedule(_TempMasterID, _TempDetailID, true);
                                Refresh(sender);
                            }
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Delete, "Schedule deleted", 0,0, 0, ActivityOutCome.Success);
                        }
                    }
                }
                //COMMENTED BY SHUBHANGI 20110314 TO RESOLVED ISSUE 8791

                ////Added by Mayuri:20100108-To check whether Provider selected or not,to fix issue-#5715
                //if (_IsNodeChecked == false)
                //{
                //    MessageBox.Show("Please select Provider", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                #endregion
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Update View if Sender is Delete 
        private void Refresh(object sender)
        {
            if (sender != null)
                FillScheduleOnCalendar();
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }



        //Close
        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Help
        private void tsb_Help_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Today
        private void tsb_Today_Click(object sender, EventArgs e)
        {
            try
            {

                juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Single;
                toolStripSeparator3.Visible = true;
                tls_btnTimeNavigation.Visible = true;
                tsb_DayView.Checked = false;
                tsb_WeekView.Checked = false;
                tsb_MonthView.Checked = false;
                juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.WeekView;
                juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.DayView;
                juc_ViewSchedule.Date = DateTime.Today.Date;
              
                juc_Calendar.CurrentDate = DateTime.Today.Date;
                juc_Calendar.CurrentDate = DateTime.Now;

                juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Schedule;
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
           
        }

        //Day View 
        private void tsb_DayView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _FillStartDateTime = juc_ViewSchedule.Dates[0];
                toolStripSeparator3.Visible = true;
                //tls_btnTimeNavigation.Visible = true;
                tsb_Today.Checked = false;
                tsb_WeekView.Checked = false;
                tsb_MonthView.Checked = false;
                //Modified by Mayuri:20100106-To display day view for selected date from calendar
                this.juc_ViewSchedule.Date = _FillStartDateTime;
                //this.juc_ViewSchedule.Date = DateTime.Today.Date;
                this.juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.DayView;
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Week View 
        private void tsb_WeekView_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime _FillStartDateTime = juc_ViewSchedule.Dates[0];
                toolStripSeparator3.Visible = false;
                tls_btnTimeNavigation.Visible = false;
                tsb_Today.Checked = false;
                tsb_DayView.Checked = false;
                tsb_MonthView.Checked = false;
                //Modified by Mayuri:20100106-To display week view for selected date from calendar
                //this.juc_ViewSchedule.Date = DateTime.Today.Date;
               juc_ViewSchedule.Date = juc_Calendar.CurrentDate;
               juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.WeekView;
               juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Month View 
        private void tsb_MonthView_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime _FillStartDateTime = juc_ViewSchedule.Dates[0];
                toolStripSeparator3.Visible = false;
                tls_btnTimeNavigation.Visible = false;
                tsb_DayView.Checked = false;
                tsb_WeekView.Checked = false;
                tsb_Today.Checked = false;
                
                //this.juc_ViewSchedule.Date = DateTime.Today.Date;
             juc_ViewSchedule.Date = juc_Calendar .CurrentDate;
               juc_ViewSchedule.View = Janus.Windows.Schedule.ScheduleView.MonthView;
               juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Procedures to Fill Controls on Load"

        private void Fill_Locations()
        {
            try
            {
                //Fill Locations
                //Locations

                DataTable oTableLocations = new DataTable();
                gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();

                //Procedure call to get Locations.
                DataTable _dtLocations = new DataTable();
                _dtLocations = oLocation.GetList();
                if (_dtLocations != null)
                {
                    if (_dtLocations.Rows.Count > 0)
                    {
                        //TABLE Location structure : _dtLocations([nLocationID, sLocation, nClinicID, bIsDefault]);

                        //Define the Columns to table.
                        oTableLocations.Columns.Add("ID");
                        oTableLocations.Columns.Add("DispName");

                        //Add Blank Row to Table for "All Location" Condition
                        DataRow oRow;
                        oRow = oTableLocations.NewRow();
                        oRow[0] = "0";
                        oRow[1] = "<All Locations>"; // Added <All Location> as default to every Location Drop Down
                        oTableLocations.Rows.Add(oRow);

                        Int64 _DefaultLocationID = 0;

                        //Add Row to Table for Location
                        for (Int32 _Counter = 0; _Counter <= _dtLocations.Rows.Count - 1; _Counter++)
                        {
                            oRow = oTableLocations.NewRow();
                            oRow[0] = _dtLocations.Rows[_Counter]["nLocationID"];
                            oRow[1] = _dtLocations.Rows[_Counter]["sLocation"];
                            oTableLocations.Rows.Add(oRow);

                            //Get the Default LocationID 
                            if (Convert.ToBoolean(_dtLocations.Rows[_Counter]["bIsDefault"]) == true)
                            {
                                _DefaultLocationID = Convert.ToInt64(_dtLocations.Rows[_Counter]["nLocationID"]);
                            }
                        }

                        //Set the datatable to DropDown Locations
                        cmbLocation.DataSource = oTableLocations;

                        //Set Display and Value member from table.
                        cmbLocation.DisplayMember = "DispName";
                        cmbLocation.ValueMember = "ID";

                        //Set the Default Location selected.
                        if (_DefaultLocationID != 0)
                        {
                            //cmbLocation.SelectedValue = _DefaultLocationID;
                        }

                        cmbLocation.SelectedText = "<All Locations>";
                        //Dispose class(Location) Objects.
                        oLocation.Dispose();

                    }
                }
                // Dispose table object of Location.
                _dtLocations.Dispose();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();

            }
            catch (Exception ex)
            {
                // On Error : Show Message and continue.
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fill Departments on  Location Changed.

            //Variables Department 
            gloAppointmentScheduling.gloAppointmnetScheduleCommon oScheduleCommon = new gloAppointmentScheduling.gloAppointmnetScheduleCommon(_databaseconnectionstring);
            DataTable oTableDepartments = new DataTable();
            gloGeneralItem.gloItems oListItems = null;
            int _Counter = 0;

            //Set the DataSource to null;
           
            cmbDepartment.DataSource = null;
            cmbDepartment.Items.Clear();
            try
            {
                //Departments                
                //If Location Selected add the respective Depts..                    
                if (cmbLocation.SelectedIndex != 0)
                {
                    oListItems = oScheduleCommon.GetDepartments(Convert.ToInt64(((System.Data.DataRowView)(cmbLocation.Items[cmbLocation.SelectedIndex])).Row.ItemArray[0]));

                    //Define Columns for the Table
                    oTableDepartments.Columns.Add("ID");
                    oTableDepartments.Columns.Add("DispName");

                    //Add Blank Row to Table for "All Dept" Condition
                    DataRow oRow;
                    oRow = oTableDepartments.NewRow();
                    oTableDepartments.Rows.Add(oRow);

                    //Add Row to Table for Dept
                    if (oListItems != null)
                    {
                        for (_Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
                        {
                            oRow = oTableDepartments.NewRow();
                            oRow[0] = oListItems[_Counter].ID;
                            oRow[1] = oListItems[_Counter].Description;
                            oTableDepartments.Rows.Add(oRow);
                        }
                    }
                    //Set the datatable to DropDown Depts
                    cmbDepartment.DataSource = oTableDepartments;

                    //Set Display and Value member
                    cmbDepartment.DisplayMember = "DispName";
                    cmbDepartment.ValueMember = "ID";

                    //object dispose.
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
                //Display Exception Information message.
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return;
            }
            finally
            {
                //Dispose the Class object.
                oScheduleCommon.Dispose();
                _Counter = 0;
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }

            //Refresh the Calender Control by pulling the Schedules on it.
            FillScheduleOnCalendar();
            
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //On Department change Fill the Calendar with respective Depts.
            //Refresh the Calender Control by pulling the Schedules on it
            FillScheduleOnCalendar();
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void Fill_Resources()
        {
            //Fill resources.
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dt = new DataTable();

            //Procedure call to get Resources.
            //AB_Resource_MST.nResourceID, AB_Resource_MST.sDescription , AB_Resource_MST.nResourceTypeID
            dt = oResource.GetResources();

            /////Code may not in use
            //Define Cols for table.
            DataTable dt_Resources = new DataTable();
            dt_Resources.Columns.Add("ID");
            dt_Resources.Columns.Add("Description");
            dt_Resources.Columns.Add("ResourceType");

            //Blank row for "All Resource"
            DataRow oRow = dt_Resources.NewRow();
            dt_Resources.Rows.Add(oRow);

            //Add resource to table
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                oRow = dt_Resources.NewRow();
                oRow[0] = dt.Rows[i]["nResourceID"];
                oRow[1] = dt.Rows[i]["sDescription"];
                oRow[2] = dt.Rows[i]["nResourceTypeID"];
                //nResourceTypeID
                dt_Resources.Rows.Add(oRow);
            }

            //Bind the table to DropDown.
            cmbResource.DataSource = dt_Resources;
            cmbResource.DisplayMember = "ID";
            cmbResource.ValueMember = "Description";
            //**Code may not in use


            //Resources

            //Clear the resources if any.
            trvResources.Nodes.Clear();

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TreeNode oNode = new TreeNode();
                    //Set the Resource values of Tree Node.
                    //Display the treeview in Format 'code Description';
                    oNode.Text = dt.Rows[i]["sCode"].ToString().Trim() + " " + dt.Rows[i]["sDescription"].ToString().Trim();

                    //In Tag passed the 'ResourceID~typeId'
                    oNode.Tag = Convert.ToInt64(dt.Rows[i]["nResourceID"]) + "~" + dt.Rows[i]["sDescription"].ToString().Trim() + "~" + ASBaseType.Resource.GetHashCode();

                    //Add the Resource to Tree Node.
                    trvResources.Nodes.Add(oNode);
                }
            }

            //Dispose Objects
            dt.Dispose();
            oResource.Dispose();
           
        }

        private void Fill_Providers()
        {
            //Fill the Providers.
            //Clear any previous Providers Treenode
            trvProvider.Nodes.Clear();

            gloAppointmentScheduling.gloAppointmnetScheduleCommon oCommon = new gloAppointmentScheduling.gloAppointmnetScheduleCommon(_databaseconnectionstring);
            gloGeneralItem.gloItems oProviders = null;

            //Get the Providers in collections.
            oProviders = oCommon.GetProviders();

            if (oProviders != null)
            {
                for (int i = 0; i <= oProviders.Count - 1; i++)
                {
                    TreeNode oNode = new TreeNode();

                    //Added to distinguish between Provider and other Resource as 'ProviderID~TypeID'.
                    oNode.Tag = oProviders[i].ID.ToString() + "~" + oProviders[i].Description + "~" + ASBaseType.Provider.GetHashCode();
                    oNode.Text = oProviders[i].Description;

                    //Provider added to tree 
                    trvProvider.Nodes.Add(oNode);
                    oNode = null;
                }
                oProviders.Clear();
                oProviders.Dispose();
                oProviders = null;
            }

            //Dispose Objects.
            
            oCommon.Dispose();

        }

        #endregion "Procedures to Fill Controls on Load"

        #region "Supporing Procedures"

        /// <summary>
        /// Purpose of Procedure: To return the At[index] element of String separated by Delimeters.
        /// By Sandip Deshmukh
        /// </summary>
        /// <param name="TagContent"></param>
        /// <param name="Delimeter"></param>
        /// <param name="Position"></param>
        /// <returns></returns>
        /// 
        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            //1. Master ID 
            //2. Detail ID 
            //3. Single/Recurrence 
            //4. Schedule Type Flag 
            //5. Line Number 

            //Var to hold the Array of splited Items. 
            string[] temp;
            try
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            return (object)"";

        }

        private void GetClinicTiming()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {
                ogloSettings.GetSetting("ClinicStartTime", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("ClinicEndTime", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + value.ToString());
                    value = null;
                }

                juc_ViewSchedule.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
                juc_ViewSchedule.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }
        private int GetNumberofColumninDayView()
        {
            //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            //object value = new object();
            //try
            //{
            //    ogloSettings.GetSetting("No of Column in Calendar", out value);
            //    if (value != null && value.ToString().Trim() != "")
            //        return Convert.ToInt16(value);
            //    else
            //        return 3;
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{
            //    value = null;
            //}
            //return 3;

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
                if (oSettings != null) { oSettings.Dispose(); }
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
                        case Interval.ThirtyMinutes:
                            tls_btnTimeNavigation_30Min_Click(null, null);
                            break;
                        default:
                            tls_btnTimeNavigation_30Min_Click(null, null);
                            break;
                    }
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


        #endregion  "Supporing Procedures"

        #region "Fill Schedules Procedure"

        //Fill Schedules On Calender
        private void FillScheduleOnCalendar()
        {
            try
            {
                DateTime _FillStartDateTime = juc_ViewSchedule.Dates[0];
                DateTime _FillEndDateTime = juc_ViewSchedule.Dates[juc_ViewSchedule.Dates.Count - 1];
              // ArrayList _FillPrvRes = new ArrayList();
              //  ASBaseType _FillPrvResType = ASBaseType.None;
                Int64 _FillClinicID = _ClinicID;
                Int64 _FillLocationID = 0;
                Int64 _FillDepartmentID = 0;
               // Int32 _DayColumn = 0;

                ArrayList _FillProviders = new ArrayList();
                ArrayList _FillResources = new ArrayList();

                juc_ViewSchedule.Appointments.Clear();
                juc_ViewSchedule.Owners.Clear();
                juc_ViewSchedule.Date = _FillStartDateTime;

                

                //for (int i = 0; i <= trvProvider.GetNodeCount(false) - 1; i++)
                //{
                //    if (trvProvider.Nodes[i].Checked == true)
                //    {
                //        Int64 PrvResID = Convert.ToInt64(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1));
                //        _FillPrvRes.Add(PrvResID);
                //        juc_ViewSchedule.Owners.Add(trvProvider.Nodes[i].Tag, trvProvider.Nodes[i].Text);
                //        _DayColumn = _DayColumn + 1;
                //    }
                //}

                //if (rbProvider.Checked == true) { _FillPrvResType = ASBaseType.Provider; }
                //if (rbResources.Checked == true) { _FillPrvResType = ASBaseType.Resource; }

                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        Int64 PrvID = Convert.ToInt64(GetTagElement(trvProvider.Nodes[i].Tag.ToString(), '~', 1));
                        _FillProviders.Add(PrvID);
                        juc_ViewSchedule.Owners.Add(trvProvider.Nodes[i].Tag, trvProvider.Nodes[i].Text);
                    }
                }

                for (int i = 0; i < trvResources.Nodes.Count; i++)
                {
                    if (trvResources.Nodes[i].Checked == true)
                    {
                        Int64 ResID = Convert.ToInt64(GetTagElement(trvResources.Nodes[i].Tag.ToString(), '~', 1));
                        _FillResources.Add(ResID);
                        juc_ViewSchedule.Owners.Add(trvResources.Nodes[i].Tag, trvResources.Nodes[i].Text);
                    }
                }


                //SHUBHANGI 20110321
                if (_FillProviders.Count <= 0 && _FillResources.Count <= 0)
                {
                    return;
                }
                //

                if (cmbLocation.SelectedIndex != 0)
                    _FillLocationID = Convert.ToInt64(cmbLocation.SelectedValue);

                if (Convert.ToInt64(cmbDepartment.SelectedIndex) != 0)
                    _FillDepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);


                gloSchedule ogloSchedule = new gloSchedule(_databaseconnectionstring);
                CalendarApointmentSchedules oSchedules = new CalendarApointmentSchedules();

                //ShortApointmentSchedules oSchedulesDetails = ogloSchedule.GetScheduleDetails(_FillStartDateTime, _FillEndDateTime, _FillProviders, _FillResources);
                // oSchedules = ogloSchedule.GetScheduleDetails(_FillStartDateTime, _FillEndDateTime, _FillPrvRes, _FillPrvResType, _FillClinicID);
                oSchedules = ogloSchedule.GetScheduleDetails(_FillStartDateTime, _FillEndDateTime, _FillProviders, _FillResources, _FillClinicID);



                Janus.Windows.Schedule.ScheduleAppointment oJUC_Schedule;
                Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_ScheduleOwner;

                for (int i = 0; i <= oSchedules.Count - 1; i++)
                {
                    oJUC_Schedule = new Janus.Windows.Schedule.ScheduleAppointment();
                    oJUC_ScheduleOwner = new Janus.Windows.Schedule.ScheduleAppointmentOwner();

                    for (int j = 0; j <= juc_ViewSchedule.Owners.Count - 1; j++)
                    {
                        Int64 OwnerID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.Owners[j].Value.ToString(), '~', 1));
                        ASBaseType OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_ViewSchedule.Owners[j].Value.ToString(), '~', 3));

                        if (OwnerID == oSchedules[i].PrvResUsrID && OwnerType == oSchedules[i].PrvResUsrFlag)
                        {
                            oJUC_ScheduleOwner = juc_ViewSchedule.Owners[j];
                            oJUC_Schedule.Owner = juc_ViewSchedule.Owners[j].Value;
                            break;
                        }
                    }


                    // oJUC_ScheduleOwner;
                    oJUC_Schedule.Text = oSchedules[i].ASText;
                    // Tag == ScheduleMasterID~ScheduleDetailID~IsRecurrence~nAppointmentFlag  
                    oJUC_Schedule.Tag = oSchedules[i].ASTag;
                    oJUC_Schedule.Description = oSchedules[i].ASDescription;
                    oJUC_Schedule.FormatStyle.BackColor = Color.FromArgb(oSchedules[i].ColorCode);
                    // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                    oJUC_Schedule.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Schedule.FormatStyle.BackColor);

                    SingleRecurrence _AppSinRec = SingleRecurrence.Single;
                    _AppSinRec = (SingleRecurrence)Convert.ToInt32(GetTagElement(oJUC_Schedule.Tag.ToString(), '~', 3));
                    if (_AppSinRec == SingleRecurrence.Recurrence)
                    {
                        oJUC_Schedule.ImageIndex1 = 1;
                    }
                    else if (_AppSinRec == SingleRecurrence.SingleInRecurrence)
                    {
                        oJUC_Schedule.ImageIndex1 = 2;
                    }

                    bool _ErrorFound = false;
                    try
                    {
                        oJUC_Schedule.EndTime = oSchedules[i].EndDateTime;
                        oJUC_Schedule.StartTime = oSchedules[i].StartDateTime;
                    }
                    catch { _ErrorFound = true; }

                    if (_ErrorFound == true)
                    {
                        try
                        {
                            oJUC_Schedule.StartTime = oSchedules[i].StartDateTime;
                            oJUC_Schedule.EndTime = oSchedules[i].EndDateTime;
                        }
                        catch { }
                    }

                    //if (oJUC_Schedule.Text.Contains(cmbLocation.Text) && (oJUC_Schedule.Text.Contains(cmbDepartment.Text)))
                    //{
                    //    juc_ViewSchedule.Appointments.Add(oJUC_Schedule);
                    //}
                    //oJUC_Schedule = null;


                    if (cmbLocation.Text == "<All Locations>")
                    {
                        if (oJUC_Schedule.Text.Contains("") && (oJUC_Schedule.Text.Contains(cmbDepartment.Text)))
                        {
                            if (oJUC_Schedule.Text.Contains("<All Locations>"))
                                oJUC_Schedule.Text = "";
                            juc_ViewSchedule.Appointments.Add(oJUC_Schedule);
                        }
                    }
                    else
                    {
                        if (((oJUC_Schedule.Text.Contains("<All Locations>")) || (oJUC_Schedule.Text.Contains(cmbLocation.Text))) && (oJUC_Schedule.Text.Contains(cmbDepartment.Text)))
                        {
                            if (oJUC_Schedule.Text.Contains("<All Locations>"))
                                oJUC_Schedule.Text = "";
                            juc_ViewSchedule.Appointments.Add(oJUC_Schedule);
                        }
                    }



                    oJUC_Schedule = null;
                }
                juc_ViewSchedule.Update();


                

                //Dispose the empty objects
                if (oSchedules != null) { oSchedules.Dispose(); }
                if (ogloSchedule != null) { ogloSchedule.Dispose(); }
            }
            catch (Exception ex)
            {
                //On Error: Show the error message and Continue.
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion "Fill Schedules Procedure"

        #region "Control Events on Schedule View Form"

        private void trvResources_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //FillScheduleOnCalendar();

            try
            {
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
                FillScheduleOnCalendar();

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
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void trvProvider_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
                FillScheduleOnCalendar();

                //Added by Mayuri:20100108-To check whether Provider selected or not,to fix issue-#5715
                _IsNodeChecked = e.Node.Checked;
                if (e.Node.Checked == true)
                {
                    Int64 PrvID = Convert.ToInt64(GetTagElement(e.Node.Tag.ToString(), '~', 1));
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.ViewSchedule, ActivityType.View, "View Schedule(" + e.Node.Text.Trim() + ")", 0, 0, PrvID, ActivityOutCome.Success);

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
                
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnSelectProvider_Click(object sender, EventArgs e)
        {
            this.trvProvider.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
            try
            {
                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = true;
                    }
                    _IsNodeChecked = true;
                }
                btnDeSelectProvider.Visible = true;
                btnSelectProvider.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvProvider.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();

            }
        }

        private void btnDeSelectProvider_Click(object sender, EventArgs e)
        {
            this.trvProvider.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
            try
            {
                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        trvProvider.Nodes[i].Checked = false;
                        _IsNodeChecked = true;
                    }
                }
                btnDeSelectProvider.Visible = false;
                btnSelectProvider.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvProvider.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();

            }
        }

        private void btnSelectResource_Click(object sender, EventArgs e)
        {
            this.trvResources.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
            try
            {
                if (trvResources.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        trvResources.Nodes[i].Checked = true;
                    }
                }
                btnSelectResource.Visible = false;
                btnDeSelectResource.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
        }

        private void btnDeSelectResource_Click(object sender, EventArgs e)
        {
            this.trvResources.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
            try
            {
                btnDeSelectResource.Visible = false;
                btnSelectResource.Visible = true;

                if (trvResources.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvResources.Nodes.Count; i++)
                    {
                        trvResources.Nodes[i].Checked = false;
                    }
                }
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.trvResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
                FillScheduleOnCalendar();
            }

        }


        private void rbProvider_CheckedChanged(object sender, EventArgs e)
        {
            //fill_Providers();
            //lblProviderResources.Text = "Providers";
            //FillScheduleOnCalendar(); 
        }

        private void rbResources_CheckedChanged(object sender, EventArgs e)
        {
            //fill_Resources();
            //lblProviderResources.Text = "Resources";
            //FillScheduleOnCalendar();
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            pnlSearchList.Visible = true;
            pnlSmallStrip.Visible = false;

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            pnlSearchList.Visible = false;
            pnlSmallStrip.Visible = true;
        }

        #endregion "Control Events on Schedule View  Form"

        #region "Calender Events"

        private void juc_ViewSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    _JanusPastePoint = e.Location;

                    juc_ViewSchedule.SelectedAppointments.Clear();

                    //By Pranit on 20111206 to check admin setting (new schedule)
                    Boolean isResult = checkSchedule();
                    if (isResult == false)
                    {
                        cmnu_Schedule_Paste.Visible = false;
                        cmnu_Schedule_Cut.Visible = false;
                        cmnu_Schedule_Copy.Visible = false;
                    }
                         

                    if (juc_ViewSchedule.HitTest(e.X, e.Y) == Janus.Windows.Schedule.HitTest.Appointment)
                    {
                        Point oPoint = new Point(e.X, e.Y);
                        juc_ViewSchedule.SelectedAppointments.Add(juc_ViewSchedule.GetAppointmentAt(oPoint));
                        juc_ViewSchedule.CurrentAppointment = juc_ViewSchedule.GetAppointmentAt(oPoint);

                        ASBaseType OwnerType = ASBaseType.None;
                        if (juc_ViewSchedule.CurrentAppointment.Owner != null) //condition Added on 20110307-To handle blank owner
                        {
                            OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(Convert.ToString(juc_ViewSchedule.CurrentAppointment.Owner), '~', 3));
                        }
                        if (OwnerType != ASBaseType.Provider)
                        {
                            juc_ViewSchedule.ContextMenuStrip = null;
                        }
                        else
                        {
                            juc_ViewSchedule.ContextMenuStrip = cmnu_ScheduleEdit;
                        }

                        cmnu_Schedule_Copy.Tag = null;
                        cmnu_Schedule_Cut.Tag = null;
                    }
                    else
                    {
                        juc_ViewSchedule.ContextMenuStrip = cmnu_ScheduleNew;
                        if (cmnu_Schedule_Copy.Tag == null && cmnu_Schedule_Cut.Tag == null)
                        {
                            cmnu_Schedule_Paste.Visible = false;
                        }
                        else
                        {
                            cmnu_Schedule_Paste.Visible = true;
                        }



                        ///Sandip Darade  20091204
                        //Bug ID 571 From mantis 
                        //dont show paste option if appointment copied from provider owned section
                        ScheduleAppointmentOwner PasteOwner;
                        PasteOwner = juc_ViewSchedule.GetOwnerAt(_JanusPastePoint);
                        if (PasteOwner != null)
                        {
                            ASBaseType OwnerType = ASBaseType.None;
                            OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(PasteOwner.Value.ToString(), '~', 3));
                            if (OwnerType != ASBaseType.Provider)
                            {
                                cmnu_Schedule_Paste.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    juc_ViewSchedule.ContextMenuStrip = null;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
        }

        // Add/Modify Schedule 
        private void juc_ViewSchedule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
                if (juc_ViewSchedule.CurrentAppointment != null)
                {
                    tsb_EditSchedule_Click(null, null);
                }
                else
                {
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
                    //Added By Pramod Nair For User Rights Mngnt
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);
                    if (oClsgloUserRights.NewSchedule)
                    {
                        HitTest HitTestInfo = juc_ViewSchedule.HitTest(e.Location);
                        if (HitTestInfo != HitTest.Schedule)
                        {
                            // if clicked on Schedule Header
                            return;
                        }
                        else if (juc_ViewSchedule.GetDateTimeAt(e.Location).TimeOfDay.TotalMinutes == 0)
                        {
                            // if clicked on day nevigation button on Calender Header 
                            return;
                        }

                        frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);

                        #region " Set initial parameters"

                        ofrmSchedule.StartTime = juc_ViewSchedule.GetDateTimeAt(e.Location);

                        if (juc_ViewSchedule.GetOwnerAt(e.Location) != null)
                        {
                            ASBaseType OwnerType = ASBaseType.None;
                            OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_ViewSchedule.GetOwnerAt(e.Location).Value.ToString(), '~', 3));
                            if (OwnerType == ASBaseType.Provider)
                            {
                                ofrmSchedule.ScheduleType = AppointmentScheduleFlag.ProviderSchedule;
                            }
                            if (OwnerType == ASBaseType.Resource)
                            {
                                ofrmSchedule.ScheduleType = AppointmentScheduleFlag.BlockedSchedule;
                            }
                            ofrmSchedule.PRUID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.GetOwnerAt(e.Location).Value.ToString(), '~', 1));
                        }

                        if (cmbLocation.SelectedIndex > 0)
                        {
                            ofrmSchedule.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                        }
                        if (cmbDepartment.SelectedIndex > 0)
                        {
                            ofrmSchedule.DepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);
                        }
                        #endregion

                        ofrmSchedule.ShowDialog(this);
                        ofrmSchedule.Dispose();
                    }
                }
                FillScheduleOnCalendar();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void juc_ViewSchedule_DatesChanged(object sender, EventArgs e)
        {
            FillScheduleOnCalendar();
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        #endregion

        #region Calender Time Intervel Events

        private void tls_btnTimeNavigation_5Min_Click(object sender, EventArgs e)
        {
            juc_ViewSchedule.Interval = Janus.Windows.Schedule.Interval.FiveMinutes;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_10Min_Click(object sender, EventArgs e)
        {
            juc_ViewSchedule.Interval = Janus.Windows.Schedule.Interval.TenMinutes;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_15Min_Click(object sender, EventArgs e)
        {
            juc_ViewSchedule.Interval = Janus.Windows.Schedule.Interval.FifteenMinutes;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void tls_btnTimeNavigation_30Min_Click(object sender, EventArgs e)
        {
            juc_ViewSchedule.Interval = Janus.Windows.Schedule.Interval.ThirtyMinutes;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        #endregion

        #region "Modify Schedule Methods and Events"

        private void juc_ViewSchedule_AppointmentChanged(object sender, Janus.Windows.Schedule.AppointmentChangeEventArgs e)
        {
            try
            {

                if (e.Appointment != null)
                {


                    //By Pranit on 20111206 to check admin setting (new schedule)
                    Boolean isResult = checkSchedule();
                    if (isResult == false)
                    {
                        MessageBox.Show("You don't have rights to access schedule. Please contact your administrator for more information.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                    ASBaseType OwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.Appointment.Owner.ToString(), '~', 3));
                    if (OwnerType == ASBaseType.Provider)
                    {
                        #region "Clinic Timing validations"

                        Int32 _nProposedStartTime1 = gloDateMaster.gloDate.DateAsNumber(e.Appointment.StartTime.ToShortDateString());
                        Int32 _nProposedEndTime1 = gloDateMaster.gloDate.DateAsNumber(e.Appointment.EndTime.ToShortDateString());


                        if (_nProposedStartTime1 != _nProposedEndTime1)
                        {
                            return;
                        }

                        //Clinic Timing validations
                        Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
                        Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
                        Int32 _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(e.Appointment.StartTime.ToShortTimeString());
                        Int32 _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(e.Appointment.EndTime.ToShortTimeString());

                        DialogResult _DialogResult = DialogResult.None;

                        if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
                        {
                            _DialogResult = MessageBox.Show("Schedule is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (_DialogResult == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
                        {
                            _DialogResult = MessageBox.Show("Schedule is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (_DialogResult == DialogResult.No)
                            {
                                return;
                            }
                        }
                        #endregion

                        #region " Get Schedule Information "

                        ShortApointmentSchedule oShortSchedule = new ShortApointmentSchedule();

                        oShortSchedule.MasterID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 1));
                        oShortSchedule.DetailID = Convert.ToInt64(GetTagElement(e.Appointment.Tag.ToString(), '~', 2));
                        oShortSchedule.StartDate = e.Appointment.StartTime;
                        oShortSchedule.StartTime = e.Appointment.StartTime;
                        oShortSchedule.EndDate = e.Appointment.EndTime;
                        oShortSchedule.EndTime = e.Appointment.EndTime;
                        oShortSchedule.ASCommonFlag = ASBaseType.Provider;
                        oShortSchedule.ASCommonID = Convert.ToInt64(GetTagElement(e.Appointment.Owner.ToString(), '~', 1));
                        oShortSchedule.ASCommonCode = "";
                        oShortSchedule.ASCommonDescription = Convert.ToString(GetTagElement(e.Appointment.Owner.ToString(), '~', 2));

                        #endregion " Get Appointment Information "

                        UpdateSchedule(oShortSchedule);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }

        }

        private void juc_ViewSchedule_DroppingAppointment(object sender, Janus.Windows.Schedule.DroppingAppointmentEventArgs e)
        {
            if (e.Appointment != null)
              
            {
                ASBaseType OwnerType = ASBaseType.None;
                ASBaseType ProposedOwnerType = ASBaseType.None;
                OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.Appointment.Owner.ToString(), '~', 3));
                ProposedOwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.ProposedOwner.ToString(), '~', 3));
                //Condition modified by Mayuri:20100115-To fix issue:#5774- In schedule add notes->100 character then page crash.   
                
                if (OwnerType != ASBaseType.Provider || ProposedOwnerType != ASBaseType.Provider || e.ProposedStartTime.ToShortTimeString() == e.ProposedEndTime.ToShortTimeString())
                {
                    e.Cancel = true;
                }
            }
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            #region "Commented Code"
            //bool _canModify = true;
            //try
            //{
            //    //if (e.Appointment.Tag != null)
            //    //{
            //    //    //Appointment.Tag ==  ScheduleMasterID~ScheduleDetailID~ScheduleType~PRUType~IsRecurrence  

            //    //    ASBaseType PRUType = (ASBaseType)GetTagElement(e.Appointment.Tag.ToString(), '~', 4);                    
            //    //    ASBaseType OwnerType = (ASBaseType)GetTagElement(e.ProposedOwner.ToString(), '~', 2);
            //    //    AppointmentScheduleFlag ScheduleType = (AppointmentScheduleFlag)GetTagElement(e.Appointment.Tag.ToString(), '~', 3);

            //    //    //Blocked Schedule can not be modified
            //    //    if (ScheduleType == AppointmentScheduleFlag.BlockedSchedule)
            //    //    {
            //    //        MessageBox.Show(" Can not modify Blocked schedule", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //        _canModify = false; 
            //    //    }

            //    //    //Provider can not be moved to resource And Resource can not be moved to Provider 
            //    //    if (PRUType != OwnerType)  
            //    //    {
            //    //        if (PRUType == ASBaseType.Resource)
            //    //        {
            //    //            MessageBox.Show(" Can not move Resource schedule to Provider ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //            _canModify = false; 
            //    //        }
            //    //        if (PRUType == ASBaseType.Provider)
            //    //        {
            //    //            MessageBox.Show(" Can not move Provider schedule to Resource ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //            _canModify = false; 
            //    //        }
            //    //    }
            //    //    else 
            //    //    {
            //    //        //Resource1 can not be moved to Resource2 in Resource Schedule 
            //    //        if(PRUType == ASBaseType.Resource  && ScheduleType == AppointmentScheduleFlag.ResourceSchedule)
            //    //        {
            //    //            MessageBox.Show(" Can not move Resource to another Resource in Resource Schedule", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //            _canModify = false; 
            //    //        }

            //    //    }

            //    //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //if (_canModify == false)
            //{
            //    FillScheduleOnCalendar();
            //}

            #endregion
        }

        private void juc_ViewSchedule_AppointmentChanging(object sender, Janus.Windows.Schedule.AppointmentChangeCancelEventArgs e)
        {
            try
            {
                if (e.Appointment != null)
                {
                    ASBaseType OwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(e.Appointment.Owner.ToString(), '~', 3));

                    if (OwnerType == ASBaseType.Resource)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
        }

        private void UpdateSchedule(ShortApointmentSchedule oShortSchedule)
        {
            gloAppointmentScheduling.gloSchedule ogloSchedule = null;
            gloAppointmentScheduling.MasterSchedule oMasterSchedule = null;
            Int64 _retValue = 0;
            object _CheckValue = new object();
            string flag = "Single";

            try
            {
                if (oShortSchedule != null)
                {
                    ogloSchedule = new gloSchedule(_databaseconnectionstring);
                    oMasterSchedule = new MasterSchedule();
                    oMasterSchedule = ogloSchedule.GetMasterSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, false, this._ClinicID);

                    if (oMasterSchedule != null)
                    {
                        #region " Ask User for Modify "

                        frmModifyScheduleCriteria ofrmModifyScheduleCriteria = new frmModifyScheduleCriteria();
                        ofrmModifyScheduleCriteria.ProblemTypes = oMasterSchedule.ProblemTypes;
                        ofrmModifyScheduleCriteria.Resources = oMasterSchedule.Resources;
                        ofrmModifyScheduleCriteria.NewStartTime = oShortSchedule.StartTime;
                        ofrmModifyScheduleCriteria.NewEndTime = oShortSchedule.EndTime;

                        if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                        {
                            ofrmModifyScheduleCriteria.MessageText = "Do you want to modify this Schedule ?";
                            flag = "Single";
                        }
                        else if (oMasterSchedule.IsRecurrence == SingleRecurrence.Recurrence)
                        {
                            ofrmModifyScheduleCriteria.MessageText = "This is Recurrence Schedule. Do you want to modify this occurrence ?";
                            flag = "Recurrence";
                        }

                        DialogResult _dialogResult = ofrmModifyScheduleCriteria.ShowDialog(this);

                        if (_dialogResult != DialogResult.OK)
                        {
                            ofrmModifyScheduleCriteria.Dispose();
                            return;
                        }

                        oMasterSchedule.ProblemTypes = ofrmModifyScheduleCriteria.ProblemTypes;
                        oMasterSchedule.Resources = ofrmModifyScheduleCriteria.Resources;

                        ofrmModifyScheduleCriteria.Dispose();
                        #endregion

                        //Make new single entry for the Schedule
                        #region "Master Schedule Data"

                        oMasterSchedule.MasterID = 0;
                        oMasterSchedule.IsRecurrence = SingleRecurrence.Single;
                       // oMasterSchedule.ASFlag = AppointmentScheduleFlag.ProviderSchedule;

                        

                        _CheckValue = oMasterSchedule.ScheduleTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterSchedule.ScheduleTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterSchedule.ScheduleTypeDesc;// = cmbApp_AppointmentType.Text;



                        if (oMasterSchedule.ASFlag != AppointmentScheduleFlag.BlockedSchedule)
                        {
                            oMasterSchedule.ASBaseID = oShortSchedule.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                            oMasterSchedule.ASBaseCode = oShortSchedule.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                            oMasterSchedule.ASBaseDescription = oShortSchedule.ASCommonDescription; // cmbApp_Provider.Text;
                            oMasterSchedule.ASBaseFlag = ASBaseType.Provider;
                        }
                        
                        //oMasterSchedule.ASBaseID = oShortSchedule.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        //oMasterSchedule.ASBaseCode = oShortSchedule.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        //oMasterSchedule.ASBaseDescription = oShortSchedule.ASCommonDescription; // cmbApp_Provider.Text;
                        //oMasterSchedule.ASBaseFlag = ASBaseType.Provider;
                      

                        oMasterSchedule.StartDate = oShortSchedule.StartDate;
                        oMasterSchedule.StartTime = oShortSchedule.StartTime;
                        oMasterSchedule.EndDate = oShortSchedule.EndDate;
                        oMasterSchedule.EndTime = oShortSchedule.EndTime;
                        oMasterSchedule.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;

                        _CheckValue = oMasterSchedule.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        _CheckValue = oMasterSchedule.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterSchedule.LocationName; // = cmbApp_Location.Text;
                        _CheckValue = oMasterSchedule.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterSchedule.DepartmentName; // = cmbApp_Department.Text;
                        _CheckValue = oMasterSchedule.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterSchedule.ClinicID; // = _SetAppointmentParameter.ClinicID;

                        oMasterSchedule.UsedStatus = ASUsedStatus.NotUsed;

                        #endregion

                        #region "Schedule Criteria"

                        oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.StartDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.EndDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterSchedule.ProblemTypes.Count; i++)
                        {

                            oMasterSchedule.ProblemTypes[i].MasterID = 0;
                            oMasterSchedule.ProblemTypes[i].DetailID = 0;
                            oMasterSchedule.ProblemTypes[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].PatientID;
                            oMasterSchedule.ProblemTypes[i].LineNo = 0;
                            oMasterSchedule.ProblemTypes[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterSchedule.ProblemTypes[i].StartDate = oShortSchedule.StartDate;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].StartTime;    //oMasterSchedule.ProblemTypes[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.ProblemTypes[i].EndDate = oShortSchedule.EndDate;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].EndTime;    //oMasterSchedule.ProblemTypes[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.ProblemTypes[i].ViewOtherDetails = "";
                            oMasterSchedule.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                        }

                        #endregion

                        #region "Resources"

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            oMasterSchedule.Resources[i].MasterID = 0;
                            oMasterSchedule.Resources[i].DetailID = 0;
                            oMasterSchedule.Resources[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            _CheckValue = oMasterSchedule.Resources[i].PatientID;
                            oMasterSchedule.Resources[i].LineNo = 0;
                            oMasterSchedule.Resources[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.Resources[i].ASCommonFlag = ASBaseType.Resource;

                            oMasterSchedule.Resources[i].StartDate = oShortSchedule.StartDate;
                            _CheckValue = oMasterSchedule.Resources[i].StartTime; //oMasterSchedule.Resources[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.Resources[i].EndDate = oShortSchedule.EndDate;
                            _CheckValue = oMasterSchedule.Resources[i].EndTime;  //oMasterSchedule.Resources[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.Resources[i].ViewOtherDetails = "";
                            oMasterSchedule.Resources[i].UsedStatus = ASUsedStatus.NotUsed;

                        }

                        #endregion


                        #region " Users "
                        for (int i = 0; i < oMasterSchedule.Users.Count; i++)
                        {
                            oMasterSchedule.Users[i].MasterID = 0;
                            oMasterSchedule.Users[i].DetailID = 0;
                            oMasterSchedule.Users[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            //oMasterSchedule.Resources[i].PatientID = oMasterSchedule.PatientID;
                            oMasterSchedule.Users[i].LineNo = 0;
                            oMasterSchedule.Users[i].ASFlag = oMasterSchedule.ASFlag;

                            if (oMasterSchedule.ASFlag == AppointmentScheduleFlag.BlockedSchedule)
                            {
                                oMasterSchedule.Users[i].ASCommonID = oShortSchedule.ASCommonID;
                                oMasterSchedule.Users[i].ASCommonCode = oShortSchedule.ASCommonCode;
                                oMasterSchedule.Users[i].ASCommonDescription = oShortSchedule.ASCommonDescription;
                                oMasterSchedule.Users[i].ASCommonFlag = ASBaseType.Provider;
                            }
                            else
                            {
                                _CheckValue = oMasterSchedule.Users[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                _CheckValue = oMasterSchedule.Users[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                                _CheckValue = oMasterSchedule.Users[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                                oMasterSchedule.Users[i].ASCommonFlag = ASBaseType.User;
                            }

                            oMasterSchedule.Users[i].StartDate = oShortSchedule.StartDate;
                            oMasterSchedule.Users[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.Users[i].EndDate = oShortSchedule.EndDate;
                            oMasterSchedule.Users[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.Users[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.Users[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.Users[i].ViewOtherDetails = "";

                        }

                        #endregion





                        _retValue = ogloSchedule.Add(oMasterSchedule, 0);

                        //Delete the Reccurance entry of the appointment

                        if (_retValue > 0)
                        {
                            bool _DeleteMaster = false;
                            //if (oShortSchedule.IsRecurrence == SingleRecurrence.Recurrence)
                            //    _DeleteMaster = false;
                            //else if (oShortSchedule.IsRecurrence == SingleRecurrence.Single)
                            //    _DeleteMaster = true;
                            if (flag=="Recurrence")
                                _DeleteMaster = false;
                            else if (flag == "Single")
                                _DeleteMaster = true;



                            bool _IsDeleted = DeleteSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, _DeleteMaster);
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
                MessageBox.Show("ERROR : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                if (oMasterSchedule != null) { oMasterSchedule.Dispose(); }
                if (_CheckValue != null) { _CheckValue = null; }
                if (oShortSchedule != null) { oShortSchedule.Dispose(); }
            }
        }

        #region "Old Update Schedule Methods"

        private void UpdateSingleSchedule_Old(ShortApointmentSchedule oShortSchedule)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
         //   bool _IsUpdated = false;
            int retValue = 0;

            try
            {
                oDB.Connect(false);
                if (oShortSchedule != null)
                {
                    //1.First Update the Master Table entry
                    _sqlQuery = " UPDATE AS_Schedule_MST SET "
                               + " nASBaseID = " + oShortSchedule.ASCommonID + " , "
                               + " sASBaseCode= '" + oShortSchedule.ASCommonCode + "' ,"
                               + " sASBaseDesc= '" + oShortSchedule.ASCommonDescription + "' ,"
                               + " nASBaseFlag= " + oShortSchedule.ASCommonFlag.GetHashCode() + " ,"
                               + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortSchedule.StartDate.ToString()) + " ,"
                               + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToString()) + " ,"
                               + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortSchedule.EndDate.ToString()) + " ,"
                               + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToString()) + " "
                               + " WHERE nMSTScheduleID = " + oShortSchedule.MasterID + "  AND  nClinicID = " + this._ClinicID + "  ";

                    retValue = oDB.Execute_Query(_sqlQuery);

                    //2.Check if the Mater entry is modified ,If yes Update the Detail Table entry
                    if (retValue > 0)
                    {
                        _sqlQuery = " UPDATE AS_Schedule_DTL SET "
                                   + " nASBaseID = " + oShortSchedule.ASCommonID + " , "
                                   + " sASBaseCode= '" + oShortSchedule.ASCommonCode + "' ,"
                                   + " sASBaseDesc= '" + oShortSchedule.ASCommonDescription + "' ,"
                                   + " nASBaseFlag= " + oShortSchedule.ASCommonFlag.GetHashCode() + " ,"
                                   + " dtStartDate= " + gloDateMaster.gloDate.DateAsNumber(oShortSchedule.StartDate.ToString()) + " ,"
                                   + " dtStartTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToString()) + " ,"
                                   + " dtEndDate= " + gloDateMaster.gloDate.DateAsNumber(oShortSchedule.EndDate.ToString()) + " ,"
                                   + " dtEndTime= " + gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToString()) + " "
                                   + " WHERE nMSTScheduleID = " + oShortSchedule.MasterID + " AND nDTLScheduleID = " + oShortSchedule.DetailID + " "
                                   + " AND  nClinicID = " + this._ClinicID + " ";

                        retValue = oDB.Execute_Query(_sqlQuery);
                        if (retValue > 0)
                        { 
                        //    _IsUpdated = true; 
                        }
                    }

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

        private void UpdateReccuranceSchedule_Old(ShortApointmentSchedule oShortSchedule)
        {
            gloAppointmentScheduling.gloSchedule ogloSchedule = null;
            gloAppointmentScheduling.MasterSchedule oMasterSchedule = null;
            Int64 _retValue = 0;
            object _CheckValue = new object();

            try
            {
                if (oShortSchedule != null)
                {
                    ogloSchedule = new gloSchedule(_databaseconnectionstring);
                    oMasterSchedule = new MasterSchedule();
                    oMasterSchedule = ogloSchedule.GetMasterSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, true, this._ClinicID);

                    if (oMasterSchedule != null)
                    {

                        //Make new single entry for the Schedule

                        #region "Master Schedule Data"

                        oMasterSchedule.MasterID = 0;
                        oMasterSchedule.IsRecurrence = SingleRecurrence.Single;
                        oMasterSchedule.ASFlag = AppointmentScheduleFlag.ProviderSchedule;

                        _CheckValue = oMasterSchedule.ScheduleTypeID; //Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                        _CheckValue = oMasterSchedule.ScheduleTypeCode;// = cmbApp_AppointmentType.Text;//Remark
                        _CheckValue = oMasterSchedule.ScheduleTypeDesc;// = cmbApp_AppointmentType.Text;

                        oMasterSchedule.ASBaseID = oShortSchedule.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                        oMasterSchedule.ASBaseCode = oShortSchedule.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                        oMasterSchedule.ASBaseDescription = oShortSchedule.ASCommonDescription; // cmbApp_Provider.Text;
                        oMasterSchedule.ASBaseFlag = ASBaseType.Provider;

                        oMasterSchedule.StartDate = oShortSchedule.StartDate;
                        oMasterSchedule.StartTime = oShortSchedule.StartTime;
                        oMasterSchedule.EndDate = oShortSchedule.EndDate;
                        oMasterSchedule.EndTime = oShortSchedule.EndTime;
                        oMasterSchedule.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;

                        _CheckValue = oMasterSchedule.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                        _CheckValue = oMasterSchedule.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterSchedule.LocationName; // = cmbApp_Location.Text;
                        _CheckValue = oMasterSchedule.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterSchedule.DepartmentName; // = cmbApp_Department.Text;
                        _CheckValue = oMasterSchedule.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterSchedule.ClinicID; // = _SetAppointmentParameter.ClinicID;

                        oMasterSchedule.UsedStatus = ASUsedStatus.NotUsed;

                        #endregion

                        #region "Schedule Criteria"

                        oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.StartDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.EndDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterSchedule.ProblemTypes.Count; i++)
                        {

                            oMasterSchedule.ProblemTypes[i].MasterID = 0;
                            oMasterSchedule.ProblemTypes[i].DetailID = 0;
                            oMasterSchedule.ProblemTypes[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].PatientID;
                            oMasterSchedule.ProblemTypes[i].LineNo = 0;
                            oMasterSchedule.ProblemTypes[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterSchedule.ProblemTypes[i].StartDate = oShortSchedule.StartDate;
                            oMasterSchedule.ProblemTypes[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.ProblemTypes[i].EndDate = oShortSchedule.EndDate;
                            oMasterSchedule.ProblemTypes[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.ProblemTypes[i].ViewOtherDetails = "";
                            oMasterSchedule.ProblemTypes[i].UsedStatus = ASUsedStatus.NotUsed;
                        }

                        #endregion

                        #region "Resources"

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            oMasterSchedule.Resources[i].MasterID = 0;
                            oMasterSchedule.Resources[i].DetailID = 0;
                            oMasterSchedule.Resources[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            _CheckValue = oMasterSchedule.Resources[i].PatientID;
                            oMasterSchedule.Resources[i].LineNo = 0;
                            oMasterSchedule.Resources[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.Resources[i].ASCommonFlag = ASBaseType.Resource;

                            oMasterSchedule.Resources[i].StartDate = oShortSchedule.StartDate;
                            oMasterSchedule.Resources[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.Resources[i].EndDate = oShortSchedule.EndDate;
                            oMasterSchedule.Resources[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.Resources[i].ViewOtherDetails = "";
                            oMasterSchedule.Resources[i].UsedStatus = ASUsedStatus.NotUsed;

                        }

                        #endregion


                        _retValue = ogloSchedule.Add(oMasterSchedule, 0);

                        //Delete the Reccurance entry of the appointment

                        if (_retValue > 0)
                        {
                            bool _IsDeleted = DeleteSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, false);
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
                MessageBox.Show("ERROR : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                if (oMasterSchedule != null) { oMasterSchedule.Dispose(); }
                if (_CheckValue != null) { _CheckValue = null; }
                if (oShortSchedule != null) { oShortSchedule.Dispose(); }
            }
        }

        #endregion

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

        private void PasteSchedule_Click(CutCopyPaste oCutCopy)
        {
            TimeSpan tsPasteTime = new TimeSpan(0, 0, 0);
            DateTime dtDate;
            ScheduleAppointmentOwner PasteOwner;
            Janus.Windows.Schedule.ScheduleAppointment oJUC_Schedule;
            ShortApointmentSchedule oShortSchedule = null;

            try
            {
                if (_JanusPastePoint.IsEmpty == false)
                {
                    if (oCutCopy == CutCopyPaste.Copy && cmnu_Schedule_Copy.Tag != null)
                    {
                        oJUC_Schedule = (ScheduleAppointment)cmnu_Schedule_Copy.Tag;
                    }
                    else if (oCutCopy == CutCopyPaste.Cut && cmnu_Schedule_Cut.Tag != null)
                    {
                        oJUC_Schedule = (ScheduleAppointment)cmnu_Schedule_Cut.Tag;
                    }
                    else
                    {
                        oJUC_Schedule = null;
                    }

                    #region " Get Appointment Time "

                    switch (juc_ViewSchedule.View)
                    {
                        case ScheduleView.DayView:
                            {
                                tsPasteTime = juc_ViewSchedule.GetTimeAt(_JanusPastePoint);
                            }
                            break;
                        case ScheduleView.MonthView:
                            {
                                tsPasteTime = oJUC_Schedule.StartTime.TimeOfDay;
                            }
                            break;
                        case ScheduleView.WeekView:
                            {
                                tsPasteTime = oJUC_Schedule.StartTime.TimeOfDay;
                            }
                            break;
                        case ScheduleView.WorkWeek:
                            {
                                tsPasteTime = juc_ViewSchedule.GetTimeAt(_JanusPastePoint);
                            }
                            break;
                        default:
                            {
                                tsPasteTime = oJUC_Schedule.StartTime.TimeOfDay;
                            }
                            break;
                    }

                    #endregion " Get Appointment Time "

                    #region " Get New Schedule Date,Owner Information "

                    dtDate = juc_ViewSchedule.GetDateAt(_JanusPastePoint);
                    PasteOwner = juc_ViewSchedule.GetOwnerAt(_JanusPastePoint);
                    // TimeSpan tsScheduleDuration = oJUC_Schedule.EndTime.TimeOfDay.Subtract(oJUC_Schedule.StartTime.TimeOfDay);
                    TimeSpan tsScheduleDuration = oJUC_Schedule.Duration;

                    #endregion

                    ASBaseType OwnerType = ASBaseType.None;
                    OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(PasteOwner.Value.ToString(), '~', 3));
                    if (OwnerType != ASBaseType.Provider)
                    {
                        return;
                    }

                    #region " Get Schedule Information "

                    oShortSchedule = new ShortApointmentSchedule();

                    oShortSchedule.MasterID = Convert.ToInt64(GetTagElement(oJUC_Schedule.Tag.ToString(), '~', 1));
                    oShortSchedule.DetailID = Convert.ToInt64(GetTagElement(oJUC_Schedule.Tag.ToString(), '~', 2));
                    oShortSchedule.ASFlag = (AppointmentScheduleFlag)Convert.ToInt32(GetTagElement(oJUC_Schedule.Tag.ToString(), '~', 4));
                    oShortSchedule.StartDate = dtDate;
                    oShortSchedule.StartTime = Convert.ToDateTime(dtDate.ToShortDateString() + " " + tsPasteTime.Hours + ":" + tsPasteTime.Minutes + ":" + tsPasteTime.Seconds);
                    oShortSchedule.EndDate = oShortSchedule.StartTime.Add(tsScheduleDuration);
                    oShortSchedule.EndTime = oShortSchedule.StartTime.Add(tsScheduleDuration);
                    oShortSchedule.ASCommonFlag = ASBaseType.Provider;
                    oShortSchedule.ASCommonID = Convert.ToInt64(GetTagElement(PasteOwner.Value.ToString(), '~', 1));
                    oShortSchedule.ASCommonCode = "";
                    oShortSchedule.ASCommonDescription = Convert.ToString(GetTagElement(PasteOwner.Value.ToString(), '~', 2));
                    oShortSchedule.IsRecurrence = (SingleRecurrence)Convert.ToInt32(GetTagElement(oJUC_Schedule.Tag.ToString(), '~', 3));

                    #endregion " Get Schedule Information "

                    #region "Clinic Timing validations"

                    //Clinic Timing validations
                    Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
                    Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
                    Int32 _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToShortTimeString());
                    Int32 _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToShortTimeString());

                    DialogResult _DialogResult = DialogResult.None;

                    if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
                    {
                        _DialogResult = MessageBox.Show("Schedule is outside clinic time.  Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (_DialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
                    {
                        _DialogResult = MessageBox.Show("Schedule is outside clinic time.  Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (_DialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                    #endregion

                    MakePasteSchedule(oShortSchedule, oCutCopy);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                cmnu_Schedule_Copy.Tag = null;
                cmnu_Schedule_Cut.Tag = null;
                if (oShortSchedule != null) { oShortSchedule.Dispose(); }
                oJUC_Schedule = null;
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
        }

        private void MakePasteSchedule(ShortApointmentSchedule oShortSchedule, CutCopyPaste oCutCopy)
        {

            gloAppointmentScheduling.gloSchedule ogloSchedule = null;
            gloAppointmentScheduling.MasterSchedule oMasterSchedule = null;
            Int64 _retValue = 0;
            object _CheckValue = new object();

            try
            {
                if (oShortSchedule != null)
                {
                    ogloSchedule = new gloSchedule(_databaseconnectionstring);
                    oMasterSchedule = new MasterSchedule();
                    bool RetriveSingleInRecurrence = true;

                    if (oShortSchedule.ASFlag == AppointmentScheduleFlag.BlockedSchedule)
                        RetriveSingleInRecurrence = false;
                    else
                        RetriveSingleInRecurrence = true;

                    oMasterSchedule = ogloSchedule.GetMasterSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, SingleRecurrence.Single, SingleRecurrence.Recurrence, RetriveSingleInRecurrence, this._ClinicID);

                    if (oMasterSchedule != null)
                    {
                        //Make new single entry for the appointment
                        #region " Ask User for Modify "

                        frmModifyScheduleCriteria ofrmModifyScheduleCriteria = new frmModifyScheduleCriteria();
                        ofrmModifyScheduleCriteria.ProblemTypes = oMasterSchedule.ProblemTypes;
                        ofrmModifyScheduleCriteria.Resources = oMasterSchedule.Resources;
                        ofrmModifyScheduleCriteria.NewStartTime = oShortSchedule.StartTime;
                        ofrmModifyScheduleCriteria.NewEndTime = oShortSchedule.EndTime;

                        if (oCutCopy == CutCopyPaste.Cut && oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                        {
                            ofrmModifyScheduleCriteria.MessageText = "Do you want to modify this Schedule ?";
                        }
                        else if (oCutCopy == CutCopyPaste.Cut && oMasterSchedule.IsRecurrence == SingleRecurrence.Recurrence)
                        {
                            ofrmModifyScheduleCriteria.MessageText = "This is Recuurrence Schedule. Do you want to modify this occurrence ?";
                        }
                        if (oCutCopy == CutCopyPaste.Copy)
                        {
                            ofrmModifyScheduleCriteria.MessageText = "Do you want to create this Schedule ?";
                        }

                        DialogResult _dialogResult = ofrmModifyScheduleCriteria.ShowDialog(this);

                        if (_dialogResult != DialogResult.OK)
                        {
                            ofrmModifyScheduleCriteria.Dispose();
                            return;
                        }

                        oMasterSchedule.ProblemTypes = ofrmModifyScheduleCriteria.ProblemTypes;
                        oMasterSchedule.Resources = ofrmModifyScheduleCriteria.Resources;

                        ofrmModifyScheduleCriteria.Dispose();

                        #endregion

                        #region "Master Schedule Data"

                        //if Modify Appointment and 

                        oMasterSchedule.MasterID = 0;
                        oMasterSchedule.IsRecurrence = SingleRecurrence.Single;
                        oMasterSchedule.ASFlag = oShortSchedule.ASFlag;

                        if (oMasterSchedule.ASFlag != AppointmentScheduleFlag.BlockedSchedule)
                        {
                            oMasterSchedule.ASBaseID = oShortSchedule.ASCommonID;   //  Convert.ToInt64(cmbApp_Provider.SelectedValue);
                            oMasterSchedule.ASBaseCode = oShortSchedule.ASCommonCode;  //cmbApp_Provider.Text; //Remark
                            oMasterSchedule.ASBaseDescription = oShortSchedule.ASCommonDescription; // cmbApp_Provider.Text;
                            oMasterSchedule.ASBaseFlag = ASBaseType.Provider;
                        }

                        oMasterSchedule.StartDate = oShortSchedule.StartDate;
                        oMasterSchedule.StartTime = oShortSchedule.StartTime;
                        oMasterSchedule.EndDate = oShortSchedule.EndDate;
                        oMasterSchedule.EndTime = oShortSchedule.EndTime;
                        oMasterSchedule.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;
                        _CheckValue = oMasterSchedule.ColorCode;  // lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                        _CheckValue = oMasterSchedule.LocationID; // = Convert.ToInt64(cmbApp_Location.SelectedValue);
                        _CheckValue = oMasterSchedule.LocationName; // = cmbApp_Location.Text;

                        _CheckValue = oMasterSchedule.DepartmentID; // = Convert.ToInt64(cmbApp_Department.SelectedValue);
                        _CheckValue = oMasterSchedule.DepartmentName; // = cmbApp_Department.Text;

                        _CheckValue = oMasterSchedule.Notes; // = txtApp_Notes.Text;
                        _CheckValue = oMasterSchedule.ClinicID; // = _SetAppointmentParameter.ClinicID;

                        #endregion

                        #region "Schedule Criteria"

                        oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.StartDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.StartTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oShortSchedule.EndDate.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oShortSchedule.EndTime.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.Duration = (Decimal)((TimeSpan)(oShortSchedule.EndTime.TimeOfDay.Subtract(oShortSchedule.StartTime.TimeOfDay))).TotalMinutes;

                        #endregion

                        #region "Problem Types"

                        for (int i = 0; i < oMasterSchedule.ProblemTypes.Count; i++)
                        {

                            oMasterSchedule.ProblemTypes[i].MasterID = 0;
                            oMasterSchedule.ProblemTypes[i].DetailID = 0;
                            oMasterSchedule.ProblemTypes[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            //oMasterSchedule.ProblemTypes[i].PatientID = oMasterSchedule.PatientID;
                            oMasterSchedule.ProblemTypes[i].LineNo = 0;
                            oMasterSchedule.ProblemTypes[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonID; // = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonCode; // = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ASCommonDescription; // = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.ProblemTypes[i].ASCommonFlag = ASBaseType.ProblemType;

                            oMasterSchedule.ProblemTypes[i].StartDate = oShortSchedule.StartDate;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].StartTime; //oMasterSchedule.ProblemTypes[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.ProblemTypes[i].EndDate = oShortSchedule.EndDate;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].EndTime; //oMasterSchedule.ProblemTypes[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();
                            _CheckValue = oMasterSchedule.ProblemTypes[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.ProblemTypes[i].ViewOtherDetails = "";
                        }

                        #endregion

                        #region "Resources"

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            oMasterSchedule.Resources[i].MasterID = 0;
                            oMasterSchedule.Resources[i].DetailID = 0;
                            oMasterSchedule.Resources[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            //oMasterSchedule.Resources[i].PatientID = oMasterSchedule.PatientID;
                            oMasterSchedule.Resources[i].LineNo = 0;
                            oMasterSchedule.Resources[i].ASFlag = oMasterSchedule.ASFlag;
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                            _CheckValue = oMasterSchedule.Resources[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                            oMasterSchedule.Resources[i].ASCommonFlag = ASBaseType.Resource;

                            oMasterSchedule.Resources[i].StartDate = oShortSchedule.StartDate;
                            _CheckValue = oMasterSchedule.Resources[i].StartTime; // oMasterSchedule.Resources[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.Resources[i].EndDate = oShortSchedule.EndDate;
                            _CheckValue = oMasterSchedule.Resources[i].EndTime; //oMasterSchedule.Resources[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.Resources[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.Resources[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.Resources[i].ViewOtherDetails = "";

                        }

                        #endregion

                        #region " Users "
                        for (int i = 0; i < oMasterSchedule.Users.Count; i++)
                        {
                            oMasterSchedule.Users[i].MasterID = 0;
                            oMasterSchedule.Users[i].DetailID = 0;
                            oMasterSchedule.Users[i].IsRecurrence = oMasterSchedule.IsRecurrence;
                            //oMasterSchedule.Resources[i].PatientID = oMasterSchedule.PatientID;
                            oMasterSchedule.Users[i].LineNo = 0;
                            oMasterSchedule.Users[i].ASFlag = oMasterSchedule.ASFlag;

                            if (oShortSchedule.ASFlag == AppointmentScheduleFlag.BlockedSchedule)
                            {
                                oMasterSchedule.Users[i].ASCommonID = oShortSchedule.ASCommonID;
                                oMasterSchedule.Users[i].ASCommonCode = oShortSchedule.ASCommonCode;
                                oMasterSchedule.Users[i].ASCommonDescription = oShortSchedule.ASCommonDescription;
                                oMasterSchedule.Users[i].ASCommonFlag = ASBaseType.Provider;
                            }
                            else
                            {
                                _CheckValue = oMasterSchedule.Users[i].ASCommonID; // = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                _CheckValue = oMasterSchedule.Users[i].ASCommonCode; // = c1Resources.GetData(i, COL_CODE).ToString();
                                _CheckValue = oMasterSchedule.Users[i].ASCommonDescription; // = c1Resources.GetData(i, COL_DESC).ToString();
                                oMasterSchedule.Users[i].ASCommonFlag = ASBaseType.User;
                            }

                            oMasterSchedule.Users[i].StartDate = oShortSchedule.StartDate;
                            oMasterSchedule.Users[i].StartTime = oShortSchedule.StartTime;
                            oMasterSchedule.Users[i].EndDate = oShortSchedule.EndDate;
                            oMasterSchedule.Users[i].EndTime = oShortSchedule.EndTime;
                            _CheckValue = oMasterSchedule.Users[i].ColorCode; // = lblApp_DateTime_ColorContainer.BackColor.ToArgb();

                            _CheckValue = oMasterSchedule.Users[i].ClinicID; // = _SetAppointmentParameter.ClinicID;
                            oMasterSchedule.Users[i].ViewOtherDetails = "";

                        }

                        #endregion

                        _retValue = ogloSchedule.Add(oMasterSchedule, 0);

                        if (oCutCopy == CutCopyPaste.Cut)
                        {
                            if (_retValue > 0)
                            {
                                bool _IsDeleted = false;
                                if (oShortSchedule.IsRecurrence == SingleRecurrence.Recurrence)
                                {
                                    _IsDeleted = DeleteSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, false);
                                }
                                else if (oShortSchedule.IsRecurrence == SingleRecurrence.Single)
                                {
                                    _IsDeleted = DeleteSchedule(oShortSchedule.MasterID, oShortSchedule.DetailID, true);
                                }
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
                MessageBox.Show("ERROR : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                if (oMasterSchedule != null) { oMasterSchedule.Dispose(); }
                if (_CheckValue != null) { _CheckValue = null; }
                if (oShortSchedule != null) { oShortSchedule.Dispose(); }
            }
        }

        #endregion

        //private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        //{
        //    Font printFont = new Font("Arial", 10);
        //    string line = "test";
        //    ev.Graphics.DrawString(line, printFont, Brushes.Black, 20, 20, new StringFormat());
        //    ev.HasMorePages = false;

        //}


        #region " Context Menu Item Click Event "

        private void cmnu_ScheduleEdit_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Text)
                {
                    case "Copy":
                        {
                            #region " Copy Schedule "

                            //1.Copy the selected Appointment object in context menu tag property
                            if (juc_ViewSchedule.CurrentAppointment != null)
                            {
                                cmnu_Schedule_Copy.Tag = juc_ViewSchedule.CurrentAppointment;
                                cmnu_Schedule_Paste.Tag = CutCopyPaste.Copy;
                                cmnu_Schedule_Cut.Tag = null;
                            }

                            #endregion
                        }
                        break;
                    case "Open":
                        {
                            cmnu_ScheduleEdit.Hide();
                            tsb_EditSchedule_Click(null, null);
                        }
                        break;
                    case "Print":
                        {
                            #region " Print Schedule "
                            //if (juc_ViewSchedule.CurrentAppointment != null)
                            //{
                            //    Int64 _TempMasterID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1));
                            //    Int64 _TempDetailID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2));
                            //    if (_TempMasterID > 0 && _TempDetailID > 0)
                            //    {

                            //        MasterSchedule oMasterSchedule = new MasterSchedule();
                            //        gloSchedule ogloSchedule = new gloSchedule(_databaseconnectionstring);

                            //        System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                            //        pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
                            //        pd.Print();
                            //        //System.Drawing.Printing.StandardPrintController oPrintController = new System.Drawing.Printing.StandardPrintController();                                                                        
                            //        //printSchedule.PrintController = oPrintController;                                    
                            //        //printSchedule.Print();

                            //    }
                            //}
                            #endregion
                        }
                        break;
                    case "Delete":
                        {
                            cmnu_ScheduleEdit.Hide();
                            tsb_DeleteSchedule_Click(null, null);
                        }
                        break;
                    case "Add Notes":
                        {
                            {
                                cmnu_ScheduleEdit.Hide();
                                if (juc_ViewSchedule.CurrentAppointment != null)
                                {
                                    Int64 nMSTAppointmentID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 1).ToString());
                                    Int64 nDTLAppointmentID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.CurrentAppointment.Tag.ToString(), '~', 2).ToString());

                                    frmAddNotes ofrmAddNotes = new frmAddNotes(_databaseconnectionstring, false);
                                    ofrmAddNotes.MSTAppointmentID = nMSTAppointmentID;
                                    ofrmAddNotes.DTLAppointmentID = nDTLAppointmentID;
                                    ofrmAddNotes.ShowDialog(this);
                                    ofrmAddNotes.Dispose();
                                }
                            }
                        }
                        break;
                    case "Cut":
                        {
                            #region "Cut Schedule"

                            if (juc_ViewSchedule.CurrentAppointment != null)
                            {
                                cmnu_ScheduleEdit.Hide();
                                cmnu_Schedule_Cut.Tag = juc_ViewSchedule.CurrentAppointment;
                                cmnu_Schedule_Paste.Tag = CutCopyPaste.Cut;
                                cmnu_Schedule_Copy.Tag = null;
                            }

                            #endregion
                        }
                        break;

                    case "New Schedule":
                        {
                            frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);

                            #region " Set initial parameters"

                            ofrmSchedule.StartTime = juc_ViewSchedule.GetDateTimeAt(_JanusPastePoint);

                            if (juc_ViewSchedule.GetOwnerAt(_JanusPastePoint) != null)
                            {
                                ASBaseType OwnerType = ASBaseType.None;
                                OwnerType = (ASBaseType)Convert.ToInt32(GetTagElement(juc_ViewSchedule.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 3));
                                if (OwnerType == ASBaseType.Provider)
                                {
                                    ofrmSchedule.ScheduleType = AppointmentScheduleFlag.ProviderSchedule;
                                }
                                if (OwnerType == ASBaseType.Resource)
                                {
                                    ofrmSchedule.ScheduleType = AppointmentScheduleFlag.BlockedSchedule;
                                }

                                ofrmSchedule.PRUID = Convert.ToInt64(GetTagElement(juc_ViewSchedule.GetOwnerAt(_JanusPastePoint).Value.ToString(), '~', 1));
                            }

                            if (cmbLocation.SelectedIndex > 0)
                            {
                                ofrmSchedule.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                            }
                            if (cmbDepartment.SelectedIndex > 0)
                            {
                                ofrmSchedule.DepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);
                            }
                            #endregion

                            ofrmSchedule.ShowDialog(this);
                            ofrmSchedule.Dispose();
                            FillScheduleOnCalendar();
                        }
                        break;
                    case "Go To":
                        {
                            #region "Go To"

                            frmSearchDate oGoTo = new frmSearchDate();
                            oGoTo.SelectedDate = juc_Calendar.CurrentDate;
                            if (juc_ViewSchedule.View == ScheduleView.DayView)
                            {
                                oGoTo.SelectedView = "Day View";
                            }
                            else if (juc_ViewSchedule.View == ScheduleView.WeekView)
                            {
                                oGoTo.SelectedView = "Week View";
                            }
                            else if (juc_ViewSchedule.View == ScheduleView.MonthView)
                            {
                                oGoTo.SelectedView = "Monthly View";
                            }
                            oGoTo.ShowDialog(this);

                            if (oGoTo.SelectedResult == true)
                            {
                                juc_Calendar.CurrentDate = oGoTo.SelectedDate;
                                juc_ViewSchedule.Date = oGoTo.SelectedDate;

                                if (oGoTo.SelectedView == "Day View")
                                {
                                    juc_ViewSchedule.View = ScheduleView.DayView;
                                }
                                else if (oGoTo.SelectedView == "Week View")
                                {
                                    juc_ViewSchedule.View = ScheduleView.WeekView;
                                }
                                else if (oGoTo.SelectedView == "Monthly View")
                                {
                                    juc_ViewSchedule.View = ScheduleView.MonthView;
                                }
                            }
                            oGoTo.Dispose();

                            #endregion
                        }
                        break;
                    case "Refresh":
                        {
                            FillScheduleOnCalendar();
                        }
                        break;
                    case "Paste":
                        {
                            #region "Paste Schedule"

                            if (cmnu_Schedule_Paste.Tag != null)
                            {
                                if (((CutCopyPaste)cmnu_Schedule_Paste.Tag) == CutCopyPaste.Cut)
                                {
                                    PasteSchedule_Click(CutCopyPaste.Cut);
                                }
                                else if (((CutCopyPaste)cmnu_Schedule_Paste.Tag) == CutCopyPaste.Copy)
                                {
                                    PasteSchedule_Click(CutCopyPaste.Copy);
                                }

                                cmnu_Schedule_Paste.Tag = null;
                            }

                            #endregion
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                juc_ViewSchedule.ContextMenu = null;
                FillScheduleOnCalendar();
                juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
            }
        }

        #endregion


        //Added By Pramod Nair For UserRights 20090720
        private void AssignUserRights()
        {
            try
            {
                if (_UserName.Trim() != "")
                {
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
                    if (!oClsgloUserRights.NewSchedule)
                    {
                        tsb_NewSchedule.Enabled = false;
                        tsb_EditSchedule.Enabled = false;
                        tsb_DeleteSchedule.Enabled = false;
                        NewSchedule.Enabled = false;
                        ModifySchedule.Enabled = false;
                        cmnu_Schedule_New.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }


        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void ts_SmallStrip_btn_Calendar_Click(object sender, EventArgs e)
        {
            pnlSearchList.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void ts_SmallStrip_btn_Provider_Click(object sender, EventArgs e)
        {
            pnlSearchList.Visible = true;
            pnlSmallStrip.Visible = false;
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
        }

        private void ts_SmallStrip_btn_Calendar_MouseHover(object sender, EventArgs e)
        {
            pnlSearchList.Visible = true;
            pnlSmallStrip.Visible = false;
        }

        private void ts_SmallStrip_btn_Calendar_MouseLeave(object sender, EventArgs e)
        {
            pnlSearchList.Visible = false;
            pnlSmallStrip.Visible = true;
        }

        private void juc_Calendar_SelectionChanged(object sender, EventArgs e)
        {
           
            #region "Today's Selection"
            DateTime _FillStartDateTime = juc_ViewSchedule.Dates[0];
           
            //Condition Modified by Mayuri:20100106-To get currently selected date from calendar
            if (juc_Calendar.CurrentDate!= DateTime.Today.Date)
            //if (juc_Calendar.CurrentDate != DateTime.Today.Date)
            {

                if (tsb_Today.Checked == true)
                {
                    tsb_Today.Checked = false;
                }
                if (juc_ViewSchedule.View == ScheduleView.DayView)
                {
                    //Added by Mayuri:20100106-To fix issue:#5691-In Schedule the Zoom time icon is not displaying when we come back from month view
                    //juc_Calendar.CurrentDate = _FillStartDateTime;
                   
                    tls_btnTimeNavigation.Visible = true;
                    tsb_WeekView.Checked = false;

                    tsb_DayView.Checked = false;
                    tsb_MonthView.Checked = false;
                }
            }
            else
            {
                if (juc_ViewSchedule.View == ScheduleView.DayView)
                {
                    //Added by Mayuri:20100106-To fix issue:#5691-In Schedule the Zoom time icon is not displaying when we come back from month view
                    //juc_Calendar.CurrentDate = _FillStartDateTime;
                    tls_btnTimeNavigation.Visible = true;
                    tsb_Today.Checked = true;
                    tsb_WeekView.Checked = false;
                    tsb_DayView.Checked = false;
                    tsb_MonthView.Checked = false;

                    //20100111 Code for setting the today date
                    juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Single;
                    juc_Calendar.CurrentDate = DateTime.Today.Date;
                    juc_Calendar.CurrentDate = DateTime.Now;

                    juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Schedule;
                    
                }
            }
            juc_ViewSchedule.DayColumns = GetNumberofColumninDayView();
           
            #endregion
        }



        #region " Form Events"

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    // Dispose managed resources. 

                    if ((components != null))
                    {
                        components.Dispose();
                    }
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
                        if (cntMenuMain != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(cntMenuMain);
                            if (cntMenuMain.Items != null)
                            {
                                cntMenuMain.Items.Clear();

                            }
                            cntMenuMain.Dispose();
                            cntMenuMain = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        System.Windows.Forms.ContextMenuStrip[] cntmenuControls = { cmnu_ScheduleNew, cmnu_ScheduleEdit };
                        System.Windows.Forms.Control[] cntControl = { cmnu_ScheduleNew, cmnu_ScheduleEdit };
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
                        
                        // //if (cmnu_ScheduleNew != null)
                       //// {
                       //     gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_ScheduleNew);
                       //     if (cmnu_ScheduleNew.Items != null)
                       //     {
                       //         cmnu_ScheduleNew.Items.Clear();

                       //     }
                       //     cmnu_ScheduleNew.Dispose();
                       //     cmnu_ScheduleNew = null;
                       //// }
                    }
                    catch
                    {
                    }
                    



                    if (tlTip != null)
                    {
                        tlTip.Dispose();
                        tlTip = null;
                    }
                    try
                    {
                        if (printSchedule != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(printSchedule);
                            printSchedule.Dispose();
                            printSchedule = null;
                        }
                    }
                    catch
                    {
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmViewSchedule()
        {
            Dispose(false);
        }

        private void frmViewSchedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        private void btnLeft_MouseHover(object sender, EventArgs e)
        {
          btnLeft.Image = global::gloAppointmentScheduling.Properties.Resources.RewindHover;
          btnLeft.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btnLeft_MouseLeave(object sender, EventArgs e)
        {
            btnLeft.Image = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btnLeft.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btn_Right1_MouseLeave(object sender, EventArgs e)
        {
            btn_Right1.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_Right1.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btn_Right1_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Right1.Image = global::gloAppointmentScheduling.Properties.Resources.ForwardHover;
            btn_Right1.ImageAlign = ContentAlignment.MiddleCenter;
        }
        ToolTip tlTip = null;
        private void btnDeSelectProvider_MouseHover(object sender, EventArgs e)
        {
            if (tlTip == null)
            {
                tlTip = new ToolTip();
            }
            tlTip.SetToolTip(btnDeSelectProvider, "DeSelect All");
        }

        private void btnSelectProvider_MouseHover(object sender, EventArgs e)
        {
            if (tlTip == null)
            {
                tlTip = new ToolTip();
            }
            tlTip.SetToolTip(btnSelectProvider, "Select All");
        }

        private void btnDeSelectResource_MouseHover(object sender, EventArgs e)
        {
            if (tlTip == null)
            {
                tlTip = new ToolTip();
            }
            tlTip.SetToolTip(btnDeSelectResource, "DeSelect All");
        }

        private void btnSelectResource_MouseHover(object sender, EventArgs e)
        {
            if (tlTip == null)
            {
                tlTip = new ToolTip();
            }
            tlTip.SetToolTip(btnSelectResource, "Select All");
        }

      
    }
}