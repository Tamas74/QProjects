namespace gloBilling.Collections
{
    partial class frmInsurance_Claim_Followup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsurance_Claim_Followup));
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnExit = new System.Windows.Forms.ToolStripButton();
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlSSRSViewer = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsBottomBrd = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Top.SuspendLayout();
            this.pnlSSRSViewer.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.SuspendLayout();
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
            this.tls_Top.Size = new System.Drawing.Size(945, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.TabStop = true;
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
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(3, 4);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.Size = new System.Drawing.Size(939, 588);
            this.SSRSViewer.TabIndex = 15969;
            // 
            // pnlSSRSViewer
            // 
            this.pnlSSRSViewer.Controls.Add(this.label3);
            this.pnlSSRSViewer.Controls.Add(this.label2);
            this.pnlSSRSViewer.Controls.Add(this.SSRSViewer);
            this.pnlSSRSViewer.Controls.Add(this.label1);
            this.pnlSSRSViewer.Controls.Add(this.lbl_pnlPaymentDetailsBottomBrd);
            this.pnlSSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSSRSViewer.Location = new System.Drawing.Point(0, 54);
            this.pnlSSRSViewer.Name = "pnlSSRSViewer";
            this.pnlSSRSViewer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSSRSViewer.Size = new System.Drawing.Size(945, 596);
            this.pnlSSRSViewer.TabIndex = 15970;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(941, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 588);
            this.label3.TabIndex = 123;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 588);
            this.label2.TabIndex = 122;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(939, 1);
            this.label1.TabIndex = 121;
            this.label1.Text = "label2";
            // 
            // lbl_pnlPaymentDetailsBottomBrd
            // 
            this.lbl_pnlPaymentDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlPaymentDetailsBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsBottomBrd.Location = new System.Drawing.Point(3, 592);
            this.lbl_pnlPaymentDetailsBottomBrd.Name = "lbl_pnlPaymentDetailsBottomBrd";
            this.lbl_pnlPaymentDetailsBottomBrd.Size = new System.Drawing.Size(939, 1);
            this.lbl_pnlPaymentDetailsBottomBrd.TabIndex = 120;
            this.lbl_pnlPaymentDetailsBottomBrd.Text = "label2";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.tls_Top);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(945, 54);
            this.pnlToolstrip.TabIndex = 15971;
            // 
            // frmInsurance_Claim_Followup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(945, 650);
            this.Controls.Add(this.pnlSSRSViewer);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsurance_Claim_Followup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Claim Follow-up Reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInsurance_Claim_Followup_Load);
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlSSRSViewer.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnExit;
        public Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;
        private System.Windows.Forms.Panel pnlSSRSViewer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsBottomBrd;
        private System.Windows.Forms.Panel pnlToolstrip;
    }
}