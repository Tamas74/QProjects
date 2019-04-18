using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using pdftron;
using pdftron.Common;
using pdftron.Filters;
using pdftron.SDF;
using pdftron.PDF;


namespace gloCMSEDI
{
    static class clsClinicalChartPrinting
    {
        private static string ExamNewDocumentName
        {
            get
            {
                string _Path = gloSettings.FolderSettings.AppTempFolderPath;
                string _NewDocumentName = "";
                string _Extension = ".docx";
                DateTime _dtCurrentDateTime = System.DateTime.Now;

                int i = 0;
                _NewDocumentName = String.Format(_dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt")) + " " + _dtCurrentDateTime.Millisecond + _Extension;
                while (File.Exists(_Path + "\\" + _NewDocumentName) == true)
                {
                    i = i + 1;
                    _NewDocumentName = String.Format(_dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt")) + " " + _dtCurrentDateTime.Millisecond + "-" + i + _Extension;
                }
                return _Path + "\\" + _NewDocumentName;
            }
        }

        private static bool GetXWidthYHeight(float HorRes, float VerRes, float pxHeight, float pxWidth, out float XWidth, out float YHeight)
        {
            bool _result = false;
            float _XWidth = 0;
            float _YHeight = 0;
            if (HorRes == 0)
            {
                HorRes = 72;
            }
            if (VerRes == 0)
            {
                VerRes = 72;
            }
            try
            {
                //Height & Width in Inches
                float _XInch = pxWidth / HorRes;
                float _YInch = pxHeight / VerRes;

                ////Calculate Return Height and Width in Pixcel
                if (_XInch < _YInch)
                {
                    _XWidth = _XInch * 72;
                    _YHeight = _YInch * 72 * VerRes / HorRes;
                }
                else
                {
                    _YHeight = _YInch * 72;
                    _XWidth = _XInch * 72 * HorRes / VerRes;
                }
                _result = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            XWidth = _XWidth;
            YHeight = _YHeight;
            return _result;
        }

        public static string ConvertTiffToPDF(string TiffFilePath, string sFilePath, bool isPrintData, bool bCompressOutputPDF = false, bool applyLossyCompression =false)
        {
            string strTempExamfilepath = string.Empty;
            try
            {
                strTempExamfilepath = sFilePath; //ExamNewDocumentName.ToString().Replace(".docx", ".pdf");

                if (isPrintData == true)
                {
                    using (pdftron.PDF.PDFDoc pdfDocClaim = new pdftron.PDF.PDFDoc())
                    {
                        using (pdftron.PDF.ElementBuilder bld = new pdftron.PDF.ElementBuilder())
                        {
                            using (pdftron.PDF.ElementWriter writer = new pdftron.PDF.ElementWriter())
                            {
                                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(TiffFilePath, true);
                                float XWidth = 0;
                                float YHeight = 0;
                                GetXWidthYHeight(bmp.HorizontalResolution, bmp.VerticalResolution, bmp.Height, bmp.Width, out XWidth, out YHeight);
                                pdftron.PDF.Rect oRect = new pdftron.PDF.Rect();
                                oRect.x1 = XWidth;
                                oRect.x2 = 0;
                                oRect.y1 = YHeight;
                                oRect.y2 = 0;
                                pdftron.PDF.Page page = pdfDocClaim.PageCreate(oRect);
                                // Start a new page 
                                writer.Begin(page);
                                // Begin writing to this page
                                //Dim img As pdftron.PDF.Image = pdftron.PDF.Image.Create(pdfDocClaim.GetSDFDoc(), bmp)
                                pdftron.PDF.Image img = pdftron.PDF.Image.Create(pdfDocClaim, bmp);
                                pdftron.PDF.Element element = bld.CreateImage(img, new pdftron.Common.Matrix2D(XWidth, 0, 0, YHeight, 0, 0));
                                writer.WritePlacedElement(element);
                                writer.End();
                                // Finish writing to the page
                                pdfDocClaim.PagePushBack(page);
                                pdfDocClaim.Save(strTempExamfilepath, pdftron.SDF.SDFDoc.SaveOptions.e_incremental);
                                bmp.Dispose();
                                oRect.Dispose();
                                img.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    pdftron.PDF.PDFDoc pdfDoc = new PDFDoc();
                    pdfDoc.InitSecurityHandler();
                    pdftron.PDF.Convert.ToPdf(pdfDoc, TiffFilePath);
                    pdfDoc.Save(strTempExamfilepath, pdftron.SDF.SDFDoc.SaveOptions.e_incremental);
                    pdfDoc.Close();
                    pdfDoc.Dispose();
                    pdfDoc = null;
                }

                //Compress PDF
                if (bCompressOutputPDF == true)
                {
                    string _compressedPDFFilePath = string.Empty;

                    try
                    {
                        if (File.Exists(strTempExamfilepath) == true)
                        {
                            _compressedPDFFilePath = strTempExamfilepath.Replace(".pdf", "_C.pdf");
                            strTempExamfilepath = CompressPDF(strTempExamfilepath, _compressedPDFFilePath, applyLossyCompression);
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            return strTempExamfilepath;
        }

        private static string CompressPDF(string inputPDFFile, string outputPDFFile, bool applyLossyCompression)
        {
            Obj _JBIG2_hint = null;
            PDFDoc doc = null;
            pdftron.SDF.ObjSet hint_set = new ObjSet();
            byte[] _img_buf = null;
            string copyOfInputPDFFile = string.Empty;

            try
            {
                if (File.Exists(inputPDFFile) == true)
                {
                    copyOfInputPDFFile = inputPDFFile.Replace(".pdf", "_COPY.pdf");
                    File.Copy(inputPDFFile, copyOfInputPDFFile, false);

                    _JBIG2_hint = hint_set.CreateArray();
                    _JBIG2_hint.PushBackName("JBIG2");
                    _JBIG2_hint.PushBackName("Lossless");

                    doc = new PDFDoc(copyOfInputPDFFile);
                    doc.InitSecurityHandler();

                    if (applyLossyCompression)
                    {
                        _JBIG2_hint.GetAt(1).SetName("Lossy");
                    }
                    else
                    {
                        _JBIG2_hint.GetAt(1).SetName("Lossless");
                    }

                    SDFDoc cos_doc = doc.GetSDFDoc();
                    int num_objs = cos_doc.XRefSize();

                    for (int i = 1; i < num_objs; ++i)
                    {
                        Obj obj = cos_doc.GetObj(i);

                        if (obj != null && !obj.IsFree() && obj.IsStream())
                        {
                            // Process only images
                            DictIterator itr = obj.Find("Subtype");
                            if (!itr.HasNext() || itr.Value().GetName() != "Image")
                                continue;

                            pdftron.PDF.Image input_image = new pdftron.PDF.Image(obj);
                            pdftron.PDF.Image new_image = null;

                            // Process only gray-scale images
                            if (input_image.GetComponentNum() != 1)
                                continue;

                            int bpc = input_image.GetBitsPerComponent();
                            if (bpc == 1) // Recompress 1 BPC images
                            {
                                // Skip images that are already compressed using JBIG2
                                itr = obj.Find("Filter");
                                if (itr.HasNext() && itr.Value().IsName() &&
                                    itr.Value().GetName() == "JBIG2Decode")
                                    continue;

                                FilterReader reader = new FilterReader(obj.GetDecodedStream());
                                new_image = pdftron.PDF.Image.Create(
                                    cos_doc,
                                    reader,
                                    input_image.GetImageWidth(),
                                    input_image.GetImageHeight(),
                                    1,
                                    ColorSpace.CreateDeviceGray(),
                                    _JBIG2_hint  // A hint to image encoder to use JBIG2 compression
                                    );
                            }
                            else if (bpc == 8)
                            {
                                // Some black and white images are stored as 8 bits per pixel
                                // grayscale images. In this case convert the image to 1 BPC 
                                // image and then compress using JBIG2.
                                int width = input_image.GetImageWidth();
                                int height = input_image.GetImageHeight();
                                int bytes_per_row = (width + 7) / 8;
                                int img_buf_size = width * height;

                                if (_img_buf == null || _img_buf.Length < img_buf_size)
                                {
                                    _img_buf = new byte[img_buf_size * 2];
                                }

                                FilterReader reader = new FilterReader(obj.GetDecodedStream());
                                byte acc;
                                int in_pos = 0;
                                int out_pos = 0;
                                int pad = width % 8;

                                reader.Read(_img_buf);

                                for (int y = 0; y < height; ++y)
                                {
                                    acc = 0;
                                    for (int x = 0; x < width; ++x)
                                    {
                                        acc <<= 1;
                                        if (_img_buf[in_pos++] > 128)
                                        {
                                            acc |= 0x01;
                                        }

                                        if (x != 0 && (x % 8) == 0)
                                        {
                                            _img_buf[out_pos] = acc;
                                            ++out_pos;
                                        }
                                    }

                                    if (pad != 0)
                                    {
                                        acc <<= (8 - pad);
                                    }

                                    _img_buf[out_pos] = acc;
                                    ++out_pos;
                                }

                                new_image = pdftron.PDF.Image.Create(
                                    cos_doc,
                                    _img_buf,
                                    width, height, 1, ColorSpace.CreateDeviceGray(),
                                    _JBIG2_hint  // A hint to image encoder to use JBIG2 compression
                                    );
                            }
                            else
                            {
                                continue;
                            }

                            Obj new_img_obj = new_image.GetSDFObj();

                            // Copy any important entries from the image dictionary
                            itr = obj.Find("Decode");
                            if (itr.HasNext()) new_img_obj.Put("Decode", itr.Value());

                            itr = obj.Find("ImageMask");
                            if (itr.HasNext()) new_img_obj.Put("ImageMask", itr.Value());

                            itr = obj.Find("Mask");
                            if (itr.HasNext()) new_img_obj.Put("Mask", itr.Value());

                            cos_doc.Swap(i, new_image.GetSDFObj().GetObjNum());
                        }
                    }

                    doc.Save(outputPDFFile, SDFDoc.SaveOptions.e_remove_unused);

                    if (File.Exists(inputPDFFile) == true)
                    { File.Delete(inputPDFFile); }

                } //End Of : if (File.Exists(inputPDFFile) == true)
            }
            catch (pdftron.Common.PDFNetException pdfEx)
            {
                outputPDFFile = inputPDFFile; //if exception occured during comprssion then return the original file path
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while compressing pdf at method: CompressPDF in gloCMSEDI.clsClinicalChartPrinting class.", false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(pdfEx, false);
            }
            catch (Exception ex)
            {
                outputPDFFile = inputPDFFile; //if exception occured during comprssion then return the original file path
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while compressing pdf at method: CompressPDF in gloCMSEDI.clsClinicalChartPrinting class.", false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (doc != null) { doc.Close(); doc = null; }
                if (_JBIG2_hint != null) { _JBIG2_hint.Dispose(); _JBIG2_hint = null; }
                if (hint_set != null) { hint_set.Dispose(); hint_set = null; }
                if (_img_buf != null) { _img_buf = null; }

                if (File.Exists(copyOfInputPDFFile) == true)
                { File.Delete(copyOfInputPDFFile); }
            }

            return outputPDFFile;
        } //Method End
    }

    public class clsPrintDocumentConversion
    {
        private string dbConnectionString = "";
        private string PrinterName = "";
        private string BillingType = "";

        private float _PaperWidth = 8.5f;
        public float PaperWidth
        {
            get { return _PaperWidth; }
            set { _PaperWidth = value; }
        }

        private float _PaperHeight = 11f;
        public float PaperHeight
        {
            get { return _PaperHeight; }
            set { _PaperHeight = value; }
        }

        private float _DpiX = 600f;
        public float DpiX
        {
            get { return _DpiX; }
            set { _DpiX = value; }
        }

        private float _DpiY = 600f;
        public float DpiY
        {
            get { return _DpiY; }
            set { _DpiY = value; }
        }

        private int _BoundX = 0;
        public int BoundX
        {
            get { return _BoundX; }
            set { _BoundX = value; }
        }

        private int _BoundY = 0;
        public int BoundY
        {
            get { return _BoundY; }
            set { _BoundY = value; }
        }

        private int _BoundWidth = 850;
        public int BoundWidth
        {
            get { return _BoundWidth; }
            set { _BoundWidth = value; }
        }

        private int _BoundHeight = 1100;
        public int BoundHeight
        {
            get { return _BoundHeight; }
            set { _BoundHeight = value; }
        }


        private int _MarginBoundsX = 100;
        public int MarginBoundsX
        {
            get { return _MarginBoundsX; }
            set { _MarginBoundsX = value; }
        }

        private int _MarginBoundsY = 100;
        public int MarginBoundsY
        {
            get { return _MarginBoundsY; }
            set { _MarginBoundsY = value; }
        }

        private int _MarginBoundsWidth = 650;
        public int MarginBoundsWidth
        {
            get { return _MarginBoundsWidth; }
            set { _MarginBoundsWidth = value; }
        }

        private int _MarginBoundsHeight = 900;
        public int MarginBoundsHeight
        {
            get { return _MarginBoundsHeight; }
            set { _MarginBoundsHeight = value; }
        }



        public clsPrintDocumentConversion(string ConnectionString,string Printer,string FormBillingType)
        {
            dbConnectionString = ConnectionString;
            PrinterName = Printer;
            BillingType = FormBillingType;
            LoadConversionSettings();
        }

        public void LoadConversionSettings()
        {

            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataTable dtConversion = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(dbConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                dtConversion = new DataTable();

                oDB.Connect(false);
                oDBParameters.Add("@PrinterName", PrinterName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@BillingType", BillingType, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GetClaimPrintConversionFactor", oDBParameters, out dtConversion);
                oDB.Disconnect();

                if (dtConversion != null)
                {
                    if (dtConversion.Rows.Count > 0)
                    {

                        try
                        {
                            PaperWidth = float.Parse(System.Convert.ToString(dtConversion.Rows[0]["PaperWidth"]), CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception ex)
                        {
                            PaperWidth = 8.5f;
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }


                        try
                        {
                            PaperHeight = float.Parse(System.Convert.ToString(dtConversion.Rows[0]["PaperHeight"]), CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception ex)
                        {
                            PaperHeight = 11f;
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }

                        try
                        {
                            DpiX = float.Parse(System.Convert.ToString(dtConversion.Rows[0]["DpiX"]), CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception ex)
                        {
                            DpiX = 600f;
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }

                        try
                        {
                            DpiY = float.Parse(System.Convert.ToString(dtConversion.Rows[0]["DpiY"]), CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception ex)
                        {
                            DpiY = 600f;
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }

                        BoundX = System.Convert.ToInt32(dtConversion.Rows[0]["BoundX"]);
                        BoundY = System.Convert.ToInt32(dtConversion.Rows[0]["BoundY"]);
                        BoundHeight = System.Convert.ToInt32(dtConversion.Rows[0]["BoundHeight"]);
                        BoundWidth = System.Convert.ToInt32(dtConversion.Rows[0]["BoundWidth"]);

                        MarginBoundsX = System.Convert.ToInt32(dtConversion.Rows[0]["MarginBoundsX"]);
                        MarginBoundsY = System.Convert.ToInt32(dtConversion.Rows[0]["MarginBoundsY"]);
                        MarginBoundsHeight = System.Convert.ToInt32(dtConversion.Rows[0]["MarginBoundsHeight"]);
                        MarginBoundsWidth = System.Convert.ToInt32(dtConversion.Rows[0]["MarginBoundsWidth"]);
                    }
                }
            }
            catch (Exception exLog)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exLog, false);
            }
            finally
            {
                if (dtConversion != null) { dtConversion.Dispose(); dtConversion = null; }
                if (oDBParameters != null) { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

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

                //string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.ToString();
                //MessageBox.Show(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
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
                //string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.ToString();
                //MessageBox.Show(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                ex = null;
            }
        }
    }
}
