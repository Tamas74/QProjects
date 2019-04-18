using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using gloAppointmentScheduling.gloSearching;
using C1.Win.C1FlexGrid;
using gloGeneralItem;
using gloAuditTrail;
using Janus.Windows.Schedule;

namespace gloAppointmentScheduling
{
    internal partial class frmSearchAppointment : Form
    {

        //for searching criteria    
        gloAppointmentScheduling.Criteria.FindRecurrences oFindCriteria = new gloAppointmentScheduling.Criteria.FindRecurrences();
        AppointmentSearchCriteria oCriteria;

        private bool _IsFormLoading = false;
        private bool _IsPatternChanging = false;
        private bool _IsPatternFinding = false;
        //DataTable dtTemp;
        gloGeneralItem.gloItems oProviderItems;
        private string _messageBoxCaption = "gloPM";
        private Int64 _DefaultLocationID = 0;
        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");
        private ArrayList _WeekDays = new ArrayList();

        /// <summary>
        ///     List Control to Select the Diffrent Values of Types like Provider, Resources, Procedures
        /// </summary>
        /// <remarks>
        ///     Add at Runtime & Also Set the gloListControlType to Get The Respective Data
        /// </remarks>
        private gloListControl.gloListControl oListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        //gloPatient.gloSmartPatientControl oSmartPatient;

        //22-Nov-13 Aniket: Optimization of loading the patient list control
        gloPatient.PatientListControl oSmartPatient;

        // Display in List view 
        private SearchProviders oSearchProviders = null;

        private const Int32 COL_DATE = 0;
        private const Int32 COL_TIME = 1;
        private const Int32 COL_TIMETODISPLAY = 2;
        private const Int32 COL_DURATION = 3;
        private const Int32 COL_APPOINTMENTTYPE = 4;
        private const Int32 COL_APPOINTMENTTYPEID = 5;
        private const Int32 COL_PROVIDER = 6;
        private const Int32 COL_PROVIDERID = 7;
        private const Int32 COL_LOCATION = 8;
        private const Int32 COL_DEPATMENT = 9;
        private const Int32 COL_TEMPLATEID = 10;
        private const Int32 COL_TEMPLATEMSTID = 11;
        private const Int32 COL_STATUS = 12;
        private const Int32 COL_COUNT = 13;
        //---------------------

        public string sProviderName = "";
        public Int64 nProviderID = 0;
        public DateTime dtFollowUpStartDate=default(DateTime);
        public DateTime dtFollowUpEndDate = default(DateTime);
        public bool bFindFollowUp = false;

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private String _MessageBoxCaption = string.Empty;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        #endregion

        #region " Constructor Destructor "

        public frmSearchAppointment()
        {
            InitializeComponent();
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region "Retrieve UserId"

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
            #endregion ""

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

        #endregion

        #region " Form Load Event "

        private void frmSearchAppointment_Load(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            
            // Get Work week For Janus From DataBase
            GetWorkWeekForJanus();

            btn_HideSearchPnl.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btn_HideSearchPnl.BackgroundImageLayout = ImageLayout.Center;


            btn_ShowSearchPnl.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_ShowSearchPnl.ImageAlign = ContentAlignment.MiddleCenter;

            _DefaultLocationID = GetDefaultLocation();


            try
            {
                _IsFormLoading = true;
                BindRecurrenceEvents();
                GetClinicTiming();
                //To Implement WeekDay Setting 
                GetWeekDays();
                Fill_Location(); 
                Fill_Department();
                FillRecurrenceControls();
                FillProviders();
                FillAppointmentTypes();

                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now.AddMonths(1);
                cmbAMPMAll.SelectedIndex = 0;

                btnSelectAllProvider.Visible = true;
                btnClearAllProvider.Visible = false;
                btnSelectAllAppTypes.Visible = true;
                btnClearAllAppTypes.Visible = false;


                pnlListControl.Size = new Size(634, 409);
                _IsFormLoading = false;

                if (bFindFollowUp == true)
                {
                    ShowFollowupAppointments();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            btn_HideSearchPnl_MouseLeave(null, null);
            btn_ShowSearchPnl_MouseLeave(null, null);
            btn_AppSearch_Up_MouseLeave(null, null);
            btn_AdvanceSearch_Down_MouseLeave(null, null);
            btn_Recurrencepattern_Down_MouseLeave(null, null);
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

        #endregion

        #region "Tool Strip Button Events"

        private void SearchBlankSlot()
        {
            if (Validate())
            {
                pnlProgressbar.Visible = true;
                lblSearchingMessage.Text = "Searching...";
                lblSearchingMessage.Refresh();
                progbarSearch.Value = 0;
                SearchAppointment();
                juc_Appointment.Date = dtpStartDate.Value;

                if (juc_Appointment.Appointments.Count == 0)
                {
                    MessageBox.Show("No appointments found.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                pnlProgressbar.Visible = false;
                lblSearchingMessage.Text = "";
                lblSearchingMessage.Refresh();
                progbarSearch.Value = 0;
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "SearchAppointment":
                        {
                            SearchBlankSlot();
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "DayView":
                        {
                            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.DayView;
                        }
                        break;
                    case "WeekView":
                        {
                            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.WeekView;

                        }
                        break;
                    case "MonthView":
                        {
                            juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.MonthView;
                        }
                        break;
                    case "ListView":
                        {
                            // --------------------------------------
                            // Code modified by Pankaj Bedse 08012010
                            // Changed the if condition as Text contains the & for hot keys.
                            if (c1Appointments.Rows.Count == 1)
                            {
                                SearchBlankSlot();
                            }
                            if (tsb_ListView.Text.Contains("List"))
                            {
                                tsb_ListView.Text = "Calendar &View";   //bug no:629
                                tsb_ListView.ToolTipText = "Calendar View";
                                tsb_ListView.Image = null;
                                tsb_ListView.Image = global::gloAppointmentScheduling.Properties.Resources.Calender_View;
                                pnlCalendarView.Visible = false;
                                pnlListView.Visible = true;
                                tsb_DayView.Enabled = false;
                                tsb_WeekView.Enabled = false;
                                tsb_MonthView.Enabled = false;
                                
                            }
                            else if (tsb_ListView.Text.Contains("Calendar"))
                            {
                                tsb_ListView.Text = "&List View";   //bug no:629
                                tsb_ListView.ToolTipText = "List View";
                                tsb_ListView.Image = null;
                                tsb_ListView.Image = global::gloAppointmentScheduling.Properties.Resources.List_View;
                                pnlCalendarView.Visible = true;
                                pnlListView.Visible = false;
                                tsb_DayView.Enabled = true;
                                tsb_WeekView.Enabled = true;
                                tsb_MonthView.Enabled = true;
                            }
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
        }

        #endregion

        #region "Search & Fill Appointment Methods"

        private void SearchAppointment()
        {
            try
            {
                //Optimization Code (search works faster when control is hide avoiding redraw when each App is added)
                juc_Appointment.Hide();
                Application.DoEvents();

                oCriteria = new AppointmentSearchCriteria();
                gloSearching.gloSearching oSearch = new gloSearching.gloSearching(_databaseconnectionstring);

                oProviderItems = new gloItems();
                gloItems oProblemTypeItems = new gloItems();
                gloItems oResourceItems = new gloItems();

                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        if (trvProvider.Nodes[i].Checked == true)
                        {
                            gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                            oItem.ID = Convert.ToInt64(trvProvider.Nodes[i].Tag);
                            oItem.Description = Convert.ToString(trvProvider.Nodes[i].Text); // Provider Name
                            oProviderItems.Add(oItem);
                            oItem.Dispose();
                            oItem = null;
                        }
                    }
                }
                else
                {
                    // If Providers are not Selected in ComboBox then Select all Providers
                    gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                    DataTable dt = oResource.GetProviders();
                    oResource.Dispose();
                    oResource = null;

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

                            oItem.ID = Convert.ToInt64(dt.Rows[i]["nProviderID"]);
                            oItem.Description = dt.Rows[i]["ProviderName"].ToString(); // Provider Name
                            oProviderItems.Add(oItem);
                            oItem.Dispose();
                            oItem = null;
                        }
                        dt.Dispose();
                        dt = null;
                    }
                }

                //Set Problem Type to Search
                if (cmbProcedures.Items.Count > 0)
                {
                    for (int i = 0; i < cmbProcedures.Items.Count; i++)
                    {
                        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

                        oItem.ID = Convert.ToInt64(((System.Data.DataRowView)(cmbProcedures.Items[i])).Row.ItemArray[0]);
                        oItem.Description = ((System.Data.DataRowView)(cmbProcedures.Items[i])).Row.ItemArray[2].ToString(); // Provider Name
                        oProblemTypeItems.Add(oItem);
                        oItem.Dispose();
                        oItem = null;
                    }
                }

                //Set Resources to Search
                if (cmbResources.Items.Count > 0)
                {
                    for (int i = 0; i < cmbResources.Items.Count; i++)
                    {
                        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

                        oItem.ID = Convert.ToInt64(((System.Data.DataRowView)(cmbResources.Items[i])).Row.ItemArray[0]);
                        oItem.Description = ((System.Data.DataRowView)(cmbResources.Items[i])).Row.ItemArray[2].ToString(); // Provider Name
                        oResourceItems.Add(oItem);
                        oItem.Dispose();
                        oItem = null;
                    }
                }

                oCriteria.Department = cmbDepartment_Appointment.Text.Trim();
                oCriteria.Location = cmbLocation_Appointment.Text.Trim();
                

                //Set Appointment Criteria
                oCriteria.Providers = oProviderItems;
                oCriteria.ProblemTypes = oProblemTypeItems;
                oCriteria.Resources = oResourceItems;
                if (chkApplyRecPattern.Checked == true)
                {
                    FindRecurrence();
                    ArrayList _FindDates = new ArrayList();
                    _FindDates = oFindCriteria.Dates;
                    oCriteria.Dates = _FindDates;
                }
                else
                {
                    oCriteria.Dates = new ArrayList();

                    for (DateTime i = dtpStartDate.Value.Date; i <= dtpEndDate.Value.Date; i = i.Date.AddDays(1))
                    {
                        // if( i >= juc_Appointment.Dates[0].Date && i <= juc_Appointment.Dates[juc_Appointment.Dates.Count -1].Date)
                        //if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                        //    oCriteria.Dates.Add(i);

                        // Code added to implement WeekDay Setting 
                        foreach (DayOfWeek weekday in _WeekDays)
                        {
                            if (i.DayOfWeek == weekday)
                            {
                                oCriteria.Dates.Add(i);
                                break;
                            }
                        }

                    }
                }

                oCriteria.Duration = Convert.ToInt64(num_Duration.Value);
                oCriteria.ClinicId = 0;



                if (chkApplyAdvSearch.Checked == true)
                {
                    oCriteria.IsAdvanceSearch = true;
                }
                else
                {
                    oCriteria.IsAdvanceSearch = false;
                }

                if (rbProblemTypeFilter.Checked == true)
                {
                    oCriteria.IsProblemTypeSearch = true;
                }
                else
                {
                    oCriteria.IsProblemTypeSearch = false;
                    oCriteria.Department = "";
                    oCriteria.Location = "";
                }

                //-----------   Search In AM/PM/All
                Int32 _tempClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
                Int32 _tempClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
                if (cmbAMPMAll.SelectedIndex != -1)
                {
                    if (cmbAMPMAll.SelectedItem.ToString().Trim() == "AM")
                    {
                        _tempClinicEndTime = 1200;
                    }
                    else if (cmbAMPMAll.SelectedItem.ToString().Trim() == "PM")
                    {
                        _tempClinicStartTime = 1200;
                    }
                }

                // Change Clinic Timing according to selection 
                _dtClinicStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _tempClinicStartTime);
                _dtClinicEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _tempClinicEndTime);
                juc_Appointment.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
                juc_Appointment.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);
                //-------------
              

                if (chkApplyAdvSearch.Checked == false && chkSearchInTemplate.Checked == true)
                {
                    FillTemplateAppointments();
                }
                else
                {
                    DataTable dtTemp = null;
                    if (oCriteria.Dates.Count != 0)
                    {
                        dtTemp = oSearch.GetAppointments(oCriteria);

                        if (dtTemp != null)
                        {
                            FillAppointments(dtTemp);
                            dtTemp.Dispose();
                            dtTemp = null;
                        }
                    }  
                }


                if (oCriteria.Dates.Count != 0)
                {
                    juc_Appointment.Date = Convert.ToDateTime(oCriteria.Dates[0]);
                    if (oCriteria.Dates.Count > 1)
                    {
                        juc_Appointment.View = ScheduleView.MonthView;
                    }
                    else
                    {
                        juc_Appointment.View = ScheduleView.DayView;
                    }
                }

                // Display Appointments in List View 
                FillListViewProviders();
                FillAppointmentListView();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.SearchAppointment, ActivityType.None, "Appointment searched", 0, 0, 0, ActivityOutCome.Success);
            } // try

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                juc_Appointment.Show();
                Application.DoEvents();
                //reset clinic timings
                GetClinicTiming();
            }
        }



        private void FillAppointments(DataTable dtAppointments)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                juc_Appointment.Appointments.Clear();
                juc_Appointment.Owners.Clear();

                #region Fill Owners

                //for (int i = 0; i < cmbProviders1.Items.Count; i++)
                //{
                //    cmbProviders1.SelectedIndex = i;
                //    cmbProviders1.Refresh();
                //    juc_Appointment.Owners.Add(Convert.ToInt64(cmbProviders1.SelectedValue), cmbProviders1.Text);
                //}

                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        juc_Appointment.Owners.Add(Convert.ToInt64(trvProvider.Nodes[i].Tag), trvProvider.Nodes[i].Text);
                    }
                }

                #endregion

                Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
                Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_Owner = null;

                if (dtAppointments != null)
                {
                    for (int i = 0; i <= dtAppointments.Rows.Count - 1; i++)
                    {
                        //Do not fill Cancel/Noshow/Delete Appointments 
                        ASUsedStatus oASUsedStatus = ASUsedStatus.Unknown5;
                        oASUsedStatus = (ASUsedStatus)Convert.ToInt32(dtAppointments.Rows[i]["nUsedStatus"]);
                        if (oASUsedStatus == ASUsedStatus.Cancel || oASUsedStatus == ASUsedStatus.NoShow || oASUsedStatus == ASUsedStatus.Delete)
                        {
                            continue;
                        }

                        #region Add Appointments

                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Owner = new Janus.Windows.Schedule.ScheduleAppointmentOwner();

                        // oJUC_Appointment.Text = Convert.ToDateTime(dtAppointments.Rows[i]["dtStartTime"]).ToShortTimeString() + " " + Convert.ToDateTime(dtAppointments.Rows[i]["dtEndTime"]).ToShortTimeString(); ;
                        oJUC_Appointment.Text = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"])).ToShortTimeString() + " " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"])).ToShortTimeString();
                        oJUC_Appointment.Tag = "Block";
                        oJUC_Appointment.Description = "";

                        //Added by Amit to differentiate Blocked and Scheduled Appointment
                        if (oASUsedStatus == ASUsedStatus.Block)
                        {
                            oJUC_Appointment.FormatStyle.BackColor = Color.DarkRed;
                        }
                        else
                        {
                            oJUC_Appointment.FormatStyle.BackColor = Color.Gray;
                        }

                        oJUC_Appointment.FormatStyle.ForeColor = Color.White;
                        // end 

                        oJUC_Appointment.FormatStyle.FontStrikeout = Janus.Windows.Schedule.TriState.True;

                        #region "Appointment Date & Time"
                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtEndDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"]));
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"]));
                        }
                        catch { _ErrorFound = true; }
                        
                                         
                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"]));
                                oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtEndDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"]));
                            }
                            catch { }
                        }
                        #endregion
                       
                        ASBaseType BaseType = (ASBaseType)Convert.ToInt32(dtAppointments.Rows[i]["nASBaseFlag"]);

                        //If Provider Block then add To Corresponding Owner
                        if (BaseType == ASBaseType.Provider)
                        {
                            for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
                            {
                                Int64 OwnerID = Convert.ToInt64(juc_Appointment.Owners[j].Value.ToString());
                                if (OwnerID == Convert.ToInt64(dtAppointments.Rows[i]["ProviderID"]))
                                {
                                    oJUC_Owner = juc_Appointment.Owners[j];
                                    oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                                    break;
                                }
                            }
                            juc_Appointment.Appointments.Add(oJUC_Appointment);
                        }
                        else if (BaseType == ASBaseType.Resource) //IF Resource Block then Add to All Owners
                        {
                            Janus.Windows.Schedule.ScheduleAppointment oJUC_TempAppointment;
                            for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
                            {
                                oJUC_TempAppointment = (ScheduleAppointment)oJUC_Appointment.Clone();
                                oJUC_Owner = juc_Appointment.Owners[j];
                                oJUC_TempAppointment.Owner = juc_Appointment.Owners[j].Value;
                                juc_Appointment.Appointments.Add(oJUC_TempAppointment);
                                oJUC_TempAppointment = null;
                            }
                        }

                        oJUC_Appointment = null;

                        #endregion
                    }
                    juc_Appointment.Update();
                }

                if (chkApplyAdvSearch.Checked == true && chkSearchInTemplate.Checked == false)
                {
                    if (cmbProcedures.Items.Count > 0 && rbProblemTypeFilter.Checked == true)
                        FillFreeSlotsWithProblemType();
                    else if (cmbResources.Items.Count > 0 && rbResourceFilter.Checked == true)
                        FillFreeSlotsWithProblemType();
                    else
                        FillFreeSlots();

                }
                else if (chkApplyAdvSearch.Checked == false && chkSearchInTemplate.Checked == false)
                {
                    FillFreeSlots();
                }
                foreach (ScheduleAppointment scheduleappt in juc_Appointment.Appointments)
                {
                    if (Convert.ToString(GetTagElement(scheduleappt.Tag.ToString(), '~', 1)) != "FreeSlot")
                    {
                        scheduleappt.Visible = false;
                    }
                }
               
            }
                

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                juc_Appointment.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
                juc_Appointment.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);

            }


        }

        private void FillTemplateAppointments()
        {
            gloSearching.gloSearching oSearch = new gloSearching.gloSearching(_databaseconnectionstring);
            ArrayList _FindDates = new ArrayList();
            String _sLocation = "";
            String _sDepartment = "";
            String strProviderIDs = "";
            String strAppointmentTypes = "";
            String strAppointmentTypesAll = "";

            this.Cursor = Cursors.WaitCursor;
            try
            {

                //Collection to store providers free slotes ( Used to display search in list view)
                oSearchProviders = new SearchProviders();

                _sLocation = cmbLocation_Appointment.Text.Trim();
                _sDepartment = cmbDepartment_Appointment.Text.Trim();


                if (chkApplyRecPattern.Checked == true)
                {
                    FindRecurrence();
                    _FindDates = oFindCriteria.Dates;
                }
                else
                {
                    for (DateTime i = dtpStartDate.Value.Date; i <= dtpEndDate.Value.Date; i = i.Date.AddDays(1))
                    {
                        _FindDates.Add(i.ToShortDateString());
                    }
                }


                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        if (trvProvider.Nodes[i].Checked == true)
                        {
                            if (strProviderIDs.Trim() == "")
                                strProviderIDs = Convert.ToString(trvProvider.Nodes[i].Tag);
                            else
                                strProviderIDs = strProviderIDs + "," + Convert.ToString(trvProvider.Nodes[i].Tag);
                        }
                    }
                }

                if (trvAppointmentType.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
                    {
                        if (trvAppointmentType.Nodes[i].Checked == true)
                        {
                            if (strAppointmentTypes.Trim() == "")
                                strAppointmentTypes = "" + Convert.ToString(trvAppointmentType.Nodes[i].Text.Trim()).Replace("'", "''") + "";
                            else
                                strAppointmentTypes = strAppointmentTypes + "," + Convert.ToString(trvAppointmentType.Nodes[i].Text.Trim()).Replace("'", "''") + "";
                        }
                        else
                        {
                            if (strAppointmentTypesAll.Trim() == "")
                                strAppointmentTypesAll = "" + Convert.ToString(trvAppointmentType.Nodes[i].Text.Trim()).Replace("'", "''") + "";
                            else
                                strAppointmentTypesAll = strAppointmentTypesAll + "," + Convert.ToString(trvAppointmentType.Nodes[i].Text.Trim()).Replace("'", "''") + "";
                        }
                    }
                }

                if (strAppointmentTypes == "")
                {
                    strAppointmentTypes = strAppointmentTypesAll;
                }

                DataTable dtTemplates = oSearch.GetTemplateAppointments(strProviderIDs, _FindDates, strAppointmentTypes, _sLocation, _sDepartment);

                juc_Appointment.Appointments.Clear();
                juc_Appointment.Owners.Clear();

                if (trvProvider.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvProvider.Nodes.Count; i++)
                    {
                        if (trvProvider.Nodes[i].Checked == true)
                        {
                            juc_Appointment.Owners.Add(Convert.ToInt64(trvProvider.Nodes[i].Tag), trvProvider.Nodes[i].Text);
                            SearchProvider oProvider = new SearchProvider();
                            oProvider.ProviderID = Convert.ToInt64(trvProvider.Nodes[i].Tag);
                            oProvider.ProviderName = trvProvider.Nodes[i].Text;
                            oSearchProviders.Add(oProvider);
                            oProvider = null;
                        }
                    }
                }

                Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
                Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_Owner = null;

                if (dtTemplates != null)
                {
                    progbarSearch.Minimum = 0;
                    progbarSearch.Maximum = dtTemplates.Rows.Count;

                    for (int i = 0; i <= dtTemplates.Rows.Count - 1; i++)
                    {
                        if (cmbAMPMAll.Text.Trim() == "AM")
                        {
                            if (Convert.ToInt32(dtTemplates.Rows[i]["dtStartTime"]) >= 1200)
                            {
                                continue;
                            }
                        }
                        if (cmbAMPMAll.Text.Trim() == "PM")
                        {
                            if (Convert.ToInt32(dtTemplates.Rows[i]["dtEndTime"]) <= 1200)
                            {
                                continue;
                            }
                        }
                      
                        #region Add Appointement
                        
                        ASUsedStatus oASUsedStatus = ASUsedStatus.Unknown5;
                        oASUsedStatus = (ASUsedStatus)Convert.ToInt32(dtTemplates.Rows[i]["nUsedStatus"]);

                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Owner = new Janus.Windows.Schedule.ScheduleAppointmentOwner();
                        oJUC_Appointment.Text = Convert.ToString(dtTemplates.Rows[i]["sAppointmentTypeDesc"]) + " " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtTemplates.Rows[i]["dtStartTime"])).ToShortTimeString() + " " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtTemplates.Rows[i]["dtEndTime"])).ToShortTimeString();


                        if (oASUsedStatus == ASUsedStatus.Block)
                        {
                            oJUC_Appointment.Tag = "Block" + "~" + Convert.ToString(dtTemplates.Rows[i]["sLocationName"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["sDepartmentName"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["sAppointmentTypeDesc"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["nTemplateAllocationMasterID"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["nTemplateAllocationID"]);
                            oJUC_Appointment.FormatStyle.BackColor = Color.DarkRed;
                            oJUC_Appointment.FormatStyle.ForeColor = Color.White;
                            oJUC_Appointment.FormatStyle.FontStrikeout = Janus.Windows.Schedule.TriState.True;
                        }
                        else
                        {
                            oJUC_Appointment.Tag = "Template" + "~" + Convert.ToString(dtTemplates.Rows[i]["sLocationName"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["sDepartmentName"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["sAppointmentTypeDesc"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["nTemplateAllocationMasterID"]) + "~" + Convert.ToString(dtTemplates.Rows[i]["nTemplateAllocationID"]);
                            oJUC_Appointment.Description = "Click here to make Appointment..";
                            oJUC_Appointment.BorderColor = Color.DarkRed;

                            oJUC_Appointment.FormatStyle.BackColor = Color.GhostWhite;
                            oJUC_Appointment.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Horizontal;
                            oJUC_Appointment.FormatStyle.BackColorGradient = Color.FromArgb(Convert.ToInt32(dtTemplates.Rows[i]["nColorCode"]));
                            oJUC_Appointment.ImageIndex1 = 0;
                        }

                        #region "Appointment Date & Time"
                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtTemplates.Rows[i]["dtEndDate"])), Convert.ToInt32(dtTemplates.Rows[i]["dtEndTime"]));
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtTemplates.Rows[i]["dtStartDate"])), Convert.ToInt32(dtTemplates.Rows[i]["dtStartTime"]));
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtTemplates.Rows[i]["dtStartDate"])), Convert.ToInt32(dtTemplates.Rows[i]["dtStartTime"]));
                                oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtTemplates.Rows[i]["dtEndDate"])), Convert.ToInt32(dtTemplates.Rows[i]["dtEndTime"]));
                            }
                            catch { }
                        }
                        #endregion

                        ASBaseType BaseType = (ASBaseType)Convert.ToInt32(dtTemplates.Rows[i]["nASBaseFlag"]);

                        //If Provider Block then add To Corresponding Owner
                        if (BaseType == ASBaseType.Provider)
                        {
                            for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
                            {
                                Int64 OwnerID = Convert.ToInt64(juc_Appointment.Owners[j].Value.ToString());
                                if (OwnerID == Convert.ToInt64(dtTemplates.Rows[i]["nASBaseID"]))
                                {
                                    oJUC_Owner = juc_Appointment.Owners[j];
                                    oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                                    break;
                                }
                            }
                            juc_Appointment.Appointments.Add(oJUC_Appointment);
                        }

                        //------------------- (display search in list view)


                        if (oASUsedStatus != ASUsedStatus.Block)
                        {

                            FreeSlot oFreeSlot = new FreeSlot();
                            oFreeSlot.Date = oJUC_Appointment.StartTime;
                            oFreeSlot.StartTime = oJUC_Appointment.StartTime;
                            oFreeSlot.EndTime = oJUC_Appointment.EndTime;
                            oFreeSlot.AppointmentType = Convert.ToString(dtTemplates.Rows[i]["sAppointmentTypeDesc"]);
                            oFreeSlot.AppointmentTypeID = 0;
                            oFreeSlot.TemplateAllocationID = Convert.ToInt64(dtTemplates.Rows[i]["nTemplateAllocationID"]);
                            oFreeSlot.TemplateAllocationMasterID = Convert.ToInt64(dtTemplates.Rows[i]["nTemplateAllocationMasterID"]);
                            oFreeSlot.Location = Convert.ToString(dtTemplates.Rows[i]["sLocationName"]);
                            oFreeSlot.Department = Convert.ToString(dtTemplates.Rows[i]["sDepartmentName"]);

                            for (int k = 0; k < oSearchProviders.Count; k++)
                            {
                                if (oSearchProviders[k].ProviderID == Convert.ToInt64(oJUC_Appointment.Owner))
                                {
                                    oSearchProviders[k].ProviderFreeSlots.Add(oFreeSlot);
                                    break;
                                }
                            }
                            oFreeSlot = null;
                        }
                        //-------------------------------------------------

                        oJUC_Appointment = null;

                        #endregion

                        progbarSearch.Increment(1);
                        progbarSearch.Refresh();
                    }
                    juc_Appointment.Update();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oSearch != null) { oSearch.Dispose(); }
            }
        }

        private void FillFreeSlots()
        {
            //Show Progress bar
            progbarSearch.Minimum = 0;
            TimeSpan tsTotalClinicDuration = _dtClinicEndTime.Subtract(_dtClinicStartTime);
            progbarSearch.Maximum = (Int32)(oCriteria.Dates.Count * juc_Appointment.Owners.Count * (tsTotalClinicDuration.TotalMinutes / Convert.ToInt32(num_Duration.Value)));
            //Show Progress bar

            juc_Appointment.Interval = Janus.Windows.Schedule.Interval.OneMinute;

            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_Owner = null;
            DateTime dtStartTime;
            DateTime dtEndTime;
            DateTime dtFreeSlot;
            DateTime _tempClinicEndTime;
            Janus.Windows.Schedule.ScheduleAppointment[] oAppointments;

            //Collection to store providers free slotes ( Used to display search in list view)
            oSearchProviders = new SearchProviders();

            for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
            {
                //------------------- (display search in list view)
                SearchProvider oProvider = new SearchProvider();
                oProvider.ProviderID = Convert.ToInt64(juc_Appointment.Owners[j].Value);
                oProvider.ProviderName = juc_Appointment.Owners[j].Text;
                //------------------------------------------------

                for (int k = 0; k < oCriteria.Dates.Count; k++)
                {

                    //------------------- (display search in list view)
                    FreeSlot oFreeSlot = null;
                    //------------------------------------------------

                    //_tempClinicEndTime = Convert.ToDateTime(Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + juc_Appointment.WorkEndTime.Hours + ":" + juc_Appointment.WorkEndTime.Minutes + ":" + juc_Appointment.WorkEndTime.Seconds);
                    _tempClinicEndTime = Convert.ToDateTime(Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + _dtClinicEndTime.ToShortTimeString());

                    do
                    {
                        #region Create Free Slot

                        //Set FreeSlot Owner
                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Owner = juc_Appointment.Owners[j];
                        oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;

                        //Find FreeSlot Start & End time                       
                        dtFreeSlot = juc_Appointment.FindFreeTimeSlot(Convert.ToDateTime(oCriteria.Dates[k]), new TimeSpan(0, 0, 1), true, oJUC_Owner);
                        if (Convert.ToDateTime(oCriteria.Dates[k]).Date != dtFreeSlot.Date)
                        {
                            break;
                        }
                        dtStartTime = dtFreeSlot;
                        dtEndTime = dtStartTime.AddMinutes(Convert.ToInt64(num_Duration.Value));

                        //Find if any App starts in  FreeSlot timing (if yes then set FreeSlot end time = app start time)

                        //oAppointments = juc_Appointment.GetAppointmentsFromDate(Convert.ToDateTime(oCriteria.Dates[k]));

                        string sRangeStart = Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + dtStartTime.ToShortTimeString();
                        string sRangeEnd = Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + dtEndTime.ToShortTimeString();
                        DateRange dtRange = new DateRange(Convert.ToDateTime(sRangeStart), Convert.ToDateTime(sRangeEnd));
                        oAppointments = juc_Appointment.GetAppointmentsFromRange(dtRange, oJUC_Owner);

                        for (int i = 0; i < oAppointments.Length; i++)
                        {
                            if (oAppointments[i].Owner == oJUC_Appointment.Owner)
                            {
                                if (dtStartTime <= oAppointments[i].StartTime && dtEndTime > oAppointments[i].StartTime)
                                {
                                    dtEndTime = oAppointments[i].StartTime;
                                    break;
                                }
                            }
                        }

                        if (dtEndTime > _tempClinicEndTime)
                        {
                            dtEndTime = _tempClinicEndTime;
                        }

                        #endregion

                        #region Add Free Slot

                        //oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        //oJUC_Owner = new Janus.Windows.Schedule.ScheduleAppointmentOwner();

                        //for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
                        //{
                        //    Int64 OwnerID = Convert.ToInt64(juc_Appointment.Owners[j].Value.ToString());
                        //    oJUC_Owner = juc_Appointment.Owners[j];
                        //    oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                        //    break;
                        //}

                        oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                        oJUC_Appointment.Text = Convert.ToDateTime(dtStartTime).ToShortTimeString() + " " + Convert.ToDateTime(dtEndTime).ToShortTimeString();
                        oJUC_Appointment.Tag = "FreeSlot" + "~" + cmbLocation_Appointment.Text.Trim() + "~" + cmbDepartment_Appointment.Text.Trim();
                        oJUC_Appointment.Description = "Click here to make Appointment..";
                        oJUC_Appointment.FormatStyle.BackColor = Color.White;
                        oJUC_Appointment.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Horizontal;
                        //oJUC_Appointment.FormatStyle.BackColorGradient = Color.RosyBrown;
                        oJUC_Appointment.FormatStyle.FontUnderline = TriState.True;
                        oJUC_Appointment.ImageIndex1 = 0;

                        #region "Appointment Date & Time"
                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.EndTime = dtEndTime;
                            oJUC_Appointment.StartTime = dtStartTime;
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.StartTime = dtStartTime;
                                oJUC_Appointment.EndTime = dtEndTime;
                            }
                            catch { }
                        }
                        #endregion



                        juc_Appointment.Appointments.Add(oJUC_Appointment);
                        juc_Appointment.Update();

                        #endregion

                        //------------------- (display search in list view)
                        oFreeSlot = new FreeSlot();
                        oFreeSlot.Date = oJUC_Appointment.StartTime;
                        oFreeSlot.StartTime = oJUC_Appointment.StartTime;
                        oFreeSlot.EndTime = oJUC_Appointment.EndTime;
                        oFreeSlot.AppointmentType = "";
                        oFreeSlot.AppointmentTypeID = 0;
                        oProvider.ProviderFreeSlots.Add(oFreeSlot);
                       
                        //-------------------------------------------------
                       
                        //Progress bar
                        progbarSearch.Increment(1);
                        progbarSearch.Refresh();
                      
                    } while (Convert.ToDateTime(oCriteria.Dates[k]).Date == dtFreeSlot.Date);
                    
                }
             
                //------------------- (display search in list view)

                oSearchProviders.Add(oProvider);
                //------------------------------------------------
            }

            juc_Appointment.Interval = Janus.Windows.Schedule.Interval.ThirtyMinutes;
            juc_Appointment.Refresh();


        }

        private void FillFreeSlotsWithProblemType()
        {
            progbarSearch.Minimum = 0;
            progbarSearch.Maximum = (oCriteria.Dates.Count * juc_Appointment.Owners.Count);

            juc_Appointment.Interval = Janus.Windows.Schedule.Interval.OneMinute;

            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            Janus.Windows.Schedule.ScheduleAppointmentOwner oJUC_Owner = null;
            DateTime dtStartTime;
            DateTime dtEndTime;
            DateTime dtFreeSlot;
            DateTime _tempClinicEndTime;
            Janus.Windows.Schedule.ScheduleAppointment[] oAppointments;
            gloSearching.gloSearching oSearch = new gloSearching.gloSearching(_databaseconnectionstring);
            DataTable dtSchedules = new DataTable();


            for (int j = 0; j <= juc_Appointment.Owners.Count - 1; j++)
            {
                lblSearchingMessage.Text = "Searching for " + juc_Appointment.Owners[j].Text + "...";
                lblSearchingMessage.Refresh();

                dtSchedules = oSearch.GetSchedules(oCriteria, Convert.ToInt64(juc_Appointment.Owners[j].Value));

                if (dtSchedules != null)
                {
                    //Show Progress bar
                    progbarSearch.Value = 0;
                    progbarSearch.Minimum = 0;

                    for (int m = 0; m < dtSchedules.Rows.Count; m++)
                    {
                        DateTime dtScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSchedules.Rows[m]["dtStartDate"])), Convert.ToInt32(dtSchedules.Rows[m]["dtStartTime"]));
                        DateTime dtScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSchedules.Rows[m]["dtEndDate"])), Convert.ToInt32(dtSchedules.Rows[m]["dtEndTime"]));

                        TimeSpan tsTotalClinicDuration = dtScheduleEndTime.Subtract(dtScheduleStartTime);
                        progbarSearch.Maximum = (Int32)(dtSchedules.Rows.Count * (tsTotalClinicDuration.TotalMinutes / Convert.ToInt32(num_Duration.Value)));
                        //Show Progress bar


                        juc_Appointment.WorkStartTime = new TimeSpan(dtScheduleStartTime.Hour, dtScheduleStartTime.Minute, dtScheduleStartTime.Second);
                        juc_Appointment.WorkEndTime = new TimeSpan(dtScheduleEndTime.Hour, dtScheduleEndTime.Minute, dtScheduleEndTime.Second);
                        juc_Appointment.Update();
                        for (int k = 0; k < oCriteria.Dates.Count; k++)
                        {

                            if (Convert.ToDateTime(oCriteria.Dates[k]).Date != gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSchedules.Rows[m]["dtStartDate"])).Date)
                                continue;

                            _tempClinicEndTime = Convert.ToDateTime(Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + juc_Appointment.WorkEndTime.Hours + ":" + juc_Appointment.WorkEndTime.Minutes + ":" + juc_Appointment.WorkEndTime.Seconds);

                            do
                            {
                                #region Create Free Slot


                                //Set FreeSlot Owner
                                oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                                oJUC_Owner = juc_Appointment.Owners[j];
                                oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;

                                //Find FreeSlot Start & End time 
                                dtFreeSlot = juc_Appointment.FindFreeTimeSlot(Convert.ToDateTime(oCriteria.Dates[k]), new TimeSpan(0, 0, 1), true, oJUC_Owner);
                                if (Convert.ToDateTime(oCriteria.Dates[k]).Date != dtFreeSlot.Date)
                                {
                                    break;
                                }

                                dtStartTime = dtFreeSlot;
                                dtEndTime = dtStartTime.AddMinutes(Convert.ToInt64(num_Duration.Value));


                                //Find if any App starts in  FreeSlot timing (if yes then set FreeSlot end time = app start time)

                                //oAppointments = juc_Appointment.GetAppointmentsFromDate(Convert.ToDateTime(oCriteria.Dates[k]));
                                string sRangeStart = Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + dtStartTime.ToShortTimeString();
                                string sRangeEnd = Convert.ToDateTime(oCriteria.Dates[k]).ToShortDateString() + " " + dtEndTime.ToShortTimeString();
                                DateRange dtRange = new DateRange(Convert.ToDateTime(sRangeStart), Convert.ToDateTime(sRangeEnd));
                                oAppointments = juc_Appointment.GetAppointmentsFromRange(dtRange, oJUC_Owner);

                                for (int i = 0; i < oAppointments.Length; i++)
                                {
                                    if (oAppointments[i].Owner == oJUC_Appointment.Owner)
                                    {
                                        if (dtStartTime <= oAppointments[i].StartTime && dtEndTime > oAppointments[i].StartTime)
                                        {
                                            dtEndTime = oAppointments[i].StartTime;
                                            break;
                                        }
                                    }
                                }

                                if (dtEndTime.TimeOfDay > _tempClinicEndTime.TimeOfDay)
                                {
                                    dtEndTime = _tempClinicEndTime;
                                }
                                //if (dtEndTime > _ClinicEndTime)
                                //{
                                //    dtEndTime = _ClinicEndTime;
                                //}

                                #endregion

                                #region Add Free Slot

                                oJUC_Appointment.Owner = juc_Appointment.Owners[j].Value;
                                oJUC_Appointment.Text = Convert.ToDateTime(dtStartTime).ToShortTimeString() + " " + Convert.ToDateTime(dtEndTime).ToShortTimeString();
                                oJUC_Appointment.Tag = "FreeSlot" + "~" + cmbLocation.Text.Trim() + "~" + cmbDepartment.Text.Trim();
                                oJUC_Appointment.Description = Convert.ToString(dtSchedules.Rows[m]["ProlemType"]) + " - Click here to make Appointment..";
                                oJUC_Appointment.FormatStyle.BackColor = Color.GhostWhite;
                                oJUC_Appointment.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Horizontal;
                                oJUC_Appointment.FormatStyle.BackColorGradient = Color.RosyBrown;
                                oJUC_Appointment.FormatStyle.FontUnderline = TriState.True;
                                oJUC_Appointment.ImageIndex1 = 0;

                                #region "Appointment Date & Time"
                                bool _ErrorFound = false;
                                try
                                {
                                    oJUC_Appointment.EndTime = dtEndTime;
                                    oJUC_Appointment.StartTime = dtStartTime;
                                }
                                catch { _ErrorFound = true; }

                                if (_ErrorFound == true)
                                {
                                    try
                                    {
                                        oJUC_Appointment.StartTime = dtStartTime;
                                        oJUC_Appointment.EndTime = dtEndTime;
                                    }
                                    catch { }
                                }
                                #endregion

                                juc_Appointment.Appointments.Add(oJUC_Appointment);
                                juc_Appointment.Update();

                                #endregion

                                progbarSearch.Increment(1);
                                progbarSearch.Refresh();

                            } while (Convert.ToDateTime(oCriteria.Dates[k]).Date == dtFreeSlot.Date);

                        } // Criteria
                    }
                }
            }

            juc_Appointment.Interval = Janus.Windows.Schedule.Interval.ThirtyMinutes;
            juc_Appointment.Refresh();
        }

        private void FillListViewProviders()
        {
            try
            {
                if (oSearchProviders != null)
                {
                    DataTable dtProvides = new DataTable();
                    dtProvides.Columns.Add("Desc");
                    dtProvides.Columns.Add("ID");
                    for (int i = 0; i < oSearchProviders.Count; i++)
                    {
                        DataRow dr = dtProvides.NewRow();
                        dr["Desc"] = oSearchProviders[i].ProviderName;
                        dr["ID"] = oSearchProviders[i].ProviderID;
                        dtProvides.Rows.Add(dr);
                        dtProvides.AcceptChanges();
                    }
                    cmbListViewProvider.DataSource = dtProvides;
                    cmbListViewProvider.DisplayMember = "Desc";
                    cmbListViewProvider.ValueMember = "ID";

                    if (cmbListViewProvider.Items.Count > 0)
                        cmbListViewProvider.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        private void FillAppointmentListView()
        {
            try
            {
                
                if (oSearchProviders != null)
                {
                    c1Appointments.Rows.Count = 1;
                    c1Appointments.Rows.Fixed = 1;
                    c1Appointments.Cols.Count = COL_COUNT;
                    c1Appointments.Cols.Fixed = 0;


                    c1Appointments.AllowEditing = false;

                    c1Appointments.SetData(0, COL_DATE, "Date");
                    c1Appointments.SetData(0, COL_TIME, " Time");
                    c1Appointments.SetData(0, COL_TIMETODISPLAY, " Time");
                    c1Appointments.SetData(0, COL_DURATION, "Duration");
                    c1Appointments.SetData(0, COL_APPOINTMENTTYPE, "Appointment Type");
                    c1Appointments.SetData(0, COL_APPOINTMENTTYPEID, "Appointment Type");
                    c1Appointments.SetData(0, COL_PROVIDER, "Provider");
                    c1Appointments.SetData(0, COL_PROVIDERID, "Provider ID");
                    c1Appointments.SetData(0, COL_TEMPLATEID, "Template ID");
                    c1Appointments.SetData(0, COL_TEMPLATEMSTID, "TeampletMaster ID");
                    c1Appointments.SetData(0, COL_LOCATION, "Location");
                    c1Appointments.SetData(0, COL_DEPATMENT, "Department");
                    c1Appointments.SetData(0, COL_STATUS, "Status");

                    CellRangeCollection crCellRanges = new CellRangeCollection(c1Appointments);
                    CellRange crMearge = c1Appointments.GetCellRange(0, 0, 0, 0);

                    c1Appointments.Cols[COL_PROVIDERID].Visible = false;
                    c1Appointments.Cols[COL_APPOINTMENTTYPEID].Visible = false;
                    c1Appointments.Cols[COL_TEMPLATEID].Visible = false;
                    c1Appointments.Cols[COL_TEMPLATEMSTID].Visible = false;
                    c1Appointments.Cols[COL_LOCATION].Visible = false;
                    c1Appointments.Cols[COL_DEPATMENT].Visible = false;
                    c1Appointments.Cols[COL_STATUS].Visible = false;

                    c1Appointments.Cols[COL_DATE].Width = 100;
                    c1Appointments.Cols[COL_TIME].Width = 0;
                    c1Appointments.Cols[COL_TIMETODISPLAY].Width = 150;
                    c1Appointments.Cols[COL_DURATION].Width = 100;
                    c1Appointments.Cols[COL_APPOINTMENTTYPE].Width = 200;
                    c1Appointments.Cols[COL_APPOINTMENTTYPEID].Width = 0;
                    c1Appointments.Cols[COL_PROVIDER].Width = 200;
                    c1Appointments.Cols[COL_PROVIDERID].Width = 100;
                    c1Appointments.Cols[COL_STATUS].Width = 0;

                    string sCurrentDate = "";
                    Int32 RowIndex = 0;
                    TimeSpan tsDuration = new TimeSpan();


                    //Find Provider Index from Collection
                    Int32 Index = 0;
                    if (cmbListViewProvider.SelectedIndex > -1)
                    {
                        for (int i = 0; i < oSearchProviders.Count; i++)
                        {
                            if (cmbListViewProvider.SelectedValue != null)
                            {
                                if (Convert.ToInt64(cmbListViewProvider.SelectedValue) == oSearchProviders[i].ProviderID)
                                {
                                    Index = i;
                                    break;
                                }
                            }
                        }

                        // Taken Main DataTable as dtSearchProvider which will fill c1Appointments
                        DataTable dtSearchProvider = new DataTable();

                        dtSearchProvider.Columns.Add("ProviderID");
                        dtSearchProvider.Columns.Add("ProviderName");
                        dtSearchProvider.Columns.Add("nASBaseFlag");
                        dtSearchProvider.Columns.Add("dtStartDate");
                        dtSearchProvider.Columns.Add("dtStartTime");
                        dtSearchProvider.Columns.Add("dtEndDate");
                        dtSearchProvider.Columns.Add("dtEndTime");
                        dtSearchProvider.Columns.Add("AppType");
                        dtSearchProvider.Columns.Add("AppTypeID");
                        dtSearchProvider.Columns.Add("TemplateID");
                        dtSearchProvider.Columns.Add("TemplateMasterID");
                        dtSearchProvider.Columns.Add("Location");
                        dtSearchProvider.Columns.Add("Department");
                        dtSearchProvider.Columns.Add("Status");
                        dtSearchProvider.Columns.Add("duration");
                        dtSearchProvider.Columns.Add("dateTime", System.Type.GetType("System.DateTime"));

                        DataRow row;

                        //Take Data from oSearchProviders[Index].ProviderFreeSlots and put in to DataTable named "dtSearchProvider"
                        for (int i = 0; i < oSearchProviders[Index].ProviderFreeSlots.Count; i++)
                        {

                            row = dtSearchProvider.NewRow();

                            row[0] = oSearchProviders[Index].ProviderID;
                            row[1] = oSearchProviders[Index].ProviderName;
                            row[2] = "";
                            row[3] = gloDateMaster.gloDate.DateAsNumber(oSearchProviders[Index].ProviderFreeSlots[i].Date.ToShortDateString());
                            row[4] = gloDateMaster.gloTime.TimeAsNumber(oSearchProviders[Index].ProviderFreeSlots[i].StartTime.ToShortTimeString());
                            row[5] = gloDateMaster.gloTime.TimeAsNumber(oSearchProviders[Index].ProviderFreeSlots[i].Date.ToShortTimeString());
                            row[6] = gloDateMaster.gloTime.TimeAsNumber(oSearchProviders[Index].ProviderFreeSlots[i].EndTime.ToShortTimeString());
                            row[7] = oSearchProviders[Index].ProviderFreeSlots[i].AppointmentType;
                            row[8] = oSearchProviders[Index].ProviderFreeSlots[i].AppointmentTypeID;
                            row[9] = oSearchProviders[Index].ProviderFreeSlots[i].TemplateAllocationID;
                            row[10] = oSearchProviders[Index].ProviderFreeSlots[i].TemplateAllocationMasterID;
                            row[11] = oSearchProviders[Index].ProviderFreeSlots[i].Location;
                            row[12] = oSearchProviders[Index].ProviderFreeSlots[i].Department;
                            row[13] = "open";
                            tsDuration = oSearchProviders[Index].ProviderFreeSlots[i].EndTime.Subtract(oSearchProviders[Index].ProviderFreeSlots[i].StartTime);
                            row[14] = tsDuration.TotalMinutes;

                            DateTime dateTime = new DateTime();
                            dateTime = Convert.ToDateTime(oSearchProviders[Index].ProviderFreeSlots[i].StartTime);

                            row[15] = dateTime;

                            dtSearchProvider.Rows.Add(row);

                        }


                        // Get Schedule and block times for Particular Provider if Checkbox Search in Template is Checked
                        //if (chkSearchInTemplate.Checked == false)
                        //{
                        //    gloSearching.gloSearching oSearch = new gloSearching.gloSearching(_databaseconnectionstring);
                        //    DataTable dtAppointments = new DataTable();
                        //    if (oCriteria.Dates.Count != 0)
                        //    {
                        //       dtAppointments = oSearch.GetAppointmentsUsingProviderID(oCriteria, Convert.ToInt64(oSearchProviders[Index].ProviderID));
                        //    }

                        //    //Take Data from dtAppointments and put in to DataTable named "dtSearchProvider"
                        //    if (dtAppointments != null)
                        //    {
                        //        if (dtAppointments.Rows.Count > 0)
                        //        {
                        //            string status = string.Empty;
                        //            for (int i = 0; i < dtAppointments.Rows.Count; i++)
                        //            {

                        //                //Do not fill Cancel/Noshow/Delete Appointments 
                        //                ASUsedStatus oASUsedStatus = ASUsedStatus.Unknown5;
                        //                oASUsedStatus = (ASUsedStatus)Convert.ToInt32(dtAppointments.Rows[i]["nUsedStatus"]);
                        //                if (oASUsedStatus == ASUsedStatus.Cancel || oASUsedStatus == ASUsedStatus.NoShow || oASUsedStatus == ASUsedStatus.Delete)
                        //                {
                        //                    continue;
                        //                }


                        //                row = dtSearchProvider.NewRow();

                        //                row[0] = dtAppointments.Rows[i]["ProviderID"].ToString();
                        //                row[1] = dtAppointments.Rows[i]["ProviderName"].ToString();
                        //                row[2] = dtAppointments.Rows[i]["nAsBaseFlag"].ToString();
                        //                row[3] = dtAppointments.Rows[i]["dtStartDate"].ToString();
                        //                row[4] = dtAppointments.Rows[i]["dtStartTime"].ToString();
                        //                row[5] = dtAppointments.Rows[i]["dtEndDate"].ToString();
                        //                row[6] = dtAppointments.Rows[i]["dtEndTime"].ToString();
                        //                row[7] = "";
                        //                row[8] = "";
                        //                row[9] = "";
                        //                row[10] = "";
                        //                row[11] = "";
                        //                row[12] = "";

                        //                if (oASUsedStatus == ASUsedStatus.Block)
                        //                {
                        //                    status = "block";
                        //                }
                        //                else
                        //                {
                        //                    status = "schedule";
                        //                }

                        //                row[13] = status;
                        //                tsDuration = ((gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"]))) - (gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtEndDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"]))));
                        //                row[14] = tsDuration.TotalMinutes;
                        //                DateTime dateTime = new DateTime();
                        //                dateTime = (gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"])));
                        //                row[15] = dateTime;

                        //                dtSearchProvider.Rows.Add(row);
                        //            }
                        //        }
                        //    }
                        //}
                        
                        //else
                        //{

                        //    gloSearching.gloSearching oSearch = new gloSearching.gloSearching(_databaseconnectionstring);
                        //    DataTable dtAppointments = new DataTable();
                        //    if (oCriteria.Dates.Count != 0)
                        //    {
                        //        //dtAppointments = oSearch.GetAppointmentsUsingProviderID(oCriteria, Convert.ToInt64(oSearchProviders[Index].ProviderID));
                        //    }

                        //    //Take Data from dtAppointments and put in to DataTable named "dtSearchProvider"
                        //    if (dtAppointments != null)
                        //    {
                        //        if (dtAppointments.Rows.Count > 0)
                        //        {
                        //            string status = string.Empty;
                        //            for (int i = 0; i < dtAppointments.Rows.Count; i++)
                        //            {

                        //                //Do not fill Cancel/Noshow/Delete Appointments 
                        //                ASUsedStatus oASUsedStatus = ASUsedStatus.Unknown5;
                        //                oASUsedStatus = (ASUsedStatus)Convert.ToInt32(dtAppointments.Rows[i]["nUsedStatus"]);
                        //                if (oASUsedStatus == ASUsedStatus.Cancel || oASUsedStatus == ASUsedStatus.NoShow || oASUsedStatus == ASUsedStatus.Delete)
                        //                {
                        //                    continue;
                        //                }

                        //                if (oASUsedStatus == ASUsedStatus.Block)
                        //                {
                        //                    row = dtSearchProvider.NewRow();

                        //                    row[0] = dtAppointments.Rows[i]["ProviderID"].ToString();
                        //                    row[1] = dtAppointments.Rows[i]["ProviderName"].ToString();
                        //                    row[2] = dtAppointments.Rows[i]["nAsBaseFlag"].ToString();
                        //                    row[3] = dtAppointments.Rows[i]["dtStartDate"].ToString();
                        //                    row[4] = dtAppointments.Rows[i]["dtStartTime"].ToString();
                        //                    row[5] = dtAppointments.Rows[i]["dtEndDate"].ToString();
                        //                    row[6] = dtAppointments.Rows[i]["dtEndTime"].ToString();
                        //                    row[7] = "";
                        //                    row[8] = "";
                        //                    row[9] = "";
                        //                    row[10] = "";
                        //                    row[11] = "";
                        //                    row[12] = "";

                        //                    // if (oASUsedStatus == ASUsedStatus.Block)
                        //                    // {
                        //                    status = "block";
                        //                    //  }


                        //                    row[13] = status;
                        //                    tsDuration = ((gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"]))) - (gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtEndDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"]))));
                        //                    row[14] = tsDuration.TotalMinutes;
                        //                    DateTime dateTime = new DateTime();
                        //                    dateTime = (gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])), Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"])));
                        //                    row[15] = dateTime;

                        //                    dtSearchProvider.Rows.Add(row);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        
                        DataView dvView = new DataView(dtSearchProvider);
                        dvView.Sort = "dateTime";


                        dtSearchProvider = new DataTable();
                        dtSearchProvider = dvView.ToTable();

                        // Fill original Grid with dataTable
                        for (int i = 0; i < dtSearchProvider.Rows.Count; i++)
                        {
                            //Create New Row
                            c1Appointments.Rows.Add();
                            RowIndex = c1Appointments.Rows.Count - 1;

                            c1Appointments.Rows[RowIndex].AllowMerging = true;

                            //Create new cell range for each date
                            if (sCurrentDate == gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])).ToShortDateString())
                            {
                                crMearge.r2 = RowIndex;
                            }
                            else
                            {
                                crMearge = c1Appointments.GetCellRange(RowIndex, 0, RowIndex, COL_COUNT - 1);
                                sCurrentDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])).ToShortDateString();
                            }


                            crCellRanges.Add(crMearge, true);



                            c1Appointments.SetData(RowIndex, COL_DATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])).ToShortDateString());
                            c1Appointments.SetData(RowIndex, COL_TIME, gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])), Convert.ToInt32(dtSearchProvider.Rows[i]["dtStartTime"])).ToShortTimeString());
                            c1Appointments.SetData(RowIndex, COL_TIMETODISPLAY, gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])), Convert.ToInt32(dtSearchProvider.Rows[i]["dtStartTime"])).ToShortTimeString() + " - " + gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtEndDate"])), Convert.ToInt32(dtSearchProvider.Rows[i]["dtEndTime"])).ToShortTimeString());
                            // tsDuration = ((gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtStartDate"])), Convert.ToInt32(dtSearchProvider.Rows[i]["dtEndTime"]))) - (gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtSearchProvider.Rows[i]["dtEndDate"])), Convert.ToInt32(dtSearchProvider.Rows[i]["dtStartTime"]))));
                            //c1Appointments.SetData(RowIndex, COL_DURATION, tsDuration.TotalMinutes);
                            c1Appointments.SetData(RowIndex, COL_DURATION, dtSearchProvider.Rows[i]["duration"].ToString());
                            c1Appointments.SetData(RowIndex, COL_APPOINTMENTTYPE, dtSearchProvider.Rows[i]["AppType"].ToString());
                            c1Appointments.SetData(RowIndex, COL_APPOINTMENTTYPEID, dtSearchProvider.Rows[i]["AppTypeID"].ToString());
                            c1Appointments.SetData(RowIndex, COL_PROVIDERID, dtSearchProvider.Rows[i]["ProviderID"].ToString());
                            c1Appointments.SetData(RowIndex, COL_PROVIDER, dtSearchProvider.Rows[i]["ProviderName"].ToString());
                            c1Appointments.SetData(RowIndex, COL_TEMPLATEID, dtSearchProvider.Rows[i]["TemplateID"].ToString());
                            c1Appointments.SetData(RowIndex, COL_TEMPLATEMSTID, dtSearchProvider.Rows[i]["TemplateMasterID"].ToString());
                            c1Appointments.SetData(RowIndex, COL_LOCATION, dtSearchProvider.Rows[i]["Location"].ToString());
                            c1Appointments.SetData(RowIndex, COL_DEPATMENT, dtSearchProvider.Rows[i]["Department"].ToString());
                            c1Appointments.SetData(RowIndex, COL_STATUS, dtSearchProvider.Rows[i]["Status"].ToString());

                        }
                    }

                    c1Appointments.Cols[COL_DATE].AllowMerging = true;
                    c1Appointments.AllowMerging = AllowMergingEnum.Free;

                    //Create Cell Styles

                    CellStyle csNormal;// = c1Appointments.Styles.Add("cs_Normal");
                    try
                    {
                        if (c1Appointments.Styles.Contains("cs_Normal"))
                        {
                            csNormal = c1Appointments.Styles["cs_Normal"];
                        }
                        else
                        {
                            csNormal = c1Appointments.Styles.Add("cs_Normal");
                            csNormal.BackColor = Color.FromArgb(222, 231, 250);
                            csNormal.TextAlign = TextAlignEnum.LeftTop;
                            csNormal.Border.Style = BorderStyleEnum.Flat;
                            csNormal.Border.Color = Color.FromArgb(240, 247, 255);
                            //csNormal.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold);

                        }

                    }
                    catch
                    {
                        csNormal = c1Appointments.Styles.Add("cs_Normal");
                        csNormal.BackColor = Color.FromArgb(222, 231, 250);
                        csNormal.TextAlign = TextAlignEnum.LeftTop;
                        csNormal.Border.Style = BorderStyleEnum.Flat;
                        csNormal.Border.Color = Color.FromArgb(240, 247, 255);
                        //csNormal.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold);

                    }
                  
                    CellStyle csAlternate;// = c1Appointments.Styles.Add("cs_Alternate");
                    try
                    {
                        if (c1Appointments.Styles.Contains("cs_Alternate"))
                        {
                            csAlternate = c1Appointments.Styles["cs_Alternate"];
                        }
                        else
                        {
                            csAlternate = c1Appointments.Styles.Add("cs_Alternate");
                            csAlternate.BackColor = Color.FromArgb(240, 247, 255);
                            csAlternate.TextAlign = TextAlignEnum.LeftTop;
                            csAlternate.Border.Style = BorderStyleEnum.Flat;
                            csAlternate.Border.Color = Color.FromArgb(222, 231, 250);
                            //csAlternate.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold);

                        }

                    }
                    catch
                    {
                        csAlternate = c1Appointments.Styles.Add("cs_Alternate");
                        csAlternate.BackColor = Color.FromArgb(240, 247, 255);
                        csAlternate.TextAlign = TextAlignEnum.LeftTop;
                        csAlternate.Border.Style = BorderStyleEnum.Flat;
                        csAlternate.Border.Color = Color.FromArgb(222, 231, 250);
                        //csAlternate.Font = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold);

                    }
 


                    //Apply Cell Style to Grid
                    CellRange cellRng;
                    for (int i = 0; i < crCellRanges.Count; i++)
                    {
                        cellRng = crCellRanges[i];
                        if (i % 2 == 0)
                            cellRng.Style = csNormal;
                        else
                            cellRng.Style = csAlternate;
                    }

                }

               //  Apply Block and schedule color to Respective Rows
                if (c1Appointments.Rows.Count > 0)
                {

                    CellStyle csBlock;// = c1Appointments.Styles.Add("cs_Block");
                    try
                    {
                        if (c1Appointments.Styles.Contains("cs_Block"))
                        {
                            csBlock = c1Appointments.Styles["cs_Block"];
                        }
                        else
                        {
                            csBlock = c1Appointments.Styles.Add("cs_Block");
                            csBlock.BackColor = Color.DarkRed;
                            csBlock.TextAlign = TextAlignEnum.LeftTop;
                            csBlock.Border.Style = BorderStyleEnum.Flat;
                            csBlock.Border.Color = Color.FromArgb(222, 231, 250);
                            csBlock.ForeColor = Color.White;
                            csBlock.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout);

                        }

                    }
                    catch
                    {
                        csBlock = c1Appointments.Styles.Add("cs_Block");
                        csBlock.BackColor = Color.DarkRed;
                        csBlock.TextAlign = TextAlignEnum.LeftTop;
                        csBlock.Border.Style = BorderStyleEnum.Flat;
                        csBlock.Border.Color = Color.FromArgb(222, 231, 250);
                        csBlock.ForeColor = Color.White;
                        csBlock.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout);

                    }
 


                    CellStyle csSchedule;// = c1Appointments.Styles.Add("cs_Schedule");
                    try
                    {
                        if (c1Appointments.Styles.Contains("cs_Schedule"))
                        {
                            csSchedule = c1Appointments.Styles["cs_Schedule"];
                        }
                        else
                        {
                            csSchedule = c1Appointments.Styles.Add("cs_Schedule");
                            csSchedule.BackColor = Color.Gray;
                            csSchedule.TextAlign = TextAlignEnum.LeftTop;
                            csSchedule.Border.Style = BorderStyleEnum.Flat;
                            csSchedule.Border.Color = Color.FromArgb(222, 231, 250);
                            csSchedule.ForeColor = Color.White;
                            csSchedule.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout);
      
                        }

                    }
                    catch
                    {
                        csSchedule = c1Appointments.Styles.Add("cs_Schedule");
                        csSchedule.BackColor = Color.Gray;
                        csSchedule.TextAlign = TextAlignEnum.LeftTop;
                        csSchedule.Border.Style = BorderStyleEnum.Flat;
                        csSchedule.Border.Color = Color.FromArgb(222, 231, 250);
                        csSchedule.ForeColor = Color.White;
                        csSchedule.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout);
      
                    }
               
                    for (int i = 1; i < c1Appointments.Rows.Count; i++)
                    {
                        if (Convert.ToString(c1Appointments.Rows[i][COL_STATUS]) == "block")
                        {
                            c1Appointments.SetCellStyle(i, COL_APPOINTMENTTYPE, csBlock);
                            c1Appointments.SetCellStyle(i, COL_DURATION, csBlock);
                            c1Appointments.SetCellStyle(i, COL_TIMETODISPLAY, csBlock);
                            c1Appointments.SetCellStyle(i, COL_PROVIDER, csBlock);
                        }
                        else if (Convert.ToString(c1Appointments.Rows[i][COL_STATUS]) == "schedule")
                        {
                            c1Appointments.SetCellStyle(i, COL_APPOINTMENTTYPE, csSchedule);
                            c1Appointments.SetCellStyle(i, COL_DURATION, csSchedule);
                            c1Appointments.SetCellStyle(i, COL_TIMETODISPLAY, csSchedule);
                            c1Appointments.SetCellStyle(i, COL_PROVIDER, csSchedule);
                    
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void ShowFollowupAppointments()
        {
            gloAppointmentBook.Books.AppointmentType ogloAppointmentType = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
            try
            {
                //DataTable oBindTable = new DataTable();
                //oBindTable.Columns.Add("ProviderID");
                //oBindTable.Columns.Add("Code");
                //oBindTable.Columns.Add("Provider Name");
                //DataRow dr = oBindTable.NewRow();
                //dr["Provider Name"] = sProviderName;
                //dr["Code"] = "";
                //dr["ProviderID"] = nProviderID;
                //oBindTable.Rows.Add(dr);
                //cmbProviders1.DataSource = oBindTable;
                //cmbProviders1.DisplayMember = "Provider Name";
                //cmbProviders1.ValueMember = "ProviderID";
                //cmbProviders1.SelectedIndex = 0;
                //cmbProviders1.Refresh();

                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (Convert.ToInt64(trvProvider.Nodes[i].Tag) == nProviderID)
                    {
                        trvProvider.Nodes[i].Checked = true;
                        break;
                    }
                }


                dtpStartDate.Value = dtFollowUpStartDate;
                dtpEndDate.Value = dtFollowUpEndDate;

                btn_AppSearch_Down_Click(null, null);
                chkSearchInTemplate_CheckedChanged(null, null);

                //cmbLocation_Appointment.SelectedIndex = -1;
                cmbDepartment_Appointment.SelectedIndex = -1;

                DataTable dtFollowup = new DataTable();
                dtFollowup = ogloAppointmentType.GetAppointmentTypes(gloAppointmentBook.AppointmentTypeFlag.Followup);
                //if (dtFollowup != null)
                //{
                //    cmbAppointmentType1.DataSource = dtFollowup;
                //    cmbAppointmentType1.DisplayMember = "sAppointmentType";
                //    cmbAppointmentType1.ValueMember = "nAppointmentTypeID";
                //    cmbAppointmentType1.Refresh();
                //}

                if (dtFollowup != null)
                {
                    for (int i = 0; i < dtFollowup.Rows.Count; i++)
                    {
                        for (int k = 0; k < trvAppointmentType.Nodes.Count; k++)
                        {
                            if (Convert.ToInt64(trvAppointmentType.Nodes[k].Tag) == Convert.ToInt64(dtFollowup.Rows[i]["nAppointmentTypeID"]))
                            {
                                trvAppointmentType.Nodes[k].Checked = true;
                                break;
                            }
                        }
                    }
                }

                if (dtFollowup != null && dtFollowup.Rows.Count > 0)
                    SearchAppointment();

                juc_Appointment.Date = dtFollowUpStartDate;
                juc_Appointment.Refresh();

                TimeSpan Duration = dtFollowUpEndDate.Date.Subtract(dtFollowUpStartDate.Date);
                if (Duration.TotalDays == 1)
                {
                    juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.DayView;
                }
                else
                {
                    juc_Appointment.View = Janus.Windows.Schedule.ScheduleView.MonthView;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void juc_Appointment_DatesChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Validate())
            //    {
            //        SearchAppointment();
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        #endregion

        #region Recurrance Criteria Selection Methods & Events

        private void FindRecurrence()
        {
            _IsPatternFinding = true;
            //oFindCriteria = new gloAppointmentScheduling.gloAppointment.Common.FindRecurrences(); its implemented at top level bcz we need it at save time also

            //Recurrence
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
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString());
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
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString()); //(MonthRange)cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString().GetHashCode();

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;

                }
                else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = MonthRange.January;

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString()); // (FirstLastCriteria)cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString().GetHashCode();
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString());  // (DayWeekday)cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString().GetHashCode();
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString());  // (MonthRange)cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString().GetHashCode();
                }
            }

            if (oFindCriteria.FindRecurrence() == true)
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

        private void BindRecurrenceEvents()
        {
            try
            {
                this.dtpRec_Range_StartDate.ValueChanged += new System.EventHandler(this.dtpRec_Range_StartDate_ValueChanged);
                this.rbRec_Range_EndBy.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndBy_CheckedChanged);
                this.rbRec_Range_EndAfterOccurence.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndAfterOccurence_CheckedChanged);
                this.dtpRec_Range_EndBy.ValueChanged += new System.EventHandler(this.dtpRec_Range_EndBy_ValueChanged);
                this.numRec_Range_EndAfterOccurence.ValueChanged += new System.EventHandler(this.numRec_Range_EndAfterOccurence_ValueChanged);
                this.rbRec_Pattern_Yearly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_CheckedChanged);
                this.rbRec_Pattern_Monthly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_CheckedChanged);
                this.rbRec_Pattern_Daily.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_CheckedChanged);
                this.rbRec_Pattern_Weekly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Weekly_CheckedChanged);
                this.numRec_Pattern_Daily_EveryDay.ValueChanged += new System.EventHandler(this.numRec_Pattern_Daily_EveryDay_ValueChanged);
                this.rbRec_Pattern_Daily_EveryWeekday.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryWeekday_CheckedChanged);
                this.rbRec_Pattern_Daily_EveryDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryDay_CheckedChanged);
                this.numRec_Pattern_Monthly_Criteria_Month.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Criteria_Month_ValueChanged);
                this.numRec_Pattern_Monthly_Day_Month.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Day_Month_ValueChanged);
                this.numRec_Pattern_Monthly_Day_Day.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Day_Day_ValueChanged);
                this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged);
                this.cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged);
                this.rbRec_Pattern_Monthly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Criteria_CheckedChanged);
                this.rbRec_Pattern_Monthly_Day.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Day_CheckedChanged);
                this.numRec_Pattern_Yearly_Every_MonthDay.ValueChanged += new System.EventHandler(this.numRec_Pattern_Yearly_Every_MonthDay_ValueChanged);
                this.cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged);
                this.cmbRec_Pattern_Yearly_Every_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged);
                this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged);
                this.cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged);
                this.rbRec_Pattern_Yearly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_Criteria_CheckedChanged);
                this.rbRec_Pattern_Yearly_EveryMonthDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Saturday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Saturday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Friday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Friday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Sunday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Sunday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Tuesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Tuesday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Wednesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Wednesday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Thursday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Thursday_CheckedChanged);
                this.ChkRec_Pattern_Weekly_Monday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Monday_CheckedChanged);
                this.numRec_Pattern_Weekly_WeekOn.ValueChanged += new System.EventHandler(this.numRec_Pattern_Weekly_WeekOn_ValueChanged);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Event which affect on changeing recurrence criteria"

        #region "Daily Pattern"
        private void rbRec_Pattern_Daily_EveryDay_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                {
                    rbRec_Pattern_Daily_EveryDay.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                    FindRecurrence();
                    return;
                }
                else
                {

                    rbRec_Pattern_Daily_EveryDay.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                }
            }
        }

        private void numRec_Pattern_Daily_EveryDay_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
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
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                if (rbRec_Pattern_Daily_EveryWeekday.Checked == true)
                {
                    rbRec_Pattern_Daily_EveryWeekday.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                    FindRecurrence();
                    return;
                }
                else
                {
                    rbRec_Pattern_Daily_EveryWeekday.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                }

            }
        }


        #endregion

        #region "Weekly Pattern"
        private void numRec_Pattern_Weekly_WeekOn_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Monday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Friday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                IsWeekDayChecked();
                FindRecurrence();
                return;
            }
        }

        private void ChkRec_Pattern_Weekly_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
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
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Monthly_Day_Day_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Monthly_Day_Month_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void rbRec_Pattern_Monthly_Criteria_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Monthly_Criteria_Month_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }
        #endregion

        #region "Yearly Pattern"
        private void rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Yearly_Every_MonthDay_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void rbRec_Pattern_Yearly_Criteria_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }
        #endregion

        #endregion

        #region "Pattern Selection"

        private void rbRec_Pattern_Daily_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Pattern_Daily.Checked == true)
            {
                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
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
            else
            {

                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbRec_Pattern_Weekly_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Pattern_Weekly.Checked == true)
            {
                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
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
            else
            {

                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbRec_Pattern_Monthly_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Pattern_Monthly.Checked == true)
            {
                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
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
            else
            {

                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbRec_Pattern_Yearly_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Pattern_Yearly.Checked == true)
            {
                rbRec_Pattern_Yearly.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
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
            else
            {

                rbRec_Pattern_Yearly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbDateTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (rbDateTime.Checked == true)
                //{
                //    pnlDateTime.Enabled = true;
                //    pnlRecurrance.Enabled = false;
                //}
                //else
                //{
                //    pnlDateTime.Enabled = false;
                //    pnlRecurrance.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void rbRec_Range_EndAfterOccurence_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Range_EndAfterOccurence.Checked == true)
            {
                rbRec_Range_EndAfterOccurence.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRec_Range_EndAfterOccurence.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbRecurrence_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (rbRecurrence.Checked == true)
                //{
                //    pnlRecurrance.Enabled = true;
                //}
                //else
                //{
                //    pnlRecurrance.Enabled = false;
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region "Pattern Range Selection"
        private void dtpRec_Range_StartDate_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Range_EndAfterOccurence_ValueChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
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
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
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

        #endregion

        #endregion

        #region " Designer Code for Search Panel "

        private void btn_Recurrencepattern_Down_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlRecurrance_Body.Visible = true;
            btn_Recurrencepattern_Down.Visible = false;
            btn_Recurrencepattern_UP.Visible = true;
            chkApplyRecPattern.Checked = true;
        }

        private void btn_Recurrencepattern_UP_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlRecurrance_Body.Visible = false;
            btn_Recurrencepattern_Down.Visible = true;
            btn_Recurrencepattern_UP.Visible = false;
            chkApplyRecPattern.Checked = false;

        }

        private void chkApplyRecPattern_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (chkApplyRecPattern.Checked == true)
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
            else
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
        }

        private void btn_ShowSearchPnl_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlVertical_tslp.Visible = false;
            pnlSearch.Visible = true;

        }

        private void btn_HideSearchPnl_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlSearch.Visible = false;
            pnlVertical_tslp.Visible = true;
        }

        private void btn_AdvanceSearch_UP_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnl_AdvanceSearch_Body.Visible = false;
            pnlAdvanceSearch_UP.Visible = false;
            pnlAdvanceSearch_Down.Visible = true;
            chkApplyAdvSearch.Checked = false;

        }

        private void btn_AdvanceSearch_Down_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnl_AdvanceSearch_Body.Visible = true;
            pnlAdvanceSearch_Down.Visible = false;
            pnlAdvanceSearch_UP.Visible = true;
            chkApplyAdvSearch.Checked = true;
            btn_AppSearch_Up_Click(null, null);

        }

        private void btn_AppSearch_Down_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlAppointmentSearchBody.Visible = true;
            pnlAppSearch_Down.Visible = false;
            pnlAppSearch_Up.Visible = true;

            chkSearchInTemplate.CheckedChanged -= chkSearchInTemplate_CheckedChanged;
            chkSearchInTemplate.Checked = true;
            chkSearchInTemplate.CheckedChanged += chkSearchInTemplate_CheckedChanged;

            btn_AdvanceSearch_UP_Click(null, null);
        }

        private void btn_AppSearch_Up_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlAppointmentSearchBody.Visible = false;
            pnlAppSearch_Up.Visible = false;
            pnlAppSearch_Down.Visible = true;

            chkSearchInTemplate.CheckedChanged -= chkSearchInTemplate_CheckedChanged;
            chkSearchInTemplate.Checked = true;
            chkSearchInTemplate.CheckedChanged += chkSearchInTemplate_CheckedChanged;

        }

        private void ts_btnSearch_Click(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            pnlVertical_tslp.Visible = false;
            pnlSearch.Visible = true;

        }

        #endregion " Designer Code for Search Panel "

        #region  " Private & Public Fill Methods "

        private void Fill_Location()
        {
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable dt = new DataTable();
            dt = oLocation.GetList();
            //nLocationID, sLocation, nClinicID 
            oLocation.Dispose();
            oLocation = null;

            DataRow r;
            r = dt.NewRow();
            r["nLocationID"] = 0;
            r["sLocation"] = "<All Locations>";
            r["nClinicID"] = 0;
            dt.Rows.InsertAt(r, 0);

            if (dt != null)
            {
                cmbLocation.DataSource = dt.Copy();
                cmbLocation.ValueMember = dt.Columns["nLocationID"].ColumnName;
                cmbLocation.DisplayMember = dt.Columns["sLocation"].ColumnName;

                cmbLocation_Appointment.DataSource = dt.Copy();
                cmbLocation_Appointment.ValueMember = dt.Columns["nLocationID"].ColumnName;
                cmbLocation_Appointment.DisplayMember = dt.Columns["sLocation"].ColumnName;
            }

           if (cmbLocation_Appointment != null && cmbLocation_Appointment.Items.Count > 0)
            {
                if (_DefaultLocationID > 0)
                {
                    cmbLocation_Appointment.SelectedValue = _DefaultLocationID;
                }
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
        private void Fill_Department()
        {
            gloAppointmentBook.Books.Department oDepartment = new gloAppointmentBook.Books.Department(_databaseconnectionstring);
            DataTable dt = new DataTable();
            dt = oDepartment.GetList();
            //nDepartmentID, sDepartment , nLocationID , sLocation , nClinicID 
            oDepartment.Dispose();
            oDepartment = null;

            DataRow r;
            r = dt.NewRow();
            r["nDepartmentID"] = 0;
            r["sDepartment"] = "";
            r["nLocationID"] = 0;
            r["sLocation"] = "";
            r["nClinicID"] = 0;

            dt.Rows.InsertAt(r, 0);
            if (dt != null)
            {
                cmbDepartment.DataSource = dt.Copy();
                cmbDepartment.ValueMember = dt.Columns["nDepartmentID"].ColumnName;
                cmbDepartment.DisplayMember = dt.Columns["sDepartment"].ColumnName;

                cmbDepartment_Appointment.DataSource = dt.Copy();
                cmbDepartment_Appointment.ValueMember = dt.Columns["nDepartmentID"].ColumnName;
                cmbDepartment_Appointment.DisplayMember = dt.Columns["sDepartment"].ColumnName;
            }
        }

        private void FillRecurrenceControls()
        {
            try
            {
                //Fill Monthly Criteria
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Clear();
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.first.ToString());
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.second.ToString());
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.third.ToString());
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.fourth.ToString());
                cmbRec_Pattern_Monthly_Criteria_FstLst.Items.Add(FirstLastCriteria.last.ToString());

                cmbRec_Pattern_Monthly_Criteria_DayWeekday.Items.Clear();
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
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Clear();
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.first.ToString());
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.second.ToString());
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.third.ToString());
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.fourth.ToString());
                cmbRec_Pattern_Yearly_Criteria_FstLst.Items.Add(FirstLastCriteria.last.ToString());

                cmbRec_Pattern_Yearly_Criteria_DayWeekday.Items.Clear();
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


                cmbRec_Pattern_Yearly_Every_Month.Items.Clear();
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


                cmbRec_Pattern_Yearly_Criteria_Month.Items.Clear();
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void FillAppointmentTypes()
        {
            try
            {
                gloAppointmentBook.Books.AppointmentType oAppointmentType = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
                DataTable dtAppointmentType = oAppointmentType.GetList(gloAppointmentBook.AppointmentProcedureType.AppointmentType);
                if (dtAppointmentType != null)
                {
                    trvAppointmentType.Nodes.Clear();
                    for (int i = 0; i < dtAppointmentType.Rows.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = Convert.ToString(dtAppointmentType.Rows[i]["sAppointmentType"]);
                        oNode.Tag = Convert.ToString(dtAppointmentType.Rows[i]["nAppointmentTypeID"]);
                        trvAppointmentType.Nodes.Add(oNode);
                        oNode = null;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void FillProviders()
        {

            gloAppointmnetScheduleCommon oCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);
            gloGeneralItem.gloItems oProviders = null;
            //gloAppointmentBook.Books.Resource ogloResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            try
            {
                trvProvider.Nodes.Clear();
                oProviders = oCommon.GetProviders();
                trvProvider.Nodes.Clear();
                if (oProviders != null)
                {
                    for (int i = 0; i < oProviders.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = oProviders[i].Description;
                        oNode.Tag = oProviders[i].ID;
                        trvProvider.Nodes.Add(oNode);
                        oNode = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oProviders != null)
                {
                    oProviders.Clear();
                    oProviders.Dispose();
                    oProviders = null;
                }
                oCommon.Dispose();
            }
        }

        #endregion  " Private & Public Fill Methods "

        #region Fill Control & oListControl Events

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.Providers:
                        {
                            //cmbProviders1.DataSource = null;
                            //cmbProviders1.Items.Clear();
                            //DataTable dtTemp = new DataTable();
                            //dtTemp.Columns.Add("ProviderID");
                            //dtTemp.Columns.Add("code");
                            //dtTemp.Columns.Add("Provider Name");
                            //if (oListControl.SelectedItems.Count > 0)
                            //{
                            //    for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            //    {
                            //        DataRow r;
                            //        r = dtTemp.NewRow();
                            //        //Column[0]= "ProviderID"
                            //        //Column[1]= "code"
                            //        //Column[2]= "Provider Name"
                            //        r["ProviderID"] = oListControl.SelectedItems[_Counter].ID.ToString(); // dt.Rows[i]["nProviderID"];
                            //        r["Code"] = oListControl.SelectedItems[_Counter].Code.ToString();
                            //        r["Provider Name"] = oListControl.SelectedItems[_Counter].Description.ToString();

                            //        dtTemp.Rows.Add(r);
                            //    }
                            //}
                            //cmbProviders1.DataSource = dtTemp;
                            //cmbProviders1.DisplayMember = "Provider Name";
                            //cmbProviders1.ValueMember = "ProviderID";
                        }
                        break;
                    case gloListControl.gloListControlType.Procedures:
                        {
                            //cmbProcedures.Items.Clear();
                            cmbProcedures.DataSource = null;
                            cmbProcedures.Items.Clear();
                            //cmbResources.Items.Clear();
                            cmbResources.DataSource = null;
                            cmbResources.Items.Clear();

                            DataTable dtTemp = new DataTable();
                            dtTemp.Columns.Add("ProcedureID");
                            dtTemp.Columns.Add("Code");
                            dtTemp.Columns.Add("Name");

                            DataTable dtTemp1 = new DataTable();
                            dtTemp1.Columns.Add("ResourceID");
                            dtTemp1.Columns.Add("Code");
                            dtTemp1.Columns.Add("Name");


                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    DataRow r;
                                    r = dtTemp.NewRow();
                                    //Column[0]= "ProcedureID"
                                    //Column[1]= "Code"
                                    //Column[2]= "Name"
                                    r["ProcedureID"] = oListControl.SelectedItems[_Counter].ID.ToString(); // dt.Rows[i]["nProviderID"];
                                    r["Code"] = oListControl.SelectedItems[_Counter].Code.ToString();
                                    r["Name"] = oListControl.SelectedItems[_Counter].Description.ToString();

                                    dtTemp.Rows.Add(r);

                                    if (oListControl.SelectedItems[_Counter].SubItems.Count > 0)
                                    {
                                        for (Int32 _Counter1 = 0; _Counter1 <= oListControl.SelectedItems[_Counter].SubItems.Count - 1; _Counter1++)
                                        {
                                            DataRow r1;
                                            r1 = dtTemp1.NewRow();
                                            //Column[0]= "ResourceID"
                                            //Column[1]= "Code"
                                            //Column[2]= "Name"
                                            r1["ResourceID"] = oListControl.SelectedItems[_Counter].SubItems[_Counter1].ID.ToString(); // dt.Rows[i]["nProviderID"];
                                            r1["Code"] = oListControl.SelectedItems[_Counter].SubItems[_Counter1].Code.ToString();
                                            r1["Name"] = oListControl.SelectedItems[_Counter].SubItems[_Counter1].Description.ToString();

                                            dtTemp1.Rows.Add(r1);
                                        }
                                    }
                                }
                                chkApplyAdvSearch.Checked = true;
                                rbProblemTypeFilter.Checked = true;
                            }
                            cmbProcedures.DataSource = dtTemp;
                            cmbProcedures.DisplayMember = "Name";
                            cmbProcedures.ValueMember = "ProcedureID";

                            cmbResources.DataSource = dtTemp1;
                            cmbResources.DisplayMember = "Name";
                            cmbResources.ValueMember = "ResourceID";

                        }

                        break;
                    case gloListControl.gloListControlType.Resources:
                        {
                         //   cmbResources.Items.Clear();
                            cmbResources.DataSource = null;
                            cmbResources.Items.Clear();
                            DataTable dtTemp = new DataTable();
                            dtTemp.Columns.Add("ResourceID");
                            dtTemp.Columns.Add("Code");
                            dtTemp.Columns.Add("Name");

                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    DataRow r;
                                    r = dtTemp.NewRow();
                                    //Column[0]= "ResourceID"
                                    //Column[1]= "Code"
                                    //Column[2]= "Name"
                                    r["ResourceID"] = oListControl.SelectedItems[_Counter].ID.ToString(); // dt.Rows[i]["nProviderID"];
                                    r["Code"] = oListControl.SelectedItems[_Counter].Code.ToString();
                                    r["Name"] = oListControl.SelectedItems[_Counter].Description.ToString();

                                    dtTemp.Rows.Add(r);
                                }
                                chkApplyAdvSearch.Checked = true;
                                rbResourceFilter.Checked = true;
                            }

                            cmbResources.DataSource = dtTemp;
                            cmbResources.DisplayMember = "Name";
                            cmbResources.ValueMember = "ResourceID";
                        }
                        break;
                    case gloListControl.gloListControlType.AppointmentType:
                        {
                            //cmbAppointmentType1.DataSource = null;
                            //cmbAppointmentType1.Items.Clear();
                            //DataTable dtTemp = new DataTable();
                            //dtTemp.Columns.Add("AppointmentTypeID");
                            //dtTemp.Columns.Add("AppointmentTypeDesc");
                            //if (oListControl.SelectedItems.Count > 0)
                            //{
                            //    for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            //    {
                            //        DataRow r;
                            //        r = dtTemp.NewRow();
                            //        r["AppointmentTypeID"] = oListControl.SelectedItems[_Counter].ID.ToString(); // dt.Rows[i]["nProviderID"];
                            //        r["AppointmentTypeDesc"] = oListControl.SelectedItems[_Counter].Description.ToString();
                            //        dtTemp.Rows.Add(r);
                            //    }
                            //    chkSearchInTemplate.Checked = true;
                            //}
                            //cmbAppointmentType1.DataSource = dtTemp;
                            //cmbAppointmentType1.DisplayMember = "AppointmentTypeDesc";
                            //cmbAppointmentType1.ValueMember = "AppointmentTypeID";
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
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                tsb_SearchAppointment.Enabled = true;
                pnlListControl.Visible = false;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            tsb_SearchAppointment.Enabled = true;
            pnlListControl.Visible = false;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            gloAppointmentScheduling.gloAppointmnetScheduleCommon oApptCommon = new gloAppointmentScheduling.gloAppointmnetScheduleCommon(_databaseconnectionstring);
            gloGeneralItem.gloItems oListItems = null;
            int _Counter = 0;
           
            cmbDepartment.DataSource = null;
            cmbDepartment.Items.Clear();
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            try
            {
                //Departments
                if (cmbLocation.SelectedItem != null)
                {
                   // oListItems = new gloGeneralItem.gloItems();
                    oListItems = oApptCommon.GetDepartments(Convert.ToInt64(((System.Data.DataRowView)(cmbLocation.Items[cmbLocation.SelectedIndex])).Row.ItemArray[0]));
                    DataTable oTableDepartments = new DataTable();

                    oTableDepartments.Columns.Add("ID");
                    oTableDepartments.Columns.Add("DispName");

                    DataRow r;
                    r = oTableDepartments.NewRow();
                    r["ID"] = 0;
                    r["DispName"] = "";
                    oTableDepartments.Rows.InsertAt(r, 0);
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
                    cmbDepartment.DataSource = oTableDepartments;
                    cmbDepartment.DisplayMember = "DispName";
                    cmbDepartment.ValueMember = "ID";
                    cmbDepartment.Refresh();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oApptCommon.Dispose();
                _Counter = 0;
            }
        }

        private void cmbLocation_Appointment_SelectedIndexChanged(object sender, EventArgs e)
        {
            gloAppointmentScheduling.gloAppointmnetScheduleCommon oApptCommon = new gloAppointmentScheduling.gloAppointmnetScheduleCommon(_databaseconnectionstring);
            gloGeneralItem.gloItems oListItems=null;
            int _Counter = 0;
           
            cmbDepartment_Appointment.DataSource = null;
            cmbDepartment_Appointment.Items.Clear();
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            try
            {
                //Departments
                if (cmbLocation_Appointment.SelectedItem != null)
                {
                    //oListItems = new gloGeneralItem.gloItems();
                    oListItems = oApptCommon.GetDepartments(Convert.ToInt64(((System.Data.DataRowView)(cmbLocation_Appointment.Items[cmbLocation_Appointment.SelectedIndex])).Row.ItemArray[0]));
                    DataTable oTableDepartments = new DataTable();

                    oTableDepartments.Columns.Add("ID");
                    oTableDepartments.Columns.Add("DispName");

                    DataRow r;
                    r = oTableDepartments.NewRow();
                    r["ID"] = 0;
                    r["DispName"] = "";
                    oTableDepartments.Rows.InsertAt(r, 0);
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
                    cmbDepartment_Appointment.DataSource = oTableDepartments;
                    cmbDepartment_Appointment.DisplayMember = "DispName";
                    cmbDepartment_Appointment.ValueMember = "ID";
                    cmbDepartment_Appointment.Refresh();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oApptCommon.Dispose();
                _Counter = 0;
            }
        }

        private void btn_BrowseProcedure_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}

                if (oListControl != null)
                {
                    for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
                    {
                        if (pnlListControl.Controls[i].Name == oListControl.Name)
                        {
                            pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Procedures, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Procedures";
                tsb_SearchAppointment.Enabled = false;
                _CurrentControlType = gloListControl.gloListControlType.Procedures;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                //this.Controls.Add(oListControl);
                pnlListControl.Controls.Add(oListControl);

                for (int i = 0; i <= cmbProcedures.Items.Count - 1; i++)
                {
                    gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                    Int64 _tempid = Convert.ToInt64(((System.Data.DataRowView)(cmbProcedures.Items[i])).Row.ItemArray[0]);
                    oItem.ID = _tempid; // ProcedureID
                    oItem.Code = ((System.Data.DataRowView)(cmbProcedures.Items[i])).Row.ItemArray[1].ToString();  // Procedure Code 
                    oItem.Description = ((System.Data.DataRowView)(cmbProcedures.Items[i])).Row.ItemArray[2].ToString(); // Procedure Name

                    oListControl.SelectedItems.Add(oItem);
                    oItem.Dispose();
                    oItem = null;
                }

                pnlListControl.Parent = this;
                pnlListControl.SetBounds(cmbProcedures.Location.X, 378, 0, 0, BoundsSpecified.Location);
                pnlListControl.Visible = true;
                pnlListControl.BringToFront();

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_BrowseResource_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}

                if (oListControl != null)
                {
                    for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
                    {
                        if (pnlListControl.Controls[i].Name == oListControl.Name)
                        {
                            pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Resources, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Resources";
                tsb_SearchAppointment.Enabled = false;
                _CurrentControlType = gloListControl.gloListControlType.Resources;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                //this.Controls.Add(oListControl);
                pnlListControl.Controls.Add(oListControl);

                for (int i = 0; i <= cmbResources.Items.Count - 1; i++)
                {
                    gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                    Int64 _tempid = Convert.ToInt64(((System.Data.DataRowView)(cmbResources.Items[i])).Row.ItemArray[0]);
                    oItem.ID = _tempid; // ResourcesID
                    oItem.Code = ((System.Data.DataRowView)(cmbResources.Items[i])).Row.ItemArray[1].ToString();  // Resources Code 
                    oItem.Description = ((System.Data.DataRowView)(cmbResources.Items[i])).Row.ItemArray[2].ToString(); // Resources Name

                    oListControl.SelectedItems.Add(oItem);
                    oItem.Dispose();
                    oItem = null;
                }

                pnlListControl.Parent = this;
                pnlListControl.SetBounds(cmbResources.Location.X, 407, 0, 0, BoundsSpecified.Location);
                pnlListControl.Visible = true;
                pnlListControl.BringToFront();

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_BrowseLocation_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_BrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                ////if (oListControl != null)
                ////{
                ////    for (int i = this.Controls.Count - 1; i >= 0; i--)
                ////    {
                ////        if (this.Controls[i].Name == oListControl.Name)
                ////        {
                ////            this.Controls.Remove(this.Controls[i]);
                ////            break;
                ////        }
                ////    }
                ////}

                //if (oListControl != null)
                //{
                //    for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (pnlListControl.Controls[i].Name == oListControl.Name)
                //        {
                //            pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
                //            break;
                //        }
                //    }
                //}

                //oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                //oListControl.ClinicID = _ClinicID;
                //oListControl.ControlHeader = "Provider";
                //tsb_SearchAppointment.Enabled = false;

                //_CurrentControlType = gloListControl.gloListControlType.Providers;
                //oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                //oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ////this.Controls.Add(oListControl);
                //pnlListControl.Controls.Add(oListControl);


                //if (cmbProviders1.Items.Count > 0)
                //{
                //    for (int i = 0; i <= cmbProviders1.Items.Count - 1; i++)
                //    {
                //        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                //        Int64 _tempid = Convert.ToInt64(((System.Data.DataRowView)(cmbProviders1.Items[i])).Row.ItemArray[0]);
                //        oItem.ID = _tempid; // ProviderID
                //        oItem.Code = ((System.Data.DataRowView)(cmbProviders1.Items[i])).Row.ItemArray[1].ToString();  // Code = ""
                //        oItem.Description = ((System.Data.DataRowView)(cmbProviders1.Items[i])).Row.ItemArray[2].ToString(); // Provider Name


                //        oListControl.SelectedItems.Add(oItem);
                //        oItem.Dispose();
                //    }
                //}

                //pnlListControl.Parent = this;
                //pnlListControl.SetBounds(cmbProviders1.Location.X, 130, 0, 0, BoundsSpecified.Location);
                //pnlListControl.Visible = true;
                //pnlListControl.BringToFront();

                //oListControl.OpenControl();
                //oListControl.Dock = DockStyle.Fill;
                //oListControl.BringToFront();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_BrowseAppointmentType_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                ////if (oListControl != null)
                ////{
                ////    for (int i = this.Controls.Count - 1; i >= 0; i--)
                ////    {
                ////        if (this.Controls[i].Name == oListControl.Name)
                ////        {
                ////            this.Controls.Remove(this.Controls[i]);
                ////            break;
                ////        }
                ////    }
                ////}

                //if (oListControl != null)
                //{
                //    for (int i = pnlListControl.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (pnlListControl.Controls[i].Name == oListControl.Name)
                //        {
                //            pnlListControl.Controls.Remove(pnlListControl.Controls[i]);
                //            break;
                //        }
                //    }
                //}

                //oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.AppointmentType, true, this.Width);
                //oListControl.ClinicID = _ClinicID;
                //oListControl.ControlHeader = " Appointment Type";
                //tsb_SearchAppointment.Enabled = false;

                //_CurrentControlType = gloListControl.gloListControlType.AppointmentType;
                //oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                //oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ////this.Controls.Add(oListControl);
                //pnlListControl.Controls.Add(oListControl);

                //if (cmbAppointmentType1.Items.Count > 0)
                //{
                //    for (int i = 0; i <= cmbAppointmentType1.Items.Count - 1; i++)
                //    {
                //        cmbAppointmentType1.SelectedIndex = i;
                //        cmbAppointmentType1.Refresh();

                //        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                //        Int64 _tempid = Convert.ToInt64(cmbAppointmentType1.SelectedValue);
                //        oItem.ID = _tempid; // ProviderID
                //        oItem.Code = "";  // Code = ""
                //        oItem.Description = Convert.ToString(cmbAppointmentType1.SelectedText); // Provider Name

                //        oListControl.SelectedItems.Add(oItem);
                //        oItem.Dispose();
                //    }
                //}

                //pnlListControl.Parent = this;
                //pnlListControl.SetBounds(cmbAppointmentType1.Location.X, 278, 0, 0, BoundsSpecified.Location);
                //pnlListControl.Visible = true;
                //pnlListControl.BringToFront();

                //oListControl.OpenControl();
                //oListControl.Dock = DockStyle.Fill;
                //oListControl.BringToFront();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ClearProvider_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //cmbProviders1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ClearProcedure_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            
                cmbProcedures.DataSource = null;
                cmbProcedures.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ClearResource_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
               
                cmbResources.DataSource = null;
                cmbResources.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ClearAppointmentType_Click(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //cmbAppointmentType1.DataSource = null;
                //cmbAppointmentType1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void juc_Appointment_MouseMove(object sender, MouseEventArgs e)
        {
            ScheduleAppointment oAppointment = juc_Appointment.GetAppointmentAt(e.X, e.Y);
            if (oAppointment != null)
            {
                if (Convert.ToString(GetTagElement(oAppointment.Tag.ToString(), '~', 1)) == "FreeSlot")
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void chkSearchInTemplate_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (chkSearchInTemplate.Checked == true)
            {
                chkApplyAdvSearch.Checked = false;
                num_Duration.Visible = false;
                //cmbAMPMAll.Visible = false;
                lblSimple_Duration.Visible = false;
                btnClearAllAppTypes.Visible = true;
                btnSelectAllAppTypes.Visible = false;
                //btnSelectAllAppTypes_Click(null, null);
                if (trvAppointmentType.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
                    {
                        trvAppointmentType.Nodes[i].Checked = true;
                    }
                }
            }
            else
            {
                num_Duration.Visible = true;
                //cmbAMPMAll.Visible = true;
                lblSimple_Duration.Visible = true;

                //Uncheck All Appointment Types
                //Commented by Shubhangi
                //btnSelectAllAppTypes.Text = "Clear All";
                btnClearAllAppTypes.Visible = true;
                //Commented by Shubhangi
                //btnSelectAllAppTypes_Click(null, null);
                btnClearAllAppTypes_Click(null, null);
            }
        }

        private void chkApplyAdvSearch_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (chkApplyAdvSearch.Checked == true)
            {
                chkSearchInTemplate.CheckedChanged -= chkSearchInTemplate_CheckedChanged;
                chkSearchInTemplate.Checked = true;
                chkSearchInTemplate.CheckedChanged += chkSearchInTemplate_CheckedChanged;
            }
        }

        private void trvAppointmentType_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                Int16 nSelectedNodeCount = 0;
                for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
                {
                    if (trvAppointmentType.Nodes[i].Checked == true)
                        nSelectedNodeCount++;
                }

                chkSearchInTemplate.CheckedChanged -= chkSearchInTemplate_CheckedChanged;
                if (nSelectedNodeCount > 0)
                {
                    chkSearchInTemplate.Checked = true;
                    num_Duration.Visible = false;
                    lblSimple_Duration.Visible = false;
                }
                else
                {
                    chkSearchInTemplate.Checked = false;
                    num_Duration.Visible = true;
                    lblSimple_Duration.Visible = true;
                }
                chkSearchInTemplate.CheckedChanged += chkSearchInTemplate_CheckedChanged;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion

        #region Register Appointment

        //22-Nov-13 Aniket: Optimization of loading the patient list control
        void oPatientListControl_GridRowSelect_Click(object sender, EventArgs e)
        {

        }

        void oPatientListControl_Grid_MouseDown(object sender, EventArgs e)
        {
        }

        private void juc_Appointment_Click(object sender, EventArgs e)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            try
            {



                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                if (juc_Appointment.CurrentAppointment != null)
                {
                    string _BlockType = "";
                    _BlockType = Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));

                    if (_BlockType != "Template")
                    {
                        //Validation for TemplateAppointment creation

                        if (ogloSettings.GetSettingUserSpecific("RegisterTemplateAppointmentOnly", _UserID, _ClinicID) == true)
                        {
                            MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }


                    if (_BlockType == "FreeSlot" || _BlockType == "Template")
                    {

                        //oSmartPatient = new gloPatient.PatientListControl(0, _databaseconnectionstring);
                        
                       //22-Nov-13 Aniket: Optimization of loading the patient list control

                        this.Cursor = Cursors.WaitCursor;


                        if (pnlOther2.Visible == false && oSmartPatient == null)
                        {


                            if (oSmartPatient != null)
                            {
                                for (int i = pnlOther2.Controls.Count - 1; i >= 0; i--)
                                {
                                    if (pnlOther2.Controls[i].Name == oSmartPatient.Name)
                                    {
                                        oSmartPatient = null;
                                        pnlOther2.Controls.Remove(oSmartPatient);
                                        //pnlOther2.Controls.RemoveAt(i);
                                        break;
                                    }
                                }
                                try
                                {
                                    oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                                    oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                                    oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                                    oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                                }
                                catch
                                {
                                }
                                oSmartPatient.Dispose();
                                oSmartPatient = null;
                            }


                            oSmartPatient = new gloPatient.PatientListControl();
                            //oSmartPatient.IsOpenedFromCalender = true;

                            oSmartPatient.DatabaseConnection = _databaseconnectionstring;

                            oSmartPatient.ClinicID = _ClinicID;

                            //oSmartPatient.ItemSelectedClick += new gloPatient.gloSmartPatientControl.ItemSelected(oSmartPatient_ItemSelectedClick);
                            //oSmartPatient.ItemClosedClick += new gloPatient.gloSmartPatientControl.ItemClosed(oSmartPatient_ItemClosedClick);

                            //22-Nov-13 Aniket: Optimization of loading the patient list control
                            oSmartPatient.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                            oSmartPatient.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                            oSmartPatient.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                            oSmartPatient.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                            pnlOther2.Height = 0;
                            pnlOther2.Visible = true;
                            pnlOther2.Controls.Add(oSmartPatient);
                            //oSmartPatient.LoadPatients();
                            //22-Nov-13 Aniket: Optimization of loading the patient list control
                            oSmartPatient.FillPatients();
                            oSmartPatient.Padding = new Padding(0);
                            oSmartPatient.BringToFront();
                            oSmartPatient.Dock = DockStyle.Fill;
                            oSmartPatient.Select();
                            oSmartPatient.txtSearch.Text = "";
                            oSmartPatient.txtSearch.SelectionStart = 1;
                            oSmartPatient.txtSearch.Select();
                            pnlOther2.Height = 300;
                        }
                        this.Cursor = Cursors.Default;

                    }
                    else
                    {
                        if (oSmartPatient != null)
                        {

                            for (int i = pnlOther2.Controls.Count - 1; i >= 0; i--)
                            {
                                if (pnlOther2.Controls[i].Name == oSmartPatient.Name)
                                {
                                    oSmartPatient = null;
                                    pnlOther2.Controls.Remove(oSmartPatient);
                                    //pnlOther2.Controls.RemoveAt(i);
                                    break;
                                }
                               

                            }
                            try
                            {
                                oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                                oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                                oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                                oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                            }
                            catch
                            {
                            }
                        }
                        pnlOther2.Visible = false;

                    }
                }
                else
                {
                    if (oSmartPatient != null)
                    {
                        for (int i = pnlOther2.Controls.Count - 1; i >= 0; i--)
                        {
                            if (pnlOther2.Controls[i].Name == oSmartPatient.Name)
                            {
                                oSmartPatient = null;
                                pnlOther2.Controls.Remove(oSmartPatient);
                                //pnlOther2.Controls.RemoveAt(i);
                                break;
                            }
                            

                        }
                        try
                        {
                            oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                            oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                            oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                            oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                        }
                        catch
                        {
                        }
                    }
                    pnlOther2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
            }


        }

        private void c1Appointments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            try
            {
                //Validation for TemplateAppointment creation
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                HitTestInfo HitTest = c1Appointments.HitTest(e.X, e.Y);
                if (HitTest.Row > 0 && HitTest.Column > COL_DATE)
                {
                    //string status= Convert.ToString(c1Appointments.Rows[c1Appointments.Row][COL_STATUS]);
                    string status = Convert.ToString(c1Appointments.Rows[HitTest.Row][COL_STATUS]);

                    if (status == "block")
                    {
                        //  MessageBox.Show("New appointments cannot be set on blocks time. Please contact your administrator for more information.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (status == "schedule")
                    {
                        // MessageBox.Show("New appointments cannot be set on schedule time. Please contact your administrator for more information.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (chkSearchInTemplate.Checked == false)
                    {

                        if (ogloSettings.GetSettingUserSpecific("RegisterTemplateAppointmentOnly", _UserID, _ClinicID) == true)
                        {
                            MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    if (oSmartPatient != null)
                    {
                        for (int i = pnlOther2.Controls.Count - 1; i >= 0; i--)
                        {
                            if (pnlOther2.Controls[i].Name == oSmartPatient.Name)
                            {
                                pnlOther2.Controls.RemoveAt(i);
                                break;
                            }

                        }

                        try
                        {
                            oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                            oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                            oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                            oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                        }
                        catch
                        {
                        }
                        oSmartPatient.Dispose();
                        oSmartPatient = null;
                    }

                    //oSmartPatient = new gloPatient.PatientListControl(0, _databaseconnectionstring);
                    oSmartPatient = new gloPatient.PatientListControl(); 
                    oSmartPatient.DatabaseConnection = _databaseconnectionstring;

                    oSmartPatient.ClinicID = _ClinicID;
                    
                    //oSmartPatient.ItemSelectedClick += new gloPatient.gloSmartPatientControl.ItemSelected(oSmartPatient_ItemSelectedClick);
                    //oSmartPatient.ItemClosedClick += new gloPatient.gloSmartPatientControl.ItemClosed(oSmartPatient_ItemClosedClick);

                    //22-Nov-13 Aniket: Optimization of loading the patient list control
                    oSmartPatient.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                    oSmartPatient.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                    oSmartPatient.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                    oSmartPatient.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                    pnlOther2.Visible = true;
                    pnlOther2.Height = 300;

                    
                    pnlOther2.Controls.Add(oSmartPatient);

                    //oSmartPatient.LoadPatients();

                    //22-Nov-13 Aniket: Optimization of loading the patient list control
                    oSmartPatient.FillPatients();
                    oSmartPatient.Padding = new Padding(0);
                    oSmartPatient.BringToFront();
                    oSmartPatient.Dock = DockStyle.Fill;
                    oSmartPatient.Select();
                    oSmartPatient.txtSearch.Text = "";
                    oSmartPatient.txtSearch.SelectionStart = 1;
                    oSmartPatient.txtSearch.Select();

                }
                else
                {
                    if (oSmartPatient != null)
                    {
                        for (int i = pnlOther2.Controls.Count - 1; i >= 0; i--)
                        {
                            if (pnlOther2.Controls[i].Name == oSmartPatient.Name)
                            {
                                pnlOther2.Controls.RemoveAt(i);
                                break;
                            }
                           

                        }
                        try
                        {
                            oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                            oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                            oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                            oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                        }
                        catch
                        {
                        }
                    }
                    pnlOther2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
            }


        }

        void oSmartPatient_ItemClosedClick(object sender, EventArgs e)
        {
            if (oSmartPatient != null)
            {
                pnlOther2.Controls.Remove(oSmartPatient);
                try
                {
                    oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                    oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                    oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                    oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                }
                catch
                {
                }
            }
            //if (oSmartPatient != null) { oSmartPatient.Dispose(); }
            pnlOther2.Visible = false;
        }

        void oSmartPatient_ItemSelectedClick(object sender, EventArgs e)
        {
            if (oSmartPatient.PatientID > 0)
            {
                if (pnlCalendarView.Visible == true)
                {
                    #region "Setup Appointment from calendar View "

                    if (juc_Appointment.CurrentAppointment != null)
                    {

                        frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                        SetAppointmentParameter oAppParameters = new SetAppointmentParameter();
                        try
                        {
                            string _BlockType = "";
                            _BlockType = Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 1));

                            #region "Set Appointment Parameter"

                            oAppParameters.MasterAppointmentID = 0;
                            oAppParameters.AppointmentID = 0;
                            oAppParameters.AppointmentFlag = AppointmentScheduleFlag.Appointment;
                            oAppParameters.AppointmentTypeID = 0;
                            oAppParameters.AppointmentTypeCode = "";
                            oAppParameters.AppointmentTypeDesc = "";
                            oAppParameters.ProviderID = Convert.ToInt64(juc_Appointment.CurrentOwner.Value.ToString());
                            oAppParameters.ProviderName = juc_Appointment.CurrentOwner.Text;
                            oAppParameters.ProblemTypes = null;
                            oAppParameters.Resources = null;
                            oAppParameters.PatientID = oSmartPatient.PatientID;
                            oAppParameters.AddTrue_ModifyFalse_Flag = true;
                            oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifySingleAppointmentFromReccurence = false;
                            oAppParameters.Location = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 2).ToString();
                            oAppParameters.Department = GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 3).ToString();
                            oAppParameters.StartDate = juc_Appointment.CurrentAppointment.StartTime;
                            oAppParameters.StartTime = juc_Appointment.CurrentAppointment.StartTime;
                            oAppParameters.Duration = Convert.ToDecimal(juc_Appointment.CurrentAppointment.Duration.TotalMinutes);
                            oAppParameters.ClinicID = _ClinicID;
                            oAppParameters.LineNumber = 0;

                            //Not Applicable
                            oAppParameters.TemplateAllocationMasterID = 0;
                            oAppParameters.TemplateAllocationID = 0;
                            //--

                            oAppParameters.LoadParameters = true;

                            #endregion
                            if (_BlockType == "FreeSlot")
                            {
                                oAppParameters.AppointmentTypeID = 0;
                                oAppParameters.AppointmentTypeCode = "";
                                oAppParameters.AppointmentTypeDesc = "";

                                oAppParameters.TemplateAllocationMasterID = 0;
                                oAppParameters.TemplateAllocationID = 0;
                            }
                            else if (_BlockType == "Template")
                            {
                                oAppParameters.AppointmentTypeID = 0;
                                oAppParameters.AppointmentTypeCode = "";
                                oAppParameters.AppointmentTypeDesc = Convert.ToString(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 4));

                                oAppParameters.TemplateAllocationMasterID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 5));
                                oAppParameters.TemplateAllocationID = Convert.ToInt64(GetTagElement(juc_Appointment.CurrentAppointment.Tag.ToString(), '~', 6));
                            }

                            oSetupAppointment.SetAppointmentParameters = oAppParameters;
                            oSetupAppointment.ShowDialog(this);

                            if (oSetupAppointment.AppointmentChanged)
                            {
                                pnlOther2.Visible = false;
                                if (oSmartPatient != null)
                                {

                                    pnlOther2.Controls.Remove(oSmartPatient);
                                    try
                                    {
                                        oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                                        oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                                        oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                                        oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                                    }
                                    catch
                                    {
                                    }
                                    oSmartPatient.Dispose();
                                    oSmartPatient = null;
                                }
                                SearchAppointment();
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        finally
                        {
                            oSetupAppointment.Dispose();
                            oAppParameters.Dispose();
                        }
                    }
                    #endregion
                }
                else if (pnlListView.Visible == true)
                {
                    #region "Setup Appointment from List View "

                    if (c1Appointments.Row > 0 && c1Appointments.Col > COL_DATE)
                    {
                        frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
                        SetAppointmentParameter oAppParameters = new SetAppointmentParameter();
                        try
                        {

                            #region "Set Appointment Parameter"

                            oAppParameters.MasterAppointmentID = 0;
                            oAppParameters.AppointmentID = 0;
                            oAppParameters.AppointmentFlag = AppointmentScheduleFlag.Appointment;
                            oAppParameters.AppointmentTypeID = 0;
                            oAppParameters.AppointmentTypeCode = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_APPOINTMENTTYPE));
                            oAppParameters.AppointmentTypeDesc = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_APPOINTMENTTYPE));
                            oAppParameters.ProviderID = Convert.ToInt64(c1Appointments.GetData(c1Appointments.Row, COL_PROVIDERID));
                            oAppParameters.ProviderName = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_PROVIDER));
                            oAppParameters.ProblemTypes = null;
                            oAppParameters.Resources = null;
                            oAppParameters.PatientID = oSmartPatient.PatientID;
                            oAppParameters.AddTrue_ModifyFalse_Flag = true;
                            oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                            oAppParameters.ModifySingleAppointmentFromReccurence = false;
                            oAppParameters.Location = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_LOCATION));
                            oAppParameters.Department = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_DEPATMENT));
                            oAppParameters.StartDate = Convert.ToDateTime(c1Appointments.GetData(c1Appointments.Row, COL_DATE));
                            oAppParameters.StartTime = Convert.ToDateTime(c1Appointments.GetData(c1Appointments.Row, COL_TIME));
                            oAppParameters.Duration = Convert.ToDecimal(c1Appointments.GetData(c1Appointments.Row, COL_DURATION));
                            oAppParameters.ClinicID = _ClinicID;
                            oAppParameters.LineNumber = 0;

                            //Not Applicable
                            oAppParameters.TemplateAllocationMasterID = Convert.ToInt64(c1Appointments.GetData(c1Appointments.Row, COL_TEMPLATEMSTID));
                            oAppParameters.TemplateAllocationID = Convert.ToInt64(c1Appointments.GetData(c1Appointments.Row, COL_TEMPLATEID));
                            //--

                            oAppParameters.LoadParameters = true;

                            #endregion
                            if (oSmartPatient != null)
                            {
                                pnlOther2.Controls.Remove(oSmartPatient);
                                try
                                {
                                    oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                                    oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                                    oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                                    oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                                }
                                catch
                                {
                                }
                            }
                            pnlOther2.Visible = false;

                            oSetupAppointment.SetAppointmentParameters = oAppParameters;
                            oSetupAppointment.ShowDialog(this);
                            SearchAppointment();

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        finally
                        {
                            oSetupAppointment.Dispose();
                            oAppParameters.Dispose();
                        }
                    }
                    #endregion
                }
            }
            else
            {
                if (oSmartPatient != null)
                {
                    pnlOther2.Controls.Remove(oSmartPatient);
                    try
                    {
                        oSmartPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
                        oSmartPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
                        oSmartPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(oSmartPatient_ItemSelectedClick);
                        oSmartPatient.ItemClosedClick -= new gloPatient.PatientListControl.ItemClosed(oSmartPatient_ItemClosedClick);

                    }
                    catch
                    {
                    }
                }
                pnlOther2.Visible = false;
            }
        }

        #endregion

        private bool Validate()
        {
            bool _returnValue = true;
            try
            {
                bool IsProviderSelected = false;
                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    if (trvProvider.Nodes[i].Checked == true)
                    {
                        IsProviderSelected = true;
                        break;
                    }
                }

                //if (cmbProviders1 == null || cmbProviders1.Items.Count <= 0)
                if (IsProviderSelected == false)
                {
                    MessageBox.Show("Please select a provider.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    trvProvider.Focus();
                    _returnValue = false;
                }
                else if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
                {
                    MessageBox.Show("End date should be after start date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpEndDate.Focus();
                    _returnValue = false;
                }
                else if (num_Duration.Value <= 0)
                {
                    MessageBox.Show("Please enter a valid appointment duration.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    num_Duration.Focus();
                    _returnValue = false;
                }
                else if (chkApplyAdvSearch.Checked)
                {
                    if (cmbProcedures == null || cmbProcedures.Items.Count <= 0)
                    {
                        //MessageBox.Show("Please select Problem Type for Advance Search", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //btn_BrowseProcedure.Focus();
                        //_returnValue = false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _returnValue = false;
            }
            finally
            {

            }
            return _returnValue;
        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            //1. FreeSlot / Block
            //2. Location 
            //3. Department 
            string[] temp;
            if (TagContent.Contains(Delimeter.ToString()))
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            else
            {
                return TagContent;
            }
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

                juc_Appointment.WorkStartTime = new TimeSpan(_dtClinicStartTime.Hour, _dtClinicStartTime.Minute, _dtClinicStartTime.Second);
                juc_Appointment.WorkEndTime = new TimeSpan(_dtClinicEndTime.Hour, _dtClinicEndTime.Minute, _dtClinicEndTime.Second);

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
        //To Implement WeekDay Setting 
        private void GetWeekDays()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {
                ogloSettings.GetSetting("Week Days", out value);

                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    if (Convert.ToString(value).Trim() != "")
                    {
                        string[] WeekDays = Convert.ToString(value).Trim().Split(',');
                        for (int i = 0; i < WeekDays.Length; i++)
                        {
                            if (WeekDays[i].Trim() == "0")
                            {
                                _WeekDays.Add(DayOfWeek.Sunday);
                            }
                            if (WeekDays[i].Trim() == "1")
                            {
                                _WeekDays.Add(DayOfWeek.Monday);
                            }
                            if (WeekDays[i].Trim() == "2")
                            {
                                _WeekDays.Add(DayOfWeek.Tuesday);

                            }
                            if (WeekDays[i].Trim() == "3")
                            {
                                _WeekDays.Add(DayOfWeek.Wednesday);

                            }
                            if (WeekDays[i].Trim() == "4")
                            {
                                _WeekDays.Add(DayOfWeek.Thursday);

                            }
                            if (WeekDays[i].Trim() == "5")
                            {
                                _WeekDays.Add(DayOfWeek.Friday);

                            }
                            if (WeekDays[i].Trim() == "6")
                            {
                                _WeekDays.Add(DayOfWeek.Saturday);

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
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        #region "Designer Events"

        //<<<<<<<<Ojeswini>>>>>>>>>>>>>>>
        private void rbProblemTypeFilter_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbProblemTypeFilter.Checked == true)
            {
                rbProblemTypeFilter.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbProblemTypeFilter.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbResourceFilter_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbResourceFilter.Checked == true)
            {
                rbResourceFilter.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbResourceFilter.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbRec_Range_EndBy_CheckedChanged(object sender, EventArgs e)
        {
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (rbRec_Range_EndBy.Checked == true)
            {
                rbRec_Range_EndBy.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRec_Range_EndBy.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void chkLocationDepartmentFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
                //if (chkLocationDepartmentFilter.Checked == true)
                //{
                if (Convert.ToString(appSettings["DefaultLocationID"]).Trim() != "")
                {

                    if (Convert.ToInt64(appSettings["DefaultLocationID"]) > 0)
                    {
                        cmbLocation.SelectedValue = Convert.ToInt64(appSettings["DefaultLocationID"]);
                        cmbLocation.Text = Convert.ToString(appSettings["DefaultLocation"]);
                    }
                }
                //}
                //else if (chkLocationDepartmentFilter.Checked == false)
                //{
                //    cmbLocation.SelectedIndex = -1;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_HideSearchPnl_MouseHover(object sender, EventArgs e)
        {
            btn_HideSearchPnl.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.RewindHover;
            btn_HideSearchPnl.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_HideSearchPnl_MouseLeave(object sender, EventArgs e)
        {
            btn_HideSearchPnl.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Rewind;
            btn_HideSearchPnl.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_ShowSearchPnl_MouseHover(object sender, EventArgs e)
        {
            btn_ShowSearchPnl.Image = global::gloAppointmentScheduling.Properties.Resources.ForwardHover;
            btn_ShowSearchPnl.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void btn_ShowSearchPnl_MouseLeave(object sender, EventArgs e)
        {
            btn_ShowSearchPnl.Image = global::gloAppointmentScheduling.Properties.Resources.Forward;
            btn_ShowSearchPnl.ImageAlign = ContentAlignment.MiddleCenter;

        }

        private void btn_AppSearch_Down_MouseHover(object sender, EventArgs e)
        {
            btn_AppSearch_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.DownHover;
            btn_AppSearch_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AppSearch_Down_MouseLeave(object sender, EventArgs e)
        {
            btn_AppSearch_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Down;
            btn_AppSearch_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AppSearch_Up_MouseHover(object sender, EventArgs e)
        {
            btn_AppSearch_Up.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UPHover;
            btn_AppSearch_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AppSearch_Up_MouseLeave(object sender, EventArgs e)
        {
            btn_AppSearch_Up.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UP;
            btn_AppSearch_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AdvanceSearch_Down_MouseHover(object sender, EventArgs e)
        {
            btn_AdvanceSearch_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.DownHover;
            btn_AdvanceSearch_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AdvanceSearch_Down_MouseLeave(object sender, EventArgs e)
        {
            btn_AdvanceSearch_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Down;
            btn_AdvanceSearch_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AdvanceSearch_UP_MouseHover(object sender, EventArgs e)
        {
            btn_AdvanceSearch_UP.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UPHover;
            btn_AdvanceSearch_UP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_AdvanceSearch_UP_MouseLeave(object sender, EventArgs e)
        {
            btn_AdvanceSearch_UP.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UP;
            btn_AdvanceSearch_UP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Recurrencepattern_UP_MouseHover(object sender, EventArgs e)
        {
            btn_Recurrencepattern_UP.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UPHover;
            btn_Recurrencepattern_UP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Recurrencepattern_UP_MouseLeave(object sender, EventArgs e)
        {
            btn_Recurrencepattern_UP.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.UP;
            btn_Recurrencepattern_UP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Recurrencepattern_Down_MouseHover(object sender, EventArgs e)
        {
            btn_Recurrencepattern_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.DownHover;
            btn_Recurrencepattern_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Recurrencepattern_Down_MouseLeave(object sender, EventArgs e)
        {
            btn_Recurrencepattern_Down.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Down;
            btn_Recurrencepattern_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        //<<<<<<<<Ojeswini>>>>>>>>>>>>>>> 

        #endregion

        private void btnSelectAllProvider_Click(object sender, EventArgs e)
        {
            //if (btnSelectAllProvider.Text.Trim() == "Select All")
            //{
            //    foreach (TreeNode  oNode in trvProvider.Nodes)
            //    {
            //        oNode.Checked = true;  
            //    }
            //    btnSelectAllProvider.Text = "Clear All";
            //}
            //else if (btnSelectAllProvider.Text.Trim() == "Clear All")
            //{
            //    foreach (TreeNode oNode in trvProvider.Nodes)
            //    {
            //        oNode.Checked = false;
            //    }
            //    btnSelectAllProvider.Text = "Select All";

            //}

            //this.trvProvider.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (trvProvider.Nodes.Count > 0)
            {
                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    trvProvider.Nodes[i].Checked = true;
                }
            }
            btnClearAllProvider.Visible = true;
            btnSelectAllProvider.Visible = false;
        }


        private void btnSelectAllAppTypes_Click(object sender, EventArgs e)
        {
            //if (btnSelectAllAppTypes.Text.Trim() == "Select All")
            //{
            //    foreach (TreeNode oNode in trvAppointmentType.Nodes)
            //    {
            //        oNode.Checked = true;
            //    }
            //    btnSelectAllAppTypes.Text = "Clear All";
            //}
            //else if (btnSelectAllAppTypes.Text.Trim() == "Clear All")
            //{
            //    foreach (TreeNode oNode in trvAppointmentType.Nodes)
            //    {
            //        oNode.Checked = false;
            //    }
            //    btnSelectAllAppTypes.Text = "Select All";
            //}
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
            if (trvAppointmentType.Nodes.Count > 0)
            {
                for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
                {
                    trvAppointmentType.Nodes[i].Checked = true;
                }
            }

            btnClearAllAppTypes.Visible = true;
            btnSelectAllAppTypes.Visible = false;

        }

        private void btnClearAllAppTypes_Click(object sender, EventArgs e)
        {
            if (trvAppointmentType.Nodes.Count > 0)
            {
                //chkSearchInTemplate.CheckedChanged -= chkSearchInTemplate_CheckedChanged;

                for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
                {
                    trvAppointmentType.Nodes[i].Checked = false;
                }

                //chkSearchInTemplate.CheckedChanged += chkSearchInTemplate_CheckedChanged;

                num_Duration.Visible = true ;
                lblSimple_Duration.Visible = true;
            }

            btnClearAllAppTypes.Visible = false;
            btnSelectAllAppTypes.Visible = true;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }



        private void juc_Appointment_AppointmentChanged(object sender, AppointmentChangeEventArgs e)
        {

        }


        #region "Display Appointments in List view"

        private void cmbListViewProvider_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FillAppointmentListView();
                juc_Appointment.DayColumns = GetNumberofColumninDayView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        public class SearchProvider : IDisposable
        {
            #region "Constructor & Distructor"

            public SearchProvider()
            {
                _FreeSlotes = new FreeSlots();
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

            ~SearchProvider()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Private variables"

            private Int64 _nProviderID = 0;
            private string _nProviderName = "";
            private FreeSlots _FreeSlotes = null;

            #endregion "Private variables"

            #region "Property Procedures"

            public Int64 ProviderID
            {
                get { return _nProviderID; }
                set { _nProviderID = value; }
            }

            public string ProviderName
            {
                get { return _nProviderName; }
                set { _nProviderName = value; }
            }

            public FreeSlots ProviderFreeSlots
            {
                get { return _FreeSlotes; }
                set { _FreeSlotes = value; }
            }

            #endregion "Property Procedures"
        }

        public class SearchProviders : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public SearchProviders()
            {
                _innerlist = new ArrayList();
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


            ~SearchProviders()
            {
                Dispose(false);
            }
            #endregion


            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(SearchProvider item)
            {
                _innerlist.Add(item);
            }



            public bool Remove(SearchProvider item)
            //Remark - Work Remining for comparision
            {
                bool result = false;
                //DBParameter obj;

                //for (int i = 0; i < _innerlist.Count; i++)
                //{
                //    //store current index being checked
                //    obj = new DBParameter();
                //    obj = (DBParameter)_innerlist[i];
                //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
                //    {
                //        _innerlist.RemoveAt(i);
                //        result = true;
                //        break;
                //    }
                //    obj = null;
                //}

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public SearchProvider this[int index]
            {
                get
                { return (SearchProvider)_innerlist[index]; }
            }

            public bool Contains(SearchProvider item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(SearchProvider item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(SearchProvider[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class FreeSlot : IDisposable
        {
            #region "Constructor & Distructor"

            public FreeSlot()
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

            ~FreeSlot()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Private variables"

            private DateTime _dtDate;
            private DateTime _dtStartTime;
            private DateTime _dtEndTime;
            private string _AppointmentType = "";
            private Int64 _AppointmentTypeID = 0;
            private Int64 _TemplateAllocationID = 0;
            private Int64 _TemplateAllocationMSTID = 0;
            private string _Location = "";
            private string _Department = "";

            #endregion "Private variables"

            #region "Property Procedures"

            public DateTime Date
            {
                get { return _dtDate; }
                set { _dtDate = value; }
            }

            public DateTime StartTime
            {
                get { return _dtStartTime; }
                set { _dtStartTime = value; }
            }

            public DateTime EndTime
            {
                get { return _dtEndTime; }
                set { _dtEndTime = value; }
            }

            public string AppointmentType
            {
                get { return _AppointmentType; }
                set { _AppointmentType = value; }
            }

            public Int64 AppointmentTypeID
            {
                get { return _AppointmentTypeID; }
                set { _AppointmentTypeID = value; }
            }

            public Int64 TemplateAllocationID
            {
                get { return _TemplateAllocationID; }
                set { _TemplateAllocationID = value; }
            }

            public Int64 TemplateAllocationMasterID
            {
                get { return _TemplateAllocationMSTID; }
                set { _TemplateAllocationMSTID = value; }
            }

            public string Location
            {
                get { return _Location; }
                set { _Location = value; }
            }

            public string Department
            {
                get { return _Department; }
                set { _Department = value; }
            }

            #endregion "Property Procedures"
        }

        public class FreeSlots : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public FreeSlots()
            {
                _innerlist = new ArrayList();
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


            ~FreeSlots()
            {
                Dispose(false);
            }
            #endregion


            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(FreeSlot item)
            {
                _innerlist.Add(item);
            }



            public bool Remove(FreeSlot item)
            //Remark - Work Remining for comparision
            {
                bool result = false;
                //DBParameter obj;

                //for (int i = 0; i < _innerlist.Count; i++)
                //{
                //    //store current index being checked
                //    obj = new DBParameter();
                //    obj = (DBParameter)_innerlist[i];
                //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
                //    {
                //        _innerlist.RemoveAt(i);
                //        result = true;
                //        break;
                //    }
                //    obj = null;
                //}

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public FreeSlot this[int index]
            {
                get
                { return (FreeSlot)_innerlist[index]; }
            }

            public bool Contains(FreeSlot item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(FreeSlot item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(FreeSlot[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #endregion

        private void btnClearAllProvider_Click(object sender, EventArgs e)
        {
            if (trvProvider.Nodes.Count > 0)
            {
                for (int i = 0; i < trvProvider.Nodes.Count; i++)
                {
                    trvProvider.Nodes[i].Checked = false;
                }
            }
            btnClearAllProvider.Visible = false;
            btnSelectAllProvider.Visible = true;
            juc_Appointment.DayColumns = GetNumberofColumninDayView();
        }








        //private void btnClearAllAppTypes_Click(object sender, EventArgs e)
        //{
        //    //if (trvAppointmentType.Nodes.Count > 0)
        //    //{
        //    //    for (int i = 0; i < trvAppointmentType.Nodes.Count; i++)
        //    //       
        //    //        trvAppointmentType.Nodes[i].Checked = false;
        //    //    }
        //    //}
        //    //btnSelectAllAppTypes.Visible = true;
        //    //btnClearAllAppTypes.Visible = false;

        //}



    }
}
