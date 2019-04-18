namespace gloReports
{
    partial class frmRpt_FinancialSummary
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_FinancialSummary));
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
            this.grpFinancial = new System.Windows.Forms.GroupBox();
            this.rbtn_FinDetails = new System.Windows.Forms.RadioButton();
            this.rbtn_FinSummary = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbBreakBy = new System.Windows.Forms.ComboBox();
            this.lblBreakBy = new System.Windows.Forms.Label();
            this.pnlFacility = new System.Windows.Forms.Panel();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.btnClearFacility = new System.Windows.Forms.Button();
            this.btnBrowseFacility = new System.Windows.Forms.Button();
            this.lblFacility = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.chkIncludeSubTotal = new System.Windows.Forms.CheckBox();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.crvReportViewerBackend = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlReportViewer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.grpFinancial.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.pnlProvider.SuspendLayout();
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
            this.pnlReportViewer.Location = new System.Drawing.Point(0, 195);
            this.pnlReportViewer.Name = "pnlReportViewer";
            this.pnlReportViewer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlReportViewer.Size = new System.Drawing.Size(1188, 460);
            this.pnlReportViewer.TabIndex = 31;
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.CausesValidation = false;
            this.crvReportViewer.DisplayBackgroundEdge = false;
            this.crvReportViewer.DisplayGroupTree = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.Location = new System.Drawing.Point(3, 0);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(1182, 457);
            this.crvReportViewer.TabIndex = 30;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            this.crvReportViewer.Visible = false;
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
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 108);
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
            this.label3.Size = new System.Drawing.Size(1, 108);
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
            this.label5.Location = new System.Drawing.Point(4, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1180, 1);
            this.label5.TabIndex = 239;
            this.label5.Text = "label1";
            // 
            // grpFinancial
            // 
            this.grpFinancial.Controls.Add(this.rbtn_FinDetails);
            this.grpFinancial.Controls.Add(this.rbtn_FinSummary);
            this.grpFinancial.Location = new System.Drawing.Point(14, 63);
            this.grpFinancial.Name = "grpFinancial";
            this.grpFinancial.Size = new System.Drawing.Size(363, 34);
            this.grpFinancial.TabIndex = 2;
            this.grpFinancial.TabStop = false;
            // 
            // rbtn_FinDetails
            // 
            this.rbtn_FinDetails.AutoSize = true;
            this.rbtn_FinDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_FinDetails.Location = new System.Drawing.Point(171, 11);
            this.rbtn_FinDetails.Name = "rbtn_FinDetails";
            this.rbtn_FinDetails.Size = new System.Drawing.Size(108, 18);
            this.rbtn_FinDetails.TabIndex = 1;
            this.rbtn_FinDetails.Text = "Financial Details";
            this.rbtn_FinDetails.UseVisualStyleBackColor = true;
            this.rbtn_FinDetails.CheckedChanged += new System.EventHandler(this.rbtn_AgingDetails_CheckedChanged);
            // 
            // rbtn_FinSummary
            // 
            this.rbtn_FinSummary.AutoSize = true;
            this.rbtn_FinSummary.Checked = true;
            this.rbtn_FinSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_FinSummary.Location = new System.Drawing.Point(14, 11);
            this.rbtn_FinSummary.Name = "rbtn_FinSummary";
            this.rbtn_FinSummary.Size = new System.Drawing.Size(137, 18);
            this.rbtn_FinSummary.TabIndex = 0;
            this.rbtn_FinSummary.TabStop = true;
            this.rbtn_FinSummary.Text = "Financial Summary";
            this.rbtn_FinSummary.UseVisualStyleBackColor = true;
            this.rbtn_FinSummary.CheckedChanged += new System.EventHandler(this.rbtn_AgingSummary_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 235;
            this.label1.Text = "Start Date :";
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
            this.dtpStartDate.Location = new System.Drawing.Point(89, 9);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(98, 22);
            this.dtpStartDate.TabIndex = 236;
            // 
            // cmbBreakBy
            // 
            this.cmbBreakBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBreakBy.FormattingEnabled = true;
            this.cmbBreakBy.Location = new System.Drawing.Point(274, 10);
            this.cmbBreakBy.Name = "cmbBreakBy";
            this.cmbBreakBy.Size = new System.Drawing.Size(159, 22);
            this.cmbBreakBy.TabIndex = 236;
            // 
            // lblBreakBy
            // 
            this.lblBreakBy.AutoSize = true;
            this.lblBreakBy.Location = new System.Drawing.Point(209, 14);
            this.lblBreakBy.Name = "lblBreakBy";
            this.lblBreakBy.Size = new System.Drawing.Size(62, 14);
            this.lblBreakBy.TabIndex = 235;
            this.lblBreakBy.Text = "Break By :";
            // 
            // pnlFacility
            // 
            this.pnlFacility.Controls.Add(this.cmbFacility);
            this.pnlFacility.Controls.Add(this.btnClearFacility);
            this.pnlFacility.Controls.Add(this.btnBrowseFacility);
            this.pnlFacility.Controls.Add(this.lblFacility);
            this.pnlFacility.Location = new System.Drawing.Point(454, 14);
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
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.label10);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Location = new System.Drawing.Point(454, 44);
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
            // chkIncludeSubTotal
            // 
            this.chkIncludeSubTotal.AutoSize = true;
            this.chkIncludeSubTotal.Location = new System.Drawing.Point(517, 76);
            this.chkIncludeSubTotal.Name = "chkIncludeSubTotal";
            this.chkIncludeSubTotal.Size = new System.Drawing.Size(153, 18);
            this.chkIncludeSubTotal.TabIndex = 244;
            this.chkIncludeSubTotal.Text = "Include Daily Sub-totals";
            this.chkIncludeSubTotal.UseVisualStyleBackColor = true;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCriteria.Controls.Add(this.crvReportViewerBackend);
            this.pnlCriteria.Controls.Add(this.dtpEndDate);
            this.pnlCriteria.Controls.Add(this.label6);
            this.pnlCriteria.Controls.Add(this.chkIncludeSubTotal);
            this.pnlCriteria.Controls.Add(this.pnlProvider);
            this.pnlCriteria.Controls.Add(this.pnlFacility);
            this.pnlCriteria.Controls.Add(this.lblBreakBy);
            this.pnlCriteria.Controls.Add(this.cmbBreakBy);
            this.pnlCriteria.Controls.Add(this.dtpStartDate);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.grpFinancial);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.label3);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 84);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria.Size = new System.Drawing.Size(1188, 111);
            this.pnlCriteria.TabIndex = 30;
            // 
            // crvReportViewerBackend
            // 
            this.crvReportViewerBackend.ActiveViewIndex = -1;
            this.crvReportViewerBackend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewerBackend.Location = new System.Drawing.Point(967, 9);
            this.crvReportViewerBackend.Name = "crvReportViewerBackend";
            this.crvReportViewerBackend.SelectionFormula = "";
            this.crvReportViewerBackend.Size = new System.Drawing.Size(150, 59);
            this.crvReportViewerBackend.TabIndex = 247;
            this.crvReportViewerBackend.ViewTimeSelectionFormula = "";
            this.crvReportViewerBackend.Visible = false;
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
            this.dtpEndDate.Location = new System.Drawing.Point(89, 35);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(98, 22);
            this.dtpEndDate.TabIndex = 246;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 245;
            this.label6.Text = "End Date :";
            // 
            // frmRpt_FinancialSummary
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
            this.Name = "frmRpt_FinancialSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Summary ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_FinancialSummary_FormClosed);
            this.Load += new System.EventHandler(this.frmRpt_FinancialSummary_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlReportViewer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            this.grpFinancial.ResumeLayout(false);
            this.grpFinancial.PerformLayout();
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
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
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
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
        private System.Windows.Forms.GroupBox grpFinancial;
        private System.Windows.Forms.RadioButton rbtn_FinDetails;
        private System.Windows.Forms.RadioButton rbtn_FinSummary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbBreakBy;
        private System.Windows.Forms.Label lblBreakBy;
        private System.Windows.Forms.Panel pnlFacility;
        private System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Button btnClearFacility;
        internal System.Windows.Forms.Button btnBrowseFacility;
        private System.Windows.Forms.Label lblFacility;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btnClearProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.CheckBox chkIncludeSubTotal;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label6;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewerBackend;
    }
}