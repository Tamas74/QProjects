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
    public partial class frmRpt_Medication : Form
    {

        #region " Declarations"

        private string _databaseconnectionstring;
        gloEMRReportViewer _ogloEMRReportViewer;

        #endregion


        #region "Constructor"

        public frmRpt_Medication(string databaseconnectionstring)
        {
            InitializeComponent();

            _ogloEMRReportViewer = new gloEMRReportViewer();

            _databaseconnectionstring = databaseconnectionstring;

            //Attaching the event Handler
            _ogloEMRReportViewer.onReportsClose_Clicked += new gloEMRReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloEMRReportViewer.onGenerateReport_Clicked += new gloEMRReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
        }

        #endregion


        #region "User Control Events"

        //Event For Generate Report
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {

            FillMedication(_ogloEMRReportViewer.sMedication);
        }

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region "Fill Methods"

        private void FillMedication(String Medication)
        {

            Rpt_MedicationReport oMedicationReport = new Rpt_MedicationReport();
            dsgloEMRReports dsReports = new dsgloEMRReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da;
            //string sQuery = null;
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " +
                                          " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId=1";

                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Clinic_MST");
                da.Dispose();
                da = null;

                if(Medication != "")
                _sqlcommand.CommandText = "SELECT * FROM  VwMedicationReport where sMedication = '" + Medication + "'";
                else
                _sqlcommand.CommandText = "SELECT * FROM  VwMedicationReport";

                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_MedicationReport");


                da.Dispose();
                da = null;
                oMedicationReport.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloEMRReportViewer.ReportViewer = oMedicationReport;


            }
            catch (Exception) // ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //ex.ToString();
                //ex = null; 

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


        #region " Form Events"

        private void frmRpt_Medication_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
          //  pnlContainer.Controls.Add(_ogloEMRReportViewer);
            panel1.Controls.Add(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dock = DockStyle.Fill;
            _ogloEMRReportViewer.Visible = true;
           _ogloEMRReportViewer.showMedication = true;

            FillMedication(_ogloEMRReportViewer.sMedication);
        }

        #endregion

        private void frmRpt_Medication_FormClosed(object sender, FormClosedEventArgs e)
        {

            _ogloEMRReportViewer.onReportsClose_Clicked -= new gloEMRReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloEMRReportViewer.onGenerateReport_Clicked -= new gloEMRReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
            panel1.Controls.Remove(_ogloEMRReportViewer);
            _ogloEMRReportViewer.Dispose();
        }

    }
}