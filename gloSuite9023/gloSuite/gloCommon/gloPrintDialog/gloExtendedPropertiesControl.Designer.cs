namespace gloPrintDialog
{
    partial class gloExtendedPropertiesControl
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
                if (fontDialogFooter != null)
                {
                    fontDialogFooter.Dispose();
                    fontDialogFooter = null;
                }
                DisposeTimer();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlExtPanelContainer = new System.Windows.Forms.Panel();
            this.gbFooterMargins = new System.Windows.Forms.GroupBox();
            this.lblFooterMarginsTop = new System.Windows.Forms.Label();
            this.txtFooterMarginsTop = new System.Windows.Forms.TextBox();
            this.lblFooterMarginsLeft = new System.Windows.Forms.Label();
            this.txtFooterMarginsLeft = new System.Windows.Forms.TextBox();
            this.lblFooterMarginsRight = new System.Windows.Forms.Label();
            this.txtFooterMarginsRight = new System.Windows.Forms.TextBox();
            this.lblFooterMarginsBottom = new System.Windows.Forms.Label();
            this.txtFooterMarginsBottom = new System.Windows.Forms.TextBox();
            this.gbFooterFont = new System.Windows.Forms.GroupBox();
            this.btnFooterFont = new System.Windows.Forms.Button();
            this.lblFooterFont = new System.Windows.Forms.Label();
            this.gbOverlapActualSize = new System.Windows.Forms.GroupBox();
            this.txtOverlapFlat = new System.Windows.Forms.TextBox();
            this.lblOverlapActualSizeFlat = new System.Windows.Forms.Label();
            this.txtOverlapErect = new System.Windows.Forms.TextBox();
            this.lblOverlapActualSizeErect = new System.Windows.Forms.Label();
            this.txtOverlapActualSizeWidth = new System.Windows.Forms.TextBox();
            this.lblOverlapActualSizeWidth = new System.Windows.Forms.Label();
            this.txtOverlapActualSizeHeight = new System.Windows.Forms.TextBox();
            this.lblOverlapActualSizeHeight = new System.Windows.Forms.Label();
            this.gbGutterActualSize = new System.Windows.Forms.GroupBox();
            this.txtGutterFlat = new System.Windows.Forms.TextBox();
            this.lblGutterActualSizeFlat = new System.Windows.Forms.Label();
            this.txtGutterErect = new System.Windows.Forms.TextBox();
            this.lblGutterActualSizeErect = new System.Windows.Forms.Label();
            this.txtGutterActualSizeWidth = new System.Windows.Forms.TextBox();
            this.lblGutterActualSizeWidth = new System.Windows.Forms.Label();
            this.txtGutterActualSizeHeight = new System.Windows.Forms.TextBox();
            this.lblGutterActualSizeHeight = new System.Windows.Forms.Label();
            this.gbPageSetting = new System.Windows.Forms.GroupBox();
            this.chkPrinterLandscape = new System.Windows.Forms.CheckBox();
            this.chkPrintOnePage = new System.Windows.Forms.CheckBox();
            this.gbFlowDirection = new System.Windows.Forms.GroupBox();
            this.rbLeftToRight = new System.Windows.Forms.RadioButton();
            this.rbTopToBottom = new System.Windows.Forms.RadioButton();
            this.gbPrinterMargins = new System.Windows.Forms.GroupBox();
            this.rbFitToPage = new System.Windows.Forms.RadioButton();
            this.rbActualPageSize = new System.Windows.Forms.RadioButton();
            this.rbTwoByOne = new System.Windows.Forms.RadioButton();
            this.rbTwoByTwo = new System.Windows.Forms.RadioButton();
            this.rbThreeByTwo = new System.Windows.Forms.RadioButton();
            this.rbThreeByThree = new System.Windows.Forms.RadioButton();
            this.gbResolution = new System.Windows.Forms.GroupBox();
            this.rbDefaultDPIPrintResolution = new System.Windows.Forms.RadioButton();
            this.rbCustomDPIPrintResolution = new System.Windows.Forms.RadioButton();
            this.txtDPIPrintResolutionValue = new System.Windows.Forms.TextBox();
            this.lblDPIPrintResolution = new System.Windows.Forms.Label();
            this.gbPageMargin = new System.Windows.Forms.GroupBox();
            this.lblPrinterMarginsTop = new System.Windows.Forms.Label();
            this.txtPrinterMarginsTop = new System.Windows.Forms.TextBox();
            this.lblPrinterMarginsLeft = new System.Windows.Forms.Label();
            this.txtPrinterMarginsLeft = new System.Windows.Forms.TextBox();
            this.lblPrinterMarginsRight = new System.Windows.Forms.Label();
            this.txtPrinterMarginsRight = new System.Windows.Forms.TextBox();
            this.lblPrinterMarginsBottom = new System.Windows.Forms.Label();
            this.txtPrinterMarginsBottom = new System.Windows.Forms.TextBox();
            this.gbPrint = new System.Windows.Forms.GroupBox();
            this.chkShowProgress = new System.Windows.Forms.CheckBox();
            this.chkBackground = new System.Windows.Forms.CheckBox();
            this.fontDialogFooter = new System.Windows.Forms.FontDialog();
            this.timerToRefresh = new System.Windows.Forms.Timer(this.components);
            this.pnlExtPanelContainer.SuspendLayout();
            this.gbFooterMargins.SuspendLayout();
            this.gbFooterFont.SuspendLayout();
            this.gbOverlapActualSize.SuspendLayout();
            this.gbGutterActualSize.SuspendLayout();
            this.gbPageSetting.SuspendLayout();
            this.gbFlowDirection.SuspendLayout();
            this.gbPrinterMargins.SuspendLayout();
            this.gbResolution.SuspendLayout();
            this.gbPageMargin.SuspendLayout();
            this.gbPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlExtPanelContainer
            // 
            this.pnlExtPanelContainer.Controls.Add(this.gbFooterMargins);
            this.pnlExtPanelContainer.Controls.Add(this.gbFooterFont);
            this.pnlExtPanelContainer.Controls.Add(this.gbOverlapActualSize);
            this.pnlExtPanelContainer.Controls.Add(this.gbGutterActualSize);
            this.pnlExtPanelContainer.Controls.Add(this.gbPageSetting);
            this.pnlExtPanelContainer.Controls.Add(this.gbFlowDirection);
            this.pnlExtPanelContainer.Controls.Add(this.gbPrinterMargins);
            this.pnlExtPanelContainer.Controls.Add(this.gbResolution);
            this.pnlExtPanelContainer.Controls.Add(this.gbPageMargin);
            this.pnlExtPanelContainer.Controls.Add(this.gbPrint);
            this.pnlExtPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExtPanelContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlExtPanelContainer.Name = "pnlExtPanelContainer";
            this.pnlExtPanelContainer.Size = new System.Drawing.Size(411, 318);
            this.pnlExtPanelContainer.TabIndex = 0;
            // 
            // gbFooterMargins
            // 
            this.gbFooterMargins.Controls.Add(this.lblFooterMarginsTop);
            this.gbFooterMargins.Controls.Add(this.txtFooterMarginsTop);
            this.gbFooterMargins.Controls.Add(this.lblFooterMarginsLeft);
            this.gbFooterMargins.Controls.Add(this.txtFooterMarginsLeft);
            this.gbFooterMargins.Controls.Add(this.lblFooterMarginsRight);
            this.gbFooterMargins.Controls.Add(this.txtFooterMarginsRight);
            this.gbFooterMargins.Controls.Add(this.lblFooterMarginsBottom);
            this.gbFooterMargins.Controls.Add(this.txtFooterMarginsBottom);
            this.gbFooterMargins.Location = new System.Drawing.Point(0, -1);
            this.gbFooterMargins.Name = "gbFooterMargins";
            this.gbFooterMargins.Size = new System.Drawing.Size(207, 66);
            this.gbFooterMargins.TabIndex = 1;
            this.gbFooterMargins.TabStop = false;
            this.gbFooterMargins.Text = "Footer Margin";
            // 
            // lblFooterMarginsTop
            // 
            this.lblFooterMarginsTop.AutoSize = true;
            this.lblFooterMarginsTop.Location = new System.Drawing.Point(10, 19);
            this.lblFooterMarginsTop.Name = "lblFooterMarginsTop";
            this.lblFooterMarginsTop.Size = new System.Drawing.Size(26, 13);
            this.lblFooterMarginsTop.TabIndex = 2;
            this.lblFooterMarginsTop.Text = "Top";
            // 
            // txtFooterMarginsTop
            // 
            this.txtFooterMarginsTop.Location = new System.Drawing.Point(40, 15);
            this.txtFooterMarginsTop.Name = "txtFooterMarginsTop";
            this.txtFooterMarginsTop.ShortcutsEnabled = false;
            this.txtFooterMarginsTop.Size = new System.Drawing.Size(54, 20);
            this.txtFooterMarginsTop.TabIndex = 3;
            // 
            // lblFooterMarginsLeft
            // 
            this.lblFooterMarginsLeft.AutoSize = true;
            this.lblFooterMarginsLeft.Location = new System.Drawing.Point(115, 19);
            this.lblFooterMarginsLeft.Name = "lblFooterMarginsLeft";
            this.lblFooterMarginsLeft.Size = new System.Drawing.Size(25, 13);
            this.lblFooterMarginsLeft.TabIndex = 4;
            this.lblFooterMarginsLeft.Text = "Left";
            // 
            // txtFooterMarginsLeft
            // 
            this.txtFooterMarginsLeft.Location = new System.Drawing.Point(144, 15);
            this.txtFooterMarginsLeft.Name = "txtFooterMarginsLeft";
            this.txtFooterMarginsLeft.ShortcutsEnabled = false;
            this.txtFooterMarginsLeft.Size = new System.Drawing.Size(54, 20);
            this.txtFooterMarginsLeft.TabIndex = 5;
            // 
            // lblFooterMarginsRight
            // 
            this.lblFooterMarginsRight.AutoSize = true;
            this.lblFooterMarginsRight.Location = new System.Drawing.Point(4, 43);
            this.lblFooterMarginsRight.Name = "lblFooterMarginsRight";
            this.lblFooterMarginsRight.Size = new System.Drawing.Size(32, 13);
            this.lblFooterMarginsRight.TabIndex = 6;
            this.lblFooterMarginsRight.Text = "Right";
            // 
            // txtFooterMarginsRight
            // 
            this.txtFooterMarginsRight.Location = new System.Drawing.Point(40, 39);
            this.txtFooterMarginsRight.Name = "txtFooterMarginsRight";
            this.txtFooterMarginsRight.ShortcutsEnabled = false;
            this.txtFooterMarginsRight.Size = new System.Drawing.Size(54, 20);
            this.txtFooterMarginsRight.TabIndex = 7;
            // 
            // lblFooterMarginsBottom
            // 
            this.lblFooterMarginsBottom.AutoSize = true;
            this.lblFooterMarginsBottom.Location = new System.Drawing.Point(100, 43);
            this.lblFooterMarginsBottom.Name = "lblFooterMarginsBottom";
            this.lblFooterMarginsBottom.Size = new System.Drawing.Size(40, 13);
            this.lblFooterMarginsBottom.TabIndex = 8;
            this.lblFooterMarginsBottom.Text = "Bottom";
            // 
            // txtFooterMarginsBottom
            // 
            this.txtFooterMarginsBottom.Location = new System.Drawing.Point(144, 39);
            this.txtFooterMarginsBottom.Name = "txtFooterMarginsBottom";
            this.txtFooterMarginsBottom.ShortcutsEnabled = false;
            this.txtFooterMarginsBottom.Size = new System.Drawing.Size(54, 20);
            this.txtFooterMarginsBottom.TabIndex = 9;
            // 
            // gbFooterFont
            // 
            this.gbFooterFont.Controls.Add(this.btnFooterFont);
            this.gbFooterFont.Controls.Add(this.lblFooterFont);
            this.gbFooterFont.Location = new System.Drawing.Point(212, -1);
            this.gbFooterFont.Name = "gbFooterFont";
            this.gbFooterFont.Size = new System.Drawing.Size(195, 66);
            this.gbFooterFont.TabIndex = 2;
            this.gbFooterFont.TabStop = false;
            this.gbFooterFont.Text = "Footer Font";
            // 
            // btnFooterFont
            // 
            this.btnFooterFont.Location = new System.Drawing.Point(165, 20);
            this.btnFooterFont.Name = "btnFooterFont";
            this.btnFooterFont.Size = new System.Drawing.Size(24, 40);
            this.btnFooterFont.TabIndex = 0;
            this.btnFooterFont.Text = "...";
            this.btnFooterFont.UseVisualStyleBackColor = true;
            this.btnFooterFont.Click += new System.EventHandler(this.btnFooterFont_Click);
            // 
            // lblFooterFont
            // 
            this.lblFooterFont.BackColor = System.Drawing.Color.White;
            this.lblFooterFont.Location = new System.Drawing.Point(9, 20);
            this.lblFooterFont.Name = "lblFooterFont";
            this.lblFooterFont.Size = new System.Drawing.Size(150, 39);
            this.lblFooterFont.TabIndex = 11;
            this.lblFooterFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFooterFont.Click += new System.EventHandler(this.lblFooterFont_Click);
            // 
            // gbOverlapActualSize
            // 
            this.gbOverlapActualSize.Controls.Add(this.txtOverlapFlat);
            this.gbOverlapActualSize.Controls.Add(this.lblOverlapActualSizeFlat);
            this.gbOverlapActualSize.Controls.Add(this.txtOverlapErect);
            this.gbOverlapActualSize.Controls.Add(this.lblOverlapActualSizeErect);
            this.gbOverlapActualSize.Controls.Add(this.txtOverlapActualSizeWidth);
            this.gbOverlapActualSize.Controls.Add(this.lblOverlapActualSizeWidth);
            this.gbOverlapActualSize.Controls.Add(this.txtOverlapActualSizeHeight);
            this.gbOverlapActualSize.Controls.Add(this.lblOverlapActualSizeHeight);
            this.gbOverlapActualSize.Location = new System.Drawing.Point(0, 67);
            this.gbOverlapActualSize.Name = "gbOverlapActualSize";
            this.gbOverlapActualSize.Size = new System.Drawing.Size(207, 60);
            this.gbOverlapActualSize.TabIndex = 3;
            this.gbOverlapActualSize.TabStop = false;
            this.gbOverlapActualSize.Text = "Overlap Actual Size";
            // 
            // txtOverlapFlat
            // 
            this.txtOverlapFlat.Location = new System.Drawing.Point(40, 14);
            this.txtOverlapFlat.Name = "txtOverlapFlat";
            this.txtOverlapFlat.ShortcutsEnabled = false;
            this.txtOverlapFlat.Size = new System.Drawing.Size(54, 20);
            this.txtOverlapFlat.TabIndex = 16;
            // 
            // lblOverlapActualSizeFlat
            // 
            this.lblOverlapActualSizeFlat.AutoSize = true;
            this.lblOverlapActualSizeFlat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverlapActualSizeFlat.Location = new System.Drawing.Point(12, 18);
            this.lblOverlapActualSizeFlat.Name = "lblOverlapActualSizeFlat";
            this.lblOverlapActualSizeFlat.Size = new System.Drawing.Size(24, 13);
            this.lblOverlapActualSizeFlat.TabIndex = 15;
            this.lblOverlapActualSizeFlat.Text = "Flat";
            // 
            // txtOverlapErect
            // 
            this.txtOverlapErect.Location = new System.Drawing.Point(40, 36);
            this.txtOverlapErect.Name = "txtOverlapErect";
            this.txtOverlapErect.ShortcutsEnabled = false;
            this.txtOverlapErect.Size = new System.Drawing.Size(54, 20);
            this.txtOverlapErect.TabIndex = 18;
            // 
            // lblOverlapActualSizeErect
            // 
            this.lblOverlapActualSizeErect.AutoSize = true;
            this.lblOverlapActualSizeErect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverlapActualSizeErect.Location = new System.Drawing.Point(4, 40);
            this.lblOverlapActualSizeErect.Name = "lblOverlapActualSizeErect";
            this.lblOverlapActualSizeErect.Size = new System.Drawing.Size(32, 13);
            this.lblOverlapActualSizeErect.TabIndex = 17;
            this.lblOverlapActualSizeErect.Text = "Erect";
            // 
            // txtOverlapActualSizeWidth
            // 
            this.txtOverlapActualSizeWidth.Location = new System.Drawing.Point(144, 14);
            this.txtOverlapActualSizeWidth.Name = "txtOverlapActualSizeWidth";
            this.txtOverlapActualSizeWidth.ShortcutsEnabled = false;
            this.txtOverlapActualSizeWidth.Size = new System.Drawing.Size(54, 20);
            this.txtOverlapActualSizeWidth.TabIndex = 20;
            this.txtOverlapActualSizeWidth.Text = "50.9";
            // 
            // lblOverlapActualSizeWidth
            // 
            this.lblOverlapActualSizeWidth.AutoSize = true;
            this.lblOverlapActualSizeWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverlapActualSizeWidth.Location = new System.Drawing.Point(105, 18);
            this.lblOverlapActualSizeWidth.Name = "lblOverlapActualSizeWidth";
            this.lblOverlapActualSizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblOverlapActualSizeWidth.TabIndex = 19;
            this.lblOverlapActualSizeWidth.Text = "Width";
            // 
            // txtOverlapActualSizeHeight
            // 
            this.txtOverlapActualSizeHeight.Location = new System.Drawing.Point(144, 36);
            this.txtOverlapActualSizeHeight.Name = "txtOverlapActualSizeHeight";
            this.txtOverlapActualSizeHeight.ShortcutsEnabled = false;
            this.txtOverlapActualSizeHeight.Size = new System.Drawing.Size(54, 20);
            this.txtOverlapActualSizeHeight.TabIndex = 22;
            this.txtOverlapActualSizeHeight.Text = "50.9";
            // 
            // lblOverlapActualSizeHeight
            // 
            this.lblOverlapActualSizeHeight.AutoSize = true;
            this.lblOverlapActualSizeHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverlapActualSizeHeight.Location = new System.Drawing.Point(102, 40);
            this.lblOverlapActualSizeHeight.Name = "lblOverlapActualSizeHeight";
            this.lblOverlapActualSizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblOverlapActualSizeHeight.TabIndex = 21;
            this.lblOverlapActualSizeHeight.Text = "Height";
            // 
            // gbGutterActualSize
            // 
            this.gbGutterActualSize.Controls.Add(this.txtGutterFlat);
            this.gbGutterActualSize.Controls.Add(this.lblGutterActualSizeFlat);
            this.gbGutterActualSize.Controls.Add(this.txtGutterErect);
            this.gbGutterActualSize.Controls.Add(this.lblGutterActualSizeErect);
            this.gbGutterActualSize.Controls.Add(this.txtGutterActualSizeWidth);
            this.gbGutterActualSize.Controls.Add(this.lblGutterActualSizeWidth);
            this.gbGutterActualSize.Controls.Add(this.txtGutterActualSizeHeight);
            this.gbGutterActualSize.Controls.Add(this.lblGutterActualSizeHeight);
            this.gbGutterActualSize.Location = new System.Drawing.Point(212, 67);
            this.gbGutterActualSize.Name = "gbGutterActualSize";
            this.gbGutterActualSize.Size = new System.Drawing.Size(195, 60);
            this.gbGutterActualSize.TabIndex = 4;
            this.gbGutterActualSize.TabStop = false;
            this.gbGutterActualSize.Text = "Fit to Page Gutter";
            // 
            // txtGutterFlat
            // 
            this.txtGutterFlat.Location = new System.Drawing.Point(40, 14);
            this.txtGutterFlat.Name = "txtGutterFlat";
            this.txtGutterFlat.ShortcutsEnabled = false;
            this.txtGutterFlat.Size = new System.Drawing.Size(54, 20);
            this.txtGutterFlat.TabIndex = 21;
            // 
            // lblGutterActualSizeFlat
            // 
            this.lblGutterActualSizeFlat.AutoSize = true;
            this.lblGutterActualSizeFlat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGutterActualSizeFlat.Location = new System.Drawing.Point(12, 18);
            this.lblGutterActualSizeFlat.Name = "lblGutterActualSizeFlat";
            this.lblGutterActualSizeFlat.Size = new System.Drawing.Size(24, 13);
            this.lblGutterActualSizeFlat.TabIndex = 20;
            this.lblGutterActualSizeFlat.Text = "Flat";
            // 
            // txtGutterErect
            // 
            this.txtGutterErect.Location = new System.Drawing.Point(40, 36);
            this.txtGutterErect.Name = "txtGutterErect";
            this.txtGutterErect.ShortcutsEnabled = false;
            this.txtGutterErect.Size = new System.Drawing.Size(54, 20);
            this.txtGutterErect.TabIndex = 23;
            // 
            // lblGutterActualSizeErect
            // 
            this.lblGutterActualSizeErect.AutoSize = true;
            this.lblGutterActualSizeErect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGutterActualSizeErect.Location = new System.Drawing.Point(4, 40);
            this.lblGutterActualSizeErect.Name = "lblGutterActualSizeErect";
            this.lblGutterActualSizeErect.Size = new System.Drawing.Size(32, 13);
            this.lblGutterActualSizeErect.TabIndex = 22;
            this.lblGutterActualSizeErect.Text = "Erect";
            // 
            // txtGutterActualSizeWidth
            // 
            this.txtGutterActualSizeWidth.Location = new System.Drawing.Point(137, 14);
            this.txtGutterActualSizeWidth.Name = "txtGutterActualSizeWidth";
            this.txtGutterActualSizeWidth.ShortcutsEnabled = false;
            this.txtGutterActualSizeWidth.Size = new System.Drawing.Size(54, 20);
            this.txtGutterActualSizeWidth.TabIndex = 20;
            this.txtGutterActualSizeWidth.Text = "0";
            // 
            // lblGutterActualSizeWidth
            // 
            this.lblGutterActualSizeWidth.AutoSize = true;
            this.lblGutterActualSizeWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGutterActualSizeWidth.Location = new System.Drawing.Point(100, 18);
            this.lblGutterActualSizeWidth.Name = "lblGutterActualSizeWidth";
            this.lblGutterActualSizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblGutterActualSizeWidth.TabIndex = 19;
            this.lblGutterActualSizeWidth.Text = "Width";
            // 
            // txtGutterActualSizeHeight
            // 
            this.txtGutterActualSizeHeight.Location = new System.Drawing.Point(137, 36);
            this.txtGutterActualSizeHeight.Name = "txtGutterActualSizeHeight";
            this.txtGutterActualSizeHeight.ShortcutsEnabled = false;
            this.txtGutterActualSizeHeight.Size = new System.Drawing.Size(54, 20);
            this.txtGutterActualSizeHeight.TabIndex = 22;
            this.txtGutterActualSizeHeight.Text = "0";
            // 
            // lblGutterActualSizeHeight
            // 
            this.lblGutterActualSizeHeight.AutoSize = true;
            this.lblGutterActualSizeHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGutterActualSizeHeight.Location = new System.Drawing.Point(97, 40);
            this.lblGutterActualSizeHeight.Name = "lblGutterActualSizeHeight";
            this.lblGutterActualSizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblGutterActualSizeHeight.TabIndex = 21;
            this.lblGutterActualSizeHeight.Text = "Height";
            // 
            // gbPageSetting
            // 
            this.gbPageSetting.Controls.Add(this.chkPrinterLandscape);
            this.gbPageSetting.Controls.Add(this.chkPrintOnePage);
            this.gbPageSetting.Location = new System.Drawing.Point(0, 131);
            this.gbPageSetting.Name = "gbPageSetting";
            this.gbPageSetting.Size = new System.Drawing.Size(207, 60);
            this.gbPageSetting.TabIndex = 9;
            this.gbPageSetting.TabStop = false;
            this.gbPageSetting.Text = "Page Setting";
            // 
            // chkPrinterLandscape
            // 
            this.chkPrinterLandscape.AutoSize = true;
            this.chkPrinterLandscape.Checked = true;
            this.chkPrinterLandscape.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrinterLandscape.Location = new System.Drawing.Point(6, 17);
            this.chkPrinterLandscape.Name = "chkPrinterLandscape";
            this.chkPrinterLandscape.Size = new System.Drawing.Size(154, 17);
            this.chkPrinterLandscape.TabIndex = 0;
            this.chkPrinterLandscape.Text = "Do Not Change Orientation";
            this.chkPrinterLandscape.UseVisualStyleBackColor = true;
            // 
            // chkPrintOnePage
            // 
            this.chkPrintOnePage.AutoSize = true;
            this.chkPrintOnePage.Checked = true;
            this.chkPrintOnePage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintOnePage.Location = new System.Drawing.Point(6, 38);
            this.chkPrintOnePage.Name = "chkPrintOnePage";
            this.chkPrintOnePage.Size = new System.Drawing.Size(183, 17);
            this.chkPrintOnePage.TabIndex = 1;
            this.chkPrintOnePage.Text = "Print to Only One Sheet per Page";
            this.chkPrintOnePage.UseVisualStyleBackColor = true;
            // 
            // gbFlowDirection
            // 
            this.gbFlowDirection.Controls.Add(this.rbLeftToRight);
            this.gbFlowDirection.Controls.Add(this.rbTopToBottom);
            this.gbFlowDirection.Location = new System.Drawing.Point(212, 131);
            this.gbFlowDirection.Name = "gbFlowDirection";
            this.gbFlowDirection.Size = new System.Drawing.Size(195, 60);
            this.gbFlowDirection.TabIndex = 9;
            this.gbFlowDirection.TabStop = false;
            this.gbFlowDirection.Text = "Layout Direction";
            // 
            // rbLeftToRight
            // 
            this.rbLeftToRight.AutoSize = true;
            this.rbLeftToRight.Checked = true;
            this.rbLeftToRight.Location = new System.Drawing.Point(15, 19);
            this.rbLeftToRight.Name = "rbLeftToRight";
            this.rbLeftToRight.Size = new System.Drawing.Size(83, 17);
            this.rbLeftToRight.TabIndex = 0;
            this.rbLeftToRight.TabStop = true;
            this.rbLeftToRight.Text = "Left to Right";
            this.rbLeftToRight.UseVisualStyleBackColor = true;
            // 
            // rbTopToBottom
            // 
            this.rbTopToBottom.AutoSize = true;
            this.rbTopToBottom.Location = new System.Drawing.Point(15, 38);
            this.rbTopToBottom.Name = "rbTopToBottom";
            this.rbTopToBottom.Size = new System.Drawing.Size(92, 17);
            this.rbTopToBottom.TabIndex = 1;
            this.rbTopToBottom.Text = "Top to Bottom";
            this.rbTopToBottom.UseVisualStyleBackColor = true;
            // 
            // gbPrinterMargins
            // 
            this.gbPrinterMargins.Controls.Add(this.rbFitToPage);
            this.gbPrinterMargins.Controls.Add(this.rbActualPageSize);
            this.gbPrinterMargins.Controls.Add(this.rbTwoByOne);
            this.gbPrinterMargins.Controls.Add(this.rbTwoByTwo);
            this.gbPrinterMargins.Controls.Add(this.rbThreeByTwo);
            this.gbPrinterMargins.Controls.Add(this.rbThreeByThree);
            this.gbPrinterMargins.Location = new System.Drawing.Point(0, 191);
            this.gbPrinterMargins.Name = "gbPrinterMargins";
            this.gbPrinterMargins.Size = new System.Drawing.Size(207, 57);
            this.gbPrinterMargins.TabIndex = 5;
            this.gbPrinterMargins.TabStop = false;
            this.gbPrinterMargins.Text = "Pages per Sheet";
            // 
            // rbFitToPage
            // 
            this.rbFitToPage.AutoSize = true;
            this.rbFitToPage.Checked = true;
            this.rbFitToPage.Location = new System.Drawing.Point(12, 16);
            this.rbFitToPage.Name = "rbFitToPage";
            this.rbFitToPage.Size = new System.Drawing.Size(76, 17);
            this.rbFitToPage.TabIndex = 0;
            this.rbFitToPage.TabStop = true;
            this.rbFitToPage.Text = "Fit to Page";
            this.rbFitToPage.UseVisualStyleBackColor = true;
            this.rbFitToPage.CheckedChanged += new System.EventHandler(rbFitToPage_CheckedChanged);
            // 
            // rbActualPageSize
            // 
            this.rbActualPageSize.AutoSize = true;
            this.rbActualPageSize.Location = new System.Drawing.Point(12, 35);
            this.rbActualPageSize.Name = "rbActualPageSize";
            this.rbActualPageSize.Size = new System.Drawing.Size(78, 17);
            this.rbActualPageSize.TabIndex = 1;
            this.rbActualPageSize.Text = "Actual Size";
            this.rbActualPageSize.UseVisualStyleBackColor = true;
            this.rbActualPageSize.CheckedChanged += new System.EventHandler(rbActualPageSize_CheckedChanged);
            // 
            // rbTwoByOne
            // 
            this.rbTwoByOne.AutoSize = true;
            this.rbTwoByOne.Location = new System.Drawing.Point(99, 16);
            this.rbTwoByOne.Name = "rbTwoByOne";
            this.rbTwoByOne.Size = new System.Drawing.Size(42, 17);
            this.rbTwoByOne.TabIndex = 2;
            this.rbTwoByOne.Text = "2x1";
            this.rbTwoByOne.UseVisualStyleBackColor = true;
            this.rbTwoByOne.CheckedChanged += new System.EventHandler(rbTwoByOne_CheckedChanged);
            // 
            // rbTwoByTwo
            // 
            this.rbTwoByTwo.AutoSize = true;
            this.rbTwoByTwo.Location = new System.Drawing.Point(99, 35);
            this.rbTwoByTwo.Name = "rbTwoByTwo";
            this.rbTwoByTwo.Size = new System.Drawing.Size(42, 17);
            this.rbTwoByTwo.TabIndex = 3;
            this.rbTwoByTwo.Text = "2x2";
            this.rbTwoByTwo.UseVisualStyleBackColor = true;
            this.rbTwoByTwo.CheckedChanged += new System.EventHandler(rbTwoByTwo_CheckedChanged);
            // 
            // rbThreeByTwo
            // 
            this.rbThreeByTwo.AutoSize = true;
            this.rbThreeByTwo.Location = new System.Drawing.Point(155, 16);
            this.rbThreeByTwo.Name = "rbThreeByTwo";
            this.rbThreeByTwo.Size = new System.Drawing.Size(42, 17);
            this.rbThreeByTwo.TabIndex = 4;
            this.rbThreeByTwo.Text = "3x2";
            this.rbThreeByTwo.UseVisualStyleBackColor = true;
            this.rbThreeByTwo.CheckedChanged += new System.EventHandler(rbThreeByTwo_CheckedChanged);
            // 
            // rbThreeByThree
            // 
            this.rbThreeByThree.AutoSize = true;
            this.rbThreeByThree.Location = new System.Drawing.Point(155, 35);
            this.rbThreeByThree.Name = "rbThreeByThree";
            this.rbThreeByThree.Size = new System.Drawing.Size(42, 17);
            this.rbThreeByThree.TabIndex = 5;
            this.rbThreeByThree.Text = "3x3";
            this.rbThreeByThree.UseVisualStyleBackColor = true;
            this.rbThreeByThree.CheckedChanged += new System.EventHandler(rbThreeByThree_CheckedChanged);
            // 
            // gbResolution
            // 
            this.gbResolution.Controls.Add(this.rbDefaultDPIPrintResolution);
            this.gbResolution.Controls.Add(this.rbCustomDPIPrintResolution);
            this.gbResolution.Controls.Add(this.txtDPIPrintResolutionValue);
            this.gbResolution.Controls.Add(this.lblDPIPrintResolution);
            this.gbResolution.Location = new System.Drawing.Point(212, 192);
            this.gbResolution.Name = "gbResolution";
            this.gbResolution.Size = new System.Drawing.Size(195, 57);
            this.gbResolution.TabIndex = 31;
            this.gbResolution.TabStop = false;
            this.gbResolution.Text = "Print Resolution";
            // 
            // rbDefaultDPIPrintResolution
            // 
            this.rbDefaultDPIPrintResolution.AutoSize = true;
            this.rbDefaultDPIPrintResolution.Checked = true;
            this.rbDefaultDPIPrintResolution.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbDefaultDPIPrintResolution.Location = new System.Drawing.Point(15, 15);
            this.rbDefaultDPIPrintResolution.Name = "rbDefaultDPIPrintResolution";
            this.rbDefaultDPIPrintResolution.Size = new System.Drawing.Size(59, 17);
            this.rbDefaultDPIPrintResolution.TabIndex = 32;
            this.rbDefaultDPIPrintResolution.TabStop = true;
            this.rbDefaultDPIPrintResolution.Text = "Default";
            this.rbDefaultDPIPrintResolution.UseVisualStyleBackColor = true;
            this.rbDefaultDPIPrintResolution.CheckedChanged += new System.EventHandler(this.rbDefaultDPIPrintResolution_CheckedChanged);
            // 
            // rbCustomDPIPrintResolution
            // 
            this.rbCustomDPIPrintResolution.AutoSize = true;
            this.rbCustomDPIPrintResolution.Location = new System.Drawing.Point(16, 34);
            this.rbCustomDPIPrintResolution.Name = "rbCustomDPIPrintResolution";
            this.rbCustomDPIPrintResolution.Size = new System.Drawing.Size(60, 17);
            this.rbCustomDPIPrintResolution.TabIndex = 33;
            this.rbCustomDPIPrintResolution.TabStop = true;
            this.rbCustomDPIPrintResolution.Text = "Custom";
            this.rbCustomDPIPrintResolution.UseVisualStyleBackColor = true;
            // 
            // txtDPIPrintResolutionValue
            // 
            this.txtDPIPrintResolutionValue.Enabled = false;
            this.txtDPIPrintResolutionValue.Location = new System.Drawing.Point(77, 32);
            this.txtDPIPrintResolutionValue.Name = "txtDPIPrintResolutionValue";
            this.txtDPIPrintResolutionValue.ShortcutsEnabled = false;
            this.txtDPIPrintResolutionValue.Size = new System.Drawing.Size(65, 20);
            this.txtDPIPrintResolutionValue.TabIndex = 35;
            // 
            // lblDPIPrintResolution
            // 
            this.lblDPIPrintResolution.AutoSize = true;
            this.lblDPIPrintResolution.Location = new System.Drawing.Point(144, 36);
            this.lblDPIPrintResolution.Name = "lblDPIPrintResolution";
            this.lblDPIPrintResolution.Size = new System.Drawing.Size(21, 13);
            this.lblDPIPrintResolution.TabIndex = 34;
            this.lblDPIPrintResolution.Text = "dpi";
            // 
            // gbPageMargin
            // 
            this.gbPageMargin.Controls.Add(this.lblPrinterMarginsTop);
            this.gbPageMargin.Controls.Add(this.txtPrinterMarginsTop);
            this.gbPageMargin.Controls.Add(this.lblPrinterMarginsLeft);
            this.gbPageMargin.Controls.Add(this.txtPrinterMarginsLeft);
            this.gbPageMargin.Controls.Add(this.lblPrinterMarginsRight);
            this.gbPageMargin.Controls.Add(this.txtPrinterMarginsRight);
            this.gbPageMargin.Controls.Add(this.lblPrinterMarginsBottom);
            this.gbPageMargin.Controls.Add(this.txtPrinterMarginsBottom);
            this.gbPageMargin.Location = new System.Drawing.Point(0, 250);
            this.gbPageMargin.Name = "gbPageMargin";
            this.gbPageMargin.Size = new System.Drawing.Size(207, 63);
            this.gbPageMargin.TabIndex = 7;
            this.gbPageMargin.TabStop = false;
            this.gbPageMargin.Text = "Page Margin";
            // 
            // lblPrinterMarginsTop
            // 
            this.lblPrinterMarginsTop.AutoSize = true;
            this.lblPrinterMarginsTop.Location = new System.Drawing.Point(13, 18);
            this.lblPrinterMarginsTop.Name = "lblPrinterMarginsTop";
            this.lblPrinterMarginsTop.Size = new System.Drawing.Size(26, 13);
            this.lblPrinterMarginsTop.TabIndex = 37;
            this.lblPrinterMarginsTop.Text = "Top";
            // 
            // txtPrinterMarginsTop
            // 
            this.txtPrinterMarginsTop.Location = new System.Drawing.Point(43, 14);
            this.txtPrinterMarginsTop.Name = "txtPrinterMarginsTop";
            this.txtPrinterMarginsTop.ShortcutsEnabled = false;
            this.txtPrinterMarginsTop.Size = new System.Drawing.Size(54, 20);
            this.txtPrinterMarginsTop.TabIndex = 38;
            // 
            // lblPrinterMarginsLeft
            // 
            this.lblPrinterMarginsLeft.AutoSize = true;
            this.lblPrinterMarginsLeft.Location = new System.Drawing.Point(116, 18);
            this.lblPrinterMarginsLeft.Name = "lblPrinterMarginsLeft";
            this.lblPrinterMarginsLeft.Size = new System.Drawing.Size(25, 13);
            this.lblPrinterMarginsLeft.TabIndex = 39;
            this.lblPrinterMarginsLeft.Text = "Left";
            // 
            // txtPrinterMarginsLeft
            // 
            this.txtPrinterMarginsLeft.Location = new System.Drawing.Point(144, 14);
            this.txtPrinterMarginsLeft.Name = "txtPrinterMarginsLeft";
            this.txtPrinterMarginsLeft.ShortcutsEnabled = false;
            this.txtPrinterMarginsLeft.Size = new System.Drawing.Size(54, 20);
            this.txtPrinterMarginsLeft.TabIndex = 40;
            // 
            // lblPrinterMarginsRight
            // 
            this.lblPrinterMarginsRight.AutoSize = true;
            this.lblPrinterMarginsRight.Location = new System.Drawing.Point(7, 42);
            this.lblPrinterMarginsRight.Name = "lblPrinterMarginsRight";
            this.lblPrinterMarginsRight.Size = new System.Drawing.Size(32, 13);
            this.lblPrinterMarginsRight.TabIndex = 41;
            this.lblPrinterMarginsRight.Text = "Right";
            // 
            // txtPrinterMarginsRight
            // 
            this.txtPrinterMarginsRight.Location = new System.Drawing.Point(43, 38);
            this.txtPrinterMarginsRight.Name = "txtPrinterMarginsRight";
            this.txtPrinterMarginsRight.ShortcutsEnabled = false;
            this.txtPrinterMarginsRight.Size = new System.Drawing.Size(54, 20);
            this.txtPrinterMarginsRight.TabIndex = 42;
            // 
            // lblPrinterMarginsBottom
            // 
            this.lblPrinterMarginsBottom.AutoSize = true;
            this.lblPrinterMarginsBottom.Location = new System.Drawing.Point(101, 42);
            this.lblPrinterMarginsBottom.Name = "lblPrinterMarginsBottom";
            this.lblPrinterMarginsBottom.Size = new System.Drawing.Size(40, 13);
            this.lblPrinterMarginsBottom.TabIndex = 43;
            this.lblPrinterMarginsBottom.Text = "Bottom";
            // 
            // txtPrinterMarginsBottom
            // 
            this.txtPrinterMarginsBottom.Location = new System.Drawing.Point(144, 38);
            this.txtPrinterMarginsBottom.Name = "txtPrinterMarginsBottom";
            this.txtPrinterMarginsBottom.ShortcutsEnabled = false;
            this.txtPrinterMarginsBottom.Size = new System.Drawing.Size(54, 20);
            this.txtPrinterMarginsBottom.TabIndex = 44;
            // 
            // gbPrint
            // 
            this.gbPrint.Controls.Add(this.chkShowProgress);
            this.gbPrint.Controls.Add(this.chkBackground);
            this.gbPrint.Location = new System.Drawing.Point(212, 251);
            this.gbPrint.Name = "gbPrint";
            this.gbPrint.Size = new System.Drawing.Size(195, 63);
            this.gbPrint.TabIndex = 8;
            this.gbPrint.TabStop = false;
            this.gbPrint.Text = "Print";
            // 
            // chkShowProgress
            // 
            this.chkShowProgress.AutoSize = true;
            this.chkShowProgress.Checked = false;
            this.chkShowProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowProgress.Location = new System.Drawing.Point(17, 17);
            this.chkShowProgress.Name = "chkShowProgress";
            this.chkShowProgress.Size = new System.Drawing.Size(97, 17);
            this.chkShowProgress.TabIndex = 46;
            this.chkShowProgress.Text = "Show Progress";
            this.chkShowProgress.UseVisualStyleBackColor = true;
            this.chkShowProgress.CheckedChanged += new System.EventHandler(this.chkShowProgress_CheckedChanged);
            // 
            // chkBackground
            // 
            this.chkBackground.AutoSize = true;
            this.chkBackground.Checked = true;
            this.chkBackground.Enabled = false;
            this.chkBackground.Location = new System.Drawing.Point(17, 40);
            this.chkBackground.Name = "chkBackground";
            this.chkBackground.Size = new System.Drawing.Size(119, 17);
            this.chkBackground.TabIndex = 47;
            this.chkBackground.Text = "Print in Background";
            this.chkBackground.UseVisualStyleBackColor = true;
            this.chkBackground.CheckedChanged += new System.EventHandler(this.chkBackground_CheckedChanged);
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
            this.timerToRefresh.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gloExtendedPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlExtPanelContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "gloExtendedPropertiesControl";
            this.Size = new System.Drawing.Size(411, 318);
            this.pnlExtPanelContainer.ResumeLayout(false);
            this.gbFooterMargins.ResumeLayout(false);
            this.gbFooterMargins.PerformLayout();
            this.gbFooterFont.ResumeLayout(false);
            this.gbOverlapActualSize.ResumeLayout(false);
            this.gbOverlapActualSize.PerformLayout();
            this.gbGutterActualSize.ResumeLayout(false);
            this.gbGutterActualSize.PerformLayout();
            this.gbPageSetting.ResumeLayout(false);
            this.gbPageSetting.PerformLayout();
            this.gbFlowDirection.ResumeLayout(false);
            this.gbFlowDirection.PerformLayout();
            this.gbPrinterMargins.ResumeLayout(false);
            this.gbPrinterMargins.PerformLayout();
            this.gbResolution.ResumeLayout(false);
            this.gbResolution.PerformLayout();
            this.gbPageMargin.ResumeLayout(false);
            this.gbPageMargin.PerformLayout();
            this.gbPrint.ResumeLayout(false);
            this.gbPrint.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Label lblDPIPrintResolution;
        private System.Windows.Forms.TextBox txtDPIPrintResolutionValue;
        private System.Windows.Forms.RadioButton rbCustomDPIPrintResolution;
        private System.Windows.Forms.RadioButton rbDefaultDPIPrintResolution;
        private System.Windows.Forms.GroupBox gbPrinterMargins;
        private System.Windows.Forms.TextBox txtPrinterMarginsBottom;
        private System.Windows.Forms.TextBox txtPrinterMarginsLeft;
        private System.Windows.Forms.TextBox txtPrinterMarginsRight;
        private System.Windows.Forms.TextBox txtPrinterMarginsTop;
        private System.Windows.Forms.Label lblPrinterMarginsLeft;
        private System.Windows.Forms.Label lblPrinterMarginsRight;
        private System.Windows.Forms.Label lblPrinterMarginsBottom;
        private System.Windows.Forms.Label lblPrinterMarginsTop;
        private System.Windows.Forms.RadioButton rbActualPageSize;
        private System.Windows.Forms.RadioButton rbFitToPage;
        private System.Windows.Forms.Panel pnlExtPanelContainer;
        private System.Windows.Forms.GroupBox gbPrint;
        private System.Windows.Forms.CheckBox chkShowProgress;
        private System.Windows.Forms.CheckBox chkBackground;
        private System.Windows.Forms.Button btnFooterFont;
        private System.Windows.Forms.Label lblFooterMarginsTop;
        private System.Windows.Forms.TextBox txtFooterMarginsLeft;
        private System.Windows.Forms.Label lblFooterMarginsLeft;
        private System.Windows.Forms.TextBox txtFooterMarginsTop;
        private System.Windows.Forms.Label lblFooterMarginsRight;
        private System.Windows.Forms.TextBox txtFooterMarginsBottom;
        private System.Windows.Forms.Label lblFooterMarginsBottom;
        private System.Windows.Forms.TextBox txtFooterMarginsRight;
        private System.Windows.Forms.GroupBox gbFooterFont;
        private System.Windows.Forms.Label lblFooterFont;
        private System.Windows.Forms.GroupBox gbFooterMargins;
        private System.Windows.Forms.FontDialog fontDialogFooter;
        private System.Windows.Forms.GroupBox gbOverlapActualSize;
        private System.Windows.Forms.Label lblGutterActualSizeErect;
        private System.Windows.Forms.Label lblGutterActualSizeFlat;
        private System.Windows.Forms.TextBox txtGutterErect;
        private System.Windows.Forms.TextBox txtGutterFlat;
        private System.Windows.Forms.Label lblOverlapActualSizeErect;
        private System.Windows.Forms.Label lblOverlapActualSizeFlat;
        private System.Windows.Forms.TextBox txtOverlapErect;
        private System.Windows.Forms.TextBox txtOverlapFlat;
        private System.Windows.Forms.RadioButton rbThreeByThree;
        private System.Windows.Forms.RadioButton rbThreeByTwo;
        private System.Windows.Forms.RadioButton rbTwoByTwo;
        private System.Windows.Forms.RadioButton rbTwoByOne;
        private System.Windows.Forms.GroupBox gbPageMargin;
        private System.Windows.Forms.GroupBox gbGutterActualSize;
        private System.Windows.Forms.TextBox txtGutterActualSizeWidth;
        private System.Windows.Forms.Label lblGutterActualSizeWidth;
        private System.Windows.Forms.TextBox txtGutterActualSizeHeight;
        private System.Windows.Forms.Label lblGutterActualSizeHeight;
        private System.Windows.Forms.TextBox txtOverlapActualSizeWidth;
        private System.Windows.Forms.Label lblOverlapActualSizeWidth;
        private System.Windows.Forms.TextBox txtOverlapActualSizeHeight;
        private System.Windows.Forms.Label lblOverlapActualSizeHeight;
        private System.Windows.Forms.Timer timerToRefresh;
        private System.Windows.Forms.GroupBox gbFlowDirection;
        private System.Windows.Forms.RadioButton rbLeftToRight;
        private System.Windows.Forms.RadioButton rbTopToBottom;
        private System.Windows.Forms.GroupBox gbPageSetting;
        private System.Windows.Forms.CheckBox chkPrintOnePage;
        private System.Windows.Forms.CheckBox chkPrinterLandscape;
        private System.Windows.Forms.GroupBox gbResolution;
    }
}
