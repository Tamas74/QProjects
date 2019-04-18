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
    public partial class frmRpt_ReimbursementByCPTByCarrier : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_ReimbursementByCPTByInsurance objrptReimbursementByCPTByInsurance;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        private StringBuilder sbCarrierID = new StringBuilder();
        private StringBuilder sbCPTCode = new StringBuilder();

        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_ReimbursementByCPTByCarrier(string databaseconnectionstring)
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

        private void frmRpt_ReimbursementByCPTByCarrier_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            //For Showing the controls in the Search Criteria
            _ogloReportViewer.showCPTCriteria = true;
            _ogloReportViewer.showInsuranceCriteria = true;

            //For Selecting Charges or Allowed amount
            _ogloReportViewer.showAmountType = true;
 
            //Property to show the Export Button on Tool Bar
            _ogloReportViewer.showExport = true;
 

            FillReimbursementByCPTByCarrier("", "");

        }

        #endregion

          #region "User Control Events"

        //Event For Generate Report on Click
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            try
            {
                sbCarrierID.Remove(0, sbCarrierID.Length);

                Dictionary<Int64, String> dictCarrierID = new Dictionary<long, string>();
                dictCarrierID = _ogloReportViewer.dictInsurance;

                List<Int64> values = new List<Int64>(dictCarrierID.Keys);
                values.Sort();

                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbCarrierID.Append(values[i].ToString());
                    }
                    else
                    {
                        sbCarrierID.Append(values[i].ToString() + ",");
                    }
                }

                sbCPTCode.Remove(0, sbCPTCode.Length);

                Dictionary<Int64, String> dictCPTCode = new Dictionary<long, string>();
                dictCPTCode = _ogloReportViewer.dictCPT;

                List<String> CPTvalues = new List<String>(dictCPTCode.Values);
                CPTvalues.Sort();

                for (int i = 0; i <= CPTvalues.Count - 1; i++)
                {
                    if (i == CPTvalues.Count - 1)
                    {
                        sbCPTCode.Append(CPTvalues[i].ToString());
                    }
                    else
                    {
                        sbCPTCode.Append(CPTvalues[i].ToString() + ",");
                    }
                }


                FillReimbursementByCPTByCarrier(sbCarrierID.ToString(), sbCPTCode.ToString());
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region " Fill Methods "

        private void FillReimbursementByCPTByCarrier(String sContactID,String sCPTCode)
        {
            //Creating the object of the Report
            objrptReimbursementByCPTByInsurance = new Rpt_ReimbursementByCPTByInsurance();

            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Reimbursement_By_CPTCode_By_InsuranceCarrier";
                _sqlcommand.Connection = oConnection;

                if (sContactID != "")
                {
                    _sqlcommand.Parameters.Add("@nContactID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nContactID"].Value = sContactID;
                }

                if (sCPTCode != "")
                {
                    _sqlcommand.Parameters.Add("@sCPTCode", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@sCPTCode"].Value = sCPTCode ;
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
                da.Fill(dsReports, "dt_ReimbursementByCPTByInsuranceCarrier");
                da.Dispose();

                objrptReimbursementByCPTByInsurance.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptReimbursementByCPTByInsurance;


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