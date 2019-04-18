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
    public partial class frmRpt_ProductionTrendsByProcedureGroup : Form
    {

        #region " Declarations "

        //For Creating the object of the Report Viewer User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ProductionTrendsByProcedureGroup objrptProductionByMonth;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Decimal dyear;
        private Int32 nmonths = 0;

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        public frmRpt_ProductionTrendsByProcedureGroup(string databaseconnectionstring)
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


        private void frmRpt_ProductionTrendsByProcedureGroup_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            //For Hiding the controls from the Search Criteria
            _ogloReportViewer.showMonthCriteria = true;
            _ogloReportViewer.showYearCriteria = true;

            dyear = _ogloReportViewer.sYear;
            nmonths = Convert.ToInt32(System.DateTime.Now.Month);

            //For Selecting Charges or Allowed amount
            _ogloReportViewer.showAmountType = true;

            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 
            FillProductionTrendsByProcedureGroup(Convert.ToInt32(dyear), nmonths);
        }


        #region "User Control Events"

        //Event For Closing the Form
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            dyear = _ogloReportViewer.sYear;
            nmonths = _ogloReportViewer.nMonth;
            FillProductionTrendsByProcedureGroup(Convert.ToInt32(dyear), nmonths);
        }

        //Event Fot Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }






        #endregion


        #region "Fill Methods"


        /// <summary>
        /// For fetching the FillProductionTrendsByProcedureGroup and binding it to the report
        /// </summary>
        /// <param name="nYear"></param>
        /// <param name="nYear"></param>
        private void FillProductionTrendsByProcedureGroup(Int32 nYear, Int32 nmonths)
        {

            //Creating the object of the Report
            objrptProductionByMonth = new Rpt_ProductionTrendsByProcedureGroup();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_ProductionTrends_By_ProcedureGroup";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.Parameters.Add("@Year", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@Year"].Value = nYear;

                if (nmonths != 0)
                {
                    _sqlcommand.Parameters.Add("@Month", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@Month"].Value = nmonths;
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
                da.Fill(dsReports, "dt_ProductionTrendsByProcedureGroup");
                da.Dispose();

                objrptProductionByMonth.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptProductionByMonth;


            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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