using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloICDAnalysis.ClassLib;
using gloSettings;
using gloSSRSApplication.SSRS;

namespace gloICDAnalysis
{
    public partial class frmMappingReport : Form
    {
         //public string DBConnectionString;
         DataTable _CodeList = new DataTable();

         BackgroundWorker wrkReport;

         DataTable dtReport = null;

         string strReportProtocol = string.Empty;
         string strReportServer = string.Empty;
         string strReportFolder = string.Empty;
         string strVirtualDir = string.Empty;
         string _reportName = string.Empty;
         string _UserName = string.Empty;      
         string _conn = string.Empty;
         string _parameterName = string.Empty;
         string _ParameterValue = string.Empty;
         string strParameter = string.Empty;
         string reportParam = string.Empty;

         string Provtype = "";
         string provid = "";
         string icd = "";
         string dtenddate = "";
         string dtstartdate = "";
         string printvalue = "";
        
         System.Uri SSRSReportURL;
         List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
         System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;   

         public string parameterName
         {
             get { return _parameterName; }
             set { _parameterName = value; }
         }

         public string ParameterValue
         {
             get { return _ParameterValue; }
             set { _ParameterValue = value; }
         }
            
        public frmMappingReport(DataTable CodeList)
        {
            _CodeList = CodeList.DefaultView.ToTable(true, new string[] { "sICDCode" });         
            InitializeComponent();
        }

        public frmMappingReport(clsICDAnalysis.ProviderType providerType, Int64? ProviderID, DateTime StartDate, DateTime EndDate,  Int32 ICDType, string Print)
        {                      
            Provtype = providerType.ToString ();
            provid =ProviderID.ToString();
            dtenddate = EndDate.ToShortDateString();
            dtstartdate = StartDate.ToShortDateString();
            icd =  ICDType.ToString();
            printvalue = Print.ToString();
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)            
        {
            try
            {
                if (printvalue == "")
                {
                    pnlPleasewait.Visible = true;
                    wrkReport = new System.ComponentModel.BackgroundWorker();
                    wrkReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(wrkReport_RunWorkerCompleted);
                    wrkReport.DoWork += new System.ComponentModel.DoWorkEventHandler(wrkReport_DoWork);
                    wrkReport.RunWorkerAsync();
                    Cursor = Cursors.WaitCursor;
                    tsb_Close.Enabled = false;
                    btnPrint.Visible = false;
                }
                else
                {
                    tsb_Close.Enabled = false;
                    btnPrint.Enabled = false;
                    LoadReport();
                }       
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
            }

            this.rptICDMapping.RefreshReport();
        }

        void wrkReport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                dtReport = clsICDAnalysis.GetMappings(_CodeList);
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
        }

        void wrkReport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                rptICDMapping.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsICD", dtReport));
                pnlPleasewait.Visible = false;
                this.rptICDMapping.RefreshReport();
                Cursor = Cursors.Default;
                tsb_Close.Enabled = true;
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;                
                if (dtReport != null)
                {
                    dtReport.Dispose();
                    dtReport = null;
                }
            }   
        }

       
        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void frmMappingReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (printvalue == "")
            {
                if (!wrkReport.IsBusy)
                {
                    if (wrkReport != null)
                    {
                        wrkReport.Dispose();
                        wrkReport = null;
                    }
                    if (rptICDMapping != null)
                    {
                        rptICDMapping.Dispose();
                        rptICDMapping = null;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
             
        void LoadReport()
        {
            Cursor.Current = Cursors.WaitCursor;

            object oValue = new object();        
            _conn = appSettings["DataBaseConnectionString"].ToString();
            GeneralSettings oSetting = new GeneralSettings(_conn);

            _reportName = "rptViewICDReport";
            
            try
            {

                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    { _UserName = Convert.ToString(appSettings["UserName"]); }
                }

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }

                if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                try
                {                  
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);                
                    rptICDMapping.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    rptICDMapping.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
                    return;
                }

                if (_reportName == "rptViewICDReport")
                {
                    rptICDMapping.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                    
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("User", _UserName, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderID", provid , false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderType", Provtype, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate", dtstartdate, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtEndDate", dtenddate, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ICDType", icd, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Print", "1", false));

                    this.rptICDMapping.ServerReport.SetParameters(paramList);

                    this.rptICDMapping.RefreshReport();                                   
                    pnlPleasewait.Visible = false;                 
                    tsb_Close.Enabled = true;
                    btnPrint.Enabled = true ;
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;              
                oValue = null;
                if (oSetting != null) { oSetting.Dispose(); oSetting = null; }                
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string sqlServerName = string.Empty;
            string sqlDatabaseName = string.Empty;
            string sqlUser = string.Empty;
            string sqlPwd = string.Empty;
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string PDFFileName = "";
            try
            {
                sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
                sqlDatabaseName = Convert.ToString(appSettings["DataBaseName"]);
                sqlUser = Convert.ToString(appSettings["SQLLoginName"]);
                sqlPwd = Convert.ToString(appSettings["SQLPassword"]);

                bool blSQLAuth = !(Convert.ToBoolean(appSettings["WindowAuthentication"]));
                bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));
                               
                _parameterName = "User,ProviderID,ProviderType,dtStartDate,dtEndDate,ICDType,Print";

                _ParameterValue = _UserName + "," + provid + "," + Provtype + "," + dtstartdate + "," + dtenddate + "," + icd + "," + "1" ;

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth, sqlUser, sqlPwd);   
             
                clsPrntRpt.PrintReport(_reportName, _parameterName, _ParameterValue, gblnIsDefaultPrinter, "", PDFFileName, rptICDMapping.ServerReport);

            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog(ex.ToString(), true);
            }
            finally
            {               
                sqlServerName = string.Empty;
                sqlDatabaseName = string.Empty;
                sqlUser = string.Empty;
                sqlPwd = string.Empty;

                if (clsPrntRpt != null)
                {
                    clsPrntRpt.Dispose();
                    clsPrntRpt = null;
                }
            }
        }
    }
}
