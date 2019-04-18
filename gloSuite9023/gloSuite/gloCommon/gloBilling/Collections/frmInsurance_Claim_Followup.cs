using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Runtime.InteropServices;
using gloBilling; 


namespace gloBilling.Collections
{
    public partial class frmInsurance_Claim_Followup : Form
    {
        string strReportServer = string.Empty;
        //private ComboBox combo;
        string strReportFolder = string.Empty;
        string strReportProtocol = string.Empty;
        string strVirtualDir = string.Empty;
        string _reportName = string.Empty;
        string _UserName = string.Empty;
        string _reportTitle = string.Empty;
        string _conn = string.Empty;
        string _messageboxcaption = string.Empty;
        string _parameterName = string.Empty;
        string _ParameterValue = string.Empty;
        string strParameter = string.Empty;
        string reportParam = string.Empty;
        //private bool _IsAllDatesValid = true;
        //gloListControl.gloListControl oListControl = null;
        //gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        
        bool _IsgloStreamReport;
        private Image _Img = null;

        System.Uri SSRSReportURL;
        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public frmInsurance_Claim_Followup()
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
        public Image formIcon
        {
            get { return _Img; }
            set { _Img = value; }
        }
        public bool IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }
        public bool  Close_Btn
        {
            get { return tls_btnExit.Enabled; }
            set { tls_btnExit.Enabled= value; }
        }



        private void tls_btnExit_Click(object sender, EventArgs e)
        {
             
            SSRSViewer.Clear();
            SSRSViewer.Dispose(); 
            this.Close();
        
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);
        private void frmInsurance_Claim_Followup_Load(object sender, EventArgs e)
        {
           
            object oValue = new object(); 
            try
            {
                if (_Img != null)
                {
                    IntPtr myIcon = ((Bitmap)(_Img)).GetHicon();
                    this.Icon = Icon.FromHandle(myIcon);
                    DestroyIcon(myIcon);
                }

                //CURRENT LOGGED IN USER NAME
                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    {_UserName = Convert.ToString(appSettings["UserName"]); }
                }

                
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageboxcaption = "gloPM"; 
                    }
                }
                else
                { _messageboxcaption = "gloPM";  }



                //REPORT SERVER NAME
                GeneralSettings oSetting = new GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                //string str123 = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                //REPORT FOLDER NAME
                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                //VIRTUAL DIRECTORY NAME
                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }

                if (strReportProtocol == "" ||  strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    this.Text = _reportTitle;
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
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
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate", "02/04/2012", false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtEndDate", "02/04/2012", false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sActionCode", "1", false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderID", "1", false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sGroupBy", "1", false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    //this.SSRSViewer.ServerReport.SetParameters(paramList);
                    //this.SSRSViewer.RefreshReport();
                    //tsb_Accept.Visible = false;
                    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    this.SSRSViewer.ServerReport.SetParameters(paramList);
                }
                
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
                else
                {
                    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //_UserName = null;
                oValue = null;
                Cursor.Current = Cursors.Default;
            }

            try
            {
                generateReport();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        
        }

        private void generateReport()
        {

            SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
            this.SSRSViewer.RefreshReport();

        }    
    
    
    
    }
      

       
           

}


       