namespace gloSSRSApplication
{
    partial class frmPatientDemographicsViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientDemographicsViewer));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnExit = new System.Windows.Forms.ToolStripButton();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(537, 54);
            this.pnlToolStrip.TabIndex = 18;
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
            this.tls_Top.Size = new System.Drawing.Size(537, 53);
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
            // pnlViewer
            // 
            this.pnlViewer.Controls.Add(this.SSRSViewer);
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 54);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlViewer.Size = new System.Drawing.Size(537, 375);
            this.pnlViewer.TabIndex = 19;
            // 
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(3, 3);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.Size = new System.Drawing.Size(531, 369);
            this.SSRSViewer.TabIndex = 0;
            // 
            // frmPatientDemographicsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 429);
            this.Controls.Add(this.pnlViewer);
            this.Controls.Add(this.pnlToolStrip);
            this.Name = "frmPatientDemographicsViewer";
            this.Text = "frmPatientDemographicsViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPatientDemographicsViewer_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlViewer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnExit;
        private System.Windows.Forms.Panel pnlViewer;
        public Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;


    }
}