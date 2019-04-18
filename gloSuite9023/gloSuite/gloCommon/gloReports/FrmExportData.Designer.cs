namespace gloReports
{
    partial class FrmExportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportData));
            this.pnlExport = new System.Windows.Forms.Panel();
            this.btnbrwfile = new System.Windows.Forms.Button();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblpageRange = new System.Windows.Forms.Label();
            this.txtPageRange = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rd_CurrentPage = new System.Windows.Forms.RadioButton();
            this.rd_PageRange = new System.Windows.Forms.RadioButton();
            this.rd_SelectAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tblExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.pnlExport.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlExport
            // 
            this.pnlExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlExport.Controls.Add(this.btnbrwfile);
            this.pnlExport.Controls.Add(this.txtFilepath);
            this.pnlExport.Controls.Add(this.label6);
            this.pnlExport.Controls.Add(this.label5);
            this.pnlExport.Controls.Add(this.label4);
            this.pnlExport.Controls.Add(this.label3);
            this.pnlExport.Controls.Add(this.label2);
            this.pnlExport.Controls.Add(this.lblpageRange);
            this.pnlExport.Controls.Add(this.txtPageRange);
            this.pnlExport.Controls.Add(this.label12);
            this.pnlExport.Controls.Add(this.rd_CurrentPage);
            this.pnlExport.Controls.Add(this.rd_PageRange);
            this.pnlExport.Controls.Add(this.rd_SelectAll);
            this.pnlExport.Controls.Add(this.label1);
            this.pnlExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExport.Location = new System.Drawing.Point(0, 53);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Padding = new System.Windows.Forms.Padding(3);
            this.pnlExport.Size = new System.Drawing.Size(492, 180);
            this.pnlExport.TabIndex = 1;
            // 
            // btnbrwfile
            // 
            this.btnbrwfile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnbrwfile.BackgroundImage")));
            this.btnbrwfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnbrwfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnbrwfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnbrwfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnbrwfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbrwfile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbrwfile.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnbrwfile.Image = ((System.Drawing.Image)(resources.GetObject("btnbrwfile.Image")));
            this.btnbrwfile.Location = new System.Drawing.Point(455, 139);
            this.btnbrwfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnbrwfile.Name = "btnbrwfile";
            this.btnbrwfile.Size = new System.Drawing.Size(21, 22);
            this.btnbrwfile.TabIndex = 6;
            this.btnbrwfile.UseVisualStyleBackColor = true;
            this.btnbrwfile.Click += new System.EventHandler(this.btnbrwfile_Click);
            // 
            // txtFilepath
            // 
            this.txtFilepath.BackColor = System.Drawing.Color.White;
            this.txtFilepath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilepath.Location = new System.Drawing.Point(131, 140);
            this.txtFilepath.MaxLength = 50;
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.ReadOnly = true;
            this.txtFilepath.ShortcutsEnabled = false;
            this.txtFilepath.Size = new System.Drawing.Size(318, 22);
            this.txtFilepath.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 14);
            this.label6.TabIndex = 26;
            this.label6.Text = "Select Folder Path :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(4, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(484, 1);
            this.label5.TabIndex = 25;
            this.label5.Text = "Enter Page range to Export \r\neg. 1-3,4,6\r\n";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(488, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 172);
            this.label4.TabIndex = 24;
            this.label4.Text = "Enter Page range to Export \r\neg. 1-3,4,6\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 172);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter Page range to Export \r\neg. 1-3,4,6\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(3, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(486, 1);
            this.label2.TabIndex = 22;
            this.label2.Text = "Enter Page range to Export \r\neg. 1-3,4,6\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // lblpageRange
            // 
            this.lblpageRange.AutoSize = true;
            this.lblpageRange.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpageRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpageRange.Location = new System.Drawing.Point(153, 116);
            this.lblpageRange.Name = "lblpageRange";
            this.lblpageRange.Size = new System.Drawing.Size(231, 14);
            this.lblpageRange.TabIndex = 5;
            this.lblpageRange.Text = "Enter Page Range To Export eg. 1-3,4,6\r\n";
            this.lblpageRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblpageRange.Visible = false;
            // 
            // txtPageRange
            // 
            this.txtPageRange.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtPageRange.Location = new System.Drawing.Point(151, 92);
            this.txtPageRange.MaxLength = 50;
            this.txtPageRange.Name = "txtPageRange";
            this.txtPageRange.ShortcutsEnabled = false;
            this.txtPageRange.Size = new System.Drawing.Size(301, 22);
            this.txtPageRange.TabIndex = 4;
            this.txtPageRange.Visible = false;
            this.txtPageRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageRange_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(11, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 14);
            this.label12.TabIndex = 12;
            this.label12.Text = "Export Pages :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rd_CurrentPage
            // 
            this.rd_CurrentPage.AutoSize = true;
            this.rd_CurrentPage.Checked = true;
            this.rd_CurrentPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_CurrentPage.Location = new System.Drawing.Point(15, 40);
            this.rd_CurrentPage.Name = "rd_CurrentPage";
            this.rd_CurrentPage.Size = new System.Drawing.Size(97, 18);
            this.rd_CurrentPage.TabIndex = 1;
            this.rd_CurrentPage.TabStop = true;
            this.rd_CurrentPage.Text = "Current Page";
            this.rd_CurrentPage.UseVisualStyleBackColor = true;
            this.rd_CurrentPage.CheckedChanged += new System.EventHandler(this.rd_CurrentPage_CheckedChanged);
            // 
            // rd_PageRange
            // 
            this.rd_PageRange.AutoSize = true;
            this.rd_PageRange.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_PageRange.Location = new System.Drawing.Point(15, 95);
            this.rd_PageRange.Name = "rd_PageRange";
            this.rd_PageRange.Size = new System.Drawing.Size(139, 18);
            this.rd_PageRange.TabIndex = 3;
            this.rd_PageRange.TabStop = true;
            this.rd_PageRange.Text = "Choose Pages Range";
            this.rd_PageRange.UseVisualStyleBackColor = true;
            this.rd_PageRange.CheckedChanged += new System.EventHandler(this.rd_PageRange_CheckedChanged);
            // 
            // rd_SelectAll
            // 
            this.rd_SelectAll.AutoSize = true;
            this.rd_SelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_SelectAll.Location = new System.Drawing.Point(15, 67);
            this.rd_SelectAll.Name = "rd_SelectAll";
            this.rd_SelectAll.Size = new System.Drawing.Size(73, 18);
            this.rd_SelectAll.TabIndex = 2;
            this.rd_SelectAll.TabStop = true;
            this.rd_SelectAll.Text = "All Pages";
            this.rd_SelectAll.UseVisualStyleBackColor = true;
            this.rd_SelectAll.CheckedChanged += new System.EventHandler(this.rd_SelectAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(486, 1);
            this.label1.TabIndex = 21;
            this.label1.Text = "Enter Page range to Export \r\neg. 1-3,4,6\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblExport,
            this.toolStripButton6});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(492, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "ToolStrip1";
            // 
            // tblExport
            // 
            this.tblExport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblExport.Image = ((System.Drawing.Image)(resources.GetObject("tblExport.Image")));
            this.tblExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblExport.Name = "tblExport";
            this.tblExport.Size = new System.Drawing.Size(58, 50);
            this.tblExport.Text = " &Export";
            this.tblExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblExport.Click += new System.EventHandler(this.tblExport_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton6.Text = "&Close";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // FrmExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 233);
            this.Controls.Add(this.pnlExport);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Data";
            this.pnlExport.ResumeLayout(false);
            this.pnlExport.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlExport;
        internal System.Windows.Forms.Label lblpageRange;
        private System.Windows.Forms.TextBox txtPageRange;
        internal gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        internal System.Windows.Forms.ToolStripButton tblExport;
        internal System.Windows.Forms.ToolStripButton toolStripButton6;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.RadioButton rd_CurrentPage;
        internal System.Windows.Forms.RadioButton rd_PageRange;
        internal System.Windows.Forms.RadioButton rd_SelectAll;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnbrwfile;
    }
}