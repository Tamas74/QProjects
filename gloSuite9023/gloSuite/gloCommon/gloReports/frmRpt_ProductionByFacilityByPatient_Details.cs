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
    public partial class frmRpt_ProductionByFacilityByPatient_Details : Form
    {


        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ProductionByFacilityByPatientDetails objrptProductionByFacilityByPatient;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

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

        public frmRpt_ProductionByFacilityByPatient_Details(string databaseconnectionstring)
        {
            try
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion


        #region "Form Events"

        private void frmRpt_ProductionByFacilityByPatient_Details_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;


            //For Hiding the controls from the Search Criteria
            _ogloReportViewer.showFacilityCriteria = true;
            _ogloReportViewer.showPatientCriteria = true;

            _ogloReportViewer.showPatientMultiselect = true;

            //For Selecting Charges or Allowed amount
            _ogloReportViewer.showAmountType = false;

            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 
            //To fill The Reports
            FillProductionByFacilityByPatientDetails("", "");

        }
        #endregion


        #region "User Control Events"

        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            try
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

                FillProductionByFacilityByPatientDetails(_ogloReportViewer.sFacilityCode, sbPatientID.ToString());
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion


        #region "Fill Methods"

        private void FillProductionByFacilityByPatientDetails(String sFacilityCode, String sPatientID)
        {

            objrptProductionByFacilityByPatient = new Rpt_ProductionByFacilityByPatientDetails();
            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Production_By_Facility_By PatientDetails";
                _sqlcommand.Connection = oConnection;

                if (sFacilityCode != "")
                {
                    _sqlcommand.Parameters.Add("@FacilityCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@FacilityCode"].Value = sFacilityCode;
                }

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
                da.Fill(dsReports, "dt_ProductionByFacilityByPatientDetails");
                da.Dispose();
                objrptProductionByFacilityByPatient.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptProductionByFacilityByPatient;
            }
            catch (SqlException ex)
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
                    oConnection.Dispose();

                }
            }
        }

        #endregion

      
    }
}