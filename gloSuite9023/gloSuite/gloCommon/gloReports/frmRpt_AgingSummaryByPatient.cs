using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloReports
{
    public partial class frmRpt_AgingSummaryByPatient : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_AgingSummaryByPatient objrptAgingSummaryByPatient;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        private Int64 _ClinicID = 0;
        private Int64 PatientID;

        private StringBuilder sbPatientID = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        
        #region " Constructor "

        public frmRpt_AgingSummaryByPatient(string databaseconnectionstring, Int64 nPatientID)
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


            PatientID = nPatientID;
        }

        #endregion

       
        #region "Form Events"

        private void frmRpt_AgingSummaryByPatient_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;


                //To Hide the Multi Select Option in Patient List Control
                //_ogloReportViewer.showPatientMultiselect = false;
                _ogloReportViewer.showPatientMultiselect = true;
                _ogloReportViewer.setPatientID = PatientID;

               
                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showAgingCriteria = true;


                //For Selecting Charges or Allowed amount
                _ogloReportViewer.showAmountType = true;

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 

                FillAgingSummaryByPatient(Convert.ToString(PatientID));
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

                sbPatientID.Remove(0, sbPatientID.Length);

                Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                dictPatientID = _ogloReportViewer.dictPatients;

                List<Int64> values = new List<Int64>(dictPatientID.Keys);
                values.Sort();

                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbPatientID.Append(values[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(values[i].ToString() + ",");
                    }
                }
                //Pass selected insuarance Id's
                if (sbPatientID.ToString() != "")
                {
                    FillAgingSummaryByPatient(sbPatientID.ToString());
                }
                //If No Patients are avilable for the Criteria then display blank Recocrd
                     //If No Aging Criteria are Selected then show all Patients
                else if (_ogloReportViewer.dtAgingDate == 0)
                {
                    FillAgingSummaryByPatient("");
                }
            
                else
                {
                    FillAgingSummaryByPatient("0");
                }
                                

        }
        
        #endregion


        #region " Fill Methods"

        private void FillAgingSummaryByPatient(String sPatientID )
        {

            //Creating the object of the Report
            if (objrptAgingSummaryByPatient != null)
            {
                objrptAgingSummaryByPatient.Dispose();
                objrptAgingSummaryByPatient = null;
            }
            objrptAgingSummaryByPatient = new Rpt_AgingSummaryByPatient();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Aging_By_Patient";
                _sqlcommand.Connection = oConnection;

                if (sPatientID != "")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = sPatientID;
                }

                #region "Show Hide The Charges And Allowed Colums"

                //If Allowed Amount Pass the Parameter as 1
                if (_ogloReportViewer.bAllowed)
                {
                    _sqlcommand.Parameters.Add("@Mode", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@Mode"].Value = 1;
                }

                #endregion

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_AgingsummaryByPatient");
                da.Dispose();

                objrptAgingSummaryByPatient.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptAgingSummaryByPatient;


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

    }
}