namespace gloCommunity.UserControls
{
    partial class UCAppointConf
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAppointConf));
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.flxfollup = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.CalendarTemplate = new Janus.Windows.Schedule.Schedule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.trvAppointmentBook = new System.Windows.Forms.TreeView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnltrvSerchAppBook = new System.Windows.Forms.Panel();
            this.txtSearchAppBook = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnltrvSerchAppBookBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookRightBrd = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.flxDept = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.flxLoc = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.flxApptstat = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flxAppt = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flxPrb = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flxApptblk = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flxRes = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabblconf = new System.Windows.Forms.TabControl();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trvappconf = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxfollup)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnltrvSerchAppBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxDept)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxLoc)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxApptstat)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxAppt)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxPrb)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxApptblk)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxRes)).BeginInit();
            this.tabblconf.SuspendLayout();
            this.pnlleft.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.flxfollup);
            this.tabPage10.Location = new System.Drawing.Point(4, 23);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(564, 593);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "FollowUp";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // flxfollup
            // 
            this.flxfollup.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxfollup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxfollup.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxfollup.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxfollup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxfollup.ExtendLastCol = true;
            this.flxfollup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxfollup.Location = new System.Drawing.Point(0, 0);
            this.flxfollup.Name = "flxfollup";
            this.flxfollup.Rows.Count = 1;
            this.flxfollup.Rows.DefaultSize = 21;
            this.flxfollup.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxfollup.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxfollup.Size = new System.Drawing.Size(564, 593);
            this.flxfollup.StyleInfo = resources.GetString("flxfollup.StyleInfo");
            this.flxfollup.TabIndex = 7;
            this.flxfollup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxfollup_MouseDown);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.CalendarTemplate);
            this.tabPage8.Controls.Add(this.panel1);
            this.tabPage8.Location = new System.Drawing.Point(4, 23);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(564, 593);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Template";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // CalendarTemplate
            // 
            this.CalendarTemplate.AcceptsTab = false;
            this.CalendarTemplate.AddNewMode = Janus.Windows.Schedule.AddNewMode.Manual;
            this.CalendarTemplate.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None;
            this.CalendarTemplate.AllowEdit = false;
            this.CalendarTemplate.BorderStyle = Janus.Windows.Schedule.BorderStyle.None;
            this.CalendarTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarTemplate.HorizontalScrollPosition = 0;
            this.CalendarTemplate.Location = new System.Drawing.Point(237, 0);
            this.CalendarTemplate.Name = "CalendarTemplate";
            this.CalendarTemplate.Size = new System.Drawing.Size(327, 593);
            this.CalendarTemplate.TabIndex = 2;
            this.CalendarTemplate.VerticalScrollPosition = 16;
            this.CalendarTemplate.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlBase);
            this.panel1.Controls.Add(this.pnltrvSerchAppBook);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 593);
            this.panel1.TabIndex = 11;
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBase.Controls.Add(this.trvAppointmentBook);
            this.pnlBase.Controls.Add(this.panel5);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBase.Location = new System.Drawing.Point(0, 28);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(237, 565);
            this.pnlBase.TabIndex = 17;
            // 
            // trvAppointmentBook
            // 
            this.trvAppointmentBook.BackColor = System.Drawing.Color.White;
            this.trvAppointmentBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvAppointmentBook.CheckBoxes = true;
            this.trvAppointmentBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAppointmentBook.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvAppointmentBook.ForeColor = System.Drawing.Color.Black;
            this.trvAppointmentBook.HideSelection = false;
            this.trvAppointmentBook.Indent = 20;
            this.trvAppointmentBook.ItemHeight = 20;
            this.trvAppointmentBook.Location = new System.Drawing.Point(0, 0);
            this.trvAppointmentBook.Name = "trvAppointmentBook";
            this.trvAppointmentBook.ShowLines = false;
            this.trvAppointmentBook.ShowNodeToolTips = true;
            this.trvAppointmentBook.ShowRootLines = false;
            this.trvAppointmentBook.Size = new System.Drawing.Size(237, 565);
            this.trvAppointmentBook.TabIndex = 0;
            this.trvAppointmentBook.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvAppointmentBook_AfterSelect);
            this.trvAppointmentBook.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvAppointmentBook_NodeMouseClick);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(68, 131);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(24, 100);
            this.panel5.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 4);
            this.label8.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(4, 98);
            this.label9.TabIndex = 6;
            // 
            // pnltrvSerchAppBook
            // 
            this.pnltrvSerchAppBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvSerchAppBook.Controls.Add(this.txtSearchAppBook);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnltrvSerchAppBook.Controls.Add(this.PicBx_Search);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookBottomBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookTopBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookLeftBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookRightBrd);
            this.pnltrvSerchAppBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltrvSerchAppBook.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltrvSerchAppBook.ForeColor = System.Drawing.Color.Black;
            this.pnltrvSerchAppBook.Location = new System.Drawing.Point(0, 0);
            this.pnltrvSerchAppBook.Name = "pnltrvSerchAppBook";
            this.pnltrvSerchAppBook.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnltrvSerchAppBook.Size = new System.Drawing.Size(237, 28);
            this.pnltrvSerchAppBook.TabIndex = 16;
            // 
            // txtSearchAppBook
            // 
            this.txtSearchAppBook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchAppBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchAppBook.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAppBook.Location = new System.Drawing.Point(29, 5);
            this.txtSearchAppBook.Name = "txtSearchAppBook";
            this.txtSearchAppBook.Size = new System.Drawing.Size(207, 15);
            this.txtSearchAppBook.TabIndex = 7;
            this.txtSearchAppBook.Visible = false;
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(207, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 20);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(207, 4);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 1);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 23);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            this.PicBx_Search.Visible = false;
            // 
            // lbl_pnltrvSerchAppBookBottomBrd
            // 
            this.lbl_pnltrvSerchAppBookBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnltrvSerchAppBookBottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_pnltrvSerchAppBookBottomBrd.Name = "lbl_pnltrvSerchAppBookBottomBrd";
            this.lbl_pnltrvSerchAppBookBottomBrd.Size = new System.Drawing.Size(235, 1);
            this.lbl_pnltrvSerchAppBookBottomBrd.TabIndex = 35;
            this.lbl_pnltrvSerchAppBookBottomBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookTopBrd
            // 
            this.lbl_pnltrvSerchAppBookTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnltrvSerchAppBookTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnltrvSerchAppBookTopBrd.Name = "lbl_pnltrvSerchAppBookTopBrd";
            this.lbl_pnltrvSerchAppBookTopBrd.Size = new System.Drawing.Size(235, 1);
            this.lbl_pnltrvSerchAppBookTopBrd.TabIndex = 36;
            this.lbl_pnltrvSerchAppBookTopBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookLeftBrd
            // 
            this.lbl_pnltrvSerchAppBookLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnltrvSerchAppBookLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnltrvSerchAppBookLeftBrd.Name = "lbl_pnltrvSerchAppBookLeftBrd";
            this.lbl_pnltrvSerchAppBookLeftBrd.Size = new System.Drawing.Size(1, 25);
            this.lbl_pnltrvSerchAppBookLeftBrd.TabIndex = 39;
            this.lbl_pnltrvSerchAppBookLeftBrd.Text = "label4";
            // 
            // lbl_pnltrvSerchAppBookRightBrd
            // 
            this.lbl_pnltrvSerchAppBookRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnltrvSerchAppBookRightBrd.Location = new System.Drawing.Point(236, 0);
            this.lbl_pnltrvSerchAppBookRightBrd.Name = "lbl_pnltrvSerchAppBookRightBrd";
            this.lbl_pnltrvSerchAppBookRightBrd.Size = new System.Drawing.Size(1, 25);
            this.lbl_pnltrvSerchAppBookRightBrd.TabIndex = 40;
            this.lbl_pnltrvSerchAppBookRightBrd.Text = "label4";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.flxDept);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(564, 593);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Department";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // flxDept
            // 
            this.flxDept.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxDept.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxDept.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxDept.ExtendLastCol = true;
            this.flxDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxDept.Location = new System.Drawing.Point(0, 0);
            this.flxDept.Name = "flxDept";
            this.flxDept.Rows.Count = 1;
            this.flxDept.Rows.DefaultSize = 21;
            this.flxDept.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxDept.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxDept.Size = new System.Drawing.Size(564, 593);
            this.flxDept.StyleInfo = resources.GetString("flxDept.StyleInfo");
            this.flxDept.TabIndex = 5;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.flxLoc);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(564, 593);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Location";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // flxLoc
            // 
            this.flxLoc.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxLoc.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxLoc.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxLoc.ExtendLastCol = true;
            this.flxLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxLoc.Location = new System.Drawing.Point(0, 0);
            this.flxLoc.Name = "flxLoc";
            this.flxLoc.Rows.Count = 1;
            this.flxLoc.Rows.DefaultSize = 21;
            this.flxLoc.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxLoc.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxLoc.Size = new System.Drawing.Size(564, 593);
            this.flxLoc.StyleInfo = resources.GetString("flxLoc.StyleInfo");
            this.flxLoc.TabIndex = 5;
            this.flxLoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxLoc_MouseDown);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.flxApptstat);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(564, 593);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Appointment Status";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // flxApptstat
            // 
            this.flxApptstat.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxApptstat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxApptstat.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxApptstat.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxApptstat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxApptstat.ExtendLastCol = true;
            this.flxApptstat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxApptstat.Location = new System.Drawing.Point(0, 0);
            this.flxApptstat.Name = "flxApptstat";
            this.flxApptstat.Rows.Count = 1;
            this.flxApptstat.Rows.DefaultSize = 21;
            this.flxApptstat.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxApptstat.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxApptstat.Size = new System.Drawing.Size(564, 593);
            this.flxApptstat.StyleInfo = resources.GetString("flxApptstat.StyleInfo");
            this.flxApptstat.TabIndex = 5;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flxAppt);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(564, 593);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Appointment Type";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // flxAppt
            // 
            this.flxAppt.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxAppt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxAppt.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxAppt.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxAppt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxAppt.ExtendLastCol = true;
            this.flxAppt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxAppt.Location = new System.Drawing.Point(0, 0);
            this.flxAppt.Name = "flxAppt";
            this.flxAppt.Rows.Count = 1;
            this.flxAppt.Rows.DefaultSize = 21;
            this.flxAppt.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxAppt.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxAppt.Size = new System.Drawing.Size(564, 593);
            this.flxAppt.StyleInfo = resources.GetString("flxAppt.StyleInfo");
            this.flxAppt.TabIndex = 5;
            this.flxAppt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxAppt_MouseDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flxPrb);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(564, 593);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Problem Type";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // flxPrb
            // 
            this.flxPrb.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxPrb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxPrb.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxPrb.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxPrb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxPrb.ExtendLastCol = true;
            this.flxPrb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxPrb.Location = new System.Drawing.Point(1, 1);
            this.flxPrb.Name = "flxPrb";
            this.flxPrb.Rows.Count = 1;
            this.flxPrb.Rows.DefaultSize = 21;
            this.flxPrb.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxPrb.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxPrb.Size = new System.Drawing.Size(562, 591);
            this.flxPrb.StyleInfo = resources.GetString("flxPrb.StyleInfo");
            this.flxPrb.TabIndex = 14;
            this.flxPrb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxPrb_MouseDown);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(563, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 591);
            this.label17.TabIndex = 13;
            this.label17.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 591);
            this.label16.TabIndex = 12;
            this.label16.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 592);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(564, 1);
            this.label15.TabIndex = 11;
            this.label15.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(564, 1);
            this.label14.TabIndex = 10;
            this.label14.Text = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.flxApptblk);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(564, 593);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Appointment Block Type";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(563, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 591);
            this.label19.TabIndex = 13;
            this.label19.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 591);
            this.label18.TabIndex = 12;
            this.label18.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 592);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(564, 1);
            this.label4.TabIndex = 11;
            this.label4.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(564, 1);
            this.label3.TabIndex = 10;
            this.label3.Text = "label1";
            // 
            // flxApptblk
            // 
            this.flxApptblk.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxApptblk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxApptblk.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxApptblk.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxApptblk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxApptblk.ExtendLastCol = true;
            this.flxApptblk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxApptblk.Location = new System.Drawing.Point(0, 0);
            this.flxApptblk.Name = "flxApptblk";
            this.flxApptblk.Rows.Count = 1;
            this.flxApptblk.Rows.DefaultSize = 21;
            this.flxApptblk.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxApptblk.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxApptblk.Size = new System.Drawing.Size(564, 593);
            this.flxApptblk.StyleInfo = resources.GetString("flxApptblk.StyleInfo");
            this.flxApptblk.TabIndex = 5;
            this.flxApptblk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxApptblk_MouseDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.flxRes);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(564, 593);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Resource";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(563, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 591);
            this.label23.TabIndex = 13;
            this.label23.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 591);
            this.label22.TabIndex = 12;
            this.label22.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 592);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(564, 1);
            this.label2.TabIndex = 11;
            this.label2.Text = "label1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 1);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // flxRes
            // 
            this.flxRes.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxRes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxRes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxRes.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxRes.ExtendLastCol = true;
            this.flxRes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxRes.Location = new System.Drawing.Point(0, 0);
            this.flxRes.Name = "flxRes";
            this.flxRes.Rows.Count = 1;
            this.flxRes.Rows.DefaultSize = 21;
            this.flxRes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxRes.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxRes.Size = new System.Drawing.Size(564, 593);
            this.flxRes.StyleInfo = resources.GetString("flxRes.StyleInfo");
            this.flxRes.TabIndex = 4;
            // 
            // tabblconf
            // 
            this.tabblconf.Controls.Add(this.tabPage1);
            this.tabblconf.Controls.Add(this.tabPage2);
            this.tabblconf.Controls.Add(this.tabPage3);
            this.tabblconf.Controls.Add(this.tabPage4);
            this.tabblconf.Controls.Add(this.tabPage5);
            this.tabblconf.Controls.Add(this.tabPage6);
            this.tabblconf.Controls.Add(this.tabPage7);
            this.tabblconf.Controls.Add(this.tabPage8);
            this.tabblconf.Controls.Add(this.tabPage10);
            this.tabblconf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabblconf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabblconf.Location = new System.Drawing.Point(3, 3);
            this.tabblconf.Name = "tabblconf";
            this.tabblconf.SelectedIndex = 0;
            this.tabblconf.Size = new System.Drawing.Size(572, 620);
            this.tabblconf.TabIndex = 15;
            this.tabblconf.SelectedIndexChanged += new System.EventHandler(this.tabblconf_SelectedIndexChanged);
            // 
            // pnlleft
            // 
            this.pnlleft.Controls.Add(this.panel3);
            this.pnlleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlleft.Location = new System.Drawing.Point(28, 0);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlleft.Size = new System.Drawing.Size(220, 626);
            this.pnlleft.TabIndex = 16;
            this.pnlleft.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trvappconf);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(217, 620);
            this.panel3.TabIndex = 25;
            // 
            // trvappconf
            // 
            this.trvappconf.BackColor = System.Drawing.Color.White;
            this.trvappconf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvappconf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvappconf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvappconf.ForeColor = System.Drawing.Color.Black;
            this.trvappconf.HideSelection = false;
            this.trvappconf.Indent = 20;
            this.trvappconf.ItemHeight = 20;
            this.trvappconf.Location = new System.Drawing.Point(0, 0);
            this.trvappconf.Name = "trvappconf";
            this.trvappconf.ShowLines = false;
            this.trvappconf.ShowNodeToolTips = true;
            this.trvappconf.ShowRootLines = false;
            this.trvappconf.Size = new System.Drawing.Size(217, 617);
            this.trvappconf.TabIndex = 28;
            this.trvappconf.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvappconf_NodeMouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabblconf);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(248, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel2.Size = new System.Drawing.Size(575, 626);
            this.panel2.TabIndex = 17;
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label5);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 626);
            this.pnltls.TabIndex = 102;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(4, 622);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 1);
            this.label5.TabIndex = 144;
            // 
            // tlsgloCommunity
            // 
            this.tlsgloCommunity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tlsgloCommunity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsgloCommunity.BackgroundImage")));
            this.tlsgloCommunity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsgloCommunity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsgloCommunity.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsgloCommunity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbClinicRepository,
            this.tlbGlobalRepository});
            this.tlsgloCommunity.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tlsgloCommunity.Location = new System.Drawing.Point(4, 23);
            this.tlsgloCommunity.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 600);
            this.tlsgloCommunity.TabIndex = 21;
            this.tlsgloCommunity.Text = "toolStrip1";
            this.tlsgloCommunity.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tlbClinicRepository
            // 
            this.tlbClinicRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbClinicRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbClinicRepository.Image")));
            this.tlbClinicRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClinicRepository.Name = "tlbClinicRepository";
            this.tlbClinicRepository.Size = new System.Drawing.Size(21, 154);
            this.tlbClinicRepository.Text = "  Practice Repository";
            this.tlbClinicRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbClinicRepository.Click += new System.EventHandler(this.tlbClinicRepository_Click);
            // 
            // tlbGlobalRepository
            // 
            this.tlbGlobalRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbGlobalRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbGlobalRepository.Image")));
            this.tlbGlobalRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbGlobalRepository.Name = "tlbGlobalRepository";
            this.tlbGlobalRepository.Size = new System.Drawing.Size(21, 143);
            this.tlbGlobalRepository.Text = "  Global Repository";
            this.tlbGlobalRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbGlobalRepository.Click += new System.EventHandler(this.tlbGlobalRepository_Click);
            // 
            // btn_Right1
            // 
            this.btn_Right1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Right1.BackgroundImage")));
            this.btn_Right1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right1.FlatAppearance.BorderSize = 0;
            this.btn_Right1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Right1.Location = new System.Drawing.Point(4, 1);
            this.btn_Right1.Name = "btn_Right1";
            this.btn_Right1.Size = new System.Drawing.Size(23, 22);
            this.btn_Right1.TabIndex = 16;
            this.btn_Right1.UseVisualStyleBackColor = false;
            // 
            // lbl_pnlSmallStripLeftBrd
            // 
            this.lbl_pnlSmallStripLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSmallStripLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlSmallStripLeftBrd.Name = "lbl_pnlSmallStripLeftBrd";
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 622);
            this.lbl_pnlSmallStripLeftBrd.TabIndex = 9;
            // 
            // lbl_pnlSmallStripTopBrd
            // 
            this.lbl_pnlSmallStripTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSmallStripTopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSmallStripTopBrd.Name = "lbl_pnlSmallStripTopBrd";
            this.lbl_pnlSmallStripTopBrd.Size = new System.Drawing.Size(24, 1);
            this.lbl_pnlSmallStripTopBrd.TabIndex = 141;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(27, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 623);
            this.label53.TabIndex = 143;
            // 
            // UCAppointConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlleft);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCAppointConf";
            this.Size = new System.Drawing.Size(823, 626);
            this.Load += new System.EventHandler(this.UCAppointConf_Load);
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxfollup)).EndInit();
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlBase.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnltrvSerchAppBook.ResumeLayout(false);
            this.pnltrvSerchAppBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxDept)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxLoc)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxApptstat)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxAppt)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxPrb)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxApptblk)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxRes)).EndInit();
            this.tabblconf.ResumeLayout(false);
            this.pnlleft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage10;
        public C1.Win.C1FlexGrid.C1FlexGrid flxfollup;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage7;
        public C1.Win.C1FlexGrid.C1FlexGrid flxDept;
        private System.Windows.Forms.TabPage tabPage6;
        public C1.Win.C1FlexGrid.C1FlexGrid flxLoc;
        private System.Windows.Forms.TabPage tabPage5;
        public C1.Win.C1FlexGrid.C1FlexGrid flxApptstat;
        private System.Windows.Forms.TabPage tabPage4;
        public C1.Win.C1FlexGrid.C1FlexGrid flxAppt;
        private System.Windows.Forms.TabPage tabPage3;
        public C1.Win.C1FlexGrid.C1FlexGrid flxPrb;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public C1.Win.C1FlexGrid.C1FlexGrid flxApptblk;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public C1.Win.C1FlexGrid.C1FlexGrid flxRes;
        private System.Windows.Forms.TabControl tabblconf;
        public System.Windows.Forms.Panel pnlleft;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.Schedule.Schedule CalendarTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlBase;
        public System.Windows.Forms.TreeView trvAppointmentBook;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Panel pnltrvSerchAppBook;
        private System.Windows.Forms.TextBox txtSearchAppBook;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookBottomBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookTopBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookLeftBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookRightBrd;
        private System.ServiceProcess.ServiceController serviceController1;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label5;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.TreeView trvappconf;

    }
}
