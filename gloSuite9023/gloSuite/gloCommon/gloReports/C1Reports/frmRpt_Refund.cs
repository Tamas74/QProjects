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
    public partial class frmRpt_Refund : Form
    {
        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_Refund objrptRefund;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbPatient = new StringBuilder();

        private StringBuilder sbProviders = new StringBuilder();

        private StringBuilder sbDiagnosis = new StringBuilder();

        private StringBuilder sbCPTCode = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructors"

        public frmRpt_Refund(string databaseconnectionstring)
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

        private void frmRpt_Refund_Load(object sender, EventArgs e)
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
                _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showPatientMultiselect = true;
                _ogloReportViewer.showCPTCriteria = true;

                _ogloReportViewer.showDiagnosisCriteria = true;

                _ogloReportViewer.setdatesAsCurrentMonth();

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                FillRefunds(null,null,null,null);
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #region "Fill Methods"


        
        private void FillRefunds(String sPatient,String sProviders,String sDiagnosis,String sCPTCode)
        {
            if (objrptRefund != null)
            {
                objrptRefund.Dispose();
                objrptRefund = null;
            }
            objrptRefund = new Rpt_Refund();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Refund";
                _sqlcommand.Connection = oConnection;

                _sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);
             

                _sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@nToDate"].Value = nEnddt;

                if (sPatient != null && sPatient !="")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = sPatient;
                }

                if (sProviders != null && sProviders != "")
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = sProviders;
                }

                if (sDiagnosis != null && sDiagnosis != "")
                {
                    _sqlcommand.Parameters.Add("@sDiagnosisCodes", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sDiagnosisCodes"].Value = sDiagnosis;
                }

                if (sCPTCode != null && sCPTCode != "")
                {
                    _sqlcommand.Parameters.Add("@sCPTCode", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@sCPTCode"].Value = sCPTCode;
                }


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Refund");
                da.Dispose();
                objrptRefund.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptRefund;


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

                #region "Patient"

                sbPatient.Remove(0, sbPatient.Length);

                Dictionary<Int64, String> dictPatient = new Dictionary<long, string>();
                dictPatient = _ogloReportViewer.dictPatients;

                List<Int64> lstPatient = new List<Int64>(dictPatient.Keys);
                lstPatient.Sort();

                for (int i = 0; i <= lstPatient.Count - 1; i++)
                {
                    if (i == lstPatient.Count - 1)
                    {
                        sbPatient.Append(lstPatient[i].ToString());
                    }
                    else
                    {
                        sbPatient.Append(lstPatient[i].ToString() + ",");
                    }
                }
                #endregion

                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);

                Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                dictProviders = _ogloReportViewer.dictProviders;

                List<Int64> lstProviders = new List<Int64>(dictProviders.Keys);
                lstProviders.Sort();

                for (int i = 0; i <= lstProviders.Count - 1; i++)
                {
                    if (i == lstProviders.Count - 1)
                    {
                        sbProviders.Append(lstProviders[i].ToString());
                    }
                    else
                    {
                        sbProviders.Append(lstProviders[i].ToString() + ",");
                    }
                }
                #endregion

                #region "Diagnosis"

                sbDiagnosis.Remove(0, sbDiagnosis.Length);

                Dictionary<Int64, String> dictDiagnosis = new Dictionary<long, string>();
                dictDiagnosis = _ogloReportViewer.dictDgCode;

                List<Int64> lstDiagnosis = new List<Int64>(dictDiagnosis.Keys);
                lstDiagnosis.Sort();

                for (int i = 0; i <= lstDiagnosis.Count - 1; i++)
                {
                    if (i == lstDiagnosis.Count - 1)
                    {
                        sbDiagnosis.Append(lstDiagnosis[i].ToString());
                    }
                    else
                    {
                        sbDiagnosis.Append(lstDiagnosis[i].ToString() + ",");
                    }
                }
                #endregion

                #region "CPTCode"

                sbCPTCode.Remove(0, sbCPTCode.Length);

                Dictionary<Int64, String> dictCPTCode = new Dictionary<long, string>();
                dictCPTCode = _ogloReportViewer.dictCPT;

                List<String> values = new List<String>(dictCPTCode.Values);
                values.Sort();

                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbCPTCode.Append(values[i].ToString());
                    }
                    else
                    {
                        sbCPTCode.Append(values[i].ToString() + ",");
                    }
                }

                #endregion

                FillRefunds(sbPatient.ToString(), sbProviders.ToString(), sbDiagnosis.ToString(), sbCPTCode.ToString());

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion

    }
}