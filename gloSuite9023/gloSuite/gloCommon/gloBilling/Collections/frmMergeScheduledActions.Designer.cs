namespace gloBilling.Collections
{
    partial class frmMergeScheduledActions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeScheduledActions));
            this.ts_ViewButtons = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbtn_Merge = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlTopRight = new System.Windows.Forms.Panel();
            this.rdPatAccountFolloup = new System.Windows.Forms.RadioButton();
            this.rdInsClaimFollowup = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtMergeScheduledAction = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.c1ScheduledActionList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.ts_ViewButtons.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ScheduledActionList)).BeginInit();
            this.SuspendLayout();
            // 
            // ts_ViewButtons
            // 
            this.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent;
            this.ts_ViewButtons.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_ViewButtons.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_ViewButtons.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_ViewButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Merge,
            this.ts_btnClose});
            this.ts_ViewButtons.Location = new System.Drawing.Point(0, 0);
            this.ts_ViewButtons.Name = "ts_ViewButtons";
            this.ts_ViewButtons.Size = new System.Drawing.Size(984, 53);
            this.ts_ViewButtons.TabIndex = 2;
            this.ts_ViewButtons.Text = "ToolStrip1";
            // 
            // tsbtn_Merge
            // 
            this.tsbtn_Merge.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Merge.Image")));
            this.tsbtn_Merge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Merge.Name = "tsbtn_Merge";
            this.tsbtn_Merge.Size = new System.Drawing.Size(49, 50);
            this.tsbtn_Merge.Tag = "Merge";
            this.tsbtn_Merge.Text = "Mer&ge";
            this.tsbtn_Merge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_Merge.ToolTipText = "Merge";
            this.tsbtn_Merge.Click += new System.EventHandler(this.tsbtn_Merge_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.pnlTopRight);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(984, 30);
            this.pnlSearch.TabIndex = 5;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTopRight.Controls.Add(this.rdPatAccountFolloup);
            this.pnlTopRight.Controls.Add(this.rdInsClaimFollowup);
            this.pnlTopRight.Controls.Add(this.Label1);
            this.pnlTopRight.Controls.Add(this.panel4);
            this.pnlTopRight.Controls.Add(this.Label5);
            this.pnlTopRight.Controls.Add(this.Label6);
            this.pnlTopRight.Controls.Add(this.Label7);
            this.pnlTopRight.Controls.Add(this.Label8);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopRight.Location = new System.Drawing.Point(3, 3);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(978, 24);
            this.pnlTopRight.TabIndex = 1;
            // 
            // rdPatAccountFolloup
            // 
            this.rdPatAccountFolloup.AutoSize = true;
            this.rdPatAccountFolloup.Location = new System.Drawing.Point(298, 3);
            this.rdPatAccountFolloup.Name = "rdPatAccountFolloup";
            this.rdPatAccountFolloup.Size = new System.Drawing.Size(171, 18);
            this.rdPatAccountFolloup.TabIndex = 52;
            this.rdPatAccountFolloup.TabStop = true;
            this.rdPatAccountFolloup.Text = "Pateint Account Follow Up";
            this.rdPatAccountFolloup.UseVisualStyleBackColor = true;
            this.rdPatAccountFolloup.CheckedChanged += new System.EventHandler(this.rdPatAccountFolloup_CheckedChanged);
            // 
            // rdInsClaimFollowup
            // 
            this.rdInsClaimFollowup.AutoSize = true;
            this.rdInsClaimFollowup.Location = new System.Drawing.Point(121, 3);
            this.rdInsClaimFollowup.Name = "rdInsClaimFollowup";
            this.rdInsClaimFollowup.Size = new System.Drawing.Size(166, 18);
            this.rdInsClaimFollowup.TabIndex = 51;
            this.rdInsClaimFollowup.TabStop = true;
            this.rdInsClaimFollowup.Text = "Insurance Claim Follow Up";
            this.rdInsClaimFollowup.UseVisualStyleBackColor = true;
            this.rdInsClaimFollowup.CheckedChanged += new System.EventHandler(this.rdInsClaimFollowup_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(1, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(108, 22);
            this.Label1.TabIndex = 50;
            this.Label1.Text = "Follow Up Type :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.TxtMergeScheduledAction);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(736, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 22);
            this.panel4.TabIndex = 49;
            this.panel4.Visible = false;
            // 
            // TxtMergeScheduledAction
            // 
            this.TxtMergeScheduledAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMergeScheduledAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtMergeScheduledAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMergeScheduledAction.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMergeScheduledAction.Location = new System.Drawing.Point(5, 3);
            this.TxtMergeScheduledAction.Name = "TxtMergeScheduledAction";
            this.TxtMergeScheduledAction.Size = new System.Drawing.Size(214, 15);
            this.TxtMergeScheduledAction.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(5, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(214, 3);
            this.label21.TabIndex = 37;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(5, 17);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(214, 5);
            this.label20.TabIndex = 43;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(219, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 22);
            this.btnClear.TabIndex = 46;
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(4, 22);
            this.label22.TabIndex = 38;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 22);
            this.label23.TabIndex = 39;
            this.label23.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Location = new System.Drawing.Point(240, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 22);
            this.label24.TabIndex = 40;
            this.label24.Text = "label4";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(1, 23);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(976, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(0, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 23);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(977, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 23);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(0, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(978, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.c1ScheduledActionList);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 83);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnl_Base.Size = new System.Drawing.Size(984, 534);
            this.pnl_Base.TabIndex = 6;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 530);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(976, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 529);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // c1ScheduledActionList
            // 
            this.c1ScheduledActionList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ScheduledActionList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ScheduledActionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ScheduledActionList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ScheduledActionList.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1ScheduledActionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ScheduledActionList.ExtendLastCol = true;
            this.c1ScheduledActionList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ScheduledActionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ScheduledActionList.Location = new System.Drawing.Point(3, 2);
            this.c1ScheduledActionList.Name = "c1ScheduledActionList";
            this.c1ScheduledActionList.Rows.DefaultSize = 19;
            this.c1ScheduledActionList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ScheduledActionList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ScheduledActionList.ShowCellLabels = true;
            this.c1ScheduledActionList.Size = new System.Drawing.Size(977, 529);
            this.c1ScheduledActionList.StyleInfo = resources.GetString("c1ScheduledActionList.StyleInfo");
            this.c1ScheduledActionList.TabIndex = 0;
            this.c1ScheduledActionList.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1ScheduledActionList_BeforeSort);
            this.c1ScheduledActionList.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1ScheduledActionList_AfterSort);
            this.c1ScheduledActionList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ScheduledActionList_AfterEdit);
            this.c1ScheduledActionList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ScheduledActionList_MouseMove);
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(980, 2);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 529);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(978, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmMergeScheduledActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(984, 617);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.ts_ViewButtons);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMergeScheduledActions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Merge Scheduled Actions";
            this.Load += new System.EventHandler(this.frmMergeScheduledActions_Load);
            this.ts_ViewButtons.ResumeLayout(false);
            this.ts_ViewButtons.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlTopRight.ResumeLayout(false);
            this.pnlTopRight.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnl_Base.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ScheduledActionList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_ViewButtons;
        internal System.Windows.Forms.ToolStripButton tsbtn_Merge;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Panel pnlTopRight;
        internal System.Windows.Forms.RadioButton rdPatAccountFolloup;
        internal System.Windows.Forms.RadioButton rdInsClaimFollowup;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.TextBox TxtMergeScheduledAction;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ScheduledActionList;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}