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
    public partial class frmRpt_PaymentTray : Form
    {


        #region " Declarations "

        gloReportViewer _ogloReportViewer;
        Rpt_PaymentTray objrptPaymentTray;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region "Constructors"

        public frmRpt_PaymentTray(string databaseconnectionstring)
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
        
        private void frmRpt_PaymentTray_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                _ogloReportViewer.showPayTraySelection = true;
                _ogloReportViewer.showActivePayTray = true;
                _ogloReportViewer.showExport = true;

                FillPaymentTray();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
 
        #endregion


        #region "Fill Methods"

        private void FillPaymentTray()
        {
            if (objrptPaymentTray != null)
            {
                objrptPaymentTray.Dispose();
                objrptPaymentTray = null;
            }
            objrptPaymentTray = new Rpt_PaymentTray();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {


                Int64 nPayTrayID = _ogloReportViewer.nAvtivePayTray;
                int SelectIsClosed = 0;

                if (_ogloReportViewer.bActivePaymentTray)
                {
                    SelectIsClosed = 0;
                }
                else if (_ogloReportViewer.bClosedPaymentTray)
                {
                    SelectIsClosed = 1;
                }
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "BL_SELECT_PaymentClaims_Tray";
                _sqlcommand.Connection = oConnection;

                _sqlcommand.Parameters.Add("@SearchText", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@SearchText"].Value = "";

                _sqlcommand.Parameters.Add("@CloseDayTrayID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@CloseDayTrayID"].Value = nPayTrayID;

                _sqlcommand.Parameters.Add("@NoOfClaims", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@NoOfClaims"].Value = 10000;

                _sqlcommand.Parameters.Add("@NoOfClaimsApplicable", System.Data.SqlDbType.Bit);
                _sqlcommand.Parameters["@NoOfClaimsApplicable"].Value = 0;

                _sqlcommand.Parameters.Add("@ShowAll", System.Data.SqlDbType.Bit);
                _sqlcommand.Parameters["@ShowAll"].Value =1;

                _sqlcommand.Parameters.Add("@IsSearching", System.Data.SqlDbType.Bit);
                _sqlcommand.Parameters["@IsSearching"].Value = 0;

                _sqlcommand.Parameters.Add("@UserID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@UserID"].Value = 1;


                _sqlcommand.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@UserName"].Value = "admin";

                _sqlcommand.Parameters.Add("@SelectIsClosed", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters["@SelectIsClosed"].Value = SelectIsClosed;


                _sqlcommand.Parameters.Add("@ClinicID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@ClinicID"].Value = _ClinicID;
              

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_PaymentTray");
                da.Dispose();

                objrptPaymentTray.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptPaymentTray;


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
                FillPaymentTray();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion


    }
}