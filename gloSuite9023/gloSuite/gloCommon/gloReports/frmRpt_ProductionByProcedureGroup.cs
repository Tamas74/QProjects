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
    public partial class frmRpt_ProductionByProcedureGroup : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ProductionByProcedureGroup objrptProductionByProcedureGroup;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private StringBuilder sbCPTCode = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region "Constructors"

        public frmRpt_ProductionByProcedureGroup(string databaseconnectionstring)
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

        private void frmRpt_ProductionByProcedureGroup_Load(object sender, EventArgs e)
        {

            //For Selecting Charges or Allowed amount
            _ogloReportViewer.showAmountType = false;

            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            _ogloReportViewer.showTransCriteria = true;
            _ogloReportViewer.showDatesCriteria = true;

            _ogloReportViewer.setdatesAsCurrentMonth();

            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 
            FillProductionByProcedureGroup();

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
            //sbCPTCode.Remove(0, sbCPTCode.Length);

            //Dictionary<Int64, String> dictCPTCode = new Dictionary<long, string>();
            //dictCPTCode = _ogloReportViewer.dictCPT;

            //List<String> values = new List<String>(dictCPTCode.Values);
            //values.Sort();

            //for (int i = 0; i <= values.Count - 1; i++)
            //{
            //    if (i == values.Count - 1)
            //    {
            //        sbCPTCode.Append(values[i].ToString());
            //    }
            //    else
            //    {
            //        sbCPTCode.Append(values[i].ToString() + ",");
            //    }
            //}

            FillProductionByProcedureGroup();
        }


        #endregion


        #region "Fill Methods"

        private void FillProductionByProcedureGroup()
        {

            //Creating the object of the Report
            objrptProductionByProcedureGroup = new Rpt_ProductionByProcedureGroup();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                Int32 stDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                Int32 endDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Production_By_ProcedureGroup";
                _sqlcommand.Connection = oConnection;

                //if (sCPTCode != "")
                //{
                //    _sqlcommand.Parameters.Add("@sCPTCode", System.Data.SqlDbType.NVarChar);
                //    _sqlcommand.Parameters["@sCPTCode"].Value = sCPTCode;
                //}

                #region "Show Hide The Charges And Allowed Colums"

                //If Allowed Amount Pass the Parameter as 1
                if (_ogloReportViewer.bAllowed)
                {
                    _sqlcommand.Parameters.Add("@Mode", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@Mode"].Value = 1;

                 
                }

                #endregion

                _sqlcommand.Parameters.Add("@startdate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@startdate"].Value = stDate;

                _sqlcommand.Parameters.Add("@enddate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@enddate"].Value = endDate;

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_ProductionByProcedureGroup");
                da.Dispose();

                objrptProductionByProcedureGroup.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptProductionByProcedureGroup;


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