using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;

using System.Drawing.Printing;
using pdftron;
using pdftron.PDF;
using pdftron.SDF;
using pdftron.Filters;
using pdftron.Common;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace gloPrintDialog
{

    public class gloAsyncOperation
    {
        // The method to be executed asynchronously.
        public string DrawMethod(PDFDraw draw, Page currentPage, Graphics pdfGraphics, double Width, double Height, out int threadId) 
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            try
            {

                using (Rect pageRect = new Rect(0, 0, Width, Height))
                {
                    draw.DrawInRect(currentPage, pdfGraphics, pageRect);
                    //pageRect.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "";
        }

    }
    public delegate string AsyncDrawMethodCaller(PDFDraw draw, Page currentPage, Graphics pdfGraphics, double Width, double Height, out int threadId); 
    public class gloRecoverPDF
    {
       // public static string tempDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static bool gIsPDFTronConnected = false;
        public static void ConnectToPDFTron()
        {
            if (gIsPDFTronConnected == true)
            {
                DisconnectToPDFTron();
            }

            try
            {
                //changed license key 
                pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20130603):4DE63118A4FA49B931EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA");
                gIsPDFTronConnected = true;
            }
            catch (pdftron.Common.PDFNetException ex)
            {

                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.ToString();
                MessageBox.Show(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }

        }
        public static void DisconnectToPDFTron()
        {
            try
            {
                pdftron.PDFNet.Terminate();
                gIsPDFTronConnected = false;
            }
            catch (pdftron.Common.PDFNetException ex)
            {
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.ToString();
                MessageBox.Show(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        public static String SetDefaultFontForDocumentFields(String PDFFileName, String tempDir)
        {
            try
            {
                ConnectToPDFTron();

                PDFDoc _pdfdoc = new PDFDoc(PDFFileName);
                try
                {
                    _pdfdoc.InitSecurityHandler();
                }
                catch (Exception)
                {

                    //Intetionally left Blank
                }

                pdftron.PDF.Font fnt = null;
                Obj formObj = null;
                Obj dr = null;
                Obj font_dict = null;
                Obj pdffont = null;
                Obj pdffont_dict = null;
                try
                {
                    formObj = _pdfdoc.GetAcroForm();
                    if (formObj != null)
                    {
                        dr = formObj.FindObj("DR");
                        if (dr == null) dr = formObj.PutDict("DR");
                        font_dict = dr.FindObj("Font");
                        if (font_dict == null) font_dict = dr.PutDict("Font");
                        pdffont = font_dict.FindObj("MyFont");
                        if (pdffont == null)
                        {
                            fnt = pdftron.PDF.Font.Create(_pdfdoc, pdftron.PDF.Font.StandardType1Font.e_times_roman, true);
                            pdffont = fnt.GetSDFObj();
                            pdffont_dict = font_dict.Put("MyFont", pdffont);
                            Obj o = null;
                            Obj m = null;
                            for (FieldIterator fi = _pdfdoc.GetFieldIterator(); fi.HasNext(); fi.Next())
                            {
                                Field currentField = fi.Current();
                                if (currentField.GetType() == Field.Type.e_text)
                                {
                                    o = currentField.GetSDFObj();
                                    if (o != null)
                                    {
                                        m = o.PutString("DA", "/MyFont 10 Tf 0 0 0 rg ");
                                    }
                                    // currentField.EraseAppearance();
                                    try
                                    {
                                        currentField.RefreshAppearance();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            o = null;
                            m = null;

                            String filePath = Path.Combine(tempDir, Guid.NewGuid().ToString() + ".pdf");
                            _pdfdoc.Save(filePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                            return filePath;
                        }
                        else
                        {
                            return PDFFileName;
                        }
                    }
                    else
                    {
                        return PDFFileName;
                    }
                }
                catch
                {

                    return PDFFileName;

                }
                finally
                {
                    if (fnt != null) { fnt.Dispose(); fnt = null; }
                    if (formObj != null) { formObj.Dispose(); formObj = null; }
                    if (dr != null) { dr.Dispose(); dr = null; }
                    if (font_dict != null) { font_dict.Dispose(); font_dict = null; }
                    if (pdffont != null) { pdffont.Dispose(); pdffont = null; }
                    if (pdffont_dict != null) { pdffont_dict.Dispose(); pdffont_dict = null; }
                    if (_pdfdoc != null) 
                    {
                        try
                        {
                            _pdfdoc.Close();
                        }
                        catch
                        {
                        }
                        _pdfdoc.Dispose(); 
                        _pdfdoc = null; 
                    }
                }
            }
            catch
            {
                return PDFFileName;
            }
            finally
            {
                try
                {
                    DisconnectToPDFTron();
                }
                catch
                {
                }

            }

        }

        private static bool errorHappened = false;

        public static String RecoverIfNotSupported(String PDFFileName, String tempDir, out bool bIsNotSupported, out bool bOkToProceed, out int iPageCount)
        {
            String FilePath = SetDefaultFontForDocumentFields(PDFFileName, tempDir);
            iPageCount = 0;
            bOkToProceed = true;
            try
            {
                try
                {
                    ConnectToPDFTron();
                }
                catch
                {
                }

                using (PDFDoc in_doc = new PDFDoc(FilePath))
                {
                    iPageCount = in_doc.GetPageCount();
                    try
                    {
                        in_doc.InitSecurityHandler();
                    }
                    catch
                    {
                    }
                    using (PDFDraw draw = new PDFDraw())
                    {
                        bool bLocked = false;
                        try
                        {
                            in_doc.Lock();
                            bLocked = true;
                        }
                        catch
                        {
                        }
                        bool bNoErrorAddingThread = false;
                        bool bNoErrorAddingUnhandled = false;
                        try
                        {
                            draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
                            draw.SetDPI(72);
                            // Add the event handler for handling UI thread exceptions to the event.
                            try
                            {
                                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
                                bNoErrorAddingThread = true;
                            }
                            catch
                            {

                            }
                            // Set the unhandled exception mode to force all Windows Forms errors to go through 
                            // our handler.


                            AppDomain currentDomain = AppDomain.CurrentDomain;
                            try
                            {
                                currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
                                bNoErrorAddingUnhandled = true;
                            }
                            catch
                            {

                            }
                            errorHappened = false;
                            //Incidents 00055243 and 00055292 : Scan Doc Performance issue.
                            //for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
                            PageIterator itr = in_doc.GetPageIterator();
                            if (itr.HasNext() && !(errorHappened))
                            {
                                Page currentPage = itr.Current();

                                try
                                {
                                    double Width = currentPage.GetPageWidth();
                                    double Height = currentPage.GetPageHeight();
                                    using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
                                    {
                                        using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
                                        {
                                            using (Rect newRect = new Rect(0, 0, Width, Height))
                                            {
                                                try
                                                {
                                                    draw.DrawInRect(currentPage, pdfGraphics, newRect);
                                                }
                                                catch (PDFNetException)
                                                {
                                                    errorHappened = true;

                                                }
                                                catch (AccessViolationException)
                                                {
                                                    errorHappened = true;

                                                }
                                            }

                                        }

                                    }

                                }
                                catch
                                {
                                    errorHappened = true;
                                }
                            }
                            try
                            {
                                if (bNoErrorAddingThread)
                                {
                                    Application.ThreadException -= UIThreadException;
                                }
                                if (bNoErrorAddingUnhandled)
                                {
                                    currentDomain.UnhandledException -= UnHandler;
                                }
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                            errorHappened = true;
                        }
                        if (bLocked)
                        {
                            try
                            {
                                if (in_doc != null)
                                {
                                    in_doc.Unlock();
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    try
                    {
                        if (in_doc != null)
                        {
                            in_doc.Close();
                        }
                    }
                    catch
                    {
                    }
                }
                
                if (errorHappened)
                {

                    try
                    {
                        // SLR: Already connected, hence commented.
                        //            gloEDocV3Admin.ConnectToPDFTron();
                        using (PDFDoc in_doc = new PDFDoc(FilePath))
                        {


                            using (PDFDraw draw = new PDFDraw())
                            {


                                try
                                {
                                    draw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
                                    draw.SetDPI(600);
                                }
                                catch
                                {
                                    bOkToProceed = false;
                                }
                                if (bOkToProceed)
                                {
                                    PDFDoc new_doc = new PDFDoc();
                                    bool bLocked = false;
                                    try
                                    {
                                        in_doc.InitSecurityHandler();
                                        in_doc.Lock();
                                        bLocked = true;
                                    }
                                    catch
                                    {

                                    }
                                    try
                                    {
                                        for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
                                        {
                                            Page currentPage = itr.Current();
                                            //try
                                            //{

                                            //    Bitmap pdfBitmap = draw.GetBitmap(currentPage);
                                            //    String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
                                            //    pdfBitmap.Save(NewBmp);
                                            //    AddImage(ref new_doc, pdfBitmap);
                                            //    pdfBitmap.Dispose();
                                            //}
                                            //catch
                                            //{

                                            try
                                            {
                                                double Width = currentPage.GetPageWidth();
                                                double Height = currentPage.GetPageHeight();
                                                float bmpHorzRes = 200;
                                                float bmpVertRes = 200;
                                                using (Bitmap pdfBitmap = new Bitmap((int)(Width * bmpHorzRes /  72.0  ), (int)(Height * bmpVertRes / 72.0  )))
                                                {
                                                    pdfBitmap.SetResolution(bmpHorzRes, bmpVertRes);
                                                    using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
                                                    {
                                                        // SLR: Moved following code to another thread due to hanging sometimes.
                                                        //try
                                                        //{
                                                        //    draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
                                                        //}
                                                        //catch
                                                        //{
                                                        //    okToProceed = false;
                                                        //}

                                                        try
                                                        {
                                                            gloAsyncOperation AsyncDraw = new gloAsyncOperation();

                                                            // Create the delegate.
                                                            AsyncDrawMethodCaller caller = new AsyncDrawMethodCaller(AsyncDraw.DrawMethod);
                                                            AsyncCallback myCallBackmethod = new AsyncCallback(CallDrawBackMethod);
                                                            int threadId = 0;
                                                            // Initiate the asychronous call.
                                                            IAsyncResult result = caller.BeginInvoke(draw, currentPage, pdfGraphics, Width * bmpHorzRes / 96.0 , Height * bmpVertRes / 96.0 , out threadId, myCallBackmethod, null);
                                                            // Poll while simulating work.
                                                            int noOfTimes = 5;
                                                            while ((result.IsCompleted == false) && (noOfTimes > 0))
                                                            {
                                                                Thread.Sleep(1000);
                                                                noOfTimes--;
                                                            }

                                                            if (!result.IsCompleted)
                                                            {
                                                                bOkToProceed = false;
                                                            }
                                                            else
                                                            {
                                                                bOkToProceed = (_CallDrawBackException == "");
                                                            }
                                                            myCallBackmethod = null;
                                                            caller = null;
                                                            AsyncDraw = null;
                                                        }
                                                        catch
                                                        {
                                                            bOkToProceed = false;
                                                        }
                                                        //String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
                                                        //pdfBitmap.Save(NewBmp);
                                                        if (bOkToProceed)
                                                        {
                                                            AddImage(ref new_doc, pdfBitmap, Width, Height);
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                   


                                                }
                                            }
                                            catch
                                            {
                                                bOkToProceed = false;
                                            }
                                            if (!bOkToProceed)
                                            {
                                                break;
                                            }
                                            //}
                                        }
                                    }
                                    catch
                                    {
                                        bOkToProceed = false;
                                    }
                                    if (bLocked)
                                    {
                                        try
                                        {
                                            if (in_doc != null)
                                            {
                                                in_doc.Unlock();
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    try
                                    {
                                        if (in_doc != null)
                                        {
                                            in_doc.Close();
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    if (bOkToProceed)
                                    {
                                        String NewFilePath = Path.Combine(tempDir, Guid.NewGuid().ToString() + ".pdf");
                                        try
                                        {

                                            new_doc.Save(NewFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                                        }
                                        catch
                                        {
                                            bOkToProceed = false;
                                        }
                                        if (new_doc != null)
                                        {
                                            try
                                            {
                                                try
                                                {
                                                    new_doc.Close();
                                                }
                                                catch
                                                {
                                                }
                                                new_doc.Dispose();
                                                new_doc = null;
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (bOkToProceed)
                                        {
                                            bIsNotSupported = true;
                                            return NewFilePath;
                                        }
                                        else
                                        {
                                            bIsNotSupported = false;
                                            return FilePath;
                                        }
                                    }
                                    else
                                    {
                                        if (new_doc != null)
                                        {
                                            try
                                            {
                                                try
                                                {
                                                    new_doc.Close();
                                                }
                                                catch
                                                {
                                                }
                                                new_doc.Dispose();
                                                new_doc = null;
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        bIsNotSupported = false;
                                        return FilePath;
                                    }
                                }
                                else
                                {
                                    bIsNotSupported = false;
                                    try
                                    {
                                        if (in_doc != null)
                                        {
                                            in_doc.Close();
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    return FilePath;
                                }
                            }
                        }
                    }
                    catch
                    {
                        bIsNotSupported = false;
                        bOkToProceed = false;
                        return FilePath;
                    }

                }
                else
                {
                    bIsNotSupported = false;
                    return FilePath;
                }

            }
            catch
            {
                bIsNotSupported = false;
                bOkToProceed = false;
                return FilePath;
            }
            finally
            {
                try
                {
                    DisconnectToPDFTron();
                }
                catch
                {
                }
            }

        }
        // The callback method must have the same signature as the
        // AsyncCallback delegate.
        private static string _CallDrawBackException = "";
        private static void CallDrawBackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            AsyncDrawMethodCaller caller = (AsyncDrawMethodCaller)result.AsyncDelegate;


            //// Retrieve the format string that was passed as state 
            //// information.
            //string formatString = (string)ar.AsyncState;

            // Define a variable to receive the value of the out parameter.
            // If the parameter were ref rather than out then it would have to
            // be a class-level field so it could also be passed to BeginInvoke.
            int threadId = 0;

            // Call EndInvoke to retrieve the results.
            _CallDrawBackException = caller.EndInvoke(out threadId, ar);

        }
        private static void UnHandler(object sender, UnhandledExceptionEventArgs args)
        {
            errorHappened = true;
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHappened = true;
        }

        private static void AddImage(ref PDFDoc doc, System.Drawing.Bitmap bimp, double Width, double Height)
        {
            using (ElementBuilder bld = new ElementBuilder())   // Used to build new Element objects
            {
                using (ElementWriter writer = new ElementWriter())  // Used to write Elements to the page   
                {
                    using (Rect pdfRect = new Rect(0,0,Width,Height) )
                    {
                        Page page = doc.PageCreate(pdfRect);   // Start a new page 
                        writer.Begin(page);             // Begin writing to this page
                        using (pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc.GetSDFDoc(), bimp))
                        {
                            Element element = bld.CreateImage(img, 0.0, 0.0, Width,Height);
                            writer.WritePlacedElement(element);
                            writer.End();   // Finish writing to the page
                            doc.PagePushBack(page);
                        }
                    }
                    //String newFilePath =     Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                    //doc.Save(newFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                }
            }
        }
        //static public List<string> SplitPDFToMaxNoOfPages(string strInput, int iNoOfPages = 0, string strOutput = null)
        //{
        //    int iNoOfDocs = 0;
        //    List<string> lstFiles = new List<string>();
        //    try
        //    {
        //        ConnectToPDFTron();
        //    }
        //    catch
        //    {
        //    }
        //    try
        //    {
        //        using (PDFDoc in_doc = new PDFDoc(strInput))
        //        {
        //            in_doc.InitSecurityHandler();

        //            int iPageCount = in_doc.GetPageCount();
        //            if (((iNoOfPages == 0) || (iNoOfPages >= iPageCount)) && (strOutput == null))
        //            {
        //                lstFiles.Add(strInput);
        //                try
        //                {
        //                    if (in_doc != null)
        //                    {
        //                        in_doc.Close();
        //                    }
        //                }
        //                catch
        //                {
        //                }

        //                return lstFiles;
        //            }
        //            if (strOutput == null)
        //            {
        //                strOutput = strInput;
        //            }

        //            if (!strOutput.Contains(".pdf"))
        //            {
        //                strOutput += ".pdf";
        //            }


        //            if ((iNoOfPages == 0) || (iNoOfPages >= iPageCount))
        //            {
        //                iNoOfPages = iPageCount;
        //            }
        //            List<ArrayList> copy_pages = new List<ArrayList>();

        //            int pCount = 0;
        //            copy_pages.Add(new ArrayList());
        //            for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //            {
        //                if (pCount < iNoOfPages)
        //                {
        //                    copy_pages[iNoOfDocs].Add(itr.Current());
        //                }
        //                else
        //                {
        //                    copy_pages.Add(new ArrayList());
        //                    iNoOfDocs++;
        //                    pCount = 0;
        //                    copy_pages[iNoOfDocs].Add(itr.Current());
        //                }
        //                pCount++;
        //            }
        //            for (int iDoc = 0; iDoc < iNoOfDocs; iDoc++)
        //            {
        //                using (PDFDoc new_doc = new PDFDoc())
        //                {
        //                    ArrayList imported_pages = new_doc.ImportPages(copy_pages[iDoc]);
        //                    for (int i = 0; i < imported_pages.Count; i++)
        //                    {
        //                        new_doc.PagePushBack((Page)imported_pages[i]);
        //                    }
        //                    string StrOutput = strOutput.Replace(".pdf", "_" + (iDoc + 1).ToString() + ".pdf");
        //                    new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);
        //                    lstFiles.Add(StrOutput);
        //                    try
        //                    {
        //                        if (new_doc != null)
        //                        {
        //                            new_doc.Close();
        //                        }
        //                    }
        //                    catch
        //                    {
        //                    }

        //                }
        //            }
        //            try
        //            {
        //                if (in_doc != null)
        //                {
        //                    in_doc.Close();
        //                }
        //            }
        //            catch
        //            {
        //            }

        //        }

        //        return lstFiles;
        //    }
        //    catch
        //    {
        //        lstFiles.Clear();
        //        lstFiles.Add(strInput);
        //        return lstFiles;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            DisconnectToPDFTron();
        //        }
        //        catch
        //        {
        //        }

        //    }

        //}

        static public string SelectedPagesToPDF(string strInput, ArrayList oSourceDocSelectedPages)
        {

            string StrOutput = strInput;
            try
            {
                ConnectToPDFTron();
            }
            catch
            {
                return strInput;
            }
            try
            {
                using (PDFDoc in_doc = new PDFDoc(strInput))
                {
                    in_doc.InitSecurityHandler();

                    int iPageCount = in_doc.GetPageCount();
                     

                    int oLength = oSourceDocSelectedPages.Count;
                    if (oLength == 0) return strInput;
                   ArrayList copy_pages=new ArrayList();
                    
                    int pCount = 0;
                    //copy_pages.Add(new ArrayList());
                    int oCount = 0;
                    int currentPage = 0;
                    currentPage = System.Convert.ToInt32(oSourceDocSelectedPages[oCount]);
                    for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
                    {
                        if (pCount > iPageCount) break;
                        pCount++;
                       
                        if (pCount == currentPage) 
                        {
                            copy_pages.Add(itr.Current());
                            oCount++;
                             if (oCount >= oLength) break;
                            currentPage = System.Convert.ToInt32(oSourceDocSelectedPages[oCount]);
                        }

                    }
                     
                        using (PDFDoc new_doc = new PDFDoc())
                        {
                            ArrayList imported_pages = new_doc.ImportPages(copy_pages );
                            for (int i = 0; i < imported_pages.Count; i++)
                            {
                                new_doc.PagePushBack((Page)imported_pages[i]);
                            }
                            StrOutput = strInput.Replace(".pdf", "_Selected.pdf");
                            bool ExceptionInSaving = false;
                            try
                            {

                                new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);
                            }
                            catch
                            {
                                ExceptionInSaving = true;
                            }
                            try
                            {
                                if (new_doc != null)
                                {
                                    new_doc.Close();
                                }
                            }
                            catch
                            {
                            }
                            if (ExceptionInSaving)
                            {
                                return strInput;
                            }
                        }
                    
                    try
                    {
                        if (in_doc != null)
                        {
                            in_doc.Close();
                        }
                    }
                    catch
                    {
                    }

                }

                return StrOutput;
            }
            catch
            {
                return StrOutput;  
            }
            finally
            {
                try
                {
                    DisconnectToPDFTron();
                }
                catch
                {
                }

            }

        }

        static public List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> SplitPDFToMaxNoOfPages(string strInput, int iNoOfPages = 0, string strOutput = null, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo> lstFooter = null)
        {

            int iNoOfDocs = 0;
            int iPageCount = 0;


            try
            {
                ConnectToPDFTron();
            }
            catch
            {
            }
            try
            {
                List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> lstToAddDocuments = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
                int startFrompage = 1;

                using (PDFDoc in_doc = new PDFDoc(strInput))
                {
                    in_doc.InitSecurityHandler();

                    iPageCount = in_doc.GetPageCount();
                    if (((iNoOfPages == 0) || (iNoOfPages >= iPageCount)) && (strOutput == null))
                    {
                        if (strOutput == null)
                        {
                            strOutput = strInput;
                        }

                        if (!strOutput.Contains(".pdf"))
                        {
                            strOutput += ".pdf";
                        }
                        startFrompage = ChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, strInput);
                        return lstToAddDocuments;
                    }
                    else
                    {

                        if (strOutput == null)
                        {
                            strOutput = strInput;
                        }

                        if (!strOutput.Contains(".pdf"))
                        {
                            strOutput += ".pdf";
                        }
                        if ((iNoOfPages == 0) || (iNoOfPages >= iPageCount))
                        {
                            iNoOfPages = iPageCount;
                        }
                        List<ArrayList> copy_pages = new List<ArrayList>();

                        int pCount = 0;
                        copy_pages.Add(new ArrayList());
                        for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
                        {
                            if (pCount < iNoOfPages)
                            {
                                copy_pages[iNoOfDocs].Add(itr.Current());
                            }
                            else
                            {
                                copy_pages.Add(new ArrayList());
                                iNoOfDocs++;
                                pCount = 0;
                                copy_pages[iNoOfDocs].Add(itr.Current());
                            }
                            pCount++;
                        }

                        for (int iDoc = 0; iDoc <= iNoOfDocs; iDoc++)
                        {
                            string StrOutput = strOutput.Replace(".pdf", "_" + (iDoc + 1).ToString() + ".pdf");

                            using (PDFDoc new_doc = new PDFDoc())
                            {
                                ArrayList imported_pages = new_doc.ImportPages(copy_pages[iDoc]);
                                for (int i = 0; i < imported_pages.Count; i++)
                                {
                                    new_doc.PagePushBack((Page)imported_pages[i]);
                                }
                                new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);

                                try
                                {
                                    if (new_doc != null)
                                    {
                                        new_doc.Close();
                                    }
                                }
                                catch
                                {
                                }

                            }
                            startFrompage = ChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, StrOutput);

                        }
                    }
                    try
                    {
                        if (in_doc != null)
                        {
                            in_doc.Close();
                        }
                    }
                    catch
                    {
                    }
                    return lstToAddDocuments;
                }
            }
            catch
            {
                try
                {
                    List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> lstToAddDocuments = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
                    int startFrompage = 1;

                    if (strOutput == null)
                    {
                        strOutput = strInput;
                    }

                    if (!strOutput.Contains(".pdf"))
                    {
                        strOutput += ".pdf";
                    }
                    startFrompage = ChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, strInput);
                    return lstToAddDocuments;

                }
                catch
                {
                    List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> lstToAddDocuments = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo>();
                    gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo curDocInfo = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                    curDocInfo.PdfFileName = strInput;
                    curDocInfo.SrcFileName = strInput;
                    curDocInfo.footerInfo = lstFooter;
                    lstToAddDocuments.Add(curDocInfo);
                    return lstToAddDocuments;
                }
            }
            finally
            {
                try
                {
                    DisconnectToPDFTron();
                }
                catch
                {
                }

            }





        }

        private static int ChangeFooterInfoAccordingToSplitedPages(int iNoOfPages, string strOutput, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo> lstFooter, int iPageCount, List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo> lstToAddDocuments, int startFrompage, string strInput)
        {
            List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo> footerList = null;
                
                if ((lstFooter != null) && (lstFooter.Count > 0))
                {
                    footerList = new List<gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo>();
                    for (int iFooter = 0; iFooter < lstFooter.Count; iFooter++)
                    {
                        gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo ftfooter = lstFooter[iFooter];
                        gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo footer = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo();
                        footer.CopyExceptText(ftfooter);

                        if (footer.ToPage == 0)
                        {
                            footer.ToPage = iPageCount;
                        }
                        if (footer.FromPage == 0)
                        {
                            footer.FromPage = 1;
                        }

                        footer.StartingPage = startFrompage + (footer.FromPage - 1) % iNoOfPages;
                        footer.FromPage = footer.FromPage - startFrompage + 1;
                        footer.ToPage = footer.ToPage - startFrompage + 1;
                        bool bAddToList = true;
                        if ((footer.FromPage > iNoOfPages) || ((footer.FromPage <= 0) && (footer.ToPage <= 0)))
                        {
                            bAddToList = false;
                        }
                        else
                        {
                            if (footer.FromPage <= 0)
                            {
                                footer.FromPage = 0;
                                footer.StartingPage = startFrompage;
                            }
                        }
                        startFrompage += iNoOfPages;
                        if (bAddToList)
                        {
                            footer.TotalPages = iPageCount;

                            //footer.CenterText = "Date:[{DATE(MMddyy)}]";
                            //footer.RightText = "[{DATE()}][{PAGE()}] of [{TOTAL()}]";
                            //footer.LeftText = "Some MeaningfulDescription";

                            footerList.Add(footer);
                        }
                    }
                }
                gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo curDocInfo = new gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo();
                if ((footerList !=null) && (footerList.Count > 0))
                {
                    curDocInfo.footerInfo = footerList;
                }
                else
                {
                    curDocInfo.footerInfo = null;
                }
                curDocInfo.PdfFileName = strInput;
                curDocInfo.SrcFileName = strOutput;

                lstToAddDocuments.Add(curDocInfo);

                return startFrompage;
        }
        //SLR: Following function needed for PDFPrintProject:
        static public List<gloPrintProgressController.DocumentInfo> ProgressSplitPDFToMaxNoOfPages(string strInput, int iNoOfPages = 0, string strOutput = null, List<gloPrintProgressController.FooterInfo> lstFooter = null)
        {

            int iNoOfDocs = 0;
            int iPageCount = 0;


            try
            {
                ConnectToPDFTron();
            }
            catch
            {
            }
            try
            {
                List<gloPrintProgressController.DocumentInfo> lstToAddDocuments = new List<gloPrintProgressController.DocumentInfo>();
                int startFrompage = 1;

                using (PDFDoc in_doc = new PDFDoc(strInput))
                {
                    in_doc.InitSecurityHandler();

                    iPageCount = in_doc.GetPageCount();
                    if (((iNoOfPages == 0) || (iNoOfPages >= iPageCount)) && (strOutput == null))
                    {
                        if (strOutput == null)
                        {
                            strOutput = strInput;
                        }

                        if (!strOutput.Contains(".pdf"))
                        {
                            strOutput += ".pdf";
                        }
                        startFrompage = ProgressChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, strInput);
                        return lstToAddDocuments;
                    }
                    else
                    {

                        if (strOutput == null)
                        {
                            strOutput = strInput;
                        }

                        if (!strOutput.Contains(".pdf"))
                        {
                            strOutput += ".pdf";
                        }
                        if ((iNoOfPages == 0) || (iNoOfPages >= iPageCount))
                        {
                            iNoOfPages = iPageCount;
                        }
                        List<ArrayList> copy_pages = new List<ArrayList>();

                        int pCount = 0;
                        copy_pages.Add(new ArrayList());
                        for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
                        {
                            if (pCount < iNoOfPages)
                            {
                                copy_pages[iNoOfDocs].Add(itr.Current());
                            }
                            else
                            {
                                copy_pages.Add(new ArrayList());
                                iNoOfDocs++;
                                pCount = 0;
                                copy_pages[iNoOfDocs].Add(itr.Current());
                            }
                            pCount++;
                        }

                        for (int iDoc = 0; iDoc <= iNoOfDocs; iDoc++)
                        {
                            string StrOutput = strOutput.Replace(".pdf", "_" + (iDoc + 1).ToString() + ".pdf");

                            using (PDFDoc new_doc = new PDFDoc())
                            {
                                ArrayList imported_pages = new_doc.ImportPages(copy_pages[iDoc]);
                                for (int i = 0; i < imported_pages.Count; i++)
                                {
                                    new_doc.PagePushBack((Page)imported_pages[i]);
                                }
                                new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);

                                try
                                {
                                    if (new_doc != null)
                                    {
                                        new_doc.Close();
                                    }
                                }
                                catch
                                {
                                }

                            }
                            startFrompage = ProgressChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, StrOutput);

                        }
                    }
                    try
                    {
                        if (in_doc != null)
                        {
                            in_doc.Close();
                        }
                    }
                    catch
                    {
                    }
                    return lstToAddDocuments;
                }
            }
            catch
            {
                try
                {
                    List<gloPrintProgressController.DocumentInfo> lstToAddDocuments = new List<gloPrintProgressController.DocumentInfo>();
                    int startFrompage = 1;

                    if (strOutput == null)
                    {
                        strOutput = strInput;
                    }

                    if (!strOutput.Contains(".pdf"))
                    {
                        strOutput += ".pdf";
                    }
                    startFrompage = ProgressChangeFooterInfoAccordingToSplitedPages(iNoOfPages, strOutput, lstFooter, iPageCount, lstToAddDocuments, startFrompage, strInput);
                    return lstToAddDocuments;

                }
                catch
                {
                    List<gloPrintProgressController.DocumentInfo> lstToAddDocuments = new List<gloPrintProgressController.DocumentInfo>();
                    gloPrintProgressController.DocumentInfo curDocInfo = new gloPrintProgressController.DocumentInfo();
                    curDocInfo.PdfFileName = strInput;
                    curDocInfo.SrcFileName = strInput;
                    curDocInfo.footerInfo = lstFooter;
                    lstToAddDocuments.Add(curDocInfo);
                    return lstToAddDocuments;
                }
            }
            finally
            {
                try
                {
                    DisconnectToPDFTron();
                }
                catch
                {
                }

            }





        }

        private static int ProgressChangeFooterInfoAccordingToSplitedPages(int iNoOfPages, string strOutput, List<gloPrintProgressController.FooterInfo> lstFooter, int iPageCount, List<gloPrintProgressController.DocumentInfo> lstToAddDocuments, int startFrompage, string strInput)
        {
            List<gloPrintProgressController.FooterInfo> footerList = null;

            if ((lstFooter != null) && (lstFooter.Count > 0))
            {
                footerList = new List<gloPrintProgressController.FooterInfo>();
                for (int iFooter = 0; iFooter < lstFooter.Count; iFooter++)
                {
                    gloPrintProgressController.FooterInfo ftfooter = lstFooter[iFooter];
                    gloPrintProgressController.FooterInfo footer = new gloPrintProgressController.FooterInfo();
                    footer.CopyExceptText(ftfooter);

                    if (footer.ToPage == 0)
                    {
                        footer.ToPage = iPageCount;
                    }
                    if (footer.FromPage == 0)
                    {
                        footer.FromPage = 1;
                    }

                    footer.StartingPage = startFrompage + (footer.FromPage - 1) % iNoOfPages;
                    footer.FromPage = footer.FromPage - startFrompage + 1;
                    footer.ToPage = footer.ToPage - startFrompage + 1;
                    bool bAddToList = true;
                    if ((footer.FromPage > iNoOfPages) || ((footer.FromPage <= 0) && (footer.ToPage <= 0)))
                    {
                        bAddToList = false;
                    }
                    else
                    {
                        if (footer.FromPage <= 0)
                        {
                            footer.FromPage = 0;
                            footer.StartingPage = startFrompage;
                        }
                    }
                    startFrompage += iNoOfPages;
                    if (bAddToList)
                    {
                        footer.TotalPages = iPageCount;

                        //footer.CenterText = "Date:[{DATE(MMddyy)}]";
                        //footer.RightText = "[{DATE()}][{PAGE()}] of [{TOTAL()}]";
                        //footer.LeftText = "Some MeaningfulDescription";
                        footer.CenterText = ftfooter.CenterText;
                        footer.RightText = ftfooter.RightText;
                        footer.LeftText = ftfooter.LeftText;

                        footerList.Add(footer);
                    }
                }
            }
            gloPrintProgressController.DocumentInfo curDocInfo = new gloPrintProgressController.DocumentInfo();
            if ((footerList != null) && (footerList.Count > 0))
            {
                curDocInfo.footerInfo = footerList;
            }
            else
            {
                curDocInfo.footerInfo = null;
            }
            curDocInfo.PdfFileName = strInput;
            curDocInfo.SrcFileName = strOutput;

            lstToAddDocuments.Add(curDocInfo);

            return startFrompage;
        }

         #region " Solutions tried"

        //static public List<gloPrintProgressController.DocumentInfo> SplitPDFToMaxNoOfPages(string strInput, int iNoOfPages = 0, string strOutput = null, List<gloPrintProgressController.FooterInfo> lstFooter = null)
        //{

        //    int iNoOfDocs = 0;
        //    int iPageCount = 0;
        //    List<string> lstFiles = new List<string>();

        //    try
        //    {
        //        ConnectToPDFTron();
        //    }
        //    catch
        //    {
        //    }
        //    try
        //    {
        //        using (PDFDoc in_doc = new PDFDoc(strInput))
        //        {
        //            in_doc.InitSecurityHandler();

        //            iPageCount = in_doc.GetPageCount();
        //            if (((iNoOfPages == 0) || (iNoOfPages >= iPageCount)) && (strOutput == null))
        //            {
        //                lstFiles.Add(strInput);

        //            }
        //            else
        //            {

        //                if (strOutput == null)
        //                {
        //                    strOutput = strInput;
        //                }

        //                if (!strOutput.Contains(".pdf"))
        //                {
        //                    strOutput += ".pdf";
        //                }
        //                if ((iNoOfPages == 0) || (iNoOfPages >= iPageCount))
        //                {
        //                    iNoOfPages = iPageCount;
        //                }
        //                List<ArrayList> copy_pages = new List<ArrayList>();

        //                int pCount = 0;
        //                copy_pages.Add(new ArrayList());
        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                {
        //                    if (pCount < iNoOfPages)
        //                    {
        //                        copy_pages[iNoOfDocs].Add(itr.Current());
        //                    }
        //                    else
        //                    {
        //                        copy_pages.Add(new ArrayList());
        //                        iNoOfDocs++;
        //                        pCount = 0;
        //                        copy_pages[iNoOfDocs].Add(itr.Current());
        //                    }
        //                    pCount++;
        //                }
                     
        //                for (int iDoc = 0; iDoc <= iNoOfDocs; iDoc++)
        //                {
        //                    using (PDFDoc new_doc = new PDFDoc())
        //                    {
        //                        ArrayList imported_pages = new_doc.ImportPages(copy_pages[iDoc]);
        //                        for (int i = 0; i < imported_pages.Count; i++)
        //                        {
        //                            new_doc.PagePushBack((Page)imported_pages[i]);
        //                        }
        //                        string StrOutput = strOutput.Replace(".pdf", "_" + (iDoc + 1).ToString() + ".pdf");
        //                        new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);
        //                        lstFiles.Add(StrOutput);
        //                        try
        //                        {
        //                            if (new_doc != null)
        //                            {
        //                                new_doc.Close();
        //                            }
        //                        }
        //                        catch
        //                        {
        //                        }

        //                    }
        //                }
        //            }
        //            try
        //            {
        //                if (in_doc != null)
        //                {
        //                    in_doc.Close();
        //                }
        //            }
        //            catch
        //            {
        //            }

        //        }
        //    }
        //    catch
        //    {
        //        lstFiles.Clear();
        //        lstFiles.Add(strInput);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            DisconnectToPDFTron();
        //        }
        //        catch
        //        {
        //        }

        //    }

        //    List<gloPrintProgressController.DocumentInfo> lstToAddDocuments = new List<gloPrintProgressController.DocumentInfo>();
        //    int startFrompage = 1;
        //    if (strOutput == null)
        //    {
        //        strOutput = strInput;
        //    }

        //    foreach (string eachoutput in lstFiles)
        //    {

        //        List<gloPrintProgressController.FooterInfo> footerList = null;
        //        if ((lstFooter != null) && (lstFooter.Count > 0))
        //        {
        //            footerList = new List<gloPrintProgressController.FooterInfo>();

        //            for (int iFooter = 0; iFooter < lstFooter.Count; iFooter++)
        //            {
        //                gloPrintProgressController.FooterInfo ftfooter = lstFooter[iFooter];
        //                gloPrintProgressController.FooterInfo footer = new gloPrintProgressController.FooterInfo();
        //                footer.CopyExceptText(ftfooter);

        //                if (footer.ToPage == 0)
        //                {
        //                    footer.ToPage = iPageCount;
        //                }
        //                if (footer.FromPage == 0)
        //                {
        //                    footer.FromPage = 1;
        //                }

        //                footer.StartingPage = startFrompage + (footer.FromPage - 1) % iNoOfPages;
        //                footer.FromPage = footer.FromPage - startFrompage + 1;
        //                footer.ToPage = footer.ToPage - startFrompage + 1;
        //                bool bAddToList = true;
        //                if ((footer.FromPage > iNoOfPages) || ((footer.FromPage <= 0) && (footer.ToPage <= 0)))
        //                {
        //                    bAddToList = false;
        //                }
        //                else
        //                {
        //                    if (footer.FromPage <= 0)
        //                    {
        //                        footer.FromPage = 0;
        //                        footer.StartingPage = startFrompage;
        //                    }
        //                }
        //                startFrompage += iNoOfPages;
        //                if (bAddToList)
        //                {
        //                    footer.TotalPages = iPageCount;

        //                    //footer.CenterText = "Date:[{DATE(MMddyy)}]";
        //                    //footer.RightText = "[{DATE()}][{PAGE()}] of [{TOTAL()}]";
        //                    //footer.LeftText = "Some MeaningfulDescription";
        //                    footer.CenterText = ftfooter.CenterText;
        //                    footer.RightText = ftfooter.RightText;
        //                    footer.LeftText = ftfooter.LeftText;

        //                    footerList.Add(footer);
        //                }
        //            }
        //        }
        //        gloPrintProgressController.DocumentInfo curDocInfo = new gloPrintProgressController.DocumentInfo();
        //        if (footerList.Count > 0)
        //        {
        //            curDocInfo.footerInfo = footerList;
        //        }
        //        else
        //        {
        //            curDocInfo.footerInfo = null;
        //        }
        //        curDocInfo.PdfFileName = eachoutput;
        //        curDocInfo.SrcFileName = strOutput;

        //        lstToAddDocuments.Add(curDocInfo);
        //    }
        //    return lstToAddDocuments;

        //}

        //static public List<gloPrintProgressController.DocumentInfo> SplitPDFToMaxNoOfPages(string strInput, int iNoOfPages = 0, string strOutput = null, gloPrintProgressController.FooterInfo ftfooter = null)
        //{

        //    int iNoOfDocs = 0;
        //    int iPageCount = 0;
        //    List<string> lstFiles = new List<string>();

        //    try
        //    {
        //        ConnectToPDFTron();
        //    }
        //    catch
        //    {
        //    }
        //    try
        //    {
        //        using (PDFDoc in_doc = new PDFDoc(strInput))
        //        {
        //            in_doc.InitSecurityHandler();

        //            iPageCount = in_doc.GetPageCount();
        //            if (((iNoOfPages == 0) || (iNoOfPages >= iPageCount)) && (strOutput == null))
        //            {
        //                lstFiles.Add(strInput);

        //            }
        //            else
        //            {

        //                if (strOutput == null)
        //                {
        //                    strOutput = strInput;
        //                }

        //                if (!strOutput.Contains(".pdf"))
        //                {
        //                    strOutput += ".pdf";
        //                }
        //                if ((iNoOfPages == 0) || (iNoOfPages >= iPageCount))
        //                {
        //                    iNoOfPages = iPageCount;
        //                }
        //                List<ArrayList> copy_pages = new List<ArrayList>();

        //                int pCount = 0;
        //                copy_pages.Add(new ArrayList());
        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                {
        //                    if (pCount < iNoOfPages)
        //                    {
        //                        copy_pages[iNoOfDocs].Add(itr.Current());
        //                    }
        //                    else
        //                    {
        //                        copy_pages.Add(new ArrayList());
        //                        iNoOfDocs++;
        //                        pCount = 0;
        //                        copy_pages[iNoOfDocs].Add(itr.Current());
        //                    }
        //                    pCount++;
        //                }
        //                for (int iDoc = 0; iDoc < iNoOfDocs; iDoc++)
        //                {
        //                    using (PDFDoc new_doc = new PDFDoc())
        //                    {
        //                        ArrayList imported_pages = new_doc.ImportPages(copy_pages[iDoc]);
        //                        for (int i = 0; i < imported_pages.Count; i++)
        //                        {
        //                            new_doc.PagePushBack((Page)imported_pages[i]);
        //                        }
        //                        string StrOutput = strOutput.Replace(".pdf", "_" + (iDoc + 1).ToString() + ".pdf");
        //                        new_doc.Save(StrOutput, SDFDoc.SaveOptions.e_remove_unused);
        //                        lstFiles.Add(StrOutput);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        lstFiles.Clear();
        //        lstFiles.Add(strInput);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            DisconnectToPDFTron();
        //        }
        //        catch
        //        {
        //        }

        //    }

        //    List<gloPrintProgressController.DocumentInfo> lstToAddDocuments = new List<gloPrintProgressController.DocumentInfo>();
        //    int startFrompage = 1;
        //    if (strOutput == null)
        //    {
        //        strOutput = strInput;
        //    }

        //    foreach (string eachoutput in lstFiles)
        //    {

        //        List<gloPrintProgressController.FooterInfo> footerList = null;
        //        if (ftfooter != null)
        //        {
        //            footerList = new List<gloPrintProgressController.FooterInfo>();
        //            gloPrintProgressController.FooterInfo footer = new gloPrintProgressController.FooterInfo();
        //            footer.CopyExceptText(ftfooter);

        //            if (footer.ToPage == 0)
        //            {
        //                footer.ToPage = iPageCount;
        //            }
        //            if (footer.FromPage == 0)
        //            {
        //                footer.FromPage = 1;
        //            }

        //            footer.StartingPage = startFrompage + (footer.FromPage - 1) % iNoOfPages;
        //            footer.FromPage = footer.FromPage - startFrompage + 1;
        //            footer.ToPage = footer.ToPage - startFrompage + 1;
        //            if ((footer.FromPage <= 0) && (footer.ToPage <= 0))
        //            {
        //                footer.PrintFooter = false;
        //            }
        //            else
        //            {
        //                if (footer.FromPage <= 0)
        //                {
        //                    footer.FromPage = 0;
        //                    footer.StartingPage = startFrompage;
        //                }
        //            }


        //            footer.TotalPages = iPageCount;
        //            startFrompage += iNoOfPages;
        //            //footer.CenterText = "Date:[{DATE(MMddyy)}]";
        //            //footer.RightText = "[{DATE()}][{PAGE()}] of [{TOTAL()}]";
        //            //footer.LeftText = "Some MeaningfulDescription";
        //            footer.CenterText = ftfooter.CenterText;
        //            footer.RightText = ftfooter.RightText;
        //            footer.LeftText = ftfooter.LeftText;

        //            footerList.Add(footer);
        //        }
        //        gloPrintProgressController.DocumentInfo curDocInfo = new gloPrintProgressController.DocumentInfo();
        //        curDocInfo.footerInfo = footerList;
        //        curDocInfo.PdfFileName = eachoutput;
        //        curDocInfo.SrcFileName = strOutput;

        //        lstToAddDocuments.Add(curDocInfo);
        //    }
        //    return lstToAddDocuments;

        //}

        //private static string pdfFileWithPath = "";

        //private static Nullable<bool> _isService = null;
        ///// <summary>
        ///// Gets a value indicating whether the application is a windows service.
        ///// </summary>
        ///// <value>
        ///// <c>true</c> if this instance is service; otherwise, <c>false</c>.
        ///// </value>
        //public static bool IsService
        //{
        //    get
        //    {
        //        // Determining whether or not the host application is a service is
        //        // an expensive operation (it uses reflection), so we cache the
        //        // result of the first call to this method so that we don't have to
        //        // recalculate it every call.

        //        // If we have not already determined whether or not the application
        //        // is running as a service...

        //        if ((_isService == null))
        //        {
        //            // Get details of the host assembly.
        //            Assembly entryAssembly = Assembly.GetEntryAssembly();

        //            // Get the method that was called to enter the host assembly.
        //            System.Reflection.MethodInfo entryPoint = entryAssembly.EntryPoint;

        //            // If the base type of the host assembly inherits from the
        //            // "ServiceBase" class, it must be a windows service. We store
        //            // the result ready for the next caller of this method.
        //            _isService = (entryPoint.ReflectedType.BaseType.FullName == "System.ServiceProcess.ServiceBase");

        //        }

        //        // Return the cached result.
        //        return System.Convert.ToBoolean(_isService);
        //    }
        //}

        //public static String RecoverIfNotSupported_reflection(String PDFFileName)
        //{
        //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in RecoverIfNotSupported()");
        //    String FilePath = SetDefaultFontForDocumentFields(PDFFileName);
        //    try
        //    {
        //        if (IsService)
        //        {
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in isService loop");
        //            String thisAssemblyLoaderPath = Assembly.GetExecutingAssembly().CodeBase;
        //            String thisAssemblyNameSpace = typeof(Cls_RecoverPDF).FullName;
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("thisAssemblyLoaderPath : " + thisAssemblyLoaderPath);
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("thisAssemblyNameSpace : " + thisAssemblyNameSpace);
        //            AppDomain privateDomainAddress = AppDomain.CreateDomain("gloRecoverNotSupportedPDF");

        //            AssemblyName privateAssemblyName = new AssemblyName();
        //            privateAssemblyName.CodeBase = thisAssemblyLoaderPath;
        //            try
        //            {
        //                Assembly ptrAssembly = privateDomainAddress.Load(privateAssemblyName);

        //                try
        //                {
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before reflection ");
        //                    Type privategloPrintDialog = ptrAssembly.GetType(thisAssemblyNameSpace);
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After gettype for privategloPrintDialog");
        //                    dynamic privateClass = Activator.CreateInstance(privategloPrintDialog);
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After Creating private class instance");
        //                    try
        //                    {
        //                        bool noErrors = privateClass.IsSupportedPDF(FilePath);
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After IsSupportedPDF call");
        //                        if (noErrors)
        //                        {
        //                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before SuccessfullConversion");
        //                            bool execConversion = privateClass.SuccessfullConversion();
        //                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after SuccessfullConversion");
        //                            if (!execConversion)
        //                            {
        //                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Error occured");
        //                                errorHappened = true;
        //                            }
        //                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after execConversion if block");
        //                        }
        //                        else
        //                        {
        //                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("else part of noErrors");
        //                            errorHappened = true;
        //                        }
        //                    }
        //                    catch
        //                    {
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in catch block");
        //                        errorHappened = true;
        //                    }
        //                    try
        //                    {
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before privateClass.Dispose");
        //                        privateClass.Dispose();
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after privateClass.Dispose");
        //                    }
        //                    catch
        //                    {
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("error in privateClass.Dispose");
        //                    }
        //                    privategloPrintDialog = null;
        //                }
        //                catch
        //                {
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("outer catch");
        //                    errorHappened = true;
        //                }
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before Unload");
        //                AppDomain.Unload(privateDomainAddress);
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After Unload");
        //                privateAssemblyName = null;
        //                privateDomainAddress = null;
        //                ptrAssembly = null;
        //            }
        //            catch
        //            {
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("outer most catch");
        //                errorHappened = true;
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {

        //                if (IsFileSupported(FilePath))
        //                {
        //                    if (!executedConversion)
        //                    {
        //                        errorHappened = true;
        //                    }
        //                }
        //                else
        //                {
        //                    errorHappened = true;
        //                }
        //            }
        //            catch
        //            {
        //                errorHappened = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        errorHappened = true;
        //    }
        //    //using (PDFDoc in_doc = new PDFDoc(FilePath))
        //    //{
        //    //    in_doc.InitSecurityHandler();

        //    //    using (PDFDraw draw = new PDFDraw())
        //    //    {

        //    //        in_doc.Lock();
        //    //        draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //    //        draw.SetDPI(72);
        //    //        // Add the event handler for handling UI thread exceptions to the event.
        //    //        Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
        //    //        // Set the unhandled exception mode to force all Windows Forms errors to go through 
        //    //        // our handler.
        //    //        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
        //    //        errorHappened = false;
        //    //        for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //    //        {
        //    //            if (errorHappened)
        //    //            {
        //    //                break;
        //    //            }
        //    //            Page currentPage = itr.Current();



        //    //            try
        //    //            {

        //    //                double Width = currentPage.GetPageWidth();
        //    //                double Height = currentPage.GetPageHeight();
        //    //                using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //    //                {
        //    //                    using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //    //                    {
        //    //                        try
        //    //                        {
        //    //                            draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //    //                        }
        //    //                        catch (PDFNetException)
        //    //                        {
        //    //                            errorHappened = true;

        //    //                        }
        //    //                        catch (AccessViolationException)
        //    //                        {
        //    //                            errorHappened = true;

        //    //                        }
        //    //                    }

        //    //                }

        //    //            }
        //    //            catch
        //    //            {
        //    //                errorHappened = true;
        //    //            }

        //    //        }


        //    //        AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnHandler);
        //    //        Application.ThreadException -= new ThreadExceptionEventHandler(UIThreadException);
        //    //        in_doc.Unlock();
        //    //    }
        //    //}

        //    if (errorHappened)
        //    {

        //        gloEDocV3Admin.ConnectToPDFTron();
        //        using (PDFDoc in_doc = new PDFDoc(FilePath))
        //        {
        //            in_doc.InitSecurityHandler();

        //            using (PDFDraw draw = new PDFDraw())
        //            {

        //                in_doc.Lock();

        //                draw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
        //                draw.SetDPI(600);
        //                PDFDoc new_doc = new PDFDoc();

        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                {
        //                    Page currentPage = itr.Current();
        //                    //try
        //                    //{

        //                    //    Bitmap pdfBitmap = draw.GetBitmap(currentPage);
        //                    //    String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                    //    pdfBitmap.Save(NewBmp);
        //                    //    AddImage(ref new_doc, pdfBitmap);
        //                    //    pdfBitmap.Dispose();
        //                    //}
        //                    //catch
        //                    //{

        //                    try
        //                    {
        //                        double Width = currentPage.GetPageWidth();
        //                        double Height = currentPage.GetPageHeight();
        //                        using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                        {
        //                            using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                            {
        //                                try
        //                                {
        //                                    draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                }
        //                                catch
        //                                {
        //                                }
        //                                //String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                                //pdfBitmap.Save(NewBmp);
        //                                AddImage(ref new_doc, pdfBitmap);
        //                            }

        //                        }
        //                    }
        //                    catch
        //                    {
        //                    }
        //                    //}
        //                }
        //                in_doc.Unlock();
        //                String NewFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        //                new_doc.Save(NewFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
        //                new_doc.Dispose();
        //                new_doc = null;
        //                gloEDocV3Admin.DisconnectToPDFTron();
        //                return NewFilePath;
        //            }

        //        }

        //    }
        //    else
        //    {
        //        return FilePath;
        //    }

        //}

        //private static bool executedConversion = false;
        //public bool SuccessfullConversion()
        //{
        //    return executedConversion;
        //}

        //public bool IsSupportedPDF(String PdfFileName)
        //{
        //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In IsSupportedPDF");
        //    return IsFileSupported(PdfFileName);
        //}

        //public static bool IsFileSupported(String PDFFileName)
        //{
        //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In IsFileSupported");
        //    executedConversion = false;
        //    try
        //    {
        //        gloEDocV3Admin.ConnectToPDFTron();

        //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After ConnectToPDFTron");
        //        using (PDFDoc in_doc = new PDFDoc(PDFFileName))
        //        {
        //            in_doc.InitSecurityHandler();
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After InitSecurityHandler");
        //            using (PDFDraw draw = new PDFDraw())
        //            {

        //                in_doc.Lock();
        //                draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //                draw.SetDPI(72);
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After draw.SetDPI(72)");
        //                // Add the event handler for handling UI thread exceptions to the event.
        //                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
        //                // Set the unhandled exception mode to force all Windows Forms errors to go through 
        //                // our handler.
        //                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
        //                errorHappened = false;
        //                for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                {
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in PageIterator");
        //                    if (errorHappened)
        //                    {
        //                        break;
        //                    }
        //                    Page currentPage = itr.Current();



        //                    try
        //                    {

        //                        double Width = currentPage.GetPageWidth();
        //                        double Height = currentPage.GetPageHeight();
        //                        using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                        {
        //                            using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                            {
        //                                try
        //                                {
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Before DrawInRect");
        //                                    draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("After DrawInRect");
        //                                }
        //                                catch (PDFNetException)
        //                                {
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("catch PDFNetException for  DrawInRect");
        //                                    errorHappened = true;

        //                                }
        //                                catch (AccessViolationException)
        //                                {
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("catch AccessViolationException for  DrawInRect");
        //                                    errorHappened = true;

        //                                }
        //                            }

        //                        }

        //                    }
        //                    catch
        //                    {
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("outer catch in IsFileSupported");
        //                        errorHappened = true;
        //                    }

        //                }
        //                draw.SetDPI(600);

        //                AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnHandler);
        //                Application.ThreadException -= new ThreadExceptionEventHandler(UIThreadException);
        //                in_doc.Unlock();
        //                executedConversion = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        gloEDocV3Admin.DisconnectToPDFTron();
        //    }
        //    return (!errorHappened);
        //}

        //private static Boolean isCrash = false;
        //public static String RecoverIfNotSupported_Thread(String PDFFileName)
        //{
        //    //pdfFileWithPath = PDFFileName;
        //    try
        //    {
        //        ManualResetEvent syncEvent = new ManualResetEvent(false);

        //        Thread t1 = new Thread(
        //            () =>
        //            {
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In thread1");
        //                PDFDoc _pdfdoc = new PDFDoc(PDFFileName);
        //                String FilePath = SetDefaultFontForDocumentFields(PDFFileName);
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Before try in thread1");
        //                try
        //                {
        //                    isCrash = true;
        //                    gloEDocV3Admin.ConnectToPDFTron();
        //                    using (PDFDoc in_doc = new PDFDoc(FilePath))
        //                    {
        //                        in_doc.InitSecurityHandler();
        //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in first using thread1");
        //                        using (PDFDraw draw = new PDFDraw())
        //                        {

        //                            in_doc.Lock();
        //                            draw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn);
        //                            draw.SetDPI(72);
        //                            // Add the event handler for handling UI thread exceptions to the event.
        //                            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);

        //                            // Set the unhandled exception mode to force all Windows Forms errors to go through 
        //                            // our handler.


        //                            AppDomain currentDomain = AppDomain.CurrentDomain;
        //                            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnHandler);
        //                            errorHappened = false;
        //                            for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                            {
        //                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in first PageIterator");
        //                                if (errorHappened)
        //                                {
        //                                    break;
        //                                }
        //                                Page currentPage = itr.Current();


        //                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before first try");
        //                                try
        //                                {
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in first try");
        //                                    double Width = currentPage.GetPageWidth();
        //                                    double Height = currentPage.GetPageHeight();
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after get width height");
        //                                    using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                                    {
        //                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in first using Bitmap");
        //                                        using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                                        {
        //                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in inner using Graphics");
        //                                            try
        //                                            {
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("before DrawInRect");
        //                                                draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                                isCrash = false;
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after DrawInRect");
        //                                            }
        //                                            catch (PDFNetException pnex)
        //                                            {
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in PDFNetException" + pnex.Message);
        //                                                errorHappened = true;
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after PDFNetException");
        //                                                return;
        //                                            }
        //                                            catch (AccessViolationException avex)
        //                                            {
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("in AccessViolationException" + avex.Message);
        //                                                errorHappened = true;
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after AccessViolationException");
        //                                                return;
        //                                            }
        //                                            catch (Exception ex)
        //                                            {
        //                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.Message);
        //                                                errorHappened = true;
        //                                                return;
        //                                            }
        //                                        }

        //                                    }

        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("outer catch" + ex.Message);
        //                                    errorHappened = true;
        //                                    return;
        //                                }
        //                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("after first try");
        //                                errorHappened = false;
        //                            }

        //                            Application.ThreadException -= UIThreadException;
        //                            currentDomain.UnhandledException -= UnHandler;
        //                            in_doc.Unlock();
        //                        }
        //                    }
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In RecoverIfNotSupported after first try");
        //                    gloEDocV3Admin.DisconnectToPDFTron();
        //                }
        //                catch
        //                {
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In outermost catch");
        //                    errorHappened = true;
        //                }
        //                finally
        //                {
        //                    if (_pdfdoc != null)
        //                    {
        //                        _pdfdoc.Dispose();
        //                        _pdfdoc = null;
        //                    }
        //                }
        //                syncEvent.Set();
        //            }
        //        );
        //        t1.Start();
        //        Thread.Sleep(10000);
        //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Post thread execution");


        //        Thread t2 = new Thread(
        //            () =>
        //            {
        //                syncEvent.WaitOne();

        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In thread2");
        //                PDFDoc _pdfdoc = new PDFDoc(PDFFileName);
        //                String FilePath = SetDefaultFontForDocumentFields(PDFFileName);
        //                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Before try in thread2");
        //                try
        //                {
        //                    gloEDocV3Admin.ConnectToPDFTron();
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In thread2 after first try");

        //                    using (PDFDoc in_doc = new PDFDoc(FilePath))
        //                    {
        //                        in_doc.InitSecurityHandler();

        //                        using (PDFDraw draw = new PDFDraw())
        //                        {

        //                            in_doc.Lock();

        //                            draw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus);
        //                            draw.SetDPI(600);
        //                            PDFDoc new_doc = new PDFDoc();

        //                            for (PageIterator itr = in_doc.GetPageIterator(); itr.HasNext(); itr.Next())
        //                            {
        //                                Page currentPage = itr.Current();
        //                                //try
        //                                //{

        //                                //    Bitmap pdfBitmap = draw.GetBitmap(currentPage);
        //                                //    String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                                //    pdfBitmap.Save(NewBmp);
        //                                //    AddImage(ref new_doc, pdfBitmap);
        //                                //    pdfBitmap.Dispose();
        //                                //}
        //                                //catch
        //                                //{

        //                                try
        //                                {
        //                                    double Width = currentPage.GetPageWidth();
        //                                    double Height = currentPage.GetPageHeight();
        //                                    using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
        //                                    {
        //                                        using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
        //                                        {
        //                                            try
        //                                            {
        //                                                draw.DrawInRect(currentPage, pdfGraphics, new Rect(0, 0, Width, Height));
        //                                            }
        //                                            catch
        //                                            {
        //                                            }
        //                                            //String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        //                                            //pdfBitmap.Save(NewBmp);
        //                                            AddImage(ref new_doc, pdfBitmap);
        //                                        }

        //                                    }
        //                                }
        //                                catch
        //                                {
        //                                }
        //                                //}
        //                            }
        //                            in_doc.Unlock();
        //                            String NewFilePath = Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, Guid.NewGuid().ToString() + ".pdf");
        //                            new_doc.Save(NewFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
        //                            new_doc.Dispose();
        //                            new_doc = null;
        //                            gloEDocV3Admin.DisconnectToPDFTron();
        //                            PDFFileName = NewFilePath;
        //                        }

        //                    }
        //                }
        //                catch
        //                {
        //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("In outermost catch");
        //                }
        //                finally
        //                {
        //                    if (_pdfdoc != null)
        //                    {
        //                        _pdfdoc.Dispose();
        //                        _pdfdoc = null;
        //                    }
        //                }
        //            }
        //        );
        //        if (errorHappened || isCrash)
        //        {
        //            t2.Start();
        //        }
        //        return PDFFileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Error in RecoverIfNotSupported() : " + ex.Message);
        //        return PDFFileName;
        //    }
        //}
        #endregion " Solutions tried"
    }
}
