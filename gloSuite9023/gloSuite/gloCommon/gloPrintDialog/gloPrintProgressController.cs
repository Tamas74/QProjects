using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;

using System.Drawing.Printing;
using System.Drawing.Imaging;
using pdftron;
using pdftron.PDF;
using pdftron.SDF;
using pdftron.Filters;
using pdftron.Common;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Runtime.Remoting.Messaging;



namespace gloPrintDialog
{
    public partial class gloPrintProgressController : Form
    {

        // int _nPagesCnt = 0;
        bool _IsFromQueue = false;
        string _srcPDFFile = "";
        bool NoTSPrint = true;
        public bool bIsFromScanDoc = false;
        public bool showTSPrinterSelection = !gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false);
        public bool isTSPrintCancelled = false;
        gloClinicalQueueGeneral.QueueDocumentDocumentDetails popUpDetails = null;

        //private static int _printactualTotalPages = 0;
        //private static int _printactualPages = 0;
        //private static int _printPageSubIndex = 0;
        //private static int _printPageMaxIndex = 0;
        //private static int _printPageMaxWidth = 0;
        //private static int _printPageMaxHeight = 0;

        //private int _printPageHorizontalIndex = 0;
        //private int _printPageVerticalIndex = 0;

        //private static int _printBreakHorizontalIndex = 0;
        //private static int _printBreakVerticalIndex = 0;
        private float _footerFontHeight = 0;
        private System.Drawing.Font[] _ExtendedPrinterSettingsFooterFontBy = new System.Drawing.Font[3];

        private bool _printLandscapeMode = false;
        //public bool AutoLandscape = true;
        //public bool AutoMultipage = true;
        //public bool HorizontalFirst = true; 
        private bool _IsOnPagePDFDocCreated = false;
        gloStandardPrintController _gloStandardPrintController = null;


        String _PDFFileName = null;
        PrintDocument _oPrintDocument = null;
        PrinterSettings _PrinterSetting = null;
        private gloExtendedPrinterSettings _ExtendedPrinterSettings = new gloExtendedPrinterSettings();
        private gloPrintControllerEventArgs _gloPrintProgress = new gloPrintControllerEventArgs();

        PDFDraw _PdfDraw = null;
        // Rect _PrintRect = null;
        PDFDoc _PDFDoc = null;
        //Metafile myMetaData = null;
        List<System.Drawing.Image> myEmfImage = new List<System.Drawing.Image>();
        //System.Drawing.Image myMetaImage = null;
        List<gloPrintProgressController.FooterInfo> _PDFFooter = null;

        public delegate void ThreadInitiated(object sender, gloPrintControllerEventArgs e);
        public event ThreadInitiated OnThreadInitiated;

        public delegate void MeasurementsCalculated(object sender, gloPrintControllerEventArgs e);
        public event MeasurementsCalculated OnMeasurementsCalculated;

        public delegate void PrintThreadInitiated(object sender, gloPrintControllerEventArgs e);
        public event PrintThreadInitiated OnPrintThreadInitiated;

        public delegate void PrintingStarted(object sender, gloPrintControllerEventArgs e);
        public event PrintingStarted OnPrintingStarted;
        public delegate void PrintingPages(object sender, gloPrintControllerEventArgs e);
        public event PrintingPages OnPrintingPages;
        public delegate void PrintingFinished(object sender, gloPrintControllerEventArgs e);
        public event PrintingFinished OnPrintingFinished;

        public delegate void ThreadCompleted(object sender, gloPrintControllerEventArgs e);
        public bool IamPrinting = false;
        public event ThreadCompleted OnThreadCompleted;
        //public gloPrintProgressController()
        //{
        //    //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        //    InitializeComponent();
        //}

        public static gloPrintProgressController thisform = null;
        private ArrayList _oSourceDocSelectedPages = null;
        private List<DocumentInfo> _lstDocuments = null;
        private Boolean _useEMFForSSRS = false;
        private Boolean _isPDRPrinting = false;

        public gloPrintProgressController(PDFDoc pdfDoc, String sPDFFileName, PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, List<gloPrintProgressController.FooterInfo> footerList = null, object Key = null, Boolean blnautoLandScape = true, Boolean blnIsFromClinicalChart = false, Boolean blnUseEMFForSSRS = false, string strFileType = "PDF", Boolean blnisPDRPrinting = false)
        {
          
            _useEMFForSSRS = blnUseEMFForSSRS;
            _isPDRPrinting = blnisPDRPrinting;
            gloPrintProgressControllerCall(pdfDoc, sPDFFileName, sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages, footerList, false, "", Key, blnautoLandScape, blnIsFromClinicalChart, strFileType);
        }

        public gloPrintProgressController(String sPDFFileName, PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, List<gloPrintProgressController.FooterInfo> footerList = null, bool IsFromQueue = false, string srcPDFFile = "", object Key = null, Boolean blnautoLandScape = true, Boolean blnIsFromClinicalChart = false, Boolean blnUseEMFForSSRS = false, string strFileType = "PDF", Boolean blnisPDRPrinting = false)
        {
           
            _useEMFForSSRS = blnUseEMFForSSRS;
            _isPDRPrinting = blnisPDRPrinting;
            gloPrintProgressControllerCall((PDFDoc)null, sPDFFileName, sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages, footerList, IsFromQueue, srcPDFFile, Key, blnautoLandScape, blnIsFromClinicalChart, strFileType);
        }

        private bool _ErrorNoDocuments = false;
        public gloPrintProgressController(List<DocumentInfo> lstDocuments, PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, bool IsFromQueue = false, object Key = null, Boolean blnautoLandScape = true, Boolean blnIsFromClinicalChart = false, Boolean blnUseEMFForSSRS = false, string strFileType = "PDF", Boolean blnisPDRPrinting = false)
        {
            if (lstDocuments.Count > 0)
            {
                _useEMFForSSRS = blnUseEMFForSSRS;
                _isPDRPrinting = blnisPDRPrinting;
                _lstDocuments = lstDocuments.ToList(); //SLR: Reference getting disposed and hence made toList?
                DocumentInfo docInfo = _lstDocuments[0];
                AssignPrinterSettings(sPrinterSettings, docInfo);
                gloPrintProgressControllerCall(null, docInfo.PdfFileName, sPrinterSettings, sExtendedPrinterSettings, null, docInfo.footerInfo, IsFromQueue, docInfo.SrcFileName, Key, blnautoLandScape, blnIsFromClinicalChart);//,strFileType);
            }
            else
            {
                _ErrorNoDocuments = true;
            }

        }

        private static void AssignPrinterSettings(PrinterSettings sPrinterSettings, DocumentInfo docInfo)
        {
            if (sPrinterSettings == null)
            {
                return;
            }
            if (docInfo.versionNumber == 1)
            {

                if (!string.IsNullOrEmpty(docInfo.printerPaperSize))
                {
                    bool notFound = true;
                    try
                    {
                        foreach (PaperSize paper in sPrinterSettings.PaperSizes)
                        {
                            if (paper.PaperName == docInfo.printerPaperSize)
                            {
                                sPrinterSettings.DefaultPageSettings.PaperSize = paper;
                                notFound = false;
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (notFound)
                    {
                        try
                        {
                            string strCustom = "Custom(";
                            if (docInfo.printerPaperSize.StartsWith(strCustom))
                            {
                                int cross = docInfo.printerPaperSize.IndexOf('x');
                                if (cross > 0)
                                {
                                    //double InchesThousandThMilliMeter = 2540;
                                    int width = System.Convert.ToInt32(System.Convert.ToDouble(docInfo.printerPaperSize.Substring(strCustom.Length, cross - strCustom.Length - 1)) * gloClinicalQueueGeneral.gloQueueMetadatawriter.InchesHundreds);
                                    int height = System.Convert.ToInt32(System.Convert.ToDouble(docInfo.printerPaperSize.Substring(cross + 1, docInfo.printerPaperSize.Length - cross - 3)) * gloClinicalQueueGeneral.gloQueueMetadatawriter.InchesHundreds);
                                    PaperSize custom = new PaperSize(docInfo.printerPaperSize, width, height);
                                    sPrinterSettings.DefaultPageSettings.PaperSize = custom;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                }
                if (!string.IsNullOrEmpty(docInfo.printerPaperSource))
                {
                    try
                    {
                        foreach (PaperSource paper in sPrinterSettings.PaperSources)
                        {
                            if (paper.SourceName == docInfo.printerPaperSource)
                            {
                                sPrinterSettings.DefaultPageSettings.PaperSource = paper;
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }

                }
                if (docInfo.printFromPageNumber != 0)
                {
                    sPrinterSettings.FromPage = docInfo.printFromPageNumber;
                }
                if (docInfo.printToPageNumber != 0)
                {
                    sPrinterSettings.ToPage = docInfo.printToPageNumber;
                }
                if (docInfo.printerCopies > 0)
                {
                    sPrinterSettings.Copies = (short)docInfo.printerCopies;
                }
                if (sPrinterSettings.CanDuplex)
                {
                    if (docInfo.printerDuplex == 1)
                    {
                        sPrinterSettings.Duplex = Duplex.Simplex;
                    }
                    if (docInfo.printerDuplex == 2)
                    {
                        sPrinterSettings.Duplex = Duplex.Vertical;
                    }
                    if (docInfo.printerDuplex == 3)
                    {
                        sPrinterSettings.Duplex = Duplex.Horizontal;
                    }
                }
                //if (docInfo.printerLandscape)
                {
                    sPrinterSettings.DefaultPageSettings.Landscape = docInfo.printerLandscape;// true;
                }

                if (docInfo.printFromPageNumber != 0)
                {
                    sPrinterSettings.PrintRange = PrintRange.Selection;
                }
                sPrinterSettings.Collate = docInfo.isCollete;
            }
        }
        public static void SendForPrint(List<DocumentInfo> lstDocs)
        {
            gloPrintProgressController ogloPrintProgressController = null;

            try
            {
                gloExtendedPrinterSettings extendedPrinterSettings = new gloExtendedPrinterSettings();
                extendedPrinterSettings.IsShowProgress = false;
                extendedPrinterSettings.IsBackGroundPrint = true;
                ogloPrintProgressController = new gloPrintProgressController(lstDocs, null, extendedPrinterSettings, blnUseEMFForSSRS: true);
                ogloPrintProgressController.ShowProgress(null);
                extendedPrinterSettings.Dispose();
                extendedPrinterSettings = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;

            }
            finally
            {
            }
        }
        public static void SendForPrint(List<DocumentInfo> lstDocs,bool bShowExtendedSettings)
        {
            gloPrintProgressController ogloPrintProgressController = null;

            try
            {
                gloExtendedPrinterSettings extendedPrinterSettings = new gloExtendedPrinterSettings();
                extendedPrinterSettings.IsShowProgress = false;
                extendedPrinterSettings.IsBackGroundPrint = true;
                ogloPrintProgressController = new gloPrintProgressController(lstDocs, null, extendedPrinterSettings, blnUseEMFForSSRS: true);
                ogloPrintProgressController.bIsFromScanDoc = bShowExtendedSettings;
                ogloPrintProgressController.ShowProgress(null);
                extendedPrinterSettings.Dispose();
                extendedPrinterSettings = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;

            }
            finally
            {
            }
        }
        private Boolean _blnautoLandScape = true;
        private string _strFileType = ".pdf";
        private static Boolean _isTSPrintDialogOpen = false;
        private static Boolean _isTSPrinterSelectionOpen = false;
        private Boolean isEventAdded = false;
        internal void gloPrintProgressControllerCall(PDFDoc pdfDoc, String sPDFFileName, PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, List<gloPrintProgressController.FooterInfo> footerList = null, bool IsFromQueue = false, string srcPDFFile = "", object Key = null, Boolean blnautoLandScape = true, Boolean blnIsFromClinicalChart = false, string strFileType = "PDF")
        {

            try
            {
                try
                {
                    InitializeComponent();
                }
                catch
                {
                }
                this.ControlBox = false;
                thisform = this;
                //_strFileType = strFileType;
                //_PDFFile = _strFileType == "PDF";
                PageArray.Clear();
                _PDFDoc = pdfDoc;
                _PDFFileName = sPDFFileName;
                _strFileType = Path.GetExtension(_PDFFileName).ToLower();
                if (String.IsNullOrEmpty(_strFileType.Trim()))
                {
                    _strFileType = ".pdf";
                }
                _PDFFile = _strFileType == ".pdf";
                _PrinterSetting = sPrinterSettings;
                _ExtendedPrinterSettings.Copy(sExtendedPrinterSettings);
                _gloPrintProgress.Key = Key;
                _blnautoLandScape = blnautoLandScape;

                //SLR: In Service, It can not be userinteractive
                if (gloGlobal.clsMISC.IsService)
                {
                    _ExtendedPrinterSettings.IsShowProgress = false;
                }
                if (_ExtendedPrinterSettings.IsShowProgress == false)
                {
                    _ExtendedPrinterSettings.IsBackGroundPrint = true;
                }
                //     _ExtendedPrinterSettings.CurrentPageSize = gloExtendedPrinterSettings.PageSize.TwoByThree;
                _PDFFooter = footerList;

                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    if (gloGlobal.gloTSPrint.isMapped())
                    {
                        String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Guid.NewGuid().ToString() + ".pdf");
                        try
                        {
                            if (pdfDoc != null)
                            {
                                pdfDoc.Save(filePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                                _PDFFileName = filePath;
                            }
                            _gloPrintProgress.success = true;

                            if (OnThreadInitiated != null)
                            {
                                gloResult ResultsSs = new gloResult();
                                ResultsSs.ResultMethod = "OnInit";
                                ResultsSs.ResultType = 0;
                                ResultsSs.ResultObject = "Success";
                                _gloPrintProgress.Result.Add(ResultsSs);

                            }
                        }
                        catch (Exception ex)
                        {
                            _gloPrintProgress.success = false;

                            if (OnThreadInitiated != null)
                            {
                                gloResult ResultsSs = new gloResult();
                                ResultsSs.ResultMethod = "OnInit";
                                ResultsSs.ResultType = 1;
                                ResultsSs.ResultObject = ex;
                                _gloPrintProgress.Result.Add(ResultsSs);
                            }
                        }

                        _oSourceDocSelectedPages = oSourceDocSelectedPages;

                        if (OnThreadInitiated != null)
                        {
                            OnThreadInitiated(this, _gloPrintProgress);
                        }
                        NoTSPrint = false;
                    }
                    else
                    {
                        if (!blnIsFromClinicalChart)
                        {
                            if (!_isTSPrintDialogOpen)
                            {
                                _isTSPrintDialogOpen = true;
                                DialogResult s = MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (s == DialogResult.Yes)
                                {
                                    _isTSPrintDialogOpen = false;
                                    gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found. Using RDP printer", ShowMessageBox: false);

                                }
                                else
                                {
                                    _isTSPrintDialogOpen = false;
                                    gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found. Document Not Printed", ShowMessageBox: false);
                                    _ErrorNoDocuments = true;
                                }
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found Messagebox already active. Document Not Printed", ShowMessageBox: false);
                                _ErrorNoDocuments = true;
                            }

                        }

                    }

                }
                if (NoTSPrint && !_ErrorNoDocuments)
                {
                    if (_PDFFile)
                    {
                        gloRecoverPDF.ConnectToPDFTron();
                        if (_PDFDoc == null)
                        {

                            _PDFDoc = new PDFDoc(_PDFFileName);
                            _IsOnPagePDFDocCreated = true;
                            _IsFromQueue = IsFromQueue;
                            _srcPDFFile = srcPDFFile;
                        }
                        else
                        {
                            if (_ExtendedPrinterSettings.IsBackGroundPrint)
                            {  // If background, _pdfdoc may get disposed by user at foreground form closure. Hence have a copy.
                                try
                                {

                                    byte[] buffer = pdfDoc.Save(pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                                    _PDFDoc = new PDFDoc(buffer, buffer.Length);
                                    buffer = null;
                                    _IsOnPagePDFDocCreated = true;
                                }
                                catch
                                {
                                    // In case unable to handle buffer size due to huge file, store it to a temp file and then load,
                                    try
                                    {
                                        String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Guid.NewGuid().ToString() + ".pdf");
                                        pdfDoc.Save(filePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                                        _PDFDoc = new PDFDoc(filePath);
                                        _IsOnPagePDFDocCreated = true;
                                    }
                                    catch
                                    {

                                        _PDFDoc = pdfDoc;
                                    }
                                }
                            }
                        }
                        _PDFDoc.InitSecurityHandler();
                    }
                    else
                    {
                        _IsFromQueue = IsFromQueue;
                        _srcPDFFile = srcPDFFile;
                        AddImageFilesToPrint();
                        //if (_strFileType != "EMF")
                        //{
                        //    _ErrorNoDocuments = true;
                        //}
                        // myMetaFileDelegate = new Graphics.EnumerateMetafileProc(MetaFileCallBack);
                        //  myMetaData = new Metafile(_PDFFileName);

                        //myMetaImage = System.Drawing.Image.FromFile(_PDFFileName);
                    }
                    //oPDFDOC needs to be initialized
                    if (_PrinterSetting == null)
                    {
                        _PrinterSetting = new System.Drawing.Printing.PrinterSettings();
                    }

                    _oPrintDocument = new PrintDocument();

                    if (_PrinterSetting != null)
                    {
                        try
                        {
                            _oPrintDocument.PrinterSettings = _PrinterSetting;

                        }
                        catch
                        {
                            _oPrintDocument.PrinterSettings.PrinterName = _PrinterSetting.PrinterName;
                        }
                        try
                        {
                            _oPrintDocument.DefaultPageSettings = _PrinterSetting.DefaultPageSettings;
                        }
                        catch
                        {
                        }
                    }
                    _oSourceDocSelectedPages = oSourceDocSelectedPages;
                    _oPrintDocument.QueryPageSettings += new QueryPageSettingsEventHandler(_oPrintDocument_QueryPageSettings);
                    _oPrintDocument.PrintPage += new PrintPageEventHandler(_oPrintDocument_PrintPage);
                    _oPrintDocument.BeginPrint += new PrintEventHandler(_oPrintDocument_BeginPrint);
                    _oPrintDocument.EndPrint += new PrintEventHandler(_oPrintDocument_EndPrint);
                    isEventAdded = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }

        private void AddImageFilesToPrint()
        {
            if (_strFileType == ".zip")
            {
                //_PDFFileName = gloGlobal.gloTSPrint.UnZipEMF(_PDFFileName);
                //myEmfImage.Add(System.Drawing.Image.FromFile(_PDFFileName));
                myEmfImage = gloGlobal.gloTSPrint.UnZipBytesToImages(_PDFFileName);
            }
            else
            {
                System.Drawing.Image myMetaImage = null;
                try
                {
                    myMetaImage = System.Drawing.Image.FromFile(_PDFFileName);
                }
                catch
                {
                }
                if (myMetaImage != null)
                {
                    bool IsTifFile = gloGlobal.gloTSPrint.AddImagesIntoImageList(myEmfImage, myMetaImage);
                    if (IsTifFile)
                    {
                        try
                        {
                            myMetaImage.Dispose();
                            myMetaImage = null;
                        }
                        catch
                        {
                        }
                    }
                }

            }
        }

        //private string UnZip(string _PDFFileName)
        //{
        //    return _PDFFileName;
        //}
        ////Graphics.EnumerateMetafileProc myMetaFileDelegate = null;

        //private bool MetaFileCallBack(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        //{
        //    byte[] dataArray = null;
        //    if (data != IntPtr.Zero)
        //    {
        //        dataArray = new byte[dataSize];
        //        Marshal.Copy(data, dataArray, 0, dataSize);
        //    }
        //    if (myMetaData != null)
        //    {
        //        myMetaData.PlayRecord(recordType, flags, dataSize, dataArray);
        //    }
        //    return true;
        //}
        //public void ShowProgress(Control parent)
        //{
        //    if (_ErrorNoDocuments)
        //    {
        //        _gloPrintProgress.success = false;
        //        if (OnThreadCompleted != null)
        //        {
        //            gloResult ResultsS = new gloResult();
        //            ResultsS.ResultMethod = "OnShowProgress";
        //            ResultsS.ResultType = 2;
        //            ResultsS.ResultObject = "No files selected for printing";
        //            _gloPrintProgress.Result.Add(ResultsS);
        //        }
        //        try
        //        {
        //            if (OnThreadCompleted != null)
        //            {
        //                OnThreadCompleted(this, _gloPrintProgress);
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        //if (!gloGlobal.clsMISC.IsService)
        //        //{
        //        //    MessageBox.Show("No files selected for printing");
        //        //}
        //        if (this != null)
        //        {
        //            this.Dispose();
        //        }
        //        return;
        //    }
        //    //PopulatePrinterPageArray();
        //    if (gloGlobal.gloTSPrint.isCopyPrint && (NoTSPrint == false) && showTSPrinterSelection)
        //    {
        //        _ExtendedPrinterSettings.IsBackGroundPrint = true;
        //        _ExtendedPrinterSettings.IsShowProgress = false;
        //    }

        //    if (gloGlobal.clsMISC.IsService)
        //    {
        //        this.Hide();
        //        //this.PopulatePrinterPageArrayWithOrWithoutBackground();
        //        this.CopyOrPrint();
        //    }
        //    else
        //    {
        //        if (_ExtendedPrinterSettings.IsBackGroundPrint)
        //        {
        //            if (_ExtendedPrinterSettings.IsShowProgress)
        //            {
        //                if (_IsFromQueue)
        //                {
        //                    _ExtendedPrinterSettings.IsShowProgress = false;
        //                    this.Hide();
        //                    this.CopyOrPrint();
        //                }
        //                else
        //                {
        //                    this.Show();
        //                    //this.CopyOrPrint();
        //                }
        //            }
        //            else
        //            {
        //                this.Hide();
        //                // this.PopulatePrinterPageArrayWithOrWithoutBackground();
        //                this.CopyOrPrint();
        //            }
        //        }
        //        else
        //        {
        //            this.TopMost = true;
        //            this.ShowInTaskbar = false;
        //            if (parent == null)
        //            {
        //                try
        //                {
        //                    parent = ActiveForm;
        //                }
        //                catch
        //                {
        //                }
        //            }
        //            if (parent != null)
        //            {
        //                this.ShowDialog(parent);
        //            }
        //            else
        //            {
        //                this.ShowDialog();
        //            }
        //            if (this != null)
        //            {
        //                this.Dispose();
        //            }
        //        }
        //    }
        //}


        public delegate void ShowProgressDelegate();

        private Control parent;
        public void ShowProgress(Control Objparent)
        {
            parent = Objparent;
            ShowProgressControl();
        }

        public void ShowProgressControl()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ShowProgressDelegate(ShowProgressControl));
                    //this.Invoke((MethodInvoker)delegate {ShowProgress(parent);
                }
                else
                {
                    ShowProgressInvoke();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void ShowProgressInvoke() //Control parent
        {
            if (_ErrorNoDocuments)
            {
                _gloPrintProgress.success = false;
                if (OnThreadCompleted != null)
                {
                    gloResult ResultsS = new gloResult();
                    ResultsS.ResultMethod = "OnShowProgress";
                    ResultsS.ResultType = 2;
                    ResultsS.ResultObject = "No files selected for printing";
                    _gloPrintProgress.Result.Add(ResultsS);
                }
                try
                {
                    if (OnThreadCompleted != null)
                    {

                        OnThreadCompleted(this, _gloPrintProgress);
                        IamPrinting = true;
                    }
                }
                catch
                {
                }




                if (this != null)
                {
                    this.Dispose();
                }
                return;
            }

            //PopulatePrinterPageArray();
            if (gloGlobal.gloTSPrint.isCopyPrint && (NoTSPrint == false) && showTSPrinterSelection)
            {
                _ExtendedPrinterSettings.IsBackGroundPrint = true;
                _ExtendedPrinterSettings.IsShowProgress = false;
            }

            if (gloGlobal.clsMISC.IsService)
            {
                this.Hide();
                //this.PopulatePrinterPageArrayWithOrWithoutBackground();
                this.CopyOrPrint();
            }
            else
            {
                if (_ExtendedPrinterSettings.IsBackGroundPrint)
                {
                    if (_ExtendedPrinterSettings.IsShowProgress)
                    {
                        if (_IsFromQueue)
                        {
                            _ExtendedPrinterSettings.IsShowProgress = false;
                            this.Hide();
                            this.CopyOrPrint();
                        }
                        else
                        {
                            //if (parent == null)
                            //{
                            //    try
                            //    {
                            //        parent = ActiveForm;
                            //    }
                            //    catch
                            //    {
                            //    }
                            //}
                            this.Show();
                            if (parent != null)
                            {
                                try
                                {
                         //           parent.Focus();
                                }
                                catch
                                {
                                }
                            }
                            //this.CopyOrPrint();
                        }
                    }
                    else
                    {
                        this.Hide();
                        // this.PopulatePrinterPageArrayWithOrWithoutBackground();
                        this.CopyOrPrint();
                    }
                }
                else
                {
                    this.TopMost = true;
                    this.ShowInTaskbar = false;
                    if (parent == null)
                    {
                        try
                        {
                            parent = ActiveForm;
                        }
                        catch
                        {
                        }
                    }
                    if (parent != null)
                    {
                        this.ShowDialog(parent);
                    }
                    else
                    {
                        this.ShowDialog();
                    }
                    if (this != null)
                    {
                        this.Dispose();
                    }
                }
            }
        }


        public gloExtendedPrinterSettings GetExtendedPrinterSettings()
        {
            return _ExtendedPrinterSettings;
        }

        void _oPrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {

            //   _printPageHorizontalIndex = _ExtendedPrinterSettings.GetHorizontalPagesCount();
            //   _printPageVerticalIndex = _ExtendedPrinterSettings.GetVerticalPagesCount();
            if (_gloPrintProgress.Progress._printPageIndex < PageArray.Count)
            {
                //SLR: Set as part of pagearray itself.
                //if (AutoLandscape)
                //{
                e.PageSettings.Landscape = PageArray[_gloPrintProgress.Progress._printPageIndex].landscape;
                //}
                //else
                //{
                //    e.PageSettings.Landscape = _printLandscapeMode;
                //}
            }
            //if (e.PageSettings.Landscape)
            //{
            //   // e.PageSettings.Landscape = !e.PageSettings.Landscape;

            //    int temp = _printPageHorizontalIndex;
            //    _printPageHorizontalIndex = _printPageVerticalIndex;
            //    _printPageVerticalIndex = temp;

            //}


            //Graphics thisGraphics = _oPrintDocument.PrinterSettings.CreateMeasurementGraphics(e.PageSettings);
            //landscape = e.PageSettings.Landscape;
            //_oPrintDocument_MeasurePageSettings(thisGraphics,  e.PageSettings, _gloPrintProgress.Progress._printPageIndex, ref landscape);
            //e.PageSettings.Landscape = landscape;
            //thisGraphics.Dispose();
        }
        private float prevPageWidth = 0;
        private float prevPageHeight = 0;
        private printOutput prevOutput = null;
        private bool prevFooterFound = false;
        private bool prevlandscape = false;
        private gloExtendedPrinterSettings.PageSize prevPageSize = gloExtendedPrinterSettings.PageSize.None;
        //int _oPrintDocument_MeasurePageSettings(Graphics thisGraphics, PageSettings pg, int index)
        //{

        //    bool landscape = false;
        //    try
        //    {
        //        gloPrintProgressController.FooterInfo footer = null;
        //        thisGraphics.PageUnit = GraphicsUnit.Point;

        //        int thisPageNumber = System.Convert.ToInt32(PageArray[index].page);

        //        if (thisPageNumber >= 0)
        //        {
        //            Page thisPage = _PDFDoc.GetPage(thisPageNumber);
        //            footer = GetFooter(_PDFFooter, thisPageNumber);
        //            if (thisPage != null)
        //            {


        //                //Rect mediaRect = thisPage.GetMediaBox();
        //                double pageWidth = thisPage.GetPageWidth();
        //                double pageHeight = thisPage.GetPageHeight();
        //                if ((pageHeight != prevPageHeight) || (pageWidth != prevPageWidth))
        //                {
        //                    prevPageWidth = pageWidth;
        //                    prevPageHeight = pageHeight;

        //                    GetRealMeasurePageBoundsInPoints(thisGraphics, pg, footer);


        //                    int measurePageHorzWidth = (int)Math.Ceiling((pageWidth - _ExtendedPrinterSettings.HorizontalOverlap) / (double)(measurePrinterHorizontalBounds.Width - _ExtendedPrinterSettings.HorizontalOverlap));
        //                    int measurePageHorzHeight = (int)Math.Ceiling((pageHeight - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)) / (double)(measurePrinterHorizontalBounds.Height - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)));

        //                    int measurePageVertWidth = (int)Math.Ceiling((pageWidth - _ExtendedPrinterSettings.HorizontalOverlap) / (double)(measurePrinterVerticalBounds.Width - _ExtendedPrinterSettings.HorizontalOverlap));
        //                    int measurePageVertHeight = (int)Math.Ceiling((pageHeight - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)) / (double)(measurePrinterVerticalBounds.Height - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)));


        //                    if (measurePageHorzWidth <= 0) measurePageHorzWidth = 1;
        //                    if (measurePageVertWidth <= 0) measurePageVertWidth = 1;
        //                    if (measurePageHorzHeight <= 0) measurePageHorzHeight = 1;
        //                    if (measurePageVertHeight <= 0) measurePageVertHeight = 1;

        //                    if ((measurePageHorzHeight * measurePageHorzWidth) > (measurePageVertHeight * measurePageVertWidth))
        //                    {
        //                        landscape = true;
        //                        //int temp = _printPageHorizontalIndex;
        //                        //_printPageHorizontalIndex = _printPageVerticalIndex;
        //                        //_printPageVerticalIndex = temp;

        //                        PageArray[index].printerbound = new RectangleF(measurePrinterVerticalBounds.Location, measurePrinterVerticalBounds.Size);
        //                        PageArray[index].landscape = landscape;
        //                        PageArray[index].horzsplit = measurePageVertWidth;
        //                        PageArray[index].vertsplit = measurePageVertHeight;

        //                        if (_footerFontHeight != 0)
        //                        {
        //                            PageArray[index].footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterVerticalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterVerticalBounds.Width - _ExtendedPrinterSettings.FooterRight + measurePrinterVerticalBounds.X, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
        //                        }

        //                        return measurePageVertHeight * measurePageVertWidth;
        //                    }
        //                    landscape = false;
        //                    PageArray[index].printerbound = new RectangleF(measurePrinterHorizontalBounds.Location, measurePrinterHorizontalBounds.Size);
        //                    PageArray[index].landscape = landscape;
        //                    PageArray[index].horzsplit = measurePageHorzWidth;
        //                    PageArray[index].vertsplit = measurePageHorzHeight;
        //                    if (_footerFontHeight != 0)
        //                    {
        //                        PageArray[index].footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterHorizontalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterHorizontalBounds.Width - _ExtendedPrinterSettings.FooterRight + measurePrinterHorizontalBounds.X, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
        //                    }

        //                    return measurePageHorzHeight * measurePageHorzWidth;
        //                }
        //                else
        //                {
        //                    PageArray[index].printerbound = PageArray[index - 1].printerbound;
        //                    PageArray[index].landscape = PageArray[index-1].landscape;
        //                    PageArray[index].horzsplit = PageArray[index-1].horzsplit;
        //                    PageArray[index].vertsplit = PageArray[index-1].vertsplit;
        //                    PageArray[index].footerbound = PageArray[index - 1].footerbound;
        //                    return PageArray[index].vertsplit * PageArray[index].horzsplit; 
        //                }
        //            }
        //        }


        //        PageArray[index].landscape = landscape;
        //        return 1;
        //    }
        //    catch
        //    {

        //        PageArray[index].landscape = landscape;
        //        return 1;
        //    }
        //    finally
        //    {

        //    }
        //}
        //public static float AdjustActualPageHorizontalPageWidthMargin = 50.9f;
        //public static float AdjustActualPageVerticalPageHeightMargin = 50.9f;
        //public static float AdjustFitToPageHorizontalPageWidthMargin = 50.9f;
        //public static float AdjustFitToPageVerticalPageHeightMargin = 50.9f;
        private bool _PDFFile = false;

        printOutput _oPrintDocument_MeasurePageParameters(int iPage, int to)
        {
            printOutput thisoutput = new printOutput();

            gloExtendedPrinterSettings.GraphicsBound useThisGraphics = null;
            bReCalcmeasureBounds = true;

            try
            {

                int thisPageNumber = (_oSourceDocSelectedPages == null) ? iPage : System.Convert.ToInt32(_oSourceDocSelectedPages[iPage - 1]);
                if (thisPageNumber >= 0)
                {
                    thisoutput.page = thisPageNumber;
                    if (_PDFFile)
                    {
                        thisoutput.thisPage = _PDFDoc.GetPage(thisPageNumber);
                    }
                    thisoutput.footer = GetFooter(_PDFFooter, thisPageNumber);
                    if ((thisoutput.thisPage != null) || (!_PDFFile))
                    {
                        //double pageWidth = 0;
                        //double pageHeight = 0;
                        if (_PDFFile)
                        {
                            thisoutput.pageWidth = (float)thisoutput.thisPage.GetPageWidth() - 1;
                            thisoutput.pageHeight = (float)thisoutput.thisPage.GetPageHeight() - 1;
                            // Check if file supports clipping
                            if (thisoutput.pageWidth > thisoutput.pageHeight)
                            {
                                //float orgWidth = thisoutput.pageWidth;
                                //float orgHeight = thisoutput.pageHeight;
                                //thisoutput.thisPage.SetRotation(Page.Rotate.e_90);
                                //float newWidth = (float)thisoutput.thisPage.GetPageWidth() - 1;
                                //float newHeight = (float)thisoutput.thisPage.GetPageHeight() - 1;
                                //if ((newWidth == orgWidth) && (newHeight == orgHeight))
                                //{
                                    //_isClippingSupported = false;
                                //}
                                //else
                                //{
                                //    thisoutput.thisPage.SetRotation(Page.Rotate.e_0);
                                //}

                                _isClippingSupported = false;
                            }
                        }
                        else
                        {
                            //pageWidth = myMetaImage.Width / myMetaImage.HorizontalResolution * 72; 
                            //pageHeight = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                            System.Drawing.Image myMetaImage = myEmfImage[thisoutput.page - 1];
                            //thisoutput.pageWidth = myMetaImage.Width / myMetaImage.HorizontalResolution * 72;
                            //thisoutput.pageHeight = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                            AssignWidthAndHeight(ref thisoutput, myMetaImage);
                        }
                        thisoutput.sourcebound = new RectangleF(0, 0, thisoutput.pageWidth, thisoutput.pageHeight);
                        bool footerFound;
                        bool actualLandscape;
                        bool actualMultiPage;
                        int PageHorizontalIndex = 1;
                        int PageVerticalIndex = 1;

                        gloExtendedPrinterSettings.PageSize thisPageSize;
                        if (thisoutput.footer == null)
                        {
                            footerFound = false;
                            actualLandscape = _ExtendedPrinterSettings.IsActualLandscape;
                            actualMultiPage = _ExtendedPrinterSettings.IsActualMultiPage;
                            thisPageSize = _ExtendedPrinterSettings.CurrentPageSize;
                        }
                        else
                        {
                            footerFound = thisoutput.footer.PrintFooter;
                            actualLandscape = thisoutput.footer.AutoLandscape;
                            actualMultiPage = thisoutput.footer.AutoMultipage;
                            thisPageSize = thisoutput.footer.CurrentPageSize;
                        }
                        if (actualLandscape)
                        {
                            useThisGraphics = _ExtendedPrinterSettings.FlatSettings;
                        }
                        else
                        {
                            useThisGraphics = _ExtendedPrinterSettings.NormalSettings;
                        }
                        PageHorizontalIndex = gloExtendedPrinterSettings.GetHorizontalPagesCount(thisPageSize);
                        PageVerticalIndex = gloExtendedPrinterSettings.GetVerticalPagesCount(thisPageSize);

                        if ((thisoutput.pageHeight != prevPageHeight) || (thisoutput.pageWidth != prevPageWidth) || (footerFound != prevFooterFound) || (thisPageSize != prevPageSize) || (actualLandscape != prevlandscape))
                        {
                            prevPageWidth = thisoutput.pageWidth;
                            prevPageHeight = thisoutput.pageHeight;
                            if (bReCalcmeasureBounds == false)
                            {
                                bReCalcmeasureBounds = (footerFound != prevFooterFound) || (thisPageSize != prevPageSize) || (actualLandscape != prevlandscape);
                            }
                            prevFooterFound = footerFound;
                            prevPageSize = thisPageSize;
                            prevlandscape = actualLandscape;
                            GetRealMeasurePageBoundsInPoints(useThisGraphics, thisoutput.footer);

                            if (thisPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
                            {
                                int measurePageHorzWidth = (int)Math.Ceiling((thisoutput.pageWidth - _ExtendedPrinterSettings.HorizontalOverlap) / (double)(measurePrinterHorizontalBounds.Width + _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin - _ExtendedPrinterSettings.HorizontalOverlap));
                                int measurePageHorzHeight = (int)Math.Ceiling((thisoutput.pageHeight - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)) / (double)(measurePrinterHorizontalBounds.Height + _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)));

                                int measurePageVertWidth = (int)Math.Ceiling((thisoutput.pageWidth - _ExtendedPrinterSettings.HorizontalOverlap) / (double)(measurePrinterVerticalBounds.Width + _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin - _ExtendedPrinterSettings.HorizontalOverlap));
                                int measurePageVertHeight = (int)Math.Ceiling((thisoutput.pageHeight - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)) / (double)(measurePrinterVerticalBounds.Height + _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin - _ExtendedPrinterSettings.VerticalOverlap - (_footerFontHeight)));

                                if (measurePageHorzWidth <= 0) measurePageHorzWidth = 1;
                                if (measurePageVertWidth <= 0) measurePageVertWidth = 1;
                                if (measurePageHorzHeight <= 0) measurePageHorzHeight = 1;
                                if (measurePageVertHeight <= 0) measurePageVertHeight = 1;
                                if (actualLandscape)
                                {
                                    bool desideLandscape = (measurePageHorzHeight * measurePageHorzWidth) > (measurePageVertHeight * measurePageVertWidth);
                                    if (!desideLandscape)
                                    {
                                        if ((measurePageHorzHeight * measurePageHorzWidth) == (measurePageVertHeight * measurePageVertWidth))
                                        {
                                            desideLandscape = thisoutput.pageWidth > thisoutput.pageHeight;
                                        }
                                    }
                                    if (desideLandscape)
                                    {
                                        thisoutput.landscape = true;
                                        thisoutput.printerbound = new RectangleF(measurePrinterVerticalBounds.Location, measurePrinterVerticalBounds.Size);

                                        if (actualMultiPage)
                                        {
                                            thisoutput.horzsplit = measurePageVertWidth;
                                            thisoutput.vertsplit = measurePageVertHeight;
                                        }
                                        else
                                        {
                                            thisoutput.horzsplit = 1;
                                            thisoutput.vertsplit = 1;
                                        }

                                        if (_footerFontHeight != 0)
                                        {
                                            thisoutput.footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterVerticalBounds.Top + measurePrinterVerticalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterVerticalBounds.Width - _ExtendedPrinterSettings.FooterRight - 10, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
                                        }
                                        thisoutput.footerfontheight = _footerFontHeight;
                                        thisoutput.iPageBreakIndex = 1;
                                        prevOutput = thisoutput;

                                        return thisoutput;
                                    }

                                    thisoutput.landscape = false;
                                }
                                else
                                {
                                    thisoutput.landscape = _printLandscapeMode;
                                }
                                thisoutput.printerbound = new RectangleF(measurePrinterHorizontalBounds.Location, measurePrinterHorizontalBounds.Size);

                                if (actualMultiPage)
                                {
                                    thisoutput.horzsplit = measurePageHorzWidth;
                                    thisoutput.vertsplit = measurePageHorzHeight;
                                }
                                else
                                {
                                    thisoutput.horzsplit = 1;
                                    thisoutput.vertsplit = 1;
                                }
                                if (_footerFontHeight != 0)
                                {
                                    thisoutput.footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterHorizontalBounds.Top + measurePrinterHorizontalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterHorizontalBounds.Width - _ExtendedPrinterSettings.FooterRight - 10, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
                                }
                                thisoutput.footerfontheight = _footerFontHeight;
                                thisoutput.iPageBreakIndex = 1;
                                prevOutput = thisoutput;
                                return thisoutput;
                            }
                            else
                            {
                                double measurePageHorzWidth = (thisoutput.pageWidth) / (double)(measurePrinterHorizontalBounds.Width);
                                double measurePageHorzHeight = (thisoutput.pageHeight - (_footerFontHeight)) / (double)(measurePrinterHorizontalBounds.Height - (_footerFontHeight));

                                double measurePageVertWidth = (thisoutput.pageWidth) / (double)(measurePrinterVerticalBounds.Width);
                                double measurePageVertHeight = (thisoutput.pageHeight - (_footerFontHeight)) / (double)(measurePrinterVerticalBounds.Height - (_footerFontHeight));

                                if (measurePageHorzWidth <= 0) measurePageHorzWidth = 1.0;
                                if (measurePageVertWidth <= 0) measurePageVertWidth = 1.0;
                                if (measurePageHorzHeight <= 0) measurePageHorzHeight = 1.0;
                                if (measurePageVertHeight <= 0) measurePageVertHeight = 1.0;

                                if (actualLandscape)
                                {
                                    if (Math.Max(measurePageHorzHeight, measurePageHorzWidth) > Math.Max(measurePageVertHeight, measurePageVertWidth))
                                    {
                                        thisoutput.landscape = true;
                                        //thisoutput.printerbound = new RectangleF(measurePrinterVerticalBounds.Location.X, measurePrinterVerticalBounds.Location.Y, measurePrinterVerticalBounds.Width + (_ExtendedPrinterSettings.AdjustFitToPageVerticalPageHeightMargin / _printPageVerticalIndex), measurePrinterVerticalBounds.Height + (_ExtendedPrinterSettings.AdjustFitToPageHorizontalPageWidthMargin / _printPageHorizontalIndex));
                                        //thisoutput.printerbound = new RectangleF(measurePrinterHorizontalBounds.Location, measurePrinterHorizontalBounds.Size);
                                        thisoutput.printerbound = new RectangleF(measurePrinterVerticalBounds.Location, measurePrinterVerticalBounds.Size);

                                        thisoutput.horzsplit = PageVerticalIndex;
                                        thisoutput.vertsplit = PageHorizontalIndex;

                                        if (_footerFontHeight != 0)
                                        {
                                            thisoutput.footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterVerticalBounds.Top + measurePrinterVerticalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterVerticalBounds.Width - _ExtendedPrinterSettings.FooterRight - 10, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
                                        }
                                        thisoutput.footerfontheight = _footerFontHeight;
                                        thisoutput.iPageBreakIndex = GetMaxPageBreakIndex(iPage, _oSourceDocSelectedPages, to, thisoutput, thisPageSize);
                                        prevOutput = thisoutput;
                                        return thisoutput;
                                    }

                                    thisoutput.landscape = false;
                                }
                                else
                                {
                                    thisoutput.landscape = _printLandscapeMode;
                                }
                                thisoutput.printerbound = new RectangleF(measurePrinterHorizontalBounds.Location, measurePrinterHorizontalBounds.Size);
                                //thisoutput.printerbound = new RectangleF(measurePrinterHorizontalBounds.Location.X, measurePrinterHorizontalBounds.Location.Y, measurePrinterHorizontalBounds.Width + (_ExtendedPrinterSettings.AdjustFitToPageHorizontalPageWidthMargin / _printPageHorizontalIndex), measurePrinterHorizontalBounds.Height + (_ExtendedPrinterSettings.AdjustFitToPageVerticalPageHeightMargin / _printPageVerticalIndex));

                                thisoutput.horzsplit = PageHorizontalIndex;
                                thisoutput.vertsplit = PageVerticalIndex;

                                if (_footerFontHeight != 0)
                                {
                                    thisoutput.footerbound = new RectangleF(_ExtendedPrinterSettings.FooterLeft, measurePrinterHorizontalBounds.Top + measurePrinterHorizontalBounds.Height + _ExtendedPrinterSettings.FooterTop, measurePrinterHorizontalBounds.Width - _ExtendedPrinterSettings.FooterRight - 10, _footerFontHeight + _ExtendedPrinterSettings.FooterBottom);
                                }
                                thisoutput.footerfontheight = _footerFontHeight;
                                thisoutput.iPageBreakIndex = GetMaxPageBreakIndex(iPage, _oSourceDocSelectedPages, to, thisoutput, thisPageSize);
                                prevOutput = thisoutput;
                                return thisoutput;

                            }
                        }
                        else
                        {
                            if (prevOutput != null)
                            {
                                thisoutput.CopyExceptPage(prevOutput);
                            }
                            if (thisPageSize != gloExtendedPrinterSettings.PageSize.ActualPageSize)
                            {
                                thisoutput.iPageBreakIndex = GetMaxPageBreakIndex(iPage, _oSourceDocSelectedPages, to, thisoutput, thisPageSize);
                            }
                            else
                            {
                                thisoutput.iPageBreakIndex = 1;
                            }
                            prevOutput = thisoutput;
                            return thisoutput;
                        }
                    }
                }


                if (prevOutput != null)
                {
                    thisoutput.CopyExceptPage(prevOutput);

                }
                thisoutput.iPageBreakIndex = 1;
                prevOutput = thisoutput;

                return thisoutput;

            }
            catch
            {

                if (prevOutput != null)
                {
                    thisoutput.CopyExceptPage(prevOutput);
                }
                thisoutput.iPageBreakIndex = 1;
                prevOutput = thisoutput;

                return thisoutput;
            }
            finally
            {
                //thisGraphicsAutoLandscape.PageUnit = myUnitAutoLandscape;
                //thisGraphics.PageUnit = myUnit;
                useThisGraphics = null;
            }
        }

        private void AssignWidthAndHeight(ref printOutput thisoutput, System.Drawing.Image myMetaImage)
        {
            bool reCalc = true;
            System.Guid imageType = myMetaImage.RawFormat.Guid;
            if (imageType == System.Drawing.Imaging.ImageFormat.Emf.Guid)
            {
                if (myMetaImage.HorizontalResolution != 96 || myMetaImage.VerticalResolution != 96)
                {
                    thisoutput.pageWidth = ((float)myMetaImage.Width) / 96f * 72f;
                    thisoutput.pageHeight = ((float)myMetaImage.Height) / 96f * 72f;
                    if (thisoutput.pageWidth > thisoutput.pageHeight)
                    {
                        if (thisoutput.pageWidth >= 750 && thisoutput.pageWidth <= 830)
                        {
                            reCalc = false;
                        }
                        else if (thisoutput.pageHeight >= 570 && thisoutput.pageHeight <= 650)
                        {
                            reCalc = false;
                        }
                    }
                    else
                    {
                        if (thisoutput.pageHeight >= 750 && thisoutput.pageHeight <= 830)
                        {
                            reCalc = false;
                        }
                        else if (thisoutput.pageWidth >= 570 && thisoutput.pageWidth <= 650)
                        {
                            reCalc = false;
                        }
                    }
                }
            }
            if (reCalc)
            {
                thisoutput.pageWidth = ((float)myMetaImage.Width) / myMetaImage.HorizontalResolution * 72f;
                thisoutput.pageHeight = ((float)myMetaImage.Height) / myMetaImage.VerticalResolution * 72f;
            }
            else
            {
                thisoutput.reCalcSourceBound = true;
            }
        }

        private int GetMaxPageBreakIndex(int iPage, ArrayList oSourceDocSelectedPages, int to, printOutput thisoutput, gloExtendedPrinterSettings.PageSize thisPageSize)
        {
            int iPageBreakIndex = 1;
            if (thisPageSize != gloExtendedPrinterSettings.PageSize.FitToPage)
            {
                int maxPageBreakIndex = gloExtendedPrinterSettings.GetSubPagesCount(thisPageSize);
                for (int jPage = iPage + 1; jPage <= to; jPage++)
                {
                    int nextPageNumber = (oSourceDocSelectedPages == null) ? jPage : System.Convert.ToInt32(oSourceDocSelectedPages[jPage - 1]);
                    FooterInfo nextFooter = GetFooter(_PDFFooter, nextPageNumber);
                    bool toContinue = false;
                    if ((nextFooter == null) && (thisoutput.footer == null))
                    {
                        toContinue = true;
                    }
                    else
                    {
                        toContinue = (nextFooter != null) && (thisoutput.footer != null);
                        if (toContinue)
                        {
                            toContinue = (nextFooter.CurrentPageSize == thisoutput.footer.CurrentPageSize) && (nextFooter.FromPage == thisoutput.footer.FromPage) && (nextFooter.ToPage == thisoutput.footer.ToPage);
                        }
                    }
                    if (toContinue)
                    {
                        iPageBreakIndex++;
                        if (iPageBreakIndex == maxPageBreakIndex)
                        {
                            break;
                        }
                    }
                }
            }
            return iPageBreakIndex;
        }
        private void gloPrintProgressController_Load(object sender, EventArgs e)
        {
            try
            {

                if (!_ExtendedPrinterSettings.IsShowProgress)
                {
                    this.Hide();
                    //this.PopulatePrinterPageArrayWithOrWithoutBackground();

                }
                this.CopyOrPrint();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }

        private void gloPrintProgressController_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_ExtendedPrinterSettings.IsShowProgress)
                {
                    //this.PopulatePrinterPageArrayWithOrWithoutBackground();
                    if (gloGlobal.gloTSPrint.isCopyPrint && (NoTSPrint == false) && showTSPrinterSelection)
                    {
                        this.Hide();
                    }
                    //    this.CopyOrPrint();
                }
                else
                {
                    this.Hide();
                }
                if (parent != null)
                {
                    try
                    {
                    //    parent.Focus();
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }
        private EventHandler methodToInvokePrint = null;
        private EventHandler methodToInvokeCalculation = null;
        private EventHandler methodToInvokeCopy = null;

        public void PrintWithOrWithoutBackground()
        {
            try
            {
                if ((_ExtendedPrinterSettings != null) && (_ExtendedPrinterSettings.IsShowProgress))
                {
                    int retry = 20;
                    while (!IsPrintingCompletedWhilePressingPause)
                    {
                        InvokeBackgroundUpdateControls();
                        Thread.Sleep(100);
                        if (System.Math.Max(System.Threading.Interlocked.Decrement(ref retry), retry + 1) == 0)
                        {
                            //Exhaused waiting for 2 seconds
                            break;
                        };

                    }
                    IsPrintingCompletedWhilePressingPause = false;
                }
                if (_ExtendedPrinterSettings.IsBackGroundPrint)
                {
                    if (methodToInvokePrint != null)
                    {
                        methodToInvokePrint = null;
                    }
                    methodToInvokePrint = new EventHandler(this.OnPrint);
                    methodToInvokePrint.BeginInvoke(this, null, new AsyncCallback(this.OnPrintComplete), null);

                }
                else
                {
                    Print();
                    try
                    {
                        if (OnThreadCompleted != null)
                        {
                            OnThreadCompleted(this, _gloPrintProgress);
                        }
                    }
                    catch
                    {
                    }
                    IsPrintingCompletedWhilePressingPause = true;
                    //if (!IsPrintingResumed)
                    //{
                    //    if (gloGlobal.clsMISC.IsService || (!_ExtendedPrinterSettings.IsShowProgress))
                    //    {
                    //        this.Dispose(true);
                    //    }
                    //    else
                    //    {
                    //        this.Close();
                    //    }
                    //}
                    CloseCompleteControls();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void gloPrintProgressController_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            ClearMemory();
        }
        private bool bClearedMemory = false;
        public void ClearMemory()
        {
            if (bClearedMemory == false)
            {
                try
                {
                    _gloPrintProgress.Progress._printPageIndex = PageArray.Count;
                    if (isEventAdded)
                    {
                        _oPrintDocument.PrintPage -= new PrintPageEventHandler(_oPrintDocument_PrintPage);
                        _oPrintDocument.BeginPrint -= new PrintEventHandler(_oPrintDocument_BeginPrint);
                        _oPrintDocument.EndPrint -= new PrintEventHandler(_oPrintDocument_EndPrint);
                        _oPrintDocument.QueryPageSettings -= new QueryPageSettingsEventHandler(_oPrintDocument_QueryPageSettings);
                        _oPrintDocument.DefaultPageSettings.Landscape = _printLandscapeMode;
                    }
                    if (_PrinterSetting != null)
                    {
                        _PrinterSetting.Copies = _PrintOriginalCopies;
                    }
                    methodToInvokePrint = null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    ex = null;
                }
                finally
                {
                    bClearedMemory = true;
                }
            }
        }
        public class FooterInfo
        {
            public string LeftText = "";
            public string RightText = "";
            public string CenterText = "";
            public int FromPage = 0;
            public int ToPage = 0;
            public int StartingPage = 0;
            public int TotalPages = 0;
            public bool AutoLandscape = false;
            public bool AutoMultipage = false;
            public bool HorizontalFirst = true;
            public bool PrintFooter = true;
            public gloExtendedPrinterSettings.PageSize CurrentPageSize = gloExtendedPrinterSettings.PageSize.None;
            public void CopyExceptText(FooterInfo thisfooter)
            {
                FromPage = thisfooter.FromPage;
                ToPage = thisfooter.ToPage;
                StartingPage = thisfooter.StartingPage;
                TotalPages = thisfooter.TotalPages;
                AutoLandscape = thisfooter.AutoLandscape;
                AutoMultipage = thisfooter.AutoMultipage;
                PrintFooter = thisfooter.PrintFooter;
                CurrentPageSize = thisfooter.CurrentPageSize;
                HorizontalFirst = thisfooter.HorizontalFirst;
            }
        }
        public class DocumentInfo
        {
            public String PdfFileName;
            public String SrcFileName;
            public List<FooterInfo> footerInfo;
            public int versionNumber;
            public int printFromPageNumber;
            public int printToPageNumber;
            public int printerCopies;
            public bool printerLandscape;
            public int printerDuplex;
            public string printerPaperSize;
            public string printerPaperSource;
            public Boolean isCollete = true;
        }

        class printOutput
        {
            public int page;
            public int copy;
            public int horzsplit;
            public int vertsplit;
            public int horzindex;
            public int vertindex;
            public int iPageBreakIndex;
            public bool landscape;
            public RectangleF printerbound;
            public RectangleF footerbound;
            public RectangleF sourcebound;
            public bool toClip = false;
            public Page thisPage;
            public FooterInfo footer;
            public float footerfontheight;
            public float pageWidth;
            public float pageHeight;
            public bool reCalcSourceBound = false;
            public void Copy(printOutput thisoutput)
            {
                page = thisoutput.page;
                copy = thisoutput.copy;
                horzsplit = thisoutput.horzsplit;
                vertsplit = thisoutput.vertsplit;
                horzindex = thisoutput.horzindex;
                vertindex = thisoutput.vertindex;
                landscape = thisoutput.landscape;
                printerbound = thisoutput.printerbound;
                footerbound = thisoutput.footerbound;
                sourcebound = thisoutput.sourcebound;
                toClip = thisoutput.toClip;
                thisPage = thisoutput.thisPage;
                footer = thisoutput.footer;
                footerfontheight = thisoutput.footerfontheight;
                iPageBreakIndex = thisoutput.iPageBreakIndex;
                pageWidth = thisoutput.pageWidth;
                pageHeight = thisoutput.pageHeight;
                reCalcSourceBound = thisoutput.reCalcSourceBound;
            }
            public void CopyExceptPage(printOutput thisoutput)
            {
                //page = thisoutput.page;
                copy = thisoutput.copy;
                horzsplit = thisoutput.horzsplit;
                vertsplit = thisoutput.vertsplit;
                horzindex = thisoutput.horzindex;
                vertindex = thisoutput.vertindex;
                landscape = thisoutput.landscape;
                printerbound = thisoutput.printerbound;
                footerbound = thisoutput.footerbound;
                sourcebound = thisoutput.sourcebound;
                toClip = thisoutput.toClip;
                //thisPage = thisoutput.thisPage;
                //footer = thisoutput.footer;
                footerfontheight = thisoutput.footerfontheight;
                iPageBreakIndex = thisoutput.iPageBreakIndex;
                //pageWidth = thisoutput.pageWidth;
                //pageHeight = thisoutput.pageHeight;
                reCalcSourceBound = thisoutput.reCalcSourceBound;
            }
            public void CopyOnlyNeeded(printOutput thisoutput)
            {
                page = thisoutput.page;
                //  copy = thisoutput.copy;
                horzsplit = thisoutput.horzsplit;
                vertsplit = thisoutput.vertsplit;
                //  horzindex = thisoutput.horzindex;
                //  vertindex = thisoutput.vertindex;
                landscape = thisoutput.landscape;
                //  printerbound = thisoutput.printerbound;
                //  footerbound = thisoutput.footerbound;
                //  thisPage = thisoutput.thisPage;
                footer = thisoutput.footer;
                footerfontheight = thisoutput.footerfontheight;
                //iPageBreakIndex = thisoutput.iPageBreakIndex;
                //pageWidth = thisoutput.pageWidth;
                //pageHeight = thisoutput.pageHeight;
                reCalcSourceBound = thisoutput.reCalcSourceBound;
            }
        }


        public void PopulatePrinterPageArrayWithOrWithoutBackground()
        {
            try
            {
                if (_ExtendedPrinterSettings.IsBackGroundPrint)
                {

                    methodToInvokeCalculation = new EventHandler(this.OnPopulatePrinterPageArray);
                    methodToInvokeCalculation.BeginInvoke(this, null, new AsyncCallback(this.OnPopulatePrinterPageArrayComplete), null);

                }
                else
                {
                    PopulatePrinterPageArray();
                    try
                    {
                        if (OnMeasurementsCalculated != null)
                        {
                            OnMeasurementsCalculated(this, _gloPrintProgress);
                        }
                    }
                    catch
                    {
                    }

                    PrintWithOrWithoutBackground();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        public void CopyOrPrint()
        {
            if (gloGlobal.gloTSPrint.isCopyPrint && NoTSPrint == false)
            {
                try
                {
                    lblPrinterName.Text = "Copying To :";
                    lblPrinterNameValue.Text = " Mapped drive for printing";
                    lblPrinterName.Update();
                    lblPrinterNameValue.Update();
                }
                catch { }
                
                if (showTSPrinterSelection)
                {
                    if (_isTSPrinterSelectionOpen)
                    {
                        isTSPrintCancelled = true;
                        InvokeCloseControls();
                        return;
                    }
                    _isTSPrinterSelectionOpen = true;
                    frmTSPrintDialog tsPrintDialog = new frmTSPrintDialog();
                    Form parentForm = null;
                    try
                    {
                        parentForm = ActiveForm;
                    }
                    catch
                    {
                    }
                    //SLR: Bug #100306: TS Print>>TSprint popup goes to backside of application after changing default printer setting
                    tsPrintDialog.TopMost = true;
                    tsPrintDialog.bIsShowExtended = bIsFromScanDoc;
                    if (parentForm == null)
                    {
                        tsPrintDialog.ShowDialog();
                    }
                    else
                    {
                        tsPrintDialog.ShowDialog(parentForm);
                    }
                    _isTSPrinterSelectionOpen = false;
                    if (tsPrintDialog.cancelPirnt == true)
                    {
                        isTSPrintCancelled = true;
                        InvokeCloseControls();
                        return;
                    }
                    popUpDetails = new gloClinicalQueueGeneral.QueueDocumentDocumentDetails();
                    popUpDetails.PrintFrom = tsPrintDialog.pageFrom;
                    popUpDetails.PrintTo = tsPrintDialog.pageTo;
                    popUpDetails.Printer = tsPrintDialog.currPrinterFile;
                    popUpDetails.Copies = tsPrintDialog.NoOfCopies;
                    popUpDetails.Landscape = tsPrintDialog.isLandscape;
                    popUpDetails.Duplex = tsPrintDialog.duplex;
                    popUpDetails.Size = tsPrintDialog.currSize;
                    popUpDetails.Tray = tsPrintDialog.currTray;
                    popUpDetails.isCollete = tsPrintDialog.isCollete;
                }
                else
                {
                    popUpDetails = null;
                }
                CopyToPrint();
            }
            else
            {
                PopulatePrinterPageArrayWithOrWithoutBackground();
            }
        }

        public void CopyToPrint()
        {
            try
            {
                if (_ExtendedPrinterSettings.IsBackGroundPrint)
                {

                    methodToInvokeCopy = new EventHandler(this.OnCopyPrintDoc);
                    methodToInvokeCopy.BeginInvoke(this, null, new AsyncCallback(this.OnCopyPrintDocComplete), null);
                }
                else
                {
                    //PopulatePrinterPageArray();
                    //try
                    //{
                    //    if (OnMeasurementsCalculated != null)
                    //    {
                    //        OnMeasurementsCalculated(this, _gloPrintProgress);
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    CopyPrintDoc();
                    //finalize();
                    CloseCompleteControls();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        /// <summary>
        /// Do actual calculating job.
        /// </summary>
        /// <param name="sender">source control</param>
        /// <param name="e">calculate job parameter</param>
        private void OnPopulatePrinterPageArray(object sender, System.EventArgs e)
        {
            try
            {
                PopulatePrinterPageArray();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        /// <summary>
        /// background calculation complete handler
        /// </summary>
        private void OnPopulatePrinterPageArrayComplete(IAsyncResult iar)
        {

            //try
            //{

            //    try
            //    {
            //        if (!IsPrintingCanceled)
            //        {
            //            InvokeCompleteUpdateCalculations();
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            //    ex = null;
            //}
            //finally
            //{
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
            try
            {
                if (OnMeasurementsCalculated != null)
                {
                    OnMeasurementsCalculated(this, _gloPrintProgress);
                }
            }
            catch
            {
            }
            PrintWithOrWithoutBackground();
            //}
        }

        /// <summary>
        /// Call copy funtion in background
        /// </summary>
        /// <param name="sender">source control</param>
        /// <param name="e">copy event parameter</param>
        private void OnCopyPrintDoc(object sender, System.EventArgs e)
        {
            try
            {
                CopyPrintDoc();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        /// <summary>
        /// background Copy event complete handler
        /// </summary>
        private void OnCopyPrintDocComplete(IAsyncResult iar)
        {
            gloAuditTrail.gloAuditTrail.PrintLog(strException: "On Copy Doc event completed", ShowMessageBox: false);
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
            //finalize();
            InvokeCloseControls();
        }
        private static int myCounter = 0;
        public void CopyPrintDoc()
        {
            try
            {
                if (((_useEMFForSSRS || _isPDRPrinting) && _lstDocuments != null) || (_PDFFileName != null && _PDFFileName.Trim() != ""))
                {
                    List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> SplitDocList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
                    if (_useEMFForSSRS || _isPDRPrinting)
                    {
                        for (int i = 0; i < _lstDocuments.Count; i++)
                        {
                            gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                            physicalDoc.PdfFileName = _lstDocuments[i].PdfFileName;
                            physicalDoc.SrcFileName = _lstDocuments[i].SrcFileName;
                            physicalDoc.footerInfo = null;
                            SplitDocList.Add(physicalDoc);
                        }
                    }
                    else
                    {
                        string newPdfFileName = _PDFFileName;
                        if (_oSourceDocSelectedPages != null)
                        {
                            newPdfFileName = gloRecoverPDF.SelectedPagesToPDF(_PDFFileName, _oSourceDocSelectedPages);
                        }

                        //Split large file as per setting


                        if (gloGlobal.gloTSPrint.NoOfPages > 0)
                        {
                            gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo footer = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo();
                            List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo> footerList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo>();
                            footer.FromPage = 0;
                            footer.ToPage = 0;
                            footerList.Add(footer);

                            SplitDocList = gloRecoverPDF.SplitPDFToMaxNoOfPages(newPdfFileName, gloGlobal.gloTSPrint.NoOfPages, null, footerList);
                        }
                        else
                        {
                            gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                            physicalDoc.PdfFileName = newPdfFileName;
                            physicalDoc.SrcFileName = newPdfFileName;
                            physicalDoc.footerInfo = null;
                            SplitDocList.Add(physicalDoc);
                        }
                    }

                    myCounter++;
                    if (myCounter == 1000)
                    { myCounter = 0; }
                    String strMetaDataFilePath = "";
                    Boolean MetaDataGenerated = false;
                    if (!gloGlobal.gloTSPrint.UseZippedMetadata)
                    {
                        strMetaDataFilePath = gloGlobal.gloTSPrint.TempPath + "01" + DateTime.Now.ToString("_MMddyyyy_hhmmsstt") + myCounter.ToString("D3") + ".xml";
                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _blnautoLandScape, popUpDetails: popUpDetails, bUseFileZip: false);
                    }
                    
                    String PDFWithoutPath = "";
                    bool First = true;
                    for (int fileCntr = 0; fileCntr < SplitDocList.Count; fileCntr++)
                    {
                        PDFWithoutPath = SplitDocList[fileCntr].PdfFileName.Substring(SplitDocList[fileCntr].PdfFileName.LastIndexOf("\\") + 1);
                        gloGlobal.gloTSPrint.CopyFileToNetworkShare(SplitDocList[fileCntr].PdfFileName, gloGlobal.gloTSPrint.AppFolderPath + "\\" + PDFWithoutPath);
                        if (First)
                        {
                            if (!gloGlobal.gloTSPrint.UseZippedMetadata)
                            {
                                gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\\") + 1));
                            }
                            else
                            {
                                strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath + "\\01" + DateTime.Now.ToString("_MMddyyyy_hhmmsstt") + myCounter.ToString("D3") + ".xmz";
                                MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _blnautoLandScape, popUpDetails: popUpDetails, bUseFileZip: true);
                            }

                            First = false;
                        }
                    }

                    gloAuditTrail.gloAuditTrail.PrintLog(strException: "PDF and MetaData files copied to virtual drive for Word Printing.", ShowMessageBox: false);

                    if (OnThreadCompleted != null)
                    {
                        gloResult ResultsSs = new gloResult();
                        ResultsSs.ResultMethod = "OnCopyPrintDoc";
                        ResultsSs.ResultType = 0;
                        ResultsSs.ResultObject = "Success";
                        _gloPrintProgress.Result.Add(ResultsSs);
                    }

                }
            }
            catch (Exception ex)
            {
                _gloPrintProgress.success = false;

                if (OnThreadCompleted != null)
                {
                    gloResult ResultsSs = new gloResult();
                    ResultsSs.ResultMethod = "OnCopyPrintDoc";
                    ResultsSs.ResultType = 1;
                    ResultsSs.ResultObject = ex;
                    _gloPrintProgress.Result.Add(ResultsSs);
                }

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        public void finalize()
        {
            methodToInvokeCopy = null;
            try
            {


            }
            catch
            {

            }
            finally
            {
                try
                {
                    if (OnThreadCompleted != null)
                    {
                        OnThreadCompleted(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
                if (gloGlobal.clsMISC.IsService || (_ExtendedPrinterSettings != null && !_ExtendedPrinterSettings.IsShowProgress))
                {
                    this.Dispose(true);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private bool GenerateMetaDataFile(string strFilePath, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> PhysicalFileDocs, Boolean autoLandScape = true, gloClinicalQueueGeneral.QueueDocumentDocumentDetails popUpDetails = null, Boolean bUseFileZip = false)
        {
            gloClinicalQueueGeneral.gloQueueMetadatawriter _QueueWriter = new gloClinicalQueueGeneral.gloQueueMetadatawriter();
            gloClinicalQueueGeneral.Queue QueueDoc = null;
            try
            {
                //Dim strFilePath As String = GenerateClinicalChartFileName(ds, 0, True)

                QueueDoc = _QueueWriter.GenerateWordMetaDataFile(gloGlobal.gloTSPrint.PatientName, gloGlobal.gloTSPrint.PatientDOB, false, PhysicalFileDocs, strFilePath.Substring(strFilePath.LastIndexOf("\\") + 1), autoLandScape, popUpDetails: popUpDetails);
                try
                {
                    gloQueueSchema.gloSerialization.SetClinicalDocument(strFilePath, QueueDoc, bUseFileZip);
                    return true;
                }
                catch (Exception ex)
                {
                    _gloPrintProgress.success = false;

                    if (OnThreadCompleted != null)
                    {
                        gloResult ResultsSs = new gloResult();
                        ResultsSs.ResultMethod = "OnSetClinicalDocument";
                        ResultsSs.ResultType = 1;
                        ResultsSs.ResultObject = ex;
                        _gloPrintProgress.Result.Add(ResultsSs);
                    }

                    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                    ex = null;
                    return false;
                }
            }
            catch (Exception ex)
            {

                _gloPrintProgress.success = false;

                if (OnThreadCompleted != null)
                {
                    gloResult ResultsSs = new gloResult();
                    ResultsSs.ResultMethod = "OnGenerateMetaDataFile";
                    ResultsSs.ResultType = 1;
                    ResultsSs.ResultObject = ex;
                    _gloPrintProgress.Result.Add(ResultsSs);
                }

                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
                return false;
            }
            finally
            {
                if ((_QueueWriter != null))
                {
                    _QueueWriter.Dispose();
                    _QueueWriter = null;
                }
                if ((QueueDoc != null))
                {
                    QueueDoc = null;
                }
            }

        }


        public delegate void UpdateBeginCalculationsDelegate();
        public void InvokeBeginUpdateCalculations()
        {
            try
            {
                if (gloGlobal.clsMISC.IsService)
                {
                    UpdateBeginCalculations();
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateBeginCalculationsDelegate(UpdateBeginCalculations));
                    }
                    else
                    {
                        UpdateBeginCalculations();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        private void UpdateBeginCalculations()
        {
            try
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    lblPrinterNameValue.Text = _PrinterSetting.PrinterName;
                    btnPause.Enabled = false;
                    gbPrintingBox.Text = "Calculating";
                    lblPageNoOfDocument.Text = "Document" + (_lstDocuments == null ? "" : " " + (_gloPrintProgress.Progress._DocumentIndex + 1).ToString() + " of " + _lstDocuments.Count.ToString());

                    lblPrinterNameValue.Update();
                    lblPageNoOfDocument.Update();
                    btnPause.Update();
                    gbPrintingBox.Update();
                    lblPrinterNameValue.ResumeLayout();
                    btnPause.ResumeLayout();
                    gbPrintingBox.ResumeLayout();

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        public delegate void UpdateCompleteCalculationsDelegate();
        public void InvokeCompleteUpdateCalculations()
        {
            try
            {
                if (gloGlobal.clsMISC.IsService)
                {
                    UpdateCompleteCalculations();
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateCompleteCalculationsDelegate(UpdateCompleteCalculations));
                    }
                    else
                    {
                        UpdateCompleteCalculations();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        private void UpdateCompleteCalculations()
        {
            try
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    btnPause.Enabled = true;
                    gbPrintingBox.Text = "Printing";
                    btnPause.Update();
                    gbPrintingBox.Update();
                    btnPause.ResumeLayout();
                    gbPrintingBox.ResumeLayout();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }


        List<printOutput> PageArray = new List<printOutput> { };
        private short _PrintOriginalCopies = 1;
        private int _SplitPageCount = 1;

        private void PopulatePrinterPageArray()
        {

            try
            {
                try
                {
                    if (OnThreadInitiated != null)
                    {
                        OnThreadInitiated(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
                try
                {
                    InvokeBeginUpdateCalculations();
                }
                catch (Exception)
                {
                }
                PageArray.Clear();
                try
                {
                    int maxPage = 1;
                    if (_PDFFile)
                    {
                        if (_PDFDoc != null)
                        {
                            maxPage = _PDFDoc.GetPageCount();
                        }
                    }
                    else
                    {
                        maxPage = myEmfImage.Count;
                    }
                    _SplitPageCount = maxPage;
                    bool collate = _PrinterSetting.Collate;
                    _PrintOriginalCopies = _PrinterSetting.Copies;
                    int copies = _PrinterSetting.Copies;
                    int from = (_oSourceDocSelectedPages == null) ? (_PrinterSetting.PrintRange == PrintRange.AllPages ? 1 : _PrinterSetting.FromPage) : 1;
                    int to = (_oSourceDocSelectedPages == null) ? (_PrinterSetting.PrintRange == PrintRange.AllPages ? maxPage : _PrinterSetting.ToPage) : _oSourceDocSelectedPages.Count;
                    if (from < 1) from = 1;
                    if (to > maxPage) to = maxPage;
                    if (_PrinterSetting != null)
                    {
                        try
                        {
                            _oPrintDocument.PrinterSettings = _PrinterSetting;

                        }
                        catch
                        {
                            _oPrintDocument.PrinterSettings.PrinterName = _PrinterSetting.PrinterName;
                        }
                        try
                        {
                            _oPrintDocument.DefaultPageSettings = _PrinterSetting.DefaultPageSettings;
                        }
                        catch
                        {
                        }

                    }

                    try
                    {
                        _printLandscapeMode = _oPrintDocument.DefaultPageSettings.Landscape;
                    }
                    catch (Exception)
                    {
                    }

                    if (_ExtendedPrinterSettings.FooterFont == null)
                    {
                        _ExtendedPrinterSettings.FooterFont = System.Drawing.SystemFonts.CaptionFont;
                    }
                    if (_ExtendedPrinterSettingsFooterFontBy != null)
                    {
                        for (int scaling = 1; scaling <= 3; scaling++)
                        {
                            if (_ExtendedPrinterSettingsFooterFontBy[scaling - 1] != null)
                            {
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1].Dispose();
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1] = null;
                            }
                            _ExtendedPrinterSettingsFooterFontBy[scaling - 1] = new System.Drawing.Font(_ExtendedPrinterSettings.FooterFont.FontFamily, _ExtendedPrinterSettings.FooterFont.Size / ((float)scaling), _ExtendedPrinterSettings.FooterFont.Style);
                        }
                    }
                    List<printOutput> PageParameters = new List<printOutput>();
                    if ((_ExtendedPrinterSettings.NormalSettings == null) || (_ExtendedPrinterSettings.NormalSettings.bValuesAssigned == false))
                    {
                        _ExtendedPrinterSettings.NormalSettings = _ExtendedPrinterSettings.GetGraphicsBound(_PrinterSetting, _ExtendedPrinterSettingsFooterFontBy);
                    }
                    bool thisLandscape = _PrinterSetting.DefaultPageSettings.Landscape;
                    _PrinterSetting.DefaultPageSettings.Landscape = false;
                    if (thisLandscape != _PrinterSetting.DefaultPageSettings.Landscape)
                    {
                        if ((_ExtendedPrinterSettings.FlatSettings == null) || (_ExtendedPrinterSettings.FlatSettings.bValuesAssigned == false))
                        {
                            _ExtendedPrinterSettings.FlatSettings = _ExtendedPrinterSettings.GetGraphicsBound(_PrinterSetting, _ExtendedPrinterSettingsFooterFontBy);
                        }
                    }
                    else
                    {
                        _ExtendedPrinterSettings.FlatSettings = _ExtendedPrinterSettings.NormalSettings;
                    }
                    if (!_ExtendedPrinterSettings.IsActualLandscape)
                    {
                        _PrinterSetting.DefaultPageSettings.Landscape = thisLandscape;
                    }
                    prevPageHeight = 0;
                    prevPageWidth = 0;
                    prevFooterFound = false;
                    bReCalcmeasureBounds = true;
                    // _printactualTotalPages = 0;

                    for (int iPage = from; iPage <= to; )
                    {
                        printOutput thisPageOutput = _oPrintDocument_MeasurePageParameters(iPage, to);
                        PageParameters.Add(thisPageOutput);
                        iPage += thisPageOutput.iPageBreakIndex;
                    }
                    if (!collate)
                    {
                        int nCount = -1;
                        for (int iPage = from; iPage <= to; )
                        {
                            nCount++;
                            printOutput pageParameter = PageParameters[nCount];
                            gloExtendedPrinterSettings.PageSize CurrentPageSize = _ExtendedPrinterSettings.CurrentPageSize;
                            bool CurrentHorizontalFirst = _ExtendedPrinterSettings.IsHorizontalFlow;
                            if (pageParameter.footer != null)
                            {
                                CurrentPageSize = pageParameter.footer.CurrentPageSize;
                                CurrentHorizontalFirst = pageParameter.footer.HorizontalFirst;
                            }
                            //float pageWidth = 0;
                            //float pageHeight = 0;
                            if (_PDFFile)
                            {
                                pageParameter.pageWidth = (float)pageParameter.thisPage.GetPageWidth() - 1;
                                pageParameter.pageHeight = (float)pageParameter.thisPage.GetPageHeight() - 1;
                            }
                            else
                            {
                                System.Drawing.Image myMetaImage = myEmfImage[pageParameter.page - 1];
                                //pageParameter.pageWidth = myMetaImage.Width / myMetaImage.HorizontalResolution * 72;
                                //pageParameter.pageHeight = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                                AssignWidthAndHeight(ref pageParameter, myMetaImage);
                            }
                            pageParameter.sourcebound = new RectangleF(0, 0, pageParameter.pageWidth, pageParameter.pageHeight);
                            int prevCount = PageArray.Count();
                            int curCount = PageArray.Count();
                            for (int icopies = 1; icopies <= copies; icopies++)
                            {
                                if (icopies == 1)
                                {
                                    PopulateRectangleCoOrdinatesForPages(to, icopies, iPage, pageParameter, CurrentPageSize, CurrentHorizontalFirst);
                                    curCount = PageArray.Count();
                                }
                                else
                                {
                                    for (int sCount = prevCount; sCount < curCount; sCount++)
                                    {
                                        PageArray.Add(PageArray[sCount]);
                                    }
                                }

                            }
                            iPage += pageParameter.iPageBreakIndex;
                        }
                    }
                    else
                    {

                        int prevCount = PageArray.Count();
                        int curCount = PageArray.Count();

                        for (int icopies = 1; icopies <= copies; icopies++)
                        {
                            if (icopies == 1)
                            {

                                int nCount = -1;
                                for (int iPage = from; iPage <= to; )
                                {
                                    nCount++;
                                    printOutput pageParameter = PageParameters[nCount];
                                    gloExtendedPrinterSettings.PageSize CurrentPageSize = _ExtendedPrinterSettings.CurrentPageSize;
                                    bool CurrentHorizontalFirst = _ExtendedPrinterSettings.IsHorizontalFlow;
                                    if (pageParameter.footer != null)
                                    {
                                        CurrentPageSize = pageParameter.footer.CurrentPageSize;
                                        CurrentHorizontalFirst = pageParameter.footer.HorizontalFirst;
                                    }

                                    //float pageWidth = 0;
                                    //float pageHeight = 0;
                                    if (_PDFFile)
                                    {
                                        pageParameter.pageWidth = (float)pageParameter.thisPage.GetPageWidth() - 1;
                                        pageParameter.pageHeight = (float)pageParameter.thisPage.GetPageHeight() - 1;
                                    }
                                    else
                                    {
                                        System.Drawing.Image myMetaImage = myEmfImage[pageParameter.page - 1];
                                        //pageParameter.pageWidth = myMetaImage.Width / myMetaImage.HorizontalResolution * 72;
                                        //pageParameter.pageHeight = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                                        AssignWidthAndHeight(ref pageParameter, myMetaImage);
                                    }
                                    pageParameter.sourcebound = new RectangleF(0, 0, pageParameter.pageWidth, pageParameter.pageHeight);
                                    PopulateRectangleCoOrdinatesForPages(to, icopies, iPage, pageParameter, CurrentPageSize, CurrentHorizontalFirst);
                                    iPage += pageParameter.iPageBreakIndex;
                                }
                                curCount = PageArray.Count();
                            }
                            else
                            {
                                for (int sCount = prevCount; sCount < curCount; sCount++)
                                {
                                    PageArray.Add(PageArray[sCount]);
                                }
                            }

                        }



                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    _PrinterSetting.Copies = 1;
                    if (_ExtendedPrinterSettings.IsActualLandscape)
                    {
                        _oPrintDocument.DefaultPageSettings.Landscape = false;
                    }

                    _gloPrintProgress.Progress._PrintPageTotal = PageArray.Count;
                    gloResult Result = new gloResult();
                    Result.ResultMethod = "MeasureGraphics";
                    Result.ResultType = 0;
                    Result.ResultObject = "Success";
                    _gloPrintProgress.Result.Add(Result);

                }
                catch (Exception)
                {

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                _gloPrintProgress.success = false;
                gloResult Result = new gloResult();
                Result.ResultMethod = "MeasureGraphics";
                Result.ResultType = 1;
                Result.ResultObject = ex;
                _gloPrintProgress.Result.Add(Result);
                ex = null;
            }
            finally
            {
                InvokeCompleteUpdateCalculations();
            }

        }

        private int PopulateRectangleCoOrdinatesForPages(int to, int icopies, int iPage, printOutput pageParameter, gloExtendedPrinterSettings.PageSize CurrentPageSize, bool CurrentHorizontalFirst)
        {
            int spages = 0;
            if (CurrentHorizontalFirst)
            {
                for (int vpages = 0; vpages < pageParameter.vertsplit; vpages++)
                {
                    for (int hpages = 0; hpages < pageParameter.horzsplit; hpages++)
                    {

                        spages = PopulateRectangleCoOrdinatesForEachPage(to, icopies, iPage, pageParameter, CurrentPageSize, spages, hpages, vpages);
                    }
                }
            }
            else
            {
                for (int hpages = 0; hpages < pageParameter.horzsplit; hpages++)
                {
                    for (int vpages = 0; vpages < pageParameter.vertsplit; vpages++)
                    {
                        spages = PopulateRectangleCoOrdinatesForEachPage(to, icopies, iPage, pageParameter, CurrentPageSize, spages, hpages, vpages);
                    }
                }
            }
            return spages;
        }
        private bool _isClippingSupported = true;
        private int PopulateRectangleCoOrdinatesForEachPage(int to, int icopies, int iPage, printOutput pageParameter, gloExtendedPrinterSettings.PageSize CurrentPageSize, int spages, int hpages, int vpages)
        {
            printOutput ostruct = new printOutput();
            if (spages == 0)
            {
                ostruct.Copy(pageParameter);
                ostruct.copy = icopies;
                if (CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
                {
                    ostruct.iPageBreakIndex = 1;
                }
                else
                {
                    ostruct.iPageBreakIndex = pageParameter.horzsplit * pageParameter.vertsplit;
                }
            }
            else
            {
                ostruct.CopyOnlyNeeded(pageParameter);
                ostruct.copy = icopies;
                if ((iPage + spages) <= to)
                {
                    if (spages < pageParameter.iPageBreakIndex)
                    {
                        ostruct.page = (_oSourceDocSelectedPages == null) ? (iPage + spages) : System.Convert.ToInt32(_oSourceDocSelectedPages[iPage + spages - 1]);
                        if (_PDFFile)
                        {
                            ostruct.thisPage = _PDFDoc.GetPage(ostruct.page);
                            ostruct.pageWidth = (float)ostruct.thisPage.GetPageWidth() - 1;
                            ostruct.pageHeight = (float)ostruct.thisPage.GetPageHeight() - 1;
                        }
                        else
                        {
                            System.Drawing.Image myMetaImage = myEmfImage[pageParameter.page - 1];
                            //ostruct.pageWidth = myMetaImage.Width / myMetaImage.HorizontalResolution * 72;
                            //ostruct.pageHeight = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                            AssignWidthAndHeight(ref ostruct, myMetaImage);
                        }
                        ostruct.sourcebound = new RectangleF(0, 0, ostruct.pageWidth, ostruct.pageHeight);
                    }
                    else
                    {
                        ostruct.page = -1;
                    }
                }
                else
                {
                    ostruct.page = -1;
                }
            }
            if (CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
            {
                ostruct.horzindex = hpages;
                ostruct.vertindex = vpages;
                float Left = 0;
                float Top = 0;
                if (_isClippingSupported)
                {
                    Left = (hpages * (pageParameter.printerbound.Width - _ExtendedPrinterSettings.HorizontalOverlap));
                    Top = (vpages * (pageParameter.printerbound.Height - _ExtendedPrinterSettings.VerticalOverlap - pageParameter.footerfontheight));
                    float width = ostruct.pageWidth; //pageParameter.printerbound.Width; // +_ExtendedPrinterSettings.HorizontalOverlap;  
                    float height = ostruct.pageHeight;// pageParameter.printerbound.Height; // +_ExtendedPrinterSettings.VerticalOverlap + pageParameter.footerfontheight;
                    //                    if (!_PDFFile)
                    {
                        if (pageParameter.landscape)
                        {
                            width = Math.Min(width, pageParameter.printerbound.Width + _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin);
                            height = Math.Min(height, pageParameter.printerbound.Height + _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin);
                        }
                        else
                        {
                            width = Math.Min(width, pageParameter.printerbound.Width + _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin);
                            height = Math.Min(height, pageParameter.printerbound.Height + _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin);
                        }
                        ostruct.printerbound = new RectangleF(pageParameter.printerbound.X, pageParameter.printerbound.Y, width, height);
                        ostruct.sourcebound = new RectangleF(Left, Top, width, height);
                        ostruct.toClip = true;
                    }
                    //else
                    //{
                    //    width = pageParameter.printerbound.Width; // +_ExtendedPrinterSettings.HorizontalOverlap;  
                    //    height = pageParameter.printerbound.Height; // +_ExtendedPrinterSettings.VerticalOverlap + pageParameter.footerfontheight;
                    //    if ( (hpages > 0) || (vpages > 0) )
                    //    {
                    //        ostruct.printerbound = new RectangleF(pageParameter.printerbound.X, pageParameter.printerbound.Y, width, height);
                    //        if (pageParameter.landscape)
                    //        {
                    //            width += _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin;
                    //            height += _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin;
                    //        }
                    //        else
                    //        {
                    //            width += _ExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin;
                    //            height += _ExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin;
                    //        }
                    //        ostruct.sourcebound = new RectangleF(Left, Top, width, height);
                    //        ostruct.toClip = true;
                    //    }
                    //    else
                    //    {
                    //        Left = pageParameter.printerbound.Left - (hpages * (pageParameter.printerbound.Width - _ExtendedPrinterSettings.HorizontalOverlap));
                    //        Top = pageParameter.printerbound.Top - (vpages * (pageParameter.printerbound.Height - _ExtendedPrinterSettings.VerticalOverlap - pageParameter.footerfontheight));
                    //        ostruct.printerbound = new RectangleF(Left, Top, ostruct.pageWidth, ostruct.pageHeight);
                    //    }
                    //}
                }
                else
                {
                    Left = pageParameter.printerbound.Left - (hpages * (pageParameter.printerbound.Width - _ExtendedPrinterSettings.HorizontalOverlap));
                    Top = pageParameter.printerbound.Top - (vpages * (pageParameter.printerbound.Height - _ExtendedPrinterSettings.VerticalOverlap - pageParameter.footerfontheight));
                    ostruct.printerbound = new RectangleF(Left, Top, ostruct.pageWidth, ostruct.pageHeight);
                }
                ostruct.footer = GetFooter(_PDFFooter, ostruct.page, hpages, vpages, pageParameter.horzsplit, pageParameter.vertsplit);
                if (ostruct.footerfontheight != 0)
                {
                    Left = pageParameter.footerbound.X;// -pageParameter.printerbound.X + ostruct.printerbound.X;
                    Top = pageParameter.footerbound.Y;// -pageParameter.printerbound.Y + ostruct.printerbound.Y;
                    ostruct.footerbound = new RectangleF(Left, Top, pageParameter.footerbound.Width, pageParameter.footerbound.Height);
                }

            }
            else
            {

                float Left = pageParameter.printerbound.Left + ((pageParameter.printerbound.Width + _ExtendedPrinterSettings.HorizontalGutter) * hpages);
                float Top = pageParameter.printerbound.Top + ((pageParameter.printerbound.Height + (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight)) * vpages);
                if (pageParameter.landscape)
                {
                    ostruct.printerbound = new RectangleF(Left, Top, pageParameter.printerbound.Width + (_ExtendedPrinterSettings.AdjustFitToPageVerticalPageHeightMargin / pageParameter.horzsplit), pageParameter.printerbound.Height + (_ExtendedPrinterSettings.AdjustFitToPageHorizontalPageWidthMargin / pageParameter.vertsplit));
                }
                else
                {
                    ostruct.printerbound = new RectangleF(Left, Top, pageParameter.printerbound.Width + (_ExtendedPrinterSettings.AdjustFitToPageHorizontalPageWidthMargin / pageParameter.horzsplit), pageParameter.printerbound.Height + (_ExtendedPrinterSettings.AdjustFitToPageVerticalPageHeightMargin / pageParameter.vertsplit));
                }
                ostruct.footer = GetFooter(_PDFFooter, ostruct.page);
                if (ostruct.footerfontheight != 0)
                {
                    Left = pageParameter.footerbound.X - pageParameter.printerbound.X + ostruct.printerbound.X;
                    Top = pageParameter.footerbound.Y - pageParameter.printerbound.Y + ostruct.printerbound.Y;
                    ostruct.footerbound = new RectangleF(Left, Top, pageParameter.footerbound.Width, pageParameter.footerbound.Height);
                }
                if (!_PDFFile)
                {

                    float widthScale = ostruct.printerbound.Width / ostruct.pageWidth;
                    float heightScale = ostruct.printerbound.Height / ostruct.pageHeight;

                    if (widthScale < heightScale)
                    {
                        float newHeight = ostruct.pageHeight * widthScale;
                        ostruct.printerbound.Y += (ostruct.printerbound.Height - newHeight) / 2;
                        ostruct.printerbound.Height = newHeight;
                    }
                    else
                    {
                        float newWidth = ostruct.pageWidth * heightScale;
                        ostruct.printerbound.X += (ostruct.printerbound.Width - newWidth) / 2;
                        ostruct.printerbound.Width = newWidth;
                    }

                }

                spages++;
            }
            PageArray.Add(ostruct);
            _isClippingSupported = true;
            return spages;
        }

        private void Print()
        {
            bool beforePrintStart = true;

            try
            {
                bool noDocuments = false;
                while ((_gloPrintProgress.Progress._printPageIndex >= PageArray.Count))
                {
                    if (!BringNextDocuments())
                    {
                        noDocuments = true;
                        break;
                    }
                }
                if (noDocuments)
                {
                    CompleteTheThreadAndExit();
                    return;
                }
                //if (_PrintRect != null)
                //{
                //    _PrintRect.Dispose();
                //    _PrintRect = null;
                //}
                if (_oPrintDocument == null)
                {
                    CompleteTheThreadAndExit();
                    //_gloPrintProgress.success = false;
                    //gloResult Results = new gloResult();
                    //Results.ResultMethod = "Print";
                    //Results.ResultType = 1;
                    //Results.ResultObject = "Failed";
                    //_gloPrintProgress.Result.Add(Results);
                    return;
                }
                if (_PDFFile)
                {
                    _PdfDraw = new pdftron.PDF.PDFDraw();
                }

                _oPrintDocument.DocumentName = _PDFFileName;
                _gloStandardPrintController = new gloStandardPrintController();
                _oPrintDocument.PrintController = _gloStandardPrintController;
                if (OnPrintThreadInitiated != null)
                {
                    gloResult Results = new gloResult();
                    Results.ResultMethod = "Print";
                    Results.ResultType = 0;
                    Results.ResultObject = "Success";
                    _gloPrintProgress.Result.Add(Results);
                }
                try
                {
                    if (OnPrintThreadInitiated != null)
                    {
                        OnPrintThreadInitiated(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
                beforePrintStart = false;
                _oPrintDocument.Print();
                if (_PDFFile)
                {
                    if (_PdfDraw != null)
                    {
                        _PdfDraw.Dispose();
                        _PdfDraw = null;
                    }
                }
                //if (_PrintRect != null)
                //{
                //    _PrintRect.Dispose();
                //    _PrintRect = null;
                //}
                if (_gloStandardPrintController != null)
                {
                    _gloStandardPrintController = null;
                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();

                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    if (!gloGlobal.clsMISC.IsService)
                    {
                        MessageBox.Show(_MessageString);
                    }
                    _MessageString = "";
                }
                #endregion " Make Log Entry "


                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                //ex = null;
                _gloPrintProgress.success = false;
                if (OnPrintThreadInitiated != null)
                {
                    gloResult Results = new gloResult();
                    Results.ResultMethod = "Print";
                    Results.ResultType = 1;
                    Results.ResultObject = ex;
                    _gloPrintProgress.Result.Add(Results);
                }
                _ErrorMessage = string.Empty;
                ex = null;

            }
            finally
            {
                if (_PDFFile)
                {
                    if (_PdfDraw != null)
                    {
                        _PdfDraw.Dispose();
                        _PdfDraw = null;
                    }
                }
                //if (_PrintRect != null)
                //{
                //    _PrintRect.Dispose();
                //    _PrintRect = null;
                //}
                try
                {
                    if (beforePrintStart)
                    {
                        if (OnPrintThreadInitiated != null)
                        {
                            OnPrintThreadInitiated(this, _gloPrintProgress);
                        }
                    }
                }
                catch
                {
                }
            }

        }

        private void CompleteTheThreadAndExit()
        {
            ClearMemory();

            //if (_ExtendedPrinterSettings.IsBackGroundPrint)
            //{
            //    this.Dispose(true);
            //}
            _gloPrintProgress.success = false;
            if (OnThreadCompleted != null)
            {
                gloResult Result = new gloResult();
                Result.ResultMethod = "Print";
                Result.ResultType = 2;
                Result.ResultObject = "Failure";
                _gloPrintProgress.Result.Add(Result);
            }
            try
            {
                if (OnThreadCompleted != null)
                {
                    OnThreadCompleted(this, _gloPrintProgress);
                }
            }
            catch
            {
            }
            //try
            //{
            //    if (gloGlobal.clsMISC.IsService || (!_ExtendedPrinterSettings.IsShowProgress))
            //    {
            //        this.Dispose(true);
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
            //}
            //catch
            //{

            //}
            InvokeCloseControls();
        }

        /// <summary>
        /// Do actual printing job.
        /// </summary>
        /// <param name="sender">source control</param>
        /// <param name="e">print job parameter</param>
        private void OnPrint(object sender, System.EventArgs e)
        {
            try
            {
                Print();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        void _oPrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            try
            {
                //MessageBox.Show("Finished printing document");
                AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnHandler);
                Application.ThreadException -= new ThreadExceptionEventHandler(UIThreadException);
                IsInsidePrinting = false;
                if (!IsPrintingResumed)
                {
                    try
                    {
                        InvokeCompleteUpdateControls();
                    }
                    catch
                    {
                    }
                }
                if (OnPrintingFinished != null)
                {
                    gloResult Results = new gloResult();
                    Results.ResultMethod = "OnPrintEnd";
                    Results.ResultType = 0;
                    Results.ResultObject = "Success";
                    _gloPrintProgress.Result.Add(Results);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                //SLR: need not be false because it has printed only exception in updating controls?
                //_gloPrintProgress.success = false;
                if (OnPrintingFinished != null)
                {
                    gloResult Results = new gloResult();
                    Results.ResultMethod = "OnPrintEnd";
                    Results.ResultType = 1;
                    Results.ResultObject = ex;
                    _gloPrintProgress.Result.Add(Results);
                }
                ex = null;
            }
            finally
            {
                try
                {
                    if (OnPrintingFinished != null)
                    {
                        OnPrintingFinished(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
            }
        }

        public delegate void CloseCompleteControlsDelegate();
        public void InvokeCloseControls()
        {
            try
            {
                if (gloGlobal.clsMISC.IsService)
                {
                    CloseCompleteControls();
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new CloseCompleteControlsDelegate(CloseCompleteControls));
                    }
                    else
                    {
                        CloseCompleteControls();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void CloseCompleteControls()
        {
            try
            {
                if (!IsPrintingResumed)
                {
                    if (gloGlobal.clsMISC.IsService || ((_ExtendedPrinterSettings != null && !_ExtendedPrinterSettings.IsShowProgress)))
                    {
                        this.Dispose(true);
                    }
                    else
                    {
                        this.Close();
                    }
                }
                IamPrinting = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private volatile bool IsPrintingCompletedWhilePressingPause = true;
        /// <summary>
        /// background print complete handler
        /// </summary>
        private void OnPrintComplete(IAsyncResult iar)
        {

            try
            {
                IsPrintingCompletedWhilePressingPause = true;
                try
                {
                    if (!IsPrintingResumed)
                    {
                        InvokeCompleteUpdateControls();
                    }
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
            finally
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
                try
                {
                    if (OnThreadCompleted != null)
                    {
                        OnThreadCompleted(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
                if (!IsPrintingResumed)
                {

                    InvokeCloseControls();

                    //15-Jun-16 Aniket: Resolving Bug #96962: gloEMR>>Background Printing>>Application crash during change the page no from printing dialogue
                    //if (gloGlobal.clsMISC.IsService || ( (_ExtendedPrinterSettings!=null && !_ExtendedPrinterSettings.IsShowProgress)))
                    //{
                    //    this.Dispose(true);
                    //}
                    //else
                    //{
                    //    this.Close();
                    //}
                }
            }
        }


        public delegate void UpdateCompleteControlsDelegate();
        public void InvokeCompleteUpdateControls()
        {
            try
            {
                if (gloGlobal.clsMISC.IsService)
                {
                    UpdateCompleteControls();
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateCompleteControlsDelegate(UpdateCompleteControls));
                    }
                    else
                    {
                        UpdateCompleteControls();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        private void UpdateCompleteControls()
        {
            try
            {
                if (_gloStandardPrintController != null)
                {
                    if ((_gloStandardPrintController.IsErrors || btnRestart.Enabled) && _ExtendedPrinterSettings.IsShowProgress)
                    {
                        if (_gloStandardPrintController.IsErrors)
                        {

                            _gloPrintProgress.Progress._printPageIndex = 0;
                        }
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            btnRestart.Enabled = true;
                            //if (btnPause.Text == "&Pause")
                            //{
                            //    btnPause.Text = "&Resume";
                            //    IsPrintingResumed = true;
                            //}
                            if (btnPause.Visible == false)
                            {
                                IsPrintingResumed = true;
                            }


                            btnPause.Update();
                            btnRestart.Update();
                            btnPause.ResumeLayout();
                            btnRestart.ResumeLayout();
                        }
                        _gloStandardPrintController.IsErrors = false;
                    }
                    else
                    {
                        ClearMemory();
                        try
                        {
                            if (!_gloStandardPrintController.IsErrors && _gloPrintProgress.success)
                            {
                                MovePrintedFiles();
                            }
                            //if (gloGlobal.clsMISC.IsService || (!_ExtendedPrinterSettings.IsShowProgress))
                            //{
                            //    this.Dispose(true);
                            //}
                            //else
                            //{
                            //    this.Close();
                            //}
                        }
                        catch
                        {

                        }
                        ////if (_ExtendedPrinterSettings.IsBackGroundPrint)
                        ////{
                        ////    this.Dispose(true);
                        ////}
                    }
                }
                else
                {
                    ClearMemory();
                    try
                    {
                        if (_gloPrintProgress.success)
                        {
                            MovePrintedFiles();
                        }
                        //if (gloGlobal.clsMISC.IsService || ( _ExtendedPrinterSettings!=null &&  !_ExtendedPrinterSettings.IsShowProgress))
                        //{
                        //    this.Dispose(true);
                        //}
                        //else
                        //{
                        //    this.Close();
                        //}
                    }
                    catch
                    {

                    }
                    ////if (_ExtendedPrinterSettings.IsBackGroundPrint)
                    ////{
                    ////    this.Dispose(true);
                    ////}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void MovePrintedFiles()
        {
            try
            {
                if ((_IsOnPagePDFDocCreated) || (_IsFromQueue))
                {
                    if (_PDFFile)
                    {
                        if (_PDFDoc != null)
                        {
                            _PDFDoc.Close();
                        }
                    }
                    else
                    {
                        ClearMyEMFImages();
                    }
                }
            }
            catch
            {
            }

            if (_IsFromQueue == true)
            {
                // gloGlobal.gloTSPrint.MoveOrDelete(_srcPDFFile);
                gloGlobal.gloTSPrint.MoveOrDeleteToProcessed(_srcPDFFile);
            }
        }

        private void ClearMyEMFImages()
        {
            foreach (System.Drawing.Image myMetaImage in myEmfImage)
            {
                if (myMetaImage != null)
                {
                    try
                    {
                        myMetaImage.Dispose();
                    }
                    catch
                    {
                    }

                }
            }
            myEmfImage.Clear();
        }


        public delegate void UpdateBeginControlsDelegate();
        public void InvokeBeginUpdateControls()
        {
            try
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateBeginControlsDelegate(UpdateBeginControls));
                    }
                    else
                    {
                        UpdateBeginControls();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void UpdateBeginControls()
        {
            try
            {
                // update your controls here
                lblDocumentName.Text = _oPrintDocument.DocumentName;
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName;
                //if (_ExtendedPrinterSettings.CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
                //{
                //    pbDocument.Maximum = _printactualTotalPages;
                //}
                //else
                {
                    pbDocument.Maximum = PageArray.Count;
                }
                pbDocument.Minimum = 0;
                pbDocument.Step = 1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }


        public delegate void UpdateBackgroundControlsDelegate();
        public void InvokeBackgroundUpdateControls()
        {
            try
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateBackgroundControlsDelegate(UpdateBackgroundControls));
                    }
                    else
                    {
                        UpdateBackgroundControls();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void UpdateBackgroundControls()
        {  //Dummy try catch, incase cross reference error comes from background thread.
            try
            {
                // int CurPage = PageArray[_gloPrintProgress.Progress._printPageIndex+_printBreakVerticalIndex*_printPageHorizontalIndex+_printBreakHorizontalIndex].page;
                int curIndex = _gloPrintProgress.Progress._printPageIndex + _gloPrintProgress.Progress._printPageBreakCountIndex;
                if (curIndex < PageArray.Count())
                {
                    int CurPage = PageArray[curIndex].page;
                    if (CurPage != -1)
                    {
                        lblPageNoOfDocument.Text = "Waiting for Background to Finish Page Number " + CurPage.ToString() + " from Document" + (_lstDocuments == null ? "" : " " + (_gloPrintProgress.Progress._DocumentIndex + 1).ToString() + " of " + _lstDocuments.Count.ToString());
                    }
                }
            }
            catch
            {
            }
        }


        public delegate void UpdateProgressControlsDelegate();
        public void InvokeProgressUpdateControls()
        {
            try
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateProgressControlsDelegate(UpdateProgressControls));
                    }
                    else
                    {
                        UpdateProgressControls();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void UpdateProgressControls()
        {
            try
            {
                int curIndex = _gloPrintProgress.Progress._printPageIndex + _gloPrintProgress.Progress._printPageBreakCountIndex;// +_printBreakVerticalIndex * _printPageHorizontalIndex + _printBreakHorizontalIndex;
                if (curIndex >= PageArray.Count)
                    return;
                lblCopies.Text = PageArray[curIndex].copy.ToString() + " of " + _PrintOriginalCopies.ToString() + " Copies";
                //if (_ExtendedPrinterSettings.CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
                //{
                //    if (_printactualPages >= pbDocument.Maximum)
                //    {
                //        pbDocument.Value = pbDocument.Maximum;
                //    }
                //    else
                //    {
                //        pbDocument.Value = _printactualPages + 1;
                //    }

                //    lblPages.Text = (_printactualPages + 1).ToString() + " of " + _printactualTotalPages.ToString() + " Pages";
                //}
                //else
                {
                    if (curIndex >= pbDocument.Maximum)
                    {
                        pbDocument.Value = pbDocument.Maximum;
                    }
                    else
                    {
                        pbDocument.Value = curIndex + 1;
                    }

                    lblPages.Text = (curIndex + 1).ToString() + " of " + PageArray.Count().ToString() + " Pages";
                }
                int curPage = PageArray[curIndex].page;

                lblPageNoOfDocument.Text = ((curPage < 0) ? "" : "Page Number " + curPage.ToString() + " from ") + "Document" + (_lstDocuments == null ? "" : " " + (_gloPrintProgress.Progress._DocumentIndex + 1).ToString() + " of " + _lstDocuments.Count.ToString());


                pbDocument.Refresh();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private volatile bool IsInsidePrinting = false;
        private bool customDPISet = false;
        void _oPrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                if (IsPrintingCanceled)
                {
                    return;
                }
                //bReCalcFooterBounds = true;
                //bReCalcPrinterBounds = true;
                bReCalcmeasureBounds = true;
                // _printactualPages = 0;
                // MessageBox.Show("Started Printing Job", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    if (_ExtendedPrinterSettings.IsShowProgress)
                    {
                        InvokeBeginUpdateControls();
                    }
                }
                catch
                {
                }
                if (_PDFFile)
                {
                    if (_PDFDoc == null)
                    {
                        _gloPrintProgress.success = false;
                        if (OnPrintingStarted != null)
                        {
                            gloResult Results = new gloResult();
                            Results.ResultMethod = "OnPrintBegin";
                            Results.ResultType = 2;
                            Results.ResultObject = "Error: Print document is not selected.";
                            _gloPrintProgress.Result.Add(Results);

                        }
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Error: Print document is not selected.", false);
                            //MessageBox.Show("Error: Print document is not selected.");
                        }

                        return;
                    }
                }
                else
                {

                    if ((myEmfImage == null) || (myEmfImage.Count < 1))
                    {
                        _gloPrintProgress.success = false;
                        if (OnPrintingStarted != null)
                        {
                            gloResult Results = new gloResult();
                            Results.ResultMethod = "OnPrintBegin";
                            Results.ResultType = 2;
                            Results.ResultObject = "Error: Print document is not selected.";
                            _gloPrintProgress.Result.Add(Results);

                        }
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Error: Print document is not selected.", false);
                            //MessageBox.Show("Error: Print document is not selected.");
                        }

                        return;
                    }
                    if (_oPrintDocument != null)
                    {
                        _oPrintDocument.OriginAtMargins = false;
                    }

                }
                //     print_page_itr = oPDFDoc.GetPageIterator();// doc.GetPage(1);// PageBegin;
                //if (_PrintRect != null)
                //{
                //    _PrintRect.Dispose(); 
                //    _PrintRect = null;
                //}
                // Add the event handler for handling UI thread exceptions to the event.
                if (_PDFFile)
                {
                    if (_PdfDraw == null)
                    {
                        _gloPrintProgress.success = false;
                        if (OnPrintingStarted != null)
                        {
                            gloResult Results = new gloResult();
                            Results.ResultMethod = "OnPrintBegin";
                            Results.ResultType = 2;
                            Results.ResultObject = "Error: PDfDraw missing.";
                            _gloPrintProgress.Result.Add(Results);
                        }
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            MessageBox.Show("Error: PDfDraw missing.");
                        }
                        return;
                    }
                }
                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
                // Set the unhandled exception mode to force all Windows Forms errors to go through 
                // our handler.
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
                IsInsidePrinting = true;
                if (_PDFFile)
                {
                    if (_ExtendedPrinterSettings.IsCustomDPI)
                    {
                        //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting Builtin Rasterizer");
                        _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
                        customDPISet = true;
                        //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting Builtin Rasterizer");
                    }
                    else
                    {
                        //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting GDI Rasterizer");
                        _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
                        customDPISet = false;
                        //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting GDI Rasterizer");               
                    }
                }
                if (OnPrintingStarted != null)
                {
                    gloResult ResultsS = new gloResult();
                    ResultsS.ResultMethod = "OnPrintBegin";
                    ResultsS.ResultType = 0;
                    ResultsS.ResultObject = "Success";
                    _gloPrintProgress.Result.Add(ResultsS);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                _gloPrintProgress.success = false;
                if (OnPrintingStarted != null)
                {
                    gloResult ResultsS = new gloResult();
                    ResultsS.ResultMethod = "OnPrintBegin";
                    ResultsS.ResultType = 1;
                    ResultsS.ResultObject = ex;
                    _gloPrintProgress.Result.Add(ResultsS);
                }
                ex = null;
            }
            finally
            {
                try
                {
                    if (OnPrintingStarted != null)
                    {
                        OnPrintingStarted(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
            }

        }


        void _oPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                IsInsidePrinting = true;
                if (e.HasMorePages)
                {
                    _gloPrintProgress.success = false;
                    if (OnPrintingPages != null)
                    {
                        gloResult ResultsS = new gloResult();
                        ResultsS.ResultMethod = "OnPrintPage";
                        ResultsS.ResultType = 2;
                        ResultsS.ResultObject = " Has More pages ";
                        _gloPrintProgress.Result.Add(ResultsS);
                    }
                    return;
                }
                if (_PDFFile)
                {
                    if (_PdfDraw == null)
                    {
                        _gloPrintProgress.success = false;
                        if (OnPrintingPages != null)
                        {
                            gloResult ResultsS = new gloResult();
                            ResultsS.ResultMethod = "OnPrintPage";
                            ResultsS.ResultType = 2;
                            ResultsS.ResultObject = " _pdfDraw is not exisitng ";
                            _gloPrintProgress.Result.Add(ResultsS);
                        }
                        return;
                    }
                }
                //Graphics gr = e.Graphics;
                //if (_PDFFile)
                //{
                //    e.Graphics.PageUnit = GraphicsUnit.Inch;
                //}
                //else
                {
                    e.Graphics.PageUnit = GraphicsUnit.Point;
                }
                //Rectangle rectPage = e.PageBounds;
                // print without margins 
                // Rectangle rectPage = ev.MarginBounds ' print using margins 
                PDFDoc doc = null;
                if (_PDFFile)
                {
                    if (_ExtendedPrinterSettings.IsCustomDPI)
                    {
                        float dpi = Math.Max(e.Graphics.DpiX, e.Graphics.DpiY);

                        if (dpi > (float)_ExtendedPrinterSettings.CustomDPI)
                        {
                            dpi = (float)_ExtendedPrinterSettings.CustomDPI;
                            //   gloAuditTrail.gloAuditTrail.PrintLog("Started Setting DPI "+dpi.ToString());
                            _PdfDraw.SetDPI(dpi);
                            //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting DPI " + dpi.ToString());
                        }
                    }

                    //if (_ExtendedPrinterSettings.IsActualPageSize)
                    //{
                    //    RectangleF realPagePounds = GetRealPageBounds(e);

                    //}
                    //else
                    {
                        //RectangleF realPageBounds = GetRealPageBoundsInPoints(e);
                        //if (_PrintRect != null)
                        //{
                        //    _PrintRect.Dispose();
                        //}

                        //_PrintRect = new pdftron.PDF.Rect(realPageBounds.Left, realPageBounds.Top, realPageBounds.Right, realPageBounds.Bottom);

                        //_PrintRect = new pdftron.PDF.Rect(((double)realPagePounds.Left / 100) * 72, ((double)realPagePounds.Bottom / 100) * 72, ((double)realPagePounds.Right / 100) * 72, ((double)realPagePounds.Top / 100) * 72);
                    }
                    //double left;
                    //double right;
                    //double top;
                    //double bottom;

                    //left = rectPage.Left / 100;
                    //right = rectPage.Right / 100;
                    //top = rectPage.Top / 100;
                    //bottom = rectPage.Bottom / 100;


                    doc = _PDFDoc;
                    doc.Lock();
                }
                bool bErrorInGraphics = false;
                if (_gloPrintProgress.Progress._printPageIndex < PageArray.Count)
                {
                    try
                    {
                        printOutput thisPageOutput = PageArray[_gloPrintProgress.Progress._printPageIndex];
                        _gloPrintProgress.Progress._printPageBreakIndex = thisPageOutput.iPageBreakIndex;
                    }
                    catch
                    {
                        _gloPrintProgress.Progress._printPageBreakIndex = 1;
                    }
                }
                else
                {
                    _gloPrintProgress.Progress._printPageBreakIndex = 1;
                }
                for (_gloPrintProgress.Progress._printPageBreakCountIndex = 0; _gloPrintProgress.Progress._printPageBreakCountIndex < _gloPrintProgress.Progress._printPageBreakIndex; _gloPrintProgress.Progress._printPageBreakCountIndex++)
                {
                    //for (_printBreakVerticalIndex = 0; _printBreakVerticalIndex < _printPageVerticalIndex; _printBreakVerticalIndex++)
                    //{
                    //    for (_printBreakHorizontalIndex = 0; _printBreakHorizontalIndex < _printPageHorizontalIndex; _printBreakHorizontalIndex++)
                    //    {
                    try
                    {
                        try
                        {
                            if (_ExtendedPrinterSettings.IsShowProgress)
                            {
                                InvokeProgressUpdateControls();
                            }
                        }
                        catch
                        {
                        }
                        errorHappened = false;

                        try
                        {
                            //bErrorInGraphics = PrintPDFPage(e, _printBreakHorizontalIndex, _printBreakVerticalIndex);
                            bErrorInGraphics = PrintPDFPage(e);
                        }
                        catch (PDFNetException ex)
                        {
                            errorHappened = true;
                            #region " Make Log Entry "

                            string _ErrorMessage = ex.ToString();
                            //Code added on 7rd October 2008 By - Sagar Ghodke
                            //Make Log entry in DMSExceptionLog file for any exceptions found
                            if (_ErrorMessage.Trim() != "")
                            {
                                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                _MessageString = "";
                            }

                            //End Code add
                            #endregion " Make Log Entry "
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                            ex = null;

                        }
                        catch (AccessViolationException ex)
                        {
                            errorHappened = true;
                            #region " Make Log Entry "

                            string _ErrorMessage = ex.ToString();
                            //Code added on 7rd October 2008 By - Sagar Ghodke
                            //Make Log entry in DMSExceptionLog file for any exceptions found
                            if (_ErrorMessage.Trim() != "")
                            {
                                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                _MessageString = "";
                            }

                            //End Code add
                            #endregion " Make Log Entry "
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                            ex = null;
                        }
                        if (errorHappened)
                        {
                            //Reverse the type:
                            if (_PDFFile)
                            {
                                if (customDPISet)
                                {
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting GDI Rasterizer");
                                    _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
                                    customDPISet = false;
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting GDI Rasterizer");               

                                }
                                else
                                {
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting Builtin Rasterizer");
                                    _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting Builtin Rasterizer");
                                    _PdfDraw.SetDPI(300);
                                    customDPISet = true;

                                }
                            }
                            errorHappened = false;
                            try
                            {
                                //bErrorInGraphics = PrintPDFPage(e, _printBreakHorizontalIndex, _printBreakVerticalIndex); 
                                bErrorInGraphics = PrintPDFPage(e);//, _printBreakHorizontalIndex, _printBreakVerticalIndex); 

                            }
                            catch (PDFNetException ex)
                            {
                                errorHappened = true;
                                #region " Make Log Entry "

                                string _ErrorMessage = ex.ToString();
                                //Code added on 7rd October 2008 By - Sagar Ghodke
                                //Make Log entry in DMSExceptionLog file for any exceptions found
                                if (_ErrorMessage.Trim() != "")
                                {
                                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                    //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                    _MessageString = "";
                                }

                                //End Code add
                                #endregion " Make Log Entry "
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                ex = null;
                            }
                            catch (AccessViolationException ex)
                            {
                                errorHappened = true;
                                #region " Make Log Entry "

                                string _ErrorMessage = ex.ToString();
                                //Code added on 7rd October 2008 By - Sagar Ghodke
                                //Make Log entry in DMSExceptionLog file for any exceptions found
                                if (_ErrorMessage.Trim() != "")
                                {
                                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                    //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                    _MessageString = "";
                                }
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                ex = null;
                                //End Code add
                                #endregion " Make Log Entry "

                            }
                            if (errorHappened)
                            {
                                bErrorInGraphics = true;
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
                            //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }

                        //End Code add
                        #endregion " Make Log Entry "
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        if (!gloGlobal.clsMISC.IsService)
                        {
                            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (!gloGlobal.clsMISC.IsService)
                    {
                        Application.DoEvents();
                    }

                    if (_gloPrintProgress.Progress._printPageBreakCountIndex < (_gloPrintProgress.Progress._printPageBreakIndex - 1))
                    {
                        if (OnPrintingPages != null)
                        {
                            gloResult ResultsS = new gloResult();
                            ResultsS.ResultMethod = "OnPrintPage";
                            ResultsS.ResultType = 0;
                            ResultsS.ResultObject = "Success";
                            _gloPrintProgress.Result.Add(ResultsS);
                        }
                        try
                        {
                            if (OnPrintingPages != null)
                            {
                                OnPrintingPages(this, _gloPrintProgress);
                            }
                        }
                        catch
                        {
                        }

                    }
                    //    }
                    //}
                }
                //_printBreakVerticalIndex = 0;
                //_printBreakHorizontalIndex = 0;
                try
                {
                    if (_PDFFile)
                    {
                        doc.Unlock();
                    }
                    if (bErrorInGraphics)
                    {
                        e.HasMorePages = false;
                        _gloPrintProgress.success = false;
                        if (OnPrintingPages != null)
                        {
                            gloResult ResultsS = new gloResult();
                            ResultsS.ResultMethod = "OnPrintPage";
                            ResultsS.ResultType = 2;
                            ResultsS.ResultObject = " Error in graphics ";
                            _gloPrintProgress.Result.Add(ResultsS);
                        }
                        //if (_gloStandardPrintController != null)
                        //{
                        //    _gloStandardPrintController.IsErrors = true;
                        //}
                    }
                    else
                    {
                        //if ((_printPageSubIndex+1) < _printPageMaxIndex)
                        //{
                        //    _printPageSubIndex++;
                        //    _printactualPages++;
                        //    e.HasMorePages = true;
                        //}
                        //else
                        {
                            _gloPrintProgress.Progress._printPageIndex += _gloPrintProgress.Progress._printPageBreakIndex;
                            // _printactualPages++;
                            if (_gloPrintProgress.Progress._printPageIndex < PageArray.Count)
                            {
                                e.HasMorePages = true;
                            }
                            else
                            {
                                if (BringNextDocuments())
                                {
                                    e.HasMorePages = true;
                                    while ((_gloPrintProgress.Progress._printPageIndex >= PageArray.Count))
                                    {
                                        if (!BringNextDocuments())
                                        {
                                            e.HasMorePages = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            _gloPrintProgress.Progress._printPageBreakCountIndex = 0;
                            if (OnPrintingPages != null)
                            {
                                gloResult ResultsS = new gloResult();
                                ResultsS.ResultMethod = "OnPrintPage";
                                ResultsS.ResultType = 0;
                                ResultsS.ResultObject = "Success";
                                _gloPrintProgress.Result.Add(ResultsS);
                            }
                            //_printPageSubIndex = 0;
                            // _printPageMaxIndex = 0;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    MessageBox.Show(ex.ToString());
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
            finally
            {
                if (IsPrintingCanceled || IsPrintingResumed)
                {
                    e.HasMorePages = false;
                }
                try
                {
                    if (OnPrintingPages != null)
                    {
                        OnPrintingPages(this, _gloPrintProgress);
                    }
                }
                catch
                {
                }
            }
        }

        private bool BringNextDocuments()
        {
            bool bHasMorePages = false;
            if (_lstDocuments != null)
            {

                _gloPrintProgress.Progress._DocumentIndex++;
                if (_gloPrintProgress.Progress._DocumentIndex < _lstDocuments.Count)
                {

                    MovePrintedFiles();
                    try
                    {
                        //if (_IsOnPagePDFDocCreated)
                        //{
                        try
                        {
                            if (_PDFFile)
                            {
                                if (_PDFDoc != null)
                                {
                                    _PDFDoc.Dispose();
                                    _PDFDoc = null;
                                }
                            }
                            else
                            {
                                //foreach (System.Drawing.Image myMetaImage in myEmfImage)
                                //{
                                //    if (myMetaImage != null)
                                //    {
                                //        try
                                //        {
                                //            myMetaImage.Dispose();
                                //        }
                                //        catch
                                //        {
                                //        }

                                //    }
                                //}
                                //myEmfImage.Clear();
                                ClearMyEMFImages();
                            }
                        }
                        catch
                        {
                        }
                        DocumentInfo docInfo = _lstDocuments[_gloPrintProgress.Progress._DocumentIndex];
                        _PDFFileName = docInfo.PdfFileName;
                        _strFileType = Path.GetExtension(_PDFFileName).ToLower();
                        if (String.IsNullOrEmpty(_strFileType.Trim()))
                        {
                            _strFileType = ".pdf";
                        }
                        _PDFFile = _strFileType == ".pdf";
                        _srcPDFFile = docInfo.SrcFileName;
                        _PDFFooter = docInfo.footerInfo;

                        if (gloGlobal.clsMISC.WaitForFileToBeReady(_PDFFileName, 1000, 1000))
                        {
                            if (_PDFFile)
                            {
                                _PDFDoc = new PDFDoc(_PDFFileName);
                                _PDFDoc.InitSecurityHandler();
                                //SLR: moved from bottom in order to gain memory for PopulatePageArray
                                if (_PdfDraw != null)
                                {
                                    _PdfDraw.Dispose();
                                    _PdfDraw = null;
                                }
                            }
                            else
                            {
                                //if (_strFileType == "ZIP")
                                //{
                                //    _PDFFileName = gloGlobal.gloTSPrint.UnZipEMF(_PDFFileName);
                                // }
                                ////myMetaData = new Metafile(_PDFFileName);
                                //myEmfImage.Add(System.Drawing.Image.FromFile(_PDFFileName));
                                AddImageFilesToPrint();
                            }
                            _gloPrintProgress.Progress._printPageIndex = 0;
                            if (_PrinterSetting.FromPage > _SplitPageCount)
                            {
                                _PrinterSetting.FromPage -= _SplitPageCount;

                            }
                            else
                            {
                                if (_PrinterSetting.FromPage > 0)
                                {
                                    _PrinterSetting.FromPage = 0;
                                }
                            }

                            if (_PrinterSetting.ToPage > _SplitPageCount)
                            {
                                _PrinterSetting.ToPage -= _SplitPageCount;

                            }
                            else
                            {
                                if (_PrinterSetting.ToPage > 0)
                                {
                                    _PrinterSetting.ToPage = 0;
                                }
                            }
                            AssignPrinterSettings(_PrinterSetting, docInfo);
                            PopulatePrinterPageArray();
                            try
                            {
                                if (OnMeasurementsCalculated != null)
                                {
                                    OnMeasurementsCalculated(this, _gloPrintProgress);
                                }
                            }
                            catch
                            {
                            }
                            bHasMorePages = true;
                            if (_PDFFile)
                            {

                                _PdfDraw = new PDFDraw();

                                if (_ExtendedPrinterSettings.IsCustomDPI)
                                {
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting Builtin Rasterizer");
                                    _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
                                    customDPISet = true;
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting Builtin Rasterizer");
                                }
                                else
                                {
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting GDI Rasterizer");
                                    _PdfDraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
                                    customDPISet = false;
                                    //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting GDI Rasterizer");               
                                }
                            }
                        }
                        else
                        {

                            bHasMorePages = false;
                            _gloPrintProgress.success = false;
                            if (OnPrintingPages != null)
                            {
                                gloResult ResultsSs = new gloResult();
                                ResultsSs.ResultMethod = "OnPrintPage";
                                ResultsSs.ResultType = 2;
                                ResultsSs.ResultObject = " Error in Getting Continution of Next File ";
                                _gloPrintProgress.Result.Add(ResultsSs);
                            }
                        }
                        //    _IsOnPagePDFDocCreated = true;
                        //}
                    }
                    catch (Exception Ex)
                    {

                        bHasMorePages = false;
                        _gloPrintProgress.success = false;
                        if (OnPrintingPages != null)
                        {
                            gloResult ResultsSs = new gloResult();
                            ResultsSs.ResultMethod = "OnPrintPage";
                            ResultsSs.ResultType = 1;
                            ResultsSs.ResultObject = Ex;
                            _gloPrintProgress.Result.Add(ResultsSs);
                        }
                    }

                }
                else
                {
                    bHasMorePages = false;
                }
            }
            else
            {
                bHasMorePages = false;
            }
            return bHasMorePages;
        }
        //private bool PrintPDFPage(PrintPageEventArgs e, int breakHorizontal, int breakVertical)
        //{
        //    //   gloAuditTrail.gloAuditTrail.PrintLog("Started Drawing PDF" +_gloPrintProgress.Progress._printPageIndex.ToString());
        //    //   e.graphcis some how becomes blank, if printer has a problem, and it has to be checked whenever it has to be accessed.
        //    bool bErrorInGraphics = false;
        //    try
        //    {
        //        gloPrintProgressController.FooterInfo footer = null;
        //        if (e != null)
        //        {
        //            if (e.Graphics != null)
        //            {
        //                if (_PdfDraw != null)
        //                {
        //                    if (_gloPrintProgress.Progress._printPageIndex < PageArray.Count)
        //                    {
        //                        int curIndex = _gloPrintProgress.Progress._printPageIndex + _printBreakVerticalIndex * _printPageHorizontalIndex + _printBreakHorizontalIndex;
        //                        int thisPageNumber = System.Convert.ToInt32(PageArray[curIndex].page);
        //                        if (thisPageNumber >= 0)
        //                        {
        //                            Page thisPage = _PDFDoc.GetPage(thisPageNumber);
        //                            footer = GetFooter(_PDFFooter, thisPageNumber);
        //                            if (thisPage != null)
        //                            {
        //                                int curPageMaxWidth = 0;
        //                                int curPageMaxHeight = 0;
        //                                //if (_PrintRect == null)
        //                                {
        //                                    RectangleF realPageBounds = PageArray[curIndex].printerbound;
        //                                    if (_ExtendedPrinterSettings.CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
        //                                    {

        //                                        //Rect mediaRect = thisPage.GetMediaBox();
        //                                       // Rect mediaRect = new pdftron.PDF.Rect(0, 0, thisPage.GetPageWidth(), thisPage.GetPageHeight());
        //                                        if (_printPageSubIndex == 0)
        //                                        {
        //                                            _printPageMaxWidth = PageArray[curIndex].horzsplit;

        //                                            _printPageMaxHeight = PageArray[curIndex].vertsplit; 
        //                                            if (_printPageMaxWidth <= 0) _printPageMaxWidth = 1;
        //                                            if (_printPageMaxHeight <= 0) _printPageMaxHeight = 1;
        //                                            _printPageMaxIndex = _printPageMaxWidth * _printPageMaxHeight;

        //                                        }
        //                                        if (_PrintRect != null)
        //                                        {
        //                                            _PrintRect.Dispose();
        //                                        }
        //                                        curPageMaxWidth = _printPageSubIndex / _printPageMaxHeight;
        //                                        curPageMaxHeight = _printPageSubIndex - (curPageMaxWidth * _printPageMaxHeight);
        //                                        float Left = realPageBounds.Left - (curPageMaxWidth * (realPageBounds.Width - _ExtendedPrinterSettings.HorizontalOverlap));
        //                                        float Top = realPageBounds.Top - (curPageMaxHeight * (realPageBounds.Height - _ExtendedPrinterSettings.VerticalOverlap - _footerFontHeight));
        //                                        _PrintRect = new pdftron.PDF.Rect(Left, Top, Left + (float)thisPage.GetPageWidth(), Top + (float)thisPage.GetPageHeight());
        //                                        footer = GetFooter(_PDFFooter, thisPageNumber, curPageMaxWidth, curPageMaxHeight);
        //                                    }
        //                                    else
        //                                    {

        //                                        if (_PrintRect != null)
        //                                        {
        //                                            _PrintRect.Dispose();
        //                                        }

        //                                        float Left = realPageBounds.Left + ((realPageBounds.Width + _ExtendedPrinterSettings.HorizontalGutter) * breakHorizontal);
        //                                        float Top = realPageBounds.Top + ((realPageBounds.Height + (_ExtendedPrinterSettings.VerticalGutter+_footerFontHeight)) * breakVertical);

        //                                        _PrintRect = new pdftron.PDF.Rect(Left, Top, Left + realPageBounds.Width, Top + realPageBounds.Height);
        //                                        //                                            _PrintRect = new pdftron.PDF.Rect(((double)realPagePounds.Left / 100) * 72, ((double)realPagePounds.Bottom / 100) * 72, ((double)realPagePounds.Right / 100) * 72, ((double)realPagePounds.Top / 100) * 72);
        //                                    }
        //                                }
        //                                if (_PrintRect != null)
        //                                {
        //                                    if (e.Graphics != null)
        //                                    {
        //                                        _PdfDraw.DrawInRect(thisPage, e.Graphics, _PrintRect);

        //                                        PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);


        //                                    }
        //                                    else
        //                                    {
        //                                        bErrorInGraphics = true;
        //                                        //MessageBox.Show("Graphics not found1");
        //                                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 1", false);
        //                                    }

        //                                }
        //                                else
        //                                {
        //                                    if (e.Graphics != null)
        //                                    {
        //                                        e.Graphics.DrawString("Unable To Get the Printer Coordinates From the Rectangle, Hence Printing a Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(10, 10));

        //                                        PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);


        //                                        bErrorInGraphics = true;
        //                                    }
        //                                    else
        //                                    {
        //                                        bErrorInGraphics = true;
        //                                        //MessageBox.Show("Graphics not found1");
        //                                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 2", false);

        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (e.Graphics != null)
        //                                {
        //                                    e.Graphics.DrawString("Unable To Get the Page From the File, Hence Printing a Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(10, 10));

        //                                    PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);


        //                                }
        //                                else
        //                                {
        //                                    bErrorInGraphics = true;
        //                                    //MessageBox.Show("Graphics not found1");
        //                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 3", false);

        //                                }

        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (e.Graphics != null)
        //                        {

        //                            bErrorInGraphics = true;
        //                            e.Graphics.DrawString("Last Page Was Already Printed, Hence Printing a Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(10, 10));

        //                     //       PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);



        //                        }
        //                        else
        //                        {
        //                            bErrorInGraphics = true;
        //                            //MessageBox.Show("Graphics not found3");
        //                            gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 3", false);
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    if (e.Graphics != null)
        //                    {
        //                        bErrorInGraphics = true;
        //                        e.Graphics.DrawString("Unable To Get the PDF Drawing From the File, Hence Printing a Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(10, 10));

        //                      //  PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);



        //                    }
        //                    else
        //                    {
        //                        bErrorInGraphics = true;
        //                        //MessageBox.Show("Graphics not found3");
        //                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 4", false);
        //                    }

        //                }

        //                //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Drawing PDF" + _gloPrintProgress.Progress._printPageIndex.ToString());
        //            }
        //            else
        //            {
        //                bErrorInGraphics = true;
        //                //MessageBox.Show("Graphics not found3");
        //                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics 5", false);
        //            }

        //        }
        //        else
        //        {
        //            bErrorInGraphics = true;
        //            //MessageBox.Show("Graphics not found3");
        //            gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Event Graphics", false);
        //        }
        //    }
        //    catch
        //    {
        //        bErrorInGraphics = true;
        //        throw;
        //    }
        //    return bErrorInGraphics;
        //}
        private bool PrintPDFPage(PrintPageEventArgs e)
        {
            //   gloAuditTrail.gloAuditTrail.PrintLog("Started Drawing PDF" +_gloPrintProgress.Progress._printPageIndex.ToString());
            //   e.graphcis some how becomes blank, if printer has a problem, and it has to be checked whenever it has to be accessed.
            bool bErrorInGraphics = false;
            //bool toPlayFromRecord = false;
            try
            {

                if (e != null)
                {
                    if (e.Graphics != null)
                    {
                        if ((_PdfDraw != null) || (!_PDFFile))
                        {
                            if (_gloPrintProgress.Progress._printPageIndex < PageArray.Count)
                            {
                                int curIndex = _gloPrintProgress.Progress._printPageIndex + _gloPrintProgress.Progress._printPageBreakCountIndex;
                                if (curIndex < PageArray.Count)
                                {
                                    printOutput thisPageOutput = PageArray[curIndex];

                                    //  int curIndex = _gloPrintProgress.Progress._printPageIndex + _printBreakVerticalIndex * _printPageHorizontalIndex + _printBreakHorizontalIndex;
                                    //int thisPageNumber = System.Convert.ToInt32(thisPageOutput.page);
                                    if (thisPageOutput.page >= 0)
                                    {
                                        if (!_PDFFile)
                                        {
                                            // System.Drawing.Image myMetaImage = myEmfImage[thisPageNumber-1];
                                            if (e.Graphics != null)
                                            {

                                                //   System.Drawing.Point point = new System.Drawing.Point(0, 0);
                                                //  float widthInPoints = myMetaImage.Width / myMetaImage.HorizontalResolution * 72;
                                                //  float HeightInPoints = myMetaImage.Height / myMetaImage.VerticalResolution * 72;
                                                //if (toPlayFromRecord)
                                                //{
                                                //    //SLR: margins not needed..
                                                //    //float widthScale = widthInPoints / (e.MarginBounds.Width + e.MarginBounds.X);
                                                //    //float heightScale = HeightInPoints / (e.MarginBounds.Height + e.MarginBounds.Y);
                                                //    float widthScale = widthInPoints / (e.MarginBounds.Width  );
                                                //    float heightScale = HeightInPoints / (e.MarginBounds.Height );
                                                //    float bothScale = 1;
                                                //    if (widthScale < heightScale)
                                                //    {
                                                //        bothScale = widthScale;
                                                //    }
                                                //    else
                                                //    {
                                                //        bothScale = heightScale;
                                                //    }
                                                //    if (bothScale > 1)
                                                //    {
                                                //        bothScale = 1;
                                                //    }
                                                //    e.Graphics.ScaleTransform(bothScale, bothScale);
                                                //    System.Drawing.Rectangle newRect = new System.Drawing.Rectangle(0, 0, System.Convert.ToInt32(widthInPoints), System.Convert.ToInt32(HeightInPoints));


                                                //    e.Graphics.EnumerateMetafile(myMetaData, point, newRect, GraphicsUnit.Point, myMetaFileDelegate);
                                                //}
                                                //else
                                                {
                                                    //float widthScale = (e.MarginBounds.Width + e.MarginBounds.X - _ExtendedPrinterSettings.PrinterMarginsLeft - _ExtendedPrinterSettings.PrinterMarginsRight) / widthInPoints;
                                                    //float heightScale = (e.MarginBounds.Height + e.MarginBounds.Y - _ExtendedPrinterSettings.PrinterMarginsTop - _ExtendedPrinterSettings.PrinterMarginsBottom) / HeightInPoints;
                                                    //float bothScale = 1;
                                                    //if (widthScale < heightScale)
                                                    //{
                                                    //    bothScale = widthScale;
                                                    //}
                                                    //else
                                                    //{
                                                    //    bothScale = heightScale;
                                                    //}
                                                    //SLR: not needed if it is scaled down also to set it back
                                                    //if (bothScale < 1)
                                                    //{
                                                    //    bothScale = 1;
                                                    //}
                                                    //if (myMetaData != null)
                                                    //{
                                                    //    myMetaData.Dispose();
                                                    //    myMetaData = null;
                                                    //}
                                                    //System.Drawing.Image myImage = System.Drawing.Image.FromFile(_PDFFileName);

                                                    //e.Graphics.ScaleTransform(bothScale, bothScale);

                                                    //System.Drawing.RectangleF newRectF = new System.Drawing.RectangleF(_ExtendedPrinterSettings.PrinterMarginsLeft, _ExtendedPrinterSettings.PrinterMarginsRight, widthInPoints * bothScale, HeightInPoints * bothScale);

                                                    //Point[] points = { new Point(e.MarginBounds.Location.X, e.MarginBounds.Y), new Point(e.MarginBounds.Right, e.MarginBounds.Top), new Point(e.MarginBounds.Left, e.MarginBounds.Bottom) };
                                                    //e.Graphics.EnumerateMetafile(myMetaData, point, newRect,GraphicsUnit.Point, myMetaFileDelegate);
                                                    // e.Graphics.EnumerateMetafile(myMetaData, point,  myMetaFileDelegate);
                                                    //if (thisPageOutput.toClip)
                                                    if (thisPageOutput.reCalcSourceBound)
                                                    {
                                                        System.Drawing.Image myMetaImage = myEmfImage[thisPageOutput.page - 1];
                                                        RectangleF rf = new RectangleF(0, 0, ((float)myMetaImage.Width) / myMetaImage.HorizontalResolution * 72f, ((float)myMetaImage.Height) / myMetaImage.VerticalResolution * 72f);
                                                        e.Graphics.DrawImage(myMetaImage, thisPageOutput.printerbound, rf, GraphicsUnit.Point);
                                                    }
                                                    else
                                                    {
                                                        e.Graphics.DrawImage(myEmfImage[thisPageOutput.page - 1], thisPageOutput.printerbound, thisPageOutput.sourcebound, GraphicsUnit.Point);
                                                    }
                                                    //else
                                                    //{
                                                    //    System.Drawing.RectangleF srcRectF = new System.Drawing.RectangleF(0f, 0f, thisPageOutput.pageWidth, thisPageOutput.pageHeight);
                                                    //    e.Graphics.DrawImage(myEmfImage[thisPageOutput.page - 1], thisPageOutput.printerbound, srcRectF, GraphicsUnit.Point);
                                                    //}

                                                    //myImage.Dispose();
                                                    //myImage = null;
                                                }
                                                // e.Graphics.EnumerateMetafile(myMetaData, point, myMetaFileDelegate);
                                                PrintFooter(e, thisPageOutput.footer, thisPageOutput.footerbound); //, thisPageOutput.printerbound);
                                            }
                                            else
                                            {
                                                bErrorInGraphics = true;
                                                //MessageBox.Show("Graphics not found1");
                                                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics Not found in 1st instance", false);
                                            }
                                        }
                                        else
                                        {
                                            if (thisPageOutput.thisPage != null)
                                            {
                                                using (Rect thisPrintRect = new pdftron.PDF.Rect(thisPageOutput.printerbound.Left, thisPageOutput.printerbound.Top, thisPageOutput.printerbound.Left + thisPageOutput.printerbound.Width, thisPageOutput.printerbound.Top + thisPageOutput.printerbound.Height))
                                                {
                                                    if (e.Graphics != null)
                                                    {
                                                        //SLR: in pdf 0,0 is bottom left and hence subtracting height to get top, left
                                                        using (Rect clipRect = new pdftron.PDF.Rect(thisPageOutput.sourcebound.Left, thisPageOutput.pageHeight - thisPageOutput.sourcebound.Top - thisPageOutput.sourcebound.Height, thisPageOutput.sourcebound.Left + thisPageOutput.sourcebound.Width, thisPageOutput.pageHeight - thisPageOutput.sourcebound.Top))
                                                        {
                                                            Rect oldcrop = null;
                                                            if (thisPageOutput.toClip)
                                                            {
                                                                oldcrop = thisPageOutput.thisPage.GetCropBox();
                                                                thisPageOutput.thisPage.SetCropBox(clipRect);
                                                                _PdfDraw.SetPageBox(Page.Box.e_crop);
                                                            }
                                                            _PdfDraw.DrawInRect(thisPageOutput.thisPage, e.Graphics, thisPrintRect);
                                                            if (thisPageOutput.toClip)
                                                            {
                                                                thisPageOutput.thisPage.SetCropBox(oldcrop);
                                                            }
                                                            if (oldcrop != null)
                                                            {
                                                                oldcrop.Dispose();
                                                                oldcrop = null;
                                                            }
                                                        }
                                                        PrintFooter(e, thisPageOutput.footer, thisPageOutput.footerbound); //, thisPageOutput.printerbound);


                                                    }
                                                    else
                                                    {
                                                        bErrorInGraphics = true;
                                                        //MessageBox.Show("Graphics not found1");
                                                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics Not found in 2nd instance", false);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (e.Graphics != null)
                                                {
                                                    e.Graphics.DrawString("Unable To Get The Page From The File, Hence Printing A Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(1, 1));

                                                    PrintFooter(e, thisPageOutput.footer, thisPageOutput.footerbound);//, thisPageOutput.printerbound);


                                                }
                                                else
                                                {
                                                    bErrorInGraphics = true;
                                                    //MessageBox.Show("Graphics not found1");
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Unable To Get The Page and Also Error In Graphics", false);

                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (e.Graphics != null)
                                    {

                                        bErrorInGraphics = true;
                                        e.Graphics.DrawString("Last Page Was Already Printed, Hence Printing A Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(1, 1));

                                        //       PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);



                                    }
                                    else
                                    {
                                        bErrorInGraphics = true;
                                        //MessageBox.Show("Graphics not found3");
                                        gloAuditTrail.gloAuditTrail.ExceptionLog("Last Page Was Already Printed and Also Error in Graphics", false);
                                    }
                                }
                            }
                            else
                            {
                                if (e.Graphics != null)
                                {

                                    bErrorInGraphics = true;
                                    e.Graphics.DrawString("Last Page Was Already Printed, Hence Printing A Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(1, 1));

                                    //       PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);



                                }
                                else
                                {
                                    bErrorInGraphics = true;
                                    //MessageBox.Show("Graphics not found3");
                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Last Page Was Already Printed and Also Error in Graphics", false);
                                }
                            }

                        }
                        else
                        {
                            if (e.Graphics != null)
                            {
                                bErrorInGraphics = true;
                                e.Graphics.DrawString("Unable To Get The PDF Drawing From The File, Hence Printing A Blank Page", System.Drawing.SystemFonts.CaptionFont, Brushes.Black, new PointF(1, 1));

                                //  PrintFooter(e, footer, PageArray[curIndex].footerbound, PageArray[curIndex].printerbound);



                            }
                            else
                            {
                                bErrorInGraphics = true;
                                //MessageBox.Show("Graphics not found3");
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Unable to Get The PDF Drawing and Also Error In Graphics", false);
                            }

                        }

                        //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Drawing PDF" + _gloPrintProgress.Progress._printPageIndex.ToString());
                    }
                    else
                    {
                        bErrorInGraphics = true;
                        //MessageBox.Show("Graphics not found3");
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Graphics", false);
                    }

                }
                else
                {
                    bErrorInGraphics = true;
                    //MessageBox.Show("Graphics not found3");
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error in Event Graphics", false);
                }
            }
            catch
            {
                bErrorInGraphics = true;
                throw;
            }
            return bErrorInGraphics;
        }
        private FooterInfo GetFooter(List<FooterInfo> lPDFFooter, int thisPageNumber)
        {
            if (lPDFFooter != null)
            {
                foreach (FooterInfo footer in lPDFFooter)
                {
                    if (((footer.FromPage <= thisPageNumber) || (footer.FromPage == 0)) && ((footer.ToPage >= thisPageNumber) || (footer.ToPage == 0)))
                    {
                        return GetTransformed(footer, thisPageNumber);
                    }
                }
            }
            return null;
        }

        private FooterInfo GetTransformed(FooterInfo footer, int thisPageNumber)
        {
            FooterInfo retInfo = new FooterInfo();
            retInfo.CopyExceptText(footer);
            if (footer.CurrentPageSize == gloExtendedPrinterSettings.PageSize.None)
            {
                retInfo.CurrentPageSize = _ExtendedPrinterSettings.CurrentPageSize;
            }
            int startingNumber = footer.StartingPage > 0 ? footer.StartingPage + thisPageNumber - (footer.FromPage == 0 ? 1 : footer.FromPage) : thisPageNumber;
            retInfo.LeftText = GetTransformed(footer.LeftText, startingNumber, footer.TotalPages);
            retInfo.RightText = GetTransformed(footer.RightText, startingNumber, footer.TotalPages);
            retInfo.CenterText = GetTransformed(footer.CenterText, startingNumber, footer.TotalPages);
            return retInfo;
        }

        private string GetTransformed(string p, int thisPageNumber, int totPages)
        {
            string returnString = "";
            string strLoopStr = p;
            string[] patternsToFindString = { "[{PAGE(", "[{TOTAL(", "[{DATE(" };
            int myPageCount = 1;
            if (_PDFFile)
            {
                if (_PDFDoc != null)
                {
                    myPageCount = _PDFDoc.GetPageCount();
                }
            }
            else
            {
                myPageCount = myEmfImage.Count();
            }

            while (true)
            {
                int beginIndexOfPattern = strLoopStr.Length;
                if (beginIndexOfPattern == 0) return returnString;
                int indexFoundAtString = patternsToFindString.Length;
                for (int i = 0; i < patternsToFindString.Length; i++)
                {
                    int thisIndex = strLoopStr.IndexOf(patternsToFindString[i]);
                    if ((thisIndex >= 0) && (thisIndex < beginIndexOfPattern))
                    {
                        beginIndexOfPattern = thisIndex;
                        indexFoundAtString = i;
                    }
                }
                if ((beginIndexOfPattern != strLoopStr.Length) && (indexFoundAtString != patternsToFindString.Length))
                {
                    string substring = strLoopStr.Substring(0, beginIndexOfPattern);

                    string restString = strLoopStr.Substring(beginIndexOfPattern + patternsToFindString[indexFoundAtString].Length);
                    int endIndexOfPattern = restString.IndexOf(")}]");
                    if (endIndexOfPattern >= 0)
                    {
                        returnString += substring;

                        if (endIndexOfPattern > 0)
                        {
                            string formatString = restString.Substring(0, endIndexOfPattern);
                            if (indexFoundAtString == 0)
                            {
                                returnString += thisPageNumber.ToString(formatString);
                            }
                            if (indexFoundAtString == 1)
                            {
                                returnString += (totPages > 0 ? totPages.ToString(formatString) : myPageCount.ToString(formatString));
                            }
                            if (indexFoundAtString == 2)
                            {
                                returnString += System.DateTime.Now.ToString(formatString);
                            }
                        }
                        else
                        {
                            if (indexFoundAtString == 0)
                            {
                                returnString += thisPageNumber.ToString();
                            }
                            if (indexFoundAtString == 1)
                            {
                                returnString += (totPages > 0 ? totPages.ToString() : myPageCount.ToString());
                            }
                            if (indexFoundAtString == 2)
                            {
                                returnString += System.DateTime.Now.ToString();
                            }
                        }
                        restString = restString.Substring(endIndexOfPattern + 3);
                    }
                    else
                    {
                        returnString += substring + patternsToFindString[indexFoundAtString];
                    }
                    strLoopStr = restString;
                }
                else
                {
                    returnString += strLoopStr;
                    return returnString;
                }
            }

        }

        private FooterInfo GetFooter(List<FooterInfo> lPDFFooter, int thisPageNumber, int thisPageSubWidth, int thisPageSubHeight, int thisPageMaxWidth, int thisPageMaxHeight)
        {
            if (lPDFFooter != null)
            {
                foreach (FooterInfo footer in lPDFFooter)
                {
                    if (((footer.FromPage <= thisPageNumber) || (footer.FromPage == 0)) && ((footer.ToPage >= thisPageNumber) || (footer.ToPage == 0)))
                    {
                        return GetTransformed(footer, thisPageNumber, thisPageSubWidth, thisPageSubHeight, thisPageMaxWidth, thisPageMaxHeight);
                    }
                }
            }
            return null;
        }

        private FooterInfo GetTransformed(FooterInfo footer, int thisPageNumber, int thisPageSubWidth, int thisPageSubHeight, int thisPageMaxWidth, int thisPageMaxHeight)
        {
            FooterInfo retInfo = new FooterInfo();
            retInfo.CopyExceptText(footer);
            if (footer.CurrentPageSize == gloExtendedPrinterSettings.PageSize.None)
            {
                retInfo.CurrentPageSize = _ExtendedPrinterSettings.CurrentPageSize;
            }
            int startingNumber = footer.StartingPage > 0 ? footer.StartingPage + thisPageNumber - (footer.FromPage == 0 ? 1 : footer.FromPage) : thisPageNumber;
            retInfo.LeftText = GetTransformed(footer.LeftText, startingNumber, footer.TotalPages, thisPageSubWidth, thisPageSubHeight, thisPageMaxWidth, thisPageMaxHeight);
            retInfo.RightText = GetTransformed(footer.RightText, startingNumber, footer.TotalPages, thisPageSubWidth, thisPageSubHeight, thisPageMaxWidth, thisPageMaxHeight);
            retInfo.CenterText = GetTransformed(footer.CenterText, startingNumber, footer.TotalPages, thisPageSubWidth, thisPageSubHeight, thisPageMaxWidth, thisPageMaxHeight);
            return retInfo;
        }

        private string GetTransformed(string p, int thisPageNumber, int totPages, int thisPageSubWidth, int thisPageSubHeight, int thisPageMaxWidth, int thisPageMaxHeight)
        {
            string returnString = "";
            string strLoopStr = p;
            string[] patternsToFindString = { "[{PAGE(", "[{TOTAL(", "[{DATE(" };
            int myPageCount = 1;
            if (_PDFFile)
            {
                if (_PDFDoc != null)
                {
                    myPageCount = _PDFDoc.GetPageCount();
                }
            }
            else
            {
                myPageCount = myEmfImage.Count;
            }
            while (true)
            {
                int beginIndexOfPattern = strLoopStr.Length;
                if (beginIndexOfPattern == 0) return returnString;
                int indexFoundAtString = patternsToFindString.Length;
                for (int i = 0; i < patternsToFindString.Length; i++)
                {
                    int thisIndex = strLoopStr.IndexOf(patternsToFindString[i]);
                    if ((thisIndex >= 0) && (thisIndex < beginIndexOfPattern))
                    {
                        beginIndexOfPattern = thisIndex;
                        indexFoundAtString = i;
                    }
                }
                if ((beginIndexOfPattern != strLoopStr.Length) && (indexFoundAtString != patternsToFindString.Length))
                {
                    string substring = strLoopStr.Substring(0, beginIndexOfPattern);

                    string restString = strLoopStr.Substring(beginIndexOfPattern + patternsToFindString[indexFoundAtString].Length);
                    int endIndexOfPattern = restString.IndexOf(")}]");
                    if (endIndexOfPattern >= 0)
                    {
                        returnString += substring;

                        if (endIndexOfPattern > 0)
                        {
                            string formatString = restString.Substring(0, endIndexOfPattern);
                            if (indexFoundAtString == 0)
                            {
                                returnString += thisPageNumber.ToString(formatString);
                                if (thisPageMaxWidth > 1)
                                {
                                    returnString += "[" + (thisPageSubWidth + 1).ToString(formatString) + "/" + thisPageMaxWidth.ToString(formatString) + "]";
                                }
                                if (thisPageMaxHeight > 1)
                                {
                                    returnString += "[" + (thisPageSubHeight + 1).ToString(formatString) + "/" + thisPageMaxHeight.ToString(formatString) + "]";
                                }
                            }
                            if (indexFoundAtString == 1)
                            {
                                returnString += (totPages > 0 ? totPages.ToString(formatString) : myPageCount.ToString(formatString));
                            }
                            if (indexFoundAtString == 2)
                            {
                                returnString += System.DateTime.Now.ToString(formatString);
                            }
                        }
                        else
                        {
                            if (indexFoundAtString == 0)
                            {
                                returnString += thisPageNumber.ToString();
                                if (thisPageMaxWidth > 1)
                                {
                                    returnString += "[" + (thisPageSubWidth + 1).ToString() + "/" + thisPageMaxWidth.ToString() + "]";
                                }
                                if (thisPageMaxHeight > 1)
                                {
                                    returnString += "[" + (thisPageSubHeight + 1).ToString() + "/" + thisPageMaxHeight.ToString() + "]";
                                }

                            }
                            if (indexFoundAtString == 1)
                            {
                                returnString += (totPages > 0 ? totPages.ToString() : myPageCount.ToString());
                            }
                            if (indexFoundAtString == 2)
                            {
                                returnString += System.DateTime.Now.ToString();
                            }
                        }
                        restString = restString.Substring(endIndexOfPattern + 3);
                    }
                    else
                    {
                        returnString += substring + patternsToFindString[indexFoundAtString];
                    }
                    strLoopStr = restString;
                }
                else
                {
                    returnString += strLoopStr;
                    return returnString;
                }
            }

        }


        //public static String SetDefaultFontForDocumentFields(String PDFFileName)
        //{
        //    try
        //    {
        //        gloRecoverPDF.ConnectToPDFTron();
        //        PDFDoc _pdfdoc = new PDFDoc(PDFFileName);
        //        _pdfdoc.InitSecurityHandler();

        //        pdftron.PDF.Font fnt = null;
        //        Obj formObj = null;
        //        Obj dr = null;
        //        Obj font_dict = null;
        //        Obj pdffont = null;
        //        Obj pdffont_dict = null;
        //        try
        //        {
        //            formObj = _pdfdoc.GetAcroForm();
        //            if (formObj != null)
        //            {
        //                dr = formObj.FindObj("DR");
        //                if (dr == null) dr = formObj.PutDict("DR");
        //                font_dict = dr.FindObj("Font");
        //                if (font_dict == null) font_dict = dr.PutDict("Font");
        //                pdffont = font_dict.FindObj("MyFont");
        //                if (pdffont == null)
        //                {
        //                    fnt = pdftron.PDF.Font.Create(_pdfdoc, pdftron.PDF.Font.StandardType1Font.e_times_roman, true);
        //                    pdffont = fnt.GetSDFObj();
        //                    pdffont_dict = font_dict.Put("MyFont", pdffont);
        //                    Obj o = null;
        //                    Obj m = null;
        //                    for (FieldIterator fi = _pdfdoc.GetFieldIterator(); fi.HasNext(); fi.Next())
        //                    {
        //                        Field currentField = fi.Current();
        //                        if (currentField.GetType() == Field.Type.e_text)
        //                        {
        //                            o = currentField.GetSDFObj();
        //                            if (o != null)
        //                            {
        //                                m = o.PutString("DA", "/MyFont 10 Tf 0 0 0 rg ");
        //                            }
        //                            try
        //                            {
        //                                currentField.RefreshAppearance();
        //                            }
        //                            catch
        //                            {
        //                            }
        //                        }
        //                    }
        //                    o = null;
        //                    m = null;
        //                    String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Guid.NewGuid().ToString() + ".pdf");
        //                    _pdfdoc.Save(filePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
        //                    return filePath;
        //                }
        //                else
        //                {
        //                    return PDFFileName;
        //                }
        //            }
        //            else
        //            {
        //                return PDFFileName;
        //            }
        //        }
        //        catch
        //        {

        //            return PDFFileName;

        //        }
        //        finally
        //        {
        //            if (fnt != null) { fnt.Dispose(); fnt = null; }
        //            if (formObj != null) { formObj.Dispose(); formObj = null; }
        //            if (dr != null) { dr.Dispose(); dr = null; }
        //            if (font_dict != null) { font_dict.Dispose(); font_dict = null; }
        //            if (pdffont != null) { pdffont.Dispose(); pdffont = null; }
        //            if (pdffont_dict != null) { pdffont_dict.Dispose(); pdffont_dict = null; }
        //            if (_pdfdoc != null) { _pdfdoc.Dispose(); _pdfdoc = null; }
        //            gloRecoverPDF.DisconnectToPDFTron();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return PDFFileName;
        //}

        private static bool errorHappened = false;

        //public static String RecoverIfNotSupported(String PDFFileName, ref int PageCount, ref int Error)
        //{

        //    String FilePath = SetDefaultFontForDocumentFields(PDFFileName);
        //    try
        //    {
        //        if (IsService)
        //        {
        //            String thisAssemblyLoaderPath = Assembly.GetExecutingAssembly().CodeBase;
        //            String thisAssemblyNameSpace = typeof(gloPrintProgressController).FullName;

        //            AppDomain privateDomainAddress = AppDomain.CreateDomain("gloPrintDialog");

        //            AssemblyName privateAssemblyName = new AssemblyName();
        //            privateAssemblyName.CodeBase = thisAssemblyLoaderPath;
        //            try
        //            {
        //                Assembly ptrAssembly = privateDomainAddress.Load(privateAssemblyName);

        //                try
        //                {

        //                    Type privategloPrintDialog = ptrAssembly.GetType(thisAssemblyNameSpace);
        //                    dynamic privateClass = Activator.CreateInstance(privategloPrintDialog);
        //                    try
        //                    {
        //                        bool noErrors = privateClass.IsSupportedPDF(FilePath, ref PageCount);
        //                        if (noErrors)
        //                        {
        //                            bool execConversion = privateClass.SuccessfullConversion();
        //                            if (!execConversion)
        //                            {
        //                                errorHappened = true;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            errorHappened = true;
        //                        }
        //                    }
        //                    catch
        //                    {
        //                        errorHappened = true;
        //                    }
        //                    try
        //                    {
        //                        privateClass.Dispose();
        //                    }
        //                    catch
        //                    {
        //                    }
        //                    privategloPrintDialog = null;
        //                }
        //                catch
        //                {
        //                    errorHappened = true;
        //                }
        //                AppDomain.Unload(privateDomainAddress);
        //                privateAssemblyName = null;
        //                privateDomainAddress = null;
        //                ptrAssembly = null;
        //            }
        //            catch
        //            {
        //                errorHappened = true;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {

        //                if (IsFileSupported(FilePath, ref PageCount))
        //                {
        //                    if (!executedConversion)
        //                    {
        //                        errorHappened = true;
        //                    }
        //                }
        //                else
        //                {
        //                    errorHappened = true;
        //                }
        //            }
        //            catch
        //            {
        //                errorHappened = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        errorHappened = true;
        //    }
        //    //using (PDFDoc in_doc = new PDFDoc(FilePath))
        //    //{
        //    //    in_doc.InitSecurityHandler();

        //    //    using (PDFDraw draw = new PDFDraw())
        //    //    {

        //    //        in_doc.Lock();
        //    //        draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //    //        draw.SetDPI(72);
        //    //        // Add the event handler for handling UI thread exceptions to the event.
        //    //        Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
        //    //        // Set the unhandled exception mode to force all Windows Forms errors to go through 
        //    //        // our handler.
        //    //        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
        //    //        errorHappened = false;
        //    //        for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //    //        {
        //    //            if (errorHappened)
        //    //            {
        //    //                break;
        //    //            }
        //    //            Page currentPage = itr.Current();



        //    //            try
        //    //            {

        //    //                double Width = currentPage.GetPageWidth();
        //    //                double Height = currentPage.GetPageHeight();
        //    //                using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //    //                {
        //    //                    using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //    //                    {
        //    //                        try
        //    //                        {
        //    //                            draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //    //                        }
        //    //                        catch (PDFNetException)
        //    //                        {
        //    //                            errorHappened = true;

        //    //                        }
        //    //                        catch (AccessViolationException)
        //    //                        {
        //    //                            errorHappened = true;

        //    //                        }
        //    //                    }

        //    //                }

        //    //            }
        //    //            catch
        //    //            {
        //    //                errorHappened = true;
        //    //            }

        //    //        }


        //    //        AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnHandler);
        //    //        Application.ThreadException -= new ThreadExceptionEventHandler(UIThreadException);
        //    //        in_doc.Unlock();
        //    //    }
        //    //}

        //    if (errorHappened)
        //    {

        //        gloRecoverPDF.ConnectToPDFTron();
        //        using (PDFDoc in_doc = new PDFDoc(FilePath))
        //        {
        //            in_doc.InitSecurityHandler();

        //            //using (PDFDraw draw = new PDFDraw())
        //            PDFDraw draw = new PDFDraw();
        //            {

        //                in_doc.Lock();

        //                draw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
        //                draw.SetDPI(600);
        //                PDFDoc new_doc = new PDFDoc();
        //                bool bHasCreatedImage = true;
        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                {
        //                    if (bHasCreatedImage == false) break;
        //                    Page currentPage = itr.Current();
        //                    //try
        //                    //{

        //                    //    Bitmap pdfBitmap = draw.GetBitmap(currentPage);
        //                    //    String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                    //    pdfBitmap.Save(NewBmp);
        //                    //    AddImage(ref new_doc, pdfBitmap);
        //                    //    pdfBitmap.Dispose();
        //                    //}
        //                    //catch
        //                    //{

        //                    try
        //                    {
        //                        double Width = currentPage.GetPageWidth();
        //                        double Height = currentPage.GetPageHeight();


        //                        using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                        {
        //                            //using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                            Graphics pdfGraphics = Graphics.FromImage(pdfBitmap);
        //                            {
        //                                //SLR: For Bad PDF sometime, following call hangs and hence written a thread to execute it.
        //                                //try
        //                                //{
        //                                //    draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                //}
        //                                //catch
        //                                //{
        //                                //}
        //                                int threadId;
        //                                gloAsyncOperation AsyncDraw = new gloAsyncOperation();

        //                                // Create the delegate.
        //                                AsyncMethodCaller caller = new AsyncMethodCaller(AsyncDraw.DrawMethod);
        //                                AsyncCallback myCallBackmethod = new AsyncCallback(CallDrawBackMethod);
        //                                // Initiate the asychronous call.
        //                                IAsyncResult result = caller.BeginInvoke(draw, currentPage, pdfGraphics, Width, Height, out threadId, myCallBackmethod, null);
        //                                // Poll while simulating work.
        //                                int noOfTimes = 5;
        //                                while ( (result.IsCompleted == false) && (noOfTimes > 0 ) )
        //                                {
        //                                    Thread.Sleep(1000);
        //                                    noOfTimes--;
        //                                }

        //                                if (result.IsCompleted)
        //                                {
        //                                    //String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                                    //pdfBitmap.Save(NewBmp);
        //                                    AddImage(ref new_doc, pdfBitmap);
        //                                }
        //                                else
        //                                {
        //                                    bHasCreatedImage = false;
        //                                }
        //                            }
        //                            pdfGraphics.Dispose();

        //                        }
        //                    }
        //                    catch
        //                    {
        //                    }
        //                    //}
        //                }
        //                in_doc.Unlock();
        //                if (bHasCreatedImage)
        //                {
        //                    String NewFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Guid.NewGuid().ToString() + ".pdf");
        //                    new_doc.Save(NewFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
        //                    new_doc.Dispose();
        //                    new_doc = null;
        //                    gloRecoverPDF.DisconnectToPDFTron();
        //                    draw.Dispose();
        //                    Error = 0;
        //                    return NewFilePath;
        //                }
        //                else
        //                {
        //                    draw.Dispose();
        //                    Error = 1;
        //                    return FilePath;
        //                }
        //            }
        //            //draw.Dispose();

        //        }

        //    }
        //    else
        //    {
        //        return FilePath;
        //        Error = 0;
        //    }

        //}
        //// The callback method must have the same signature as the
        //// AsyncCallback delegate.
        //static void CallDrawBackMethod(IAsyncResult ar)
        //{
        //    // Retrieve the delegate.
        //    AsyncResult result = (AsyncResult)ar;
        //    AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;


        //    // Retrieve the format string that was passed as state 
        //    // information.
        //    string formatString = (string)ar.AsyncState;

        //    // Define a variable to receive the value of the out parameter.
        //    // If the parameter were ref rather than out then it would have to
        //    // be a class-level field so it could also be passed to BeginInvoke.
        //    int threadId = 0;

        //    // Call EndInvoke to retrieve the results.
        //    string returnValue = caller.EndInvoke(out threadId, ar);

        //}

        //private static bool executedConversion = false;

        //public bool SuccessfullConversion()
        //{
        //    return executedConversion;
        //}

        //public bool IsSupportedPDF(String PdfFileName, ref int PageCount)
        //{
        //    return IsFileSupported(PdfFileName, ref PageCount);
        //}

        //public static bool IsFileSupported(String PDFFileName, ref int PageCount)
        //{
        //    executedConversion = false;
        //    try
        //    {
        //        gloRecoverPDF.ConnectToPDFTron();


        //        using (PDFDoc in_doc = new PDFDoc(PDFFileName))
        //        {
        //            PageCount = in_doc.GetPageCount();
        //            in_doc.InitSecurityHandler();

        //            using (PDFDraw draw = new PDFDraw())
        //            {

        //                in_doc.Lock();
        //                draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //                draw.SetDPI(72);
        //                // Add the event handler for handling UI thread exceptions to the event.
        //                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
        //                // Set the unhandled exception mode to force all Windows Forms errors to go through 
        //                // our handler.
        //                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
        //                errorHappened = false;
        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext();)
        //                {
        //                    if (errorHappened)
        //                    {
        //                        break;
        //                    }
        //                    Page currentPage = itr.Current();



        //                    try
        //                    {

        //                        double Width = currentPage.GetPageWidth();
        //                        double Height = currentPage.GetPageHeight();
        //                        using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                        {
        //                            using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                            {
        //                                try
        //                                {
        //                                    draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                }
        //                                catch (PDFNetException)
        //                                {
        //                                    errorHappened = true;

        //                                }
        //                                catch (AccessViolationException)
        //                                {
        //                                    errorHappened = true;

        //                                }
        //                            }

        //                        }

        //                    }
        //                    catch
        //                    {
        //                        errorHappened = true;
        //                    }
        //                    break;
        //                }
        //                draw.SetDPI(600);

        //                AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnHandler);
        //                Application.ThreadException -= new ThreadExceptionEventHandler(UIThreadException);
        //                in_doc.Unlock();
        //                executedConversion = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        gloRecoverPDF.DisconnectToPDFTron();
        //    }
        //    return (!errorHappened);
        //}

        private static void UnHandler(object sender, UnhandledExceptionEventArgs args)
        {

            errorHappened = true;
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHappened = true;
        }

        //private static void AddImage(ref PDFDoc doc, System.Drawing.Bitmap bimp)
        //{
        //    using (ElementBuilder bld = new ElementBuilder())   // Used to build new Element objects
        //    {
        //        using (ElementWriter writer = new ElementWriter())  // Used to write Elements to the page   
        //        {
        //            Page page = doc.PageCreate();   // Start a new page 
        //            writer.Begin(page);             // Begin writing to this page
        //            using (pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc.GetSDFDoc(), bimp))
        //            {
        //                Element element = bld.CreateImage(img, 0.0, 0.0, (double)bimp.Width * 72.0 / 100.0, (double)bimp.Height * 72.0 / 100.0);
        //                writer.WritePlacedElement(element);
        //                writer.End();   // Finish writing to the page
        //                doc.PagePushBack(page);
        //            }
        //            //String newFilePath =     Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        //            //doc.Save(newFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);

        //        }
        //    }

        //}


        private RectangleF measurePrinterHorizontalBounds;
        private RectangleF measurePrinterVerticalBounds;

        private bool bReCalcmeasureBounds = true;

        //private void GetRealMeasurePageBoundsInPoints(Graphics gr, FooterInfo footer)
        //{
        //    if (bReCalcmeasureBounds)
        //    {
        //        // Translate to units of 1/100 inch
        //        RectangleF vpb = gr.VisibleClipBounds;
        //        PointF[] bottomRight = { new PointF(vpb.Size.Width, vpb.Size.Height) };
        //        gr.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);
        //        float dpiX = gr.DpiX;
        //        float dpiY = gr.DpiY;
        //        float cx = _oPrintDocument.DefaultPageSettings.HardMarginX;
        //        float cy = _oPrintDocument.DefaultPageSettings.HardMarginY;
        //        int PrintPageHorizontalIndex = 1;
        //        int PrintPageVerticalIndex = 1;

        //        if (footer != null)
        //        {
        //            if (footer.PrintFooter)
        //            {
        //                _footerFontHeight = gloExtendedPrinterSettings.GetFooterFontSize(footer.CurrentPageSize, gr, _ExtendedPrinterSettingsFooterFontBy);
        //            }
        //            else
        //            {
        //                _footerFontHeight = 0;
        //            }
        //            PrintPageHorizontalIndex = gloExtendedPrinterSettings.GetHorizontalPagesCount(footer.CurrentPageSize);
        //            PrintPageVerticalIndex = gloExtendedPrinterSettings.GetVerticalPagesCount(footer.CurrentPageSize);
        //        }
        //        else
        //        {
        //            _footerFontHeight = gloExtendedPrinterSettings.GetFooterFontSize(_ExtendedPrinterSettings.CurrentPageSize, gr, _ExtendedPrinterSettingsFooterFontBy);
        //        }
        //        float width = ((bottomRight[0].X + _ExtendedPrinterSettings.HorizontalGutter) / PrintPageHorizontalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
        //        float height = ((bottomRight[0].Y + (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight)) / PrintPageVerticalIndex) - (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight);

        //        measurePrinterHorizontalBounds = new RectangleF((-cx * 72 / dpiX) + _ExtendedPrinterSettings.PrinterMarginsLeft, (-cy * 72 / dpiY) + _ExtendedPrinterSettings.PrinterMarginsTop, ((width) * 72 / dpiX) - _ExtendedPrinterSettings.PrinterMarginsRight - _ExtendedPrinterSettings.PrinterMarginsLeft, ((height) * 72 / dpiY) - _ExtendedPrinterSettings.PrinterMarginsBottom - _ExtendedPrinterSettings.PrinterMarginsTop - _footerFontHeight);

        //        width = ((bottomRight[0].Y + _ExtendedPrinterSettings.HorizontalGutter) / PrintPageVerticalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
        //        height = ((bottomRight[0].X + (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight)) / PrintPageHorizontalIndex) - (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight);

        //        measurePrinterVerticalBounds = new RectangleF((-cx * 72 / dpiX) + _ExtendedPrinterSettings.PrinterMarginsTop, (-cy * 72 / dpiY) + _ExtendedPrinterSettings.PrinterMarginsLeft, ((width) * 72 / dpiX) - _ExtendedPrinterSettings.PrinterMarginsBottom - _ExtendedPrinterSettings.PrinterMarginsTop, ((height) * 72 / dpiY) - _ExtendedPrinterSettings.PrinterMarginsRight - _ExtendedPrinterSettings.PrinterMarginsLeft - _footerFontHeight);

        //        bReCalcmeasureBounds = false;
        //    }

        //}

        private void GetRealMeasurePageBoundsInPoints(gloExtendedPrinterSettings.GraphicsBound gr, FooterInfo footer)
        {
            if (bReCalcmeasureBounds)
            {

                int PrintPageHorizontalIndex = 1;
                int PrintPageVerticalIndex = 1;

                if (footer != null)
                {
                    if (footer.PrintFooter)
                    {
                        _footerFontHeight = gloExtendedPrinterSettings.GetFooterFontSize(footer.CurrentPageSize, gr);
                    }
                    else
                    {
                        _footerFontHeight = 0;
                    }
                    PrintPageHorizontalIndex = gloExtendedPrinterSettings.GetHorizontalPagesCount(footer.CurrentPageSize);
                    PrintPageVerticalIndex = gloExtendedPrinterSettings.GetVerticalPagesCount(footer.CurrentPageSize);
                }
                else
                {
                    _footerFontHeight = 0;
                    PrintPageHorizontalIndex = gloExtendedPrinterSettings.GetHorizontalPagesCount(_ExtendedPrinterSettings.CurrentPageSize);
                    PrintPageVerticalIndex = gloExtendedPrinterSettings.GetVerticalPagesCount(_ExtendedPrinterSettings.CurrentPageSize);

                    //_footerFontHeight = gloExtendedPrinterSettings.GetFooterFontSize(_ExtendedPrinterSettings.CurrentPageSize, gr);
                }
                float width = (((gr.Right * 72 / gr.DpiX) + _ExtendedPrinterSettings.HorizontalGutter) / PrintPageHorizontalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
                float height = (((gr.Bottom * 72 / gr.DpiY) + (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight)) / PrintPageVerticalIndex) - (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight);

                measurePrinterHorizontalBounds = new RectangleF((-gr.Left * 72 / gr.DpiX) + _ExtendedPrinterSettings.PrinterMarginsLeft, (-gr.Top * 72 / gr.DpiY) + _ExtendedPrinterSettings.PrinterMarginsTop, ((width)) - _ExtendedPrinterSettings.PrinterMarginsRight - _ExtendedPrinterSettings.PrinterMarginsLeft, ((height)) - _ExtendedPrinterSettings.PrinterMarginsBottom - _ExtendedPrinterSettings.PrinterMarginsTop - _footerFontHeight);

                width = (((gr.Bottom * 72 / gr.DpiY) + _ExtendedPrinterSettings.HorizontalGutter) / PrintPageVerticalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
                height = (((gr.Right * 72 / gr.DpiX) + (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight)) / PrintPageHorizontalIndex) - (_ExtendedPrinterSettings.VerticalGutter + _footerFontHeight);

                measurePrinterVerticalBounds = new RectangleF((-gr.Left * 72 / gr.DpiX) + _ExtendedPrinterSettings.PrinterMarginsTop, (-gr.Top * 72 / gr.DpiY) + _ExtendedPrinterSettings.PrinterMarginsLeft, ((width)) - _ExtendedPrinterSettings.PrinterMarginsBottom - _ExtendedPrinterSettings.PrinterMarginsTop, ((height)) - _ExtendedPrinterSettings.PrinterMarginsRight - _ExtendedPrinterSettings.PrinterMarginsLeft - _footerFontHeight);

                bReCalcmeasureBounds = false;
            }

        }

        // Get real page bounds based on printable area of the page
        //public gloExtendedPrinterSettings.GraphicsBound GetGraphicsBound(PrinterSettings prSettings)
        //{
        //    try
        //    {
        //        using (Graphics thisGraphics = prSettings.CreateMeasurementGraphics(prSettings.DefaultPageSettings))
        //        {
        //            gloExtendedPrinterSettings.GraphicsBound thisGraphicsBound = new gloExtendedPrinterSettings.GraphicsBound();
        //            GraphicsUnit myUnit = thisGraphics.PageUnit;
        //            thisGraphics.PageUnit = GraphicsUnit.Point;
        //            // Translate to units of 1/100 inch
        //            RectangleF vpb = thisGraphics.VisibleClipBounds;
        //            PointF[] bottomRight = { new PointF(vpb.Size.Width, vpb.Size.Height) };
        //            thisGraphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);

        //            thisGraphicsBound.Right = bottomRight[0].X;
        //            thisGraphicsBound.Bottom = bottomRight[0].Y;
        //            thisGraphicsBound.Left = prSettings.DefaultPageSettings.HardMarginX;
        //            thisGraphicsBound.Top = prSettings.DefaultPageSettings.HardMarginY;
        //            thisGraphicsBound.DpiX = thisGraphics.DpiX;
        //            thisGraphicsBound.DpiY = thisGraphics.DpiY;
        //            if (_ExtendedPrinterSettingsFooterFontBy != null)
        //            {
        //                for (int scaling = 0; scaling < 3; scaling++)
        //                {
        //                    thisGraphicsBound.FontHeight[scaling] = _ExtendedPrinterSettingsFooterFontBy[scaling].GetHeight(thisGraphics);
        //                }
        //            }
        //            thisGraphicsBound.bValuesAssigned = true;
        //            thisGraphics.PageUnit = myUnit;
        //            return thisGraphicsBound;
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        //private static RectangleF rectPrinterBounds;
        //private static bool bReCalcPrinterBounds = true;
        //// Get real page bounds based on printable area of the page
        //private static RectangleF GetRealPageBoundsInPoints(PrintPageEventArgs e)
        //{
        //    if (bReCalcPrinterBounds)
        //    {
        //        // Translate to units of 1/100 inch
        //        RectangleF vpb = e.Graphics.VisibleClipBounds;
        //        PointF[] bottomRight = { new PointF(vpb.Size.Width, vpb.Size.Height) };
        //        e.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);
        //        float dpiX = e.Graphics.DpiX;
        //        float dpiY = e.Graphics.DpiY;
        //        float cx = e.PageSettings.HardMarginX;
        //        float cy = e.PageSettings.HardMarginY;
        //        System.Drawing.Font curFooterFont = _ExtendedPrinterSettingsFooterFont == null ? System.Drawing.SystemFonts.CaptionFont : _ExtendedPrinterSettingsFooterFont;

        //        _footerFontHeight = curFooterFont.GetHeight(e.Graphics)*72;

        //        float width = ((bottomRight[0].X + _ExtendedPrinterSettings.HorizontalGutter) / _printPageHorizontalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
        //        float height = ((bottomRight[0].Y + (_ExtendedPrinterSettings.VerticalGutter+_footerFontHeight)) / _printPageVerticalIndex) - (_ExtendedPrinterSettings.VerticalGutter+_footerFontHeight);

        //        rectPrinterBounds = new RectangleF((-cx * 72 / dpiX) + _ExtendedPrinterSettings.PrinterMarginsLeft, (-cy * 72 / dpiY) + _ExtendedPrinterSettings.PrinterMarginsTop, ((width ) * 72 / dpiX) - _ExtendedPrinterSettings.PrinterMarginsRight + _ExtendedPrinterSettings.PrinterMarginsLeft, ((height ) * 72 / dpiY) - _ExtendedPrinterSettings.PrinterMarginsBottom + _ExtendedPrinterSettings.PrinterMarginsTop - _footerFontHeight);
        //        bReCalcPrinterBounds = false;
        //    }
        //    return rectPrinterBounds;
        //}
        //private static RectangleF rectFooterBounds ;
        //private static bool bReCalcFooterBounds = true;
        //private static System.Drawing.Font fntFooterFont = null;
        //// Get real Footer bounds based on printable area of the page
        //private static RectangleF GetRealFooterBounds(PrintPageEventArgs e)
        //{
        //    if (bReCalcFooterBounds)
        //    {
        //        // Translate to units of 1/100 inch
        //        fntFooterFont = _ExtendedPrinterSettingsFooterFont == null ? System.Drawing.SystemFonts.CaptionFont : _ExtendedPrinterSettingsFooterFont;

        //        RectangleF vpb = e.Graphics.VisibleClipBounds;
        //        PointF[] bottomRight = { new PointF(vpb.Size.Width, vpb.Size.Height) };
        //        e.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);
        //      //  RectangleF pb = e.PageBounds;
        //        float fh = fntFooterFont.GetHeight(e.Graphics); 
        //        float hx = fh / fntFooterFont.Height;
        //        float dpiX = e.Graphics.DpiX;
        //        float dpiY = e.Graphics.DpiY;
        //        float cx = e.PageSettings.HardMarginX;
        //        float cy = e.PageSettings.HardMarginY;
        //        float width = ((bottomRight[0].X + _ExtendedPrinterSettings.HorizontalGutter) / _printPageHorizontalIndex) - _ExtendedPrinterSettings.HorizontalGutter;
        //        float height = ((bottomRight[0].Y + (_ExtendedPrinterSettings.VerticalGutter+fh)) / _printPageVerticalIndex) - (_ExtendedPrinterSettings.VerticalGutter+fh);

        //        rectFooterBounds = new RectangleF((0 * 72 / dpiY) + _ExtendedPrinterSettings.FooterLeft, ((height-cy) * 72 / dpiY) - fh  - _ExtendedPrinterSettings.FooterBottom, ((width-cx) * 72 / dpiX)  - _ExtendedPrinterSettings.FooterRight, fh+_ExtendedPrinterSettings.FooterTop); 
        //        bReCalcFooterBounds = false;
        //    }

        //    return rectFooterBounds;
        //}
        //// Get real Footer bounds based on printable area of the page
        //private static void PrintFooter(PrintPageEventArgs e, String strText)
        //{
        //    GraphicsUnit myUnit = e.Graphics.PageUnit;
        //    e.Graphics.PageUnit= GraphicsUnit.Point;
        //    RectangleF realFooterPounds = GetRealFooterBounds(e );

        //    e.Graphics.DrawString(strText, fntFooterFont == null ? System.Drawing.SystemFonts.CaptionFont : fntFooterFont, Brushes.Black, realFooterPounds.Left, realFooterPounds.Top);

        //    e.Graphics.PageUnit = myUnit; 
        //}
        //
        private static StringFormat GetStringFormatFromContentAllignment(ContentAlignment ca)
        {
            StringFormat format = new StringFormat();


            Int32 lNum = LogBase2((long)ca); //(Int32)Math.Log((Double)ca, 2);
            Int32 lNumBy4 = lNum >> 2;
            Int32 lNumMod4 = (lNum & 0x03);
            format.LineAlignment = (StringAlignment)(lNumBy4); // (StringAlignment)(lNum / 4);

            format.Alignment = (StringAlignment)(lNumMod4); //(StringAlignment)(lNum % 4);
            format.FormatFlags = StringFormatFlags.FitBlackBox;

            return format;
        }
        private static Int32 LogBase2(long number)
        {
            Int32 bits = 0;
            if (number > 0xffff)
            {
                number >>= 16;
                bits = 0x10;
            }

            if (number > 0xff)
            {
                number >>= 8;
                bits |= 0x8;
            }

            if (number > 0xf)
            {
                number >>= 4;
                bits |= 0x4;
            }

            if (number > 0x3)
            {
                number >>= 2;
                bits |= 0x2;
            }

            if (number > 0x1)
            {
                bits |= 0x1;
            }
            return bits;
        }
        // Get real Footer bounds based on printable area of the page
        private void PrintFooter(PrintPageEventArgs e, gloPrintProgressController.FooterInfo footer, RectangleF realFooterBounds) //,   RectangleF realPageBounds)
        {
            try
            {
                if (footer != null)
                {
                    if (footer.PrintFooter)
                    {
                        //RectangleF realFooterPounds = new RectangleF(realFooterBounds.Location,realFooterBounds.Size);

                        GraphicsUnit myUnit = e.Graphics.PageUnit;
                        // if (_PDFFile)
                        {
                            e.Graphics.PageUnit = GraphicsUnit.Point;
                        }
                        //realFooterPounds.X += ((realPageBounds.Width + _ExtendedPrinterSettings.HorizontalGutter) * _printBreakHorizontalIndex) -10;
                        //realFooterPounds.Y += ((realPageBounds.Height + (_ExtendedPrinterSettings.VerticalGutter+_footerFontHeight)) * _printBreakVerticalIndex) ;

                        System.Drawing.Font thisFont = gloExtendedPrinterSettings.GetFooterFont(footer.CurrentPageSize, _ExtendedPrinterSettingsFooterFontBy);
                        //   realFooterPounds = new RectangleF(realFooterPounds.Left, realFooterPounds.Top, e.PageBounds.Width*0.5f, realFooterPounds.Height);
                        if (footer.CurrentPageSize == gloExtendedPrinterSettings.PageSize.ActualPageSize)
                        {
                            using (SolidBrush labelBackColorBrush = new SolidBrush(gloGlobal.clsgloFont.BestForegroundColorForBackground(_ExtendedPrinterSettings.FooterColor)))
                            {
                                e.Graphics.FillRectangle(labelBackColorBrush, realFooterBounds);
                            }
                        }
                        using (SolidBrush labelForeColorBrush = new SolidBrush(_ExtendedPrinterSettings.FooterColor))
                        {
                            if (!string.IsNullOrEmpty(footer.LeftText))
                            {
                                using (StringFormat sf = GetStringFormatFromContentAllignment(ContentAlignment.MiddleLeft))
                                {

                                    e.Graphics.DrawString(footer.LeftText, thisFont, labelForeColorBrush, realFooterBounds, sf);
                                }
                            }

                            if (!string.IsNullOrEmpty(footer.CenterText))
                            {
                                using (StringFormat sf = GetStringFormatFromContentAllignment(ContentAlignment.MiddleCenter))
                                {

                                    e.Graphics.DrawString(footer.CenterText, thisFont, labelForeColorBrush, realFooterBounds, sf);
                                }
                            }
                            if (!string.IsNullOrEmpty(footer.RightText))
                            {
                                using (StringFormat sf = GetStringFormatFromContentAllignment(ContentAlignment.MiddleRight))
                                {

                                    e.Graphics.DrawString(footer.RightText, thisFont, labelForeColorBrush, realFooterBounds, sf);

                                }
                            }
                        }
                        e.Graphics.PageUnit = myUnit;
                    }
                }
            }
            catch
            {
            }
        }
        private volatile bool IsPrintingCanceled = false;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                IsPrintingCanceled = true;
                if (_gloStandardPrintController != null)
                {
                    _gloStandardPrintController.IsCancel = true;
                }
                if (IsInsidePrinting == false)
                {
                    InvokeCompleteUpdateControls();
                    InvokeCloseControls();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                IsPrintingResumed = false;
                IsPrintingCanceled = false;
                _gloPrintProgress.Progress._printPageIndex = 0;


                //if (btnPause.Text == "&Resume")
                //{
                //    btnPause.Text = "&Pause";
                //    IsPrintingResumed = false;
                //}

                btnPlay.Enabled = false;
                btnPause.Enabled = true;
                if (btnPause.Enabled == true)
                {
                    IsPrintingResumed = false;
                }


                ((Button)(sender)).Enabled = false;
                //if (_gloStandardPrintController != null)
                //{
                //    _gloStandardPrintController.IsRestart = true;
                //}
                //((Button)(sender)).Update();
                //btnPause.Update();
                //((Button)(sender)).ResumeLayout();
                //btnPause.ResumeLayout();
                PrintWithOrWithoutBackground();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }


        private volatile bool IsPrintingResumed = false;
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                //if (((Button)(sender)).Tag == "Pause")
                //{
                //((Button)(sender)).Image = null;
                //((Button)(sender)).Image = Properties.Resources.play;
                //((Button)(sender)).Tag = "Play";

                btnPause.Enabled = false;
                btnPlay.Enabled = true;
                IsPrintingResumed = true;
                btnRestart.Enabled = true;
                btnRestart.Update();

                btnRestart.ResumeLayout();
                //}
                //else
                //{
                //    ((Button)(sender)).Tag = "Pause";
                //    ((Button)(sender)).Image = null;
                //    ((Button)(sender)).Image  = Properties.Resources.pause;
                //    IsPrintingResumed = false;
                //    btnRestart.Enabled = false;
                //    ((Button)(sender)).Update();
                //    btnRestart.Update();
                //    ((Button)(sender)).ResumeLayout();
                //    btnRestart.ResumeLayout();
                //    if (_gloStandardPrintController != null)
                //    {
                //        _gloStandardPrintController.IsRestart = true;
                //    }
                //    PrintWithOrWithoutBackground();
                //}
            }
            catch (Exception ex)
            {
                if (!gloGlobal.clsMISC.IsService)
                {
                    MessageBox.Show("Pause throws error");
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPause.Enabled = true;
            btnPlay.Enabled = false;
            IsPrintingResumed = false;
            // btnRestart.Enabled = false;
            // ((Button)(sender)).Update();
            btnRestart.Update();
            // ((Button)(sender)).ResumeLayout();
            btnRestart.ResumeLayout();
            //if (_gloStandardPrintController != null)
            //{
            //    _gloStandardPrintController.IsRestart = true;
            //}
            PrintWithOrWithoutBackground();
        }

    }
    public class gloPrintControllerEventArgs : EventArgs
    {
        public object Key;
        public gloPrintProgressCounters Progress = new gloPrintProgressCounters();
        public List<gloResult> Result = new List<gloResult>();
        public bool success = true;
    }
    public class gloResult
    {
        public string ResultMethod = "";
        public int ResultType = 0;
        public object ResultObject;
    }
    public class gloPrintProgressCounters
    {
        public int _PrintPageTotal = 0;
        public int _printPageIndex = 0;
        public int _printPageBreakIndex = 0;
        public int _printPageBreakCountIndex = 0;
        public int _DocumentIndex = 0;
    }
}

