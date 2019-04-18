using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using gloRemoteScanGeneral;

namespace gloCardScanning
{
    class gloDrivingLicenseCardScanWithOCR
    {
        #region   "Declararions"

        private NetScanW.SLibEx _sLib;
        private NetScanWex.SLibEx _sLibEx;

        private NetScanWex.CActivateSw _activation;

        private NetScanW.CImage _image;
        private NetScanWex.CImage _imageEx;

        private NetScanW.IdData _idData;
        private NetScanWex.IdData _idDataEx;

        private NetScanW.CBarCode _barCode;

        private String _SelectedScanner = String.Empty;

        private String _CardFrontImagePath = string.Empty;

        private String _CardBackImagePath = string.Empty;

        private String _CardFaceImagePath = string.Empty; 
      
        private bool _bIsScanDrivingLicenceWithOCR=false;
        private bool _bIsScanDrivingLicenceImage = false; 
        short _Resolution = 300;

        float _ColorScheme = 2;

        bool _CenterImage = false;

        bool _DuplexScan = false;

        string imageSource = string.Empty;
        int stateID = 0;
        gloDrivingLicenceData ogloDrivingLicenceData = null;

        string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        String _TempImageDirPath = gloSettings.FolderSettings.AppTempFolderPath + "ScanImages\\";

        public String TempImageDirPath
        {
            get { return _TempImageDirPath; }
        }
        

        public gloDrivingLicenceData gloDrivingLicenceData
        {
            get { return ogloDrivingLicenceData; } 
        }

       
        public int StateID
        {
            get
            {
                return this.stateID;
            }
        }
        public float ColorScheme
        {
            get
            {
                return this._ColorScheme;
            }
        }
        public String SelectedScanner
        {
            set 
                {
                    this._SelectedScanner = value;
                }
            get
                {
                    return this._SelectedScanner;
                }
        }
        public String ImageSource
        {
            
            get
            {
                return this.imageSource;
            }
        }
        public String CardFrontImagePath 
        {
            get {
                return _CardFrontImagePath;
            }
            set {
                this._CardFrontImagePath = value;
            }
        }

        public String CardBackImagePath
        {
            get
            {
                return _CardBackImagePath;
            }
            set
            {
                this._CardBackImagePath = value;
            }
        }


        public String CardFaceImagePath
        {
            get
            {
                return _CardFaceImagePath;
            }
            set
            {
                this._CardFaceImagePath = value;
            }
        }

        public bool bIsScanDrivingLicenceWithOCR
        { get { return _bIsScanDrivingLicenceWithOCR; } }
        public bool bIsScanDrivingLicenceImage
        { get { return _bIsScanDrivingLicenceImage; } }
        #endregion

        #region "Constructor"


        public gloDrivingLicenseCardScanWithOCR()
        {

            ogloDrivingLicenceData = new global::gloCardScanning.gloDrivingLicenceData();
            

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion
       
        #region  "Scan Driving Licence With OCR"

        private bool InitiateScanner()
        {
            // Scanner objects
            _sLib = new NetScanW.SLibEx();
            _sLibEx = new NetScanWex.SLibEx();

            // Image Object
            _image = new NetScanW.CImage();
            _imageEx = new NetScanWex.CImage();

            // Data objects
            _idData = new NetScanW.IdData();

            // Activation
            _activation = new NetScanWex.CActivateSw();

            _idDataEx = new NetScanWex.IdData();

            //Bar Code Class
            _barCode = new NetScanW.CBarCode();

            LoadSettings();

            _sLibEx.UseFixedModel = gloCSlibconst.CSSN_TWN;
            _sLibEx.SetTwainScanner(SelectedScanner);


            // Init Scanner
            int scannerResult = _sLib.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
            int status = _sLib.IsScannerValid;
            if (status == gloCSlibconst.SLIB_ERR_INVALID_SCANNER || _sLib.LastErrorStatus == gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND)
            {
                MessageBox.Show("The scanner is not found. Verify that the scanner is connected", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_sLib.PaperInTray <= 0 && SelectedScanner!="RemoteScan(TM)")
            {
                MessageBox.Show("Insert the card to be scanned", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToBoolean(_sLib.IsNeedCalibration))
            {
                MessageBox.Show("Calibrate the scanner", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (scannerResult < 0 && scannerResult != -13)
            {
                _bIsScanDrivingLicenceWithOCR = false;
                return false ;
            }
            else
            {
                // Init Id data library
               int result = _idData.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                //Initializing Bar Code library
                result = _barCode.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                return true;
            }
        }


        private void UnLoadScaner()
        {

            if (_sLibEx != null)
                _sLibEx.UnInit();

            _sLib = null;
            _sLibEx = null;
            _idData = null;            
            _image = null;
            _imageEx = null;
            _activation = null;
            _idDataEx = null;
            _barCode = null;
        }

        private bool ScanImageFromScanner()
        {
            if (_sLib.IsScannerValid == 1)
            {
                // Calibration
                if (_sLib.IsNeedCalibration == 1)
                {
                    _sLibEx.CalibrateScannerEx();
                }

                //For duplex scanning
                if (_DuplexScan)
                    _sLibEx.Duplex = 1;
                if (_CenterImage) 
                    _sLibEx.SetCenteredImage = 1;

                _sLib.ScanHeight = 360;
                _sLib.ScanWidth = 220;
                _sLib.Resolution = _Resolution;


                _CardFrontImagePath = gloScannerGeneral.RetrieveFileName(_TempImageDirPath) + ".jpg";
                _CardBackImagePath = _CardFrontImagePath;
                _CardFaceImagePath = _CardFrontImagePath;
                _CardBackImagePath = _CardBackImagePath.Insert(_CardBackImagePath.Length - 4, "-Back");
                _CardFaceImagePath = _CardFaceImagePath.Insert(_CardFaceImagePath.Length - 4, "-Face");  

               int result = _sLib.ScanToFileEx(_CardFrontImagePath);

               int retVal = 1;

               #region " .Check if any Scan error present "

               int lastError = _sLib.LastErrorStatus;
               if (lastError != gloCSlibconst.SLIB_TRUE)
               {
                   switch (lastError)
                   {
                       case ClsgloScanConstants.LICENSE_EXPIRED:
                           MessageBox.Show("ERROR: Licence expired!", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case ClsgloScanConstants.LICENSE_INVALID:
                           MessageBox.Show("ERROR: Licence does not match this type of library", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_SCANNER_GENERAL_FAIL:
                           MessageBox.Show("ERROR: General failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_CANCELED_BY_USER:
                           MessageBox.Show("ERROR: Scan canceled by the user", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND:
                           MessageBox.Show("ERROR: Scanner not found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_HARDWARE_ERROR:
                           MessageBox.Show("ERROR: Hardware failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_PAPER_FED_ERROR:
                           MessageBox.Show("ERROR: Paper feed problem", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_SCANABORT:
                           MessageBox.Show("ERROR: Scanner aborted", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_NO_PAPER:
                           MessageBox.Show("ERROR: No paper found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_PAPER_JAM:
                           MessageBox.Show("ERROR: Paper Jammed", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_FILE_IO_ERROR:
                           MessageBox.Show("ERROR: Hardware IO failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_PRINTER_PORT_USED:
                           MessageBox.Show("ERROR: Printer port already used by other utility (for parallel models only)", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_OUT_OF_MEMORY:
                           MessageBox.Show("ERROR: Out of memory", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                       case gloCSlibconst.SLIB_ERR_INVALID_SCANNER:
                           MessageBox.Show("ERROR: Scanner type is not supported", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           retVal = 0;
                           break;
                   }
               }

               #endregion " .Check if any Scan error present "

               if (retVal == 0)
                   return false;
                if (result < 0 && result != -13)
                {
                    _bIsScanDrivingLicenceImage = false;
                    return false ;
                }

                if (result >= 0)
                {
                    _bIsScanDrivingLicenceImage = true;
                    return true;
                    //picScannedImage.Image = _GetImage(_CardFrontImagePath);
                    //pictureBox1.Image = _GetImage(_CardBackImagePath);
                }
                else if (result < 0)
                {
                    _bIsScanDrivingLicenceImage = false;
                    return false ;
                }
            }

            else
            {
                MessageBox.Show("Scanner not valid. Please check the connection to the scanner." ,_MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return false;
        }

        public void ScanDrivingLicenceWithOCR()
        {
            int result;

            if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
            {
                try
                {
                    if (!gloGlobal.gloTSPrint.isLocalMachineUpdated())
                    {
                        return;
                    }
                    if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                    {
                        return;
                    }
                    LoadSettings();

                    CardScanSettingsScanCardSettings cardScanSettings = new CardScanSettingsScanCardSettings();
                    cardScanSettings.InsuranceCardDPI = _Resolution;
                    cardScanSettings.DrivingLicenseCardDPI = _Resolution;
                    cardScanSettings.ScannerColor = _ColorScheme.ToString();
                    cardScanSettings.ScanDuplex = _DuplexScan;
                    cardScanSettings.EnableOCR = true;
                    cardScanSettings.CenterImage = _CenterImage;
                    cardScanSettings.SelectedScanner = _SelectedScanner;
                    cardScanSettings.CardType = "DrivingLicence";
                    cardScanSettings.Status = (int)CardScanStatus.SentToScan; 
                    cardScanSettings.Description = "";

                    if (gloScannerGeneral.PerformRemoteScan(ref cardScanSettings))
                    {
                        ogloDrivingLicenceData.NameFirst = cardScanSettings.FirstName;
                        ogloDrivingLicenceData.NameMiddle = cardScanSettings.MiddleName;
                        ogloDrivingLicenceData.NameLast = cardScanSettings.LastName;
                        ogloDrivingLicenceData.DateOfBirth = cardScanSettings.DateOfBirth;
                        ogloDrivingLicenceData.Sex = cardScanSettings.Sex;
                        ogloDrivingLicenceData.City = cardScanSettings.City;
                        ogloDrivingLicenceData.Zip = cardScanSettings.Zip;
                        ogloDrivingLicenceData.Address = cardScanSettings.Address;
                        ogloDrivingLicenceData.State = cardScanSettings.State;
                        String imgPath = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanCardWithOCRFolder, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName);
                        String cardFront, cardBack, CardFace;
                        cardFront = Path.Combine(gloGlobal.gloTSPrint.TempPath, cardScanSettings.CardImageFront);
                        cardBack = Path.Combine(gloGlobal.gloTSPrint.TempPath, cardScanSettings.CardImageBack);
                        CardFace = Path.Combine(gloGlobal.gloTSPrint.TempPath, cardScanSettings.CardFaceImage);
                        _bIsScanDrivingLicenceImage = false;
                        if (gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(Path.Combine(imgPath, cardScanSettings.CardImageFront), cardFront))
                        {
                            if (gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(Path.Combine(imgPath, cardScanSettings.CardImageBack), cardBack))
                            {
                                if (gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(Path.Combine(imgPath, cardScanSettings.CardFaceImage), CardFace))
                                {
                                    _CardFrontImagePath = cardFront;
                                    _CardBackImagePath = cardBack;
                                    _CardFaceImagePath = CardFace;
                                    _bIsScanDrivingLicenceImage = true;
                                }
                            }
                        }
                        _bIsScanDrivingLicenceWithOCR = true;
                    }
                    else
                    {
                        _bIsScanDrivingLicenceWithOCR = false;
                    }
                }
                catch (Exception ex)
                {
                    _bIsScanDrivingLicenceWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
            }
            else
            {
                try
                {
                    if (InitiateScanner())
                    {
                        #region  "Scan Image From Scanner"
                        if (ScanImageFromScanner())
                        {



                            #region  " OCR"

                            _idDataEx.RegionSetDetectionSequence(0, -1, -1, -1, -1, -1, -1);

                            // Auto detect crops and rotates the internal image for better OCR results
                            stateID = _idData.AutoDetectState(imageSource);
                            if (stateID < 0)
                            {
                                // Alert(CSSNCore.GetIdDataClassError(stateID));
                                _bIsScanDrivingLicenceWithOCR = false;
                                return;
                            }


                            int angleA = 0;
                            int angleB = 0;
                            int imageA_assignment = 0;

                            result = _idDataEx.DetectProcessDuplexEX("", "", stateID, ref imageA_assignment, ref angleA, ref angleB, 0);
                            if (result < 0)
                            {
                                _bIsScanDrivingLicenceWithOCR = false;
                                return;
                            }

                            if (angleA > 0)
                            {
                                _AlignIDImage(angleA);
                            }

                            if (result >= 0)
                            {
                                _idData.RefreshData();

                                #region "Required Scanned Data"

                                ogloDrivingLicenceData.NameFirst = _idData.NameFirst;
                                ogloDrivingLicenceData.NameMiddle = _idData.NameMiddle;
                                ogloDrivingLicenceData.NameLast = _idData.NameLast;
                                ogloDrivingLicenceData.DateOfBirth = _idData.DateOfBirth;
                                ogloDrivingLicenceData.Sex = _idData.Sex;
                                ogloDrivingLicenceData.City = _idData.City;
                                ogloDrivingLicenceData.Zip = _idData.Zip;
                                ogloDrivingLicenceData.Address = _idData.Address;
                                ogloDrivingLicenceData.State = _idData.State;
                                #endregion "Required Scanned Data"

                                // Get face image
                                result = _idData.GetFaceImage(_CardFrontImagePath, _CardFaceImagePath, stateID);
                                _bIsScanDrivingLicenceWithOCR = true;
                            }
                            else
                            {
                                _bIsScanDrivingLicenceWithOCR = false;
                                return;
                            }
                            //_DumpImage();
                            #endregion

                        }
                        #endregion
                    }

                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    _bIsScanDrivingLicenceWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
                catch (IOException ex)
                {
                    _bIsScanDrivingLicenceWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
                catch (Exception ex)
                {
                    _bIsScanDrivingLicenceWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
                finally
                {
                    UnLoadScaner();

                }
            }
        }

        
        #endregion

        #region " LoadSettings "
        private void LoadSettings()
        {
            using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
            {
                try
                {

                    #region " Get Resolution settings for Insurance "
                  
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance"))
                    {
                        case "300":
                            _Resolution = 300;
                            break;
                        case "600":
                            _Resolution = 600;
                            break;
                        default:
                            _Resolution = 600;
                            break;
                    }

                    

                    #endregion

                    #region " Get ColorScheme Settings for Scan "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme"))
                    {
                        case "4"://true color
                            _ColorScheme = 2;
                            break;
                        case "1"://gray 
                            _ColorScheme = 1;
                            break;
                        case "2"://black & white
                            _ColorScheme = 0;
                            break;
                        default:
                            _ColorScheme = 2;
                            break;
                    }
                   

                    #endregion



                    #region " Get Scan Mode Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex") != "")
                    {
                        string _scanduplex = oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex");
                        if (_scanduplex == "1")
                        {
                            _DuplexScan = true;
                        }
                        else
                        {
                            _DuplexScan = false;

                        }
                    }
                    else
                    {
                        _DuplexScan = false;

                    }

                    #endregion

                    //Added on 05052010
                    #region " Get Selected Scanner Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") != "")
                    {
                        if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") == "None")
                        {
                            _SelectedScanner = "None";
                        }
                        else
                        {
                            string _sScannerName = oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner");
                            if (_sScannerName.ToString().Trim().Length > 0)
                            {
                                _SelectedScanner = _sScannerName;
                            }
                            else
                            {
                                _SelectedScanner = "None";
                            }
                        }
                    }
                    else
                    {
                        _SelectedScanner = "None";
                    }

                    #endregion

                    #region " Get CenterImage Settings for Scan "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "CenterImage").ToString().ToUpper())
                    {
                        case "1"://true color
                            _CenterImage = true;
                            break;
                        case "0"://true color
                            _CenterImage = false;
                            break;
                        default:
                            _CenterImage = true;
                            break;
                    }


                    #endregion

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oSettings != null) { oSettings.Dispose(); }
                }
            }//Using //04-03-2011 : Commperss
        }
        #endregion

        #region   "Private Method"
        private int GetRegionIntValue(string region)
        {
            switch (region)
            {
                case "United States":
                    return 0;
                case "Australia":
                    return 4;
                case "Asia":
                    return 5;
                case "Canada":
                    return 1;
                case "America":
                    return 2;
                case "Europe":
                    return 3;
                case "Africa":
                    return 7;
                default:
                    return -1;
            }
        }
        private void _DumpImage()
        {
            int result = _image.ReformatImage("", 0, 0, _CardFrontImagePath);

            //// Check result
            if (result >= 0)
               _image.ReformatImage("", 0, 0, _CardBackImagePath);
            
        }
        private void _AlignIDImage(int angle)
        {

            int result = _image.RotateImage(_CardFrontImagePath + "scannedImage.jpg", angle, 0, _CardFrontImagePath + "scannedImage.jpg");
               

        }
        #endregion

    }

    class gloInsuranceCardScanWithOCR
    {
        #region "Constructor"

        public gloInsuranceCardScanWithOCR()
        { 
           #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
            ogloInsuranceCardData = new gloInsuranceCardData();
        
        }

        #endregion


        #region   "Declararions"
        private NetScanWex.SLibEx _sLibEx;
        private NetScanW.SLibEx _sLib;

        /*Declaring Image Objects*/
        private NetScanW.CImage _image;
        private NetScanWex.CImage _imageEx;

        private NetScanWex.CActivateSw _activation;

        private NetMedicSdkCOM.Med _med;

        private string _tempImageName = string.Empty;

        private String _SelectedScanner = String.Empty;

        private String _CardFrontImagePath = string.Empty;

        private String _CardBackImagePath = string.Empty;

        private String _CardFaceImagePath = string.Empty;

        short _Resolution = 300;

        float _ColorScheme = 2;

        bool _CenterImage = false;

        bool _DuplexScan = false;
        gloInsuranceCardData ogloInsuranceCardData = null;

        String _TempImageDirPath = gloSettings.FolderSettings.AppTempFolderPath + "ScanImages\\";

        public String TempImageDirPath
        {
            get { return _TempImageDirPath; }
        }

        public gloInsuranceCardData gloInsuranceCardData
        {
            get 
            {
                return ogloInsuranceCardData; 
            } 
        }

        private bool _bIsInsuranceCardScanWithOCR = false;
        private bool _bIsInsuranceCardScanImage = false;

        public bool bIsInsuranceCardScanWithOCR
        {
            get
            { 
                return _bIsInsuranceCardScanWithOCR; 
            } 
        }
        public bool bIsInsuranceCardScanImage
        {
            get
            {
                return _bIsInsuranceCardScanImage;
            }
        }
        public float ColorScheme
        {
            get
            {
                return this._ColorScheme;
            }
        }

        public String SelectedScanner
        {
            set
            {
                this._SelectedScanner = value;
            }
            get
            {
                return this._SelectedScanner;
            }
        }

        public String CardFrontImagePath
        {
            get
            {
                return _CardFrontImagePath;
            }
            set
            {
                this._CardFrontImagePath = value;
            }
        }

        public String CardBackImagePath
        {
            get
            {
                return _CardBackImagePath;
            }
            set
            {
                this._CardBackImagePath = value;
            }
        }


        public String CardFaceImagePath
        {
            get
            {
                return _CardFaceImagePath;
            }
            set
            {
                this._CardFaceImagePath = value;
            }
        }

        string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion


        #region  "Scan InsuranceCard With OCR"

        public void ScanInsuranceCardWithOCR()
        {
            if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
            {
                try
                {
                    if (!gloGlobal.gloTSPrint.isLocalMachineUpdated())
                    {
                        return;
                    }
                    if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                    {
                        return;
                    }
                    LoadSettings();

                    CardScanSettingsScanCardSettings cardScanSettings = new CardScanSettingsScanCardSettings();
                    cardScanSettings.InsuranceCardDPI = _Resolution;
                    cardScanSettings.DrivingLicenseCardDPI = _Resolution;
                    cardScanSettings.ScannerColor = _ColorScheme.ToString();
                    cardScanSettings.ScanDuplex = _DuplexScan;
                    cardScanSettings.EnableOCR = true;
                    cardScanSettings.CenterImage = _CenterImage;
                    cardScanSettings.SelectedScanner = _SelectedScanner;
                    cardScanSettings.CardType = "Insurance";
                    cardScanSettings.Status = (int)CardScanStatus.SentToScan;
                    cardScanSettings.Description = "";

                    if (gloScannerGeneral.PerformRemoteScan(ref cardScanSettings))
                    {
                        ogloInsuranceCardData.InsMemberID = cardScanSettings.InsMemberID;
                        ogloInsuranceCardData.InsMemberName = cardScanSettings.InsMemberName;
                        ogloInsuranceCardData.InsGroupNo = cardScanSettings.InsGroupNo;
                        ogloInsuranceCardData.PlanProvider = cardScanSettings.PlanProvider;
                        ogloInsuranceCardData.GroupNumber = cardScanSettings.GroupNumber;
                        String imgPath = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanCardWithOCRFolder, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName);
                        String cardFront, cardBack;
                        cardFront = Path.Combine(gloGlobal.gloTSPrint.TempPath, cardScanSettings.CardImageFront);
                        cardBack = Path.Combine(gloGlobal.gloTSPrint.TempPath, cardScanSettings.CardImageBack);
                        _bIsInsuranceCardScanImage = false;
                        if (gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(Path.Combine(imgPath, cardScanSettings.CardImageFront), cardFront))
                        {
                            if (gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(Path.Combine(imgPath, cardScanSettings.CardImageBack), cardBack))
                            {
                                _CardFrontImagePath = cardFront;
                                _CardBackImagePath = cardBack;
                                _CardFaceImagePath = _CardFrontImagePath;
                                _bIsInsuranceCardScanImage = true;
                            }
                        }
                        _bIsInsuranceCardScanWithOCR = true;
                    }
                    else 
                    {
                        _bIsInsuranceCardScanWithOCR = false;
                    }
                }
                catch (Exception ex)
                {
                    _bIsInsuranceCardScanWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
            }
            else
            {

                try
                {

                    if (InitiateScanner())
                    {

                        if (ScanImageFromScanner())
                        {
                            #region  " OCR"
                            int _result = _med.ProcessMedical("", "", 0);
                            if (_result >= 0)
                            {

                                #region "ReQuired Scanned Insurance Data"

                                ogloInsuranceCardData.InsMemberID = _med.propMemberID;
                                ogloInsuranceCardData.InsMemberName = _med.propMemberName;
                                ogloInsuranceCardData.InsGroupNo = _med.propGroupNumber;
                                ogloInsuranceCardData.PlanProvider = _med.propPlanProvider;
                                ogloInsuranceCardData.GroupNumber = _med.propGroupNumber;

                                #endregion

                                if (_result > 0)
                                {
                                    RotateScannedImage(_result);
                                }

                                _bIsInsuranceCardScanWithOCR = true;
                            }

                            #endregion

                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    _bIsInsuranceCardScanWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                catch (IOException ex)
                {
                    _bIsInsuranceCardScanWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                catch (Exception ex)
                {
                    _bIsInsuranceCardScanWithOCR = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    UnLoadScaner();
                }
            }
           
        }

        private bool InitiateScanner()
        {

            // Scanner objects
            _sLib = new NetScanW.SLibEx();
            _sLibEx = new NetScanWex.SLibEx();

            // Medical objects
            _med = new NetMedicSdkCOM.Med();

            // Activation
            _activation = new NetScanWex.CActivateSw();


            // Image Object
            _image = new NetScanW.CImage();
            _imageEx = new NetScanWex.CImage();

            #region  "Initialize Scanner"
            LoadSettings();
            _sLibEx.UseFixedModel = gloCSlibconst.CSSN_TWN;
            _sLibEx.SetTwainScanner(SelectedScanner);
            // Init Scanner
            int scannerResult = _sLib.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
            int status = _sLib.IsScannerValid;
            if (status == gloCSlibconst.SLIB_ERR_INVALID_SCANNER || _sLib.LastErrorStatus == gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND)
            {
                MessageBox.Show("The scanner is not found. Verify that the scanner is connected", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_sLib.PaperInTray <= 0 && SelectedScanner != "RemoteScan(TM)")
            {
                MessageBox.Show("Insert the card to be scanned", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToBoolean(_sLib.IsNeedCalibration))
            {
                MessageBox.Show("Calibrate the scanner", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (scannerResult < 0 && scannerResult != -13)
            {
                _bIsInsuranceCardScanWithOCR = false;
                return false;
            }
            else
            {
                //  Initializing Image Library
                int result = _image.InitLibrary(ClsgloScanConstants.LICENSE_VALUE);
                // Init Medical data library
                result = _med.InitSdk(ClsgloScanConstants.LICENSE_VALUE);
                return true;
            }
            #endregion
        }

        private void UnLoadScaner()
        {

            if (_sLibEx != null)
            {
                _sLibEx.UnInit();
            }
            if (_med != null)
            {
                _med.UnInitSdk();
            }
            _sLibEx = null;
            _sLib = null;
            _med = null;
            _activation = null;
        }

        private bool ScanImageFromScanner()
        {
            #region  "Scan Image From Scanner"
            if (_sLib.IsScannerValid == 1)
            {
                // Calibration
                if (_sLib.IsNeedCalibration == 1)
                {
                    _sLibEx.CalibrateScannerEx();
                }

                //For duplex scanning
                if (_DuplexScan)
                    _sLibEx.Duplex = 1;
                if(_CenterImage)
                _sLibEx.SetCenteredImage = 1;

                _sLib.ScanHeight = 360;
                _sLib.ScanWidth = 220;
                _sLib.Resolution = _Resolution;



                _CardFrontImagePath = gloScannerGeneral.RetrieveFileName(_TempImageDirPath) + ".jpg";
                _CardBackImagePath = _CardFrontImagePath;
                _CardFaceImagePath = _CardFrontImagePath;
                _CardBackImagePath = _CardBackImagePath.Insert(_CardBackImagePath.Length - 4, "-Back");
                


                int result = _sLib.ScanToFileEx(_CardFrontImagePath);
                int retVal = 1;

                #region " .Check if any Scan error present "

                int lastError = _sLib.LastErrorStatus;
                                    if (lastError != gloCSlibconst.SLIB_TRUE)
                                    {
                                        switch (lastError)
                                        {
                                            case ClsgloScanConstants.LICENSE_EXPIRED:
                                                MessageBox.Show("ERROR: Licence expired!", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case ClsgloScanConstants.LICENSE_INVALID:
                                                MessageBox.Show("ERROR: Licence does not match this type of library", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANNER_GENERAL_FAIL:
                                                MessageBox.Show("ERROR: General failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_CANCELED_BY_USER:
                                                MessageBox.Show("ERROR: Scan canceled by the user", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANNER_NOT_FOUND:
                                                MessageBox.Show("ERROR: Scanner not found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_HARDWARE_ERROR:
                                                MessageBox.Show("ERROR: Hardware failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PAPER_FED_ERROR:
                                                MessageBox.Show("ERROR: Paper feed problem", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_SCANABORT:
                                                MessageBox.Show("ERROR: Scanner aborted", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_NO_PAPER:
                                                MessageBox.Show("ERROR: No paper found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PAPER_JAM:
                                                MessageBox.Show("ERROR: Paper Jammed", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_FILE_IO_ERROR:
                                                MessageBox.Show("ERROR: Hardware IO failure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_PRINTER_PORT_USED:
                                                MessageBox.Show("ERROR: Printer port already used by other utility (for parallel models only)", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_OUT_OF_MEMORY:
                                                MessageBox.Show("ERROR: Out of memory", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                            case gloCSlibconst.SLIB_ERR_INVALID_SCANNER:
                                                MessageBox.Show("ERROR: Scanner type is not supported", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retVal = 0;
                                                break;
                                        }
                                    }
                
                                    #endregion " .Check if any Scan error present "
									
                if(retVal==0)
                    return false;

                if (result < 0 && result != -13)
                {
                    _bIsInsuranceCardScanImage = false;
                    return false;
                }

                if (result >= 0)
                {
                    _bIsInsuranceCardScanImage = true;
                    return true;

                }
                else if (result < 0)
                {
                    _bIsInsuranceCardScanImage = false;
                    return false;
                }
            }

            else
            {
                MessageBox.Show("Scanner not valid. Please check the connection to the scanner.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bIsInsuranceCardScanWithOCR = false;
                return false; ;
            }
            return false; 
            #endregion
        }

        #endregion


        #region " LoadSettings "
        private void LoadSettings()
        {
            using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
            {
                try
                {

                    #region " Get Resolution settings for Insurance "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance"))
                    {
                        case "300":
                            _Resolution = 300;
                            break;
                        case "600":
                            _Resolution = 600;
                            break;
                        default:
                            _Resolution = 600;
                            break;
                    }



                    #endregion

                    #region " Get ColorScheme Settings for Scan "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme"))
                    {
                        case "4"://true color
                            _ColorScheme = 2;
                            break;
                        case "1"://gray 
                            _ColorScheme = 1;
                            break;
                        case "2"://black & white
                            _ColorScheme = 0;
                            break;
                        default:
                            _ColorScheme = 2;
                            break;
                    }


                    #endregion



                    #region " Get Scan Mode Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex") != "")
                    {
                        string _scanduplex = oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex");
                        if (_scanduplex == "1")
                        {
                            _DuplexScan = true;
                        }
                        else
                        {
                            _DuplexScan = false;

                        }
                        _scanduplex = null;
                    }
                    else
                    {
                        _DuplexScan = false;

                    }

                    #endregion

                    //Added on 05052010
                    #region " Get Selected Scanner Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") != "")
                    {
                        if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") == "None")
                        {
                            _SelectedScanner = "None";
                        }
                        else
                        {
                            string _sScannerName = oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner");
                            if (_sScannerName.ToString().Trim().Length > 0)
                            {
                                _SelectedScanner = _sScannerName;
                            }
                            else
                            {
                                _SelectedScanner = "None";
                            }
                            _sScannerName = null;
                        }
                    }
                    else
                    {
                        _SelectedScanner = "None";
                    }

                    #endregion


                    #region " Get CenterImage Settings for Scan "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "CenterImage").ToString().ToUpper())
                    {
                        case "1"://true color
                            _CenterImage = true;
                            break;
                        case "0"://true color
                            _CenterImage = false;
                            break;
                        default:
                            _CenterImage = true;
                            break;
                    }


                    #endregion


                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oSettings != null) { oSettings.Dispose(); }
                }
            }//Using //04-03-2011 : Commperss
        }
        #endregion


        #region   "Private Method"

       

        private void RotateScannedImage(int angle)
        {
            if (angle > 0)
            {
                int rotateAngle = 4 - angle;
                int result = _image.RotateImage(_CardFaceImagePath, rotateAngle, 0, _CardFaceImagePath);

                // Check result
                //if (result >= 0)
                //    picScannedImage.Image = _GetImage(txtFileName.Text);
                //else
                //    Alert(CSSNCore.GetImageClassError(result));

                if (_sLibEx.Duplex == 1)
                {
                    result = _image.RotateImage(_CardBackImagePath, rotateAngle, 0, _CardBackImagePath);

                    // Check result
                    //if (result >= 0)
                    //    pictureBox1.Image = _GetImage(CSSNCore.tempFolderPath + "scannedImage-back.jpg");
                    //else
                    //    Alert(CSSNCore.GetImageClassError(result));
                }
            }
        }

        #endregion

    }

    class gloScannerGeneral
    {


        public static string RetrieveFileName(string gstrutputDirectory)
        {
            try
            {
                String _NewFileName = String.Empty;

                if (!Directory.Exists(gstrutputDirectory))
                { Directory.CreateDirectory(gstrutputDirectory); }

                _NewFileName = gstrutputDirectory + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss fff tt") + System.Guid.NewGuid().ToString();

                while (File.Exists(_NewFileName + ".jpg") && File.Exists(_NewFileName + "_Back" + ".jpg"))
                {
                    _NewFileName = gstrutputDirectory + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");//DateTime.Now.ToString("MM dd yyyy - hh mm ss fff tt") + System.Guid.NewGuid().ToString(); }
                }
                return _NewFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//RetrieveDirectoryName

        public static Boolean PerformRemoteScan(ref CardScanSettingsScanCardSettings cardScanSettings)
        {
            Boolean result = false;
            //String ScanCardRequestFile = gloGlobal.gloTSPrint.TempPath + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml";
            String ScanCardRequestFile =Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanCardWithOCRFolder, gloGlobal.gloRemoteScanSettings.ToBeScannnedFolderName, gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml");
            
            if (!gloRemoteScanMetaDataWriter.CreateXMLFile(cardScanSettings, ScanCardRequestFile))
            {
                MessageBox.Show("Unable to Scan, Scan request file empty", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            String referenceFileName = Path.GetFileName(ScanCardRequestFile);
            //gloGlobal.gloTSPrint.CopyFileToNetworkShare(ScanCardRequestFile, System.IO.Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanCardWithOCRFolder, gloGlobal.gloRemoteScanSettings.ToBeScannnedFolderName, referenceFileName));

            String scannedPath = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScanCardWithOCRFolder, gloGlobal.gloRemoteScanSettings.ScannedFolderName);
            bool exist = Directory.EnumerateFiles(scannedPath, referenceFileName).Any();
            String ScannedDataFile = Path.Combine(scannedPath, referenceFileName);
            Int16 attempts = 0;
            //Wait for card getting scanned on local machine
            while (!exist && attempts < 200)
            {
                attempts++;
                System.Threading.Thread.Sleep(1000);
                Application.DoEvents();
                exist = Directory.EnumerateFiles(scannedPath, referenceFileName).Any();
            }

            if (exist == true)
            {
                bool bisReady = gloGlobal.clsMISC.WaitForFileToBeReady(ScannedDataFile, 100, 1000);
                if (bisReady)
                {
                    //Copy XML file from mapped folder to local drive
                    String responseFile = Path.Combine(gloGlobal.gloTSPrint.TempPath, referenceFileName);
                    gloGlobal.gloRemoteScanSettings.CopyAndDeleteScannedImages(ScannedDataFile, responseFile);
                    bool bisResponseFileReady = gloGlobal.clsMISC.WaitForFileToBeReady(responseFile, 100, 1000);
                    if (bisResponseFileReady)
                    {
                        cardScanSettings = null;
                        cardScanSettings = gloRemoteScanMetaDataWriter.GetScanCardSettings(responseFile);
                        if (cardScanSettings != null)
                        {
                            CardScanStatus c = (CardScanStatus)cardScanSettings.Status;
                            switch (c)
                            {
                                case CardScanStatus.SentToScan:
                                    MessageBox.Show("Scanning request status not updated", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    result = false;
                                    break;
                                case CardScanStatus.Success:
                                    result = true;

                                    break;
                                case CardScanStatus.Information:
                                    MessageBox.Show(cardScanSettings.Description, "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    result = false;
                                    break;
                                case CardScanStatus.Error:
                                    MessageBox.Show(cardScanSettings.Description, "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    result = false;
                                    break;
                                default:
                                    MessageBox.Show("Somehow scanning request status not updated", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    result = false;
                                    break;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Unable to read scanning responce file", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            result = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Responce file not ready", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result = false;
                    }
                }
                else
                {
                    MessageBox.Show("Scanned file not ready", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    result = false;
                }
            }
            else
            {
                MessageBox.Show("Discarding current scanning request, since no reply from scanning service", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            return result;
        }
    }
}
