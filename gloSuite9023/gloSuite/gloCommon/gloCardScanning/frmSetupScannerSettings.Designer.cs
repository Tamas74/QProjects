namespace gloCardScanning
{
    partial class frmSetupScannerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupScannerSettings));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbCalibrate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chkUniformCardPrinting = new System.Windows.Forms.CheckBox();
            this.chkCenterImage = new System.Windows.Forms.CheckBox();
            this.chkBoxScanPhoto = new System.Windows.Forms.CheckBox();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lblColorScheme = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.cmbColorScheme = new System.Windows.Forms.ComboBox();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.chkboxDuplex = new System.Windows.Forms.CheckBox();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnlLicenseResolution = new System.Windows.Forms.Panel();
            this.rbLicense600 = new System.Windows.Forms.RadioButton();
            this.rbLicense300 = new System.Windows.Forms.RadioButton();
            this.pnlInsuranceResolution = new System.Windows.Forms.Panel();
            this.rbSR600 = new System.Windows.Forms.RadioButton();
            this.rdSR300 = new System.Windows.Forms.RadioButton();
            this.lblResolution = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPScanners = new System.Windows.Forms.ComboBox();
            this.c1ScannerProps = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.twainPro1 = new PegasusImaging.WinForms.TwainPro5.TwainPro(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkZipScannerSettings = new System.Windows.Forms.CheckBox();
            this.btnRefreshTwainScanners = new System.Windows.Forms.Button();
            this.chkEliminatePegasus = new System.Windows.Forms.CheckBox();
            this.btnRefreshRemoteScanner = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkEnableRemoteScanner = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlScannerSettings = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnDefaultSet = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLicenseResolution.SuspendLayout();
            this.pnlInsuranceResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ScannerProps)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlScannerSettings.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(556, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCalibrate,
            this.toolStripButton1,
            this.toolStripButton2});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(556, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.Text = "toolStrip1";
            this.tls_SetupResource.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SetupResource_ItemClicked);
            // 
            // tsbCalibrate
            // 
            this.tsbCalibrate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbCalibrate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbCalibrate.Image = ((System.Drawing.Image)(resources.GetObject("tsbCalibrate.Image")));
            this.tsbCalibrate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCalibrate.Name = "tsbCalibrate";
            this.tsbCalibrate.Size = new System.Drawing.Size(118, 50);
            this.tsbCalibrate.Tag = "Calibrate";
            this.tsbCalibrate.Text = "Calibrate &Scanner";
            this.tsbCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton1.Tag = "OK";
            this.toolStripButton1.Text = "Sa&ve&&Cls";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Save and Close";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.chkUniformCardPrinting);
            this.pnlMain.Controls.Add(this.chkCenterImage);
            this.pnlMain.Controls.Add(this.chkBoxScanPhoto);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lblColorScheme);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.cmbColorScheme);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.chkboxDuplex);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 198);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(556, 83);
            this.pnlMain.TabIndex = 1;
            // 
            // chkUniformCardPrinting
            // 
            this.chkUniformCardPrinting.AutoSize = true;
            this.chkUniformCardPrinting.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkUniformCardPrinting.Location = new System.Drawing.Point(358, 56);
            this.chkUniformCardPrinting.Name = "chkUniformCardPrinting";
            this.chkUniformCardPrinting.Size = new System.Drawing.Size(141, 18);
            this.chkUniformCardPrinting.TabIndex = 4;
            this.chkUniformCardPrinting.Text = "Uniform Card Printing";
            this.chkUniformCardPrinting.UseVisualStyleBackColor = true;
            // 
            // chkCenterImage
            // 
            this.chkCenterImage.AutoSize = true;
            this.chkCenterImage.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkCenterImage.Location = new System.Drawing.Point(358, 33);
            this.chkCenterImage.Name = "chkCenterImage";
            this.chkCenterImage.Size = new System.Drawing.Size(101, 18);
            this.chkCenterImage.TabIndex = 2;
            this.chkCenterImage.Text = "Center Image";
            this.chkCenterImage.UseVisualStyleBackColor = true;
            // 
            // chkBoxScanPhoto
            // 
            this.chkBoxScanPhoto.AutoSize = true;
            this.chkBoxScanPhoto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxScanPhoto.Location = new System.Drawing.Point(165, 56);
            this.chkBoxScanPhoto.Name = "chkBoxScanPhoto";
            this.chkBoxScanPhoto.Size = new System.Drawing.Size(93, 18);
            this.chkBoxScanPhoto.TabIndex = 3;
            this.chkBoxScanPhoto.Text = "Enable OCR ";
            this.chkBoxScanPhoto.UseVisualStyleBackColor = true;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 79);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(548, 1);
            this.lbl_BottomBrd.TabIndex = 16;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lblColorScheme
            // 
            this.lblColorScheme.AutoSize = true;
            this.lblColorScheme.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorScheme.Location = new System.Drawing.Point(24, 9);
            this.lblColorScheme.Name = "lblColorScheme";
            this.lblColorScheme.Size = new System.Drawing.Size(138, 14);
            this.lblColorScheme.TabIndex = 10;
            this.lblColorScheme.Text = "Scanner Color Scheme :";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 79);
            this.lbl_LeftBrd.TabIndex = 15;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // cmbColorScheme
            // 
            this.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorScheme.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbColorScheme.ForeColor = System.Drawing.Color.Black;
            this.cmbColorScheme.FormattingEnabled = true;
            this.cmbColorScheme.Location = new System.Drawing.Point(165, 5);
            this.cmbColorScheme.Name = "cmbColorScheme";
            this.cmbColorScheme.Size = new System.Drawing.Size(334, 22);
            this.cmbColorScheme.TabIndex = 0;
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(552, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 79);
            this.lbl_RightBrd.TabIndex = 14;
            this.lbl_RightBrd.Text = "label3";
            // 
            // chkboxDuplex
            // 
            this.chkboxDuplex.AutoSize = true;
            this.chkboxDuplex.Checked = true;
            this.chkboxDuplex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkboxDuplex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkboxDuplex.Location = new System.Drawing.Point(165, 33);
            this.chkboxDuplex.Name = "chkboxDuplex";
            this.chkboxDuplex.Size = new System.Drawing.Size(93, 18);
            this.chkboxDuplex.TabIndex = 1;
            this.chkboxDuplex.Text = "Scan Duplex";
            this.chkboxDuplex.UseVisualStyleBackColor = true;
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(550, 1);
            this.lbl_TopBrd.TabIndex = 13;
            this.lbl_TopBrd.Text = "label1";
            // 
            // pnlLicenseResolution
            // 
            this.pnlLicenseResolution.Controls.Add(this.rbLicense600);
            this.pnlLicenseResolution.Controls.Add(this.rbLicense300);
            this.pnlLicenseResolution.Location = new System.Drawing.Point(165, 108);
            this.pnlLicenseResolution.Name = "pnlLicenseResolution";
            this.pnlLicenseResolution.Size = new System.Drawing.Size(173, 28);
            this.pnlLicenseResolution.TabIndex = 6;
            // 
            // rbLicense600
            // 
            this.rbLicense600.AutoSize = true;
            this.rbLicense600.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLicense600.Location = new System.Drawing.Point(94, 4);
            this.rbLicense600.Name = "rbLicense600";
            this.rbLicense600.Size = new System.Drawing.Size(66, 18);
            this.rbLicense600.TabIndex = 1;
            this.rbLicense600.Text = "600 dpi";
            this.rbLicense600.UseVisualStyleBackColor = true;
            this.rbLicense600.CheckedChanged += new System.EventHandler(this.rbLicense600_CheckedChanged);
            // 
            // rbLicense300
            // 
            this.rbLicense300.AutoSize = true;
            this.rbLicense300.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLicense300.Location = new System.Drawing.Point(7, 5);
            this.rbLicense300.Name = "rbLicense300";
            this.rbLicense300.Size = new System.Drawing.Size(66, 18);
            this.rbLicense300.TabIndex = 0;
            this.rbLicense300.Text = "300 dpi";
            this.rbLicense300.UseVisualStyleBackColor = true;
            this.rbLicense300.CheckedChanged += new System.EventHandler(this.rbLicense300_CheckedChanged);
            // 
            // pnlInsuranceResolution
            // 
            this.pnlInsuranceResolution.Controls.Add(this.rbSR600);
            this.pnlInsuranceResolution.Controls.Add(this.rdSR300);
            this.pnlInsuranceResolution.Location = new System.Drawing.Point(165, 80);
            this.pnlInsuranceResolution.Name = "pnlInsuranceResolution";
            this.pnlInsuranceResolution.Size = new System.Drawing.Size(173, 28);
            this.pnlInsuranceResolution.TabIndex = 5;
            // 
            // rbSR600
            // 
            this.rbSR600.AutoSize = true;
            this.rbSR600.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSR600.Location = new System.Drawing.Point(93, 5);
            this.rbSR600.Name = "rbSR600";
            this.rbSR600.Size = new System.Drawing.Size(66, 18);
            this.rbSR600.TabIndex = 1;
            this.rbSR600.Text = "600 dpi";
            this.rbSR600.UseVisualStyleBackColor = true;
            this.rbSR600.CheckedChanged += new System.EventHandler(this.rbSR600_CheckedChanged);
            // 
            // rdSR300
            // 
            this.rdSR300.AutoSize = true;
            this.rdSR300.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSR300.Location = new System.Drawing.Point(6, 5);
            this.rdSR300.Name = "rdSR300";
            this.rdSR300.Size = new System.Drawing.Size(66, 18);
            this.rdSR300.TabIndex = 0;
            this.rdSR300.Text = "300 dpi";
            this.rdSR300.UseVisualStyleBackColor = true;
            this.rdSR300.CheckedChanged += new System.EventHandler(this.rdSR300_CheckedChanged);
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResolution.Location = new System.Drawing.Point(66, 88);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(96, 14);
            this.lblResolution.TabIndex = 7;
            this.lblResolution.Text = "Insurance Card :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "Driving License Card :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "Select Scanner :";
            // 
            // cmbPScanners
            // 
            this.cmbPScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPScanners.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPScanners.ForeColor = System.Drawing.Color.Black;
            this.cmbPScanners.FormattingEnabled = true;
            this.cmbPScanners.Location = new System.Drawing.Point(165, 10);
            this.cmbPScanners.Name = "cmbPScanners";
            this.cmbPScanners.Size = new System.Drawing.Size(334, 22);
            this.cmbPScanners.TabIndex = 0;
            this.cmbPScanners.SelectedIndexChanged += new System.EventHandler(this.cmbPScanners_SelectedIndexChanged);
            // 
            // c1ScannerProps
            // 
            this.c1ScannerProps.AllowEditing = false;
            this.c1ScannerProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ScannerProps.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ScannerProps.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1ScannerProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ScannerProps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ScannerProps.Location = new System.Drawing.Point(4, 35);
            this.c1ScannerProps.Name = "c1ScannerProps";
            this.c1ScannerProps.Rows.Count = 1;
            this.c1ScannerProps.Rows.DefaultSize = 21;
            this.c1ScannerProps.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ScannerProps.ShowCellLabels = true;
            this.c1ScannerProps.Size = new System.Drawing.Size(548, 113);
            this.c1ScannerProps.StyleInfo = resources.GetString("c1ScannerProps.StyleInfo");
            this.c1ScannerProps.TabIndex = 1;
            this.c1ScannerProps.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ScannerProps_StartEdit);
            this.c1ScannerProps.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ScannerProps_AfterEdit);
            this.c1ScannerProps.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ScannerProps_SetupEditor);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkZipScannerSettings);
            this.panel1.Controls.Add(this.btnRefreshTwainScanners);
            this.panel1.Controls.Add(this.chkEliminatePegasus);
            this.panel1.Controls.Add(this.btnRefreshRemoteScanner);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.chkEnableRemoteScanner);
            this.panel1.Controls.Add(this.pnlLicenseResolution);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pnlInsuranceResolution);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblResolution);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(556, 144);
            this.panel1.TabIndex = 0;
            // 
            // chkZipScannerSettings
            // 
            this.chkZipScannerSettings.AutoSize = true;
            this.chkZipScannerSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkZipScannerSettings.Location = new System.Drawing.Point(171, 58);
            this.chkZipScannerSettings.Name = "chkZipScannerSettings";
            this.chkZipScannerSettings.Size = new System.Drawing.Size(139, 18);
            this.chkZipScannerSettings.TabIndex = 4;
            this.chkZipScannerSettings.Tag = "Zip Scanner Settings";
            this.chkZipScannerSettings.Text = "Zip Scanner Settings";
            this.chkZipScannerSettings.UseVisualStyleBackColor = true;
            this.chkZipScannerSettings.CheckedChanged += new System.EventHandler(this.chkZipMetadata_CheckedChanged);
            // 
            // btnRefreshTwainScanners
            // 
            this.btnRefreshTwainScanners.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshTwainScanners.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongButton;
            this.btnRefreshTwainScanners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshTwainScanners.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshTwainScanners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshTwainScanners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshTwainScanners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshTwainScanners.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshTwainScanners.Image")));
            this.btnRefreshTwainScanners.Location = new System.Drawing.Point(462, 32);
            this.btnRefreshTwainScanners.Name = "btnRefreshTwainScanners";
            this.btnRefreshTwainScanners.Size = new System.Drawing.Size(28, 23);
            this.btnRefreshTwainScanners.TabIndex = 3;
            this.btnRefreshTwainScanners.UseVisualStyleBackColor = false;
            this.btnRefreshTwainScanners.Click += new System.EventHandler(this.btnRefreshTwainScanners_Click);
            // 
            // chkEliminatePegasus
            // 
            this.chkEliminatePegasus.AutoSize = true;
            this.chkEliminatePegasus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEliminatePegasus.Location = new System.Drawing.Point(364, 34);
            this.chkEliminatePegasus.Name = "chkEliminatePegasus";
            this.chkEliminatePegasus.Size = new System.Drawing.Size(92, 18);
            this.chkEliminatePegasus.TabIndex = 2;
            this.chkEliminatePegasus.Text = "Use gloScan";
            this.chkEliminatePegasus.UseVisualStyleBackColor = true;
            this.chkEliminatePegasus.CheckedChanged += new System.EventHandler(this.chkEliminatePegasus_CheckedChanged);
            // 
            // btnRefreshRemoteScanner
            // 
            this.btnRefreshRemoteScanner.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshRemoteScanner.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Button;
            this.btnRefreshRemoteScanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshRemoteScanner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshRemoteScanner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshRemoteScanner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshRemoteScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshRemoteScanner.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshRemoteScanner.Image")));
            this.btnRefreshRemoteScanner.Location = new System.Drawing.Point(315, 32);
            this.btnRefreshRemoteScanner.Name = "btnRefreshRemoteScanner";
            this.btnRefreshRemoteScanner.Size = new System.Drawing.Size(28, 23);
            this.btnRefreshRemoteScanner.TabIndex = 1;
            this.btnRefreshRemoteScanner.UseVisualStyleBackColor = false;
            this.btnRefreshRemoteScanner.Click += new System.EventHandler(this.btnRefreshRemoteScanner_Click);
            this.btnRefreshRemoteScanner.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRefreshRemoteScanner.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongButton;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(548, 24);
            this.panel3.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(0, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(548, 1);
            this.label11.TabIndex = 17;
            this.label11.Text = "label2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 14);
            this.label12.TabIndex = 7;
            this.label12.Text = "Scan Resolution";
            // 
            // chkEnableRemoteScanner
            // 
            this.chkEnableRemoteScanner.AutoSize = true;
            this.chkEnableRemoteScanner.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableRemoteScanner.Location = new System.Drawing.Point(171, 34);
            this.chkEnableRemoteScanner.Name = "chkEnableRemoteScanner";
            this.chkEnableRemoteScanner.Size = new System.Drawing.Size(141, 18);
            this.chkEnableRemoteScanner.TabIndex = 0;
            this.chkEnableRemoteScanner.Text = "Enable Local Scanner";
            this.chkEnableRemoteScanner.UseVisualStyleBackColor = true;
            this.chkEnableRemoteScanner.CheckedChanged += new System.EventHandler(this.chkEnableRemoteScanner_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(4, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(548, 1);
            this.label3.TabIndex = 16;
            this.label3.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 137);
            this.label4.TabIndex = 15;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(552, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 137);
            this.label5.TabIndex = 14;
            this.label5.Text = "label3";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(550, 1);
            this.label6.TabIndex = 13;
            this.label6.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbPScanners);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(556, 45);
            this.panel2.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(4, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(548, 1);
            this.label13.TabIndex = 19;
            this.label13.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 41);
            this.label8.TabIndex = 15;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(552, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 41);
            this.label9.TabIndex = 14;
            this.label9.Text = "label3";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(550, 1);
            this.label10.TabIndex = 13;
            this.label10.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlScannerSettings);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 281);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(556, 197);
            this.panel4.TabIndex = 20;
            // 
            // pnlScannerSettings
            // 
            this.pnlScannerSettings.Controls.Add(this.c1ScannerProps);
            this.pnlScannerSettings.Controls.Add(this.panel6);
            this.pnlScannerSettings.Controls.Add(this.label7);
            this.pnlScannerSettings.Controls.Add(this.label15);
            this.pnlScannerSettings.Controls.Add(this.label16);
            this.pnlScannerSettings.Controls.Add(this.label17);
            this.pnlScannerSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScannerSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlScannerSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlScannerSettings.Location = new System.Drawing.Point(0, 45);
            this.pnlScannerSettings.Name = "pnlScannerSettings";
            this.pnlScannerSettings.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlScannerSettings.Size = new System.Drawing.Size(556, 152);
            this.pnlScannerSettings.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.btnDefaultSet);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(4, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(548, 34);
            this.panel6.TabIndex = 3;
            // 
            // btnDefaultSet
            // 
            this.btnDefaultSet.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_LongButton;
            this.btnDefaultSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefaultSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultSet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultSet.Location = new System.Drawing.Point(448, 4);
            this.btnDefaultSet.Name = "btnDefaultSet";
            this.btnDefaultSet.Size = new System.Drawing.Size(93, 25);
            this.btnDefaultSet.TabIndex = 0;
            this.btnDefaultSet.Text = "Set Default";
            this.btnDefaultSet.UseVisualStyleBackColor = true;
            this.btnDefaultSet.Click += new System.EventHandler(this.btnDefaultSet_Click);
            this.btnDefaultSet.MouseLeave += new System.EventHandler(this.btnDefaultSet_MouseLeave);
            this.btnDefaultSet.MouseHover += new System.EventHandler(this.btnDefaultSet_MouseHover);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(0, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(548, 1);
            this.label14.TabIndex = 17;
            this.label14.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(4, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(548, 1);
            this.label7.TabIndex = 19;
            this.label7.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 148);
            this.label15.TabIndex = 15;
            this.label15.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(552, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 148);
            this.label16.TabIndex = 14;
            this.label16.Text = "label3";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(550, 1);
            this.label17.TabIndex = 13;
            this.label17.Text = "label1";
            // 
            // frmSetupScannerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(556, 478);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupScannerSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan Card Settings";
            this.Load += new System.EventHandler(this.frmSetupScannerSettings_Load);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlLicenseResolution.ResumeLayout(false);
            this.pnlLicenseResolution.PerformLayout();
            this.pnlInsuranceResolution.ResumeLayout(false);
            this.pnlInsuranceResolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ScannerProps)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlScannerSettings.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.RadioButton rdSR300;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.RadioButton rbSR600;
        private System.Windows.Forms.Label lblColorScheme;
        private System.Windows.Forms.ComboBox cmbColorScheme;
        private System.Windows.Forms.CheckBox chkboxDuplex;
        private System.Windows.Forms.ToolStripButton tsbCalibrate;
        private System.Windows.Forms.RadioButton rbLicense600;
        private System.Windows.Forms.RadioButton rbLicense300;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.ComboBox cmbPScanners;
        private System.Windows.Forms.Label label1;
        private PegasusImaging.WinForms.TwainPro5.TwainPro twainPro1;
        private System.Windows.Forms.CheckBox chkBoxScanPhoto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlInsuranceResolution;
        private System.Windows.Forms.Panel pnlLicenseResolution;
        private System.Windows.Forms.CheckBox chkCenterImage;
        private System.Windows.Forms.CheckBox chkUniformCardPrinting;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ScannerProps;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlScannerSettings;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnDefaultSet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnRefreshRemoteScanner;
        internal System.Windows.Forms.CheckBox chkEnableRemoteScanner;
        private System.Windows.Forms.Button btnRefreshTwainScanners;
        internal System.Windows.Forms.CheckBox chkEliminatePegasus;
        internal System.Windows.Forms.CheckBox chkZipScannerSettings;
    }
}