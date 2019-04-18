using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using Microsoft.Reporting.WinForms;
using System.IO;



namespace gloSSRSApplication
{
    public partial class frmPatientDemographicsViewer : Form
    {

        string strReportProtocol = string.Empty;
        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;

        string _conn = string.Empty;
        string _reportName = string.Empty;
        string _reportTitle = string.Empty;
        string _UserName = string.Empty;
        string _parameterName = string.Empty;
        string _ParameterValue = string.Empty;

        string strParameter = string.Empty;
        string reportParam = string.Empty;


        System.Uri SSRSReportURL;
        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public frmPatientDemographicsViewer()
        {
            InitializeComponent();
        }


        public string Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public string reportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        public string reportTitle
        {
            get { return _reportTitle; }
            set { _reportTitle = value; }
        }

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

        private void frmPatientDemographicsViewer_Load(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            object oValue = new object();
            GeneralSettings oSetting = new GeneralSettings(_conn);

            try
            {

                if (appSettings["UserName"] != null)
                {   if (appSettings["UserName"] != "")
                    { _UserName = Convert.ToString(appSettings["UserName"]); }
                }

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {   strReportProtocol = oValue.ToString();
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

                oSetting.Dispose();
                oSetting = null;

                if (strReportServer == "" || strReportFolder == "" || strVirtualDir == "" || strReportProtocol == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    this.Text = _reportTitle;
                    SSRSReportURL = new Uri( strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }

                        SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;

                        paramList.Clear();
                        string[] PName = _parameterName.Split(',');
                        string[] Pvalue = _ParameterValue.Split(',');


                        int count = PName.Length;
                        for (int i = 0; i <= count - 1; i++)
                        {
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i], false));
                        }

                        PName = null;
                        Pvalue = null;
                        this.SSRSViewer.ServerReport.SetParameters(paramList);

                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                    this.SSRSViewer.RefreshReport();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rsItemNotFound"))
                {
                    if (_reportTitle.Contains("report") || _reportTitle.Contains("Report"))
                    {
                        MessageBox.Show(_reportTitle + " is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(_reportTitle + " Report is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    MessageBox.Show("Report server is not available. Please check report server settings.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Message.Contains("The Report Server Windows service 'ReportServer' is not running"))
                {
                    MessageBox.Show("SQL Server Reporting Service is not installed or Report Server is not running.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Message == "Unable to connect to the remote server" || ex.Message == "The request failed with HTTP status 404: ." || ex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
                {
                    MessageBox.Show("Unable to connect to the report server. Please check report settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _UserName = null;
                oValue = null;
                if (oSetting != null) { oSetting.Dispose(); oSetting = null; }
                Cursor.Current = Cursors.Default;
            }

        }

        private void tls_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
