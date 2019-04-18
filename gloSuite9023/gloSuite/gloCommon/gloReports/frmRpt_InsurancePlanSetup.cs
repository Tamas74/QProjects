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
    public partial class frmRpt_InsurancePlanSetup : Form
    {
             #region " Declarations "

            //For Creating the object of the Report Viewer User Control
            gloReportViewer _ogloReportViewer;
            //For Creating the Object of the CrystalReport
            Rpt_InsurancePlanWithCompanyName objrptInsurancePlanWithCompanyName;
            Rpt_InsurancePlanWithoutCompanyName objrptInsurancePlanWithoutCompanyName;
            Rpt_InsurancePlanWithInsRptCategory objRpt_InsurancePlanWithInsRptCategory;
            Rpt_InsurancePlanwithoutInsRptCategory objRpt_InsurancePlanwithoutInsRptCategory;
            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;
        //    private Int64 PatientID;
            private StringBuilder sbPatientID = new StringBuilder();
        
        #endregion " Declarations "

            #region  " Property Procedures "

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

        #endregion  " Property Procedures "


         #region " Constructor "

        public frmRpt_InsurancePlanSetup(string databaseconnectionstring)
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
                //PatientID = nPatientID;
            }

        #endregion

           

       

     
        
        #region "Fill Methods"

        private void FillInsurancePlansWithCompanyName()
        {
            if (objrptInsurancePlanWithCompanyName != null)
            {
                objrptInsurancePlanWithCompanyName.Dispose();
                objrptInsurancePlanWithCompanyName = null;
            }
            objrptInsurancePlanWithCompanyName = new Rpt_InsurancePlanWithCompanyName();
            if (objrptInsurancePlanWithoutCompanyName != null)
            {
                objrptInsurancePlanWithoutCompanyName.Dispose();
                objrptInsurancePlanWithoutCompanyName = null;
            }
            objrptInsurancePlanWithoutCompanyName = new Rpt_InsurancePlanWithoutCompanyName();
            if (objRpt_InsurancePlanWithInsRptCategory != null)
            {
                objRpt_InsurancePlanWithInsRptCategory.Dispose();
                objRpt_InsurancePlanWithInsRptCategory = null;
            }
            objRpt_InsurancePlanWithInsRptCategory = new Rpt_InsurancePlanWithInsRptCategory();
            if (objRpt_InsurancePlanwithoutInsRptCategory != null)
            {
                objRpt_InsurancePlanwithoutInsRptCategory.Dispose();
                objRpt_InsurancePlanwithoutInsRptCategory = null;
            }
            objRpt_InsurancePlanwithoutInsRptCategory = new Rpt_InsurancePlanwithoutInsRptCategory();
            dsReports dsReports = new dsReports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                if (_ogloReportViewer.nReportType==0)
                {
                    _sqlcommand.CommandText = "gsp_GetInsurancePlanswithCompanyName";
                    _sqlcommand.Connection = oConnection;
                   
                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(dsReports, "dt_InsurancePlanswithCompanyName");
                    da.Dispose();

                    objrptInsurancePlanWithCompanyName.SetDataSource(dsReports);

                    
                    //Binds the Report to the Report viewer
                    _ogloReportViewer.ReportViewer = objrptInsurancePlanWithCompanyName;
                    
                    
                }
                else if (_ogloReportViewer.nReportType == 1)
                {
                    _sqlcommand.CommandText = "gsp_GetInsurancePlanswithoutCompanyName";
                    _sqlcommand.Connection = oConnection;
                
                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(dsReports, "dt_InsurancePlansWithoutCompanyName");
                    da.Dispose();

                    objrptInsurancePlanWithoutCompanyName.SetDataSource(dsReports);

                    //Binds the Report to the Report viewer
                    _ogloReportViewer.ReportViewer = objrptInsurancePlanWithoutCompanyName;
                  
                }

                else if (_ogloReportViewer.nReportType == 2)
                {
                    _sqlcommand.CommandText = "gsp_GetInsPlanswithInsReportingCategory";
                    _sqlcommand.Connection = oConnection;

                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(dsReports, "dt_InsurancePlanswithInsRptCategory");
                    da.Dispose();

                    objRpt_InsurancePlanWithInsRptCategory.SetDataSource(dsReports);

                    //Binds the Report to the Report viewer
                    _ogloReportViewer.ReportViewer = objRpt_InsurancePlanWithInsRptCategory;

                }

                else if (_ogloReportViewer.nReportType == 3)
                {
                    _sqlcommand.CommandText = "gsp_GetInsPlanswithOutInsReportingCategory";
                    _sqlcommand.Connection = oConnection;

                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(dsReports, "dt_InsurancePlansWithoutInsRptCategory");
                    da.Dispose();

                    objRpt_InsurancePlanwithoutInsRptCategory.SetDataSource(dsReports);

                    //Binds the Report to the Report viewer
                    _ogloReportViewer.ReportViewer = objRpt_InsurancePlanwithoutInsRptCategory;

                }
                else
                
                {
                    _sqlcommand.CommandText = "gsp_GetInsPlanswithOutInsReportingCategory";
                    _sqlcommand.Connection = oConnection;

                    SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(dsReports, "dt_InsurancePlansWithoutInsRptCategory");
                    da.Dispose();

                    objRpt_InsurancePlanwithoutInsRptCategory.SetDataSource(dsReports);

                    //Binds the Report to the Report viewer
                    _ogloReportViewer.ReportViewer = objRpt_InsurancePlanwithoutInsRptCategory;

                
                }

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

        //Event Fot Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            FillInsurancePlansWithCompanyName();
        }

        #endregion
       
    
      

        
        #region "Form Events"
        private void frmRpt_InsurancePlanSetup_Load(object sender, EventArgs e)
        {
            //For Addding the ReportViewer User Control in form
            pnlContainer.Controls.Add(_ogloReportViewer);
            _ogloReportViewer.Dock = DockStyle.Fill;

            _ogloReportViewer.showPatientMultiselect = true;
            _ogloReportViewer.setPatientID = 0; // PatientID;

            _ogloReportViewer.showReportCriteria = true;


            //Property to show the Export Button on Tool Bar fpnlCriteria
            _ogloReportViewer.showExport = true;
            _ogloReportViewer.btnDown.Visible = false;
            _ogloReportViewer.btnUP.Visible = false;
            _ogloReportViewer.Label23.Visible = false;
           _ogloReportViewer.showFiterCritiria = false; 
            FillInsurancePlansWithCompanyName();
        }
        #endregion
    }
    }
