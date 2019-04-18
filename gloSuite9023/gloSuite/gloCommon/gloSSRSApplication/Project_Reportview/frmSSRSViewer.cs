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
using System.Reflection;


namespace SSRSApplication
{
    public partial class frmSSRSViewer : Form
    {
        string strReportProtocol = string.Empty;
        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;
        string _reportName = string.Empty;
        string _UserName = string.Empty;
        string _reportTitle = string.Empty;
        string _conn = string.Empty;
        string _parameterName = string.Empty;
        string _ParameterValue = string.Empty;
        string strParameter = string.Empty;
        string reportParam = string.Empty;

        public delegate void AcceptClick(Object Sender, EventArgs e, AcceptClickArgs eAccept, ref ProgressBar oProgress, ref Label oLabel);
        public event AcceptClick Accept_Click;
        public delegate void TrayClick(Object Sender, EventArgs e);
        public event TrayClick Tray_Click;
        public delegate void TrayLoad(Object Sender, EventArgs e);
        public event TrayLoad Tray_Load;
        public delegate void CloseClick(Object Sender, EventArgs e);
        public event CloseClick Close_Click;

        private EventHandler methodToInvokedeltaUpdate = null;
        private static String sLastUpdatedOn = "";
        
        bool _IsgloStreamReport;
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

        public string PaymentTray
        {
            get { return lblPaymentTray.Text.Trim(); }
            set { lblPaymentTray.Text = value; }
        }

        public Int64 TrayID
        {
            get;
            set;
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
        public bool Accept_Btn
        {
            get { return tsb_Accept.Enabled; }
            set { tsb_Accept.Enabled = value; }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);
        
       private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
           
            Cursor.Current = Cursors.WaitCursor;

            object oValue = new object();
            GeneralSettings oSetting = new GeneralSettings(_conn);

            try
            {
                if (_Img != null)
                {
                    IntPtr myIcon = ((Bitmap)(_Img)).GetHicon();
                    this.Icon = Icon.FromHandle(myIcon);
                    DestroyIcon(myIcon);
                }


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
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    this.Text = _reportTitle;
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewer.ServerReport.Timeout = -1;
                    SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }


                
                if (_reportName == "rptFinancialReport" || _reportName == "rptFinProReport")
                {
                    StringBuilder sStrMsg = new StringBuilder();
                    sStrMsg.Append("New Cached-Financial Productivity summary report is available for use.");
                    sStrMsg.AppendLine();
                    sStrMsg.AppendLine();
                    sStrMsg.Append("Benefits: Data is already “gathered” so report runs more quickly and does not consume system resources.");
                    sStrMsg.AppendLine();
                    sStrMsg.AppendLine();
                    sStrMsg.AppendLine("Limitations: Transactions are as of previous midnight so does not include transactions that have occurred since previous mid-night");
                    //MessageBox.Show("New Cached-Financial Productivity summary report is available for use." + Environment.NewLine + "To access this report, click \" MIS->Cached-Financial Productivity Summary \"", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(sStrMsg.ToString() , "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sStrMsg = null;
                }
                    

                if (_reportName == "rptFinProReportCache")
                {
                    tsbUpdate.Visible = true;
                    pnlLastUpdatedOn.Visible = true;
                    setLastUpdateDate();
                }
                else
                {
                    tsbUpdate.Visible = false;
                    pnlLastUpdatedOn.Visible = false;
                }

                if (_IsgloStreamReport == true)
                {
                    
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;

                    if (_reportName.Contains("rptERA"))
                    {
                        paramList.Clear();
                        string[] PName = _parameterName.Split(',');
                        string[] Pvalue = _ParameterValue.Split(',');

                        if (_reportName == "rptERAPosting")
                        {
                            tsb_Accept.Visible = true;
                            pnlPaymentTrayDate.Visible = true;
                            Tray_Load(sender, e);
                        }
                        else
                        {
                            tsb_Accept.Visible = false;
                            pnlPaymentTrayDate.Visible = false;
                        }

                        int count = PName.Length;
                        for (int i = 0; i <= count - 1; i++)
                        {
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i], false));
                        }
                        if (_reportName == "rptERAPayerSetupReport")
                        {
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sPractice", getClinicName(), false));
                        }
                        PName = null;
                        Pvalue = null;
                    }
                    else if (_reportName == "RptMonthlyYearlyProcAnalysis_WithPayment")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptCPTMasterListing")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "%ExpCollectionReport")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptMissOppReport")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rpt_US_Collection_Export")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptAppCensusReport")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptAgedpayment")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptPQRS")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    }
                    else if (_reportName == "rptBadDebt")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    }
                    else if (_reportName == "rpt_ChargeAllowed")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    }
                    else if (_reportName == "rpt_PayerLag")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    }
                    else if (_reportName == "rpt_Patient_Paymentplan")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                    }
                    else if (_reportName == "RptBatchEligiility")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                    }
                    else if (_reportName == "rptClaimRemittance")
                    {
                        tsb_Accept.Visible = false;
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
                    }
                    else if (_reportName == "rptTriggeredClaimRuleInformation")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("UserName", _UserName, false));

                    }
                    else if (_reportName == "rptEMRTreatmentException")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("UserName", _UserName, false));
                    }
                    else if (_reportName == "rptCPTActiveDates")
                    {
                        tsb_Accept.Visible = false;
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("UserName", _UserName, false));
                    }
                    else
                    {
                        if (_reportName == "rptBatchDetail")
                        {
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(parameterName, ParameterValue, false));
                        }
                        else if (_reportName == "rptAgingClaimHistory")
                        {
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
                        }
                        
                        //Duplicate Claim Report.
                        //If condition added to pass startdate and enddate to SSRS Report when calling from print.
                        else if (_reportName=="rptDuplicateClaimReport")
                        {
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
                        }

                      

                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                        tsb_Accept.Visible = false;
                        pnlPaymentTrayDate.Visible = false;
                    }

                    if (_reportName == "rptPatientSaving")
                    {
                        paramList.Clear();
                        string[] PName = _parameterName.Split(',');
                        string[] Pvalue = _ParameterValue.Split(',');

                        int count = PName.Length;
                        for (int i = 0; i <= count - 1; i++)
                        {
                            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter(PName[i], Pvalue[i], false));
                        }

                        paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("user", _UserName, false));                       
                        tsb_Accept.Visible = false;
                        pnlPaymentTrayDate.Visible = false;
                        PName = null;
                        Pvalue = null;
                    }



                    this.SSRSViewer.ServerReport.SetParameters(paramList);
                    
                    
                }
                else
                {
                    tsb_Accept.Visible = false;
                    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                }
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

        //CLINIC NAME
        private string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_conn);
            object _Result = null;
            try
            {
                oDB.Connect(false);
                _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST");
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
               // _conn = null;
                _Result = null;
            }
        }

        private void tls_btnExit_Click(object sender, EventArgs e)
        {
            SSRSViewer.Clear();
            SSRSViewer.Dispose(); 
            this.Close();
        }

        private void tsb_Accept_Click(object sender, EventArgs e)
        {
            AcceptClickArgs _eAccept = new AcceptClickArgs();
            Accept_Click(sender, e, _eAccept, ref prgProgress, ref lblProgress);
            _eAccept = null;
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            Tray_Click(sender, e);
        }

        private void frmSSRSViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close_Click != null && _reportName.Contains("rptERA"))
            {
                if (_reportName != "rptERAPayerSetupReport")
                Close_Click(sender, e);
            }
        }

        private void mskCloseDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void SSRSViewer_PrintingBegin(object sender, ReportPrintEventArgs e)
        {
            if (e.PrinterSettings.PrinterName == "Black Ice TIFF")
            {
                e.Cancel = true;
            }
            else
            {
                
                SSRSViewer.PrinterSettings.PrinterName = e.PrinterSettings.PrinterName;
                SSRSViewer.PrinterSettings.FromPage = e.PrinterSettings.FromPage;
                SSRSViewer.PrinterSettings.ToPage = e.PrinterSettings.ToPage;
            }
        }

        private void tsbtn_print_Click(object sender, EventArgs e)
        {
            if (SSRSViewer .CurrentStatus .CanRefreshData  == false)
            {
                MessageBox.Show("Report is not generated. Generate report before print.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                printReport();
                   // ConvertSSRStoPDF(_reportName );
              
                
            }
        }


        private string ConvertSSRStoPDF(string RptName)
        {
            try
            {
                Warning[] warnings = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string extension = null;
                byte[] bytes = null;
                string Format = null;


                Format = "PDF";
                bytes = this.SSRSViewer.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                string _FileName = "";
                _FileName = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".PDF";
                FileStream fs = new FileStream(_FileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
                fs = null;

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, RptName + " Usage Report Printed..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                return _FileName;
                // Print(_FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //private void Print(string _PDFFileName)
        //{
        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

        //    try
        //    {
        //        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
        //        {
        //            oDialog.ConnectionString = _conn;
        //            oDialog.TopMost = true;
        //            oDialog.ShowPrinterProfileDialog = true;
                    
        //            oDialog.ModuleName = "SSRSReports";
        //            oDialog.RegistryModuleName = "SSRSReports";
                   
        //            if (oDialog != null)
        //            {

        //                oDialog.PrinterSettings = SSRSViewer.PrinterSettings;

        //                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    if (reportName == "%ExpCollectionReport" || reportName =="rptAgingReport" || reportName =="rptFinancialReport" || reportName =="rptAvailableReserves" || reportName =="rptDailyCloseSummary" || reportName =="rptMonthlyCloseSummary" || reportName =="rptFinProReport" || reportName =="rptFinProReport - ICD" || reportName =="rptMissOppReport" || reportName =="RptBatchEligiility" || reportName =="rptPriorReport" || reportName =="rptExcludePatientDue" || reportName =="rptBatchReport" || reportName =="rptFeeScheduleUnderReimbursement" || reportName =="rptBatchLagReport")
                              
        //                    {
        //                        oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
        //                    }
        //                    SSRSViewer.PrinterSettings = oDialog.PrinterSettings;
        //                    if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
        //                    {
        //                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
        //                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
        //                    }
        //                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName , SSRSViewer.PrinterSettings, oDialog.CustomPrinterExtendedSettings);

        //                    ogloPrintProgressController.ShowProgress(this);
                           


        //                }//if
                      
        //            }
        //            else
        //            {
        //                string _ErrorMessage = "Error in Showing Print Dialog";

        //                if (_ErrorMessage.Trim() != "")
        //                {
        //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                           
        //                    _MessageString = "";
        //                }


        //                MessageBox.Show(_ErrorMessage, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
                   
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //          //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;
        //    }
        //    finally
        //    {
        //    }

        //}
        private int getTotalPages()
        {
            try
            {
                PropertyInfo currentReport = typeof(ReportViewer).GetProperty("CurrentReport", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object currentReportObj = currentReport.GetValue(this.SSRSViewer, null);
                PropertyInfo fileManager = currentReportObj.GetType().GetProperty("FileManager", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object fileManagerObj = fileManager.GetValue(currentReportObj, null);
                PropertyInfo count = fileManagerObj.GetType().GetProperty("Count", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object countObj = count.GetValue(fileManagerObj, null);
                return Convert.ToInt32(countObj);
            }
            catch
            {
                return 0;
            }

        }
        private void printReport()
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

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth, sqlUser, sqlPwd);
                if (!(gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForSSRS))
                {
                    PDFFileName = ConvertSSRStoPDF(_reportName);
                }
                clsPrntRpt.PrintReport(_reportName, _parameterName, _ParameterValue, gblnIsDefaultPrinter, "", PDFFileName, SSRSViewer.ServerReport);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                //  Cleanup for used variables under this method.
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

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblPleaseWait.Visible = true;
                SSRSViewer.Enabled = false;
                this.Refresh();
                methodToInvokedeltaUpdate = new EventHandler(this.OnDeltaUpdate);
                methodToInvokedeltaUpdate.BeginInvoke(this, null, new AsyncCallback(this.OnDeltaUpdateComplete), null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        private void OnDeltaUpdate(object sender, System.EventArgs e)
        {
            try
            {
                UpdateDelta();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void UpdateDelta()
        {
            //Cursor.Current = Cursors.WaitCursor;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBPara.Add("@UserID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@DeltaUpdate", 1, ParameterDirection.Input, SqlDbType.Int);
                //oDB.Execute("rpt_FinPro_SSRS_V2_Task_Cache", oDBPara);
                Object lastUpdate = oDB.ExecuteScalar("rpt_FinPro_SSRS_V2_Task_Cache", oDBPara);
                sLastUpdatedOn = lastUpdate.ToString();
                lastUpdate = null;

                //for (long i = 0; i < 50000000000; i++)
                //{
                //    //   System.Threading.Thread.Sleep(1000);
                //}
                reflectDate();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }
            }
        }

        public delegate void OnDeltaUpdateCompleteDelegate();
        private void reflectDate()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new OnDeltaUpdateCompleteDelegate(reflectDate));
                }
                else 
                {
                    lblLastUpdatedOn.Text = sLastUpdatedOn;
                    lblPleaseWait.Visible = false;
                    SSRSViewer.Enabled = true;
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        private void OnDeltaUpdateComplete(IAsyncResult iar)
        {
            try
            {
                System.Runtime.Remoting.Messaging.AsyncResult ar = iar as System.Runtime.Remoting.Messaging.AsyncResult;
                if (ar != null)
                {
                    EventHandler invokedMethod = ar.AsyncDelegate as EventHandler;
                    if (invokedMethod != null)
                    {
                        try
                        {
                            invokedMethod.EndInvoke(iar);
                        }
                        catch
                        {
                        }
                    }
                }
                MessageBox.Show("Delta updated, Re-run the report to verify updated transactions.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void setLastUpdateDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBPara.Add("@UserID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPara.Add("@DeltaUpdate", 2, ParameterDirection.Input, SqlDbType.Int);
                Object lastUpdate= oDB.ExecuteScalar("rpt_FinPro_SSRS_V2_Task_Cache", oDBPara);
                lblLastUpdatedOn.Text = lastUpdate.ToString();
                lastUpdate = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }

            }
        }
    }

    public class AcceptClickArgs : EventArgs
    {
        public Int64 TrayID { get; set; }
    }
}
