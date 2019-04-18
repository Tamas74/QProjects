namespace gloEmdeonInterface.Forms
{
    partial class frmViewUnMatchPatients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewUnMatchPatients));
            this.ts_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_MatchPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_RegisterPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_DiscardPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlUnmatchPatientList = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTask = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.C1UserTasks = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.imgList_Common = new System.Windows.Forms.ImageList(this.components);
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.ts_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlUnmatchPatientList.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlTask.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1UserTasks)).BeginInit();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.panel5.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Main
            // 
            this.ts_Main.BackColor = System.Drawing.Color.Transparent;
            this.ts_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Main.BackgroundImage")));
            this.ts_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_MatchPatient,
            this.tlbbtn_RegisterPatient,
            this.tlbbtn_DiscardPatient,
            this.tlbbtn_Close});
            this.ts_Main.Location = new System.Drawing.Point(0, 0);
            this.ts_Main.Name = "ts_Main";
            this.ts_Main.Size = new System.Drawing.Size(902, 53);
            this.ts_Main.TabIndex = 1;
            this.ts_Main.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Main_ItemClicked);
            // 
            // tlbbtn_MatchPatient
            // 
            this.tlbbtn_MatchPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_MatchPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_MatchPatient.Image")));
            this.tlbbtn_MatchPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_MatchPatient.Name = "tlbbtn_MatchPatient";
            this.tlbbtn_MatchPatient.Size = new System.Drawing.Size(98, 50);
            this.tlbbtn_MatchPatient.Text = "&Match Patient";
            this.tlbbtn_MatchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_MatchPatient.ToolTipText = "Match Patient";
            // 
            // tlbbtn_RegisterPatient
            // 
            this.tlbbtn_RegisterPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_RegisterPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RegisterPatient.Image")));
            this.tlbbtn_RegisterPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_RegisterPatient.Name = "tlbbtn_RegisterPatient";
            this.tlbbtn_RegisterPatient.Size = new System.Drawing.Size(86, 50);
            this.tlbbtn_RegisterPatient.Text = "&New Patient";
            this.tlbbtn_RegisterPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_RegisterPatient.ToolTipText = "New Patient";
            // 
            // tlbbtn_DiscardPatient
            // 
            this.tlbbtn_DiscardPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_DiscardPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_DiscardPatient.Image")));
            this.tlbbtn_DiscardPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_DiscardPatient.Name = "tlbbtn_DiscardPatient";
            this.tlbbtn_DiscardPatient.Size = new System.Drawing.Size(128, 50);
            this.tlbbtn_DiscardPatient.Text = "&Discard Lab Report";
            this.tlbbtn_DiscardPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_DiscardPatient.ToolTipText = "Discard Lab Report";
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowEditing = false;
            this.c1PatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientList.Location = new System.Drawing.Point(1, 27);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Rows.DefaultSize = 19;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.ShowCellLabels = true;
            this.c1PatientList.Size = new System.Drawing.Size(669, 456);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlUnmatchPatientList);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.pnlTask);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(902, 490);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlUnmatchPatientList
            // 
            this.pnlUnmatchPatientList.Controls.Add(this.c1PatientList);
            this.pnlUnmatchPatientList.Controls.Add(this.panel7);
            this.pnlUnmatchPatientList.Controls.Add(this.label7);
            this.pnlUnmatchPatientList.Controls.Add(this.label8);
            this.pnlUnmatchPatientList.Controls.Add(this.label5);
            this.pnlUnmatchPatientList.Controls.Add(this.label6);
            this.pnlUnmatchPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUnmatchPatientList.Location = new System.Drawing.Point(228, 3);
            this.pnlUnmatchPatientList.Name = "pnlUnmatchPatientList";
            this.pnlUnmatchPatientList.Size = new System.Drawing.Size(671, 484);
            this.pnlUnmatchPatientList.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(1, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(669, 26);
            this.panel7.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(669, 25);
            this.label10.TabIndex = 5;
            this.label10.Text = "  Unmatched Patient List";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(0, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(669, 1);
            this.label14.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(1, 483);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(669, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(669, 1);
            this.label8.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 484);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(670, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 484);
            this.label6.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(225, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 484);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // pnlTask
            // 
            this.pnlTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTask.Controls.Add(this.panel1);
            this.pnlTask.Controls.Add(this.pnlSearch);
            this.pnlTask.Controls.Add(this.panel5);
            this.pnlTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTask.Location = new System.Drawing.Point(3, 3);
            this.pnlTask.Name = "pnlTask";
            this.pnlTask.Size = new System.Drawing.Size(222, 484);
            this.pnlTask.TabIndex = 0;
            this.pnlTask.Resize += new System.EventHandler(this.pnlTask_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.C1UserTasks);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 428);
            this.panel1.TabIndex = 37;
            // 
            // C1UserTasks
            // 
            this.C1UserTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1UserTasks.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1UserTasks.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1UserTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1UserTasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1UserTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1UserTasks.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1UserTasks.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1UserTasks.Location = new System.Drawing.Point(1, 1);
            this.C1UserTasks.Name = "C1UserTasks";
            this.C1UserTasks.Rows.Count = 5;
            this.C1UserTasks.Rows.DefaultSize = 19;
            this.C1UserTasks.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1UserTasks.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1UserTasks.Size = new System.Drawing.Size(220, 426);
            this.C1UserTasks.StyleInfo = resources.GetString("C1UserTasks.StyleInfo");
            this.C1UserTasks.TabIndex = 33;
            this.C1UserTasks.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1UserTasks_AfterRowColChange);
            this.C1UserTasks.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1UserTasks_AfterSelChange);
            this.C1UserTasks.Click += new System.EventHandler(this.C1UserTasks_Click);
            this.C1UserTasks.DoubleClick += new System.EventHandler(this.C1UserTasks_DoubleClick);
            this.C1UserTasks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.C1UserTasks_KeyPress);
            this.C1UserTasks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1UserTasks_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 1);
            this.label1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(221, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 427);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(1, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 1);
            this.label2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 428);
            this.label4.TabIndex = 3;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.PicBx_Search);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 26);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlSearch.Size = new System.Drawing.Size(222, 30);
            this.pnlSearch.TabIndex = 40;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(29, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(171, 15);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 4);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(171, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 21);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(171, 5);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(200, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 22);
            this.btnClear.TabIndex = 41;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 4);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 22);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 26);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(220, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 3);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(220, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(221, 3);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(222, 26);
            this.panel5.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(220, 1);
            this.label13.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(221, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 25);
            this.label12.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 25);
            this.label11.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(0, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(222, 1);
            this.label15.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(222, 26);
            this.label9.TabIndex = 4;
            this.label9.Text = "  Tasks";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.ts_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(902, 54);
            this.pnlToolstrip.TabIndex = 5;
            // 
            // imgList_Common
            // 
            this.imgList_Common.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList_Common.ImageStream")));
            this.imgList_Common.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList_Common.Images.SetKeyName(0, "");
            this.imgList_Common.Images.SetKeyName(1, "");
            this.imgList_Common.Images.SetKeyName(2, "");
            this.imgList_Common.Images.SetKeyName(3, "");
            this.imgList_Common.Images.SetKeyName(4, "");
            this.imgList_Common.Images.SetKeyName(5, "");
            this.imgList_Common.Images.SetKeyName(6, "");
            this.imgList_Common.Images.SetKeyName(7, "");
            this.imgList_Common.Images.SetKeyName(8, "");
            this.imgList_Common.Images.SetKeyName(9, "");
            this.imgList_Common.Images.SetKeyName(10, "");
            this.imgList_Common.Images.SetKeyName(11, "");
            this.imgList_Common.Images.SetKeyName(12, "");
            this.imgList_Common.Images.SetKeyName(13, "");
            this.imgList_Common.Images.SetKeyName(14, "");
            this.imgList_Common.Images.SetKeyName(15, "");
            this.imgList_Common.Images.SetKeyName(16, "New.ico");
            this.imgList_Common.Images.SetKeyName(17, "Note.ico");
            this.imgList_Common.Images.SetKeyName(18, "Password Policy.ico");
            this.imgList_Common.Images.SetKeyName(19, "Low Priority.ico");
            this.imgList_Common.Images.SetKeyName(20, "High Priority.ico");
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmViewUnMatchPatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(902, 544);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmViewUnMatchPatients";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unmatched Patient";
            this.Load += new System.EventHandler(this.frmViewUnMatchPatients_Load);
            this.ts_Main.ResumeLayout(false);
            this.ts_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlUnmatchPatientList.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.pnlTask.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1UserTasks)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Main;
        internal System.Windows.Forms.ToolStripButton tlbbtn_RegisterPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_DiscardPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_MatchPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTask;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlUnmatchPatientList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlToolstrip;
        internal C1.Win.C1FlexGrid.C1FlexGrid C1UserTasks;
        private System.Windows.Forms.ImageList imgList_Common;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
    }
}