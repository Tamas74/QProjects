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
    public partial class frmRpt_PatientReport : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_PatientReport objrptPatientReport;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructors"

        public frmRpt_PatientReport(string databaseconnectionstring)
        {
            InitializeComponent();

            _ogloReportViewer = new gloReportViewer();

            //Attaching the event Handler
            _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);


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

        #endregion



        #region "Form Events"

        private void frmRpt_PatientReport_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                //_ogloReportViewer.showTransCriteria = true;
                //_ogloReportViewer.showDatesCriteria = true;

                //_ogloReportViewer.showProviderCriteria = true;
                //_ogloReportViewer.showInsuranceCriteria = true;
                //_ogloReportViewer.showCPTCriteria = true;
                //_ogloReportViewer.showFacilityCriteria = true;

                //_ogloReportViewer.showZipCriteria = true;
                //_ogloReportViewer.showStateCriteria = true;
              
                //_ogloReportViewer.setdatesAsCurrentMonth();

                //Changed Code By Mitesh

                _ogloReportViewer.showTransCriteria = false;
                _ogloReportViewer.showDatesCriteria = false;

                _ogloReportViewer.showProviderCriteria = true;
                _ogloReportViewer.showInsuranceCriteria = false ;
                _ogloReportViewer.showCPTCriteria = false;
                _ogloReportViewer.showFacilityCriteria = false;

                _ogloReportViewer.showZipCriteria = false;
                _ogloReportViewer.showStateCriteria = false;

                _ogloReportViewer.setdatesAsCurrentMonth();
                //---


                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                FillPatientReport();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region "Fill Methods"

        private void FillPatientReport()
        {
            if (objrptPatientReport != null)
            {
                objrptPatientReport.Dispose();
                objrptPatientReport = null;
            }

            objrptPatientReport = new Rpt_PatientReport();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                //Changed By Mitesh
              //  nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
               // nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientReport";
                _sqlcommand.Connection = oConnection;

                //_sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                //_sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("nProviderID", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.Int);


                //_sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                //_sqlcommand.Parameters["@nToDate"].Value = nEnddt;
                if (sbProviders.ToString() == "")
                {
                    _sqlcommand.Parameters["nProviderID"].Value = null;
                }
                else
                {
                    _sqlcommand.Parameters["nProviderID"].Value = sbProviders.ToString();
                }
                
                _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;



                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_PatientReport");
                da.Dispose();
                objrptPatientReport.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptPatientReport;


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

                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                FillPatientReport();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion
    }
}