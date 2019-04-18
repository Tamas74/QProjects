namespace gloAppointmentScheduling
{
    partial class frmSetupSchedule
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpApp_DateTime_EndTime, dtpApp_DateTime_StartTime, dtpApp_DateTime_StartDate, dtpRec_Range_EndBy, dtpRec_Range_StartDate, dtpRec_EndTime, dtpRec_StartTime };
            System.Windows.Forms.Control[] cntControls = { dtpApp_DateTime_EndTime, dtpApp_DateTime_StartTime, dtpApp_DateTime_StartDate, dtpRec_Range_EndBy, dtpRec_Range_StartDate, dtpRec_EndTime, dtpRec_StartTime };

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
                    if (colorDialog1 != null)
                    {

                        colorDialog1.Dispose();
                        colorDialog1 = null;
                    }
                }
                catch
                {
                }
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupSchedule));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Recurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_RemoveRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_ApplyRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_CancelRecurrence = new System.Windows.Forms.ToolStripButton();
            this.pnlBlockedSchedule = new System.Windows.Forms.Panel();
            this.lbl_ProviderAsterix = new System.Windows.Forms.Label();
            this.lbl_pnlBlockedScheduleBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBlockedScheduleTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBlockedScheduleRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBlockedScheduleLeftBrd = new System.Windows.Forms.Label();
            this.btnBrowseBlockedProvider = new System.Windows.Forms.Button();
            this.btnClearBlockedProvider = new System.Windows.Forms.Button();
            this.rbBlockProvider = new System.Windows.Forms.RadioButton();
            this.rbBlockResource = new System.Windows.Forms.RadioButton();
            this.btnClearBlockedResources = new System.Windows.Forms.Button();
            this.cmbBlockType = new System.Windows.Forms.ComboBox();
            this.lblBlock = new System.Windows.Forms.Label();
            this.lblBlockType = new System.Windows.Forms.Label();
            this.pnlCriteria_BlockedProviders = new System.Windows.Forms.Panel();
            this.c1BlockedProviders = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_BlockedProvidersHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd = new System.Windows.Forms.Label();
            this.lblProviders = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedProvidersTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedProvidersRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd = new System.Windows.Forms.Label();
            this.pnlCriteria_BlockedResources = new System.Windows.Forms.Panel();
            this.c1BlockedResources = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_BlockedResources_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblResources = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedResourcesTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedResourcesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd = new System.Windows.Forms.Label();
            this.btnBrowseBlockedResources = new System.Windows.Forms.Button();
            this.pnlResourceSchedule = new System.Windows.Forms.Panel();
            this.lbl_pnlResourceScheduleBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceScheduleTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceScheduleRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceScheduleLeftBrd = new System.Windows.Forms.Label();
            this.btnClearResource = new System.Windows.Forms.Button();
            this.btnBrowseResource = new System.Windows.Forms.Button();
            this.pnlCriteria_Resources = new System.Windows.Forms.Panel();
            this.c1Resources = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_Resources_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCriteria_Resources_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesLeftBrd = new System.Windows.Forms.Label();
            this.pnlProviderSchedule = new System.Windows.Forms.Panel();
            this.btnBrowseProviderResources = new System.Windows.Forms.Button();
            this.btnBrowseProviderProblemType = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_pnlProviderScheduleBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderScheduleTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderScheduleRightBrd = new System.Windows.Forms.Label();
            this.lbl_lbl_pnlProviderScheduleLeftBrd = new System.Windows.Forms.Label();
            this.btnClearProviderResources = new System.Windows.Forms.Button();
            this.btnClearProviderProblemType = new System.Windows.Forms.Button();
            this.pnlCriteria_ProviderResources = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd = new System.Windows.Forms.Label();
            this.c1ProviderResources = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_ProviderResources_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCriteria_ProviderResources_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderResourcesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderResourcesTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd = new System.Windows.Forms.Label();
            this.pnlCriteria_ProviderProblemType = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd = new System.Windows.Forms.Label();
            this.c1ProviderProblemType = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_ProviderProblemType_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCriteria_ProviderProblemType_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblProvider = new System.Windows.Forms.Label();
            this.btnClearProviderUsers = new System.Windows.Forms.Button();
            this.btnBrowseProviderUsers = new System.Windows.Forms.Button();
            this.pnlCriteria_ProviderUsers = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderUsersBottomBrd = new System.Windows.Forms.Label();
            this.c1ProviderUsers = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_ProviderUsers_Header = new System.Windows.Forms.Panel();
            this.lblResourcesUsers = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderUsersRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderUsersTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderUsersLeftBrd = new System.Windows.Forms.Label();
            this.lblSimple_Duration = new System.Windows.Forms.Label();
            this.btnColorCode = new System.Windows.Forms.Button();
            this.numRec_Duration = new System.Windows.Forms.NumericUpDown();
            this.lblSimple_Color = new System.Windows.Forms.Label();
            this.lblSimple_EndTime = new System.Windows.Forms.Label();
            this.lblRec_ColorContainer = new System.Windows.Forms.Label();
            this.dtpRec_EndTime = new System.Windows.Forms.DateTimePicker();
            this.pnlRecurrance = new System.Windows.Forms.Panel();
            this.pnlRecurring_Pattern = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_PatternBottomBrd = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Yearly = new System.Windows.Forms.RadioButton();
            this.pnlRecurring_Pattern_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblRecurring_Pattern_Header = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Monthly = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Weekly = new System.Windows.Forms.RadioButton();
            this.lbl_pnlRecurring_PatternTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_PatternLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_PatternRightBrd = new System.Windows.Forms.Label();
            this.pnlRec_Pattern_Daily = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_DailyLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyTopBrd = new System.Windows.Forms.Label();
            this.numRec_Pattern_Daily_EveryDay = new System.Windows.Forms.NumericUpDown();
            this.lblRec_Pattern_Daily_Days = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Daily_EveryWeekday = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily_EveryDay = new System.Windows.Forms.RadioButton();
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
            this.pnlRec_Pattern_Yearly = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_YearlyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd = new System.Windows.Forms.Label();
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
            this.pnlRecurring_Range = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_RangeBottomBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Range_Header = new System.Windows.Forms.Panel();
            this.lblRec_Range_EndDate = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_Range_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.cmbRec_Range_NoEndDateYear = new System.Windows.Forms.ComboBox();
            this.lblRecurring_Range_Header = new System.Windows.Forms.Label();
            this.rbRec_Range_NoEndDate = new System.Windows.Forms.RadioButton();
            this.dtpRec_Range_StartDate = new System.Windows.Forms.DateTimePicker();
            this.rbRec_Range_EndBy = new System.Windows.Forms.RadioButton();
            this.lblRec_Range_StartDate = new System.Windows.Forms.Label();
            this.rbRec_Range_EndAfterOccurence = new System.Windows.Forms.RadioButton();
            this.dtpRec_Range_EndBy = new System.Windows.Forms.DateTimePicker();
            this.numRec_Range_EndAfterOccurence = new System.Windows.Forms.NumericUpDown();
            this.lblRec_Range_Occurence = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeRightBrd = new System.Windows.Forms.Label();
            this.dtpRec_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lblSimple_StartTime = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.rbBlockedSchedule = new System.Windows.Forms.RadioButton();
            this.txtScheduleNote = new System.Windows.Forms.RichTextBox();
            this.rbResourceSchedule = new System.Windows.Forms.RadioButton();
            this.rbProviderSchedule = new System.Windows.Forms.RadioButton();
            this.pnlScedules = new System.Windows.Forms.Panel();
            this.lblApp_Recurrence_Time = new System.Windows.Forms.Label();
            this.pnlApp_DateTime = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_pnlApp_DateTimeBootomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeLeftBrd = new System.Windows.Forms.Label();
            this.lblDurationUnit = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Date = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lblApp_ColorContainer = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Duration = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Time = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Color = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_StartDate = new System.Windows.Forms.DateTimePicker();
            this.btnApp_DateTime_Color = new System.Windows.Forms.Button();
            this.numApp_DateTime_Duration = new System.Windows.Forms.NumericUpDown();
            this.chkApp_DateTime_IsAllDayEvent = new System.Windows.Forms.CheckBox();
            this.pnlAppointmentHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlAppointmentHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblAppointment = new System.Windows.Forms.Label();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.lbl_pnlScedulesBottomBrd = new System.Windows.Forms.Label();
            this.rbRecurrence = new System.Windows.Forms.RadioButton();
            this.lbl_pnlScedulesTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlScedulesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlScedulesLeftBrd = new System.Windows.Forms.Label();
            this.pnlRecurrenceContainer = new System.Windows.Forms.Panel();
            this.pnlShowRecurrence = new System.Windows.Forms.Panel();
            this.c1Recurrence = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlShowRecurrence_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1 = new System.Windows.Forms.Label();
            this.lblSchedules = new System.Windows.Forms.Label();
            this.lbl_pnlShowRecurrenceBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlShowRecurrenceTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlShowRecurrenceRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlShowRecurrenceLeftBrd = new System.Windows.Forms.Label();
            this.pnlRecurrenceContainer_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblRecurrence = new System.Windows.Forms.Label();
            this.lbl_pnlRecurrenceContainerBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurrenceContainerTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurrenceContainerRightBrd = new System.Windows.Forms.Label();
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd = new System.Windows.Forms.Label();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.pnlListControl_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlListControl_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblList = new System.Windows.Forms.Label();
            this.lbl_pnlListControlBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlListControlTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlListControlRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlListControlLeftBrd = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlBlockedSchedule.SuspendLayout();
            this.pnlCriteria_BlockedProviders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BlockedProviders)).BeginInit();
            this.pnlCriteria_BlockedProvidersHeader.SuspendLayout();
            this.pnlCriteria_BlockedResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BlockedResources)).BeginInit();
            this.pnlCriteria_BlockedResources_Header.SuspendLayout();
            this.pnlResourceSchedule.SuspendLayout();
            this.pnlCriteria_Resources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Resources)).BeginInit();
            this.pnlCriteria_Resources_Header.SuspendLayout();
            this.pnlProviderSchedule.SuspendLayout();
            this.pnlCriteria_ProviderResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderResources)).BeginInit();
            this.pnlCriteria_ProviderResources_Header.SuspendLayout();
            this.pnlCriteria_ProviderProblemType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderProblemType)).BeginInit();
            this.pnlCriteria_ProviderProblemType_Header.SuspendLayout();
            this.pnlCriteria_ProviderUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderUsers)).BeginInit();
            this.pnlCriteria_ProviderUsers_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Duration)).BeginInit();
            this.pnlRecurrance.SuspendLayout();
            this.pnlRecurring_Pattern.SuspendLayout();
            this.pnlRecurring_Pattern_Header.SuspendLayout();
            this.pnlRec_Pattern_Daily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).BeginInit();
            this.pnlRec_Pattern_Weekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).BeginInit();
            this.pnlRec_Pattern_Yearly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).BeginInit();
            this.pnlRec_Pattern_Monthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).BeginInit();
            this.pnlRecurring_Range.SuspendLayout();
            this.pnlRecurring_Range_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).BeginInit();
            this.pnlScedules.SuspendLayout();
            this.pnlApp_DateTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).BeginInit();
            this.pnlAppointmentHeader.SuspendLayout();
            this.pnlRecurrenceContainer.SuspendLayout();
            this.pnlShowRecurrence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Recurrence)).BeginInit();
            this.pnlShowRecurrence_Header.SuspendLayout();
            this.pnlRecurrenceContainer_Header.SuspendLayout();
            this.pnlListControl.SuspendLayout();
            this.pnlListControl_Header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(725, 55);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Recurrence,
            this.tsb_RemoveRecurrence,
            this.tsb_ApplyRecurrence,
            this.tsb_ShowRecurrence,
            this.tsb_OK,
            this.tsb_Cancel,
            this.tsb_CancelRecurrence});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(725, 53);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Recurrence
            // 
            this.tsb_Recurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Recurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Recurrence.Image")));
            this.tsb_Recurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Recurrence.Name = "tsb_Recurrence";
            this.tsb_Recurrence.Size = new System.Drawing.Size(79, 50);
            this.tsb_Recurrence.Tag = "Recurrence";
            this.tsb_Recurrence.Text = "&Recurrence";
            this.tsb_Recurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Recurrence.ToolTipText = "Show Recurrence";
            // 
            // tsb_RemoveRecurrence
            // 
            this.tsb_RemoveRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RemoveRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RemoveRecurrence.Image")));
            this.tsb_RemoveRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RemoveRecurrence.Name = "tsb_RemoveRecurrence";
            this.tsb_RemoveRecurrence.Size = new System.Drawing.Size(64, 50);
            this.tsb_RemoveRecurrence.Tag = "RemoveRecurrence";
            this.tsb_RemoveRecurrence.Text = "Re&move ";
            this.tsb_RemoveRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RemoveRecurrence.ToolTipText = "Remove Recurrence";
            // 
            // tsb_ApplyRecurrence
            // 
            this.tsb_ApplyRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ApplyRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ApplyRecurrence.Image")));
            this.tsb_ApplyRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ApplyRecurrence.Name = "tsb_ApplyRecurrence";
            this.tsb_ApplyRecurrence.Size = new System.Drawing.Size(50, 50);
            this.tsb_ApplyRecurrence.Tag = "ApplyRecurrence";
            this.tsb_ApplyRecurrence.Text = "&Apply ";
            this.tsb_ApplyRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ApplyRecurrence.ToolTipText = "Apply Recurrence";
            // 
            // tsb_ShowRecurrence
            // 
            this.tsb_ShowRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowRecurrence.Image")));
            this.tsb_ShowRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowRecurrence.Name = "tsb_ShowRecurrence";
            this.tsb_ShowRecurrence.Size = new System.Drawing.Size(50, 50);
            this.tsb_ShowRecurrence.Tag = "ShowRecurrence";
            this.tsb_ShowRecurrence.Text = "&Show ";
            this.tsb_ShowRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowRecurrence.ToolTipText = "Show Recurrence";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(70, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = " Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(47, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " &Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.ToolTipText = "Close";
            // 
            // tsb_CancelRecurrence
            // 
            this.tsb_CancelRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_CancelRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CancelRecurrence.Image")));
            this.tsb_CancelRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CancelRecurrence.Name = "tsb_CancelRecurrence";
            this.tsb_CancelRecurrence.Size = new System.Drawing.Size(43, 50);
            this.tsb_CancelRecurrence.Tag = "RecurrenceCancel";
            this.tsb_CancelRecurrence.Text = "&Close";
            this.tsb_CancelRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CancelRecurrence.ToolTipText = "Close";
            // 
            // pnlBlockedSchedule
            // 
            this.pnlBlockedSchedule.Controls.Add(this.lbl_ProviderAsterix);
            this.pnlBlockedSchedule.Controls.Add(this.lbl_pnlBlockedScheduleBottomBrd);
            this.pnlBlockedSchedule.Controls.Add(this.lbl_pnlBlockedScheduleTopBrd);
            this.pnlBlockedSchedule.Controls.Add(this.lbl_pnlBlockedScheduleRightBrd);
            this.pnlBlockedSchedule.Controls.Add(this.lbl_pnlBlockedScheduleLeftBrd);
            this.pnlBlockedSchedule.Controls.Add(this.btnBrowseBlockedProvider);
            this.pnlBlockedSchedule.Controls.Add(this.btnClearBlockedProvider);
            this.pnlBlockedSchedule.Controls.Add(this.rbBlockProvider);
            this.pnlBlockedSchedule.Controls.Add(this.rbBlockResource);
            this.pnlBlockedSchedule.Controls.Add(this.btnClearBlockedResources);
            this.pnlBlockedSchedule.Controls.Add(this.cmbBlockType);
            this.pnlBlockedSchedule.Controls.Add(this.lblBlock);
            this.pnlBlockedSchedule.Controls.Add(this.lblBlockType);
            this.pnlBlockedSchedule.Controls.Add(this.pnlCriteria_BlockedProviders);
            this.pnlBlockedSchedule.Controls.Add(this.pnlCriteria_BlockedResources);
            this.pnlBlockedSchedule.Controls.Add(this.btnBrowseBlockedResources);
            this.pnlBlockedSchedule.Location = new System.Drawing.Point(83, 147);
            this.pnlBlockedSchedule.Name = "pnlBlockedSchedule";
            this.pnlBlockedSchedule.Size = new System.Drawing.Size(608, 424);
            this.pnlBlockedSchedule.TabIndex = 8;
            // 
            // lbl_ProviderAsterix
            // 
            this.lbl_ProviderAsterix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ProviderAsterix.AutoEllipsis = true;
            this.lbl_ProviderAsterix.AutoSize = true;
            this.lbl_ProviderAsterix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ProviderAsterix.ForeColor = System.Drawing.Color.Red;
            this.lbl_ProviderAsterix.Location = new System.Drawing.Point(2, 71);
            this.lbl_ProviderAsterix.Name = "lbl_ProviderAsterix";
            this.lbl_ProviderAsterix.Size = new System.Drawing.Size(14, 14);
            this.lbl_ProviderAsterix.TabIndex = 191;
            this.lbl_ProviderAsterix.Text = "*";
            // 
            // lbl_pnlBlockedScheduleBottomBrd
            // 
            this.lbl_pnlBlockedScheduleBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBlockedScheduleBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBlockedScheduleBottomBrd.Location = new System.Drawing.Point(1, 423);
            this.lbl_pnlBlockedScheduleBottomBrd.Name = "lbl_pnlBlockedScheduleBottomBrd";
            this.lbl_pnlBlockedScheduleBottomBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlBlockedScheduleBottomBrd.TabIndex = 186;
            // 
            // lbl_pnlBlockedScheduleTopBrd
            // 
            this.lbl_pnlBlockedScheduleTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBlockedScheduleTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlBlockedScheduleTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlBlockedScheduleTopBrd.Name = "lbl_pnlBlockedScheduleTopBrd";
            this.lbl_pnlBlockedScheduleTopBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlBlockedScheduleTopBrd.TabIndex = 185;
            // 
            // lbl_pnlBlockedScheduleRightBrd
            // 
            this.lbl_pnlBlockedScheduleRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBlockedScheduleRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlBlockedScheduleRightBrd.Location = new System.Drawing.Point(607, 0);
            this.lbl_pnlBlockedScheduleRightBrd.Name = "lbl_pnlBlockedScheduleRightBrd";
            this.lbl_pnlBlockedScheduleRightBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_pnlBlockedScheduleRightBrd.TabIndex = 184;
            this.lbl_pnlBlockedScheduleRightBrd.Text = "label76";
            // 
            // lbl_pnlBlockedScheduleLeftBrd
            // 
            this.lbl_pnlBlockedScheduleLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBlockedScheduleLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlBlockedScheduleLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlBlockedScheduleLeftBrd.Name = "lbl_pnlBlockedScheduleLeftBrd";
            this.lbl_pnlBlockedScheduleLeftBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_pnlBlockedScheduleLeftBrd.TabIndex = 183;
            this.lbl_pnlBlockedScheduleLeftBrd.Text = "label77";
            // 
            // btnBrowseBlockedProvider
            // 
            this.btnBrowseBlockedProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseBlockedProvider.BackgroundImage")));
            this.btnBrowseBlockedProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseBlockedProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseBlockedProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseBlockedProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseBlockedProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseBlockedProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseBlockedProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseBlockedProvider.Image")));
            this.btnBrowseBlockedProvider.Location = new System.Drawing.Point(566, 66);
            this.btnBrowseBlockedProvider.Name = "btnBrowseBlockedProvider";
            this.btnBrowseBlockedProvider.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseBlockedProvider.TabIndex = 3;
            this.btnBrowseBlockedProvider.UseVisualStyleBackColor = true;
            this.btnBrowseBlockedProvider.Click += new System.EventHandler(this.btnBrowseBlockedProvider_Click);
            this.btnBrowseBlockedProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseBlockedProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearBlockedProvider
            // 
            this.btnClearBlockedProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearBlockedProvider.BackgroundImage")));
            this.btnClearBlockedProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearBlockedProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearBlockedProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearBlockedProvider.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearBlockedProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearBlockedProvider.Image")));
            this.btnClearBlockedProvider.Location = new System.Drawing.Point(566, 96);
            this.btnClearBlockedProvider.Name = "btnClearBlockedProvider";
            this.btnClearBlockedProvider.Size = new System.Drawing.Size(24, 24);
            this.btnClearBlockedProvider.TabIndex = 5;
            this.btnClearBlockedProvider.UseVisualStyleBackColor = false;
            this.btnClearBlockedProvider.Click += new System.EventHandler(this.btnClearBlockedProvider_Click);
            this.btnClearBlockedProvider.Leave += new System.EventHandler(this.btnClearBlockedProvider_Leave);
            this.btnClearBlockedProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearBlockedProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // rbBlockProvider
            // 
            this.rbBlockProvider.AutoSize = true;
            this.rbBlockProvider.Checked = true;
            this.rbBlockProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBlockProvider.Location = new System.Drawing.Point(98, 39);
            this.rbBlockProvider.Name = "rbBlockProvider";
            this.rbBlockProvider.Size = new System.Drawing.Size(76, 18);
            this.rbBlockProvider.TabIndex = 1;
            this.rbBlockProvider.TabStop = true;
            this.rbBlockProvider.Text = "Provider";
            this.rbBlockProvider.UseVisualStyleBackColor = true;
            this.rbBlockProvider.CheckedChanged += new System.EventHandler(this.rbBlockProvider_CheckedChanged);
            // 
            // rbBlockResource
            // 
            this.rbBlockResource.AutoSize = true;
            this.rbBlockResource.Location = new System.Drawing.Point(196, 39);
            this.rbBlockResource.Name = "rbBlockResource";
            this.rbBlockResource.Size = new System.Drawing.Size(75, 18);
            this.rbBlockResource.TabIndex = 2;
            this.rbBlockResource.Text = "Resource";
            this.rbBlockResource.UseVisualStyleBackColor = true;
            // 
            // btnClearBlockedResources
            // 
            this.btnClearBlockedResources.BackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedResources.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearBlockedResources.BackgroundImage")));
            this.btnClearBlockedResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearBlockedResources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearBlockedResources.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedResources.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearBlockedResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearBlockedResources.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearBlockedResources.Image = ((System.Drawing.Image)(resources.GetObject("btnClearBlockedResources.Image")));
            this.btnClearBlockedResources.Location = new System.Drawing.Point(566, 96);
            this.btnClearBlockedResources.Name = "btnClearBlockedResources";
            this.btnClearBlockedResources.Size = new System.Drawing.Size(24, 24);
            this.btnClearBlockedResources.TabIndex = 8;
            this.btnClearBlockedResources.UseVisualStyleBackColor = false;
            this.btnClearBlockedResources.Click += new System.EventHandler(this.btnClearBlockedResources_Click);
            this.btnClearBlockedResources.Leave += new System.EventHandler(this.btnClearBlockedResources_Leave);
            this.btnClearBlockedResources.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearBlockedResources.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbBlockType
            // 
            this.cmbBlockType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBlockType.ForeColor = System.Drawing.Color.Black;
            this.cmbBlockType.FormattingEnabled = true;
            this.cmbBlockType.Location = new System.Drawing.Point(99, 12);
            this.cmbBlockType.Name = "cmbBlockType";
            this.cmbBlockType.Size = new System.Drawing.Size(235, 22);
            this.cmbBlockType.TabIndex = 0;
            this.cmbBlockType.SelectionChangeCommitted += new System.EventHandler(this.cmbBlockType_SelectionChangeCommitted);
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Location = new System.Drawing.Point(54, 40);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(43, 14);
            this.lblBlock.TabIndex = 173;
            this.lblBlock.Text = "Block :";
            // 
            // lblBlockType
            // 
            this.lblBlockType.AutoSize = true;
            this.lblBlockType.Location = new System.Drawing.Point(22, 15);
            this.lblBlockType.Name = "lblBlockType";
            this.lblBlockType.Size = new System.Drawing.Size(75, 14);
            this.lblBlockType.TabIndex = 173;
            this.lblBlockType.Text = "Block Type :";
            // 
            // pnlCriteria_BlockedProviders
            // 
            this.pnlCriteria_BlockedProviders.Controls.Add(this.c1BlockedProviders);
            this.pnlCriteria_BlockedProviders.Controls.Add(this.pnlCriteria_BlockedProvidersHeader);
            this.pnlCriteria_BlockedProviders.Controls.Add(this.lbl_pnlCriteria_BlockedProvidersBottomBrd);
            this.pnlCriteria_BlockedProviders.Controls.Add(this.lbl_pnlCriteria_BlockedProvidersTopBrd);
            this.pnlCriteria_BlockedProviders.Controls.Add(this.lbl_pnlCriteria_BlockedProvidersRightBrd);
            this.pnlCriteria_BlockedProviders.Controls.Add(this.lbl_pnlCriteria_BlockedProvidersLeftBrd);
            this.pnlCriteria_BlockedProviders.Location = new System.Drawing.Point(17, 67);
            this.pnlCriteria_BlockedProviders.Name = "pnlCriteria_BlockedProviders";
            this.pnlCriteria_BlockedProviders.Size = new System.Drawing.Size(544, 347);
            this.pnlCriteria_BlockedProviders.TabIndex = 180;
            // 
            // c1BlockedProviders
            // 
            this.c1BlockedProviders.AllowEditing = false;
            this.c1BlockedProviders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1BlockedProviders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BlockedProviders.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1BlockedProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BlockedProviders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BlockedProviders.Location = new System.Drawing.Point(1, 27);
            this.c1BlockedProviders.Name = "c1BlockedProviders";
            this.c1BlockedProviders.Rows.Count = 5;
            this.c1BlockedProviders.Rows.DefaultSize = 21;
            this.c1BlockedProviders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BlockedProviders.Size = new System.Drawing.Size(542, 319);
            this.c1BlockedProviders.StyleInfo = resources.GetString("c1BlockedProviders.StyleInfo");
            this.c1BlockedProviders.TabIndex = 4;
            // 
            // pnlCriteria_BlockedProvidersHeader
            // 
            this.pnlCriteria_BlockedProvidersHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_BlockedProvidersHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_BlockedProvidersHeader.Controls.Add(this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd);
            this.pnlCriteria_BlockedProvidersHeader.Controls.Add(this.lblProviders);
            this.pnlCriteria_BlockedProvidersHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_BlockedProvidersHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_BlockedProvidersHeader.Name = "pnlCriteria_BlockedProvidersHeader";
            this.pnlCriteria_BlockedProvidersHeader.Size = new System.Drawing.Size(542, 26);
            this.pnlCriteria_BlockedProvidersHeader.TabIndex = 137;
            // 
            // lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd
            // 
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.Location = new System.Drawing.Point(0, 25);
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.Name = "lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd";
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd.TabIndex = 190;
            // 
            // lblProviders
            // 
            this.lblProviders.BackColor = System.Drawing.Color.Transparent;
            this.lblProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviders.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProviders.Location = new System.Drawing.Point(0, 0);
            this.lblProviders.Name = "lblProviders";
            this.lblProviders.Size = new System.Drawing.Size(542, 26);
            this.lblProviders.TabIndex = 0;
            this.lblProviders.Text = " Providers";
            this.lblProviders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_BlockedProvidersBottomBrd
            // 
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.Location = new System.Drawing.Point(1, 346);
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.Name = "lbl_pnlCriteria_BlockedProvidersBottomBrd";
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedProvidersBottomBrd.TabIndex = 190;
            // 
            // lbl_pnlCriteria_BlockedProvidersTopBrd
            // 
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.Name = "lbl_pnlCriteria_BlockedProvidersTopBrd";
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedProvidersTopBrd.TabIndex = 189;
            // 
            // lbl_pnlCriteria_BlockedProvidersRightBrd
            // 
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.Location = new System.Drawing.Point(543, 0);
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.Name = "lbl_pnlCriteria_BlockedProvidersRightBrd";
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.Size = new System.Drawing.Size(1, 347);
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.TabIndex = 188;
            this.lbl_pnlCriteria_BlockedProvidersRightBrd.Text = "label84";
            // 
            // lbl_pnlCriteria_BlockedProvidersLeftBrd
            // 
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.Name = "lbl_pnlCriteria_BlockedProvidersLeftBrd";
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.Size = new System.Drawing.Size(1, 347);
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.TabIndex = 187;
            this.lbl_pnlCriteria_BlockedProvidersLeftBrd.Text = "label85";
            // 
            // pnlCriteria_BlockedResources
            // 
            this.pnlCriteria_BlockedResources.Controls.Add(this.c1BlockedResources);
            this.pnlCriteria_BlockedResources.Controls.Add(this.pnlCriteria_BlockedResources_Header);
            this.pnlCriteria_BlockedResources.Controls.Add(this.lbl_pnlCriteria_BlockedResourcesBottomBrd);
            this.pnlCriteria_BlockedResources.Controls.Add(this.lbl_pnlCriteria_BlockedResourcesTopBrd);
            this.pnlCriteria_BlockedResources.Controls.Add(this.lbl_pnlCriteria_BlockedResourcesRightBrd);
            this.pnlCriteria_BlockedResources.Controls.Add(this.lbl_pnlCriteria_BlockedResourcesLeftBrd);
            this.pnlCriteria_BlockedResources.Location = new System.Drawing.Point(17, 67);
            this.pnlCriteria_BlockedResources.Name = "pnlCriteria_BlockedResources";
            this.pnlCriteria_BlockedResources.Size = new System.Drawing.Size(544, 347);
            this.pnlCriteria_BlockedResources.TabIndex = 167;
            // 
            // c1BlockedResources
            // 
            this.c1BlockedResources.AllowEditing = false;
            this.c1BlockedResources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1BlockedResources.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BlockedResources.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1BlockedResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BlockedResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BlockedResources.Location = new System.Drawing.Point(1, 27);
            this.c1BlockedResources.Name = "c1BlockedResources";
            this.c1BlockedResources.Rows.Count = 5;
            this.c1BlockedResources.Rows.DefaultSize = 21;
            this.c1BlockedResources.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BlockedResources.Size = new System.Drawing.Size(542, 319);
            this.c1BlockedResources.StyleInfo = resources.GetString("c1BlockedResources.StyleInfo");
            this.c1BlockedResources.TabIndex = 7;
            // 
            // pnlCriteria_BlockedResources_Header
            // 
            this.pnlCriteria_BlockedResources_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_BlockedResources_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_BlockedResources_Header.Controls.Add(this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd);
            this.pnlCriteria_BlockedResources_Header.Controls.Add(this.lblResources);
            this.pnlCriteria_BlockedResources_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_BlockedResources_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_BlockedResources_Header.Name = "pnlCriteria_BlockedResources_Header";
            this.pnlCriteria_BlockedResources_Header.Size = new System.Drawing.Size(542, 26);
            this.pnlCriteria_BlockedResources_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_BlockedResources_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.Location = new System.Drawing.Point(0, 25);
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.Name = "lbl_pnlCriteria_BlockedResources_HeaderBottomBrd";
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedResources_HeaderBottomBrd.TabIndex = 190;
            // 
            // lblResources
            // 
            this.lblResources.BackColor = System.Drawing.Color.Transparent;
            this.lblResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblResources.Location = new System.Drawing.Point(0, 0);
            this.lblResources.Name = "lblResources";
            this.lblResources.Size = new System.Drawing.Size(542, 26);
            this.lblResources.TabIndex = 0;
            this.lblResources.Text = " Resources";
            this.lblResources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_BlockedResourcesBottomBrd
            // 
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.Location = new System.Drawing.Point(1, 346);
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.Name = "lbl_pnlCriteria_BlockedResourcesBottomBrd";
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedResourcesBottomBrd.TabIndex = 190;
            // 
            // lbl_pnlCriteria_BlockedResourcesTopBrd
            // 
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.Name = "lbl_pnlCriteria_BlockedResourcesTopBrd";
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_BlockedResourcesTopBrd.TabIndex = 189;
            // 
            // lbl_pnlCriteria_BlockedResourcesRightBrd
            // 
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.Location = new System.Drawing.Point(543, 0);
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.Name = "lbl_pnlCriteria_BlockedResourcesRightBrd";
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.Size = new System.Drawing.Size(1, 347);
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.TabIndex = 188;
            this.lbl_pnlCriteria_BlockedResourcesRightBrd.Text = "label80";
            // 
            // lbl_pnlCriteria_BlockedResourcesLeftBrd
            // 
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.Name = "lbl_pnlCriteria_BlockedResourcesLeftBrd";
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.Size = new System.Drawing.Size(1, 347);
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.TabIndex = 187;
            this.lbl_pnlCriteria_BlockedResourcesLeftBrd.Text = "label81";
            // 
            // btnBrowseBlockedResources
            // 
            this.btnBrowseBlockedResources.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseBlockedResources.BackgroundImage")));
            this.btnBrowseBlockedResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseBlockedResources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseBlockedResources.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseBlockedResources.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseBlockedResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseBlockedResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseBlockedResources.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseBlockedResources.Image")));
            this.btnBrowseBlockedResources.Location = new System.Drawing.Point(566, 66);
            this.btnBrowseBlockedResources.Name = "btnBrowseBlockedResources";
            this.btnBrowseBlockedResources.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseBlockedResources.TabIndex = 192;
            this.btnBrowseBlockedResources.UseVisualStyleBackColor = true;
            this.btnBrowseBlockedResources.Click += new System.EventHandler(this.btnBrowseBlockedResources_Click);
            this.btnBrowseBlockedResources.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseBlockedResources.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlResourceSchedule
            // 
            this.pnlResourceSchedule.Controls.Add(this.lbl_pnlResourceScheduleBottomBrd);
            this.pnlResourceSchedule.Controls.Add(this.lbl_pnlResourceScheduleTopBrd);
            this.pnlResourceSchedule.Controls.Add(this.lbl_pnlResourceScheduleRightBrd);
            this.pnlResourceSchedule.Controls.Add(this.lbl_pnlResourceScheduleLeftBrd);
            this.pnlResourceSchedule.Controls.Add(this.btnClearResource);
            this.pnlResourceSchedule.Controls.Add(this.btnBrowseResource);
            this.pnlResourceSchedule.Controls.Add(this.pnlCriteria_Resources);
            this.pnlResourceSchedule.Location = new System.Drawing.Point(83, 147);
            this.pnlResourceSchedule.Name = "pnlResourceSchedule";
            this.pnlResourceSchedule.Size = new System.Drawing.Size(608, 424);
            this.pnlResourceSchedule.TabIndex = 9;
            // 
            // lbl_pnlResourceScheduleBottomBrd
            // 
            this.lbl_pnlResourceScheduleBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceScheduleBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlResourceScheduleBottomBrd.Location = new System.Drawing.Point(1, 423);
            this.lbl_pnlResourceScheduleBottomBrd.Name = "lbl_pnlResourceScheduleBottomBrd";
            this.lbl_pnlResourceScheduleBottomBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlResourceScheduleBottomBrd.TabIndex = 175;
            // 
            // lbl_pnlResourceScheduleTopBrd
            // 
            this.lbl_pnlResourceScheduleTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceScheduleTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlResourceScheduleTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlResourceScheduleTopBrd.Name = "lbl_pnlResourceScheduleTopBrd";
            this.lbl_pnlResourceScheduleTopBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlResourceScheduleTopBrd.TabIndex = 174;
            // 
            // lbl_pnlResourceScheduleRightBrd
            // 
            this.lbl_pnlResourceScheduleRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceScheduleRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlResourceScheduleRightBrd.Location = new System.Drawing.Point(607, 0);
            this.lbl_pnlResourceScheduleRightBrd.Name = "lbl_pnlResourceScheduleRightBrd";
            this.lbl_pnlResourceScheduleRightBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_pnlResourceScheduleRightBrd.TabIndex = 173;
            this.lbl_pnlResourceScheduleRightBrd.Text = "label72";
            // 
            // lbl_pnlResourceScheduleLeftBrd
            // 
            this.lbl_pnlResourceScheduleLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceScheduleLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlResourceScheduleLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlResourceScheduleLeftBrd.Name = "lbl_pnlResourceScheduleLeftBrd";
            this.lbl_pnlResourceScheduleLeftBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_pnlResourceScheduleLeftBrd.TabIndex = 172;
            this.lbl_pnlResourceScheduleLeftBrd.Text = "label73";
            // 
            // btnClearResource
            // 
            this.btnClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btnClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearResource.BackgroundImage")));
            this.btnClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearResource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearResource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btnClearResource.Image")));
            this.btnClearResource.Location = new System.Drawing.Point(569, 40);
            this.btnClearResource.Name = "btnClearResource";
            this.btnClearResource.Size = new System.Drawing.Size(24, 24);
            this.btnClearResource.TabIndex = 171;
            this.btnClearResource.UseVisualStyleBackColor = false;
            this.btnClearResource.Click += new System.EventHandler(this.btnClearResource_Click);
            this.btnClearResource.Leave += new System.EventHandler(this.btnClearResource_Leave);
            this.btnClearResource.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearResource.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseResource
            // 
            this.btnBrowseResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseResource.BackgroundImage")));
            this.btnBrowseResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseResource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseResource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseResource.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseResource.Image")));
            this.btnBrowseResource.Location = new System.Drawing.Point(569, 11);
            this.btnBrowseResource.Name = "btnBrowseResource";
            this.btnBrowseResource.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseResource.TabIndex = 170;
            this.btnBrowseResource.UseVisualStyleBackColor = true;
            this.btnBrowseResource.Click += new System.EventHandler(this.btnBrowseResource_Click);
            this.btnBrowseResource.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseResource.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlCriteria_Resources
            // 
            this.pnlCriteria_Resources.Controls.Add(this.c1Resources);
            this.pnlCriteria_Resources.Controls.Add(this.pnlCriteria_Resources_Header);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesBottomBrd);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesTopBrd);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesRightBrd);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesLeftBrd);
            this.pnlCriteria_Resources.Location = new System.Drawing.Point(20, 12);
            this.pnlCriteria_Resources.Name = "pnlCriteria_Resources";
            this.pnlCriteria_Resources.Size = new System.Drawing.Size(544, 406);
            this.pnlCriteria_Resources.TabIndex = 167;
            // 
            // c1Resources
            // 
            this.c1Resources.AllowEditing = false;
            this.c1Resources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Resources.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Resources.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1Resources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Resources.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1Resources.Location = new System.Drawing.Point(1, 25);
            this.c1Resources.Name = "c1Resources";
            this.c1Resources.Rows.Count = 5;
            this.c1Resources.Rows.DefaultSize = 21;
            this.c1Resources.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Resources.Size = new System.Drawing.Size(542, 380);
            this.c1Resources.StyleInfo = resources.GetString("c1Resources.StyleInfo");
            this.c1Resources.TabIndex = 138;
            // 
            // pnlCriteria_Resources_Header
            // 
            this.pnlCriteria_Resources_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_Resources_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_Resources_Header.Controls.Add(this.lbl_pnlCriteria_Resources_HeaderBottomBrd);
            this.pnlCriteria_Resources_Header.Controls.Add(this.lblCriteria_Resources_Header);
            this.pnlCriteria_Resources_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_Resources_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_Resources_Header.Name = "pnlCriteria_Resources_Header";
            this.pnlCriteria_Resources_Header.Size = new System.Drawing.Size(542, 24);
            this.pnlCriteria_Resources_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_Resources_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Location = new System.Drawing.Point(0, 23);
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Name = "lbl_pnlCriteria_Resources_HeaderBottomBrd";
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.TabIndex = 191;
            // 
            // lblCriteria_Resources_Header
            // 
            this.lblCriteria_Resources_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_Resources_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_Resources_Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_Resources_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_Resources_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_Resources_Header.Name = "lblCriteria_Resources_Header";
            this.lblCriteria_Resources_Header.Size = new System.Drawing.Size(542, 24);
            this.lblCriteria_Resources_Header.TabIndex = 0;
            this.lblCriteria_Resources_Header.Text = " Resources";
            this.lblCriteria_Resources_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ResourcesBottomBrd
            // 
            this.lbl_pnlCriteria_ResourcesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ResourcesBottomBrd.Location = new System.Drawing.Point(1, 405);
            this.lbl_pnlCriteria_ResourcesBottomBrd.Name = "lbl_pnlCriteria_ResourcesBottomBrd";
            this.lbl_pnlCriteria_ResourcesBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ResourcesBottomBrd.TabIndex = 190;
            // 
            // lbl_pnlCriteria_ResourcesTopBrd
            // 
            this.lbl_pnlCriteria_ResourcesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ResourcesTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlCriteria_ResourcesTopBrd.Name = "lbl_pnlCriteria_ResourcesTopBrd";
            this.lbl_pnlCriteria_ResourcesTopBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ResourcesTopBrd.TabIndex = 189;
            // 
            // lbl_pnlCriteria_ResourcesRightBrd
            // 
            this.lbl_pnlCriteria_ResourcesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ResourcesRightBrd.Location = new System.Drawing.Point(543, 0);
            this.lbl_pnlCriteria_ResourcesRightBrd.Name = "lbl_pnlCriteria_ResourcesRightBrd";
            this.lbl_pnlCriteria_ResourcesRightBrd.Size = new System.Drawing.Size(1, 406);
            this.lbl_pnlCriteria_ResourcesRightBrd.TabIndex = 188;
            this.lbl_pnlCriteria_ResourcesRightBrd.Text = "label88";
            // 
            // lbl_pnlCriteria_ResourcesLeftBrd
            // 
            this.lbl_pnlCriteria_ResourcesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ResourcesLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ResourcesLeftBrd.Name = "lbl_pnlCriteria_ResourcesLeftBrd";
            this.lbl_pnlCriteria_ResourcesLeftBrd.Size = new System.Drawing.Size(1, 406);
            this.lbl_pnlCriteria_ResourcesLeftBrd.TabIndex = 187;
            this.lbl_pnlCriteria_ResourcesLeftBrd.Text = "label89";
            // 
            // pnlProviderSchedule
            // 
            this.pnlProviderSchedule.Controls.Add(this.btnBrowseProviderResources);
            this.pnlProviderSchedule.Controls.Add(this.btnBrowseProviderProblemType);
            this.pnlProviderSchedule.Controls.Add(this.label7);
            this.pnlProviderSchedule.Controls.Add(this.lbl_pnlProviderScheduleBottomBrd);
            this.pnlProviderSchedule.Controls.Add(this.lbl_pnlProviderScheduleTopBrd);
            this.pnlProviderSchedule.Controls.Add(this.lbl_pnlProviderScheduleRightBrd);
            this.pnlProviderSchedule.Controls.Add(this.lbl_lbl_pnlProviderScheduleLeftBrd);
            this.pnlProviderSchedule.Controls.Add(this.btnClearProviderResources);
            this.pnlProviderSchedule.Controls.Add(this.btnClearProviderProblemType);
            this.pnlProviderSchedule.Controls.Add(this.pnlCriteria_ProviderResources);
            this.pnlProviderSchedule.Controls.Add(this.pnlCriteria_ProviderProblemType);
            this.pnlProviderSchedule.Controls.Add(this.cmbProvider);
            this.pnlProviderSchedule.Controls.Add(this.lblProvider);
            this.pnlProviderSchedule.Location = new System.Drawing.Point(83, 147);
            this.pnlProviderSchedule.Name = "pnlProviderSchedule";
            this.pnlProviderSchedule.Size = new System.Drawing.Size(608, 424);
            this.pnlProviderSchedule.TabIndex = 7;
            // 
            // btnBrowseProviderResources
            // 
            this.btnBrowseProviderResources.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProviderResources.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderResources.BackgroundImage")));
            this.btnBrowseProviderResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProviderResources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProviderResources.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseProviderResources.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseProviderResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProviderResources.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderResources.Image")));
            this.btnBrowseProviderResources.Location = new System.Drawing.Point(567, 224);
            this.btnBrowseProviderResources.Name = "btnBrowseProviderResources";
            this.btnBrowseProviderResources.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseProviderResources.TabIndex = 187;
            this.btnBrowseProviderResources.UseVisualStyleBackColor = false;
            this.btnBrowseProviderResources.Click += new System.EventHandler(this.btnBrowseProviderResources_Click);
            this.btnBrowseProviderResources.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseProviderResources.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseProviderProblemType
            // 
            this.btnBrowseProviderProblemType.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProviderProblemType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderProblemType.BackgroundImage")));
            this.btnBrowseProviderProblemType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProviderProblemType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProviderProblemType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseProviderProblemType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseProviderProblemType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProviderProblemType.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderProblemType.Image")));
            this.btnBrowseProviderProblemType.Location = new System.Drawing.Point(567, 35);
            this.btnBrowseProviderProblemType.Name = "btnBrowseProviderProblemType";
            this.btnBrowseProviderProblemType.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseProviderProblemType.TabIndex = 186;
            this.btnBrowseProviderProblemType.UseVisualStyleBackColor = false;
            this.btnBrowseProviderProblemType.Click += new System.EventHandler(this.btnBrowseProviderProblemType_Click);
            this.btnBrowseProviderProblemType.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseProviderProblemType.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(15, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 176;
            this.label7.Text = "*";
            // 
            // lbl_pnlProviderScheduleBottomBrd
            // 
            this.lbl_pnlProviderScheduleBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderScheduleBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderScheduleBottomBrd.Location = new System.Drawing.Point(1, 423);
            this.lbl_pnlProviderScheduleBottomBrd.Name = "lbl_pnlProviderScheduleBottomBrd";
            this.lbl_pnlProviderScheduleBottomBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlProviderScheduleBottomBrd.TabIndex = 175;
            // 
            // lbl_pnlProviderScheduleTopBrd
            // 
            this.lbl_pnlProviderScheduleTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderScheduleTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderScheduleTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlProviderScheduleTopBrd.Name = "lbl_pnlProviderScheduleTopBrd";
            this.lbl_pnlProviderScheduleTopBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlProviderScheduleTopBrd.TabIndex = 174;
            // 
            // lbl_pnlProviderScheduleRightBrd
            // 
            this.lbl_pnlProviderScheduleRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderScheduleRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderScheduleRightBrd.Location = new System.Drawing.Point(607, 0);
            this.lbl_pnlProviderScheduleRightBrd.Name = "lbl_pnlProviderScheduleRightBrd";
            this.lbl_pnlProviderScheduleRightBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_pnlProviderScheduleRightBrd.TabIndex = 173;
            this.lbl_pnlProviderScheduleRightBrd.Text = "label68";
            // 
            // lbl_lbl_pnlProviderScheduleLeftBrd
            // 
            this.lbl_lbl_pnlProviderScheduleLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_lbl_pnlProviderScheduleLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lbl_pnlProviderScheduleLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_lbl_pnlProviderScheduleLeftBrd.Name = "lbl_lbl_pnlProviderScheduleLeftBrd";
            this.lbl_lbl_pnlProviderScheduleLeftBrd.Size = new System.Drawing.Size(1, 424);
            this.lbl_lbl_pnlProviderScheduleLeftBrd.TabIndex = 172;
            this.lbl_lbl_pnlProviderScheduleLeftBrd.Text = "label69";
            // 
            // btnClearProviderResources
            // 
            this.btnClearProviderResources.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderResources.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProviderResources.BackgroundImage")));
            this.btnClearProviderResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProviderResources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProviderResources.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderResources.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProviderResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearProviderResources.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProviderResources.Image")));
            this.btnClearProviderResources.Location = new System.Drawing.Point(567, 252);
            this.btnClearProviderResources.Name = "btnClearProviderResources";
            this.btnClearProviderResources.Size = new System.Drawing.Size(24, 24);
            this.btnClearProviderResources.TabIndex = 6;
            this.btnClearProviderResources.UseVisualStyleBackColor = false;
            this.btnClearProviderResources.Click += new System.EventHandler(this.btnClearProviderResources_Click);
            this.btnClearProviderResources.Leave += new System.EventHandler(this.btnClearProviderResources_Leave);
            this.btnClearProviderResources.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProviderResources.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearProviderProblemType
            // 
            this.btnClearProviderProblemType.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderProblemType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProviderProblemType.BackgroundImage")));
            this.btnClearProviderProblemType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProviderProblemType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProviderProblemType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderProblemType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderProblemType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProviderProblemType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearProviderProblemType.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProviderProblemType.Image")));
            this.btnClearProviderProblemType.Location = new System.Drawing.Point(567, 63);
            this.btnClearProviderProblemType.Name = "btnClearProviderProblemType";
            this.btnClearProviderProblemType.Size = new System.Drawing.Size(24, 24);
            this.btnClearProviderProblemType.TabIndex = 3;
            this.btnClearProviderProblemType.UseVisualStyleBackColor = false;
            this.btnClearProviderProblemType.Click += new System.EventHandler(this.btnClearProviderProblemType_Click);
            this.btnClearProviderProblemType.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProviderProblemType.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlCriteria_ProviderResources
            // 
            this.pnlCriteria_ProviderResources.Controls.Add(this.lbl_pnlCriteria_ProviderResourcesBottomBrd);
            this.pnlCriteria_ProviderResources.Controls.Add(this.c1ProviderResources);
            this.pnlCriteria_ProviderResources.Controls.Add(this.pnlCriteria_ProviderResources_Header);
            this.pnlCriteria_ProviderResources.Controls.Add(this.lbl_pnlCriteria_ProviderResourcesRightBrd);
            this.pnlCriteria_ProviderResources.Controls.Add(this.lbl_pnlCriteria_ProviderResourcesTopBrd);
            this.pnlCriteria_ProviderResources.Controls.Add(this.lbl_pnlCriteria_ProviderResourcesLeftBrd);
            this.pnlCriteria_ProviderResources.Location = new System.Drawing.Point(17, 224);
            this.pnlCriteria_ProviderResources.Name = "pnlCriteria_ProviderResources";
            this.pnlCriteria_ProviderResources.Size = new System.Drawing.Size(544, 194);
            this.pnlCriteria_ProviderResources.TabIndex = 167;
            // 
            // lbl_pnlCriteria_ProviderResourcesBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.Location = new System.Drawing.Point(1, 193);
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.Name = "lbl_pnlCriteria_ProviderResourcesBottomBrd";
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ProviderResourcesBottomBrd.TabIndex = 183;
            // 
            // c1ProviderResources
            // 
            this.c1ProviderResources.AllowEditing = false;
            this.c1ProviderResources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProviderResources.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProviderResources.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1ProviderResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProviderResources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProviderResources.Location = new System.Drawing.Point(1, 24);
            this.c1ProviderResources.Name = "c1ProviderResources";
            this.c1ProviderResources.Rows.Count = 5;
            this.c1ProviderResources.Rows.DefaultSize = 21;
            this.c1ProviderResources.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProviderResources.Size = new System.Drawing.Size(542, 170);
            this.c1ProviderResources.StyleInfo = resources.GetString("c1ProviderResources.StyleInfo");
            this.c1ProviderResources.TabIndex = 5;
            // 
            // pnlCriteria_ProviderResources_Header
            // 
            this.pnlCriteria_ProviderResources_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_ProviderResources_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProviderResources_Header.Controls.Add(this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd);
            this.pnlCriteria_ProviderResources_Header.Controls.Add(this.lblCriteria_ProviderResources_Header);
            this.pnlCriteria_ProviderResources_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderResources_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_ProviderResources_Header.Name = "pnlCriteria_ProviderResources_Header";
            this.pnlCriteria_ProviderResources_Header.Size = new System.Drawing.Size(542, 23);
            this.pnlCriteria_ProviderResources_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_ProviderResources_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.Name = "lbl_pnlCriteria_ProviderResources_HeaderBottomBrd";
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ProviderResources_HeaderBottomBrd.TabIndex = 181;
            // 
            // lblCriteria_ProviderResources_Header
            // 
            this.lblCriteria_ProviderResources_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_ProviderResources_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_ProviderResources_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_ProviderResources_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_ProviderResources_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_ProviderResources_Header.Name = "lblCriteria_ProviderResources_Header";
            this.lblCriteria_ProviderResources_Header.Size = new System.Drawing.Size(542, 23);
            this.lblCriteria_ProviderResources_Header.TabIndex = 0;
            this.lblCriteria_ProviderResources_Header.Text = " Resources";
            this.lblCriteria_ProviderResources_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ProviderResourcesRightBrd
            // 
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.Location = new System.Drawing.Point(543, 1);
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.Name = "lbl_pnlCriteria_ProviderResourcesRightBrd";
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.Size = new System.Drawing.Size(1, 193);
            this.lbl_pnlCriteria_ProviderResourcesRightBrd.TabIndex = 180;
            // 
            // lbl_pnlCriteria_ProviderResourcesTopBrd
            // 
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.Name = "lbl_pnlCriteria_ProviderResourcesTopBrd";
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.Size = new System.Drawing.Size(543, 1);
            this.lbl_pnlCriteria_ProviderResourcesTopBrd.TabIndex = 182;
            // 
            // lbl_pnlCriteria_ProviderResourcesLeftBrd
            // 
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.Name = "lbl_pnlCriteria_ProviderResourcesLeftBrd";
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.Size = new System.Drawing.Size(1, 194);
            this.lbl_pnlCriteria_ProviderResourcesLeftBrd.TabIndex = 184;
            // 
            // pnlCriteria_ProviderProblemType
            // 
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.c1ProviderProblemType);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.pnlCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeleftBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeRightBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeTopBrd);
            this.pnlCriteria_ProviderProblemType.Location = new System.Drawing.Point(17, 36);
            this.pnlCriteria_ProviderProblemType.Name = "pnlCriteria_ProviderProblemType";
            this.pnlCriteria_ProviderProblemType.Size = new System.Drawing.Size(544, 178);
            this.pnlCriteria_ProviderProblemType.TabIndex = 165;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Location = new System.Drawing.Point(1, 177);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeBottomBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.TabIndex = 181;
            // 
            // c1ProviderProblemType
            // 
            this.c1ProviderProblemType.AllowEditing = false;
            this.c1ProviderProblemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProviderProblemType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProviderProblemType.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1ProviderProblemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProviderProblemType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProviderProblemType.Location = new System.Drawing.Point(1, 24);
            this.c1ProviderProblemType.Name = "c1ProviderProblemType";
            this.c1ProviderProblemType.Rows.Count = 5;
            this.c1ProviderProblemType.Rows.DefaultSize = 21;
            this.c1ProviderProblemType.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProviderProblemType.Size = new System.Drawing.Size(542, 154);
            this.c1ProviderProblemType.StyleInfo = resources.GetString("c1ProviderProblemType.StyleInfo");
            this.c1ProviderProblemType.TabIndex = 2;
            // 
            // pnlCriteria_ProviderProblemType_Header
            // 
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd);
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lblCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_ProviderProblemType_Header.Name = "pnlCriteria_ProviderProblemType_Header";
            this.pnlCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(542, 23);
            this.pnlCriteria_ProviderProblemType_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Name = "lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd";
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Size = new System.Drawing.Size(542, 1);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.TabIndex = 182;
            // 
            // lblCriteria_ProviderProblemType_Header
            // 
            this.lblCriteria_ProviderProblemType_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_ProviderProblemType_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_ProviderProblemType_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_ProviderProblemType_Header.Name = "lblCriteria_ProviderProblemType_Header";
            this.lblCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(542, 23);
            this.lblCriteria_ProviderProblemType_Header.TabIndex = 0;
            this.lblCriteria_ProviderProblemType_Header.Text = " Problem Types";
            this.lblCriteria_ProviderProblemType_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeleftBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeleftBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.Size = new System.Drawing.Size(1, 177);
            this.lbl_pnlCriteria_ProviderProblemTypeleftBrd.TabIndex = 178;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeRightBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Location = new System.Drawing.Point(543, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeRightBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Size = new System.Drawing.Size(1, 177);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.TabIndex = 179;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeTopBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeTopBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Size = new System.Drawing.Size(544, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.TabIndex = 180;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(87, 7);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(172, 22);
            this.cmbProvider.TabIndex = 0;
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(26, 11);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(59, 14);
            this.lblProvider.TabIndex = 54;
            this.lblProvider.Text = "Provider :";
            // 
            // btnClearProviderUsers
            // 
            this.btnClearProviderUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProviderUsers.BackgroundImage")));
            this.btnClearProviderUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProviderUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProviderUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearProviderUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProviderUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearProviderUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProviderUsers.Image")));
            this.btnClearProviderUsers.Location = new System.Drawing.Point(21, 376);
            this.btnClearProviderUsers.Name = "btnClearProviderUsers";
            this.btnClearProviderUsers.Size = new System.Drawing.Size(23, 23);
            this.btnClearProviderUsers.TabIndex = 171;
            this.btnClearProviderUsers.UseVisualStyleBackColor = false;
            this.btnClearProviderUsers.Visible = false;
            this.btnClearProviderUsers.Click += new System.EventHandler(this.btnClearResource_Click);
            this.btnClearProviderUsers.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProviderUsers.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseProviderUsers
            // 
            this.btnBrowseProviderUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderUsers.BackgroundImage")));
            this.btnBrowseProviderUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProviderUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProviderUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProviderUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProviderUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProviderUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseProviderUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProviderUsers.Image")));
            this.btnBrowseProviderUsers.Location = new System.Drawing.Point(21, 349);
            this.btnBrowseProviderUsers.Name = "btnBrowseProviderUsers";
            this.btnBrowseProviderUsers.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseProviderUsers.TabIndex = 170;
            this.btnBrowseProviderUsers.Text = "...";
            this.btnBrowseProviderUsers.UseVisualStyleBackColor = true;
            this.btnBrowseProviderUsers.Visible = false;
            this.btnBrowseProviderUsers.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseProviderUsers.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlCriteria_ProviderUsers
            // 
            this.pnlCriteria_ProviderUsers.Controls.Add(this.lbl_pnlCriteria_ProviderUsersBottomBrd);
            this.pnlCriteria_ProviderUsers.Controls.Add(this.c1ProviderUsers);
            this.pnlCriteria_ProviderUsers.Controls.Add(this.pnlCriteria_ProviderUsers_Header);
            this.pnlCriteria_ProviderUsers.Controls.Add(this.lbl_pnlCriteria_ProviderUsersRightBrd);
            this.pnlCriteria_ProviderUsers.Controls.Add(this.lbl_pnlCriteria_ProviderUsersTopBrd);
            this.pnlCriteria_ProviderUsers.Controls.Add(this.lbl_pnlCriteria_ProviderUsersLeftBrd);
            this.pnlCriteria_ProviderUsers.Location = new System.Drawing.Point(21, 297);
            this.pnlCriteria_ProviderUsers.Name = "pnlCriteria_ProviderUsers";
            this.pnlCriteria_ProviderUsers.Size = new System.Drawing.Size(38, 48);
            this.pnlCriteria_ProviderUsers.TabIndex = 167;
            this.pnlCriteria_ProviderUsers.Visible = false;
            // 
            // lbl_pnlCriteria_ProviderUsersBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.Location = new System.Drawing.Point(1, 47);
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.Name = "lbl_pnlCriteria_ProviderUsersBottomBrd";
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.Size = new System.Drawing.Size(36, 1);
            this.lbl_pnlCriteria_ProviderUsersBottomBrd.TabIndex = 183;
            // 
            // c1ProviderUsers
            // 
            this.c1ProviderUsers.AllowEditing = false;
            this.c1ProviderUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProviderUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProviderUsers.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1ProviderUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProviderUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProviderUsers.Location = new System.Drawing.Point(1, 24);
            this.c1ProviderUsers.Name = "c1ProviderUsers";
            this.c1ProviderUsers.Rows.Count = 5;
            this.c1ProviderUsers.Rows.DefaultSize = 21;
            this.c1ProviderUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProviderUsers.Size = new System.Drawing.Size(36, 24);
            this.c1ProviderUsers.StyleInfo = resources.GetString("c1ProviderUsers.StyleInfo");
            this.c1ProviderUsers.TabIndex = 138;
            // 
            // pnlCriteria_ProviderUsers_Header
            // 
            this.pnlCriteria_ProviderUsers_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.pnlCriteria_ProviderUsers_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProviderUsers_Header.Controls.Add(this.lblResourcesUsers);
            this.pnlCriteria_ProviderUsers_Header.Controls.Add(this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd);
            this.pnlCriteria_ProviderUsers_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderUsers_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_ProviderUsers_Header.Name = "pnlCriteria_ProviderUsers_Header";
            this.pnlCriteria_ProviderUsers_Header.Size = new System.Drawing.Size(36, 23);
            this.pnlCriteria_ProviderUsers_Header.TabIndex = 137;
            // 
            // lblResourcesUsers
            // 
            this.lblResourcesUsers.BackColor = System.Drawing.Color.Transparent;
            this.lblResourcesUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResourcesUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResourcesUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblResourcesUsers.Location = new System.Drawing.Point(0, 0);
            this.lblResourcesUsers.Name = "lblResourcesUsers";
            this.lblResourcesUsers.Size = new System.Drawing.Size(36, 22);
            this.lblResourcesUsers.TabIndex = 0;
            this.lblResourcesUsers.Text = " Resource Users";
            this.lblResourcesUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.Name = "lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd";
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.Size = new System.Drawing.Size(36, 1);
            this.lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd.TabIndex = 181;
            // 
            // lbl_pnlCriteria_ProviderUsersRightBrd
            // 
            this.lbl_pnlCriteria_ProviderUsersRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderUsersRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ProviderUsersRightBrd.Location = new System.Drawing.Point(37, 1);
            this.lbl_pnlCriteria_ProviderUsersRightBrd.Name = "lbl_pnlCriteria_ProviderUsersRightBrd";
            this.lbl_pnlCriteria_ProviderUsersRightBrd.Size = new System.Drawing.Size(1, 47);
            this.lbl_pnlCriteria_ProviderUsersRightBrd.TabIndex = 180;
            // 
            // lbl_pnlCriteria_ProviderUsersTopBrd
            // 
            this.lbl_pnlCriteria_ProviderUsersTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderUsersTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ProviderUsersTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlCriteria_ProviderUsersTopBrd.Name = "lbl_pnlCriteria_ProviderUsersTopBrd";
            this.lbl_pnlCriteria_ProviderUsersTopBrd.Size = new System.Drawing.Size(37, 1);
            this.lbl_pnlCriteria_ProviderUsersTopBrd.TabIndex = 182;
            // 
            // lbl_pnlCriteria_ProviderUsersLeftBrd
            // 
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.Name = "lbl_pnlCriteria_ProviderUsersLeftBrd";
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.Size = new System.Drawing.Size(1, 48);
            this.lbl_pnlCriteria_ProviderUsersLeftBrd.TabIndex = 184;
            // 
            // lblSimple_Duration
            // 
            this.lblSimple_Duration.AutoSize = true;
            this.lblSimple_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblSimple_Duration.Location = new System.Drawing.Point(552, 37);
            this.lblSimple_Duration.Name = "lblSimple_Duration";
            this.lblSimple_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblSimple_Duration.TabIndex = 0;
            this.lblSimple_Duration.Text = "Duration :";
            this.lblSimple_Duration.Visible = false;
            // 
            // btnColorCode
            // 
            this.btnColorCode.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnColorCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColorCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnColorCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorCode.Image = ((System.Drawing.Image)(resources.GetObject("btnColorCode.Image")));
            this.btnColorCode.Location = new System.Drawing.Point(493, 33);
            this.btnColorCode.Name = "btnColorCode";
            this.btnColorCode.Size = new System.Drawing.Size(23, 23);
            this.btnColorCode.TabIndex = 2;
            this.btnColorCode.UseVisualStyleBackColor = true;
            this.btnColorCode.Click += new System.EventHandler(this.btnColorCode_Click);
            this.btnColorCode.Leave += new System.EventHandler(this.btnColorCode_Leave);
            this.btnColorCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnColorCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // numRec_Duration
            // 
            this.numRec_Duration.Location = new System.Drawing.Point(614, 33);
            this.numRec_Duration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numRec_Duration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Duration.Name = "numRec_Duration";
            this.numRec_Duration.Size = new System.Drawing.Size(59, 22);
            this.numRec_Duration.TabIndex = 3;
            this.numRec_Duration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numRec_Duration.Visible = false;
            this.numRec_Duration.ValueChanged += new System.EventHandler(this.numRec_Duration_ValueChanged);
            // 
            // lblSimple_Color
            // 
            this.lblSimple_Color.AutoSize = true;
            this.lblSimple_Color.BackColor = System.Drawing.Color.Transparent;
            this.lblSimple_Color.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimple_Color.Location = new System.Drawing.Point(418, 37);
            this.lblSimple_Color.Name = "lblSimple_Color";
            this.lblSimple_Color.Size = new System.Drawing.Size(42, 14);
            this.lblSimple_Color.TabIndex = 96;
            this.lblSimple_Color.Text = "Color :";
            this.lblSimple_Color.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSimple_EndTime
            // 
            this.lblSimple_EndTime.AutoSize = true;
            this.lblSimple_EndTime.BackColor = System.Drawing.Color.Transparent;
            this.lblSimple_EndTime.Location = new System.Drawing.Point(226, 37);
            this.lblSimple_EndTime.Name = "lblSimple_EndTime";
            this.lblSimple_EndTime.Size = new System.Drawing.Size(67, 14);
            this.lblSimple_EndTime.TabIndex = 157;
            this.lblSimple_EndTime.Text = "End Time :";
            // 
            // lblRec_ColorContainer
            // 
            this.lblRec_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lblRec_ColorContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRec_ColorContainer.Location = new System.Drawing.Point(464, 35);
            this.lblRec_ColorContainer.Name = "lblRec_ColorContainer";
            this.lblRec_ColorContainer.Size = new System.Drawing.Size(24, 19);
            this.lblRec_ColorContainer.TabIndex = 98;
            // 
            // dtpRec_EndTime
            // 
            this.dtpRec_EndTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpRec_EndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_EndTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpRec_EndTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpRec_EndTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpRec_EndTime.CustomFormat = "hh:mm tt";
            this.dtpRec_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_EndTime.Location = new System.Drawing.Point(296, 33);
            this.dtpRec_EndTime.Name = "dtpRec_EndTime";
            this.dtpRec_EndTime.ShowUpDown = true;
            this.dtpRec_EndTime.Size = new System.Drawing.Size(95, 22);
            this.dtpRec_EndTime.TabIndex = 1;
            this.dtpRec_EndTime.ValueChanged += new System.EventHandler(this.dtpRec_EndTime_ValueChanged);
            // 
            // pnlRecurrance
            // 
            this.pnlRecurrance.Controls.Add(this.pnlRecurring_Pattern);
            this.pnlRecurrance.Controls.Add(this.pnlRecurring_Range);
            this.pnlRecurrance.Location = new System.Drawing.Point(21, 65);
            this.pnlRecurrance.Name = "pnlRecurrance";
            this.pnlRecurrance.Size = new System.Drawing.Size(682, 224);
            this.pnlRecurrance.TabIndex = 149;
            // 
            // pnlRecurring_Pattern
            // 
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternBottomBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Yearly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRecurring_Pattern_Header);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Monthly);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Daily);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Weekly);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternTopBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternLeftBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternRightBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Daily);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Weekly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Yearly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Monthly);
            this.pnlRecurring_Pattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Pattern.Location = new System.Drawing.Point(0, 0);
            this.pnlRecurring_Pattern.Name = "pnlRecurring_Pattern";
            this.pnlRecurring_Pattern.Size = new System.Drawing.Size(682, 139);
            this.pnlRecurring_Pattern.TabIndex = 4;
            // 
            // lbl_pnlRecurring_PatternBottomBrd
            // 
            this.lbl_pnlRecurring_PatternBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_PatternBottomBrd.Location = new System.Drawing.Point(1, 138);
            this.lbl_pnlRecurring_PatternBottomBrd.Name = "lbl_pnlRecurring_PatternBottomBrd";
            this.lbl_pnlRecurring_PatternBottomBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_PatternBottomBrd.TabIndex = 11;
            // 
            // rbRec_Pattern_Yearly
            // 
            this.rbRec_Pattern_Yearly.AutoSize = true;
            this.rbRec_Pattern_Yearly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Yearly.Location = new System.Drawing.Point(13, 112);
            this.rbRec_Pattern_Yearly.Name = "rbRec_Pattern_Yearly";
            this.rbRec_Pattern_Yearly.Size = new System.Drawing.Size(58, 18);
            this.rbRec_Pattern_Yearly.TabIndex = 4;
            this.rbRec_Pattern_Yearly.Text = "Yearly";
            this.rbRec_Pattern_Yearly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_CheckedChanged);
            // 
            // pnlRecurring_Pattern_Header
            // 
            this.pnlRecurring_Pattern_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Pattern_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lbl_pnlRecurring_Pattern_HeaderBottomBrd);
            this.pnlRecurring_Pattern_Header.Controls.Add(this.lblRecurring_Pattern_Header);
            this.pnlRecurring_Pattern_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Pattern_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlRecurring_Pattern_Header.Name = "pnlRecurring_Pattern_Header";
            this.pnlRecurring_Pattern_Header.Size = new System.Drawing.Size(680, 23);
            this.pnlRecurring_Pattern_Header.TabIndex = 5;
            // 
            // lbl_pnlRecurring_Pattern_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Name = "lbl_pnlRecurring_Pattern_HeaderBottomBrd";
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.TabIndex = 10;
            // 
            // lblRecurring_Pattern_Header
            // 
            this.lblRecurring_Pattern_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Pattern_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Pattern_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Pattern_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Pattern_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_Pattern_Header.Name = "lblRecurring_Pattern_Header";
            this.lblRecurring_Pattern_Header.Size = new System.Drawing.Size(680, 23);
            this.lblRecurring_Pattern_Header.TabIndex = 0;
            this.lblRecurring_Pattern_Header.Text = " Recurrence Pattern";
            this.lblRecurring_Pattern_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbRec_Pattern_Monthly
            // 
            this.rbRec_Pattern_Monthly.AutoSize = true;
            this.rbRec_Pattern_Monthly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Monthly.Location = new System.Drawing.Point(13, 83);
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
            this.rbRec_Pattern_Daily.Location = new System.Drawing.Point(13, 28);
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
            this.rbRec_Pattern_Weekly.Location = new System.Drawing.Point(13, 54);
            this.rbRec_Pattern_Weekly.Name = "rbRec_Pattern_Weekly";
            this.rbRec_Pattern_Weekly.Size = new System.Drawing.Size(65, 18);
            this.rbRec_Pattern_Weekly.TabIndex = 2;
            this.rbRec_Pattern_Weekly.Text = "Weekly";
            this.rbRec_Pattern_Weekly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Weekly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Weekly_CheckedChanged);
            // 
            // lbl_pnlRecurring_PatternTopBrd
            // 
            this.lbl_pnlRecurring_PatternTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_PatternTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRecurring_PatternTopBrd.Name = "lbl_pnlRecurring_PatternTopBrd";
            this.lbl_pnlRecurring_PatternTopBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_PatternTopBrd.TabIndex = 9;
            // 
            // lbl_pnlRecurring_PatternLeftBrd
            // 
            this.lbl_pnlRecurring_PatternLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_PatternLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRecurring_PatternLeftBrd.Name = "lbl_pnlRecurring_PatternLeftBrd";
            this.lbl_pnlRecurring_PatternLeftBrd.Size = new System.Drawing.Size(1, 139);
            this.lbl_pnlRecurring_PatternLeftBrd.TabIndex = 12;
            // 
            // lbl_pnlRecurring_PatternRightBrd
            // 
            this.lbl_pnlRecurring_PatternRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_PatternRightBrd.Location = new System.Drawing.Point(681, 0);
            this.lbl_pnlRecurring_PatternRightBrd.Name = "lbl_pnlRecurring_PatternRightBrd";
            this.lbl_pnlRecurring_PatternRightBrd.Size = new System.Drawing.Size(1, 139);
            this.lbl_pnlRecurring_PatternRightBrd.TabIndex = 13;
            // 
            // pnlRec_Pattern_Daily
            // 
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyLeftBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyRightBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyBottomBrdBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyTopBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.numRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lblRec_Pattern_Daily_Days);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryWeekday);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Daily.Location = new System.Drawing.Point(92, 36);
            this.pnlRec_Pattern_Daily.Name = "pnlRec_Pattern_Daily";
            this.pnlRec_Pattern_Daily.Size = new System.Drawing.Size(491, 89);
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
            this.lbl_pnlRec_Pattern_DailyRightBrd.Location = new System.Drawing.Point(490, 1);
            this.lbl_pnlRec_Pattern_DailyRightBrd.Name = "lbl_pnlRec_Pattern_DailyRightBrd";
            this.lbl_pnlRec_Pattern_DailyRightBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlRec_Pattern_DailyRightBrd.TabIndex = 20;
            // 
            // lbl_pnlRec_Pattern_DailyBottomBrdBrd
            // 
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.Location = new System.Drawing.Point(0, 88);
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.Name = "lbl_pnlRec_Pattern_DailyBottomBrdBrd";
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.Size = new System.Drawing.Size(491, 1);
            this.lbl_pnlRec_Pattern_DailyBottomBrdBrd.TabIndex = 19;
            // 
            // lbl_pnlRec_Pattern_DailyTopBrd
            // 
            this.lbl_pnlRec_Pattern_DailyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_DailyTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_DailyTopBrd.Name = "lbl_pnlRec_Pattern_DailyTopBrd";
            this.lbl_pnlRec_Pattern_DailyTopBrd.Size = new System.Drawing.Size(491, 1);
            this.lbl_pnlRec_Pattern_DailyTopBrd.TabIndex = 18;
            // 
            // numRec_Pattern_Daily_EveryDay
            // 
            this.numRec_Pattern_Daily_EveryDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(66, 16);
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
            this.numRec_Pattern_Daily_EveryDay.TabIndex = 1;
            this.numRec_Pattern_Daily_EveryDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Daily_EveryDay.ValueChanged += new System.EventHandler(this.numRec_Pattern_Daily_EveryDay_ValueChanged);
            this.numRec_Pattern_Daily_EveryDay.Leave += new System.EventHandler(this.numRec_Pattern_Daily_EveryDay_Leave);
            // 
            // lblRec_Pattern_Daily_Days
            // 
            this.lblRec_Pattern_Daily_Days.AutoSize = true;
            this.lblRec_Pattern_Daily_Days.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Daily_Days.Location = new System.Drawing.Point(117, 19);
            this.lblRec_Pattern_Daily_Days.Name = "lblRec_Pattern_Daily_Days";
            this.lblRec_Pattern_Daily_Days.Size = new System.Drawing.Size(41, 14);
            this.lblRec_Pattern_Daily_Days.TabIndex = 14;
            this.lblRec_Pattern_Daily_Days.Text = "day(s)";
            // 
            // rbRec_Pattern_Daily_EveryWeekday
            // 
            this.rbRec_Pattern_Daily_EveryWeekday.AutoSize = true;
            this.rbRec_Pattern_Daily_EveryWeekday.Location = new System.Drawing.Point(10, 51);
            this.rbRec_Pattern_Daily_EveryWeekday.Name = "rbRec_Pattern_Daily_EveryWeekday";
            this.rbRec_Pattern_Daily_EveryWeekday.Size = new System.Drawing.Size(108, 18);
            this.rbRec_Pattern_Daily_EveryWeekday.TabIndex = 2;
            this.rbRec_Pattern_Daily_EveryWeekday.Text = "Every weekday";
            this.rbRec_Pattern_Daily_EveryWeekday.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Daily_EveryWeekday.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryWeekday_CheckedChanged);
            this.rbRec_Pattern_Daily_EveryWeekday.Leave += new System.EventHandler(this.rbRec_Pattern_Daily_EveryWeekday_Leave);
            // 
            // rbRec_Pattern_Daily_EveryDay
            // 
            this.rbRec_Pattern_Daily_EveryDay.AutoSize = true;
            this.rbRec_Pattern_Daily_EveryDay.Checked = true;
            this.rbRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(10, 17);
            this.rbRec_Pattern_Daily_EveryDay.Name = "rbRec_Pattern_Daily_EveryDay";
            this.rbRec_Pattern_Daily_EveryDay.Size = new System.Drawing.Size(55, 18);
            this.rbRec_Pattern_Daily_EveryDay.TabIndex = 0;
            this.rbRec_Pattern_Daily_EveryDay.TabStop = true;
            this.rbRec_Pattern_Daily_EveryDay.Text = "Every";
            this.rbRec_Pattern_Daily_EveryDay.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Daily_EveryDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Daily_EveryDay_CheckedChanged);
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
            this.pnlRec_Pattern_Weekly.Location = new System.Drawing.Point(92, 36);
            this.pnlRec_Pattern_Weekly.Name = "pnlRec_Pattern_Weekly";
            this.pnlRec_Pattern_Weekly.Size = new System.Drawing.Size(491, 89);
            this.pnlRec_Pattern_Weekly.TabIndex = 6;
            this.pnlRec_Pattern_Weekly.Visible = false;
            // 
            // lbl_pnlRec_Pattern_WeeklyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Location = new System.Drawing.Point(1, 88);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Name = "lbl_pnlRec_Pattern_WeeklyBottomBrd";
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Size = new System.Drawing.Size(489, 1);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.TabIndex = 88;
            // 
            // lbl_pnlRec_Pattern_WeeklyTopBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Name = "lbl_pnlRec_Pattern_WeeklyTopBrd";
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Size = new System.Drawing.Size(489, 1);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.TabIndex = 87;
            // 
            // lbl_pnlRec_Pattern_WeeklyRightBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Location = new System.Drawing.Point(490, 0);
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
            this.lblRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(140, 10);
            this.lblRec_Pattern_Weekly_WeekOn.Name = "lblRec_Pattern_Weekly_WeekOn";
            this.lblRec_Pattern_Weekly_WeekOn.Size = new System.Drawing.Size(70, 14);
            this.lblRec_Pattern_Weekly_WeekOn.TabIndex = 1;
            this.lblRec_Pattern_Weekly_WeekOn.Text = "week(s) on";
            // 
            // lblRec_Pattern_Weekly_RecurEvery
            // 
            this.lblRec_Pattern_Weekly_RecurEvery.AutoSize = true;
            this.lblRec_Pattern_Weekly_RecurEvery.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Weekly_RecurEvery.Location = new System.Drawing.Point(10, 10);
            this.lblRec_Pattern_Weekly_RecurEvery.Name = "lblRec_Pattern_Weekly_RecurEvery";
            this.lblRec_Pattern_Weekly_RecurEvery.Size = new System.Drawing.Size(72, 14);
            this.lblRec_Pattern_Weekly_RecurEvery.TabIndex = 14;
            this.lblRec_Pattern_Weekly_RecurEvery.Text = "Recur every";
            // 
            // ChkRec_Pattern_Weekly_Saturday
            // 
            this.ChkRec_Pattern_Weekly_Saturday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Saturday.Location = new System.Drawing.Point(176, 64);
            this.ChkRec_Pattern_Weekly_Saturday.Name = "ChkRec_Pattern_Weekly_Saturday";
            this.ChkRec_Pattern_Weekly_Saturday.Size = new System.Drawing.Size(74, 18);
            this.ChkRec_Pattern_Weekly_Saturday.TabIndex = 8;
            this.ChkRec_Pattern_Weekly_Saturday.Text = "Saturday";
            this.ChkRec_Pattern_Weekly_Saturday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Saturday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Saturday_CheckedChanged);
            this.ChkRec_Pattern_Weekly_Saturday.Leave += new System.EventHandler(this.ChkRec_Pattern_Weekly_Saturday_Leave);
            // 
            // ChkRec_Pattern_Weekly_Friday
            // 
            this.ChkRec_Pattern_Weekly_Friday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Friday.Location = new System.Drawing.Point(92, 64);
            this.ChkRec_Pattern_Weekly_Friday.Name = "ChkRec_Pattern_Weekly_Friday";
            this.ChkRec_Pattern_Weekly_Friday.Size = new System.Drawing.Size(57, 18);
            this.ChkRec_Pattern_Weekly_Friday.TabIndex = 7;
            this.ChkRec_Pattern_Weekly_Friday.Text = "Friday";
            this.ChkRec_Pattern_Weekly_Friday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Friday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Friday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Sunday
            // 
            this.ChkRec_Pattern_Weekly_Sunday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Sunday.Location = new System.Drawing.Point(10, 38);
            this.ChkRec_Pattern_Weekly_Sunday.Name = "ChkRec_Pattern_Weekly_Sunday";
            this.ChkRec_Pattern_Weekly_Sunday.Size = new System.Drawing.Size(66, 18);
            this.ChkRec_Pattern_Weekly_Sunday.TabIndex = 2;
            this.ChkRec_Pattern_Weekly_Sunday.Text = "Sunday";
            this.ChkRec_Pattern_Weekly_Sunday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Sunday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Sunday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Tuesday
            // 
            this.ChkRec_Pattern_Weekly_Tuesday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Tuesday.Location = new System.Drawing.Point(176, 38);
            this.ChkRec_Pattern_Weekly_Tuesday.Name = "ChkRec_Pattern_Weekly_Tuesday";
            this.ChkRec_Pattern_Weekly_Tuesday.Size = new System.Drawing.Size(72, 18);
            this.ChkRec_Pattern_Weekly_Tuesday.TabIndex = 4;
            this.ChkRec_Pattern_Weekly_Tuesday.Text = "Tuesday";
            this.ChkRec_Pattern_Weekly_Tuesday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Tuesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Tuesday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Wednesday
            // 
            this.ChkRec_Pattern_Weekly_Wednesday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Wednesday.Location = new System.Drawing.Point(264, 38);
            this.ChkRec_Pattern_Weekly_Wednesday.Name = "ChkRec_Pattern_Weekly_Wednesday";
            this.ChkRec_Pattern_Weekly_Wednesday.Size = new System.Drawing.Size(90, 18);
            this.ChkRec_Pattern_Weekly_Wednesday.TabIndex = 5;
            this.ChkRec_Pattern_Weekly_Wednesday.Text = "Wednesday";
            this.ChkRec_Pattern_Weekly_Wednesday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Wednesday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Wednesday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Thursday
            // 
            this.ChkRec_Pattern_Weekly_Thursday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Thursday.Location = new System.Drawing.Point(10, 64);
            this.ChkRec_Pattern_Weekly_Thursday.Name = "ChkRec_Pattern_Weekly_Thursday";
            this.ChkRec_Pattern_Weekly_Thursday.Size = new System.Drawing.Size(76, 18);
            this.ChkRec_Pattern_Weekly_Thursday.TabIndex = 6;
            this.ChkRec_Pattern_Weekly_Thursday.Text = "Thursday";
            this.ChkRec_Pattern_Weekly_Thursday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Thursday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Thursday_CheckedChanged);
            // 
            // ChkRec_Pattern_Weekly_Monday
            // 
            this.ChkRec_Pattern_Weekly_Monday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Monday.Location = new System.Drawing.Point(92, 38);
            this.ChkRec_Pattern_Weekly_Monday.Name = "ChkRec_Pattern_Weekly_Monday";
            this.ChkRec_Pattern_Weekly_Monday.Size = new System.Drawing.Size(68, 18);
            this.ChkRec_Pattern_Weekly_Monday.TabIndex = 3;
            this.ChkRec_Pattern_Weekly_Monday.Text = "Monday";
            this.ChkRec_Pattern_Weekly_Monday.UseVisualStyleBackColor = true;
            this.ChkRec_Pattern_Weekly_Monday.CheckedChanged += new System.EventHandler(this.ChkRec_Pattern_Weekly_Monday_CheckedChanged);
            // 
            // numRec_Pattern_Weekly_WeekOn
            // 
            this.numRec_Pattern_Weekly_WeekOn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Weekly_WeekOn.ForeColor = System.Drawing.Color.Black;
            this.numRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(88, 7);
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
            this.numRec_Pattern_Weekly_WeekOn.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Weekly_WeekOn.TabIndex = 1;
            this.numRec_Pattern_Weekly_WeekOn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Weekly_WeekOn.ValueChanged += new System.EventHandler(this.numRec_Pattern_Weekly_WeekOn_ValueChanged);
            // 
            // pnlRec_Pattern_Yearly
            // 
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyTopBrd);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd);
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
            this.pnlRec_Pattern_Yearly.Location = new System.Drawing.Point(92, 36);
            this.pnlRec_Pattern_Yearly.Name = "pnlRec_Pattern_Yearly";
            this.pnlRec_Pattern_Yearly.Size = new System.Drawing.Size(491, 89);
            this.pnlRec_Pattern_Yearly.TabIndex = 8;
            this.pnlRec_Pattern_Yearly.Visible = false;
            // 
            // lbl_pnlRec_Pattern_YearlyTopBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Name = "lbl_pnlRec_Pattern_YearlyTopBrd";
            this.lbl_pnlRec_Pattern_YearlyTopBrd.Size = new System.Drawing.Size(489, 1);
            this.lbl_pnlRec_Pattern_YearlyTopBrd.TabIndex = 85;
            // 
            // lbl_pnlRec_Pattern_YearlyBottomBrdBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.Location = new System.Drawing.Point(1, 88);
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.Name = "lbl_pnlRec_Pattern_YearlyBottomBrdBrd";
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.Size = new System.Drawing.Size(489, 1);
            this.lbl_pnlRec_Pattern_YearlyBottomBrdBrd.TabIndex = 84;
            // 
            // lbl_pnlRec_Pattern_YearlyRightBrd
            // 
            this.lbl_pnlRec_Pattern_YearlyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_YearlyRightBrd.Location = new System.Drawing.Point(490, 0);
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
            this.numRec_Pattern_Yearly_Every_MonthDay.Location = new System.Drawing.Point(188, 17);
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
            this.numRec_Pattern_Yearly_Every_MonthDay.TabIndex = 3;
            this.numRec_Pattern_Yearly_Every_MonthDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbRec_Pattern_Yearly_Criteria_Month
            // 
            this.cmbRec_Pattern_Yearly_Criteria_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_Month.Location = new System.Drawing.Point(332, 53);
            this.cmbRec_Pattern_Yearly_Criteria_Month.Name = "cmbRec_Pattern_Yearly_Criteria_Month";
            this.cmbRec_Pattern_Yearly_Criteria_Month.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Yearly_Criteria_Month.TabIndex = 7;
            this.cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Every_Month
            // 
            this.cmbRec_Pattern_Yearly_Every_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Every_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Every_Month.Location = new System.Drawing.Point(72, 16);
            this.cmbRec_Pattern_Yearly_Every_Month.Name = "cmbRec_Pattern_Yearly_Every_Month";
            this.cmbRec_Pattern_Yearly_Every_Month.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Yearly_Every_Month.TabIndex = 2;
            this.cmbRec_Pattern_Yearly_Every_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_DayWeekday
            // 
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Location = new System.Drawing.Point(188, 53);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Yearly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.TabIndex = 6;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Location = new System.Drawing.Point(72, 53);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Name = "cmbRec_Pattern_Yearly_Criteria_FstLst";
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.TabIndex = 5;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Yearly_Criteria_Of
            // 
            this.lblRec_Pattern_Yearly_Criteria_Of.AutoSize = true;
            this.lblRec_Pattern_Yearly_Criteria_Of.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Yearly_Criteria_Of.Location = new System.Drawing.Point(306, 57);
            this.lblRec_Pattern_Yearly_Criteria_Of.Name = "lblRec_Pattern_Yearly_Criteria_Of";
            this.lblRec_Pattern_Yearly_Criteria_Of.Size = new System.Drawing.Size(18, 14);
            this.lblRec_Pattern_Yearly_Criteria_Of.TabIndex = 19;
            this.lblRec_Pattern_Yearly_Criteria_Of.Text = "of";
            // 
            // rbRec_Pattern_Yearly_Criteria
            // 
            this.rbRec_Pattern_Yearly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Yearly_Criteria.Location = new System.Drawing.Point(10, 55);
            this.rbRec_Pattern_Yearly_Criteria.Name = "rbRec_Pattern_Yearly_Criteria";
            this.rbRec_Pattern_Yearly_Criteria.Size = new System.Drawing.Size(47, 18);
            this.rbRec_Pattern_Yearly_Criteria.TabIndex = 4;
            this.rbRec_Pattern_Yearly_Criteria.Text = "The";
            this.rbRec_Pattern_Yearly_Criteria.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_Criteria_CheckedChanged);
            // 
            // rbRec_Pattern_Yearly_EveryMonthDay
            // 
            this.rbRec_Pattern_Yearly_EveryMonthDay.AutoSize = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Checked = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Location = new System.Drawing.Point(10, 18);
            this.rbRec_Pattern_Yearly_EveryMonthDay.Name = "rbRec_Pattern_Yearly_EveryMonthDay";
            this.rbRec_Pattern_Yearly_EveryMonthDay.Size = new System.Drawing.Size(55, 18);
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabIndex = 1;
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabStop = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Text = "Every";
            this.rbRec_Pattern_Yearly_EveryMonthDay.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged);
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
            this.pnlRec_Pattern_Monthly.Location = new System.Drawing.Point(92, 36);
            this.pnlRec_Pattern_Monthly.Name = "pnlRec_Pattern_Monthly";
            this.pnlRec_Pattern_Monthly.Size = new System.Drawing.Size(491, 89);
            this.pnlRec_Pattern_Monthly.TabIndex = 7;
            this.pnlRec_Pattern_Monthly.Visible = false;
            // 
            // lbl_pnlRec_Pattern_MonthlyRightBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Location = new System.Drawing.Point(490, 1);
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
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Size = new System.Drawing.Size(491, 1);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.TabIndex = 86;
            // 
            // lbl_pnlRec_Pattern_MonthlyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Location = new System.Drawing.Point(0, 88);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Name = "lbl_pnlRec_Pattern_MonthlyBottomBrd";
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Size = new System.Drawing.Size(491, 1);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.TabIndex = 85;
            // 
            // numRec_Pattern_Monthly_Criteria_Month
            // 
            this.numRec_Pattern_Monthly_Criteria_Month.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(359, 52);
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
            this.numRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(48, 21);
            this.numRec_Pattern_Monthly_Criteria_Month.TabIndex = 7;
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
            this.numRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(182, 16);
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
            this.numRec_Pattern_Monthly_Day_Month.TabIndex = 3;
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
            this.numRec_Pattern_Monthly_Day_Day.Location = new System.Drawing.Point(65, 16);
            this.numRec_Pattern_Monthly_Day_Day.Maximum = new decimal(new int[] {
            31,
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
            this.numRec_Pattern_Monthly_Day_Day.TabIndex = 2;
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
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Location = new System.Drawing.Point(180, 51);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Monthly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.TabIndex = 6;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Monthly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Location = new System.Drawing.Point(64, 51);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Name = "cmbRec_Pattern_Monthly_Criteria_FstLst";
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Size = new System.Drawing.Size(108, 22);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.TabIndex = 5;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Monthly_Criteria_Month
            // 
            this.lblRec_Pattern_Monthly_Criteria_Month.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Month.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(414, 55);
            this.lblRec_Pattern_Monthly_Criteria_Month.Name = "lblRec_Pattern_Monthly_Criteria_Month";
            this.lblRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Criteria_Month.TabIndex = 21;
            this.lblRec_Pattern_Monthly_Criteria_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Criteria_Every
            // 
            this.lblRec_Pattern_Monthly_Criteria_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Every.Location = new System.Drawing.Point(297, 55);
            this.lblRec_Pattern_Monthly_Criteria_Every.Name = "lblRec_Pattern_Monthly_Criteria_Every";
            this.lblRec_Pattern_Monthly_Criteria_Every.Size = new System.Drawing.Size(56, 14);
            this.lblRec_Pattern_Monthly_Criteria_Every.TabIndex = 19;
            this.lblRec_Pattern_Monthly_Criteria_Every.Text = "of every ";
            // 
            // rbRec_Pattern_Monthly_Criteria
            // 
            this.rbRec_Pattern_Monthly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Monthly_Criteria.Location = new System.Drawing.Point(10, 53);
            this.rbRec_Pattern_Monthly_Criteria.Name = "rbRec_Pattern_Monthly_Criteria";
            this.rbRec_Pattern_Monthly_Criteria.Size = new System.Drawing.Size(47, 18);
            this.rbRec_Pattern_Monthly_Criteria.TabIndex = 4;
            this.rbRec_Pattern_Monthly_Criteria.Text = "The";
            this.rbRec_Pattern_Monthly_Criteria.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly_Criteria.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Criteria_CheckedChanged);
            // 
            // lblRec_Pattern_Monthly_Day_Month
            // 
            this.lblRec_Pattern_Monthly_Day_Month.AutoSize = true;
            this.lblRec_Pattern_Monthly_Day_Month.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(237, 19);
            this.lblRec_Pattern_Monthly_Day_Month.Name = "lblRec_Pattern_Monthly_Day_Month";
            this.lblRec_Pattern_Monthly_Day_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Day_Month.TabIndex = 16;
            this.lblRec_Pattern_Monthly_Day_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Day_Every
            // 
            this.lblRec_Pattern_Monthly_Day_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Day_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Day_Every.Location = new System.Drawing.Point(120, 19);
            this.lblRec_Pattern_Monthly_Day_Every.Name = "lblRec_Pattern_Monthly_Day_Every";
            this.lblRec_Pattern_Monthly_Day_Every.Size = new System.Drawing.Size(56, 14);
            this.lblRec_Pattern_Monthly_Day_Every.TabIndex = 14;
            this.lblRec_Pattern_Monthly_Day_Every.Text = "of every ";
            // 
            // rbRec_Pattern_Monthly_Day
            // 
            this.rbRec_Pattern_Monthly_Day.AutoSize = true;
            this.rbRec_Pattern_Monthly_Day.Checked = true;
            this.rbRec_Pattern_Monthly_Day.Location = new System.Drawing.Point(10, 17);
            this.rbRec_Pattern_Monthly_Day.Name = "rbRec_Pattern_Monthly_Day";
            this.rbRec_Pattern_Monthly_Day.Size = new System.Drawing.Size(45, 18);
            this.rbRec_Pattern_Monthly_Day.TabIndex = 1;
            this.rbRec_Pattern_Monthly_Day.TabStop = true;
            this.rbRec_Pattern_Monthly_Day.Text = "Day";
            this.rbRec_Pattern_Monthly_Day.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly_Day.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Day_CheckedChanged);
            // 
            // pnlRecurring_Range
            // 
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeBottomBrd);
            this.pnlRecurring_Range.Controls.Add(this.pnlRecurring_Range_Header);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.numRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_Occurence);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeTopBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeLeftBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeRightBrd);
            this.pnlRecurring_Range.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRecurring_Range.Location = new System.Drawing.Point(0, 146);
            this.pnlRecurring_Range.Name = "pnlRecurring_Range";
            this.pnlRecurring_Range.Size = new System.Drawing.Size(682, 78);
            this.pnlRecurring_Range.TabIndex = 5;
            // 
            // lbl_pnlRecurring_RangeBottomBrd
            // 
            this.lbl_pnlRecurring_RangeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_RangeBottomBrd.Location = new System.Drawing.Point(1, 77);
            this.lbl_pnlRecurring_RangeBottomBrd.Name = "lbl_pnlRecurring_RangeBottomBrd";
            this.lbl_pnlRecurring_RangeBottomBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_RangeBottomBrd.TabIndex = 20;
            // 
            // pnlRecurring_Range_Header
            // 
            this.pnlRecurring_Range_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Range_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Range_Header.Controls.Add(this.lblRec_Range_EndDate);
            this.pnlRecurring_Range_Header.Controls.Add(this.lbl_pnlRecurring_Range_HeaderBottomBrd);
            this.pnlRecurring_Range_Header.Controls.Add(this.cmbRec_Range_NoEndDateYear);
            this.pnlRecurring_Range_Header.Controls.Add(this.lblRecurring_Range_Header);
            this.pnlRecurring_Range_Header.Controls.Add(this.rbRec_Range_NoEndDate);
            this.pnlRecurring_Range_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Range_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlRecurring_Range_Header.Name = "pnlRecurring_Range_Header";
            this.pnlRecurring_Range_Header.Size = new System.Drawing.Size(680, 23);
            this.pnlRecurring_Range_Header.TabIndex = 5;
            // 
            // lblRec_Range_EndDate
            // 
            this.lblRec_Range_EndDate.AutoSize = true;
            this.lblRec_Range_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_EndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_EndDate.Location = new System.Drawing.Point(320, 5);
            this.lblRec_Range_EndDate.Name = "lblRec_Range_EndDate";
            this.lblRec_Range_EndDate.Size = new System.Drawing.Size(66, 14);
            this.lblRec_Range_EndDate.TabIndex = 17;
            this.lblRec_Range_EndDate.Text = "End Date :";
            this.lblRec_Range_EndDate.Visible = false;
            // 
            // lbl_pnlRecurring_Range_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Name = "lbl_pnlRecurring_Range_HeaderBottomBrd";
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.TabIndex = 19;
            // 
            // cmbRec_Range_NoEndDateYear
            // 
            this.cmbRec_Range_NoEndDateYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Range_NoEndDateYear.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmbRec_Range_NoEndDateYear.ForeColor = System.Drawing.Color.Black;
            this.cmbRec_Range_NoEndDateYear.FormattingEnabled = true;
            this.cmbRec_Range_NoEndDateYear.Location = new System.Drawing.Point(486, 1);
            this.cmbRec_Range_NoEndDateYear.Name = "cmbRec_Range_NoEndDateYear";
            this.cmbRec_Range_NoEndDateYear.Size = new System.Drawing.Size(72, 21);
            this.cmbRec_Range_NoEndDateYear.TabIndex = 26;
            this.cmbRec_Range_NoEndDateYear.Visible = false;
            // 
            // lblRecurring_Range_Header
            // 
            this.lblRecurring_Range_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Range_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Range_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Range_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Range_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_Range_Header.Name = "lblRecurring_Range_Header";
            this.lblRecurring_Range_Header.Size = new System.Drawing.Size(680, 23);
            this.lblRecurring_Range_Header.TabIndex = 0;
            this.lblRecurring_Range_Header.Text = " Range of recurrence";
            this.lblRecurring_Range_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbRec_Range_NoEndDate
            // 
            this.rbRec_Range_NoEndDate.AutoSize = true;
            this.rbRec_Range_NoEndDate.BackColor = System.Drawing.Color.Transparent;
            this.rbRec_Range_NoEndDate.Checked = true;
            this.rbRec_Range_NoEndDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_NoEndDate.Location = new System.Drawing.Point(548, 3);
            this.rbRec_Range_NoEndDate.Name = "rbRec_Range_NoEndDate";
            this.rbRec_Range_NoEndDate.Size = new System.Drawing.Size(86, 17);
            this.rbRec_Range_NoEndDate.TabIndex = 14;
            this.rbRec_Range_NoEndDate.TabStop = true;
            this.rbRec_Range_NoEndDate.Text = "For this year";
            this.rbRec_Range_NoEndDate.UseVisualStyleBackColor = false;
            this.rbRec_Range_NoEndDate.Visible = false;
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
            this.dtpRec_Range_StartDate.Location = new System.Drawing.Point(93, 37);
            this.dtpRec_Range_StartDate.Name = "dtpRec_Range_StartDate";
            this.dtpRec_Range_StartDate.Size = new System.Drawing.Size(98, 22);
            this.dtpRec_Range_StartDate.TabIndex = 1;
            this.dtpRec_Range_StartDate.ValueChanged += new System.EventHandler(this.dtpRec_Range_StartDate_ValueChanged);
            // 
            // rbRec_Range_EndBy
            // 
            this.rbRec_Range_EndBy.AutoSize = true;
            this.rbRec_Range_EndBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_EndBy.Location = new System.Drawing.Point(220, 39);
            this.rbRec_Range_EndBy.Name = "rbRec_Range_EndBy";
            this.rbRec_Range_EndBy.Size = new System.Drawing.Size(63, 18);
            this.rbRec_Range_EndBy.TabIndex = 2;
            this.rbRec_Range_EndBy.Text = "End by";
            this.rbRec_Range_EndBy.UseVisualStyleBackColor = true;
            this.rbRec_Range_EndBy.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndBy_CheckedChanged);
            // 
            // lblRec_Range_StartDate
            // 
            this.lblRec_Range_StartDate.AutoSize = true;
            this.lblRec_Range_StartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_StartDate.Location = new System.Drawing.Point(18, 41);
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
            this.rbRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(415, 39);
            this.rbRec_Range_EndAfterOccurence.Name = "rbRec_Range_EndAfterOccurence";
            this.rbRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(82, 18);
            this.rbRec_Range_EndAfterOccurence.TabIndex = 4;
            this.rbRec_Range_EndAfterOccurence.TabStop = true;
            this.rbRec_Range_EndAfterOccurence.Text = "End after";
            this.rbRec_Range_EndAfterOccurence.UseVisualStyleBackColor = true;
            this.rbRec_Range_EndAfterOccurence.CheckedChanged += new System.EventHandler(this.rbRec_Range_EndAfterOccurence_CheckedChanged_1);
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
            this.dtpRec_Range_EndBy.Location = new System.Drawing.Point(290, 37);
            this.dtpRec_Range_EndBy.Name = "dtpRec_Range_EndBy";
            this.dtpRec_Range_EndBy.Size = new System.Drawing.Size(98, 22);
            this.dtpRec_Range_EndBy.TabIndex = 3;
            this.dtpRec_Range_EndBy.ValueChanged += new System.EventHandler(this.dtpRec_Range_EndBy_ValueChanged);
            // 
            // numRec_Range_EndAfterOccurence
            // 
            this.numRec_Range_EndAfterOccurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Range_EndAfterOccurence.ForeColor = System.Drawing.Color.Black;
            this.numRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(500, 37);
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
            this.numRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(56, 22);
            this.numRec_Range_EndAfterOccurence.TabIndex = 5;
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
            this.lblRec_Range_Occurence.Location = new System.Drawing.Point(559, 41);
            this.lblRec_Range_Occurence.Name = "lblRec_Range_Occurence";
            this.lblRec_Range_Occurence.Size = new System.Drawing.Size(75, 14);
            this.lblRec_Range_Occurence.TabIndex = 12;
            this.lblRec_Range_Occurence.Text = "Occurrences";
            // 
            // lbl_pnlRecurring_RangeTopBrd
            // 
            this.lbl_pnlRecurring_RangeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_RangeTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRecurring_RangeTopBrd.Name = "lbl_pnlRecurring_RangeTopBrd";
            this.lbl_pnlRecurring_RangeTopBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlRecurring_RangeTopBrd.TabIndex = 18;
            // 
            // lbl_pnlRecurring_RangeLeftBrd
            // 
            this.lbl_pnlRecurring_RangeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_RangeLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRecurring_RangeLeftBrd.Name = "lbl_pnlRecurring_RangeLeftBrd";
            this.lbl_pnlRecurring_RangeLeftBrd.Size = new System.Drawing.Size(1, 78);
            this.lbl_pnlRecurring_RangeLeftBrd.TabIndex = 21;
            // 
            // lbl_pnlRecurring_RangeRightBrd
            // 
            this.lbl_pnlRecurring_RangeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_RangeRightBrd.Location = new System.Drawing.Point(681, 0);
            this.lbl_pnlRecurring_RangeRightBrd.Name = "lbl_pnlRecurring_RangeRightBrd";
            this.lbl_pnlRecurring_RangeRightBrd.Size = new System.Drawing.Size(1, 78);
            this.lbl_pnlRecurring_RangeRightBrd.TabIndex = 22;
            // 
            // dtpRec_StartTime
            // 
            this.dtpRec_StartTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpRec_StartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_StartTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpRec_StartTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpRec_StartTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpRec_StartTime.CustomFormat = "hh:mm tt";
            this.dtpRec_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_StartTime.Location = new System.Drawing.Point(101, 33);
            this.dtpRec_StartTime.Name = "dtpRec_StartTime";
            this.dtpRec_StartTime.ShowUpDown = true;
            this.dtpRec_StartTime.Size = new System.Drawing.Size(95, 22);
            this.dtpRec_StartTime.TabIndex = 0;
            this.dtpRec_StartTime.ValueChanged += new System.EventHandler(this.dtpRec_StartTime_ValueChanged);
            // 
            // lblSimple_StartTime
            // 
            this.lblSimple_StartTime.AutoSize = true;
            this.lblSimple_StartTime.BackColor = System.Drawing.Color.Transparent;
            this.lblSimple_StartTime.Location = new System.Drawing.Point(28, 37);
            this.lblSimple_StartTime.Name = "lblSimple_StartTime";
            this.lblSimple_StartTime.Size = new System.Drawing.Size(73, 14);
            this.lblSimple_StartTime.TabIndex = 156;
            this.lblSimple_StartTime.Text = "Start Time :";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(432, 35);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(260, 22);
            this.cmbDepartment.TabIndex = 2;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDepartment.Location = new System.Drawing.Point(348, 39);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(81, 14);
            this.lblDepartment.TabIndex = 146;
            this.lblDepartment.Text = "Department :";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.ForeColor = System.Drawing.Color.Black;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(82, 35);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(260, 22);
            this.cmbLocation.TabIndex = 0;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLocation.Location = new System.Drawing.Point(18, 39);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 14);
            this.lblLocation.TabIndex = 144;
            this.lblLocation.Text = "Location :";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(32, 586);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(47, 14);
            this.lblNotes.TabIndex = 175;
            this.lblNotes.Text = "Notes :";
            // 
            // rbBlockedSchedule
            // 
            this.rbBlockedSchedule.AutoSize = true;
            this.rbBlockedSchedule.Checked = true;
            this.rbBlockedSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBlockedSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbBlockedSchedule.Location = new System.Drawing.Point(237, 121);
            this.rbBlockedSchedule.Name = "rbBlockedSchedule";
            this.rbBlockedSchedule.Size = new System.Drawing.Size(107, 18);
            this.rbBlockedSchedule.TabIndex = 5;
            this.rbBlockedSchedule.TabStop = true;
            this.rbBlockedSchedule.Text = "Block Schedule";
            this.rbBlockedSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbBlockedSchedule.UseVisualStyleBackColor = true;
            this.rbBlockedSchedule.CheckedChanged += new System.EventHandler(this.rbProviderSchedule_CheckedChanged);
            // 
            // txtScheduleNote
            // 
            this.txtScheduleNote.ForeColor = System.Drawing.Color.Black;
            this.txtScheduleNote.Location = new System.Drawing.Point(82, 583);
            this.txtScheduleNote.MaxLength = 1000;
            this.txtScheduleNote.Name = "txtScheduleNote";
            this.txtScheduleNote.Size = new System.Drawing.Size(608, 32);
            this.txtScheduleNote.TabIndex = 10;
            this.txtScheduleNote.Text = "";
            // 
            // rbResourceSchedule
            // 
            this.rbResourceSchedule.AutoSize = true;
            this.rbResourceSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbResourceSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbResourceSchedule.Location = new System.Drawing.Point(363, 121);
            this.rbResourceSchedule.Name = "rbResourceSchedule";
            this.rbResourceSchedule.Size = new System.Drawing.Size(129, 18);
            this.rbResourceSchedule.TabIndex = 6;
            this.rbResourceSchedule.Text = "Resource Schedule";
            this.rbResourceSchedule.UseVisualStyleBackColor = true;
            this.rbResourceSchedule.Visible = false;
            this.rbResourceSchedule.CheckedChanged += new System.EventHandler(this.rbProviderSchedule_CheckedChanged);
            // 
            // rbProviderSchedule
            // 
            this.rbProviderSchedule.AutoSize = true;
            this.rbProviderSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProviderSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbProviderSchedule.Location = new System.Drawing.Point(83, 121);
            this.rbProviderSchedule.Name = "rbProviderSchedule";
            this.rbProviderSchedule.Size = new System.Drawing.Size(135, 18);
            this.rbProviderSchedule.TabIndex = 4;
            this.rbProviderSchedule.Text = "Provider Schedule";
            this.rbProviderSchedule.UseVisualStyleBackColor = true;
            this.rbProviderSchedule.CheckedChanged += new System.EventHandler(this.rbProviderSchedule_CheckedChanged);
            // 
            // pnlScedules
            // 
            this.pnlScedules.Controls.Add(this.panel1);
            this.pnlScedules.Controls.Add(this.lblApp_Recurrence_Time);
            this.pnlScedules.Controls.Add(this.pnlApp_DateTime);
            this.pnlScedules.Controls.Add(this.pnlAppointmentHeader);
            this.pnlScedules.Controls.Add(this.lbl_pnlScedulesBottomBrd);
            this.pnlScedules.Controls.Add(this.btnClearProviderUsers);
            this.pnlScedules.Controls.Add(this.lbl_pnlScedulesTopBrd);
            this.pnlScedules.Controls.Add(this.btnBrowseProviderUsers);
            this.pnlScedules.Controls.Add(this.lbl_pnlScedulesRightBrd);
            this.pnlScedules.Controls.Add(this.lbl_pnlScedulesLeftBrd);
            this.pnlScedules.Controls.Add(this.lblLocation);
            this.pnlScedules.Controls.Add(this.txtScheduleNote);
            this.pnlScedules.Controls.Add(this.lblNotes);
            this.pnlScedules.Controls.Add(this.pnlCriteria_ProviderUsers);
            this.pnlScedules.Controls.Add(this.rbBlockedSchedule);
            this.pnlScedules.Controls.Add(this.rbResourceSchedule);
            this.pnlScedules.Controls.Add(this.rbProviderSchedule);
            this.pnlScedules.Controls.Add(this.cmbLocation);
            this.pnlScedules.Controls.Add(this.lblDepartment);
            this.pnlScedules.Controls.Add(this.cmbDepartment);
            this.pnlScedules.Controls.Add(this.pnlBlockedSchedule);
            this.pnlScedules.Controls.Add(this.pnlProviderSchedule);
            this.pnlScedules.Controls.Add(this.pnlResourceSchedule);
            this.pnlScedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScedules.Location = new System.Drawing.Point(0, 55);
            this.pnlScedules.Name = "pnlScedules";
            this.pnlScedules.Padding = new System.Windows.Forms.Padding(3);
            this.pnlScedules.Size = new System.Drawing.Size(725, 629);
            this.pnlScedules.TabIndex = 0;
            // 
            // lblApp_Recurrence_Time
            // 
            this.lblApp_Recurrence_Time.AutoSize = true;
            this.lblApp_Recurrence_Time.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Recurrence_Time.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp_Recurrence_Time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblApp_Recurrence_Time.Location = new System.Drawing.Point(85, 60);
            this.lblApp_Recurrence_Time.Name = "lblApp_Recurrence_Time";
            this.lblApp_Recurrence_Time.Size = new System.Drawing.Size(42, 14);
            this.lblApp_Recurrence_Time.TabIndex = 184;
            this.lblApp_Recurrence_Time.Text = "Time :";
            this.lblApp_Recurrence_Time.Visible = false;
            // 
            // pnlApp_DateTime
            // 
            this.pnlApp_DateTime.Controls.Add(this.label1);
            this.pnlApp_DateTime.Controls.Add(this.dtpApp_DateTime_EndTime);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeBootomBrd);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeTopBrd);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeRightBrd);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeLeftBrd);
            this.pnlApp_DateTime.Controls.Add(this.lblDurationUnit);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_DateTime_Date);
            this.pnlApp_DateTime.Controls.Add(this.dtpApp_DateTime_StartTime);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_ColorContainer);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_DateTime_Duration);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_DateTime_Time);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_DateTime_Color);
            this.pnlApp_DateTime.Controls.Add(this.dtpApp_DateTime_StartDate);
            this.pnlApp_DateTime.Controls.Add(this.btnApp_DateTime_Color);
            this.pnlApp_DateTime.Controls.Add(this.numApp_DateTime_Duration);
            this.pnlApp_DateTime.Controls.Add(this.chkApp_DateTime_IsAllDayEvent);
            this.pnlApp_DateTime.Location = new System.Drawing.Point(83, 77);
            this.pnlApp_DateTime.Name = "pnlApp_DateTime";
            this.pnlApp_DateTime.Size = new System.Drawing.Size(608, 35);
            this.pnlApp_DateTime.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(326, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 185;
            this.label1.Text = "End Time :";
            // 
            // dtpApp_DateTime_EndTime
            // 
            this.dtpApp_DateTime_EndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_EndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_EndTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_EndTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_EndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_EndTime.CustomFormat = "hh:mm tt";
            this.dtpApp_DateTime_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_EndTime.Location = new System.Drawing.Point(396, 5);
            this.dtpApp_DateTime_EndTime.Name = "dtpApp_DateTime_EndTime";
            this.dtpApp_DateTime_EndTime.ShowUpDown = true;
            this.dtpApp_DateTime_EndTime.Size = new System.Drawing.Size(83, 22);
            this.dtpApp_DateTime_EndTime.TabIndex = 2;
            this.dtpApp_DateTime_EndTime.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_EndTime_ValueChanged);
            // 
            // lbl_pnlApp_DateTimeBootomBrd
            // 
            this.lbl_pnlApp_DateTimeBootomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeBootomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlApp_DateTimeBootomBrd.Location = new System.Drawing.Point(1, 34);
            this.lbl_pnlApp_DateTimeBootomBrd.Name = "lbl_pnlApp_DateTimeBootomBrd";
            this.lbl_pnlApp_DateTimeBootomBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlApp_DateTimeBootomBrd.TabIndex = 116;
            // 
            // lbl_pnlApp_DateTimeTopBrd
            // 
            this.lbl_pnlApp_DateTimeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlApp_DateTimeTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlApp_DateTimeTopBrd.Name = "lbl_pnlApp_DateTimeTopBrd";
            this.lbl_pnlApp_DateTimeTopBrd.Size = new System.Drawing.Size(606, 1);
            this.lbl_pnlApp_DateTimeTopBrd.TabIndex = 115;
            // 
            // lbl_pnlApp_DateTimeRightBrd
            // 
            this.lbl_pnlApp_DateTimeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlApp_DateTimeRightBrd.Location = new System.Drawing.Point(607, 0);
            this.lbl_pnlApp_DateTimeRightBrd.Name = "lbl_pnlApp_DateTimeRightBrd";
            this.lbl_pnlApp_DateTimeRightBrd.Size = new System.Drawing.Size(1, 35);
            this.lbl_pnlApp_DateTimeRightBrd.TabIndex = 114;
            this.lbl_pnlApp_DateTimeRightBrd.Text = "label7";
            // 
            // lbl_pnlApp_DateTimeLeftBrd
            // 
            this.lbl_pnlApp_DateTimeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlApp_DateTimeLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlApp_DateTimeLeftBrd.Name = "lbl_pnlApp_DateTimeLeftBrd";
            this.lbl_pnlApp_DateTimeLeftBrd.Size = new System.Drawing.Size(1, 35);
            this.lbl_pnlApp_DateTimeLeftBrd.TabIndex = 113;
            this.lbl_pnlApp_DateTimeLeftBrd.Text = "label17";
            // 
            // lblDurationUnit
            // 
            this.lblDurationUnit.AutoSize = true;
            this.lblDurationUnit.Location = new System.Drawing.Point(553, 61);
            this.lblDurationUnit.Name = "lblDurationUnit";
            this.lblDurationUnit.Size = new System.Drawing.Size(41, 14);
            this.lblDurationUnit.TabIndex = 87;
            this.lblDurationUnit.Text = "(mins)";
            this.lblDurationUnit.Visible = false;
            // 
            // lblApp_DateTime_Date
            // 
            this.lblApp_DateTime_Date.AutoSize = true;
            this.lblApp_DateTime_Date.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Date.Location = new System.Drawing.Point(8, 9);
            this.lblApp_DateTime_Date.Name = "lblApp_DateTime_Date";
            this.lblApp_DateTime_Date.Size = new System.Drawing.Size(41, 14);
            this.lblApp_DateTime_Date.TabIndex = 42;
            this.lblApp_DateTime_Date.Text = "Date :";
            this.lblApp_DateTime_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpApp_DateTime_StartTime
            // 
            this.dtpApp_DateTime_StartTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_StartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_StartTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_StartTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_StartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_StartTime.CustomFormat = "hh:mm tt";
            this.dtpApp_DateTime_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_StartTime.Location = new System.Drawing.Point(242, 5);
            this.dtpApp_DateTime_StartTime.Name = "dtpApp_DateTime_StartTime";
            this.dtpApp_DateTime_StartTime.ShowUpDown = true;
            this.dtpApp_DateTime_StartTime.Size = new System.Drawing.Size(80, 22);
            this.dtpApp_DateTime_StartTime.TabIndex = 1;
            this.dtpApp_DateTime_StartTime.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_StartTime_ValueChanged);
            // 
            // lblApp_ColorContainer
            // 
            this.lblApp_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lblApp_ColorContainer.Location = new System.Drawing.Point(533, 7);
            this.lblApp_ColorContainer.Name = "lblApp_ColorContainer";
            this.lblApp_ColorContainer.Size = new System.Drawing.Size(28, 19);
            this.lblApp_ColorContainer.TabIndex = 85;
            // 
            // lblApp_DateTime_Duration
            // 
            this.lblApp_DateTime_Duration.AutoSize = true;
            this.lblApp_DateTime_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Duration.Location = new System.Drawing.Point(436, 62);
            this.lblApp_DateTime_Duration.Name = "lblApp_DateTime_Duration";
            this.lblApp_DateTime_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblApp_DateTime_Duration.TabIndex = 44;
            this.lblApp_DateTime_Duration.Text = "Duration :";
            this.lblApp_DateTime_Duration.Visible = false;
            // 
            // lblApp_DateTime_Time
            // 
            this.lblApp_DateTime_Time.AutoSize = true;
            this.lblApp_DateTime_Time.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Time.Location = new System.Drawing.Point(167, 9);
            this.lblApp_DateTime_Time.Name = "lblApp_DateTime_Time";
            this.lblApp_DateTime_Time.Size = new System.Drawing.Size(73, 14);
            this.lblApp_DateTime_Time.TabIndex = 59;
            this.lblApp_DateTime_Time.Text = "Start Time :";
            // 
            // lblApp_DateTime_Color
            // 
            this.lblApp_DateTime_Color.AutoSize = true;
            this.lblApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Color.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp_DateTime_Color.Location = new System.Drawing.Point(488, 9);
            this.lblApp_DateTime_Color.Name = "lblApp_DateTime_Color";
            this.lblApp_DateTime_Color.Size = new System.Drawing.Size(42, 14);
            this.lblApp_DateTime_Color.TabIndex = 81;
            this.lblApp_DateTime_Color.Text = "Color :";
            this.lblApp_DateTime_Color.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpApp_DateTime_StartDate
            // 
            this.dtpApp_DateTime_StartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_StartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_StartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_StartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_StartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_StartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApp_DateTime_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_StartDate.Location = new System.Drawing.Point(52, 5);
            this.dtpApp_DateTime_StartDate.Name = "dtpApp_DateTime_StartDate";
            this.dtpApp_DateTime_StartDate.Size = new System.Drawing.Size(105, 22);
            this.dtpApp_DateTime_StartDate.TabIndex = 0;
            // 
            // btnApp_DateTime_Color
            // 
            this.btnApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_DateTime_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.BackgroundImage")));
            this.btnApp_DateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_DateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_DateTime_Color.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnApp_DateTime_Color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnApp_DateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_DateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.Image")));
            this.btnApp_DateTime_Color.Location = new System.Drawing.Point(567, 4);
            this.btnApp_DateTime_Color.Name = "btnApp_DateTime_Color";
            this.btnApp_DateTime_Color.Size = new System.Drawing.Size(24, 24);
            this.btnApp_DateTime_Color.TabIndex = 3;
            this.btnApp_DateTime_Color.UseVisualStyleBackColor = false;
            this.btnApp_DateTime_Color.Click += new System.EventHandler(this.btnApp_DateTime_Color_Click);
            this.btnApp_DateTime_Color.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnApp_DateTime_Color.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // numApp_DateTime_Duration
            // 
            this.numApp_DateTime_Duration.ForeColor = System.Drawing.Color.Black;
            this.numApp_DateTime_Duration.Location = new System.Drawing.Point(500, 58);
            this.numApp_DateTime_Duration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.Name = "numApp_DateTime_Duration";
            this.numApp_DateTime_Duration.Size = new System.Drawing.Size(48, 22);
            this.numApp_DateTime_Duration.TabIndex = 63;
            this.numApp_DateTime_Duration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.Visible = false;
            this.numApp_DateTime_Duration.ValueChanged += new System.EventHandler(this.numApp_DateTime_Duration_ValueChanged);
            // 
            // chkApp_DateTime_IsAllDayEvent
            // 
            this.chkApp_DateTime_IsAllDayEvent.AutoSize = true;
            this.chkApp_DateTime_IsAllDayEvent.BackColor = System.Drawing.Color.Transparent;
            this.chkApp_DateTime_IsAllDayEvent.Location = new System.Drawing.Point(430, 58);
            this.chkApp_DateTime_IsAllDayEvent.Name = "chkApp_DateTime_IsAllDayEvent";
            this.chkApp_DateTime_IsAllDayEvent.Size = new System.Drawing.Size(111, 18);
            this.chkApp_DateTime_IsAllDayEvent.TabIndex = 86;
            this.chkApp_DateTime_IsAllDayEvent.Text = "Is All Day Event";
            this.chkApp_DateTime_IsAllDayEvent.UseVisualStyleBackColor = false;
            this.chkApp_DateTime_IsAllDayEvent.Visible = false;
            // 
            // pnlAppointmentHeader
            // 
            this.pnlAppointmentHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlAppointmentHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAppointmentHeader.Controls.Add(this.lbl_pnlAppointmentHeaderBottomBrd);
            this.pnlAppointmentHeader.Controls.Add(this.lblAppointment);
            this.pnlAppointmentHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppointmentHeader.Location = new System.Drawing.Point(4, 4);
            this.pnlAppointmentHeader.Name = "pnlAppointmentHeader";
            this.pnlAppointmentHeader.Size = new System.Drawing.Size(717, 23);
            this.pnlAppointmentHeader.TabIndex = 181;
            // 
            // lbl_pnlAppointmentHeaderBottomBrd
            // 
            this.lbl_pnlAppointmentHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAppointmentHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlAppointmentHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlAppointmentHeaderBottomBrd.Name = "lbl_pnlAppointmentHeaderBottomBrd";
            this.lbl_pnlAppointmentHeaderBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlAppointmentHeaderBottomBrd.TabIndex = 180;
            // 
            // lblAppointment
            // 
            this.lblAppointment.BackColor = System.Drawing.Color.Transparent;
            this.lblAppointment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointment.ForeColor = System.Drawing.Color.White;
            this.lblAppointment.Location = new System.Drawing.Point(0, 0);
            this.lblAppointment.Name = "lblAppointment";
            this.lblAppointment.Size = new System.Drawing.Size(717, 23);
            this.lblAppointment.TabIndex = 0;
            this.lblAppointment.Text = " Schedule";
            this.lblAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Checked = true;
            this.rbSimple.Location = new System.Drawing.Point(11, 9);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(60, 18);
            this.rbSimple.TabIndex = 183;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Simple";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.Visible = false;
            // 
            // lbl_pnlScedulesBottomBrd
            // 
            this.lbl_pnlScedulesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlScedulesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlScedulesBottomBrd.Location = new System.Drawing.Point(4, 625);
            this.lbl_pnlScedulesBottomBrd.Name = "lbl_pnlScedulesBottomBrd";
            this.lbl_pnlScedulesBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlScedulesBottomBrd.TabIndex = 180;
            // 
            // rbRecurrence
            // 
            this.rbRecurrence.AutoSize = true;
            this.rbRecurrence.Location = new System.Drawing.Point(77, 8);
            this.rbRecurrence.Name = "rbRecurrence";
            this.rbRecurrence.Size = new System.Drawing.Size(87, 18);
            this.rbRecurrence.TabIndex = 182;
            this.rbRecurrence.TabStop = true;
            this.rbRecurrence.Text = "Recurrence";
            this.rbRecurrence.UseVisualStyleBackColor = true;
            this.rbRecurrence.Visible = false;
            // 
            // lbl_pnlScedulesTopBrd
            // 
            this.lbl_pnlScedulesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlScedulesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlScedulesTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlScedulesTopBrd.Name = "lbl_pnlScedulesTopBrd";
            this.lbl_pnlScedulesTopBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlScedulesTopBrd.TabIndex = 179;
            // 
            // lbl_pnlScedulesRightBrd
            // 
            this.lbl_pnlScedulesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlScedulesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlScedulesRightBrd.Location = new System.Drawing.Point(721, 3);
            this.lbl_pnlScedulesRightBrd.Name = "lbl_pnlScedulesRightBrd";
            this.lbl_pnlScedulesRightBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_pnlScedulesRightBrd.TabIndex = 178;
            // 
            // lbl_pnlScedulesLeftBrd
            // 
            this.lbl_pnlScedulesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlScedulesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlScedulesLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlScedulesLeftBrd.Name = "lbl_pnlScedulesLeftBrd";
            this.lbl_pnlScedulesLeftBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_pnlScedulesLeftBrd.TabIndex = 177;
            // 
            // pnlRecurrenceContainer
            // 
            this.pnlRecurrenceContainer.Controls.Add(this.pnlShowRecurrence);
            this.pnlRecurrenceContainer.Controls.Add(this.lblSimple_Duration);
            this.pnlRecurrenceContainer.Controls.Add(this.pnlRecurrenceContainer_Header);
            this.pnlRecurrenceContainer.Controls.Add(this.btnColorCode);
            this.pnlRecurrenceContainer.Controls.Add(this.lbl_pnlRecurrenceContainerBottomBrd);
            this.pnlRecurrenceContainer.Controls.Add(this.numRec_Duration);
            this.pnlRecurrenceContainer.Controls.Add(this.lbl_pnlRecurrenceContainerTopBrd);
            this.pnlRecurrenceContainer.Controls.Add(this.lblSimple_Color);
            this.pnlRecurrenceContainer.Controls.Add(this.lbl_pnlRecurrenceContainerRightBrd);
            this.pnlRecurrenceContainer.Controls.Add(this.lblSimple_EndTime);
            this.pnlRecurrenceContainer.Controls.Add(this.lbl_lbl_pnlRecurrenceContainerLeftBrd);
            this.pnlRecurrenceContainer.Controls.Add(this.lblRec_ColorContainer);
            this.pnlRecurrenceContainer.Controls.Add(this.dtpRec_EndTime);
            this.pnlRecurrenceContainer.Controls.Add(this.pnlRecurrance);
            this.pnlRecurrenceContainer.Controls.Add(this.lblSimple_StartTime);
            this.pnlRecurrenceContainer.Controls.Add(this.dtpRec_StartTime);
            this.pnlRecurrenceContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecurrenceContainer.Location = new System.Drawing.Point(0, 55);
            this.pnlRecurrenceContainer.Name = "pnlRecurrenceContainer";
            this.pnlRecurrenceContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRecurrenceContainer.Size = new System.Drawing.Size(725, 629);
            this.pnlRecurrenceContainer.TabIndex = 1;
            // 
            // pnlShowRecurrence
            // 
            this.pnlShowRecurrence.Controls.Add(this.c1Recurrence);
            this.pnlShowRecurrence.Controls.Add(this.pnlShowRecurrence_Header);
            this.pnlShowRecurrence.Controls.Add(this.lbl_pnlShowRecurrenceBottomBrd);
            this.pnlShowRecurrence.Controls.Add(this.lbl_pnlShowRecurrenceTopBrd);
            this.pnlShowRecurrence.Controls.Add(this.lbl_pnlShowRecurrenceRightBrd);
            this.pnlShowRecurrence.Controls.Add(this.lbl_pnlShowRecurrenceLeftBrd);
            this.pnlShowRecurrence.Location = new System.Drawing.Point(21, 301);
            this.pnlShowRecurrence.Name = "pnlShowRecurrence";
            this.pnlShowRecurrence.Size = new System.Drawing.Size(682, 314);
            this.pnlShowRecurrence.TabIndex = 6;
            // 
            // c1Recurrence
            // 
            this.c1Recurrence.AllowEditing = false;
            this.c1Recurrence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Recurrence.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Recurrence.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1Recurrence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Recurrence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Recurrence.Location = new System.Drawing.Point(1, 24);
            this.c1Recurrence.Name = "c1Recurrence";
            this.c1Recurrence.Rows.Count = 1;
            this.c1Recurrence.Rows.DefaultSize = 21;
            this.c1Recurrence.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Recurrence.Size = new System.Drawing.Size(680, 289);
            this.c1Recurrence.StyleInfo = resources.GetString("c1Recurrence.StyleInfo");
            this.c1Recurrence.TabIndex = 0;
            // 
            // pnlShowRecurrence_Header
            // 
            this.pnlShowRecurrence_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlShowRecurrence_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlShowRecurrence_Header.Controls.Add(this.lbl_pnlShowRecurrence_HeaderBottomBrd1);
            this.pnlShowRecurrence_Header.Controls.Add(this.lblSchedules);
            this.pnlShowRecurrence_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlShowRecurrence_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlShowRecurrence_Header.Name = "pnlShowRecurrence_Header";
            this.pnlShowRecurrence_Header.Size = new System.Drawing.Size(680, 23);
            this.pnlShowRecurrence_Header.TabIndex = 137;
            // 
            // lbl_pnlShowRecurrence_HeaderBottomBrd1
            // 
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.Name = "lbl_pnlShowRecurrence_HeaderBottomBrd1";
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlShowRecurrence_HeaderBottomBrd1.TabIndex = 190;
            // 
            // lblSchedules
            // 
            this.lblSchedules.BackColor = System.Drawing.Color.Transparent;
            this.lblSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSchedules.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchedules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSchedules.Location = new System.Drawing.Point(0, 0);
            this.lblSchedules.Name = "lblSchedules";
            this.lblSchedules.Size = new System.Drawing.Size(680, 23);
            this.lblSchedules.TabIndex = 0;
            this.lblSchedules.Text = " Schedules";
            this.lblSchedules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlShowRecurrenceBottomBrd
            // 
            this.lbl_pnlShowRecurrenceBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlShowRecurrenceBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlShowRecurrenceBottomBrd.Location = new System.Drawing.Point(1, 313);
            this.lbl_pnlShowRecurrenceBottomBrd.Name = "lbl_pnlShowRecurrenceBottomBrd";
            this.lbl_pnlShowRecurrenceBottomBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlShowRecurrenceBottomBrd.TabIndex = 190;
            // 
            // lbl_pnlShowRecurrenceTopBrd
            // 
            this.lbl_pnlShowRecurrenceTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlShowRecurrenceTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlShowRecurrenceTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlShowRecurrenceTopBrd.Name = "lbl_pnlShowRecurrenceTopBrd";
            this.lbl_pnlShowRecurrenceTopBrd.Size = new System.Drawing.Size(680, 1);
            this.lbl_pnlShowRecurrenceTopBrd.TabIndex = 189;
            // 
            // lbl_pnlShowRecurrenceRightBrd
            // 
            this.lbl_pnlShowRecurrenceRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlShowRecurrenceRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlShowRecurrenceRightBrd.Location = new System.Drawing.Point(681, 0);
            this.lbl_pnlShowRecurrenceRightBrd.Name = "lbl_pnlShowRecurrenceRightBrd";
            this.lbl_pnlShowRecurrenceRightBrd.Size = new System.Drawing.Size(1, 314);
            this.lbl_pnlShowRecurrenceRightBrd.TabIndex = 188;
            this.lbl_pnlShowRecurrenceRightBrd.Text = "label98";
            // 
            // lbl_pnlShowRecurrenceLeftBrd
            // 
            this.lbl_pnlShowRecurrenceLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlShowRecurrenceLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlShowRecurrenceLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlShowRecurrenceLeftBrd.Name = "lbl_pnlShowRecurrenceLeftBrd";
            this.lbl_pnlShowRecurrenceLeftBrd.Size = new System.Drawing.Size(1, 314);
            this.lbl_pnlShowRecurrenceLeftBrd.TabIndex = 187;
            this.lbl_pnlShowRecurrenceLeftBrd.Text = "label99";
            // 
            // pnlRecurrenceContainer_Header
            // 
            this.pnlRecurrenceContainer_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlRecurrenceContainer_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurrenceContainer_Header.Controls.Add(this.lbl_pnlRecurrenceContainer_HeaderBottomBrd);
            this.pnlRecurrenceContainer_Header.Controls.Add(this.lblRecurrence);
            this.pnlRecurrenceContainer_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurrenceContainer_Header.Location = new System.Drawing.Point(4, 4);
            this.pnlRecurrenceContainer_Header.Name = "pnlRecurrenceContainer_Header";
            this.pnlRecurrenceContainer_Header.Size = new System.Drawing.Size(717, 23);
            this.pnlRecurrenceContainer_Header.TabIndex = 181;
            // 
            // lbl_pnlRecurrenceContainer_HeaderBottomBrd
            // 
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.Name = "lbl_pnlRecurrenceContainer_HeaderBottomBrd";
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlRecurrenceContainer_HeaderBottomBrd.TabIndex = 180;
            // 
            // lblRecurrence
            // 
            this.lblRecurrence.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurrence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurrence.ForeColor = System.Drawing.Color.White;
            this.lblRecurrence.Location = new System.Drawing.Point(0, 0);
            this.lblRecurrence.Name = "lblRecurrence";
            this.lblRecurrence.Size = new System.Drawing.Size(717, 23);
            this.lblRecurrence.TabIndex = 0;
            this.lblRecurrence.Text = " Recurrence";
            this.lblRecurrence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlRecurrenceContainerBottomBrd
            // 
            this.lbl_pnlRecurrenceContainerBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurrenceContainerBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurrenceContainerBottomBrd.Location = new System.Drawing.Point(4, 625);
            this.lbl_pnlRecurrenceContainerBottomBrd.Name = "lbl_pnlRecurrenceContainerBottomBrd";
            this.lbl_pnlRecurrenceContainerBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlRecurrenceContainerBottomBrd.TabIndex = 180;
            // 
            // lbl_pnlRecurrenceContainerTopBrd
            // 
            this.lbl_pnlRecurrenceContainerTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurrenceContainerTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurrenceContainerTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlRecurrenceContainerTopBrd.Name = "lbl_pnlRecurrenceContainerTopBrd";
            this.lbl_pnlRecurrenceContainerTopBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlRecurrenceContainerTopBrd.TabIndex = 179;
            // 
            // lbl_pnlRecurrenceContainerRightBrd
            // 
            this.lbl_pnlRecurrenceContainerRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurrenceContainerRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurrenceContainerRightBrd.Location = new System.Drawing.Point(721, 3);
            this.lbl_pnlRecurrenceContainerRightBrd.Name = "lbl_pnlRecurrenceContainerRightBrd";
            this.lbl_pnlRecurrenceContainerRightBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_pnlRecurrenceContainerRightBrd.TabIndex = 178;
            // 
            // lbl_lbl_pnlRecurrenceContainerLeftBrd
            // 
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.Name = "lbl_lbl_pnlRecurrenceContainerLeftBrd";
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_lbl_pnlRecurrenceContainerLeftBrd.TabIndex = 177;
            // 
            // pnlListControl
            // 
            this.pnlListControl.Controls.Add(this.pnlListControl_Header);
            this.pnlListControl.Controls.Add(this.lbl_pnlListControlBottomBrd);
            this.pnlListControl.Controls.Add(this.lbl_pnlListControlTopBrd);
            this.pnlListControl.Controls.Add(this.lbl_pnlListControlRightBrd);
            this.pnlListControl.Controls.Add(this.lbl_pnlListControlLeftBrd);
            this.pnlListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListControl.Location = new System.Drawing.Point(0, 55);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Padding = new System.Windows.Forms.Padding(3);
            this.pnlListControl.Size = new System.Drawing.Size(725, 629);
            this.pnlListControl.TabIndex = 2;
            // 
            // pnlListControl_Header
            // 
            this.pnlListControl_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlListControl_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlListControl_Header.Controls.Add(this.lbl_pnlListControl_HeaderBottomBrd);
            this.pnlListControl_Header.Controls.Add(this.lblList);
            this.pnlListControl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListControl_Header.Location = new System.Drawing.Point(4, 4);
            this.pnlListControl_Header.Name = "pnlListControl_Header";
            this.pnlListControl_Header.Size = new System.Drawing.Size(717, 26);
            this.pnlListControl_Header.TabIndex = 181;
            this.pnlListControl_Header.Visible = false;
            // 
            // lbl_pnlListControl_HeaderBottomBrd
            // 
            this.lbl_pnlListControl_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlListControl_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlListControl_HeaderBottomBrd.Location = new System.Drawing.Point(0, 25);
            this.lbl_pnlListControl_HeaderBottomBrd.Name = "lbl_pnlListControl_HeaderBottomBrd";
            this.lbl_pnlListControl_HeaderBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlListControl_HeaderBottomBrd.TabIndex = 180;
            // 
            // lblList
            // 
            this.lblList.BackColor = System.Drawing.Color.Transparent;
            this.lblList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblList.ForeColor = System.Drawing.Color.White;
            this.lblList.Location = new System.Drawing.Point(0, 0);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(717, 26);
            this.lblList.TabIndex = 0;
            this.lblList.Text = " List";
            this.lblList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlListControlBottomBrd
            // 
            this.lbl_pnlListControlBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlListControlBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlListControlBottomBrd.Location = new System.Drawing.Point(4, 625);
            this.lbl_pnlListControlBottomBrd.Name = "lbl_pnlListControlBottomBrd";
            this.lbl_pnlListControlBottomBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlListControlBottomBrd.TabIndex = 180;
            // 
            // lbl_pnlListControlTopBrd
            // 
            this.lbl_pnlListControlTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlListControlTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlListControlTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlListControlTopBrd.Name = "lbl_pnlListControlTopBrd";
            this.lbl_pnlListControlTopBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_pnlListControlTopBrd.TabIndex = 179;
            // 
            // lbl_pnlListControlRightBrd
            // 
            this.lbl_pnlListControlRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlListControlRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlListControlRightBrd.Location = new System.Drawing.Point(721, 3);
            this.lbl_pnlListControlRightBrd.Name = "lbl_pnlListControlRightBrd";
            this.lbl_pnlListControlRightBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_pnlListControlRightBrd.TabIndex = 178;
            // 
            // lbl_pnlListControlLeftBrd
            // 
            this.lbl_pnlListControlLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlListControlLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlListControlLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlListControlLeftBrd.Name = "lbl_pnlListControlLeftBrd";
            this.lbl_pnlListControlLeftBrd.Size = new System.Drawing.Size(1, 623);
            this.lbl_pnlListControlLeftBrd.TabIndex = 177;
            // 
            // panel1
            // 
            //panel added against incident 00064417
            this.panel1.Controls.Add(this.rbSimple);
            this.panel1.Controls.Add(this.rbRecurrence);
            this.panel1.Location = new System.Drawing.Point(507, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 30);
            this.panel1.TabIndex = 185;
            // 
            // frmSetupSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(725, 684);
            this.Controls.Add(this.pnlScedules);
            this.Controls.Add(this.pnlRecurrenceContainer);
            this.Controls.Add(this.pnlListControl);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schedule";
            this.Load += new System.EventHandler(this.frmSetupSchedule_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlBlockedSchedule.ResumeLayout(false);
            this.pnlBlockedSchedule.PerformLayout();
            this.pnlCriteria_BlockedProviders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1BlockedProviders)).EndInit();
            this.pnlCriteria_BlockedProvidersHeader.ResumeLayout(false);
            this.pnlCriteria_BlockedResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1BlockedResources)).EndInit();
            this.pnlCriteria_BlockedResources_Header.ResumeLayout(false);
            this.pnlResourceSchedule.ResumeLayout(false);
            this.pnlCriteria_Resources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Resources)).EndInit();
            this.pnlCriteria_Resources_Header.ResumeLayout(false);
            this.pnlProviderSchedule.ResumeLayout(false);
            this.pnlProviderSchedule.PerformLayout();
            this.pnlCriteria_ProviderResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderResources)).EndInit();
            this.pnlCriteria_ProviderResources_Header.ResumeLayout(false);
            this.pnlCriteria_ProviderProblemType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderProblemType)).EndInit();
            this.pnlCriteria_ProviderProblemType_Header.ResumeLayout(false);
            this.pnlCriteria_ProviderUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderUsers)).EndInit();
            this.pnlCriteria_ProviderUsers_Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Duration)).EndInit();
            this.pnlRecurrance.ResumeLayout(false);
            this.pnlRecurring_Pattern.ResumeLayout(false);
            this.pnlRecurring_Pattern.PerformLayout();
            this.pnlRecurring_Pattern_Header.ResumeLayout(false);
            this.pnlRec_Pattern_Daily.ResumeLayout(false);
            this.pnlRec_Pattern_Daily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).EndInit();
            this.pnlRec_Pattern_Weekly.ResumeLayout(false);
            this.pnlRec_Pattern_Weekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).EndInit();
            this.pnlRec_Pattern_Yearly.ResumeLayout(false);
            this.pnlRec_Pattern_Yearly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).EndInit();
            this.pnlRec_Pattern_Monthly.ResumeLayout(false);
            this.pnlRec_Pattern_Monthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).EndInit();
            this.pnlRecurring_Range.ResumeLayout(false);
            this.pnlRecurring_Range.PerformLayout();
            this.pnlRecurring_Range_Header.ResumeLayout(false);
            this.pnlRecurring_Range_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).EndInit();
            this.pnlScedules.ResumeLayout(false);
            this.pnlScedules.PerformLayout();
            this.pnlApp_DateTime.ResumeLayout(false);
            this.pnlApp_DateTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).EndInit();
            this.pnlAppointmentHeader.ResumeLayout(false);
            this.pnlRecurrenceContainer.ResumeLayout(false);
            this.pnlRecurrenceContainer.PerformLayout();
            this.pnlShowRecurrence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Recurrence)).EndInit();
            this.pnlShowRecurrence_Header.ResumeLayout(false);
            this.pnlRecurrenceContainer_Header.ResumeLayout(false);
            this.pnlListControl.ResumeLayout(false);
            this.pnlListControl_Header.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlResourceSchedule;
        private System.Windows.Forms.Panel pnlProviderSchedule;
        private System.Windows.Forms.RadioButton rbResourceSchedule;
        private System.Windows.Forms.RadioButton rbProviderSchedule;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Label lblCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Panel pnlCriteria_ProviderResources;
        private System.Windows.Forms.Panel pnlCriteria_ProviderResources_Header;
        private System.Windows.Forms.Label lblCriteria_ProviderResources_Header;
        private System.Windows.Forms.Panel pnlCriteria_Resources;
        private System.Windows.Forms.Panel pnlCriteria_Resources_Header;
        private System.Windows.Forms.Label lblCriteria_Resources_Header;
        private System.Windows.Forms.Button btnClearProviderProblemType;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProviderResources;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProviderProblemType;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Resources;
        private System.Windows.Forms.Button btnClearResource;
        private System.Windows.Forms.Button btnBrowseResource;
        private System.Windows.Forms.Label lblRec_ColorContainer;
        private System.Windows.Forms.Label lblSimple_Color;
        internal System.Windows.Forms.Button btnColorCode;
        private System.Windows.Forms.Button btnClearProviderResources;
        private System.Windows.Forms.Button btnClearProviderUsers;
        private System.Windows.Forms.Button btnBrowseProviderUsers;
        private System.Windows.Forms.Panel pnlCriteria_ProviderUsers;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProviderUsers;
        private System.Windows.Forms.Panel pnlCriteria_ProviderUsers_Header;
        private System.Windows.Forms.Label lblResourcesUsers;
        private System.Windows.Forms.Panel pnlRecurrance;
        private System.Windows.Forms.Panel pnlRecurring_Pattern;
        private System.Windows.Forms.Panel pnlRec_Pattern_Daily;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyBottomBrdBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyTopBrd;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Daily_EveryDay;
        private System.Windows.Forms.Label lblRec_Pattern_Daily_Days;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryWeekday;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryDay;
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
        private System.Windows.Forms.Panel pnlRec_Pattern_Yearly;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_YearlyBottomBrdBrd;
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
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderBottomBrd;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly;
        private System.Windows.Forms.Panel pnlRecurring_Pattern_Header;
        private System.Windows.Forms.Label lblRecurring_Pattern_Header;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Weekly;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternRightBrd;
        private System.Windows.Forms.Panel pnlRecurring_Range;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Range_HeaderBottomBrd;
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
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeRightBrd;
        internal System.Windows.Forms.Label lblSimple_Duration;
        private System.Windows.Forms.NumericUpDown numRec_Duration;
        internal System.Windows.Forms.Label lblSimple_EndTime;
        internal System.Windows.Forms.DateTimePicker dtpRec_EndTime;
        internal System.Windows.Forms.DateTimePicker dtpRec_StartTime;
        internal System.Windows.Forms.Label lblSimple_StartTime;
        private System.Windows.Forms.Panel pnlBlockedSchedule;
        private System.Windows.Forms.Button btnClearBlockedResources;
        private System.Windows.Forms.Panel pnlCriteria_BlockedResources;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BlockedResources;
        private System.Windows.Forms.Panel pnlCriteria_BlockedResources_Header;
        private System.Windows.Forms.Label lblResources;
        private System.Windows.Forms.RadioButton rbBlockedSchedule;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.RichTextBox txtScheduleNote;
        private System.Windows.Forms.ComboBox cmbBlockType;
        private System.Windows.Forms.Label lblBlockType;
        private System.Windows.Forms.RadioButton rbBlockProvider;
        private System.Windows.Forms.RadioButton rbBlockResource;
        private System.Windows.Forms.Button btnBrowseBlockedProvider;
        private System.Windows.Forms.Button btnClearBlockedProvider;
        private System.Windows.Forms.Panel pnlCriteria_BlockedProviders;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BlockedProviders;
        private System.Windows.Forms.Panel pnlCriteria_BlockedProvidersHeader;
        private System.Windows.Forms.Label lblProviders;
        private System.Windows.Forms.ToolStripButton tsb_Recurrence;
        internal System.Windows.Forms.ToolStripButton tsb_RemoveRecurrence;
        internal System.Windows.Forms.ToolStripButton tsb_ApplyRecurrence;
        private System.Windows.Forms.Panel pnlScedules;
        private System.Windows.Forms.Label lbl_pnlScedulesBottomBrd;
        private System.Windows.Forms.Label lbl_pnlScedulesTopBrd;
        private System.Windows.Forms.Label lbl_pnlScedulesRightBrd;
        private System.Windows.Forms.Label lbl_pnlScedulesLeftBrd;
        private System.Windows.Forms.Panel pnlListControl;
        private System.Windows.Forms.Panel pnlListControl_Header;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.Label lbl_pnlListControlBottomBrd;
        private System.Windows.Forms.Label lbl_pnlListControlTopBrd;
        private System.Windows.Forms.Label lbl_pnlListControlRightBrd;
        private System.Windows.Forms.Label lbl_pnlListControlLeftBrd;
        private System.Windows.Forms.Panel pnlRecurrenceContainer;
        private System.Windows.Forms.Panel pnlRecurrenceContainer_Header;
        private System.Windows.Forms.Label lblRecurrence;
        private System.Windows.Forms.Label lbl_pnlRecurrenceContainerBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurrenceContainerTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurrenceContainerRightBrd;
        private System.Windows.Forms.Label lbl_lbl_pnlRecurrenceContainerLeftBrd;
        private System.Windows.Forms.Panel pnlAppointmentHeader;
        private System.Windows.Forms.Label lblAppointment;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.RadioButton rbRecurrence;
        private System.Windows.Forms.Panel pnlApp_DateTime;
        internal System.Windows.Forms.Label lbl_pnlApp_DateTimeBootomBrd;
        internal System.Windows.Forms.Label lbl_pnlApp_DateTimeTopBrd;
        private System.Windows.Forms.Label lbl_pnlApp_DateTimeRightBrd;
        private System.Windows.Forms.Label lbl_pnlApp_DateTimeLeftBrd;
        private System.Windows.Forms.Label lblDurationUnit;
        internal System.Windows.Forms.CheckBox chkApp_DateTime_IsAllDayEvent;
        internal System.Windows.Forms.Label lblApp_DateTime_Date;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartTime;
        internal System.Windows.Forms.Label lblApp_ColorContainer;
        internal System.Windows.Forms.Label lblApp_DateTime_Duration;
        internal System.Windows.Forms.Label lblApp_DateTime_Time;
        private System.Windows.Forms.Label lblApp_DateTime_Color;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartDate;
        internal System.Windows.Forms.Button btnApp_DateTime_Color;
        internal System.Windows.Forms.NumericUpDown numApp_DateTime_Duration;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeleftBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeTopBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderUsersBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderUsers_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderUsersRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderUsersTopBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderUsersLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderResourcesBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderResources_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderResourcesRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderResourcesTopBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderResourcesLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlBlockedScheduleBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlBlockedScheduleTopBrd;
        private System.Windows.Forms.Label lbl_pnlBlockedScheduleRightBrd;
        private System.Windows.Forms.Label lbl_pnlBlockedScheduleLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlResourceScheduleBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlResourceScheduleTopBrd;
        private System.Windows.Forms.Label lbl_pnlResourceScheduleRightBrd;
        private System.Windows.Forms.Label lbl_pnlResourceScheduleLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlProviderScheduleBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlProviderScheduleTopBrd;
        private System.Windows.Forms.Label lbl_pnlProviderScheduleRightBrd;
        private System.Windows.Forms.Label lbl_lbl_pnlProviderScheduleLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedProvidersBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedProvidersTopBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_BlockedProvidersRightBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_BlockedProvidersLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedResourcesBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedResourcesTopBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_BlockedResourcesRightBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_BlockedResourcesLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ResourcesBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ResourcesTopBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ResourcesRightBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ResourcesLeftBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_Resources_HeaderBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedProvidersHeaderBootomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_BlockedResources_HeaderBottomBrd;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Panel pnlShowRecurrence;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Recurrence;
        private System.Windows.Forms.Panel pnlShowRecurrence_Header;
        internal System.Windows.Forms.Label lbl_pnlShowRecurrence_HeaderBottomBrd1;
        private System.Windows.Forms.Label lblSchedules;
        internal System.Windows.Forms.Label lbl_pnlShowRecurrenceBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlShowRecurrenceTopBrd;
        private System.Windows.Forms.Label lbl_pnlShowRecurrenceRightBrd;
        private System.Windows.Forms.Label lbl_pnlShowRecurrenceLeftBrd;
        internal System.Windows.Forms.ToolStripButton tsb_ShowRecurrence;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_EndTime;
        internal System.Windows.Forms.ToolStripButton tsb_CancelRecurrence;
        internal System.Windows.Forms.Label lblApp_Recurrence_Time;
        private System.Windows.Forms.Label lbl_pnlAppointmentHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlListControl_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurrenceContainer_HeaderBottomBrd;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_ProviderAsterix;
        private System.Windows.Forms.ColorDialog colorDialog1;
        internal System.Windows.Forms.Button btnBrowseProviderResources;
        internal System.Windows.Forms.Button btnBrowseProviderProblemType;
        private System.Windows.Forms.Button btnBrowseBlockedResources;
        private System.Windows.Forms.Panel panel1;
    }
}