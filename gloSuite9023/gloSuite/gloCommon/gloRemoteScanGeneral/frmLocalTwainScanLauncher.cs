using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloRemoteScanGeneral;
using Saraff.Twain;
using System.Collections;
using System.IO;
using System.Diagnostics;
using gloScanWIA;
using WIA;
using FiScnUtildN;

namespace gloRemoteScanGeneral
{
   

    public partial class frmLocalTwainScanLauncher : Form
    {
        string sScanType = string.Empty;
        string sScaaningFilePath = string.Empty;
        public bool bFormIsOpened = false;

        gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog("gloTSPrint");

        public bool bFromCardScan = false;
        public bool bFromScanDocCardScan = false;
        public bool bInsideScanning
        { get; set; }
        public string pToBeScannedFilePath { get; set; }
        ScannerCurrentSettingsScannerSettings objScannerSettings = null;

        public int nProcessId = 0;
        private Twain32 SaraffTwainDevice = null;

        public delegate void ForEachImageScanned(object sender, gloTwainScanControllerEventArgs e);
        public event ForEachImageScanned OnForEachImageScanned;

        public delegate void ImageScanningDone(object sender, gloTwainScanningDoneEventArgs e);
        public event ImageScanningDone OnImageScanningDone;

        public delegate void ForEachWIAImageScanned(object sender, gloWIAScanControllerEventArgs e);
        public event ForEachWIAImageScanned OnForEachWIAImageScanned;

        public delegate void WIAImageScanningDone(object sender, gloWIAScanningDoneEventArgs e);
        public event WIAImageScanningDone OnWIAImageScanningDone;

        public bool IsWIAScanning { get; set; }

        public frmLocalTwainScanLauncher(ScannerCurrentSettingsScannerSettings _objScannerSettings)
        {
            InitializeComponent();

            objScannerSettings = _objScannerSettings;

        }

        private void frmLocalTwainScanLauncher_Load(object sender, EventArgs e)
        {
            //Run();
            try
            {
                if (IsWIAScanning)
                {
                    WIAImageScanning();

                    this.TopMost = false;
                    if (bFormIsOpened)
                    {
                        this.Close();
                    }
                    try
                    {
                        this.Dispose();
                    }
                    catch { }
                    //frmClipboard.objScanLauncher = null;
                    bInsideScanning = false;

                }
                else
                {
                    UpdateBackgroundControls();
                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
        }

        //public void ShowRemoteScanProgress()
        //{
        //    //Run();
        //}

        //private System.Timers.Timer WaitForScannerToCompleteTimer = null;
        //public void StartScanningControls()
        //{
        //    if ((SaraffTwainDevice != null) || (bInsideScanning))
        //    {
        //        //objScanLauncher.Close();
        //        //objScanLauncher.Dispose();
        //        //objScanLauncher = null;
        //        //StartWaitForScannerToCompleteTimer();
        //    }
        //    else
        //    {
        //        UpdateBackgroundControls();
        //    }

        //}

        //private void StartWaitForScannerToCompleteTimer()
        //{
        //    if (WaitForScannerToCompleteTimer == null)
        //    {
        //        WaitForScannerToCompleteTimer = new System.Timers.Timer();
        //        WaitForScannerToCompleteTimer.Elapsed += new System.Timers.ElapsedEventHandler(WaitForScannerToCompleteTimer_Elapsed);
        //        WaitForScannerToCompleteTimer.AutoReset = true;

        //        WaitForScannerToCompleteTimer.Interval = 5000;
        //        WaitForScannerToCompleteTimer.Enabled = true;
        //        WaitForScannerToCompleteTimer.Start();
        //        WaitForScannerToCompleteTimer_Elapsed(null, null);
        //    }
        //    gloGlobal.gloProgressAndClipboard.currentClipboardCopy = null;
        //    gloGlobal.gloProgressAndClipboard.bClipSet = false;
        //    gloGlobal.gloDataProgressBar.TotalPages = 0;
        //}

        private static ScannerDevice scannerDevice;
        public int Resolution { get; set; }
        
        //private class ScanSource
        //{
        //    public string Name { get; set; }
        //    public DocumentHandlingSelect DocumentHandlingSelect { get; set; }
        //}

        public static ScannerDevice ScannerDevice
        {
            get { return scannerDevice; }
            set
            {
                // Proxies zum einfachen Zugriff auf die WIA-Eigenschaften erstellen
                scannerDevice = value;

                // die verfügbaren Dokumentenquellen auflisten
                //List<ScanSource> scanSources = new List<ScanSource>();
                //if ((ScannerDevice.DeviceSettings.DocumentHandlingCapabilities & DocumentHandlingCapabilities.Feed) > 0)
                //    scanSources.Add(new ScanSource { Name = "Front", DocumentHandlingSelect = DocumentHandlingSelect.Feeder });
                //if ((ScannerDevice.DeviceSettings.DocumentHandlingCapabilities & DocumentHandlingCapabilities.Dup) > 0)
                //    scanSources.Add(new ScanSource { Name = "Duplex", DocumentHandlingSelect = DocumentHandlingSelect.Duplex });
                //if ((ScannerDevice.DeviceSettings.DocumentHandlingCapabilities & DocumentHandlingCapabilities.Flat) > 0)
                //    scanSources.Add(new ScanSource { Name = "Flat Bed", DocumentHandlingSelect = DocumentHandlingSelect.Flatbed });
                //this.comboBoxSource.DataSource = scanSources;
                //this.comboBoxSource.DisplayMember = "Name";
                //this.comboBoxSource.ValueMember = "DocumentHandlingSelect";
            }
        }
        
        public void WIAImageScanning()
        {
            try 
            {
                ScannerDevice = WiaDevice.GetFirstScannerDevice(objScannerSettings.ScannerName).AsScannerDevice();
            }
            // catch (Exception comException) { MessageBox.Show(WiaException.GetMessageFromComException(comException), "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch { }

            ScannerDevice = RemoteScanSettings.SetWIAScannerCap(ScannerDevice, objScannerSettings);
            //ScannerDevice = SetWIAScannerCap(ScannerDevice);

            PerformWIAScan(ScannerDevice.ShowUIForWIAScan(objScannerSettings.ShowUI, ScannerDevice));

          

        }


        public void CheckExtAndBarodeForWIA(Image img, out string sExt,out string IsBarcode)
        {
            sExt = ".png";
            IsBarcode = "DMS";

            if ((sScanType == "RCM"))
            {
                IsBarcode = (CheckBarcode(img));
            }

            if (objScannerSettings.ScanModeName == "BW")
            {
                sExt = ".png";
            }
            else if (objScannerSettings.ScanModeName != "BW")
            {
                sExt = ".jpeg";
            }

        }

        //public List<Image> PerformWIAScanning(Items objItem)
        //{

        //    List<Image> images = null;
        //    images = ScannerDevice.PerformScan(objItem);
        //    return images;
        //}

        public void PerformWIAScan(Items objItem)
        {
            //Image tempIMG;
            //string sExt = ".png";
            //string IsBarcode = "DMS";
            try
            {
                if (objItem != null)
                {
                    bool hasMorePages = true;
                    //bool bFirstScan = true;
                    // Scanvorgang durchführen
                    //iCnt = 0;
                    bInsideScanning = true;
                    //List<Image> images = null;
                    do
                    {
                        List<byte[]> images = null;
                        bool bScanned = false;

                        try
                        {
                            images = ScannerDevice.PerformScan(objItem);
                            bScanned = (images != null);
                        }
                        catch (Exception)
                        {
                            bScanned = false;
                            hasMorePages = false;
                        }

                        if (images.Count <= 0)
                        {
                            bScanned = false;
                            hasMorePages = false;
                        }

                        if (bScanned)
                        {
                            if (OnForEachWIAImageScanned != null)
                            {
                                gloWIAScanControllerEventArgs WIAEvent = new gloWIAScanControllerEventArgs();

                                if (images.Count > 0)
                                {
                                    for (int i = 0; i < images.Count; i++)
                                    {
                                        MemoryStream ms = new MemoryStream(images[i]);
                                        Image tempImg = null;
                                        if (ms != null)
                                        {
                                            tempImg = Image.FromStream(ms);

                                        }

                                        //MessageBox.Show(tempImg.RawFormat.Guid.ToString());

                                        WIAEvent.ScannedImg = tempImg;

                                        if ((sScanType == "RCM"))
                                        {
                                            WIAEvent.sBarcode = (CheckBarcode(tempImg));
                                        }

                                        if (objScannerSettings.ScanModeName == "BW")
                                        {
                                            WIAEvent.sExt = ".png";
                                        }
                                        else if (objScannerSettings.ScanModeName != "BW")
                                        {
                                            WIAEvent.sExt = ".jpeg";
                                        }

                                        //tempImg.Save(@"D:\LOG\" + System.Guid.NewGuid() + WIAEvent.sExt);


                                        OnForEachWIAImageScanned(this, WIAEvent);

                                        if (ms != null)
                                        {
                                            ms.Dispose(); ms = null;
                                        }
                                        if (tempImg != null)
                                        {
                                            tempImg.Dispose(); tempImg = null;
                                        }
                                    }

                                }
                            }
                            if (hasMorePages)
                            {
                                try
                                {
                                    hasMorePages = (ScannerDevice.DeviceSettings.DocumentHandlingStatus & DocumentHandlingStatus.FeedReady) == DocumentHandlingStatus.FeedReady;
                                }
                                catch
                                {
                                    hasMorePages = false;
                                }
                            }
                        }
                        //else
                        //{
                        //    hasMorePages = false;
                        //}

                        try
                        {   //Memory Handling
                            if (images != null)
                            {
                                images.Clear(); images = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            logdata.ExceptionLog(ex);
                        }

                    } while (hasMorePages);

                    //if (!hasMorePages)
                    //{
                    //    if (OnWIAImageScanningDone != null)
                    //    {
                    //        gloWIAScanningDoneEventArgs WIAScanningDone = new gloWIAScanningDoneEventArgs();

                    //        WIAScanningDone.bSuccess = true;
                    //        WIAScanningDone.sScanStatus = "";
                    //        OnWIAImageScanningDone(this, WIAScanningDone);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }

            if (OnWIAImageScanningDone != null)
            {
                gloWIAScanningDoneEventArgs WIAScanningDone = new gloWIAScanningDoneEventArgs();
                WIAScanningDone.bSuccess = true;
                WIAScanningDone.sScanStatus = "";
                OnWIAImageScanningDone(this, WIAScanningDone);
            }

            //gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, false, ".end", bInsideScanning, iCnt, nProcessId);

            //CloseRemoteScanLauncherForWIA();

            bInsideScanning = false;
            iImagCnt = 0;
            //bInsideScanning = false;
            //bFirst = true;
            //bEnteredInEndXfer = false;

        }

        //private ScannerDevice SetWIAScannerCap(ScannerDevice ScannerDevice)
        //{
        //    Resolution = Convert.ToInt32(objScannerSettings.ResolutionName);

        //    ScannerDevice.PictureSettings.HorizontalResolution = Resolution;
        //    ScannerDevice.PictureSettings.VerticalResolution = Resolution;

        //    ScannerDevice.PictureSettings.HorizontalExtent = (int)(objScannerSettings.CardWidth * Resolution);
        //    ScannerDevice.PictureSettings.VerticalExtent = (int)(objScannerSettings.CardLength * this.Resolution);

        //    ScannerDevice.PictureSettings.HorizontalStartPosition = (int)objScannerSettings.CardLeft;
        //    ScannerDevice.PictureSettings.VerticalStartPosition = (int)objScannerSettings.CardTop;

        //    //Brightness
        //    int nBrightness;
        //    int.TryParse(objScannerSettings.BrightnessName, out nBrightness);
        //    ScannerDevice.PictureSettings.Brightness = nBrightness;

        //    //Contrast
        //    int nContrast;
        //    int.TryParse(objScannerSettings.ContrastName, out nContrast);
        //    ScannerDevice.PictureSettings.Contrast = nContrast;

        //    //Scan Side
        //    if (objScannerSettings.ScanSideName == "Front Side")
        //    {
        //        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Feeder;
        //    }
        //    else if (objScannerSettings.ScanSideName == "Flat Bed")
        //    {
        //        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Flatbed;
        //    }
        //    else if (objScannerSettings.ScanSideName == "Duplex")
        //    {
        //        ScannerDevice.DeviceSettings.DocumentHandlingSelect = DocumentHandlingSelect.Duplex;
        //    }

        //    ScannerDevice.DeviceSettings.Pages = (ScannerDevice.DeviceSettings.DocumentHandlingSelect == DocumentHandlingSelect.Duplex) ? 2 : 1;

        //    if (objScannerSettings.ScanModeName == "BW" || objScannerSettings.ScanModeName == "Gray")
        //    {
        //        ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeGrayscale;
        //    }
        //    else if (objScannerSettings.ScanModeName == "RGB")
        //    {
        //        ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeColor;
        //    }
        //    else
        //    {
        //        ScannerDevice.PictureSettings.CurrentIntent = CurrentIntent.ImageTypeText;
        //    }

        //    return ScannerDevice;

        //}

        AxFiScnLib.AxFiScn axFiScn1 = null;
        bool blnOpenScanner = false;
        public bool isNotPaperStream()
        {
            bool bUseTwain = true;
            bool bFailed = false;
            int status;
            int ErrorCode;

            if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName.StartsWith("PaperStream"))
            {
                bUseTwain = false;
                try
                {
                    if (axFiScn1 != null)
                    {
                        axFiScn1.Dispose();
                        axFiScn1 = null;
                    }
                    axFiScn1 = new AxFiScnLib.AxFiScn();
                    axFiScn1.CreateControl();
                    axFiScn1.ScanToRawEx += new AxFiScnLib._DFiScnEvents_ScanToRawExEventHandler(this.axFiScn1_ScanToRawEx);
                }
                catch (Exception ex)
                {
                    logdata.ExceptionLog(ex);
                    if (gloGlobal.gloRemoteScanSettings.twainVersion != "Two")
                    {
                        bUseTwain = true;
                        logdata.Log("Looks like Fujitsu SDK not installed, So using twain for paper stream");
                    }
                    else
                    {
                        bFailed = true;
                    }
                }

                if (bUseTwain == false && bFailed == false)
                {
                    try
                    {
                        status = axFiScn1.SelectSourceName(objScannerSettings.ScannerName);
                        //status = axFiScn2.SelectSource(this.Handle.ToInt32());
                        if (status == -1)
                        {
                            ErrorCode = axFiScn1.ErrorCode;
                            logdata.Log("A scanner was not able to be selected. Error code : " + HexString(ErrorCode));
                            //MessageBox.Show("A scanner was not able to be selected.\n\terror code : " + HexString(ErrorCode), "fiScanTest");
                            bFailed = true;
                        }
                        else if (status == 0)
                        {
                            if (blnOpenScanner == true)
                            {
                                axFiScn1.CloseScanner(this.Handle.ToInt32());
                                blnOpenScanner = false;
                            }

                            // Open Scanner
                            status = axFiScn1.OpenScanner(this.Handle.ToInt32());
                            if (status == -1)  //ModuleScan.RC_FAILURE
                            {
                                ErrorCode = axFiScn1.ErrorCode;
                                logdata.Log("The \"OpenScanner\" function became an error. Error code : " + HexString(ErrorCode));
                                //MessageBox.Show("The \"OpenScanner\" function became an error.\n\terror code : " + HexString(ErrorCode), "fiScanTest");
                                bFailed = true;
                            }
                            else if (status == 2)   //ModuleScan.RC_NOT_DS_FJTWAIN
                            {
                                logdata.Log("It is not FUJITSU TWAIN32 Driver.");
                                //MessageBox.Show("It is not \"FUJITSU TWAIN32 Driver.\"", "fiScanTest");
                                bFailed = true;
                            }
                            else
                            {
                                blnOpenScanner = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                        bFailed = true;
                    }
                    try
                    {
                        if (!bFailed)
                        {
                            ScanModeSet();
                            bInsideScanning = true;
                            status = axFiScn1.StartScan(this.Handle.ToInt32());

                            //failure
                            if (status == -1)
                            {
                                ErrorCode = axFiScn1.ErrorCode;
                                String msg = "The scanning error occurred. Error code : " + HexString(ErrorCode);
                                logdata.Log(msg);
                                //MessageBox.Show("The scanning error occurred.\n\terror code : " + HexString(ErrorCode), "fiScanTest");
                                //MessageBox.Show(axFiScn1.PageCount + "sheets were scanned.");

                                bInsideScanning = false;
                                bFirst = true;
                                CloseRemoteScanLauncher();
                                if (OnImageScanningDone != null)
                                {
                                    gloTwainScanningDoneEventArgs TwainScanningDone = new gloTwainScanningDoneEventArgs();
                                    TwainScanningDone.bSuccess = false;
                                    TwainScanningDone.sScanStatus = msg;
                                    OnImageScanningDone(this, TwainScanningDone);
                                }
                            }
                            else if (status == 1)  //ModuleScan.RC_CANCEL
                            {
                                logdata.Log("The user canceled scanning Or the error which cannot continue scanning was detected.");
                                //MessageBox.Show("The user canceled scanning.\nOr the error which cannot continue scanning was detected.", "fiScanTest");
                                //MessageBox.Show(axFiScn1.PageCount + "sheets were scanned.");
                                
                                bInsideScanning = false;
                                bFirst = true;
                                CloseRemoteScanLauncher();
                                if (OnImageScanningDone != null)
                                {
                                    gloTwainScanningDoneEventArgs TwainScanningDone = new gloTwainScanningDoneEventArgs();
                                    TwainScanningDone.bSuccess = true;
                                    TwainScanningDone.sScanStatus = "";
                                    OnImageScanningDone(this, TwainScanningDone);
                                }
                                
                            }
                            else
                            {
                                bInsideScanning = false;
                                bFirst = true;
                                CloseRemoteScanLauncher();
                                if (OnImageScanningDone != null)
                                {
                                    gloTwainScanningDoneEventArgs TwainScanningDone = new gloTwainScanningDoneEventArgs();
                                    TwainScanningDone.bSuccess = true;
                                    TwainScanningDone.sScanStatus = "";
                                    OnImageScanningDone(this, TwainScanningDone);
                                }
                                //MessageBox.Show(axFiScn1.PageCount + "sheets were scanned.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logdata.ExceptionLog(ex);
                        bFailed = true;
                        bFirst = true;

                        bInsideScanning = false;
                        CloseRemoteScanLauncher();
                        if (OnImageScanningDone != null)
                        {
                            gloTwainScanningDoneEventArgs TwainScanningDone = new gloTwainScanningDoneEventArgs();
                            TwainScanningDone.bSuccess = false;
                            TwainScanningDone.sScanStatus = ex.ToString();
                            OnImageScanningDone(this, TwainScanningDone);
                        }
                    }
                }
            }
            return bUseTwain;
        }

        private void ScanModeSet()
        {
            //axFiScn1.FileName=@"D:\SantoshJ\Work Items\Fun and Learn\VCS 2008\bin\Debug\image#####";
            //Set pixel type
            try
            {
                if (objScannerSettings.ScanModeID == null)
                {
                    switch (objScannerSettings.ScanModeName)
                    {
                        case "RGB":
                            this.axFiScn1.PixelType = 2;
                            break;
                        case "Gray":
                            this.axFiScn1.PixelType = 1;
                            break;
                        case "BW":
                            this.axFiScn1.PixelType = 0;
                            break;
                        default:
                            this.axFiScn1.PixelType = 2;
                            break;
                    }
                }
                else
                {
                    this.axFiScn1.PixelType = Convert.ToInt16(objScannerSettings.ScanModeID);
                }
            }
            catch (Exception ex)
            {
                logdata.Log("Capability PixelType not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            //Set Resolution
            try
            {
                this.axFiScn1.Resolution = 99;  //ModuleScan.RS_CUSTM
                this.axFiScn1.CustomResolution = short.Parse(objScannerSettings.ResolutionName);
            }
            catch (Exception ex)
            {
                logdata.Log("Capability Resolution not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            //Brightness
            try
            {
                if (!String.IsNullOrEmpty(objScannerSettings.BrightnessName))
                {
                    axFiScn1.Brightness = Convert.ToInt16(objScannerSettings.BrightnessName);
                }
            }
            catch (Exception ex)
            {
                logdata.Log("Capability Brightness not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            //Contrast
            try
            {
                if (!String.IsNullOrEmpty(objScannerSettings.ContrastName))
                {
                    axFiScn1.Contrast = Convert.ToInt16(objScannerSettings.ContrastName);
                }
            }
            catch (Exception ex)
            {
                logdata.Log("Capability Contrast not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            //Scan Side
            try
            {
                string[] sScanSideWithFeeder = objScannerSettings.ScanSideName.Split(';');
                string sScanSide = "";
                string sFeeder = "";
                if (sScanSideWithFeeder.Length > 0)
                {
                    sScanSide = sScanSideWithFeeder[0];
                    if (sScanSideWithFeeder.Length > 1)
                    {
                        sFeeder = sScanSideWithFeeder[1];
                    }
                }

                if (sScanSide == "Duplex")
                {
                    axFiScn1.PaperSupply = 2; //ADF(Duplex)
                }
                else
                {
                    if (sFeeder == "flatbed")
                    {
                        axFiScn1.PaperSupply = 0; //Flatbed
                    }
                    else
                    {
                        axFiScn1.PaperSupply = 1; //ADF
                    }
                }
            }
            catch (Exception ex)
            {
                logdata.Log("Capability ScanSide not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            //page size
            try
            {
                if (objScannerSettings.CardLength > 0 && objScannerSettings.CardWidth > 0)
                {
                    this.axFiScn1.PaperSize = 99; // ModuleScan.PSIZE_DATA_CUSTOM; custom size
                    float customPaperWidthValue;
                    try
                    {
                        customPaperWidthValue = (float)objScannerSettings.CardWidth;
                    }
                    catch (FormatException)
                    {
                        customPaperWidthValue = 8.268f;
                    }
                    this.axFiScn1.CustomPaperWidth = customPaperWidthValue;
                    float customPaperLengthValue;
                    try
                    {
                        customPaperLengthValue = (float)objScannerSettings.CardLength;
                    }
                    catch (FormatException)
                    {
                        customPaperLengthValue = 11.693f;
                    }
                    this.axFiScn1.CustomPaperLength = customPaperLengthValue;
                }
            }
            catch (Exception ex)
            {
                logdata.Log("Capability Contrast not available for this scanner.");
                logdata.ExceptionLog(ex);
            }

            try
            {
                this.axFiScn1.ShowSourceUI = objScannerSettings.ShowUI;
                this.axFiScn1.CloseSourceUI = objScannerSettings.ShowUI;
                this.axFiScn1.ScanTo = 2;
                this.axFiScn1.AutomaticColorBackground = 1;
                this.axFiScn1.CropPriority = 1;
                this.axFiScn1.Overwrite = 1;
                this.axFiScn1.CompressionType = 0;
            }
            catch (Exception ex)
            {
                logdata.Log("Capability settings not available for this scanner.");
                logdata.ExceptionLog(ex);
            }
        }

        //----------------------------------------------------------------------------
        //   Function    : Raw image data is received by memory and it saves at a BITMAP file.
        //   Argument    : resolution
        //                 image width
        //                 image length
        //                 pixel number
        //                 compression type
        //                 image size
        //                 memory handle
        //   Return code : Nothing
        //----------------------------------------------------------------------------
        private void axFiScn1_ScanToRawEx(object sender, AxFiScnLib._DFiScnEvents_ScanToRawExEvent e)
        {
            try
            {
                Bitmap BitMap;
                int fileCounter;
                ConvH2BM Conv = new ConvH2BM();

                // Converting the memory handle into the Bitmap class
                BitMap = Conv.GetBitmapFromRAW(e.resolution, e.imageWidth, e.imageLength, e.bitPerPixel, e.compressionType, e.size, e.raw);
                if (BitMap == null)
                {
                    logdata.Log("An error occurred during conversion into the Bitmap class. Error code of the GetBitmapFromRAW:" + HexString(Conv.ErrorCode));
                    //MessageBox.Show("An error occurred during conversion into the Bitmap class. \nError code of the GetBitmapFromRAW:" + HexString(Conv.ErrorCode), "fiScanTest");
                    return;
                }
                Image tempIMG = (Image)BitMap;
                if (tempIMG != null)
                {
                    if (OnForEachImageScanned != null)
                    {
                        gloTwainScanControllerEventArgs TwainEvent = new gloTwainScanControllerEventArgs();
                        TwainEvent.ScannedImg = null;
                        try
                        {
                            TwainEvent.ScannedImg = tempIMG;
                        }
                        catch { }

                        if (TwainEvent.ScannedImg != null)
                        {
                            if ((sScanType == "RCM"))
                            {
                                TwainEvent.sBarcode = (CheckBarcode(tempIMG));
                            }

                            if (objScannerSettings.ScanModeName == "BW")
                            {
                                TwainEvent.sExt = ".png";
                            }
                            else if (objScannerSettings.ScanModeName != "BW")
                            {
                                TwainEvent.sExt = ".jpeg";
                            }

                            OnForEachImageScanned(this, TwainEvent);
                        }
                    }
                    tempIMG.Dispose(); tempIMG = null;
                }

                gloGlobal.gloProgressAndClipboard.lastSeen = DateTime.Now;

                //BitMap.Save(Path, System.Drawing.Imaging.ImageFormat.Bmp);
                BitMap.Dispose();

                // One value of FileCounter is increased.
                fileCounter = this.axFiScn1.FileCounter;
                if (fileCounter == 65535)
                {
                    fileCounter = 1;
                }
                else if (fileCounter != -1)
                {
                    fileCounter++;
                }
                this.axFiScn1.FileCounter = fileCounter;

                //if (ModuleScan.bCancelScan)
                //{
                //    axFiScn1.CancelScan();
                //    ModuleScan.bCancelScan = false;
                //}

                //if (this.MenuItemOutputResult.Checked)
                //{
                //    System.IO.StreamWriter sw = new System.IO.StreamWriter(ModuleScan.strOutputResult, true);
                //    sw.WriteLine("ScanToRawEx Event");
                //    sw.WriteLine("Resolution             : " + e.resolution);
                //    sw.WriteLine("ImageWidth             : " + e.imageWidth);
                //    sw.WriteLine("ImageLength            : " + e.imageLength);
                //    sw.WriteLine("BitPerPixel            : " + e.bitPerPixel);
                //    sw.WriteLine("CompressionType        : " + e.compressionType);
                //    sw.WriteLine("Size                   : " + e.size);
                //    sw.WriteLine("hRaw                   : " + e.raw);
                //    sw.WriteLine("");
                //    sw.WriteLine("PageNumber Property");
                //    sw.WriteLine("value                  : " + axFiScn1.PageNumber);
                //    sw.WriteLine("");
                //    sw.Close();
                //}
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
        }

        public string HexString(int ErrorCode)
        {
            string strWork;
            strWork = ErrorCode.ToString("X");
            if (strWork.Length == 1)
            {
                strWork = "0x0000000" + strWork;
            }
            else if (strWork.Length == 2)
            {
                strWork = "0x000000" + strWork;
            }
            else if (strWork.Length == 3)
            {
                strWork = "0x00000" + strWork;
            }
            else if (strWork.Length == 4)
            {
                strWork = "0x0000" + strWork;
            }
            else if (strWork.Length == 5)
            {
                strWork = "0x000" + strWork;
            }
            else if (strWork.Length == 6)
            {
                strWork = "0x00" + strWork;
            }
            else if (strWork.Length == 7)
            {
                strWork = "0x0" + strWork;
            }
            else
            {
                strWork = "0x" + strWork;
            }
            return strWork;
        }

        public void UpdateBackgroundControls()
        {
            bool bFailed = false;

            if (isNotPaperStream())
            {
                //logdata.Log("XML File : In UpdateBackgroundControls");
                try
                {
                    //string fileNamePath = null;
                    //try
                    //{
                    //    fileNamePath = Path.GetFileName(pToBeScannedFilePath);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logdata.ExceptionLog(ex);
                    //}
                    //if (fileNamePath != null)
                    //{


                    //    sScaaningFilePath = Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScanningFolderName, fileNamePath);
                    //    //ImageBaseName = Path.GetFileNameWithoutExtension(pToBeScannedFilePath);
                    //    //bool bFileReady = false;
                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        if (gloGlobal.clsMISC.WaitForFileToBeReady(pToBeScannedFilePath, 100, 200))
                    //        { break; }// bFileReady = true;
                    //    }

                    //    // 
                    //    //if (!bFileReady) 
                    //    //{ ///Create New file with remark Unabletocopy
                    //    //  ///// Delete source file 
                    //    //      gloRemoteScanMetaDataWriter.CopyAndDeleteFile(ToBeScannedFilePath, "");
                    //    //}
                    //    //logdata.Log("XML File ready");
                    //    gloRemoteScanMetaDataWriter.CopyAndDeleteFile(pToBeScannedFilePath, sScaaningFilePath);
                    //    //     logdata.Log("AFTER CopyAndDeleteFile ");
                    //    //logdata.Log("BEFORE GetScannerCurrentSettings");
                    //    objScannerSettings = gloRemoteScanMetaDataWriter.GetScannerCurrentSettings(sScaaningFilePath);
                    //}
                    //else
                    //{
                    //    objScannerSettings = null;
                    //}
                    //logdata.Log("After GetScannerCurrentSettings");
                    if (objScannerSettings != null)
                    {

                        sScanType = objScannerSettings.ScanType;

                        nProcessId = Convert.ToInt32(objScannerSettings.ScannerSettingsID);

                        objScannerSettings.Status = "";
                        objScannerSettings.Remark = "";
                        try
                        {

                            SaraffTwainDevice = new Saraff.Twain.Twain32(gloGlobal.gloRemoteScanSettings.twainVersion);
                            SaraffTwainDevice.AppProductName = "gloSuite";
                            SaraffTwainDevice.Parent = this;
                            this.TopMost = true;
                            SaraffTwainDevice.ShowUI = objScannerSettings.ShowUI; //true; 
                            SaraffTwainDevice.AcquireCompleted += new EventHandler(SaraffTwainDevice_AcquireCompleted);
                            SaraffTwainDevice.AcquireError += new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireError);
                            SaraffTwainDevice.EndXfer += new EventHandler<Twain32.EndXferEventArgs>(SaraffTwainDevice_EndXfer);
                            SaraffTwainDevice.AcquireClosure += new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireClosure);
                            SaraffTwainDevice.AcquireFailure += new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireFailure);
                            SaraffTwainDevice.AcquireException += new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireException);
                            SaraffTwainDevice.TwainStateChanged += new EventHandler<Twain32.TwainStateEventArgs>(SaraffTwainDevice_TwainStateChanged);
                            //         logdata.Log("objTwain32 Initialized");
                            bool bOpenDSM = false;
                            try
                            {
                                //logdata.Log("Before OpenDSM");
                                bOpenDSM = SaraffTwainDevice.OpenDSM();
                            }
                            catch (Exception ex)
                            {
                                bOpenDSM = false;
                                //logdata.Log(ex.Message);
                                //logdata.ExceptionLog(ex);
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),false); 
                                objScannerSettings.ImageCount = 0;
                                objScannerSettings.Status += "Failed - OpenDSM";
                                objScannerSettings.Remark += ex.ToString();
                                bFailed = true;
                                //  WriteStatusToFile();
                            }

                            if (bOpenDSM)
                            {
                                //logdata.Log("After OpenDSM");

                                if (objScannerSettings.ScannerName == SaraffTwainDevice.GetSourceProductName(Convert.ToInt32(objScannerSettings.ScannerID)))
                                {
                                    SaraffTwainDevice.SourceIndex = Convert.ToInt32(objScannerSettings.ScannerID);
                                }
                                else
                                {
                                    for (int p = 0; p < SaraffTwainDevice.SourcesCount; p++)
                                    {
                                        if (objScannerSettings.ScannerName == SaraffTwainDevice.GetSourceProductName(p))
                                        {
                                            SaraffTwainDevice.SourceIndex = p; //1; 
                                        }
                                    }
                                }
                                //           objTwain32.SourceIndex = Convert.ToInt32(objScannerSettings.ScannerID); //1; 
                                //objTwain32.OpenDSM();
                                bool bOpenDataSource = false;
                                try
                                {
                                    //logdata.Log("Before OpenDataSource");
                                    bOpenDataSource = SaraffTwainDevice.OpenDataSource();
                                    if (bOpenDataSource)
                                    {
                                        try
                                        {
                                            //logdata.Log("After OpenDataSource");
                                            SetScannerCap(SaraffTwainDevice, objScannerSettings);
                                            //logdata.Log("After SetScannerCap()");
                                        }
                                        catch (Exception ex)
                                        {
                                            //logdata.Log("Catch SetScannerCap()");
                                            //logdata.Log(ex.Message);
                                            //logdata.ExceptionLog(ex);
                                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 

                                            objScannerSettings.ImageCount = 0;
                                            objScannerSettings.Status += "Failed - OpenDataSource";
                                            objScannerSettings.Remark += ex.ToString();
                                            //  WriteStatusToFile();
                                            bFailed = true;
                                        }
                                        bool bCloseDataSource = false;
                                        try
                                        {
                                            //logdata.Log("Before CloseDataSource");
                                            bCloseDataSource = true;//objTwain32.CloseDataSource(); 
                                            //logdata.Log("After CloseDataSource");
                                            if (bCloseDataSource)
                                            {
                                                //logdata.Log("Before Acquire");
                                                //iCnt = 0;
                                                bInsideScanning = true;
                                                //StartWaitForScannerToCompleteTimer();
                                                //objTwain32.Acquire(); 
                                                SaraffTwainDevice.Acquire_WithoutClose();
                                                //SaraffTwainDevice.Acquire();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            bCloseDataSource = false;
                                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
                                            objScannerSettings.ImageCount = 0;
                                            objScannerSettings.Status += "Failed - Acquire_WithoutClose";
                                            objScannerSettings.Remark += ex.ToString();
                                            bFailed = true;
                                            //   WriteStatusToFile();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    bOpenDataSource = false;
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
                                    objScannerSettings.ImageCount = 0;
                                    objScannerSettings.Status += "Failed Main bOpenDataSource";
                                    objScannerSettings.Remark += ex.ToString();
                                    bFailed = true;
                                    //   WriteStatusToFile();
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 

                            objScannerSettings.ImageCount = 0;
                            objScannerSettings.Status += "Failed- Main objScannerSettings!=null";
                            objScannerSettings.Remark += ex.ToString();
                            bFailed = true;
                            // WriteStatusToFile();

                        }
                        finally
                        {

                        }
                    }
                    else
                    {
                        bFailed = true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 

                    if (objScannerSettings == null)
                    {
                        bFailed = true;
                    }
                }
                finally
                {
                    pToBeScannedFilePath = null;

                    if (bFailed || (SaraffTwainDevice == null))
                    {
                        //logdata.Log("Before SetImageToClipboard .end");
                        //gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, false, ".end", bInsideScanning, iCnt, nProcessId);
                        //if ()
                        //{
                        //CloseRemoteScanLauncher();
                        //}
                        RemoveTwainEvents();
                        ActionForClosure(SaraffTwainDevice, false);
                        //gloTwainAccessor.InitiateRemoteScanLauncher(gloTwainAccessor.bServiceByOption);

                    }
                }
            }
        }


        void SaraffTwainDevice_TwainStateChanged(object sender, Twain32.TwainStateEventArgs e)
        {
            //logdata.Log("SaraffTwainDevice_TwainStateChanged");
            gloGlobal.gloProgressAndClipboard.lastSeen = DateTime.Now;
        }



        public void CloseRemoteScanLauncher()
        {
            RemoveTwainEvents();

            if (blnOpenScanner)
            {
                try
                {
                    blnOpenScanner = false;
                    axFiScn1.CloseScanner(this.Handle.ToInt32());
                    axFiScn1.Dispose();
                    axFiScn1 = null;
                }
                catch
                {
                }
            }

            if (bFormIsOpened)
            {
                this.Close();
            }
            try
            {
                this.Dispose();
            }
            catch { }
            //frmClipboard.objScanLauncher = null;
            bInsideScanning = false;
        }
        public delegate void UpdateBackgroundControlsDelegateForTimer();


        //private void InvokeTimerElapsedEvent()
        //{

        //    try
        //    {
        //        //logdata.Log("InvokeBackgroundUpdateControlsForQueue");
        //        if (this.InvokeRequired)
        //        {
        //            this.Invoke(new UpdateBackgroundControlsDelegateForTimer(TimerElapsedEvent));
        //        }
        //        else
        //        {
        //            TimerElapsedEvent();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logdata.ExceptionLog(ex);
        //    }
        //}

        public int attemptForTakenClipboard = 0;
        //void WaitForScannerToCompleteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    WaitForScannerToCompleteTimer.Enabled = false;
        //    WaitForScannerToCompleteTimer.Stop();
        //    InvokeTimerElapsedEvent();
        //}

        //private void TimerElapsedEvent()
        //{
        //    if ((SaraffTwainDevice == null) && (!bInsideScanning))
        //    {
        //        try
        //        {
        //            if (WaitForScannerToCompleteTimer != null)
        //            {
        //                WaitForScannerToCompleteTimer.Elapsed -= WaitForScannerToCompleteTimer_Elapsed;
        //                WaitForScannerToCompleteTimer.Dispose();
        //                WaitForScannerToCompleteTimer = null;
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        UpdateBackgroundControls();
        //    }
        //    else
        //    {

        //        attemptForTakenClipboard++;
        //        if (attemptForTakenClipboard > 50)
        //        {
        //            logdata.Log("Closing the form automatically since attempt had exceeded");
        //            CloseRemoteScanLauncher();
        //        }
        //        else
        //        {
        //            //if (((attemptForTakenClipboard % 10) == 0) && (attemptForTakenClipboard > 0))
        //            //{
        //            //    //SLR: not needed since from gloSuite .again will be sent..
        //            //    //logdata.Log("attempting to send again : " + bInsideScanning.ToString() + " " + iCnt.ToString() + " " + nProcessId.ToString());
        //            //    //if (gloGlobal.gloDataClipboardCopy.SetImageToClipboard(null, null, false, ".again", bInsideScanning, iCnt, nProcessId))
        //            //    //{
        //            //    //    CloseRemoteScanLauncher();
        //            //    //}
        //            //}
        //            WaitForScannerToCompleteTimer.Enabled = true;
        //            WaitForScannerToCompleteTimer.Start();
        //        }
        //    }
        //}
        //private bool IsInScanning = false;

        float _fRmtScanerX = 0;
        float _fRmtScanerY = 0;
        float _fRmtScanerHeight = 0;
        float _fRmtScanerWidth = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private DataTable getRemoteScannerNames()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(appSettings["DataBaseConnectionString"].ToString());
            DataTable dt = null;
            try
            {

                if (oDB != null)
                {
                    oDB.Connect(false);
                    string SqlQuery = "select sScannerName FROM  CS_ScannerCoordinates";
                    oDB.Retrive_Query(SqlQuery, out  dt);
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        private void LoadRemoteScannerSetting()
        {
            DataTable _dtRemoteScanner = null;
            try
            {
                bool _issetProperties = false;
                _dtRemoteScanner = getRemoteScannerNames();
                if (_dtRemoteScanner != null && _dtRemoteScanner.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtRemoteScanner.Rows.Count; i++)
                    {
                        using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
                        {
                            if (oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "ISDEFAULT") == "True")
                            {
                                _fRmtScanerX = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "X").ToString()));
                                _fRmtScanerY = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "Y").ToString()));
                                _fRmtScanerHeight = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "HEIGHT").ToString()));
                                _fRmtScanerWidth = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "WIDTH").ToString()));
                                _issetProperties = true;
                                break;
                            }
                        }
                    }
                    if (!_issetProperties && _dtRemoteScanner != null)
                    {
                        DataRow[] _dtr = _dtRemoteScanner.Select(" sScannerName  = 'FUJITSU fi-6130' ");
                        if (_dtr != null && _dtr.Length > 0)
                        {
                            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                            _fRmtScanerX = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "X").ToString()));
                            _fRmtScanerY = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "Y").ToString()));
                            _fRmtScanerHeight = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr.ToString(), "HEIGHT").ToString()));
                            _fRmtScanerWidth = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "WIDTH").ToString()));
                            oSettings.Dispose();
                            oSettings = null;
                        }
                        _dtr = null;
                    }
                }
            }
            catch (Exception Ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                if (_dtRemoteScanner != null) { _dtRemoteScanner.Dispose(); _dtRemoteScanner = null; }
            }
        }

        private void SetScannerCap(Twain32 objTwain32, ScannerCurrentSettingsScannerSettings objScannerSettings)
        {
            try
            {
                try
                {
                    //MODE
                    var _PixelType = objTwain32.Capabilities.PixelType.Get();
                    int _PixelTypeIndex = Convert.ToInt32(objScannerSettings.ScanModeID);
                    if (_PixelType.Count > _PixelTypeIndex && _PixelTypeIndex > -1 && Convert.ToString(_PixelType[_PixelTypeIndex]) == objScannerSettings.ScanModeName)
                    {
                        objTwain32.SetCap(TwCap.IPixelType, _PixelType[_PixelTypeIndex]);
                    }
                    else
                    {
                        for (int i = 0; i < _PixelType.Count; i++)
                        {
                            if (Convert.ToString(_PixelType[i]) == objScannerSettings.ScanModeName)
                            {
                                objTwain32.SetCap(TwCap.IPixelType, _PixelType[i]);
                                break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
                }

                try
                {
                    //DEPTH
                    var _Depth = objTwain32.Capabilities.BitDepth.Get();
                    int _DepthIndex = Convert.ToInt32(objScannerSettings.ScanDepthID);
                    if (_Depth.Count > _DepthIndex && _DepthIndex > -1 && Convert.ToString(_Depth[_DepthIndex]) == objScannerSettings.ScanDepthName)
                    {
                        objTwain32.SetCap(TwCap.BitDepth, _Depth[_DepthIndex]);
                    }
                    else
                    {
                        for (int i = 0; i < _Depth.Count; i++)
                        {
                            if (Convert.ToString(_Depth[i]) == objScannerSettings.ScanDepthName)
                            {
                                objTwain32.SetCap(TwCap.BitDepth, _Depth[i]);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {
                    //RESOLUTION
                    var _Resolution = objTwain32.Capabilities.XResolution.Get();
                    int _ResolutionIndex = Convert.ToInt32(objScannerSettings.ResolutionID);
                    if (_Resolution.Count > _ResolutionIndex && _ResolutionIndex > -1 && Convert.ToString(_Resolution[_ResolutionIndex]) == objScannerSettings.ResolutionName)
                    {

                        objTwain32.SetCap(TwCap.XResolution, _Resolution[_ResolutionIndex]);
                        try
                        {
                            objTwain32.SetCap(TwCap.YResolution, _Resolution[_ResolutionIndex]);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _Resolution.Count; i++)
                        {
                            if (Convert.ToString(_Resolution[i]) == objScannerSettings.ResolutionName)
                            {
                                objTwain32.SetCap(TwCap.XResolution, _Resolution[i]);
                                try
                                {
                                    objTwain32.SetCap(TwCap.YResolution, _Resolution[i]);
                                }
                                catch
                                {
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {
                    if (!String.IsNullOrEmpty(objScannerSettings.BrightnessName))
                    {
                        //BRIGHTNESS
                        var _Brightness = objTwain32.Capabilities.Brightness.Get();
                        //int _BrightnessIndex = Convert.ToInt32(objScannerSettings.BrightnessID);
                        //Int32 BrightnessScale = 0;
                        //Int32 BrightnessDiv = 1;
                        Int32 BrightnessName;
                        Int32 BrightnessMin, BrightnessMax;

                        Int32.TryParse(objScannerSettings.BrightnessName, out BrightnessName);
                        //RemoteScanSettings.getScallingForBrighnessAndCotrast(Convert.ToString(_Brightness[0]), out BrightnessScale, out BrightnessDiv);
                        //BrightnessName = (BrightnessName - BrightnessScale) * BrightnessDiv;

                        BrightnessMin = Convert.ToInt32(_Brightness[0]);
                        BrightnessMax = Convert.ToInt32(_Brightness[_Brightness.Count - 1]);

                        BrightnessName = RemoteScanSettings.GetCalculatedRevCapValue(BrightnessMin, BrightnessMax, BrightnessName);

                        //if (_Brightness.Count > _BrightnessIndex && _BrightnessIndex > -1 && Convert.ToString(_Brightness[_BrightnessIndex]) == Convert.ToString(BrightnessName))
                        //{
                        //    objTwain32.SetCap(TwCap.Brightness, _Brightness[_BrightnessIndex]);
                        //}
                        //else
                        //{
                        for (int i = 0; i < _Brightness.Count; i++)
                        {
                            if (Convert.ToString(_Brightness[i]) == Convert.ToString(BrightnessName))
                            {
                                objTwain32.SetCap(TwCap.Brightness, _Brightness[i]);
                                break;
                            }
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {
                    if (!String.IsNullOrEmpty(objScannerSettings.ContrastName))
                    {
                        //CONTRAST
                        var _Contrast = objTwain32.Capabilities.Contrast.Get();
                        //int _ContrastIndex = Convert.ToInt32(objScannerSettings.ContrastID);
                        //Int32 CotrastScale = 0;
                        //Int32 CotrastDiv = 1;
                        Int32 ContrastName;
                        Int32 ContrastMin, ContrastMax;

                        Int32.TryParse(objScannerSettings.ContrastName, out ContrastName);
                        //RemoteScanSettings.getScallingForBrighnessAndCotrast(Convert.ToString(_Contrast[0]), out CotrastScale, out CotrastDiv);
                        //ContrastName = (ContrastName - CotrastScale) * CotrastDiv;

                        ContrastMin = Convert.ToInt32(_Contrast[0]);
                        ContrastMax = Convert.ToInt32(_Contrast[_Contrast.Count - 1]);

                        ContrastName = RemoteScanSettings.GetCalculatedRevCapValue(ContrastMin, ContrastMax, ContrastName);

                        //if (_Contrast.Count > _ContrastIndex && _ContrastIndex > -1 && Convert.ToString(_Contrast[_ContrastIndex]) == Convert.ToString(ContrastName))
                        //{
                        //    objTwain32.SetCap(TwCap.Contrast, _Contrast[_ContrastIndex]);
                        //}
                        //else
                        //{
                        for (int i = 0; i < _Contrast.Count; i++)
                        {
                            if (Convert.ToString(_Contrast[i]) == Convert.ToString(ContrastName))
                            {
                                objTwain32.SetCap(TwCap.Contrast, _Contrast[i]);
                                break;
                            }
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {

                    //SCANSIDE
                    string sDuplexCap = objTwain32.IsCapSupported(TwCap.DuplexEnabled).ToString();
                    string sFeederCap = objTwain32.IsCapSupported(TwCap.FeederEnabled).ToString();
                    string sDuplexSetCap = objTwain32.IsCapSupported(TwCap.Duplex).ToString();
                    string[] sScanSideWithFeeder = objScannerSettings.ScanSideName.Split(';');
                    string sScanSide = "";
                    string sFeeder = "";
                    if (sScanSideWithFeeder.Length > 0)
                    {
                        sScanSide = sScanSideWithFeeder[0];
                        if (sScanSideWithFeeder.Length > 1)
                        {
                            sFeeder = sScanSideWithFeeder[1];
                        }
                    }
                    if (sScanSide == "Duplex")
                    {
                        //try
                        //{
                        //    if (sFeederCap.ToLower().Contains("set"))
                        //    {
                        //        objTwain32.SetCap(TwCap.FeederEnabled, true);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        //}

                        try
                        {
                            if (sDuplexCap.ToLower().Contains("set"))
                            {
                                objTwain32.SetCap(TwCap.DuplexEnabled, true);
                            }

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                        if (gloGlobal.gloRemoteScanSettings.twainDuplex != "Default")
                        {
                            try
                            {
                                if (sDuplexSetCap.ToLower().Contains("set"))
                                {
                                    if (gloGlobal.gloRemoteScanSettings.twainDuplex == "One Pass")
                                    {
                                        objTwain32.SetCap(TwCap.Duplex, TwDX.OnePassDuplex);
                                    }
                                    else
                                    {
                                        if (gloGlobal.gloRemoteScanSettings.twainDuplex == "Two Pass")
                                        {
                                            objTwain32.SetCap(TwCap.Duplex, TwDX.TwoPassDuplex);
                                        }
                                        else
                                        {
                                            if (gloGlobal.gloRemoteScanSettings.twainDuplex == "None")
                                            {
                                                objTwain32.SetCap(TwCap.Duplex, TwDX.None);
                                            }
                                        }
                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (sDuplexCap.ToLower().Contains("set"))
                            {
                                objTwain32.SetCap(TwCap.DuplexEnabled, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }

                        //if (sFeederCap.ToLower().Contains("set"))
                        //{
                        //    objTwain32.SetCap(TwCap.FeederEnabled, false);
                        //}
                    }

                    if (sFeeder == "feeder")
                    {
                        try
                        {
                            if (sFeederCap.ToLower().Contains("set"))
                            {
                                objTwain32.SetCap(TwCap.FeederEnabled, true);
                            }
                        }
                        catch //(Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                    else if (sFeeder == "flatbed")
                    {
                        try
                        {
                            if (sFeederCap.ToLower().Contains("set"))
                            {
                                objTwain32.SetCap(TwCap.FeederEnabled, false);
                            }
                        }
                        catch //(Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                if ((objScannerSettings.ScannerName != "RemoteScan(TM)") || !bFromCardScan)
                {
                    try
                    {
                        //SUPPORTEDSIZES
                        string sSizeSupported = objTwain32.IsCapSupported(TwCap.SupportedSizes).ToString();
                        if (sSizeSupported.ToLower().Contains("get"))
                        {
                            var _SupportedSizes = objTwain32.Capabilities.SupportedSizes.Get();
                            int _SupportedSizesIndex = Convert.ToInt32(objScannerSettings.SupportedSizeID);
                            if (_SupportedSizes.Count > _SupportedSizesIndex && _SupportedSizesIndex > -1 && RemoteScanSettings.supportedSizes[RemoteScanSettings.getValueFromSupportedSizeFromScanner(Convert.ToString(_SupportedSizes[_SupportedSizesIndex]))] == objScannerSettings.SupportedSizeName)
                            {

                                objTwain32.SetCap(TwCap.SupportedSizes, _SupportedSizes[_SupportedSizesIndex]);
                            }
                            else
                            {
                                for (int i = 0; i < _SupportedSizes.Count; i++)
                                {
                                    if (RemoteScanSettings.supportedSizes[RemoteScanSettings.getValueFromSupportedSizeFromScanner(Convert.ToString(_SupportedSizes[i]))] == objScannerSettings.SupportedSizeName)
                                    {
                                        objTwain32.SetCap(TwCap.SupportedSizes, _SupportedSizes[i]);
                                        break;
                                    }
                                }

                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Capability SupportedSizes not supported", gloAuditTrail.ActivityOutCome.Failure);
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Capability SupportedSizes not supported", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                    //SCAN CARD
                    try
                    {
                        TwUnits _units = objTwain32.Capabilities.Units.GetCurrent();
                        if (Convert.ToString(_units) != Convert.ToString(TwUnits.Inches))
                        {
                            objTwain32.SetCap(TwCap.IUnits, TwUnits.Inches);
                        }
                    }
                    catch
                    {
                    }
                }
                
                try
                {
                    RectangleF TwainRect = objTwain32.ImageLayout;
                    if (objScannerSettings.CardLength > 0)
                    {
                        TwainRect.Height = (float)objScannerSettings.CardLength;
                    }
                    if (objScannerSettings.CardWidth > 0)
                    {
                        TwainRect.Width = (float)objScannerSettings.CardWidth;
                    }

                    TwainRect.X = (float)objScannerSettings.CardLeft;
                    TwainRect.Y = (float)objScannerSettings.CardTop;

                    if (TwainRect.Width == 0)
                    {
                        if (bFromCardScan)
                        {
                            TwainRect.Width = 4F;
                        }
                        else
                        {
                            TwainRect.Width = 8.5F;
                        }
                    }
                    if (TwainRect.Height == 0)
                    {
                        if (bFromCardScan)
                        {
                            TwainRect.Height = 4F;
                        }
                        else
                        {
                            TwainRect.Height = 11.0F;
                        }
                    }

                    objTwain32.ImageLayout = TwainRect;

                    if ((objScannerSettings.ScannerName == "RemoteScan(TM)") && !bFromScanDocCardScan)
                    {
                        if (!bFromCardScan)
                        {
                            objTwain32.ImageLayout = new System.Drawing.RectangleF(0.0F, 0.0F, 8.5F, 11.0F);
                        }
                        else
                        {
                            LoadRemoteScannerSetting();
                            if (_fRmtScanerWidth == 0)
                            {
                                _fRmtScanerWidth = 4F;
                            }
                            if (_fRmtScanerHeight == 0)
                            {
                                _fRmtScanerHeight = 4F;
                            }
                            objTwain32.ImageLayout = new System.Drawing.RectangleF(_fRmtScanerX, _fRmtScanerY, _fRmtScanerWidth, _fRmtScanerHeight);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {
                    objTwain32.Capabilities.ImageFileFormat.Set(TwFF.Bmp);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

                try
                {
                    objTwain32.Capabilities.Compression.Set(TwCompression.None);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SaraffTwainDevice_AcquireCompleted(object sender, EventArgs e)
        {
           // Saraff.Twain.Twain32 objTwain = null;
            try
            {
                //logdata.Log("objTwain32_AcquireCompleted");
                //objTwain = (Saraff.Twain.Twain32)(sender);
                //logdata.Log("Before SetImageToClipboard .end");

                //if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, false, ".end", bInsideScanning, iCnt, nProcessId))
                //{
                CloseRemoteScanLauncher();
                //}
                iImagCnt = 0;
                bInsideScanning = false;
                bFirst = true;
                bEnteredInEndXfer = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                // logdata.Log("Before SetImageToClipboard .err");

                //if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, false, ".err", bInsideScanning, iCnt, nProcessId))
                //{
                CloseRemoteScanLauncher();
                //}
            }
            finally
            {
                //logdata.Log("objTwain32_AcquireCompleted");
               // RemoveTwainEvents();

            }
        }

        private void WriteStatusToFile(string ext = "", Int64 ImgCount = 0)
        {
            try
            {
                string sScannedFilePath = Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScannedFolderName, Path.GetFileNameWithoutExtension(sScaaningFilePath));
                if (string.IsNullOrEmpty(ext))
                {
                    gloRemoteScanMetaDataWriter.CreateXMLFile(objScannerSettings, sScannedFilePath + ".xml");
                    gloRemoteScanMetaDataWriter.CopyAndDeleteFile(sScaaningFilePath, sScannedFilePath + ".xml", true);
                }
                else
                {
                    if (ImgCount > 0) { sScannedFilePath += "_" + Convert.ToString(ImgCount); }

                    System.IO.DirectoryInfo di = new DirectoryInfo(Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloRemoteScanSettings.ScanFolderName, gloGlobal.gloRemoteScanSettings.ScannedFolderName));
                    foreach (FileInfo file in di.GetFiles("*." + ext))
                    {
                        file.Delete();
                    }
                    using (var myFile = File.Create(Path.Combine(sScannedFilePath + "." + ext)))
                    {
                        try
                        {
                            myFile.Close();
                        }
                        catch
                        {
                        }
                         //SLR: it should be closed, if somebody has to delete
                        // interact with myFile here, it will be disposed automatically
                    }

                }
            }
            catch (Exception ex)
            {
                logdata.ExceptionLog(ex);
            }
        }

        private void RemoveTwainEvents()
        {
            if (SaraffTwainDevice != null)
            {

                try
                {
                    SaraffTwainDevice.AcquireClosure -= new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireClosure);

                }
                catch (Exception)
                {

                }
                try
                {

                    SaraffTwainDevice.AcquireFailure -= new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireFailure);

                }
                catch (Exception)
                {

                }
                try
                {
                    SaraffTwainDevice.AcquireException -= new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireException);
                }
                catch (Exception)
                {

                }
                try
                {
                    SaraffTwainDevice.AcquireCompleted -= new System.EventHandler(this.SaraffTwainDevice_AcquireCompleted);
                }
                catch (Exception)
                {

                }
                try
                {
                    SaraffTwainDevice.AcquireError -= new EventHandler<Twain32.AcquireErrorEventArgs>(SaraffTwainDevice_AcquireError);
                }
                catch (Exception)
                {

                }
                try
                {
                    SaraffTwainDevice.EndXfer -= new EventHandler<Twain32.EndXferEventArgs>(SaraffTwainDevice_EndXfer);
                }
                catch (Exception)
                {

                }
                try
                {
                    SaraffTwainDevice.TwainStateChanged -= new EventHandler<Twain32.TwainStateEventArgs>(SaraffTwainDevice_TwainStateChanged);
                }
                catch (Exception)
                {

                }

                try
                {
                    SaraffTwainDevice.CloseDSM();
                }
                catch (Exception)
                {

                }
                this.TopMost = false;
                SaraffTwainDevice.Dispose(); SaraffTwainDevice = null;
            }
        }

        void SaraffTwainDevice_AcquireException(object sender, Twain32.AcquireErrorEventArgs e)
        {
            logdata.Log("objTwain32_AcquireException");
            Twain32 objTw = (Twain32)sender;
            ActionForClosure(objTw, false);
        }

        void SaraffTwainDevice_AcquireFailure(object sender, Twain32.AcquireErrorEventArgs e)
        {
            logdata.Log("objTwain32_AcquireFailure");
            Twain32 objTw = (Twain32)sender;
            ActionForClosure(objTw, false);
        }

        void SaraffTwainDevice_AcquireClosure(object sender, Twain32.AcquireErrorEventArgs e)
        {
            //logdata.Log("objTwain32_AcquireClosure");
            Twain32 objTw = (Twain32)sender;
            ActionForClosure(objTw, objTw.bBeforeAquireCompletion);
        }

        private void ActionForClosure(Twain32 objTw, bool bCompletion)
        {
            //logdata.Log("ActionForClosure");

           
            //bool bClosedByUser = false;
            if (bEnteredInEndXfer)
            {
                //RemoveTwainEvents();
                //logdata.Log("bEnteredInEndXfer-Before SetImageToClipboard .err");

                //if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, true, ".err", bInsideScanning, iCnt, nProcessId))
                //{
                    CloseRemoteScanLauncher();
                //}//bClosedByUser = true; 

            }
            else
            {
                if (!bCompletion)
                {
                    //RemoveTwainEvents();
                    if (objScannerSettings != null)
                    {
                        //if (objScannerSettings.Status != "Success")
                        //{
                        //    objScannerSettings.Status += "Closed by user";

                        //    if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, true, ".err", bInsideScanning, iCnt, nProcessId))
                        //    {
                                CloseRemoteScanLauncher();
                            //}
                            //bClosedByUser = true;
                        //}
                    }

                }
            }
            bInsideScanning = false;
            attemptForTakenClipboard = 0;
            if (OnImageScanningDone != null)
            {
                gloTwainScanningDoneEventArgs TwainScanningDone = new gloTwainScanningDoneEventArgs();
                TwainScanningDone.bSuccess = true;
                TwainScanningDone.sScanStatus = "";
                OnImageScanningDone(this, TwainScanningDone);
            }
            //if (bClosedByUser == true) { gloTwainAccessor.InitiateRemoteScanLauncher(gloTwainAccessor.bServiceByOption); }
        }

        //public string ImageBaseName = "";
        public Int64 iImagCnt = 0;
        public bool bFirst = true;
        string ImageFullPath = string.Empty;
        string ScannedImgPath = string.Empty;
        public bool bEnteredInEndXfer = false;
        //private int iCnt = 0;
        private string CheckBarcode(Image tempIMG)
        {
            ArrayList barcodes = new ArrayList();
            Int32 iScans = 100;
            BarcodeImaging.UseBarcodeZones = false;
            BarcodeImaging.FullScanBarcodeTypes = BarcodeImaging.BarcodeType.Code39;
            int BarCodeCnt = 0;
            string sBarcode = null;
            Bitmap TempBMP = new Bitmap(tempIMG);
            try
            {
                BarcodeImaging.FullScanPage(ref barcodes, TempBMP, iScans);

                BarCodeCnt = barcodes.Count;
                if (BarCodeCnt > 0 && barcodes[0].ToString().ToUpper() == "RCM")
                { sBarcode = barcodes[0].ToString(); }

            }
            catch { }
            finally
            {
                if (TempBMP != null) { TempBMP.Dispose(); TempBMP = null; }
            }
            return sBarcode;
        }

        void SaraffTwainDevice_EndXfer(object sender, Twain32.EndXferEventArgs e)
        {
            //throw new NotImplementedException();
            //logdata.Log("objTwain32_EndXfer");
            Saraff.Twain.Twain32 objTwain = null;
            objTwain = (Saraff.Twain.Twain32)(sender);
            bEnteredInEndXfer = true;
            //System.Drawing.Imaging.ImageFormat ImgFormat = null;

           // gloGlobal.gloProgressAndClipboard.lastSeen = DateTime.Now;

            if (OnForEachImageScanned != null)
            {
                gloTwainScanControllerEventArgs TwainEvent = new gloTwainScanControllerEventArgs();
                TwainEvent.ScannedImg = null;

                try
                {
                    TwainEvent.ScannedImg = e.Image;
                }
                catch{ }

                if (TwainEvent.ScannedImg != null)
                {
                    if ((sScanType == "RCM"))
                    {
                        TwainEvent.sBarcode = (CheckBarcode(e.Image));
                    }

                    if (objScannerSettings.ScanModeName == "BW")
                    {
                        TwainEvent.sExt = ".png";
                    }
                    else if (objScannerSettings.ScanModeName != "BW")
                    {
                        TwainEvent.sExt = ".jpeg";
                    }

                    OnForEachImageScanned(this, TwainEvent);
                }
            }

            //tempIMG = e.Image;
            //iCnt++;

            //if ((sScanType == "RCM") )
            //{
            //    sExt = ".bar";
            //}
            //else
            //{
            //    //if (objScannerSettings.ScanModeName == "BW")
            //    //{
            //    //    sExt = ".png";
            //    //    ImgFormat = System.Drawing.Imaging.ImageFormat.Png;
            //    //}
            //    //else 
            //    {
            //        if (objScannerSettings.ScanModeName != "BW")
            //        {
            //            sExt = ".jpeg";
            //            //ImgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            //        }
            //    }
            //}
            //try
            //{
            //    //logdata.Log("bFirst : " + bFirst);
            //    if (bFirst)
            //    {
            //        bFirst = false;

            //        if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(tempIMG, sExt, false, "", bInsideScanning, iCnt, nProcessId, iCnt + 1))
            //        {
            //            CloseRemoteScanLauncher();
            //        }
            //    }
            //    else
            //    {

            //        if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(tempIMG, sExt, true, "", bInsideScanning, iCnt, nProcessId, iCnt + 1))
            //        {
            //            CloseRemoteScanLauncher();
            //        }
            //    }

            //    //logdata.Log("sExt : " + sExt);


            //    //logdata.Log("After SetImageToClipboard   ");
            //}
            //catch (Exception ex)
            //{
            //    //logdata.Log("Catch SetImageToClipboard  : " + ex.ToString());
            //    //logdata.Log("Before SetImageToClipboard .err");
            //    logdata.ExceptionLog(ex);

            //    //if (gloGlobal.gloProgressAndClipboard.SetImageToClipboard(null, null, false, ".err", bInsideScanning, iCnt, nProcessId))
            //    //{
            //        CloseRemoteScanLauncher();
            //    //}
            //}
            attemptForTakenClipboard = 0;
        }

        void SaraffTwainDevice_AcquireError(object sender, Twain32.AcquireErrorEventArgs e)
        {
            // throw new NotImplementedException();
            // logdata.Log("objTwain32_AcquireError");
            Twain32 objTw = (Twain32)sender;
            ActionForClosure(objTw, false);
        }

        private void frmLocalTwainScanLauncher_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (WaitForScannerToCompleteTimer != null)
            //{
            //    try
            //    {
            //        WaitForScannerToCompleteTimer.Elapsed -= WaitForScannerToCompleteTimer_Elapsed;
            //        WaitForScannerToCompleteTimer.Dispose();
            //        WaitForScannerToCompleteTimer = null;
            //    }
            //    catch
            //    {
            //    }
            //    bInsideScanning = false;
            //}

            bFormIsOpened = false;
        }

        private void frmLocalTwainScanLauncher_Shown(object sender, EventArgs e)
        {
            bFormIsOpened = true;
        }


    }

    public class gloTwainScanControllerEventArgs : EventArgs
    {
        public Image ScannedImg;
        public string sBarcode;
        public string sExt;
    }
    public class gloTwainScanningDoneEventArgs : EventArgs
    {
        public bool bSuccess;
        public string sScanStatus;
    }

    public class gloWIAScanControllerEventArgs : EventArgs
    {
        public Image ScannedImg;
        public string sBarcode;
        public string sExt;
    }
    public class gloWIAScanningDoneEventArgs : EventArgs
    {
        public bool bSuccess;
        public string sScanStatus;
    }
}
