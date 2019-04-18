using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace gloReports.C1Reports
{
    public partial class frmRpt_Appointments : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_Appointments objrptAppointments;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();
        private StringBuilder sbResource = new StringBuilder();

        private Boolean _IsFromCalendar;
        private DataTable _dtProviders;
        private bool _IsPrint = false;
        private bool _DefaultPrinter = false;
        string _strUserName = "";

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_Appointments(string databaseconnectionstring)
        {
            InitializeComponent();

            _ogloReportViewer = new gloReportViewer();

            //Attaching the event Handler
            _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);

            // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
            // Print report event attached
            _ogloReportViewer.onPrintReport_Clicked += new gloReportViewer.onPrintReportClicked(ogloReports_onPrintReport_Clicked);

            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrive UserName from appSettings "

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

            #endregion
        }

        //Added By MaheshB For Calendar.
        public frmRpt_Appointments(string databaseconnectionstring, DataTable dtProviders, DateTime dtstartdate, DateTime dtenddate, bool Isprint)
        {
            InitializeComponent();
            //_Isprint = Isprint;
            _ogloReportViewer = new gloReportViewer(dtProviders, dtstartdate, dtenddate, Isprint);
            _IsPrint = Isprint;

            if (appSettings["DefaultPrinter"] != null)
            {
                if (appSettings["DefaultPrinter"] != "")
                { _DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]); }
                else { _DefaultPrinter = false; }
            }
            else
            { _DefaultPrinter = false; }
            //Attaching the event Handler
            _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);

            // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
            // Print report event attached
            _ogloReportViewer.onPrintReport_Clicked += new gloReportViewer.onPrintReportClicked(ogloReports_onPrintReport_Clicked);

            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #region " Retrive UserName from appSettings "

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

            #endregion
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
            if (Isprint == true)
            {
                _IsFromCalendar = true;
                this.Hide();
            }

        }

        #endregion


        #region "Form Events"
        private void frmRpt_Appointments_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showTransCriteria = true;
                _ogloReportViewer.showDatesCriteria = true;

                _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showPatientMultiselect = true;

                _ogloReportViewer.showProviderCriteria = true;
                _ogloReportViewer.showLocation = true;

                _ogloReportViewer.showAppointmentSort = false;
                _ogloReportViewer.showresourcepanel = true;

                if (_IsFromCalendar == false)
                {
                    _ogloReportViewer.setdatesAsCurrentMonth();
                }
                _ogloReportViewer.setCrystalReportViewerProperties();

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;

                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                pnlContainer.Height = this.Height - 30;
                pnlContainer.Width = this.Width;
                if (_IsFromCalendar == false)
                {
                    FillAppointments("", "", ""); ;
                }
                else
                {
                    string Providers = "";
                    if (_dtProviders != null && _dtProviders.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtProviders.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                Providers = Providers + "," + Convert.ToString(_dtProviders.Rows[i]["ID"]);
                            }
                            else
                            {
                                Providers = Convert.ToString(_dtProviders.Rows[i]["ID"]);
                            }

                        }
                        if (Providers.EndsWith(",") == true)
                        {
                            Providers = Providers.Remove(Providers.Length - 1);
                        }
                    }
                    //FillAppointments("","");

                    FillAppointments(Providers, "", "");

                    if (_IsPrint == true)
                    {
                        // Hide the form
                        this.Hide();
                        this.Visible = false;

                        // Send the Appoinments to print
                        SendToPrint();

                        // Close the form
                        this.Close();
                        Application.DoEvents();
                    }
                }
                //else
                //{
                //    _ogloReportViewer.tsb_Print_Click(null, null);
                //}

                this.AutoScroll = false;
            }

            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion


        #region "Fill Methods"

        private void FillAppointments(string ProviderID, string PatientID, string ResourceID)
        {
            if (objrptAppointments != null)
            {
                objrptAppointments.Dispose();
                objrptAppointments = null;
            }
            objrptAppointments = new Rpt_Appointments();
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


                #region "OLD LOGIC"
                //if ((ProviderID == null || ProviderID == "") && (ResourceID != null && ResourceID != ""))
                //{
                //    _Value = 3; 
                //}
                //if ((ProviderID != null && ProviderID != "") && (ResourceID != null && ResourceID != ""))
                //{
                // //    _Value = 1;
                // //}
                //// _sqlcommand.Parameters["@nRefFlag"].Value = _Value;

                // if (_ogloReportViewer.bsortAppointmentDates)
                // {
                //     _sortMode = 2;
                // }
                // else
                // {
                //     _sortMode = 1;
                // }
                // _sqlcommand.Parameters.Add("@nsortMode", System.Data.SqlDbType.BigInt);
                // _sqlcommand.Parameters["@nsortMode"].Value = _sortMode;

                #endregion
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

                //To Check For Location Criteria
                if (_ogloReportViewer.sLocation != null && _ogloReportViewer.sLocation != "")
                {
                    _sqlcommand.Parameters.Add("@Locations", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@Locations"].Value = _ogloReportViewer.sLocation;
                }
                if (ResourceID != "")
                {
                    _sqlcommand.Parameters.Add("@Resources", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@Resources"].Value = ResourceID;
                }
                _sqlcommand.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Appointments");
                #region "Old Logic"
                //if (dsReports.Tables[0].Rows.Count > 0)
                //{

                //    for (int iRowCount = 0; iRowCount <= dsReports.Tables[0].Rows.Count - 1; iRowCount++)
                //    {

                //        if ((DiffMstAppID == Convert.ToInt64(dsReports.Tables[0].Rows[iRowCount]["nMSTAppointmentID"])))
                //        {
                //            if(Convert.ToInt64(dsReports.Tables[0].Rows[iRowCount]["bIsSingleRecurrence"])==1)
                //             dsReports.Tables[0].Rows[iRowCount].Delete(); 
                //        }
                //        else
                //        { 
                //            DiffMstAppID = Convert.ToInt64(dsReports.Tables[0].Rows[iRowCount]["nMSTAppointmentID"]);
                //            dsReports.Tables[0].Rows[iRowCount]["UserName"] = _strUserName;
                //            //code below added to show resources and providers together
                //            DataTable dt = new DataTable();
                //            _sqlcommand = null;
                //            _sqlcommand = new SqlCommand();
                //            _sqlcommand.CommandType = CommandType.StoredProcedure;
                //            _sqlcommand.CommandText = "SP_GetApptmntResources";
                //            _sqlcommand.Connection = oConnection;
                //            _sqlcommand.Parameters.Add("@nAppointmentID", System.Data.SqlDbType.BigInt);
                //            _sqlcommand.Parameters["@nAppointmentID"].Value = dsReports.Tables[0].Rows[iRowCount]["nMSTAppointmentID"];
                //            _sqlcommand.Parameters.Add("@nAppointmentDate", System.Data.SqlDbType.BigInt);
                //            _sqlcommand.Parameters["@nAppointmentDate"].Value = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dsReports.Tables[0].Rows[iRowCount]["dtStartDate"]));
                //            da = new SqlDataAdapter(_sqlcommand);
                //            da.Fill(dt);

                //            dsReports.Tables[0].Rows[iRowCount]["sProviderName"] = Convert.ToString(dt.Rows[0][0]);
                //        }
                //    }
                //}
                //dsReports.AcceptChanges();  
                #endregion
                da.Dispose();
                objrptAppointments.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptAppointments;


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

                Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                dictProviders = _ogloReportViewer.dictProviders;

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


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                dictPatientID = _ogloReportViewer.dictPatients;

                List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
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
                Dictionary<Int64, String> dictResourceID = new Dictionary<long, string>();
                dictResourceID = _ogloReportViewer.dictResource;

                List<Int64> lstResvalues = new List<Int64>(dictResourceID.Keys);
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


                #endregion

                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                FillAppointments(sbProviders.ToString(), sbPatientID.ToString(), sbResource.ToString());

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
        // If Default printer is set then print directly without prompting the print dialogue
        // Else show the print dialogue to select the printer
        private void ogloReports_onPrintReport_Clicked(object sender, EventArgs e)
        {
            SendToPrint();
        }

        // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
        // This method will send the appointments to printer based on the default printer setting.
        private void SendToPrint()
        {
            try
            {
                //For Fetching the Default Printer setting
                if (appSettings["DefaultPrinter"] != null)
                {
                    if (appSettings["DefaultPrinter"] != "")
                    { _DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]); }
                    else { _DefaultPrinter = false; }
                }
                else
                { _DefaultPrinter = false; }


                if (objrptAppointments != null)
                {
                    if (_DefaultPrinter == false)
                    {
                        // Default printer is not set, show print dialoge box
                        if (PrintDialog1 != null)
                        {
                            PrintDialog1.Dispose();
                            PrintDialog1 = null;
                        }
                        PrintDialog1 = new PrintDialog();
                        PrintDialog1.UseEXDialog = true;

                        if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            objrptAppointments.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                            // Bug #52496: 00000131 : Printing - EMR
                            // Copies and Collate Parameters passed to resolve issue.
                            objrptAppointments.PrintToPrinter(PrintDialog1.PrinterSettings.Copies, PrintDialog1.PrinterSettings.Collate, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage);
                        }
                        PrintDialog1.Dispose();
                        PrintDialog1 = null;
                    }
                    else
                    {
                        // Default printer is set, send directly to print
                        PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = true;
                        objrptAppointments.PrintToPrinter(1, false, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
        // Detaching the events associated to reportviewer object
        private void frmRpt_Appointments_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ogloReportViewer != null)
            {
                _ogloReportViewer.onReportsClose_Clicked -= new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
                _ogloReportViewer.onGenerateReport_Clicked -= new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
                _ogloReportViewer.onPrintReport_Clicked -= new gloReportViewer.onPrintReportClicked(ogloReports_onPrintReport_Clicked);
                _ogloReportViewer.Dispose();
                _ogloReportViewer = null;
            }

            if (PrintDialog1 != null)
            {
                PrintDialog1.Dispose();
                PrintDialog1 = null;
            }

            if (objrptAppointments != null)
            {
                objrptAppointments.Close();
                objrptAppointments.Dispose();
                objrptAppointments = null;
            }
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

    }
}