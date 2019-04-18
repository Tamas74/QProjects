namespace gloBilling
{
    partial class frmBatchEligibilityActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchEligibilityActivity));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbView270File = new System.Windows.Forms.ToolStripButton();
            this.tsbView271File = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMST = new System.Windows.Forms.Panel();
            this.lblClearingHouse = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBatchSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTab = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.C1BatchEligibiltyActivity = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.mnuSetup = new System.Windows.Forms.MenuStrip();
            this.mnuItemAddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSaveClose = new System.Windows.Forms.ToolStripMenuItem();
            this.chkNoResponseFoundStatus = new System.Windows.Forms.CheckBox();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlMST.SuspendLayout();
            this.pnlTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1BatchEligibiltyActivity)).BeginInit();
            this.mnuSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Main);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(794, 54);
            this.pnlToolStrip.TabIndex = 21;
            // 
            // tls_Main
            // 
            this.tls_Main.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbView270File,
            this.tsbView271File,
            this.tsbRefresh,
            this.tsb_SaveClose,
            this.tsb_Close});
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(794, 53);
            this.tls_Main.TabIndex = 0;
            this.tls_Main.Text = "toolStrip1";
            // 
            // tsbView270File
            // 
            this.tsbView270File.Image = global::gloBilling.Properties.Resources.View_270;
            this.tsbView270File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView270File.Name = "tsbView270File";
            this.tsbView270File.Size = new System.Drawing.Size(91, 50);
            this.tsbView270File.Tag = "View 270 File";
            this.tsbView270File.Text = "View 27&0 File";
            this.tsbView270File.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbView270File.ToolTipText = "View 270 File";
            this.tsbView270File.Click += new System.EventHandler(this.tsbView270File_Click);
            // 
            // tsbView271File
            // 
            this.tsbView271File.Image = global::gloBilling.Properties.Resources.View_271;
            this.tsbView271File.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView271File.Name = "tsbView271File";
            this.tsbView271File.Size = new System.Drawing.Size(91, 50);
            this.tsbView271File.Tag = "View 271 File";
            this.tsbView271File.Text = "View 27&1 File";
            this.tsbView271File.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbView271File.ToolTipText = "View 271 File";
            this.tsbView271File.Click += new System.EventHandler(this.tsbView271File_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(58, 50);
            this.tsbRefresh.Tag = "Refresh";
            this.tsbRefresh.Text = "&Refresh";
            this.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRefresh.ToolTipText = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsb_SaveClose
            // 
            this.tsb_SaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveClose.Image")));
            this.tsb_SaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveClose.Name = "tsb_SaveClose";
            this.tsb_SaveClose.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveClose.Tag = "SaveClose";
            this.tsb_SaveClose.Text = "Sa&ve&&Cls";
            this.tsb_SaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveClose.ToolTipText = "Save and Close";
            this.tsb_SaveClose.Visible = false;
            this.tsb_SaveClose.Click += new System.EventHandler(this.tsb_SaveClose_Click);
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
            // pnlMST
            // 
            this.pnlMST.Controls.Add(this.lblClearingHouse);
            this.pnlMST.Controls.Add(this.chkNoResponseFoundStatus);
            this.pnlMST.Controls.Add(this.label5);
            this.pnlMST.Controls.Add(this.label10);
            this.pnlMST.Controls.Add(this.label9);
            this.pnlMST.Controls.Add(this.label4);
            this.pnlMST.Controls.Add(this.label8);
            this.pnlMST.Controls.Add(this.txtBatchSearch);
            this.pnlMST.Controls.Add(this.label1);
            this.pnlMST.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMST.Location = new System.Drawing.Point(0, 54);
            this.pnlMST.Name = "pnlMST";
            this.pnlMST.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMST.Size = new System.Drawing.Size(794, 67);
            this.pnlMST.TabIndex = 0;
            // 
            // lblClearingHouse
            // 
            this.lblClearingHouse.AutoSize = true;
            this.lblClearingHouse.BackColor = System.Drawing.Color.Transparent;
            this.lblClearingHouse.Location = new System.Drawing.Point(114, 12);
            this.lblClearingHouse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClearingHouse.Name = "lblClearingHouse";
            this.lblClearingHouse.Size = new System.Drawing.Size(87, 14);
            this.lblClearingHouse.TabIndex = 1;
            this.lblClearingHouse.Text = "Clearing House";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 14);
            this.label5.TabIndex = 29;
            this.label5.Text = "Clearinghouse :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(4, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(786, 1);
            this.label10.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(4, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(786, 1);
            this.label9.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(790, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 61);
            this.label4.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 61);
            this.label8.TabIndex = 22;
            // 
            // txtBatchSearch
            // 
            this.txtBatchSearch.Location = new System.Drawing.Point(114, 33);
            this.txtBatchSearch.MaxLength = 100;
            this.txtBatchSearch.Name = "txtBatchSearch";
            this.txtBatchSearch.Size = new System.Drawing.Size(188, 22);
            this.txtBatchSearch.TabIndex = 5;
            this.txtBatchSearch.TextChanged += new System.EventHandler(this.txtBatchSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search :";
            // 
            // pnlTab
            // 
            this.pnlTab.Controls.Add(this.label11);
            this.pnlTab.Controls.Add(this.label7);
            this.pnlTab.Controls.Add(this.label6);
            this.pnlTab.Controls.Add(this.label3);
            this.pnlTab.Controls.Add(this.C1BatchEligibiltyActivity);
            this.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab.Location = new System.Drawing.Point(0, 121);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTab.Size = new System.Drawing.Size(794, 451);
            this.pnlTab.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(4, 447);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(786, 1);
            this.label11.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(786, 1);
            this.label7.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(790, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 448);
            this.label6.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 448);
            this.label3.TabIndex = 34;
            // 
            // C1BatchEligibiltyActivity
            // 
            this.C1BatchEligibiltyActivity.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1BatchEligibiltyActivity.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1BatchEligibiltyActivity.BackColor = System.Drawing.Color.White;
            this.C1BatchEligibiltyActivity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1BatchEligibiltyActivity.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1BatchEligibiltyActivity.ColumnInfo = resources.GetString("C1BatchEligibiltyActivity.ColumnInfo");
            this.C1BatchEligibiltyActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1BatchEligibiltyActivity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1BatchEligibiltyActivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1BatchEligibiltyActivity.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1BatchEligibiltyActivity.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1BatchEligibiltyActivity.Location = new System.Drawing.Point(3, 0);
            this.C1BatchEligibiltyActivity.Name = "C1BatchEligibiltyActivity";
            this.C1BatchEligibiltyActivity.Padding = new System.Windows.Forms.Padding(3);
            this.C1BatchEligibiltyActivity.Rows.Count = 1;
            this.C1BatchEligibiltyActivity.Rows.DefaultSize = 19;
            this.C1BatchEligibiltyActivity.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.C1BatchEligibiltyActivity.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1BatchEligibiltyActivity.Size = new System.Drawing.Size(788, 448);
            this.C1BatchEligibiltyActivity.StyleInfo = resources.GetString("C1BatchEligibiltyActivity.StyleInfo");
            this.C1BatchEligibiltyActivity.TabIndex = 33;
            this.C1BatchEligibiltyActivity.TabStop = false;
            // 
            // mnuSetup
            // 
            this.mnuSetup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemAddLine,
            this.mnuItemRemoveLine,
            this.mnuItemSaveClose});
            this.mnuSetup.Location = new System.Drawing.Point(0, 0);
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(623, 24);
            this.mnuSetup.TabIndex = 26;
            this.mnuSetup.Text = "menuStrip1";
            this.mnuSetup.Visible = false;
            // 
            // mnuItemAddLine
            // 
            this.mnuItemAddLine.Name = "mnuItemAddLine";
            this.mnuItemAddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuItemAddLine.Size = new System.Drawing.Size(66, 20);
            this.mnuItemAddLine.Text = "Add Line";
            // 
            // mnuItemRemoveLine
            // 
            this.mnuItemRemoveLine.Name = "mnuItemRemoveLine";
            this.mnuItemRemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuItemRemoveLine.Size = new System.Drawing.Size(87, 20);
            this.mnuItemRemoveLine.Text = "Remove Line";
            // 
            // mnuItemSaveClose
            // 
            this.mnuItemSaveClose.Name = "mnuItemSaveClose";
            this.mnuItemSaveClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuItemSaveClose.Size = new System.Drawing.Size(75, 20);
            this.mnuItemSaveClose.Text = "Save Close";
            // 
            // chkNoResponseFoundStatus
            // 
            this.chkNoResponseFoundStatus.BackColor = System.Drawing.Color.Transparent;
            this.chkNoResponseFoundStatus.Location = new System.Drawing.Point(307, 37);
            this.chkNoResponseFoundStatus.Margin = new System.Windows.Forms.Padding(2);
            this.chkNoResponseFoundStatus.Name = "chkNoResponseFoundStatus";
            this.chkNoResponseFoundStatus.Size = new System.Drawing.Size(211, 18);
            this.chkNoResponseFoundStatus.TabIndex = 6;
            this.chkNoResponseFoundStatus.Text = "Hide \'No Response Found\' Status";
            this.chkNoResponseFoundStatus.UseVisualStyleBackColor = false;
            this.chkNoResponseFoundStatus.Visible = false;
            this.chkNoResponseFoundStatus.CheckedChanged += new System.EventHandler(this.chkNoResponseFoundStatus_CheckedChanged);
            // 
            // frmBatchEligibilityActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.pnlTab);
            this.Controls.Add(this.pnlMST);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.mnuSetup);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuSetup;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchEligibilityActivity";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Eligibilty Activity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBatchEligibilityActivity_FormClosing);
            this.Load += new System.EventHandler(this.frmBatchEligibilityActivity_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlMST.ResumeLayout(false);
            this.pnlMST.PerformLayout();
            this.pnlTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1BatchEligibiltyActivity)).EndInit();
            this.mnuSetup.ResumeLayout(false);
            this.mnuSetup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Main;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlMST;
        private System.Windows.Forms.ToolStripButton tsb_SaveClose;
        private System.Windows.Forms.TextBox txtBatchSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlTab;
        private System.Windows.Forms.MenuStrip mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRemoveLine;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSaveClose;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1FlexGrid.C1FlexGrid C1BatchEligibiltyActivity;
        internal System.Windows.Forms.Label lblClearingHouse;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbView270File;
        private System.Windows.Forms.ToolStripButton tsbView271File;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        internal System.Windows.Forms.CheckBox chkNoResponseFoundStatus;

    }
}