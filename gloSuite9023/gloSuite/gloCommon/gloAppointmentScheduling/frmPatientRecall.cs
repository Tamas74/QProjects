using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.IO;

namespace gloAppointmentScheduling
{
    public partial class frmPatientRecall : Form
    {
        #region "Private Variables "

        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string _MessageBoxCaption =string.Empty;
        string _ListControlType = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloListControl.gloListControl oListControl;
        //private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        #region "C1 Column Constants"

        //Expired Authorization
        const Int32 COL_AUTH_PATINETID = 0;
        const Int32 COL_AUTH_PATINETNAME = 1;
        const Int32 COL_AUTH_DATE = 2;
        const Int32 COL_AUTH_EXPIRYDATE = 3;
        const Int32 COL_AUTH_COUNT = 4;

        //Reschedule Recall
        const Int32 COL_RESCH_APPOINTMENTID_MST = 0;
        const Int32 COL_RESCH_APPOINTMENTID_DTL = 1;
        const Int32 COL_RESCH_SELECT = 2;
        const Int32 COL_RESCH_STARTDATE = 3;
        const Int32 COL_RESCH_ENDDATE = 4;
        const Int32 COL_RESCH_PATIENTID = 5;
        const Int32 COL_RESCH_PATIENTNAME = 6;
        const Int32 COL_RESCH_STARTTIME = 7;
        const Int32 COL_RESCH_ENDTIME = 8;
        const Int32 COL_RESCH_DURATION = 9;
        const Int32 COL_RESCH_PROVIDERID = 10;
        const Int32 COL_RESCH_PROVIDER = 11;
        const Int32 COL_RESCH_LOCATION = 12;
        const Int32 COL_RESCH_DEPARTMENT = 13;
        const Int32 COL_RESCH_APPOINTMENTTYPE = 14;
        const Int32 COL_RESCH_APPOINTMENTFLAG = 15;
        const Int32 COL_RESCH_COUNT = 16;

        #endregion

        #endregion "Private Variables "

        #region "Contructor"

        public frmPatientRecall(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }


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

        #region "Form Load Event"

        private void frmPatientRecall_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(C1Patients, false);

                        
            Fill_Location();
            Fill_Department();
            Fill_AppointmentTypes();
            if (C1Patients.Rows.Count <= 1)
            {
                tls_btnExportToExcel.Enabled = false;
                tls_btnExportToExcelOpen.Enabled = false;
            }
            else
            {
                tls_btnExportToExcel.Enabled = true;
                tls_btnExportToExcelOpen.Enabled = true;
            }
            //to remove selection criteria for Expired Authorization at bottom for first time only 

            if (rbExpiredAuthorization.Checked == true)
            {
                rbExpiredAuthorization.Font = gloGlobal.clsgloFont.gFont_BOLD; new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;

            }
            else
            {

                rbExpiredAuthorization.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid();


        } 

        #endregion

        #region "Fill Data"
        private void Fill_Location()
        {
            try
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
                r["sLocation"] = "";
                r["nClinicID"] = 0;
                dt.Rows.InsertAt(r, 0);

                if (dt != null)
                {
                    cmbApp_Location.DataSource = dt.Copy();
                    cmbApp_Location.ValueMember = dt.Columns["nLocationID"].ColumnName;
                    cmbApp_Location.DisplayMember = dt.Columns["sLocation"].ColumnName;

                    cmbReSchLocation.DataSource = dt.Copy();
                    cmbReSchLocation.ValueMember = dt.Columns["nLocationID"].ColumnName;
                    cmbReSchLocation.DisplayMember = dt.Columns["sLocation"].ColumnName;
                }

                if (Convert.ToString(appSettings["DefaultLocationID"]) != "")
                {

                    if (Convert.ToInt64(appSettings["DefaultLocationID"]) > 0)
                    {
                        cmbApp_Location.SelectedValue = Convert.ToInt64(appSettings["DefaultLocationID"]);
                        cmbApp_Location.Text = Convert.ToString(appSettings["DefaultLocation"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
        }

        private void Fill_Department()
        {
            try
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
                    cmbApp_Department.DataSource = dt.Copy();
                    cmbApp_Department.ValueMember = dt.Columns["nDepartmentID"].ColumnName;
                    cmbApp_Department.DisplayMember = dt.Columns["sDepartment"].ColumnName;

                    cmbReSchDepartment.DataSource = dt.Copy();
                    cmbReSchDepartment.ValueMember = dt.Columns["nDepartmentID"].ColumnName;
                    cmbReSchDepartment.DisplayMember = dt.Columns["sDepartment"].ColumnName;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
        }

        private void Fill_AppointmentTypes()
        {
            //Appointment Types
            try
            {
                gloAppointmnetScheduleCommon oApptCommon = new gloAppointmnetScheduleCommon(_databaseconnectionstring);
                gloGeneralItem.gloItems oListItems = oApptCommon.GetAppointmentTypes();
                DataTable oTableAppTypes = new DataTable();

                oTableAppTypes.Columns.Add("ID");
                oTableAppTypes.Columns.Add("DispName");

                DataRow dr;
                dr = oTableAppTypes.NewRow();
                dr[0] = 0;
                dr[1] = "";
                oTableAppTypes.Rows.Add(dr);
                if (oListItems != null)
                {
                    for (int _Counter = 0; _Counter <= oListItems.Count - 1; _Counter++)
                    {
                        DataRow oRow;
                        oRow = oTableAppTypes.NewRow();
                        oRow[0] = oListItems[_Counter].ID;
                        oRow[1] = oListItems[_Counter].Description;
                        oTableAppTypes.Rows.Add(oRow);
                    }
                }
                cmbApp_AppointmentType.DataSource = oTableAppTypes.Copy();
                cmbApp_AppointmentType.DisplayMember = "DispName";
                cmbApp_AppointmentType.ValueMember = "ID";

                cmbReSchAppointmentType.DataSource = oTableAppTypes.Copy();
                cmbReSchAppointmentType.DisplayMember = "DispName";
                cmbReSchAppointmentType.ValueMember = "ID";
                if (oListItems != null)
                {
                    oListItems.Clear();
                    oListItems.Dispose();
                    oListItems = null;
                }
                oApptCommon.Dispose();
                oApptCommon = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbApp_AppointmentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            gloAppointmentBook.Books.AppointmentType oa = new gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);
            try
            {
                if (cmbApp_AppointmentType.SelectedIndex > 0)
                {
                    btnApp_DateTime_Color.Enabled = false;
                    btnApp_ClearDateTime_Color.Enabled = false;

                    dt = oa.GetAppointmentType(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            numApp_DateTime_Duration.Value = Convert.ToInt64(dt.Rows[0]["nDuration"]);
                            lblApp_DateTime_ColorContainer.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[0]["sColorCode"].ToString()));
                        }
                        else
                        {
                            lblApp_DateTime_ColorContainer.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        lblApp_DateTime_ColorContainer.BackColor = Color.White;
                    }
                }
                else
                {
                    lblApp_DateTime_ColorContainer.BackColor = Color.White;
                    btnApp_DateTime_Color.Enabled = true;
                    btnApp_ClearDateTime_Color.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloPM");
            }
            finally
            {
                if (oa != null) { oa.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }

        } 
        #endregion

        #region "Form Control Events"

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
                    lblApp_DateTime_ColorContainer.BackColor = colorDialog1.Color;
                    try
                    {
                        gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                    }
                    catch
                    {
                    }
                }
               // oColorDialog.Dispose();
               // oColorDialog = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnApp_ClearDateTime_Color_Click(object sender, EventArgs e)
        {
            lblApp_DateTime_ColorContainer.BackColor = Color.White;
            lblApp_DateTime_ColorContainer.Refresh();
        }

        private void btn_BrowseProvider_Click(object sender, EventArgs e)
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
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = "Provider";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                _ListControlType = "RecallAppointment";
                this.Controls.Add(oListControl);

                if (cmbApp_Provider.Items.Count > 0)
                {
                    for (int i = 0; i <= cmbApp_Provider.Items.Count - 1; i++)
                    {
                        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                        Int64 _tempid = Convert.ToInt64(((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[0]);
                        oItem.ID = _tempid; // ProviderID
                        oItem.Code = ((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[1].ToString();  // Code = ""
                        oItem.Description = ((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[2].ToString(); // Provider Name


                        oListControl.SelectedItems.Add(oItem);
                        oItem.Dispose();
                        oItem = null;
                    }
                }
                pnlRecallOptions.Visible = false;
                pnlc1Grid.Visible = false;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
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
                
                cmbApp_Provider.DataSource = null;
                cmbApp_Provider.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseReSchProvider_Click(object sender, EventArgs e)
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
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                oListControl.ClinicID = _clinicId;
                oListControl.ControlHeader = "Provider";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                _ListControlType = "RescheduleProvider";
                this.Controls.Add(oListControl);

                if (cmbApp_Provider.Items.Count > 0)
                {
                    for (int i = 0; i <= cmbApp_Provider.Items.Count - 1; i++)
                    {
                        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                        Int64 _tempid = Convert.ToInt64(((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[0]);
                        oItem.ID = _tempid; // ProviderID
                        oItem.Code = ((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[1].ToString();  // Code = ""
                        oItem.Description = ((System.Data.DataRowView)(cmbApp_Provider.Items[i])).Row.ItemArray[2].ToString(); // Provider Name


                        oListControl.SelectedItems.Add(oItem);
                        oItem.Dispose();
                        oItem = null;
                    }
                }
                pnlRecallOptions.Visible = false;
                pnlc1Grid.Visible = false;
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearReSchProvider_Click(object sender, EventArgs e)
        {
            try
            {
                
                cmbReSchProviders.DataSource = null;
                cmbReSchProviders.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("ProviderID");
                dtTemp.Columns.Add("code");
                dtTemp.Columns.Add("Provider Name");
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (int _Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                    {
                        DataRow r;
                        r = dtTemp.NewRow();
                        //Column[0]= "ProviderID"
                        //Column[1]= "code"
                        //Column[2]= "Provider Name"
                        r["ProviderID"] = oListControl.SelectedItems[_Counter].ID.ToString(); // dt.Rows[i]["nProviderID"];
                        r["Code"] = oListControl.SelectedItems[_Counter].Code.ToString();
                        r["Provider Name"] = oListControl.SelectedItems[_Counter].Description.ToString();

                        dtTemp.Rows.Add(r);
                    }
                }

                switch (_ListControlType)
                {
                    case "RecallAppointment":
                       // cmbApp_Provider.Items.Clear();
                        cmbApp_Provider.DataSource = null;
                        cmbApp_Provider.Items.Clear();
                        cmbApp_Provider.DataSource = dtTemp;
                        cmbApp_Provider.DisplayMember = "Provider Name";
                        cmbApp_Provider.ValueMember = "ProviderID";
                        break;

                    case "RescheduleProvider":
                       // cmbReSchProviders.Items.Clear();
                        cmbReSchProviders.DataSource = null;
                        cmbReSchProviders.Items.Clear();
                        cmbReSchProviders.DataSource = dtTemp;
                        cmbReSchProviders.DisplayMember = "Provider Name";
                        cmbReSchProviders.ValueMember = "ProviderID";
                        break; 

                    default:
                        break;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            pnlRecallOptions.Visible = true;
            pnlc1Grid.Visible = true;
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
                catch
                {
                }
                 
            }
            pnlRecallOptions.Visible = true;
            pnlc1Grid.Visible = true;
        }

        private void dtpApp_DateTime_StartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
                dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

                dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
                dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void dtpApp_DateTime_StartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
                dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

                dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));
                dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void numApp_DateTime_Duration_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan _tempTS = new TimeSpan(Convert.ToInt64(numApp_DateTime_Duration.Value) * 600000000);

                string _ActTime = dtpApp_DateTime_StartTime.Value.ToShortTimeString();
                dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));
                dtpApp_DateTime_StartDate.Value = Convert.ToDateTime(string.Format(dtpApp_DateTime_StartDate.Value.ToShortDateString() + " " + _ActTime, "MM/dd/yyyy hh:mm:ss tt"));

                dtpApp_DateTime_EndDate.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));
                dtpApp_DateTime_EndTime.Value = dtpApp_DateTime_StartTime.Value.AddMinutes(Convert.ToDouble(_tempTS.TotalMinutes));

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        #endregion

        #region "Tool Strip Buttons Events"

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbExpiredAuthorization.Checked == true)
                {
                    gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Add; // Code Start - Added by kanchan on 20091205
                    // for getting HL7 message name is this action for S12/S13
                    if (SaveRecallAppointment() == true)
                    {
                        this.Close();
                    }
                }
                else if (rbRescheduleRecall.Checked == true)
                {
                    gloHL7._AppointmentHL7Flag = HL7AppointmentFlag.Update ; // Code Start - Added by kanchan on 20091205
                    // for getting HL7 message name is this action for S12/S13
                    if (SaveRescheduleAppointments() == true)
                    {
                        this.Close();  
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
        }

       
        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DesignGrid();

                if (rbExpiredAuthorization.Checked == true)
                {
                    ShowExpiredAuthorization();
                }
                else if (rbRescheduleRecall.Checked == true)
                {
                    ShowRescheduleRecall();
                }

                if (C1Patients.Rows.Count <= 1)
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
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion

        private bool SaveRecallAppointment()
        {
            if (ValidateDataRecallAppointment() == false)
            {
                return false; 
            }

            MasterAppointment oMasterAppointment = new MasterAppointment();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            Int64 _result = 0;
            try
            {
           
                #region "Master Appointment Data"
                
                oMasterAppointment.MasterID = 0;
               
                oMasterAppointment.IsRecurrence = SingleRecurrence.Single;
                oMasterAppointment.ASFlag = AppointmentScheduleFlag.Appointment;  

                oMasterAppointment.AppointmentTypeID = Convert.ToInt64(cmbApp_AppointmentType.SelectedValue);
                oMasterAppointment.AppointmentTypeCode = cmbApp_AppointmentType.Text;//Remark
                oMasterAppointment.AppointmentTypeDesc = cmbApp_AppointmentType.Text;

                oMasterAppointment.ASBaseID = Convert.ToInt64(cmbApp_Provider.SelectedValue);
                oMasterAppointment.ASBaseCode = cmbApp_Provider.Text; //Remark
                oMasterAppointment.ASBaseDescription = cmbApp_Provider.Text;
                oMasterAppointment.ASBaseFlag = ASBaseType.Provider;

                oMasterAppointment.ReferralProviderID = 0;
                oMasterAppointment.ReferralProviderCode = ""; //Remark
                oMasterAppointment.ReferralProviderName = "";

                oMasterAppointment.PatientID = Convert.ToInt64(C1Patients.GetData(C1Patients.Row,0));

                oMasterAppointment.StartDate = dtpApp_DateTime_StartDate.Value;
                oMasterAppointment.StartTime = dtpApp_DateTime_StartTime.Value;
                oMasterAppointment.EndDate = dtpApp_DateTime_EndDate.Value;
                oMasterAppointment.EndTime = dtpApp_DateTime_EndTime.Value;
                oMasterAppointment.Duration = numApp_DateTime_Duration.Value;
                oMasterAppointment.ColorCode = lblApp_DateTime_ColorContainer.BackColor.ToArgb();


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

                oMasterAppointment.Notes = txtApp_Notes.Text;
                oMasterAppointment.ClinicID = _clinicId;
                oMasterAppointment.UsedStatus = ASUsedStatus.NotUsed;

                #endregion

                #region "Appointment Criteria"
                //2.1 Criteria Flag

               
                oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                //2.2 If single appointment then setup -- 
                oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_StartDate.Value.ToString("MM/dd/yyyy"));
                oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_StartTime.Value.ToString("hh:mm tt"));
                oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(dtpApp_DateTime_EndDate.Value.ToString("MM/dd/yyyy"));
                oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(dtpApp_DateTime_EndTime.Value.ToString("hh:mm tt"));
                oMasterAppointment.Criteria.SingleCriteria.Duration = numApp_DateTime_Duration.Value;

                #endregion

                _result = ogloAppointment.Add(oMasterAppointment);

                if (_result > 0)
                {
                    return true; 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oMasterAppointment.Dispose();
                ogloAppointment.Dispose();
            }
            return false; 
        }

        private bool ValidateDataRecallAppointment()
        {
            bool _result = true;

            //Provider
            if (cmbApp_Provider.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
            }
            //Location
            else if (cmbApp_Location.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a location.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;

            }

            //Time
            else if
            (numApp_DateTime_Duration.Value < 1)
            {
                MessageBox.Show("Please select a valid duration.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
            }

            if (C1Patients.Row < 1)
            {
                MessageBox.Show("Please select a patient.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
            }


            return _result;
        }

        private bool SaveRescheduleAppointments()
        {
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);  
            try
            {
                for (int i = 1; i < C1Patients.Rows.Count; i++)
                {
                    Int64 _nAppointmentMasterID = 0;
                    Int64 _nAppointmentDetailID = 0;
                    SingleRecurrence _IsSingleRecurrence = SingleRecurrence.Single;
                    DateTime _RescheduleDate;

                    if (Convert.ToBoolean(C1Patients.GetData(i,COL_RESCH_SELECT)) == true)
                    {
                        _nAppointmentMasterID = Convert.ToInt64(C1Patients.GetData(i,COL_RESCH_APPOINTMENTID_MST));
                        _nAppointmentDetailID = Convert.ToInt64(C1Patients.GetData(i, COL_RESCH_APPOINTMENTID_DTL));
                        _IsSingleRecurrence = (SingleRecurrence)Convert.ToInt32(C1Patients.GetData(i, COL_RESCH_APPOINTMENTFLAG));
                        _RescheduleDate = dtpRescheduleDate.Value;
                        ogloAppointment.RescheduleAppointment(_nAppointmentMasterID, _nAppointmentDetailID, _RescheduleDate, _IsSingleRecurrence);     
                        
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return true; 
        }


        private void ShowExpiredAuthorization()
        {
            DataTable dtPatients = new DataTable();
            dtPatients = GetExpiredAuthorizationPatient();
            if (dtPatients != null)
            {
                for (int i = 0; i < dtPatients.Rows.Count; i++)
                {
                    Int32 RowIndex = C1Patients.Rows.Count;
                    C1Patients.Rows.Add();

                    C1Patients.SetData(RowIndex, COL_AUTH_PATINETID, Convert.ToString(dtPatients.Rows[i]["nPatientID"]));
                    C1Patients.SetData(RowIndex, COL_AUTH_PATINETNAME, Convert.ToString(dtPatients.Rows[i]["sPatientName"]));
                    C1Patients.SetData(RowIndex, COL_AUTH_DATE, Convert.ToDateTime(dtPatients.Rows[i]["dtAuthorization"]).ToShortDateString());
                    C1Patients.SetData(RowIndex, COL_AUTH_EXPIRYDATE, Convert.ToDateTime(dtPatients.Rows[i]["dtAuthorizationThrough"]).ToShortDateString());
                }
            }

        }

        private void ShowRescheduleRecall()
        {
            DataTable dtAppointments = new DataTable();
            dtAppointments = GetAppointmentsForReschedule();

            if (dtAppointments != null)
            {
                Int32 RowIndex = 0;
                for (int i = 0; i < dtAppointments.Rows.Count; i++)
                {
                    RowIndex = C1Patients.Rows.Count;
                    C1Patients.Rows.Add();
                   
                    TimeSpan tsDuration = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"])).Subtract(gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"]))); 

                    C1Patients.SetData(RowIndex, COL_RESCH_APPOINTMENTID_MST, Convert.ToString(dtAppointments.Rows[i]["nMSTAppointmentID"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_APPOINTMENTID_DTL, Convert.ToString(dtAppointments.Rows[i]["nDTLAppointmentID"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_PATIENTID, Convert.ToString(dtAppointments.Rows[i]["nPatientID"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_PATIENTNAME, Convert.ToString(dtAppointments.Rows[i]["sPatientName"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_STARTDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtStartDate"])).ToShortDateString());
                    C1Patients.SetData(RowIndex, COL_RESCH_ENDDATE, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtAppointments.Rows[i]["dtEndDate"])).ToShortDateString());
                    C1Patients.SetData(RowIndex, COL_RESCH_STARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtStartTime"])).ToShortTimeString());
                    C1Patients.SetData(RowIndex, COL_RESCH_ENDTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtAppointments.Rows[i]["dtEndTime"])).ToShortTimeString());
                    C1Patients.SetData(RowIndex, COL_RESCH_DURATION, tsDuration.TotalMinutes.ToString());
                    C1Patients.SetData(RowIndex, COL_RESCH_PROVIDERID, Convert.ToString(dtAppointments.Rows[i]["nProviderID"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_PROVIDER, Convert.ToString(dtAppointments.Rows[i]["sProviderName"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_LOCATION, Convert.ToString(dtAppointments.Rows[i]["sLocationName"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_DEPARTMENT, Convert.ToString(dtAppointments.Rows[i]["sDepartmentName"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_APPOINTMENTTYPE, Convert.ToString(dtAppointments.Rows[i]["sAppointmentType"]));
                    C1Patients.SetData(RowIndex, COL_RESCH_APPOINTMENTFLAG, Convert.ToString(dtAppointments.Rows[i]["bIsSingleRecurrence"]));
                }
            }
        }

        //Get Appointments For Reschedule
        private DataTable GetAppointmentsForReschedule()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            DataTable dt = new DataTable();
            try
            {

                string strSQL = "SELECT AS_Appointment_MST.nMSTAppointmentID, AS_Appointment_DTL.nDTLAppointmentID, ISNULL(AS_Appointment_MST.nPatientID,0) AS  nPatientID,"
                + " ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') AS sPatientName,"
                + " ISNULL(AS_Appointment_DTL.sLocationName,'') AS sLocationName,ISNULL(AS_Appointment_DTL.sDepartmentName,'') AS sDepartmentName,"
                + " AS_Appointment_DTL.dtStartDate, AS_Appointment_DTL.dtStartTime, AS_Appointment_DTL.dtEndDate, AS_Appointment_DTL.dtEndTime, "
                + " ISNULL(AS_Appointment_DTL.nASBaseID,0) AS nProviderID, ISNULL(AS_Appointment_DTL.sASBaseDesc,'') AS sProviderName,"
                + " ISNULL(AS_Appointment_MST.sAppointmentTypeDesc,'') AS sAppointmentType,AS_Appointment_DTL.bIsSingleRecurrence"
                + " FROM AS_Appointment_MST  WITH(NOLOCK) INNER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID "
                + " INNER JOIN Patient  WITH(NOLOCK) ON AS_Appointment_MST.nPatientID = Patient.nPatientID"
                + " WHERE  (AS_Appointment_MST.nClinicID = "+ _clinicId +") AND (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") "
                + " AND ((AS_Appointment_DTL.dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(dtpReSchStartDate.Value.ToShortDateString()) + ") AND (AS_Appointment_DTL.dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(dtpReSchEndDate.Value.ToShortDateString()) + ")) AND (ISNULL(Patient.nExemptFromReport,0) <> 1)";    //GLO2010-0006070 : Added this condition (Patient.nExemptFromReport <> 1) so that if Exempt from Report is check-on Patient will not display on report
                
                if (cmbReSchProviders.Items.Count > 0)
                {
                    string _strProviderIDs = "";
                    for (int i = 0; i < cmbReSchProviders.Items.Count; i++)
                    {
                        cmbReSchProviders.SelectedIndex = i;
                        cmbReSchProviders.Refresh();
                        if (i == 0)
                        {
                            _strProviderIDs = "(" + Convert.ToInt64(cmbReSchProviders.SelectedValue);
                        }
                        else
                        {
                            _strProviderIDs += "," + Convert.ToInt64(cmbReSchProviders.SelectedValue);
                        }

                        if (i == cmbReSchProviders.Items.Count - 1)
                        {
                            _strProviderIDs += ")";
                        }
                    }

                    if (_strProviderIDs != "")
                        strSQL += " AND (AS_Appointment_DTL.nASBaseID IN " + _strProviderIDs  +") ";
                }

                if (cmbReSchLocation.Text.Trim() != "")
                {
                    strSQL += " AND (AS_Appointment_DTL.sLocationName = '" + cmbReSchLocation.Text.Trim() + "') ";
                }

                if (cmbReSchDepartment.Text.Trim() != "")
                {
                    strSQL += " AND (AS_Appointment_DTL.sDepartmentName = '" + cmbReSchDepartment.Text.Trim() + "') ";
                }

                if (cmbReSchAppointmentType.Text.Trim() != "")
                {
                    strSQL += " AND (AS_Appointment_MST.sAppointmentTypeDesc = '" + cmbReSchAppointmentType.Text.Trim() + "') ";
                }

            
                oDB.Connect(false);
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

        //Get Patient with Expired Authorization
        private DataTable GetExpiredAuthorizationPatient()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dt = new DataTable();
            try
            {

                oDB.Connect(false);
                strSQL = "SELECT DISTINCT PatientPriorAuthorization.nAuthorizationID, ISNULL(PatientPriorAuthorization.nPatientID,0) AS nPatientID, ISNULL(PatientPriorAuthorization.nInsuranceID,0) AS nInsuranceID, "
                        + " ISNULL(PatientPriorAuthorization.sInsuranceName,'') AS sInsuranceName, PatientPriorAuthorization.dtAuthorization,PatientPriorAuthorization.dtAuthorizationThrough,"
                        + " ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') As sPatientName "
                        + " FROM PatientPriorAuthorization  WITH(NOLOCK) INNER JOIN Patient  WITH(NOLOCK)  ON PatientPriorAuthorization.nPatientID = Patient.nPatientID"
                        + " WHERE dtAuthorizationThrough < '" + DateTime.Now.ToShortDateString() + "' AND (ISNULL(Patient.nExemptFromReport,0) <> 1)";       //GLO2010-0006070 : Added this condition (Patient.nExemptFromReport <> 1) so that if Exempt from Report is check-on Patient will not display on report
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
        
        #region "C1 Grid Design"

        private void DesignGrid()
        {
            try
            {

                C1Patients.Clear(ClearFlags.All);
                if (rbExpiredAuthorization.Checked == true)
                {

                    C1Patients.Rows.Count = 1;
                    C1Patients.Cols.Count = COL_AUTH_COUNT;

                    C1Patients.SetData(0, COL_AUTH_PATINETID, "Patient ID");
                    C1Patients.SetData(0, COL_AUTH_PATINETNAME, "Patient Name");
                    C1Patients.SetData(0, COL_AUTH_DATE, "Authorization Date");
                    C1Patients.SetData(0, COL_AUTH_EXPIRYDATE, "Authorization Expiry Date");

                    C1Patients.Cols[COL_AUTH_PATINETID].Visible = false;

                    Int32 _width = pnlc1Grid.Width - 15;
                    C1Patients.Cols[COL_AUTH_PATINETID].Width = 0;
                    C1Patients.Cols[COL_AUTH_PATINETNAME].Width = Convert.ToInt32(_width * 0.5);
                    C1Patients.Cols[COL_AUTH_DATE].Width = Convert.ToInt32(_width * 0.2);
                    C1Patients.Cols[COL_AUTH_EXPIRYDATE].Width = Convert.ToInt32(_width * 0.2);
                }
                else if (rbRescheduleRecall.Checked == true)
                {
                    C1Patients.Rows.Count = 1;
                    C1Patients.Cols.Count = COL_RESCH_COUNT;

                    C1Patients.SetData(0, COL_RESCH_SELECT, "Select");
                    C1Patients.SetData(0, COL_RESCH_APPOINTMENTID_MST, "Appointment Master ID");
                    C1Patients.SetData(0, COL_RESCH_APPOINTMENTID_DTL, "Appointment Details ID");
                    C1Patients.SetData(0, COL_RESCH_PATIENTID, "Patient ID");
                    C1Patients.SetData(0, COL_RESCH_PATIENTNAME, "Patient Name");
                    C1Patients.SetData(0, COL_RESCH_STARTDATE, "Appointment Date");
                    C1Patients.SetData(0, COL_RESCH_ENDDATE, "End Date");
                    C1Patients.SetData(0, COL_RESCH_STARTTIME, "Start Time");
                    C1Patients.SetData(0, COL_RESCH_ENDTIME, "End Time");
                    C1Patients.SetData(0, COL_RESCH_DURATION, "Duration");
                    C1Patients.SetData(0, COL_RESCH_PROVIDERID, "Provider ID");
                    C1Patients.SetData(0, COL_RESCH_PROVIDER, "Provider Name");
                    C1Patients.SetData(0, COL_RESCH_LOCATION, "Location");
                    C1Patients.SetData(0, COL_RESCH_DEPARTMENT, "Department");
                    C1Patients.SetData(0, COL_RESCH_APPOINTMENTTYPE, "Appointment Type");
                    C1Patients.SetData(0, COL_RESCH_APPOINTMENTFLAG, "Appointment Flag");

                    C1Patients.Cols[COL_RESCH_SELECT].DataType = typeof(System.Boolean);

                    C1Patients.Cols[COL_RESCH_SELECT].Visible = true;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_MST].Visible = false;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_DTL].Visible = false;
                    C1Patients.Cols[COL_RESCH_PATIENTID].Visible = false;
                    C1Patients.Cols[COL_RESCH_PATIENTNAME].Visible = true;
                    C1Patients.Cols[COL_RESCH_STARTDATE].Visible = true;
                    C1Patients.Cols[COL_RESCH_ENDDATE].Visible = false;
                    C1Patients.Cols[COL_RESCH_STARTTIME].Visible = true;
                    C1Patients.Cols[COL_RESCH_ENDTIME].Visible = true;
                    C1Patients.Cols[COL_RESCH_DURATION].Visible = true;
                    C1Patients.Cols[COL_RESCH_PROVIDERID].Visible = false;
                    C1Patients.Cols[COL_RESCH_PROVIDER].Visible = true;
                    C1Patients.Cols[COL_RESCH_LOCATION].Visible = true;
                    C1Patients.Cols[COL_RESCH_DEPARTMENT].Visible = true;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTTYPE].Visible = true;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTFLAG].Visible = false;

                    C1Patients.AllowEditing = true;

                    C1Patients.Cols[COL_RESCH_SELECT].AllowEditing = true;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_MST].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_DTL].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_PATIENTID].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_PATIENTNAME].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_STARTDATE].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_ENDDATE].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_STARTTIME].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_ENDTIME].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_DURATION].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_PROVIDERID].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_PROVIDER].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_LOCATION].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_DEPARTMENT].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTTYPE].AllowEditing = false;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTFLAG].AllowEditing = false;


                    C1Patients.Cols[COL_RESCH_SELECT].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_MST].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTID_DTL].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_PATIENTID].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_PATIENTNAME].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_STARTDATE].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_ENDDATE].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_STARTTIME].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_ENDTIME].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_DURATION].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_PROVIDERID].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_PROVIDER].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_LOCATION].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_DEPARTMENT].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTTYPE].TextAlign = TextAlignEnum.LeftCenter;
                    C1Patients.Cols[COL_RESCH_APPOINTMENTFLAG].TextAlign = TextAlignEnum.LeftCenter;


                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void ClearGrid()
        {
            C1Patients.Clear(ClearFlags.All);
            C1Patients.Rows.Count = 1;
            C1Patients.Cols.Count = 0;
        }

        #endregion

        #region "Designer Events"

        //<<<<<<<<<<Ojeswini>>>>>>>>>>>>>>>>>>
        private void rbIncopleteOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIncopleteOrder.Checked == true)
            {
                rbIncopleteOrder.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;
            }
            else
            {

                rbIncopleteOrder.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid();
        }

        private void rbTestResult_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTestResult.Checked == true)
            {
                rbTestResult.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;
            }
            else
            {

                rbTestResult.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid();
        }

        private void rbExpiredAuthorization_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpiredAuthorization.Checked == true)
            {
                rbExpiredAuthorization.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;

            }
            else
            {

                rbExpiredAuthorization.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid();
        }

        private void rbExhaustedPrescription_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExhaustedPrescription.Checked == true)
            {
                rbExhaustedPrescription.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;
            }
            else
            {

                rbExhaustedPrescription.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid(); 
        }

        private void rbRescheduleRecall_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRescheduleRecall.Checked == true)
            {
                rbRescheduleRecall.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = true;
                pnlRecallAppointment.Visible = false;  
            }
            else
            {

                rbRescheduleRecall.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = true;
            }
            ClearGrid();
        }

        private void rbRoutinePatient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRoutinePatient.Checked == true)
            {
                rbRoutinePatient.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                pnlRescheduleCriteria.Visible = false;
                pnlRecallAppointment.Visible = false;
            }
            else
            {

                rbRoutinePatient.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            ClearGrid();
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

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (C1Patients != null && C1Patients.Rows.Count > 1)
            {
                ExportReportToExcel(false);
               
            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {
            if (C1Patients != null && C1Patients.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }


        private void ExportReportToExcel(bool OpenReport)
        {
      //      gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
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

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

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
                    
                    saveFileDialog.InitialDirectory = _DefaultLocationPath;

                }

                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                    return;
                }
                _FilePath = saveFileDialog.FileName;


                C1Patients.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    {
                        System.Diagnostics.Process.Start(_FilePath);
                    }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog.Dispose();
                saveFileDialog = null;

            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ioEx.ToString(), false);
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void C1Patients_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

       
        



        #endregion
       
    }
}