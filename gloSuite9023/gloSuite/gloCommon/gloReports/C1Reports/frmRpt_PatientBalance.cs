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
    public partial class frmRpt_PatientBalance : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_PatientBalance objrptPatientBalance;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();
        private StringBuilder sbCarrierID = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        
        #region "Constructor"

        public frmRpt_PatientBalance(string databaseconnectionstring)
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

        private void frmRpt_PatientBalance_Load(object sender, EventArgs e)
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
                _ogloReportViewer.showInsuranceCriteria = true;
                _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showPatientMultiselect = true;

                
                _ogloReportViewer.setdatesAsCurrentMonth();

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                FillPatientBalance();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion


        #region "Fill Methods"

        private void FillPatientBalance()
        {
            if (objrptPatientBalance != null)
            {
                objrptPatientBalance.Dispose();
                objrptPatientBalance = null;
            }
            objrptPatientBalance = new Rpt_PatientBalance();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_PatientBalance";
                _sqlcommand.Connection = oConnection;

                _sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);


                _sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@nToDate"].Value = nEnddt;

                if (sbPatientID.ToString() != "")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = sbPatientID.ToString();
                }

                if (sbProviders.ToString() != "")
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.VarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = sbProviders.ToString();
                }

                if (sbCarrierID.ToString() != "")
                {
                    _sqlcommand.Parameters.Add("@sInsuranceName", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sInsuranceName"].Value = sbCarrierID.ToString();
                }


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_PatientBalance");
                da.Dispose();
                objrptPatientBalance.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptPatientBalance;


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

                #region "Providers"

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
                #endregion

                #region "Patients"

                sbPatientID.Remove(0, sbPatientID.Length);

                Dictionary<Int64, String> dictPatient = new Dictionary<long, string>();
                dictPatient = _ogloReportViewer.dictPatients;

                List<Int64> valuesPatients = new List<Int64>(dictPatient.Keys);
                valuesPatients.Sort();

                for (int i = 0; i <= valuesPatients.Count - 1; i++)
                {
                    if (i == valuesPatients.Count - 1)
                    {
                        sbPatientID.Append(valuesPatients[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(valuesPatients[i].ToString() + ",");
                    }
                }
                #endregion

                #region "Insurance Carrier"

                sbCarrierID.Remove(0, sbCarrierID.Length);

                Dictionary<Int64, String> dictCarrierID = new Dictionary<long, string>();
                dictCarrierID = _ogloReportViewer.dictInsurance;

                List<String> valuesCarrierID = new List<String>(dictCarrierID.Values);
                valuesCarrierID.Sort();

                for (int i = 0; i <= valuesCarrierID.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbCarrierID.Append(valuesCarrierID[i].ToString());
                    }
                    else
                    {
                        sbCarrierID.Append(valuesCarrierID[i].ToString() + ",");
                    }
                }

                #endregion

                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                FillPatientBalance();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion

    }
}