using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using pdftron.Common;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace gloEDocumentV3
{
    namespace SDKInteraction
    {
        public class eDocV3SDKInteraction
        {
            private string _ErrorMessage = "";
            bool _HasError = false;
            private int _EventProgressValue = 0;
            private int _EventProgressMaxValue = 0;
            private int _EventProgressThraySholdValue = 0;

            public string ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

            public bool HasError
            {
                get { return _HasError; }
                set { _HasError = value; }
            }


            public delegate void SDKProgress(int Percentage, string Message);
            public event SDKProgress SDKProgressEvent;

            #region "Constructor & Distructor"

            public eDocV3SDKInteraction()
            {

            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~eDocV3SDKInteraction()
            {
                Dispose(false);
            }

            #endregion

            private static void ErrorMessagees(string _ErrorMessage)
            {
                #region " Make Log Entry "
                try
                {
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorHere = ex.ToString();
                    MessageBox.Show("Unable to update Log with " + _ErrorMessage, _ErrorHere);
                }

                //End Code add
                #endregion " Make Log Entry "

            }

            #region "Dhruv 2010 -> ConvertImagesToPDF"

            public bool ConvertImagesToPDF(System.Collections.ArrayList oImageDocuments, string OutPutDocumentFilePath, out int OutPutFilePageCount)
            {
                pdftron.PDF.PDFDoc doc = new pdftron.PDF.PDFDoc();
                pdftron.PDF.Image img = null;
                pdftron.PDF.ElementBuilder oElementBuilder = new pdftron.PDF.ElementBuilder();	// Used to build new Element objects
                pdftron.PDF.ElementWriter oElementWriter = new pdftron.PDF.ElementWriter();	// Used to write Elements to the page	
                string _returnResult = "";
                pdftron.PDF.Element element = null;
                System.Drawing.Bitmap bmp = null;
                pdftron.PDF.Rect oRect = new pdftron.PDF.Rect(); ;
                pdftron.PDF.Page page = null;
                string _tempProcessPath = OutPutDocumentFilePath;
                bool _result = false;
                int _returnPageCount = 0;
                if (doc == null)
                {
                    _ErrorMessage = "Error is due to the Doc object is null";
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                    _returnResult = "";
                    System.Windows.Forms.MessageBox.Show("Exception in the doc", "ConvertImagesToPDF");
                    OutPutFilePageCount = 0;
                    return false;
                }

                try
                {
                    for (int i = 0; i < oImageDocuments.Count; i++)
                    {

                        try
                        {
                            bmp = new System.Drawing.Bitmap(oImageDocuments[i].ToString());

                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = "Error is due to the bmp object is null";
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                            _returnResult = "";
                            System.Windows.Forms.MessageBox.Show("Exception in the BMP", ex.ToString());
                            break;
                        }
                        if (bmp == null)
                        {
                            _ErrorMessage = "Error is due to the bmp object is null";
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                            _returnResult = "";
                            break;
                        }


                        int imgCount = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);


                        float _hr = bmp.HorizontalResolution;
                        float _vr = bmp.VerticalResolution;
                        float _wd = bmp.Width;
                        float _ht = bmp.Height;
                        //SLR: Why it was disposed?
                        //if (imgCount <= 1)
                        //{
                        //    bmp.Dispose();
                        //    bmp = null;
                        //}

                        int XWidth = 0;
                        int YHeight = 0;
                        bool XYRet = false;

                        XYRet = GetXWidthYHeight(_hr, _vr, _ht, _wd, out XWidth, out YHeight);


                        oRect.x1 = XWidth;// img.GetBitmap().Width;
                        oRect.x2 = 0;
                        oRect.y1 = YHeight;// img.GetBitmap().Height;
                        oRect.y2 = 0;


                        for (int j = 0; j < imgCount; j++)
                        {
                            if ((j >= 1) && (bmp != null))
                            {
                                // Select the current TIFF page using SelectActiveFrame
                                try
                                {
                                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, j);
                                }
                                catch //(Exception ex1)
                                {

                                }

                            }

                            //Set Page Size as of Rect
                            page = doc.PageCreate(oRect);
                            if (page != null)
                            {
                                oElementWriter.Begin(page);


                                if (imgCount <= 1)
                                {
                                    img = pdftron.PDF.Image.Create(doc, oImageDocuments[i].ToString());
                                }
                                else
                                {
                                    try
                                    {
                                        img = pdftron.PDF.Image.Create(doc, bmp);
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = "Error is due to the img object is null";
                                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                        _MessageString = "";
                                        _returnResult = "";

                                        System.Windows.Forms.MessageBox.Show("Error in Img", ex.ToString());
                                        break;
                                    }
                                }
                                if (img == null)
                                {
                                    _ErrorMessage = "Error is due to the img object is null";
                                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                    _MessageString = "";
                                    _returnResult = "";
                                    break;
                                }

                                element = oElementBuilder.CreateImage(img, new Matrix2D(XWidth, 0, 0, YHeight, 0, 0));// );//new Matrix2D(612, 0, 0, 794, 0, 0) //
                                if (element == null)
                                {
                                    _ErrorMessage = "Error is due to the element object is null";
                                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                    _MessageString = "";
                                    _returnResult = "";
                                    break;
                                }
                                oElementWriter.WritePlacedElement(element);
                                oElementWriter.End();
                                doc.PagePushBack(page);
                            }
                        }




                        if (page != null)
                            page = null;

                        if (bmp != null)
                        {
                            bmp.Dispose();
                            bmp = null;
                        }

                    }

                    doc.Save(_tempProcessPath, pdftron.SDF.SDFDoc.SaveOptions.e_linearized);
                    doc.Close();


                    if (File.Exists(_tempProcessPath) == true)
                    {
                        _returnPageCount = GetPageCount(_tempProcessPath);

                    }
                    _returnResult = _tempProcessPath;
                    _result = true;

                }
                catch (Exception ex)
                {


                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + ex.ToString();
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";


                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                    _returnResult = "";
                }
                finally
                {
                    if (doc != null)
                    {
                        doc.Dispose();
                        doc = null;
                    }

                    if (oElementBuilder != null)
                    {
                        oElementBuilder.Dispose();
                        oElementBuilder = null;
                    }

                    if (oElementBuilder != null)
                    {
                        oElementWriter.Dispose();
                        oElementWriter = null;
                    }

                    if (oRect != null)
                    {
                        oRect = null;
                    }

                    if (page != null)
                        page = null;

                }
                OutPutFilePageCount = _returnPageCount;
                return _result;
            }
            #endregion "Dhruv 2010 -> ConvertImagesToPDF"


            private bool GetXWidthYHeight(float HorRes, float VerRes, float pxHeight, float pxWidth, out int XWidth, out int YHeight)
            {
                bool _result = false;
                float _XWidth = 0;
                float _YHeight = 0;
                try
                {

                    bool _XMajor = false;
                    //string _Orientation = "S"; // P-Potrait, L-Landscape, S-Same
                    float _XInch = 0;
                    float _YInch = 0;


                    if (HorRes != 0) _XInch = pxHeight / HorRes;


                    if (VerRes != 0) _YInch = pxWidth / VerRes;

                    //Page Orientation
                    if (_XInch > _YInch)
                    { //_Orientation = "L";
                        _XMajor = false;
                    }
                    else if (_XInch < _YInch)
                    { //_Orientation = "P";
                        _XMajor = true;
                    }



                    //Calculate Return Height and Width in Pixcel
                    if (_XMajor == true)
                    {
                        _XWidth = pxWidth;
                        if (VerRes != 0) _YHeight = pxHeight * HorRes / VerRes;
                        else _YHeight = pxHeight;
                    }
                    else
                    {
                        _YHeight = pxHeight;
                        if (HorRes != 0) _XWidth = pxWidth * VerRes / HorRes;
                        else _XWidth = pxWidth;

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


                }


                XWidth = Convert.ToInt32(_XWidth);
                YHeight = Convert.ToInt32(_YHeight);
                return _result;
            }


            #region "Dhruv 2010 -> MergeDocuments"

            public bool MergeDocuments(System.Collections.ArrayList oDocuments, string OutPutDocumentFilePath, bool MergeInOutPutDocumentFile, out int OutPutFilePageCount)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result;

                pdftron.PDF.PDFDoc oMergeDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;

                int _NoOfPages = 0;
                int _FileCounter = 0;
                int _PageCounter = 0;
                int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;
                    if (oDocuments.Count > 0)
                    {
                        _EventProgressThraySholdValue = Convert.ToInt32(Math.Abs((_EventProgressMaxValue / oDocuments.Count)));
                    }
                    else
                    {
                        _EventProgressThraySholdValue = 1;
                    }
                    OutPutFilePageCount = _OutPutFilePageCount;


                    #region "Intialization"
                    if (MergeInOutPutDocumentFile == false)
                    {
                        if (File.Exists(OutPutDocumentFilePath) == true)
                        {
                            File.Delete(OutPutDocumentFilePath);
                        }
                    }
                    else
                    {
                        if (File.Exists(OutPutDocumentFilePath) == false)
                        {
                            return false;
                        }
                    }

                    if (MergeInOutPutDocumentFile == false)
                    {
                        oMergeDocument = new pdftron.PDF.PDFDoc();

                        _OutPutFileBeforePageCount = 0;
                    }
                    else
                    {
                        oMergeDocument = new pdftron.PDF.PDFDoc(OutPutDocumentFilePath);
                        if (oMergeDocument != null)
                        {
                            _OutPutFileBeforePageCount = oMergeDocument.GetPageCount();
                        }
                    }
                    #endregion
                    if (oMergeDocument != null)
                    {
                        for (_FileCounter = 0; _FileCounter <= oDocuments.Count - 1; _FileCounter++)
                        {
                            using (oSourceDocument = new pdftron.PDF.PDFDoc(oDocuments[_FileCounter].ToString()))
                            {
                                if (oSourceDocument != null)
                                {
                                    try
                                    {
                                        oSourceDocument.InitSecurityHandler();
                                    }
                                    catch (Exception)
                                    {

                                        //Intetionally left Blank
                                    }

                                    _NoOfPages = oSourceDocument.GetPageCount();

                                    for (_PageCounter = 1; _PageCounter <= _NoOfPages; _PageCounter++)
                                    {
                                        int nopages = oMergeDocument.GetPageCount();
                                        if (nopages == 0)
                                        {
                                            oPageWhere = null;
                                        }
                                        else
                                        {
                                            oPageWhere = oMergeDocument.GetPageIterator(nopages - 1);
                                        }
                                        oPage = oSourceDocument.GetPage(_PageCounter);

                                        if (oPage != null)
                                        {
                                            oMergeDocument.PagePushBack(oPage);
                                        }

                                        if (oSourceDocument != null)
                                        {
                                            oSourceDocument.Close();
                                            oSourceDocument.Dispose();
                                            oSourceDocument = null;
                                        }
                                    }
                                }
                            }
                            _EventProgressValue = _FileCounter * _EventProgressThraySholdValue;
                            if (_EventProgressValue > _EventProgressMaxValue)
                            {
                                _EventProgressValue = _EventProgressMaxValue;
                            }
                            SDKProgressEvent(_EventProgressValue, "Mergeing Documents");
                        }

                        oMergeDocument.Save(OutPutDocumentFilePath, 0);


                        if (MergeInOutPutDocumentFile == false)
                        {
                            _OutPutFilePageCount = oMergeDocument.GetPageCount();
                        }
                        else
                        {
                            _OutPutFilePageCount = oMergeDocument.GetPageCount() - _OutPutFileBeforePageCount;
                        }
                        if (oMergeDocument != null)
                        {
                            oMergeDocument.Close();
                            oMergeDocument.Dispose();
                            oMergeDocument = null;
                        }
                    }
                    _result = true;
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }



                OutPutFilePageCount = _OutPutFilePageCount;

                return _result;
            }
            #endregion "Dhruv 2010 -> MergeDocuments"


            public bool DeletePages(System.Collections.ArrayList oPagesInReverseOrder, string _FilePath, byte[] InputStream, out byte[] OutputStream, out int OutPutFilePageCount)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;

                bool _result = false;
                byte[] _OutputStream = null;
                int _OutPutFilePageCount = 0;

                pdftron.PDF.PDFDoc oDeleteDocument;
                pdftron.PDF.PageIterator oPage;

                int _PageCounter = 0;

                int _OutPutFileBeforePageCount = 0;

                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;
                    if (oPagesInReverseOrder.Count > 0)
                    {
                        _EventProgressThraySholdValue = Convert.ToInt32(Math.Abs((_EventProgressMaxValue / oPagesInReverseOrder.Count)));
                    }
                    else
                    {
                        _EventProgressThraySholdValue = 1;
                    }




                    oDeleteDocument = new pdftron.PDF.PDFDoc(_FilePath);
                    if (oDeleteDocument != null)
                    {
                        try
                        {
                            oDeleteDocument.InitSecurityHandler();
                        }
                        catch (Exception)
                        {

                            //Intetionally left Blank
                        }

                        _OutPutFileBeforePageCount = oDeleteDocument.GetPageCount();


                        for (_PageCounter = 0; _PageCounter <= oPagesInReverseOrder.Count - 1; _PageCounter++)
                        {
                            oPage = oDeleteDocument.GetPageIterator(Convert.ToInt32(oPagesInReverseOrder[_PageCounter].ToString()));

                            if (oPage != null)
                            {
                                oDeleteDocument.PageRemove(oPage);
                            }

                            _EventProgressValue = _PageCounter * _EventProgressThraySholdValue;
                            if (_EventProgressValue > _EventProgressMaxValue)
                            {
                                _EventProgressValue = _EventProgressMaxValue;
                            }
                            SDKProgressEvent(_EventProgressValue, "Deleteting Page " + oPagesInReverseOrder[_PageCounter].ToString());
                        }

                        byte[] buf = new byte[1];
                        //int buf_sz = 1;
                        oDeleteDocument.Save(_FilePath, 0);// (ref buf, ref buf_sz, pdftron.SDF.SDFDoc.SaveOptions.e_remove_unused);
                        _OutputStream = buf;

                        _OutPutFilePageCount = oDeleteDocument.GetPageCount();
                        if (oDeleteDocument != null)
                        {
                            oDeleteDocument.Close();
                            oDeleteDocument.Dispose();
                            oDeleteDocument = null;
                        }

                        _result = true;
                    }
                }
                catch (PDFNetException pdfEx)
                {
                    _result = false;
                    _HasError = true;
                    string _PdfErrorMessage = pdfEx.ToString() + "DeletePages";
                    ErrorMessagees(_PdfErrorMessage);
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }

                OutputStream = _OutputStream;
                OutPutFilePageCount = _OutPutFilePageCount;

                return _result;
            }




            #region "Dhruv 2010 -> GenerateOneFile"

            public pdftron.PDF.PDFDoc GenerateOneFile_old(System.Collections.ArrayList oPages, string InPutDocumentFilePath, pdftron.PDF.PDFDoc OutPutDocumentFile)//, string OutPutDocumentFilePath)
            {

                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;

                int _NoOfPages = 0;
                int _PageCounter = 0;

                try
                {



                    using (oSourceDocument = new pdftron.PDF.PDFDoc(InPutDocumentFilePath))
                    {
                        if (oSourceDocument != null)
                        {
                            try
                            {
                                oSourceDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _NoOfPages = oSourceDocument.GetPageCount();

                            int count;
                            count = OutPutDocumentFile.GetPageCount();
                            if (oPages != null)
                            {

                                for (_PageCounter = 1; _PageCounter <= oPages.Count; _PageCounter++)
                                {
                                    int _Counts = OutPutDocumentFile.GetPageCount();
                                    if (_Counts == 0)
                                    {
                                        oPageWhere = null;

                                    }
                                    else
                                    {

                                        oPageWhere = OutPutDocumentFile.GetPageIterator(OutPutDocumentFile.GetPageCount() - 1);
                                    }

                                    //oPage = oSourceDocument.GetPage(_PageCounter);
                                    oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter - 1]));
                                    if (oPage != null)
                                    {
                                        oPage.SetRotation(pdftron.PDF.Page.Rotate.e_0);

                                        if (oPageWhere != null || oPage != null)
                                        {
                                            //oMergeDocument.PageInsert(oPageWhere, oPage);
                                            OutPutDocumentFile.PagePushBack(oPage);
                                        }
                                        if (oPage != null)
                                        {
                                            oPage = null;
                                        }
                                    }



                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();

                    ErrorMessagees(_ErrorMessage);
                }


                return OutPutDocumentFile;
            }
            public pdftron.PDF.PDFDoc GenerateOneFile(System.Collections.ArrayList oPages, string InPutDocumentFilePath, pdftron.PDF.PDFDoc OutPutDocumentFile)//, string OutPutDocumentFilePath)
            {

                pdftron.PDF.PDFDoc oSourceDocument;
                //pdftron.PDF.PageIterator oPageWhere = null;
                //   pdftron.PDF.Page oPage;

                int _NoOfPages = 0;
                int _PageCounter = 0;

                try
                {



                    using (oSourceDocument = new pdftron.PDF.PDFDoc(InPutDocumentFilePath))
                    {
                        if (oSourceDocument != null)
                        {
                            try
                            {
                                oSourceDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _NoOfPages = oSourceDocument.GetPageCount();

                            int count;
                            count = OutPutDocumentFile.GetPageCount();
                            if (oPages != null)
                            {
                                ArrayList copy_pages = new ArrayList(); //new Code

                                for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                {

                                    Int32 iPage = Convert.ToInt32(oPages[_PageCounter]);
                                    pdftron.PDF.Page oPage = oSourceDocument.GetPage(iPage);

                                    PDFPageResize(ref oPage, false);

                                    copy_pages.Add(oPage);

                                    //oPageWhere = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                    //copy_pages.Add(oPageWhere.Current());

                                }
                                if (copy_pages != null && copy_pages.Count > 0) //(oPageWhere != null)
                                {
                                    ArrayList imported_Pages = OutPutDocumentFile.ImportPages(copy_pages);
                                    for (int i = 0; i != imported_Pages.Count; i++)
                                    {
                                        OutPutDocumentFile.PagePushBack((pdftron.PDF.Page)imported_Pages[i]);

                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();

                    ErrorMessagees(_ErrorMessage);
                }


                return OutPutDocumentFile;
            }
            //End/GLO2011-0011047	Lost scanned documents when moving from one folder to another
            #endregion "Dhruv 2010 -> GenerateOneFile"
            private static Single XMargin = 0F;
            private static Single YMargin = 0F;
            private static Single USLetterWidth = 612.0F - XMargin;
            private static Single USLetterHeight = 792.0F - YMargin;

            public static void PDFPageResize(ref pdftron.PDF.Page src_Page, bool bForPrint)
            {
                try
                {

                    double pageWidth = src_Page.GetPageWidth() - 1;
                    double pageHeight = src_Page.GetPageHeight() - 1;
                    Single pageScale = 1.0F;

                    if (bForPrint)
                    {
                        if ((pageWidth > pageHeight))
                        {
                            if ((pageWidth > USLetterHeight) | (pageHeight > USLetterWidth))
                            {
                                float pageScaleW = (float)(USLetterHeight / pageWidth);
                                float pageScaleH = (float)(USLetterWidth / pageHeight);
                                pageScale = Math.Min(pageScaleW, pageScaleH);
                            }
                        }
                        else
                        {
                            if ((pageWidth > USLetterWidth) | (pageHeight > USLetterHeight))
                            {
                                float pageScaleW = (float)(USLetterWidth / pageWidth);
                                float pageScaleH = (float)(USLetterHeight / pageHeight);
                                pageScale = Math.Min(pageScaleW, pageScaleH);
                            }
                        }

                    }
                    else
                    {
                        if ((pageWidth > USLetterWidth) | (pageHeight > USLetterHeight))
                        {
                            float pageScaleW = (float)(USLetterWidth / pageWidth);
                            float pageScaleH = (float)(USLetterHeight / pageHeight);
                            pageScale = Math.Min(pageScaleW, pageScaleH);
                        }
                    }
                    if (pageScale < 1.0F)
                    {
                        src_Page.Scale(pageScale);
                    }


                }
                catch (Exception ex)
                {
                    ErrorMessagees(ex.ToString());
                }
            }


            #region "Dhruv 2010 -> SplitPagesIntoOneFile"

            public bool SplitPagesIntoOneFile(System.Collections.ArrayList oPages, string InPutDocumentFilePath, string OutPutDocumentFilePath)
            {
                bool _result = false;

                pdftron.PDF.PDFDoc oMergeDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;

                int _NoOfPages = 0;
                int _PageCounter = 0;

                try
                {

                    using (oSourceDocument = new pdftron.PDF.PDFDoc(InPutDocumentFilePath))
                    {
                        if (oSourceDocument != null)
                        {
                            try
                            {
                                oSourceDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _NoOfPages = oSourceDocument.GetPageCount();
                            using (oMergeDocument = new pdftron.PDF.PDFDoc())
                            {
                                if (oMergeDocument != null)
                                {
                                    for (_PageCounter = 1; _PageCounter <= oPages.Count; _PageCounter++)
                                    {
                                        if (_NoOfPages == 0)
                                        {
                                            oPageWhere = null;
                                        }
                                        else
                                        {
                                            oPageWhere = oMergeDocument.GetPageIterator(oMergeDocument.GetPageCount() - 1);

                                            //oPage = oSourceDocument.GetPage(_PageCounter);
                                            oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter - 1]));
                                            if (oPage != null)
                                            {
                                                oPage.SetRotation(pdftron.PDF.Page.Rotate.e_0);

                                                if (oPageWhere != null || oPage != null)
                                                {
                                                    //oMergeDocument.PageInsert(oPageWhere, oPage);
                                                    oMergeDocument.PagePushBack(oPage);
                                                }
                                                if (oPage != null)
                                                {
                                                    oPage = null;
                                                }
                                            }
                                        }
                                    }


                                    oMergeDocument.Save(OutPutDocumentFilePath, 0);
                                    _result = true;
                                }
                            }
                        }
                        if (oSourceDocument != null)
                        {
                            oSourceDocument.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }




                return _result;
            }

            #endregion "Dhruv 2010 -> SplitPagesIntoOneFile"


            #region "Dhruv 2010 -> SplitPagesIntoOneFile"

            public bool SplitPagesIntoOneFile(Int32 FromPage, Int32 ToPage, string InPutDocumentFilePath, string OutPutDocumentFilePath)
            {
                bool _result = false;

                pdftron.PDF.PDFDoc oMergeDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;

                int _NoOfPages = 0;
                int _PageCounter = 0;

                try
                {


                    using (oSourceDocument = new pdftron.PDF.PDFDoc(InPutDocumentFilePath))
                    {
                        if (oSourceDocument != null)
                        {
                            try
                            {
                                oSourceDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _NoOfPages = oSourceDocument.GetPageCount();
                            using (oMergeDocument = new pdftron.PDF.PDFDoc())
                            {
                                if (oMergeDocument != null)
                                {
                                    for (_PageCounter = FromPage; _PageCounter <= ToPage; _PageCounter++)
                                    {
                                        int _NoOfPagess = oSourceDocument.GetPageCount();
                                        if (_NoOfPagess == 0)
                                        {
                                            oPageWhere = null;
                                        }
                                        else
                                        {
                                            oPageWhere = oMergeDocument.GetPageIterator(oMergeDocument.GetPageCount() - 1);//
                                        }

                                        oPage = oSourceDocument.GetPage(_PageCounter);//
                                        if (oPage != null)
                                        {
                                            oPage.SetRotation(pdftron.PDF.Page.Rotate.e_0);

                                            if (oPageWhere != null || oPage != null)
                                            {
                                                oMergeDocument.PagePushBack(oPage);
                                            }
                                            if (oPage != null)
                                            {
                                                oPage = null;
                                            }
                                        }

                                    }

                                    oMergeDocument.Save(OutPutDocumentFilePath, 0);
                                    _result = true;
                                }
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);

                    _result = false;
                }




                return _result;
            }
            #endregion "Dhruv 2010 -> SplitPagesIntoOneFile"



            #region "Dhruv 2010 -> PDFToTIFF"

            public bool PDFToTIFF(string InPutDocumentFilePath, string OutPutDocumentFilePath)
            {
                bool _result;

                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PDFDraw oSourceDraw;
                //  pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;
                ArrayList oTifFiles = new ArrayList();

                int _NoOfPages = 0;
                int _PageCounter = 0;
                string _TIFFTempFolder = gloEDocV3Admin.gPDFTronTemporaryProcessPath + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");// +System.Guid.NewGuid().ToString();
                if (Directory.Exists(_TIFFTempFolder) == false)
                {
                    Directory.CreateDirectory(_TIFFTempFolder);
                    if (Directory.Exists(_TIFFTempFolder) == false)
                    {
                        _ErrorMessage = "Unable to create the directory." + _TIFFTempFolder;
                        ErrorMessagees(_ErrorMessage);
                    }

                }

                try
                {
                    using (oSourceDocument = new pdftron.PDF.PDFDoc(InPutDocumentFilePath))
                    {
                        if (oSourceDocument != null)
                        {
                            try
                            {
                                oSourceDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _NoOfPages = oSourceDocument.GetPageCount();
                            using (oSourceDraw = new pdftron.PDF.PDFDraw())
                            {
                                if (oSourceDraw != null)
                                {
                                    for (_PageCounter = 1; _PageCounter <= _NoOfPages; _PageCounter++)
                                    {
                                        int _NoOfPagess = oSourceDocument.GetPageCount();
                                        if (_NoOfPagess == 0)
                                        {
                                            oPage = null;
                                        }
                                        else
                                        {
                                            oPage = oSourceDocument.GetPage(_PageCounter);
                                        }
                                        if (oPage != null)
                                        {
                                            oSourceDraw.Export(oPage, _TIFFTempFolder + "\\" + _PageCounter.ToString() + ".tif");
                                            if (File.Exists(_TIFFTempFolder + "\\" + _PageCounter.ToString() + ".tif") == true)
                                            {
                                                oTifFiles.Add(_TIFFTempFolder + "\\" + _PageCounter.ToString() + ".tif");
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }

                    eDocTIFFManager oTiffManager = new eDocTIFFManager();
                    //oTiffManager.JoinTiffImages(oTifFiles, OutPutDocumentFilePath, EncoderValue.CompressionNone);
                    if (oTiffManager != null)
                    {
                        oTiffManager.JoinTiffImages_New(oTifFiles, OutPutDocumentFilePath, EncoderValue.CompressionNone);
                        if (oTiffManager != null)
                        {
                            oTiffManager.Dispose();
                            oTiffManager = null;

                        }
                    }



                    _result = true;
                    try
                    {
                        Directory.Delete(_TIFFTempFolder, true);
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        ErrorMessagees(_ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);


                    _result = false;
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> PDFToTIFF"

            #region "Dhruv 2010 -> GetPageCount"

            public Int32 GetPageCount(string FileFullPath)
            {
                Int32 _result = 0;

                pdftron.PDF.PDFDoc oDocument;

                try
                {
                    using (oDocument = new pdftron.PDF.PDFDoc(FileFullPath))
                    {
                        if (oDocument != null)
                        {
                            try
                            {
                                oDocument.InitSecurityHandler();
                            }
                            catch (Exception)
                            {

                                //Intetionally left Blank
                            }

                            _result = oDocument.GetPageCount();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _result = 0;
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                    _result = 0;
                }


                return _result;
            }

            #endregion "Dhruv 2010 -> GetPageCount"


            #region "Work on File using Byte stream"

            #region "Dhruv 2010 -> MergePagesinExistingDocument"

            public bool MergePagesinExistingDocument(System.Collections.ArrayList oPages, ref byte[] SourceStream, ref byte[] DestinationStream)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result = false;

                pdftron.PDF.PDFDoc oDestinationDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;
                pdftron.PDF.PageIterator oPageRemove;
                byte[] _SourceStream = null;
                byte[] _DestinationStream = null;
                int _NoOfPages = 0;
                //  int _FileCounter = 0;
                int _PageCounter = 0;
                //  int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;


                    _SourceStream = SourceStream;
                    _DestinationStream = DestinationStream;
                    #region "Intialization"

                    if (_DestinationStream == null)
                    {
                        oDestinationDocument = new pdftron.PDF.PDFDoc();
                        _OutPutFileBeforePageCount = 0;
                    }
                    else
                    {
                        oDestinationDocument = new pdftron.PDF.PDFDoc(_DestinationStream, _DestinationStream.Length);
                        if (oDestinationDocument != null)
                        {
                            _OutPutFileBeforePageCount = oDestinationDocument.GetPageCount();
                        }
                    }
                    #endregion
                    if (oDestinationDocument != null)
                    {
                        using (oSourceDocument = new pdftron.PDF.PDFDoc(_SourceStream, _SourceStream.Length))
                        {
                            if (oSourceDocument != null)
                            {
                                try
                                {
                                    oSourceDocument.InitSecurityHandler();
                                }
                                catch (Exception)
                                {

                                    //Intetionally left Blank
                                }

                                _NoOfPages = oSourceDocument.GetPageCount();

                                for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                {
                                    int _NoOfPagess = oSourceDocument.GetPageCount();
                                    if (_NoOfPagess == 0)
                                    {
                                        oPageWhere = null;
                                    }
                                    else
                                    {
                                        oPageWhere = oDestinationDocument.GetPageIterator(oDestinationDocument.GetPageCount() - 1);
                                    }

                                    oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter]));
                                    if (oPage != null)
                                    {
                                        oPageRemove = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                        if (oPageWhere != null || oPage != null || oPageRemove != null)
                                        {
                                            oDestinationDocument.PagePushBack(oPage);
                                        }
                                        //if (oPageRemove != null)
                                        //{
                                        //    oSourceDocument.PageRemove(oPageRemove);
                                        //}
                                    }
                                }
                                _PageCounter = 0;
                                for (_PageCounter = oPages.Count - 1; _PageCounter >= 0; _PageCounter--)
                                {

                                    oPageRemove = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                    if (oPageRemove != null)
                                    {
                                        oSourceDocument.PageRemove(oPageRemove);
                                    }
                                }



                            }
                            if (oSourceDocument != null)
                            {
                                oSourceDocument.Close();
                            }
                        }
                        if (oDestinationDocument != null)
                        {
                            oDestinationDocument.Close();
                            oDestinationDocument.Dispose();
                            oDestinationDocument = null;
                        }
                        _result = true;
                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                    _result = false;

                }
                finally
                {
                    if (SourceStream != null)
                    {

                        SourceStream = null;
                    }
                    if (DestinationStream != null)
                    {
                        DestinationStream = null;
                    }
                    SourceStream = _SourceStream;
                    DestinationStream = _DestinationStream;
                }


                return _result;
            }
            #endregion "Dhruv 2010 -> MergePagesinExistingDocument"


            #region "Dhruv 2010 -> MergePagesinExistingDocument"



            //Start/GLO2011-0011047	Lost scanned documents when moving from one folder to another
            public bool MergePagesinExistingDocument_old(System.Collections.ArrayList oPages, string SourceFilePath, string DestFilePath)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result = false;

                pdftron.PDF.PDFDoc oDestinationDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;
                pdftron.PDF.PageIterator oPageRemove;
                //      byte[] _SourceStream = null;
                //      byte[] _DestinationStream = null;
                int _NoOfPages = 0;
                //     int _FileCounter = 0;
                int _PageCounter = 0;
                //     int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;

                    if (File.Exists(SourceFilePath) == true)
                    {
                        #region "Intialization"

                        if (File.Exists(DestFilePath) == false)
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc();
                            _OutPutFileBeforePageCount = 0;
                        }
                        else
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc(DestFilePath);
                            if (oDestinationDocument != null)
                            {
                                _OutPutFileBeforePageCount = oDestinationDocument.GetPageCount();
                            }
                        }
                        #endregion
                        if (oDestinationDocument != null)
                        {
                            using (oSourceDocument = new pdftron.PDF.PDFDoc(SourceFilePath))
                            {
                                if (oSourceDocument != null)
                                {
                                    if (oPages != null)
                                    {
                                        _NoOfPages = oSourceDocument.GetPageCount();
                                        int PageCount = 0;
                                        for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                        {

                                            PageCount = oDestinationDocument.GetPageCount();
                                            if (PageCount == 0)
                                            {
                                                oPageWhere = null;//oDestinationDocument.GetPageIterator(0);
                                            }
                                            else
                                            {
                                                oPageWhere = oDestinationDocument.GetPageIterator(oDestinationDocument.GetPageCount() - 1);
                                            }
                                            oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter]));
                                            if (oPageWhere != null || oPage != null)
                                            {
                                                oDestinationDocument.PagePushBack(oPage);
                                            }
                                        }
                                        _PageCounter = 0;
                                        for (_PageCounter = oPages.Count - 1; _PageCounter >= 0; _PageCounter--)
                                        {

                                            oPageRemove = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                            if (oPageRemove != null)
                                            {
                                                oSourceDocument.PageRemove(oPageRemove);
                                            }
                                        }
                                        oSourceDocument.Save(SourceFilePath, 0);

                                        oDestinationDocument.Save(DestFilePath, 0);

                                        _result = true;
                                    }
                                }
                                if (oSourceDocument != null)
                                {
                                    oSourceDocument.Close();

                                }
                            }
                        }
                        else
                        {
                            _result = false;
                        }

                        if (oDestinationDocument != null)
                        {
                            oDestinationDocument.Close();
                            oDestinationDocument.Dispose();
                            oDestinationDocument = null;
                        }
                    }


                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                    _result = false;
                }

                return _result;
            }
            public bool MergePagesinExistingDocument(System.Collections.ArrayList oPages, string SourceFilePath, string DestFilePath)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result = false;

                pdftron.PDF.PDFDoc oDestinationDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere = null;//new Code
                //     pdftron.PDF.Page oPage = null;
                pdftron.PDF.PageIterator oPageRemove;
                //    byte[] _SourceStream = null;
                //    byte[] _DestinationStream = null;
                int _NoOfPages = 0;
                //    int _FileCounter = 0;
                int _PageCounter = 0;
                //    int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;

                    ArrayList copy_pages = new ArrayList(); //new Code

                    if (File.Exists(SourceFilePath) == true)
                    {
                        #region "Intialization"

                        if (File.Exists(DestFilePath) == false)
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc();
                            _OutPutFileBeforePageCount = 0;
                        }
                        else
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc(DestFilePath);
                            if (oDestinationDocument != null)
                            {
                                _OutPutFileBeforePageCount = oDestinationDocument.GetPageCount();
                            }
                        }
                        #endregion
                        if (oDestinationDocument != null)
                        {
                            using (oSourceDocument = new pdftron.PDF.PDFDoc(SourceFilePath))
                            {
                                if (oSourceDocument != null)
                                {
                                    if (oPages != null)
                                    {
                                        _NoOfPages = oSourceDocument.GetPageCount();
                                        //  int PageCount = 0;
                                        for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                        {

                                            //PageCount = oSourceDocument.GetPageCount();
                                            //if (PageCount == 0)
                                            //{
                                            //    oPageWhere = null;//oDestinationDocument.GetPageIterator(0);
                                            //}
                                            //else
                                            //{
                                            //    //oPageWhere = oDestinationDocument.GetPageIterator(oDestinationDocument.GetPageCount() - 1);
                                            //    copy_pages.Add(oPageWhere.Current());

                                            //}
                                            //oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter]));
                                            //if (PageCount == 0)
                                            //{
                                            //    oPageWhere = null;
                                            //}
                                            //else
                                            //{
                                            oPageWhere = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                            copy_pages.Add(oPageWhere.Current());

                                            //}

                                        }
                                        //oDestinationDocument.ImportPages(copy_pages);
                                        if (oPageWhere != null)
                                        {
                                            ArrayList imported_Pages = oDestinationDocument.ImportPages(copy_pages);
                                            for (int i = 0; i != imported_Pages.Count; ++i)
                                            {
                                                oDestinationDocument.PagePushBack((pdftron.PDF.Page)imported_Pages[i]);

                                            }
                                        }
                                        _PageCounter = 0;
                                        for (_PageCounter = oPages.Count - 1; _PageCounter >= 0; _PageCounter--)
                                        {

                                            oPageRemove = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                            if (oPageRemove != null)
                                            {
                                                oSourceDocument.PageRemove(oPageRemove);
                                            }
                                        }
                                        oSourceDocument.Save(SourceFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_remove_unused);

                                        oDestinationDocument.Save(DestFilePath, pdftron.SDF.SDFDoc.SaveOptions.e_remove_unused);

                                        _result = true;
                                    }
                                }
                                if (oSourceDocument != null)
                                {
                                    oSourceDocument.Close();

                                }
                            }
                        }
                        else
                        {
                            _result = false;
                        }

                        if (oDestinationDocument != null)
                        {
                            oDestinationDocument.Close();
                            oDestinationDocument.Dispose();
                            oDestinationDocument = null;
                        }
                    }


                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                    _result = false;
                }

                return _result;
            }
            //End/GLO2011-0011047	Lost scanned documents when moving from one folder to another

            #endregion "Dhruv 2010 -> MergePagesinExistingDocument"


            #region "Dhruv 2010 -> MergePagesinExistingDocument"


            //Start/GLO2011-0011047	Lost scanned documents when moving from one folder to another
            public bool MergePagesinExistingDocument_old(System.Collections.ArrayList oPages, string SourceFilePath, string DestFilePath, Boolean IsFaxCoverPage)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result = false;

                pdftron.PDF.PDFDoc oDestinationDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere;
                pdftron.PDF.Page oPage;
                //    pdftron.PDF.PageIterator oPageRemove;
                //    byte[] _SourceStream = null;
                //    byte[] _DestinationStream = null;
                int _NoOfPages = 0;
                //     int _FileCounter = 0;
                int _PageCounter = 0;
                //     int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;

                    if (File.Exists(SourceFilePath) == true)
                    {
                        #region "Intialization"

                        if (File.Exists(DestFilePath) == false)
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc();
                            _OutPutFileBeforePageCount = 0;
                        }
                        else
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc(DestFilePath);
                            if (oDestinationDocument != null)
                            {
                                _OutPutFileBeforePageCount = oDestinationDocument.GetPageCount();
                            }
                        }
                        #endregion
                        if (oDestinationDocument != null)
                        {
                            using (oSourceDocument = new pdftron.PDF.PDFDoc(SourceFilePath))
                            {
                                if (oSourceDocument != null)
                                {
                                    if (oPages != null)
                                    {
                                        _NoOfPages = oSourceDocument.GetPageCount();
                                        int PageCount = 0;
                                        for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                        {
                                            PageCount = oDestinationDocument.GetPageCount();
                                            if (PageCount == 0)
                                            {
                                                oPageWhere = null;
                                            }
                                            else
                                            {
                                                oPageWhere = oDestinationDocument.GetPageIterator(oDestinationDocument.GetPageCount() - 1);
                                            }

                                            oPage = oSourceDocument.GetPage(Convert.ToInt32(oPages[_PageCounter]));
                                            if (oPageWhere != null || oPage != null)
                                            {
                                                oDestinationDocument.PagePushBack(oPage);
                                            }

                                        }




                                        //oSourceDocument.Save(SourceFilePath, 0);

                                        oDestinationDocument.Save(DestFilePath, 0);


                                        _result = true;
                                    }
                                }
                            }
                            if (oSourceDocument != null)
                            {
                                oSourceDocument.Close();
                            }
                        }
                        else
                        {
                            _result = false;
                        }

                        if (oDestinationDocument != null)
                        {
                            oDestinationDocument.Close();
                            oDestinationDocument.Dispose();
                            oDestinationDocument = null;
                        }

                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }

                return _result;
            }
            public bool MergePagesinExistingDocument(System.Collections.ArrayList oPages, string SourceFilePath, string DestFilePath, Boolean IsFaxCoverPage)
            {
                _EventProgressValue = 0;
                _EventProgressMaxValue = 0;
                _EventProgressThraySholdValue = 0;
                bool _result = false;

                pdftron.PDF.PDFDoc oDestinationDocument;
                pdftron.PDF.PDFDoc oSourceDocument;
                pdftron.PDF.PageIterator oPageWhere = null;
                //    pdftron.PDF.Page oPage;
                //   pdftron.PDF.PageIterator oPageRemove;
                //  byte[] _SourceStream = null;
                // byte[] _DestinationStream = null;
                int _NoOfPages = 0;
                //   int _FileCounter = 0;
                int _PageCounter = 0;
                //    int _OutPutFilePageCount = 0;
                int _OutPutFileBeforePageCount = 0;
                try
                {

                    _EventProgressValue = 0;
                    _EventProgressMaxValue = 100;

                    if (File.Exists(SourceFilePath) == true)
                    {
                        #region "Intialization"

                        if (File.Exists(DestFilePath) == false)
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc();
                            _OutPutFileBeforePageCount = 0;
                        }
                        else
                        {
                            oDestinationDocument = new pdftron.PDF.PDFDoc(DestFilePath);
                            if (oDestinationDocument != null)
                            {
                                _OutPutFileBeforePageCount = oDestinationDocument.GetPageCount();
                            }
                        }
                        #endregion
                        if (oDestinationDocument != null)
                        {
                            using (oSourceDocument = new pdftron.PDF.PDFDoc(SourceFilePath))
                            {
                                if (oSourceDocument != null)
                                {
                                    if (oPages != null)
                                    {
                                        _NoOfPages = oSourceDocument.GetPageCount();
                                        //  int PageCount = 0;

                                        ArrayList copy_pages = new ArrayList(); //new Code
                                        for (_PageCounter = 0; _PageCounter <= oPages.Count - 1; _PageCounter++)
                                        {
                                            oPageWhere = oSourceDocument.GetPageIterator(Convert.ToInt32(oPages[_PageCounter]));
                                            copy_pages.Add(oPageWhere.Current());

                                        }
                                        if (oPageWhere != null)
                                        {
                                            ArrayList imported_Pages = oDestinationDocument.ImportPages(copy_pages);
                                            for (int i = 0; i != imported_Pages.Count; ++i)
                                            {
                                                oDestinationDocument.PagePushBack((pdftron.PDF.Page)imported_Pages[i]);

                                            }
                                        }

                                        oDestinationDocument.Save(DestFilePath, 0);

                                        _result = true;
                                    }
                                }
                            }
                            if (oSourceDocument != null)
                            {
                                oSourceDocument.Close();
                            }
                        }
                        else
                        {
                            _result = false;
                        }

                        if (oDestinationDocument != null)
                        {
                            oDestinationDocument.Close();
                            oDestinationDocument.Dispose();
                            oDestinationDocument = null;
                        }

                    }
                }
                catch (Exception ex)
                {
                    _result = false;
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }

                return _result;
            }
            //End/GLO2011-0011047	Lost scanned documents when moving from one folder to another
            #endregion "Dhruv 2010 -> MergePagesinExistingDocument"


            #endregion

        }

        internal class eDocTIFFManager
        {
            private string _ImageFileName;
            private int _PageNumber;
            private Image image;
            private string _TempWorkingDir = gloEDocV3Admin.gPDFTronTemporaryProcessPath + "\\TiffProcessFolder";



            #region "Dhruv 2010 -> eDocTIFFManager"

            public eDocTIFFManager(string imageFileName)
            {
                this._ImageFileName = imageFileName;
                image = Image.FromFile(_ImageFileName);
                GetPageNumber();

                if (!gloGlobal.clsFileExtensions.IsSystemOrRootDir(_TempWorkingDir))
                {
                    try
                    {
                        if (Directory.Exists(_TempWorkingDir))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(_TempWorkingDir);
                            if (dirInfo != null)
                            {
                                dirInfo.Delete(true);

                                if (dirInfo != null)
                                {
                                    dirInfo = null;
                                }
                            }

                        }
                    }
                    catch
                    {


                    }
                    try
                    {
                        Directory.CreateDirectory(_TempWorkingDir);
                    }
                    catch
                    {
                    }

                }
                if (Directory.Exists(_TempWorkingDir) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + _TempWorkingDir;

                }

            }
            #endregion "Dhruv 2010 -> eDocTIFFManager"



            #region "Dhruv 2010 -> eDocTIFFManager"

            public eDocTIFFManager()
            {
                if (!gloGlobal.clsFileExtensions.IsSystemOrRootDir(_TempWorkingDir))
                {
                    try
                    {
                        if (Directory.Exists(_TempWorkingDir))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(_TempWorkingDir);
                            if (dirInfo != null)
                            {
                                dirInfo.Delete(true);

                                if (dirInfo != null)
                                {
                                    dirInfo = null;
                                }
                            }

                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        Directory.CreateDirectory(_TempWorkingDir);
                    }
                    catch
                    {
                    }
                }
                if (Directory.Exists(_TempWorkingDir) == false)
                {
                    string _ErrorMessage = "Unable to create directory. " + _TempWorkingDir;
                }


            }
            #endregion "Dhruv 2010 -> eDocTIFFManager"



            #region "Dhruv 2010 -> GetPageNumber"

            private void GetPageNumber()
            {
                _PageNumber = 0;
                if (image != null)
                {
                    Guid[] objGuids = image.FrameDimensionsList;

                    if (objGuids.Length >= 0)
                    {
                        Guid objGuid = objGuids[0];
                        if (objGuid != null)
                        {
                            FrameDimension objDimension = new FrameDimension(objGuid);
                            if (objDimension != null)
                            {
                                _PageNumber = image.GetFrameCount(objDimension);
                                return;
                            }

                        }
                    }
                }

            }



            #endregion "Dhruv 2010 -> GetPageNumber"



            #region "Dhruv 2010 -> GetPageNumber"

            private void GetPageNumber(string imageFileName)
            {
                string _ErrorMessage = "";

                try
                {
                    this._ImageFileName = imageFileName;
                    image = Image.FromFile(_ImageFileName);
                    GetPageNumber();

                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);

                    MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _PageNumber = 0;
                    return;
                }

            }

            #endregion "Dhruv 2010 -> GetPageNumber"


            #region "Dhruv 2010 -> GetFileNameStartString"


            private string GetFileNameStartString(string strFullName)
            {
                int posDot = _ImageFileName.LastIndexOf(".");
                if (posDot == -1)
                {
                    posDot = _ImageFileName.Length;
                }

                int posSlash = _ImageFileName.LastIndexOf(@"\");
                return _ImageFileName.Substring(posSlash + 1, posDot - posSlash - 1);
            }

            #endregion "Dhruv 2010 -> GetFileNameStartString"


            #region "Dhruv 2010 -> SplitTiffImage"


            public ArrayList SplitTiffImage(string outPutDirectory, EncoderValue format)
            {
                string fileStartString = outPutDirectory + "\\" + GetFileNameStartString(_ImageFileName);
                ArrayList splitedFileNames = new ArrayList();
                string _ErrorMessage = "";
                try
                {
                    if (image != null)
                    {
                        Guid[] objGuids = image.FrameDimensionsList;
                        if (objGuids.Length >= 0)
                        {
                            for (int ij = 0; ij < objGuids.Length; ij++)
                            {
                                Guid objGuid = objGuids[ij];
                                if (objGuid != null)
                                {
                                    FrameDimension objDimension = new FrameDimension(objGuid);
                                    if (objDimension != null)
                                    {
                                        //Saves every frame as a separate file.
                                        System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.Compression;
                                        int curFrame = 0;
                                        for (int i = 0; i < _PageNumber; i++)
                                        {
                                            if (objDimension != null)
                                            {
                                                // Select the current TIFF page using SelectActiveFrame
                                                try
                                                {
                                                    image.SelectActiveFrame(objDimension, curFrame);
                                                }
                                                catch //(Exception Ex)
                                                {
                                                    try
                                                    {
                                                        image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                    }
                                                    catch //(Exception ex2)
                                                    {

                                                    }

                                                }
                                            }

                                            else
                                            {
                                                try
                                                {
                                                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                }
                                                catch //(Exception ex3)
                                                {

                                                }
                                            }
                                            //image.SelectActiveFrame(objDimension, curFrame);

                                            using (EncoderParameters ep = new EncoderParameters(1))
                                            {
                                                if (ep != null)
                                                {
                                                    using (ep.Param[0] = new EncoderParameter(enc, (long)format))
                                                    {
                                                        if (ep.Param != null)
                                                        {
                                                            if (ep.Param.Length >= 0)
                                                            {
                                                                ImageCodecInfo info = GetEncoderInfo("image/tiff");

                                                                //Save the master bitmap
                                                                string fileName = string.Format("{0}{1}.TIF", fileStartString, i.ToString());
                                                                try
                                                                {
                                                                    image.Save(fileName, info, ep);
                                                                    splitedFileNames.Add(fileName);
                                                                    curFrame++;

                                                                }
                                                                catch //(Exception ex)
                                                                {

                                                                }


                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        if (objDimension != null)
                                        {
                                            objDimension = null;
                                        }

                                    }

                                }


                            }//if (objLength)
                        }//for(int ij)
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    return null;

                }


                return splitedFileNames;
            }

            #endregion "Dhruv 2010 -> SplitTiffImage"




            #region "Dhruv 2010 -> SplitTiffImage"


            public ArrayList SplitTiffImage(string outPutDirectory, EncoderValue format, string imageFileName)
            {

                string fileStartString = "";
                ArrayList splitedFileNames = new ArrayList();
                try
                {

                    if (File.Exists(imageFileName))
                    {
                        System.IO.FileInfo oFile = new System.IO.FileInfo(imageFileName);

                        this._ImageFileName = oFile.Name;
                        image = Image.FromFile(imageFileName);
                        if (image != null)
                        {
                            GetPageNumber();

                            fileStartString = outPutDirectory + "\\" + GetFileNameStartString(_ImageFileName);
                            Guid[] objGuids = image.FrameDimensionsList;
                            if (objGuids.Length >= 0)
                            {
                                for (int ij = 0; ij < objGuids.Length; ij++)
                                {
                                    Guid objGuid = objGuids[ij];
                                    if (objGuid != null)
                                    {
                                        FrameDimension objDimension = new FrameDimension(objGuid);
                                        if (objDimension != null)
                                        {
                                            //Saves every frame as a separate file.
                                            System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.Compression;
                                            int curFrame = 0;
                                            for (int i = 0; i < _PageNumber; i++)
                                            {
                                                if (objDimension != null)
                                                {
                                                    try
                                                    {
                                                        // Select the current TIFF page using SelectActiveFrame
                                                        image.SelectActiveFrame(objDimension, curFrame);
                                                    }
                                                    catch //(Exception ex)
                                                    {
                                                        try
                                                        {
                                                            image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                        }
                                                        catch //(Exception ex1)
                                                        {
                                                        }


                                                    }
                                                }

                                                else
                                                {
                                                    try
                                                    {
                                                        image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                    }
                                                    catch //(Exception ex1)
                                                    {
                                                    }
                                                }
                                                //image.SelectActiveFrame(objDimension, curFrame);
                                                using (EncoderParameters ep = new EncoderParameters(1))
                                                {
                                                    if (ep != null)
                                                    {
                                                        //ep.Param[0] = new EncoderParameter(enc, (long)format);
                                                        ImageCodecInfo info = GetEncoderInfo("image/tiff");


                                                        //check for the horizontal & vertical resolution of the file
                                                        //if 0 set to default i.e 96 >< 96
                                                        if (image.HorizontalResolution <= 0 && image.VerticalResolution <= 0)
                                                        {

                                                            Bitmap btImage = null;
                                                            //    Image tempImage = null;
                                                            btImage = (Bitmap)image.Clone();
                                                            btImage.SetResolution(96, 96);
                                                            //tempImage = (Image)btImage;

                                                            int _curPgNo = i + 1;
                                                            string _strFileExt = _curPgNo.ToString("#000");

                                                            string fileName = string.Format("{0}{1}.TIF", fileStartString, _strFileExt);

                                                            using (ep.Param[0] = new EncoderParameter(enc, Convert.ToInt64(EncoderValue.CompressionLZW.GetHashCode())))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        btImage.Save(fileName, info, ep);
                                                                        splitedFileNames.Add(fileName);

                                                                        if (btImage != null)
                                                                        {
                                                                            btImage.Dispose();
                                                                            btImage = null;
                                                                        }

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (btImage != null)
                                                                    {
                                                                        btImage.Dispose();
                                                                        btImage = null;
                                                                    }

                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)format))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        int _curPgNo = i + 1;
                                                                        string _strFileExt = _curPgNo.ToString("#000");

                                                                        string fileName = string.Format("{0}{1}.TIF", fileStartString, _strFileExt);
                                                                        try
                                                                        {
                                                                            image.Save(fileName, info, ep);
                                                                            splitedFileNames.Add(fileName);
                                                                        }
                                                                        catch //(Exception ex)
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                                curFrame++;
                                            }

                                        }
                                    }
                                }//if(objGuid)

                            }//for (ij)
                        }
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    ErrorMessagees(_ErrorMessage);


                    return null;

                }
                finally
                {
                    if (image != null)
                    {
                        image.Dispose();
                        image = null;
                    }
                }

                return splitedFileNames;
            }

            #endregion "Dhruv 2010 -> SplitTiffImage"

            #region "Dhruv 2010 -> SplitTiffImage"

            public ArrayList SplitTiffImage(string outPutDirectory, EncoderValue format, string imageFileName, int nRowCount)
            {
                string fileStartString = "";
                ArrayList splitedFileNames = new ArrayList();
                imageFileName = imageFileName.Replace(".TIFF", ".TIF");
                try
                {
                    if (File.Exists(imageFileName))
                    {
                        if (image != null)
                        {
                            this._ImageFileName = imageFileName;
                            image = Image.FromFile(_ImageFileName);
                            GetPageNumber();

                            fileStartString = outPutDirectory + "\\" + GetFileNameStartString(_ImageFileName);
                            Guid[] objGuids = image.FrameDimensionsList;
                            if (objGuids.Length >= 0)
                            {
                                for (int ij = 0; ij < objGuids.Length; ij++)
                                {
                                    Guid objGuid = objGuids[ij];
                                    FrameDimension objDimension = new FrameDimension(objGuid);
                                    if (objDimension != null)
                                    {
                                        //Saves every frame as a separate file.
                                        System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.Compression;
                                        int curFrame = 0;
                                        for (int i = 0; i < _PageNumber; i++)
                                        {
                                            if (objDimension != null)
                                            {
                                                // Select the current TIFF page using SelectActiveFrame
                                                try
                                                {
                                                    image.SelectActiveFrame(objDimension, curFrame);
                                                }
                                                catch //(Exception ex)
                                                {
                                                    try
                                                    {
                                                        image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                    }
                                                    catch //(Exception exSplitTiffImage)
                                                    {
                                                    }


                                                }
                                            }

                                            else
                                            {
                                                try
                                                {
                                                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                }
                                                catch //(Exception ex)
                                                {
                                                }
                                            }
                                            // image.SelectActiveFrame(objDimension, curFrame);
                                            using (EncoderParameters ep = new EncoderParameters(1))
                                            {
                                                if (ep != null)
                                                {
                                                    ImageCodecInfo info = GetEncoderInfo("image/tiff");


                                                    if (image.HorizontalResolution <= 0 && image.VerticalResolution <= 0)
                                                    {

                                                        Bitmap btImage = null;
                                                        // Image tempImage = null;
                                                        btImage = (Bitmap)image.Clone();
                                                        btImage.SetResolution(96, 96);
                                                        //tempImage = (Image)btImage;

                                                        int _curPgNo = i + 1;
                                                        string _strFileExt = _curPgNo.ToString("#00");
                                                        string _strNewFileName = fileStartString.Replace(_ImageFileName.Replace(".TIF", ""), "Image - " + nRowCount.ToString("000#") + _strFileExt);

                                                        string fileName = string.Format("{0}.TIF", _strNewFileName);
                                                        using (ep.Param[0] = new EncoderParameter(enc, Convert.ToInt64(EncoderValue.CompressionLZW.GetHashCode())))
                                                        {
                                                            if (ep.Param != null)
                                                            {
                                                                if (ep.Param.Length >= 0)
                                                                {
                                                                    btImage.Save(fileName, info, ep);
                                                                    splitedFileNames.Add(fileName);

                                                                    if (btImage != null)
                                                                    {
                                                                        btImage.Dispose();
                                                                        btImage = null;
                                                                    }

                                                                }
                                                            }
                                                            else
                                                            {

                                                                if (btImage != null)
                                                                {
                                                                    btImage.Dispose();
                                                                    btImage = null;
                                                                }
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        using (ep.Param[0] = new EncoderParameter(enc, (long)format))
                                                        {
                                                            if (ep.Param != null)
                                                            {
                                                                if (ep.Param.Length >= 0)
                                                                {
                                                                    int _curPgNo = i + 1;
                                                                    string _strFileExt = _curPgNo.ToString("#00");

                                                                    string _strNewFileName = fileStartString.Replace(_ImageFileName.Replace(".TIF", ""), "Image - " + nRowCount.ToString("000#") + _strFileExt);

                                                                    string fileName = string.Format("{0}.TIF", _strNewFileName);
                                                                    try
                                                                    {
                                                                        image.Save(fileName, info, ep);
                                                                        splitedFileNames.Add(fileName);
                                                                    }
                                                                    catch //(Exception ex)
                                                                    {

                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }

                                            curFrame++;

                                        }
                                    }
                                }//if(oGuid)

                            }//for(ij)
                        }
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);


                    return null;

                }
                finally
                {
                    if (image != null)
                    {
                        image.Dispose();
                        image = null;
                    }
                }

                return splitedFileNames;
            }

            #endregion "Dhruv 2010 -> SplitTiffImage"




            #region "Dhruv 2010 -> RotateTiffPages"

            #endregion "Not in Used :: rotate Page"


            #region "Dhruv 2010 -> RotateSinglePageTiff"




            #region "Not in Used :: Rotate Single Page Tiff"

            #endregion "Not in Used :: Rotate Single Page Tiff"


            #endregion "Dhruv 2010 -> RotateSinglePageTiff"


            #region "Dhruv 2010 -> RotateTiffPages"



            #region "Not in Used :: Rotate Tiff Page"

            #endregion "Not in Used :: Rotate Tiff Page"
            #endregion "Dhruv 2010 -> RotateTiffPages"




            #region "Dhruv 2010 -> IsAcknowledged"

            public string SplitTiffImageWithPageRotation(string outPutDirectory, EncoderValue format, ArrayList PageIndexesToRotate, RotateFlipType RFType)
            {
                string fileStartString = outPutDirectory + "\\" + GetFileNameStartString(_ImageFileName);
                ArrayList splitedFileNames = new ArrayList();
                try
                {
                    if (image != null)
                    {
                        Guid[] objGuids = image.FrameDimensionsList;
                        if (objGuids.Length >= 0)
                        {
                            for (int ij = 0; ij < objGuids.Length; ij++)
                            {
                                Guid objGuid = objGuids[0];
                                FrameDimension objDimension = new FrameDimension(objGuid);
                                if (objDimension != null)
                                {
                                    //Saves every frame as a separate file.
                                    System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.Compression;
                                    int curFrame = 0;
                                    for (int i = 0; i < _PageNumber; i++)
                                    {
                                        if (objDimension != null)
                                        {
                                            // Select the current TIFF page using SelectActiveFrame
                                            try
                                            {
                                                image.SelectActiveFrame(objDimension, curFrame);
                                            }
                                            catch //(Exception ex)
                                            {
                                                try
                                                {
                                                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                                }
                                                catch //(Exception exSplitTiffImageWithPageRotation)
                                                {

                                                }

                                            }
                                        }

                                        else
                                        {
                                            try
                                            {
                                                image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, ij);
                                            }
                                            catch //(Exception ex)
                                            {

                                            }
                                        }
                                        // image.SelectActiveFrame(objDimension, curFrame);


                                        using (EncoderParameters ep = new EncoderParameters(1))
                                        {
                                            if (ep != null)
                                            {
                                                using (ep.Param[0] = new EncoderParameter(enc, (long)format))
                                                {
                                                    if (ep.Param != null)
                                                    {
                                                        if (ep.Param.Length >= 0)
                                                        {
                                                            ImageCodecInfo info = GetEncoderInfo("image/tiff");

                                                            //Save the master bitmap
                                                            string fileName = string.Format("{0}{1}.TIF", fileStartString, i.ToString());
                                                            try
                                                            {
                                                                image.Save(fileName, info, ep);
                                                                splitedFileNames.Add(fileName);


                                                                curFrame++;
                                                            }
                                                            catch //(Exception ex)
                                                            {

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (splitedFileNames.Count > 0)
                                    {
                                        JoinTiffImages(splitedFileNames, _ImageFileName, EncoderValue.CompressionNone, PageIndexesToRotate, RFType);
                                    }
                                }
                            }//if(oGuid)
                        }//for(ij)
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);

                    throw;
                }


                return _ImageFileName;
            }

            #region "Dhruv 2010 -> IsAcknowledged"



            public void JoinTiffImages(ArrayList imageFiles, string outFile, EncoderValue compressEncoder, ArrayList RotationPages, RotateFlipType RTType)
            {
                try
                {
                    //If only one page in the collection, copy it directly to the target file.
                    if (imageFiles.Count == 1)
                    {
                        File.Copy(Convert.ToString(imageFiles[0]), outFile, true);
                        return;
                    }

                    //use the save encoder
                    System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;

                    using (EncoderParameters ep = new EncoderParameters(2))
                    {
                        if (ep != null)
                        {
                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame))
                            {
                                if (ep.Param != null)
                                {
                                    if (ep.Param.Length >= 0)
                                    {
                                        using (ep.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)compressEncoder))
                                        {
                                            if (ep.Param != null)
                                            {
                                                if (ep.Param.Length >= 1)
                                                {
                                                    Bitmap pages = null;
                                                    int frame = 0;
                                                    ImageCodecInfo info = GetEncoderInfo("image/tiff");
                                                    bool Rotationflag = false;

                                                    foreach (string strImageFile in imageFiles)
                                                    {

                                                        for (int f = 0; f < RotationPages.Count; f++)
                                                        {
                                                            if (frame == Convert.ToInt32(RotationPages[f]))
                                                            {
                                                                Rotationflag = true;
                                                                break;
                                                            }
                                                        }

                                                        if (frame == 0)
                                                        {
                                                            pages = (Bitmap)Image.FromFile(strImageFile);
                                                            if (pages != null)
                                                            {
                                                                if (Rotationflag)
                                                                {
                                                                    pages.RotateFlip(RTType);
                                                                }
                                                                //save the first frame
                                                                outFile = gloSettings.FolderSettings.AppTempFolderPath + "SpiltTiff\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + "FinalTif.tif";
                                                                pages.Save(outFile, info, ep);
                                                            }

                                                        }
                                                        else
                                                        {
                                                            //save the intermediate frames
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        if (Rotationflag)
                                                                        {
                                                                            Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                                                                            if (bm != null)
                                                                            {

                                                                                bm.RotateFlip(RTType);
                                                                            }
                                                                            pages.SaveAdd(bm, ep);
                                                                            if (bm != null)
                                                                            {
                                                                                bm.Dispose();
                                                                                bm = null;
                                                                            }
                                                                        }


                                                                    }
                                                                }
                                                            }

                                                        }


                                                        if (frame == imageFiles.Count - 1)
                                                        {
                                                            //flush and close.
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        pages.SaveAdd(ep);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        frame++;
                                                        Rotationflag = false;
                                                    }
                                                }
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

                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    throw;
                }

                return;
            }
            #endregion

            #endregion " My Region "


            #region "Dhruv 2010 -> JoinTiffImages"


            public void JoinTiffImages(string[] imageFiles, string outFile, EncoderValue compressEncoder)
            {
                try
                {
                    //If only one page in the collection, copy it directly to the target file.
                    if (imageFiles.Length == 1)
                    {
                        File.Copy(imageFiles[0], outFile, true);
                        return;
                    }

                    //use the save encoder
                    System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;

                    using (EncoderParameters ep = new EncoderParameters(2))
                    {
                        if (ep != null)
                        {
                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame))
                            {
                                if (ep.Param != null)
                                {
                                    if (ep.Param.Length >= 0)
                                    {
                                        using (ep.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)compressEncoder))
                                        {


                                            if (ep.Param != null)
                                            {
                                                if (ep.Param.Length >= 1)
                                                {
                                                    Bitmap pages = null;
                                                    int frame = 0;
                                                    ImageCodecInfo info = GetEncoderInfo("image/tiff");


                                                    foreach (string strImageFile in imageFiles)
                                                    {
                                                        if (frame == 0)
                                                        {
                                                            pages = (Bitmap)Image.FromFile(strImageFile);
                                                            if (pages != null)
                                                            {
                                                                //save the first frame
                                                                pages.Save(outFile, info, ep);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //save the intermediate frames
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                                                                        if (pages != null)
                                                                        {
                                                                            if (bm != null)
                                                                            {
                                                                                pages.SaveAdd(bm, ep);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (frame == imageFiles.Length - 1)
                                                        {
                                                            //flush and close.
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush))
                                                            {
                                                                if (pages != null)
                                                                {
                                                                    if (ep.Param != null)
                                                                    {
                                                                        if (ep.Param.Length >= 0)
                                                                        {
                                                                            pages.SaveAdd(ep);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        frame++;
                                                    }
                                                }
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
                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                }

                return;
            }




            #endregion "Dhruv 2010 -> JoinTiffImages"

            #region "Dhruv 2010 -> JoinTiffImages"


            public bool JoinTiffImages(ArrayList imageFiles, string outFile, EncoderValue compressEncoder)
            {
                Bitmap pages = null;
                try
                {
                    //If only one page in the collection, copy it directly to the target file.
                    if (imageFiles.Count == 1)
                    {
                        File.Copy(Convert.ToString(imageFiles[0]), outFile, true);
                        return true;
                    }

                    //use the save encoder
                    System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;

                    using (EncoderParameters ep = new EncoderParameters(2))
                    {
                        if (ep != null)
                        {
                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame))
                            {
                                if (ep.Param != null)
                                {
                                    if (ep.Param.Length >= 0)
                                    {
                                        using (ep.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)compressEncoder))
                                        {
                                            if (ep.Param != null)
                                            {
                                                if (ep.Param.Length >= 1)
                                                {
                                                    int frame = 0;
                                                    ImageCodecInfo info = GetEncoderInfo("image/tiff");

                                                    foreach (string strImageFile in imageFiles)
                                                    {
                                                        if (frame == 0)
                                                        {
                                                            pages = (Bitmap)Image.FromFile(strImageFile);
                                                            if (pages != null)
                                                            {
                                                                pages.Save(outFile, info, ep);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //save the intermediate frames
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                                                                        if (bm != null)
                                                                        {
                                                                            pages.SaveAdd(bm, ep);
                                                                            if (bm != null)
                                                                            {
                                                                                bm.Dispose();
                                                                                bm = null;
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (frame == imageFiles.Count - 1)
                                                        {
                                                            //flush and close.
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        pages.SaveAdd(ep);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        frame++;
                                                    }
                                                }
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

                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    return false;
                }
                finally
                {
                    if (pages != null)
                    {
                        pages.Dispose();
                        pages = null;
                    }
                }
                return true;
            }

            #endregion "Dhruv 2010 -> JoinTiffImages"



            #region "Dhruv 2010 -> JoinTiffImages_New"


            public bool JoinTiffImages_New(ArrayList imageFiles, string outFile, EncoderValue compressEncoder)
            {
                Bitmap pages = null;
                try
                {
                    //use the save encoder
                    System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;

                    using (EncoderParameters ep = new EncoderParameters(2))
                    {
                        if (ep != null)
                        {
                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame))
                            {
                                if (ep.Param != null)
                                {
                                    if (ep.Param.Length >= 0)
                                    {
                                        using (ep.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)compressEncoder))
                                        {
                                            if (ep.Param != null)
                                            {
                                                if (ep.Param.Length >= 1)
                                                {
                                                    int frame = 0;
                                                    ImageCodecInfo info = GetEncoderInfo("image/tiff");

                                                    foreach (string strImageFile in imageFiles)
                                                    {
                                                        if (frame == 0)
                                                        {
                                                            pages = (Bitmap)Image.FromFile(strImageFile);
                                                            if (pages != null)
                                                            {
                                                                pages.Save(outFile, info, ep);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //save the intermediate frames
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage))
                                                            {
                                                                if (pages != null)
                                                                {
                                                                    if (ep.Param != null)
                                                                    {
                                                                        if (ep.Param.Length >= 0)
                                                                        {
                                                                            Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                                                                            if (bm != null)
                                                                            {
                                                                                pages.SaveAdd(bm, ep);
                                                                                if (bm != null)
                                                                                {
                                                                                    bm.Dispose();
                                                                                    bm = null;
                                                                                }
                                                                            }


                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (frame == imageFiles.Count - 1)
                                                        {
                                                            //flush and close.
                                                            using (ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush))
                                                            {
                                                                if (ep.Param != null)
                                                                {
                                                                    if (ep.Param.Length >= 0)
                                                                    {
                                                                        pages.SaveAdd(ep);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        frame++;
                                                    }
                                                }
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

                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    return false;
                }
                finally
                {
                    if (pages != null)
                    {
                        pages.Dispose();
                        pages = null;
                    }
                }
                return true;
            }
            #endregion "Dhruv 2010 -> JoinTiffImages_New"

            #region "Dhruv 2010 -> RemoveAPage"


            public bool RemoveAPage(int pageNumber, EncoderValue compressEncoder, string strFileName)
            {
                try
                {
                    //Split the image files to single pages.
                    ArrayList arrSplited = SplitTiffImage(this._TempWorkingDir, compressEncoder);

                    //Remove the specific page from the collection
                    string strPageRemove = string.Format("{0}\\{1}{2}.TIF", _TempWorkingDir, GetFileNameStartString(this._ImageFileName), pageNumber);
                    arrSplited.Remove(strPageRemove);
                    string _newFileName = _TempWorkingDir + "\\" + GetFileNameStartString(this._ImageFileName);
                    if (JoinTiffImages(arrSplited, _newFileName, compressEncoder))
                    {
                        File.Copy(_newFileName, strFileName, true);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }

            #endregion "Dhruv 2010 -> RemoveAPage"


            #region "Dhruv 2010 -> RemovePage"


            public bool RemovePage(int pageNumber, EncoderValue compressEncoder, string strFileName)
            {

                bool _Result = false;
                try
                {

                    if (File.Exists(strFileName))
                    {

                        this._ImageFileName = strFileName;
                        image = Image.FromFile(_ImageFileName);
                        if (image != null)
                        {
                            GetPageNumber();

                            //Split the image files to single pages.
                            ArrayList arrSplited = SplitTiffImage(this._TempWorkingDir, compressEncoder, _ImageFileName);

                            if (arrSplited != null)
                            {
                                if (arrSplited.Count >= 0)
                                {
                                    //Remove the specific page from the collection
                                    string strPageRemove = string.Format("{0}\\{1}{2}.TIF", _TempWorkingDir, GetFileNameStartString(this._ImageFileName), pageNumber);
                                    arrSplited.Remove(strPageRemove);
                                    string _newFileName = _TempWorkingDir + "\\" + GetFileNameStartString(this._ImageFileName);
                                    if (JoinTiffImages(arrSplited, _newFileName, compressEncoder))
                                    {
                                        File.Copy(_newFileName, strFileName, true);
                                        _Result = true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {


                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);

                    MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (image != null)
                    {
                        image.Dispose();
                        image = null;
                    }
                }

                return _Result;
            }


            #endregion "Dhruv 2010 -> RemovePage"


            #region "Dhruv 2010 -> RemoveMultiplePages"


            public bool RemoveMultiplePages(ArrayList pageNumber, EncoderValue compressEncoder, string strFileName)
            {
                try
                {
                    //Split the image files to single pages.
                    ArrayList arrSplited = SplitTiffImage(this._TempWorkingDir, compressEncoder);

                    if (pageNumber != null)
                    {
                        if (pageNumber.Count >= 0)
                        {
                            for (int i = 0; i < pageNumber.Count; i++)
                            {
                                //Remove the specific page from the collection
                                string strPageRemove = string.Format("{0}\\{1}{2}.TIF", _TempWorkingDir, GetFileNameStartString(this._ImageFileName), (Convert.ToInt32(pageNumber[i]) - 1));
                                arrSplited.Remove(strPageRemove);
                            }

                        }

                        string _newFileName = _TempWorkingDir + "\\" + GetFileNameStartString(this._ImageFileName);
                        if (JoinTiffImages(arrSplited, _newFileName, compressEncoder))
                        {
                            File.Copy(_newFileName, strFileName, true);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }

            #endregion "Dhruv 2010 -> RemoveMultiplePages"


            #region "Dhruv 2010 -> RemovePages"

            public bool RemovePages(ArrayList pageNumber, EncoderValue compressEncoder, string strFileName)
            {

                bool _Result = false;

                try
                {
                    if (File.Exists(strFileName))
                    {
                        this._ImageFileName = strFileName;
                        image = Image.FromFile(_ImageFileName);
                        if (image != null)
                        {
                            GetPageNumber();

                            //Split the image files to single pages.
                            ArrayList arrSplited = SplitTiffImage(this._TempWorkingDir, compressEncoder);

                            if (pageNumber != null)
                            {
                                if (pageNumber.Count >= 0)
                                {
                                    for (int i = 0; i < pageNumber.Count; i++)
                                    {
                                        //Remove the specific page from the collection
                                        string strPageRemove = string.Format("{0}\\{1}{2}.TIF", _TempWorkingDir, GetFileNameStartString(this._ImageFileName), (Convert.ToInt32(pageNumber[i]) - 1));
                                        arrSplited.Remove(strPageRemove);
                                    }

                                }

                                string _newFileName = _TempWorkingDir + "\\" + GetFileNameStartString(this._ImageFileName);
                                if (JoinTiffImages(arrSplited, _newFileName, compressEncoder))
                                {
                                    File.Copy(_newFileName, strFileName, true);
                                    _Result = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();

                    ErrorMessagees(_ErrorMessage);

                    MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Result = false;
                }

                return _Result;
            }

            #endregion "Dhruv 2010 -> RemovePages"


            #region "Dhruv 2010 -> GetEncoderInfo"


            private ImageCodecInfo GetEncoderInfo(string mimeType)
            {
                ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                if (encoders != null)
                {
                    if (encoders.Length >= 0)
                    {
                        for (int j = 0; j < encoders.Length; j++)
                        {
                            if (encoders[j].MimeType == mimeType)
                                return encoders[j];
                        }
                    }
                }

                throw new Exception(mimeType + " mime type not found in ImageCodecInfo");
            }

            #endregion "Dhruv 2010 -> GetEncoderInfo"


            #region "Dhruv 2010 -> GetSpecificPage"



            #region "Not in Used :: GetSpecificPage"

            #endregion "Not in Used :: GetSpecificPage"

            #endregion "Dhruv 2010 -> GetSpecificPage"


            #region "Dhruv 2010 -> GetSpecificPage"



            #region "Not in Used :: GetSpecificPage"

            #endregion "Not in Used :: GetSpecificPage"
            #endregion "Dhruv 2010 -> GetSpecificPage"


            #region "Dhruv 2010 -> ConvertTiffFormat"


            public void ConvertTiffFormat(string strNewImageFileName, EncoderValue compressEncoder)
            {

                ArrayList arrSplited = SplitTiffImage(this._TempWorkingDir, compressEncoder);
                if (arrSplited.Count >= 0)
                {
                    JoinTiffImages(arrSplited, strNewImageFileName, compressEncoder);
                }

                return;
            }

            #endregion "Dhruv 2010 -> ConvertTiffFormat"

            private void ErrorMessagees(string _ErrorMessage)
            {
                #region " Make Log Entry "
                try
                {
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorHere = ex.ToString();
                    MessageBox.Show("Unable to update Log with " + _ErrorMessage, _ErrorHere);
                }

                //End Code add
                #endregion " Make Log Entry "

            }


            /// <summary>
            /// Image file to operate
            /// </summary>
            /// 
            public string ImageFileName
            {
                get
                {
                    return _ImageFileName;
                }
                set
                {
                    _ImageFileName = value;
                }
            }

            /// <summary>
            /// Buffering directory
            /// </summary>
            public string TempWorkingDir
            {
                get
                {
                    return _TempWorkingDir;
                }
                set
                {
                    _TempWorkingDir = value;
                }
            }

            /// <summary>
            /// Image page number
            /// </summary>
            public int PageNumber
            {
                get
                {
                    return _PageNumber;
                }
            }


            #region IDisposable Members

            public void Dispose()
            {

                System.GC.SuppressFinalize(this);
            }

            #endregion
        }
    }
}
