namespace gloCommunity.Forms
{
    partial class FrmgloCommunityDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmgloCommunityDashboard));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_gloCommunityDashboard = new gloGlobal.gloToolStripIgnoreFocus();
            this.homeButton = new System.Windows.Forms.ToolStripButton();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.WbgloCommunity = new System.Windows.Forms.WebBrowser();
            this.pnlProcess = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_gloCommunityDashboard.SuspendLayout();
            this.pnlProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.tls_gloCommunityDashboard);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1119, 54);
            this.pnlToolStrip.TabIndex = 15;
            // 
            // tls_gloCommunityDashboard
            // 
            this.tls_gloCommunityDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_gloCommunityDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_gloCommunityDashboard.BackgroundImage")));
            this.tls_gloCommunityDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_gloCommunityDashboard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_gloCommunityDashboard.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_gloCommunityDashboard.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_gloCommunityDashboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeButton,
            this.backButton,
            this.forwardButton,
            this.refreshButton,
            this.ts_btnClose});
            this.tls_gloCommunityDashboard.Location = new System.Drawing.Point(0, 0);
            this.tls_gloCommunityDashboard.Name = "tls_gloCommunityDashboard";
            this.tls_gloCommunityDashboard.Size = new System.Drawing.Size(1119, 53);
            this.tls_gloCommunityDashboard.TabIndex = 0;
            this.tls_gloCommunityDashboard.Text = "ToolStrip1";
            // 
            // homeButton
            // 
            this.homeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.homeButton.Image = ((System.Drawing.Image)(resources.GetObject("homeButton.Image")));
            this.homeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(46, 50);
            this.homeButton.Tag = "Home";
            this.homeButton.Text = "&Home";
            this.homeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // backButton
            // 
            this.backButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(63, 50);
            this.backButton.Tag = "Previous";
            this.backButton.Text = "&Previous";
            this.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(39, 50);
            this.forwardButton.Tag = "Next";
            this.forwardButton.Text = "&Next";
            this.forwardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(58, 50);
            this.refreshButton.Tag = "Refresh";
            this.refreshButton.Text = "&Refresh";
            this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // WbgloCommunity
            // 
            this.WbgloCommunity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WbgloCommunity.Location = new System.Drawing.Point(0, 54);
            this.WbgloCommunity.MinimumSize = new System.Drawing.Size(20, 20);
            this.WbgloCommunity.Name = "WbgloCommunity";
            this.WbgloCommunity.Size = new System.Drawing.Size(1119, 761);
            this.WbgloCommunity.TabIndex = 16;
            this.WbgloCommunity.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WbgloCommunity_DocumentCompleted);
            this.WbgloCommunity.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WbgloCommunity_Navigated);
            this.WbgloCommunity.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WbgloCommunity_Navigating);
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.Color.White;
            this.pnlProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProcess.Controls.Add(this.label4);
            this.pnlProcess.Controls.Add(this.label3);
            this.pnlProcess.Controls.Add(this.label2);
            this.pnlProcess.Controls.Add(this.label1);
            this.pnlProcess.Controls.Add(this.pictureBox1);
            this.pnlProcess.Controls.Add(this.lblProcess);
            this.pnlProcess.Location = new System.Drawing.Point(476, 330);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(167, 154);
            this.pnlProcess.TabIndex = 18;
            this.pnlProcess.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(166, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 152);
            this.label4.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 152);
            this.label3.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(0, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 1);
            this.label2.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 1);
            this.label1.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::gloCommunity.Properties.Resources.Wait;
            this.pictureBox1.Location = new System.Drawing.Point(33, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 98);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // lblProcess
            // 
            this.lblProcess.AutoEllipsis = true;
            this.lblProcess.AutoSize = true;
            this.lblProcess.BackColor = System.Drawing.Color.Transparent;
            this.lblProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblProcess.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcess.Location = new System.Drawing.Point(12, 118);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(143, 23);
            this.lblProcess.TabIndex = 17;
            this.lblProcess.Text = "Please Wait...";
            // 
            // FrmgloCommunityDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1119, 815);
            this.Controls.Add(this.pnlProcess);
            this.Controls.Add(this.WbgloCommunity);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmgloCommunityDashboard";
            this.Text = "gloCommunity Dashboard";
            this.Activated += new System.EventHandler(this.FrmgloCommunityDashboard_Activated);
            this.Deactivate += new System.EventHandler(this.FrmgloCommunityDashboard_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmgloCommunityDashboard_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmgloCommunityDashboard_FormClosed);
            this.Load += new System.EventHandler(this.FrmgloCommunityDashboard_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_gloCommunityDashboard.ResumeLayout(false);
            this.tls_gloCommunityDashboard.PerformLayout();
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_gloCommunityDashboard;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.WebBrowser WbgloCommunity;
        internal System.Windows.Forms.ToolStripButton homeButton;
        internal System.Windows.Forms.ToolStripButton refreshButton;
        internal System.Windows.Forms.ToolStripButton backButton;
        internal System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.Panel pnlProcess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblProcess;      
    }
}