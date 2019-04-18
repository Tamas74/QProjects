using gloCommunity.UserControls;
namespace gloCommunity.Forms
{
    partial class gloCommunityViewData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloCommunityViewData));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_ExportTemplate = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_gloCommunityUpload = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuMainUploadgloCommunity = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainUploadGlobalRepository = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_gloCommunityDownload = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlToolStrip.SuspendLayout();
            this.tls_ExportTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.tls_ExportTemplate);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1119, 54);
            this.pnlToolStrip.TabIndex = 14;
            // 
            // tls_ExportTemplate
            // 
            this.tls_ExportTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_ExportTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_ExportTemplate.BackgroundImage")));
            this.tls_ExportTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_ExportTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ExportTemplate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_ExportTemplate.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_ExportTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_gloCommunityUpload,
            this.ts_gloCommunityDownload,
            this.ts_btnClose});
            this.tls_ExportTemplate.Location = new System.Drawing.Point(0, 0);
            this.tls_ExportTemplate.Name = "tls_ExportTemplate";
            this.tls_ExportTemplate.Size = new System.Drawing.Size(1119, 53);
            this.tls_ExportTemplate.TabIndex = 0;
            this.tls_ExportTemplate.Text = "ToolStrip1";
            // 
            // ts_gloCommunityUpload
            // 
            this.ts_gloCommunityUpload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainUploadgloCommunity,
            this.mnuMainUploadGlobalRepository});
            this.ts_gloCommunityUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_gloCommunityUpload.Image = ((System.Drawing.Image)(resources.GetObject("ts_gloCommunityUpload.Image")));
            this.ts_gloCommunityUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_gloCommunityUpload.Name = "ts_gloCommunityUpload";
            this.ts_gloCommunityUpload.Size = new System.Drawing.Size(158, 50);
            this.ts_gloCommunityUpload.Tag = "Share Point";
            this.ts_gloCommunityUpload.Text = "gloCommunity Upload";
            this.ts_gloCommunityUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // mnuMainUploadgloCommunity
            // 
            this.mnuMainUploadgloCommunity.BackColor = System.Drawing.Color.Transparent;
            this.mnuMainUploadgloCommunity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mnuMainUploadgloCommunity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuMainUploadgloCommunity.Image = ((System.Drawing.Image)(resources.GetObject("mnuMainUploadgloCommunity.Image")));
            this.mnuMainUploadgloCommunity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuMainUploadgloCommunity.Name = "mnuMainUploadgloCommunity";
            this.mnuMainUploadgloCommunity.Size = new System.Drawing.Size(257, 22);
            this.mnuMainUploadgloCommunity.Text = "Upload to Practice Repository";
            this.mnuMainUploadgloCommunity.Click += new System.EventHandler(this.mnuMainUploadgloCommunity_Click);
            // 
            // mnuMainUploadGlobalRepository
            // 
            this.mnuMainUploadGlobalRepository.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuMainUploadGlobalRepository.Image = ((System.Drawing.Image)(resources.GetObject("mnuMainUploadGlobalRepository.Image")));
            this.mnuMainUploadGlobalRepository.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuMainUploadGlobalRepository.Name = "mnuMainUploadGlobalRepository";
            this.mnuMainUploadGlobalRepository.Size = new System.Drawing.Size(257, 22);
            this.mnuMainUploadGlobalRepository.Text = "Upload to Global Repository";
            this.mnuMainUploadGlobalRepository.Click += new System.EventHandler(this.mnuMainUploadGlobalRepository_Click);
            // 
            // ts_gloCommunityDownload
            // 
            this.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_gloCommunityDownload.Image = ((System.Drawing.Image)(resources.GetObject("ts_gloCommunityDownload.Image")));
            this.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload";
            this.ts_gloCommunityDownload.Size = new System.Drawing.Size(166, 50);
            this.ts_gloCommunityDownload.Tag = "Close";
            this.ts_gloCommunityDownload.Text = "&gloCommunity Download";
            this.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_gloCommunityDownload.Click += new System.EventHandler(this.ts_gloCommunityDownload_ButtonClick);
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
            // gloCommunityViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1119, 815);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "gloCommunityViewData";
            this.Text = "gloCommunity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.gloCommunityViewData_FormClosing);
            this.Load += new System.EventHandler(this.gloCommunityViewData_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_ExportTemplate.ResumeLayout(false);
            this.tls_ExportTemplate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

       
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_ExportTemplate;
        internal System.Windows.Forms.ToolStripSplitButton ts_gloCommunityUpload;
        internal System.Windows.Forms.ToolStripMenuItem mnuMainUploadgloCommunity;
        internal System.Windows.Forms.ToolStripMenuItem mnuMainUploadGlobalRepository;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.ToolStripButton ts_gloCommunityDownload;
    }
}