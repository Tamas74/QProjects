using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Text;
using System.Data.SqlClient;


namespace gloReports
{
    public partial class frmRpt_ProductionByDoctor : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ProductionByDoctor objrptProductionByDoctor;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

       // private Int32 nStartdt = 0;
      //  private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();

        #endregion " Declarations "

        
        #region  " Property Procedures "

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_ProductionByDoctor(string databaseconnectionstring)
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

        private void frmRpt_ProductionByDoctor_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showTransCriteria = true;
                _ogloReportViewer.showDatesCriteria = true;
                _ogloReportViewer.showProviderCriteria = true;

                //For Selecting Charges or Allowed amount
                _ogloReportViewer.showAmountType = false;

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                FillProductionByDoctor("",0,0);
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        
        #region "Fill Methods"

        private void FillProductionByDoctor(string ProviderID, Int32 stDate, Int32 endDate)
        {

            objrptProductionByDoctor = new Rpt_ProductionByDoctor();
            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Production_By_Doctor";
                _sqlcommand.Connection = oConnection;

                if (stDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nStartDate"].Value = stDate;
                }

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nEndDate"].Value = endDate;
                }

                if (ProviderID != "")
                {
                    _sqlcommand.Parameters.Add("@ProviderID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@ProviderID"].Value = ProviderID;
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
                da.Fill(dsReports, "dt_ProductionByDoctor");
                da.Dispose();
                objrptProductionByDoctor.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptProductionByDoctor;


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
                    
                    Int32 stDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                    Int32 endDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());
                    
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
                    FillProductionByDoctor(sbProviders.ToString(),stDate, endDate);
                }
                catch (SqlException ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                
            }


        #endregion


    }
}