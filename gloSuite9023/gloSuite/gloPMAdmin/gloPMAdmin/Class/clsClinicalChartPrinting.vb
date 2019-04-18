Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports gloClinicalQueueGeneral

Imports pdftron
Imports pdftron.Common
Imports pdftron.Filters
Imports pdftron.SDF
Imports pdftron.PDF

Public Class clsClinicalChartPrinting
    Private Sub New()
    End Sub
    Private Shared ReadOnly Property ExamNewDocumentName() As String
        Get
            Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            Dim _NewDocumentName As String = ""
            Dim _Extension As String = ".docx"
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            Dim i As Integer = 0
            _NewDocumentName = [String].Format(_dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt")) + " " + _dtCurrentDateTime.Millisecond + _Extension
            While File.Exists(_Path + "\\" + _NewDocumentName) = True
                i = i + 1
                _NewDocumentName = [String].Format(_dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt")) + " " + _dtCurrentDateTime.Millisecond + "-" + i + _Extension
            End While
            Return _Path + "\\" + _NewDocumentName
        End Get
    End Property

    Private Shared Function GetXWidthYHeight(HorRes As Single, VerRes As Single, pxHeight As Single, pxWidth As Single, ByRef XWidth As Single, ByRef YHeight As Single) As Boolean
        Dim _result As Boolean = False
        Dim _XWidth As Single = 0
        Dim _YHeight As Single = 0
        If HorRes = 0 Then
            HorRes = 72
        End If
        If VerRes = 0 Then
            VerRes = 72
        End If
        Try
            'Height & Width in Inches
            Dim _XInch As Single = pxWidth / HorRes
            Dim _YInch As Single = pxHeight / VerRes

            ''/Calculate Return Height and Width in Pixcel
            If _XInch < _YInch Then
                _XWidth = _XInch * 72
                _YHeight = _YInch * 72 * VerRes / HorRes
            Else
                _YHeight = _YInch * 72
                _XWidth = _XInch * 72 * HorRes / VerRes
            End If

            _result = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
        XWidth = _XWidth
        YHeight = _YHeight
        Return _result
    End Function

    Public Shared Function ConvertTiffToPDF(TiffFilePath As String, sFilePath As String, isPrintData As Boolean, Optional bCompressOutputPDF As Boolean = False, Optional applyLossyCompression As Boolean = False) As String
        Dim strTempExamfilepath As String = String.Empty
        Try
            strTempExamfilepath = sFilePath
            'ExamNewDocumentName.ToString().Replace(".docx", ".pdf");
            If isPrintData = True Then
                Using pdfDocClaim As New pdftron.PDF.PDFDoc()
                    Using bld As New pdftron.PDF.ElementBuilder()
                        Using writer As New pdftron.PDF.ElementWriter()
                            Dim bmp As New System.Drawing.Bitmap(TiffFilePath, True)
                            Dim XWidth As Single = 0
                            Dim YHeight As Single = 0
                            GetXWidthYHeight(bmp.HorizontalResolution, bmp.VerticalResolution, bmp.Height, bmp.Width, XWidth, YHeight)
                            Dim oRect As New pdftron.PDF.Rect()
                            oRect.x1 = XWidth
                            oRect.x2 = 0
                            oRect.y1 = YHeight
                            oRect.y2 = 0
                            Dim page As pdftron.PDF.Page = pdfDocClaim.PageCreate(oRect)
                            ' Start a new page 
                            writer.Begin(page)
                            ' Begin writing to this page
                            'Dim img As pdftron.PDF.Image = pdftron.PDF.Image.Create(pdfDocClaim.GetSDFDoc(), bmp)
                            Dim img As pdftron.PDF.Image = pdftron.PDF.Image.Create(pdfDocClaim, bmp)
                            Dim element As pdftron.PDF.Element = bld.CreateImage(img, New pdftron.Common.Matrix2D(XWidth, 0, 0, YHeight, 0, 0))
                            writer.WritePlacedElement(element)
                            writer.[End]()
                            ' Finish writing to the page
                            pdfDocClaim.PagePushBack(page)
                            pdfDocClaim.Save(strTempExamfilepath, pdftron.SDF.SDFDoc.SaveOptions.e_incremental)
                            bmp.Dispose()
                            oRect.Dispose()
                            img.Dispose()
                        End Using
                    End Using
                End Using
            Else
                Dim pdfDoc As pdftron.PDF.PDFDoc = New PDFDoc()
                pdfDoc.InitSecurityHandler()
                pdftron.PDF.Convert.ToPdf(pdfDoc, TiffFilePath)
                pdfDoc.Save(strTempExamfilepath, pdftron.SDF.SDFDoc.SaveOptions.e_incremental)
                pdfDoc.Close()
                pdfDoc.Dispose()
                pdfDoc = Nothing
            End If

            'Compress PDF
            If bCompressOutputPDF = True Then
                Dim _compressedPDFFilePath As String = String.Empty

                Try
                    If File.Exists(strTempExamfilepath) = True Then
                        _compressedPDFFilePath = strTempExamfilepath.Replace(".pdf", "_C.pdf")
                        strTempExamfilepath = CompressPDF(strTempExamfilepath, _compressedPDFFilePath, applyLossyCompression)
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                End Try
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try

        Return strTempExamfilepath
    End Function

    Private Shared Function CompressPDF(inputPDFFile As String, outputPDFFile As String, applyLossyCompression As Boolean) As String
        Dim _JBIG2_hint As Obj = Nothing
        Dim doc As PDFDoc = Nothing
        Dim hint_set As pdftron.SDF.ObjSet = New ObjSet()
        Dim _img_buf As Byte() = Nothing
        Dim copyOfInputPDFFile As String = String.Empty

        Try
            If File.Exists(inputPDFFile) = True Then
                copyOfInputPDFFile = inputPDFFile.Replace(".pdf", "_COPY.pdf")
                File.Copy(inputPDFFile, copyOfInputPDFFile, False)

                _JBIG2_hint = hint_set.CreateArray()
                _JBIG2_hint.PushBackName("JBIG2")
                _JBIG2_hint.PushBackName("Lossless")

                doc = New PDFDoc(copyOfInputPDFFile)
                doc.InitSecurityHandler()

                If applyLossyCompression Then
                    _JBIG2_hint.GetAt(1).SetName("Lossy")
                Else
                    _JBIG2_hint.GetAt(1).SetName("Lossless")
                End If

                Dim cos_doc As SDFDoc = doc.GetSDFDoc()
                Dim num_objs As Integer = cos_doc.XRefSize()

                For i As Integer = 1 To num_objs - 1
                    Dim obj As Obj = cos_doc.GetObj(i)

                    If obj IsNot Nothing AndAlso Not obj.IsFree() AndAlso obj.IsStream() Then
                        ' Process only images
                        Dim itr As DictIterator = obj.Find("Subtype")
                        If Not itr.HasNext() OrElse itr.Value().GetName() <> "Image" Then
                            Continue For
                        End If

                        Dim input_image As New pdftron.PDF.Image(obj)
                        Dim new_image As pdftron.PDF.Image = Nothing

                        ' Process only gray-scale images
                        If input_image.GetComponentNum() <> 1 Then
                            Continue For
                        End If

                        Dim bpc As Integer = input_image.GetBitsPerComponent()
                        If bpc = 1 Then
                            ' Recompress 1 BPC images
                            ' Skip images that are already compressed using JBIG2
                            itr = obj.Find("Filter")
                            If itr.HasNext() AndAlso itr.Value().IsName() AndAlso itr.Value().GetName() = "JBIG2Decode" Then
                                Continue For
                            End If

                            Dim reader As New FilterReader(obj.GetDecodedStream())
                            ' A hint to image encoder to use JBIG2 compression
                            new_image = pdftron.PDF.Image.Create(cos_doc, reader, input_image.GetImageWidth(), input_image.GetImageHeight(), 1, ColorSpace.CreateDeviceGray(), _
                             _JBIG2_hint)
                        ElseIf bpc = 8 Then
                            ' Some black and white images are stored as 8 bits per pixel
                            ' grayscale images. In this case convert the image to 1 BPC 
                            ' image and then compress using JBIG2.
                            Dim width As Integer = input_image.GetImageWidth()
                            Dim height As Integer = input_image.GetImageHeight()
                            Dim bytes_per_row As Integer = (width + 7) / 8
                            Dim img_buf_size As Integer = width * height

                            If _img_buf Is Nothing OrElse _img_buf.Length < img_buf_size Then
                                _img_buf = New Byte(img_buf_size * 2 - 1) {}
                            End If

                            Dim reader As New FilterReader(obj.GetDecodedStream())
                            Dim acc As Byte
                            Dim in_pos As Integer = 0
                            Dim out_pos As Integer = 0
                            Dim pad As Integer = width Mod 8

                            reader.Read(_img_buf)

                            For y As Integer = 0 To height - 1
                                acc = 0
                                For x As Integer = 0 To width - 1
                                    acc <<= 1
                                    If _img_buf(System.Math.Max(System.Threading.Interlocked.Increment(in_pos), in_pos - 1)) > 128 Then
                                        acc = acc Or &H1
                                    End If

                                    If x <> 0 AndAlso (x Mod 8) = 0 Then
                                        _img_buf(out_pos) = acc
                                        out_pos += 1
                                    End If
                                Next

                                If pad <> 0 Then
                                    acc <<= (8 - pad)
                                End If

                                _img_buf(out_pos) = acc
                                out_pos += 1
                            Next

                            ' A hint to image encoder to use JBIG2 compression
                            new_image = pdftron.PDF.Image.Create(cos_doc, _img_buf, width, height, 1, ColorSpace.CreateDeviceGray(), _
                             _JBIG2_hint)
                        Else
                            Continue For
                        End If

                        Dim new_img_obj As Obj = new_image.GetSDFObj()

                        ' Copy any important entries from the image dictionary
                        itr = obj.Find("Decode")
                        If itr.HasNext() Then
                            new_img_obj.Put("Decode", itr.Value())
                        End If

                        itr = obj.Find("ImageMask")
                        If itr.HasNext() Then
                            new_img_obj.Put("ImageMask", itr.Value())
                        End If

                        itr = obj.Find("Mask")
                        If itr.HasNext() Then
                            new_img_obj.Put("Mask", itr.Value())
                        End If

                        cos_doc.Swap(i, new_image.GetSDFObj().GetObjNum())
                    End If
                Next

                doc.Save(outputPDFFile, SDFDoc.SaveOptions.e_remove_unused)

                If File.Exists(inputPDFFile) = True Then
                    File.Delete(inputPDFFile)

                End If
                'End Of : if (File.Exists(inputPDFFile) == true)
            End If
        Catch pdfEx As pdftron.Common.PDFNetException
            outputPDFFile = inputPDFFile
            'if exception occured during comprssion then return the original file path
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while compressing pdf at method: CompressPDF in gloCMSEDI.clsClinicalChartPrinting class.", False)
            gloAuditTrail.gloAuditTrail.ExceptionLog(pdfEx, False)
        Catch ex As Exception
            outputPDFFile = inputPDFFile
            'if exception occured during comprssion then return the original file path
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while compressing pdf at method: CompressPDF in gloCMSEDI.clsClinicalChartPrinting class.", False)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        Finally
            If doc IsNot Nothing Then
                doc.Close()
                doc = Nothing
            End If
            If _JBIG2_hint IsNot Nothing Then
                _JBIG2_hint.Dispose()
                _JBIG2_hint = Nothing
            End If
            If hint_set IsNot Nothing Then
                hint_set.Dispose()
                hint_set = Nothing
            End If
            If _img_buf IsNot Nothing Then
                _img_buf = Nothing
            End If

            If File.Exists(copyOfInputPDFFile) = True Then
                File.Delete(copyOfInputPDFFile)
            End If
        End Try

        Return outputPDFFile
    End Function
    'Method End
End Class

Public Class clsPrintDocumentConversion
    Private dbConnectionString As String = ""
    Private PrinterName As String = ""
    Private BillingType As String = ""

    Private _PaperWidth As Single = 8.5F
    Public Property PaperWidth() As Single
        Get
            Return _PaperWidth
        End Get
        Set(value As Single)
            _PaperWidth = value
        End Set
    End Property

    Private _PaperHeight As Single = 11.0F
    Public Property PaperHeight() As Single
        Get
            Return _PaperHeight
        End Get
        Set(value As Single)
            _PaperHeight = value
        End Set
    End Property

    Private _DpiX As Single = 600.0F
    Public Property DpiX() As Single
        Get
            Return _DpiX
        End Get
        Set(value As Single)
            _DpiX = value
        End Set
    End Property

    Private _DpiY As Single = 600.0F
    Public Property DpiY() As Single
        Get
            Return _DpiY
        End Get
        Set(value As Single)
            _DpiY = value
        End Set
    End Property

    Private _BoundX As Integer = 0
    Public Property BoundX() As Integer
        Get
            Return _BoundX
        End Get
        Set(value As Integer)
            _BoundX = value
        End Set
    End Property

    Private _BoundY As Integer = 0
    Public Property BoundY() As Integer
        Get
            Return _BoundY
        End Get
        Set(value As Integer)
            _BoundY = value
        End Set
    End Property

    Private _BoundWidth As Integer = 850
    Public Property BoundWidth() As Integer
        Get
            Return _BoundWidth
        End Get
        Set(value As Integer)
            _BoundWidth = value
        End Set
    End Property

    Private _BoundHeight As Integer = 1100
    Public Property BoundHeight() As Integer
        Get
            Return _BoundHeight
        End Get
        Set(value As Integer)
            _BoundHeight = value
        End Set
    End Property


    Private _MarginBoundsX As Integer = 100
    Public Property MarginBoundsX() As Integer
        Get
            Return _MarginBoundsX
        End Get
        Set(value As Integer)
            _MarginBoundsX = value
        End Set
    End Property

    Private _MarginBoundsY As Integer = 100
    Public Property MarginBoundsY() As Integer
        Get
            Return _MarginBoundsY
        End Get
        Set(value As Integer)
            _MarginBoundsY = value
        End Set
    End Property

    Private _MarginBoundsWidth As Integer = 650
    Public Property MarginBoundsWidth() As Integer
        Get
            Return _MarginBoundsWidth
        End Get
        Set(value As Integer)
            _MarginBoundsWidth = value
        End Set
    End Property

    Private _MarginBoundsHeight As Integer = 900
    Public Property MarginBoundsHeight() As Integer
        Get
            Return _MarginBoundsHeight
        End Get
        Set(value As Integer)
            _MarginBoundsHeight = value
        End Set
    End Property



    Public Sub New(ConnectionString As String, Printer As String, FormBillingType As String)
        dbConnectionString = ConnectionString
        PrinterName = Printer
        BillingType = FormBillingType
        LoadConversionSettings()
    End Sub

    Public Sub LoadConversionSettings()

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtConversion As DataTable = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(dbConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters()
            dtConversion = New DataTable()

            oDB.Connect(False)
            oDBParameters.Add("@PrinterName", PrinterName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BillingType", BillingType, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Retrive("gsp_GetClaimPrintConversionFactor", oDBParameters, dtConversion)
            oDB.Disconnect()

            If dtConversion IsNot Nothing Then
                If dtConversion.Rows.Count > 0 Then

                    Try
                        PaperWidth = Single.Parse(System.Convert.ToString(dtConversion.Rows(0)("PaperWidth")), CultureInfo.InvariantCulture.NumberFormat)
                    Catch ex As Exception
                        PaperWidth = 8.5F
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try


                    Try
                        PaperHeight = Single.Parse(System.Convert.ToString(dtConversion.Rows(0)("PaperHeight")), CultureInfo.InvariantCulture.NumberFormat)
                    Catch ex As Exception
                        PaperHeight = 11.0F
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try

                    Try
                        DpiX = Single.Parse(System.Convert.ToString(dtConversion.Rows(0)("DpiX")), CultureInfo.InvariantCulture.NumberFormat)
                    Catch ex As Exception
                        DpiX = 600.0F
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try

                    Try
                        DpiY = Single.Parse(System.Convert.ToString(dtConversion.Rows(0)("DpiY")), CultureInfo.InvariantCulture.NumberFormat)
                    Catch ex As Exception
                        DpiY = 600.0F
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try

                    BoundX = System.Convert.ToInt32(dtConversion.Rows(0)("BoundX"))
                    BoundY = System.Convert.ToInt32(dtConversion.Rows(0)("BoundY"))
                    BoundHeight = System.Convert.ToInt32(dtConversion.Rows(0)("BoundHeight"))
                    BoundWidth = System.Convert.ToInt32(dtConversion.Rows(0)("BoundWidth"))

                    MarginBoundsX = System.Convert.ToInt32(dtConversion.Rows(0)("MarginBoundsX"))
                    MarginBoundsY = System.Convert.ToInt32(dtConversion.Rows(0)("MarginBoundsY"))
                    MarginBoundsHeight = System.Convert.ToInt32(dtConversion.Rows(0)("MarginBoundsHeight"))
                    MarginBoundsWidth = System.Convert.ToInt32(dtConversion.Rows(0)("MarginBoundsWidth"))
                End If
            End If
        Catch exLog As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exLog, False)
        Finally
            If dtConversion IsNot Nothing Then
                dtConversion.Dispose()
                dtConversion = Nothing
            End If
            If oDBParameters IsNot Nothing Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub
End Class

Public Class gloClinicalQueueFunctions
    Private Shared _isTSPrintDialogOpen As Boolean = False

    Public Shared Function CopyPrintDoc(outputFile As [String], claimType As [String], PrintType As [String]) As Boolean
        Try
            If outputFile IsNot Nothing Then
                Dim SplitDocList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)()


                Dim physicalDoc As New gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo()
                physicalDoc.PdfFileName = outputFile
                physicalDoc.SrcFileName = outputFile
                physicalDoc.footerInfo = Nothing
                SplitDocList.Add(physicalDoc)




                ''Generate MetaData File
                'Dim PDFWithoutPath As String = PDFFileName.Substring(PDFFileName.LastIndexOf("\") + 1)
                ' Dim strMetaDataFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".xml", "MMddyyyyHHmmssffff")
                Dim strMetaDataFilePath As String = gloGlobal.gloTSPrint.TempPath & "01" & gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") & DateTime.Now.Millisecond & ".xml"
                Dim MetaDataGenerated As Boolean = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, claimType, PrintType)

                ''Copy Files to mapped virtual drive
                If MetaDataGenerated Then
                    gloAuditTrail.gloAuditTrail.PrintLog(strException:="MetaData file generated for Word Printing.", ShowMessageBox:=False)
                    If gloGlobal.gloTSPrint.isMapped() Then
                        'File.Copy(PDFFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                        Dim PDFWithoutPath As [String] = ""
                        Dim First As Boolean = True
                        For fileCntr As Integer = 0 To SplitDocList.Count - 1
                            PDFWithoutPath = SplitDocList(fileCntr).PdfFileName.Substring(SplitDocList(fileCntr).PdfFileName.LastIndexOf("\") + 1)
                            gloGlobal.gloTSPrint.CopyFileToNetworkShare(SplitDocList(fileCntr).PdfFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                            If First Then
                                gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\") + 1))
                                First = False
                            End If
                        Next

                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="PDF and MetaData files copied to virtual drive for Word Printing.", ShowMessageBox:=False)
                    Else
                        If _isTSPrintDialogOpen = False Then
                            _isTSPrintDialogOpen = True
                            Dim s As DialogResult = MessageBox.Show("Unable to find mapped drive. Please check whether gloClinicalQueue Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If s = System.Windows.Forms.DialogResult.Yes Then
                                _isTSPrintDialogOpen = False
                                gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Using RDP printer", ShowMessageBox:=False)
                                Return False
                            Else
                                _isTSPrintDialogOpen = False
                                gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Document Not Printed", ShowMessageBox:=False)
                                Return True
                            End If
                        Else
                            gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found messagebox already active. Document Not Printed", ShowMessageBox:=False)
                            Return True


                        End If
                    End If

                    Return True
                Else
                    gloAuditTrail.gloAuditTrail.PrintLog(strException:="Error in MetaData file generation for Word Printing.", ShowMessageBox:=False)
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        Finally
            gloGlobal.gloTSPrint.SetTestPatient()
        End Try
    End Function

    Private Shared Function GenerateMetaDataFile(strFilePath As String, PhysicalFile As List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo), claimType As [String], PrintType As [String]) As Boolean
        Dim _QueueWriter As New gloClinicalQueueGeneral.gloQueueMetadatawriter()
        Dim QueueDoc As gloClinicalQueueGeneral.Queue = Nothing
        Try
            'Dim strFilePath As String = GenerateClinicalChartFileName(ds, 0, True)

            QueueDoc = _QueueWriter.GenerateWordMetaDataFile(gloGlobal.gloTSPrint.PatientName, gloGlobal.gloTSPrint.PatientDOB, gloGlobal.gloTSPrint.AddFooterInService, PhysicalFile, strFilePath.Substring(strFilePath.LastIndexOf("\") + 1), isClaim:=True, _
             claimType:=claimType, PrintType:=PrintType)
            Try
                gloQueueSchema.gloSerialization.SetClinicalDocument(strFilePath, QueueDoc)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
                ex = Nothing
                Return False
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        Finally
            If (_QueueWriter IsNot Nothing) Then
                _QueueWriter.Dispose()
                _QueueWriter = Nothing
            End If
            If (QueueDoc IsNot Nothing) Then
                QueueDoc = Nothing
            End If
        End Try

    End Function
End Class
