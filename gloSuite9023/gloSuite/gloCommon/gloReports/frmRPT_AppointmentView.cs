using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloSSRSApplication;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace gloReports
{
    public partial class frmRPT_AppointmentView : Form
    {

      
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID;
        public string gstrSQLServerName;
        public string gstrDatabaseName;
        public bool gblnSQLAuthentication;
        public string gstrSQLUser;
        public string gstrSQLPassword;
      //  public string msgCaption = "";
        
        private string RptName = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Boolean FlagPatientMultiselect;
        private Int64 _setPatientID;
        private Dictionary<Int64, String> _dictResource = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictPatients = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictProviders = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictMedCategory = new Dictionary<Int64,String>();
        private Dictionary<String, Int64> _dictApptSelCols = new Dictionary<String,Int64>();
        private Dictionary<Int64, String> _dictLocation = new Dictionary<Int64, String>();
        //Delegates For Patient Selection Index Event
        public delegate void onPatientsSelectedIndexChanged(object sender, EventArgs e);
        public event onPatientsSelectedIndexChanged onPatients_SelectedIndexChanged;
        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        string gstrMessageBoxCaption = "gloEMR";
        //Delegates For Close Event
        public delegate void onReportsCloseClicked(object sender, EventArgs e);
        public event onReportsCloseClicked onReportsClose_Clicked;

        //Delegates For Generate Report Event
        public delegate void onGenerateReportClicked(object sender, EventArgs e);
        public event onGenerateReportClicked onGenerateReport_Clicked;


     


        //Delegates For Patient Selection Index Event
        public delegate void onGenerateBatchClicked(object sender, EventArgs e);
        public event onGenerateBatchClicked onGenerateBatch_Clicked;

        // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
        // Delegate & event added for print report event
        public delegate void onPrintReportClicked(object sender, EventArgs e);
        public event onPrintReportClicked onPrintReport_Clicked; 

 
       // private string _MessageBoxCaption = string.Empty;
      

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();
        private StringBuilder sbResource = new StringBuilder();
        private StringBuilder sbMedicalCat = new StringBuilder();
        private StringBuilder sbAppSelCols = new StringBuilder();
        private StringBuilder sbLocations = new StringBuilder();
        private Boolean _IsFromCalendar=false ;
        private DataTable _dtProviders;
        private bool _IsPrint = false;
    //    private bool _DefaultPrinter = false;
        string _strUserName = "";
        string _reportName = "";
        string _ParaNamesWithCSV = string.Empty;
        string _ParaValuesWithCSV = string.Empty;

        private DateTime _dtFromtime = DateTime.Now;
        private DateTime _dtTotime = DateTime.Now;
        private static Font BoldFont=null;
        private static Font RegularFont=null;

        //  #endregion " Declarations "


       // #region  " Property Procedures "

    
        public frmRPT_AppointmentView()
        {
            InitializeComponent();
        }

        public frmRPT_AppointmentView(string databaseconnectionstring, string ReportName, string _gstrSQLServerName, string  _gstrDatabaseName, bool _gblnSQLAuthentication, string _gstrSQLUserEMR, string _gstrSQLPasswordEMR,string MsgBoxCaption)
        {
            InitializeComponent();

            
           // gloReportAppt_Viewer _ogloReportApptViewer = new gloReportAppt_Viewer(ReportName);
            //this.Text = ReportName;
            _reportName = ReportName; 
            //Attaching the event Handler
      
            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { 
                _ClinicID = 0; 
            }

        //    #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _strUserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _strUserName = "";
            }

            gstrSQLServerName=_gstrSQLServerName;
            gstrDatabaseName=_gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUserEMR;
            gstrSQLPassword = _gstrSQLPasswordEMR;
            gstrMessageBoxCaption  = MsgBoxCaption;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { 
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else 
                { 
                    _ClinicID = 0;
                }
            }
            else
            { 
                _ClinicID = 0; 
            }

            if (appSettings["DataBaseConnectionString"] != null)
                _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);

            if (ReportName == "rptCancelAppointments")
            {
                pnlResource.Visible = false;
                pnlCancelApp.Visible = true;
                pnlmedicalcat.Location = new Point(pnlmedicalcat.Location.X, 43);
                this.Text = "Cancel Appointments";
            }
            else
            {
                pnlResource.Location = new Point(pnlResource.Location.X, 6);

                pnlmedicalcat.Location = new Point(pnlmedicalcat.Location.X, 43);
                this.Text = "Appointments";
            }
            
            
            
            
        
            // #endregion
        }




        //   _databaseconnectionstring, dtproviders, juc_Appointment.DateRange.Start, juc_Appointment.DateRange.End, true

        public frmRPT_AppointmentView(string databaseconnectionstring, DataTable dtProviders, DateTime dtstartdate, DateTime dtenddate, bool Isprint, string MsgBoxCaption)
        {
            
            InitializeComponent();
            if (Isprint == true)
            {
                _IsFromCalendar = true;
                _IsPrint = true;
                this.Hide();
            }

          

            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { 
                 _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); 
                }
                else 
                { 
                    _ClinicID = 0; 
                }
            }
            else
            { 
                _ClinicID = 0; 
            }

            //    #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _strUserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _strUserName = "";
            }

          

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                 _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); 
                }
                else
                { 
                    _ClinicID = 0; 
                }
            }

            else
            { 
                _ClinicID = 0; 
            }

            if (appSettings["DataBaseConnectionString"] != null)
                _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
                
                 
            
                 _reportName = "rptAppointmentList";   
                 pnlResource.Location = new Point(pnlResource.Location.X, 6);
                 pnlmedicalcat.Location = new Point(pnlmedicalcat.Location.X, 43);
                this.Text = "Appointments";
        

                if (dtProviders != null)
                {
                    if (dtProviders.Rows.Count > 0)
                    {
                        _IsFromCalendar = true;
                        _dtProviders = dtProviders.Copy();
                    }
                    else
                    {
                        _IsFromCalendar = true;
                    }
                }

                nStartdt = gloDateMaster.gloDate.DateAsNumber(dtstartdate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(dtenddate.ToShortDateString());
                dtpStartDate.Value = dtstartdate;
                dtpEndDate.Value = dtenddate;
                _dtFromtime = dtstartdate;
                _dtTotime = dtenddate;
                gstrMessageBoxCaption = MsgBoxCaption;
            // #endregion
        }
     
        private void FillLocation()
        {
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable _dtLocations = null;
            try
            {
                _dtLocations = oLocation.GetList();

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
                if (_dtLocations != null)
                {
                    _dtLocations.Dispose();
                    _dtLocations = null;
                }
                if (oLocation != null)
                {
                    oLocation.Dispose();
                    oLocation = null;
                }
            }
        }

      
        public Dictionary<Int64, String> dictPatients
        {
            //Property for storing the PatientID and Name
            get
            {
               
                    this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                    _dictPatients.Clear();
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {

                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                      
                        if (cmbPatients.SelectedValue != null)
                            _dictPatients.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);

                    }
                    this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);

                
                return _dictPatients;
            }

        }

        public Dictionary<Int64, String> dictProviders
        {
            //Property for storing the ProviderID and Name
            get
            {
                _dictProviders.Clear();
                for (int i = 0; i < cmbProvider.Items.Count; i++)
                {
                    cmbProvider.SelectedIndex = i;
                    cmbProvider.Refresh();
                    _dictProviders.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                }
                return _dictProviders;
            }

        }


        public Dictionary<Int64, String> dictMedicalCategory
        {
            //Property for storing the MedicalCategory and Name
            get
            {
                _dictMedCategory.Clear();
                for (int i = 0; i < cmbMedCategory.Items.Count; i++)
                {
                    cmbMedCategory.SelectedIndex = i;
                    cmbMedCategory.Refresh();
                    _dictMedCategory.Add(Convert.ToInt64(cmbMedCategory.SelectedValue), cmbMedCategory.Text);
                }
                return _dictMedCategory;
            }

        }

        public Dictionary<Int64, String> dictLocation
        {
            //Property for storing the LocationID and Name
            get
            {
                if (_reportName == "rptAppointmentList")
                {
                    _dictLocation.Clear();
                    for (int i = 0; i < cmbLocation.Items.Count; i++)
                    {
                        cmbLocation.SelectedIndex = i;
                        cmbLocation.Refresh();
                        _dictLocation.Add(Convert.ToInt64(cmbLocation.SelectedValue), cmbLocation.Text);
                    }
                }
              
                return _dictLocation;
            }

        }

        public Dictionary<String, Int64> dictApptSelCols
        {
            //Property for storing the SelectColID and ColName
            get
            {
                _dictApptSelCols.Clear();
                for (int i = 0; i < cmbSelectedColumns.Items.Count; i++)
                {
                    cmbSelectedColumns.SelectedIndex = i;
                    cmbSelectedColumns.Refresh();
                    _dictApptSelCols.Add(cmbSelectedColumns.Text, Convert.ToInt64(cmbSelectedColumns.SelectedValue));
                }
                return _dictApptSelCols;
            }

        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Dictionary<Int64, String> dictResource
        {
            get
            {
                _dictResource.Clear();
                for (int i = 0; i < cmbResouce.Items.Count; i++)
                {
                    cmbResouce.SelectedIndex = i;
                    cmbResouce.Refresh();
                    _dictResource.Add(Convert.ToInt64(cmbResouce.SelectedValue), cmbResouce.Text);
                }
                return _dictResource;
            }


        }


        public Int64 nPatientID
        {

            get
            {
                if (!FlagPatientMultiselect)
                {
                    if (cmbPatients.SelectedValue != null)
                        return Convert.ToInt64(cmbPatients.SelectedValue);
                    else
                        return 0;
                }
                else
                {
                    //return 0;
                    if (cmbPatients.SelectedValue != null)
                        return Convert.ToInt64(cmbPatients.SelectedValue);
                    else
                        return 0;
                }
            }
            set
            {
                cmbPatients.SelectedValue = value;
            }
        }

        public Boolean showPatientMultiselect
        {
            //Flag For Showing/Hiding the Multi Select in a PatientList Control
            get { return FlagPatientMultiselect; }
            set { FlagPatientMultiselect = value; }
        }


        public Int64 setPatientID
        {
            get { return _setPatientID; }
            set { _setPatientID = value; }
        }
        
        
        private void ogloReports_onPrintReport_Clicked(object sender, EventArgs e)
        {
          
        }

        // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
        // This method will send the appointments to printer based on the default printer setting.

       
        
      

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {

            this.Close();
        }


      

        private void FillAppointments(string ProviderID, string PatientID, string ResourceID)
        {
            //if (objrptAppointments != null)
            //{
            //    objrptAppointments.Dispose();
            //    objrptAppointments = null;
            //}
            //objrptAppointments = new Rpt_Appointments();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Appointments_Revised";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.Parameters.Add("@FromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@ToDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@ClinicID", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@UserName", System.Data.SqlDbType.Text);

                _sqlcommand.Parameters["@FromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@ToDate"].Value = nEnddt;
                _sqlcommand.Parameters["@ClinicID"].Value = _ClinicID;
                _sqlcommand.Parameters["@UserName"].Value = _strUserName;


            

                //#endregion
                if (ProviderID != "")
                {
                    _sqlcommand.Parameters.Add("@Providers", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@Providers"].Value = ProviderID;
                }

                if (PatientID != "")
                {
                    _sqlcommand.Parameters.Add("@PatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@PatientID"].Value = PatientID;
                }

              
                if (ResourceID != "")
                {
                    _sqlcommand.Parameters.Add("@Resources", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@Resources"].Value = ResourceID;
                }
                _sqlcommand.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Appointments");
             
            
                da.Dispose();
             


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();

                }

            }
        }
        private void removeOListControl(bool bDispose=true)
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch
                {
                }
                if (bDispose)
                {
                    oListControl.Dispose();
                    oListControl = null;
                }
            }
        }
        private void btnBrowsePatient_Click(object sender, EventArgs e)
        {
            try
            {
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
                removeOListControl();
                //Used Only for Patient Statement - Temp Code


                //if (FlagPatientMultiselect)
                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                //else
                //    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);


                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbPatients.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
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



        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {
                        this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                      
                        cmbPatients.DataSource = null;
                        cmbPatients.Items.Clear();
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


                            cmbPatients.DataSource = oBindTable;
                            cmbPatients.DisplayMember = "DispName";
                            cmbPatients.ValueMember = "ID";

                        }
                        this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
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

                case gloListControl.gloListControlType.Location:
                    {

                        cmbLocation.DataSource = null;
                        cmbLocation.Items.Clear();
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

                            cmbLocation.DataSource = oBindTable;
                            cmbLocation.DisplayMember = "DispName";
                            cmbLocation.ValueMember = "ID";
                        }

                    }
                    break;
                case gloListControl.gloListControlType.MedicalCategory:
                    {
                      
                        cmbMedCategory.DataSource = null;
                        cmbMedCategory.Items.Clear();
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

                            cmbMedCategory.DataSource = oBindTable;
                            cmbMedCategory.DisplayMember = "DispName";
                            cmbMedCategory.ValueMember = "ID";
                        }

                    }
                    break;


            
                case gloListControl.gloListControlType.Resources:
                    {
                       
                        cmbResouce.DataSource = null;
                        cmbResouce.Items.Clear();
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

                            cmbResouce.DataSource = oBindTable;
                            cmbResouce.DisplayMember = "DispName";
                            cmbResouce.ValueMember = "ID";
                        }

                    }
                    break;
                case gloListControl.gloListControlType.ApptSelectedCols:
                    {
                       
                        cmbSelectedColumns.DataSource = null;
                        cmbSelectedColumns.Items.Clear();
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

                            cmbSelectedColumns.DataSource = oBindTable;
                            cmbSelectedColumns.DisplayMember = "DispName";
                            cmbSelectedColumns.ValueMember = "ID";
                        }

                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }

        private void SetDefaultValueToSelectedColumnParameter()
        {
            cmbSelectedColumns.DataSource = null;
            cmbSelectedColumns.Items.Clear();
            //if (oListControl.SelectedItems.Count > 0)
            //{
            DataTable oBindTable = new DataTable();

            oBindTable.Columns.Add("ID");
            oBindTable.Columns.Add("DispName");

            DataRow oRow;
            oRow = oBindTable.NewRow();
            oRow[0] = 1;
            oRow[1] = "Pat.Code";
            oBindTable.Rows.Add(oRow);

            cmbSelectedColumns.DataSource = oBindTable;
            cmbSelectedColumns.DisplayMember = "DispName";
            cmbSelectedColumns.ValueMember = "ID";
            //}
        }


        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
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
            removeOListControl(false);
        }

        private void btnBrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
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
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _ClinicID;
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

        private void btnBrowseLocation_Click(object sender, EventArgs e)
        {
            try
            {
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
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Location, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Location";
                _CurrentControlType = gloListControl.gloListControlType.Location;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbLocation.DataSource != null)
                {
                    for (int i = 0; i < cmbLocation.Items.Count; i++)
                    {
                        cmbLocation.SelectedIndex = i;
                        cmbLocation.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbLocation.SelectedValue), cmbLocation.Text);
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

        private void btnBrwsSelectedColumns_Click(object sender, EventArgs e)
        {
            try
            {
                
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
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.ApptSelectedCols, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Select Appointment Columns";
                _CurrentControlType = gloListControl.gloListControlType.ApptSelectedCols;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbSelectedColumns.DataSource != null)
                {
                    for (int i = 0; i < cmbSelectedColumns.Items.Count; i++)
                    {
                        cmbSelectedColumns.SelectedIndex = i;
                        cmbSelectedColumns.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbSelectedColumns.SelectedValue), cmbSelectedColumns.Text);
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

      

        private void btnBrwsMultiResource_Click(object sender, EventArgs e)
        {
            try
            {
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
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Resources, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Resource";
                _CurrentControlType = gloListControl.gloListControlType.Resources;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbResouce.DataSource != null)
                {
                    for (int i = 0; i < cmbResouce.Items.Count; i++)
                    {
                        cmbResouce.SelectedIndex = i;
                        cmbResouce.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbResouce.SelectedValue), cmbResouce.Text);
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

        private void btnbrwMedCategory_Click(object sender, EventArgs e)
        {
            try
            {
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
                removeOListControl(); 
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.MedicalCategory, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Medical Category";
                _CurrentControlType = gloListControl.gloListControlType.MedicalCategory;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbMedCategory.DataSource != null)
                {
                    for (int i = 0; i < cmbMedCategory.Items.Count; i++)
                    {
                        cmbMedCategory.SelectedIndex = i;
                        cmbMedCategory.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbMedCategory.SelectedValue), cmbMedCategory.Text);
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

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
         //   cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;  
            cmbPatients.Items.Clear();
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

        private void frmRPT_AppointmentView_Load(object sender, EventArgs e)
        {
            
         

            if ((_IsFromCalendar == true) && (_IsPrint == true))
            {
                // Hide the form
                this.Hide();
                this.Visible = false;

            }
            if (cmb_datefilter.SelectedIndex == 0)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }

            Fill_FilterDatesCombo();
            cmb_datefilter.SelectedIndex = 0;
            //FillLocation(); //9010 PER - Allow Multiple Locations in Appointment Report,  CAS-17831-P3P6L2 CRM:0022010
            if (_IsFromCalendar == true)
            {
           
                LoadreportFromAppointment();
                dtpStartDate.Value =  _dtFromtime ;
                dtpEndDate.Value   =  _dtTotime ; 
            }


            if (_reportName == "rptCancelAppointments")
            {
                FillLocation();

                if (BoldFont == null)
                {
                    BoldFont = new Font(rbNoShowAppointments.Font, FontStyle.Bold);
                }
                if (RegularFont == null)
                {
                    RegularFont = new Font(rbNoShowAppointments.Font, FontStyle.Regular);
                }

                pnlSelectedColumns.Visible = false;

                //do not shot for cancel appointment report, 9010 PER - Allow Multiple Locations in Appointment Report,  CAS-17831-P3P6L2 CRM:0022010
                btnBrowseLocation.Visible = false;
                btnClearLocation.Visible = false;
            }
            else if (_reportName == "rptAppointmentList")
            {
                SetDefaultValueToSelectedColumnParameter();
            }

            //08-Oct-14 Aniket: Resolving Bug #74859: gloEMR:Appointments-report get generated automatically,while loading appointments page first time
            Loadreport();

        }

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
          
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            if (cmb_datefilter.SelectedIndex == 0)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }

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

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClearMedCategory_Click(object sender, EventArgs e)
        {
           // cmbMedCategory.Items.Clear();
            cmbMedCategory.DataSource = null;  
            cmbMedCategory.Items.Clear();
  
        }

        private void btnClearResource_Click(object sender, EventArgs e)
        {
          //  cmbResouce.Items.Clear();   
            cmbResouce.DataSource = null;  
            cmbResouce.Items.Clear();   
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
          //  cmbProvider.Items.Clear();
            cmbProvider.DataSource = null;  
            cmbProvider.Items.Clear();
        }

        private void btnClearLocation_Click(object sender, EventArgs e)
        {
            //  cmbLocation.Items.Clear();
            cmbLocation.DataSource = null;
            cmbLocation.Items.Clear();
        }

        private void btnClearSelectedColumns_Click(object sender, EventArgs e)
        {
           // cmbSelectedColumns.Items.Clear();
            cmbSelectedColumns.DataSource = null;
            cmbSelectedColumns.Items.Clear();
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Tag.ToString())
            {

                case "Generate Report":
                    {
                        //08-Oct-14 Aniket: Resolving Bug #74864: gloEMR:Appointments Reports-application gives exception 
                        tsb_Print.Enabled = false;
                        Loadreport();
                    }
                    break;

                case "Print":
                    
                    if (pnlssrsviewer.Visible == false)
                    {
                        MessageBox.Show("Report is not generated. Generate report before print.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
                        //{
                        //    PrintSSRSReport(_reportName, _ParaNamesWithCSV, _ParaValuesWithCSV);
                        //}
                        //else
                        //{
                            //reportViewer1.PrintDialog();
                            //Print();
                        printReport();
                       // }
                    }

                    break;

                case "Export":
                    if (pnlssrsviewer.Visible == false)
                    {
                        MessageBox.Show("Report is not generated. Generate report before Export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        ExportReport();
                    }
                    break;

                case "More":
                    break;

                case "Hide":
                    break;

              

                case "Close":
                    this.Close();
                    break;
            }
        }
        //private void Print()
        //{
        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;
        //    string _PDFFileName = "";
        //    try
        //    {
        //        Warning[] warnings = null;
        //        string[] streamids = null;
        //        string mimeType = null;
        //        string encoding = null;
        //        string extension = null;
        //        byte[] bytes = null;
        //        string Format = null;
               

        //        Format = "PDF";
        //        bytes = this.reportViewer1.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
        //        gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
        //        string _FileName = "";
        //        _FileName = gloSettings.FolderSettings.AppTempFolderPath  + Guid.NewGuid().ToString() + ".PDF";
        //        FileStream fs = new FileStream(_FileName, FileMode.Create);
        //        fs.Write(bytes, 0, bytes.Length);
        //        fs.Close();
        //        fs.Dispose();
        //        fs = null;
        //        _PDFFileName = _FileName;
        //        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
        //        {
        //            oDialog.ConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
        //            oDialog.TopMost = true;
        //            oDialog.ShowPrinterProfileDialog = true;

        //            oDialog.ModuleName = "SSRSReports";
        //            oDialog.RegistryModuleName = "SSRSReports";
                    
        //            if (oDialog != null)
        //            {

        //                oDialog.PrinterSettings = reportViewer1.PrinterSettings;

        //                //if (bIsSelectedPages)
        //                //{
        //                //    oDialog.AllowSelection = true;
        //                //    PrintDialog1.AllowSelection = true;
        //                //}
        //                //else
        //                //{
        //                //    oDialog.AllowSomePages = true;

        //                //    oDialog.PrinterSettings.ToPage = maxPage;
        //                //    oDialog.PrinterSettings.FromPage = 1;
        //                //    oDialog.PrinterSettings.MaximumPage = maxPage;
        //                //    oDialog.PrinterSettings.MinimumPage = 1;

        //                //    PrintDialog1.AllowSomePages = true;
        //                //    PrintDialog1.PrinterSettings.ToPage = maxPage;
        //                //    PrintDialog1.PrinterSettings.FromPage = 1;
        //                //    PrintDialog1.PrinterSettings.MaximumPage = maxPage;
        //                //    PrintDialog1.PrinterSettings.MinimumPage = 1;
        //                //}

        //                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {

        //                    reportViewer1.PrinterSettings = oDialog.PrinterSettings;
        //                    if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
        //                    {
        //                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
        //                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
        //                    }

        //                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, reportViewer1.PrinterSettings, oDialog.CustomPrinterExtendedSettings);

        //                    ogloPrintProgressController.ShowProgress(this);
                           

        //                }//if

        //            }
        //            else
        //            {
        //                string _ErrorMessage = "Error in Showing Print Dialog";

        //                if (_ErrorMessage.Trim() != "")
        //                {
        //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;

        //                    _MessageString = "";
        //                }


        //                MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
                  
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;
        //    }
        //    finally
        //    {
              
        //    }
        //}

        private void printReport()
        {
            string sqlServerName = string.Empty;
            string sqlDatabaseName = string.Empty;
            string sqlUser = string.Empty;
            string sqlPwd = string.Empty;
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string PDFFileName = "";
            try
            {
                sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
                sqlDatabaseName = Convert.ToString(appSettings["DataBaseName"]);
                sqlUser = Convert.ToString(appSettings["SQLLoginName"]);
                sqlPwd = Convert.ToString(appSettings["SQLPassword"]);

                bool blSQLAuth = !(Convert.ToBoolean(appSettings["WindowAuthentication"]));
                bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth, sqlUser, sqlPwd);
                if (!(gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForSSRS))
                {
                    PDFFileName = ConvertSSRStoPDF();
                }
                clsPrntRpt.PrintReport(_reportName, _ParaNamesWithCSV, _ParaValuesWithCSV, gblnIsDefaultPrinter, "", PDFFileName, reportViewer1.ServerReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                //  Cleanup for used variables under this method.
                sqlServerName = string.Empty;
                sqlDatabaseName = string.Empty;
                sqlUser = string.Empty;
                sqlPwd = string.Empty;

                if (clsPrntRpt != null)
                {
                    clsPrntRpt.Dispose();
                    clsPrntRpt = null;
                }
            }
        }
        private string ConvertSSRStoPDF()
        {
            try
            {
                Warning[] warnings = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string extension = null;
                byte[] bytes = null;
                string Format = null;


                Format = "PDF";
                bytes = this.reportViewer1.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                string _FileName = "";
                _FileName = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".PDF";

                FileStream fs = new FileStream(_FileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
                fs = null;
                return _FileName;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }

        }
        private void ExportReport()
        {
            saveFileDialog1.Filter = "XML file with report data (*.xml)|*.xml|CSV (comma delimited) (*.csv)|*.csv|TIFF (*.tif)|*.tif|Acrobat (PDF) file (*.pdf)|*.pdf|Web archive(*.mhtml)|*.mhtml|Excel (*.xls)|*.xls";
            saveFileDialog1.AddExtension = true;
          //  saveFileDialog1.FileName = RptName;
            saveFileDialog1.ShowDialog(this);
        }


        private void Loadreport()
        {
            string strReportProtocol = "";
            string strReportServer = "";
            string strReportFolder = "";
            string strVirtualDir = "";
          //  string _reportName = "";
           
            System.Uri SSRSReportURL; 
          //  string reportParam = "";

            pnlssrsviewer.Visible = false ;
           
            try
            {
                //if (oListControl != null) // Bug #74547  
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
                removeOListControl(false);
                Cursor.Current = Cursors.WaitCursor;
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                object oValue = null;


                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }
                oSetting.Dispose();
                oSetting = null;
                if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", gstrMessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }







                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);
                List<Int64> values = new List<Int64>(dictProviders.Keys);
                values.Sort();

                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbProviders.Append(values[i].ToString());
                    }
                    else
                    {
                        sbProviders.Append(values[i].ToString() + ",");
                    }
                }
                #endregion

                #region "Location"

                sbLocations.Remove(0, sbLocations.Length);
                List<Int64> lstPatlocnvalues = new List<Int64>(dictLocation.Keys);
                lstPatlocnvalues.Sort();

                for (int i = 0; i <= lstPatlocnvalues.Count - 1; i++)
                {
                    if (i == lstPatlocnvalues.Count - 1)
                    {
                        sbLocations.Append(lstPatlocnvalues[i].ToString());
                    }
                    else
                    {
                        sbLocations.Append(lstPatlocnvalues[i].ToString() + ",");
                    }
                }
                #endregion

                sbPatientID.Remove(0, sbPatientID.Length);

                //Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                //dictPatientID = _ogloReportViewer.dictPatients;

                List<Int64> lstPatvalues = new List<Int64>(dictPatients.Keys);
                lstPatvalues.Sort();

                for (int i = 0; i <= lstPatvalues.Count - 1; i++)
                {
                    if (i == lstPatvalues.Count - 1)
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString() + ",");
                    }
                }

                sbResource.Remove(0, sbResource.Length);
                //Dictionary<Int64, String> dictResourceID = new Dictionary<long, string>();
                //dictResourceID = _ogloReportViewer.dictResource;

                List<Int64> lstResvalues = new List<Int64>(dictResource.Keys);
                lstResvalues.Sort();
                for (int i = 0; i <= lstResvalues.Count - 1; i++)
                {
                    if (i == lstResvalues.Count - 1)
                    {
                        sbResource.Append(lstResvalues[i].ToString());
                    }
                    else
                    {
                        sbResource.Append(lstResvalues[i].ToString() + ",");
                    }
                }

                sbMedicalCat.Remove(0, sbMedicalCat.Length);
                List<Int64> lstMedicalCat = new List<Int64>(dictMedicalCategory.Keys);
                lstMedicalCat.Sort();
                for (int i = 0; i <= lstMedicalCat.Count - 1; i++)
                {
                    if (i == lstMedicalCat.Count - 1)
                    {
                        sbMedicalCat.Append(lstMedicalCat[i].ToString());
                    }
                    else
                    {
                        sbMedicalCat.Append(lstMedicalCat[i].ToString() + ",");
                    }
                }


                sbAppSelCols.Remove(0, sbAppSelCols.Length);
                List<String> lstAppSelCols = new List<String>(dictApptSelCols.Keys);
                values.Sort();

                for (int i = 0; i <= lstAppSelCols.Count - 1; i++)
                {
                    if (i == lstAppSelCols.Count - 1)
                    {
                        sbAppSelCols.Append(lstAppSelCols[i].ToString());
                    }
                    else
                    {
                        sbAppSelCols.Append(lstAppSelCols[i].ToString() + ",");
                    }
                }


                pnlssrsviewer.Visible = true;




                try
                {
                    //   this.Text = _reportTitle;
                    //  reportParam = "Conn=" + _conn;
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    reportViewer1.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("SSRS Reporting Service is not available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }

                gloSSRS.Create_Datasource("dsEMR", gstrMessageBoxCaption, _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);
                reportViewer1.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// _reportName;

                if (nStartdt == 0)
                {
                    nStartdt = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                    nEnddt = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
                }

                string strLocations = "";
                if (_reportName == "rptCancelAppointments")
                { 
                    strLocations =cmbLocation.SelectedValue.ToString();
                }
                
                string ParameterValue = "";
                string ParameterList = "";
                switch (_reportName)
                {

                    case "rptAppointmentList":



                        ParameterList += "FromDate,ToDate,ClinicID,UserName";
                        ParameterValue += "" + nStartdt + "|" + nEnddt + "|" + _ClinicID + "|" + _strUserName + "";

                        if (sbProviders.ToString().Trim() != "")
                        {
                            ParameterList += ",Providers";
                            ParameterValue += "|" + sbProviders.ToString();
                        }
                        else
                        {
                            ParameterList += ",Providers";
                            ParameterValue += "|0";
                        }

                        if (sbLocations.ToString().Trim() != "")
                        {
                            ParameterList += ",Locations";
                            //ParameterValue += "|" + strLocations.ToString();//9010 PER - Allow Multiple Locations in Appointment Report,  CAS-17831-P3P6L2 CRM:0022010
                            ParameterValue += "|" + sbLocations.ToString();
                        }
                        else
                        {
                            ParameterList += ",Locations";
                            ParameterValue += "|0";
                        }

                        if (sbPatientID.ToString().Trim() != "")
                        {
                            ParameterList += ",PatientID";
                            ParameterValue += "|" + sbPatientID.ToString();
                        }
                        else
                        {
                            ParameterList += ",PatientID";
                            ParameterValue += "|0";
                        }


                        if (sbResource.ToString().Trim() != "")
                        {
                            ParameterList += ",Resources";
                            ParameterValue += "|" + sbResource.ToString();
                        }
                        else
                        {
                            ParameterList += ",Resources";
                            ParameterValue += "|0";
                        }

                        if (sbMedicalCat.ToString().Trim() != "")
                        {
                            ParameterList += ",MedicalCategories";
                            ParameterValue += "|" + sbMedicalCat.ToString();
                        }
                        else
                        {
                            ParameterList += ",MedicalCategories";
                            ParameterValue += "|0";
                        }

                        if (sbAppSelCols.ToString().Trim() != "")
                        {
                            ParameterList += ",SelectColoums";
                            ParameterValue += "|" + sbAppSelCols.ToString();
                        }
                        else
                        {
                            ParameterList += ",SelectColoums";
                            ParameterValue += "|0";
                        }

                        SetParameter(ParameterList, ParameterValue);

                        _ParaNamesWithCSV =  ParameterList;

                        if (ParameterValue.Contains(","))
                        {
                            _ParaValuesWithCSV = ParameterValue;
                            _ParaValuesWithCSV = _ParaValuesWithCSV.Replace(",", "@");
                            _ParaValuesWithCSV = _ParaValuesWithCSV.Replace("|", ",");
                        }
                        else
                        {
                            _ParaValuesWithCSV = ParameterValue.Replace("|", ",");
                        }
                        

                        break;
                    case "rptCancelAppointments":

                       
                        int ntrackingstatus = 0;
                        if (rbNoShowAppointments.Checked)
                            ntrackingstatus = 5;
                        else if (rbDeletedAppointments.Checked)
                            ntrackingstatus = 7;
                        else
                            ntrackingstatus = 6;
                        ParameterList += "nFromDate,nToDate,UserName,clinicID,nTrackingStatus";
                        ParameterValue += "" + nStartdt + "|" + nEnddt + "|" + _strUserName + "|" + _ClinicID + "|" + ntrackingstatus + "";
                        if (sbProviders.ToString().Trim() != "")
                        {
                            ParameterList += ",nProviderID";
                            ParameterValue += "|" + sbProviders.ToString();
                        }
                        else
                        {
                            ParameterList += ",nProviderID";
                            ParameterValue += "|0";
                        }

                        if (Location.ToString().Trim() != "")
                        {
                            ParameterList += ",LocationID";
                            ParameterValue += "|" + strLocations.ToString();
                        }
                        else
                        {
                            ParameterList += ",LocationID";
                            ParameterValue += "|0";
                        }

                        if (sbPatientID.ToString().Trim() != "")
                        {
                            ParameterList += ",nPatientID";
                            ParameterValue += "|" + sbPatientID.ToString();
                        }
                        else
                        {
                            ParameterList += ",nPatientID";
                            ParameterValue += "|0";
                        }

                        if (sbMedicalCat.ToString().Trim() != "")
                        {
                            ParameterList += ",MedicalCategories";
                            ParameterValue += "|" + sbMedicalCat.ToString();
                        }
                        else
                        {
                            ParameterList += ",MedicalCategories";
                            ParameterValue += "|0";
                        }


                        ParameterList += ",nResourceID";
                        ParameterValue += "|0";

                        _ParaNamesWithCSV = ParameterList;

                        if (ParameterValue.Contains(","))
                        {
                            _ParaValuesWithCSV = ParameterValue;
                            _ParaValuesWithCSV = _ParaValuesWithCSV.Replace(",", "@");
                            _ParaValuesWithCSV = _ParaValuesWithCSV.Replace("|", ",");
                        }
                        else
                        {
                            _ParaValuesWithCSV = ParameterValue.Replace("|", ",");
                        }

                        SetParameter(ParameterList, ParameterValue);
                        break;

                }


                this.reportViewer1.ServerReport.SetParameters(paramList);
                this.reportViewer1.RefreshReport();
            }
            catch
            {
            }
            finally
            {
                nStartdt = 0;
                nEnddt = 0;
            }
        }
        
    
        private void LoadreportFromAppointment()
        {
           

            if (_dtProviders != null)
            {
                cmbProvider.DataSource = _dtProviders;
                cmbProvider.DisplayMember = "DispName";
                cmbProvider.ValueMember = "ID";
            }
                Loadreport(); 

        }
        private void SetParameter(string ParameterName, string ParameterValue)
        {
            paramList.Clear();
            string[] PName = ParameterName.Split(',');
            string[] Pvalue = ParameterValue.Split('|');
            int count = PName.Length;
            for (int i = 0; i <= count - 1; i++)
            {
                
                    Pvalue[i] = Pvalue[i].Replace("|", ",");
                
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i],false));
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
            {

             //08-Oct-14 Aniket: Resolving Bug #74864: gloEMR:Appointments Reports-application gives exception 
            tsb_Print.Enabled = true;
            }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Warning[] warnings = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string extension = null;
               byte[] bytes = null;
                string Format = null;
                int intindex;
                intindex = saveFileDialog1.FilterIndex;

                if (intindex == 1)
                    Format = "XML";
                else if (intindex == 2)
                    Format = "CSV";
                else if (intindex == 3)
                    Format = "Image";
                else if (intindex == 4)
                    Format = "PDF";
                else if (intindex == 5)
                    Format = "MHTML";
                else if (intindex == 6)
                    Format = "Excel";

              bytes = this.reportViewer1.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);

              FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
              fs.Write(bytes, 0, bytes.Length);
              fs.Close();
              fs.Dispose();
              fs = null;
              MessageBox.Show("File Saved Successfully ....!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
              gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, RptName + " Usage Report Exported..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbCancelAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if ((BoldFont != null) && (RegularFont != null))  //added for Bug #74630 selected radio button font style change to bold
            {
                if (rbCancelAppointments.Checked == true)
                {


                    rbDeletedAppointments.Font = RegularFont;
                    rbNoShowAppointments.Font = RegularFont;
                    rbCancelAppointments.Font = BoldFont;
                }
                
            }
        }

        private void rbNoShowAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if ((BoldFont != null) && (RegularFont != null)) //added for Bug #74630 selected radio button font style change to bold 
            {
                if (rbNoShowAppointments.Checked == true)
                {


                    rbDeletedAppointments.Font = RegularFont;
                    rbCancelAppointments.Font = RegularFont;
                    rbNoShowAppointments.Font = BoldFont;
                }
            }
        }

        private void rbDeletedAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if ((BoldFont != null) && (RegularFont != null)) //added for Bug #74630 selected radio button font style change to bold
            {
                if (rbDeletedAppointments.Checked == true)
                {


                    rbNoShowAppointments.Font = RegularFont;
                    rbCancelAppointments.Font = RegularFont;
                    rbDeletedAppointments.Font = BoldFont;   
                }
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndDate.MinDate = dtpStartDate.Value;  //enddate less than or equal to startdate  Bug #74568:  

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndDate.MinDate = dtpStartDate.Value;  //enddate less than or equal to startdate  Bug #74568:  
        }

        private void frmRPT_AppointmentView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (BoldFont != null)
            {
                BoldFont.Dispose();
                BoldFont = null;
            }
            if (RegularFont != null)
            {
                RegularFont.Dispose();
                RegularFont = null;
            }
        }

      

     
    }
}
