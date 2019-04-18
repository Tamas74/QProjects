using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using gloCommon;
using Wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Windows.Forms;
using gloWord;
using System.Runtime.InteropServices;

namespace gloOffice
{
    //Bug ID: 00000295 (Printing - EMR)
    //Reason: Make this class as public to access it from gloPMGeneral project.
    //Description: Performance improvement for Check-In Printing issue
    public class clsPatientTemplate
    {
        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);
        string _databaseConnectionString = string.Empty;
        String _messageBoxCaption =string.Empty ;
       public Form ParentForm;
        //Bug #53177: gloPM - 7040 - 00000461 : Printing - EMR - Print wndow pops up again after cancel is clicked
       public string _SelectedPrinter=string.Empty;
       public string _OldDefaultPrinter = string.Empty;
       public bool _CancelPrinting=false;
       object Copies = 1;
       object Collate = false;
        //-------
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //public struct PatientMessage
        //{
        //    public Int64 PatientID;
        //    public StringBuilder Message;
        //}
        List<frmgloPrintWordProgressController.PatientMessage> oPatientMessages = new List<frmgloPrintWordProgressController.PatientMessage>();
        public clsPatientTemplate(string DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
        }

       // bool blnBatchPrintinProgress = false; 
        private bool CheckBatchPrintProcessRunning()
        {

            try
            {


                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {

                    if (oFrm.Name == "frmgloPrintWordProgressController")
                    {
                        DialogResult dg = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if ((dg == DialogResult.Yes))
                        {
                            oFrm.Close();
                           // blnBatchPrintinProgress = false;
                            return false;
                          //  break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            oFrm.Visible = true;
                            return true;
                          //  break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                return false;
            }
            catch //(Exception ex)
            {
                //ex = null;
                return false;
            }
        }

        
        //public void PrintOriginal(List<gloTemplate> gloTemplates, bool IsFromAppointmentTab,Int64 AccountID=0)
        //{
        //    //Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
        //    //wordApplication = new Microsoft.Office.Interop.Word.Application();
        //    gloWord.LoadAndCloseWord myLoadWord = new LoadAndCloseWord();

        //    Boolean isTemplateSkipped =false ;
        //    object missing_new = Type.Missing;
        //    object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        //    StringBuilder strExcludedTemplates = new StringBuilder();
        //  // strExcludedTemplates.Append("Following patient(s) will be excluded from batch printing as they are having multiple accounts :");
        //    strExcludedTemplates.Append(System.Environment.NewLine);
        //    strExcludedTemplates.Append("Attention – Your Appointment for Print selection includes patients with multiple accounts. These patients cannot print as part of a batch and will be excluded from this batch. After this batch print is complete, select each excluded patient one at a time and print. You will be asked to select the correct account for each patient.");
        //    strExcludedTemplates.Append(System.Environment.NewLine);
        //    strExcludedTemplates.Append(System.Environment.NewLine);
        //    strExcludedTemplates.Append("Excluded Patients:");
        //    strExcludedTemplates.Append(System.Environment.NewLine);
        //    foreach (gloTemplate template in gloTemplates)
        //    {
        //        try
        //        {
        //            if (AccountID == 0)
        //            {
        //                if (template.IsPatientHaveMultipleAccounts && template.IsTemplateContainsPatientAccountFields)
        //                {
        //                    AddSkipedTemplateInfo(template.PatientID, template.PatientName, template.TemplateName, template.AppoinmentTime.ToString());
        //                    isTemplateSkipped = true;
        //                    continue;
        //                }
        //            }
        //            gloOffice.Supporting.AppointmentID = 0;
        //            gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
        //            gloOffice.Supporting.PatientID = template.PatientID;

        //            gloOffice.Supporting.PrimaryID = template.TemplateID;

        //            //if (IsFromAppointmentTab == true)
        //            //{ gloOffice.Supporting.FromDate = template.FromDate; }
        //            //else
        //            //{ gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy")); }

        //            //gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    
        //            //Bug #92723: 00001067: Appointment 
        //            gloOffice.Supporting.FromDate = template.FromDate;
        //            gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

        //            //Create New Document in Word
        //         //   object missing = System.Reflection.Missing.Value;
        //            //object fileName = gloOffice.Supporting.GenerateDocumentFile();
        //            String strFileName = gloOffice.Supporting.NewDocumentName();
        //            try
        //            {
        //                System.IO.File.Copy(template.TemplateFilePath, strFileName);
        //            }
        //            catch (Exception ex)
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

        //            }
        //            object fileName = strFileName;

        //            object newTemplate = false;
        //            object docType = 0;
        //            object isVisible = true;

        //            // Create a new Document, by calling the Add function in the Documents collection
        //            Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(strFileName); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

        //            gloOffice.Supporting.PrimaryID = template.PrimeryID;
        //            gloOffice.Supporting.WdApplication = aDoc.Application; // wordApplication;
        //            gloOffice.Supporting.CurrentDocument = aDoc;

        //            System.Windows.Forms.Application.DoEvents();
        //            gloOffice.Supporting.isFromBatchPrint = true;
        //            gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
        //            gloOffice.Supporting.isFromBatchPrint = false;
        //            //gloWord.gloWord.CurrentDoc = aDoc;
        //            //gloWord.gloWord.CleanupDocument();
        //            //aDoc = gloWord.gloWord.CurrentDoc;
        //            gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);  
        //            //// need to see the created document, so make it visible
        //            //wordApplication.Visible = true;
        //            //aDoc.Activate();

        //            DocumentPrintOut(ref aDoc);
        //            myLoadWord.CloseWordOnly(ref aDoc);
        //            //try
        //            //{
        //            //    aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

        //            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

        //            //    aDoc = null;
        //            //}
        //            //catch
        //            //{
        //            //}
                    
        //            //GC.Collect();
        //            //GC.WaitForPendingFinalizers();
        //            try
        //            {
        //                if (System.IO.File.Exists(strFileName))
        //                {
        //                    System.IO.File.Delete(strFileName);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
        //            }

        //            if (_CancelPrinting == true)
        //            {
        //                _CancelPrinting = false;
        //                break;
        //            }
        //        }
        //        catch (System.Runtime.InteropServices.COMException ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        //        }
        //        finally
        //        {
        //            gloOffice.Supporting.isFromBatchPrint = false;
        //        }
        //    }
        //    //wordApplication.Application.Quit(ref saveOptions, ref missing_new, ref missing_new);
        //    //try
        //    //{
        //    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
        //    //    wordApplication = null;
        //    //}
        //    //catch
        //    //{
        //    //}
        //    myLoadWord.CloseApplicationOnly();
        //    myLoadWord = null;
        //    if (isTemplateSkipped)
        //    {
        //        foreach (frmgloPrintWordProgressController.PatientMessage oMsg in oPatientMessages)
        //        {
        //            strExcludedTemplates.Append(System.Environment.NewLine);
        //            strExcludedTemplates.Append(oMsg.Message);
        //        }
        //        frmgloMessageBox oMsgForm = new frmgloMessageBox();
        //        oMsgForm.Text = _messageBoxCaption;
        //       oMsgForm.Setmessage(strExcludedTemplates);
        //       oMsgForm.ShowDialog(ParentForm);
        //       oMsgForm.Dispose();
        //       oMsgForm = null;
        //        // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private static System.Drawing.Printing.PrinterSettings myPrinterSetting = new System.Drawing.Printing.PrinterSettings();


        public void Print(List<gloTemplate> gloTemplates, bool IsFromAppointmentTab, Int64 AccountID = 0, System.Drawing.Printing.PrintDocument printDocument1 = null)
        {
            //  List<gloTemplate> _gloTemplates = new List<gloTemplate>();
            //  _gloTemplates = gloTemplates; 
            if (gloTemplates != null)
            {
                if (gloTemplates.Count > 0)
                {
                    if (CheckBatchPrintProcessRunning() == false)
                    {
                        string OldPrinterName = "";
                        // if (blnBatchPrintinProgress == false)
                        // {
                        //Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
                        //wordApplication = new Microsoft.Office.Interop.Word.Application();
                        // gloWord.LoadAndCloseWord myLoadWord = new LoadAndCloseWord();

                        //  Boolean isTemplateSkipped = false;
                        object missing_new = Type.Missing;
                        object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                        //StringBuilder strExcludedTemplates = new StringBuilder();
                        ////// strExcludedTemplates.Append("Following patient(s) will be excluded from batch printing as they are having multiple accounts :");
                        //strExcludedTemplates.Append(System.Environment.NewLine);
                        //strExcludedTemplates.Append("Attention – Your Appointment for Print selection includes patients with multiple accounts. These patients cannot print as part of a batch and will be excluded from this batch. After this batch print is complete, select each excluded patient one at a time and print. You will be asked to select the correct account for each patient.");
                        //strExcludedTemplates.Append(System.Environment.NewLine);
                        //strExcludedTemplates.Append(System.Environment.NewLine);
                        //strExcludedTemplates.Append("Excluded Patients:");
                        //strExcludedTemplates.Append(System.Environment.NewLine);
                        //    blnBatchPrintinProgress = true;
                        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog(true))
                        {
                            oDialog.ConnectionString = _databaseConnectionString;
                            ////blnBatchPrintinProgress = true;
                            oDialog.TopMost = true;

                            oDialog.ModuleName = "Printing Batch ReferralLetter";

                            oDialog.RegistryModuleName = "PrintBatchDocuments";

                            if (oDialog != null)
                            {
                                oDialog.AllowSomePages = true;
                                oDialog.ShowPrinterProfileDialog = true;
                                if (!gloGlobal.gloTSPrint.isCopyPrint)
                                {
                                    //if (printDocument1 != null)
                                    {
                                        try
                                        {
                                            OldPrinterName = myPrinterSetting.PrinterName;//printDocument1.PrinterSettings.PrinterName;
                                        }
                                        catch
                                        {
                                        }
                                        oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
                                        oDialog.PrinterSettings.ToPage = 1;
                                        //maxPage;
                                        oDialog.PrinterSettings.FromPage = 1;
                                        oDialog.PrinterSettings.MaximumPage = 1;
                                        // maxPage;
                                        oDialog.PrinterSettings.MinimumPage = 1;
                                    }
                                }
                            }
                            oDialog.AllowSomePages = true;

                            if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {

                                if ((oDialog.bUseDefaultPrinter == true))
                                {
                                    oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                                }

                                frmgloPrintWordProgressController ogloPrintProgressController = new frmgloPrintWordProgressController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, null,ParentForm );
                                ogloPrintProgressController.oldPrinterName = OldPrinterName;
                                ogloPrintProgressController.blnPrintgloTemplatesonly = false;
                                ogloPrintProgressController.lstgloTemplate = gloTemplates;
                                ogloPrintProgressController.oPatientMessages = oPatientMessages;
                                ogloPrintProgressController.AccountID = AccountID;

                                ogloPrintProgressController._databaseConnectionString = _databaseConnectionString;
                                if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                {

                                    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                                    {


                                        ogloPrintProgressController.Show();

                                    }
                                    else
                                    {
                                        ogloPrintProgressController.Show();
                                    }
                                    try
                                    {
                                        if (ParentForm != null)
                                        {
                                         
                                            ParentForm.Focus();
                                        }
                                        }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    ogloPrintProgressController.TopMost = true;
                                    ogloPrintProgressController.ShowInTaskbar = false;

                                    ogloPrintProgressController.ShowDialog();
                                    if (ogloPrintProgressController != null)
                                    {
                                        ogloPrintProgressController.Dispose();
                                    }
                                    ogloPrintProgressController = null;


                                }
                            }
                        }
                        //    foreach (gloTemplate template in gloTemplates)
                        //{
                        //    try
                        //    {
                        //        if (AccountID == 0)
                        //        {
                        //            if (template.IsPatientHaveMultipleAccounts && template.IsTemplateContainsPatientAccountFields)
                        //            {
                        //                AddSkipedTemplateInfo(template.PatientID, template.PatientName, template.TemplateName, template.AppoinmentTime.ToString());
                        //                isTemplateSkipped = true;
                        //                continue;
                        //            }
                        //        }
                        //        gloOffice.Supporting.AppointmentID = 0;
                        //        gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
                        //        gloOffice.Supporting.PatientID = template.PatientID;

                        //        gloOffice.Supporting.PrimaryID = template.TemplateID;



                        //        //Bug #92723: 00001067: Appointment 
                        //        gloOffice.Supporting.FromDate = template.FromDate;
                        //        gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                        //        //Create New Document in Word

                        //        String strFileName = gloOffice.Supporting.NewDocumentName();
                        //        try
                        //        {
                        //            System.IO.File.Copy(template.TemplateFilePath, strFileName);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                        //        }
                        //        object fileName = strFileName;

                        //        object newTemplate = false;
                        //        object docType = 0;
                        //        object isVisible = true;

                        //        // Create a new Document, by calling the Add function in the Documents collection
                        //        Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(strFileName); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

                        //        gloOffice.Supporting.PrimaryID = template.PrimeryID;
                        //        gloOffice.Supporting.WdApplication = aDoc.Application; // wordApplication;
                        //        gloOffice.Supporting.CurrentDocument = aDoc;

                        //        System.Windows.Forms.Application.DoEvents();
                        //        gloOffice.Supporting.isFromBatchPrint = true;
                        //        gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
                        //        gloOffice.Supporting.isFromBatchPrint = false;

                        //        gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);

                        //        DocumentPrintOut(ref aDoc);
                        //        myLoadWord.CloseWordOnly(ref aDoc);


                        //        try
                        //        {
                        //            if (System.IO.File.Exists(strFileName))
                        //            {
                        //                System.IO.File.Delete(strFileName);
                        //            }
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                        //        }

                        //        if (_CancelPrinting == true)
                        //        {
                        //            _CancelPrinting = false;
                        //            break;
                        //        }
                        //    }
                        //    catch (System.Runtime.InteropServices.COMException ex)
                        //    {
                        //        System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                        //    }
                        //    finally
                        //    {
                        //        gloOffice.Supporting.isFromBatchPrint = false;
                        //    }
                        //}

                        //myLoadWord.CloseApplicationOnly();
                        //myLoadWord = null;
                        //if (isTemplateSkipped)
                        //{
                        //    foreach (frmgloPrintWordProgressController.PatientMessage oMsg in oPatientMessages)
                        //    {
                        //        strExcludedTemplates.Append(System.Environment.NewLine);
                        //        strExcludedTemplates.Append(oMsg.Message);
                        //    }
                        //    frmgloMessageBox oMsgForm = new frmgloMessageBox();
                        //    oMsgForm.Text = _messageBoxCaption;
                        //    oMsgForm.Setmessage(strExcludedTemplates);
                        //    oMsgForm.ShowDialog(ParentForm);
                        //    oMsgForm.Dispose();
                        //    oMsgForm = null;
                        //    // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        // }

                    }
                }

            }
        }

        private void DocumentPrintOut(ref Wd.Document CurrentDocument)
        {
            //Bug ID: 00000295 (Printing - EMR)
            //Reason: To resolve reported exception.
            //Description: Set background =false referring link http://www.xtremevbtalk.com/showthread.php?t=55010
            object Background = true;
            object Range = Wd.WdPrintOutRange.wdPrintAllDocument;            
            object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
            object PrintToFile = false;            
            object ActivePrinterMacGX = Type.Missing;
            object ManualDuplexPrint = false;
            object PrintZoomColumn = 1;
            object PrintZoomRow = 1;
            object missing = Type.Missing;
            try
            {

                // 00000461 : Batch print templates option doesn't allow user to select printer to print
                bool DefaultPrinter = gloGlobal.gloTSPrint.IsDefaultPrinterOn();//false;

                //if (appSettings["DefaultPrinter"] != null)
                //{
                //    if (appSettings["DefaultPrinter"] != "")
                //    { DefaultPrinter = Convert.ToBoolean(appSettings["DefaultPrinter"]); }
                //    else { DefaultPrinter = false; }
                //}
                //else
                //{ DefaultPrinter = false; }

                //Bug #53177: gloPM - 7040 - 00000461 : Printing - EMR - Print wndow pops up again after cancel is clicked
                // Following If --- Else conditions changed to resolve the issue.
                if (!DefaultPrinter && _SelectedPrinter == string.Empty)
                {
                    //Bug #53176: gloPM-7040 - 00000461 : Printing - EMR - Mouse Pointer shows the processing icon unless user clicks in Printer Option
                    // Mouse Pointer (Cursor) set to resolve the issue.

                    // Default printer is not set, show print dialoge box
                    PrintDialog PrintDialog1 = new PrintDialog();
                    //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                    //_OldDefaultPrinter = PrintDialog1.PrinterSettings.PrinterName;
                    PrintDialog1.UseEXDialog = true;
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        _OldDefaultPrinter = PrintDialog1.PrinterSettings.PrinterName;
                    }
                    //Bug #53446: 7040-gloPM - 00000461 : Printing - EMR - Exception on appointment Check In
                    // ParentForm check for not null to resolve the issue.
                    if (ParentForm != null)
                    {
                        ParentForm.Cursor = Cursors.Default;
                    }
                    if (PrintDialog1.ShowDialog(ParentForm == null?System.Windows.Forms.Form.ActiveForm:ParentForm) == System.Windows.Forms.DialogResult.OK)
                    {
                        Copies = gloGlobal.gloTSPrint.isCopyPrint?(short)1:PrintDialog1.PrinterSettings.Copies;
                        Collate = gloGlobal.gloTSPrint.isCopyPrint? true: PrintDialog1.PrinterSettings.Collate;
                        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            if (_OldDefaultPrinter != PrintDialog1.PrinterSettings.PrinterName)
                            {
                                gloGlobal.gloTSPrint.SetDefaultPrinterSettings(PrintDialog1.PrinterSettings.PrinterName);
                                Application.DoEvents();
                            }
                            _SelectedPrinter = PrintDialog1.PrinterSettings.PrinterName;
                        }
                        //CurrentDocument.Application.Options.PrintBackground = true;
                        //CurrentDocument.PrintOut(ref Background, ref missing, ref missing, ref missing,
                        //ref missing, ref missing, ref missing, ref Copies,
                        //ref missing, ref missing, ref PrintToFile, ref Collate,
                        //ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                        //ref PrintZoomRow, ref missing, ref missing);

                        //System.Threading.Thread.Sleep(1000);
                        gloWord.LoadAndCloseWord.PrintDocument(ref CurrentDocument,ref Background, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref Copies,
                        ref missing, ref missing, ref PrintToFile, ref Collate,
                        ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                        ref PrintZoomRow, ref missing, ref missing);
                       
                        if (ParentForm != null)
                        {
                            ParentForm.Cursor = Cursors.WaitCursor;
                        }
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                            if (_OldDefaultPrinter != PrintDialog1.PrinterSettings.PrinterName)
                            {
                                gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_OldDefaultPrinter);
                                Application.DoEvents();
                            }
                        }
                    }
                    else
                    {
                        _SelectedPrinter = string.Empty;
                        _CancelPrinting = true;
                    }
                    PrintDialog1.Dispose();
                    PrintDialog1 = null;
                }
                     
                else
                {
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (_SelectedPrinter != string.Empty)
                        {
                            //CurrentDocument.Application.ActivePrinter = _SelectedPrinter;
                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_SelectedPrinter);
                            Application.DoEvents();
                        }
                        //CurrentDocument.Application.Options.PrintBackground = true;
                    }
                    //CurrentDocument.PrintOut(ref Background, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing, ref Copies,
                    //ref missing, ref missing, ref PrintToFile, ref Collate,
                    //ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                    //ref PrintZoomRow, ref missing, ref missing);

                    //System.Threading.Thread.Sleep(1000);
                    gloWord.LoadAndCloseWord.PrintDocument(ref CurrentDocument, ref Background, ref missing, ref missing, ref missing,
                      ref missing, ref missing, ref missing, ref Copies,
                      ref missing, ref missing, ref PrintToFile, ref Collate,
                      ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                      ref PrintZoomRow, ref missing, ref missing);
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                        if (_OldDefaultPrinter != String.Empty)
                        {
                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_OldDefaultPrinter);
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        ///// <summary>
        ///// Method not is use, need to test before use.
        ///// </summary>
        ///// <param name="wordApplication"></param>
        //private void DocumentsPrintOut(Microsoft.Office.Interop.Word.Application wordApplication)
        //{
        //    object Background = true;
        //    object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
        //    object Copies = 2;
        //    object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
        //    object PrintToFile = false;
        //    object Collate = false;
        //    object ActivePrinterMacGX = Type.Missing;
        //    object ManualDuplexPrint = false;
        //    object PrintZoomColumn = 1;
        //    object PrintZoomRow = 1;
        //    object missing = Type.Missing;

        //    wordApplication.Documents.Application.Options.PrintBackground = true;
        //    wordApplication.Documents.Application.PrintOut(ref Background, ref missing, ref Range, ref missing, ref missing, ref missing, ref missing, ref Copies, ref missing, ref PageType, ref PrintToFile, ref Collate, ref missing, ref ActivePrinterMacGX, ref ManualDuplexPrint, ref PrintZoomColumn, ref PrintZoomRow, ref missing, ref missing);
        //}

        private bool SavePatientTemplate(gloTemplate _gloTemplate, out string TemplateFilePath)
        {
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            try
            {
                ogloTemplate.ClinicID = _gloTemplate.ClinicID;
                ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                ogloTemplate.TemplateID = _gloTemplate.TemplateID;
                ogloTemplate.PatientID = _gloTemplate.PatientID;
                if ((_gloTemplate.TemplateName == "") || (_gloTemplate.CategoryID == 0) || (_gloTemplate.CategoryName == ""))
                {
                    DataTable dtTemplateDetails = new DataTable();
                    dtTemplateDetails = ogloTemplate.GetTemplateDetails(_gloTemplate.TemplateID);
                    if (dtTemplateDetails != null && dtTemplateDetails.Rows.Count > 0)
                    {
                        ogloTemplate.TemplateName = dtTemplateDetails.Rows[0]["sTemplateName"].ToString();
                        ogloTemplate.CategoryID = Convert.ToInt64(dtTemplateDetails.Rows[0]["nCategoryID"]);
                        ogloTemplate.CategoryName = dtTemplateDetails.Rows[0]["sCategoryName"].ToString();
                    }
                    else
                    {
                        ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                        ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                        ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                    }
                    if (dtTemplateDetails != null) { dtTemplateDetails = null; }
                }
                else
                {
                    ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                    ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                    ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                }

                TemplateFilePath = ogloTemplate.TemplateFilePath;

                long _TransactionID = 0;
                _TransactionID = ogloTemplate.SavePatientTemplate(_TransactionID);

                if (_TransactionID > 0)
                { return true; }
                else
                { return false; }
            }
            catch //(Exception ex)
            {
                TemplateFilePath = null;
                return false;
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate = null; }
            }
        }
        private void AddSkipedTemplateInfo(Int64 PatientID, String PatientName, String TemplateName, String strTime)
        {
            Boolean IspatientExist=false ;
            foreach (frmgloPrintWordProgressController.PatientMessage oMsg in oPatientMessages)
            {
                if (oMsg.PatientID == PatientID)
                {
                    IspatientExist = true;
                    //oMsg.Message.Append(System.Environment.NewLine);
                    //oMsg.Message.Append("              " + TemplateName + "[" + strTime + "]");
                }
            }
            if(IspatientExist==false )
            {
                frmgloPrintWordProgressController.PatientMessage oMessage = new frmgloPrintWordProgressController.PatientMessage();
            oMessage.Message = new StringBuilder();
            oMessage.PatientID = PatientID;
            //oMessage.Message.Append("Patient   : " + PatientName);
            oMessage.Message.Append(PatientName);
            //oMessage.Message.Append(System.Environment.NewLine);
            //oMessage.Message.Append("Template : ");
            //oMessage.Message.Append(System.Environment.NewLine);
            //oMessage.Message.Append("              " + TemplateName+"["+ strTime +"]");
            oPatientMessages.Add( oMessage);
            }
            
 

        }

        //Bug ID: 00000295 (Printing - EMR)
        //Reason: Override print method with single parameter as list of template from frmCheckIn.
        //Description: Performance improvement for Check-In Printing issue
        /// <summary>
        /// This method is used for print check-in template.
        /// </summary>
        /// <param name="gloTemplates">List of templates.</param>
        public void Print(List<gloTemplate> gloTemplates, System.Drawing.Printing.PrintDocument printDocument1 = null)
        {
            //Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            //wordApplication = new Microsoft.Office.Interop.Word.Application();
            //gloWord.LoadAndCloseWord myLoadWord = new gloWord.LoadAndCloseWord();
            //object missing_new = Type.Missing;
            //object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            string OldPrinterName = "";
            if (gloTemplates != null)
            {
                if (gloTemplates.Count > 0)
                {
                    try
                    {
                        if (CheckBatchPrintProcessRunning() == false)
                        {
                            // if (blnBatchPrintinProgress == false)
                            // {

                            using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog(true))
                            {
                                oDialog.ConnectionString = _databaseConnectionString;
                                ////blnBatchPrintinProgress = true;
                                oDialog.TopMost = true;

                                oDialog.ModuleName = "Printing Batch ReferralLetter";

                                oDialog.RegistryModuleName = "PrintBatchDocuments";

                                if (oDialog != null)
                                {
                                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                                    {
                                        //if (printDocument1 != null)
                                        {
                                            try
                                            {
                                                OldPrinterName = myPrinterSetting.PrinterName;//printDocument1.PrinterSettings.PrinterName;
                                            }
                                            catch
                                            {
                                            }
                                            oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
                                            oDialog.PrinterSettings.ToPage = 1;
                                            //maxPage;
                                            oDialog.PrinterSettings.FromPage = 1;
                                            oDialog.PrinterSettings.MaximumPage = 1;
                                            // maxPage;
                                            oDialog.PrinterSettings.MinimumPage = 1;
                                        }
                                    }
                                    oDialog.AllowSomePages = true;
                                    oDialog.ShowPrinterProfileDialog = true;

                                }
                                oDialog.AllowSomePages = true;
                                //  blnBatchPrintinProgress = true;
                                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {

                                    if ((oDialog.bUseDefaultPrinter == true))
                                    {
                                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                                    }

                                    frmgloPrintWordProgressController ogloPrintProgressController = new frmgloPrintWordProgressController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, null,ParentForm );
                                    ogloPrintProgressController.oldPrinterName = OldPrinterName;
                                    ogloPrintProgressController.blnPrintgloTemplatesonly = true;
                                    ogloPrintProgressController.lstgloTemplate = gloTemplates;
                                    ogloPrintProgressController.oPatientMessages = oPatientMessages;
                                    ogloPrintProgressController.AccountID = 0;

                                    ogloPrintProgressController._databaseConnectionString = _databaseConnectionString;
                                    if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                    {

                                        if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                                        {


                                            ogloPrintProgressController.Show();

                                        }
                                        else
                                        {
                                            ogloPrintProgressController.Show();
                                        }
                                        if (ParentForm != null)
                                        {
                                            try
                                            {
                                                ParentForm.Focus();
                                            }
                                            catch
                                            {
                                            }
                                    }
                                    }
                                    else
                                    {
                                        ogloPrintProgressController.TopMost = true;
                                        ogloPrintProgressController.ShowInTaskbar = false;

                                        ogloPrintProgressController.ShowDialog();
                                        if (ogloPrintProgressController != null)
                                        {
                                            ogloPrintProgressController.Dispose();
                                        }
                                        ogloPrintProgressController = null;


                                    }
                                }
                            }
                        }
                    }
                    //foreach (gloTemplate template in gloTemplates)
                    //{
                    //    try
                    //    {
                    //        gloOffice.Supporting.AppointmentID = 0;
                    //        gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
                    //        gloOffice.Supporting.PatientID = template.PatientID;

                 //        gloOffice.Supporting.PrimaryID = template.TemplateID;

                 //        gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    //        gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                 //        //Create New Document in Word
                    //      //  object missing = System.Reflection.Missing.Value;
                    //        String strFileName = gloOffice.Supporting.NewDocumentName();
                    //        try
                    //        {
                    //            System.IO.File.Copy(template.TemplateFilePath, strFileName);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                 //        }
                    //        object fileName = strFileName;

                 //        object newTemplate = false;
                    //        object docType = 0;
                    //        object isVisible = true;

                 //        // Create a new Document, by calling the Add function in the Documents collection
                    //        Microsoft.Office.Interop.Word.Document aDoc =myLoadWord.LoadWordApplication(strFileName) ; //wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

                 //        gloOffice.Supporting.PrimaryID = template.PrimeryID;
                    //        gloOffice.Supporting.WdApplication = aDoc.Application;
                    //        gloOffice.Supporting.CurrentDocument = aDoc;

                 //        System.Windows.Forms.Application.DoEvents();
                    //        //pass the AccountID = 0 as it also pass as 0 in old logic from frmCheckIn.cs in PrintTemplate() method.
                    //        gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, 0);
                    //        //gloWord.gloWord.CurrentDoc = aDoc;
                    //        //gloWord.gloWord.CleanupDocument();
                    //        //aDoc = gloWord.gloWord.CurrentDoc;
                    //        gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);  
                    //        DocumentPrintOut(ref aDoc);
                    //        myLoadWord.CloseWordOnly(ref aDoc);
                    //        //try
                    //        //{
                    //        //    aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

                 //        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

                 //        //    aDoc = null;
                    //        //}
                    //        //catch
                    //        //{
                    //        //}
                    //        //GC.Collect();
                    //        //GC.WaitForPendingFinalizers();
                    //        try
                    //        {
                    //            if (System.IO.File.Exists(strFileName))
                    //            {
                    //                System.IO.File.Delete(strFileName);
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    //        }
                    //    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "EMR");
                    }
                    finally
                    {
                        //gloOffice.Supporting.isFromBatchPrint = false;
                    }
                    //   }
                    // myLoadWord.CloseApplicationOnly();
                    // myLoadWord = null;
                    //wordApplication.Application.Quit(ref saveOptions, ref missing_new, ref missing_new);
                    //try
                    //{
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
                    //    wordApplication = null;
                    //}
                    //catch
                    //{
                    //}


                }
            }

        }

    }
}
