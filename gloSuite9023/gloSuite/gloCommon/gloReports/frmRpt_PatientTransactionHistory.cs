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
    public partial class frmRpt_PatientTransactionHistory : Form
    {
        
        #region " Declarations "
            
            //For Creating the object of the User Control
            gloReportViewer _ogloReportViewer;

            //For Creating the Object of the Report
            Rpt_PatientTransactionHistory objrptPatientTransHis;

            private Int64 PatientID;
            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;

            private StringBuilder sbCPTCode = new StringBuilder();
            private StringBuilder sbDiagnosis = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

        #endregion  " Property Procedures "


        #region " Constructor "

            public frmRpt_PatientTransactionHistory(string databaseconnectionstring,Int64 nPatientID)
            {
                InitializeComponent();

                _ogloReportViewer = new gloReportViewer();
                
                //Attaching the event Handler
                _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
                _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);


                PatientID = nPatientID;
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


        #region"Form Events"

        private void frmRpt_PatientTransactionHistory_Load(object sender, EventArgs e)
        {

            //For Hiding the controls from the Search Criteria
            _ogloReportViewer.showPatientCriteria = true;
            _ogloReportViewer.showFacilityCriteria = true;
            _ogloReportViewer.showTransCriteria = true;
            _ogloReportViewer.showDatesCriteria = true;

            _ogloReportViewer.setdatesAsCurrentMonth();

            //Added By Pramod Nair On  20090813
            _ogloReportViewer.showCPTCriteria = true;
            _ogloReportViewer.showCPTMultiselect = true;
            _ogloReportViewer.showDiagnosisCriteria = true;


            //To Hide the Multi Select Option in Patient List Control
            _ogloReportViewer.showPatientMultiselect = false;
            _ogloReportViewer.setPatientID = PatientID;

            _ogloReportViewer.showAmountType = true;
            _ogloReportViewer.showBothChrgAllowed = true;

            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            //To Fill The Reports
            FillTransHistoryReport(PatientID,"",0,0,"","");

        }


        #endregion


        #region " User Control Events"

        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            String sFacilityCode = _ogloReportViewer.sFacilityCode;
            
            Int32 stDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
            Int32 endDate = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());
            
            if (_ogloReportViewer.nPatientID != 0)
                PatientID =_ogloReportViewer.nPatientID;


            //Added By Pramod Nair On  20090813
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

            #region "Diagnosis"

            sbDiagnosis.Remove(0, sbDiagnosis.Length);

            Dictionary<Int64, String> dictDiagnosis = new Dictionary<long, string>();
            dictDiagnosis = _ogloReportViewer.dictDgCode;

            List<String> lstDiagnosis = new List<String>(dictDiagnosis.Values);
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

            FillTransHistoryReport(PatientID, sFacilityCode, stDate, endDate, sbCPTCode.ToString(), sbDiagnosis.ToString());
        }


        #endregion


        #region "Fill Methods"

        private void FillTransHistoryReport(Int64 nPatientID, String sFacilityCode, Int32 stDate, Int32 endDate, String CPTCode,String DxCode)
        {
            objrptPatientTransHis = new Rpt_PatientTransactionHistory();
            dsReports dsReports = new dsReports();

            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Patient_TransactionHistory";
                _sqlcommand.Connection = oConnection;

                if (nPatientID != 0)
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = nPatientID.ToString();
                }

                if (sFacilityCode != "")
                {
                    _sqlcommand.Parameters.Add("@sFacilityCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sFacilityCode"].Value = sFacilityCode;
                }

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

                if (CPTCode != "")
                {
                    _sqlcommand.Parameters.Add("@sCPTCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sCPTCode"].Value = CPTCode;
                }

                if (DxCode != "")
                {
                    _sqlcommand.Parameters.Add("@DxCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@DxCode"].Value = DxCode;
                }

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_PatientTransactionHistory");
                da.Dispose();

                objrptPatientTransHis.SetDataSource(dsReports);
                
                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptPatientTransHis;

                #region "Show Hide The Charges And Allowed Colums"

                if (_ogloReportViewer.bCharges)
                {
                    objrptPatientTransHis.SetParameterValue("Charges", true);
                    objrptPatientTransHis.SetParameterValue("Allowed", false);
                }
                else if (_ogloReportViewer.bAllowed)
                {
                    objrptPatientTransHis.SetParameterValue("Allowed", true);
                    objrptPatientTransHis.SetParameterValue("Charges", false);
                }
                else
                {
                    objrptPatientTransHis.SetParameterValue("Charges", true);
                    objrptPatientTransHis.SetParameterValue("Allowed", true);
                }

                #endregion
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


    }
}