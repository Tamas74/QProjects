using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook.Books;
using gloAuditTrail;

namespace gloAppointmentScheduling
{
    public partial class frmSetupSchedule : Form
    {
        #region " Variale Declration"
        private string _databaseConnectionString = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _CurrentListControlType = "";
        private Int64 _nScheduleMasterID = 0;
        private DateTime _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM");
        private DateTime _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM");

        gloListControl.gloListControl oListControl;

        //For Recurrence pattern
        gloAppointmentScheduling.Criteria.FindRecurrences oFindCriteria = new gloAppointmentScheduling.Criteria.FindRecurrences();

        private bool _IsFormLoading = false;
        private bool _IsPatternChanging = false;
        private bool _IsPatternFinding = false;

        //These Variables Set from view schedule form 
        //To set initial parameters for setup Schedule
        private Int64 _nLocationID = 0;
        private Int64 _nDepartmentID = 0;
        private Int64 _nPRUID = 0;
        private AppointmentScheduleFlag _ScheduleType;
        private DateTime _dtStartTime;

        public Int64 LocationID
        {
            get { return _nLocationID; }
            set { _nLocationID = value; }
        }

        public Int64 DepartmentID
        {
            get { return _nDepartmentID; }
            set { _nDepartmentID = value; }
        }

        public Int64 PRUID
        {
            get { return _nPRUID; }
            set { _nPRUID = value; }
        }

        public AppointmentScheduleFlag ScheduleType
        {
            get { return _ScheduleType; }
            set { _ScheduleType = value; }
        }

        public DateTime StartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        } 
        #endregion
       
        #region C1Grid column Constants

        private const int COL_ID = 0;
        private const int COL_CODE = 1;
        private const int COL_DESC = 2;
        private const int COL_STARTTIME = 3;
        private const int COL_ENDTIME = 4;
        private const int COL_COLUMNCOUNT = 5;
        bool bln_rbProviderSchedulechecked = false; //added for checking provider schedule check box 
        #endregion

        #region Contructor

        public frmSetupSchedule(string DatabaseConnectionString)
        {
            InitializeComponent();
            _SetScheduleParameter = new SetScheduleParameter();
            _databaseConnectionString = DatabaseConnectionString;
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
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
         

        }

        public frmSetupSchedule(Int64 ScheduleMasterID, string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseConnectionString = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            _nScheduleMasterID = ScheduleMasterID;



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


        private SetScheduleParameter _SetScheduleParameter;

        public SetScheduleParameter SetScheduleParameters
        {
            get { return _SetScheduleParameter; }
            set { _SetScheduleParameter = value; }
        }
        

        #endregion

        #region "Form Load Events"

        private void frmSetupSchedule_Load(object sender, EventArgs e)
        {
            //
            //Code Added to resolve bug no.64962:: Screen Resolution-gloEMR-Go->Schedule

            //double DesignScreenWidth = Convert.ToDouble(1280);
            //double DesignScreenHeight = Convert.ToDouble(1024);
            //double CurrentScreenWidth = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width);
            //double CurrentScreenHeight = Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height);
            //double RatioX = CurrentScreenWidth / DesignScreenWidth;
            //float RatioY = Convert.ToSingle(CurrentScreenHeight / DesignScreenHeight);
            ////Me.Width = Me.Width * RatioX
            //this.Height = Convert.ToInt32(this.Height * RatioY);
            //this.Top = Convert.ToInt32(this.Top * RatioX);
            //this.Left = Convert.ToInt32(this.Left * RatioY);
            //this.AutoScroll = true;

            //pnlRecurrenceContainer.AutoScroll = true;
            //pnlScedules.AutoScroll = true;
            //pnlListControl.AutoScroll = true;

            //if (RatioY != 1)
            //{
            //    foreach (Control cnt in pnlRecurrenceContainer.Controls)
            //    {
            //        if (cnt.Name.Contains("pnl"))
            //        {
            //            cnt.Height = Convert.ToInt32(cnt.Height * RatioX);
            //        }
            //    }

            //    foreach (Control cnt in pnlScedules.Controls)
            //    {
            //        if (cnt.Name.Contains("pnl"))
            //        {
            //            cnt.Height = Convert.ToInt32(cnt.Height * RatioX);
            //        }
            //    }
            //    txtScheduleNote.Height = Convert.ToInt32(txtScheduleNote.Height * RatioY);
            //    c1Recurrence.Height = Convert.ToInt32(c1Recurrence.Height * RatioX);
            //}
            //
            gloC1FlexStyle.Style(c1ProviderProblemType, false);
            gloC1FlexStyle.Style(c1BlockedProviders, false);
            gloC1FlexStyle.Style(c1BlockedResources, false);
            gloC1FlexStyle.Style(c1ProviderResources, false);
            gloC1FlexStyle.Style(c1ProviderUsers, false);
            gloC1FlexStyle.Style(c1Recurrence, false);
            gloC1FlexStyle.Style(c1Resources, false);
            
            _IsFormLoading = true;            
            GetClinicTiming();
            Fill_Location();

            Fill_BlockTypes();
            FillProviders();
            FillRecurrenceControls();
           
            DesignGrid();
            
            rbSimple.Checked = true;
            ShowHideRecurrence(false);


            if (_nScheduleMasterID > 0)
            {                
                if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true || _SetScheduleParameter.ModifyAppointmentMethod == SingleRecurrence.Single)
                    tsb_Recurrence.Visible = false;
                else
                    tsb_Recurrence.Visible = true;

                LoadSchedule();
            }
            else
            {
                SetInitialParameters();
                dtpApp_DateTime_StartTime_ValueChanged(null, null);
                dtpRec_StartTime_ValueChanged(null, null);   
            }

            _IsFormLoading = false;

            
        }

        #endregion      

        #region "ToolStrip Commands"

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            if (_SetScheduleParameter.AddTrue_ModifyFalse_Flag == true || _SetScheduleParameter.ModifySingleAppointmentFromReccurence==false )
                            {
                                if (SaveSchedule() == true)
                                    this.Close();
                            }
                            else
                            {
                                if (SaveSchedule() == true)
                                    this.Close();
                            }
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "Recurrence":
                        {
                            if (rbRecurrence.Checked == true)
                            {
                                tsb_RemoveRecurrence.Visible = true;
                            }
                            ShowHideRecurrence(true);
                        }
                        break;
                    case "RemoveRecurrence":
                        {
                            if (rbRecurrence.Checked == true)
                            {
                                if (MessageBox.Show("Are you sure you want to clear this recurring schedule?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    tsb_Recurrence.Visible = true;
                                    tsb_RemoveRecurrence.Visible = false;

                                    lblApp_Recurrence_Time.Text = "";
                                    lblApp_Recurrence_Time.Tag = "";

                                    pnlApp_DateTime.Visible = true;
                                    lblApp_Recurrence_Time.Visible = false;

                                    //lvwRec_Apointments.Items.Clear();
                                    //lvwRec_Exception.Items.Clear();

                                    rbRecurrence.Checked = false;
                                    rbSimple.Checked = true;
                                    //DesignGrid();
                                    //ClearRecurrence();

                                    ShowHideRecurrence(false);
                                    ValidateData();
                                }
                            }
                            //rbSimple.Checked = true;
                            //rbSimple_CheckedChanged(null, null);
                            //ShowHideRecurrence(false);
                        }
                        break;
                    case "ApplyRecurrence":
                        
                        {
                             if (dtpRec_StartTime.Value > dtpRec_EndTime.Value)
                            {
                                MessageBox.Show(" End time must be greter than Start time.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                            ShowRecurrence();
                            rbRecurrence.Checked = true;
                            rbRecurrence_CheckedChanged(null, null);
                            rbSimple_CheckedChanged(null, null);
                            ShowHideRecurrence(false);
                        }
                        }
                        break;
                    case "ShowRecurrence":
                        {
                            if (dtpRec_StartTime.Value > dtpRec_EndTime.Value)
                            {
                                MessageBox.Show(" End time must be greter than Start time.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ShowRecurrence();
                            }
                        }
                        break;
                    case "RecurrenceCancel":
                        {
                            ShowHideRecurrence(false);
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

        #region " Fill Data"

        private void Fill_Location()
        {
            try
            {
                gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
                DataTable dt = new DataTable();
                dt = oLocation.GetLocationList();
                oLocation.Dispose();
                oLocation = null;

                if (dt != null)
                {
                    cmbLocation.DataSource = dt;
                    cmbLocation.ValueMember = dt.Columns["nLocationID"].ColumnName;
                    cmbLocation.DisplayMember = dt.Columns["sLocation"].ColumnName;
                }

                Int64 _DefaultLocationID = 0;
                _DefaultLocationID = GetDefaultLocation();

                if (_DefaultLocationID > 0)
                {
                    cmbLocation.SelectedValue = _DefaultLocationID;
                }

            }
            catch (Exception ex)
            {
                    
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void Fill_Department()
        {
            try
            {
                gloAppointmentBook.Books.Department oDepartment = new gloAppointmentBook.Books.Department(_databaseConnectionString);
                DataTable dt = new DataTable();
                dt = oDepartment.GetList();
                oDepartment.Dispose();
                oDepartment = null;

                if (dt != null)
                {
                    cmbDepartment.DataSource = dt;
                    cmbDepartment.ValueMember = dt.Columns["nDepartmentID"].ColumnName;
                    cmbDepartment.DisplayMember = dt.Columns["sDepartment"].ColumnName;
                    cmbDepartment.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {   
                
             gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        public Int64 GetDefaultLocation()
        {
            object oValue;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

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

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fill Departments on  Location Changed.

            //Variables Department 
            gloAppointmentScheduling.gloAppointmnetScheduleCommon oScheduleCommon = new gloAppointmentScheduling.gloAppointmnetScheduleCommon(_databaseConnectionString);
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
                if (cmbLocation.SelectedIndex > 0)
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
            }

          
        }
       
        private void FillProviders()
        {
            try
            {
                DataTable dt;
                // Fill Providers in the Combo Box
                gloAppointmentBook.Books.Resource oProvider = new gloAppointmentBook.Books.Resource(_databaseConnectionString);
                dt = oProvider.GetProviders();

                if (dt != null)
                {
                    cmbProvider.DataSource = dt;
                    cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                    cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                    cmbProvider.Refresh();
                    cmbProvider.SelectedIndex = -1;                    
                }
                //dt = null;
                oProvider.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        public void Fill_BlockTypes()
        {
            gloAppointmentBook.Books.AppointmentBlockType oBlockType = new gloAppointmentBook.Books.AppointmentBlockType(_databaseConnectionString);
            try
            {
                DataTable dt = new DataTable();
                dt = oBlockType.GetList();

                if (dt != null)
                {                    
                    cmbBlockType.DataSource = dt;
                    cmbBlockType.DisplayMember = "sAppointmentBlockType";
                    cmbBlockType.ValueMember = "nAppointmentBlockTypeID";
                    cmbBlockType.SelectedIndex = -1; 
                } 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oBlockType.Dispose();
            }
        }

        private void SetInitialParameters()
        {
            try
            {
                    if(_nLocationID != 0) 
                        cmbLocation.SelectedValue = _nLocationID;
                    if(_nDepartmentID != 0)
                    cmbDepartment.SelectedValue = _nDepartmentID;
                    
                    rbSimple.Checked = true;
                    dtpApp_DateTime_StartDate.Value = _dtStartTime.Date;
                    dtpApp_DateTime_StartTime.Value = _dtStartTime;
                    dtpApp_DateTime_StartTime.Value = _dtStartTime;

                    dtpApp_DateTime_StartTime_ValueChanged(null, null);
                    dtpRec_StartTime_ValueChanged(null, null);

                    if (_ScheduleType == AppointmentScheduleFlag.ProviderSchedule || _ScheduleType == AppointmentScheduleFlag.None)
                    {
                       // rbProviderSchedule.Checked = true;
                        rbBlockedSchedule.Checked = true;
                        rbBlockProvider.Checked = true;  
                        rbProviderSchedule_CheckedChanged(null, null);
                        cmbProvider.SelectedValue = _nPRUID;  
                        FillDefaultProvider(_nPRUID);
                    }
                    if (_ScheduleType == AppointmentScheduleFlag.BlockedSchedule)
                    {
                        rbBlockedSchedule.Checked = true;
                        rbBlockResource.Checked = true;
                        rbBlockProvider_CheckedChanged(null, null);

                        //
                        pnlProviderSchedule.Visible = false;
                        pnlResourceSchedule.Visible = false;
                        pnlBlockedSchedule.Visible = true;
                        pnlBlockedSchedule.BringToFront();
                        rbProviderSchedule.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                        rbResourceSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                        rbBlockedSchedule.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                        lbl_ProviderAsterix.Visible = true;
                        lbl_ProviderAsterix.BringToFront();
                        //

                        Resource oResource = new Resource(_databaseConnectionString);
                        oResource.GetResource(_nPRUID);
                         
                        c1BlockedResources.Rows.Add();
                        Int32 RowIndex = c1BlockedResources.Rows.Count - 1;
                        c1BlockedResources.SetData(RowIndex, COL_ID, Convert.ToString(_nPRUID));
                        c1BlockedResources.SetData(RowIndex, COL_CODE, Convert.ToString(oResource.Code));
                        c1BlockedResources.SetData(RowIndex, COL_DESC, Convert.ToString(oResource.Description));
                        c1BlockedResources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                        c1BlockedResources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                    }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }


        public void FillDefaultProvider(Int64 Providerid)
        {
            DataTable dt = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

            String _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " select nProviderID, '' as Code, sFirstName + ' ' + ISNULL(Provider_MST.sMiddleName,'') + ' ' + ISNULL(sLastName,'') as Name from Provider_MST  where nProviderID = " + Providerid;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                oDB.Dispose();
                oDB = null;

                if (dt.Rows.Count > 0)
                {
                    c1BlockedProviders.Rows.Count = 1;
                        c1BlockedProviders.Rows.Add();
                        Int32 RowIndex = c1BlockedProviders.Rows.Count - 1;
                        c1BlockedProviders.SetData(RowIndex, COL_ID, Convert.ToString(dt.Rows[0]["nProviderID"]));
                        c1BlockedProviders.SetData(RowIndex, COL_CODE, Convert.ToString(dt.Rows[0]["Code"]));
                        c1BlockedProviders.SetData(RowIndex, COL_DESC, Convert.ToString(dt.Rows[0]["Name"]));
                        if (rbSimple.Checked == true)
                        {
                            c1BlockedProviders.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                            c1BlockedProviders.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                        }
                        else
                        {
                            c1BlockedProviders.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                            c1BlockedProviders.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                        }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }

        }


        private void GetClinicTiming()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
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
        #endregion

        #region "Save / Modify / Fill Schedule"

        private void LoadSchedule()
        {
            try
            {
                gloSchedule ogloSchedule = new gloSchedule(_databaseConnectionString);
                //MasterSchedule oMasterSchedule = ogloSchedule.GetMasterSchedule(_nScheduleMasterID, 40040446454004001, SingleRecurrence.Single, SingleRecurrence.Recurrence, false, this._ClinicID);

                MasterSchedule oMasterSchedule = ogloSchedule.GetMasterSchedule(_SetScheduleParameter.MasterAppointmentID, _SetScheduleParameter.AppointmentID, _SetScheduleParameter.ModifyAppointmentMethod, _SetScheduleParameter.ModifyMasterAppointmentMethod, _SetScheduleParameter.ModifySingleAppointmentFromReccurence, _SetScheduleParameter.ClinicID);

                if (oMasterSchedule != null)
                {
                    DesignGrid();

                    #region "Patient & Notes"
                    txtScheduleNote.Text = oMasterSchedule.Notes;
                    #endregion

                    #region "Location"
                    if (oMasterSchedule.LocationName != "")
                    {
                        bool _AddItem = true;
                        int _t = cmbLocation.FindStringExact(oMasterSchedule.LocationName);
                        if (_t >= 0) { _AddItem = false; }

                        if (_AddItem == true)
                        {
                            DataTable oBindTable;
                            if (cmbLocation.DataSource != null)
                            {
                                oBindTable = (DataTable)cmbLocation.DataSource;
                            }
                            else
                            {
                                oBindTable = new DataTable();
                            }


                            DataRow oRow;
                            oRow = oBindTable.NewRow();
                            oRow[0] = oMasterSchedule.LocationID;
                            oRow[1] = oMasterSchedule.LocationName;
                            oBindTable.Rows.Add(oRow);

                            cmbLocation.DataSource = oBindTable;
                            cmbLocation.Refresh();
                        }

                        cmbLocation.SelectedIndex = cmbLocation.FindStringExact(oMasterSchedule.LocationName);
                    }
                    else
                    {
                        cmbLocation.SelectedIndex = -1;
                    }

                    #endregion

                    #region "Department"
                    if (oMasterSchedule.DepartmentName != "")
                    {
                        bool _AddItem = true;
                        int _t = cmbDepartment.FindStringExact(oMasterSchedule.DepartmentName);
                        if (_t >= 0) { _AddItem = false; }

                        if (_AddItem == true)
                        {
                            DataTable oBindTable;
                            if (cmbDepartment.DataSource != null)
                            {
                                oBindTable = (DataTable)cmbDepartment.DataSource;
                            }
                            else
                            {
                                oBindTable = new DataTable();
                            }


                            DataRow oRow;
                            oRow = oBindTable.NewRow();
                            oRow[0] = oMasterSchedule.DepartmentID;
                            oRow[1] = oMasterSchedule.DepartmentName;
                            oBindTable.Rows.Add(oRow);
                            cmbDepartment.DataSource = oBindTable;
                            cmbDepartment.Refresh();  
                        }

                        cmbDepartment.SelectedIndex = cmbDepartment.FindStringExact(oMasterSchedule.DepartmentName);
                    }
                    #endregion

                    #region Schedule Date, Time, Duration 

                    if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                    {
                        rbSimple.Checked = true;                        
                        dtpApp_DateTime_StartDate.Value = oMasterSchedule.StartDate;
                        dtpApp_DateTime_StartTime.Value = oMasterSchedule.StartTime;
                        dtpApp_DateTime_EndTime.Value = oMasterSchedule.EndTime;
                        //Code Added by Mayuri:20100115-To fix issue:#5774- In schedule add notes->100 character then page crash.   
                        if (oMasterSchedule.Duration == 0)
                        {
                            numApp_DateTime_Duration.Value = 1;
                        }
                        else
                        {
                            numApp_DateTime_Duration.Value = oMasterSchedule.Duration;
                        }
                        //End code Added by Mayuri:20100115
                        lblApp_ColorContainer.BackColor = Color.FromArgb(oMasterSchedule.ColorCode);                        
                    }
                    else
                    {
                        //if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                        //{
                        //    rbSimple.Checked = true;
                        //}
                        //else
                        //{
                            rbRecurrence.Checked = true;
                        //}
                        dtpApp_DateTime_StartDate.Value = oMasterSchedule.StartDate;
                        dtpApp_DateTime_StartTime.Value = oMasterSchedule.StartTime;
                        dtpApp_DateTime_EndTime.Value = oMasterSchedule.EndTime;
                        dtpRec_Range_StartDate.Value = oMasterSchedule.StartDate;
                        dtpRec_StartTime.Value = oMasterSchedule.StartTime;
                        dtpRec_EndTime.Value = oMasterSchedule.EndTime;
                        numRec_Duration.Value = oMasterSchedule.Duration;
                        lblRec_ColorContainer.BackColor = Color.FromArgb(oMasterSchedule.ColorCode);                        
                    }

                   


                    #endregion

                    #region " Provider Schedule "

                    if (oMasterSchedule.ASFlag == AppointmentScheduleFlag.ProviderSchedule)
                    {
                        rbProviderSchedule.Checked = true;
                        rbProviderSchedule.Enabled = true; 
                        rbResourceSchedule.Enabled = false;
                        rbBlockedSchedule.Enabled = false;
                        rbProviderSchedule_CheckedChanged(null, null); 

                        cmbProvider.SelectedValue = oMasterSchedule.ASBaseID;

                        for (int i = 0; i < oMasterSchedule.ProblemTypes.Count; i++)
                        {
                            c1ProviderProblemType.Rows.Add();
                            Int32 RowIndex = c1ProviderProblemType.Rows.Count - 1;
                            c1ProviderProblemType.SetData(RowIndex, COL_ID, oMasterSchedule.ProblemTypes[i].ASCommonID.ToString());
                            c1ProviderProblemType.SetData(RowIndex, COL_CODE, oMasterSchedule.ProblemTypes[i].ASCommonCode);
                            c1ProviderProblemType.SetData(RowIndex, COL_DESC, oMasterSchedule.ProblemTypes[i].ASCommonDescription);
                            c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.ProblemTypes[i].StartTime.ToShortTimeString());
                            c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.ProblemTypes[i].EndTime.ToShortTimeString());
                            txtScheduleNote.Text = oMasterSchedule.Notes;
                        }

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            if (!IsResourceAdded(oMasterSchedule.Resources[i].ASCommonID, oMasterSchedule.Resources[i].ASCommonCode))
                            {
                                c1ProviderResources.Rows.Add();
                                Int32 RowIndex = c1ProviderResources.Rows.Count - 1;
                                c1ProviderResources.SetData(RowIndex, COL_ID, oMasterSchedule.Resources[i].ASCommonID.ToString());
                                c1ProviderResources.SetData(RowIndex, COL_CODE, oMasterSchedule.Resources[i].ASCommonCode);
                                c1ProviderResources.SetData(RowIndex, COL_DESC, oMasterSchedule.Resources[i].ASCommonDescription);
                                c1ProviderResources.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.Resources[i].StartTime.ToShortTimeString());
                                c1ProviderResources.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.Resources[i].EndTime.ToShortTimeString());
                                txtScheduleNote.Text = oMasterSchedule.Notes;
                            }
                        }

                        for (int i = 0; i < oMasterSchedule.Users.Count; i++)
                        {
                            c1ProviderUsers.Rows.Add();
                            Int32 RowIndex = c1ProviderUsers.Rows.Count - 1;
                            c1ProviderUsers.SetData(RowIndex, COL_ID, oMasterSchedule.Users[i].ASCommonID.ToString());
                            c1ProviderUsers.SetData(RowIndex, COL_CODE, oMasterSchedule.Users[i].ASCommonCode);
                            c1ProviderUsers.SetData(RowIndex, COL_DESC, oMasterSchedule.Users[i].ASCommonDescription);
                            c1ProviderUsers.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.Users[i].StartTime.ToShortTimeString());
                            c1ProviderUsers.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.Users[i].EndTime.ToShortTimeString());
                            txtScheduleNote.Text = oMasterSchedule.Notes;
                        }

                    } 
                    #endregion

                    #region "Resource Schedule"

                    // Fill Resource Schedule
                    if (oMasterSchedule.ASFlag == AppointmentScheduleFlag.ResourceSchedule)
                    {
                        rbResourceSchedule.Checked = true;
                        rbResourceSchedule.Enabled = true; 
                        rbProviderSchedule.Enabled = false;
                        rbBlockedSchedule.Enabled = false;
                        rbProviderSchedule_CheckedChanged(null, null); 

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            c1Resources.Rows.Add();
                            Int32 RowIndex = c1Resources.Rows.Count - 1;
                            c1Resources.SetData(RowIndex, COL_ID, oMasterSchedule.Resources[i].ASCommonID.ToString());
                            c1Resources.SetData(RowIndex, COL_CODE, oMasterSchedule.Resources[i].ASCommonCode);
                            c1Resources.SetData(RowIndex, COL_DESC, oMasterSchedule.Resources[i].ASCommonDescription);
                            c1Resources.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.Resources[i].StartTime.ToShortTimeString());
                            c1Resources.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.Resources[i].EndTime.ToShortTimeString());
                            txtScheduleNote.Text = oMasterSchedule.Notes;
                        }
                    }

                    #endregion

                    #region " Blocked Schedule "
                    if (oMasterSchedule.ASFlag == AppointmentScheduleFlag.BlockedSchedule)
                    {
                        rbResourceSchedule.Enabled = false;
                        rbProviderSchedule.Enabled = false;
                        rbBlockedSchedule.Enabled = true;
                        rbBlockedSchedule.Checked = true;
                        rbProviderSchedule_CheckedChanged(null, null); 

                        cmbBlockType.SelectedValue = oMasterSchedule.ASBaseID;

                        for (int i = 0; i < oMasterSchedule.Resources.Count; i++)
                        {
                            rbBlockResource.Checked = true;
  
                            c1BlockedResources.Rows.Add();
                            Int32 RowIndex = c1BlockedResources.Rows.Count - 1;
                            c1BlockedResources.SetData(RowIndex, COL_ID, oMasterSchedule.Resources[i].ASCommonID.ToString());
                            c1BlockedResources.SetData(RowIndex, COL_CODE, oMasterSchedule.Resources[i].ASCommonCode);
                            c1BlockedResources.SetData(RowIndex, COL_DESC, oMasterSchedule.Resources[i].ASCommonDescription);
                            c1BlockedResources.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.Resources[i].StartTime.ToShortTimeString());
                            c1BlockedResources.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.Resources[i].EndTime.ToShortTimeString());
                            txtScheduleNote.Text = oMasterSchedule.Notes;
                        }

                        for (int i = 0; i < oMasterSchedule.Users.Count; i++)
                        {
                            rbBlockProvider.Checked = true;

                            c1BlockedProviders.Rows.Add();
                            Int32 RowIndex = c1BlockedProviders.Rows.Count - 1;
                            c1BlockedProviders.SetData(RowIndex, COL_ID, oMasterSchedule.Users[i].ASCommonID.ToString());
                            c1BlockedProviders.SetData(RowIndex, COL_CODE, oMasterSchedule.Users[i].ASCommonCode);
                            c1BlockedProviders.SetData(RowIndex, COL_DESC, oMasterSchedule.Users[i].ASCommonDescription);
                            c1BlockedProviders.SetData(RowIndex, COL_STARTTIME, oMasterSchedule.Users[i].StartTime.ToShortTimeString());
                            c1BlockedProviders.SetData(RowIndex, COL_ENDTIME, oMasterSchedule.Users[i].EndTime.ToShortTimeString());
                            txtScheduleNote.Text = oMasterSchedule.Notes;
                        }
                    }

                    #endregion

                    #region "Master Schedule Criteria"

                    if (oMasterSchedule.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                    {

                        //if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                        //{
                        //    rbSimple.Checked = true;
                        //}
                        //else
                        //{
                            rbRecurrence.Checked = true;
                        //}

                        if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                        { rbRec_Pattern_Daily.Checked = true; }
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                        { rbRec_Pattern_Weekly.Checked = true; }
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                        { rbRec_Pattern_Monthly.Checked = true; }
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                        { rbRec_Pattern_Yearly.Checked = true; }

                        //        //*************************/START/*********************//
                        //        //2.3.2.1 Recurrence Pattern - Daily
                        if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                        {
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay == RecurrencePatternFlag.EveryDay)
                            {
                                rbRec_Pattern_Daily_EveryDay.Checked = true;
                                numRec_Pattern_Daily_EveryDay.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                            }
                            else
                            {
                                rbRec_Pattern_Daily_EveryDay.Checked = false;
                                numRec_Pattern_Daily_EveryDay.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                            }
                        }

                         //2.3.2.2 Recurrence Pattern - Weekly
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                        {
                            rbRec_Pattern_Weekly.Checked = true;
                            numRec_Pattern_Weekly_WeekOn.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;

                            ChkRec_Pattern_Weekly_Sunday.Checked = false;
                            ChkRec_Pattern_Weekly_Monday.Checked = false;
                            ChkRec_Pattern_Weekly_Tuesday.Checked = false;
                            ChkRec_Pattern_Weekly_Wednesday.Checked = false;
                            ChkRec_Pattern_Weekly_Thursday.Checked = false;
                            ChkRec_Pattern_Weekly_Friday.Checked = false;
                            ChkRec_Pattern_Weekly_Saturday.Checked = false;

                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday == true) { ChkRec_Pattern_Weekly_Sunday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday == true) { ChkRec_Pattern_Weekly_Monday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday == true) { ChkRec_Pattern_Weekly_Tuesday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday == true) { ChkRec_Pattern_Weekly_Wednesday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday == true) { ChkRec_Pattern_Weekly_Thursday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday == true) { ChkRec_Pattern_Weekly_Friday.Checked = true; }
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday == true) { ChkRec_Pattern_Weekly_Saturday.Checked = true; }
                        }
                        //2.3.2.3 Recurrence Pattern - Monthly
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                        {
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                            {
                                rbRec_Pattern_Daily.Checked = true;
                                numRec_Pattern_Monthly_Day_Day.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                                numRec_Pattern_Monthly_Day_Month.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                            }
                            else
                            {
                                rbRec_Pattern_Monthly_Criteria.Checked = true;

                                numRec_Pattern_Monthly_Criteria_Month.Value = (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber);

                                numRec_Pattern_Monthly_Day_Day.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                                cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.ToString();
                                cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;
                            }


                        }
                        //2.3.2.4 Recurrence Pattern - Yearly
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                        {

                            rbRec_Pattern_Yearly.Checked = true;
                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                            {
                                rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
                                numRec_Pattern_Yearly_Every_MonthDay.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                                cmbRec_Pattern_Yearly_Every_Month.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.ToString();

                            }
                            else if (oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.SelectedCriteria)
                            {
                                rbRec_Pattern_Yearly_Criteria.Checked = true;
                                numRec_Pattern_Yearly_Every_MonthDay.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                                cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.ToString();//= (MonthRange).ToString().GetHashCode();
                                cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria; // = (FirstLastCriteria).ToString().GetHashCode();
                                cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;  //= (DayWeekday).ToString().GetHashCode();
                            }
                        }

                        //        //*************************/FINISH/*********************//
                        //        //2.3.3 Recurrence Range

                        if (oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                        {
                            rbRec_Range_EndAfterOccurence.Checked = true;
                            dtpRec_Range_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate);
                            dtpRec_Range_EndBy.Value = gloDateMaster.gloDate.DateAsDate(oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate);
                            numRec_Range_EndAfterOccurence.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber;

                        }
                        else if (oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                        {
                            rbRec_Range_EndBy.Checked = true;
                            dtpRec_Range_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate);
                            dtpRec_Range_EndBy.Value = gloDateMaster.gloDate.DateAsDate(oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate);
                            numRec_Range_EndAfterOccurence.Value = oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber;

                        }
                        else
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndByYear;

                            if (oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear != 0)
                            {
                                cmbRec_Range_NoEndDateYear.SelectedItem = oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear.ToString();
                                dtpRec_Range_StartDate.Value = gloDateMaster.gloDate.DateAsDate(oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate);
                            }

                        }
                        rbSimple_CheckedChanged(null, null);
                        ShowRecurrence();

                        if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                        {
                            dtpApp_DateTime_StartDate.Value = oMasterSchedule.StartDate;
                            dtpApp_DateTime_StartTime.Value = oMasterSchedule.StartTime;
                            dtpApp_DateTime_EndTime.Value = oMasterSchedule.EndTime;
                            numApp_DateTime_Duration.Value = oMasterSchedule.Duration;
                            lblApp_ColorContainer.BackColor = Color.FromArgb(oMasterSchedule.ColorCode);                             
                            lblRec_ColorContainer.BackColor = Color.FromArgb(oMasterSchedule.ColorCode);                            
                            //lblSimple_Color.BackColor = Color.FromArgb(oMasterSchedule.ColorCode);

                            lblApp_Recurrence_Time.Text = "";
                            pnlApp_DateTime.Visible = true;
                            lblApp_Recurrence_Time.Visible = false;
                        
                        }
                    


                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool SaveSchedule()
        {
            Int64 _result = 0;
            try
            {
                if (ValidateData() == false)
                {
                    return false;
                }
                MasterSchedule oMasterSchedule = new MasterSchedule();

                #region "MasterSchedule Data"

                oMasterSchedule.MasterID = _nScheduleMasterID;

                if (rbSimple.Checked == true)
                    oMasterSchedule.IsRecurrence = SingleRecurrence.Single;
                else
                    oMasterSchedule.IsRecurrence = SingleRecurrence.Recurrence;

                if (rbProviderSchedule.Checked == true)
                {
                    oMasterSchedule.ASFlag = AppointmentScheduleFlag.ProviderSchedule;

                    oMasterSchedule.ASBaseID = Convert.ToInt64(cmbProvider.SelectedValue);
                    oMasterSchedule.ASBaseCode = "0";
                    oMasterSchedule.ASBaseDescription = cmbProvider.Text;
                    oMasterSchedule.ASBaseFlag = ASBaseType.Provider;

                }
                if (rbResourceSchedule.Checked == true)
                {
                    oMasterSchedule.ASFlag = AppointmentScheduleFlag.ResourceSchedule;
                    oMasterSchedule.ASBaseID = 0; //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                    oMasterSchedule.ASBaseCode = "";
                    oMasterSchedule.ASBaseDescription = "";
                    oMasterSchedule.ASBaseFlag = ASBaseType.Resource;

                }
                if (rbBlockedSchedule.Checked == true)
                {
                    oMasterSchedule.ASFlag = AppointmentScheduleFlag.BlockedSchedule;
                    oMasterSchedule.ASBaseID = Convert.ToInt64(cmbBlockType.SelectedValue);
                    oMasterSchedule.ASBaseCode = "";
                    oMasterSchedule.ASBaseDescription = cmbBlockType.Text;
                    oMasterSchedule.ASBaseFlag = ASBaseType.Block;
                }

                oMasterSchedule.ScheduleTypeID = 0; //***PROVISIONAL*** Schedule Predefined Type
                oMasterSchedule.ScheduleTypeCode = "";
                oMasterSchedule.ScheduleTypeDesc = "";

                if (rbSimple.Checked == true)
                {
                    oMasterSchedule.StartDate = dtpApp_DateTime_StartDate.Value;
                    oMasterSchedule.EndDate = dtpApp_DateTime_StartDate.Value;
                    oMasterSchedule.StartTime = dtpApp_DateTime_StartTime.Value;
                    oMasterSchedule.EndTime = dtpApp_DateTime_EndTime.Value;
                    oMasterSchedule.Duration = numApp_DateTime_Duration.Value;
                    oMasterSchedule.ColorCode = lblApp_ColorContainer.BackColor.ToArgb();
                }
                else // if recurrence
                {
                    //oMasterSchedule.StartDate = dtpRec_Range_StartDate.Value;
                    //oMasterSchedule.EndDate = dtpRec_Range_EndBy.Value;
                    //oMasterSchedule.StartTime = dtpRec_StartTime.Value;
                    //oMasterSchedule.EndTime = dtpRec_EndTime.Value;
                    //oMasterSchedule.Duration = numRec_Duration.Value;
                    //oMasterSchedule.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();

                    if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                    {
                        oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;

                        oMasterSchedule.StartDate = dtpApp_DateTime_StartDate.Value;
                        oMasterSchedule.EndDate = dtpApp_DateTime_StartDate.Value;
                        oMasterSchedule.StartTime = dtpApp_DateTime_StartTime.Value;
                        oMasterSchedule.EndTime = dtpApp_DateTime_EndTime.Value;
                        oMasterSchedule.Duration = numApp_DateTime_Duration.Value;
                        oMasterSchedule.ColorCode = lblApp_ColorContainer.BackColor.ToArgb();
                    }
                    else
                    {
                        oMasterSchedule.StartDate = dtpRec_Range_StartDate.Value;
                        oMasterSchedule.EndDate = dtpRec_Range_EndBy.Value;
                        oMasterSchedule.StartTime = dtpRec_StartTime.Value;
                        oMasterSchedule.EndTime = dtpRec_EndTime.Value;
                        oMasterSchedule.Duration = numRec_Duration.Value;
                        oMasterSchedule.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                    }
                                      

                }
                     // By Pranit on 29 feb 2012
                    DateTime newEndDate = Convert.ToDateTime(oMasterSchedule.EndDate.ToShortDateString() +" "+ oMasterSchedule.EndTime.ToShortTimeString());
                    oMasterSchedule.EndDate = newEndDate;

                if (cmbLocation.SelectedIndex > 0)
                {
                    oMasterSchedule.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                    oMasterSchedule.LocationName = cmbLocation.Text;
                }


                if (cmbLocation.SelectedIndex == 0)
                {
                    oMasterSchedule.LocationID = 0;
                    oMasterSchedule.LocationName = cmbLocation.Text;
                }



                if (cmbDepartment.SelectedIndex > 0)
                {
                    oMasterSchedule.DepartmentID = Convert.ToInt64(cmbDepartment.SelectedValue);
                    oMasterSchedule.DepartmentName = cmbDepartment.Text;
                }

                oMasterSchedule.Notes = txtScheduleNote.Text;

                oMasterSchedule.ClinicID = _ClinicID;
                #endregion

                #region "Master Schedule Criteria"

                //2.1. Is it single Schedule or recurring Schedule
                if (rbRecurrence.Checked == true)
                {
                    oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Recurrence;
                }
                else
                {
                    oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;
                }

                //2.2 If single Schedule then setup -- ***SINGLE SCHEDULE***
                if (rbRecurrence.Checked == false)
                {
                    oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                    oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                    oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                    oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                    oMasterSchedule.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;
                }
                //2.3 If single schedule then setup -- ***RECURRENCE SCHEDULE***
                else
                {
                    if (_SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                    {
                        oMasterSchedule.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;
                        oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.SingleInRecurrence;
                    }
                    else
                    {
                        //2.3.1 Schedule Date & Time
                        oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_StartTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_EndTime.Value.ToString("hh:mm tt"));
                        oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.Duration = numRec_Duration.Value;

                        //2.3.2 Recurrence Pattern

                        if (rbRec_Pattern_Daily.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;
                        }
                        else if (rbRec_Pattern_Weekly.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Weekly;
                        }
                        else if (rbRec_Pattern_Monthly.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Monthly;
                        }
                        else if (rbRec_Pattern_Yearly.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Yearly;
                        }
                        //*************************/START/*********************//
                        //2.3.2.1 Recurrence Pattern - Daily
                        if (rbRec_Pattern_Daily.Checked == true)
                        {
                            if (rbRec_Pattern_Daily_EveryDay.Checked == true)
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Value);
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                            }
                            else
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 0;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryWeekday;
                            }
                        }
                        //2.3.2.2 Recurrence Pattern - Weekly
                        else if (rbRec_Pattern_Weekly.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Value);
                            if (ChkRec_Pattern_Weekly_Sunday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = true; }
                            if (ChkRec_Pattern_Weekly_Monday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = true; }
                            if (ChkRec_Pattern_Weekly_Tuesday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = true; }
                            if (ChkRec_Pattern_Weekly_Wednesday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = true; }
                            if (ChkRec_Pattern_Weekly_Thursday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = true; }
                            if (ChkRec_Pattern_Weekly_Friday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = true; }
                            if (ChkRec_Pattern_Weekly_Saturday.Checked == true) { oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = true; }
                        }

                        //2.3.2.3 Recurrence Pattern - Monthly
                        else if (rbRec_Pattern_Monthly.Checked == true)
                        {
                            if (rbRec_Pattern_Daily.Checked == true)
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Value);
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Value);
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = DayWeekday.day;
                            }
                            else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = 0;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Value);
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString());
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString());
                            }
                        }
                        //2.3.2.4 Recurrence Pattern - Yearly
                        else if (rbRec_Pattern_Yearly.Checked == true)
                        {
                            if (rbRec_Pattern_Yearly_EveryMonthDay.Checked == true)
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Value);
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString());
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;
                            }
                            else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                            {
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Criteria_Month.SelectedItem.ToString());
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (FirstLastCriteria)Enum.Parse(typeof(FirstLastCriteria), cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedItem.ToString());
                                oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (DayWeekday)Enum.Parse(typeof(DayWeekday), cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedItem.ToString());
                            }
                        }
                        //*************************/FINISH/*********************//
                        //2.3.3 Recurrence Range
                        if (rbRec_Range_EndAfterOccurence.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndAfterOccurence;
                        }
                        else if (rbRec_Range_EndBy.Checked == true)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate;
                        }
                        else
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndByYear;
                        }

                        oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_StartDate.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpRec_Range_EndBy.Value.ToString("MM/dd/yyyy"));
                        oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(numRec_Range_EndAfterOccurence.Value);
                        if (cmbRec_Range_NoEndDateYear.SelectedItem != null)
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(cmbRec_Range_NoEndDateYear.SelectedItem.ToString());
                        }
                        else
                        {
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(DateTime.Now.Year);
                        }
                    }

                }
                #endregion

                //Provider Schedule 
                if (rbProviderSchedule.Checked == true)
                {
                    #region "Provider - Problem Type"

                    for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();

                        oShortDetail.MasterID = _nScheduleMasterID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1ProviderProblemType.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1ProviderProblemType.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.ProblemType;

                        if (rbRecurrence.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                        }
                        oShortDetail.StartTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME));
                        oShortDetail.EndTime = Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME));
                        oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                        oShortDetail.ClinicID = _ClinicID;
                        oShortDetail.ViewOtherDetails = "";

                        oMasterSchedule.ProblemTypes.Add(oShortDetail);
                    }

                    #endregion

                    #region "Provider - Resources"

                    for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _nScheduleMasterID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1ProviderResources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1ProviderResources.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1ProviderResources.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.Resource;

                        if (rbRecurrence.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                        }
                        oShortDetail.StartTime = Convert.ToDateTime(c1ProviderResources.GetData(i, COL_STARTTIME));
                        oShortDetail.EndTime = Convert.ToDateTime(c1ProviderResources.GetData(i, COL_ENDTIME));
                        oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                        oShortDetail.ClinicID = _ClinicID;
                        oShortDetail.ViewOtherDetails = "";

                        oMasterSchedule.Resources.Add(oShortDetail);
                    }

                    #endregion

                    #region "Provider - Users"

                    for (int i = 1; i < c1ProviderUsers.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _nScheduleMasterID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1ProviderUsers.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1ProviderUsers.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1ProviderUsers.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.User;

                        if (rbRecurrence.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                        }
                        oShortDetail.StartTime = Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_STARTTIME));
                        oShortDetail.EndTime = Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_ENDTIME));
                        oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                        oShortDetail.ClinicID = _ClinicID;
                        oShortDetail.ViewOtherDetails = "";

                        oMasterSchedule.Resources.Add(oShortDetail);
                    }

                    #endregion
                }
                //Resource Schedule
                if (rbResourceSchedule.Checked == true)
                {
                    #region "Resources"

                    for (int i = 1; i < c1Resources.Rows.Count; i++)
                    {
                        ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                        oShortDetail.MasterID = _nScheduleMasterID;
                        oShortDetail.DetailID = 0;
                        oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                        oShortDetail.LineNo = 0;
                        oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                        oShortDetail.ASCommonID = Convert.ToInt64(c1Resources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                        oShortDetail.ASCommonCode = c1Resources.GetData(i, COL_CODE).ToString();
                        oShortDetail.ASCommonDescription = c1Resources.GetData(i, COL_DESC).ToString();
                        oShortDetail.ASCommonFlag = ASBaseType.Resource;

                        if (rbRecurrence.Checked == true)
                        {
                            oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                            oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                        }
                        else
                        {
                            oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                            oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                        }
                        oShortDetail.StartTime = Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME));
                        oShortDetail.EndTime = Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME));

                        oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                        oShortDetail.ClinicID = _ClinicID;
                        oShortDetail.ViewOtherDetails = "";

                        oMasterSchedule.Resources.Add(oShortDetail);
                    }

                    #endregion

                    oMasterSchedule.ProblemTypes.Clear();
                    oMasterSchedule.Users.Clear();
                }
                //Blocked Schedule
                if (rbBlockedSchedule.Checked == true)
                {
                    #region "Block - Resources"
                    if (rbBlockResource.Checked == true)
                    {

                        for (int i = 1; i < c1BlockedResources.Rows.Count; i++)
                        {
                            ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                            oShortDetail.MasterID = _nScheduleMasterID;
                            oShortDetail.DetailID = 0;
                            oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                            oShortDetail.LineNo = 0;
                            oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                            oShortDetail.ASCommonID = Convert.ToInt64(c1BlockedResources.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            oShortDetail.ASCommonCode = c1BlockedResources.GetData(i, COL_CODE).ToString();
                            oShortDetail.ASCommonDescription = c1BlockedResources.GetData(i, COL_DESC).ToString();
                            oShortDetail.ASCommonFlag = ASBaseType.Resource;

                            if (rbRecurrence.Checked == true)
                            {
                                oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                                oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            }
                            else
                            {
                                oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                                oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                            }
                            oShortDetail.StartTime = Convert.ToDateTime(c1BlockedResources.GetData(i, COL_STARTTIME));
                            oShortDetail.EndTime = Convert.ToDateTime(c1BlockedResources.GetData(i, COL_ENDTIME));

                           

                            oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                            oShortDetail.ClinicID = _ClinicID;
                            oShortDetail.ViewOtherDetails = "";

                            oMasterSchedule.Resources.Add(oShortDetail);
                        }

                    }

                    #endregion

                    #region "Block - Providers"
                    if (rbBlockProvider.Checked == true)
                    {

                        for (int i = 1; i < c1BlockedProviders.Rows.Count; i++)
                        {
                            ShortApointmentSchedule oShortDetail = new ShortApointmentSchedule();
                            oShortDetail.MasterID = _nScheduleMasterID;
                            oShortDetail.DetailID = 0;
                            oShortDetail.IsRecurrence = oMasterSchedule.IsRecurrence;
                            oShortDetail.LineNo = 0;
                            oShortDetail.ASFlag = oMasterSchedule.ASFlag;
                            oShortDetail.ASCommonID = Convert.ToInt64(c1BlockedProviders.GetData(i, COL_ID).ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            oShortDetail.ASCommonCode = c1BlockedProviders.GetData(i, COL_CODE).ToString();
                            oShortDetail.ASCommonDescription = c1BlockedProviders.GetData(i, COL_DESC).ToString();
                            oShortDetail.ASCommonFlag = ASBaseType.Provider;

                            if (rbRecurrence.Checked == true)
                            {
                                oShortDetail.StartDate = dtpRec_Range_StartDate.Value;
                                oShortDetail.EndDate = dtpRec_Range_EndBy.Value;
                            }
                            else
                            {
                                oShortDetail.StartDate = dtpApp_DateTime_StartDate.Value;
                                oShortDetail.EndDate = dtpApp_DateTime_StartDate.Value;
                            }
                            oShortDetail.StartTime = Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_STARTTIME));
                            oShortDetail.EndTime = Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_ENDTIME));

                         

                            oShortDetail.ColorCode = lblRec_ColorContainer.BackColor.ToArgb();
                            oShortDetail.ClinicID = _ClinicID;
                            oShortDetail.ViewOtherDetails = "";

                            oMasterSchedule.Users.Add(oShortDetail);
                        }
                    }
                    #endregion

                    oMasterSchedule.ProblemTypes.Clear();
                }

                oMasterSchedule.ScheduleDetails = null;

                gloSchedule ogloSchedule = new gloSchedule(_databaseConnectionString);
                _result = ogloSchedule.Add(oMasterSchedule, _SetScheduleParameter.AppointmentID);


                if (_result > 0)
                {
                    if (_nScheduleMasterID == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Add, "Schedule saved", 0, _result, 0, ActivityOutCome.Success);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Modify, "Schedule modified", 0, _result, 0, ActivityOutCome.Success);
                    }
                    ogloSchedule.Dispose();
                    return true;
                }
                else
                {
                    if (_nScheduleMasterID == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Add, "Add schedule", 0, _result, 0, ActivityOutCome.Failure);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.SetupSchedule, ActivityType.Modify, "Modify schedule", 0, _result, 0, ActivityOutCome.Failure);
                    }
                    ogloSchedule.Dispose();
                
                    return false;
                }

               // ogloSchedule.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return false;
        }

        private bool ValidateData()
        {
            DateTime dtScheduleStartTime;
            DateTime dtScheduleEndTime;
            //if (rbSimple.Checked == true)
            //{
            //    dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            //    dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            //}
            //else
            //{
            //    dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            //    dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            //}

            if (rbRecurrence.Checked == false || _SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
            {
                dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpApp_DateTime_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            }
            else
            {
                dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_StartTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + dtpRec_EndTime.Value.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            }


            if (rbProviderSchedule.Checked == true)
            {
                #region " Provider Schedule Validations "
                //Provider
                if (cmbProvider.SelectedIndex == -1)
                {
                    MessageBox.Show(" Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbProvider.Focus();
                    return false;
                }

                //Check Problem Type Start & End Time is between Schedule Time 
                for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                {
                    DateTime dtPTStartTime;
                    DateTime dtPTEndTime;

                    dtPTStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtPTEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                    if (dtPTStartTime < dtScheduleStartTime || dtPTStartTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Problem type 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderProblemType.Focus();
                        c1ProviderProblemType.Row = i;
                        c1ProviderProblemType.Col = COL_STARTTIME;
                        return false;
                    }
                    if (dtPTEndTime < dtScheduleStartTime || dtPTEndTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Problem type 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderProblemType.Focus();
                        c1ProviderProblemType.Row = i;
                        c1ProviderProblemType.Col = COL_ENDTIME;
                        return false;
                    }
                }

                //Check Resource Start & End Time is between Schedule Time 
                for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                {
                    DateTime dtRSStartTime;
                    DateTime dtRSEndTime;

                    dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderResources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderResources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                    if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderResources.Focus();
                        c1ProviderResources.Row = i;
                        c1ProviderResources.Col = COL_STARTTIME;
                        return false;
                    }
                    if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderResources.Focus();
                        c1ProviderResources.Row = i;
                        c1ProviderResources.Col = COL_ENDTIME;
                        return false;
                    }
                }

                //Check Resource Start & End Time is between Schedule Time 
                for (int i = 1; i < c1ProviderUsers.Rows.Count; i++)
                {
                    DateTime dtURStartTime;
                    DateTime dtUREndTime;

                    dtURStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtUREndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                    if (dtURStartTime < dtScheduleStartTime || dtURStartTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderUsers.Focus();
                        c1ProviderUsers.Row = i;
                        c1ProviderUsers.Col = COL_STARTTIME;
                        return false;
                    }
                    if (dtUREndTime < dtScheduleStartTime || dtUREndTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1ProviderUsers.Focus();
                        c1ProviderUsers.Row = i;
                        c1ProviderUsers.Col = COL_ENDTIME;
                        return false;
                    }
                }
                #endregion
            }
            if (rbResourceSchedule.Checked == true)
            {
                #region " Resource Schedule Validations "

                //Check Resource Start & End Time is between Schedule Time 
                for (int i = 1; i < c1Resources.Rows.Count; i++)
                {
                    DateTime dtRSStartTime;
                    DateTime dtRSEndTime;

                    dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                    dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                    if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Resources.Focus();
                        c1Resources.Row = i;
                        c1Resources.Col = COL_STARTTIME;
                        return false;
                    }
                    if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                    {
                        MessageBox.Show(" Resource 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1Resources.Focus();
                        c1Resources.Row = i;
                        c1Resources.Col = COL_ENDTIME;
                        return false;
                    }
                }

                #endregion
            }
            if (rbBlockedSchedule.Checked == true)
            {
                if (rbBlockProvider.Checked == true)
                {
                    if (c1BlockedProviders.Rows.Count <= 1)
                    {
                        MessageBox.Show("Select a provider.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;
                    }
                    #region " Block Provider Validations "

                    //Check Resource Start & End Time is between Schedule Time 
                    for (int i = 1; i < c1BlockedProviders.Rows.Count; i++)
                    {
                        DateTime dtRSStartTime;
                        DateTime dtRSEndTime;

                        dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                        dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                        if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                        {
                            MessageBox.Show(" Provider 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1BlockedProviders.Focus();
                            c1BlockedProviders.Row = i;
                            c1BlockedProviders.Col = COL_STARTTIME;
                            return false;
                        }
                        if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                        {
                            MessageBox.Show(" Provider 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1BlockedProviders.Focus();
                            c1BlockedProviders.Row = i;
                            c1BlockedProviders.Col = COL_ENDTIME;
                            return false;
                        }
                    }

                    #endregion
                }
                else if (rbBlockResource.Checked == true)
                {
                    #region " Block Resource Validations "

                    if (c1BlockedResources.Rows.Count > 1)
                    {
                        //Check Resource Start & End Time is between Schedule Time 
                        for (int i = 1; i < c1BlockedResources.Rows.Count; i++)
                        {
                            DateTime dtRSStartTime;
                            DateTime dtRSEndTime;

                            dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1BlockedResources.GetData(i, COL_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                            dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1BlockedResources.GetData(i, COL_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                            if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                            {
                                MessageBox.Show(" Provider 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1BlockedResources.Focus();
                                c1BlockedResources.Row = i;
                                c1BlockedResources.Col = COL_STARTTIME;
                                return false;
                            }
                            if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                            {
                                MessageBox.Show(" Provider 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1BlockedResources.Focus();
                                c1BlockedResources.Row = i;
                                c1BlockedResources.Col = COL_ENDTIME;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select a Resource.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    #endregion
                }
            }

            #region "Clinic Timing validations"

            //Clinic Timing validations
            Int32 _nClinicStartTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicStartTime.ToShortTimeString());
            Int32 _nClinicEndTime = gloDateMaster.gloTime.TimeAsNumber(_dtClinicEndTime.ToShortTimeString());
            Int32 _nProposedStartTime;
            Int32 _nProposedEndTime;

            if (rbSimple.Checked == true)
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());
            }
            else
            {
                _nProposedStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_StartTime.Value.ToShortTimeString());
                _nProposedEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_EndTime.Value.ToShortTimeString());
            }


            DialogResult _DialogResult = DialogResult.None;

            if (_nProposedStartTime < _nClinicStartTime || _nProposedStartTime > _nClinicEndTime)
            {
                _DialogResult = MessageBox.Show(" Schedule  is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_DialogResult == DialogResult.No)
                {
                    if (rbSimple.Checked == true)
                        dtpApp_DateTime_StartTime.Focus();
                    else
                        dtpRec_StartTime.Focus();

                    return false;
                }
            }
            else if (_nProposedEndTime < _nClinicStartTime || _nProposedEndTime > _nClinicEndTime)
            {
                _DialogResult = MessageBox.Show("Schedule  is outside clinic time.  Do you want to continue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_DialogResult == DialogResult.No)
                {
                    if (rbSimple.Checked == true)
                        dtpApp_DateTime_StartTime.Focus();
                    else
                        dtpRec_StartTime.Focus();

                    return false;
                }
            }
            #endregion


            return true;
        }

        #endregion

        #region "Update Appointments - Modify Appointment"


        #endregion

        #region "Form Controls Event & Methods"
     
        private void ShowHideRecurrence(bool ShowRecurrence)
        {
            if (ShowRecurrence == true)
            {
               // Commented by Pranit on 21 sep 2011 to show correct recurrence start time and end time
               // dtpRec_StartTime.Value = dtpApp_DateTime_StartTime.Value;
               // dtpRec_EndTime.Value = dtpApp_DateTime_EndTime.Value;

               // Below 2 lines Added By Pranit on 21 sep 2011 to show correct recurrence start time and end time 

                if (c1Recurrence.Rows.Count > 1)
                {
                    dtpApp_DateTime_StartTime.Value = dtpRec_StartTime.Value;
                    dtpApp_DateTime_EndTime.Value = dtpRec_EndTime.Value;
                    lblApp_ColorContainer.BackColor = lblRec_ColorContainer.BackColor;

                }
                else
                {
                   dtpRec_StartTime.Value = dtpApp_DateTime_StartTime.Value;
                   dtpRec_EndTime.Value = dtpApp_DateTime_EndTime.Value;
                   lblRec_ColorContainer.BackColor = lblApp_ColorContainer.BackColor;
                }              


                tsb_OK.Visible = false;
                tsb_Cancel.Visible = false;
                tsb_Recurrence.Visible = false;
                tsb_ApplyRecurrence.Visible = true;
                tsb_ShowRecurrence.Visible = true;
                tsb_CancelRecurrence.Visible = true;  
                pnlScedules.Visible = false;
                pnlListControl.Visible = false;
                pnlRecurrenceContainer.Visible = true;
                pnlRecurrenceContainer.BringToFront();
            }
            else
            {
                dtpApp_DateTime_StartTime.Value =  dtpRec_StartTime.Value;
                dtpApp_DateTime_EndTime.Value =  dtpRec_EndTime.Value;
                tsb_OK.Visible = true;
                tsb_Cancel.Visible = true;
                tsb_Recurrence.Visible = true;
                tsb_RemoveRecurrence.Visible = false;
                tsb_ApplyRecurrence.Visible = false;
                tsb_ShowRecurrence.Visible = false;
                tsb_CancelRecurrence.Visible = false;
                pnlScedules.Visible = true;
                pnlListControl.Visible = false;
                pnlRecurrenceContainer.Visible = false;
                pnlRecurrenceContainer.SendToBack();
                if (bln_rbProviderSchedulechecked == true) //added for bugid 93459
                {
                    rbProviderSchedule.CheckedChanged  -= rbProviderSchedule_CheckedChanged;
                    rbProviderSchedule.Checked = true;
                    rbProviderSchedule.CheckedChanged += rbProviderSchedule_CheckedChanged;
                }
                else
                {
                    rbBlockedSchedule.CheckedChanged -= rbProviderSchedule_CheckedChanged;
                    rbBlockedSchedule.Checked = true;
                    rbBlockedSchedule.CheckedChanged += rbProviderSchedule_CheckedChanged;
                }
            }
        }

        private void rbProviderSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProviderSchedule.Checked == true)
            {
                pnlProviderSchedule.Visible = true;
                pnlProviderSchedule.BringToFront();
                pnlResourceSchedule.Visible = false;
                pnlBlockedSchedule.Visible = false;
                rbProviderSchedule.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbResourceSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbBlockedSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);

                lbl_ProviderAsterix.Visible = false;
                bln_rbProviderSchedulechecked = true; //added for bugid 93459
            }
            if (rbResourceSchedule.Checked == true)
            {
                pnlProviderSchedule.Visible = false;
                pnlResourceSchedule.Visible = true;
                pnlResourceSchedule.BringToFront();
                pnlBlockedSchedule.Visible = false;
                rbProviderSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbResourceSchedule.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbBlockedSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);

                lbl_ProviderAsterix.Visible = false;
            }
            if (rbBlockedSchedule.Checked == true)
            {
                pnlProviderSchedule.Visible = false;
                pnlResourceSchedule.Visible = false;
                pnlBlockedSchedule.Visible = true;
                pnlBlockedSchedule.BringToFront();
                rbProviderSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbResourceSchedule.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbBlockedSchedule.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                lbl_ProviderAsterix.Visible = true ;
                lbl_ProviderAsterix.BringToFront();
                bln_rbProviderSchedulechecked = false; //added for bugid 93459
            }
        }

        private void rbBlockProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBlockResource.Checked == true)
            {
                rbBlockResource.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbBlockProvider.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                pnlCriteria_BlockedResources.Visible = true;
                btnBrowseBlockedResources.Visible = true;
                btnClearBlockedResources.Visible = true;

                pnlCriteria_BlockedProviders.Visible = false;
                btnBrowseBlockedProvider.Visible = false;
                btnClearBlockedProvider.Visible = false;
                //pnlCriteria_BlockedResources.Visible = false;
                //btnBrowseBlockedResources.Visible = false;
                //btnClearBlockedResources.Visible = false;

                //pnlCriteria_BlockedProviders.Visible = true;
                //btnBrowseBlockedProvider.Visible = true;
                //btnClearBlockedProvider.Visible = true;
            }
            if (rbBlockProvider.Checked == true)
            {
                rbBlockResource.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbBlockProvider.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlCriteria_BlockedResources.Visible = false;
                btnBrowseBlockedResources.Visible = false; 
                btnClearBlockedResources.Visible = false;

                pnlCriteria_BlockedProviders.Visible = true;
                btnBrowseBlockedProvider.Visible = true;
                btnClearBlockedProvider.Visible = true;
                lbl_ProviderAsterix.BringToFront();
            }
        }

        private void btnColorCode_Click(object sender, EventArgs e)
        {
            try
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
                    lblRec_ColorContainer.BackColor = colorDialog1.Color;
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnApp_DateTime_Color_Click(object sender, EventArgs e)
        {
            try
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
                    lblApp_ColorContainer.BackColor = colorDialog1.Color;
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "C1 Grid Design"

        private void DesignGrid()
        {
            try
            {
                int _width;

                #region Provider Resources
                c1ProviderResources.Rows.Count = 1;
                c1ProviderResources.Rows.Fixed = 1;
                c1ProviderResources.Cols.Count = COL_COLUMNCOUNT;
                c1ProviderResources.Cols.Fixed = 0;

                c1ProviderResources.SetData(0, COL_ID, "ID");                
                c1ProviderResources.SetData(0, COL_CODE, "Code");
                c1ProviderResources.SetData(0, COL_DESC, "Description");
                c1ProviderResources.SetData(0, COL_STARTTIME, "Start Time");
                c1ProviderResources.SetData(0, COL_ENDTIME, "End Time");

                DateTimePicker timePicker = new DateTimePicker();
                timePicker.Format = DateTimePickerFormat.Custom;
                timePicker.Value = System.DateTime.Now;
                timePicker.CustomFormat = "hh:mm tt";
                timePicker.ShowUpDown = true;
                
                
                c1ProviderResources.Cols[COL_ID].Visible = false;
                c1ProviderResources.Cols[COL_CODE].Visible = true;
                c1ProviderResources.Cols[COL_DESC].Visible = true;
                c1ProviderResources.Cols[COL_STARTTIME].Visible = true;
                c1ProviderResources.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_ProviderResources.Width - 2);

                c1ProviderResources.Cols[COL_ID].Width = 0;
                c1ProviderResources.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.23);
                c1ProviderResources.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
                c1ProviderResources.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1ProviderResources.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1ProviderResources.AllowEditing = true;
                c1ProviderResources.Cols[COL_ID].AllowEditing = false;
                c1ProviderResources.Cols[COL_CODE].AllowEditing = false;
                c1ProviderResources.Cols[COL_DESC].AllowEditing = false;
                c1ProviderResources.Cols[COL_STARTTIME].AllowEditing = true;
                c1ProviderResources.Cols[COL_ENDTIME].AllowEditing = true;
                
                c1ProviderResources.Cols[COL_STARTTIME].Editor = timePicker;
                c1ProviderResources.Cols[COL_ENDTIME].Editor = timePicker;

                //SHUBHANGI 20110314 TO RESOLVED ISSUE 8842
                c1ProviderResources.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region Provider ProblemType

                c1ProviderProblemType.Rows.Count = 1;
                c1ProviderProblemType.Rows.Fixed = 1;
                c1ProviderProblemType.Cols.Count = COL_COLUMNCOUNT;
                c1ProviderProblemType.Cols.Fixed = 0;

                c1ProviderProblemType.SetData(0, COL_ID, "ID");
                c1ProviderProblemType.SetData(0, COL_CODE, "Code");
                c1ProviderProblemType.SetData(0, COL_DESC, "Description");
                c1ProviderProblemType.SetData(0, COL_STARTTIME, "Start Time");
                c1ProviderProblemType.SetData(0, COL_ENDTIME, "End Time");
                
                             
                c1ProviderProblemType.Cols[COL_STARTTIME].Editor = timePicker;
                c1ProviderProblemType.Cols[COL_ENDTIME].Editor = timePicker;
                
                c1ProviderProblemType.Cols[COL_ID].Visible = false;
                c1ProviderProblemType.Cols[COL_CODE].Visible = true;
                c1ProviderProblemType.Cols[COL_DESC].Visible = true;
                c1ProviderProblemType.Cols[COL_STARTTIME].Visible = true;
                c1ProviderProblemType.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_ProviderProblemType.Width - 2);

                c1ProviderProblemType.Cols[COL_ID].Width = 0;
                c1ProviderProblemType.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.23);
                c1ProviderProblemType.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
                c1ProviderProblemType.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1ProviderProblemType.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1ProviderProblemType.AllowEditing = true;
                c1ProviderProblemType.Cols[COL_ID].AllowEditing = false;
                c1ProviderProblemType.Cols[COL_CODE].AllowEditing = false;
                c1ProviderProblemType.Cols[COL_DESC].AllowEditing = false;
                c1ProviderProblemType.Cols[COL_STARTTIME].AllowEditing = true;
                c1ProviderProblemType.Cols[COL_ENDTIME].AllowEditing = true;

                c1ProviderProblemType.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region Provider Users

                c1ProviderUsers.Rows.Count = 1;
                c1ProviderUsers.Rows.Fixed = 1;
                c1ProviderUsers.Cols.Count = COL_COLUMNCOUNT;
                c1ProviderUsers.Cols.Fixed = 0;

                c1ProviderUsers.SetData(0, COL_ID, "ID");
                c1ProviderUsers.SetData(0, COL_CODE, "Code");
                c1ProviderUsers.SetData(0, COL_DESC, "Description");
                c1ProviderUsers.SetData(0, COL_STARTTIME, "Start Time");
                c1ProviderUsers.SetData(0, COL_ENDTIME, "End Time");

                c1ProviderUsers.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1ProviderUsers.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1ProviderUsers.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1ProviderUsers.Cols[COL_ENDTIME].Format = "hh:mm tt";

                c1ProviderUsers.Cols[COL_ID].Visible = false;
                c1ProviderUsers.Cols[COL_CODE].Visible = true;
                c1ProviderUsers.Cols[COL_DESC].Visible = true;
                c1ProviderUsers.Cols[COL_STARTTIME].Visible = true;
                c1ProviderUsers.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_ProviderUsers.Width - 2);

                c1ProviderUsers.Cols[COL_ID].Width = 0;
                c1ProviderUsers.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.23);
                c1ProviderUsers.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
                c1ProviderUsers.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1ProviderUsers.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1ProviderUsers.AllowEditing = true;
                c1ProviderUsers.Cols[COL_ID].AllowEditing = false;
                c1ProviderUsers.Cols[COL_CODE].AllowEditing = false;
                c1ProviderUsers.Cols[COL_DESC].AllowEditing = false;
                c1ProviderUsers.Cols[COL_STARTTIME].AllowEditing = true;
                c1ProviderUsers.Cols[COL_ENDTIME].AllowEditing = true;

                #endregion

                #region Resources

                c1Resources.Rows.Count = 1;
                c1Resources.Rows.Fixed = 1;
                c1Resources.Cols.Count = COL_COLUMNCOUNT;
                c1Resources.Cols.Fixed = 0;

                c1Resources.SetData(0, COL_ID, "ID");
                c1Resources.SetData(0, COL_CODE, "Code");
                c1Resources.SetData(0, COL_DESC, "Description");
                c1Resources.SetData(0, COL_STARTTIME, "Start Time");
                c1Resources.SetData(0, COL_ENDTIME, "End Time");

                c1Resources.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1Resources.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1Resources.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1Resources.Cols[COL_ENDTIME].Format = "hh:mm tt";

                c1Resources.Cols[COL_ID].Visible = false;
                c1Resources.Cols[COL_CODE].Visible = true;
                c1Resources.Cols[COL_DESC].Visible = true;
                c1Resources.Cols[COL_STARTTIME].Visible = true;
                c1Resources.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_Resources.Width - 2);

                c1Resources.Cols[COL_ID].Width = 0;
                c1Resources.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.23);
                c1Resources.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
                c1Resources.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1Resources.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1Resources.AllowEditing = true;
                c1Resources.Cols[COL_ID].AllowEditing = false;
                c1Resources.Cols[COL_CODE].AllowEditing = false;
                c1Resources.Cols[COL_DESC].AllowEditing = false;
                c1Resources.Cols[COL_STARTTIME].AllowEditing = true;
                c1Resources.Cols[COL_ENDTIME].AllowEditing = true;
                //sHUBHANGI
                //c1Resources.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Resources.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region Blocked  Resources

                c1BlockedResources.Rows.Count = 1;
                c1BlockedResources.Rows.Fixed = 1;
                c1BlockedResources.Cols.Count = COL_COLUMNCOUNT;
                c1BlockedResources.Cols.Fixed = 0;

                c1BlockedResources.SetData(0, COL_ID, "ID");
                c1BlockedResources.SetData(0, COL_CODE, "Code");
                c1BlockedResources.SetData(0, COL_DESC, "Description");
                c1BlockedResources.SetData(0, COL_STARTTIME, "Start Time");
                c1BlockedResources.SetData(0, COL_ENDTIME, "End Time");

                c1BlockedResources.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1BlockedResources.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1BlockedResources.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1BlockedResources.Cols[COL_ENDTIME].Format = "hh:mm tt";

                c1BlockedResources.Cols[COL_ID].Visible = false;
                c1BlockedResources.Cols[COL_CODE].Visible = true;
                c1BlockedResources.Cols[COL_DESC].Visible = true;
                c1BlockedResources.Cols[COL_STARTTIME].Visible = true;
                c1BlockedResources.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_BlockedResources.Width - 2);

                c1BlockedResources.Cols[COL_ID].Width = 0;
                c1BlockedResources.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.23);
                c1BlockedResources.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.28);
                c1BlockedResources.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1BlockedResources.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1BlockedResources.AllowEditing = true;
                c1BlockedResources.Cols[COL_ID].AllowEditing = false;
                c1BlockedResources.Cols[COL_CODE].AllowEditing = false;
                c1BlockedResources.Cols[COL_DESC].AllowEditing = false;
                c1BlockedResources.Cols[COL_STARTTIME].AllowEditing = true;
                c1BlockedResources.Cols[COL_ENDTIME].AllowEditing = true;
                //sHUBHANGI
                //c1BlockedResources.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1BlockedResources.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                #endregion

                #region Blocked  Provider

                c1BlockedProviders.Rows.Count = 1;
                c1BlockedProviders.Rows.Fixed = 1;
                c1BlockedProviders.Cols.Count = COL_COLUMNCOUNT;
                c1BlockedProviders.Cols.Fixed = 0;

                c1BlockedProviders.SetData(0, COL_ID, "ID");
                c1BlockedProviders.SetData(0, COL_CODE, "Code");
                c1BlockedProviders.SetData(0, COL_DESC, "Name");
                c1BlockedProviders.SetData(0, COL_STARTTIME, "Start Time");
                c1BlockedProviders.SetData(0, COL_ENDTIME, "End Time");

                c1BlockedProviders.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1BlockedProviders.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1BlockedProviders.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1BlockedProviders.Cols[COL_ENDTIME].Format = "hh:mm tt";

                c1BlockedProviders.Cols[COL_ID].Visible = false;
                c1BlockedProviders.Cols[COL_CODE].Visible = true;
                c1BlockedProviders.Cols[COL_DESC].Visible = true;
                c1BlockedProviders.Cols[COL_STARTTIME].Visible = true;
                c1BlockedProviders.Cols[COL_ENDTIME].Visible = true;

                _width = (pnlCriteria_BlockedProviders.Width - 5);

                c1BlockedProviders.Cols[COL_ID].Width = 0;
                c1BlockedProviders.Cols[COL_CODE].Width = 0;
                c1BlockedProviders.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.50);
                c1BlockedProviders.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.24);
                c1BlockedProviders.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.24);

                c1BlockedProviders.AllowEditing = true;
                c1BlockedProviders.Cols[COL_ID].AllowEditing = false;
                c1BlockedProviders.Cols[COL_CODE].AllowEditing = false;
                c1BlockedProviders.Cols[COL_DESC].AllowEditing = false;
                c1BlockedProviders.Cols[COL_STARTTIME].AllowEditing = true;
                c1BlockedProviders.Cols[COL_ENDTIME].AllowEditing = true;

                #endregion
                c1BlockedProviders.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1BlockedResources.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ProviderProblemType.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ProviderResources.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ProviderUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void DesignRecurrenceGrid()
        {
            try
            {
                int _width;

                #region Provider Resources
                c1Recurrence.Rows.Count = 1;
                c1Recurrence.Rows.Fixed = 1;
                c1Recurrence.Cols.Count = 5;
                c1Recurrence.Cols.Fixed = 0;

                c1Recurrence.SetData(0, 0, "No. ");
                c1Recurrence.SetData(0, 1, "Date ");
                c1Recurrence.SetData(0, 2, "Start Time");
                c1Recurrence.SetData(0, 3, "End Time");
                c1Recurrence.SetData(0, 4, "Duration");

           
                _width = (pnlShowRecurrence.Width - 2);
                
                c1Recurrence.Cols[0].Width = Convert.ToInt32(_width * 0.10);
                c1Recurrence.Cols[1].Width = Convert.ToInt32(_width * 0.20);
                c1Recurrence.Cols[2].Width = Convert.ToInt32(_width * 0.20);
                c1Recurrence.Cols[3].Width = Convert.ToInt32(_width * 0.20);
                c1Recurrence.Cols[4].Width = Convert.ToInt32(_width * 0.20);

                c1Recurrence.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Recurrence.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Recurrence.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Recurrence.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Recurrence.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Recurrence.AllowEditing = false;
                c1Recurrence.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        #endregion

        #region "Browse Resources & problem Type"

        //Browse Provider --> Problem Type
        private void btnBrowseProviderProblemType_Click(object sender, EventArgs e)
        {
            try
            {
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
                            

                        }
                        catch
                        {
                        }
                        oListControl.Dispose();
                        oListControl = null;
                   

                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.Procedures, true, pnlListControl.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Problem Type";
                _CurrentListControlType = "Problem Type";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                pnlListControl.Controls.Add(oListControl);
               
                pnlListControl.Visible = true;
               
                ts_Commands.Enabled = false;

                for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                {
                    gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                    oItem.ID = Convert.ToInt64(c1ProviderProblemType.GetData(i, COL_ID));
                    oItem.Code = "";
                    oItem.Description = Convert.ToString(c1ProviderProblemType.GetData(i, COL_DESC)); // Procedure Name
                    oListControl.SelectedItems.Add(oItem);
                    oItem.Dispose();
                    oItem = null;
                }

                for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                {
                    gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                    oItem.ID = Convert.ToInt64(c1ProviderResources.GetData(i, COL_ID));
                    oItem.Code = "";
                    oItem.Description = Convert.ToString(c1ProviderResources.GetData(i, COL_DESC)); // Procedure Name
                    oListControl.SelectedItems.Add(oItem);
                    oItem.Dispose();
                    oItem = null;
                }

                
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
                oListControl.OpenControl();
                
                pnlScedules.Visible = false;
                pnlRecurrenceContainer.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Browse Provider -- > Resources
        private void btnBrowseProviderResources_Click(object sender, EventArgs e)
        {
            try
            {
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
                        

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.Resources, true, pnlListControl.Width);
                oListControl.ControlHeader = "Resources";
                _CurrentListControlType = "Provider Resources";

                for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(c1ProviderResources.GetData(i, COL_ID)), Convert.ToString(c1ProviderResources.GetData(i, COL_DESC)));
                }

                oListControl.ClinicID = _ClinicID;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ts_Commands.Enabled = false;

                pnlListControl.Controls.Add(oListControl);
                pnlListControl.Visible = true;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

                pnlScedules.Visible = false;
                pnlListControl.Visible = true;
                pnlRecurrenceContainer.Visible = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Browse Resources 
        private void btnBrowseResource_Click(object sender, EventArgs e)
        {
            try
            {
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
                        

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.Resources, true, pnlListControl.Width);
                oListControl.ControlHeader = "Resources";
                _CurrentListControlType = "Resources";
                oListControl.ClinicID = _ClinicID;

                for (int i = 1; i < c1Resources.Rows.Count; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(c1Resources.GetData(i, COL_ID)), Convert.ToString(c1Resources.GetData(i, COL_DESC)));                    
                }
               
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ts_Commands.Enabled = false;

                pnlListControl.Controls.Add(oListControl);
                pnlListControl.Visible = true;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

                pnlScedules.Visible = false;
                pnlListControl.Visible = true;
                pnlRecurrenceContainer.Visible = false;

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Browse Blocked Resources
        private void btnBrowseBlockedResources_Click(object sender, EventArgs e)
        {
            try
            {
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
                         

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.Resources, true, pnlListControl.Width);
                oListControl.ControlHeader = "Resources";
                _CurrentListControlType = "Blocked Resources";
                oListControl.ClinicID = _ClinicID;

                for (int i = 1; i < c1BlockedResources.Rows.Count; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(c1BlockedResources.GetData(i, COL_ID)), Convert.ToString(c1BlockedResources.GetData(i, COL_DESC)));
                }

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ts_Commands.Enabled = false;

                pnlListControl.Controls.Add(oListControl);
                pnlListControl.Visible = true;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

                pnlScedules.Visible = false;
                pnlListControl.Visible = true;
                pnlRecurrenceContainer.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Browse Blocked Providers
        private void btnBrowseBlockedProvider_Click(object sender, EventArgs e)
        {
            try
            {
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
                         

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.Providers, true, pnlListControl.Width);
                oListControl.ControlHeader = "Providers";
                _CurrentListControlType = "Blocked Providers";
                oListControl.ClinicID = _ClinicID;

                for (int i = 1; i < c1BlockedProviders.Rows.Count; i++)
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(c1BlockedProviders.GetData(i, COL_ID)), Convert.ToString(c1BlockedProviders.GetData(i, COL_DESC)));
                }

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                ts_Commands.Enabled = false;

                pnlListControl.Controls.Add(oListControl);
                pnlListControl.Visible = true;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

                pnlScedules.Visible = false;
                pnlListControl.Visible = true;
                pnlRecurrenceContainer.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Clear Provider --> Problem Type
        private void btnClearProviderProblemType_Click(object sender, EventArgs e)
        {
            //c1ProviderProblemType.Rows.Count = 1;
            // solving TFS issue id-3951
            if (c1ProviderProblemType != null && c1ProviderProblemType.Rows.Count > 0)
            {
                
                    if (c1ProviderProblemType.RowSel > 0)
                    {
                        int _RemoveItem = c1ProviderProblemType.RowSel;
                        c1ProviderProblemType.RemoveItem(_RemoveItem);
                    }

                
            }
            // end
        }

        //Clear Provider -- > Resources
        private void btnClearProviderResources_Click(object sender, EventArgs e)
        {
            //c1ProviderResources.Rows.Count = 1;
            // solving TFS issue id-3952
            if (c1ProviderResources != null && c1ProviderResources.Rows.Count > 0)
            {
               
              
                    if (c1ProviderResources.RowSel > 0)
                    {
                        int _RemoveItem = c1ProviderResources.RowSel;
                        c1ProviderResources.RemoveItem(_RemoveItem);
                    }
              
            }
            // end
        }

        //Clear Resources
        private void btnClearResource_Click(object sender, EventArgs e)
        {
            c1Resources.Rows.Count = 1;
        }

        //Clear Blocked Resources
        private void btnClearBlockedResources_Click(object sender, EventArgs e)
        {
           // c1BlockedResources.Rows.Count = 1;
            // solving TFS issue id-3951
            if (c1BlockedResources != null && c1BlockedResources.Rows.Count > 0)
            {
                
                
                    if (c1BlockedResources.RowSel > 0)
                    {
                        int _RemoveItem = c1BlockedResources.RowSel;
                        c1BlockedResources.RemoveItem(_RemoveItem);
                    }
                
            }
            // end

        }

        //Clear Blocked Providers
        private void btnClearBlockedProvider_Click(object sender, EventArgs e)
        {
            //c1BlockedProviders.Rows.Count = 1;
            // solving TFS issue id-3952
            if (c1BlockedProviders != null && c1BlockedProviders.Rows.Count > 0)
            {
                
                
                    if (c1BlockedProviders.RowSel > 0)
                    {
                        int _RemoveItem = c1BlockedProviders.RowSel;
                        c1BlockedProviders.RemoveItem(_RemoveItem);
                    }
                
            }
            // end

        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {               
                switch (_CurrentListControlType)
                {
                    case "Problem Type":
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
                                    if (rbSimple.Checked == true)
                                    {
                                        c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                        c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                    }
                                    else
                                    {
                                        c1ProviderProblemType.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                        c1ProviderProblemType.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                    }

                                    if (oListControl.SelectedItems[i].SubItems.Count > 0)
                                    {
                                        
                                        for (int k = 0; k < oListControl.SelectedItems[i].SubItems.Count; k++)
                                        {
                                            if (!IsResourceAdded(oListControl.SelectedItems[i].SubItems[k].ID, oListControl.SelectedItems[i].SubItems[k].Code))
                                            {
                                                c1ProviderResources.Rows.Add();
                                                Int32 ResourcesRowIndex = c1ProviderResources.Rows.Count - 1;
                                                c1ProviderResources.SetData(ResourcesRowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].ID));
                                                c1ProviderResources.SetData(ResourcesRowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].Code));
                                                c1ProviderResources.SetData(ResourcesRowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].SubItems[k].Description));
                                                if (rbSimple.Checked == true)
                                                {
                                                    c1ProviderResources.SetData(ResourcesRowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                                    c1ProviderResources.SetData(ResourcesRowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                                }
                                                else
                                                {

                                                    c1ProviderResources.SetData(ResourcesRowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                                    c1ProviderResources.SetData(ResourcesRowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (c1ProviderResources.Rows.Count > 0)
                                {
                                    c1ProviderResources.Rows.Count = 1;
                                }
                            }
                        }
                        break;
                    case "Provider Resources":
                        {
                            c1ProviderResources.Rows.Count = 1;
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                                {
                                    if (!IsResourceAdded(oListControl.SelectedItems[i].ID, oListControl.SelectedItems[i].Code))
                                    {
                                        c1ProviderResources.Rows.Add();
                                        Int32 RowIndex = c1ProviderResources.Rows.Count - 1;
                                        c1ProviderResources.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                        c1ProviderResources.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                        c1ProviderResources.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));

                                        if (rbSimple.Checked == true)
                                        {
                                            c1ProviderResources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                            c1ProviderResources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                        }
                                        else
                                        {

                                            c1ProviderResources.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                            c1ProviderResources.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Resources":
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                c1Resources.Rows.Count = 1;
                                for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                                {
                                    c1Resources.Rows.Add();
                                                                      
                                    Int32 RowIndex = c1Resources.Rows.Count - 1;
                                    c1Resources.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                    c1Resources.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                    c1Resources.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));


                                    
                                    if (rbSimple.Checked == true)
                                    {
                                        c1Resources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                        c1Resources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                    }

                                    else
                                    {
                                        c1Resources.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                        c1Resources.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                    }
                                }
                            }
                        }
                        break;
                    case "Blocked Resources":
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                c1BlockedResources.Rows.Count = 1;
                                for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                                {
                                    c1BlockedResources.Rows.Add();
                                    Int32 RowIndex = c1BlockedResources.Rows.Count - 1;
                                    c1BlockedResources.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                    c1BlockedResources.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                    c1BlockedResources.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));
                                    if (rbSimple.Checked == true)
                                    {
                                        c1BlockedResources.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                        c1BlockedResources.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                    }
                                    else
                                    {
                                        c1BlockedResources.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                        c1BlockedResources.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                    }
                                }
                            }
                        }
                        break;
                    case "Blocked Providers":
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                c1BlockedProviders.Rows.Count = 1;
                                for (int i = 0; i < oListControl.SelectedItems.Count; i++)
                                {
                                    c1BlockedProviders.Rows.Add();
                                    Int32 RowIndex = c1BlockedProviders.Rows.Count - 1;
                                    c1BlockedProviders.SetData(RowIndex, COL_ID, Convert.ToString(oListControl.SelectedItems[i].ID));
                                    c1BlockedProviders.SetData(RowIndex, COL_CODE, Convert.ToString(oListControl.SelectedItems[i].Code));
                                    c1BlockedProviders.SetData(RowIndex, COL_DESC, Convert.ToString(oListControl.SelectedItems[i].Description));
                                    if (rbSimple.Checked == true)
                                    {
                                        c1BlockedProviders.SetData(RowIndex, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                                        c1BlockedProviders.SetData(RowIndex, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                                    }
                                    else
                                    {
                                        c1BlockedProviders.SetData(RowIndex, COL_STARTTIME, dtpRec_StartTime.Value.ToShortTimeString());
                                        c1BlockedProviders.SetData(RowIndex, COL_ENDTIME, dtpRec_EndTime.Value.ToShortTimeString());
                                    }
                                }
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
            finally
            {
                pnlScedules.Visible = true;
                pnlListControl.Visible = false;
                pnlRecurrenceContainer.Visible = false;
                ts_Commands.Enabled = true;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
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
                        

                    }
                    catch
                    {
                    }
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                pnlScedules.Visible = true;
                pnlListControl.Visible = false;
                pnlRecurrenceContainer.Visible = false;
                ts_Commands.Enabled = true;
            }
        }

        private bool IsResourceAdded(Int64 id, string code)
        {
            bool IsAdded = false;
            if (c1ProviderResources != null)
            {
                Int64 _id = 0;
                string _code = "";

                for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                {
                    _id = Convert.ToInt64(c1ProviderResources.GetData(i, COL_ID));
                    _code = Convert.ToString(c1ProviderResources.GetData(i, COL_CODE));

                    if (_id.Equals(id) && _code.Equals(code))
                    {
                        IsAdded = true;
                        break;
                    }
                }
            }
            return IsAdded;
        }
        #endregion

        #region "Schedule Date Time selection Events"

        private void numApp_DateTime_Duration_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numApp_DateTime_Duration.Value) * 600000000);

                string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
                dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void dtpApp_DateTime_StartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numApp_DateTime_Duration.Value) * 600000000);

                string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
                dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void dtpApp_DateTime_EndTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan();

                _tempTS = dtpApp_DateTime_EndTime.Value.Subtract(dtpApp_DateTime_StartTime.Value);

                //if (_tempTS.TotalMinutes > 0)
                if (_tempTS.TotalMinutes >= Convert.ToDouble(numApp_DateTime_Duration.Minimum) && Convert.ToDouble(_tempTS.TotalMinutes) <= Convert.ToDouble(numApp_DateTime_Duration.Maximum))
                {
                    numApp_DateTime_Duration.Value = Convert.ToDecimal(_tempTS.TotalMinutes);
                }
                else
                {
                    dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(30);
                    //numApp_DateTime_Duration.Value = 0; 
                }
                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void numRec_Duration_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numRec_Duration.Value) * 600000000);

                string _ActTime = dtpRec_StartTime.Value.ToShortTimeString();
                dtpRec_StartTime.Value = Convert.ToDateTime(string.Format(dtpRec_Range_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpRec_Range_StartDate.Value = Convert.ToDateTime(string.Format(dtpRec_Range_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpRec_EndTime.Value = dtpRec_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void dtpRec_StartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numRec_Duration.Value) * 600000000);

                string _ActTime = dtpRec_StartTime.Value.ToShortTimeString();
                dtpRec_StartTime.Value = Convert.ToDateTime(string.Format(dtpRec_Range_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));                
                dtpRec_Range_StartDate.Value = Convert.ToDateTime(string.Format(dtpRec_Range_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpRec_EndTime.Value = dtpRec_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void dtpRec_EndTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                  TimeSpan _tempTS = new TimeSpan();

                _tempTS = dtpRec_EndTime.Value.Subtract(dtpRec_StartTime.Value);

                //if (_tempTS.TotalMinutes > 0)
                if (_tempTS.TotalMinutes >= Convert.ToDouble(numRec_Duration.Minimum) && Convert.ToDouble(_tempTS.TotalMinutes) <= Convert.ToDouble(numRec_Duration.Maximum))
                {
                    numRec_Duration.Value = Convert.ToDecimal(_tempTS.TotalMinutes);
                }
                else
                {
                    dtpRec_EndTime.Value = dtpRec_StartTime.Value.AddMinutes(30);
                    //numRec_Duration.Value = 0; 
                }
                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void ValidatePRUTiming()
        {
            try
            {
                //if PRU Start time is not between Schedule Time 
                //   then Make Schedule Start time as PRU start time 
                //if PRU End time is not between Schedule Time 
                //   then Make Schedule End time as PRU End time 

                Int32 _dtStartTime;
                Int32 _dtEndTime;

                //if (rbSimple.Checked == true)
                 if (rbRecurrence.Checked == false || _SetScheduleParameter.ModifySingleAppointmentFromReccurence == true)
                {
                    _dtStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                    _dtEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToShortTimeString());

                
                }
                else
                {
                   
                    
                    _dtStartTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_StartTime.Value.ToShortTimeString());
                    _dtEndTime = gloDateMaster.gloTime.TimeAsNumber(dtpRec_EndTime.Value.ToShortTimeString());
                }

                // Provider Schedule
                if (rbProviderSchedule.Checked == true)
                {
                    
                    //Provider -- > Problem Type
                    for (int i = 1; i < c1ProviderProblemType.Rows.Count; i++)
                    {
                        // Change Time only if outside the schedule
 
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderProblemType.GetData(i,COL_STARTTIME)).ToShortTimeString()) ;
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderProblemType.GetData(i, COL_ENDTIME)).ToShortTimeString());
                                     
                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)                        
                        //    c1ProviderProblemType.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now,_dtStartTime));                              
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1ProviderProblemType.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        // Change Time by Start & End Time of Schedule time 
                        
                        c1ProviderProblemType.SetData(i, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                        c1ProviderProblemType.SetData(i, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());

                        //c1ProviderProblemType.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //c1ProviderProblemType.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                    }

                    for (int i = 1; i < c1ProviderResources.Rows.Count; i++)
                    {
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderResources.GetData(i, COL_STARTTIME)).ToShortTimeString());
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderResources.GetData(i, COL_ENDTIME)).ToShortTimeString());

                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)                        
                        //    c1ProviderResources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1ProviderResources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                      
                        //c1ProviderResources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //c1ProviderResources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        c1ProviderResources.SetData(i, COL_STARTTIME, dtpApp_DateTime_StartTime.Value.ToShortTimeString());
                        c1ProviderResources.SetData(i, COL_ENDTIME, dtpApp_DateTime_EndTime.Value.ToShortTimeString());
                        
                    }

                    for (int i = 1; i < c1ProviderUsers.Rows.Count; i++)
                    {
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_STARTTIME)).ToShortTimeString());
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1ProviderUsers.GetData(i, COL_ENDTIME)).ToShortTimeString());

                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)
                        //    c1ProviderUsers.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1ProviderUsers.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        c1ProviderUsers.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        c1ProviderUsers.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                    }
                }

                // Resource Schedule
                if(rbResourceSchedule.Checked == true)
                {
                    for (int i = 1; i < c1Resources.Rows.Count; i++)
                    {
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1Resources.GetData(i, COL_STARTTIME)).ToShortTimeString());
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1Resources.GetData(i, COL_ENDTIME)).ToShortTimeString());

                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)
                        //    c1Resources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1Resources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        c1Resources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        c1Resources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                    }
                }

                // Block Schedule
                if (rbBlockedSchedule.Checked == true)
                {
                    for (int i = 1; i < c1BlockedResources.Rows.Count; i++)
                    {
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1BlockedResources.GetData(i, COL_STARTTIME)).ToShortTimeString());
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1BlockedResources.GetData(i, COL_ENDTIME)).ToShortTimeString());

                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)
                        //    c1BlockedResources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1BlockedResources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        c1BlockedResources.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        c1BlockedResources.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                    }

                    for (int i = 1; i < c1BlockedProviders.Rows.Count; i++)
                    {
                        //Int32 _PRUStartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_STARTTIME)).ToShortTimeString());
                        //Int32 _PRUEndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToDateTime(c1BlockedProviders.GetData(i, COL_ENDTIME)).ToShortTimeString());

                        //if (_PRUStartTime < _dtStartTime || _PRUStartTime > _dtEndTime)
                        //    c1BlockedProviders.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        //if (_PRUEndTime > _dtEndTime || _PRUEndTime < _dtStartTime)
                        //    c1BlockedProviders.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));

                        c1BlockedProviders.SetData(i, COL_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtStartTime));
                        c1BlockedProviders.SetData(i, COL_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, _dtEndTime));
                    }


                  
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion

        #region Recurrence Range Selection Methods & Events

        private void ShowRecurrence()
        {
            try
            {
                DesignRecurrenceGrid();
                FindRecurrence();
                TimeSpan tsDuration = dtpRec_EndTime.Value.Subtract(dtpRec_StartTime.Value);   
                for (int i = 0; i < oFindCriteria.Dates.Count; i++)
                {
                    c1Recurrence.Rows.Add();
                    Int32 RowIndex = c1Recurrence.Rows.Count - 1;
                    c1Recurrence.SetData(RowIndex, 0, i + 1);
                    c1Recurrence.SetData(RowIndex, 1, Convert.ToDateTime(oFindCriteria.Dates[i]).ToShortDateString());
                    c1Recurrence.SetData(RowIndex, 2, dtpRec_StartTime.Value.ToShortTimeString());
                    c1Recurrence.SetData(RowIndex, 3, dtpRec_EndTime.Value.ToShortTimeString());
                    c1Recurrence.SetData(RowIndex, 4, tsDuration.TotalMinutes.ToString() + " Min");
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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
                    if (numRec_Pattern_Daily_EveryDay.Text.Trim().ToString() == "")
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Minimum);
                    }
                    else
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Value);
                    }
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                else
                {
                   // oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(numRec_Pattern_Daily_EveryDay.Minimum);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryWeekday;
                }
            }
            //Weekly
            else if (rbRec_Pattern_Weekly.Checked == true)
            {
                oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Weekly;

                if (numRec_Pattern_Weekly_WeekOn.Text.Trim().ToString() == "")
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Minimum);
                }
                else
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(numRec_Pattern_Weekly_WeekOn.Value);
                }

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



                    if (numRec_Pattern_Monthly_Day_Day.Value.ToString() == "")
                    {
                        numRec_Pattern_Monthly_Day_Day.Value = 5;
                        numRec_Pattern_Monthly_Day_Day.Text = "5";
                        //numRec_Pattern_Monthly_Day_Day.Value = numRec_Pattern_Monthly_Day_Day.Minimum;
                    }

                    if (numRec_Pattern_Monthly_Day_Day.Text.Trim().ToString() == "")
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Minimum);
                    }
                    else
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Value);
                    }

                    if (numRec_Pattern_Monthly_Day_Month.Text.Trim().ToString() == "")
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Minimum);
                    }
                    else
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Month.Value);
                    }
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = DayWeekday.day;

                }
                else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;

                   // oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = 0;                   
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Monthly_Day_Day.Minimum);

                    if (numRec_Pattern_Monthly_Criteria_Month.Text.Trim().ToString() == "")
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Minimum);
                    }
                    else
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(numRec_Pattern_Monthly_Criteria_Month.Value);
                    }
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

                    if (numRec_Pattern_Yearly_Every_MonthDay.Text.Trim().ToString() == "")
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Minimum);
                    }
                    else
                    {
                        oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Value);
                    }

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (MonthRange)Enum.Parse(typeof(MonthRange), cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString()); //(MonthRange)cmbRec_Pattern_Yearly_Every_Month.SelectedItem.ToString().GetHashCode();

                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = FirstLastCriteria.first;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = DayWeekday.day;

                }
                else if (rbRec_Pattern_Yearly_Criteria.Checked == true)
                {
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = RecurrencePatternFlag.SelectedCriteria;

                    //oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = 0;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(numRec_Pattern_Yearly_Every_MonthDay.Minimum);
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
                MessageBox.Show("Error while finding range.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _IsPatternFinding = false;
        }


        private void ShowHideToolStripButtons()
        {
            if (_SetScheduleParameter.AddTrue_ModifyFalse_Flag == true)
            {
                tsb_OK.Visible = true;
                tsb_Cancel.Visible = true;
                tsb_Recurrence.Visible = true;
                tsb_ShowRecurrence.Visible = false;
                tsb_ApplyRecurrence.Visible = false;
                tsb_CancelRecurrence.Visible = false;
            }
            else
            {
                if (_SetScheduleParameter.ModifyAppointmentMethod == SingleRecurrence.Single)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;                    
                    tsb_Recurrence.Visible = false;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
                else if (_SetScheduleParameter.ModifyAppointmentMethod == SingleRecurrence.Recurrence)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;
                    tsb_Recurrence.Visible = true;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
                else if (_SetScheduleParameter.ModifyAppointmentMethod == SingleRecurrence.SingleInRecurrence)
                {
                    tsb_OK.Visible = true;
                    tsb_Cancel.Visible = true;
                    tsb_Recurrence.Visible = false;
                    tsb_ShowRecurrence.Visible = false;
                    tsb_ApplyRecurrence.Visible = false;
                    tsb_CancelRecurrence.Visible = false;
                }
            }
        }


        #region "Pattern Selection"

        private void rbRec_Pattern_Daily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Pattern_Daily.Checked == true)
            {
                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Yearly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
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
                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Yearly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
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
                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbRec_Pattern_Yearly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
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
                rbRec_Pattern_Daily.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Weekly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                rbRec_Pattern_Monthly.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
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
        }

        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbSimple.Checked == true)
                {
                    //pnlRecurrenceContainer.Visible = false;
                    //pnlScedules.Visible = true;
                    //pnlListControl.Visible = false;
                    //pnlScedules.BringToFront();
                    
                    lblApp_Recurrence_Time.Text = "";
                    lblApp_Recurrence_Time.SendToBack();
                    lblApp_Recurrence_Time.Visible = false;
                }
                else
                {
                    //pnlRecurrenceContainer.Visible = true;
                    //pnlScedules.Visible = false;
                    //pnlListControl.Visible = false;
                    //pnlScedules.BringToFront();

                    string _RecText = "";
                    _RecText = "Start Date : " + dtpRec_Range_StartDate.Value.ToShortDateString() + "   End Date : " + dtpRec_Range_EndBy.Value.ToShortDateString() + " Start Time : " + dtpRec_StartTime.Value.ToShortTimeString() + "  Duration : " + numRec_Duration.Value.ToString() + "  Occurences : " + numRec_Range_EndAfterOccurence.Value.ToString();
                    lblApp_Recurrence_Time.Text = _RecText;
                    lblApp_Recurrence_Time.BringToFront();
                    lblApp_Recurrence_Time.Visible = true;
                }
                ValidatePRUTiming();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbRec_Range_EndAfterOccurence_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbRecurrence_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region "Event which affect on changeing recurrence criteria"
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
                return;
            }
        }

        private void numRec_Pattern_Monthly_Day_Day_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Monthly_Day_Month_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void rbRec_Pattern_Monthly_Criteria_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Monthly_Criteria_Month_ValueChanged(object sender, EventArgs e)
        {
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
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void numRec_Pattern_Yearly_Every_MonthDay_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void rbRec_Pattern_Yearly_Criteria_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }

        private void cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
            }
        }
        #endregion
        #endregion

        #region "Pattern Range Selection"
        private void dtpRec_Range_StartDate_ValueChanged(object sender, EventArgs e)
        {
            if (_IsFormLoading == false && _IsPatternChanging == false && _IsPatternFinding == false)
            {
                FindRecurrence();
                return;
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

       

       

       

       #endregion

        private void rbRec_Range_EndBy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRec_Range_EndBy.Checked == true)
            {
                rbRec_Range_EndBy.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRec_Range_EndBy.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbRec_Range_EndAfterOccurence_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbRec_Range_EndAfterOccurence.Checked == true)
            {
                rbRec_Range_EndAfterOccurence.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbRec_Range_EndAfterOccurence.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void cmbBlockType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //commented by Amit B -  No need to show Block Type combo box text in Netes Text box
            //                       only manually entered text will show

            //if (cmbBlockType.SelectedIndex != -1)
            //{
            //    // solving TFS issue id -3958 which overwrite exsting notes when we change BlockType.
            //    if (txtScheduleNote.Text.Contains(cmbBlockType.Text) == false)
            //    {
            //        txtScheduleNote.Text = string.Concat(txtScheduleNote.Text + " ", cmbBlockType.Text);
            //    }
            //}
        }

        private void btnClearProviderResources_Leave(object sender, EventArgs e)
        {
            txtScheduleNote.Focus();
        }

        private void btnClearResource_Leave(object sender, EventArgs e)
        {
            txtScheduleNote.Focus();
        }

        private void btnClearBlockedProvider_Leave(object sender, EventArgs e)
        {
            txtScheduleNote.Focus();
        }

        private void btnClearBlockedResources_Leave(object sender, EventArgs e)
        {
            txtScheduleNote.Focus();
        }

        private void ChkRec_Pattern_Weekly_Saturday_Leave(object sender, EventArgs e)
        {
            dtpRec_Range_StartDate.Focus();
        }

        private void rbRec_Pattern_Daily_EveryWeekday_Leave(object sender, EventArgs e)
        {
            dtpRec_Range_StartDate.Focus();
        }

        private void numRec_Pattern_Daily_EveryDay_Leave(object sender, EventArgs e)
        {
            rbRec_Pattern_Daily_EveryWeekday.Focus();
        }

        private void btnColorCode_Leave(object sender, EventArgs e)
        {
            rbRec_Pattern_Daily.Focus();
        }

    }



    public partial class SetScheduleParameter : IDisposable
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
        public string Department = "";
        public DateTime StartDate = DateTime.Now;
        public DateTime StartTime = DateTime.Now;
        public decimal Duration = 15;
        public Int64 ClinicID = 0;
        public bool LoadParameters = false;
        public Int64 TemplateAllocationMasterID = 0;
        public Int64 TemplateAllocationID = 0;

        #region "Constructors and Destructors"
        public SetScheduleParameter()
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

        ~SetScheduleParameter()
        {
            Dispose(false);
        }

        #endregion
    }
}