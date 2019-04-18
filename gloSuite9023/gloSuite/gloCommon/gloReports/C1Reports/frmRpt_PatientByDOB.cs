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
    public partial class frmRpt_PatientByDOB : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_PatientByDOB objrptPatientByDOB;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

       
        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbMonths = new StringBuilder();

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region "Constructors"

        public frmRpt_PatientByDOB(string databaseconnectionstring)
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


        private void frmRpt_PatientByDOB_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showListMonths = true;
             
                _ogloReportViewer.showProviderCriteria = true;
             
                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;

                FillPatientByDOB();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Fill Methods"

        private void FillPatientByDOB()
        {
            if (objrptPatientByDOB != null)
            {
                objrptPatientByDOB.Dispose();
                objrptPatientByDOB = null;
            }
            objrptPatientByDOB = new Rpt_PatientByDOB();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {


                //nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                //nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Patient_By_DOB";
                _sqlcommand.Connection = oConnection;

                //_sqlcommand.Parameters.Add("@dtStartDate", System.Data.SqlDbType.DateTime);
                //_sqlcommand.Parameters.Add("@dtEndDate", System.Data.SqlDbType.DateTime);

                //_sqlcommand.Parameters["@dtStartDate"].Value = _ogloReportViewer.dtStartDate;
                //_sqlcommand.Parameters["@dtEndDate"].Value = _ogloReportViewer.dtEndDate;

                if (sbMonths.ToString() != "" && sbMonths.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@Month", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@Month"].Value = sbMonths.ToString();

                }

                if (sbProviders.ToString() != "" && sbProviders.Length > 0)
                {
                    _sqlcommand.Parameters.Add("@ProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@ProviderID"].Value = sbProviders.ToString();
              
                }

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_PatientByDOB");
                da.Dispose();

                objrptPatientByDOB.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptPatientByDOB;


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

                #region "Months"

                sbMonths.Remove(0, sbMonths.Length);

                Dictionary<Int64, String> dictMonths = new Dictionary<long, string>();
                dictMonths = _ogloReportViewer.dictMonthsLst;

                List<Int64> Monthsvalues = new List<Int64>(dictMonths.Keys);
                Monthsvalues.Sort();

                for (int i = 0; i <= Monthsvalues.Count - 1; i++)
                {
                    if (i == Monthsvalues.Count - 1)
                    {
                        sbMonths.Append(Monthsvalues[i].ToString());
                    }
                    else
                    {
                        sbMonths.Append(Monthsvalues[i].ToString() + ",");
                        //sbMonths = sbMonths + Monthsvalues[i].ToString() + ",";
                    }
                }
                #endregion

                FillPatientByDOB();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion
    }
}