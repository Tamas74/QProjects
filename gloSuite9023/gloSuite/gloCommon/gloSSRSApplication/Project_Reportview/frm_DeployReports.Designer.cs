namespace SSRSApplication
{
    partial class frm_DeployReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DeployReports));
            this.pbReport = new System.Windows.Forms.ProgressBar();
            this.lstReport = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btn_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.tls_btn_Deploy = new System.Windows.Forms.ToolStripButton();
            this.tls_btn_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnllstReport = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_pbReport = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnllstReport.SuspendLayout();
            this.pnl_pbReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbReport
            // 
            this.pbReport.Location = new System.Drawing.Point(11, 9);
            this.pbReport.Name = "pbReport";
            this.pbReport.Size = new System.Drawing.Size(584, 15);
            this.pbReport.TabIndex = 4;
            // 
            // lstReport
            // 
            this.lstReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstReport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lstReport.FullRowSelect = true;
            this.lstReport.GridLines = true;
            this.lstReport.Location = new System.Drawing.Point(3, 3);
            this.lstReport.Name = "lstReport";
            this.lstReport.Size = new System.Drawing.Size(601, 397);
            this.lstReport.TabIndex = 5;
            this.lstReport.UseCompatibleStateImageBehavior = false;
            this.lstReport.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 53);
            this.panel1.TabIndex = 8;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btn_OpenFile,
            this.tls_btn_Deploy,
            this.tls_btn_Cancel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(607, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tls_btn_OpenFile
            // 
            this.tls_btn_OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_OpenFile.Image")));
            this.tls_btn_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_OpenFile.Name = "tls_btn_OpenFile";
            this.tls_btn_OpenFile.Size = new System.Drawing.Size(66, 50);
            this.tls_btn_OpenFile.Text = "Open File";
            this.tls_btn_OpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_OpenFile.Click += new System.EventHandler(this.tls_btn_OpenFile_Click);
            // 
            // tls_btn_Deploy
            // 
            this.tls_btn_Deploy.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_Deploy.Image")));
            this.tls_btn_Deploy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_Deploy.Name = "tls_btn_Deploy";
            this.tls_btn_Deploy.Size = new System.Drawing.Size(53, 50);
            this.tls_btn_Deploy.Text = "Deploy";
            this.tls_btn_Deploy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_Deploy.Click += new System.EventHandler(this.tls_btn_Deploy_Click);
            // 
            // tls_btn_Cancel
            // 
            this.tls_btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_Cancel.Image")));
            this.tls_btn_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_Cancel.Name = "tls_btn_Cancel";
            this.tls_btn_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btn_Cancel.Text = "Close";
            this.tls_btn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_Cancel.Click += new System.EventHandler(this.tls_btn_Cancel_Click);
            // 
            // pnllstReport
            // 
            this.pnllstReport.Controls.Add(this.label4);
            this.pnllstReport.Controls.Add(this.label3);
            this.pnllstReport.Controls.Add(this.label2);
            this.pnllstReport.Controls.Add(this.label1);
            this.pnllstReport.Controls.Add(this.lstReport);
            this.pnllstReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnllstReport.Location = new System.Drawing.Point(0, 53);
            this.pnllstReport.Name = "pnllstReport";
            this.pnllstReport.Padding = new System.Windows.Forms.Padding(3);
            this.pnllstReport.Size = new System.Drawing.Size(607, 403);
            this.pnllstReport.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(603, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 395);
            this.label4.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 395);
            this.label3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 399);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(601, 1);
            this.label2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(601, 1);
            this.label1.TabIndex = 6;
            // 
            // pnl_pbReport
            // 
            this.pnl_pbReport.Controls.Add(this.label5);
            this.pnl_pbReport.Controls.Add(this.pbReport);
            this.pnl_pbReport.Controls.Add(this.label6);
            this.pnl_pbReport.Controls.Add(this.label7);
            this.pnl_pbReport.Controls.Add(this.label8);
            this.pnl_pbReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_pbReport.Location = new System.Drawing.Point(0, 456);
            this.pnl_pbReport.Name = "pnl_pbReport";
            this.pnl_pbReport.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_pbReport.Size = new System.Drawing.Size(607, 34);
            this.pnl_pbReport.TabIndex = 10;
            this.pnl_pbReport.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(603, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 29);
            this.label5.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 29);
            this.label6.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(601, 1);
            this.label7.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(601, 1);
            this.label8.TabIndex = 6;
            // 
            // frm_DeployReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(607, 490);
            this.Controls.Add(this.pnllstReport);
            this.Controls.Add(this.pnl_pbReport);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DeployReports";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deploy SSRS Reports";
            this.Load += new System.EventHandler(this.frm_DeployReports_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnllstReport.ResumeLayout(false);
            this.pnl_pbReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbReport;
        private System.Windows.Forms.ListView lstReport;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tls_btn_OpenFile;
        private System.Windows.Forms.ToolStripButton tls_btn_Deploy;
        private System.Windows.Forms.ToolStripButton tls_btn_Cancel;
        private System.Windows.Forms.Panel pnllstReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_pbReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}