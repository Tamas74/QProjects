using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace gloEMRReports
{
    public partial class frmRpt_RecentVisit : Form
    {

        #region " Declarations"

        private string _databaseconnectionstring;
        gloEMRReportViewer _ogloEMRReportViewer;

        #endregion


        #region "Constructor"

        public frmRpt_RecentVisit(string databaseconnectionstring)
        {
            InitializeComponent();

            _ogloEMRReportViewer = new gloEMRReportViewer();

            _databaseconnectionstring = databaseconnectionstring;

            //Attaching the event Handler
            _ogloEMRReportViewer.onReportsClose_Clicked += new gloEMRReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloEMRReportViewer.onGenerateReport_Clicked += new gloEMRReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);

        }

        #endregion


        #region " Form Events"

        private void frmRpt_RecentVisit_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dock = DockStyle.Fill;

            FillPatientVisit();
        }

        #endregion


        #region "User Control Events"
        //Event For Generate Report
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            string Age = _ogloEMRReportViewer.sAge;

            FillPatientVisit();
        }

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region "Fill Methods"

        private void FillPatientVisit()
        {

            Rpt_472 oRpt472 = new Rpt_472();
            dsgloEMRReports dsReports = new dsgloEMRReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da;
        //    string sQuery = null;
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " +
                                          " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where  nclinicId=1";

                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Clinic_MST");
                da.Dispose();
                da = null;


                //String dtFrom = new DateTime(DateTime.Now.Year-3, 1, 1).ToShortDateString();
                //String dtToDate = DateTime.Today.ToShortDateString();

                DateTime dtFrom = new DateTime(DateTime.Now.Year - 3, 1, 1);
                DateTime dtToDate = DateTime.Today;
              
                //dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
                //dtpEndDate.Value = DateTime.Today;

                _sqlcommand.CommandText = "SELECT DISTINCT PatientID, PatientCode, FirstName, MiddleName, LastName,"+
                                          "CONVERT(VARCHAR,DOB,101)  AS DOB , Gender,AddressLine1, AddressLine2," +
                                          "CASE LEN(CONVERT(VARCHAR,isnull(City,'-'))) WHEN 0 THEN '-' ELSE City END AS City," +
                                           "CASE LEN(CONVERT(VARCHAR,isnull(state,'-'))) WHEN 0 THEN '-' ELSE state END AS State," +
                                           "CASE LEN(CONVERT(VARCHAR,isnull(Zip,'-'))) WHEN 0 THEN '-' ELSE Zip END AS Zip," +
                                           "isnull(Country,'-') as Country, "+
                                          "CASE LEN(CONVERT(VARCHAR,isnull(ExamName,'-'))) WHEN 0 THEN '-' ELSE ExamName END AS ExamName "+
                                          ", CONVERT(varchar, VisitDate, 101) AS sVisitDate, VisitDate," +
                                          "IsFinished, SSN, nExemptFromReport FROM vwCustRptPatientVisitExamInfo "+
                                          //"WHERE     CONVERT(varchar, VisitDate, 101) > '"+ dtFrom +"' and CONVERT(varchar, VisitDate, 101) < '"+ dtToDate +"'" +
                                           "WHERE      VisitDate > '" + dtFrom + "' and  VisitDate < '" + dtToDate + "'" +
                                          " ORDER BY VisitDate desc";

                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_vwCustRptPatientVisitExamInfo");

                da.Dispose();
                da = null;
                oRpt472.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloEMRReportViewer.ReportViewer = oRpt472;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null)
                {
                    oConnection.Close();
                    oConnection.Dispose();
                    oConnection = null;
                }
            }
        }

        #endregion

        private void frmRpt_RecentVisit_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ogloEMRReportViewer.onReportsClose_Clicked -= new gloEMRReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloEMRReportViewer.onGenerateReport_Clicked -= new gloEMRReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
            pnlContainer.Controls.Remove(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dispose();
        }

     
    }
}