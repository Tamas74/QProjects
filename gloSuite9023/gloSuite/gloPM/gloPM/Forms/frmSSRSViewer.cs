using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPM.Forms
{
    public partial class frmSSRSViewer : Form
    {
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
        private Image _Img = null;

        System.Uri SSRSReportURL;
        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        public frmSSRSViewer()
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

        public Image formIcon
        {
            get { return _Img; }
            set { _Img = value; }
        }

        public bool  IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }
        
        private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
            object oValue = new object();

            try
            {
                if (_Img!=null)
                this.Icon = Icon.FromHandle(((Bitmap)(_Img)).GetHicon());

                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    { _UserName = Convert.ToString(appSettings["UserName"]); }
                }

                //RETRIVING REPORT SERVER NAME
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(Program.GetConnectionString());
                
                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                //RETRIVING REPORT FOLDER NAME
                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                //RETRIVING VIRTUAL DIRECTORY NAME
                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }

                if (strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;                   
                }
                try
                {
                    this.Text = _reportTitle;
                    SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return; 
                }
                

                if (_IsgloStreamReport == true)
                {
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;// +reportParam;
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    this.SSRSViewer.ServerReport.SetParameters(paramList);
                }
                else
                {
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                }
                this.SSRSViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
              
            }
            finally
            {
                _UserName = null;
                oValue = null;
            }
        }

        private string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_conn);
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST");
                if (_Result.ToString() != "")
                { return _Result.ToString(); }
                else
                { return ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }

            finally
            {
                oDB.Dispose();
                _conn = null;
            }

        }

        private void tls_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
