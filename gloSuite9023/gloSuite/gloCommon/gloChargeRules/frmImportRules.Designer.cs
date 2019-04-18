namespace ChargeRules
{
    partial class frmImportRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportRules));
            this.pnlImport = new System.Windows.Forms.Panel();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.rbtDirectory = new System.Windows.Forms.RadioButton();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.rbtFile = new System.Windows.Forms.RadioButton();
            this.btnDirectoryPath = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.rtStatus = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_L = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlImport.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImport
            // 
            this.pnlImport.Controls.Add(this.groupBox);
            this.pnlImport.Controls.Add(this.label3);
            this.pnlImport.Controls.Add(this.label2);
            this.pnlImport.Controls.Add(this.label1);
            this.pnlImport.Controls.Add(this.label59);
            this.pnlImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImport.Location = new System.Drawing.Point(0, 53);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Padding = new System.Windows.Forms.Padding(3);
            this.pnlImport.Size = new System.Drawing.Size(575, 140);
            this.pnlImport.TabIndex = 1;
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.groupBox.Controls.Add(this.txtDirectoryPath);
            this.groupBox.Controls.Add(this.rbtDirectory);
            this.groupBox.Controls.Add(this.btnFilePath);
            this.groupBox.Controls.Add(this.rbtFile);
            this.groupBox.Controls.Add(this.btnDirectoryPath);
            this.groupBox.Controls.Add(this.txtFilePath);
            this.groupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox.Location = new System.Drawing.Point(7, 5);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(559, 123);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Import By";
            // 
            // txtDirectoryPath
            // 
            this.txtDirectoryPath.BackColor = System.Drawing.Color.White;
            this.txtDirectoryPath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectoryPath.ForeColor = System.Drawing.Color.Black;
            this.txtDirectoryPath.Location = new System.Drawing.Point(17, 43);
            this.txtDirectoryPath.Name = "txtDirectoryPath";
            this.txtDirectoryPath.ReadOnly = true;
            this.txtDirectoryPath.Size = new System.Drawing.Size(502, 22);
            this.txtDirectoryPath.TabIndex = 1;
            this.txtDirectoryPath.TabStop = false;
            // 
            // rbtDirectory
            // 
            this.rbtDirectory.AutoSize = true;
            this.rbtDirectory.Checked = true;
            this.rbtDirectory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDirectory.Location = new System.Drawing.Point(17, 22);
            this.rbtDirectory.Name = "rbtDirectory";
            this.rbtDirectory.Size = new System.Drawing.Size(114, 18);
            this.rbtDirectory.TabIndex = 0;
            this.rbtDirectory.TabStop = true;
            this.rbtDirectory.Text = "Directory Path";
            this.rbtDirectory.UseVisualStyleBackColor = true;
            this.rbtDirectory.CheckedChanged += new System.EventHandler(this.rbtDirectory_CheckedChanged);
            // 
            // btnFilePath
            // 
            this.btnFilePath.AutoEllipsis = true;
            this.btnFilePath.BackColor = System.Drawing.Color.Transparent;
            this.btnFilePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFilePath.BackgroundImage")));
            this.btnFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFilePath.Enabled = false;
            this.btnFilePath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnFilePath.Image")));
            this.btnFilePath.Location = new System.Drawing.Point(522, 91);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(22, 22);
            this.btnFilePath.TabIndex = 2;
            this.btnFilePath.UseVisualStyleBackColor = false;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // rbtFile
            // 
            this.rbtFile.AutoSize = true;
            this.rbtFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtFile.Location = new System.Drawing.Point(17, 70);
            this.rbtFile.Name = "rbtFile";
            this.rbtFile.Size = new System.Drawing.Size(75, 18);
            this.rbtFile.TabIndex = 1;
            this.rbtFile.TabStop = true;
            this.rbtFile.Text = " File Path";
            this.rbtFile.UseVisualStyleBackColor = true;
            this.rbtFile.CheckedChanged += new System.EventHandler(this.rbtFile_CheckedChanged);
            // 
            // btnDirectoryPath
            // 
            this.btnDirectoryPath.AutoEllipsis = true;
            this.btnDirectoryPath.BackColor = System.Drawing.Color.Transparent;
            this.btnDirectoryPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDirectoryPath.BackgroundImage")));
            this.btnDirectoryPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDirectoryPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDirectoryPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectoryPath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectoryPath.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectoryPath.Image")));
            this.btnDirectoryPath.Location = new System.Drawing.Point(522, 43);
            this.btnDirectoryPath.Name = "btnDirectoryPath";
            this.btnDirectoryPath.Size = new System.Drawing.Size(22, 22);
            this.btnDirectoryPath.TabIndex = 0;
            this.btnDirectoryPath.UseVisualStyleBackColor = false;
            this.btnDirectoryPath.Click += new System.EventHandler(this.btnDirectoryPath_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.Location = new System.Drawing.Point(17, 91);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(502, 22);
            this.txtFilePath.TabIndex = 3;
            this.txtFilePath.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(567, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(567, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(571, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 134);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 134);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.rtStatus);
            this.pnlStatus.Controls.Add(this.label6);
            this.pnlStatus.Controls.Add(this.label7);
            this.pnlStatus.Controls.Add(this.label8);
            this.pnlStatus.Controls.Add(this.lbl_L);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatus.Location = new System.Drawing.Point(0, 193);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlStatus.Size = new System.Drawing.Size(575, 1);
            this.pnlStatus.TabIndex = 112;
            this.pnlStatus.Visible = false;
            // 
            // rtStatus
            // 
            this.rtStatus.BackColor = System.Drawing.Color.White;
            this.rtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtStatus.Location = new System.Drawing.Point(4, 1);
            this.rtStatus.Name = "rtStatus";
            this.rtStatus.ReadOnly = true;
            this.rtStatus.Size = new System.Drawing.Size(567, 0);
            this.rtStatus.TabIndex = 46;
            this.rtStatus.Text = "";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(4, -3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(567, 1);
            this.label6.TabIndex = 45;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(567, 1);
            this.label7.TabIndex = 44;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(571, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 0);
            this.label8.TabIndex = 43;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_L
            // 
            this.lbl_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_L.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Location = new System.Drawing.Point(3, 0);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(1, 0);
            this.lbl_L.TabIndex = 42;
            this.lbl_L.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.AutoSize = true;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(575, 53);
            this.pnltlsStrip.TabIndex = 2;
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
            this.tsb_Import,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(575, 53);
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
            // openFileDialog
            // 
            this.openFileDialog.Filter = "XML Files|*.xml";
            this.openFileDialog.Title = "Import Rule";
            // 
            // frmImportRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(575, 194);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlImport);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportRules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Rules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportRules_FormClosing);
            this.pnlImport.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.RichTextBox rtStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton rbtFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.TextBox txtDirectoryPath;
        private System.Windows.Forms.RadioButton rbtDirectory;
        private System.Windows.Forms.Button btnDirectoryPath;

    }
}