using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using pdftron.PDF;
using gloEDocumentV3.DocumentContextMenu;
using gloEDocumentV3.Enumeration;

    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Print : Form
        {
            #region " Constructor "

            public frmEDocEvent_Print()
            {
                _SelectedDocuments = new eContextDocuments();
                InitializeComponent();
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {
                    if (myPrinterSetting == null)
                    {
                        myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                    }
                }
            } 

            #endregion
            private bool bDefaultDPI = true;
            private string sCustomDIPValue = "150";

            #region "Page Manuipulation Variables"
            
            public bool oDialogResultIsOK = false;
            public Int64 oClinicID = 0;
            private eContextDocuments _SelectedDocuments = null;

            pdftron.PDF.PDFDoc oPDFDoc = null;
         //   pdftron.PDF.PageIterator print_page_itr;
          //  pdftron.PDF.PDFDraw pdfdraw = null;
            private static System.Drawing.Printing.PrinterSettings myPrinterSetting = null;
                       
            //gloEDocumentV3.Document.BasePages oBasePages = null;
        //    private int _printPageIndex = 0;
            public Int64 oSourceDocPatientID = 0;
            public Int64 oSourceDocContainerID = 0;
            public Int64 oSourceDocDocumentID = 0;
            public string oSourceDocCategory = "";
            public string oSourceDocYear = "";
            public string oSourceDocMonth = "";
            public string oSourceDocDocumentName = "";
            public ArrayList oSourceDocSelectedPages = new ArrayList();
            public enum_DocumentColumnType oSourceDocColumnType = enum_DocumentColumnType.None;
            public string _ErrorMessage = "";

            public int _nPagesCnt = 0;
            string FileName = string.Empty;

            public enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;

            #endregion

            #region " Property Procedures "

            public eContextDocuments oSelectedDocuments
            {
                get { return _SelectedDocuments; }
                set { _SelectedDocuments = value; }
            }

            #endregion
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
            private void frmEDocEvent_Print_Load(object sender, EventArgs e)
            {
                this.Visible = false;
                PrintDocuments();
                this.Close();
                //txtPrintStatus.Visible = true;
                //txtPrintStatus.Text = "";
                //this.Select();
                //this.BringToFront();
                //pbDocument.Minimum = 0;
                //pbDocument.Maximum = 100;
                //pbDocument.Value = 0;
                ////pbDocument.Step = 25;
              
                // //rbCustomDPI.Checked = true;
                // //numCustomDPIValue.Value = 150;
                ////#CR380 Added Code While printing from scan doc DPI Setting not respecting
                //getDefaultDPISetting();

            }

            private void tlb_Cancel_Click(object sender, EventArgs e)
            {
                oDialogResultIsOK = false;
                this.Close();
            }

            private void tlb_Ok_Click(object sender, EventArgs e)
            {
                
                try
                {
                    pnlDPISetting.Enabled = false;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Print  - Start" + " " + DateTime.Now.TimeOfDay);
                    Application.DoEvents();
                    tlb_Print.Enabled = false;
                    tlb_Cancel.Enabled = false;
                    Application.DoEvents();
                    PrintDocuments();
                    UpdateDPISetting();
                    this.Close();
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Print  - End" + " " + DateTime.Now.TimeOfDay);
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

                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    tlb_Print.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    pnlDPISetting.Enabled = true;
                }
            }

            void oDocManager_DocumentProgressEvent(int Percentage, string Message)
            {
                Application.DoEvents();
                int _PVal = 0;
                _PVal = pbDocument.Value + Percentage;
                if (_PVal <= pbDocument.Maximum) { pbDocument.Value = _PVal; }
            }

            #region "Print with pdftron"

            public void PrintDocuments()
            {
               // gloAuditTrail.gloAuditTrail.PrintLog("Print  - Start");
                
                gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                //Byte[] byteRead = null;
                 string _retFilepath = null;
                 Boolean bIsSelectedPages = false;
                try
                {
                    pdftron.PDFNet.SetResourcesPath(gloEDocV3Admin.gPDFTronResourcePath);

                    for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                    {
                        for (int j = 0; j <= oSelectedDocuments[i].Containers.Count - 1; j++)
                        {
                            oSourceDocSelectedPages.Clear();
                            //Moved GetcontainerStream code outside loop,so that query will not execute per page.-Added on 2025-02-13
                            _retFilepath = oList.GetContainerStream(oSelectedDocuments[i].DocumentID, oSelectedDocuments[i].Containers[j].ContainerID, gloEDocV3Admin.gClinicID, true, _OpenExternalSource);
                            //Moved below code outside loop to avoid memory usage problems
                            if (_retFilepath != null && _retFilepath != "")
                            {
                                oPDFDoc = new PDFDoc(_retFilepath);
                                for (int k = 0; k <= oSelectedDocuments[i].Containers[j].Pages.Count - 1; k++)
                                {
                                    oSourceDocPatientID = oSelectedDocuments[i].PatientID;
                                    oSourceDocDocumentID = oSelectedDocuments[i].DocumentID;
                                    oSourceDocDocumentName = oSelectedDocuments[i].DocumentName;
                                    oSourceDocCategory = oSelectedDocuments[i].Category;
                                    oSourceDocYear = oSelectedDocuments[i].Year;
                                    oSourceDocMonth = oSelectedDocuments[i].Month;
                                    oSourceDocContainerID = oSelectedDocuments[i].Containers[j].ContainerID;
                                    //   gloAuditTrail.gloAuditTrail.PrintLog("Adding PDF Stream " + i.ToString() + " " + j.ToString() + " " + k.ToString());
                                    oSourceDocSelectedPages.Add(oSelectedDocuments[i].Containers[j].Pages[k].ContainerPageNumber);


                                }
                            }
                            txtPrintStatus.AppendText("Document Name : " + oSelectedDocuments[i].DocumentName);
                            txtPrintStatus.AppendText(Environment.NewLine);
                            txtPrintStatus.AppendText("Printing Pages of Container"+ (j+1).ToString()+" ..." );
                            txtPrintStatus.AppendText(Environment.NewLine);
                            _nPagesCnt = oSourceDocSelectedPages.Count;
                            //pbDocument.Maximum = _nPagesCnt ;
                       //     gloAuditTrail.gloAuditTrail.PrintLog("Starting PDF Print " + i.ToString()+" "+j.ToString() );
                            //Print(oSelectedDocuments[i].DocumentName+" "+ (j+1).ToString());

                            if (oSelectedDocuments[i].PageCount != oSourceDocSelectedPages.Count)
                            { bIsSelectedPages = true; }

                            Print(oPDFDoc,bIsSelectedPages);
                        //    gloAuditTrail.gloAuditTrail.PrintLog("Finishing PDF Print " + i.ToString() + " " + j.ToString());
                            //Disposed object
                            if (oPDFDoc != null) { oPDFDoc.Dispose(); oPDFDoc = null; }
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

                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (oList != null) { oList.Dispose(); }
                    if (oPDFDoc != null) { oPDFDoc.Dispose(); }
                }
            }

           
            private void Print(PDFDoc doc, Boolean bIsSelectedPages)
            {
                gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;
                
                try
                {
                    using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                    {
                        oDialog.ConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;
                        oDialog.TopMost = true;
                        oDialog.ShowPrinterProfileDialog = true;
                        //oDialog.AllowSomePages = true;

                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        {
                            oDialog.ModuleName = "PrintRCMDocuments";
                            oDialog.RegistryModuleName = "RCMDocuments";
                        }
                        else
                        {
                            oDialog.ModuleName = "PrintDMSDocuments";
                            oDialog.RegistryModuleName = "DMSDocuments";
                        }

                        if (oDialog != null)
                        {

                            doc.Lock();
                            int maxPage = doc.GetPageCount();

                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                oDialog.PrinterSettings = myPrinterSetting; //printDocument1.PrinterSettings;

                                if (bIsSelectedPages)
                                {
                                    oDialog.AllowSelection = true;
                                    //PrintDialog1.AllowSelection = true;
                                }
                                else
                                {
                                    oDialog.AllowSomePages = true;

                                    oDialog.PrinterSettings.ToPage = maxPage;
                                    oDialog.PrinterSettings.FromPage = 1;
                                    oDialog.PrinterSettings.MaximumPage = maxPage;
                                    oDialog.PrinterSettings.MinimumPage = 1;

                                    //PrintDialog1.AllowSomePages = true;
                                    //PrintDialog1.PrinterSettings.ToPage = maxPage;
                                    //PrintDialog1.PrinterSettings.FromPage = 1;
                                    //PrintDialog1.PrinterSettings.MaximumPage = maxPage;
                                    //PrintDialog1.PrinterSettings.MinimumPage = 1;
                                }
                            }
                            //26-May-16 Aniket: Resolving Bug #96494: gloEMR : RCM Docs (Background printing) : application loses focus as user click on cancel button
                            if (oDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                if (!gloGlobal.gloTSPrint.isCopyPrint)
                                {
                                    myPrinterSetting = oDialog.PrinterSettings;
                                }
                                if (bIsSelectedPages)
                                {
                                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController
                                                                  (doc, doc.GetFileName(), oDialog.PrinterSettings,
                                                                  oDialog.CustomPrinterExtendedSettings, oSourceDocSelectedPages, blnautoLandScape : false);
                                }
                                else
                                {
                                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController
                                                                  (doc, doc.GetFileName(), oDialog.PrinterSettings,
                                                                  oDialog.CustomPrinterExtendedSettings, blnautoLandScape: false);
                                }
                                //ogloPrintProgressController.AutoLandscape = false;
                                ogloPrintProgressController.bIsFromScanDoc = true;
                                ogloPrintProgressController.ShowProgress(this);
                                //if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                //{
                                //    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                                //    {
                                //        ogloPrintProgressController.Show();
                                //    }
                                //    else
                                //    {
                                //        ogloPrintProgressController.Show();
                                //    }
                                //}
                                //else
                                //{
                                //    ogloPrintProgressController.TopMost = true;
                                //    ogloPrintProgressController.ShowInTaskbar = false;

                                //    ogloPrintProgressController.ShowDialog(this);
                                //    if (ogloPrintProgressController != null)
                                //    {
                                //        ogloPrintProgressController.Dispose();
                                //    }
                                //    ogloPrintProgressController = null;
                                //}


                            }//if
                            doc.Unlock();
                            //   PrintDialog1.Dispose();
                            //   PrintDialog1 = null;
                        }
                        else
                        {
                            string _ErrorMessage = "Error in Showing Print Dialog";

                            if (_ErrorMessage.Trim() != "")
                            {
                                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                _MessageString = "";
                            }


                            MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ////}
                        ////else
                        ////{
                        ////    // printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                        ////    if (myPrinterSetting == null)
                        ////    {
                        ////        myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                        ////    }

                        ////    if (myPrinterSetting != null)
                        ////    {
                        ////        try
                        ////        {
                        ////            printDocument1.PrinterSettings = myPrinterSetting;
                        ////        }
                        ////        catch
                        ////        {
                        ////            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
                        ////        }
                        ////    }


                        ////oDialogResultIsOK = true;

                     //   pdfdraw = new pdftron.PDF.PDFDraw();
                        //if (rect != null)
                        //{
                        //    rect.Dispose();
                        //    rect = null;
                        //}
                        ////  printDocument1.DocumentName = strFileName;
                        //printDocument1.Print();
                        //   myPrinterSetting = null;
                        //if (pdfdraw != null)
                        //{
                        //    pdfdraw.Dispose();
                        //    pdfdraw = null;
                        //}
                        //if (rect != null)
                        //{
                        //    rect.Dispose();
                        //    rect = null;
                        //}
                    }

                }
                catch (Exception ex)
                {
                    #region " Make Log Entry "

                    string _ErrorMessage = ex.ToString();
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

                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;
                }
                finally
                {
                    //if (pdfdraw != null)
                    //{
                    //    pdfdraw.Dispose();
                    //    pdfdraw = null;
                    //}
                    //if (rect != null)
                    //{
                    //    rect.Dispose();
                    //    rect = null;
                    //}
                    //if (PrintDialog1 != null)
                    //{
                    //   PrintDialog1.Dispose();
                    //   PrintDialog1 = null;
                    //}
                    //PageArray.Clear();
                }

            }
            //List<int> PageArray = new List<int> { };
            //private void PopulatePrinterPageArray(int maxPage, System.Drawing.Printing.PrinterSettings printerSettings)
            //{

            //    PageArray.Clear();
            //    bool collate = printerSettings.Collate;
            //    int copies = printerSettings.Copies;
            //    int from = printerSettings.FromPage;
            //    int to = printerSettings.ToPage;
            //    if (from < 1) from = 1;
            //    if (to > maxPage) to = maxPage;
            //    if (collate)
            //    {
            //        for (int iPage = from; iPage <= to; iPage++)
            //        {
            //            for (int icopies = 0; icopies < copies; icopies++)
            //            {
            //                PageArray.Add(iPage);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int icopies = 0; icopies < copies; icopies++)
            //        {
            //            for (int iPage = from; iPage <= to; iPage++)
            //            {
            //                PageArray.Add(iPage);
            //            }
            //        }

            //    }
            //}
            //private void PrintOld(string strFileName)
            //{
            //    try
            //    {
            //        //System.Drawing.Printing.StandardPrintController oPrintController = new System.Drawing.Printing.StandardPrintController();
            //        //printDocument1.PrintController = oPrintController;

            //        if (gloEDocV3Admin.blnUseDefaultPrinterDialog == false)
            //        {

            //            // PrintDialog1 = new PrintDialog();
            //            if (PrintDialog1 != null)
            //            {
            //                if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //                {
            //                    myPrinterSetting = PrintDialog1.PrinterSettings;
            //                    try
            //                    {
            //                        printDocument1.PrinterSettings = myPrinterSetting;
            //                    }
            //                    catch
            //                    {
            //                        printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
            //                    }

            //                    //Lines Added By Dipak 20090909 
            //                    oDialogResultIsOK = true;
            //                    pdfdraw = new pdftron.PDF.PDFDraw();
            //                    if (rect != null)
            //                    {
            //                        rect.Dispose();
            //                        rect = null;
            //                    }
            //                    printDocument1.DocumentName = strFileName;
            //                    printDocument1.Print();
            //                    if (pdfdraw != null)
            //                    {
            //                        pdfdraw.Dispose();
            //                        pdfdraw = null;
            //                    }
            //                    if (rect != null)
            //                    {
            //                        rect.Dispose();
            //                        rect = null;
            //                    }
            //                }//if
            //                //   PrintDialog1.Dispose();
            //                //   PrintDialog1 = null;
            //            }
            //            else
            //            {
            //                _ErrorMessage = "Error in Showing Print Dialog";

            //                if (_ErrorMessage.Trim() != "")
            //                {
            //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                    _MessageString = "";
            //                }


            //                MessageBox.Show(_ErrorMessage, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        else
            //        {
            //            // printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
            //            if (myPrinterSetting == null)
            //            {
            //                myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
            //            }

            //            if (myPrinterSetting != null)
            //            {
            //                try
            //                {
            //                    printDocument1.PrinterSettings = myPrinterSetting;
            //                }
            //                catch
            //                {
            //                    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
            //                }
            //            }


            //            oDialogResultIsOK = true;

            //            pdfdraw = new pdftron.PDF.PDFDraw();
            //            if (rect != null)
            //            {
            //                rect.Dispose();
            //                rect = null;
            //            }
            //            printDocument1.DocumentName = strFileName;
            //            printDocument1.Print();
            //            //   myPrinterSetting = null;
            //            if (pdfdraw != null)
            //            {
            //                pdfdraw.Dispose();
            //                pdfdraw = null;
            //            }
            //            if (rect != null)
            //            {
            //                rect.Dispose();
            //                rect = null;
            //            }
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

            //        MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        if (pdfdraw != null)
            //        {
            //            pdfdraw.Dispose();
            //            pdfdraw = null;
            //        }
            //        if (rect != null)
            //        {
            //            rect.Dispose();
            //            rect = null;
            //        }
            //        //if (PrintDialog1 != null)
            //        //{
            //        //   PrintDialog1.Dispose();
            //        //   PrintDialog1 = null;
            //        //}
            //    }

            //}

           // private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
           // {
           //     //PDFDoc doc = oPDFDoc; //GetPDFDoc();

           //     if (oPDFDoc == null)
           //     {
           //         MessageBox.Show("Error: Print document is not selected.");
           //         return;
           //     }
           ////     print_page_itr = oPDFDoc.GetPageIterator();// doc.GetPage(1);// PageBegin;
           //     if (rect != null)
           //     {
           //         rect.Dispose();
           //         rect = null;
           //     }
                
           //     if (rbCustomDPI.Checked)
           //     {

           //       //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting Builtin Rasterizer");
           //         pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
           //       //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting Builtin Rasterizer");

           //     }
           //     else
           //     {
           //       //  gloAuditTrail.gloAuditTrail.PrintLog("Started Setting GDI Rasterizer");
           //         pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
           //       //  gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting GDI Rasterizer");
           //     }

           // }
            //pdftron.PDF.Rect rect = null;
            //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
            //{
            //    //Graphics gr = e.Graphics;
            //    e.Graphics.PageUnit = GraphicsUnit.Inch;
            //    //Rectangle rectPage = e.PageBounds;
            //    // print without margins 
            //    // Rectangle rectPage = ev.MarginBounds ' print using margins 
            //    if (rect == null)
            //    {
            //        if (rbCustomDPI.Checked)
            //        {
            //            float dpi = Math.Max(e.Graphics.DpiX, e.Graphics.DpiY);

            //            if (dpi > (float)numCustomDPIValue.Value)
            //            {
            //                dpi = (float)numCustomDPIValue.Value;
            //             //   gloAuditTrail.gloAuditTrail.PrintLog("Started Setting DPI "+dpi.ToString());
            //                pdfdraw.SetDPI(dpi);
            //             //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Setting DPI " + dpi.ToString());
            //            }
            //        }

            //        //double left;
            //        //double right;
            //        //double top;
            //        //double bottom;

            //        //left = rectPage.Left / 100;
            //        //right = rectPage.Right / 100;
            //        //top = rectPage.Top / 100;
            //        //bottom = rectPage.Bottom / 100;

            //        rect = new pdftron.PDF.Rect((e.PageBounds.Left / 100) * 72, (e.PageBounds.Bottom / 100) * 72, (e.PageBounds.Right / 100) * 72, (e.PageBounds.Top / 100) * 72);
            //    }
            //    PDFDoc doc = oPDFDoc;
            //    doc.Lock();

            //    try
            //    {

            //        Int16 val =System.Convert.ToInt16( Math.Ceiling (100.00 / _nPagesCnt));
            //        int _cnt = val * (_printPageIndex + 2);
            //        if (_cnt > 100)
            //        {
            //            //pbDocument.Value = 100;
            //            pbDocument.Value = pbDocument.Maximum;
            //        }
            //        else
            //        {
            //            pbDocument.Value = _cnt;
            //        }
            //     //   gloAuditTrail.gloAuditTrail.PrintLog("Started Drawing PDF" +_printPageIndex.ToString());
            //        pdfdraw.DrawInRect(oPDFDoc.GetPage(System.Convert.ToInt32(oSourceDocSelectedPages[_printPageIndex])), e.Graphics, rect);
            //     //   gloAuditTrail.gloAuditTrail.PrintLog("Finished Drawing PDF" + _printPageIndex.ToString());

            //        txtPrintStatus.AppendText("Printing Page " + oSourceDocSelectedPages[_printPageIndex].ToString());
            //        txtPrintStatus.AppendText(Environment.NewLine);
                   
            //        pbDocument.Refresh();
            //        Application.DoEvents();

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

            //        MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
                
            //    doc.Unlock();

            //    if (_printPageIndex < oSourceDocSelectedPages.Count - 1)
            //    {
            //        e.HasMorePages = true;
            //        _printPageIndex = _printPageIndex + 1;
            //    }
            //    else
            //    {
            //        e.HasMorePages = false;
            //        _printPageIndex = 0;
            //        oSourceDocSelectedPages.Clear();

            //    }
               
            //}

            #endregion

            private void rbCustomDPI_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (rbCustomDPI.Checked)
                    {
                        numCustomDPIValue.Enabled = true;
                    }
                    else
                    {
                        numCustomDPIValue.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;
                }
            }

            //#CR380 Added Code While printing from scan doc DPI Setting not respecting
            private void AuditLogErrorMessage(string _ErrorMessage)
            {
                string _MessageString = "";
                if (_ErrorMessage.Trim() != "")
                {
                    _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }


            }

            public void getDefaultDPISetting()
            {

                try
                {
                    //To Get CustomDPIValue
                    object oDPISetting;
                    ogloSettings.GetSetting("CustomDPIValue", gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID, out oDPISetting);
                    if (oDPISetting != null)
                    {
                        sCustomDIPValue = System.Convert.ToString(oDPISetting);
                        decimal d=150;
                        if (!decimal.TryParse(sCustomDIPValue, out d))
                        {
                            //invalid value
                            sCustomDIPValue = "150";
                        }
                        else
                        {
                            sCustomDIPValue = ((long)d).ToString();
                        }

                    }
                    if (oDPISetting != null)
                    {
                        oDPISetting = null;
                    }
                    ogloSettings.GetSetting("DefaultDpi", gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID, out oDPISetting);
                    if (oDPISetting != null)
                    {
                        if (System.Convert.ToString(oDPISetting) != "")
                        {
                            bDefaultDPI = System.Convert.ToBoolean(oDPISetting);
                        }
                    }
                    if (oDPISetting != null)
                    {
                        oDPISetting = null;
                    }

                    if (bDefaultDPI)
                    {
                        rbDefaultDPI.Checked = true;
                    }
                    else
                    {
                        rbCustomDPI.Checked = true;
                        numCustomDPIValue.Value = System.Convert.ToInt64(sCustomDIPValue);
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                {
                }

            }

            private void UpdateDPISetting()
            {
                if (bDefaultDPI != rbDefaultDPI.Checked)
                {
                    ogloSettings.AddSetting("DefaultDPI", rbDefaultDPI.Checked.ToString(), gloEDocumentV3.gloEDocV3Admin.gClinicID, gloEDocumentV3.gloEDocV3Admin.gUserID, gloSettings.SettingFlag.User);

                    ogloSettings.AddSetting("CustomDPIValue", numCustomDPIValue.Value.ToString(), gloEDocumentV3.gloEDocV3Admin.gClinicID, gloEDocumentV3.gloEDocV3Admin.gUserID, gloSettings.SettingFlag.User);

                }
            }


        }
    }
