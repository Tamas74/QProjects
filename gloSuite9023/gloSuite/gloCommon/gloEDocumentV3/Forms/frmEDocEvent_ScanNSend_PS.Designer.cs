
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_ScanNSend_PS
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

                    ReleaseScanTimer();
                    //if (myWatcher != null && bReleaseClipboardEvents == false)
                    //{
                    //    try
                    //    {
                    //        myWatcher.OnClipboardContentChanged -= new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                    //        bReleaseClipboardEvents = true;
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                    //if (ScanTimer != null)
                    //{
                    //    ScanTimer.Tick -= ScanTimerFired;
                    //    ScanTimer.Dispose();
                    //    ScanTimer = null;
                    //}

                    if (twainDevice != null)
                    {
                        twainDevice.Dispose();
                        twainDevice = null;
                    }
                    components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_ScanNSend_PS));
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_BWFront = new System.Windows.Forms.ToolStripButton();
            this.tlb_ColorFront = new System.Windows.Forms.ToolStripButton();
            this.tlb_GrayFront = new System.Windows.Forms.ToolStripButton();
            this.tlb_BWDuplex = new System.Windows.Forms.ToolStripButton();
            this.tlb_ColorDuplex = new System.Windows.Forms.ToolStripButton();
            this.tlb_GrayDuplex = new System.Windows.Forms.ToolStripButton();
            this.tlb_SaveSetting = new System.Windows.Forms.ToolStripButton();
            this.tlb_CloseSetting = new System.Windows.Forms.ToolStripButton();
            this.tlb_Scan = new System.Windows.Forms.ToolStripButton();
            this.tls_btnScanCard = new System.Windows.Forms.ToolStripButton();
            this.tls_btnScanHalf = new System.Windows.Forms.ToolStripButton();
            this.tlb_Remove = new System.Windows.Forms.ToolStripButton();
            this.tlb_RemoveAll = new System.Windows.Forms.ToolStripButton();
            this.tlb_LoadImages = new System.Windows.Forms.ToolStripButton();
            this.tlb_RotateBack = new System.Windows.Forms.ToolStripButton();
            this.tlb_RotateForward = new System.Windows.Forms.ToolStripButton();
            this.tlb_Settings = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.txtCardWidth = new System.Windows.Forms.TextBox();
            this.txtCardLength = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbContrast = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbBrightness = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkShowScannerDialog = new System.Windows.Forms.CheckBox();
            this.cmbScanMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbScanSide = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbScanner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlScanDocument = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.imageControl1 = new gloScanImaging.ImageControl();
            this.label29 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pnlPreviewHeader = new System.Windows.Forms.Panel();
            this.chkUseCompression = new System.Windows.Forms.CheckBox();
            this.chkSplitFile = new System.Windows.Forms.CheckBox();
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.ZoomHeightButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomFitButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomWidthButton = new System.Windows.Forms.ToolStripButton();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.btnRegionZoom = new System.Windows.Forms.ToolStripButton();
            this.btnZoomingOut = new System.Windows.Forms.ToolStripButton();
            this.cmbZoom = new System.Windows.Forms.ToolStripComboBox();
            this.btnZoomingIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlSmallStrip = new System.Windows.Forms.Panel();
            this.pnlSmallStripMain = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_SmallStrip_btn_Document = new System.Windows.Forms.ToolStripButton();
            this.btn_Right = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlDocumentNameAcquiredImages = new System.Windows.Forms.Panel();
            this.pnlAcquiredImages = new System.Windows.Forms.Panel();
            this.c1Documents = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.btn_Left = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlDocumentName = new System.Windows.Forms.Panel();
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.twainPro1 = new PegasusImaging.WinForms.TwainPro5.TwainPro(this.components);
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlSetting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlScanDocument.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlPreviewHeader.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.pnlProgressBar.SuspendLayout();
            this.pnlSmallStrip.SuspendLayout();
            this.pnlSmallStripMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlDocumentNameAcquiredImages.SuspendLayout();
            this.pnlAcquiredImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).BeginInit();
            this.panel8.SuspendLayout();
            this.pnlDocumentName.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_BWFront,
            this.tlb_ColorFront,
            this.tlb_GrayFront,
            this.tlb_BWDuplex,
            this.tlb_ColorDuplex,
            this.tlb_GrayDuplex,
            this.tlb_SaveSetting,
            this.tlb_CloseSetting,
            this.tlb_Scan,
            this.tls_btnScanCard,
            this.tls_btnScanHalf,
            this.tlb_Remove,
            this.tlb_RemoveAll,
            this.tlb_LoadImages,
            this.tlb_RotateBack,
            this.tlb_RotateForward,
            this.tlb_Settings,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(1122, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.BackColor = System.Drawing.Color.Transparent;
            this.tlb_Ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.BackgroundImage")));
            this.tlb_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "&Save&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_BWFront
            // 
            this.tlb_BWFront.BackColor = System.Drawing.Color.Transparent;
            this.tlb_BWFront.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_BWFront.BackgroundImage")));
            this.tlb_BWFront.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_BWFront.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_BWFront.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_BWFront.Image = ((System.Drawing.Image)(resources.GetObject("tlb_BWFront.Image")));
            this.tlb_BWFront.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_BWFront.Name = "tlb_BWFront";
            this.tlb_BWFront.Size = new System.Drawing.Size(75, 50);
            this.tlb_BWFront.Tag = "BWFront";
            this.tlb_BWFront.Text = "&B/W Front";
            this.tlb_BWFront.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_BWFront.ToolTipText = "Black and White Front";
            this.tlb_BWFront.Click += new System.EventHandler(this.tlb_BWFront_Click);
            // 
            // tlb_ColorFront
            // 
            this.tlb_ColorFront.BackColor = System.Drawing.Color.Transparent;
            this.tlb_ColorFront.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_ColorFront.BackgroundImage")));
            this.tlb_ColorFront.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_ColorFront.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ColorFront.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_ColorFront.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ColorFront.Image")));
            this.tlb_ColorFront.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ColorFront.Name = "tlb_ColorFront";
            this.tlb_ColorFront.Size = new System.Drawing.Size(80, 50);
            this.tlb_ColorFront.Tag = "ColorFront";
            this.tlb_ColorFront.Text = "Color &Front";
            this.tlb_ColorFront.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ColorFront.ToolTipText = "Color Front";
            this.tlb_ColorFront.Click += new System.EventHandler(this.tlb_ColorFront_Click);
            // 
            // tlb_GrayFront
            // 
            this.tlb_GrayFront.BackColor = System.Drawing.Color.Transparent;
            this.tlb_GrayFront.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_GrayFront.BackgroundImage")));
            this.tlb_GrayFront.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_GrayFront.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_GrayFront.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_GrayFront.Image = ((System.Drawing.Image)(resources.GetObject("tlb_GrayFront.Image")));
            this.tlb_GrayFront.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_GrayFront.Name = "tlb_GrayFront";
            this.tlb_GrayFront.Size = new System.Drawing.Size(75, 50);
            this.tlb_GrayFront.Tag = "Gray Front";
            this.tlb_GrayFront.Text = "Gray Front";
            this.tlb_GrayFront.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_GrayFront.ToolTipText = "Gray Front";
            this.tlb_GrayFront.Click += new System.EventHandler(this.tlb_GrayFront_Click);
            // 
            // tlb_BWDuplex
            // 
            this.tlb_BWDuplex.BackColor = System.Drawing.Color.Transparent;
            this.tlb_BWDuplex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_BWDuplex.BackgroundImage")));
            this.tlb_BWDuplex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_BWDuplex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_BWDuplex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_BWDuplex.Image = ((System.Drawing.Image)(resources.GetObject("tlb_BWDuplex.Image")));
            this.tlb_BWDuplex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_BWDuplex.Name = "tlb_BWDuplex";
            this.tlb_BWDuplex.Size = new System.Drawing.Size(84, 50);
            this.tlb_BWDuplex.Tag = "BWDuplex";
            this.tlb_BWDuplex.Text = "B/W D&uplex";
            this.tlb_BWDuplex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_BWDuplex.ToolTipText = "Black and White Duplex";
            this.tlb_BWDuplex.Click += new System.EventHandler(this.tlb_BWDuplex_Click);
            // 
            // tlb_ColorDuplex
            // 
            this.tlb_ColorDuplex.BackColor = System.Drawing.Color.Transparent;
            this.tlb_ColorDuplex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_ColorDuplex.BackgroundImage")));
            this.tlb_ColorDuplex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_ColorDuplex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ColorDuplex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_ColorDuplex.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ColorDuplex.Image")));
            this.tlb_ColorDuplex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ColorDuplex.Name = "tlb_ColorDuplex";
            this.tlb_ColorDuplex.Size = new System.Drawing.Size(89, 50);
            this.tlb_ColorDuplex.Tag = "ColorDuplex";
            this.tlb_ColorDuplex.Text = "Color Duple&x";
            this.tlb_ColorDuplex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ColorDuplex.ToolTipText = "Color Duplex";
            this.tlb_ColorDuplex.Click += new System.EventHandler(this.tlb_ColorDuplex_Click);
            // 
            // tlb_GrayDuplex
            // 
            this.tlb_GrayDuplex.BackColor = System.Drawing.Color.Transparent;
            this.tlb_GrayDuplex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_GrayDuplex.BackgroundImage")));
            this.tlb_GrayDuplex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_GrayDuplex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_GrayDuplex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_GrayDuplex.Image = ((System.Drawing.Image)(resources.GetObject("tlb_GrayDuplex.Image")));
            this.tlb_GrayDuplex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_GrayDuplex.Name = "tlb_GrayDuplex";
            this.tlb_GrayDuplex.Size = new System.Drawing.Size(84, 50);
            this.tlb_GrayDuplex.Tag = "Gray Duplex";
            this.tlb_GrayDuplex.Text = "Gray Duplex";
            this.tlb_GrayDuplex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_GrayDuplex.ToolTipText = "Gray Duplex";
            this.tlb_GrayDuplex.Click += new System.EventHandler(this.tlb_GrayDuplex_Click);
            // 
            // tlb_SaveSetting
            // 
            this.tlb_SaveSetting.BackColor = System.Drawing.Color.Transparent;
            this.tlb_SaveSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_SaveSetting.BackgroundImage")));
            this.tlb_SaveSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_SaveSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SaveSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SaveSetting.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SaveSetting.Image")));
            this.tlb_SaveSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SaveSetting.Name = "tlb_SaveSetting";
            this.tlb_SaveSetting.Size = new System.Drawing.Size(66, 50);
            this.tlb_SaveSetting.Tag = "Save";
            this.tlb_SaveSetting.Text = "&Save&&Cls";
            this.tlb_SaveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SaveSetting.ToolTipText = "Save and Close";
            this.tlb_SaveSetting.Click += new System.EventHandler(this.tlb_SaveSetting_Click);
            // 
            // tlb_CloseSetting
            // 
            this.tlb_CloseSetting.BackColor = System.Drawing.Color.Transparent;
            this.tlb_CloseSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_CloseSetting.BackgroundImage")));
            this.tlb_CloseSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_CloseSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_CloseSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_CloseSetting.Image = ((System.Drawing.Image)(resources.GetObject("tlb_CloseSetting.Image")));
            this.tlb_CloseSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_CloseSetting.Name = "tlb_CloseSetting";
            this.tlb_CloseSetting.Size = new System.Drawing.Size(47, 50);
            this.tlb_CloseSetting.Tag = "Close";
            this.tlb_CloseSetting.Text = " &Close";
            this.tlb_CloseSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_CloseSetting.ToolTipText = "Close";
            this.tlb_CloseSetting.Click += new System.EventHandler(this.tlb_CloseSetting_Click);
            // 
            // tlb_Scan
            // 
            this.tlb_Scan.BackColor = System.Drawing.Color.Transparent;
            this.tlb_Scan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_Scan.BackgroundImage")));
            this.tlb_Scan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_Scan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Scan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Scan.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Scan.Image")));
            this.tlb_Scan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Scan.Name = "tlb_Scan";
            this.tlb_Scan.Size = new System.Drawing.Size(98, 50);
            this.tlb_Scan.Tag = "Scan";
            this.tlb_Scan.Text = "Scan &Full Page";
            this.tlb_Scan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Scan.ToolTipText = "Scan Full Page";
            this.tlb_Scan.Visible = false;
            this.tlb_Scan.Click += new System.EventHandler(this.tlb_Scan_Click);
            // 
            // tls_btnScanCard
            // 
            this.tls_btnScanCard.BackColor = System.Drawing.Color.Transparent;
            this.tls_btnScanCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnScanCard.BackgroundImage")));
            this.tls_btnScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnScanCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnScanCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnScanCard.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnScanCard.Image")));
            this.tls_btnScanCard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnScanCard.Name = "tls_btnScanCard";
            this.tls_btnScanCard.Size = new System.Drawing.Size(72, 50);
            this.tls_btnScanCard.Tag = "Scan";
            this.tls_btnScanCard.Text = "Sca&n Card";
            this.tls_btnScanCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnScanCard.ToolTipText = "Scan Card";
            this.tls_btnScanCard.Click += new System.EventHandler(this.tls_btnScanCard_Click);
            // 
            // tls_btnScanHalf
            // 
            this.tls_btnScanHalf.BackColor = System.Drawing.Color.Transparent;
            this.tls_btnScanHalf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnScanHalf.BackgroundImage")));
            this.tls_btnScanHalf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnScanHalf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnScanHalf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnScanHalf.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnScanHalf.Image")));
            this.tls_btnScanHalf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnScanHalf.Name = "tls_btnScanHalf";
            this.tls_btnScanHalf.Size = new System.Drawing.Size(102, 50);
            this.tls_btnScanHalf.Tag = "Scan";
            this.tls_btnScanHalf.Text = "Scan &Half Page";
            this.tls_btnScanHalf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnScanHalf.ToolTipText = "Scan Half Page";
            this.tls_btnScanHalf.Visible = false;
            this.tls_btnScanHalf.Click += new System.EventHandler(this.tls_btnScanHalf_Click);
            // 
            // tlb_Remove
            // 
            this.tlb_Remove.BackColor = System.Drawing.Color.Transparent;
            this.tlb_Remove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_Remove.BackgroundImage")));
            this.tlb_Remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_Remove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Remove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Remove.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Remove.Image")));
            this.tlb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Remove.Name = "tlb_Remove";
            this.tlb_Remove.Size = new System.Drawing.Size(60, 50);
            this.tlb_Remove.Tag = "Remove";
            this.tlb_Remove.Text = "Re&move";
            this.tlb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Remove.ToolTipText = "Remove";
            this.tlb_Remove.Click += new System.EventHandler(this.tlb_Remove_Click);
            // 
            // tlb_RemoveAll
            // 
            this.tlb_RemoveAll.BackColor = System.Drawing.Color.Transparent;
            this.tlb_RemoveAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_RemoveAll.BackgroundImage")));
            this.tlb_RemoveAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_RemoveAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RemoveAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_RemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RemoveAll.Image")));
            this.tlb_RemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RemoveAll.Name = "tlb_RemoveAll";
            this.tlb_RemoveAll.Size = new System.Drawing.Size(79, 50);
            this.tlb_RemoveAll.Tag = "RemoveAll";
            this.tlb_RemoveAll.Text = "&Rem&ove All";
            this.tlb_RemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RemoveAll.ToolTipText = "Remove All";
            this.tlb_RemoveAll.Click += new System.EventHandler(this.tlb_RemoveAll_Click);
            // 
            // tlb_LoadImages
            // 
            this.tlb_LoadImages.BackColor = System.Drawing.Color.Transparent;
            this.tlb_LoadImages.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_LoadImages.BackgroundImage")));
            this.tlb_LoadImages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_LoadImages.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_LoadImages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_LoadImages.Image = ((System.Drawing.Image)(resources.GetObject("tlb_LoadImages.Image")));
            this.tlb_LoadImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_LoadImages.Name = "tlb_LoadImages";
            this.tlb_LoadImages.Size = new System.Drawing.Size(89, 50);
            this.tlb_LoadImages.Tag = "LoadImages";
            this.tlb_LoadImages.Text = "Load &Images";
            this.tlb_LoadImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_LoadImages.ToolTipText = "Load Images";
            this.tlb_LoadImages.Click += new System.EventHandler(this.tlb_LoadImages_Click);
            // 
            // tlb_RotateBack
            // 
            this.tlb_RotateBack.BackColor = System.Drawing.Color.Transparent;
            this.tlb_RotateBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_RotateBack.BackgroundImage")));
            this.tlb_RotateBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_RotateBack.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RotateBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_RotateBack.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RotateBack.Image")));
            this.tlb_RotateBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RotateBack.Name = "tlb_RotateBack";
            this.tlb_RotateBack.Size = new System.Drawing.Size(86, 50);
            this.tlb_RotateBack.Tag = "BackRotate";
            this.tlb_RotateBack.Text = "Rotate CC&W";
            this.tlb_RotateBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RotateBack.ToolTipText = "Rotate Counterclockwise";
            this.tlb_RotateBack.Click += new System.EventHandler(this.tlb_RotateBack_Click);
            // 
            // tlb_RotateForward
            // 
            this.tlb_RotateForward.BackColor = System.Drawing.Color.Transparent;
            this.tlb_RotateForward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_RotateForward.BackgroundImage")));
            this.tlb_RotateForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_RotateForward.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RotateForward.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_RotateForward.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RotateForward.Image")));
            this.tlb_RotateForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RotateForward.Name = "tlb_RotateForward";
            this.tlb_RotateForward.Size = new System.Drawing.Size(78, 50);
            this.tlb_RotateForward.Tag = "ForwardRotate";
            this.tlb_RotateForward.Text = "Rotat&e CW";
            this.tlb_RotateForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RotateForward.ToolTipText = "Rotate Clockwise";
            this.tlb_RotateForward.Click += new System.EventHandler(this.tlb_RotateForward_Click);
            // 
            // tlb_Settings
            // 
            this.tlb_Settings.BackColor = System.Drawing.Color.Transparent;
            this.tlb_Settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_Settings.BackgroundImage")));
            this.tlb_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_Settings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Settings.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Settings.Image")));
            this.tlb_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Settings.Name = "tlb_Settings";
            this.tlb_Settings.Size = new System.Drawing.Size(67, 50);
            this.tlb_Settings.Tag = "Setting";
            this.tlb_Settings.Text = " Set&tings";
            this.tlb_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Settings.ToolTipText = "Settings";
            this.tlb_Settings.Click += new System.EventHandler(this.tlb_Settings_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.tlb_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.BackgroundImage")));
            this.tlb_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
            this.pnlSetting.Controls.Add(this.txtCardWidth);
            this.pnlSetting.Controls.Add(this.txtCardLength);
            this.pnlSetting.Controls.Add(this.label35);
            this.pnlSetting.Controls.Add(this.label40);
            this.pnlSetting.Controls.Add(this.label42);
            this.pnlSetting.Controls.Add(this.label44);
            this.pnlSetting.Controls.Add(this.label49);
            this.pnlSetting.Controls.Add(this.cmbContrast);
            this.pnlSetting.Controls.Add(this.label18);
            this.pnlSetting.Controls.Add(this.cmbBrightness);
            this.pnlSetting.Controls.Add(this.label17);
            this.pnlSetting.Controls.Add(this.panel1);
            this.pnlSetting.Controls.Add(this.label14);
            this.pnlSetting.Controls.Add(this.label13);
            this.pnlSetting.Controls.Add(this.label8);
            this.pnlSetting.Controls.Add(this.label7);
            this.pnlSetting.Controls.Add(this.chkShowScannerDialog);
            this.pnlSetting.Controls.Add(this.cmbScanMode);
            this.pnlSetting.Controls.Add(this.label4);
            this.pnlSetting.Controls.Add(this.cmbScanSide);
            this.pnlSetting.Controls.Add(this.label3);
            this.pnlSetting.Controls.Add(this.cmbResolution);
            this.pnlSetting.Controls.Add(this.label2);
            this.pnlSetting.Controls.Add(this.cmbScanner);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetting.Location = new System.Drawing.Point(0, 54);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSetting.Size = new System.Drawing.Size(1122, 605);
            this.pnlSetting.TabIndex = 6;
            // 
            // txtCardWidth
            // 
            this.txtCardWidth.Location = new System.Drawing.Point(144, 316);
            this.txtCardWidth.Name = "txtCardWidth";
            this.txtCardWidth.ShortcutsEnabled = false;
            this.txtCardWidth.Size = new System.Drawing.Size(71, 22);
            this.txtCardWidth.TabIndex = 29;
            this.txtCardWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardWidth_KeyPress);
            this.txtCardWidth.Leave += new System.EventHandler(this.txtCardWidth_Leave);
            // 
            // txtCardLength
            // 
            this.txtCardLength.Location = new System.Drawing.Point(245, 316);
            this.txtCardLength.Name = "txtCardLength";
            this.txtCardLength.ShortcutsEnabled = false;
            this.txtCardLength.Size = new System.Drawing.Size(71, 22);
            this.txtCardLength.TabIndex = 30;
            this.txtCardLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardLength_KeyPress);
            this.txtCardLength.Leave += new System.EventHandler(this.txtCardLength_Leave);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(159, 340);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 11);
            this.label35.TabIndex = 34;
            this.label35.Text = "(Width)";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(262, 340);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(38, 11);
            this.label40.TabIndex = 35;
            this.label40.Text = "(Length)";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(319, 320);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 14);
            this.label42.TabIndex = 33;
            this.label42.Text = "(Inches)";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(223, 320);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(14, 14);
            this.label44.TabIndex = 31;
            this.label44.Text = "X";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(74, 319);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(64, 14);
            this.label49.TabIndex = 32;
            this.label49.Text = "Card Size :";
            // 
            // cmbContrast
            // 
            this.cmbContrast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContrast.FormattingEnabled = true;
            this.cmbContrast.Location = new System.Drawing.Point(144, 279);
            this.cmbContrast.Name = "cmbContrast";
            this.cmbContrast.Size = new System.Drawing.Size(227, 22);
            this.cmbContrast.TabIndex = 28;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(77, 283);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 14);
            this.label18.TabIndex = 27;
            this.label18.Text = "Contrast :";
            // 
            // cmbBrightness
            // 
            this.cmbBrightness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrightness.FormattingEnabled = true;
            this.cmbBrightness.Location = new System.Drawing.Point(144, 235);
            this.cmbBrightness.Name = "cmbBrightness";
            this.cmbBrightness.Size = new System.Drawing.Size(227, 22);
            this.cmbBrightness.TabIndex = 26;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(67, 239);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 14);
            this.label17.TabIndex = 25;
            this.label17.Text = "Brightness :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Blue2007;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 24);
            this.panel1.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1114, 23);
            this.label15.TabIndex = 2;
            this.label15.Text = "   Settings";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(0, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1114, 1);
            this.label16.TabIndex = 3;
            this.label16.Text = "label16";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 601);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1114, 1);
            this.label14.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(4, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1114, 1);
            this.label13.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 599);
            this.label8.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(1118, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 599);
            this.label7.TabIndex = 20;
            // 
            // chkShowScannerDialog
            // 
            this.chkShowScannerDialog.AutoSize = true;
            this.chkShowScannerDialog.Location = new System.Drawing.Point(144, 359);
            this.chkShowScannerDialog.Name = "chkShowScannerDialog";
            this.chkShowScannerDialog.Size = new System.Drawing.Size(141, 18);
            this.chkShowScannerDialog.TabIndex = 31;
            this.chkShowScannerDialog.Text = "Show Scanner Dialog";
            this.chkShowScannerDialog.UseVisualStyleBackColor = true;
            // 
            // cmbScanMode
            // 
            this.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanMode.FormattingEnabled = true;
            this.cmbScanMode.Location = new System.Drawing.Point(144, 195);
            this.cmbScanMode.Name = "cmbScanMode";
            this.cmbScanMode.Size = new System.Drawing.Size(227, 22);
            this.cmbScanMode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "Scan Mode :";
            // 
            // cmbScanSide
            // 
            this.cmbScanSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanSide.FormattingEnabled = true;
            this.cmbScanSide.Location = new System.Drawing.Point(144, 149);
            this.cmbScanSide.Name = "cmbScanSide";
            this.cmbScanSide.Size = new System.Drawing.Size(227, 22);
            this.cmbScanSide.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Scan Side :";
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(144, 103);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(227, 22);
            this.cmbResolution.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "Resolution :";
            // 
            // cmbScanner
            // 
            this.cmbScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanner.FormattingEnabled = true;
            this.cmbScanner.Location = new System.Drawing.Point(144, 57);
            this.cmbScanner.Name = "cmbScanner";
            this.cmbScanner.Size = new System.Drawing.Size(227, 22);
            this.cmbScanner.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scanner :";
            // 
            // pnlScanDocument
            // 
            this.pnlScanDocument.Controls.Add(this.panel2);
            this.pnlScanDocument.Controls.Add(this.splitter1);
            this.pnlScanDocument.Controls.Add(this.pnlSmallStrip);
            this.pnlScanDocument.Controls.Add(this.pnlDocumentNameAcquiredImages);
            this.pnlScanDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScanDocument.Location = new System.Drawing.Point(0, 54);
            this.pnlScanDocument.Name = "pnlScanDocument";
            this.pnlScanDocument.Size = new System.Drawing.Size(1122, 605);
            this.pnlScanDocument.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(320, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(802, 605);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.pnlPreview);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.pnlProgressBar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(802, 605);
            this.panel3.TabIndex = 9;
            // 
            // pnlPreview
            // 
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.AutoSize = true;
            this.pnlPreview.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreview.Controls.Add(this.panel4);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 30);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlPreview.Size = new System.Drawing.Size(802, 543);
            this.pnlPreview.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.imageControl1);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(799, 540);
            this.panel4.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label26.Location = new System.Drawing.Point(1, 539);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(797, 1);
            this.label26.TabIndex = 32;
            this.label26.Text = "label2";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 539);
            this.label27.TabIndex = 31;
            this.label27.Text = "label4";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(798, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 539);
            this.label28.TabIndex = 30;
            this.label28.Text = "label3";
            // 
            // imageControl1
            // 
            this.imageControl1.AutoScroll = true;
            this.imageControl1.CurrImage = null;
            this.imageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageControl1.ForeColor = System.Drawing.SystemColors.Control;
            this.imageControl1.ImgPath = null;
            this.imageControl1.Location = new System.Drawing.Point(0, 1);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(799, 539);
            this.imageControl1.TabIndex = 10;
            this.imageControl1.SizeChanged += new System.EventHandler(this.imageControl1_SizeChanged);
            this.imageControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageControl1_MouseClick);
            this.imageControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageControl1_MouseDown);
            this.imageControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageControl1_MouseUp);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(799, 1);
            this.label29.TabIndex = 29;
            this.label29.Text = "label1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlPreviewHeader);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel7.Size = new System.Drawing.Size(802, 30);
            this.panel7.TabIndex = 13;
            // 
            // pnlPreviewHeader
            // 
            this.pnlPreviewHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewHeader.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.pnlPreviewHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPreviewHeader.Controls.Add(this.chkUseCompression);
            this.pnlPreviewHeader.Controls.Add(this.chkSplitFile);
            this.pnlPreviewHeader.Controls.Add(this.toolStrip2);
            this.pnlPreviewHeader.Controls.Add(this.btnZoomOut);
            this.pnlPreviewHeader.Controls.Add(this.btnZoomIn);
            this.pnlPreviewHeader.Controls.Add(this.label20);
            this.pnlPreviewHeader.Controls.Add(this.button1);
            this.pnlPreviewHeader.Controls.Add(this.button2);
            this.pnlPreviewHeader.Controls.Add(this.label6);
            this.pnlPreviewHeader.Controls.Add(this.label22);
            this.pnlPreviewHeader.Controls.Add(this.label24);
            this.pnlPreviewHeader.Controls.Add(this.label25);
            this.pnlPreviewHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlPreviewHeader.Name = "pnlPreviewHeader";
            this.pnlPreviewHeader.Size = new System.Drawing.Size(799, 24);
            this.pnlPreviewHeader.TabIndex = 4;
            // 
            // chkUseCompression
            // 
            this.chkUseCompression.AutoSize = true;
            this.chkUseCompression.BackColor = System.Drawing.Color.Transparent;
            this.chkUseCompression.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkUseCompression.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseCompression.Location = new System.Drawing.Point(145, 1);
            this.chkUseCompression.Name = "chkUseCompression";
            this.chkUseCompression.Size = new System.Drawing.Size(129, 22);
            this.chkUseCompression.TabIndex = 29;
            this.chkUseCompression.Text = "Use Compression";
            this.chkUseCompression.UseVisualStyleBackColor = false;
            this.chkUseCompression.Visible = false;
            // 
            // chkSplitFile
            // 
            this.chkSplitFile.AutoSize = true;
            this.chkSplitFile.BackColor = System.Drawing.Color.Transparent;
            this.chkSplitFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSplitFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSplitFile.Location = new System.Drawing.Point(83, 1);
            this.chkSplitFile.Name = "chkSplitFile";
            this.chkSplitFile.Size = new System.Drawing.Size(62, 22);
            this.chkSplitFile.TabIndex = 28;
            this.chkSplitFile.Text = "Split  ";
            this.chkSplitFile.UseVisualStyleBackColor = false;
            this.chkSplitFile.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomHeightButton,
            this.ZoomFitButton,
            this.ZoomWidthButton,
            this.btnPan,
            this.btnRegionZoom,
            this.btnZoomingOut,
            this.cmbZoom,
            this.btnZoomingIn});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(467, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(241, 22);
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
            this.ZoomHeightButton.Size = new System.Drawing.Size(23, 19);
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
            this.ZoomFitButton.Size = new System.Drawing.Size(23, 19);
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
            this.ZoomWidthButton.Size = new System.Drawing.Size(23, 19);
            this.ZoomWidthButton.Text = "toolStripButton2";
            this.ZoomWidthButton.ToolTipText = "Fit to Width";
            this.ZoomWidthButton.Click += new System.EventHandler(this.ZoomWidthButton_Click);
            // 
            // btnPan
            // 
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = ((System.Drawing.Image)(resources.GetObject("btnPan.Image")));
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(23, 19);
            this.btnPan.Tag = "PAN";
            this.btnPan.Text = "Pan Image";
            this.btnPan.Visible = false;
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnRegionZoom
            // 
            this.btnRegionZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRegionZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnRegionZoom.Image")));
            this.btnRegionZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRegionZoom.Name = "btnRegionZoom";
            this.btnRegionZoom.Size = new System.Drawing.Size(23, 19);
            this.btnRegionZoom.Tag = "REGIONSELECTION";
            this.btnRegionZoom.Text = "Select Zoom an Image";
            this.btnRegionZoom.Visible = false;
            this.btnRegionZoom.Click += new System.EventHandler(this.btnRegionZoom_Click);
            // 
            // btnZoomingOut
            // 
            this.btnZoomingOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomingOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomingOut.Image")));
            this.btnZoomingOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomingOut.Name = "btnZoomingOut";
            this.btnZoomingOut.Size = new System.Drawing.Size(23, 19);
            this.btnZoomingOut.Tag = "ZOOMOUT";
            this.btnZoomingOut.Text = "Zoom Out Image";
            this.btnZoomingOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // cmbZoom
            // 
            this.cmbZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoom.Items.AddRange(new object[] {
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
            this.cmbZoom.Name = "cmbZoom";
            this.cmbZoom.Size = new System.Drawing.Size(121, 22);
            this.cmbZoom.SelectedIndexChanged += new System.EventHandler(this.cmbZoom_SelectedIndexChanged);
            // 
            // btnZoomingIn
            // 
            this.btnZoomingIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomingIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomingIn.Image")));
            this.btnZoomingIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomingIn.Name = "btnZoomingIn";
            this.btnZoomingIn.Size = new System.Drawing.Size(23, 19);
            this.btnZoomingIn.Tag = "ZOOMIN";
            this.btnZoomingIn.Text = "Zoom In Image";
            this.btnZoomingIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Location = new System.Drawing.Point(708, 1);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(24, 22);
            this.btnZoomOut.TabIndex = 8;
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Visible = false;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseLeave += new System.EventHandler(this.btnZoomOut_MouseLeave);
            this.btnZoomOut.MouseHover += new System.EventHandler(this.btnZoomOut_MouseHover);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Location = new System.Drawing.Point(732, 1);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(18, 22);
            this.btnZoomIn.TabIndex = 7;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Visible = false;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomIn.MouseLeave += new System.EventHandler(this.btnZoomIn_MouseLeave);
            this.btnZoomIn.MouseHover += new System.EventHandler(this.btnZoomIn_MouseHover);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 22);
            this.label20.TabIndex = 2;
            this.label20.Text = "   Preview";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(750, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 22);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(774, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 22);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(1, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(797, 1);
            this.label6.TabIndex = 33;
            this.label6.Text = "label2";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 23);
            this.label22.TabIndex = 32;
            this.label22.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(798, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 23);
            this.label24.TabIndex = 31;
            this.label24.Text = "label3";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(799, 1);
            this.label25.TabIndex = 30;
            this.label25.Text = "label1";
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProgressBar.Controls.Add(this.pbDocument);
            this.pnlProgressBar.Controls.Add(this.label12);
            this.pnlProgressBar.Controls.Add(this.label11);
            this.pnlProgressBar.Controls.Add(this.label10);
            this.pnlProgressBar.Controls.Add(this.label9);
            this.pnlProgressBar.Controls.Add(this.label19);
            this.pnlProgressBar.Controls.Add(this.label30);
            this.pnlProgressBar.Controls.Add(this.label31);
            this.pnlProgressBar.Controls.Add(this.label33);
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 573);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlProgressBar.Size = new System.Drawing.Size(802, 32);
            this.pnlProgressBar.TabIndex = 10;
            // 
            // pbDocument
            // 
            this.pbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDocument.Location = new System.Drawing.Point(10, 1);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(779, 22);
            this.pbDocument.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(789, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(9, 22);
            this.label12.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(1, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(9, 22);
            this.label11.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(797, 5);
            this.label10.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(1, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(797, 27);
            this.label9.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(1, 28);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(797, 1);
            this.label19.TabIndex = 15;
            this.label19.Text = "label2";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(0, 1);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 28);
            this.label30.TabIndex = 14;
            this.label30.Text = "label4";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label31.Location = new System.Drawing.Point(798, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 28);
            this.label31.TabIndex = 13;
            this.label31.Text = "label3";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(799, 1);
            this.label33.TabIndex = 12;
            this.label33.Text = "label1";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(316, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 605);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // pnlSmallStrip
            // 
            this.pnlSmallStrip.Controls.Add(this.pnlSmallStripMain);
            this.pnlSmallStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSmallStrip.Location = new System.Drawing.Point(285, 0);
            this.pnlSmallStrip.Name = "pnlSmallStrip";
            this.pnlSmallStrip.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlSmallStrip.Size = new System.Drawing.Size(31, 605);
            this.pnlSmallStrip.TabIndex = 18;
            // 
            // pnlSmallStripMain
            // 
            this.pnlSmallStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSmallStripMain.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_VerticalBtnStrip;
            this.pnlSmallStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSmallStripMain.Controls.Add(this.toolStrip1);
            this.pnlSmallStripMain.Controls.Add(this.btn_Right);
            this.pnlSmallStripMain.Controls.Add(this.label5);
            this.pnlSmallStripMain.Controls.Add(this.label21);
            this.pnlSmallStripMain.Controls.Add(this.label53);
            this.pnlSmallStripMain.Controls.Add(this.label23);
            this.pnlSmallStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSmallStripMain.Location = new System.Drawing.Point(4, 3);
            this.pnlSmallStripMain.Name = "pnlSmallStripMain";
            this.pnlSmallStripMain.Size = new System.Drawing.Size(23, 599);
            this.pnlSmallStripMain.TabIndex = 15;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_SmallStrip_btn_Document});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(1, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(21, 574);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // ts_SmallStrip_btn_Document
            // 
            this.ts_SmallStrip_btn_Document.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Document.Image = ((System.Drawing.Image)(resources.GetObject("ts_SmallStrip_btn_Document.Image")));
            this.ts_SmallStrip_btn_Document.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Document.Name = "ts_SmallStrip_btn_Document";
            this.ts_SmallStrip_btn_Document.Size = new System.Drawing.Size(19, 129);
            this.ts_SmallStrip_btn_Document.Text = "Acquired Images";
            this.ts_SmallStrip_btn_Document.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_SmallStrip_btn_Document.Click += new System.EventHandler(this.btn_Right_Click);
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
            this.btn_Right.Size = new System.Drawing.Size(21, 23);
            this.btn_Right.TabIndex = 16;
            this.btn_Right.UseVisualStyleBackColor = false;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            this.btn_Right.MouseLeave += new System.EventHandler(this.btn_Right_MouseLeave);
            this.btn_Right.MouseHover += new System.EventHandler(this.btn_Right_MouseHover);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 1);
            this.label5.TabIndex = 20;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 598);
            this.label21.TabIndex = 9;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(22, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 598);
            this.label53.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(0, 598);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 1);
            this.label23.TabIndex = 17;
            // 
            // pnlDocumentNameAcquiredImages
            // 
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlAcquiredImages);
            this.pnlDocumentNameAcquiredImages.Controls.Add(this.pnlDocumentName);
            this.pnlDocumentNameAcquiredImages.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDocumentNameAcquiredImages.Location = new System.Drawing.Point(0, 0);
            this.pnlDocumentNameAcquiredImages.Name = "pnlDocumentNameAcquiredImages";
            this.pnlDocumentNameAcquiredImages.Size = new System.Drawing.Size(285, 605);
            this.pnlDocumentNameAcquiredImages.TabIndex = 0;
            // 
            // pnlAcquiredImages
            // 
            this.pnlAcquiredImages.Controls.Add(this.c1Documents);
            this.pnlAcquiredImages.Controls.Add(this.panel8);
            this.pnlAcquiredImages.Controls.Add(this.label45);
            this.pnlAcquiredImages.Controls.Add(this.label46);
            this.pnlAcquiredImages.Controls.Add(this.label47);
            this.pnlAcquiredImages.Controls.Add(this.label48);
            this.pnlAcquiredImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAcquiredImages.Location = new System.Drawing.Point(0, 76);
            this.pnlAcquiredImages.Name = "pnlAcquiredImages";
            this.pnlAcquiredImages.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlAcquiredImages.Size = new System.Drawing.Size(285, 529);
            this.pnlAcquiredImages.TabIndex = 10;
            // 
            // c1Documents
            // 
            this.c1Documents.BackColor = System.Drawing.Color.White;
            this.c1Documents.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Documents.ColumnInfo = "3,0,0,0,0,105,Columns:";
            this.c1Documents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Documents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Documents.Location = new System.Drawing.Point(4, 25);
            this.c1Documents.Name = "c1Documents";
            this.c1Documents.Rows.DefaultSize = 21;
            this.c1Documents.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Documents.Size = new System.Drawing.Size(280, 500);
            this.c1Documents.StyleInfo = resources.GetString("c1Documents.StyleInfo");
            this.c1Documents.TabIndex = 9;
            this.c1Documents.Tree.NodeImageCollapsed = global::gloEDocumentV3.Properties.Resources.Plus;
            this.c1Documents.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Documents.Tree.NodeImageExpanded")));
            this.c1Documents.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1Documents.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
            this.c1Documents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Documents_MouseDown);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.btn_Left);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(4, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(280, 24);
            this.panel8.TabIndex = 4;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(256, 23);
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
            this.btn_Left.Location = new System.Drawing.Point(256, 0);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(24, 23);
            this.btn_Left.TabIndex = 0;
            this.btn_Left.UseVisualStyleBackColor = false;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            this.btn_Left.MouseLeave += new System.EventHandler(this.btn_Left_MouseLeave);
            this.btn_Left.MouseHover += new System.EventHandler(this.btn_Left_MouseHover);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(0, 23);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(280, 1);
            this.label43.TabIndex = 3;
            this.label43.Text = "label43";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Location = new System.Drawing.Point(3, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 524);
            this.label45.TabIndex = 3;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(284, 1);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 524);
            this.label46.TabIndex = 2;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Location = new System.Drawing.Point(3, 525);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(282, 1);
            this.label47.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(3, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(282, 1);
            this.label48.TabIndex = 0;
            // 
            // pnlDocumentName
            // 
            this.pnlDocumentName.Controls.Add(this.txtDocumentName);
            this.pnlDocumentName.Controls.Add(this.panel6);
            this.pnlDocumentName.Controls.Add(this.label36);
            this.pnlDocumentName.Controls.Add(this.label37);
            this.pnlDocumentName.Controls.Add(this.label38);
            this.pnlDocumentName.Controls.Add(this.label39);
            this.pnlDocumentName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDocumentName.Location = new System.Drawing.Point(0, 0);
            this.pnlDocumentName.Name = "pnlDocumentName";
            this.pnlDocumentName.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlDocumentName.Size = new System.Drawing.Size(285, 76);
            this.pnlDocumentName.TabIndex = 9;
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(12, 39);
            this.txtDocumentName.MaxLength = 150;
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(205, 22);
            this.txtDocumentName.TabIndex = 5;
            this.txtDocumentName.TextChanged += new System.EventHandler(this.txtDocumentName_TextChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(280, 24);
            this.panel6.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(280, 23);
            this.label32.TabIndex = 2;
            this.label32.Text = "  eDocument Name";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(0, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(280, 1);
            this.label34.TabIndex = 3;
            this.label34.Text = "label34";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(3, 4);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 68);
            this.label36.TabIndex = 3;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Location = new System.Drawing.Point(284, 4);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 68);
            this.label37.TabIndex = 2;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Location = new System.Drawing.Point(3, 72);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(282, 1);
            this.label38.TabIndex = 1;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Location = new System.Drawing.Point(3, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(282, 1);
            this.label39.TabIndex = 0;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolstrip.Controls.Add(this.tls_MaintainDoc);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(1122, 54);
            this.pnlToolstrip.TabIndex = 0;
            // 
            // frmEDocEvent_ScanNSend_PS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1122, 659);
            this.Controls.Add(this.pnlScanDocument);
            this.Controls.Add(this.pnlSetting);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_ScanNSend_PS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEDocEvent_ScanNSend_PS_FormClosing);
            this.Load += new System.EventHandler(this.frmEDocEvent_ScanNSend_PS_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlScanDocument.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.pnlPreviewHeader.ResumeLayout(false);
            this.pnlPreviewHeader.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.pnlProgressBar.ResumeLayout(false);
            this.pnlSmallStrip.ResumeLayout(false);
            this.pnlSmallStripMain.ResumeLayout(false);
            this.pnlSmallStripMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlDocumentNameAcquiredImages.ResumeLayout(false);
            this.pnlAcquiredImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).EndInit();
            this.panel8.ResumeLayout(false);
            this.pnlDocumentName.ResumeLayout(false);
            this.pnlDocumentName.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ToolStripButton tlb_Scan;
            private System.Windows.Forms.ToolStripButton tlb_Remove;
            private System.Windows.Forms.ToolStripButton tlb_RemoveAll;
            private System.Windows.Forms.ToolStripButton tlb_LoadImages;
            private System.Windows.Forms.ToolStripButton tlb_RotateBack;
            private System.Windows.Forms.ToolStripButton tlb_RotateForward;
            private System.Windows.Forms.ToolStripButton tlb_Settings;
            private System.Windows.Forms.Panel pnlSetting;
            private System.Windows.Forms.Panel pnlScanDocument;
            private System.Windows.Forms.Panel pnlToolstrip;
            private System.Windows.Forms.ToolStripButton tlb_SaveSetting;
            private System.Windows.Forms.ToolStripButton tlb_CloseSetting;
            private System.Windows.Forms.Panel pnlDocumentNameAcquiredImages;
            private System.Windows.Forms.Panel pnlAcquiredImages;
            private System.Windows.Forms.Panel panel8;
            private System.Windows.Forms.Label label41;
            private System.Windows.Forms.Button btn_Left;
            private System.Windows.Forms.Label label43;
            private System.Windows.Forms.Label label45;
            private System.Windows.Forms.Label label46;
            private System.Windows.Forms.Label label47;
            private System.Windows.Forms.Label label48;
            private System.Windows.Forms.Panel pnlDocumentName;
            private System.Windows.Forms.TextBox txtDocumentName;
            private System.Windows.Forms.Panel panel6;
            private System.Windows.Forms.Label label32;
            private System.Windows.Forms.Label label34;
            private System.Windows.Forms.Label label36;
            private System.Windows.Forms.Label label37;
            private System.Windows.Forms.Label label38;
            private System.Windows.Forms.Label label39;
            private C1.Win.C1FlexGrid.C1FlexGrid c1Documents;
            private System.Windows.Forms.CheckBox chkShowScannerDialog;
            private System.Windows.Forms.ComboBox cmbScanMode;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.ComboBox cmbScanSide;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.ComboBox cmbResolution;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.ComboBox cmbScanner;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label15;
            private System.Windows.Forms.Label label16;
            private System.Windows.Forms.Label label14;
            private System.Windows.Forms.Label label13;
            private System.Windows.Forms.Label label8;
            private System.Windows.Forms.Label label7;
            private PegasusImaging.WinForms.TwainPro5.TwainPro twainPro1;
            private System.Windows.Forms.ComboBox cmbContrast;
            private System.Windows.Forms.Label label18;
            private System.Windows.Forms.ComboBox cmbBrightness;
            private System.Windows.Forms.Label label17;
            //private PegasusImaging.WinForms.ImagXpress9.ImagXpress imagXpress1;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.Panel pnlPreview;
           // private PegasusImaging.WinForms.ImagXpress9.ImageXView imageXView;
            private System.Windows.Forms.Panel pnlProgressBar;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.Panel pnlPreviewHeader;
            private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
            private System.Windows.Forms.ToolStripButton ZoomHeightButton;
            private System.Windows.Forms.ToolStripButton ZoomFitButton;
            private System.Windows.Forms.ToolStripButton ZoomWidthButton;
            private System.Windows.Forms.Button btnZoomOut;
            //private System.Windows.Forms.ComboBox cmbZoomPercentage;
            private System.Windows.Forms.Button btnZoomIn;
            private System.Windows.Forms.Label label20;
            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Button button2;
            private System.Windows.Forms.CheckBox chkUseCompression;
            private System.Windows.Forms.CheckBox chkSplitFile;
            private System.Windows.Forms.ToolStripButton tls_btnScanCard;
            private System.Windows.Forms.ToolStripButton tls_btnScanHalf;
            private System.Windows.Forms.Label label12;
            private System.Windows.Forms.Label label11;
            private System.Windows.Forms.Label label10;
            private System.Windows.Forms.Label label9;
            private System.Windows.Forms.Panel pnlSmallStrip;
            private System.Windows.Forms.Panel pnlSmallStripMain;
            private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
            private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Document;
            private System.Windows.Forms.Button btn_Right;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.Label label21;
            private System.Windows.Forms.Label label53;
            private System.Windows.Forms.Label label23;
            private System.Windows.Forms.Panel panel7;
            private System.Windows.Forms.Label label26;
            private System.Windows.Forms.Label label27;
            private System.Windows.Forms.Label label28;
            private System.Windows.Forms.Label label29;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label22;
            private System.Windows.Forms.Label label24;
            private System.Windows.Forms.Label label25;
            private System.Windows.Forms.Label label19;
            private System.Windows.Forms.Label label30;
            private System.Windows.Forms.Label label31;
            private System.Windows.Forms.Label label33;
            private System.Windows.Forms.Splitter splitter1;
            private System.Windows.Forms.Panel panel4;
            private System.Windows.Forms.TextBox txtCardWidth;
            private System.Windows.Forms.TextBox txtCardLength;
            private System.Windows.Forms.Label label35;
            private System.Windows.Forms.Label label40;
            private System.Windows.Forms.Label label42;
            private System.Windows.Forms.Label label44;
            private System.Windows.Forms.Label label49;
            private System.Windows.Forms.ToolStripButton tlb_BWFront;
            private System.Windows.Forms.ToolStripButton tlb_BWDuplex;
            private System.Windows.Forms.ToolStripButton tlb_ColorFront;
            private System.Windows.Forms.ToolStripButton tlb_ColorDuplex;
            private gloScanImaging.ImageControl imageControl1;
            private System.Windows.Forms.ToolStripButton btnPan;
            private System.Windows.Forms.ToolStripButton btnRegionZoom;
            private System.Windows.Forms.ToolStripButton btnZoomingOut;
            private System.Windows.Forms.ToolStripComboBox cmbZoom;
            private System.Windows.Forms.ToolStripButton btnZoomingIn;
            private System.Windows.Forms.ToolStripButton tlb_GrayFront;
            private System.Windows.Forms.ToolStripButton tlb_GrayDuplex;
        }
    }
