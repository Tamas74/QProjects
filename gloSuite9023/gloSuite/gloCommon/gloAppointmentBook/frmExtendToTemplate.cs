using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook.Books;
using gloDatabaseLayer;
using System.Collections;

namespace gloAppointmentBook
{
    public partial class frmExtendToTemplate : Form
    {

        #region Variable Declaration

        private string _MessageBoxCaption = string.Empty;
        private string _databaseConnectionString = "";
        private Int64 _ProviderID = 0;
        DateTime _dtStartdate;
        DateTime _dtEnddate;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion

        #region Properties

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region Constructor

        public frmExtendToTemplate(Int64 ProviderID, DateTime _Startdate, DateTime _Enddate, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _ProviderID = ProviderID;
            _dtStartdate = _Startdate;
            _dtEnddate = _Enddate;
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


        #endregion

        #region Form load method
        private void frmExtendToTemplate_Load(object sender, EventArgs e)
        {
            FillProvider();
            FillTemplates();
            Fill_TemplateAppointments();

        }
        #endregion

        #region Fill Methods

        private void FillTemplates()
        {
            try
            {
                DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                String _strSQL = "";
                DataTable dtTemplates = null;

                _strSQL = " SELECT distinct  AB_AppointmentTemplate_Allocation.sTemplateName,AB_AppointmentTemplate_MST.nAppointmentTemplateID  " +
                " FROM AB_AppointmentTemplate_Allocation   WITH(NOLOCK) inner join AB_AppointmentTemplate_MST  WITH(NOLOCK) on AB_AppointmentTemplate_Allocation.sTemplateName = AB_AppointmentTemplate_MST.sAppointmentTemplates  WHERE (AB_AppointmentTemplate_Allocation.dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(_dtStartdate.ToString()) + " ) AND (AB_AppointmentTemplate_Allocation.dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(_dtEnddate.ToString()) + " )" +
                "AND (AB_AppointmentTemplate_Allocation.dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(_dtStartdate.ToString()) + " ) AND (AB_AppointmentTemplate_Allocation.dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_dtEnddate.ToString()) + " ) AND  AB_AppointmentTemplate_MST.nClinicID = " + this.ClinicID + " AND AB_AppointmentTemplate_Allocation.nASBaseID = " + _ProviderID + " ";
                oDB.Connect(false);

                oDB.Retrive_Query(_strSQL, out dtTemplates);
                _strSQL = null;
                oDB.Disconnect();
                if (dtTemplates != null)
                {
                    cmbTemplates.DataSource = dtTemplates;
                    cmbTemplates.DisplayMember = dtTemplates.Columns["sTemplateName"].ColumnName;
                    cmbTemplates.ValueMember = dtTemplates.Columns["nAppointmentTemplateID"].ColumnName;
                }
                cmbTemplates.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillProvider()
        {
            try
            {
                DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                String _strSQL = "";
                DataTable dtProvider = null;

                if (this._ClinicID == 0)
                    _strSQL = "SELECT  nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST   WITH(NOLOCK) WHERE nProviderID = " + _ProviderID + " ORDER BY ProviderName";
                else
                    _strSQL = "SELECT nProviderID, (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WITH(NOLOCK)  WHERE  nProviderID = " + _ProviderID + " AND nClinicID = " + this.ClinicID + " ORDER BY ProviderName";


                oDB.Connect(false);

                oDB.Retrive_Query(_strSQL, out dtProvider);
                _strSQL = null;
                oDB.Disconnect();

                if (dtProvider != null)
                {
                    cmbProvider.DataSource = dtProvider;
                    cmbProvider.ValueMember = dtProvider.Columns["nProviderID"].ColumnName;
                    cmbProvider.DisplayMember = dtProvider.Columns["ProviderName"].ColumnName;
                    cmbProvider.Refresh();
                    cmbProvider.SelectedIndex = -1;
                }
                dtProvider = null;

                if (_ProviderID != 0)
                {
                    cmbProvider.SelectedValue = _ProviderID;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void Fill_TemplateAppointments()
        {


            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            AppointmentTemplate oAppointmentTemplate = null;
            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            DataTable dtAllocations = null;
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            dtAllocations = ogloTemplate.GetTemplateAllocations(_ProviderID, _dtStartdate, _dtEnddate);
            CalendarTemplate.Appointments.Clear();
            try
            {
                if (dtAllocations != null)
                {
                    for (int i = 0; i < dtAllocations.Rows.Count; i++)
                    {
                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Appointment.Text = Convert.ToString(dtAllocations.Rows[i]["sAppointmentTypeDesc"]) + " - " + Convert.ToString(dtAllocations.Rows[i]["sTemplateName"]);
                        oJUC_Appointment.Description = "";
                        oJUC_Appointment.Prefix = "";
                        oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(Convert.ToInt32(dtAllocations.Rows[i]["nColorCode"].ToString()));

                        // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                        oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);

                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtStartDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtStartTime"].ToString()));
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtEndDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtEndTime"].ToString()));
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtEndDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtEndTime"].ToString()));
                                oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtStartDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtStartTime"].ToString()));
                            }
                            catch
                            {
                            }
                        }

                        CalendarTemplate.Appointments.Add(oJUC_Appointment);
                    }
                }
                CalendarTemplate.Date = _dtStartdate;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                ogloAppointmentTemplate.Dispose();
                if (oAppointmentTemplate != null)
                {
                    oAppointmentTemplate.Dispose();
                }
                oJUC_Appointment = null;
                if (dtAllocations != null) { dtAllocations.Dispose(); dtAllocations = null; }
            }



        }

        #endregion

        #region Save method and supporting method

        private bool SaveAllocation()
        {
            bool Result = false;
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            AppointmentTemplate oAppointmentTemplate = new AppointmentTemplate();
            ArrayList Dates = null;
            String sDate = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            Object MasterTemplateID = null;
            String _sqlQuery = "";
            Object _IsMasterIDExist = null;
            try
            {
                //AppointmentTemplateAllocation oAllocation;
                //AppointmentTemplateAllocations oAppointmentTemplateAllocations = new AppointmentTemplateAllocations();
                if (cmbTemplates != null)
                {
                    //&& cmbTemplates.SelectedIndex >= 0
                    for (int j = 0; j < cmbTemplates.Items.Count; j++)
                    {
                        cmbTemplates.SelectedIndex = j;
                        cmbTemplates.Refresh();
                        oAppointmentTemplate = ogloTemplate.GetTemplate(Convert.ToInt64(cmbTemplates.SelectedValue));

                        if (oAppointmentTemplate != null)
                        {
                            Dates = GetPattern();

                            for (int k = 0; k < Dates.Count; k++)
                            {
                                if (sDate == "")
                                {
                                    sDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(Dates[k]).ToShortDateString()).ToString();
                                }
                                else
                                {
                                    sDate = sDate + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToDateTime(Dates[k]).ToShortDateString()).ToString();
                                }

                                //for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                                //{
                                //    oAllocation = new AppointmentTemplateAllocation();

                                //    oAllocation.TemplateAllocationMasterID = Convert.ToInt64(cmbTemplates.SelectedValue);
                                //    oAllocation.TemplateAllocationID = 0;
                                //    oAllocation.LineNumber = 0;
                                //    oAllocation.TemplateName = Convert.ToString(cmbTemplates.Text);
                                //    oAllocation.ASBaseID = Convert.ToInt64(cmbProvider.SelectedValue);
                                //    oAllocation.ASBaseCode = "";
                                //    oAllocation.ASBaseDesc = Convert.ToString(cmbProvider.Text);
                                //    oAllocation.ASBaseFlag = 1;
                                //    oAllocation.AppointmentTypeID = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeID;
                                //    oAllocation.AppointmentTypeCode = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeCode;
                                //    oAllocation.AppointmentTypeDesc = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                                //    oAllocation.StartDate = Convert.ToDateTime(Dates[k]);
                                //    oAllocation.StartTime = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(Dates[k]), oAppointmentTemplate.TemplateDetails[i].StartTime);
                                //    oAllocation.EndDate = Convert.ToDateTime(Dates[k]);
                                //    oAllocation.EndTime = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(Dates[k]), oAppointmentTemplate.TemplateDetails[i].EndTime);
                                //    oAllocation.ColorCode = oAppointmentTemplate.TemplateDetails[i].ColorCode;
                                //    oAllocation.LocationID = oAppointmentTemplate.TemplateDetails[i].LocationID;
                                //    oAllocation.LocationName = oAppointmentTemplate.TemplateDetails[i].LocationName;
                                //    oAllocation.DepartmentID = oAppointmentTemplate.TemplateDetails[i].DepartmentID;
                                //    oAllocation.DepartmentName = oAppointmentTemplate.TemplateDetails[i].DepartmentName;
                                //    oAllocation.ClinicID = this.ClinicID;

                                //    oAppointmentTemplateAllocations.Add(oAllocation);
                                //    oAllocation = null;
                                //    Result = true;
                                //}
                            }
                        }
                    }
                    //ogloTemplate.AddTemplateAllocations(oAppointmentTemplateAllocations);

                    oDB.Connect(false);

                    _sqlQuery = "";
                    _sqlQuery = "SELECT ISNULL(nTemplateAllocationMasterID,0) AS nTemplateAllocationMasterID FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) WHERE convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18)," + oDB.GetPrefixTransactionID(0) + ")+ '%'";
                    _IsMasterIDExist = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_IsMasterIDExist != null && Convert.ToString(_IsMasterIDExist) != "")
                    {
                        _sqlQuery = "";
                        _sqlQuery = "SELECT isnull(max(nTemplateAllocationMasterID),0)+1 FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) where convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18),@MachineID)+ '%'";

                        MasterTemplateID = oDB.ExecuteScalar_Query(_sqlQuery);
                    }
                    else
                    {
                        _sqlQuery = "";
                        _sqlQuery = "SELECT convert(numeric(18,0), convert(varchar(18)," + oDB.GetPrefixTransactionID(0) + ") + '001')";

                        MasterTemplateID = oDB.ExecuteScalar_Query(_sqlQuery);
                    }


                    if (MasterTemplateID == null)
                    {
                        return false;
                    }


                    oDBParameters = new DBParameters();
                    oDBParameters.Add("@nTemplateAllocationID", Convert.ToInt64(cmbTemplates.SelectedValue), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sDate", sDate, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nASBaseID", Convert.ToInt64(cmbProvider.SelectedValue), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sASBaseDesc", Convert.ToString(cmbProvider.Text), ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@nMasterID", Convert.ToInt64(MasterTemplateID), ParameterDirection.Input, SqlDbType.BigInt);
                    int retVal = oDB.Execute("AB_INUP_AppointmentTemplateAllocations", oDBParameters);
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                Result = false;
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (oAppointmentTemplate != null) { oAppointmentTemplate.Dispose(); oAppointmentTemplate = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                Dates = null;
                sDate = null;
                MasterTemplateID = null;
                _sqlQuery = null;
                _IsMasterIDExist = null;
            }
            return Result;
        }

        private ArrayList GetPattern()
        {
            ArrayList Dates = new ArrayList();
            DateTime InStartDate;
            DateTime InEndDate;
            try
            {

                InStartDate = dtpStartDate.Value;
                InEndDate = dtpEndDate.Value;

                for (DateTime dt = InStartDate.Date; dt <= InEndDate.Date; dt = dt.AddDays(1))
                {
                    Dates.Add(dt);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Dates;
        }

        #endregion

        #region  Button Click Events

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                MessageBox.Show("End date should be after start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpStartDate.Focus();
                return;
            }
            SaveAllocation();

        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion









    }
}