

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Threading;
using gloPrintDialog;
//using gloEMR.gloEMRWord;
using System.Data.SqlClient;
using Wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.InteropServices;
//using gloWord;
//using gloEMRGeneralLibrary.gloEMRDatabase;
namespace gloOffice
{
    public partial class frmgloPrintWordProgressController : Form
    {
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);
        //    private int _nPagesCnt = 0;
     //   private int _printPageIndex = 0;
      //  private bool _IsOnPagePDFDocCreated = false;
        //   Private _gloStandardPrintController As gloStandardPrintController = Nothing
     //   private Microsoft.Office.Interop.Word.Application oWordApp;
      //  private String _PDFFileName = null;
        // Private _oPrintDocument As PrintDocument = Nothing
        private PrinterSettings _PrinterSetting = null;

        private gloExtendedPrinterSettings _ExtendedPrinterSettings = null;
        public bool IsPrintingResumed = false;

        public bool IsPrintingCanceled = false;
        private bool IsPrintingCompletedWhilePressingPause = true;
      //  private ArrayList _objwordList = null;
        public DataTable dtSelectPatient = null;
        public Dictionary<Int64, string> dictPatientLetter;
      //  bool blntaskidPres = false;
        public bool ChkRemiderForUnSchedle = false;
        public static bool blnBackGroundPrint = false;
      //  private Microsoft.Office.Interop.Word.Document oCurDoc;
        private int PrintDocument = 0;
        public string oldPrinterName = "";
        //    private Int64 _PatientID = 0;
    //    private string _PatientName = "";
    //    private Int64 _TemplateID = 0;
     //   private string _TemplateName = "";
    //    private Int64 _VisitID = 0;
    //    private Int64 _TaskID = 0;
        public bool _bIsUnscheduledCare = false;
      //  private Int64 _nCommunicationTypeID = 0;
        private DateTime clickTime = DateTime.Now;
        private bool blnbtnPauseclicked = false;
       public long AccountID=0;
        private bool _isFormClose = false;
        //  clsPatientLetters ocls = new clsPatientLetters();
        public List<gloTemplate> lstgloTemplate = null;
        public bool blnPrintgloTemplatesonly = false;
        gloWord.LoadAndCloseWord myLoadWord =null;
        private gloClinicalQueueGeneral.QueueDocumentDocumentDetails _popUpDetails = null;
        Microsoft.Office.Interop.Word.WdStatistic PagesCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
        private System.Guid strBackgroundPrintReportGUID;
        public   struct PatientMessage
        {
            public Int64 PatientID;
            public StringBuilder Message;
        }
      public   List<PatientMessage> oPatientMessages = new List<PatientMessage>();
      public string _databaseConnectionString = "";
        private Control myCaller = null;
        public frmgloPrintWordProgressController(Control myControl = null)
        {
         
            Shown += frmgloPrintWordProgressController_Shown;
            FormClosed += frmgloPrintWordProgressController_FormClosed;
            myCaller = myControl;
           
            InitializeComponent();

        }
        public frmgloPrintWordProgressController(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, Control myControl = null)
        {
            //   Shown += frmgloPrintWordProgressController_Shown;
            //  FormClosed += frmgloPrintWordProgressController_FormClosed;
           
            myCaller = myControl;
            gloPrintWordProgressControllerCall(sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages);

        }



        internal void gloPrintWordProgressControllerCall(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null)
        {
            try
            {
                InitializeComponent();
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    this.Text = "Copying for Printing";
                }
                else
                {
                    this.Text = "Printing";
                }
                //' Me.ControlBox = False
                this.BringToFront();
                _PrinterSetting = sPrinterSettings;
                _ExtendedPrinterSettings = sExtendedPrinterSettings;
                if (gloGlobal.gloTSPrint.isCopyPrint && (! gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:false)))
                {
                    _ExtendedPrinterSettings.IsBackGroundPrint = true;
                    _ExtendedPrinterSettings.IsShowProgress = false;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }

        const int CP_NOCLOSE_BUTTON = 512;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        private void frmgloPrintWordProgressController_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            try
            {
                methodToInvoke = null;
                gloGlobal.LoadFromAssembly.PrintMenuEventclick(false, "frmgloPrintWordProgressController");

                //dynamic frm = Application.OpenForms["frmDashBoardMain"];
                //if (frm != null)
                //{
                //    frm.PrintMenuEventclick(false , "frmgloPrintWordProgressController");
                //}

                //if (((myCaller == null) == false))
                //{
                //    ((MainMenu)myCaller).PrintMenuEventclick(false);
                //}

                if ((myLoadWord != null))
                {
                    myLoadWord.CloseApplicationOnly();
                    myLoadWord = null;
                }
                //if ((dtSelectPatient != null))
                //{
                //    dtSelectPatient.Dispose();
                //    dtSelectPatient = null;
                //}
                //if ((dictPatientLetter != null))
                //{
                //    dictPatientLetter.Clear();
                //    dictPatientLetter = null;
                //}
                blnBackGroundPrint = false;
                //if ((ocls != null))
                //{
                //    ocls.Dispose();
                //    ocls = null;
                //}
                if (lstgloTemplate != null)
                {
                    foreach (gloTemplate Template in lstgloTemplate)
                    {
                        if (System.IO.File.Exists(Template.TemplateFilePath))
                        {
                            System.IO.File.Delete(Template.TemplateFilePath);
                        }
                    }
                    lstgloTemplate.Clear();
                    lstgloTemplate = null;
                }

                if (oPatientMessages != null)
                {
                    oPatientMessages.Clear();
                    oPatientMessages = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
            finally
            {
                try
                {
                   
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (oldPrinterName != String.Empty)
                        {
                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName);
                            Application.DoEvents();
                        }
                    }
                }
                catch
                {
                }
            }
        }


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private void frmgloPrintWordProgressController_Shown(object sender, EventArgs e)
        {
            try
            {
                //Int64 cntrec = 0;

                //  dynamic frm = Application.OpenForms["frmDashBoardMain"];
                // if (frm != null)
                // {
                //    frm.PrintMenuEventclick(true, "frmgloPrintWordProgressController");
                // }
               
                myLoadWord = new gloWord.LoadAndCloseWord();
                gloGlobal.LoadFromAssembly.PrintMenuEventclick(true, "frmgloPrintWordProgressController");

                //if ((dtSelectPatient != null))
                //{
                //    cntrec = dtSelectPatient.Rows.Count;
                //}
                //if ((dictPatientLetter != null))
                //{
                //    cntrec = cntrec * dictPatientLetter.Count;
                //}
                if (lstgloTemplate != null)
                {
                    if (lstgloTemplate.Count > 0)
                    {
                        pbDocument.Maximum = lstgloTemplate.Count;
                    }
                }
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    lblPrinterNameValue.Text = "To Network Drive";
                }
                else
                {
                    lblPrinterNameValue.Text = _PrinterSetting.PrinterName;
                }
                if (_ExtendedPrinterSettings.IsShowProgress)
                {
                    PrintWithOrWithoutBackground();
                }
                else
                {
                    Hide();
                    PrintWithOrWithoutBackground();
                }

                if (this.Visible == true)
                {
                    //if (((myCaller == null) == false))
                    //{
                    //    ((MainMenu)myCaller).PrintMenuEventclick(true);
                    //}


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
            finally
            {
                if (myCaller != null)
                {
                    try
                    {
                        myCaller.Focus();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private delegate void del();
        private EventHandler methodToInvoke;
        public void PrintWithOrWithoutBackground()
        {
            try
            {
                if (_ExtendedPrinterSettings.IsShowProgress)
                {
                    int retry = 20;
                    while (!IsPrintingCompletedWhilePressingPause)
                    {
                        InvokeBackgroundUpdateControls();
                        Thread.Sleep(100);
                         if (System.Math.Max(System.Threading.Interlocked.Decrement(ref retry), retry + 1) == 0)
                        {
                        //Exhaused waiting for 2 seconds
                           break; // TODO: might not be correct. Was : Exit While
                         }



                    }
                    IsPrintingCompletedWhilePressingPause = false;
                }

                // Show priinter selection popup for gloTS print
                bool res = true;
                _popUpDetails = gloWord.LoadAndCloseWord.getTSPrintDialogDetails(ref res);
                if (!res)
                {
                    IsPrintingCompletedWhilePressingPause = true;
                    InvokeCompleteUpdateControls();
                    return;
                }

                if (_ExtendedPrinterSettings.IsBackGroundPrint)
                {

                    blnBackGroundPrint = true;
                    if ((methodToInvoke != null))
                    {
                        methodToInvoke = null;
                    }
                    methodToInvoke = new EventHandler(this.OnPrint);
                    methodToInvoke.BeginInvoke(this, null, new AsyncCallback(this.OnPrintComplete), null);


                }
                else
                {
                    if (blnPrintgloTemplatesonly == false)
                    {
                        Print();
                    }
                    else
                    {
                        PrintgloTemplatesonly(); 
                    }
                    IsPrintingCompletedWhilePressingPause = true;
                }
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
                if (this.InvokeRequired)
                {
                    this.Invoke(new UpdateBackgroundControlsDelegate(UpdateBackgroundControls));
                }
                else
                {
                    UpdateBackgroundControls();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        int TempPrintDocument = 0;

        private void UpdateBackgroundControls()
        {
            try
            {
                //' lblPageNoOfDocument.Text = "Waiting for Background to Finish Page Number " & PrintDocument.ToString() & " from Document"
                TempPrintDocument = PrintDocument;
                if ((TempPrintDocument == 0))
                {
                    TempPrintDocument = 1;
                }
                lblPages.Text = "Printing " + TempPrintDocument.ToString() + " of " + pbDocument.Maximum.ToString();

            }
            catch
            {
            }
        }


        private void OnPrint(object sender, System.EventArgs e)
        {

            try
            {
                if (blnPrintgloTemplatesonly == false)
                {
                    Print();
                }
                else
                {
                    PrintgloTemplatesonly();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }


        public delegate void UpdateBeginControlsDelegate();
        public void InvokeBeginUpdateControls()
        {
            try
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
                //'
                // update your controls here
                //    lblDocumentName.Text = _oPrintDocument.DocumentName;
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    lblPrinterNameValue.Text = "To Network Drive";
                }
                else
                {
                    lblPrinterNameValue.Text = _PrinterSetting.PrinterName;
                }


                //   pbDocument.Step = 1;
                pbDocument.Minimum = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
       // bool gblnPageNo = true;
    
        
        
        
        
        
        
//        private void PrintOriginal()
//{
//    Int64 fromDoc = 0;
//    bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
//    short Copies = _PrinterSetting.Copies;
//    //'

//    try {


//        foreach (DataRow dr in dtSelectPatient.Rows) {


//            foreach (KeyValuePair<Int64, string> di in dictPatientLetter) {


//                if ((fromDoc < PrintDocument)) {
//                    fromDoc += 1;
//                } else {
//                    //'added for bugid 92668
//                    if ((_isFormClose == true)) {
//                        return;
//                    }
//                    lock (this) {
//                        _PatientID = Convert.ToInt64(dr("PatientID"));
//                        _PatientName = Convert.ToString(dr("Patient Name"));
//                        _TemplateID = di.Key;
//                        _TemplateName = di.Value;

//                        ///'''''cheking if taskid exist or not
//                        if (blntaskidPres == false) {
//                            _TaskID = 0;
//                        } else {
//                            if (Information.IsNumeric(dr("nTaskID"))) {
//                                _TaskID = Convert.ToInt64(dr("nTaskID"));
//                            } else {
//                                _TaskID = 0;
//                            }
//                        }



//                        _VisitID = GenerateVisitID(Now, _PatientID);
//                        _bIsUnscheduledCare = ChkRemiderForUnSchedle;

//                        string strFileName = "";

//                        try {

//                            Fill_TemplateGallery();
//                            strFileName = ExamNewDocumentName;
//                            if ((oCurDoc != null)) {
//                                oCurDoc.SaveAs(strFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, false, "", false);
//                            }

//                        } catch (Exception ex) {
//                        }
//                        if (!string.IsNullOrEmpty(strFileName)) {
//                            if ((myLoadWord == null)) {
//                                return;
//                            }
//                            if ((oCurDoc != null)) {
//                                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc);

//                                if (gblnPageNo == true) {
//                                    UpdateLog("InsertNamePageNo start ");

//                                    if (blnBuildBlockSetting) {
//                                        InsertNamePageNo(oCurDoc, GetPatientDetails(_PatientID));
//                                    } else {
//                                        InsertPageFooterWithoutMSWBuildingBlock(oCurDoc, GetPatientDetails(_PatientID));
//                                    }

//                                    UpdateLog("InsertNamePageNo end");
//                                }
//                                if ((oCurDoc != null)) {
//                                    oCurDoc.Application.ActivePrinter = _PrinterSetting.PrinterName;
//                                    oCurDoc.PrintOut(, , , , , , , Copies);
//                                }
//                            }

//                            if ((ocls != null)) {
//                                if ((ocls.SavePatientLetter(0, _PatientID, _TemplateID, System.DateTime.Now, strFileName, _TemplateName, false, _bIsUnscheduledCare, _nCommunicationTypeID) > 0)) {
//                                    ///'''''Integrated by Chetan  as on 21 oct 2010 - for DM Setup Report
//                                    if (_TaskID != 0) {
//                                        UpdateReminder(_TaskID);
//                                    }

//                                }

//                            }
//                            if (myLoadWord == null) {
//                                return;
//                            }
//                            myLoadWord.CloseWordOnly(oCurDoc);
//                            PrintDocument += 1;
//                            fromDoc = PrintDocument;
//                            InvokeProgressUpdateControls();
//                        }
//                        if (blnbtnPauseclicked) {
//                            InvokeEnableDisablePauseButton();
//                        }
//                        if (IsPrintingResumed == true) {
//                            return;
//                        }
//                    }
//                }


//            }

//        }
//        //'if foreground print is on then closing  form after completing printing
//        if ((blnBackGroundPrint == false) & _isFormClose == false) {
//            _isFormClose = true;
//            this.Close();
//            this.Dispose(true);
//        }
//    } catch (Exception ex) {
//        ex = null;
//    }
//}

        private void AddSkipedTemplateInfo(Int64 PatientID, String PatientName, String TemplateName, String strTime)
        {
            Boolean IspatientExist = false;
            foreach (PatientMessage oMsg in oPatientMessages)
            {
                if (oMsg.PatientID == PatientID)
                {
                    IspatientExist = true;
                    //oMsg.Message.Append(System.Environment.NewLine);
                    //oMsg.Message.Append("              " + TemplateName + "[" + strTime + "]");
                }
            }
            if (IspatientExist == false)
            {
                PatientMessage oMessage = new PatientMessage();
                oMessage.Message = new StringBuilder();
                oMessage.PatientID = PatientID;
                //oMessage.Message.Append("Patient   : " + PatientName);
                oMessage.Message.Append(PatientName);
                //oMessage.Message.Append(System.Environment.NewLine);
                //oMessage.Message.Append("Template : ");
                //oMessage.Message.Append(System.Environment.NewLine);
                //oMessage.Message.Append("              " + TemplateName+"["+ strTime +"]");
                oPatientMessages.Add(oMessage);
            }



        }

       
  private void Print()
{
	Int64 fromDoc = 0;
	//bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
	
	//'
    object Background = false;
    object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
    object Copies =gloGlobal.gloTSPrint.isCopyPrint?(short) 1: _PrinterSetting.Copies;
    object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
    object PrintToFile = false;
    object Collate = gloGlobal.gloTSPrint.isCopyPrint?true:_PrinterSetting.Collate;
    object ActivePrinterMacGX = Type.Missing;
    object ManualDuplexPrint = false;
    object PrintZoomColumn = 1;
    object PrintZoomRow = 1;
    object missing = Type.Missing;
    try
    {
        strBackgroundPrintReportGUID = System.Guid.NewGuid();
        UpdateBackgroundPrintReport(strBackgroundPrintReportGUID, pbDocument.Maximum, "", "", 0, 0); 
  
        if(myLoadWord ==null)
       {
           myLoadWord = new gloWord.LoadAndCloseWord();
       }
       bool DefaultPrinter = false;
       if (appSettings["DefaultPrinter"] != null)
       {
           if (appSettings["DefaultPrinter"] != "")
           { DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]); }
           else { DefaultPrinter = false; }
       }
       else
       { DefaultPrinter = false; }

        Boolean isTemplateSkipped = false;
        object missing_new = Type.Missing;
        object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        StringBuilder strExcludedTemplates = new StringBuilder();
        // strExcludedTemplates.Append("Following patient(s) will be excluded from batch printing as they are having multiple accounts :");
        strExcludedTemplates.Append(System.Environment.NewLine);
        strExcludedTemplates.Append("Attention – Your Appointment for Print selection includes patients with multiple accounts. These patients cannot print as part of a batch and will be excluded from this batch. After this batch print is complete, select each excluded patient one at a time and print. You will be asked to select the correct account for each patient.");
        strExcludedTemplates.Append(System.Environment.NewLine);
        strExcludedTemplates.Append(System.Environment.NewLine);
        strExcludedTemplates.Append("Excluded Patients:");
        strExcludedTemplates.Append(System.Environment.NewLine);

        List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> SplitDocList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
        List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> TempSplitDocList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
        Int32 nTemplateCnt = 0;
        foreach (gloTemplate template in lstgloTemplate)
        {
            if (fromDoc < PrintDocument)
            {
                fromDoc += 1;
            }
            else
            {
                if (_isFormClose == true)
                {
                    return;
                }
                try
                {
                    if (AccountID == 0)
                    {
                        if (template.IsPatientHaveMultipleAccounts && template.IsTemplateContainsPatientAccountFields)
                        {
                            AddSkipedTemplateInfo(template.PatientID, template.PatientName, template.TemplateName, template.AppoinmentTime.ToString());
                            isTemplateSkipped = true;
                            continue;
                        }
                    }
                    gloOffice.Supporting.AppointmentID = 0;
                    gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
                    gloOffice.Supporting.PatientID = template.PatientID;

                    gloOffice.Supporting.PrimaryID = template.TemplateID;

                    //if (IsFromAppointmentTab == true)
                    //{ gloOffice.Supporting.FromDate = template.FromDate; }
                    //else
                    //{ gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy")); }

                    //gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                    //Bug #92723: 00001067: Appointment 
                    gloOffice.Supporting.FromDate = template.FromDate;
                    gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                    //Create New Document in Word
                    //   object missing = System.Reflection.Missing.Value;
                    //object fileName = gloOffice.Supporting.GenerateDocumentFile();
                    String strFileName = gloOffice.Supporting.NewDocumentName();
                    try
                    {
                        System.IO.File.Copy(template.TemplateFilePath, strFileName);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                    }
                    object fileName = strFileName;

                    object newTemplate = false;
                    object docType = 0;
                    object isVisible = true;

                    // Create a new Document, by calling the Add function in the Documents collection
                    Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(strFileName); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

                    gloOffice.Supporting.PrimaryID = template.PrimeryID;
                    gloOffice.Supporting.WdApplication = aDoc.Application; // wordApplication;
                    gloOffice.Supporting.CurrentDocument = aDoc;

                    System.Windows.Forms.Application.DoEvents();
                    gloOffice.Supporting.isFromBatchPrint = true;
                    gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
                    gloOffice.Supporting.isFromBatchPrint = false;
                    
                    gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);

                    aDoc.Save();
                    try
                    {
                        aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
                        aDoc = null;
                    }
                    catch
                    {
                    }
                    aDoc = myLoadWord.LoadWordApplication(strFileName);
                    //// need to see the created document, so make it visible
                    //wordApplication.Visible = true;
                    //aDoc.Activate();

                    //  DocumentPrintOut(ref aDoc);
                   
                    if ((aDoc != null))
                    {
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            if (oldPrinterName != _PrinterSetting.PrinterName)
                            {
                                gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName);
                                Application.DoEvents();
                            }
                        }
                        //gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                        //      ref missing, ref missing, ref missing, ref Copies,
                        //      ref missing, ref missing, ref PrintToFile, ref Collate,
                        //      ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                        //      ref PrintZoomRow, ref missing, ref missing, gloOffice.Supporting.PatientID, popupDetails: _popUpDetails);
                        //// aDoc.Application.ActivePrinter = _PrinterSetting.PrinterName;
                       // aDoc.PrintOut();

                        Int64 RetVal = -2;
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            if (gloGlobal.gloTSPrint.NoOfTemplatesPerJob == 1)
                            {
                                RetVal = gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                                   ref missing, ref missing, ref missing, ref Copies,
                                   ref missing, ref missing, ref PrintToFile, ref Collate,
                                   ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                                   ref PrintZoomRow, ref missing, ref missing, gloOffice.Supporting.PatientID, popupDetails: _popUpDetails);
                            }
                            else
                            {
                                nTemplateCnt++;

                                // Set local printer to word document which will improve performance
                                if (gloGlobal.gloTSPrint._strlocalprinter == "")
                                {
                                    gloGlobal.gloTSPrint.SetlocalPrinter();
                                }
                                if (gloGlobal.gloTSPrint._strlocalprinter != "NoLocalPrinter")
                                {
                                    try
                                    {
                                        //aDoc.Application.WordBasic.FilePrintSetup(Printer: gloGlobal.gloTSPrint._strlocalprinter, DoNotSetAsSysDefault: 1);
                                        gloWord.LoadAndCloseWord.FilePrinterSetupToApplication(ref aDoc, gloGlobal.gloTSPrint._strlocalprinter, 1);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                                        ex = null;
                                    }
                                }
                                //

                                if (gloGlobal.gloTSPrint.UseEMFForWord)
                                {
                                    TempSplitDocList = null;
                                    TempSplitDocList = gloWord.LoadAndCloseWord.getEMFForWord(aDoc);
                                    gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc;
                                    for (int i = 0; i < TempSplitDocList.Count; i++)
                                    {
                                        physicalDoc = TempSplitDocList[i];
                                        SplitDocList.Add(physicalDoc);
                                    }
                                }
                                else
                                {
                                    String PDFFileName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".pdf", "MMddyyyyHHmmssffff");
                                    aDoc.SaveAs(PDFFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, false, "", false);
                                    FileSystem.FileClose();



                                    gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo physicalDoc = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                                    physicalDoc.PdfFileName = PDFFileName;
                                    physicalDoc.SrcFileName = PDFFileName;
                                    physicalDoc.footerInfo = null;
                                    SplitDocList.Add(physicalDoc);
                                }
                                if (nTemplateCnt == gloGlobal.gloTSPrint.NoOfTemplatesPerJob)
                                {
                                    gloWord.LoadAndCloseWord.CopyPrintDocList(SplitDocList, gloOffice.Supporting.PatientID, _popUpDetails);
                                    nTemplateCnt = 0;
                                    SplitDocList.Clear();
                                }
                                RetVal = -1;
                            }
                        }
                        else
                        {
                            RetVal = gloWord.LoadAndCloseWord.PrintDocumentEMF(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                                  ref missing, ref missing, ref missing, ref Copies,
                                  ref missing, ref missing, ref PrintToFile, ref Collate,
                                  ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                                  ref PrintZoomRow, ref missing, ref missing, gloOffice.Supporting.PatientID, _PrinterSetting );
                        }
                        if (RetVal == -1)
                        {
                            UpdateBackgroundPrintReport(strBackgroundPrintReportGUID, 0, template.PatientName, template.TemplateName, template.PatientID, template.TemplateID);
                        }

                        int PagesCount = aDoc.ComputeStatistics(PagesCountStat);

                        if (PagesCount > 1) //added for batch printing issue document getting mixedup
                            Thread.Sleep(200 * PagesCount);


                    }
                    myLoadWord.CloseWordOnly(ref aDoc);
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (oldPrinterName != String.Empty)
                        {
                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName);
                            Application.DoEvents();
                        }
                    }
                    PrintDocument += 1;
                    fromDoc = PrintDocument;
                    InvokeProgressUpdateControls();
                    //try
                    //{
                    //    aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

                    //    aDoc = null;
                    //}
                    //catch
                    //{
                    //}

                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();

                    try
                    {
                        if (System.IO.File.Exists(strFileName))
                        {
                            System.IO.File.Delete(strFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    }

                //    PrintDocument += 1;
                  //  fromDoc = PrintDocument;
                //    InvokeProgressUpdateControls();
                    if (blnbtnPauseclicked)
                    {
                        InvokeEnableDisablePauseButton();
                    }

                    if (IsPrintingResumed)
                    {
                        return;
                    }







                }
                catch (System.Runtime.InteropServices.COMException)
                {
                  //  System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    gloOffice.Supporting.isFromBatchPrint = false;
                }
            }//else end
        } //end of for
        //'if foreground print is on then closing  form after completing printing

        if (SplitDocList.Count > 0 && gloGlobal.gloTSPrint.isCopyPrint)
        {
            gloWord.LoadAndCloseWord.CopyPrintDocList(SplitDocList, gloOffice.Supporting.PatientID, _popUpDetails);
        }
        
        if (isTemplateSkipped)
        {
            foreach (frmgloPrintWordProgressController.PatientMessage oMsg in oPatientMessages)
            {
                strExcludedTemplates.Append(System.Environment.NewLine);
                strExcludedTemplates.Append(oMsg.Message);
            }
            gloWord.frmgloMessageBox oMsgForm = new gloWord.frmgloMessageBox();
            oMsgForm.Text = "";// _messageBoxCaption;
            oMsgForm.Setmessage(strExcludedTemplates);
            oMsgForm.ShowDialog(ParentForm);
            oMsgForm.Dispose();
            oMsgForm = null;
            // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
    catch (Exception)
    {
       // ex = null;
    }
    finally
    {
      
        
        if ((blnBackGroundPrint == false) & _isFormClose == false)
        {
            _isFormClose = true;
            this.Close();
            this.Dispose(true);
        }
    }
}

  private void UpdateBackgroundPrintReport(System.Guid ID, Int64 TotalDocuments, String PatientName, String TemplateName, Int64 PatientID, Int64 TemplateID)
  {
      SqlConnection connMain = null;
      SqlCommand cmdMain = null;

      try
      {
          if (_databaseConnectionString != "")
          {
              connMain = new SqlConnection();
              connMain.ConnectionString = _databaseConnectionString;

              cmdMain = new SqlCommand();
              cmdMain.Connection = connMain;
              cmdMain.CommandType = CommandType.StoredProcedure;
              cmdMain.CommandText = "gsp_UpdateBackgroundPrintReport";

              cmdMain.Parameters.Add("ID", SqlDbType.UniqueIdentifier);
              cmdMain.Parameters["ID"].Value = ID;

              cmdMain.Parameters.Add("TotalDocuments", SqlDbType.Int);
              cmdMain.Parameters["TotalDocuments"].Value = TotalDocuments;

              cmdMain.Parameters.Add("PatientName", SqlDbType.VarChar);
              cmdMain.Parameters["PatientName"].Value = PatientName;

              cmdMain.Parameters.Add("TemplateName", SqlDbType.VarChar);
              cmdMain.Parameters["TemplateName"].Value = TemplateName;

              cmdMain.Parameters.Add("PatientID", SqlDbType.VarChar);
              cmdMain.Parameters["PatientID"].Value = PatientID;

              cmdMain.Parameters.Add("TemplateID", SqlDbType.VarChar);
              cmdMain.Parameters["TemplateID"].Value = TemplateID;

              connMain.Open();
              cmdMain.ExecuteNonQuery();
              connMain.Close();
          }
      }

      catch (Exception ex)
      {
          gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
      }

      finally
      {
          if (cmdMain != null)
          {
              cmdMain.Dispose();
              cmdMain = null;
          }

          if (connMain != null)
          {
              connMain.Dispose();
              connMain = null;
          }
      }
  }


  private void PrintgloTemplatesonly()
  {
      Int64 fromDoc = 0;
      //bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
      object Background = false;
      object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
      object Copies = gloGlobal.gloTSPrint.isCopyPrint?(short)1:_PrinterSetting.Copies;
      object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
      object PrintToFile = false;
      object Collate = gloGlobal.gloTSPrint.isCopyPrint?true:_PrinterSetting.Collate;
      object ActivePrinterMacGX = Type.Missing;
      object ManualDuplexPrint = false;
      object PrintZoomColumn = 1;
      object PrintZoomRow = 1;
      object missing = Type.Missing;
      try
      {
          if (myLoadWord == null)
          {
              myLoadWord = new gloWord.LoadAndCloseWord();
          }
          // gloWord.LoadAndCloseWord myLoadWord = new gloWord.LoadAndCloseWord();

          //Boolean isTemplateSkipped = false;
          object missing_new = Type.Missing;
          object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
         
          foreach (gloTemplate template in lstgloTemplate)
          {

              if (fromDoc < PrintDocument)
              {
                  fromDoc += 1;
              }
              else
              {
                  if (_isFormClose == true)
                  {
                      return;
                  }
              
              
              try
              {
                  
                  gloOffice.Supporting.AppointmentID = 0;
                  gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
                  gloOffice.Supporting.PatientID = template.PatientID;

                  gloOffice.Supporting.PrimaryID = template.TemplateID;
                  gloOffice.Supporting.VisitID = template.VisitID;   //added for caseid CAS-07903-V1Y1P8  
                  gloOffice.Supporting.FromDate = template.FromDate;
                  gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                  //Create New Document in Word
                  //   object missing = System.Reflection.Missing.Value;
                  //object fileName = gloOffice.Supporting.GenerateDocumentFile();
                  String strFileName = gloOffice.Supporting.NewDocumentName();
                  try
                  {
                      System.IO.File.Copy(template.TemplateFilePath, strFileName);
                  }
                  catch (Exception ex)
                  {
                      gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                  }
                  object fileName = strFileName;

                  object newTemplate = false;
                  object docType = 0;
                  object isVisible = true;

                  // Create a new Document, by calling the Add function in the Documents collection
                  Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(strFileName); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

                  gloOffice.Supporting.PrimaryID = template.PrimeryID;
                  gloOffice.Supporting.WdApplication = aDoc.Application; // wordApplication;
                  gloOffice.Supporting.CurrentDocument = aDoc;

                  System.Windows.Forms.Application.DoEvents();
                
                  gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, 0);
               
                  gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
                  //// need to see the created document, so make it visible
                  //wordApplication.Visible = true;
                  //aDoc.Activate();

                  //  DocumentPrintOut(ref aDoc);

                  if ((aDoc != null))
                  {
                      if (!gloGlobal.gloTSPrint.isCopyPrint)
                      {
                          if (oldPrinterName != _PrinterSetting.PrinterName)
                          {
                              gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName);
                              Application.DoEvents();
                          }
                      }
                      gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref Copies,
                            ref missing, ref missing, ref PrintToFile, ref Collate,
                            ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                            ref PrintZoomRow, ref missing, ref missing, gloOffice.Supporting.PatientID, popupDetails: _popUpDetails);
                  }
                  myLoadWord.CloseWordOnly(ref aDoc);
                  if (!gloGlobal.gloTSPrint.isCopyPrint)
                  {
                      if (oldPrinterName != String.Empty)
                      {
                          gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName);
                          Application.DoEvents();
                      }
                  }
                  PrintDocument += 1;
                  fromDoc = PrintDocument;
                  InvokeProgressUpdateControls();
                  //try
                  //{
                  //    aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

                  //    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

                  //    aDoc = null;
                  //}
                  //catch
                  //{
                  //}

                  //GC.Collect();
                  //GC.WaitForPendingFinalizers();

                  try
                  {
                      if (System.IO.File.Exists(strFileName))
                      {
                          System.IO.File.Delete(strFileName);
                      }
                  }
                  catch (Exception ex)
                  {
                      gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                  }
                  if (blnbtnPauseclicked)
                  {
                      InvokeEnableDisablePauseButton();
                  }
                  if (IsPrintingResumed == true)
                  {
                      return;
                  }
                  //if (_CancelPrinting == true)
                  //{
                  //    _CancelPrinting = false;
                  //    break;
                  //}




               //   PrintDocument += 1;
                 // fromDoc = PrintDocument;
               //   InvokeProgressUpdateControls();
                  if (blnbtnPauseclicked)
                  {
                      InvokeEnableDisablePauseButton();
                  }

                  if (IsPrintingResumed)
                  {
                      return;
                  }





              }
              catch (System.Runtime.InteropServices.COMException )
              {
                 // System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
              }
              finally
              {
                  gloOffice.Supporting.isFromBatchPrint = false;
              }
          }//else
          }
          //'if foreground print is on then closing  form after completing printing

         // myLoadWord.CloseApplicationOnly();
        //  myLoadWord = null;
          //if (isTemplateSkipped)
          //{
          //    foreach (frmgloPrintWordProgressController.PatientMessage oMsg in oPatientMessages)
          //    {
          //        strExcludedTemplates.Append(System.Environment.NewLine);
          //        strExcludedTemplates.Append(oMsg.Message);
          //    }
          //    gloWord.frmgloMessageBox oMsgForm = new gloWord.frmgloMessageBox();
          //    oMsgForm.Text = "";// _messageBoxCaption;
          //    oMsgForm.Setmessage(strExcludedTemplates);
          //    oMsgForm.ShowDialog(ParentForm);
          //    oMsgForm.Dispose();
          //    oMsgForm = null;
          //    // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
          //}

          if ((blnBackGroundPrint == false) & _isFormClose == false)
          {
              _isFormClose = true;
              this.Close();
              this.Dispose(true);
          }
      }
      catch (Exception )
      {
         // ex = null;
      }
  }

        //private void Print()
        //{
        //    Int64 fromDoc = 0;
        //    //bool blnBuildBlockSetting = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
        //    bool blnBuildBlockSetting = false;
        //    short Copies = _PrinterSetting.Copies;
        //    //'

        //    try
        //    {


        //        foreach (DataRow dr in dtSelectPatient.Rows)
        //        {


        //            foreach (KeyValuePair<Int64, string> di in dictPatientLetter)
        //            {


        //                if ((fromDoc < PrintDocument))
        //                {
        //                    fromDoc += 1;
        //                }
        //                else
        //                {
        //                    //'added for bugid 92668
        //                    if ((_isFormClose == true))
        //                    {
        //                        return;
        //                    }
        //                    lock (this)
        //                    {
                               
        //                        _bIsUnscheduledCare = ChkRemiderForUnSchedle;

        //                        string strFileName = "";

        //                        try
        //                        {

        //                            Fill_TemplateGallery();
        //                            //	strFileName = ExamNewDocumentName;
        //                            if ((oCurDoc != null))
        //                            {
        //                                oCurDoc.SaveAs(strFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, false, "", false);
        //                            }

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                        }
        //                        if (!string.IsNullOrEmpty(strFileName))
        //                        {
        //                            if ((myLoadWord == null))
        //                            {
        //                                return;
        //                            }
        //                            if ((oCurDoc != null))
        //                            {
        //                                gloWord.LoadAndCloseWord.CleanupDoc(ref oCurDoc);

        //                                if (gblnPageNo == true)
        //                                {
        //                                    //	UpdateLog("InsertNamePageNo start ");

        //                                    //if (blnBuildBlockSetting) {
        //                                    ////	InsertNamePageNo(ref oCurDoc, GetPatientDetails(_PatientID));
        //                                    //} else {
        //                                    //    InsertPageFooterWithoutMSWBuildingBlock(ref oCurDoc, GetPatientDetails(_PatientID));
        //                                    //}

        //                                    //UpdateLog("InsertNamePageNo end");
        //                                }
        //                                if ((oCurDoc != null))
        //                                {
        //                                    oCurDoc.Application.ActivePrinter = _PrinterSetting.PrinterName;
        //                                    //oCurDoc.PrintOut(, , , , , , , Copies);
        //                                }
        //                            }

                                   
        //                            if (myLoadWord == null)
        //                            {
        //                                return;
        //                            }
        //                            myLoadWord.CloseWordOnly(ref oCurDoc);
        //                            PrintDocument += 1;
        //                            fromDoc = PrintDocument;
        //                            InvokeProgressUpdateControls();
        //                        }
        //                        if (blnbtnPauseclicked)
        //                        {
        //                            InvokeEnableDisablePauseButton();
        //                        }
        //                        if (IsPrintingResumed == true)
        //                        {
        //                            return;
        //                        }
        //                    }
        //                }


        //            }

        //        }
        //        //'if foreground print is on then closing  form after completing printing
        //        if ((blnBackGroundPrint == false) & _isFormClose == false)
        //        {
        //            _isFormClose = true;
        //            this.Close();
        //            this.Dispose(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = null;
        //    }
        //}

        public delegate void EnableDisablePauseButtonDelegate();
        public void InvokeEnableDisablePauseButton()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EnableDisablePauseButtonDelegate(EnableDisablePauseButton));
                }
                else
                {
                    EnableDisablePauseButton();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void EnableDisablePauseButton()
        {
            TimeSpan ts = DateTime.Now.Subtract(clickTime);
            if (ts.Seconds > 1)
            {
                blnbtnPauseclicked = false;
                btnPause.Enabled = true;
            }

        }
        public object GetPatientDetails(Int64 m_PatientId)
        {
            //string strName = "";

            //DataBaseLayer oDB = new DataBaseLayer();

            //try
            //{
            //    string strSQL = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ' , DOB : ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" + m_PatientId;
            //    strName = oDB.GetRecord_Query(strSQL);
            //    if ((strName != null))
            //    {
            //        return strName;
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return "";
            //}
            //finally
            //{
            //    oDB.Dispose();
            //    //Change made to solve memory Leak and word crash issue
            //    oDB = null;
            //}
            return null;
        }
        public void InsertNamePageNo(ref Wd.Document oCurDoc, string sName)
        {

            if (oCurDoc == null)
            {
                return;
            }
            try
            {
                if (oCurDoc.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdNormalView | oCurDoc.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdOutlineView)
                {
                    oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView;
                }
                oCurDoc.Activate();

                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter;

                oCurDoc.Application.Selection.Select();
                if (oCurDoc.Application.Selection.HeaderFooter.IsHeader)
                {
                    oCurDoc.Application.Selection.HeaderFooter.Range.Select();

                }

                string strFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Document Building Blocks\\1033";
                string strtxt = "";

                //Office 2010
                if (Directory.Exists(strFolderPath + "\\14"))
                {
                    strtxt = strFolderPath + "\\14\\Built-In Building Blocks.dotx";
                    //Office 2013
                }
                else if (Directory.Exists(strFolderPath + "\\15"))
                {
                    strtxt = strFolderPath + "\\15\\Built-In Building Blocks.dotx";
                    //Office 2007
                }
                else
                {
                    strtxt = strFolderPath + "\\Building Blocks.dotx";
                }

                //Dim strtxt As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                //strtxt &= "\Microsoft\Document Building Blocks\1033\15\Built-In Building Blocks.dotx"
                //'strtxt &= "\Microsoft\Document Building Blocks\1033\Building Blocks.dotx"

                if (File.Exists(strtxt))
                {
                    // oCurDoc.AttachedTemplate = strtxt;
                    if (strtxt.Contains("14") == false & strtxt.Contains("15") == false)
                    {
                        oCurDoc.XMLSchemaReferences.AutomaticValidation = true;
                        oCurDoc.XMLSchemaReferences.AllowSaveAsXMLWithoutValidation = false;
                    }
                }
                if (File.Exists(strtxt))
                {
                    System.IO.FileAttributes attribute = default(System.IO.FileAttributes);
                    attribute = File.GetAttributes(strtxt);
                    if (attribute != FileAttributes.ReadOnly)
                    {
                        attribute = FileAttributes.ReadOnly;
                        File.SetAttributes(strtxt, attribute);
                    }
                }
                foreach (Wd.Template objTemp in oCurDoc.Application.Templates)
                {
                    if (objTemp.Name == "Building Blocks.dotx" | objTemp.Name == "Built-In Building Blocks.dotx")
                    {
                        objTemp.BuildingBlockEntries.Item("Bold Numbers 3").Insert(Where: oCurDoc.Application.Selection.HeaderFooter.Range, RichText: true);
                    }
                }
                if (!string.IsNullOrEmpty(sName))
                {
                    oCurDoc.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft;
                    oCurDoc.Application.Selection.HeaderFooter.Range.InsertBefore(sName + Constants.vbTab + Constants.vbTab);
                    oCurDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory);
                    oCurDoc.Application.Selection.TypeBackspace();
                }


            }
            catch //(Exception ex)
            {
            }
            finally
            {
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument;
            
                if (myCaller != null)
                {
                    try
                    {
                        myCaller.Focus();
                    }
                    catch
                    {
                    }
                }
             
            }
        }


        public void InsertPageFooterWithoutMSWBuildingBlock(ref Wd.Document oCurDoc, string sName)
        {

            if (oCurDoc == null)
            {
                return;
            }

            try
            {

                foreach (Wd.Section oSection in oCurDoc.Sections)
                {
                    if (oSection.Application.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdNormalView | oCurDoc.ActiveWindow.ActivePane.View.Type == Wd.WdViewType.wdOutlineView)
                    {
                        oSection.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView;
                    }


                    oSection.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter;

                    oSection.Application.Selection.HeaderFooter.Range.Delete();
                    oSection.Application.Selection.HeaderFooter.Range.Font.Name = "Arial";

                    oSection.Application.Selection.HeaderFooter.Range.Font.Size = 8;
                    oSection.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft;

                    oSection.Application.Selection.Range.Text = string.Empty;

                    oSection.Application.Selection.TypeText("Page ");

                    dynamic CurrentPage = Wd.WdFieldType.wdFieldPage;

                    //   oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, CurrentPage);

                    oSection.Application.ActiveWindow.Selection.TypeText(" of ");

                    dynamic TotalPages = Wd.WdFieldType.wdFieldNumPages;

                    //  oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, TotalPages);

                    if (!string.IsNullOrEmpty(sName.Trim()))
                    {
                        oSection.Application.Selection.HeaderFooter.Range.InsertBefore(sName.Trim() + Constants.vbTab + Constants.vbTab);
                    }

                }




            }
            catch //(Exception ex)
            {
            }
            finally
            {
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument;
            }
        }


        private void UpdateReminder(Int64 TaskID)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            try
            {
                //  objCon.ConnectionString = GetConnectionString();
                objCon.Open();
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = "UPDATE RM_Reminder_MST SET bIsDismissed ='TRUE' WHERE nRefrenceType = 2 AND nReferenceID = " + TaskID + " ";
                objCmd.Connection = objCon;
                objCmd.ExecuteNonQuery();
            }
            catch (SqlException)// ex)
            {
                if (objCon.State == ConnectionState.Open)
                {
                    objCon.Close();
                }
            }
            catch (Exception)// ex)
            {
                if (objCon.State == ConnectionState.Open)
                {
                    objCon.Close();
                }
            }
            finally
            {
                if ((objCon != null))
                {
                    if (objCon.State == ConnectionState.Open)
                    {
                        objCon.Close();
                    }
                    objCon.Dispose();
                    objCon = null;
                }

                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }


            }
        }
        //As String
        private void Fill_TemplateGallery()
        {
            //   string strFileName = "";
            //   //dynamic objWord = new clsWordDocument();
            //   //dynamic objCriteria = new DocCriteria();
            //   //objCriteria.DocCategory = enumDocCategory.Template;
            //   //objCriteria.PrimaryID = _TemplateID;
            //   //objWord.DocumentCriteria = objCriteria;
            //   ////'//Retrieving the Patient Education from DB and Save it as Physical File
            //   //strFileName = objWord.RetrieveDocumentFile();
            //   //objCriteria.Dispose();
            ////   objCriteria = null;
            //  // objWord = null;
            //   if (((strFileName == null) == false))
            //   {
            //       if (!string.IsNullOrEmpty(strFileName))
            //       {
            //           LoadWordUserControl(strFileName, true);
            //           //Set the Start postion of the cursor in documents
            //           oCurDoc.Application.Selection.HomeKey(Microsoft.Office.Interop.Word.WdUnits.wdStory);

            //       }

            //   }



        }
       // gloWord.LoadAndCloseWord myLoadWord = new gloWord.LoadAndCloseWord();

        private void LoadWordUserControl(string strFileName, bool blnGetData = false)
        {
            //oCurDoc = null;
            //oCurDoc = myLoadWord.LoadWordApplication(strFileName);
            //if (blnGetData)
            //{
            //    //'//To retrieve the Form fields for the Word document
            //    //dynamic objWord = new clsWordDocument();
            //    //dynamic objCriteria = new DocCriteria();
            //    //objCriteria.DocCategory = enumDocCategory.Others;
            //    //objCriteria.PatientID = _PatientID;
            //    //objCriteria.VisitID = _VisitID;
            //    //objCriteria.PrimaryID = _TemplateID;
            //    ////'0
            //    //objWord.DocumentCriteria = objCriteria;
            //    //objWord.CurDocument = oCurDoc;
            //    ////'Replace Form fields with Concerned data
            //    //objWord.GetFormFieldData(enumDocType.None);
            //    //oCurDoc = objWord.CurDocument;
            //    //oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
            //    //objCriteria.Dispose();
            //    //objCriteria = null;
            //    //objWord = null;
            //}
            //else
            //{
            //    //dynamic objWord = new clsWordDocument();
            //    //objWord.CurDocument = oCurDoc;
            //    //objWord.HighlightColor();
            //    //oCurDoc = objWord.CurDocument;
            //    //oCurDoc.ActiveWindow.View.ShowFieldCodes = false;
            //    //objWord = null;
            //}
        }

        public delegate void UpdateProgressControlsDelegate();
        public void InvokeProgressUpdateControls()
        {
            try
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
                if ((pbDocument.Value < pbDocument.Maximum))
                {
                    pbDocument.Value = PrintDocument;
                    pbDocument.Refresh();
                    // If _objwordList IsNot Nothing Then
                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (PrintDocument == pbDocument.Maximum)
                        {
                            lblPages.Text = "Sending documents to local printer ";
                        }
                        else
                        {
                            lblPages.Text = "Processing " + PrintDocument.ToString() + " of " + pbDocument.Maximum.ToString();
                        }
                    }
                    else
                    {
                        lblPages.Text = "Printing " + PrintDocument.ToString() + " of " + pbDocument.Maximum.ToString();
                    }
                    // End If
                    if ((PrintDocument == 3))
                    {
                        btnRestart.Enabled = true;
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }



        private void OnPrintComplete(IAsyncResult iar)
        {
            //Dim r As IAsyncResult = Nothing

            try
            {
                IsPrintingCompletedWhilePressingPause = true;
                //'  MessageBox.Show(PrintDocument.ToString())
                //If (PrintDocument <> pbDocument.Maximum) Then
                //    Print(PrintDocument)
                //End If
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
                //Return Nothing
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

            }
            //Return r
        }

        public delegate void UpdateCompleteControlsDelegate();
        public void InvokeCompleteUpdateControls()
        {
            try
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
                //'added for bugid 92017
                if ((_isFormClose == false))
                {
                    _isFormClose = true;
                    this.Close();
                    this.Dispose(true);

                    btnRestart.Enabled = true;
                    //btnPause.Visible = False
                    //btnPlay.Visible = True
                    //IsPrintingResumed = True
                    //If btnPause.Text = "&Pause" Then
                    //    btnPause.Text = "&Resume"
                    //    IsPrintingResumed = True
                    //End If
                    if ((btnPause.Visible == false))
                    {
                        IsPrintingResumed = true;
                    }
                    btnPause.Update();
                    btnRestart.Update();
                    btnPause.ResumeLayout();
                    btnRestart.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex,false);
                ex = null;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                IsPrintingResumed = false;
                IsPrintingCanceled = false;
              //  _printPageIndex = 0;
                PrintDocument = 0;

                btnRestart.Enabled = false;
                //If btnPause.Text = "&Resume" Then
                //    btnPause.Text = "&Pause"
                //    IsPrintingResumed = False
                //End If

                btnPlay.Visible = false;
                btnPause.Visible = true;

                if ((btnPause.Visible == true))
                {
                    IsPrintingResumed = false;
                }

                ((Button)sender).Enabled = false;
                //     If _gloStandardPrintController IsNot Nothing Then
                //_gloStandardPrintController.IsRestart = True
                //  End If
                //DirectCast(sender, Button).Update()
                //btnPause.Update()
                //DirectCast(sender, Button).ResumeLayout()
                //btnPause.ResumeLayout()
                try
                {
                    pbDocument.Value = PrintDocument;
                }
                catch
                {
                }
                PrintWithOrWithoutBackground();
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {

            try
            {

                //If (btnPause.Text = "&Resume") Or (pbDocument.Value >= pbDocument.Maximum) Then
                //    btnPause.Enabled = False
                //    clickTime = DateTime.Now
                //    blnbtnPauseclicked = True
                //End If
                //If DirectCast(sender, Button).Text = "&Pause" Then
                //    DirectCast(sender, Button).Text = "&Resume"
                //    IsPrintingResumed = True
                //    btnRestart.Enabled = True
                //    DirectCast(sender, Button).Update()
                //    btnRestart.Update()
                //    DirectCast(sender, Button).ResumeLayout()
                //    btnRestart.ResumeLayout()

                //Else

                //  DirectCast(sender, Button).Text = "&Pause"
                btnPause.Visible = false;
                btnPlay.Visible = true;
                IsPrintingResumed = true;

                // DirectCast(sender, Button).Update()
                btnRestart.Update();
                // DirectCast(sender, Button).ResumeLayout()
                btnRestart.ResumeLayout();
                //If _gloStandardPrintController IsNot Nothing Then
                //    _gloStandardPrintController.IsRestart = True
                //End If

                // PrintWithOrWithoutBackground()
                // End If
            }
            catch (Exception ex)
            {
               
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }

       // private bool IsInsidePrinting = false;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                IsPrintingCanceled = true;
                //If _gloStandardPrintController IsNot Nothing Then
                //    _gloStandardPrintController.IsCancel = True
                //End If
                InvokeCompleteUpdateControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }





        private void btnPlay_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                btnPause.Visible = true;
                btnPlay.Visible = false;

                //If (btnPause.Text = "&Resume") Or (pbDocument.Value >= pbDocument.Maximum) Then
                //    btnPause.Enabled = False
                //    clickTime = DateTime.Now
                //    blnbtnPauseclicked = True
                //End If
                //If DirectCast(sender, Button).Text = "&Pause" Then
                //    DirectCast(sender, Button).Text = "&Resume"
                //    IsPrintingResumed = True
                //    btnRestart.Enabled = True
                //    DirectCast(sender, Button).Update()
                //    btnRestart.Update()
                //    DirectCast(sender, Button).ResumeLayout()
                //    btnRestart.ResumeLayout()

                //Else
                //   DirectCast(sender, Button).Text = "&Pause"
                IsPrintingResumed = false;

                // DirectCast(sender, Button).Update()
                btnRestart.Update();
                // DirectCast(sender, Button).ResumeLayout()
                btnRestart.ResumeLayout();
                //If _gloStandardPrintController IsNot Nothing Then
                //    _gloStandardPrintController.IsRestart = True
                //End If

                PrintWithOrWithoutBackground();
                // End If
            }
            catch (Exception ex)
            {
              
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }
    }


}
