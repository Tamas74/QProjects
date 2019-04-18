using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Web.Services.Protocols;
using System.Runtime.InteropServices;
using gloSSRSApplication.SSRS;
using gloSSRSApplication.rsExecService;
using gloSettings;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace gloSSRSApplication
{
    public class clsPrintReport
    {
        private Dictionary<String, Byte[]> m_renderedReport = null;

        private MemoryStream m_currentPageStream = null;
        private Metafile m_metafile = null;
        int m_numberOfPages;
        private int m_currentPrintingPage;
        private int m_lastPrintingPage;
        string _historyID = null;
        string extension = String.Empty;
        bool _forRendering = false;
        
       
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        string ReportProtocol = string.Empty;
        string Reportserver = string.Empty;
        string Reportfolder = string.Empty;
        string VirtualDirectory = string.Empty;

        string dbConn = string.Empty;
        string dbServerName = string.Empty;
        string dbName = string.Empty;
        string dbUser = string.Empty;
        string dbPassword = string.Empty;

        Boolean SQLAuth;

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        private List<string> PDRFilesToPrint = null;
        
        ////this property proc will hold the value for selected printer, this will be used when it is called from Rx-Meds print when the selected printer value is "Black ICE TIFF"
        string _PrinterName;
        public string PrinterName 
        { get 
             {
               return _PrinterName;
             }
          set
             {
               _PrinterName = value;
             }
        }
      
        ////this is added for to check print is successfull or not 
        bool _IsPrintSuccess=true;
        public bool IsPrintSuccess
        {
            get
            {
                return _IsPrintSuccess;
            }
            set
            {
                _IsPrintSuccess = value;
            }
        }

        public clsPrintReport(string _dbServerName, string _dbName, Boolean _SQLAuth, string _dbUser, string _dbPassword, List<string> PDRFilesToPrint)
        {
            InitializePrintReport(_dbServerName, _dbName, _SQLAuth, _dbUser, _dbPassword);
            this.PDRFilesToPrint = PDRFilesToPrint;
        }

        public clsPrintReport(string _dbServerName, string _dbName, Boolean _SQLAuth, string _dbUser, string _dbPassword)
        {
            InitializePrintReport(_dbServerName, _dbName, _SQLAuth, _dbUser, _dbPassword);
        }

        private void InitializePrintReport(string _dbServerName, string _dbName, Boolean _SQLAuth, string _dbUser, string _dbPassword)
        {
            GeneralSettings oSetting = new GeneralSettings(appSettings["DataBaseConnectionString"].ToString());
            DataTable dt = oSetting.GetSSRSReportSettings();

            if (dt != null)
            {
                DataRow[] dr = dt.Select("sSettingsName='ReportProtocol'");
                if (dr.Length > 0)
                {
                    ReportProtocol = dr[0]["sSettingsValue"].ToString();
                }
                dr = null;


                dr = dt.Select("sSettingsName='ReportServer'");
                if (dr.Length > 0)
                {
                    Reportserver = dr[0]["sSettingsValue"].ToString();
                }
                dr = null;


                dr = dt.Select("sSettingsName='ReportFolder'");
                if (dr.Length > 0)
                {
                    Reportfolder = dr[0]["sSettingsValue"].ToString();
                }
                dr = null;

                dr = dt.Select("sSettingsName='ReportVirtualDirectory'");
                if (dr.Length > 0)
                {
                    VirtualDirectory = dr[0]["sSettingsValue"].ToString();
                }
                dr = null;
                dt.Dispose();
                dt = null;
            }

            dbConn = appSettings["DataBaseConnectionString"].ToString();
            dbServerName = _dbServerName;
            dbName = _dbName;
            SQLAuth = _SQLAuth;
            dbUser = _dbUser;
            dbPassword = _dbPassword;

            oSetting.Dispose();
            oSetting = null;
        }

        public Dictionary<string, byte[]> RenderReport(string rptName, string ParamName, String ParamValue)
        {
            Cursor.Current = Cursors.WaitCursor;

            gloSSRS.Create_Datasource("dsEMR", "gloEMR", dbConn, dbServerName, dbName, SQLAuth, dbUser, dbPassword, true);

           gloSSRSApplication.SSRS.ReportingService2005 rs   = new ReportingService2005(); 
           gloSSRSApplication.rsExecService.ReportExecutionService rsExec = new ReportExecutionService();
           
             

            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url =  ReportProtocol  + "://" + Reportserver + "/" + VirtualDirectory + "/ReportService2005.asmx";
            rsExec.Url = ReportProtocol + "://" + Reportserver + "/" + VirtualDirectory + "/ReportExecution2005.asmx";

            string deviceInfo = null;
            string format = "IMAGE";
            //Byte[] firstPage = null;
            string encoding;
            string mimeType;
            gloSSRSApplication.rsExecService.Warning[] warnings = null;
            string[] streamIDs = null;
            //Byte[][] pages = null;
            Dictionary<String, Byte[]> dicBytes = new Dictionary<string, byte[]>();
            string historyID = null;

            string _reportName = @"/" + Reportfolder + "/" + rptName;

            deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><PrintDpiX>96</PrintDpiX><PrintDpiY>96</PrintDpiY></DeviceInfo>", "emf");
            gloSSRSApplication.SSRS.ParameterValue[] _values = null;
            gloSSRSApplication.SSRS.DataSourceCredentials[] _credentials = null;
            gloSSRSApplication.SSRS.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                gloSSRSApplication.rsExecService.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                if (ParamName != "")
                {

                    string[] SplitReportParamName = ParamName.Split(',');
                    string[] SplitReportParamValue = ParamValue.Split(',');

                    int ParamCount = SplitReportParamName.Length;
                    gloSSRSApplication.rsExecService.ParameterValue[] parameters = new gloSSRSApplication.rsExecService.ParameterValue[ParamCount];

                    if (_parameters.Length > 0)
                    {
                        for (int j = 0; j <= ParamCount - 1; j++)
                        {
                            parameters[j] = new gloSSRSApplication.rsExecService.ParameterValue();
                            parameters[j].Label = SplitReportParamName[j];
                            parameters[j].Name = SplitReportParamName[j];
                            parameters[j].Value = SplitReportParamValue[j].Replace("@", ",");
                        }
                    }
                    rsExec.SetExecutionParameters(parameters, "en-us");
                    SplitReportParamName = null;
                    SplitReportParamValue = null;
                    parameters = null;
                }


                //firstPage = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);


                //m_numberOfPages = streamIDs.Length + 1;
                //pages = new Byte[m_numberOfPages][];

                //pages[0] = firstPage;

                //for (int pageIndex = 1; pageIndex < m_numberOfPages; pageIndex++)
                //{
                //    deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>96</PrintDpiX><PrintDpiY>96</PrintDpiY></DeviceInfo>", "emf", pageIndex + 1);
                //    pages[pageIndex] = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                //}

                //pages = new Byte[0][];

                Byte[] TempPage = null;
                //int pageIndex;
                //pageIndex = 1;
                Boolean DidPageLoad;
                m_numberOfPages = 0;
                int numberOfPages = 1;
                Int32 PrintDpiX, PrintDpiY;
                gloResolution.getPrintDpi_service(rsExec, out PrintDpiX, out PrintDpiY);
                if (PrintDpiX < PrintDpiY)
                {
                    PrintDpiY = PrintDpiX;
                }
                else
                {
                    PrintDpiX = PrintDpiY;
                }
                do
                {
                    DidPageLoad = true;
                    deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>{2}</PrintDpiX><PrintDpiY>{3}</PrintDpiY></DeviceInfo>", "emf", numberOfPages,PrintDpiX, PrintDpiY);
                    //deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>96</PrintDpiX><PrintDpiY>96</PrintDpiY></DeviceInfo>", "emf", numberOfPages );
                    try
                    {
                        TempPage = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                        if (TempPage.Length > 0)
                        {
                            //Page was loaded, so inrease number of pages and expand the array
                            

                            dicBytes.Add((numberOfPages).ToString(), TempPage);
                            numberOfPages += 1;
                            
                            //Array.Resize(ref pages, m_numberOfPages + 1);

                            ////Save the page
                            //pages[pageIndex-1] = TempPage;

                            //Move to the next page
                            //pageIndex += 1;
                        }
                        else
                        {
                            //Page returned was empty, so we are done loading pages
                            DidPageLoad = false;
                        }
                        m_numberOfPages = numberOfPages -1;
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        DidPageLoad = false;
                    }
                } while (DidPageLoad);

                TempPage = null;

            }
            catch (SoapException ex)
            {

                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                this._IsPrintSuccess = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                rs.Dispose();
                rsExec.Dispose();

                deviceInfo = null;
                format = null;
                encoding = null;
                mimeType = null;
                warnings = null;
                streamIDs = null;
                //historyID = null;
                _reportName = null;
            }
            return dicBytes;
            //return pages;
        }

        public bool PrintReport(string rptName, string ParamName, String ParamValue, Boolean blnPrintDialog, string FaxPrinterName, string PDFFileName = "",Microsoft.Reporting.WinForms.ServerReport svrRpt = null)
        {
            if (String.IsNullOrEmpty(FaxPrinterName))
            {
            string msgboxcaption = "";
            if ((appSettings["MessageBOXCaption"])!=null)
            {
                msgboxcaption = appSettings["MessageBOXCaption"];
            }
            if (gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForSSRS)
            {
                string strfilename = "";
                strfilename = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".emf";
                if (svrRpt == null)
                {
                    this.RenderedReport = this.RenderReport(rptName, ParamName, ParamValue);
                }
                else
                {
                    this.RenderedReport = ConvertSSRStoBytes(svrRpt);
                }
                
                //Dictionary<String, Byte[]> dicBytes = new Dictionary<string, byte[]>();
                //for (int i = 0; i < this.RenderedReport.GetLength(0); i++)
                //{
                //    if (this.RenderedReport[i] != null)
                //    {
                //        dicBytes.Add((i + 1).ToString(), this.RenderedReport[i]);
                //    } 
                //}
                List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs = new List<gloPrintDialog.gloPrintProgressController.DocumentInfo>();
                //List<string> ZipedFiles = gloGlobal.gloTSPrint.ZipAllBytes(dicBytes, strfilename, gloGlobal.gloTSPrint.NoOfPages);
                List<string> ZipedFiles = gloGlobal.gloTSPrint.ZipAllBytes(this.RenderedReport, strfilename, gloGlobal.gloTSPrint.NoOfPages);
                for (int i = 0; i <= ZipedFiles.Count - 1; i++)
                {
                    gloPrintDialog.gloPrintProgressController.DocumentInfo DocInfo = new gloPrintDialog.gloPrintProgressController.DocumentInfo();
                    DocInfo.PdfFileName = ZipedFiles[i];
                    DocInfo.SrcFileName = ZipedFiles[i];
                    DocInfo.footerInfo = null;
                    lstDocs.Add(DocInfo);
                }
                Print("", rptName, msgboxcaption, blnPrintDialog, lstDocs,true);
            }
            else
            {
                if (PDFFileName != "")
                {
                    Print(PDFFileName, rptName, msgboxcaption, blnPrintDialog);
                }
                else
                {
                    string strfilename = "";
                    strfilename = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".PDF";

                    bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));

                    gloSSRSApplication.clsSSRSRender ocls = new gloSSRSApplication.clsSSRSRender(dbServerName, dbName, SQLAuth, dbUser, dbPassword);

                    ocls.SSRSGeneratePDF(rptName, ParamName, ParamValue, strfilename);

                    ocls = null;
                    Print(strfilename, rptName, msgboxcaption, blnPrintDialog);
                    
                }
            }
            }
            else
            {
                this.RenderedReport = this.RenderReport(rptName, ParamName, ParamValue);
                PrinterSettings printerSettings = null;
                this._IsPrintSuccess = true;
                try
                {
                    if (m_numberOfPages < 1)
                        return false;
                    printerSettings = new PrinterSettings();
                    if (blnPrintDialog)
                    {
                        PrintDialog PrintDialog1 = new PrintDialog();
                        if (PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) == DialogResult.OK)
                        {
                            printerSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                            ////this logic is added if the printer type is "Black Ice TIFF", therefore the logic wil be compatible to Rx-Meds print logic
                            _PrinterName = printerSettings.PrinterName;

                            if (_PrinterName == "Black Ice TIFF")
                            {
                                //printerSettings.PrinterName = printerName;
                                PrintDocument pd1 = new PrintDocument();
                                m_currentPrintingPage = 1;
                                m_lastPrintingPage = m_numberOfPages;
                                pd1.PrinterSettings = printerSettings;
                                // Print report
                                Console.WriteLine("Printing report...");
                                pd1.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                                pd1.Print();
                                pd1.PrintPage -= new PrintPageEventHandler(this.pd_PrintPage);
                                pd1.Dispose();
                                PrintDialog1.Dispose();
                                PrintDialog1 = null;
                                return false;
                            }
                            PrintDialog1.Dispose();
                            PrintDialog1 = null;
                        }
                        else//////if cancle button is clicked then return false
                        {
                            PrintDialog1.Dispose();
                            PrintDialog1 = null;
                            this._IsPrintSuccess = false;
                            return false;
                        }
                    }


                    /////check if the FaxPrinterName var is not blank then it means var is pass from Rx-Meds when internetfax is false and fax button is clicked
                    if (FaxPrinterName != "")
                    {
                        printerSettings.PrinterName = FaxPrinterName;
                    }

                    //pd = new PrintDocument();
                    m_currentPrintingPage = 1;
                    m_lastPrintingPage = m_numberOfPages;
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings = printerSettings;

                    if (rptName == "rptMU" || rptName == "rptMU_stage1" || rptName == "rptMU_stage2" || rptName == "rptLabFlowSheet" || rptName == "RptPatientImmunizationSummary" || rptName == "rptPatientImmSummaryByTrade" || rptName == "RptVaccineInventory" || rptName == "rpt_InsurancePymtLog")
                    {
                        pd.DefaultPageSettings.Landscape = true;
                    }
                    else
                    {
                        pd.DefaultPageSettings.Landscape = false;
                    }
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                    pd.Print();
                    pd.PrintPage -= new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Dispose();
                }
                catch (Exception ex)
                {
                    this._IsPrintSuccess = false;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (printerSettings != null)
                    {

                        printerSettings = null;
                    }
                }
            }

            return true;
        }

        private Dictionary<String, Byte[]> ConvertSSRStoBytes(Microsoft.Reporting.WinForms.ServerReport svrRpt)
        {
            //Byte[][] pages = null;
            Dictionary<String, Byte[]> dicBytes = new Dictionary<string, byte[]>();
            try
            {
                string mimeType = null;
                string Format = "Image";
                String deviceInfo = null;
                //List<Stream> m_pages = new List<Stream>();

                Int32 PrintDpiX, PrintDpiY;
                gloResolution.getPrintDpi(svrRpt, out PrintDpiX, out PrintDpiY);
                if (PrintDpiX < PrintDpiY)
                {
                    PrintDpiY = PrintDpiX;
                }
                else
                {
                    PrintDpiX = PrintDpiY;
                }
                string fileExtension;

                NameValueCollection firstPageParameters = new NameValueCollection();
                firstPageParameters.Add("rs:PersistStreams", "True");

                // GetNextStream returns the next page in the sequence from the background process
                // started by PersistStreams.
                NameValueCollection nonFirstPageParameters = new NameValueCollection();
                nonFirstPageParameters.Add("rs:GetNextStream", "True");
                deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><PrintDpiX>{1}</PrintDpiX><PrintDpiY>{2}</PrintDpiY></DeviceInfo>", "emf",  PrintDpiX, PrintDpiY);
                Stream pageStream = svrRpt.Render(Format, deviceInfo, firstPageParameters, out mimeType, out fileExtension);
                int numberOfPages = 0;
                // The server returns an empty stream when moving beyond the last page.
                while (pageStream.Length > 0)
                {
                    //m_pages.Add(pageStream);
                    //Move to the next page
                    numberOfPages += 1;

                    dicBytes.Add((numberOfPages).ToString(),ReadFully(pageStream));
                    pageStream = svrRpt.Render(Format, deviceInfo, nonFirstPageParameters, out mimeType, out fileExtension);
                }

                //pages = new Byte[m_pages.Count][];
                //for (int i = 0; i < m_pages.Count; i++)
                //{
                //    pages[i] = ReadFully(m_pages[i]);
                //}

                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, svrRpt.ReportPath + " Rendered to emf..", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                //return pages;
                return dicBytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //return pages;
            return dicBytes;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void Print(string _PDFFileName, string rptName, string msgboxcaption, Boolean blnPrintDialog,List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs= null,Boolean useEMF = false)
        {



            gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

            try
            {
                using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                {
                    oDialog.ConnectionString = dbConn;
                    oDialog.TopMost = true;
                    oDialog.ShowPrinterProfileDialog = true;


                    oDialog.ModuleName = "SSRSReports";
                    oDialog.RegistryModuleName = "SSRSReports";
                    if (!useEMF)
                    {
                        System.Drawing.Printing.PrinterSettings _printsettings = new PrinterSettings();
                        oDialog.PrinterSettings = _printsettings;
                    }
                    if (blnPrintDialog == false)
                    {
                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                        oDialog.bUseDefaultPrinter = true;
                    }

                    //31-Aug-16 Aniket: Resolving Incident 00065617
                    else
                    {
                        oDialog.bUseDefaultPrinter = false;
                    }

                    if (oDialog != null)
                    {

                      
                        IntPtr handle = GetActiveWindow();
                        Control NetControl = ControlFromHandle(handle);
                        if (!useEMF)
                        {
                            try
                            {
                                //Bug #96771 added for selecting specific pages for printing 
                                FileStream fs = new FileStream(_PDFFileName, FileMode.Open, FileAccess.Read);
                                StreamReader r = new StreamReader(fs);
                                string pdfText = r.ReadToEnd();
                                Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                                MatchCollection matches = rx1.Matches(pdfText);
                                fs.Close();
                                fs.Dispose();
                                r.Close();
                                r.Dispose();
                                fs = null;
                                r = null;
                                pdfText = null;
                                if (matches.Count > 0)
                                {
                                    oDialog.AllowSomePages = true;
                                    oDialog.PrinterSettings.ToPage = matches.Count;
                                    oDialog.PrinterSettings.FromPage = 1;
                                }
                            }
                            catch
                            {
                            }
                        }

                        DialogResult result = oDialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
                            {
                                oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                            }
                            if (!useEMF)
                            {
                                if (rptName.Contains("rptViewICDReport") || rptName.Contains("rptPatientPrescriptions") || rptName.Contains("rpteRx") || rptName.Contains("rptFinanciaDetails") || rptName.Contains("rptCPTAnalysisDetailsReport") || rptName.Contains("PrescriptionUsageReport") || rptName.Contains("rptPatientList") || rptName.Contains("rptMU") || rptName.Contains("rptMU_stage1") || rptName.Contains("rptMU_stage2") || rptName.Contains("rptLabFlowSheet") || rptName.Contains("RptPatientImmunizationSummary") || rptName.Contains("rptPatientImmSummaryByTrade") || rptName.Contains("RptVaccineInventory") || rptName.Contains("rpt_InsurancePymtLog") || rptName.Contains("%ExpCollectionReport") || rptName.Contains("rptAgingReport") || rptName.Contains("rptFinancialReport") || rptName.Contains("rptAvailableReserves") || rptName.Contains("rptDailyCloseSummary") || rptName.Contains("rptMonthlyCloseSummary") || rptName.Contains("rptFinProReport") || rptName.Contains("rptFinProReport - ICD") || rptName.Contains("rptMissOppReport") || rptName.Contains("RptBatchEligiility") || rptName.Contains("rptPriorReport") || rptName.Contains("rptExcludePatientDue") || rptName.Contains("rptBatchReport") || rptName.Contains("rptFeeScheduleUnderReimbursement") || rptName.Contains("rptBatchLagReport") || rptName.Contains("RptViewVitalsCustomization"))
                                {
                                    oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
                                }

                            }
                            if (PDRFilesToPrint != null && PDRFilesToPrint.Count > 0)
                            {
                                if (lstDocs == null)
                                {
                                    lstDocs = new List<gloPrintDialog.gloPrintProgressController.DocumentInfo>();
                                }
                                gloPrintDialog.gloPrintProgressController.DocumentInfo pdrDocInfo = null;
                                if (!useEMF)
                                {
                                    pdrDocInfo = new gloPrintDialog.gloPrintProgressController.DocumentInfo();
                                    pdrDocInfo.PdfFileName = _PDFFileName;
                                    pdrDocInfo.SrcFileName = _PDFFileName;
                                    pdrDocInfo.footerInfo = null;
                                    lstDocs.Add(pdrDocInfo);
                                    pdrDocInfo = null;
                                }
                                for (int i = 0; i <= PDRFilesToPrint.Count - 1; i++)
                                {
                                    pdrDocInfo = new gloPrintDialog.gloPrintProgressController.DocumentInfo();
                                    pdrDocInfo.PdfFileName = PDRFilesToPrint[i];
                                    pdrDocInfo.SrcFileName = PDRFilesToPrint[i];
                                    pdrDocInfo.footerInfo = null;
                                    lstDocs.Add(pdrDocInfo);
                                    pdrDocInfo = null;
                                }
                                ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(lstDocs, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, blnUseEMFForSSRS: useEMF, blnisPDRPrinting: true);
                            }
                            else
                            {
                                if (useEMF)
                                {
                                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(lstDocs, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, blnUseEMFForSSRS: true);
                                }
                                else
                                {
                                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings);
                                }
                            }
                            ogloPrintProgressController.showTSPrinterSelection = blnPrintDialog;
                            ogloPrintProgressController.ShowProgress(NetControl);
                            if (ogloPrintProgressController.isTSPrintCancelled)
                            {
                                 this.IsPrintSuccess = false;
                            }
                        }
                        else if (result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            this.IsPrintSuccess = false;
                        }
                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;

                            _MessageString = "";
                        }

                        
                        MessageBox.Show(_ErrorMessage, msgboxcaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                
                

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, msgboxcaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {

            }

        }
        private static Control ControlFromHandle(IntPtr hWND)
        {
            while (hWND != IntPtr.Zero)
            {
                Control control = Control.FromChildHandle(hWND);
                if (control != null)
                {
                    if (control is Form)
                    {
                        Control childControl = ((Form)control).ActiveControl;
                        if (childControl != null)
                        {
                            control = childControl;
                        }
                    }
                }
                if (control != null)
                    return control;

                hWND = GetParent(hWND);

                //  Control control2 = System.Windows.Forms.Control.FromChildHandle(hWND);
                // IntPtr hwnd = (IntPtr)this.Handle.ToPointer();
            }

            return null;
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            ev.HasMorePages = false;
            if (m_currentPrintingPage <= m_lastPrintingPage && MoveToPage(m_currentPrintingPage))
            {
                ReportDrawPage(ev.Graphics, ev);
                if (++m_currentPrintingPage <= m_lastPrintingPage)
                    ev.HasMorePages = true;
            }
        }

        //private void ReportDrawPage(Graphics g)
        //{
        //    if (null == m_currentPageStream || 0 == m_currentPageStream.Length || null == m_metafile)
        //        return;
        //    lock (this)
        //    {
        //        int width = m_metafile.Width;
        //        int height = m_metafile.Height;
        //        m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
        //        Point destPoint = new Point(0, 0);
        //        g.EnumerateMetafile(m_metafile, destPoint, m_delegate);
        //        m_delegate = null;
        //    }
        //}

        private void ReportDrawPage(Graphics g, PrintPageEventArgs ev)
        {
            if (null == m_currentPageStream || 0 == m_currentPageStream.Length || null == m_metafile)
                return;
            lock (this)
            {
                int width = m_metafile.Width;
                int height = m_metafile.Height;
                Graphics.EnumerateMetafileProc m_delegate = null;
                m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
                Point destPoint = new Point(0, 0);

                Rectangle m_printRect;
                m_printRect = new Rectangle(ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX, ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY, width, height);

                g.EnumerateMetafile(m_metafile, m_printRect, m_delegate);

                m_delegate = null;
            }
        }

        private bool MoveToPage(Int32 page)
        {
            try
            {
                if (null == this.RenderedReport[m_currentPrintingPage.ToString()])
                    return false;
                if (null != m_currentPageStream)
                {
                    m_currentPageStream.Dispose();
                    m_currentPageStream = null;
                }
                m_currentPageStream = new MemoryStream(this.RenderedReport[m_currentPrintingPage.ToString()]);
                m_currentPageStream.Position = 0;
                if (null != m_metafile)
                {
                    m_metafile.Dispose();
                    m_metafile = null;
                }
                m_metafile = new Metafile((Stream)m_currentPageStream);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        private bool MetafileCallback(
           EmfPlusRecordType recordType,
           int flags,
           int dataSize,
           IntPtr data,
           PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
            }
            m_metafile.PlayRecord(recordType, flags, dataSize, dataArray);
            return true;
        }

        public Dictionary<String, Byte[]> RenderedReport
        {
            get
            {
                return m_renderedReport;
            }
            set
            {
                if (m_renderedReport != null)
                {
                    m_renderedReport.Clear();
                }
                m_renderedReport = value;
            }
        }
        public void Dispose()
        {
            if (null != m_metafile)
            {
                m_metafile.Dispose();
                m_metafile = null;
            }
            if (null != m_currentPageStream)
            {
                m_currentPageStream.Dispose();
                m_currentPageStream = null;
            }
            if (m_renderedReport != null)
            {
                m_renderedReport.Clear();
                m_renderedReport = null;
            }
        }
    }

    public static class gloResolution
    {
        private static Int32 _printDpiX = 96;
        private static Int32 _printDpiY = 96;

        private static Int32 _printDpi_serviceX = 96;
        private static Int32 _printDpi_serviceY = 96;

        private static bool _isDpiUpdated = false;
        private static bool _isDpiUpdated_service = false;

        public static void getPrintDpi(Microsoft.Reporting.WinForms.ServerReport svrRpt, out Int32 dpiX, out Int32 dpiY)
        {
            dpiX = _printDpiX;
            dpiY = _printDpiY;
            
            if (!_isDpiUpdated)
            {
                try
                {
                    Microsoft.Reporting.WinForms.Warning[] warnings = null;
                    string[] streamids = null;
                    string mimeType = null;
                    string encoding = null;
                    string extension = null;
                    string Format = null;
                    String deviceInfo = null;
                    Format = "Image";
                    byte[] TempPage = null;

                    deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>{2}</PrintDpiX><PrintDpiY>{3}</PrintDpiY></DeviceInfo>", "emf", 1, _printDpiX, _printDpiY);
                    try
                    {
                        TempPage = svrRpt.Render(Format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                        if (TempPage.Length > 0)
                        {
                            MemoryStream tempS = new MemoryStream(TempPage);
                            tempS.Seek(0, SeekOrigin.Begin);
                            System.Drawing.Image myMetaImage = null;
                            try
                            {
                                myMetaImage = System.Drawing.Image.FromStream(tempS);
                            }
                            catch
                            {
                            }
                            if (myMetaImage != null)
                            {
                                _printDpiX=(Int32)myMetaImage.HorizontalResolution;
                                _printDpiY = (Int32)myMetaImage.VerticalResolution;
                                dpiX = _printDpiX;
                                dpiY = _printDpiY;
                                _isDpiUpdated = true;
                                myMetaImage.Dispose();
                                myMetaImage = null;
                            }
                            if (tempS != null)
                            {
                                tempS.Dispose();
                                tempS = null;
                            }
                        }
                    }
                    catch
                    {}
                    TempPage = null;
                }
                catch
                {}
            }
        }

        public static void getPrintDpi_service(ReportExecutionService rsExec, out Int32 dpiX, out Int32 dpiY)
        {
            dpiX = _printDpi_serviceX;
            dpiY = _printDpi_serviceY;

            if (!_isDpiUpdated_service)
            {
                try
                {
                    gloSSRSApplication.rsExecService.Warning[] warnings = null;
                    string[] streamids = null;
                    string mimeType = null;
                    string encoding = null;
                    string extension = null;
                    string Format = null;
                    string deviceInfo = null;
                    Format = "Image";
                    byte[] TempPage = null;

                    deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>{2}</PrintDpiX><PrintDpiY>{3}</PrintDpiY></DeviceInfo>", "emf", 1, _printDpi_serviceX, _printDpi_serviceY);
                    try
                    {
                        TempPage = rsExec.Render(Format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamids);
                        //TempPage = svrRpt.Render(Format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                        if (TempPage.Length > 0)
                        {
                            MemoryStream tempS = new MemoryStream(TempPage);
                            tempS.Seek(0, SeekOrigin.Begin);
                            System.Drawing.Image myMetaImage = null;
                            try
                            {
                                myMetaImage = System.Drawing.Image.FromStream(tempS);
                            }
                            catch
                            {
                            }
                            if (myMetaImage != null)
                            {
                                _printDpi_serviceX = (Int32)myMetaImage.HorizontalResolution;
                                _printDpi_serviceY = (Int32)myMetaImage.VerticalResolution;
                                dpiX = _printDpi_serviceX;
                                dpiY = _printDpi_serviceY;
                                _isDpiUpdated_service = true;
                                myMetaImage.Dispose();
                                myMetaImage = null;
                            }
                            if (tempS != null)
                            {
                                tempS.Dispose();
                                tempS = null;
                            }
                        }
                    }
                    catch
                    { }
                    TempPage = null;
                }
                catch
                { }
            }
        }
    }
}
