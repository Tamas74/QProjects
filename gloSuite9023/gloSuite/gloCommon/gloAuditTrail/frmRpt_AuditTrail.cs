using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloAuditTrail
{
    public partial class frmRpt_AuditTrail : Form
    {
        #region "Private Variables "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //nAuditTrailID,dtActivityDateTime,sActivityModule,sActivityCategory,sActivityType,sDescription,nPatientID,nTransactionID,nProviderID,nUserID,sUserName,sMachineName,sOutcome,nClinicID

        const Int32 COL_AUDITTRAILID = 0;
        const Int32 COL_ACTIVITYDATE = 1;
        const Int32 COL_ACTIVITYTIME = 2;
        const Int32 COL_ACTIVITYMODULE = 3;
        const Int32 COL_ACTIVITYCATEGORY = 4;
        const Int32 COL_ACTIVITYTYPE = 5;
        const Int32 COL_DESCRIPTION = 6; 
        const Int32 COL_PATIENTID = 7;
        const Int32 COL_TRANSACTIONID = 8;
        const Int32 COL_PROVIDERID = 9;
        const Int32 COL_USERID = 10;
        const Int32 COL_USERNAME = 11;
        const Int32 COL_MACHINENAME = 12;
        const Int32 COL_OUTCOME = 13;
        const Int32 Col_ClinicID = 14;
        const Int32 COL_MachineIP = 15;
        const Int32 COL_RemoteMachineName = 16;
        const Int32 COL_RemoteMachineIP = 17;
        const Int32 COL_RemoteUserName = 18;
        const Int32 COL_Domain = 19;
        const Int32 COL_COUNT = 20;

        #endregion

        public frmRpt_AuditTrail(String DatabaseConnectionString)
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

            _databaseconnectionstring = DatabaseConnectionString;

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

 
        }

        private void frmRpt_AuditTrail_Load(object sender, EventArgs e)
        {
            FillActivityModules();
            Fill_FilterDatesCombo();
        }

        private void FillActivityModules()
        {
            try
            {
                cmbModule.Items.Clear();
                cmbModule.Items.Add("");
                cmbModule.Items.Add(ActivityModule.Security.ToString());
                cmbModule.Items.Add(ActivityModule.Appointment.ToString());
                cmbModule.Items.Add(ActivityModule.Scheduling.ToString());
                cmbModule.Items.Add(ActivityModule.Billing.ToString());
                cmbModule.Items.Add(ActivityModule.Patient.ToString());
                cmbModule.Items.Add(ActivityModule.Contact.ToString());
                cmbModule.Items.Add(ActivityModule.AppointmentBook.ToString());
                cmbModule.Items.Add(ActivityModule.Setting.ToString());
                cmbModule.Items.Add(ActivityModule.DMS.ToString());
                cmbModule.Items.Add(ActivityModule.RCMDMS.ToString());
                cmbModule.Items.Add(ActivityModule.NYWCForms.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Tool Strip Button events"

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ShowReport();
            }
            catch (Exception ex)
            {
                gloAuditTrail.ExceptionLog(ex.ToString(), true);    
            }
        }

      

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string _FilePath = "";
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
                C1AuditTrail.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        } 

        #endregion

        private void ShowReport()
        {
            DataTable dtAudit = null;
            try
            {
                DesignGrid();
                dtAudit = gloAuditTrail.ViewUserLog(cmbModule.Text, dtpStartDate.Value, dtpEndDate.Value, _clinicId);
                if (dtAudit != null && dtAudit.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAudit.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = C1AuditTrail.Rows.Add();

                        C1AuditTrail.SetData(NewRow.Index, COL_AUDITTRAILID, Convert.ToString(dtAudit.Rows[i]["nAuditTrailID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_ACTIVITYDATE, Convert.ToDateTime(dtAudit.Rows[i]["dtActivityDateTime"]).ToShortDateString());
                        C1AuditTrail.SetData(NewRow.Index, COL_ACTIVITYTIME, Convert.ToDateTime(dtAudit.Rows[i]["dtActivityDateTime"]).ToShortTimeString());
                        C1AuditTrail.SetData(NewRow.Index, COL_ACTIVITYMODULE, Convert.ToString(dtAudit.Rows[i]["sActivityModule"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_ACTIVITYCATEGORY, Convert.ToString(dtAudit.Rows[i]["sActivityCategory"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_ACTIVITYTYPE, Convert.ToString(dtAudit.Rows[i]["sActivityType"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_DESCRIPTION, Convert.ToString(dtAudit.Rows[i]["sDescription"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_PATIENTID, Convert.ToString(dtAudit.Rows[i]["nPatientID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_TRANSACTIONID, Convert.ToString(dtAudit.Rows[i]["nTransactionID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_PROVIDERID, Convert.ToString(dtAudit.Rows[i]["nProviderID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_USERID, Convert.ToString(dtAudit.Rows[i]["nUserID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_USERNAME, Convert.ToString(dtAudit.Rows[i]["sUserName"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_MACHINENAME, Convert.ToString(dtAudit.Rows[i]["sMachineName"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_OUTCOME, Convert.ToString(dtAudit.Rows[i]["sOutcome"]));
                        C1AuditTrail.SetData(NewRow.Index, Col_ClinicID, Convert.ToString(dtAudit.Rows[i]["nClinicID"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_MachineIP, Convert.ToString(dtAudit.Rows[i]["Local Machine IP"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_RemoteMachineName, Convert.ToString(dtAudit.Rows[i]["Remote Machine Name"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_RemoteMachineIP, Convert.ToString(dtAudit.Rows[i]["Remote Machine IP"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_RemoteUserName, Convert.ToString(dtAudit.Rows[i]["Remote User Name"]));
                        C1AuditTrail.SetData(NewRow.Index, COL_Domain, Convert.ToString(dtAudit.Rows[i]["Domain"]));


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (dtAudit!=null)
                {
                    dtAudit.Dispose(); dtAudit = null;
                }
            }
        }

        private void DesignGrid()
        {
            try
            {

                C1AuditTrail.Rows.Count = 1;
                C1AuditTrail.Cols.Count = COL_COUNT;
                C1AuditTrail.Rows.Fixed = 1;


                C1AuditTrail.SetData(0, COL_AUDITTRAILID, "AuditTrailID");
                C1AuditTrail.SetData(0, COL_ACTIVITYDATE, "Date");
                C1AuditTrail.SetData(0, COL_ACTIVITYTIME, "Time");
                C1AuditTrail.SetData(0, COL_ACTIVITYMODULE, "Module");
                C1AuditTrail.SetData(0, COL_ACTIVITYCATEGORY, "Category");
                C1AuditTrail.SetData(0, COL_ACTIVITYTYPE, "Type");
                C1AuditTrail.SetData(0, COL_DESCRIPTION, "Description");
                C1AuditTrail.SetData(0, COL_PATIENTID, "Patient ID");
                C1AuditTrail.SetData(0, COL_TRANSACTIONID, "Transaction ID");
                C1AuditTrail.SetData(0, COL_PROVIDERID, "Provider ID");
                C1AuditTrail.SetData(0, COL_USERID, "User ID");
                C1AuditTrail.SetData(0, COL_USERNAME, "User Name");
                C1AuditTrail.SetData(0, COL_MACHINENAME, "Machine Name");
                C1AuditTrail.SetData(0, COL_OUTCOME, "Outcome");
                C1AuditTrail.SetData(0, Col_ClinicID, "nClinicID");
                C1AuditTrail.SetData(0, COL_MachineIP, "Machine IP");
                C1AuditTrail.SetData(0, COL_RemoteMachineName, "Remote Machine Name");
                C1AuditTrail.SetData(0, COL_RemoteMachineIP, "Remote Machine IP");
                C1AuditTrail.SetData(0, COL_RemoteUserName, "Remote User Name");
                C1AuditTrail.SetData(0, COL_Domain, "Domain");

                //COL_AUDITTRAILID
                //COL_ACTIVITYDATETIME
                //COL_ACTIVITYMODULE
                //COL_ACTIVITYCATEGORY
                //COL_ACTIVITYTYPE
                //COL_DESCRIPTION
                //COL_PATIENTID
                //COL_TRANSACTIONID
                //COL_PROVIDERID
                //COL_USERID
                //COL_USERNAME
                //COL_MACHINENAME
                //COL_OUTCOME

                C1AuditTrail.Cols[COL_AUDITTRAILID].Visible = false;
                C1AuditTrail.Cols[COL_ACTIVITYDATE].Visible = true;
                C1AuditTrail.Cols[COL_ACTIVITYTIME].Visible = true;
                C1AuditTrail.Cols[COL_ACTIVITYMODULE].Visible = true;
                C1AuditTrail.Cols[COL_ACTIVITYCATEGORY].Visible = true;
                C1AuditTrail.Cols[COL_ACTIVITYTYPE].Visible = true;
                C1AuditTrail.Cols[COL_DESCRIPTION].Visible = true;
                C1AuditTrail.Cols[COL_PATIENTID].Visible = false;
                C1AuditTrail.Cols[COL_TRANSACTIONID].Visible = false;
                C1AuditTrail.Cols[COL_PROVIDERID].Visible = false;
                C1AuditTrail.Cols[COL_USERID].Visible = false;
                C1AuditTrail.Cols[COL_USERNAME].Visible = true;
                C1AuditTrail.Cols[COL_MACHINENAME].Visible = true;
                C1AuditTrail.Cols[COL_OUTCOME].Visible = true;
                C1AuditTrail.Cols[Col_ClinicID].Visible = false;
                C1AuditTrail.Cols[COL_MachineIP].Visible = true;
                C1AuditTrail.Cols[COL_RemoteMachineName].Visible = true;
                C1AuditTrail.Cols[COL_RemoteMachineIP].Visible = true;
                C1AuditTrail.Cols[COL_RemoteUserName].Visible = true;
                C1AuditTrail.Cols[COL_Domain].Visible = true;


                C1AuditTrail.Cols[COL_ACTIVITYDATE].DataType = typeof(System.String);
                C1AuditTrail.Cols[COL_ACTIVITYTIME].DataType = typeof(System.String);

                int _width = pnlAuditTrail.Width - 5;

                C1AuditTrail.Cols[COL_AUDITTRAILID].Width = 0;
                C1AuditTrail.Cols[COL_ACTIVITYDATE].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_ACTIVITYTIME].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_ACTIVITYMODULE].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_ACTIVITYCATEGORY].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_ACTIVITYTYPE].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_DESCRIPTION].Width = Convert.ToInt32(_width * 0.2);
                C1AuditTrail.Cols[COL_PATIENTID].Width = 0;
                C1AuditTrail.Cols[COL_TRANSACTIONID].Width = 0;
                C1AuditTrail.Cols[COL_PROVIDERID].Width = 0;
                C1AuditTrail.Cols[COL_USERID].Width = 0;
                C1AuditTrail.Cols[COL_USERNAME].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_MACHINENAME].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[COL_OUTCOME].Width = Convert.ToInt32(_width * 0.1);
                C1AuditTrail.Cols[Col_ClinicID].Width = 0;
                C1AuditTrail.Cols[COL_MachineIP].Width = Convert.ToInt32(_width * 0.2);
                C1AuditTrail.Cols[COL_RemoteMachineName].Width = Convert.ToInt32(_width * 0.2);
                C1AuditTrail.Cols[COL_RemoteMachineIP].Width = Convert.ToInt32(_width * 0.2);
                C1AuditTrail.Cols[COL_RemoteUserName].Width = Convert.ToInt32(_width * 0.2);
                C1AuditTrail.Cols[COL_Domain].Width = Convert.ToInt32(_width * 0.2);


                C1AuditTrail.Cols[COL_AUDITTRAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_ACTIVITYDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_ACTIVITYTIME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_ACTIVITYMODULE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_ACTIVITYCATEGORY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_ACTIVITYTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_DESCRIPTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_TRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_USERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_MACHINENAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_OUTCOME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_MachineIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_RemoteMachineName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_RemoteMachineIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_RemoteUserName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1AuditTrail.Cols[COL_Domain].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            }
            catch (Exception ex)
            {
                gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
                gloAuditTrail.ExceptionLog(ex.ToString(), true);

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

                case 2://Yesterday
                    FilterBy_Yesterday();
                    break;

                case 3://This week
                    FilterBy_Thisweek();
                    break;

                case 4://Last Week
                    FilterBy_lastweek();
                    break;

                case 5://Current Month
                    FilterBy_currentmonth();
                    break;

                case 6://Last Month
                    FilterBy_lastmonth();
                    break;

                case 7://Current Year
                    FilterBy_currenYear();
                    break;

                case 8://Last 30 days
                    FilterBy_last30days();
                    break;

                case 9://Last 60 days
                    FilterBy_last60days();
                    break;

                case 10://Last 90 days
                    FilterBy_last90days();
                    break;

                case 11://Last 120 days
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
            DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);

            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

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


        #endregion "Sort By Criteria "

       

       
    }
}