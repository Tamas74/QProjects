using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloReports
{
    public partial class frmEMRSSRSViewer : Form
    {

        string sReportProtocol = string.Empty;
        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;


        string _reportName = string.Empty;
        string strParameter = string.Empty;
        string _UserName = string.Empty;
        string _conn = string.Empty;
        string _reportTitle = string.Empty;
        string reportParam = string.Empty;
        bool _IsgloStreamReport ;
        
        System.Uri SSRSReportURL; 


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        public frmEMRSSRSViewer()
        {
            InitializeComponent();
        }

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

        public bool  IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }

       
        private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]); }
            }


            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_conn);
            object oValue = new object();


            oSetting.GetSetting("ReportProtocol", out oValue);
            if (oValue != null)
            {   sReportProtocol = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {   strReportServer = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportFolder", out oValue);
            if (oValue != null)
            {   strReportFolder = oValue.ToString();
                oValue = null;
            }

            oSetting.GetSetting("ReportVirtualDirectory", out oValue);
            if (oValue != null)
            {   strVirtualDir = oValue.ToString();
                oValue = null;

            }

            if (strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
            {
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
            reportParam = "&suser=" + _UserName + "&Practice=" + getClinicName() + "&Conn=" + _conn;
            this.Text = _reportTitle;

            List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();


            SSRSReportURL = new Uri(sReportProtocol + "://" + strReportServer+ "/" + strVirtualDir );

            SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
            
                
             
            if (_IsgloStreamReport == true )
            {
                SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Conn", _conn, false));
                this.SSRSViewer.ServerReport.SetParameters(paramList);    
            }
            else 
            {
                SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
            }
            this.SSRSViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SSRS Reporting Service is not available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return;
            }
        }

        private string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_conn);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST");
            if (_Result.ToString() != "")
            { return _Result.ToString(); }
            else
            { return ""; }
        }

        private void SetParameter(string ParameterName, string ParameterValue)
        {
            string[] PName = ParameterName.Split(',');
            string[] Pvalue = ParameterValue.Split(',');
            int count = PName.Length;
            for (int i = 1; i<=count; i++)
                {
                    MessageBox.Show("Current value of i is - " + i);
                }
        }

        private void tls_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
