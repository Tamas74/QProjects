namespace gloEmdeonInterface.Forms
{
    partial class frmLabDemonstration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabDemonstration));
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowserEmdeon = new System.Windows.Forms.WebBrowser();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlTaskControl = new System.Windows.Forms.Panel();
            this.pnlBrowser.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Controls.Add(this.label5);
            this.pnlBrowser.Controls.Add(this.label4);
            this.pnlBrowser.Controls.Add(this.label2);
            this.pnlBrowser.Controls.Add(this.label1);
            this.pnlBrowser.Controls.Add(this.webBrowserEmdeon);
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 162);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBrowser.Size = new System.Drawing.Size(1284, 780);
            this.pnlBrowser.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1280, 884);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 0);
            this.label5.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 884);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 0);
            this.label4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 776);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1278, 1);
            this.label2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 883);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1278, 1);
            this.label1.TabIndex = 1;
            // 
            // webBrowserEmdeon
            // 
            this.webBrowserEmdeon.Dock = System.Windows.Forms.DockStyle.Top;
            this.webBrowserEmdeon.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserEmdeon.Location = new System.Drawing.Point(3, 0);
            this.webBrowserEmdeon.MinimumSize = new System.Drawing.Size(27, 24);
            this.webBrowserEmdeon.Name = "webBrowserEmdeon";
            this.webBrowserEmdeon.ScriptErrorsSuppressed = true;
            this.webBrowserEmdeon.Size = new System.Drawing.Size(1278, 883);
            this.webBrowserEmdeon.TabIndex = 0;
            this.webBrowserEmdeon.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.btn_Refresh);
            this.pnlToolStrip.Controls.Add(this.toolStrip1);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1284, 56);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_Refresh.FlatAppearance.BorderSize = 0;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.Image")));
            this.btn_Refresh.Location = new System.Drawing.Point(1251, 15);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(22, 22);
            this.btn_Refresh.TabIndex = 13;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_save,
            this.tlbbtn_Close});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 53);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlbbtn_save
            // 
            this.tlbbtn_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_save.Image")));
            this.tlbbtn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_save.Name = "tlbbtn_save";
            this.tlbbtn_save.Size = new System.Drawing.Size(66, 50);
            this.tlbbtn_save.Tag = "SaveAndClose";
            this.tlbbtn_save.Text = "&Save&&Cls";
            this.tlbbtn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_save.ToolTipText = "Save and Close";
            this.tlbbtn_save.Click += new System.EventHandler(this.tlbbtn_save_Click);
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
            this.tlbbtn_Close.Click += new System.EventHandler(this.tlbbtn_Close_Click);
            // 
            // pnlTaskControl
            // 
            this.pnlTaskControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTaskControl.Location = new System.Drawing.Point(0, 56);
            this.pnlTaskControl.Name = "pnlTaskControl";
            this.pnlTaskControl.Size = new System.Drawing.Size(1284, 106);
            this.pnlTaskControl.TabIndex = 8;
            this.pnlTaskControl.Visible = false;
            // 
            // frmLabDemonstration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 942);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.pnlTaskControl);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLabDemonstration";
            this.Text = "Demo Lab";
            this.Load += new System.EventHandler(this.frmLabDemonstration_Load);
            this.pnlBrowser.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowserEmdeon;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        internal System.Windows.Forms.ToolStripButton tlbbtn_save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Panel pnlTaskControl;
    }
}