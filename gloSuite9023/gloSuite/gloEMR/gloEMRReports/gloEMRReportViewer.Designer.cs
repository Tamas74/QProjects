namespace gloEMRReports
{
    partial class gloEMRReportViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloEMRReportViewer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.fpnlCriteria = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAgingCriteria = new System.Windows.Forms.Panel();
            this.lblAgingCriteria = new System.Windows.Forms.Label();
            this.cmbAgingCritera = new System.Windows.Forms.ComboBox();
            this.pnlMedication = new System.Windows.Forms.Panel();
            this.lblMedication = new System.Windows.Forms.Label();
            this.cmbMedication = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.fpnlCriteria.SuspendLayout();
            this.pnlAgingCriteria.SuspendLayout();
            this.pnlMedication.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.crvReportViewer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 176);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1199, 588);
            this.panel1.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 584);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1191, 1);
            this.label1.TabIndex = 208;
            this.label1.Text = "label2";
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.DisplayGroupTree = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.EnableDrillDown = false;
            this.crvReportViewer.Location = new System.Drawing.Point(4, 1);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(1191, 584);
            this.crvReportViewer.TabIndex = 29;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 584);
            this.label2.TabIndex = 207;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(1195, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 584);
            this.label3.TabIndex = 206;
            this.label3.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1193, 1);
            this.label12.TabIndex = 205;
            this.label12.Text = "label1";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1199, 54);
            this.pnlToolStrip.TabIndex = 92;
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
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1199, 53);
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
            // fpnlCriteria
            // 
            this.fpnlCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpnlCriteria.Controls.Add(this.pnlAgingCriteria);
            this.fpnlCriteria.Controls.Add(this.pnlMedication);
            this.fpnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpnlCriteria.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpnlCriteria.Location = new System.Drawing.Point(0, 54);
            this.fpnlCriteria.Name = "fpnlCriteria";
            this.fpnlCriteria.Size = new System.Drawing.Size(1199, 122);
            this.fpnlCriteria.TabIndex = 256;
            // 
            // pnlAgingCriteria
            // 
            this.pnlAgingCriteria.Controls.Add(this.lblAgingCriteria);
            this.pnlAgingCriteria.Controls.Add(this.cmbAgingCritera);
            this.pnlAgingCriteria.Location = new System.Drawing.Point(3, 3);
            this.pnlAgingCriteria.Name = "pnlAgingCriteria";
            this.pnlAgingCriteria.Size = new System.Drawing.Size(195, 29);
            this.pnlAgingCriteria.TabIndex = 218;
            this.pnlAgingCriteria.Visible = false;
            // 
            // lblAgingCriteria
            // 
            this.lblAgingCriteria.AutoSize = true;
            this.lblAgingCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgingCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAgingCriteria.Location = new System.Drawing.Point(3, 7);
            this.lblAgingCriteria.Name = "lblAgingCriteria";
            this.lblAgingCriteria.Size = new System.Drawing.Size(75, 14);
            this.lblAgingCriteria.TabIndex = 235;
            this.lblAgingCriteria.Text = "Select Age :";
            // 
            // cmbAgingCritera
            // 
            this.cmbAgingCritera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgingCritera.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgingCritera.FormattingEnabled = true;
            this.cmbAgingCritera.Location = new System.Drawing.Point(96, 3);
            this.cmbAgingCritera.Name = "cmbAgingCritera";
            this.cmbAgingCritera.Size = new System.Drawing.Size(84, 22);
            this.cmbAgingCritera.TabIndex = 236;
            // 
            // pnlMedication
            // 
            this.pnlMedication.Controls.Add(this.lblMedication);
            this.pnlMedication.Controls.Add(this.cmbMedication);
            this.pnlMedication.Location = new System.Drawing.Point(3, 38);
            this.pnlMedication.Name = "pnlMedication";
            this.pnlMedication.Size = new System.Drawing.Size(294, 29);
            this.pnlMedication.TabIndex = 219;
            this.pnlMedication.Visible = false;
            // 
            // lblMedication
            // 
            this.lblMedication.AutoSize = true;
            this.lblMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblMedication.Location = new System.Drawing.Point(3, 7);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(111, 14);
            this.lblMedication.TabIndex = 235;
            this.lblMedication.Text = "Select Medication :";
            // 
            // cmbMedication
            // 
            this.cmbMedication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMedication.FormattingEnabled = true;
            this.cmbMedication.Location = new System.Drawing.Point(120, 3);
            this.cmbMedication.Name = "cmbMedication";
            this.cmbMedication.Size = new System.Drawing.Size(171, 22);
            this.cmbMedication.TabIndex = 236;
            // 
            // gloEMRReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fpnlCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Name = "gloEMRReportViewer";
            this.Size = new System.Drawing.Size(1199, 764);
            this.Load += new System.EventHandler(this.gloEMRReportViewer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gloEMRReportViewer_Paint);
            this.panel1.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.fpnlCriteria.ResumeLayout(false);
            this.pnlAgingCriteria.ResumeLayout(false);
            this.pnlAgingCriteria.PerformLayout();
            this.pnlMedication.ResumeLayout(false);
            this.pnlMedication.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.FlowLayoutPanel fpnlCriteria;
        private System.Windows.Forms.Panel pnlAgingCriteria;
        private System.Windows.Forms.Label lblAgingCriteria;
        private System.Windows.Forms.ComboBox cmbAgingCritera;
        private System.Windows.Forms.Panel pnlMedication;
        private System.Windows.Forms.Label lblMedication;
        private System.Windows.Forms.ComboBox cmbMedication;

    }
}
