using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace gloReports
{
    public partial class frmRpt_ProductionByMonth : Form
    {

       #region " Declarations "

            //For Creating the object of the Report Viewer User Control
            gloReportViewer _ogloReportViewer;

            //For Creating the Object of the CrystalReport
            Rpt_ProductionByMonth objrptProductionByMonth;


            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;
            
            private Decimal dyear;
            private Int32 nmonths=0;
        
        #endregion " Declarations "


       #region  " Property Procedures "

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

        #endregion  " Property Procedures "


       #region " Constructor "

            public frmRpt_ProductionByMonth(string databaseconnectionstring)
            {
                InitializeComponent();

                _ogloReportViewer = new gloReportViewer();

                //Attaching the event Handler
                _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
                _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);
                //_ogloReportViewer.onReportMonth_SelectedIndexChanged += new gloReportViewer.onReportMonthSelectedIndexChanged(ogloReports_onMonth_SelectedIndexChanged);
                //_ogloReportViewer.onReportYear_ValueChanged += new gloReportViewer.onReportYearValueChanged(ogloReports_onYear_ValueChanged);

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

            private void frmRpt_ProductionByMonth_Load(object sender, EventArgs e)
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showMonthCriteria = true;
               _ogloReportViewer.showYearCriteria=true;

                dyear = _ogloReportViewer.sYear;
                nmonths = 0;

                //For Selecting Charges or Allowed amount
                _ogloReportViewer.showAmountType = false ;
 
                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 

                FillProductionByMonth(Convert.ToInt32(dyear), nmonths);

            }

        #endregion

        
       #region "User Control Events"

           //Event For Closing the Form
           private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
            {
                dyear = _ogloReportViewer.sYear;
                nmonths=_ogloReportViewer.nMonth;
                FillProductionByMonth(Convert.ToInt32(dyear), nmonths);
            }

           //Event Fot Closing the Form
           private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
           {
                this.Close();
           } 


           ////Event For  on Month Selected Index Changed
           // private void ogloReports_onMonth_SelectedIndexChanged(object sender, EventArgs e)
           // {
           //     dyear = _ogloReportViewer.sYear;
           //     nmonths = _ogloReportViewer.nMonth;
           //     FillProductionByMonth(Convert.ToInt32(dyear), nmonths);
           // }

           ////Event For Year Value Changed 
           //private void ogloReports_onYear_ValueChanged(object sender, EventArgs e)
           // {
           //     dyear = _ogloReportViewer.sYear;
           //     nmonths = _ogloReportViewer.nMonth;
           //     FillProductionByMonth(Convert.ToInt32(dyear), nmonths);
           // }

           

            

       #endregion

       
       #region "Fill Methods"


        /// <summary>
        /// For fetching the Production By Month and binding it to the report
        /// </summary>
        /// <param name="nYear"></param>
        /// <param name="nYear"></param>
        private void FillProductionByMonth(Int32 nYear, Int32 nmonths)
        {

            //Creating the object of the Report
            objrptProductionByMonth = new Rpt_ProductionByMonth();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Production_By_Month";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.Parameters.Add("@nYear", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@nYear"].Value = nYear;

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
                da.Fill(dsReports, "dt_ProductionByMonth");
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