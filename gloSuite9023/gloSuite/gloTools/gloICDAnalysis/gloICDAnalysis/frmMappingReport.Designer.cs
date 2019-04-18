namespace gloICDAnalysis
{
    partial class frmMappingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMappingReport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rptICDMapping = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlPleasewait = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlCDuedate = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlPleasewait.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rptICDMapping);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(734, 656);
            this.panel1.TabIndex = 4;
            // 
            // rptICDMapping
            // 
            this.rptICDMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptICDMapping.LocalReport.ReportEmbeddedResource = "gloICDAnalysis.Reports.rptICD10Mapping.rdlc";
            this.rptICDMapping.Location = new System.Drawing.Point(4, 4);
            this.rptICDMapping.Name = "rptICDMapping";
            this.rptICDMapping.Size = new System.Drawing.Size(726, 648);
            this.rptICDMapping.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(730, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 648);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 648);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 652);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(728, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(728, 1);
            this.label1.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip.BackgroundImage")));
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.tsb_Close});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(734, 53);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "ToolStrip";
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Text = "Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(734, 56);
            this.panel2.TabIndex = 8;
            // 
            // pnlPleasewait
            // 
            this.pnlPleasewait.Controls.Add(this.label45);
            this.pnlPleasewait.Controls.Add(this.label44);
            this.pnlPleasewait.Controls.Add(this.label43);
            this.pnlPleasewait.Controls.Add(this.label42);
            this.pnlPleasewait.Controls.Add(this.label41);
            this.pnlPleasewait.Controls.Add(this.label25);
            this.pnlPleasewait.Controls.Add(this.pnlCDuedate);
            this.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPleasewait.Location = new System.Drawing.Point(0, 56);
            this.pnlPleasewait.Name = "pnlPleasewait";
            this.pnlPleasewait.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPleasewait.Size = new System.Drawing.Size(734, 656);
            this.pnlPleasewait.TabIndex = 35;
            this.pnlPleasewait.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.White;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Baskerville Old Face", 48F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(4, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(726, 654);
            this.label45.TabIndex = 238;
            this.label45.Text = "Please wait...";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(3, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 654);
            this.label44.TabIndex = 36;
            this.label44.Text = "label44";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(730, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 654);
            this.label43.TabIndex = 35;
            this.label43.Text = "label43";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(3, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(728, 1);
            this.label42.TabIndex = 34;
            this.label42.Text = "label42";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 655);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(728, 1);
            this.label41.TabIndex = 33;
            this.label41.Text = "label41";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(52, 90);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(108, 14);
            this.label25.TabIndex = 234;
            this.label25.Text = "Transaction Date :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label25.Visible = false;
            // 
            // pnlCDuedate
            // 
            this.pnlCDuedate.Location = new System.Drawing.Point(615, 57);
            this.pnlCDuedate.Name = "pnlCDuedate";
            this.pnlCDuedate.Size = new System.Drawing.Size(418, 51);
            this.pnlCDuedate.TabIndex = 236;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(41, 50);
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmMappingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(734, 712);
            this.Controls.Add(this.pnlPleasewait);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMappingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICD9 to ICD10 Mapping Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMappingReport_FormClosing);
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.panel1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlPleasewait.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlPleasewait;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel pnlCDuedate;
        private Microsoft.Reporting.WinForms.ReportViewer rptICDMapping;
        internal System.Windows.Forms.ToolStripButton btnPrint;


    }
}