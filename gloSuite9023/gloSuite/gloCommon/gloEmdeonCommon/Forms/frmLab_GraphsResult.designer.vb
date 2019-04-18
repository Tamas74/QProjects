<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_GraphsResult
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
            Try
                If (IsNothing(printDialog) = False) Then
                    printDialog.Dispose()
                    printDialog = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(printGraph) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(printGraph)
                    printGraph.Dispose()
                    printGraph = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(printPreviewDialog) = False) Then
                    printPreviewDialog.Dispose()
                    printPreviewDialog = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_GraphsResult))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlMainAll = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chtResultsGraph = New AxMSChart20Lib.AxMSChart
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        'Removed PegausImageXpress7 -> Dhruv
        'Me.PicImag_container = New PegasusImaging.WinForms.ImagXpress7.PICImagXpress
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.pnlTopDtls = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblTest = New System.Windows.Forms.Label
        Me.lblTestDetails = New System.Windows.Forms.Label
        Me.lblResult = New System.Windows.Forms.Label
        Me.lblResultDetails = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblToDate = New System.Windows.Forms.Label
        Me.lblToDt = New System.Windows.Forms.Label
        Me.lblfromDate = New System.Windows.Forms.Label
        Me.lblfromDt = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.DataGrid2 = New System.Windows.Forms.DataGridView
        Me.DataGrid1 = New System.Windows.Forms.DataGridView
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.pnlLegents = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblBlueTxt = New System.Windows.Forms.Label
        Me.lblBlueColor = New System.Windows.Forms.Label
        Me.lblGreenTxt = New System.Windows.Forms.Label
        Me.lblGreenColor = New System.Windows.Forms.Label
        Me.lblBrownTxt = New System.Windows.Forms.Label
        Me.lblBrownL = New System.Windows.Forms.Label
        Me.lblBlue = New System.Windows.Forms.Label
        Me.lblGreen = New System.Windows.Forms.Label
        Me.lblBrown = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.printGraph = New System.Drawing.Printing.PrintDocument
        Me.printPreviewDialog = New System.Windows.Forms.PrintPreviewDialog
        Me.printDialog = New System.Windows.Forms.PrintDialog
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel
        Me.tlsp_LabGraphResult = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ts_btnChangeCriteria = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.PrintPro1 = New PegasusImaging.WinForms.PrintPro4.PrintPro(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlMainAll.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chtResultsGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTopDtls.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.pnlLegents.SuspendLayout()
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tlsp_LabGraphResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.pnlMainAll)
        Me.pnlMain.Controls.Add(Me.pnlTopDtls)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(771, 572)
        Me.pnlMain.TabIndex = 1
        '
        'pnlMainAll
        '
        Me.pnlMainAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMainAll.Controls.Add(Me.Panel6)
        Me.pnlMainAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainAll.Location = New System.Drawing.Point(0, 33)
        Me.pnlMainAll.Name = "pnlMainAll"
        Me.pnlMainAll.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlMainAll.Size = New System.Drawing.Size(771, 539)
        Me.pnlMainAll.TabIndex = 4
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.chtResultsGraph)
        Me.Panel6.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel6.Controls.Add(Me.lbl_RightBrd)
        Me.Panel6.Controls.Add(Me.lbl_TopBrd)
        'Removed PegausImageXpress7 -> Dhruv
        'Me.Panel6.Controls.Add(Me.PicImag_container)
        Me.Panel6.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(765, 539)
        Me.Panel6.TabIndex = 9
        '
        'chtResultsGraph
        '
        Me.chtResultsGraph.DataSource = Nothing
        Me.chtResultsGraph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtResultsGraph.Location = New System.Drawing.Point(1, 1)
        Me.chtResultsGraph.Name = "chtResultsGraph"
        Me.chtResultsGraph.OcxState = CType(resources.GetObject("chtResultsGraph.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtResultsGraph.Size = New System.Drawing.Size(763, 537)
        Me.chtResultsGraph.TabIndex = 4
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 538)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(763, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(764, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 538)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(1, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(764, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        'Removed PegausImageXpress7 -> Dhruv
        '
        'PicImag_container
        '
        'Me.PicImag_container.AddOnBehavior = PegasusImaging.WinForms.ImagXpress7.enumAddOnBehavior.ADDONBEHAVIOR_ShowEval
        'Me.PicImag_container.AlignH = PegasusImaging.WinForms.ImagXpress7.enumAlignH.ALIGNH_Center
        'Me.PicImag_container.AlignV = PegasusImaging.WinForms.ImagXpress7.enumAlignV.ALIGNV_Center
        'Me.PicImag_container.AspectX = 1
        'Me.PicImag_container.AspectY = 1
        'Me.PicImag_container.Async = False
        'Me.PicImag_container.AsyncCancelOnClose = False
        'Me.PicImag_container.AsyncMaxThreads = 4
        'Me.PicImag_container.AsyncPriority = PegasusImaging.WinForms.ImagXpress7.enumAsyncPriority.ASYNC_Normal
        'Me.PicImag_container.AutoClose = True
        'Me.PicImag_container.AutoInvalidate = True
        'Me.PicImag_container.AutoSize = PegasusImaging.WinForms.ImagXpress7.enumAutoSize.ISIZE_CropImage
        'Me.PicImag_container.BorderType = PegasusImaging.WinForms.ImagXpress7.enumBorderType.BORD_Inset
        'Me.PicImag_container.CancelLoad = False
        'Me.PicImag_container.CancelMode = PegasusImaging.WinForms.ImagXpress7.enumCancelMode.CM_None
        'Me.PicImag_container.CancelRemove = False
        'Me.PicImag_container.CompressInMemory = PegasusImaging.WinForms.ImagXpress7.enumCompressInMemory.CMEM_NONE
        'Me.PicImag_container.CropX = 0
        'Me.PicImag_container.CropY = 0
        'Me.PicImag_container.DIBXPos = -1
        'Me.PicImag_container.DIBYPos = -1
        'Me.PicImag_container.DisplayError = False
        'Me.PicImag_container.DisplayMode = PegasusImaging.WinForms.ImagXpress7.enumPicDisplayMode.DM_TrueColor
        'Me.PicImag_container.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.PicImag_container.DrawFillColor = System.Drawing.Color.Black
        'Me.PicImag_container.DrawFillStyle = PegasusImaging.WinForms.ImagXpress7.enumFillStyle.FILL_Transparent
        'Me.PicImag_container.DrawMode = PegasusImaging.WinForms.ImagXpress7.enumDrawMode.PEN_CopyPen
        'Me.PicImag_container.DrawStyle = PegasusImaging.WinForms.ImagXpress7.enumBorderStyle.STYLE_Solid
        'Me.PicImag_container.DrawWidth = 1
        'Me.PicImag_container.Edition = PegasusImaging.WinForms.ImagXpress7.enumEdition.EDITION_Photo
        'Me.PicImag_container.EvalMode = PegasusImaging.WinForms.ImagXpress7.enumEvaluationMode.EVAL_Automatic
        'Me.PicImag_container.FileName = ""
        'Me.PicImag_container.FileOffsetUse = PegasusImaging.WinForms.ImagXpress7.enumFileOffsetUse.FO_IMAGE
        'Me.PicImag_container.FileTimeout = 10000
        'Me.PicImag_container.FTPMode = PegasusImaging.WinForms.ImagXpress7.enumFTPMode.FTP_ACTIVE
        'Me.PicImag_container.FTPPassword = ""
        'Me.PicImag_container.FTPUserName = ""
        'Me.PicImag_container.IResUnits = PegasusImaging.WinForms.ImagXpress7.enumIRes.IRes_Inch
        'Me.PicImag_container.IResX = 0
        'Me.PicImag_container.IResY = 0
        'Me.PicImag_container.JPEGCosited = False
        'Me.PicImag_container.JPEGEnhDecomp = True
        'Me.PicImag_container.LoadCropEnabled = False
        'Me.PicImag_container.LoadCropHeight = 0
        'Me.PicImag_container.LoadCropWidth = 0
        'Me.PicImag_container.LoadCropX = 0
        'Me.PicImag_container.LoadCropY = 0
        'Me.PicImag_container.LoadResizeEnabled = False
        'Me.PicImag_container.LoadResizeHeight = 0
        'Me.PicImag_container.LoadResizeMaintainAspectRatio = True
        'Me.PicImag_container.LoadResizeWidth = 0
        'Me.PicImag_container.LoadRotated = PegasusImaging.WinForms.ImagXpress7.enumLoadRotated.LR_NONE
        'Me.PicImag_container.LoadThumbnail = PegasusImaging.WinForms.ImagXpress7.enumLoadThumbnail.THUMBNAIL_None
        'Me.PicImag_container.Location = New System.Drawing.Point(1, 0)
        'Me.PicImag_container.LZWPassword = ""
        'Me.PicImag_container.ManagePalette = True
        'Me.PicImag_container.MaxHeight = 0
        'Me.PicImag_container.MaxWidth = 0
        'Me.PicImag_container.MinHeight = 1
        'Me.PicImag_container.MinWidth = 1
        'Me.PicImag_container.MousePointer = PegasusImaging.WinForms.ImagXpress7.enumMousePointer.MP_Default
        'Me.PicImag_container.Multitask = False
        'Me.PicImag_container.Name = "PicImag_container"
        'Me.PicImag_container.Notify = False
        'Me.PicImag_container.NotifyDelay = 0
        'Me.PicImag_container.OLEDropMode = PegasusImaging.WinForms.ImagXpress7.enumOLEDropMode.OLEDROP_NONE
        'Me.PicImag_container.OwnDIB = True
        'Me.PicImag_container.PageNbr = 0
        'Me.PicImag_container.Pages = 0
        'Me.PicImag_container.Palette = PegasusImaging.WinForms.ImagXpress7.enumPalette.IPAL_Optimized
        'Me.PicImag_container.PDFSwapBlackandWhite = False
        'Me.PicImag_container.Persistence = True
        'Me.PicImag_container.PFileName = ""
        'Me.PicImag_container.PICPassword = ""
        'Me.PicImag_container.Picture = Nothing
        'Me.PicImag_container.PictureEnabled = True
        'Me.PicImag_container.PrinterBanding = False
        'Me.PicImag_container.ProgressEnabled = False
        'Me.PicImag_container.ProgressPct = 10
        'Me.PicImag_container.ProxyServer = ""
        'Me.PicImag_container.RaiseExceptions = False
        'Me.PicImag_container.SaveCompressSize = 0
        'Me.PicImag_container.SaveEXIFThumbnailSize = CType(0, Short)
        'Me.PicImag_container.SaveFileName = ""
        'Me.PicImag_container.SaveFileType = PegasusImaging.WinForms.ImagXpress7.enumSaveFileType.FT_DEFAULT
        'Me.PicImag_container.SaveGIFInterlaced = False
        'Me.PicImag_container.SaveGIFType = PegasusImaging.WinForms.ImagXpress7.enumGIFType.GIF89a
        'Me.PicImag_container.SaveJLSInterLeave = 0
        'Me.PicImag_container.SaveJLSMaxValue = 0
        'Me.PicImag_container.SaveJLSNear = 0
        'Me.PicImag_container.SaveJLSPoint = 0
        'Me.PicImag_container.SaveJP2Order = PegasusImaging.WinForms.ImagXpress7.enumProgressionOrder.PO_DEFAULT
        'Me.PicImag_container.SaveJP2Type = PegasusImaging.WinForms.ImagXpress7.enumJP2Type.JP2_LOSSY
        'Me.PicImag_container.SaveJPEGChromFactor = CType(10, Short)
        'Me.PicImag_container.SaveJPEGCosited = False
        'Me.PicImag_container.SaveJPEGGrayscale = False
        'Me.PicImag_container.SaveJPEGLumFactor = CType(10, Short)
        'Me.PicImag_container.SaveJPEGProgressive = False
        'Me.PicImag_container.SaveJPEGSubSampling = PegasusImaging.WinForms.ImagXpress7.enumSaveJPEGSubSampling.SS_411
        'Me.PicImag_container.SaveLJPCompMethod = PegasusImaging.WinForms.ImagXpress7.enumSaveLJPCompMethod.LJPMETHOD_JPEG
        'Me.PicImag_container.SaveLJPCompType = PegasusImaging.WinForms.ImagXpress7.enumSaveLJPCompType.LJPTYPE_RGB
        'Me.PicImag_container.SaveLJPPrediction = CType(1, Short)
        'Me.PicImag_container.SaveMultiPage = False
        'Me.PicImag_container.SavePBMType = PegasusImaging.WinForms.ImagXpress7.enumPortableType.PT_Binary
        'Me.PicImag_container.SavePDFCompression = PegasusImaging.WinForms.ImagXpress7.enumSavePDFCompression.PDF_Compress_Default
        'Me.PicImag_container.SavePDFSwapBlackandWhite = False
        'Me.PicImag_container.SavePGMType = PegasusImaging.WinForms.ImagXpress7.enumPortableType.PT_Binary
        'Me.PicImag_container.SavePNGInterlaced = False
        'Me.PicImag_container.SavePPMType = PegasusImaging.WinForms.ImagXpress7.enumPortableType.PT_Binary
        'Me.PicImag_container.SaveTIFFByteOrder = PegasusImaging.WinForms.ImagXpress7.enumSaveTIFFByteOrder.TIFF_INTEL
        'Me.PicImag_container.SaveTIFFCompression = PegasusImaging.WinForms.ImagXpress7.enumSaveTIFFCompression.TIFF_Uncompressed
        'Me.PicImag_container.SaveTIFFRowsPerStrip = 0
        'Me.PicImag_container.SaveTileHeight = 0
        'Me.PicImag_container.SaveTileWidth = 0
        'Me.PicImag_container.SaveToBuffer = False
        'Me.PicImag_container.SaveTransparencyColor = System.Drawing.Color.Black
        'Me.PicImag_container.SaveTransparent = PegasusImaging.WinForms.ImagXpress7.enumTransparencyMatch.TRANSPARENT_None
        'Me.PicImag_container.SaveWSQBlack = CType(0, Short)
        'Me.PicImag_container.SaveWSQQuant = 1
        'Me.PicImag_container.SaveWSQWhite = CType(255, Short)
        'Me.PicImag_container.ScaleResizeToGray = False
        'Me.PicImag_container.ScrollBarLargeChangeH = 10
        'Me.PicImag_container.ScrollBarLargeChangeV = 10
        'Me.PicImag_container.ScrollBars = PegasusImaging.WinForms.ImagXpress7.enumScrollBars.SB_None
        'Me.PicImag_container.ScrollBarSmallChangeH = 1
        'Me.PicImag_container.ScrollBarSmallChangeV = 1
        'Me.PicImag_container.ShowHourglass = False
        'Me.PicImag_container.Size = New System.Drawing.Size(764, 539)
        'Me.PicImag_container.SN = "TEXHZ700AX-PEG0Z0001MG"
        'Me.PicImag_container.SpecialTIFFHandling = False
        'Me.PicImag_container.TabIndex = 3
        'Me.PicImag_container.Text = "PicImagXpress1"
        'Me.PicImag_container.TIFFIFDOffset = 0
        'Me.PicImag_container.Timer = 0
        'Me.PicImag_container.TWAINManufacturer = ""
        'Me.PicImag_container.TWAINProductFamily = ""
        'Me.PicImag_container.TWAINProductName = ""
        'Me.PicImag_container.TWAINVersionInfo = ""
        'Me.PicImag_container.UndoEnabled = False
        'Me.PicImag_container.ViewAntialias = True
        'Me.PicImag_container.ViewBrightness = CType(0, Short)
        'Me.PicImag_container.ViewContrast = CType(0, Short)
        'Me.PicImag_container.ViewDithered = True
        'Me.PicImag_container.ViewGrayMode = PegasusImaging.WinForms.ImagXpress7.enumGrayMode.GRAY_Standard
        'Me.PicImag_container.ViewProgressive = False
        'Me.PicImag_container.ViewSmoothing = True
        'Me.PicImag_container.ViewUpdate = True
        'Me.PicImag_container.WMFConvert = False
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 539)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'pnlTopDtls
        '
        Me.pnlTopDtls.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopDtls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopDtls.Controls.Add(Me.Panel5)
        Me.pnlTopDtls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopDtls.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopDtls.Name = "pnlTopDtls"
        Me.pnlTopDtls.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTopDtls.Size = New System.Drawing.Size(771, 33)
        Me.pnlTopDtls.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Panel4)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(765, 27)
        Me.Panel5.TabIndex = 6
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lblTest)
        Me.Panel4.Controls.Add(Me.lblTestDetails)
        Me.Panel4.Controls.Add(Me.lblResult)
        Me.Panel4.Controls.Add(Me.lblResultDetails)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(410, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(354, 25)
        Me.Panel4.TabIndex = 10
        '
        'lblTest
        '
        Me.lblTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTest.Location = New System.Drawing.Point(0, 0)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(128, 25)
        Me.lblTest.TabIndex = 2
        Me.lblTest.Text = "Test :"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTest.Visible = False
        '
        'lblTestDetails
        '
        Me.lblTestDetails.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblTestDetails.Location = New System.Drawing.Point(128, 0)
        Me.lblTestDetails.Name = "lblTestDetails"
        Me.lblTestDetails.Size = New System.Drawing.Size(67, 25)
        Me.lblTestDetails.TabIndex = 8
        Me.lblTestDetails.Text = "Testdtl"
        Me.lblTestDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTestDetails.Visible = False
        '
        'lblResult
        '
        Me.lblResult.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResult.Location = New System.Drawing.Point(195, 0)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(77, 25)
        Me.lblResult.TabIndex = 6
        Me.lblResult.Text = "Result :"
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblResult.Visible = False
        '
        'lblResultDetails
        '
        Me.lblResultDetails.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblResultDetails.Location = New System.Drawing.Point(272, 0)
        Me.lblResultDetails.Name = "lblResultDetails"
        Me.lblResultDetails.Size = New System.Drawing.Size(82, 25)
        Me.lblResultDetails.TabIndex = 7
        Me.lblResultDetails.Text = "ResultDtls"
        Me.lblResultDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblResultDetails.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblToDate)
        Me.Panel2.Controls.Add(Me.lblToDt)
        Me.Panel2.Controls.Add(Me.lblfromDate)
        Me.Panel2.Controls.Add(Me.lblfromDt)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(391, 25)
        Me.Panel2.TabIndex = 9
        '
        'lblToDate
        '
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(257, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(134, 25)
        Me.lblToDate.TabIndex = 5
        Me.lblToDate.Text = "ToDate"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblToDt
        '
        Me.lblToDt.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDt.Location = New System.Drawing.Point(190, 0)
        Me.lblToDt.Name = "lblToDt"
        Me.lblToDt.Size = New System.Drawing.Size(67, 25)
        Me.lblToDt.TabIndex = 4
        Me.lblToDt.Text = "To Date :"
        Me.lblToDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblfromDate
        '
        Me.lblfromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblfromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfromDate.Location = New System.Drawing.Point(89, 0)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(101, 25)
        Me.lblfromDate.TabIndex = 1
        Me.lblfromDate.Text = "fromDate"
        Me.lblfromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblfromDt
        '
        Me.lblfromDt.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblfromDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfromDt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblfromDt.Location = New System.Drawing.Point(0, 0)
        Me.lblfromDt.Name = "lblfromDt"
        Me.lblfromDt.Size = New System.Drawing.Size(89, 25)
        Me.lblfromDt.TabIndex = 0
        Me.lblfromDt.Text = "  From Date :"
        Me.lblfromDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 25)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(0, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(764, 1)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(764, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 26)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(765, 1)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.DataGrid2)
        Me.Panel1.Controls.Add(Me.DataGrid1)
        Me.Panel1.Location = New System.Drawing.Point(40, 341)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(70, 56)
        Me.Panel1.TabIndex = 3
        Me.Panel1.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(155, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(175, 100)
        Me.Panel3.TabIndex = 3
        '
        'DataGrid2
        '
        Me.DataGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid2.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGrid2.Location = New System.Drawing.Point(0, 150)
        Me.DataGrid2.Name = "DataGrid2"
        Me.DataGrid2.Size = New System.Drawing.Size(70, 150)
        Me.DataGrid2.TabIndex = 2
        Me.DataGrid2.Visible = False
        '
        'DataGrid1
        '
        Me.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGrid1.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(70, 150)
        Me.DataGrid1.TabIndex = 0
        Me.DataGrid1.Visible = False
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.pnlLegents)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 626)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlBottom.Size = New System.Drawing.Size(771, 28)
        Me.pnlBottom.TabIndex = 2
        Me.pnlBottom.Visible = False
        '
        'pnlLegents
        '
        Me.pnlLegents.BackColor = System.Drawing.Color.Transparent
        Me.pnlLegents.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.pnlLegents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLegents.Controls.Add(Me.Label9)
        Me.pnlLegents.Controls.Add(Me.Label10)
        Me.pnlLegents.Controls.Add(Me.Label11)
        Me.pnlLegents.Controls.Add(Me.Label8)
        Me.pnlLegents.Controls.Add(Me.lblBlueTxt)
        Me.pnlLegents.Controls.Add(Me.lblBlueColor)
        Me.pnlLegents.Controls.Add(Me.lblGreenTxt)
        Me.pnlLegents.Controls.Add(Me.lblGreenColor)
        Me.pnlLegents.Controls.Add(Me.lblBrownTxt)
        Me.pnlLegents.Controls.Add(Me.lblBrownL)
        Me.pnlLegents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLegents.Location = New System.Drawing.Point(3, 3)
        Me.pnlLegents.Name = "pnlLegents"
        Me.pnlLegents.Size = New System.Drawing.Size(765, 22)
        Me.pnlLegents.TabIndex = 3
        Me.pnlLegents.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 20)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(764, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 20)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(765, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(0, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(765, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'lblBlueTxt
        '
        Me.lblBlueTxt.AutoSize = True
        Me.lblBlueTxt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlueTxt.Location = New System.Drawing.Point(221, 4)
        Me.lblBlueTxt.Name = "lblBlueTxt"
        Me.lblBlueTxt.Size = New System.Drawing.Size(82, 14)
        Me.lblBlueTxt.TabIndex = 5
        Me.lblBlueTxt.Text = "Actual value"
        '
        'lblBlueColor
        '
        Me.lblBlueColor.BackColor = System.Drawing.Color.Blue
        Me.lblBlueColor.Location = New System.Drawing.Point(197, 9)
        Me.lblBlueColor.Name = "lblBlueColor"
        Me.lblBlueColor.Size = New System.Drawing.Size(18, 5)
        Me.lblBlueColor.TabIndex = 4
        '
        'lblGreenTxt
        '
        Me.lblGreenTxt.AutoSize = True
        Me.lblGreenTxt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGreenTxt.Location = New System.Drawing.Point(129, 4)
        Me.lblGreenTxt.Name = "lblGreenTxt"
        Me.lblGreenTxt.Size = New System.Drawing.Size(62, 14)
        Me.lblGreenTxt.TabIndex = 3
        Me.lblGreenTxt.Text = "Minimum"
        '
        'lblGreenColor
        '
        Me.lblGreenColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGreenColor.Location = New System.Drawing.Point(105, 9)
        Me.lblGreenColor.Name = "lblGreenColor"
        Me.lblGreenColor.Size = New System.Drawing.Size(18, 5)
        Me.lblGreenColor.TabIndex = 2
        '
        'lblBrownTxt
        '
        Me.lblBrownTxt.AutoSize = True
        Me.lblBrownTxt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrownTxt.Location = New System.Drawing.Point(34, 4)
        Me.lblBrownTxt.Name = "lblBrownTxt"
        Me.lblBrownTxt.Size = New System.Drawing.Size(65, 14)
        Me.lblBrownTxt.TabIndex = 1
        Me.lblBrownTxt.Text = "Maximum"
        '
        'lblBrownL
        '
        Me.lblBrownL.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBrownL.Location = New System.Drawing.Point(10, 9)
        Me.lblBrownL.Name = "lblBrownL"
        Me.lblBrownL.Size = New System.Drawing.Size(18, 5)
        Me.lblBrownL.TabIndex = 0
        '
        'lblBlue
        '
        Me.lblBlue.AutoSize = True
        Me.lblBlue.BackColor = System.Drawing.Color.Transparent
        Me.lblBlue.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblBlue.Location = New System.Drawing.Point(208, 0)
        Me.lblBlue.Name = "lblBlue"
        Me.lblBlue.Size = New System.Drawing.Size(83, 14)
        Me.lblBlue.TabIndex = 7
        Me.lblBlue.Text = "Actual value"
        '
        'lblGreen
        '
        Me.lblGreen.AutoSize = True
        Me.lblGreen.BackColor = System.Drawing.Color.Transparent
        Me.lblGreen.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGreen.Location = New System.Drawing.Point(105, 0)
        Me.lblGreen.Name = "lblGreen"
        Me.lblGreen.Size = New System.Drawing.Size(83, 14)
        Me.lblGreen.TabIndex = 6
        Me.lblGreen.Text = "Actual value"
        '
        'lblBrown
        '
        Me.lblBrown.AutoSize = True
        Me.lblBrown.BackColor = System.Drawing.Color.Transparent
        Me.lblBrown.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblBrown.Location = New System.Drawing.Point(20, 0)
        Me.lblBrown.Name = "lblBrown"
        Me.lblBrown.Size = New System.Drawing.Size(65, 14)
        Me.lblBrown.TabIndex = 5
        Me.lblBrown.Text = "Maximum"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 27)
        Me.Label1.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(85, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 27)
        Me.Label2.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Blue
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(188, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 27)
        Me.Label3.TabIndex = 10
        '
        'printPreviewDialog
        '
        Me.printPreviewDialog.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.printPreviewDialog.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.printPreviewDialog.ClientSize = New System.Drawing.Size(400, 300)
        Me.printPreviewDialog.Enabled = True
        Me.printPreviewDialog.Icon = CType(resources.GetObject("printPreviewDialog.Icon"), System.Drawing.Icon)
        Me.printPreviewDialog.Name = "printPreviewDialog"
        Me.printPreviewDialog.Visible = False
        '
        'printDialog
        '
        Me.printDialog.UseEXDialog = True
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.Controls.Add(Me.tlsp_LabGraphResult)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(771, 54)
        Me.pnl_ToolStrip.TabIndex = 14
        '
        'tlsp_LabGraphResult
        '
        Me.tlsp_LabGraphResult.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_LabGraphResult.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_LabGraphResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_LabGraphResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_LabGraphResult.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_LabGraphResult.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnPrint, Me.ts_btnChangeCriteria, Me.ts_btnClose})
        Me.tlsp_LabGraphResult.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_LabGraphResult.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_LabGraphResult.Name = "tlsp_LabGraphResult"
        Me.tlsp_LabGraphResult.Size = New System.Drawing.Size(771, 53)
        Me.tlsp_LabGraphResult.TabIndex = 0
        Me.tlsp_LabGraphResult.Text = "toolStrip1"
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnChangeCriteria
        '
        Me.ts_btnChangeCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnChangeCriteria.Image = CType(resources.GetObject("ts_btnChangeCriteria.Image"), System.Drawing.Image)
        Me.ts_btnChangeCriteria.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnChangeCriteria.Name = "ts_btnChangeCriteria"
        Me.ts_btnChangeCriteria.Size = New System.Drawing.Size(105, 50)
        Me.ts_btnChangeCriteria.Tag = "Change Criteria"
        Me.ts_btnChangeCriteria.Text = "&Change Criteria"
        Me.ts_btnChangeCriteria.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmLab_GraphsResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(771, 654)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_ToolStrip)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLab_GraphsResult"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab Graphs Result"
        Me.TopMost = True
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMainAll.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.chtResultsGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTopDtls.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlLegents.ResumeLayout(False)
        Me.pnlLegents.PerformLayout()
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tlsp_LabGraphResult.ResumeLayout(False)
        Me.tlsp_LabGraphResult.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGrid2 As System.Windows.Forms.DataGridView
    Friend WithEvents pnlMainAll As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopDtls As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblResultDetails As System.Windows.Forms.Label
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblToDt As System.Windows.Forms.Label
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents lblfromDate As System.Windows.Forms.Label
    Friend WithEvents lblfromDt As System.Windows.Forms.Label
    Friend WithEvents lblTestDetails As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblBlue As System.Windows.Forms.Label
    Friend WithEvents lblGreen As System.Windows.Forms.Label
    Friend WithEvents lblBrown As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlLegents As System.Windows.Forms.Panel
    Friend WithEvents lblBlueTxt As System.Windows.Forms.Label
    Friend WithEvents lblBlueColor As System.Windows.Forms.Label
    Friend WithEvents lblGreenTxt As System.Windows.Forms.Label
    Friend WithEvents lblGreenColor As System.Windows.Forms.Label
    Friend WithEvents lblBrownTxt As System.Windows.Forms.Label
    Friend WithEvents lblBrownL As System.Windows.Forms.Label
    Friend WithEvents printGraph As System.Drawing.Printing.PrintDocument
    Friend WithEvents printPreviewDialog As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents printDialog As System.Windows.Forms.PrintDialog
    Friend WithEvents chtResultsGraph As AxMSChart20Lib.AxMSChart
    'Removed PegausImageXpress7 -> Dhruv
    'Friend WithEvents PicImag_container As PegasusImaging.WinForms.ImagXpress7.PICImagXpress
    Private WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Private WithEvents tlsp_LabGraphResult As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnChangeCriteria As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PrintPro1 As PegasusImaging.WinForms.PrintPro4.PrintPro
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    'Friend WithEvents Panel7 As System.Windows.Forms.Panel
End Class
