namespace gloBilling.gloERA
{
    partial class frmERAFiles
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmERAFiles));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Process = new System.Windows.Forms.ToolStripButton();
            this.tsb_UnProcess = new System.Windows.Forms.ToolStripButton();
            this.tsb_View = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.chkShowUnProcessed = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1ERAFiles = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.panel2.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ERAFiles)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_Main);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1159, 54);
            this.panel2.TabIndex = 20;
            // 
            // tls_Main
            // 
            this.tls_Main.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Process,
            this.tsb_UnProcess,
            this.tsb_View,
            this.tsb_Delete,
            this.tsb_Import,
            this.tsb_Refresh,
            this.tsb_Close});
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(1159, 53);
            this.tls_Main.TabIndex = 0;
            this.tls_Main.Text = "toolStrip1";
            // 
            // tsb_Process
            // 
            this.tsb_Process.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Process.Image")));
            this.tsb_Process.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Process.Name = "tsb_Process";
            this.tsb_Process.Size = new System.Drawing.Size(57, 50);
            this.tsb_Process.Tag = "Process";
            this.tsb_Process.Text = "&Process";
            this.tsb_Process.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Process.Click += new System.EventHandler(this.tsb_Process_Click);
            // 
            // tsb_UnProcess
            // 
            this.tsb_UnProcess.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UnProcess.Image")));
            this.tsb_UnProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UnProcess.Name = "tsb_UnProcess";
            this.tsb_UnProcess.Size = new System.Drawing.Size(78, 50);
            this.tsb_UnProcess.Tag = "ERAFiles";
            this.tsb_UnProcess.Text = "&Un-Process";
            this.tsb_UnProcess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UnProcess.Click += new System.EventHandler(this.tsb_UnProcess_Click);
            // 
            // tsb_View
            // 
            this.tsb_View.Image = ((System.Drawing.Image)(resources.GetObject("tsb_View.Image")));
            this.tsb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_View.Name = "tsb_View";
            this.tsb_View.Size = new System.Drawing.Size(40, 50);
            this.tsb_View.Tag = "View";
            this.tsb_View.Text = "&View";
            this.tsb_View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_View.ToolTipText = "View";
            this.tsb_View.Click += new System.EventHandler(this.tsb_View_Click);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblProgress);
            this.panel6.Controls.Add(this.prgProgress);
            this.panel6.Controls.Add(this.btnClearSearch);
            this.panel6.Controls.Add(this.chkShowUnProcessed);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.txtSearch);
            this.panel6.Controls.Add(this.lblSearch);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1153, 24);
            this.panel6.TabIndex = 22;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProgress.Location = new System.Drawing.Point(182, 1);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(341, 22);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "label23";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgProgress
            // 
            this.prgProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgProgress.Location = new System.Drawing.Point(523, 1);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(450, 22);
            this.prgProgress.TabIndex = 1;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearch.Image")));
            this.btnClearSearch.Location = new System.Drawing.Point(294, 1);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(23, 22);
            this.btnClearSearch.TabIndex = 32;
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Visible = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // chkShowUnProcessed
            // 
            this.chkShowUnProcessed.BackColor = System.Drawing.Color.Transparent;
            this.chkShowUnProcessed.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkShowUnProcessed.Location = new System.Drawing.Point(973, 1);
            this.chkShowUnProcessed.Name = "chkShowUnProcessed";
            this.chkShowUnProcessed.Size = new System.Drawing.Size(179, 22);
            this.chkShowUnProcessed.TabIndex = 29;
            this.chkShowUnProcessed.Text = "Show Un-Processed Only";
            this.chkShowUnProcessed.UseVisualStyleBackColor = false;
            this.chkShowUnProcessed.CheckedChanged += new System.EventHandler(this.chkShowUnProcessed_CheckedChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(289, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(5, 22);
            this.label10.TabIndex = 31;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearch.Location = new System.Drawing.Point(69, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 22);
            this.txtSearch.TabIndex = 30;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearch.Location = new System.Drawing.Point(1, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(68, 22);
            this.lblSearch.TabIndex = 29;
            this.lblSearch.Text = "Search :";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1152, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 22);
            this.label1.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1152, 1);
            this.label3.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1152, 1);
            this.label4.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 24);
            this.label6.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1ERAFiles);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1159, 636);
            this.panel1.TabIndex = 23;
            // 
            // c1ERAFiles
            // 
            this.c1ERAFiles.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ERAFiles.AutoResize = false;
            this.c1ERAFiles.BackColor = System.Drawing.Color.White;
            this.c1ERAFiles.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ERAFiles.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ERAFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ERAFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ERAFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ERAFiles.Location = new System.Drawing.Point(4, 1);
            this.c1ERAFiles.Name = "c1ERAFiles";
            this.c1ERAFiles.Padding = new System.Windows.Forms.Padding(3);
            this.c1ERAFiles.Rows.Count = 1;
            this.c1ERAFiles.Rows.DefaultSize = 19;
            this.c1ERAFiles.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.c1ERAFiles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ERAFiles.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ERAFiles.ShowCellLabels = true;
            this.c1ERAFiles.Size = new System.Drawing.Size(1151, 631);
            this.c1ERAFiles.StyleInfo = resources.GetString("c1ERAFiles.StyleInfo");
            this.c1ERAFiles.TabIndex = 29;
            this.c1ERAFiles.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1ERAFiles_AfterRowColChange);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(1155, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 631);
            this.label2.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(4, 632);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1152, 1);
            this.label5.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1152, 1);
            this.label7.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 633);
            this.label8.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(1159, 30);
            this.panel3.TabIndex = 30;
            // 
            // tsb_Import
            // 
            this.tsb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Import.Image")));
            this.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(54, 50);
            this.tsb_Import.Tag = "Import";
            this.tsb_Import.Text = "&Import";
            this.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Import.ToolTipText = "Import ERA Files";
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
            // 
            // frmERAFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1159, 720);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmERAFiles";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERA Files";
            this.Load += new System.EventHandler(this.frmERAFiles_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmERAFiles_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ERAFiles)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private gloGlobal.gloToolStripIgnoreFocus tls_Main;
        private System.Windows.Forms.ToolStripButton tsb_Process;
        private System.Windows.Forms.ToolStripButton tsb_UnProcess;
        private System.Windows.Forms.ToolStripButton tsb_View;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkShowUnProcessed;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ERAFiles;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ToolStripButton tsb_Import;
    }
}