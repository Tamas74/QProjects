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
    public partial class frmRpt_ChargesSummary : Form
    {

        #region " Declarations "

        gloReportViewer _ogloReportViewer;
        Rpt_ChargesSummary objrptChargesSummary;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private StringBuilder sbProviders = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region "Constructors"

        public frmRpt_ChargesSummary(string databaseconnectionstring)
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

        private void frmRpt_ChargesSummary_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showTransCriteria = true;
                _ogloReportViewer.showDatesCriteria = true;
                _ogloReportViewer.setdatesAsCurrentMonth();

                _ogloReportViewer.showProviderCriteria = true;
                _ogloReportViewer.showMultiFacility = true;
                _ogloReportViewer.showMultiChargesTray = true;
                _ogloReportViewer.showUser = true;

                _ogloReportViewer.showLoginUsers = true;
                _ogloReportViewer.showRunReportAs = true;

                _ogloReportViewer.showExport = true;
                _ogloReportViewer.showSealCharges = true;

                FillChargesTray();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion


        #region "Fill Methods"

        private void FillChargesTray()
        {
            if (objrptChargesSummary != null)
            {
                objrptChargesSummary.Dispose();
                objrptChargesSummary = null;
            }
            objrptChargesSummary = new Rpt_ChargesSummary();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;

                if (sbProviders.ToString() != "")
                {
                    _sqlcommand.Parameters.Add("@ProviderID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@ProviderID"].Value = sbProviders.ToString();
                }

                _sqlcommand.CommandText = "Rpt_ChargesTray";
                _sqlcommand.Connection = oConnection;


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_ChargesTray");
                da.Dispose();

                objrptChargesSummary.SetDataSource(dsReports);

                _ogloReportViewer.ReportViewer = objrptChargesSummary;


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

                FillChargesTray();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion
    }
}