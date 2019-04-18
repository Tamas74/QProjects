namespace gloPatient
{
    partial class frmMapUnMatchPatients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapUnMatchPatients));
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPatientList = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.ts_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_MatchPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_DiscardPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_RegisterPatient = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblFirstNameValue = new System.Windows.Forms.Label();
            this.lblLastNameValue = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblGenderValue = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblPatientDOBValue = new System.Windows.Forms.Label();
            this.lblPatientDOB = new System.Windows.Forms.Label();
            this.lblMiddleNameValue = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblRefReqLastName = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblRefReqFirstName = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblRefReqGender = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblRefReqDOB = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblRefReqMiddleName = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.trvMedication = new System.Windows.Forms.TreeView();
            this.c1ActiveMedication = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label42 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label44 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblRequestedQty = new System.Windows.Forms.Label();
            this.lblRequested = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblMedication = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.c1RequestedMedication = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.pnlSelectedRefill = new System.Windows.Forms.Panel();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Label112 = new System.Windows.Forms.Label();
            this.Label113 = new System.Windows.Forms.Label();
            this.lblTextContent = new System.Windows.Forms.Label();
            this.Label115 = new System.Windows.Forms.Label();
            this.Label114 = new System.Windows.Forms.Label();
            this.pnlUnsentlabel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlPatientList.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.ts_Main.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ActiveMedication)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RequestedMedication)).BeginInit();
            this.panel6.SuspendLayout();
            this.pnlSelectedRefill.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.pnlUnsentlabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(382, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(144, 14);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search Different Names :";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(6, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(213, 15);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowEditing = false;
            this.c1PatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "10,0,0,0,0,105,Columns:";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientList.Location = new System.Drawing.Point(3, 1);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Rows.Count = 1;
            this.c1PatientList.Rows.DefaultSize = 21;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.ShowCellLabels = true;
            this.c1PatientList.Size = new System.Drawing.Size(773, 199);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 5;
            this.c1PatientList.SelChange += new System.EventHandler(this.c1PatientList_SelChange);
            this.c1PatientList.Click += new System.EventHandler(this.c1PatientList_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(773, 30);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select Patient From Patient List :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.txtSearch);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.lbl_WhiteSpaceTop);
            this.panel4.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.panel4.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.panel4.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.panel4.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(528, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 24);
            this.panel4.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.White;
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(1, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(5, 14);
            this.label26.TabIndex = 42;
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(1, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(218, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(1, 18);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(218, 5);
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
            this.btnClear.Location = new System.Drawing.Point(219, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 22);
            this.btnClear.TabIndex = 41;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 23);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(239, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(239, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(240, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 29);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(772, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 29);
            this.label3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(773, 1);
            this.label1.TabIndex = 0;
            // 
            // pnlPatientList
            // 
            this.pnlPatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientList.Controls.Add(this.label5);
            this.pnlPatientList.Controls.Add(this.label6);
            this.pnlPatientList.Controls.Add(this.label7);
            this.pnlPatientList.Controls.Add(this.c1PatientList);
            this.pnlPatientList.Controls.Add(this.label8);
            this.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientList.Location = new System.Drawing.Point(0, 393);
            this.pnlPatientList.Name = "pnlPatientList";
            this.pnlPatientList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPatientList.Size = new System.Drawing.Size(779, 203);
            this.pnlPatientList.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 198);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(775, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 198);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(773, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(773, 1);
            this.label8.TabIndex = 0;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.ts_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(779, 54);
            this.pnlToolstrip.TabIndex = 7;
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
            this.tlbbtn_DiscardPatient,
            this.tlbbtn_RegisterPatient,
            this.tlbbtn_Close});
            this.ts_Main.Location = new System.Drawing.Point(0, 0);
            this.ts_Main.Name = "ts_Main";
            this.ts_Main.Size = new System.Drawing.Size(779, 53);
            this.ts_Main.TabIndex = 4;
            // 
            // tlbbtn_MatchPatient
            // 
            this.tlbbtn_MatchPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_MatchPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_MatchPatient.Image")));
            this.tlbbtn_MatchPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_MatchPatient.Name = "tlbbtn_MatchPatient";
            this.tlbbtn_MatchPatient.Size = new System.Drawing.Size(97, 50);
            this.tlbbtn_MatchPatient.Text = "&Select Patient";
            this.tlbbtn_MatchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_MatchPatient.ToolTipText = "Select Patient";
            this.tlbbtn_MatchPatient.Click += new System.EventHandler(this.tlbbtn_MatchPatient_Click);
            // 
            // tlbbtn_DiscardPatient
            // 
            this.tlbbtn_DiscardPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_DiscardPatient.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_DiscardPatient.Image")));
            this.tlbbtn_DiscardPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_DiscardPatient.Name = "tlbbtn_DiscardPatient";
            this.tlbbtn_DiscardPatient.Size = new System.Drawing.Size(97, 50);
            this.tlbbtn_DiscardPatient.Text = "&Deny Request";
            this.tlbbtn_DiscardPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_DiscardPatient.ToolTipText = "Deny Refill Request";
            this.tlbbtn_DiscardPatient.Click += new System.EventHandler(this.tlbbtn_DiscardPatient_Click);
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
            this.tlbbtn_RegisterPatient.Click += new System.EventHandler(this.tlbbtn_RegisterPatient_Click);
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
            this.tlbbtn_Close.Click += new System.EventHandler(this.tlbbtn_Close_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.panel2);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 363);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlSearch.Size = new System.Drawing.Size(779, 30);
            this.pnlSearch.TabIndex = 8;
            // 
            // lblFirstNameValue
            // 
            this.lblFirstNameValue.AutoSize = true;
            this.lblFirstNameValue.BackColor = System.Drawing.Color.Transparent;
            this.lblFirstNameValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstNameValue.Location = new System.Drawing.Point(97, 31);
            this.lblFirstNameValue.Name = "lblFirstNameValue";
            this.lblFirstNameValue.Size = new System.Drawing.Size(81, 14);
            this.lblFirstNameValue.TabIndex = 11;
            this.lblFirstNameValue.Text = "Patient Name";
            // 
            // lblLastNameValue
            // 
            this.lblLastNameValue.AutoSize = true;
            this.lblLastNameValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLastNameValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastNameValue.Location = new System.Drawing.Point(97, 77);
            this.lblLastNameValue.Name = "lblLastNameValue";
            this.lblLastNameValue.Size = new System.Drawing.Size(81, 14);
            this.lblLastNameValue.TabIndex = 10;
            this.lblLastNameValue.Text = "Patient Name";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(23, 77);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 14);
            this.label31.TabIndex = 9;
            this.label31.Text = "Last Name :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(23, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 14);
            this.label20.TabIndex = 8;
            this.label20.Text = "First Name :";
            // 
            // lblGenderValue
            // 
            this.lblGenderValue.AutoSize = true;
            this.lblGenderValue.BackColor = System.Drawing.Color.Transparent;
            this.lblGenderValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenderValue.Location = new System.Drawing.Point(97, 123);
            this.lblGenderValue.Name = "lblGenderValue";
            this.lblGenderValue.Size = new System.Drawing.Size(47, 14);
            this.lblGenderValue.TabIndex = 7;
            this.lblGenderValue.Text = "Gender";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(40, 123);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(55, 14);
            this.label38.TabIndex = 6;
            this.label38.Text = "Gender :";
            // 
            // lblPatientDOBValue
            // 
            this.lblPatientDOBValue.AutoSize = true;
            this.lblPatientDOBValue.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientDOBValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientDOBValue.Location = new System.Drawing.Point(97, 100);
            this.lblPatientDOBValue.Name = "lblPatientDOBValue";
            this.lblPatientDOBValue.Size = new System.Drawing.Size(31, 14);
            this.lblPatientDOBValue.TabIndex = 5;
            this.lblPatientDOBValue.Text = "DOB";
            // 
            // lblPatientDOB
            // 
            this.lblPatientDOB.AutoSize = true;
            this.lblPatientDOB.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientDOB.Location = new System.Drawing.Point(56, 100);
            this.lblPatientDOB.Name = "lblPatientDOB";
            this.lblPatientDOB.Size = new System.Drawing.Size(39, 14);
            this.lblPatientDOB.TabIndex = 4;
            this.lblPatientDOB.Text = "DOB :";
            // 
            // lblMiddleNameValue
            // 
            this.lblMiddleNameValue.AutoSize = true;
            this.lblMiddleNameValue.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddleNameValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiddleNameValue.Location = new System.Drawing.Point(97, 54);
            this.lblMiddleNameValue.Name = "lblMiddleNameValue";
            this.lblMiddleNameValue.Size = new System.Drawing.Size(81, 14);
            this.lblMiddleNameValue.TabIndex = 4;
            this.lblMiddleNameValue.Text = "Patient Name";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(11, 54);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(84, 14);
            this.lblPatientName.TabIndex = 4;
            this.lblPatientName.Text = "Middle Name :";
            // 
            // lblRefReqLastName
            // 
            this.lblRefReqLastName.AutoSize = true;
            this.lblRefReqLastName.BackColor = System.Drawing.Color.Transparent;
            this.lblRefReqLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefReqLastName.ForeColor = System.Drawing.Color.Green;
            this.lblRefReqLastName.Location = new System.Drawing.Point(123, 75);
            this.lblRefReqLastName.Name = "lblRefReqLastName";
            this.lblRefReqLastName.Size = new System.Drawing.Size(81, 14);
            this.lblRefReqLastName.TabIndex = 15;
            this.lblRefReqLastName.Text = "Patient Name";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(47, 75);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(72, 14);
            this.label43.TabIndex = 14;
            this.label43.Text = "Last Name :";
            // 
            // lblRefReqFirstName
            // 
            this.lblRefReqFirstName.AutoSize = true;
            this.lblRefReqFirstName.BackColor = System.Drawing.Color.Transparent;
            this.lblRefReqFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefReqFirstName.ForeColor = System.Drawing.Color.Green;
            this.lblRefReqFirstName.Location = new System.Drawing.Point(123, 31);
            this.lblRefReqFirstName.Name = "lblRefReqFirstName";
            this.lblRefReqFirstName.Size = new System.Drawing.Size(81, 14);
            this.lblRefReqFirstName.TabIndex = 13;
            this.lblRefReqFirstName.Text = "Patient Name";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(47, 31);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(72, 14);
            this.label41.TabIndex = 12;
            this.label41.Text = "First Name :";
            // 
            // lblRefReqGender
            // 
            this.lblRefReqGender.AutoSize = true;
            this.lblRefReqGender.BackColor = System.Drawing.Color.Transparent;
            this.lblRefReqGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefReqGender.ForeColor = System.Drawing.Color.Green;
            this.lblRefReqGender.Location = new System.Drawing.Point(123, 119);
            this.lblRefReqGender.Name = "lblRefReqGender";
            this.lblRefReqGender.Size = new System.Drawing.Size(47, 14);
            this.lblRefReqGender.TabIndex = 9;
            this.lblRefReqGender.Text = "Gender";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(64, 119);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(55, 14);
            this.label40.TabIndex = 8;
            this.label40.Text = "Gender :";
            // 
            // lblRefReqDOB
            // 
            this.lblRefReqDOB.AutoSize = true;
            this.lblRefReqDOB.BackColor = System.Drawing.Color.Transparent;
            this.lblRefReqDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefReqDOB.ForeColor = System.Drawing.Color.Green;
            this.lblRefReqDOB.Location = new System.Drawing.Point(123, 97);
            this.lblRefReqDOB.Name = "lblRefReqDOB";
            this.lblRefReqDOB.Size = new System.Drawing.Size(31, 14);
            this.lblRefReqDOB.TabIndex = 5;
            this.lblRefReqDOB.Text = "DOB";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(80, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 14);
            this.label21.TabIndex = 4;
            this.label21.Text = "DOB :";
            // 
            // lblRefReqMiddleName
            // 
            this.lblRefReqMiddleName.AutoSize = true;
            this.lblRefReqMiddleName.BackColor = System.Drawing.Color.Transparent;
            this.lblRefReqMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefReqMiddleName.ForeColor = System.Drawing.Color.Green;
            this.lblRefReqMiddleName.Location = new System.Drawing.Point(123, 53);
            this.lblRefReqMiddleName.Name = "lblRefReqMiddleName";
            this.lblRefReqMiddleName.Size = new System.Drawing.Size(81, 14);
            this.lblRefReqMiddleName.TabIndex = 4;
            this.lblRefReqMiddleName.Text = "Patient Name";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(35, 53);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(84, 14);
            this.label27.TabIndex = 4;
            this.label27.Text = "Middle Name :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel12);
            this.panel9.Controls.Add(this.panel11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(773, 242);
            this.panel9.TabIndex = 10;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel15);
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(382, 0);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel12.Size = new System.Drawing.Size(391, 242);
            this.panel12.TabIndex = 16;
            // 
            // panel15
            // 
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.Controls.Add(this.trvMedication);
            this.panel15.Controls.Add(this.c1ActiveMedication);
            this.panel15.Controls.Add(this.label42);
            this.panel15.Controls.Add(this.label46);
            this.panel15.Controls.Add(this.label47);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(5, 145);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(386, 97);
            this.panel15.TabIndex = 17;
            // 
            // trvMedication
            // 
            this.trvMedication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.trvMedication.Location = new System.Drawing.Point(1, 21);
            this.trvMedication.Name = "trvMedication";
            this.trvMedication.Size = new System.Drawing.Size(138, 76);
            this.trvMedication.TabIndex = 22;
            this.trvMedication.Visible = false;
            this.trvMedication.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvMedication_BeforeCollapse);
            // 
            // c1ActiveMedication
            // 
            this.c1ActiveMedication.AllowEditing = false;
            this.c1ActiveMedication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ActiveMedication.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ActiveMedication.ColumnInfo = "2,0,0,0,0,105,Columns:0{Width:263;Name:\"Col_Medication\";Caption:\"Medication\";}\t1{" +
    "Name:\"Col_Quantity\";Caption:\"Quantity\";}\t";
            this.c1ActiveMedication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ActiveMedication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ActiveMedication.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1ActiveMedication.Location = new System.Drawing.Point(1, 0);
            this.c1ActiveMedication.Name = "c1ActiveMedication";
            this.c1ActiveMedication.Rows.Count = 1;
            this.c1ActiveMedication.Rows.DefaultSize = 21;
            this.c1ActiveMedication.ShowCellLabels = true;
            this.c1ActiveMedication.Size = new System.Drawing.Size(384, 96);
            this.c1ActiveMedication.StyleInfo = resources.GetString("c1ActiveMedication.StyleInfo");
            this.c1ActiveMedication.TabIndex = 6;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Left;
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 96);
            this.label42.TabIndex = 3;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(385, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 96);
            this.label46.TabIndex = 2;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Location = new System.Drawing.Point(0, 96);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(386, 1);
            this.label47.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.label44);
            this.panel13.Controls.Add(this.lblFirstNameValue);
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Controls.Add(this.label20);
            this.panel13.Controls.Add(this.label29);
            this.panel13.Controls.Add(this.lblLastNameValue);
            this.panel13.Controls.Add(this.label36);
            this.panel13.Controls.Add(this.lblPatientName);
            this.panel13.Controls.Add(this.label31);
            this.panel13.Controls.Add(this.lblMiddleNameValue);
            this.panel13.Controls.Add(this.label48);
            this.panel13.Controls.Add(this.lblGenderValue);
            this.panel13.Controls.Add(this.lblPatientDOB);
            this.panel13.Controls.Add(this.label38);
            this.panel13.Controls.Add(this.lblPatientDOBValue);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(5, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(386, 145);
            this.panel13.TabIndex = 16;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label44.Location = new System.Drawing.Point(1, 144);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(384, 1);
            this.label44.TabIndex = 17;
            // 
            // panel14
            // 
            this.panel14.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.label13);
            this.panel14.Controls.Add(this.label15);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.ForeColor = System.Drawing.Color.White;
            this.panel14.Location = new System.Drawing.Point(1, 1);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(384, 23);
            this.panel14.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(384, 22);
            this.label13.TabIndex = 4;
            this.label13.Text = "Selected Patient";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(0, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(384, 1);
            this.label15.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 144);
            this.label29.TabIndex = 3;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Location = new System.Drawing.Point(385, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 144);
            this.label36.TabIndex = 2;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(0, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(386, 1);
            this.label48.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.panel5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(382, 242);
            this.panel11.TabIndex = 16;
            // 
            // panel5
            // 
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.lblRequestedQty);
            this.panel5.Controls.Add(this.lblRequested);
            this.panel5.Controls.Add(this.lblQuantity);
            this.panel5.Controls.Add(this.lblMedication);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.lblRefReqLastName);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label27);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.label43);
            this.panel5.Controls.Add(this.lblRefReqMiddleName);
            this.panel5.Controls.Add(this.lblRefReqFirstName);
            this.panel5.Controls.Add(this.label32);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.lblRefReqGender);
            this.panel5.Controls.Add(this.lblRefReqDOB);
            this.panel5.Controls.Add(this.label40);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(382, 242);
            this.panel5.TabIndex = 15;
            // 
            // lblRequestedQty
            // 
            this.lblRequestedQty.AutoSize = true;
            this.lblRequestedQty.BackColor = System.Drawing.Color.Transparent;
            this.lblRequestedQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestedQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRequestedQty.Location = new System.Drawing.Point(123, 163);
            this.lblRequestedQty.Name = "lblRequestedQty";
            this.lblRequestedQty.Size = new System.Drawing.Size(85, 14);
            this.lblRequestedQty.TabIndex = 21;
            this.lblRequestedQty.Text = "30 Day Supply";
            // 
            // lblRequested
            // 
            this.lblRequested.AutoSize = true;
            this.lblRequested.BackColor = System.Drawing.Color.Transparent;
            this.lblRequested.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequested.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRequested.Location = new System.Drawing.Point(123, 141);
            this.lblRequested.Name = "lblRequested";
            this.lblRequested.Size = new System.Drawing.Size(209, 14);
            this.lblRequested.TabIndex = 20;
            this.lblRequested.Text = "Zioptan 0.015% Solution Ophthalmic";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(57, 163);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(62, 14);
            this.lblQuantity.TabIndex = 19;
            this.lblQuantity.Text = "Quantity :";
            // 
            // lblMedication
            // 
            this.lblMedication.AutoSize = true;
            this.lblMedication.BackColor = System.Drawing.Color.Transparent;
            this.lblMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedication.Location = new System.Drawing.Point(18, 141);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(101, 14);
            this.lblMedication.TabIndex = 18;
            this.lblMedication.Text = "Requested Med :";
            // 
            // panel8
            // 
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.c1RequestedMedication);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.label33);
            this.panel8.Controls.Add(this.label35);
            this.panel8.Location = new System.Drawing.Point(319, 33);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(46, 27);
            this.panel8.TabIndex = 15;
            this.panel8.Visible = false;
            // 
            // c1RequestedMedication
            // 
            this.c1RequestedMedication.AllowEditing = false;
            this.c1RequestedMedication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RequestedMedication.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RequestedMedication.ColumnInfo = "2,0,0,0,0,105,Columns:0{Width:263;Name:\"Col_Medication\";Caption:\"Medication\";}\t1{" +
    "Name:\"Col_Quantity\";Caption:\"Quantity\";}\t";
            this.c1RequestedMedication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RequestedMedication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RequestedMedication.Location = new System.Drawing.Point(1, 0);
            this.c1RequestedMedication.Name = "c1RequestedMedication";
            this.c1RequestedMedication.Rows.DefaultSize = 21;
            this.c1RequestedMedication.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1RequestedMedication.ShowCellLabels = true;
            this.c1RequestedMedication.Size = new System.Drawing.Size(44, 26);
            this.c1RequestedMedication.StyleInfo = resources.GetString("c1RequestedMedication.StyleInfo");
            this.c1RequestedMedication.TabIndex = 16;
            this.c1RequestedMedication.Tree.LineColor = System.Drawing.Color.DarkKhaki;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 26);
            this.label14.TabIndex = 3;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.label33.Location = new System.Drawing.Point(45, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 26);
            this.label33.TabIndex = 2;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label35.Location = new System.Drawing.Point(0, 26);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(46, 1);
            this.label35.TabIndex = 1;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.Location = new System.Drawing.Point(1, 241);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(380, 1);
            this.label30.TabIndex = 17;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblPatientInfo);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.ForeColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(1, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(380, 23);
            this.panel6.TabIndex = 16;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(380, 22);
            this.lblPatientInfo.TabIndex = 4;
            this.lblPatientInfo.Text = "Refill Request Patient Info";
            this.lblPatientInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Location = new System.Drawing.Point(0, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(380, 1);
            this.label12.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 241);
            this.label16.TabIndex = 3;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(381, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 241);
            this.label25.TabIndex = 2;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(382, 1);
            this.label32.TabIndex = 0;
            // 
            // pnlSelectedRefill
            // 
            this.pnlSelectedRefill.Controls.Add(this.panel9);
            this.pnlSelectedRefill.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectedRefill.Location = new System.Drawing.Point(0, 118);
            this.pnlSelectedRefill.Name = "pnlSelectedRefill";
            this.pnlSelectedRefill.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSelectedRefill.Size = new System.Drawing.Size(779, 245);
            this.pnlSelectedRefill.TabIndex = 11;
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Panel7.Controls.Add(this.Label112);
            this.Panel7.Controls.Add(this.Label113);
            this.Panel7.Controls.Add(this.lblTextContent);
            this.Panel7.Controls.Add(this.Label115);
            this.Panel7.Controls.Add(this.Label114);
            this.Panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel7.Location = new System.Drawing.Point(3, 3);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(773, 58);
            this.Panel7.TabIndex = 31;
            // 
            // Label112
            // 
            this.Label112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(68)))), ((int)(((byte)(3)))));
            this.Label112.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label112.Enabled = false;
            this.Label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label112.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label112.Location = new System.Drawing.Point(1, 57);
            this.Label112.Name = "Label112";
            this.Label112.Size = new System.Drawing.Size(771, 1);
            this.Label112.TabIndex = 27;
            this.Label112.Text = "From";
            this.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label113
            // 
            this.Label113.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(68)))), ((int)(((byte)(3)))));
            this.Label113.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label113.Enabled = false;
            this.Label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label113.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label113.Location = new System.Drawing.Point(1, 0);
            this.Label113.Name = "Label113";
            this.Label113.Size = new System.Drawing.Size(771, 1);
            this.Label113.TabIndex = 28;
            this.Label113.Text = "From";
            this.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTextContent
            // 
            this.lblTextContent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(68)))), ((int)(((byte)(3)))));
            this.lblTextContent.Location = new System.Drawing.Point(6, 7);
            this.lblTextContent.Name = "lblTextContent";
            this.lblTextContent.Size = new System.Drawing.Size(707, 45);
            this.lblTextContent.TabIndex = 32;
            this.lblTextContent.Text = resources.GetString("lblTextContent.Text");
            // 
            // Label115
            // 
            this.Label115.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(68)))), ((int)(((byte)(3)))));
            this.Label115.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label115.Enabled = false;
            this.Label115.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label115.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label115.Location = new System.Drawing.Point(0, 0);
            this.Label115.Name = "Label115";
            this.Label115.Size = new System.Drawing.Size(1, 58);
            this.Label115.TabIndex = 30;
            this.Label115.Text = "From";
            this.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label114
            // 
            this.Label114.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(68)))), ((int)(((byte)(3)))));
            this.Label114.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label114.Enabled = false;
            this.Label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label114.Location = new System.Drawing.Point(772, 0);
            this.Label114.Name = "Label114";
            this.Label114.Size = new System.Drawing.Size(1, 58);
            this.Label114.TabIndex = 29;
            this.Label114.Text = "From";
            this.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlUnsentlabel
            // 
            this.pnlUnsentlabel.BackColor = System.Drawing.Color.Transparent;
            this.pnlUnsentlabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlUnsentlabel.Controls.Add(this.Panel7);
            this.pnlUnsentlabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUnsentlabel.Location = new System.Drawing.Point(0, 54);
            this.pnlUnsentlabel.Name = "pnlUnsentlabel";
            this.pnlUnsentlabel.Padding = new System.Windows.Forms.Padding(3);
            this.pnlUnsentlabel.Size = new System.Drawing.Size(779, 64);
            this.pnlUnsentlabel.TabIndex = 27;
            // 
            // frmMapUnMatchPatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(779, 596);
            this.Controls.Add(this.pnlPatientList);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlSelectedRefill);
            this.Controls.Add(this.pnlUnsentlabel);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMapUnMatchPatients";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Not Found for Refill Request - Select a Patient, Create a New Patient, or" +
    " Deny the Refill Request";
            this.Load += new System.EventHandler(this.frmMapUnMatchPatients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlPatientList.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ts_Main.ResumeLayout(false);
            this.ts_Main.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ActiveMedication)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1RequestedMedication)).EndInit();
            this.panel6.ResumeLayout(false);
            this.pnlSelectedRefill.ResumeLayout(false);
            this.Panel7.ResumeLayout(false);
            this.pnlUnsentlabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Main;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlPatientList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlToolstrip;
        private System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.ToolStripButton tlbbtn_MatchPatient;
        private System.Windows.Forms.Label lblPatientDOB;
        private System.Windows.Forms.Label lblMiddleNameValue;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientDOBValue;
        internal System.Windows.Forms.ToolStripButton tlbbtn_RegisterPatient;
        internal System.Windows.Forms.ToolStripButton tlbbtn_DiscardPatient;
        private System.Windows.Forms.Label lblRefReqDOB;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblRefReqMiddleName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel pnlSelectedRefill;
        private System.Windows.Forms.Label lblGenderValue;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblRefReqGender;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblFirstNameValue;
        private System.Windows.Forms.Label lblLastNameValue;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblRefReqLastName;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lblRefReqFirstName;
        private System.Windows.Forms.Label label41;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Panel Panel7;
        private System.Windows.Forms.Label lblTextContent;
        internal System.Windows.Forms.Label Label112;
        internal System.Windows.Forms.Label Label113;
        internal System.Windows.Forms.Label Label115;
        internal System.Windows.Forms.Label Label114;
        internal System.Windows.Forms.Panel pnlUnsentlabel;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1ActiveMedication;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1RequestedMedication;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRequestedQty;
        private System.Windows.Forms.Label lblRequested;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblMedication;
        private System.Windows.Forms.TreeView trvMedication;
    }
}