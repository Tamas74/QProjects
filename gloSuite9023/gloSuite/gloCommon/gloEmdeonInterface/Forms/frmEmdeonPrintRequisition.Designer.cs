namespace gloEmdeonInterface.Forms
{
    partial class frmEmdeonPrintRequisition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmdeonPrintRequisition));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.pictureBoxWait = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowserEmdeon = new System.Windows.Forms.WebBrowser();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.pnlBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1220, 54);
            this.pnlToolStrip.TabIndex = 30;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackColor = System.Drawing.Color.Transparent;
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_Close});
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(1220, 53);
            this.ts_LabMain.TabIndex = 0;
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Close.Click += new System.EventHandler(this.tlbbtn_Close_Click);
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Controls.Add(this.pictureBoxWait);
            this.pnlBrowser.Controls.Add(this.label5);
            this.pnlBrowser.Controls.Add(this.label4);
            this.pnlBrowser.Controls.Add(this.label2);
            this.pnlBrowser.Controls.Add(this.label1);
            this.pnlBrowser.Controls.Add(this.webBrowserEmdeon);
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 54);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.pnlBrowser.Size = new System.Drawing.Size(1220, 708);
            this.pnlBrowser.TabIndex = 31;
            // 
            // pictureBoxWait
            // 
            this.pictureBoxWait.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWait.Image")));
            this.pictureBoxWait.Location = new System.Drawing.Point(681, 332);
            this.pictureBoxWait.Name = "pictureBoxWait";
            this.pictureBoxWait.Size = new System.Drawing.Size(34, 34);
            this.pictureBoxWait.TabIndex = 6;
            this.pictureBoxWait.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1216, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 700);
            this.label5.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 700);
            this.label4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 704);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1214, 1);
            this.label2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1214, 1);
            this.label1.TabIndex = 1;
            // 
            // webBrowserEmdeon
            // 
            this.webBrowserEmdeon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserEmdeon.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserEmdeon.Location = new System.Drawing.Point(3, 3);
            this.webBrowserEmdeon.MinimumSize = new System.Drawing.Size(27, 24);
            this.webBrowserEmdeon.Name = "webBrowserEmdeon";
            this.webBrowserEmdeon.ScriptErrorsSuppressed = true;
            this.webBrowserEmdeon.Size = new System.Drawing.Size(1214, 702);
            this.webBrowserEmdeon.TabIndex = 0;
            this.webBrowserEmdeon.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // frmEmdeonPrintRequisition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1220, 762);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmdeonPrintRequisition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Requisition";
            this.Load += new System.EventHandler(this.frmEmdeonPrintRequisition_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmdeonPrintRequisition_FormClosing);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlBrowser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.PictureBox pictureBoxWait;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowserEmdeon;

    }
}