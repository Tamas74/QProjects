Imports System.Windows.Forms
Imports pdftron.PDF
Imports System.IO

Public Class frmPrintDialog
    Dim _file As String
    Dim isprinted As Boolean = False
    'Start'GLO2011-0011148 ''White Space Issue'
    Public oDialogResultIsOK As Boolean = False
    Dim pdfdraw As pdftron.PDF.PDFDraw = Nothing
    Dim oPDFDoc As pdftron.PDF.PDFDoc = Nothing
    Private print_page_itr As pdftron.PDF.PageIterator
    Private Shared myPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing
    'Private _printPageIndex As Integer = 0
    ' Private _nPagesCnt As Integer = 0
    'End'GLO2011-0011148 ''White Space Issue'
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmPrintDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rbCustomDPI.Checked = True
            numCustomDPIValue.Value = 150

            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        End Try
    End Sub
    Private Function PrintPDFDocument(ByVal myString As String) As Boolean
        'Dim ObjPrintFax As New clsPrintFAX()



        'Dim myPDfDraw As PDFDraw = Nothing
        Dim myPDFdoc As PDFDoc = Nothing
        'Dim myPageitr As PageIterator = Nothing
        '  Dim printDocument1 As System.Drawing.Printing.PrintDocument = New System.Drawing.Printing.PrintDocument()
        myPDFdoc = New PDFDoc(myString)
        myPDFdoc.InitSecurityHandler()

        Dim myPrintMode As PrinterMode = New PrinterMode()
        Try
            myPrintMode.SetCollation(True)
            myPrintMode.SetCopyCount(1)
            myPrintMode.SetDPI(300)     ' regardless of ordering, an explicit DPI setting overrides the OutputQuality setting
            myPrintMode.SetDuplexing(PrinterMode.DuplexMode.e_Duplex_Auto)
            myPrintMode.SetOutputColor(PrinterMode.OutputColor.e_OutputColor_Grayscale)
            myPrintMode.SetOutputQuality(PrinterMode.OutputQuality.e_OutputQuality_Medium)


            Dim myPagesToPrint As PageSet = New PageSet(1, myPDFdoc.GetPageCount(), PageSet.Filter.e_all)

            'Print.StartPrintJob(myPDFdoc, "", myPDFdoc.GetFileName(), "", myPagesToPrint, myPrintMode)
            'Dim strTIFFFileName As String
            ' Dim ObjPrintFax As clsPrintFAX = New clsPrintFAX()
            ' strTIFFFileName = ObjPrintFax.RetrieveFAXDocumentName()
            Dim PrinterName = ""
            If gblnUseDefaultPrinter Then
            Else

            End If
            '----------
            Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
            Dim _sDefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
            objPrintDocument.Dispose()
            objPrintDocument = Nothing
            If gblnUseDefaultPrinter = True Then

                PrinterName = _sDefaultPrinter
                ' oTempDoc.Application.ActivePrinter = oTempDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).
            Else
                '  Dim PrintDialog1 As PrintDialog = New PrintDialog()
                If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    PrinterName = PrintDialog1.PrinterSettings.PrinterName
                    myPrinterSetting = PrintDialog1.PrinterSettings
                Else
                    '     PrintDialog1.Dispose()
                    '    PrintDialog1 = Nothing

                    myPagesToPrint.Dispose()
                    myPagesToPrint = Nothing
                    myPrintMode.Dispose()
                    myPrintMode = Nothing
                    Return False
                End If

                'PrintDialog1.Dispose()
                'PrintDialog1 = Nothing
            End If
            '----------
            Me.SuspendLayout()
            Try
                Print.StartPrintJob(myPDFdoc, PrinterName, myPDFdoc.GetFileName(), "", myPagesToPrint, myPrintMode)
            Catch ex As Exception
                If (ex.ToString.Contains("Unable to run COM library in COINIT_MULTITHREADED")) Then
                    Print.StartPrintJob(myPDFdoc, PrinterName, myPDFdoc.GetFileName(), "", myPagesToPrint, myPrintMode)
                End If

            End Try


            myPagesToPrint.Dispose()
            myPagesToPrint = Nothing
            If IsNothing(myPDFdoc) = False Then
                myPDFdoc.Dispose()
            End If
            Threading.Thread.Sleep(1000)
            Me.ResumeLayout()
        Catch ex As Exception
            Me.ResumeLayout()
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Return False
        End Try

        myPrintMode.Dispose()
        myPrintMode = Nothing
        Return True
    End Function

    Public Sub New(ByVal file As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _file = file
        If (IsNothing(myPrinterSetting)) Then
            myPrinterSetting = New System.Drawing.Printing.PrinterSettings()
        End If

    End Sub

    Private Sub tblbtn_Print_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Print_32.Click
        Try
            Panel2.Enabled = False
            lblPrint.Text = "Printing..."
            Application.DoEvents()
            'PrintPDFDocument(_file) Start'GLO2011-0011148 ''White Space Issue''
            PrintDocuments(_file)
            isprinted = True
            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        Finally
            Panel2.Enabled = True
        End Try
    End Sub

#Region "Start'GLO2011-0011148 ''White Space Issue''"
    Private Sub PrintPDF(ByVal myString As String)
        'Dim oPrintController As System.Drawing.Printing.StandardPrintController = Nothing

        Try
            print_page_itr = oPDFDoc.GetPageIterator()
            'oPrintController = New System.Drawing.Printing.StandardPrintController()
            'printDocument1.PrintController = oPrintController

            If gblnUseDefaultPrinter = False Then
                Try
                    ' PrintDialog1 = New PrintDialog()
                    '   PrintDialog1.ShowDialog();
                    '    'If PrintDialog1.Then Then
                    '   'If Not IsNothing(oRpt) Then
                    '   '    oRpt.Close()
                    '    'End If


                    'get the flag to show default printer driver
                    If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = System.Windows.Forms.DialogResult.OK Then
                        myPrinterSetting = PrintDialog1.PrinterSettings
                        Try
                            printDocument1.PrinterSettings = myPrinterSetting
                        Catch ex As Exception
                            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName
                        End Try


                        'Lines Added By Dipak 20090909 
                        Me.Cursor = Cursors.WaitCursor
                        oDialogResultIsOK = True
                        pdfdraw = New pdftron.PDF.PDFDraw()

                        If rbCustomDPI.Checked Then
                            pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn)
                        Else
                            pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus)
                        End If

                        oPDFDoc.Lock()
                        '  _printPageIndex = 0
                        printDocument1.DocumentName = myString

                        If Not IsNothing(rect) Then
                            rect.Dispose()
                            rect = Nothing
                        End If

                        printDocument1.Print()
                        oPDFDoc.Unlock()

                        'if
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    ex = Nothing
                Finally
                    If Not IsNothing(pdfdraw) Then
                        pdfdraw.Dispose()
                        pdfdraw = Nothing
                    End If

                    'If Not IsNothing(PrintDialog1) Then
                    'PrintDialog1.Dispose()
                    'PrintDialog1 = Nothing
                    'End If
                End Try

            Else

                Me.Cursor = Cursors.WaitCursor
                Try
                    'else case added for default printer to fix bug 4941 :-Scan Docs > Print > Can’t print DMS item

                    If (IsNothing(myPrinterSetting)) Then
                        myPrinterSetting = New System.Drawing.Printing.PrinterSettings()
                    End If

                    If (IsNothing(myPrinterSetting) = False) Then
                        Try
                            printDocument1.PrinterSettings = myPrinterSetting
                        Catch ex As Exception
                            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName
                        End Try
                    End If


                    oDialogResultIsOK = True
                    pdfdraw = New pdftron.PDF.PDFDraw()

                    If rbCustomDPI.Checked Then
                        pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn)
                    Else
                        pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus)
                    End If

                    oPDFDoc.Lock()
                    '  _printPageIndex = 0
                    printDocument1.DocumentName = myString

                    If Not IsNothing(rect) Then
                        rect.Dispose()
                        rect = Nothing
                    End If

                    printDocument1.Print()
                    oPDFDoc.Unlock()
                    'if
                    'Lines comented by dipak to fix bug 2690 :Print from DMS Comented line are moved above
                    'I Change only Conditional scope.
                    'pdfdraw = new pdftron.PDF.PDFDraw();
                    'printDocument1.Print();
                    'pdfdraw.Dispose();
                    'pdfdraw = null;
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    ex = Nothing
                Finally
                    If Not IsNothing(pdfdraw) Then
                        pdfdraw.Dispose()
                        pdfdraw = Nothing
                    End If
                    'If Not IsNothing(myPrinterSetting) Then
                    '    ' myPrinterSetting.Dispose()
                    '    myPrinterSetting = Nothing
                    'End If
                End Try

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        Finally
            'oPrintController = Nothing

            If Not IsNothing(print_page_itr) Then
                print_page_itr.Dispose()
                print_page_itr = Nothing
            End If
            If Not IsNothing(rect) Then
                rect.Dispose()
                rect = Nothing
            End If
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Dim rect As pdftron.PDF.Rect = Nothing
    Private Sub printDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDocument1.PrintPage

        ' Dim gr As Graphics = Nothing


        '  Dim doc As PDFDoc = Nothing

        Try
            e.Graphics.PageUnit = GraphicsUnit.Inch

            If (IsNothing(rect)) Then

                If rbCustomDPI.Checked Then
                    Dim dpi As Single = Math.Max(e.Graphics.DpiX, e.Graphics.DpiY)
                    If dpi > numCustomDPIValue.Value Then
                        dpi = numCustomDPIValue.Value
                        pdfdraw.SetDPI(dpi)
                    End If
                End If


                'Dim rectPage As Rectangle = e.PageBounds

                'Dim left As Double = Nothing
                'Dim right As Double = Nothing
                'Dim top As Double = Nothing
                'Dim bottom As Double = Nothing

                '' print without margins 
                '' Rectangle rectPage = ev.MarginBounds ' print using margins 

                'left = rectPage.Left / 100
                'right = rectPage.Right / 100
                'top = rectPage.Top / 100
                'bottom = rectPage.Bottom / 100

                ' The above page dimensions are in inches. We need to convert 
                ' the page dimensions to PDF units (or points). One point is 
                ' 1/72 of an inch. 

                ''Page footer was cutting while printing exam from clinical chart.
                rect = New pdftron.PDF.Rect((e.PageBounds.Left / 100) * 72, ((e.PageBounds.Bottom / 100) * 72) - 10, (e.PageBounds.Right / 100) * 72, (e.PageBounds.Top / 100) * 72)
                'rectPage = Nothing

                'left = Nothing
                'right = Nothing
                'top = Nothing
                'bottom = Nothing


            End If
            ' Dim cnt As Int32 = Nothing
            Try
                prgGeneratefile.Value += 1
                lblPrint.Text = "Printing..." & prgGeneratefile.Value & "/" & prgGeneratefile.Maximum
                prgGeneratefile.Parent.Refresh()
                'lblPrint.Parent.Refresh()

                Application.DoEvents()
                pdfdraw.DrawInRect(print_page_itr.Current, e.Graphics, rect)


                'prgGeneratefile.Invalidate()
                'lblPrint.Invalidate()
                'prgGeneratefile.Refresh()

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                ex = Nothing
            Finally
                'cnt = Nothing
            End Try

            print_page_itr.Next()
            e.HasMorePages = print_page_itr.HasNext()


            'If _printPageIndex < (_nPagesCnt - 1) Then
            '    e.HasMorePages = True
            '    _printPageIndex = _printPageIndex + 1

            'Else
            '    e.HasMorePages = False
            '    _printPageIndex = 0

            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        Finally


            'If Not IsNothing(rect) Then
            '    rect.Dispose()
            '    rect = Nothing
            'End If


        End Try
    End Sub
    'Private Sub printDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Try
    '        'PDFDoc doc = oPDFDoc; //GetPDFDoc();
    '        If oPDFDoc Is Nothing Then
    '            MessageBox.Show("Error: Print document is not selected.")
    '            Return
    '        End If

    '        'print_page_itr = oPDFDoc.GetPageIterator()
    '        ' doc.GetPage(1);// PageBegin;
    '        ' PDFNet includes two different rasterizer implementations. 
    '        ' 
    '        ' The two implementations offer a trade-off between print 
    '        ' speed and accuracy/quality, as well as a trade-off between 
    '        ' vector and raster output. 
    '        ' 
    '        ' e_GDIPlus rasterizer can be used to render the page 
    '        ' using Windows GDI+, whereas e_BuiltIn rasterizer can 
    '        ' be used to render bitmaps using platform-independent 
    '        ' graphics engine (in this case images are always converted 
    '        ' to bitmap prior to printing). 

    '        pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_GDIPlus)

    '        ' You can uncomment the following lines in order to use 
    '        ' built-in, platform-independent rasterizer instead of GDI+. 
    '        ' pdfdraw.SetRasterizerType(PDFRasterizer.Type.e_BuiltIn) 
    '        ' pdfdraw.SetDPI(200) 
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
    '        ex = Nothing
    '    End Try
    'End Sub
    Private Function PrintDocuments(ByVal myString As String) As Boolean
        Dim oFile As FileInfo = Nothing
        Try
            oFile = New FileInfo(myString)

            oPDFDoc = New PDFDoc(myString)

            If oPDFDoc Is Nothing Then
                MessageBox.Show("Error: Print document is not selected.")
                Return False
            End If

            Dim _nPagesCnt As Integer = oPDFDoc.GetPageCount()

            If (_nPagesCnt > 0) Then
                prgGeneratefile.Maximum = _nPagesCnt
                prgGeneratefile.Value = 0
                PrintPDF(oFile.Name)
            Else
                MessageBox.Show("Error: No pages in the document.")
                Return False
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Return False
        Finally
            If Not IsNothing(oPDFDoc) Then
                oPDFDoc.Dispose()
                oPDFDoc = Nothing
            End If

            If Not IsNothing(oFile) Then
                oFile = Nothing
            End If
        End Try

        Return True
    End Function
#End Region




    
    Private Sub rbCustomDPI_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbCustomDPI.CheckedChanged
        Try
            If rbCustomDPI.Checked Then
                numCustomDPIValue.Enabled = True
            Else
                numCustomDPIValue.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            ex = Nothing
        End Try
    End Sub
End Class
