using System;
using System.Data;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;
using gloUserControlLibrary;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.IO;
using System.Collections;
using System.Collections.Generic;

//Audit trail exceptions added by Madan-20100402
namespace gloEmdeonInterface.Forms
{
    public partial class frmEmdeonInterface : Form
    {
            
        clsGetGloLabData objclsGetGloLabData = new clsGetGloLabData();        
        private System.Timers.Timer _orderTimer;
        public string TestsName = ""; // added to show testnames on EmdeonScreen ,v8022
        public string strDiag = "";
        private bool _orderIsPlaced = false;
        private Int32 _sessionTimerCount = 0;
        private bool _msgBoxActivated = false;
        //Added by Mayuri:20140508-to set providerid as selected in provider dropdown
        private Int64 _ProviderID=0;
        private Int64 _ClinicID=0;

        gloUserControlLibrary.gloUC_PatientStrip oPatientStrip = null;
        Int64 _PatientID = 0;
        Int64 _SelectedOrderID = 0;
        String _sTestIDs = "";

        

        // Code by : Abhijeet Farkande on date : 20100203
        // defines the variable for save User id and Name
        long _UserID = 0;
        string _UserName = "";
        // End of code
        //Added code session login & logout---By Madan
        string URl1 = string.Empty;
        string URl2 = string.Empty;
        string URl = string.Empty;
        string _strLogout = string.Empty;
        System.Uri _Uri;
        //---
        string DatabaseConnectionString = string.Empty;
        //Added by madan on 20100513
        string gstrMessageBoxCaption = string.Empty;
        bool _IsFormClose = false;

        //Added by madan on 20100610-- for modify order.
        string _OrderRefrenceId = string.Empty;
        Int64 _OrderId = 0;
        bool _IsOrderEdited = false;

        public Int64 nTaskID = 0;
       
        public bool IsOrderEdited
        {
            get { return _IsOrderEdited; }
            set { _IsOrderEdited = value; }
        }
        //End madan changes.
        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order 
        public bool _isSplitOrder = false;
        public bool isSplitOrder
        {
            get { return _isSplitOrder; }
            set { _isSplitOrder = value; }
        }

        public Int64 LoginProviderID { get; set; }
   

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public frmEmdeonInterface()
        {

            SetupOrderTimer();          

            InitializeComponent();
            LoadPatientStrip();

            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion
        }



        public frmEmdeonInterface(Int64 PatientID, Int64 SelectedOrderID = 0,string sTestIDs = "",Boolean bIsDelete = false )
        {
            SetupOrderTimer();
            InitializeComponent();
            _PatientID = PatientID;
            _SelectedOrderID = SelectedOrderID;
            _sTestIDs = sTestIDs;

            if (appSettings != null)
            {
                DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);

                // Code by : Abhijeet Farkande on date : 20100203
                // changes : accessing the user id and Name
                _UserID = Convert.ToInt64(appSettings["UserID"]);
                _UserName = Convert.ToString(appSettings["UserName"]);
                // End of code
            }
            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion
            LoadPatientStrip();
        }
        //Added by madan on 20100610-- for editing order.
        public frmEmdeonInterface(Int64 PatientID,string OrderReferanceId,Int64 OrderId)
        {
            SetupOrderTimer();       
             InitializeComponent();
            _PatientID = PatientID;
            //Added by madan on 20100610
            _OrderRefrenceId = OrderReferanceId;
            _OrderId = OrderId;
            //end madan

            //appSettings = new System.Collections.Specialized.NameValueCollection(); 
            if (appSettings != null)
            {
                DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);

                // Code by : Abhijeet Farkande on date : 20100203
                // changes : accessing the user id and Name
                _UserID = Convert.ToInt64(appSettings["UserID"]);
                _UserName = Convert.ToString(appSettings["UserName"]);
                // End of code
            }
            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion
            LoadPatientStrip();
        }

        bool bIsInPrint = false;
        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                //Problem : 00000229
                //Issue : gloEMR crashes after placing the emdeon lab order.
                //Change : avoid execution of same code repeatedly when user selects same action multiple times.
                //         If the flag  _IsFormClose is set then display a waiting message and return from this method.
                if (_IsFormClose == true)
                {
                    pnlregistration.Visible = true;
                    pnlregistration.BringToFront();
                    Application.DoEvents();

                    return;
                }

                if (e.ClickedItem.Tag != null)
                {
                    switch (e.ClickedItem.Tag.ToString().ToLower())
                    {
                        case "close":

                            //Problem : 00000229
                            //Issue : gloEMR crashes after placing the emdeon lab order.
                            //Change : display a waiting cursor once the user selects the action.
                            this.Cursor = Cursors.WaitCursor;

                            // _IsFormClose = true;
                            if (_orderIsPlaced)
                            {
                                if (_OrderId > 0 && _OrderRefrenceId != "")
                                {
                                    _IsFormClose = true;
                                    LogOutBrowserSession();
                                    
                                    objclsGetGloLabData.OrderNotCPOE = chkCPOE.Checked;
                                    objclsGetGloLabData.ProviderID = _ProviderID;

                                    objclsGetGloLabData.ModifiyOrderId = _OrderId;
                                    objclsGetGloLabData.ModifiyOrderReferenceId = _OrderRefrenceId;
                                    objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Selected);
                                    _IsOrderEdited = true;

                                    //by Abhijeet on 20100619                           
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed by modifying order successfully", _PatientID, _OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                                    this.Close();
                                }
                                else
                                {

                                    //Modified as per "DREW NALON" Comments by madan on 20100515
                                    DialogResult drMesgResult = MessageBox.Show("You have already placed a lab order in Emdeon.Would you like to save the order to the EMR? \r\n"
                                                                                 + "If you do not save the order now, the order will be downloaded the next time an order is saved.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    //if (MessageBox.Show("You have placed an order in Emdeon, Do you want save an Order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    //{
                                    if (drMesgResult == DialogResult.No)
                                    {
                                        _IsFormClose = true;
                                        LogOutBrowserSession();
                                        //by Abhijeet on 20100429                           
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        this.Close();

                                    }
                                    else
                                    {
                                        _IsFormClose = true;
                                        LogOutBrowserSession();
                                        objclsGetGloLabData.OrderNotCPOE = chkCPOE.Checked;
                                        objclsGetGloLabData.ProviderID = _ProviderID;

                                        objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Duration);
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        _IsOrderEdited = true;
                                        this.Close();

                                    }
                                }
                            }
                            else
                            {
                                if (_OrderId > 0 && _OrderRefrenceId != "")
                                {
                                    if (MessageBox.Show("Are you sure you want to exit without editing a lab order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _IsFormClose = true;
                                        LogOutBrowserSession();
                                        //by Abhijeet on 20100429                           
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        this.Close();
                                    }
                                    else
                                    {
                                        _IsFormClose = false;
                                        return;
                                    }
                                }
                                else
                                {

                                    //Modified as per "DREW NALON" Comments by madan on 20100515
                                    if (MessageBox.Show("Are you sure you want to exit without placing a lab order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _IsFormClose = true;
                                        LogOutBrowserSession();
                                        //by Abhijeet on 20100429                           
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        this.Close();
                                    }
                                    else
                                    {
                                        _IsFormClose = false;
                                        return;
                                    }
                                }

                            }

                            break;

                        case "saveandclose":
                            //Problem : 00000229
                            //Issue : gloEMR crashes after placing the emdeon lab order.
                            //Change : display a waiting cursor once the user selects the action.
                            this.Cursor = Cursors.WaitCursor;

                            if (!_orderIsPlaced)
                            {
                                //15-Dec-15 Aniket: Resolving Bug #91893: glo EMR >> On Print Requisition screen loading symbol is still display after loading page 
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("A lab order must be placed in order to save.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            LogOutBrowserSession();

                            //modified by madan on 20100612 for Edit Emdeon order.
                            
                            objclsGetGloLabData.OrderNotCPOE = chkCPOE.Checked;
                            objclsGetGloLabData.ProviderID = _ProviderID;
                            if (_OrderId > 0 && _OrderRefrenceId != "")
                            {
                                objclsGetGloLabData.ModifiyOrderId = _OrderId;
                                objclsGetGloLabData.ModifiyOrderReferenceId = _OrderRefrenceId;
                                objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Selected);
                                _IsOrderEdited = true;

                                //by Abhijeet on 20100619                           
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Modify, "External lab interface order modified successfully", _PatientID, _OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            }
                            else
                            {
                                //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order 
                                objclsGetGloLabData.GetMySelectedOrderId = _SelectedOrderID;
                                objclsGetGloLabData.IsSplitOrder = _isSplitOrder;
                                objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Duration);
                                _IsOrderEdited = true;
                                //by Abhijeet on 20100619                            
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "External lab interface order save & closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            }



                            if (_SelectedOrderID != 0 && _sTestIDs != "")   //Delete Test After Creating New Order.
                            {
                                Delete_LabTests(_SelectedOrderID,_sTestIDs);
                            }
                            else if (_SelectedOrderID != 0 && _sTestIDs == "")   //Delete order After Creating New Order.
                            {
                                Delete_LabOrder(_SelectedOrderID);
                            }
                           


                            if (objclsGetGloLabData != null)
                            {
                                objclsGetGloLabData = null;
                            }

                            //by Abhijeet on 20100429                            
                            _IsFormClose = true;
                            this.Close();

                            // End of code By Abhijeet Farkande On Date 20100227

                            break;

                        case "print":
                            if (!bIsInPrint)
                            {
                                bIsInPrint = true;
                                tlbbtn_Print.Enabled = false;
                                Application.DoEvents();
                                mshtml.IHTMLFrameBase frame = webBrowserEmdeon.Document.GetElementById("orderTemplate").DomElement as mshtml.IHTMLFrameBase;
                                String frmUrl;
                                if (frame.src.Contains(clsEmdeonGeneral.emdeonURL))
                                {
                                    frmUrl = frame.src;
                                }
                                else
                                {
                                    frmUrl = clsEmdeonGeneral.emdeonURL + frame.src;
                                }
                                Uri rptUrl = new System.Uri(frmUrl);

                                WebBrowser webBrowserforPrint = new WebBrowser();
                                //webBrowserforPrint.Url = webBrowserEmdeon.Url;
                                webBrowserforPrint.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowserforPrint_DocumentCompleted);
                                webBrowserforPrint.Url = rptUrl;
                            }
                            //else
                            //{
                            //    MessageBox.Show("Print request already sent", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}

                            //bIsInPrint = false;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {   // By Abhijeet Farkande On Date 20100308 ,stored exception in log file
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                if (bIsInPrint)
                {
                    bIsInPrint = false;
                    tlbbtn_Print.Enabled = true;
                    Application.DoEvents();
                }
            }

        }

        void webBrowserforPrint_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                WebBrowser wb = sender as WebBrowser;
                if (e.Url.AbsolutePath == wb.Url.AbsolutePath)
                {
                    String htmlName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".mht", "MMddyyyyHHmmssffff");
                    if (File.Exists(htmlName))
                    {
                        File.Delete(htmlName);
                    }

                    gloGlobal.SaveWebPage.SaveWebPageAsMHT(wb, htmlName);

                    bool res = CopyPrintMHT(htmlName);
                    if (!res)
                    {
                        MessageBox.Show("Unable print document using gloLDS service.", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    wb.Stop();
                    wb.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
            }
            finally
            {
                bIsInPrint = false;
                tlbbtn_Print.Enabled = true;
                Application.DoEvents();
            }
        }

        private static bool _isTSPrinterSelectionOpen = false;
        public static bool CopyPrintMHT(String outputFile)
        {
            try
            {
                if (outputFile != null)
                {
                    if (gloGlobal.gloTSPrint.isMapped())
                    {
                        String newZipName;
                        try
                        {
                            newZipName = gloGlobal.gloTSPrint.ZipMyFile(outputFile);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                            ex = null;
                            newZipName = outputFile;
                        }
                        
                        List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> SplitDocList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();

                        gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                        physicalDoc.PdfFileName = newZipName;
                        physicalDoc.SrcFileName = newZipName;
                        physicalDoc.footerInfo = null;
                        SplitDocList.Add(physicalDoc);

                        gloClinicalQueueGeneral.QueueDocumentDocumentDetails _popUpDetails = null;

                        if (!gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false))
                        {
                            if (_isTSPrinterSelectionOpen)
                            {
                                return true;
                            }
                            _isTSPrinterSelectionOpen = true;
                            gloPrintDialog.frmTSPrintDialog tsPrintDialog = new gloPrintDialog.frmTSPrintDialog();
                            tsPrintDialog.ShowDialog();
                            _isTSPrinterSelectionOpen = false;
                            if (tsPrintDialog.cancelPirnt == true)
                            {
                                return true;
                            }
                            _popUpDetails = new gloClinicalQueueGeneral.QueueDocumentDocumentDetails();
                            _popUpDetails.PrintFrom = tsPrintDialog.pageFrom;
                            _popUpDetails.PrintTo = tsPrintDialog.pageTo;
                            _popUpDetails.Printer = tsPrintDialog.currPrinterFile;
                            _popUpDetails.Copies = tsPrintDialog.NoOfCopies;
                            _popUpDetails.Landscape = tsPrintDialog.isLandscape;
                            _popUpDetails.Duplex = tsPrintDialog.duplex;
                            _popUpDetails.Size = tsPrintDialog.currSize;
                            _popUpDetails.Tray = tsPrintDialog.currTray;
                            _popUpDetails.isCollete = tsPrintDialog.isCollete;
                        }

                        String strMetaDataFilePath = "";
                        Boolean MetaDataGenerated = false;
                        if (!gloGlobal.gloTSPrint.UseZippedMetadata)
                        {
                            strMetaDataFilePath = gloGlobal.gloTSPrint.TempPath + "01" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml";
                            MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, popUpDetails: _popUpDetails,sClaimType:"emdeon", bUseFileZip: false);
                        }
                        if (MetaDataGenerated || gloGlobal.gloTSPrint.UseZippedMetadata)
                        {
                            if (MetaDataGenerated)
                            {
                                gloAuditTrail.gloAuditTrail.PrintLog(strException: "MetaData file generated for emdeon Printing.", ShowMessageBox: false);
                            }
                            String PDFWithoutPath = "";
                            bool First = true;
                            for (int fileCntr = 0; fileCntr <= SplitDocList.Count - 1; fileCntr++)
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
                                        strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath + "\\01" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xmz";
                                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList,  popUpDetails: _popUpDetails,sClaimType:"emdeon", bUseFileZip: true);
                                    }
                                    First = false;
                                }
                            }
                            gloAuditTrail.gloAuditTrail.PrintLog(strException: "MHT and MetaData files copied to virtual drive for Emdeon Printing.", ShowMessageBox: false);
                            return true;
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.PrintLog(strException: "Error in MetaData file generation for emdeon Printing.", ShowMessageBox: false);
                            return false;
                        }
                    }
                    else
                    {
                         MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP.", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                         gloAuditTrail.gloAuditTrail.PrintLog(strException: "Mapped drive not found. Document Not Printed", ShowMessageBox: false);
                         return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
                return false;
            }
            finally
            {
                gloGlobal.gloTSPrint.SetTestPatient();
            }
        }

        private static bool GenerateMetaDataFile(string strFilePath, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> PhysicalFile, gloClinicalQueueGeneral.QueueDocumentDocumentDetails popUpDetails,String sClaimType, Boolean bUseFileZip = false)
        {
            gloClinicalQueueGeneral.gloQueueMetadatawriter _QueueWriter = new gloClinicalQueueGeneral.gloQueueMetadatawriter();
            gloClinicalQueueGeneral.Queue QueueDoc = null;
            try
            {
                //Dim strFilePath As String = GenerateClinicalChartFileName(ds, 0, True)

                QueueDoc = _QueueWriter.GenerateWordMetaDataFile(gloGlobal.gloTSPrint.PatientName, gloGlobal.gloTSPrint.PatientDOB, gloGlobal.gloTSPrint.AddFooterInService, PhysicalFile, strFilePath.Substring(strFilePath.LastIndexOf("\\") + 1), claimType: sClaimType, popUpDetails: popUpDetails);
                try
                {
                    gloQueueSchema.gloSerialization.SetClinicalDocument(strFilePath, QueueDoc, bUseFileZip);
                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                    ex = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
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

        public void Delete_LabOrder(Int64 Orderid)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nOrderid";
                oParamater.Value = Orderid;
                oDB.DBParametersCol.Add(oParamater);

                oDB.Delete("gsp_DeleteLabOrder");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((oParamater != null))
                {                    
                    oParamater = null;
                }
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }

        }
        public void Delete_LabTests(Int64 Orderid,string sTestID)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nOrderid";
                oParamater.Value = Orderid;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nTestIds";
                oParamater.Value = sTestID;
                oDB.DBParametersCol.Add(oParamater);              


                oDB.Delete("gsp_DeleteSelectiveTests");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((oParamater != null))
                {
                    oParamater = null;
                }
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }

        }

        private void LoadPatientStrip()
        {   // By Abhijeet Farkande On Date 20100308 ,added try catch & stored exception in log file
            try
            {
                oPatientStrip = new gloUserControlLibrary.gloUC_PatientStrip();
                oPatientStrip.Dock = DockStyle.Top;
                oPatientStrip.Padding = new Padding(3, 0, 3, 0);
                this.Controls.Add(oPatientStrip);
                pnlToolStrip.SendToBack();
                pictureBoxWait.BringToFront();
                oPatientStrip.DTPEnabled = false;
                oPatientStrip.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, 0, 0, false, false, false, "", false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmEmdeonInterface_Load(object sender, EventArgs e)
        {
            // By Abhijeet Farkande On Date : 20100224
            // Added Try catch block
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            ShowTaskInfo();
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _PatientID, DatabaseConnectionString, gstrMessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            try
            {
                //added ordering provider selection on emdeon form and compareprovider logic moved to emdeon form load.
               //cmbProvider.SelectedIndexChanged -= new EventHandler(cmbProvider_SelectedIndexChanged);
               if (LoginProviderID == 0)
               {
                   _ProviderID = oPatientStrip.ProviderID;
               }
               else
               {
                   _ProviderID = LoginProviderID;
               }
                if (_OrderRefrenceId!="" && _OrderId >0)
                {
                    SetValueOrderNotCPOE(_OrderId);
                    LauchOrderEditScreen();
                }
                else if (_SelectedOrderID > 0)
                {
                    SetValueOrderNotCPOE(_SelectedOrderID);
                    LaunchEmdeonScreen();
                }
                else
                {
                    if (GetIsCPOEOrder() > 0)
                    { chkCPOE.Checked = false; }
                    else
                    { chkCPOE.Checked = true; }

                    LaunchEmdeonScreen();//Added by madan on 20100513
                }
                //FillProviders();

                //if (!compareProvider(_ProviderID))
                //{
                //    return;
                //}
                
                //cmbProvider.SelectedIndexChanged += new EventHandler(cmbProvider_SelectedIndexChanged);

                tlbbtn_Print.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error during launching Emdeon : " + ex.ToString(), false);
            }
        }

        private int GetIsCPOEOrder()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParam = null;
            DataTable dt = null;
            int IsCPOEOrder = 0;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_IsCPOEOrder", oParam, out dt);
                oDB.Disconnect();

                if (dt.Rows.Count > 0)
                {
                    IsCPOEOrder = (int)dt.Rows[0][0];
                }

                return IsCPOEOrder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if ((oParam != null)) { oParam.Dispose(); oParam = null; }
                if ((oDB != null)) { oDB.Dispose(); oDB = null; }
                if ((dt != null)) { dt.Dispose(); dt = null; }
            }
        }

        //private bool compareProvider(Int64 _ProviderID)
        //{
        //    string strProviderName = string.Empty;
        //    string strLoginUserName = string.Empty;
        //    string strLabID = string.Empty;
        //    clsGeneral objclsgeneral=null;
        //    try
        //    {
        //        objclsgeneral = new clsGeneral();
        //        if (_ProviderID != 0)
        //        {
        //            strProviderName =objclsgeneral.GetProviderName(_ProviderID, _ClinicID);
        //        }
                
               
        //            strLabID = objclsgeneral.GetProvidergloLabId(_ProviderID );
        //            if (objclsgeneral.ConfirmNull(strLabID.ToString()))
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                if (MessageBox.Show("The current provider '" + strProviderName + " ' does not have a lab ID set up.\r\n"
        //                   + "If you place a lab order, you will have to select a provider in the labs interface.\r\n"
        //                   + "Would you like to proceed with the lab order? \r\n\r\n"
        //                   + "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Placing external lab order even patient provider does not have LabId", _PatientID, 0, _ProviderID , gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
        //                    return true;
        //                }
        //                else
        //                {
        //                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Placing external lab Order cancled by user", _PatientID, 0, _ProviderID , gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
        //                    _IsFormClose = true;
        //                    this.Close();
        //                    return false;
        //                }
        //            }
                   
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        return false;
        //    }

        //}
        //private void FillProviders()
        //{
        //    clsGeneral _objclsgeneral = new clsGeneral();
        //    DataTable dtprovider;
        //    dtprovider = _objclsgeneral.GetProviders();
        //    if (dtprovider != null)
        //    {
        //        if (dtprovider.Rows.Count > 0)
        //        {
        //            cmbProvider.DataSource = dtprovider.DefaultView;
        //            cmbProvider.ValueMember = dtprovider.Columns["nProviderID"].ColumnName;
        //            cmbProvider.DisplayMember = dtprovider.Columns["ProviderName"].ColumnName;

        //            cmbProvider.SelectedValue = _ProviderID;
                   
                   
        //        }
        //    }
        //    if (_objclsgeneral != null)
        //    {
        //        _objclsgeneral.Dispose();
        //        _objclsgeneral = null;
        //    }
            

        //}
        private void SetValueOrderNotCPOE(Int64 intOrderId)
        {
            string strQuery = string.Empty;
            DataTable dtResult = new DataTable();

            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                objDbLayer.Connect(false);
                strQuery = " SELECT isnull(blnOrderNotCPOE,0) as blnOrderNotCPOE,isnull(labom_ProviderID,0) AS labom_ProviderID from Lab_Order_MST WHERE labom_OrderID = " + intOrderId;

                objDbLayer.Retrive_Query(strQuery, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    chkCPOE.Checked = Convert.ToBoolean(dtResult.Rows[0]["blnOrderNotCPOE"].ToString());
                    _ProviderID = Convert.ToInt64 (dtResult.Rows[0]["labom_ProviderID"]);
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                { objDbLayer.Dispose(); objDbLayer = null; }

                if (dtResult != null)
                { dtResult.Dispose(); dtResult = null; }

                strQuery = null;
            }
        }

        //Developer: Sanjog Dhamke
        //Date:10 Dec 2011
        //Bug ID/PRD Name/Salesforce Case: Lab Usability PRD Show Task Information on Emdeon Lab 
        //Reason: To show task info

        private void ShowTaskInfo()
        {
            if (nTaskID > 0)
            {
                pnlTaskControl.Visible = true;
                gloUC_TaskInfo oUC = new gloUC_TaskInfo(nTaskID);
                oUC.Padding = new Padding(3, 0, 3, 0);
                oUC.Dock = DockStyle.Fill;
                oPatientStrip.DTPEnabled = false;
                pnlTaskControl.Controls.Add(oUC);
            }
            else
            {
                if (TestsName.Trim() != "")  // added to show testnames on EmdeonScreen ,v8022
                {
                    pnlTaskControl.Visible = true;
                    gloUC_TaskInfo oUC = new gloUC_TaskInfo(-1,TestsName,strDiag );
                    oUC.Padding = new Padding(3, 0, 3, 0);
                    oUC.Dock = DockStyle.Fill;
                   
                    if (strDiag.Trim() == "")   //if diagnosis is not there then changing height of panel
                    {

                        pnlTaskControl.Height = 78;
                    }
                    oPatientStrip.DTPEnabled = false;
                    pnlTaskControl.Controls.Add(oUC);
                }
  
            }
        }

        private void webBrowserEmdeon_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            try
            {
                if (webBrowserEmdeon.ReadyState == WebBrowserReadyState.Interactive)
                {                   
                    pictureBoxWait.Visible = false;
                   
                    if (!_orderIsPlaced)
                    {
                        _orderTimer.Enabled = true;
                        _orderTimer.Start();
                    }
                    else
                    {
                        DisableOrderTimer();
                    }
                    //Added by madan on 20100510
                    tmr_session.Stop();
                    _sessionTimerCount = 0;
                    tmr_session.Enabled = true;
                    tmr_session.Start();
                }
                else if (webBrowserEmdeon.ReadyState == WebBrowserReadyState.Complete && webBrowserEmdeon.Url.AbsolutePath.Contains("RequisitionPrintFrame.jsp"))
                {
                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (!tlbbtn_Print.Enabled)
                        {
                            tlbbtn_Print.Enabled = true;
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        //Added by madan to launch emdoen Screeen-- on 20100513
        private void LaunchEmdeonScreen()
        {
            try
            {
                 gloPatient.Patient objpatient = new gloPatient.Patient();
                gloPatient.gloPatient objgloPatient = new gloPatient.gloPatient(DatabaseConnectionString);
                objpatient = objgloPatient.GetPatient(_PatientID);

                if (clsEmdeonGeneral.gloLab_BillingType == "p")
                {
                    SelectGuarantor(objpatient);
                }
                if (clsEmdeonGeneral.gloLab_BillingType == "c")
                {
                    objpatient.PatientGuarantors.Clear();
                }

                clsgloLabOrderSummary objClsOrderSum = new clsgloLabOrderSummary();
                string _patientInfo = objClsOrderSum.GetPatientInformation(objpatient, _PatientID);
                Application.DoEvents();
                string strpDOB = FormateBirthDate(oPatientStrip.PatientDateOfBirth);
                if (strpDOB != string.Empty && _patientInfo != "")
                {
                    URl1 = clsEmdeonGeneral.emdeonURL + "/servlet/DxLogin?userid=" + clsEmdeonGeneral.emdeonUserName.ToString() + "&PW=" + clsEmdeonGeneral.emdeonUserPassword.ToString();
                    URl2 = "&target=jsp/lab/order/ESummaryOrder.jsp&apiLogin=true&hdnBusiness=" + clsEmdeonGeneral.emdeonFacilityCode.ToString() + "&actionCommand=EOrderSummary" + _patientInfo + "&O_Oper=" + clsEmdeonGeneral.emdeonUserName.ToString() + "&O_Type=E&S_HEADER=true";
                    URl = URl1 + URl2;

                    //****************************************************
                    //////Maximum URL Length...Although the specification of the HTTP protocol does not specify any maximum length, 
                    // practical limits are imposed by web browser and server software. 
                    ///Microsoft IE :2083 characters.
                    /// Mozilla FireFox:65,536 characters
                    /// Safari (Browser):80,000 characters
                    /// Opera (Browser):190,000 characters 
                    /// Apache (Server):4,000 characters, after which Apache produces a "413 Entity Too Large" error
                    /// Microsoft Internet Information Server:16,384 characters 
                    ///*******************************************************
                    int _count = URl.Length;
                    if (_count < 2035)
                    {
                        _Uri = new System.Uri(URl);
                        webBrowserEmdeon.Url = _Uri;

                        //by Abhijeet on 20100429                        
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "External lab interface launched", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                    else
                    {
                        //by Abhijeet on 20100429                       
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Unable to lunch external lab interface because of Url length exceeded maximum limit", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error during launching Emdeon : " + ex.ToString(), false);
            }
        }

        public int SelectGuarantor(gloPatient.Patient oPat)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            DataTable dtGuarantors=new DataTable();
            Int64 _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            Int64 nPAcnt = 0;
            try
            {
                if (oPat.PatientAccounts.Count > 0)
                {
                    nPAcnt = clsEmdeonGeneral.gloLab_BillGuarantorAcntID;
                }
                    if (nPAcnt > 0)
                    {
                        string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                        + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                        + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                        + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                        + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                        + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                        + " FROM Patient_OtherContacts WHERE ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId =" + nPAcnt + " and bIsAccountGuarantor = 1 ORDER BY nPatientContactTypeFlag";
                        oDB.Connect(false);
                        oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);
                        if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                        {
                            oPat.PatientGuarantors.Clear();
                            for (int i = 0; i < dtGuarantors.Rows.Count; i++)
                            {
                                gloPatient.PatientOtherContact oGuarantor = new gloPatient.PatientOtherContact();

                                //Account guarantor
                                oGuarantor.PatientID = _PatientID;
                                oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantors.Rows[i]["nPatientContactID"]);
                                oGuarantor.FirstName = Convert.ToString(dtGuarantors.Rows[i]["sFirstName"]);
                                oGuarantor.MiddleName = Convert.ToString(dtGuarantors.Rows[i]["sMiddleName"]);
                                oGuarantor.LastName = Convert.ToString(dtGuarantors.Rows[i]["sLastName"]);
                                if (dtGuarantors.Rows[i]["nDOB"] != null && dtGuarantors.Rows[i]["nDOB"].ToString() != "")
                                {
                                    oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[i]["nDOB"]));
                                }
                                oGuarantor.SSN = Convert.ToString(dtGuarantors.Rows[i]["sSSN"]);
                                if (Convert.ToInt64(dtGuarantors.Rows[i]["nPatientID"]) == _PatientID)
                                {
                                    oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                                }
                                    else
                                {
                                    oGuarantor.Relation = "Other";
                                }

                                oGuarantor.Gender = Convert.ToString(dtGuarantors.Rows[i]["sGender"]);
                                oGuarantor.AddressLine1 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine1"]);
                                oGuarantor.AddressLine2 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine2"]);
                                oGuarantor.City = Convert.ToString(dtGuarantors.Rows[i]["sCity"]);
                                oGuarantor.State = Convert.ToString(dtGuarantors.Rows[i]["sState"]);

                                oGuarantor.County = Convert.ToString(dtGuarantors.Rows[i]["sCounty"]);
                                oGuarantor.Country = Convert.ToString(dtGuarantors.Rows[i]["sCountry"]);

                                oGuarantor.Zip = Convert.ToString(dtGuarantors.Rows[i]["sZIP"]);
                                oGuarantor.Phone = Convert.ToString(dtGuarantors.Rows[i]["sPhone"]);
                                oGuarantor.Mobile = Convert.ToString(dtGuarantors.Rows[i]["sMobile"]);
                                oGuarantor.Email = Convert.ToString(dtGuarantors.Rows[i]["sEmail"]);
                                oGuarantor.Fax = Convert.ToString(dtGuarantors.Rows[i]["sFax"]);
                                oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsActive"]);
                                oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[i]["nVisitID"]);
                                oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[i]["nAppointmentID"]);
                                oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                                oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                                oGuarantor.OtherConatctType = (gloPatient.PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactType"]);
                                oGuarantor.PAccountID = Convert.ToInt64(oPat.PatientAccounts[0].PAccountID);
                                oGuarantor.GurantorType = (gloPatient.GuarantorType)Convert.ToInt32(dtGuarantors.Rows[i]["nGuarantorType"]);
                                oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsAccountGuarantor"]);
                                oPat.PatientGuarantors.Add(oGuarantor);
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Patient is not associated with an Account.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
           
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in SelectGuarantor : " + ex.ToString(), false);
            }
            finally
            {
                if (dtGuarantors != null)
                {
                    dtGuarantors.Dispose();
                    dtGuarantors = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB=null;
                }

            }
            return 0;
        }


        private string FormateBirthDate(DateTime PatientBirthDate)
        {
            string FormatedBirthDate = string.Empty;
            int Month = PatientBirthDate.Month;
            int Day = PatientBirthDate.Day;
            int Year = PatientBirthDate.Year;
            try
            {
                if (Month < 10 && Day < 10)
                {
                    FormatedBirthDate = "0" + Month.ToString() + "/0" + Day.ToString() + "/" + Year.ToString();
                }
                else if (Month < 10 && Day > 10)
                {
                    FormatedBirthDate = "0" + Month.ToString() + "/" + Day.ToString() + "/" + Year.ToString();
                }
                else if (Month > 10 && Day < 10)
                {
                    FormatedBirthDate = Month.ToString() + "/0" + Day.ToString() + "/" + Year.ToString();
                }
                else
                {
                    FormatedBirthDate = Month.ToString() + "/" + Day.ToString() + "/" + Year.ToString();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            return FormatedBirthDate;
        }
        //Added by madan on 20100504
        private void frmEmdeonInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_IsFormClose==false)
                {
                   // _IsFormClose = true;
                    if (_orderIsPlaced)
                    {                      

                        if (_OrderId > 0 && _OrderRefrenceId != "")
                        {
                            _IsFormClose = true;
                            objclsGetGloLabData.ModifiyOrderId = _OrderId;
                            objclsGetGloLabData.ModifiyOrderReferenceId = _OrderRefrenceId;
                            objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Selected);
                            _IsOrderEdited = true;
                            e.Cancel = false;
                        }
                        else
                        {
                            
                            //Modified as per "DREW NALON" Comments by madan on 20100515
                            DialogResult drMesgResult = MessageBox.Show("You have already placed a lab order in Emdeon.Would you like to save the order to the EMR? \r\n"
                                                                         + "If you do not save the order now, the order will be downloaded the next time an order is saved.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (drMesgResult == DialogResult.No)
                            {
                                _IsFormClose = true;
                                LogOutBrowserSession();
                                //by Abhijeet on 20100429                           
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                e.Cancel = false;
                            }
                            else
                            {
                                _IsFormClose = true;
                                LogOutBrowserSession();
                                objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Duration);

                                _IsOrderEdited = true;
                                //by Abhijeet on 20100429                           
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                e.Cancel = false;
                            }
                        }
                    }
                    else
                    {
                        if (_OrderId > 0 && _OrderRefrenceId != "")
                        {
                            if (MessageBox.Show("Are you sure you want to exit without editing a lab order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _IsFormClose = true;
                                LogOutBrowserSession();
                                //by Abhijeet on 20100429                           
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, _OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                this.Close();
                            }
                            else
                            {
                                _IsFormClose = false;
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            //Modified as per "DREW NALON" Comments by madan on 20100515
                            if (MessageBox.Show("Are you sure you want to exit without placing a lab order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _IsFormClose = true;
                                LogOutBrowserSession();
                                //by Abhijeet on 20100429                           
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                this.Close();
                            }
                            else
                            {
                                _IsFormClose = false;
                                e.Cancel = true;
                            }
                        }

                    } 
                }
                //LogOutBrowserSession();
                if (e.Cancel==false)
                {
                    _orderTimer.Stop();
                    _orderTimer.Dispose();
                    tmr_session.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);               
            }
          
        }
        private void LogOutBrowserSession()
        { // Comment by Abhijeet on 20100514
          // called this method always before 'from close' event   
            try
            {
                //Problem : 00000229
                //Issue : gloEMR crashes after placing the emdeon lab order.
                //Change : if web browser is loading then avoid logout. logout URL is changed. BaseURL is modified as per Clinician Web Services API document.
                if (webBrowserEmdeon.ReadyState == WebBrowserReadyState.Loading) { return; }

                _strLogout = "https://cli-cert.emdeon.com/servlet/lab.security.DxLogout?userid=" + clsEmdeonGeneral.emdeonUserName.ToString() + "&BaseUrl=" + "https://cli-cert.emdeon.com" + "&LogoutPath=/html/AutoPrintFinished.html";

                _Uri = new System.Uri(_strLogout);

                webBrowserEmdeon.Url = _Uri;
                //Added by madan.. on 20100510              

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _Uri = null;
            }
        }

        //Added by madan on 2010511
        private void SetupOrderTimer()
        {
            try
            {
                if (_orderTimer == null)
                {
                    _orderTimer = new System.Timers.Timer();
                    _orderTimer.Interval = 1000;
                    _orderTimer.Elapsed += new System.Timers.ElapsedEventHandler(Ordertimer_Elapsed);
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        private void DisableOrderTimer()
        {
            try
            {
                _orderTimer.Enabled = false;
                _orderTimer.Stop();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        //Added by madan on 2010511
        void Ordertimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strResultURL = string.Empty;
            try
            {
                _orderTimer.Enabled = false;

                strResultURL = Convert.ToString(webBrowserEmdeon.Url.AbsoluteUri);
              
                if (strResultURL.Contains("RequisitionPrintFrame"))
                {
                    _orderIsPlaced = true;
                }

                if (_orderIsPlaced == false)
                {
                    _orderTimer.Enabled = true; 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                strResultURL = string.Empty;
            }
            finally
            {
                if (_orderIsPlaced == true)
                {
                    DisableOrderTimer();                    
                }
            }
        }        
       
        //Added by madan on 2010511
        private void tmr_session_Tick(object sender, EventArgs e)
        {
            try
            {
                _sessionTimerCount += 1;
                if (_sessionTimerCount == 20)
                {
                    _orderTimer.Stop();
                    _orderTimer.Enabled = false;
                    tmr_session.Enabled = false;
                    tmr_session.Stop();
                    _msgBoxActivated = true;
                    if (clsEmdeonGeneral.IsLockScreenActivated == false)
                    {
                        LoadSessionMsgBox();                      
                    }
                    else
                    {
                        _sessionTimerCount = 0;
                    }                  
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        //added by madan on 20100513
        private void LoadSessionMsgBox()
        {
            _msgBoxActivated = false;
            if (_orderIsPlaced)
            {
                if (_OrderId >0 && _OrderRefrenceId !="")
                {
                    _IsFormClose = true;
                    objclsGetGloLabData.ModifiyOrderId = _OrderId;
                    objclsGetGloLabData.ModifiyOrderReferenceId = _OrderRefrenceId;
                    objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Selected);
                    _IsOrderEdited = true;
                    this.Close();

                }
                else
                {

                    _IsFormClose = true;
                    DialogResult drMesgResult = MessageBox.Show("You have already placed a lab order in Emdeon.Would you like to save the order to the EMR? \r\n"
                                                                         + "If you do not save the order now, the order will be downloaded the next time an order is saved.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drMesgResult == DialogResult.Yes)
                    {
                        LogOutBrowserSession();
                        objclsGetGloLabData.GetAllLatestOrder(_PatientID, DatabaseConnectionString, clsGetGloLabData.RetrievePatientOrdersType.Duration);
                        //by Abhijeet on 20100429                           
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        _IsOrderEdited = true;
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                if (_OrderId > 0 && _OrderRefrenceId != "")
                {
                    if (MessageBox.Show("Session has been expired,Do you want to proceed editing orders in Labs.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sessionTimerCount = 0;
                        //by Abhijeet on 20100429                           
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Open, "Requested for External lab interface to relaunch because of session timeout while modifying order", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        LaunchEmdeonScreen();
                    }
                    else
                    {
                        //by Abhijeet on 20100429                           
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed by user due to session timeout while modifying order", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        this.Close();
                    }
                }
                else
                {

                    if (MessageBox.Show("Session has been expired,Do you want to proceed placing orders in Labs.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sessionTimerCount = 0;
                        //by Abhijeet on 20100429                           
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Open, "Requested for External lab interface to relaunch because of session timeout while creating order", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        LaunchEmdeonScreen();
                    }
                    else
                    {
                        //by Abhijeet on 20100429                           
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "External lab interface closed by user due to session timeout while creating order", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        this.Close();
                    }
                }
            }
 
        }
        //Added by madan on 20100513
        private void frmEmdeonInterface_Activated(object sender, EventArgs e)
        {
            if (_msgBoxActivated)
            {
                if (clsEmdeonGeneral.IsLockScreenActivated==false)
                {
                    LoadSessionMsgBox();                    
                }
                
            }
        }
        //Added by madan on 20100610

        private void LauchOrderEditScreen()
        {
            clsgloLabPatientLayer objClsgloLabPatientLayer = new clsgloLabPatientLayer();           
            try
            {

                if (_OrderRefrenceId != null && _OrderRefrenceId.ToString().Trim().Length != 0 && _OrderRefrenceId.ToString() != "")
                {
                    URl2 = clsEmdeonGeneral.emdeonURL + "/servlet/DxLogin?userid=" + clsEmdeonGeneral.emdeonUserName + "&PW=" + clsEmdeonGeneral.emdeonUserPassword + "&target=servlet/servlets.apiOrderServlet&apiuserid="+clsEmdeonGeneral.emdeonUserName+ "&actionCommand=EditSummaryOrder&orderid=" +_OrderRefrenceId.ToString();
                    _Uri = new System.Uri(URl2);

                    webBrowserEmdeon.Url = _Uri;

                    //by Abhijeet on 20100619                        
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Modify, "Lunched external lab interface for modifying order", _PatientID, _OrderId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmEmdeonInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Problem : 00000229
            //Issue : gloEMR crashes after placing the emdeon lab order.
            //Change : Set the cursor to default.
            this.Cursor = Cursors.Default;
            // if the Locked by by the Current User & on Current Machine only
            clsGeneral objClsGeneral = new clsGeneral();
            try
            {
                if (clsEmdeonGeneral.gloLab_IsOrderLocked && _OrderId >0)
                {
                    objClsGeneral.UnLockRecords(clsGeneral.TrnType.Labs,_OrderId, 0, DateTime.Now);
                    clsEmdeonGeneral.gloLab_IsOrderLocked = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objClsGeneral != null)
                {
                    objClsGeneral.Dispose();
                }
                //Bug #48804: 00000433 : Lab Order
                if (webBrowserEmdeon != null)
                {
                    webBrowserEmdeon.Stop();
                    webBrowserEmdeon.Dispose();
                }
                this.Close();
            }
        }

        //private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbProvider.SelectedValue != null)
        //    {
        //        _ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
        //        if (!compareProvider(_ProviderID))
        //        {
        //            return;
        //        }

              
                
        //    }
        //} 
    }
}