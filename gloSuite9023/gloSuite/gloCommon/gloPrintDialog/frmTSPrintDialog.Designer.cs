namespace gloPrintDialog
{
    partial class frmTSPrintDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTSPrintDialog));
            this.fontDialogFooter = new System.Windows.Forms.FontDialog();
            this.timerToRefresh = new System.Windows.Forms.Timer(this.components);
            this.grbName = new System.Windows.Forms.GroupBox();
            this.btnRefreshPrinters = new System.Windows.Forms.Button();
            this.cmbPrinterName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grbPrintRange = new System.Windows.Forms.GroupBox();
            this.numUpDownPageTo = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numUpDownPageFrom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.rbPrintRange_Pages = new System.Windows.Forms.RadioButton();
            this.rbPrintRange_All = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.cmbDuplex = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grbCopies = new System.Windows.Forms.GroupBox();
            this.pnlCollateDisabled = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pnlCollate_Enabled = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numUpDownNoOfCopies = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCollete = new System.Windows.Forms.CheckBox();
            this.grbOrientation = new System.Windows.Forms.GroupBox();
            this.rbOrientation_Landscape = new System.Windows.Forms.RadioButton();
            this.rbOrientation_Portrait = new System.Windows.Forms.RadioButton();
            this.btnShowExtendedSettings = new System.Windows.Forms.Button();
            this.grbPageSetup = new System.Windows.Forms.GroupBox();
            this.cmbTray = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlCustomSize = new System.Windows.Forms.Panel();
            this.NumUpDownCustomHeight = new System.Windows.Forms.NumericUpDown();
            this.NumUpDownCustomWidth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBottomPrinter = new System.Windows.Forms.Panel();
            this.pnlTopPrinter = new System.Windows.Forms.Panel();
            this.pnlMiddlePrinter = new System.Windows.Forms.Panel();
            this.grbName.SuspendLayout();
            this.grbPrintRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageFrom)).BeginInit();
            this.grbCopies.SuspendLayout();
            this.pnlCollateDisabled.SuspendLayout();
            this.pnlCollate_Enabled.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNoOfCopies)).BeginInit();
            this.grbOrientation.SuspendLayout();
            this.grbPageSetup.SuspendLayout();
            this.pnlCustomSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownCustomHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownCustomWidth)).BeginInit();
            this.pnlBottomPrinter.SuspendLayout();
            this.pnlTopPrinter.SuspendLayout();
            this.SuspendLayout();
            // 
            // fontDialogFooter
            // 
            this.fontDialogFooter.ShowApply = true;
            this.fontDialogFooter.ShowColor = true;
            this.fontDialogFooter.ShowHelp = true;
            // 
            // timerToRefresh
            // 
            this.timerToRefresh.Interval = 1000;
            // 
            // grbName
            // 
            this.grbName.Controls.Add(this.btnRefreshPrinters);
            this.grbName.Controls.Add(this.cmbPrinterName);
            this.grbName.Controls.Add(this.label1);
            this.grbName.Location = new System.Drawing.Point(9, 3);
            this.grbName.Name = "grbName";
            this.grbName.Size = new System.Drawing.Size(414, 42);
            this.grbName.TabIndex = 0;
            this.grbName.TabStop = false;
            this.grbName.Text = "Printer";
            // 
            // btnRefreshPrinters
            // 
            this.btnRefreshPrinters.Location = new System.Drawing.Point(341, 14);
            this.btnRefreshPrinters.Name = "btnRefreshPrinters";
            this.btnRefreshPrinters.Size = new System.Drawing.Size(67, 21);
            this.btnRefreshPrinters.TabIndex = 1;
            this.btnRefreshPrinters.Text = "Refresh";
            this.btnRefreshPrinters.UseVisualStyleBackColor = true;
            this.btnRefreshPrinters.Click += new System.EventHandler(this.btnRefreshPrinters_Click);
            // 
            // cmbPrinterName
            // 
            this.cmbPrinterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterName.FormattingEnabled = true;
            this.cmbPrinterName.Location = new System.Drawing.Point(65, 15);
            this.cmbPrinterName.Name = "cmbPrinterName";
            this.cmbPrinterName.Size = new System.Drawing.Size(266, 21);
            this.cmbPrinterName.TabIndex = 0;
            this.cmbPrinterName.SelectedIndexChanged += new System.EventHandler(this.cmbPrinterName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Paper Size:";
            // 
            // grbPrintRange
            // 
            this.grbPrintRange.Controls.Add(this.numUpDownPageTo);
            this.grbPrintRange.Controls.Add(this.label6);
            this.grbPrintRange.Controls.Add(this.numUpDownPageFrom);
            this.grbPrintRange.Controls.Add(this.label5);
            this.grbPrintRange.Controls.Add(this.rbPrintRange_Pages);
            this.grbPrintRange.Controls.Add(this.rbPrintRange_All);
            this.grbPrintRange.Location = new System.Drawing.Point(9, 93);
            this.grbPrintRange.Name = "grbPrintRange";
            this.grbPrintRange.Size = new System.Drawing.Size(224, 83);
            this.grbPrintRange.TabIndex = 2;
            this.grbPrintRange.TabStop = false;
            this.grbPrintRange.Text = "Print Range";
            // 
            // numUpDownPageTo
            // 
            this.numUpDownPageTo.Location = new System.Drawing.Point(171, 38);
            this.numUpDownPageTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownPageTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownPageTo.Name = "numUpDownPageTo";
            this.numUpDownPageTo.Size = new System.Drawing.Size(47, 20);
            this.numUpDownPageTo.TabIndex = 3;
            this.numUpDownPageTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "To:";
            // 
            // numUpDownPageFrom
            // 
            this.numUpDownPageFrom.Location = new System.Drawing.Point(97, 38);
            this.numUpDownPageFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownPageFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownPageFrom.Name = "numUpDownPageFrom";
            this.numUpDownPageFrom.Size = new System.Drawing.Size(45, 20);
            this.numUpDownPageFrom.TabIndex = 2;
            this.numUpDownPageFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "From:";
            // 
            // rbPrintRange_Pages
            // 
            this.rbPrintRange_Pages.AutoSize = true;
            this.rbPrintRange_Pages.Location = new System.Drawing.Point(11, 39);
            this.rbPrintRange_Pages.Name = "rbPrintRange_Pages";
            this.rbPrintRange_Pages.Size = new System.Drawing.Size(55, 17);
            this.rbPrintRange_Pages.TabIndex = 1;
            this.rbPrintRange_Pages.Text = "Pages";
            this.rbPrintRange_Pages.UseVisualStyleBackColor = true;
            this.rbPrintRange_Pages.CheckedChanged += new System.EventHandler(this.rbPrintRange_Pages_CheckedChanged);
            // 
            // rbPrintRange_All
            // 
            this.rbPrintRange_All.AutoSize = true;
            this.rbPrintRange_All.Checked = true;
            this.rbPrintRange_All.Location = new System.Drawing.Point(11, 18);
            this.rbPrintRange_All.Name = "rbPrintRange_All";
            this.rbPrintRange_All.Size = new System.Drawing.Size(36, 17);
            this.rbPrintRange_All.TabIndex = 0;
            this.rbPrintRange_All.TabStop = true;
            this.rbPrintRange_All.Text = "All";
            this.rbPrintRange_All.UseVisualStyleBackColor = true;
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Location = new System.Drawing.Point(64, 16);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(138, 21);
            this.cmbPaperSize.TabIndex = 0;
            this.cmbPaperSize.SelectedIndexChanged += new System.EventHandler(this.cmbPaperSize_SelectedIndexChanged);
            // 
            // cmbDuplex
            // 
            this.cmbDuplex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuplex.FormattingEnabled = true;
            this.cmbDuplex.Location = new System.Drawing.Point(270, 13);
            this.cmbDuplex.Name = "cmbDuplex";
            this.cmbDuplex.Size = new System.Drawing.Size(138, 21);
            this.cmbDuplex.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Duplex:";
            // 
            // grbCopies
            // 
            this.grbCopies.Controls.Add(this.pnlCollateDisabled);
            this.grbCopies.Controls.Add(this.pnlCollate_Enabled);
            this.grbCopies.Controls.Add(this.numUpDownNoOfCopies);
            this.grbCopies.Controls.Add(this.label4);
            this.grbCopies.Controls.Add(this.chkCollete);
            this.grbCopies.Location = new System.Drawing.Point(239, 93);
            this.grbCopies.Name = "grbCopies";
            this.grbCopies.Size = new System.Drawing.Size(184, 83);
            this.grbCopies.TabIndex = 3;
            this.grbCopies.TabStop = false;
            this.grbCopies.Text = "Copies";
            // 
            // pnlCollateDisabled
            // 
            this.pnlCollateDisabled.Controls.Add(this.button5);
            this.pnlCollateDisabled.Controls.Add(this.button3);
            this.pnlCollateDisabled.Controls.Add(this.button4);
            this.pnlCollateDisabled.Location = new System.Drawing.Point(6, 40);
            this.pnlCollateDisabled.Name = "pnlCollateDisabled";
            this.pnlCollateDisabled.Size = new System.Drawing.Size(106, 41);
            this.pnlCollateDisabled.TabIndex = 22;
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(71, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(32, 32);
            this.button5.TabIndex = 22;
            this.button5.TabStop = false;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(37, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 21;
            this.button3.TabStop = false;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 20;
            this.button4.TabStop = false;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // pnlCollate_Enabled
            // 
            this.pnlCollate_Enabled.Controls.Add(this.button2);
            this.pnlCollate_Enabled.Controls.Add(this.button1);
            this.pnlCollate_Enabled.Location = new System.Drawing.Point(6, 40);
            this.pnlCollate_Enabled.Name = "pnlCollate_Enabled";
            this.pnlCollate_Enabled.Size = new System.Drawing.Size(106, 41);
            this.pnlCollate_Enabled.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(41, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 21;
            this.button2.TabStop = false;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 20;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numUpDownNoOfCopies
            // 
            this.numUpDownNoOfCopies.Location = new System.Drawing.Point(117, 19);
            this.numUpDownNoOfCopies.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownNoOfCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownNoOfCopies.Name = "numUpDownNoOfCopies";
            this.numUpDownNoOfCopies.Size = new System.Drawing.Size(64, 20);
            this.numUpDownNoOfCopies.TabIndex = 0;
            this.numUpDownNoOfCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Number of copies:";
            // 
            // chkCollete
            // 
            this.chkCollete.AutoSize = true;
            this.chkCollete.Location = new System.Drawing.Point(122, 50);
            this.chkCollete.Margin = new System.Windows.Forms.Padding(0);
            this.chkCollete.Name = "chkCollete";
            this.chkCollete.Size = new System.Drawing.Size(58, 17);
            this.chkCollete.TabIndex = 1;
            this.chkCollete.Text = "Collate";
            this.chkCollete.UseVisualStyleBackColor = true;
            this.chkCollete.CheckedChanged += new System.EventHandler(this.chkCollete_CheckedChanged);
            // 
            // grbOrientation
            // 
            this.grbOrientation.Controls.Add(this.cmbDuplex);
            this.grbOrientation.Controls.Add(this.label2);
            this.grbOrientation.Controls.Add(this.rbOrientation_Landscape);
            this.grbOrientation.Controls.Add(this.rbOrientation_Portrait);
            this.grbOrientation.Location = new System.Drawing.Point(9, 177);
            this.grbOrientation.Name = "grbOrientation";
            this.grbOrientation.Size = new System.Drawing.Size(414, 41);
            this.grbOrientation.TabIndex = 4;
            this.grbOrientation.TabStop = false;
            this.grbOrientation.Text = "Orientation";
            // 
            // rbOrientation_Landscape
            // 
            this.rbOrientation_Landscape.AutoSize = true;
            this.rbOrientation_Landscape.Location = new System.Drawing.Point(95, 19);
            this.rbOrientation_Landscape.Name = "rbOrientation_Landscape";
            this.rbOrientation_Landscape.Size = new System.Drawing.Size(78, 17);
            this.rbOrientation_Landscape.TabIndex = 1;
            this.rbOrientation_Landscape.Text = "Landscape";
            this.rbOrientation_Landscape.UseVisualStyleBackColor = true;
            // 
            // rbOrientation_Portrait
            // 
            this.rbOrientation_Portrait.AutoSize = true;
            this.rbOrientation_Portrait.Checked = true;
            this.rbOrientation_Portrait.Location = new System.Drawing.Point(13, 18);
            this.rbOrientation_Portrait.Name = "rbOrientation_Portrait";
            this.rbOrientation_Portrait.Size = new System.Drawing.Size(58, 17);
            this.rbOrientation_Portrait.TabIndex = 0;
            this.rbOrientation_Portrait.TabStop = true;
            this.rbOrientation_Portrait.Text = "Portrait";
            this.rbOrientation_Portrait.UseVisualStyleBackColor = true;
            // 
            // btnShowExtendedSettings
            // 
            this.btnShowExtendedSettings.Location = new System.Drawing.Point(9, 5);
            this.btnShowExtendedSettings.Name = "btnShowExtendedSettings";
            this.btnShowExtendedSettings.Size = new System.Drawing.Size(146, 30);
            this.btnShowExtendedSettings.TabIndex = 0;
            this.btnShowExtendedSettings.Text = "Show Extended Settings";
            this.btnShowExtendedSettings.UseVisualStyleBackColor = true;
            this.btnShowExtendedSettings.Click += new System.EventHandler(this.btnShowExtendedSettings_Click);
            // 
            // grbPageSetup
            // 
            this.grbPageSetup.Controls.Add(this.cmbTray);
            this.grbPageSetup.Controls.Add(this.label9);
            this.grbPageSetup.Controls.Add(this.cmbPaperSize);
            this.grbPageSetup.Controls.Add(this.label3);
            this.grbPageSetup.Controls.Add(this.pnlCustomSize);
            this.grbPageSetup.Location = new System.Drawing.Point(9, 51);
            this.grbPageSetup.Name = "grbPageSetup";
            this.grbPageSetup.Size = new System.Drawing.Size(414, 42);
            this.grbPageSetup.TabIndex = 1;
            this.grbPageSetup.TabStop = false;
            this.grbPageSetup.Text = "Page Setup";
            // 
            // cmbTray
            // 
            this.cmbTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTray.FormattingEnabled = true;
            this.cmbTray.Location = new System.Drawing.Point(270, 14);
            this.cmbTray.Name = "cmbTray";
            this.cmbTray.Size = new System.Drawing.Size(138, 21);
            this.cmbTray.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(224, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Tray :";
            // 
            // pnlCustomSize
            // 
            this.pnlCustomSize.Controls.Add(this.NumUpDownCustomHeight);
            this.pnlCustomSize.Controls.Add(this.NumUpDownCustomWidth);
            this.pnlCustomSize.Controls.Add(this.label8);
            this.pnlCustomSize.Controls.Add(this.label7);
            this.pnlCustomSize.Location = new System.Drawing.Point(202, 12);
            this.pnlCustomSize.Name = "pnlCustomSize";
            this.pnlCustomSize.Size = new System.Drawing.Size(22, 24);
            this.pnlCustomSize.TabIndex = 12;
            this.pnlCustomSize.Visible = false;
            // 
            // NumUpDownCustomHeight
            // 
            this.NumUpDownCustomHeight.Location = new System.Drawing.Point(155, 2);
            this.NumUpDownCustomHeight.Name = "NumUpDownCustomHeight";
            this.NumUpDownCustomHeight.Size = new System.Drawing.Size(64, 20);
            this.NumUpDownCustomHeight.TabIndex = 1;
            // 
            // NumUpDownCustomWidth
            // 
            this.NumUpDownCustomWidth.Location = new System.Drawing.Point(45, 2);
            this.NumUpDownCustomWidth.Name = "NumUpDownCustomWidth";
            this.NumUpDownCustomWidth.Size = new System.Drawing.Size(64, 20);
            this.NumUpDownCustomWidth.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Width:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Height:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(290, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(359, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBottomPrinter
            // 
            this.pnlBottomPrinter.Controls.Add(this.btnShowExtendedSettings);
            this.pnlBottomPrinter.Controls.Add(this.btnCancel);
            this.pnlBottomPrinter.Controls.Add(this.btnOK);
            this.pnlBottomPrinter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomPrinter.Location = new System.Drawing.Point(0, 219);
            this.pnlBottomPrinter.Name = "pnlBottomPrinter";
            this.pnlBottomPrinter.Size = new System.Drawing.Size(434, 38);
            this.pnlBottomPrinter.TabIndex = 2;
            // 
            // pnlTopPrinter
            // 
            this.pnlTopPrinter.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopPrinter.Controls.Add(this.grbName);
            this.pnlTopPrinter.Controls.Add(this.grbPageSetup);
            this.pnlTopPrinter.Controls.Add(this.grbOrientation);
            this.pnlTopPrinter.Controls.Add(this.grbPrintRange);
            this.pnlTopPrinter.Controls.Add(this.grbCopies);
            this.pnlTopPrinter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopPrinter.Location = new System.Drawing.Point(0, 0);
            this.pnlTopPrinter.Name = "pnlTopPrinter";
            this.pnlTopPrinter.Size = new System.Drawing.Size(434, 221);
            this.pnlTopPrinter.TabIndex = 0;
            // 
            // pnlMiddlePrinter
            // 
            this.pnlMiddlePrinter.BackColor = System.Drawing.Color.Transparent;
            this.pnlMiddlePrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddlePrinter.Location = new System.Drawing.Point(0, 221);
            this.pnlMiddlePrinter.Name = "pnlMiddlePrinter";
            this.pnlMiddlePrinter.Size = new System.Drawing.Size(434, 0);
            this.pnlMiddlePrinter.TabIndex = 1;
            // 
            // frmTSPrintDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 257);
            this.Controls.Add(this.pnlMiddlePrinter);
            this.Controls.Add(this.pnlTopPrinter);
            this.Controls.Add(this.pnlBottomPrinter);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTSPrintDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gloLDSSniffer Print Dialog";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTSPrintDialog_FormClosing);
            this.Load += new System.EventHandler(this.frmTSPrintDialog_Load);
            this.Shown += new System.EventHandler(this.frmTSPrintDialog_Shown);
            this.grbName.ResumeLayout(false);
            this.grbName.PerformLayout();
            this.grbPrintRange.ResumeLayout(false);
            this.grbPrintRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageFrom)).EndInit();
            this.grbCopies.ResumeLayout(false);
            this.grbCopies.PerformLayout();
            this.pnlCollateDisabled.ResumeLayout(false);
            this.pnlCollateDisabled.PerformLayout();
            this.pnlCollate_Enabled.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNoOfCopies)).EndInit();
            this.grbOrientation.ResumeLayout(false);
            this.grbOrientation.PerformLayout();
            this.grbPageSetup.ResumeLayout(false);
            this.grbPageSetup.PerformLayout();
            this.pnlCustomSize.ResumeLayout(false);
            this.pnlCustomSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownCustomHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpDownCustomWidth)).EndInit();
            this.pnlBottomPrinter.ResumeLayout(false);
            this.pnlTopPrinter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FontDialog fontDialogFooter;
        private System.Windows.Forms.Timer timerToRefresh;
        private System.Windows.Forms.GroupBox grbName;
        private System.Windows.Forms.ComboBox cmbDuplex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.ComboBox cmbPrinterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbPrintRange;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbPrintRange_Pages;
        private System.Windows.Forms.RadioButton rbPrintRange_All;
        private System.Windows.Forms.GroupBox grbCopies;
        private System.Windows.Forms.CheckBox chkCollete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlCollateDisabled;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel pnlCollate_Enabled;
        private System.Windows.Forms.GroupBox grbOrientation;
        private System.Windows.Forms.RadioButton rbOrientation_Landscape;
        private System.Windows.Forms.RadioButton rbOrientation_Portrait;
        private System.Windows.Forms.Button btnShowExtendedSettings;
        private System.Windows.Forms.GroupBox grbPageSetup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown NumUpDownCustomWidth;
        private System.Windows.Forms.Panel pnlCustomSize;
        private System.Windows.Forms.NumericUpDown NumUpDownCustomHeight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlBottomPrinter;
        private System.Windows.Forms.Panel pnlTopPrinter;
        private System.Windows.Forms.Panel pnlMiddlePrinter;
        private System.Windows.Forms.NumericUpDown numUpDownPageTo;
        private System.Windows.Forms.NumericUpDown numUpDownPageFrom;
        private System.Windows.Forms.NumericUpDown numUpDownNoOfCopies;
        private System.Windows.Forms.Button btnRefreshPrinters;
        private System.Windows.Forms.ComboBox cmbTray;
        private System.Windows.Forms.Label label9;
    }
}