namespace gloReports
{
    partial class frmRpt_AgingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_AgingReport));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnGenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlReportViewer = new System.Windows.Forms.Panel();
            this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.lblbtnDown = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_DateOfResponsibility = new System.Windows.Forms.RadioButton();
            this.rbtn_DateOfService = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_AgingDetails = new System.Windows.Forms.RadioButton();
            this.rbtn_AgingSummary = new System.Windows.Forms.RadioButton();
            this.grpAppStatus = new System.Windows.Forms.GroupBox();
            this.rbtn_Both = new System.Windows.Forms.RadioButton();
            this.rbtn_PatientDue = new System.Windows.Forms.RadioButton();
            this.rbtn_InsurancePending = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cmbBreakBy = new System.Windows.Forms.ComboBox();
            this.lblBreakBy = new System.Windows.Forms.Label();
            this.pnlInsuranceCompany = new System.Windows.Forms.Panel();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.cmbInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.pnlFacility = new System.Windows.Forms.Panel();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.btnClearFacility = new System.Windows.Forms.Button();
            this.btnBrowseFacility = new System.Windows.Forms.Button();
            this.lblFacility = new System.Windows.Forms.Label();
            this.pnlReportingCategory = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbReportingCategory = new System.Windows.Forms.ComboBox();
            this.btnClearReportingCriteria = new System.Windows.Forms.Button();
            this.btnBrowseReportingCriteria = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.grInsurance = new System.Windows.Forms.GroupBox();
            this.rbtn_ReportingCategory = new System.Windows.Forms.RadioButton();
            this.rbtn_InsCompany = new System.Windows.Forms.RadioButton();
            this.chkPhone = new System.Windows.Forms.CheckBox();
            this.chkUncloseAct = new System.Windows.Forms.CheckBox();
            this.txtPatBal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.chkPatientDetail = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAgingDays = new System.Windows.Forms.TextBox();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlReportViewer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpAppStatus.SuspendLayout();
            this.pnlInsuranceCompany.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.pnlReportingCategory.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.grInsurance.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
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
            this.pnlToolStrip.Size = new System.Drawing.Size(1188, 54);
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
            this.tsb_btnGenerateBatch,
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1188, 53);
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
            this.tsb_btnExportReport.Click += new System.EventHandler(this.tsb_btnExport_Click);
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
            // pnlReportViewer
            // 
            this.pnlReportViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlReportViewer.Controls.Add(this.crvReportViewer);
            this.pnlReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReportViewer.Location = new System.Drawing.Point(0, 228);
            this.pnlReportViewer.Name = "pnlReportViewer";
            this.pnlReportViewer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlReportViewer.Size = new System.Drawing.Size(1188, 427);
            this.pnlReportViewer.TabIndex = 31;
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.CausesValidation = false;
            this.crvReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReportViewer.DisplayBackgroundEdge = false;
            this.crvReportViewer.DisplayGroupTree = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.EnableDrillDown = false;
            this.crvReportViewer.Location = new System.Drawing.Point(3, 0);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(1182, 424);
            this.crvReportViewer.TabIndex = 30;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            this.crvReportViewer.Visible = false;
            this.crvReportViewer.Load += new System.EventHandler(this.crvReportViewer_Load);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Panel8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(1188, 30);
            this.panel2.TabIndex = 261;
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Transparent;
            this.Panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel8.BackgroundImage")));
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.btnDown);
            this.Panel8.Controls.Add(this.btnUP);
            this.Panel8.Controls.Add(this.Label25);
            this.Panel8.Controls.Add(this.Label26);
            this.Panel8.Controls.Add(this.Label23);
            this.Panel8.Controls.Add(this.Label27);
            this.Panel8.Controls.Add(this.lblbtnDown);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel8.Location = new System.Drawing.Point(3, 3);
            this.Panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(1182, 24);
            this.Panel8.TabIndex = 260;
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(1129, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 22);
            this.btnDown.TabIndex = 12;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // btnUP
            // 
            this.btnUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUP.FlatAppearance.BorderSize = 0;
            this.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUP.Location = new System.Drawing.Point(1155, 1);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(26, 22);
            this.btnUP.TabIndex = 11;
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.MouseLeave += new System.EventHandler(this.btnUP_MouseLeave);
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
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
            this.Label26.Location = new System.Drawing.Point(1181, 1);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(1, 22);
            this.Label26.TabIndex = 6;
            this.Label26.Text = "label3";
            // 
            // Label23
            // 
            this.Label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(0, 1);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(1182, 22);
            this.Label23.TabIndex = 9;
            this.Label23.Text = "  Report Settings :";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(0, 0);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(1182, 1);
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
            this.lblbtnDown.Size = new System.Drawing.Size(1182, 1);
            this.lblbtnDown.TabIndex = 13;
            this.lblbtnDown.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 141);
            this.label2.TabIndex = 236;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1184, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 141);
            this.label3.TabIndex = 237;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1180, 1);
            this.label4.TabIndex = 238;
            this.label4.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1180, 1);
            this.label5.TabIndex = 239;
            this.label5.Text = "label1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtn_DateOfResponsibility);
            this.groupBox3.Controls.Add(this.rbtn_DateOfService);
            this.groupBox3.Location = new System.Drawing.Point(80, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 30);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // rbtn_DateOfResponsibility
            // 
            this.rbtn_DateOfResponsibility.AutoSize = true;
            this.rbtn_DateOfResponsibility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_DateOfResponsibility.Location = new System.Drawing.Point(171, 10);
            this.rbtn_DateOfResponsibility.Name = "rbtn_DateOfResponsibility";
            this.rbtn_DateOfResponsibility.Size = new System.Drawing.Size(141, 18);
            this.rbtn_DateOfResponsibility.TabIndex = 1;
            this.rbtn_DateOfResponsibility.Text = "Date of Responsibility";
            this.rbtn_DateOfResponsibility.UseVisualStyleBackColor = true;
            this.rbtn_DateOfResponsibility.CheckedChanged += new System.EventHandler(this.rbtn_DateOfResponsibility_CheckedChanged);
            // 
            // rbtn_DateOfService
            // 
            this.rbtn_DateOfService.AutoSize = true;
            this.rbtn_DateOfService.Checked = true;
            this.rbtn_DateOfService.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_DateOfService.Location = new System.Drawing.Point(15, 9);
            this.rbtn_DateOfService.Name = "rbtn_DateOfService";
            this.rbtn_DateOfService.Size = new System.Drawing.Size(118, 18);
            this.rbtn_DateOfService.TabIndex = 0;
            this.rbtn_DateOfService.TabStop = true;
            this.rbtn_DateOfService.Text = "Date of Service";
            this.rbtn_DateOfService.UseVisualStyleBackColor = true;
            this.rbtn_DateOfService.CheckedChanged += new System.EventHandler(this.rbtn_DateOfService_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_AgingDetails);
            this.groupBox1.Controls.Add(this.rbtn_AgingSummary);
            this.groupBox1.Location = new System.Drawing.Point(80, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 30);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbtn_AgingDetails
            // 
            this.rbtn_AgingDetails.AutoSize = true;
            this.rbtn_AgingDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_AgingDetails.Location = new System.Drawing.Point(171, 9);
            this.rbtn_AgingDetails.Name = "rbtn_AgingDetails";
            this.rbtn_AgingDetails.Size = new System.Drawing.Size(95, 18);
            this.rbtn_AgingDetails.TabIndex = 1;
            this.rbtn_AgingDetails.Text = "Aging Details";
            this.rbtn_AgingDetails.UseVisualStyleBackColor = true;
            this.rbtn_AgingDetails.CheckedChanged += new System.EventHandler(this.rbtn_AgingDetails_CheckedChanged);
            // 
            // rbtn_AgingSummary
            // 
            this.rbtn_AgingSummary.AutoSize = true;
            this.rbtn_AgingSummary.Checked = true;
            this.rbtn_AgingSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_AgingSummary.Location = new System.Drawing.Point(15, 9);
            this.rbtn_AgingSummary.Name = "rbtn_AgingSummary";
            this.rbtn_AgingSummary.Size = new System.Drawing.Size(122, 18);
            this.rbtn_AgingSummary.TabIndex = 0;
            this.rbtn_AgingSummary.TabStop = true;
            this.rbtn_AgingSummary.Text = "Aging Summary";
            this.rbtn_AgingSummary.UseVisualStyleBackColor = true;
            this.rbtn_AgingSummary.CheckedChanged += new System.EventHandler(this.rbtn_AgingSummary_CheckedChanged);
            // 
            // grpAppStatus
            // 
            this.grpAppStatus.Controls.Add(this.rbtn_Both);
            this.grpAppStatus.Controls.Add(this.rbtn_PatientDue);
            this.grpAppStatus.Controls.Add(this.rbtn_InsurancePending);
            this.grpAppStatus.Location = new System.Drawing.Point(80, 89);
            this.grpAppStatus.Name = "grpAppStatus";
            this.grpAppStatus.Size = new System.Drawing.Size(363, 30);
            this.grpAppStatus.TabIndex = 2;
            this.grpAppStatus.TabStop = false;
            // 
            // rbtn_Both
            // 
            this.rbtn_Both.AutoSize = true;
            this.rbtn_Both.Checked = true;
            this.rbtn_Both.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Both.Location = new System.Drawing.Point(283, 9);
            this.rbtn_Both.Name = "rbtn_Both";
            this.rbtn_Both.Size = new System.Drawing.Size(55, 18);
            this.rbtn_Both.TabIndex = 2;
            this.rbtn_Both.TabStop = true;
            this.rbtn_Both.Text = "Both";
            this.rbtn_Both.UseVisualStyleBackColor = true;
            this.rbtn_Both.CheckedChanged += new System.EventHandler(this.rbtn_Both_CheckedChanged);
            // 
            // rbtn_PatientDue
            // 
            this.rbtn_PatientDue.AutoSize = true;
            this.rbtn_PatientDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_PatientDue.Location = new System.Drawing.Point(171, 9);
            this.rbtn_PatientDue.Name = "rbtn_PatientDue";
            this.rbtn_PatientDue.Size = new System.Drawing.Size(90, 18);
            this.rbtn_PatientDue.TabIndex = 1;
            this.rbtn_PatientDue.Text = "Patient Due";
            this.rbtn_PatientDue.UseVisualStyleBackColor = true;
            this.rbtn_PatientDue.CheckedChanged += new System.EventHandler(this.rbtn_PatientDue_CheckedChanged);
            // 
            // rbtn_InsurancePending
            // 
            this.rbtn_InsurancePending.AutoSize = true;
            this.rbtn_InsurancePending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_InsurancePending.Location = new System.Drawing.Point(14, 9);
            this.rbtn_InsurancePending.Name = "rbtn_InsurancePending";
            this.rbtn_InsurancePending.Size = new System.Drawing.Size(130, 18);
            this.rbtn_InsurancePending.TabIndex = 1;
            this.rbtn_InsurancePending.Text = "Insurance Pending ";
            this.rbtn_InsurancePending.UseVisualStyleBackColor = true;
            this.rbtn_InsurancePending.CheckedChanged += new System.EventHandler(this.rbtn_InsurancePending_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 235;
            this.label1.Text = "As of Date :";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(89, 7);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(111, 22);
            this.dtpEndDate.TabIndex = 236;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // cmbBreakBy
            // 
            this.cmbBreakBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBreakBy.FormattingEnabled = true;
            this.cmbBreakBy.Location = new System.Drawing.Point(274, 7);
            this.cmbBreakBy.Name = "cmbBreakBy";
            this.cmbBreakBy.Size = new System.Drawing.Size(159, 22);
            this.cmbBreakBy.TabIndex = 236;
            // 
            // lblBreakBy
            // 
            this.lblBreakBy.AutoSize = true;
            this.lblBreakBy.Location = new System.Drawing.Point(209, 11);
            this.lblBreakBy.Name = "lblBreakBy";
            this.lblBreakBy.Size = new System.Drawing.Size(62, 14);
            this.lblBreakBy.TabIndex = 235;
            this.lblBreakBy.Text = "Break by :";
            // 
            // pnlInsuranceCompany
            // 
            this.pnlInsuranceCompany.Controls.Add(this.lblInsurance);
            this.pnlInsuranceCompany.Controls.Add(this.cmbInsuranceCompany);
            this.pnlInsuranceCompany.Controls.Add(this.btnClearInsurance);
            this.pnlInsuranceCompany.Controls.Add(this.btnBrowseInsurance);
            this.pnlInsuranceCompany.Location = new System.Drawing.Point(776, 33);
            this.pnlInsuranceCompany.Name = "pnlInsuranceCompany";
            this.pnlInsuranceCompany.Size = new System.Drawing.Size(381, 22);
            this.pnlInsuranceCompany.TabIndex = 240;
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Location = new System.Drawing.Point(6, 4);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(122, 14);
            this.lblInsurance.TabIndex = 197;
            this.lblInsurance.Text = "Insurance Company :";
            // 
            // cmbInsuranceCompany
            // 
            this.cmbInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.FormattingEnabled = true;
            this.cmbInsuranceCompany.Location = new System.Drawing.Point(131, 0);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new System.Drawing.Size(189, 22);
            this.cmbInsuranceCompany.TabIndex = 189;
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(351, 0);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(23, 22);
            this.btnClearInsurance.TabIndex = 192;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(324, 0);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 191;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            // 
            // pnlFacility
            // 
            this.pnlFacility.Controls.Add(this.cmbFacility);
            this.pnlFacility.Controls.Add(this.btnClearFacility);
            this.pnlFacility.Controls.Add(this.btnBrowseFacility);
            this.pnlFacility.Controls.Add(this.lblFacility);
            this.pnlFacility.Location = new System.Drawing.Point(454, 7);
            this.pnlFacility.Name = "pnlFacility";
            this.pnlFacility.Size = new System.Drawing.Size(314, 22);
            this.pnlFacility.TabIndex = 241;
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(63, 0);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(189, 22);
            this.cmbFacility.TabIndex = 236;
            // 
            // btnClearFacility
            // 
            this.btnClearFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearFacility.BackgroundImage")));
            this.btnClearFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFacility.Image")));
            this.btnClearFacility.Location = new System.Drawing.Point(282, 0);
            this.btnClearFacility.Name = "btnClearFacility";
            this.btnClearFacility.Size = new System.Drawing.Size(23, 22);
            this.btnClearFacility.TabIndex = 238;
            this.btnClearFacility.UseVisualStyleBackColor = false;
            this.btnClearFacility.Click += new System.EventHandler(this.btnClearFacility_Click);
            // 
            // btnBrowseFacility
            // 
            this.btnBrowseFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.BackgroundImage")));
            this.btnBrowseFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.Image")));
            this.btnBrowseFacility.Location = new System.Drawing.Point(256, 0);
            this.btnBrowseFacility.Name = "btnBrowseFacility";
            this.btnBrowseFacility.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseFacility.TabIndex = 237;
            this.btnBrowseFacility.UseVisualStyleBackColor = false;
            this.btnBrowseFacility.Click += new System.EventHandler(this.btnBrowseFacility_Click);
            // 
            // lblFacility
            // 
            this.lblFacility.AutoSize = true;
            this.lblFacility.Location = new System.Drawing.Point(12, 4);
            this.lblFacility.Name = "lblFacility";
            this.lblFacility.Size = new System.Drawing.Size(50, 14);
            this.lblFacility.TabIndex = 235;
            this.lblFacility.Text = "Facility :";
            // 
            // pnlReportingCategory
            // 
            this.pnlReportingCategory.Controls.Add(this.label7);
            this.pnlReportingCategory.Controls.Add(this.cmbReportingCategory);
            this.pnlReportingCategory.Controls.Add(this.btnClearReportingCriteria);
            this.pnlReportingCategory.Controls.Add(this.btnBrowseReportingCriteria);
            this.pnlReportingCategory.Location = new System.Drawing.Point(776, 33);
            this.pnlReportingCategory.Name = "pnlReportingCategory";
            this.pnlReportingCategory.Size = new System.Drawing.Size(381, 22);
            this.pnlReportingCategory.TabIndex = 243;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 14);
            this.label7.TabIndex = 185;
            this.label7.Text = "Reporting Category :";
            // 
            // cmbReportingCategory
            // 
            this.cmbReportingCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportingCategory.FormattingEnabled = true;
            this.cmbReportingCategory.Location = new System.Drawing.Point(131, 0);
            this.cmbReportingCategory.Name = "cmbReportingCategory";
            this.cmbReportingCategory.Size = new System.Drawing.Size(189, 22);
            this.cmbReportingCategory.TabIndex = 184;
            // 
            // btnClearReportingCriteria
            // 
            this.btnClearReportingCriteria.BackColor = System.Drawing.Color.Transparent;
            this.btnClearReportingCriteria.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearReportingCriteria.BackgroundImage")));
            this.btnClearReportingCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearReportingCriteria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearReportingCriteria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReportingCriteria.Image = ((System.Drawing.Image)(resources.GetObject("btnClearReportingCriteria.Image")));
            this.btnClearReportingCriteria.Location = new System.Drawing.Point(351, 0);
            this.btnClearReportingCriteria.Name = "btnClearReportingCriteria";
            this.btnClearReportingCriteria.Size = new System.Drawing.Size(22, 22);
            this.btnClearReportingCriteria.TabIndex = 187;
            this.btnClearReportingCriteria.UseVisualStyleBackColor = false;
            this.btnClearReportingCriteria.Click += new System.EventHandler(this.btnClearReportingCriteria_Click);
            // 
            // btnBrowseReportingCriteria
            // 
            this.btnBrowseReportingCriteria.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseReportingCriteria.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseReportingCriteria.BackgroundImage")));
            this.btnBrowseReportingCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseReportingCriteria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseReportingCriteria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseReportingCriteria.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseReportingCriteria.Image")));
            this.btnBrowseReportingCriteria.Location = new System.Drawing.Point(324, 0);
            this.btnBrowseReportingCriteria.Name = "btnBrowseReportingCriteria";
            this.btnBrowseReportingCriteria.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseReportingCriteria.TabIndex = 186;
            this.btnBrowseReportingCriteria.UseVisualStyleBackColor = false;
            this.btnBrowseReportingCriteria.Click += new System.EventHandler(this.btnBrowseReportingCriteria_Click);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.label10);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Location = new System.Drawing.Point(454, 33);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(314, 22);
            this.pnlProvider.TabIndex = 242;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 14);
            this.label10.TabIndex = 185;
            this.label10.Text = "Provider :";
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(282, 0);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(63, 0);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(189, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(256, 0);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // grInsurance
            // 
            this.grInsurance.Controls.Add(this.rbtn_ReportingCategory);
            this.grInsurance.Controls.Add(this.rbtn_InsCompany);
            this.grInsurance.Location = new System.Drawing.Point(776, -1);
            this.grInsurance.Name = "grInsurance";
            this.grInsurance.Size = new System.Drawing.Size(328, 30);
            this.grInsurance.TabIndex = 2;
            this.grInsurance.TabStop = false;
            // 
            // rbtn_ReportingCategory
            // 
            this.rbtn_ReportingCategory.AutoSize = true;
            this.rbtn_ReportingCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_ReportingCategory.Location = new System.Drawing.Point(177, 10);
            this.rbtn_ReportingCategory.Name = "rbtn_ReportingCategory";
            this.rbtn_ReportingCategory.Size = new System.Drawing.Size(131, 18);
            this.rbtn_ReportingCategory.TabIndex = 1;
            this.rbtn_ReportingCategory.Text = "Reporting Category";
            this.rbtn_ReportingCategory.UseVisualStyleBackColor = true;
            this.rbtn_ReportingCategory.CheckedChanged += new System.EventHandler(this.rbtn_ReportingCategory_CheckedChanged);
            // 
            // rbtn_InsCompany
            // 
            this.rbtn_InsCompany.AutoSize = true;
            this.rbtn_InsCompany.Checked = true;
            this.rbtn_InsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_InsCompany.Location = new System.Drawing.Point(12, 10);
            this.rbtn_InsCompany.Name = "rbtn_InsCompany";
            this.rbtn_InsCompany.Size = new System.Drawing.Size(146, 18);
            this.rbtn_InsCompany.TabIndex = 0;
            this.rbtn_InsCompany.TabStop = true;
            this.rbtn_InsCompany.Text = "Insurance Company";
            this.rbtn_InsCompany.UseVisualStyleBackColor = true;
            this.rbtn_InsCompany.CheckedChanged += new System.EventHandler(this.rbtn_InsCompany_CheckedChanged);
            // 
            // chkPhone
            // 
            this.chkPhone.AutoSize = true;
            this.chkPhone.Enabled = false;
            this.chkPhone.Location = new System.Drawing.Point(517, 64);
            this.chkPhone.Name = "chkPhone";
            this.chkPhone.Size = new System.Drawing.Size(157, 18);
            this.chkPhone.TabIndex = 244;
            this.chkPhone.Text = "Display Patient Phone #";
            this.chkPhone.UseVisualStyleBackColor = true;
            // 
            // chkUncloseAct
            // 
            this.chkUncloseAct.AutoSize = true;
            this.chkUncloseAct.Location = new System.Drawing.Point(517, 89);
            this.chkUncloseAct.Name = "chkUncloseAct";
            this.chkUncloseAct.Size = new System.Drawing.Size(164, 18);
            this.chkUncloseAct.TabIndex = 245;
            this.chkUncloseAct.Text = "Include unclosed activity ";
            this.chkUncloseAct.UseVisualStyleBackColor = true;
            this.chkUncloseAct.Visible = false;
            this.chkUncloseAct.CheckedChanged += new System.EventHandler(this.chkUncloseAct_CheckedChanged);
            // 
            // txtPatBal
            // 
            this.txtPatBal.Location = new System.Drawing.Point(1021, 62);
            this.txtPatBal.MaxLength = 9;
            this.txtPatBal.Name = "txtPatBal";
            this.txtPatBal.Size = new System.Drawing.Size(75, 22);
            this.txtPatBal.TabIndex = 246;
            this.txtPatBal.Text = "0.00";
            this.txtPatBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPatBal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatBal_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(765, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 14);
            this.label6.TabIndex = 247;
            this.label6.Text = "Include patients with Balances greater than :";
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCriteria.Controls.Add(this.chkPatientDetail);
            this.pnlCriteria.Controls.Add(this.label8);
            this.pnlCriteria.Controls.Add(this.txtAgingDays);
            this.pnlCriteria.Controls.Add(this.label6);
            this.pnlCriteria.Controls.Add(this.txtPatBal);
            this.pnlCriteria.Controls.Add(this.chkUncloseAct);
            this.pnlCriteria.Controls.Add(this.chkPhone);
            this.pnlCriteria.Controls.Add(this.pnlProvider);
            this.pnlCriteria.Controls.Add(this.pnlReportingCategory);
            this.pnlCriteria.Controls.Add(this.pnlFacility);
            this.pnlCriteria.Controls.Add(this.pnlInsuranceCompany);
            this.pnlCriteria.Controls.Add(this.lblBreakBy);
            this.pnlCriteria.Controls.Add(this.cmbBreakBy);
            this.pnlCriteria.Controls.Add(this.dtpEndDate);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.grpAppStatus);
            this.pnlCriteria.Controls.Add(this.groupBox1);
            this.pnlCriteria.Controls.Add(this.groupBox3);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.label3);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.grInsurance);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 84);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria.Size = new System.Drawing.Size(1188, 144);
            this.pnlCriteria.TabIndex = 30;
            // 
            // chkPatientDetail
            // 
            this.chkPatientDetail.AutoSize = true;
            this.chkPatientDetail.Enabled = false;
            this.chkPatientDetail.Location = new System.Drawing.Point(517, 114);
            this.chkPatientDetail.Name = "chkPatientDetail";
            this.chkPatientDetail.Size = new System.Drawing.Size(176, 18);
            this.chkPatientDetail.TabIndex = 250;
            this.chkPatientDetail.Text = "Display each patient charge";
            this.chkPatientDetail.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(751, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(268, 14);
            this.label8.TabIndex = 249;
            this.label8.Text = "Include patients with Aging Days greater than :";
            // 
            // txtAgingDays
            // 
            this.txtAgingDays.Location = new System.Drawing.Point(1021, 87);
            this.txtAgingDays.MaxLength = 3;
            this.txtAgingDays.Name = "txtAgingDays";
            this.txtAgingDays.Size = new System.Drawing.Size(74, 22);
            this.txtAgingDays.TabIndex = 248;
            this.txtAgingDays.Text = "0";
            this.txtAgingDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAgingDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAgingDays_KeyPress);
            // 
            // frmRpt_AgingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1188, 655);
            this.Controls.Add(this.pnlReportViewer);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_AgingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aging ";
            this.Load += new System.EventHandler(this.frmRpt_AgingReport_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_AgingReport_FormClosed);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlReportViewer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpAppStatus.ResumeLayout(false);
            this.grpAppStatus.PerformLayout();
            this.pnlInsuranceCompany.ResumeLayout(false);
            this.pnlInsuranceCompany.PerformLayout();
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.pnlReportingCategory.ResumeLayout(false);
            this.pnlReportingCategory.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.grInsurance.ResumeLayout(false);
            this.grInsurance.PerformLayout();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        private System.Windows.Forms.ToolStripButton tsb_btnGenerateBatch;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlReportViewer;
        internal System.Windows.Forms.Panel Panel8;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label27;
        private System.Windows.Forms.Label lblbtnDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtn_DateOfResponsibility;
        private System.Windows.Forms.RadioButton rbtn_DateOfService;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_AgingDetails;
        private System.Windows.Forms.RadioButton rbtn_AgingSummary;
        private System.Windows.Forms.GroupBox grpAppStatus;
        private System.Windows.Forms.RadioButton rbtn_Both;
        private System.Windows.Forms.RadioButton rbtn_PatientDue;
        private System.Windows.Forms.RadioButton rbtn_InsurancePending;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cmbBreakBy;
        private System.Windows.Forms.Label lblBreakBy;
        private System.Windows.Forms.Panel pnlInsuranceCompany;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.ComboBox cmbInsuranceCompany;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.Panel pnlFacility;
        private System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Button btnClearFacility;
        internal System.Windows.Forms.Button btnBrowseFacility;
        private System.Windows.Forms.Label lblFacility;
        private System.Windows.Forms.Panel pnlReportingCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbReportingCategory;
        internal System.Windows.Forms.Button btnClearReportingCriteria;
        internal System.Windows.Forms.Button btnBrowseReportingCriteria;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btnClearProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.GroupBox grInsurance;
        private System.Windows.Forms.RadioButton rbtn_ReportingCategory;
        private System.Windows.Forms.RadioButton rbtn_InsCompany;
        private System.Windows.Forms.CheckBox chkPhone;
        private System.Windows.Forms.CheckBox chkUncloseAct;
        private System.Windows.Forms.TextBox txtPatBal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAgingDays;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
        private System.Windows.Forms.CheckBox chkPatientDetail;
    }
}