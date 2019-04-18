using System;
using pdftron.PDF;
using pdftron.SDF;
using pdftron.Common;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;


namespace gloEDocumentV3
{
    class Cls_RecoverPDF 
    {
        public static String SetDefaultFontForDocumentFields(String PDFFileName)
        {
            try
            {
                gloEDocV3Admin.ConnectToPDFTron();

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

                            String filePath = Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, Guid.NewGuid().ToString() + ".pdf");
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
                    if (_pdfdoc != null) { _pdfdoc.Dispose(); _pdfdoc = null; }
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
                    gloEDocV3Admin.DisconnectToPDFTron();
                }
                catch
                {
                }

            }

        }

        private static bool errorHappened = false;

        public static String RecoverIfNotSupported(String PDFFileName, out bool bIsNotSupported)
        {
            String FilePath = SetDefaultFontForDocumentFields(PDFFileName);
            try
            {
                try
                {
                    gloEDocV3Admin.ConnectToPDFTron();
                }
                catch
                {
                }

                using (PDFDoc in_doc = new PDFDoc(FilePath))
                {
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
                                            try
                                            {
                                                using( Rect newRect = new Rect(0, 0, Width, Height) )
                                                {
                                                    draw.DrawInRect(currentPage, pdfGraphics, newRect);
                                                }
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
                                in_doc.Unlock();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                bool okToProceed = true;
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
                                    okToProceed = false;
                                }
                                if (okToProceed)
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
                                                using (Bitmap pdfBitmap = new Bitmap((int)(Width * 100.0 / 72.0), (int)(Height * 100.0 / 72.0)))
                                                {
                                                    using (Graphics pdfGraphics = Graphics.FromImage(pdfBitmap))
                                                    {
                                                        try
                                                        {
                                                            using (Rect newRect = new Rect(0, 0, Width, Height))
                                                            {
                                                                draw.DrawInRect(currentPage, pdfGraphics, newRect);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            okToProceed = false;
                                                        }
                                                        //String NewBmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
                                                        //pdfBitmap.Save(NewBmp);
                                                        if (okToProceed)
                                                        {
                                                            AddImage(ref new_doc, pdfBitmap);
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
                                                okToProceed = false;
                                            }
                                            if (!okToProceed)
                                            {
                                                break;
                                            }
                                            //}
                                        }
                                    }
                                    catch
                                    {
                                        okToProceed = false;
                                    }
                                    if (bLocked)
                                    {
                                        try
                                        {
                                            in_doc.Unlock();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (okToProceed)
                                    {
                                        String NewFilePath = Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, Guid.NewGuid().ToString() + ".pdf");
                                        try
                                        {

                                            new_doc.Save(NewFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                                        }
                                        catch
                                        {
                                            okToProceed = false;
                                        }
                                        if (new_doc != null)
                                        {
                                            try
                                            {
                                                new_doc.Dispose();
                                                new_doc = null;
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (okToProceed)
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
                                    return FilePath;
                                }
                            }
                        }
                    }
                    catch
                    {
                        bIsNotSupported = false;
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
                return FilePath;
            }
            finally
            {
                try
                {
                    gloEDocV3Admin.DisconnectToPDFTron();
                }
                catch
                {
                }
            }

        }

        private static void UnHandler(object sender, UnhandledExceptionEventArgs args)
        {
            errorHappened = true;
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHappened = true;
        }

        private static void AddImage(ref PDFDoc doc, System.Drawing.Bitmap bimp)
        {
            using (ElementBuilder bld = new ElementBuilder())   // Used to build new Element objects
            {
                using (ElementWriter writer = new ElementWriter())  // Used to write Elements to the page   
                {
                    Page page = doc.PageCreate();   // Start a new page 
                    writer.Begin(page);             // Begin writing to this page
                    using (pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc.GetSDFDoc(), bimp))
                    {
                        Element element = bld.CreateImage(img, 0.0, 0.0, (double)bimp.Width * 72.0 / 100.0, (double)bimp.Height * 72.0 / 100.0);
                        writer.WritePlacedElement(element);
                        writer.End();   // Finish writing to the page
                        doc.PagePushBack(page);
                    }
                    //String newFilePath =     Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                    //doc.Save(newFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_compatibility);
                }
            }
        }

        #region " Solutions tried"

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
