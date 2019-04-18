namespace gloBilling
{
    partial class frmImportRVUSchedule
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
                    if (dlgBrowseFile != null)
                    {

                        dlgBrowseFile.Dispose();
                        dlgBrowseFile = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportRVUSchedule));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlImport = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbPEType = new System.Windows.Forms.ComboBox();
            this.lblPEType = new System.Windows.Forms.Label();
            this.lblPETypeStar = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtImportFile = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.dlgBrowseFile = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.pnlImport.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(494, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Import,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(494, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Import
            // 
            this.tsb_Import.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Import.Image")));
            this.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(54, 50);
            this.tsb_Import.Tag = "Import";
            this.tsb_Import.Text = "&Import";
            this.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
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
            // pnlImport
            // 
            this.pnlImport.Controls.Add(this.panel1);
            this.pnlImport.Controls.Add(this.panel3);
            this.pnlImport.Controls.Add(this.label3);
            this.pnlImport.Controls.Add(this.label2);
            this.pnlImport.Controls.Add(this.label1);
            this.pnlImport.Controls.Add(this.label59);
            this.pnlImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImport.Location = new System.Drawing.Point(0, 54);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Padding = new System.Windows.Forms.Padding(3);
            this.pnlImport.Size = new System.Drawing.Size(494, 99);
            this.pnlImport.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbPEType);
            this.panel1.Controls.Add(this.lblPEType);
            this.panel1.Controls.Add(this.lblPETypeStar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 33);
            this.panel1.TabIndex = 118;
            // 
            // cmbPEType
            // 
            this.cmbPEType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPEType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPEType.ForeColor = System.Drawing.Color.Black;
            this.cmbPEType.FormattingEnabled = true;
            this.cmbPEType.Location = new System.Drawing.Point(170, 4);
            this.cmbPEType.Name = "cmbPEType";
            this.cmbPEType.Size = new System.Drawing.Size(276, 22);
            this.cmbPEType.TabIndex = 112;
            // 
            // lblPEType
            // 
            this.lblPEType.AutoSize = true;
            this.lblPEType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPEType.Location = new System.Drawing.Point(28, 8);
            this.lblPEType.Name = "lblPEType";
            this.lblPEType.Size = new System.Drawing.Size(140, 14);
            this.lblPEType.TabIndex = 113;
            this.lblPEType.Text = "Practice Expense Type :";
            // 
            // lblPETypeStar
            // 
            this.lblPETypeStar.AutoEllipsis = true;
            this.lblPETypeStar.AutoSize = true;
            this.lblPETypeStar.BackColor = System.Drawing.Color.Transparent;
            this.lblPETypeStar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPETypeStar.ForeColor = System.Drawing.Color.Red;
            this.lblPETypeStar.Location = new System.Drawing.Point(15, 7);
            this.lblPETypeStar.Name = "lblPETypeStar";
            this.lblPETypeStar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPETypeStar.Size = new System.Drawing.Size(14, 14);
            this.lblPETypeStar.TabIndex = 114;
            this.lblPETypeStar.Text = "*";
            this.lblPETypeStar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtImportFile);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.rbStandard);
            this.panel3.Controls.Add(this.btn_Browse);
            this.panel3.Controls.Add(this.rbCustom);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(486, 58);
            this.panel3.TabIndex = 119;
            // 
            // txtImportFile
            // 
            this.txtImportFile.BackColor = System.Drawing.Color.White;
            this.txtImportFile.ForeColor = System.Drawing.Color.Black;
            this.txtImportFile.Location = new System.Drawing.Point(171, 33);
            this.txtImportFile.Name = "txtImportFile";
            this.txtImportFile.ReadOnly = true;
            this.txtImportFile.Size = new System.Drawing.Size(275, 22);
            this.txtImportFile.TabIndex = 100;
            this.txtImportFile.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(89, 37);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 111;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Location = new System.Drawing.Point(313, 9);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(148, 18);
            this.rbStandard.TabIndex = 0;
            this.rbStandard.Text = "gloStream Layout .xlsx";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);
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
            this.btn_Browse.Location = new System.Drawing.Point(450, 32);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(22, 22);
            this.btn_Browse.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_Browse, "Browse File");
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Checked = true;
            this.rbCustom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rbCustom.Location = new System.Drawing.Point(171, 9);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(137, 18);
            this.rbCustom.TabIndex = 1;
            this.rbCustom.TabStop = true;
            this.rbCustom.Text = "CMS PPRRVU .xlsx";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 33;
            this.label4.Text = "File Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 14);
            this.label6.TabIndex = 117;
            this.label6.Text = "Data File Type :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(486, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(486, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(490, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 93);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 93);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // dlgBrowseFile
            // 
            this.dlgBrowseFile.FileName = "openFileDialog1";
            // 
            // frmImportRVUSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(494, 153);
            this.Controls.Add(this.pnlImport);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportRVUSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New RVU Schedule from CMS File";
            this.Load += new System.EventHandler(this.frmImportRVUSchedule_Load);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlImport.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox txtImportFile;
        private System.Windows.Forms.OpenFileDialog dlgBrowseFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblPETypeStar;
        internal System.Windows.Forms.Label lblPEType;
        private System.Windows.Forms.ComboBox cmbPEType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}