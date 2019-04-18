
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_ReceivedFax
        {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    components.Dispose();
                    try
                    {
                        if (printDocument1 != null)
                        {
                            try
                            {
                                
                                printDocument1.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                            }
                            catch
                            {
                            }
                            try
                            {

                                printDocument1.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
                            }
                            catch
                            {
                            }
                            printDocument1.Dispose();
                            printDocument1 = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (timer1 != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(timer1);
                            timer1.Dispose();
                            timer1 = null;
                        }
                    }
                    catch
                    {
                    }
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_ReceivedFax));
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_SendToRCM = new System.Windows.Forms.ToolStripButton();
            this.tlb_Remove = new System.Windows.Forms.ToolStripButton();
            this.tlb_RemoveAll = new System.Windows.Forms.ToolStripButton();
            this.tlb_Print = new System.Windows.Forms.ToolStripButton();
            this.tlb_LoadImages = new System.Windows.Forms.ToolStripButton();
            this.tlb_RotateBack = new System.Windows.Forms.ToolStripButton();
            this.tlb_RotateForward = new System.Windows.Forms.ToolStripButton();
            this.tlb_LoadMoreFaxFiles = new System.Windows.Forms.ToolStripButton();
            this.tlb_ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tlb_ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tlb_ExportToPDF = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlScanDocument = new System.Windows.Forms.Panel();
            this.pnlPreviewDMSDoc = new System.Windows.Forms.Panel();
            this.label61 = new System.Windows.Forms.Label();
            this.pnlPreviewCommand = new System.Windows.Forms.Panel();
            this.lblPreviewStatus = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.pnlViewImage = new System.Windows.Forms.Panel();
            this.pnlPreviewProgressBar = new System.Windows.Forms.Panel();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.txtWait = new System.Windows.Forms.TextBox();
            this.imageControl1 = new gloScanImaging.ImageControl();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.ZoomHeightButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomFitButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomWidthButton = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.cmbZoomPercentage = new System.Windows.Forms.ComboBox();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPatients = new System.Windows.Forms.Label();
            this.btnPat_Up = new System.Windows.Forms.Button();
            this.btnPat_Down = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlSmallStrip = new System.Windows.Forms.Panel();
            this.pnlSmallStripMain = new System.Windows.Forms.Panel();
            this.ts_SmallStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_SmallStrip_btn_Document = new System.Windows.Forms.ToolStripButton();
            this.btn_Right = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlDocumentNameAcquiredImages = new System.Windows.Forms.Panel();
            this.pnlAcquiredImages = new System.Windows.Forms.Panel();
            this.c1Documents = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label66 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlAcquiredImagesHeader = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btn_Collapse = new System.Windows.Forms.Button();
            this.btn_Expand = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.btn_Left = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.btn_Collapse_1 = new System.Windows.Forms.Button();
            this.btn_Expand_1 = new System.Windows.Forms.Button();
            this.pnlReceivedFaxDetailsBody = new System.Windows.Forms.Panel();
            this.label42 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblNumberOfFiles = new System.Windows.Forms.Label();
            this.lblFilesLoaded = new System.Windows.Forms.Label();
            this.lbl_FilesRemaining = new System.Windows.Forms.Label();
            this.lblFilesRemaining = new System.Windows.Forms.Label();
            this.lbl_TotalFiles = new System.Windows.Forms.Label();
            this.lbl_LoadedFiles = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlReceivedFaxDeatilsHeader = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.btn_Down = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.btn_Up = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.pnlDocumentName = new System.Windows.Forms.Panel();
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tmrprint = new System.Windows.Forms.Timer(this.components);
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlSetting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlScanDocument.SuspendLayout();
            this.pnlPreviewDMSDoc.SuspendLayout();
            this.pnlPreviewCommand.SuspendLayout();
            this.pnlViewImage.SuspendLayout();
            this.pnlPreviewProgressBar.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.pnlProgressBar.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSmallStrip.SuspendLayout();
            this.pnlSmallStripMain.SuspendLayout();
            this.ts_SmallStrip.SuspendLayout();
            this.pnlDocumentNameAcquiredImages.SuspendLayout();
            this.pnlAcquiredImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).BeginInit();
            this.pnlAcquiredImagesHeader.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlReceivedFaxDetailsBody.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlReceivedFaxDeatilsHeader.SuspendLayout();
            this.pnlDocumentName.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_SendToRCM,
            this.tlb_Remove,
            this.tlb_RemoveAll,
            this.tlb_Print,
            this.tlb_LoadImages,
            this.tlb_RotateBack,
            this.tlb_RotateForward,
            this.tlb_LoadMoreFaxFiles,
            this.tlb_ZoomIn,
            this.tlb_ZoomOut,
            this.tlb_ExportToPDF,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(941, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(93, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "&Send To DMS";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Send To DMS";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_SendToRCM
            // 
            this.tlb_SendToRCM.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SendToRCM.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SendToRCM.Image")));
            this.tlb_SendToRCM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SendToRCM.Name = "tlb_SendToRCM";
            this.tlb_SendToRCM.Size = new System.Drawing.Size(93, 50);
            this.tlb_SendToRCM.Tag = "Send To RCM";
            this.tlb_SendToRCM.Text = "Send &To RCM";
            this.tlb_SendToRCM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SendToRCM.ToolTipText = "Send To RCM (Revenue Cycle Management)";
            this.tlb_SendToRCM.Click += new System.EventHandler(this.tlb_SendToRCM_Click);
            // 
            // tlb_Remove
            // 
            this.tlb_Remove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Remove.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Remove.Image")));
            this.tlb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Remove.Name = "tlb_Remove";
            this.tlb_Remove.Size = new System.Drawing.Size(50, 50);
            this.tlb_Remove.Tag = "Remove";
            this.tlb_Remove.Text = "&Delete";
            this.tlb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Remove.ToolTipText = "Delete";
            this.tlb_Remove.Click += new System.EventHandler(this.tlb_Remove_Click);
            // 
            // tlb_RemoveAll
            // 
            this.tlb_RemoveAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RemoveAll.Image")));
            this.tlb_RemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RemoveAll.Name = "tlb_RemoveAll";
            this.tlb_RemoveAll.Size = new System.Drawing.Size(79, 50);
            this.tlb_RemoveAll.Tag = "RemoveAll";
            this.tlb_RemoveAll.Text = "Re&move All";
            this.tlb_RemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RemoveAll.ToolTipText = "Remove All";
            this.tlb_RemoveAll.Visible = false;
            // 
            // tlb_Print
            // 
            this.tlb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Print.Image")));
            this.tlb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Print.Name = "tlb_Print";
            this.tlb_Print.Size = new System.Drawing.Size(41, 50);
            this.tlb_Print.Tag = "Print";
            this.tlb_Print.Text = "&Print";
            this.tlb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Print.ToolTipText = "Print";
            this.tlb_Print.Click += new System.EventHandler(this.tlb_Print_Click);
            // 
            // tlb_LoadImages
            // 
            this.tlb_LoadImages.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_LoadImages.Image = ((System.Drawing.Image)(resources.GetObject("tlb_LoadImages.Image")));
            this.tlb_LoadImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_LoadImages.Name = "tlb_LoadImages";
            this.tlb_LoadImages.Size = new System.Drawing.Size(58, 50);
            this.tlb_LoadImages.Tag = "LoadImages";
            this.tlb_LoadImages.Text = "&Refresh";
            this.tlb_LoadImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_LoadImages.ToolTipText = "Refresh";
            this.tlb_LoadImages.Click += new System.EventHandler(this.tlb_LoadImages_Click);
            // 
            // tlb_RotateBack
            // 
            this.tlb_RotateBack.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RotateBack.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RotateBack.Image")));
            this.tlb_RotateBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RotateBack.Name = "tlb_RotateBack";
            this.tlb_RotateBack.Size = new System.Drawing.Size(86, 50);
            this.tlb_RotateBack.Tag = "BackRotate";
            this.tlb_RotateBack.Text = "Rotate &CCW";
            this.tlb_RotateBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RotateBack.ToolTipText = "Rotate Counterclockwise";
            this.tlb_RotateBack.Click += new System.EventHandler(this.tlb_RotateBack_Click);
            // 
            // tlb_RotateForward
            // 
            this.tlb_RotateForward.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RotateForward.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RotateForward.Image")));
            this.tlb_RotateForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RotateForward.Name = "tlb_RotateForward";
            this.tlb_RotateForward.Size = new System.Drawing.Size(78, 50);
            this.tlb_RotateForward.Tag = "ForwardRotate";
            this.tlb_RotateForward.Text = "Rotate C&W";
            this.tlb_RotateForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RotateForward.ToolTipText = "Rotate Clockwise";
            this.tlb_RotateForward.Click += new System.EventHandler(this.tlb_RotateForward_Click);
            // 
            // tlb_LoadMoreFaxFiles
            // 
            this.tlb_LoadMoreFaxFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_LoadMoreFaxFiles.Image = ((System.Drawing.Image)(resources.GetObject("tlb_LoadMoreFaxFiles.Image")));
            this.tlb_LoadMoreFaxFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_LoadMoreFaxFiles.Name = "tlb_LoadMoreFaxFiles";
            this.tlb_LoadMoreFaxFiles.Size = new System.Drawing.Size(71, 50);
            this.tlb_LoadMoreFaxFiles.Tag = "MoreFiles";
            this.tlb_LoadMoreFaxFiles.Text = "More &Files";
            this.tlb_LoadMoreFaxFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_LoadMoreFaxFiles.ToolTipText = "More Files";
            this.tlb_LoadMoreFaxFiles.Click += new System.EventHandler(this.tlb_LoadMoreFaxFiles_Click);
            // 
            // tlb_ZoomIn
            // 
            this.tlb_ZoomIn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ZoomIn.Image")));
            this.tlb_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ZoomIn.Name = "tlb_ZoomIn";
            this.tlb_ZoomIn.Size = new System.Drawing.Size(62, 50);
            this.tlb_ZoomIn.Tag = "ZoomIn";
            this.tlb_ZoomIn.Text = "Zoom &In";
            this.tlb_ZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ZoomIn.Visible = false;
            // 
            // tlb_ZoomOut
            // 
            this.tlb_ZoomOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ZoomOut.Image")));
            this.tlb_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ZoomOut.Name = "tlb_ZoomOut";
            this.tlb_ZoomOut.Size = new System.Drawing.Size(72, 50);
            this.tlb_ZoomOut.Tag = "ZoomOut";
            this.tlb_ZoomOut.Text = "Zoom &Out";
            this.tlb_ZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ZoomOut.Visible = false;
            // 
            // tlb_ExportToPDF
            // 
            this.tlb_ExportToPDF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ExportToPDF.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ExportToPDF.Image")));
            this.tlb_ExportToPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ExportToPDF.Name = "tlb_ExportToPDF";
            this.tlb_ExportToPDF.Size = new System.Drawing.Size(52, 50);
            this.tlb_ExportToPDF.Tag = "Export";
            this.tlb_ExportToPDF.Text = "&Export";
            this.tlb_ExportToPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ExportToPDF.ToolTipText = "Export";
            this.tlb_ExportToPDF.Click += new System.EventHandler(this.tlb_ExportToPDF_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.panel1);
            this.pnlSetting.Controls.Add(this.label14);
            this.pnlSetting.Controls.Add(this.label13);
            this.pnlSetting.Controls.Add(this.label8);
            this.pnlSetting.Controls.Add(this.label7);
            this.pnlSetting.Controls.Add(this.checkBox1);
            this.pnlSetting.Controls.Add(this.comboBox4);
            this.pnlSetting.Controls.Add(this.label4);
            this.pnlSetting.Controls.Add(this.comboBox3);
            this.pnlSetting.Controls.Add(this.label3);
            this.pnlSetting.Controls.Add(this.comboBox2);
            this.pnlSetting.Controls.Add(this.label2);
            this.pnlSetting.Controls.Add(this.comboBox1);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetting.Location = new System.Drawing.Point(0, 56);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSetting.Size = new System.Drawing.Size(941, 642);
            this.pnlSetting.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 24);
            this.panel1.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(933, 23);
            this.label15.TabIndex = 2;
            this.label15.Text = "Settings";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Black;
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(0, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(933, 1);
            this.label16.TabIndex = 3;
            this.label16.Text = "label16";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 638);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(933, 1);
            this.label14.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(4, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(933, 1);
            this.label13.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 636);
            this.label8.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(937, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 636);
            this.label7.TabIndex = 20;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(315, 386);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 18);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Show Scanner Dialog";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(315, 341);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(227, 22);
            this.comboBox4.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "Scan Mode :";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(315, 295);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(227, 22);
            this.comboBox3.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Scan Side :";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(315, 249);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(227, 22);
            this.comboBox2.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "Resolution :";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(315, 203);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(227, 22);
            this.comboBox1.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scanner :";
            // 
            // pnlScanDocument
            // 
            this.pnlScanDocument.Controls.Add(this.pnlPreviewDMSDoc);
            this.pnlScanDocument.Controls.Add(this.pnlViewImage);
            this.pnlScanDocument.Controls.Add(this.pnlPatients);
            this.pnlScanDocument.Controls.Add(this.panel7);
            this.pnlScanDocument.Controls.Add(this.pnlSmallStrip);
            this.pnlScanDocument.Controls.Add(this.splitter1);
            this.pnlScanDocument.Controls.Add(this.pnlDocumentNameAcquiredImages);
            this.pnlScanDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScanDocument.Location = new System.Drawing.Point(0, 56);
            this.pnlScanDocument.Name = "pnlScanDocument";
            this.pnlScanDocument.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlScanDocument.Size = new System.Drawing.Size(941, 642);
            this.pnlScanDocument.TabIndex = 0;
            // 
            // pnlPreviewDMSDoc
            // 
            this.pnlPreviewDMSDoc.BackColor = System.Drawing.Color.White;
            this.pnlPreviewDMSDoc.Controls.Add(this.label61);
            this.pnlPreviewDMSDoc.Controls.Add(this.pnlPreviewCommand);
            this.pnlPreviewDMSDoc.Controls.Add(this.label59);
            this.pnlPreviewDMSDoc.Controls.Add(this.label60);
            this.pnlPreviewDMSDoc.Controls.Add(this.label62);
            this.pnlPreviewDMSDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewDMSDoc.ForeColor = System.Drawing.Color.White;
            this.pnlPreviewDMSDoc.Location = new System.Drawing.Point(312, 145);
            this.pnlPreviewDMSDoc.Name = "pnlPreviewDMSDoc";
            this.pnlPreviewDMSDoc.Size = new System.Drawing.Size(626, 494);
            this.pnlPreviewDMSDoc.TabIndex = 28;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label61.Location = new System.Drawing.Point(1, 493);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(624, 1);
            this.label61.TabIndex = 27;
            this.label61.Text = "label2";
            // 
            // pnlPreviewCommand
            // 
            this.pnlPreviewCommand.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewCommand.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.pnlPreviewCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPreviewCommand.Controls.Add(this.lblPreviewStatus);
            this.pnlPreviewCommand.Controls.Add(this.btnLast);
            this.pnlPreviewCommand.Controls.Add(this.btnNext);
            this.pnlPreviewCommand.Controls.Add(this.btnPrevious);
            this.pnlPreviewCommand.Controls.Add(this.btnFirst);
            this.pnlPreviewCommand.Controls.Add(this.label58);
            this.pnlPreviewCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPreviewCommand.Location = new System.Drawing.Point(1, 1);
            this.pnlPreviewCommand.Name = "pnlPreviewCommand";
            this.pnlPreviewCommand.Size = new System.Drawing.Size(624, 26);
            this.pnlPreviewCommand.TabIndex = 0;
            // 
            // lblPreviewStatus
            // 
            this.lblPreviewStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblPreviewStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviewStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPreviewStatus.Location = new System.Drawing.Point(96, 0);
            this.lblPreviewStatus.Name = "lblPreviewStatus";
            this.lblPreviewStatus.Size = new System.Drawing.Size(528, 25);
            this.lblPreviewStatus.TabIndex = 20;
            this.lblPreviewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Transparent;
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.Location = new System.Drawing.Point(72, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(24, 25);
            this.btnLast.TabIndex = 3;
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(48, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(24, 25);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(24, 0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(24, 25);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Transparent;
            this.btnFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Location = new System.Drawing.Point(0, 0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(24, 25);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label58.Location = new System.Drawing.Point(0, 25);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(624, 1);
            this.label58.TabIndex = 24;
            this.label58.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Right;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label59.Location = new System.Drawing.Point(625, 1);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 493);
            this.label59.TabIndex = 25;
            this.label59.Text = "label2";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.Location = new System.Drawing.Point(0, 1);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 493);
            this.label60.TabIndex = 26;
            this.label60.Text = "label2";
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(626, 1);
            this.label62.TabIndex = 28;
            this.label62.Text = "label2";
            // 
            // pnlViewImage
            // 
            this.pnlViewImage.AutoScroll = true;
            this.pnlViewImage.Controls.Add(this.pnlPreviewProgressBar);
            this.pnlViewImage.Controls.Add(this.panel9);
            this.pnlViewImage.Controls.Add(this.pnlProgressBar);
            this.pnlViewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewImage.Location = new System.Drawing.Point(312, 145);
            this.pnlViewImage.Name = "pnlViewImage";
            this.pnlViewImage.Size = new System.Drawing.Size(626, 494);
            this.pnlViewImage.TabIndex = 2;
            // 
            // pnlPreviewProgressBar
            // 
            this.pnlPreviewProgressBar.AutoScroll = true;
            this.pnlPreviewProgressBar.AutoSize = true;
            this.pnlPreviewProgressBar.Controls.Add(this.pnlPreview);
            this.pnlPreviewProgressBar.Controls.Add(this.label22);
            this.pnlPreviewProgressBar.Controls.Add(this.label23);
            this.pnlPreviewProgressBar.Controls.Add(this.label24);
            this.pnlPreviewProgressBar.Controls.Add(this.label25);
            this.pnlPreviewProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewProgressBar.Location = new System.Drawing.Point(0, 28);
            this.pnlPreviewProgressBar.Name = "pnlPreviewProgressBar";
            this.pnlPreviewProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPreviewProgressBar.Size = new System.Drawing.Size(626, 434);
            this.pnlPreviewProgressBar.TabIndex = 9;
            // 
            // pnlPreview
            // 
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.AutoSize = true;
            this.pnlPreview.Controls.Add(this.txtWait);
            this.pnlPreview.Controls.Add(this.imageControl1);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(4, 1);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(618, 429);
            this.pnlPreview.TabIndex = 12;
            // 
            // txtWait
            // 
            this.txtWait.BackColor = System.Drawing.Color.White;
            this.txtWait.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWait.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWait.Enabled = false;
            this.txtWait.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWait.ForeColor = System.Drawing.Color.Navy;
            this.txtWait.Location = new System.Drawing.Point(0, 0);
            this.txtWait.Multiline = true;
            this.txtWait.Name = "txtWait";
            this.txtWait.ReadOnly = true;
            this.txtWait.Size = new System.Drawing.Size(618, 429);
            this.txtWait.TabIndex = 27;
            this.txtWait.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\nLoading fax files... Please wait ...";
            this.txtWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imageControl1
            // 
            this.imageControl1.AutoScroll = true;
            this.imageControl1.BackColor = System.Drawing.Color.White;
            this.imageControl1.CurrImage = null;
            this.imageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.imageControl1.ImgPath = null;
            this.imageControl1.Location = new System.Drawing.Point(0, 0);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(618, 429);
            this.imageControl1.TabIndex = 7;
            this.imageControl1.SizeChanged += new System.EventHandler(this.imageControl1_SizeChanged);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(4, 430);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(618, 1);
            this.label22.TabIndex = 16;
            this.label22.Text = "label2";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(3, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 430);
            this.label23.TabIndex = 15;
            this.label23.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(622, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 430);
            this.label24.TabIndex = 14;
            this.label24.Text = "label3";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(620, 1);
            this.label25.TabIndex = 13;
            this.label25.Text = "label1";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel4);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel9.Size = new System.Drawing.Size(626, 28);
            this.panel9.TabIndex = 16;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label67);
            this.panel4.Controls.Add(this.toolStrip2);
            this.panel4.Controls.Add(this.btnZoomOut);
            this.panel4.Controls.Add(this.cmbZoomPercentage);
            this.panel4.Controls.Add(this.btnZoomIn);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(620, 25);
            this.panel4.TabIndex = 4;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Dock = System.Windows.Forms.DockStyle.Top;
            this.label67.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(1, 1);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(345, 22);
            this.label67.TabIndex = 14;
            this.label67.Text = "  Preview";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_LongButton;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomHeightButton,
            this.ZoomFitButton,
            this.ZoomWidthButton});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(346, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(72, 23);
            this.toolStrip2.TabIndex = 9;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // ZoomHeightButton
            // 
            this.ZoomHeightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZoomHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomHeightButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomHeightButton.Image")));
            this.ZoomHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomHeightButton.Name = "ZoomHeightButton";
            this.ZoomHeightButton.Size = new System.Drawing.Size(23, 20);
            this.ZoomHeightButton.Text = "toolStripButton1";
            this.ZoomHeightButton.ToolTipText = "Fit To Height";
            this.ZoomHeightButton.Click += new System.EventHandler(this.ZoomHeightButton_Click);
            // 
            // ZoomFitButton
            // 
            this.ZoomFitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZoomFitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomFitButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomFitButton.Image")));
            this.ZoomFitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomFitButton.Name = "ZoomFitButton";
            this.ZoomFitButton.Size = new System.Drawing.Size(23, 20);
            this.ZoomFitButton.Text = "toolStripButton1";
            this.ZoomFitButton.ToolTipText = "Best Fit";
            this.ZoomFitButton.Click += new System.EventHandler(this.ZoomFitButton_Click);
            // 
            // ZoomWidthButton
            // 
            this.ZoomWidthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomWidthButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomWidthButton.Image")));
            this.ZoomWidthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomWidthButton.Name = "ZoomWidthButton";
            this.ZoomWidthButton.Size = new System.Drawing.Size(23, 20);
            this.ZoomWidthButton.Text = "toolStripButton2";
            this.ZoomWidthButton.ToolTipText = "Fit to Width";
            this.ZoomWidthButton.Click += new System.EventHandler(this.ZoomWidthButton_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Location = new System.Drawing.Point(418, 1);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(24, 23);
            this.btnZoomOut.TabIndex = 8;
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseLeave += new System.EventHandler(this.btnZoomOut_MouseLeave);
            this.btnZoomOut.MouseHover += new System.EventHandler(this.btnZoomOut_MouseHover);
            // 
            // cmbZoomPercentage
            // 
            this.cmbZoomPercentage.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbZoomPercentage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoomPercentage.FormattingEnabled = true;
            this.cmbZoomPercentage.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "125%",
            "150%",
            "175%",
            "200%",
            "400%",
            "FITPAGE",
            "FITWIDTH",
            "FITHEIGHT",
            "ACTUALSIZE"});
            this.cmbZoomPercentage.Location = new System.Drawing.Point(442, 1);
            this.cmbZoomPercentage.Name = "cmbZoomPercentage";
            this.cmbZoomPercentage.Size = new System.Drawing.Size(105, 22);
            this.cmbZoomPercentage.TabIndex = 6;
            this.cmbZoomPercentage.SelectedIndexChanged += new System.EventHandler(this.cmbZoomPercentage_SelectedIndexChanged);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Location = new System.Drawing.Point(547, 1);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(24, 23);
            this.btnZoomIn.TabIndex = 7;
            this.btnZoomIn.UseVisualStyleBackColor = false;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomIn.MouseLeave += new System.EventHandler(this.btnZoomIn_MouseLeave);
            this.btnZoomIn.MouseHover += new System.EventHandler(this.btnZoomIn_MouseHover);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(571, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 1;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(595, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 23);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(618, 1);
            this.label9.TabIndex = 13;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 24);
            this.label10.TabIndex = 12;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(619, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 24);
            this.label11.TabIndex = 11;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(620, 1);
            this.label12.TabIndex = 10;
            this.label12.Text = "label1";
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.Controls.Add(this.label21);
            this.pnlProgressBar.Controls.Add(this.label26);
            this.pnlProgressBar.Controls.Add(this.label56);
            this.pnlProgressBar.Controls.Add(this.label57);
            this.pnlProgressBar.Controls.Add(this.pbDocument);
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 462);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlProgressBar.Size = new System.Drawing.Size(626, 32);
            this.pnlProgressBar.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(4, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(618, 1);
            this.label21.TabIndex = 12;
            this.label21.Text = "label2";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 28);
            this.label26.TabIndex = 11;
            this.label26.Text = "label4";
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label56.Location = new System.Drawing.Point(622, 1);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 28);
            this.label56.TabIndex = 10;
            this.label56.Text = "label3";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Top;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(3, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(620, 1);
            this.label57.TabIndex = 9;
            this.label57.Text = "label1";
            // 
            // pbDocument
            // 
            this.pbDocument.Location = new System.Drawing.Point(10, 5);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(577, 19);
            this.pbDocument.TabIndex = 6;
            // 
            // pnlPatients
            // 
            this.pnlPatients.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatients.Location = new System.Drawing.Point(312, 36);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlPatients.Size = new System.Drawing.Size(626, 109);
            this.pnlPatients.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(312, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel7.Size = new System.Drawing.Size(626, 36);
            this.panel7.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_LongOrange;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblPatients);
            this.panel2.Controls.Add(this.btnPat_Up);
            this.panel2.Controls.Add(this.btnPat_Down);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 30);
            this.panel2.TabIndex = 0;
            // 
            // lblPatients
            // 
            this.lblPatients.AutoEllipsis = true;
            this.lblPatients.BackColor = System.Drawing.Color.Transparent;
            this.lblPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatients.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblPatients.ForeColor = System.Drawing.Color.Black;
            this.lblPatients.Location = new System.Drawing.Point(1, 1);
            this.lblPatients.Name = "lblPatients";
            this.lblPatients.Size = new System.Drawing.Size(573, 28);
            this.lblPatients.TabIndex = 2;
            this.lblPatients.Text = "  Patients";
            this.lblPatients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPat_Up
            // 
            this.btnPat_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPat_Up.FlatAppearance.BorderSize = 0;
            this.btnPat_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPat_Up.Location = new System.Drawing.Point(574, 1);
            this.btnPat_Up.Name = "btnPat_Up";
            this.btnPat_Up.Size = new System.Drawing.Size(24, 28);
            this.btnPat_Up.TabIndex = 1;
            this.btnPat_Up.UseVisualStyleBackColor = false;
            this.btnPat_Up.Click += new System.EventHandler(this.btnPat_Up_Click);
            this.btnPat_Up.MouseLeave += new System.EventHandler(this.btnPat_Up_MouseLeave);
            this.btnPat_Up.MouseHover += new System.EventHandler(this.btnPat_Up_MouseHover);
            // 
            // btnPat_Down
            // 
            this.btnPat_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPat_Down.FlatAppearance.BorderSize = 0;
            this.btnPat_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPat_Down.Location = new System.Drawing.Point(598, 1);
            this.btnPat_Down.Name = "btnPat_Down";
            this.btnPat_Down.Size = new System.Drawing.Size(24, 28);
            this.btnPat_Down.TabIndex = 0;
            this.btnPat_Down.UseVisualStyleBackColor = false;
            this.btnPat_Down.Click += new System.EventHandler(this.btnPat_Down_Click);
            this.btnPat_Down.MouseLeave += new System.EventHandler(this.btnPat_Down_MouseLeave);
            this.btnPat_Down.MouseHover += new System.EventHandler(this.btnPat_Down_MouseHover);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(1, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(621, 1);
            this.label17.TabIndex = 12;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 29);
            this.label18.TabIndex = 11;
            this.label18.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(622, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 29);
            this.label19.TabIndex = 10;
            this.label19.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(623, 1);
            this.label20.TabIndex = 9;
            this.label20.Text = "label1";
            // 
            // pnlSmallStrip
            // 
            this.pnlSmallStrip.Controls.Add(this.pnlSmallStripMain);
            this.pnlSmallStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSmallStrip.Location = new System.Drawing.Point(284, 0);
            this.pnlSmallStrip.Name = "pnlSmallStrip";
            this.pnlSmallStrip.Size = new System.Drawing.Size(28, 639);
            this.pnlSmallStrip.TabIndex = 17;
            // 
            // pnlSmallStripMain
            // 
            this.pnlSmallStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSmallStripMain.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_VerticalBtnStrip;
            this.pnlSmallStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSmallStripMain.Controls.Add(this.ts_SmallStrip);
            this.pnlSmallStripMain.Controls.Add(this.btn_Right);
            this.pnlSmallStripMain.Controls.Add(this.label6);
            this.pnlSmallStripMain.Controls.Add(this.label52);
            this.pnlSmallStripMain.Controls.Add(this.label53);
            this.pnlSmallStripMain.Controls.Add(this.label54);
            this.pnlSmallStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSmallStripMain.Location = new System.Drawing.Point(0, 0);
            this.pnlSmallStripMain.Name = "pnlSmallStripMain";
            this.pnlSmallStripMain.Size = new System.Drawing.Size(28, 639);
            this.pnlSmallStripMain.TabIndex = 15;
            // 
            // ts_SmallStrip
            // 
            this.ts_SmallStrip.BackColor = System.Drawing.Color.Transparent;
            this.ts_SmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_SmallStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_SmallStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_SmallStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_SmallStrip_btn_Document});
            this.ts_SmallStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ts_SmallStrip.Location = new System.Drawing.Point(1, 24);
            this.ts_SmallStrip.Name = "ts_SmallStrip";
            this.ts_SmallStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ts_SmallStrip.Size = new System.Drawing.Size(26, 614);
            this.ts_SmallStrip.TabIndex = 21;
            this.ts_SmallStrip.Text = "toolStrip1";
            this.ts_SmallStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            this.ts_SmallStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_SmallStrip_ItemClicked);
            // 
            // ts_SmallStrip_btn_Document
            // 
            this.ts_SmallStrip_btn_Document.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Document.Image = ((System.Drawing.Image)(resources.GetObject("ts_SmallStrip_btn_Document.Image")));
            this.ts_SmallStrip_btn_Document.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Document.Name = "ts_SmallStrip_btn_Document";
            this.ts_SmallStrip_btn_Document.Size = new System.Drawing.Size(24, 129);
            this.ts_SmallStrip_btn_Document.Text = "Acquired Images";
            this.ts_SmallStrip_btn_Document.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // btn_Right
            // 
            this.btn_Right.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Right.BackgroundImage")));
            this.btn_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right.FlatAppearance.BorderSize = 0;
            this.btn_Right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right.Location = new System.Drawing.Point(1, 1);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(26, 23);
            this.btn_Right.TabIndex = 16;
            this.btn_Right.UseVisualStyleBackColor = false;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            this.btn_Right.MouseLeave += new System.EventHandler(this.btn_Right_MouseLeave);
            this.btn_Right.MouseHover += new System.EventHandler(this.btn_Right_MouseHover);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 1);
            this.label6.TabIndex = 20;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 638);
            this.label52.TabIndex = 9;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(27, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 638);
            this.label53.TabIndex = 14;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label54.Location = new System.Drawing.Point(0, 638);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(28, 1);
            this.label54.TabIndex = 17;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(280, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 639);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // pnlDocumentNameAcquiredImages
            // 
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlAcquiredImages);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlAcquiredImagesHeader);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlReceivedFaxDetailsBody);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.panel3);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlDocumentName);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.panel5);
            this.pnlDocumentNameAcquiredImages.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDocumentNameAcquiredImages.Location = new System.Drawing.Point(0, 0);
            this.pnlDocumentNameAcquiredImages.Name = "pnlDocumentNameAcquiredImages";
            this.pnlDocumentNameAcquiredImages.Size = new System.Drawing.Size(280, 639);
            this.pnlDocumentNameAcquiredImages.TabIndex = 0;
            // 
            // pnlAcquiredImages
            // 
            this.pnlAcquiredImages.Controls.Add(this.c1Documents);
            this.pnlAcquiredImages.Controls.Add(this.label66);
            this.pnlAcquiredImages.Controls.Add(this.label45);
            this.pnlAcquiredImages.Controls.Add(this.label46);
            this.pnlAcquiredImages.Controls.Add(this.label47);
            this.pnlAcquiredImages.Controls.Add(this.label48);
            this.pnlAcquiredImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAcquiredImages.Location = new System.Drawing.Point(0, 230);
            this.pnlAcquiredImages.Name = "pnlAcquiredImages";
            this.pnlAcquiredImages.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlAcquiredImages.Size = new System.Drawing.Size(280, 409);
            this.pnlAcquiredImages.TabIndex = 10;
            this.pnlAcquiredImages.Resize += new System.EventHandler(this.pnlAcquiredImages_Resize);
            // 
            // c1Documents
            // 
            this.c1Documents.BackColor = System.Drawing.Color.White;
            this.c1Documents.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Documents.ColumnInfo = "3,0,0,0,0,105,Columns:0{Style:\"Font:Tahoma, 9pt;Margins:1, 0, 0, 0;TextEffect:Fla" +
    "t;TextDirection:Normal;Trimming:None;Border:Flat,3,Transparent,Both;\";}\t";
            this.c1Documents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Documents.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Documents.Location = new System.Drawing.Point(9, 1);
            this.c1Documents.Name = "c1Documents";
            this.c1Documents.Rows.Count = 1;
            this.c1Documents.Rows.DefaultSize = 21;
            this.c1Documents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1Documents.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Documents.Size = new System.Drawing.Size(270, 407);
            this.c1Documents.StyleInfo = resources.GetString("c1Documents.StyleInfo");
            this.c1Documents.TabIndex = 9;
            this.c1Documents.Tree.NodeImageCollapsed = global::gloEDocumentV3.Properties.Resources.Plus;
            this.c1Documents.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Documents.Tree.NodeImageExpanded")));
            this.c1Documents.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1Documents.RowColChange += new System.EventHandler(this.c1Documents_RowColChange);
            this.c1Documents.AfterCollapse += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Documents_AfterCollapse);
            this.c1Documents.Click += new System.EventHandler(this.c1Documents_Click);
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.White;
            this.label66.Dock = System.Windows.Forms.DockStyle.Left;
            this.label66.Location = new System.Drawing.Point(4, 1);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(5, 407);
            this.label66.TabIndex = 10;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Location = new System.Drawing.Point(3, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 407);
            this.label45.TabIndex = 3;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(279, 1);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 407);
            this.label46.TabIndex = 2;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Location = new System.Drawing.Point(3, 408);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(277, 1);
            this.label47.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(3, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(277, 1);
            this.label48.TabIndex = 0;
            // 
            // pnlAcquiredImagesHeader
            // 
            this.pnlAcquiredImagesHeader.Controls.Add(this.panel8);
            this.pnlAcquiredImagesHeader.Controls.Add(this.btn_Collapse_1);
            this.pnlAcquiredImagesHeader.Controls.Add(this.btn_Expand_1);
            this.pnlAcquiredImagesHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcquiredImagesHeader.Location = new System.Drawing.Point(0, 204);
            this.pnlAcquiredImagesHeader.Name = "pnlAcquiredImagesHeader";
            this.pnlAcquiredImagesHeader.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlAcquiredImagesHeader.Size = new System.Drawing.Size(280, 26);
            this.pnlAcquiredImagesHeader.TabIndex = 12;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.btn_Collapse);
            this.panel8.Controls.Add(this.btn_Expand);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.btn_Left);
            this.panel8.Controls.Add(this.Label5);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Controls.Add(this.label44);
            this.panel8.Controls.Add(this.label49);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(277, 23);
            this.panel8.TabIndex = 4;
            // 
            // btn_Collapse
            // 
            this.btn_Collapse.BackColor = System.Drawing.Color.Transparent;
            this.btn_Collapse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Collapse.FlatAppearance.BorderSize = 0;
            this.btn_Collapse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Collapse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Collapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Collapse.Image = ((System.Drawing.Image)(resources.GetObject("btn_Collapse.Image")));
            this.btn_Collapse.Location = new System.Drawing.Point(202, 1);
            this.btn_Collapse.Name = "btn_Collapse";
            this.btn_Collapse.Size = new System.Drawing.Size(23, 21);
            this.btn_Collapse.TabIndex = 14;
            this.btn_Collapse.UseVisualStyleBackColor = true;
            this.btn_Collapse.Click += new System.EventHandler(this.btn_Collapse_Click_1);
            // 
            // btn_Expand
            // 
            this.btn_Expand.BackColor = System.Drawing.Color.Transparent;
            this.btn_Expand.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Expand.FlatAppearance.BorderSize = 0;
            this.btn_Expand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Expand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Expand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expand.Image = ((System.Drawing.Image)(resources.GetObject("btn_Expand.Image")));
            this.btn_Expand.Location = new System.Drawing.Point(225, 1);
            this.btn_Expand.Name = "btn_Expand";
            this.btn_Expand.Size = new System.Drawing.Size(23, 21);
            this.btn_Expand.TabIndex = 13;
            this.btn_Expand.UseVisualStyleBackColor = false;
            this.btn_Expand.Visible = false;
            this.btn_Expand.Click += new System.EventHandler(this.btn_Expand_Click_1);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(1, 1);
            this.label41.Name = "label41";
            this.label41.Padding = new System.Windows.Forms.Padding(3);
            this.label41.Size = new System.Drawing.Size(132, 20);
            this.label41.TabIndex = 2;
            this.label41.Text = "  Acquired Images";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Left
            // 
            this.btn_Left.BackColor = System.Drawing.Color.Transparent;
            this.btn_Left.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Left.FlatAppearance.BorderSize = 0;
            this.btn_Left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Left.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Left.Location = new System.Drawing.Point(248, 1);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(28, 21);
            this.btn_Left.TabIndex = 0;
            this.btn_Left.UseVisualStyleBackColor = false;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            this.btn_Left.MouseLeave += new System.EventHandler(this.btn_Left_MouseLeave);
            this.btn_Left.MouseHover += new System.EventHandler(this.btn_Left_MouseHover);
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(1, 22);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(275, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(0, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 22);
            this.label43.TabIndex = 11;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(276, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 22);
            this.label44.TabIndex = 10;
            this.label44.Text = "label3";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Top;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(277, 1);
            this.label49.TabIndex = 9;
            this.label49.Text = "label1";
            // 
            // btn_Collapse_1
            // 
            this.btn_Collapse_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Collapse_1.FlatAppearance.BorderSize = 0;
            this.btn_Collapse_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Collapse_1.Image = ((System.Drawing.Image)(resources.GetObject("btn_Collapse_1.Image")));
            this.btn_Collapse_1.Location = new System.Drawing.Point(20, 0);
            this.btn_Collapse_1.Name = "btn_Collapse_1";
            this.btn_Collapse_1.Size = new System.Drawing.Size(9, 25);
            this.btn_Collapse_1.TabIndex = 4;
            this.btn_Collapse_1.UseVisualStyleBackColor = false;
            this.btn_Collapse_1.Visible = false;
            // 
            // btn_Expand_1
            // 
            this.btn_Expand_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Expand_1.FlatAppearance.BorderSize = 0;
            this.btn_Expand_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Expand_1.Image = ((System.Drawing.Image)(resources.GetObject("btn_Expand_1.Image")));
            this.btn_Expand_1.Location = new System.Drawing.Point(15, 0);
            this.btn_Expand_1.Name = "btn_Expand_1";
            this.btn_Expand_1.Size = new System.Drawing.Size(9, 25);
            this.btn_Expand_1.TabIndex = 5;
            this.btn_Expand_1.UseVisualStyleBackColor = false;
            this.btn_Expand_1.Visible = false;
            // 
            // pnlReceivedFaxDetailsBody
            // 
            this.pnlReceivedFaxDetailsBody.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Gradient;
            this.pnlReceivedFaxDetailsBody.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.label42);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.label33);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.label31);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.label30);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lblNumberOfFiles);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lblFilesLoaded);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lbl_FilesRemaining);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lblFilesRemaining);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lbl_TotalFiles);
            this.pnlReceivedFaxDetailsBody.Controls.Add(this.lbl_LoadedFiles);
            this.pnlReceivedFaxDetailsBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReceivedFaxDetailsBody.Location = new System.Drawing.Point(0, 107);
            this.pnlReceivedFaxDetailsBody.Name = "pnlReceivedFaxDetailsBody";
            this.pnlReceivedFaxDetailsBody.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlReceivedFaxDetailsBody.Size = new System.Drawing.Size(280, 97);
            this.pnlReceivedFaxDetailsBody.TabIndex = 11;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Location = new System.Drawing.Point(4, 93);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(275, 1);
            this.label42.TabIndex = 14;
            this.label42.Text = "label42";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(275, 1);
            this.label33.TabIndex = 13;
            this.label33.Text = "label33";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Location = new System.Drawing.Point(279, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 94);
            this.label31.TabIndex = 12;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(3, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 94);
            this.label30.TabIndex = 11;
            // 
            // lblNumberOfFiles
            // 
            this.lblNumberOfFiles.AutoSize = true;
            this.lblNumberOfFiles.BackColor = System.Drawing.Color.Transparent;
            this.lblNumberOfFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfFiles.Location = new System.Drawing.Point(28, 14);
            this.lblNumberOfFiles.Name = "lblNumberOfFiles";
            this.lblNumberOfFiles.Size = new System.Drawing.Size(105, 14);
            this.lblNumberOfFiles.TabIndex = 5;
            this.lblNumberOfFiles.Text = "Total Fax Files :";
            // 
            // lblFilesLoaded
            // 
            this.lblFilesLoaded.AutoSize = true;
            this.lblFilesLoaded.BackColor = System.Drawing.Color.Transparent;
            this.lblFilesLoaded.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesLoaded.Location = new System.Drawing.Point(38, 38);
            this.lblFilesLoaded.Name = "lblFilesLoaded";
            this.lblFilesLoaded.Size = new System.Drawing.Size(95, 14);
            this.lblFilesLoaded.TabIndex = 6;
            this.lblFilesLoaded.Text = "Files Loaded :";
            // 
            // lbl_FilesRemaining
            // 
            this.lbl_FilesRemaining.AutoSize = true;
            this.lbl_FilesRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FilesRemaining.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FilesRemaining.Location = new System.Drawing.Point(138, 63);
            this.lbl_FilesRemaining.Name = "lbl_FilesRemaining";
            this.lbl_FilesRemaining.Size = new System.Drawing.Size(16, 14);
            this.lbl_FilesRemaining.TabIndex = 10;
            this.lbl_FilesRemaining.Text = "0";
            // 
            // lblFilesRemaining
            // 
            this.lblFilesRemaining.AutoSize = true;
            this.lblFilesRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblFilesRemaining.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesRemaining.Location = new System.Drawing.Point(20, 62);
            this.lblFilesRemaining.Name = "lblFilesRemaining";
            this.lblFilesRemaining.Size = new System.Drawing.Size(113, 14);
            this.lblFilesRemaining.TabIndex = 7;
            this.lblFilesRemaining.Text = "Files Remaining :";
            // 
            // lbl_TotalFiles
            // 
            this.lbl_TotalFiles.AutoSize = true;
            this.lbl_TotalFiles.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TotalFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotalFiles.Location = new System.Drawing.Point(138, 15);
            this.lbl_TotalFiles.Name = "lbl_TotalFiles";
            this.lbl_TotalFiles.Size = new System.Drawing.Size(16, 14);
            this.lbl_TotalFiles.TabIndex = 8;
            this.lbl_TotalFiles.Text = "0";
            // 
            // lbl_LoadedFiles
            // 
            this.lbl_LoadedFiles.AutoSize = true;
            this.lbl_LoadedFiles.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LoadedFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LoadedFiles.Location = new System.Drawing.Point(138, 39);
            this.lbl_LoadedFiles.Name = "lbl_LoadedFiles";
            this.lbl_LoadedFiles.Size = new System.Drawing.Size(16, 14);
            this.lbl_LoadedFiles.TabIndex = 9;
            this.lbl_LoadedFiles.Text = "0";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlReceivedFaxDeatilsHeader);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 77);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel3.Size = new System.Drawing.Size(280, 30);
            this.panel3.TabIndex = 13;
            // 
            // pnlReceivedFaxDeatilsHeader
            // 
            this.pnlReceivedFaxDeatilsHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlReceivedFaxDeatilsHeader.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.pnlReceivedFaxDeatilsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.label27);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.btn_Down);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.label35);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.btn_Up);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.label28);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.label40);
            this.pnlReceivedFaxDeatilsHeader.Controls.Add(this.label29);
            this.pnlReceivedFaxDeatilsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReceivedFaxDeatilsHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlReceivedFaxDeatilsHeader.Name = "pnlReceivedFaxDeatilsHeader";
            this.pnlReceivedFaxDeatilsHeader.Size = new System.Drawing.Size(277, 24);
            this.pnlReceivedFaxDeatilsHeader.TabIndex = 4;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(0, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 22);
            this.label27.TabIndex = 6;
            // 
            // btn_Down
            // 
            this.btn_Down.BackColor = System.Drawing.Color.Transparent;
            this.btn_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Down.FlatAppearance.BorderSize = 0;
            this.btn_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Down.Location = new System.Drawing.Point(228, 1);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(24, 22);
            this.btn_Down.TabIndex = 4;
            this.btn_Down.UseVisualStyleBackColor = false;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            this.btn_Down.MouseLeave += new System.EventHandler(this.btn_Down_MouseLeave_1);
            this.btn_Down.MouseHover += new System.EventHandler(this.btn_Down_MouseHover_1);
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(0, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(252, 22);
            this.label35.TabIndex = 2;
            this.label35.Text = "  Received Files Details ";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Up
            // 
            this.btn_Up.BackColor = System.Drawing.Color.Transparent;
            this.btn_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Up.FlatAppearance.BorderSize = 0;
            this.btn_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Up.Location = new System.Drawing.Point(252, 1);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(24, 22);
            this.btn_Up.TabIndex = 5;
            this.btn_Up.UseVisualStyleBackColor = false;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            this.btn_Up.MouseLeave += new System.EventHandler(this.btn_Up_MouseLeave);
            this.btn_Up.MouseHover += new System.EventHandler(this.btn_Up_MouseHover);
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Location = new System.Drawing.Point(276, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 22);
            this.label28.TabIndex = 7;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label40.Location = new System.Drawing.Point(0, 23);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(277, 1);
            this.label40.TabIndex = 3;
            this.label40.Text = "label40";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(277, 1);
            this.label29.TabIndex = 8;
            this.label29.Text = "label29";
            // 
            // pnlDocumentName
            // 
            this.pnlDocumentName.Controls.Add(this.txtDocumentName);
            this.pnlDocumentName.Controls.Add(this.label36);
            this.pnlDocumentName.Controls.Add(this.label37);
            this.pnlDocumentName.Controls.Add(this.label38);
            this.pnlDocumentName.Controls.Add(this.label39);
            this.pnlDocumentName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDocumentName.Location = new System.Drawing.Point(0, 24);
            this.pnlDocumentName.Name = "pnlDocumentName";
            this.pnlDocumentName.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.pnlDocumentName.Size = new System.Drawing.Size(280, 53);
            this.pnlDocumentName.TabIndex = 9;
            this.pnlDocumentName.Visible = false;
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(17, 16);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(252, 22);
            this.txtDocumentName.TabIndex = 11;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(3, 4);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 48);
            this.label36.TabIndex = 3;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Location = new System.Drawing.Point(279, 4);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 48);
            this.label37.TabIndex = 2;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Location = new System.Drawing.Point(3, 52);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(277, 1);
            this.label38.TabIndex = 1;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Location = new System.Drawing.Point(3, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(277, 1);
            this.label39.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel5.Size = new System.Drawing.Size(280, 24);
            this.panel5.TabIndex = 14;
            this.panel5.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label50);
            this.panel6.Controls.Add(this.label51);
            this.panel6.Controls.Add(this.label55);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(277, 24);
            this.panel6.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(1, 1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(275, 22);
            this.label32.TabIndex = 2;
            this.label32.Text = "  Document Name";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label34.Location = new System.Drawing.Point(1, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(275, 1);
            this.label34.TabIndex = 12;
            this.label34.Text = "label2";
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(0, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 23);
            this.label50.TabIndex = 11;
            this.label50.Text = "label4";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Right;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label51.Location = new System.Drawing.Point(276, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 23);
            this.label51.TabIndex = 10;
            this.label51.Text = "label3";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Top;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(0, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(277, 1);
            this.label55.TabIndex = 9;
            this.label55.Text = "label1";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.tls_MaintainDoc);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(941, 56);
            this.pnlToolstrip.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // tmrprint
            // 
            this.tmrprint.Interval = 500;
            this.tmrprint.Tick += new System.EventHandler(this.tmrprint_Tick);
            // 
            // frmEDocEvent_ReceivedFax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(941, 698);
            this.Controls.Add(this.pnlScanDocument);
            this.Controls.Add(this.pnlSetting);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmEDocEvent_ReceivedFax";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "ReceivedFax";
            this.Text = "Received Faxes";
            this.Activated += new System.EventHandler(this.frmEDocEvent_ReceivedFax_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEDocEvent_ReceivedFax_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEDocEvent_ReceivedFax_FormClosed);
            this.Load += new System.EventHandler(this.frmEDocEvent_ReceivedFax_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlScanDocument.ResumeLayout(false);
            this.pnlPreviewDMSDoc.ResumeLayout(false);
            this.pnlPreviewCommand.ResumeLayout(false);
            this.pnlViewImage.ResumeLayout(false);
            this.pnlViewImage.PerformLayout();
            this.pnlPreviewProgressBar.ResumeLayout(false);
            this.pnlPreviewProgressBar.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.pnlProgressBar.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlSmallStrip.ResumeLayout(false);
            this.pnlSmallStripMain.ResumeLayout(false);
            this.pnlSmallStripMain.PerformLayout();
            this.ts_SmallStrip.ResumeLayout(false);
            this.ts_SmallStrip.PerformLayout();
            this.pnlDocumentNameAcquiredImages.ResumeLayout(false);
            this.pnlAcquiredImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).EndInit();
            this.pnlAcquiredImagesHeader.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnlReceivedFaxDetailsBody.ResumeLayout(false);
            this.pnlReceivedFaxDetailsBody.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlReceivedFaxDeatilsHeader.ResumeLayout(false);
            this.pnlDocumentName.ResumeLayout(false);
            this.pnlDocumentName.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ResumeLayout(false);

            }


            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ToolStripButton tlb_Remove;
            private System.Windows.Forms.ToolStripButton tlb_RemoveAll;
            private System.Windows.Forms.ToolStripButton tlb_LoadImages;
            private System.Windows.Forms.ToolStripButton tlb_RotateBack;
            private System.Windows.Forms.ToolStripButton tlb_RotateForward;
            private System.Windows.Forms.Panel pnlSetting;
            private System.Windows.Forms.Panel pnlScanDocument;
            private System.Windows.Forms.Panel pnlToolstrip;
            private System.Windows.Forms.Panel pnlViewImage;
            private System.Windows.Forms.Splitter splitter1;
            private System.Windows.Forms.Panel pnlDocumentNameAcquiredImages;
            private System.Windows.Forms.Panel pnlPreviewProgressBar;
            private System.Windows.Forms.Panel panel4;
            private System.Windows.Forms.Button button3;
            private System.Windows.Forms.Button button4;
            private System.Windows.Forms.Panel pnlAcquiredImages;
            private System.Windows.Forms.Panel panel8;
            private System.Windows.Forms.Label label41;
            private System.Windows.Forms.Button btn_Left;
            private System.Windows.Forms.Label label45;
            private System.Windows.Forms.Label label46;
            private System.Windows.Forms.Label label47;
            private System.Windows.Forms.Label label48;
            private System.Windows.Forms.Panel pnlDocumentName;
            private System.Windows.Forms.Panel panel6;
            private System.Windows.Forms.Label label32;
            private System.Windows.Forms.Label label36;
            private System.Windows.Forms.Label label37;
            private System.Windows.Forms.Label label38;
            private System.Windows.Forms.Label label39;
            private System.Windows.Forms.Panel pnlProgressBar;
            private System.Windows.Forms.ProgressBar pbDocument;
            private C1.Win.C1FlexGrid.C1FlexGrid c1Documents;
            private System.Windows.Forms.Panel pnlSmallStripMain;
            private gloGlobal.gloToolStripIgnoreFocus ts_SmallStrip;
            private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Document;
            private System.Windows.Forms.Button btn_Right;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label52;
            private System.Windows.Forms.Label label53;
            private System.Windows.Forms.Label label54;
            private System.Windows.Forms.CheckBox checkBox1;
            private System.Windows.Forms.ComboBox comboBox4;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.ComboBox comboBox3;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.ComboBox comboBox2;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.ComboBox comboBox1;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label15;
            private System.Windows.Forms.Label label16;
            private System.Windows.Forms.Label label14;
            private System.Windows.Forms.Label label13;
            private System.Windows.Forms.Label label8;
            private System.Windows.Forms.Label label7;
            private System.Windows.Forms.Panel pnlPatients;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.Label lblPatients;
            private System.Windows.Forms.Button btnPat_Up;
            private System.Windows.Forms.Button btnPat_Down;
            private System.Windows.Forms.ToolStripButton tlb_ZoomIn;
            private System.Windows.Forms.Panel pnlPreview;
            private System.Windows.Forms.ToolStripButton tlb_ZoomOut;
            private System.Windows.Forms.Button btnZoomOut;
            private System.Windows.Forms.ComboBox cmbZoomPercentage;
            private System.Windows.Forms.Button btnZoomIn;
            private System.Windows.Forms.TextBox txtWait;
            private System.Windows.Forms.Timer timer1;
            private System.Windows.Forms.ToolStripButton tlb_Print;
            private System.Drawing.Printing.PrintDocument printDocument1;
            private System.Windows.Forms.ToolStripButton tlb_LoadMoreFaxFiles;
            private System.Windows.Forms.Button btn_Expand_1;
            private System.Windows.Forms.Button btn_Collapse_1;
            private System.Windows.Forms.Label lblFilesLoaded;
            private System.Windows.Forms.Label lblNumberOfFiles;
            private System.Windows.Forms.Label lbl_FilesRemaining;
            private System.Windows.Forms.Label lbl_LoadedFiles;
            private System.Windows.Forms.Label lbl_TotalFiles;
            private System.Windows.Forms.Label lblFilesRemaining;
            private System.Windows.Forms.TextBox txtDocumentName;
            private System.Windows.Forms.Panel pnlReceivedFaxDeatilsHeader;
            private System.Windows.Forms.Label label35;
            private System.Windows.Forms.Label label40;
            private System.Windows.Forms.Button btn_Up;
            private System.Windows.Forms.Button btn_Down;
            private System.Windows.Forms.Panel pnlReceivedFaxDetailsBody;
            private System.Windows.Forms.Label label42;
            private System.Windows.Forms.Label label33;
            private System.Windows.Forms.Label label31;
            private System.Windows.Forms.Label label30;
            private System.Windows.Forms.Label label27;
            private System.Windows.Forms.Label label28;
            private System.Windows.Forms.Label label29;
            //private PegasusImaging.WinForms.ImagXpress9.ImageXView imageXView;
            //private PegasusImaging.WinForms.ImagXpress9.ImagXpress imagXpress1;
            private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
            private System.Windows.Forms.ToolStripButton ZoomHeightButton;
            private System.Windows.Forms.ToolStripButton ZoomFitButton;
            private System.Windows.Forms.ToolStripButton ZoomWidthButton;
            private System.Windows.Forms.Panel pnlAcquiredImagesHeader;
            private System.Windows.Forms.Label Label5;
            private System.Windows.Forms.Label label43;
            private System.Windows.Forms.Label label44;
            private System.Windows.Forms.Label label49;
            private System.Windows.Forms.Panel panel5;
            private System.Windows.Forms.Label label34;
            private System.Windows.Forms.Label label50;
            private System.Windows.Forms.Label label51;
            private System.Windows.Forms.Label label55;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.Panel pnlSmallStrip;
            private System.Windows.Forms.Panel panel7;
            private System.Windows.Forms.Label label17;
            private System.Windows.Forms.Label label18;
            private System.Windows.Forms.Label label19;
            private System.Windows.Forms.Label label20;
            private System.Windows.Forms.Panel panel9;
            private System.Windows.Forms.Label label9;
            private System.Windows.Forms.Label label10;
            private System.Windows.Forms.Label label11;
            private System.Windows.Forms.Label label12;
            private System.Windows.Forms.Label label21;
            private System.Windows.Forms.Label label26;
            private System.Windows.Forms.Label label56;
            private System.Windows.Forms.Label label57;
            private System.Windows.Forms.Label label67;
            private System.Windows.Forms.Label label66;
            private System.Windows.Forms.Label label22;
            private System.Windows.Forms.Label label23;
            private System.Windows.Forms.Label label24;
            private System.Windows.Forms.Label label25;
            private System.Windows.Forms.ToolStripButton tlb_ExportToPDF;
            private System.Windows.Forms.Button btn_Expand;
            private System.Windows.Forms.Button btn_Collapse;
            internal System.Windows.Forms.Panel pnlPreviewCommand;
            internal System.Windows.Forms.Label lblPreviewStatus;
            internal System.Windows.Forms.Button btnLast;
            internal System.Windows.Forms.Button btnNext;
            internal System.Windows.Forms.Button btnPrevious;
            internal System.Windows.Forms.Button btnFirst;
            private System.Windows.Forms.Label label58;
            internal System.Windows.Forms.Panel pnlPreviewDMSDoc;
            private System.Windows.Forms.Label label61;
            private System.Windows.Forms.Label label59;
            private System.Windows.Forms.Label label60;
            private System.Windows.Forms.Label label62;
            private System.Windows.Forms.ToolStripButton tlb_SendToRCM;
            private System.Windows.Forms.Timer tmrprint;
            private gloScanImaging.ImageControl imageControl1;
        }
    }
