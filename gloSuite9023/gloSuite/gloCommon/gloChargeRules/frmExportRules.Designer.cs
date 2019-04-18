namespace ChargeRules
{
    partial class frmExportRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportRules));
            this.pnlImport = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRules = new System.Windows.Forms.CheckedListBox();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Export = new System.Windows.Forms.ToolStripButton();
            this.tsb_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlImport.SuspendLayout();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImport
            // 
            this.pnlImport.Controls.Add(this.txtSearch);
            this.pnlImport.Controls.Add(this.label3);
            this.pnlImport.Controls.Add(this.label5);
            this.pnlImport.Controls.Add(this.label2);
            this.pnlImport.Controls.Add(this.chkRules);
            this.pnlImport.Controls.Add(this.txtFolderPath);
            this.pnlImport.Controls.Add(this.label1);
            this.pnlImport.Controls.Add(this.label19);
            this.pnlImport.Controls.Add(this.label59);
            this.pnlImport.Controls.Add(this.btn_Browse);
            this.pnlImport.Controls.Add(this.label4);
            this.pnlImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImport.Location = new System.Drawing.Point(0, 53);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Padding = new System.Windows.Forms.Padding(3);
            this.pnlImport.Size = new System.Drawing.Size(539, 379);
            this.pnlImport.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(70, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(447, 22);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(531, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Search :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(531, 1);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // chkRules
            // 
            this.chkRules.CheckOnClick = true;
            this.chkRules.FormattingEnabled = true;
            this.chkRules.Location = new System.Drawing.Point(70, 41);
            this.chkRules.Name = "chkRules";
            this.chkRules.Size = new System.Drawing.Size(447, 293);
            this.chkRules.TabIndex = 1;
            this.chkRules.SelectedIndexChanged += new System.EventHandler(this.chkRules_SelectedIndexChanged);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.BackColor = System.Drawing.Color.White;
            this.txtFolderPath.ForeColor = System.Drawing.Color.Black;
            this.txtFolderPath.Location = new System.Drawing.Point(70, 340);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(419, 22);
            this.txtFolderPath.TabIndex = 2;
            this.txtFolderPath.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(535, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 373);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(13, 344);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 0;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 373);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // btn_Browse
            // 
            this.btn_Browse.AutoEllipsis = true;
            this.btn_Browse.BackColor = System.Drawing.Color.Transparent;
            this.btn_Browse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Browse.BackgroundImage")));
            this.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Browse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Browse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Image = ((System.Drawing.Image)(resources.GetObject("btn_Browse.Image")));
            this.btn_Browse.Location = new System.Drawing.Point(493, 339);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(24, 24);
            this.btn_Browse.TabIndex = 3;
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 344);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 33;
            this.label4.Text = "Path :";
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.AutoSize = true;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(539, 53);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_SetupResource.BackgroundImage")));
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Export,
            this.tsb_SelectAll,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(539, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Export
            // 
            this.tsb_Export.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Export.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Export.Image")));
            this.tsb_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Export.Name = "tsb_Export";
            this.tsb_Export.Size = new System.Drawing.Size(52, 50);
            this.tsb_Export.Tag = "Export";
            this.tsb_Export.Text = "&Export";
            this.tsb_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Export.Click += new System.EventHandler(this.tsb_Export_Click);
            // 
            // tsb_SelectAll
            // 
            this.tsb_SelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SelectAll.Image = global::ChargeRules.Properties.Resources.SelectAllRules;
            this.tsb_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectAll.Name = "tsb_SelectAll";
            this.tsb_SelectAll.Size = new System.Drawing.Size(67, 50);
            this.tsb_SelectAll.Tag = "Select All";
            this.tsb_SelectAll.Text = "Select &All";
            this.tsb_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectAll.Click += new System.EventHandler(this.tsb_SelectAll_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Cancel";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // frmExportRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(539, 432);
            this.Controls.Add(this.pnlImport);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportRules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Rules";
            this.Load += new System.EventHandler(this.frmExportRules_Load);
            this.pnlImport.ResumeLayout(false);
            this.pnlImport.PerformLayout();
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlImport;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Export;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckedListBox chkRules;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsb_SelectAll;
    }
}