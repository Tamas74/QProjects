using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook;

namespace gloBilling
{
    public partial class frmRpt_NewPatientvsEstablishedPatient : Form
    {
        #region "Varivables & Properties"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Providers;
        //gloPatientStripControl.gloPatientStripControl oPatientStripControl = null;

        private Int64 _clinicId = 0;
        private Int64 _patientId = 0;

        public Int64 ClinicId
        {
            get { return _clinicId; }
            set { _clinicId = value; }
        }

        public Int64 PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }
        
        #endregion

        #region "Variables FOr C1 Columns "

        private const int COL_PATIENTID = 0;
        private const int COL_PATIENTCODE = 1;
        private const int COL_PATIENT = 2;
        private const int COL_PATIENTDOB = 3;
        private const int COL_PROVIDER = 4;
        private const int COL_APPOINTMENTDATE = 5;
        private const int COL_COUNT = 6;

        #endregion

        #region "Constructors"
        public frmRpt_NewPatientvsEstablishedPatient(string DataBaseConnectionString)
        {
            InitializeComponent();


            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }
            this._databaseconnectionstring = DataBaseConnectionString;

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

        public frmRpt_NewPatientvsEstablishedPatient(string DataBaseConnectionString, Int64 PatientId)
        {
            InitializeComponent();

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

            this._databaseconnectionstring = DataBaseConnectionString;
            this.PatientId = PatientId;

        } 
        #endregion

        #region "Form Load Event "

            private void frmRPT_New_PatientvsEstablishedPatient_Load(object sender, EventArgs e)
            {
                DesignGrid();
                FillPatients();
                if (c1PatientList.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                }
                Fill_FilterDatesCombo();
            }
            
        #endregion
        

        #region " Public & Private Methods "

         private void DesignGrid()
            {
                c1PatientList.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                c1PatientList.Cols.Count = COL_COUNT;
                c1PatientList.Rows.Count = 1;
                c1PatientList.Rows.Fixed = 1;

                c1PatientList.SetData(0, COL_PATIENTID, "PatientID");
                c1PatientList.SetData(0, COL_PATIENT, "Patient ");
                c1PatientList.SetData(0, COL_PATIENTCODE, "Patient Code");
                c1PatientList.SetData(0, COL_PATIENTDOB, "Date of Birth");
                c1PatientList.SetData(0, COL_PROVIDER, "Provider");
                c1PatientList.SetData(0, COL_APPOINTMENTDATE, "Appointment Date");

                c1PatientList.Cols[COL_PATIENT].Width = 200;
                c1PatientList.Cols[COL_PROVIDER].Width = 200;
                c1PatientList.Cols[COL_PATIENTCODE].Width = 100;
                c1PatientList.Cols[COL_PATIENTDOB].Width = 100;
                c1PatientList.Cols[COL_APPOINTMENTDATE].Width = 200;

                c1PatientList.Cols[COL_PATIENTCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientList.Cols[COL_PATIENTDOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
             
                c1PatientList.Cols[COL_PATIENTID].Visible = false;

                c1PatientList.AllowEditing = false;
                c1PatientList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

                c1PatientList.Cols[COL_PATIENT].AllowSorting = true;
                c1PatientList.Cols[COL_PROVIDER].AllowSorting = true;
                c1PatientList.Cols[COL_PATIENTCODE].AllowSorting = true;
                c1PatientList.Cols[COL_APPOINTMENTDATE].AllowSorting = true;

            }

         private void FillPatients()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPatient = null;
            string strSQL = " ";

            try
            {
                oDB.Connect(false);

                strSQL = " SELECT  DISTINCT   ISNULL(Patient.sFirstName + SPACE(1)+ Patient.sMiddleName + SPACE(1)+ Patient.sLastName,'') AS PatientName,ISNULL(Patient.sPatientCode,'') AS  PatientCode,ISNULL(Patient.nPatientID,0) AS  PatientID ,convert(varchar,Patient.dtDOB,101) As dtDOB, " +
                        " ISNULL(AS_Appointment_DTL.dtStartDate,0) AS AppointMentDate,ISNULL(AS_Appointment_MST.sASBaseDesc,'') AS Provider FROM  AS_Appointment_MST  LEFT OUTER JOIN  AS_Appointment_DTL  ON  AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID " +
                        " LEFT OUTER JOIN  Patient ON Patient.nPatientID = AS_Appointment_MST.nPatientID  LEFT OUTER JOIN  AB_AppointmentType ON  AB_AppointmentType.nAppointmentTypeID = AS_Appointment_MST.nAppointmentTypeID "+ 	
                        " WHERE  (AS_Appointment_DTL.dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + ") AND (AS_Appointment_DTL.dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + ") "+
                        " AND (AS_Appointment_DTL.dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") AND (AS_Appointment_DTL.dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ") ";

                if (rbSortNewPatient.Checked == true)
                {
                    strSQL += " AND  AB_AppointmentType.nAppointmentTypeFlag = 3 ";
                }
                else
                {
                    strSQL += " AND  AB_AppointmentType.nAppointmentTypeFlag <> 3 ";
                }
                //if (cmbApp_AppointmentType.SelectedIndex != -1 && cmbApp_AppointmentType.Text!="")
                //{
                //    strSQL += " AND  AB_AppointmentType.nAppointmentTypeID ='"+cmbApp_AppointmentType.SelectedValue +"' ";
                //}
                string _strProviderIDs = "";
                for (int i = 0; i < cmbProvider.Items.Count; i++)
                {
                    cmbProvider.SelectedIndex = i;
                    cmbProvider.Refresh();
                    if (i == 0)
                    {
                        _strProviderIDs = "(" + Convert.ToInt64(cmbProvider.SelectedValue);
                    }
                    else
                    {
                        _strProviderIDs += "," + Convert.ToInt64(cmbProvider.SelectedValue);
                    }

                    if (i == cmbProvider.Items.Count - 1)
                    {
                        _strProviderIDs += ")";
                    }
                }

                if (_strProviderIDs != "")
                    strSQL += " AND AS_Appointment_MST.nASBaseID IN " + _strProviderIDs + "";

                string _strAppointmentTypeIDs = "";
                for (int i = 0; i < cmbApp_AppointmentType.Items.Count; i++)
                {
                    cmbApp_AppointmentType.SelectedIndex = i;
                    cmbApp_AppointmentType.Refresh();
                    if (i == 0)
                    {
                        _strAppointmentTypeIDs = "(" + Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                    }
                    else
                    {
                        _strAppointmentTypeIDs += "," + Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                    }

                    if (i == cmbApp_AppointmentType.Items.Count - 1)
                    {
                        _strAppointmentTypeIDs += ")";
                    }
                }

                if (_strAppointmentTypeIDs != "")
                    strSQL += " AND AB_AppointmentType.nAppointmentTypeID IN " + _strAppointmentTypeIDs + "";

                dtPatient = new DataTable();
                oDB.Retrive_Query(strSQL, out dtPatient);

                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPatient.Rows.Count; i++)
                    {
                        c1PatientList.Rows.Add();
                        int rowIndex = c1PatientList.Rows.Count - 1;

                        c1PatientList.SetData(rowIndex, COL_PATIENTID, Convert.ToString(dtPatient.Rows[i]["PatientID"]));
                        c1PatientList.SetData(rowIndex, COL_PATIENTCODE, Convert.ToString(dtPatient.Rows[i]["PatientCode"]));
                        c1PatientList.SetData(rowIndex, COL_PATIENTDOB, Convert.ToString(dtPatient.Rows[i]["dtDOB"]));
                        c1PatientList.SetData(rowIndex, COL_PATIENT, Convert.ToString(dtPatient.Rows[i]["PatientName"]));
                        c1PatientList.SetData(rowIndex, COL_PROVIDER, Convert.ToString(dtPatient.Rows[i]["Provider"]));
                        c1PatientList.SetData(rowIndex, COL_APPOINTMENTDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPatient.Rows[i]["AppointMentDate"])).ToShortDateString());
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (dtPatient != null) { dtPatient.Dispose(); }
            }
        }

        private void Fill_AppointmentTypes()
        {
            gloAppointmentBook.Books.AppointmentType oAppointmentType = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
            DataTable dtAppointmentType = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);
            try
            {
            if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 1)
            {
                DataRow dr = dtAppointmentType.NewRow();
                dtAppointmentType.Rows.InsertAt(dr, 0);
                cmbApp_AppointmentType.DataSource = dtAppointmentType;
            
                cmbApp_AppointmentType.DisplayMember=Convert.ToString(dtAppointmentType.Columns["sAppointmentType"]);
                cmbApp_AppointmentType.ValueMember =Convert.ToString(dtAppointmentType.Columns["nAppointmentTypeID"]);
			
            }
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

        #endregion

        #region "ToolStrip Button Events"

         private void ts_btnRefresh_Click(object sender, EventArgs e)
        {
            DesignGrid();
            FillPatients();
            if (c1PatientList.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
            }

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.NewPatientVsEst_PatientRpt, gloAuditTrail.ActivityType.View, "View Report", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
        }
         private void ts_btnCancel_Click(object sender, EventArgs e)
        {
             
            this.Close();
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
          if(c1PatientList!=null &&c1PatientList.Rows.Count > 1)
            {
                ExportReportToExcel(false);
            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            if (c1PatientList != null && c1PatientList.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }
        #endregion

        #region "List Control Events "

          private void btnBrowseProvider_Click(object sender, EventArgs e)
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
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = " Provider";
                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbProvider.DataSource != null)
                {
                    for (int i = 0; i < cmbProvider.Items.Count; i++)
                    {
                        cmbProvider.SelectedIndex = i;
                        cmbProvider.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

          private void btnClearProvider_Click(object sender, EventArgs e)
        {
         //   cmbProvider.Items.Clear();
            cmbProvider.DataSource = null;
            cmbProvider.Refresh();
        }

          void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.AppointmentType:
                    {
                        
                        cmbApp_AppointmentType.DataSource = null;
                        cmbApp_AppointmentType.Items.Clear();
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

                            cmbApp_AppointmentType.DataSource = oBindTable;
                            cmbApp_AppointmentType.DisplayMember = "DispName";
                            cmbApp_AppointmentType.ValueMember = "ID";
                        }
                    }
                    break;
                case gloListControl.gloListControlType.Providers:
                    {
                        
                        cmbProvider.DataSource = null;
                        cmbProvider.Items.Clear();
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

                            cmbProvider.DataSource = oBindTable;
                            cmbProvider.DisplayMember = "DispName";
                            cmbProvider.ValueMember = "ID";
                        }

                    }
                    break;
               
                default:
                    {
                    }
                    break;
            }
        }

          void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }
                 
            }
        } 

        #endregion

        private void btnBrowseAppointmentType_Click(object sender, EventArgs e)
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
                            break;
                        }
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.AppointmentType, true, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = "Appointment Type";
                _CurrentControlType = gloListControl.gloListControlType.AppointmentType;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

               
                   if(cmbApp_AppointmentType.DataSource != null)

                {
                    
                    for (int i = 0; i < cmbApp_AppointmentType.Items.Count; i++)
                    {
                        cmbApp_AppointmentType.SelectedIndex = i;
                        cmbApp_AppointmentType.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue), cmbApp_AppointmentType.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearAppointmentType_Click(object sender, EventArgs e)
        {
          //  cmbApp_AppointmentType.Items.Clear();
            cmbApp_AppointmentType.DataSource = null;
            cmbApp_AppointmentType.Refresh();
        }

        private void ExportReportToExcel(bool OpenReport)
        {
           // gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
           // object value = new object();
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

                    _FilePath = _DefaultLocationPath + "\\New Patient Report";

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

                c1PatientList.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    { System.Diagnostics.Process.Start(_FilePath); }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ioEx.ToString(), true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }


        #region "Filter by date"

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

        #region " Methods "

        private void FilterBy_Today()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_Tomorrow()
        {
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }


        private void FilterBy_Yesterday()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Thisweek()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastweek()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currentmonth()
        {
            // new added ..to remove current month problem in report...

            DateTime firstDay = new DateTime(dtpStartDate.Value.Year, DateTime.Now.Month, 1);                       

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

          

            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;


        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);

            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);

            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);

            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }

        #endregion

        #endregion "Filter by date"

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        
       }
}