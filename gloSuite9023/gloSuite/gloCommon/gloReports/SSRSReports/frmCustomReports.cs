using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSSRSApplication;

namespace gloReports
{
    public partial class frmCustomReports : Form
    {
        private string _databaseconnectionstring = "";
        private string _ReportNM = "";
        private string gstrMessageBoxCaption = "gloEMR";


        string sReportProtocol = string.Empty;
        string sReportserver = string.Empty;
        string sReportfolder = string.Empty;
        string sVirtualDir = string.Empty;

        string _conn = string.Empty;
        string _reportName = string.Empty;
        string _reportTitle = string.Empty;        
        string reportParam = string.Empty;


        private string gstrSQLServerName;
        private string gstrDatabaseName;
        private bool gblnSQLAuthentication;
        private string gstrSQLUser;
        private string gstrSQLPassword;        

        /// ////////////
         System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
         List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

         public string reportName
         {
             get { return _reportName; }
             set { _reportName = value; }
         }

         public string Conn
         {
             get { return _conn; }
             set { _conn = value; }
         }

         public string reportTitle
         {
             get { return _reportTitle; }
             set { _reportTitle = value; }
         }

        public frmCustomReports()
        {
            InitializeComponent();
        }

        public frmCustomReports(string databaseconnectionstring, string ReportNm, string _gstrSQLServerName, string _gstrDatabaseName, bool _gblnSQLAuthentication, string _gstrSQLUser, string _gstrSQLPassword)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _ReportNM = ReportNm;
            InitializeComponent();
            _conn = databaseconnectionstring;
            ////////////
            gstrSQLServerName = _gstrSQLServerName;
            gstrDatabaseName = _gstrDatabaseName;
            gblnSQLAuthentication = _gblnSQLAuthentication;
            gstrSQLUser = _gstrSQLUser;
            gstrSQLPassword = _gstrSQLPassword;
            ////////////
        }

        public frmCustomReports(string databaseconnectionstring, string ReportNm)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _ReportNM = ReportNm;
            InitializeComponent();
            _conn = databaseconnectionstring;           
        }


        private void frmCustomReports_Load(object sender, EventArgs e)
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_conn);
            object oValue = new object();


            oSetting.GetSetting("ReportProtocol", out oValue);
            if (oValue != null)
            {   sReportProtocol = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {   sReportserver = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("CustomizedReportFolder", out oValue);
            if (oValue != null)
            {   sReportfolder = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportVirtualDirectory", out oValue);
            if (oValue != null)
            {   sVirtualDir = oValue.ToString();
                oValue = null;
            }

            if (sReportserver == "" || sReportfolder == "" || sVirtualDir == "")
            {
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 

            if (_ReportNM == "")
            {
                this.Text = "Design Customized Reports";
                this.Hide();   
                webBrowser1.BringToFront();
                gloSSRS.Create_Datasource("CCHIT Working", "gloCustomizedSSRS", _databaseconnectionstring, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUser, gstrSQLPassword, true);
                //Fix Bug id :41658 - replace parameter 'false' To 'true' of webBrowser1.Navigate method for working on windows 7 & 8 OS on 20121213
                //webBrowser1.Navigate( sReportProtocol + "://" + sReportserver.ToString().Trim() + "/" + sVirtualDir + "/ReportBuilder/reportbuilder.application?", true);

                //////ReportBuilder/reportbuilder.application?method was commented for because updating to sql 2012 and below code works for both SQL 2008 R2 and sql 2012
                webBrowser1.Navigate(sReportProtocol + "://" + sReportserver.ToString().Trim() + "/" + sVirtualDir + "/ReportBuilder/ReportBuilder_3_0_0_0.application?", true);
                Application.DoEvents();
                this.Close();
                //End Bug id :41658
            }
            else
            {

                this.Text = _ReportNM.Trim();
                reportViewer1.BringToFront();
                               
                reportViewer1.ShowDocumentMapButton = false;
                reportViewer1.DocumentMapCollapsed = true;
                reportViewer1.ShowParameterPrompts = true;
                reportViewer1.ShowBackButton = true;                


                reportParam = "Conn=" + _conn;
                reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                reportViewer1.ServerReport.ReportServerUrl = new Uri(sReportProtocol + "://" + sReportserver + "/" + sVirtualDir); 

                reportViewer1.ServerReport.ReportPath = "/" + sReportfolder.ToString().Trim() + "/"+_ReportNM.Trim () ;
                reportViewer1.RefreshReport();
            }             
        }


       private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Close();
        }

      
    }
}
    