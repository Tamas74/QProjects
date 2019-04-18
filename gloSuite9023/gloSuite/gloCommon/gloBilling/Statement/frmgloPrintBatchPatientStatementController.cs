

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
    public partial class frmgloPrintBatchPatientStatementController : Form
    {

        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);
        //  private int _nPagesCnt = 0;
      //  private int _printPageIndex = 0;
      //  private bool _IsOnPagePDFDocCreated = false;
        //   Private _gloStandardPrintController As gloStandardPrintController = Nothing
       // private Microsoft.Office.Interop.Word.Application oWordApp;
       // private String _PDFFileName = null;
        // Private _oPrintDocument As PrintDocument = Nothing
        private PrinterSettings _PrinterSetting = null;

        private gloExtendedPrinterSettings _ExtendedPrinterSettings = null;
        public bool IsPrintingResumed = false;

        public bool IsPrintingCanceled = false;
        private bool IsPrintingCompletedWhilePressingPause = true;
      
        public bool ChkRemiderForUnSchedle = false;
        public static bool blnBackGroundPrint = false;
    //    private Microsoft.Office.Interop.Word.Document oCurDoc;
        private int PrintDocument = 0;
    
        public bool _bIsUnscheduledCare = false;
      //  private Int64 _nCommunicationTypeID = 0;
        private DateTime clickTime = DateTime.Now;
        private bool blnbtnPauseclicked = false;
      // public long AccountID=0;
        private bool _isFormClose = false;
        public ArrayList oListTempleteIds = null;
        public string OldPrinterName = "";
        gloWord.LoadAndCloseWord myLoadWord = null;
      //public   List<PatientMessage> oPatientMessages = new List<PatientMessage>();
      public string _databaseConnectionString = "";
      private gloClinicalQueueGeneral.QueueDocumentDocumentDetails _popUpDetails = null;
        private Control myCaller = null;
        public frmgloPrintBatchPatientStatementController(Control myControl = null)
        {
           
            Shown += frmgloPrintWordProgressController_Shown;
            FormClosed += frmgloPrintWordProgressController_FormClosed;
            myCaller = myControl;
            InitializeComponent();

        }
        public frmgloPrintBatchPatientStatementController(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, Control myControl = null)
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
                if (gloGlobal.gloTSPrint.isCopyPrint && (!gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false)))
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


                //if (((myCaller == null) == false))
                //{
                //    ((MainMenu)myCaller).PrintMenuEventclick(false);
                //}
                //dynamic frm = Application.OpenForms["frmDashBoardMain"];
                //if (frm != null)
                //{
                //    frm.PrintMenuEventclick(false , "frmgloPrintBatchPatientStatementController");
                //}
                gloGlobal.LoadFromAssembly.PrintMenuEventclick(false, "frmgloPrintBatchPatientStatementController");

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

                if (oListTempleteIds != null)
                {
                    oListTempleteIds.Clear();
                    oListTempleteIds = null;
                }

                //if (lstgloTemplate != null)
                //{
                //    foreach (gloTemplate Template in lstgloTemplate)
                //    {
                //        if (System.IO.File.Exists(Template.TemplateFilePath))
                //        {
                //            System.IO.File.Delete(Template.TemplateFilePath);
                //        }
                //    }
                //}
            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (OldPrinterName != String.Empty)
                        {
                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(OldPrinterName);
                            Application.DoEvents();
                        }
                    }
                }
                catch
                {
                }
            }
        }




        private void frmgloPrintWordProgressController_Shown(object sender, EventArgs e)
        {
            try
            {
                //Int64 cntrec = 0;
                //if ((dtSelectPatient != null))
                //{
                //    cntrec = dtSelectPatient.Rows.Count;
                //}
                //if ((dictPatientLetter != null))
                //{
                //    cntrec = cntrec * dictPatientLetter.Count;
                //}
                //dynamic frm = Application.OpenForms["frmDashBoardMain"];
                //if (frm != null)
                //{
                //    frm.PrintMenuEventclick(true, "frmgloPrintBatchPatientStatementController");
                //}
                myLoadWord = new gloWord.LoadAndCloseWord();
                gloGlobal.LoadFromAssembly.PrintMenuEventclick(true, "frmgloPrintBatchPatientStatementController");

                   if (oListTempleteIds != null)
                {
                    if (oListTempleteIds.Count > 0)
                    {
                        pbDocument.Maximum = oListTempleteIds.Count;
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
                  //lblPrinterNameValue.Text = _PrinterSetting.PrinterName;

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
                      //  Exhaused waiting for 2 seconds
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
                    
                        Print();
                   
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
               
                    Print();
               
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
                //lblPrinterNameValue.Text = _PrinterSetting.PrinterName;
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


        long _ntempTemplateID = 0;
  private void Print()
{
	Int64 fromDoc = 0;
	//bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
	//short Copies = _PrinterSetting.Copies;
	//'

    try
    {
        if (myLoadWord == null)
        {
         myLoadWord =   new gloWord.LoadAndCloseWord();
        }

        for (int iCount = 0; iCount < oListTempleteIds.Count; iCount++)
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
                _ntempTemplateID = Convert.ToInt64(oListTempleteIds[iCount]);
                try
                {
                    PrintTemplate(_ntempTemplateID, ref myLoadWord);
                    PrintDocument += 1;
                    fromDoc = PrintDocument;
                    InvokeProgressUpdateControls();
                    if (blnbtnPauseclicked)
                    {
                        InvokeEnableDisablePauseButton();
                    }

                    if (IsPrintingResumed)
                    {
                        break;
                    }
                }
               
                
                catch (Exception ex1)
                {
                    // MessageBox.Show("Error while Printing.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
                }
                _ntempTemplateID = 0;
            }

        }
        //if (myLoadWord != null)
        //{
        //    myLoadWord.CloseApplicationOnly();
        //}
        //    myLoadWord = null;


      
    }
    catch (System.Runtime.InteropServices.COMException ex)
    {
        //  System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
        ex = null;
    }
    catch (Exception )
    {
     //   ex = null;
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

  public DataTable GetPatientTemplate(Int64 TransactionID)
  {
      gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
      //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
      oDB.Connect(false);
      string strSQL = "";
      DataTable dt = null;
      strSQL = " SELECT isnull(nPatientID,0) as nPatientID, isnull(nCategoryID,0) AS nCategoryID , ISNULL(sCategoryName,'') as sCategoryName , isnull(nTemplateID,0) AS nTemplateID , ISNULL(sTemplateName,'') as sTemplateName , nFromDate, nToDate, ISNULL(nProviderID,0) AS nProviderID, ISNULL(iTemplate,null) AS iTemplate, ISNULL(nCount,1) AS nCount, ISNULL(nClinicID,0) AS nClinicID " +
               " FROM PatientTemplates WITH (NOLOCK) " +
               " WHERE nTransactionID = " + TransactionID + " ";
      oDB.Retrive_Query(strSQL, out  dt);
      oDB.Disconnect();
      oDB.Dispose();
      return dt;
  }

  public bool ConvertBinaryToFile(object StreamData, string FilePath)
  {
      bool _result = false;
      //  string _FilePath = FilePath;
      try
      {
          if (StreamData != null && StreamData != DBNull.Value)
          {
              Byte[] byteRead = (byte[])StreamData;
              //  MemoryStream oDataStream = new MemoryStream(byteRead);
              FileStream oFile = new FileStream(FilePath, FileMode.Create);
              oFile.Write(byteRead, 0, byteRead.Length);
              // oDataStream.WriteTo(oFile);
              //oDataStream.Close();
              //oDataStream.Dispose();
              //oDataStream = null;
              oFile.Close();
              oFile.Dispose();
              oFile = null;
              // oFile.Dispose();
              _result = true;
          }
      }
      catch //(Exception ex)
      {
        //  MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      return _result;
  }
  string TemplateName = "";
  public void PrintTemplate(Int64 _TransactionID, ref gloWord.LoadAndCloseWord myLoadWord)
  {
      DataTable dttemp = null;//SLR: new is not needed
      object Visible = false;
      object Background = false;
      object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
      object Copies = gloGlobal.gloTSPrint.isCopyPrint ? (short)1 : _PrinterSetting.Copies;
      object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
      object PrintToFile = false;
      object Collate = gloGlobal.gloTSPrint.isCopyPrint ? true : _PrinterSetting.Collate;
      object ActivePrinterMacGX = Type.Missing;
      object ManualDuplexPrint = false;
      object PrintZoomColumn = 1;
      object PrintZoomRow = 1;
      object missing = Type.Missing;

      try
      {
          dttemp = GetPatientTemplate(_TransactionID);

          if (dttemp != null && dttemp.Rows.Count > 0)
          {
              //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
              //PatientID = Convert.ToInt64(dttemp.Rows[0]["nPatientID"].ToString());
              //PrimeryID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
              //CategoryID = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
              //CategoryName = dttemp.Rows[0]["sCategoryName"].ToString();
              //TemplateID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
              TemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
              // FromDate = Convert.ToInt32(dttemp.Rows[0]["nFromDate"]);

              //Set the File to control
              string strNewDocumentName = "";
              strNewDocumentName = gloOffice.Supporting.NewDocumentName();

              object objTemplateDocument;

              if (!string.IsNullOrEmpty(dttemp.Rows[0]["iTemplate"].ToString()))
              {
                  objTemplateDocument = dttemp.Rows[0]["iTemplate"];

                  //Bug #63771: 00000624: Patient Statement Print
                  //When you click the 'Print' button, nothing happens at all.
                  //While click on Reprint batch nothing is happens.
                  if (TemplateName.Contains("PatientStatement"))
                  { strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc"); }

                  ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);

                  Wd.Document oTemp = myLoadWord.LoadWordApplication(strNewDocumentName); // oWordApp.Documents.Open(strNewDocumentName);

                  //oTemp.Application.Options.PrintBackground = true;
                  //oTemp.PrintOut(Background: true);
                  // gloWord.LoadAndCloseWord.PrintWordDocument(ref oTemp, false);
                  if ((oTemp != null))
                  {
                      if (!gloGlobal.gloTSPrint.isCopyPrint)
                      {
                          if (OldPrinterName != _PrinterSetting.PrinterName)
                          {
                              gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName);
                              Application.DoEvents();
                          }
                      }
                      gloWord.LoadAndCloseWord.PrintDocument(ref oTemp, ref Background, ref missing, ref missing, ref missing,
                   ref missing, ref missing, ref missing, ref Copies,
                   ref missing, ref missing, ref PrintToFile, ref Collate,
                   ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                   ref PrintZoomRow, ref missing, ref missing, popupDetails: _popUpDetails);
                      // oTemp.Application.ActivePrinter = _PrinterSetting.PrinterName;
                      // oTemp.PrintOut();
                  }
                  myLoadWord.CloseWordOnly(ref oTemp);
                  if (!gloGlobal.gloTSPrint.isCopyPrint)
                  {
                      if (OldPrinterName != String.Empty)
                      {
                          gloGlobal.gloTSPrint.SetDefaultPrinterSettings(OldPrinterName);
                          Application.DoEvents();
                      }
                  }
                  //oTemp.Close(SaveChanges:false);
                  //try
                  //{
                  //    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTemp);
                  //    oTemp = null;
                  //}
                  //catch
                  //{
                  //}
              }
          }
      }

      catch (System.Runtime.InteropServices.COMException ex)
      {
          //  System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
          gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
          ex = null;
      }
      catch (Exception)
      {
          //   gloAuditTrail.gloAuditTrail.PrintLog(Ex.ToString());
          //   Ex = null;
      }
      finally
      {
          if (dttemp != null)
          {
              dttemp.Dispose();
          }
          dttemp = null;

          //ogloTemplate.Dispose(); 
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
                    lblPages.Text = "Printing " + PrintDocument.ToString() + " of " + pbDocument.Maximum.ToString();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                IsPrintingResumed = false;
                IsPrintingCanceled = false;
               // _printPageIndex = 0;
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
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false );
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false );
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
