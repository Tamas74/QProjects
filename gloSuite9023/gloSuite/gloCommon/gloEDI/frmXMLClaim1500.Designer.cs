namespace gloEDI
{
    partial class frmXMLClaim1500
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
                try
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXMLClaim1500));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_HCFA1500 = new System.Windows.Forms.ToolStripButton();
            this.tsb_Send = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.txtXMLReport = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtEDIData = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1145, 53);
            this.panel2.TabIndex = 8;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_HCFA1500,
            this.tsb_Send,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1145, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tls_HCFA1500
            // 
            this.tls_HCFA1500.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_HCFA1500.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_HCFA1500.Image = ((System.Drawing.Image)(resources.GetObject("tls_HCFA1500.Image")));
            this.tls_HCFA1500.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_HCFA1500.Name = "tls_HCFA1500";
            this.tls_HCFA1500.Size = new System.Drawing.Size(52, 50);
            this.tls_HCFA1500.Tag = "H1500";
            this.tls_HCFA1500.Text = "&H1500";
            this.tls_HCFA1500.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_HCFA1500.ToolTipText = "Print";
            this.tls_HCFA1500.Visible = false;
            this.tls_HCFA1500.Click += new System.EventHandler(this.tls_HCFA1500_Click);
            // 
            // tsb_Send
            // 
            this.tsb_Send.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send.Image")));
            this.tsb_Send.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Send.Name = "tsb_Send";
            this.tsb_Send.Size = new System.Drawing.Size(46, 50);
            this.tsb_Send.Tag = "Scrub";
            this.tsb_Send.Text = "&Scrub";
            this.tsb_Send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Send.ToolTipText = "Scrub";
            this.tsb_Send.Click += new System.EventHandler(this.tsb_Send_Click);
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
            this.tsb_Cancel.ToolTipText = "Close";
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlResult);
            this.pnlMain.Controls.Add(this.txtEDIData);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1145, 755);
            this.pnlMain.TabIndex = 9;
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.txtXMLReport);
            this.pnlResult.Controls.Add(this.lblResult);
            this.pnlResult.Location = new System.Drawing.Point(45, 688);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(1073, 55);
            this.pnlResult.TabIndex = 3;
            this.pnlResult.Visible = false;
            // 
            // txtXMLReport
            // 
            this.txtXMLReport.Location = new System.Drawing.Point(54, 17);
            this.txtXMLReport.Multiline = true;
            this.txtXMLReport.Name = "txtXMLReport";
            this.txtXMLReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXMLReport.Size = new System.Drawing.Size(1016, 20);
            this.txtXMLReport.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(5, 20);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(43, 13);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "Result :";
            // 
            // txtEDIData
            // 
            this.txtEDIData.Location = new System.Drawing.Point(45, 6);
            this.txtEDIData.Multiline = true;
            this.txtEDIData.Name = "txtEDIData";
            this.txtEDIData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEDIData.Size = new System.Drawing.Size(1073, 676);
            this.txtEDIData.TabIndex = 0;
            // 
            // frmXMLClaim1500
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1145, 808);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Name = "frmXMLClaim1500";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XML HCFA 1500";
            this.Load += new System.EventHandler(this.frmXMLClaim1500_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlResult.ResumeLayout(false);
            this.pnlResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Send;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtEDIData;
        internal System.Windows.Forms.ToolStripButton tls_HCFA1500;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.TextBox txtXMLReport;
        private System.Windows.Forms.Label lblResult;
    }
}