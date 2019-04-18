using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloBilling;
using System.Collections.Specialized;
using System.Collections;
using System.Web.UI.WebControls;

namespace gloReports.C1Reports
{
    public partial class frmRpt_AppointmentWaiting : Form
    {
       
             
        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer=new gloReportViewer();

        //For Creating the Object of the CrystalReport
        //Rpt_Appointments objrptAppointments;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();

      //  private Boolean _IsFromCalendar;
     //   private DataTable _dtProviders;
      //  private bool _IsPrint=false;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        const int COL_PATIENTID = 0;
        const int COL_PATIENTCODE = 1;
        const int COL_PATIENTNAME = 2;
        const int COL_DOB = 3;
        const int COL_PHONENO = 4;
        const int COL_PROVIDER = 5;
        const int COL_APPOINTMENTTYPE = 6;
        const int COL_APPOINTMENTDATE = 7;
        const int COL_STARTTIME = 8;
        const int COL_ENDTIME = 9;
        const int COL_DURATION = 10;
        const int COL_INSURANCENAME = 11;
        const int COL_PLOCIY = 12;
        const int COL_NOTES = 13;
        const int COL_COPAY = 14;
        const int COL_COUNT = 15;

        //public delegate void onPatientsSelectedIndexChanged(object sender, EventArgs e);
        //public event onPatientsSelectedIndexChanged onPatients_SelectedIndexChanged;


        //Array for storing the Months
        private string[] Months = new string[13];

    //    private Boolean FlagPatientMultiselect;

   //     private Boolean FlagCPTMultiselect = true;

     //   private Int64 _setPatientID;

     //   private gloGeneralItem.gloItems ogloItems = null;


        public Int64 dtAgingDate;

        //Variables for Report from Calendar
        public DataTable _dtProvider;
        public DateTime _startdate;
        public DateTime _enddate;
        //bool _IsPrint = false;
      


        //C1.Win.C1FlexGrid.C1FlexGrid c1AppointmentWaiting = new C1.Win.C1FlexGrid.C1FlexGrid();
        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_AppointmentWaiting(string databaseconnectionstring)
        {
            InitializeComponent();

            //_ogloReportViewer = new gloReportViewer();

            ////Attaching the event Handler
            //_ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            //_ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
          

            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
        }

        ////Added By MaheshB For Calendar.
        //public frmRpt_AppointmentWaiting(string databaseconnectionstring, DataTable dtProviders, DateTime dtstartdate, DateTime dtenddate, bool Isprint)
        //{
        //    InitializeComponent();
        //    //_Isprint = Isprint;
        //    _ogloReportViewer = new gloReportViewer(dtProviders, dtstartdate, dtenddate, Isprint);
        //    _IsPrint = Isprint;
        //    //Attaching the event Handler
        //    _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
        //    _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);


        //    _databaseconnectionstring = databaseconnectionstring;
        //    if (appSettings["ClinicID"] != null)
        //    {
        //        if (appSettings["ClinicID"] != "")
        //        { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
        //        else { _ClinicID = 0; }
        //    }
        //    else
        //    { _ClinicID = 0; }
        //    if (dtProviders != null)
        //    {
        //        if (dtProviders.Rows.Count > 0)
        //        {
        //            _IsFromCalendar = true;
        //            _dtProviders = dtProviders.Copy();
        //        }
        //    }
        //    if (Isprint == true)
        //    {
        //        this.Hide();
        //    }

        //}

        #endregion


        #region "Form Events"

        private void frmRpt_AppointmentWaiting_Load(object sender, EventArgs e)
        {
            try
            {
                //c1AppointmentWaiting = new C1.Win.C1FlexGrid.C1FlexGrid(); 
                //For Addding the ReportViewer User Control in form
                //pnlCriteria.Dock = DockStyle.Top;
                //pnlCriteria.Controls.Add(_ogloReportViewer);
                //_ogloReportViewer.Dock = DockStyle.Fill;

                Fill_FilterDatesCombo();
                FillLocation();
                DesignGrid();

                //For Hiding the controls from the Search Criteria
                //_ogloReportViewer.showOnlyCriteria = false;
                ////_ogloReportViewer.showDatesCriteria = true;

                //_ogloReportViewer.showPatientCriteria = true;
                //_ogloReportViewer.showPatientMultiselect = true;

                //_ogloReportViewer.showProviderCriteria = true;
                //_ogloReportViewer.showLocation = true;

                //if (_IsFromCalendar==false)
                //{
                //    _ogloReportViewer.setdatesAsCurrentMonth();
                //}

                ////Property to show the Export Button on Tool Bar
                //_ogloReportViewer.showExport = true;
 

                //nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                //nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                //if (_IsFromCalendar == false)
                //{
                    FillAppointments("", ""); ;
                //}
                //else
                //{
                //    string Providers="";
                //    for (int i = 0; i < _dtProviders.Rows.Count; i++)
                //    {
                //        if (i != 0)
                //        {
                //            Providers = Providers + "," + Convert.ToString(_dtProviders.Rows[i]["ID"]);
                //        }
                //        else
                //        {
                //            Providers =Convert.ToString(_dtProviders.Rows[i]["ID"]);
                //        }
                        
                //    }
                //    if (Providers.EndsWith(",") == true)
                //    {
                //      Providers=Providers.Remove(Providers.Length - 1);
                //    }
                //    //FillAppointments("","");
                    
                //        FillAppointments(Providers, "");
                //        if (_IsPrint == true)
                //        {
                //            this.Hide();
                //            this.Visible = false;
                //            _ogloReportViewer.tsb_Print_Click(null, null);
                            
                //            this.Close();
                //            Application.DoEvents();
                //        }
                //        //else
                //        //{
                //        //    _ogloReportViewer.tsb_Print_Click(null, null);
                //        //}


                //        FillLocation();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

         
        #endregion
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
                //case gloListControl.gloListControlType.AllPatientInsurances:
                //    {
                //        cmbInsurance.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");
                //            ogloItems = new gloGeneralItem.gloItems();
                //            gloGeneralItem.gloItem ogloItem = null;
                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //                ogloItem = new gloGeneralItem.gloItem();
                //                ogloItem.ID = oListControl.SelectedItems[_Counter].ID;
                //                ogloItem.Description = oListControl.SelectedItems[_Counter].Description;
                //                ogloItems.Add(ogloItem);
                //            }

                //            cmbInsurance.DataSource = oBindTable;
                //            cmbInsurance.DisplayMember = "DispName";
                //            cmbInsurance.ValueMember = "ID";
                //        }


                //    }
                //    break;
                //case gloListControl.gloListControlType.Diagnosis:
                //    {
                //        cmbDiagnosisCode.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbDiagnosisCode.DataSource = oBindTable;
                //            cmbDiagnosisCode.DisplayMember = "DispName";
                //            cmbDiagnosisCode.ValueMember = "ID";
                //        }


                //    }
                //    break;
                //case gloListControl.gloListControlType.CPT:
                //    {
                //        cmbCPT.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbCPT.DataSource = oBindTable;
                //            cmbCPT.DisplayMember = "DispName";
                //            cmbCPT.ValueMember = "ID";
                //        }


                //    }
                //    break;

                //case gloListControl.gloListControlType.AppointmentType:
                //    {
                //        cmbApp_AppointmentType.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbApp_AppointmentType.DataSource = oBindTable;
                //            cmbApp_AppointmentType.DisplayMember = "DispName";
                //            cmbApp_AppointmentType.ValueMember = "ID";
                //        }

                //    }
                //    break;

                //case gloListControl.gloListControlType.Facility:
                //    {
                //        cmbMultiFacility.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbMultiFacility.DataSource = oBindTable;
                //            cmbMultiFacility.DisplayMember = "DispName";
                //            cmbMultiFacility.ValueMember = "ID";
                //        }

                //    }
                //    break;

                //case gloListControl.gloListControlType.ChargeTray:
                //    {
                //        cmbMultiChargesTray.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbMultiChargesTray.DataSource = oBindTable;
                //            cmbMultiChargesTray.DisplayMember = "DispName";
                //            cmbMultiChargesTray.ValueMember = "ID";
                //        }

                //    }
                //    break;



                //case gloListControl.gloListControlType.PaymentTray:
                //    {
                //        cmblMultiPayTray.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmblMultiPayTray.DataSource = oBindTable;
                //            cmblMultiPayTray.DisplayMember = "DispName";
                //            cmblMultiPayTray.ValueMember = "ID";
                //        }

                //    }
                //    break;

                //case gloListControl.gloListControlType.Users:
                //    {
                //        cmbUser.DataSource = null;
                //        if (oListControl.SelectedItems.Count > 0)
                //        {
                //            DataTable oBindTable = new DataTable();

                //            oBindTable.Columns.Add("ID");
                //            oBindTable.Columns.Add("DispName");

                //            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                //            {
                //                DataRow oRow;
                //                oRow = oBindTable.NewRow();
                //                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                //                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                //                oBindTable.Rows.Add(oRow);
                //            }

                //            cmbUser.DataSource = oBindTable;
                //            cmbUser.DisplayMember = "DispName";
                //            cmbUser.ValueMember = "ID";
                //        }

                //    }
                //    break;

                default:
                    {
                    }
                    break;
            }
        }
        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (PnlC1.Controls.Contains(oListControl))
                {
                    PnlC1.Controls.Remove(oListControl);
                }
                oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dispose();
                oListControl = null;
            }
        }
        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeOListControl();
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
        }


        #region "Fill Methods"

        private void FillAppointments(string ProviderID, string PatientID)
        {

            //objrptAppointments = new Rpt_Appointments();
            //dsC1Reports dsReports = new dsC1Reports();
            //SqlCommand _sqlcommand = new SqlCommand();
            //SqlConnection oConnection = new SqlConnection();

            try
            {
                //oConnection.ConnectionString = _databaseconnectionstring;
                //_sqlcommand.CommandType = CommandType.StoredProcedure;
                //_sqlcommand.CommandText = "Rpt_AppointmentsWaiting";
                //_sqlcommand.Connection = oConnection;

                gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
                odbparam.Add("@nFromDate", nStartdt, ParameterDirection.Input, System.Data.SqlDbType.Int);
                odbparam.Add("@nToDate", nEnddt, ParameterDirection.Input, System.Data.SqlDbType.Int);
                odbparam.Add("@clinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                odbparam.Add("@nRefFlag", 0, ParameterDirection.Input, SqlDbType.BigInt);

                if (ProviderID != "")
                {
                    odbparam.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.NVarChar);
                }
                else
                {

                    odbparam.Add("@nProviderID", null, ParameterDirection.Input, SqlDbType.NVarChar);
                }

                if (PatientID != "")
                {
                    odbparam.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.NVarChar);
                }
                else
                {
                    odbparam.Add("@nPatientID", null, ParameterDirection.Input, SqlDbType.NVarChar);
                }
                if (Convert.ToString(cmbLocation.SelectedValue)!="0")
                {
                    odbparam.Add("@LocationID", Convert.ToInt64(cmbLocation.SelectedValue), ParameterDirection.Input, SqlDbType.NVarChar);
                }
                else
                {
                    odbparam.Add("@LocationID",null, ParameterDirection.Input, SqlDbType.Int);
                }

                odbparam.Add("@nTrackingStatus", 2, ParameterDirection.Input, SqlDbType.BigInt);







                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                DataTable dtAppointments = null;
                ODB.Retrive("Rpt_AppointmentsWaiting", odbparam,out dtAppointments);
                ODB.Disconnect();
                ODB.Dispose();
                ODB = null;


                if (dtAppointments != null)
                {
                    if (c1AppointmentWaiting.Rows.Count > 0)
                    {
                        c1AppointmentWaiting.Rows.RemoveRange(1, c1AppointmentWaiting.Rows.Count - 1);
                    }
                    for (int i = 0; i < dtAppointments.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1AppointmentWaiting.Rows.Add();

                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PATIENTID, Convert.ToString(dtAppointments.Rows[i]["nPatientID"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PATIENTCODE, Convert.ToString(dtAppointments.Rows[i]["sPatientCode"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PHONENO, Convert.ToString(dtAppointments.Rows[i]["sPhone"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PROVIDER, Convert.ToString(dtAppointments.Rows[i]["sProviderName"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_APPOINTMENTDATE, Convert.ToString(dtAppointments.Rows[i]["dtStartDate"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_STARTTIME, Convert.ToString(dtAppointments.Rows[i]["dtStartTime"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_DURATION, Convert.ToString(dtAppointments.Rows[i]["nDuration"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_INSURANCENAME, Convert.ToString(dtAppointments.Rows[i]["sInsuranceName"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PLOCIY, Convert.ToString(dtAppointments.Rows[i]["sSubscriberPolicy"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_NOTES, Convert.ToString(dtAppointments.Rows[i]["sNOTES"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_COPAY, Convert.ToString(dtAppointments.Rows[i]["Copay"]));

                        c1AppointmentWaiting.SetData(NewRow.Index, COL_APPOINTMENTTYPE, Convert.ToString(dtAppointments.Rows[i]["sAppointmentType"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_DOB, Convert.ToString(dtAppointments.Rows[i]["dtDOB"]));
                        c1AppointmentWaiting.SetData(NewRow.Index, COL_PATIENTNAME, Convert.ToString(dtAppointments.Rows[i]["sPatientName"]));
                    }
                    dtAppointments.Dispose();
                    dtAppointments = null;
                }
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Rep, gloAuditTrail.ActivityCategory.MissingCharges, gloAuditTrail.ActivityType.View, "View Missing Charges", gloAuditTrail.ActivityOutCome.Success);
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
                //    if (//_sqlcommand != null)
                //    {
                //        //_sqlcommand.Dispose();
                //    }
            }
        }

        // For Filling The Facilities Combo
        //private void FillFacilities()
        //{
        //    try
        //    {
        //        DataTable _dtLocations = new DataTable();
        //        gloFacility ogloFacilities = new gloFacility(_databaseconnectionstring);
        //        _dtLocations = ogloFacilities.GetFacilities();

        //        DataTable dtLocations;
        //        if (_dtLocations != null && _dtLocations.Rows.Count > 0)
        //        {
        //            dtLocations = new DataTable();
        //            dtLocations.Columns.Add("sFacilityCode");
        //            dtLocations.Columns.Add("sFacilityName");

        //            dtLocations.Clear();
        //            dtLocations.Rows.Add(0, "");

        //            for (int i = 0; i < _dtLocations.Rows.Count; i++)
        //            {
        //                dtLocations.Rows.Add(_dtLocations.Rows[i]["sFacilityCode"], _dtLocations.Rows[i]["sFacilityName"]);
        //            }

        //            cmbFacility.DataSource = dtLocations;
        //            cmbFacility.DisplayMember = "sFacilityName";
        //            cmbFacility.ValueMember = "sFacilityCode";
        //        }

        //        _dtLocations = null;
        //        ogloFacilities.Dispose();


        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        // For Filling the Dates Combo
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


        //private void FillMonths()
        //{
        //    try
        //    {
        //        //this.drpMonth.SelectedIndexChanged -= new System.EventHandler(this.drpMonth_SelectedIndexChanged);
        //        for (int i = 0; i <= 12; i++)
        //        {
        //            ListItem liMonths = new ListItem(Months[i], i.ToString());
        //            drpMonth.Items.Add(liMonths);

        //        }
        //        drpMonth.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        //this.drpMonth.SelectedIndexChanged += new System.EventHandler(this.drpMonth_SelectedIndexChanged);
        //    }


        //}

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
                if (_dtLocations != null)
                {
                    _dtLocations.Dispose();
                    _dtLocations = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oLocation.Dispose();
                oLocation = null;
            }
        }

        public string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                if (dtPatient != null)
                {
                    _result = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                    dtPatient.Dispose();
                    dtPatient = null;
                }


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null; 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            {

               
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _result;

        }

        #endregion


        #region "User Control Events"

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }


        //Event For Generate Report
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            try
            {

                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);

                //Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                //dictProviders = _ogloReportViewer.dictProviders;

                //List<Int64> values = new List<Int64>(dictProviders.Keys);
                //values.Sort();
                

                for (int i = 0; i <= cmbProvider.Items.Count - 1; i++)
                {
                    if (i == cmbProvider.Items.Count - 1)
                    {
                        sbProviders.Append(cmbProvider.Items[i].ToString());
                    }
                    else
                    {
                        sbProviders.Append(cmbProvider.Items[i].ToString() + ",");
                    }
                }
                #endregion


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                //Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                //dictPatientID = _ogloReportViewer.dictPatients;

                //List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                //lstPatvalues.Sort();

                for (int i = 0; i <= cmbPatients.Items.Count - 1; i++)
                {
                    if (i == cmbPatients.Items.Count - 1)
                    {
                        sbPatientID.Append(cmbPatients.Items[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(cmbPatients.Items[i].ToString() + ",");
                    }
                }

                #endregion

                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());
                

                FillAppointments(sbProviders.ToString(),sbPatientID.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void frmRpt_AppointmentWaiting_LocationChanged(object sender, EventArgs e)
        {

        }

        //private void onExportToExcelOpen_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //// Declare variables and get the export options.
        //        //ExportOptions exportOpts = new ExportOptions();
        //        //ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
        //        //DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
        //        //exportOpts = objrptAppointments .ExportOptions;

        //        //// Set the excel format options.
        //        //excelFormatOpts.ExcelUseConstantColumnWidth = true;
        //        //excelFormatOpts.ExcelConstantColumnWidth = 500;
        //        //excelFormatOpts.ExcelTabHasColumnHeadings = true;
 
        //        //exportOpts.ExportFormatType = ExportFormatType.Excel;
        //        //exportOpts.FormatOptions = excelFormatOpts;

        //        //// Set the disk file options and export.
        //        //exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
        //        //diskOpts.DiskFileName = "aaa.xls";
        //        //exportOpts.DestinationOptions = diskOpts;

        //        //objrptAppointments.Export();
        //        //objrptAppointments.Export();
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //}


        #endregion

        #region Design Grid

        private void DesignGrid()
        {
            c1AppointmentWaiting.Rows.Count = 1;
            c1AppointmentWaiting.Cols.Count = COL_COUNT;

            //c1AppointmentWaiting.SetData(0, COL_DATEOFSERVICE, "Date Of Service");
            c1AppointmentWaiting.SetData(0, COL_PATIENTID, "Patient ID");
            c1AppointmentWaiting.SetData(0, COL_PATIENTNAME, "Patient Name");
            c1AppointmentWaiting.SetData(0, COL_APPOINTMENTTYPE, "Appointment Type");

            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1AppointmentWaiting.SetData(0, COL_PATIENTCODE, "Patient Code");
            c1AppointmentWaiting.SetData(0, COL_PHONENO, "Phone No");
            c1AppointmentWaiting.SetData(0, COL_PROVIDER, "Privider");
             c1AppointmentWaiting.SetData(0, COL_APPOINTMENTDATE, "Appointment Date");
             c1AppointmentWaiting.SetData(0, COL_DOB, "Date Of Birth");
             c1AppointmentWaiting.SetData(0, COL_DURATION, "Duration");
             c1AppointmentWaiting.SetData(0, COL_INSURANCENAME, "Insurance Name");
             c1AppointmentWaiting.SetData(0, COL_PLOCIY, "POLICY");
             c1AppointmentWaiting.SetData(0, COL_NOTES, "NOTES");
             c1AppointmentWaiting.SetData(0, COL_COPAY, "COPY");
             c1AppointmentWaiting.SetData(0, COL_DURATION, "Duration");
             c1AppointmentWaiting.SetData(0, COL_STARTTIME, "StartTime");
             c1AppointmentWaiting.SetData(0, COL_ENDTIME, "StartTime");

            //c1AppointmentWaiting.Cols[COL_DATEOFSERVICE].Width = 100;
            c1AppointmentWaiting.Cols[COL_PATIENTID].Width = 0;
            c1AppointmentWaiting.Cols[COL_PATIENTNAME].Width = 300;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTTYPE].Width = 170;
            c1AppointmentWaiting.Cols[COL_PROVIDER].Width = 170;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTDATE].Width = 170;

            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1AppointmentWaiting.Cols[COL_PATIENTCODE].Width = 180;
            c1AppointmentWaiting.Cols[COL_PHONENO].Width = 100;
            c1AppointmentWaiting.Cols[COL_DOB].Width = 100;


            //c1AppointmentWaiting.Cols[COL_DATEOFSERVICE].Visible = true;
            c1AppointmentWaiting.Cols[COL_PATIENTID].Visible = false;
            c1AppointmentWaiting.Cols[COL_PATIENTNAME].Visible = true;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTTYPE].Visible = true;


         
            c1AppointmentWaiting.Cols[COL_PATIENTCODE].Visible = true;
            c1AppointmentWaiting.Cols[COL_PHONENO].Visible = true;
            c1AppointmentWaiting.Cols[COL_DOB].Visible = true;

            c1AppointmentWaiting.Cols[COL_PHONENO].Visible = true;
            c1AppointmentWaiting.Cols[COL_PROVIDER].Visible = true;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTTYPE].Visible = true;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTDATE].Visible = true;
            c1AppointmentWaiting.Cols[COL_STARTTIME].Visible = true;
            //c1AppointmentWaiting.Cols[COL_ENDTIME].Visible = true;
            c1AppointmentWaiting.Cols[COL_DURATION].Visible = true;
            c1AppointmentWaiting.Cols[COL_INSURANCENAME].Visible = true;
            c1AppointmentWaiting.Cols[COL_PLOCIY].Visible = true;
            c1AppointmentWaiting.Cols[COL_NOTES].Visible = true;
            c1AppointmentWaiting.Cols[COL_COPAY].Visible = true;
            c1AppointmentWaiting.Cols[COL_ENDTIME].Visible = false;

            //c1AppointmentWaiting.Cols[COL_DATEOFSERVICE].DataType = typeof(System.DateTime);

            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1AppointmentWaiting.Cols[COL_DOB].DataType = typeof(System.DateTime);

            //c1AppointmentWaiting.Cols[COL_DATEOFSERVICE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1AppointmentWaiting.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1AppointmentWaiting.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1AppointmentWaiting.Cols[COL_APPOINTMENTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            //Added By Pramod Nair For Displaying Additional Columns --- 2009-05-27
            c1AppointmentWaiting.Cols[COL_PATIENTCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1AppointmentWaiting.Cols[COL_PHONENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1AppointmentWaiting.Cols[COL_DOB].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            //c1AppointmentWaiting.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            c1AppointmentWaiting.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

            c1AppointmentWaiting.AllowEditing = false;
            c1AppointmentWaiting.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
           
        }

        #endregion

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
            //DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);
            //DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
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



        private void btnBrowsePatient_Click(object sender, EventArgs e)
        {
            try
            {
                removeOListControl();
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

                //Used Only for Patient Statement - Temp Code
                bool FlagPatientMultiselect=true;

                if (FlagPatientMultiselect)
                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                else
                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);


                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                PnlC1.Controls.Add(oListControl);

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

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
           // cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;
            cmbPatients.Items.Clear();
            cmbPatients.Refresh();
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
        }

        private void btnBrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
                removeOListControl();
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

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
           
            cmbProvider.DataSource = null;
            cmbProvider.Items.Clear();
            cmbProvider.Refresh();
        }

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            try
            {

                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);

                //Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                //dictProviders = _ogloReportViewer.dictProviders;

                //List<Int64> values = new List<Int64>(dictProviders.Keys);
                //values.Sort();


                for (int i = 0; i <= cmbProvider.Items.Count - 1; i++)
                {
                    if (i == cmbProvider.Items.Count - 1)
                    {
                        sbProviders.Append(cmbProvider.SelectedValue.ToString());
                        cmbProvider.SelectedIndex = i;
                    }
                    else
                    {
                        sbProviders.Append(cmbProvider.SelectedValue.ToString() + ",");
                        cmbProvider.SelectedIndex = i;
                    }
                }
                #endregion


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                //Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                //dictPatientID = _ogloReportViewer.dictPatients;

                //List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                //lstPatvalues.Sort();

                for (int i = 0; i <= cmbPatients.Items.Count - 1; i++)
                {
                    if (i == cmbPatients.Items.Count - 1)
                    {
                        sbPatientID.Append(cmbPatients.SelectedValue.ToString()); 
                        cmbPatients.SelectedIndex = i;
                    }
                    else
                    {
                        sbPatientID.Append(cmbPatients.SelectedValue.ToString() + ",");
                        cmbPatients.SelectedIndex = i;
                    }
                }

                #endregion

                nStartdt = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());


                FillAppointments(sbProviders.ToString(), sbPatientID.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            c1AppointmentWaiting.PrintGrid("Wating Appointment");
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
       
            //if (onPatients_SelectedIndexChanged != null)
            //{
            //    onPatients_SelectedIndexChanged(sender, e);
            //}
       
        }


    }
}