namespace gloReports.C1Reports
{
    partial class frmRpt_ChargePaymentSummary
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpChrgEndDate, dtpChrgStartDate, dtpPayEndDate, dtpPayStartDate, dateTimePicker1, dateTimePicker2 };
            System.Windows.Forms.Control[] cntControls = { dtpChrgEndDate, dtpChrgStartDate, dtpPayEndDate, dtpPayStartDate, dateTimePicker1, dateTimePicker2 };
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_ChargePaymentSummary));
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("10/10/2009");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("11/10/2009");
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSummaryContainer = new System.Windows.Forms.Panel();
            this.btnCloseCharges = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlSealCharges = new System.Windows.Forms.Panel();
            this.btnTrayDown = new System.Windows.Forms.Button();
            this.btnTrayUp = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlSummaryHeader = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabSummary = new System.Windows.Forms.TabControl();
            this.tabPgCharges = new System.Windows.Forms.TabPage();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.crvRptViewCharges = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.flwPnlDailyCharges = new System.Windows.Forms.FlowLayoutPanel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.rbChrgAudit = new System.Windows.Forms.RadioButton();
            this.rbchrgPreForClose = new System.Windows.Forms.RadioButton();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_Chargesdatefilter = new System.Windows.Forms.ComboBox();
            this.pnlChrgDates = new System.Windows.Forms.Panel();
            this.dtpChrgEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpChrgStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlCloseDate = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label46 = new System.Windows.Forms.Label();
            this.cmbChargesProvider = new System.Windows.Forms.ComboBox();
            this.btnClearCProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.pnlMultiFacility = new System.Windows.Forms.Panel();
            this.cmbChargesMultiFacility = new System.Windows.Forms.ComboBox();
            this.lblMultiFacility = new System.Windows.Forms.Label();
            this.btnBrowseCMultiFacility = new System.Windows.Forms.Button();
            this.btnClearCMultiFacility = new System.Windows.Forms.Button();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.cmbChargesUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnBrowseCUser = new System.Windows.Forms.Button();
            this.btnClearCUser = new System.Windows.Forms.Button();
            this.pnlMultiChargesTray = new System.Windows.Forms.Panel();
            this.cmbMultiChargesTray = new System.Windows.Forms.ComboBox();
            this.lblMultiChargesTray = new System.Windows.Forms.Label();
            this.btnBrowseCMultiChargesTray = new System.Windows.Forms.Button();
            this.btnClearCMultiChargesTray = new System.Windows.Forms.Button();
            this.pnlLoginUsers = new System.Windows.Forms.Panel();
            this.chkIncludeAllTrays = new System.Windows.Forms.CheckBox();
            this.chkLoginUsers = new System.Windows.Forms.CheckBox();
            this.pnlRunReportAs = new System.Windows.Forms.Panel();
            this.chkSealedCharges = new System.Windows.Forms.CheckBox();
            this.chkOpenCharges = new System.Windows.Forms.CheckBox();
            this.pnlChargesShowHide = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnChrgDown = new System.Windows.Forms.Button();
            this.btnChrgUP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPgPayment = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.crvRptViewPayment = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.label32 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.flowPnlPayment = new System.Windows.Forms.FlowLayoutPanel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.rbPayAudit = new System.Windows.Forms.RadioButton();
            this.rbPayPreForClose = new System.Windows.Forms.RadioButton();
            this.pnlTransPayDate = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.cmb_Paydatefilter = new System.Windows.Forms.ComboBox();
            this.pnlPayDates = new System.Windows.Forms.Panel();
            this.dtpPayEndDate = new System.Windows.Forms.DateTimePicker();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.dtpPayStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlPayCloseDate = new System.Windows.Forms.Panel();
            this.label55 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.pnlPayType = new System.Windows.Forms.Panel();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.btnBrowsePayType = new System.Windows.Forms.Button();
            this.btnClearPayType = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.cmbPayUser = new System.Windows.Forms.ComboBox();
            this.lblPayUser = new System.Windows.Forms.Label();
            this.btnBrowsePayUser = new System.Windows.Forms.Button();
            this.btnClearPayUser = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.cmbPayTray = new System.Windows.Forms.ComboBox();
            this.lblPayTray = new System.Windows.Forms.Label();
            this.btnBrowsePayTray = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.cmbPayProvider = new System.Windows.Forms.ComboBox();
            this.btnClearPayProvider = new System.Windows.Forms.Button();
            this.btnBrowsePayProvider = new System.Windows.Forms.Button();
            this.btnClearPayTray = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.cmbPaymentProvider = new System.Windows.Forms.ComboBox();
            this.btnClearPayProviders = new System.Windows.Forms.Button();
            this.btnBrowsePayProviders = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.cmbPayFacility = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.btnBrowsePayFacility = new System.Windows.Forms.Button();
            this.btnClearPayFacility = new System.Windows.Forms.Button();
            this.panel22 = new System.Windows.Forms.Panel();
            this.chkPayIncludeDetails = new System.Windows.Forms.CheckBox();
            this.pnlPayShowHide = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnPayDown = new System.Windows.Forms.Button();
            this.btnPayUP = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPgClosedTray = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.crvRptViewCloseDay = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pnlCloseDaySearch = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.chkClosedShwDetails = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.lblLstClosedDt = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.pnlCloseDayShowhide = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnCloseDown = new System.Windows.Forms.Button();
            this.btnCloseUP = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlSummaryContainer.SuspendLayout();
            this.pnlSealCharges.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabPgCharges.SuspendLayout();
            this.panel21.SuspendLayout();
            this.flwPnlDailyCharges.SuspendLayout();
            this.panel18.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            this.pnlChrgDates.SuspendLayout();
            this.pnlCloseDate.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlMultiFacility.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlMultiChargesTray.SuspendLayout();
            this.pnlLoginUsers.SuspendLayout();
            this.pnlRunReportAs.SuspendLayout();
            this.pnlChargesShowHide.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPgPayment.SuspendLayout();
            this.panel20.SuspendLayout();
            this.flowPnlPayment.SuspendLayout();
            this.panel19.SuspendLayout();
            this.pnlTransPayDate.SuspendLayout();
            this.pnlPayDates.SuspendLayout();
            this.pnlPayCloseDate.SuspendLayout();
            this.pnlPayType.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel22.SuspendLayout();
            this.pnlPayShowHide.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabPgClosedTray.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCloseDaySearch.SuspendLayout();
            this.panel17.SuspendLayout();
            this.pnlLSTMonths.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.panel15.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.pnlCloseDayShowhide.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
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
            this.pnlToolStrip.Size = new System.Drawing.Size(1284, 55);
            this.pnlToolStrip.TabIndex = 29;
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
            this.tsb_btnExportReport.Text = "Export";
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
            // pnlSummaryContainer
            // 
            this.pnlSummaryContainer.Controls.Add(this.btnCloseCharges);
            this.pnlSummaryContainer.Controls.Add(this.label19);
            this.pnlSummaryContainer.Controls.Add(this.label18);
            this.pnlSummaryContainer.Controls.Add(this.label17);
            this.pnlSummaryContainer.Controls.Add(this.label16);
            this.pnlSummaryContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryContainer.Location = new System.Drawing.Point(0, 708);
            this.pnlSummaryContainer.Name = "pnlSummaryContainer";
            this.pnlSummaryContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSummaryContainer.Size = new System.Drawing.Size(1284, 38);
            this.pnlSummaryContainer.TabIndex = 263;
            // 
            // btnCloseCharges
            // 
            this.btnCloseCharges.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseCharges.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCloseCharges.BackgroundImage")));
            this.btnCloseCharges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseCharges.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCloseCharges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseCharges.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCloseCharges.Location = new System.Drawing.Point(433, 10);
            this.btnCloseCharges.Name = "btnCloseCharges";
            this.btnCloseCharges.Size = new System.Drawing.Size(135, 22);
            this.btnCloseCharges.TabIndex = 195;
            this.btnCloseCharges.Text = "Close Charges";
            this.btnCloseCharges.UseVisualStyleBackColor = false;
            this.btnCloseCharges.Click += new System.EventHandler(this.btnCloseCharges_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(4, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1276, 1);
            this.label19.TabIndex = 15;
            this.label19.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(4, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1276, 1);
            this.label18.TabIndex = 14;
            this.label18.Text = "label1";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1280, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 32);
            this.label17.TabIndex = 9;
            this.label17.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 32);
            this.label16.TabIndex = 8;
            this.label16.Text = "label4";
            // 
            // pnlSealCharges
            // 
            this.pnlSealCharges.BackColor = System.Drawing.Color.Transparent;
            this.pnlSealCharges.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.pnlSealCharges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSealCharges.Controls.Add(this.btnTrayDown);
            this.pnlSealCharges.Controls.Add(this.btnTrayUp);
            this.pnlSealCharges.Controls.Add(this.label12);
            this.pnlSealCharges.Controls.Add(this.label13);
            this.pnlSealCharges.Controls.Add(this.pnlSummaryHeader);
            this.pnlSealCharges.Controls.Add(this.label15);
            this.pnlSealCharges.Controls.Add(this.label11);
            this.pnlSealCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSealCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSealCharges.Location = new System.Drawing.Point(3, 3);
            this.pnlSealCharges.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlSealCharges.Name = "pnlSealCharges";
            this.pnlSealCharges.Size = new System.Drawing.Size(1278, 23);
            this.pnlSealCharges.TabIndex = 264;
            this.pnlSealCharges.Tag = "Orange";
            // 
            // btnTrayDown
            // 
            this.btnTrayDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTrayDown.FlatAppearance.BorderSize = 0;
            this.btnTrayDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTrayDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTrayDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrayDown.Location = new System.Drawing.Point(1233, 1);
            this.btnTrayDown.Name = "btnTrayDown";
            this.btnTrayDown.Size = new System.Drawing.Size(22, 21);
            this.btnTrayDown.TabIndex = 12;
            this.btnTrayDown.UseVisualStyleBackColor = true;
            this.btnTrayDown.MouseLeave += new System.EventHandler(this.btnTrayDown_MouseLeave);
            this.btnTrayDown.Click += new System.EventHandler(this.btnTrayDown_Click);
            this.btnTrayDown.MouseHover += new System.EventHandler(this.btnTrayDown_MouseHover);
            // 
            // btnTrayUp
            // 
            this.btnTrayUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTrayUp.FlatAppearance.BorderSize = 0;
            this.btnTrayUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTrayUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTrayUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrayUp.Location = new System.Drawing.Point(1255, 1);
            this.btnTrayUp.Name = "btnTrayUp";
            this.btnTrayUp.Size = new System.Drawing.Size(22, 21);
            this.btnTrayUp.TabIndex = 11;
            this.btnTrayUp.UseVisualStyleBackColor = true;
            this.btnTrayUp.MouseLeave += new System.EventHandler(this.btnTrayUp_MouseLeave);
            this.btnTrayUp.Click += new System.EventHandler(this.btnTrayUp_Click);
            this.btnTrayUp.MouseHover += new System.EventHandler(this.btnTrayUp_MouseHover);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 21);
            this.label12.TabIndex = 7;
            this.label12.Text = "label4";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(1277, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 21);
            this.label13.TabIndex = 6;
            this.label13.Text = "label3";
            // 
            // pnlSummaryHeader
            // 
            this.pnlSummaryHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummaryHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSummaryHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSummaryHeader.Location = new System.Drawing.Point(0, 1);
            this.pnlSummaryHeader.Name = "pnlSummaryHeader";
            this.pnlSummaryHeader.Size = new System.Drawing.Size(1278, 21);
            this.pnlSummaryHeader.TabIndex = 9;
            this.pnlSummaryHeader.Text = "Close Charges";
            this.pnlSummaryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1278, 1);
            this.label15.TabIndex = 5;
            this.label15.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1278, 1);
            this.label11.TabIndex = 13;
            this.label11.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabSummary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 627);
            this.panel1.TabIndex = 265;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.tabPgCharges);
            this.tabSummary.Controls.Add(this.tabPgPayment);
            this.tabSummary.Controls.Add(this.tabPgClosedTray);
            this.tabSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSummary.Location = new System.Drawing.Point(0, 0);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.SelectedIndex = 0;
            this.tabSummary.Size = new System.Drawing.Size(1284, 627);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.SelectedIndexChanged += new System.EventHandler(this.tabSummary_SelectedIndexChanged);
            // 
            // tabPgCharges
            // 
            this.tabPgCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgCharges.Controls.Add(this.panel21);
            this.tabPgCharges.Controls.Add(this.flwPnlDailyCharges);
            this.tabPgCharges.Controls.Add(this.pnlChargesShowHide);
            this.tabPgCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tabPgCharges.Location = new System.Drawing.Point(4, 23);
            this.tabPgCharges.Name = "tabPgCharges";
            this.tabPgCharges.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgCharges.Size = new System.Drawing.Size(1276, 600);
            this.tabPgCharges.TabIndex = 0;
            this.tabPgCharges.Tag = "DailyCharges";
            this.tabPgCharges.Text = "Daily Charge Report";
            this.tabPgCharges.ToolTipText = "Daily Charge Report";
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label59);
            this.panel21.Controls.Add(this.label58);
            this.panel21.Controls.Add(this.label57);
            this.panel21.Controls.Add(this.label41);
            this.panel21.Controls.Add(this.crvRptViewCharges);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(3, 189);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel21.Size = new System.Drawing.Size(1270, 408);
            this.panel21.TabIndex = 270;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(1, 407);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1268, 1);
            this.label59.TabIndex = 273;
            this.label59.Text = "label4";
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Top;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(1, 3);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(1268, 1);
            this.label58.TabIndex = 272;
            this.label58.Text = "label4";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Right;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(1269, 3);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1, 405);
            this.label57.TabIndex = 271;
            this.label57.Text = "label4";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Left;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(0, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 405);
            this.label41.TabIndex = 270;
            this.label41.Text = "label4";
            // 
            // crvRptViewCharges
            // 
            this.crvRptViewCharges.ActiveViewIndex = -1;
            this.crvRptViewCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRptViewCharges.CausesValidation = false;
            this.crvRptViewCharges.DisplayBackgroundEdge = false;
            this.crvRptViewCharges.DisplayGroupTree = false;
            this.crvRptViewCharges.DisplayStatusBar = false;
            this.crvRptViewCharges.DisplayToolbar = false;
            this.crvRptViewCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvRptViewCharges.EnableDrillDown = false;
            this.crvRptViewCharges.Location = new System.Drawing.Point(0, 3);
            this.crvRptViewCharges.Name = "crvRptViewCharges";
            this.crvRptViewCharges.SelectionFormula = "";
            this.crvRptViewCharges.ShowCloseButton = false;
            this.crvRptViewCharges.ShowGroupTreeButton = false;
            this.crvRptViewCharges.ShowPrintButton = false;
            this.crvRptViewCharges.ShowRefreshButton = false;
            this.crvRptViewCharges.Size = new System.Drawing.Size(1270, 405);
            this.crvRptViewCharges.TabIndex = 269;
            this.crvRptViewCharges.ViewTimeSelectionFormula = "";
            // 
            // flwPnlDailyCharges
            // 
            this.flwPnlDailyCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwPnlDailyCharges.Controls.Add(this.panel18);
            this.flwPnlDailyCharges.Controls.Add(this.pnlTransDate);
            this.flwPnlDailyCharges.Controls.Add(this.pnlChrgDates);
            this.flwPnlDailyCharges.Controls.Add(this.pnlCloseDate);
            this.flwPnlDailyCharges.Controls.Add(this.pnlProvider);
            this.flwPnlDailyCharges.Controls.Add(this.pnlMultiFacility);
            this.flwPnlDailyCharges.Controls.Add(this.pnlUser);
            this.flwPnlDailyCharges.Controls.Add(this.pnlMultiChargesTray);
            this.flwPnlDailyCharges.Controls.Add(this.pnlLoginUsers);
            this.flwPnlDailyCharges.Controls.Add(this.pnlRunReportAs);
            this.flwPnlDailyCharges.Dock = System.Windows.Forms.DockStyle.Top;
            this.flwPnlDailyCharges.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwPnlDailyCharges.Location = new System.Drawing.Point(3, 27);
            this.flwPnlDailyCharges.Name = "flwPnlDailyCharges";
            this.flwPnlDailyCharges.Size = new System.Drawing.Size(1270, 162);
            this.flwPnlDailyCharges.TabIndex = 246;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.rbChrgAudit);
            this.panel18.Controls.Add(this.rbchrgPreForClose);
            this.panel18.Location = new System.Drawing.Point(3, 3);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(236, 26);
            this.panel18.TabIndex = 244;
            // 
            // rbChrgAudit
            // 
            this.rbChrgAudit.AutoSize = true;
            this.rbChrgAudit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbChrgAudit.Location = new System.Drawing.Point(166, 5);
            this.rbChrgAudit.Name = "rbChrgAudit";
            this.rbChrgAudit.Size = new System.Drawing.Size(54, 18);
            this.rbChrgAudit.TabIndex = 1;
            this.rbChrgAudit.Text = "Audit";
            this.rbChrgAudit.UseVisualStyleBackColor = true;
            this.rbChrgAudit.CheckedChanged += new System.EventHandler(this.rbChrgAudit_CheckedChanged);
            // 
            // rbchrgPreForClose
            // 
            this.rbchrgPreForClose.AutoSize = true;
            this.rbchrgPreForClose.Checked = true;
            this.rbchrgPreForClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbchrgPreForClose.Location = new System.Drawing.Point(8, 4);
            this.rbchrgPreForClose.Name = "rbchrgPreForClose";
            this.rbchrgPreForClose.Size = new System.Drawing.Size(128, 18);
            this.rbchrgPreForClose.TabIndex = 0;
            this.rbchrgPreForClose.TabStop = true;
            this.rbchrgPreForClose.Text = "Prepare for close";
            this.rbchrgPreForClose.UseVisualStyleBackColor = true;
            this.rbchrgPreForClose.CheckedChanged += new System.EventHandler(this.rbchrgPreForClose_CheckedChanged);
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.cmb_Chargesdatefilter);
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransDate.Location = new System.Drawing.Point(3, 35);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(236, 26);
            this.pnlTransDate.TabIndex = 242;
            this.pnlTransDate.Visible = false;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(53, 5);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(73, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Close Date :";
            // 
            // cmb_Chargesdatefilter
            // 
            this.cmb_Chargesdatefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Chargesdatefilter.FormattingEnabled = true;
            this.cmb_Chargesdatefilter.Location = new System.Drawing.Point(117, 2);
            this.cmb_Chargesdatefilter.Name = "cmb_Chargesdatefilter";
            this.cmb_Chargesdatefilter.Size = new System.Drawing.Size(103, 22);
            this.cmb_Chargesdatefilter.TabIndex = 217;
            this.cmb_Chargesdatefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_Chargesdatefilter_SelectedIndexChanged);
            // 
            // pnlChrgDates
            // 
            this.pnlChrgDates.Controls.Add(this.dtpChrgEndDate);
            this.pnlChrgDates.Controls.Add(this.dtpChrgStartDate);
            this.pnlChrgDates.Controls.Add(this.lblStartDate);
            this.pnlChrgDates.Controls.Add(this.lblEndDate);
            this.pnlChrgDates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlChrgDates.Location = new System.Drawing.Point(3, 67);
            this.pnlChrgDates.Name = "pnlChrgDates";
            this.pnlChrgDates.Size = new System.Drawing.Size(236, 55);
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
            this.dtpChrgEndDate.Checked = false;
            this.dtpChrgEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpChrgEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChrgEndDate.Location = new System.Drawing.Point(117, 29);
            this.dtpChrgEndDate.Name = "dtpChrgEndDate";
            this.dtpChrgEndDate.Size = new System.Drawing.Size(103, 22);
            this.dtpChrgEndDate.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(54, 7);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(60, 33);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
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
            this.dtpChrgStartDate.Location = new System.Drawing.Point(117, 3);
            this.dtpChrgStartDate.Name = "dtpChrgStartDate";
            this.dtpChrgStartDate.Size = new System.Drawing.Size(103, 22);
            this.dtpChrgStartDate.TabIndex = 5;
            // 
            // pnlCloseDate
            // 
            this.pnlCloseDate.Controls.Add(this.dateTimePicker1);
            this.pnlCloseDate.Controls.Add(this.label3);
            this.pnlCloseDate.Location = new System.Drawing.Point(3, 128);
            this.pnlCloseDate.Name = "pnlCloseDate";
            this.pnlCloseDate.Size = new System.Drawing.Size(236, 26);
            this.pnlCloseDate.TabIndex = 245;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Close Date :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker1.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(117, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(103, 22);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.cmbChargesProvider);
            this.pnlProvider.Controls.Add(this.btnClearCProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Controls.Add(this.label46);
            this.pnlProvider.Location = new System.Drawing.Point(245, 3);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(328, 29);
            this.pnlProvider.TabIndex = 241;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(43, 8);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(59, 14);
            this.label46.TabIndex = 185;
            this.label46.Text = "Provider :";
            // 
            // cmbChargesProvider
            // 
            this.cmbChargesProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesProvider.FormattingEnabled = true;
            this.cmbChargesProvider.Location = new System.Drawing.Point(94, 4);
            this.cmbChargesProvider.Name = "cmbChargesProvider";
            this.cmbChargesProvider.Size = new System.Drawing.Size(174, 22);
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
            this.btnClearCProvider.Location = new System.Drawing.Point(296, 5);
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
            this.btnBrowseProvider.Location = new System.Drawing.Point(272, 5);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // pnlMultiFacility
            // 
            this.pnlMultiFacility.Controls.Add(this.cmbChargesMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnBrowseCMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnClearCMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.lblMultiFacility);
            this.pnlMultiFacility.Location = new System.Drawing.Point(245, 38);
            this.pnlMultiFacility.Name = "pnlMultiFacility";
            this.pnlMultiFacility.Size = new System.Drawing.Size(328, 29);
            this.pnlMultiFacility.TabIndex = 233;
            // 
            // cmbChargesMultiFacility
            // 
            this.cmbChargesMultiFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesMultiFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesMultiFacility.FormattingEnabled = true;
            this.cmbChargesMultiFacility.Location = new System.Drawing.Point(94, 3);
            this.cmbChargesMultiFacility.Name = "cmbChargesMultiFacility";
            this.cmbChargesMultiFacility.Size = new System.Drawing.Size(174, 22);
            this.cmbChargesMultiFacility.TabIndex = 197;
            // 
            // lblMultiFacility
            // 
            this.lblMultiFacility.AutoSize = true;
            this.lblMultiFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiFacility.Location = new System.Drawing.Point(52, 7);
            this.lblMultiFacility.Name = "lblMultiFacility";
            this.lblMultiFacility.Size = new System.Drawing.Size(50, 14);
            this.lblMultiFacility.TabIndex = 196;
            this.lblMultiFacility.Text = "Facility :";
            // 
            // btnBrowseCMultiFacility
            // 
            this.btnBrowseCMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiFacility.BackgroundImage")));
            this.btnBrowseCMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiFacility.Image")));
            this.btnBrowseCMultiFacility.Location = new System.Drawing.Point(272, 4);
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
            this.btnClearCMultiFacility.Location = new System.Drawing.Point(296, 4);
            this.btnClearCMultiFacility.Name = "btnClearCMultiFacility";
            this.btnClearCMultiFacility.Size = new System.Drawing.Size(20, 20);
            this.btnClearCMultiFacility.TabIndex = 195;
            this.btnClearCMultiFacility.UseVisualStyleBackColor = false;
            this.btnClearCMultiFacility.Click += new System.EventHandler(this.btnClearMultiFacility_Click);
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.cmbChargesUser);
            this.pnlUser.Controls.Add(this.btnBrowseCUser);
            this.pnlUser.Controls.Add(this.btnClearCUser);
            this.pnlUser.Controls.Add(this.lblUser);
            this.pnlUser.Location = new System.Drawing.Point(245, 73);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(328, 29);
            this.pnlUser.TabIndex = 238;
            // 
            // cmbChargesUser
            // 
            this.cmbChargesUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesUser.FormattingEnabled = true;
            this.cmbChargesUser.Location = new System.Drawing.Point(94, 3);
            this.cmbChargesUser.Name = "cmbChargesUser";
            this.cmbChargesUser.Size = new System.Drawing.Size(174, 22);
            this.cmbChargesUser.TabIndex = 197;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(63, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 196;
            this.lblUser.Text = "User :";
            // 
            // btnBrowseCUser
            // 
            this.btnBrowseCUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCUser.BackgroundImage")));
            this.btnBrowseCUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCUser.Image")));
            this.btnBrowseCUser.Location = new System.Drawing.Point(272, 4);
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
            this.btnClearCUser.Location = new System.Drawing.Point(296, 4);
            this.btnClearCUser.Name = "btnClearCUser";
            this.btnClearCUser.Size = new System.Drawing.Size(20, 20);
            this.btnClearCUser.TabIndex = 195;
            this.btnClearCUser.UseVisualStyleBackColor = false;
            this.btnClearCUser.Click += new System.EventHandler(this.btnClearUser_Click);
            // 
            // pnlMultiChargesTray
            // 
            this.pnlMultiChargesTray.Controls.Add(this.cmbMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnBrowseCMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnClearCMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.lblMultiChargesTray);
            this.pnlMultiChargesTray.Location = new System.Drawing.Point(245, 108);
            this.pnlMultiChargesTray.Name = "pnlMultiChargesTray";
            this.pnlMultiChargesTray.Size = new System.Drawing.Size(328, 29);
            this.pnlMultiChargesTray.TabIndex = 236;
            // 
            // cmbMultiChargesTray
            // 
            this.cmbMultiChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiChargesTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMultiChargesTray.FormattingEnabled = true;
            this.cmbMultiChargesTray.Location = new System.Drawing.Point(94, 4);
            this.cmbMultiChargesTray.Name = "cmbMultiChargesTray";
            this.cmbMultiChargesTray.Size = new System.Drawing.Size(174, 22);
            this.cmbMultiChargesTray.TabIndex = 197;
            // 
            // lblMultiChargesTray
            // 
            this.lblMultiChargesTray.AutoSize = true;
            this.lblMultiChargesTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiChargesTray.Location = new System.Drawing.Point(21, 7);
            this.lblMultiChargesTray.Name = "lblMultiChargesTray";
            this.lblMultiChargesTray.Size = new System.Drawing.Size(86, 14);
            this.lblMultiChargesTray.TabIndex = 196;
            this.lblMultiChargesTray.Text = "Charges Tray :";
            // 
            // btnBrowseCMultiChargesTray
            // 
            this.btnBrowseCMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiChargesTray.BackgroundImage")));
            this.btnBrowseCMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCMultiChargesTray.Image")));
            this.btnBrowseCMultiChargesTray.Location = new System.Drawing.Point(272, 5);
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
            this.btnClearCMultiChargesTray.Location = new System.Drawing.Point(296, 5);
            this.btnClearCMultiChargesTray.Name = "btnClearCMultiChargesTray";
            this.btnClearCMultiChargesTray.Size = new System.Drawing.Size(20, 20);
            this.btnClearCMultiChargesTray.TabIndex = 195;
            this.btnClearCMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnClearCMultiChargesTray.Click += new System.EventHandler(this.btnClearMultiChargesTray_Click);
            // 
            // pnlLoginUsers
            // 
            this.pnlLoginUsers.Controls.Add(this.chkIncludeAllTrays);
            this.pnlLoginUsers.Controls.Add(this.chkLoginUsers);
            this.pnlLoginUsers.Location = new System.Drawing.Point(579, 3);
            this.pnlLoginUsers.Name = "pnlLoginUsers";
            this.pnlLoginUsers.Size = new System.Drawing.Size(328, 29);
            this.pnlLoginUsers.TabIndex = 239;
            // 
            // chkIncludeAllTrays
            // 
            this.chkIncludeAllTrays.AutoSize = true;
            this.chkIncludeAllTrays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeAllTrays.Location = new System.Drawing.Point(170, 6);
            this.chkIncludeAllTrays.Name = "chkIncludeAllTrays";
            this.chkIncludeAllTrays.Size = new System.Drawing.Size(119, 18);
            this.chkIncludeAllTrays.TabIndex = 1;
            this.chkIncludeAllTrays.Text = "Include All Trays ";
            this.chkIncludeAllTrays.UseVisualStyleBackColor = true;
            // 
            // chkLoginUsers
            // 
            this.chkLoginUsers.AutoSize = true;
            this.chkLoginUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLoginUsers.Location = new System.Drawing.Point(14, 6);
            this.chkLoginUsers.Name = "chkLoginUsers";
            this.chkLoginUsers.Size = new System.Drawing.Size(116, 18);
            this.chkLoginUsers.TabIndex = 0;
            this.chkLoginUsers.Text = "Login Users Only";
            this.chkLoginUsers.UseVisualStyleBackColor = true;
            // 
            // pnlRunReportAs
            // 
            this.pnlRunReportAs.Controls.Add(this.chkSealedCharges);
            this.pnlRunReportAs.Controls.Add(this.chkOpenCharges);
            this.pnlRunReportAs.Location = new System.Drawing.Point(579, 38);
            this.pnlRunReportAs.Name = "pnlRunReportAs";
            this.pnlRunReportAs.Size = new System.Drawing.Size(328, 29);
            this.pnlRunReportAs.TabIndex = 240;
            // 
            // chkSealedCharges
            // 
            this.chkSealedCharges.AutoSize = true;
            this.chkSealedCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSealedCharges.Location = new System.Drawing.Point(170, 6);
            this.chkSealedCharges.Name = "chkSealedCharges";
            this.chkSealedCharges.Size = new System.Drawing.Size(153, 18);
            this.chkSealedCharges.TabIndex = 3;
            this.chkSealedCharges.Text = "Include Sealed Charges";
            this.chkSealedCharges.UseVisualStyleBackColor = true;
            // 
            // chkOpenCharges
            // 
            this.chkOpenCharges.AutoSize = true;
            this.chkOpenCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOpenCharges.Location = new System.Drawing.Point(14, 6);
            this.chkOpenCharges.Name = "chkOpenCharges";
            this.chkOpenCharges.Size = new System.Drawing.Size(147, 18);
            this.chkOpenCharges.TabIndex = 2;
            this.chkOpenCharges.Text = "Include Open Charges";
            this.chkOpenCharges.UseVisualStyleBackColor = true;
            // 
            // pnlChargesShowHide
            // 
            this.pnlChargesShowHide.Controls.Add(this.panel4);
            this.pnlChargesShowHide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChargesShowHide.Location = new System.Drawing.Point(3, 3);
            this.pnlChargesShowHide.Name = "pnlChargesShowHide";
            this.pnlChargesShowHide.Size = new System.Drawing.Size(1270, 24);
            this.pnlChargesShowHide.TabIndex = 267;
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
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1270, 24);
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
            this.btnChrgDown.Location = new System.Drawing.Point(1225, 1);
            this.btnChrgDown.Name = "btnChrgDown";
            this.btnChrgDown.Size = new System.Drawing.Size(22, 23);
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
            this.btnChrgUP.Location = new System.Drawing.Point(1247, 1);
            this.btnChrgUP.Name = "btnChrgUP";
            this.btnChrgUP.Size = new System.Drawing.Size(22, 23);
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
            this.label1.Size = new System.Drawing.Size(1, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(1269, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 23);
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
            this.label4.Size = new System.Drawing.Size(1270, 23);
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
            this.label5.Size = new System.Drawing.Size(1270, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label1";
            // 
            // tabPgPayment
            // 
            this.tabPgPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgPayment.Controls.Add(this.panel20);
            this.tabPgPayment.Controls.Add(this.flowPnlPayment);
            this.tabPgPayment.Controls.Add(this.pnlPayShowHide);
            this.tabPgPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tabPgPayment.Location = new System.Drawing.Point(4, 23);
            this.tabPgPayment.Name = "tabPgPayment";
            this.tabPgPayment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgPayment.Size = new System.Drawing.Size(1276, 600);
            this.tabPgPayment.TabIndex = 1;
            this.tabPgPayment.Tag = "DailyPayment";
            this.tabPgPayment.Text = "Daily Payment Report";
            this.tabPgPayment.ToolTipText = "DailyPayment";
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.label14);
            this.panel20.Controls.Add(this.crvRptViewPayment);
            this.panel20.Controls.Add(this.label32);
            this.panel20.Controls.Add(this.label39);
            this.panel20.Controls.Add(this.label40);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(3, 187);
            this.panel20.Name = "panel20";
            this.panel20.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel20.Size = new System.Drawing.Size(1270, 410);
            this.panel20.TabIndex = 271;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1268, 1);
            this.label14.TabIndex = 15;
            this.label14.Text = "label1";
            // 
            // crvRptViewPayment
            // 
            this.crvRptViewPayment.ActiveViewIndex = -1;
            this.crvRptViewPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRptViewPayment.CausesValidation = false;
            this.crvRptViewPayment.DisplayBackgroundEdge = false;
            this.crvRptViewPayment.DisplayGroupTree = false;
            this.crvRptViewPayment.DisplayStatusBar = false;
            this.crvRptViewPayment.DisplayToolbar = false;
            this.crvRptViewPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvRptViewPayment.EnableDrillDown = false;
            this.crvRptViewPayment.Location = new System.Drawing.Point(1, 3);
            this.crvRptViewPayment.Name = "crvRptViewPayment";
            this.crvRptViewPayment.ReportSource = "";
            this.crvRptViewPayment.SelectionFormula = "";
            this.crvRptViewPayment.ShowCloseButton = false;
            this.crvRptViewPayment.ShowGroupTreeButton = false;
            this.crvRptViewPayment.ShowPrintButton = false;
            this.crvRptViewPayment.ShowRefreshButton = false;
            this.crvRptViewPayment.Size = new System.Drawing.Size(1268, 406);
            this.crvRptViewPayment.TabIndex = 270;
            this.crvRptViewPayment.ViewTimeSelectionFormula = "";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(1, 409);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1268, 1);
            this.label32.TabIndex = 14;
            this.label32.Text = "label1";
            this.label32.Visible = false;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Right;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(1269, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 407);
            this.label39.TabIndex = 9;
            this.label39.Text = "label4";
            this.label39.Visible = false;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(0, 3);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 407);
            this.label40.TabIndex = 8;
            this.label40.Text = "label4";
            this.label40.Visible = false;
            // 
            // flowPnlPayment
            // 
            this.flowPnlPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPnlPayment.Controls.Add(this.panel19);
            this.flowPnlPayment.Controls.Add(this.pnlTransPayDate);
            this.flowPnlPayment.Controls.Add(this.pnlPayDates);
            this.flowPnlPayment.Controls.Add(this.pnlPayCloseDate);
            this.flowPnlPayment.Controls.Add(this.pnlPayType);
            this.flowPnlPayment.Controls.Add(this.panel13);
            this.flowPnlPayment.Controls.Add(this.panel11);
            this.flowPnlPayment.Controls.Add(this.panel3);
            this.flowPnlPayment.Controls.Add(this.panel14);
            this.flowPnlPayment.Controls.Add(this.panel22);
            this.flowPnlPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPnlPayment.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPnlPayment.Location = new System.Drawing.Point(3, 27);
            this.flowPnlPayment.Name = "flowPnlPayment";
            this.flowPnlPayment.Size = new System.Drawing.Size(1270, 160);
            this.flowPnlPayment.TabIndex = 253;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.rbPayAudit);
            this.panel19.Controls.Add(this.rbPayPreForClose);
            this.panel19.Location = new System.Drawing.Point(3, 3);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(236, 26);
            this.panel19.TabIndex = 250;
            // 
            // rbPayAudit
            // 
            this.rbPayAudit.AutoSize = true;
            this.rbPayAudit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPayAudit.Location = new System.Drawing.Point(166, 5);
            this.rbPayAudit.Name = "rbPayAudit";
            this.rbPayAudit.Size = new System.Drawing.Size(54, 18);
            this.rbPayAudit.TabIndex = 1;
            this.rbPayAudit.Text = "Audit";
            this.rbPayAudit.UseVisualStyleBackColor = true;
            this.rbPayAudit.CheckedChanged += new System.EventHandler(this.rbPayAudit_CheckedChanged);
            // 
            // rbPayPreForClose
            // 
            this.rbPayPreForClose.AutoSize = true;
            this.rbPayPreForClose.Checked = true;
            this.rbPayPreForClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPayPreForClose.Location = new System.Drawing.Point(8, 4);
            this.rbPayPreForClose.Name = "rbPayPreForClose";
            this.rbPayPreForClose.Size = new System.Drawing.Size(128, 18);
            this.rbPayPreForClose.TabIndex = 0;
            this.rbPayPreForClose.TabStop = true;
            this.rbPayPreForClose.Text = "Prepare for close";
            this.rbPayPreForClose.UseVisualStyleBackColor = true;
            this.rbPayPreForClose.CheckedChanged += new System.EventHandler(this.rbPayPreForClose_CheckedChanged);
            // 
            // pnlTransPayDate
            // 
            this.pnlTransPayDate.Controls.Add(this.cmb_Paydatefilter);
            this.pnlTransPayDate.Controls.Add(this.label49);
            this.pnlTransPayDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransPayDate.Location = new System.Drawing.Point(3, 35);
            this.pnlTransPayDate.Name = "pnlTransPayDate";
            this.pnlTransPayDate.Size = new System.Drawing.Size(236, 26);
            this.pnlTransPayDate.TabIndex = 248;
            this.pnlTransPayDate.Visible = false;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(53, 6);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(73, 14);
            this.label49.TabIndex = 216;
            this.label49.Text = "Close Date :";
            // 
            // cmb_Paydatefilter
            // 
            this.cmb_Paydatefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Paydatefilter.FormattingEnabled = true;
            this.cmb_Paydatefilter.Location = new System.Drawing.Point(117, 2);
            this.cmb_Paydatefilter.Name = "cmb_Paydatefilter";
            this.cmb_Paydatefilter.Size = new System.Drawing.Size(103, 22);
            this.cmb_Paydatefilter.TabIndex = 217;
            this.cmb_Paydatefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_Paydatefilter_SelectedIndexChanged);
            // 
            // pnlPayDates
            // 
            this.pnlPayDates.Controls.Add(this.dtpPayEndDate);
            this.pnlPayDates.Controls.Add(this.dtpPayStartDate);
            this.pnlPayDates.Controls.Add(this.label47);
            this.pnlPayDates.Controls.Add(this.label48);
            this.pnlPayDates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPayDates.Location = new System.Drawing.Point(3, 67);
            this.pnlPayDates.Name = "pnlPayDates";
            this.pnlPayDates.Size = new System.Drawing.Size(236, 55);
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
            this.dtpPayEndDate.Checked = false;
            this.dtpPayEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpPayEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPayEndDate.Location = new System.Drawing.Point(117, 29);
            this.dtpPayEndDate.Name = "dtpPayEndDate";
            this.dtpPayEndDate.Size = new System.Drawing.Size(103, 22);
            this.dtpPayEndDate.TabIndex = 7;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(54, 6);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(72, 14);
            this.label47.TabIndex = 4;
            this.label47.Text = "Start Date :";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(60, 33);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(66, 14);
            this.label48.TabIndex = 6;
            this.label48.Text = "End Date :";
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
            this.dtpPayStartDate.Location = new System.Drawing.Point(117, 3);
            this.dtpPayStartDate.Name = "dtpPayStartDate";
            this.dtpPayStartDate.Size = new System.Drawing.Size(103, 22);
            this.dtpPayStartDate.TabIndex = 5;
            // 
            // pnlPayCloseDate
            // 
            this.pnlPayCloseDate.Controls.Add(this.dateTimePicker2);
            this.pnlPayCloseDate.Controls.Add(this.label55);
            this.pnlPayCloseDate.Location = new System.Drawing.Point(3, 128);
            this.pnlPayCloseDate.Name = "pnlPayCloseDate";
            this.pnlPayCloseDate.Size = new System.Drawing.Size(236, 26);
            this.pnlPayCloseDate.TabIndex = 251;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(53, 6);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(73, 14);
            this.label55.TabIndex = 6;
            this.label55.Text = "Close Date :";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker2.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker2.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker2.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker2.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(117, 2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(103, 22);
            this.dateTimePicker2.TabIndex = 7;
            // 
            // pnlPayType
            // 
            this.pnlPayType.Controls.Add(this.cmbPayType);
            this.pnlPayType.Controls.Add(this.btnBrowsePayType);
            this.pnlPayType.Controls.Add(this.btnClearPayType);
            this.pnlPayType.Controls.Add(this.label56);
            this.pnlPayType.Location = new System.Drawing.Point(245, 3);
            this.pnlPayType.Name = "pnlPayType";
            this.pnlPayType.Size = new System.Drawing.Size(328, 29);
            this.pnlPayType.TabIndex = 252;
            // 
            // cmbPayType
            // 
            this.cmbPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Location = new System.Drawing.Point(94, 4);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(174, 22);
            this.cmbPayType.TabIndex = 197;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(57, 7);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(43, 14);
            this.label56.TabIndex = 196;
            this.label56.Text = "Type :";
            // 
            // btnBrowsePayType
            // 
            this.btnBrowsePayType.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayType.BackgroundImage")));
            this.btnBrowsePayType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayType.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayType.Image")));
            this.btnBrowsePayType.Location = new System.Drawing.Point(272, 5);
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
            this.btnClearPayType.Location = new System.Drawing.Point(296, 5);
            this.btnClearPayType.Name = "btnClearPayType";
            this.btnClearPayType.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayType.TabIndex = 195;
            this.btnClearPayType.UseVisualStyleBackColor = false;
            this.btnClearPayType.Click += new System.EventHandler(this.btnClearPayType_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.cmbPayUser);
            this.panel13.Controls.Add(this.btnBrowsePayUser);
            this.panel13.Controls.Add(this.btnClearPayUser);
            this.panel13.Controls.Add(this.lblPayUser);
            this.panel13.Location = new System.Drawing.Point(245, 38);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(328, 29);
            this.panel13.TabIndex = 246;
            // 
            // cmbPayUser
            // 
            this.cmbPayUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayUser.FormattingEnabled = true;
            this.cmbPayUser.Location = new System.Drawing.Point(94, 4);
            this.cmbPayUser.Name = "cmbPayUser";
            this.cmbPayUser.Size = new System.Drawing.Size(174, 22);
            this.cmbPayUser.TabIndex = 197;
            // 
            // lblPayUser
            // 
            this.lblPayUser.AutoSize = true;
            this.lblPayUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayUser.Location = new System.Drawing.Point(60, 8);
            this.lblPayUser.Name = "lblPayUser";
            this.lblPayUser.Size = new System.Drawing.Size(39, 14);
            this.lblPayUser.TabIndex = 196;
            this.lblPayUser.Text = "User :";
            // 
            // btnBrowsePayUser
            // 
            this.btnBrowsePayUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayUser.BackgroundImage")));
            this.btnBrowsePayUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayUser.Image")));
            this.btnBrowsePayUser.Location = new System.Drawing.Point(272, 5);
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
            this.btnClearPayUser.Location = new System.Drawing.Point(296, 5);
            this.btnClearPayUser.Name = "btnClearPayUser";
            this.btnClearPayUser.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayUser.TabIndex = 195;
            this.btnClearPayUser.UseVisualStyleBackColor = false;
            this.btnClearPayUser.Click += new System.EventHandler(this.btnClearPayUser_Click);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.cmbPayTray);
            this.panel11.Controls.Add(this.btnBrowsePayTray);
            this.panel11.Controls.Add(this.panel9);
            this.panel11.Controls.Add(this.btnClearPayTray);
            this.panel11.Controls.Add(this.lblPayTray);
            this.panel11.Location = new System.Drawing.Point(245, 73);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(328, 29);
            this.panel11.TabIndex = 245;
            // 
            // cmbPayTray
            // 
            this.cmbPayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayTray.FormattingEnabled = true;
            this.cmbPayTray.Location = new System.Drawing.Point(94, 4);
            this.cmbPayTray.Name = "cmbPayTray";
            this.cmbPayTray.Size = new System.Drawing.Size(174, 22);
            this.cmbPayTray.TabIndex = 197;
            // 
            // lblPayTray
            // 
            this.lblPayTray.AutoSize = true;
            this.lblPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayTray.Location = new System.Drawing.Point(14, 7);
            this.lblPayTray.Name = "lblPayTray";
            this.lblPayTray.Size = new System.Drawing.Size(91, 14);
            this.lblPayTray.TabIndex = 196;
            this.lblPayTray.Text = "Payment Tray :";
            // 
            // btnBrowsePayTray
            // 
            this.btnBrowsePayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayTray.BackgroundImage")));
            this.btnBrowsePayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePayTray.Image")));
            this.btnBrowsePayTray.Location = new System.Drawing.Point(272, 5);
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
            this.btnClearPayProvider.Click += new System.EventHandler(this.btnClearPayProvider_Click);
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
            this.btnBrowsePayProvider.Click += new System.EventHandler(this.btnBrowsePayProvider_Click);
            // 
            // btnClearPayTray
            // 
            this.btnClearPayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPayTray.BackgroundImage")));
            this.btnClearPayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPayTray.Image")));
            this.btnClearPayTray.Location = new System.Drawing.Point(296, 5);
            this.btnClearPayTray.Name = "btnClearPayTray";
            this.btnClearPayTray.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayTray.TabIndex = 195;
            this.btnClearPayTray.UseVisualStyleBackColor = false;
            this.btnClearPayTray.Click += new System.EventHandler(this.btnClearPayTray_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbPaymentProvider);
            this.panel3.Controls.Add(this.btnClearPayProviders);
            this.panel3.Controls.Add(this.btnBrowsePayProviders);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Location = new System.Drawing.Point(245, 108);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(328, 29);
            this.panel3.TabIndex = 253;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(41, 8);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 14);
            this.label38.TabIndex = 185;
            this.label38.Text = "Provider :";
            // 
            // cmbPaymentProvider
            // 
            this.cmbPaymentProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentProvider.FormattingEnabled = true;
            this.cmbPaymentProvider.Location = new System.Drawing.Point(94, 4);
            this.cmbPaymentProvider.Name = "cmbPaymentProvider";
            this.cmbPaymentProvider.Size = new System.Drawing.Size(174, 22);
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
            this.btnClearPayProviders.Location = new System.Drawing.Point(296, 5);
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
            this.btnBrowsePayProviders.Location = new System.Drawing.Point(272, 5);
            this.btnBrowsePayProviders.Name = "btnBrowsePayProviders";
            this.btnBrowsePayProviders.Size = new System.Drawing.Size(20, 20);
            this.btnBrowsePayProviders.TabIndex = 186;
            this.btnBrowsePayProviders.UseVisualStyleBackColor = false;
            this.btnBrowsePayProviders.Click += new System.EventHandler(this.btnBrowsePayProvider_Click);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.cmbPayFacility);
            this.panel14.Controls.Add(this.label53);
            this.panel14.Controls.Add(this.btnBrowsePayFacility);
            this.panel14.Controls.Add(this.btnClearPayFacility);
            this.panel14.Location = new System.Drawing.Point(579, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(328, 29);
            this.panel14.TabIndex = 244;
            // 
            // cmbPayFacility
            // 
            this.cmbPayFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPayFacility.FormattingEnabled = true;
            this.cmbPayFacility.Location = new System.Drawing.Point(94, 4);
            this.cmbPayFacility.Name = "cmbPayFacility";
            this.cmbPayFacility.Size = new System.Drawing.Size(174, 22);
            this.cmbPayFacility.TabIndex = 197;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(49, 8);
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
            this.btnBrowsePayFacility.Location = new System.Drawing.Point(272, 5);
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
            this.btnClearPayFacility.Location = new System.Drawing.Point(296, 5);
            this.btnClearPayFacility.Name = "btnClearPayFacility";
            this.btnClearPayFacility.Size = new System.Drawing.Size(20, 20);
            this.btnClearPayFacility.TabIndex = 195;
            this.btnClearPayFacility.UseVisualStyleBackColor = false;
            this.btnClearPayFacility.Click += new System.EventHandler(this.btnClearPayFacility_Click);
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.chkPayIncludeDetails);
            this.panel22.Location = new System.Drawing.Point(579, 38);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(299, 29);
            this.panel22.TabIndex = 254;
            // 
            // chkPayIncludeDetails
            // 
            this.chkPayIncludeDetails.AutoSize = true;
            this.chkPayIncludeDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPayIncludeDetails.Location = new System.Drawing.Point(94, 5);
            this.chkPayIncludeDetails.Name = "chkPayIncludeDetails";
            this.chkPayIncludeDetails.Size = new System.Drawing.Size(105, 18);
            this.chkPayIncludeDetails.TabIndex = 2;
            this.chkPayIncludeDetails.Text = "Include Details";
            this.chkPayIncludeDetails.UseVisualStyleBackColor = true;
            this.chkPayIncludeDetails.CheckedChanged += new System.EventHandler(this.chkPayIncludeDetails_CheckedChanged);
            // 
            // pnlPayShowHide
            // 
            this.pnlPayShowHide.Controls.Add(this.panel10);
            this.pnlPayShowHide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPayShowHide.Location = new System.Drawing.Point(3, 3);
            this.pnlPayShowHide.Name = "pnlPayShowHide";
            this.pnlPayShowHide.Size = new System.Drawing.Size(1270, 24);
            this.pnlPayShowHide.TabIndex = 268;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.btnPayDown);
            this.panel10.Controls.Add(this.btnPayUP);
            this.panel10.Controls.Add(this.label28);
            this.panel10.Controls.Add(this.label29);
            this.panel10.Controls.Add(this.label30);
            this.panel10.Controls.Add(this.label31);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1270, 24);
            this.panel10.TabIndex = 264;
            this.panel10.Tag = "Orange";
            // 
            // btnPayDown
            // 
            this.btnPayDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPayDown.FlatAppearance.BorderSize = 0;
            this.btnPayDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPayDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPayDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayDown.Location = new System.Drawing.Point(1225, 1);
            this.btnPayDown.Name = "btnPayDown";
            this.btnPayDown.Size = new System.Drawing.Size(22, 23);
            this.btnPayDown.TabIndex = 12;
            this.btnPayDown.UseVisualStyleBackColor = true;
            this.btnPayDown.MouseLeave += new System.EventHandler(this.btnPayDown_MouseLeave);
            this.btnPayDown.Click += new System.EventHandler(this.btnPayDown_Click);
            this.btnPayDown.MouseHover += new System.EventHandler(this.btnPayDown_MouseHover);
            // 
            // btnPayUP
            // 
            this.btnPayUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPayUP.FlatAppearance.BorderSize = 0;
            this.btnPayUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPayUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPayUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayUP.Location = new System.Drawing.Point(1247, 1);
            this.btnPayUP.Name = "btnPayUP";
            this.btnPayUP.Size = new System.Drawing.Size(22, 23);
            this.btnPayUP.TabIndex = 11;
            this.btnPayUP.UseVisualStyleBackColor = true;
            this.btnPayUP.MouseLeave += new System.EventHandler(this.btnPayUP_MouseLeave);
            this.btnPayUP.Click += new System.EventHandler(this.btnPayUP_Click);
            this.btnPayUP.MouseHover += new System.EventHandler(this.btnPayUP_MouseHover);
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(0, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 23);
            this.label28.TabIndex = 7;
            this.label28.Text = "label4";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label29.Location = new System.Drawing.Point(1269, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 23);
            this.label29.TabIndex = 6;
            this.label29.Text = "label3";
            // 
            // label30
            // 
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(0, 1);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1270, 23);
            this.label30.TabIndex = 9;
            this.label30.Text = " Search";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Top;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1270, 1);
            this.label31.TabIndex = 5;
            this.label31.Text = "label1";
            // 
            // tabPgClosedTray
            // 
            this.tabPgClosedTray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgClosedTray.Controls.Add(this.panel5);
            this.tabPgClosedTray.Controls.Add(this.pnlCloseDaySearch);
            this.tabPgClosedTray.Controls.Add(this.pnlCloseDayShowhide);
            this.tabPgClosedTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgClosedTray.Location = new System.Drawing.Point(4, 23);
            this.tabPgClosedTray.Name = "tabPgClosedTray";
            this.tabPgClosedTray.Size = new System.Drawing.Size(1276, 600);
            this.tabPgClosedTray.TabIndex = 2;
            this.tabPgClosedTray.Tag = "DailyClose";
            this.tabPgClosedTray.Text = "Daily Close ";
            this.tabPgClosedTray.ToolTipText = "Daily Close ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.crvRptViewCloseDay);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 119);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1276, 481);
            this.panel5.TabIndex = 271;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 480);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1274, 1);
            this.label10.TabIndex = 274;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(1, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1274, 1);
            this.label9.TabIndex = 273;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(1275, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 478);
            this.label8.TabIndex = 272;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 478);
            this.label6.TabIndex = 271;
            // 
            // crvRptViewCloseDay
            // 
            this.crvRptViewCloseDay.ActiveViewIndex = -1;
            this.crvRptViewCloseDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRptViewCloseDay.CausesValidation = false;
            this.crvRptViewCloseDay.DisplayBackgroundEdge = false;
            this.crvRptViewCloseDay.DisplayGroupTree = false;
            this.crvRptViewCloseDay.DisplayStatusBar = false;
            this.crvRptViewCloseDay.DisplayToolbar = false;
            this.crvRptViewCloseDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvRptViewCloseDay.EnableDrillDown = false;
            this.crvRptViewCloseDay.Location = new System.Drawing.Point(0, 3);
            this.crvRptViewCloseDay.Name = "crvRptViewCloseDay";
            this.crvRptViewCloseDay.ReportSource = "";
            this.crvRptViewCloseDay.SelectionFormula = "";
            this.crvRptViewCloseDay.ShowCloseButton = false;
            this.crvRptViewCloseDay.ShowGroupTreeButton = false;
            this.crvRptViewCloseDay.ShowPrintButton = false;
            this.crvRptViewCloseDay.ShowRefreshButton = false;
            this.crvRptViewCloseDay.Size = new System.Drawing.Size(1276, 478);
            this.crvRptViewCloseDay.TabIndex = 270;
            this.crvRptViewCloseDay.ViewTimeSelectionFormula = "";
            // 
            // pnlCloseDaySearch
            // 
            this.pnlCloseDaySearch.Controls.Add(this.panel17);
            this.pnlCloseDaySearch.Controls.Add(this.pnlLSTMonths);
            this.pnlCloseDaySearch.Controls.Add(this.panel15);
            this.pnlCloseDaySearch.Controls.Add(this.pnlPatients);
            this.pnlCloseDaySearch.Controls.Add(this.label42);
            this.pnlCloseDaySearch.Controls.Add(this.label43);
            this.pnlCloseDaySearch.Controls.Add(this.label44);
            this.pnlCloseDaySearch.Controls.Add(this.label45);
            this.pnlCloseDaySearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCloseDaySearch.Location = new System.Drawing.Point(0, 29);
            this.pnlCloseDaySearch.Name = "pnlCloseDaySearch";
            this.pnlCloseDaySearch.Size = new System.Drawing.Size(1276, 90);
            this.pnlCloseDaySearch.TabIndex = 269;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.chkClosedShwDetails);
            this.panel17.Controls.Add(this.checkBox1);
            this.panel17.Location = new System.Drawing.Point(4, 57);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(290, 24);
            this.panel17.TabIndex = 241;
            // 
            // chkClosedShwDetails
            // 
            this.chkClosedShwDetails.AutoSize = true;
            this.chkClosedShwDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClosedShwDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkClosedShwDetails.Location = new System.Drawing.Point(17, 3);
            this.chkClosedShwDetails.Name = "chkClosedShwDetails";
            this.chkClosedShwDetails.Size = new System.Drawing.Size(105, 18);
            this.chkClosedShwDetails.TabIndex = 2;
            this.chkClosedShwDetails.Text = "Include Details";
            this.chkClosedShwDetails.UseVisualStyleBackColor = true;
            this.chkClosedShwDetails.CheckedChanged += new System.EventHandler(this.chkClosedShwDetails_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkBox1.Location = new System.Drawing.Point(136, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 18);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Page Break on Detail";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pnlLSTMonths
            // 
            this.pnlLSTMonths.Controls.Add(this.panel16);
            this.pnlLSTMonths.Location = new System.Drawing.Point(301, 7);
            this.pnlLSTMonths.Name = "pnlLSTMonths";
            this.pnlLSTMonths.Size = new System.Drawing.Size(173, 74);
            this.pnlLSTMonths.TabIndex = 229;
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
            this.panel16.Size = new System.Drawing.Size(173, 74);
            this.panel16.TabIndex = 302;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 73);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(171, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvMonths);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(171, 50);
            this.pnlProviderBody.TabIndex = 92;
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
            treeNode13.Name = "Node0";
            treeNode13.Text = "10/10/2009";
            treeNode14.Name = "Node1";
            treeNode14.Text = "11/10/2009";
            this.trvMonths.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.trvMonths.ShowLines = false;
            this.trvMonths.ShowPlusMinus = false;
            this.trvMonths.ShowRootLines = false;
            this.trvMonths.Size = new System.Drawing.Size(171, 50);
            this.trvMonths.TabIndex = 19;
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
            this.pnlProviderHeader.Size = new System.Drawing.Size(171, 23);
            this.pnlProviderHeader.TabIndex = 91;
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
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(109, 0);
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
            this.btnSelectCreditCard.Location = new System.Drawing.Point(140, 0);
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
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(171, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 97;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreditCard.Location = new System.Drawing.Point(0, 0);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(171, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "  Day to close :";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 73);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(172, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 73);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(173, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.lblUserNm);
            this.panel15.Controls.Add(this.label52);
            this.panel15.Controls.Add(this.label54);
            this.panel15.Location = new System.Drawing.Point(5, 32);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(290, 22);
            this.panel15.TabIndex = 209;
            // 
            // lblUserNm
            // 
            this.lblUserNm.AutoSize = true;
            this.lblUserNm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUserNm.Location = new System.Drawing.Point(131, 4);
            this.lblUserNm.Name = "lblUserNm";
            this.lblUserNm.Size = new System.Drawing.Size(35, 14);
            this.lblUserNm.TabIndex = 198;
            this.lblUserNm.Text = "User ";
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
            this.label54.Location = new System.Drawing.Point(73, 4);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(41, 14);
            this.label54.TabIndex = 196;
            this.label54.Text = "User :";
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.lblLstClosedDt);
            this.pnlPatients.Controls.Add(this.label51);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Location = new System.Drawing.Point(5, 7);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(290, 22);
            this.pnlPatients.TabIndex = 208;
            // 
            // lblLstClosedDt
            // 
            this.lblLstClosedDt.AutoSize = true;
            this.lblLstClosedDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLstClosedDt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLstClosedDt.Location = new System.Drawing.Point(131, 4);
            this.lblLstClosedDt.Name = "lblLstClosedDt";
            this.lblLstClosedDt.Size = new System.Drawing.Size(20, 14);
            this.lblLstClosedDt.TabIndex = 198;
            this.lblLstClosedDt.Text = "Dt";
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
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(7, 4);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(118, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Last Closed Date :";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(1, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1274, 1);
            this.label42.TabIndex = 15;
            this.label42.Text = "label1";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(1, 89);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1274, 1);
            this.label43.TabIndex = 14;
            this.label43.Text = "label1";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(1275, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 90);
            this.label44.TabIndex = 9;
            this.label44.Text = "label4";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 90);
            this.label45.TabIndex = 8;
            this.label45.Text = "label4";
            // 
            // pnlCloseDayShowhide
            // 
            this.pnlCloseDayShowhide.Controls.Add(this.panel12);
            this.pnlCloseDayShowhide.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCloseDayShowhide.Location = new System.Drawing.Point(0, 0);
            this.pnlCloseDayShowhide.Name = "pnlCloseDayShowhide";
            this.pnlCloseDayShowhide.Size = new System.Drawing.Size(1276, 29);
            this.pnlCloseDayShowhide.TabIndex = 268;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.btnCloseDown);
            this.panel12.Controls.Add(this.btnCloseUP);
            this.panel12.Controls.Add(this.label33);
            this.panel12.Controls.Add(this.label34);
            this.panel12.Controls.Add(this.label35);
            this.panel12.Controls.Add(this.label36);
            this.panel12.Controls.Add(this.label37);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel12.Size = new System.Drawing.Size(1276, 29);
            this.panel12.TabIndex = 264;
            this.panel12.Tag = "Orange";
            // 
            // btnCloseDown
            // 
            this.btnCloseDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseDown.FlatAppearance.BorderSize = 0;
            this.btnCloseDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDown.Location = new System.Drawing.Point(1231, 4);
            this.btnCloseDown.Name = "btnCloseDown";
            this.btnCloseDown.Size = new System.Drawing.Size(22, 21);
            this.btnCloseDown.TabIndex = 12;
            this.btnCloseDown.UseVisualStyleBackColor = true;
            this.btnCloseDown.MouseLeave += new System.EventHandler(this.btnCloseDown_MouseLeave);
            this.btnCloseDown.Click += new System.EventHandler(this.btnCloseDown_Click);
            this.btnCloseDown.MouseHover += new System.EventHandler(this.btnCloseDown_MouseHover);
            // 
            // btnCloseUP
            // 
            this.btnCloseUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseUP.FlatAppearance.BorderSize = 0;
            this.btnCloseUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseUP.Location = new System.Drawing.Point(1253, 4);
            this.btnCloseUP.Name = "btnCloseUP";
            this.btnCloseUP.Size = new System.Drawing.Size(22, 21);
            this.btnCloseUP.TabIndex = 11;
            this.btnCloseUP.UseVisualStyleBackColor = true;
            this.btnCloseUP.MouseLeave += new System.EventHandler(this.btnCloseUP_MouseLeave);
            this.btnCloseUP.Click += new System.EventHandler(this.btnCloseUP_Click);
            this.btnCloseUP.MouseHover += new System.EventHandler(this.btnCloseUP_MouseHover);
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(0, 4);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 21);
            this.label33.TabIndex = 7;
            this.label33.Text = "label4";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Right;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label34.Location = new System.Drawing.Point(1275, 4);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 21);
            this.label34.TabIndex = 6;
            this.label34.Text = "label3";
            // 
            // label35
            // 
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(0, 4);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1276, 21);
            this.label35.TabIndex = 9;
            this.label35.Text = " Search";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(0, 3);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1276, 1);
            this.label36.TabIndex = 5;
            this.label36.Text = "label1";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(0, 25);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1276, 1);
            this.label37.TabIndex = 13;
            this.label37.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlSealCharges);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 682);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel2.Size = new System.Drawing.Size(1284, 26);
            this.panel2.TabIndex = 266;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.CausesValidation = false;
            this.crystalReportViewer1.DisplayBackgroundEdge = false;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.EnableDrillDown = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 121);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = "";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowPrintButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(647, 323);
            this.crystalReportViewer1.TabIndex = 269;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 27);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel6.Size = new System.Drawing.Size(647, 94);
            this.panel6.TabIndex = 268;
            this.panel6.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(645, 1);
            this.label7.TabIndex = 15;
            this.label7.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1, 90);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(645, 1);
            this.label20.TabIndex = 14;
            this.label20.Text = "label1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(646, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 88);
            this.label21.TabIndex = 9;
            this.label21.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 88);
            this.label22.TabIndex = 8;
            this.label22.Text = "label4";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(647, 24);
            this.panel7.TabIndex = 267;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.button3);
            this.panel8.Controls.Add(this.button4);
            this.panel8.Controls.Add(this.label23);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Controls.Add(this.label27);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(647, 24);
            this.panel8.TabIndex = 264;
            this.panel8.Tag = "Orange";
            this.panel8.Visible = false;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(602, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 22);
            this.button3.TabIndex = 12;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(624, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 22);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 22);
            this.label23.TabIndex = 7;
            this.label23.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(646, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 22);
            this.label24.TabIndex = 6;
            this.label24.Text = "label3";
            // 
            // label25
            // 
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(0, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(647, 22);
            this.label25.TabIndex = 9;
            this.label25.Text = " Seal Charges";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(647, 1);
            this.label26.TabIndex = 5;
            this.label26.Text = "label1";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(647, 1);
            this.label27.TabIndex = 13;
            this.label27.Text = "label1";
            // 
            // frmRpt_ChargePaymentSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 746);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlSummaryContainer);
            this.Controls.Add(this.pnlToolStrip);
            this.Name = "frmRpt_ChargePaymentSummary";
            this.Text = "Charge Payment Summary";
            this.Load += new System.EventHandler(this.frmRpt_ChargePaymentSummary_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlSummaryContainer.ResumeLayout(false);
            this.pnlSealCharges.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabPgCharges.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.flwPnlDailyCharges.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            this.pnlChrgDates.ResumeLayout(false);
            this.pnlChrgDates.PerformLayout();
            this.pnlCloseDate.ResumeLayout(false);
            this.pnlCloseDate.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlMultiFacility.ResumeLayout(false);
            this.pnlMultiFacility.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlMultiChargesTray.ResumeLayout(false);
            this.pnlMultiChargesTray.PerformLayout();
            this.pnlLoginUsers.ResumeLayout(false);
            this.pnlLoginUsers.PerformLayout();
            this.pnlRunReportAs.ResumeLayout(false);
            this.pnlRunReportAs.PerformLayout();
            this.pnlChargesShowHide.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPgPayment.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.flowPnlPayment.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.pnlTransPayDate.ResumeLayout(false);
            this.pnlTransPayDate.PerformLayout();
            this.pnlPayDates.ResumeLayout(false);
            this.pnlPayDates.PerformLayout();
            this.pnlPayCloseDate.ResumeLayout(false);
            this.pnlPayCloseDate.PerformLayout();
            this.pnlPayType.ResumeLayout(false);
            this.pnlPayType.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.pnlPayShowHide.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tabPgClosedTray.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
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
            this.pnlCloseDayShowhide.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlSummaryContainer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Panel pnlSealCharges;
        internal System.Windows.Forms.Button btnTrayDown;
        internal System.Windows.Forms.Button btnTrayUp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label pnlSummaryHeader;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabSummary;
        private System.Windows.Forms.TabPage tabPgCharges;
        private System.Windows.Forms.TabPage tabPgPayment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPgClosedTray;
        private System.Windows.Forms.Panel pnlChargesShowHide;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button btnChrgDown;
        internal System.Windows.Forms.Button btnChrgUP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvRptViewCharges;
        private System.Windows.Forms.Panel pnlPayShowHide;
        internal System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.Button btnPayDown;
        internal System.Windows.Forms.Button btnPayUP;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel pnlCloseDaySearch;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel pnlCloseDayShowhide;
        internal System.Windows.Forms.Panel panel12;
        internal System.Windows.Forms.Button btnCloseDown;
        internal System.Windows.Forms.Button btnCloseUP;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        internal System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvRptViewPayment;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvRptViewCloseDay;
        private System.Windows.Forms.Panel pnlMultiFacility;
        private System.Windows.Forms.ComboBox cmbChargesMultiFacility;
        private System.Windows.Forms.Label lblMultiFacility;
        internal System.Windows.Forms.Button btnBrowseCMultiFacility;
        internal System.Windows.Forms.Button btnClearCMultiFacility;
        private System.Windows.Forms.Panel pnlMultiChargesTray;
        private System.Windows.Forms.ComboBox cmbMultiChargesTray;
        private System.Windows.Forms.Label lblMultiChargesTray;
        internal System.Windows.Forms.Button btnBrowseCMultiChargesTray;
        internal System.Windows.Forms.Button btnClearCMultiChargesTray;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.ComboBox cmbChargesUser;
        private System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.Button btnBrowseCUser;
        internal System.Windows.Forms.Button btnClearCUser;
        private System.Windows.Forms.Panel pnlLoginUsers;
        private System.Windows.Forms.CheckBox chkIncludeAllTrays;
        private System.Windows.Forms.CheckBox chkLoginUsers;
        private System.Windows.Forms.Panel pnlRunReportAs;
        private System.Windows.Forms.CheckBox chkSealedCharges;
        private System.Windows.Forms.CheckBox chkOpenCharges;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox cmbChargesProvider;
        internal System.Windows.Forms.Button btnClearCProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_Chargesdatefilter;
        private System.Windows.Forms.Panel pnlChrgDates;
        private System.Windows.Forms.DateTimePicker dtpChrgEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpChrgStartDate;
        private System.Windows.Forms.Panel pnlPayDates;
        private System.Windows.Forms.DateTimePicker dtpPayEndDate;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.DateTimePicker dtpPayStartDate;
        private System.Windows.Forms.Panel pnlTransPayDate;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox cmb_Paydatefilter;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cmbPayProvider;
        internal System.Windows.Forms.Button btnClearPayProvider;
        internal System.Windows.Forms.Button btnBrowsePayProvider;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox cmbPayTray;
        private System.Windows.Forms.Label lblPayTray;
        internal System.Windows.Forms.Button btnBrowsePayTray;
        internal System.Windows.Forms.Button btnClearPayTray;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.ComboBox cmbPayUser;
        private System.Windows.Forms.Label lblPayUser;
        internal System.Windows.Forms.Button btnBrowsePayUser;
        internal System.Windows.Forms.Button btnClearPayUser;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ComboBox cmbPayFacility;
        private System.Windows.Forms.Label label53;
        internal System.Windows.Forms.Button btnBrowsePayFacility;
        internal System.Windows.Forms.Button btnClearPayFacility;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label51;
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
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.Button btnCloseCharges;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.RadioButton rbChrgAudit;
        private System.Windows.Forms.RadioButton rbchrgPreForClose;
        private System.Windows.Forms.Panel pnlCloseDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Panel pnlPayCloseDate;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.RadioButton rbPayAudit;
        private System.Windows.Forms.RadioButton rbPayPreForClose;
        private System.Windows.Forms.Panel pnlPayType;
        private System.Windows.Forms.ComboBox cmbPayType;
        private System.Windows.Forms.Label label56;
        internal System.Windows.Forms.Button btnBrowsePayType;
        internal System.Windows.Forms.Button btnClearPayType;
        private System.Windows.Forms.FlowLayoutPanel flowPnlPayment;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox cmbPaymentProvider;
        internal System.Windows.Forms.Button btnClearPayProviders;
        internal System.Windows.Forms.Button btnBrowsePayProviders;
        private System.Windows.Forms.FlowLayoutPanel flwPnlDailyCharges;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblLstClosedDt;
        private System.Windows.Forms.Label lblUserNm;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.CheckBox chkPayIncludeDetails;
        private System.Windows.Forms.CheckBox chkClosedShwDetails;
    }
}