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

    public partial class frmRpt_PatientAge : Form
    {

        #region " Declarations"

        private string _databaseconnectionstring;
        gloEMRReportViewer _ogloEMRReportViewer;

        #endregion

        
        #region "Constructor"

        public frmRpt_PatientAge(string databaseconnectionstring)
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

        private void frmRpt_PatientAge_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dock = DockStyle.Fill;

            _ogloEMRReportViewer.showAgingCriteria = true;

            FillPatientsByAge("");

        }

        #endregion

        
        #region "User Control Events"

        //Event For Generate Report
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            string Age = _ogloEMRReportViewer.sAge;

            FillPatientsByAge(Age);
        }

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        
        #region "Fill Methods"

        private void FillPatientsByAge(string AgeCriteria)
        {

            RptPatientInfo_469 oRptPatientInfo_469 = new RptPatientInfo_469();
            dsgloEMRReports dsReports = new dsgloEMRReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da;
            string sQuery=null;
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " +
                                          " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST";

                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Clinic_MST");
                da.Dispose();
                da = null;
                if (AgeCriteria == "> 20")
                {
                    sQuery="where PatientAge > 20  ORDER BY PatientID";
                }
                else if (AgeCriteria == "< 20")
                {

                  
                  sQuery ="where PatientAge < 20  ORDER BY PatientID";
                }

                if (sQuery != null)
                {
                    _sqlcommand.CommandText = "SELECT PatientID, PatientCode,FirstName, MiddleName, LastName, "+
                                                    "CASE LEN(CONVERT(VARCHAR,isnull(SSN,'-'))) WHEN 0 THEN '-' ELSE SSN END AS SSN," + 
                                                    "CONVERT(VARCHAR,DOB,101)  AS DOB , " +
                                                   "PatientAge,Gender, MaritalStatus, AddressLine1, AddressLine2, "+
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(City,'-'))) WHEN 0 THEN '-' ELSE City END AS City," +
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(state,'-'))) WHEN 0 THEN '-' ELSE state END AS State," +
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(Zip,'-'))) WHEN 0 THEN '-' ELSE Zip END AS Zip," +
                                                   "isnull(Country,'-') as Country, Phone, Mobile, Email, Fax,Occupation, EmploymentStatus, " +
                                                   "PlaceofEmployment, WorkAddressline1, WorkAddressline2, WorkCity, " +
                                                   "WorkState, WorkZip, WorkPhone, WorkFax, Race,ProviderName, PharmacyName, " +
                                                   "Physician, HandDominance, Photo FROM   vwCustRptPatientInformation " + sQuery + " ";
                }
                else
                {
                    _sqlcommand.CommandText = "SELECT PatientID, PatientCode,FirstName, MiddleName, LastName, " +
                                                    "CASE LEN(CONVERT(VARCHAR,isnull(SSN,'-'))) WHEN 0 THEN '-' ELSE SSN END AS SSN," +
                                                    "CONVERT(VARCHAR,DOB,101)  AS DOB , " +
                                                   "PatientAge,Gender, MaritalStatus, AddressLine1, AddressLine2, " +
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(City,'-'))) WHEN 0 THEN '-' ELSE City END AS City," +
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(state,'-'))) WHEN 0 THEN '-' ELSE state END AS State," +
                                                   "CASE LEN(CONVERT(VARCHAR,isnull(Zip,'-'))) WHEN 0 THEN '-' ELSE Zip END AS Zip," +
                                                   "isnull(Country,'-') as Country, Phone, Mobile, Email, Fax,Occupation, EmploymentStatus, " +
                                                   "PlaceofEmployment, WorkAddressline1, WorkAddressline2, WorkCity, " +
                                                   "WorkState, WorkZip, WorkPhone, WorkFax, Race,ProviderName, PharmacyName, " +
                                                 "Physician, HandDominance, Photo FROM   vwCustRptPatientInformation ORDER BY PatientID";

                }

              
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_VWRptPatientInfo");

                da.Dispose();
                da = null;
                oRptPatientInfo_469.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloEMRReportViewer.ReportViewer = oRptPatientInfo_469;


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

        private void frmRpt_PatientAge_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ogloEMRReportViewer.onReportsClose_Clicked -= new gloEMRReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloEMRReportViewer.onGenerateReport_Clicked -= new gloEMRReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
            pnlContainer.Controls.Remove(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dispose();
        }

       

    }
}