namespace gloAppointmentScheduling
{
    partial class frmViewSchedule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSchedule));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_NewSchedule = new System.Windows.Forms.ToolStripButton();
            this.tsb_EditSchedule = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeleteSchedule = new System.Windows.Forms.ToolStripButton();
            this.tsb_Help = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Today = new System.Windows.Forms.ToolStripButton();
            this.tsb_DayView = new System.Windows.Forms.ToolStripButton();
            this.tsb_WeekView = new System.Windows.Forms.ToolStripButton();
            this.tsb_MonthView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tls_btnTimeNavigation = new System.Windows.Forms.ToolStripDropDownButton();
            this.tls_btnTimeNavigation_5Min = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_btnTimeNavigation_10Min = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_btnTimeNavigation_15Min = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_btnTimeNavigation_30Min = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.juc_Calendar = new Janus.Windows.Schedule.Calendar();
            this.juc_ViewSchedule = new Janus.Windows.Schedule.Schedule();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlCalendarHeader = new System.Windows.Forms.Panel();
            this.btnLeft = new System.Windows.Forms.Button();
            this.lblCalender = new System.Windows.Forms.Label();
            this.lbl_pnlCalendarHeaderBottomBrd = new System.Windows.Forms.Label();
            this.pnlSearchList = new System.Windows.Forms.Panel();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.lbl_pnlSelectionBottomBrd = new System.Windows.Forms.Label();
            this.pnlSelectionBody = new System.Windows.Forms.Panel();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.pnlSelectionHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlSelectionHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblSelection = new System.Windows.Forms.Label();
            this.lbl_pnlSelectionLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSelectionRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSelectionTopBrd = new System.Windows.Forms.Label();
            this.Spt_pnlResourcesTop = new System.Windows.Forms.Splitter();
            this.pnlResources = new System.Windows.Forms.Panel();
            this.lbl_pnlResourcesBottomBrd = new System.Windows.Forms.Label();
            this.pnlResources_trv = new System.Windows.Forms.Panel();
            this.trvResources = new System.Windows.Forms.TreeView();
            this.pnlResourcesHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectResource = new System.Windows.Forms.Button();
            this.btnSelectResource = new System.Windows.Forms.Button();
            this.lbl_pnlResourcesHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblResources = new System.Windows.Forms.Label();
            this.lbl_pnlResourcesLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourcesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourcesTopBrd = new System.Windows.Forms.Label();
            this.Spt_pnlSelectionTop = new System.Windows.Forms.Splitter();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lbl_pnlProviderBottomBrd = new System.Windows.Forms.Label();
            this.pnlProviderBody = new System.Windows.Forms.Panel();
            this.trvProvider = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectProvider = new System.Windows.Forms.Button();
            this.btnSelectProvider = new System.Windows.Forms.Button();
            this.lbl_pnlProviderHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblProviderResources = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTopBrd = new System.Windows.Forms.Label();
            this.Spt_pnlProviderTop = new System.Windows.Forms.Splitter();
            this.pnlCalendar = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlCalendarBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCalendarTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCalendarLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCalendarRightBrd = new System.Windows.Forms.Label();
            this.splSearchList = new System.Windows.Forms.Splitter();
            this.cntMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ModifySchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSearchCriteria = new System.Windows.Forms.Panel();
            this.lbl_pnlSearchCriteriaBottomBrd = new System.Windows.Forms.Label();
            this.cmbResource = new System.Windows.Forms.ComboBox();
            this.lblResource = new System.Windows.Forms.Label();
            this.pnlSearchCriteriaHeader = new System.Windows.Forms.Panel();
            this.lblSerchCriteria = new System.Windows.Forms.Label();
            this.lbl_pnlSearchCriteriaHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchCriterialeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchCriteriaRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchCriteriaTopBrd = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.pnlViewSchedule = new System.Windows.Forms.Panel();
            this.lbl_pnlViewScheduleRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlViewScheduleLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlViewScheduleTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlViewScheduleBottomBrd = new System.Windows.Forms.Label();
            this.pnlSmallStrip = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.ts_SmallStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_SmallStrip_btn_Calendar = new System.Windows.Forms.ToolStripButton();
            this.ts_SmallStrip_btn_Provider = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.spt_pnlViewScheduleTop = new System.Windows.Forms.Splitter();
            this.cmnu_ScheduleNew = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnu_Schedule_New = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_GoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_ScheduleEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnu_Schedule_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_AddNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_Schedule_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.printSchedule = new System.Drawing.Printing.PrintDocument();
            this.ts_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.juc_Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.juc_ViewSchedule)).BeginInit();
            this.pnlCalendarHeader.SuspendLayout();
            this.pnlSearchList.SuspendLayout();
            this.pnlSelection.SuspendLayout();
            this.pnlSelectionBody.SuspendLayout();
            this.pnlSelectionHeader.SuspendLayout();
            this.pnlResources.SuspendLayout();
            this.pnlResources_trv.SuspendLayout();
            this.pnlResourcesHeader.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnlCalendar.SuspendLayout();
            this.cntMenuMain.SuspendLayout();
            this.pnlSearchCriteria.SuspendLayout();
            this.pnlSearchCriteriaHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.pnlViewSchedule.SuspendLayout();
            this.pnlSmallStrip.SuspendLayout();
            this.ts_SmallStrip.SuspendLayout();
            this.cmnu_ScheduleNew.SuspendLayout();
            this.cmnu_ScheduleEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_NewSchedule,
            this.tsb_EditSchedule,
            this.tsb_DeleteSchedule,
            this.tsb_Help,
            this.toolStripSeparator1,
            this.tsb_Today,
            this.tsb_DayView,
            this.tsb_WeekView,
            this.tsb_MonthView,
            this.toolStripSeparator3,
            this.tls_btnTimeNavigation,
            this.toolStripSeparator4,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1095, 53);
            this.ts_Commands.TabIndex = 12;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_NewSchedule
            // 
            this.tsb_NewSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_NewSchedule.Image = ((System.Drawing.Image)(resources.GetObject("tsb_NewSchedule.Image")));
            this.tsb_NewSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NewSchedule.Name = "tsb_NewSchedule";
            this.tsb_NewSchedule.Size = new System.Drawing.Size(37, 50);
            this.tsb_NewSchedule.Tag = "Add";
            this.tsb_NewSchedule.Text = "&New";
            this.tsb_NewSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_NewSchedule.Click += new System.EventHandler(this.tsb_NewSchedule_Click);
            // 
            // tsb_EditSchedule
            // 
            this.tsb_EditSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_EditSchedule.Image = ((System.Drawing.Image)(resources.GetObject("tsb_EditSchedule.Image")));
            this.tsb_EditSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_EditSchedule.Name = "tsb_EditSchedule";
            this.tsb_EditSchedule.Size = new System.Drawing.Size(36, 50);
            this.tsb_EditSchedule.Tag = "Modify";
            this.tsb_EditSchedule.Text = "&Edit";
            this.tsb_EditSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_EditSchedule.Click += new System.EventHandler(this.tsb_EditSchedule_Click);
            // 
            // tsb_DeleteSchedule
            // 
            this.tsb_DeleteSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DeleteSchedule.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DeleteSchedule.Image")));
            this.tsb_DeleteSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DeleteSchedule.Name = "tsb_DeleteSchedule";
            this.tsb_DeleteSchedule.Size = new System.Drawing.Size(50, 50);
            this.tsb_DeleteSchedule.Tag = "Delete";
            this.tsb_DeleteSchedule.Text = "&Delete";
            this.tsb_DeleteSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DeleteSchedule.Click += new System.EventHandler(this.tsb_DeleteSchedule_Click);
            // 
            // tsb_Help
            // 
            this.tsb_Help.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Help.Image")));
            this.tsb_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Help.Name = "tsb_Help";
            this.tsb_Help.Size = new System.Drawing.Size(38, 50);
            this.tsb_Help.Tag = "Help";
            this.tsb_Help.Text = "&Help";
            this.tsb_Help.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Help.Visible = false;
            this.tsb_Help.Click += new System.EventHandler(this.tsb_Help_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // tsb_Today
            // 
            this.tsb_Today.CheckOnClick = true;
            this.tsb_Today.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Today.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Today.Image")));
            this.tsb_Today.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Today.Name = "tsb_Today";
            this.tsb_Today.Size = new System.Drawing.Size(48, 50);
            this.tsb_Today.Tag = "Today";
            this.tsb_Today.Text = "&Today";
            this.tsb_Today.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Today.Click += new System.EventHandler(this.tsb_Today_Click);
            // 
            // tsb_DayView
            // 
            this.tsb_DayView.CheckOnClick = true;
            this.tsb_DayView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DayView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DayView.Image")));
            this.tsb_DayView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DayView.Name = "tsb_DayView";
            this.tsb_DayView.Size = new System.Drawing.Size(36, 50);
            this.tsb_DayView.Tag = "DayView";
            this.tsb_DayView.Text = "D&ay";
            this.tsb_DayView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DayView.Click += new System.EventHandler(this.tsb_DayView_Click);
            // 
            // tsb_WeekView
            // 
            this.tsb_WeekView.CheckOnClick = true;
            this.tsb_WeekView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_WeekView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_WeekView.Image")));
            this.tsb_WeekView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_WeekView.Name = "tsb_WeekView";
            this.tsb_WeekView.Size = new System.Drawing.Size(44, 50);
            this.tsb_WeekView.Tag = "WeekView";
            this.tsb_WeekView.Text = "&Week";
            this.tsb_WeekView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_WeekView.Click += new System.EventHandler(this.tsb_WeekView_Click);
            // 
            // tsb_MonthView
            // 
            this.tsb_MonthView.CheckOnClick = true;
            this.tsb_MonthView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MonthView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MonthView.Image")));
            this.tsb_MonthView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MonthView.Name = "tsb_MonthView";
            this.tsb_MonthView.Size = new System.Drawing.Size(52, 50);
            this.tsb_MonthView.Tag = "MonthView";
            this.tsb_MonthView.Text = "&Month";
            this.tsb_MonthView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MonthView.Click += new System.EventHandler(this.tsb_MonthView_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 50);
            // 
            // tls_btnTimeNavigation
            // 
            this.tls_btnTimeNavigation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnTimeNavigation_5Min,
            this.tls_btnTimeNavigation_10Min,
            this.tls_btnTimeNavigation_15Min,
            this.tls_btnTimeNavigation_30Min});
            this.tls_btnTimeNavigation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnTimeNavigation.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnTimeNavigation.Image")));
            this.tls_btnTimeNavigation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnTimeNavigation.Name = "tls_btnTimeNavigation";
            this.tls_btnTimeNavigation.Size = new System.Drawing.Size(90, 50);
            this.tls_btnTimeNavigation.Tag = " Zoom Time";
            this.tls_btnTimeNavigation.Text = "  &ZoomTime";
            this.tls_btnTimeNavigation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnTimeNavigation.ToolTipText = " ZoomTime";
            // 
            // tls_btnTimeNavigation_5Min
            // 
            this.tls_btnTimeNavigation_5Min.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnTimeNavigation_5Min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnTimeNavigation_5Min.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnTimeNavigation_5Min.Image")));
            this.tls_btnTimeNavigation_5Min.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tls_btnTimeNavigation_5Min.Name = "tls_btnTimeNavigation_5Min";
            this.tls_btnTimeNavigation_5Min.Size = new System.Drawing.Size(141, 22);
            this.tls_btnTimeNavigation_5Min.Tag = "TN5Min";
            this.tls_btnTimeNavigation_5Min.Text = "5 Minute";
            this.tls_btnTimeNavigation_5Min.Click += new System.EventHandler(this.tls_btnTimeNavigation_5Min_Click);
            // 
            // tls_btnTimeNavigation_10Min
            // 
            this.tls_btnTimeNavigation_10Min.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnTimeNavigation_10Min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnTimeNavigation_10Min.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnTimeNavigation_10Min.Image")));
            this.tls_btnTimeNavigation_10Min.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tls_btnTimeNavigation_10Min.Name = "tls_btnTimeNavigation_10Min";
            this.tls_btnTimeNavigation_10Min.Size = new System.Drawing.Size(141, 22);
            this.tls_btnTimeNavigation_10Min.Tag = "TN10Min";
            this.tls_btnTimeNavigation_10Min.Text = "10 Minute";
            this.tls_btnTimeNavigation_10Min.Click += new System.EventHandler(this.tls_btnTimeNavigation_10Min_Click);
            // 
            // tls_btnTimeNavigation_15Min
            // 
            this.tls_btnTimeNavigation_15Min.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnTimeNavigation_15Min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnTimeNavigation_15Min.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnTimeNavigation_15Min.Image")));
            this.tls_btnTimeNavigation_15Min.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tls_btnTimeNavigation_15Min.Name = "tls_btnTimeNavigation_15Min";
            this.tls_btnTimeNavigation_15Min.Size = new System.Drawing.Size(141, 22);
            this.tls_btnTimeNavigation_15Min.Tag = "TN15Min";
            this.tls_btnTimeNavigation_15Min.Text = "15 Minute";
            this.tls_btnTimeNavigation_15Min.Click += new System.EventHandler(this.tls_btnTimeNavigation_15Min_Click);
            // 
            // tls_btnTimeNavigation_30Min
            // 
            this.tls_btnTimeNavigation_30Min.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnTimeNavigation_30Min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnTimeNavigation_30Min.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnTimeNavigation_30Min.Image")));
            this.tls_btnTimeNavigation_30Min.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tls_btnTimeNavigation_30Min.Name = "tls_btnTimeNavigation_30Min";
            this.tls_btnTimeNavigation_30Min.Size = new System.Drawing.Size(141, 22);
            this.tls_btnTimeNavigation_30Min.Tag = "TN30Min";
            this.tls_btnTimeNavigation_30Min.Text = "30 Minute";
            this.tls_btnTimeNavigation_30Min.Click += new System.EventHandler(this.tls_btnTimeNavigation_30Min_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 53);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // juc_Calendar
            // 
            this.juc_Calendar.AllowDrop = true;
            this.juc_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.juc_Calendar.FirstMonth = new System.DateTime(2007, 12, 1, 0, 0, 0, 0);
            this.juc_Calendar.Location = new System.Drawing.Point(22, 33);
            this.juc_Calendar.Name = "juc_Calendar";
            this.juc_Calendar.Schedule = this.juc_ViewSchedule;
            this.juc_Calendar.SelectionStyle = Janus.Windows.Schedule.CalendarSelectionStyle.Schedule;
            this.juc_Calendar.Size = new System.Drawing.Size(166, 284);
            this.juc_Calendar.TabIndex = 133;
            this.juc_Calendar.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            this.juc_Calendar.SelectionChanged += new System.EventHandler(this.juc_Calendar_SelectionChanged);
            this.juc_Calendar.DatesChanged += new System.EventHandler(this.juc_ViewSchedule_DatesChanged);
            // 
            // juc_ViewSchedule
            // 
            this.juc_ViewSchedule.AddNewMode = Janus.Windows.Schedule.AddNewMode.Manual;
            this.juc_ViewSchedule.AllowDelete = false;
            this.juc_ViewSchedule.AllowEdit = false;
            this.juc_ViewSchedule.BorderStyle = Janus.Windows.Schedule.BorderStyle.None;
            this.juc_ViewSchedule.Date = new System.DateTime(2007, 12, 24, 0, 0, 0, 0);
            this.juc_ViewSchedule.DayColumns = 1;
            this.juc_ViewSchedule.DayNavigationButtons = true;
            this.juc_ViewSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.juc_ViewSchedule.FirstVisibleTime = System.TimeSpan.Parse("09:00:00");
            this.juc_ViewSchedule.HorizontalScrollPosition = 0;
            this.juc_ViewSchedule.ImageList = this.imageList1;
            this.juc_ViewSchedule.Interval = Janus.Windows.Schedule.Interval.FifteenMinutes;
            this.juc_ViewSchedule.Location = new System.Drawing.Point(3, 3);
            this.juc_ViewSchedule.MonthDaysFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.juc_ViewSchedule.MonthDaysFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.juc_ViewSchedule.MonthDaysFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.juc_ViewSchedule.MultiOwner = true;
            this.juc_ViewSchedule.Name = "juc_ViewSchedule";
            this.juc_ViewSchedule.NewAppointmentText = "Click to add schedule";
            this.juc_ViewSchedule.NextAppointmentText = "Next Schedule";
            this.juc_ViewSchedule.PreviousAppointmentText = "Previous Schedule";
            this.juc_ViewSchedule.RecurrenceExceptionImageIndex = -1;
            this.juc_ViewSchedule.RecurrenceImageIndex = -1;
            this.juc_ViewSchedule.ReminderImageIndex = -1;
            this.juc_ViewSchedule.ShowMinutesInTimeNavigator = true;
            this.juc_ViewSchedule.Size = new System.Drawing.Size(846, 803);
            this.juc_ViewSchedule.TabIndex = 0;
            this.juc_ViewSchedule.VerticalScrollPosition = 36;
            this.juc_ViewSchedule.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            this.juc_ViewSchedule.WeekDaysBackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.juc_ViewSchedule.WeekDaysBackgroundStyle.BackColorGradient = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.juc_ViewSchedule.WeekDaysBackgroundStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.juc_ViewSchedule.WorkEndTime = System.TimeSpan.Parse("18:00:00");
            this.juc_ViewSchedule.WorkStartTime = System.TimeSpan.Parse("09:00:00");
            this.juc_ViewSchedule.AppointmentChanged += new Janus.Windows.Schedule.AppointmentChangeEventHandler(this.juc_ViewSchedule_AppointmentChanged);
            this.juc_ViewSchedule.AppointmentChanging += new Janus.Windows.Schedule.AppointmentChangeCancelEventHandler(this.juc_ViewSchedule_AppointmentChanging);
            this.juc_ViewSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.juc_ViewSchedule_MouseDown);
            this.juc_ViewSchedule.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.juc_ViewSchedule_MouseDoubleClick);
            this.juc_ViewSchedule.DroppingAppointment += new Janus.Windows.Schedule.DroppingAppointmentEventHandler(this.juc_ViewSchedule_DroppingAppointment);
            this.juc_ViewSchedule.DatesChanged += new System.EventHandler(this.juc_ViewSchedule_DatesChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Open Signature.ico");
            this.imageList1.Images.SetKeyName(1, "Recurrence.ico");
            this.imageList1.Images.SetKeyName(2, "SingleInRecurrence.ico");
            // 
            // pnlCalendarHeader
            // 
            this.pnlCalendarHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlCalendarHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCalendarHeader.Controls.Add(this.btnLeft);
            this.pnlCalendarHeader.Controls.Add(this.lblCalender);
            this.pnlCalendarHeader.Controls.Add(this.lbl_pnlCalendarHeaderBottomBrd);
            this.pnlCalendarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCalendarHeader.Location = new System.Drawing.Point(4, 4);
            this.pnlCalendarHeader.Name = "pnlCalendarHeader";
            this.pnlCalendarHeader.Size = new System.Drawing.Size(204, 23);
            this.pnlCalendarHeader.TabIndex = 90;
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLeft.FlatAppearance.BorderSize = 0;
            this.btnLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.Location = new System.Drawing.Point(177, 0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(27, 22);
            this.btnLeft.TabIndex = 2;
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.MouseLeave += new System.EventHandler(this.btnLeft_MouseLeave);
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            this.btnLeft.MouseHover += new System.EventHandler(this.btnLeft_MouseHover);
            // 
            // lblCalender
            // 
            this.lblCalender.BackColor = System.Drawing.Color.Transparent;
            this.lblCalender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCalender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalender.ForeColor = System.Drawing.Color.White;
            this.lblCalender.Location = new System.Drawing.Point(0, 0);
            this.lblCalender.Name = "lblCalender";
            this.lblCalender.Size = new System.Drawing.Size(204, 22);
            this.lblCalender.TabIndex = 0;
            this.lblCalender.Text = " Calendar";
            this.lblCalender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_pnlCalendarHeaderBottomBrd
            // 
            this.lbl_pnlCalendarHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCalendarHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCalendarHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlCalendarHeaderBottomBrd.Name = "lbl_pnlCalendarHeaderBottomBrd";
            this.lbl_pnlCalendarHeaderBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlCalendarHeaderBottomBrd.TabIndex = 143;
            // 
            // pnlSearchList
            // 
            this.pnlSearchList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchList.Controls.Add(this.pnlSelection);
            this.pnlSearchList.Controls.Add(this.Spt_pnlResourcesTop);
            this.pnlSearchList.Controls.Add(this.pnlResources);
            this.pnlSearchList.Controls.Add(this.Spt_pnlSelectionTop);
            this.pnlSearchList.Controls.Add(this.pnlProvider);
            this.pnlSearchList.Controls.Add(this.Spt_pnlProviderTop);
            this.pnlSearchList.Controls.Add(this.pnlCalendar);
            this.pnlSearchList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearchList.Location = new System.Drawing.Point(0, 53);
            this.pnlSearchList.Name = "pnlSearchList";
            this.pnlSearchList.Size = new System.Drawing.Size(210, 914);
            this.pnlSearchList.TabIndex = 15;
            // 
            // pnlSelection
            // 
            this.pnlSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSelection.Controls.Add(this.lbl_pnlSelectionBottomBrd);
            this.pnlSelection.Controls.Add(this.pnlSelectionBody);
            this.pnlSelection.Controls.Add(this.pnlSelectionHeader);
            this.pnlSelection.Controls.Add(this.lbl_pnlSelectionLeftBrd);
            this.pnlSelection.Controls.Add(this.lbl_pnlSelectionRightBrd);
            this.pnlSelection.Controls.Add(this.lbl_pnlSelectionTopBrd);
            this.pnlSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelection.Location = new System.Drawing.Point(0, 593);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Padding = new System.Windows.Forms.Padding(3, 2, 1, 3);
            this.pnlSelection.Size = new System.Drawing.Size(210, 321);
            this.pnlSelection.TabIndex = 143;
            // 
            // lbl_pnlSelectionBottomBrd
            // 
            this.lbl_pnlSelectionBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSelectionBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSelectionBottomBrd.Location = new System.Drawing.Point(4, 317);
            this.lbl_pnlSelectionBottomBrd.Name = "lbl_pnlSelectionBottomBrd";
            this.lbl_pnlSelectionBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlSelectionBottomBrd.TabIndex = 97;
            // 
            // pnlSelectionBody
            // 
            this.pnlSelectionBody.Controls.Add(this.cmbDepartment);
            this.pnlSelectionBody.Controls.Add(this.lblLocation);
            this.pnlSelectionBody.Controls.Add(this.cmbLocation);
            this.pnlSelectionBody.Controls.Add(this.lblDepartment);
            this.pnlSelectionBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelectionBody.Location = new System.Drawing.Point(4, 26);
            this.pnlSelectionBody.Name = "pnlSelectionBody";
            this.pnlSelectionBody.Size = new System.Drawing.Size(204, 292);
            this.pnlSelectionBody.TabIndex = 92;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(9, 88);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(189, 22);
            this.cmbDepartment.TabIndex = 135;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbDepartment_SelectedIndexChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblLocation.Location = new System.Drawing.Point(7, 12);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 14);
            this.lblLocation.TabIndex = 132;
            this.lblLocation.Text = "Location :";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.ForeColor = System.Drawing.Color.Black;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(8, 33);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(190, 22);
            this.cmbLocation.TabIndex = 133;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Location = new System.Drawing.Point(7, 66);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(81, 14);
            this.lblDepartment.TabIndex = 134;
            this.lblDepartment.Text = "Department :";
            // 
            // pnlSelectionHeader
            // 
            this.pnlSelectionHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlSelectionHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSelectionHeader.Controls.Add(this.lbl_pnlSelectionHeaderBottomBrd);
            this.pnlSelectionHeader.Controls.Add(this.lblSelection);
            this.pnlSelectionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectionHeader.Location = new System.Drawing.Point(4, 3);
            this.pnlSelectionHeader.Name = "pnlSelectionHeader";
            this.pnlSelectionHeader.Size = new System.Drawing.Size(204, 23);
            this.pnlSelectionHeader.TabIndex = 91;
            // 
            // lbl_pnlSelectionHeaderBottomBrd
            // 
            this.lbl_pnlSelectionHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSelectionHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSelectionHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlSelectionHeaderBottomBrd.Name = "lbl_pnlSelectionHeaderBottomBrd";
            this.lbl_pnlSelectionHeaderBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlSelectionHeaderBottomBrd.TabIndex = 97;
            // 
            // lblSelection
            // 
            this.lblSelection.BackColor = System.Drawing.Color.Transparent;
            this.lblSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSelection.Location = new System.Drawing.Point(0, 0);
            this.lblSelection.Name = "lblSelection";
            this.lblSelection.Size = new System.Drawing.Size(204, 23);
            this.lblSelection.TabIndex = 0;
            this.lblSelection.Text = " Selection";
            this.lblSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlSelectionLeftBrd
            // 
            this.lbl_pnlSelectionLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSelectionLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSelectionLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlSelectionLeftBrd.Name = "lbl_pnlSelectionLeftBrd";
            this.lbl_pnlSelectionLeftBrd.Size = new System.Drawing.Size(1, 315);
            this.lbl_pnlSelectionLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlSelectionRightBrd
            // 
            this.lbl_pnlSelectionRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSelectionRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSelectionRightBrd.Location = new System.Drawing.Point(208, 3);
            this.lbl_pnlSelectionRightBrd.Name = "lbl_pnlSelectionRightBrd";
            this.lbl_pnlSelectionRightBrd.Size = new System.Drawing.Size(1, 315);
            this.lbl_pnlSelectionRightBrd.TabIndex = 94;
            // 
            // lbl_pnlSelectionTopBrd
            // 
            this.lbl_pnlSelectionTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSelectionTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSelectionTopBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_pnlSelectionTopBrd.Name = "lbl_pnlSelectionTopBrd";
            this.lbl_pnlSelectionTopBrd.Size = new System.Drawing.Size(206, 1);
            this.lbl_pnlSelectionTopBrd.TabIndex = 96;
            // 
            // Spt_pnlResourcesTop
            // 
            this.Spt_pnlResourcesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spt_pnlResourcesTop.Location = new System.Drawing.Point(0, 590);
            this.Spt_pnlResourcesTop.Name = "Spt_pnlResourcesTop";
            this.Spt_pnlResourcesTop.Size = new System.Drawing.Size(210, 3);
            this.Spt_pnlResourcesTop.TabIndex = 145;
            this.Spt_pnlResourcesTop.TabStop = false;
            // 
            // pnlResources
            // 
            this.pnlResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlResources.Controls.Add(this.lbl_pnlResourcesBottomBrd);
            this.pnlResources.Controls.Add(this.pnlResources_trv);
            this.pnlResources.Controls.Add(this.pnlResourcesHeader);
            this.pnlResources.Controls.Add(this.lbl_pnlResourcesLeftBrd);
            this.pnlResources.Controls.Add(this.lbl_pnlResourcesRightBrd);
            this.pnlResources.Controls.Add(this.lbl_pnlResourcesTopBrd);
            this.pnlResources.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResources.Location = new System.Drawing.Point(0, 467);
            this.pnlResources.Name = "pnlResources";
            this.pnlResources.Padding = new System.Windows.Forms.Padding(3, 2, 1, 1);
            this.pnlResources.Size = new System.Drawing.Size(210, 123);
            this.pnlResources.TabIndex = 144;
            // 
            // lbl_pnlResourcesBottomBrd
            // 
            this.lbl_pnlResourcesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourcesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlResourcesBottomBrd.Location = new System.Drawing.Point(4, 121);
            this.lbl_pnlResourcesBottomBrd.Name = "lbl_pnlResourcesBottomBrd";
            this.lbl_pnlResourcesBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlResourcesBottomBrd.TabIndex = 97;
            // 
            // pnlResources_trv
            // 
            this.pnlResources_trv.Controls.Add(this.trvResources);
            this.pnlResources_trv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResources_trv.Location = new System.Drawing.Point(4, 26);
            this.pnlResources_trv.Name = "pnlResources_trv";
            this.pnlResources_trv.Size = new System.Drawing.Size(204, 96);
            this.pnlResources_trv.TabIndex = 92;
            // 
            // trvResources
            // 
            this.trvResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvResources.CheckBoxes = true;
            this.trvResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvResources.ForeColor = System.Drawing.Color.Black;
            this.trvResources.Location = new System.Drawing.Point(0, 0);
            this.trvResources.Name = "trvResources";
            this.trvResources.ShowLines = false;
            this.trvResources.ShowPlusMinus = false;
            this.trvResources.ShowRootLines = false;
            this.trvResources.Size = new System.Drawing.Size(204, 96);
            this.trvResources.TabIndex = 2;
            this.trvResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvResources_AfterCheck);
            // 
            // pnlResourcesHeader
            // 
            this.pnlResourcesHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlResourcesHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlResourcesHeader.Controls.Add(this.btnDeSelectResource);
            this.pnlResourcesHeader.Controls.Add(this.btnSelectResource);
            this.pnlResourcesHeader.Controls.Add(this.lbl_pnlResourcesHeaderBottomBrd);
            this.pnlResourcesHeader.Controls.Add(this.lblResources);
            this.pnlResourcesHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResourcesHeader.Location = new System.Drawing.Point(4, 3);
            this.pnlResourcesHeader.Name = "pnlResourcesHeader";
            this.pnlResourcesHeader.Size = new System.Drawing.Size(204, 23);
            this.pnlResourcesHeader.TabIndex = 91;
            // 
            // btnDeSelectResource
            // 
            this.btnDeSelectResource.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectResource.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectResource.FlatAppearance.BorderSize = 0;
            this.btnDeSelectResource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectResource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectResource.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectResource.Image")));
            this.btnDeSelectResource.Location = new System.Drawing.Point(142, 0);
            this.btnDeSelectResource.Name = "btnDeSelectResource";
            this.btnDeSelectResource.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectResource.TabIndex = 102;
            this.btnDeSelectResource.Tag = "Select";
            this.btnDeSelectResource.UseVisualStyleBackColor = false;
            this.btnDeSelectResource.Visible = false;
            this.btnDeSelectResource.Click += new System.EventHandler(this.btnDeSelectResource_Click);
            this.btnDeSelectResource.MouseHover += new System.EventHandler(this.btnDeSelectResource_MouseHover);
            // 
            // btnSelectResource
            // 
            this.btnSelectResource.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectResource.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectResource.FlatAppearance.BorderSize = 0;
            this.btnSelectResource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectResource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectResource.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectResource.Image")));
            this.btnSelectResource.Location = new System.Drawing.Point(173, 0);
            this.btnSelectResource.Name = "btnSelectResource";
            this.btnSelectResource.Size = new System.Drawing.Size(31, 22);
            this.btnSelectResource.TabIndex = 101;
            this.btnSelectResource.Tag = "Select";
            this.btnSelectResource.UseVisualStyleBackColor = false;
            this.btnSelectResource.Click += new System.EventHandler(this.btnSelectResource_Click);
            this.btnSelectResource.MouseHover += new System.EventHandler(this.btnSelectResource_MouseHover);
            // 
            // lbl_pnlResourcesHeaderBottomBrd
            // 
            this.lbl_pnlResourcesHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourcesHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlResourcesHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlResourcesHeaderBottomBrd.Name = "lbl_pnlResourcesHeaderBottomBrd";
            this.lbl_pnlResourcesHeaderBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlResourcesHeaderBottomBrd.TabIndex = 97;
            // 
            // lblResources
            // 
            this.lblResources.BackColor = System.Drawing.Color.Transparent;
            this.lblResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblResources.Location = new System.Drawing.Point(0, 0);
            this.lblResources.Name = "lblResources";
            this.lblResources.Size = new System.Drawing.Size(204, 23);
            this.lblResources.TabIndex = 0;
            this.lblResources.Text = " Resources";
            this.lblResources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlResourcesLeftBrd
            // 
            this.lbl_pnlResourcesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourcesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlResourcesLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlResourcesLeftBrd.Name = "lbl_pnlResourcesLeftBrd";
            this.lbl_pnlResourcesLeftBrd.Size = new System.Drawing.Size(1, 119);
            this.lbl_pnlResourcesLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlResourcesRightBrd
            // 
            this.lbl_pnlResourcesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourcesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlResourcesRightBrd.Location = new System.Drawing.Point(208, 3);
            this.lbl_pnlResourcesRightBrd.Name = "lbl_pnlResourcesRightBrd";
            this.lbl_pnlResourcesRightBrd.Size = new System.Drawing.Size(1, 119);
            this.lbl_pnlResourcesRightBrd.TabIndex = 94;
            // 
            // lbl_pnlResourcesTopBrd
            // 
            this.lbl_pnlResourcesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourcesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlResourcesTopBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_pnlResourcesTopBrd.Name = "lbl_pnlResourcesTopBrd";
            this.lbl_pnlResourcesTopBrd.Size = new System.Drawing.Size(206, 1);
            this.lbl_pnlResourcesTopBrd.TabIndex = 96;
            // 
            // Spt_pnlSelectionTop
            // 
            this.Spt_pnlSelectionTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spt_pnlSelectionTop.Location = new System.Drawing.Point(0, 464);
            this.Spt_pnlSelectionTop.Name = "Spt_pnlSelectionTop";
            this.Spt_pnlSelectionTop.Size = new System.Drawing.Size(210, 3);
            this.Spt_pnlSelectionTop.TabIndex = 142;
            this.Spt_pnlSelectionTop.TabStop = false;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.pnlProvider.Controls.Add(this.pnlProviderBody);
            this.pnlProvider.Controls.Add(this.pnlProviderHeader);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 328);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Padding = new System.Windows.Forms.Padding(3, 2, 1, 1);
            this.pnlProvider.Size = new System.Drawing.Size(210, 136);
            this.pnlProvider.TabIndex = 141;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(4, 134);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvProvider);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(4, 26);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(204, 109);
            this.pnlProviderBody.TabIndex = 92;
            // 
            // trvProvider
            // 
            this.trvProvider.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvProvider.CheckBoxes = true;
            this.trvProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProvider.ForeColor = System.Drawing.Color.Black;
            this.trvProvider.Location = new System.Drawing.Point(0, 0);
            this.trvProvider.Name = "trvProvider";
            this.trvProvider.ShowLines = false;
            this.trvProvider.ShowPlusMinus = false;
            this.trvProvider.ShowRootLines = false;
            this.trvProvider.Size = new System.Drawing.Size(204, 109);
            this.trvProvider.TabIndex = 2;
            this.trvProvider.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvProvider_AfterCheck);
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectProvider);
            this.pnlProviderHeader.Controls.Add(this.btnSelectProvider);
            this.pnlProviderHeader.Controls.Add(this.lbl_pnlProviderHeaderBottomBrd);
            this.pnlProviderHeader.Controls.Add(this.lblProviderResources);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(4, 3);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(204, 23);
            this.pnlProviderHeader.TabIndex = 91;
            // 
            // btnDeSelectProvider
            // 
            this.btnDeSelectProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectProvider.FlatAppearance.BorderSize = 0;
            this.btnDeSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectProvider.Image")));
            this.btnDeSelectProvider.Location = new System.Drawing.Point(142, 0);
            this.btnDeSelectProvider.Name = "btnDeSelectProvider";
            this.btnDeSelectProvider.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectProvider.TabIndex = 101;
            this.btnDeSelectProvider.Tag = "Select";
            this.btnDeSelectProvider.UseVisualStyleBackColor = false;
            this.btnDeSelectProvider.Visible = false;
            this.btnDeSelectProvider.Click += new System.EventHandler(this.btnDeSelectProvider_Click);
            this.btnDeSelectProvider.MouseHover += new System.EventHandler(this.btnDeSelectProvider_MouseHover);
            // 
            // btnSelectProvider
            // 
            this.btnSelectProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectProvider.FlatAppearance.BorderSize = 0;
            this.btnSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectProvider.Image")));
            this.btnSelectProvider.Location = new System.Drawing.Point(173, 0);
            this.btnSelectProvider.Name = "btnSelectProvider";
            this.btnSelectProvider.Size = new System.Drawing.Size(31, 22);
            this.btnSelectProvider.TabIndex = 100;
            this.btnSelectProvider.Tag = "Select";
            this.btnSelectProvider.UseVisualStyleBackColor = false;
            this.btnSelectProvider.Click += new System.EventHandler(this.btnSelectProvider_Click);
            this.btnSelectProvider.MouseHover += new System.EventHandler(this.btnSelectProvider_MouseHover);
            // 
            // lbl_pnlProviderHeaderBottomBrd
            // 
            this.lbl_pnlProviderHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlProviderHeaderBottomBrd.Name = "lbl_pnlProviderHeaderBottomBrd";
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 97;
            // 
            // lblProviderResources
            // 
            this.lblProviderResources.BackColor = System.Drawing.Color.Transparent;
            this.lblProviderResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProviderResources.Location = new System.Drawing.Point(0, 0);
            this.lblProviderResources.Name = "lblProviderResources";
            this.lblProviderResources.Size = new System.Drawing.Size(204, 23);
            this.lblProviderResources.TabIndex = 0;
            this.lblProviderResources.Text = " Providers";
            this.lblProviderResources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 132);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(208, 3);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 132);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(206, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // Spt_pnlProviderTop
            // 
            this.Spt_pnlProviderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spt_pnlProviderTop.Location = new System.Drawing.Point(0, 325);
            this.Spt_pnlProviderTop.Name = "Spt_pnlProviderTop";
            this.Spt_pnlProviderTop.Size = new System.Drawing.Size(210, 3);
            this.Spt_pnlProviderTop.TabIndex = 140;
            this.Spt_pnlProviderTop.TabStop = false;
            // 
            // pnlCalendar
            // 
            this.pnlCalendar.Controls.Add(this.juc_Calendar);
            this.pnlCalendar.Controls.Add(this.label4);
            this.pnlCalendar.Controls.Add(this.label3);
            this.pnlCalendar.Controls.Add(this.label2);
            this.pnlCalendar.Controls.Add(this.label1);
            this.pnlCalendar.Controls.Add(this.lbl_pnlCalendarBottomBrd);
            this.pnlCalendar.Controls.Add(this.pnlCalendarHeader);
            this.pnlCalendar.Controls.Add(this.lbl_pnlCalendarTopBrd);
            this.pnlCalendar.Controls.Add(this.lbl_pnlCalendarLeftBrd);
            this.pnlCalendar.Controls.Add(this.lbl_pnlCalendarRightBrd);
            this.pnlCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCalendar.Location = new System.Drawing.Point(0, 0);
            this.pnlCalendar.Name = "pnlCalendar";
            this.pnlCalendar.Padding = new System.Windows.Forms.Padding(3, 3, 1, 1);
            this.pnlCalendar.Size = new System.Drawing.Size(210, 325);
            this.pnlCalendar.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(22, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 6);
            this.label4.TabIndex = 149;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(22, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 6);
            this.label3.TabIndex = 148;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(190, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 296);
            this.label2.TabIndex = 147;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 296);
            this.label1.TabIndex = 146;
            // 
            // lbl_pnlCalendarBottomBrd
            // 
            this.lbl_pnlCalendarBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCalendarBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCalendarBottomBrd.Location = new System.Drawing.Point(4, 323);
            this.lbl_pnlCalendarBottomBrd.Name = "lbl_pnlCalendarBottomBrd";
            this.lbl_pnlCalendarBottomBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlCalendarBottomBrd.TabIndex = 143;
            // 
            // lbl_pnlCalendarTopBrd
            // 
            this.lbl_pnlCalendarTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCalendarTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCalendarTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlCalendarTopBrd.Name = "lbl_pnlCalendarTopBrd";
            this.lbl_pnlCalendarTopBrd.Size = new System.Drawing.Size(204, 1);
            this.lbl_pnlCalendarTopBrd.TabIndex = 142;
            // 
            // lbl_pnlCalendarLeftBrd
            // 
            this.lbl_pnlCalendarLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCalendarLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCalendarLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlCalendarLeftBrd.Name = "lbl_pnlCalendarLeftBrd";
            this.lbl_pnlCalendarLeftBrd.Size = new System.Drawing.Size(1, 321);
            this.lbl_pnlCalendarLeftBrd.TabIndex = 144;
            // 
            // lbl_pnlCalendarRightBrd
            // 
            this.lbl_pnlCalendarRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCalendarRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCalendarRightBrd.Location = new System.Drawing.Point(208, 3);
            this.lbl_pnlCalendarRightBrd.Name = "lbl_pnlCalendarRightBrd";
            this.lbl_pnlCalendarRightBrd.Size = new System.Drawing.Size(1, 321);
            this.lbl_pnlCalendarRightBrd.TabIndex = 145;
            // 
            // splSearchList
            // 
            this.splSearchList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splSearchList.Location = new System.Drawing.Point(210, 53);
            this.splSearchList.Name = "splSearchList";
            this.splSearchList.Size = new System.Drawing.Size(3, 914);
            this.splSearchList.TabIndex = 16;
            this.splSearchList.TabStop = false;
            // 
            // cntMenuMain
            // 
            this.cntMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSchedule,
            this.toolStripSeparator2,
            this.ModifySchedule});
            this.cntMenuMain.Name = "cntMenuMain";
            this.cntMenuMain.Size = new System.Drawing.Size(164, 54);
            // 
            // NewSchedule
            // 
            this.NewSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.NewSchedule.Name = "NewSchedule";
            this.NewSchedule.Size = new System.Drawing.Size(163, 22);
            this.NewSchedule.Text = "New Schedule";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // ModifySchedule
            // 
            this.ModifySchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ModifySchedule.Name = "ModifySchedule";
            this.ModifySchedule.Size = new System.Drawing.Size(163, 22);
            this.ModifySchedule.Text = "Modify Schedule";
            // 
            // pnlSearchCriteria
            // 
            this.pnlSearchCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchCriteria.Controls.Add(this.lbl_pnlSearchCriteriaBottomBrd);
            this.pnlSearchCriteria.Controls.Add(this.cmbResource);
            this.pnlSearchCriteria.Controls.Add(this.lblResource);
            this.pnlSearchCriteria.Controls.Add(this.pnlSearchCriteriaHeader);
            this.pnlSearchCriteria.Controls.Add(this.lbl_pnlSearchCriterialeftBrd);
            this.pnlSearchCriteria.Controls.Add(this.lbl_pnlSearchCriteriaRightBrd);
            this.pnlSearchCriteria.Controls.Add(this.lbl_pnlSearchCriteriaTopBrd);
            this.pnlSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchCriteria.Location = new System.Drawing.Point(3, 3);
            this.pnlSearchCriteria.Name = "pnlSearchCriteria";
            this.pnlSearchCriteria.Size = new System.Drawing.Size(846, 98);
            this.pnlSearchCriteria.TabIndex = 16;
            // 
            // lbl_pnlSearchCriteriaBottomBrd
            // 
            this.lbl_pnlSearchCriteriaBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchCriteriaBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchCriteriaBottomBrd.Location = new System.Drawing.Point(1, 97);
            this.lbl_pnlSearchCriteriaBottomBrd.Name = "lbl_pnlSearchCriteriaBottomBrd";
            this.lbl_pnlSearchCriteriaBottomBrd.Size = new System.Drawing.Size(844, 1);
            this.lbl_pnlSearchCriteriaBottomBrd.TabIndex = 142;
            // 
            // cmbResource
            // 
            this.cmbResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResource.FormattingEnabled = true;
            this.cmbResource.Location = new System.Drawing.Point(98, 116);
            this.cmbResource.Name = "cmbResource";
            this.cmbResource.Size = new System.Drawing.Size(294, 22);
            this.cmbResource.TabIndex = 137;
            this.cmbResource.Visible = false;
            // 
            // lblResource
            // 
            this.lblResource.AutoSize = true;
            this.lblResource.BackColor = System.Drawing.Color.Transparent;
            this.lblResource.Location = new System.Drawing.Point(21, 121);
            this.lblResource.Name = "lblResource";
            this.lblResource.Size = new System.Drawing.Size(65, 14);
            this.lblResource.TabIndex = 136;
            this.lblResource.Text = "Resource :";
            this.lblResource.Visible = false;
            // 
            // pnlSearchCriteriaHeader
            // 
            this.pnlSearchCriteriaHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlSearchCriteriaHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchCriteriaHeader.Controls.Add(this.lblSerchCriteria);
            this.pnlSearchCriteriaHeader.Controls.Add(this.lbl_pnlSearchCriteriaHeaderBottomBrd);
            this.pnlSearchCriteriaHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchCriteriaHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlSearchCriteriaHeader.Name = "pnlSearchCriteriaHeader";
            this.pnlSearchCriteriaHeader.Size = new System.Drawing.Size(844, 23);
            this.pnlSearchCriteriaHeader.TabIndex = 90;
            // 
            // lblSerchCriteria
            // 
            this.lblSerchCriteria.BackColor = System.Drawing.Color.Transparent;
            this.lblSerchCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSerchCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerchCriteria.ForeColor = System.Drawing.Color.White;
            this.lblSerchCriteria.Location = new System.Drawing.Point(0, 0);
            this.lblSerchCriteria.Name = "lblSerchCriteria";
            this.lblSerchCriteria.Size = new System.Drawing.Size(844, 22);
            this.lblSerchCriteria.TabIndex = 0;
            this.lblSerchCriteria.Text = " Schedules";
            this.lblSerchCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlSearchCriteriaHeaderBottomBrd
            // 
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.Name = "lbl_pnlSearchCriteriaHeaderBottomBrd";
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.Size = new System.Drawing.Size(844, 1);
            this.lbl_pnlSearchCriteriaHeaderBottomBrd.TabIndex = 141;
            // 
            // lbl_pnlSearchCriterialeftBrd
            // 
            this.lbl_pnlSearchCriterialeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchCriterialeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchCriterialeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlSearchCriterialeftBrd.Name = "lbl_pnlSearchCriterialeftBrd";
            this.lbl_pnlSearchCriterialeftBrd.Size = new System.Drawing.Size(1, 97);
            this.lbl_pnlSearchCriterialeftBrd.TabIndex = 138;
            // 
            // lbl_pnlSearchCriteriaRightBrd
            // 
            this.lbl_pnlSearchCriteriaRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchCriteriaRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchCriteriaRightBrd.Location = new System.Drawing.Point(845, 1);
            this.lbl_pnlSearchCriteriaRightBrd.Name = "lbl_pnlSearchCriteriaRightBrd";
            this.lbl_pnlSearchCriteriaRightBrd.Size = new System.Drawing.Size(1, 97);
            this.lbl_pnlSearchCriteriaRightBrd.TabIndex = 139;
            // 
            // lbl_pnlSearchCriteriaTopBrd
            // 
            this.lbl_pnlSearchCriteriaTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchCriteriaTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchCriteriaTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchCriteriaTopBrd.Name = "lbl_pnlSearchCriteriaTopBrd";
            this.lbl_pnlSearchCriteriaTopBrd.Size = new System.Drawing.Size(846, 1);
            this.lbl_pnlSearchCriteriaTopBrd.TabIndex = 140;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.pnlSearchCriteria);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(243, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.pnlSearch.Size = new System.Drawing.Size(852, 102);
            this.pnlSearch.TabIndex = 13;
            this.pnlSearch.Visible = false;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1095, 53);
            this.pnlToolStrip.TabIndex = 18;
            // 
            // pnlViewSchedule
            // 
            this.pnlViewSchedule.BackColor = System.Drawing.Color.Transparent;
            this.pnlViewSchedule.Controls.Add(this.lbl_pnlViewScheduleRightBrd);
            this.pnlViewSchedule.Controls.Add(this.lbl_pnlViewScheduleLeftBrd);
            this.pnlViewSchedule.Controls.Add(this.lbl_pnlViewScheduleTopBrd);
            this.pnlViewSchedule.Controls.Add(this.lbl_pnlViewScheduleBottomBrd);
            this.pnlViewSchedule.Controls.Add(this.juc_ViewSchedule);
            this.pnlViewSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewSchedule.Location = new System.Drawing.Point(243, 158);
            this.pnlViewSchedule.Name = "pnlViewSchedule";
            this.pnlViewSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.pnlViewSchedule.Size = new System.Drawing.Size(852, 809);
            this.pnlViewSchedule.TabIndex = 0;
            // 
            // lbl_pnlViewScheduleRightBrd
            // 
            this.lbl_pnlViewScheduleRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlViewScheduleRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlViewScheduleRightBrd.Location = new System.Drawing.Point(848, 4);
            this.lbl_pnlViewScheduleRightBrd.Name = "lbl_pnlViewScheduleRightBrd";
            this.lbl_pnlViewScheduleRightBrd.Size = new System.Drawing.Size(1, 801);
            this.lbl_pnlViewScheduleRightBrd.TabIndex = 146;
            // 
            // lbl_pnlViewScheduleLeftBrd
            // 
            this.lbl_pnlViewScheduleLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlViewScheduleLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlViewScheduleLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlViewScheduleLeftBrd.Name = "lbl_pnlViewScheduleLeftBrd";
            this.lbl_pnlViewScheduleLeftBrd.Size = new System.Drawing.Size(1, 801);
            this.lbl_pnlViewScheduleLeftBrd.TabIndex = 145;
            // 
            // lbl_pnlViewScheduleTopBrd
            // 
            this.lbl_pnlViewScheduleTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlViewScheduleTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlViewScheduleTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlViewScheduleTopBrd.Name = "lbl_pnlViewScheduleTopBrd";
            this.lbl_pnlViewScheduleTopBrd.Size = new System.Drawing.Size(846, 1);
            this.lbl_pnlViewScheduleTopBrd.TabIndex = 144;
            // 
            // lbl_pnlViewScheduleBottomBrd
            // 
            this.lbl_pnlViewScheduleBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlViewScheduleBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlViewScheduleBottomBrd.Location = new System.Drawing.Point(3, 805);
            this.lbl_pnlViewScheduleBottomBrd.Name = "lbl_pnlViewScheduleBottomBrd";
            this.lbl_pnlViewScheduleBottomBrd.Size = new System.Drawing.Size(846, 1);
            this.lbl_pnlViewScheduleBottomBrd.TabIndex = 143;
            // 
            // pnlSmallStrip
            // 
            this.pnlSmallStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSmallStrip.Controls.Add(this.label5);
            this.pnlSmallStrip.Controls.Add(this.label45);
            this.pnlSmallStrip.Controls.Add(this.ts_SmallStrip);
            this.pnlSmallStrip.Controls.Add(this.btn_Right1);
            this.pnlSmallStrip.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnlSmallStrip.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnlSmallStrip.Controls.Add(this.label53);
            this.pnlSmallStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSmallStrip.Location = new System.Drawing.Point(213, 53);
            this.pnlSmallStrip.Name = "pnlSmallStrip";
            this.pnlSmallStrip.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlSmallStrip.Size = new System.Drawing.Size(30, 914);
            this.pnlSmallStrip.TabIndex = 99;
            this.pnlSmallStrip.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 910);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 1);
            this.label5.TabIndex = 144;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(3, 26);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(24, 1);
            this.label45.TabIndex = 142;
            // 
            // ts_SmallStrip
            // 
            this.ts_SmallStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_SmallStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_SmallStrip.BackgroundImage")));
            this.ts_SmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_SmallStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_SmallStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_SmallStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_SmallStrip_btn_Calendar,
            this.ts_SmallStrip_btn_Provider});
            this.ts_SmallStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ts_SmallStrip.Location = new System.Drawing.Point(3, 26);
            this.ts_SmallStrip.Name = "ts_SmallStrip";
            this.ts_SmallStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ts_SmallStrip.Size = new System.Drawing.Size(24, 885);
            this.ts_SmallStrip.TabIndex = 21;
            this.ts_SmallStrip.Text = "toolStrip1";
            this.ts_SmallStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // ts_SmallStrip_btn_Calendar
            // 
            this.ts_SmallStrip_btn_Calendar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Calendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Calendar.Name = "ts_SmallStrip_btn_Calendar";
            this.ts_SmallStrip_btn_Calendar.Size = new System.Drawing.Size(22, 80);
            this.ts_SmallStrip_btn_Calendar.Text = "  Calendar  ";
            this.ts_SmallStrip_btn_Calendar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_SmallStrip_btn_Calendar.Click += new System.EventHandler(this.ts_SmallStrip_btn_Calendar_Click);
            // 
            // ts_SmallStrip_btn_Provider
            // 
            this.ts_SmallStrip_btn_Provider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Provider.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Provider.Name = "ts_SmallStrip_btn_Provider";
            this.ts_SmallStrip_btn_Provider.Size = new System.Drawing.Size(22, 78);
            this.ts_SmallStrip_btn_Provider.Text = "  Provider  ";
            this.ts_SmallStrip_btn_Provider.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_SmallStrip_btn_Provider.Click += new System.EventHandler(this.ts_SmallStrip_btn_Provider_Click);
            // 
            // btn_Right1
            // 
            this.btn_Right1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btn_Right1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right1.FlatAppearance.BorderSize = 0;
            this.btn_Right1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Right1.Location = new System.Drawing.Point(3, 4);
            this.btn_Right1.Name = "btn_Right1";
            this.btn_Right1.Size = new System.Drawing.Size(24, 22);
            this.btn_Right1.TabIndex = 16;
            this.btn_Right1.UseVisualStyleBackColor = false;
            this.btn_Right1.MouseLeave += new System.EventHandler(this.btn_Right1_MouseLeave);
            this.btn_Right1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Right1_MouseMove);
            this.btn_Right1.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // lbl_pnlSmallStripLeftBrd
            // 
            this.lbl_pnlSmallStripLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSmallStripLeftBrd.Location = new System.Drawing.Point(2, 4);
            this.lbl_pnlSmallStripLeftBrd.Name = "lbl_pnlSmallStripLeftBrd";
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 907);
            this.lbl_pnlSmallStripLeftBrd.TabIndex = 9;
            // 
            // lbl_pnlSmallStripTopBrd
            // 
            this.lbl_pnlSmallStripTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSmallStripTopBrd.Location = new System.Drawing.Point(2, 3);
            this.lbl_pnlSmallStripTopBrd.Name = "lbl_pnlSmallStripTopBrd";
            this.lbl_pnlSmallStripTopBrd.Size = new System.Drawing.Size(25, 1);
            this.lbl_pnlSmallStripTopBrd.TabIndex = 141;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(27, 3);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 908);
            this.label53.TabIndex = 143;
            // 
            // spt_pnlViewScheduleTop
            // 
            this.spt_pnlViewScheduleTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.spt_pnlViewScheduleTop.Location = new System.Drawing.Point(243, 155);
            this.spt_pnlViewScheduleTop.Name = "spt_pnlViewScheduleTop";
            this.spt_pnlViewScheduleTop.Size = new System.Drawing.Size(852, 3);
            this.spt_pnlViewScheduleTop.TabIndex = 144;
            this.spt_pnlViewScheduleTop.TabStop = false;
            this.spt_pnlViewScheduleTop.Visible = false;
            // 
            // cmnu_ScheduleNew
            // 
            this.cmnu_ScheduleNew.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnu_Schedule_New,
            this.cmnu_Schedule_GoTo,
            this.cmnu_Schedule_Refresh,
            this.cmnu_Schedule_Paste});
            this.cmnu_ScheduleNew.Name = "cmnu_Schedule";
            this.cmnu_ScheduleNew.Size = new System.Drawing.Size(153, 92);
            this.cmnu_ScheduleNew.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmnu_ScheduleEdit_ItemClicked);
            // 
            // cmnu_Schedule_New
            // 
            this.cmnu_Schedule_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_New.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_New.Image")));
            this.cmnu_Schedule_New.Name = "cmnu_Schedule_New";
            this.cmnu_Schedule_New.Size = new System.Drawing.Size(152, 22);
            this.cmnu_Schedule_New.Text = "New Schedule";
            // 
            // cmnu_Schedule_GoTo
            // 
            this.cmnu_Schedule_GoTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_GoTo.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_GoTo.Image")));
            this.cmnu_Schedule_GoTo.Name = "cmnu_Schedule_GoTo";
            this.cmnu_Schedule_GoTo.Size = new System.Drawing.Size(152, 22);
            this.cmnu_Schedule_GoTo.Text = "Go To";
            // 
            // cmnu_Schedule_Refresh
            // 
            this.cmnu_Schedule_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Refresh.Image")));
            this.cmnu_Schedule_Refresh.Name = "cmnu_Schedule_Refresh";
            this.cmnu_Schedule_Refresh.Size = new System.Drawing.Size(152, 22);
            this.cmnu_Schedule_Refresh.Text = "Refresh";
            // 
            // cmnu_Schedule_Paste
            // 
            this.cmnu_Schedule_Paste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Paste.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Paste.Image")));
            this.cmnu_Schedule_Paste.Name = "cmnu_Schedule_Paste";
            this.cmnu_Schedule_Paste.Size = new System.Drawing.Size(152, 22);
            this.cmnu_Schedule_Paste.Text = "Paste";
            // 
            // cmnu_ScheduleEdit
            // 
            this.cmnu_ScheduleEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnu_Schedule_Open,
            this.cmnu_Schedule_Print,
            this.cmnu_Schedule_Delete,
            this.cmnu_Schedule_AddNotes,
            this.cmnu_Schedule_Cut,
            this.cmnu_Schedule_Copy});
            this.cmnu_ScheduleEdit.Name = "cmnu_Appointment";
            this.cmnu_ScheduleEdit.Size = new System.Drawing.Size(136, 136);
            this.cmnu_ScheduleEdit.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmnu_ScheduleEdit_ItemClicked);
            // 
            // cmnu_Schedule_Open
            // 
            this.cmnu_Schedule_Open.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Open.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Open.Image")));
            this.cmnu_Schedule_Open.Name = "cmnu_Schedule_Open";
            this.cmnu_Schedule_Open.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_Open.Text = "Open";
            // 
            // cmnu_Schedule_Print
            // 
            this.cmnu_Schedule_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Print.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Print.Image")));
            this.cmnu_Schedule_Print.Name = "cmnu_Schedule_Print";
            this.cmnu_Schedule_Print.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_Print.Text = "Print";
            // 
            // cmnu_Schedule_Delete
            // 
            this.cmnu_Schedule_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Delete.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Delete.Image")));
            this.cmnu_Schedule_Delete.Name = "cmnu_Schedule_Delete";
            this.cmnu_Schedule_Delete.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_Delete.Text = "Delete";
            // 
            // cmnu_Schedule_AddNotes
            // 
            this.cmnu_Schedule_AddNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_AddNotes.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_AddNotes.Image")));
            this.cmnu_Schedule_AddNotes.Name = "cmnu_Schedule_AddNotes";
            this.cmnu_Schedule_AddNotes.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_AddNotes.Text = "Add Notes";
            // 
            // cmnu_Schedule_Cut
            // 
            this.cmnu_Schedule_Cut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Cut.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Cut.Image")));
            this.cmnu_Schedule_Cut.Name = "cmnu_Schedule_Cut";
            this.cmnu_Schedule_Cut.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_Cut.Text = "Cut";
            // 
            // cmnu_Schedule_Copy
            // 
            this.cmnu_Schedule_Copy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_Schedule_Copy.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_Schedule_Copy.Image")));
            this.cmnu_Schedule_Copy.Name = "cmnu_Schedule_Copy";
            this.cmnu_Schedule_Copy.Size = new System.Drawing.Size(135, 22);
            this.cmnu_Schedule_Copy.Text = "Copy";
            // 
            // frmViewSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1095, 967);
            this.Controls.Add(this.pnlViewSchedule);
            this.Controls.Add(this.spt_pnlViewScheduleTop);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlSmallStrip);
            this.Controls.Add(this.splSearchList);
            this.Controls.Add(this.pnlSearchList);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schedule";
            this.Load += new System.EventHandler(this.frmViewSchedule_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewSchedule_FormClosed);
            this.Resize += new System.EventHandler(this.frmViewSchedule_Resize);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.juc_Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.juc_ViewSchedule)).EndInit();
            this.pnlCalendarHeader.ResumeLayout(false);
            this.pnlSearchList.ResumeLayout(false);
            this.pnlSelection.ResumeLayout(false);
            this.pnlSelectionBody.ResumeLayout(false);
            this.pnlSelectionBody.PerformLayout();
            this.pnlSelectionHeader.ResumeLayout(false);
            this.pnlResources.ResumeLayout(false);
            this.pnlResources_trv.ResumeLayout(false);
            this.pnlResourcesHeader.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnlCalendar.ResumeLayout(false);
            this.cntMenuMain.ResumeLayout(false);
            this.pnlSearchCriteria.ResumeLayout(false);
            this.pnlSearchCriteria.PerformLayout();
            this.pnlSearchCriteriaHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.pnlViewSchedule.ResumeLayout(false);
            this.pnlSmallStrip.ResumeLayout(false);
            this.pnlSmallStrip.PerformLayout();
            this.ts_SmallStrip.ResumeLayout(false);
            this.ts_SmallStrip.PerformLayout();
            this.cmnu_ScheduleNew.ResumeLayout(false);
            this.cmnu_ScheduleEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_NewSchedule;
        internal System.Windows.Forms.ToolStripButton tsb_DeleteSchedule;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.ToolStripButton tsb_Help;
        private System.Windows.Forms.ToolStripButton tsb_DayView;
        private System.Windows.Forms.ToolStripButton tsb_WeekView;
        private System.Windows.Forms.ToolStripButton tsb_MonthView;
        private System.Windows.Forms.Panel pnlCalendarHeader;
        private System.Windows.Forms.Label lblCalender;
        private Janus.Windows.Schedule.Calendar juc_Calendar;
        private System.Windows.Forms.Panel pnlSearchList;
        private System.Windows.Forms.Splitter splSearchList;
        private Janus.Windows.Schedule.Schedule juc_ViewSchedule;
        private System.Windows.Forms.ContextMenuStrip cntMenuMain;
        private System.Windows.Forms.ToolStripMenuItem NewSchedule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ModifySchedule;
        private System.Windows.Forms.Panel pnlSearchCriteria;
        private System.Windows.Forms.ComboBox cmbResource;
        internal System.Windows.Forms.Label lblResource;
        private System.Windows.Forms.ComboBox cmbDepartment;
        internal System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbLocation;
        internal System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Panel pnlSearchCriteriaHeader;
        private System.Windows.Forms.Label lblSerchCriteria;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton tsb_EditSchedule;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Panel pnlCalendar;
        private System.Windows.Forms.Panel pnlViewSchedule;
        private System.Windows.Forms.Splitter Spt_pnlProviderTop;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label lbl_pnlProviderBottomBrd;
        private System.Windows.Forms.Panel pnlProviderBody;
        private System.Windows.Forms.TreeView trvProvider;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Label lblProviderResources;
        private System.Windows.Forms.Label lbl_pnlProviderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlProviderRightBrd;
        private System.Windows.Forms.Label lbl_pnlProviderTopBrd;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.Label lbl_pnlSelectionBottomBrd;
        private System.Windows.Forms.Panel pnlSelectionBody;
        private System.Windows.Forms.Panel pnlSelectionHeader;
        private System.Windows.Forms.Label lblSelection;
        private System.Windows.Forms.Label lbl_pnlSelectionLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSelectionRightBrd;
        private System.Windows.Forms.Label lbl_pnlSelectionTopBrd;
        private System.Windows.Forms.Splitter Spt_pnlSelectionTop;
        internal System.Windows.Forms.ToolStripDropDownButton tls_btnTimeNavigation;
        private System.Windows.Forms.ToolStripMenuItem tls_btnTimeNavigation_5Min;
        private System.Windows.Forms.ToolStripMenuItem tls_btnTimeNavigation_10Min;
        private System.Windows.Forms.ToolStripMenuItem tls_btnTimeNavigation_15Min;
        private System.Windows.Forms.ToolStripMenuItem tls_btnTimeNavigation_30Min;
        private System.Windows.Forms.Label lbl_pnlSearchCriteriaBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchCriteriaHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchCriterialeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchCriteriaRightBrd;
        private System.Windows.Forms.Label lbl_pnlSearchCriteriaTopBrd;
        private System.Windows.Forms.Panel pnlSmallStrip;
        private gloGlobal.gloToolStripIgnoreFocus ts_SmallStrip;
        private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Calendar;
        private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Provider;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Splitter spt_pnlViewScheduleTop;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Label lbl_pnlViewScheduleRightBrd;
        private System.Windows.Forms.Label lbl_pnlViewScheduleLeftBrd;
        private System.Windows.Forms.Label lbl_pnlViewScheduleTopBrd;
        private System.Windows.Forms.Label lbl_pnlViewScheduleBottomBrd;
        private System.Windows.Forms.ContextMenuStrip cmnu_ScheduleNew;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_New;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_GoTo;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Refresh;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Paste;
        private System.Windows.Forms.ContextMenuStrip cmnu_ScheduleEdit;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Open;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Print;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Delete;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_AddNotes;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Cut;
        private System.Windows.Forms.ToolStripMenuItem cmnu_Schedule_Copy;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lbl_pnlCalendarTopBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label lbl_pnlCalendarHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSelectionHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlProviderHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCalendarBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCalendarLeftBrd;
        private System.Windows.Forms.Label lbl_pnlCalendarRightBrd;
        private System.Windows.Forms.Panel pnlResources;
        private System.Windows.Forms.Label lbl_pnlResourcesBottomBrd;
        private System.Windows.Forms.Panel pnlResources_trv;
        private System.Windows.Forms.TreeView trvResources;
        private System.Windows.Forms.Panel pnlResourcesHeader;
        private System.Windows.Forms.Label lbl_pnlResourcesHeaderBottomBrd;
        private System.Windows.Forms.Label lblResources;
        private System.Windows.Forms.Label lbl_pnlResourcesLeftBrd;
        private System.Windows.Forms.Label lbl_pnlResourcesRightBrd;
        private System.Windows.Forms.Label lbl_pnlResourcesTopBrd;
        private System.Windows.Forms.Splitter Spt_pnlResourcesTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsb_Today;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Button btnDeSelectProvider;
        private System.Windows.Forms.Button btnSelectProvider;
        private System.Windows.Forms.Button btnDeSelectResource;
        private System.Windows.Forms.Button btnSelectResource;
        private System.Drawing.Printing.PrintDocument printSchedule;
        private System.Windows.Forms.Label label5;
    }
}