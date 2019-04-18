//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace gloOffice
//{
//    public partial class frmgloPrintWordController : Form
//    {
//        public frmgloPrintWordController()
//        {
//            InitializeComponent();
//        }
//    }
//}

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
namespace gloBilling.Collections
{
    public partial class frmgloPrintSetupDateActionTemplate : Form
    {
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);
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
        public static bool blnBackGroundPrint = false;
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
     public    List<gloOffice.gloTemplate> lstgloTemplate = null;
     public bool bIsBatchGenerate = false;
     private gloClinicalQueueGeneral.QueueDocumentDocumentDetails _popUpDetails = null;

      public string _databaseConnectionString = "";
        private Control myCaller = null;
        gloWord.LoadAndCloseWord myLoadWord =null; 
        public frmgloPrintSetupDateActionTemplate(Control myControl = null)
        {
          
            Shown += frmgloPrintWordProgressController_Shown;
            FormClosed += frmgloPrintWordProgressController_FormClosed;
            myCaller = myControl;
            InitializeComponent();

        }
        public frmgloPrintSetupDateActionTemplate(PrinterSettings sPrinterSettings, gloExtendedPrinterSettings sExtendedPrinterSettings, ArrayList oSourceDocSelectedPages = null, Control myControl = null)
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
                gloGlobal.LoadFromAssembly.PrintMenuEventclick(false, "frmgloPrintSetupDateActionTemplate");


                //dynamic frm = Application.OpenForms["frmDashBoardMain"];
                //if (frm != null)
                //{
                //    frm.PrintMenuEventclick(false, "frmgloPrintSetupDateActionTemplate");
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
                if (lstgloTemplate != null)
                {
                    lstgloTemplate.Clear();
                    lstgloTemplate = null;
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
                    if (oldPrinterName != String.Empty)
                    {
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName);
                        Application.DoEvents();
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
            myLoadWord =  new gloWord.LoadAndCloseWord();
                if (lstgloTemplate != null)
                {
                    pbDocument.Maximum = lstgloTemplate.Count;
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

                 //dynamic  frm=   Application.OpenForms["frmDashBoardMain"];
                 //if (frm != null)
                 //{

                 //    frm.PrintMenuEventclick(true, "frmgloPrintSetupDateActionTemplate");
                 //}
                    gloGlobal.LoadFromAssembly.PrintMenuEventclick(true, "frmgloPrintSetupDateActionTemplate");


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
     //   bool gblnPageNo = true;
    
        
        
        
        
        
        



        //long _ntempTemplateID = 0;
  private void Print()
{
	Int64 fromDoc = 0;
	//bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
	//short Copies = gloGlobal.gloTSPrint.isCopyPrint?(short)1:_PrinterSetting.Copies;
    Int64 _TransactionID = 0;
	//'
    gloOffice.gloTemplate _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
    try
    {
        if (myLoadWord == null)
        {
            myLoadWord = new gloWord.LoadAndCloseWord();
        }
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
                string newpatientTemplateFilePath = string.Empty;
                if (bIsBatchGenerate)
                {


                    for (int i = 0; i <= lstgloTemplate.Count - 1; i++)
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
                            string BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                          lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" Before Print CALL : " + BatchPatientLog);

                            gloOffice.Supporting.FieldID2 = lstgloTemplate[i].TransactionID;
                            gloOffice.Supporting.FieldID3 = lstgloTemplate[i].TransactionMstID;

                            Print(ref myLoadWord, lstgloTemplate[i], false, 0, gloGlobal.gloPMGlobal.DatabaseConnectionString);

                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                          lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" End Print CALL : " + BatchPatientLog);


                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                               lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" Before Save Called for Printed document against Patient : " + BatchPatientLog);


                             _TransactionID = _gloTemplate.SavePatientTemplate(0, lstgloTemplate[i]);


                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                          lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" End Save Called for Printed document against Patient : " + BatchPatientLog);

                            newpatientTemplateFilePath = string.Empty;
                            newpatientTemplateFilePath = Convert.ToString(lstgloTemplate[i].TemplateFilePath);

                            try
                            {
                                if (System.IO.File.Exists(newpatientTemplateFilePath))
                                {
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                    System.IO.File.Delete(newpatientTemplateFilePath);
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                }
                                else
                                {
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                }
                            }
                            catch (Exception ex)
                            {
                                newpatientTemplateFilePath = string.Empty;
                                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                                ex = null;
                            }

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
                else
                {
                    // int itemp = 0;
                    foreach (gloOffice.gloTemplate template in lstgloTemplate)
                    {
                        if (fromDoc < PrintDocument)
                        {
                            fromDoc += 1;
                        }
                        else
                        {
                            string BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : bIsBatchGenerate {7}  : UserID  {8} :UserName {9} : LoginProviderID {10}",
                            template.TemplateFilePath, template.TemplateName, template.TemplateID, template.PatientName, template.PatientID, fromDoc, lstgloTemplate.Count, !bIsBatchGenerate, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" Before Print CALL : " + BatchPatientLog);

                            Print(ref myLoadWord, template, false, 0, gloGlobal.gloPMGlobal.DatabaseConnectionString);

                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : bIsBatchGenerate {7} : UserID  {8} :UserName {9} : LoginProviderID {10}",
                            template.TemplateFilePath, template.TemplateName, template.TemplateID, template.PatientName, template.PatientID, fromDoc, lstgloTemplate.Count, !bIsBatchGenerate, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" End Print CALL : " + BatchPatientLog);


                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : bIsBatchGenerate {7} : UserID  {8} :UserName {9} : LoginProviderID {10}",
                            template.TemplateFilePath, template.TemplateName, template.TemplateID, template.PatientName, template.PatientID, fromDoc, lstgloTemplate.Count, !bIsBatchGenerate, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" Before Save Called for Printed document against Patient : " + BatchPatientLog);

                              _TransactionID = _gloTemplate.SavePatientTemplate(0, template);

                            BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : bIsBatchGenerate {7} : UserID  {8} : UserName {9} : LoginProviderID {10}",
                            template.TemplateFilePath, template.TemplateName, template.TemplateID, template.PatientName, template.PatientID, fromDoc, lstgloTemplate.Count, !bIsBatchGenerate, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            gloAuditTrail.gloAuditTrail.PrintLog(" End Save Called for Printed document against Patient : " + BatchPatientLog);

                            newpatientTemplateFilePath = string.Empty;
                            newpatientTemplateFilePath = Convert.ToString(template.TemplateFilePath);

                            try
                            {
                                if (System.IO.File.Exists(newpatientTemplateFilePath))
                                {
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                    System.IO.File.Delete(newpatientTemplateFilePath);
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                }
                                else
                                {
                                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", newpatientTemplateFilePath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                                }
                            }
                            catch (Exception ex)
                            {
                                newpatientTemplateFilePath = string.Empty;
                                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                                ex = null;
                            }
                            
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

        catch (System.Reflection.TargetInvocationException )
        {
          //  MessageBox.Show(ext.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            // this.Close();
        }















    }
    catch (Exception )
    {
       // ex = null;
    }
    finally
    {
        if (_gloTemplate != null)
        {
            _gloTemplate.Dispose();  
            _gloTemplate = null;
        }
            if ((blnBackGroundPrint == false) & _isFormClose == false)
        {
            _isFormClose = true;
            this.Close();
            this.Dispose(true);
        }
    }
}
  public  void Print(ref gloWord.LoadAndCloseWord myLoadWord, gloOffice.gloTemplate template, bool IsFromAppointmentTab, Int64 AccountID = 0, string databasestring = "")
  {
      //  Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
     // bool toQuit = false;
      if (myLoadWord == null)
      {
          myLoadWord = new gloWord.LoadAndCloseWord();
         // toQuit = true;
      }
      object Background = false ;
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
      //object missing_new = Type.Missing;
      //object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
      //object templatename = gloSettings.FolderSettings.AppTempFolderPath;
      //   gloOffice.gloTemplate _gloTemplate = null;
      //foreach (gloOffice.gloTemplate template in gloTemplates)
      //{
      try
      {
          gloOffice.Supporting.DataBaseConnectionString = databasestring;
          gloOffice.Supporting.PatientID = template.PatientID;

          gloOffice.Supporting.PrimaryID = template.TemplateID;
          AccountID = template.nPAccountID;

          gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
          gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

          //Create New Document in Word
          // object missing = System.Reflection.Missing.Value;
          //object fileName = gloOffice.Supporting.GenerateDocumentFile();
          String strFileName = gloOffice.Supporting.NewDocumentName();
          try
          {
              System.IO.File.Copy(template.TemplateFilePath, strFileName.ToString());
              template.TemplateFilePath = strFileName;
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
          gloOffice.Supporting.WdApplication = aDoc.Application;
          gloOffice.Supporting.CurrentDocument = aDoc;

          System.Windows.Forms.Application.DoEvents();
          gloOffice.Supporting.isFromBatchPrint = true;
          gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
          gloOffice.Supporting.isFromBatchPrint = false;
          //gloWord.gloWord.CurrentDoc = aDoc;
          //gloWord.gloWord.CleanupDocument();
          //aDoc = gloWord.gloWord.CurrentDoc;
          gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
          try
          {
              gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Save aDoc object start {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
              aDoc.Save();
              gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Save aDoc object finish {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));

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
              //aDoc.Application.ActivePrinter = _PrinterSetting.PrinterName;
              // aDoc.PrintOut();
          }
          gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Finished Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Called for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          myLoadWord.CloseWordOnly(ref aDoc);
          gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Finished for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          //   GC.Collect();
          //   GC.WaitForPendingFinalizers();

          //try
          //{
          //    if (System.IO.File.Exists(strFileName.ToString()))
          //    {
          //        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          //        System.IO.File.Delete(strFileName.ToString());
          //        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          //    }
          //    else
          //    {
          //        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
          //    }
          //}
          //catch (Exception ex)
          //{
          //    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
          //    ex = null;
          //}
      }
      catch (System.Runtime.InteropServices.COMException ex)
      {
          //  System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
          gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
          ex = null;
      }
      catch (Exception )
      {
          //ex = null;
      }
      finally
      {
          gloOffice.Supporting.isFromBatchPrint = false;
          if (!gloGlobal.gloTSPrint.isCopyPrint)
          {
              if (oldPrinterName != String.Empty)
              {
                  gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName);
                  Application.DoEvents();
              }
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
  

  //public bool ConvertBinaryToFile(object StreamData, string FilePath)
  //{
  //    bool _result = false;
  //    //  string _FilePath = FilePath;
  //    try
  //    {
  //        if (StreamData != null && StreamData != DBNull.Value)
  //        {
  //            Byte[] byteRead = (byte[])StreamData;
  //            //  MemoryStream oDataStream = new MemoryStream(byteRead);
  //            FileStream oFile = new FileStream(FilePath, FileMode.Create);
  //            oFile.Write(byteRead, 0, byteRead.Length);
  //            // oDataStream.WriteTo(oFile);
  //            //oDataStream.Close();
  //            //oDataStream.Dispose();
  //            //oDataStream = null;
  //            oFile.Close();
  //            oFile.Dispose();
  //            oFile = null;
  //            // oFile.Dispose();
  //            _result = true;
  //        }
  //    }
  //    catch (Exception ex)
  //    {
  //      //  MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
  //    }
  //    return _result;
  //}
  //string TemplateName = "";
  //public void PrintTemplate(Int64 _TransactionID, ref gloWord.LoadAndCloseWord myLoadWord)
  //{
  //    DataTable dttemp = null;//SLR: new is not needed
  //    object Visible = false;
  //    try
  //    {
  //        dttemp = GetPatientTemplate(_TransactionID);

  //        if (dttemp != null && dttemp.Rows.Count > 0)
  //        {
  //            //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
  //            //PatientID = Convert.ToInt64(dttemp.Rows[0]["nPatientID"].ToString());
  //            //PrimeryID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
  //            //CategoryID = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
  //            //CategoryName = dttemp.Rows[0]["sCategoryName"].ToString();
  //            //TemplateID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
  //            TemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
  //           // FromDate = Convert.ToInt32(dttemp.Rows[0]["nFromDate"]);

  //            //Set the File to control
  //            string strNewDocumentName = "";
  //            strNewDocumentName = gloOffice.Supporting.NewDocumentName();

  //            object objTemplateDocument;

  //            if (!string.IsNullOrEmpty(dttemp.Rows[0]["iTemplate"].ToString()))
  //            {
  //                objTemplateDocument = dttemp.Rows[0]["iTemplate"];

  //                //Bug #63771: 00000624: Patient Statement Print
  //                //When you click the 'Print' button, nothing happens at all.
  //                //While click on Reprint batch nothing is happens.
  //                if (TemplateName.Contains("PatientStatement"))
  //                { strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc"); }

  //                ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);

  //                Wd.Document oTemp = myLoadWord.LoadWordApplication(strNewDocumentName); // oWordApp.Documents.Open(strNewDocumentName);

  //                //oTemp.Application.Options.PrintBackground = true;
  //                //oTemp.PrintOut(Background: true);
  //               // gloWord.LoadAndCloseWord.PrintWordDocument(ref oTemp, false);
  //                if ((oTemp != null))
  //                {
  //                    oTemp.Application.ActivePrinter = _PrinterSetting.PrinterName;
  //                    oTemp.PrintOut();
  //                }
  //                myLoadWord.CloseWordOnly(ref oTemp);

  //                //oTemp.Close(SaveChanges:false);
  //                //try
  //                //{
  //                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTemp);
  //                //    oTemp = null;
  //                //}
  //                //catch
  //                //{
  //                //}
  //            }
  //        }
  //    }
  //    catch (Exception Ex)
  //    {
  //        gloAuditTrail.gloAuditTrail.PrintLog(Ex.ToString());
  //        Ex = null;
  //    }
  //    finally
  //    {
  //        dttemp.Dispose(); dttemp = null;

  //        //ogloTemplate.Dispose(); 
  //    }
  //}



 

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
              //  _printPageIndex = 0;
                PrintDocument = 0;

                btnRestart.Enabled = false;
                //If btnPause.Text = "&Resume" Then
                //    btnPause.Text = "&Pause"
                //    IsPrintingResumed = False
                //End If

                 btnPlay.Visible = false ;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false );
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

      //  private bool IsInsidePrinting = false;
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
               
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false );
                ex = null;
            }

        }
    }


}
