namespace gloPMClaimService
{
    partial class frmDownloadFileList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadFileList));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_DonwloadImport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Download = new System.Windows.Forms.ToolStripButton();
            this.tsb_MoveToRCM = new System.Windows.Forms.ToolStripButton();
            this.tsb_DownloadAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_Select_All = new System.Windows.Forms.ToolStripButton();
            this.tsb_Clear_All = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listServer = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlRCMDOCCategory = new System.Windows.Forms.Panel();
            this.cmbRCMDOCCategory = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listServer)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlRCMDOCCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(566, 54);
            this.pnlToolStrip.TabIndex = 48;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_DonwloadImport,
            this.tsb_Download,
            this.tsb_MoveToRCM,
            this.tsb_DownloadAll,
            this.tsb_Select_All,
            this.tsb_Clear_All,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(566, 53);
            this.ts_Commands.TabIndex = 13;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_DonwloadImport
            // 
            this.tsb_DonwloadImport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DonwloadImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_DonwloadImport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DonwloadImport.Image")));
            this.tsb_DonwloadImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DonwloadImport.Name = "tsb_DonwloadImport";
            this.tsb_DonwloadImport.Size = new System.Drawing.Size(73, 50);
            this.tsb_DonwloadImport.Tag = "Import";
            this.tsb_DonwloadImport.Text = "&Download";
            this.tsb_DonwloadImport.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_DonwloadImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DonwloadImport.ToolTipText = "Download";
            this.tsb_DonwloadImport.Click += new System.EventHandler(this.tsb_DownloadImport_Click);
            // 
            // tsb_Download
            // 
            this.tsb_Download.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Download.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Download.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Download.Image")));
            this.tsb_Download.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Download.Name = "tsb_Download";
            this.tsb_Download.Size = new System.Drawing.Size(134, 50);
            this.tsb_Download.Tag = "OK";
            this.tsb_Download.Text = "&Download to Server";
            this.tsb_Download.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Download.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Download.ToolTipText = "Download to Server";
            this.tsb_Download.Click += new System.EventHandler(this.tsb_Download_Click);
            // 
            // tsb_MoveToRCM
            // 
            this.tsb_MoveToRCM.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MoveToRCM.Image = global::gloBilling.Properties.Resources.Add_Note;
            this.tsb_MoveToRCM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MoveToRCM.Name = "tsb_MoveToRCM";
            this.tsb_MoveToRCM.Size = new System.Drawing.Size(95, 50);
            this.tsb_MoveToRCM.Tag = "MoveToRCM";
            this.tsb_MoveToRCM.Text = "Move To RCM";
            this.tsb_MoveToRCM.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsb_MoveToRCM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MoveToRCM.Click += new System.EventHandler(this.tsb_MoveToRCM_Click);
            // 
            // tsb_DownloadAll
            // 
            this.tsb_DownloadAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DownloadAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_DownloadAll.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DownloadAll.Image")));
            this.tsb_DownloadAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DownloadAll.Name = "tsb_DownloadAll";
            this.tsb_DownloadAll.Size = new System.Drawing.Size(92, 50);
            this.tsb_DownloadAll.Tag = "DownloadAll";
            this.tsb_DownloadAll.Text = "&Download All";
            this.tsb_DownloadAll.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_DownloadAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DownloadAll.ToolTipText = "Download All";
            this.tsb_DownloadAll.Visible = false;
            this.tsb_DownloadAll.Click += new System.EventHandler(this.tsb_DownloadAll_Click);
            // 
            // tsb_Select_All
            // 
            this.tsb_Select_All.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Select_All.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Select_All.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Select_All.Image")));
            this.tsb_Select_All.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Select_All.Name = "tsb_Select_All";
            this.tsb_Select_All.Size = new System.Drawing.Size(67, 50);
            this.tsb_Select_All.Tag = "OK";
            this.tsb_Select_All.Text = "&Select All";
            this.tsb_Select_All.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Select_All.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Select_All.ToolTipText = "Select All";
            this.tsb_Select_All.Click += new System.EventHandler(this.tsb_Select_All_Click);
            // 
            // tsb_Clear_All
            // 
            this.tsb_Clear_All.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Clear_All.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Clear_All.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Clear_All.Image")));
            this.tsb_Clear_All.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Clear_All.Name = "tsb_Clear_All";
            this.tsb_Clear_All.Size = new System.Drawing.Size(60, 50);
            this.tsb_Clear_All.Tag = "OK";
            this.tsb_Clear_All.Text = "&Clear All";
            this.tsb_Clear_All.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Clear_All.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Clear_All.ToolTipText = "Clear All";
            this.tsb_Clear_All.Visible = false;
            this.tsb_Clear_All.Click += new System.EventHandler(this.tsb_Clear_All_Click);
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
            this.tsb_Close.ToolTipText = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listServer);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(566, 368);
            this.panel1.TabIndex = 50;
            // 
            // listServer
            // 
            this.listServer.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.listServer.BackColor = System.Drawing.Color.White;
            this.listServer.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.listServer.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.listServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listServer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listServer.ForeColor = System.Drawing.Color.Black;
            this.listServer.Location = new System.Drawing.Point(4, 1);
            this.listServer.Name = "listServer";
            this.listServer.Rows.Count = 1;
            this.listServer.Rows.DefaultSize = 19;
            this.listServer.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.listServer.ShowCellLabels = true;
            this.listServer.Size = new System.Drawing.Size(558, 363);
            this.listServer.StyleInfo = resources.GetString("listServer.StyleInfo");
            this.listServer.TabIndex = 18;
            this.listServer.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.listServer_BeforeSort);
            this.listServer.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.listServer_AfterSort);
            
            this.listServer.SelChange += new System.EventHandler(this.listServer_SelChange);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 363);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(562, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 363);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(560, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(560, 1);
            this.label1.TabIndex = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 278;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 210;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 210;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlRCMDOCCategory);
            this.panel2.Controls.Add(this.cmbFileType);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(566, 34);
            this.panel2.TabIndex = 51;
            // 
            // pnlRCMDOCCategory
            // 
            this.pnlRCMDOCCategory.Controls.Add(this.cmbRCMDOCCategory);
            this.pnlRCMDOCCategory.Controls.Add(this.label10);
            this.pnlRCMDOCCategory.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRCMDOCCategory.Location = new System.Drawing.Point(232, 4);
            this.pnlRCMDOCCategory.Name = "pnlRCMDOCCategory";
            this.pnlRCMDOCCategory.Size = new System.Drawing.Size(330, 26);
            this.pnlRCMDOCCategory.TabIndex = 19;
            // 
            // cmbRCMDOCCategory
            // 
            this.cmbRCMDOCCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRCMDOCCategory.FormattingEnabled = true;
            this.cmbRCMDOCCategory.Items.AddRange(new object[] {
            "",
            ".txt",
            ".pdf"});
            this.cmbRCMDOCCategory.Location = new System.Drawing.Point(97, 2);
            this.cmbRCMDOCCategory.Name = "cmbRCMDOCCategory";
            this.cmbRCMDOCCategory.Size = new System.Drawing.Size(225, 22);
            this.cmbRCMDOCCategory.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(29, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 14);
            this.label10.TabIndex = 6;
            this.label10.Text = "Category :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbFileType
            // 
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "",
            ".txt",
            ".pdf"});
            this.cmbFileType.Location = new System.Drawing.Point(78, 6);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(128, 22);
            this.cmbFileType.TabIndex = 5;
            this.cmbFileType.SelectedIndexChanged += new System.EventHandler(this.cmbFileType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(9, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "File Type : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 26);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(562, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 26);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(560, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(560, 1);
            this.label8.TabIndex = 0;
            // 
            // frmDownloadFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(566, 456);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownloadFileList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download ERA Files";
            this.Load += new System.EventHandler(this.frmDownloadFileList_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listServer)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlRCMDOCCategory.ResumeLayout(false);
            this.pnlRCMDOCCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.ToolStripButton tsb_Download;
        internal System.Windows.Forms.ToolStripButton tsb_DownloadAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        internal System.Windows.Forms.ToolStripButton tsb_DonwloadImport;
        internal System.Windows.Forms.ToolStripButton tsb_Select_All;
        internal System.Windows.Forms.ToolStripButton tsb_Clear_All;
        internal C1.Win.C1FlexGrid.C1FlexGrid listServer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton tsb_MoveToRCM;
        private System.Windows.Forms.Panel pnlRCMDOCCategory;
        private System.Windows.Forms.ComboBox cmbRCMDOCCategory;
        private System.Windows.Forms.Label label10;

    }
}