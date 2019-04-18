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
    public partial class frmRpt_TransactionNotes : Form
    {
        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_TransactionNotes objrptTransactionNotes;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();
        private StringBuilder sbCPTCode = new StringBuilder();
        private StringBuilder sbCarrierID = new StringBuilder();
        private StringBuilder sbDxCodes = new StringBuilder();

        #endregion " Declarations "



        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructor "

        public frmRpt_TransactionNotes(string databaseconnectionstring)
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

        private void frmRpt_TransactionNotes_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showTransCriteria = true;
                _ogloReportViewer.showDatesCriteria = true;

                _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showPatientMultiselect = true;

                _ogloReportViewer.showProviderCriteria = true;

                _ogloReportViewer.setdatesAsCurrentMonth();

                _ogloReportViewer.showInsuranceCriteria = true;
                _ogloReportViewer.showCPTCriteria = true;
                _ogloReportViewer.showDiagnosisCriteria = true;

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                FillTransactionNotes("", "", "", "","");
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        #region "Fill Methods"

        private void FillTransactionNotes(string ProviderID, string PatientID, string CPTCode, string CarrierID,string DxCodes)
        {
            if (objrptTransactionNotes != null)
            {
                objrptTransactionNotes.Dispose();
                objrptTransactionNotes = null;
            }
            objrptTransactionNotes = new Rpt_TransactionNotes();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());


                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_TransactionNotes";
                _sqlcommand.Connection = oConnection;

                _sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);


                _sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@nToDate"].Value = nEnddt;

      //          if (_ClinicID != null)
                {
                    _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;
                }

                if (ProviderID != "")
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = ProviderID;
                }

                if (PatientID != "")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = PatientID;
                }

                if (CPTCode != "")
                {
                    _sqlcommand.Parameters.Add("@sCPTCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sCPTCode"].Value = CPTCode;
                }

                if (DxCodes  != "")
                {
                    _sqlcommand.Parameters.Add("@DxCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@DxCode"].Value = DxCodes;
                }

                if (CarrierID != "")
                {
                    _sqlcommand.Parameters.Add("@sInsuranceName", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sInsuranceName"].Value = CarrierID;
                }

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_TransactionNotes");
                da.Dispose();
                objrptTransactionNotes.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptTransactionNotes;


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


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                dictPatientID = _ogloReportViewer.dictPatients;

                List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                lstPatvalues.Sort();

                for (int i = 0; i <= lstPatvalues.Count - 1; i++)
                {
                    if (i == lstPatvalues.Count - 1)
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString() + ",");
                    }
                }

                #endregion

                #region "CPT Code"

                sbCPTCode.Remove(0, sbCPTCode.Length);

                Dictionary<Int64, String> dictCPTCode = new Dictionary<long, string>();
                dictCPTCode = _ogloReportViewer.dictCPT;

                List<String> valuesCPT = new List<String>(dictCPTCode.Values);
                valuesCPT.Sort();

                for (int i = 0; i <= valuesCPT.Count - 1; i++)
                {
                    if (i == valuesCPT.Count - 1)
                    {
                        sbCPTCode.Append(valuesCPT[i].ToString());
                    }
                    else
                    {
                        sbCPTCode.Append(valuesCPT[i].ToString() + ",");
                    }
                }

                #endregion

                #region "Insurance Carrier"

                sbCarrierID.Remove(0, sbCarrierID.Length);

                Dictionary<Int64, String> dictCarrierID = new Dictionary<long, string>();
                dictCarrierID = _ogloReportViewer.dictInsurance;

                List<Int64> valuesCarrierID = new List<Int64>(dictCarrierID.Keys);
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

                #region "Diagnosis"

                sbDxCodes.Remove(0, sbDxCodes.Length);

                Dictionary<Int64, String> dictDiagnosis = new Dictionary<long, string>();
                dictDiagnosis = _ogloReportViewer.dictDgCode;

                List<String> lstDiagnosis = new List<String>(dictDiagnosis.Values);
                lstDiagnosis.Sort();

                for (int i = 0; i <= lstDiagnosis.Count - 1; i++)
                {
                    if (i == lstDiagnosis.Count - 1)
                    {
                        sbDxCodes.Append(lstDiagnosis[i].ToString());
                    }
                    else
                    {
                        sbDxCodes.Append(lstDiagnosis[i].ToString() + ",");
                    }
                }
                #endregion

              

                FillTransactionNotes(sbProviders.ToString(), sbPatientID.ToString(), sbCPTCode.ToString(), sbCarrierID.ToString(),sbDxCodes.ToString());

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion
    }
}