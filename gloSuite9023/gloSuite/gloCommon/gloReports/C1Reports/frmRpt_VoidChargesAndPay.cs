using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloReports.C1Reports
{
    public partial class frmRpt_VoidChargesAndPay : Form
    {

        #region " Declarations "

        gloReportViewer _ogloReportViewer;
        Rpt_VoidClaims objrptVoidClaims;
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

       // private Int32 nStartdt = 0;
       // private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();

       // private Boolean _IsFromCalendar;
       // private DataTable _dtProviders;
       // private bool _IsPrint = false;

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_VoidChargesAndPay(string databaseconnectionstring)
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



        private void frmRpt_VoidChargesAndPay_Load(object sender, EventArgs e)
        {
            try
            {
              
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.showDatesCriteria = true;
                _ogloReportViewer.Dock = DockStyle.Fill;
                _ogloReportViewer.showExport = true;
                _ogloReportViewer.dtStartDate = FirstDayOfMonthFromDateTime(DateTime.Now.Date);
                _ogloReportViewer.dtEndDate = DateTime.Now.Date;
                FillVoidClaims();
                
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        #region "Fill Methods"

        private void FillVoidClaims()
        {
            if (objrptVoidClaims != null)
            {
                objrptVoidClaims.Dispose();
                objrptVoidClaims = null;
            }
            objrptVoidClaims = new Rpt_VoidClaims();
            dsEOBPaymentReports dsReports = new dsEOBPaymentReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
           
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_VoidClaims";
                _sqlcommand.Connection = oConnection;
               
                _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;

                _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nStartDate"].Value = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());

                _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nEndDate"].Value = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_VoidClaims");
                da.Dispose();

                objrptVoidClaims.SetDataSource(dsReports);
                _ogloReportViewer.ReportViewer = objrptVoidClaims;


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


        #region "User Control Events"

        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            try
            {

                FillVoidClaims();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
        #endregion
    }
}