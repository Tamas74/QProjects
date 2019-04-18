namespace gloReports
{
    partial class frmEMRSSRSViewer
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



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMRSSRSViewer));
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnExit = new System.Windows.Forms.ToolStripButton();
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.lbl_pnlPaymentDetailsBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsTopBrd = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlPaymentDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(4, 4);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.Size = new System.Drawing.Size(695, 354);
            this.SSRSViewer.TabIndex = 0;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(703, 54);
            this.pnlToolStrip.TabIndex = 17;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnExit});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(703, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnExit
            // 
            this.tls_btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExit.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExit.Image")));
            this.tls_btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExit.Name = "tls_btnExit";
            this.tls_btnExit.Size = new System.Drawing.Size(43, 50);
            this.tls_btnExit.Tag = "Close";
            this.tls_btnExit.Text = "&Close";
            this.tls_btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExit.Click += new System.EventHandler(this.tls_btnExit_Click);
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.Controls.Add(this.SSRSViewer);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsBottomBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsLeftBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsRightBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsTopBrd);
            this.pnlPaymentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(0, 54);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPaymentDetails.Size = new System.Drawing.Size(703, 362);
            this.pnlPaymentDetails.TabIndex = 18;
            // 
            // lbl_pnlPaymentDetailsBottomBrd
            // 
            this.lbl_pnlPaymentDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlPaymentDetailsBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsBottomBrd.Location = new System.Drawing.Point(4, 358);
            this.lbl_pnlPaymentDetailsBottomBrd.Name = "lbl_pnlPaymentDetailsBottomBrd";
            this.lbl_pnlPaymentDetailsBottomBrd.Size = new System.Drawing.Size(695, 1);
            this.lbl_pnlPaymentDetailsBottomBrd.TabIndex = 119;
            this.lbl_pnlPaymentDetailsBottomBrd.Text = "label2";
            // 
            // lbl_pnlPaymentDetailsLeftBrd
            // 
            this.lbl_pnlPaymentDetailsLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlPaymentDetailsLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlPaymentDetailsLeftBrd.Name = "lbl_pnlPaymentDetailsLeftBrd";
            this.lbl_pnlPaymentDetailsLeftBrd.Size = new System.Drawing.Size(1, 355);
            this.lbl_pnlPaymentDetailsLeftBrd.TabIndex = 118;
            this.lbl_pnlPaymentDetailsLeftBrd.Text = "label4";
            // 
            // lbl_pnlPaymentDetailsRightBrd
            // 
            this.lbl_pnlPaymentDetailsRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlPaymentDetailsRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsRightBrd.Location = new System.Drawing.Point(699, 4);
            this.lbl_pnlPaymentDetailsRightBrd.Name = "lbl_pnlPaymentDetailsRightBrd";
            this.lbl_pnlPaymentDetailsRightBrd.Size = new System.Drawing.Size(1, 355);
            this.lbl_pnlPaymentDetailsRightBrd.TabIndex = 117;
            this.lbl_pnlPaymentDetailsRightBrd.Text = "label3";
            // 
            // lbl_pnlPaymentDetailsTopBrd
            // 
            this.lbl_pnlPaymentDetailsTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlPaymentDetailsTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlPaymentDetailsTopBrd.Name = "lbl_pnlPaymentDetailsTopBrd";
            this.lbl_pnlPaymentDetailsTopBrd.Size = new System.Drawing.Size(697, 1);
            this.lbl_pnlPaymentDetailsTopBrd.TabIndex = 116;
            this.lbl_pnlPaymentDetailsTopBrd.Text = "label1";
            // 
            // frmEMRSSRSViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(703, 416);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEMRSSRSViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSSRSViewer_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlPaymentDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnExit;
        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsBottomBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsLeftBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsRightBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsTopBrd;


    }
}