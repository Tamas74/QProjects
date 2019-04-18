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
using System.Data.SqlClient;
using Wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.InteropServices;
using gloWord;

namespace gloPrintDialog
{
    public partial class frmgloPrintQueueController : Form
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
        // private int _nPagesCnt = 0;
        // private int _printPageIndex = 0;
        // private bool _IsOnPagePDFDocCreated = false;
        //   Private _gloStandardPrintController As gloStandardPrintController = Nothing
        // private Microsoft.Office.Interop.Word.Application oWordApp;
        // private String _PDFFileName = null;
        // Private _oPrintDocument As PrintDocument = Nothing
        private PrinterSettings _PrinterSetting = null;

        private gloExtendedPrinterSettings _ExtendedPrinterSettings = null;
        public bool IsPrintingResumed = false;

        public bool IsPrintingCanceled = false;
        private bool IsPrintingCompletedWhilePressingPause = true;
        //private ArrayList _objwordList = null;
        public DataTable dtSelectPatient = null;
        public Dictionary<Int64, string> dictPatientLetter;
        //bool blntaskidPres = false;
        public bool ChkRemiderForUnSchedle = false;
        public  bool blnBackGroundPrint = false;
        //private Microsoft.Office.Interop.Word.Document oCurDoc;
        private int PrintDocument = 0;
        //private Int64 _PatientID = 0;
        //private string _PatientName = "";
        //private Int64 _TemplateID = 0;
        // private string _TemplateName = "";
        // private Int64 _VisitID = 0;
        // private Int64 _TaskID = 0;
        public bool _bIsUnscheduledCare = false;
        //  private Int64 _nCommunicationTypeID = 0;
        private DateTime clickTime = DateTime.Now;
        private bool blnbtnPauseclicked = false;
        // public long AccountID=0;
        private bool _isFormClose = false;
        public string oldPrinterName = "";
      //  public List<gloOffice.gloTemplate> lstgloTemplate = null;
        public bool bIsBatchGenerate = false;

        public string _databaseConnectionString = "";
        private Control myCaller = null;
             public     List<clsPrintWordQueue> lstgloTemplate = new List<clsPrintWordQueue>() ;
             private bool _blsisprinted = false;  
        //public frmobjgloPrintQueueController(Control myControl = null)
        //{
        //    Shown += frmgloPrintQueueController_Shown;
        //    FormClosed += frmgloPrintQueueController_FormClosed;
        //    myCaller = myControl;
        //    InitializeComponent();

        //}
             public frmgloPrintQueueController(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, Control myControl = null)
             {
                 
                 Shown +=frmgloPrintQueueController_Shown ;
                  FormClosed += frmgloPrintQueueController_FormClosed ;
                 myCaller = myControl;
                 gloPrintWordProgressControllerCall(sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages);

             }



        private  void gloPrintWordProgressControllerCall(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null)
        {
            try
            {
                InitializeComponent();
                this.Text = "Printing";
                //' Me.ControlBox = False
                this.BringToFront();
                _PrinterSetting = sPrinterSettings;
                _ExtendedPrinterSettings = sExtendedPrinterSettings;
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName;

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
        
        public frmgloPrintQueueController()
        {
            InitializeComponent();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {

              
                btnPause.Visible = false;
                btnPlay.Visible = true;
                IsPrintingResumed = true;
                btnRestart.Update();
                btnRestart.ResumeLayout();
             
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
        private delegate void del();
        private EventHandler methodToInvoke;
        public void PrintWithOrWithoutBackground()
        {
            try
            {
                if (_blsisprinted == false)
                {
                    _blsisprinted = true; 
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
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


        private void Print()
        {
            Int64 fromDoc = 0;
            //bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
            short Copies = _PrinterSetting.Copies;
            //Int64 _TransactionID = 0;
            //'
         //   gloOffice.gloTemplate _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //if (myLoadWord == null)
                //{
                //    myLoadWord = new gloWord.LoadAndCloseWord();
                //}
                object missing_new = Type.Missing;
                object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                //for (int iCount = 0; iCount < lstgloTemplate.Count; iCount++)
                //{
                //    _ntempTemplateID = Convert.ToInt64(lstgloTemplate[iCount]);
                //    try
                //    {
                //       // PrintTemplate(_ntempTemplateID, ref myLoadWord);
                //    }
                //    catch (Exception ex1)
                //    {
                //       // MessageBox.Show("Error while Printing.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
                //    }
                //    _ntempTemplateID = 0;


                //}



                try
                {
                    // Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
                    //wordApplication = new Microsoft.Office.Interop.Word.Application();



                    try
                    {
                        //if (bIsBatchGenerate)
                        //{

                       
                            for (int i = 0; i <  1; i++)
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
                                  //  string BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                                  //lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                                  //  gloAuditTrail.gloAuditTrail.PrintLog(" Before Print CALL : " + BatchPatientLog);
                                    clsPrintWordQueue objcls = lstgloTemplate[i]; 

                                    Print(lstgloTemplate[i].FilePath.ToString());
                                    lstgloTemplate.RemoveAt(0);  
                                 


                                    //  prgFileGeneration.Value = i + 1;
                                    //  lblFile.Text = "Printing File " + prgFileGeneration.Value + "/" + lstgloTemplate.Count;
                                    //   this.Invalidate();
                                    // this.Refresh();
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
                            }
                        }
                       
                    
                    catch //(Exception ex1)
                    {
                        // MessageBox.Show(ex1.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    //if (myLoadWord != null)
                    //{
                    //    myLoadWord.CloseApplicationOnly();
                    //}
                    //myLoadWord = null;


                }

                catch (System.Reflection.TargetInvocationException)
                {
                    //  MessageBox.Show(ext.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // this.Close();
                }















            }
            catch (Exception)
            {
                // ex = null;
            }
            finally
            {
                //if (_gloTemplate != null)
                //{
                //    _gloTemplate.Dispose();
                //    _gloTemplate = null;
                //}
                if ((blnBackGroundPrint == false) & _isFormClose == false)
                {
                    _isFormClose = true;
                    this.Close();
                    this.Dispose(true);
                }
                if (lstgloTemplate != null)
                    lstgloTemplate.Clear();
                lstgloTemplate = null;
            }
        }

      //  Wd.Application wdApplication = new Wd.Application();
        gloWord.LoadAndCloseWord myLoadWord = null; 
        public void Print(string filePath)
        {
            //  Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            // bool toQuit = false;
            //if (myLoadWord == null)
            //{
            //    myLoadWord = new gloWord.LoadAndCloseWord();
            //    // toQuit = true;
            //}
            if (myLoadWord == null)
            {
                myLoadWord = new gloWord.LoadAndCloseWord();
                // toQuit = true;
            }
            object Background = false;
            object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
            object Copies = _PrinterSetting.Copies;
            object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
            object PrintToFile = false;
            object Collate = _PrinterSetting.Collate;
            object ActivePrinterMacGX = Type.Missing;
            object ManualDuplexPrint = false;
            object PrintZoomColumn = 1;
            object PrintZoomRow = 1;
            object missing = Type.Missing;
            //object missing_new = Type.Missing;
            //object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            //object templatename = gloSettings.FolderSettings.AppTempFolderPath;
            //   gloOffice.gloTemplate _gloTemplate = null;
            //foreach (gloOffice.gloTemplate template in gloTemplates)
            //{
            try
            {
                //gloOffice.Supporting.DataBaseConnectionString = databasestring;
                //gloOffice.Supporting.PatientID = template.PatientID;

                //gloOffice.Supporting.PrimaryID = template.TemplateID;
                //AccountID = template.nPAccountID;

                //gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                //gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                //Create New Document in Word
                // object missing = System.Reflection.Missing.Value;
                //object fileName = gloOffice.Supporting.GenerateDocumentFile();
               // String strFileName = gloOffice.Supporting.NewDocumentName();
                //try
                //{
                //    System.IO.File.Copy(template.TemplateFilePath, strFileName.ToString());
                //}
                //catch (Exception ex)
                //{
                //    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                //}
             

                object newTemplate = false;
                object docType = 0;
                object isVisible = true;

                // Create a new Document, by calling the Add function in the Documents collection
                Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(filePath,false,false );   //wdApplication.Documents.Open(filePath); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
                       
                //gloOffice.Supporting.PrimaryID = template.PrimeryID;
                //gloOffice.Supporting.WdApplication = aDoc.Application;
                //gloOffice.Supporting.CurrentDocument = aDoc;

                //System.Windows.Forms.Application.DoEvents();
                //gloOffice.Supporting.isFromBatchPrint = true;
                //gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
                //gloOffice.Supporting.isFromBatchPrint = false;
                ////gloWord.gloWord.CurrentDoc = aDoc;
                ////gloWord.gloWord.CleanupDocument();
                ////aDoc = gloWord.gloWord.CurrentDoc;
                //gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
                try
                {
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Save aDoc object start {0} : UserID {1} : UserName {2} : LoginProviderID {3} ",  gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                     aDoc.Save();
                 
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Save aDoc object finish {0} : UserID {1} : UserName {2} : LoginProviderID {3} ",  gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));

                    //if (System.IO.File.Exists(strFileName) == true && System.IO.File.Exists(template.TemplateFilePath) == true)
                    //{
                    //    if (strFileName.ToUpper() != template.TemplateFilePath.ToUpper())
                    //    {
                    //        System.IO.File.Copy(Convert.ToString(strFileName), template.TemplateFilePath, true);
                    //    }
                    //}
                    //else
                    //{
                    //    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Either of source or dest file not exists {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    //}
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure);

                }
                // need to see the created document, so make it visible
                //wordApplication.Visible = true;
                //aDoc.Activate();
                //object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                //object oFileName = (object)template.TemplateFilePath;
                //aDoc.SaveAs(oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Sent Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                if (aDoc != null)
                {
                    if (oldPrinterName != _PrinterSetting.PrinterName)
                    {
                        SetDefaultPrinter(_PrinterSetting.PrinterName);
                        Application.DoEvents();
                    }
                    
                  //gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                  //        ref missing, ref missing, ref missing, ref Copies,
                  //        ref missing, ref missing, ref PrintToFile, ref Collate,
                  //        ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                  //        ref PrintZoomRow, ref missing, ref missing);
                   
                    aDoc.PrintOut ( ref Background, ref missing, ref missing, ref missing,
                         ref missing, ref missing, ref missing, ref Copies,
                         ref missing, ref missing, ref PrintToFile, ref Collate,
                         ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                         ref PrintZoomRow, ref missing, ref missing);
                    myLoadWord.CloseWordOnly(ref aDoc);
                    //aDoc.Application.ActivePrinter = _PrinterSetting.PrinterName;
                    // aDoc.PrintOut();
                }
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Finished Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Called for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
              //  myLoadWord.CloseWordOnly(ref aDoc);
               // gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Finished for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                //   GC.Collect();
                //   GC.WaitForPendingFinalizers();

                try
                {
                    //if (System.IO.File.Exists(strFileName.ToString()))
                    //{
                    //    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    //    System.IO.File.Delete(strFileName.ToString());
                    //    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    //}
                    //else
                    //{
                    //    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    //}
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                    ex = null;
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
                //ex = null;
            }
            finally
            {
              //  gloOffice.Supporting.isFromBatchPrint = false;
                if (oldPrinterName != String.Empty)
                {
                    SetDefaultPrinter(oldPrinterName);
                    Application.DoEvents();
                }
                
            }
            //}



            //if (isTemplateSkipped)
            //{
            //    foreach (PatientMessage oMsg in oPatientMessages)
            //    {
            //        strExcludedTemplates.Append(System.Environment.NewLine);
            //        strExcludedTemplates.Append(oMsg.Message);
            //    }
            //    frmgloMessageBox oMsgForm = new frmgloMessageBox();
            //    oMsgForm.Text = _messageBoxCaption;
            //   oMsgForm.Setmessage(strExcludedTemplates);
            //   oMsgForm.ShowDialog(ParentForm); 

            //    // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
  

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
                if ((pbDocument.Value <= pbDocument.Maximum))
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


        private void btnPlay_Click(object sender, EventArgs e)
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

        private void frmgloPrintQueueController_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_ExtendedPrinterSettings.IsShowProgress)
                {
                    pbDocument.Maximum = 1;//lstgloTemplate.Count;  
                    PrintWithOrWithoutBackground();
                }
                else
                {
                    Hide();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }

        private void frmgloPrintQueueController_FormClosed(object sender, FormClosedEventArgs e)
        {
            methodToInvoke = null;
            if ((myLoadWord != null))
            {
                myLoadWord.CloseApplicationOnly();
                myLoadWord = null;
            }
          
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

        private void frmobjgloPrintQueueController_Load(object sender, EventArgs e)
        {
            try
            {

                if (!_ExtendedPrinterSettings.IsShowProgress)
                {
                    Hide();
                    PrintWithOrWithoutBackground();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }
    }
}
