using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using gloEDocumentV3.Enumeration;

using C1.Win.C1FlexGrid;

using System.IO;
using System.Drawing.Imaging;
//using PegasusImaging.WinForms.ImagXpress9;

namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_ReceivedFax : Form
    {

        #region " Enumerations "

        public enum Col_Type
        { Folder = 0, File = 1, Page = 2 }

        enum ShowHideFlag
        { Opearations = 1, Document = 2, Legend = 3, Patient = 4, Pages = 5, Preview = 6, Notes = 7, Tags = 8, Search = 9 }

        #endregion

        #region " C1 Document Grid Variables "

        private const int COL_DOC_SELECT = 0;
        private const int COL_DOC_NAME = 1;
        private const int COL_DOC_PATH = 2;
        private const int COL_DOC_TYPE = 3;
        private const int COL_DOC_HiddenPath = 4;
        private const int COL_DOC_HiddenPageNo = 5;
        private const int COCL_DOC_HiddenDocumentName = 6;


        private const int COL_DOC_COUNT = 7;

        #endregion " C1 Document Grid Variables "

        #region " Tiff Manipulation Related Variables "

        int rowIndex = 0;
        int pageFrameIndex = 0;
        string _ImageFileName = "";

        #endregion

        #region " Variables Declarations "

        public delegate void RefreshDocuments();
        public event RefreshDocuments EvnRefreshDocuments;
        public delegate void ActivateReciveFax(Int64 PatientID);
        // public event ActivateReciveFax Int64<PatientID> EvnActivateReciveFax;
        public event ActivateReciveFax EvnActivateReciveFax;
        private string _FaxSystemPath = "";
        private string _ReceivedFaxFilesFolderPath = "";
        private string _ReceivedFaxCategory = "";
        public bool oDialogResultIsOK = false;
        public Int64 oDialogDocumentID = 0;
        public Int64 oDialogContainerID = 0;
        public Int64 DMSPatientID = 0;
        private ArrayList _SelectedDocuments = null;
        

        private gloPatient.PatientListControl oPatientListControl = null;

        public Int64 _PatientID = 0;
        string _PatientCode = "";
        string _PatientName = "";
        string _PrintFilePathe = "";
        //string path = string.Empty;
        //private Int32 nTiffpageCount = 0;
       

        private int _fileLoadCount = 10;
        private bool _IsLoading = false;
        public string _ErrorMessage = "";
        //enum_ZoomState _ZoomState = enum_ZoomState.BestFit;
        bool _blnUseDefaultPrinter;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;
       // public Int64 _DMSPatientID = 0;

        //Shubhangi 20091231
        public Boolean IsformLoad = false;
        ToolTip oToolTip = new ToolTip();

        pdftron.PDF.PDFViewCtrl oPDFView;
        pdftron.PDF.PDFDoc oPDFDoc;
       // Byte nPageNo;
        Label oProcessLabel = null;

        #endregion

        #region " Property Procedures "
        string _strNonXMLPath = "";
        public string strNonXMLPath
        {
            get { return _strNonXMLPath; }
            set { _strNonXMLPath = value; }
        }
        public ArrayList oSelectedDocuments
        {
            get { return _SelectedDocuments; }
            set { _SelectedDocuments = value; }
        }

        public int FileLoadCount
        {
            get { return _fileLoadCount; }
            set { _fileLoadCount = value; }
        }

        #endregion " Property Procedures "


        #region " Constructor "
          public frmEDocEvent_ReceivedFax()
         {
             if (appSettings["DataBaseConnectionString"] != null)
             {
                 if (appSettings["DataBaseConnectionString"] != "")
                 { _databaseconnectionstring = System.Convert.ToString(appSettings["DataBaseConnectionString"]).Trim(); }
                 else
                 { _databaseconnectionstring = ""; }
             }
             else
             { _databaseconnectionstring = ""; }



             InitializeComponent();
             _SelectedDocuments = new ArrayList();
            }
        public frmEDocEvent_ReceivedFax(string ReceivedFaxFilesFolderPath, string Category)
        {

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _databaseconnectionstring = System.Convert.ToString(appSettings["DataBaseConnectionString"]).Trim(); }
                else
                { _databaseconnectionstring = ""; }
            }
            else
            { _databaseconnectionstring = ""; }



            InitializeComponent();
            if (!gloGlobal.gloTSPrint.isCopyPrint)
            {
                if (myPrinterSetting == null)
                {
                    myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                }
            }
            _SelectedDocuments = new ArrayList();
            // CR00000126 : FAX for Terminal Server
            // Code changes reverted back to 7007. Issue resolved by adding a new setting (FaxDownloadDirectory) for Terminal Server.
            _ReceivedFaxFilesFolderPath = ReceivedFaxFilesFolderPath;
            _ReceivedFaxCategory = Category;
            _FaxSystemPath = _ReceivedFaxFilesFolderPath + "\\FaxSystem";
            ShowFaxFilesDetails();

            //..Register Image Xpress 9.0
            //imagXpress1.Licensing.UnlockRuntime(1908208815, 373700144, 1341181380, 19197);
        }

        #endregion
        
        #region " Form Load Event "
       
        private void frmEDocEvent_ReceivedFax_Load(object sender, EventArgs e)
        {
           
            if (strNonXMLPath != "" && _OpenExternalSource== enum_OpenExternalSource.DirectMessage)
            {
                Direct_Form_Load();
            }
            else
            {
                ReceivedFax_FormLoad();
            }

           
        }

        #endregion

        #region "Patient control Events"
        void oPatientListControl_ItemClosedClick(object sender, EventArgs e)
        {
            //pnlPatients.Visible = false;
            //btnPat_Up_Click(null, null);
            btnPatUpClick();
        }
        void oPatientListControl_Grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                imageControl1.UpdateScreenOfControl(false);
                Point myPos = imageControl1.PictureScrollPos;

                _PatientID = System.Convert.ToInt64(oPatientListControl.SelectedPatientID);
                _PatientCode = oPatientListControl.PatientCode.ToString();
                _PatientName = oPatientListControl.FirstName.ToString() + " " + oPatientListControl.LastName.ToString();
                lblPatients.Text = "Selected Patient: " + _PatientName;
                try
                {
                    if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                    {
                        this.Text = "Direct Message Document";
                    }
                    else
                    {
                        this.Text = "Received Faxes";
                    }
                    
                    gloPatient.gloPatient.GetWindowTitle(this, _PatientID, _databaseconnectionstring, gloEDocV3Admin.gMessageBoxCaption);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                {
                    btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.DownHover;
                    btnPat_Down.BackgroundImageLayout = ImageLayout.Center;
                }
                
                //btnPat_Up_Click(null, null);
                btnPatUpClick();

                if (_PatientID > 0)
                {
                    if (this.MdiParent != null)
                    {
                        if (_OpenExternalSource != enum_OpenExternalSource.DirectMessage)
                        {
                            EvnActivateReciveFax(_PatientID);
                        }
                    }
                }

                imageControl1.PictureScrollPos = myPos;
                imageControl1.UpdateScreenOfControl(true);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion

        #region " Patient List Control Events "

        DataTable GetPatientId(Int64 mobileno)
        {

            gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
            DataTable oDataTable = new DataTable();
            string _strSQL = "";

            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT PatientId FROM Patient WHERE sMobile=" + mobileno;
                oDB.Retrive_Query(_strSQL, out oDataTable);

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                _ErrorMessage = ex.Message;


                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oDataTable;
        }


        private string GetRowFilter(string RowFilter)
        {
            if (RowFilter == "")
            {
                return RowFilter;
            }
            else
            {
                return RowFilter + " AND ";
            }
        }

        #endregion

        #region " Tool Bar Button Events "
        private void ReceivedFax_FormLoad()
        {
            gloUserControlLibrary.gloC1FlexStyle.Style(c1Documents, false);


            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;

            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;

            btn_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btn_Up.BackgroundImageLayout = ImageLayout.Center;

            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;

            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browsePlus;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;

            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browseMinus;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;

            oToolTip.SetToolTip(btn_Expand, "Expand All");
            oToolTip.SetToolTip(btn_Collapse, "Collapse All");
            //imagXpress1.Licensing.UnlockRuntime(1908208815, 373700144, 1341181380, 19197);
            try
            {
                #region " Designer Code "
                //Set the form load event true
                IsformLoad = true;

                tlb_Ok.Visible = true;
                tlb_Cancel.Visible = true;
                tlb_Remove.Visible = true;
                tlb_RotateForward.Visible = true;
                tlb_RotateBack.Visible = true;
                tlb_SendToRCM.Visible = true;

                pnlSetting.Visible = false;
                pnlScanDocument.Visible = true;
                pnlSmallStripMain.Visible = true;
                pnlSmallStrip.Visible = false;
                pnlDocumentNameAcquiredImages.Visible = true;
                pnlProgressBar.Visible = false;
                ShowHideControl(ShowHideFlag.Patient, true);

                btn_Down.Visible = false;
                //btn_Collapse.Visible = false;
                //Shubhangi
                btn_Collapse.Visible = true;
                btn_Expand.Visible = false;

                #endregion

                //.Get the number of file load count
                GetFileLoadCount();

                #region ".Load Patient List Control "

                oPatientListControl = new gloPatient.PatientListControl();

                //oPatientListControl.ClinicID = _SetAppointmentParameter.ClinicID;
                oPatientListControl.DatabaseConnection = _databaseconnectionstring;
                oPatientListControl.FillPatients();

                pnlPatients.Controls.Add(oPatientListControl);
                oPatientListControl.Dock = DockStyle.Fill;
                oPatientListControl.BringToFront();
                oPatientListControl.Visible = true;
                oPatientListControl.ShowOKCancel(true);                
                oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);


                #endregion ".Load Patient List Control "

                //btnPat_Up_Click(null, null);

                btnPatUpClick();


                txtWait.Visible = true;
                timer1.Enabled = true;
                //Shubhangi set the Laod event variable
                IsformLoad = false;
                pnlScanDocument.Controls.Remove(pnlPreviewDMSDoc);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                //IsformLoad = false;


            }
        }
        private void Direct_Form_Load()
        {
            this.Text = "Direct Message Document";

           
            try
            {

                
            pnlToolstrip.Visible = false;
            #region " Designer Code "
            //Set the form load event true
            IsformLoad = true;
            tlb_LoadImages.Visible = false;
            tlb_LoadMoreFaxFiles.Visible = false;
            tlb_Print.Visible = false;
            tlb_ExportToPDF.Visible = false;
            tlb_Ok.Visible = true;
            tlb_Cancel.Visible = true;
            tlb_Remove.Visible = false;
            tlb_RotateForward.Visible = false;
            tlb_RotateBack.Visible = false;
            tlb_SendToRCM.Visible = false;

            pnlSetting.Visible = false;
            pnlScanDocument.Visible = true;
            pnlSmallStripMain.Visible = false;
            pnlSmallStrip.Visible = false;
            pnlDocumentNameAcquiredImages.Visible = false;
            pnlProgressBar.Visible = false;
            ShowHideControl(ShowHideFlag.Patient, true);

            btn_Down.Visible = false;

            btn_Collapse.Visible = true;
            btn_Expand.Visible = false;

            #endregion
           Application.DoEvents();

            #region "Wait Process"
            if (oProcessLabel != null)
            {
                if (this.Controls.Contains(oProcessLabel) == true) { this.Controls.Remove(oProcessLabel); }
                oProcessLabel.Dispose(); oProcessLabel = null;
            }
            oProcessLabel = new Label();
            this.Controls.Add(oProcessLabel);
            oProcessLabel.Dock = DockStyle.Fill;
            oProcessLabel.Location = new System.Drawing.Point(0, 0);
            oProcessLabel.ForeColor = Color.Blue;
            this.BackColor = Color.White;
            oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
            oProcessLabel.Text = "Please wait !!!";
            oProcessLabel.Name = "lblProcess";
            oProcessLabel.Visible = true;
           
            oProcessLabel.BringToFront();
            #endregion
                btnPat_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
                btnPat_Up.BackgroundImageLayout = ImageLayout.Center;
           Application.DoEvents();
          
                #region ".Load Patient List Control "

                oPatientListControl = new gloPatient.PatientListControl();

                oPatientListControl.DatabaseConnection = _databaseconnectionstring;
                   
                //btnPat_Down_Click(null, null);

                btnPatDownClick();

               
               
                pnlPatients.Controls.Add(oPatientListControl);
                oPatientListControl.Dock = DockStyle.Fill;
                oPatientListControl.BringToFront();
                oPatientListControl.Visible = true;
                oPatientListControl.ShowOKCancel(true);                
                oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);                
                oPatientListControl.FillPatients();
                if (DMSPatientID != 0)
                {
                   
                    SelectPatientInList();
                }
                    _PatientID = System.Convert.ToInt64(oPatientListControl.SelectedPatientID);
                
                _PatientCode = oPatientListControl.PatientCode.ToString();
                _PatientName = oPatientListControl.FirstName.ToString() + " " + oPatientListControl.LastName.ToString();
                lblPatients.Text = "Selected Patient: " + _PatientName;
                try
                {
                    if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                    {
                        this.Text = "Direct Message Document";
                    }
                   

                    gloPatient.gloPatient.GetWindowTitle(this, _PatientID, _databaseconnectionstring, gloEDocV3Admin.gMessageBoxCaption);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                #endregion ".Load Patient List Control "

              


                FileInfo ofileinfo = new FileInfo(_strNonXMLPath);
                if (ofileinfo.Extension.ToUpper() == ".PDF")
                {
                    ShowPDFPreview();

                }
                else
                {
                    string _strPDFFilePath = "";
                    _strPDFFilePath = gloWord.gloWord.ConvertFileToPDF(_strNonXMLPath, gloSettings.FolderSettings.AppTempFolderPath);
                    _strNonXMLPath = _strPDFFilePath;
                    ShowPDFPreview();

                }
                ofileinfo = null;
                IsformLoad = false;
               
              

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                if (oProcessLabel != null)
                {
                    if (this.Controls.Contains(oProcessLabel) == true) { this.Controls.Remove(oProcessLabel); }
                    oProcessLabel.Dispose(); oProcessLabel = null;
                }
                pnlToolstrip.Visible = true;
                this.BackColor = System.Drawing.Color.FromArgb(207, 224, 248);
                //IsformLoad = false;


            }

        }
        private void SelectPatientInList()
        {
            bool _isPatientinList = false;
            _isPatientinList = oPatientListControl.IsPatientInList(DMSPatientID );
            if (_isPatientinList == true)
            {
                oPatientListControl.SelectPatient(DMSPatientID);
            }
            else if (oPatientListControl.ProviderID==0)
            {
                oPatientListControl.AddPatientInList(DMSPatientID);
            }

        }
        private void ShowPDFPreview()
        {

            pnlScanDocument.Controls.Remove(pnlViewImage );
            lblPreviewStatus.Text = "";

            if (pnlPreviewDMSDoc.Controls.Contains(oPDFView))
                {
                    pnlPreviewDMSDoc.Controls.Add(oPDFView);
                }
                btnPrevious.Enabled = false ;
                btnFirst.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                pnlPreviewCommand.Visible = true;
              //  nPageNo = 1;

                if (oPDFView == null)
                {
                    //oPDFView = new pdftron.PDF.PDFViewCtrl();
                    oPDFView = new pdftron.PDF.PDFViewCtrl();
                }
            pdftron.PDF.PDFDoc oldDoc = oPDFView.GetDoc();
           
            oPDFDoc = new pdftron.PDF.PDFDoc(strNonXMLPath);
            if (oPDFView == null)
            {
                oPDFView = new pdftron.PDF.PDFViewCtrl();
            }
            oPDFView.Show();
            oPDFView.SetDoc(oPDFDoc);
            if (oldDoc != null)
            {
                oldDoc.Dispose();
                oldDoc = null;
            }
            pnlPreviewDMSDoc .Controls.Add(oPDFView);
            oPDFView.Dock = DockStyle.Fill;
            oPDFView.BringToFront();
            oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page);
            oPDFView.SetCaching(true);
            oPDFView.SetProgressiveRendering(true);
            oPDFView.Visible = true;
            oPDFView.Refresh();
            oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page);
            oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width);
              string  Percentage= "100%";
              oPDFView.SetZoom(System.Convert.ToDouble(Percentage.Substring(0, Percentage.Length - 1).ToString()) / 100);

            if (oPDFView.GotoFirstPage() ==true )
            {
                oPDFView.GetSelectionBeginPage();
            }
             lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage() + " of " + oPDFView.GetPageCount();
        btnPrevious.Enabled = false;
        btnFirst.Enabled = false;
        if( oPDFView.GetPageCount() > 1)
        {
            btnNext.Enabled = true;
            btnLast.Enabled = true;
        }
        else

        {
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
            oPDFView.EnableInteractiveForms(false);
        }
        private bool SendtoDMSFromDirect()
        {
            bool _Result = false;
            try
            {
               
                _SelectedDocuments.Clear();
                //Check if Patient is selected or not
                if (_PatientID == 0)
                {
                    MessageBox.Show("Select Patient", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //btnPat_Down_Click(null, null);

                    btnPatDownClick();

                }
                else
                {
                    
                    _SelectedDocuments.Add(_strNonXMLPath);
                    if (_SelectedDocuments.Count > 0)
                    {
                       //  DialogResult  oDialogResultIsOK;
                        frmEDocEvent_SendRecFax ofrmSendRexFax = new frmEDocEvent_SendRecFax();
                        ofrmSendRexFax.PatientID = this._PatientID;
                        ofrmSendRexFax.oSelectedDocuments = this._SelectedDocuments;
                        ofrmSendRexFax.strNonXML = _strNonXMLPath;
                        ofrmSendRexFax._OpenExternalSource = _OpenExternalSource;
                      ofrmSendRexFax.ShowDialog(this);
                        if (ofrmSendRexFax.SendResult)
                        {

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Direct Message attachment document moved to DMS", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                            this.Close();
                        }


                        if (ofrmSendRexFax != null)
                            ofrmSendRexFax.Dispose();
                    }

                    _Result = true;

                }


            }
            catch (Exception ex)
            {
                #region " Make Log Entry "
                
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Direct Message attachment document moved to DMS", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure);

                _ErrorMessage = ex.ToString();
             
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

            }
            finally
            {

            }

            return _Result;
        }
        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (_strNonXMLPath != "" && _OpenExternalSource == enum_OpenExternalSource.DirectMessage)
            {
                if (_OpenExternalSource == enum_OpenExternalSource.DirectMessage)
                {
                    _PatientID = System.Convert.ToInt64(oPatientListControl.PatientID);
                    _PatientCode = oPatientListControl.PatientCode.ToString();
                    _PatientName = oPatientListControl.FirstName.ToString() + " " + oPatientListControl.LastName.ToString();
                    lblPatients.Text = "Selected Patient: " + _PatientName;
                    try
                    {


                        this.Text = "Direct Message Document";


                        gloPatient.gloPatient.GetWindowTitle(this, _PatientID, _databaseconnectionstring, gloEDocV3Admin.gMessageBoxCaption);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    }
                }
                SendtoDMSFromDirect();
                return;
            }
            else
            {
                if (SendToDMS())
                {
                    ShowFaxFilesDetails();

                    return;
                }
            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_LoadImages_Click(object sender, EventArgs e)
        {
            //if (imageXView.Image != null)
            //{
            //    imageXView.Image.Dispose();
            //    imageXView.Image = null;
            //}
            //if (imageControl1 != null)
            //{
            //    imageControl1.Dispose();
            //    imageControl1 = null;
            //}
            //c1Documents.Visible = false;
            if (_ReceivedFaxFilesFolderPath != null)
            {
                //Check if Received Fax Files Folder Path is empty
                if (_ReceivedFaxFilesFolderPath != "")
                {
                    //Check for the Folder exists
                    if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)
                    {
                        bool _IsStructureCreated = CreateFolderStructure();
                        if (_IsStructureCreated)
                        {
                            FillGrid();
                        }
                        c1Documents.Visible = true;

                        if (IsformLoad == false)
                        {
                            if (btn_Collapse.Visible == true)
                            {
                                btn_Collapse.Visible = false;
                                btn_Expand.Visible = true;
                            }
                        }

                        ShowFaxFilesDetails();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find received Fax files folder", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

        }

        private void tlb_LoadMoreFaxFiles_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDocumentNameAcquiredImages.Enabled = false;
                tls_MaintainDoc.Enabled = false;
                pnlPatients.Enabled = false;
                panel4.Enabled = false;
                cmbZoomPercentage.Enabled = false;
                txtWait.Visible = true;
                txtWait.BringToFront();

                //imageXView.SendToBack();
                //imageXView.Image = null;

                imageControl1.SendToBack();
                imageControl1.CloseCurrentImage();
                imageControl1.UnloadDisplayImage(); 

                imageControl1.CurrImage = null;

                bool retValue = LoadMoreFaxFiles();
                if (retValue)
                {
                    tlb_LoadImages_Click(null, null);
                    if (btn_Collapse.Visible == true)
                    {
                        btn_Collapse.Visible = false;
                        btn_Expand.Visible = true;
                    }
                }

                pnlDocumentNameAcquiredImages.Enabled = true;
                tls_MaintainDoc.Enabled = true;
                pnlPatients.Enabled = true;
                panel4.Enabled = true;
                cmbZoomPercentage.Enabled = true;
                txtWait.Visible = false;

                //imageXView.BringToFront();
                imageControl1.BringToFront();
                c1Documents_RowColChange(null, null);
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


            }
            finally
            {

            }
        }

        private void tlb_Remove_Click(object sender, EventArgs e)
        {
            //commented by Sandip Darade 20090730
            //added messegebox prior to deleting document
            if (c1Documents.Rows.Count > 1)
            {
                //Commented by Mayuri:20091010
                //To show message only if Node to delete is checked
                bool _isselect = false;
                for (int i = c1Documents.Rows.Count - 1; i > 0; i--)
                {
                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                    {
                        _isselect = true;
                        break;
                    }
                }
                //end Code by Mayuri:20091010
                if (_isselect == true)
                {
                    if (MessageBox.Show("Are you sure you want to delete selected document(s)? ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int folderSelected = 0;
                        int filesSelected = 0;

                        for (int i = c1Documents.Rows.Count - 1; i > 0; i--)
                        {
                            if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                            {
                                Node oNode = c1Documents.Rows[i].Node;
                                if (oNode != null)
                                {
                                    if (oNode.Level == 1)
                                    {
                                        folderSelected += 1;
                                    }
                                    else if (oNode.Level == 2)
                                    {
                                        filesSelected += 1;
                                    }
                                }
                                if (oNode != null) { oNode = null; }
                            }
                        }

                        if (MessageBox.Show("You are about to delete " + System.Convert.ToString(folderSelected) + " Faxes and " + System.Convert.ToString(filesSelected) + " Pages "+ Environment.NewLine + "Do you want to continue ?", gloEDocV3Admin.gMessageBoxCaption + " - Critical Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (Remove())
                            {
                                return;
                            }
                            else
                            {
                                return;
                            }

                        }
                    }
                    //}
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Select document(s) to delete", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            else
            {
                return;
            }

        }

        #endregion

        #region " Form Button Click Events "

        private void btn_Left_Click(object sender, EventArgs e)
        {
            //pnlSmallStripMain.Visible = true;
            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;

            pnlSmallStrip.Visible = true;
            splitter1.Visible = false;
            pnlDocumentNameAcquiredImages.Visible = false;

            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);

        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;

            //pnlSmallStripMain.Visible = false;
            pnlSmallStrip.Visible = false;
            splitter1.Visible = true;
            pnlDocumentNameAcquiredImages.Visible = true;

            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);

        }

        private void btnPatUpClick()
        {
            pnlPatients.Height = 26;
            pnlPatients.Visible = false;
            btnPat_Up.Visible = false;
            btnPat_Down.Visible = true;
        }

        private void btnPat_Up_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;
            btnPatUpClick();
            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);
        }


        private void btnPatDownClick()
        {
            pnlPatients.Height = 200;
            pnlPatients.Visible = true;
            btnPat_Up.Visible = true;
            btnPat_Down.Visible = false;
            LoadPatientListControl();
        }

        private void btnPat_Down_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;
            btnPatDownClick();
            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);
        }

        #endregion

        #region " Tool Strip ItemClick Event "

        private void ts_SmallStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            btn_Right_Click(null, null);
        }

        #endregion

        #region " c1 Grid Events "

        private void c1Documents_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Documents != null && c1Documents.Rows.Count > 2)
                {
                    if (_IsLoading == false)
                    {
                        if (c1Documents.Rows.Count == 0)
                        {
                            //imageXView.Image = null;
                            return;
                        }

                        //If Root Node is clicked select all nodes below.
                        if (c1Documents.GetCellCheck(c1Documents.RowSel, COL_DOC_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            CellRange rng = c1Documents.Rows[c1Documents.RowSel].Node.GetCellRange();
                            for (int i = rng.TopRow + 1; i <= rng.BottomRow; i++)
                            {
                                c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Checked);
                            }
                        }

                        //If Root Node is clicked(unchecked)deselect all nodes below.
                        if (c1Documents.GetCellCheck(c1Documents.RowSel, COL_DOC_SELECT) == CheckEnum.Unchecked)
                        {
                            CellRange rng = c1Documents.Rows[c1Documents.RowSel].Node.GetCellRange();
                            for (int i = rng.TopRow + 1; i <= rng.BottomRow; i++)
                            {
                                c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                            }
                        }

                        //If ParentNode is Checked(ie all child nodes are selected) & any of the child node
                        //is unchecked then uncheck the ParentNode.
                        if (c1Documents.GetCellCheck(c1Documents.RowSel, COL_DOC_SELECT) == CheckEnum.Unchecked)
                        {
                            Node oParent = c1Documents.Rows[c1Documents.RowSel].Node;

                            if (oParent.Level == 0)
                            {
                                CellRange rng = oParent.GetCellRange();
                                c1Documents.SetCellCheck(rng.r1, COL_DOC_SELECT, CheckEnum.Unchecked);
                            }
                            else if (oParent.Level == 1)
                            {

                                Node oNode = c1Documents.Rows[c1Documents.RowSel].Node.GetNode(NodeTypeEnum.Parent);
                                CellRange rng = oNode.GetCellRange();
                                c1Documents.SetCellCheck(rng.r1, COL_DOC_SELECT, CheckEnum.Unchecked);
                                oNode = null;
                            }
                            if (oParent.Level == 2)
                            {
                                Node oChildNode = c1Documents.Rows[c1Documents.RowSel].Node.GetNode(NodeTypeEnum.Parent);
                                CellRange rng = oChildNode.GetCellRange();
                                c1Documents.SetCellCheck(rng.r1, COL_DOC_SELECT, CheckEnum.Unchecked);
                                oChildNode = null;

                                //also uncheck the root node
                                Node oRootNode = c1Documents.Rows[c1Documents.RowSel].Node.GetNode(NodeTypeEnum.Parent).GetNode(NodeTypeEnum.Parent);
                                CellRange rngRoot = oRootNode.GetCellRange();
                                c1Documents.SetCellCheck(rngRoot.r1, COL_DOC_SELECT, CheckEnum.Unchecked);
                                oRootNode = null;
                            }
                        }

                        if (c1Documents.GetCellCheck(c1Documents.RowSel, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            Node oNode = c1Documents.Rows[c1Documents.RowSel].Node;
                            if (oNode.Level != 0)
                            {
                                Node ParentNode = oNode.GetNode(NodeTypeEnum.Parent);
                                CellRange rng = ParentNode.GetCellRange();
                                bool flag = true;
                                for (int i = rng.TopRow + 1; i <= rng.BottomRow; i++)
                                {
                                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Unchecked)
                                    {
                                        flag = false;
                                    }

                                }
                                if (flag)
                                {
                                    c1Documents.SetCellCheck(rng.TopRow, COL_DOC_SELECT, CheckEnum.Checked);

                                    if (ParentNode.Level == 1)
                                    {
                                        Node RootNode = ParentNode.GetNode(NodeTypeEnum.Parent);
                                        CellRange rng1 = RootNode.GetCellRange();
                                     //   bool flg = true;
                                        for (int i = rng1.TopRow + 1; i <= rng1.BottomRow; i++)
                                        {
                                            if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Unchecked)
                                            {
                                                flag = false;
                                            }

                                        }
                                        if (flag)
                                        {
                                            c1Documents.SetCellCheck(rng1.TopRow, COL_DOC_SELECT, CheckEnum.Checked);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void c1Documents_RowColChange(object sender, EventArgs e)
        {

            try
            {
                if (_IsLoading == false)
                {
                    Node oNode = c1Documents.Rows[c1Documents.RowSel].Node;
                    if (oNode.Level == 2)
                    {
                        string _strFilePath = c1Documents.GetData(c1Documents.RowSel, COL_DOC_HiddenPath).ToString();
                        int _pageNo = System.Convert.ToInt32(c1Documents.GetData(c1Documents.RowSel, COL_DOC_HiddenPageNo).ToString());
                        int oldSelectedIndex = cmbZoomPercentage.SelectedIndex;

                        imageControl1.UpdateScreenOfControl(false);

                        if (oldSelectedIndex >= 0)
                        {
                            LoadImage(_strFilePath, oldSelectedIndex);
                        }
                        else
                        {
                            LoadImage(_strFilePath);
                            if (imageControl1._CurrZoomIndex >= 0)
                            {
                                cmbZoomPercentage.SelectedIndex = imageControl1._CurrZoomIndex;
                            }
                        }
                        //Zoom();
                        //cmbZoomPercentage

                        imageControl1.ZoomValueChanged((ComboBox)cmbZoomPercentage);
                       // imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;
                        if ((oldSelectedIndex >= 0) && (oldSelectedIndex != cmbZoomPercentage.SelectedIndex))
                        {
                            cmbZoomPercentage.SelectedIndex = oldSelectedIndex;
                            imageControl1.ZoomValueChanged((ComboBox)cmbZoomPercentage);
                            imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;
                        }
                        imageControl1.UpdateScreenOfControl( true);

                    }
                    else
                    {
                        //imageXView.Image = null;
                        imageControl1.CloseCurrentImage();
                        imageControl1.UnloadDisplayImage();

                        imageControl1.CurrImage = null;

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {

            }
        }

        private void c1Documents_AfterCollapse(object sender, RowColEventArgs e)
        {
            try
            {


                if (_IsLoading == false)
                {
                    bool _Expanded = true;

                    if (c1Documents != null && c1Documents.Rows.Count > 0)
                    {

                        if (c1Documents.Rows[e.Row].Node.Level == 0)
                        {
                            if (c1Documents.Rows[e.Row].Node.Expanded == true)
                            {
                                btn_Expand.Visible = true;
                                btn_Collapse.Visible = false;
                            }
                            else
                            {
                                btn_Expand.Visible = false;
                                btn_Collapse.Visible = true;
                                return;
                            }

                        }


                        for (int i = 0; i < c1Documents.Rows.Count; i++)
                        {
                            if (c1Documents.Rows[i].Node.Level == 1)
                            {
                                if (c1Documents.Rows[i].Node.Expanded == false)
                                {
                                    _Expanded = false;
                                    break;
                                }
                            }
                        }

                        if (_Expanded)
                        {
                            btn_Expand.Visible = true;
                            btn_Collapse.Visible = false;
                        }
                        else
                        {
                            btn_Expand.Visible = false;
                            btn_Collapse.Visible = true;
                        }
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region " Private & Public Methods "

        private string getFileName(string _filePath)
        {
            FileInfo fInfo = null;
            try
            {
                fInfo = new FileInfo(_filePath);
                return (fInfo.Name);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            finally
            {
                fInfo = null;
            }
        }

        private int getImagePageCount(string _filePath)
        {
            System.Drawing.Bitmap bmp = null;
            int imgCount = 0;
            try
            {
                bmp = new System.Drawing.Bitmap(_filePath);
                imgCount = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
                return imgCount;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                return 0;
            }
            finally
            {
                if (bmp != null)
                    bmp.Dispose();

            }
        }

        private void ShowHideControl(ShowHideFlag flag, bool show)
        {
            switch (flag)
            {

                case ShowHideFlag.Patient:
                    {
                        pnlPatients.Visible = show;
                    }
                    break;

            }
        }

        private void saveFiles()
        {
            try
            {
                for (int i = 0; i < c1Documents.Rows.Count; i++)
                {
                    Node oNode = c1Documents.Rows[i].Node;

                    if (oNode.Level == 0)
                    {
                        //Check If Parent Node is checked
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            //as all files are imported delete all code goes here
                            string _faxFolderPath = c1Documents.GetData(i, COL_DOC_PATH).ToString();
                            if (!gloGlobal.clsFileExtensions.IsSystemOrRootDir(_faxFolderPath))
                            {
                                try
                                {
                                    DirectoryInfo dirInfo = new DirectoryInfo(_faxFolderPath);
                                    dirInfo.Delete(true);
                                }
                                catch
                                {
                                }
                            }
                            break;

                        }

                    }
                    else if (oNode.Level == 1)
                    {
                        string _filePath = "";
                        CellRange rng;
                        //if particular document is checked delete that doc.
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            _filePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                            FileInfo fInfo = new FileInfo(_filePath);
                            fInfo.Delete();
                            rng = c1Documents.Rows[i].Node.GetCellRange();
                            i = i + rng.BottomRow;
                        }
                        else
                        {
                            _filePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                            rng = c1Documents.Rows[i].Node.GetCellRange();
                            ArrayList arrCheckedPages = new ArrayList();
                            for (int j = rng.TopRow + 1; j <= rng.BottomRow; j++)
                            {
                                if (c1Documents.GetCellCheck(j, COL_DOC_SELECT) == CheckEnum.Checked)
                                {
                                    arrCheckedPages.Add(c1Documents.GetData(j, (COL_DOC_HiddenPageNo - 1)).ToString());
                                }

                            }
                            if (arrCheckedPages.Count > 0)
                            {
                                deletePages(c1Documents.GetData(i, COL_DOC_HiddenPath).ToString(), arrCheckedPages);
                            }

                        }

                    }
                    else if (oNode.Level == 2)
                    {
                        string _filePath = "";
                        CellRange rng;
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            _filePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                            rng = c1Documents.Rows[i].Node.GetNode(NodeTypeEnum.Parent).GetCellRange();
                            ArrayList arrCheckedPages = new ArrayList();
                            for (int j = rng.TopRow + 1; j <= rng.BottomRow; j++)
                            {
                                if (c1Documents.GetCellCheck(j, COL_DOC_SELECT) == CheckEnum.Checked)
                                {
                                    arrCheckedPages.Add(c1Documents.GetData(j, COL_DOC_HiddenPageNo).ToString());
                                }

                            }
                            if (arrCheckedPages.Count > 0)
                            {
                                deletePages(c1Documents.GetData(i, COL_DOC_HiddenPath).ToString(), arrCheckedPages);
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {

            }
        }

        private void deletePages(string fileName, ArrayList PagestoDelete)
        {
            System.Drawing.Bitmap bmp = null;
            System.Drawing.Image img;
            System.Drawing.Bitmap newbmp = new System.Drawing.Bitmap(fileName);

            try
            {
                bmp = new System.Drawing.Bitmap(fileName);
                //SLR: Some logic problem in forward deletion: Please verify. 5/19/2014
                for (int i = 0; i < PagestoDelete.Count; i++)
                {
                    if (System.Convert.ToInt32(PagestoDelete[i]) != i)
                    {

                        int Id = bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, (System.Convert.ToInt32(PagestoDelete[i]) - 1));
                        img = bmp.GetThumbnailImage(bmp.Width, bmp.Height, null, IntPtr.Zero);
                        //img.SaveAdd(img,null);
                        System.Drawing.Imaging.EncoderParameter oenPara = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, 1);
                        System.Drawing.Imaging.EncoderParameters oenParas = new System.Drawing.Imaging.EncoderParameters(1);
                        oenParas.Param.Initialize();// oenPara;
                        oenParas.Param.SetValue(oenPara, 0);
                        newbmp.SaveAdd(img, oenParas);

                    }
                }
                newbmp.Save(gloSettings.FolderSettings.AppTempFolderPath + "fax.tiff", System.Drawing.Imaging.ImageFormat.Tiff);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {

            }
        }

        private bool loadReceivedFaxFiles()
        {
            bool _result = false;
            try
            {
                if (_ReceivedFaxFilesFolderPath == "" || _ReceivedFaxFilesFolderPath == null)
                {
                    MessageBox.Show("Received Fax folder path cannot be Empty", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return _result;
                }
                if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)
                {
                    C1.Win.C1FlexGrid.Node oMainNode;
                    C1.Win.C1FlexGrid.Node oFileNode;

                    string[] arrFilesPath = Directory.GetFiles(_ReceivedFaxFilesFolderPath, "*.tif");
                    if (arrFilesPath != null)
                    {
                        if (arrFilesPath.Length > 0)
                        {

                            c1Documents.Rows.Add();
                            c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                            c1Documents.Rows[c1Documents.Rows.Count - 1].ImageAndText = true;//true;
                            c1Documents.Rows[c1Documents.Rows.Count - 1].Height = 22;
                            c1Documents.Rows[c1Documents.Rows.Count - 1].IsNode = true;
                            //c1Documents.Rows[c1Documents.Rows.Count - 1].Style = c1Documents.Styles["CS_Folder"];
                            c1Documents.Rows[c1Documents.Rows.Count - 1].Node.Level = 0;
                            c1Documents.Rows[c1Documents.Rows.Count - 1].Node.Data = "Received Fax";// _ReceivedFaxFilesFolderPath.ToString() ;

                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_PATH, _ReceivedFaxFilesFolderPath);
                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.Folder);

                            oMainNode = c1Documents.Rows[c1Documents.Rows.Count - 1].Node;//.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild);

                            for (int i = 0; i < arrFilesPath.Length; i++)
                            {
                                //Add Files
                                //c1Documents.Rows.Add();
                                int pageCount = getImagePageCount(arrFilesPath[i].ToString());

                                if (pageCount > 0)
                                {
                                    oMainNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, getFileName(arrFilesPath[i]).ToString());
                                    c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_PATH, arrFilesPath[i].ToString());
                                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.File);
                                    c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPath, arrFilesPath[i].ToString());

                                    //Add Pages
                                    oFileNode = oMainNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild);

                                    for (int j = 0; j < pageCount; j++)
                                    {
                                        //c1Documents.Rows.Add();
                                        oFileNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Page " + (j + 1).ToString());
                                        c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.Page);
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPath, arrFilesPath[i].ToString());
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPageNo, (j + 1));

                                    }
                                }

                            }

                        }
                        else
                        {
                            return _result;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid path or folder is Empty", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return _result;
                    }
                }///
                else
                {
                    MessageBox.Show("Invalid path or folder does not Exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return _result;
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return _result;
            }
            finally
            {

            }
            return _result;
        }

        private void designC1Documents()
        {
            try
            {
                _IsLoading = true;
                c1Documents.ExtendLastCol = false;

                c1Documents.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
                c1Documents.Rows.Count = 0;
                c1Documents.Rows.Fixed = 0;
                c1Documents.Cols.Count = COL_DOC_COUNT;
                c1Documents.Cols.Fixed = 0;

                c1Documents.Cols[COL_DOC_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                c1Documents.Cols[COL_DOC_NAME].AllowEditing = false;
                c1Documents.Cols[COL_DOC_SELECT].AllowEditing = true;
                c1Documents.Cols[COL_DOC_PATH].AllowEditing = false;
                c1Documents.Cols[COL_DOC_TYPE].AllowEditing = false;
                c1Documents.Cols[COL_DOC_HiddenPath].AllowEditing = false;
                c1Documents.Cols[COL_DOC_HiddenPageNo].AllowEditing = false;
                c1Documents.Cols[COCL_DOC_HiddenDocumentName].AllowEditing = false;

                c1Documents.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes;

                c1Documents.Cols[COL_DOC_SELECT].Width = 17;
                c1Documents.Cols[COL_DOC_NAME].Width = (c1Documents.Width - 35);
                c1Documents.Cols[COL_DOC_PATH].Width = 0;
                c1Documents.Cols[COL_DOC_TYPE].Width = 0;
                c1Documents.Cols[COL_DOC_HiddenPath].Width = 0;
                c1Documents.Cols[COL_DOC_HiddenPageNo].Width = 0;
                c1Documents.Cols[COCL_DOC_HiddenDocumentName].Width = 0;

                c1Documents.Cols[COL_DOC_NAME].Visible = true;
                c1Documents.Cols[COL_DOC_SELECT].Visible = true;
                c1Documents.Cols[COL_DOC_PATH].Visible = false;
                c1Documents.Cols[COL_DOC_TYPE].Visible = false;
                c1Documents.Cols[COL_DOC_HiddenPath].Visible = false;
                c1Documents.Cols[COL_DOC_HiddenPageNo].Visible = false;
                c1Documents.Cols[COCL_DOC_HiddenDocumentName].Visible = false;

                c1Documents.Tree.Column = COL_DOC_NAME;
                c1Documents.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                _IsLoading = false;
            }
        }

        private void GetFileLoadCount()
        {
            Database.DBLayer oDB = new Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            string _sqlQuery = "";
            Object retValue = null;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select sSettingsValue from Settings where sSettingsName = 'Load Number Of Faxes'";
                retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retValue != null && System.Convert.ToString(retValue) != "" && retValue != DBNull.Value && System.Convert.ToInt32(retValue) > 0)
                {
                    if (System.Convert.ToInt32(retValue) < 10)
                    {
                        FileLoadCount = 10;
                    }
                    else
                    {
                        FileLoadCount = System.Convert.ToInt32(retValue);
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (retValue != null) { retValue = null; }
            }

        }

        #endregion

        #region " Rotation Events "

        private void tlb_RotateForward_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);

            Rotate(true, cmbZoomPercentage.SelectedIndex);
            imageControl1.ZoomValueChanged(cmbZoomPercentage);
            imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;

            imageControl1.UpdateScreenOfControl(true);
            //Rotate(270.0);
        }

        private void tlb_RotateBack_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);

            Rotate(false, cmbZoomPercentage.SelectedIndex);
            imageControl1.ZoomValueChanged(cmbZoomPercentage);
            imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;

            imageControl1.UpdateScreenOfControl(true);
        
            //Rotate(90.0);
        }

        #endregion " Rotation Events "

        #region " Node Selection Methods "

        private bool IsMultiSelect()
        {
            bool _Result = false;

            try
            {
                for (int i = 0; i < c1Documents.Rows.Count; i++)
                {
                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                    {
                        _Result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {

            }
            return _Result;
        }

        private bool IsRootNodeSelected()
        {
            bool _Result = false;

            try
            {

                if (c1Documents.Rows.Count > 0)
                {
                    if (c1Documents.GetCellCheck(0, COL_DOC_SELECT) == CheckEnum.Checked)
                    {
                        _Result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {

            }
            return _Result;
        }

        private bool IsDocumentNodeSelected()
        {
            bool _Result = false;

            try
            {
                if (c1Documents.Rows.Count > 0)
                {
                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            Node _RootChildrenNode = c1Documents.Rows[i].Node;

                            if (_RootChildrenNode.Level == 1) //if document node
                            {
                                _Result = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Result = false;
            }
            finally
            {

            }
            return _Result;
        }

        private ArrayList GetSelectedRootChildrenNodes()
        {
            ArrayList arrSelectedNodes = new ArrayList();
            Node _RootChildrenNode = null; //doucument node
            try
            {
                if (c1Documents.Rows.Count > 0)
                {
                    arrSelectedNodes = new ArrayList();
                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            _RootChildrenNode = c1Documents.Rows[i].Node;

                            if (_RootChildrenNode.Level == 1) //if document node
                            {
                                arrSelectedNodes.Add(i);
                            }
                            _RootChildrenNode = null;
                        }
                    }
                }
                return arrSelectedNodes;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                arrSelectedNodes = null;
            }
        }

        #endregion " Node Selection Methods "

        #region " Delete Pages "

        private void RemoveSentPages()
        {
            gloEDocumentV3.SDKInteraction.eDocTIFFManager oTiffManager = null;
            try
            {
                if (c1Documents.Rows.Count == 0)
                {
                    return;
                }
                if (IsMultiSelect())
                {
                    if (IsRootNodeSelected())
                    {
                        if (!gloGlobal.clsFileExtensions.IsSystemOrRootDir(_ReceivedFaxCategory))
                        {
                            try
                            {
                                //delete all files if root node is selected
                                DirectoryInfo dirInfo = new DirectoryInfo(_ReceivedFaxFilesFolderPath);
                                dirInfo.Delete(true);
                            }
                            catch
                            {
                            }
                            try
                            {
                                System.IO.Directory.CreateDirectory(_ReceivedFaxFilesFolderPath);
                            }
                            catch
                            {
                            }
                        }

                        //also remove the nodes from the c1grid - 20080711
                        c1Documents.Clear(ClearFlags.Content);
                        //imageXView.Image = null;
                        imageControl1.CurrImage = null;

                    }
                    else if (IsDocumentNodeSelected())
                    {
                        ArrayList _SelectedRootChildrens = new ArrayList();
                        //get the selected document nodes if any
                        _SelectedRootChildrens = GetSelectedRootChildrenNodes();
                        if (_SelectedRootChildrens != null)
                        {
                            if (_SelectedRootChildrens.Count > 0)
                            {
                                for (int i = _SelectedRootChildrens.Count - 1; i >= 0; i--)
                                {
                                    string _documentName = c1Documents.GetData(System.Convert.ToInt32(_SelectedRootChildrens[i]), COL_DOC_HiddenPath).ToString();

                                    if (File.Exists(_documentName) == true)
                                    {
                                        File.Delete(_documentName);
                                        Node TempNode = c1Documents.Rows[System.Convert.ToInt32(_SelectedRootChildrens[i])].Node;
                                        TempNode.RemoveNode();
                                        TempNode = null;
                                    }
                                    else
                                    {

                                    }
                                }
                            }

                        }
                    }

                    //Pages are selected with documents or only pages are selected //20080711
                    if (IsMultiSelect()) //if only multiple pages are selected
                    {
                        for (int i = c1Documents.Rows.Count - 1; i > 0; i--)
                        {
                            if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                            {
                                string _fName = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                                Node tempNode = c1Documents.Rows[i].Node.GetNode(NodeTypeEnum.Parent);
                                CellRange rng =  tempNode.GetCellRange();
                                ArrayList arrPages = new ArrayList();
                                for (int j = rng.BottomRow; j > rng.TopRow; j--)
                                {
                                    if (c1Documents.GetCellCheck(j, COL_DOC_SELECT) == CheckEnum.Checked)
                                    {
                                        int PageNo = System.Convert.ToInt32(c1Documents.GetData(j, COL_DOC_HiddenPageNo));
                                        arrPages.Add(PageNo);
                                        PageNo = 0;
                                    }
                                }
                                oTiffManager = new SDKInteraction.eDocTIFFManager(_fName);
                                if (oTiffManager.RemoveMultiplePages(arrPages, EncoderValue.CompressionNone, _fName))
                                {
                                    //also remove the rows from the c1 grid
                                    for (int k = rng.BottomRow; k >= rng.TopRow; k--)
                                    {
                                        if (c1Documents.GetCellCheck(k, COL_DOC_SELECT) == CheckEnum.Checked)
                                        {
                                            c1Documents.RemoveItem(k);
                                        }
                                    }
                                    if (IsMultiSelect())
                                    {
                                        i = c1Documents.Rows.Count;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Error deleting Pages", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;

                                }
                            }
                        }
                    }


                }
                else
                {
                    rowIndex = c1Documents.RowSel;
                    pageFrameIndex = System.Convert.ToInt32(c1Documents.GetData(rowIndex, COL_DOC_HiddenPageNo));
                    _ImageFileName = c1Documents.GetData(rowIndex, COL_DOC_HiddenPath).ToString();

                    if (rowIndex < 0)
                    {
                        MessageBox.Show("Select page to Remove", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Node oNode = c1Documents.Rows[rowIndex].Node;
                    if (oNode != null)
                    {
                        if (oNode.Level == 1)
                        {
                            if (File.Exists(_ImageFileName) == true)
                            {
                                File.Delete(_ImageFileName);
                                tlb_LoadImages_Click(null, null);
                            }
                            else
                            {
                                //MessageBox.Show("File not exist.", gloEDocumentAdmin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }


                    if (_ImageFileName != "")
                    {
                        int FilePageCount = getImagePageCount(_ImageFileName);
                        if (FilePageCount == 1)
                        {
                            //if only 1 page is in document then remove the document
                            if (File.Exists(_ImageFileName))
                            {
                                File.Delete(_ImageFileName);
                            }
                        }
                        else
                        {
                            oTiffManager = new SDKInteraction.eDocTIFFManager(_ImageFileName);
                            pageFrameIndex = pageFrameIndex - 1;
                            oTiffManager.RemoveAPage(pageFrameIndex, EncoderValue.CompressionNone, _ImageFileName);
                        }
                    }
                }

                tlb_LoadImages_Click(null, null);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {

            }
        }

        #endregion

        #region " Patient List Control "

        private bool LoadPatientListControl()
        {
            bool _returnResult = false;

            try
            {
                oPatientListControl.FillPatients();
                
                pnlPatients.Controls.Add(oPatientListControl);
                //pnlPatients.Controls.Add(oPatientListControl);
                oPatientListControl.Dock = DockStyle.Fill;
                oPatientListControl.BringToFront();
                oPatientListControl.Visible = true;
                
                oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
                oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);

                _returnResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
            return _returnResult;
        }

        #endregion

        #region " 20080723 "

        private void ShowFaxFilesDetails()
        {
            int _fileFolderCount = 0;
            int _fileRemainingCount = 0;
            int _totalfileCount = 0;

            try
            {
                if (_ReceivedFaxFilesFolderPath != null)
                {
                    //Check if Received Fax Files Folder Path is empty
                    if (_ReceivedFaxFilesFolderPath != "")
                    {
                        //Check for the Folder exists
                        if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)
                        {

                            //Get the existing file directories within the received fax folder
                            DirectoryInfo[] _ExistingFileFolders = GetExistingFileFolders(_ReceivedFaxFilesFolderPath);
                            if (_ExistingFileFolders != null)
                            {
                                _fileFolderCount = _ExistingFileFolders.Length;
                                _ExistingFileFolders = null;
                            }

                            //Get files from the Fax folder of type tiff.
                            string[] arrFilesPath = Directory.GetFiles(_ReceivedFaxFilesFolderPath, "*.tif", SearchOption.TopDirectoryOnly);
                            if (arrFilesPath != null)
                            {
                                _fileRemainingCount = arrFilesPath.Length;
                                arrFilesPath = null;
                            }

                            _totalfileCount = _fileFolderCount + _fileRemainingCount;

                            lbl_FilesRemaining.Text = _fileRemainingCount.ToString();
                            lbl_LoadedFiles.Text = _fileFolderCount.ToString();
                            lbl_TotalFiles.Text = System.Convert.ToString(_fileRemainingCount + _fileFolderCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


            }
            finally
            {

            }
        }

        // Problem : 00000062
        // Ref : http://msdn.microsoft.com/en-us/library/bzw8611x.aspx
        // Reason : function sort the folder accroding to creationdatetime descending order.
 
        private int CompareByCreationDateTime(DirectoryInfo x, DirectoryInfo y)
        {
            return y.CreationTime.CompareTo(x.CreationTime);
        }

        private bool FillGrid()
        {
            bool _returnResult = false;
            C1.Win.C1FlexGrid.Node oMainNode = null;
            C1.Win.C1FlexGrid.Node oFileNode = null;
            DirectoryInfo[] _FileFolder = null;
            string FileFolderName = "";
            string FileFolderPath = "";
            ArrayList filesPath = null;

            try
            {
                designC1Documents();

                _FileFolder = GetExistingFileFolders(_ReceivedFaxFilesFolderPath);

                // Problem: 00000062 
                // Reason : add code to pass _FileFolder array to CompareByCreationDateTime to compare and sort folder by Creationdatetime.
                // Ref : http://msdn.microsoft.com/en-us/library/9771wchs.aspx

                Array.Sort(_FileFolder, CompareByCreationDateTime);

                if (_FileFolder != null)
                {
                    if (_FileFolder.Length > 0)
                    {
                        for (int i = 0; i < _FileFolder.Length; i++)
                        {
                            FileFolderName = _FileFolder[i].Name.ToString();
                            FileFolderPath = _FileFolder[i].FullName.ToString();
                            filesPath = GetFolderFiles(FileFolderPath);

                            if (Directory.Exists(FileFolderPath) == true)
                            {
                                if (filesPath != null)
                                {
                                    //6.Check if files are present or folder is empty
                                    if (filesPath.Count > 0)
                                    {
                                        //Add the Root Node 
                                        if (c1Documents.Rows.Count <= 0)
                                        {
                                            c1Documents.Rows.Add();
                                            c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            c1Documents.Rows[c1Documents.Rows.Count - 1].ImageAndText = true;//true;
                                            c1Documents.Rows[c1Documents.Rows.Count - 1].Height = 22;
                                            c1Documents.Rows[c1Documents.Rows.Count - 1].IsNode = true;
                                            c1Documents.Rows[c1Documents.Rows.Count - 1].Node.Level = 0;
                                            c1Documents.Rows[c1Documents.Rows.Count - 1].Node.Data = "Received Fax";
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_PATH, _ReceivedFaxFilesFolderPath);
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.Folder);
                                        }

                                        oMainNode = c1Documents.Rows[0].Node;

                                        //Add filefolder Nodes
                                        oMainNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, FileFolderName);
                                        c1Documents.Rows[c1Documents.Rows.Count - 1].TextAlign = TextAlignEnum.LeftCenter;
                                        c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_PATH, FileFolderPath);
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.Folder);
                                        c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPath, FileFolderPath);

                                        oFileNode = oMainNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild);

                                        for (int j = 0; j < filesPath.Count; j++)
                                        {
                                            oFileNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Page " + (j + 1).ToString());
                                            c1Documents.SetCellCheck(c1Documents.Rows.Count - 1, COL_DOC_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_TYPE, Col_Type.Page);
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPath, filesPath[j].ToString());
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COL_DOC_HiddenPageNo, (j + 1));
                                            c1Documents.SetData(c1Documents.Rows.Count - 1, COCL_DOC_HiddenDocumentName, FileFolderName);

                                        }

                                    }
                                }

                            }

                            if (filesPath != null)
                                filesPath = null;

                        }//end  - for 
                    }
                    else
                    {
                        MessageBox.Show("No received Fax files", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _returnResult = false;
                    }
                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _returnResult = false;

            }
            finally
            {
                if (oMainNode != null)
                    oMainNode = null;

                if (oFileNode != null)
                    oFileNode = null;
            }

            return _returnResult;
        }

        private bool CreateFolderStructure()
        {
            bool _Result = false;
        //    string _FileFolderName = "";
            gloEDocumentV3.SDKInteraction.eDocTIFFManager TiffManager = null;

            try
            {
                //Check if Received Fax Files Folder Path is null
                if (_ReceivedFaxFilesFolderPath != null)
                {
                    //Check if Received Fax Files Folder Path is empty
                    if (_ReceivedFaxFilesFolderPath != "")
                    {
                        //Check for the Folder exists
                        if (gloGlobal.clsFileExtensions.IsSystemOrRootDir(_ReceivedFaxFilesFolderPath))
                        {
                            string strMessageToShow = "Access to the path " + _ReceivedFaxFilesFolderPath + " is denied." + Environment.NewLine + "Make sure the path is not a system directory or a root directory or disk is not full or write-protected " + Environment.NewLine + "and that the file is not currently in use." + Environment.NewLine + "You may not be able to use this functionality fully.";
                            MessageBox.Show(strMessageToShow, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(strMessageToShow, false);

                        }
                        else
                        {
                            if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)
                            {

                                //Get the existing file directories within the received fax folder
                                DirectoryInfo[] _ExistingFileFolders = GetExistingFileFolders(_ReceivedFaxFilesFolderPath);

                                //Check for empty Fax File folder if any delete it
                                //Special Case 
                                bool _HasEmptyFileFolder = false;
                                try
                                {
                                    for (int i = _ExistingFileFolders.Length - 1; i >= 0; i--)
                                    {
                                        if (_ExistingFileFolders[i].GetFiles("*.tif").Length == 0)
                                        {
                                            _ExistingFileFolders[i].Delete(true);
                                            _HasEmptyFileFolder = true;
                                        }
                                    }
                                }


                                catch (UnauthorizedAccessException ex)
                                {
                                    //MessageBox.Show("Unable to Access Path :" + _ReceivedFaxFilesFolderPath, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("Access to the path " + _ReceivedFaxFilesFolderPath + " is denied." + Environment.NewLine + "Make sure the disk is not full or write-protected " + Environment.NewLine + "and that the file is not currently in use." + Environment.NewLine + "You may not be able to use this functionality fully.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);

                                }
                                catch (IOException ex)
                                {
                                    MessageBox.Show("Access to the path " + _ReceivedFaxFilesFolderPath + " is denied." + Environment.NewLine + "Make sure the disk is not full or write-protected " + Environment.NewLine + "and that the file is not currently in use." + Environment.NewLine + "You may not be able to use this functionality fully.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                                }

                                //Read the Existing folders again
                                if (_HasEmptyFileFolder)
                                {
                                    if (_ExistingFileFolders != null) { _ExistingFileFolders = null; }
                                    _ExistingFileFolders = GetExistingFileFolders(_ReceivedFaxFilesFolderPath);
                                }
                                //


                                if (_ExistingFileFolders != null)
                                {
                                    //if (_ExistingFileFolders.Length < 10)
                                    if (_ExistingFileFolders.Length < FileLoadCount)
                                    {
                                        //Get the Files count to Load
                                        //int _newFileCountToLoad = 10 - _ExistingFileFolders.Length;
                                        int _newFileCountToLoad = FileLoadCount - _ExistingFileFolders.Length;

                                        //Get files from the Fax folder of type tiff.
                                        string[] arrFilesPath = Directory.GetFiles(_ReceivedFaxFilesFolderPath, "*.tif", SearchOption.TopDirectoryOnly);

                                        // GLO2010-0004361 i.e Received faxes are not populating by date/time fax received, it is ordering by number
                                        // Change Logic : use Sorted listed for sorting files based on Created date time.





                                        SortedList<DateTime, string> sortedList = new SortedList<DateTime, string>();
                                        DateTime fileCreatedDateTime = default(DateTime);
                                        string fileName = string.Empty;

                                        foreach (string file in arrFilesPath)
                                        {
                                            fileCreatedDateTime = new FileInfo(file).CreationTime;

                                            //00003540 : Bayside Orthopaedic is receiving an error when trying to pull in faxes.
                                            //If more than one file found with same created datetime then at the time of creating sort list of faxes it throws error.
                                            //Commented the below If condition and new While loop added. Verified that if same key already exists in sort collection then create the new key by adding 10 milliseconds.

                                            while (sortedList.ContainsKey(fileCreatedDateTime))
                                            {
                                                fileCreatedDateTime = fileCreatedDateTime.AddMilliseconds(10);
                                            }

                                            sortedList.Add(fileCreatedDateTime, file);
                                        }






                                        if (sortedList.Count > 0)
                                        {
                                            string _strFileFolderName = "";
                                            string _strFileFolderPath = "";
                                            ArrayList _FilePages = null;
                                            string _filePath = string.Empty;
                                            int cnt = 0;


                                            foreach (string file in sortedList.Values)
                                            {
                                                _filePath = new FileInfo(file).FullName;

                                                //Get the Folder Name for the File
                                                _strFileFolderName = getFileName(_filePath);
                                                _strFileFolderName = _strFileFolderName.Remove((_strFileFolderName.Length - 4));
                                                _strFileFolderPath = _ReceivedFaxFilesFolderPath + "\\" + _strFileFolderName;

                                                //.Check for the Folder exists or not if not create
                                                if (Directory.Exists(_strFileFolderPath) == false)
                                                {
                                                    Directory.CreateDirectory(_strFileFolderPath);
                                                    TiffManager = new SDKInteraction.eDocTIFFManager();
                                                    _FilePages = TiffManager.SplitTiffImage(_strFileFolderPath, EncoderValue.CompressionNone, _filePath);
                                                    //Developer:Mitesh Patel
                                                    //Date: 29 Jun 2012
                                                    //Bug ID: 29003
                                                    if (_FilePages != null)
                                                    {
                                                        File.Delete(_filePath);
                                                    }
                                                }


                                                cnt += 1;

                                                if (cnt == (_newFileCountToLoad - 1))
                                                { break; }
                                            }
                                        }
                                        _Result = true;
                                    }
                                    else
                                    {
                                        _Result = true;
                                    }





                                }


                            }
                            else
                            {
                                MessageBox.Show("Folder empty or received Fax files not found.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fax folder does not Exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }//end - if (_ReceivedFaxFilesFolderPath != "")
                else
                {
                    MessageBox.Show("Invalid folder path", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
            return _Result;
        }

        private bool LoadMoreFaxFiles()
        {
            bool _Result = false;
         //   string _FileFolderName = "";
            gloEDocumentV3.SDKInteraction.eDocTIFFManager TiffManager = null;

            try
            {
                //Check if Received Fax Files Folder Path is null
                if (_ReceivedFaxFilesFolderPath != null)
                {
                    //Check if Received Fax Files Folder Path is empty
                    if (_ReceivedFaxFilesFolderPath != "")
                    {
                        //Check for the Folder exists
                        if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)
                        {

                            //Get the Files count to Load
                            //int _newFileCountToLoad = 10;
                            int _newFileCountToLoad = FileLoadCount;

                            //Get files from the Fax folder of type tiff.
                            string[] arrFilesPath = Directory.GetFiles(_ReceivedFaxFilesFolderPath, "*.tif", SearchOption.TopDirectoryOnly);


                            //Check if arrFilesPath is not null or empty
                            if (arrFilesPath != null && arrFilesPath.Length > 0)
                            {

                                #region " Get the number of files to Load "

                                //..Check for the no. of files present if less than 10 load all else load only 10 
                                //..set _newFileCountToLoad accordingly

                                //if (arrFilesPath.Length < 10)
                                if (arrFilesPath.Length < FileLoadCount)
                                {
                                    _newFileCountToLoad = arrFilesPath.Length;
                                }

                                #endregion " Get the number of files to Load "

                                #region " Sort Files by Created Date Time (Ascending Order) "

                                FileInfo _iFileInfo = null;
                                FileInfo _jFileIno = null;
                                for (int i = 0; i < arrFilesPath.Length; i++)
                                {
                                    _iFileInfo = new FileInfo(arrFilesPath[i].ToString());

                                    for (int j = i + 1; j < arrFilesPath.Length; j++)
                                    {
                                        _jFileIno = new FileInfo(arrFilesPath[j].ToString());
                                        if (_iFileInfo.CreationTime > _jFileIno.CreationTime)
                                        {
                                            arrFilesPath[i] = _jFileIno.FullName;
                                            arrFilesPath[j] = _iFileInfo.FullName;
                                            break;
                                        }
                                    }
                                }
                                if (_iFileInfo != null)
                                    _iFileInfo = null;

                                if (_jFileIno != null)
                                    _jFileIno = null;

                                #endregion " Sort Files by Created Date Time (Ascending Order) "

                                #region " Split & Create Folder Structure "

                                string _strFileFolderName = "";
                                string _strFileFolderPath = "";
                                ArrayList _FilePages = null;
                                for (int i = 0; i < _newFileCountToLoad; i++)
                                {
                                    //Get the Folder Name for the File
                                    _strFileFolderName = getFileName(arrFilesPath[i].ToString());
                                    _strFileFolderName = _strFileFolderName.Remove((_strFileFolderName.Length - 4));
                                    _strFileFolderPath = _ReceivedFaxFilesFolderPath + "\\" + _strFileFolderName;
                                    //.Check for the Folder exists or not if not create
                                    if (Directory.Exists(_strFileFolderPath) == false)
                                    {
                                        Directory.CreateDirectory(_strFileFolderPath);
                                        TiffManager = new SDKInteraction.eDocTIFFManager();
                                        _FilePages = TiffManager.SplitTiffImage(_strFileFolderPath, EncoderValue.CompressionNone, arrFilesPath[i].ToString());
                                        //Developer:Mitesh Patel
                                        //Date: 29 Jun 2012
                                        //Bug ID: 29003
                                        if (_FilePages != null)
                                        {
                                            File.Delete(arrFilesPath[i].ToString());
                                        }
                                    }

                                }//end - for (int i = 0; i < arrFilesPath.Length; i++)

                                #endregion " Split & Create Folder Structure "

                                _Result = true;

                            }//end - if (arrFilesPath != null && arrFilesPath.Length > 0)

                        }//end - if (Directory.Exists(_ReceivedFaxFilesFolderPath) == true)

                    } // end - if (_ReceivedFaxFilesFolderPath != "")

                }// end - if (_ReceivedFaxFilesFolderPath != null)

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Result = false;
            }
            finally
            {

            }
            return _Result;
        }

        private DirectoryInfo[] GetExistingFileFolders(string BaseFolderPath)
        {
            DirectoryInfo dirInfo = null;
            try
            {
                dirInfo = new DirectoryInfo(BaseFolderPath);
                return dirInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (dirInfo != null)
                {
                    dirInfo = null;
                }
            }
        }

        private ArrayList GetFolderFiles(string FolderPath)
        {
            string[] FilesPathList = null;
            ArrayList PageFilesList = null;
            try
            {
                FilesPathList = Directory.GetFiles(FolderPath, "*.tif", SearchOption.TopDirectoryOnly);
                if (FilesPathList != null)
                {
                    PageFilesList = new ArrayList();
                    for (int i = 0; i < FilesPathList.Length; i++)
                    {
                        PageFilesList.Add(FilesPathList[i].ToString());
                    }
                }
                return PageFilesList;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (FilesPathList != null)
                    FilesPathList = null;

                if (PageFilesList != null)
                    PageFilesList = null;
            }

        }

        private bool Remove()
        {
            bool _result = false;

            try
            {
                for (int i = c1Documents.Rows.Count - 1; i > 0; i--)
                {
                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                    {

                        Node oNode = c1Documents.Rows[i].Node;
                        if (oNode != null)
                        {
                            if (oNode.Level == 1)
                            {
                                string _FileFolderPath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                                if (gloGlobal.clsFileExtensions.IsSystemOrRootDir(_FileFolderPath))
                                {
                                    string strMessageToShow = "Access to the path " + _FileFolderPath + " is denied." + Environment.NewLine + "Make sure the path is not a system directory or a root directory or disk is not full or write-protected " + Environment.NewLine + "and that the file is not currently in use." + Environment.NewLine + "You may not be able to use this functionality fully.";
                                    MessageBox.Show(strMessageToShow, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    if (Directory.Exists(_FileFolderPath))
                                    {
                                        DirectoryInfo dirInfo = new DirectoryInfo(_FileFolderPath);
                                        imageControl1.CloseCurrentImage();
                                        try
                                        {
                                            imageControl1.UnloadDisplayImage();
                                        }
                                        catch
                                        {

                                        }
                                        try
                                        {
                                            dirInfo.Delete(true);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Unable to delete document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        if (Directory.Exists(_FileFolderPath))
                                        {
                                            MessageBox.Show("Unable to delete document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        if (dirInfo != null) { dirInfo = null; }

                                        // Audit log for delete Received fax
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Fax, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Delete, "Fax '" + System.Convert.ToString(oNode.Data) + "' deleted from Received fax ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                                    }
                                }

                            }
                            else if (oNode.Level == 2)
                            {
                                string _PageFilePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                                if (File.Exists(_PageFilePath))
                                {
                                    //if (imageControl1.CurrImage != null)
                                    //{
                                    //    imageControl1.CurrImage = null;
                                    //}
                                    imageControl1.CloseCurrentImage();
                                    try
                                    {
                                        imageControl1.UnloadDisplayImage();
                                    }
                                    catch
                                    {

                                    }
                                    try
                                    {
                                        File.Delete(_PageFilePath);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Unable to delete document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    if (File.Exists(_PageFilePath))
                                    {
                                        MessageBox.Show("Unable to delete document", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    // Audit log for delete Received fax
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Fax, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Delete, "" + System.Convert.ToString(oNode.Data) + " of fax '" + System.Convert.ToString(oNode.LastSibling.Parent.Data) + "' is deleted from Received fax", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                }
                            }
                        }
                        c1Documents.RemoveItem(i);
                        if (oNode != null) { oNode = null; }
                    }

                }

                _result = true;
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                _result = false;
            }
            finally
            {
                ShowFaxFilesDetails();
            }
            return _result;
        }

        private bool SendToDMS()
        {
            bool _Result = false;
            try
            {
                if (c1Documents.Rows.Count > 0)
                {
                    _SelectedDocuments.Clear();
                    //Check if Patient is selected or not
                    if (_PatientID == 0)
                    {
                        MessageBox.Show("Select Patient", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //btnPat_Down_Click(null, null);

                        btnPatDownClick();

                        //ogloPatientCntrl.txtSearchPatient.Focus();
                    }
                    else
                    {
                        for (int i = 0; i < c1Documents.Rows.Count; i++)
                        {
                            if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                            {
                                Node oNode = c1Documents.Rows[i].Node;
                                if (oNode.Level == 2)
                                {
                                    string _tempfilePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                                    int _tempPageNo = System.Convert.ToInt32(c1Documents.GetData(i, COL_DOC_HiddenPageNo));
                                    _tempPageNo = _tempPageNo - 1;
                                    _SelectedDocuments.Add(_tempfilePath);
                                }
                            }

                        }
                        if (_SelectedDocuments.Count > 0)
                        {
                            frmEDocEvent_SendRecFax ofrmSendRexFax = new frmEDocEvent_SendRecFax();
                            ofrmSendRexFax.PatientID = this._PatientID;
                            ofrmSendRexFax.oSelectedDocuments = this._SelectedDocuments;
                            ofrmSendRexFax._OpenExternalSource = _OpenExternalSource;
                            ofrmSendRexFax.ShowDialog(this);
                            if (ofrmSendRexFax.SendResult)
                            {
                                //Add in Audit Trail
                                //Roopali against bugzilla case := 5249 and sales force case no:= GLO2009-0002981
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Received fax moved to DMS", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                                Remove();
                                //Clear the existing image in the viewer control
                                //if (imageXView.Image != null)
                                //{
                                //    imageXView.Image.Dispose();
                                //}
                                //imageXView.Image = null;
                                //if (imageControl1 != null)
                                //{
                                //    imageControl1.Dispose();
                                //    imageControl1 = null;
                                //}
                            }


                            if (ofrmSendRexFax != null)
                                ofrmSendRexFax.Dispose();
                        }

                        _Result = true;

                    }
                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "
                //Add in Audit Trail
                //Roopali against bugzilla case := 5249 and sales force case no:= GLO2009-0002981
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Received fax moved to DMS", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure);

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

            }
            finally
            {

            }

            return _Result;
        }

        #endregion " 20080723 "

        #region  " Create Folder Structure of Files "

        private bool ConvertFileToFolderStructure(ArrayList FilePathList, string OutputFolderPath)
        {
            //Method for implementation in class for download fax files
            //and converting it to folder structure

            bool _Result = false;
         //   string _FileFolderName = "";
            gloEDocumentV3.SDKInteraction.eDocTIFFManager TiffManager = null;

            try
            {
                //Check if Received Fax Files Folder Path is null
                if (OutputFolderPath != null)
                {
                    //Check if Received Fax Files Folder Path is empty
                    if (OutputFolderPath != "")
                    {
                        //Check for the Folder exists
                        if (Directory.Exists(OutputFolderPath) == true)
                        {
                            if (FilePathList != null)
                            {
                                string _strFileFolderName = "";
                                string _strFileFolderPath = "";
                                ArrayList _FilePages = null;

                                if (FilePathList.Count > 0)
                                {
                                    for (int i = 0; i < FilePathList.Count; i++)
                                    {

                                        if (File.Exists(FilePathList[i].ToString()))
                                        {
                                            //Get the Folder Name for the File
                                            _strFileFolderName = getFileName(FilePathList[i].ToString());
                                            _strFileFolderName = _strFileFolderName.Remove((_strFileFolderName.Length - 4));
                                            _strFileFolderPath = OutputFolderPath + "\\" + _strFileFolderName;
                                            //.Check for the Folder exists or not if not create
                                            if (Directory.Exists(_strFileFolderPath) == false)
                                            {
                                                //Create Directory with the File Name
                                                Directory.CreateDirectory(_strFileFolderPath);
                                                //Spilt the Pages of File into individual files (i.e - 1 file per page)
                                                //and save them in the file name folder (created above)
                                                TiffManager = new SDKInteraction.eDocTIFFManager();
                                                _FilePages = TiffManager.SplitTiffImage(_strFileFolderPath, EncoderValue.CompressionNone, FilePathList[i].ToString());
                                                //Delete the existing file
                                                //Developer:Mitesh Patel
                                                //Date: 29 Jun 2012
                                                //Bug ID: 29003
                                                if (_FilePages != null)
                                                {
                                                    File.Delete(FilePathList[i].ToString());
                                                }
                                                _Result = true;

                                            }//end - if (Directory.Exists(_strFileFolderPath) == false)

                                        }//end - if (File.Exists(FilePathList[i].ToString()))

                                    }//end - for (int i = 0; i < FilePathList.Count; i++) 

                                }//end - if (FilePathList.Count > 0)

                            }//end - if (FilePathList != null)

                        }//end - if (Directory.Exists(OutputFolderPath) == true)
                        else
                        {
                            MessageBox.Show("Output folder does not Exist.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (TiffManager != null)
                {
                    TiffManager.Dispose();
                }

                if (FilePathList != null)
                {
                    FilePathList = null;
                }
            }
            return _Result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1.Enabled = false;
                pnlDocumentNameAcquiredImages.Enabled = false;
                tls_MaintainDoc.Enabled = false;
                pnlPatients.Enabled = false;
                panel4.Enabled = false;
                cmbZoomPercentage.Enabled = false;
                txtWait.Visible = true;
                txtWait.BringToFront();

                tlb_LoadImages_Click(null, null);

                pnlDocumentNameAcquiredImages.Enabled = true;
                tls_MaintainDoc.Enabled = true;
                pnlPatients.Enabled = true;
                panel4.Enabled = true;
                cmbZoomPercentage.Enabled = true;
                txtWait.Visible = false;

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


            }
            finally
            {



            }


        }

        #endregion " Create Folder Structure of Files "

        #region " Print Functionality "
        private static System.Drawing.Printing.PrinterSettings myPrinterSetting = null;
        private void tlb_Print_Click(object sender, EventArgs e)
        {
            //changes done for incident CAS-01863-Z8C4G1 click on print twice
            tmrprint.Start(); 
        }
        /// <summary>
        /// Print the selected Document
        /// </summary>
        /// 

        //private ImageCodecInfo getCodecForstring(string type)
        //{
        //    ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

        //    for (int i = 0; i < info.Length; i++)
        //    {
        //        string EnumName = type.ToString();
        //        if (info[i].FormatDescription.Equals(EnumName))
        //        {
        //            return info[i];
        //        }
        //    }

        //    return null;
        //}

        //public bool saveMultipage(System.Drawing.Image[] bmp, string location, string type)
        //{
        //    if (bmp != null)
        //    {
        //        try
        //        {
        //            ImageCodecInfo codecInfo = getCodecForstring(type);

        //            for (int i = 0; i < bmp.Length; i++)
        //            {
        //                if (bmp[i] == null)
        //                    break;
        //                bmp[i] = (Bitmap)bmp[i];
        //            }

        //            if (bmp.Length == 1)
        //            {

        //                EncoderParameters iparams = new EncoderParameters(1); // add some parameter
        //                System.Drawing.Imaging.Encoder iparam = System.Drawing.Imaging.Encoder.Compression;
        //                EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionNone));
        //                iparams.Param[0] = iparamPara;
        //                bmp[0].Save(location, codecInfo, iparams);


        //            }
        //            else if (bmp.Length > 1)
        //            {

        //                System.Drawing.Imaging.Encoder saveEncoder;
        //                System.Drawing.Imaging.Encoder compressionEncoder;
        //                EncoderParameter SaveEncodeParam;
        //                EncoderParameter CompressionEncodeParam;
        //                EncoderParameters EncoderParams = new EncoderParameters(2);

        //                saveEncoder = System.Drawing.Imaging.Encoder.SaveFlag;
        //                compressionEncoder = System.Drawing.Imaging.Encoder.Compression;

                        
        //                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
        //                CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionNone);
        //                EncoderParams.Param[0] = CompressionEncodeParam;
        //                EncoderParams.Param[1] = SaveEncodeParam;

        //                bmp[0].Save(location, codecInfo, EncoderParams);


        //                for (int i = 1; i < bmp.Length; i++)
        //                {
        //                    if (bmp[i] == null)
        //                        break;

        //                    SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
        //                    CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionNone);
        //                    EncoderParams.Param[0] = CompressionEncodeParam;
        //                    EncoderParams.Param[1] = SaveEncodeParam;
        //                    bmp[0].SaveAdd(bmp[i], EncoderParams);

        //                }

        //                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
        //                EncoderParams.Param[0] = SaveEncodeParam;
        //                bmp[0].SaveAdd(EncoderParams);
        //            }
        //            return true;


        //        }
        //        catch (System.Exception ee)
        //        {
        //            throw new Exception(ee.Message + "  Error in saving as multipage ");
        //        }
        //    }
        //    else
        //        return false;

        //}
        
        private List<Image> getAllImages = null;
        private Int32 nCurrentImagePrintCount = 0;

        private void MergeAndPrintTiffFiles()
        {
            bool blnPrinterSelected =false;            
            DialogResult dlgResult;
            dlgResult = DialogResult.None;
            PrintDialog PrintDialog1 = null;

            //int k = 0;
            List<Image> getNewImage = null;
            System.Drawing.Image getmyImage = null;
           
            nCurrentImagePrintCount = 0;
            //bool _isSave = false;
            string fileName = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".TIFF", "yyyyMMddhhmmssmmm");

            try
            {
                _blnUseDefaultPrinter = System.Convert.ToBoolean(appSettings["UseDefaultPrinter"]);
                getNewImage = new List<Image>();
                for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                    {
                        if (c1Documents.Rows[i].Node.Level == 2)
                        {
                            _PrintFilePathe = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                            if (File.Exists(_PrintFilePathe) == true)
                            {

                                getmyImage = System.Drawing.Image.FromFile(_PrintFilePathe, true);
                                getNewImage.Add(getmyImage);
                                //getNewImage[k] = getmyImage;
                                //k++;

                            }

                            c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                        }
                        else
                        {
                            c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                        }

                    }
                }
                if ((getNewImage != null) && (getNewImage.Count > 0))
                {
                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        Dictionary<String, Byte[]> dictImages = new Dictionary<string, byte[]>();
                        for (int i = 0; i < getNewImage.Count; i++)
                        {
                            if (getNewImage[i] != null)
                            {
                                //SLR: move to reusable component
                                gloGlobal.gloTSPrint.AddImagesIntoDictonaryList(ref dictImages, getNewImage[i], i);
                                //using (MemoryStream ms = new MemoryStream())
                                //{
                                //    getNewImage[i].Save(ms, ImageFormat.Png);
                                //    try
                                //    {
                                //        ms.Flush();
                                //    }
                                //    catch
                                //    {
                                //    }
                                //    dictImages.Add((i + 1).ToString(), ms.ToArray());
                                //    try
                                //    {
                                //        ms.Close();
                                //    }
                                //    catch
                                //    {
                                //    }
                                //}
                            }
                        }
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
                        gloPrintDialog.gloPrintProgressController.SendForPrint(lstDocs);
                        lstDocs.Clear();
                        ZipedFiles.Clear();
                        dictImages.Clear();
                    }
                    else
                    {

                        //try
                        //{
                        //    if (k > 0)
                        //    {
                        //        path = System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, fileName);
                        //        _isSave = saveMultipage(getNewImage, fileName, "TIFF");
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Select document(s) to print", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("ERROR While Merging TIFF files : " + ex.ToString());
                        //}
                        getAllImages = new List<Image>();
                        for (int i = 0; i < getNewImage.Count; i++)
                        {
                            gloGlobal.gloTSPrint.AddImagesIntoImageList(getAllImages, getNewImage[i]);
                        }

                        //if (_isSave == true)
                        {
                            if (_blnUseDefaultPrinter == false)
                            {
                                if (blnPrinterSelected == false)
                                {
                                    PrintDialog1 = new PrintDialog();
                                    PrintDialog1.UseEXDialog = true;
                                    dlgResult = PrintDialog1.ShowDialog(this);
                                    blnPrinterSelected = true;
                                }


                                if (dlgResult == DialogResult.OK && blnPrinterSelected == true)
                                {

                                    myPrinterSetting = PrintDialog1.PrinterSettings;

                                    try
                                    {
                                        printDocument1.PrinterSettings = myPrinterSetting;
                                    }
                                    catch
                                    {
                                        printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
                                    }

                                    printDocument1.Print();

                                }

                            }
                            else
                            {
                                if (myPrinterSetting == null)
                                {
                                    myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                                }
                                if (myPrinterSetting != null)
                                {
                                    try
                                    {
                                        printDocument1.PrinterSettings = myPrinterSetting;
                                    }
                                    catch
                                    {
                                        printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
                                    }
                                }
                                printDocument1.Print();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select document(s) to print", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString());
            }
            finally
            {

                if (PrintDialog1 != null)
                {
                    PrintDialog1.Dispose();
                    PrintDialog1 = null;
                }
                ClearImagesInList(ref getAllImages);
                ClearImagesInList(ref getNewImage);

            }
        }

        private void ClearImagesInList(ref List<Image> getNewImage)
        {
            if ((getNewImage != null) && (getNewImage.Count > 0))
            {
                for (int i = 0; i < getNewImage.Count; i++)
                {
                    try
                    {
                        System.Drawing.Image getmyImage = getNewImage[i];
                        if (getmyImage != null)
                        {
                            getmyImage.Dispose();
                            getmyImage = null;
                        }
                    }
                    catch
                    {
                    }
                }
                try
                {
                    getNewImage.Clear();
                }
                catch
                {
                }
                getNewImage = null;
            }
            
        }

        //private void SendForPrint(List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs)
        //{
        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

        //    try
        //    {
        //        gloPrintDialog.gloExtendedPrinterSettings extendedPrinterSettings= new gloPrintDialog.gloExtendedPrinterSettings();
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
      

        //private void Print()
        //{
        //    bool blnPrinterSelected;
        //    blnPrinterSelected = false;
        //    DialogResult dlgResult;
        //    dlgResult = DialogResult.None;
        //    //Bug #79555: 00000863: Received faxes printing issue
        //    PrintDialog PrintDialog1 = null; //SLR: Moved the initiation to approbriate place
        //    try
        //    {
        //        //Mayuri:20090905
        //        //Variable for checking default printer settings checked or unchecked
        //        _blnUseDefaultPrinter = System.Convert.ToBoolean(appSettings["UseDefaultPrinter"]);
        //        for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
        //        {
        //            if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
        //            {

        //                if (c1Documents.Rows[i].Node.Level == 2)
        //                {
        //                    _PrintFilePathe = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
        //                    if (File.Exists(_PrintFilePathe) == true)
        //                    {
        //                        //Added by Mayuri:20090905
        //                        //if DefaultPrinterSettings checkbox is unchecked
        //                        if (_blnUseDefaultPrinter == false)
        //                        {
        //                            //solving sales force case-GLO2010-0005251
        //                            if (blnPrinterSelected == false)
        //                            {
        //                                PrintDialog1 = new PrintDialog();
        //                                PrintDialog1.UseEXDialog = true;
        //                                dlgResult = PrintDialog1.ShowDialog(this);
        //                                blnPrinterSelected = true;
        //                            }

        //                            if (dlgResult == DialogResult.OK && blnPrinterSelected == true)
        //                            {
        //                                myPrinterSetting = PrintDialog1.PrinterSettings;
        //                                try
        //                                {
        //                                    printDocument1.PrinterSettings = myPrinterSetting;
        //                                }
        //                                catch
        //                                {
        //                                    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
        //                                }
                                       
        //                                 printDocument1.Print();
        //                            }
        //                            //End.
        //                        }

        //                        //if DefaultPrinterSettings checkbox is checked
        //                        else
        //                        {
        //                            if (myPrinterSetting == null)
        //                            {
        //                                myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
        //                            }
        //                            if (myPrinterSetting != null)
        //                            {
        //                                try
        //                                {
        //                                    printDocument1.PrinterSettings = myPrinterSetting;
        //                                }
        //                                catch
        //                                {
        //                                    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
        //                                }
        //                            }
        //                            printDocument1.Print();
                                    
        //                        }

        //                    }
        //                    c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
        //                }
        //                else
        //                {
        //                    c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
        //                }

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.ToString());
        //    }
        //    finally
        //    {
        //        //Bug #79555: 00000863: Received faxes printing issue
        //        if (PrintDialog1 != null)
        //        {
        //            PrintDialog1.Dispose();
        //            PrintDialog1 = null;
        //        }
        //    }
        //}
        bool foundPageBound = false;
        SizeF pageBound = new SizeF(0, 0);
        private SizeF getPageBound(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (foundPageBound) return pageBound;
            RectangleF marginBounds = e.MarginBounds;
            RectangleF printableArea = e.PageSettings.PrintableArea;
            float availableWidth =  printDocument1.OriginAtMargins ? marginBounds.Width : (e.PageSettings.Landscape ? printableArea.Height : printableArea.Width);
            float availableHeight =  printDocument1.OriginAtMargins ? marginBounds.Height : (e.PageSettings.Landscape ? printableArea.Width : printableArea.Height);
            foundPageBound = true;
            pageBound = new SizeF(availableWidth, availableHeight);
            return pageBound;
        }
        void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printDocument1.OriginAtMargins = false; 
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if ((getAllImages == null) || (getAllImages.Count <= 0)) return;
            try
            {
                 System.Drawing.Image img = getAllImages[nCurrentImagePrintCount];
                 if (img != null)
                 {
                     SizeF pageSize = getPageBound(e);
                     float zoomZ = 100.0f;
                     float Width = 0;
                     try
                     {
                         Width = (img.Width / img.HorizontalResolution);
                     }
                     catch
                     {
                         Width = pageSize.Width;
                     }
                     float Height = 0;
                     try
                     {
                         Height = (img.Height / img.VerticalResolution);
                     }
                     catch
                     {
                         Height = pageSize.Height;
                     }
                     if (Width <= 0)
                     {
                         Width = e.MarginBounds.Width;
                     }
                     if (Height <= 0)
                     {
                         Height = e.MarginBounds.Height;
                     }
                     float zoomX =   pageSize.Width /  Width ;
                     float zoomY = pageSize.Height / Height;
                     
                     zoomZ = Math.Min(zoomX, zoomY);

                     if (zoomZ == 0)
                     {
                         zoomZ = 1;
                     }
                     e.Graphics.DrawImage(img, 0, 0, (Width * zoomZ), (Height * zoomZ));
                     //SLR: 9/26/2016 The bottom code is the residual Bug, if residual bug is the correct implementation then, comment the above code and uncomment bottom code as well as comment beginprint event to have nothing.
                     //e.Graphics.DrawImage(img, e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                     
                     img = null;
                 }
                ++nCurrentImagePrintCount;
                e.HasMorePages = (nCurrentImagePrintCount < getAllImages.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString());
            }
        }

        #endregion " Print Functionality "

        #region " Designer Code "

        private void btn_Down_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloEDocumentV3.Properties.Resources.yellowbtn;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btn_Down_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackgroundImage = global::gloEDocumentV3.Properties.Resources.btn_img;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void pnlAcquiredImages_Resize(object sender, EventArgs e)
        {
            try
            {
                if (_OpenExternalSource != enum_OpenExternalSource.DirectMessage)
                {
                    c1Documents.Cols[COL_DOC_SELECT].Width = 17;
                    c1Documents.Cols[COL_DOC_NAME].Width = (c1Documents.Width - 35);
                    c1Documents.Cols[COL_DOC_PATH].Width = 0;
                    c1Documents.Cols[COL_DOC_TYPE].Width = 0;
                    c1Documents.Cols[COL_DOC_HiddenPath].Width = 0;
                    c1Documents.Cols[COL_DOC_HiddenPageNo].Width = 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btn_Up_Click(object sender, EventArgs e)
        {
            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;

            btn_Down.Visible = true;
            btn_Up.Visible = false;
            pnlReceivedFaxDetailsBody.Visible = false;

            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);

        }

        private void btn_Down_Click(object sender, EventArgs e)
        {

            imageControl1.UpdateScreenOfControl(false);
            Point myPos = imageControl1.PictureScrollPos;

            btn_Down.Visible = false;
            btn_Up.Visible = true;
            pnlReceivedFaxDetailsBody.Visible = true;

            imageControl1.PictureScrollPos = myPos;
            imageControl1.UpdateScreenOfControl(true);

        }


        #endregion " Designer Code "

        #region " Pegasus Imaging "

        private void LoadImage(string _strFilePath,int _CurrIndex=9)
        {

            try
            {

                //To check wheather the image is being loading or not [Dhruv] 
                string sImage = _strFilePath;
                // bmp = new System.Drawing.Bitmap(oImageDocuments[i].ToString());
                if (File.Exists(sImage) == true)
                {
                    //imageXView.Image = PegasusImaging.WinForms.ImagXpress9.ImageX.FromFile(imagXpress1, sImage, 1);
                    //imageXView.Image.ImageXData.Resolution.Units = GraphicsUnit.Inch;

                    imageControl1.SetImageWithPath(_strFilePath, _CurrIndex:_CurrIndex);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("This fax has already been processed by another user.", gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }


            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Contains("File format unsupported"))
                {
                    MessageBox.Show("File format not supported for the below file:" + "\n" + "\n" + _strFilePath, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    MessageBox.Show(ex.ToString());
                }
                //End Code add
                #endregion " Make Log Entry "
            }
            finally
            {

            }
        }

        private void Rotate(bool Clockwise, int _CurrIndex)
        {
            //processor of ImagXpress for image processing operations
            //PegasusImaging.WinForms.ImagXpress9.Processor processor1 = new PegasusImaging.WinForms.ImagXpress9.Processor(imagXpress1);

            try
            {

                if (imageControl1.CurrImage != null)
                {

                    //imageControl1.SetImageWithPath(Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)));
                    imageControl1.RotateImage(Clockwise, _CurrIndex);
                    //oImg.CurrentDisplayedImage.Save(Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_PATH)));

                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //if (processor1 != null)
                //{
                //    processor1.Image = null;
                //    processor1.Dispose();
                //}
            }


        }
        private void Rotate(double angle)
        {
            //processor of ImagXpress for image processing operations
            //PegasusImaging.WinForms.ImagXpress9.Processor processor1 = new PegasusImaging.WinForms.ImagXpress9.Processor(imagXpress1);

            try
            {
                //if (imageXView.Image != null)
                //{
                //    processor1.Image = imageXView.Image;
                //    processor1.Rotate(angle);
                //    imageXView.Image.Save(c1Documents.GetData(c1Documents.RowSel, COL_DOC_HiddenPath).ToString());
                //}
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "


                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //if (processor1 != null)
                //{
                //    processor1.Image = null;
                //    processor1.Dispose();
                //}
            }


        }

        private void Zoom()
        {
            //if (cmbZoomPercentage.SelectedIndex != -1)
            //{
            //    double _ZoomFactor = 1.0;
            //    try
            //    {
            //        if (imageXView.Image != null)
            //        {
            //            switch (cmbZoomPercentage.Text)
            //            {
            //                case "25%":
            //                    _ZoomFactor = 0.25;
            //                    break;
            //                case "50%":
            //                    _ZoomFactor = 0.50;
            //                    break;
            //                case "75%":
            //                    _ZoomFactor = 0.75;
            //                    break;
            //                case "100%":
            //                    _ZoomFactor = 1.0;
            //                    break;
            //                case "125%":
            //                    _ZoomFactor = 1.25;
            //                    break;
            //                case "150%":
            //                    _ZoomFactor = 1.50;
            //                    break;
            //                case "175%":
            //                    _ZoomFactor = 1.75;
            //                    break;
            //                case "200%":
            //                    _ZoomFactor = 2.0;
            //                    break;
            //                case "400%":
            //                    _ZoomFactor = 4.0;
            //                    break;
            //            }

            //            imageXView.ZoomFactor = (double)_ZoomFactor;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        #region " Make Log Entry "

            //        _ErrorMessage = ex.ToString();
            //        //Code added on 7rd October 2008 By - Sagar Ghodke
            //        //Make Log entry in DMSExceptionLog file for any exceptions found
            //        if (_ErrorMessage.Trim() != "")
            //        {
            //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //            _MessageString = "";
            //        }

            //        //End Code add
            //        #endregion " Make Log Entry "
            //    }
            //}
            //else
            //{
            //    // Sudhir 20090112
            //    if (_ZoomState == enum_ZoomState.BestFit) { ZoomFitButton_Click(null, null); }
            //    if (_ZoomState == enum_ZoomState.FitToHeight) { ZoomHeightButton_Click(null, null); }
            //    if (_ZoomState == enum_ZoomState.FitToWidth) { ZoomWidthButton_Click(null, null); }
            //}

        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
           
            if (imageControl1.CurrImage != null)
            {
               
                if (cmbZoomPercentage.SelectedIndex < (cmbZoomPercentage.Items.Count - 1))
                {
                    if (cmbZoomPercentage.SelectedIndex != 8)
                    {
                        imageControl1.UpdateScreenOfControl(false);
                        if (cmbZoomPercentage.SelectedIndex == 9 || cmbZoomPercentage.SelectedIndex == 10 || cmbZoomPercentage.SelectedIndex == 11 || cmbZoomPercentage.SelectedIndex == 12)
                        {
                            cmbZoomPercentage.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex + 1;
                        }
                        imageControl1.UpdateScreenOfControl(true);
                    }
                }
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
           
            if (imageControl1.CurrImage != null)
            {
                if (cmbZoomPercentage.SelectedIndex > 0)
                {
                    if (cmbZoomPercentage.SelectedIndex != 9 && cmbZoomPercentage.SelectedIndex != 10 && cmbZoomPercentage.SelectedIndex != 11 && cmbZoomPercentage.SelectedIndex != 12)
                    {
                        imageControl1.UpdateScreenOfControl(false);
                     
                       cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex - 1; 

                        imageControl1.UpdateScreenOfControl(true);
                    }
                }
            }
        }

        private void cmbZoomPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZoomPercentage.SelectedIndex != -1)
            {
                imageControl1.UpdateScreenOfControl(false);
                imageControl1.ZoomValueChanged((ComboBox)sender);
                imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;
                imageControl1.UpdateScreenOfControl(true);
               // Zoom();
              //  _ZoomState = enum_ZoomState.ZoomPercent;
            }
        }

        private void ZoomHeightButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitHeight);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITHEIGHT);
            cmbZoomPercentage.SelectedIndex = 11;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.FitToHeight;
        }

        private void ZoomFitButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitBest);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITPAGE);
            cmbZoomPercentage.SelectedIndex = 9;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.BestFit;
        }

        private void ZoomWidthButton_Click(object sender, EventArgs e)
        {
            //ZoomToolUpdate(ZoomToFitType.FitWidth);
            imageControl1.UpdateScreenOfControl(false);
            imageControl1.ZoomImage(gloScanImaging.ZoomMode.FITWIDTH);
            cmbZoomPercentage.SelectedIndex = 10;
            imageControl1.UpdateScreenOfControl(true);
            //_ZoomState = enum_ZoomState.FitToWidth;
        }

        //private void ZoomToolUpdate(ZoomToFitType zoom)
        //{
        //    try
        //    {
        //        //imageXView object's AutoResize is set to BestFit at startup, we must
        //        //set to a different enumeration that allows ZoomToFit() to have an effect
        //       // imageXView.AutoResize = AutoResizeType.CropImage;

        //        //Zoom to BestFit
        //      //  imageXView.ZoomToFit(zoom);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "


        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        #endregion

        private void frmEDocEvent_ReceivedFax_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_OpenExternalSource != enum_OpenExternalSource.DirectMessage)
            {
                EvnRefreshDocuments();
            }
            if (oToolTip != null)
            {
                oToolTip.RemoveAll();
                oToolTip.Dispose();
                oToolTip = null;
            }

            try
            {
                if (oPDFView != null)
                {
                    //SG: Memory Leaks, removing control before dispose
                    if (pnlPreview.Controls.Contains(oPDFView) == true) { pnlPreview.Controls.Remove(oPDFView); }
                    //

                    if (oPDFView.GetDoc() != null)
                    {
                        oPDFView.CloseDoc();
                        oPDFDoc.Close();
                        oPDFDoc.Dispose();
                        oPDFDoc = null;

                        try
                        {
                            if (oPDFView.Container != null)
                            {
                                oPDFView.Container.Dispose();
                            }

                            oPDFView.Dispose();
                        }
                        catch (Exception ex)
                        {
                           // _ErrorMessage = ex.ToString();
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                        }
                        oPDFView = null;
                    }
                    else
                    {
                        oPDFView.CloseDoc();
                        try
                        {
                            if (oPDFView.Container != null)
                            {
                                oPDFView.Container.Dispose();
                            }


                            oPDFView.Dispose();
                        }
                        catch (Exception ex)
                        {
                           // _ErrorMessage = ex.ToString();
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                        }
                        oPDFView = null;
                    }
                }

                if (oPDFDoc != null) { oPDFDoc.Dispose(); oPDFDoc = null; }
                try
                {
                    if (oPatientListControl != null)
                    {
                        try
                        {
                            oPatientListControl.Grid_DoubleClick -= oPatientListControl_Grid_DoubleClick;
                        }
                        catch
                        {
                        }
                        try
                        {
                            oPatientListControl.ItemClosedClick -= oPatientListControl_ItemClosedClick;
                        }
                        catch
                        {
                        }
                        oPatientListControl.Dispose();
                        oPatientListControl = null;
                    }
                }
                catch
                {
                }

            }
            catch (Exception ex)
            {
               // _ErrorMessage = ex.ToString();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btn_Left_MouseHover(object sender, EventArgs e)
        {
            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.RewindHover;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Left_MouseLeave(object sender, EventArgs e)
        {
            btn_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btn_Left.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Right_MouseHover(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.ForwardHover;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Right_MouseLeave(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Up_MouseHover(object sender, EventArgs e)
        {
            btn_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UPHover;
            btn_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Up_MouseLeave(object sender, EventArgs e)
        {
            btn_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btn_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Down_MouseHover_1(object sender, EventArgs e)
        {
            btn_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.DownHover;
            btn_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Down_MouseLeave_1(object sender, EventArgs e)
        {
            btn_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btn_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Down_MouseHover(object sender, EventArgs e)
        {
            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.DownHover;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Down_MouseLeave(object sender, EventArgs e)
        {
            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Up_MouseHover(object sender, EventArgs e)
        {
            btnPat_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UPHover;
            btnPat_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Up_MouseLeave(object sender, EventArgs e)
        {
            btnPat_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btnPat_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseHover(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browsePlusHover;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseLeave(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browsePlus;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseHover(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browseMinusHover;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseLeave(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.browseMinus;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }



        private void tlb_ExportToPDF_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.Document.ExportDocument oExportDocument = null; // new gloEDocumentV3.Document.ExportDocument();
            gloEDocumentV3.Document.Documents oDocument = new gloEDocumentV3.Document.Documents();
           


            try
            {

                if (c1Documents.Rows.Count > 1)
                {

                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            Node oNode = c1Documents.Rows[i].Node;
                            if (oNode.Level == 2)
                            {
                                oExportDocument = new gloEDocumentV3.Document.ExportDocument();
                                oExportDocument.DocumentSelect = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_SELECT));
                                oExportDocument.PageName = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_NAME));
                                oExportDocument.Documentname = System.Convert.ToString(c1Documents.GetData(i, COCL_DOC_HiddenDocumentName));
                                oExportDocument.DocumentPath = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_PATH));
                                oExportDocument.DocumentType = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_TYPE));
                                oExportDocument.DocumentHiddenPath = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_HiddenPath));
                                oExportDocument.DocumentHiddenPageNo = System.Convert.ToString(c1Documents.GetData(i, COL_DOC_HiddenPageNo));

                                oDocument.Add(oExportDocument);


                            }
                        }

                    }
                    if (oDocument.Count > 0)
                    {
                        frmEDocEvent_ExportToPDF ofrmExportToPDF = new frmEDocEvent_ExportToPDF(oDocument);
                        ofrmExportToPDF.oSelectedDocuments = this._SelectedDocuments;
                        ofrmExportToPDF.ShowDialog(this);
                        ofrmExportToPDF.Dispose();
                        ofrmExportToPDF = null;
                        for (int i = 0; i < oDocument.Count; i++)
                        {
                            oExportDocument = oDocument[i];
                            if (oExportDocument != null)
                            {
                                oExportDocument.Dispose();
                                oExportDocument = null;
                            }
                        }
                        oDocument.Dispose();
                        oDocument = null;
                    }


                    if (c1Documents != null)
                    {

                        //when form closed It should Disselect all the checkoboxes 
                        if (DialogResult.Cancel == System.Windows.Forms.DialogResult.Cancel)
                        {
                            for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                            {
                                Node oPageNode = c1Documents.Rows[i].Node;

                                if (oPageNode.Level == 2)
                                {
                                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    {
                                        c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                                    }

                                }
                                if (oPageNode.Level == 1)
                                {
                                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    {
                                        c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                                    }
                                }
                                if (oPageNode.Level == 0)
                                {
                                    if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    {
                                        c1Documents.SetCellCheck(i, COL_DOC_SELECT, CheckEnum.Unchecked);
                                    }
                                }


                            }
                        }
                    }



                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Shubhangi 20091231
        //Add event for the new button

        private void btn_Collapse_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (c1Documents != null && c1Documents.Rows.Count > 0)
                {
                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.Rows[i].IsNode)
                        {
                            if (c1Documents.Rows[i].Node.Level == 1)
                            {
                                c1Documents.Rows[i].Node.Collapsed = true;
                            }
                        }
                    }

                    System.Drawing.Point scrollPt = new System.Drawing.Point(0, 0);
                    c1Documents.ScrollPosition = scrollPt;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                //btn_Collapse_1.Visible = true;
                //btn_Expand_1.Visible = false;

                btn_Collapse.Visible = false;
                btn_Expand.Visible = true;
            }

        }

        //Shubhangi
        //Add event for the new button
        private void btn_Expand_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (c1Documents != null && c1Documents.Rows.Count > 0)
                {
                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.Rows[i].IsNode)
                        {
                            //Check for if Root Node is collapsed if yes expand it
                            if (c1Documents.Rows[i].Node.Level == 0 && c1Documents.Rows[i].Node.Expanded == false)
                            {
                                c1Documents.Rows[i].Node.Expanded = true;
                            }
                            else if (c1Documents.Rows[i].Node.Level == 1)
                            {
                                c1Documents.Rows[i].Node.Expanded = true;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                //btn_Collapse_1.Visible = false;
                //btn_Expand_1.Visible = true;

                btn_Collapse.Visible = true;
                btn_Expand.Visible = false;
            }
        }

        private void frmEDocEvent_ReceivedFax_Activated(object sender, EventArgs e)
        {
            if (_PatientID > 0)
            {
                if (this.MdiParent != null)
                {
                    if (_OpenExternalSource != enum_OpenExternalSource.DirectMessage)
                    {
                        EvnActivateReciveFax(_PatientID);
                    }
                }
            }

        }


        public Int64 GetCurrentPatientID
        {
            get { return _PatientID; }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
             try
             {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                  //  nPageNo = 1;
                    if( oPDFView.GetDoc() !=null )
                    {
                    oPDFView.GotoFirstPage();
                    }

                    lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage() + " of " + oPDFView.GetPageCount();
               
             }
            catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
             }
       
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;

               // nPageNo = 1;
                if (oPDFView.GetDoc() != null)
                {
                    oPDFView.GotoPreviousPage ();
                }
                if (oPDFView.GetCurrentPage()==1)
                {
                    btnPrevious.Enabled = false;
                    btnFirst .Enabled = false;
                }

                lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage() + " of " + oPDFView.GetPageCount();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                 btnPrevious.Enabled = true ;
                 btnFirst.Enabled = true;

             //   nPageNo = 1;
                if (oPDFView.GetDoc() != null)
                {
                    oPDFView.GotoNextPage();
                }
                 if( oPDFView.GetCurrentPage() >= oPDFView.GetPageCount())
                 {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                 }

                lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage() + " of " + oPDFView.GetPageCount();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
              //  nPageNo = 1;
                if (oPDFView.GetDoc() != null)
                {
                    oPDFView.GotoLastPage();
                }

                lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage() + " of " + oPDFView.GetPageCount();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void frmEDocEvent_ReceivedFax_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void tlb_SendToRCM_Click(object sender, EventArgs e)
        {
            try
            {
                if (SendToRCM())
                {
                    ShowFaxFilesDetails();

                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }


        private bool SendToRCM()
        {
            bool _Result = false;

            try
            {
                if (c1Documents.Rows.Count > 0)
                {
                    _SelectedDocuments.Clear();

                    for (int i = 0; i < c1Documents.Rows.Count; i++)
                    {
                        if (c1Documents.GetCellCheck(i, COL_DOC_SELECT) == CheckEnum.Checked)
                        {
                            Node oNode = c1Documents.Rows[i].Node;

                            if (oNode.Level == 2)
                            {
                                string _tempfilePath = c1Documents.GetData(i, COL_DOC_HiddenPath).ToString();
                                int _tempPageNo = System.Convert.ToInt32(c1Documents.GetData(i, COL_DOC_HiddenPageNo));
                                _tempPageNo = _tempPageNo - 1;
                                _SelectedDocuments.Add(_tempfilePath);
                            }
                        }
                    }

                    if (_SelectedDocuments.Count > 0)
                    {
                        frmEDocEvent_SendRecFax ofrmSendRexFax = new frmEDocEvent_SendRecFax();
                        ofrmSendRexFax.PatientID = -1;
                        ofrmSendRexFax.oSelectedDocuments = this._SelectedDocuments;
                        ofrmSendRexFax._OpenExternalSource = enum_OpenExternalSource.RCM;
                        ofrmSendRexFax.chkSendTask.Enabled = true;
                        ofrmSendRexFax.chkSendTask.Checked = false;
                        ofrmSendRexFax.ShowDialog(this);

                        if (ofrmSendRexFax.SendResult)
                        {
                            //Add in Audit Trail
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Received fax moved to RCM", -1, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                            Remove();
                            //Clear the existing image in the viewer control
                            //if (imageXView.Image != null)
                            //{
                            //    imageXView.Image.Dispose();
                            //    imageXView.Image = null;
                            //}
                            //if (imageControl1 != null)
                            //{
                            //    imageControl1.Dispose();
                            //    imageControl1 = null;
                            //}
                            
                        }

                        if (ofrmSendRexFax != null)
                        {
                            ofrmSendRexFax.Dispose();
                            ofrmSendRexFax = null;
                        }
                    }

                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                #region " Make Log Entry "
                //Add in Audit Trail
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Received fax moved to RCM", -1, 0, 0, gloAuditTrail.ActivityOutCome.Failure);

                _ErrorMessage = ex.ToString();
                
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                #endregion " Make Log Entry "

            }
            finally
            {

            }

            return _Result;
        }

        private void tmrprint_Tick(object sender, EventArgs e)
        {
            //changes done for incident CAS-01863-Z8C4G1 click on print twice
            tmrprint.Stop(); 
            nCurrentImagePrintCount = 0;
            foundPageBound = false;
            MergeAndPrintTiffFiles();
            nCurrentImagePrintCount = 0;
            foundPageBound = false;
        }


        void imageControl1_SizeChanged(object sender, System.EventArgs e)
        {
            if (cmbZoomPercentage.SelectedIndex != -1)
            {
                imageControl1.UpdateScreenOfControl(false);
                imageControl1.ZoomValueChanged((ComboBox)cmbZoomPercentage);
                imageControl1._CurrZoomIndex = cmbZoomPercentage.SelectedIndex;
                imageControl1.UpdateScreenOfControl(true);
                // Zoom();
                //  _ZoomState = enum_ZoomState.ZoomPercent;
            }
        }

    }//end - form

}//end - namespace
