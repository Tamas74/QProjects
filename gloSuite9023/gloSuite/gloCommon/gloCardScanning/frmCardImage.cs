using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using PegasusImaging.WinForms.TwainPro5;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using gloSettings;
using System.Linq;
using gloRemoteScanGeneral;

namespace gloCardScanning
{
    public partial class frmCardImage : Form
    {


        #region " Variable Declarations "

        private string _DatabaseConnectionString = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        //   private bool _blnUpdatePatientPhoto = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private CardScanType _CardScanType = CardScanType.None;
        string _sScannerType = string.Empty;
        private TwaingloCardScan oTwaingloCardScan = null;
        private bool DefaultPrinter = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public CardScanType CardType
        {
            get { return _CardScanType; }
            set { _CardScanType = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "
        private IntPtr sc_activewindow = IntPtr.Zero;
        public frmCardImage(string databaseconnectionstring, Int64 patientid, IntPtr Activewindow)
        {
            sc_activewindow = Activewindow;
            InitializeComponent();

            _DatabaseConnectionString = databaseconnectionstring;
            _PatientID = patientid;

            if (!gloGlobal.gloEliminatePegasus.bEliminatePegasus)
            {
                InitTwaingloCardScan();
            }

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrieve DefaultPrinter from AppSettings "

            if (appSettings["DefaultPrinter"] != null)
            {
                if (appSettings["DefaultPrinter"] != "")
                { DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]); }
                else { DefaultPrinter = false; }
            }
            else
            { DefaultPrinter = false; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

        }

        #endregion " Constructor "

        #region " Form Events "

        private void frmCardImage_Load(object sender, EventArgs e)
        {
            try
            {
                _CardScanType = CardScanType.CardImages;
                LoadPatientStripControl();
                //imageControl1 = new gloScanImaging.ImageControl();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            try
            {
                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Check Network Dir/File Exists : " + ex.ToString(), false);
            }

        }

        private void frmCardImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                InitTwaingloCardScan();
                oTwaingloCardScan.DisposeTwainObjects();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion " Form Load "

        #region " Private & Public Functions "

        gloCardPatientStripControl.gloCardPatientStripControl oPatientStripControl = null;
        private void LoadPatientStripControl()
        {
            try
            {
                using (oPatientStripControl = new gloCardPatientStripControl.gloCardPatientStripControl(_DatabaseConnectionString))
                {
                    //oPatientStripControl.ControlSize_Changed -= new gloCardPatientStripControl.gloCardPatientStripControl.ControlSizeChanged(oPatientStripControl_ControlSize_Changed);//dhruv
                    //oPatientStripControl.ControlSize_Changed += new gloCardPatientStripControl.gloCardPatientStripControl.ControlSizeChanged(oPatientStripControl_ControlSize_Changed);
                    oPatientStripControl.DTP.Visible = false;
                    oPatientStripControl.FillDetails(_PatientID, gloCardPatientStripControl.FormName.Billing, 1, false);
                    pnlPatientStrip.Controls.Add(oPatientStripControl);
                    oPatientStripControl.Dock = DockStyle.Fill;
                    oPatientStripControl.SendToBack();
                    oPatientStripControl.Visible = false;
                    pnlPatientStrip.Height = 88;
                    pnlPatientStrip.Visible = true;
                    panel1.SendToBack();
                    pnlCards.BringToFront();
                    lblPatientCode.Text = oPatientStripControl.PatientCode;
                    lblPatientName.Text = oPatientStripControl.PatientName;
                    lblProvider.Text = oPatientStripControl.Provider;
                    if (_PatientID == 0)
                    {
                        lblDOB.Text = "";
                    }
                    else
                    {
                        lblDOB.Text = oPatientStripControl.PatientDateOfBirth.ToShortDateString();
                    }
                    lblAge.Text = oPatientStripControl.PatientAge.Age;
                    lblTodaysDate.Text = DateTime.Now.ToShortDateString();
                }




            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }




        }
        void oPatientStripControl_ControlSize_Changed(object sender, EventArgs e)
        {
            try
            {
                //pnlPatientStrip.Height = oPatientStripControl.Height + 2;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }
        private void SaveData()
        {
            gloCardScanning ogloCardScanning = new gloCardScanning(_DatabaseConnectionString);
            //Int64 _PatId = 0;
            bool _IsDataSaved = false;

            try
            {
                if (_PatientID <= 0)
                {
                    MessageBox.Show("Please select patient", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (pb_FrontSide.Image == null)
                {
                    MessageBox.Show("Please select the card image.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (ogloCardScanning != null)
                {
                    if (pb_BackSide.Image != null)
                    {
                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, pb_BackSide.Image, DateTime.Now, _CardScanType.GetHashCode(), "", _UserID);
                    }
                    else
                    {
                        _IsDataSaved = ogloCardScanning.SaveScanData(_PatientID, pb_FrontSide.Image, null, DateTime.Now, _CardScanType.GetHashCode(), "", _UserID);
                    }
                }
                this.Close();

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (ogloCardScanning != null) { ogloCardScanning.Dispose(); ogloCardScanning = null; }
            }
        }
        private void DisposeAllAttachedEvent()
        {
            //oPatientStripControl.ControlSize_Changed -= new gloCardPatientStripControl.gloCardPatientStripControl.ControlSizeChanged(oPatientStripControl_ControlSize_Changed);
            this.rbOther.CheckedChanged -= new System.EventHandler(this.rbOther_CheckedChanged);
            this.rbInsuranceCard.CheckedChanged -= new System.EventHandler(this.rbInsuranceCard_CheckedChanged);
            this.rbDrivingLicense.CheckedChanged -= new System.EventHandler(this.rbDrivingLicense_CheckedChanged);
            this.tsb_Scan.Click -= new System.EventHandler(this.tsb_Scan_Click);
            this.tsb_ClearData.Click -= new System.EventHandler(this.tsb_ClearData_Click);
            this.tsb_Print.Click -= new System.EventHandler(this.tsb_Print_Click);
            this.tsb_Save.Click -= new System.EventHandler(this.tsb_Save_Click);
            this.tsb_Cancel.Click -= new System.EventHandler(this.tsb_Cancel_Click);
            this.printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            this.Load -= new System.EventHandler(this.frmCardImage_Load);
            this.FormClosed -= new System.Windows.Forms.FormClosedEventHandler(this.frmCardImage_FormClosed);
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.frmCardImage_FormClosing);

        }
        private void DisposeAllControls()
        {

            //this.axms_InsuranceCard.Dispose();
            //this.imagXpress1.Dispose();
            this.twainPro1.Dispose();
            this.printDialog1.Dispose();
            this.printDoc.Dispose();

        }
        private void DisposeAllPrivateVariable()
        {

            _DatabaseConnectionString = null;
            _messageBoxCaption = null;
            _UserName = null;
            appSettings = null;
            _sScannerType = null;
            if (oTwaingloCardScan != null)
            {
                oTwaingloCardScan.Dispose();
                oTwaingloCardScan = null;
            }
        }
        private void PrintImage()
        {
            try
            {
                if (pb_FrontSide.Image == null)
                {
                    MessageBox.Show("Please scan the image first. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        Dictionary<String, Byte[]> dictImages = printdoc_Print_Conversion(8.5f, 11f, 600, 600);
                        string fileName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".zip", "MMddyyyyHHmmssffff");
                        List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs = new List<gloPrintDialog.gloPrintProgressController.DocumentInfo>();
                        List<string> ZipedFiles = gloGlobal.gloTSPrint.ZipAllBytes(dictImages, fileName, gloGlobal.gloTSPrint.NoOfPages);
                        for (int i = 0; i <= ZipedFiles.Count - 1; i++)
                        {
                            gloPrintDialog.gloPrintProgressController.DocumentInfo DocInfo = new gloPrintDialog.gloPrintProgressController.DocumentInfo();
                            DocInfo.PdfFileName = ZipedFiles[i];
                            DocInfo.SrcFileName = ZipedFiles[i];
                            DocInfo.footerInfo = null;
                            lstDocs.Add(DocInfo);
                        }
                        gloPrintDialog.gloPrintProgressController.SendForPrint(lstDocs,true);
                    }
                    else
                    {
                        if (DefaultPrinter)
                        {
                            printDoc.Print();
                        }
                        else
                        {
                            if (printDialog1.ShowDialog(this) == DialogResult.OK)
                            {
                                //set printer settings on printdocument object
                                printDoc.PrinterSettings = printDialog1.PrinterSettings;
                                //print...
                                printDoc.Print();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);//message shown on exception form 
            }
        }

        //private void SendForPrint(List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs)
        //{
        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

        //    try
        //    {
        //        gloPrintDialog.gloExtendedPrinterSettings extendedPrinterSettings = new gloPrintDialog.gloExtendedPrinterSettings();
        //        extendedPrinterSettings.IsShowProgress = false;
        //        extendedPrinterSettings.IsBackGroundPrint = true;
        //        ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(lstDocs, null, extendedPrinterSettings, blnUseEMFForSSRS: true);
        //        ogloPrintProgressController.ShowProgress(null);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;

        //    }
        //    finally
        //    {
        //    }
        //}

        private void DeleteTempImages()
        {
            try
            {
                string sPath = null;
                imageControl1.CloseCurrentImage();

                if (pb_FrontSide.ImageLocation != null)
                {
                    sPath = pb_FrontSide.ImageLocation;
                    pb_FrontSide.ImageLocation = null;
                    DeletePhysicalImgFile(sPath);
                }

                if (pb_BackSide.ImageLocation != null)
                {
                    sPath = pb_BackSide.ImageLocation;
                    pb_BackSide.ImageLocation = null;
                    DeletePhysicalImgFile(sPath);
                }

                if (Directory.Exists(oTwaingloCardScan.TempProcessDirPath) == true)
                {

                    //dhruv
                    if (pb_FrontSide != null)
                    {
                        pb_FrontSide.ImageLocation = string.Empty;
                        pb_FrontSide.ImageLocation = "";
                        if (pb_FrontSide.Image != null)
                        {
                            pb_FrontSide.Image.Dispose();
                            pb_FrontSide.Image = null;
                        }
                        pb_FrontSide.Dispose();
                        pb_FrontSide = null;
                    }
                    if (pb_BackSide != null)
                    {
                        pb_BackSide.ImageLocation = string.Empty;
                        pb_BackSide.ImageLocation = "";
                        if (pb_BackSide.Image != null)
                        {
                            pb_BackSide.Image.Dispose();
                            pb_BackSide.Image = null;
                        }
                        pb_BackSide.Dispose();
                        pb_BackSide = null;

                    }

                    oTwaingloCardScan.CardBackImagePath = "";
                    oTwaingloCardScan.CardFaceImagePath = "";
                    oTwaingloCardScan.CardFrontImagePath = "";





                    //if (imageXView1 != null)
                    //{
                    //    imageXView1.Dispose();
                    //    imageXView1 = null;
                    //}

                    //for Image


                    //Directory.Delete(_TempProcessDirPath, true);

                    DirectoryInfo oDirInfo = new DirectoryInfo(oTwaingloCardScan.TempProcessDirPath);

                    foreach (FileInfo oFile in oDirInfo.GetFiles())
                    {
                        try
                        {
                            File.Delete(oFile.FullName);
                        }
                        catch (IOException ioEX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(), false);//dhruv
                        }


                    }
                    if (oDirInfo != null)
                    {
                        oDirInfo = null;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region " ToolStrip Button Click Events "
        private static bool _isDoubleClicked = false;//04-03-2011 : Commperss
        //Save & close
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (_isDoubleClicked == false)
            {
                _isDoubleClicked = true;
                SetScanCardType();
                SaveData();
                _isDoubleClicked = false;
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                InitTwaingloCardScan();
                oTwaingloCardScan.DisposeTwainObjects();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (IOException ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.Close();
            }
        }
        //Start/04-03-2011 : Commperss
        private void tsb_ClearData_Click(object sender, EventArgs e)
        {
            string sImgPath = string.Empty;

            if (_isDoubleClicked == false)
            {
                _isDoubleClicked = true;

                imageControl1.CloseCurrentImage();

                if (pb_FrontSide != null)
                {
                    if (pb_FrontSide.Image != null)
                    {
                        pb_FrontSide.Image.Dispose();
                        pb_FrontSide.Image = null;
                    }

                    sImgPath = pb_FrontSide.ImageLocation;
                    pb_FrontSide.ImageLocation = null;

                    DeletePhysicalImgFile(sImgPath);
                }

                if (pb_BackSide != null)
                {
                    if (pb_BackSide.Image != null)
                    {
                        pb_BackSide.Image.Dispose();
                        pb_BackSide.Image = null;
                    }

                    sImgPath = pb_BackSide.ImageLocation;
                    pb_BackSide.ImageLocation = null;

                    DeletePhysicalImgFile(sImgPath);
                }
                
                _isDoubleClicked = false;
            }
        }

        private void DeletePhysicalImgFile(string sImgPath)
        {
                if (!string.IsNullOrEmpty(sImgPath))
                {
                    if (File.Exists(sImgPath))
                    {
                        try
                        {
                            File.Delete(sImgPath);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                }
        }

        private void tsb_LoadImage_Click(object sender, EventArgs e)
        {
            string sTempPath=null;

            if (_isDoubleClicked == false)
            {
                _isDoubleClicked = true;
                try
                {
                    OpenFileDialog opnflDlg = new OpenFileDialog();
                    //04-03-2011 : Commperss
                    if (opnflDlg != null)
                    {
                        opnflDlg.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
                        opnflDlg.ShowDialog(this);
                        if (System.IO.File.Exists(opnflDlg.FileName) == true)
                        {
                          //  Resolved bug no.76439:: Driver License lliquid link not working
                            //if (rbDrivingLicense.Checked)
                            //    _CardScanType = CardScanType.DrivingLicense;
                            //else if (rbInsuranceCard.Checked)
                            //    _CardScanType = CardScanType.InsuranceCard;
                            //else if (rbOther.Checked)
                            //    _CardScanType = CardScanType.CardImages;
                            SetScanCardType();

                            string sTempImgName = "IMG_" + System.Guid.NewGuid().ToString() + Path.GetExtension(opnflDlg.FileName);

                            if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                            {
                                sTempPath = Path.Combine(gloGlobal.gloTSPrint.TempPath, sTempImgName);
                            }
                            else
                            {
                                sTempPath = Path.Combine(oTwaingloCardScan.TempProcessDirPath, sTempImgName);
                            }

                            File.Copy(opnflDlg.FileName, sTempPath);

                            pb_FrontSide.ImageLocation =sTempPath ;//;opnflDlg.FileName

                        }
                    }
                    //04-03-2011 : Commperss
                    if (opnflDlg != null)
                    {
                        opnflDlg.Dispose();
                        opnflDlg = null;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                _isDoubleClicked = false;
            }
        }

        private void SetScanCardType() //added for bugid 104297 If we load image as "Driver's License", It is saved as "Insurance card".
        {
            if (rbDrivingLicense.Checked)
                _CardScanType = CardScanType.DrivingLicense;
            else if (rbInsuranceCard.Checked)
                _CardScanType = CardScanType.InsuranceCard;
            else if (rbOther.Checked)
                _CardScanType = CardScanType.CardImages;
        }
        private void tsb_Scan_Click(object sender, EventArgs e)
        {
            try
            {
                ts_Commands.Enabled = false; //Disable ToolStrip at the time of Scanning.

                if (_isDoubleClicked == false)
                {
                    _isDoubleClicked = true;

                    pb_FrontSide.ImageLocation = null;
                    pb_BackSide.ImageLocation = null;

                    SetScanCardType();

                    //send to service scan if remote scanning setting enabled
                    if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan || gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                    {
                        CallScanningForRemoteScan(gloGlobal.gloEliminatePegasus.bEliminatePegasus);
                    }
                    else
                    {
                        InitTwaingloCardScan();
                        oTwaingloCardScan.TwainScaningImage();
                        if (oTwaingloCardScan.CardFrontImagePath != "" && oTwaingloCardScan.CardFrontImagePath.Length > 0 && File.Exists(oTwaingloCardScan.CardFrontImagePath))
                        {
                            pb_FrontSide.ImageLocation = oTwaingloCardScan.CardFrontImagePath;
                        }

                        if (oTwaingloCardScan.CardBackImagePath != "" && oTwaingloCardScan.CardBackImagePath.Length > 0 && File.Exists(oTwaingloCardScan.CardBackImagePath))
                        {
                            pb_BackSide.ImageLocation = oTwaingloCardScan.CardBackImagePath;
                        }
                    }

                    _isDoubleClicked = false;

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (!gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                {
                    ts_Commands.Enabled = true;
                }
            }

        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            if (_isDoubleClicked == false)
            {
                _isDoubleClicked = true;

                try
                {
                    PrintImage();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                _isDoubleClicked = false;
            }
        }

        #endregion " ToolStrip Button Click Events "

        #region "Private Events"




        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.PageUnit = GraphicsUnit.Pixel;
                _DpiX = e.Graphics.DpiX;
                _DpiY = e.Graphics.DpiY;
                _bmpWidth = (float)e.PageBounds.Width * _DpiX / 100.0f;
                _bmpHeight = (float)e.PageBounds.Height * _DpiY / 100.0f;
                FitImageToScaleAndKeepAtCenter(e.Graphics);
                //int y = 3;

                //if (pb_FrontSide.Image != null)
                //{
                //    //Start/Image File Size is null used blank Try/Catch.
                //    //Image logo = Image.FromFile(pb_FrontSide.ImageLocation);
                //    Image logo = gloCardScanning.ImageFromFile(pb_FrontSide.ImageLocation);
                //    //End/Image File Size is null used blank Try/Catch.

                //    if (logo != null)
                //    {
                //        e.Graphics.DrawImage(logo, new Point(3, y));
                //        y = y + 250;
                //    }
                //    if (logo != null)
                //    {
                //        logo.Dispose();
                //        logo = null;
                //    }
                //}
                //if (pb_BackSide.Image != null)
                //{
                //    //Start/Image File Size is null used blank Try/Catch.
                //    //Image logo = Image.FromFile(pb_BackSide.ImageLocation);
                //    Image logo = gloCardScanning.ImageFromFile(pb_BackSide.ImageLocation);
                //    //End/Image File Size is null used blank Try/Catch.

                //    if (logo != null)
                //    {
                //        e.Graphics.DrawImage(logo, new Point(3, y));
                //        y = y + 250;
                //    }
                //    if (logo != null)
                //    {
                //        logo.Dispose();
                //        logo = null;
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForImages;
        private int CreateEMFForImages(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                thisGraphics.Clear(Color.White);
                FitImageToScaleAndKeepAtCenter(thisGraphics);

                return 0;
            }
            catch
            {
                return 1;
            }

        }

        public Dictionary<String, Byte[]> printdoc_Print_Conversion(float pageWidth, float pageHeight, float DpiX, float DpiY)
        {

            Dictionary<String, Byte[]> myList = new Dictionary<String, Byte[]>();
            _DpiX = DpiX;
            _DpiY = DpiY;
            _bmpWidth = pageWidth * _DpiX;
            _bmpHeight = pageHeight * _DpiY;


            bool bAnyError = true;
            try
            {
                using (System.Drawing.Bitmap NewBitmap = new Bitmap(Convert.ToInt32(_bmpWidth), Convert.ToInt32(_bmpHeight)))
                {
                    byte[] emfBytes = null;
                    NewBitmap.SetResolution(_DpiX, _DpiY);
                    try
                    {
                        try
                        {
                            if (toCreateEMF)
                            {
                                emfBytes = gloGlobal.CreateEMF.GetEMFBytes(pageWidth, pageHeight, _bmpWidth, _bmpHeight, CreateEMFForImages);
                                bAnyError = false;
                            }
                        }
                        catch
                        {
                        }
                        if (bAnyError)
                        {
                            toCreateEMF = false;
                            using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
                            {
                                FitImageToScaleAndKeepAtCenter(eGraphics);

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    NewBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    try
                                    {
                                        ms.Flush();

                                    }
                                    catch
                                    {
                                    }

                                    myList.Add("1", ms.ToArray());
                                    try
                                    {
                                        ms.Close();

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            myList.Add("1", emfBytes);
                        }

                    }
                    catch (Exception ex)
                    {
                        bAnyError = true;
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during conversion before printing in frmCardIamges", false);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    }
                    if (bAnyError)
                    {
                        return myList;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            return myList;
        }
        private float _bmpHeight = 0f;
        private float _bmpWidth = 0f;
        private float _DpiX = 600f;
        private float _DpiY = 600f;
        private void FitImageToScaleAndKeepAtCenter(System.Drawing.Graphics eGraphics)
        {
            try
            {
                if ((pb_FrontSide == null) && (pb_BackSide == null))
                {
                    return;
                }
                //SLR: Compute the card width and height to current graphics coordinates
                float cardFrontWidth = pb_FrontSide.Image == null ? 0 : (float)pb_FrontSide.Image.Width * _DpiX / pb_FrontSide.Image.HorizontalResolution;
                float cardFrontHeight = pb_FrontSide.Image == null ? 0 : (float)pb_FrontSide.Image.Height * _DpiY / pb_FrontSide.Image.VerticalResolution;
                float cardBackWidth = pb_BackSide.Image == null ? 0 : (float)pb_BackSide.Image.Width * _DpiX / pb_BackSide.Image.HorizontalResolution;
                float cardBackHeight = pb_BackSide.Image == null ? 0 : (float)pb_BackSide.Image.Height * _DpiY / pb_BackSide.Image.VerticalResolution;

                float xMargin = _DpiX;
                float yMargin = _DpiY;
                float slices = (pb_FrontSide.Image == null) || (pb_BackSide.Image == null) ? 2.0f : 3.0f;

                //SLR: Compute if pages to be splitted horizontally
                float yHorzGutter = slices * yMargin;
                float xHorzGutter = 2.0f * xMargin;
                float horzSplitScale = 1f;
                float yHorzScale = (_bmpHeight - yHorzGutter) / (cardFrontHeight + cardBackHeight);
                if (horzSplitScale > yHorzScale)
                {
                    horzSplitScale = yHorzScale;
                }
                float xHorzScale = (_bmpWidth - xHorzGutter) / Math.Max(cardFrontWidth, cardBackWidth);
                if (horzSplitScale > xHorzScale)
                {
                    horzSplitScale = xHorzScale;
                }


                //SLR: Compute if pages to be splitted vertically
                float yVertGutter = slices * xMargin;
                float xVertGutter = 2.0f * yMargin;
                float vertSplitScale = 1f;
                float xVertScale = (_bmpWidth - xVertGutter) / (cardFrontWidth + cardBackWidth);
                if (vertSplitScale > xVertScale)
                {
                    vertSplitScale = xVertScale;
                }
                float yVertScale = (_bmpHeight - yVertGutter) / Math.Max(cardFrontHeight, cardBackHeight);
                if (vertSplitScale > yVertScale)
                {
                    vertSplitScale = yVertScale;
                }


                if (horzSplitScale >= vertSplitScale) //SLR: horizontal scale is bigger, hence go to horizontal splitting
                {
                    cardFrontHeight *= horzSplitScale;
                    cardFrontWidth *= horzSplitScale;
                    cardBackWidth *= horzSplitScale;
                    cardBackHeight *= horzSplitScale;

                    yMargin = (_bmpHeight - (cardFrontHeight + cardBackHeight)) / (slices);
                    if (pb_FrontSide.Image != null)
                    {
                        xMargin = (_bmpWidth - cardFrontWidth) / 2.0f;
                        eGraphics.DrawImage(pb_FrontSide.Image, new RectangleF(xMargin, yMargin, cardFrontWidth, cardFrontHeight));
                        yMargin += yMargin + cardFrontHeight;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        xMargin = (_bmpWidth - cardBackWidth) / 2.0f;
                        eGraphics.DrawImage(pb_BackSide.Image, new RectangleF(xMargin, yMargin, cardBackWidth, cardBackHeight));
                    }
                }
                else //SLR: vertical scale is bigger, hence go to vertical splitting 
                {
                    cardFrontHeight *= vertSplitScale;
                    cardFrontWidth *= vertSplitScale;
                    cardBackWidth *= vertSplitScale;
                    cardBackHeight *= vertSplitScale;

                    xMargin = (_bmpWidth - (cardFrontWidth + cardBackWidth)) / (slices);
                    if (pb_FrontSide.Image != null)
                    {
                        yMargin = (_bmpHeight - cardFrontHeight) / 2.0f;
                        eGraphics.DrawImage(pb_FrontSide.Image, new RectangleF(xMargin, yMargin, cardFrontWidth, cardFrontHeight));
                        xMargin += xMargin + cardFrontWidth;
                    }
                    if (pb_BackSide.Image != null)
                    {
                        yMargin = (_bmpHeight - cardBackHeight) / 2.0f;
                        eGraphics.DrawImage(pb_BackSide.Image, new RectangleF(xMargin, yMargin, cardBackWidth, cardBackHeight));
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void frmCardImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            oTwaingloCardScan.DisposeTwainObjects();
            InitTwaingloCardScan();
            DeleteTempImages();
            DisposeAllPrivateVariable();
            DisposeAllAttachedEvent();
            DisposeAllControls();
        }

        private void InitTwaingloCardScan()
        {
            if (oTwaingloCardScan == null)
            {
                oTwaingloCardScan = new TwaingloCardScan(_PatientID);
            }
        }

        private void rbInsuranceCard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInsuranceCard.Checked == true)
            {
                rbInsuranceCard.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInsuranceCard.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbDrivingLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrivingLicense.Checked == true)
            {
                rbDrivingLicense.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbDrivingLicense.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOther.Checked == true)
            {
                rbOther.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbOther.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }

        }

        #endregion "Private Events"

        private void frmCardImage_Shown(object sender, EventArgs e)
        {
            gloGlobal.WordDialogBoxBackgroundCloser.ForceWindowIntoForeground(sc_activewindow);
            this.Activate();
        }


        //string _sScannerType = string.Empty;
        float _Resolution = 300;
        string _ScanMode = "RGB";
        bool _DuplexScan = false;
        string _sSelectedScannerName = string.Empty;
        private void LoadSettings()
        {
            using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
            {
                try
                {

                    #region " Get Resolution settings for Insurance "
                    String resolutionKey = "ResolutionInsurance";
                    if ( _CardScanType == CardScanType.DrivingLicense)
                    {
                        resolutionKey = "RESOLUTIONLICENSE";
                    }
                    switch (oSettings.ReadSettings_XML("CardScannerSettings", resolutionKey))
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
                            _ScanMode = "RGB";
                            break;
                        case "1"://gray 
                            _ScanMode = "Gray";
                            break;
                        case "2"://black & white
                            _ScanMode = "BW";
                            break;
                        default:
                            _ScanMode = "RGB";
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
                            _sSelectedScannerName = "None";
                        }
                        else
                        {
                            string _sScannerName = oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner");
                            if (_sScannerName.ToString().Trim().Length > 0)
                            {
                                _sSelectedScannerName = _sScannerName;
                            }
                            else
                            {
                                _sSelectedScannerName = "None";
                            }
                        }

                    }
                    else
                    {
                        _sSelectedScannerName = "None";
                    }

                    #endregion


                    #region " Get ScannerType Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType") != "")
                    {
                        string _sSType = oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType");
                        if (_sSType.ToString().Trim().Length > 0)
                        {
                            _sScannerType = _sSType;
                        }
                        else
                        {
                            //? _sScannerType = "Other";   ?????????????????????
                        }
                        _sSType = null;
                    }
                    else
                    {
                        // ?_sScannerType = "";                 ???????????????
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
            }
        }

        public int imgCnt = 0;
        Int16 attempts = 0;
        private static gloGlobal.gloClipboardWatcher myWatcher = null;

        private static gloGlobal.clsDatalog logdata = new gloGlobal.clsDatalog("gloTSPrint");
        private static string nextFileName = "";
        void myWatcher_OnClipboardContentChanged(object sender, EventArgs e)
        {
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = false;
                ScanTimer.Stop();
            }
            //AuditLogErrorMessage("myWatcher_OnClipboardContentChanged: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            string sExtForScan = string.Empty;

            string sImgTempPath = Path.Combine(gloGlobal.gloTSPrint.TempPath, "Image - " + (imgCnt + 1).ToString("000#"));
            logdata.Log("myWatcher_OnClipboardContentChanged executed for sImgTempPath : " + sImgTempPath);
            int nCurrProcId = gloGlobal.gloProgressAndClipboard.getProcessIdInt();
            //AuditLogErrorMessage("BEFORE GetImageFromClipboard" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            gloGlobal.gloProgressAndClipboard objgloProgressAndClipboard = gloGlobal.gloProgressAndClipboard.GetImageFromClipboard(3, sImgTempPath, ref sExtForScan, nCurrProcId);
            if (objgloProgressAndClipboard != null)
            {
                logdata.Log("objgloProgressAndClipboard != null sExtForScan : " + sExtForScan);
                if (objgloProgressAndClipboard.objgloDataClipboardCopy != null)
                {
                    ////AuditLogErrorMessage("TOTAL COUNTS :  " + objgloProgressAndClipboard.objgloDataProgressBar.sequenceNo + " , " + objgloProgressAndClipboard.objgloDataProgressBar.totalcnt);
                    ////AuditLogErrorMessage("AFTER GetImageFromClipboard -- >" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                    ////AuditLogErrorMessage("sExtForScan: " + sExtForScan);
                    //if (sExtForScan == ".bar")
                    //{
                    //    using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloDataClipboardCopy.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".next")))
                    //    {
                    //        myFile.Close();
                    //        // interact with myFile here, it will be disposed automatically
                    //    }
                    //    //if (File.Exists(sImgTempPath + sExtForScan))
                    //    //{
                    //    RemoteScanning_ImageLoad(null, true);//sImgTempPath + sExtForScan
                    //    //}
                    //}
                    //else 
                    if (sExtForScan == ".end")
                    {
                        ReleaseScanTimer();
                        imgCnt = 0;
                    }
                    else if (sExtForScan == ".err")
                    {
                        ReleaseScanTimer(bAddFailLog: true);
                        imgCnt = 0;
                    }
                    else
                    {
                        if (sExtForScan != ".wait")
                        {
                            imgCnt++;
                            nCurrScanCnt++;
                            //AuditLogErrorMessage("Before Creating .Next file " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                            //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".next")))
                            //{
                            //    myFile.Close();
                            //    // interact with myFile here, it will be disposed automatically
                            //}

                            if (File.Exists(sImgTempPath + sExtForScan))
                            {

                                //AuditLogErrorMessage("BEFORE RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                                //RemoteScanning_ImageLoad(sImgTempPath + sExtForScan, false);
                                if (imgCnt == 1)
                                {
                                    pb_FrontSide.ImageLocation = sImgTempPath + sExtForScan;
                                }
                                else
                                {
                                    pb_BackSide.ImageLocation = sImgTempPath + sExtForScan;
                                }
                                bScannerStatus = true;
                                //AuditLogErrorMessage("AFTER RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                            }
                            else
                            { imgCnt--;
                            logdata.Log("file not exists : " + sImgTempPath + sExtForScan); 
                            }

                            int totcount = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.PAGESMASK;
                            int scanstatus = objgloProgressAndClipboard.objgloDataProgressBar.totalcnt & gloGlobal.gloDataProgressBar.STAUSMASK;
                            if ((scanstatus > 0) && ((totcount - 1) <= nCurrScanCnt))
                            {
                                if (scanstatus == gloGlobal.gloDataProgressBar.SCANEND)
                                {
                                    ReleaseScanTimer();
                                }
                                else
                                {
                                    ReleaseScanTimer(bAddFailLog: true);
                                }
                                GetScannedFile(".over");
                                imgCnt = 0;
                            }
                            else
                            {
                                GetScannedFile(".next");
                            }
                        }
                        else
                        {
                            //logdata.Log("sExtForScan == .wait");
                            //Calling .next again for wait
                            GetScannedFile(".next");
                        }
                    }
                }
                else
                {
                    logdata.Log("objgloProgressAndClipboard.objgloDataClipboardCopy = null");
                }
                attempts = 0;
            }
            else
            {
                //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".again")))
                //{
                //    myFile.Close();
                //    // interact with myFile here, it will be disposed automatically
                //}
                GetScannedFile(".again");
            }
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = true;
                ScanTimer.Start();
            }
        }

        private void GetScannedFile(string sExt)
        {
            String sFileName = Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + sExt);
            nextFileName = Path.GetFileNameWithoutExtension(sFileName);
            using (var myFile = File.Create(sFileName))
            {
                myFile.Close();
                // interact with myFile here, it will be disposed automatically
            }
            //logdata.Log("GetScannedFile sExt :" + sExt);
        }

        private static Timer ScanTimer = null;
        private void ScanTimerFired(object sender, EventArgs e)
        {
            //AuditLogErrorMessage("ScanTimerFired");
            //AuditLogErrorMessage("attempts : " + attempts);
            ScanTimer.Enabled = false;

            if (attempts < 50)
            {
                attempts++;
                if ((attempts % 10 == 0) || (nextFileName != "" && File.Exists(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, nextFileName + ".pushed"))))
                {
                    //using (var myFile = File.Create(Path.Combine(gloGlobal.gloRemoteScanSettings.ScanPath, gloGlobal.gloRemoteScanSettings.ScannedImgFolderName, gloGlobal.gloProgressAndClipboard.getProcessIdString() + "_" + imgCnt.ToString() + "-" + gloGlobal.clsFileExtensions.GetUniqueID() + ".again")))
                    //{
                    //    myFile.Close();
                    //    // interact with myFile here, it will be disposed automatically
                    //}
                    GetScannedFile(".again");
                }
                ScanTimer.Enabled = true;
            }
            else
            {
                if (attempts != 50)
                {
                    ScanTimer.Enabled = true;
                }
                else
                {
                    attempts = 0;
                    ReleaseScanTimer(bAddFailLog: true);
                }
            }
        }
        private static bool bScannerStatus = false;
        private void StartScanTimer()
        {
            bScannerStatus = false;
            if (ScanTimer == null)
            {
                ScanTimer = new Timer();
                ScanTimer.Interval = 2000;//5000
                ScanTimer.Tick += ScanTimerFired;
            }
            if (ScanTimer != null)
            {
                ScanTimer.Enabled = true;
            }
            attempts = 0;
            if (myWatcher == null)
            {
                myWatcher = gloGlobal.gloProgressAndClipboard.getClipboardWatcher();
            }
            if (myWatcher != null)
            {
                myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                bReleaseClipboardEvents = false;
            }
            //if (objfrmScanProgress == null)
            //{
            //    objfrmScanProgress = new frmScanProgress();
            //    objfrmScanProgress.BringToFront();
            //    objfrmScanProgress.lblCurrentProgress.Text = "Started Remote Scanning...";
            //    objfrmScanProgress.prbarScanning.Maximum = 1;
            //    objfrmScanProgress.prbarScanning.Value = 0;
            //}
            //if (objfrmScanProgress != null)
            //{
            //    objfrmScanProgress.Show();
            //}
            //EnableTaskBarButtons(false);
            ts_Commands.Enabled = false;
            Application.DoEvents();
        }

        bool bReleaseClipboardEvents = false;
        private void ReleaseScanTimer(bool bAddFailLog = false)
        {
            if (!bScannerStatus)
            {
                if (bAddFailLog)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, "Failure", gloAuditTrail.ActivityOutCome.Failure);
                }
            }
            else
            {
                bScannerStatus = false;
            }

            if (ScanTimer != null)
            {
                ScanTimer.Enabled = false;
                ScanTimer.Stop();

                ScanTimer.Tick -= ScanTimerFired;
                ScanTimer.Dispose();
                ScanTimer = null;
            }
            if (myWatcher != null && !bReleaseClipboardEvents)
            {
                try
                {
                    myWatcher.OnClipboardContentChanged -= new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                    bReleaseClipboardEvents = true;
                }
                catch
                {
                }
            }

            //if (objfrmScanProgress != null)
            //{
            //    objfrmScanProgress.Dispose();
            //    objfrmScanProgress = null;
            //}

            ts_Commands.Enabled = true;
            Application.DoEvents();
        }
        public static frmLocalTwainScanLauncher objLocalTwainScanLauncher = null;
        bool bSuccess = false;
        string sScanStatus = null;
        public int imgCntTwain = 0;
        int nCurrScanCnt;

        private void CallScanningForRemoteScan(bool bEliminatePagasus=false)
        {
           //gloEDocumentV3.Common.RemoteScanCommon oRemoteScanCommon = new Common.RemoteScanCommon();
            try
            {
                nCurrScanCnt = 0;
                if (!bEliminatePagasus)
                {
                    if (!gloGlobal.gloTSPrint.isLocalMachineUpdated())
                    {
                        ts_Commands.Enabled = true;
                        Application.DoEvents();
                        return;
                    }
                    if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                    {
                        ts_Commands.Enabled = true;
                        Application.DoEvents();
                        return;
                    }
                }
                string sRetVal = SetRemoteScannerCurrentSettings();

                if (!string.IsNullOrEmpty(sRetVal))
                { gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRetVal, gloAuditTrail.ActivityOutCome.Failure); }
                else
                {
                    if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                    {
                        StartScanTimer();

                        string sScannedFilePath = string.Empty;
                        try
                        {
                            sScannedFilePath = gloRemoteScanGeneral.RemoteScanSettings.CreateCurrentSettingsForRemoteScan();
                            if (string.IsNullOrEmpty(sScannedFilePath))
                            {
                                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                                if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                                {
                                    ReleaseScanTimer();
                                    return;
                                }
                                else
                                {
                                    ReleaseScanTimer();
                                    MessageBox.Show("Unable to start a scan request . Please check whether gloLDSSniffer Service is running and local scan setting is enabled in service", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                nextFileName = Path.GetFileNameWithoutExtension(sScannedFilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                            if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg: true))
                            {
                                ReleaseScanTimer();
                                return;
                            }
                        }
                    }
                    else
                    {
                        imgCntTwain = 0;
                        objLocalTwainScanLauncher = new frmLocalTwainScanLauncher(gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting);
                        objLocalTwainScanLauncher.bFromCardScan = true;
                        if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName.StartsWith("WIA"))
                        {
                            //objLocalTwainScanLauncher.WIAScanning();
                            objLocalTwainScanLauncher.IsWIAScanning = true;
                            objLocalTwainScanLauncher.OnForEachWIAImageScanned += new frmLocalTwainScanLauncher.ForEachWIAImageScanned(objLocalTwainScanLauncher_OnForEachWIAImageScanned);
                            objLocalTwainScanLauncher.OnWIAImageScanningDone += new frmLocalTwainScanLauncher.WIAImageScanningDone(objLocalTwainScanLauncher_OnWIAImageScanningDone);
                        }
                        else
                        {
                            objLocalTwainScanLauncher.IsWIAScanning = false;
                            objLocalTwainScanLauncher.OnForEachImageScanned += new frmLocalTwainScanLauncher.ForEachImageScanned(objLocalTwainScanLauncher_OnForEachImageScanned);
                            objLocalTwainScanLauncher.OnImageScanningDone += new frmLocalTwainScanLauncher.ImageScanningDone(objLocalTwainScanLauncher_OnImageScanningDone);
                            //bLocalScanningDone = false;
                            //AuditLogErrorMessage("Before EnableTaskBarButtons");
                        }
                        //bLocalScanningDone = false;
                        //EnableTaskBarButtons(false);
                        Application.DoEvents();
                        objLocalTwainScanLauncher.ShowDialog();
                        //while (bLocalScanningDone == false)
                        //{
                        //    System.Threading.Thread.Sleep(100);
                        //}
                        if (bSuccess)
                        {
                        }
                        else
                        {
                            MessageBox.Show(sScanStatus);
                        }

                        if (objLocalTwainScanLauncher!=null)
                        {
                            objLocalTwainScanLauncher.Dispose();
                            objLocalTwainScanLauncher = null;
                        }
                        //EnableTaskBarButtons(true);
                        Application.DoEvents();
                    
                    }
                    //string sExt = ".png";
                    //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting != null)
                    //{
                    //    //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName == "BW")
                    //    //{
                    //    //    sExt = ".png";
                    //    //}
                    //    //else 
                    //    {
                    //        if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName != "BW")
                    //        {
                    //            sExt = ".jpeg";
                    //        }
                    //    }
                    //}

                    //string sRet = PerformRemoteScan(sScannedFilePath, sExt);
                    //if (!string.IsNullOrEmpty(sRet))
                    //{ gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRet, gloAuditTrail.ActivityOutCome.Failure); }

                }
            }
            catch (Exception ex)
            {
                ts_Commands.Enabled = true;
                Application.DoEvents();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //oRemoteScanCommon = null;
            }
        }

        public void objLocalTwainScanLauncher_OnForEachImageScanned(object sender, gloTwainScanControllerEventArgs e)
        {
            Image iScannedImage = e.ScannedImg;
            string sImgTempPath = Path.Combine(gloGlobal.gloTSPrint.TempPath, "Image - " + (imgCntTwain + 1).ToString("000#") +".png");

            try
            {
                imageControl1.CloseCurrentImage();
                DeletePhysicalImgFile(sImgTempPath);
                iScannedImage.Save(sImgTempPath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            //oTwaingloCardScan.setImagePath();
            imageControl1.BringToFront();
            //imageXView1.SendToBack();
            imageControl1.SetImageWithPath(sImgTempPath,true);

            if (File.Exists(sImgTempPath))
            {
                imgCntTwain++;
                //AuditLogErrorMessage("BEFORE RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                //RemoteScanning_ImageLoad(sImgTempPath + sExtForScan, false);
                if (imgCntTwain == 1)
                {
                    pb_FrontSide.ImageLocation = sImgTempPath;
                }
                else
                {
                    pb_BackSide.ImageLocation = sImgTempPath ;
                }
                bScannerStatus = true;
                //AuditLogErrorMessage("AFTER RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            }
            else
            { imgCntTwain--; }
           

        }

        void objLocalTwainScanLauncher_OnImageScanningDone(object sender, gloTwainScanningDoneEventArgs e)
        {
            //bLocalScanningDone = true;
            bSuccess = e.bSuccess;
            sScanStatus = e.sScanStatus;
        }

        void objLocalTwainScanLauncher_OnWIAImageScanningDone(object sender, gloWIAScanningDoneEventArgs e)
        {
            bSuccess = e.bSuccess;
            sScanStatus = e.sScanStatus;
        }

        void objLocalTwainScanLauncher_OnForEachWIAImageScanned(object sender, gloWIAScanControllerEventArgs e)
        {
            Image iScannedImage = e.ScannedImg;
            string sImgTempPath = Path.Combine(gloGlobal.gloTSPrint.TempPath, "Image - " + (imgCntTwain + 1).ToString("000#") + ".png");

            try
            {
                imageControl1.CloseCurrentImage();
                DeletePhysicalImgFile(sImgTempPath);
                iScannedImage.Save(sImgTempPath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            //oTwaingloCardScan.setImagePath();
            imageControl1.BringToFront();
            //imageXView1.SendToBack();
            imageControl1.SetImageWithPath(sImgTempPath, true);

            if (File.Exists(sImgTempPath))
            {
                imgCntTwain++;
                //AuditLogErrorMessage("BEFORE RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
                //RemoteScanning_ImageLoad(sImgTempPath + sExtForScan, false);
                if (imgCntTwain == 1)
                {
                    pb_FrontSide.ImageLocation = sImgTempPath;
                }
                else
                {
                    pb_BackSide.ImageLocation = sImgTempPath;
                }
                bScannerStatus = true;
                //AuditLogErrorMessage("AFTER RemoteScanning_ImageLoad " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
            }
            else
            { imgCntTwain--; }
        }

        public string SetRemoteScannerCurrentSettings()
        {
            string sRegVal = null;
            LoadSettings();
            if (String.IsNullOrEmpty(_sSelectedScannerName) || _sSelectedScannerName.ToLower() == "none")
            {
                MessageBox.Show("Please select local scanner in card scanner settings", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "Scanner not selected";
            }
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerID =0;
            //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting == null)
            //{
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting = new gloRemoteScanGeneral.ScannerCurrentSettingsScannerSettings();
            //}
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName = _sSelectedScannerName;

            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName = _ScanMode;


            if (_DuplexScan)
            {
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName="Duplex";
            }
            else
            {
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName = "Front Side";
            }


            //Get Resolution ID
            sRegVal = Convert.ToString(_Resolution);
            //iCurrentID = GetScanSettingID("ScanResolution", sRegVal, iCurrentScanner, 0);
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ResolutionID = Convert.ToString(iCurrentID);
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ResolutionName = sRegVal;
            //

            ////Get Brightness ID
            //sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanBright);
            //iCurrentID = GetScanSettingID("ScanBrightness", sRegVal, iCurrentScanner, 0);
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.BrightnessID = Convert.ToString(iCurrentID); ;
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.BrightnessName = sRegVal;
            ////

            ////Get Contrast ID
            //sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast);
            //iCurrentID = GetScanSettingID("ScanContrast", sRegVal, iCurrentScanner, 0);
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ContrastID = Convert.ToString(iCurrentID); ;
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ContrastName = sRegVal;
            ////

            //Get Supported Size
            //sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize);
            //iCurrentID = GetScanSettingID("ScanSupportedSize", sRegVal, iCurrentScanner, 0);
            //gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeID = Convert.ToString(iCurrentID); ;

            if (_sSelectedScannerName.Contains("FUJITSU") || _sSelectedScannerName.ToUpper().Contains("FUJITSU"))
            {
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeName = "A6";
            }
            else
            {
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeName = "BusinessCard";
            }

            //Image Layout
            float _CardWidth=3.583f;
            float _CardLength=2.166f;
            float X;
            float Y = 0.0f; 
            if (_sSelectedScannerName.Contains("FUJITSU") || _sSelectedScannerName.ToUpper().Contains("FUJITSU")) //check scanner setting for selected scaner
            {
                X = 0.25f;
            }
            else if (_sSelectedScannerName.ToUpper().Contains("SCANSHELL") || _sSelectedScannerName.Contains("SCANSHELL"))////check scanner setting for selected scaner
            {
                //3000D
                if (_sSelectedScannerName.Contains("3000"))
                {
                    X = 2.5f; 
                }
                else
                {
                    X = 0.0f;
                }
            }
            else if (_sSelectedScannerName.ToString().Trim() == string.Empty)
            {
                X = 0.0f;
            }
            else
            {
                X = 2.5f;
                Y = -0.003f;
            }

            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardLeft = X;
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardLength = _CardLength;
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardTop = Y;
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardWidth = _CardWidth;

            // Scan Show UI
            gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ShowUI = false;
            //

            return "";
        }

        //public string PerformRemoteScan(string aScannedFilePath, string sExt)//ref CardScanSettingsScanCardSettings cardScanSettings
        //{
        //    string sResult = string.Empty;
        //    attempts = 0;
        //    //String ScanCardRequestFile = gloGlobal.gloTSPrint.TempPath + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml";
        //    //gloRemoteScanMetaDataWriter.CreateXMLFile(cardScanSettings, ScanCardRequestFile);
        //    //String referenceFileName = Path.GetFileName(ScanCardRequestFile);

        //    //AuditLogErrorMessage("PerformRemoteScan myWatcher: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //    if (myWatcher == null)
        //    {
        //        // AuditLogErrorMessage("myWatcher==null" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //        myWatcher = gloGlobal.gloDataClipboardCopy.getClipboardWatcher();
        //        //myWatcher.Start();
        //    }
        //    if (myWatcher != null)
        //    {
        //        myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
        //    }


        //    // AuditLogErrorMessage("OUT of PerformRemoteSCan : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        //    return sResult;
        //}
    }
}

