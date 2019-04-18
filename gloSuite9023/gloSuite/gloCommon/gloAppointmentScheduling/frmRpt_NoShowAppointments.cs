using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloAppointmentScheduling
{
    public partial class frmRpt_NoShowAppointments : Form
    {
        #region "Declarations"

        String _MessageBoxCaption = string.Empty;
        //Int64 _providerID = 0;
        Int64 _clinicID = 0;
        //String _providerName = "";
        //String _location = "";
        String _databaseConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region  "C1 Constants "
        
        
        private const int COL_PATIENTID = 0;
        private const int COL_PATIENTCODE = 1;
        private const int COL_PATIENTFNAME = 2;
        private const int COL_PATIENTMNAME = 3;
        private const int COL_PATIENTLNAME = 4;
        private const int COL_PATIENTDOB = 5;
        private const int COL_PATIENTPHONE = 6;
        private const int COL_PATIENTSSN = 7;
        private const int COL_APPPROVIDER = 8;
        private const int COL_PATIENTAPPTYPE = 9;
        private const int COL_PATIENTLOCATON = 10;
        private const int COL_APPSTARTDATE = 11;
        private const int COL_APPSTARTTIME = 12;
        private const int COL_APPENDTIME = 13;
        private const int COL_APPDURATION = 14;
        private const int COL_APPNOTES = 15;
        private const int COL_APPSTATUS = 16;
        private const int COL_USERNAME = 17;
        private const int COL_COUNT = 18;

        private void DesignGrid()
        {
            c1Appointments.Rows.Count = 1; 
            c1Appointments.Rows.Fixed = 1;
            c1Appointments.Cols.Count = COL_COUNT;
            c1Appointments.Cols.Fixed = 0;

            c1Appointments.SetData(0, COL_PATIENTID, "Patient ID");
            c1Appointments.SetData(0, COL_PATIENTCODE, "Patient Code");
            c1Appointments.SetData(0, COL_PATIENTFNAME, "First Name");
            c1Appointments.SetData(0, COL_PATIENTMNAME, "MI");
            c1Appointments.SetData(0, COL_PATIENTLNAME, "Last Name");
            c1Appointments.SetData(0, COL_PATIENTDOB, "DOB");
            c1Appointments.SetData(0, COL_PATIENTPHONE, "Phone");
            c1Appointments.SetData(0, COL_PATIENTSSN, "SSN");
            c1Appointments.SetData(0, COL_APPPROVIDER, "Provider");
            c1Appointments.SetData(0, COL_PATIENTAPPTYPE, "App. Type");
            c1Appointments.SetData(0, COL_PATIENTLOCATON, "Location");
            c1Appointments.SetData(0, COL_APPSTARTDATE, "App. Date");
            c1Appointments.SetData(0, COL_APPSTARTTIME, "Start time");
            c1Appointments.SetData(0, COL_APPENDTIME, "End time");
            c1Appointments.SetData(0, COL_APPDURATION, "Duration");
            c1Appointments.SetData(0, COL_APPNOTES, "Notes");

            c1Appointments.SetData(0, COL_APPSTATUS, "Status");
            c1Appointments.SetData(0, COL_USERNAME, "User Name");


            //c1Appointments.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;            
            c1Appointments.AllowEditing = false;
            c1Appointments.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            c1Appointments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

            c1Appointments.Cols[COL_APPSTARTDATE].DataType = typeof(System.DateTime);

            c1Appointments.Cols[COL_PATIENTID].Visible = false;
            c1Appointments.Cols[COL_PATIENTSSN].Visible = false;
            c1Appointments.Cols[COL_USERNAME].Visible = false;

            c1Appointments.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

            //int _pnlWidth = pnlContainer.Width;
            //c1Appointments.Cols[COL_PATIENTID].Width = 0;
            //c1Appointments.Cols[COL_PATIENTCODE].Width = (int) (_pnlWidth*0.12);
            //c1Appointments.Cols[COL_PATIENTFNAME].Width = (int)(_pnlWidth * 0.12);
            //c1Appointments.Cols[COL_PATIENTMNAME].Width = (int)(_pnlWidth * 0.04);
            //c1Appointments.Cols[COL_PATIENTLNAME].Width = (int)(_pnlWidth * 0.12);
            //c1Appointments.Cols[COL_PATIENTLOCATON].Width = (int)(_pnlWidth * 0.10);
            //c1Appointments.Cols[COL_PATIENTAPPTYPE].Width = (int)(_pnlWidth * 0.10);
            //c1Appointments.Cols[COL_PATIENTPROVIDER].Width = (int)(_pnlWidth * 0.15);
            //c1Appointments.Cols[COL_PATIENTDOB].Width = (int)(_pnlWidth * 0.12);
            //c1Appointments.Cols[COL_PATIENTPHONE].Width = (int)(_pnlWidth * 0.12);
            //c1Appointments.Cols[COL_PATIENTSSN].Width = 0;


        }

        #endregion "C1 Constants "

        #region "Contructor"

        public frmRpt_NoShowAppointments(string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseConnectionString = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }



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

        private void frmRpt_NoShowAppointments_Load(object sender, EventArgs e)
        {
            try
            {
                FillControls();
                Fill_FilterDatesCombo();
                ShowReport();

                if (c1Appointments.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillControls()
        {
            gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseConnectionString);
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable _dtProviders = new DataTable();
            DataTable _dtLocations = new DataTable();
            try
            {
                _dtProviders = oResource.GetProviders();
                _dtLocations = oLocation.GetList();

                DataTable dtProviders;
                if (_dtProviders != null && _dtProviders.Rows.Count > 0)
                {
                    dtProviders = new DataTable();
                    dtProviders.Columns.Add("nProviderID");
                    dtProviders.Columns.Add("ProviderName");

                    dtProviders.Clear();
                    dtProviders.Rows.Add(0, "");

                    for (int i = 0; i < _dtProviders.Rows.Count; i++)
                    {
                        dtProviders.Rows.Add(_dtProviders.Rows[i]["nProviderID"], _dtProviders.Rows[i]["ProviderName"]);
                    }

                    cmbProviders.DataSource = dtProviders;
                    cmbProviders.DisplayMember = "ProviderName";
                    cmbProviders.ValueMember = "nProviderID";
                }


                DataTable dtLocations;
                if (_dtLocations != null && _dtLocations.Rows.Count > 0)
                {
                    dtLocations = new DataTable();
                    dtLocations.Columns.Add("nLocationID");
                    dtLocations.Columns.Add("slocation");

                    dtLocations.Clear();
                    dtLocations.Rows.Add(0, "");

                    for (int i = 0; i < _dtLocations.Rows.Count; i++)
                    {
                        dtLocations.Rows.Add(_dtLocations.Rows[i]["nLocationID"], _dtLocations.Rows[i]["slocation"]);
                    }

                    cmbLocation.DataSource = dtLocations;
                    cmbLocation.DisplayMember = "slocation";
                    cmbLocation.ValueMember = "nLocationID";
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }

        #region "Tool Strip button Events"

        private void ts_btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                ShowReport();

                if (c1Appointments.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                } 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

       
        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {

            ExportToExcel(false);

            //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
            //object value = new object();
            //string _DefaultLocationPath = "";
            //string _FilePath = "";
            //bool _Checked = false;
            //try
            //{
            //        //ogloSettings.GetSetting("ExportToDefaultLocation", out value);
            //        //if (value != null)
            //        //{
            //        //    if (value.ToString() != "")
            //        //    {
            //        //        _Checked = Convert.ToBoolean(value);
            //        //    }
            //        //}
            //        //value = null;
            //        //ogloSettings.GetSetting("ExportToDefaultLocationPath", out value);

            //        //if (value != null)
            //        //{
            //        //    _DefaultLocationPath = value.ToString();
            //        //}


            //        gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(Application.StartupPath);
            //        if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
            //        {
            //            _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
            //        }
            //        else
            //        {
            //            _Checked = false;
            //        }
            //        _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
            //        oSettings.Dispose();


            //        if (_DefaultLocationPath != "" && _Checked == true)
            //        {
            //            if (_DefaultLocationPath.EndsWith("\\"))
            //            {

            //                _DefaultLocationPath = _DefaultLocationPath.Replace("\\", "");
            //            }
            //            // If not exist create directory
            //            if (Directory.Exists(_DefaultLocationPath) == false)
            //            {
            //                Directory.CreateDirectory(value.ToString());
            //            }

            //            _FilePath = _DefaultLocationPath + "\\NoShow Appointment";

            //            _FilePath += Convert.ToString(DateTime.Now).Replace(":", "");
            //            _FilePath = _FilePath.Replace("/", "") + ".xls";
            //        }
            //        else
            //        {
            //            FileDialog saveFileDialog = new SaveFileDialog();
            //            saveFileDialog.Filter = "Excel File(.xls)|*.xls";
            //            saveFileDialog.DefaultExt = ".xls";
            //            saveFileDialog.AddExtension = true;
            //            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            //            {
            //                return;
            //            }
            //            _FilePath = saveFileDialog.FileName;
            //        }

            //        c1Appointments.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
            //        MessageBox.Show("File saved successfully.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}

        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            ExportToExcel(true);
        }

        private void tls_btnClose_Click(object sender, EventArgs e)
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

        private void tlsPrintPatientsOn_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        } 

        #endregion

        private void ShowReport()
        {
            DataTable dtAppointments = null;
            DesignGrid(); 

            try
            {
                if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    MessageBox.Show("Please select valid range of Dates.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromDate.Focus();
                    return;
                }

                dtAppointments = new DataTable();

                dtAppointments = GetNoshowAppointments();      //Procedure to fetch data from database.
                if (dtAppointments != null && dtAppointments.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAppointments.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row oNewRow = c1Appointments.Rows.Add();
                        c1Appointments.Rows[oNewRow.Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTID, Convert.ToString(dtAppointments.Rows[i]["nPatientID"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTCODE, Convert.ToString(dtAppointments.Rows[i]["sPatientCode"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTFNAME, Convert.ToString(dtAppointments.Rows[i]["sFirstName"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTMNAME, Convert.ToString(dtAppointments.Rows[i]["sMiddleName"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTLNAME, Convert.ToString(dtAppointments.Rows[i]["sLastName"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTDOB, Convert.ToString(dtAppointments.Rows[i]["dtDOB"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTPHONE, Convert.ToString(dtAppointments.Rows[i]["sPhone"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTSSN, Convert.ToString(dtAppointments.Rows[i]["nSSN"]));
                        c1Appointments.SetData(oNewRow.Index, COL_APPPROVIDER, Convert.ToString(dtAppointments.Rows[i]["sProviderName"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTAPPTYPE, Convert.ToString(dtAppointments.Rows[i]["sAppointmentType"]));
                        c1Appointments.SetData(oNewRow.Index, COL_PATIENTLOCATON, Convert.ToString(dtAppointments.Rows[i]["sLocationName"]));
                        c1Appointments.SetData(oNewRow.Index, COL_APPSTARTDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])).ToShortDateString());
                        c1Appointments.SetData(oNewRow.Index, COL_APPSTARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"])).ToShortTimeString());
                        c1Appointments.SetData(oNewRow.Index, COL_APPENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"])).ToShortTimeString());
                        c1Appointments.SetData(oNewRow.Index, COL_APPDURATION, Convert.ToString(dtAppointments.Rows[i]["nDuration"]));
                        c1Appointments.SetData(oNewRow.Index, COL_APPNOTES, Convert.ToString(dtAppointments.Rows[i]["sNotes"]).Trim());
                        c1Appointments.SetData(oNewRow.Index, COL_APPSTATUS, ((ASUsedStatus)Convert.ToInt32(dtAppointments.Rows[i]["nAppointmentStatus"])).ToString());
                    }

                }
                c1Appointments.Sort(C1.Win.C1FlexGrid.SortFlags.Descending, COL_APPSTARTDATE);
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
                //_IsControlFilling = false;
            }
        }

        private DataTable GetNoshowAppointments()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);

                ASUsedStatus oASUsedStatus = ASUsedStatus.Cancel;
                if (rbNoShowAppointments.Checked == true)
                    oASUsedStatus = ASUsedStatus.NoShow;
                else if (rbDeletedAppointments.Checked == true)
                    oASUsedStatus = ASUsedStatus.Delete;

                strSQL = "SELECT DISTINCT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS sPatientCode, ISNULL(Patient.sFirstName, '') AS sFirstName, ISNULL(Patient.sMiddleName, '') "
                + " AS sMiddleName, ISNULL(Patient.sLastName, '') AS sLastName, ISNULL(Patient.nSSN, '') AS nSSN, CONVERT(varchar, Patient.dtDOB, 101) AS dtDOB,  "
                + " ISNULL(Patient.sPhone, '') AS sPhone, ISNULL(AS_Appointment_MST.nMSTAppointmentID, 0) AS nMSTAppointmentID, ISNULL(AS_Appointment_MST.nASBaseID, 0)  "
                + " AS nProviderID, ISNULL(AS_Appointment_MST.sASBaseDesc, '') AS sProviderName, ISNULL(AS_Appointment_MST.sAppointmentTypeDesc, '') AS sAppointmentType,  "
                + " AS_Appointment_MST.dtStartDate, AS_Appointment_MST.dtStartTime, AS_Appointment_MST.dtEndTime, ISNULL(AS_Appointment_MST.nDuration, 0) AS nDuration,  "
                + " ISNULL(AS_Appointment_MST.sLocationName, '') AS sLocationName, ISNULL(AS_Appointment_MST.sDepartmentName, '') AS sDepartmentName,  "
                + " ISNULL(AS_Appointment_DTL.sNotes, '') AS sNotes,ISNULL(PatientTracking.nTrackingStatus,0) AS nAppointmentStatus  "
                + " FROM AS_Appointment_MST  WITH(NOLOCK) INNER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  "
                + " INNER JOIN Patient  WITH(NOLOCK) ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN "
                + " PatientTracking  WITH(NOLOCK) ON AS_Appointment_DTL.nMSTAppointmentID = PatientTracking.nMSTAppointmentID AND  "
                + " AS_Appointment_DTL.nDTLAppointmentID = PatientTracking.nDTLAppointmentID "
                + " WHERE AS_Appointment_MST.nClinicID = 1 AND PatientTracking.nTrackingStatus = " + oASUsedStatus.GetHashCode() + " " // 
                + " AND (AS_Appointment_MST.dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpFromDate.Value.ToShortDateString()) + " AND AS_Appointment_MST.dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpToDate.Value.ToShortDateString()) + ") ";

                

                if (cmbProviders.SelectedIndex > 0)
                {
                    strSQL += " AND AS_Appointment_MST.nASBaseID = " + Convert.ToInt64(cmbProviders.SelectedValue) + " ";
                }

                if (cmbLocation.SelectedIndex > 0)
                {
                    strSQL += " AND AS_Appointment_MST.sLocationName = '" + cmbLocation.Text.Trim() + "'";
                }

                oDB.Retrive_Query(strSQL, out dt);
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
            }
            return dt;
        }

        private void ExportToExcel(bool ShowReport)
        {
            //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
            //object value = new object();
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
                //ogloSettings.GetSetting("ExportToDefaultLocation", out value);
                //if (value != null)
                //{
                //    if (value.ToString() != "")
                //    {
                //        _Checked = Convert.ToBoolean(value);
                //    }
                //}
                //value = null;
                //ogloSettings.GetSetting("ExportToDefaultLocationPath", out value);

                //if (value != null)
                //{
                //    _DefaultLocationPath = value.ToString();
                //}


                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    _Checked = false;
                }
                _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                oSettings.Dispose();


                if (_DefaultLocationPath != "" && _Checked == true)
                {
                    if (_DefaultLocationPath.EndsWith("\\"))
                    {
                        char[] trimChars = { '\\' };
                        _DefaultLocationPath = _DefaultLocationPath.TrimEnd(trimChars);
                    }
                    // If not exist create directory
                    if (Directory.Exists(_DefaultLocationPath) == false)
                    {
                        Directory.CreateDirectory(_DefaultLocationPath);
                    }

                    _FilePath = _DefaultLocationPath + "\\NoShow Appointment";

                    _FilePath += Convert.ToString(DateTime.Now).Replace(":", "");
                    _FilePath = _FilePath.Replace("/", "") + ".xls";
                }
                else
                {
                    FileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                    saveFileDialog.DefaultExt = ".xls";
                    saveFileDialog.AddExtension = true;
                    if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        saveFileDialog.Dispose();
                        saveFileDialog = null;
                        return;
                    }
                    _FilePath = saveFileDialog.FileName;
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                }

                c1Appointments.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (ShowReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    { System.Diagnostics.Process.Start(_FilePath); }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ioEx.ToString(), false);
                ioEx = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Sort By Criteria "

        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmb_datefilter.Items.Clear();
                cmb_datefilter.Items.Add("Custom");
                cmb_datefilter.Items.Add("Today");
                cmb_datefilter.Items.Add("Tomorrow");
                cmb_datefilter.Items.Add("Yesterday");
                cmb_datefilter.Items.Add("This Week");
                cmb_datefilter.Items.Add("Last Week");
                cmb_datefilter.Items.Add("Current Month");
                cmb_datefilter.Items.Add("Last Month");
                cmb_datefilter.Items.Add("Current Year");
                cmb_datefilter.Items.Add("Last 30 Days");
                cmb_datefilter.Items.Add("Last 60 Days");
                cmb_datefilter.Items.Add("Last 90 Days");
                cmb_datefilter.Items.Add("Last 120 Days");
                cmb_datefilter.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;

            _filterby = cmb_datefilter.SelectedIndex;
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

        private void FilterBy_Today()
        {

            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_Tomorrow()
        {
            dtpFromDate.Value = DateTime.Now.AddDays(1);
            dtpToDate.Value = DateTime.Now.AddDays(1);

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
        }

        private void FilterBy_Yesterday()
        {
            dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpToDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
        }

        private void FilterBy_Thisweek()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpFromDate.Value = DateTime.Today;
                dtpToDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_lastweek()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpToDate.Value = dtpFromDate.Value.AddDays(6);
            }

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_currentmonth()
        {
            DateTime dtFrom = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1);

            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, dtpFromDate.Value.Month, 1);
            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpFromDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpToDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;


        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

            dtpFromDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpToDate.Value = Convert.ToDateTime(lastDay.Date);

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

            dtpFromDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpFromDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            dtpFromDate.Enabled = true;
            dtpToDate.Enabled = true;

        }

        #endregion "Sort By Criteria "

        private void rbCancelAppointments_CheckedChanged(object sender, EventArgs e)
        {
            ShowReport();

            if (c1Appointments.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpToDate.Value = dtpFromDate.Value;   
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

       
    }
}