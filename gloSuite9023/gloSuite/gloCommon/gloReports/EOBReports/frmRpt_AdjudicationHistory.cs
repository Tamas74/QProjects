using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;

namespace gloReports
{
    public partial class frmRpt_AdjudicationHistory : Form
    {

        #region " Declarations "

        //For Creating the Object of the CrystalReport
        Rpt_AdjudicationHistory objAdjudicationHistory;


        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _nPatientID;
        private Int64 _nTransactionID;
        
        #endregion


        #region  " Property Procedures "


        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_AdjudicationHistory(string databaseconnectionstring, Int64 nPatientID, Int64 nTransactionID)
        {
            InitializeComponent();
            _nPatientID = nPatientID;
            _nTransactionID = nTransactionID;
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


        #region "  Form Events  "

        private void frmRpt_AdjudicationHistory_Load_1(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(crvReportViewer);
                crvReportViewer.Dock = DockStyle.Fill;
                string CloseDate = getCloseDate();
                CloseDate = getCloseDate();
                //Property to show the Export Button on Tool Bar
                crvReportViewer.ShowExportButton= false;

                FillAdjudicationHistory(_nPatientID, CloseDate);

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion


        #region "User Control sEvents"

        //Event For Generate Report on Click
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
           // FillAdjudicationHistory();
        }


        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region " Fill Methods "


        private void FillAdjudicationHistory(Int64 nPatientID, string CloseDate)
        {
            //Creating the object of the Report
            objAdjudicationHistory = new Rpt_AdjudicationHistory();
            Rpt_AdjClaimSubReport oAdjClaimSubReport = new Rpt_AdjClaimSubReport();
            dsAdjudicationHistory dsAdjudicationHistory = new dsAdjudicationHistory();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            int _CloseDate = gloDateMaster.gloDate.DateAsNumber(CloseDate);
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
               //_sqlcommand.CommandText = "SELECT  BL_EOBPayment_EOB.nEOBPaymentID, "+
               //                             "		BL_EOBPayment_EOB.nBillingTransactionDetailID, "+ 
               //                             "		BL_EOBPayment_EOB.nBillingTransactionID,  "+
               //                             "		dbo.CONVERT_TO_DATE(BL_EOBPayment_EOB.nDOSFrom) AS nDOSFrom,   "+
               //                             "		Contacts_MST.sName,  "+
               //                             "		BL_EOBPayment_EOB.sCPTCode,  "+
               //                             "		BL_EOBPayment_EOB.sCPTDescription,  "+
               //                             "		BL_EOBPayment_EOB.dTotalCharges, "+
               //                             "		SUM(BL_EOBPayment_EOB.dAllowed) AS dAllowed, "+
               //                             "		SUM(BL_EOBPayment_EOB.dPayment) AS dPayment,  "+
               //                             "		BL_EOBPayment_EOB.dWriteOff,  "+
               //                             "		BL_EOBPayment_EOB.dCopay,  "+
               //                             "		BL_EOBPayment_EOB.dDeductible,  "+
               //                             "		BL_EOBPayment_EOB.dCoInsurance,  "+
               //                             "		BL_EOBPayment_EOB.dWithhold,  "+
               //                             "		BL_EOBPayment_EOB.nCloseDate, "+
               //                             "		BL_EOBPayment_EOB.nClaimNo "+
               //                             "FROM BL_EOBPayment_EOB  "+
               //                             "		INNER JOIN "+
               //                             "	Contacts_MST ON BL_EOBPayment_EOB.nContactID = Contacts_MST.nContactID "+
               //                             "WHERE   (BL_EOBPayment_EOB.nBillingTransactionID = 40251610772374101) " +
               //                             "GROUP BY BL_EOBPayment_EOB.nDOSFrom, Contacts_MST.sName, BL_EOBPayment_EOB.nDOSFrom, BL_EOBPayment_EOB.sCPTCode,  "+
               //                             "		BL_EOBPayment_EOB.sCPTDescription, BL_EOBPayment_EOB.dTotalCharges, BL_EOBPayment_EOB.dWriteOff, BL_EOBPayment_EOB.dCopay,  "+
               //                             "	BL_EOBPayment_EOB.dDeductible, BL_EOBPayment_EOB.dCoInsurance, BL_EOBPayment_EOB.dWithhold, BL_EOBPayment_EOB.nUserID,  "+
               //                             "		BL_EOBPayment_EOB.sUserName, BL_EOBPayment_EOB.nBillingTransactionDetailID, BL_EOBPayment_EOB.nEOBPaymentID,  "+
               //                             "		BL_EOBPayment_EOB.dPayment, BL_EOBPayment_EOB.nBillingTransactionID, BL_EOBPayment_EOB.nCloseDate,BL_EOBPayment_EOB.nClaimNo "+
               //                             "ORDER BY BL_EOBPayment_EOB.nBillingTransactionDetailID ";


               // _sqlcommand.Connection = oConnection;
               // oConnection.Open();
               // da = new SqlDataAdapter(_sqlcommand);
               // da.Fill(dsAdjudicationHistory, "dt_Remittance");
                _sqlcommand.CommandText = "SELECT SUM(dPayment) AS Paid,SUM(dAllowed) AS Approved "+
                                          " FROM BL_EOBPayment_EOB WHERE nTrackTrnID = " +_nTransactionID ;
                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsAdjudicationHistory, "dt_PaymentInfo");

               _sqlcommand.CommandType = CommandType.StoredProcedure;
               _sqlcommand.CommandText = "Rpt_AdjudicationHistory";
               
                SqlParameter ParaTransactionID = new SqlParameter();
                {
                    ParaTransactionID.ParameterName = "@nTransactionID";
                    ParaTransactionID.Value = _nTransactionID;
                    ParaTransactionID.Direction = ParameterDirection.Input;
                    ParaTransactionID.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaTransactionID);
                SqlParameter ParaClinicID = new SqlParameter();
                {
                    ParaClinicID.ParameterName = "@nClinicID";
                    ParaClinicID.Value = _ClinicID;
                    ParaClinicID.Direction = ParameterDirection.Input;
                    ParaClinicID.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaClinicID);
                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsAdjudicationHistory, "dt_Charges_Payment");

                objAdjudicationHistory.SetDataSource(dsAdjudicationHistory);
                oAdjClaimSubReport.SetDataSource(dsAdjudicationHistory);
                //Binds the Report to the Report viewer
                crvReportViewer.ReportSource = objAdjudicationHistory;
                
 
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
                    _sqlcommand.Dispose();
                }
            }
        }


        #endregion

        private string getCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("select dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
            if (_Result.ToString() != "")
            {
                return _Result.ToString();
            }
            else
            {

                return "";

            }
        }

        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            crvReportViewer.PrintReport();
        }

    }
}