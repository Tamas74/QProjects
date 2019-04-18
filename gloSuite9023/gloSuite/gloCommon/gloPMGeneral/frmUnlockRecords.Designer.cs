namespace gloPMGeneral
{
    partial class frmUnlockRecords
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnlockRecords));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Unlock = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Select = new System.Windows.Forms.ToolStripButton();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_User = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.c1UnLockGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.lblFileCounter = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.trvClosePeriod = new System.Windows.Forms.TreeView();
            this.imgUnlock = new System.Windows.Forms.ImageList(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_pnlBaseBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseTopBrd = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkBatchClaimCount = new System.Windows.Forms.CheckBox();
            this.numBatchClaimCount = new System.Windows.Forms.NumericUpDown();
            this.chkBatchGeneralSearch = new System.Windows.Forms.CheckBox();
            this.txtBatchSearch = new System.Windows.Forms.TextBox();
            this.lblSearhBatch = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_User.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1UnLockGrid)).BeginInit();
            this.pnlProgressBar.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(697, 56);
            this.pnl_tlsp_Top.TabIndex = 1;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Unlock,
            this.tsb_Refresh,
            this.tsb_Select,
            this.btnOK,
            this.tsb_Close});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(697, 53);
            this.tstrip.TabIndex = 0;
            this.tstrip.TabStop = true;
            this.tstrip.Text = "ToolStrip1";
            // 
            // tsb_Unlock
            // 
            this.tsb_Unlock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Unlock.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Unlock.Image")));
            this.tsb_Unlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Unlock.Name = "tsb_Unlock";
            this.tsb_Unlock.Size = new System.Drawing.Size(55, 50);
            this.tsb_Unlock.Text = "&UnLock";
            this.tsb_Unlock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Unlock.ToolTipText = "UnLock Records";
            this.tsb_Unlock.Click += new System.EventHandler(this.tsb_Unlock_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(55, 50);
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Select
            // 
            this.tsb_Select.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Select.Image")));
            this.tsb_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Select.Name = "tsb_Select";
            this.tsb_Select.Size = new System.Drawing.Size(46, 50);
            this.tsb_Select.Text = "Select";
            this.tsb_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Select.Click += new System.EventHandler(this.tsb_Select_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 50);
            this.btnOK.Text = "&Save&&Cls";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Save and Close";
            this.btnOK.Visible = false;
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.ToolTipText = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 572);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_User);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgUnlock;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(697, 572);
            this.tabControl1.TabIndex = 0;
            // 
            // tp_User
            // 
            this.tp_User.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tp_User.Controls.Add(this.panel9);
            this.tp_User.Controls.Add(this.splitter1);
            this.tp_User.Controls.Add(this.pnlBase);
            this.tp_User.Controls.Add(this.panel4);
            this.tp_User.ImageKey = "New01.ico";
            this.tp_User.Location = new System.Drawing.Point(4, 23);
            this.tp_User.Name = "tp_User";
            this.tp_User.Padding = new System.Windows.Forms.Padding(3);
            this.tp_User.Size = new System.Drawing.Size(689, 545);
            this.tp_User.TabIndex = 0;
            this.tp_User.Tag = "ClosePeriod";
            this.tp_User.Text = "Records";
            this.tp_User.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.c1UnLockGrid);
            this.panel9.Controls.Add(this.label44);
            this.panel9.Controls.Add(this.label45);
            this.panel9.Controls.Add(this.label46);
            this.panel9.Controls.Add(this.label47);
            this.panel9.Controls.Add(this.pnlProgressBar);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(220, 27);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(466, 515);
            this.panel9.TabIndex = 29;
            // 
            // c1UnLockGrid
            // 
            this.c1UnLockGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1UnLockGrid.AutoResize = false;
            this.c1UnLockGrid.BackColor = System.Drawing.Color.White;
            this.c1UnLockGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1UnLockGrid.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1UnLockGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1UnLockGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1UnLockGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1UnLockGrid.Location = new System.Drawing.Point(1, 4);
            this.c1UnLockGrid.Name = "c1UnLockGrid";
            this.c1UnLockGrid.Padding = new System.Windows.Forms.Padding(3);
            this.c1UnLockGrid.Rows.Count = 1;
            this.c1UnLockGrid.Rows.DefaultSize = 19;
            this.c1UnLockGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1UnLockGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1UnLockGrid.ShowCellLabels = true;
            this.c1UnLockGrid.Size = new System.Drawing.Size(464, 510);
            this.c1UnLockGrid.StyleInfo = resources.GetString("c1UnLockGrid.StyleInfo");
            this.c1UnLockGrid.TabIndex = 27;
            this.c1UnLockGrid.Tag = "ClosePeriod";
            this.c1UnLockGrid.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1UnLockGrid_AfterEdit);
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label44.Location = new System.Drawing.Point(1, 514);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(464, 1);
            this.label44.TabIndex = 25;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(1, 3);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(464, 1);
            this.label45.TabIndex = 24;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(465, 3);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 512);
            this.label46.TabIndex = 23;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Location = new System.Drawing.Point(0, 3);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 512);
            this.label47.TabIndex = 22;
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.Controls.Add(this.lblFileCounter);
            this.pnlProgressBar.Controls.Add(this.lblFile);
            this.pnlProgressBar.Controls.Add(this.label21);
            this.pnlProgressBar.Controls.Add(this.label4);
            this.pnlProgressBar.Controls.Add(this.label1);
            this.pnlProgressBar.Controls.Add(this.label3);
            this.pnlProgressBar.Controls.Add(this.label2);
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 491);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(466, 24);
            this.pnlProgressBar.TabIndex = 29;
            this.pnlProgressBar.Visible = false;
            // 
            // lblFileCounter
            // 
            this.lblFileCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter.Location = new System.Drawing.Point(344, 2);
            this.lblFileCounter.Name = "lblFileCounter";
            this.lblFileCounter.Size = new System.Drawing.Size(121, 19);
            this.lblFileCounter.TabIndex = 24;
            this.lblFileCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(1, 2);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(286, 19);
            this.lblFile.TabIndex = 1;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Padding = new System.Windows.Forms.Padding(3);
            this.label21.Size = new System.Drawing.Size(464, 2);
            this.label21.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 21);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(464, 2);
            this.label4.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(1, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 1);
            this.label1.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(465, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 24);
            this.label3.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 24);
            this.label2.TabIndex = 27;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(217, 27);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 515);
            this.splitter1.TabIndex = 28;
            this.splitter1.TabStop = false;
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBase.Controls.Add(this.trvClosePeriod);
            this.pnlBase.Controls.Add(this.label13);
            this.pnlBase.Controls.Add(this.label7);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseBottomBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseLeftBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseRightBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseTopBrd);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBase.Location = new System.Drawing.Point(3, 27);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlBase.Size = new System.Drawing.Size(214, 515);
            this.pnlBase.TabIndex = 27;
            // 
            // trvClosePeriod
            // 
            this.trvClosePeriod.BackColor = System.Drawing.Color.White;
            this.trvClosePeriod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvClosePeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvClosePeriod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvClosePeriod.ForeColor = System.Drawing.Color.Black;
            this.trvClosePeriod.HideSelection = false;
            this.trvClosePeriod.ImageIndex = 0;
            this.trvClosePeriod.ImageList = this.imgUnlock;
            this.trvClosePeriod.Indent = 20;
            this.trvClosePeriod.ItemHeight = 20;
            this.trvClosePeriod.Location = new System.Drawing.Point(4, 7);
            this.trvClosePeriod.Name = "trvClosePeriod";
            this.trvClosePeriod.SelectedImageIndex = 0;
            this.trvClosePeriod.ShowLines = false;
            this.trvClosePeriod.ShowNodeToolTips = true;
            this.trvClosePeriod.ShowRootLines = false;
            this.trvClosePeriod.Size = new System.Drawing.Size(209, 507);
            this.trvClosePeriod.TabIndex = 0;
            this.trvClosePeriod.Tag = "ClosePeriod";
            this.trvClosePeriod.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvClosePeriod_AfterSelect);
            // 
            // imgUnlock
            // 
            this.imgUnlock.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgUnlock.ImageStream")));
            this.imgUnlock.TransparentColor = System.Drawing.Color.Transparent;
            this.imgUnlock.Images.SetKeyName(0, "Charges.ico");
            this.imgUnlock.Images.SetKeyName(1, "Check.ico");
            this.imgUnlock.Images.SetKeyName(2, "New01.ico");
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(209, 3);
            this.label13.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(3, 510);
            this.label7.TabIndex = 5;
            // 
            // lbl_pnlBaseBottomBrd
            // 
            this.lbl_pnlBaseBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBaseBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseBottomBrd.Location = new System.Drawing.Point(1, 514);
            this.lbl_pnlBaseBottomBrd.Name = "lbl_pnlBaseBottomBrd";
            this.lbl_pnlBaseBottomBrd.Size = new System.Drawing.Size(212, 1);
            this.lbl_pnlBaseBottomBrd.TabIndex = 4;
            this.lbl_pnlBaseBottomBrd.Text = "label2";
            // 
            // lbl_pnlBaseLeftBrd
            // 
            this.lbl_pnlBaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlBaseLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseLeftBrd.Location = new System.Drawing.Point(0, 4);
            this.lbl_pnlBaseLeftBrd.Name = "lbl_pnlBaseLeftBrd";
            this.lbl_pnlBaseLeftBrd.Size = new System.Drawing.Size(1, 511);
            this.lbl_pnlBaseLeftBrd.TabIndex = 3;
            this.lbl_pnlBaseLeftBrd.Text = "label4";
            // 
            // lbl_pnlBaseRightBrd
            // 
            this.lbl_pnlBaseRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlBaseRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseRightBrd.Location = new System.Drawing.Point(213, 4);
            this.lbl_pnlBaseRightBrd.Name = "lbl_pnlBaseRightBrd";
            this.lbl_pnlBaseRightBrd.Size = new System.Drawing.Size(1, 511);
            this.lbl_pnlBaseRightBrd.TabIndex = 2;
            this.lbl_pnlBaseRightBrd.Text = "label3";
            // 
            // lbl_pnlBaseTopBrd
            // 
            this.lbl_pnlBaseTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlBaseTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseTopBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlBaseTopBrd.Name = "lbl_pnlBaseTopBrd";
            this.lbl_pnlBaseTopBrd.Size = new System.Drawing.Size(214, 1);
            this.lbl_pnlBaseTopBrd.TabIndex = 0;
            this.lbl_pnlBaseTopBrd.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.chkBatchClaimCount);
            this.panel4.Controls.Add(this.numBatchClaimCount);
            this.panel4.Controls.Add(this.chkBatchGeneralSearch);
            this.panel4.Controls.Add(this.txtBatchSearch);
            this.panel4.Controls.Add(this.lblSearhBatch);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(683, 24);
            this.panel4.TabIndex = 18;
            // 
            // chkBatchClaimCount
            // 
            this.chkBatchClaimCount.AutoSize = true;
            this.chkBatchClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkBatchClaimCount.Location = new System.Drawing.Point(444, 1);
            this.chkBatchClaimCount.Name = "chkBatchClaimCount";
            this.chkBatchClaimCount.Size = new System.Drawing.Size(175, 22);
            this.chkBatchClaimCount.TabIndex = 66;
            this.chkBatchClaimCount.Tag = "ClosePeriod";
            this.chkBatchClaimCount.Text = "Show all for selected batch";
            this.chkBatchClaimCount.UseVisualStyleBackColor = true;
            this.chkBatchClaimCount.Visible = false;
            // 
            // numBatchClaimCount
            // 
            this.numBatchClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.numBatchClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Location = new System.Drawing.Point(619, 1);
            this.numBatchClaimCount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Name = "numBatchClaimCount";
            this.numBatchClaimCount.Size = new System.Drawing.Size(53, 22);
            this.numBatchClaimCount.TabIndex = 65;
            this.numBatchClaimCount.Tag = "ClosePeriod";
            this.numBatchClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBatchClaimCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Visible = false;
            // 
            // chkBatchGeneralSearch
            // 
            this.chkBatchGeneralSearch.AutoSize = true;
            this.chkBatchGeneralSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkBatchGeneralSearch.Location = new System.Drawing.Point(253, 1);
            this.chkBatchGeneralSearch.Name = "chkBatchGeneralSearch";
            this.chkBatchGeneralSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkBatchGeneralSearch.Size = new System.Drawing.Size(114, 22);
            this.chkBatchGeneralSearch.TabIndex = 63;
            this.chkBatchGeneralSearch.Tag = "ClosePeriod";
            this.chkBatchGeneralSearch.Text = "General Search";
            this.chkBatchGeneralSearch.UseVisualStyleBackColor = true;
            this.chkBatchGeneralSearch.Visible = false;
            // 
            // txtBatchSearch
            // 
            this.txtBatchSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtBatchSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchSearch.ForeColor = System.Drawing.Color.Black;
            this.txtBatchSearch.Location = new System.Drawing.Point(71, 1);
            this.txtBatchSearch.Name = "txtBatchSearch";
            this.txtBatchSearch.Size = new System.Drawing.Size(182, 22);
            this.txtBatchSearch.TabIndex = 15;
            this.txtBatchSearch.Tag = "ClosePeriod";
            this.txtBatchSearch.TextChanged += new System.EventHandler(this.txtBatchSearch_TextChanged);
            // 
            // lblSearhBatch
            // 
            this.lblSearhBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearhBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearhBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearhBatch.Location = new System.Drawing.Point(1, 1);
            this.lblSearhBatch.Name = "lblSearhBatch";
            this.lblSearhBatch.Size = new System.Drawing.Size(70, 22);
            this.lblSearhBatch.TabIndex = 6;
            this.lblSearhBatch.Text = "Search :";
            this.lblSearhBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 22);
            this.label16.TabIndex = 21;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(672, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 22);
            this.label17.TabIndex = 60;
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(682, 1);
            this.label18.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(0, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(682, 1);
            this.label19.TabIndex = 20;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(682, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 24);
            this.label20.TabIndex = 22;
            // 
            // frmUnlockRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(697, 628);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUnlockRecords";
            this.Text = "Unlock Records";
            this.Load += new System.EventHandler(this.frmUnlockRecords_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tp_User.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1UnLockGrid)).EndInit();
            this.pnlProgressBar.ResumeLayout(false);
            this.pnlBase.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        private System.Windows.Forms.ToolStripButton tsb_Unlock;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_User;
        private System.Windows.Forms.Panel panel9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1UnLockGrid;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lblFileCounter;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.TreeView trvClosePeriod;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_pnlBaseBottomBrd;
        private System.Windows.Forms.Label lbl_pnlBaseLeftBrd;
        private System.Windows.Forms.Label lbl_pnlBaseRightBrd;
        private System.Windows.Forms.Label lbl_pnlBaseTopBrd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkBatchClaimCount;
        private System.Windows.Forms.NumericUpDown numBatchClaimCount;
        private System.Windows.Forms.CheckBox chkBatchGeneralSearch;
        private System.Windows.Forms.TextBox txtBatchSearch;
        private System.Windows.Forms.Label lblSearhBatch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ImageList imgUnlock;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_Select;
    }
}