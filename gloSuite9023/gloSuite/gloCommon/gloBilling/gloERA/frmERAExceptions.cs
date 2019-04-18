using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;

namespace gloBilling
{
    public partial class frmERAExceptions : Form
    {

        #region " Declarations "

        //For Creating the Object of the CrystalReport
        gloERA.rptERAExceptions objRptERAReport;
        

        private string _DataBaseConnectionString = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;

        private Int64 _BPRID = 0;
        private Int64 _FileID = 0;

        #endregion

        #region " Constructor "

        public frmERAExceptions(Int64  nBPRID , Int64 nERAFileID, Int64 nUserID)
        {

            #region " Get DatabaseConnectionString from AppSettings "
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            #endregion

            #region " Get MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBoxCaption"] != "")
                { _MessageBoxCaption = Convert.ToString(appSettings["MessageBoxCaption"]); }
                else
                    _MessageBoxCaption = "gloPM";
            }
            else
                _MessageBoxCaption = "gloPM";
            #endregion

            #region " Get ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion

            InitializeComponent();

            _BPRID = nBPRID;
            _FileID = nERAFileID;
            _UserID = nUserID;

        }

        #endregion

        #region " Form Events "

        private void frmERAExceptions_Load(object sender, EventArgs e)
        {
            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(crvReportViewer);
                crvReportViewer.Dock = DockStyle.Fill;
                //Property to show the Export Button on Tool Bar
                crvReportViewer.ShowExportButton = false;

                FillERAData();

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Tool Strip Events "

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

            try
            {
                crvReportViewer.PrintReport();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        #endregion

        #region " User Control Events "

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


        private void FillERAData()
        {
            //Creating the object of the Report
            if (!(object.ReferenceEquals(objRptERAReport, null)))
            {
                objRptERAReport.Close();
                objRptERAReport.Dispose();
                objRptERAReport = null;
            }

            objRptERAReport = new gloERA.rptERAExceptions();
            gloERA.dsRPTExceptions dsERAReport = new gloERA.dsRPTExceptions();
                        
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Cursor = Cursors.WaitCursor;
                oConnection.ConnectionString = _DataBaseConnectionString;

                oConnection.Open();
                _sqlcommand.Connection = oConnection;

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "ERA_Exceptions_Report";
                _sqlcommand.CommandTimeout = 5000;
                _sqlcommand.Parameters.Add("@nBPRID", SqlDbType.BigInt).Value = _BPRID;
                _sqlcommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = _UserID;

                da.SelectCommand = _sqlcommand;
                da.Fill(dsERAReport, "dt_Exceptions");


                objRptERAReport.SetDataSource(dsERAReport);

                //Binds the Report to the Report viewer
                crvReportViewer.ReportSource = objRptERAReport;
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
                Cursor = Cursors.Default;
            }
        }


        #endregion

        

    }
}