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
    public partial class frmRpt_ProductionAnalysisByProcedureGroup : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ProductionAnalysisByProcedureGroup objrptProductionAnalysisByProcedureGroup;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Decimal dyear;
        private Int32 nmonths = 0;
        

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
        public frmRpt_ProductionAnalysisByProcedureGroup(string databaseconnectionstring)
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

        #region " Form Events"

        private void frmRpt_ProductionAnalysisByProcedureGroup_Load(object sender, EventArgs e)
        {

            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            //For Showing the controls from the Search Criteria
            _ogloReportViewer.showMonthCriteria = true;
            _ogloReportViewer.showYearCriteria = true;

            dyear = _ogloReportViewer.sYear;
            nmonths =Convert.ToInt32(System.DateTime.Now.Month) ;

            //For Selecting Charges or Allowed amount
            //_ogloReportViewer.showAmountType = true;

            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 

            ProductionAnalysisByProcedureGroup(Convert.ToInt32(dyear), nmonths);
        }

        #endregion

        #region "Fill Methods"

        private void ProductionAnalysisByProcedureGroup(Int32 nYear, Int32 nmonths)
        {

            objrptProductionAnalysisByProcedureGroup = new Rpt_ProductionAnalysisByProcedureGroup();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Production_Analysis_By_ProcedureGroup";
                _sqlcommand.Connection = oConnection;

                if (nYear != 0)
                {
                    _sqlcommand.Parameters.Add("@nYear", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nYear"].Value = nYear;
                }

                if (nmonths != 0)
                {
                    _sqlcommand.Parameters.Add("@nMonth", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nMonth"].Value = nmonths;
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
                da.Fill(dsReports, "dt_ProductionAnalysisByProcedureGroup");
                da.Dispose();

                objrptProductionAnalysisByProcedureGroup.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptProductionAnalysisByProcedureGroup;


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
                dyear = _ogloReportViewer.sYear;
                nmonths = _ogloReportViewer.nMonth;
                ProductionAnalysisByProcedureGroup(Convert.ToInt32(dyear), nmonths);
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion
    }
}