namespace gloReports.C1Reports
{
    partial class frmRpt_DailyMonthlyChargesPaySummary
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpCloseDate, dtpChrgEndDate, dtpChrgStartDate, dtPayCloseDate, dtpPayEndDate, dtpPayStartDate, dtpLastCloseDate };
            System.Windows.Forms.Control[] cntControls = { dtpCloseDate, dtpChrgEndDate, dtpChrgStartDate, dtPayCloseDate, dtpPayEndDate, dtpPayStartDate, dtpLastCloseDate };

            if (disposing && (components != null))
            {
                try
                {
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }

                }
                catch
                {
                }
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
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_DailyMonthlyChargesPaySummary));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnDailyClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnChrgSelectLoginUsrs = new System.Windows.Forms.Button();
            this.btnPaySelectLoginUsrs = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.crvRptViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label43 = new System.Windows.Forms.Label();
            this.pnlCloseDaySearch = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.chkClosedShwDetails = new System.Windows.Forms.CheckBox();
            this.chkPageBreak = new System.Windows.Forms.CheckBox();
            this.pnlLSTMonths = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.lbl_pnlProviderBottomBrd = new System.Windows.Forms.Label();
            this.pnlProviderBody = new System.Windows.Forms.Panel();
            this.trvMonths = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectCreditCard = new System.Windows.Forms.Button();
            this.btnSelectCreditCard = new System.Windows.Forms.Button();
            this.lbl_pnlProviderHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTopBrd = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.lblUserNm = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.dtpLastCloseDate = new System.Windows.Forms.DateTimePicker();
            this.lblLstClosedDt = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.flowPnlPayment = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPayCloseDate = new System.Windows.Forms.Panel();
            this.dtPayCloseDate = new System.Windows.Forms.DateTimePicker();
            this.label55 = new System.Windows.Forms.Label();
            this.pnlTransPayDate = new System.Windows.Forms.Panel();
            this.cmb_Paydatefilter = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.pnlPayDates = new System.Windows.Forms.Panel();
            this.dtpPayEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPayStartDate = new System.Windows.Forms.DateTimePicker();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbPaymentProvider = new System.Windows.Forms.ComboBox();
            this.btnClearPayProviders = new System.Windows.Forms.Button();
            this.btnBrowsePayProviders = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.cmbPayFacility = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.btnBrowsePayFacility = new System.Windows.Forms.Button();
            this.btnClearPayFacility = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.cmbPayUser = new System.Windows.Forms.ComboBox();
            this.btnBrowsePayUser = new System.Windows.Forms.Button();
            this.btnClearPayUser = new System.Windows.Forms.Button();
            this.lblPayUser = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.cmbPayTray = new System.Windows.Forms.ComboBox();
            this.btnBrowsePayTray = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.cmbPayProvider = new System.Windows.Forms.ComboBox();
            this.btnClearPayProvider = new System.Windows.Forms.Button();
            this.btnBrowsePayProvider = new System.Windows.Forms.Button();
            this.btnClearPayTray = new System.Windows.Forms.Button();
            this.lblPayTray = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnlPayType = new System.Windows.Forms.Panel();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.btnBrowsePayType = new System.Windows.Forms.Button();
            this.btnClearPayType = new System.Windows.Forms.Button();
            this.label56 = new System.Windows.Forms.Label();
            this.pnlPaySortBy = new System.Windows.Forms.Panel();
            this.cmbPaySortBy = new System.Windows.Forms.ComboBox();
            this.lblPaySortBy = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.pnlIncludeDtls = new System.Windows.Forms.Panel();
            this.chkPayIncludeDetails = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.flwPnlDailyCharges = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCloseDate = new System.Windows.Forms.Panel();
            this.dtpCloseDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.cmb_Chargesdatefilter = new System.Windows.Forms.ComboBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.pnlChrgDates = new System.Windows.Forms.Panel();
            this.dtpChrgEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpChrgStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.cmbChargesProvider = new System.Windows.Forms.ComboBox();
            this.btnClearCProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.pnlMultiFacility = new System.Windows.Forms.Panel();
            this.cmbChargesMultiFacility = new System.Windows.Forms.ComboBox();
            this.btnBrowseCMultiFacility = new System.Windows.Forms.Button();
            this.btnClearCMultiFacility = new System.Windows.Forms.Button();
            this.lblMultiFacility = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.cmbChargesUser = new System.Windows.Forms.ComboBox();
            this.btnBrowseCUser = new System.Windows.Forms.Button();
            this.btnClearCUser = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlMultiChargesTray = new System.Windows.Forms.Panel();
            this.cmbMultiChargesTray = new System.Windows.Forms.ComboBox();
            this.btnBrowseCMultiChargesTray = new System.Windows.Forms.Button();
            this.btnClearCMultiChargesTray = new System.Windows.Forms.Button();
            this.lblMultiChargesTray = new System.Windows.Forms.Label();
            this.pnlRunReportAs = new System.Windows.Forms.Panel();
            this.chkSealedCharges = new System.Windows.Forms.CheckBox();
            this.chkOpenCharges = new System.Windows.Forms.CheckBox();
            this.pnlLoginUsers = new System.Windows.Forms.Panel();
            this.cmbChrgSortBy = new System.Windows.Forms.ComboBox();
            this.lblChrgSortBy = new System.Windows.Forms.Label();
            this.chkIncludeAllTrays = new System.Windows.Forms.CheckBox();
            this.chkLoginUsers = new System.Windows.Forms.CheckBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlChargesShowHide = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnChrgDown = new System.Windows.Forms.Button();
            this.btnChrgUP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCloseDaySearch.SuspendLayout();
            this.panel17.SuspendLayout();
            this.pnlLSTMonths.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.panel15.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.flowPnlPayment.SuspendLayout();
            this.pnlPayCloseDate.SuspendLayout();
            this.pnlTransPayDate.SuspendLayout();
            this.pnlPayDates.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlPayType.SuspendLayout();
            this.pnlPaySortBy.SuspendLayout();
            this.pnlIncludeDtls.SuspendLayout();
            this.flwPnlDailyCharges.SuspendLayout();
            this.pnlCloseDate.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            this.pnlChrgDates.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlMultiFacility.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlMultiChargesTray.SuspendLayout();
            this.pnlRunReportAs.SuspendLayout();
            this.pnlLoginUsers.SuspendLayout();
            this.pnlChargesShowHide.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1284, 54);
            this.pnlToolStrip.TabIndex = 30;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateReport,
            this.tsb_btnExportReport,
            this.tsb_Print,
            this.tsb_btnDailyClose,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1284, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_GenerateReport
            // 
            this.tsb_GenerateReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateReport.Image")));
            this.tsb_GenerateReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateReport.Name = "tsb_GenerateReport";
            this.tsb_GenerateReport.Size = new System.Drawing.Size(113, 50);
            this.tsb_GenerateReport.Tag = "Generate Report";
            this.tsb_GenerateReport.Text = "&Generate Report";
            this.tsb_GenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateReport.ToolTipText = "Generate Report";
            this.tsb_GenerateReport.Click += new System.EventHandler(this.tsb_GenerateReport_Click);
            // 
            // tsb_btnExportReport
            // 
            this.tsb_btnExportReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnExportReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnExportReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnExportReport.Image")));
            this.tsb_btnExportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnExportReport.Name = "tsb_btnExportReport";
            this.tsb_btnExportReport.Size = new System.Drawing.Size(52, 50);
            this.tsb_btnExportReport.Tag = "Export";
            this.tsb_btnExportReport.Text = "&Export";
            this.tsb_btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnExportReport.Click += new System.EventHandler(this.tsb_btnExportReport_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_btnDailyClose
            // 
            this.tsb_btnDailyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnDailyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnDailyClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnDailyClose.Image")));
            this.tsb_btnDailyClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnDailyClose.Name = "tsb_btnDailyClose";
            this.tsb_btnDailyClose.Size = new System.Drawing.Size(80, 50);
            this.tsb_btnDailyClose.Tag = "DailyClose ";
            this.tsb_btnDailyClose.Text = "&Daily Close ";
            this.tsb_btnDailyClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnDailyClose.ToolTipText = "Daily Close ";
            this.tsb_btnDailyClose.Click += new System.EventHandler(this.tsb_btnDailyClose_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Daily Charges rpt.ico");
            this.imageList1.Images.SetKeyName(1, "Daily Payment rtp.ico");
            this.imageList1.Images.SetKeyName(2, "Daily close.ico");
            // 
            // btnChrgSelectLoginUsrs
            // 
            this.btnChrgSelectLoginUsrs.BackColor = System.Drawing.Color.Transparent;
            this.btnChrgSelectLoginUsrs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChrgSelectLoginUsrs.BackgroundImage")));
            this.btnChrgSelectLoginUsrs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChrgSelectLoginUsrs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChrgSelectLoginUsrs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChrgSelectLoginUsrs.Location = new System.Drawing.Point(3, 2);
            this.btnChrgSelectLoginUsrs.Name = "btnChrgSelectLoginUsrs";
            this.btnChrgSelectLoginUsrs.Size = new System.Drawing.Size(143, 25);
            this.btnChrgSelectLoginUsrs.TabIndex = 196;
            this.btnChrgSelectLoginUsrs.Text = "Select Logged In User";
            this.toolTip1.SetToolTip(this.btnChrgSelectLoginUsrs, "Select Logged In User");
            this.btnChrgSelectLoginUsrs.UseVisualStyleBackColor = false;
            this.btnChrgSelectLoginUsrs.Click += new System.EventHandler(this.btnChrgSelectLoginUsrs_Click);
            // 
            // btnPaySelectLoginUsrs
            // 
            this.btnPaySelectLoginUsrs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPaySelectLoginUsrs.BackColor = System.Drawing.Color.Transparent;
            this.btnPaySelectLoginUsrs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPaySelectLoginUsrs.BackgroundImage")));
            this.btnPaySelectLoginUsrs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPaySelectLoginUsrs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPaySelectLoginUsrs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaySelectLoginUsrs.Location = new System.Drawing.Point(3, 0);
            this.btnPaySelectLoginUsrs.Name = "btnPaySelectLoginUsrs";
            this.btnPaySelectLoginUsrs.Size = new System.Drawing.Size(143, 24);
            this.btnPaySelectLoginUsrs.TabIndex = 196;
            this.btnPaySelectLoginUsrs.Text = "Select Logged In User";
            this.toolTip1.SetToolTip(this.btnPaySelectLoginUsrs, "Select Logged In User");
            this.btnPaySelectLoginUsrs.UseVisualStyleBackColor = false;
            this.btnPaySelectLoginUsrs.Click += new System.EventHandler(this.btnPaySelectLoginUsrs_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnlChargesShowHide);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 585);
            this.panel1.TabIndex = 271;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.crvRptViewer);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 96);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(1284, 489);
            this.panel5.TabIndex = 272;
            // 
            // crvRptViewer
            // 
            this.crvRptViewer.ActiveViewIndex = -1;
            this.crvRptViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRptViewer.CausesValidation = false;
            this.crvRptViewer.DisplayBackgroundEdge = false;
            this.crvRptViewer.DisplayGroupTree = false;
            this.crvRptViewer.DisplayStatusBar = false;
            this.crvRptViewer.DisplayToolbar = false;
            this.crvRptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvRptViewer.EnableDrillDown = false;
            this.crvRptViewer.Location = new System.Drawing.Point(3, 3);
            this.crvRptViewer.Name = "crvRptViewer";
            this.crvRptViewer.ReportSource = "";
            this.crvRptViewer.SelectionFormula = "";
            this.crvRptViewer.ShowCloseButton = false;
            this.crvRptViewer.ShowGroupTreeButton = false;
            this.crvRptViewer.ShowPrintButton = false;
            this.crvRptViewer.ShowRefreshButton = false;
            this.crvRptViewer.Size = new System.Drawing.Size(1278, 483);
            this.crvRptViewer.TabIndex = 275;
            this.crvRptViewer.ViewTimeSelectionFormula = "";
            this.crvRptViewer.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.pnlCloseDaySearch);
            this.panel2.Controls.Add(this.flowPnlPayment);
            this.panel2.Controls.Add(this.flwPnlDailyCharges);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel2.Size = new System.Drawing.Size(1284, 72);
            this.panel2.TabIndex = 269;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(4, 71);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1276, 1);
            this.label43.TabIndex = 14;
            this.label43.Text = "label1";
            // 
            // pnlCloseDaySearch
            // 
            this.pnlCloseDaySearch.Controls.Add(this.panel17);
            this.pnlCloseDaySearch.Controls.Add(this.pnlLSTMonths);
            this.pnlCloseDaySearch.Controls.Add(this.panel15);
            this.pnlCloseDaySearch.Controls.Add(this.pnlPatients);
            this.pnlCloseDaySearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCloseDaySearch.Location = new System.Drawing.Point(4, 143);
            this.pnlCloseDaySearch.Name = "pnlCloseDaySearch";
            this.pnlCloseDaySearch.Size = new System.Drawing.Size(1276, 0);
            this.pnlCloseDaySearch.TabIndex = 270;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.chkClosedShwDetails);
            this.panel17.Controls.Add(this.chkPageBreak);
            this.panel17.Location = new System.Drawing.Point(237, 3);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(315, 24);
            this.panel17.TabIndex = 241;
            // 
            // chkClosedShwDetails
            // 
            this.chkClosedShwDetails.AutoSize = true;
            this.chkClosedShwDetails.Checked = true;
            this.chkClosedShwDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClosedShwDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClosedShwDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkClosedShwDetails.Location = new System.Drawing.Point(7, 3);
            this.chkClosedShwDetails.Name = "chkClosedShwDetails";
            this.chkClosedShwDetails.Size = new System.Drawing.Size(155, 18);
            this.chkClosedShwDetails.TabIndex = 2;
            this.chkClosedShwDetails.Text = "Include A/R by Provider";
            this.chkClosedShwDetails.UseVisualStyleBackColor = true;
            this.chkClosedShwDetails.CheckedChanged += new System.EventHandler(this.chkClosedShwDetails_CheckedChanged);
            // 
            // chkPageBreak
            // 
            this.chkPageBreak.AutoSize = true;
            this.chkPageBreak.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPageBreak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkPageBreak.Location = new System.Drawing.Point(165, 2);
            this.chkPageBreak.Name = "chkPageBreak";
            this.chkPageBreak.Size = new System.Drawing.Size(139, 18);
            this.chkPageBreak.TabIndex = 3;
            this.chkPageBreak.Text = "Page Break on Detail";
            this.chkPageBreak.UseVisualStyleBackColor = true;
            this.chkPageBreak.Visible = false;
            this.chkPageBreak.Click += new System.EventHandler(this.chkPageBreak_CheckedChanged);
            // 
            // pnlLSTMonths
            // 
            this.pnlLSTMonths.Controls.Add(this.panel16);
            this.pnlLSTMonths.Location = new System.Drawing.Point(555, 3);
            this.pnlLSTMonths.Name = "pnlLSTMonths";
            this.pnlLSTMonths.Size = new System.Drawing.Size(169, 68);
            this.pnlLSTMonths.TabIndex = 229;
            this.pnlLSTMonths.Visible = false;
            // 
            // panel16
            // 
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.panel16.Controls.Add(this.pnlProviderBody);
            this.panel16.Controls.Add(this.pnlProviderHeader);
            this.panel16.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.panel16.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.panel16.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(169, 68);
            this.panel16.TabIndex = 302;
            this.panel16.Visible = false;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 67);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(167, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            this.lbl_pnlProviderBottomBrd.Visible = false;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvMonths);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(167, 44);
            this.pnlProviderBody.TabIndex = 92;
            this.pnlProviderBody.Visible = false;
            // 
            // trvMonths
            // 
            this.trvMonths.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMonths.CheckBoxes = true;
            this.trvMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMonths.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvMonths.ForeColor = System.Drawing.Color.Black;
            this.trvMonths.Location = new System.Drawing.Point(0, 0);
            this.trvMonths.Name = "trvMonths";
            this.trvMonths.ShowLines = false;
            this.trvMonths.ShowPlusMinus = false;
            this.trvMonths.ShowRootLines = false;
            this.trvMonths.Size = new System.Drawing.Size(167, 44);
            this.trvMonths.TabIndex = 19;
            this.trvMonths.Visible = false;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.btnSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.lbl_pnlProviderHeaderBottomBrd);
            this.pnlProviderHeader.Controls.Add(this.lblCreditCard);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(167, 23);
            this.pnlProviderHeader.TabIndex = 91;
            this.pnlProviderHeader.Visible = false;
            // 
            // btnDeSelectCreditCard
            // 
            this.btnDeSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnDeSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectCreditCard.Image")));
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(105, 0);
            this.btnDeSelectCreditCard.Name = "btnDeSelectCreditCard";
            this.btnDeSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectCreditCard.TabIndex = 101;
            this.btnDeSelectCreditCard.Tag = "Select";
            this.btnDeSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnDeSelectCreditCard.Visible = false;
            // 
            // btnSelectCreditCard
            // 
            this.btnSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCreditCard.Image")));
            this.btnSelectCreditCard.Location = new System.Drawing.Point(136, 0);
            this.btnSelectCreditCard.Name = "btnSelectCreditCard";
            this.btnSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnSelectCreditCard.TabIndex = 100;
            this.btnSelectCreditCard.Tag = "Select";
            this.btnSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnSelectCreditCard.Visible = false;
            // 
            // lbl_pnlProviderHeaderBottomBrd
            // 
            this.lbl_pnlProviderHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlProviderHeaderBottomBrd.Name = "lbl_pnlProviderHeaderBottomBrd";
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(167, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 97;
            this.lbl_pnlProviderHeaderBottomBrd.Visible = false;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreditCard.Location = new System.Drawing.Point(0, 0);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(167, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "  Day to close :";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCreditCard.Visible = false;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 67);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            this.lbl_pnlProviderLeftBrd.Visible = false;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(168, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 67);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            this.lbl_pnlProviderRightBrd.Visible = false;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(169, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            this.lbl_pnlProviderTopBrd.Visible = false;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.lblUserNm);
            this.panel15.Controls.Add(this.label52);
            this.panel15.Controls.Add(this.label54);
            this.panel15.Location = new System.Drawing.Point(5, 30);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(229, 22);
            this.panel15.TabIndex = 209;
            // 
            // lblUserNm
            // 
            this.lblUserNm.AutoSize = true;
            this.lblUserNm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUserNm.Location = new System.Drawing.Point(125, 4);
            this.lblUserNm.Name = "lblUserNm";
            this.lblUserNm.Size = new System.Drawing.Size(35, 14);
            this.lblUserNm.TabIndex = 198;
            this.lblUserNm.Text = "User ";
            this.lblUserNm.Visible = false;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(123, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(0, 14);
            this.label52.TabIndex = 197;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(84, 4);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(41, 14);
            this.label54.TabIndex = 196;
            this.label54.Text = "User :";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label54.Visible = false;
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.dtpLastCloseDate);
            this.pnlPatients.Controls.Add(this.lblLstClosedDt);
            this.pnlPatients.Controls.Add(this.label51);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Location = new System.Drawing.Point(5, 5);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(229, 22);
            this.pnlPatients.TabIndex = 208;
            // 
            // dtpLastCloseDate
            // 
            this.dtpLastCloseDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpLastCloseDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpLastCloseDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpLastCloseDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpLastCloseDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpLastCloseDate.CustomFormat = "MM/dd/yyyy";
            this.dtpLastCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLastCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLastCloseDate.Location = new System.Drawing.Point(127, 0);
            this.dtpLastCloseDate.Name = "dtpLastCloseDate";
            this.dtpLastCloseDate.Size = new System.Drawing.Size(94, 22);
            this.dtpLastCloseDate.TabIndex = 199;
            // 
            // lblLstClosedDt
            // 
            this.lblLstClosedDt.AutoSize = true;
            this.lblLstClosedDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLstClosedDt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLstClosedDt.Location = new System.Drawing.Point(125, 4);
            this.lblLstClosedDt.Name = "lblLstClosedDt";
            this.lblLstClosedDt.Size = new System.Drawing.Size(20, 14);
            this.lblLstClosedDt.TabIndex = 198;
            this.lblLstClosedDt.Text = "Dt";
            this.lblLstClosedDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(123, 8);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(0, 14);
            this.label51.TabIndex = 197;
            // 
            // lblPatient
            // 
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(7, 4);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(118, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = " Close Date :";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowPnlPayment
            // 
            this.flowPnlPayment.Controls.Add(this.pnlPayCloseDate);
            this.flowPnlPayment.Controls.Add(this.pnlTransPayDate);
            this.flowPnlPayment.Controls.Add(this.pnlPayDates);
            this.flowPnlPayment.Controls.Add(this.panel3);
            this.flowPnlPayment.Controls.Add(this.panel14);
            this.flowPnlPayment.Controls.Add(this.panel13);
            this.flowPnlPayment.Controls.Add(this.panel11);
            this.flowPnlPayment.Controls.Add(this.panel6);
            this.flowPnlPayment.Controls.Add(this.pnlPayType);
            this.flowPnlPayment.Controls.Add(this.pnlPaySortBy);
            this.flowPnlPayment.Controls.Add(this.pnlIncludeDtls);
            this.flowPnlPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPnlPayment.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPnlPayment.Location = new System.Drawing.Point(4, 69);
            this.flowPnlPayment.Name = "flowPnlPayment";
            this.flowPnlPayment.Size = new System.Drawing.Size(1276, 74);
            this.flowPnlPayment.TabIndex = 254;
            // 
            // pnlPayCloseDate
            // 
            this.pnlPayCloseDate.Controls.Add(this.dtPayCloseDate);
            this.pnlPayCloseDate.Controls.Add(this.label55);
            this.pnlPayCloseDate.Location = new System.Drawing.Point(3, 3);
            this.pnlPayCloseDate.Name = "pnlPayCloseDate";
            this.pnlPayCloseDate.Size = new System.Drawing.Size(186, 26);
            this.pnlPayCloseDate.TabIndex = 251;
            // 
            // dtPayCloseDate
            // 
            this.dtPayCloseDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtPayCloseDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtPayCloseDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtPayCloseDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtPayCloseDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtPayCloseDate.CustomFormat = "MM/dd/yyyy";
            this.dtPayCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPayCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPayCloseDate.Location = new System.Drawing.Point(89, 2);
            this.dtPayCloseDate.Name = "dtPayCloseDate";
            this.dtPayCloseDate.Size = new System.Drawing.Size(94, 22);
            this.dtPayCloseDate.TabIndex = 7;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Location = new System.Drawing.Point(14, 6);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(73, 14);
            this.label55.TabIndex = 6;
            this.label55.Text = "Close Date :";
            // 
            // pnlTransPayDate
            // 
            this.pnlTransPayDate.Controls.Add(this.cmb_Paydatefilter);
            this.pnlTransPayDate.Controls.Add(this.label49);
            this.pnlTransPayDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransPayDate.Location = new System.Drawing.Point(3, 35);
            this.pnlTransPayDate.Name = "pnlTransPayDate";
            this.pnlTransPayDate.Size = new System.Drawing.Size(186, 26);
            this.pnlTransPayDate.TabIndex = 248;
            // 
            // cmb_Paydatefilter
            // 
            this.cmb_Paydatefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Paydatefilter.FormattingEnabled = true;
            this.cmb_Paydatefilter.Location = new System.Drawing.Point(89, 2);
            this.cmb_Paydatefilter.Name = "cmb_Paydatefilter";
            this.cmb_Paydatefilter.Size = new System.Drawing.Size(94, 22);
            this.cmb_Paydatefilter.TabIndex = 217;
            this.cmb_Paydatefilter.Visible = false;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(14, 6);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(73, 14);
            this.label49.TabIndex = 216;
            this.label49.Text = "Close Date :";
            this.label49.Visible = false;
            // 
            // pnlPayDates
            // 
            this.pnlPayDates.Controls.Add(this.dtpPayEndDate);
            this.pnlPayDates.Controls.Add(this.dtpPayStartDate);
            this.pnlPayDates.Controls.Add(this.label47);
            this.pnlPayDates.Controls.Add(this.label48);
            this.pnlPayDates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPayDates.Location = new System.Drawing.Point(195, 3);
            this.pnlPayDates.Name = "pnlPayDates";
            this.pnlPayDates.Size = new System.Drawing.Size(173, 57);
            this.pnlPayDates.TabIndex = 249;
            this.pnlPayDates.Visible = false;
            // 
            // dtpPayEndDate
            // 
            this.dtpPayEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpPayEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpPayEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpPayEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpPayEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpPayEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpPayEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPayEndDate.Location = new System.Drawing.Point(76, 33);
            this.dtpPayEndDate.Name = "dtpPayEndDate";
            this.dtpPayEndDate.Size = new System.Drawing.Size(94, 22);
            this.dtpPayEndDate.TabIndex = 5;
            // 
            // dtpPayStartDate
            // 
            this.dtpPayStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpPayStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpPayStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpPayStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpPayStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpPayStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpPayStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPayStartDate.Location = new System.Drawing.Point(76, 3);
            this.dtpPayStartDate.Name = "dtpPayStartDate";
            this.dtpPayStartDate.Size = new System.Drawing.Size(94, 22);
            this.dtpPayStartDate.TabIndex = 5;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(1, 6);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(72, 14);
            this.label47.TabIndex = 4;
            this.label47.Text = "Start Date :";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(7, 38);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(66, 14);
            this.label48.TabIndex = 6;
            this.label48.Text = "End Date :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbPaymentProvider);
            this.panel3.Controls.Add(this.btnClearPayProviders);
            this.panel3.Controls.Add(this.btnBrowsePayProviders);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Location = new System.Drawing.Point(374, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(251, 26);
            this.panel3.TabIndex = 253;
            this.panel3.Visible = false;
            // 
            // cmbPaymentProvider
            // 
            this.cmbPaymentProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentProvider.FormattingEnabled = true;
            this.cmbPaymentProvider.Location = new System.Drawing.Point(67, 1);
            this.cmbPaymentProvider.Name = "cmbPaymentProvider";
            this.cmbPaymentProvider.Size = new System.Drawing.Size(134, 22);
            this.cmbPaymentProvider.TabIndex = 184;
            // 
            // btnClearPayProviders
            // 
            this.btnClearPayProviders.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayProviders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayProviders.BackgroundImage")));
            this.btnClearPayProviders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayProviders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayProviders.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayProviders.Image")));
            this.btnClearPayProviders.Location = new System.Drawing.Point(227, 1);
            this.btnClearPayProviders.Name = "btnClearPayProviders";
            this.btnClearPayProviders.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayProviders.TabIndex = 187;
            this.btnClearPayProviders.UseVisualStyleBackColor = false;
            this.btnClearPayProviders.Click += new System.EventHandler(this.btnClearPayProvider_Click);
            // 
            // btnBrowsePayProviders
            // 
            this.btnBrowsePayProviders.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayProviders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayProviders.BackgroundImage")));
            this.btnBrowsePayProviders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayProviders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayProviders.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayProviders.Image")));
            this.btnBrowsePayProviders.Location = new System.Drawing.Point(205, 1);
            this.btnBrowsePayProviders.Name = "btnBrowsePayProviders";
            this.btnBrowsePayProviders.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayProviders.TabIndex = 186;
            this.btnBrowsePayProviders.UseVisualStyleBackColor = false;
            this.btnBrowsePayProviders.Click += new System.EventHandler(this.btnBrowsePayProvider_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(5, 5);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 14);
            this.label38.TabIndex = 185;
            this.label38.Text = "Provider :";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.cmbPayFacility);
            this.panel14.Controls.Add(this.label53);
            this.panel14.Controls.Add(this.btnBrowsePayFacility);
            this.panel14.Controls.Add(this.btnClearPayFacility);
            this.panel14.Location = new System.Drawing.Point(374, 35);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(251, 26);
            this.panel14.TabIndex = 244;
            this.panel14.Visible = false;
            // 
            // cmbPayFacility
            // 
            this.cmbPayFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayFacility.FormattingEnabled = true;
            this.cmbPayFacility.Location = new System.Drawing.Point(67, 2);
            this.cmbPayFacility.Name = "cmbPayFacility";
            this.cmbPayFacility.Size = new System.Drawing.Size(134, 22);
            this.cmbPayFacility.TabIndex = 197;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(14, 7);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(50, 14);
            this.label53.TabIndex = 196;
            this.label53.Text = "Facility :";
            // 
            // btnBrowsePayFacility
            // 
            this.btnBrowsePayFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayFacility.BackgroundImage")));
            this.btnBrowsePayFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayFacility.Image")));
            this.btnBrowsePayFacility.Location = new System.Drawing.Point(205, 4);
            this.btnBrowsePayFacility.Name = "btnBrowsePayFacility";
            this.btnBrowsePayFacility.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayFacility.TabIndex = 194;
            this.btnBrowsePayFacility.UseVisualStyleBackColor = false;
            this.btnBrowsePayFacility.Click += new System.EventHandler(this.btnBrowsePayFacility_Click);
            // 
            // btnClearPayFacility
            // 
            this.btnClearPayFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayFacility.BackgroundImage")));
            this.btnClearPayFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayFacility.Image")));
            this.btnClearPayFacility.Location = new System.Drawing.Point(227, 4);
            this.btnClearPayFacility.Name = "btnClearPayFacility";
            this.btnClearPayFacility.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayFacility.TabIndex = 195;
            this.btnClearPayFacility.UseVisualStyleBackColor = false;
            this.btnClearPayFacility.Click += new System.EventHandler(this.btnClearPayFacility_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.cmbPayUser);
            this.panel13.Controls.Add(this.btnBrowsePayUser);
            this.panel13.Controls.Add(this.btnClearPayUser);
            this.panel13.Controls.Add(this.lblPayUser);
            this.panel13.Location = new System.Drawing.Point(631, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(251, 26);
            this.panel13.TabIndex = 246;
            // 
            // cmbPayUser
            // 
            this.cmbPayUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayUser.FormattingEnabled = true;
            this.cmbPayUser.Location = new System.Drawing.Point(52, 1);
            this.cmbPayUser.Name = "cmbPayUser";
            this.cmbPayUser.Size = new System.Drawing.Size(136, 22);
            this.cmbPayUser.TabIndex = 197;
            // 
            // btnBrowsePayUser
            // 
            this.btnBrowsePayUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayUser.BackgroundImage")));
            this.btnBrowsePayUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayUser.Image")));
            this.btnBrowsePayUser.Location = new System.Drawing.Point(191, 2);
            this.btnBrowsePayUser.Name = "btnBrowsePayUser";
            this.btnBrowsePayUser.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayUser.TabIndex = 194;
            this.btnBrowsePayUser.UseVisualStyleBackColor = false;
            this.btnBrowsePayUser.Click += new System.EventHandler(this.btnBrowsePayUser_Click);
            // 
            // btnClearPayUser
            // 
            this.btnClearPayUser.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayUser.BackgroundImage")));
            this.btnClearPayUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayUser.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayUser.Image")));
            this.btnClearPayUser.Location = new System.Drawing.Point(214, 2);
            this.btnClearPayUser.Name = "btnClearPayUser";
            this.btnClearPayUser.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayUser.TabIndex = 195;
            this.btnClearPayUser.UseVisualStyleBackColor = false;
            this.btnClearPayUser.Click += new System.EventHandler(this.btnClearPayUser_Click);
            // 
            // lblPayUser
            // 
            this.lblPayUser.AutoSize = true;
            this.lblPayUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayUser.Location = new System.Drawing.Point(9, 5);
            this.lblPayUser.Name = "lblPayUser";
            this.lblPayUser.Size = new System.Drawing.Size(39, 14);
            this.lblPayUser.TabIndex = 196;
            this.lblPayUser.Text = "User :";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.cmbPayTray);
            this.panel11.Controls.Add(this.btnBrowsePayTray);
            this.panel11.Controls.Add(this.panel9);
            this.panel11.Controls.Add(this.btnClearPayTray);
            this.panel11.Controls.Add(this.lblPayTray);
            this.panel11.Location = new System.Drawing.Point(631, 35);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(251, 24);
            this.panel11.TabIndex = 245;
            // 
            // cmbPayTray
            // 
            this.cmbPayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayTray.FormattingEnabled = true;
            this.cmbPayTray.Location = new System.Drawing.Point(52, 1);
            this.cmbPayTray.Name = "cmbPayTray";
            this.cmbPayTray.Size = new System.Drawing.Size(136, 22);
            this.cmbPayTray.TabIndex = 197;
            // 
            // btnBrowsePayTray
            // 
            this.btnBrowsePayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayTray.BackgroundImage")));
            this.btnBrowsePayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayTray.Image")));
            this.btnBrowsePayTray.Location = new System.Drawing.Point(191, 2);
            this.btnBrowsePayTray.Name = "btnBrowsePayTray";
            this.btnBrowsePayTray.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayTray.TabIndex = 194;
            this.btnBrowsePayTray.UseVisualStyleBackColor = false;
            this.btnBrowsePayTray.Click += new System.EventHandler(this.btnBrowsePayTray_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label50);
            this.panel9.Controls.Add(this.cmbPayProvider);
            this.panel9.Controls.Add(this.btnClearPayProvider);
            this.panel9.Controls.Add(this.btnBrowsePayProvider);
            this.panel9.Location = new System.Drawing.Point(5, 31);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(328, 29);
            this.panel9.TabIndex = 247;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(30, 8);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(59, 14);
            this.label50.TabIndex = 185;
            this.label50.Text = "Provider :";
            // 
            // cmbPayProvider
            // 
            this.cmbPayProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayProvider.FormattingEnabled = true;
            this.cmbPayProvider.Location = new System.Drawing.Point(94, 5);
            this.cmbPayProvider.Name = "cmbPayProvider";
            this.cmbPayProvider.Size = new System.Drawing.Size(174, 22);
            this.cmbPayProvider.TabIndex = 184;
            // 
            // btnClearPayProvider
            // 
            this.btnClearPayProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayProvider.BackgroundImage")));
            this.btnClearPayProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayProvider.Image")));
            this.btnClearPayProvider.Location = new System.Drawing.Point(300, 3);
            this.btnClearPayProvider.Name = "btnClearPayProvider";
            this.btnClearPayProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearPayProvider.TabIndex = 187;
            this.btnClearPayProvider.UseVisualStyleBackColor = false;
            // 
            // btnBrowsePayProvider
            // 
            this.btnBrowsePayProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayProvider.BackgroundImage")));
            this.btnBrowsePayProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayProvider.Image")));
            this.btnBrowsePayProvider.Location = new System.Drawing.Point(274, 4);
            this.btnBrowsePayProvider.Name = "btnBrowsePayProvider";
            this.btnBrowsePayProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePayProvider.TabIndex = 186;
            this.btnBrowsePayProvider.UseVisualStyleBackColor = false;
            // 
            // btnClearPayTray
            // 
            this.btnClearPayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayTray.BackgroundImage")));
            this.btnClearPayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayTray.Image")));
            this.btnClearPayTray.Location = new System.Drawing.Point(214, 2);
            this.btnClearPayTray.Name = "btnClearPayTray";
            this.btnClearPayTray.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayTray.TabIndex = 195;
            this.btnClearPayTray.UseVisualStyleBackColor = false;
            this.btnClearPayTray.Click += new System.EventHandler(this.btnClearPayTray_Click);
            // 
            // lblPayTray
            // 
            this.lblPayTray.AutoSize = true;
            this.lblPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayTray.Location = new System.Drawing.Point(9, 4);
            this.lblPayTray.Name = "lblPayTray";
            this.lblPayTray.Size = new System.Drawing.Size(39, 14);
            this.lblPayTray.TabIndex = 196;
            this.lblPayTray.Text = "Tray :";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnPaySelectLoginUsrs);
            this.panel6.Location = new System.Drawing.Point(888, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(251, 26);
            this.panel6.TabIndex = 255;
            // 
            // pnlPayType
            // 
            this.pnlPayType.Controls.Add(this.cmbPayType);
            this.pnlPayType.Controls.Add(this.btnBrowsePayType);
            this.pnlPayType.Controls.Add(this.btnClearPayType);
            this.pnlPayType.Controls.Add(this.label56);
            this.pnlPayType.Location = new System.Drawing.Point(888, 35);
            this.pnlPayType.Name = "pnlPayType";
            this.pnlPayType.Size = new System.Drawing.Size(251, 24);
            this.pnlPayType.TabIndex = 252;
            // 
            // cmbPayType
            // 
            this.cmbPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Location = new System.Drawing.Point(48, 1);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(123, 22);
            this.cmbPayType.TabIndex = 197;
            // 
            // btnBrowsePayType
            // 
            this.btnBrowsePayType.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayType.BackgroundImage")));
            this.btnBrowsePayType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayType.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayType.Image")));
            this.btnBrowsePayType.Location = new System.Drawing.Point(174, 2);
            this.btnBrowsePayType.Name = "btnBrowsePayType";
            this.btnBrowsePayType.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayType.TabIndex = 194;
            this.btnBrowsePayType.UseVisualStyleBackColor = false;
            this.btnBrowsePayType.Click += new System.EventHandler(this.btnBrowsePayType_Click);
            // 
            // btnClearPayType
            // 
            this.btnClearPayType.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayType.BackgroundImage")));
            this.btnClearPayType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayType.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayType.Image")));
            this.btnClearPayType.Location = new System.Drawing.Point(197, 2);
            this.btnClearPayType.Name = "btnClearPayType";
            this.btnClearPayType.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayType.TabIndex = 195;
            this.btnClearPayType.UseVisualStyleBackColor = false;
            this.btnClearPayType.Click += new System.EventHandler(this.btnClearPayType_Click);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(3, 5);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(43, 14);
            this.label56.TabIndex = 196;
            this.label56.Text = "Type :";
            // 
            // pnlPaySortBy
            // 
            this.pnlPaySortBy.Controls.Add(this.cmbPaySortBy);
            this.pnlPaySortBy.Controls.Add(this.lblPaySortBy);
            this.pnlPaySortBy.Controls.Add(this.checkBox1);
            this.pnlPaySortBy.Controls.Add(this.checkBox2);
            this.pnlPaySortBy.Location = new System.Drawing.Point(1145, 3);
            this.pnlPaySortBy.Name = "pnlPaySortBy";
            this.pnlPaySortBy.Size = new System.Drawing.Size(251, 24);
            this.pnlPaySortBy.TabIndex = 256;
            // 
            // cmbPaySortBy
            // 
            this.cmbPaySortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaySortBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaySortBy.FormattingEnabled = true;
            this.cmbPaySortBy.Location = new System.Drawing.Point(57, 1);
            this.cmbPaySortBy.Name = "cmbPaySortBy";
            this.cmbPaySortBy.Size = new System.Drawing.Size(136, 22);
            this.cmbPaySortBy.TabIndex = 199;
            // 
            // lblPaySortBy
            // 
            this.lblPaySortBy.AutoSize = true;
            this.lblPaySortBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaySortBy.Location = new System.Drawing.Point(1, 5);
            this.lblPaySortBy.Name = "lblPaySortBy";
            this.lblPaySortBy.Size = new System.Drawing.Size(55, 14);
            this.lblPaySortBy.TabIndex = 198;
            this.lblPaySortBy.Text = "Sort By :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(209, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 18);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Include All Trays ";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(191, 8);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(116, 18);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Login Users Only";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // pnlIncludeDtls
            // 
            this.pnlIncludeDtls.Controls.Add(this.chkPayIncludeDetails);
            this.pnlIncludeDtls.Controls.Add(this.checkBox3);
            this.pnlIncludeDtls.Controls.Add(this.checkBox4);
            this.pnlIncludeDtls.Location = new System.Drawing.Point(1145, 33);
            this.pnlIncludeDtls.Name = "pnlIncludeDtls";
            this.pnlIncludeDtls.Size = new System.Drawing.Size(251, 24);
            this.pnlIncludeDtls.TabIndex = 257;
            // 
            // chkPayIncludeDetails
            // 
            this.chkPayIncludeDetails.AutoSize = true;
            this.chkPayIncludeDetails.Checked = true;
            this.chkPayIncludeDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPayIncludeDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPayIncludeDetails.Location = new System.Drawing.Point(4, 4);
            this.chkPayIncludeDetails.Name = "chkPayIncludeDetails";
            this.chkPayIncludeDetails.Size = new System.Drawing.Size(105, 18);
            this.chkPayIncludeDetails.TabIndex = 2;
            this.chkPayIncludeDetails.Text = "Include Details";
            this.chkPayIncludeDetails.UseVisualStyleBackColor = true;
            this.chkPayIncludeDetails.CheckedChanged += new System.EventHandler(this.chkPayIncludeDetails_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(209, 7);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(119, 18);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Include All Trays ";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.Location = new System.Drawing.Point(191, 8);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(116, 18);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Login Users Only";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Visible = false;
            // 
            // flwPnlDailyCharges
            // 
            this.flwPnlDailyCharges.Controls.Add(this.pnlCloseDate);
            this.flwPnlDailyCharges.Controls.Add(this.pnlTransDate);
            this.flwPnlDailyCharges.Controls.Add(this.pnlChrgDates);
            this.flwPnlDailyCharges.Controls.Add(this.pnlProvider);
            this.flwPnlDailyCharges.Controls.Add(this.pnlMultiFacility);
            this.flwPnlDailyCharges.Controls.Add(this.pnlUser);
            this.flwPnlDailyCharges.Controls.Add(this.pnlMultiChargesTray);
            this.flwPnlDailyCharges.Controls.Add(this.pnlRunReportAs);
            this.flwPnlDailyCharges.Controls.Add(this.pnlLoginUsers);
            this.flwPnlDailyCharges.Dock = System.Windows.Forms.DockStyle.Top;
            this.flwPnlDailyCharges.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwPnlDailyCharges.Location = new System.Drawing.Point(4, 4);
            this.flwPnlDailyCharges.Name = "flwPnlDailyCharges";
            this.flwPnlDailyCharges.Size = new System.Drawing.Size(1276, 65);
            this.flwPnlDailyCharges.TabIndex = 247;
            // 
            // pnlCloseDate
            // 
            this.pnlCloseDate.Controls.Add(this.dtpCloseDate);
            this.pnlCloseDate.Controls.Add(this.label3);
            this.pnlCloseDate.Location = new System.Drawing.Point(3, 3);
            this.pnlCloseDate.Name = "pnlCloseDate";
            this.pnlCloseDate.Size = new System.Drawing.Size(186, 26);
            this.pnlCloseDate.TabIndex = 245;
            // 
            // dtpCloseDate
            // 
            this.dtpCloseDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpCloseDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpCloseDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpCloseDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpCloseDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpCloseDate.CustomFormat = "MM/dd/yyyy";
            this.dtpCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCloseDate.Location = new System.Drawing.Point(89, 2);
            this.dtpCloseDate.Name = "dtpCloseDate";
            this.dtpCloseDate.Size = new System.Drawing.Size(94, 22);
            this.dtpCloseDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Close Date :";
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.cmb_Chargesdatefilter);
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransDate.Location = new System.Drawing.Point(3, 35);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(186, 26);
            this.pnlTransDate.TabIndex = 242;
            // 
            // cmb_Chargesdatefilter
            // 
            this.cmb_Chargesdatefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Chargesdatefilter.FormattingEnabled = true;
            this.cmb_Chargesdatefilter.Location = new System.Drawing.Point(89, 2);
            this.cmb_Chargesdatefilter.Name = "cmb_Chargesdatefilter";
            this.cmb_Chargesdatefilter.Size = new System.Drawing.Size(94, 22);
            this.cmb_Chargesdatefilter.TabIndex = 217;
            this.cmb_Chargesdatefilter.Visible = false;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(14, 6);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(73, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Close Date :";
            this.lbl_datefilter.Visible = false;
            // 
            // pnlChrgDates
            // 
            this.pnlChrgDates.Controls.Add(this.dtpChrgEndDate);
            this.pnlChrgDates.Controls.Add(this.dtpChrgStartDate);
            this.pnlChrgDates.Controls.Add(this.lblStartDate);
            this.pnlChrgDates.Controls.Add(this.lblEndDate);
            this.pnlChrgDates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlChrgDates.Location = new System.Drawing.Point(195, 3);
            this.pnlChrgDates.Name = "pnlChrgDates";
            this.pnlChrgDates.Size = new System.Drawing.Size(173, 58);
            this.pnlChrgDates.TabIndex = 243;
            this.pnlChrgDates.Visible = false;
            // 
            // dtpChrgEndDate
            // 
            this.dtpChrgEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpChrgEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpChrgEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpChrgEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpChrgEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpChrgEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpChrgEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChrgEndDate.Location = new System.Drawing.Point(76, 33);
            this.dtpChrgEndDate.Name = "dtpChrgEndDate";
            this.dtpChrgEndDate.Size = new System.Drawing.Size(94, 22);
            this.dtpChrgEndDate.TabIndex = 7;
            // 
            // dtpChrgStartDate
            // 
            this.dtpChrgStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpChrgStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpChrgStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpChrgStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpChrgStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpChrgStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpChrgStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChrgStartDate.Location = new System.Drawing.Point(76, 3);
            this.dtpChrgStartDate.Name = "dtpChrgStartDate";
            this.dtpChrgStartDate.Size = new System.Drawing.Size(94, 22);
            this.dtpChrgStartDate.TabIndex = 5;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(1, 7);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(7, 37);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.cmbChargesProvider);
            this.pnlProvider.Controls.Add(this.btnClearCProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Controls.Add(this.label46);
            this.pnlProvider.Location = new System.Drawing.Point(374, 3);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(250, 27);
            this.pnlProvider.TabIndex = 241;
            // 
            // cmbChargesProvider
            // 
            this.cmbChargesProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesProvider.FormattingEnabled = true;
            this.cmbChargesProvider.Location = new System.Drawing.Point(67, 3);
            this.cmbChargesProvider.Name = "cmbChargesProvider";
            this.cmbChargesProvider.Size = new System.Drawing.Size(134, 22);
            this.cmbChargesProvider.TabIndex = 184;
            // 
            // btnClearCProvider
            // 
            this.btnClearCProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCProvider.BackgroundImage")));
            this.btnClearCProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCProvider.Image")));
            this.btnClearCProvider.Location = new System.Drawing.Point(227, 4);
            this.btnClearCProvider.Name = "btnClearCProvider";
            this.btnClearCProvider.Size = new System.Drawing.Size(20, 20);
            this.btnClearCProvider.TabIndex = 187;
            this.btnClearCProvider.UseVisualStyleBackColor = false;
            this.btnClearCProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(205, 4);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(5, 7);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(59, 14);
            this.label46.TabIndex = 185;
            this.label46.Text = "Provider :";
            // 
            // pnlMultiFacility
            // 
            this.pnlMultiFacility.Controls.Add(this.cmbChargesMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnBrowseCMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnClearCMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.lblMultiFacility);
            this.pnlMultiFacility.Location = new System.Drawing.Point(374, 36);
            this.pnlMultiFacility.Name = "pnlMultiFacility";
            this.pnlMultiFacility.Size = new System.Drawing.Size(250, 25);
            this.pnlMultiFacility.TabIndex = 233;
            // 
            // cmbChargesMultiFacility
            // 
            this.cmbChargesMultiFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesMultiFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesMultiFacility.FormattingEnabled = true;
            this.cmbChargesMultiFacility.Location = new System.Drawing.Point(67, 1);
            this.cmbChargesMultiFacility.Name = "cmbChargesMultiFacility";
            this.cmbChargesMultiFacility.Size = new System.Drawing.Size(134, 22);
            this.cmbChargesMultiFacility.TabIndex = 197;
            // 
            // btnBrowseCMultiFacility
            // 
            this.btnBrowseCMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiFacility.BackgroundImage")));
            this.btnBrowseCMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiFacility.Image")));
            this.btnBrowseCMultiFacility.Location = new System.Drawing.Point(205, 2);
            this.btnBrowseCMultiFacility.Name = "btnBrowseCMultiFacility";
            this.btnBrowseCMultiFacility.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseCMultiFacility.TabIndex = 194;
            this.btnBrowseCMultiFacility.UseVisualStyleBackColor = false;
            this.btnBrowseCMultiFacility.Click += new System.EventHandler(this.btnBrowseMultiFacility_Click);
            // 
            // btnClearCMultiFacility
            // 
            this.btnClearCMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCMultiFacility.BackgroundImage")));
            this.btnClearCMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCMultiFacility.Image")));
            this.btnClearCMultiFacility.Location = new System.Drawing.Point(227, 2);
            this.btnClearCMultiFacility.Name = "btnClearCMultiFacility";
            this.btnClearCMultiFacility.Size = new System.Drawing.Size(20, 20);
            this.btnClearCMultiFacility.TabIndex = 195;
            this.btnClearCMultiFacility.UseVisualStyleBackColor = false;
            this.btnClearCMultiFacility.Click += new System.EventHandler(this.btnClearMultiFacility_Click);
            // 
            // lblMultiFacility
            // 
            this.lblMultiFacility.AutoSize = true;
            this.lblMultiFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiFacility.Location = new System.Drawing.Point(14, 5);
            this.lblMultiFacility.Name = "lblMultiFacility";
            this.lblMultiFacility.Size = new System.Drawing.Size(50, 14);
            this.lblMultiFacility.TabIndex = 196;
            this.lblMultiFacility.Text = "Facility :";
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.cmbChargesUser);
            this.pnlUser.Controls.Add(this.btnBrowseCUser);
            this.pnlUser.Controls.Add(this.btnClearCUser);
            this.pnlUser.Controls.Add(this.lblUser);
            this.pnlUser.Location = new System.Drawing.Point(630, 3);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(250, 27);
            this.pnlUser.TabIndex = 238;
            // 
            // cmbChargesUser
            // 
            this.cmbChargesUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesUser.FormattingEnabled = true;
            this.cmbChargesUser.Location = new System.Drawing.Point(52, 2);
            this.cmbChargesUser.Name = "cmbChargesUser";
            this.cmbChargesUser.Size = new System.Drawing.Size(136, 22);
            this.cmbChargesUser.TabIndex = 197;
            // 
            // btnBrowseCUser
            // 
            this.btnBrowseCUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCUser.BackgroundImage")));
            this.btnBrowseCUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCUser.Image")));
            this.btnBrowseCUser.Location = new System.Drawing.Point(191, 3);
            this.btnBrowseCUser.Name = "btnBrowseCUser";
            this.btnBrowseCUser.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseCUser.TabIndex = 194;
            this.btnBrowseCUser.UseVisualStyleBackColor = false;
            this.btnBrowseCUser.Click += new System.EventHandler(this.btnBrowseUser_Click);
            // 
            // btnClearCUser
            // 
            this.btnClearCUser.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCUser.BackgroundImage")));
            this.btnClearCUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCUser.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCUser.Image")));
            this.btnClearCUser.Location = new System.Drawing.Point(214, 3);
            this.btnClearCUser.Name = "btnClearCUser";
            this.btnClearCUser.Size = new System.Drawing.Size(20, 20);
            this.btnClearCUser.TabIndex = 195;
            this.btnClearCUser.UseVisualStyleBackColor = false;
            this.btnClearCUser.Click += new System.EventHandler(this.btnClearUser_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(9, 6);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 196;
            this.lblUser.Text = "User :";
            // 
            // pnlMultiChargesTray
            // 
            this.pnlMultiChargesTray.Controls.Add(this.cmbMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnBrowseCMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnClearCMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.lblMultiChargesTray);
            this.pnlMultiChargesTray.Location = new System.Drawing.Point(630, 36);
            this.pnlMultiChargesTray.Name = "pnlMultiChargesTray";
            this.pnlMultiChargesTray.Size = new System.Drawing.Size(250, 26);
            this.pnlMultiChargesTray.TabIndex = 236;
            // 
            // cmbMultiChargesTray
            // 
            this.cmbMultiChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiChargesTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMultiChargesTray.FormattingEnabled = true;
            this.cmbMultiChargesTray.Location = new System.Drawing.Point(52, 2);
            this.cmbMultiChargesTray.Name = "cmbMultiChargesTray";
            this.cmbMultiChargesTray.Size = new System.Drawing.Size(136, 22);
            this.cmbMultiChargesTray.TabIndex = 197;
            // 
            // btnBrowseCMultiChargesTray
            // 
            this.btnBrowseCMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiChargesTray.BackgroundImage")));
            this.btnBrowseCMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiChargesTray.Image")));
            this.btnBrowseCMultiChargesTray.Location = new System.Drawing.Point(191, 3);
            this.btnBrowseCMultiChargesTray.Name = "btnBrowseCMultiChargesTray";
            this.btnBrowseCMultiChargesTray.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseCMultiChargesTray.TabIndex = 194;
            this.btnBrowseCMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnBrowseCMultiChargesTray.Click += new System.EventHandler(this.btnBrowseMultiChargesTray_Click);
            // 
            // btnClearCMultiChargesTray
            // 
            this.btnClearCMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCMultiChargesTray.BackgroundImage")));
            this.btnClearCMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCMultiChargesTray.Image")));
            this.btnClearCMultiChargesTray.Location = new System.Drawing.Point(214, 3);
            this.btnClearCMultiChargesTray.Name = "btnClearCMultiChargesTray";
            this.btnClearCMultiChargesTray.Size = new System.Drawing.Size(20, 20);
            this.btnClearCMultiChargesTray.TabIndex = 195;
            this.btnClearCMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnClearCMultiChargesTray.Click += new System.EventHandler(this.btnClearMultiChargesTray_Click);
            // 
            // lblMultiChargesTray
            // 
            this.lblMultiChargesTray.AutoSize = true;
            this.lblMultiChargesTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiChargesTray.Location = new System.Drawing.Point(9, 5);
            this.lblMultiChargesTray.Name = "lblMultiChargesTray";
            this.lblMultiChargesTray.Size = new System.Drawing.Size(39, 14);
            this.lblMultiChargesTray.TabIndex = 196;
            this.lblMultiChargesTray.Text = "Tray :";
            // 
            // pnlRunReportAs
            // 
            this.pnlRunReportAs.Controls.Add(this.btnChrgSelectLoginUsrs);
            this.pnlRunReportAs.Controls.Add(this.chkSealedCharges);
            this.pnlRunReportAs.Controls.Add(this.chkOpenCharges);
            this.pnlRunReportAs.Location = new System.Drawing.Point(886, 3);
            this.pnlRunReportAs.Name = "pnlRunReportAs";
            this.pnlRunReportAs.Size = new System.Drawing.Size(149, 29);
            this.pnlRunReportAs.TabIndex = 240;
            // 
            // chkSealedCharges
            // 
            this.chkSealedCharges.AutoSize = true;
            this.chkSealedCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSealedCharges.Location = new System.Drawing.Point(299, 5);
            this.chkSealedCharges.Name = "chkSealedCharges";
            this.chkSealedCharges.Size = new System.Drawing.Size(153, 18);
            this.chkSealedCharges.TabIndex = 3;
            this.chkSealedCharges.Text = "Include Sealed Charges";
            this.chkSealedCharges.UseVisualStyleBackColor = true;
            this.chkSealedCharges.Visible = false;
            // 
            // chkOpenCharges
            // 
            this.chkOpenCharges.AutoSize = true;
            this.chkOpenCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOpenCharges.Location = new System.Drawing.Point(161, 5);
            this.chkOpenCharges.Name = "chkOpenCharges";
            this.chkOpenCharges.Size = new System.Drawing.Size(147, 18);
            this.chkOpenCharges.TabIndex = 2;
            this.chkOpenCharges.Text = "Include Open Charges";
            this.chkOpenCharges.UseVisualStyleBackColor = true;
            this.chkOpenCharges.Visible = false;
            // 
            // pnlLoginUsers
            // 
            this.pnlLoginUsers.Controls.Add(this.cmbChrgSortBy);
            this.pnlLoginUsers.Controls.Add(this.lblChrgSortBy);
            this.pnlLoginUsers.Controls.Add(this.chkIncludeAllTrays);
            this.pnlLoginUsers.Controls.Add(this.chkLoginUsers);
            this.pnlLoginUsers.Location = new System.Drawing.Point(1041, 3);
            this.pnlLoginUsers.Name = "pnlLoginUsers";
            this.pnlLoginUsers.Size = new System.Drawing.Size(328, 28);
            this.pnlLoginUsers.TabIndex = 239;
            // 
            // cmbChrgSortBy
            // 
            this.cmbChrgSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChrgSortBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChrgSortBy.FormattingEnabled = true;
            this.cmbChrgSortBy.Location = new System.Drawing.Point(60, 2);
            this.cmbChrgSortBy.Name = "cmbChrgSortBy";
            this.cmbChrgSortBy.Size = new System.Drawing.Size(95, 22);
            this.cmbChrgSortBy.TabIndex = 199;
            // 
            // lblChrgSortBy
            // 
            this.lblChrgSortBy.AutoSize = true;
            this.lblChrgSortBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChrgSortBy.Location = new System.Drawing.Point(2, 6);
            this.lblChrgSortBy.Name = "lblChrgSortBy";
            this.lblChrgSortBy.Size = new System.Drawing.Size(55, 14);
            this.lblChrgSortBy.TabIndex = 198;
            this.lblChrgSortBy.Text = "Sort By :";
            // 
            // chkIncludeAllTrays
            // 
            this.chkIncludeAllTrays.AutoSize = true;
            this.chkIncludeAllTrays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeAllTrays.Location = new System.Drawing.Point(285, 4);
            this.chkIncludeAllTrays.Name = "chkIncludeAllTrays";
            this.chkIncludeAllTrays.Size = new System.Drawing.Size(119, 18);
            this.chkIncludeAllTrays.TabIndex = 1;
            this.chkIncludeAllTrays.Text = "Include All Trays ";
            this.chkIncludeAllTrays.UseVisualStyleBackColor = true;
            this.chkIncludeAllTrays.Visible = false;
            // 
            // chkLoginUsers
            // 
            this.chkLoginUsers.AutoSize = true;
            this.chkLoginUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLoginUsers.Location = new System.Drawing.Point(161, 4);
            this.chkLoginUsers.Name = "chkLoginUsers";
            this.chkLoginUsers.Size = new System.Drawing.Size(116, 18);
            this.chkLoginUsers.TabIndex = 0;
            this.chkLoginUsers.Text = "Login Users Only";
            this.chkLoginUsers.UseVisualStyleBackColor = true;
            this.chkLoginUsers.Visible = false;
            this.chkLoginUsers.CheckedChanged += new System.EventHandler(this.chkLoginUsers_CheckedChanged);
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(3, 4);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 68);
            this.label45.TabIndex = 8;
            this.label45.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(1280, 4);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 68);
            this.label44.TabIndex = 9;
            this.label44.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1278, 1);
            this.label11.TabIndex = 271;
            this.label11.Text = "label1";
            // 
            // pnlChargesShowHide
            // 
            this.pnlChargesShowHide.Controls.Add(this.panel4);
            this.pnlChargesShowHide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChargesShowHide.Location = new System.Drawing.Point(0, 0);
            this.pnlChargesShowHide.Name = "pnlChargesShowHide";
            this.pnlChargesShowHide.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlChargesShowHide.Size = new System.Drawing.Size(1284, 24);
            this.pnlChargesShowHide.TabIndex = 268;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.btnChrgDown);
            this.panel4.Controls.Add(this.btnChrgUP);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(3, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1278, 24);
            this.panel4.TabIndex = 264;
            this.panel4.Tag = "Orange";
            // 
            // btnChrgDown
            // 
            this.btnChrgDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChrgDown.FlatAppearance.BorderSize = 0;
            this.btnChrgDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChrgDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChrgDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChrgDown.Location = new System.Drawing.Point(1233, 1);
            this.btnChrgDown.Name = "btnChrgDown";
            this.btnChrgDown.Size = new System.Drawing.Size(22, 22);
            this.btnChrgDown.TabIndex = 12;
            this.btnChrgDown.UseVisualStyleBackColor = true;
            this.btnChrgDown.MouseLeave += new System.EventHandler(this.btnChrgDown_MouseLeave);
            this.btnChrgDown.Click += new System.EventHandler(this.btnChrgDown_Click);
            this.btnChrgDown.MouseHover += new System.EventHandler(this.btnChrgDown_MouseHover);
            // 
            // btnChrgUP
            // 
            this.btnChrgUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChrgUP.FlatAppearance.BorderSize = 0;
            this.btnChrgUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChrgUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChrgUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChrgUP.Location = new System.Drawing.Point(1255, 1);
            this.btnChrgUP.Name = "btnChrgUP";
            this.btnChrgUP.Size = new System.Drawing.Size(22, 22);
            this.btnChrgUP.TabIndex = 11;
            this.btnChrgUP.UseVisualStyleBackColor = true;
            this.btnChrgUP.MouseLeave += new System.EventHandler(this.btnChrgUP_MouseLeave);
            this.btnChrgUP.Click += new System.EventHandler(this.btnChrgUP_Click);
            this.btnChrgUP.MouseHover += new System.EventHandler(this.btnChrgUP_MouseHover);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(1277, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "label3";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1278, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = " Search";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1278, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label1";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1278, 1);
            this.label7.TabIndex = 13;
            this.label7.Text = "label1";
            // 
            // frmRpt_DailyMonthlyChargesPaySummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 639);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_DailyMonthlyChargesPaySummary";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Charge Report";
            this.Load += new System.EventHandler(this.frmRpt_DailyMonthlyChargesPaySummary_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_DailyMonthlyChargesPaySummary_FormClosed);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlCloseDaySearch.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.pnlLSTMonths.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.pnlPatients.PerformLayout();
            this.flowPnlPayment.ResumeLayout(false);
            this.pnlPayCloseDate.ResumeLayout(false);
            this.pnlPayCloseDate.PerformLayout();
            this.pnlTransPayDate.ResumeLayout(false);
            this.pnlTransPayDate.PerformLayout();
            this.pnlPayDates.ResumeLayout(false);
            this.pnlPayDates.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.pnlPayType.ResumeLayout(false);
            this.pnlPayType.PerformLayout();
            this.pnlPaySortBy.ResumeLayout(false);
            this.pnlPaySortBy.PerformLayout();
            this.pnlIncludeDtls.ResumeLayout(false);
            this.pnlIncludeDtls.PerformLayout();
            this.flwPnlDailyCharges.ResumeLayout(false);
            this.pnlCloseDate.ResumeLayout(false);
            this.pnlCloseDate.PerformLayout();
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            this.pnlChrgDates.ResumeLayout(false);
            this.pnlChrgDates.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlMultiFacility.ResumeLayout(false);
            this.pnlMultiFacility.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlMultiChargesTray.ResumeLayout(false);
            this.pnlMultiChargesTray.PerformLayout();
            this.pnlRunReportAs.ResumeLayout(false);
            this.pnlRunReportAs.PerformLayout();
            this.pnlLoginUsers.ResumeLayout(false);
            this.pnlLoginUsers.PerformLayout();
            this.pnlChargesShowHide.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_btnDailyClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlChargesShowHide;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnChrgDown;
        internal System.Windows.Forms.Button btnChrgUP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flwPnlDailyCharges;
        private System.Windows.Forms.Panel pnlCloseDate;
        private System.Windows.Forms.DateTimePicker dtpCloseDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.ComboBox cmb_Chargesdatefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.Panel pnlChrgDates;
        private System.Windows.Forms.DateTimePicker dtpChrgEndDate;
        private System.Windows.Forms.DateTimePicker dtpChrgStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.ComboBox cmbChargesProvider;
        internal System.Windows.Forms.Button btnClearCProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Panel pnlMultiFacility;
        private System.Windows.Forms.ComboBox cmbChargesMultiFacility;
        internal System.Windows.Forms.Button btnBrowseCMultiFacility;
        internal System.Windows.Forms.Button btnClearCMultiFacility;
        private System.Windows.Forms.Label lblMultiFacility;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.ComboBox cmbChargesUser;
        internal System.Windows.Forms.Button btnBrowseCUser;
        internal System.Windows.Forms.Button btnClearCUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnlMultiChargesTray;
        private System.Windows.Forms.ComboBox cmbMultiChargesTray;
        internal System.Windows.Forms.Button btnBrowseCMultiChargesTray;
        internal System.Windows.Forms.Button btnClearCMultiChargesTray;
        private System.Windows.Forms.Label lblMultiChargesTray;
        private System.Windows.Forms.Panel pnlRunReportAs;
        internal System.Windows.Forms.Button btnChrgSelectLoginUsrs;
        private System.Windows.Forms.CheckBox chkSealedCharges;
        private System.Windows.Forms.CheckBox chkOpenCharges;
        private System.Windows.Forms.Panel pnlLoginUsers;
        private System.Windows.Forms.ComboBox cmbChrgSortBy;
        private System.Windows.Forms.Label lblChrgSortBy;
        private System.Windows.Forms.CheckBox chkIncludeAllTrays;
        private System.Windows.Forms.CheckBox chkLoginUsers;
        private System.Windows.Forms.FlowLayoutPanel flowPnlPayment;
        private System.Windows.Forms.Panel pnlPayCloseDate;
        private System.Windows.Forms.DateTimePicker dtPayCloseDate;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Panel pnlTransPayDate;
        private System.Windows.Forms.ComboBox cmb_Paydatefilter;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Panel pnlPayDates;
        private System.Windows.Forms.DateTimePicker dtpPayEndDate;
        private System.Windows.Forms.DateTimePicker dtpPayStartDate;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbPaymentProvider;
        internal System.Windows.Forms.Button btnClearPayProviders;
        internal System.Windows.Forms.Button btnBrowsePayProviders;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ComboBox cmbPayFacility;
        private System.Windows.Forms.Label label53;
        internal System.Windows.Forms.Button btnBrowsePayFacility;
        internal System.Windows.Forms.Button btnClearPayFacility;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.ComboBox cmbPayUser;
        internal System.Windows.Forms.Button btnBrowsePayUser;
        internal System.Windows.Forms.Button btnClearPayUser;
        private System.Windows.Forms.Label lblPayUser;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox cmbPayTray;
        internal System.Windows.Forms.Button btnBrowsePayTray;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cmbPayProvider;
        internal System.Windows.Forms.Button btnClearPayProvider;
        internal System.Windows.Forms.Button btnBrowsePayProvider;
        internal System.Windows.Forms.Button btnClearPayTray;
        private System.Windows.Forms.Label lblPayTray;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Button btnPaySelectLoginUsrs;
        private System.Windows.Forms.Panel pnlPayType;
        private System.Windows.Forms.ComboBox cmbPayType;
        internal System.Windows.Forms.Button btnBrowsePayType;
        internal System.Windows.Forms.Button btnClearPayType;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Panel pnlPaySortBy;
        private System.Windows.Forms.ComboBox cmbPaySortBy;
        private System.Windows.Forms.Label lblPaySortBy;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Panel pnlIncludeDtls;
        private System.Windows.Forms.CheckBox chkPayIncludeDetails;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Panel pnlCloseDaySearch;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.CheckBox chkClosedShwDetails;
        private System.Windows.Forms.CheckBox chkPageBreak;
        private System.Windows.Forms.Panel pnlLSTMonths;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label lbl_pnlProviderBottomBrd;
        private System.Windows.Forms.Panel pnlProviderBody;
        private System.Windows.Forms.TreeView trvMonths;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Button btnDeSelectCreditCard;
        private System.Windows.Forms.Button btnSelectCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderHeaderBottomBrd;
        private System.Windows.Forms.Label lblCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlProviderRightBrd;
        private System.Windows.Forms.Label lbl_pnlProviderTopBrd;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label lblUserNm;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Label lblLstClosedDt;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpLastCloseDate;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvRptViewer;
    }
}