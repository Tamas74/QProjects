namespace gloEmdeonInterface.Forms
{
    partial class frmIntuitPatientReview
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
                    if (oTimer != null)
                    {
                        oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                        oTimer.Dispose();
                        oTimer = null;
                    }
                }
                catch
                {
                }
                if (toolTip != null)
                {
                    toolTip.Dispose();
                    toolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntuitPatientReview));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.ts_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_MatchPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_UnmatchPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbbtn_AcceptPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_RejectPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlTask = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.c1IntuitPatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.rdbtnAll = new System.Windows.Forms.RadioButton();
            this.rdbtnMatched = new System.Windows.Forms.RadioButton();
            this.rdbtnUnMatched = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.btnSearchClose = new System.Windows.Forms.Button();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labIntuitPatientList = new System.Windows.Forms.Label();
            this.pnlUnmatchPatientList = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label8 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlProviderSelection = new System.Windows.Forms.Panel();
            this.btnDeleteProvider = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblProvider = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlToolstrip.SuspendLayout();
            this.ts_Main.SuspendLayout();
            this.pnlTask.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1IntuitPatientList)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlUnmatchPatientList.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.panel7.SuspendLayout();
            this.pnlProviderSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.ts_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(943, 56);
            this.pnlToolstrip.TabIndex = 6;
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
            this.tlbbtn_UnmatchPatient,
            this.tlbbtn_Refresh,
            this.toolStripSeparator1,
            this.tlbbtn_AcceptPatient,
            this.tlbbtn_RejectPatient,
            this.tlbbtn_Close});
            this.ts_Main.Location = new System.Drawing.Point(0, 0);
            this.ts_Main.Name = "ts_Main";
            this.ts_Main.Size = new System.Drawing.Size(943, 53);
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
            this.tlbbtn_MatchPatient.Tag = "MATCHPATIENT";
            this.tlbbtn_MatchPatient.Text = "&Match Patient";
            this.tlbbtn_MatchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_MatchPatient.ToolTipText = "Match Patient";
            // 
            // tlbbtn_UnmatchPatient
            // 
            this.tlbbtn_UnmatchPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_UnmatchPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_UnmatchPatient.Image")));
            this.tlbbtn_UnmatchPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_UnmatchPatient.Name = "tlbbtn_UnmatchPatient";
            this.tlbbtn_UnmatchPatient.Size = new System.Drawing.Size(114, 50);
            this.tlbbtn_UnmatchPatient.Tag = "Unmatch";
            this.tlbbtn_UnmatchPatient.Text = "&Unmatch Patient";
            this.tlbbtn_UnmatchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_UnmatchPatient.ToolTipText = "Unmatch Patient";
            // 
            // tlbbtn_Refresh
            // 
            this.tlbbtn_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Refresh.Image")));
            this.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Refresh.Name = "tlbbtn_Refresh";
            this.tlbbtn_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tlbbtn_Refresh.Tag = "REFRESH";
            this.tlbbtn_Refresh.Text = "&Refresh";
            this.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            this.toolStripSeparator1.Tag = "";
            // 
            // tlbbtn_AcceptPatient
            // 
            this.tlbbtn_AcceptPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_AcceptPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_AcceptPatient.Image")));
            this.tlbbtn_AcceptPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_AcceptPatient.Name = "tlbbtn_AcceptPatient";
            this.tlbbtn_AcceptPatient.Size = new System.Drawing.Size(53, 50);
            this.tlbbtn_AcceptPatient.Tag = "NEW&ACCEPT";
            this.tlbbtn_AcceptPatient.Text = "&Accept";
            this.tlbbtn_AcceptPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_AcceptPatient.ToolTipText = "Accept Patient";
            // 
            // tlbbtn_RejectPatient
            // 
            this.tlbbtn_RejectPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_RejectPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RejectPatient.Image")));
            this.tlbbtn_RejectPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_RejectPatient.Name = "tlbbtn_RejectPatient";
            this.tlbbtn_RejectPatient.Size = new System.Drawing.Size(50, 50);
            this.tlbbtn_RejectPatient.Tag = "REJECT";
            this.tlbbtn_RejectPatient.Text = "R&eject";
            this.tlbbtn_RejectPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_RejectPatient.ToolTipText = "Reject Patient";
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Tag = "CLOSE";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlTask
            // 
            this.pnlTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTask.Controls.Add(this.panel4);
            this.pnlTask.Controls.Add(this.panel1);
            this.pnlTask.Controls.Add(this.pnlSearch);
            this.pnlTask.Controls.Add(this.panel3);
            this.pnlTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTask.Location = new System.Drawing.Point(0, 56);
            this.pnlTask.Name = "pnlTask";
            this.pnlTask.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlTask.Size = new System.Drawing.Size(249, 666);
            this.pnlTask.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.c1IntuitPatientList);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 81);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel4.Size = new System.Drawing.Size(246, 582);
            this.panel4.TabIndex = 38;
            // 
            // c1IntuitPatientList
            // 
            this.c1IntuitPatientList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1IntuitPatientList.AllowEditing = false;
            this.c1IntuitPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1IntuitPatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1IntuitPatientList.ColumnInfo = "10,1,0,0,0,95,Columns:1{Width:120;}\t";
            this.c1IntuitPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1IntuitPatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1IntuitPatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1IntuitPatientList.Location = new System.Drawing.Point(1, 4);
            this.c1IntuitPatientList.Name = "c1IntuitPatientList";
            this.c1IntuitPatientList.Rows.DefaultSize = 19;
            this.c1IntuitPatientList.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1IntuitPatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1IntuitPatientList.ShowCellLabels = true;
            this.c1IntuitPatientList.Size = new System.Drawing.Size(244, 577);
            this.c1IntuitPatientList.StyleInfo = resources.GetString("c1IntuitPatientList.StyleInfo");
            this.c1IntuitPatientList.TabIndex = 37;
            this.c1IntuitPatientList.BeforeMouseDown += new C1.Win.C1FlexGrid.BeforeMouseDownEventHandler(this.c1IntuitPatientList_BeforeMouseDown);
            this.c1IntuitPatientList.RowColChange += new System.EventHandler(this.c1IntuitPatientList_RowColChange);
            this.c1IntuitPatientList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1IntuitPatientList_KeyDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(1, 581);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 1);
            this.label2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(245, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 578);
            this.label3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 1);
            this.label1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 579);
            this.label4.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.rdbtnAll);
            this.panel1.Controls.Add(this.rdbtnMatched);
            this.panel1.Controls.Add(this.rdbtnUnMatched);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 28);
            this.panel1.TabIndex = 36;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(1, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(244, 1);
            this.label18.TabIndex = 10;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(245, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 27);
            this.label17.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 27);
            this.label16.TabIndex = 8;
            // 
            // rdbtnAll
            // 
            this.rdbtnAll.AutoSize = true;
            this.rdbtnAll.Checked = true;
            this.rdbtnAll.Location = new System.Drawing.Point(8, 4);
            this.rdbtnAll.Name = "rdbtnAll";
            this.rdbtnAll.Size = new System.Drawing.Size(37, 18);
            this.rdbtnAll.TabIndex = 7;
            this.rdbtnAll.TabStop = true;
            this.rdbtnAll.Text = "All";
            this.rdbtnAll.UseVisualStyleBackColor = true;
            this.rdbtnAll.CheckedChanged += new System.EventHandler(this.rdbtnMatched_CheckedChanged);
            // 
            // rdbtnMatched
            // 
            this.rdbtnMatched.AutoSize = true;
            this.rdbtnMatched.Location = new System.Drawing.Point(52, 4);
            this.rdbtnMatched.Name = "rdbtnMatched";
            this.rdbtnMatched.Size = new System.Drawing.Size(72, 18);
            this.rdbtnMatched.TabIndex = 6;
            this.rdbtnMatched.Text = "Matched";
            this.rdbtnMatched.UseVisualStyleBackColor = true;
            this.rdbtnMatched.CheckedChanged += new System.EventHandler(this.rdbtnMatched_CheckedChanged);
            // 
            // rdbtnUnMatched
            // 
            this.rdbtnUnMatched.AutoSize = true;
            this.rdbtnUnMatched.Location = new System.Drawing.Point(131, 4);
            this.rdbtnUnMatched.Name = "rdbtnUnMatched";
            this.rdbtnUnMatched.Size = new System.Drawing.Size(88, 18);
            this.rdbtnUnMatched.TabIndex = 6;
            this.rdbtnUnMatched.Text = "Unmatched";
            this.rdbtnUnMatched.UseVisualStyleBackColor = true;
            this.rdbtnUnMatched.CheckedChanged += new System.EventHandler(this.rdbtnMatched_CheckedChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(0, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(246, 1);
            this.label9.TabIndex = 2;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.btnSearchClose);
            this.pnlSearch.Controls.Add(this.PicBx_Search);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(3, 25);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlSearch.Size = new System.Drawing.Size(246, 28);
            this.pnlSearch.TabIndex = 38;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 22);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(198, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 41;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(29, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(198, 15);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 4);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(198, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // btnSearchClose
            // 
            this.btnSearchClose.BackColor = System.Drawing.Color.White;
            this.btnSearchClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchClose.FlatAppearance.BorderSize = 0;
            this.btnSearchClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearchClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearchClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchClose.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchClose.Image")));
            this.btnSearchClose.Location = new System.Drawing.Point(227, 4);
            this.btnSearchClose.Name = "btnSearchClose";
            this.btnSearchClose.Size = new System.Drawing.Size(18, 20);
            this.btnSearchClose.TabIndex = 32;
            this.btnSearchClose.UseVisualStyleBackColor = false;
            this.btnSearchClose.Click += new System.EventHandler(this.btnSearchClose_Click);
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 4);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 20);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(244, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 3);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(244, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(245, 3);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(246, 25);
            this.panel3.TabIndex = 38;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.labIntuitPatientList);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(246, 25);
            this.panel5.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(244, 1);
            this.label13.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(245, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 24);
            this.label12.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 24);
            this.label10.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(0, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(246, 1);
            this.label15.TabIndex = 2;
            // 
            // labIntuitPatientList
            // 
            this.labIntuitPatientList.BackColor = System.Drawing.Color.Transparent;
            this.labIntuitPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labIntuitPatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIntuitPatientList.Location = new System.Drawing.Point(0, 0);
            this.labIntuitPatientList.Name = "labIntuitPatientList";
            this.labIntuitPatientList.Size = new System.Drawing.Size(246, 25);
            this.labIntuitPatientList.TabIndex = 4;
            this.labIntuitPatientList.Text = "Portal Patient List";
            this.labIntuitPatientList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlUnmatchPatientList
            // 
            this.pnlUnmatchPatientList.Controls.Add(this.panel2);
            this.pnlUnmatchPatientList.Controls.Add(this.panel7);
            this.pnlUnmatchPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUnmatchPatientList.Location = new System.Drawing.Point(252, 56);
            this.pnlUnmatchPatientList.Name = "pnlUnmatchPatientList";
            this.pnlUnmatchPatientList.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlUnmatchPatientList.Size = new System.Drawing.Size(691, 666);
            this.pnlUnmatchPatientList.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.c1PatientDetails);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel2.Size = new System.Drawing.Size(688, 638);
            this.panel2.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(687, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 633);
            this.label6.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(1, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(687, 1);
            this.label5.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 634);
            this.label11.TabIndex = 4;
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientDetails.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientDetails.Location = new System.Drawing.Point(0, 3);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.ShowCellLabels = true;
            this.c1PatientDetails.Size = new System.Drawing.Size(688, 634);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 2;
            this.c1PatientDetails.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_AfterEdit);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 637);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(688, 1);
            this.label8.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.pnlProviderSelection);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.chkSelectAll);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Controls.Add(this.label20);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(688, 25);
            this.panel7.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 23);
            this.label19.TabIndex = 9;
            // 
            // pnlProviderSelection
            // 
            this.pnlProviderSelection.BackColor = System.Drawing.Color.Transparent;
            this.pnlProviderSelection.Controls.Add(this.btnDeleteProvider);
            this.pnlProviderSelection.Controls.Add(this.cmbProvider);
            this.pnlProviderSelection.Controls.Add(this.lblProvider);
            this.pnlProviderSelection.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProviderSelection.Location = new System.Drawing.Point(396, 1);
            this.pnlProviderSelection.Name = "pnlProviderSelection";
            this.pnlProviderSelection.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlProviderSelection.Size = new System.Drawing.Size(291, 23);
            this.pnlProviderSelection.TabIndex = 6;
            // 
            // btnDeleteProvider
            // 
            this.btnDeleteProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteProvider.FlatAppearance.BorderSize = 0;
            this.btnDeleteProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeleteProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteProvider.Image")));
            this.btnDeleteProvider.Location = new System.Drawing.Point(259, 0);
            this.btnDeleteProvider.Name = "btnDeleteProvider";
            this.btnDeleteProvider.Size = new System.Drawing.Size(22, 23);
            this.btnDeleteProvider.TabIndex = 7;
            this.btnDeleteProvider.UseVisualStyleBackColor = true;
            this.btnDeleteProvider.Click += new System.EventHandler(this.btnDeleteProvider_Click);
            // 
            // cmbProvider
            // 
            this.cmbProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(82, 1);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(176, 22);
            this.cmbProvider.TabIndex = 7;
            // 
            // lblProvider
            // 
            this.lblProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProvider.AutoSize = true;
            this.lblProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(12, 4);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(66, 14);
            this.lblProvider.TabIndex = 5;
            this.lblProvider.Text = "Provider :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(687, 1);
            this.label7.TabIndex = 7;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.Location = new System.Drawing.Point(9, 4);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(82, 18);
            this.chkSelectAll.TabIndex = 3;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = false;
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(0, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(687, 1);
            this.label14.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(687, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 25);
            this.label20.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(249, 56);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 666);
            this.splitter1.TabIndex = 38;
            this.splitter1.TabStop = false;
            // 
            // frmIntuitPatientReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(943, 722);
            this.Controls.Add(this.pnlUnmatchPatientList);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlTask);
            this.Controls.Add(this.pnlToolstrip);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmIntuitPatientReview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Portal Patients";
            this.Load += new System.EventHandler(this.frmIntuitPatientReview_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ts_Main.ResumeLayout(false);
            this.ts_Main.PerformLayout();
            this.pnlTask.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1IntuitPatientList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlUnmatchPatientList.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlProviderSelection.ResumeLayout(false);
            this.pnlProviderSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Main;
        internal System.Windows.Forms.ToolStripButton tlbbtn_MatchPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_AcceptPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_RejectPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Refresh;
        private System.Windows.Forms.Panel pnlTask;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labIntuitPatientList;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1IntuitPatientList;
        private System.Windows.Forms.Panel pnlUnmatchPatientList;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlProviderSelection;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Button btnDeleteProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbtnMatched;
        private System.Windows.Forms.RadioButton rdbtnUnMatched;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdbtnAll;
        internal System.Windows.Forms.ToolStripButton tlbbtn_UnmatchPatient;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        private System.Windows.Forms.Button btnSearchClose;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
    }
}