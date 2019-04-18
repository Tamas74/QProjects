namespace gloEDocumentV3.Forms
{
    partial class frm_EDocEvent_ScannerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_EDocEvent_ScannerSettings));
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.cmbImageFormat = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.Label119 = new System.Windows.Forms.Label();
            this.Label118 = new System.Windows.Forms.Label();
            this.cmbSupportedSize = new System.Windows.Forms.ComboBox();
            this.lblSuportedSize = new System.Windows.Forms.Label();
            this.txtStartX = new System.Windows.Forms.TextBox();
            this.txtStartY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbBitDepth = new System.Windows.Forms.ComboBox();
            this.lblBitDepth = new System.Windows.Forms.Label();
            this.txtCardWidth = new System.Windows.Forms.TextBox();
            this.txtCardLength = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbContrast = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbBrightness = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbScanMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbScanSide = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbScanner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowScannerDialog = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRefreshScanners = new System.Windows.Forms.Button();
            this.chkEnableRemoteScanner = new System.Windows.Forms.CheckBox();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_CloseSetting = new System.Windows.Forms.ToolStripButton();
            this.pnlEnableRemoteScan = new System.Windows.Forms.Panel();
            this.chkZipScannerSettings = new System.Windows.Forms.CheckBox();
            this.btnRefreshTwainScanners = new System.Windows.Forms.Button();
            this.chkEliminatePegasus = new System.Windows.Forms.CheckBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlRemoteScan = new System.Windows.Forms.Panel();
            this.cmbRemoteImageFormat = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbRemoteSupportedSize = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRemoteStartX = new System.Windows.Forms.TextBox();
            this.txtRemoteStartY = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbRemoteBitDepth = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRemoteCardWidth = new System.Windows.Forms.TextBox();
            this.txtRemoteCardLength = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbRemoteContrast = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbRemoteBrightness = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbRemoteScanMode = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbRemoteScanSide = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmbRemoteResolution = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cmbRemoteScanner = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.chkRemoteShowScannerDialog = new System.Windows.Forms.CheckBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbRemoteScanSideFeeder = new System.Windows.Forms.ComboBox();
            this.pnlSetting.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlEnableRemoteScan.SuspendLayout();
            this.pnlRemoteScan.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.cmbImageFormat);
            this.pnlSetting.Controls.Add(this.label46);
            this.pnlSetting.Controls.Add(this.label44);
            this.pnlSetting.Controls.Add(this.Label119);
            this.pnlSetting.Controls.Add(this.Label118);
            this.pnlSetting.Controls.Add(this.cmbSupportedSize);
            this.pnlSetting.Controls.Add(this.lblSuportedSize);
            this.pnlSetting.Controls.Add(this.txtStartX);
            this.pnlSetting.Controls.Add(this.txtStartY);
            this.pnlSetting.Controls.Add(this.label5);
            this.pnlSetting.Controls.Add(this.label6);
            this.pnlSetting.Controls.Add(this.label9);
            this.pnlSetting.Controls.Add(this.label11);
            this.pnlSetting.Controls.Add(this.cmbBitDepth);
            this.pnlSetting.Controls.Add(this.lblBitDepth);
            this.pnlSetting.Controls.Add(this.txtCardWidth);
            this.pnlSetting.Controls.Add(this.txtCardLength);
            this.pnlSetting.Controls.Add(this.label35);
            this.pnlSetting.Controls.Add(this.label40);
            this.pnlSetting.Controls.Add(this.label42);
            this.pnlSetting.Controls.Add(this.label49);
            this.pnlSetting.Controls.Add(this.cmbContrast);
            this.pnlSetting.Controls.Add(this.label18);
            this.pnlSetting.Controls.Add(this.cmbBrightness);
            this.pnlSetting.Controls.Add(this.label17);
            this.pnlSetting.Controls.Add(this.cmbScanMode);
            this.pnlSetting.Controls.Add(this.label4);
            this.pnlSetting.Controls.Add(this.cmbScanSide);
            this.pnlSetting.Controls.Add(this.label3);
            this.pnlSetting.Controls.Add(this.cmbResolution);
            this.pnlSetting.Controls.Add(this.label2);
            this.pnlSetting.Controls.Add(this.cmbScanner);
            this.pnlSetting.Controls.Add(this.label1);
            this.pnlSetting.Controls.Add(this.chkShowScannerDialog);
            this.pnlSetting.Controls.Add(this.label14);
            this.pnlSetting.Controls.Add(this.label13);
            this.pnlSetting.Controls.Add(this.label8);
            this.pnlSetting.Controls.Add(this.label7);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetting.Location = new System.Drawing.Point(0, 3);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSetting.Size = new System.Drawing.Size(413, 398);
            this.pnlSetting.TabIndex = 1;
            // 
            // cmbImageFormat
            // 
            this.cmbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageFormat.Enabled = false;
            this.cmbImageFormat.FormattingEnabled = true;
            this.cmbImageFormat.Location = new System.Drawing.Point(116, 201);
            this.cmbImageFormat.Name = "cmbImageFormat";
            this.cmbImageFormat.Size = new System.Drawing.Size(264, 22);
            this.cmbImageFormat.TabIndex = 6;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(22, 205);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(91, 14);
            this.label46.TabIndex = 112;
            this.label46.Text = "Image Format :";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(14, 10);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(112, 14);
            this.label44.TabIndex = 110;
            this.label44.Text = "Scanner Settings";
            // 
            // Label119
            // 
            this.Label119.AutoSize = true;
            this.Label119.Font = new System.Drawing.Font("Sylfaen", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label119.Location = new System.Drawing.Point(202, 327);
            this.Label119.Name = "Label119";
            this.Label119.Size = new System.Drawing.Size(16, 16);
            this.Label119.TabIndex = 109;
            this.Label119.Text = "X";
            // 
            // Label118
            // 
            this.Label118.AutoSize = true;
            this.Label118.Font = new System.Drawing.Font("Sylfaen", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label118.Location = new System.Drawing.Point(202, 289);
            this.Label118.Name = "Label118";
            this.Label118.Size = new System.Drawing.Size(16, 16);
            this.Label118.TabIndex = 108;
            this.Label118.Text = "X";
            // 
            // cmbSupportedSize
            // 
            this.cmbSupportedSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupportedSize.FormattingEnabled = true;
            this.cmbSupportedSize.Location = new System.Drawing.Point(116, 256);
            this.cmbSupportedSize.Name = "cmbSupportedSize";
            this.cmbSupportedSize.Size = new System.Drawing.Size(264, 22);
            this.cmbSupportedSize.TabIndex = 8;
            this.cmbSupportedSize.SelectedIndexChanged += new System.EventHandler(this.cmbSupportedSize_SelectedIndexChanged);
            // 
            // lblSuportedSize
            // 
            this.lblSuportedSize.AutoSize = true;
            this.lblSuportedSize.Location = new System.Drawing.Point(15, 260);
            this.lblSuportedSize.Name = "lblSuportedSize";
            this.lblSuportedSize.Size = new System.Drawing.Size(98, 14);
            this.lblSuportedSize.TabIndex = 68;
            this.lblSuportedSize.Text = "Supported Size :";
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(116, 325);
            this.txtStartX.MaxLength = 10;
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.ShortcutsEnabled = false;
            this.txtStartX.Size = new System.Drawing.Size(82, 22);
            this.txtStartX.TabIndex = 11;
            this.txtStartX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartX_KeyPress);
            // 
            // txtStartY
            // 
            this.txtStartY.Location = new System.Drawing.Point(222, 325);
            this.txtStartY.MaxLength = 10;
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.ShortcutsEnabled = false;
            this.txtStartY.Size = new System.Drawing.Size(82, 22);
            this.txtStartY.TabIndex = 12;
            this.txtStartY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartY_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(144, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 11);
            this.label5.TabIndex = 62;
            this.label5.Text = "(Left)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(250, 351);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 11);
            this.label6.TabIndex = 66;
            this.label6.Text = "(Top)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(309, 328);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 14);
            this.label9.TabIndex = 65;
            this.label9.Text = "(Inches)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 327);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 14);
            this.label11.TabIndex = 64;
            this.label11.Text = "Card Location :";
            // 
            // cmbBitDepth
            // 
            this.cmbBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBitDepth.FormattingEnabled = true;
            this.cmbBitDepth.Location = new System.Drawing.Point(116, 93);
            this.cmbBitDepth.Name = "cmbBitDepth";
            this.cmbBitDepth.Size = new System.Drawing.Size(264, 22);
            this.cmbBitDepth.TabIndex = 2;
            this.cmbBitDepth.SelectedIndexChanged += new System.EventHandler(this.cmbBitDepth_SelectedIndexChanged);
            // 
            // lblBitDepth
            // 
            this.lblBitDepth.AutoSize = true;
            this.lblBitDepth.Location = new System.Drawing.Point(34, 97);
            this.lblBitDepth.Name = "lblBitDepth";
            this.lblBitDepth.Size = new System.Drawing.Size(79, 14);
            this.lblBitDepth.TabIndex = 59;
            this.lblBitDepth.Text = "Scan Depth :";
            // 
            // txtCardWidth
            // 
            this.txtCardWidth.Location = new System.Drawing.Point(116, 285);
            this.txtCardWidth.MaxLength = 10;
            this.txtCardWidth.Name = "txtCardWidth";
            this.txtCardWidth.ShortcutsEnabled = false;
            this.txtCardWidth.Size = new System.Drawing.Size(82, 22);
            this.txtCardWidth.TabIndex = 9;
            this.txtCardWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardWidth_KeyPress);
            // 
            // txtCardLength
            // 
            this.txtCardLength.Location = new System.Drawing.Point(222, 286);
            this.txtCardLength.MaxLength = 10;
            this.txtCardLength.Name = "txtCardLength";
            this.txtCardLength.ShortcutsEnabled = false;
            this.txtCardLength.Size = new System.Drawing.Size(82, 22);
            this.txtCardLength.TabIndex = 10;
            this.txtCardLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardLength_KeyPress);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(140, 310);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 11);
            this.label35.TabIndex = 46;
            this.label35.Text = "(Width)";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(244, 310);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(38, 11);
            this.label40.TabIndex = 57;
            this.label40.Text = "(Length)";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(308, 289);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 14);
            this.label42.TabIndex = 56;
            this.label42.Text = "(Inches)";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(49, 289);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(64, 14);
            this.label49.TabIndex = 55;
            this.label49.Text = "Card Size :";
            // 
            // cmbContrast
            // 
            this.cmbContrast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContrast.FormattingEnabled = true;
            this.cmbContrast.Location = new System.Drawing.Point(116, 174);
            this.cmbContrast.Name = "cmbContrast";
            this.cmbContrast.Size = new System.Drawing.Size(264, 22);
            this.cmbContrast.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(52, 178);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 14);
            this.label18.TabIndex = 53;
            this.label18.Text = "Contrast :";
            // 
            // cmbBrightness
            // 
            this.cmbBrightness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrightness.FormattingEnabled = true;
            this.cmbBrightness.Location = new System.Drawing.Point(116, 147);
            this.cmbBrightness.Name = "cmbBrightness";
            this.cmbBrightness.Size = new System.Drawing.Size(264, 22);
            this.cmbBrightness.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(42, 151);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 14);
            this.label17.TabIndex = 52;
            this.label17.Text = "Brightness :";
            // 
            // cmbScanMode
            // 
            this.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanMode.FormattingEnabled = true;
            this.cmbScanMode.Location = new System.Drawing.Point(116, 66);
            this.cmbScanMode.Name = "cmbScanMode";
            this.cmbScanMode.Size = new System.Drawing.Size(264, 22);
            this.cmbScanMode.TabIndex = 1;
            this.cmbScanMode.SelectedIndexChanged += new System.EventHandler(this.cmbScanMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 14);
            this.label4.TabIndex = 51;
            this.label4.Text = "Scan Mode :";
            // 
            // cmbScanSide
            // 
            this.cmbScanSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanSide.FormattingEnabled = true;
            this.cmbScanSide.Location = new System.Drawing.Point(116, 229);
            this.cmbScanSide.Name = "cmbScanSide";
            this.cmbScanSide.Size = new System.Drawing.Size(264, 22);
            this.cmbScanSide.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 48;
            this.label3.Text = "Scan Side :";
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(116, 120);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(264, 22);
            this.cmbResolution.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 49;
            this.label2.Text = "Resolution :";
            // 
            // cmbScanner
            // 
            this.cmbScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanner.Location = new System.Drawing.Point(116, 39);
            this.cmbScanner.Name = "cmbScanner";
            this.cmbScanner.Size = new System.Drawing.Size(264, 22);
            this.cmbScanner.TabIndex = 0;
            this.cmbScanner.SelectedIndexChanged += new System.EventHandler(this.cmbScanner_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 50;
            this.label1.Text = "Scanner :";
            // 
            // chkShowScannerDialog
            // 
            this.chkShowScannerDialog.AutoSize = true;
            this.chkShowScannerDialog.Location = new System.Drawing.Point(117, 366);
            this.chkShowScannerDialog.Name = "chkShowScannerDialog";
            this.chkShowScannerDialog.Size = new System.Drawing.Size(141, 18);
            this.chkShowScannerDialog.TabIndex = 13;
            this.chkShowScannerDialog.Text = "Show Scanner Dialog";
            this.chkShowScannerDialog.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 394);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(405, 1);
            this.label14.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(405, 1);
            this.label13.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 395);
            this.label8.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(409, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 395);
            this.label7.TabIndex = 20;
            // 
            // btnRefreshScanners
            // 
            this.btnRefreshScanners.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshScanners.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.btnRefreshScanners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshScanners.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshScanners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshScanners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshScanners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshScanners.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshScanners.Image")));
            this.btnRefreshScanners.Location = new System.Drawing.Point(178, 7);
            this.btnRefreshScanners.Name = "btnRefreshScanners";
            this.btnRefreshScanners.Size = new System.Drawing.Size(28, 23);
            this.btnRefreshScanners.TabIndex = 1;
            this.btnRefreshScanners.UseVisualStyleBackColor = false;
            this.btnRefreshScanners.Click += new System.EventHandler(this.btnRefreshScanners_Click);
            this.btnRefreshScanners.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRefreshScanners.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // chkEnableRemoteScanner
            // 
            this.chkEnableRemoteScanner.AutoSize = true;
            this.chkEnableRemoteScanner.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableRemoteScanner.Location = new System.Drawing.Point(21, 10);
            this.chkEnableRemoteScanner.Name = "chkEnableRemoteScanner";
            this.chkEnableRemoteScanner.Size = new System.Drawing.Size(141, 18);
            this.chkEnableRemoteScanner.TabIndex = 0;
            this.chkEnableRemoteScanner.Text = "Enable Local Scanner";
            this.chkEnableRemoteScanner.UseVisualStyleBackColor = true;
            this.chkEnableRemoteScanner.CheckedChanged += new System.EventHandler(this.chkEnableRemoteScanner_CheckedChanged);
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolstrip.Controls.Add(this.tls_MaintainDoc);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(413, 54);
            this.pnlToolstrip.TabIndex = 36;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_CloseSetting});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(413, 53);
            this.tls_MaintainDoc.TabIndex = 0;
            this.tls_MaintainDoc.Text = "toolStrip1";
            this.tls_MaintainDoc.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_MaintainDoc_ItemClicked);
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "&Save&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            // 
            // tlb_CloseSetting
            // 
            this.tlb_CloseSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_CloseSetting.Image = ((System.Drawing.Image)(resources.GetObject("tlb_CloseSetting.Image")));
            this.tlb_CloseSetting.Name = "tlb_CloseSetting";
            this.tlb_CloseSetting.Size = new System.Drawing.Size(43, 50);
            this.tlb_CloseSetting.Tag = "Close";
            this.tlb_CloseSetting.Text = "&Close";
            this.tlb_CloseSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlEnableRemoteScan
            // 
            this.pnlEnableRemoteScan.Controls.Add(this.chkZipScannerSettings);
            this.pnlEnableRemoteScan.Controls.Add(this.btnRefreshTwainScanners);
            this.pnlEnableRemoteScan.Controls.Add(this.chkEliminatePegasus);
            this.pnlEnableRemoteScan.Controls.Add(this.label43);
            this.pnlEnableRemoteScan.Controls.Add(this.label41);
            this.pnlEnableRemoteScan.Controls.Add(this.label39);
            this.pnlEnableRemoteScan.Controls.Add(this.label38);
            this.pnlEnableRemoteScan.Controls.Add(this.btnRefreshScanners);
            this.pnlEnableRemoteScan.Controls.Add(this.chkEnableRemoteScanner);
            this.pnlEnableRemoteScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEnableRemoteScan.Location = new System.Drawing.Point(0, 54);
            this.pnlEnableRemoteScan.Name = "pnlEnableRemoteScan";
            this.pnlEnableRemoteScan.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlEnableRemoteScan.Size = new System.Drawing.Size(413, 58);
            this.pnlEnableRemoteScan.TabIndex = 0;
            // 
            // chkZipScannerSettings
            // 
            this.chkZipScannerSettings.AutoSize = true;
            this.chkZipScannerSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkZipScannerSettings.Location = new System.Drawing.Point(21, 34);
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
            this.btnRefreshTwainScanners.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.btnRefreshTwainScanners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshTwainScanners.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshTwainScanners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshTwainScanners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshTwainScanners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshTwainScanners.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshTwainScanners.Image")));
            this.btnRefreshTwainScanners.Location = new System.Drawing.Point(350, 7);
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
            this.chkEliminatePegasus.Location = new System.Drawing.Point(251, 10);
            this.chkEliminatePegasus.Name = "chkEliminatePegasus";
            this.chkEliminatePegasus.Size = new System.Drawing.Size(92, 18);
            this.chkEliminatePegasus.TabIndex = 2;
            this.chkEliminatePegasus.Text = "Use gloScan";
            this.chkEliminatePegasus.UseVisualStyleBackColor = true;
            this.chkEliminatePegasus.CheckedChanged += new System.EventHandler(this.chkEliminatePegasus_CheckedChanged);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(4, 57);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(405, 1);
            this.label43.TabIndex = 115;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Location = new System.Drawing.Point(4, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(405, 1);
            this.label41.TabIndex = 114;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Right;
            this.label39.Location = new System.Drawing.Point(409, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 55);
            this.label39.TabIndex = 113;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Location = new System.Drawing.Point(3, 3);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 55);
            this.label38.TabIndex = 112;
            // 
            // pnlRemoteScan
            // 
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteScanSideFeeder);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteImageFormat);
            this.pnlRemoteScan.Controls.Add(this.label47);
            this.pnlRemoteScan.Controls.Add(this.label45);
            this.pnlRemoteScan.Controls.Add(this.label10);
            this.pnlRemoteScan.Controls.Add(this.label12);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteSupportedSize);
            this.pnlRemoteScan.Controls.Add(this.label15);
            this.pnlRemoteScan.Controls.Add(this.txtRemoteStartX);
            this.pnlRemoteScan.Controls.Add(this.txtRemoteStartY);
            this.pnlRemoteScan.Controls.Add(this.label16);
            this.pnlRemoteScan.Controls.Add(this.label19);
            this.pnlRemoteScan.Controls.Add(this.label20);
            this.pnlRemoteScan.Controls.Add(this.label21);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteBitDepth);
            this.pnlRemoteScan.Controls.Add(this.label22);
            this.pnlRemoteScan.Controls.Add(this.txtRemoteCardWidth);
            this.pnlRemoteScan.Controls.Add(this.txtRemoteCardLength);
            this.pnlRemoteScan.Controls.Add(this.label23);
            this.pnlRemoteScan.Controls.Add(this.label24);
            this.pnlRemoteScan.Controls.Add(this.label25);
            this.pnlRemoteScan.Controls.Add(this.label26);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteContrast);
            this.pnlRemoteScan.Controls.Add(this.label27);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteBrightness);
            this.pnlRemoteScan.Controls.Add(this.label28);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteScanMode);
            this.pnlRemoteScan.Controls.Add(this.label29);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteScanSide);
            this.pnlRemoteScan.Controls.Add(this.label30);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteResolution);
            this.pnlRemoteScan.Controls.Add(this.label31);
            this.pnlRemoteScan.Controls.Add(this.cmbRemoteScanner);
            this.pnlRemoteScan.Controls.Add(this.label32);
            this.pnlRemoteScan.Controls.Add(this.chkRemoteShowScannerDialog);
            this.pnlRemoteScan.Controls.Add(this.label33);
            this.pnlRemoteScan.Controls.Add(this.label34);
            this.pnlRemoteScan.Controls.Add(this.label36);
            this.pnlRemoteScan.Controls.Add(this.label37);
            this.pnlRemoteScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRemoteScan.Location = new System.Drawing.Point(0, 3);
            this.pnlRemoteScan.Name = "pnlRemoteScan";
            this.pnlRemoteScan.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlRemoteScan.Size = new System.Drawing.Size(413, 398);
            this.pnlRemoteScan.TabIndex = 38;
            // 
            // cmbRemoteImageFormat
            // 
            this.cmbRemoteImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteImageFormat.FormattingEnabled = true;
            this.cmbRemoteImageFormat.Location = new System.Drawing.Point(116, 201);
            this.cmbRemoteImageFormat.Name = "cmbRemoteImageFormat";
            this.cmbRemoteImageFormat.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteImageFormat.TabIndex = 6;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(22, 205);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(91, 14);
            this.label47.TabIndex = 114;
            this.label47.Text = "Image Format :";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(14, 10);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 14);
            this.label45.TabIndex = 111;
            this.label45.Text = "Local Settings";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Sylfaen", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(202, 327);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 16);
            this.label10.TabIndex = 109;
            this.label10.Text = "X";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Sylfaen", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(202, 289);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 16);
            this.label12.TabIndex = 108;
            this.label12.Text = "X";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // cmbRemoteSupportedSize
            // 
            this.cmbRemoteSupportedSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteSupportedSize.FormattingEnabled = true;
            this.cmbRemoteSupportedSize.Location = new System.Drawing.Point(116, 256);
            this.cmbRemoteSupportedSize.Name = "cmbRemoteSupportedSize";
            this.cmbRemoteSupportedSize.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteSupportedSize.TabIndex = 9;
            this.cmbRemoteSupportedSize.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteSupportedSize_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 260);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 14);
            this.label15.TabIndex = 68;
            this.label15.Text = "Supported Size :";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // txtRemoteStartX
            // 
            this.txtRemoteStartX.Location = new System.Drawing.Point(116, 325);
            this.txtRemoteStartX.MaxLength = 10;
            this.txtRemoteStartX.Name = "txtRemoteStartX";
            this.txtRemoteStartX.ShortcutsEnabled = false;
            this.txtRemoteStartX.Size = new System.Drawing.Size(82, 22);
            this.txtRemoteStartX.TabIndex = 12;
            this.txtRemoteStartX.TextChanged += new System.EventHandler(this.txtRemoteStartX_TextChanged);
            this.txtRemoteStartX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteStartX_KeyPress);
            // 
            // txtRemoteStartY
            // 
            this.txtRemoteStartY.Location = new System.Drawing.Point(222, 325);
            this.txtRemoteStartY.MaxLength = 10;
            this.txtRemoteStartY.Name = "txtRemoteStartY";
            this.txtRemoteStartY.ShortcutsEnabled = false;
            this.txtRemoteStartY.Size = new System.Drawing.Size(82, 22);
            this.txtRemoteStartY.TabIndex = 13;
            this.txtRemoteStartY.TextChanged += new System.EventHandler(this.txtRemoteStartY_TextChanged);
            this.txtRemoteStartY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteStartY_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(144, 350);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 11);
            this.label16.TabIndex = 62;
            this.label16.Text = "(Left)";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(250, 351);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 11);
            this.label19.TabIndex = 66;
            this.label19.Text = "(Top)";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(309, 328);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 14);
            this.label20.TabIndex = 65;
            this.label20.Text = "(Inches)";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(24, 327);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 14);
            this.label21.TabIndex = 64;
            this.label21.Text = "Card Location :";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // cmbRemoteBitDepth
            // 
            this.cmbRemoteBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteBitDepth.FormattingEnabled = true;
            this.cmbRemoteBitDepth.Location = new System.Drawing.Point(116, 93);
            this.cmbRemoteBitDepth.Name = "cmbRemoteBitDepth";
            this.cmbRemoteBitDepth.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteBitDepth.TabIndex = 2;
            this.cmbRemoteBitDepth.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteBitDepth_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(34, 97);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 14);
            this.label22.TabIndex = 59;
            this.label22.Text = "Scan Depth :";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // txtRemoteCardWidth
            // 
            this.txtRemoteCardWidth.Location = new System.Drawing.Point(116, 285);
            this.txtRemoteCardWidth.MaxLength = 10;
            this.txtRemoteCardWidth.Name = "txtRemoteCardWidth";
            this.txtRemoteCardWidth.ShortcutsEnabled = false;
            this.txtRemoteCardWidth.Size = new System.Drawing.Size(82, 22);
            this.txtRemoteCardWidth.TabIndex = 10;
            this.txtRemoteCardWidth.TextChanged += new System.EventHandler(this.txtRemoteCardWidth_TextChanged);
            this.txtRemoteCardWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteCardWidth_KeyPress);
            // 
            // txtRemoteCardLength
            // 
            this.txtRemoteCardLength.Location = new System.Drawing.Point(222, 286);
            this.txtRemoteCardLength.MaxLength = 10;
            this.txtRemoteCardLength.Name = "txtRemoteCardLength";
            this.txtRemoteCardLength.ShortcutsEnabled = false;
            this.txtRemoteCardLength.Size = new System.Drawing.Size(82, 22);
            this.txtRemoteCardLength.TabIndex = 11;
            this.txtRemoteCardLength.TextChanged += new System.EventHandler(this.txtRemoteCardLength_TextChanged);
            this.txtRemoteCardLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteCardLength_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(140, 310);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 11);
            this.label23.TabIndex = 46;
            this.label23.Text = "(Width)";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(244, 310);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 11);
            this.label24.TabIndex = 57;
            this.label24.Text = "(Length)";
            this.label24.Click += new System.EventHandler(this.label24_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(308, 289);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 14);
            this.label25.TabIndex = 56;
            this.label25.Text = "(Inches)";
            this.label25.Click += new System.EventHandler(this.label25_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(49, 289);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 14);
            this.label26.TabIndex = 55;
            this.label26.Text = "Card Size :";
            this.label26.Click += new System.EventHandler(this.label26_Click);
            // 
            // cmbRemoteContrast
            // 
            this.cmbRemoteContrast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteContrast.FormattingEnabled = true;
            this.cmbRemoteContrast.Location = new System.Drawing.Point(116, 174);
            this.cmbRemoteContrast.Name = "cmbRemoteContrast";
            this.cmbRemoteContrast.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteContrast.TabIndex = 5;
            this.cmbRemoteContrast.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteContrast_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(52, 178);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(61, 14);
            this.label27.TabIndex = 53;
            this.label27.Text = "Contrast :";
            this.label27.Click += new System.EventHandler(this.label27_Click);
            // 
            // cmbRemoteBrightness
            // 
            this.cmbRemoteBrightness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteBrightness.FormattingEnabled = true;
            this.cmbRemoteBrightness.Location = new System.Drawing.Point(116, 147);
            this.cmbRemoteBrightness.Name = "cmbRemoteBrightness";
            this.cmbRemoteBrightness.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteBrightness.TabIndex = 4;
            this.cmbRemoteBrightness.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteBrightness_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(42, 151);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(71, 14);
            this.label28.TabIndex = 52;
            this.label28.Text = "Brightness :";
            this.label28.Click += new System.EventHandler(this.label28_Click);
            // 
            // cmbRemoteScanMode
            // 
            this.cmbRemoteScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteScanMode.FormattingEnabled = true;
            this.cmbRemoteScanMode.Location = new System.Drawing.Point(116, 66);
            this.cmbRemoteScanMode.Name = "cmbRemoteScanMode";
            this.cmbRemoteScanMode.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteScanMode.TabIndex = 1;
            this.cmbRemoteScanMode.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteScanMode_SelectedIndexChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(38, 70);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(75, 14);
            this.label29.TabIndex = 51;
            this.label29.Text = "Scan Mode :";
            this.label29.Click += new System.EventHandler(this.label29_Click);
            // 
            // cmbRemoteScanSide
            // 
            this.cmbRemoteScanSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteScanSide.FormattingEnabled = true;
            this.cmbRemoteScanSide.Location = new System.Drawing.Point(116, 229);
            this.cmbRemoteScanSide.Name = "cmbRemoteScanSide";
            this.cmbRemoteScanSide.Size = new System.Drawing.Size(128, 22);
            this.cmbRemoteScanSide.TabIndex = 7;
            this.cmbRemoteScanSide.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteScanSide_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(45, 233);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 14);
            this.label30.TabIndex = 48;
            this.label30.Text = "Scan Side :";
            this.label30.Click += new System.EventHandler(this.label30_Click);
            // 
            // cmbRemoteResolution
            // 
            this.cmbRemoteResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteResolution.FormattingEnabled = true;
            this.cmbRemoteResolution.Location = new System.Drawing.Point(116, 120);
            this.cmbRemoteResolution.Name = "cmbRemoteResolution";
            this.cmbRemoteResolution.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteResolution.TabIndex = 3;
            this.cmbRemoteResolution.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteResolution_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(42, 124);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 14);
            this.label31.TabIndex = 49;
            this.label31.Text = "Resolution :";
            this.label31.Click += new System.EventHandler(this.label31_Click);
            // 
            // cmbRemoteScanner
            // 
            this.cmbRemoteScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteScanner.Location = new System.Drawing.Point(116, 39);
            this.cmbRemoteScanner.Name = "cmbRemoteScanner";
            this.cmbRemoteScanner.Size = new System.Drawing.Size(264, 22);
            this.cmbRemoteScanner.TabIndex = 0;
            this.cmbRemoteScanner.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteScanner_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(54, 43);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(59, 14);
            this.label32.TabIndex = 50;
            this.label32.Text = "Scanner :";
            this.label32.Click += new System.EventHandler(this.label32_Click);
            // 
            // chkRemoteShowScannerDialog
            // 
            this.chkRemoteShowScannerDialog.AutoSize = true;
            this.chkRemoteShowScannerDialog.Location = new System.Drawing.Point(117, 366);
            this.chkRemoteShowScannerDialog.Name = "chkRemoteShowScannerDialog";
            this.chkRemoteShowScannerDialog.Size = new System.Drawing.Size(141, 18);
            this.chkRemoteShowScannerDialog.TabIndex = 14;
            this.chkRemoteShowScannerDialog.Text = "Show Scanner Dialog";
            this.chkRemoteShowScannerDialog.UseVisualStyleBackColor = true;
            this.chkRemoteShowScannerDialog.CheckedChanged += new System.EventHandler(this.chkRemoteShowScannerDialog_CheckedChanged);
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Location = new System.Drawing.Point(4, 394);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(405, 1);
            this.label33.TabIndex = 23;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Location = new System.Drawing.Point(4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(405, 1);
            this.label34.TabIndex = 22;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(3, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 395);
            this.label36.TabIndex = 21;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Location = new System.Drawing.Point(409, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 395);
            this.label37.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSetting);
            this.panel1.Controls.Add(this.pnlRemoteScan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 112);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(413, 401);
            this.panel1.TabIndex = 39;
            // 
            // cmbRemoteScanSideFidder
            // 
            this.cmbRemoteScanSideFeeder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemoteScanSideFeeder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRemoteScanSideFeeder.FormattingEnabled = true;
            this.cmbRemoteScanSideFeeder.Location = new System.Drawing.Point(252, 229);
            this.cmbRemoteScanSideFeeder.Name = "cmbRemoteScanSideFidder";
            this.cmbRemoteScanSideFeeder.Size = new System.Drawing.Size(128, 22);
            this.cmbRemoteScanSideFeeder.TabIndex = 8;
            // 
            // frm_EDocEvent_ScannerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(413, 513);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlEnableRemoteScan);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_EDocEvent_ScannerSettings";
            this.Text = "Scanner Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_EDocEvent_ScannerSettings_FormClosing);
            this.Load += new System.EventHandler(this.frm_EDocEvent_ScannerSettings_Load);
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlEnableRemoteScan.ResumeLayout(false);
            this.pnlEnableRemoteScan.PerformLayout();
            this.pnlRemoteScan.ResumeLayout(false);
            this.pnlRemoteScan.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_CloseSetting;
        private System.Windows.Forms.ComboBox cmbBitDepth;
        private System.Windows.Forms.Label lblBitDepth;
        private System.Windows.Forms.TextBox txtCardWidth;
        private System.Windows.Forms.TextBox txtCardLength;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox cmbContrast;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbBrightness;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbScanMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbScanSide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbScanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowScannerDialog;
        private System.Windows.Forms.TextBox txtStartX;
        private System.Windows.Forms.TextBox txtStartY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSupportedSize;
        private System.Windows.Forms.Label lblSuportedSize;
        private System.Windows.Forms.Button btnRefreshScanners;
        internal System.Windows.Forms.CheckBox chkEnableRemoteScanner;
        private System.Windows.Forms.Label Label119;
        private System.Windows.Forms.Label Label118;
        private System.Windows.Forms.Panel pnlEnableRemoteScan;
        private System.Windows.Forms.Panel pnlRemoteScan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbRemoteSupportedSize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRemoteStartX;
        private System.Windows.Forms.TextBox txtRemoteStartY;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbRemoteBitDepth;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtRemoteCardWidth;
        private System.Windows.Forms.TextBox txtRemoteCardLength;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbRemoteContrast;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbRemoteBrightness;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbRemoteScanMode;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbRemoteScanSide;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cmbRemoteResolution;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmbRemoteScanner;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox chkRemoteShowScannerDialog;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        internal System.Windows.Forms.CheckBox chkEliminatePegasus;
        private System.Windows.Forms.Button btnRefreshTwainScanners;
        private System.Windows.Forms.ComboBox cmbImageFormat;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox cmbRemoteImageFormat;
        private System.Windows.Forms.Label label47;
        internal System.Windows.Forms.CheckBox chkZipScannerSettings;
        private System.Windows.Forms.ComboBox cmbRemoteScanSideFeeder;
    }
}
