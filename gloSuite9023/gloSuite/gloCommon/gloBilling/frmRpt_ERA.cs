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
    public partial class frmRpt_ERA : Form
    {

        #region " Declarations "

        //For Creating the Object of the CrystalReport
        rptERAReport objRptERAReport;
        

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
    
        
        #endregion


        #region  " Property Procedures "


        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_ERA(string databaseconnectionstring)
        {
            InitializeComponent();
           
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


        private void FillERAData()
        {
            //Creating the object of the Report
            if (!(object.ReferenceEquals(objRptERAReport, null)))
            {
                objRptERAReport.Close();
                objRptERAReport.Dispose();
                objRptERAReport = null;
            }

            objRptERAReport = new rptERAReport();

            dsERAReports dsERAReport = new dsERAReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;

                _sqlcommand.Connection = oConnection;
                
               

               _sqlcommand.CommandType = CommandType.StoredProcedure;
               _sqlcommand.CommandText = "ERA_EOB_Report";
             
               da.SelectCommand = _sqlcommand;
                da.Fill(dsERAReport, "dt_ERA");
              
               
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
            }
        }


        #endregion

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

        private void frmRpt_ERA_Load(object sender, EventArgs e)
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

    }
}