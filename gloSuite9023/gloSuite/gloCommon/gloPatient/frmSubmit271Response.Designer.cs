namespace gloPatient
{
    partial class frmSubmit271Response
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
                    if (dlg271ResponseBrowser != null)
                    {
                        
                        dlg271ResponseBrowser.Dispose();
                        dlg271ResponseBrowser = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubmit271Response));
            this.btnRemoveResponseFile = new System.Windows.Forms.Button();
            this.btnBrowseResponseFile = new System.Windows.Forms.Button();
            this.txtResponseFilePath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.dlg271ResponseBrowser = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_MergePatientRecords = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbtnCheckEligibility = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCheckBatchEligibility = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_MergePatientRecords.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemoveResponseFile
            // 
            this.btnRemoveResponseFile.AutoEllipsis = true;
            this.btnRemoveResponseFile.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveResponseFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveResponseFile.BackgroundImage")));
            this.btnRemoveResponseFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveResponseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveResponseFile.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveResponseFile.Image")));
            this.btnRemoveResponseFile.Location = new System.Drawing.Point(411, 57);
            this.btnRemoveResponseFile.Name = "btnRemoveResponseFile";
            this.btnRemoveResponseFile.Size = new System.Drawing.Size(21, 21);
            this.btnRemoveResponseFile.TabIndex = 45;
            this.btnRemoveResponseFile.UseVisualStyleBackColor = false;
            this.btnRemoveResponseFile.Click += new System.EventHandler(this.btnRemoveResponseFile_Click);
            // 
            // btnBrowseResponseFile
            // 
            this.btnBrowseResponseFile.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseResponseFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseResponseFile.BackgroundImage")));
            this.btnBrowseResponseFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseResponseFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseResponseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseResponseFile.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseResponseFile.Image")));
            this.btnBrowseResponseFile.Location = new System.Drawing.Point(386, 57);
            this.btnBrowseResponseFile.Name = "btnBrowseResponseFile";
            this.btnBrowseResponseFile.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseResponseFile.TabIndex = 44;
            this.btnBrowseResponseFile.UseVisualStyleBackColor = false;
            this.btnBrowseResponseFile.Click += new System.EventHandler(this.btnBrowseResponseFile_Click);
            // 
            // txtResponseFilePath
            // 
            this.txtResponseFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResponseFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtResponseFilePath.Location = new System.Drawing.Point(12, 57);
            this.txtResponseFilePath.Name = "txtResponseFilePath";
            this.txtResponseFilePath.Size = new System.Drawing.Size(370, 22);
            this.txtResponseFilePath.TabIndex = 46;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(13, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(211, 14);
            this.Label1.TabIndex = 47;
            this.Label1.Text = "Select 271 Response file to Submit : ";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlg271ResponseBrowser
            // 
            this.dlg271ResponseBrowser.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtResponseFilePath);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.btnBrowseResponseFile);
            this.panel1.Controls.Add(this.btnRemoveResponseFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(446, 102);
            this.panel1.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(442, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 94);
            this.label3.TabIndex = 51;
            this.label3.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 94);
            this.label12.TabIndex = 50;
            this.label12.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(3, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(440, 1);
            this.label2.TabIndex = 49;
            this.label2.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(440, 1);
            this.label15.TabIndex = 48;
            this.label15.Text = "label2";
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_MergePatientRecords);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(446, 54);
            this.pnl_tlsp.TabIndex = 49;
            // 
            // tlsp_MergePatientRecords
            // 
            this.tlsp_MergePatientRecords.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_MergePatientRecords.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.tlsp_MergePatientRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_MergePatientRecords.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_MergePatientRecords.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_MergePatientRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCheckEligibility,
            this.tsbtnCheckBatchEligibility,
            this.tsb_Close});
            this.tlsp_MergePatientRecords.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_MergePatientRecords.Location = new System.Drawing.Point(0, 0);
            this.tlsp_MergePatientRecords.Name = "tlsp_MergePatientRecords";
            this.tlsp_MergePatientRecords.Padding = new System.Windows.Forms.Padding(0);
            this.tlsp_MergePatientRecords.Size = new System.Drawing.Size(446, 53);
            this.tlsp_MergePatientRecords.TabIndex = 5;
            this.tlsp_MergePatientRecords.Text = "toolStrip1";
            // 
            // tsbtnCheckEligibility
            // 
            this.tsbtnCheckEligibility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnCheckEligibility.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCheckEligibility.Image")));
            this.tsbtnCheckEligibility.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCheckEligibility.Name = "tsbtnCheckEligibility";
            this.tsbtnCheckEligibility.Size = new System.Drawing.Size(105, 50);
            this.tsbtnCheckEligibility.Tag = "Merge";
            this.tsbtnCheckEligibility.Text = "Check &Eligibility";
            this.tsbtnCheckEligibility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnCheckEligibility.Click += new System.EventHandler(this.tsbtnCheckEligibility_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // tsbtnCheckBatchEligibility
            // 
            this.tsbtnCheckBatchEligibility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnCheckBatchEligibility.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCheckBatchEligibility.Image")));
            this.tsbtnCheckBatchEligibility.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCheckBatchEligibility.Name = "tsbtnCheckBatchEligibility";
            this.tsbtnCheckBatchEligibility.Size = new System.Drawing.Size(144, 50);
            this.tsbtnCheckBatchEligibility.Tag = "Merge";
            this.tsbtnCheckBatchEligibility.Text = "Check &Batch Eligibility";
            this.tsbtnCheckBatchEligibility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnCheckBatchEligibility.Click += new System.EventHandler(this.tsbtnCheckBatchEligibility_Click);
            // 
            // frmSubmit271Response
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(446, 156);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlsp);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSubmit271Response";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Submit 271 Response";
            this.Load += new System.EventHandler(this.frmSubmit271Response_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_MergePatientRecords.ResumeLayout(false);
            this.tlsp_MergePatientRecords.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveResponseFile;
        private System.Windows.Forms.Button btnBrowseResponseFile;
        private System.Windows.Forms.TextBox txtResponseFilePath;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.OpenFileDialog dlg271ResponseBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MergePatientRecords;
        private System.Windows.Forms.ToolStripButton tsbtnCheckEligibility;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripButton tsbtnCheckBatchEligibility;
    }
}