using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook.Books;
using C1.Win.C1FlexGrid;
using gloAppointmentBook;
using System.IO;
using gloAuditTrail;
using gloDatabaseLayer;

namespace gloAppointmentBook
{
    public partial class frmSetupTemplateAllocation : Form
    {
        #region "Variable Declaration"

        private string _MessageBoxCaption = string.Empty;
        private string _databaseConnectionString = "";
        private Int64 _AllocationID = 0;
        private Int64 _ProviderID = 0;
        private Int32 _rowIndex = 0;
        private DataTable _dtTemplates = new DataTable();
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _nTemplateAllocationMasterID = 0;

        #endregion

        #region Properties

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        DateTime _SelectedStartDate = DateTime.Today;
        public DateTime SelectedStartDate
        {
            get { return _SelectedStartDate; }
            set { _SelectedStartDate = value; }
        }

        DateTime _SelectedEndDate = DateTime.Today;
        public DateTime SelectedEndDate
        {
            get { return _SelectedEndDate; }
            set { _SelectedEndDate = value; }
        }

        #endregion

        #region Constructor

        public frmSetupTemplateAllocation(Int64 ProviderID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _ProviderID = ProviderID;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        public frmSetupTemplateAllocation(Int64 ProviderID, Int64 AllocationID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _AllocationID = AllocationID;
            _ProviderID = ProviderID;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        //private void frmSetupTemplateAllocation_Load(object sender, EventArgs e)
        //{
        //    DesignAllocationGrid();
        //    DesignRecurrnceGrid();
        //    FillProviders();
        //    FillTemplates();
        //    if (_AllocationID != 0)
        //    {
        //        FillAllocation();
        //        rbRecurrence.Enabled = false;
        //        rbSimple.Enabled = false;
        //    }

        //    pnlTemplateAllocation.Enabled = false;
        //    btnAdd.Enabled = true;
        //    btnRemove.Enabled = true;
        //    rbSimple_CheckedChanged(null, null);

        //    numFromMonth.Value = DateTime.Now.Month;
        //    numFromYear.Value = DateTime.Now.Year;
        //    numToMonth.Value = DateTime.Now.Month;
        //    numToYear.Value = DateTime.Now.Year;
        //}

        private void frmSetupTemplateAllocation_Load(object sender, EventArgs e)
        {
            FillProviders();
            FillTemplates();
            rbRecurrence.Checked = false;
            rbSimple.Enabled = true;
            DesignRecurrnceGrid();

            ShowForSimple();
            
            //btnAdd.Enabled = true;
            //btnRemove.Enabled = true;
            //rbSimple_CheckedChanged(null, null);

            numFromMonth.Value = SelectedStartDate.Month; //DateTime.Now.Month;
            numFromYear.Value = SelectedStartDate.Year; //DateTime.Now.Year;

            numToMonth.Value = SelectedEndDate.Month; //DateTime.Now.Month;
            numToYear.Value = SelectedEndDate.Year; //DateTime.Now.Year;

            dtpStartDate.Value = SelectedStartDate;
            dtpEndDate.Value = SelectedEndDate;
        }

        private void FillTemplates()
        {
            gloAppointmentTemplate ogloTemplate = null;
            try
            {
                ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                DataTable dtTemplates = ogloTemplate.GetTemplates();
                if (dtTemplates != null)
                {
                    cmbTemplates.DisplayMember = dtTemplates.Columns["sAppointmentTemplates"].ColumnName;
                    cmbTemplates.ValueMember = dtTemplates.Columns["nAppointmentTemplateID"].ColumnName;
                    cmbTemplates.DataSource = dtTemplates;
                }
                cmbTemplates.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
            }
        }

        private void FillAllocation()
        {
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            DataTable dtAllocation = null;
            try
            {
                dtAllocation = ogloTemplate.GetTemplateAllocation(_AllocationID);
                if (dtAllocation != null)
                {
                    if (dtAllocation.Rows.Count > 0)
                    {
                        cmbProvider.SelectedValue = Convert.ToString(dtAllocation.Rows[0]["nProviderID"]);

                        c1Template.Rows.Add();

                        c1Template.SetData(1, 0, Convert.ToString(dtAllocation.Rows[0]["nTemplateID"]));
                        c1Template.SetData(1, 1, Convert.ToString(dtAllocation.Rows[0]["sAppointmentTemplates"]));
                        c1Template.SetData(1, 2, Convert.ToString(dtAllocation.Rows[0]["StartDate"]));
                        c1Template.SetData(1, 3, Convert.ToString(dtAllocation.Rows[0]["EndDate"]));
                        c1Template.SetData(1, 4, Convert.ToString(dtAllocation.Rows[0]["nTemplateAllocationID"]));

                        cmbProvider.Enabled = false;
                        _AllocationID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (dtAllocation != null) { dtAllocation.Dispose(); dtAllocation = null; }
            }
        }

        private void FillProviders()
        {
            try
            {

                DataTable dt;
                // Fill Providers in the Combo Box
                Resource oProvider = new Resource(_databaseConnectionString);
                dt = oProvider.GetProviders();

                //nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, imgSignature, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode , (ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') + ' ' +ISNULL(sLastName,'')) AS ProviderName
                if (dt != null)
                {
                    cmbProvider.DataSource = dt;
                    cmbProvider.ValueMember = dt.Columns["nProviderID"].ColumnName;
                    cmbProvider.DisplayMember = dt.Columns["ProviderName"].ColumnName;
                    cmbProvider.Refresh();
                    cmbProvider.SelectedIndex = -1;
                }
                dt = null;
                oProvider.Dispose();

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

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        bool result = false;
                        result = SaveAllocationNew();
                        if (result == true)
                        {
                            this.Close();
                        }
                        break;
                    case "Save":
                        result = false;
                        result = SaveAllocationNew();
                        if (result == true)
                        {
                            _ProviderID=0;
                            _AllocationID = 0;
                            cmbProvider.SelectedIndex = -1;
                            cmbTemplates.SelectedIndex = -1;

                            ShowForSimple();
                            rbSimple.Checked = true;

                            //Clear Recurrence Criteria  
                            DesignRecurrnceGrid();

                            numFromMonth.Value = SelectedStartDate.Month ;//DateTime.Now.Month;
                            numFromYear.Value = SelectedStartDate.Year; //DateTime.Now.Year;

                            numToMonth.Value = SelectedEndDate.Month; //DateTime.Now.Month;
                            numToYear.Value = SelectedEndDate.Year; //DateTime.Now.Year;

                            numFromYear_ValueChanged(null, null);
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "ShowRecurrence":
                        { ShowReccurance(); }
                        break;
                    case "HideRecurrence":
                        { ShowForSimple(); }
                        break;
                    case "RemoveRecurrence":
                        {
                            if (MessageBox.Show("Are you sure you want to remove Recurrence?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                ShowForSimple();
                                rbSimple.Checked = true;

                                //Clear Recurrence Criteria  
                                DesignRecurrnceGrid();

                                numFromMonth.Value = SelectedStartDate.Month; //DateTime.Now.Month;
                                numFromYear.Value = SelectedStartDate.Year; //DateTime.Now.Year;

                                numToMonth.Value = SelectedEndDate.Month; //DateTime.Now.Month;
                                numToYear.Value = SelectedEndDate.Year; //DateTime.Now.Year;

                                numFromYear_ValueChanged(null, null);
                                //----------------------------
                            }
                        }
                        break;
                    case "ApplyRecurrence":
                        { 
                            ArrayList arr=new ArrayList();
                            arr=GetPattern();
                            if (arr != null)
                            {
                                if (arr.Count == 0)
                                {
                                    MessageBox.Show(" Select date for Recurrence.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            }
                            ShowForSimple();
                            rbRecurrence.Checked = true;  
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

        //private bool SaveAllocation_1()
        //{
        //    bool Result = false;
        //    try
        //    {
        //        if (ValidateData())
        //        {
        //            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
        //            //AppointmentTemplateAllocations oAllocations = new AppointmentTemplateAllocations();
        //            AppointmentTemplateAllocation oAllocation;
        //            for (int i = 1; i < c1Template.Rows.Count; i++)
        //            {
        //                string sTemplateName = Convert.ToString(c1Template.GetData(i, 1));

        //                if (sTemplateName.Trim() != "")
        //                {
        //                    oAllocation = new AppointmentTemplateAllocation();
        //                    oAllocation.TemplateAllocationID = Convert.ToInt64(c1Template.GetData(i, 4));
        //                    oAllocation.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
        //                    oAllocation.TemplateID = Convert.ToInt64(c1Template.GetData(i, 0));
        //                    oAllocation.StartDate = Convert.ToDateTime(c1Template.GetData(i, 2));
        //                    oAllocation.EndDate = Convert.ToDateTime(c1Template.GetData(i, 3));

        //                    //for (int k = 0; k < _dtTemplates.Rows.Count; k++)
        //                    //{
        //                    //    if (sTemplateName == Convert.ToString(_dtTemplates.Rows[k]["sAppointmentTemplates"]))
        //                    //    {
        //                    //        oAllocation.TemplateID = Convert.ToInt64(_dtTemplates.Rows[k]["nAppointmentTemplateID"]);
        //                    //        break;
        //                    //    }
        //                    //}

        //                    Result = ogloTemplate.AddTemplateAllocation(oAllocation);
        //                    if (Result == false)
        //                        return Result;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    return Result;
        //}


        private bool SaveAllocation()
        {
            bool Result = false;
            gloAppointmentTemplate ogloTemplate = null;
            AppointmentTemplate oAppointmentTemplate = null;
            AppointmentTemplateAllocations oAppointmentTemplateAllocations = null;
            try
            {
                if (ValidateData())
                {
                    ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                    oAppointmentTemplate = new AppointmentTemplate();
                    AppointmentTemplateAllocation oAllocation;
                    oAppointmentTemplateAllocations = new AppointmentTemplateAllocations();

                    oAppointmentTemplate = ogloTemplate.GetTemplate(Convert.ToInt64(cmbTemplates.SelectedValue));

                    if (oAppointmentTemplate != null)
                    {
                        ArrayList Dates = GetPattern();

                        for (int k = 0; k < Dates.Count; k++)
                        {
                            for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                            {
                                oAllocation = new AppointmentTemplateAllocation();

                                oAllocation.TemplateAllocationMasterID = _nTemplateAllocationMasterID;
                                oAllocation.TemplateAllocationID = 0;
                                oAllocation.LineNumber = 0;
                                oAllocation.TemplateName = Convert.ToString(cmbTemplates.Text);
                                oAllocation.ASBaseID = Convert.ToInt64(cmbProvider.SelectedValue);
                                oAllocation.ASBaseCode = "";
                                oAllocation.ASBaseDesc = Convert.ToString(cmbProvider.Text);
                                oAllocation.ASBaseFlag = 1;
                                oAllocation.AppointmentTypeID = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeID;
                                oAllocation.AppointmentTypeCode = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeCode;
                                oAllocation.AppointmentTypeDesc = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                                oAllocation.StartDate = Convert.ToDateTime(Dates[k]);
                                oAllocation.StartTime = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(Dates[k]), oAppointmentTemplate.TemplateDetails[i].StartTime);
                                oAllocation.EndDate = Convert.ToDateTime(Dates[k]);
                                oAllocation.EndTime = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(Dates[k]), oAppointmentTemplate.TemplateDetails[i].EndTime);
                                oAllocation.ColorCode = oAppointmentTemplate.TemplateDetails[i].ColorCode;
                                oAllocation.LocationID = oAppointmentTemplate.TemplateDetails[i].LocationID;
                                oAllocation.LocationName = oAppointmentTemplate.TemplateDetails[i].LocationName;
                                oAllocation.DepartmentID = oAppointmentTemplate.TemplateDetails[i].DepartmentID;
                                oAllocation.DepartmentName = oAppointmentTemplate.TemplateDetails[i].DepartmentName;
                                oAllocation.ClinicID = this.ClinicID;

                                oAppointmentTemplateAllocations.Add(oAllocation);
                                oAllocation = null;
                                Result = true;
                            }
                        }
                        Dates = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TemplateAllocation, ActivityType.Add, "Add Template Allocation", 0, _nTemplateAllocationMasterID, _ProviderID, ActivityOutCome.Success);



                    }
                    ogloTemplate.AddTemplateAllocations(oAppointmentTemplateAllocations);
                }

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
                if (oAppointmentTemplateAllocations != null) { oAppointmentTemplateAllocations.Dispose(); oAppointmentTemplateAllocations = null; }
            }
            return Result;
        }

        private bool SaveAllocationNew()
        {
            bool Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            AppointmentTemplate oAppointmentTemplate = new AppointmentTemplate();
            gloDatabaseLayer.DBParameters oDBParameters=null;
            Object MasterTemplateID = null;
            String _sqlQuery = "";
            Object _IsMasterIDExist = null;
            try
            {
                if (ValidateData())
                {
                    // AppointmentTemplateAllocation oAllocation;
                    //AppointmentTemplateAllocations oAppointmentTemplateAllocations = new AppointmentTemplateAllocations();
                    String sDate = "";
                    oAppointmentTemplate = ogloTemplate.GetTemplate(Convert.ToInt64(cmbTemplates.SelectedValue));

                    if (oAppointmentTemplate != null)
                    {
                        ArrayList Dates = GetPattern();

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

                            //    oAllocation.TemplateAllocationMasterID = _nTemplateAllocationMasterID;
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
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TemplateAllocation, ActivityType.Add, "Add Template Allocation", 0, _nTemplateAllocationMasterID, _ProviderID, ActivityOutCome.Success);



                    }
                    //ogloTemplate.AddTemplateAllocations(oAppointmentTemplateAllocations);

                    oDB.Connect(false);


                    _sqlQuery = "";
                    _sqlQuery = "SELECT ISNULL(nTemplateAllocationMasterID,0) AS nTemplateAllocationMasterID FROM AB_AppointmentTemplate_Allocation WHERE convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18)," + oDB.GetPrefixTransactionID(0) + ")+ '%'";
                    _IsMasterIDExist = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_IsMasterIDExist != null && Convert.ToString(_IsMasterIDExist) != "")
                    {
                        _sqlQuery = "";
                        _sqlQuery = "SELECT isnull(max(nTemplateAllocationMasterID),0)+1 FROM AB_AppointmentTemplate_Allocation where convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18),@MachineID)+ '%'";

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
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                Result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (oAppointmentTemplate != null) { oAppointmentTemplate.Dispose(); oAppointmentTemplate = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                MasterTemplateID = null;
                _sqlQuery = "";
                _IsMasterIDExist = null;
            }
            return Result;
        }

        private bool ValidateData()
        {
            try
            {
                if (cmbProvider.SelectedIndex == -1)
                {
                    MessageBox.Show(" Please select a Provider.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbProvider.Focus();
                    return false;
                }
                if (cmbTemplates.SelectedIndex == -1)
                {
                    MessageBox.Show(" Please select a Template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbProvider.Focus();
                    return false;
                }

                if (rbSimple.Checked == true)
                {
                    if (dtpStartDate.Value.Date  > dtpEndDate.Value.Date )
                    {
                        MessageBox.Show("End date should be after start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpStartDate.Focus();
                        return false;
                    }
                }

                for (int i = 1; i < c1Template.Rows.Count; i++)
                {
                    string sTemplateName = Convert.ToString(c1Template.GetData(i, 1));

                    if (sTemplateName.Trim() != "")
                    {
                        if ((Convert.ToString(c1Template.GetData(i, 2)).Trim() == ""))
                        {
                            MessageBox.Show("Enter the date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Template.Row = i;
                            c1Template.Col = 2;
                            return false;
                        }
                        if ((Convert.ToString(c1Template.GetData(i, 3)).Trim() == ""))
                        {
                            MessageBox.Show("Enter the date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Template.Row = i;
                            c1Template.Col = 3;
                            return false;
                        }


                        try
                        {
                            Convert.ToDateTime(c1Template.GetData(i, 2));
                        }
                        catch
                        {
                            MessageBox.Show("Invalid date format.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Template.Row = i;
                            c1Template.Col = 2;
                            return false;
                        }

                        try
                        {
                            Convert.ToDateTime(c1Template.GetData(i, 3));
                        }
                        catch
                        {
                            MessageBox.Show("Invalid date format.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Template.Row = i;
                            c1Template.Col = 3;
                            return false;
                        }

                        if (Convert.ToDateTime(c1Template.GetData(i, 2)) > Convert.ToDateTime(c1Template.GetData(i, 3)))
                        {
                            MessageBox.Show("End date should be after start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1Template.Row = i;
                            c1Template.Col = 2;
                            return false;
                        }

                        gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                        DateTime StartDate = Convert.ToDateTime(c1Template.GetData(i, 2));
                        DateTime EndDate = Convert.ToDateTime(c1Template.GetData(i, 3));
                        Int64 AllocationID = Convert.ToInt64(c1Template.GetData(i, 4));
                        if (ogloTemplate.IsTemplateAllocated(Convert.ToInt64(cmbProvider.SelectedValue), AllocationID, StartDate, EndDate))
                        {
                            MessageBox.Show("Template already allocated between specified dates.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                    }

                    sTemplateName = null;

                    //if(Convert.ToString(c1Template.GetData(i, 2)).Trim() != "")
                    //{

                    //}

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return true;
        }

        //Add Allocation to Grid
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTemplates.SelectedIndex != -1)
                {
                    if (rbSimple.Checked == true) //Simple Allocation Criteria
                    {
                        if (dtpStartDate.Value > dtpEndDate.Value)
                        {
                            MessageBox.Show("End date should be after start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Int32 Index = 0;
                        if (_rowIndex == 0)
                        {
                            c1Template.Rows.Add();
                            Index = c1Template.Rows.Count - 1;
                        }
                        else
                        {
                            Index = _rowIndex;
                        }

                        c1Template.SetData(Index, 0, Convert.ToString(cmbTemplates.SelectedValue));
                        c1Template.SetData(Index, 1, Convert.ToString(cmbTemplates.Text));
                        c1Template.SetData(Index, 2, Convert.ToString(dtpStartDate.Text));
                        c1Template.SetData(Index, 3, Convert.ToString(dtpEndDate.Text));
                        c1Template.SetData(Index, 4, Convert.ToString(_AllocationID));

                        _rowIndex = 0;
                        _AllocationID = 0;
                        ClearControls();
                        btnCancel_Click(null, null);
                    }
                    else //Recurrence 
                    {
                        ArrayList Dates = new ArrayList();
                        Dates = FindRecurrnce();
                        c1Template.Rows.Count = 1;
                        for (int i = 0; i < Dates.Count; i++)
                        {
                            Int32 Index = 0;

                            c1Template.Rows.Add();
                            Index = c1Template.Rows.Count - 1;

                            c1Template.SetData(Index, 0, Convert.ToString(cmbTemplates.SelectedValue));
                            c1Template.SetData(Index, 1, Convert.ToString(cmbTemplates.Text));
                            c1Template.SetData(Index, 2, Convert.ToString(((DateTime)(Dates[i])).ToShortDateString()));
                            c1Template.SetData(Index, 3, Convert.ToString(((DateTime)(Dates[i])).ToShortDateString()));
                            c1Template.SetData(Index, 4, Convert.ToString(_AllocationID));

                            _rowIndex = 0;
                            _AllocationID = 0;
                        }
                        ClearControls();
                        btnCancel_Click(null, null);
                        Dates = null;
                    }
                }
                else
                {
                    MessageBox.Show("Select the template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbTemplates.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Select Row for Modification
        private void c1Template_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1Template.Row != 0)
                {
                    _rowIndex = c1Template.Row;
                    cmbTemplates.SelectedIndex = cmbTemplates.FindStringExact(Convert.ToString(c1Template.GetData(_rowIndex, 1)));
                    dtpStartDate.Value = Convert.ToDateTime(c1Template.GetData(_rowIndex, 2));
                    dtpEndDate.Value = Convert.ToDateTime(c1Template.GetData(_rowIndex, 3));
                    _AllocationID = Convert.ToInt64(c1Template.GetData(_rowIndex, 4));
                    btnAdd_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ShowForSimple()
        {
            try
            {
                pnlTemplateAllocation.Visible = true;
                pnlTemplateAllocation.BringToFront();
                tsb_Recurrence.Text = "&Recurrence";
                tsb_Recurrence.Tag = "ShowRecurrence";
                tsb_ApplyRecurrence.Visible = false;
                tsb_RemoveRecurrence.Visible = false;
                tsb_OK.Visible = true;
                tsb_Cancel.Visible = true;
                rbSimple.Checked = true;
                tsb_Save.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void ShowReccurance()
        {
            try
            {
                pnlRecMain.Visible = true;
                pnlRecMain.BringToFront();
                tsb_Recurrence.Text = "&Hide Recurrence";
                tsb_Recurrence.Tag = "HideRecurrence";
                tsb_ApplyRecurrence.Visible = true;
                tsb_RemoveRecurrence.Visible = true;
                tsb_OK.Visible = false;
                tsb_Cancel.Visible = false;
                rbRecurrence.Checked = true;
                tsb_Save.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void ClearControls()
        {
            try
            {
                cmbTemplates.SelectedIndex = -1;

                dtpStartDate.Value = SelectedStartDate; //DateTime.Now;
                dtpEndDate.Value = SelectedEndDate; //DateTime.Now;

                numFromMonth.Value = SelectedStartDate.Month; //DateTime.Now.Month;
                numFromYear.Value = SelectedStartDate.Year; //DateTime.Now.Year;

                numToMonth.Value = SelectedEndDate.Month; //DateTime.Now.Month;
                numToYear.Value = SelectedEndDate.Year; //DateTime.Now.Year;

                numFromYear_ValueChanged(null, null);

                rbSimple.Checked = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimple.Checked == true)
            {
                gbSimpleAllocation.Visible = true;
                gbRecurrncePattern.Visible = false;
                //btnSave.Location = new Point(77, 185);
                //btnCancel.Location = new Point(147, 185);
            }
            else
            {
                gbSimpleAllocation.Visible = false;
                gbRecurrncePattern.Visible = true;
                //btnSave.Location = new Point(77, 591);
                //btnCancel.Location = new Point(147, 591);
            }
        }

        #region C1 Flex Grid Design

        private void DesignAllocationGrid()
        {

            try
            {
                // c1Template.DataSource = dt;
                c1Template.Clear(ClearFlags.All);
                c1Template.Cols.Count = 5;

                c1Template.SetData(0, 0, "nTemplateID");
                c1Template.SetData(0, 1, "Template Name");
                c1Template.SetData(0, 2, "Start Date");
                c1Template.SetData(0, 3, "End Date");
                c1Template.SetData(0, 4, "AllocationID");

                c1Template.Cols[0].Visible = false;
                c1Template.Cols[1].Visible = true;
                c1Template.Cols[2].Visible = true;
                c1Template.Cols[3].Visible = true;
                c1Template.Cols[4].Visible = false;


                int nWidth = pnlMain.Width;
                c1Template.Cols[0].Width = 0;
                c1Template.Cols[1].Width = (int)(0.48 * (nWidth));
                c1Template.Cols[2].Width = (int)(0.24 * (nWidth));
                c1Template.Cols[3].Width = (int)(0.24 * (nWidth));
                c1Template.Cols[4].Width = 0;


                //c1Template.Cols[2].EditMask = "00/00/0000";
                //c1Template.Cols[3].EditMask = "00/00/0000";


                //string sTemplateNameList = "";
                //gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                //_dtTemplates = ogloTemplate.GetTemplates();
                //if (_dtTemplates != null)
                //{
                //    for (int i = 0; i < _dtTemplates.Rows.Count; i++)
                //    {
                //        sTemplateNameList += "|" + Convert.ToString(_dtTemplates.Rows[i]["sAppointmentTemplates"]);
                //    }
                //    sTemplateNameList = sTemplateNameList.Substring(1, sTemplateNameList.Length - 1);
                //}
                //c1Template.Cols[1].ComboList = sTemplateNameList;

                //c1Template.Rows.Add();
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void DesignRecurrnceGrid()
        {
            try
            {
                // c1Template.DataSource = dt;
                c1RecurrnceCriteria.Clear(ClearFlags.All);
                c1RecurrnceCriteria.Cols.Count = 10;
                c1RecurrnceCriteria.Rows.Count = 2;

                c1RecurrnceCriteria.SetData(0, 1, "");
                c1RecurrnceCriteria.SetData(0, 2, "Month");
                c1RecurrnceCriteria.SetData(0, 3, "Mon");
                c1RecurrnceCriteria.SetData(0, 4, "Tue");
                c1RecurrnceCriteria.SetData(0, 5, "Wed");
                c1RecurrnceCriteria.SetData(0, 6, "Thu");
                c1RecurrnceCriteria.SetData(0, 7, "Fri");
                c1RecurrnceCriteria.SetData(0, 8, "Sat");
                c1RecurrnceCriteria.SetData(0, 9, "Sun");


                int nWidth = c1RecurrnceCriteria.Width;
                c1RecurrnceCriteria.Cols[0].Width = 0;
                c1RecurrnceCriteria.Cols[1].Width = (int)(0.03 * (nWidth));
                c1RecurrnceCriteria.Cols[2].Width = (int)(0.17 * (nWidth));
                c1RecurrnceCriteria.Cols[3].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[4].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[5].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[6].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[7].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[8].Width = (int)(0.11 * (nWidth));
                c1RecurrnceCriteria.Cols[9].Width = (int)(0.11 * (nWidth));

                //c1RecurrnceCriteria.Cols[1].DataType = System.Type.GetType("System.Boolean");                
                //c1RecurrnceCriteria.Cols[3].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[4].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[5].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[6].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[7].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[8].DataType = System.Type.GetType("System.Boolean");
                //c1RecurrnceCriteria.Cols[9].DataType = System.Type.GetType("System.Boolean");

                // c1RecurrnceCriteria.Cols[2].AllowEditing = false;
                //  c1RecurrnceCriteria.Cols[1].AllowEditing = false;

                //c1RecurrnceCriteria.SetData(1, 2, "January");
                //c1RecurrnceCriteria.SetData(2, 2, "February");
                //c1RecurrnceCriteria.SetData(3, 2, "March");
                //c1RecurrnceCriteria.SetData(4, 2, "April");
                //c1RecurrnceCriteria.SetData(5, 2, "May");
                //c1RecurrnceCriteria.SetData(6, 2, "June");
                //c1RecurrnceCriteria.SetData(7, 2, "July");
                //c1RecurrnceCriteria.SetData(8, 2, "August");
                //c1RecurrnceCriteria.SetData(9, 2, "September");
                //c1RecurrnceCriteria.SetData(10, 2, "October");
                //c1RecurrnceCriteria.SetData(11, 2, "November");
                //c1RecurrnceCriteria.SetData(12, 2, "December");


                CellStyle cs = c1RecurrnceCriteria.Styles.Normal; //Normal;
                cs.Border.Direction = BorderDirEnum.Both; //Vertical;            
                cs.WordWrap = true;
              //  cs = c1RecurrnceCriteria.Styles.Add("Parent");
                try
                {
                    if (c1RecurrnceCriteria.Styles.Contains("Parent"))
                    {
                        cs = c1RecurrnceCriteria.Styles["Parent"];
                    }
                    else
                    {
                        cs = c1RecurrnceCriteria.Styles.Add("Parent");
                        cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(c1RecurrnceCriteria.Font, FontStyle.Bold);
                        cs.BackColor = Color.Lavender;
                    }

                }
                catch
                {
                    cs = c1RecurrnceCriteria.Styles.Add("Parent");
                    cs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(c1RecurrnceCriteria.Font, FontStyle.Bold);
                    cs.BackColor = Color.Lavender;
                }
              

              //  cs = c1RecurrnceCriteria.Styles.Add("Child");
                try
                {
                    if (c1RecurrnceCriteria.Styles.Contains("Child"))
                    {
                        cs = c1RecurrnceCriteria.Styles["Child"];
                    }
                    else
                    {
                        cs = c1RecurrnceCriteria.Styles.Add("Child");
                        cs.BackColor = Color.AliceBlue;
                        cs.TextAlign = TextAlignEnum.LeftCenter;
                    }

                }
                catch
                {
                    cs = c1RecurrnceCriteria.Styles.Add("Child");
                    cs.BackColor = Color.AliceBlue;
                    cs.TextAlign = TextAlignEnum.LeftCenter;
                }
                cs = null;

                string[] Months = new string[13];
                Months[0] = "";
                Months[1] = "January";
                Months[2] = "February";
                Months[3] = "March";
                Months[4] = "April";
                Months[5] = "May";
                Months[6] = "June";
                Months[7] = "July";
                Months[8] = "August";
                Months[9] = "September";
                Months[10] = "October";
                Months[11] = "November";
                Months[12] = "December";

                string[] Days = new string[5];
                Days[0] = "First";
                Days[1] = "Second";
                Days[2] = "Third";
                Days[3] = "Fourth";
                Days[4] = "Last";

                // Make tree node
                c1RecurrnceCriteria.Tree.Column = 1; //COL_PatientName  ;
                c1RecurrnceCriteria.Tree.Style = TreeStyleFlags.Simple; // Simple;
                c1RecurrnceCriteria.AllowMerging = AllowMergingEnum.None; //Nodes;

                //c1RecurrnceCriteria.SetData(1, 3, false);
                //c1RecurrnceCriteria.SetData(1, 4, false);
                //c1RecurrnceCriteria.SetData(1, 5, false);
                //c1RecurrnceCriteria.SetData(1, 6, false);
                //c1RecurrnceCriteria.SetData(1, 7, false);
                //c1RecurrnceCriteria.SetData(1, 8, false);
                //c1RecurrnceCriteria.SetData(1, 9, false);

                c1RecurrnceCriteria.SetUserData(1, 3, DayOfWeek.Monday);
                c1RecurrnceCriteria.SetUserData(1, 4, DayOfWeek.Tuesday);
                c1RecurrnceCriteria.SetUserData(1, 5, DayOfWeek.Wednesday);
                c1RecurrnceCriteria.SetUserData(1, 6, DayOfWeek.Thursday);
                c1RecurrnceCriteria.SetUserData(1, 7, DayOfWeek.Friday);
                c1RecurrnceCriteria.SetUserData(1, 8, DayOfWeek.Saturday);
                c1RecurrnceCriteria.SetUserData(1, 9, DayOfWeek.Sunday);

                c1RecurrnceCriteria.SetCellCheck(1, 3, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 4, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 5, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 6, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 7, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 8, CheckEnum.Unchecked);
                c1RecurrnceCriteria.SetCellCheck(1, 9, CheckEnum.Unchecked);

                //for (int i = 1; i <= 12; i++)
                //{
                //    c1RecurrnceCriteria.Rows.Add();
                //    Int32 RowIndex = c1RecurrnceCriteria.Rows.Count - 1;
                //    c1RecurrnceCriteria.SetData(RowIndex, 2, Months[i]);
                //    c1RecurrnceCriteria.SetData(RowIndex, 1, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 3, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 4, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 5, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 6, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 7, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 8, false);
                //    c1RecurrnceCriteria.SetData(RowIndex, 9, false);


                //    Node oParent;
                //    c1RecurrnceCriteria.Rows[RowIndex].IsNode = true;
                //    c1RecurrnceCriteria.Rows[RowIndex].Node.Key = i;
                //    oParent = c1RecurrnceCriteria.Rows[RowIndex].Node;

                //    for (int j = 0; j < 5; j++)
                //    {
                //        Node oChildNode;
                //        oChildNode = oParent.AddNode(NodeTypeEnum.LastChild, "",j, null);
                //        Int32  ChildRowIndex = oChildNode.Row.Index;
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 2, Days[j]);                        
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 3, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 4, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 5, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 6, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 7, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 8, false);
                //        c1RecurrnceCriteria.SetData(ChildRowIndex, 9, false);

                //        //Set Cell Style for Child
                //        CellStyle chieldCellStyle = c1RecurrnceCriteria.Styles["Child"];
                //        if (chieldCellStyle != null)
                //        {
                //            for (int k = 0; k < c1RecurrnceCriteria.Cols.Count; k++)
                //            {
                //                c1RecurrnceCriteria.SetCellStyle(ChildRowIndex, k, chieldCellStyle);
                //            }
                //        }
                //    }

                //    //Set Cell style of Parent Node
                //    CellStyle parentCellStyle = c1RecurrnceCriteria.Styles["Parent"];
                //    if (parentCellStyle != null)
                //    {
                //        for (int k = 0; k < c1RecurrnceCriteria.Cols.Count; k++)
                //        {
                //            c1RecurrnceCriteria.SetCellStyle(RowIndex, k, parentCellStyle);
                //        }
                //    }
                //}



                //for (int i = 1; i <= 12; i++)
                //{                    
                //    c1RecurrnceCriteria.Rows[i].Visible = false;
                //}
                // DirectoryInfo di = new DirectoryInfo("C:\\Temp");


            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region Allocation Patern

        private void dtpStartRecurrence_ValueChanged(object sender, EventArgs e)
        {

        }

        private void c1RecurrnceCriteria_Click(object sender, EventArgs e)
        {
            SetRecurrnceCriteria();

            #region "Old code commented "
            //Int32 RowIndex = c1RecurrnceCriteria.Row;
            //if (RowIndex == 1)
            //{
            //    Int32 ColIndex = c1RecurrnceCriteria.Col;   //Selected Day column    
            //    Int32 DayCount = 0;
            //    CheckEnum chkStatus = c1RecurrnceCriteria.GetCellCheck(RowIndex, c1RecurrnceCriteria.Col);
            //    if (chkStatus == CheckEnum.Checked)
            //    {
            //        Int32 ParentRowIndex = 0;
            //        for (int i = 2; i < c1RecurrnceCriteria.Rows.Count; i++)
            //        {
            //            if (c1RecurrnceCriteria.Rows[i].Node.Children > 0)
            //            {
            //                ParentRowIndex = i;
            //            }
            //            if (c1RecurrnceCriteria.Rows[i].Node.GetNode(NodeTypeEnum.Parent) != null)
            //            {
            //                if (c1RecurrnceCriteria.GetCellCheck(ParentRowIndex, 2) == CheckEnum.Checked)  //Current month is selected 
            //                {
            //                    if (Convert.ToString(c1RecurrnceCriteria.GetData(i, ColIndex)) != "")
            //                    {
            //                        DayCount++;

            //                        if (chkAll.Checked == false)
            //                        {

            //                            switch (DayCount)      //Day of month i.e. second Monday, third Sunday etc.
            //                            {
            //                                case 1:
            //                                    if (chkFirst.Checked == true)
            //                                        c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                    break;
            //                                case 2:
            //                                    if (chkSecond.Checked == true)
            //                                        c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                    break;
            //                                case 3:
            //                                    if (chkThird.Checked == true)
            //                                        c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                    break;
            //                                case 4:
            //                                    if (chkFourth.Checked == true)
            //                                        c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                    else
            //                                    {
            //                                        if (chkLast.Checked == true)
            //                                        {
            //                                            if (i < c1RecurrnceCriteria.Rows.Count - 1)//if next row is present
            //                                            {
            //                                                if (c1RecurrnceCriteria.GetData(i + 1, ColIndex) == null)
            //                                                {
            //                                                    c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                                }
            //                                                else
            //                                                {
            //                                                    c1RecurrnceCriteria.SetCellCheck(i + 1, ColIndex, CheckEnum.Checked);
            //                                                }
            //                                            }
            //                                            else
            //                                            {
            //                                                c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                                            }
            //                                        }
            //                                    }
            //                                    break;
            //                                case 5:
            //                                    if (chkLast.Checked == true)
            //                                        c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);


            //                                    break;
            //                                default:
            //                                    break;
            //                            }
            //                        }
            //                        else     //Select All Days
            //                        {
            //                            c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Checked);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    DayCount = 0;
            //                }
            //            }
            //            else
            //            {
            //                DayCount = 0;
            //            }

            //        }

            //        //for (int i = 2; i < c1RecurrnceCriteria.Rows.Count; i++)
            //        //{
            //        //    if (c1RecurrnceCriteria.Rows[i].Node.Children > 0)
            //        //    {

            //        //    }
            //        //}
            //    }
            //    else
            //    {
            //        for (int i = 2; i < c1RecurrnceCriteria.Rows.Count; i++)
            //        {
            //            if (c1RecurrnceCriteria.Rows[i].Node.GetNode(NodeTypeEnum.Parent) != null)
            //            {
            //                if (Convert.ToString(c1RecurrnceCriteria.GetData(i, ColIndex)) != "")
            //                {
            //                    c1RecurrnceCriteria.SetCellCheck(i, ColIndex, CheckEnum.Unchecked);
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
        }
        public SortedList<DayOfWeek, int> GetDayCounter()
        {
            SortedList<DayOfWeek, int> counter = new SortedList<DayOfWeek, int>();
            counter.Add(DayOfWeek.Monday, 0);
            counter.Add(DayOfWeek.Tuesday, 0);
            counter.Add(DayOfWeek.Wednesday, 0);
            counter.Add(DayOfWeek.Thursday, 0);
            counter.Add(DayOfWeek.Friday, 0);
            counter.Add(DayOfWeek.Saturday, 0);
            counter.Add(DayOfWeek.Sunday, 0);
            return counter;
        }

        public void SetRecurrnceCriteria()
        {
            CheckEnum chkParent = CheckEnum.Unchecked;
            SortedList<DayOfWeek, int> dayCounter = GetDayCounter();

            if ((c1RecurrnceCriteria.RowSel.Equals(1)) || (c1RecurrnceCriteria.ColSel.Equals(2)))
            {
                for (int row = 2; row < c1RecurrnceCriteria.Rows.Count; row++)
                {
                    if (c1RecurrnceCriteria.Rows[row].Node.Children > 0)
                    { chkParent = c1RecurrnceCriteria.GetCellCheck(row, 2); }

                    if (c1RecurrnceCriteria.Rows[row].Node.GetNode(NodeTypeEnum.Parent) != null)
                    {
                        for (int col = 3; col < c1RecurrnceCriteria.Cols.Count; col++)
                        {
                            CheckEnum chkDay = c1RecurrnceCriteria.GetCellCheck(1, col); // If col is selected (mon, tue, wed etc.)
                            DayOfWeek day = (DayOfWeek)c1RecurrnceCriteria.GetUserData(1, col);
                            string day_ = Convert.ToString(c1RecurrnceCriteria.GetData(row, col));

                            if (Convert.ToString(c1RecurrnceCriteria.GetData(row, col)) != "")
                            {
                                dayCounter[day]++;
                                if ((chkParent == CheckEnum.Checked) && (chkDay == CheckEnum.Checked))
                                {
                                    if ((dayCounter[day].Equals(1)) && (chkFirst.Checked))
                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                    else if ((dayCounter[day].Equals(2)) && (chkSecond.Checked))
                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                    else if ((dayCounter[day].Equals(3)) && (chkThird.Checked))
                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                    else if ((dayCounter[day].Equals(4)))
                                    {
                                        if (chkFourth.Checked)
                                        { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                        else
                                        {
                                            if (chkLast.Checked == true)
                                            {
                                                if (row < c1RecurrnceCriteria.Rows.Count - 1)//if next row is present
                                                {
                                                    if (Convert.ToString(c1RecurrnceCriteria.GetData(row + 1, col)) == "")
                                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                                    else
                                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Unchecked); }
                                                }
                                                else
                                                { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                            }
                                            else
                                            { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Unchecked); }
                                        }
                                    }
                                    else if ((dayCounter[day].Equals(5)) && (chkLast.Checked))
                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Checked); }
                                    else
                                    { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Unchecked); }
                                }
                                else
                                { c1RecurrnceCriteria.SetCellCheck(row, col, CheckEnum.Unchecked); }
                            }
                        }
                    }
                    else
                    { dayCounter = GetDayCounter(); }
                }
            }
            dayCounter = null;
        }

        private void numFromYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // SUDHIR 20091112 //
                if (numFromYear.Value > 2050)
                {
                    numFromYear.Value = 2050;
                    return;
                }

                for (int i = 3; i <= 9; i++)
                {
                    c1RecurrnceCriteria.SetCellCheck(1, i, CheckEnum.Unchecked);
                }


                c1RecurrnceCriteria.Rows.Count = 2;
                string[] Months = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                DateTime InStartDate = Convert.ToDateTime(Convert.ToInt32(numFromMonth.Value) + "/1/" + Convert.ToInt32(numFromYear.Value));
                DateTime InEndDate = Convert.ToDateTime(Convert.ToInt32(numToMonth.Value) + "/1/" + Convert.ToInt32(numToYear.Value));
                if (InStartDate.Date <= InEndDate.Date)
                {
                    while (InStartDate <= InEndDate)
                    {
                        Int32 MonthNo = InStartDate.Month;


                        c1RecurrnceCriteria.Rows.Add();
                        Int32 RowIndex = c1RecurrnceCriteria.Rows.Count - 1;
                        c1RecurrnceCriteria.SetData(RowIndex, 2, Months[MonthNo]);
                        c1RecurrnceCriteria.SetCellCheck(RowIndex, 2, CheckEnum.Checked);

                        Node oParent;
                        c1RecurrnceCriteria.Rows[RowIndex].IsNode = true;
                        c1RecurrnceCriteria.Rows[RowIndex].Node.Key = MonthNo;
                        oParent = c1RecurrnceCriteria.Rows[RowIndex].Node;

                        bool IsNewChield = true;
                        Int32 ChildRowIndex = 0;
                        Int32 ChieldColIndex = 0;
                        for (int j = 1; j <= DateTime.DaysInMonth(InStartDate.Year, MonthNo); j++)
                        {
                            DateTime Today = Convert.ToDateTime(InStartDate.Month + "/" + j + "/" + InStartDate.Year);
                            if (IsNewChield == true)
                            {
                                Node oChildNode;
                                oChildNode = oParent.AddNode(NodeTypeEnum.LastChild, "", MonthNo, null);
                                ChildRowIndex = oChildNode.Row.Index;
                                IsNewChield = false;
                            }
                            switch (Today.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    ChieldColIndex = 3;
                                    break;
                                case DayOfWeek.Tuesday:
                                    ChieldColIndex = 4;
                                    break;
                                case DayOfWeek.Wednesday:
                                    ChieldColIndex = 5;
                                    break;
                                case DayOfWeek.Thursday:
                                    ChieldColIndex = 6;
                                    break;
                                case DayOfWeek.Friday:
                                    ChieldColIndex = 7;
                                    break;
                                case DayOfWeek.Saturday:
                                    ChieldColIndex = 8;
                                    break;
                                case DayOfWeek.Sunday:
                                    ChieldColIndex = 9;
                                    IsNewChield = true;
                                    break;
                                default:
                                    break;
                            }

                            //if (Today >= SelectedStartDate && Today <= SelectedEndDate)
                            //{
                            //    c1RecurrnceCriteria.SetCellCheck(ChildRowIndex, ChieldColIndex, CheckEnum.Checked);
                            //}
                            //else
                            //{
                            //    c1RecurrnceCriteria.SetCellCheck(ChildRowIndex, ChieldColIndex, CheckEnum.Unchecked);
                            //}

                            c1RecurrnceCriteria.SetCellCheck(ChildRowIndex, ChieldColIndex, CheckEnum.Unchecked);
                            c1RecurrnceCriteria.SetData(ChildRowIndex, ChieldColIndex, j);


                            //Set Cell Style for Child
                            CellStyle chieldCellStyle = c1RecurrnceCriteria.Styles["Child"];
                            if (chieldCellStyle != null)
                            {
                                for (int k = 0; k < c1RecurrnceCriteria.Cols.Count; k++)
                                {
                                    c1RecurrnceCriteria.SetCellStyle(ChildRowIndex, k, chieldCellStyle);
                                }
                            }
                        }

                        //Set Cell style of Parent Node
                        CellStyle parentCellStyle = c1RecurrnceCriteria.Styles["Parent"];
                        if (parentCellStyle != null)
                        {
                            for (int k = 0; k < c1RecurrnceCriteria.Cols.Count; k++)
                            {
                                c1RecurrnceCriteria.SetCellStyle(RowIndex, k, parentCellStyle);
                            }
                        }
                        parentCellStyle = null;

                        InStartDate = InStartDate.AddMonths(1);
                    }
                }
                else
                {
                    numToMonth.Value = numFromMonth.Value;
                    numToYear.Value = numFromYear.Value;
                }
                Months = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private ArrayList FindRecurrnce()
        {
            ArrayList Dates = new ArrayList();

            try
            {
                #region Commented
                //if (dtpStartRecurrence.Value <= dtpEndRecurrence.Value)
                //{
                //    DateTime InStartDate = dtpStartRecurrence.Value;
                //    DateTime InEndDate = dtpEndRecurrence.Value;

                //    while (InStartDate <= InEndDate)
                //    {
                //        //Int32 Month = InStartDate.Month;
                //        bool IsSelectedDay = false;
                //        switch (InStartDate.DayOfWeek)
                //        {
                //            case DayOfWeek.Monday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 3));
                //                break;
                //            case DayOfWeek.Tuesday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 4));
                //                break;
                //            case DayOfWeek.Wednesday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 5));
                //                break;
                //            case DayOfWeek.Thursday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 6));
                //                break;
                //            case DayOfWeek.Friday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 7));
                //                break;
                //            case DayOfWeek.Saturday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 8));
                //                break;
                //            case DayOfWeek.Sunday:
                //                IsSelectedDay = Convert.ToBoolean(c1RecurrnceCriteria.GetData(InStartDate.Month, 9));
                //                break;
                //            default:
                //                break;
                //        }
                //        if (IsSelectedDay == true)
                //        {
                //            Dates.Add(InStartDate);
                //        }
                //        InStartDate = InStartDate.AddDays(1);
                //        IsSelectedDay = false;

                //        //--------------------------------
                //    }
                //} 
                #endregion

                #region Commented on 20080328
                // DateTime InStartDate = dtpStartRecurrence.Value;
                // DateTime InEndDate = dtpEndRecurrence.Value;

                //while (InStartDate.Date <= InEndDate.Date)
                //{
                //    int DayNo = 0;
                //    DayOfWeek _DayOFWeek = InStartDate.DayOfWeek;
                //    int LastDay = 0;

                //    for (int i = 1; i <= DateTime.DaysInMonth(InStartDate.Year,InStartDate.Month); i++)
                //    {
                //        DateTime dtTempDate = Convert.ToDateTime(InStartDate.Month + "/" + i + "/" + InStartDate.Year);
                //        if (dtTempDate.DayOfWeek == _DayOFWeek)
                //        {
                //            DayNo++;
                //        }
                //        if (dtTempDate.Date == InStartDate.Date)
                //        {
                //            break;
                //        }
                //    }

                //    for (int i = 1; i < c1RecurrnceCriteria.Rows.Count; i++)
                //    {
                //        if (c1RecurrnceCriteria.Rows[i].Visible == true)
                //        {
                //            if (c1RecurrnceCriteria.Rows[i].Node.Children > 0)
                //            {
                //                if (Convert.ToInt32(c1RecurrnceCriteria.Rows[i].Node.Key) == InStartDate.Month)
                //                {
                //                    Int32 RowIndex = 0;
                //                    Int32 ColIndex = 0;

                //                    RowIndex = i + DayNo;

                //                    switch (_DayOFWeek)
                //                    {
                //                        case DayOfWeek.Monday:
                //                            ColIndex = 3;
                //                            break;
                //                        case DayOfWeek.Tuesday:
                //                            ColIndex = 4;
                //                            break;
                //                        case DayOfWeek.Wednesday:
                //                            ColIndex = 5;
                //                            break;
                //                        case DayOfWeek.Thursday:
                //                            ColIndex = 6;
                //                            break;
                //                        case DayOfWeek.Friday:
                //                            ColIndex = 7;
                //                            break;                                        
                //                        case DayOfWeek.Saturday:
                //                            ColIndex = 8;
                //                            break;
                //                        case DayOfWeek.Sunday:
                //                            ColIndex = 9;
                //                            break;                                                                                                                      
                //                        default:
                //                            break;
                //                    }

                //                    if (c1RecurrnceCriteria.GetCellCheck(RowIndex, ColIndex) == CheckEnum.Checked)
                //                    {
                //                        Dates.Add(InStartDate);
                //                        break;
                //                    }                                   

                //                }
                //            }
                //        }
                //    }// End For(c1c1RecurrnceCriteria)
                //    InStartDate = InStartDate.AddDays(1);
                //} 
                #endregion

                DateTime InStartDate = Convert.ToDateTime(Convert.ToInt32(numFromMonth.Value) + "/1/" + Convert.ToInt32(numFromYear.Value));
                DateTime InEndDate = Convert.ToDateTime(Convert.ToInt32(numToMonth.Value) + "/" + DateTime.DaysInMonth(Convert.ToInt32(numToYear.Value), Convert.ToInt32(numToMonth.Value)) + "/" + Convert.ToInt32(numToYear.Value));


                while (InStartDate.Date <= InEndDate.Date)
                {
                    Int32 ParentRowIndex = 0;
                    for (int i = 2; i < c1RecurrnceCriteria.Rows.Count; i++)
                    {
                        if (c1RecurrnceCriteria.Rows[i].Node.Children > 0)
                        {
                            ParentRowIndex = i;
                        }

                        if (c1RecurrnceCriteria.Rows[i].Node.Children == 0 && Convert.ToInt64(c1RecurrnceCriteria.Rows[i].Node.Key) == InStartDate.Month)
                        {
                            if (c1RecurrnceCriteria.GetCellCheck(ParentRowIndex, 2) == CheckEnum.Checked)
                            {
                                Int32 ColNo = 0;
                                switch (InStartDate.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        ColNo = 3;
                                        break;
                                    case DayOfWeek.Tuesday:
                                        ColNo = 4;
                                        break;
                                    case DayOfWeek.Wednesday:
                                        ColNo = 5;
                                        break;
                                    case DayOfWeek.Thursday:
                                        ColNo = 6;
                                        break;
                                    case DayOfWeek.Friday:
                                        ColNo = 7;
                                        break;
                                    case DayOfWeek.Saturday:
                                        ColNo = 8;
                                        break;
                                    case DayOfWeek.Sunday:
                                        ColNo = 9;
                                        break;
                                }

                                if (c1RecurrnceCriteria.GetCellCheck(i, ColNo) == CheckEnum.Checked && Convert.ToInt32(c1RecurrnceCriteria.GetData(i, ColNo)) == InStartDate.Day)
                                {
                                    Dates.Add(InStartDate);
                                    break;
                                }
                            }
                        }
                    }
                    InStartDate = InStartDate.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            return Dates;
        }

        private ArrayList GetPattern()
        {
            ArrayList Dates = new ArrayList();
            DateTime InStartDate ;
            DateTime InEndDate;
            try
            {
                if (rbRecurrence.Checked == true)
                {
                    InStartDate = Convert.ToDateTime(Convert.ToInt32(numFromMonth.Value) + "/1/" + Convert.ToInt32(numFromYear.Value));
                    InEndDate = Convert.ToDateTime(Convert.ToInt32(numToMonth.Value) + "/" + DateTime.DaysInMonth(Convert.ToInt32(numToYear.Value), Convert.ToInt32(numToMonth.Value)) + "/" + Convert.ToInt32(numToYear.Value));

                    while (InStartDate.Date <= InEndDate.Date)
                    {
                        Int32 ParentRowIndex = 0;
                        for (int i = 2; i < c1RecurrnceCriteria.Rows.Count; i++)
                        {
                            if (c1RecurrnceCriteria.Rows[i].Node.Children > 0)
                            {
                                ParentRowIndex = i;
                            }

                            if (c1RecurrnceCriteria.Rows[i].Node.Children == 0 && Convert.ToInt64(c1RecurrnceCriteria.Rows[i].Node.Key) == InStartDate.Month)
                            {
                                if (c1RecurrnceCriteria.GetCellCheck(ParentRowIndex, 2) == CheckEnum.Checked)
                                {
                                    Int32 ColNo = 0;
                                    switch (InStartDate.DayOfWeek)
                                    {
                                        case DayOfWeek.Monday:
                                            ColNo = 3;
                                            break;
                                        case DayOfWeek.Tuesday:
                                            ColNo = 4;
                                            break;
                                        case DayOfWeek.Wednesday:
                                            ColNo = 5;
                                            break;
                                        case DayOfWeek.Thursday:
                                            ColNo = 6;
                                            break;
                                        case DayOfWeek.Friday:
                                            ColNo = 7;
                                            break;
                                        case DayOfWeek.Saturday:
                                            ColNo = 8;
                                            break;
                                        case DayOfWeek.Sunday:
                                            ColNo = 9;
                                            break;
                                    }

                                    if (c1RecurrnceCriteria.GetCellCheck(i, ColNo) == CheckEnum.Checked && Convert.ToInt32(c1RecurrnceCriteria.GetData(i, ColNo)) == InStartDate.Day)
                                    {
                                        Dates.Add(InStartDate);
                                        break;
                                    }
                                }
                            }
                        }
                        InStartDate = InStartDate.AddDays(1);
                    }
                }
                else if (rbSimple.Checked == true)
                {
                    InStartDate = dtpStartDate.Value;
                    InEndDate = dtpEndDate.Value;

                    for (DateTime dt = InStartDate.Date; dt <= InEndDate.Date; dt = dt.AddDays(1))
                    {
                        Dates.Add(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            return Dates;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            chkFirst.CheckedChanged -= new EventHandler(chk_CheckedChanged);
            chkFirst.Checked = chkAll.Checked;
            chkFirst.CheckedChanged += new EventHandler(chk_CheckedChanged);

            chkSecond.CheckedChanged -= new EventHandler(chk_CheckedChanged);
            chkSecond.Checked = chkAll.Checked;
            chkSecond.CheckedChanged += new EventHandler(chk_CheckedChanged);

            chkThird.CheckedChanged -= new EventHandler(chk_CheckedChanged);
            chkThird.Checked = chkAll.Checked;
            chkThird.CheckedChanged += new EventHandler(chk_CheckedChanged);

            chkFourth.CheckedChanged -= new EventHandler(chk_CheckedChanged);
            chkFourth.Checked = chkAll.Checked;
            chkFourth.CheckedChanged += new EventHandler(chk_CheckedChanged);

            chkLast.CheckedChanged -= new EventHandler(chk_CheckedChanged);
            chkLast.Checked = chkAll.Checked;
            chkLast.CheckedChanged += new EventHandler(chk_CheckedChanged);

            if (chkAll.Checked == true)
            {
                chkFirst.Enabled = false;
                chkFourth.Enabled = false;
                chkLast.Enabled = false;
                chkSecond.Enabled = false;
                chkThird.Enabled = false;
            }
            else
            {
                chkFirst.Enabled = true;
                chkFourth.Enabled = true;
                chkLast.Enabled = true;
                chkSecond.Enabled = true;
                chkThird.Enabled = true;
            }

            SetRecurrnceCriteria();
        }

        #endregion

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlTemplateAllocation.Enabled = true;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rowIndex > 0)
                {
                    c1Template.Rows.Remove(_rowIndex);
                    ClearControls();
                    _AllocationID = 0;
                }
                else
                {
                    c1Template.Rows.Remove(c1Template.Row);
                }
                _rowIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlTemplateAllocation.Enabled = false;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
        }

        //Display Template
        private void cmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            AppointmentTemplate oAppointmentTemplate = null;

            CalendarTemplate.Date = SelectedStartDate;

            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            try
            {
                CalendarTemplate.Appointments.Clear();

                if (cmbTemplates != null && cmbTemplates.SelectedIndex >= 0)
                {
                    oAppointmentTemplate = ogloAppointmentTemplate.GetTemplate(Convert.ToInt64(cmbTemplates.SelectedValue));
                    for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                    {
                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Appointment.Text = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                        oJUC_Appointment.Description = "";
                        oJUC_Appointment.Prefix = "";
                        oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(oAppointmentTemplate.TemplateDetails[i].ColorCode);
                        
                        // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                        oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);

                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(CalendarTemplate.Date, oAppointmentTemplate.TemplateDetails[i].EndTime);
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(CalendarTemplate.Date, oAppointmentTemplate.TemplateDetails[i].StartTime);
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(CalendarTemplate.Date, oAppointmentTemplate.TemplateDetails[i].StartTime);
                                oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(CalendarTemplate.Date, oAppointmentTemplate.TemplateDetails[i].EndTime);
                            }
                            catch { }
                        }

                        CalendarTemplate.Appointments.Add(oJUC_Appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (ogloAppointmentTemplate != null) { ogloAppointmentTemplate.Dispose(); }
                if (oAppointmentTemplate != null) { oAppointmentTemplate.Dispose(); }
                oJUC_Appointment = null;
            }

        }


        #region Mouse Hover Leave Events
        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_ButtonHover;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {

            btnAdd.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseHover(object sender, EventArgs e)
        {

            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_ButtonHover;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_ButtonHover;
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_ButtonHover;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion

        private void CalendarTemplate_AppointmentChanged(object sender, Janus.Windows.Schedule.AppointmentChangeEventArgs e)
        {

        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            SetRecurrnceCriteria();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime InStartDate = Convert.ToDateTime(Convert.ToInt32(numFromMonth.Value) + "/1/" + Convert.ToInt32(numFromYear.Value));
            DateTime InEndDate = Convert.ToDateTime(Convert.ToInt32(numToMonth.Value) + "/1/" + Convert.ToInt32(numToYear.Value));

            if (dtpStartDate.Value <= dtpEndDate.Value)
            {
            }
            else
            { dtpEndDate.Value = dtpStartDate.Value; }
        }
    }
}