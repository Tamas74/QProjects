using System;
using System.Data;
using System.Windows.Forms;
using gloCommon;

namespace gloAppointmentScheduling
{
    public partial class frmRpt_AppointmentStatus : Form
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        gloListControl.gloListControl oListControl = null;

        private Int64 _clinicId = 0;
        private String _SelectedFilterCombo = "";

        String _databaseconnectionstring = "";
        string _messageBoxCaption = String.Empty;
        DataTable oBindTable = new DataTable();
        
        private const int COL_APPOINTMENTID = 0;
        private const int COL_DATE = 1;
        private const int COL_TIME = 2;
        private const int COL_TYPE = 3;
        private const int COL_PATIENT = 4;
        private const int COL_PROVIDER = 5;
        private const int COL_LOCATION = 6;
        private const int COL_STATUS = 7;
        private const int COL_PATIENTID = 8;
        private const int COL_DTLAPPOINTMENTID = 9;
        private const int COL_nDATE = 10;
        private const int COL_nTIME = 11;
        private const int COL_SentDateTime = 12;
        private const int COL_LastSentDateTime = 13;
        private const int COL_StatusRcvDateTime = 14;
        private const int COL_COLCOUNT = 15;

        private const int COL_sReminderType = 0;
        private const int COL_sReminderStatus = 1;
        private const int COL_ReminderStatusRecievedDATE = 2;
        private const int COL_ReminderId = 3;


        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");
                
        public frmRpt_AppointmentStatus(String DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }

            #endregion

            InitializeComponent();

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
        ToolTip oToolTip = null;
        private void frmRpt_AppointmentStatus_Load(object sender, EventArgs e)
        {
            try
            {

                //Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                //Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                //// This method actually sets the order all the way down the control hierarchy.
                //tom.SetTabOrder(scheme);

                gloC1FlexStyle.Style(c1ApptLetterPatients, false);

                cmbApptLetterDateRange.Focus();

                Fill_FilterDatesCombo();
                Fill_StatusCombo();
                SetTiming();
                DesignGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region "Browse & Clear Combobox Event"

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;

            gloListControl.gloListControlType _ControlType = gloListControl.gloListControlType.Other;
            string _ControlHeader = "";
            gloGeneralItem.gloItems oSelectedItems = new gloGeneralItem.gloItems();
            bool _isMultiSelect = false;


            try
            {
                switch (btn.Name)
                {
                    case "btnApptLetterBrowseLocation":
                        _SelectedFilterCombo = "btnApptLetterBrowseLocation";

                        _ControlType = gloListControl.gloListControlType.Location;
                        _ControlHeader = "Location";
                        _isMultiSelect = true;

                        if (cmbApptLetterLocation.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterLocation.Items.Count; i++)
                            {
                                cmbApptLetterLocation.SelectedIndex = i;
                                cmbApptLetterLocation.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterLocation.SelectedValue), cmbApptLetterLocation.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseProvider":
                        _SelectedFilterCombo = "btnApptLetterBrowseProvider";
                        _ControlType = gloListControl.gloListControlType.Providers;
                        _ControlHeader = "Provider";
                        _isMultiSelect = true;

                        if (cmbApptLetterProvider.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterProvider.Items.Count; i++)
                            {
                                cmbApptLetterProvider.SelectedIndex = i;
                                cmbApptLetterProvider.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterProvider.SelectedValue), cmbApptLetterProvider.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseResource":
                        _SelectedFilterCombo = "btnApptLetterBrowseResource";
                        _ControlType = gloListControl.gloListControlType.Resources;
                        _ControlHeader = "Resource";
                        _isMultiSelect = true;

                        if (cmbApptLetterResource.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterResource.Items.Count; i++)
                            {
                                cmbApptLetterResource.SelectedIndex = i;
                                cmbApptLetterResource.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterResource.SelectedValue), cmbApptLetterResource.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseApptType":
                        _SelectedFilterCombo = "btnApptLetterBrowseApptType";
                        _ControlType = gloListControl.gloListControlType.AppointmentType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbApptLetterApptType.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterApptType.Items.Count; i++)
                            {
                                cmbApptLetterApptType.SelectedIndex = i;
                                cmbApptLetterApptType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterApptType.SelectedValue), cmbApptLetterApptType.Text);
                            }
                        }
                        break;
                    case "btnApptLetterBrowseApptTypeType":
                        _SelectedFilterCombo = "btnApptLetterBrowseApptTypeType";
                        _ControlType = gloListControl.gloListControlType.AppointmentTypeType;
                        _ControlHeader = "Appointment Type";
                        _isMultiSelect = true;

                        if (cmbApptLetterApptTypeType.DataSource != null)
                        {
                            for (int i = 0; i < cmbApptLetterApptTypeType.Items.Count; i++)
                            {
                                cmbApptLetterApptTypeType.SelectedIndex = i;
                                cmbApptLetterApptTypeType.Refresh();
                                oSelectedItems.Add(Convert.ToInt64(cmbApptLetterApptTypeType.SelectedValue), cmbApptLetterApptTypeType.Text);
                            }
                        }
                        break;
                    default:
                        _SelectedFilterCombo = "";
                        _ControlType = gloListControl.gloListControlType.Other;
                        _ControlHeader = "";
                        break;
                }

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
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, _ControlType, _isMultiSelect, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = _ControlHeader;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.SelectedItems = oSelectedItems;

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _SelectedFilterCombo = "";
            }

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                switch (btn.Name)
                {
                    case "btnApptLetterClearLocation":

                        cmbApptLetterLocation.DataSource = null;
                        cmbApptLetterLocation.Items.Clear();
                        cmbApptLetterLocation.Refresh();

                        break;
                    case "btnApptLetterClearProvider":

                        cmbApptLetterProvider.DataSource = null;
                        cmbApptLetterProvider.Items.Clear();
                        cmbApptLetterProvider.Refresh();

                        break;
                    case "btnApptLetterClearResource":

                        cmbApptLetterResource.DataSource = null;
                        cmbApptLetterResource.Items.Clear();
                        cmbApptLetterResource.Refresh();

                        break;
                    case "btnApptLetterClearApptType":

                        cmbApptLetterApptType.DataSource = null;
                        cmbApptLetterApptType.Items.Clear();
                        cmbApptLetterApptType.Refresh();

                        break;
                    case "btnApptLetterClearApptTypeType":

                        cmbApptLetterApptTypeType.DataSource = null;
                        cmbApptLetterApptTypeType.Items.Clear();
                        cmbApptLetterApptTypeType.Refresh();

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

        #region "List Control Events "

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            try
            {
                switch (_SelectedFilterCombo)
                {
                    case "btnApptLetterBrowseLocation":

                        cmbApptLetterLocation.DataSource = null;
                        cmbApptLetterLocation.Items.Clear();
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
                            cmbApptLetterLocation.DataSource = oBindTable;
                            cmbApptLetterLocation.DisplayMember = "DispName";
                            cmbApptLetterLocation.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseProvider":

                        cmbApptLetterProvider.DataSource = null;
                        cmbApptLetterProvider.Items.Clear();
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
                            cmbApptLetterProvider.DataSource = oBindTable;
                            cmbApptLetterProvider.DisplayMember = "DispName";
                            cmbApptLetterProvider.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseResource":

                        cmbApptLetterResource.DataSource = null;
                        cmbApptLetterResource.Items.Clear();
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
                            cmbApptLetterResource.DataSource = oBindTable;
                            cmbApptLetterResource.DisplayMember = "DispName";
                            cmbApptLetterResource.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseApptType":

                        cmbApptLetterApptType.DataSource = null;
                        cmbApptLetterApptType.Items.Clear();
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
                            cmbApptLetterApptType.DataSource = oBindTable;
                            cmbApptLetterApptType.DisplayMember = "DispName";
                            cmbApptLetterApptType.ValueMember = "ID";
                        }
                        break;
                    case "btnApptLetterBrowseApptTypeType":

                        cmbApptLetterApptTypeType.DataSource = null;
                        cmbApptLetterApptTypeType.Items.Clear();
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
                            cmbApptLetterApptTypeType.DataSource = oBindTable;
                            cmbApptLetterApptTypeType.DisplayMember = "DispName";
                            cmbApptLetterApptTypeType.ValueMember = "ID";
                        }
                        break;
                    default:
                        _SelectedFilterCombo = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                _SelectedFilterCombo = "";
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            pnl_tlspTOP.Visible = true;
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                _SelectedFilterCombo = "";
            }
        }

        #endregion

        #region "Methods"

        private void FilterBy_Today()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Today;
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_Tomorrow()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.AddDays(1);
                dtpApptLetterToDate.Value = DateTime.Now.AddDays(1);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_Yesterday()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_Thisweek()
        {
            try
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Today;
                    dtpApptLetterToDate.Value = DateTime.Now.Date.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_lastweek()
        {
            try
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);

                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                    dtpApptLetterToDate.Value = dtpApptLetterFromDate.Value.AddDays(6);
                }

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_currentmonth()
        {
            try
            {
                DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // for any date passed in to the method
                // create a datetime variable set to the passed in date
                DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // overshoot the date by a month

                dtTo = dtTo.AddMonths(1);
                // remove all of the days in the next month
                // to get bumped down to the last day of the 
                // previous month
                dtTo = dtTo.AddDays(-(dtTo.Day));
                dtpApptLetterFromDate.Value = Convert.ToDateTime(dtFrom.Date);
                dtpApptLetterToDate.Value = Convert.ToDateTime(dtTo.Date);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_lastmonth()
        {
            try
            {
                DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

                int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

                DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

                dtpApptLetterFromDate.Value = Convert.ToDateTime(firstDay.Date);
                dtpApptLetterToDate.Value = Convert.ToDateTime(lastDay.Date);

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_currenYear()
        {
            try
            {
                DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

                dtpApptLetterFromDate.Value = Convert.ToDateTime(dtFrom.Date);
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last30days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last60days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void FilterBy_last90days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_last120days()
        {
            try
            {
                dtpApptLetterFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
                dtpApptLetterToDate.Value = DateTime.Today;

                dtpApptLetterFromDate.Enabled = false;
                dtpApptLetterToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FilterBy_DateRange()
        {
            try
            {
                dtpApptLetterFromDate.Enabled = true;
                dtpApptLetterToDate.Enabled = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void SetTiming()
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

                dtpApptLetterFromDate.Value = DateTime.Today;
                dtpApptLetterToDate.Value = DateTime.Today;

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


        private void Fill_StatusCombo()
        {
            try
            {
                cmbApptStatus.Items.Clear();
                cmbApptStatus.Items.Add("All");
                cmbApptStatus.Items.Add("Confirmed");
                cmbApptStatus.Items.Add("Not Confirmed");
                cmbApptStatus.Items.Add("Cancelled");
                cmbApptStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }

        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmbApptLetterDateRange.Items.Clear();
                cmbApptLetterDateRange.Items.Add("Custom");
                cmbApptLetterDateRange.Items.Add("Today");
                cmbApptLetterDateRange.Items.Add("Tomorrow");
                cmbApptLetterDateRange.Items.Add("Yesterday");
                cmbApptLetterDateRange.Items.Add("This Week");
                cmbApptLetterDateRange.Items.Add("Last Week");
                cmbApptLetterDateRange.Items.Add("Current Month");
                cmbApptLetterDateRange.Items.Add("Last Month");
                cmbApptLetterDateRange.Items.Add("Current Year");
                cmbApptLetterDateRange.Items.Add("Last 30 Days");
                cmbApptLetterDateRange.Items.Add("Last 60 Days");
                cmbApptLetterDateRange.Items.Add("Last 90 Days");
                cmbApptLetterDateRange.Items.Add("Last 120 Days");
                cmbApptLetterDateRange.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }
        
        private DataTable getBatchAppointment()
        {
            DataTable dtBatchAppt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                String sLocations = string.Empty;
                String sProviders = string.Empty;
                String sResources = string.Empty;
                String sAppointmentType = string.Empty;
                String sAppointmentTypeType = string.Empty;
                Int32 ndtFromDate = 0;
                Int32 ndtToDate = 0;
                String sInsuranceType = string.Empty;
                String sAppointmentStatus = string.Empty;

                oDB.Connect(false);
                
                    if (cmbApptLetterLocation.Items.Count > 0)
                    {

                        for (int cntrApptLetterLocation = 0; cntrApptLetterLocation <= cmbApptLetterLocation.Items.Count - 1; cntrApptLetterLocation++)
                        {
                            if (sLocations == string.Empty)
                                sLocations = (Convert.ToString(((DataRowView)cmbApptLetterLocation.Items[cntrApptLetterLocation])["ID"]));
                            else
                                sLocations = sLocations + "," + (Convert.ToString(((DataRowView)cmbApptLetterLocation.Items[cntrApptLetterLocation])["ID"]));
                        }
                    }

                    if (cmbApptLetterProvider.Items.Count > 0)
                    {

                        for (int cntrApptLetterProvider = 0; cntrApptLetterProvider <= cmbApptLetterProvider.Items.Count - 1; cntrApptLetterProvider++)
                        {
                            if (sProviders == string.Empty)
                                sProviders = (Convert.ToString(((DataRowView)cmbApptLetterProvider.Items[cntrApptLetterProvider])["ID"]));
                            else
                                sProviders = sProviders + "," + (Convert.ToString(((DataRowView)cmbApptLetterProvider.Items[cntrApptLetterProvider])["ID"]));
                        }
                    }

                    if (cmbApptLetterResource.Items.Count > 0)
                    {

                        for (int cntrApptLetterReosurce = 0; cntrApptLetterReosurce <= cmbApptLetterResource.Items.Count - 1; cntrApptLetterReosurce++)
                        {
                            if (sResources == string.Empty)
                                sResources = (Convert.ToString(((DataRowView)cmbApptLetterResource.Items[cntrApptLetterReosurce])["ID"]));
                            else
                                sResources = sResources + "," + (Convert.ToString(((DataRowView)cmbApptLetterResource.Items[cntrApptLetterReosurce])["ID"]));
                        }
                    }

                    if (cmbApptLetterApptType.Items.Count > 0)
                    {

                        for (int cntrApptLetterApptType = 0; cntrApptLetterApptType <= cmbApptLetterApptType.Items.Count - 1; cntrApptLetterApptType++)
                        {
                            if (sAppointmentType == string.Empty)
                                sAppointmentType = (Convert.ToString(((DataRowView)cmbApptLetterApptType.Items[cntrApptLetterApptType])["ID"]));
                            else
                                sAppointmentType = sAppointmentType + "," + (Convert.ToString(((DataRowView)cmbApptLetterApptType.Items[cntrApptLetterApptType])["ID"]));
                        }
                    }

                    if (cmbApptLetterApptTypeType.Items.Count > 0)
                    {

                        for (int cntrApptLetterApptTypeType = 0; cntrApptLetterApptTypeType <= cmbApptLetterApptTypeType.Items.Count - 1; cntrApptLetterApptTypeType++)
                        {
                            if (sAppointmentTypeType == string.Empty)
                                sAppointmentTypeType = (Convert.ToString(((DataRowView)cmbApptLetterApptTypeType.Items[cntrApptLetterApptTypeType])["ID"]));
                            else
                                sAppointmentTypeType = sAppointmentTypeType + "," + (Convert.ToString(((DataRowView)cmbApptLetterApptTypeType.Items[cntrApptLetterApptTypeType])["ID"]));
                        }
                    }

                    ndtFromDate = gloDateMaster.gloDate.DateAsNumber(dtpApptLetterFromDate.Value.ToString("MM/dd/yyyy"));
                    ndtToDate = gloDateMaster.gloDate.DateAsNumber(dtpApptLetterToDate.Value.ToString("MM/dd/yyyy"));


                    if (cmbApptStatus.SelectedItem.ToString().Trim() == "Confirmed")
                    {
                        sAppointmentStatus = "Scheduled";
                    }
                    else if (cmbApptStatus.SelectedItem.ToString().Trim() == "Not Confirmed")
                    {
                        sAppointmentStatus = "pthold";
                    }
                    else
                    {
                        sAppointmentStatus = cmbApptStatus.SelectedItem.ToString();
                    }

                    //if (rbtnApptAll.Checked)
                    //{
                    //    sAppointmentStatus = "All";
                    //}
                    //else if (rbtnApptCancelled.Checked)
                    //{
                    //    sAppointmentStatus = "Cancelled";
                    //}
                    //else if (rbtnApptConfirmed.Checked)
                    //{
                    //    sAppointmentStatus = "Scheduled";
                    //}

                oDBParameters.Add("@FromDate", ndtFromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); // NUMERIC= NULL,   
                oDBParameters.Add("@ToDate", ndtToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int); //  NUMERIC= NULL,  

                if (sProviders != string.Empty)
                    oDBParameters.Add("@Providers", sProviders, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,      
                else
                    oDBParameters.Add("@Providers", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                if (sLocations != string.Empty)
                    oDBParameters.Add("@Locations", sLocations, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 
                else
                    oDBParameters.Add("@Locations", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL, 

                if (sResources != string.Empty)
                    oDBParameters.Add("@Resources", sResources, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    
                else
                    oDBParameters.Add("@Resources", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,    

                if (sAppointmentType != string.Empty)
                    oDBParameters.Add("@AppointmentTypes", sAppointmentType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   
                else
                    oDBParameters.Add("@AppointmentTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);//  VARCHAR(MAX)=NULL,   

                if (sAppointmentTypeType != string.Empty)
                    oDBParameters.Add("@AppointmentTypeTypes", sAppointmentTypeType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,
                else
                    oDBParameters.Add("@AppointmentTypeTypes", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,

                oDBParameters.Add("@AppointmentStatus", sAppointmentStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar); //  VARCHAR(MAX)=NULL,  

                oDB.Retrive("gspRpt_AppointmentStatus", oDBParameters, out dtBatchAppt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return dtBatchAppt;
        }

        private void generateBatch()
        {
            try
            {
                DataTable oBindTable = new DataTable();
                oBindTable = getBatchAppointment();
                
                   // c1ApptLetterPatients.DataSource = oBindTable;

                    c1ApptLetterPatients.DataSource = oBindTable;
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Caption = "Appointment ID";
                    c1ApptLetterPatients.Cols[COL_DATE].Caption = "Date";
                    c1ApptLetterPatients.Cols[COL_TIME].Caption = "Time";
                    c1ApptLetterPatients.Cols[COL_TYPE].Caption = "Type";
                    c1ApptLetterPatients.Cols[COL_PATIENT].Caption = "Patient";
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Caption = "Provider";
                    c1ApptLetterPatients.Cols[COL_LOCATION].Caption = "Location";
                    c1ApptLetterPatients.Cols[COL_STATUS].Caption = "Status";
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
                    c1ApptLetterPatients.Cols[COL_nDATE].Caption = "nDate";
                    c1ApptLetterPatients.Cols[COL_nTIME].Caption = "nTime";
                    c1ApptLetterPatients.Cols[COL_SentDateTime].Caption = "Appt Sent Datetime";
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].Caption = "Last Appt Sent Datetime";
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Caption = "Status Received Datetime";


                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_DATE].Visible = true;
                    c1ApptLetterPatients.Cols[COL_TIME].Visible = true;
                    c1ApptLetterPatients.Cols[COL_TYPE].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PATIENT].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Visible = true;
                    c1ApptLetterPatients.Cols[COL_LOCATION].Visible = true;
                    c1ApptLetterPatients.Cols[COL_STATUS].Visible = true;
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
                    c1ApptLetterPatients.Cols[COL_nDATE].Visible = false;
                    c1ApptLetterPatients.Cols[COL_nTIME].Visible = false;
                    c1ApptLetterPatients.Cols[COL_SentDateTime].Visible = false;
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].Visible = false;
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Visible = false;

                    c1ApptLetterPatients.Cols[COL_DATE].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_DATE].Format = "MM/dd/yyyy";

                    c1ApptLetterPatients.Cols[COL_SentDateTime].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_SentDateTime].Format = "MM/dd/yyyy hh:mm tt";
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].Format = "MM/dd/yyyy hh:mm tt";
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Format = "MM/dd/yyyy hh:mm tt";

                    c1ApptLetterPatients.Cols[COL_TIME].DataType = typeof(System.DateTime);
                    c1ApptLetterPatients.Cols[COL_TIME].Format = "hh:mm tt";
                    int width = pnlApptLetterGrid.Width / 9;
                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_DATE].Width = (int)(width * 0.6);
                    c1ApptLetterPatients.Cols[COL_TIME].Width = (int)(width * 0.5);
                    c1ApptLetterPatients.Cols[COL_TYPE].Width = (int)(width * 0.9);
                    c1ApptLetterPatients.Cols[COL_PATIENT].Width = (int)(width * 1.9); 
                    c1ApptLetterPatients.Cols[COL_PROVIDER].Width = width ;
                    c1ApptLetterPatients.Cols[COL_LOCATION].Width = (int)(width * 0.9);
                    c1ApptLetterPatients.Cols[COL_STATUS].Width = (int)(width * 0.7);
                    c1ApptLetterPatients.Cols[COL_PATIENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
                    c1ApptLetterPatients.Cols[COL_nDATE].Width = 0;
                    c1ApptLetterPatients.Cols[COL_nTIME].Width = 0;
                    c1ApptLetterPatients.Cols[COL_SentDateTime].Width =0;
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].Width = 0;
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Width = 0;

                    c1ApptLetterPatients.Cols[COL_APPOINTMENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_DATE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_TIME].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_TYPE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PATIENT].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PROVIDER].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_LOCATION].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_STATUS].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_PATIENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_nDATE].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_nTIME].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_SentDateTime].AllowEditing = false;
                    c1ApptLetterPatients.Cols[COL_LastSentDateTime].AllowEditing=false;
                    c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].AllowEditing=false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void DesignGrid()
        {
            //DESIGN CHECK IN APPOINTMENT LETTER GRID
            c1ApptLetterPatients.DataSource = null;
            c1ApptLetterPatients.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
            c1ApptLetterPatients.Cols.Count = COL_COLCOUNT;
            c1ApptLetterPatients.Rows.Count = 1;
            c1ApptLetterPatients.Cols.Fixed = 0;

            c1ApptLetterPatients.SetData(0, COL_APPOINTMENTID, "Appointment ID");
            c1ApptLetterPatients.SetData(0, COL_DATE, "Date");
            c1ApptLetterPatients.SetData(0, COL_TIME, "Time");
            c1ApptLetterPatients.SetData(0, COL_TYPE, "Type");
            c1ApptLetterPatients.SetData(0, COL_PATIENT, "Patient");
            c1ApptLetterPatients.SetData(0, COL_PROVIDER, "Provider");
            c1ApptLetterPatients.SetData(0, COL_LOCATION, "Location");
            c1ApptLetterPatients.SetData(0, COL_STATUS, "Status");
            c1ApptLetterPatients.Cols[COL_PATIENTID].Caption = "Patient ID";
            c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Caption = "Appointment Detail ID";
            c1ApptLetterPatients.Cols[COL_nDATE].Caption = "nDate";
            c1ApptLetterPatients.Cols[COL_nTIME].Caption = "nTime";
            c1ApptLetterPatients.Cols[COL_SentDateTime].Caption = "Appt Sent Datetime";
            c1ApptLetterPatients.Cols[COL_LastSentDateTime].Caption = "Last Appt Sent Datetime";
            c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Caption = "Status Received Datetime";

            c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Visible = false;
            c1ApptLetterPatients.Cols[COL_DATE].Visible = true;
            c1ApptLetterPatients.Cols[COL_TIME].Visible = true;
            c1ApptLetterPatients.Cols[COL_TYPE].Visible = true;
            c1ApptLetterPatients.Cols[COL_PATIENT].Visible = true;
            c1ApptLetterPatients.Cols[COL_PROVIDER].Visible = true;
            c1ApptLetterPatients.Cols[COL_LOCATION].Visible = true;
            c1ApptLetterPatients.Cols[COL_STATUS].Visible = true;
            c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Visible = false;
            c1ApptLetterPatients.Cols[COL_PATIENTID].Visible = false;
            c1ApptLetterPatients.Cols[COL_nDATE].Visible = false;
            c1ApptLetterPatients.Cols[COL_nTIME].Visible = false;
            c1ApptLetterPatients.Cols[COL_SentDateTime].Visible = false;
            c1ApptLetterPatients.Cols[COL_LastSentDateTime].Visible = false;
            c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Visible = false ;

            c1ApptLetterPatients.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

            int width = pnlApptLetterGrid.Width / 9;
            c1ApptLetterPatients.Cols[COL_APPOINTMENTID].Width = 0;
            c1ApptLetterPatients.Cols[COL_DATE].Width = (int)(width * 0.5);
            c1ApptLetterPatients.Cols[COL_TIME].Width = (int)(width * 0.5);
            c1ApptLetterPatients.Cols[COL_TYPE].Width = (int)(width * 0.9);
            c1ApptLetterPatients.Cols[COL_PATIENT].Width = (int)(width * 1.5);
            c1ApptLetterPatients.Cols[COL_PROVIDER].Width = width;
            c1ApptLetterPatients.Cols[COL_LOCATION].Width = (int)(width * 0.9);
            c1ApptLetterPatients.Cols[COL_STATUS].Width = (int)(width * 0.7);
            c1ApptLetterPatients.Cols[COL_PATIENTID].Width = 0;
            c1ApptLetterPatients.Cols[COL_DTLAPPOINTMENTID].Width = 0;
            c1ApptLetterPatients.Cols[COL_nDATE].Width = 0;
            c1ApptLetterPatients.Cols[COL_nTIME].Width = 0;
            c1ApptLetterPatients.Cols[COL_SentDateTime].Width = 0;
            c1ApptLetterPatients.Cols[COL_LastSentDateTime].Width = 0;
            c1ApptLetterPatients.Cols[COL_StatusRcvDateTime].Width = 0;
        }

        #endregion

        private void cmbApptLetterDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;
            try
            {
                _filterby = cmbApptLetterDateRange.SelectedIndex;
                switch (_filterby)
                {
                    case 0://Date Range
                        FilterBy_DateRange();
                        break;

                    case 1://Today
                        FilterBy_Today();
                        break;

                    case 2://Tomorrow
                        FilterBy_Tomorrow();
                        break;

                    case 3://Yesterday
                        FilterBy_Yesterday();
                        break;

                    case 4://This week
                        FilterBy_Thisweek();
                        break;

                    case 5://Last Week
                        FilterBy_lastweek();
                        break;

                    case 6://Current Month
                        FilterBy_currentmonth();
                        break;

                    case 7://Last Month
                        FilterBy_lastmonth();
                        break;

                    case 8://Current Year
                        FilterBy_currenYear();
                        break;

                    case 9://Last 30 days
                        FilterBy_last30days();
                        break;

                    case 10://Last 60 days
                        FilterBy_last60days();
                        break;

                    case 11://Last 90 days
                        FilterBy_last90days();
                        break;

                    case 12://Last 120 days
                        FilterBy_last120days();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_GenerateBatch_Click(object sender, EventArgs e)
        {
           generateBatch(); 
        }        

        private void c1ApptLetterPatients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1ChkInPatients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        
        private void frmRpt_AppointmentStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (oListControl != null) { oListControl.Dispose(); }
            if (oBindTable != null) { oBindTable = null; }
            if (appSettings != null) { appSettings = null; }
        }

        private void c1ApptLetterPatients_Click(object sender, EventArgs e)
        {
            
        }

        private void c1ApptLetterPatients_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            SetRemiderStatus();
        }
        private void SetRemiderStatus()
        {
            try
            {
                Int64 nappointmentID = 0;
                if (c1ApptLetterPatients.Row >= 1)
                {
                    nappointmentID = Convert.ToInt64(c1ApptLetterPatients[c1ApptLetterPatients.Row, COL_DTLAPPOINTMENTID]);
                }
                    GenerateRemiderStatus(nappointmentID);
            }

            catch (Exception) { }
        }
        private DataTable getBatchAppointmentReminder(long nappointmentID)
        {
            DataTable dtBatchAppt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nAppointmentID", nappointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Retrive("gspRpt_AppointmentReminderStatus", oDBParameters, out dtBatchAppt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return dtBatchAppt;
        }
        private void GenerateRemiderStatus(Int64 nappointmentID)
        {
            try
            {

                DataTable oBindTable = new DataTable(); 
                //oBindTable = new DataTable();
                if (nappointmentID > 0)
                {
                    oBindTable = getBatchAppointmentReminder(nappointmentID);
                    c1ApptReminderStatus.DataSource = oBindTable;
                }
                else
                {
                    c1ApptReminderStatus.DataSource = null;
                    c1ApptReminderStatus.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                    c1ApptReminderStatus.Cols.Count = 4;
                    c1ApptReminderStatus.Rows.Count = 1;
                    c1ApptReminderStatus.Cols.Fixed = 0;
                }
                c1ApptReminderStatus.Cols[COL_sReminderType].Caption = "Type";
                c1ApptReminderStatus.Cols[COL_sReminderStatus].Caption = "Reminder Status";
                c1ApptReminderStatus.Cols[COL_ReminderStatusRecievedDATE].Caption = "Status Received Datetime";

                c1ApptReminderStatus.Cols[COL_sReminderType].Visible = true;
                c1ApptReminderStatus.Cols[COL_sReminderStatus].Visible = true;
                c1ApptReminderStatus.Cols[COL_ReminderStatusRecievedDATE].Visible = true;
                c1ApptReminderStatus.Cols[COL_ReminderId].Visible = false;

                  

                int width = pnlAppointmentReminderStatus.Width / 4;
                c1ApptReminderStatus.Cols[COL_sReminderType].Width = (int)(width * 0.9);
                c1ApptReminderStatus.Cols[COL_sReminderStatus].Width = (int)(width * 0.9);
                c1ApptReminderStatus.Cols[COL_ReminderStatusRecievedDATE].Width = (int)(width * 1.5);
                c1ApptReminderStatus.Cols[COL_ReminderId].Width = 0;

                c1ApptReminderStatus.Cols[COL_sReminderType].AllowEditing = false;
                c1ApptReminderStatus.Cols[COL_sReminderStatus].AllowEditing = false;
                c1ApptReminderStatus.Cols[COL_ReminderStatusRecievedDATE].AllowEditing = false;
                c1ApptReminderStatus.Cols[COL_ReminderId].AllowEditing = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }      
    }
}
