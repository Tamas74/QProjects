namespace gloEmdeonInterface.Forms
{
    partial class frmEmdeonInterface
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
                try
                {
                    if (_orderTimer != null)
                    {
                        _orderTimer.Elapsed -= new System.Timers.ElapsedEventHandler(Ordertimer_Elapsed);
                        _orderTimer.Dispose();
                        _orderTimer = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oPatientStrip != null)
                    {
                        oPatientStrip.Dispose();
                        oPatientStrip = null;
                    }
                }
                catch
                {
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmdeonInterface));
            this.webBrowserEmdeon = new System.Windows.Forms.WebBrowser();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.pnlregistration = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblProcessInformation = new System.Windows.Forms.Label();
            this.pictureBoxWait = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.tmr_session = new System.Windows.Forms.Timer(this.components);
            this.pnlTaskControl = new System.Windows.Forms.Panel();
            this.pnlChkCPOE = new System.Windows.Forms.Panel();
            this.chkCPOE = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBrowser.SuspendLayout();
            this.pnlregistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlChkCPOE.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserEmdeon
            // 
            this.webBrowserEmdeon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserEmdeon.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserEmdeon.Location = new System.Drawing.Point(3, 0);
            this.webBrowserEmdeon.MinimumSize = new System.Drawing.Size(23, 22);
            this.webBrowserEmdeon.Name = "webBrowserEmdeon";
            this.webBrowserEmdeon.ScriptErrorsSuppressed = true;
            this.webBrowserEmdeon.Size = new System.Drawing.Size(1193, 506);
            this.webBrowserEmdeon.TabIndex = 0;
            this.webBrowserEmdeon.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowserEmdeon.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserEmdeon_Navigated);
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Controls.Add(this.pnlregistration);
            this.pnlBrowser.Controls.Add(this.pictureBoxWait);
            this.pnlBrowser.Controls.Add(this.label5);
            this.pnlBrowser.Controls.Add(this.label4);
            this.pnlBrowser.Controls.Add(this.label2);
            this.pnlBrowser.Controls.Add(this.label1);
            this.pnlBrowser.Controls.Add(this.webBrowserEmdeon);
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 196);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBrowser.Size = new System.Drawing.Size(1199, 509);
            this.pnlBrowser.TabIndex = 4;
            // 
            // pnlregistration
            // 
            this.pnlregistration.BackColor = System.Drawing.Color.White;
            this.pnlregistration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlregistration.BackgroundImage")));
            this.pnlregistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlregistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlregistration.Controls.Add(this.lblPleaseWait);
            this.pnlregistration.Controls.Add(this.lblProcessInformation);
            this.pnlregistration.Location = new System.Drawing.Point(388, 231);
            this.pnlregistration.Name = "pnlregistration";
            this.pnlregistration.Size = new System.Drawing.Size(423, 80);
            this.pnlregistration.TabIndex = 7;
            this.pnlregistration.Visible = false;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(20, 11);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(119, 19);
            this.lblPleaseWait.TabIndex = 0;
            this.lblPleaseWait.Text = "Please wait...";
            // 
            // lblProcessInformation
            // 
            this.lblProcessInformation.AutoSize = true;
            this.lblProcessInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblProcessInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProcessInformation.Location = new System.Drawing.Point(21, 42);
            this.lblProcessInformation.Name = "lblProcessInformation";
            this.lblProcessInformation.Size = new System.Drawing.Size(174, 14);
            this.lblProcessInformation.TabIndex = 0;
            this.lblProcessInformation.Text = "Request is being processed";
            // 
            // pictureBoxWait
            // 
            this.pictureBoxWait.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWait.Image")));
            this.pictureBoxWait.Location = new System.Drawing.Point(583, 307);
            this.pictureBoxWait.Name = "pictureBoxWait";
            this.pictureBoxWait.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxWait.TabIndex = 6;
            this.pictureBoxWait.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1195, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 504);
            this.label5.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 504);
            this.label4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 505);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1193, 1);
            this.label2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1193, 1);
            this.label1.TabIndex = 1;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.toolStrip1);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1199, 56);
            this.pnlToolStrip.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.tlbbtn_Print,
            this.tlbbtn_Close});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1199, 53);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Top_ItemClicked);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton2.Tag = "SaveAndClose";
            this.toolStripButton2.Text = "&Save&&Cls";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.ToolTipText = "Save and Close";
            // 
            // tlbbtn_Print
            // 
            this.tlbbtn_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Print.Image")));
            this.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Print.Name = "tlbbtn_Print";
            this.tlbbtn_Print.Size = new System.Drawing.Size(41, 50);
            this.tlbbtn_Print.Tag = "print";
            this.tlbbtn_Print.Text = "&Print";
            this.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Print.ToolTipText = "Print";
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Tag = "close";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Close.ToolTipText = "Close";
            // 
            // tmr_session
            // 
            this.tmr_session.Interval = 60000;
            this.tmr_session.Tick += new System.EventHandler(this.tmr_session_Tick);
            // 
            // pnlTaskControl
            // 
            this.pnlTaskControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTaskControl.Location = new System.Drawing.Point(0, 56);
            this.pnlTaskControl.Name = "pnlTaskControl";
            this.pnlTaskControl.Size = new System.Drawing.Size(1199, 106);
            this.pnlTaskControl.TabIndex = 9;
            this.pnlTaskControl.Visible = false;
            // 
            // pnlChkCPOE
            // 
            this.pnlChkCPOE.Controls.Add(this.chkCPOE);
            this.pnlChkCPOE.Controls.Add(this.label8);
            this.pnlChkCPOE.Controls.Add(this.label7);
            this.pnlChkCPOE.Controls.Add(this.label6);
            this.pnlChkCPOE.Controls.Add(this.label3);
            this.pnlChkCPOE.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChkCPOE.Location = new System.Drawing.Point(0, 162);
            this.pnlChkCPOE.Name = "pnlChkCPOE";
            this.pnlChkCPOE.Padding = new System.Windows.Forms.Padding(3);
            this.pnlChkCPOE.Size = new System.Drawing.Size(1199, 34);
            this.pnlChkCPOE.TabIndex = 10;
            // 
            // chkCPOE
            // 
            this.chkCPOE.AutoSize = true;
            this.chkCPOE.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkCPOE.Location = new System.Drawing.Point(1064, 4);
            this.chkCPOE.Name = "chkCPOE";
            this.chkCPOE.Size = new System.Drawing.Size(131, 26);
            this.chkCPOE.TabIndex = 0;
            this.chkCPOE.Text = "Order Not CPOE    ";
            this.chkCPOE.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(4, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1191, 1);
            this.label8.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1191, 1);
            this.label7.TabIndex = 48;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(1195, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 28);
            this.label6.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 28);
            this.label3.TabIndex = 46;
            // 
            // frmEmdeonInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1199, 705);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.pnlChkCPOE);
            this.Controls.Add(this.pnlTaskControl);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmdeonInterface";
            this.ShowInTaskbar = false;
            this.Text = "gloLab Interface";
            this.Activated += new System.EventHandler(this.frmEmdeonInterface_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmdeonInterface_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEmdeonInterface_FormClosed);
            this.Load += new System.EventHandler(this.frmEmdeonInterface_Load);
            this.pnlBrowser.ResumeLayout(false);
            this.pnlregistration.ResumeLayout(false);
            this.pnlregistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlChkCPOE.ResumeLayout(false);
            this.pnlChkCPOE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserEmdeon;
        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        internal System.Windows.Forms.ToolStripButton toolStripButton2;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxWait;
        private System.Windows.Forms.Timer tmr_session;
        private System.Windows.Forms.Panel pnlTaskControl;
        private System.Windows.Forms.Panel pnlregistration;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.Label lblProcessInformation;
        private System.Windows.Forms.Panel pnlChkCPOE;
        private System.Windows.Forms.CheckBox chkCPOE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Print;
    }
}