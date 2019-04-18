namespace gloAppointmentScheduling
{
    partial class frmSearchAppointment
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpRec_Range_StartDate, dtpRec_Range_EndBy, dtpEndDate, dtpStartDate };
            System.Windows.Forms.Control[] cntControls = { dtpRec_Range_StartDate, dtpRec_Range_EndBy, dtpEndDate, dtpStartDate };

            if (disposing && (components != null))
            {

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (cmnu_Appointment != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_Appointment);
                        if (cmnu_Appointment.Items != null)
                        {
                            cmnu_Appointment.Items.Clear();

                        }
                        cmnu_Appointment.Dispose();
                        cmnu_Appointment = null;
                    }
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);


            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }


            System.Windows.Forms.ContextMenuStrip[] dtpControlsContextMenuStrip = { cmnu_Appointment };
            

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControlsContextMenuStrip);
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(ref dtpControlsContextMenuStrip);

                }
            }

            


        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchAppointment));
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlRecurrance_Body = new System.Windows.Forms.Panel();
            this.pnlRecurring_Range = new System.Windows.Forms.Panel();
            this.cmbRec_Range_NoEndDateYear = new System.Windows.Forms.ComboBox();
            this.lbl_pnlRecurring_RangeBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeTopBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Range_Header = new System.Windows.Forms.Panel();
            this.lblRecurring_Range_Header = new System.Windows.Forms.Label();
            this.rbRec_Range_NoEndDate = new System.Windows.Forms.RadioButton();
            this.lblRec_Range_EndDate = new System.Windows.Forms.Label();
            this.dtpRec_Range_StartDate = new System.Windows.Forms.DateTimePicker();
            this.rbRec_Range_EndBy = new System.Windows.Forms.RadioButton();
            this.lblRec_Range_StartDate = new System.Windows.Forms.Label();
            this.rbRec_Range_EndAfterOccurence = new System.Windows.Forms.RadioButton();
            this.dtpRec_Range_EndBy = new System.Windows.Forms.DateTimePicker();
            this.numRec_Range_EndAfterOccurence = new System.Windows.Forms.NumericUpDown();
            this.lblRec_Range_Occurence = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeRightBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Pattern_Body = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Yearly = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Monthly = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Weekly = new System.Windows.Forms.RadioButton();
            this.lbl_pnlRecurring_Pattern_BodyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_BodyRightBrd = new System.Windows.Forms.Label();
            this.pnlRec_Pattern_Daily = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_DailyLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyTopBrd = new System.Windows.Forms.Label();
            this.numRec_Pattern_Daily_EveryDay = new System.Windows.Forms.NumericUpDown();
            this.lblRec_Pattern_Daily_Days = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Daily_EveryWeekday = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily_EveryDay = new System.Windows.Forms.RadioButton();
            this.pnlRec_Pattern_Monthly = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_MonthlyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd = new System.Windows.Forms.Label();
            this.numRec_Pattern_Monthly_Criteria_Month = new System.Windows.Forms.NumericUpDown();
            this.numRec_Pattern_Monthly_Day_Month = new System.Windows.Forms.NumericUpDown();
            this.numRec_Pattern_Monthly_Day_Day = new System.Windows.Forms.NumericUpDown();
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Monthly_Criteria_FstLst = new System.Windows.Forms.ComboBox();
            this.lblRec_Pattern_Monthly_Criteria_Month = new System.Windows.Forms.Label();
            this.lblRec_Pattern_Monthly_Criteria_Every = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Monthly_Criteria = new System.Windows.Forms.RadioButton();
            this.lblRec_Pattern_Monthly_Day_Month = new System.Windows.Forms.Label();
            this.lblRec_Pattern_Monthly_Day_Every = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Monthly_Day = new System.Windows.Forms.RadioButton();
            this.pnlRec_Pattern_Yearly = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_YearlyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_YearlyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_YearlyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_YearlyLeftBrd = new System.Windows.Forms.Label();
            this.numRec_Pattern_Yearly_Every_MonthDay = new System.Windows.Forms.NumericUpDown();
            this.cmbRec_Pattern_Yearly_Criteria_Month = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Every_Month = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Criteria_FstLst = new System.Windows.Forms.ComboBox();
            this.lblRec_Pattern_Yearly_Criteria_Of = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Yearly_Criteria = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Yearly_EveryMonthDay = new System.Windows.Forms.RadioButton();
            this.pnlRec_Pattern_Weekly = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_WeeklyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_WeeklyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd = new System.Windows.Forms.Label();
            this.lblRec_Pattern_Weekly_WeekOn = new System.Windows.Forms.Label();
            this.lblRec_Pattern_Weekly_RecurEvery = new System.Windows.Forms.Label();
            this.ChkRec_Pattern_Weekly_Saturday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Friday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Sunday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Tuesday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Wednesday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Thursday = new System.Windows.Forms.CheckBox();
            this.ChkRec_Pattern_Weekly_Monday = new System.Windows.Forms.CheckBox();
            this.numRec_Pattern_Weekly_WeekOn = new System.Windows.Forms.NumericUpDown();
            this.pnlProgressbar = new System.Windows.Forms.Panel();
            this.lblSearchingMessage = new System.Windows.Forms.Label();
            this.progbarSearch = new System.Windows.Forms.ProgressBar();
            this.pnlRecurring_Pattern_Header = new System.Windows.Forms.Panel();
            this.lblRecurring_Pattern_Header = new System.Windows.Forms.Label();
            this.chkApplyRecPattern = new System.Windows.Forms.CheckBox();
            this.btn_Recurrencepattern_UP = new System.Windows.Forms.Button();
            this.btn_Recurrencepattern_Down = new System.Windows.Forms.Button();
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd = new System.Windows.Forms.Label();
            this.pnl_AdvanceSearch_Body = new System.Windows.Forms.Panel();
            this.rbResourceFilter = new System.Windows.Forms.RadioButton();
            this.rbProblemTypeFilter = new System.Windows.Forms.RadioButton();
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_AdvanceSearch_BodyRightBrd = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lbl_pnl_AdvanceSearch_BodyTopBrd = new System.Windows.Forms.Label();
            this.cmbResources = new System.Windows.Forms.ComboBox();
            this.cmbProcedures = new System.Windows.Forms.ComboBox();
            this.lbl_Department = new System.Windows.Forms.Label();
            this.btn_BrowseProcedure = new System.Windows.Forms.Button();
            this.btn_ClearProcedure = new System.Windows.Forms.Button();
            this.btn_ClearResource = new System.Windows.Forms.Button();
            this.lbl_Location = new System.Windows.Forms.Label();
            this.btn_BrowseResource = new System.Windows.Forms.Button();
            this.pnlAdvanceSearchHeader = new System.Windows.Forms.Panel();
            this.pnlAdvanceSearch_Down = new System.Windows.Forms.Panel();
            this.btn_AdvanceSearch_Down = new System.Windows.Forms.Button();
            this.pnlAdvanceSearch_UP = new System.Windows.Forms.Panel();
            this.btn_AdvanceSearch_UP = new System.Windows.Forms.Button();
            this.lblAdvanceSearch = new System.Windows.Forms.Label();
            this.chkApplyAdvSearch = new System.Windows.Forms.CheckBox();
            this.lbl_pnlAdvanceSearchHeader_LeftSpace = new System.Windows.Forms.Label();
            this.lbl_pnlAdvanceSearchHeaderTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAdvanceSearchHeaderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAdvanceSearchHeaderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAdvanceSearchHeaderBottomBrd = new System.Windows.Forms.Label();
            this.pnlAppointmentSearchBody = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trvAppointmentType = new System.Windows.Forms.TreeView();
            this.label18 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClearAllAppTypes = new System.Windows.Forms.Button();
            this.btnSelectAllAppTypes = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbLocation_Appointment = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbDepartment_Appointment = new System.Windows.Forms.ComboBox();
            this.pnl_AppointmentSearchHeader = new System.Windows.Forms.Panel();
            this.pnlAppSearch_Down = new System.Windows.Forms.Panel();
            this.btn_AppSearch_Down = new System.Windows.Forms.Button();
            this.pnlAppSearch_Up = new System.Windows.Forms.Panel();
            this.btn_AppSearch_Up = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSearchInTemplate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlSimpleSearch = new System.Windows.Forms.Panel();
            this.pnlSearchDates = new System.Windows.Forms.Panel();
            this.lbl_pnlSimpleSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSimpleSearchRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSimpleSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSimpleSearchLeftBrd = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbAMPMAll = new System.Windows.Forms.ComboBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblSimple_Duration = new System.Windows.Forms.Label();
            this.num_Duration = new System.Windows.Forms.NumericUpDown();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trvProvider = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnClearAllProvider = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.btnSelectAllProvider = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlSimpleSearchHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlSimpleSearchHeaderLeftBrd = new System.Windows.Forms.Label();
            this.lblSimpleSearch = new System.Windows.Forms.Label();
            this.pnl_btn_HideSearchPnl = new System.Windows.Forms.Panel();
            this.btn_HideSearchPnl = new System.Windows.Forms.Button();
            this.lbl_pnlSimpleSearchHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSimpleSearchHeaderTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSimpleSearchHeaderRightBrd = new System.Windows.Forms.Label();
            this.pnlCalendarView = new System.Windows.Forms.Panel();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.lbl_pnlJanusControlBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlJanusControlLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlJanusControlRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlJanusControlTopBrd = new System.Windows.Forms.Label();
            this.juc_Appointment = new Janus.Windows.Schedule.Schedule();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.pnlOther2 = new System.Windows.Forms.Panel();
            this.lbl_pnlOther2BottomBrd = new System.Windows.Forms.Label();
            this.pnlOther2Header = new System.Windows.Forms.Panel();
            this.lbl_pnlOther2HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblOther2 = new System.Windows.Forms.Label();
            this.lbl_pnlOther2LeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlOther2RightBrd = new System.Windows.Forms.Label();
            this.lbl_lbl_pnlOther2TopBrd = new System.Windows.Forms.Label();
            this.pnlCalender = new System.Windows.Forms.Panel();
            this.pnlListView = new System.Windows.Forms.Panel();
            this.c1Appointments = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbListViewProvider = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlVertical_tslp = new System.Windows.Forms.Panel();
            this.tl_Search = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSearch = new System.Windows.Forms.ToolStripButton();
            this.label16 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.btn_ShowSearchPnl = new System.Windows.Forms.Button();
            this.lbl_pnlVertical_tslpLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlVertical_tslpTopBrd = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmnu_Appointment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnu_SetAppointment = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_DayView = new System.Windows.Forms.ToolStripButton();
            this.tsb_WeekView = new System.Windows.Forms.ToolStripButton();
            this.tsb_MonthView = new System.Windows.Forms.ToolStripButton();
            this.tsb_SearchAppointment = new System.Windows.Forms.ToolStripButton();
            this.tsb_ListView = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSearch.SuspendLayout();
            this.pnlRecurrance_Body.SuspendLayout();
            this.pnlRecurring_Range.SuspendLayout();
            this.pnlRecurring_Range_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).BeginInit();
            this.pnlRecurring_Pattern_Body.SuspendLayout();
            this.pnlRec_Pattern_Daily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).BeginInit();
            this.pnlRec_Pattern_Monthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).BeginInit();
            this.pnlRec_Pattern_Yearly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).BeginInit();
            this.pnlRec_Pattern_Weekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).BeginInit();
            this.pnlProgressbar.SuspendLayout();
            this.pnlRecurring_Pattern_Header.SuspendLayout();
            this.pnl_AdvanceSearch_Body.SuspendLayout();
            this.pnlAdvanceSearchHeader.SuspendLayout();
            this.pnlAdvanceSearch_Down.SuspendLayout();
            this.pnlAdvanceSearch_UP.SuspendLayout();
            this.pnlAppointmentSearchBody.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnl_AppointmentSearchHeader.SuspendLayout();
            this.pnlAppSearch_Down.SuspendLayout();
            this.pnlAppSearch_Up.SuspendLayout();
            this.pnlSimpleSearch.SuspendLayout();
            this.pnlSearchDates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Duration)).BeginInit();
            this.pnlProvider.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnlSimpleSearchHeader.SuspendLayout();
            this.pnl_btn_HideSearchPnl.SuspendLayout();
            this.pnlCalendarView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.juc_Appointment)).BeginInit();
            this.pnlOther2.SuspendLayout();
            this.pnlOther2Header.SuspendLayout();
            this.pnlCalender.SuspendLayout();
            this.pnlListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).BeginInit();
            this.panel7.SuspendLayout();
            this.pnlVertical_tslp.SuspendLayout();
            this.tl_Search.SuspendLayout();
            this.cmnu_Appointment.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnl_Main.SuspendLayout();
            this.pnl_ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.pnlRecurrance_Body);
            this.pnlSearch.Controls.Add(this.pnlProgressbar);
            this.pnlSearch.Controls.Add(this.pnlRecurring_Pattern_Header);
            this.pnlSearch.Controls.Add(this.pnl_AdvanceSearch_Body);
            this.pnlSearch.Controls.Add(this.pnlAdvanceSearchHeader);
            this.pnlSearch.Controls.Add(this.pnlAppointmentSearchBody);
            this.pnlSearch.Controls.Add(this.pnl_AppointmentSearchHeader);
            this.pnlSearch.Controls.Add(this.pnlSimpleSearch);
            this.pnlSearch.Controls.Add(this.pnlSimpleSearchHeader);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSearch.Size = new System.Drawing.Size(437, 807);
            this.pnlSearch.TabIndex = 65;
            // 
            // pnlRecurrance_Body
            // 
            this.pnlRecurrance_Body.Controls.Add(this.pnlRecurring_Range);
            this.pnlRecurrance_Body.Controls.Add(this.pnlRecurring_Pattern_Body);
            this.pnlRecurrance_Body.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurrance_Body.Location = new System.Drawing.Point(0, 717);
            this.pnlRecurrance_Body.Name = "pnlRecurrance_Body";
            this.pnlRecurrance_Body.Size = new System.Drawing.Size(437, 271);
            this.pnlRecurrance_Body.TabIndex = 129;
            this.pnlRecurrance_Body.Visible = false;
            // 
            // pnlRecurring_Range
            // 
            this.pnlRecurring_Range.Controls.Add(this.cmbRec_Range_NoEndDateYear);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeBottomBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeTopBrd);
            this.pnlRecurring_Range.Controls.Add(this.pnlRecurring_Range_Header);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_EndDate);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.numRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_Occurence);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeLeftBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeRightBrd);
            this.pnlRecurring_Range.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecurring_Range.Location = new System.Drawing.Point(0, 157);
            this.pnlRecurring_Range.Name = "pnlRecurring_Range";
            this.pnlRecurring_Range.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRecurring_Range.Size = new System.Drawing.Size(437, 114);
            this.pnlRecurring_Range.TabIndex = 119;
            // 
            // cmbRec_Range_NoEndDateYear
            // 
            this.cmbRec_Range_NoEndDateYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Range_NoEndDateYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Range_NoEndDateYear.ForeColor = System.Drawing.Color.Black;
            this.cmbRec_Range_NoEndDateYear.FormattingEnabled = true;
            this.cmbRec_Range_NoEndDateYear.Location = new System.Drawing.Point(188, 20);
            this.cmbRec_Range_NoEndDateYear.Name = "cmbRec_Range_NoEndDateYear";
            this.cmbRec_Range_NoEndDateYear.Size = new System.Drawing.Size(55, 22);
            this.cmbRec_Range_NoEndDateYear.TabIndex = 26;
            this.cmbRec_Range_NoEndDateYear.Visible = false;
            // 
            // lbl_pnlRecurring_RangeBottomBrd
            // 
            this.lbl_pnlRecurring_RangeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_RangeBottomBrd.Location = new System.Drawing.Point(4, 110);
            this.lbl_pnlRecurring_RangeBottomBrd.Name = "lbl_pnlRecurring_RangeBottomBrd";
            this.lbl_pnlRecurring_RangeBottomBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlRecurring_RangeBottomBrd.TabIndex = 20;
            // 
            // lbl_pnlRecurring_RangeTopBrd
            // 
            this.lbl_pnlRecurring_RangeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_RangeTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlRecurring_RangeTopBrd.Name = "lbl_pnlRecurring_RangeTopBrd";
            this.lbl_pnlRecurring_RangeTopBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlRecurring_RangeTopBrd.TabIndex = 19;
            // 
            // pnlRecurring_Range_Header
            // 
            this.pnlRecurring_Range_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Range_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Range_Header.Controls.Add(this.lblRecurring_Range_Header);
            this.pnlRecurring_Range_Header.Controls.Add(this.rbRec_Range_NoEndDate);
            this.pnlRecurring_Range_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Range_Header.Location = new System.Drawing.Point(4, 3);
            this.pnlRecurring_Range_Header.Name = "pnlRecurring_Range_Header";
            this.pnlRecurring_Range_Header.Size = new System.Drawing.Size(429, 0);
            this.pnlRecurring_Range_Header.TabIndex = 5;
            // 
            // lblRecurring_Range_Header
            // 
            this.lblRecurring_Range_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Range_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Range_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Range_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Range_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_Range_Header.Name = "lblRecurring_Range_Header";
            this.lblRecurring_Range_Header.Size = new System.Drawing.Size(429, 0);
            this.lblRecurring_Range_Header.TabIndex = 0;
            this.lblRecurring_Range_Header.Text = " Range of recurrence";
            this.lblRecurring_Range_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbRec_Range_NoEndDate
            // 
            this.rbRec_Range_NoEndDate.AutoSize = true;
            this.rbRec_Range_NoEndDate.BackColor = System.Drawing.Color.Transparent;
            this.rbRec_Range_NoEndDate.Checked = true;
            this.rbRec_Range_NoEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_NoEndDate.Location = new System.Drawing.Point(548, 3);
            this.rbRec_Range_NoEndDate.Name = "rbRec_Range_NoEndDate";
            this.rbRec_Range_NoEndDate.Size = new System.Drawing.Size(92, 18);
            this.rbRec_Range_NoEndDate.TabIndex = 14;
            this.rbRec_Range_NoEndDate.TabStop = true;
            this.rbRec_Range_NoEndDate.Text = "For this year";
            this.rbRec_Range_NoEndDate.UseVisualStyleBackColor = false;
            this.rbRec_Range_NoEndDate.Visible = false;
            // 
            // lblRec_Range_EndDate
            // 
            this.lblRec_Range_EndDate.AutoSize = true;
            this.lblRec_Range_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_EndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_EndDate.Location = new System.Drawing.Point(16, 59);
            this.lblRec_Range_EndDate.Name = "lblRec_Range_EndDate";
            this.lblRec_Range_EndDate.Size = new System.Drawing.Size(66, 14);
            this.lblRec_Range_EndDate.TabIndex = 17;
            this.lblRec_Range_EndDate.Text = "End Date :";
            // 
            // dtpRec_Range_StartDate
            // 
            this.dtpRec_Range_StartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRec_Range_StartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_Range_StartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRec_Range_StartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRec_Range_StartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRec_Range_StartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpRec_Range_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRec_Range_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_Range_StartDate.Location = new System.Drawing.Point(84, 20);
            this.dtpRec_Range_StartDate.Name = "dtpRec_Range_StartDate";
            this.dtpRec_Range_StartDate.Size = new System.Drawing.Size(98, 22);
            this.dtpRec_Range_StartDate.TabIndex = 10;
            this.dtpRec_Range_StartDate.ValueChanged += new System.EventHandler(this.dtpRec_Range_StartDate_ValueChanged);
            // 
            // rbRec_Range_EndBy
            // 
            this.rbRec_Range_EndBy.AutoSize = true;
            this.rbRec_Range_EndBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_EndBy.Location = new System.Drawing.Point(84, 85);
            this.rbRec_Range_EndBy.Name = "rbRec_Range_EndBy";
            this.rbRec_Range_EndBy.Size = new System.Drawing.Size(63, 18);
            this.rbRec_Range_EndBy.TabIndex = 16;
            this.rbRec_Range_EndBy.Text = "End by";
            this.rbRec_Range_EndBy.UseVisualStyleBackColor = true;
            this.rbRec_Range_EndBy.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndBy_CheckedChanged);
            // 
            // lblRec_Range_StartDate
            // 
            this.lblRec_Range_StartDate.AutoSize = true;
            this.lblRec_Range_StartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_StartDate.Location = new System.Drawing.Point(10, 23);
            this.lblRec_Range_StartDate.Name = "lblRec_Range_StartDate";
            this.lblRec_Range_StartDate.Size = new System.Drawing.Size(72, 14);
            this.lblRec_Range_StartDate.TabIndex = 9;
            this.lblRec_Range_StartDate.Text = "Start Date :";
            // 
            // rbRec_Range_EndAfterOccurence
            // 
            this.rbRec_Range_EndAfterOccurence.AutoSize = true;
            this.rbRec_Range_EndAfterOccurence.Checked = true;
            this.rbRec_Range_EndAfterOccurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(84, 57);
            this.rbRec_Range_EndAfterOccurence.Name = "rbRec_Range_EndAfterOccurence";
            this.rbRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(82, 18);
            this.rbRec_Range_EndAfterOccurence.TabIndex = 15;
            this.rbRec_Range_EndAfterOccurence.TabStop = true;
            this.rbRec_Range_EndAfterOccurence.Text = "End after";
            this.rbRec_Range_EndAfterOccurence.UseVisualStyleBackColor = true;
            this.rbRec_Range_EndAfterOccurence.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndAfterOccurence_CheckedChanged);
            // 
            // dtpRec_Range_EndBy
            // 
            this.dtpRec_Range_EndBy.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRec_Range_EndBy.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_Range_EndBy.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRec_Range_EndBy.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRec_Range_EndBy.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRec_Range_EndBy.CustomFormat = "MM/dd/yyyy";
            this.dtpRec_Range_EndBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRec_Range_EndBy.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_Range_EndBy.Location = new System.Drawing.Point(171, 84);
            this.dtpRec_Range_EndBy.Name = "dtpRec_Range_EndBy";
            this.dtpRec_Range_EndBy.Size = new System.Drawing.Size(93, 22);
            this.dtpRec_Range_EndBy.TabIndex = 11;
            this.dtpRec_Range_EndBy.ValueChanged += new System.EventHandler(this.dtpRec_Range_EndBy_ValueChanged);
            // 
            // numRec_Range_EndAfterOccurence
            // 
            this.numRec_Range_EndAfterOccurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Range_EndAfterOccurence.ForeColor = System.Drawing.Color.Black;
            this.numRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(171, 56);
            this.numRec_Range_EndAfterOccurence.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numRec_Range_EndAfterOccurence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Range_EndAfterOccurence.Name = "numRec_Range_EndAfterOccurence";
            this.numRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(93, 22);
            this.numRec_Range_EndAfterOccurence.TabIndex = 13;
            this.numRec_Range_EndAfterOccurence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Range_EndAfterOccurence.ValueChanged += new System.EventHandler(this.numRec_Range_EndAfterOccurence_ValueChanged);
            // 
            // lblRec_Range_Occurence
            // 
            this.lblRec_Range_Occurence.AutoSize = true;
            this.lblRec_Range_Occurence.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_Occurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_Occurence.Location = new System.Drawing.Point(267, 60);
            this.lblRec_Range_Occurence.Name = "lblRec_Range_Occurence";
            this.lblRec_Range_Occurence.Size = new System.Drawing.Size(75, 14);
            this.lblRec_Range_Occurence.TabIndex = 12;
            this.lblRec_Range_Occurence.Text = "Occurrences";
            // 
            // lbl_pnlRecurring_RangeLeftBrd
            // 
            this.lbl_pnlRecurring_RangeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_RangeLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlRecurring_RangeLeftBrd.Name = "lbl_pnlRecurring_RangeLeftBrd";
            this.lbl_pnlRecurring_RangeLeftBrd.Size = new System.Drawing.Size(1, 108);
            this.lbl_pnlRecurring_RangeLeftBrd.TabIndex = 121;
            // 
            // lbl_pnlRecurring_RangeRightBrd
            // 
            this.lbl_pnlRecurring_RangeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_RangeRightBrd.Location = new System.Drawing.Point(433, 3);
            this.lbl_pnlRecurring_RangeRightBrd.Name = "lbl_pnlRecurring_RangeRightBrd";
            this.lbl_pnlRecurring_RangeRightBrd.Size = new System.Drawing.Size(1, 108);
            this.lbl_pnlRecurring_RangeRightBrd.TabIndex = 122;
            // 
            // pnlRecurring_Pattern_Body
            // 
            this.pnlRecurring_Pattern_Body.Controls.Add(this.lbl_pnlRecurring_Pattern_BodyBottomBrd);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.rbRec_Pattern_Yearly);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.rbRec_Pattern_Monthly);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.rbRec_Pattern_Daily);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.rbRec_Pattern_Weekly);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.lbl_pnlRecurring_Pattern_BodyTopBrd);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.lbl_pnlRecurring_Pattern_BodyLeftBrd);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.lbl_pnlRecurring_Pattern_BodyRightBrd);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.pnlRec_Pattern_Daily);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.pnlRec_Pattern_Monthly);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.pnlRec_Pattern_Yearly);
            this.pnlRecurring_Pattern_Body.Controls.Add(this.pnlRec_Pattern_Weekly);
            this.pnlRecurring_Pattern_Body.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Pattern_Body.Location = new System.Drawing.Point(0, 0);
            this.pnlRecurring_Pattern_Body.Name = "pnlRecurring_Pattern_Body";
            this.pnlRecurring_Pattern_Body.Padding = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.pnlRecurring_Pattern_Body.Size = new System.Drawing.Size(437, 157);
            this.pnlRecurring_Pattern_Body.TabIndex = 4;
            // 
            // lbl_pnlRecurring_Pattern_BodyBottomBrd
            // 
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.Location = new System.Drawing.Point(4, 154);
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.Name = "lbl_pnlRecurring_Pattern_BodyBottomBrd";
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlRecurring_Pattern_BodyBottomBrd.TabIndex = 123;
            // 
            // rbRec_Pattern_Yearly
            // 
            this.rbRec_Pattern_Yearly.AutoSize = true;
            this.rbRec_Pattern_Yearly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Yearly.Location = new System.Drawing.Point(250, 16);
            this.rbRec_Pattern_Yearly.Name = "rbRec_Pattern_Yearly";
            this.rbRec_Pattern_Yearly.Size = new System.Drawing.Size(58, 18);
            this.rbRec_Pattern_Yearly.TabIndex = 4;
            this.rbRec_Pattern_Yearly.Text = "Yearly";
            this.rbRec_Pattern_Yearly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_CheckedChanged);
            // 
            // rbRec_Pattern_Monthly
            // 
            this.rbRec_Pattern_Monthly.AutoSize = true;
            this.rbRec_Pattern_Monthly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Monthly.Location = new System.Drawing.Point(170, 16);
            this.rbRec_Pattern_Monthly.Name = "rbRec_Pattern_Monthly";
            this.rbRec_Pattern_Monthly.Size = new System.Drawing.Size(68, 18);
            this.rbRec_Pattern_Monthly.TabIndex = 3;
            this.rbRec_Pattern_Monthly.Text = "Monthly";
            this.rbRec_Pattern_Monthly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_CheckedChanged);
            // 
            // rbRec_Pattern_Daily
            // 
            this.rbRec_Pattern_Daily.AutoSize = true;
            this.rbRec_Pattern_Daily.Checked = true;
            this.rbRec_Pattern_Daily.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Daily.Location = new System.Drawing.Point(30, 16);
            this.rbRec_Pattern_Daily.Name = "rbRec_Pattern_Daily";
            this.rbRec_Pattern_Daily.Size = new System.Drawing.Size(54, 18);
            this.rbRec_Pattern_Daily.TabIndex = 1;
            this.rbRec_Pattern_Daily.TabStop = true;
            this.rbRec_Pattern_Daily.Text = "Daily";
            this.rbRec_Pattern_Daily.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Daily.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_CheckedChanged);
            // 
            // rbRec_Pattern_Weekly
            // 
            this.rbRec_Pattern_Weekly.AutoSize = true;
            this.rbRec_Pattern_Weekly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Weekly.Location = new System.Drawing.Point(93, 16);
            this.rbRec_Pattern_Weekly.Name = "rbRec_Pattern_Weekly";
            this.rbRec_Pattern_Weekly.Size = new System.Drawing.Size(65, 18);
            this.rbRec_Pattern_Weekly.TabIndex = 2;
            this.rbRec_Pattern_Weekly.Text = "Weekly";
            this.rbRec_Pattern_Weekly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Weekly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Weekly_CheckedChanged);
            // 
            // lbl_pnlRecurring_Pattern_BodyTopBrd
            // 
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.Location = new System.Drawing.Point(4, 5);
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.Name = "lbl_pnlRecurring_Pattern_BodyTopBrd";
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlRecurring_Pattern_BodyTopBrd.TabIndex = 9;
            // 
            // lbl_pnlRecurring_Pattern_BodyLeftBrd
            // 
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.Location = new System.Drawing.Point(3, 5);
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.Name = "lbl_pnlRecurring_Pattern_BodyLeftBrd";
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.Size = new System.Drawing.Size(1, 150);
            this.lbl_pnlRecurring_Pattern_BodyLeftBrd.TabIndex = 121;
            // 
            // lbl_pnlRecurring_Pattern_BodyRightBrd
            // 
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.Location = new System.Drawing.Point(433, 5);
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.Name = "lbl_pnlRecurring_Pattern_BodyRightBrd";
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.Size = new System.Drawing.Size(1, 150);
            this.lbl_pnlRecurring_Pattern_BodyRightBrd.TabIndex = 122;
            // 
            // pnlRec_Pattern_Daily
            // 
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyLeftBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyRightBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyBottomBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyTopBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.numRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lblRec_Pattern_Daily_Days);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryWeekday);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Daily.Location = new System.Drawing.Point(19, 46);
            this.pnlRec_Pattern_Daily.Name = "pnlRec_Pattern_Daily";
            this.pnlRec_Pattern_Daily.Size = new System.Drawing.Size(388, 89);
            this.pnlRec_Pattern_Daily.TabIndex = 5;
            // 
            // lbl_pnlRec_Pattern_DailyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_DailyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Name = "lbl_pnlRec_Pattern_DailyLeftBrd";
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlRec_Pattern_DailyLeftBrd.TabIndex = 21;
            // 
            // lbl_pnlRec_Pattern_DailyRightBrd
            // 
            this.lbl_pnlRec_Pattern_DailyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_DailyRightBrd.Location = new System.Drawing.Point(387, 1);
            this.lbl_pnlRec_Pattern_DailyRightBrd.Name = "lbl_pnlRec_Pattern_DailyRightBrd";
            this.lbl_pnlRec_Pattern_DailyRightBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlRec_Pattern_DailyRightBrd.TabIndex = 20;
            // 
            // lbl_pnlRec_Pattern_DailyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_DailyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Location = new System.Drawing.Point(0, 88);
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Name = "lbl_pnlRec_Pattern_DailyBottomBrd";
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Size = new System.Drawing.Size(388, 1);
            this.lbl_pnlRec_Pattern_DailyBottomBrd.TabIndex = 19;
            // 
            // lbl_pnlRec_Pattern_DailyTopBrd
            // 
            this.lbl_pnlRec_Pattern_DailyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_DailyTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_DailyTopBrd.Name = "lbl_pnlRec_Pattern_DailyTopBrd";
            this.lbl_pnlRec_Pattern_DailyTopBrd.Size = new System.Drawing.Size(388, 1);
            this.lbl_pnlRec_Pattern_DailyTopBrd.TabIndex = 18;
            // 
            // numRec_Pattern_Daily_EveryDay
            // 
            this.numRec_Pattern_Daily_EveryDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(72, 17);
            this.numRec_Pattern_Daily_EveryDay.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numRec_Pattern_Daily_EveryDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Daily_EveryDay.Name = "numRec_Pattern_Daily_EveryDay";
            this.numRec_Pattern_Daily_EveryDay.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Daily_EveryDay.TabIndex = 17;
            this.numRec_Pattern_Daily_EveryDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Daily_EveryDay.ValueChanged += new System.EventHandler(this.numRec_Pattern_Daily_EveryDay_ValueChanged);
            // 
            // lblRec_Pattern_Daily_Days
            // 
            this.lblRec_Pattern_Daily_Days.AutoSize = true;
            this.lblRec_Pattern_Daily_Days.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Daily_Days.Location = new System.Drawing.Point(127, 20);
            this.lblRec_Pattern_Daily_Days.Name = "lblRec_Pattern_Daily_Days";
            this.lblRec_Pattern_Daily_Days.Size = new System.Drawing.Size(41, 14);
            this.lblRec_Pattern_Daily_Days.TabIndex = 14;
            this.lblRec_Pattern_Daily_Days.Text = "day(s)";
            // 
            // rbRec_Pattern_Daily_EveryWeekday
            // 
            this.rbRec_Pattern_Daily_EveryWeekday.AutoSize = true;
            this.rbRec_Pattern_Daily_EveryWeekday.Location = new System.Drawing.Point(10, 53);
            this.rbRec_Pattern_Daily_EveryWeekday.Name = "rbRec_Pattern_Daily_EveryWeekday";
            this.rbRec_Pattern_Daily_EveryWeekday.Size = new System.Drawing.Size(108, 18);
            this.rbRec_Pattern_Daily_EveryWeekday.TabIndex = 3;
            this.rbRec_Pattern_Daily_EveryWeekday.Text = "Every weekday";
            this.rbRec_Pattern_Daily_EveryWeekday.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Daily_EveryWeekday.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryWeekday_CheckedChanged);
            // 
            // rbRec_Pattern_Daily_EveryDay
            // 
            this.rbRec_Pattern_Daily_EveryDay.AutoSize = true;
            this.rbRec_Pattern_Daily_EveryDay.Checked = true;
            this.rbRec_Pattern_Daily_EveryDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(10, 18);
            this.rbRec_Pattern_Daily_EveryDay.Name = "rbRec_Pattern_Daily_EveryDay";
            this.rbRec_Pattern_Daily_EveryDay.Size = new System.Drawing.Size(58, 18);
            this.rbRec_Pattern_Daily_EveryDay.TabIndex = 2;
            this.rbRec_Pattern_Daily_EveryDay.TabStop = true;
            this.rbRec_Pattern_Daily_EveryDay.Text = "Every";
            this.rbRec_Pattern_Daily_EveryDay.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Daily_EveryDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryDay_CheckedChanged);
            // 
            // pnlRec_Pattern_Monthly
            // 
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyRightBrd);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyLeftBrd);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyTopBrd);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyBottomBrd);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.numRec_Pattern_Monthly_Criteria_Month);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.numRec_Pattern_Monthly_Day_Month);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.numRec_Pattern_Monthly_Day_Day);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.cmbRec_Pattern_Monthly_Criteria_FstLst);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lblRec_Pattern_Monthly_Criteria_Month);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lblRec_Pattern_Monthly_Criteria_Every);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.rbRec_Pattern_Monthly_Criteria);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lblRec_Pattern_Monthly_Day_Month);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lblRec_Pattern_Monthly_Day_Every);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.rbRec_Pattern_Monthly_Day);
            this.pnlRec_Pattern_Monthly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Monthly.Location = new System.Drawing.Point(19, 46);
            this.pnlRec_Pattern_Monthly.Name = "pnlRec_Pattern_Monthly";
            this.pnlRec_Pattern_Monthly.Size = new System.Drawing.Size(388, 89);
            this.pnlRec_Pattern_Monthly.TabIndex = 7;
            // 
            // lbl_pnlRec_Pattern_MonthlyRightBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Location = new System.Drawing.Point(387, 1);
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Name = "lbl_pnlRec_Pattern_MonthlyRightBrd";
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.TabIndex = 88;
            // 
            // lbl_pnlRec_Pattern_MonthlyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Name = "lbl_pnlRec_Pattern_MonthlyLeftBrd";
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.TabIndex = 87;
            // 
            // lbl_pnlRec_Pattern_MonthlyTopBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Name = "lbl_pnlRec_Pattern_MonthlyTopBrd";
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Size = new System.Drawing.Size(388, 1);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.TabIndex = 86;
            // 
            // lbl_pnlRec_Pattern_MonthlyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Location = new System.Drawing.Point(0, 88);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Name = "lbl_pnlRec_Pattern_MonthlyBottomBrd";
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Size = new System.Drawing.Size(388, 1);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.TabIndex = 85;
            // 
            // numRec_Pattern_Monthly_Criteria_Month
            // 
            this.numRec_Pattern_Monthly_Criteria_Month.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(283, 52);
            this.numRec_Pattern_Monthly_Criteria_Month.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Criteria_Month.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Criteria_Month.Name = "numRec_Pattern_Monthly_Criteria_Month";
            this.numRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(41, 21);
            this.numRec_Pattern_Monthly_Criteria_Month.TabIndex = 84;
            this.numRec_Pattern_Monthly_Criteria_Month.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Criteria_Month.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Criteria_Month_ValueChanged);
            // 
            // numRec_Pattern_Monthly_Day_Month
            // 
            this.numRec_Pattern_Monthly_Day_Month.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(175, 15);
            this.numRec_Pattern_Monthly_Day_Month.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Month.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Month.Name = "numRec_Pattern_Monthly_Day_Month";
            this.numRec_Pattern_Monthly_Day_Month.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Monthly_Day_Month.TabIndex = 83;
            this.numRec_Pattern_Monthly_Day_Month.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Month.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Day_Month_ValueChanged);
            // 
            // numRec_Pattern_Monthly_Day_Day
            // 
            this.numRec_Pattern_Monthly_Day_Day.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Day_Day.Location = new System.Drawing.Point(61, 15);
            this.numRec_Pattern_Monthly_Day_Day.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Day.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Day.Name = "numRec_Pattern_Monthly_Day_Day";
            this.numRec_Pattern_Monthly_Day_Day.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Monthly_Day_Day.TabIndex = 82;
            this.numRec_Pattern_Monthly_Day_Day.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Monthly_Day_Day.ValueChanged += new System.EventHandler(this.numRec_Pattern_Monthly_Day_Day_ValueChanged);
            // 
            // cmbRec_Pattern_Monthly_Criteria_DayWeekday
            // 
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.FormattingEnabled = true;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Location = new System.Drawing.Point(147, 51);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Monthly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Size = new System.Drawing.Size(75, 22);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.TabIndex = 23;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Monthly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Location = new System.Drawing.Point(61, 51);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Name = "cmbRec_Pattern_Monthly_Criteria_FstLst";
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Size = new System.Drawing.Size(80, 22);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.TabIndex = 22;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Monthly_Criteria_Month
            // 
            this.lblRec_Pattern_Monthly_Criteria_Month.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Month.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(327, 55);
            this.lblRec_Pattern_Monthly_Criteria_Month.Name = "lblRec_Pattern_Monthly_Criteria_Month";
            this.lblRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Criteria_Month.TabIndex = 21;
            this.lblRec_Pattern_Monthly_Criteria_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Criteria_Every
            // 
            this.lblRec_Pattern_Monthly_Criteria_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Every.Location = new System.Drawing.Point(226, 55);
            this.lblRec_Pattern_Monthly_Criteria_Every.Name = "lblRec_Pattern_Monthly_Criteria_Every";
            this.lblRec_Pattern_Monthly_Criteria_Every.Size = new System.Drawing.Size(56, 14);
            this.lblRec_Pattern_Monthly_Criteria_Every.TabIndex = 19;
            this.lblRec_Pattern_Monthly_Criteria_Every.Text = "of every ";
            // 
            // rbRec_Pattern_Monthly_Criteria
            // 
            this.rbRec_Pattern_Monthly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Monthly_Criteria.Location = new System.Drawing.Point(8, 53);
            this.rbRec_Pattern_Monthly_Criteria.Name = "rbRec_Pattern_Monthly_Criteria";
            this.rbRec_Pattern_Monthly_Criteria.Size = new System.Drawing.Size(47, 18);
            this.rbRec_Pattern_Monthly_Criteria.TabIndex = 17;
            this.rbRec_Pattern_Monthly_Criteria.Text = "The";
            this.rbRec_Pattern_Monthly_Criteria.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Criteria_CheckedChanged);
            // 
            // lblRec_Pattern_Monthly_Day_Month
            // 
            this.lblRec_Pattern_Monthly_Day_Month.AutoSize = true;
            this.lblRec_Pattern_Monthly_Day_Month.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(229, 18);
            this.lblRec_Pattern_Monthly_Day_Month.Name = "lblRec_Pattern_Monthly_Day_Month";
            this.lblRec_Pattern_Monthly_Day_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Day_Month.TabIndex = 16;
            this.lblRec_Pattern_Monthly_Day_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Day_Every
            // 
            this.lblRec_Pattern_Monthly_Day_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Day_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Day_Every.Location = new System.Drawing.Point(114, 18);
            this.lblRec_Pattern_Monthly_Day_Every.Name = "lblRec_Pattern_Monthly_Day_Every";
            this.lblRec_Pattern_Monthly_Day_Every.Size = new System.Drawing.Size(56, 14);
            this.lblRec_Pattern_Monthly_Day_Every.TabIndex = 14;
            this.lblRec_Pattern_Monthly_Day_Every.Text = "of every ";
            // 
            // rbRec_Pattern_Monthly_Day
            // 
            this.rbRec_Pattern_Monthly_Day.AutoSize = true;
            this.rbRec_Pattern_Monthly_Day.Checked = true;
            this.rbRec_Pattern_Monthly_Day.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Monthly_Day.Location = new System.Drawing.Point(8, 16);
            this.rbRec_Pattern_Monthly_Day.Name = "rbRec_Pattern_Monthly_Day";
            this.rbRec_Pattern_Monthly_Day.Size = new System.Drawing.Size(45, 18);
            this.rbRec_Pattern_Monthly_Day.TabIndex = 2;
            this.rbRec_Pattern_Monthly_Day.TabStop = true;
            this.rbRec_Pattern_Monthly_Day.Text = "Day";
            this.rbRec_Pattern_Monthly_Day.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly_Day.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Day_CheckedChanged);
            // 
            // pnlRec_Pattern_Yearly
            // 
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyTopBrd);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyBottomBrd);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyRightBrd);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyLeftBrd);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.numRec_Pattern_Yearly_Every_MonthDay);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_Month);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Every_Month);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_DayWeekday);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_FstLst);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lblRec_Pattern_Yearly_Criteria_Of);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.rbRec_Pattern_Yearly_Criteria);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.rbRec_Pattern_Yearly_EveryMonthDay);
            this.pnlRec_Pattern_Yearly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Yearly.Location = new System.Drawing.Point(19, 46);
            this.pnlRec_Pattern_Yearly.Name = "pnlRec_Pattern_Yearly";
            this.pnlRec_Pattern_Yearly.Size = new System.Drawing.Size(388, 89);
            this.pnlRec_Pattern_Yearly.TabIndex = 8;
            // 
            // lbl_pnlRec_Pattern_YearlyTopBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Name = "lbl_pnlRec_Pattern_YearlyTopBrd";
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Size = new System.Drawing.Size(386, 1);
            this.lbl_pnlRec_Pattern_YearlyTopBrd.TabIndex = 85;
            // 
            // lbl_pnlRec_Pattern_YearlyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.Location = new System.Drawing.Point(1, 88);
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.Name = "lbl_pnlRec_Pattern_YearlyBottomBrd";
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.Size = new System.Drawing.Size(386, 1);
            this.lbl_pnlRec_Pattern_YearlyBottomBrd.TabIndex = 84;
            // 
            // lbl_pnlRec_Pattern_YearlyRightBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Location = new System.Drawing.Point(387, 0);
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Name = "lbl_pnlRec_Pattern_YearlyRightBrd";
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Size = new System.Drawing.Size(1, 89);
            this.lbl_pnlRec_Pattern_YearlyRightBrd.TabIndex = 83;
            // 
            // lbl_pnlRec_Pattern_YearlyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.Name = "lbl_pnlRec_Pattern_YearlyLeftBrd";
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.Size = new System.Drawing.Size(1, 89);
            this.lbl_pnlRec_Pattern_YearlyLeftBrd.TabIndex = 82;
            // 
            // numRec_Pattern_Yearly_Every_MonthDay
            // 
            this.numRec_Pattern_Yearly_Every_MonthDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Yearly_Every_MonthDay.Location = new System.Drawing.Point(173, 15);
            this.numRec_Pattern_Yearly_Every_MonthDay.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numRec_Pattern_Yearly_Every_MonthDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Yearly_Every_MonthDay.Name = "numRec_Pattern_Yearly_Every_MonthDay";
            this.numRec_Pattern_Yearly_Every_MonthDay.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Yearly_Every_MonthDay.TabIndex = 81;
            this.numRec_Pattern_Yearly_Every_MonthDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Yearly_Every_MonthDay.ValueChanged += new System.EventHandler(this.numRec_Pattern_Yearly_Every_MonthDay_ValueChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_Month
            // 
            this.cmbRec_Pattern_Yearly_Criteria_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_Month.Location = new System.Drawing.Point(293, 52);
            this.cmbRec_Pattern_Yearly_Criteria_Month.Name = "cmbRec_Pattern_Yearly_Criteria_Month";
            this.cmbRec_Pattern_Yearly_Criteria_Month.Size = new System.Drawing.Size(86, 22);
            this.cmbRec_Pattern_Yearly_Criteria_Month.TabIndex = 25;
            this.cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Every_Month
            // 
            this.cmbRec_Pattern_Yearly_Every_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Every_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Every_Month.Location = new System.Drawing.Point(71, 14);
            this.cmbRec_Pattern_Yearly_Every_Month.Name = "cmbRec_Pattern_Yearly_Every_Month";
            this.cmbRec_Pattern_Yearly_Every_Month.Size = new System.Drawing.Size(94, 22);
            this.cmbRec_Pattern_Yearly_Every_Month.TabIndex = 24;
            this.cmbRec_Pattern_Yearly_Every_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_DayWeekday
            // 
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Location = new System.Drawing.Point(173, 52);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Yearly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Size = new System.Drawing.Size(91, 22);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.TabIndex = 23;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Location = new System.Drawing.Point(71, 52);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Name = "cmbRec_Pattern_Yearly_Criteria_FstLst";
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Size = new System.Drawing.Size(94, 22);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.TabIndex = 22;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Yearly_Criteria_Of
            // 
            this.lblRec_Pattern_Yearly_Criteria_Of.AutoSize = true;
            this.lblRec_Pattern_Yearly_Criteria_Of.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Yearly_Criteria_Of.Location = new System.Drawing.Point(271, 56);
            this.lblRec_Pattern_Yearly_Criteria_Of.Name = "lblRec_Pattern_Yearly_Criteria_Of";
            this.lblRec_Pattern_Yearly_Criteria_Of.Size = new System.Drawing.Size(18, 14);
            this.lblRec_Pattern_Yearly_Criteria_Of.TabIndex = 19;
            this.lblRec_Pattern_Yearly_Criteria_Of.Text = "of";
            // 
            // rbRec_Pattern_Yearly_Criteria
            // 
            this.rbRec_Pattern_Yearly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Yearly_Criteria.Location = new System.Drawing.Point(9, 54);
            this.rbRec_Pattern_Yearly_Criteria.Name = "rbRec_Pattern_Yearly_Criteria";
            this.rbRec_Pattern_Yearly_Criteria.Size = new System.Drawing.Size(47, 18);
            this.rbRec_Pattern_Yearly_Criteria.TabIndex = 17;
            this.rbRec_Pattern_Yearly_Criteria.Text = "The";
            this.rbRec_Pattern_Yearly_Criteria.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_Criteria_CheckedChanged);
            // 
            // rbRec_Pattern_Yearly_EveryMonthDay
            // 
            this.rbRec_Pattern_Yearly_EveryMonthDay.AutoSize = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Location = new System.Drawing.Point(9, 16);
            this.rbRec_Pattern_Yearly_EveryMonthDay.Name = "rbRec_Pattern_Yearly_EveryMonthDay";
            this.rbRec_Pattern_Yearly_EveryMonthDay.Size = new System.Drawing.Size(55, 18);
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabIndex = 2;
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabStop = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Text = "Every";
            this.rbRec_Pattern_Yearly_EveryMonthDay.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged);
            // 
            // pnlRec_Pattern_Weekly
            // 
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lbl_pnlRec_Pattern_WeeklyBottomBrd);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lbl_pnlRec_Pattern_WeeklyTopBrd);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lbl_pnlRec_Pattern_WeeklyRightBrd);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lbl_pnlRec_Pattern_WeeklyLeftBrd);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lblRec_Pattern_Weekly_WeekOn);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.lblRec_Pattern_Weekly_RecurEvery);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Saturday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Friday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Sunday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Tuesday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Wednesday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Thursday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.ChkRec_Pattern_Weekly_Monday);
            this.pnlRec_Pattern_Weekly.Controls.Add(this.numRec_Pattern_Weekly_WeekOn);
            this.pnlRec_Pattern_Weekly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Weekly.Location = new System.Drawing.Point(19, 46);
            this.pnlRec_Pattern_Weekly.Name = "pnlRec_Pattern_Weekly";
            this.pnlRec_Pattern_Weekly.Size = new System.Drawing.Size(388, 89);
            this.pnlRec_Pattern_Weekly.TabIndex = 6;
            // 
            // lbl_pnlRec_Pattern_WeeklyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Location = new System.Drawing.Point(1, 88);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Name = "lbl_pnlRec_Pattern_WeeklyBottomBrd";
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Size = new System.Drawing.Size(386, 1);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.TabIndex = 88;
            // 
            // lbl_pnlRec_Pattern_WeeklyTopBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Name = "lbl_pnlRec_Pattern_WeeklyTopBrd";
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Size = new System.Drawing.Size(386, 1);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.TabIndex = 87;
            // 
            // lbl_pnlRec_Pattern_WeeklyRightBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Location = new System.Drawing.Point(387, 0);
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Name = "lbl_pnlRec_Pattern_WeeklyRightBrd";
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Size = new System.Drawing.Size(1, 89);
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.TabIndex = 84;
            // 
            // lbl_pnlRec_Pattern_WeeklyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Name = "lbl_pnlRec_Pattern_WeeklyLeftBrd";
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Size = new System.Drawing.Size(1, 89);
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.TabIndex = 83;
            // 
            // lblRec_Pattern_Weekly_WeekOn
            // 
            this.lblRec_Pattern_Weekly_WeekOn.AutoSize = true;
            this.lblRec_Pattern_Weekly_WeekOn.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Weekly_WeekOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(147, 10);
            this.lblRec_Pattern_Weekly_WeekOn.Name = "lblRec_Pattern_Weekly_WeekOn";
            this.lblRec_Pattern_Weekly_WeekOn.Size = new System.Drawing.Size(70, 14);
            this.lblRec_Pattern_Weekly_WeekOn.TabIndex = 15;
            this.lblRec_Pattern_Weekly_WeekOn.Text = "week(s) on";
            // 
            // lblRec_Pattern_Weekly_RecurEvery
            // 
            this.lblRec_Pattern_Weekly_RecurEvery.AutoSize = true;
            this.lblRec_Pattern_Weekly_RecurEvery.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Weekly_RecurEvery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Weekly_RecurEvery.Location = new System.Drawing.Point(10, 10);
            this.lblRec_Pattern_Weekly_RecurEvery.Name = "lblRec_Pattern_Weekly_RecurEvery";
            this.lblRec_Pattern_Weekly_RecurEvery.Size = new System.Drawing.Size(72, 14);
            this.lblRec_Pattern_Weekly_RecurEvery.TabIndex = 14;
            this.lblRec_Pattern_Weekly_RecurEvery.Text = "Recur every";
            // 
            // ChkRec_Pattern_Weekly_Saturday
            // 
            this.ChkRec_Pattern_Weekly_Saturday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Saturday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Saturday.Location = new System.Drawing.Point(176, 64);
            this.ChkRec_Pattern_Weekly_Saturday.Name = "ChkRec_Pattern_Weekly_Saturday";
            this.ChkRec_Pattern_Weekly_Saturday.Size = new System.Drawing.Size(74, 18);
            this.ChkRec_Pattern_Weekly_Saturday.TabIndex = 79;
            this.ChkRec_Pattern_Weekly_Saturday.Text = "Saturday";
            this.ChkRec_Pattern_Weekly_Saturday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Saturday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Saturday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Friday
            // 
            this.ChkRec_Pattern_Weekly_Friday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Friday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Friday.Location = new System.Drawing.Point(93, 64);
            this.ChkRec_Pattern_Weekly_Friday.Name = "ChkRec_Pattern_Weekly_Friday";
            this.ChkRec_Pattern_Weekly_Friday.Size = new System.Drawing.Size(57, 18);
            this.ChkRec_Pattern_Weekly_Friday.TabIndex = 78;
            this.ChkRec_Pattern_Weekly_Friday.Text = "Friday";
            this.ChkRec_Pattern_Weekly_Friday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Friday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Friday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Sunday
            // 
            this.ChkRec_Pattern_Weekly_Sunday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Sunday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Sunday.Location = new System.Drawing.Point(10, 38);
            this.ChkRec_Pattern_Weekly_Sunday.Name = "ChkRec_Pattern_Weekly_Sunday";
            this.ChkRec_Pattern_Weekly_Sunday.Size = new System.Drawing.Size(66, 18);
            this.ChkRec_Pattern_Weekly_Sunday.TabIndex = 73;
            this.ChkRec_Pattern_Weekly_Sunday.Text = "Sunday";
            this.ChkRec_Pattern_Weekly_Sunday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Sunday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Sunday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Tuesday
            // 
            this.ChkRec_Pattern_Weekly_Tuesday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Tuesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Tuesday.Location = new System.Drawing.Point(176, 38);
            this.ChkRec_Pattern_Weekly_Tuesday.Name = "ChkRec_Pattern_Weekly_Tuesday";
            this.ChkRec_Pattern_Weekly_Tuesday.Size = new System.Drawing.Size(72, 18);
            this.ChkRec_Pattern_Weekly_Tuesday.TabIndex = 77;
            this.ChkRec_Pattern_Weekly_Tuesday.Text = "Tuesday";
            this.ChkRec_Pattern_Weekly_Tuesday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Tuesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Tuesday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Wednesday
            // 
            this.ChkRec_Pattern_Weekly_Wednesday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Wednesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Wednesday.Location = new System.Drawing.Point(264, 38);
            this.ChkRec_Pattern_Weekly_Wednesday.Name = "ChkRec_Pattern_Weekly_Wednesday";
            this.ChkRec_Pattern_Weekly_Wednesday.Size = new System.Drawing.Size(90, 18);
            this.ChkRec_Pattern_Weekly_Wednesday.TabIndex = 74;
            this.ChkRec_Pattern_Weekly_Wednesday.Text = "Wednesday";
            this.ChkRec_Pattern_Weekly_Wednesday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Wednesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Wednesday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Thursday
            // 
            this.ChkRec_Pattern_Weekly_Thursday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Thursday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Thursday.Location = new System.Drawing.Point(10, 64);
            this.ChkRec_Pattern_Weekly_Thursday.Name = "ChkRec_Pattern_Weekly_Thursday";
            this.ChkRec_Pattern_Weekly_Thursday.Size = new System.Drawing.Size(76, 18);
            this.ChkRec_Pattern_Weekly_Thursday.TabIndex = 76;
            this.ChkRec_Pattern_Weekly_Thursday.Text = "Thursday";
            this.ChkRec_Pattern_Weekly_Thursday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Thursday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Thursday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Monday
            // 
            this.ChkRec_Pattern_Weekly_Monday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Monday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Monday.Location = new System.Drawing.Point(93, 38);
            this.ChkRec_Pattern_Weekly_Monday.Name = "ChkRec_Pattern_Weekly_Monday";
            this.ChkRec_Pattern_Weekly_Monday.Size = new System.Drawing.Size(68, 18);
            this.ChkRec_Pattern_Weekly_Monday.TabIndex = 75;
            this.ChkRec_Pattern_Weekly_Monday.Text = "Monday";
            this.ChkRec_Pattern_Weekly_Monday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Monday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Monday_CheckedChanged);
            // 
            // numRec_Pattern_Weekly_WeekOn
            // 
            this.numRec_Pattern_Weekly_WeekOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Weekly_WeekOn.ForeColor = System.Drawing.Color.Black;
            this.numRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(93, 6);
            this.numRec_Pattern_Weekly_WeekOn.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numRec_Pattern_Weekly_WeekOn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Weekly_WeekOn.Name = "numRec_Pattern_Weekly_WeekOn";
            this.numRec_Pattern_Weekly_WeekOn.Size = new System.Drawing.Size(48, 22);
            this.numRec_Pattern_Weekly_WeekOn.TabIndex = 80;
            this.numRec_Pattern_Weekly_WeekOn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Weekly_WeekOn.ValueChanged += new System.EventHandler(this.numRec_Pattern_Weekly_WeekOn_ValueChanged);
            // 
            // pnlProgressbar
            // 
            this.pnlProgressbar.Controls.Add(this.lblSearchingMessage);
            this.pnlProgressbar.Controls.Add(this.progbarSearch);
            this.pnlProgressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlProgressbar.Location = new System.Drawing.Point(0, 763);
            this.pnlProgressbar.Name = "pnlProgressbar";
            this.pnlProgressbar.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlProgressbar.Size = new System.Drawing.Size(437, 44);
            this.pnlProgressbar.TabIndex = 135;
            this.pnlProgressbar.Visible = false;
            // 
            // lblSearchingMessage
            // 
            this.lblSearchingMessage.AutoSize = true;
            this.lblSearchingMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchingMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchingMessage.Location = new System.Drawing.Point(10, 3);
            this.lblSearchingMessage.Name = "lblSearchingMessage";
            this.lblSearchingMessage.Size = new System.Drawing.Size(0, 14);
            this.lblSearchingMessage.TabIndex = 135;
            // 
            // progbarSearch
            // 
            this.progbarSearch.Location = new System.Drawing.Point(7, 19);
            this.progbarSearch.Name = "progbarSearch";
            this.progbarSearch.Size = new System.Drawing.Size(423, 17);
            this.progbarSearch.TabIndex = 134;
            // 
            // pnlRecurring_Pattern_Header
            // 
            this.pnlRecurring_Pattern_Header.BackColor = System.Drawing.Color.Transparent;
            this.pnlRecurring_Pattern_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Pattern_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lblRecurring_Pattern_Header);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.chkApplyRecPattern);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.btn_Recurrencepattern_UP);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.btn_Recurrencepattern_Down);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderRightBrd);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderLeftSpace);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderTopBrd);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderBottomBrd);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderLeftBrd);
            this.pnlRecurring_Pattern_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Pattern_Header.ForeColor = System.Drawing.Color.White;
            this.pnlRecurring_Pattern_Header.Location = new System.Drawing.Point(0, 691);
            this.pnlRecurring_Pattern_Header.Name = "pnlRecurring_Pattern_Header";
            this.pnlRecurring_Pattern_Header.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlRecurring_Pattern_Header.Size = new System.Drawing.Size(437, 26);
            this.pnlRecurring_Pattern_Header.TabIndex = 5;
            // 
            // lblRecurring_Pattern_Header
            // 
            this.lblRecurring_Pattern_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Pattern_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Pattern_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Pattern_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Pattern_Header.Location = new System.Drawing.Point(24, 4);
            this.lblRecurring_Pattern_Header.Name = "lblRecurring_Pattern_Header";
            this.lblRecurring_Pattern_Header.Size = new System.Drawing.Size(353, 21);
            this.lblRecurring_Pattern_Header.TabIndex = 0;
            this.lblRecurring_Pattern_Header.Text = "  Recurrence Pattern";
            this.lblRecurring_Pattern_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkApplyRecPattern
            // 
            this.chkApplyRecPattern.AutoSize = true;
            this.chkApplyRecPattern.BackColor = System.Drawing.Color.Transparent;
            this.chkApplyRecPattern.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkApplyRecPattern.Location = new System.Drawing.Point(9, 4);
            this.chkApplyRecPattern.Name = "chkApplyRecPattern";
            this.chkApplyRecPattern.Size = new System.Drawing.Size(15, 21);
            this.chkApplyRecPattern.TabIndex = 104;
            this.chkApplyRecPattern.UseVisualStyleBackColor = false;
            this.chkApplyRecPattern.CheckedChanged += new System.EventHandler(this.chkApplyRecPattern_CheckedChanged);
            // 
            // btn_Recurrencepattern_UP
            // 
            this.btn_Recurrencepattern_UP.BackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_UP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Recurrencepattern_UP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Recurrencepattern_UP.FlatAppearance.BorderSize = 0;
            this.btn_Recurrencepattern_UP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_UP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_UP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recurrencepattern_UP.Location = new System.Drawing.Point(377, 4);
            this.btn_Recurrencepattern_UP.Name = "btn_Recurrencepattern_UP";
            this.btn_Recurrencepattern_UP.Size = new System.Drawing.Size(28, 21);
            this.btn_Recurrencepattern_UP.TabIndex = 125;
            this.btn_Recurrencepattern_UP.UseVisualStyleBackColor = false;
            this.btn_Recurrencepattern_UP.Visible = false;
            this.btn_Recurrencepattern_UP.Click += new System.EventHandler(this.btn_Recurrencepattern_UP_Click);
            this.btn_Recurrencepattern_UP.MouseLeave += new System.EventHandler(this.btn_Recurrencepattern_UP_MouseLeave);
            this.btn_Recurrencepattern_UP.MouseHover += new System.EventHandler(this.btn_Recurrencepattern_UP_MouseHover);
            // 
            // btn_Recurrencepattern_Down
            // 
            this.btn_Recurrencepattern_Down.BackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Recurrencepattern_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Recurrencepattern_Down.FlatAppearance.BorderSize = 0;
            this.btn_Recurrencepattern_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Recurrencepattern_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Recurrencepattern_Down.Location = new System.Drawing.Point(405, 4);
            this.btn_Recurrencepattern_Down.Name = "btn_Recurrencepattern_Down";
            this.btn_Recurrencepattern_Down.Size = new System.Drawing.Size(28, 21);
            this.btn_Recurrencepattern_Down.TabIndex = 124;
            this.btn_Recurrencepattern_Down.UseVisualStyleBackColor = false;
            this.btn_Recurrencepattern_Down.Click += new System.EventHandler(this.btn_Recurrencepattern_Down_Click);
            this.btn_Recurrencepattern_Down.MouseLeave += new System.EventHandler(this.btn_Recurrencepattern_Down_MouseLeave);
            this.btn_Recurrencepattern_Down.MouseHover += new System.EventHandler(this.btn_Recurrencepattern_Down_MouseHover);
            // 
            // lbl_pnlRecurring_Pattern_HeaderRightBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.Location = new System.Drawing.Point(433, 4);
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.Name = "lbl_pnlRecurring_Pattern_HeaderRightBrd";
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_pnlRecurring_Pattern_HeaderRightBrd.TabIndex = 123;
            // 
            // lbl_pnlRecurring_Pattern_HeaderLeftSpace
            // 
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.Location = new System.Drawing.Point(4, 4);
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.Name = "lbl_pnlRecurring_Pattern_HeaderLeftSpace";
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.Size = new System.Drawing.Size(5, 21);
            this.lbl_pnlRecurring_Pattern_HeaderLeftSpace.TabIndex = 105;
            // 
            // lbl_pnlRecurring_Pattern_HeaderTopBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.Name = "lbl_pnlRecurring_Pattern_HeaderTopBrd";
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.Size = new System.Drawing.Size(430, 1);
            this.lbl_pnlRecurring_Pattern_HeaderTopBrd.TabIndex = 106;
            // 
            // lbl_pnlRecurring_Pattern_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Location = new System.Drawing.Point(4, 25);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Name = "lbl_pnlRecurring_Pattern_HeaderBottomBrd";
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Size = new System.Drawing.Size(430, 1);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.TabIndex = 107;
            // 
            // lbl_pnlRecurring_Pattern_HeaderLeftBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.Name = "lbl_pnlRecurring_Pattern_HeaderLeftBrd";
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlRecurring_Pattern_HeaderLeftBrd.TabIndex = 122;
            // 
            // pnl_AdvanceSearch_Body
            // 
            this.pnl_AdvanceSearch_Body.Controls.Add(this.rbResourceFilter);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.rbProblemTypeFilter);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_pnl_AdvanceSearch_BodyBottomBrd);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_pnl_AdvanceSearch_BodyRightBrd);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.cmbLocation);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_pnl_AdvanceSearch_BodyLeftBrd);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.cmbDepartment);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_pnl_AdvanceSearch_BodyTopBrd);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.cmbResources);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.cmbProcedures);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_Department);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.btn_BrowseProcedure);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.btn_ClearProcedure);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.btn_ClearResource);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.lbl_Location);
            this.pnl_AdvanceSearch_Body.Controls.Add(this.btn_BrowseResource);
            this.pnl_AdvanceSearch_Body.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_AdvanceSearch_Body.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_AdvanceSearch_Body.Location = new System.Drawing.Point(0, 545);
            this.pnl_AdvanceSearch_Body.Name = "pnl_AdvanceSearch_Body";
            this.pnl_AdvanceSearch_Body.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnl_AdvanceSearch_Body.Size = new System.Drawing.Size(437, 146);
            this.pnl_AdvanceSearch_Body.TabIndex = 3;
            this.pnl_AdvanceSearch_Body.Visible = false;
            // 
            // rbResourceFilter
            // 
            this.rbResourceFilter.AutoSize = true;
            this.rbResourceFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbResourceFilter.Location = new System.Drawing.Point(43, 50);
            this.rbResourceFilter.Name = "rbResourceFilter";
            this.rbResourceFilter.Size = new System.Drawing.Size(83, 18);
            this.rbResourceFilter.TabIndex = 125;
            this.rbResourceFilter.Text = "Resource :";
            this.rbResourceFilter.UseVisualStyleBackColor = true;
            this.rbResourceFilter.CheckedChanged += new System.EventHandler(this.rbResourceFilter_CheckedChanged);
            // 
            // rbProblemTypeFilter
            // 
            this.rbProblemTypeFilter.AutoSize = true;
            this.rbProblemTypeFilter.Checked = true;
            this.rbProblemTypeFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProblemTypeFilter.Location = new System.Drawing.Point(10, 19);
            this.rbProblemTypeFilter.Name = "rbProblemTypeFilter";
            this.rbProblemTypeFilter.Size = new System.Drawing.Size(116, 18);
            this.rbProblemTypeFilter.TabIndex = 124;
            this.rbProblemTypeFilter.TabStop = true;
            this.rbProblemTypeFilter.Text = "Problem Type :";
            this.rbProblemTypeFilter.UseVisualStyleBackColor = true;
            this.rbProblemTypeFilter.CheckedChanged += new System.EventHandler(this.rbProblemTypeFilter_CheckedChanged);
            // 
            // lbl_pnl_AdvanceSearch_BodyBottomBrd
            // 
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.Location = new System.Drawing.Point(4, 140);
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.Name = "lbl_pnl_AdvanceSearch_BodyBottomBrd";
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnl_AdvanceSearch_BodyBottomBrd.TabIndex = 124;
            // 
            // lbl_pnl_AdvanceSearch_BodyRightBrd
            // 
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.Location = new System.Drawing.Point(433, 6);
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.Name = "lbl_pnl_AdvanceSearch_BodyRightBrd";
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.Size = new System.Drawing.Size(1, 135);
            this.lbl_pnl_AdvanceSearch_BodyRightBrd.TabIndex = 123;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(136, 77);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(211, 22);
            this.cmbLocation.TabIndex = 5;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // lbl_pnl_AdvanceSearch_BodyLeftBrd
            // 
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.Location = new System.Drawing.Point(3, 6);
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.Name = "lbl_pnl_AdvanceSearch_BodyLeftBrd";
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.Size = new System.Drawing.Size(1, 135);
            this.lbl_pnl_AdvanceSearch_BodyLeftBrd.TabIndex = 122;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(136, 106);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(211, 22);
            this.cmbDepartment.TabIndex = 7;
            // 
            // lbl_pnl_AdvanceSearch_BodyTopBrd
            // 
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.Location = new System.Drawing.Point(3, 5);
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.Name = "lbl_pnl_AdvanceSearch_BodyTopBrd";
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.Size = new System.Drawing.Size(431, 1);
            this.lbl_pnl_AdvanceSearch_BodyTopBrd.TabIndex = 97;
            // 
            // cmbResources
            // 
            this.cmbResources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResources.FormattingEnabled = true;
            this.cmbResources.Location = new System.Drawing.Point(136, 46);
            this.cmbResources.Name = "cmbResources";
            this.cmbResources.Size = new System.Drawing.Size(214, 22);
            this.cmbResources.TabIndex = 27;
            // 
            // cmbProcedures
            // 
            this.cmbProcedures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcedures.FormattingEnabled = true;
            this.cmbProcedures.Location = new System.Drawing.Point(136, 17);
            this.cmbProcedures.Name = "cmbProcedures";
            this.cmbProcedures.Size = new System.Drawing.Size(214, 22);
            this.cmbProcedures.TabIndex = 26;
            // 
            // lbl_Department
            // 
            this.lbl_Department.AutoSize = true;
            this.lbl_Department.Location = new System.Drawing.Point(45, 112);
            this.lbl_Department.Name = "lbl_Department";
            this.lbl_Department.Size = new System.Drawing.Size(81, 14);
            this.lbl_Department.TabIndex = 6;
            this.lbl_Department.Text = "Department :";
            // 
            // btn_BrowseProcedure
            // 
            this.btn_BrowseProcedure.BackColor = System.Drawing.Color.Transparent;
            this.btn_BrowseProcedure.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowseProcedure.BackgroundImage")));
            this.btn_BrowseProcedure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowseProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseProcedure.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowseProcedure.Image")));
            this.btn_BrowseProcedure.Location = new System.Drawing.Point(355, 17);
            this.btn_BrowseProcedure.Name = "btn_BrowseProcedure";
            this.btn_BrowseProcedure.Size = new System.Drawing.Size(21, 21);
            this.btn_BrowseProcedure.TabIndex = 36;
            this.btn_BrowseProcedure.UseVisualStyleBackColor = false;
            this.btn_BrowseProcedure.Click += new System.EventHandler(this.btn_BrowseProcedure_Click);
            this.btn_BrowseProcedure.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_BrowseProcedure.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_ClearProcedure
            // 
            this.btn_ClearProcedure.BackColor = System.Drawing.Color.Transparent;
            this.btn_ClearProcedure.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearProcedure.BackgroundImage")));
            this.btn_ClearProcedure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearProcedure.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearProcedure.Image")));
            this.btn_ClearProcedure.Location = new System.Drawing.Point(382, 17);
            this.btn_ClearProcedure.Name = "btn_ClearProcedure";
            this.btn_ClearProcedure.Size = new System.Drawing.Size(21, 21);
            this.btn_ClearProcedure.TabIndex = 39;
            this.btn_ClearProcedure.UseVisualStyleBackColor = false;
            this.btn_ClearProcedure.Click += new System.EventHandler(this.btn_ClearProcedure_Click);
            this.btn_ClearProcedure.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_ClearProcedure.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_ClearResource
            // 
            this.btn_ClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btn_ClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearResource.BackgroundImage")));
            this.btn_ClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearResource.Image")));
            this.btn_ClearResource.Location = new System.Drawing.Point(382, 46);
            this.btn_ClearResource.Name = "btn_ClearResource";
            this.btn_ClearResource.Size = new System.Drawing.Size(21, 21);
            this.btn_ClearResource.TabIndex = 39;
            this.btn_ClearResource.UseVisualStyleBackColor = false;
            this.btn_ClearResource.Click += new System.EventHandler(this.btn_ClearResource_Click);
            this.btn_ClearResource.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_ClearResource.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lbl_Location
            // 
            this.lbl_Location.AutoSize = true;
            this.lbl_Location.Location = new System.Drawing.Point(65, 81);
            this.lbl_Location.Name = "lbl_Location";
            this.lbl_Location.Size = new System.Drawing.Size(61, 14);
            this.lbl_Location.TabIndex = 4;
            this.lbl_Location.Text = "Location :";
            // 
            // btn_BrowseResource
            // 
            this.btn_BrowseResource.BackColor = System.Drawing.Color.Transparent;
            this.btn_BrowseResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowseResource.BackgroundImage")));
            this.btn_BrowseResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowseResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseResource.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowseResource.Image")));
            this.btn_BrowseResource.Location = new System.Drawing.Point(355, 46);
            this.btn_BrowseResource.Name = "btn_BrowseResource";
            this.btn_BrowseResource.Size = new System.Drawing.Size(21, 21);
            this.btn_BrowseResource.TabIndex = 38;
            this.btn_BrowseResource.UseVisualStyleBackColor = false;
            this.btn_BrowseResource.Click += new System.EventHandler(this.btn_BrowseResource_Click);
            this.btn_BrowseResource.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_BrowseResource.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlAdvanceSearchHeader
            // 
            this.pnlAdvanceSearchHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlAdvanceSearchHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAdvanceSearchHeader.Controls.Add(this.pnlAdvanceSearch_Down);
            this.pnlAdvanceSearchHeader.Controls.Add(this.pnlAdvanceSearch_UP);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lblAdvanceSearch);
            this.pnlAdvanceSearchHeader.Controls.Add(this.chkApplyAdvSearch);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lbl_pnlAdvanceSearchHeader_LeftSpace);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lbl_pnlAdvanceSearchHeaderTopBrd);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lbl_pnlAdvanceSearchHeaderLeftBrd);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lbl_pnlAdvanceSearchHeaderRightBrd);
            this.pnlAdvanceSearchHeader.Controls.Add(this.lbl_pnlAdvanceSearchHeaderBottomBrd);
            this.pnlAdvanceSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdvanceSearchHeader.Location = new System.Drawing.Point(0, 519);
            this.pnlAdvanceSearchHeader.Name = "pnlAdvanceSearchHeader";
            this.pnlAdvanceSearchHeader.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlAdvanceSearchHeader.Size = new System.Drawing.Size(437, 26);
            this.pnlAdvanceSearchHeader.TabIndex = 3;
            this.pnlAdvanceSearchHeader.Visible = false;
            // 
            // pnlAdvanceSearch_Down
            // 
            this.pnlAdvanceSearch_Down.BackColor = System.Drawing.Color.Transparent;
            this.pnlAdvanceSearch_Down.Controls.Add(this.btn_AdvanceSearch_Down);
            this.pnlAdvanceSearch_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAdvanceSearch_Down.Location = new System.Drawing.Point(377, 4);
            this.pnlAdvanceSearch_Down.Name = "pnlAdvanceSearch_Down";
            this.pnlAdvanceSearch_Down.Size = new System.Drawing.Size(28, 21);
            this.pnlAdvanceSearch_Down.TabIndex = 1;
            // 
            // btn_AdvanceSearch_Down
            // 
            this.btn_AdvanceSearch_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AdvanceSearch_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AdvanceSearch_Down.FlatAppearance.BorderSize = 0;
            this.btn_AdvanceSearch_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AdvanceSearch_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AdvanceSearch_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AdvanceSearch_Down.Location = new System.Drawing.Point(0, 0);
            this.btn_AdvanceSearch_Down.Name = "btn_AdvanceSearch_Down";
            this.btn_AdvanceSearch_Down.Size = new System.Drawing.Size(28, 21);
            this.btn_AdvanceSearch_Down.TabIndex = 0;
            this.btn_AdvanceSearch_Down.UseVisualStyleBackColor = true;
            this.btn_AdvanceSearch_Down.Click += new System.EventHandler(this.btn_AdvanceSearch_Down_Click);
            this.btn_AdvanceSearch_Down.MouseLeave += new System.EventHandler(this.btn_AdvanceSearch_Down_MouseLeave);
            this.btn_AdvanceSearch_Down.MouseHover += new System.EventHandler(this.btn_AdvanceSearch_Down_MouseHover);
            // 
            // pnlAdvanceSearch_UP
            // 
            this.pnlAdvanceSearch_UP.BackColor = System.Drawing.Color.Transparent;
            this.pnlAdvanceSearch_UP.Controls.Add(this.btn_AdvanceSearch_UP);
            this.pnlAdvanceSearch_UP.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAdvanceSearch_UP.Location = new System.Drawing.Point(405, 4);
            this.pnlAdvanceSearch_UP.Name = "pnlAdvanceSearch_UP";
            this.pnlAdvanceSearch_UP.Size = new System.Drawing.Size(28, 21);
            this.pnlAdvanceSearch_UP.TabIndex = 0;
            this.pnlAdvanceSearch_UP.Visible = false;
            // 
            // btn_AdvanceSearch_UP
            // 
            this.btn_AdvanceSearch_UP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AdvanceSearch_UP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AdvanceSearch_UP.FlatAppearance.BorderSize = 0;
            this.btn_AdvanceSearch_UP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AdvanceSearch_UP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AdvanceSearch_UP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AdvanceSearch_UP.Location = new System.Drawing.Point(0, 0);
            this.btn_AdvanceSearch_UP.Name = "btn_AdvanceSearch_UP";
            this.btn_AdvanceSearch_UP.Size = new System.Drawing.Size(28, 21);
            this.btn_AdvanceSearch_UP.TabIndex = 0;
            this.btn_AdvanceSearch_UP.UseVisualStyleBackColor = true;
            this.btn_AdvanceSearch_UP.Click += new System.EventHandler(this.btn_AdvanceSearch_UP_Click);
            this.btn_AdvanceSearch_UP.MouseLeave += new System.EventHandler(this.btn_AdvanceSearch_UP_MouseLeave);
            this.btn_AdvanceSearch_UP.MouseHover += new System.EventHandler(this.btn_AdvanceSearch_UP_MouseHover);
            // 
            // lblAdvanceSearch
            // 
            this.lblAdvanceSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblAdvanceSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAdvanceSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvanceSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAdvanceSearch.Location = new System.Drawing.Point(24, 4);
            this.lblAdvanceSearch.Name = "lblAdvanceSearch";
            this.lblAdvanceSearch.Size = new System.Drawing.Size(409, 21);
            this.lblAdvanceSearch.TabIndex = 0;
            this.lblAdvanceSearch.Text = "  Search in Provider Schedule";
            this.lblAdvanceSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkApplyAdvSearch
            // 
            this.chkApplyAdvSearch.AutoSize = true;
            this.chkApplyAdvSearch.BackColor = System.Drawing.Color.Transparent;
            this.chkApplyAdvSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkApplyAdvSearch.Location = new System.Drawing.Point(9, 4);
            this.chkApplyAdvSearch.Name = "chkApplyAdvSearch";
            this.chkApplyAdvSearch.Size = new System.Drawing.Size(15, 21);
            this.chkApplyAdvSearch.TabIndex = 0;
            this.chkApplyAdvSearch.UseVisualStyleBackColor = false;
            this.chkApplyAdvSearch.Visible = false;
            this.chkApplyAdvSearch.CheckedChanged += new System.EventHandler(this.chkApplyAdvSearch_CheckedChanged);
            // 
            // lbl_pnlAdvanceSearchHeader_LeftSpace
            // 
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.Location = new System.Drawing.Point(4, 4);
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.Name = "lbl_pnlAdvanceSearchHeader_LeftSpace";
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.Size = new System.Drawing.Size(5, 21);
            this.lbl_pnlAdvanceSearchHeader_LeftSpace.TabIndex = 104;
            // 
            // lbl_pnlAdvanceSearchHeaderTopBrd
            // 
            this.lbl_pnlAdvanceSearchHeaderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAdvanceSearchHeaderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlAdvanceSearchHeaderTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlAdvanceSearchHeaderTopBrd.Name = "lbl_pnlAdvanceSearchHeaderTopBrd";
            this.lbl_pnlAdvanceSearchHeaderTopBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlAdvanceSearchHeaderTopBrd.TabIndex = 102;
            // 
            // lbl_pnlAdvanceSearchHeaderLeftBrd
            // 
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.Name = "lbl_pnlAdvanceSearchHeaderLeftBrd";
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlAdvanceSearchHeaderLeftBrd.TabIndex = 123;
            // 
            // lbl_pnlAdvanceSearchHeaderRightBrd
            // 
            this.lbl_pnlAdvanceSearchHeaderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAdvanceSearchHeaderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlAdvanceSearchHeaderRightBrd.Location = new System.Drawing.Point(433, 3);
            this.lbl_pnlAdvanceSearchHeaderRightBrd.Name = "lbl_pnlAdvanceSearchHeaderRightBrd";
            this.lbl_pnlAdvanceSearchHeaderRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlAdvanceSearchHeaderRightBrd.TabIndex = 124;
            // 
            // lbl_pnlAdvanceSearchHeaderBottomBrd
            // 
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.Location = new System.Drawing.Point(3, 25);
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.Name = "lbl_pnlAdvanceSearchHeaderBottomBrd";
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.Size = new System.Drawing.Size(431, 1);
            this.lbl_pnlAdvanceSearchHeaderBottomBrd.TabIndex = 125;
            // 
            // pnlAppointmentSearchBody
            // 
            this.pnlAppointmentSearchBody.Controls.Add(this.panel2);
            this.pnlAppointmentSearchBody.Controls.Add(this.panel5);
            this.pnlAppointmentSearchBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppointmentSearchBody.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAppointmentSearchBody.Location = new System.Drawing.Point(0, 289);
            this.pnlAppointmentSearchBody.Name = "pnlAppointmentSearchBody";
            this.pnlAppointmentSearchBody.Size = new System.Drawing.Size(437, 230);
            this.pnlAppointmentSearchBody.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label33);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(437, 193);
            this.panel2.TabIndex = 0;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Location = new System.Drawing.Point(4, 189);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(429, 1);
            this.label33.TabIndex = 132;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(4, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(429, 1);
            this.label32.TabIndex = 131;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(3, 3);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 187);
            this.label31.TabIndex = 130;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(433, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 187);
            this.label30.TabIndex = 129;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.trvAppointmentType);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(431, 187);
            this.panel3.TabIndex = 0;
            // 
            // trvAppointmentType
            // 
            this.trvAppointmentType.BackColor = System.Drawing.Color.White;
            this.trvAppointmentType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvAppointmentType.CheckBoxes = true;
            this.trvAppointmentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAppointmentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvAppointmentType.ForeColor = System.Drawing.Color.Black;
            this.trvAppointmentType.Indent = 20;
            this.trvAppointmentType.ItemHeight = 19;
            this.trvAppointmentType.Location = new System.Drawing.Point(0, 27);
            this.trvAppointmentType.Name = "trvAppointmentType";
            this.trvAppointmentType.ShowLines = false;
            this.trvAppointmentType.ShowPlusMinus = false;
            this.trvAppointmentType.ShowRootLines = false;
            this.trvAppointmentType.Size = new System.Drawing.Size(431, 160);
            this.trvAppointmentType.TabIndex = 1;
            this.trvAppointmentType.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvAppointmentType_AfterCheck);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(0, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(431, 3);
            this.label18.TabIndex = 93;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.btnClearAllAppTypes);
            this.panel4.Controls.Add(this.btnSelectAllAppTypes);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(431, 24);
            this.panel4.TabIndex = 92;
            // 
            // btnClearAllAppTypes
            // 
            this.btnClearAllAppTypes.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllAppTypes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllAppTypes.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearAllAppTypes.FlatAppearance.BorderSize = 0;
            this.btnClearAllAppTypes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearAllAppTypes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearAllAppTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllAppTypes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllAppTypes.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllAppTypes.Image")));
            this.btnClearAllAppTypes.Location = new System.Drawing.Point(373, 0);
            this.btnClearAllAppTypes.Name = "btnClearAllAppTypes";
            this.btnClearAllAppTypes.Size = new System.Drawing.Size(29, 23);
            this.btnClearAllAppTypes.TabIndex = 101;
            this.toolTip1.SetToolTip(this.btnClearAllAppTypes, "Deselect All");
            this.btnClearAllAppTypes.UseVisualStyleBackColor = false;
            this.btnClearAllAppTypes.Click += new System.EventHandler(this.btnClearAllAppTypes_Click);
            // 
            // btnSelectAllAppTypes
            // 
            this.btnSelectAllAppTypes.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllAppTypes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectAllAppTypes.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAllAppTypes.FlatAppearance.BorderSize = 0;
            this.btnSelectAllAppTypes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllAppTypes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllAppTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllAppTypes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAllAppTypes.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllAppTypes.Image")));
            this.btnSelectAllAppTypes.Location = new System.Drawing.Point(402, 0);
            this.btnSelectAllAppTypes.Name = "btnSelectAllAppTypes";
            this.btnSelectAllAppTypes.Size = new System.Drawing.Size(29, 23);
            this.btnSelectAllAppTypes.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnSelectAllAppTypes, "Select All");
            this.btnSelectAllAppTypes.UseVisualStyleBackColor = false;
            this.btnSelectAllAppTypes.Click += new System.EventHandler(this.btnSelectAllAppTypes_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(0, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(431, 1);
            this.label19.TabIndex = 97;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(431, 24);
            this.label22.TabIndex = 0;
            this.label22.Text = "  Appointment Type :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.cmbLocation_Appointment);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.label26);
            this.panel5.Controls.Add(this.cmbDepartment_Appointment);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 193);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(437, 37);
            this.panel5.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(429, 1);
            this.label21.TabIndex = 127;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(433, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 33);
            this.label13.TabIndex = 126;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Location = new System.Drawing.Point(3, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 33);
            this.label20.TabIndex = 125;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(3, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(431, 1);
            this.label10.TabIndex = 124;
            // 
            // cmbLocation_Appointment
            // 
            this.cmbLocation_Appointment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation_Appointment.FormattingEnabled = true;
            this.cmbLocation_Appointment.Location = new System.Drawing.Point(82, 6);
            this.cmbLocation_Appointment.Name = "cmbLocation_Appointment";
            this.cmbLocation_Appointment.Size = new System.Drawing.Size(114, 22);
            this.cmbLocation_Appointment.TabIndex = 0;
            this.cmbLocation_Appointment.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_Appointment_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(19, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(61, 14);
            this.label25.TabIndex = 4;
            this.label25.Text = "Location :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(204, 10);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 14);
            this.label26.TabIndex = 6;
            this.label26.Text = "Department :";
            // 
            // cmbDepartment_Appointment
            // 
            this.cmbDepartment_Appointment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment_Appointment.FormattingEnabled = true;
            this.cmbDepartment_Appointment.Location = new System.Drawing.Point(287, 6);
            this.cmbDepartment_Appointment.Name = "cmbDepartment_Appointment";
            this.cmbDepartment_Appointment.Size = new System.Drawing.Size(114, 22);
            this.cmbDepartment_Appointment.TabIndex = 1;
            // 
            // pnl_AppointmentSearchHeader
            // 
            this.pnl_AppointmentSearchHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnl_AppointmentSearchHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_AppointmentSearchHeader.Controls.Add(this.pnlAppSearch_Down);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.pnlAppSearch_Up);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label1);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.chkSearchInTemplate);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label2);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label3);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label4);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label5);
            this.pnl_AppointmentSearchHeader.Controls.Add(this.label11);
            this.pnl_AppointmentSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_AppointmentSearchHeader.Location = new System.Drawing.Point(0, 263);
            this.pnl_AppointmentSearchHeader.Name = "pnl_AppointmentSearchHeader";
            this.pnl_AppointmentSearchHeader.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnl_AppointmentSearchHeader.Size = new System.Drawing.Size(437, 26);
            this.pnl_AppointmentSearchHeader.TabIndex = 1;
            // 
            // pnlAppSearch_Down
            // 
            this.pnlAppSearch_Down.BackColor = System.Drawing.Color.Transparent;
            this.pnlAppSearch_Down.Controls.Add(this.btn_AppSearch_Down);
            this.pnlAppSearch_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAppSearch_Down.Location = new System.Drawing.Point(377, 4);
            this.pnlAppSearch_Down.Name = "pnlAppSearch_Down";
            this.pnlAppSearch_Down.Size = new System.Drawing.Size(28, 21);
            this.pnlAppSearch_Down.TabIndex = 4;
            this.pnlAppSearch_Down.Visible = false;
            // 
            // btn_AppSearch_Down
            // 
            this.btn_AppSearch_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AppSearch_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AppSearch_Down.FlatAppearance.BorderSize = 0;
            this.btn_AppSearch_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AppSearch_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AppSearch_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AppSearch_Down.Location = new System.Drawing.Point(0, 0);
            this.btn_AppSearch_Down.Name = "btn_AppSearch_Down";
            this.btn_AppSearch_Down.Size = new System.Drawing.Size(28, 21);
            this.btn_AppSearch_Down.TabIndex = 0;
            this.btn_AppSearch_Down.UseVisualStyleBackColor = true;
            this.btn_AppSearch_Down.Click += new System.EventHandler(this.btn_AppSearch_Down_Click);
            this.btn_AppSearch_Down.MouseLeave += new System.EventHandler(this.btn_AppSearch_Down_MouseLeave);
            this.btn_AppSearch_Down.MouseHover += new System.EventHandler(this.btn_AppSearch_Down_MouseHover);
            // 
            // pnlAppSearch_Up
            // 
            this.pnlAppSearch_Up.BackColor = System.Drawing.Color.Transparent;
            this.pnlAppSearch_Up.Controls.Add(this.btn_AppSearch_Up);
            this.pnlAppSearch_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAppSearch_Up.Location = new System.Drawing.Point(405, 4);
            this.pnlAppSearch_Up.Name = "pnlAppSearch_Up";
            this.pnlAppSearch_Up.Size = new System.Drawing.Size(28, 21);
            this.pnlAppSearch_Up.TabIndex = 3;
            // 
            // btn_AppSearch_Up
            // 
            this.btn_AppSearch_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AppSearch_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AppSearch_Up.FlatAppearance.BorderSize = 0;
            this.btn_AppSearch_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AppSearch_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AppSearch_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AppSearch_Up.Location = new System.Drawing.Point(0, 0);
            this.btn_AppSearch_Up.Name = "btn_AppSearch_Up";
            this.btn_AppSearch_Up.Size = new System.Drawing.Size(28, 21);
            this.btn_AppSearch_Up.TabIndex = 0;
            this.btn_AppSearch_Up.UseVisualStyleBackColor = true;
            this.btn_AppSearch_Up.Click += new System.EventHandler(this.btn_AppSearch_Up_Click);
            this.btn_AppSearch_Up.MouseLeave += new System.EventHandler(this.btn_AppSearch_Up_MouseLeave);
            this.btn_AppSearch_Up.MouseHover += new System.EventHandler(this.btn_AppSearch_Up_MouseHover);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "  Search in Appointment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkSearchInTemplate
            // 
            this.chkSearchInTemplate.AutoSize = true;
            this.chkSearchInTemplate.BackColor = System.Drawing.Color.Transparent;
            this.chkSearchInTemplate.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSearchInTemplate.Location = new System.Drawing.Point(9, 4);
            this.chkSearchInTemplate.Name = "chkSearchInTemplate";
            this.chkSearchInTemplate.Size = new System.Drawing.Size(15, 21);
            this.chkSearchInTemplate.TabIndex = 0;
            this.chkSearchInTemplate.UseVisualStyleBackColor = false;
            this.chkSearchInTemplate.CheckedChanged += new System.EventHandler(this.chkSearchInTemplate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(5, 21);
            this.label2.TabIndex = 104;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(429, 1);
            this.label3.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 22);
            this.label4.TabIndex = 123;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(433, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 22);
            this.label5.TabIndex = 124;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(3, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(431, 1);
            this.label11.TabIndex = 126;
            // 
            // pnlSimpleSearch
            // 
            this.pnlSimpleSearch.Controls.Add(this.pnlSearchDates);
            this.pnlSimpleSearch.Controls.Add(this.pnlProvider);
            this.pnlSimpleSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSimpleSearch.Location = new System.Drawing.Point(0, 27);
            this.pnlSimpleSearch.Name = "pnlSimpleSearch";
            this.pnlSimpleSearch.Size = new System.Drawing.Size(437, 236);
            this.pnlSimpleSearch.TabIndex = 0;
            // 
            // pnlSearchDates
            // 
            this.pnlSearchDates.Controls.Add(this.lbl_pnlSimpleSearchBottomBrd);
            this.pnlSearchDates.Controls.Add(this.lbl_pnlSimpleSearchRightBrd);
            this.pnlSearchDates.Controls.Add(this.lbl_pnlSimpleSearchTopBrd);
            this.pnlSearchDates.Controls.Add(this.lbl_pnlSimpleSearchLeftBrd);
            this.pnlSearchDates.Controls.Add(this.dtpEndDate);
            this.pnlSearchDates.Controls.Add(this.lblStartDate);
            this.pnlSearchDates.Controls.Add(this.label12);
            this.pnlSearchDates.Controls.Add(this.dtpStartDate);
            this.pnlSearchDates.Controls.Add(this.cmbAMPMAll);
            this.pnlSearchDates.Controls.Add(this.lblEndDate);
            this.pnlSearchDates.Controls.Add(this.lblSimple_Duration);
            this.pnlSearchDates.Controls.Add(this.num_Duration);
            this.pnlSearchDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchDates.Location = new System.Drawing.Point(0, 173);
            this.pnlSearchDates.Name = "pnlSearchDates";
            this.pnlSearchDates.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSearchDates.Size = new System.Drawing.Size(437, 63);
            this.pnlSearchDates.TabIndex = 2;
            // 
            // lbl_pnlSimpleSearchBottomBrd
            // 
            this.lbl_pnlSimpleSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlSimpleSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSimpleSearchBottomBrd.Location = new System.Drawing.Point(4, 59);
            this.lbl_pnlSimpleSearchBottomBrd.Name = "lbl_pnlSimpleSearchBottomBrd";
            this.lbl_pnlSimpleSearchBottomBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_pnlSimpleSearchBottomBrd.TabIndex = 97;
            // 
            // lbl_pnlSimpleSearchRightBrd
            // 
            this.lbl_pnlSimpleSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlSimpleSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSimpleSearchRightBrd.Location = new System.Drawing.Point(433, 1);
            this.lbl_pnlSimpleSearchRightBrd.Name = "lbl_pnlSimpleSearchRightBrd";
            this.lbl_pnlSimpleSearchRightBrd.Size = new System.Drawing.Size(1, 59);
            this.lbl_pnlSimpleSearchRightBrd.TabIndex = 96;
            // 
            // lbl_pnlSimpleSearchTopBrd
            // 
            this.lbl_pnlSimpleSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlSimpleSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSimpleSearchTopBrd.Location = new System.Drawing.Point(4, 0);
            this.lbl_pnlSimpleSearchTopBrd.Name = "lbl_pnlSimpleSearchTopBrd";
            this.lbl_pnlSimpleSearchTopBrd.Size = new System.Drawing.Size(430, 1);
            this.lbl_pnlSimpleSearchTopBrd.TabIndex = 94;
            // 
            // lbl_pnlSimpleSearchLeftBrd
            // 
            this.lbl_pnlSimpleSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlSimpleSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSimpleSearchLeftBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSimpleSearchLeftBrd.Name = "lbl_pnlSimpleSearchLeftBrd";
            this.lbl_pnlSimpleSearchLeftBrd.Size = new System.Drawing.Size(1, 60);
            this.lbl_pnlSimpleSearchLeftBrd.TabIndex = 95;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(303, 5);
            this.dtpEndDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtpEndDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(99, 22);
            this.dtpEndDate.TabIndex = 1;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(38, 9);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(58, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 14);
            this.label12.TabIndex = 162;
            this.label12.Text = "AM/PM :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(112, 5);
            this.dtpStartDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(99, 22);
            this.dtpStartDate.TabIndex = 0;
            // 
            // cmbAMPMAll
            // 
            this.cmbAMPMAll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAMPMAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAMPMAll.ForeColor = System.Drawing.Color.Black;
            this.cmbAMPMAll.FormattingEnabled = true;
            this.cmbAMPMAll.Items.AddRange(new object[] {
            "All",
            "AM",
            "PM"});
            this.cmbAMPMAll.Location = new System.Drawing.Point(114, 34);
            this.cmbAMPMAll.Name = "cmbAMPMAll";
            this.cmbAMPMAll.Size = new System.Drawing.Size(54, 22);
            this.cmbAMPMAll.TabIndex = 2;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(235, 9);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date :";
            // 
            // lblSimple_Duration
            // 
            this.lblSimple_Duration.AutoSize = true;
            this.lblSimple_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblSimple_Duration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimple_Duration.Location = new System.Drawing.Point(243, 38);
            this.lblSimple_Duration.Name = "lblSimple_Duration";
            this.lblSimple_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblSimple_Duration.TabIndex = 160;
            this.lblSimple_Duration.Text = "Duration :";
            // 
            // num_Duration
            // 
            this.num_Duration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_Duration.Location = new System.Drawing.Point(305, 34);
            this.num_Duration.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.num_Duration.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.num_Duration.Name = "num_Duration";
            this.num_Duration.Size = new System.Drawing.Size(59, 22);
            this.num_Duration.TabIndex = 3;
            this.num_Duration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.label27);
            this.pnlProvider.Controls.Add(this.label23);
            this.pnlProvider.Controls.Add(this.label15);
            this.pnlProvider.Controls.Add(this.panel1);
            this.pnlProvider.Controls.Add(this.label28);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 0);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Padding = new System.Windows.Forms.Padding(3);
            this.pnlProvider.Size = new System.Drawing.Size(437, 173);
            this.pnlProvider.TabIndex = 1;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Location = new System.Drawing.Point(4, 169);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(429, 1);
            this.label27.TabIndex = 102;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(4, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(429, 1);
            this.label23.TabIndex = 101;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Location = new System.Drawing.Point(433, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 167);
            this.label15.TabIndex = 100;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.trvProvider);
            this.panel1.Controls.Add(this.pnlProviderHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 167);
            this.panel1.TabIndex = 2;
            // 
            // trvProvider
            // 
            this.trvProvider.BackColor = System.Drawing.Color.White;
            this.trvProvider.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvProvider.CheckBoxes = true;
            this.trvProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvProvider.ForeColor = System.Drawing.Color.Black;
            this.trvProvider.Indent = 20;
            this.trvProvider.ItemHeight = 19;
            this.trvProvider.Location = new System.Drawing.Point(0, 24);
            this.trvProvider.Name = "trvProvider";
            this.trvProvider.ShowLines = false;
            this.trvProvider.ShowPlusMinus = false;
            this.trvProvider.ShowRootLines = false;
            this.trvProvider.Size = new System.Drawing.Size(430, 143);
            this.trvProvider.TabIndex = 3;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnClearAllProvider);
            this.pnlProviderHeader.Controls.Add(this.label44);
            this.pnlProviderHeader.Controls.Add(this.btnSelectAllProvider);
            this.pnlProviderHeader.Controls.Add(this.label43);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(430, 24);
            this.pnlProviderHeader.TabIndex = 4;
            // 
            // btnClearAllProvider
            // 
            this.btnClearAllProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearAllProvider.FlatAppearance.BorderSize = 0;
            this.btnClearAllProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearAllProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearAllProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllProvider.Image")));
            this.btnClearAllProvider.Location = new System.Drawing.Point(372, 0);
            this.btnClearAllProvider.Name = "btnClearAllProvider";
            this.btnClearAllProvider.Size = new System.Drawing.Size(29, 23);
            this.btnClearAllProvider.TabIndex = 99;
            this.toolTip1.SetToolTip(this.btnClearAllProvider, "Deselect All");
            this.btnClearAllProvider.UseVisualStyleBackColor = false;
            this.btnClearAllProvider.Click += new System.EventHandler(this.btnClearAllProvider_Click);
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(401, 23);
            this.label44.TabIndex = 0;
            this.label44.Text = "   Providers :";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectAllProvider
            // 
            this.btnSelectAllProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectAllProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAllProvider.FlatAppearance.BorderSize = 0;
            this.btnSelectAllProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAllProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllProvider.Image")));
            this.btnSelectAllProvider.Location = new System.Drawing.Point(401, 0);
            this.btnSelectAllProvider.Name = "btnSelectAllProvider";
            this.btnSelectAllProvider.Size = new System.Drawing.Size(29, 23);
            this.btnSelectAllProvider.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnSelectAllProvider, "Select All");
            this.btnSelectAllProvider.UseVisualStyleBackColor = false;
            this.btnSelectAllProvider.Click += new System.EventHandler(this.btnSelectAllProvider_Click);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(0, 23);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(430, 1);
            this.label43.TabIndex = 97;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(3, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 167);
            this.label28.TabIndex = 93;
            // 
            // pnlSimpleSearchHeader
            // 
            this.pnlSimpleSearchHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlSimpleSearchHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSimpleSearchHeader.Controls.Add(this.lbl_pnlSimpleSearchHeaderLeftBrd);
            this.pnlSimpleSearchHeader.Controls.Add(this.lblSimpleSearch);
            this.pnlSimpleSearchHeader.Controls.Add(this.pnl_btn_HideSearchPnl);
            this.pnlSimpleSearchHeader.Controls.Add(this.lbl_pnlSimpleSearchHeaderBottomBrd);
            this.pnlSimpleSearchHeader.Controls.Add(this.lbl_pnlSimpleSearchHeaderTopBrd);
            this.pnlSimpleSearchHeader.Controls.Add(this.lbl_pnlSimpleSearchHeaderRightBrd);
            this.pnlSimpleSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSimpleSearchHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlSimpleSearchHeader.Name = "pnlSimpleSearchHeader";
            this.pnlSimpleSearchHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlSimpleSearchHeader.Size = new System.Drawing.Size(437, 24);
            this.pnlSimpleSearchHeader.TabIndex = 92;
            // 
            // lbl_pnlSimpleSearchHeaderLeftBrd
            // 
            this.lbl_pnlSimpleSearchHeaderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSimpleSearchHeaderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSimpleSearchHeaderLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlSimpleSearchHeaderLeftBrd.Name = "lbl_pnlSimpleSearchHeaderLeftBrd";
            this.lbl_pnlSimpleSearchHeaderLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlSimpleSearchHeaderLeftBrd.TabIndex = 126;
            // 
            // lblSimpleSearch
            // 
            this.lblSimpleSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSimpleSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSimpleSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimpleSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSimpleSearch.Location = new System.Drawing.Point(3, 1);
            this.lblSimpleSearch.Name = "lblSimpleSearch";
            this.lblSimpleSearch.Size = new System.Drawing.Size(402, 22);
            this.lblSimpleSearch.TabIndex = 0;
            this.lblSimpleSearch.Text = "  Simple Search";
            this.lblSimpleSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_btn_HideSearchPnl
            // 
            this.pnl_btn_HideSearchPnl.BackColor = System.Drawing.Color.Transparent;
            this.pnl_btn_HideSearchPnl.Controls.Add(this.btn_HideSearchPnl);
            this.pnl_btn_HideSearchPnl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_btn_HideSearchPnl.Location = new System.Drawing.Point(405, 1);
            this.pnl_btn_HideSearchPnl.Name = "pnl_btn_HideSearchPnl";
            this.pnl_btn_HideSearchPnl.Size = new System.Drawing.Size(28, 22);
            this.pnl_btn_HideSearchPnl.TabIndex = 2;
            // 
            // btn_HideSearchPnl
            // 
            this.btn_HideSearchPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_HideSearchPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_HideSearchPnl.FlatAppearance.BorderSize = 0;
            this.btn_HideSearchPnl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_HideSearchPnl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_HideSearchPnl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_HideSearchPnl.Location = new System.Drawing.Point(0, 0);
            this.btn_HideSearchPnl.Name = "btn_HideSearchPnl";
            this.btn_HideSearchPnl.Size = new System.Drawing.Size(28, 22);
            this.btn_HideSearchPnl.TabIndex = 0;
            this.btn_HideSearchPnl.UseVisualStyleBackColor = true;
            this.btn_HideSearchPnl.Click += new System.EventHandler(this.btn_HideSearchPnl_Click);
            this.btn_HideSearchPnl.MouseLeave += new System.EventHandler(this.btn_HideSearchPnl_MouseLeave);
            this.btn_HideSearchPnl.MouseHover += new System.EventHandler(this.btn_HideSearchPnl_MouseHover);
            // 
            // lbl_pnlSimpleSearchHeaderBottomBrd
            // 
            this.lbl_pnlSimpleSearchHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSimpleSearchHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSimpleSearchHeaderBottomBrd.Location = new System.Drawing.Point(3, 23);
            this.lbl_pnlSimpleSearchHeaderBottomBrd.Name = "lbl_pnlSimpleSearchHeaderBottomBrd";
            this.lbl_pnlSimpleSearchHeaderBottomBrd.Size = new System.Drawing.Size(430, 1);
            this.lbl_pnlSimpleSearchHeaderBottomBrd.TabIndex = 124;
            // 
            // lbl_pnlSimpleSearchHeaderTopBrd
            // 
            this.lbl_pnlSimpleSearchHeaderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSimpleSearchHeaderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSimpleSearchHeaderTopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSimpleSearchHeaderTopBrd.Name = "lbl_pnlSimpleSearchHeaderTopBrd";
            this.lbl_pnlSimpleSearchHeaderTopBrd.Size = new System.Drawing.Size(430, 1);
            this.lbl_pnlSimpleSearchHeaderTopBrd.TabIndex = 125;
            // 
            // lbl_pnlSimpleSearchHeaderRightBrd
            // 
            this.lbl_pnlSimpleSearchHeaderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSimpleSearchHeaderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSimpleSearchHeaderRightBrd.Location = new System.Drawing.Point(433, 0);
            this.lbl_pnlSimpleSearchHeaderRightBrd.Name = "lbl_pnlSimpleSearchHeaderRightBrd";
            this.lbl_pnlSimpleSearchHeaderRightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSimpleSearchHeaderRightBrd.TabIndex = 124;
            // 
            // pnlCalendarView
            // 
            this.pnlCalendarView.Controls.Add(this.pnlListControl);
            this.pnlCalendarView.Controls.Add(this.lbl_pnlJanusControlBottomBrd);
            this.pnlCalendarView.Controls.Add(this.lbl_pnlJanusControlLeftBrd);
            this.pnlCalendarView.Controls.Add(this.lbl_pnlJanusControlRightBrd);
            this.pnlCalendarView.Controls.Add(this.lbl_pnlJanusControlTopBrd);
            this.pnlCalendarView.Controls.Add(this.juc_Appointment);
            this.pnlCalendarView.Controls.Add(this.lblPleaseWait);
            this.pnlCalendarView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalendarView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCalendarView.Location = new System.Drawing.Point(468, 0);
            this.pnlCalendarView.Name = "pnlCalendarView";
            this.pnlCalendarView.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCalendarView.Size = new System.Drawing.Size(816, 728);
            this.pnlCalendarView.TabIndex = 40;
            // 
            // pnlListControl
            // 
            this.pnlListControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlListControl.Location = new System.Drawing.Point(540, 460);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Size = new System.Drawing.Size(215, 116);
            this.pnlListControl.TabIndex = 108;
            this.pnlListControl.Visible = false;
            // 
            // lbl_pnlJanusControlBottomBrd
            // 
            this.lbl_pnlJanusControlBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlJanusControlBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlJanusControlBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlJanusControlBottomBrd.Location = new System.Drawing.Point(4, 724);
            this.lbl_pnlJanusControlBottomBrd.Name = "lbl_pnlJanusControlBottomBrd";
            this.lbl_pnlJanusControlBottomBrd.Size = new System.Drawing.Size(808, 1);
            this.lbl_pnlJanusControlBottomBrd.TabIndex = 107;
            this.lbl_pnlJanusControlBottomBrd.Text = "label2";
            // 
            // lbl_pnlJanusControlLeftBrd
            // 
            this.lbl_pnlJanusControlLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlJanusControlLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlJanusControlLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlJanusControlLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlJanusControlLeftBrd.Name = "lbl_pnlJanusControlLeftBrd";
            this.lbl_pnlJanusControlLeftBrd.Size = new System.Drawing.Size(1, 721);
            this.lbl_pnlJanusControlLeftBrd.TabIndex = 106;
            this.lbl_pnlJanusControlLeftBrd.Text = "label4";
            // 
            // lbl_pnlJanusControlRightBrd
            // 
            this.lbl_pnlJanusControlRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlJanusControlRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlJanusControlRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlJanusControlRightBrd.Location = new System.Drawing.Point(812, 4);
            this.lbl_pnlJanusControlRightBrd.Name = "lbl_pnlJanusControlRightBrd";
            this.lbl_pnlJanusControlRightBrd.Size = new System.Drawing.Size(1, 721);
            this.lbl_pnlJanusControlRightBrd.TabIndex = 105;
            this.lbl_pnlJanusControlRightBrd.Text = "label3";
            // 
            // lbl_pnlJanusControlTopBrd
            // 
            this.lbl_pnlJanusControlTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlJanusControlTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlJanusControlTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlJanusControlTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlJanusControlTopBrd.Name = "lbl_pnlJanusControlTopBrd";
            this.lbl_pnlJanusControlTopBrd.Size = new System.Drawing.Size(810, 1);
            this.lbl_pnlJanusControlTopBrd.TabIndex = 104;
            this.lbl_pnlJanusControlTopBrd.Text = "label1";
            // 
            // juc_Appointment
            // 
            this.juc_Appointment.AddNewMode = Janus.Windows.Schedule.AddNewMode.Manual;
            this.juc_Appointment.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None;
            this.juc_Appointment.AllowDelete = false;
            this.juc_Appointment.AllowDragInNonWorkingTime = false;
            this.juc_Appointment.AllowEdit = false;
            this.juc_Appointment.DayNavigationButtons = true;
            this.juc_Appointment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.juc_Appointment.FirstVisibleTime = System.TimeSpan.Parse("09:00:00");
            this.juc_Appointment.HighlightCurrentTime = Janus.Windows.Schedule.TriState.True;
            this.juc_Appointment.HorizontalScrollPosition = 0;
            this.juc_Appointment.ImageList = this.imageList1;
            this.juc_Appointment.Location = new System.Drawing.Point(3, 3);
            this.juc_Appointment.MultiOwner = true;
            this.juc_Appointment.Name = "juc_Appointment";
            this.juc_Appointment.ShowMinutesInTimeNavigator = true;
            this.juc_Appointment.ShowTimeHintOnAppointments = Janus.Windows.Schedule.TimeHintOnAppointments.Never;
            this.juc_Appointment.Size = new System.Drawing.Size(810, 722);
            this.juc_Appointment.TabIndex = 99;
            this.juc_Appointment.VerticalScrollPosition = 18;
            this.juc_Appointment.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            this.juc_Appointment.WorkEndTime = System.TimeSpan.Parse("18:00:00");
            this.juc_Appointment.WorkStartTime = System.TimeSpan.Parse("09:00:00");
            this.juc_Appointment.WorkWeek = ((Janus.Windows.Schedule.ScheduleDayOfWeek)(((((((Janus.Windows.Schedule.ScheduleDayOfWeek.Sunday | Janus.Windows.Schedule.ScheduleDayOfWeek.Monday)
                        | Janus.Windows.Schedule.ScheduleDayOfWeek.Tuesday)
                        | Janus.Windows.Schedule.ScheduleDayOfWeek.Wednesday)
                        | Janus.Windows.Schedule.ScheduleDayOfWeek.Thursday)
                        | Janus.Windows.Schedule.ScheduleDayOfWeek.Friday)
                        | Janus.Windows.Schedule.ScheduleDayOfWeek.Saturday)));
            this.juc_Appointment.AppointmentChanged += new Janus.Windows.Schedule.AppointmentChangeEventHandler(this.juc_Appointment_AppointmentChanged);
            this.juc_Appointment.DatesChanged += new System.EventHandler(this.juc_Appointment_DatesChanged);
            this.juc_Appointment.Click += new System.EventHandler(this.juc_Appointment_Click);
            this.juc_Appointment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.juc_Appointment_MouseMove);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Open Signature.ico");
            this.imageList1.Images.SetKeyName(1, "Recurrence.ico");
            this.imageList1.Images.SetKeyName(2, "SingleInRecurrence.ico");
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPleaseWait.Font = new System.Drawing.Font("Baskerville Old Face", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(3, 3);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(810, 722);
            this.lblPleaseWait.TabIndex = 109;
            this.lblPleaseWait.Text = "Please Wait  !  !  !";
            this.lblPleaseWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOther2
            // 
            this.pnlOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOther2.Controls.Add(this.lbl_pnlOther2BottomBrd);
            this.pnlOther2.Controls.Add(this.pnlOther2Header);
            this.pnlOther2.Controls.Add(this.lbl_pnlOther2LeftBrd);
            this.pnlOther2.Controls.Add(this.lbl_pnlOther2RightBrd);
            this.pnlOther2.Controls.Add(this.lbl_lbl_pnlOther2TopBrd);
            this.pnlOther2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOther2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlOther2.Location = new System.Drawing.Point(468, 728);
            this.pnlOther2.Name = "pnlOther2";
            this.pnlOther2.Padding = new System.Windows.Forms.Padding(3);
            this.pnlOther2.Size = new System.Drawing.Size(816, 79);
            this.pnlOther2.TabIndex = 103;
            this.pnlOther2.Visible = false;
            // 
            // lbl_pnlOther2BottomBrd
            // 
            this.lbl_pnlOther2BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlOther2BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlOther2BottomBrd.Location = new System.Drawing.Point(4, 75);
            this.lbl_pnlOther2BottomBrd.Name = "lbl_pnlOther2BottomBrd";
            this.lbl_pnlOther2BottomBrd.Size = new System.Drawing.Size(808, 1);
            this.lbl_pnlOther2BottomBrd.TabIndex = 97;
            // 
            // pnlOther2Header
            // 
            this.pnlOther2Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlOther2Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOther2Header.Controls.Add(this.lbl_pnlOther2HeaderBottomBrd);
            this.pnlOther2Header.Controls.Add(this.lblOther2);
            this.pnlOther2Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOther2Header.Location = new System.Drawing.Point(4, 4);
            this.pnlOther2Header.Name = "pnlOther2Header";
            this.pnlOther2Header.Size = new System.Drawing.Size(808, 25);
            this.pnlOther2Header.TabIndex = 90;
            this.pnlOther2Header.Visible = false;
            // 
            // lbl_pnlOther2HeaderBottomBrd
            // 
            this.lbl_pnlOther2HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlOther2HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlOther2HeaderBottomBrd.Location = new System.Drawing.Point(0, 24);
            this.lbl_pnlOther2HeaderBottomBrd.Name = "lbl_pnlOther2HeaderBottomBrd";
            this.lbl_pnlOther2HeaderBottomBrd.Size = new System.Drawing.Size(808, 1);
            this.lbl_pnlOther2HeaderBottomBrd.TabIndex = 98;
            // 
            // lblOther2
            // 
            this.lblOther2.BackColor = System.Drawing.Color.Transparent;
            this.lblOther2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOther2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOther2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOther2.Location = new System.Drawing.Point(0, 0);
            this.lblOther2.Name = "lblOther2";
            this.lblOther2.Size = new System.Drawing.Size(808, 25);
            this.lblOther2.TabIndex = 0;
            this.lblOther2.Text = " Other 2";
            this.lblOther2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlOther2LeftBrd
            // 
            this.lbl_pnlOther2LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlOther2LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlOther2LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlOther2LeftBrd.Name = "lbl_pnlOther2LeftBrd";
            this.lbl_pnlOther2LeftBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlOther2LeftBrd.TabIndex = 91;
            // 
            // lbl_pnlOther2RightBrd
            // 
            this.lbl_pnlOther2RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlOther2RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlOther2RightBrd.Location = new System.Drawing.Point(812, 4);
            this.lbl_pnlOther2RightBrd.Name = "lbl_pnlOther2RightBrd";
            this.lbl_pnlOther2RightBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlOther2RightBrd.TabIndex = 94;
            // 
            // lbl_lbl_pnlOther2TopBrd
            // 
            this.lbl_lbl_pnlOther2TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_lbl_pnlOther2TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_lbl_pnlOther2TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_lbl_pnlOther2TopBrd.Name = "lbl_lbl_pnlOther2TopBrd";
            this.lbl_lbl_pnlOther2TopBrd.Size = new System.Drawing.Size(810, 1);
            this.lbl_lbl_pnlOther2TopBrd.TabIndex = 96;
            // 
            // pnlCalender
            // 
            this.pnlCalender.Controls.Add(this.pnlListView);
            this.pnlCalender.Controls.Add(this.pnlCalendarView);
            this.pnlCalender.Controls.Add(this.pnlOther2);
            this.pnlCalender.Controls.Add(this.pnlVertical_tslp);
            this.pnlCalender.Controls.Add(this.pnlSearch);
            this.pnlCalender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalender.Location = new System.Drawing.Point(0, 0);
            this.pnlCalender.Name = "pnlCalender";
            this.pnlCalender.Size = new System.Drawing.Size(1284, 807);
            this.pnlCalender.TabIndex = 23;
            // 
            // pnlListView
            // 
            this.pnlListView.Controls.Add(this.c1Appointments);
            this.pnlListView.Controls.Add(this.panel7);
            this.pnlListView.Controls.Add(this.label6);
            this.pnlListView.Controls.Add(this.label7);
            this.pnlListView.Controls.Add(this.label8);
            this.pnlListView.Controls.Add(this.label9);
            this.pnlListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlListView.Location = new System.Drawing.Point(468, 0);
            this.pnlListView.Name = "pnlListView";
            this.pnlListView.Padding = new System.Windows.Forms.Padding(3);
            this.pnlListView.Size = new System.Drawing.Size(816, 728);
            this.pnlListView.TabIndex = 131;
            this.pnlListView.Visible = false;
            // 
            // c1Appointments
            // 
            this.c1Appointments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Appointments.AllowEditing = false;
            this.c1Appointments.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Appointments.AutoGenerateColumns = false;
            this.c1Appointments.BackColor = System.Drawing.Color.Transparent;
            this.c1Appointments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Appointments.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Appointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Appointments.ExtendLastCol = true;
            this.c1Appointments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Appointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Appointments.Location = new System.Drawing.Point(4, 27);
            this.c1Appointments.Name = "c1Appointments";
            this.c1Appointments.Rows.Count = 1;
            this.c1Appointments.Rows.DefaultSize = 19;
            this.c1Appointments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Appointments.ShowCellLabels = true;
            this.c1Appointments.Size = new System.Drawing.Size(808, 697);
            this.c1Appointments.StyleInfo = resources.GetString("c1Appointments.StyleInfo");
            this.c1Appointments.TabIndex = 108;
            this.c1Appointments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Appointments_MouseDoubleClick);
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.cmbListViewProvider);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(4, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(808, 23);
            this.panel7.TabIndex = 109;
            // 
            // cmbListViewProvider
            // 
            this.cmbListViewProvider.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbListViewProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListViewProvider.FormattingEnabled = true;
            this.cmbListViewProvider.Location = new System.Drawing.Point(0, 0);
            this.cmbListViewProvider.Name = "cmbListViewProvider";
            this.cmbListViewProvider.Size = new System.Drawing.Size(213, 22);
            this.cmbListViewProvider.TabIndex = 99;
            this.cmbListViewProvider.SelectionChangeCommitted += new System.EventHandler(this.cmbListViewProvider_SelectionChangeCommitted);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(0, 22);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(808, 1);
            this.label24.TabIndex = 98;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(4, 724);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(808, 1);
            this.label6.TabIndex = 107;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 721);
            this.label7.TabIndex = 106;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(812, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 721);
            this.label8.TabIndex = 105;
            this.label8.Text = "label3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(810, 1);
            this.label9.TabIndex = 104;
            this.label9.Text = "label1";
            // 
            // pnlVertical_tslp
            // 
            this.pnlVertical_tslp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlVertical_tslp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlVertical_tslp.Controls.Add(this.tl_Search);
            this.pnlVertical_tslp.Controls.Add(this.label16);
            this.pnlVertical_tslp.Controls.Add(this.label45);
            this.pnlVertical_tslp.Controls.Add(this.btn_ShowSearchPnl);
            this.pnlVertical_tslp.Controls.Add(this.lbl_pnlVertical_tslpLeftBrd);
            this.pnlVertical_tslp.Controls.Add(this.lbl_pnlVertical_tslpTopBrd);
            this.pnlVertical_tslp.Controls.Add(this.label14);
            this.pnlVertical_tslp.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlVertical_tslp.Location = new System.Drawing.Point(437, 0);
            this.pnlVertical_tslp.Name = "pnlVertical_tslp";
            this.pnlVertical_tslp.Padding = new System.Windows.Forms.Padding(3);
            this.pnlVertical_tslp.Size = new System.Drawing.Size(31, 807);
            this.pnlVertical_tslp.TabIndex = 130;
            this.pnlVertical_tslp.Visible = false;
            // 
            // tl_Search
            // 
            this.tl_Search.BackColor = System.Drawing.Color.Transparent;
            this.tl_Search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tl_Search.BackgroundImage")));
            this.tl_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tl_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tl_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_Search.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tl_Search.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSearch});
            this.tl_Search.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tl_Search.Location = new System.Drawing.Point(4, 27);
            this.tl_Search.Name = "tl_Search";
            this.tl_Search.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tl_Search.Size = new System.Drawing.Size(23, 776);
            this.tl_Search.TabIndex = 0;
            this.tl_Search.Text = "toolStrip1";
            this.tl_Search.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // ts_btnSearch
            // 
            this.ts_btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.ts_btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSearch.Image")));
            this.ts_btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSearch.Name = "ts_btnSearch";
            this.ts_btnSearch.Size = new System.Drawing.Size(21, 68);
            this.ts_btnSearch.Text = "Search";
            this.ts_btnSearch.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            this.ts_btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_btnSearch.Click += new System.EventHandler(this.ts_btnSearch_Click);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(4, 803);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 1);
            this.label16.TabIndex = 145;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(4, 26);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(23, 1);
            this.label45.TabIndex = 143;
            // 
            // btn_ShowSearchPnl
            // 
            this.btn_ShowSearchPnl.BackColor = System.Drawing.Color.Transparent;
            this.btn_ShowSearchPnl.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btn_ShowSearchPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ShowSearchPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ShowSearchPnl.FlatAppearance.BorderSize = 0;
            this.btn_ShowSearchPnl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ShowSearchPnl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ShowSearchPnl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowSearchPnl.Location = new System.Drawing.Point(4, 4);
            this.btn_ShowSearchPnl.Name = "btn_ShowSearchPnl";
            this.btn_ShowSearchPnl.Size = new System.Drawing.Size(23, 22);
            this.btn_ShowSearchPnl.TabIndex = 1;
            this.btn_ShowSearchPnl.UseVisualStyleBackColor = false;
            this.btn_ShowSearchPnl.Click += new System.EventHandler(this.btn_ShowSearchPnl_Click);
            this.btn_ShowSearchPnl.MouseLeave += new System.EventHandler(this.btn_ShowSearchPnl_MouseLeave);
            this.btn_ShowSearchPnl.MouseHover += new System.EventHandler(this.btn_ShowSearchPnl_MouseHover);
            // 
            // lbl_pnlVertical_tslpLeftBrd
            // 
            this.lbl_pnlVertical_tslpLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlVertical_tslpLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlVertical_tslpLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlVertical_tslpLeftBrd.Name = "lbl_pnlVertical_tslpLeftBrd";
            this.lbl_pnlVertical_tslpLeftBrd.Size = new System.Drawing.Size(1, 800);
            this.lbl_pnlVertical_tslpLeftBrd.TabIndex = 44;
            // 
            // lbl_pnlVertical_tslpTopBrd
            // 
            this.lbl_pnlVertical_tslpTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlVertical_tslpTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlVertical_tslpTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlVertical_tslpTopBrd.Name = "lbl_pnlVertical_tslpTopBrd";
            this.lbl_pnlVertical_tslpTopBrd.Size = new System.Drawing.Size(24, 1);
            this.lbl_pnlVertical_tslpTopBrd.TabIndex = 47;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(27, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 801);
            this.label14.TabIndex = 144;
            // 
            // cmnu_Appointment
            // 
            this.cmnu_Appointment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnu_SetAppointment});
            this.cmnu_Appointment.Name = "cmnu_SetAppointment";
            this.cmnu_Appointment.Size = new System.Drawing.Size(181, 26);
            // 
            // cmnu_SetAppointment
            // 
            this.cmnu_SetAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_SetAppointment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_SetAppointment.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_SetAppointment.Image")));
            this.cmnu_SetAppointment.Name = "cmnu_SetAppointment";
            this.cmnu_SetAppointment.Size = new System.Drawing.Size(180, 22);
            this.cmnu_SetAppointment.Text = "Set Appointment";
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_DayView,
            this.tsb_WeekView,
            this.tsb_MonthView,
            this.tsb_SearchAppointment,
            this.tsb_ListView,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1284, 53);
            this.ts_Commands.TabIndex = 9;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_DayView
            // 
            this.tsb_DayView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DayView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DayView.Image")));
            this.tsb_DayView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DayView.Name = "tsb_DayView";
            this.tsb_DayView.Size = new System.Drawing.Size(36, 50);
            this.tsb_DayView.Tag = "DayView";
            this.tsb_DayView.Text = "&Day";
            this.tsb_DayView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_WeekView
            // 
            this.tsb_WeekView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_WeekView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_WeekView.Image")));
            this.tsb_WeekView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_WeekView.Name = "tsb_WeekView";
            this.tsb_WeekView.Size = new System.Drawing.Size(44, 50);
            this.tsb_WeekView.Tag = "WeekView";
            this.tsb_WeekView.Text = "&Week";
            this.tsb_WeekView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_MonthView
            // 
            this.tsb_MonthView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MonthView.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MonthView.Image")));
            this.tsb_MonthView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MonthView.Name = "tsb_MonthView";
            this.tsb_MonthView.Size = new System.Drawing.Size(52, 50);
            this.tsb_MonthView.Tag = "MonthView";
            this.tsb_MonthView.Text = "&Month";
            this.tsb_MonthView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_SearchAppointment
            // 
            this.tsb_SearchAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SearchAppointment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SearchAppointment.Image")));
            this.tsb_SearchAppointment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SearchAppointment.Name = "tsb_SearchAppointment";
            this.tsb_SearchAppointment.Size = new System.Drawing.Size(52, 50);
            this.tsb_SearchAppointment.Tag = "SearchAppointment";
            this.tsb_SearchAppointment.Text = "&Search";
            this.tsb_SearchAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SearchAppointment.ToolTipText = "Search Appointments";
            // 
            // tsb_ListView
            // 
            this.tsb_ListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ListView.Image = global::gloAppointmentScheduling.Properties.Resources.List_View;
            this.tsb_ListView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ListView.Name = "tsb_ListView";
            this.tsb_ListView.Size = new System.Drawing.Size(66, 50);
            this.tsb_ListView.Tag = "ListView";
            this.tsb_ListView.Text = "&List View";
            this.tsb_ListView.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ListView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ListView.ToolTipText = "List View";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.ToolTipText = "Close";
            // 
            // pnl_Main
            // 
            this.pnl_Main.Controls.Add(this.pnlCalender);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 53);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(1284, 807);
            this.pnl_Main.TabIndex = 124;
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.ts_Commands);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(1284, 53);
            this.pnl_ToolStrip.TabIndex = 125;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 860);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1284, 2);
            this.splitter1.TabIndex = 126;
            this.splitter1.TabStop = false;
            // 
            // frmSearchAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 862);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSearchAppointment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Appointments";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSearchAppointment_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlRecurrance_Body.ResumeLayout(false);
            this.pnlRecurring_Range.ResumeLayout(false);
            this.pnlRecurring_Range.PerformLayout();
            this.pnlRecurring_Range_Header.ResumeLayout(false);
            this.pnlRecurring_Range_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).EndInit();
            this.pnlRecurring_Pattern_Body.ResumeLayout(false);
            this.pnlRecurring_Pattern_Body.PerformLayout();
            this.pnlRec_Pattern_Daily.ResumeLayout(false);
            this.pnlRec_Pattern_Daily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).EndInit();
            this.pnlRec_Pattern_Monthly.ResumeLayout(false);
            this.pnlRec_Pattern_Monthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).EndInit();
            this.pnlRec_Pattern_Yearly.ResumeLayout(false);
            this.pnlRec_Pattern_Yearly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).EndInit();
            this.pnlRec_Pattern_Weekly.ResumeLayout(false);
            this.pnlRec_Pattern_Weekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).EndInit();
            this.pnlProgressbar.ResumeLayout(false);
            this.pnlProgressbar.PerformLayout();
            this.pnlRecurring_Pattern_Header.ResumeLayout(false);
            this.pnlRecurring_Pattern_Header.PerformLayout();
            this.pnl_AdvanceSearch_Body.ResumeLayout(false);
            this.pnl_AdvanceSearch_Body.PerformLayout();
            this.pnlAdvanceSearchHeader.ResumeLayout(false);
            this.pnlAdvanceSearchHeader.PerformLayout();
            this.pnlAdvanceSearch_Down.ResumeLayout(false);
            this.pnlAdvanceSearch_UP.ResumeLayout(false);
            this.pnlAppointmentSearchBody.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnl_AppointmentSearchHeader.ResumeLayout(false);
            this.pnl_AppointmentSearchHeader.PerformLayout();
            this.pnlAppSearch_Down.ResumeLayout(false);
            this.pnlAppSearch_Up.ResumeLayout(false);
            this.pnlSimpleSearch.ResumeLayout(false);
            this.pnlSearchDates.ResumeLayout(false);
            this.pnlSearchDates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Duration)).EndInit();
            this.pnlProvider.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnlSimpleSearchHeader.ResumeLayout(false);
            this.pnl_btn_HideSearchPnl.ResumeLayout(false);
            this.pnlCalendarView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.juc_Appointment)).EndInit();
            this.pnlOther2.ResumeLayout(false);
            this.pnlOther2Header.ResumeLayout(false);
            this.pnlCalender.ResumeLayout(false);
            this.pnlListView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).EndInit();
            this.panel7.ResumeLayout(false);
            this.pnlVertical_tslp.ResumeLayout(false);
            this.pnlVertical_tslp.PerformLayout();
            this.tl_Search.ResumeLayout(false);
            this.tl_Search.PerformLayout();
            this.cmnu_Appointment.ResumeLayout(false);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_Main.ResumeLayout(false);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_SearchAppointment;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lbl_Location;
        private System.Windows.Forms.Label lbl_Department;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Panel pnlCalender;
        private System.Windows.Forms.ContextMenuStrip cmnu_Appointment;
        private System.Windows.Forms.ToolStripMenuItem cmnu_SetAppointment;
        private System.Windows.Forms.ComboBox cmbResources;
        private System.Windows.Forms.ComboBox cmbProcedures;
        private System.Windows.Forms.Button btn_ClearResource;
        private System.Windows.Forms.Button btn_BrowseResource;
        private System.Windows.Forms.Button btnSelectAllProvider;
        private System.Windows.Forms.Button btn_BrowseProcedure;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Panel pnl_ToolStrip;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlRecurrance_Body;
        private System.Windows.Forms.Panel pnlRecurring_Range;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeTopBrd;
        private System.Windows.Forms.Panel pnlRecurring_Range_Header;
        private System.Windows.Forms.ComboBox cmbRec_Range_NoEndDateYear;
        private System.Windows.Forms.Label lblRecurring_Range_Header;
        private System.Windows.Forms.RadioButton rbRec_Range_NoEndDate;
        private System.Windows.Forms.Label lblRec_Range_EndDate;
        private System.Windows.Forms.DateTimePicker dtpRec_Range_StartDate;
        private System.Windows.Forms.RadioButton rbRec_Range_EndBy;
        private System.Windows.Forms.Label lblRec_Range_StartDate;
        private System.Windows.Forms.RadioButton rbRec_Range_EndAfterOccurence;
        private System.Windows.Forms.DateTimePicker dtpRec_Range_EndBy;
        private System.Windows.Forms.NumericUpDown numRec_Range_EndAfterOccurence;
        private System.Windows.Forms.Label lblRec_Range_Occurence;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Panel pnlSimpleSearch;
        private System.Windows.Forms.Panel pnlCalendarView;
        private System.Windows.Forms.Panel pnlSimpleSearchHeader;
        private System.Windows.Forms.Label lblSimpleSearch;
        private System.Windows.Forms.Panel pnlAdvanceSearchHeader;
        private System.Windows.Forms.Label lblAdvanceSearch;
        private System.Windows.Forms.Panel pnlVertical_tslp;
        private gloGlobal.gloToolStripIgnoreFocus tl_Search;
        private System.Windows.Forms.ToolStripButton ts_btnSearch;
        private System.Windows.Forms.Label lbl_pnlVertical_tslpLeftBrd;
        private System.Windows.Forms.Label lbl_pnlVertical_tslpTopBrd;
        private System.Windows.Forms.Panel pnl_btn_HideSearchPnl;
        private System.Windows.Forms.Button btn_HideSearchPnl;
        private System.Windows.Forms.Panel pnlAdvanceSearch_Down;
        private System.Windows.Forms.Button btn_AdvanceSearch_Down;
        private System.Windows.Forms.Panel pnlAdvanceSearch_UP;
        private System.Windows.Forms.Button btn_AdvanceSearch_UP;
        private System.Windows.Forms.Panel pnl_AdvanceSearch_Body;
        private System.Windows.Forms.Label lbl_pnl_AdvanceSearch_BodyTopBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchRightBrd;
        private System.Windows.Forms.Label lbl_pnlAdvanceSearchHeaderTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeRightBrd;
        private System.Windows.Forms.CheckBox chkApplyAdvSearch;
        private System.Windows.Forms.Label lbl_pnlAdvanceSearchHeader_LeftSpace;
        private System.Windows.Forms.Panel pnlRecurring_Pattern_Body;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_BodyBottomBrd;
        private System.Windows.Forms.Panel pnlRec_Pattern_Yearly;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyLeftBrd;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Yearly_Every_MonthDay;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_Month;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Every_Month;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_DayWeekday;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_FstLst;
        private System.Windows.Forms.Label lblRec_Pattern_Yearly_Criteria_Of;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly_Criteria;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly_EveryMonthDay;
        private System.Windows.Forms.Panel pnlRec_Pattern_Weekly;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyLeftBrd;
        private System.Windows.Forms.Label lblRec_Pattern_Weekly_WeekOn;
        private System.Windows.Forms.Label lblRec_Pattern_Weekly_RecurEvery;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Saturday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Friday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Sunday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Tuesday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Wednesday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Thursday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Monday;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Weekly_WeekOn;
        private System.Windows.Forms.Panel pnlRec_Pattern_Daily;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyTopBrd;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Daily_EveryDay;
        private System.Windows.Forms.Label lblRec_Pattern_Daily_Days;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryWeekday;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryDay;
        private System.Windows.Forms.Panel pnlRec_Pattern_Monthly;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyBottomBrd;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Criteria_Month;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Day_Month;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Day_Day;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Monthly_Criteria_DayWeekday;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Monthly_Criteria_FstLst;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Criteria_Month;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Criteria_Every;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly_Criteria;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Day_Month;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Day_Every;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly_Day;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly;
        private System.Windows.Forms.Panel pnlRecurring_Pattern_Header;
        private System.Windows.Forms.CheckBox chkApplyRecPattern;
        private System.Windows.Forms.Label lblRecurring_Pattern_Header;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderLeftSpace;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Weekly;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_BodyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_BodyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_BodyRightBrd;
        private System.Windows.Forms.Label lbl_pnl_AdvanceSearch_BodyBottomBrd;
        private System.Windows.Forms.Label lbl_pnl_AdvanceSearch_BodyRightBrd;
        private System.Windows.Forms.Label lbl_pnl_AdvanceSearch_BodyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlAdvanceSearchHeaderRightBrd;
        private System.Windows.Forms.Label lbl_pnlAdvanceSearchHeaderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchHeaderTopBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchHeaderBottomBrd;
        private System.Windows.Forms.Button btn_Recurrencepattern_UP;
        private System.Windows.Forms.Button btn_Recurrencepattern_Down;
        private Janus.Windows.Schedule.Schedule juc_Appointment;
        internal System.Windows.Forms.Label lblSimple_Duration;
        private System.Windows.Forms.NumericUpDown num_Duration;
        private System.Windows.Forms.ComboBox cmbAMPMAll;
        private System.Windows.Forms.ToolStripButton tsb_DayView;
        private System.Windows.Forms.ToolStripButton tsb_WeekView;
        private System.Windows.Forms.ToolStripButton tsb_MonthView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RadioButton rbResourceFilter;
        private System.Windows.Forms.RadioButton rbProblemTypeFilter;
        private System.Windows.Forms.Panel pnlOther2;
        private System.Windows.Forms.Label lbl_pnlOther2BottomBrd;
        private System.Windows.Forms.Panel pnlOther2Header;
        private System.Windows.Forms.Label lblOther2;
        private System.Windows.Forms.Label lbl_pnlOther2LeftBrd;
        private System.Windows.Forms.Label lbl_pnlOther2RightBrd;
        private System.Windows.Forms.Label lbl_lbl_pnlOther2TopBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchHeaderRightBrd;
        private System.Windows.Forms.Label lbl_pnlSimpleSearchHeaderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlAdvanceSearchHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlJanusControlBottomBrd;
        private System.Windows.Forms.Label lbl_pnlJanusControlLeftBrd;
        private System.Windows.Forms.Label lbl_pnlJanusControlRightBrd;
        private System.Windows.Forms.Label lbl_pnlJanusControlTopBrd;
        private System.Windows.Forms.Label lbl_pnlOther2HeaderBottomBrd;
        private System.Windows.Forms.Panel pnl_AppointmentSearchHeader;
        private System.Windows.Forms.Panel pnlAppSearch_Down;
        private System.Windows.Forms.Button btn_AppSearch_Down;
        private System.Windows.Forms.Panel pnlAppSearch_Up;
        private System.Windows.Forms.Button btn_AppSearch_Up;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSearchInTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlAppointmentSearchBody;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlListControl;
        private System.Windows.Forms.ComboBox cmbLocation_Appointment;
        private System.Windows.Forms.ComboBox cmbDepartment_Appointment;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ProgressBar progbarSearch;
        private System.Windows.Forms.Panel pnlProgressbar;
        private System.Windows.Forms.Label lblSearchingMessage;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView trvProvider;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView trvAppointmentType;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnSelectAllAppTypes;
        private System.Windows.Forms.Panel pnlSearchDates;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btn_ShowSearchPnl;
        private System.Windows.Forms.Button btn_ClearProcedure;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.Panel pnlListView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Appointments;
        internal System.Windows.Forms.ToolStripButton tsb_ListView;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbListViewProvider;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClearAllAppTypes;
        private System.Windows.Forms.Button btnClearAllProvider;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}