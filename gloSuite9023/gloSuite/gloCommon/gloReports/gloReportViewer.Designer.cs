namespace gloReports
{
    partial class gloReportViewer
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
                    if (dtpEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);
                        }
                        catch
                        {
                        }
                        dtpEndDate.Dispose();
                        dtpEndDate = null;
                    }
                }
                catch
                {
                }


                try
                {
                    if (dtpStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);
                        }
                        catch
                        {
                        }
                        dtpStartDate.Dispose();
                        dtpStartDate = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloReportViewer));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("January");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("February");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("March");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("April");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("May");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("June");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("July");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("August");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("September");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("October");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("November");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("December");
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnGenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCPT = new System.Windows.Forms.Panel();
            this.lblCPT = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.pnlInsurance = new System.Windows.Forms.Panel();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.ndrpYear = new System.Windows.Forms.NumericUpDown();
            this.lblYear = new System.Windows.Forms.Label();
            this.drpMonth = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblFacility = new System.Windows.Forms.Label();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDiagnosisCode = new System.Windows.Forms.ComboBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.fpnlCriteria = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLSTMonths = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.pnlDgCode = new System.Windows.Forms.Panel();
            this.btnClearDiagnosisCode = new System.Windows.Forms.Button();
            this.btnBrowseDiagnosisCode = new System.Windows.Forms.Button();
            this.pnlFacility = new System.Windows.Forms.Panel();
            this.pnlMonth = new System.Windows.Forms.Panel();
            this.pnlYear = new System.Windows.Forms.Panel();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.pnlCity = new System.Windows.Forms.Panel();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.pnlState = new System.Windows.Forms.Panel();
            this.lblState = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.pnlZip = new System.Windows.Forms.Panel();
            this.lblZip = new System.Windows.Forms.Label();
            this.pnlAgingCriteria = new System.Windows.Forms.Panel();
            this.lblAgingCriteria = new System.Windows.Forms.Label();
            this.cmbAgingCritera = new System.Windows.Forms.ComboBox();
            this.pnlAppType = new System.Windows.Forms.Panel();
            this.btnClearAppointmentType = new System.Windows.Forms.Button();
            this.btnBrowseAppointmentType = new System.Windows.Forms.Button();
            this.cmbApp_AppointmentType = new System.Windows.Forms.ComboBox();
            this.lblApp_AppointmentType = new System.Windows.Forms.Label();
            this.pnlAppFlag = new System.Windows.Forms.Panel();
            this.grpSortCriteria = new System.Windows.Forms.GroupBox();
            this.rbSortNewPatient = new System.Windows.Forms.RadioButton();
            this.rbSortEstablishedPatient = new System.Windows.Forms.RadioButton();
            this.pnlCancelApp = new System.Windows.Forms.Panel();
            this.grpAppStatus = new System.Windows.Forms.GroupBox();
            this.rbDeletedAppointments = new System.Windows.Forms.RadioButton();
            this.rbNoShowAppointments = new System.Windows.Forms.RadioButton();
            this.rbCancelAppointments = new System.Windows.Forms.RadioButton();
            this.pnlAmountType = new System.Windows.Forms.Panel();
            this.grpPayType = new System.Windows.Forms.GroupBox();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbAllowed = new System.Windows.Forms.RadioButton();
            this.rbCharges = new System.Windows.Forms.RadioButton();
            this.pnlTraySelection = new System.Windows.Forms.Panel();
            this.grpTraySelection = new System.Windows.Forms.GroupBox();
            this.rbActivePayTray = new System.Windows.Forms.RadioButton();
            this.rbClosedPayTray = new System.Windows.Forms.RadioButton();
            this.pnlActivePayTray = new System.Windows.Forms.Panel();
            this.lblActivePayTray = new System.Windows.Forms.Label();
            this.cmbActivePayTray = new System.Windows.Forms.ComboBox();
            this.pnlClosedPayTray = new System.Windows.Forms.Panel();
            this.lblClosedPayTray = new System.Windows.Forms.Label();
            this.cmbClosedPayTray = new System.Windows.Forms.ComboBox();
            this.pnlMultiFacility = new System.Windows.Forms.Panel();
            this.cmbMultiFacility = new System.Windows.Forms.ComboBox();
            this.lblMultiFacility = new System.Windows.Forms.Label();
            this.btnBrowseMultiFacility = new System.Windows.Forms.Button();
            this.btnClearMultiFacility = new System.Windows.Forms.Button();
            this.pnlMultiChargesTray = new System.Windows.Forms.Panel();
            this.cmbMultiChargesTray = new System.Windows.Forms.ComboBox();
            this.lblMultiChargesTray = new System.Windows.Forms.Label();
            this.btnBrowseMultiChargesTray = new System.Windows.Forms.Button();
            this.btnClearMultiChargesTray = new System.Windows.Forms.Button();
            this.pnlMultiPayTray = new System.Windows.Forms.Panel();
            this.cmblMultiPayTray = new System.Windows.Forms.ComboBox();
            this.lbllMultiPayTray = new System.Windows.Forms.Label();
            this.btnBrowseMultiPayTray = new System.Windows.Forms.Button();
            this.btnClearMultiPayTray = new System.Windows.Forms.Button();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnBrowseUser = new System.Windows.Forms.Button();
            this.btnClearUser = new System.Windows.Forms.Button();
            this.pnlLoginUsers = new System.Windows.Forms.Panel();
            this.chkIncludeAllTrays = new System.Windows.Forms.CheckBox();
            this.chkLoginUsers = new System.Windows.Forms.CheckBox();
            this.pnlRunReportAs = new System.Windows.Forms.Panel();
            this.chkSealedCharges = new System.Windows.Forms.CheckBox();
            this.chkOpenCharges = new System.Windows.Forms.CheckBox();
            this.pnlAppointmentSort = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoLstFst = new System.Windows.Forms.RadioButton();
            this.rdoFstLst = new System.Windows.Forms.RadioButton();
            this.pnlReportType = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoWithoutInsRepCat = new System.Windows.Forms.RadioButton();
            this.rdoWithInsRepCat = new System.Windows.Forms.RadioButton();
            this.rdoWithoutName = new System.Windows.Forms.RadioButton();
            this.rdoWithName = new System.Windows.Forms.RadioButton();
            this.pnlResource = new System.Windows.Forms.Panel();
            this.cmbResouce = new System.Windows.Forms.ComboBox();
            this.lblReaource = new System.Windows.Forms.Label();
            this.btnBrwsMultiResource = new System.Windows.Forms.Button();
            this.btnClearResource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.pnelReportType = new System.Windows.Forms.Panel();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.lblbtnDown = new System.Windows.Forms.Label();
            this.pnlSummaryContainer = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlSummaryHeader = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnTrayUp = new System.Windows.Forms.Button();
            this.btnTrayDown = new System.Windows.Forms.Button();
            this.pnlSealCharges = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlCPT.SuspendLayout();
            this.pnlInsurance.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndrpYear)).BeginInit();
            this.fpnlCriteria.SuspendLayout();
            this.pnlLSTMonths.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnlDgCode.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.pnlMonth.SuspendLayout();
            this.pnlYear.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.pnlCity.SuspendLayout();
            this.pnlState.SuspendLayout();
            this.pnlZip.SuspendLayout();
            this.pnlAgingCriteria.SuspendLayout();
            this.pnlAppType.SuspendLayout();
            this.pnlAppFlag.SuspendLayout();
            this.grpSortCriteria.SuspendLayout();
            this.pnlCancelApp.SuspendLayout();
            this.grpAppStatus.SuspendLayout();
            this.pnlAmountType.SuspendLayout();
            this.grpPayType.SuspendLayout();
            this.pnlTraySelection.SuspendLayout();
            this.grpTraySelection.SuspendLayout();
            this.pnlActivePayTray.SuspendLayout();
            this.pnlClosedPayTray.SuspendLayout();
            this.pnlMultiFacility.SuspendLayout();
            this.pnlMultiChargesTray.SuspendLayout();
            this.pnlMultiPayTray.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlLoginUsers.SuspendLayout();
            this.pnlRunReportAs.SuspendLayout();
            this.pnlAppointmentSort.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlReportType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.pnelReportType.SuspendLayout();
            this.pnlSummaryContainer.SuspendLayout();
            this.pnlSealCharges.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(3, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(3894, 55);
            this.pnlToolStrip.TabIndex = 28;
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
            this.tsb_btnGenerateBatch,
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(3894, 53);
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
            this.tsb_btnExportReport.Visible = false;
            this.tsb_btnExportReport.Click += new System.EventHandler(this.tsb_btnExport);
            // 
            // tsb_btnGenerateBatch
            // 
            this.tsb_btnGenerateBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnGenerateBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnGenerateBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnGenerateBatch.Image")));
            this.tsb_btnGenerateBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnGenerateBatch.Name = "tsb_btnGenerateBatch";
            this.tsb_btnGenerateBatch.Size = new System.Drawing.Size(105, 50);
            this.tsb_btnGenerateBatch.Tag = "Generate Batch";
            this.tsb_btnGenerateBatch.Text = "Generate Batch";
            this.tsb_btnGenerateBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnGenerateBatch.Visible = false;
            this.tsb_btnGenerateBatch.Click += new System.EventHandler(this.tsb_btnGenerateBatch_Click);
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
            // pnlCPT
            // 
            this.pnlCPT.Controls.Add(this.lblCPT);
            this.pnlCPT.Controls.Add(this.cmbCPT);
            this.pnlCPT.Controls.Add(this.btnClearCPT);
            this.pnlCPT.Controls.Add(this.btnBrowseCPT);
            this.pnlCPT.Location = new System.Drawing.Point(490, 73);
            this.pnlCPT.Name = "pnlCPT";
            this.pnlCPT.Size = new System.Drawing.Size(328, 29);
            this.pnlCPT.TabIndex = 210;
            this.pnlCPT.Visible = false;
            // 
            // lblCPT
            // 
            this.lblCPT.AutoSize = true;
            this.lblCPT.Location = new System.Drawing.Point(36, 6);
            this.lblCPT.Name = "lblCPT";
            this.lblCPT.Size = new System.Drawing.Size(37, 14);
            this.lblCPT.TabIndex = 214;
            this.lblCPT.Text = "CPT :";
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(79, 3);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(189, 22);
            this.cmbCPT.TabIndex = 210;
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(300, 3);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearCPT.TabIndex = 213;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            this.btnClearCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCPT.TabIndex = 212;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            this.btnBrowseCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlInsurance
            // 
            this.pnlInsurance.Controls.Add(this.lblInsurance);
            this.pnlInsurance.Controls.Add(this.cmbInsurance);
            this.pnlInsurance.Controls.Add(this.btnClearInsurance);
            this.pnlInsurance.Controls.Add(this.btnBrowseInsurance);
            this.pnlInsurance.Location = new System.Drawing.Point(824, 3);
            this.pnlInsurance.Name = "pnlInsurance";
            this.pnlInsurance.Size = new System.Drawing.Size(328, 29);
            this.pnlInsurance.TabIndex = 209;
            this.pnlInsurance.Visible = false;
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Location = new System.Drawing.Point(8, 8);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(68, 14);
            this.lblInsurance.TabIndex = 197;
            this.lblInsurance.Text = "Insurance :";
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(79, 4);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(189, 22);
            this.cmbInsurance.TabIndex = 189;
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(300, 3);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(23, 22);
            this.btnClearInsurance.TabIndex = 192;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 191;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.label5);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Location = new System.Drawing.Point(490, 38);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(328, 29);
            this.pnlProvider.TabIndex = 208;
            this.pnlProvider.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(79, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(189, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(300, 3);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            this.btnClearProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            this.btnBrowseProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.cmbPatients);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Controls.Add(this.btnBrowsePatient);
            this.pnlPatients.Controls.Add(this.btnClearPatient);
            this.pnlPatients.Location = new System.Drawing.Point(490, 3);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(328, 29);
            this.pnlPatients.TabIndex = 207;
            this.pnlPatients.Visible = false;
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(79, 4);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(189, 22);
            this.cmbPatients.TabIndex = 197;
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(21, 8);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Patient :";
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(274, 3);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatient.TabIndex = 194;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            this.btnBrowsePatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowsePatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(300, 3);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatient.TabIndex = 195;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            this.btnClearPatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearPatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.dtpEndDate);
            this.pnlDates.Controls.Add(this.lblStartDate);
            this.pnlDates.Controls.Add(this.lblEndDate);
            this.pnlDates.Controls.Add(this.dtpStartDate);
            this.pnlDates.Location = new System.Drawing.Point(209, 35);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(275, 55);
            this.pnlDates.TabIndex = 206;
            this.pnlDates.Visible = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(118, 30);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 22);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(41, 6);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(47, 34);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(118, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(95, 22);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged_1);
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Controls.Add(this.cmb_datefilter);
            this.pnlTransDate.Location = new System.Drawing.Point(209, 3);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(275, 26);
            this.pnlTransDate.TabIndex = 205;
            this.pnlTransDate.Visible = false;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(5, 6);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(117, 2);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(151, 22);
            this.cmb_datefilter.TabIndex = 217;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // ndrpYear
            // 
            this.ndrpYear.Location = new System.Drawing.Point(79, 3);
            this.ndrpYear.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.ndrpYear.Minimum = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            this.ndrpYear.Name = "ndrpYear";
            this.ndrpYear.Size = new System.Drawing.Size(77, 22);
            this.ndrpYear.TabIndex = 191;
            this.ndrpYear.Value = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(36, 7);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(40, 14);
            this.lblYear.TabIndex = 190;
            this.lblYear.Text = "Year :";
            // 
            // drpMonth
            // 
            this.drpMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpMonth.FormattingEnabled = true;
            this.drpMonth.Location = new System.Drawing.Point(79, 3);
            this.drpMonth.Name = "drpMonth";
            this.drpMonth.Size = new System.Drawing.Size(189, 22);
            this.drpMonth.TabIndex = 237;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(26, 7);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(50, 14);
            this.lblMonth.TabIndex = 214;
            this.lblMonth.Text = "Month :";
            // 
            // lblFacility
            // 
            this.lblFacility.AutoSize = true;
            this.lblFacility.Location = new System.Drawing.Point(25, 8);
            this.lblFacility.Name = "lblFacility";
            this.lblFacility.Size = new System.Drawing.Size(50, 14);
            this.lblFacility.TabIndex = 235;
            this.lblFacility.Text = "Facility :";
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(79, 3);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(189, 22);
            this.cmbFacility.TabIndex = 236;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 14);
            this.label4.TabIndex = 181;
            this.label4.Text = "Dx Code :";
            // 
            // cmbDiagnosisCode
            // 
            this.cmbDiagnosisCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosisCode.FormattingEnabled = true;
            this.cmbDiagnosisCode.Location = new System.Drawing.Point(79, 4);
            this.cmbDiagnosisCode.Name = "cmbDiagnosisCode";
            this.cmbDiagnosisCode.Size = new System.Drawing.Size(189, 22);
            this.cmbDiagnosisCode.TabIndex = 0;
            // 
            // txtZipCode
            // 
            this.txtZipCode.Location = new System.Drawing.Point(81, 3);
            this.txtZipCode.MaxLength = 10;
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(189, 22);
            this.txtZipCode.TabIndex = 237;
            // 
            // fpnlCriteria
            // 
            this.fpnlCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpnlCriteria.Controls.Add(this.pnlLSTMonths);
            this.fpnlCriteria.Controls.Add(this.pnlTransDate);
            this.fpnlCriteria.Controls.Add(this.pnlDates);
            this.fpnlCriteria.Controls.Add(this.pnlPatients);
            this.fpnlCriteria.Controls.Add(this.pnlProvider);
            this.fpnlCriteria.Controls.Add(this.pnlCPT);
            this.fpnlCriteria.Controls.Add(this.pnlInsurance);
            this.fpnlCriteria.Controls.Add(this.pnlDgCode);
            this.fpnlCriteria.Controls.Add(this.pnlFacility);
            this.fpnlCriteria.Controls.Add(this.pnlMonth);
            this.fpnlCriteria.Controls.Add(this.pnlYear);
            this.fpnlCriteria.Controls.Add(this.pnlLocation);
            this.fpnlCriteria.Controls.Add(this.pnlCity);
            this.fpnlCriteria.Controls.Add(this.pnlState);
            this.fpnlCriteria.Controls.Add(this.pnlZip);
            this.fpnlCriteria.Controls.Add(this.pnlAgingCriteria);
            this.fpnlCriteria.Controls.Add(this.pnlAppType);
            this.fpnlCriteria.Controls.Add(this.pnlAppFlag);
            this.fpnlCriteria.Controls.Add(this.pnlCancelApp);
            this.fpnlCriteria.Controls.Add(this.pnlAmountType);
            this.fpnlCriteria.Controls.Add(this.pnlTraySelection);
            this.fpnlCriteria.Controls.Add(this.pnlActivePayTray);
            this.fpnlCriteria.Controls.Add(this.pnlClosedPayTray);
            this.fpnlCriteria.Controls.Add(this.pnlMultiFacility);
            this.fpnlCriteria.Controls.Add(this.pnlMultiChargesTray);
            this.fpnlCriteria.Controls.Add(this.pnlMultiPayTray);
            this.fpnlCriteria.Controls.Add(this.pnlUser);
            this.fpnlCriteria.Controls.Add(this.pnlLoginUsers);
            this.fpnlCriteria.Controls.Add(this.pnlRunReportAs);
            this.fpnlCriteria.Controls.Add(this.pnlAppointmentSort);
            this.fpnlCriteria.Controls.Add(this.pnlReportType);
            this.fpnlCriteria.Controls.Add(this.pnlResource);
            this.fpnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpnlCriteria.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpnlCriteria.Location = new System.Drawing.Point(3, 79);
            this.fpnlCriteria.Name = "fpnlCriteria";
            this.fpnlCriteria.Size = new System.Drawing.Size(3894, 108);
            this.fpnlCriteria.TabIndex = 255;
            // 
            // pnlLSTMonths
            // 
            this.pnlLSTMonths.Controls.Add(this.panel3);
            this.pnlLSTMonths.Location = new System.Drawing.Point(3, 3);
            this.pnlLSTMonths.Name = "pnlLSTMonths";
            this.pnlLSTMonths.Size = new System.Drawing.Size(200, 100);
            this.pnlLSTMonths.TabIndex = 228;
            this.pnlLSTMonths.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.panel3.Controls.Add(this.pnlProviderBody);
            this.panel3.Controls.Add(this.pnlProviderHeader);
            this.panel3.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.panel3.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.panel3.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 302;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 99);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(198, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvMonths);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(198, 76);
            this.pnlProviderBody.TabIndex = 92;
            // 
            // trvMonths
            // 
            this.trvMonths.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMonths.CheckBoxes = true;
            this.trvMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMonths.ForeColor = System.Drawing.Color.Black;
            this.trvMonths.Location = new System.Drawing.Point(0, 0);
            this.trvMonths.Name = "trvMonths";
            treeNode1.Name = "January";
            treeNode1.Tag = "1";
            treeNode1.Text = "January";
            treeNode2.Name = "February";
            treeNode2.Tag = "2";
            treeNode2.Text = "February";
            treeNode3.Name = "March";
            treeNode3.Tag = "3";
            treeNode3.Text = "March";
            treeNode4.Name = "April";
            treeNode4.Tag = "4";
            treeNode4.Text = "April";
            treeNode5.Name = "May";
            treeNode5.Tag = "5";
            treeNode5.Text = "May";
            treeNode6.Name = "June";
            treeNode6.Tag = "6";
            treeNode6.Text = "June";
            treeNode7.Name = "July";
            treeNode7.Tag = "7";
            treeNode7.Text = "July";
            treeNode8.Name = "August";
            treeNode8.Tag = "8";
            treeNode8.Text = "August";
            treeNode9.Name = "September";
            treeNode9.Tag = "9";
            treeNode9.Text = "September";
            treeNode10.Name = "October";
            treeNode10.Tag = "10";
            treeNode10.Text = "October";
            treeNode11.Name = "November";
            treeNode11.Tag = "11";
            treeNode11.Text = "November";
            treeNode12.Name = "December";
            treeNode12.Tag = "12";
            treeNode12.Text = "December";
            this.trvMonths.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            this.trvMonths.ShowLines = false;
            this.trvMonths.ShowPlusMinus = false;
            this.trvMonths.ShowRootLines = false;
            this.trvMonths.Size = new System.Drawing.Size(198, 76);
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
            this.pnlProviderHeader.Size = new System.Drawing.Size(198, 23);
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
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(136, 0);
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
            this.btnSelectCreditCard.Location = new System.Drawing.Point(167, 0);
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
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(198, 1);
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
            this.lblCreditCard.Size = new System.Drawing.Size(198, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "  Months :";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 99);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(199, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 99);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(200, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // pnlDgCode
            // 
            this.pnlDgCode.Controls.Add(this.label4);
            this.pnlDgCode.Controls.Add(this.cmbDiagnosisCode);
            this.pnlDgCode.Controls.Add(this.btnClearDiagnosisCode);
            this.pnlDgCode.Controls.Add(this.btnBrowseDiagnosisCode);
            this.pnlDgCode.Location = new System.Drawing.Point(824, 38);
            this.pnlDgCode.Name = "pnlDgCode";
            this.pnlDgCode.Size = new System.Drawing.Size(328, 29);
            this.pnlDgCode.TabIndex = 211;
            this.pnlDgCode.Visible = false;
            // 
            // btnClearDiagnosisCode
            // 
            this.btnClearDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.BackgroundImage")));
            this.btnClearDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.Image")));
            this.btnClearDiagnosisCode.Location = new System.Drawing.Point(300, 3);
            this.btnClearDiagnosisCode.Name = "btnClearDiagnosisCode";
            this.btnClearDiagnosisCode.Size = new System.Drawing.Size(22, 22);
            this.btnClearDiagnosisCode.TabIndex = 183;
            this.btnClearDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnClearDiagnosisCode.Click += new System.EventHandler(this.btnClearDiagnosisCode_Click);
            this.btnClearDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseDiagnosisCode
            // 
            this.btnBrowseDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.BackgroundImage")));
            this.btnBrowseDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.Image")));
            this.btnBrowseDiagnosisCode.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseDiagnosisCode.Name = "btnBrowseDiagnosisCode";
            this.btnBrowseDiagnosisCode.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDiagnosisCode.TabIndex = 182;
            this.btnBrowseDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnBrowseDiagnosisCode.Click += new System.EventHandler(this.btnBrowseDiagnosisCode_Click);
            this.btnBrowseDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlFacility
            // 
            this.pnlFacility.Controls.Add(this.lblFacility);
            this.pnlFacility.Controls.Add(this.cmbFacility);
            this.pnlFacility.Location = new System.Drawing.Point(824, 73);
            this.pnlFacility.Name = "pnlFacility";
            this.pnlFacility.Size = new System.Drawing.Size(328, 29);
            this.pnlFacility.TabIndex = 212;
            this.pnlFacility.Visible = false;
            // 
            // pnlMonth
            // 
            this.pnlMonth.Controls.Add(this.drpMonth);
            this.pnlMonth.Controls.Add(this.lblMonth);
            this.pnlMonth.Location = new System.Drawing.Point(1158, 3);
            this.pnlMonth.Name = "pnlMonth";
            this.pnlMonth.Size = new System.Drawing.Size(328, 29);
            this.pnlMonth.TabIndex = 213;
            this.pnlMonth.Visible = false;
            // 
            // pnlYear
            // 
            this.pnlYear.Controls.Add(this.ndrpYear);
            this.pnlYear.Controls.Add(this.lblYear);
            this.pnlYear.Location = new System.Drawing.Point(1158, 38);
            this.pnlYear.Name = "pnlYear";
            this.pnlYear.Size = new System.Drawing.Size(328, 29);
            this.pnlYear.TabIndex = 214;
            this.pnlYear.Visible = false;
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.label8);
            this.pnlLocation.Controls.Add(this.cmbLocation);
            this.pnlLocation.Location = new System.Drawing.Point(1158, 73);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(328, 29);
            this.pnlLocation.TabIndex = 221;
            this.pnlLocation.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "Location : ";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(81, 3);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(189, 22);
            this.cmbLocation.TabIndex = 13;
            // 
            // pnlCity
            // 
            this.pnlCity.Controls.Add(this.lblCity);
            this.pnlCity.Controls.Add(this.txtCity);
            this.pnlCity.Location = new System.Drawing.Point(1492, 3);
            this.pnlCity.Name = "pnlCity";
            this.pnlCity.Size = new System.Drawing.Size(328, 29);
            this.pnlCity.TabIndex = 216;
            this.pnlCity.Visible = false;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(39, 8);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 14);
            this.lblCity.TabIndex = 241;
            this.lblCity.Text = "City :";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(81, 3);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(189, 22);
            this.txtCity.TabIndex = 238;
            // 
            // pnlState
            // 
            this.pnlState.Controls.Add(this.lblState);
            this.pnlState.Controls.Add(this.txtState);
            this.pnlState.Location = new System.Drawing.Point(1492, 38);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(328, 29);
            this.pnlState.TabIndex = 215;
            this.pnlState.Visible = false;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(30, 8);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 14);
            this.lblState.TabIndex = 240;
            this.lblState.Text = "State :";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(81, 3);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(189, 22);
            this.txtState.TabIndex = 239;
            // 
            // pnlZip
            // 
            this.pnlZip.Controls.Add(this.lblZip);
            this.pnlZip.Controls.Add(this.txtZipCode);
            this.pnlZip.Location = new System.Drawing.Point(1492, 73);
            this.pnlZip.Name = "pnlZip";
            this.pnlZip.Size = new System.Drawing.Size(328, 29);
            this.pnlZip.TabIndex = 217;
            this.pnlZip.Visible = false;
            // 
            // lblZip
            // 
            this.lblZip.AutoSize = true;
            this.lblZip.Location = new System.Drawing.Point(44, 8);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(31, 14);
            this.lblZip.TabIndex = 242;
            this.lblZip.Text = "Zip :";
            // 
            // pnlAgingCriteria
            // 
            this.pnlAgingCriteria.Controls.Add(this.lblAgingCriteria);
            this.pnlAgingCriteria.Controls.Add(this.cmbAgingCritera);
            this.pnlAgingCriteria.Location = new System.Drawing.Point(1826, 3);
            this.pnlAgingCriteria.Name = "pnlAgingCriteria";
            this.pnlAgingCriteria.Size = new System.Drawing.Size(328, 29);
            this.pnlAgingCriteria.TabIndex = 218;
            this.pnlAgingCriteria.Visible = false;
            // 
            // lblAgingCriteria
            // 
            this.lblAgingCriteria.AutoSize = true;
            this.lblAgingCriteria.Location = new System.Drawing.Point(3, 7);
            this.lblAgingCriteria.Name = "lblAgingCriteria";
            this.lblAgingCriteria.Size = new System.Drawing.Size(87, 14);
            this.lblAgingCriteria.TabIndex = 235;
            this.lblAgingCriteria.Text = "Aging Criteria :";
            // 
            // cmbAgingCritera
            // 
            this.cmbAgingCritera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgingCritera.FormattingEnabled = true;
            this.cmbAgingCritera.Location = new System.Drawing.Point(96, 3);
            this.cmbAgingCritera.Name = "cmbAgingCritera";
            this.cmbAgingCritera.Size = new System.Drawing.Size(229, 22);
            this.cmbAgingCritera.TabIndex = 236;
            this.cmbAgingCritera.SelectedIndexChanged += new System.EventHandler(this.cmbAgingCritera_SelectedIndexChanged);
            // 
            // pnlAppType
            // 
            this.pnlAppType.Controls.Add(this.btnClearAppointmentType);
            this.pnlAppType.Controls.Add(this.btnBrowseAppointmentType);
            this.pnlAppType.Controls.Add(this.cmbApp_AppointmentType);
            this.pnlAppType.Controls.Add(this.lblApp_AppointmentType);
            this.pnlAppType.Location = new System.Drawing.Point(1826, 38);
            this.pnlAppType.Name = "pnlAppType";
            this.pnlAppType.Size = new System.Drawing.Size(328, 29);
            this.pnlAppType.TabIndex = 219;
            this.pnlAppType.Visible = false;
            // 
            // btnClearAppointmentType
            // 
            this.btnClearAppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAppointmentType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAppointmentType.BackgroundImage")));
            this.btnClearAppointmentType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAppointmentType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAppointmentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAppointmentType.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAppointmentType.Image")));
            this.btnClearAppointmentType.Location = new System.Drawing.Point(300, 3);
            this.btnClearAppointmentType.Name = "btnClearAppointmentType";
            this.btnClearAppointmentType.Size = new System.Drawing.Size(22, 22);
            this.btnClearAppointmentType.TabIndex = 216;
            this.btnClearAppointmentType.UseVisualStyleBackColor = false;
            this.btnClearAppointmentType.Click += new System.EventHandler(this.btnClearAppointmentType_Click);
            this.btnClearAppointmentType.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearAppointmentType.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseAppointmentType
            // 
            this.btnBrowseAppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseAppointmentType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppointmentType.BackgroundImage")));
            this.btnBrowseAppointmentType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseAppointmentType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseAppointmentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAppointmentType.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppointmentType.Image")));
            this.btnBrowseAppointmentType.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseAppointmentType.Name = "btnBrowseAppointmentType";
            this.btnBrowseAppointmentType.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseAppointmentType.TabIndex = 215;
            this.btnBrowseAppointmentType.UseVisualStyleBackColor = false;
            this.btnBrowseAppointmentType.Click += new System.EventHandler(this.btnBrowseAppointmentType_Click);
            this.btnBrowseAppointmentType.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseAppointmentType.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbApp_AppointmentType
            // 
            this.cmbApp_AppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_AppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_AppointmentType.FormattingEnabled = true;
            this.cmbApp_AppointmentType.Location = new System.Drawing.Point(79, 3);
            this.cmbApp_AppointmentType.Name = "cmbApp_AppointmentType";
            this.cmbApp_AppointmentType.Size = new System.Drawing.Size(189, 22);
            this.cmbApp_AppointmentType.TabIndex = 214;
            // 
            // lblApp_AppointmentType
            // 
            this.lblApp_AppointmentType.AutoSize = true;
            this.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_AppointmentType.Location = new System.Drawing.Point(0, 7);
            this.lblApp_AppointmentType.Name = "lblApp_AppointmentType";
            this.lblApp_AppointmentType.Size = new System.Drawing.Size(78, 14);
            this.lblApp_AppointmentType.TabIndex = 213;
            this.lblApp_AppointmentType.Text = " Appt Type :";
            this.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pnlAppFlag
            // 
            this.pnlAppFlag.Controls.Add(this.grpSortCriteria);
            this.pnlAppFlag.Location = new System.Drawing.Point(1826, 73);
            this.pnlAppFlag.Name = "pnlAppFlag";
            this.pnlAppFlag.Size = new System.Drawing.Size(328, 29);
            this.pnlAppFlag.TabIndex = 225;
            this.pnlAppFlag.Visible = false;
            // 
            // grpSortCriteria
            // 
            this.grpSortCriteria.Controls.Add(this.rbSortNewPatient);
            this.grpSortCriteria.Controls.Add(this.rbSortEstablishedPatient);
            this.grpSortCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpSortCriteria.Location = new System.Drawing.Point(4, -6);
            this.grpSortCriteria.Name = "grpSortCriteria";
            this.grpSortCriteria.Padding = new System.Windows.Forms.Padding(0);
            this.grpSortCriteria.Size = new System.Drawing.Size(291, 32);
            this.grpSortCriteria.TabIndex = 201;
            this.grpSortCriteria.TabStop = false;
            // 
            // rbSortNewPatient
            // 
            this.rbSortNewPatient.AutoSize = true;
            this.rbSortNewPatient.Location = new System.Drawing.Point(172, 9);
            this.rbSortNewPatient.Name = "rbSortNewPatient";
            this.rbSortNewPatient.Size = new System.Drawing.Size(83, 17);
            this.rbSortNewPatient.TabIndex = 2;
            this.rbSortNewPatient.Text = "New Patient";
            this.rbSortNewPatient.UseVisualStyleBackColor = true;
            this.rbSortNewPatient.CheckedChanged += new System.EventHandler(this.rbSortNewPatient_CheckedChanged);
            // 
            // rbSortEstablishedPatient
            // 
            this.rbSortEstablishedPatient.AutoSize = true;
            this.rbSortEstablishedPatient.Checked = true;
            this.rbSortEstablishedPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSortEstablishedPatient.Location = new System.Drawing.Point(13, 9);
            this.rbSortEstablishedPatient.Name = "rbSortEstablishedPatient";
            this.rbSortEstablishedPatient.Size = new System.Drawing.Size(143, 18);
            this.rbSortEstablishedPatient.TabIndex = 0;
            this.rbSortEstablishedPatient.TabStop = true;
            this.rbSortEstablishedPatient.Text = "Established Patient";
            this.rbSortEstablishedPatient.UseVisualStyleBackColor = true;
            this.rbSortEstablishedPatient.CheckedChanged += new System.EventHandler(this.rbSortEstablishedPatient_CheckedChanged);
            // 
            // pnlCancelApp
            // 
            this.pnlCancelApp.Controls.Add(this.grpAppStatus);
            this.pnlCancelApp.Location = new System.Drawing.Point(2160, 3);
            this.pnlCancelApp.Name = "pnlCancelApp";
            this.pnlCancelApp.Size = new System.Drawing.Size(304, 29);
            this.pnlCancelApp.TabIndex = 226;
            this.pnlCancelApp.Visible = false;
            // 
            // grpAppStatus
            // 
            this.grpAppStatus.Controls.Add(this.rbDeletedAppointments);
            this.grpAppStatus.Controls.Add(this.rbNoShowAppointments);
            this.grpAppStatus.Controls.Add(this.rbCancelAppointments);
            this.grpAppStatus.Location = new System.Drawing.Point(4, -6);
            this.grpAppStatus.Name = "grpAppStatus";
            this.grpAppStatus.Size = new System.Drawing.Size(291, 32);
            this.grpAppStatus.TabIndex = 2;
            this.grpAppStatus.TabStop = false;
            // 
            // rbDeletedAppointments
            // 
            this.rbDeletedAppointments.AutoSize = true;
            this.rbDeletedAppointments.Location = new System.Drawing.Point(195, 9);
            this.rbDeletedAppointments.Name = "rbDeletedAppointments";
            this.rbDeletedAppointments.Size = new System.Drawing.Size(62, 17);
            this.rbDeletedAppointments.TabIndex = 1;
            this.rbDeletedAppointments.Text = "Deleted";
            this.rbDeletedAppointments.UseVisualStyleBackColor = true;
            this.rbDeletedAppointments.CheckedChanged += new System.EventHandler(this.rbDeletedAppointments_CheckedChanged);
            // 
            // rbNoShowAppointments
            // 
            this.rbNoShowAppointments.AutoSize = true;
            this.rbNoShowAppointments.Location = new System.Drawing.Point(102, 9);
            this.rbNoShowAppointments.Name = "rbNoShowAppointments";
            this.rbNoShowAppointments.Size = new System.Drawing.Size(69, 17);
            this.rbNoShowAppointments.TabIndex = 1;
            this.rbNoShowAppointments.Text = "No Show";
            this.rbNoShowAppointments.UseVisualStyleBackColor = true;
            this.rbNoShowAppointments.CheckedChanged += new System.EventHandler(this.rbNoShowAppointments_CheckedChanged);
            // 
            // rbCancelAppointments
            // 
            this.rbCancelAppointments.AutoSize = true;
            this.rbCancelAppointments.Checked = true;
            this.rbCancelAppointments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCancelAppointments.Location = new System.Drawing.Point(13, 9);
            this.rbCancelAppointments.Name = "rbCancelAppointments";
            this.rbCancelAppointments.Size = new System.Drawing.Size(64, 18);
            this.rbCancelAppointments.TabIndex = 0;
            this.rbCancelAppointments.TabStop = true;
            this.rbCancelAppointments.Text = "Cancel";
            this.rbCancelAppointments.UseVisualStyleBackColor = true;
            this.rbCancelAppointments.CheckedChanged += new System.EventHandler(this.rbCancelAppointments_CheckedChanged);
            // 
            // pnlAmountType
            // 
            this.pnlAmountType.Controls.Add(this.grpPayType);
            this.pnlAmountType.Location = new System.Drawing.Point(2160, 38);
            this.pnlAmountType.Name = "pnlAmountType";
            this.pnlAmountType.Size = new System.Drawing.Size(304, 29);
            this.pnlAmountType.TabIndex = 227;
            this.pnlAmountType.Visible = false;
            // 
            // grpPayType
            // 
            this.grpPayType.Controls.Add(this.rbBoth);
            this.grpPayType.Controls.Add(this.rbAllowed);
            this.grpPayType.Controls.Add(this.rbCharges);
            this.grpPayType.Location = new System.Drawing.Point(4, -6);
            this.grpPayType.Name = "grpPayType";
            this.grpPayType.Size = new System.Drawing.Size(291, 32);
            this.grpPayType.TabIndex = 2;
            this.grpPayType.TabStop = false;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Checked = true;
            this.rbBoth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBoth.Location = new System.Drawing.Point(197, 11);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(55, 18);
            this.rbBoth.TabIndex = 1;
            this.rbBoth.TabStop = true;
            this.rbBoth.Text = "Both";
            this.rbBoth.UseVisualStyleBackColor = true;
            this.rbBoth.Visible = false;
            this.rbBoth.CheckedChanged += new System.EventHandler(this.rbBoth_CheckedChanged);
            // 
            // rbAllowed
            // 
            this.rbAllowed.AutoSize = true;
            this.rbAllowed.Location = new System.Drawing.Point(104, 11);
            this.rbAllowed.Name = "rbAllowed";
            this.rbAllowed.Size = new System.Drawing.Size(62, 17);
            this.rbAllowed.TabIndex = 1;
            this.rbAllowed.Text = "Allowed";
            this.rbAllowed.UseVisualStyleBackColor = true;
            this.rbAllowed.CheckedChanged += new System.EventHandler(this.rbAllowed_CheckedChanged);
            // 
            // rbCharges
            // 
            this.rbCharges.AutoSize = true;
            this.rbCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCharges.Location = new System.Drawing.Point(13, 11);
            this.rbCharges.Name = "rbCharges";
            this.rbCharges.Size = new System.Drawing.Size(68, 18);
            this.rbCharges.TabIndex = 0;
            this.rbCharges.Text = "Charges";
            this.rbCharges.UseVisualStyleBackColor = true;
            this.rbCharges.CheckedChanged += new System.EventHandler(this.rbCharges_CheckedChanged);
            // 
            // pnlTraySelection
            // 
            this.pnlTraySelection.Controls.Add(this.grpTraySelection);
            this.pnlTraySelection.Location = new System.Drawing.Point(2160, 73);
            this.pnlTraySelection.Name = "pnlTraySelection";
            this.pnlTraySelection.Size = new System.Drawing.Size(332, 29);
            this.pnlTraySelection.TabIndex = 231;
            this.pnlTraySelection.Visible = false;
            // 
            // grpTraySelection
            // 
            this.grpTraySelection.Controls.Add(this.rbActivePayTray);
            this.grpTraySelection.Controls.Add(this.rbClosedPayTray);
            this.grpTraySelection.Location = new System.Drawing.Point(4, -6);
            this.grpTraySelection.Name = "grpTraySelection";
            this.grpTraySelection.Size = new System.Drawing.Size(325, 32);
            this.grpTraySelection.TabIndex = 2;
            this.grpTraySelection.TabStop = false;
            // 
            // rbActivePayTray
            // 
            this.rbActivePayTray.AutoSize = true;
            this.rbActivePayTray.Checked = true;
            this.rbActivePayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActivePayTray.Location = new System.Drawing.Point(13, 9);
            this.rbActivePayTray.Name = "rbActivePayTray";
            this.rbActivePayTray.Size = new System.Drawing.Size(151, 18);
            this.rbActivePayTray.TabIndex = 0;
            this.rbActivePayTray.TabStop = true;
            this.rbActivePayTray.Text = "Active Payment Tray";
            this.rbActivePayTray.UseVisualStyleBackColor = true;
            this.rbActivePayTray.CheckedChanged += new System.EventHandler(this.rbActivePayTray_CheckedChanged);
            // 
            // rbClosedPayTray
            // 
            this.rbClosedPayTray.AutoSize = true;
            this.rbClosedPayTray.Location = new System.Drawing.Point(179, 9);
            this.rbClosedPayTray.Name = "rbClosedPayTray";
            this.rbClosedPayTray.Size = new System.Drawing.Size(125, 17);
            this.rbClosedPayTray.TabIndex = 1;
            this.rbClosedPayTray.Text = "Closed Payment Tray";
            this.rbClosedPayTray.UseVisualStyleBackColor = true;
            this.rbClosedPayTray.CheckedChanged += new System.EventHandler(this.rbClosedPayTray_CheckedChanged);
            // 
            // pnlActivePayTray
            // 
            this.pnlActivePayTray.Controls.Add(this.lblActivePayTray);
            this.pnlActivePayTray.Controls.Add(this.cmbActivePayTray);
            this.pnlActivePayTray.Location = new System.Drawing.Point(2498, 3);
            this.pnlActivePayTray.Name = "pnlActivePayTray";
            this.pnlActivePayTray.Size = new System.Drawing.Size(328, 29);
            this.pnlActivePayTray.TabIndex = 229;
            this.pnlActivePayTray.Visible = false;
            // 
            // lblActivePayTray
            // 
            this.lblActivePayTray.AutoSize = true;
            this.lblActivePayTray.Location = new System.Drawing.Point(3, 8);
            this.lblActivePayTray.Name = "lblActivePayTray";
            this.lblActivePayTray.Size = new System.Drawing.Size(133, 14);
            this.lblActivePayTray.TabIndex = 14;
            this.lblActivePayTray.Text = "Active Payment Tray : ";
            // 
            // cmbActivePayTray
            // 
            this.cmbActivePayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivePayTray.FormattingEnabled = true;
            this.cmbActivePayTray.Location = new System.Drawing.Point(139, 2);
            this.cmbActivePayTray.Name = "cmbActivePayTray";
            this.cmbActivePayTray.Size = new System.Drawing.Size(189, 22);
            this.cmbActivePayTray.TabIndex = 13;
            // 
            // pnlClosedPayTray
            // 
            this.pnlClosedPayTray.Controls.Add(this.lblClosedPayTray);
            this.pnlClosedPayTray.Controls.Add(this.cmbClosedPayTray);
            this.pnlClosedPayTray.Location = new System.Drawing.Point(2498, 38);
            this.pnlClosedPayTray.Name = "pnlClosedPayTray";
            this.pnlClosedPayTray.Size = new System.Drawing.Size(328, 29);
            this.pnlClosedPayTray.TabIndex = 230;
            this.pnlClosedPayTray.Visible = false;
            // 
            // lblClosedPayTray
            // 
            this.lblClosedPayTray.AutoSize = true;
            this.lblClosedPayTray.Location = new System.Drawing.Point(3, 7);
            this.lblClosedPayTray.Name = "lblClosedPayTray";
            this.lblClosedPayTray.Size = new System.Drawing.Size(134, 14);
            this.lblClosedPayTray.TabIndex = 14;
            this.lblClosedPayTray.Text = "Closed Payment Tray : ";
            // 
            // cmbClosedPayTray
            // 
            this.cmbClosedPayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClosedPayTray.FormattingEnabled = true;
            this.cmbClosedPayTray.Location = new System.Drawing.Point(140, 3);
            this.cmbClosedPayTray.Name = "cmbClosedPayTray";
            this.cmbClosedPayTray.Size = new System.Drawing.Size(189, 22);
            this.cmbClosedPayTray.TabIndex = 13;
            // 
            // pnlMultiFacility
            // 
            this.pnlMultiFacility.Controls.Add(this.cmbMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.lblMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnBrowseMultiFacility);
            this.pnlMultiFacility.Controls.Add(this.btnClearMultiFacility);
            this.pnlMultiFacility.Location = new System.Drawing.Point(2498, 73);
            this.pnlMultiFacility.Name = "pnlMultiFacility";
            this.pnlMultiFacility.Size = new System.Drawing.Size(328, 29);
            this.pnlMultiFacility.TabIndex = 232;
            this.pnlMultiFacility.Visible = false;
            // 
            // cmbMultiFacility
            // 
            this.cmbMultiFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiFacility.FormattingEnabled = true;
            this.cmbMultiFacility.Location = new System.Drawing.Point(79, 2);
            this.cmbMultiFacility.Name = "cmbMultiFacility";
            this.cmbMultiFacility.Size = new System.Drawing.Size(189, 22);
            this.cmbMultiFacility.TabIndex = 197;
            // 
            // lblMultiFacility
            // 
            this.lblMultiFacility.AutoSize = true;
            this.lblMultiFacility.Location = new System.Drawing.Point(23, 6);
            this.lblMultiFacility.Name = "lblMultiFacility";
            this.lblMultiFacility.Size = new System.Drawing.Size(50, 14);
            this.lblMultiFacility.TabIndex = 196;
            this.lblMultiFacility.Text = "Facility :";
            // 
            // btnBrowseMultiFacility
            // 
            this.btnBrowseMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiFacility.BackgroundImage")));
            this.btnBrowseMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiFacility.Image")));
            this.btnBrowseMultiFacility.Location = new System.Drawing.Point(274, 2);
            this.btnBrowseMultiFacility.Name = "btnBrowseMultiFacility";
            this.btnBrowseMultiFacility.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiFacility.TabIndex = 194;
            this.btnBrowseMultiFacility.UseVisualStyleBackColor = false;
            this.btnBrowseMultiFacility.Click += new System.EventHandler(this.btnBrowseMultiFacility_Click);
            // 
            // btnClearMultiFacility
            // 
            this.btnClearMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMultiFacility.BackgroundImage")));
            this.btnClearMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMultiFacility.Image")));
            this.btnClearMultiFacility.Location = new System.Drawing.Point(300, 2);
            this.btnClearMultiFacility.Name = "btnClearMultiFacility";
            this.btnClearMultiFacility.Size = new System.Drawing.Size(22, 22);
            this.btnClearMultiFacility.TabIndex = 195;
            this.btnClearMultiFacility.UseVisualStyleBackColor = false;
            this.btnClearMultiFacility.Click += new System.EventHandler(this.btnClearMultiFacility_Click);
            // 
            // pnlMultiChargesTray
            // 
            this.pnlMultiChargesTray.Controls.Add(this.cmbMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.lblMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnBrowseMultiChargesTray);
            this.pnlMultiChargesTray.Controls.Add(this.btnClearMultiChargesTray);
            this.pnlMultiChargesTray.Location = new System.Drawing.Point(2832, 3);
            this.pnlMultiChargesTray.Name = "pnlMultiChargesTray";
            this.pnlMultiChargesTray.Size = new System.Drawing.Size(328, 29);
            this.pnlMultiChargesTray.TabIndex = 233;
            this.pnlMultiChargesTray.Visible = false;
            // 
            // cmbMultiChargesTray
            // 
            this.cmbMultiChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiChargesTray.FormattingEnabled = true;
            this.cmbMultiChargesTray.Location = new System.Drawing.Point(79, 4);
            this.cmbMultiChargesTray.Name = "cmbMultiChargesTray";
            this.cmbMultiChargesTray.Size = new System.Drawing.Size(189, 22);
            this.cmbMultiChargesTray.TabIndex = 197;
            // 
            // lblMultiChargesTray
            // 
            this.lblMultiChargesTray.AutoSize = true;
            this.lblMultiChargesTray.Location = new System.Drawing.Point(-3, 8);
            this.lblMultiChargesTray.Name = "lblMultiChargesTray";
            this.lblMultiChargesTray.Size = new System.Drawing.Size(86, 14);
            this.lblMultiChargesTray.TabIndex = 196;
            this.lblMultiChargesTray.Text = "Charges Tray :";
            // 
            // btnBrowseMultiChargesTray
            // 
            this.btnBrowseMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiChargesTray.BackgroundImage")));
            this.btnBrowseMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiChargesTray.Image")));
            this.btnBrowseMultiChargesTray.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseMultiChargesTray.Name = "btnBrowseMultiChargesTray";
            this.btnBrowseMultiChargesTray.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiChargesTray.TabIndex = 194;
            this.btnBrowseMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnBrowseMultiChargesTray.Click += new System.EventHandler(this.btnBrowseMultiChargesTray_Click);
            // 
            // btnClearMultiChargesTray
            // 
            this.btnClearMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMultiChargesTray.BackgroundImage")));
            this.btnClearMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMultiChargesTray.Image")));
            this.btnClearMultiChargesTray.Location = new System.Drawing.Point(300, 3);
            this.btnClearMultiChargesTray.Name = "btnClearMultiChargesTray";
            this.btnClearMultiChargesTray.Size = new System.Drawing.Size(22, 22);
            this.btnClearMultiChargesTray.TabIndex = 195;
            this.btnClearMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnClearMultiChargesTray.Click += new System.EventHandler(this.btnClearMultiChargesTray_Click);
            // 
            // pnlMultiPayTray
            // 
            this.pnlMultiPayTray.Controls.Add(this.cmblMultiPayTray);
            this.pnlMultiPayTray.Controls.Add(this.lbllMultiPayTray);
            this.pnlMultiPayTray.Controls.Add(this.btnBrowseMultiPayTray);
            this.pnlMultiPayTray.Controls.Add(this.btnClearMultiPayTray);
            this.pnlMultiPayTray.Location = new System.Drawing.Point(2832, 38);
            this.pnlMultiPayTray.Name = "pnlMultiPayTray";
            this.pnlMultiPayTray.Size = new System.Drawing.Size(328, 29);
            this.pnlMultiPayTray.TabIndex = 234;
            this.pnlMultiPayTray.Visible = false;
            // 
            // cmblMultiPayTray
            // 
            this.cmblMultiPayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmblMultiPayTray.FormattingEnabled = true;
            this.cmblMultiPayTray.Location = new System.Drawing.Point(79, 4);
            this.cmblMultiPayTray.Name = "cmblMultiPayTray";
            this.cmblMultiPayTray.Size = new System.Drawing.Size(189, 22);
            this.cmblMultiPayTray.TabIndex = 197;
            // 
            // lbllMultiPayTray
            // 
            this.lbllMultiPayTray.AutoSize = true;
            this.lbllMultiPayTray.Location = new System.Drawing.Point(-3, 8);
            this.lbllMultiPayTray.Name = "lbllMultiPayTray";
            this.lbllMultiPayTray.Size = new System.Drawing.Size(91, 14);
            this.lbllMultiPayTray.TabIndex = 196;
            this.lbllMultiPayTray.Text = "Payment Tray :";
            // 
            // btnBrowseMultiPayTray
            // 
            this.btnBrowseMultiPayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiPayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiPayTray.BackgroundImage")));
            this.btnBrowseMultiPayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiPayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiPayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiPayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiPayTray.Image")));
            this.btnBrowseMultiPayTray.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseMultiPayTray.Name = "btnBrowseMultiPayTray";
            this.btnBrowseMultiPayTray.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiPayTray.TabIndex = 194;
            this.btnBrowseMultiPayTray.UseVisualStyleBackColor = false;
            this.btnBrowseMultiPayTray.Click += new System.EventHandler(this.btnBrowseMultiPayTray_Click);
            // 
            // btnClearMultiPayTray
            // 
            this.btnClearMultiPayTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMultiPayTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMultiPayTray.BackgroundImage")));
            this.btnClearMultiPayTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMultiPayTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMultiPayTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMultiPayTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMultiPayTray.Image")));
            this.btnClearMultiPayTray.Location = new System.Drawing.Point(300, 3);
            this.btnClearMultiPayTray.Name = "btnClearMultiPayTray";
            this.btnClearMultiPayTray.Size = new System.Drawing.Size(22, 22);
            this.btnClearMultiPayTray.TabIndex = 195;
            this.btnClearMultiPayTray.UseVisualStyleBackColor = false;
            this.btnClearMultiPayTray.Click += new System.EventHandler(this.btnClearMultiPayTray_Click);
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.cmbUser);
            this.pnlUser.Controls.Add(this.lblUser);
            this.pnlUser.Controls.Add(this.btnBrowseUser);
            this.pnlUser.Controls.Add(this.btnClearUser);
            this.pnlUser.Location = new System.Drawing.Point(2832, 73);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(328, 29);
            this.pnlUser.TabIndex = 235;
            this.pnlUser.Visible = false;
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(79, 4);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(189, 22);
            this.cmbUser.TabIndex = 197;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(34, 8);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 196;
            this.lblUser.Text = "User :";
            // 
            // btnBrowseUser
            // 
            this.btnBrowseUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseUser.BackgroundImage")));
            this.btnBrowseUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseUser.Image")));
            this.btnBrowseUser.Location = new System.Drawing.Point(274, 3);
            this.btnBrowseUser.Name = "btnBrowseUser";
            this.btnBrowseUser.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseUser.TabIndex = 194;
            this.btnBrowseUser.UseVisualStyleBackColor = false;
            this.btnBrowseUser.Click += new System.EventHandler(this.btnBrowseUser_Click);
            // 
            // btnClearUser
            // 
            this.btnClearUser.BackColor = System.Drawing.Color.Transparent;
            this.btnClearUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearUser.BackgroundImage")));
            this.btnClearUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearUser.Image = ((System.Drawing.Image)(resources.GetObject("btnClearUser.Image")));
            this.btnClearUser.Location = new System.Drawing.Point(300, 3);
            this.btnClearUser.Name = "btnClearUser";
            this.btnClearUser.Size = new System.Drawing.Size(22, 22);
            this.btnClearUser.TabIndex = 195;
            this.btnClearUser.UseVisualStyleBackColor = false;
            this.btnClearUser.Click += new System.EventHandler(this.btnClearUser_Click);
            // 
            // pnlLoginUsers
            // 
            this.pnlLoginUsers.Controls.Add(this.chkIncludeAllTrays);
            this.pnlLoginUsers.Controls.Add(this.chkLoginUsers);
            this.pnlLoginUsers.Location = new System.Drawing.Point(3166, 3);
            this.pnlLoginUsers.Name = "pnlLoginUsers";
            this.pnlLoginUsers.Size = new System.Drawing.Size(328, 29);
            this.pnlLoginUsers.TabIndex = 237;
            this.pnlLoginUsers.Visible = false;
            // 
            // chkIncludeAllTrays
            // 
            this.chkIncludeAllTrays.AutoSize = true;
            this.chkIncludeAllTrays.Location = new System.Drawing.Point(170, 7);
            this.chkIncludeAllTrays.Name = "chkIncludeAllTrays";
            this.chkIncludeAllTrays.Size = new System.Drawing.Size(119, 18);
            this.chkIncludeAllTrays.TabIndex = 1;
            this.chkIncludeAllTrays.Text = "Include All Trays ";
            this.chkIncludeAllTrays.UseVisualStyleBackColor = true;
            // 
            // chkLoginUsers
            // 
            this.chkLoginUsers.AutoSize = true;
            this.chkLoginUsers.Location = new System.Drawing.Point(14, 8);
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
            this.pnlRunReportAs.Location = new System.Drawing.Point(3166, 38);
            this.pnlRunReportAs.Name = "pnlRunReportAs";
            this.pnlRunReportAs.Size = new System.Drawing.Size(328, 29);
            this.pnlRunReportAs.TabIndex = 238;
            this.pnlRunReportAs.Visible = false;
            // 
            // chkSealedCharges
            // 
            this.chkSealedCharges.AutoSize = true;
            this.chkSealedCharges.Location = new System.Drawing.Point(170, 7);
            this.chkSealedCharges.Name = "chkSealedCharges";
            this.chkSealedCharges.Size = new System.Drawing.Size(153, 18);
            this.chkSealedCharges.TabIndex = 3;
            this.chkSealedCharges.Text = "Include Sealed Charges";
            this.chkSealedCharges.UseVisualStyleBackColor = true;
            // 
            // chkOpenCharges
            // 
            this.chkOpenCharges.AutoSize = true;
            this.chkOpenCharges.Location = new System.Drawing.Point(14, 6);
            this.chkOpenCharges.Name = "chkOpenCharges";
            this.chkOpenCharges.Size = new System.Drawing.Size(147, 18);
            this.chkOpenCharges.TabIndex = 2;
            this.chkOpenCharges.Text = "Include Open Charges";
            this.chkOpenCharges.UseVisualStyleBackColor = true;
            // 
            // pnlAppointmentSort
            // 
            this.pnlAppointmentSort.Controls.Add(this.groupBox1);
            this.pnlAppointmentSort.Location = new System.Drawing.Point(3166, 73);
            this.pnlAppointmentSort.Name = "pnlAppointmentSort";
            this.pnlAppointmentSort.Size = new System.Drawing.Size(328, 29);
            this.pnlAppointmentSort.TabIndex = 239;
            this.pnlAppointmentSort.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoLstFst);
            this.groupBox1.Controls.Add(this.rdoFstLst);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(4, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(291, 32);
            this.groupBox1.TabIndex = 201;
            this.groupBox1.TabStop = false;
            // 
            // rdoLstFst
            // 
            this.rdoLstFst.AutoSize = true;
            this.rdoLstFst.Checked = true;
            this.rdoLstFst.Location = new System.Drawing.Point(172, 9);
            this.rdoLstFst.Name = "rdoLstFst";
            this.rdoLstFst.Size = new System.Drawing.Size(83, 17);
            this.rdoLstFst.TabIndex = 2;
            this.rdoLstFst.TabStop = true;
            this.rdoLstFst.Text = "Last To First";
            this.rdoLstFst.UseVisualStyleBackColor = true;
            this.rdoLstFst.Visible = false;
            this.rdoLstFst.CheckedChanged += new System.EventHandler(this.rdoLstFst_CheckedChanged);
            // 
            // rdoFstLst
            // 
            this.rdoFstLst.AutoSize = true;
            this.rdoFstLst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoFstLst.Location = new System.Drawing.Point(13, 9);
            this.rdoFstLst.Name = "rdoFstLst";
            this.rdoFstLst.Size = new System.Drawing.Size(92, 18);
            this.rdoFstLst.TabIndex = 0;
            this.rdoFstLst.Text = "First To Last";
            this.rdoFstLst.UseVisualStyleBackColor = true;
            this.rdoFstLst.CheckedChanged += new System.EventHandler(this.rdoFstLst_CheckedChanged);
            // 
            // pnlReportType
            // 
            this.pnlReportType.Controls.Add(this.groupBox2);
            this.pnlReportType.Location = new System.Drawing.Point(3500, 3);
            this.pnlReportType.Name = "pnlReportType";
            this.pnlReportType.Size = new System.Drawing.Size(339, 101);
            this.pnlReportType.TabIndex = 240;
            this.pnlReportType.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoWithoutInsRepCat);
            this.groupBox2.Controls.Add(this.rdoWithInsRepCat);
            this.groupBox2.Controls.Add(this.rdoWithoutName);
            this.groupBox2.Controls.Add(this.rdoWithName);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(4, -6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(332, 104);
            this.groupBox2.TabIndex = 201;
            this.groupBox2.TabStop = false;
            // 
            // rdoWithoutInsRepCat
            // 
            this.rdoWithoutInsRepCat.AutoSize = true;
            this.rdoWithoutInsRepCat.Location = new System.Drawing.Point(3, 80);
            this.rdoWithoutInsRepCat.Name = "rdoWithoutInsRepCat";
            this.rdoWithoutInsRepCat.Size = new System.Drawing.Size(283, 17);
            this.rdoWithoutInsRepCat.TabIndex = 4;
            this.rdoWithoutInsRepCat.Text = "Insurance Plan With out Insurance Reporting Category";
            this.rdoWithoutInsRepCat.UseVisualStyleBackColor = true;
            // 
            // rdoWithInsRepCat
            // 
            this.rdoWithInsRepCat.AutoSize = true;
            this.rdoWithInsRepCat.Location = new System.Drawing.Point(3, 59);
            this.rdoWithInsRepCat.Name = "rdoWithInsRepCat";
            this.rdoWithInsRepCat.Size = new System.Drawing.Size(265, 17);
            this.rdoWithInsRepCat.TabIndex = 3;
            this.rdoWithInsRepCat.Text = "Insurance Plan With Insurance Reporting Category";
            this.rdoWithInsRepCat.UseVisualStyleBackColor = true;
            // 
            // rdoWithoutName
            // 
            this.rdoWithoutName.AutoSize = true;
            this.rdoWithoutName.Location = new System.Drawing.Point(3, 35);
            this.rdoWithoutName.Name = "rdoWithoutName";
            this.rdoWithoutName.Size = new System.Drawing.Size(217, 17);
            this.rdoWithoutName.TabIndex = 2;
            this.rdoWithoutName.Text = "Insurance Plan With out Company Name";
            this.rdoWithoutName.UseVisualStyleBackColor = true;
            this.rdoWithoutName.CheckedChanged += new System.EventHandler(this.rdoWithoutName_CheckedChanged);
            // 
            // rdoWithName
            // 
            this.rdoWithName.AutoSize = true;
            this.rdoWithName.Checked = true;
            this.rdoWithName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoWithName.Location = new System.Drawing.Point(3, 13);
            this.rdoWithName.Name = "rdoWithName";
            this.rdoWithName.Size = new System.Drawing.Size(246, 18);
            this.rdoWithName.TabIndex = 0;
            this.rdoWithName.TabStop = true;
            this.rdoWithName.Text = "Insurance Plan With Company Name";
            this.rdoWithName.UseVisualStyleBackColor = true;
            this.rdoWithName.CheckedChanged += new System.EventHandler(this.rdoWithName_CheckedChanged);
            // 
            // pnlResource
            // 
            this.pnlResource.Controls.Add(this.cmbResouce);
            this.pnlResource.Controls.Add(this.lblReaource);
            this.pnlResource.Controls.Add(this.btnBrwsMultiResource);
            this.pnlResource.Controls.Add(this.btnClearResource);
            this.pnlResource.Location = new System.Drawing.Point(3845, 3);
            this.pnlResource.Name = "pnlResource";
            this.pnlResource.Size = new System.Drawing.Size(328, 29);
            this.pnlResource.TabIndex = 241;
            this.pnlResource.Visible = false;
            this.pnlResource.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlResource_Paint);
            // 
            // cmbResouce
            // 
            this.cmbResouce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResouce.FormattingEnabled = true;
            this.cmbResouce.Location = new System.Drawing.Point(71, 4);
            this.cmbResouce.Name = "cmbResouce";
            this.cmbResouce.Size = new System.Drawing.Size(189, 22);
            this.cmbResouce.TabIndex = 197;
            // 
            // lblReaource
            // 
            this.lblReaource.AutoSize = true;
            this.lblReaource.Location = new System.Drawing.Point(0, 9);
            this.lblReaource.Name = "lblReaource";
            this.lblReaource.Size = new System.Drawing.Size(65, 14);
            this.lblReaource.TabIndex = 196;
            this.lblReaource.Text = "Resource :";
            // 
            // btnBrwsMultiResource
            // 
            this.btnBrwsMultiResource.BackColor = System.Drawing.Color.Transparent;
            this.btnBrwsMultiResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrwsMultiResource.BackgroundImage")));
            this.btnBrwsMultiResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrwsMultiResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrwsMultiResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrwsMultiResource.Image = ((System.Drawing.Image)(resources.GetObject("btnBrwsMultiResource.Image")));
            this.btnBrwsMultiResource.Location = new System.Drawing.Point(266, 3);
            this.btnBrwsMultiResource.Name = "btnBrwsMultiResource";
            this.btnBrwsMultiResource.Size = new System.Drawing.Size(22, 22);
            this.btnBrwsMultiResource.TabIndex = 194;
            this.btnBrwsMultiResource.UseVisualStyleBackColor = false;
            this.btnBrwsMultiResource.Click += new System.EventHandler(this.btnBrwsMultiResource_Click);
            // 
            // btnClearResource
            // 
            this.btnClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btnClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearResource.BackgroundImage")));
            this.btnClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btnClearResource.Image")));
            this.btnClearResource.Location = new System.Drawing.Point(291, 3);
            this.btnClearResource.Name = "btnClearResource";
            this.btnClearResource.Size = new System.Drawing.Size(22, 22);
            this.btnClearResource.TabIndex = 195;
            this.btnClearResource.UseVisualStyleBackColor = false;
            this.btnClearResource.Click += new System.EventHandler(this.btnClearResource_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(3, 766);
            this.label1.TabIndex = 257;
            this.label1.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3897, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(3, 766);
            this.label2.TabIndex = 258;
            this.label2.Text = "label4";
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.CausesValidation = false;
            this.crvReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReportViewer.DisplayBackgroundEdge = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.EnableDrillDown = false;
            this.crvReportViewer.Location = new System.Drawing.Point(0, 3);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(3894, 512);
            this.crvReportViewer.TabIndex = 29;
            this.crvReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            this.crvReportViewer.Load += new System.EventHandler(this.crvReportViewer_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.crvReportViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 187);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(3894, 515);
            this.panel1.TabIndex = 90;
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Transparent;
            this.Panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel8.BackgroundImage")));
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.pnelReportType);
            this.Panel8.Controls.Add(this.Label23);
            this.Panel8.Controls.Add(this.btnDown);
            this.Panel8.Controls.Add(this.btnUP);
            this.Panel8.Controls.Add(this.Label25);
            this.Panel8.Controls.Add(this.Label26);
            this.Panel8.Controls.Add(this.Label27);
            this.Panel8.Controls.Add(this.lblbtnDown);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel8.Location = new System.Drawing.Point(3, 55);
            this.Panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(3894, 24);
            this.Panel8.TabIndex = 259;
            this.Panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel8_Paint);
            // 
            // pnelReportType
            // 
            this.pnelReportType.Controls.Add(this.cmbReportType);
            this.pnelReportType.Controls.Add(this.lblReportType);
            this.pnelReportType.Location = new System.Drawing.Point(9, 1);
            this.pnelReportType.Name = "pnelReportType";
            this.pnelReportType.Size = new System.Drawing.Size(461, 21);
            this.pnelReportType.TabIndex = 219;
            this.pnelReportType.Visible = false;
            // 
            // cmbReportType
            // 
            this.cmbReportType.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(97, 0);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(361, 22);
            this.cmbReportType.TabIndex = 221;
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblReportType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportType.Location = new System.Drawing.Point(0, 0);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Padding = new System.Windows.Forms.Padding(3);
            this.lblReportType.Size = new System.Drawing.Size(97, 20);
            this.lblReportType.TabIndex = 200;
            this.lblReportType.Text = "Report Type :";
            this.lblReportType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1, 1);
            this.Label23.Name = "Label23";
            this.Label23.Padding = new System.Windows.Forms.Padding(3);
            this.Label23.Size = new System.Drawing.Size(66, 20);
            this.Label23.TabIndex = 9;
            this.Label23.Text = "  Search ";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(3849, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 22);
            this.btnDown.TabIndex = 12;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // btnUP
            // 
            this.btnUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUP.FlatAppearance.BorderSize = 0;
            this.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUP.Location = new System.Drawing.Point(3871, 1);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(22, 22);
            this.btnUP.TabIndex = 11;
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            this.btnUP.MouseLeave += new System.EventHandler(this.btnUP_MouseLeave);
            this.btnUP.MouseHover += new System.EventHandler(this.btnUP_MouseHover);
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(0, 1);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1, 22);
            this.Label25.TabIndex = 7;
            this.Label25.Text = "label4";
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label26.Location = new System.Drawing.Point(3893, 1);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(1, 22);
            this.Label26.TabIndex = 6;
            this.Label26.Text = "label3";
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(0, 0);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(3894, 1);
            this.Label27.TabIndex = 5;
            this.Label27.Text = "label1";
            // 
            // lblbtnDown
            // 
            this.lblbtnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblbtnDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblbtnDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbtnDown.Location = new System.Drawing.Point(0, 23);
            this.lblbtnDown.Name = "lblbtnDown";
            this.lblbtnDown.Size = new System.Drawing.Size(3894, 1);
            this.lblbtnDown.TabIndex = 13;
            this.lblbtnDown.Text = "label1";
            // 
            // pnlSummaryContainer
            // 
            this.pnlSummaryContainer.Controls.Add(this.label3);
            this.pnlSummaryContainer.Controls.Add(this.radioButton1);
            this.pnlSummaryContainer.Controls.Add(this.radioButton2);
            this.pnlSummaryContainer.Controls.Add(this.label19);
            this.pnlSummaryContainer.Controls.Add(this.label18);
            this.pnlSummaryContainer.Controls.Add(this.label17);
            this.pnlSummaryContainer.Controls.Add(this.label16);
            this.pnlSummaryContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryContainer.Location = new System.Drawing.Point(3, 728);
            this.pnlSummaryContainer.Name = "pnlSummaryContainer";
            this.pnlSummaryContainer.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlSummaryContainer.Size = new System.Drawing.Size(3894, 38);
            this.pnlSummaryContainer.TabIndex = 262;
            this.pnlSummaryContainer.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "Seal Charges :";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(172, 9);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(40, 18);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.Text = "No";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(117, 9);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(45, 18);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Yes";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(3892, 1);
            this.label19.TabIndex = 15;
            this.label19.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(1, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(3892, 1);
            this.label18.TabIndex = 14;
            this.label18.Text = "label1";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3893, 3);
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
            this.label16.Location = new System.Drawing.Point(0, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 32);
            this.label16.TabIndex = 8;
            this.label16.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(3894, 1);
            this.label11.TabIndex = 13;
            this.label11.Text = "label1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(3894, 1);
            this.label15.TabIndex = 5;
            this.label15.Text = "label1";
            // 
            // pnlSummaryHeader
            // 
            this.pnlSummaryHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummaryHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSummaryHeader.Location = new System.Drawing.Point(0, 1);
            this.pnlSummaryHeader.Name = "pnlSummaryHeader";
            this.pnlSummaryHeader.Size = new System.Drawing.Size(3894, 24);
            this.pnlSummaryHeader.TabIndex = 9;
            this.pnlSummaryHeader.Text = " Seal Charges";
            this.pnlSummaryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(3893, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 24);
            this.label13.TabIndex = 6;
            this.label13.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 24);
            this.label12.TabIndex = 7;
            this.label12.Text = "label4";
            // 
            // btnTrayUp
            // 
            this.btnTrayUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTrayUp.FlatAppearance.BorderSize = 0;
            this.btnTrayUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTrayUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTrayUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrayUp.Location = new System.Drawing.Point(3871, 1);
            this.btnTrayUp.Name = "btnTrayUp";
            this.btnTrayUp.Size = new System.Drawing.Size(22, 24);
            this.btnTrayUp.TabIndex = 11;
            this.btnTrayUp.UseVisualStyleBackColor = true;
            this.btnTrayUp.Click += new System.EventHandler(this.btnTrayUp_Click);
            this.btnTrayUp.MouseLeave += new System.EventHandler(this.btnTrayUp_MouseLeave);
            this.btnTrayUp.MouseHover += new System.EventHandler(this.btnTrayUp_MouseHover);
            // 
            // btnTrayDown
            // 
            this.btnTrayDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTrayDown.FlatAppearance.BorderSize = 0;
            this.btnTrayDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTrayDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTrayDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrayDown.Location = new System.Drawing.Point(3849, 1);
            this.btnTrayDown.Name = "btnTrayDown";
            this.btnTrayDown.Size = new System.Drawing.Size(22, 24);
            this.btnTrayDown.TabIndex = 12;
            this.btnTrayDown.UseVisualStyleBackColor = true;
            this.btnTrayDown.Click += new System.EventHandler(this.btnTrayDown_Click);
            this.btnTrayDown.MouseLeave += new System.EventHandler(this.btnTrayDown_MouseLeave);
            this.btnTrayDown.MouseHover += new System.EventHandler(this.btnTrayDown_MouseHover);
            // 
            // pnlSealCharges
            // 
            this.pnlSealCharges.BackColor = System.Drawing.Color.Transparent;
            this.pnlSealCharges.BackgroundImage = global::gloReports.Properties.Resources.Img_LongOrange;
            this.pnlSealCharges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSealCharges.Controls.Add(this.btnTrayDown);
            this.pnlSealCharges.Controls.Add(this.btnTrayUp);
            this.pnlSealCharges.Controls.Add(this.label12);
            this.pnlSealCharges.Controls.Add(this.label13);
            this.pnlSealCharges.Controls.Add(this.pnlSummaryHeader);
            this.pnlSealCharges.Controls.Add(this.label15);
            this.pnlSealCharges.Controls.Add(this.label11);
            this.pnlSealCharges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSealCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSealCharges.Location = new System.Drawing.Point(3, 702);
            this.pnlSealCharges.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlSealCharges.Name = "pnlSealCharges";
            this.pnlSealCharges.Size = new System.Drawing.Size(3894, 26);
            this.pnlSealCharges.TabIndex = 261;
            this.pnlSealCharges.Tag = "Orange";
            this.pnlSealCharges.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gloReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSealCharges);
            this.Controls.Add(this.pnlSummaryContainer);
            this.Controls.Add(this.fpnlCriteria);
            this.Controls.Add(this.Panel8);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloReportViewer";
            this.Size = new System.Drawing.Size(3900, 766);
            this.Load += new System.EventHandler(this.gloReportViewer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gloReportViewer_Paint);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlCPT.ResumeLayout(false);
            this.pnlCPT.PerformLayout();
            this.pnlInsurance.ResumeLayout(false);
            this.pnlInsurance.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.pnlPatients.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndrpYear)).EndInit();
            this.fpnlCriteria.ResumeLayout(false);
            this.pnlLSTMonths.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnlDgCode.ResumeLayout(false);
            this.pnlDgCode.PerformLayout();
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.pnlMonth.ResumeLayout(false);
            this.pnlMonth.PerformLayout();
            this.pnlYear.ResumeLayout(false);
            this.pnlYear.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            this.pnlCity.ResumeLayout(false);
            this.pnlCity.PerformLayout();
            this.pnlState.ResumeLayout(false);
            this.pnlState.PerformLayout();
            this.pnlZip.ResumeLayout(false);
            this.pnlZip.PerformLayout();
            this.pnlAgingCriteria.ResumeLayout(false);
            this.pnlAgingCriteria.PerformLayout();
            this.pnlAppType.ResumeLayout(false);
            this.pnlAppType.PerformLayout();
            this.pnlAppFlag.ResumeLayout(false);
            this.grpSortCriteria.ResumeLayout(false);
            this.grpSortCriteria.PerformLayout();
            this.pnlCancelApp.ResumeLayout(false);
            this.grpAppStatus.ResumeLayout(false);
            this.grpAppStatus.PerformLayout();
            this.pnlAmountType.ResumeLayout(false);
            this.grpPayType.ResumeLayout(false);
            this.grpPayType.PerformLayout();
            this.pnlTraySelection.ResumeLayout(false);
            this.grpTraySelection.ResumeLayout(false);
            this.grpTraySelection.PerformLayout();
            this.pnlActivePayTray.ResumeLayout(false);
            this.pnlActivePayTray.PerformLayout();
            this.pnlClosedPayTray.ResumeLayout(false);
            this.pnlClosedPayTray.PerformLayout();
            this.pnlMultiFacility.ResumeLayout(false);
            this.pnlMultiFacility.PerformLayout();
            this.pnlMultiChargesTray.ResumeLayout(false);
            this.pnlMultiChargesTray.PerformLayout();
            this.pnlMultiPayTray.ResumeLayout(false);
            this.pnlMultiPayTray.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlLoginUsers.ResumeLayout(false);
            this.pnlLoginUsers.PerformLayout();
            this.pnlRunReportAs.ResumeLayout(false);
            this.pnlRunReportAs.PerformLayout();
            this.pnlAppointmentSort.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlReportType.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlResource.ResumeLayout(false);
            this.pnlResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.pnelReportType.ResumeLayout(false);
            this.pnelReportType.PerformLayout();
            this.pnlSummaryContainer.ResumeLayout(false);
            this.pnlSummaryContainer.PerformLayout();
            this.pnlSealCharges.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Button btnClearDiagnosisCode;
        internal System.Windows.Forms.Button btnBrowseDiagnosisCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDiagnosisCode;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblFacility;
        private System.Windows.Forms.ComboBox cmbFacility;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Button btnClearPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.NumericUpDown ndrpYear;
        private System.Windows.Forms.ComboBox drpMonth;
        private System.Windows.Forms.FlowLayoutPanel fpnlCriteria;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.Panel pnlInsurance;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Panel pnlCPT;
        private System.Windows.Forms.Label lblCPT;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Panel pnlFacility;
        private System.Windows.Forms.Panel pnlDgCode;
        private System.Windows.Forms.Panel pnlYear;
        private System.Windows.Forms.Panel pnlMonth;
        private System.Windows.Forms.Panel pnlCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Panel pnlState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Panel pnlZip;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.Panel pnlAgingCriteria;
        private System.Windows.Forms.Label lblAgingCriteria;
        private System.Windows.Forms.ComboBox cmbAgingCritera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlAppType;
        internal System.Windows.Forms.Button btnClearAppointmentType;
        internal System.Windows.Forms.Button btnBrowseAppointmentType;
        internal System.Windows.Forms.ComboBox cmbApp_AppointmentType;
        internal System.Windows.Forms.Label lblApp_AppointmentType;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Panel pnlAppFlag;
        private System.Windows.Forms.GroupBox grpSortCriteria;
        private System.Windows.Forms.RadioButton rbSortNewPatient;
        private System.Windows.Forms.RadioButton rbSortEstablishedPatient;
        private System.Windows.Forms.Panel pnlCancelApp;
        private System.Windows.Forms.GroupBox grpAppStatus;
        private System.Windows.Forms.RadioButton rbDeletedAppointments;
        private System.Windows.Forms.RadioButton rbNoShowAppointments;
        private System.Windows.Forms.RadioButton rbCancelAppointments;
        private System.Windows.Forms.Panel pnlAmountType;
        private System.Windows.Forms.GroupBox grpPayType;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbAllowed;
        private System.Windows.Forms.RadioButton rbCharges;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsb_btnGenerateBatch;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        private System.Windows.Forms.Panel pnlLSTMonths;
        private System.Windows.Forms.Panel panel3;
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
        private System.Windows.Forms.Panel pnlActivePayTray;
        private System.Windows.Forms.Label lblActivePayTray;
        private System.Windows.Forms.ComboBox cmbActivePayTray;
        private System.Windows.Forms.Panel pnlClosedPayTray;
        private System.Windows.Forms.Label lblClosedPayTray;
        private System.Windows.Forms.ComboBox cmbClosedPayTray;
        private System.Windows.Forms.Panel pnlTraySelection;
        private System.Windows.Forms.GroupBox grpTraySelection;
        private System.Windows.Forms.RadioButton rbClosedPayTray;
        private System.Windows.Forms.RadioButton rbActivePayTray;
        private System.Windows.Forms.Panel pnlMultiFacility;
        private System.Windows.Forms.ComboBox cmbMultiFacility;
        private System.Windows.Forms.Label lblMultiFacility;
        internal System.Windows.Forms.Button btnBrowseMultiFacility;
        internal System.Windows.Forms.Button btnClearMultiFacility;
        private System.Windows.Forms.Panel pnlMultiChargesTray;
        private System.Windows.Forms.ComboBox cmbMultiChargesTray;
        private System.Windows.Forms.Label lblMultiChargesTray;
        internal System.Windows.Forms.Button btnBrowseMultiChargesTray;
        internal System.Windows.Forms.Button btnClearMultiChargesTray;
        private System.Windows.Forms.Panel pnlMultiPayTray;
        private System.Windows.Forms.ComboBox cmblMultiPayTray;
        private System.Windows.Forms.Label lbllMultiPayTray;
        internal System.Windows.Forms.Button btnBrowseMultiPayTray;
        internal System.Windows.Forms.Button btnClearMultiPayTray;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.Button btnBrowseUser;
        internal System.Windows.Forms.Button btnClearUser;
        internal System.Windows.Forms.Panel Panel8;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label27;
        private System.Windows.Forms.Panel pnlSummaryContainer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label pnlSummaryHeader;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Button btnTrayUp;
        internal System.Windows.Forms.Button btnTrayDown;
        internal System.Windows.Forms.Panel pnlSealCharges;
        private System.Windows.Forms.Label lblbtnDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlLoginUsers;
        private System.Windows.Forms.CheckBox chkLoginUsers;
        private System.Windows.Forms.Panel pnlRunReportAs;
        private System.Windows.Forms.CheckBox chkIncludeAllTrays;
        private System.Windows.Forms.CheckBox chkSealedCharges;
        private System.Windows.Forms.CheckBox chkOpenCharges;
        private System.Windows.Forms.Panel pnlAppointmentSort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoLstFst;
        private System.Windows.Forms.RadioButton rdoFstLst;
        private System.Windows.Forms.Panel pnlReportType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoWithoutName;
        private System.Windows.Forms.RadioButton rdoWithName;
        private System.Windows.Forms.RadioButton rdoWithoutInsRepCat;
        private System.Windows.Forms.RadioButton rdoWithInsRepCat;
        private System.Windows.Forms.Panel pnelReportType;
        private System.Windows.Forms.ComboBox cmbReportType;
        internal System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Panel pnlResource;
        private System.Windows.Forms.ComboBox cmbResouce;
        private System.Windows.Forms.Label lblReaource;
        internal System.Windows.Forms.Button btnBrwsMultiResource;
        internal System.Windows.Forms.Button btnClearResource;
    }
}
